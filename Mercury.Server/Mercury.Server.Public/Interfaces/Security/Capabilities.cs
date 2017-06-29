using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Public.Interfaces.Security {

    [DataContract (Name = "SecurityAuthorityCapabilities")]
    public class Capabilities {

        #region Private Properties

        [DataMember (Name = "CanBrowseDirectory")]
        private Boolean canBrowseDirectory = false;

        [DataMember (Name = "CanManageUserAccounts")]
        private Boolean canManageUserAccounts = false;

        [DataMember (Name = "CanGetSecurityGroups")]
        private Boolean canGetSecurityGroups = false;

        #endregion 


        #region Public Properties

        public Boolean CanBrowseDirectory { get { return canBrowseDirectory; } set { canBrowseDirectory = value; } }

        public Boolean CanManageUserAccounts { get { return canManageUserAccounts; } set { canManageUserAccounts = value; } }

        public Boolean CanGetSecurityGroups { get { return canGetSecurityGroups; } set { canGetSecurityGroups = value; } }

        #endregion

    }

}
