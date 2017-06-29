using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "WorkQueueItemCollectionResponse")]
    public class WorkQueueItemCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Work.WorkQueueItem> collection = new List<Server.Core.Work.WorkQueueItem> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Work.WorkQueueItem> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}