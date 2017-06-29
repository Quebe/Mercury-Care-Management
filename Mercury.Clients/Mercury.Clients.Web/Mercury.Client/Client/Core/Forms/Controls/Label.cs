using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Forms.Controls {

    [Serializable]
    public class Label : Mercury.Client.Core.Forms.Control {

        #region Private Properties

        String text = String.Empty;

        #endregion


        #region Public Properties

        public String Text { get { return text; } set { text = value; } }

        public override String JsonExtendedProperties {

            get {

                StringBuilder jsonBuilder = new StringBuilder ();

                jsonBuilder.Append (", " + JsonObjectProperty ("Text", text));

                return jsonBuilder.ToString ();

            }

        }


        #endregion


        #region Constructors

        protected void InitializeControl (Application applicationReference) {

            BaseConstructor (applicationReference);

            ControlType = Mercury.Server.Application.FormControlType.Label;

            text = "Label:";

            return;

        }

        public Label (Application applicationReference) {

            InitializeControl (applicationReference);

            return;

        }

        public Label (Application applicationReference, Control parentControl) {

            InitializeControl (applicationReference);

            parent = parentControl;

            return;

        }

        public Label (Application applicationReference, String text) {

            InitializeControl (applicationReference);

            this.text = text;

            return;

        }

        public Label (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Mercury.Server.Application.FormControlLabel serverLabel) {

            BaseConstructor (applicationReference, parentControl, serverLabel);

            InitializeControl (applicationReference);

            text = serverLabel.Text;

            return;

        }

        #endregion


        #region Virtual Overrides

        public override void LocalControlToServer (Mercury.Server.Application.FormControl parentControl, Mercury.Server.Application.FormControl serverControl) {

            base.LocalControlToServer (parentControl, serverControl);

            ((Mercury.Server.Application.FormControlLabel) serverControl).Text = text;

            return;

        }

        #endregion

    }

}
