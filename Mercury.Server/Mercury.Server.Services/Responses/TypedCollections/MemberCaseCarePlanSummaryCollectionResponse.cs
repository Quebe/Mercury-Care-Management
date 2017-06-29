using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "MemberCaseCarePlanSummaryCollectionResponse")]
    public class MemberCaseCarePlanSummaryCollectionResponse : ResponseBase {


        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Individual.Case.Views.MemberCaseCarePlanSummary> collection = new List<Server.Core.Individual.Case.Views.MemberCaseCarePlanSummary> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Individual.Case.Views.MemberCaseCarePlanSummary> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion


    }

}