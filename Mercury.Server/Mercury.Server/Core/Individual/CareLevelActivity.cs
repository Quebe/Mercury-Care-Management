using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual {

    [DataContract (Name = "CareLevelActivity")]
    public class CareLevelActivity : Core.Activity.Activity {

        #region Private Properties

        [DataMember (Name = "CareLevelId")]
        private Int64 careLevelId;

        #endregion


        #region Public Properties
        
        public Int64 CareLevelId { get { return careLevelId; } set { careLevelId = value; } }

        #endregion


        #region Constructors
        
        public CareLevelActivity (Application applicationReference) {

            ObjectConstructor (applicationReference);
            
            return;  
        
        }

        public CareLevelActivity (Application applicationReference, Int64 forId) {

            ObjectConstructor (applicationReference);

            base.BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion


        #region Data Functions

        public override Boolean Load (Int64 forId) {

            Boolean success = false;

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable dataTable;


            if (application.EnvironmentDatabase == null) { return false; }

            selectStatement.Append ("SELECT * FROM dbo.CareLevelActivity WHERE CareLevelActivityId = " + forId.ToString ());

            dataTable = application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString (), 0);

            if (dataTable.Rows.Count == 1) {

                MapDataFields (dataTable.Rows[0]);

                success = true;

            }

            else { success = false; }


            if (success) {

                // LOAD CHILD OBJECTS 

                String selectActivities = "SELECT * FROM dbo.CareLevelActivityThreshold WHERE CareLevelActivityId = " + forId.ToString ();

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


            careLevelId = Convert.ToInt64 (currentRow["CareLevelId"]);


            return;

        }

        public override Boolean Save () {

            Boolean useTransaction = (application.EnvironmentDatabase.OpenTransactions == 0); // NO NESTED TRANSACTIONS

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            String childIds = String.Empty;


            if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.CareLevelManage)) { throw new ApplicationException ("Permission Denied."); }

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

                sqlStatement.Append ("EXEC dbo.CareLevelActivity_InsertUpdate ");

                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append ("'" + NameSql + "', ");

                sqlStatement.Append ("'" + DescriptionSql + "', ");

                sqlStatement.Append (CareLevelId.ToString () + ", ");


                sqlStatement.Append (((Int32) ActivityType).ToString () + ", ");

                sqlStatement.Append (Reoccurring.ToString () + ", ");

                sqlStatement.Append (((Int32)InitialAnchorDate).ToString () + ", ");

                sqlStatement.Append (((Int32)AnchorDate).ToString () + ", ");


                sqlStatement.Append (((Int32) ScheduleType).ToString () + ", ");

                sqlStatement.Append (ScheduleValue.ToString () + ", ");

                sqlStatement.Append (((Int32)ScheduleQualifier).ToString () + ", ");

                sqlStatement.Append (ConstraintValue.ToString () + ", ");

                sqlStatement.Append (((Int32)ConstraintQualifier).ToString () + ", ");


                sqlStatement.Append (((Int32) PerformActionDate).ToString () + ", ");

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


                String deleteStatement = "DELETE FROM CareLevelActivityThreshold WHERE CareLevelActivityId = " + Id.ToString ();

                success = application.EnvironmentDatabase.ExecuteSqlStatement (deleteStatement);


                foreach (Activity.ActivityThreshold currentThreshold in Thresholds) {

                    currentThreshold.ActivityId = Id;

                    currentThreshold.Application = application;


                    sqlStatement = new StringBuilder ();

                    sqlStatement.Append ("EXEC dbo.CareLevelActivityThreshold_InsertUpdate ");

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
