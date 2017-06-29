using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Public.Interfaces.Security {

    [DataContract (Name = "UserAccount")]
    public class UserAccount {

        #region Private Properties

        [DataMember (Name = "UserAccountId")]
        private String userAccountId = String.Empty;

        [DataMember (Name = "Name")]
        private String userAccountName = String.Empty;

        [DataMember (Name = "DisplayName")]
        private String displayName;

        [DataMember (Name = "DistinguishedName")]
        private String distinguishedName;

        [DataMember (Name = "Path")]
        private String path;

        #endregion 


        #region Public Properties

        public String UserAccountId { get { return userAccountId; } set { userAccountId = value; } }

        public String Name { get { return userAccountName; } set { userAccountName = value; } }

        public String DisplayName { get { return displayName; } set { displayName = value; } }

        public String DistinguishedName { get { return distinguishedName; } set { distinguishedName = value; } }

        public String Path { get { return path; } set { path = value; } }

        #endregion
       
    }

}
