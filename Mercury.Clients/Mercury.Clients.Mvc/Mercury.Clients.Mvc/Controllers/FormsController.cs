using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercury.Clients.Mvc.Controllers
{
    public class FormsController : Controller
    {

        public ActionResult Index() {

            return View("PermissionDenied");

        }

        public ActionResult FormDesigner (Int64? formId) {

            Models.Forms.FormsModel formsModel = new Models.Forms.FormsModel ();

            formsModel.FormId = ((formId.HasValue) ? formId.Value : 1000000000);

            return View ("/Views/Forms/FormDesigner.cshtml", formsModel);

        }

    }

}
