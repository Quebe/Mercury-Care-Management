using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Environment {

    [Serializable]
    public class RolePermission {
        
        #region Private Properties
        
        private Int64 roleId;

        private Int64 permissionId;

        private Boolean isGranted;

        private Boolean isDenied;


        private Mercury.Server.Application.AuthorityAccountStamp createAccountInfo = new Mercury.Server.Application.AuthorityAccountStamp ();

        private Mercury.Server.Application.AuthorityAccountStamp modifiedAccountInfo = new Mercury.Server.Application.AuthorityAccountStamp ();


        private Application application = null;

        #endregion


        #region Public Properties

        public Int64 RoleId { get { return roleId; } set { roleId = value; } }


        public Int64 PermissionId { get { return permissionId; } set { permissionId = value; } }

        public Boolean IsGranted { get { return isGranted; } set { isGranted = value; } }

        public Boolean IsDenied { get { return isDenied; } set { isDenied = value; } }


        public Mercury.Server.Application.AuthorityAccountStamp CreateAccountInfo { get { return createAccountInfo; } set { createAccountInfo = value; } }

        public Mercury.Server.Application.AuthorityAccountStamp ModifiedAccountInfo { get { return modifiedAccountInfo; } set { modifiedAccountInfo = value; } }


        public Application Application { get { return application; } set { application = value; } }

        #endregion


        #region Constructors

        public RolePermission (Application forApplication) { application = forApplication; return; }

        public RolePermission (Application forApplication, Mercury.Server.Application.EnvironmentRolePermission serverPermission) {

            application = forApplication;


            roleId = serverPermission.RoleId;

            permissionId = serverPermission.PermissionId;

            isGranted = serverPermission.IsGranted;

            isDenied = serverPermission.IsDenied;


            createAccountInfo = serverPermission.CreateAccountInfo;

            modifiedAccountInfo = serverPermission.ModifiedAccountInfo;


            return;

        }

        #endregion


        #region Public Methods

        public Mercury.Server.Application.EnvironmentRolePermission ToServerObject () {

            Mercury.Server.Application.EnvironmentRolePermission serverPermission = new Mercury.Server.Application.EnvironmentRolePermission ();


            serverPermission.RoleId = roleId;

            serverPermission.PermissionId = permissionId;

            serverPermission.IsGranted = isGranted;

            serverPermission.IsDenied = isDenied;


            serverPermission.CreateAccountInfo = createAccountInfo;

            serverPermission.ModifiedAccountInfo = modifiedAccountInfo;

            return serverPermission;

        }

        public Boolean IsEqual (RolePermission comparePermission) {

            Boolean isEqual = true;

            isEqual = isEqual && (permissionId == comparePermission.PermissionId);

            isEqual = isEqual && (isGranted == comparePermission.IsGranted);

            isEqual = isEqual && (isDenied == comparePermission.IsDenied);

            return isEqual;

        }

        #endregion

    }

}
