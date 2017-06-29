using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.SearchResults {

    [DataContract (Name = "SearchResultsGlobalResponse")]
    public class SearchResultsGlobalResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Search.SearchResultGlobal> collection = new List<Mercury.Server.Core.Search.SearchResultGlobal> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Search.SearchResultGlobal> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}
