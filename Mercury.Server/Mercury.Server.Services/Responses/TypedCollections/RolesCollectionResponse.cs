using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract]
    public class RoleCollectionResponse : ResponseBase {

        [DataMember (Name = "Collection")]
        private List<Mercury.Server.Environment.Role> collection;

        public List<Mercury.Server.Environment.Role> Collection { get { return collection; } set { collection = value; } } // Property: Collection

    }

}
