using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "AuthorizationLineCollectionResponse")]
    public class AuthorizationLineCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Authorizations.AuthorizationLine> collection = new List<Mercury.Server.Core.Authorizations.AuthorizationLine> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Authorizations.AuthorizationLine> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}
