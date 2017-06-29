using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Workflows {

    [Serializable]
    [DataContract (Name = "WorkflowStep")]
    public class WorkflowStep : Core.CoreObject {

        #region Private Properties

        [DataMember (Name = "WorkQueueItemId")]
        private Int64 workQueueItemId = 0;

        [DataMember (Name = "StepSequence")]
        private Int32 stepSequence = 0;

        [DataMember (Name = "StepDate")]
        private DateTime stepDate = DateTime.Now;

        [DataMember (Name = "StepStatus")]
        private Enumerations.WorkflowStepStatus stepStatus = Enumerations.WorkflowStepStatus.Informational;

        // NAME AND DATE WITH CORE OBJECT

        [DataMember (Name = "UserDisplayName")]
        private String userDisplayName = String.Empty;

        // CREATE INFORMATION WITH CORE OBJECT

        #endregion


        #region Public Properties

        public Int64 WorkQueueItemId { get { return workQueueItemId; } set { workQueueItemId = value; } }

        public Int32 StepSequence { get { return stepSequence; } set { stepSequence = value; } }

        public DateTime StepDate { get { return stepDate; } set { stepDate = value; } }

        public Enumerations.WorkflowStepStatus StepStatus { get { return stepStatus; } set { stepStatus = value; } }


        public String UserDisplayName { get { return userDisplayName; } set { userDisplayName = value; } }

        #endregion 


        #region Constructors

        public WorkflowStep (Server.Application application, String stepName, String stepDescription) {

            Name = stepName;

            Description = stepDescription;

            if (application != null) {

                CreateAccountInfo = new Data.AuthorityAccountStamp (application.Session);

                userDisplayName = application.Session.UserDisplayName;

            }

            return;

        }

        public WorkflowStep (Server.Application application, Server.Workflows.Enumerations.WorkflowStepStatus forStepStatus, String stepName, String stepDescription) {

            stepStatus = forStepStatus;

            Name = stepName;

            Description = stepDescription;

            if (application != null) {

                CreateAccountInfo = new Data.AuthorityAccountStamp (application.Session);

                userDisplayName = application.Session.UserDisplayName;

            }

            return;

        }
        public WorkflowStep () { /* DO NOTHING */ }

        #endregion 


        #region Database Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            name = (String)currentRow["StepName"];

            description = (String) currentRow ["StepDescription"];


            workQueueItemId = (Int64)currentRow["WorkQueueItemId"];

            stepSequence = (Int32) currentRow["StepSequence"];

            stepDate = (DateTime) currentRow["StepDate"];

            stepStatus = (Enumerations.WorkflowStepStatus) Convert.ToInt32 (currentRow ["StepStatus"]);


            userDisplayName = (String) currentRow["UserDisplayName"];

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        #endregion


        #region Public Methods

        public WorkflowStep Copy () {

            WorkflowStep copied = new WorkflowStep ();

            copied.workQueueItemId = workQueueItemId;

            copied.StepSequence = stepSequence;

            copied.StepDate = stepDate;

            copied.StepStatus = stepStatus;

            copied.Name = name;

            copied.Description = description;


            copied.CreateAccountInfo = new Data.AuthorityAccountStamp ();

            copied.CreateAccountInfo.SecurityAuthorityName = createAccountInfo.SecurityAuthorityName;

            copied.CreateAccountInfo.UserAccountId = createAccountInfo.UserAccountId;

            copied.CreateAccountInfo.UserAccountName = createAccountInfo.UserAccountName;

            copied.CreateAccountInfo.ActionDate = createAccountInfo.ActionDate;

            
            copied.UserDisplayName = userDisplayName;

            return copied;

        }

        #endregion

    }

}
