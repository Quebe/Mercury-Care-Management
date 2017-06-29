using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.SearchResults {

    [DataContract (Name = "SearchResultsMemberResponse")]
    public class SearchResultsMemberResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Results")]
        private List<Mercury.Server.Core.Search.SearchResultMember> results = new List<Mercury.Server.Core.Search.SearchResultMember> ();
        #endregion


        #region Public Properties

        public List<Mercury.Server.Core.Search.SearchResultMember> Results { get { return results; } set { results = value; } }

        #endregion

    }

}
