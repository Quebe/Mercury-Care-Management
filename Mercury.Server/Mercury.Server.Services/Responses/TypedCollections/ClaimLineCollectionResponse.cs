using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "ClaimLineCollectionResponse")]
    public class ClaimLineCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Claims.ClaimLine> collection = new List<Server.Core.Claims.ClaimLine> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Claims.ClaimLine> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}
