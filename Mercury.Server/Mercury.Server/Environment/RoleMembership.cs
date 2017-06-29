using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Environment {

    [DataContract (Name = "EnvironmentRoleMembership")]
    public class RoleMembership {

        #region Private Properties

        [DataMember (Name = "RoleId")]
        private Int64 roleId;

        [DataMember (Name = "SecurityAuthorityId")]
        private Int64 securityAuthorityId;

        [DataMember (Name = "SecurityGroupId")]
        private String securityGroupId;

        [DataMember (Name = "SecurityAuthorityName")]
        private String securityAuthorityName;

        [DataMember (Name = "SecurityGroupName")]
        private String securityGroupName = String.Empty;

        [DataMember (Name = "CreateAccountInfo")]
        private Mercury.Server.Data.AuthorityAccountStamp createAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp ();

        [DataMember (Name = "ModifiedAccountInfo")]
        private Mercury.Server.Data.AuthorityAccountStamp modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp ();

        private Application application;

        #endregion


        #region Public Properties

        public Int64 RoleId { get { return roleId; } set { roleId = value; } }

        public Int64 SecurityAuthorityId { get { return securityAuthorityId; } set { securityAuthorityId = value; } }

        public String SecurityGroupId { get { return securityGroupId; } set { securityGroupId = value; } }

        public Mercury.Server.Data.AuthorityAccountStamp CreateAccountInfo {
            
            get {

                if (createAccountInfo == null) { createAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (); }

                return createAccountInfo; 
            
            } 
            
            set { createAccountInfo = value; } 
        
        } // Property: CreateAccountInfo

        public Mercury.Server.Data.AuthorityAccountStamp ModifiedAccountInfo { 
            
            get {

                if (modifiedAccountInfo == null) { modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (); }

                return modifiedAccountInfo; 
            
            }
            
            set { modifiedAccountInfo = value; } 
        
        } // Property: ModifiedAccountInfo

        #endregion 


        #region Constructors

        public RoleMembership (Application application) {

            this.application = application;

            return;

        }

        #endregion


        #region Data Functions

        public void MapDataFields (System.Data.DataRow currentRow) {

            Server.Security.SecurityAuthority securityAuthority;


            roleId = (Int64) currentRow["RoleId"];

            securityAuthorityId = (Int64) currentRow["SecurityAuthorityId"];

            securityGroupId = (String) currentRow["SecurityGroupId"];

            CreateAccountInfo.MapDataFields (currentRow, "Create");

            ModifiedAccountInfo.MapDataFields (currentRow, "Modified");

            if (application != null) {

                securityAuthority = application.SecurityAuthorityGet (securityAuthorityId);

                securityAuthorityName = securityAuthority.Name;

                // securityGroupName = application.SecurityGroupGet (application.GetSecurityAuthorityProvider (securityAuthority), securityGroupId).SecurityGroupName;

            }

            return;

        }

        public Boolean Save (Mercury.Server.Data.SqlDatabase environmentDatabase) {

            Boolean success = false;		// initially set to false;

            StringBuilder sqlStatement = new StringBuilder ();
            
            sqlStatement.Append ("EXEC dbo.RoleMembership_InsertUpdate ");

            sqlStatement.Append (roleId.ToString () + ", ");

            sqlStatement.Append (securityAuthorityId.ToString () + ", ");

            sqlStatement.Append ("'" + securityGroupId.ToString () + "', ");

            sqlStatement.Append ("'" + ModifiedAccountInfo.SecurityAuthorityNameSql + "', '" + ModifiedAccountInfo.UserAccountIdSql + "', '" + ModifiedAccountInfo.UserAccountNameSql + "'");

            success = environmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

            return success;


        }

        public Boolean Delete (Mercury.Server.Data.SqlDatabase environmentDatabase) {

            Boolean success = false;		// initially set to false;

            StringBuilder sqlStatement = new StringBuilder ();


            sqlStatement.Append ("DELETE FROM dbo.RoleMembership WHERE RoleId = " + roleId.ToString ());

            sqlStatement.Append ("  AND SecurityAuthorityId = " + securityAuthorityId.ToString ());

            sqlStatement.Append ("  AND SecurityGroupId = '" + securityGroupId.ToString () + "'");

            success = environmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());


            return success;

        }

        #endregion


        #region Public Methods

        public Boolean IsEqual (RoleMembership compareMembership) {

            Boolean isEqual = true;

            if (this.roleId != compareMembership.RoleId) { isEqual = false; }

            if (this.securityAuthorityId != compareMembership.SecurityAuthorityId) { isEqual = false; }

            if (this.securityGroupId != compareMembership.SecurityGroupId) { isEqual = false; }

            return isEqual;

        }

        #endregion

    }

}
