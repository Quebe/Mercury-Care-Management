using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "ClaimCollectionResponse")]
    public class ClaimCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Claims.Claim> collection = new List<Server.Core.Claims.Claim> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Claims.Claim> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}
