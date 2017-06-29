using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercury.Clients.Mvc.Models {

    public class ApplicationGridModel : ApplicationModel {

        public Int32 CurrentPage { get; set; }

        public Int32 PageSize { get; set; }

        public Int32 ItemCount { get; set; }

    }

}