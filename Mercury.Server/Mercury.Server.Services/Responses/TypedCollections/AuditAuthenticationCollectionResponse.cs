using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "AuditAuthenticationCollectionResponse")]
    public class AuditAuthenticationCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Audit.Authentication> collection = new List<Mercury.Server.Audit.Authentication> ();

        #endregion


        #region Public Properties

        public List<Server.Audit.Authentication> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}
