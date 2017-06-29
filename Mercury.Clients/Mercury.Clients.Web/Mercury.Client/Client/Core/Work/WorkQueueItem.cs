using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Work {

    [Serializable]
    public class WorkQueueItem : CoreExtensibleObject {

        #region Private Properties

        private Int64 workQueueId;


        private String itemObjectType = String.Empty;

        private Int64 itemObjectId = 0;

        private String itemGroupKey = String.Empty;


        private Guid workflowInstanceId = Guid.Empty;

        private String workflowStatus;

        private String workflowLastStep = String.Empty;

        private String workflowNextStep = String.Empty;


        private DateTime addedDate;

        private DateTime? lastWorkedDate;

        private DateTime constraintDate;

        private DateTime milestoneDate;

        private DateTime thresholdDate;

        private DateTime dueDate;

        private DateTime? completionDate;

        private Int64 workOutcomeId = 0;


        private Int32 priority = 0;

        private Mercury.Server.Application.CalendarDayOfWeekTimes workTimeRestrictions = new Mercury.Server.Application.CalendarDayOfWeekTimes ();


        private Int64 assignedToSecurityAuthorityId;

        private String assignedToUserAccountId;

        private String assignedToUserAccountName;

        private String assignedToUserDisplayName;

        private DateTime? assignedToDate;

        #endregion


        #region Public Properties

        public override String Description { get { return description; } set { description = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Description99); } }


        public Int64 WorkQueueId { get { return workQueueId; } set { workQueueId = value; } }


        public String ItemObjectType { get { return itemObjectType; } set { itemObjectType = value; } }

        public Int64 ItemObjectId { get { return itemObjectId; } set { itemObjectId = value; } }

        public String ItemGroupKey { get { return itemGroupKey; } set { itemGroupKey = Server.CommonFunctions.SetValueMaxLength (value.Trim (), Server.Data.DataTypeConstants.Description99); } }


        public Guid WorkflowInstanceId { get { return workflowInstanceId; } set { workflowInstanceId = value; } }

        public String WorkflowStatus { get { return workflowStatus; } set { workflowStatus = value; } }

        public String WorkflowLastStep { get { return workflowLastStep; } set { workflowLastStep = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public String WorkflowNextStep { get { return workflowNextStep; } set { workflowNextStep = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }


        public DateTime AddedDate { get { return addedDate; } set { addedDate = value; } }

        public DateTime? LastWorkedDate { get { return lastWorkedDate; } set { lastWorkedDate = value; } }

        public DateTime ConstraintDate { get { return constraintDate; } set { constraintDate = value; } }

        public DateTime MilestoneDate { get { return milestoneDate; } set { milestoneDate = value; } }

        public DateTime ThresholdDate { get { return thresholdDate; } set { thresholdDate = value; } }

        public DateTime DueDate { get { return dueDate; } set { dueDate = value; } }

        public DateTime? CompletionDate { get { return completionDate; } set { completionDate = value; } }

        public Int64 WorkOutcomeId { get { return workOutcomeId; } set { workOutcomeId = value; } }


        public Int32 Priority { get { return priority; } }

        public Server.Application.CalendarDayOfWeekTimes WorkTimeRestrictions { get { return workTimeRestrictions; } set { workTimeRestrictions = value; } }


        public Int64 AssignedToSecurityAuthorityId { get { return assignedToSecurityAuthorityId; } set { assignedToSecurityAuthorityId = value; } }

        public String AssignedToUserAccountId { get { return assignedToUserAccountId; } set { assignedToUserAccountId = value; } }

        public String AssignedToUserAccountName { get { return assignedToUserAccountName; } set { assignedToUserAccountName = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public String AssignedToUserDisplayName { get { return assignedToUserDisplayName; } set { assignedToUserDisplayName = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public DateTime? AssignedToDate { get { return assignedToDate; } set { assignedToDate = value; } }

        #endregion 


        #region Public Object/Calculated Properties

        public String WorkQueueName { get { return application.CoreObjectGetNameById ("WorkQueue", workQueueId, true); } }

        public WorkQueue WorkQueue { get { return application.WorkQueueGet (workQueueId, true); } }

        public String WorkflowName { get { return ((WorkQueue != null) ? WorkQueue.WorkflowName : String.Empty); } }

        public String WorkOutcomeName { get { return application.CoreObjectGetNameById ("WorkOutcome", workOutcomeId, true); } }

        public WorkOutcome WorkOutcome { get { return application.WorkOutcomeGet (workOutcomeId, true); } }


        public String StatusText {

            get {

                String statusText = "Ok";

                if ((DateTime.Now >= milestoneDate) || (DateTime.Now >= thresholdDate)) { statusText = "Warning"; }

                if (DateTime.Now >= dueDate) { statusText = "Critical"; }

                if (CompletionDate.HasValue) { statusText = "Completed"; }

                return statusText;

            }

        }

        public Boolean HasOwnership {

            get {

                Boolean hasOwnership = false;

                if (application != null) {

                    hasOwnership = ((assignedToSecurityAuthorityId == application.Session.SecurityAuthorityId) && (assignedToUserAccountId == application.Session.UserAccountId));

                }

                return hasOwnership;

            }

        }

        #endregion

        
        #region Constructors

        public WorkQueueItem (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public WorkQueueItem (Application applicationReference, Server.Application.WorkQueueItem serverWorkQueueItem) {

            BaseConstructor (applicationReference, serverWorkQueueItem);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.WorkQueueItem serverWorkQueueItem) {

            base.BaseConstructor (applicationReference, serverWorkQueueItem);


            workQueueId = serverWorkQueueItem.WorkQueueId;


            itemObjectType = serverWorkQueueItem.ItemObjectType;

            itemObjectId = serverWorkQueueItem.ItemObjectId;

            itemGroupKey = serverWorkQueueItem.ItemGroupKey;


            workflowInstanceId = serverWorkQueueItem.WorkflowInstanceId;

            workflowStatus = serverWorkQueueItem.WorkflowStatus;

            workflowLastStep = serverWorkQueueItem.WorkflowLastStep;

            workflowNextStep = serverWorkQueueItem.WorkflowNextStep;


            addedDate = serverWorkQueueItem.AddedDate;

            lastWorkedDate = serverWorkQueueItem.LastWorkedDate;

            constraintDate = serverWorkQueueItem.ConstraintDate;

            milestoneDate = serverWorkQueueItem.MilestoneDate;

            thresholdDate = serverWorkQueueItem.ThresholdDate;

            dueDate = serverWorkQueueItem.DueDate;

            completionDate = serverWorkQueueItem.CompletionDate;

            workOutcomeId = serverWorkQueueItem.WorkOutcomeId;


            priority = serverWorkQueueItem.Priority;

            workTimeRestrictions = serverWorkQueueItem.WorkTimeRestrictions;


            assignedToSecurityAuthorityId = serverWorkQueueItem.AssignedToSecurityAuthorityId;

            assignedToUserAccountId = serverWorkQueueItem.AssignedToUserAccountId;

            assignedToUserAccountName = serverWorkQueueItem.AssignedToUserAccountName;

            assignedToUserDisplayName = serverWorkQueueItem.AssignedToUserDisplayName;

            assignedToDate = serverWorkQueueItem.AssignedToDate;

            return;

        }

        #endregion


        #region Public Methods

        public Boolean AssignTo (Int64 securityAuthorityId, String userAccountId, String userAccountName, String userDisplayName, String assignmentSource) {

            if (application == null) { return false; }

            return application.WorkQueueItemAssignTo (id, securityAuthorityId, userAccountId, userAccountName, userDisplayName, assignmentSource);

        }

        public Boolean MoveToQueue (Int64 destinationWorkQueueId) {

            if (application == null) { return false; }

            return application.WorkQueueItemMoveToQueue (id, destinationWorkQueueId);

        }

        public Boolean Close (Int64 workOutcomeId) {

            if (application == null) { return false; }

            return application.WorkQueueItemClose (id, workOutcomeId);

        }

        public Boolean Suspend (String lastStep, String nextStep, Int32 constraintDays, Int32 milestoneDays, Boolean releaseItem) {

            if (application == null) { return false; }

            return application.WorkQueueItemSuspend (id, lastStep, nextStep, constraintDays, milestoneDays, releaseItem);

        }

        #endregion 

    }

}
