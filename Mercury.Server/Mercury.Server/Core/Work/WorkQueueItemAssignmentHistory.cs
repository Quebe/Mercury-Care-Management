using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Work {

    [Serializable]
    [DataContract (Name = "WorkQueueItemAssignmentHistory")]
    public class WorkQueueItemAssignmentHistory : CoreObject {

        
        #region Private Properties

        [DataMember (Name = "WorkQueueItemId")]
        private Int64 workQueueItemId;


        [DataMember (Name = "AssignedFromWorkQueueId")]
        private Int64 assignedFromWorkQueueId = 0;

        [DataMember (Name = "AssignedToWorkQueueId")]
        private Int64 assignedToWorkQueueId = 0;


        [DataMember (Name = "AssignedToSecurityAuthorityId")]
        private Int64 assignedToSecurityAuthorityId;

        [DataMember (Name = "AssignedToUserAccountId")]
        private String assignedToUserAccountId;

        [DataMember (Name = "AssignedToUserAccountName")]
        private String assignedToUserAccountName;

        [DataMember (Name = "AssignedToUserDisplayName")]
        private String assignedToUserDisplayName;

        [DataMember (Name = "AssignedToDate")]
        private DateTime? assignedToDate;


        [DataMember (Name = "AssignmentSource")]
        private String assignmentSource = String.Empty;

        #endregion 


        #region Public Properties

        public Int64 WorkQueueItemId { get { return workQueueItemId; } set { workQueueItemId = value; } }


        public Int64 AssignedFromWorkQueueId { get { return assignedFromWorkQueueId; } set { assignedFromWorkQueueId = value; } }

        public Int64 AssignedToWorkQueueId { get { return assignedToWorkQueueId; } set { assignedToWorkQueueId = value; } }
        

        public Int64 AssignedToSecurityAuthorityId { get { return assignedToSecurityAuthorityId; } set { assignedToSecurityAuthorityId = value; } }

        public String AssignedToUserAccountId { get { return assignedToUserAccountId; } set { assignedToUserAccountId = value; } }

        public String AssignedToUserAccountName { get { return assignedToUserAccountName; } set { assignedToUserAccountName = value; } }

        public String AssignedToUserDisplayName { get { return assignedToUserDisplayName; } set { assignedToUserDisplayName = value; } }

        public DateTime? AssignedToDate { get { return assignedToDate; } set { assignedToDate = value; } }


        public String AssignmentSource { get { return assignmentSource; } set { assignmentSource = value; } }

        #endregion 


        #region Constructors

        public WorkQueueItemAssignmentHistory (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        #endregion


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            workQueueItemId = (Int64) currentRow["WorkQueueItemId"];


            assignedFromWorkQueueId = (Int64) currentRow["AssignedFromWorkQueueId"];

            assignedToWorkQueueId = (Int64) currentRow["AssignedToWorkQueueId"];


            assignedToSecurityAuthorityId = (Int64) currentRow["AssignedToSecurityAuthorityId"];

            assignedToUserAccountId = (String) currentRow["AssignedToUserAccountId"];

            assignedToUserAccountName = (String) currentRow["AssignedToUserAccountName"];

            assignedToUserDisplayName = (String) currentRow["AssignedToUserDisplayName"];

            assignedToDate = (currentRow["AssignedToDate"] is System.DBNull) ? null : (DateTime?) currentRow["AssignedToDate"];


            assignmentSource = (String) currentRow["AssignmentSource"];

            return;

        }

        #endregion 


    }

}
