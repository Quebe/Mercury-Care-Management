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

    public class Button : Control {

        #region Private Properties

        private String text = String.Empty;

        #endregion


        #region Public Properties

        public String Text { get { return text; } set { text = value; } }

        #endregion


        #region Constructor

        virtual protected void InitializeControl (Application applicationReference) {

            BaseConstructor (applicationReference);

            ControlType = Server.Application.FormControlType.Button;

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

        public Button (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Server.Application.FormControlButton serverButton) {

            InitializeControl (applicationReference);

            BaseConstructor (parentControl, serverButton);

            text = serverButton.Text;

            ChildServerControlsToLocal (this, serverButton);

            return;

        }

        #endregion


        #region Virtual Overrides

        public override void LocalControlToServer (Server.Application.FormControl parentControl, Server.Application.FormControl serverControl) {

            base.LocalControlToServer (parentControl, serverControl);

            ((Server.Application.FormControlButton) serverControl).Text = text;

            return;
        }

        //public override Boolean EventHandlers_AllSmart () { return false; }

        #endregion


        #region Public Methods

        public void Click () {

            //RaiseEvent ("Click");

            return;

        }

        #endregion 


    }

}
