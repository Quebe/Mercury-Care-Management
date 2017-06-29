using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Public.Interfaces.Security {

    public class AuthenticationResult {

        #region Private Properties

        [DataMember (Name = "Token")]
        private String token;

        [DataMember (Name = "IsAuthenticated")]
        private Boolean isAuthenticated = false;

        [DataMember (Name = "AuthenticationError")]
        private Enumerations.AuthenticationError authenticationError = Enumerations.AuthenticationError.NoError;

        private Exception authenticationException;


        [DataMember (Name = "UserAccountId")]
        private String userAccountId;

        [DataMember (Name = "UserAccountName")]
        private String userAccountName;

        [DataMember (Name = "UserDisplayName")]
        private String userDisplayName;

        [DataMember (Name = "GroupMembership")]
        private List<String> groupMembership = new List<String> ();

        #endregion


        #region Public Properties

        public String Token { get { return token; } set { token = value; } } // Property: Token

        public Boolean IsAuthenticated { get { return isAuthenticated; } set { isAuthenticated = value; } } // Property: IsAuthenticated

        public Enumerations.AuthenticationError AuthenticationError { get { return authenticationError; } set { authenticationError = value; } }

        public Exception AuthenticationException { get { return authenticationException; } set { authenticationException = value; } } // Property: AuthenticationException


        public String UserAccountId { get { return userAccountId; } set { userAccountId = value; } } // Property: UserAccountId

        public String UserAccountName { get { return userAccountName; } set { userAccountName = value; } } // Property: UserAccountName

        public String UserDisplayName { get { return userDisplayName; } set { userDisplayName = value; } } // Property: UserDisplayName

        public List<String> GroupMembership { get { return groupMembership; } set { groupMembership = value; } }

        #endregion 

    }

}
