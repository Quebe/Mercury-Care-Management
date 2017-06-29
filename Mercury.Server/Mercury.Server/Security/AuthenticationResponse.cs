using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Security {

    public class AuthenticationResponse : Mercury.Server.Public.Interfaces.Security.AuthenticationResult {

        #region Private Properties

        [DataMember (Name = "Environments")]
        private String environments;

        [DataMember (Name = "SecurityAuthorityId")]
        private Int64 securityAuthorityId;

        [DataMember (Name = "SecurityAuthorityName")]
        private String securityAuthorityName;

        [DataMember (Name = "SecurityAuthorityType")]
        private Enumerations.SecurityAuthorityType securityAuthorityType = Enumerations.SecurityAuthorityType.WindowsIntegrated;


        [DataMember (Name = "EnvironmentId")]
        private Int64 environmentId = 0;

        [DataMember (Name = "EnvironmentName")]
        private String environmentName;

        [DataMember (Name = "ConfidentialityStatement")]
        private String confidentialityStatement;


        [DataMember (Name = "RoleMembership")]
        private List<String> roleMembership = new List<String> ();

        [DataMember (Name = "EnterprisePermissionSet")]
        private List<String> enterprisePermissionSet = new List<String> ();

        [DataMember (Name = "EnvironmentPermissionSet")]
        private List<String> environmentPermissionSet = new List<String> ();


        [DataMember (Name = "WorkQueuePermissions")]
        private Dictionary<Int64, Core.Work.Enumerations.WorkQueueTeamPermission> workQueuePermissions = new Dictionary<Int64, Mercury.Server.Core.Work.Enumerations.WorkQueueTeamPermission> ();

        [DataMember (Name = "WorkTeamMembership")]
        private Dictionary<Int64, Core.Work.WorkTeamMembership> workTeamMembership = new Dictionary<long, Core.Work.WorkTeamMembership> ();

        #endregion


        #region Public Properties

        public String Environments { get { return environments; } set { environments = value; } } // Property: Environments

        public Int64 SecurityAuthorityId { get { return securityAuthorityId; } set { securityAuthorityId = value; } }

        public String SecurityAuthorityName { get { return securityAuthorityName; } set { securityAuthorityName = value; } } // Property: AuthorityName

        public Enumerations.SecurityAuthorityType SecurityAuthorityType { get { return securityAuthorityType; } set { securityAuthorityType = value; } } // Property: AuthorityName

        public Int64 EnvironmentId { get { return environmentId; } set { environmentId = value; } }

        public String EnvironmentName { get { return environmentName; } set { environmentName = value; } } // Property: EnvironmentName

        public String ConfidentialityStatement { get { return confidentialityStatement; } set { confidentialityStatement = value; } } // Property: ConfidentialityStatement


        public List<String> RoleMembership { get { return roleMembership; } set { roleMembership = value; } }

        public List<String> EnterprisePermissionSet { get { return enterprisePermissionSet; } set { enterprisePermissionSet = value; } } // Property: PermissionSet

        public List<String> EnvironmentPermissionSet { get { return environmentPermissionSet; } set { environmentPermissionSet = value; } } // Property: PermissionSet


        public Dictionary<Int64, Core.Work.Enumerations.WorkQueueTeamPermission> WorkQueuePermissions { get { return workQueuePermissions; } set { workQueuePermissions = value; } }

        public Dictionary<Int64, Core.Work.WorkTeamMembership> WorkTeamMembership { get { return workTeamMembership; } set { workTeamMembership = value; } }

        #endregion 

    }

}
