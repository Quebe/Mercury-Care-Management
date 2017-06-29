using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "WorkQueueSummaryCollectionResponse")]
    public class WorkQueueSummaryCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Work.DataViews.WorkQueueSummary> collection = new List<Server.Core.Work.DataViews.WorkQueueSummary> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Work.DataViews.WorkQueueSummary> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}