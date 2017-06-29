using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server {

    [Serializable]
    [DataContract (Name="Session")]
    public class Session {

        #region Private Properties

        [DataMember (Name = "Token")]
        private String token = Guid.NewGuid ().ToString ();

        [DataMember (Name = "SecurityAuthorityId")]
        private Int64  securityAuthorityId;

        [DataMember (Name = "SecurityAuthorityName")]
        private String securityAuthorityName;

        [DataMember (Name = "SecurityAuthorityType")]
        private Security.Enumerations.SecurityAuthorityType securityAuthorityType = Security.Enumerations.SecurityAuthorityType.WindowsIntegrated;


        [DataMember (Name = "UserAccountId")]
        private String userAccountId;

        [DataMember (Name = "UserAccountName")]
        private String userAccountName;

        [DataMember (Name = "UserDisplayName")]
        private String userDisplayName = String.Empty;

        [DataMember (Name = "GroupMembership")]
        private List<String> groupMembership = new List<String> ();     // SECURITY GROUP IDS

        [DataMember (Name = "RoleMembership")]
        private List<String> roleMembership = new List<String> ();      // ROLE IDS

        [DataMember (Name = "EnvironmentId")]
        private Int64  environmentId;

        [DataMember (Name = "EnvironmentName")]
        private String environmentName;

        private Data.SqlConfiguration environmentConfiguration = null;

        [DataMember (Name = "EnterprisePermissionSet")]
        private List<String> enterprisePermissionSet = new List<String> ();

        [DataMember (Name = "EnvironmentPermissionSet")]
        private List<String> environmentPermissionSet = new List<String> ();


        [DataMember (Name = "WorkQueuePermissions")]
        private Dictionary<Int64, Core.Work.Enumerations.WorkQueueTeamPermission> workQueuePermissions = new Dictionary<Int64, Mercury.Server.Core.Work.Enumerations.WorkQueueTeamPermission> ();

        [DataMember (Name = "WorkTeamMembership")]
        private Dictionary<Int64, Core.Work.WorkTeamMembership> workTeamMembership = null;


        [DataMember (Name = "LastActivityDate")]
        private DateTime lastActivityDate = DateTime.Now;

        #endregion


        #region Public Properies

        public String Token { get { return token; } }


        public Int64 SecurityAuthorityId { get { return securityAuthorityId; } }

        public String SecurityAuthorityName { get { return securityAuthorityName; } }

        public Security.Enumerations.SecurityAuthorityType SecurityAuthorityType { get { return securityAuthorityType; } }


        public String UserAccountId { get { return userAccountId; } }

        public String UserAccountName { get { return userAccountName; } }

        public String UserDisplayName {

            get {

                if (String.IsNullOrEmpty (userDisplayName)) { return UserAccountName; }

                return userDisplayName;

            }

        }

        public List<String> GroupMembership { get { return groupMembership; } }

        public List<String> RoleMembership { get { return roleMembership; } }

        public Int64 EnvironmentId { get { return environmentId; } }

        public String EnvironmentName { get { return environmentName; } }

        public Data.SqlConfiguration EnvironmentConfiguration { get { return environmentConfiguration; } }

        public List<String> EnterprisePermissionSet { get { return enterprisePermissionSet; } }

        public List<String> EnvironmentPermissionSet { get { return environmentPermissionSet; } }

        public Dictionary<Int64, Core.Work.Enumerations.WorkQueueTeamPermission> WorkQueuePermissions { get { return workQueuePermissions; } }

        public Dictionary<Int64, Core.Work.WorkTeamMembership> WorkTeamMembership { get { return workTeamMembership; } }

        public DateTime LastActivityDate { get { return lastActivityDate; } set { lastActivityDate = value; } }

        #endregion


        #region Constructors

        public Session (Application application, Mercury.Server.Public.Interfaces.Security.Credentials credentials) {

            Security.SecurityAuthority securityAuthority = application.SecurityAuthorityGet (credentials.SecurityAuthorityId);

            securityAuthorityId = securityAuthority.Id;

            securityAuthorityName = securityAuthority.Name;

            securityAuthorityType = securityAuthority.SecurityAuthorityType;


            userAccountId = credentials.UserAccountId;

            userAccountName = credentials.UserAccountName;

            userDisplayName = credentials.UserDisplayName;

            groupMembership = credentials.Groups;

            environmentId = application.EnvironmentGet (credentials.Environment).Id;

            environmentName = credentials.Environment;

            roleMembership = application.EnvironmentRoleMembershipListGet (environmentId, securityAuthorityId, groupMembership);

            enterprisePermissionSet = application.EnterprisePermissionsGet (securityAuthorityId, groupMembership);

            environmentPermissionSet = application.EnvironmentPermissionsGet (environmentName, securityAuthorityId, roleMembership);


            List<Core.Work.DataViews.WorkQueuePermission> viewWorkQueuePermissions = application.WorkQueuePermissionsForUserByEnvironment (securityAuthorityId, userAccountId, environmentName);

            foreach (Core.Work.DataViews.WorkQueuePermission currentPermission in viewWorkQueuePermissions) {

                workQueuePermissions.Add (currentPermission.WorkQueueId, currentPermission.Permission);

            }


            workTeamMembership = new Dictionary<long, Core.Work.WorkTeamMembership> ();

            foreach (Core.Work.WorkTeamMembership currentMembership in application.WorkTeamMembershipsForUserByEnvironment (securityAuthorityId, userAccountId, environmentName)) {

                workTeamMembership.Add (currentMembership.WorkTeamId, currentMembership);

            }


            token = (String) application.EnterpriseDatabase.LookupValue ("(SELECT NEWID () AS Token) AS GenerateToken", "CAST (Token AS VARCHAR (60)) AS Token", "");

            SerializeToDatabase (application);


            System.Diagnostics.Trace.WriteLineIf (application.TraceSwitchSecurity.TraceVerbose, "** SESSION (BEGIN) **");

            if (application.TraceSwitchSecurity.TraceVerbose) {

                foreach (String currentGroup in groupMembership) {

                    System.Diagnostics.Trace.WriteLine ("Group Membership: " + currentGroup);

                }

                foreach (String currentRole in roleMembership) {

                    System.Diagnostics.Trace.WriteLine ("Role Membership: " + currentRole);

                }

                foreach (String currentEnterprisePermission in enterprisePermissionSet) {

                    System.Diagnostics.Trace.WriteLine ("Enterprise Permission: " + currentEnterprisePermission);

                }

                foreach (String currentEnvironmentPermission in environmentPermissionSet) {

                    System.Diagnostics.Trace.WriteLine ("Environment Permission: " + currentEnvironmentPermission);

                }

                foreach (Core.Work.DataViews.WorkQueuePermission currentPermission in viewWorkQueuePermissions) {

                    System.Diagnostics.Trace.WriteLine ("Work Queue Permission: " + currentPermission.Name + " (" + currentPermission.Permission.ToString () + ")");

                }

            }

            System.Diagnostics.Trace.WriteLineIf (application.TraceSwitchSecurity.TraceVerbose, "** SESSION ( END ) **");

            return;

        }

        public Session (Application application, String sessionToken) {

            token = sessionToken;

            UnserializeFromDatabase (application, sessionToken);

            SerializeToDatabase (application);

            return;

        }

        #endregion


        #region Cache to Database

        public void SerializeToDatabase (Application application) {

            StringBuilder cachedData = new StringBuilder ();

            cachedData.Append (securityAuthorityId + "|");

            cachedData.Append (securityAuthorityName.Replace ("'", "''") + "|");

            cachedData.Append (((Int32) securityAuthorityType).ToString () + "|");

            cachedData.Append (userAccountId + "|");

            cachedData.Append (userAccountName.Replace ("'", "''") + "|");

            cachedData.Append (UserDisplayName.Replace ("'", "''") + "|"); // MUST USE PUBLIC PROPERTY


            cachedData.Append (environmentId + "|");

            cachedData.Append (environmentName + "|");

            foreach (String currentGroup in groupMembership) { 

                cachedData.Append (currentGroup + ";");

            }

            cachedData.Append ("|");

            foreach (string currentRole in roleMembership) {

                cachedData.Append (currentRole + ";");

            }

            cachedData.Append ("|");

            foreach (Int64 currentWorkQueuePermission in workQueuePermissions.Keys) {

                cachedData.Append (currentWorkQueuePermission.ToString () + "." + ((Int32) workQueuePermissions[currentWorkQueuePermission]).ToString () + ";");

            }


            String auditStatement;

            auditStatement = String.Empty;

            auditStatement += "SET TRANSACTION ISOLATION LEVEL REPEATABLE READ \r\n";

            auditStatement += "BEGIN TRANSACTION \r\n";

            auditStatement += "UPDATE Audit.Authentication \r\n";

            auditStatement += "  SET LogoffDateTime = GETDATE () \r\n";

            auditStatement += "  WHERE SessionToken IN (SELECT SessionToken FROM SessionCache WHERE (ExpirationTime < GETDATE ())) \r\n";

            auditStatement += "DELETE FROM SessionCache WHERE (ExpirationTime < GETDATE ()) \r\n";

            auditStatement += "COMMIT TRANSACTION \r\n";

            application.EnterpriseDatabase.ExecuteSqlStatement (auditStatement);


            StringBuilder insertStatement = new StringBuilder ();

            // ALLOW USER CONFIGURABLE TIME OUTS - APPLICATION.SESSIONTIMEOUTMINUTES
            
            insertStatement.Append ("BEGIN TRANSACTION;");

            insertStatement.Append ("DELETE FROM SessionCache WHERE SessionToken = '" + token.ToString () + "';");

            insertStatement.Append ("INSERT INTO SessionCache (SessionToken, ExpirationTime, CachedData) VALUES (");

            insertStatement.Append ("'" + Token + "', ");

            insertStatement.Append ("DATEADD (MINUTE, " + application.SessionTimeoutMinutes.ToString () + ", GETDATE ()), ");

            insertStatement.Append ("'" + cachedData + "');");

            insertStatement.Append ("COMMIT TRANSACTION;");


            application.EnterpriseDatabase.ExecuteSqlStatement (insertStatement.ToString (), 0);

            return;

        }

        public void UnserializeFromDatabase (Application application, String sessionToken) {

            StringBuilder selectStatement = new StringBuilder ("SELECT * FROM SessionCache WHERE SessionToken = '" + sessionToken + "'");

            System.Data.DataTable sessionCacheTable = application.EnterpriseDatabase.SelectDataTable (selectStatement.ToString ());

            if (sessionCacheTable.Rows.Count == 1) {

                securityAuthorityId = Int64.Parse (((String) sessionCacheTable.Rows[0]["CachedData"]).Split ('|')[0]);

                securityAuthorityName = (String) ((String) sessionCacheTable.Rows[0]["CachedData"]).Split ('|')[1];

                securityAuthorityType = (Security.Enumerations.SecurityAuthorityType) Convert.ToInt32 (((String) sessionCacheTable.Rows[0]["CachedData"]).Split ('|')[2]);

                userAccountId = (String) ((String) sessionCacheTable.Rows[0]["CachedData"]).Split ('|')[3];

                userAccountName = (String) ((String) sessionCacheTable.Rows[0]["CachedData"]).Split ('|')[4];

                userDisplayName = (String) ((String) sessionCacheTable.Rows[0]["CachedData"]).Split ('|')[5];

                environmentId = Int64.Parse (((String) sessionCacheTable.Rows[0]["CachedData"]).Split ('|')[6]);

                environmentName = (String) ((String) sessionCacheTable.Rows[0]["CachedData"]).Split ('|')[7];

                String groupMembershipCached = (String) ((String) sessionCacheTable.Rows [0]["CachedData"]).Split ('|') [8];

                for (Int32 currentGroupIndex = 0; currentGroupIndex < (groupMembershipCached.Split (';').Length - 1); currentGroupIndex ++) {

                    groupMembership.Add (groupMembershipCached.Split (';')[currentGroupIndex]);

                }

                String roleMembershipCached = (String) ((String) sessionCacheTable.Rows [0]["CachedData"]).Split ('|') [9];

                for (Int32 currentRoleIndex = 0; currentRoleIndex < (roleMembershipCached.Split (';').Length - 1); currentRoleIndex ++) {

                    roleMembership.Add (roleMembershipCached.Split (';')[currentRoleIndex]);

                }

                String workQueuePermissionsCached = (String) ((String) sessionCacheTable.Rows[0]["CachedData"]).Split ('|')[10];

                for (Int32 currentPermissionIndex = 0; currentPermissionIndex < (workQueuePermissionsCached.Split (';').Length - 1); currentPermissionIndex ++) {

                    String compressedPermission = workQueuePermissionsCached.Split (';')[currentPermissionIndex];

                    if (compressedPermission.Split ('.').Length == 2) {

                        Int64 workQueueId = 0;

                        Int32 permission = 0;

                        if ((Int64.TryParse (compressedPermission.Split ('.')[0], out workQueueId)) && ((Int32.TryParse (compressedPermission.Split ('.')[1], out permission)))) {

                            workQueuePermissions.Add (workQueueId, (Mercury.Server.Core.Work.Enumerations.WorkQueueTeamPermission) permission);

                        }

                    }

                }

                enterprisePermissionSet = application.EnterprisePermissionsGet (securityAuthorityId, groupMembership);

                environmentPermissionSet = application.EnvironmentPermissionsGet (EnvironmentName, securityAuthorityId, roleMembership);

            }

            return;

        }

        #endregion


    }

}
