using System;
using System.Collections;
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

namespace Mercury.Web.Application.Enterprise.Windows {

    public partial class EnvironmentProperties : System.Web.UI.Page {

        String SessionCachePrefix = String.Empty;

        String SessionCacheSuffix = String.Empty;

        protected Mercury.Client.Application application;

        protected Mercury.Server.Application.Environment environment;


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            Int64 environmentId;

            String environmentName;


            if (Session["Mercury.Application"] == null) { Response.RedirectLocation = "/SessionExpired.aspx"; return; }

            application = (Mercury.Client.Application) Session["Mercury.Application"];

            if (!application.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.EnvironmentReview)) {

                if (!IsPostBack) { Server.Transfer ("/PermissionDenied.aspx"); }

                else { Response.RedirectLocation = "/PermissionDenied.aspx"; }

                return;

            }

           
            if ((application != null) && (!Page.IsPostBack)) {

                #region Initial Page Load

                SessionCachePrefix = Form.Name;

                PageInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", "");

                SessionCacheSuffix = PageInstanceId.Text;


                environmentName = Request.QueryString["EnvironmentName"];

                environmentId = 0;

                Session.Add (SessionCachePrefix + "EnvironmentName", environmentName);


                System.Collections.Generic.Dictionary<Int64, String> environmentDictionary = application.EnvironmentDictionary (false);

                foreach (Int64 currentKey in environmentDictionary.Keys) {

                    if (environmentDictionary[currentKey] == environmentName) {

                        environmentId = currentKey;

                        Session.Add (SessionCachePrefix + "EnvironmentId" + SessionCacheSuffix, currentKey);

                        break;

                    }

                }

                if (environmentId != 0) {

                    environment = application.EnvironmentGet (environmentName);

                    Page.Title = "Environment Properties";

                    Page.Title = Page.Title + " (" + environmentName + ")";

                }

                else {

                    Session.Add (SessionCachePrefix + "EnvironmentId" + SessionCacheSuffix, 0);

                    environment = new Mercury.Server.Application.Environment ();

                    environment.SqlConfiguration = new Mercury.Server.Application.SqlConfiguration();

                    environment.CreateAccountInfo = new Mercury.Server.Application.AuthorityAccountStamp();

                    environment.ModifiedAccountInfo = new Mercury.Server.Application.AuthorityAccountStamp();
                    

                    Page.Title = "New Environment";

                    if (!application.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.EnvironmentManage)) {

                        Server.Transfer ("/PermissionDenied.aspx");

                    }

                }

                Session[SessionCachePrefix + "Environment" + SessionCacheSuffix] = environment;

                if (environmentId != 0) {

                    Initialize_GeneralPage ();

                }

                ApplySecurity ();

