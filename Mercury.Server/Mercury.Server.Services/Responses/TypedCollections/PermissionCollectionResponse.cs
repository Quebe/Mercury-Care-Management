using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract]
    public class PermissionCollectionResponse : ResponseBase {

        [DataMember (Name = "Collection")]
        private List<Mercury.Server.Security.Permission> collection;

        public List<Mercury.Server.Security.Permission> Collection { get { return collection; } set { collection = value; } } // Property: ResultList


    }

}
