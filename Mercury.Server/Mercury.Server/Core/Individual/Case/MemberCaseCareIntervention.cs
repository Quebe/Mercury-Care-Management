using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual.Case {

    [DataContract (Name = "MemberCaseCareIntervention")]
    public class MemberCaseCareIntervention : CareIntervention {

        #region Private Properties

        [DataMember (Name = "MemberCaseId")]
        private Int64 memberCaseId = 0;

        [DataMember (Name = "CareInterventionId")]
        private Int64 careInterventionId = 0;

        [DataMember (Name = "Status")]
        private Enumerations.CaseItemStatus status = Enumerations.CaseItemStatus.UnderDevelopment;

        [DataMember (Name = "CareInterventionActivities")] // MUST HAVE NEW NAME TO MAP ACROSS THE WEB SERVICES (SINCE PARENT INHERITANCE OBJECT HAS SAME NAME)
        private List<MemberCaseCareInterventionActivity> activities = new List<MemberCaseCareInterventionActivity> ();

        private MemberCase memberCase = null;

        #endregion


        #region Public Properties - Encapsulated

        public Int64 MemberCaseId { get { return memberCaseId; } set { memberCaseId = value; } }

        public Int64 CareInterventionId { get { return careInterventionId; } set { careInterventionId = value; } }

        public Enumerations.CaseItemStatus Status { get { return status; } set { status = value; } }

        public new List<MemberCaseCareInterventionActivity> Activities { get { return activities; } set { activities = value; } }

        public MemberCase MemberCase { get { return memberCase; } set { memberCase = value; } }

        public override Application Application {

            get { return base.Application; }

            set {

                base.Application = value;

                if (activities == null) { activities = new List<MemberCaseCareInterventionActivity> (); }

                foreach (MemberCaseCareInterventionActivity currentActivity in activities) {

                    currentActivity.Application = value;

                    currentActivity.MemberCaseCareIntervention = this;

                }

            }

        }

        #endregion


        #region Public Properties

        public CareIntervention CareIntervention { get { return application.CareInterventionGet (careInterventionId, true); } }

        #endregion


        #region Constructors

        public MemberCaseCareIntervention (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public MemberCaseCareIntervention (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference);


            base.BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion


        #region Data Functions

        public override Boolean LoadChildObjects () {

            Boolean success = base.LoadChildObjects ();


            if (success) {

                System.Data.DataTable activitiesTable = application.EnvironmentDatabase.SelectDataTable ("SELECT * FROM MemberCaseCareInterventionActivity WHERE MemberCaseCareInterventionId = " + Id.ToString (), 0);

                foreach (System.Data.DataRow currentRow in activitiesTable.Rows) {

                    MemberCaseCareInterventionActivity activity = new MemberCaseCareInterventionActivity (application, (Int64)currentRow["MemberCaseCareInterventionActivityId"]);

                    activities.Add (activity);

                }

            }

            return success;

        }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);
            

            MemberCaseId = Convert.ToInt64 (currentRow["MemberCaseId"]);

            CareInterventionId = base.IdFromSql (currentRow, "CareInterventionId");

            Status = (Enumerations.CaseItemStatus) Convert.ToInt32 (currentRow ["Status"]);


            return;

        }

        public Enumerations.MemberCaseActionOutcome SaveUpdate (Boolean ignoreAssignedTo = false) {

            Enumerations.MemberCaseActionOutcome outcome = Enumerations.MemberCaseActionOutcome.UnknownError;


            // DO NOT USE TRANSACTIONS, SERIALIZABLE TRANSACTION SUPPORTED THROUGH STORED PROCEDURE

            DateTime lastModifiedDate = modifiedAccountInfo.ActionDate;


            try {

                Dictionary<String, String> validationResponse = Validate ();

                if (validationResponse.Count != 0) {

                    foreach (String validationKey in validationResponse.Keys) {

                        application.SetLastExceptionQuite (new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]));

                        return Enumerations.MemberCaseActionOutcome.ValidationError;

                    }

                }

                modifiedAccountInfo = new Data.AuthorityAccountStamp (application.Session);


                // ATTEMPT TO SAVE NEW OR UPDATE EXISTING ENTITY ADDRESS

                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("dbo.MemberCaseCarePlanGoal_Update");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseCarePlanGoalId", Id);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@status", ((Int32)Status));

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseCarePlanGoalName", Name, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseCarePlanGoalDescription", Description, Server.Data.DataTypeConstants.Description);



                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@ignoreAssignedTo", ignoreAssignedTo);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityId", application.Session.SecurityAuthorityId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityName", ModifiedAccountInfo.SecurityAuthorityName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountId", ModifiedAccountInfo.UserAccountId, Server.Data.DataTypeConstants.UserAccountId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountName", ModifiedAccountInfo.UserAccountName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@lastModifiedDate", lastModifiedDate);


                // RETURNED MODIFIED DATE 

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedDate", ((DateTime?)null));

                ((System.Data.IDbDataParameter)sqlCommand.Parameters["@modifiedDate"]).Direction = System.Data.ParameterDirection.Output;


                // RETURN VALUE

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@RETURN", ((Int32)0));

                ((System.Data.IDbDataParameter)sqlCommand.Parameters["@RETURN"]).Direction = System.Data.ParameterDirection.ReturnValue;


                sqlCommand.ExecuteNonQuery ();

                outcome = (Enumerations.MemberCaseActionOutcome)(Convert.ToInt32 (((System.Data.IDbDataParameter)sqlCommand.Parameters["@RETURN"]).Value));

                // TODO: SET APPLICATION EXCEPTION

                // SetApplicationException (outcome);

                if (outcome == Enumerations.MemberCaseActionOutcome.Success) {

                    // UPDATE MODIFIED DATE FROM STORED PROCEDURE RESULTS

                    modifiedAccountInfo.ActionDate = Convert.ToDateTime (((System.Data.IDbDataParameter)sqlCommand.Parameters["@modifiedDate"]).Value);

                }


                // DO NOT SET ID, THIS IS AN UPDATE ONLY PROCEDURE 

            }

            catch (Exception applicationException) {

                outcome = Enumerations.MemberCaseActionOutcome.UnknownError;

                base.application.SetLastException (applicationException);

            }

            return outcome;

        }

        #endregion

    }

}
