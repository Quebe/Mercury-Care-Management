using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract]
    public class PopulationHeadersCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Mercury.Server.Core.Search.SearchResultPopulationHeader> collection;

        #endregion


        #region Public Properties

        public List<Mercury.Server.Core.Search.SearchResultPopulationHeader> Collection { get { return collection; } set { collection = value; } } // Property: Collection

        #endregion

    }

}
