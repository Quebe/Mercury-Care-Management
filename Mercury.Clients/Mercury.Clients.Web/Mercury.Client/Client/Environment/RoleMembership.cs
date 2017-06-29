using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Environment {

    [Serializable]
    public class RoleMembership {

        #region Private Properties
        
        private Int64 roleId;

        private Int64 securityAuthorityId; 

        private String securityAuthorityName;

        private String securityGroupId;

        private String securityGroupName;

        private Mercury.Server.Application.AuthorityAccountStamp createAccountInfo = new Mercury.Server.Application.AuthorityAccountStamp ();

        private Mercury.Server.Application.AuthorityAccountStamp modifiedAccountInfo = new Mercury.Server.Application.AuthorityAccountStamp ();


        private Application application = null;

        #endregion


        #region Public Properties

        public Int64 RoleId { get { return roleId; } set { roleId = value; } }


        public Int64 SecurityAuthorityId { get { return securityAuthorityId; } set { securityAuthorityId = value; } }

        public String SecurityAuthorityName { get { return securityAuthorityName; } set { securityAuthorityName = value; } }

        public String SecurityGroupId { get { return securityGroupId; } set { securityGroupId = value; } }

        public String SecurityGroupName { get { return securityGroupName; } set { securityGroupName = value; } }


        public Mercury.Server.Application.AuthorityAccountStamp CreateAccountInfo { get { return createAccountInfo; } set { createAccountInfo = value; } }

        public Mercury.Server.Application.AuthorityAccountStamp ModifiedAccountInfo { get { return modifiedAccountInfo; } set { modifiedAccountInfo = value; } }


        public Application Application { get { return application; } set { application = value; } }

        #endregion


        #region Constructors

        public RoleMembership (Application forApplication) { application = forApplication; return; }

        public RoleMembership (Application forApplication, Mercury.Server.Application.EnvironmentRoleMembership serverMembership) {

            application = forApplication;


            roleId = serverMembership.RoleId;

            securityAuthorityId = serverMembership.SecurityAuthorityId;

            securityAuthorityName = serverMembership.SecurityAuthorityName;

            securityGroupId = serverMembership.SecurityGroupId;

            securityGroupName = serverMembership.SecurityGroupName;


            createAccountInfo = serverMembership.CreateAccountInfo;

            modifiedAccountInfo = serverMembership.ModifiedAccountInfo;


            return;

        }

        #endregion


        #region Public Methods

        public Mercury.Server.Application.EnvironmentRoleMembership ToServerObject () {

            Mercury.Server.Application.EnvironmentRoleMembership serverMembership = new Mercury.Server.Application.EnvironmentRoleMembership ();


            serverMembership.RoleId = roleId;


            serverMembership.SecurityAuthorityId = securityAuthorityId;

            serverMembership.SecurityAuthorityName = securityAuthorityName;

            serverMembership.SecurityGroupId = securityGroupId;

            serverMembership.SecurityGroupName = securityGroupName;


            serverMembership.CreateAccountInfo = createAccountInfo;

            serverMembership.ModifiedAccountInfo = modifiedAccountInfo;

            return serverMembership;

        }

        public Boolean IsEqual (RoleMembership compareMembership) {

            Boolean isEqual = true;

            isEqual = isEqual && (securityAuthorityId == compareMembership.SecurityAuthorityId);

            isEqual = isEqual && (securityGroupId == compareMembership.SecurityGroupId);

            return isEqual;

        }

        #endregion

    }

}
