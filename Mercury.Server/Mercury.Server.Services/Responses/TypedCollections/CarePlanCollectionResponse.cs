using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "CarePlanCollectionResponse")]
    public class CarePlanCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]

        private List<Server.Core.Individual.CarePlan> collection;

        #endregion


        #region Public Properties

        public List<Server.Core.Individual.CarePlan> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}
