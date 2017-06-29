using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace Mercury.Web {

    public partial class PermissionDenied : System.Web.UI.Page {

        protected Mercury.Client.Application application;


        protected void Page_Load (object sender, EventArgs e) {

            application = (Mercury.Client.Application) Session["Mercury.Application"];


            if (application != null) {

                DebugWriteLine ("Security Authority: " + application.Session.SecurityAuthorityName);

                DebugWriteLine ("User: " + application.Session.UserDisplayName + " [" + application.Session.UserAccountId + " : " + application.Session.UserAccountName + "]");

                DebugWriteLine ("Environment Name: " + application.Session.EnvironmentName);

                DebugWriteLine (String.Empty);

                DebugWriteLine ("Enterprise Permissions: ");

                foreach (String currentPermission in application.Session.EnterprisePermissionSet.Keys) {

                    DebugWriteLine (currentPermission);

                }

                DebugWriteLine (String.Empty);

                DebugWriteLine ("Environment Permissions: ");

                foreach (String currentPermission in application.Session.EnvironmentPermissionSet.Keys) {

                    DebugWriteLine (currentPermission);

                }

                DebugWriteLine (String.Empty);

                if (application.LastException != null) {

                    Exception lastException = application.LastException;

                    if (lastException != null) {

                        DebugWriteLine ("Client.Application [" + lastException.Source + "] " + lastException.Message);

                        if (lastException.InnerException != null) {

                            DebugWriteLine ("Client.Application [" + lastException.InnerException.Source + "] " + lastException.InnerException.Message);

                        }

                        DebugWriteLine ("** Stack Trace **");

                        System.Diagnostics.StackTrace debugStack = new System.Diagnostics.StackTrace ();

                        foreach (System.Diagnostics.StackFrame currentStackFrame in debugStack.GetFrames ()) {

                            DebugWriteLine ("    [" + currentStackFrame.GetMethod ().Module.Assembly.FullName + "] " + currentStackFrame.GetMethod ().Name);

                        }

                    } // if (lastException != null) 

                }

            }
                

        }


        protected void DebugWriteLine (String debugLine) {

            DebugInformation.Text = DebugInformation.Text + debugLine + "<br />";

        }

    }

}
