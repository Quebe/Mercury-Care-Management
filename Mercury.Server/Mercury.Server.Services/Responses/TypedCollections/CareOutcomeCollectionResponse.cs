using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "CareOutcomeCollectionResponse")]
    public class CareOutcomeCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Individual.CareOutcome> collection = new List<Server.Core.Individual.CareOutcome> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Individual.CareOutcome> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}