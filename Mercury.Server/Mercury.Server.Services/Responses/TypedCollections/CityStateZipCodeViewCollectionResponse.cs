using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "CityStateZipCodeViewCollectionResponse")]
    public class CityStateZipCodeViewCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Reference.Views.CityStateZipCodeView> collection;

        #endregion


        #region Public Properties

        public List<Server.Core.Reference.Views.CityStateZipCodeView> Collection { get { return collection; } set { collection = value; } } // Property: Collection

        #endregion


    }

}