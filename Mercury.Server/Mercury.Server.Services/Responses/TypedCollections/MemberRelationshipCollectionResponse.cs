using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "MemberRelationshipCollectionResponse")]
    public class MemberRelationshipCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Member.MemberRelationship> collection = new List<Server.Core.Member.MemberRelationship> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Member.MemberRelationship> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}