                #endregion

            } // Initial Page Load

            else { // Postback

                SessionCachePrefix = Form.Name;

                SessionCacheSuffix = PageInstanceId.Text;


                environmentId = Int64.Parse (Session[SessionCachePrefix + "EnvironmentId" + SessionCacheSuffix].ToString ());

                environmentName = (String) Session[SessionCachePrefix + "EnvironmentName" + SessionCacheSuffix];

                environment = (Mercury.Server.Application.Environment) Session[SessionCachePrefix + "Environment" + SessionCacheSuffix];

            }

            return;

        }

        #endregion 


        #region Initialization

        protected void Initialize_GeneralPage () {

            if (environment.Id != 0) {

                Page.Title = "Environment Properties";

                Page.Title = Page.Title + " (" + environment.Name + ")";

            }

            else {

                Page.Title = "New Environment";

            }


            // EnvironmentId.Text = environment.EnvironmentId.ToString ();

            EnvironmentName.Text = environment.Name;

            EnvironmentConfidentialityStatement.Text = environment.ConfidentialityStatement;


            EnvironmentSqlServerName.Text = environment.SqlConfiguration.ServerName;

            EnvironmentSqlDatabaseName.Text = environment.SqlConfiguration.DatabaseName;


            EnvironmentSqlUseTrustedConnection.Checked = environment.SqlConfiguration.UseTrustedConnection;

            EnvironmentSqlUserName.Text = environment.SqlConfiguration.SqlUserName;

            EnvironmentSqlUserPassword.Text = String.Empty;

            EnvironmentSqlUserConfirmPassword.Text = String.Empty;


            EnvironmentSqlUseConnectionPooling.Checked = environment.SqlConfiguration.PoolingEnabled;

            EnvironmentSqlPoolSizeMinimum.Text = environment.SqlConfiguration.PoolSizeMinimum.ToString ();

            EnvironmentSqlPoolSizeMaximum.Text = environment.SqlConfiguration.PoolSizeMaximum.ToString ();

            EnvironmentSqlCustomAttributes.Text = environment.SqlConfiguration.CustomAttributes;


            EnvironmentCreateAuthorityName.Text = environment.CreateAccountInfo.SecurityAuthorityName;

            EnvironmentCreateAccountId.Text = environment.CreateAccountInfo.UserAccountId;

            EnvironmentCreateAccountName.Text = environment.CreateAccountInfo.UserAccountName;

            EnvironmentCreateDate.SelectedDate = environment.CreateAccountInfo.ActionDate;


            EnvironmentModifiedAuthorityName.Text = environment.ModifiedAccountInfo.SecurityAuthorityName;

            EnvironmentModifiedAccountId.Text = environment.ModifiedAccountInfo.UserAccountId;

            EnvironmentModifiedAccountName.Text = environment.ModifiedAccountInfo.UserAccountName;

            EnvironmentModifiedDate.SelectedDate = environment.ModifiedAccountInfo.ActionDate;

            return;
        
        }

        protected void ApplySecurity () {

            if (application.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.EnvironmentManage)) {

                EnvironmentName.ReadOnly = false;

                EnvironmentConfidentialityStatement.ReadOnly = false;


                EnvironmentSqlServerName.ReadOnly = false;

                EnvironmentSqlDatabaseName.ReadOnly = false;


                EnvironmentSqlUseTrustedConnection.Enabled = true;

                EnvironmentSqlUserName.ReadOnly = false;

                EnvironmentSqlUserPassword.ReadOnly = false;

                EnvironmentSqlUserConfirmPassword.ReadOnly = false;


                EnvironmentSqlUseConnectionPooling.Enabled = true;

                EnvironmentSqlPoolSizeMinimum.ReadOnly = false;

                EnvironmentSqlPoolSizeMaximum.ReadOnly = false;

                EnvironmentSqlCustomAttributes.ReadOnly = false;

            }

            else {

                ButtonCancel.Visible = false;

                ButtonApply.Visible = false;

            }

        }

        #endregion


        #region Validation Methods

        protected Boolean ValidatedValues () {

            Boolean isValid = true;

            if (EnvironmentName.Text.Length == 0) { isValid = false; }

            return isValid;

        }

        protected Boolean CompareStringValues (String originalValue, String newValue) {

            Boolean isModified = false;

            if (originalValue != newValue) { isModified = true; }

            return isModified;

        }

        #endregion


        #region Button Event Handlers

        protected Boolean ApplyChanges () {

            Boolean isModified = false;

            Boolean success = false;

            Int32 poolSizeValue = 0;

            if (!ValidatedValues ()) { return false; }

            try {

                environment = (Mercury.Server.Application.Environment) Session[SessionCachePrefix + "Environment" + SessionCacheSuffix];


                if (environment.Id == 0) { isModified = true; }  // new entry

                isModified = isModified || CompareStringValues (environment.Name, EnvironmentName.Text);

                environment.Name = EnvironmentName.Text;

                isModified = isModified || CompareStringValues (environment.ConfidentialityStatement, EnvironmentConfidentialityStatement.Text);

                environment.ConfidentialityStatement = EnvironmentConfidentialityStatement.Text;


                isModified = isModified || CompareStringValues (environment.SqlConfiguration.ServerName, EnvironmentSqlServerName.Text);

                environment.SqlConfiguration.ServerName = EnvironmentSqlServerName.Text;

                isModified = isModified || CompareStringValues (environment.SqlConfiguration.DatabaseName, EnvironmentSqlDatabaseName.Text);

                environment.SqlConfiguration.DatabaseName = EnvironmentSqlDatabaseName.Text;


                isModified = isModified || CompareStringValues (environment.SqlConfiguration.UseTrustedConnection.ToString (), EnvironmentSqlUseTrustedConnection.Checked.ToString ());

                environment.SqlConfiguration.UseTrustedConnection = EnvironmentSqlUseTrustedConnection.Checked;

                isModified = isModified || CompareStringValues (environment.SqlConfiguration.SqlUserName, EnvironmentSqlUserName.Text);

                environment.SqlConfiguration.SqlUserName = EnvironmentSqlUserName.Text;


                isModified = isModified || (EnvironmentSqlUserPassword.Text.Length > 0);

                if (EnvironmentSqlUserPassword.Text == EnvironmentSqlUserConfirmPassword.Text) {

                    environment.SqlConfiguration.SqlUserPassword = EnvironmentSqlUserPassword.Text;

                }

                else {

                    SaveResponseLabel.Text = "Confirmation Password does not match.";

                    return false;

                }


                isModified = isModified || CompareStringValues (environment.SqlConfiguration.PoolingEnabled.ToString (), EnvironmentSqlUseConnectionPooling.Checked.ToString ());

                environment.SqlConfiguration.PoolingEnabled = EnvironmentSqlUseConnectionPooling.Checked;

                isModified = isModified || CompareStringValues (environment.SqlConfiguration.PoolSizeMinimum.ToString (), EnvironmentSqlPoolSizeMinimum.Text);

                environment.SqlConfiguration.PoolSizeMinimum = (Int32.TryParse (EnvironmentSqlPoolSizeMinimum.Text, out poolSizeValue)) ? poolSizeValue : 0;

                isModified = isModified || CompareStringValues (environment.SqlConfiguration.PoolSizeMaximum.ToString (), EnvironmentSqlPoolSizeMaximum.Text);

                environment.SqlConfiguration.PoolSizeMaximum = (Int32.TryParse (EnvironmentSqlPoolSizeMaximum.Text, out poolSizeValue)) ? poolSizeValue : 0;


                if ((environment.SqlConfiguration.PoolSizeMaximum < environment.SqlConfiguration.PoolSizeMinimum) && (environment.SqlConfiguration.PoolingEnabled)) {

                    SaveResponseLabel.Text = "Invalid Pool Size Configuration.";

                    return false;

                }


                isModified = isModified || CompareStringValues (environment.SqlConfiguration.CustomAttributes, EnvironmentSqlCustomAttributes.Text);

                environment.SqlConfiguration.CustomAttributes = EnvironmentSqlCustomAttributes.Text;


                if (isModified) {

                    success = application.EnvironmentSave (environment);

                    if (success) {

                        environment = application.EnvironmentGet (environment.Name);

                        Session[SessionCachePrefix + "Environment" + SessionCacheSuffix] = environment;

                        SaveResponseLabel.Text = "Save Successful.";

                        Initialize_GeneralPage ();

                    }

                    else {

                        SaveResponseLabel.Text = "Unable to Save Service.";

                        if (application.LastException != null) { SaveResponseLabel.Text = SaveResponseLabel.Text + " [" + application.LastException.Message + "]"; }

                    }

                }

                else { success = true; }

            }

            catch (Exception accessViolation) {

                SaveResponseLabel.Text = "Unable to Save Service.";

                SaveResponseLabel.Text = SaveResponseLabel.Text + " [" + accessViolation.Message + "]";

                success = false;

            }

            return success;

        }

        protected void ButtonOk_OnClick (Object sender, EventArgs eventArgs) {

            Boolean success = false;

            if (application.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.EnvironmentManage)) {

                success = ApplyChanges ();
            }

            else {

                success = true;

            }


            if (success) {

                Server.Transfer ("/WindowCloseDialog.aspx");

            }

            return;

        }

        protected void ButtonApply_OnClick (Object sender, EventArgs eventArgs) {

            Boolean success = ApplyChanges ();

            return;

        }

        protected void ButtonCancel_OnClick (Object sender, EventArgs eventArgs) {

            Server.Transfer ("/WindowCloseDialog.aspx");

            return;

        }

        #endregion

    }

}
