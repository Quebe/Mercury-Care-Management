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

    public partial class SecurityAuthorityProperties : System.Web.UI.Page {

        String SessionCachePrefix = String.Empty;

        String SessionCacheSuffix = String.Empty;
        
        protected Mercury.Client.Application application;

        Mercury.Server.Application.SecurityAuthority securityAuthority = new Mercury.Server.Application.SecurityAuthority ();


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            Int64 securityAuthorityId;

            String securityAuthorityName;


            if (Session["Mercury.Application"] == null) { Response.RedirectLocation = "/SessionExpired.aspx"; return; }

            application = (Mercury.Client.Application) Session["Mercury.Application"];

            if (!application.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.SecurityAuthorityReview)) {

                if (!IsPostBack) { Server.Transfer ("/PermissionDenied.aspx"); }

                else { Response.RedirectLocation = "/PermissionDenied.aspx"; }

                return;

            }

            ButtonApply.Click += new EventHandler (this.ButtonApply_OnClick);

            ButtonOk.Click += new EventHandler (this.ButtonOk_OnClick);

            ButtonCancel.Click += new EventHandler (this.ButtonCancel_OnClick);

            if ((application != null) && (!Page.IsPostBack)) {

                #region Initial Page Load

                SessionCachePrefix = Form.Name;

                PageInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", "");

                SessionCacheSuffix = PageInstanceId.Text;


                securityAuthorityName = Request.QueryString["SecurityAuthorityName"];

                securityAuthorityId = 0;

                Session.Add (SessionCachePrefix + "SecurityAuthorityName", securityAuthorityName);


                System.Collections.Generic.Dictionary<Int64, String> securityAuthorityDictionary = application.SecurityAuthorityDictionary (false);

                foreach (Int64 currentKey in securityAuthorityDictionary.Keys) {

                    if (securityAuthorityDictionary[currentKey] == securityAuthorityName) {

                        securityAuthorityId = currentKey;

                        Session.Add (SessionCachePrefix + "SecurityAuthorityId" + SessionCacheSuffix, currentKey);

                        break;

                    }

                }

                if (securityAuthorityId != 0) {

                    securityAuthority = application.SecurityAuthorityGet (securityAuthorityName, false);

                    Page.Title = "Security Authority Properties";

                    Page.Title = Page.Title + " (" + securityAuthorityName + ")";

                }

                else {

                    Session.Add (SessionCachePrefix + "SecurityAuthorityId" + SessionCacheSuffix, 0);

                    securityAuthority = new Mercury.Server.Application.SecurityAuthority ();

                    Page.Title = "New Security Authority";

                    if (!application.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.SecurityAuthorityManage)) {

                        Server.Transfer ("/PermissionDenied.aspx");

                    }

                }

                Session[SessionCachePrefix + "SecurityAuthority" + SessionCacheSuffix] = securityAuthority;

                if (securityAuthorityId != 0) {

                    Initialize_GeneralPage ();

                }

                ApplySecurity ();

                #endregion

            } // Initial Page Load

            else { // Postback

                SessionCachePrefix = Form.Name;

                SessionCacheSuffix = PageInstanceId.Text;


                securityAuthorityId = Int64.Parse (Session[SessionCachePrefix + "SecurityAuthorityId" + SessionCacheSuffix].ToString ());

                securityAuthorityName = (String) Session[SessionCachePrefix + "SecurityAuthorityName" + SessionCacheSuffix];

                securityAuthority = (Mercury.Server.Application.SecurityAuthority) Session[SessionCachePrefix + "SecurityAuthority" + SessionCacheSuffix];

            }

            return;

        }

        #endregion


        #region Initializations

        protected void Initialize_GeneralPage () {

//            SecurityAuthorityId.Text = securityAuthority.SecurityAuthorityId.ToString ();

            SecurityAuthorityName.Text = securityAuthority.Name;

            SecurityAuthorityType.SelectedValue = securityAuthority.SecurityAuthorityType.ToString ();


            SecurityAuthorityProtocol.Text = securityAuthority.Protocol;

            SecurityAuthorityServerName.Text = securityAuthority.ServerName;

            SecurityAuthorityDomain.Text = securityAuthority.Domain;


            SecurityAuthorityMemberContext.Text = securityAuthority.MemberContext;

            SecurityAuthorityProviderContext.Text = securityAuthority.ProviderContext;

            SecurityAuthorityAssociateContext.Text = securityAuthority.AssociateContext;


            SecurityAuthorityAgentName.Text = securityAuthority.AgentName;

            SecurityAuthorityAgentPassword.Text = securityAuthority.AgentPassword;


            SecurityAuthorityAssemblyPath.Text = securityAuthority.ProviderAssemblyPath;

            SecurityAuthorityAssemblyName.Text = securityAuthority.ProviderAssemblyName;


            SecurityAuthorityProviderNamespace.Text = securityAuthority.ProviderNamespace;

            SecurityAuthorityProviderClassName.Text = securityAuthority.ProviderClassName;

            SecurityAuthorityConfigurationSection.Text = securityAuthority.ConfigurationSection;


            SecurityAuthorityCreateAuthorityName.Text = securityAuthority.CreateAccountInfo.SecurityAuthorityName;

            SecurityAuthorityCreateAccountId.Text = securityAuthority.CreateAccountInfo.UserAccountId;

            SecurityAuthorityCreateAccountName.Text = securityAuthority.CreateAccountInfo.UserAccountName;

            SecurityAuthorityCreateDate.SelectedDate = securityAuthority.CreateAccountInfo.ActionDate;


            SecurityAuthorityModifiedAuthorityName.Text = securityAuthority.ModifiedAccountInfo.SecurityAuthorityName;

            SecurityAuthorityModifiedAccountId.Text = securityAuthority.ModifiedAccountInfo.UserAccountId;

            SecurityAuthorityModifiedAccountName.Text = securityAuthority.ModifiedAccountInfo.UserAccountName;

            SecurityAuthorityModifiedDate.SelectedDate = securityAuthority.ModifiedAccountInfo.ActionDate;

            return;

        }

        protected void ApplySecurity () {

            if (application.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.SecurityAuthorityManage)) {

                SecurityAuthorityName.ReadOnly = false;

                // SecurityAuthoritySecurityAuthorityType

                SecurityAuthorityProtocol.ReadOnly = false;

                SecurityAuthorityServerName.ReadOnly = false;

                SecurityAuthorityDomain.ReadOnly = false;


                SecurityAuthorityMemberContext.ReadOnly = false;

                SecurityAuthorityProviderContext.ReadOnly = false;

                SecurityAuthorityAssociateContext.ReadOnly = false;


                SecurityAuthorityAgentName.ReadOnly = false;

                SecurityAuthorityAgentPassword.ReadOnly = false;


                SecurityAuthorityAssemblyPath.ReadOnly = false;

                SecurityAuthorityAssemblyName.ReadOnly = false;


                SecurityAuthorityProviderNamespace.ReadOnly = false;

                SecurityAuthorityProviderClassName.ReadOnly = false;

                SecurityAuthorityConfigurationSection.ReadOnly = false;

            }

            else {

                ButtonCancel.Visible = false;

                ButtonApply.Visible = false;

            }

            return;

        }

        #endregion


        #region Validation Methods

        protected Boolean ValidatedValues () {

            Boolean isValid = true;

            if (SecurityAuthorityName.Text.Length == 0) { isValid = false; }

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


            if (!ValidatedValues ()) { return false; }

            try {

                securityAuthority = (Mercury.Server.Application.SecurityAuthority) Session[SessionCachePrefix + "SecurityAuthority" + SessionCacheSuffix];

                if (securityAuthority.Id == 0) { isModified = true; }  // new entry

                isModified = isModified || CompareStringValues (securityAuthority.Name, SecurityAuthorityName.Text);

                securityAuthority.Name = SecurityAuthorityName.Text;

                // TODO: UPDATE V2

                //isModified = isModified || CompareStringValues (securityAuthority.SecurityAuthorityType, SecurityAuthorityType.SelectedValue);

                //securityAuthority.SecurityAuthorityType = SecurityAuthorityType.Text;

                isModified = isModified || CompareStringValues (securityAuthority.Protocol, SecurityAuthorityProtocol.Text);

                securityAuthority.Protocol = SecurityAuthorityProtocol.Text;

                isModified = isModified || CompareStringValues (securityAuthority.ServerName, SecurityAuthorityServerName.Text);

                securityAuthority.ServerName = SecurityAuthorityServerName.Text;

                isModified = isModified || CompareStringValues (securityAuthority.Domain, SecurityAuthorityDomain.Text);

                securityAuthority.Domain = SecurityAuthorityDomain.Text;

                isModified = isModified || CompareStringValues (securityAuthority.MemberContext, SecurityAuthorityMemberContext.Text);

                securityAuthority.MemberContext = SecurityAuthorityMemberContext.Text;

                isModified = isModified || CompareStringValues (securityAuthority.ProviderContext, SecurityAuthorityProviderContext.Text);

                securityAuthority.ProviderContext = SecurityAuthorityProviderContext.Text;

                isModified = isModified || CompareStringValues (securityAuthority.AssociateContext, SecurityAuthorityAssociateContext.Text);

                securityAuthority.AssociateContext = SecurityAuthorityAssociateContext.Text;

                isModified = isModified || CompareStringValues (securityAuthority.AgentName, SecurityAuthorityAgentName.Text);

                securityAuthority.AgentName = SecurityAuthorityAgentName.Text;

                isModified = isModified || CompareStringValues (securityAuthority.AgentPassword, SecurityAuthorityAgentPassword.Text);

                securityAuthority.AgentPassword = SecurityAuthorityAgentPassword.Text;

                isModified = isModified || CompareStringValues (securityAuthority.ProviderAssemblyPath, SecurityAuthorityAssemblyPath.Text);

                securityAuthority.ProviderAssemblyPath = SecurityAuthorityAssemblyPath.Text;

                isModified = isModified || CompareStringValues (securityAuthority.ProviderAssemblyName, SecurityAuthorityAssemblyName.Text);

                securityAuthority.ProviderAssemblyName = SecurityAuthorityAssemblyName.Text;

                isModified = isModified || CompareStringValues (securityAuthority.ProviderNamespace, SecurityAuthorityProviderNamespace.Text);

                securityAuthority.ProviderNamespace = SecurityAuthorityProviderNamespace.Text;

                isModified = isModified || CompareStringValues (securityAuthority.ProviderClassName, SecurityAuthorityProviderClassName.Text);

                securityAuthority.ProviderClassName = SecurityAuthorityProviderClassName.Text;

                isModified = isModified || CompareStringValues (securityAuthority.ConfigurationSection, SecurityAuthorityConfigurationSection.Text);

                securityAuthority.ConfigurationSection = SecurityAuthorityConfigurationSection.Text;

                if (isModified) {

                    success = application.SecurityAuthoritySave (securityAuthority);


                    if (success) {

                        securityAuthority = application.SecurityAuthorityGet (securityAuthority.Name, false);

                        Session[SessionCachePrefix + "SecurityAuthority" + SessionCacheSuffix] = securityAuthority;

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

            if (application.HasEnvironmentPermission (Mercury.Server.EnterprisePermissions.SecurityAuthorityManage)) {

                success = ApplyChanges ();

            }

            else {

                success = true;

            }


            if (success) {

                Server.Transfer ("/WindowCloseDialog.aspx");

            }

        }

        protected void ButtonApply_OnClick (Object sender, EventArgs eventArgs) {

            Boolean success = ApplyChanges ();

        }

        protected void ButtonCancel_OnClick (Object sender, EventArgs eventArgs) {

            Server.Transfer ("/WindowCloseDialog.aspx");

        }

        #endregion

    }

}
