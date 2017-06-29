using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Environment {

    [DataContract (Name = "Environment")]
    public class Environment : Core.CoreObject {

        #region Private Properties
        
        [DataMember (Name = "EnvironmentTypeId")]
        private Int64 environmentTypeId = 0;

        [DataMember (Name = "EnvironmentTag")]
        private String environmentTag = String.Empty;


        [DataMember (Name = "ConfidentialityStatement")]
        private String confidentialityStatement = String.Empty;

        [DataMember (Name = "SqlConfiguration")]
        private Mercury.Server.Data.SqlConfiguration sqlConfiguration = new Mercury.Server.Data.SqlConfiguration ();

        #endregion


        #region Public Properties

        public Int64 EnvironmentTypeId { get { return environmentTypeId; } set { environmentTypeId = value; } }

        public String EnvironmentTag { get { return environmentTag; } set { environmentTag = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }


        public String ConfidentialityStatement { get { return confidentialityStatement; } set { confidentialityStatement = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Description); } }

        public String SqlServerName { get { return sqlConfiguration.ServerName; } }
        
        public String SqlDatabaseName { get { return sqlConfiguration.DatabaseName; } }

        public Mercury.Server.Data.SqlConfiguration SqlConfiguration { get { return sqlConfiguration; } set { sqlConfiguration = value; } }
           
        #endregion


        #region Constructors

        public Environment (Application applicationReference) {

            base.BaseConstructor (applicationReference);

            return;

        }

        public Environment (Application applicationReference, Int64 forEnvironmentId) {

            base.BaseConstructor (applicationReference, forEnvironmentId);

            return;

        }

        #endregion


        #region Database Functions

        public override Boolean Load (long forId) {

            if (application.EnterpriseDatabase == null) { return false; }


            Boolean success = true;

            String selectStatement = "SELECT * FROM dbo.Environment WHERE EnvironmentId = " + forId.ToString ();


            System.Data.DataTable table = application.EnterpriseDatabase.SelectDataTable (selectStatement);

            if (table.Rows.Count == 1) {

                MapDataFields (table.Rows[0]);

            }

            else { success = false; }


            return success;
            
        }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            EnvironmentTypeId = (Int64) currentRow["EnvironmentTypeId"];

            EnvironmentTag = (String) currentRow["EnvironmentTag"];

            
            ConfidentialityStatement = (String) currentRow["ConfidentialityStatement"];

            
            sqlConfiguration.ServerName = (String) currentRow["SqlServerName"];

            sqlConfiguration.DatabaseName = (String) currentRow["SqlDatabaseName"];

            
            sqlConfiguration.TrustedConnection = (Boolean) currentRow["UseTrustedConnection"];

            sqlConfiguration.UserName = (String) currentRow["SqlUserName"];

            sqlConfiguration.Password = (String) currentRow["SqlPassword"];



            sqlConfiguration.PoolingEnabled = (Boolean) currentRow["UseConnectionPooling"];

            sqlConfiguration.MinPoolSize = (Int32) currentRow["PoolSizeMinimum"];

            sqlConfiguration.MaxPoolSize = (Int32) currentRow["PoolSizeMaximum"];


            sqlConfiguration.CustomAttributes = (String) currentRow["CustomAttributes"];


            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            sqlStatement.Append ("EXEC Environment_InsertUpdate ");

            sqlStatement.Append (Id.ToString () + ", '" + NameSql + "', '" + ConfidentialityStatement.Replace ("'", "''") + "', ");

            sqlStatement.Append ("'" + SqlServerName + "', '" + SqlDatabaseName + "', ");

            sqlStatement.Append (Convert.ToByte (sqlConfiguration.TrustedConnection).ToString () + ", '" + sqlConfiguration.UserName + "', '" + sqlConfiguration.Password + "', ");

            sqlStatement.Append (Convert.ToByte (sqlConfiguration.PoolingEnabled).ToString () + ", " + sqlConfiguration.MinPoolSize.ToString () + ", " + sqlConfiguration.MaxPoolSize.ToString () + ", ");

            sqlStatement.Append ("'" + sqlConfiguration.CustomAttributes.Replace ("'", "''") + "', ");

            sqlStatement.Append ("'" + modifiedAccountInfo.SecurityAuthorityNameSql + "', '" + modifiedAccountInfo.UserAccountIdSql + "', '" + modifiedAccountInfo.UserAccountNameSql + "'");

            success = application.EnterpriseDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

            return success;

        }

        public Boolean Delete (Mercury.Server.Data.SqlDatabase sqlDatabase) {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            sqlStatement.Append ("EXEC Environment_Delete " + Id);

            success = sqlDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

            return success;

        }

        #endregion

    }
}
