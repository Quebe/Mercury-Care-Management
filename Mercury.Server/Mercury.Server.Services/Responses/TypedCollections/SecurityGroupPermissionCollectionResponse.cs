using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract]
    public class SecurityGroupPermissionCollectionResponse : ResponseBase {
        
        [DataMember (Name = "Collection")]
        private List <Mercury.Server.Security.SecurityGroupPermission> collection;

        public List<Mercury.Server.Security.SecurityGroupPermission> Collection {
            get { return collection; }
            set { collection = value; }

        } // Property: ResultList

    }

}
