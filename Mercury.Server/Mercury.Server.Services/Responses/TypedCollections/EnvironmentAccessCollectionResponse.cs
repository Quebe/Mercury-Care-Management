using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "EnvironmentAccessCollectionResponse")]
    public class EnvironmentAccessCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Security.EnvironmentAccess> collection = new List<Server.Security.EnvironmentAccess> ();

        #endregion


        #region Public Properties

        public List<Server.Security.EnvironmentAccess> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}