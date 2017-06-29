using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses {

    [DataContract (Name = "GetWorkResponse")]
    public class GetWorkResponse : ResponseBase  {

        #region Private Properties

        [DataMember (Name = "WorkQueueItem")]
        private Server.Core.Work.WorkQueueItem workQueueItem = null;

        [DataMember (Name = "WorkQueue")]
        private Server.Core.Work.WorkQueue workQueue = null;

        [DataMember (Name = "Workflow")]
        private Server.Core.Work.Workflow workflow = null;

        #endregion


        #region Public Properties

        public Server.Core.Work.WorkQueueItem WorkQueueItem { get { return workQueueItem; } set { workQueueItem = value; } }

        public Server.Core.Work.WorkQueue WorkQueue { get { return workQueue; } set { workQueue = value; } }

        public Server.Core.Work.Workflow Workflow { get { return workflow; } set { workflow = value; } }

        #endregion


    }

}