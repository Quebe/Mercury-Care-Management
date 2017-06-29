using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual {

    [DataContract (Name = "ProblemCategory")]
    public class ProblemCategory : CoreConfigurationObject {

        #region Constructors

        public ProblemCategory (Application applicationReference) {

            base.BaseConstructor (applicationReference);

            return;

        }

        public ProblemCategory (Application applicationReference, Int64 forId) {

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

            selectStatement.Append ("SELECT * FROM dbo.ProblemCategory WHERE ProblemCategoryId = " + forId.ToString ());

            dataTable = application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString (), 0);

            if (dataTable.Rows.Count == 1) {

                MapDataFields (dataTable.Rows[0]);

                success = true;

            }

            else { success = false; }


            if (success) {

                // LOAD CHILD OBJECTS 

            }

            return success;

        }

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            Boolean usingTransaction = (application.EnvironmentDatabase.OpenTransactions == 0);


            if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.ProblemStatementManage)) { throw new ApplicationException ("Permission Denied [ProblemCategory.Save]."); }

            Dictionary<String, String> validationResponse = Validate ();

            if (validationResponse.Count != 0) {

                foreach (String validationKey in validationResponse.Keys) {

                    throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                }

            }

            modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);

            try {

                if (usingTransaction) { application.EnvironmentDatabase.BeginTransaction (); }

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dbo.ProblemCategory_InsertUpdate ");

                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append ("'" + NameSql + "', ");

                sqlStatement.Append ("'" + DescriptionSql + "', ");


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


                success = true;

                if (usingTransaction) { application.EnvironmentDatabase.CommitTransaction (); }

            }

            catch (Exception applicationException) {

                success = false;

                if (usingTransaction) { application.EnvironmentDatabase.RollbackTransaction (); }

                application.SetLastException (applicationException);

            }

            return success;

        }


        #endregion

    }

}
