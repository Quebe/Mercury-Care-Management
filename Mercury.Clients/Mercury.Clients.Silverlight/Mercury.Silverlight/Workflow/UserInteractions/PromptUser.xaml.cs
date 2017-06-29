using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Mercury.Silverlight.Workflow.UserInteractions {

    public partial class PromptUser : UserControl {

        #region Private Properties

        private Client.Application MercuryApplication = ((App)Application.Current).MercuryApplication;

        private WindowManager.WindowManager WindowManager = ((App)Application.Current).WindowManager;


        private Server.Application.WorkflowUserInteractionRequestPromptUser promptRequest;

        private Server.Application.WorkflowUserInteractionResponsePromptUser promptResponse;

        #endregion         

        
        #region Public Properties

        public event EventHandler<PromptUserResponseEventArgs> Completed;

        #endregion 


        #region Constructors

        public PromptUser () {

            InitializeComponent ();

        }

        #endregion


        #region Initialization

        private String ParseSmartTag (String smartString) {

            const String smartTagDefinition = "{SMART";

            String parsedString = smartString;

            Int32 lastPosition = 0;

            try {

                while (parsedString.IndexOf (smartTagDefinition, lastPosition) > -1) {

                    Int32 initialPosition = parsedString.IndexOf (smartTagDefinition);

                    Int32 finalPosition = parsedString.IndexOf ("}", initialPosition);

                    if (finalPosition == -1) { break; }


                    String smartTag = parsedString.Substring (initialPosition, (finalPosition - initialPosition) + 1);

                    smartTag = smartTag.Substring (1, smartTag.Length - 2);

                    String smartTagType = smartTag.Split ('|')[1].Split ('=')[1];

                    String smartTagId = smartTag.Split ('|')[2].Split ('=')[1];

                    String smartTagText = smartTag.Split ('|')[3].Split ('=')[1];

                    switch (smartTagType) {

                        case "Form":

                            // TODO: SILVERLIGHT UPDATE

                            // smartTag = Web.CommonFunctions.FormAnchor (Convert.ToInt64 (smartTagId), smartTagText);

                            smartTag = smartTagText;

                            break;

                        case "Member":

                            // TODO: SILVERLIGHT UPDATE

                            // smartTag = Web.CommonFunctions.MemberProfileAnchor (Convert.ToInt64 (smartTagId), smartTagText);

                            smartTag = smartTagText;

                            break;

                    }

                    String parsedHeader = parsedString.Substring (0, initialPosition);

                    String parsedFooter = parsedString.Substring (finalPosition + 1, parsedString.Length - finalPosition - 1);

                    parsedString = parsedHeader + smartTag + parsedFooter;

                    lastPosition = initialPosition + 1;

                    if (lastPosition > parsedString.Length) { lastPosition = parsedString.Length; }

                }

            }

            catch { /* DO NOTHING */ }

            return parsedString;

        }

        public void InitializePrompt (Server.Application.WorkflowUserInteractionRequestPromptUser forPromptRequest) {

            promptRequest = forPromptRequest;

            PromptTitle.Text = promptRequest.PromptTitle;

            PromptMessage.Text = ParseSmartTag (promptRequest.PromptMessage);

            if (promptRequest.PromptImage == Mercury.Server.Application.UserPromptImage.NoImage) {

                PromptImage.Visibility = System.Windows.Visibility.Collapsed;

            }

            else {

                PromptImage.Source = new System.Windows.Media.Imaging.BitmapImage (new Uri ("../../Images/Common32/" + promptRequest.PromptImage.ToString () + ".png", UriKind.Relative));

            }

            switch (promptRequest.PromptType) {

                case Mercury.Server.Application.UserPromptType.ConfirmationYesNo:

                    ButtonOk.Content = "Yes";

                    ButtonCancel.Content = "No";

                    break;

                case Mercury.Server.Application.UserPromptType.Selection:

                    PromptSelectionItemsSelection.Visibility = System.Windows.Visibility.Visible;

                    ButtonCancel.IsEnabled = promptRequest.AllowCancel;

                    if (promptRequest.SelectionItems != null) {

                        foreach (Mercury.Server.Application.WorkflowUserInteractionRequestPromptSelectionItem currentSelectionItem in promptRequest.SelectionItems) {

                            Telerik.Windows.Controls.RadComboBoxItem selectionItem = new Telerik.Windows.Controls.RadComboBoxItem ();

                            selectionItem.Content = currentSelectionItem.Text;

                            selectionItem.Tag = currentSelectionItem.Value;

                            selectionItem.IsEnabled = currentSelectionItem.Enabled;

                            selectionItem.IsSelected = currentSelectionItem.Selected;

                            PromptSelectionItemsSelection.Items.Add (selectionItem);

                        }

                        if (PromptSelectionItemsSelection.Items.Count > 0) { PromptSelectionItemsSelection.SelectedIndex = 0; }

                    }

                    break;

            }

            return;

        }

        #endregion


        #region Button Clicks

        protected void ButtonOkCancel_OnClick (Object sender, RoutedEventArgs e) {

            promptResponse = new Mercury.Server.Application.WorkflowUserInteractionResponsePromptUser ();

            promptResponse.InteractionType = Mercury.Server.Application.UserInteractionType.Prompt;


            switch (((Button)sender).Content.ToString ().Trim ().ToLower ()) {

                case "ok": promptResponse.ButtonClicked = Mercury.Server.Application.UserPromptButtonClicked.Ok; break;

                case "cancel": promptResponse.ButtonClicked = Mercury.Server.Application.UserPromptButtonClicked.Cancel; break;

                case "yes": promptResponse.ButtonClicked = Mercury.Server.Application.UserPromptButtonClicked.Yes; break;

                case "no": promptResponse.ButtonClicked = Mercury.Server.Application.UserPromptButtonClicked.No; break;

                default: promptResponse.ButtonClicked = Mercury.Server.Application.UserPromptButtonClicked.None; break;

            }

            if (PromptSelectionItemsSelection.SelectedItem != null) {

                promptResponse.SelectedValue = ((FrameworkElement)PromptSelectionItemsSelection.SelectedItem).Tag.ToString ();

                promptResponse.SelectedText = ((Telerik.Windows.Controls.RadComboBoxItem)PromptSelectionItemsSelection.SelectedItem).Content.ToString ();

            }

            if (Completed != null) {

                PromptUserResponseEventArgs eventArgs = new PromptUserResponseEventArgs (promptResponse);

                Completed (this, eventArgs);

            }

            return;

        }

        #endregion

    
    }

    public class PromptUserResponseEventArgs : EventArgs {

        public Server.Application.WorkflowUserInteractionResponsePromptUser PromptResponse { get; set; }
        
        public PromptUserResponseEventArgs (Server.Application.WorkflowUserInteractionResponsePromptUser forPromptResponse) {

            PromptResponse = forPromptResponse;

            return;

        }

    }

}
