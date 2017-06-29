using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "AuthorizedServiceCollectionResponse")]
    public class AuthorizedServiceCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.AuthorizedServices.AuthorizedService> collection = new List<Server.Core.AuthorizedServices.AuthorizedService> ();

        #endregion


        #region Public Properties

        public List<Server.Core.AuthorizedServices.AuthorizedService> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}
