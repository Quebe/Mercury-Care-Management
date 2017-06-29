using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "WorkQueueItemAssignmentHistoryCollectionResponse")]
    public class WorkQueueItemAssignmentHistoryCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Work.WorkQueueItemAssignmentHistory> collection = new List<Server.Core.Work.WorkQueueItemAssignmentHistory> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Work.WorkQueueItemAssignmentHistory> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}