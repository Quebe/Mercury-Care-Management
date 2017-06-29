using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Mercury.Server.Services.Responses {

    [DataContract]
    public class AuthenticationResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name="Token")]
        private String token;

        [DataMember (Name="IsAuthenticated")]
        private Boolean isAuthenticated = false;

        [DataMember (Name = "AuthenticationError")]
        private Server.Public.Interfaces.Security.Enumerations.AuthenticationError authenticationError = Public.Interfaces.Security.Enumerations.AuthenticationError.NoError;

        [DataMember (Name = "Environments")]
        private String environments;


        [DataMember (Name = "SecurityAuthorityId")]
        private Int64 securityAuthorityId;

        [DataMember (Name = "SecurityAuthorityName")]
        private String securityAuthorityName;

        [DataMember (Name = "SecurityAuthorityType")]
        private Server.Security.Enumerations.SecurityAuthorityType securityAuthorityType = Security.Enumerations.SecurityAuthorityType.WindowsIntegrated;


        [DataMember (Name = "EnvironmentId")]
        private Int64 environmentId = 0;

        [DataMember (Name = "EnvironmentName")]
        private String environmentName;

        [DataMember (Name = "ConfidentialityStatement")]
        private String confidentialityStatement;

        [DataMember (Name = "UserAccountId")]
        private String userAccountId;

        [DataMember (Name = "UserAccountName")]
        private String userAccountName;

        [DataMember (Name = "UserDisplayName")]
        private String userDisplayName;


        [DataMember (Name = "GroupMembership")]
        private List<String> groupMembership = new List<String> ();

        [DataMember (Name = "RoleMembership")]
        private List<String> roleMembership = new List<String> ();

        [DataMember (Name = "EnterprisePermissionSet")]
        private List<String> enterprisePermissionSet = new List<String> ();

        [DataMember (Name = "EnvironmentPermissionSet")]
        private List<String> environmentPermissionSet = new List<String> ();


        [DataMember (Name = "WorkQueuePermissions")]
        private Dictionary<Int64, Server.Core.Work.Enumerations.WorkQueueTeamPermission> workQueuePermissions = new Dictionary<Int64, Server.Core.Work.Enumerations.WorkQueueTeamPermission> ();

        [DataMember (Name = "WorkTeamMembership")]
        private Dictionary<Int64, Server.Core.Work.WorkTeamMembership> workTeamMembership = new Dictionary<Int64, Server.Core.Work.WorkTeamMembership> ();

        #endregion


        #region Public Properties

        public String Token { get { return token;  } set { token = value; } } // Property: Token

        public Boolean IsAuthenticated { get { return isAuthenticated; } set { isAuthenticated = value; } } // Property: IsAuthenticated

        public Server.Public.Interfaces.Security.Enumerations.AuthenticationError AuthenticationError { get { return authenticationError;  } set { authenticationError = value; } }

        public String Environments { get { return environments;  } set { environments = value; } } // Property: Environments

        public Int64 SecurityAuthorityId { get { return securityAuthorityId; } set { securityAuthorityId = value; } }

        public String SecurityAuthorityName { get { return securityAuthorityName; } set { securityAuthorityName = value; } } // Property: AuthorityName

        public Server.Security.Enumerations.SecurityAuthorityType SecurityAuthorityType { get { return securityAuthorityType; } set { securityAuthorityType = value; } } // Property: AuthorityName

        public Int64 EnvironmentId { get { return environmentId; } set { environmentId = value; } }

        public String EnvironmentName { get { return environmentName; } set { environmentName = value; } } // Property: EnvironmentName

        public String ConfidentialityStatement { get { return confidentialityStatement; } set { confidentialityStatement = value; } } // Property: ConfidentialityStatement

        public String UserAccountId { get { return userAccountId; } set { userAccountId = value; } } // Property: UserAccountId

        public String UserAccountName { get { return userAccountName; } set { userAccountName = value; } } // Property: UserAccountName

        public String UserDisplayName { get { return userDisplayName; } set { userDisplayName = value; } } // Property: UserDisplayName

        public List<String> GroupMembership { get { return groupMembership; } set { groupMembership = value; } }

        public List<String> RoleMembership { get { return roleMembership; } set { roleMembership = value; } }

        public List<String> EnterprisePermissionSet { get { return enterprisePermissionSet; } set { enterprisePermissionSet = value; } } // Property: PermissionSet

        public List<String> EnvironmentPermissionSet { get { return environmentPermissionSet; } set { environmentPermissionSet = value; } } // Property: PermissionSet


        public Dictionary<Int64, Server.Core.Work.Enumerations.WorkQueueTeamPermission> WorkQueuePermissions { get { return workQueuePermissions; } set { workQueuePermissions = value; } }

        public Dictionary<Int64, Server.Core.Work.WorkTeamMembership> WorkTeamMembership { get { return workTeamMembership; } set { workTeamMembership = value; } }

        #endregion 


        #region Constructors

        public AuthenticationResponse () { /* DO NOTHING */ }

        public AuthenticationResponse (Server.Security.AuthenticationResponse forAuthenticationResponse) {

            token = forAuthenticationResponse.Token;

            isAuthenticated = forAuthenticationResponse.IsAuthenticated;

            authenticationError = forAuthenticationResponse.AuthenticationError;

            environments = forAuthenticationResponse.Environments;

            securityAuthorityId = forAuthenticationResponse.SecurityAuthorityId;

            securityAuthorityName = forAuthenticationResponse.SecurityAuthorityName;

            securityAuthorityType = forAuthenticationResponse.SecurityAuthorityType;

            environmentId = forAuthenticationResponse.EnvironmentId;

            environmentName = forAuthenticationResponse.EnvironmentName;

            confidentialityStatement = forAuthenticationResponse.ConfidentialityStatement;

            userAccountId = forAuthenticationResponse.UserAccountId;

            userAccountName = forAuthenticationResponse.UserAccountName;

            userDisplayName = forAuthenticationResponse.UserDisplayName;

            groupMembership = forAuthenticationResponse.GroupMembership;

            roleMembership = forAuthenticationResponse.RoleMembership;

            enterprisePermissionSet = forAuthenticationResponse.EnterprisePermissionSet;

            environmentPermissionSet = forAuthenticationResponse.EnvironmentPermissionSet;

            workQueuePermissions = forAuthenticationResponse.WorkQueuePermissions;


            SetException (forAuthenticationResponse.AuthenticationException);

            return;

        }

        #endregion 

    }

}