using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Security {

    public class Security {

        #region Private Properties

        private Mercury.Server.Application application = new Mercury.Server.Application ();

        #endregion 


        #region Support Functions

        protected void SetLastException (Exception exception) {

            application.SetLastException (exception);

            return;

        }

        protected void SetEnvironment (Mercury.Server.Security.AuthenticationResponse authenticationResponse, String environment) {

            if (authenticationResponse.IsAuthenticated) {

                authenticationResponse.IsAuthenticated = false;

                authenticationResponse.AuthenticationError = Mercury.Server.Public.Interfaces.Security.Enumerations.AuthenticationError.MustSelectEnvironment;

                authenticationResponse.AuthenticationException = new Exception ("Please select an environment.");

            }

            return;

        }

        protected void SetAuthenticationError (Mercury.Server.Security.AuthenticationResponse authenticationResponse, Mercury.Server.Public.Interfaces.Security.Credentials credentials) {

            authenticationResponse.AuthenticationError = credentials.AuthenticationError;

            if (credentials.AuthenticationException != null) {

                authenticationResponse.AuthenticationException = credentials.AuthenticationException;

            }

            return;

        }

        protected void SetProviderCredentials (String accountType, Mercury.Server.Security.SecurityAuthority securityAuthority, Mercury.Server.Public.Interfaces.Security.Credentials credentials) {

            String userContext;

            switch (accountType.ToLower ()) {

                case "provider": userContext = securityAuthority.ProviderContext; break;

                case "member": userContext = securityAuthority.MemberContext; break;

                default: userContext = securityAuthority.AssociateContext; break;

            }


            credentials.SecurityAuthorityId = securityAuthority.Id;

            credentials.SecurityAuthorityName = securityAuthority.Name;


            credentials.Protocol = securityAuthority.Protocol;

            credentials.Domain = securityAuthority.Domain;

            credentials.Context = userContext;


            if (securityAuthority.AgentName != String.Empty) {

                credentials.SetAgentCredentials (securityAuthority.AgentName, securityAuthority.AgentPassword);

            }

            if (securityAuthority.ServerName != String.Empty) {

                credentials.ServerName = securityAuthority.ServerName;

            }

            return;

        }

        protected String EnvironmentsAvailable (Mercury.Server.Public.Interfaces.Security.Credentials credentials) {

            StringBuilder selectStatement = new StringBuilder ();

            String environments = String.Empty;
            String environmentName = String.Empty;


            selectStatement.Append ("SELECT * FROM EnvironmentAccess JOIN Environment ON EnvironmentAccess.EnvironmentId = Environment.EnvironmentId WHERE SecurityAuthorityId = " + credentials.SecurityAuthorityId.ToString ());

            selectStatement.Append (" AND SecurityGroupId IN (");

            foreach (String currentGroup in credentials.Groups) {

                selectStatement.Append ("'" + currentGroup + "', ");

            }

            selectStatement.Append ("'') ORDER BY EnvironmentName, IsGranted DESC, IsDenied DESC");

            environments = ";";

            foreach (System.Data.DataRow currentRow in application.EnterpriseDatabase.SelectDataTable (selectStatement.ToString ()).Rows) {

                environmentName = (String) currentRow ["EnvironmentName"];

                if ((Boolean) currentRow["IsGranted"]) {

                    environments = environments + environmentName + ";";

                }

                if (((Boolean) currentRow["IsDenied"]) && (environments.Contains (environmentName))) {

                    environments = environments.Replace (";" + environmentName + ";", ";");

                }

            } // foreach (System.Data.DataRow currentRow in application.EnterpriseDatabase.SelectDataTable (selectStatement.ToString ()).Rows)

            environments = environments.Substring (1, environments.Length - 1);
         
            if ((environments != String.Empty) && (environments.Substring (environments.Length - 1) == ";")) {
                environments = environments.Substring (0, environments.Length - 1);

            }

            return environments;

        }

        protected Mercury.Server.Session CreateSession (Mercury.Server.Security.SecurityAuthority securityAuthority, 
            
                Mercury.Server.Security.AuthenticationResponse authenticationResponse, 
            
                Mercury.Server.Public.Interfaces.Security.Credentials credentials, String environmentName) {


            Mercury.Server.Session session = null;

            Mercury.Server.Environment.Environment environment = null;

            Boolean connectionSuccess = false;

            if (!authenticationResponse.IsAuthenticated) {

                return null;

            }

            if (environmentName != String.Empty) {

                environment = application.EnvironmentGet (environmentName);

                Mercury.Server.Data.SqlDatabase environmentDatabase = null;

                if (environment != null) {

                    environmentDatabase = new Mercury.Server.Data.SqlDatabase (environment.SqlConfiguration);

                    connectionSuccess = environmentDatabase.Connect ();

                }

                if (!connectionSuccess) {

                    if (environmentDatabase != null) { application.SetLastException (environmentDatabase.LastException); }

                    authenticationResponse.IsAuthenticated = false;

                    authenticationResponse.Environments = EnvironmentsAvailable (credentials);

                    credentials.AuthenticationError = Mercury.Server.Public.Interfaces.Security.Enumerations.AuthenticationError.MustSelectEnvironment;

                    credentials.AuthenticationException = new Exception ("Unable to connect to requested environment.");

                    return null;

                }

            }

            // empty environment or environment selection not allowed for user 
            if ((environmentName == String.Empty) || (!((";" + EnvironmentsAvailable (credentials) + ";").Contains (";" + environmentName + ";"))) || (environment == null)) {

                authenticationResponse.IsAuthenticated = false;

                authenticationResponse.Environments = EnvironmentsAvailable (credentials);

                credentials.AuthenticationError = Mercury.Server.Public.Interfaces.Security.Enumerations.AuthenticationError.MustSelectEnvironment;

            }

            else {

                credentials.Environment = environmentName;

                authenticationResponse.IsAuthenticated = true;

                authenticationResponse.AuthenticationError = Mercury.Server.Public.Interfaces.Security.Enumerations.AuthenticationError.NoError;

                session = application.CreateSession (credentials);

                authenticationResponse.Token = session.Token;

                authenticationResponse.SecurityAuthorityId = session.SecurityAuthorityId;

                authenticationResponse.SecurityAuthorityName = session.SecurityAuthorityName;

                authenticationResponse.SecurityAuthorityType = session.SecurityAuthorityType;

                authenticationResponse.EnvironmentId = environment.Id;

                authenticationResponse.EnvironmentName = environment.Name;

                authenticationResponse.ConfidentialityStatement = environment.ConfidentialityStatement;

                authenticationResponse.UserAccountId = session.UserAccountId;

                authenticationResponse.UserAccountName = session.UserAccountName;

                authenticationResponse.UserDisplayName = session.UserDisplayName;

                authenticationResponse.GroupMembership = session.GroupMembership;

                authenticationResponse.RoleMembership = session.RoleMembership;

                authenticationResponse.EnterprisePermissionSet = session.EnterprisePermissionSet;

                authenticationResponse.EnvironmentPermissionSet = session.EnvironmentPermissionSet;

                authenticationResponse.WorkQueuePermissions = session.WorkQueuePermissions;

                authenticationResponse.WorkTeamMembership = session.WorkTeamMembership;

            }

            return session;

        }

        #endregion


        #region Public Anonymous Methods 

        public Dictionary<Int64, String> SecurityAuthorityDictionary () {

            Dictionary<Int64, String> securityAuthorities = new Dictionary<Int64, String> ();

            try {

                if (!application.EnterpriseDatabase.Connect ()) {

                    throw new Exception ("Unable to establish Database Connection.", application.EnterpriseDatabase.LastException);

                }

                else {

                    foreach (System.Data.DataRow currentRow in application.EnterpriseDatabase.SelectDataTable ("SELECT * FROM SecurityAuthority WHERE SecurityAuthorityType = " + ((Int32) Enumerations.SecurityAuthorityType.ActiveDirectory).ToString ()).Rows) {

                        securityAuthorities.Add ((Int64) currentRow["SecurityAuthorityId"], (String) currentRow["SecurityAuthorityName"]);

                    }
                    
                }

            }

            catch (System.NullReferenceException nullReferenceException) {

                SetLastException (new ApplicationException ("Unable to establish Database Connection.", nullReferenceException));
                
            }

            catch (Exception handledException) {

                application.SetLastException (handledException);

                SetLastException (handledException);

            }

            return securityAuthorities;
            
        }

        public Mercury.Server.Security.AuthenticationResponse Authenticate (String securityAuthorityName, String accountType, String accountName, String password, String newPassword, String environment) {

            Mercury.Server.Security.AuthenticationResponse authenticationResponse = new AuthenticationResponse ();

            Mercury.Server.Public.Interfaces.Security.Credentials credentials = new Public.Interfaces.Security.Credentials ("", "", "", accountName, password, newPassword);

            Mercury.Server.Security.SecurityAuthority securityAuthority = application.SecurityAuthorityGet (securityAuthorityName);

            Mercury.Server.Security.Providers.ActiveDirectory.Provider activeDirectoryProvider;

            Mercury.Server.Session session = null;


            if (securityAuthority == null) {

                authenticationResponse.AuthenticationError = Mercury.Server.Public.Interfaces.Security.Enumerations.AuthenticationError.SecurityAuthorityError;

                authenticationResponse.AuthenticationException = new Exception ("Unable to retreive Security Authority information from the database.", application.LastException);

                return authenticationResponse;

            }

            SetProviderCredentials (accountType, securityAuthority, credentials);

            activeDirectoryProvider = new Providers.ActiveDirectory.Provider (credentials);

            authenticationResponse.IsAuthenticated = activeDirectoryProvider.Authenticate ();

            if (authenticationResponse.IsAuthenticated) {

                session = CreateSession (securityAuthority, authenticationResponse, credentials, environment);

            }

            SetAuthenticationError (authenticationResponse, credentials);

            return authenticationResponse;

        }

        public Mercury.Server.Security.AuthenticationResponse Authenticate (String environment) {

            AuthenticationResponse authenticationResponse = new AuthenticationResponse ();

            Mercury.Server.Public.Interfaces.Security.Credentials credentials = new Public.Interfaces.Security.Credentials ();

            Mercury.Server.Security.SecurityAuthority securityAuthority;

            Mercury.Server.Session session = null;

            try {

                if (((System.Threading.Thread.CurrentPrincipal.Identity.AuthenticationType == "NTLM")
                        || (System.Threading.Thread.CurrentPrincipal.Identity.AuthenticationType == "Kerberos")
                        || (System.Threading.Thread.CurrentPrincipal.Identity.AuthenticationType == "Negotiate")
                        )
                    && (System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated)
                    && (!String.IsNullOrEmpty (System.Threading.Thread.CurrentPrincipal.Identity.Name))) {


                    #region Retreive Credentials from Thread.CurrentPrincipal

                    credentials.Domain = System.Threading.Thread.CurrentPrincipal.Identity.Name.Split ('\\')[0];

                    credentials.UserName = System.Threading.Thread.CurrentPrincipal.Identity.Name.Split ('\\')[1];

                    application.TraceWriteLineInfo (application.TraceSwitchSecurity, "\r\n[Mercury.Server.Security.Authenticate] Credentials: " + credentials.Domain + "\\" + credentials.UserName);

                    #endregion 

                    #region Retreive Security Authority for Domain and Authenticate

                    // validate that the domain is a trusted security authority

                    securityAuthority = application.SecurityAuthorityGet (credentials.Domain);

                    if (securityAuthority != null) {

                        if (securityAuthority.SecurityAuthorityType == Enumerations.SecurityAuthorityType.WindowsIntegrated) {

                            #region Authenticate

                            SetProviderCredentials (String.Empty, securityAuthority, credentials);

                            Mercury.Server.Security.Providers.WindowsIntegrated.Provider windowsProvider = new Providers.WindowsIntegrated.Provider ();

                            authenticationResponse.IsAuthenticated = windowsProvider.Authenticate (credentials);

                            if (authenticationResponse.IsAuthenticated) {

                                session = CreateSession (securityAuthority, authenticationResponse, credentials, environment);

                            }

                            SetAuthenticationError (authenticationResponse, credentials);

                            #endregion 

                        }

                        else {

                            #region SECURITY AUTHORITY TYPE NOT WINDOWS INTEGRATED

                            authenticationResponse.IsAuthenticated = false;

                            credentials.AuthenticationError = Public.Interfaces.Security.Enumerations.AuthenticationError.SecurityAuthorityError;

                            SetAuthenticationError (authenticationResponse, credentials);

                            authenticationResponse.AuthenticationException = new ApplicationException ("[Windows Integrated Authentication: " + credentials.Domain + "\\" + credentials.UserName + "]: Security Authority Type is not Windows Integrated.");

                            application.TraceWriteLineWarning (application.TraceSwitchSecurity, "\r\n[Windows Integrated Authentication: " + credentials.Domain + "\\" + credentials.UserName + "]: Security Authority Type is not Windows Integrated.");

                            #endregion 

                        }


                    }

                    else { 
                        
                        #region SECURITY AUTHORITY NOT FOUND

                        authenticationResponse.IsAuthenticated = false;

                        credentials.AuthenticationError = Public.Interfaces.Security.Enumerations.AuthenticationError.SecurityAuthorityError;

                        SetAuthenticationError (authenticationResponse, credentials);

                        authenticationResponse.AuthenticationException = new ApplicationException ("[Windows Integrated Authentication: " + credentials.Domain + "\\" + credentials.UserName + "]: Security Authority Not Found.");

                        application.TraceWriteLineWarning (application.TraceSwitchSecurity, "\r\n[Windows Integrated Authentication: " + credentials.Domain + "\\" + credentials.UserName + "]: Security Authority Not Found.");

                        #endregion

                    }

                    #endregion

                }

                else {

                    credentials.AuthenticationError = Mercury.Server.Public.Interfaces.Security.Enumerations.AuthenticationError.InvalidUserOrPassword;

                    SetAuthenticationError (authenticationResponse, credentials);

                }

            }

            catch (Exception domainAccountException) {

                authenticationResponse.IsAuthenticated = false;

                credentials.AuthenticationError = Mercury.Server.Public.Interfaces.Security.Enumerations.AuthenticationError.InvalidUserOrPassword;

                SetAuthenticationError (authenticationResponse, credentials);

                authenticationResponse.AuthenticationException = new ApplicationException ("[Windows Integrated Authentication: " + credentials.Domain + "\\" + credentials.UserName + "] " + authenticationResponse.AuthenticationException.Message, domainAccountException);

                application.TraceWriteLineError (application.TraceSwitchSecurity, "[Windows Integrated Authentication: " + credentials.Domain + "\\" + credentials.UserName + "] " + authenticationResponse.AuthenticationException.Message);

                application.TraceWriteLineError (application.TraceSwitchSecurity, "[Windows Integrated Authentication: " + credentials.Domain + "\\" + credentials.UserName + "] " + domainAccountException.Message);
                
            }

            return authenticationResponse;

        }

        public Mercury.Server.Security.AuthenticationResponse Authenticate (Mercury.Server.Data.SqlConfiguration enterpriseConfiguration, String environment) {

            application = new Application (enterpriseConfiguration);

            return Authenticate (environment);

        }

        #endregion

    }

}
