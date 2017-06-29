using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Workflow.UserInteractions {

    public partial class PromptUser : System.Web.UI.UserControl {

        #region Private Properties

        private Server.Application.WorkflowUserInteractionRequestPromptUser promptRequest;

        private Server.Application.WorkflowUserInteractionResponsePromptUser promptResponse;

        #endregion


        #region State Properties

        public Mercury.Web.Application.Workflow.Workflow WorkflowPage { get { return (Mercury.Web.Application.Workflow.Workflow)Page; } }

        public String SessionCachePrefix { get { return WorkflowPage.SessionCachePrefix + this.GetType ().ToString (); } }

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application)Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        public Client.Core.Entity.Entity Entity {

            get { return (Client.Core.Entity.Entity)Session[SessionCachePrefix + "Entity"]; }

            set {

                Client.Core.Entity.Entity entity = (Client.Core.Entity.Entity)Session[SessionCachePrefix + "Entity"];

                if (entity != value) {

                    Session[SessionCachePrefix + "Entity"] = value;

                }

            }

        }

        public String ResponseScript { get { return (String)Session[SessionCachePrefix + "ResponseScript"]; } set { Session[SessionCachePrefix + "ResponseScript"] = value; } }

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }


            InitializePrompt ();

            return;

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

                            smartTag = Web.CommonFunctions.FormAnchor (Convert.ToInt64 (smartTagId), smartTagText);

                            break;

                        case "Member":

                            smartTag = Web.CommonFunctions.MemberProfileAnchor (Convert.ToInt64 (smartTagId), smartTagText);

                            break;

                        case "Provider":

                            smartTag = Web.CommonFunctions.ProviderProfileAnchor (Convert.ToInt64 (smartTagId), smartTagText);

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

        private void InitializePrompt () {

            promptRequest = (Server.Application.WorkflowUserInteractionRequestPromptUser)WorkflowPage.UserInteractionRequest;


            PromptTitle.Text = ParseSmartTag (promptRequest.PromptTitle);

            PromptMessage.Text = ParseSmartTag (promptRequest.PromptMessage);

            if (promptRequest.PromptImage == Mercury.Server.Application.UserPromptImage.NoImage) {

                PromptImage.Visible = false;

            }

            else {

                PromptImage.Src = "/Images/Common32/" + promptRequest.PromptImage.ToString () + ".png";

            }

            switch (promptRequest.PromptType) {

                case Mercury.Server.Application.UserPromptType.ConfirmationYesNo:

                    ButtonOk.Text = "Yes";

                    ButtonCancel.Text = "No";

                    break;

                case Mercury.Server.Application.UserPromptType.Selection:

                    PromptSelectionItemsRow.Visible = true;

                    ButtonCancel.Enabled = promptRequest.AllowCancel;

                    if (promptRequest.SelectionItems != null) {

                        foreach (Mercury.Server.Application.WorkflowUserInteractionRequestPromptSelectionItem currentSelectionItem in promptRequest.SelectionItems) {

                            Telerik.Web.UI.RadComboBoxItem selectionItem = new Telerik.Web.UI.RadComboBoxItem (currentSelectionItem.Text, currentSelectionItem.Value);

                            selectionItem.Enabled = currentSelectionItem.Enabled;

                            selectionItem.Selected = currentSelectionItem.Selected;

                            PromptSelectionItemsSelection.Items.Add (selectionItem);

                        }

                    }

                    break;

            }

            return;

        }

        #endregion


        #region Button Clicks

        protected void ButtonOkCancel_OnClick (Object sender, EventArgs eventArgs) {

            promptRequest = (Mercury.Server.Application.WorkflowUserInteractionRequestPromptUser)WorkflowPage.UserInteractionRequest;

            promptResponse = new Mercury.Server.Application.WorkflowUserInteractionResponsePromptUser ();

            promptResponse.InteractionType = Mercury.Server.Application.UserInteractionType.Prompt;


            switch (((Button)sender).Text.Trim ().ToLower ()) {

                case "ok": promptResponse.ButtonClicked = Mercury.Server.Application.UserPromptButtonClicked.Ok; break;

                case "cancel": promptResponse.ButtonClicked = Mercury.Server.Application.UserPromptButtonClicked.Cancel; break;

                case "yes": promptResponse.ButtonClicked = Mercury.Server.Application.UserPromptButtonClicked.Yes; break;

                case "no": promptResponse.ButtonClicked = Mercury.Server.Application.UserPromptButtonClicked.No; break;

                default: promptResponse.ButtonClicked = Mercury.Server.Application.UserPromptButtonClicked.None; break;

            }

            if (PromptSelectionItemsSelection.SelectedItem != null) {

                promptResponse.SelectedValue = PromptSelectionItemsSelection.SelectedItem.Value;

                promptResponse.SelectedText = PromptSelectionItemsSelection.SelectedItem.Text;

            }

            WorkflowPage.UserInteractionResponse = promptResponse;


            if (!String.IsNullOrEmpty (ResponseScript)) {

                Telerik.Web.UI.RadAjaxManager ajaxManager = (Telerik.Web.UI.RadAjaxManager)Page.FindControl ("TelerikAjaxManager");

                ajaxManager.ResponseScripts.Add (ResponseScript);

            }

            return;

        }

        #endregion 

    }

}