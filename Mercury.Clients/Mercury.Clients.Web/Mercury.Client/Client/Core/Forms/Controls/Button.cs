using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Forms.Controls {

    [Serializable]
    public class Button : Client.Core.Forms.Control {
        
        #region Private Properties

        private String text = String.Empty;

        #endregion


        #region Public Properties

        public String Text { get { return text; } set { text = value; } }

        public override String JsonExtendedProperties {

            get {

                String json = ", \"Text\": \"" + text.Replace ("\"", @"\""").Replace (System.Environment.NewLine, "") + "\"";

                System.Diagnostics.Debug.WriteLine ("Text: " + json);

                return json;

            }

        }

        #endregion


        #region Constructor

        virtual protected void InitializeControl (Application applicationReference) {

            BaseConstructor (applicationReference);

            ControlType = Mercury.Server.Application.FormControlType.Button;

            return; 

        }

        public Button (Application applicationReference) {

            InitializeControl (applicationReference);

            return;

        }

        public Button (Application applicationReference, String forText) {

            InitializeControl (applicationReference);

            text = forText;

            return;

        }

        public Button (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Mercury.Server.Application.FormControlButton serverButton) {

            InitializeControl (applicationReference);

            BaseConstructor (applicationReference, parentControl, serverButton);

            text = serverButton.Text;

            ChildServerControlsToLocal (this, serverButton);

            return;

        }

        #endregion


        #region Virtual Overrides

        public override void LocalControlToServer (Mercury.Server.Application.FormControl parentControl, Mercury.Server.Application.FormControl serverControl) {

            base.LocalControlToServer (parentControl, serverControl);

            ((Mercury.Server.Application.FormControlButton) serverControl).Text = text;

            return;
        }

        public override Boolean EventHandlers_AllSmart () { return false;  }

        #endregion


        #region Public Methods

        public void Click () {

            RaiseEvent ("Click");

            return;

        }

        #endregion 

    }

}
