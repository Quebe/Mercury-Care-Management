using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract]
    public class SecurityAuthorityCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List <Mercury.Server.Security.SecurityAuthority> collection;

        #endregion


        #region Public Properties

        public List<Mercury.Server.Security.SecurityAuthority> Collection { get { return collection; } set { collection = value; }  } // Property: ResultList

        #endregion

    }

}
