using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "LabResultCollectionResponse")]
    public class LabResultCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Claims.LabResult> collection = new List<Server.Core.Claims.LabResult> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Claims.LabResult> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}
