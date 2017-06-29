using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "InsuranceTypeCollectionResponse")]
    public class InsuranceTypeCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Insurer.InsuranceType> collection = new List<Server.Core.Insurer.InsuranceType> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Insurer.InsuranceType> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}