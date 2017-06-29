using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "PopulationMembershipTriggerEventDataViewCollectionResponse")]
    public class PopulationMembershipTriggerEventDataViewCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Population.DataViews.PopulationMembershipTriggerEvent> collection = new List<Mercury.Server.Core.Population.DataViews.PopulationMembershipTriggerEvent> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Population.DataViews.PopulationMembershipTriggerEvent> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}
