using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercury.Clients.Mvc.Models.Forms {

    public class FormsModel : ApplicationModel {

        #region Private Properties

        #endregion 


        #region Public Properties

        public Int64 FormId { get; set; }

        public Client.Core.Forms.Form Form { get { return MercuryApplication.FormGet (FormId); } }

        #endregion 

    }

}