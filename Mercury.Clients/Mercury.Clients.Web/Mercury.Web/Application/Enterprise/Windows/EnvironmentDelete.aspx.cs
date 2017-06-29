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

namespace Mercury.Web.Application.Enterprise.Windows {
    public partial class EnvironmentDelete : System.Web.UI.Page {

        const String SessionCachePrefix = "EnterpriseManagement.EnvironmentDelete.";

        const String ManagePermission = "Enterprise.EnterpriseManagement.Environment.Manage";


        protected Mercury.Client.Application application;

        Mercury.Server.Application.Environment environment;


        protected void Page_Load (object sender, EventArgs e) {

            Int64 environmentId;

            String environmentName;

            if (Session["Mercury.Application"] == null) {

                Response.RedirectLocation = "/SessionExpired.aspx";

                return;

            }


            application = (Mercury.Client.Application) Session["Mercury.Application"];

            if (!application.HasEnterprisePermission (ManagePermission)) {

                if (!IsPostBack) {

                    Server.Transfer ("/PermissionDenied.aspx");

                }

                else {

                    Response.RedirectLocation = "/PermissionDenied.aspx";

                }

                return;

            }

            ButtonOk.Click += new EventHandler (this.ButtonOk_OnClick);

            ButtonCancel.Click += new EventHandler (this.ButtonCancel_OnClick);

            if ((application != null) && (!Page.IsPostBack)) {

                environmentName = Request.QueryString["EnvironmentName"];

                environmentId = 0;

                Session.Add (SessionCachePrefix + "EnvironmentName", environmentName);


                System.Collections.Generic.Dictionary<Int64, String> environmentDictionary = application.EnvironmentDictionary (false);

                foreach (Int64 currentKey in environmentDictionary.Keys) {

                    if (environmentDictionary[currentKey] == environmentName) {

                        environmentId = currentKey;

                        Session.Add (SessionCachePrefix + "EnvironmentId", currentKey);

                        break;

                    }

                }

                if (environmentId != 0) {

                    environment = application.EnvironmentGet (environmentId);


                }

                else {

                    Server.Transfer ("/PermissionDenied.aspx");

                }

                Session[SessionCachePrefix + "Environment"] = environment;

                UiEnvironmentName.Text = environment.Name;

                Page.Title = "Delete Environment: " + environment.Name;

            } // Initial Page Load

            else { // Postback

                environment = (Mercury.Server.Application.Environment) Session[SessionCachePrefix + "Environment"];

            }

        } // Page_Load


        protected void ButtonOk_OnClick (Object sender, EventArgs eventArgs) {

            Boolean success = false;

            if (application.HasEnterprisePermission (ManagePermission)) {

                success = application.EnvironmentDelete (environment.Name);

            }


            if (success) {

                Server.Transfer ("/WindowCloseDialog.aspx");

            }

            else {

                UiExceptionMessage.Text = application.LastException.Message;

                UiExceptionMessage.Visible = true;

            }

        }

        protected void ButtonCancel_OnClick (Object sender, EventArgs eventArgs) {

            Server.Transfer ("/WindowCloseDialog.aspx");

        }


    }

}
