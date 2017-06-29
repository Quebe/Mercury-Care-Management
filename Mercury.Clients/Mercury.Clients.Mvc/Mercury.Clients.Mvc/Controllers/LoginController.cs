using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercury.Clients.Mvc.Controllers {

    public class LoginController : Controller {

        #region Private Properties

        

        #endregion 


        #region Public Properties

        public Mercury.Client.Application MercuryApplication {

            get {

                // RETREIVE PREVIOUS APPLICATION OBJECT FROM SESSION STORAGE 

                // OR CREATE A NEW OBJECT AND PLACE IN STORAGE

                Mercury.Client.Application mercuryApplication = (Mercury.Client.Application)Session["Mercury.Client.Application"];

                if (mercuryApplication == null) { 
                    
                    mercuryApplication = new Client.Application ();

                    MercuryApplication = mercuryApplication; // CALL SET ACCESSOR

                }

                return mercuryApplication;

            }

            set { Session["Mercury.Client.Application"] = value; }

        }

        #endregion 


        #region Public Methods

        // INDEX; INDEX/ENVIRONMENT_NAME

        public ActionResult Index(String environmentName) {

            Models.Login.Login loginModel = null;


            System.Diagnostics.Trace.WriteLineIf (MercuryApplication.TraceSwitchSecurity.TraceVerbose, "\r\n[Mercury.Web.Default] Current Credentials");

            System.Diagnostics.Trace.WriteLineIf (MercuryApplication.TraceSwitchSecurity.TraceVerbose, "Web Thread Principal: " + System.Threading.Thread.CurrentPrincipal.Identity.Name);

            System.Diagnostics.Trace.WriteLineIf (MercuryApplication.TraceSwitchSecurity.TraceVerbose, "Web System Principal: " + System.Security.Principal.WindowsIdentity.GetCurrent ().Name);

            System.Diagnostics.Trace.WriteLineIf (MercuryApplication.TraceSwitchSecurity.TraceVerbose, "Web Thread Authentication Type: " + System.Threading.Thread.CurrentPrincipal.Identity.AuthenticationType);


            if (((System.Threading.Thread.CurrentPrincipal.Identity.AuthenticationType == "NTLM")

                    || (System.Threading.Thread.CurrentPrincipal.Identity.AuthenticationType == "Kerberos")

                    || (System.Threading.Thread.CurrentPrincipal.Identity.AuthenticationType == "Negotiate"))

                && (System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated)

                && (!String.IsNullOrWhiteSpace (System.Threading.Thread.CurrentPrincipal.Identity.Name))) {


                MercuryApplication.Authenticate (environmentName); // AUTHENTICATION REQUEST

                if ((MercuryApplication.AuthenticationResponse.AuthenticationError == Mercury.Server.Enterprise.AuthenticationError.NoError)

                    && (!String.IsNullOrWhiteSpace (MercuryApplication.AuthenticationResponse.Token))) {

                    // VALID, SUCCESSFUL LOGIN

                    return RedirectToRoute (new { controller = "Workspace" }); // USE STANDARD REDIRECTION WHEN AUTHENTICATED, MERCURY APPLICATION IS CACHED FOR WORKSPACE TO USE


                    // THE BELOW WAS REMOVED, IT COULD BE USED TO SUPPORT STATELESS PASSING OF THE TOKEN BETWEEN THE LOGIN PAGE AND THE WORKSPACE PAGE

                    //String encryptedToken = MercuryApplication.AuthenticationResponse.Token;

                    //#if DEBUG 

                    //System.Diagnostics.Debug.WriteLine ("Authentication Token: " + MercuryApplication.AuthenticationResponse.Token);

                    //System.Diagnostics.Debug.WriteLine ("Authentication Token Encrypted: " + encryptedToken);

                    //#endif

                    // 

                    // loginModel = new Models.Login.Login (MercuryApplication.AuthenticationResponse, environmentName);

                }

                else {

                    // NOT A SUCCESSFUL LOGIN, CREATE MODEL AND PASS VIEW

                    loginModel = new Models.Login.Login (MercuryApplication.AuthenticationResponse, environmentName);

                }

            }

            else {

                // NOT USING WINDOWS AUTHENTICATION, REDIRECT TO PERMISSION DENIED

                // TODO: REDIRECT

            }

            return View(loginModel);

        }

        #endregion 

    }

}
