using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Forms.Controls {

    [Serializable]
    public class Text : Mercury.Client.Core.Forms.Control {

        #region Private Properties

        private String text = String.Empty;

        #endregion


        #region Public Properties

        public String TextContent { get { return text; } set { text = value; } }

        public override Boolean HasValue { get { return (!String.IsNullOrEmpty (text)); } }

        public override String Value { get { return text; } }

        public override String JsonExtendedProperties {

            get {

                String json = ", \"Text\": \"" + TextContent.Replace ("\"", @"\""").Replace (System.Environment.NewLine, "") + "\"";

                System.Diagnostics.Debug.WriteLine ("Text: " + json);

                return json;

            }
        
        }

        #endregion


        #region Constructor

        virtual protected void InitializeControl (Application applicationReference) {

            BaseConstructor (applicationReference);

            ControlType = Mercury.Server.Application.FormControlType.Text;

            return; 

        }

        public Text (Application applicationReference) {

            InitializeControl (applicationReference);

            return;

        }

        public Text (Application applicationReference, String forText) {

            InitializeControl (applicationReference);

            text = forText;

            return;

        }

        public Text (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Mercury.Server.Application.FormControlText serverText) {

            InitializeControl (applicationReference);

            BaseConstructor (applicationReference, parentControl, serverText);

            text = serverText.Text;

            ChildServerControlsToLocal (this, serverText);

            return;

        }

        #endregion


        #region Virtual Overrides

        public override void LocalControlToServer (Mercury.Server.Application.FormControl parentControl, Mercury.Server.Application.FormControl serverControl) {

            base.LocalControlToServer (parentControl, serverControl);

            ((Mercury.Server.Application.FormControlText) serverControl).Text = text;

            return;
        }

        #endregion

    }

}
