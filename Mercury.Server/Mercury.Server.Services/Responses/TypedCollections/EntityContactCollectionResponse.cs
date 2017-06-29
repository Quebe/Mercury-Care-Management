using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "EntityContactCollectionResponse")]
    public class EntityContactCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Entity.EntityContact> collection = new List<Server.Core.Entity.EntityContact> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Entity.EntityContact> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}
