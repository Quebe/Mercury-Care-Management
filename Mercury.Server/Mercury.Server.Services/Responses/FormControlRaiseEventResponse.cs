using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses {

    public class FormControlRaiseEventResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Form")]
        private Server.Core.Forms.Form form;

        #endregion


        #region Public Properties

        public Server.Core.Forms.Form Form { get { return form; } set { form = value; } }

        #endregion

    }

}