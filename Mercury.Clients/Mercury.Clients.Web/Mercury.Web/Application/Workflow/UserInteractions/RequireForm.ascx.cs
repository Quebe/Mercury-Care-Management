using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Workflow.UserInteractions {

    public partial class RequireForm : System.Web.UI.UserControl {


        #region Private Properties

        private const String ResponseScriptPaint = "setTimeout (\"UserInteractionRequireForm_OnPaint ();\", 100);";

        private Server.Application.WorkflowUserInteractionRequestRequireForm requireFormRequest;

        private Telerik.Web.UI.RadAjaxManager TelerikAjaxManager { get { return (Telerik.Web.UI.RadAjaxManager)Page.FindControl ("TelerikAjaxManager"); } }

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
        
        public String ResponseScript { get { return (String)Session[SessionCachePrefix + "ResponseScript"]; } set { Session[SessionCachePrefix + "ResponseScript"] = value; } }

        #endregion


        #region Public Properties

        public Boolean AllowSaveAsDraft { get { return SaveAsDraftButton.Visible; } set { SaveAsDraftButton.Visible = value; } }

        public Boolean AllowCancel { get { return CancelButton.Visible; } set { CancelButton.Visible = value; } }

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            FormEditorControl.SetFormInstanceId (WorkflowPage.SessionCachePrefix);
            
            requireFormRequest = (Server.Application.WorkflowUserInteractionRequestRequireForm)WorkflowPage.UserInteractionRequest;

            if (WorkflowPage != null) { WorkflowPage.WorkflowAjaxManager.ResponseScripts.Add ("setTimeout (\"Workflow_OnPaint ();\", 100);setTimeout (\"UserInteractionRequireForm_OnPaint ();\", 200);"); }

            return;

        }

        #endregion
        

        #region Initialization

        public void SetForm (Client.Core.Forms.Form forForm) {

            FormEditorControl.SetForm (forForm);

            return;

        }

        #endregion 
        

        #region Events

        public void SubmitButton_OnClick (Object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            if (FormEditorControl == null) { return; }

            if (FormEditorControl.EditorForm == null) { return; }


            Client.Core.Forms.Form editorForm = FormEditorControl.EditorForm;

            List<Server.Application.FormCompileMessage> validationResponse = editorForm.Submit ();

            FormEditorControl.EditorForm = editorForm;

            if (validationResponse.Count != 0) {

                System.Data.DataTable compileOutputTable = new System.Data.DataTable ();

                compileOutputTable.Columns.Add ("MessageType");
                compileOutputTable.Columns.Add ("Description");
                compileOutputTable.Columns.Add ("ControlId");
                compileOutputTable.Columns.Add ("ControlType");
                compileOutputTable.Columns.Add ("ControlName");

                foreach (Server.Application.FormCompileMessage currentMessage in validationResponse) {

                    compileOutputTable.Rows.Add (currentMessage.MessageType.ToString (), currentMessage.Description, currentMessage.ControlId, currentMessage.ControlType.ToString (), currentMessage.ControlName);

                }

                FormSubmitGrid.DataSource = compileOutputTable;

                FormSubmitGrid.Rebind ();

                FormSubmitGridDiv.Visible = true;


                if (TelerikAjaxManager != null) {

                    TelerikAjaxManager.ResponseScripts.Add ("setTimeout (\"document.getElementById ('" + FormSubmitGrid.ClientID + "').focus ();\", 250);");

                    //TelerikAjaxManager.ResponseScripts.Add ("setTimeout (\"alert (document.getElementById('" + FormSubmitGridDiv.ClientID + "'));\", 1000");

                    //TelerikAjaxManager.ResponseScripts.Add ("setTimeout (\"document.getElementById('" + FormSubmitGridDiv.ClientID + "').focus();\", 1000");

                }

            }

            else {

                Server.Application.WorkflowUserInteractionResponseRequireForm formResponse = new Mercury.Server.Application.WorkflowUserInteractionResponseRequireForm ();

                formResponse.InteractionType = Mercury.Server.Application.UserInteractionType.RequireForm;

                formResponse.Form = (Server.Application.Form)FormEditorControl.EditorForm.ToServerObject ();


                WorkflowPage.UserInteractionResponse = formResponse;


                if (!String.IsNullOrEmpty (ResponseScript)) {

                    Telerik.Web.UI.RadAjaxManager ajaxManager = (Telerik.Web.UI.RadAjaxManager)Page.FindControl ("TelerikAjaxManager");

                    ajaxManager.ResponseScripts.Add (ResponseScript);

                }

            }

            return;

        }

        public void SaveAsDraftButton_OnClick (Object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            if (FormEditorControl == null) { return; }

            if (FormEditorControl.EditorForm == null) { return; }


            Client.Core.Forms.Form editorForm = FormEditorControl.EditorForm;

            FormEditorControl.EditorForm = editorForm;


            Server.Application.WorkflowUserInteractionResponseRequireForm formResponse = new Mercury.Server.Application.WorkflowUserInteractionResponseRequireForm ();

            formResponse.InteractionType = Mercury.Server.Application.UserInteractionType.RequireForm;

            formResponse.Form = (Server.Application.Form)FormEditorControl.EditorForm.ToServerObject ();

            formResponse.SaveAsDraft = true;

            formResponse.Cancel = false;


            WorkflowPage.UserInteractionResponse = formResponse;


            if (!String.IsNullOrEmpty (ResponseScript)) {

                Telerik.Web.UI.RadAjaxManager ajaxManager = (Telerik.Web.UI.RadAjaxManager)Page.FindControl ("TelerikAjaxManager");

                ajaxManager.ResponseScripts.Add (ResponseScript);

            }

        }

        public void CancelButton_OnClick (Object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            if (FormEditorControl == null) { return; }

            if (FormEditorControl.EditorForm == null) { return; }


            Mercury.Server.Application.WorkflowUserInteractionResponseRequireForm formResponse = new Mercury.Server.Application.WorkflowUserInteractionResponseRequireForm ();

            formResponse.InteractionType = Mercury.Server.Application.UserInteractionType.RequireForm;

            formResponse.Cancel = true;


            WorkflowPage.UserInteractionResponse = formResponse;


            if (!String.IsNullOrEmpty (ResponseScript)) {

                Telerik.Web.UI.RadAjaxManager ajaxManager = (Telerik.Web.UI.RadAjaxManager)Page.FindControl ("TelerikAjaxManager");

                ajaxManager.ResponseScripts.Add (ResponseScript);

            }

        }

        public void PrintButton_OnClick (Object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            if (FormEditorControl == null) { return; }

            if (FormEditorControl.EditorForm == null) { return; }



            String printScript = "window.open ('/Application/Forms/FormViewer/FormViewer.aspx?EntityFormSessionKey=" + FormEditorControl.EditorFormCacheKey + "&ForPrint=True', '_blank', 'toolbar=0, location=0, directories=0, status=1, menubar=0, scrollbars=1, resizable=1');";
            


            if (!String.IsNullOrEmpty (ResponseScript)) {

                Telerik.Web.UI.RadAjaxManager ajaxManager = (Telerik.Web.UI.RadAjaxManager)Page.FindControl ("TelerikAjaxManager");

                ajaxManager.ResponseScripts.Add (printScript);

            }

        }

        #endregion 
    }

}