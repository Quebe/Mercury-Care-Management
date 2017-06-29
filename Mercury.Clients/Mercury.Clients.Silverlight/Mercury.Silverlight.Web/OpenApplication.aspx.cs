using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Silverlight.Web {
    public partial class OpenApplication : System.Web.UI.Page {


        private Mercury.Client.Application MercuryApplication {

            get {

                Client.Application application = (Client.Application) Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

           
            String mercurySilverlightReference = String.Empty;

            mercurySilverlightReference = mercurySilverlightReference + "<object data=\"data:application/x-silverlight-2,\" type=\"application/x-silverlight-2\" width=\"100%\" height=\"100%\">";
            mercurySilverlightReference = mercurySilverlightReference + "<param name=\"source\" value=\"ClientBin/Mercury.Silverlight.xap?uid=" + (Guid.NewGuid ().ToString ()) + "\"/>";
            mercurySilverlightReference = mercurySilverlightReference + "<param name=\"onError\" value=\"onSilverlightError\" />";
            mercurySilverlightReference = mercurySilverlightReference + "<param name=\"windowless\" value=\"true\" />";
            mercurySilverlightReference = mercurySilverlightReference + "<param name=\"background\" value=\"white\" />";
            mercurySilverlightReference = mercurySilverlightReference + "<param name=\"minRuntimeVersion\" value=\"3.0.40624.0\" />";
            mercurySilverlightReference = mercurySilverlightReference + "<param name=\"autoUpgrade\" value=\"true\" />";
            mercurySilverlightReference = mercurySilverlightReference + "<param name=\"InitParams\" value=\"SessionToken=" + MercuryApplication.Session.Token + ",ServiceHostAddress=" + MercuryApplication.ServiceHostAddress + ",ServiceHostPort=" + MercuryApplication.ServiceHostPort + "\" />";
            mercurySilverlightReference = mercurySilverlightReference + "<a href=\"http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40624.0\" style=\"text-decoration:none\">";
            mercurySilverlightReference = mercurySilverlightReference + "<img src=\"http://go.microsoft.com/fwlink/?LinkId=108181\" alt=\"Get Microsoft Silverlight\" style=\"border-style:none\"/>";
            mercurySilverlightReference = mercurySilverlightReference + "</a>";
            mercurySilverlightReference = mercurySilverlightReference + "</object><iframe id=\"_sl_historyFrame\" style=\"visibility:hidden;height:0px;width:0px;border:0px\"></iframe>";

            SilverlightObject.Text = mercurySilverlightReference;

            return;

        }

    }

}
