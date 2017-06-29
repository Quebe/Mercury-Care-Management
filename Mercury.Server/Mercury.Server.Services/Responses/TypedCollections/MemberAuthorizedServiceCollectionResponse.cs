using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "MemberAuthorizedServiceCollectionResponse")]
    public class MemberAuthorizedServiceCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.AuthorizedServices.MemberAuthorizedService> collection = new List<Server.Core.AuthorizedServices.MemberAuthorizedService> ();

        #endregion


        #region Public Properties

        public List<Server.Core.AuthorizedServices.MemberAuthorizedService> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}
