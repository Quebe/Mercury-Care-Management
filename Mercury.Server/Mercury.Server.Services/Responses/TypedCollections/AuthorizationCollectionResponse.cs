using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "AuthorizationCollectionResponse")]
    public class AuthorizationCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Authorizations.Authorization> collection = new List<Server.Core.Authorizations.Authorization> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Authorizations.Authorization> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}
