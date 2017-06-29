using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "CareMeasureCollectionResponse")]
    public class CareMeasureCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]

        private List<Server.Core.Individual.CareMeasure> collection;

        #endregion


        #region Public Properties

        public List<Server.Core.Individual.CareMeasure> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}
