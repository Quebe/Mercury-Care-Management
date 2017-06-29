using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace Mercury.Web {

    public partial class Default : System.Web.UI.Page {

        Mercury.Client.Application application;


        #region Page Events

        protected void Page_PreInit (object sender, EventArgs e) {

            if (!String.IsNullOrEmpty ((String) System.Web.Configuration.WebConfigurationManager.AppSettings["PageBrandingMaster"])) {

                Page.MasterPageFile = @"~/PageBranding/" + (String) System.Web.Configuration.WebConfigurationManager.AppSettings["PageBrandingMaster"];

            }

        }

        protected void Page_Load (object sender, EventArgs e) {

            // Retrieve previous Application Object from Session Storage or 
            // create a new object and place in Session Storage 

            if (Session["Mercury.Application"] == null) {

                application = new Mercury.Client.Application ();

                Session.Add ("Mercury.Application", application);

            }

            else {

                application = (Mercury.Client.Application) Session["Mercury.Application"];

            }

            System.Diagnostics.Trace.WriteLineIf (application.TraceSwitchSecurity.TraceVerbose, "\r\n[Mercury.Web.Default] Current Credentials");

            System.Diagnostics.Trace.WriteLineIf (application.TraceSwitchSecurity.TraceVerbose, "Web Thread Principal: " + System.Threading.Thread.CurrentPrincipal.Identity.Name);

            System.Diagnostics.Trace.WriteLineIf (application.TraceSwitchSecurity.TraceVerbose, "Web System Principal: " + System.Security.Principal.WindowsIdentity.GetCurrent ().Name);

            System.Diagnostics.Trace.WriteLineIf (application.TraceSwitchSecurity.TraceVerbose, "Web Thread Authentication Type: " + System.Threading.Thread.CurrentPrincipal.Identity.AuthenticationType);


            Boolean browserError = false;
            String browserMessage = String.Empty;

            // Verify appropriate browser support (Cookies, JavaScript, IE 6+)

            if (!Request.Browser.Cookies) {
                browserError = true;
                browserMessage = "Your browser has cookies disabled. Cookies are required for storing Session state. You must enable cookies on your browser to proceed.";
            }

            if ((IsPostBack) && (JavaScriptEnabled.Text.ToLower (new System.Globalization.CultureInfo (String.Empty)) == "false")) {
                browserError = true;
                browserMessage = browserMessage + "<br>" + "Your browser either has JavaScript disabled or does not support JavaScript. You must enable JavaScript to proceed.";

            }

            if ((uiPanelEnvironment.Visible) && (Session["Mercury.Application"] == null)) {
                browserError = true;
                browserMessage = browserMessage + "<br>" + "Your browser has cookies disabled. Cookies are required for storing Session state. You must enable cookies on your browser to proceed.";

            }

            if ((Request.UserAgent.Contains (" MSIE ")) && (Request.Browser.MajorVersion < 6)) {
                browserError = true;
                browserMessage = browserMessage + "<br>" + "You are using a version of Microsoft Internet Explorer Version 6, which was released in 2001. You must upgrade your version.";

            }

            if (browserError) {
                uiPanelException.Visible = true;
                uiExceptionMessageLabel.Text = browserMessage;

                uiPanelIAmSelection.Visible = false;
                uiPanelCredentials.Visible = false;
                uiPanelEnvironment.Visible = false;

                return;

            }


            // Determine if the page is being posted back (IsPostBack) for authentication 
            // or environment selection; otherwise, perform initial load of page. 

            if (!IsPostBack) {

                // Not Post Back of Page, Initial Load

                
                // TRY WINDOWS AUTHENTICATION FIRST

                if (((System.Threading.Thread.CurrentPrincipal.Identity.AuthenticationType == "NTLM")
                        || (System.Threading.Thread.CurrentPrincipal.Identity.AuthenticationType == "Kerberos")
                        || (System.Threading.Thread.CurrentPrincipal.Identity.AuthenticationType == "Negotiate"))
                    && (System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated)
                    && (!String.IsNullOrEmpty (System.Threading.Thread.CurrentPrincipal.Identity.Name))) {

                    SessionStoreValue ("Credentials.Integrated", true);

                    application.Authenticate (String.Empty);

                    HandleAuthenticationResults ();

                } // Windows Integrated Authentication

                else {

                    InitializeIAmTypeSelector ();

                    InitializeSecurityAuthority ();

                }

            } // Web Page Postback

            else {
                // Post Back of Page, Check for Authentication Request or Environment Selection


            }

        } // Page_Load (object sender, EventArgs e)

        protected void Page_Unload (object sender, EventArgs e) {

            application.SecurityClientClose ();

            application.ApplicationClientClose ();

            return;

        }

        #endregion 


        #region Initialize UI Components

        private void InitializeIAmTypeSelector () {

            String defaultIAmType;

            if (application != null) { 


                if (!String.IsNullOrEmpty (System.Web.Configuration.WebConfigurationManager.AppSettings ["IAmSelectionDefault"])) {

                    defaultIAmType = (String) System.Web.Configuration.WebConfigurationManager.AppSettings ["IAmSelectionDefault"];

                    if (uiIAmSelectionRadioButtonList.Items.FindByText (defaultIAmType) != null) {
                        uiIAmSelectionRadioButtonList.Items.FindByText (defaultIAmType).Selected = true;

                    }

                } // IAmTypeDefault

                if (!(String.IsNullOrEmpty ((String) System.Web.Configuration.WebConfigurationManager.AppSettings ["IAmSelectionEnabled"]))) {

                    if (((String) System.Web.Configuration.WebConfigurationManager.AppSettings["IAmSelectionEnabled"]).ToLower (new System.Globalization.CultureInfo (String.Empty)) == "false") {

                        uiPanelIAmSelection.Visible = false;

                    }

                } // IAmTypeEnabled

            }

        }

        private void InitializeSecurityAuthority () {

            Dictionary<Int64, String> securityAuthorityDictionary = new Dictionary<Int64, String> ();

            String defaultSecurityAuthority;

            uiSecurityAuthorityDropDownList.Items.Clear ();

            if (application != null) {

                securityAuthorityDictionary = application.SecurityClient_SecurityAuthorityDictionary (true);

                if (securityAuthorityDictionary == null) { securityAuthorityDictionary = application.SecurityClient_SecurityAuthorityDictionary (false); }

                if (securityAuthorityDictionary == null) {

                    uiPanelException.Visible = true;

                    uiExceptionMessageLabel.Text = "Unable to retrieve available security authorities. This may prevent you from logging on to the system.";

                    if (application.LastException != null) { uiExceptionMessageLabel.Text += "([" + application.LastException.Source + "]: " + application.LastException.Message + ")"; }

                    return;

                }

                else if ((securityAuthorityDictionary.Count == 0) && (application.LastException != null)) {

                    uiPanelException.Visible = true;

                    uiExceptionMessageLabel.Text = "Unable to retrieve available security authorities. This may prevent you from logging on to the system. ([" + application.LastException.Source + "]: " + application.LastException.Message + ")";

                    return;

                }

                // Enumerate Available Security Authorities and Set Default
                foreach (String currentSecurityAuthorityName in Client.CommonFunctions.ReferenceDictionarySortByValue (securityAuthorityDictionary).Keys) {

                    // if restriction list is blank, or authority in list, add to drop down

                    if (!(String.IsNullOrEmpty ((String) System.Web.Configuration.WebConfigurationManager.AppSettings ["SecurityAuthorityRestrictionList"]))) {

                        if ((";" + ((String) System.Web.Configuration.WebConfigurationManager.AppSettings["SecurityAuthorityRestrictionList"]) + ";").Contains (";" + currentSecurityAuthorityName + ";")) {

                            uiSecurityAuthorityDropDownList.Items.Add (new ListItem (currentSecurityAuthorityName));

                        }

                    }

                    else {

                        uiSecurityAuthorityDropDownList.Items.Add (new ListItem (currentSecurityAuthorityName));

                    }


                } // SecurityAuthorityRestrictionList

                // if default authority is provided in configuration, make active selection
                if (!(String.IsNullOrEmpty ((String) System.Web.Configuration.WebConfigurationManager.AppSettings ["SecurityAuthorityDefault"]))) {

                    defaultSecurityAuthority = (String)System.Web.Configuration.WebConfigurationManager.AppSettings ["SecurityAuthorityDefault"];

                    if (uiSecurityAuthorityDropDownList.Items.FindByText (defaultSecurityAuthority) != null) {
                        uiSecurityAuthorityDropDownList.Items.FindByText (defaultSecurityAuthority).Selected = true;

                    }

                } // SecurityAuthorityDefault

                // check for accessibility of the Security Authority Selection
                if ((System.Web.Configuration.WebConfigurationManager.AppSettings ["SecurityAuthorityEnabled"] != null)
                    && (((String) System.Web.Configuration.WebConfigurationManager.AppSettings ["SecurityAuthorityEnabled"]).ToLower (new System.Globalization.CultureInfo (String.Empty)) == "false")) {

                    uiPanelSecurityAuthority.Visible = false;

                } // SecurityAuthorityEnabled

            }

        }

        private void InitializeEnvironments () {

            String defaultEnvironment;
            String[] environments = application.AuthenticationResponse.Environments.Split (';');

            uiEnvironmentDropDownList.Items.Clear ();

            if (String.IsNullOrEmpty (application.AuthenticationResponse.Environments)) {

                return;

            }

            if (application != null) {

                if (String.IsNullOrEmpty (application.AuthenticationResponse.Environments)) {

                    uiPanelException.Visible = true;

                    if (application.LastException == null) {
                        uiExceptionMessageLabel.Text = "Unable to retrieve available environments. This may prevent you from logging into the system.";

                    }

                    else {
                        uiExceptionMessageLabel.Text = "Unable to retrieve available environments. This may prevent you from logging into the system. ([" + application.LastException.Source + "]: " + application.LastException.Message + ")";

                    }

                    return;

                } // if (String.IsNullOrEmpty (application.AuthenticationResponse.Environments)) {

                // Enumerate Available Environments and Set Default
                foreach (String currentEnvironment in environments) {

                    // if restriction list is blank, or authority in list, add to drop down
                    if (!(String.IsNullOrEmpty ((String) System.Web.Configuration.WebConfigurationManager.AppSettings["EnvironmentRestrictionList"]))) {

                        if ((";" + ((String) System.Web.Configuration.WebConfigurationManager.AppSettings["EnvironmentRestrictionList"]) + ";").Contains (";" + currentEnvironment + ";")) {

                            if (uiEnvironmentDropDownList.Items.FindByText (currentEnvironment) == null) {

                                uiEnvironmentDropDownList.Items.Add (new ListItem (currentEnvironment));

                            }

                        }

                    }

                    else {

                        if (uiEnvironmentDropDownList.Items.FindByText (currentEnvironment) == null) {

                            uiEnvironmentDropDownList.Items.Add (new ListItem (currentEnvironment));

                        }

                    }

                } // EnvironmentRestrictionList

                // if default environment is provided in configuration, make active selection
                if (!(String.IsNullOrEmpty ((String) System.Web.Configuration.WebConfigurationManager.AppSettings ["EnvironmentDefault"]))) {

                    defaultEnvironment = (String) System.Web.Configuration.WebConfigurationManager.AppSettings["EnvironmentDefault"];

                    if (uiEnvironmentDropDownList.Items.FindByText (defaultEnvironment) != null) {
                        uiEnvironmentDropDownList.Items.FindByText (defaultEnvironment).Selected = true;

                    }

                } // EnvironmentDefault

            }

        }

        #endregion 


        #region Authentication

        protected void uiLogOnButton_Click (object sender, EventArgs e) {

            if (application == null) {
                return;

            }

            SessionStoreValue ("Credentials.Integrated", false);

            application.Authenticate (uiSecurityAuthorityDropDownList.SelectedValue, uiIAmSelectionRadioButtonList.SelectedValue,
                 uiLogOnNameTextBox.Text, uiPasswordTextBox.Text, uiNewPasswordTextBox.Text, uiEnvironmentDropDownList.SelectedValue);

            HandleAuthenticationResults ();

        } // uiLogOnButton_Click

        protected void uiEnvironmentSelect_Click (object sender, EventArgs e) {

            if (application == null) {
                return;

            }

            Boolean isIntegratedAuthentication = false;

            if (Session["Credentials.Integrated"] != null) { isIntegratedAuthentication = (Boolean) Session["Credentials.Integrated"]; }

            String securityAuthority;
            String userType;
            String logonName;
            String password;

            Boolean success = false;

            if (isIntegratedAuthentication) {
                success = application.Authenticate (uiEnvironmentDropDownList.SelectedValue);

            }

            else {

                securityAuthority = (String) Session["Credentials.SecurityAuthority"];
                userType = (String) Session["Credentials.IAmSelection"];
                logonName = (String) Session["Credentials.LogonName"];
                password = (String) Session["Credentials.Password"];

                success = application.Authenticate (securityAuthority, userType, logonName, password, String.Empty, uiEnvironmentDropDownList.SelectedValue);

            }

            HandleAuthenticationResults ();

            if (success) {

                RemoveCredentials ();

                // redirect;

            } 

        }

        protected void HandleAuthenticationResults () {

            String environmentName = string.Empty;

            if ((application.AuthenticationResponse.AuthenticationError == Mercury.Server.Enterprise.AuthenticationError.NoError)
                 && (!(String.IsNullOrEmpty (application.AuthenticationResponse.Token)))) {

                // valid successful logon

                Response.Redirect ("OpenApplication.aspx", false);

                return;

            }

            else {

                if (application.AuthenticationResponse.AuthenticationError == Mercury.Server.Enterprise.AuthenticationError.MustSelectEnvironment) {

                    #region Must Select Environment

                    if (uiEnvironmentDropDownList.SelectedIndex > -1) {

                        environmentName = uiEnvironmentDropDownList.Items[uiEnvironmentDropDownList.SelectedIndex].Text;

                    }

                    uiExceptionImage.Visible = false;
                    uiPanelException.Visible = false;
                    uiPanelIAmSelection.Visible = false;
                    uiPanelCredentials.Visible = false;
                    uiPanelChangePassword.Visible = false;
                    uiPanelSecurityAuthority.Visible = false;

                    InitializeEnvironments ();
                    uiPanelEnvironment.Visible = true;

                    StoreCredentials ();

                    if (uiEnvironmentDropDownList.Items.FindByText (environmentName) != null) {

                        uiEnvironmentDropDownList.Items.Remove (uiEnvironmentDropDownList.Items.FindByText (environmentName));

                    }



                    // if no available environments, disable logon select
                    if (uiEnvironmentDropDownList.Items.Count == 0) {

                        uiPanelException.Visible = true;
                        uiExceptionImage.Visible = true;

                        uiExceptionMessageLabel.Text = "No available environments were identified for authentication. You cannot log on.";

                        uiEnvironmentDropDownList.Items.Add ("No Available Environments");

                        uiEnvironmentSelect.Enabled = false;

                    }

                    // if only one available environment for the user, select it
                    else if ((uiEnvironmentDropDownList.Items.Count == 1) && (!IsPostBack)) {

                        uiEnvironmentSelect_Click (null, null);

                    }


                    if (application.AuthenticationResponse.HasException) {

                        uiPanelException.Visible = true;

                        uiExceptionMessageLabel.Text = application.AuthenticationResponse.Exception.Message;

                    }

                    #endregion 

                }

                else {

                    InitializeIAmTypeSelector ();

                    InitializeSecurityAuthority ();


                    uiPanelException.Visible = true;

                    uiExceptionImage.Visible = true;

                    if (application.LastException != null) {

                        uiExceptionMessageLabel.Text = "Unable to use Windows Integrated Authentication. ([" + application.LastException.Source + "]: " + application.LastException.Message + ")";

                    }

                    else if (application.AuthenticationResponse.HasException) { 

                        uiExceptionMessageLabel.Text = application.AuthenticationResponse.Exception.Message;

                    }

                    else {
                        uiExceptionMessageLabel.Text = "Unhandled Exception. Unable to authenticate.";

                    }

                }

            }

        } // HandleAuthenticationResults

        protected void SessionStoreValue (String name, Object value) {

            if (Session [name] != null) {
                Session.Remove (name);

            }

            Session.Add (name, value);

            return;

        } // SessionStoreValue

        protected void StoreCredentials () {

            SessionStoreValue ("Credentials.SecurityAuthority", uiSecurityAuthorityDropDownList.SelectedValue);
            SessionStoreValue ("Credentials.IAmSelection", uiIAmSelectionRadioButtonList.SelectedValue);
            SessionStoreValue ("Credentials.LogonName", uiLogOnNameTextBox.Text);

            if (!String.IsNullOrEmpty (uiNewPasswordTextBox.Text)) {
                SessionStoreValue ("Credentials.Password", uiNewPasswordTextBox.Text);

            }

            else {
                SessionStoreValue ("Credentials.Password", uiPasswordTextBox.Text);

            }

        } // StoreCredentials

        protected void RemoveCredentials () {

            SessionStoreValue ("Credentials.LogonName", null);
            SessionStoreValue ("Credentials.Password",  null);

        } // RemoveCredentials

        #endregion


    }

}
