using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Printing {

    [DataContract (Name = "Printer")]
    public class Printer : Core.CoreConfigurationObject {


        #region Private Properties

        [DataMember (Name = "PrintServerName")]
        private String printServerName = String.Empty;

        [DataMember (Name = "PrintQueueName")]
        private String printQueueName = String.Empty;

        #endregion 

        

        #region Public Properties

        public String PrintServerName { get { return printServerName; } set { printServerName = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public String PrintQueueName { get { return printQueueName; } set { printQueueName = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }
        
        #endregion


        #region Constructors

        public Printer (Application applicationReference) { BaseConstructor (applicationReference); return; }

        public Printer (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference, forId);

            return;

        }
        
        #endregion


        #region Validation Functions

        public override Dictionary<String, String> Validate () {

            Dictionary<String, String> validationResponse = base.Validate ();


            // VALIDATE ASSEMBLY PATH IS NOT EMPTY
            PrintServerName = PrintServerName;

            if (String.IsNullOrWhiteSpace (PrintServerName)) { validationResponse.Add ("Print Server Name", "Empty or Null."); }

            // VALIDATE ASSEMBLY NAME IS NOT EMPTY
            PrintQueueName = PrintQueueName;

            if (String.IsNullOrWhiteSpace (PrintQueueName)) { validationResponse.Add ("Print Queue Name", "Empty or Null."); }
            

            return validationResponse;

        }

        #endregion


        #region Database Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            printServerName = (String)currentRow["PrintServerName"];

            printQueueName = (String)currentRow["PrintQueueName"];


            return;

        }

        public override Boolean Save () {
            
            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();


            try {

                if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.PrinterManage)) { throw new ApplicationException ("PermissionDenied"); }

                Dictionary<String, String> validationResponse = Validate ();

                if (validationResponse.Count != 0) {

                    foreach (String validationKey in validationResponse.Keys) {

                        throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                    }

                }


                modifiedAccountInfo = new Data.AuthorityAccountStamp (application.Session);


                application.EnvironmentDatabase.BeginTransaction ();

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dbo.Printer_InsertUpdate ");


                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append ("'" + NameSql + "', ");

                sqlStatement.Append ("'" + DescriptionSql + "', ");


                sqlStatement.Append ("'" + printServerName.Replace ("'", "''") + "', ");

                sqlStatement.Append ("'" + printQueueName.Replace ("'", "''") + "', ");

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
