using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "CoverageTypeCollectionResponse")]
    public class CoverageTypeCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Insurer.CoverageType> collection = new List<Server.Core.Insurer.CoverageType> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Insurer.CoverageType> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}