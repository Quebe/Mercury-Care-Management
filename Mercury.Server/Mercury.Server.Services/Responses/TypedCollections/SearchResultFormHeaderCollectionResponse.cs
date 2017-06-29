using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "SearchResultFormHeaderCollectionResponse")]
    public class SearchResultFormHeaderCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Search.SearchResultFormHeader> collection = new List<Server.Core.Search.SearchResultFormHeader> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Search.SearchResultFormHeader> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}