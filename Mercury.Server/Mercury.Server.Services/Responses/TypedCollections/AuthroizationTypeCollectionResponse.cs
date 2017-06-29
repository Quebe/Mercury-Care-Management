using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "AuthorizationTypeCollectionResponse")]
    public class AuthorizationTypeCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]

        private List<Server.Core.Authorizations.AuthorizationType> collection;
        
        #endregion


        #region Public Properties

        public List<Server.Core.Authorizations.AuthorizationType> Collection { get { return collection; } set { collection = value; } } // Property: ResultList
        
        #endregion

    }

}
