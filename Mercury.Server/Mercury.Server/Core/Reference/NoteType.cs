using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Reference {

    [Serializable]
    [DataContract (Name = "NoteType")]
    public class NoteType : CoreConfigurationObject {

        #region Constructors

        public NoteType (Application applicationReference) {

            base.BaseConstructor (applicationReference);

            return;

        }

        public NoteType (Application applicationReference, Int64 forId) {

            base.BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion

        
        #region Data Functions
        
        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            String childIds = String.Empty;


            if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.NoteTypeManage)) { throw new ApplicationException ("Permission Denied."); }

            Dictionary<String, String> validationResponse = Validate ();

            if (validationResponse.Count != 0) {

                foreach (String validationKey in validationResponse.Keys) {

                    throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                }

            }

            modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);

            try {

                application.EnvironmentDatabase.BeginTransaction ();

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dbo.NoteType_InsertUpdate ");

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


                if (id == 0) { // RESET DOCUMENT ID CRITERIA

                    Object identity = application.EnvironmentDatabase.ExecuteScalar ("SELECT @@IDENTITY").ToString ();

                    if (!Int64.TryParse ((String) identity, out id)) {

                        throw new ApplicationException ("Unable to retreive unique id.");

                    }

                }

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

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
