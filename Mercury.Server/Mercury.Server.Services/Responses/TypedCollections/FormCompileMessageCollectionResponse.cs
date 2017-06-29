using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "FormCompileMessageCollectionResponse")]
    public class FormCompileMessageCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Forms.CompileMessage> collection = new List<Server.Core.Forms.CompileMessage> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Forms.CompileMessage> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}