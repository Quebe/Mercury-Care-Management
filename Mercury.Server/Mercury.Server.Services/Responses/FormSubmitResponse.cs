using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses {

    [DataContract (Name = "FormSubmitResponse")]
    public class FormSubmitResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Form")]
        private Server.Core.Forms.Form form;

        [DataMember (Name = "Collection")]
        private List<Server.Core.Forms.CompileMessage> collection;

        #endregion


        #region Public Properties

        public Server.Core.Forms.Form Form { get { return form; } set { form = value; } }

        public List<Server.Core.Forms.CompileMessage> Collection { get { return collection; } set { collection = value; } } // Property: Collection

        #endregion

    }

}