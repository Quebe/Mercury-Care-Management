using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "CorrespondenceCollectionResponse")]
    public class CorrespondenceCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Reference.Correspondence> collection = new List<Server.Core.Reference.Correspondence> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Reference.Correspondence> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}