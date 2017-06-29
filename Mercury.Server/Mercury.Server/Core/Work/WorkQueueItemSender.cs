using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Work {
    
    [DataContract (Name = "WorkQueueItemSender")]
    public class WorkQueueItemSender : CoreObject {

        #region Private Properties

        [DataMember (Name = "WorkQueueItemId")]
        private Int64 workQueueItemId;

        [DataMember (Name = "SenderObjectType")]
        private String senderObjectType;

        [DataMember (Name = "SenderObjectId")]
        private Int64 senderObjectId;

        [DataMember (Name = "EventObjectType")]
        private String eventObjectType;

        [DataMember (Name = "EventObjectId")]
        private Int64 eventObjectId;

        [DataMember (Name = "EventInstanceId")]
        private Int64 eventInstanceId;

        [DataMember (Name = "EventDescription")]
        private String eventDescription;

        [DataMember (Name = "Priority")]
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

        public WorkQueueItemSender (Application applicationReference, Int64 forWorkQueueItemSenderId) {

            BaseConstructor (applicationReference, forWorkQueueItemSenderId);

            return;

        }

        #endregion


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            workQueueItemId = (Int64) currentRow["WorkQueueItemId"];

            senderObjectType = (String) currentRow["SenderObjectType"];

            senderObjectId = (Int64) currentRow["SenderObjectId"];

            eventObjectType = (String) currentRow["EventObjectType"];

            eventObjectId = (Int64) currentRow["EventObjectId"];

            eventInstanceId = (Int64) currentRow["EventInstanceId"];

            eventDescription = (String) currentRow["EventDescription"];

            priority = (Int32) currentRow["Priority"];

            return;

        }

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            if (workQueueItemId == 0) { return false; }


            modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);

            try {

                application.EnvironmentDatabase.BeginTransaction ();

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dbo.WorkQueueItemSender_InsertUpdate ");

                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append (workQueueItemId.ToString () + ", ");


                sqlStatement.Append ("'" + senderObjectType + "', ");

                sqlStatement.Append (senderObjectId.ToString () + ", ");

                sqlStatement.Append ("'" + eventObjectType + "', ");

                sqlStatement.Append (eventObjectId.ToString () + ", ");

                sqlStatement.Append (eventInstanceId.ToString () + ", ");

                sqlStatement.Append ("'" + eventDescription.Replace ("'", "''") + "', ");

                sqlStatement.Append (priority.ToString () + ", ");


                sqlStatement.Append (ModifiedAccountInfo.AccountInfoSql);


                success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }


                SetIdentity ();

                application.EnvironmentDatabase.CommitTransaction ();

            }

            catch (Exception applicationException) {

                application.EnvironmentDatabase.RollbackTransaction ();

                success = false;

                application.SetLastException (applicationException);

            }

            return success;

        }

        #endregion 


        #region Public Methods

        /// <summary>
        /// Make a copy of an existing Work Queue Item Sender. Will reset Unique Id and Work Queue Item Id to 0.
        /// </summary>
        /// <returns>A copy of an existing Work Queue Item Sender.</returns>
        public WorkQueueItemSender Copy () {

            WorkQueueItemSender copied = new WorkQueueItemSender (application);

            copied.WorkQueueItemId = 0;


            copied.SenderObjectType = senderObjectType;

            copied.SenderObjectId = senderObjectId;

            copied.EventObjectType = eventObjectType;

            copied.EventObjectId = eventObjectId;

            copied.eventInstanceId = eventInstanceId;

            copied.EventDescription = eventDescription;

            copied.Priority = priority;


            return copied;

        }

        /// <summary>
        /// Make a copy of an existing Work Queue Item Sender directly to and Existing Work Queue Item.
        /// This is a database copy that makes an exact duplicate except for the Work Queue Item Id.
        /// </summary>
        /// <returns>Returns true/false based on success.</returns>
        public Boolean CopyToWorkQueueItem (Int64 destinationWorkQueueItemId) {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            if (destinationWorkQueueItemId == 0) { return false; }


            ModifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);

            try {

                application.EnvironmentDatabase.BeginTransaction ();

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dbo.WorkQueueItemSender_CopyToWorkQueueItem ");

                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append (destinationWorkQueueItemId.ToString () + ", ");

                sqlStatement.Append (ModifiedAccountInfo.AccountInfoSql);


                success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }

                application.EnvironmentDatabase.CommitTransaction ();

            }

            catch (Exception applicationException) {

                application.EnvironmentDatabase.RollbackTransaction ();

                success = false;

                application.SetLastException (applicationException);

            }
            
            return success;

        }

        #endregion 

    }

}
