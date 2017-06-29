using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Mercury.Web.Application.Configuration.PropertyPages {

    public partial class Workflow : System.Web.UI.Page {

        #region Private Propreties

        Mercury.Client.Core.Work.Workflow workflow;

        #endregion


        #region Private Session Properties

        public String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (PageInstanceId.Text)) { PageInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return Form.Name + PageInstanceId.Text + ".";

            }

        }

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application) Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            Int64 forWorkflowId = 0;


            if (MercuryApplication == null) { return; }

            if ((!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkflowReview))

                && (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkflowManage))) 
            
                { Response.Redirect ("/PermissionDenied.aspx", true); return; }

            if (!Page.IsPostBack) {

                #region Initial Page Load

                if (Request.QueryString["WorkflowId"] != null) {

                    forWorkflowId = Int64.Parse (Request.QueryString["WorkflowId"]);

                }

                if (forWorkflowId != 0) {

                    workflow = MercuryApplication.WorkflowGet (forWorkflowId, false);

                    if (workflow == null) {

                        workflow = new Mercury.Client.Core.Work.Workflow (MercuryApplication);

                    }

                    Page.Title = "Workflow - " + workflow.Name;

                }

                else {

                    workflow = new Mercury.Client.Core.Work.Workflow (MercuryApplication);

                }

                InitializeAll ();

                Session[SessionCachePrefix + "Workflow"] = workflow;

                Session[SessionCachePrefix + "WorkflowUnmodified"] = workflow.Copy ();

                #endregion

            } // Initial Page Load

            else { // Postback

                workflow = (Mercury.Client.Core.Work.Workflow) Session[SessionCachePrefix + "Workflow"];

            }

            ApplySecurity ();

            if (!String.IsNullOrEmpty (workflow.Name)) { Page.Title = "Workflow - " + workflow.Name; } else { Page.Title = "New Workflow"; }

            return;

        }

        protected void Page_Unload (object sender, EventArgs e) {

            MercuryApplication.ApplicationClientClose ();

            return;

        }

        #endregion 


        #region Initialization

        protected void InitializeAll () {

            InitializeGeneralPage ();

            InitializeAssemblyPage ();

            InitializeParametersPage ();

            InitializePermissionsPage ();

            InitializeExtendedPropertiesGrid ();

            return;

        }

        protected void InitializeGeneralPage () {

            WorkflowName.Text = workflow.Name;

            WorkflowDescription.Text = workflow.Description;


            WorkflowFramework.SelectedValue = ((Int32)workflow.Framework).ToString ();


            WorkflowEntityType.Items.Clear ();

            WorkflowEntityType.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("Not Specified", "0"));

            WorkflowEntityType.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("Insurer", "4"));

            WorkflowEntityType.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("Member", "1"));

            WorkflowEntityType.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("Provider", "2"));

            WorkflowEntityType.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("Sponsor", "3"));

            WorkflowEntityType.SelectedValue = ((Int32) workflow.EntityType).ToString ();

            WorkflowActionVerb.Text = workflow.ActionVerb;


            WorkflowEnabled.Checked = workflow.Enabled;

            WorkflowVisible.Checked = workflow.Visible;


            
            WorkflowCreateAuthorityName.Text = workflow.CreateAccountInfo.SecurityAuthorityName;

            WorkflowCreateAccountId.Text = workflow.CreateAccountInfo.UserAccountId;

            WorkflowCreateAccountName.Text = workflow.CreateAccountInfo.UserAccountName;

            WorkflowCreateDate.MinDate = DateTime.MinValue;

            WorkflowCreateDate.SelectedDate = workflow.CreateAccountInfo.ActionDate;


            WorkflowModifiedAuthorityName.Text = workflow.ModifiedAccountInfo.SecurityAuthorityName;

            WorkflowModifiedAccountId.Text = workflow.ModifiedAccountInfo.UserAccountId;

            WorkflowModifiedAccountName.Text = workflow.ModifiedAccountInfo.UserAccountName;

            WorkflowModifiedDate.MinDate = DateTime.MinValue;

            WorkflowModifiedDate.SelectedDate = workflow.ModifiedAccountInfo.ActionDate;

            return;

        }

        protected void InitializeAssemblyPage () {

            WorkflowAssemblyPath.Text = workflow.AssemblyPath;

            WorkflowAssemblyName.Text = workflow.AssemblyName;

            WorkflowAssemblyClassName.Text = workflow.AssemblyClassName;

            return;

        }

        protected void InitializeParametersPage () {

            System.Data.DataTable parametersTable = new DataTable ();

            parametersTable.Columns.Add ("ParameterName");

            parametersTable.Columns.Add ("ParameterDataTypeValue");

            parametersTable.Columns.Add ("ParameterDataType");

            parametersTable.Columns.Add ("AllowFixedValue");

            parametersTable.Columns.Add ("Required");



            foreach (String currentParameterName in workflow.WorkflowParameters.Keys) {

                parametersTable.Rows.Add (

                    currentParameterName,

                    ((Int32) workflow.WorkflowParameters [currentParameterName].DataType).ToString (),

                    workflow.WorkflowParameters [currentParameterName].DataType.ToString (),

                    workflow.WorkflowParameters[currentParameterName].AllowFixedValue.ToString (),

                    workflow.WorkflowParameters[currentParameterName].Required.ToString ()

                    );

            }

            WorkflowParametersGrid.DataSource = parametersTable;

            WorkflowParametersGrid.DataBind ();

            return;

        }

        protected void InitializePermissionsPage () {

            InitializePermissionsGrid ();


            List<Client.Core.Work.WorkTeam> workTeamsAvailable = MercuryApplication.WorkTeamsAvailable (false);

            WorkflowPermissionTeamSelection.Items.Clear ();

            foreach (Client.Core.Work.WorkTeam currentWorkTeam in workTeamsAvailable) {

                if (!workflow.ContainsPermissionWorkTeam (currentWorkTeam.Id)) {

                    WorkflowPermissionTeamSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentWorkTeam.Name, currentWorkTeam.Id.ToString ()));

                }

            }

            if (WorkflowPermissionTeamSelection.Items.Count == 0) {

                WorkflowPermissionTeamSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** No Work Teams Available", String.Empty));

            }

            return;

        }


        protected void InitializePermissionsGrid () {

            System.Data.DataTable permissionTable = new DataTable ();

            permissionTable.Columns.Add ("WorkTeamId");

            permissionTable.Columns.Add ("WorkTeamName");

            permissionTable.Columns.Add ("IsGranted");

            permissionTable.Columns.Add ("IsDenied");

            foreach (Mercury.Server.Application.WorkflowPermission currentPermission in workflow.Permissions) {

                permissionTable.Rows.Add (

                    currentPermission.WorkTeamId,

                    MercuryApplication.WorkTeamGet (currentPermission.WorkTeamId, true).Name,

                    currentPermission.IsGranted.ToString (),

                    currentPermission.IsDenied.ToString ()

                );

            }

            WorkflowPermissionsGrid.DataSource = permissionTable;

            WorkflowPermissionsGrid.DataBind ();

            return;

        }

        protected void InitializeExtendedPropertiesGrid () {

            System.Data.DataTable propertiesTable = new DataTable ();

            propertiesTable.Columns.Add ("ExtendedPropertyName");

            propertiesTable.Columns.Add ("ExtendedPropertyValue");

            foreach (String currentPropertyName in workflow.ExtendedProperties.Keys) {

                propertiesTable.Rows.Add (

                    currentPropertyName,

                    workflow.ExtendedProperties[currentPropertyName]

                );

            }

            ExtendedPropertiesGrid.DataSource = propertiesTable;

            ExtendedPropertiesGrid.DataBind ();

            return;

        }


        protected void ApplySecurity () {

            Boolean hasManagePermission = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkflowManage);

            WorkflowName.ReadOnly = !hasManagePermission;

            WorkflowDescription.ReadOnly = !hasManagePermission;

            WorkflowEnabled.Enabled = hasManagePermission;

            WorkflowVisible.Enabled = hasManagePermission;

            WorkflowEntityType.Enabled = hasManagePermission;

            WorkflowActionVerb.Enabled = hasManagePermission;

            WorkflowAssemblyPath.Enabled = hasManagePermission;

            WorkflowAssemblyName.Enabled = hasManagePermission;

            WorkflowAssemblyClassName.Enabled = hasManagePermission;

            ButtonCancel.Visible = hasManagePermission;

            ButtonApply.Visible = hasManagePermission;
           
            return;

        }

        #endregion


        #region Workflow Parameters Grid 

        protected void ButtonAddUpdateWorkflowParameter_OnClick (Object sender, EventArgs eventArgs) {

            Boolean existingParameterFound = false;

            Server.Application.ActionParameter newParameter = null;

            SaveResponseLabel.Text = String.Empty;


            if (MercuryApplication == null) { return; }


            newParameter = new Mercury.Server.Application.ActionParameter ();

            newParameter.ParameterName = WorkflowParameterName.Text;

            newParameter.DataType = (Mercury.Server.Application.ActionParameterDataType) Convert.ToInt32 (WorkflowParameterDataType.SelectedValue);

            newParameter.AllowFixedValue = WorkflowParameterAllowFixedValue.Checked;

            newParameter.Required = WorkflowParameterRequired.Checked;


            if ((!String.IsNullOrEmpty (newParameter.ParameterName)) && (newParameter.ParameterName.Trim ().ToLower () != "workflowid")) {

                existingParameterFound = false;

                foreach (String currentParameterName in workflow.WorkflowParameters.Keys) {

                    if (currentParameterName.Trim ().ToLower () == newParameter.ParameterName.Trim ().ToLower ()) {

                        existingParameterFound = true; break;

                    }

                }


                switch (((System.Web.UI.WebControls.Button) sender).ID) {

                    case "ButtonAddWorkflowParameter":

                        if (!existingParameterFound) {

                            workflow.WorkflowParameters.Add (newParameter.ParameterName, newParameter);

                            SaveResponseLabel.Text = String.Empty;

                        }

                        else { SaveResponseLabel.Text = "Duplicate Parameter."; }

                        break;


                    case "ButtonUpdateWorkflowParameter":

                        if (WorkflowParametersGrid.SelectedItems[0] != null) {

                            workflow.WorkflowParameters[newParameter.ParameterName] = MercuryApplication.CopyActionParameter (newParameter);

                        }

                        else { SaveResponseLabel.Text = "No Parameter Selected."; }

                        break;

                }

            }

            else { SaveResponseLabel.Text = "Invalid Name Specified."; }

            InitializeParametersPage ();

            return;

        }

        protected void WorkflowParametersGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            String parameterName = (String) ((Telerik.Web.UI.GridEditableItem) eventArgs.Item).OwnerTableView.DataKeyValues[eventArgs.Item.ItemIndex]["ParameterName"];

            workflow.WorkflowParameters.Remove (parameterName);

            InitializeParametersPage ();

            return;

        }

        protected void WorkflowParametersGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            switch (eventArgs.CommandName) {

                case Telerik.Web.UI.RadGrid.EditCommandName:

                    String parameterName = (String) ((Telerik.Web.UI.GridEditableItem) eventArgs.Item).OwnerTableView.DataKeyValues[eventArgs.Item.ItemIndex]["ParameterName"];

                    Mercury.Server.Application.ActionParameter workflowParameter = MercuryApplication.CopyActionParameter (workflow.WorkflowParameters[parameterName]);


                    WorkflowParameterName.Text = parameterName;

                    WorkflowParameterDataType.SelectedValue = ((Int32) workflow.WorkflowParameters[parameterName].DataType).ToString ();


                    eventArgs.Canceled = true;

                    break;

            }

            InitializeParametersPage ();

            WorkflowParametersGrid.SelectedIndexes.Add (eventArgs.Item.ItemIndex);

            return;

        }

        #endregion


        #region Workflow Permissions Grid

        protected void ButtonAddWorkflowPermission_OnClick (Object sender, EventArgs eventArgs) {

            Server.Application.WorkflowPermission newPermission = null;

            SaveResponseLabel.Text = String.Empty;


            if (MercuryApplication == null) { return; }

            if (WorkflowPermissionTeamSelection.SelectedItem == null) { return; }

            if (WorkflowPermissionTeamSelection.SelectedValue == String.Empty) { return; }

            if (workflow.ContainsPermissionWorkTeam (Convert.ToInt64 (WorkflowPermissionTeamSelection.SelectedValue))) { return; }


            newPermission = new Server.Application.WorkflowPermission ();

            newPermission.WorkflowId = workflow.Id;

            newPermission.WorkTeamId = Convert.ToInt64 (WorkflowPermissionTeamSelection.SelectedValue);

            newPermission.IsGranted = (WorkflowPermissionTeamPermissionSelection.SelectedValue == "1");

            newPermission.IsDenied = (WorkflowPermissionTeamPermissionSelection.SelectedValue == "0");

            newPermission.CreateAccountInfo = workflow.CreateAccountInfo;

            newPermission.ModifiedAccountInfo = workflow.ModifiedAccountInfo;


            workflow.Permissions.Add (newPermission);

            InitializePermissionsPage ();

            return;

        }

        protected void WorkflowPermissionsGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs e) {

            workflow.Permissions.RemoveAt (e.Item.ItemIndex);

            InitializePermissionsPage ();

            return;

        }

        #endregion


        #region Extended Properties

        protected void ButtonAddExtendedProperty_OnClick (Object Sender, EventArgs eventArgs) {

            if (MercuryApplication == null) { return; }


            if (!String.IsNullOrEmpty (WorkQueueExtendedPropertyName.Text)) {

                if (!workflow.ExtendedProperties.ContainsKey (WorkQueueExtendedPropertyName.Text)) {

                    workflow.ExtendedProperties.Add (WorkQueueExtendedPropertyName.Text, WorkQueueExtendedPropertyValue.Text);

                }

                else { SaveResponseLabel.Text = "Cannot add duplicate Extended Property to Work Queue."; }

            }

            InitializeExtendedPropertiesGrid ();

            return;

        }

        protected void ExtendedPropertiesGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }


            Int32 deleteIndex = eventArgs.Item.DataSetIndex;

            String extendedPropertyName = (String)eventArgs.Item.OwnerTableView.DataKeyValues[deleteIndex]["ExtendedPropertyName"];

            workflow.ExtendedProperties.Remove (extendedPropertyName);


            InitializeExtendedPropertiesGrid ();

            return;

        }

        #endregion


        #region Dialog Button Event Handlers

        protected Boolean ApplyChanges () {

            Boolean success = false;

            Boolean isModified = false;

            Boolean isValid = false;

            Dictionary<String, String> validationResponse;


            if (MercuryApplication == null) { return false; }


            Mercury.Client.Core.Work.Workflow workflowUnmodified = (Mercury.Client.Core.Work.Workflow) Session[SessionCachePrefix + "WorkflowUnmodified"];


            workflow.Name = WorkflowName.Text;

            workflow.Description = WorkflowDescription.Text;

            workflow.Enabled = WorkflowEnabled.Checked;

            workflow.Visible = WorkflowVisible.Checked;



            workflow.EntityType = (Mercury.Server.Application.EntityType) Convert.ToInt32 (WorkflowEntityType.SelectedValue);

            workflow.ActionVerb = WorkflowActionVerb.Text;


            workflow.Framework = (Mercury.Server.Application.WorkflowFramework)Convert.ToInt32 (WorkflowFramework.SelectedValue);
            
            workflow.AssemblyPath = WorkflowAssemblyPath.Text;

            workflow.AssemblyName = WorkflowAssemblyName.Text;

            workflow.AssemblyClassName = WorkflowAssemblyClassName.Text;


            if (workflowUnmodified.Id == 0) { isModified = true; }

            if (!isModified) { isModified = !workflow.IsEqual (workflowUnmodified); }


            validationResponse = workflow.Validate ();

            isValid = (validationResponse.Count == 0);


            if ((isModified) && (isValid)) {

                if (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkflowManage)) {

                    SaveResponseLabel.Text = "Permission Denied.";

                    return false;

                }

                success = MercuryApplication.WorkflowSave (workflow);

                if (success) {

                    workflow = MercuryApplication.WorkflowGet (workflow.Id, false);

                    Session[SessionCachePrefix + "Workflow"] = workflow;

                    Session[SessionCachePrefix + "WorkflowUnmodified"] = workflow.Copy ();

                    SaveResponseLabel.Text = "Save Successful.";

                    InitializeAll ();

                }

                else {

                    SaveResponseLabel.Text = "Unable to Save.";

                    if (MercuryApplication.LastException != null) { SaveResponseLabel.Text = SaveResponseLabel.Text + " [" + MercuryApplication.LastException.Message + "]"; }

                    success = false;

                }

            }

            else if (!isModified) { SaveResponseLabel.Text = "No Changes Detected."; success = true; }

            else if (!isValid) {

                foreach (String validationKey in validationResponse.Keys) {

                    SaveResponseLabel.Text = "Invalid [" + validationKey + "]: " + validationResponse[validationKey];

                    break;

                }

                success = false;

            }

            return success;

        }

        protected void ButtonOk_OnClick (Object sender, EventArgs eventArgs) {

            if (ApplyChanges ()) {

                Server.Transfer ("/WindowClose.aspx");

            }

            return;

        }

        protected void ButtonApply_OnClick (Object sender, EventArgs eventArgs) {

            Boolean success = ApplyChanges ();

            return;

        }

        protected void ButtonCancel_OnClick (Object sender, EventArgs eventArgs) {

            Server.Transfer ("/WindowClose.aspx");

            return;

        }

        #endregion

    }

}
