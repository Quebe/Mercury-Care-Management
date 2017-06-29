using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercury.Clients.Mvc.Controllers
{
    public class PermissionDeniedController : Controller
    {
        //
        // GET: /PermissionDenied/

        public ActionResult Index()
        {
            return View();
        }

    }
}
