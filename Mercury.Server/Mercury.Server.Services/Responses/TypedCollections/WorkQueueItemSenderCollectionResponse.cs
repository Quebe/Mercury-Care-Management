using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "WorkQueueItemSenderCollectionResponse")]
    public class WorkQueueItemSenderCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Work.WorkQueueItemSender> collection = new List<Server.Core.Work.WorkQueueItemSender> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Work.WorkQueueItemSender> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}