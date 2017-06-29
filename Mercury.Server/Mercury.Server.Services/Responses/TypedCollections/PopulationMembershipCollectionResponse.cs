using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "PopulationMembershipCollectionResponse")]
    public class PopulationMembershipCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Population.PopulationMembership> collection = new List<Mercury.Server.Core.Population.PopulationMembership> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Population.PopulationMembership> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}
