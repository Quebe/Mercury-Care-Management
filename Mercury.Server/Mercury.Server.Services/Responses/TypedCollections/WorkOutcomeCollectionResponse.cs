using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "WorkOutcomeCollectionResponse")]
    public class WorkOutcomeCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Work.WorkOutcome> collection = new List<Server.Core.Work.WorkOutcome> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Work.WorkOutcome> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}