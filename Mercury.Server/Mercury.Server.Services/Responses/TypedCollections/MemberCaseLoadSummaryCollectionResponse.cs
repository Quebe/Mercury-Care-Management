using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "MemberCaseLoadSummaryCollectionResponse")]
    public class MemberCaseLoadSummaryCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Individual.Case.Views.MemberCaseLoadSummary> collection = new List<Server.Core.Individual.Case.Views.MemberCaseLoadSummary> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Individual.Case.Views.MemberCaseLoadSummary> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}