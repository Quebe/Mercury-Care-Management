using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "PopulationMembershipEntryDataViewCollectionResponse")]
    public class PopulationMembershipEntryDataViewCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Population.DataViews.PopulationMembershipEntry> collection = new List<Mercury.Server.Core.Population.DataViews.PopulationMembershipEntry> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Population.DataViews.PopulationMembershipEntry> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}
