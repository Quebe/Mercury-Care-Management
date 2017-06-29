using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual.Case {

    [DataContract (Name = "MemberCaseCarePlan")]
    public class MemberCaseCarePlan : CoreConfigurationObject {

        #region Private Properties

        [DataMember (Name = "MemberCaseId")]
        private Int64 memberCaseId;

        private MemberCase memberCase = null;

        [DataMember (Name = "CarePlanId")]
        private Int64 carePlanId;           // ORIGINAL CARE PLAN ID (BASELINE)

        [DataMember (Name = "Status")]
        private Enumerations.CaseItemStatus status = Enumerations.CaseItemStatus.UnderDevelopment;


        [DataMember (Name = "AddedDate")]
        private DateTime addedDate;

        [DataMember (Name = "EffectiveDate")]
        private DateTime effectiveDate;

        [DataMember (Name = "TerminationDate")]
        private DateTime terminationDate;

        [DataMember (Name = "CareOutcomeId")]
        private Int64 careOutcomeId = 0;


        [DataMember (Name = "Goals")]
        private List<MemberCaseCarePlanGoal> goals = new List<MemberCaseCarePlanGoal> ();

        #endregion 


        #region Public Properties

        public Int64 MemberCaseId { get { return memberCaseId; } set { memberCaseId = value; } }

        public MemberCase MemberCase { get { return memberCase; } set { memberCase = value; } }

        public Int64 CarePlanId { get { return carePlanId; } set { carePlanId = value; } }

        public Enumerations.CaseItemStatus Status { get { return status; } set { status = value; } }


        public DateTime AddedDate { get { return addedDate; } set { addedDate = value; } }

        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }

        public Int64 CareOutcomeId { get { return careOutcomeId; } set { careOutcomeId = value; } }


        public List<MemberCaseCarePlanGoal> Goals { get { return goals; } set { goals = value; } }

        #endregion 

        
        #region Public Properties

        public override Application Application {

            set {

                base.Application = value;


                // PROPOGATE: SET ALL CHILD REFERENCES

                if (goals == null) { goals = new List<MemberCaseCarePlanGoal> (); }

                foreach (MemberCaseCarePlanGoal currentCarePlanGoal in goals) { 
                    
                    currentCarePlanGoal.Application = value;

                    currentCarePlanGoal.MemberCaseCarePlan = this;
                
                }


            }

        }

        #endregion


        #region Constructors

        public MemberCaseCarePlan (Application applicationReference) {

            BaseConstructor (applicationReference);

            return; 
        
        }

        public MemberCaseCarePlan (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion


        #region Data Functions

        public override Boolean LoadChildObjects () {

            System.Data.DataTable dataTable;

            Boolean success = base.LoadChildObjects ();


            if (success) {

                // LOAD CHILD OBJECTS 

                String selectGoals = "SELECT * FROM dbo.MemberCaseCarePlanGoal WHERE MemberCaseCarePlanId = " + Id.ToString ();

                dataTable = application.EnvironmentDatabase.SelectDataTable (selectGoals.ToString (), 0);

                foreach (System.Data.DataRow currentRow in dataTable.Rows) {

                    MemberCaseCarePlanGoal carePlanGoal = new MemberCaseCarePlanGoal (application);

                    carePlanGoal.MapDataFields (currentRow);

                    carePlanGoal.LoadChildObjects ();

                    carePlanGoal.MemberCaseCarePlan = this;

                    goals.Add (carePlanGoal);

                }
               
            }

            return success;

        }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            MemberCaseId = base.IdFromSql (currentRow, "MemberCaseId");

            CarePlanId = base.IdFromSql (currentRow, "CarePlanId");

            Status = (Enumerations.CaseItemStatus)Convert.ToInt32 (currentRow["Status"]);

            AddedDate  = (DateTime)currentRow["AddedDate"];
            
            EffectiveDate = (DateTime)currentRow["EffectiveDate"];

            TerminationDate = (DateTime)currentRow["TerminationDate"];

            careOutcomeId = base.IdFromSql (currentRow, "CareOutcomeId");
                      
            return;

        } // MapDataFields (System.Data.DataRow currentRow) 
        
        #endregion 


        #region Public Methods

        public Boolean ContainsGoal (Int64 forCarePlanGoalId) {

            Boolean containsGoal = false;


            foreach (MemberCaseCarePlanGoal currentCarePlanGoal in Goals) {

                if (currentCarePlanGoal.Id == forCarePlanGoalId) {

                    containsGoal  =  true;

                    break;

                }

            }

            return containsGoal;

        }

        public MemberCaseCarePlanGoal Goal (Int64 forCarePlanGoalId) {

            MemberCaseCarePlanGoal goal = null;


            foreach (MemberCaseCarePlanGoal currentCarePlanGoal in Goals) {

                if (currentCarePlanGoal.Id == forCarePlanGoalId) {

                    goal = currentCarePlanGoal;

                    break;

                }

            }

            return goal;

        }


        public Enumerations.MemberCaseActionOutcome DeleteGoal (Int64 forCarePlanGoalId) {
            
            // BUSINESS RULES FOR ADDING A PROBLEM STATEMENT TO A MEMBER CASE
            			
            // TODO: DOCUMENT BUSINESS RULES

            Enumerations.MemberCaseActionOutcome outcome = Enumerations.MemberCaseActionOutcome.UnknownError;

            // DO NOT USE TRANSACTIONS, SERIALIZABLE TRANSACTION SUPPORTED THROUGH STORED PROCEDURE
            
            DateTime lastModifiedDate = modifiedAccountInfo.ActionDate;

            try {

                // NO VALIDATION REQUIRED

                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("dbo.MemberCaseCarePlanGoal_Delete");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseCarePlanGoalId", forCarePlanGoalId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@ignoreAssignedTo", false);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityId", application.Session.SecurityAuthorityId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityName", application.Session.SecurityAuthorityName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountId", application.Session.UserAccountId, Server.Data.DataTypeConstants.UserAccountId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountName", application.Session.UserAccountName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@lastModifiedDate", lastModifiedDate);


                // RETURNED MODIFIED DATE 

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedDate", ((DateTime?)null));

                ((System.Data.IDataParameter) sqlCommand.Parameters["@modifiedDate"]).Direction = System.Data.ParameterDirection.Output;


                // RETURN VALUE

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@RETURN", ((Int32)0));

                ((System.Data.IDataParameter) sqlCommand.Parameters["@RETURN"]).Direction = System.Data.ParameterDirection.ReturnValue;


                sqlCommand.ExecuteNonQuery ();

                outcome = (Enumerations.MemberCaseActionOutcome)(Convert.ToInt32 (((System.Data.IDataParameter) sqlCommand.Parameters["@RETURN"]).Value));

                MemberCase.SetApplicationException (outcome, "Care Plan Goal");

                if (outcome == Enumerations.MemberCaseActionOutcome.Success) { Load (id); } // ON SUCCESS - RELOAD OBJECT

            }

            catch (Exception applicationException) {

                outcome = Enumerations.MemberCaseActionOutcome.UnknownError;

                base.application.SetLastException (applicationException);

            }

            return outcome;

        }

        public Enumerations.MemberCaseActionOutcome AddGoal (Int64 copyCarePlanGoalId, String carePlanGoalName, Int64 careMeasureId) {

            // BUSINESS RULES FOR ADDING A PROBLEM STATEMENT TO A MEMBER CASE

            // TODO: DOCUMENT BUSINESS RULES

            Enumerations.MemberCaseActionOutcome outcome = Enumerations.MemberCaseActionOutcome.UnknownError;

            // DO NOT USE TRANSACTIONS, SERIALIZABLE TRANSACTION SUPPORTED THROUGH STORED PROCEDURE

            DateTime lastModifiedDate = modifiedAccountInfo.ActionDate; // LAST MODIFIED DATE IS SET TO CARE PLAN MODIFIED DATE

            try {

                // NO VALIDATION REQUIRED

                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("dbo.MemberCaseCarePlanGoal_Add");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseCarePlanId", Id);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@copyCarePlanGoalId", copyCarePlanGoalId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@carePlanGoalName", carePlanGoalName, 60);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careMeasureId", careMeasureId);


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

                MemberCase.SetApplicationException (outcome, "Care Plan Goal");

                if (outcome == Enumerations.MemberCaseActionOutcome.Success) { Load (id); } // ON SUCCESS - RELOAD OBJECT

            }

            catch (Exception applicationException) {

                outcome = Enumerations.MemberCaseActionOutcome.UnknownError;

                base.application.SetLastException (applicationException);

            }

            return outcome;

        }


        public Enumerations.MemberCaseActionOutcome DeleteIntervention (Int64 forCarePlanInterventionId) {

            // BUSINESS RULES FOR ADDING A PROBLEM STATEMENT TO A MEMBER CASE

            // TODO: DOCUMENT BUSINESS RULES

            Enumerations.MemberCaseActionOutcome outcome = Enumerations.MemberCaseActionOutcome.UnknownError;

            // DO NOT USE TRANSACTIONS, SERIALIZABLE TRANSACTION SUPPORTED THROUGH STORED PROCEDURE

            DateTime lastModifiedDate = modifiedAccountInfo.ActionDate;

            try {

                // NO VALIDATION REQUIRED

                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("dbo.MemberCaseCarePlanIntervention_Delete");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseCarePlanInterventionId", forCarePlanInterventionId);

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

                MemberCase.SetApplicationException (outcome, "Care Plan Intervention");

                if (outcome == Enumerations.MemberCaseActionOutcome.Success) { Load (id); } // ON SUCCESS - RELOAD OBJECT

            }

            catch (Exception applicationException) {

                outcome = Enumerations.MemberCaseActionOutcome.UnknownError;

                base.application.SetLastException (applicationException);

            }

            return outcome;

        }

        public Enumerations.MemberCaseActionOutcome AddIntervention (Int64 copyCareInterventionId, String carePlanInterventionName) {

            // BUSINESS RULES FOR ADDING A PROBLEM STATEMENT TO A MEMBER CASE

            // TODO: DOCUMENT BUSINESS RULES

            Enumerations.MemberCaseActionOutcome outcome = Enumerations.MemberCaseActionOutcome.UnknownError;

            // DO NOT USE TRANSACTIONS, SERIALIZABLE TRANSACTION SUPPORTED THROUGH STORED PROCEDURE

            DateTime lastModifiedDate = modifiedAccountInfo.ActionDate;

            try {

                // NO VALIDATION REQUIRED

                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("dbo.MemberCaseCarePlanIntervention_Add");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseCarePlanId", Id);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@copyCareInterventionId", copyCareInterventionId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@carePlanInterventionName", carePlanInterventionName, 60);


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

                MemberCase.SetApplicationException (outcome, "Care Plan Intervention");

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
