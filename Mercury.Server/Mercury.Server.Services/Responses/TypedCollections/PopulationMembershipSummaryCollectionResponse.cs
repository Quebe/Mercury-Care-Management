using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "PopulationMembershipSummaryCollectionResponse")]
    public class PopulationMembershipSummaryCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Population.DataViews.PopulationMembershipSummary> collection = new List<Mercury.Server.Core.Population.DataViews.PopulationMembershipSummary> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Population.DataViews.PopulationMembershipSummary> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}
