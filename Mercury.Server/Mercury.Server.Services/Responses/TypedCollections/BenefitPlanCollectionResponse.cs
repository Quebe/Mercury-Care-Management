using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "BenefitPlanCollectionResponse")]
    public class BenefitPlanCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Insurer.BenefitPlan> collection = new List<Server.Core.Insurer.BenefitPlan> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Insurer.BenefitPlan> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}