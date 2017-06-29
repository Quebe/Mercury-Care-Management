using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Mercury.Server.Services.Responses;

namespace Mercury.Server.Services.Enterprise {

    public class Security : ISecurity {
        
        public DictionaryResponse SecurityAuthorityDictionary () {

            Responses.DictionaryResponse response = new Responses.DictionaryResponse ();

            try {

                Mercury.Server.Security.Security security = new Server.Security.Security ();

                response.Dictionary = security.SecurityAuthorityDictionary ();

            }

            catch (Exception applicaitonException) {

                response.SetException (applicaitonException);

            }

            return response;

        }

        public AuthenticationResponse Authenticate (String authorityName, String accountType, String accountName, String password, String newPassword, String environment) {

            Mercury.Server.Security.Security security = new Mercury.Server.Security.Security ();

            return new AuthenticationResponse (security.Authenticate (authorityName, accountType, accountName, password, newPassword, environment));

        }

        public AuthenticationResponse AuthenticateWindows (String environment) {

            AuthenticationResponse response = new AuthenticationResponse ();

            try {

                Mercury.Server.Security.Security security = new Mercury.Server.Security.Security ();

                response = new AuthenticationResponse (security.Authenticate (environment));

            }

            catch (Exception authenticationException) {

                Server.Application application = new Application ();

                application.SetLastException (authenticationException);

                response.SetException (authenticationException);

            }

            return response;

        }

        public AuthenticationResponse AuthenticateToken (String securityAuthority, String token, String environment) { return null; }

        public BooleanResponse LogOff (String token) {

            BooleanResponse response = new BooleanResponse ();

            CacheManager cacheManager = new CacheManager ();


            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.LogOff ();

                response.Result = true;

            }

            catch (Exception applicationException) {

                response.Result = false;

                response.SetException (applicationException);

            }

            return response;

        }

    }

}
