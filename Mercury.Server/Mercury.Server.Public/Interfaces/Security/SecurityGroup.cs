using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Public.Interfaces.Security {

    [DataContract]
    public class SecurityGroup {

        #region Private Properties

        [DataMember (Name = "SecurityAuthorityId")]
        private Int64 securityAuthorityId = 0;

        [DataMember (Name = "SecurityAuthorityName")]
        private String securityAuthorityName = String.Empty;


        [DataMember (Name = "SecurityGroupId")]
        private String securityGroupId = String.Empty;

        [DataMember (Name = "SecurityGroupName")]
        private String securityGroupName = String.Empty;

        [DataMember (Name = "Description")]
        private String description = String.Empty;

        #endregion


        #region Public Properties

        public Int64 SecurityAuthorityId { get { return securityAuthorityId; } set { securityAuthorityId = value; } }

        public String SecurityAuthorityName { get { return securityAuthorityName; } set { securityAuthorityName = value; } }


        public String SecurityGroupId { get { return securityGroupId; } set { securityGroupId = value; } }

        public String SecurityGroupName { get { return securityGroupName; } set { securityGroupName = value; } }

        public String Description { get { return description; } set { description = value; } }

        #endregion


    }

}
