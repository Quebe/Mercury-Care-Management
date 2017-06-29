using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "CareMeasureClassCollectionResponse")]
    public class CareMeasureClassCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Individual.CareMeasureClass> collection = new List<Server.Core.Individual.CareMeasureClass> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Individual.CareMeasureClass> Collection { get { return collection; } set { collection = value; } }

        #endregion

    }

}