using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Mercury.Client.Core.Forms.Controls {

    public class Label : Control {

        #region Private Properties

        String text = String.Empty;

        #endregion


        #region Public Properties

        public String Text { 
            
            get { return text; } 
            
            set {

                if (text != value) {

                    text = value;

                    NotifyPropertyChanged ("Text");

                }
            
            } 
        
        }



        #endregion


        #region Constructors

        protected void InitializeControl (Application applicationReference) {

            BaseConstructor (applicationReference);

            ControlType = Server.Application.FormControlType.Label;

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

        public Label (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Server.Application.FormControlLabel serverLabel) {

            InitializeControl (applicationReference);

            BaseConstructor (parentControl, serverLabel);

            text = serverLabel.Text;

            return;

        }

        #endregion


        #region Virtual Overrides

        public override void LocalControlToServer (Server.Application.FormControl parentControl, Server.Application.FormControl serverControl) {

            base.LocalControlToServer (parentControl, serverControl);

            ((Server.Application.FormControlLabel) serverControl).Text = text;

            return;

        }

        #endregion

    }

}
