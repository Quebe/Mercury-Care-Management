using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "PopulationMembershipServiceEventDataViewCollectionResponse")]
    public class PopulationMembershipServiceEventDataViewCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Population.DataViews.PopulationMembershipServiceEvent> collection = new List<Mercury.Server.Core.Population.DataViews.PopulationMembershipServiceEvent> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Population.DataViews.PopulationMembershipServiceEvent> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}
