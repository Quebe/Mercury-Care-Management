using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace Mercury.Client {

    public class Session {

        #region Private Properties

        private String token;

        private Int64 securityAuthorityId;

        private String securityAuthorityName;

        private Server.Enterprise.SecurityAuthorityType securityAuthorityType = Server.Enterprise.SecurityAuthorityType.WindowsIntegrated;


        private Int64 environmentId = 0;

        private String environmentName = String.Empty;


        private String confidentialityStatement;

        private String userAccountId;

        private String userAccountName;

        private String userDisplayName;


        private List<String> groupMembership = new List<String> ();

        private List<String> roleMembership = new List<String> ();

        private Dictionary<String, String> enterprisePermissionSet = new Dictionary<String, String> ();

        private Dictionary<String, String> environmentPermissionSet = new Dictionary<String, String> ();

        private Dictionary<Int64, Server.Application.WorkQueueTeamPermission> workQueuePermissions = new Dictionary<Int64, Server.Application.WorkQueueTeamPermission> ();

        #endregion


        #region Public Properties

        public String Token { get { return token; } }

        public Int64 SecurityAuthorityId { get { return securityAuthorityId; } set { securityAuthorityId = value; } }

        public String SecurityAuthorityName { get { return securityAuthorityName; } set { securityAuthorityName = value; } } // Property: AuthorityName

        public Server.Enterprise.SecurityAuthorityType SecurityAuthorityType { get { return securityAuthorityType; } set { securityAuthorityType = value; } } // Property: AuthorityType

        public Int64 EnvironmentId { get { return environmentId; } set { environmentId = value; } }

        public String EnvironmentName { get { return environmentName; } set { environmentName = value; } } // Property: EnvironmentName

        public String ConfidentialityStatement { get { return confidentialityStatement; } set { confidentialityStatement = value; } } // Property: ConfidentialityStatement

        public String UserAccountId { get { return userAccountId; } set { userAccountId = value; } }

        public String UserAccountName { get { return userAccountName; } set { userAccountName = value; } }

        public String UserDisplayName { get { return userDisplayName; } set { userDisplayName = value; } }

        public List<String> GroupMembership { get { return groupMembership; } set { groupMembership = value; } }

        public List<String> RoleMembership { get { return roleMembership; } set { roleMembership = value; } }

        public Dictionary<String, String> EnterprisePermissionSet { get { return enterprisePermissionSet; } } // Property: EnterprisePermissionSet

        public Dictionary<String, String> EnvironmentPermissionSet { get { return environmentPermissionSet; } } // Property: EnvironmentPermissionSet

        public Dictionary<Int64, Server.Application.WorkQueueTeamPermission> WorkQueuePermissions { get { return workQueuePermissions; } }

        #endregion


        #region Constructors

        public Session (Server.Enterprise.AuthenticationResponse authenticationResponse) {

            token = authenticationResponse.Token;

            securityAuthorityId = authenticationResponse.SecurityAuthorityId;

            securityAuthorityName = authenticationResponse.SecurityAuthorityName;

            securityAuthorityType = authenticationResponse.SecurityAuthorityType;

            environmentId = authenticationResponse.EnvironmentId;

            environmentName = authenticationResponse.EnvironmentName;

            confidentialityStatement = authenticationResponse.ConfidentialityStatement;

            userAccountId = authenticationResponse.UserAccountId;

            userAccountName = authenticationResponse.UserAccountName;

            userDisplayName = authenticationResponse.UserDisplayName;


            foreach (Int64 currentWorkQueueId in authenticationResponse.WorkQueuePermissions.Keys) {

                Int32 permission = (Int32)authenticationResponse.WorkQueuePermissions[currentWorkQueueId];

                workQueuePermissions.Add (currentWorkQueueId, (Server.Application.WorkQueueTeamPermission)permission);

            }


            foreach (String currentGroupMembership in authenticationResponse.GroupMembership) {

                groupMembership.Add (currentGroupMembership);

            }

            foreach (String currentRoleMembership in authenticationResponse.RoleMembership) {

                roleMembership.Add (currentRoleMembership);

            }

            foreach (String currentPermission in authenticationResponse.EnterprisePermissionSet) {

                enterprisePermissionSet.Add (currentPermission, currentPermission);

            }

            foreach (String currentPermission in authenticationResponse.EnvironmentPermissionSet) {

                environmentPermissionSet.Add (currentPermission, currentPermission);

            }

            return;

        }

        public Session (String sessionToken) {

            token = sessionToken;

            return;

        }

        public Session (Server.Application.Session serverSession) {

            token = serverSession.Token;

            securityAuthorityId = serverSession.SecurityAuthorityId;

            securityAuthorityName = serverSession.SecurityAuthorityName;

            // TODO: UPDATE SILVERLIGHT

            //securityAuthorityType = serverSession.SecurityAuthorityType;

            environmentId = serverSession.EnvironmentId;

            environmentName = serverSession.EnvironmentName;

            userAccountId = serverSession.UserAccountId;

            userAccountName = serverSession.UserAccountName;

            userDisplayName = serverSession.UserDisplayName;


            // TODO: UPDATE SILVERLIGHT

            //groupMembership = serverSession.GroupMembership;

            //roleMembership = serverSession.RoleMembership;

            enterprisePermissionSet.Clear ();

            foreach (String currentEnterprisePermission in serverSession.EnterprisePermissionSet) {

                enterprisePermissionSet.Add (currentEnterprisePermission, currentEnterprisePermission);

            }

            environmentPermissionSet.Clear ();

            foreach (String currentEnvironmentPermission in serverSession.EnvironmentPermissionSet) {

                environmentPermissionSet.Add (currentEnvironmentPermission, currentEnvironmentPermission);

            }


            workQueuePermissions.Clear ();

            // TODO: UPDATE SILVERLIGHT

            //foreach (Int64 currentWorkQueueId in serverSession.workQueuePermissions.Keys) {

            //    workQueuePermissions.Add (currentWorkQueueId, serverSession.workQueuePermissions[currentWorkQueueId]);

            //}

            return;

        }
        #endregion

    }

}
