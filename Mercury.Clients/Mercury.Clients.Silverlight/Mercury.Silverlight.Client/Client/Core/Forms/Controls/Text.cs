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

    public class Text : Control {

        #region Private Properties

        private String text = String.Empty;

        #endregion


        #region Public Properties

        public String TextContent {

            get { return text; }

            set {

                if (text != value) {

                    text = value;

                    NotifyPropertyChanged ("TextContent");

                    NotifyPropertyChanged ("TextContentHtmlStripped");

                    NotifyPropertyChanged ("HasValue");

                    NotifyPropertyChanged ("Value");

                }

            }

        }

        public String TextContentHtmlStripped {

            get {

                String textStripped = text.Trim ();

                //textStripped = textStripped.Replace ("<em>", "");

                //textStripped = textStripped.Replace ("</em>", "");

                //textStripped = textStripped.Replace ("<strong>", "");

                //textStripped = textStripped.Replace ("</strong>", "");

                textStripped = textStripped.Replace ("<br />", "\r\n");

                textStripped = System.Text.RegularExpressions.Regex.Replace (textStripped, @"<(.|\n)*?>", String.Empty);

                // TODO: OTHER STRIPPING

                return textStripped; 
            
            }

        }

        public override Boolean HasValue { get { return (!String.IsNullOrEmpty (text)); } }

        public override String Value { get { return text; } }

        #endregion


        #region Silverlight Public Properties

        public String SlTextContentFormatted {

            get {

                String content = "<div style=\"" + style.StyleAttributeTextOnly + "\">" + text + "</div>";

                return String.Format (content);

            }

        }

        #endregion 


        #region Constructor

        virtual protected void InitializeControl (Application applicationReference) {

            BaseConstructor (applicationReference);

            ControlType = Server.Application.FormControlType.Text;

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

        public Text (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Server.Application.FormControlText serverText) {

            InitializeControl (applicationReference);

            BaseConstructor (parentControl, serverText);

            text = serverText.Text;

            text = text.Trim ();

            ChildServerControlsToLocal (this, serverText);

            return;

        }

        #endregion


        #region Virtual Overrides

        public override void LocalControlToServer (Server.Application.FormControl parentControl, Server.Application.FormControl serverControl) {

            base.LocalControlToServer (parentControl, serverControl);

            ((Server.Application.FormControlText) serverControl).Text = text;

            return;
        }

        #endregion

    }

}
