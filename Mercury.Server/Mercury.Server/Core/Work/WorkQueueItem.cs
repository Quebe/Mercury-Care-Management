using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Work {

    [Serializable]
    [DataContract (Name = "WorkQueueItem")]
    public class WorkQueueItem : CoreExtensibleObject {

        #region Private Properties

        [DataMember (Name = "WorkQueueId")]
        private Int64 workQueueId;


        [DataMember (Name = "ItemObjectType")]
        private String itemObjectType = String.Empty;

        [DataMember (Name = "ItemObjectId")]
        private Int64 itemObjectId = 0;

        [DataMember (Name = "ItemGroupKey")]
        private String itemGroupKey = String.Empty;


        [DataMember (Name = "WorkflowInstanceId")]
        private Guid workflowInstanceId = Guid.Empty;

        [DataMember (Name = "WorkflowStatus")]
        private String workflowStatus;

        [DataMember (Name = "WorkflowLastStep")]
        private String workflowLastStep = String.Empty;

        [DataMember (Name = "WorkflowNextStep")]
        private String workflowNextStep = String.Empty;


        [DataMember (Name = "AddedDate")]
        private DateTime addedDate;

        [DataMember (Name = "LastWorkedDate")]
        private DateTime? lastWorkedDate;

        [DataMember (Name = "ConstraintDate")]
        private DateTime constraintDate;

        [DataMember (Name = "MilestoneDate")]
        private DateTime milestoneDate;

        [DataMember (Name = "ThresholdDate")]
        private DateTime thresholdDate;

        [DataMember (Name = "DueDate")]
        private DateTime dueDate;

        [DataMember (Name = "CompletionDate")]
        private DateTime? completionDate;

        [DataMember (Name = "WorkOutcomeId")]
        private Int64 workOutcomeId = 0;


        [DataMember (Name = "Priority")]
        private Int32 priority = 0;

        [DataMember (Name = "WorkTimeRestrictions")]
        private Calendar.DayOfWeekTimes workTimeRestrictions = new Mercury.Server.Calendar.DayOfWeekTimes ();


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


        [NonSerialized]
        private WorkQueue workQueue = null;    
    
        #endregion


        #region Public Properties

        public override String Description { get { return description; } set { description = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Description99); } }


        public Int64 WorkQueueId { get { return workQueueId; } set { workQueueId = value; } }


        public String ItemObjectType { get { return itemObjectType; } set { itemObjectType = value; } }

        public Int64 ItemObjectId { get { return itemObjectId; } set { itemObjectId = value; } }

        public String ItemGroupKey { get { return itemGroupKey; } set { itemGroupKey = CommonFunctions.SetValueMaxLength (value.Trim (), Data.DataTypeConstants.Description99); } }


        public Guid WorkflowInstanceId { get { return workflowInstanceId; } set { workflowInstanceId = value; } }

        public String WorkflowStatus { get { return workflowStatus; } set { workflowStatus = value; } }

        public String WorkflowLastStep { get { return workflowLastStep; } set { workflowLastStep = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public String WorkflowNextStep { get { return workflowNextStep; } set { workflowNextStep = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }


        public DateTime AddedDate { get { return addedDate; } set { addedDate = value; } }

        public DateTime? LastWorkedDate { get { return lastWorkedDate; } set { lastWorkedDate = value; } }

        public DateTime ConstraintDate { get { return constraintDate; } set { constraintDate = value; } }

        public DateTime MilestoneDate { get { return milestoneDate; } set { milestoneDate = value; } }

        public DateTime ThresholdDate { get { return thresholdDate; } set { thresholdDate = value; } }

        public DateTime DueDate { get { return dueDate; } set { dueDate = value; } }

        public DateTime? CompletionDate { get { return completionDate; } set { completionDate = value; } }

        public Int64 WorkOutcomeId { get { return workOutcomeId; } set { workOutcomeId = value; } }


        public Int32 Priority { get { return priority; } }

        public Calendar.DayOfWeekTimes WorkTimeRestrictions { get { return workTimeRestrictions; } set { workTimeRestrictions = value; } }


        public Int64 AssignedToSecurityAuthorityId { get { return assignedToSecurityAuthorityId; } set { assignedToSecurityAuthorityId = value; } }

        public String AssignedToUserAccountId { get { return assignedToUserAccountId; } set { assignedToUserAccountId = value; } }

        public String AssignedToUserAccountName { get { return assignedToUserAccountName; } set { assignedToUserAccountName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public String AssignedToUserDisplayName { get { return assignedToUserDisplayName; } set { assignedToUserDisplayName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public DateTime? AssignedToDate { get { return assignedToDate; } set { assignedToDate = value; } }

        
        public WorkQueue WorkQueue { 

            get { 

                if (workQueue != null) { return workQueue; }

                if (application == null) { workQueue = null; }

                else { workQueue = application.WorkQueueGet (workQueueId); }

                return workQueue;

            }

        }

        public List<WorkQueueItemSender> Senders { get { return application.WorkQueueItemSendersGet (Id); } }

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

        public WorkQueueItem (Application applicationReference, Int64 forWorkQueueItemId) {

            BaseConstructor (applicationReference, forWorkQueueItemId);

            return;

        }

        #endregion


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            System.Xml.XmlDocument workTimeRestrictionsXml = new System.Xml.XmlDocument ();

            System.Xml.XmlDocument extendedPropertiesXml = new System.Xml.XmlDocument ();


            workQueueId = (Int64) currentRow ["WorkQueueId"];


            itemObjectType = (String) currentRow["ItemObjectType"];

            itemObjectId = (Int64) currentRow ["ItemObjectId"];

            itemGroupKey = (String) currentRow["ItemGroupKey"];


            if (!(currentRow["WorkflowInstanceId"] is System.DBNull)) { workflowInstanceId = (Guid) currentRow["WorkflowInstanceId"]; }

            workflowStatus = (String) currentRow["WorkflowStatus"];

            workflowLastStep = (String) currentRow["WorkflowLastStep"];

            workflowNextStep = (String)currentRow["WorkflowNextStep"];


            DueDate = (DateTime) currentRow["DueDate"];

            AddedDate = (DateTime) currentRow["AddedDate"];

            LastWorkedDate = (currentRow["LastWorkedDate"] is System.DBNull) ? null : (DateTime?) currentRow["LastWorkedDate"];

            ConstraintDate = (DateTime) currentRow["ConstraintDate"];

            MilestoneDate = (DateTime) currentRow["MilestoneDate"];

            ThresholdDate = (DateTime) currentRow["ThresholdDate"];


            if (!(currentRow["WorkTimeRestrictions"] is System.DBNull)) {

                if (!String.IsNullOrEmpty ((String) currentRow["WorkTimeRestrictions"])) {

                    workTimeRestrictionsXml.LoadXml ((String) currentRow["WorkTimeRestrictions"]);

                    workTimeRestrictions = new Mercury.Server.Calendar.DayOfWeekTimes (workTimeRestrictionsXml);

                }

            }


            CompletionDate = (currentRow["CompletionDate"] is System.DBNull) ? null : (DateTime?) currentRow["CompletionDate"];

            WorkOutcomeId = (currentRow["WorkOutcomeId"] is System.DBNull) ? 0 : (Int64) currentRow["WorkOutcomeId"];


            priority = (Int32) currentRow["Priority"];


            assignedToSecurityAuthorityId = (Int64) currentRow["AssignedToSecurityAuthorityId"];

            assignedToUserAccountId = (String) currentRow["AssignedToUserAccountId"];

            assignedToUserAccountName = (String) currentRow["AssignedToUserAccountName"];

            assignedToUserDisplayName = (String) currentRow["AssignedToUserDisplayName"];

            assignedToDate = (currentRow["AssignedToDate"] is System.DBNull) ? null : (DateTime?) currentRow["AssignedToDate"];

            return;

        }

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            String workTimeRestrictionsXml;


            if (Id == 0) { return false; }


            modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);

            try {

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dbo.WorkQueueItem_Update ");

                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append (workQueueId.ToString () + ", ");


                sqlStatement.Append ("'" + itemObjectType + "', ");

                sqlStatement.Append (itemObjectId.ToString () + ", ");

                sqlStatement.Append ("'" + NameSql + "', ");

                sqlStatement.Append ("'" + DescriptionSql + "', ");

                sqlStatement.Append ("'" + itemGroupKey.Replace ("'", "''") + "', ");


                if (workflowInstanceId == Guid.Empty) { sqlStatement.Append ("NULL, "); }

                else { sqlStatement.Append ("'" + workflowInstanceId.ToString () + "', "); }


                sqlStatement.Append ("'" + workflowStatus.Replace ("'", "''") + "', ");

                sqlStatement.Append ("'" + workflowLastStep.Replace ("'", "''") + "', ");

                sqlStatement.Append ("'" + workflowNextStep.Replace ("'", "''") + "', ");


                sqlStatement.Append ("'" + addedDate.ToString ("MM/dd/yyyy") + "', ");

                sqlStatement.Append ((lastWorkedDate.HasValue) ? "'" + lastWorkedDate.Value.ToString () + "', " : "NULL, ");

                sqlStatement.Append ("'" + constraintDate.ToString ("MM/dd/yyyy") + "', ");

                sqlStatement.Append ("'" + milestoneDate.ToString ("MM/dd/yyyy") + "', ");

                sqlStatement.Append ("'" + thresholdDate.ToString ("MM/dd/yyyy") + "', ");

                sqlStatement.Append ("'" + dueDate.ToString ("MM/dd/yyyy") + "', ");

                sqlStatement.Append ((completionDate.HasValue) ? "'" + completionDate.Value.ToString ("MM/dd/yyyy") + "', " : "NULL, ");

                sqlStatement.Append (((workOutcomeId == 0) ? "0" : workOutcomeId.ToString ()) + ", ");


                if (!workTimeRestrictions.HasTimes) {

                    sqlStatement.Append ("NULL, ");

                }

                else {

                    workTimeRestrictionsXml = workTimeRestrictions.XmlSerialize.InnerXml;

                    workTimeRestrictionsXml = workTimeRestrictionsXml.Replace ("'", "''");

                    workTimeRestrictionsXml = workTimeRestrictionsXml.Replace ((char) 0xA0, (char) 0x20);

                    workTimeRestrictionsXml = workTimeRestrictionsXml.Replace ((char) 0xB7, (char) 0x20);

                    sqlStatement.Append ("'" + workTimeRestrictionsXml + "', ");

                }



                sqlStatement.Append (assignedToSecurityAuthorityId.ToString () + ", ");

                sqlStatement.Append ("'" + assignedToUserAccountId + "', ");

                sqlStatement.Append ("'" + assignedToUserAccountName.Replace ("'", "''") + "', ");

                sqlStatement.Append ("'" + assignedToUserDisplayName.Replace ("'", "''") + "', ");

                sqlStatement.Append ((assignedToDate.HasValue) ? "'" + assignedToDate.Value.ToString ("MM/dd/yyyy") + "', " : "NULL, ");


                sqlStatement.Append ("'" + ExtendedPropertiesSql + "', ");

                sqlStatement.Append ("'" + modifiedAccountInfo.SecurityAuthorityNameSql + "', '" + modifiedAccountInfo.UserAccountIdSql + "', '" + modifiedAccountInfo.UserAccountNameSql + "'");


                success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }

                SetIdentity ();

            }

            catch (Exception applicationException) {

                success = false;

                application.SetLastException (applicationException);

            }

            return success;

        }

        /// <summary>
        /// Add a Work Queue Item Sender to an existing (saved) Work Queue Item.
        /// </summary>
        /// <param name="forSender">Work Queue Item Sender Object Instance</param>
        /// <returns>Success (Boolean)</returns>
        public Boolean AddSender (WorkQueueItemSender forSender) {

            Boolean success = true;

            forSender.WorkQueueItemId = Id;

            success = forSender.Save ();

            return success; 

        }

        #endregion


        #region Public Functions

        public Boolean AssignTo (Int64 securityAuthorityId, String userAccountId, String userAccountName, String userDisplayName, String assignmentSource, Boolean validatePermission) {

            Boolean success = false;

            application.SetLastException (null);

            if (WorkQueue == null) { throw new ApplicationException ("Unable to reference Work Queue."); }

            try {

                if ((assignedToSecurityAuthorityId != securityAuthorityId) || (assignedToUserAccountId != userAccountId)) {

                    #region ** Not Assigned (directly or release)

                    if (securityAuthorityId == 0) { // ATTEMPT UNASSIGN

                        if ((assignedToSecurityAuthorityId == application.Session.SecurityAuthorityId) && (assignedToUserAccountId == application.Session.UserAccountId)) { // UNASSIGN FROM SELF (RELEASE)

                            assignedToSecurityAuthorityId = 0;

                            assignedToUserAccountId = String.Empty;

                            assignedToUserAccountName = String.Empty;

                            assignedToUserDisplayName = String.Empty;

                            assignedToDate = null;

                            success = Save ();

                        }

                        else if ((WorkQueue.HasManagePermission) || (!validatePermission)) { // RELEASE 

                            assignedToSecurityAuthorityId = 0;

                            assignedToUserAccountId = String.Empty;

                            assignedToUserAccountName = String.Empty;

                            assignedToUserDisplayName = String.Empty;

                            assignedToDate = null;

                            success = Save ();

                        }

                        else { throw new ApplicationException ("Permission Denied when unassigning Work Queue Item."); }

                    }

                    #endregion

                    #region Self Assign

                    else if ((securityAuthorityId == application.Session.SecurityAuthorityId) && (userAccountId == application.Session.UserAccountId)) { // SELF ASSIGN

                        if ((WorkQueue.HasManagePermission) || (WorkQueue.HasSelfAssignPermission) || (!validatePermission)) {

                            assignedToSecurityAuthorityId = securityAuthorityId;

                            assignedToUserAccountId = userAccountId;

                            assignedToUserAccountName = application.Session.UserAccountName;

                            assignedToUserDisplayName = application.Session.UserDisplayName;

                            assignedToDate = DateTime.Now;


                            success = Save ();

                        }

                        else { throw new ApplicationException ("Unable to reference Work Queue."); }

                    }

                    #endregion

                    #region Assign to Specific User

                    else {

                        if ((WorkQueue.HasManagePermission) || (!validatePermission)) {

                            assignedToSecurityAuthorityId = securityAuthorityId;

                            assignedToUserAccountId = userAccountId;

                            assignedToUserAccountName = userAccountName;

                            assignedToUserDisplayName = userDisplayName;

                            assignedToDate = DateTime.Now;

                            success = Save ();

                        }

                    }

                    #endregion


                    if (success) {

                        StringBuilder sqlStatement = new StringBuilder ();

                        sqlStatement.Append ("EXEC dbo.WorkQueueItemAssignmentHistory_Insert ");

                        sqlStatement.Append (Id.ToString () + ", ");


                        sqlStatement.Append (workQueueId.ToString () + ", ");

                        sqlStatement.Append (workQueueId.ToString () + ", ");


                        sqlStatement.Append (assignedToSecurityAuthorityId.ToString () + ", ");

                        sqlStatement.Append ("'" + assignedToUserAccountId + "', ");

                        sqlStatement.Append ("'" + assignedToUserAccountName.Replace ("'", "''") + "', ");

                        sqlStatement.Append ("'" + assignedToUserDisplayName.Replace ("'", "''") + "', ");

                        sqlStatement.Append ((assignedToDate.HasValue) ? "'" + assignedToDate.Value.ToString () + "', " : "NULL, ");

                        if (assignmentSource.Length > 60) { assignmentSource = assignmentSource.Substring (0, 60); }

                        sqlStatement.Append (((!String.IsNullOrEmpty (assignmentSource)) ? "'" + assignmentSource.Replace ("'", "''") + "'" : "NULL") + ", ");


                        Server.Data.AuthorityAccountStamp assignmentAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);

                        sqlStatement.Append ("'" + assignmentAccountInfo.SecurityAuthorityNameSql + "', '" + assignmentAccountInfo.UserAccountIdSql + "', '" + assignmentAccountInfo.UserAccountNameSql + "'");


                        success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);


                    }

                }

                else { success = true; } // NO ASSIGNMENT CHANGE
           
            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                success = false;

            }

            return success;

        }

        public Boolean SelfAssign (String assignmentSource, Boolean validatePermission) {

            return AssignTo (application.Session.SecurityAuthorityId, application.Session.UserAccountId, application.Session.UserAccountName, application.Session.UserDisplayName, assignmentSource, validatePermission);

        }

        public Boolean MoveToQueue (Int64 destinationWorkQueueId, Boolean validatePermission) {

            Boolean success = false;

            application.SetLastException (null);

            WorkQueue destinationWorkQueue = application.WorkQueueGet (destinationWorkQueueId); 

            if (WorkQueue == null) { throw new ApplicationException ("Unable to reference Source Work Queue."); }

            if (destinationWorkQueue == null) { throw new ApplicationException ("Unable to reference Destination Work Queue."); }

            try {

                if (validatePermission) {

                    if (!WorkQueue.HasManagePermission) { throw new ApplicationException ("Permission Denied on Source Work Queue."); }

                    if (!destinationWorkQueue.HasManagePermission) { throw new ApplicationException ("Permission Denied on Destination Work Queue."); }

                }


                if (workQueueId != destinationWorkQueueId) {

                    workQueue = null;

                    StringBuilder sqlStatement = new StringBuilder ();

                    sqlStatement.Append ("EXEC WorkQueueItem_MoveToQueue " + Id.ToString () + ", " + destinationWorkQueueId.ToString () + ", ");

                    sqlStatement.Append ("NULL, ");

                    Server.Data.AuthorityAccountStamp assignmentAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);

                    sqlStatement.Append ("'" + assignmentAccountInfo.SecurityAuthorityNameSql + "', '" + assignmentAccountInfo.UserAccountIdSql + "', '" + assignmentAccountInfo.UserAccountNameSql + "'");


                    success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                    if (!success) { throw application.EnvironmentDatabase.LastException; }

                }
                
            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                success = false;

            }

            return success;
        }

        public Boolean Close (Int64 forWorkOutcomeId, Boolean validatePermission) {

            Boolean success = false;

            application.SetLastException (null);

            try {

                if (WorkQueue != null) {

                    if ((WorkQueue.HasManagePermission) || (HasOwnership) || (!validatePermission) ) {

                        WorkOutcome workOutcome = application.WorkOutcomeGet (forWorkOutcomeId);

                        if (workOutcome != null) {

                            if (!completionDate.HasValue) {

                                success = SelfAssign ("Work Queue Item Close", validatePermission);

                                if (success) {

                                    lastWorkedDate = DateTime.Now;
                                    
                                    workflowNextStep = String.Empty;

                                    completionDate = DateTime.Now;

                                    workOutcomeId = forWorkOutcomeId;

                                    success = Save ();

                                }

                                else { throw new ApplicationException ("Permission Denied on Closing Work Queue Item [" + Id.ToString () + "] (not assigned to you or no manager rights to Work Queue)."); }

                            }

                            else { throw new ApplicationException ("Work Queue Item [" + Id.ToString () + "] is already closed."); }

                        }

                        else { throw new ApplicationException ("No Work Outcome Assigned for closing Work Queue Item [" + Id.ToString () + "]."); }

                    }

                    else { throw new ApplicationException ("Permission Denied on Closing Work Queue Item [" + Id.ToString () + "] (not assigned to you or no manager rights to Work Queue)."); }

                }

                else { throw new ApplicationException ("Unable to reference Work Queue for Work Queue Item [" + Id.ToString () + "]."); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                success = false;

            }

            return success;

        }

        public Boolean Suspend (String lastStep, String nextStep, Int32 constraintDays, Int32 milestoneDays, Boolean releaseItem) {

            Boolean success = false;

            application.SetLastException (null);

            try {

                if (WorkQueue != null) {

                    if ((WorkQueue.HasManagePermission) || (HasOwnership)) {

                        if (!completionDate.HasValue) {

                            LastWorkedDate = DateTime.Today;

                            WorkflowLastStep = lastStep;

                            WorkflowNextStep = nextStep;

                            ConstraintDate = DateTime.Today.AddDays (constraintDays);

                            MilestoneDate = DateTime.Today.AddDays (milestoneDays);

                            success = Save ();

                            if ((success) && (releaseItem)) {

                                success = AssignTo (0, String.Empty, String.Empty, String.Empty, "Release from Suspend", false);

                            }

                        }

                        else { throw new ApplicationException ("Item is already closed."); }

                    }

                    else { throw new ApplicationException ("Permission Denied on Suspend Work Queue Item (not assigned to you or no manager rights to Work Queue)."); }

                }

                else { throw new ApplicationException ("Unable to reference Work Queue."); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                success = false;

            }

            return success;

        }


        public List<Server.Workflows.WorkflowStep> WorkflowStepsGet () {

            return application.WorkQueueItemWorkflowStepsGet (id);

        }

        public Boolean WorkflowStepsSave (List<Server.Workflows.WorkflowStep> workflowSteps) {

            return application.WorkQueueItemWorkflowStepsSave (id, workflowSteps);

        }

        #endregion


        #region Virtual - Data Bindings

        public override Dictionary<String, String> DataBindingContexts {

            get {

                Dictionary<String, String> dataBindingContexts = base.DataBindingContexts;


                dataBindingContexts.Add ("WorkQueueId", "Id|WorkQueue");

                dataBindingContexts.Add ("ItemObjectType", "String");

                dataBindingContexts.Add ("ItemGroupKey", "String");

                dataBindingContexts.Add ("WorkflowInstanceId", "Guid");
                
                dataBindingContexts.Add ("WorkflowStatus", "String");

                dataBindingContexts.Add ("WorkflowLastStep", "String");

                dataBindingContexts.Add ("WorkflowNextStep", "String");


                dataBindingContexts.Add ("AddedDate", "DateTime");

                dataBindingContexts.Add ("LastWorkedDate", "DateTime");

                dataBindingContexts.Add ("ConstraintDate", "DateTime");

                dataBindingContexts.Add ("MilestoneDate", "DateTime");

                dataBindingContexts.Add ("ThresholdDate", "DateTime");

                dataBindingContexts.Add ("DueDate", "DateTime");

                dataBindingContexts.Add ("CompletionDate", "DateTime");


                dataBindingContexts.Add ("WorkOutcomeId", "Id|WorkOutcome");

                dataBindingContexts.Add ("Priority", "Integer");

                





                return dataBindingContexts;

            }

        }


        public override String EvaluateDataBinding (String bindingContext) {

            String dataValue = String.Empty;

            String bindingContextPart = bindingContext.Split ('.')[0];


            switch (bindingContextPart) {


                default: dataValue = base.EvaluateDataBinding (bindingContext); break;

            }

            return dataValue;

        }

        #endregion

    }

}
