using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using Mercury.Server.Data;

namespace Mercury.Server.Environment {

    [DataContract (Name="EnvironmentRole")]
    public class Role {

        #region Private Properties

        [DataMember (Name = "RoleId")]
        private Int64 roleId;

        [DataMember (Name = "Name")]
        private String roleName; 

        [DataMember (Name = "Description")]
        private String description;

        [DataMember (Name = "Permissions")]
        private List<RolePermission> permissions = new List<RolePermission> ();

        [DataMember (Name = "Membership")]
        private List<RoleMembership> membership = new List<RoleMembership> ();

        [DataMember (Name = "CreateAccountInfo")]
        private Mercury.Server.Data.AuthorityAccountStamp createAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp ();

        [DataMember (Name = "ModifiedAccountInfo")]
        private Mercury.Server.Data.AuthorityAccountStamp modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp ();

        #endregion


        #region Public Properties

        public Int64 RoleId { get { return roleId; } }

        public String Name {

            get { return roleName; }

            set { roleName = value.Substring (0, (value.Length > DataTypeConstants.Name) ? DataTypeConstants.Name : value.Length); }

        }

        public String Description {

            get { return description; }

            set { description = value.Substring (0, (value.Length > DataTypeConstants.Description) ? DataTypeConstants.Description : value.Length); }

        }

        public List<RolePermission> Permissions { get { return permissions; } set { permissions = value; } }

        public List<RoleMembership> Membership { get { return membership; } set { membership = value; } }
                
        public Mercury.Server.Data.AuthorityAccountStamp CreateAccountInfo {
            get {

                if (createAccountInfo == null) {

                    createAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp ();

                }

                return createAccountInfo;

            }

            set { createAccountInfo = value; }

        } // Property: CreateAccountInfo

        public Mercury.Server.Data.AuthorityAccountStamp ModifiedAccountInfo {
            get {

                if (modifiedAccountInfo == null) {

                    modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp ();

                }

                return modifiedAccountInfo;

            }
            set { modifiedAccountInfo = value; }

        } // Property: ModifiedAccountInfo

        #endregion


        #region Constructors

        public Role () { return; }

        public Role (Application application, String environmentName, String roleName) {

            if (!LoadFromDatabase (application, application.EnvironmentDatabaseByName (environmentName), roleName)) {

                throw new Exception ("Unable to load Role from the database for " + roleName + ".");

            }

            else {

                membership = application.EnvironmentRoleMembershipGet (environmentName, roleName);

                permissions = application.EnvironmentRolePermissionsGet (environmentName, roleName);

            }

            return;

        }

        public Role (Application application, String roleName) {

            if (!LoadFromDatabase (application, application.EnvironmentDatabase, roleName)) {

                throw new Exception ("Unable to load Role from the database for " + roleName + ".");

            }

            else {

                membership = application.EnvironmentRoleMembershipGet (roleId);

                permissions = application.EnvironmentRolePermissionsGet (roleId);

            }

            return;

        }

        public Role (Application application, Int64 roleId) {

            if (!LoadFromDatabase (application, application.EnvironmentDatabase, roleId)) {

                throw new Exception ("Unable to load Role from the database for " + roleId.ToString () + ".");

            }

            else {

                membership = application.EnvironmentRoleMembershipGet (roleId);

                permissions = application.EnvironmentRolePermissionsGet (roleId);

            }

            return;

        }

        #endregion


        #region Database Functions

        public Boolean LoadFromDatabaseWithCriteria (Application application, Mercury.Server.Data.SqlDatabase environmentDatabase, String criteria) {

            Boolean success = false;

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable tableEnvironment;

            selectStatement.Append ("SELECT * FROM Role " + criteria);

            tableEnvironment = environmentDatabase.SelectDataTable (selectStatement.ToString ());

            if (tableEnvironment.Rows.Count == 1) {

                MapDataFields (tableEnvironment.Rows[0]);

                success = true;

            }

            return success; 

        }

        public Boolean LoadFromDatabase (Application application, Mercury.Server.Data.SqlDatabase environmentDatabase, String roleName) {

            String criteria;

            criteria = "WHERE RoleName = '" + roleName + "'";

            return LoadFromDatabaseWithCriteria (application, environmentDatabase, criteria);

        }

        public Boolean LoadFromDatabase (Application application, Mercury.Server.Data.SqlDatabase environmentDatabase, Int64 roleId) {

            String criteria;

            criteria = "WHERE RoleId = " + roleId;

            return LoadFromDatabaseWithCriteria (application, environmentDatabase, criteria);

        }

        public void MapDataFields (System.Data.DataRow currentRow) {

            roleId = (Int64) currentRow["RoleId"];

            roleName = (String) currentRow["RoleName"];

            description = (String) currentRow["RoleDescription"];


            createAccountInfo.MapDataFields (currentRow, "Create");

            modifiedAccountInfo.MapDataFields (currentRow, "Modified");

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        public Boolean Save (Mercury.Server.Data.SqlDatabase environmentDatabase) {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();


            try { 

                environmentDatabase.BeginTransaction ();

                sqlStatement.Append ("EXEC Role_InsertUpdate ");

                sqlStatement.Append (RoleId.ToString () + ", '" + roleName.Replace ("'", "''") + "', '" + description.Replace ("'", "''") + "', ");

                sqlStatement.Append ("'" + modifiedAccountInfo.SecurityAuthorityNameSql + "', '" + modifiedAccountInfo.UserAccountIdSql + "', '" + modifiedAccountInfo.UserAccountNameSql + "'");

                success = environmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

                if ((success) && (roleId == 0)) {

                    try {
                        
                        Object identity = environmentDatabase.ExecuteScalar ("SELECT @@IDENTITY").ToString ();

                        if (identity != null) {

                            roleId = Int64.Parse (identity.ToString ());

                        }

                    }

                    catch {

                        success = false;

                    }

                }

                if (success) {

                    success = success && environmentDatabase.ExecuteSqlStatement ("DELETE FROM RolePermission WHERE RoleId = " + roleId.ToString ());

                    success = success && environmentDatabase.ExecuteSqlStatement ("DELETE FROM RoleMembership WHERE RoleId = " + roleId.ToString ());


                    foreach (RolePermission currentPermission in permissions) {

                        currentPermission.RoleId = roleId;

                        currentPermission.ModifiedAccountInfo = modifiedAccountInfo;

                        success = success && currentPermission.Save (environmentDatabase);

                    }

                    foreach (RoleMembership currentMembership in membership) {

                        currentMembership.RoleId = roleId;

                        currentMembership.ModifiedAccountInfo = modifiedAccountInfo;

                        success = success && currentMembership.Save (environmentDatabase);

                    }


                }
            
                if (success) { environmentDatabase.CommitTransaction (); }

                else { throw new ApplicationException ((environmentDatabase.LastException != null) ? environmentDatabase.LastException.Message : "Unable to Save Environment Role"); }

            }

            catch (Exception saveException) {

                System.Diagnostics.Debug.WriteLine ("Unable to Save: " + saveException.Message);

                success = false;
    
                environmentDatabase.RollbackTransaction (); 

            }

            return success;

        }

        #endregion


    }

}

