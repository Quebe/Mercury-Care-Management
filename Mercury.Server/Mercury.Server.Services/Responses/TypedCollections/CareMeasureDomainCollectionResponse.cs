using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "CareMeasureDomainCollectionResponse")]
    public class CareMeasureDomainCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Individual.CareMeasureDomain> collection = new List<Server.Core.Individual.CareMeasureDomain> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Individual.CareMeasureDomain> Collection { get { return collection; } set { collection = value; } }

        #endregion

    }

}