using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "EntityContactInformationCollectionResponse")]
    public class EntityContactInformationCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Entity.EntityContactInformation> collection = new List<Server.Core.Entity.EntityContactInformation> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Entity.EntityContactInformation> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}