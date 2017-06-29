using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercury.Clients.Mvc.Models.Login {

    public class Login {

        #region Private Properties

        private Server.Enterprise.AuthenticationResponse authenticationResponse;

        private String environmentName = String.Empty;

        #endregion 


        #region Public Properties

        public Server.Enterprise.AuthenticationResponse AuthenticationResponse { get { return authenticationResponse; } set { authenticationResponse = value; } }

        public String EnvironmentName { get { return environmentName; } set { environmentName = value ?? String.Empty; } }

        public String ExceptionMessage { get { return (authenticationResponse != null) ? ((authenticationResponse.Exception != null) ? authenticationResponse.Exception.Message : String.Empty) : String.Empty; } }

        public List<String> EnvironmentsAvailable { 
            
            get {

                List<String> environmentsAvailable = new List<String> ();

                if (authenticationResponse != null) {

                    if (authenticationResponse.Environments != null) {

                        environmentsAvailable = authenticationResponse.Environments.Split (';').ToList ();

                    }
                
                }

                return environmentsAvailable;  

            }

        }

        #endregion 


        #region Constructors

        public Login (Server.Enterprise.AuthenticationResponse forAuthenticationResponse, String forEnvironmentName) {

            authenticationResponse = forAuthenticationResponse;

            environmentName = forEnvironmentName;
            
            return;

        }

        #endregion 

    }

}