using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Mercury.Web.Application.Forms.FormDesigner.Windows {

    public partial class FormPreview : System.Web.UI.Page {

        private DateTime pageStartTime = DateTime.Now;

        #region Private Session Cache

        private String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (PageInstanceId.Text)) { PageInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return PageInstanceId.Text + ".";

            }

        }

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application) Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        private Mercury.Client.Core.Forms.Form DesignerForm {

            get {

                Mercury.Client.Core.Forms.Form designerForm = (Mercury.Client.Core.Forms.Form) Session[SessionCachePrefix + "DesignerForm"];

                return designerForm;

            }

        }

        #endregion 

        
        #region Page Events

        protected void Page_PreInit (Object sender, EventArgs e) {

            pageStartTime = DateTime.Now;

            System.Diagnostics.Debug.WriteLine ("");

            System.Diagnostics.Debug.WriteLine ("---------------------");

            System.Diagnostics.Debug.WriteLine ("FORM PREVIEW (BEGIN)");

            return;

        }

        protected void Page_Load (object sender, EventArgs e) {

            Boolean success = true;

            if (MercuryApplication == null) { return; }

            Page.Title = "Form Preview";

            if (!IsPostBack) {

                if (!String.IsNullOrEmpty ((String) Request.QueryString["PageInstanceId"])) {

                    PageInstanceId.Text = (String) Request.QueryString["PageInstanceId"];

                    if (DesignerForm != null) {

                        PreviewFormEditor.SetForm (DesignerForm.Copy ());

                    }

                    else { success = false; }

                }

                else { success = false; }

            }

            if (!success) {

                Server.Transfer ("/PermissionDenied.aspx");

                return;

            }

            System.Diagnostics.Debug.WriteLine ("FORM PREVIEW (Page_Load): " + DateTime.Now.Subtract (pageStartTime).TotalMilliseconds.ToString ());

            return;

        }

        protected void Page_LoadComplete (Object sender, EventArgs e) {

            System.Diagnostics.Debug.WriteLine ("FORM PREVIEW (Page_LoadComplete->TOTAL): " + DateTime.Now.Subtract (pageStartTime).TotalMilliseconds.ToString ());

            return;

        }

        #endregion


    }

}
