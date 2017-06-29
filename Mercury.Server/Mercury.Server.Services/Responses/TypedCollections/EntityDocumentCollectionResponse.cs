using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "EntityDocumentCollectionResponse")]
    public class EntityDocumentCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Entity.Views.EntityDocument> collection = new List<Server.Core.Entity.Views.EntityDocument> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Entity.Views.EntityDocument> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}
