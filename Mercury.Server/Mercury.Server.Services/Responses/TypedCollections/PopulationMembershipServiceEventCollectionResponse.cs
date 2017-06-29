using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "PopulationMembershipServiceEventCollectionResponse")]
    public class PopulationMembershipServiceEventCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Population.PopulationEvents.PopulationMembershipServiceEvent> collection = new List<Mercury.Server.Core.Population.PopulationEvents.PopulationMembershipServiceEvent> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Population.PopulationEvents.PopulationMembershipServiceEvent> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}
