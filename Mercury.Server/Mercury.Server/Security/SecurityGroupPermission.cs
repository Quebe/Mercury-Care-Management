using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Security {

    [DataContract]
    public class SecurityGroupPermission {

        #region Private Properties

        [DataMember (Name = "SecurityAuthorityId")]
        private Int64 securityAuthorityId;

        [DataMember  (Name = "SecurityGroupId")]
        private String securityGroupId;

        [DataMember (Name = "PermissionId")]
        private Int64 permissionId;


        [DataMember (Name = "IsGranted")]
        private Boolean isGranted;

        [DataMember (Name = "IsDenied")]
        private Boolean isDenied;


        [DataMember (Name = "CreateAccountInfo")]
        private Mercury.Server.Data.AuthorityAccountStamp createAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp ();

        [DataMember (Name = "ModifiedAccountInfo")]
        private Mercury.Server.Data.AuthorityAccountStamp modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp ();

        #endregion


        #region Private Properties (IDataObject)

        private Boolean isNew = true;
        private Boolean isModified;

        #endregion 


        #region Public Properties

        public Int64 SecurityAuthorityId {
            get { return securityAuthorityId; }
            set { securityAuthorityId = value; isModified = true; }

        } // Property: SecurityAuthorityId 

        public String SecurityGroupId {
            get { return securityGroupId; }
            set { securityGroupId = value; isModified = true; }

        } // Property: SecurityGroupId 

        public Int64 PermissionId {
            get { return permissionId;  }
            set { permissionId = value; isModified = true; }

        } // Property: PermissionId

        public Boolean IsGranted {
            get { return isGranted;  }
            set { isGranted = value; isModified = true; }

        }

        public Boolean IsDenied {
            get { return isDenied;  }
            set { isDenied = value; isModified = true; } 

        }

        public Mercury.Server.Data.AuthorityAccountStamp CreateAccountInfo {
            get { return createAccountInfo; }
            set { createAccountInfo = value; }

        } // Property: CreateAccountInfo

        public Mercury.Server.Data.AuthorityAccountStamp ModifiedAccountInfo {
            get { return modifiedAccountInfo; }
            set { modifiedAccountInfo = value; }

        } // Property: ModifiedAccountInfo

        #endregion



        #region Public Properties (IDataObject)

        public Boolean IsNew {
            get { return isNew; }

        }

        public Boolean IsModified {
            get { return isModified; }

        }

        #endregion


        #region Constructors

        public SecurityGroupPermission () {

            return;
            
        }

        public SecurityGroupPermission (Mercury.Server.Data.SqlDatabase sqlDatabase, Int64 securityAuthorityId, String securityGroupId, Int64 permissionId) {

            if (!LoadFromDatabase (sqlDatabase, securityAuthorityId, securityGroupId, permissionId)) {

                throw new Exception ("Unable to load Security Group Permission.");

            }

        }

        #endregion


        #region Database Functions

        public Boolean LoadFromDatabaseWithCriteria (Mercury.Server.Data.SqlDatabase sqlDatabase, String criteria) {

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable tableEnvironment;

            selectStatement.Append ("SELECT * FROM SecurityGroupPermission ");
            selectStatement.Append (criteria);

            tableEnvironment = sqlDatabase.SelectDataTable (selectStatement.ToString ());

            if (tableEnvironment.Rows.Count == 1) {

                MapDataFields (tableEnvironment.Rows[0]);

                isModified = false;

                return true;

            }

            else {

                return false;

            }

        }

        public Boolean LoadFromDatabase (Mercury.Server.Data.SqlDatabase sqlDatabase, Int64 securityAuthorityId, String securityGroupId, Int64 permissionId) {

            String criteria;

            criteria = "WHERE SecurityAuthorityId = '" + securityAuthorityId+ "' AND SecurityGroupId = '" + securityGroupId + "' AND PermissionId = " + permissionId;

            return LoadFromDatabaseWithCriteria (sqlDatabase, criteria);

        }

        public void MapDataFields (System.Data.DataRow currentRow) {

            securityAuthorityId = (Int64) currentRow["SecurityAuthorityId"];
            securityGroupId = (String) currentRow["SecurityGroupId"];
            permissionId = (Int64) currentRow["PermissionId"];
            isGranted = (Boolean) currentRow["IsGranted"];
            isDenied = (Boolean) currentRow["IsDenied"];

            createAccountInfo.SecurityAuthorityName = (String) currentRow["CreateAuthorityName"];
            createAccountInfo.UserAccountId = (String) currentRow["CreateAccountId"];
            createAccountInfo.UserAccountName = (String) currentRow["CreateAccountName"];
            createAccountInfo.ActionDate = (DateTime) currentRow["CreateDate"];

            modifiedAccountInfo.SecurityAuthorityName = (String) currentRow["ModifiedAuthorityName"];
            modifiedAccountInfo.UserAccountId = (String) currentRow["ModifiedAccountId"];
            modifiedAccountInfo.UserAccountName = (String) currentRow["ModifiedAccountName"];
            modifiedAccountInfo.ActionDate = (DateTime) currentRow["ModifiedDate"];

        } // MapDataFields (System.Data.DataRow currentRow) 

        public Boolean Save (Mercury.Server.Data.SqlDatabase sqlDatabase) {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            sqlStatement.Append ("EXEC SecurityGroupPermission_InsertUpdate " + securityAuthorityId + ", '" + securityGroupId + "', " + permissionId + ", ");
            sqlStatement.Append (Convert.ToByte (isGranted) + ", " + Convert.ToByte (isDenied));
            sqlStatement.Append (", '" + modifiedAccountInfo.SecurityAuthorityName + "', '" + modifiedAccountInfo.UserAccountId + "', '" + modifiedAccountInfo.UserAccountName + "'");

            success = sqlDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

            return success;

        }

        #endregion


    }

}
