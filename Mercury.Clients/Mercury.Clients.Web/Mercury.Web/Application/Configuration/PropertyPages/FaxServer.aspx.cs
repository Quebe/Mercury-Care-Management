using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Configuration.PropertyPages {

    public partial class FaxServer : System.Web.UI.Page {

        #region Private Properties

        private Client.Faxing.FaxServer faxServer;

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

            Int64 forFaxServerId = 0;


            if (MercuryApplication == null) { return; }

            if ((!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.FaxServerReview))

                && (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.FaxServerManage))) { Response.Redirect ("/PermissionDenied.aspx", true); return; }


            if (!Page.IsPostBack) {

                #region Initial Page Load

                if (Request.QueryString["FaxServerId"] != null) {

                    forFaxServerId = Int64.Parse (Request.QueryString["FaxServerId"]);

                }

                if (forFaxServerId != 0) {

                    faxServer = MercuryApplication.FaxServerGet (forFaxServerId, false);

                    if (faxServer == null) {

                        faxServer = new Mercury.Client.Faxing.FaxServer (MercuryApplication);

                    }

                }

                else {

                    faxServer = new Mercury.Client.Faxing.FaxServer (MercuryApplication);

                }

                InitializeAll ();

                Session[SessionCachePrefix + "FaxServer"] = faxServer;

                Session[SessionCachePrefix + "FaxServerUnmodified"] = faxServer.Copy ();

                #endregion

            } // Initial Page Load

            else { // Postback

                faxServer = (Mercury.Client.Faxing.FaxServer)Session[SessionCachePrefix + "FaxServer"];

            }

            ApplySecurity ();

            if (!String.IsNullOrEmpty (faxServer.Name)) { Page.Title = "Fax Server - " + faxServer.Name; } else { Page.Title = "Fax Server"; }

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

            InitializeFaxServerConfigurationPage ();

            InitializeConfigurationPage ();

            InitializeExtendedPropertiesGrid ();

            return;

        }


        protected void InitializeGeneralPage () {

            FaxServerName.Text = faxServer.Name;

            FaxServerDescription.Text = faxServer.Description;


            FaxServerEnabled.Checked = faxServer.Enabled;

            FaxServerVisible.Checked = faxServer.Visible;


            FaxServerCreateAuthorityName.Text = faxServer.CreateAccountInfo.SecurityAuthorityName;

            FaxServerCreateAccountId.Text = faxServer.CreateAccountInfo.UserAccountId;

            FaxServerCreateAccountName.Text = faxServer.CreateAccountInfo.UserAccountName;

            FaxServerCreateDate.MinDate = DateTime.MinValue;

            FaxServerCreateDate.SelectedDate = faxServer.CreateAccountInfo.ActionDate;


            FaxServerModifiedAuthorityName.Text = faxServer.ModifiedAccountInfo.SecurityAuthorityName;

            FaxServerModifiedAccountId.Text = faxServer.ModifiedAccountInfo.UserAccountId;

            FaxServerModifiedAccountName.Text = faxServer.ModifiedAccountInfo.UserAccountName;

            FaxServerModifiedDate.MinDate = DateTime.MinValue;

            FaxServerModifiedDate.SelectedDate = faxServer.ModifiedAccountInfo.ActionDate;

            return;

        }

        protected void InitializeAssemblyPage () {

            FaxServerAssemblyPath.Text = faxServer.AssemblyPath;

            FaxServerAssemblyName.Text = faxServer.AssemblyName;

            FaxServerAssemblyClassName.Text = faxServer.AssemblyClassName;

            return;

        }

        protected void InitializeFaxServerConfigurationPage () {

            FaxServerConfigurationFaxUrl.Text = faxServer.FaxServerConfiguration.FaxUrl;

            FaxServerConfigurationFaxQueueName.Text = faxServer.FaxServerConfiguration.FaxQueueName;

            FaxServerConfigurationSenderEmail.Text = faxServer.FaxServerConfiguration.SenderEmailAddress;

            FaxServerConfigurationMonitorInterval.Value = faxServer.FaxServerConfiguration.MonitorInterval;

            FaxServerConfigurationMonitorTimeout.Value = faxServer.FaxServerConfiguration.MonitorTimeout;

            return;

        }

        protected void InitializeConfigurationPage () {

            FaxServerConfigurationServerName.Text = faxServer.WebServiceHostConfiguration.Server;

            FaxServerConfigurationBindingProtocol.Text = faxServer.WebServiceHostConfiguration.BindingConfiguration.Protocol;

            FaxServerConfigurationPort.Value = faxServer.WebServiceHostConfiguration.Port;

            FaxServerConfigurationServicePath.Text = faxServer.WebServiceHostConfiguration.ServicePath;

            FaxServerConfigurationServiceName.Text = faxServer.WebServiceHostConfiguration.ServiceName;


            FaxServerConfigurationBindingTypeSelection.SelectedValue = ((Int32)faxServer.WebServiceHostConfiguration.BindingConfiguration.BindingType).ToString ();

            FaxServerConfigurationBindingTimeout.Value = faxServer.WebServiceHostConfiguration.BindingConfiguration.TimeoutReceive.TotalMinutes;

            FaxServerConfigurationBindingSecurityMode.SelectedValue = ((Int32)faxServer.WebServiceHostConfiguration.BindingConfiguration.SecurityMode).ToString ();

            FaxServerConfigurationBindingTransportCredential.SelectedValue = ((Int32)faxServer.WebServiceHostConfiguration.BindingConfiguration.TransportCredentialType).ToString ();

            FaxServerConfigurationBindingMessageCredential.SelectedValue = ((Int32)faxServer.WebServiceHostConfiguration.BindingConfiguration.MessageCredentialType).ToString ();


            FaxServerConfigurationClientCredentialsDomain.Text = faxServer.WebServiceHostConfiguration.ClientCredentials.Domain;

            FaxServerConfigurationClientCredentialsUserName.Text = faxServer.WebServiceHostConfiguration.ClientCredentials.UserName;

            FaxServerConfigurationClientCredentialsPassword.Text = faxServer.WebServiceHostConfiguration.ClientCredentials.Password;

            FaxServerConfigurationClientCredentialsImpersonation.SelectedValue = ((Int32)faxServer.WebServiceHostConfiguration.ClientCredentials.WindowsImpersonationLevel).ToString ();

            return;
        }

        protected void InitializeExtendedPropertiesGrid () {

            System.Data.DataTable propertiesTable = new System.Data.DataTable ();

            propertiesTable.Columns.Add ("ExtendedPropertyName");

            propertiesTable.Columns.Add ("ExtendedPropertyValue");

            foreach (String currentPropertyName in faxServer.ExtendedProperties.Keys) {

                propertiesTable.Rows.Add (

                    currentPropertyName,

                    faxServer.ExtendedProperties[currentPropertyName]

                );

            }

            ExtendedPropertiesGrid.DataSource = propertiesTable;

            ExtendedPropertiesGrid.DataBind ();

            return;

        }

        protected void ApplySecurity () {

            Boolean hasManagePermission = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.FaxServerManage);

            FaxServerName.ReadOnly = !hasManagePermission;

            FaxServerDescription.ReadOnly = !hasManagePermission;


            FaxServerEnabled.Enabled = hasManagePermission;

            FaxServerVisible.Enabled = hasManagePermission;


            ButtonCancel.Visible = hasManagePermission;

            ButtonApply.Visible = hasManagePermission;

            return;

        }

        #endregion



        #region Extended Properties Event Handlers

        protected void ButtonAddExtendedProperty_OnClick (Object Sender, EventArgs eventArgs) {

            if (MercuryApplication == null) { return; }


            if (!String.IsNullOrEmpty (CorrespondenceExtendedPropertyName.Text)) {

                if (!faxServer.ExtendedProperties.ContainsKey (CorrespondenceExtendedPropertyName.Text)) {

                    faxServer.ExtendedProperties.Add (CorrespondenceExtendedPropertyName.Text, CorrespondenceExtendedPropertyValue.Text);

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

            faxServer.ExtendedProperties.Remove (extendedPropertyName);


            InitializeExtendedPropertiesGrid ();

            return;

        }

        #endregion


        #region Dialog Button Event Handlers

        protected Boolean ApplyChanges () {

            Boolean isModified = false;

            Boolean success = false;


            Mercury.Client.Faxing.FaxServer faxServerUnmodified = (Mercury.Client.Faxing.FaxServer)Session[SessionCachePrefix + "FaxServerUnmodified"];

            if (faxServerUnmodified.Id == 0) { isModified = true; }


            faxServer.Name = FaxServerName.Text.Trim ();

            faxServer.Description = FaxServerDescription.Text.Trim ();

            faxServer.Enabled = FaxServerEnabled.Checked;

            faxServer.Visible = FaxServerVisible.Checked;


            faxServer.AssemblyPath = FaxServerAssemblyPath.Text;

            faxServer.AssemblyName = FaxServerAssemblyName.Text;

            faxServer.AssemblyClassName = FaxServerAssemblyClassName.Text;


            faxServer.FaxServerConfiguration.FaxUrl = FaxServerConfigurationFaxUrl.Text;

            faxServer.FaxServerConfiguration.FaxQueueName = FaxServerConfigurationFaxQueueName.Text;

            faxServer.FaxServerConfiguration.SenderEmailAddress = FaxServerConfigurationSenderEmail.Text;


            faxServer.FaxServerConfiguration.MonitorInterval = Convert.ToInt32 (FaxServerConfigurationMonitorInterval.Value);

            faxServer.FaxServerConfiguration.MonitorTimeout = Convert.ToInt32 (FaxServerConfigurationMonitorTimeout.Value);
                 


            faxServer.WebServiceHostConfiguration.Server = FaxServerConfigurationServerName.Text;

            faxServer.WebServiceHostConfiguration.BindingConfiguration.Protocol = FaxServerConfigurationBindingProtocol.Text;

            faxServer.WebServiceHostConfiguration.Port = Convert.ToInt32 (FaxServerConfigurationPort.Value);

            faxServer.WebServiceHostConfiguration.ServicePath = FaxServerConfigurationServicePath.Text;

            faxServer.WebServiceHostConfiguration.ServiceName = FaxServerConfigurationServiceName.Text;


            faxServer.WebServiceHostConfiguration.BindingConfiguration.BindingType = (Mercury.Server.Application.WebServiceBindingType)

                Convert.ToInt32 (FaxServerConfigurationBindingTypeSelection.SelectedValue);

            faxServer.WebServiceHostConfiguration.BindingConfiguration.TimeoutReceive = new TimeSpan (0, Convert.ToInt32 (FaxServerConfigurationBindingTimeout.Value), 0);

            faxServer.WebServiceHostConfiguration.BindingConfiguration.SecurityMode = (System.ServiceModel.BasicHttpSecurityMode)

                Convert.ToInt32 (FaxServerConfigurationBindingSecurityMode.SelectedValue);

            faxServer.WebServiceHostConfiguration.BindingConfiguration.TransportCredentialType = (System.ServiceModel.HttpClientCredentialType)

                Convert.ToInt32 (FaxServerConfigurationBindingTransportCredential.SelectedValue);

            faxServer.WebServiceHostConfiguration.BindingConfiguration.MessageCredentialType = (System.ServiceModel.MessageCredentialType)

                Convert.ToInt32 (FaxServerConfigurationBindingMessageCredential.SelectedValue);


            faxServer.WebServiceHostConfiguration.ClientCredentials.Domain = FaxServerConfigurationClientCredentialsDomain.Text;

            faxServer.WebServiceHostConfiguration.ClientCredentials.UserName = FaxServerConfigurationClientCredentialsUserName.Text;

            faxServer.WebServiceHostConfiguration.ClientCredentials.Password = FaxServerConfigurationClientCredentialsPassword.Text;

            faxServer.WebServiceHostConfiguration.ClientCredentials.WindowsImpersonationLevel = (System.Security.Principal.TokenImpersonationLevel)

                Convert.ToInt32 (FaxServerConfigurationClientCredentialsImpersonation.SelectedValue);


            if (!isModified) { isModified = !faxServer.IsEqual (faxServerUnmodified); }

            if (isModified) {

                success = MercuryApplication.FaxServerSave (faxServer);

                if (success) {

                    faxServer = MercuryApplication.FaxServerGet (faxServer.Id, false);

                    Session[SessionCachePrefix + "FaxServer"] = faxServer;

                    Session[SessionCachePrefix + "FaxServerUnmodified"] = faxServer.Copy ();

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

            if (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.FaxServerManage)) {

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
