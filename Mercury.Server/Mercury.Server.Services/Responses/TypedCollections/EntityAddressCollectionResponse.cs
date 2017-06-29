using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "EntityAddressCollectionResponse")]
    public class EntityAddressCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Entity.EntityAddress> collection = new List<Server.Core.Entity.EntityAddress> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Entity.EntityAddress> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}