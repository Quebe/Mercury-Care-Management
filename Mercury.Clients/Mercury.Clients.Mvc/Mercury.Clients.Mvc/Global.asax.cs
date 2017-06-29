using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mercury.Clients.Mvc {

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication {

        public static void RegisterGlobalFilters (GlobalFilterCollection filters) {

            filters.Add (new HandleErrorAttribute ());

            return;

        }

        public static void RegisterRoutes (RouteCollection routes) {

            routes.IgnoreRoute ("{resource}.axd/{*pathInfo}");


            routes.MapRoute ("FormDesigner", "Forms/FormDesigner/{formId}",

                new { controller = "Forms", action = "FormDesigner", formId = UrlParameter.Optional });

            routes.MapRoute ("Workspace", "Workspace/{action}/{id}",

                new { controller = "Workspace", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute ("Application", "Application/MemberSummaryInformation/{memberId}",

                new { controller = "Application", action = "MemberSummaryInformation", memberId = String.Empty });

            routes.MapRoute ("Default", "{controller}/{action}/{environmentName}",

                new { controller = "Login", action = "Index", environmentName = UrlParameter.Optional });

            return;

        }

        protected void Application_Start () {

            AreaRegistration.RegisterAllAreas ();



            RegisterGlobalFilters (GlobalFilters.Filters);

            RegisterRoutes (RouteTable.Routes);

            return;

        }

    }

}