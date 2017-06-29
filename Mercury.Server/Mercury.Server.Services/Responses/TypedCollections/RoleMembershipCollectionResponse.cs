using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "RoleMembershipCollectionResponse")]
    public class RoleMembershipCollectionResponse : ResponseBase {

        [DataMember (Name = "Collection")]
        private List<Mercury.Server.Environment.RoleMembership> collection;

        public List<Mercury.Server.Environment.RoleMembership> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

    }

}
