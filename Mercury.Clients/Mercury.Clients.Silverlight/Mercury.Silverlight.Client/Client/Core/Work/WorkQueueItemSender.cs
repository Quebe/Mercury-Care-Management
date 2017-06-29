using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Mercury.Client.Core.Work {

    public class WorkQueueItemSender : CoreObject {

        #region Private Properties

        private Int64 workQueueItemSenderId;

        private Int64 workQueueItemId;


        private String senderObjectType;

        private Int64 senderObjectId;

        private String eventObjectType;

        private Int64 eventObjectId;

        private String eventDescription;

        #endregion


        #region Public Properties

        public Int64 WorkQueueItemSenderId { get { return workQueueItemSenderId; } set { workQueueItemSenderId = value; } }

        public Int64 WorkQueueItemId { get { return workQueueItemId; } set { workQueueItemId = value; } }


        public String SenderObjectType { get { return senderObjectType; } set { senderObjectType = value; } }

        public Int64 SenderObjectId { get { return senderObjectId; } set { senderObjectId = value; } }

        public String EventObjectType { get { return eventObjectType; } set { eventObjectType = value; } }

        public Int64 EventObjectId { get { return eventObjectId; } set { eventObjectId = value; } }

        public String EventDescription { get { return eventDescription; } set { eventDescription = value; } }

        #endregion


        #region Constructors

        public WorkQueueItemSender (Application applicationReference) { base.BaseConstructor (applicationReference); return; }

        public WorkQueueItemSender (Application applicationReference, Server.Application.WorkQueueItemSender serverWorkQueueItemSender) {

            base.BaseConstructor (application);

            workQueueItemId = serverWorkQueueItemSender.WorkQueueItemId;


            senderObjectType = serverWorkQueueItemSender.SenderObjectType;

            senderObjectId = serverWorkQueueItemSender.SenderObjectId;

            eventObjectType = serverWorkQueueItemSender.EventObjectType;

            eventObjectId = serverWorkQueueItemSender.EventObjectId;

            eventDescription = serverWorkQueueItemSender.EventDescription;

            return;

        }

        #endregion

    }

}
