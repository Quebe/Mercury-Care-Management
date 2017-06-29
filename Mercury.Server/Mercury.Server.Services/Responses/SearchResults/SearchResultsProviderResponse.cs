using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.SearchResults {

    [DataContract (Name = "SearchResultsProviderResponse")]
    public class SearchResultsProviderResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Results")]
        private List<Mercury.Server.Core.Search.SearchResultProvider> results;

        #endregion


        #region Public Properties

        public List<Mercury.Server.Core.Search.SearchResultProvider> Results { get { return results; } set { results = value; } }

        #endregion

    }

}
