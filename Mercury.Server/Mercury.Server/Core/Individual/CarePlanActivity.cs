using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual {

    [DataContract (Name = "CarePlanActivity")]
    public class CarePlanActivity : Activity.Activity {
        
        #region Private Properties

        [DataMember (Name = "CarePlanId")]
        private Int64 carePlanId = 0;

        [DataMember (Name = "CarePlanActivityType")]
        private Enumerations.CarePlanActivityType carePlanActivityType = Enumerations.CarePlanActivityType.Intervention;

        [DataMember (Name = "ClinicalNarrative")]
        private String clinicalNarrative;

        [DataMember (Name = "CommonNarrative")]
        private String commonNarrative;

        #endregion


        #region Public Properties

        public Int64 CarePlanId { get { return carePlanId; } set { carePlanId = value; } }

        public Enumerations.CarePlanActivityType CarePlanActivityType { get { return carePlanActivityType; } set { carePlanActivityType = value; } }

        public String ClinicalNarrative { get { return clinicalNarrative; } set { clinicalNarrative = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Description); } }

        public String CommonNarrative { get { return commonNarrative; } set { commonNarrative = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Description); } }

        #endregion


        #region Constructors

        protected CarePlanActivity () { /* DO NOTHING, FOR INHERITANCE */ }

        public CarePlanActivity (Application applicationReference) {

            ObjectConstructor (applicationReference);
            
            return;  
        
        }

        public CarePlanActivity (Application applicationReference, Int64 forId) {

            ObjectConstructor (applicationReference);

            base.BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion


        #region Validation

        public override Dictionary<string, string> Validate () {

            Dictionary<String, String> validationResponse = base.Validate ();


            if (String.IsNullOrWhiteSpace (clinicalNarrative)) { validationResponse.Add ("Clinical Narrative", "Empty or NULL."); }

            if (String.IsNullOrWhiteSpace (commonNarrative)) { validationResponse.Add ("Common Narrative", "Empty or NULL."); }


            return validationResponse;

        }

        #endregion 


        #region Data Functions

        public override Boolean Load (Int64 forId) {

            Boolean success = false;

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable dataTable;


            if (application.EnvironmentDatabase == null) { return false; }

            selectStatement.Append ("SELECT * FROM dbo.CarePlanActivity WHERE CarePlanActivityId = " + forId.ToString ());

            dataTable = application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString (), 0);

            if (dataTable.Rows.Count == 1) {

                MapDataFields (dataTable.Rows[0]);

                success = true;

            }

            else { success = false; }


            if (success) {

                // LOAD CHILD OBJECTS 

                String selectActivities = "SELECT * FROM dbo.CarePlanActivityThreshold WHERE CarePlanActivityId = " + forId.ToString ();

                dataTable = application.EnvironmentDatabase.SelectDataTable (selectActivities.ToString (), 0);

                foreach (System.Data.DataRow currentRow in dataTable.Rows) {

                    Activity.ActivityThreshold threshold = new Activity.ActivityThreshold (application);

                    threshold.MapDataFields (currentRow);

                    Thresholds.Add (threshold);

                }


            }

            return success;

        }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            carePlanId = Convert.ToInt64 (currentRow["CarePlanId"]);

            carePlanActivityType = (Enumerations.CarePlanActivityType)Convert.ToInt32 (currentRow["CarePlanActivityType"]);

            clinicalNarrative = (String)currentRow["ClinicalNarrative"];

            commonNarrative = (String)currentRow["CommonNarrative"];


            return;

        }

        public override Boolean Save () {

            Boolean useTransaction = (application.EnvironmentDatabase.OpenTransactions == 0); // NO NESTED TRANSACTIONS

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            String childIds = String.Empty;


            if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.CarePlanManage)) { throw new ApplicationException ("Permission Denied."); }

            Dictionary<String, String> validationResponse = Validate ();

            if (validationResponse.Count != 0) {

                foreach (String validationKey in validationResponse.Keys) {

                    throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                }

            }

            modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);

            try {

                if (useTransaction) { application.EnvironmentDatabase.BeginTransaction (); }

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dbo.CarePlanActivity_InsertUpdate ");

                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append ("'" + NameSql + "', ");

                sqlStatement.Append ("'" + DescriptionSql + "', ");

                sqlStatement.Append (CarePlanId.ToString () + ", ");

                
                sqlStatement.Append (((Int32)CarePlanActivityType).ToString () + ", ");

                sqlStatement.Append ("'" + clinicalNarrative.Replace ("'", "''") + "', ");

                sqlStatement.Append ("'" + commonNarrative.Replace ("'", "''") + "', ");


                sqlStatement.Append (((Int32)ActivityType).ToString () + ", ");

                sqlStatement.Append (Reoccurring.ToString () + ", ");

                sqlStatement.Append (((Int32)InitialAnchorDate).ToString () + ", ");

                sqlStatement.Append (((Int32)AnchorDate).ToString () + ", ");


                sqlStatement.Append (((Int32)ScheduleType).ToString () + ", ");

                sqlStatement.Append (ScheduleValue.ToString () + ", ");

                sqlStatement.Append (((Int32)ScheduleQualifier).ToString () + ", ");

                sqlStatement.Append (ConstraintValue.ToString () + ", ");

                sqlStatement.Append (((Int32)ConstraintQualifier).ToString () + ", ");


                sqlStatement.Append (((Int32)PerformActionDate).ToString () + ", ");

                if (Action == null) { sqlStatement.Append ("NULL, NULL, NULL, "); }

                else {

                    sqlStatement.Append (Action.Id.ToString () + ", ");

                    sqlStatement.Append ("'" + Action.ActionParametersXmlSqlParsedString + "', ");

                    sqlStatement.Append ("'" + Action.Description + "', ");

                }


                sqlStatement.Append (modifiedAccountInfo.AccountInfoSql);


                success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }

                SetIdentity ();

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }


                String deleteStatement = "DELETE FROM CarePlanActivityThreshold WHERE CarePlanActivityId = " + Id.ToString ();

                success = application.EnvironmentDatabase.ExecuteSqlStatement (deleteStatement);


                foreach (Activity.ActivityThreshold currentThreshold in Thresholds) {

                    currentThreshold.ActivityId = Id;

                    currentThreshold.Application = application;


                    sqlStatement = new StringBuilder ();

                    sqlStatement.Append ("EXEC dbo.CarePlanActivityThreshold_InsertUpdate ");

                    sqlStatement.Append (currentThreshold.Id.ToString () + ", ");

                    sqlStatement.Append ("'" + currentThreshold.Name.Replace ("'", "''") + "', ");

                    sqlStatement.Append ("'" + currentThreshold.Description.Replace ("'", "''") + "', ");

                    sqlStatement.Append (Id.ToString () + ", ");


                    sqlStatement.Append (currentThreshold.RelativeDateValue.ToString () + ", ");

                    sqlStatement.Append (((Int32)currentThreshold.RelativeDateQualifier).ToString () + ", ");

                    sqlStatement.Append (((Int32)currentThreshold.Status).ToString () + ", ");


                    if (Action == null) { sqlStatement.Append ("NULL, NULL, NULL, "); }

                    else {

                        sqlStatement.Append (Action.Id.ToString () + ", ");

                        sqlStatement.Append ("'" + Action.ActionParametersXmlSqlParsedString + "', ");

                        sqlStatement.Append ("'" + Action.Description + "', ");

                    }


                    sqlStatement.Append (modifiedAccountInfo.AccountInfoSql);


                    success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                    if (!success) {

                        application.SetLastException (application.EnvironmentDatabase.LastException);

                        throw application.EnvironmentDatabase.LastException;

                    }

                    currentThreshold.SetIdentity ();

                }


                success = true;

                if (useTransaction) { application.EnvironmentDatabase.CommitTransaction (); }

            }

            catch (Exception applicationException) {

                success = false;

                if (useTransaction) { application.EnvironmentDatabase.RollbackTransaction (); }

                application.SetLastException (applicationException);

            }

            return success;

        }
   
        #endregion

    }

}
