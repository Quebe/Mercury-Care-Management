using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "PopulationMembershipEntryStatusDataViewCollectionResponse")]
    public class PopulationMembershipEntryStatusDataViewCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Population.DataViews.PopulationMembershipEntryStatus> collection = new List<Mercury.Server.Core.Population.DataViews.PopulationMembershipEntryStatus> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Population.DataViews.PopulationMembershipEntryStatus> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}
