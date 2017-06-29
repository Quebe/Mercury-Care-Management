using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Work {

    [Serializable]
    public class WorkQueueItemSender : CoreObject {

        #region Private Properties

        private Int64 workQueueItemId;

        private String senderObjectType;

        private Int64 senderObjectId;

        private String eventObjectType;

        private Int64 eventObjectId;

        private Int64 eventInstanceId;

        private String eventDescription;

        private Int32 priority = 0;

        #endregion


        #region Public Properties

        public Int64 WorkQueueItemId { get { return workQueueItemId; } set { workQueueItemId = value; } }

        public String SenderObjectType { get { return senderObjectType; } set { senderObjectType = value; } }

        public Int64 SenderObjectId { get { return senderObjectId; } set { senderObjectId = value; } }

        public String EventObjectType { get { return eventObjectType; } set { eventObjectType = value; } }

        public Int64 EventObjectId { get { return eventObjectId; } set { eventObjectId = value; } }

        public Int64 EventInstanceId { get { return eventInstanceId; } set { eventInstanceId = value; } }

        public String EventDescription { get { return eventDescription; } set { eventDescription = value; } }

        public Int32 Priority { get { return priority; } set { priority = value; } }

        #endregion 

        
        #region Constructors

        public WorkQueueItemSender (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public WorkQueueItemSender (Application applicationReference, Server.Application.WorkQueueItemSender serverWorkQueueItemSender) {

            BaseConstructor (applicationReference, serverWorkQueueItemSender);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.WorkQueueItemSender serverWorkQueueItemSender) {

            base.BaseConstructor (applicationReference, serverWorkQueueItemSender);

            workQueueItemId = serverWorkQueueItemSender.WorkQueueItemId;

            senderObjectType = serverWorkQueueItemSender.SenderObjectType;

            senderObjectId = serverWorkQueueItemSender.SenderObjectId;

            eventObjectType = serverWorkQueueItemSender.EventObjectType;

            eventObjectId = serverWorkQueueItemSender.EventObjectId;

            eventInstanceId = serverWorkQueueItemSender.EventInstanceId;

            eventDescription = serverWorkQueueItemSender.EventDescription;

            priority = serverWorkQueueItemSender.Priority;

            return;

        }

        #endregion

    }

}
