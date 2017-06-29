using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "MemberCaseSummaryCollectionResponse")]
    public class MemberCaseSummaryCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Individual.Case.Views.MemberCaseSummary> collection = new List<Server.Core.Individual.Case.Views.MemberCaseSummary> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Individual.Case.Views.MemberCaseSummary> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}