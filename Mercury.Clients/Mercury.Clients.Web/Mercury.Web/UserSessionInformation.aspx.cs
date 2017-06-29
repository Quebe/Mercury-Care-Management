using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web {

    public partial class UserSessionInformation : System.Web.UI.Page {

        #region Private Session Cache

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application) Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        #endregion 

        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            RenderInformation ();

            return;

        }

        #endregion 
        

        #region Methods

        protected void RenderInformation () {
            
            Client.Session session = MercuryApplication.Session;

            Page.Title = session.UserDisplayName + " [" + session.UserAccountId + " : " + session.UserAccountName + "]";


            DebugWriteLine ("Security Authority: " + session.SecurityAuthorityName + " (" + session.SecurityAuthorityId.ToString () + ")");

            DebugWriteLine ("User: " + session.UserDisplayName + " [" + session.UserAccountId + " : " + session.UserAccountName + "]");

            DebugWriteLine ("Environment Name: " + session.EnvironmentName);

            DebugWriteLine (String.Empty);

            DebugWriteLine ("Client Version: " + MercuryApplication.VersionClient);

            DebugWriteLine ("Server Version: " + MercuryApplication.VersionServer);

            DebugWriteLine (String.Empty);

            DebugWriteLine ("Enterprise Permissions: ");

            foreach (String currentPermission in session.EnterprisePermissionSet.Keys) {

                DebugWriteLine (currentPermission);

            }

            DebugWriteLine (String.Empty);

            DebugWriteLine ("Environment Permissions: ");

            foreach (String currentPermission in session.EnvironmentPermissionSet.Keys) {

                DebugWriteLine (currentPermission);

            }

            DebugWriteLine (String.Empty);

            DebugWriteLine ("Work Teams: ");

             

            foreach (Client.Core.Work.WorkTeam currentWorkTeam in MercuryApplication.WorkTeamsForSession (false)) {

                DebugWriteLine (currentWorkTeam.Name);

            }


            DebugWriteLine (String.Empty);

            DebugWriteLine ("Work Queues: ");

            foreach (Int64 currentWorkQueueId in MercuryApplication.Session.WorkQueuePermissions.Keys) {

                Client.Core.Work.WorkQueue currentWorkQueue = MercuryApplication.WorkQueueGet (currentWorkQueueId, false);

                DebugWriteLine (currentWorkQueue.Name + " (" + MercuryApplication.Session.WorkQueuePermissions[currentWorkQueueId].ToString () + ")");

            }




            DebugWriteLine (String.Empty);

            DebugWriteLine ("Role Membership: ");

            foreach (String currentMembership in session.RoleMembership) {

                DebugWriteLine (currentMembership);

            }

            DebugWriteLine (String.Empty);

            DebugWriteLine ("Security Group Membership: ");

            foreach (String currentMembership in session.GroupMembership) {

                DebugWriteLine (currentMembership);

            }

            DebugWriteLine (String.Empty);

            if (MercuryApplication.LastException != null) {

                Exception lastException = MercuryApplication.LastException;

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
        

            return;

        }

        protected void DebugWriteLine (String debugLine) {

            DebugInformation.Text = DebugInformation.Text + debugLine + "<br />";

        }

        #endregion 

    }

}
