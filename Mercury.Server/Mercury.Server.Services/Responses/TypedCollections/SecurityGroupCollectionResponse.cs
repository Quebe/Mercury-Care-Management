using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract]
    public class SecurityGroupCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Mercury.Server.Public.Interfaces.Security.SecurityGroup> collection;

        #endregion


        #region Public Properties

        public List<Mercury.Server.Public.Interfaces.Security.SecurityGroup> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}
