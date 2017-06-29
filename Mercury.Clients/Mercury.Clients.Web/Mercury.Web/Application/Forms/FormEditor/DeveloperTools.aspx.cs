using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Forms.FormEditor {

    public partial class DeveloperTools : System.Web.UI.Page {


        #region Public Properties

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application) Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        public String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (FormInstanceId.Text)) { FormInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return FormInstanceId.Text + ".";

            }

        }

        public String ResponseScript {

            get { return (String) Session[SessionCachePrefix + "ResponseScript"]; }

            set { Session[SessionCachePrefix + "ResponseScript"] = value; }

        }

        public Mercury.Client.Core.Forms.Form EditorForm {

            get {

                Mercury.Client.Core.Forms.Form editorForm = (Mercury.Client.Core.Forms.Form) Session[SessionCachePrefix + "EditorForm"];

                return editorForm;

            }

            set { Session[SessionCachePrefix + "EditorForm"] = value; }

        }

        public List<Client.Core.Forms.EventResult> EditorFormEventResults {

            get {

                List<Client.Core.Forms.EventResult> eventResults = (List<Client.Core.Forms.EventResult>) Session[SessionCachePrefix + "EditorFormEventResults"];

                if (eventResults == null) { eventResults = new List<Mercury.Client.Core.Forms.EventResult> (); }

                return eventResults;

            }

            set { Session[SessionCachePrefix + "EditorFormEventResults"] = value; }

        }

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            if (!Page.IsPostBack) {

                if (String.IsNullOrEmpty (Request.QueryString["FormInstanceId"])) { Response.Redirect ("/PermissionDenied.aspx", true); return; }

                FormInstanceId.Text = (String) Request.QueryString["FormInstanceId"];


                InitializeDeveloperTools ();

            }

            return;

        }

        #endregion 


        #region Initialization

        private void InitializeDeveloperTools () {

            InitializeFormExplorerTree ();

            InitializeEventResultListener ();

            return;

        }

        private void FormExplorerTree_AddNode (Telerik.Web.UI.RadTreeNode parentNode, Client.Core.Forms.Control formControl) {

            Telerik.Web.UI.RadTreeNode currentNode;

            String nodeText = formControl.Name;

            if ((formControl.ReadOnly) || (!formControl.Visible) || (formControl.Required)) {

                if (formControl.Required) { nodeText = nodeText + " { Required }"; }

                if (formControl.ReadOnly) { nodeText = nodeText + " { Read Only }"; }

                if (!formControl.Visible) { nodeText = nodeText + " { Not Visible }"; }

            }


            currentNode = new Telerik.Web.UI.RadTreeNode ();

            currentNode.Text = nodeText;

            currentNode.Value = formControl.ControlId.ToString ();

            currentNode.Category = formControl.ControlType.ToString ();

            currentNode.ImageUrl = "/Images/Common16/" + formControl.ControlType.ToString () + ".png";

            currentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

            parentNode.Nodes.Add (currentNode);

            foreach (Client.Core.Forms.Control currentChildControl in formControl.Controls) {

                FormExplorerTree_AddNode (currentNode, currentChildControl);

            }

            return;

        }

        private void InitializeFormExplorerTree () {

            Telerik.Web.UI.RadTreeNode rootNode = new Telerik.Web.UI.RadTreeNode ();

            FormExplorerTree_AddNode (rootNode, EditorForm);

            FormExplorerTree.Nodes.Add (rootNode.Nodes[0]);

            FormExplorerTree.Nodes[0].Expanded = true;

            return;

        }

        private void InitializeEventResultListener () {

            ToolsOutput.Text = String.Empty;

            foreach (Client.Core.Forms.EventResult currentResult in EditorFormEventResults) {

                ToolsOutput.Text = ToolsOutput.Text + currentResult.ListenerOutput + "\r\n";

                if (currentResult.HasException) {

                    Exception currentException = currentResult.LastException;

                    while (currentException != null) {

                        ToolsOutput.Text = ToolsOutput.Text + "** Exception ** [" + currentException.Source + "]: " + currentException.Message + "\r\n";

                        ToolsOutput.Text = ToolsOutput.Text + "    [" + currentException.TargetSite + "] " + currentException.StackTrace + "\r\n";

                        currentException = currentException.InnerException;

                    }

                }

            }

            return;

        }

        #endregion 


        #region Private Methods

        private void FormExplorerTree_Refresh () {

            foreach (Client.Core.Forms.Control currentControl in EditorForm.GetAllControls ()) {

                Telerik.Web.UI.RadTreeNode controlNode = FormExplorerTree.FindNodeByValue (currentControl.ControlId.ToString ());

                if (controlNode != null) {

                    String nodeText = currentControl.Name;

                    if ((currentControl.ReadOnly) || (!currentControl.Visible) || (currentControl.Required)) {

                        if (currentControl.Required) { nodeText = nodeText + " { Required }"; }

                        if (currentControl.ReadOnly) { nodeText = nodeText + " { Read Only }"; }

                        if (!currentControl.Visible) { nodeText = nodeText + " { Not Visible }"; }

                    }

                    controlNode.Text = nodeText;

                }

            }

            return;

        }

        private void FormControlPropertiesGrid_Refresh () {

            Telerik.Web.UI.RadTreeNode activeNode = FormExplorerTree.SelectedNode;

            System.Data.DataTable propertiesTable = new System.Data.DataTable ();

            propertiesTable.Columns.Add ("Name");

            propertiesTable.Columns.Add ("Value");

            propertiesTable.Columns.Add ("Type");

            if (activeNode != null) {

                Client.Core.Forms.Control selectedControl = EditorForm.FindControlById (new Guid (activeNode.Value));

                if (selectedControl != null) {

                    foreach (System.Reflection.PropertyInfo currentPropertyInfo in selectedControl.GetType ().GetProperties ()) {


                        propertiesTable.Rows.Add (

                            currentPropertyInfo.Name,

                            (currentPropertyInfo.CanRead) ? 
                            
                                ((currentPropertyInfo.GetValue (selectedControl, null) != null) ?  currentPropertyInfo.GetValue (selectedControl, null).ToString () : "null")
                                
                                : " < complex type >",

                            currentPropertyInfo.PropertyType.ToString ()

                            );

                    }

                }

            }

            FormControlPropertiesGrid.DataSource = propertiesTable;

            FormControlPropertiesGrid.AutoGenerateColumns = true;

            Telerik.Web.UI.GridSortExpression propertiesSort = new Telerik.Web.UI.GridSortExpression ();

            propertiesSort.FieldName = "Name";

            propertiesSort.SortOrder = Telerik.Web.UI.GridSortOrder.Ascending;

            FormControlPropertiesGrid.MasterTableView.SortExpressions.Clear ();

            FormControlPropertiesGrid.MasterTableView.SortExpressions.Add (propertiesSort);

            FormControlPropertiesGrid.Rebind ();

            return;

        }

        #endregion


        #region Control Events

        protected void DesignerToolbar_OnButtonClick (Object sender, Telerik.Web.UI.RadToolBarEventArgs eventArgs) {

            switch (eventArgs.Item.Text) {

                case "Refresh":

                    FormExplorerTree_Refresh ();

                    FormControlPropertiesGrid_Refresh ();

                    InitializeEventResultListener ();

                    break;

            }

            return;

        }

        protected void FormExplorerTree_OnNodeClick (Object sender, Telerik.Web.UI.RadTreeNodeEventArgs eventArgs) {

            FormControlPropertiesGrid_Refresh ();

            return;

        }

        #endregion 

    }

}
