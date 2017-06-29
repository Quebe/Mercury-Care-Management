using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "RolePermissionCollectionResponse")]
    public class RolePermissionCollectionResponse : ResponseBase {

        [DataMember (Name = "Collection")]
        private List<Mercury.Server.Environment.RolePermission> collection;

        public List<Mercury.Server.Environment.RolePermission> Collection { get { return collection; } set { collection = value; } } // Property: ResultList


    }

}
