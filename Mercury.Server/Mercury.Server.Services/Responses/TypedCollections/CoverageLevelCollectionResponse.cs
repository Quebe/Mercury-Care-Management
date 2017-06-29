using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "CoverageLevelCollectionResponse")]
    public class CoverageLevelCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Insurer.CoverageLevel> collection = new List<Server.Core.Insurer.CoverageLevel> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Insurer.CoverageLevel> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}