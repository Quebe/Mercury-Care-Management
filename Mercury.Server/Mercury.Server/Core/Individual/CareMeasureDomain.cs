using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual {

    [DataContract (Name = "CareMeasureDomain")]
    public class CareMeasureDomain : CoreConfigurationObject {

        #region Constructors

        public CareMeasureDomain (Application applicationReference) {

            base.BaseConstructor (applicationReference);

            return;

        }

        public CareMeasureDomain (Application applicationReference, Int64 forId) {

            base.BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion


        #region Data Functions
        
        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            Boolean usingTransaction = (application.EnvironmentDatabase.OpenTransactions == 0);


            if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.CareMeasureManage)) { throw new ApplicationException ("Permission Denied [CareMeasureDomain.Save]."); }

            Dictionary<String, String> validationResponse = Validate ();

            if (validationResponse.Count != 0) {

                foreach (String validationKey in validationResponse.Keys) {

                    throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                }

            }

            modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);

            try {

                if (usingTransaction) { application.EnvironmentDatabase.BeginTransaction (); }


                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("CareMeasureDomain_InsertUpdate");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                
                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careMeasureDomainId", Id);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careMeasureDomainName", Name, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careMeasureDomainDescription", Description, Server.Data.DataTypeConstants.Description);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@enabled", Enabled);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@visible", Visible);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityName", ModifiedAccountInfo.SecurityAuthorityName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountId", ModifiedAccountInfo.UserAccountId, Server.Data.DataTypeConstants.UserAccountId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountName", ModifiedAccountInfo.UserAccountName, Server.Data.DataTypeConstants.Name);


                success = (sqlCommand.ExecuteNonQuery () > 0);

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
