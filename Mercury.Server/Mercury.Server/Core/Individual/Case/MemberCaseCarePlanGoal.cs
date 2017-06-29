using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual.Case {


    [DataContract (Name = "MemberCaseCarePlanGoal")]
    public class MemberCaseCarePlanGoal : CarePlanGoal {

        #region Private Properties

        [DataMember (Name = "MemberCaseCarePlanId")]
        private Int64 memberCaseCarePlanId = 0;

        [DataMember (Name = "CarePlanGoalId")]
        private Int64 carePlanGoalId = 0;

        [DataMember (Name = "Inclusion")]
        private Enumerations.CarePlanItemInclusion inclusion = Enumerations.CarePlanItemInclusion.Optional;

        [DataMember (Name = "Status")]
        private Enumerations.CaseItemStatus status = Enumerations.CaseItemStatus.UnderDevelopment;

        [DataMember (Name = "InitialValue")]
        private Decimal initialValue = 0;

        [DataMember (Name = "LastValue")]
        private Decimal lastValue = 0;

        [DataMember (Name = "TargetValue")]
        private Decimal targetValue = 0;

        [DataMember (Name = "GoalInterventions")]
        private List<MemberCaseCarePlanGoalIntervention> goalInterventions = new List<MemberCaseCarePlanGoalIntervention> ();

        private MemberCaseCarePlan memberCaseCarePlan = null;

        #endregion


        #region Public Properties - Encapsulated

        public Int64 MemberCaseCarePlanId { get { return memberCaseCarePlanId; } set { memberCaseCarePlanId = value; } }

        public Int64 CarePlanGoalId { get { return carePlanGoalId; } set { carePlanGoalId = value; } }

        public Enumerations.CaseItemStatus Status { get { return status; } set { status = value; } }

        public Decimal InitialValue { get { return initialValue; } }

        public Decimal LastValue { get { return lastValue; } }

        public Decimal TargetValue { get { return targetValue; } }

        public List<MemberCaseCarePlanGoalIntervention> GoalInterventions { get { return goalInterventions; } set { goalInterventions = value; } }

        public MemberCaseCarePlan MemberCaseCarePlan { get { return memberCaseCarePlan; } set { memberCaseCarePlan = value; } }

        #endregion


        #region Public Properties

        public override Application Application {

            set {

                base.Application = value;

                // PROPOGATE: SET ALL CHILD REFERENCES

                if (goalInterventions == null) { goalInterventions = new List<MemberCaseCarePlanGoalIntervention> (); }

                foreach (MemberCaseCarePlanGoalIntervention currentGoalIntervention in goalInterventions) {

                    currentGoalIntervention.Application = value;

                    currentGoalIntervention.MemberCaseCarePlanGoal = this;

                }

            }

        }

        #endregion
        

        #region Constructors
        
        protected MemberCaseCarePlanGoal () { /* DO NOTHING, FOR INHERITANCE */ }

        public MemberCaseCarePlanGoal (Application applicationReference) {

            BaseConstructor (applicationReference);
            
            return;  
        
        }

        public MemberCaseCarePlanGoal (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference);
            

            base.BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion 
                
        
        #region Data Functions

        public override Boolean LoadChildObjects () {

            System.Data.DataTable dataTable;

            Boolean success = base.LoadChildObjects ();


            if (success) {

                // LOAD CHILD OBJECTS 

                String selectGoals = "SELECT * FROM dbo.MemberCaseCarePlanGoalIntervention WHERE MemberCaseCarePlanGoalId = " + Id.ToString ();

                dataTable = application.EnvironmentDatabase.SelectDataTable (selectGoals.ToString (), 0);

                foreach (System.Data.DataRow currentRow in dataTable.Rows) {

                    MemberCaseCarePlanGoalIntervention intervention = new MemberCaseCarePlanGoalIntervention (application);

                    intervention.MapDataFields (currentRow);

                    intervention.LoadChildObjects ();

                    intervention.MemberCaseCarePlanGoal = this;

                    goalInterventions.Add (intervention);

                }

            }

            return success;

        }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            MemberCaseCarePlanId = base.IdFromSql (currentRow, "MemberCaseCarePlanId");

            CarePlanGoalId = base.IdFromSql (currentRow, "CarePlanGoalId");

            Status = (Enumerations.CaseItemStatus)Convert.ToInt32 (currentRow["Status"]);

            initialValue = Convert.ToDecimal (currentRow["InitialValue"]);

            lastValue = Convert.ToDecimal (currentRow["LastValue"]);

            targetValue = Convert.ToDecimal (currentRow["TargetValue"]);

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

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

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@status", ((Int32) Status));

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseCarePlanGoalName", Name, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseCarePlanGoalDescription", Description, Server.Data.DataTypeConstants.Description);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@clinicalNarrative", ClinicalNarrative, Server.Data.DataTypeConstants.Description);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@commonNarrative", CommonNarrative, Server.Data.DataTypeConstants.Description);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@goalTimeframe", ((Int32) GoalTimeframe));

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@scheduleValue", ScheduleValue);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@scheduleQualifier", ((Int32)ScheduleQualifier));

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careMeasureId", CareMeasureId);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@ignoreAssignedTo", ignoreAssignedTo);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityId", application.Session.SecurityAuthorityId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityName", ModifiedAccountInfo.SecurityAuthorityName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountId", ModifiedAccountInfo.UserAccountId, Server.Data.DataTypeConstants.UserAccountId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountName", ModifiedAccountInfo.UserAccountName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@lastModifiedDate", lastModifiedDate);


                // RETURNED MODIFIED DATE 

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedDate", ((DateTime?)null));

                ((System.Data.IDbDataParameter) sqlCommand.Parameters["@modifiedDate"]).Direction = System.Data.ParameterDirection.Output;


                // RETURN VALUE

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@RETURN", ((Int32)0));

                ((System.Data.IDbDataParameter) sqlCommand.Parameters["@RETURN"]).Direction = System.Data.ParameterDirection.ReturnValue;


                sqlCommand.ExecuteNonQuery ();

                outcome = (Enumerations.MemberCaseActionOutcome)(Convert.ToInt32 (((System.Data.IDbDataParameter) sqlCommand.Parameters["@RETURN"]).Value));

                // TODO: SET APPLICATION EXCEPTION

                // SetApplicationException (outcome);

                if (outcome == Enumerations.MemberCaseActionOutcome.Success) {

                    // UPDATE MODIFIED DATE FROM STORED PROCEDURE RESULTS

                    modifiedAccountInfo.ActionDate = Convert.ToDateTime (((System.Data.IDbDataParameter) sqlCommand.Parameters["@modifiedDate"]).Value);

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


        #region Public Properties

        public Enumerations.MemberCaseActionOutcome AddCareIntervention (Int64 careInterventionId, Boolean isSingleInstance) {

            // BUSINESS RULES FOR ADDING A CARE INTERVENTION TO A MEMBER CASE

            // 1. THE CASE MUST BE UNDER DEVELOPMENT OR ACTIVE

            // 2. THE CASE MUST NOT BE LOCKED

            // 3. THE CASE MUST BE UNASSIGNED OR THE USER A MEMBER OF THE CARE TEAM ASSIGNED TO THE CASE

            // 4. THE CARE INTERVENTION MUST NOT ALREADY EXIST IN ACTIVE     


            Enumerations.MemberCaseActionOutcome outcome = Enumerations.MemberCaseActionOutcome.UnknownError;

            // DO NOT USE TRANSACTIONS, SERIALIZABLE TRANSACTION SUPPORTED THROUGH STORED PROCEDURE

            DateTime lastModifiedDate = modifiedAccountInfo.ActionDate;


            try {

                // NO VALIDATION REQUIRED

                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("dbo.MemberCaseCarePlanGoal_AddCareIntervention");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseId", MemberCaseCarePlan.MemberCaseId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseCarePlanGoalId", Id);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careInterventionId", careInterventionId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@isSingleInstance", isSingleInstance);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@ignoreAssignedTo", false);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityId", application.Session.SecurityAuthorityId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityName", application.Session.SecurityAuthorityName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountId", application.Session.UserAccountId, Server.Data.DataTypeConstants.UserAccountId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountName", application.Session.UserAccountName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@lastModifiedDate", lastModifiedDate);


                // RETURNED MODIFIED DATE 

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedDate", ((DateTime?)null));

                ((System.Data.IDataParameter)sqlCommand.Parameters["@modifiedDate"]).Direction = System.Data.ParameterDirection.Output;


                // RETURN VALUE

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@RETURN", ((Int32)0));

                ((System.Data.IDataParameter)sqlCommand.Parameters["@RETURN"]).Direction = System.Data.ParameterDirection.ReturnValue;


                sqlCommand.ExecuteNonQuery ();

                outcome = (Enumerations.MemberCaseActionOutcome)(Convert.ToInt32 (((System.Data.IDataParameter)sqlCommand.Parameters["@RETURN"]).Value));

                MemberCaseCarePlan.MemberCase.SetApplicationException (outcome);

                if (outcome == Enumerations.MemberCaseActionOutcome.Success) { Load (id); } // ON SUCCESS - RELOAD OBJECT

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
