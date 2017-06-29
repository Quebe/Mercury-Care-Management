using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "ContactRegardingCollectionResponse")]
    public class ContactRegardingCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Reference.ContactRegarding> collection = new List<Server.Core.Reference.ContactRegarding> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Reference.ContactRegarding> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}