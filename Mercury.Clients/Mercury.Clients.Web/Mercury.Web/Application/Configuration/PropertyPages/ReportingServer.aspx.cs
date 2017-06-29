using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Configuration.PropertyPages {

    public partial class ReportingServer : System.Web.UI.Page {

        #region Private Properties

        private Client.Reporting.ReportingServer reportingServer;

        #endregion


        #region Public Properties

        public String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (PageInstanceId.Text)) { PageInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return Form.Name + PageInstanceId.Text + ".";

            }

        }

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application)Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            Int64 forReportingServerId = 0;


            if (MercuryApplication == null) { return; }

            if ((!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ReportingServerReview))

                && (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ReportingServerManage))) { Response.Redirect ("/PermissionDenied.aspx", true); return; }


            if (!Page.IsPostBack) {

                #region Initial Page Load

                if (Request.QueryString["ReportingServerId"] != null) {

                    forReportingServerId = Int64.Parse (Request.QueryString["ReportingServerId"]);

                }

                if (forReportingServerId != 0) {

                    reportingServer = MercuryApplication.ReportingServerGet (forReportingServerId, false);

                    if (reportingServer == null) {

                        reportingServer = new Mercury.Client.Reporting.ReportingServer (MercuryApplication);

                    }

                }

                else {

                    reportingServer = new Mercury.Client.Reporting.ReportingServer (MercuryApplication);

                }

                InitializeAll ();

                Session[SessionCachePrefix + "ReportingServer"] = reportingServer;

                Session[SessionCachePrefix + "ReportingServerUnmodified"] = reportingServer.Copy ();

                #endregion

            } // Initial Page Load

            else { // Postback

                reportingServer = (Mercury.Client.Reporting.ReportingServer)Session[SessionCachePrefix + "ReportingServer"];

            }

            ApplySecurity ();

            if (!String.IsNullOrEmpty (reportingServer.Name)) { Page.Title = "Reporting Server - " + reportingServer.Name; } else { Page.Title = "Reporting Server"; }

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

            InitializeConfigurationPage ();

            InitializeExtendedPropertiesGrid ();

            return;

        }


        protected void InitializeGeneralPage () {

            ReportingServerName.Text = reportingServer.Name;

            ReportingServerDescription.Text = reportingServer.Description;


            ReportingServerEnabled.Checked = reportingServer.Enabled;

            ReportingServerVisible.Checked = reportingServer.Visible;


            ReportingServerCreateAuthorityName.Text = reportingServer.CreateAccountInfo.SecurityAuthorityName;

            ReportingServerCreateAccountId.Text = reportingServer.CreateAccountInfo.UserAccountId;

            ReportingServerCreateAccountName.Text = reportingServer.CreateAccountInfo.UserAccountName;

            ReportingServerCreateDate.MinDate = DateTime.MinValue;

            ReportingServerCreateDate.SelectedDate = reportingServer.CreateAccountInfo.ActionDate;


            ReportingServerModifiedAuthorityName.Text = reportingServer.ModifiedAccountInfo.SecurityAuthorityName;

            ReportingServerModifiedAccountId.Text = reportingServer.ModifiedAccountInfo.UserAccountId;

            ReportingServerModifiedAccountName.Text = reportingServer.ModifiedAccountInfo.UserAccountName;

            ReportingServerModifiedDate.MinDate = DateTime.MinValue;

            ReportingServerModifiedDate.SelectedDate = reportingServer.ModifiedAccountInfo.ActionDate;

            return;

        }

        protected void InitializeAssemblyPage () {

            ReportingServerAssemblyPath.Text = reportingServer.AssemblyPath;

            ReportingServerAssemblyName.Text = reportingServer.AssemblyName;

            ReportingServerAssemblyClassName.Text = reportingServer.AssemblyClassName;

            return;

        }

        protected void InitializeConfigurationPage () {

            ReportingServerConfigurationServerName.Text = reportingServer.WebServiceHostConfiguration.Server;

            ReportingServerConfigurationBindingProtocol.Text = reportingServer.WebServiceHostConfiguration.BindingConfiguration.Protocol;

            ReportingServerConfigurationPort.Value = reportingServer.WebServiceHostConfiguration.Port;

            ReportingServerConfigurationServicePath.Text = reportingServer.WebServiceHostConfiguration.ServicePath;

            ReportingServerConfigurationServiceName.Text = reportingServer.WebServiceHostConfiguration.ServiceName;


            ReportingServerConfigurationBindingTypeSelection.SelectedValue = ((Int32)reportingServer.WebServiceHostConfiguration.BindingConfiguration.BindingType).ToString ();

            ReportingServerConfigurationBindingTimeout.Value = reportingServer.WebServiceHostConfiguration.BindingConfiguration.TimeoutReceive.TotalMinutes;

            ReportingServerConfigurationBindingSecurityMode.SelectedValue = ((Int32)reportingServer.WebServiceHostConfiguration.BindingConfiguration.SecurityMode).ToString ();

            ReportingServerConfigurationBindingTransportCredential.SelectedValue = ((Int32)reportingServer.WebServiceHostConfiguration.BindingConfiguration.TransportCredentialType).ToString ();

            ReportingServerConfigurationBindingMessageCredential.SelectedValue = ((Int32)reportingServer.WebServiceHostConfiguration.BindingConfiguration.MessageCredentialType).ToString ();


            ReportingServerConfigurationClientCredentialsDomain.Text = reportingServer.WebServiceHostConfiguration.ClientCredentials.Domain;

            ReportingServerConfigurationClientCredentialsUserName.Text = reportingServer.WebServiceHostConfiguration.ClientCredentials.UserName;

            ReportingServerConfigurationClientCredentialsPassword.Text = reportingServer.WebServiceHostConfiguration.ClientCredentials.Password;

            ReportingServerConfigurationClientCredentialsImpersonation.SelectedValue = ((Int32)reportingServer.WebServiceHostConfiguration.ClientCredentials.WindowsImpersonationLevel).ToString ();

            return;
        }

        protected void InitializeExtendedPropertiesGrid () {

            System.Data.DataTable propertiesTable = new System.Data.DataTable ();

            propertiesTable.Columns.Add ("ExtendedPropertyName");

            propertiesTable.Columns.Add ("ExtendedPropertyValue");

            foreach (String currentPropertyName in reportingServer.ExtendedProperties.Keys) {

                propertiesTable.Rows.Add (

                    currentPropertyName,

                    reportingServer.ExtendedProperties[currentPropertyName]

                );

            }

            ExtendedPropertiesGrid.DataSource = propertiesTable;

            ExtendedPropertiesGrid.DataBind ();

            return;

        }

        protected void ApplySecurity () {

            Boolean hasManagePermission = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ReportingServerManage);

            ReportingServerName.ReadOnly = !hasManagePermission;

            ReportingServerDescription.ReadOnly = !hasManagePermission;


            ReportingServerEnabled.Enabled = hasManagePermission;

            ReportingServerVisible.Enabled = hasManagePermission;


            ButtonCancel.Visible = hasManagePermission;

            ButtonApply.Visible = hasManagePermission;

            return;

        }

        #endregion



        #region Extended Properties Event Handlers

        protected void ButtonAddExtendedProperty_OnClick (Object Sender, EventArgs eventArgs) {

            if (MercuryApplication == null) { return; }


            if (!String.IsNullOrEmpty (CorrespondenceExtendedPropertyName.Text)) {

                if (!reportingServer.ExtendedProperties.ContainsKey (CorrespondenceExtendedPropertyName.Text)) {

                    reportingServer.ExtendedProperties.Add (CorrespondenceExtendedPropertyName.Text, CorrespondenceExtendedPropertyValue.Text);

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

            reportingServer.ExtendedProperties.Remove (extendedPropertyName);


            InitializeExtendedPropertiesGrid ();

            return;

        }

        #endregion


        #region Dialog Button Event Handlers

        protected Boolean ApplyChanges () {

            Boolean isModified = false;

            Boolean success = false;


            Mercury.Client.Reporting.ReportingServer reportingServerUnmodified = (Mercury.Client.Reporting.ReportingServer)Session[SessionCachePrefix + "ReportingServerUnmodified"];

            if (reportingServerUnmodified.Id == 0) { isModified = true; }


            reportingServer.Name = ReportingServerName.Text.Trim ();

            reportingServer.Description = ReportingServerDescription.Text.Trim ();

            reportingServer.Enabled = ReportingServerEnabled.Checked;

            reportingServer.Visible = ReportingServerVisible.Checked;


            reportingServer.AssemblyPath = ReportingServerAssemblyPath.Text;

            reportingServer.AssemblyName = ReportingServerAssemblyName.Text;

            reportingServer.AssemblyClassName = ReportingServerAssemblyClassName.Text;


            reportingServer.WebServiceHostConfiguration.Server = ReportingServerConfigurationServerName.Text;

            reportingServer.WebServiceHostConfiguration.BindingConfiguration.Protocol = ReportingServerConfigurationBindingProtocol.Text;

            reportingServer.WebServiceHostConfiguration.Port = Convert.ToInt32 (ReportingServerConfigurationPort.Value);

            reportingServer.WebServiceHostConfiguration.ServicePath = ReportingServerConfigurationServicePath.Text;

            reportingServer.WebServiceHostConfiguration.ServiceName = ReportingServerConfigurationServiceName.Text;


            reportingServer.WebServiceHostConfiguration.BindingConfiguration.BindingType = (Mercury.Server.Application.WebServiceBindingType)

                Convert.ToInt32 (ReportingServerConfigurationBindingTypeSelection.SelectedValue);

            reportingServer.WebServiceHostConfiguration.BindingConfiguration.TimeoutOpen = new TimeSpan (0, Convert.ToInt32 (ReportingServerConfigurationBindingTimeout.Value), 0);

            reportingServer.WebServiceHostConfiguration.BindingConfiguration.TimeoutClose = new TimeSpan (0, Convert.ToInt32 (ReportingServerConfigurationBindingTimeout.Value), 0);

            reportingServer.WebServiceHostConfiguration.BindingConfiguration.TimeoutSend = new TimeSpan (0, Convert.ToInt32 (ReportingServerConfigurationBindingTimeout.Value), 0);
            
            reportingServer.WebServiceHostConfiguration.BindingConfiguration.TimeoutReceive = new TimeSpan (0, Convert.ToInt32 (ReportingServerConfigurationBindingTimeout.Value), 0);

            reportingServer.WebServiceHostConfiguration.BindingConfiguration.SecurityMode = (System.ServiceModel.BasicHttpSecurityMode)

                Convert.ToInt32 (ReportingServerConfigurationBindingSecurityMode.SelectedValue);

            reportingServer.WebServiceHostConfiguration.BindingConfiguration.TransportCredentialType = (System.ServiceModel.HttpClientCredentialType)

                Convert.ToInt32 (ReportingServerConfigurationBindingTransportCredential.SelectedValue);

            reportingServer.WebServiceHostConfiguration.BindingConfiguration.MessageCredentialType = (System.ServiceModel.MessageCredentialType)

                Convert.ToInt32 (ReportingServerConfigurationBindingMessageCredential.SelectedValue);


            reportingServer.WebServiceHostConfiguration.ClientCredentials.Domain = ReportingServerConfigurationClientCredentialsDomain.Text;

            reportingServer.WebServiceHostConfiguration.ClientCredentials.UserName = ReportingServerConfigurationClientCredentialsUserName.Text;

            reportingServer.WebServiceHostConfiguration.ClientCredentials.Password = ReportingServerConfigurationClientCredentialsPassword.Text;

            reportingServer.WebServiceHostConfiguration.ClientCredentials.WindowsImpersonationLevel = (System.Security.Principal.TokenImpersonationLevel)

                Convert.ToInt32 (ReportingServerConfigurationClientCredentialsImpersonation.SelectedValue);


            if (!isModified) { isModified = !reportingServer.IsEqual (reportingServerUnmodified); }

            if (isModified) {

                success = MercuryApplication.ReportingServerSave (reportingServer);

                if (success) {

                    reportingServer = MercuryApplication.ReportingServerGet (reportingServer.Id, false);

                    Session[SessionCachePrefix + "ReportingServer"] = reportingServer;

                    Session[SessionCachePrefix + "ReportingServerUnmodified"] = reportingServer.Copy ();

                    SaveResponseLabel.Text = "Save Successful";

                    InitializeAll ();

                }

                else {

                    SaveResponseLabel.Text = "Unable to Save.";

                    if (MercuryApplication.LastException != null) { SaveResponseLabel.Text = SaveResponseLabel.Text + " [" + MercuryApplication.LastException.Message + "]"; }

                    success = false;

                }

            }

            else { SaveResponseLabel.Text = "No Changes Detected."; success = true; }

            return success;

        }

        protected void ButtonOk_OnClick (Object sender, EventArgs eventArgs) {

            Boolean success = false;

            if (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ReportingServerManage)) {

                success = ApplyChanges ();

            }

            else {

                success = true;

            }


            if (success) {

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
