using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "MemberAuthorizedServiceDetailCollectionResponse")]
    public class MemberAuthorizedServiceDetailCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.AuthorizedServices.MemberAuthorizedServiceDetail> collection = new List<Server.Core.AuthorizedServices.MemberAuthorizedServiceDetail> ();

        #endregion


        #region Public Properties

        public List<Server.Core.AuthorizedServices.MemberAuthorizedServiceDetail> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}
