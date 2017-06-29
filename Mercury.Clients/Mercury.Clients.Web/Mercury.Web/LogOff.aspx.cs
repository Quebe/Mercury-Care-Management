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

    public partial class LogOff : System.Web.UI.Page {

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application) Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication != null) {

                MercuryApplication.LogOff ();

            }

            Session.Clear ();

            Session.Abandon ();

            return;

        }

    }

}
