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
    public partial class SecurityAuthorityDelete : System.Web.UI.Page {

        const String SessionCachePrefix = "EnterpriseManagement.SecurityAuthorityDelete.";

        const String ManagePermission = "Enterprise.EnterpriseManagement.SecurityAuthority.Manage";


        protected Mercury.Client.Application application;

        Mercury.Server.Application.SecurityAuthority securityAuthority;


        protected void Page_Load (object sender, EventArgs e) {

            String securityAuthorityName;

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

                securityAuthorityName = Request.QueryString["SecurityAuthorityName"];

                Session.Add (SessionCachePrefix + "SecurityAuthorityName", securityAuthorityName);


                securityAuthority = application.SecurityAuthorityGet (securityAuthorityName, false);

                if ((securityAuthority == null) || (securityAuthority.Id == 0)) {

                    Server.Transfer ("/PermissionDenied.aspx");

                }

                Session[SessionCachePrefix + "SecurityAuthority"] = securityAuthority;

                UiSecurityAuthorityName.Text = securityAuthority.Name;

                Page.Title = "Delete Security Authority: " + securityAuthority.Name;

            } // Initial Page Load

            else { // Postback

                securityAuthority = (Mercury.Server.Application.SecurityAuthority) Session[SessionCachePrefix + "SecurityAuthority"];

            }

        } // Page_Load


        protected void ButtonOk_OnClick (Object sender, EventArgs eventArgs) {

            Boolean success = false;

            if (application.HasEnterprisePermission (ManagePermission)) {

                success = application.SecurityAuthorityDelete (securityAuthority.Name);

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
