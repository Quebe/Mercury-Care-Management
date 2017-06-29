using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "PharmacyClaimCollectionResponse")]
    public class PharmacyClaimCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Claims.PharmacyClaim> collection = new List<Server.Core.Claims.PharmacyClaim> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Claims.PharmacyClaim> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}
