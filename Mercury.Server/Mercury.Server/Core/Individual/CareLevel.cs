using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual {

    [DataContract (Name = "CareLevel")]
    public class CareLevel : CoreConfigurationObject {

        #region Private Properties

        [DataMember (Name = "Activities")]
        private List<CareLevelActivity> activities = new List<CareLevelActivity> ();

        #endregion 


        #region Public Properties

        public List<CareLevelActivity> Activities { get { return activities; } set { activities = value; } }

        #endregion 


        #region Public Properties

        public override Application Application {

            set {

                base.Application = value;

                // PROPOGATE: SET ALL CHILD REFERENCES

                if (activities == null) { activities = new List<CareLevelActivity> (); }

                foreach (CareLevelActivity currentActivity in activities) { currentActivity.Application = value; }
                
            }

        }

        #endregion


        #region Constructors

        public CareLevel (Application applicationReference) {

            BaseConstructor (applicationReference);
            
            return;  
        
        }

        public CareLevel (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference);
            

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

            selectStatement.Append ("SELECT * FROM dbo.CareLevel WHERE CareLevelId = " + forId.ToString ());

            dataTable = application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString (), 0);

            if (dataTable.Rows.Count == 1) {

                MapDataFields (dataTable.Rows[0]);

                success = true;

            }

            else { success = false; }


            if (success) {

                // LOAD CHILD OBJECTS 

                String selectActivities = "SELECT * FROM dbo.CareLevelActivity WHERE CareLevelId = " + forId.ToString ();
                
                dataTable = application.EnvironmentDatabase.SelectDataTable (selectActivities.ToString (), 0);

                foreach (System.Data.DataRow currentRow in dataTable.Rows) { 

                    CareLevelActivity careLevelActivity = new CareLevelActivity (application, Convert.ToInt64 (currentRow ["CareLevelActivityId"]));

                    activities.Add (careLevelActivity);

                }


            }

            return success;

        }

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            String childIds = String.Empty;


            try {

                if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.CareLevelManage)) { throw new ApplicationException ("Permission Denied."); }

                Dictionary<String, String> validationResponse = Validate ();

                if (validationResponse.Count != 0) {

                    foreach (String validationKey in validationResponse.Keys) {

                        throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                    }

                }

                modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);

                application.EnvironmentDatabase.BeginTransaction ();

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dbo.CareLevel_InsertUpdate ");

                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append ("'" + NameSql + "', ");

                sqlStatement.Append ("'" + DescriptionSql + "', ");


                sqlStatement.Append ("'" + ExtendedPropertiesSql + "', ");

                sqlStatement.Append (Convert.ToInt32 (Enabled).ToString () + ", ");

                sqlStatement.Append (Convert.ToInt32 (Visible).ToString () + ", ");


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


                foreach (CareLevelActivity currentCareLevelActivity in activities) {

                    if (currentCareLevelActivity.Id != 0) {

                        childIds += currentCareLevelActivity.Id.ToString () + ", ";

                    }

                }

                if (!String.IsNullOrWhiteSpace (childIds)) { // DELETE ALL REMOVED CHILD IDS FROM DATABASE 

                    childIds += "0";

                    String deleteStatement = "DELETE FROM CareLevelActivity WHERE CareLevelId = " + Id.ToString () + " AND CareLevelActivityId NOT IN (" + childIds + ")";

                    success = application.EnvironmentDatabase.ExecuteSqlStatement (deleteStatement);

                }

                foreach (CareLevelActivity currentCareLevelActivity in activities) {

                    currentCareLevelActivity.CareLevelId = Id;

                    currentCareLevelActivity.Application = application;

                    success = currentCareLevelActivity.Save ();

                    if (!success) {

                        application.SetLastException (application.EnvironmentDatabase.LastException);

                        throw application.EnvironmentDatabase.LastException;

                    }

                }


                success = true;

                application.EnvironmentDatabase.CommitTransaction ();

            }

            catch (Exception applicationException) {

                success = false;

                application.EnvironmentDatabase.RollbackTransaction ();

                application.SetLastException (applicationException);

            }

            return success;

        }

        #endregion

    }

}
