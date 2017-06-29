using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.Objects {

    [DataContract (Name = "WorkQueueItemResponse")]
    public class WorkQueueItemResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "WorkQueueItem")]
        private Server.Core.Work.WorkQueueItem workQueueItem = null;

        #endregion


        #region Public Properties

        public Server.Core.Work.WorkQueueItem WorkQueueItem { get { return workQueueItem; } set { workQueueItem = value; } }

        #endregion

    }

}