using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "SearchResultMedicalServiceHeaderCollectionResponse")]
    public class SearchResultMedicalServiceHeaderCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Search.SearchResultMedicalServiceHeader> collection = new List<Server.Core.Search.SearchResultMedicalServiceHeader> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Search.SearchResultMedicalServiceHeader> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}