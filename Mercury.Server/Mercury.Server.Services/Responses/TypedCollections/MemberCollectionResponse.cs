using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "MemberCollectionResponse")]
    public class MemberCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Member.Member> collection = new List<Server.Core.Member.Member> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Member.Member> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}