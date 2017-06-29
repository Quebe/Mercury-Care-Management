using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Environment {

    [Serializable]
    public class Role {

        #region Private Properties
        
        private Int64 roleId;

        private String roleName; 

        private String description;


        private List<RolePermission> permissions = new List<RolePermission> ();

        private List<RoleMembership> membership = new List<RoleMembership> ();


        private Mercury.Server.Application.AuthorityAccountStamp createAccountInfo = new Mercury.Server.Application.AuthorityAccountStamp ();

        private Mercury.Server.Application.AuthorityAccountStamp modifiedAccountInfo = new Mercury.Server.Application.AuthorityAccountStamp ();


        private Application application = null;

        #endregion


        #region Public Properties
            
        public Int64 RoleId { get { return roleId; } }

        public String Name { get { return roleName; } set { roleName = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public String Description { get { return description; } set { description = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Description); } }


        public List<RolePermission> Permissions { get { return permissions; } set { permissions = value; } }

        public List<RoleMembership> Membership { get { return membership; } set { membership = value; } }


        public Mercury.Server.Application.AuthorityAccountStamp CreateAccountInfo { get { return createAccountInfo; } set { createAccountInfo = value; } }

        public Mercury.Server.Application.AuthorityAccountStamp ModifiedAccountInfo { get { return modifiedAccountInfo; } set { modifiedAccountInfo = value; } }


        public Application Application { get { return application; } set { application = value; } }

        #endregion


        #region Constructors

        public Role (Application forApplication) { application = forApplication; return; }

        public Role (Application forApplication, Mercury.Server.Application.EnvironmentRole serverRole) {

            application = forApplication;


            roleId = serverRole.RoleId;

            roleName = serverRole.Name;

            description = serverRole.Description;


            foreach (Mercury.Server.Application.EnvironmentRolePermission currentPermission in serverRole.Permissions) {

                permissions.Add (new RolePermission (application, currentPermission));

            }

            foreach (Mercury.Server.Application.EnvironmentRoleMembership currentMembership in serverRole.Membership) {

                membership.Add (new RoleMembership (application, currentMembership));

            }


            createAccountInfo = serverRole.CreateAccountInfo;

            modifiedAccountInfo = serverRole.ModifiedAccountInfo;


            return;

        }

        #endregion


        #region Public Methods

        public void AddPermission (Int64 permissionId, Boolean isGranted, Boolean isDenied) {

            RolePermission modifyPermission = Permission (permissionId);

            if (modifyPermission == null) {

                RolePermission newPermission = new RolePermission (application);

                newPermission.RoleId = roleId;

                newPermission.PermissionId = permissionId;

                newPermission.IsGranted = isGranted;

                newPermission.IsDenied = isDenied;

                permissions.Add (newPermission);

            }

            else {

                modifyPermission.IsGranted = isGranted;

                modifyPermission.IsDenied = isDenied;

            }

            return;

        }

        public void AddMembership (Int64 securityAuthorityId, String securityAuthorityName, String securityGroupId, String securityGroupName) {

            if (!MembershipExists (securityAuthorityId, securityGroupId)) { 

                RoleMembership newMembership = new RoleMembership (application);

                newMembership.RoleId = roleId;

                newMembership.SecurityAuthorityId = securityAuthorityId;

                newMembership.SecurityAuthorityName = securityAuthorityName;

                newMembership.SecurityGroupId = securityGroupId;

                newMembership.SecurityGroupName = securityGroupName;

                membership.Add (newMembership);

            }

            return;

        }

        public Boolean MembershipExists (Int64 securityAuthorityId, String securityGroupId) {

            Boolean membershipExists = false;

            foreach (RoleMembership currentMembership in membership) {

                if ((currentMembership.SecurityAuthorityId == securityAuthorityId) && (currentMembership.SecurityGroupId == securityGroupId)) {

                    membershipExists = true;

                    break;

                }

            }

            return membershipExists;

        }

        public RolePermission Permission (Int64 permissionId) {

            foreach (RolePermission currentPermission in permissions) {

                if (currentPermission.PermissionId == permissionId) { return currentPermission; }

            }

            return null;

        }

        public Mercury.Server.Application.EnvironmentRole ToServerObject () {

            Mercury.Server.Application.EnvironmentRole serverRole = new Mercury.Server.Application.EnvironmentRole ();

            serverRole.RoleId = roleId;

            serverRole.Name = roleName;

            serverRole.Description = description;


            serverRole.Permissions = new Mercury.Server.Application.EnvironmentRolePermission[permissions.Count];

            for (Int32 currentPermissionIndex = 0; currentPermissionIndex < permissions.Count; currentPermissionIndex++) {

                serverRole.Permissions[currentPermissionIndex] = permissions[currentPermissionIndex].ToServerObject ();

            }


            serverRole.Membership = new Mercury.Server.Application.EnvironmentRoleMembership[membership.Count];

            for (Int32 currentMembershipIndex = 0; currentMembershipIndex < membership.Count; currentMembershipIndex++) {

                serverRole.Membership[currentMembershipIndex] = membership[currentMembershipIndex].ToServerObject ();

            }


            serverRole.CreateAccountInfo = createAccountInfo;

            serverRole.ModifiedAccountInfo = modifiedAccountInfo;

            return serverRole;

        }

        public Role Copy () {

            return new Role (application, this.ToServerObject ());

        }

        public Boolean IsEqual (Role compareRole) {

            Boolean isEqual = true;


            isEqual = isEqual && (roleName == compareRole.Name);

            isEqual = isEqual && (description == compareRole.Description);


            isEqual = isEqual && (permissions.Count == compareRole.Permissions.Count);

            isEqual = isEqual && (membership.Count == compareRole.Membership.Count);


            if (isEqual) {

                for (Int32 permissionIndex = 0; permissionIndex < permissions.Count; permissionIndex++) {

                    isEqual = isEqual && permissions[permissionIndex].IsEqual (compareRole.Permissions[permissionIndex]);

                }

            }

            if (isEqual) {

                for (Int32 membershipIndex = 0; membershipIndex < membership.Count; membershipIndex++) {

                    isEqual = isEqual && membership[membershipIndex].IsEqual (compareRole.Membership[membershipIndex]);

                }

            }

            return isEqual;

        }

        #endregion

    }

}
