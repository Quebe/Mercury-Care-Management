using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercury.Clients.Mvc.Views.Helpers {

    public static class ApplicationMasterHelper {

        public static MvcHtmlString Scripts (this UrlHelper helper) {

             

            System.Text.StringBuilder helperResult = new System.Text.StringBuilder ();

            

            #if DEBUG

            helperResult.AppendFormat ("<script src=\"{0}\" type=\"text/javascript\"></script>", helper.Content ("~/Scripts/jquery-1.5.1.debug.js"));

            helperResult.AppendFormat ("<script src=\"{0}\" type=\"text/javascript\"></script>", helper.Content ("~/Scripts/Application/ApplicationMaster.debug.js"));

            #else

            helperResult.AppendFormat ("<script src=\"{0}\" type=\"text/javascript\"></script>", helper.Content ("~/Scripts/jquery-1.5.1.min.js"));

            helperResult.AppendFormat ("<script src=\"{0}\" type=\"text/javascript\"></script>", helper.Content ("~/Scripts/Application/ApplicationMaster.min.js"));
            
            #endif




            return MvcHtmlString.Create (helperResult.ToString ());

        }

    }

}