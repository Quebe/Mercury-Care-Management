using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Public.Interfaces.Security {

    public class Credentials {

        #region Private Properties

        private Int64 securityAuthorityId = 0;

        private String securityAuthorityName = String.Empty;
        

        private String token;
        
        private String protocol;

        private String agentName;

        private String agentPassword;

        private String serverName;

        private String domain;

        private String context;

        private String userName;

        private String password;

        private String newPassword;

        private String environment;

        private Boolean isAuthenticated = false;

        private String userAccountId;

        private String userAccountName;

        private String userDisplayName;

        private List<String> groups = new List<String>();


        private Int32 autenticationTime = 0;

        private Enumerations.AuthenticationError authenticationError = Enumerations.AuthenticationError.NoError;

        private Exception authenticationException;

        #endregion


        #region Public Properties

        public Int64 SecurityAuthorityId { get { return securityAuthorityId; } set { securityAuthorityId = value; } }

        public String SecurityAuthorityName { get { return securityAuthorityName; } set { securityAuthorityName = value; } }

        
        public String Token { get { return token;  } set { token = value; } }

        public String Protocol { get { return protocol; } set { protocol = value; } }
        
        public String AgentName { get { return agentName;  } set { agentName = value; } }

        public String AgentPassword { get { return agentPassword;  } set { agentPassword = value; } }

        public String ServerName { get { return serverName;  } set { serverName = value; } }

        public String Domain { get { return domain;  } set { domain = value; } }

        public String Context { get { return context;  } set { context = value; } }

        public String UserName { get { return userName;  } set { userName = value; } }

        public String Password { get { return password;  } set { password = value; } }

        public String NewPassword { get { return newPassword;  } set { newPassword = value; } }

        public String Environment { get { return environment;  } set { environment = value; } }

        public Boolean IsAuthenticated { get { return isAuthenticated;  } set { isAuthenticated = value; }  }


        public String UserAccountId { get { return userAccountId; } set { userAccountId = value; } }

        public String UserAccountName { get { return userAccountName; } set { userAccountName = value; } }

        public String UserDisplayName { get { return userDisplayName; } set { userDisplayName = value; } }

        public List<String> Groups { get { return groups;  } set { groups = value; } }


        public Int32 AuthenticationTime { get { return autenticationTime; } set { autenticationTime = value; } }

        public Enumerations.AuthenticationError AuthenticationError { get { return authenticationError;  }

            set { 

                authenticationError = value;

                switch (authenticationError) {

                    case Enumerations.AuthenticationError.InvalidUserOrPassword:

                        authenticationException = new ApplicationException ("Logon failure: unknown user name or bad password.");

                        break;

                    case Enumerations.AuthenticationError.AccountLockedDisabledExpired:

                        authenticationException = new ApplicationException ("Logon failure: account currently disabled, locked, or expired.");

                        break;

                    case Enumerations.AuthenticationError.MustChangePassword:
                    case Enumerations.AuthenticationError.PasswordExpired:

                        authenticationException = new ApplicationException ("Logon failure: the specified account password has expired and must be changed.");

                        break;

                    case Enumerations.AuthenticationError.PasswordRestriction:

                        authenticationException = new ApplicationException ("Unable to update the password. The value provided for the new password does not meet the length, complexity, or history requirement of the domain.");

                        break;

                    case Enumerations.AuthenticationError.MustSelectEnvironment:

                        authenticationException = new ApplicationException ("Please select an environment.");

                        break;

                    case Enumerations.AuthenticationError.SecurityAuthorityError:

                        authenticationException = new ApplicationException ("A system error occurred while authenticating against the Security Authority. Please contact your System Administrator.");

                        break;

                    case Enumerations.AuthenticationError.UnableToCreateSession:

                        authenticationException = new ApplicationException ("A system error occurred while authenticating. Unable to create a Session.");

                        break;

                    default:

                        authenticationException = new ApplicationException ("Unknown or undefined error occured while authenticating (Error " + value.ToString () + ").");

                        break;

                }

            }

        }

        public Exception AuthenticationException { get { return authenticationException;  } set { authenticationException = value; }  }

        public Boolean HasException { get { if (authenticationException != null) { return true; } else { return false; } }  }

        #endregion


        #region Constructors

        public Credentials () { return; }

        public Credentials (String forToken) { Token = forToken; return;  }

        public Credentials (String forProtocol, String forDomain, String forContext, String forUserName, String forPassword, String forNewPassword) {

            Protocol = forProtocol;

            Domain = forDomain;
            Context = forContext;

            UserName = forUserName;
            Password = forPassword;
            NewPassword = forNewPassword;

            return;

        }

        #endregion


        #region Methods

        public void SetAgentCredentials (String forAgentName, String forAgentPassword) {

            AgentName = forAgentName;

            AgentPassword = forAgentPassword;

            return;

        }

        #endregion

    }

}
