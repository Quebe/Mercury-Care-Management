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

    public partial class OpenApplication : System.Web.UI.Page {

        Mercury.Client.Application application;

        protected void Page_PreInit (object sender, EventArgs e) {

            if (!String.IsNullOrEmpty ((String) System.Web.Configuration.WebConfigurationManager.AppSettings["PageBrandingMaster"])) {

                Page.MasterPageFile = @"~\PageBranding\" + (String) System.Web.Configuration.WebConfigurationManager.AppSettings["PageBrandingMaster"];

            }

            return;

        }

        protected void Page_Load (object sender, EventArgs e) {

            if (Session["Mercury.Application"] == null) {

                uiPanelWindowConfidentiality.Visible = false;

                uiPanelWindowConfidentialityCode.Visible = false;

            }

            else {

                application = (Mercury.Client.Application) Session["Mercury.Application"];

                uiStatementLabel.Text = application.Session.ConfidentialityStatement;

            }

            System.Runtime.Serialization.Configuration.TypeElement forceDllUsage = new System.Runtime.Serialization.Configuration.TypeElement ();

            return;

        }

    }

}
