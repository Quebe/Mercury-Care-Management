using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Environment {

    [DataContract (Name = "EnvironmentRolePermission")]
    public class RolePermission {

        #region Private Properties

        [DataMember (Name = "RoleId")]
        private Int64 roleId;

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


        #region Public Properties

        public Int64 RoleId { get { return roleId; } set { roleId = value; } }

        public Int64 PermissionId { get { return permissionId; } set { permissionId = value; } }

        public Boolean IsGranted { get { return isGranted; } set { isGranted = value; }  }

        public Boolean IsDenied { get { return isDenied; } set { isDenied = value; }  }

        public Mercury.Server.Data.AuthorityAccountStamp CreateAccountInfo { get { return createAccountInfo; } set { createAccountInfo = value; } } // Property: CreateAccountInfo

        public Mercury.Server.Data.AuthorityAccountStamp ModifiedAccountInfo { get { return modifiedAccountInfo; } set { modifiedAccountInfo = value; } } // Property: ModifiedAccountInfo

        #endregion 


        #region Constructors

        #endregion


        #region Data Functions

        public void MapDataFields (System.Data.DataRow currentRow) {

            roleId = (Int64) currentRow["RoleId"];

            permissionId = (Int64) currentRow["PermissionId"];

            isGranted = (Boolean) currentRow["IsGranted"];

            isDenied = (Boolean) currentRow["IsDenied"];

            CreateAccountInfo.MapDataFields (currentRow, "Create");

            ModifiedAccountInfo.MapDataFields (currentRow, "Modified");

            return;

        }

        public Boolean Save (Mercury.Server.Data.SqlDatabase environmentDatabase) {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            sqlStatement.Append ("EXEC RolePermission_InsertUpdate ");

            sqlStatement.Append (roleId.ToString () + ", ");

            sqlStatement.Append (permissionId.ToString () + ", ");

            sqlStatement.Append (Convert.ToByte (isGranted) + ", ");

            sqlStatement.Append (Convert.ToByte (isDenied) + ", ");

            sqlStatement.Append ("'" + modifiedAccountInfo.SecurityAuthorityNameSql + "', '" + modifiedAccountInfo.UserAccountIdSql + "', '" + modifiedAccountInfo.UserAccountNameSql + "'");

            success = environmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

            return success;

        }

        #endregion 

    }

}
