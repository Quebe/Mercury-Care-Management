using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Mercury.Server.Services.Responses;
using Mercury.Server.Services.Responses.Objects;
using Mercury.Server.Services.Responses.SearchResults;
using Mercury.Server.Services.Responses.TypedCollections;

namespace Mercury.Server.Services.Core {

    /// <summary>
    /// Mercury Server Application Service
    /// </summary>
    public class Application : IApplication {

        private CacheManager cacheManager = new CacheManager ();


        #region Version

        public String VersionServer (String token) {

            String version = String.Empty;

            Mercury.Server.Application application = null;


            application = cacheManager.GetApplication (token);

            if (application != null) {

                version = application.Version;

            }

            return version;

        }

        #endregion


        #region Session

        public Server.Session SessionGet (String token) {

            Server.Session session = null;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                session = application.Session;

            }

            catch (Exception applicationException) {

                if (application != null) {

                    application.SetLastException (applicationException);

                }

            }

            return session;

        }

        public AuditAuthenticationCollectionResponse ActiveSessionsAvailable (String token) {

            AuditAuthenticationCollectionResponse response = new AuditAuthenticationCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                if ((!application.HasEnterprisePermission (Server.EnterprisePermissions.ActiveSessionsReview)) &&

                    (!application.HasEnterprisePermission (Server.EnterprisePermissions.ActiveSessionsManage))) {

                    throw new ApplicationException ("Permission Denied.");

                }

                response.Collection = application.ActiveSessionsAvailable ();

                if (application.LastException != null) { throw application.LastException; }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        #endregion 
        

        #region Enumerations - Expose Internal Server-side Enumerations for Client Use

        public void Enumeration_DataExplorerNodeResultDataType (Server.Core.DataExplorer.Enumerations.DataExplorerNodeResultDataType e) { return; }

        #endregion 


        #region Enterprise Permissions

        public StringListResponse EnterprisePermissionList (String token) {

            Mercury.Server.Services.Responses.StringListResponse response = new Mercury.Server.Services.Responses.StringListResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.ResultList = application.EnterprisePermissionList ();

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public DictionaryResponse EnterprisePermissionDictionary (String token) {

            DictionaryResponse response = new DictionaryResponse ();

            Dictionary<Int64, String> permissionDictionary = new Dictionary<Int64, String> ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Dictionary = application.EnterprisePermissionDictionary ();

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public PermissionCollectionResponse EnterprisePermissionsAvailable (String token) {

            PermissionCollectionResponse response = new PermissionCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.EnterprisePermissionsAvailable ();

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public SecurityGroupPermissionCollectionResponse SecurityGroupEnterprisePermissionsGet (String token, String securityAuthorityName, String securityGroupId) {

            SecurityGroupPermissionCollectionResponse response = new SecurityGroupPermissionCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.SecurityGroupEnterprisePermissionsGet (securityAuthorityName, securityGroupId);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public BooleanResponse SecurityGroupEnterprisePermissionSave (String token, Int64 securityAuthorityId, String securityGroupId, Int64 permissionId, Boolean isGranted, Boolean isDenied) {

            BooleanResponse response = new BooleanResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Result = application.SecurityGroupEnterprisePermissionSave (securityAuthorityId, securityGroupId, permissionId, isGranted, isDenied);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        #endregion


        #region Security Authority

        public SecurityAuthorityCollectionResponse SecurityAuthoritiesAvailable (String token) {

            SecurityAuthorityCollectionResponse response = new SecurityAuthorityCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.SecurityAuthoritiesAvailable (true);

                if (application.LastException != null) { throw application.LastException; }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public DictionaryResponse SecurityAuthorityDictionary (String token) {

            DictionaryResponse response = new DictionaryResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                response.Dictionary = application.SecurityAuthorityDictionary ();

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public DirectoryEntryCollectionResponse SecurityAuthorityGroupGetMembership (String token, Int64 securityAuthorityId, String securityGroupId) {

            DirectoryEntryCollectionResponse response = new DirectoryEntryCollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                Server.Security.SecurityAuthority securityAuthority = application.SecurityAuthorityGet (securityAuthorityId);

                Server.Public.Interfaces.Security.IProvider securityProvider = application.SecurityAuthorityProviderGet (securityAuthority);

                securityProvider.Credentials.UserAccountId = application.Session.UserAccountId;

                securityProvider.Credentials.UserAccountName = application.Session.UserAccountName;

                response.Collection = securityProvider.GetSecurityGroupMembership (securityGroupId);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }


        public Mercury.Server.Security.SecurityAuthority SecurityAuthorityGet (String token, Int64 securityAuthorityId) {

            Mercury.Server.Security.SecurityAuthority securityAuthority = null;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                securityAuthority = application.SecurityAuthorityGet (securityAuthorityId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return securityAuthority;

        }

        public Mercury.Server.Security.SecurityAuthority SecurityAuthorityGetByName (String token, String securityAuthorityName) {

            Mercury.Server.Security.SecurityAuthority securityAuthority = null;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                securityAuthority = application.SecurityAuthorityGet (securityAuthorityName);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return securityAuthority;

        }


        public BooleanResponse SecurityAuthoritySave (String token, Mercury.Server.Security.SecurityAuthority securityAuthority) {

            BooleanResponse response = new BooleanResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                securityAuthority.Application = application;

                response.Result = securityAuthority.Save ();

                if (!response.Result) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public BooleanResponse SecurityAuthorityDelete (String token, String securityAuthorityName) {

            BooleanResponse response = new BooleanResponse ();

            Mercury.Server.Application application = null;

            Mercury.Server.Security.SecurityAuthority securityAuthority;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                if (!application.HasEnterprisePermission (Server.EnterprisePermissions.SecurityAuthorityManage)) {

                    throw new ApplicationException ("Permission Denied.");

                }

                securityAuthority = application.SecurityAuthorityGet (securityAuthorityName);

                response.Result = securityAuthority.Delete (application.EnterpriseDatabase);

                if (!response.Result) { response.SetException (application.EnterpriseDatabase.LastException); }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;
        }



        #region Security Authority Provider

        public DirectoryEntryCollectionResponse SecurityAuthorityProviderBrowseDirectory (String token, String securityAuthorityName, String directoryPath) {

            DirectoryEntryCollectionResponse response = new DirectoryEntryCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                Mercury.Server.Public.Interfaces.Security.IProvider provider;

                Mercury.Server.Security.SecurityAuthority securityAuthority = application.SecurityAuthorityGet (securityAuthorityName);


                provider = application.SecurityAuthorityProviderGet (securityAuthority);

                provider.Credentials.Protocol = securityAuthority.Protocol;

                provider.Credentials.Domain = securityAuthority.Domain;

                provider.Credentials.ServerName = securityAuthority.ServerName;

                provider.Credentials.SetAgentCredentials (securityAuthority.AgentName, securityAuthority.AgentPassword);

                provider.Credentials.UserAccountId = application.Session.UserAccountId;

                provider.Credentials.UserAccountName = application.Session.UserAccountName;


                if (provider.Capabilities.CanBrowseDirectory) {

                    response.Collection = provider.BrowseDirectory (directoryPath);

                }

                else {

                    response.Collection = new List<Mercury.Server.Public.Interfaces.Security.DirectoryEntry> ();

                }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        #endregion


        #region Security Authority Groups

        public DictionaryStringResponse SecurityAuthoritySecurityGroupDictionary (String token, Int64 securityAuthorityId) {

            DictionaryStringResponse response = new DictionaryStringResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Dictionary = application.SecurityAuthoritySecurityGroupDictionary (securityAuthorityId);

                if (application.LastException != null) { throw application.LastException; }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public SecurityGroupCollectionResponse SecurityAuthoritySecurityGroups (String token, Int64 securityAuthorityId) {

            SecurityGroupCollectionResponse response = new SecurityGroupCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.SecurityAuthoritySecurityGroups (securityAuthorityId);

                if (application.LastException != null) { throw application.LastException; }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public Mercury.Server.Public.Interfaces.Security.SecurityGroup SecurityAuthoritySecurityGroupGet (String token, String securityAuthorityName, String securityGroupId) {

            Mercury.Server.Public.Interfaces.Security.SecurityGroup securityGroup = null;


            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                Mercury.Server.Public.Interfaces.Security.IProvider provider;

                Mercury.Server.Security.SecurityAuthority securityAuthority = application.SecurityAuthorityGet (securityAuthorityName);

                provider = application.SecurityAuthorityProviderGet (securityAuthority);

                provider.Credentials.UserAccountId = application.Session.UserAccountId;

                provider.Credentials.UserAccountName = application.Session.UserAccountName;

                securityGroup = application.GetSecurityGroup (provider, securityGroupId);

            }

            catch (Exception applicationException) {

                securityGroup = null;

                System.Diagnostics.Debug.WriteLine (applicationException.Message);


            }

            return securityGroup;

        }

        public DirectoryEntryCollectionResponse SecurityAuthoritySecurityGroupMembership (String token, String securityAuthorityName, String securityGroupId) {

            DirectoryEntryCollectionResponse response = new DirectoryEntryCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                Mercury.Server.Public.Interfaces.Security.IProvider provider;

                Mercury.Server.Security.SecurityAuthority securityAuthority = application.SecurityAuthorityGet (securityAuthorityName);

                provider = application.SecurityAuthorityProviderGet (securityAuthority);

                provider.Credentials.UserAccountId = application.Session.UserAccountId;

                provider.Credentials.UserAccountName = application.Session.UserAccountName;

                response.Collection = provider.GetSecurityGroupMembership (securityGroupId);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        #endregion 

        #endregion


        #region Environment

        public EnvironmentAccessCollectionResponse EnvironmentAccessGetByEnvironmentName (String token, String environmentName) {

            EnvironmentAccessCollectionResponse response = new EnvironmentAccessCollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                response.Collection = application.EnvironmentAccessGetByEnvironmentName (environmentName);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public EnvironmentAccessCollectionResponse SecurityGroupEnvironmentAccessGet (String token, String securityAuthorityName, String securityGroupId) {

            EnvironmentAccessCollectionResponse response = new EnvironmentAccessCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.SecurityGroupEnvironmentAccessGet (securityAuthorityName, securityGroupId);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public BooleanResponse SecurityGroupEnvironmentAccessSave (String token, Int64 securityAuthorityId, String securityGroupId, Int64 environmentId, Boolean isGranted, Boolean isDenied) {

            BooleanResponse response = new BooleanResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Result = application.SecurityGroupEnvironmentAccessSave (securityAuthorityId, securityGroupId, environmentId, isGranted, isDenied);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }


        public Mercury.Server.Environment.Environment EnvironmentGet (String token, Int64 environmentId) {

            Mercury.Server.Environment.Environment environment = null;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                environment = application.EnvironmentGet (environmentId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return environment;

        }

        public Mercury.Server.Environment.Environment EnvironmentGetByName (String token, String environmentName) {

            Mercury.Server.Environment.Environment environment = null;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                environment = application.EnvironmentGet (environmentName);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return environment;

        }

        public BooleanResponse EnvironmentSave (String token, Mercury.Server.Environment.Environment environment) {

            BooleanResponse response = new BooleanResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                if (!application.HasEnterprisePermission (Server.EnterprisePermissions.EnvironmentManage)) {

                    throw new ApplicationException ("Permission Denied.");

                }


                environment.Application = application;

                response.Result = environment.Save ();

                if (!response.Result) { response.SetException (application.EnterpriseDatabase.LastException); }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public BooleanResponse EnvironmentDelete (String token, String environmentName) {

            BooleanResponse response = new BooleanResponse ();

            Mercury.Server.Application application = null;

            Mercury.Server.Environment.Environment environment;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                if (!application.HasEnterprisePermission (Server.EnterprisePermissions.EnvironmentManage)) {

                    throw new ApplicationException ("Permission Denied.");

                }

                environment = application.EnvironmentGet (environmentName);

                response.Result = environment.Delete (application.EnterpriseDatabase);

                if (!response.Result) { response.SetException (application.EnterpriseDatabase.LastException); }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }


        public StringListResponse EnvironmentList (String token) {

            Mercury.Server.Services.Responses.StringListResponse response = new Mercury.Server.Services.Responses.StringListResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                response.ResultList.AddRange (application.EnvironmentDictionary ().Values);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public DictionaryResponse EnvironmentDictionary (String token) {

            Mercury.Server.Services.Responses.DictionaryResponse response = new Mercury.Server.Services.Responses.DictionaryResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Dictionary = application.EnvironmentDictionary ();

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);
                
            }

            return response;

        }

        public EnvironmentCollectionResponse EnvironmentsAvailable (String token) {

            EnvironmentCollectionResponse response = new EnvironmentCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.EnvironmentsAvailable ();

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);
                
            }

            return response;

        }


        public StringListResponse EnvironmentPermissionList (String token, String environmentName) {

            Mercury.Server.Services.Responses.StringListResponse response = new Mercury.Server.Services.Responses.StringListResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.ResultList = application.EnvironmentPermissionList (environmentName);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public PermissionCollectionResponse EnvironmentPermissionsAvailable (String token, String environmentName) {

            PermissionCollectionResponse response = new PermissionCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.EnvironmentPermissionsAvailable (environmentName);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public BooleanResponse HasEnvironmentPermissionByEnvironment (String token, String environmentName, String permissionName) {

            BooleanResponse response = new BooleanResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Result = application.HasEnvironmentPermission (environmentName, permissionName);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public StringListResponse EnvironmentRoleList (String token, String environmentName) {

            Mercury.Server.Services.Responses.StringListResponse response = new Mercury.Server.Services.Responses.StringListResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.ResultList.AddRange (application.EnvironmentRoleDictionary (environmentName).Values);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public DictionaryResponse EnvironmentRoleDictionary (String token, String environmentName) {

            Mercury.Server.Services.Responses.DictionaryResponse response = new Mercury.Server.Services.Responses.DictionaryResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Dictionary = application.EnvironmentRoleDictionary (environmentName);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public RoleCollectionResponse EnvironmentRolesAvailable (String token, String environmentName) {

            RoleCollectionResponse response = new RoleCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.EnvironmentRolesAvailable (environmentName);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public RolePermissionCollectionResponse EnvironmentRoleGetPermissions (String token, String environmentName, String roleName) {

            RolePermissionCollectionResponse response = new RolePermissionCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.EnvironmentRolePermissionsGet (environmentName, roleName);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public RoleMembershipCollectionResponse EnvironmentRoleGetMembership (String token, String environmentName, String roleName) {

            RoleMembershipCollectionResponse response = new RoleMembershipCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.EnvironmentRoleMembershipGet (environmentName, roleName);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public Mercury.Server.Environment.Role EnvironmentRoleGetByEnvironment (String token, String environmentName, String roleName) {

            if (String.IsNullOrEmpty (roleName)) { return null; }


            Mercury.Server.Environment.Role role = null;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                if (application.HasEnvironmentPermission (environmentName, Server.EnvironmentPermissions.RoleReview)) {

                    role = application.EnvironmentRoleGet (environmentName, roleName);

                }

                if (role == null) { throw new ApplicationException (application.LastException.Message); }

            }

            catch (Exception applicationException) {

                if (application != null) { application.SetLastException (applicationException); }

            }

            return role;

        }

        public ObjectSaveResponse EnvironmentRoleSaveByEnvironment (String token, String environmentName, Mercury.Server.Environment.Role environmentRole) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                if (application.HasEnvironmentPermission (environmentName, Server.EnvironmentPermissions.RoleManage)) {

                    response.Success = environmentRole.Save (application.EnvironmentDatabaseByName (environmentName));

                    if ((!response.Success) && (application.EnvironmentDatabaseByName (environmentName).LastException != null)) {

                        response.SetException (application.EnvironmentDatabaseByName (environmentName).LastException);

                    }

                }

                else {

                    response.Success = false;

                    response.SetException (new ApplicationException ("Permission Denied."));

                }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);


            }

            return response;

        }

        public BooleanResponse EnvironmentRoleSetMembership (String token, String environmentName, String roleName, System.Collections.Generic.List<Mercury.Server.Environment.RoleMembership> roleMembership) {

            BooleanResponse response = new BooleanResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                if (application.HasEnvironmentPermission (environmentName, Server.EnvironmentPermissions.RoleManage)) {

                    response.Result = application.EnvironmentRoleSetMembership (environmentName, roleName, roleMembership);

                    if ((!response.Result) && (application.LastException != null)) {

                        response.SetException (application.LastException);

                    }

                }

                else {

                    response.Result = false;

                    response.SetException (new ApplicationException ("Permission Denied."));

                }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public BooleanResponse EnvironmentRoleSetPermission (String token, String environmentName, String roleName, Int64 permissionId, Boolean isGranted, Boolean isDenied) {

            BooleanResponse response = new BooleanResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Result = application.EnvironmentRoleSetPermission (environmentName, roleName, permissionId, isGranted, isDenied);

                if ((!response.Result) && (application.LastException != null)) { throw new ApplicationException (application.LastException.Message); }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        #endregion


        #region Core Objects

        public DictionaryResponse CoreObjectDictionary (String token, String objectType) {

            DictionaryResponse response = new DictionaryResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                response.Dictionary = application.CoreObjectDictionary (objectType);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public DictionaryStringResponse CoreObject_Validate (String token, Server.Core.CoreObject coreObject) {

            DictionaryStringResponse response = new DictionaryStringResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                coreObject.Application = application;

                response.Dictionary = coreObject.Validate ();

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public DictionaryStringResponse CoreConfigurationObject_Validate (String token, Server.Core.CoreConfigurationObject coreConfigurationObject) {

            DictionaryStringResponse response = new DictionaryStringResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                coreConfigurationObject.Application = application;

                response.Dictionary = coreConfigurationObject.Validate ();

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public DictionaryStringResponse CoreObject_DataBindingContexts (String token, Server.Core.CoreObject coreObject) {

            DictionaryStringResponse response = new DictionaryStringResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                coreObject.Application = application;

                response.Dictionary = coreObject.DataBindingContexts;

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public StringResponse CoreObject_EvaluateDataBinding (String token, Server.Core.CoreObject coreObject, String bindingContext) {

            StringResponse response = new StringResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                coreObject.Application = application;

                response.Value = coreObject.EvaluateDataBinding (bindingContext);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public StringResponse CoreObject_XmlSerialize (String token, Server.Core.CoreObject coreObject) {

            StringResponse response = new StringResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                coreObject.Application = application;

                response.Value = coreObject.XmlSerialize ().OuterXml;

                if (application.LastException != null) { throw application.LastException; }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public ImportExportResponse CoreObject_XmlImport (String token, String serializedXml) {

            ImportExportResponse response = new ImportExportResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                System.Xml.XmlDocument objectDocument = new System.Xml.XmlDocument ();

                objectDocument.LoadXml (serializedXml);


                response = new ImportExportResponse (application.XmlConfigurationImport (objectDocument));
                
            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        #endregion 

        
        #region Core Reference

        #region Core Reference - Contact Regarding

        public ContactRegardingCollectionResponse ContactRegardingsAvailable (String token) {

            ContactRegardingCollectionResponse response = new ContactRegardingCollectionResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Collection = application.ContactRegardingsAvailable ();

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public DictionaryResponse ContactRegardingDictionary (String token) {

            DictionaryResponse response = new DictionaryResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Dictionary = application.CoreObjectDictionary ("ContactRegarding");

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Reference.ContactRegarding ContactRegardingGet (String token, Int64 contactRegardingId) {

            Server.Core.Reference.ContactRegarding contactRegarding = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS NEEDED, OPEN TO ALL USERS

                contactRegarding = application.ContactRegardingGet (contactRegardingId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return contactRegarding;

        }

        public Server.Core.Reference.ContactRegarding ContactRegardingGetByName (String token, String contactRegardingName) {

            Server.Core.Reference.ContactRegarding contactRegarding = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS NEEDED, OPEN TO ALL USERS

                contactRegarding = application.ContactRegardingGet (contactRegardingName);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return contactRegarding;

        }

        public ObjectSaveResponse ContactRegardingSave (String token, Server.Core.Reference.ContactRegarding contactRegarding) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                // VALIDATE PERMISSIONS

                Boolean hasPermission = application.HasEnvironmentPermission (Server.EnvironmentPermissions.ContactRegardingManage);

                if (hasPermission) {

                    response.Success = contactRegarding.Save (application);

                    response.Id = contactRegarding.Id;

                    if (!response.Success) { response.SetException (application.LastException); }

                }

                else { response.SetException (new ApplicationException ("Permission Denied.")); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion


        #region Core Reference - Correspondence

        public CorrespondenceCollectionResponse CorrespondencesAvailable (String token) {

            CorrespondenceCollectionResponse response = new CorrespondenceCollectionResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Collection = application.CorrespondencesAvailable ();

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public DictionaryResponse CorrespondenceDictionary (String token) {

            DictionaryResponse response = new DictionaryResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Dictionary = application.CoreObjectDictionary ("Correspondence");

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Reference.Correspondence CorrespondenceGet (String token, Int64 correspondenceId) {

            Server.Core.Reference.Correspondence correspondence = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS NEEDED, OPEN TO ALL USERS

                correspondence = application.CorrespondenceGet (correspondenceId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return correspondence;

        }

        public Server.Core.Reference.Correspondence CorrespondenceGetByName (String token, String correspondenceName) {

            Server.Core.Reference.Correspondence correspondence = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS NEEDED, OPEN TO ALL USERS

                correspondence = application.CorrespondenceGet (correspondenceName);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return correspondence;

        }

        public ObjectSaveResponse CorrespondenceSave (String token, Server.Core.Reference.Correspondence correspondence) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                // VALIDATE PERMISSIONS

                Boolean hasPermission = application.HasEnvironmentPermission (Server.EnvironmentPermissions.CorrespondenceManage);

                if (hasPermission) {

                    response.Success = correspondence.Save (application);

                    response.Id = correspondence.Id;

                    if (!response.Success) { response.SetException (application.LastException); }

                }

                else { response.SetException (new ApplicationException ("Permission Denied.")); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }


        public Server.Core.Reference.CorrespondenceContent CorrespondenceContentGet (String token, Int64 correspondenceContentId) {

            Server.Core.Reference.CorrespondenceContent correspondenceContent = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS NEEDED, OPEN TO ALL USERS

                correspondenceContent = application.CorrespondenceContentGet (correspondenceContentId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return correspondenceContent;

        }

        #endregion


        #region Core Reference - Note Type

        public NoteTypeCollectionResponse NoteTypesAvailable (String token) {

            NoteTypeCollectionResponse response = new NoteTypeCollectionResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS
                
                response.Collection = application.NoteTypesAvailable ();

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public DictionaryResponse NoteTypeDictionary (String token) {

            DictionaryResponse response = new DictionaryResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Dictionary = application.CoreObjectDictionary ("NoteType"); 

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Reference.NoteType NoteTypeGet (String token, Int64 noteTypeId) {

            Server.Core.Reference.NoteType noteType = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS NEEDED, OPEN TO ALL USERS

                noteType = application.NoteTypeGet (noteTypeId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return noteType;

        }

        public Server.Core.Reference.NoteType NoteTypeGetByName (String token, String noteTypeName) {

            Server.Core.Reference.NoteType noteType = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS NEEDED, OPEN TO ALL USERS

                noteType = application.NoteTypeGet (noteTypeName);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return noteType;

        }

        public ObjectSaveResponse NoteTypeSave (String token, Server.Core.Reference.NoteType noteType) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                // VALIDATE PERMISSIONS

                Boolean hasPermission = application.HasEnvironmentPermission (Server.EnvironmentPermissions.NoteTypeManage);

                if (hasPermission) {

                    response.Success = noteType.Save (application);

                    response.Id = noteType.Id;

                    if (!response.Success) { response.SetException (application.LastException); }

                }

                else { response.SetException (new ApplicationException ("Permission Denied.")); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion

        
        #region Geographic References

        public StringListResponse StateReference (String token) {

            StringListResponse response = new StringListResponse ();


            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.ResultList = application.StateReference ();

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public String StateReferenceByZipCode (String token, String zipCode) {

            String response = String.Empty;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response = application.StateReferenceByZipCode (zipCode);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return response;

        }

        public CityStateZipCodeViewCollectionResponse CityReferenceByState (String token, String state) {

            CityStateZipCodeViewCollectionResponse response = new CityStateZipCodeViewCollectionResponse ();


            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.CityReferenceByState (state);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Reference.Views.CityStateZipCodeView CityStateReferenceByZipCode (String token, String zipCode) {

            Server.Core.Reference.Views.CityStateZipCodeView response = new Server.Core.Reference.Views.CityStateZipCodeView ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response = application.CityStateReferenceByZipCode (zipCode);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return response;

        }

        public String CityReferenceByZipCode (String token, String zipCode) {

            String response = String.Empty;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response = application.CityReferenceByZipCode (zipCode);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return response;

        }

        public StringListResponse CityReferenceByStateCityName (String token, String state, String cityName) {

            StringListResponse response = new StringListResponse ();


            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.ResultList = application.CityReferenceByState (state, cityName);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public StringListResponse CountyReferenceByState (String token, String state) {

            StringListResponse response = new StringListResponse ();


            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.ResultList = application.CountyReferenceByState (state);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public String CountyReferenceByZipCode (String token, String zipCode) {

            String response = String.Empty;


            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response = application.CountyReferenceByZipCode (zipCode);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return response;

        }

        #endregion

        #endregion


        #region Code Reference - Old

        public System.Collections.Generic.Dictionary<String, String> DiagnosisDictionary (String token, String diagnosisPrefix, Int32 version) {

            System.Collections.Generic.Dictionary<String, String> collection = new Dictionary<String, String> ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                collection = application.DiagnosisDictionary (diagnosisPrefix, version);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return collection;

        }

        public String DiagnosisDescription (String token, String diagnosisCode, Int32 version) {

            String description = String.Empty;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                description = application.DiagnosisDescription (diagnosisCode, version);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return description;

        }

        public System.Collections.Generic.Dictionary<String, String> ProcedureCodeDictionary (String token, String procedureCodePrefix) {

            System.Collections.Generic.Dictionary<String, String> collection = new Dictionary<String, String> ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                collection = application.ProcedureCodeDictionary (procedureCodePrefix);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return collection;

        }

        public System.Collections.Generic.Dictionary<String, String> RevenueCodeDictionary (String token, String revenueCodePrefix) {

            System.Collections.Generic.Dictionary<String, String> collection = new Dictionary<String, String> ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                collection = application.RevenueCodeDictionary (revenueCodePrefix);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return collection;

        }

        public System.Collections.Generic.Dictionary<String, String> BillTypeDictionary (String token, String billTypePrefix) {

            System.Collections.Generic.Dictionary<String, String> collection = new Dictionary<String, String> ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                collection = application.BillTypeDictionary (billTypePrefix);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return collection;

        }

        public System.Collections.Generic.Dictionary<String, String> Icd9ProcedureCodeDictionary (String token, String icd9ProcedureCodePrefix) {

            System.Collections.Generic.Dictionary<String, String> collection = new Dictionary<String, String> ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                collection = application.Icd9ProcedureCodeDictionary (icd9ProcedureCodePrefix);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return collection;

        }

        #endregion 
        

        #region Actions

        public ActionCollectionResponse ActionsAvailable (String token) {

            ActionCollectionResponse response = new ActionCollectionResponse ();


            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.ActionsAvailable ();

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion


        #region Authorizations

        public AuthorizationTypeCollectionResponse AuthorizationTypesAvailable (String token) {

            AuthorizationTypeCollectionResponse response = new AuthorizationTypeCollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.AuthorizationTypesAvailable ();

                if (application.LastException != null) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException); 


            }

            return response;

        }

        public Server.Core.Authorizations.AuthorizationType AuthorizationTypeGet (String token, Int64 authorizationTypeId) {

            Server.Core.Authorizations.AuthorizationType authorizationType = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                authorizationType = new Server.Core.Authorizations.AuthorizationType (application, authorizationTypeId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return authorizationType;

        }


        #region Member Authorizations

        public Int64 MemberAuthorizationsGetCount (String token, Int64 memberId) {

            Int64 itemCount = 0;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                itemCount = application.MemberAuthorizationsGetCount (memberId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return itemCount;

        }

        public AuthorizationCollectionResponse MemberAuthorizationsGetByPage (String token, Int64 memberId, Int32 initialRow, Int32 count) {

            AuthorizationCollectionResponse response = new AuthorizationCollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.MemberAuthorizationsGetByPage (memberId, initialRow, count);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public AuthorizationLineCollectionResponse AuthorizationLineGetByAuthorization (String token, Int64 authorizationId) {

            AuthorizationLineCollectionResponse response = new AuthorizationLineCollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.AuthorizationLinesGet (authorizationId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion 

        #endregion 


        #region Core - Authorized Services

        public AuthorizedServiceCollectionResponse AuthorizedServicesAvailable (String token) {

            AuthorizedServiceCollectionResponse response = new AuthorizedServiceCollectionResponse ();


            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.AuthorizedServicesAvailable ();

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.AuthorizedServices.AuthorizedService AuthorizedServiceGet (String token, Int64 authorizedServiceId) {

            Server.Core.AuthorizedServices.AuthorizedService authorizedService = null;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                authorizedService = new Mercury.Server.Core.AuthorizedServices.AuthorizedService (application, authorizedServiceId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return authorizedService;

        }

        public ObjectSaveResponse AuthorizedServiceSave (String token, Server.Core.AuthorizedServices.AuthorizedService authorizedService) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                authorizedService.Application = application;

                response.Success = authorizedService.Save ();

                response.Id = authorizedService.Id;

                if (!response.Success) {

                    response.SetException (application.LastException);

                }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public MemberAuthorizedServiceDetailCollectionResponse AuthorizedServicePreview (String token, Server.Core.AuthorizedServices.AuthorizedService authorizedService) {

            MemberAuthorizedServiceDetailCollectionResponse response = new MemberAuthorizedServiceDetailCollectionResponse ();


            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                authorizedService.Application = application;

                response.Collection = authorizedService.Preview (application);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }


        public Int64 MemberAuthorizedServicesGetCount (String token, Int64 memberId, Boolean showHidden) {

            Int64 servicesCount = 0;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                servicesCount = application.MemberAuthorizedServicesGetCount (memberId, showHidden);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return servicesCount;

        }

        public MemberAuthorizedServiceCollectionResponse MemberAuthorizedServicesGetByPage (String token, Int64 memberId, Int32 initialRow, Int32 count, Boolean showHidden) {

            MemberAuthorizedServiceCollectionResponse response = new MemberAuthorizedServiceCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.MemberAuthorizedServicesGetByPage (memberId, initialRow, count, showHidden);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public MemberAuthorizedServiceDetailCollectionResponse MemberAuthorizedServiceDetailsGet (String token, Int64 memberAuthorizedServiceId) {

            MemberAuthorizedServiceDetailCollectionResponse response = new MemberAuthorizedServiceDetailCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.MemberAuthorizedServiceDetailsGet (memberAuthorizedServiceId);

                if (application.LastException != null) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion


        #region Claims

        #region Member Claims

        public Int64 MemberClaimsGetCount (String token, Int64 memberId) {

            Int64 itemCount = 0;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                itemCount = application.MemberClaimsGetCount (memberId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return itemCount;

        }

        public ClaimCollectionResponse MemberClaimsGetByPage (String token, Int64 memberId, Int32 initialRow, Int32 count) {

            ClaimCollectionResponse response = new ClaimCollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.MemberClaimsGetByPage (memberId, initialRow, count);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public ClaimLineCollectionResponse ClaimLinesGet (String token, Int64 claimId) {

            ClaimLineCollectionResponse response = new ClaimLineCollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.ClaimLinesGet (claimId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }


        public Int64 MemberPharmacyClaimsGetCount (String token, Int64 memberId) {

            Int64 itemCount = 0;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                itemCount = application.MemberPharmacyClaimsGetCount (memberId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return itemCount;

        }

        public PharmacyClaimCollectionResponse MemberPharmacyClaimsGetByPage (String token, Int64 memberId, Int32 initialRow, Int32 count) {

            PharmacyClaimCollectionResponse response = new PharmacyClaimCollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.MemberPharmacyClaimsGetByPage (memberId, initialRow, count);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }


        public Int64 MemberLabResultsGetCount (String token, Int64 memberId) {

            Int64 itemCount = 0;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                itemCount = application.MemberLabResultsGetCount (memberId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return itemCount;

        }

        public LabResultCollectionResponse MemberLabResultsGetByPage (String token, Int64 memberId, Int32 initialRow, Int32 count) {

            LabResultCollectionResponse response = new LabResultCollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.MemberLabResultsGetByPage (memberId, initialRow, count);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion

        #endregion 


        #region Core - Condition

        public ConditionClassCollectionResponse ConditionClassesAvailable (String token) {

            ConditionClassCollectionResponse response = new ConditionClassCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.ConditionClassesAvailable ();

                if (application.LastException != null) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public ConditionCollectionResponse ConditionsAvailable (String token) {

            ConditionCollectionResponse response = new ConditionCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.ConditionsAvailable ();

                if (application.LastException != null) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Condition.Condition ConditionGet (String token, Int64 conditionId) {

            Mercury.Server.Core.Condition.Condition condition = null;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                condition = new Mercury.Server.Core.Condition.Condition (application, conditionId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return condition;

        }

        public ObjectSaveResponse ConditionSave (String token, Mercury.Server.Core.Condition.Condition condition) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                condition.Application = application;

                response.Success = condition.Save (application);

                response.Id = condition.Id;

                if (!response.Success) {

                    response.SetException (application.LastException);

                }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion 


        #region Core - Entity

        #region Entity

        public Server.Core.Entity.Entity EntityGet (String token, Int64 entityId) {

            Server.Core.Entity.Entity entity = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                
                entity = application.EntityGet (entityId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return entity;

        }

        #endregion 

        
        #region Entity Address

        public Server.Core.Entity.EntityAddress EntityAddressGet (String token, Int64 entityAddressId) {

            Server.Core.Entity.EntityAddress entityContact = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                entityContact = application.EntityAddressGet (entityAddressId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return entityContact;

        }

        public Server.Core.Entity.EntityAddress EntityAddressGetByTypeDate (String token, Int64 entityId, Server.Core.Enumerations.EntityAddressType addressType, DateTime forDate) {

            Server.Core.Entity.EntityAddress entityContact = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                entityContact = application.EntityAddressGetByEntityTypeDate (entityId, addressType, forDate);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return entityContact;

        }

        public EntityAddressCollectionResponse EntityAddressesGet (String token, Int64 entityId) {

            EntityAddressCollectionResponse response = new EntityAddressCollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.EntityAddressesGet (entityId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return response;

        }


        public ObjectSaveResponse EntityAddressSave (String token, Server.Core.Entity.EntityAddress entityAddress) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                entityAddress.Application = application;


                // ENSURE VALID ENTITY RECORD 

                if (entityAddress.Entity == null) { throw new ApplicationException ("Permission Denied. Unable to determine appropriate Entity for Address."); }


                // ENSURE THAT USER HAS MANAGE RIGHTS IF EXISTING ADDRESS

                if ((entityAddress.Id != 0) && (!application.HasEnvironmentPermission ("Environment." + entityAddress.Entity.EntityType.ToString () + ".Address.Manage"))) {

                    throw new ApplicationException ("Permission Denied. Unable to modify existing Address for " + entityAddress.Entity.EntityType.ToString () + ".");

                }


                // ENSURE THAT USER HAS ADD RIGHTS IF NEW NOTE

                if ((entityAddress.Id == 0) && (!application.HasEnvironmentPermission ("Environment." + entityAddress.Entity.EntityType.ToString () + ".Address.Manage"))) {

                    throw new ApplicationException ("Permission Denied. Unable to add new Address for " + entityAddress.Entity.EntityType.ToString () + ".");

                }


                response.Success = entityAddress.Save ();

                response.Id = entityAddress.Id;

                if (!response.Success) {

                    response.SetException (application.LastException);

                }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public BooleanResponse EntityAddressTerminate (String token, Server.Core.Entity.EntityAddress entityAddress, DateTime terminationDate) {

            BooleanResponse response = new BooleanResponse ();

            Server.Application application = null;


            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                // ENSURE VALID ENTITY RECORD 

                if (entityAddress.Entity == null) { throw new ApplicationException ("Permission Denied. Unable to determine appropriate Entity for Address."); }


                // ENSURE THAT USER HAS TERMINATE RIGHTS IF EXISTING ADDRESS

                if (((entityAddress.Id != 0) && (!application.HasEnvironmentPermission ("Environment." + entityAddress.Entity.EntityType.ToString () + ".Address.Terminate")))

                    && ((entityAddress.Id != 0) && (!application.HasEnvironmentPermission ("Environment." + entityAddress.Entity.EntityType.ToString () + ".Address.Modify")))) {

                    throw new ApplicationException ("Permission Denied. Unable to terminate existing Address for " + entityAddress.Entity.EntityType.ToString () + ".");

                }

                entityAddress.Application = application;

                response.Result = entityAddress.Terminate (terminationDate);

                if (!response.Result) {

                    response.SetException (application.LastException);

                }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion 

        
        #region Entity Contact

        public Server.Core.Entity.EntityContact EntityContactGet (String token, Int64 entityContactId) {

            Server.Core.Entity.EntityContact entityContact = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                entityContact = application.EntityContactGet (entityContactId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return entityContact;

        }

        public Int64 EntityContactsGetCount (String token, Int64 entityId) {

            Int64 itemCount = 0;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                itemCount = application.EntityContactsGetCount (entityId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return itemCount;

        }

        public EntityContactCollectionResponse EntityContactsGetByPage (String token, Int64 entityId, Int32 initialRow, Int32 count) {

            EntityContactCollectionResponse response = new EntityContactCollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.EntityContactsGetByPage (entityId, initialRow, count);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }


        public ObjectSaveResponse EntityContactSave (String token, Server.Core.Entity.EntityContact entityContact) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                entityContact.Application = application;

                response.Success = entityContact.Save ();

                response.Id = entityContact.Id;

                if (!response.Success) {

                    response.SetException (application.LastException);

                }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion


        #region Entity Contact Information

        public Server.Core.Entity.EntityContactInformation EntityContactInformationGet (String token, Int64 entityContactInformationId) {

            Server.Core.Entity.EntityContactInformation entityContact = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                entityContact = application.EntityContactInformationGet (entityContactInformationId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return entityContact;

        }

        public Server.Core.Entity.EntityContactInformation EntityContactInformationGetByTypeDate (String token, Int64 entityId, Server.Core.Enumerations.EntityContactType contactType, DateTime forDate) {

            Server.Core.Entity.EntityContactInformation entityContact = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                entityContact = application.EntityContactInformationGetByEntityTypeDate (entityId, contactType, forDate);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return entityContact;

        }

        public EntityContactInformationCollectionResponse EntityContactInformationsGet (String token, Int64 entityId) {

            EntityContactInformationCollectionResponse response = new EntityContactInformationCollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.EntityContactInformationsGet (entityId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return response;

        }


        public ObjectSaveResponse EntityContactInformationSave (String token, Server.Core.Entity.EntityContactInformation entityContactInformation) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                entityContactInformation.Application = application;


                // ENSURE VALID ENTITY RECORD 

                if (entityContactInformation.Entity == null) { throw new ApplicationException ("Permission Denied. Unable to determine appropriate Entity for Contact Information."); }


                // ENSURE THAT USER HAS MANAGE RIGHTS IF EXISTING ADDRESS

                if ((entityContactInformation.Id != 0) && (!application.HasEnvironmentPermission ("Environment." + entityContactInformation.Entity.EntityType.ToString () + ".ContactInformation.Manage"))) {

                    throw new ApplicationException ("Permission Denied. Unable to modify existing Contact Information for " + entityContactInformation.Entity.EntityType.ToString () + ".");

                }


                // ENSURE THAT USER HAS ADD RIGHTS IF NEW NOTE

                if ((entityContactInformation.Id == 0) && (!application.HasEnvironmentPermission ("Environment." + entityContactInformation.Entity.EntityType.ToString () + ".ContactInformation.Manage"))) {

                    throw new ApplicationException ("Permission Denied. Unable to add new Contact Information for " + entityContactInformation.Entity.EntityType.ToString () + ".");

                }


                response.Success = entityContactInformation.Save ();

                response.Id = entityContactInformation.Id;

                if (!response.Success) {

                    response.SetException (application.LastException);

                }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public BooleanResponse EntityContactInformationTerminate (String token, Server.Core.Entity.EntityContactInformation entityContactInformation, DateTime terminationDate) {

            BooleanResponse response = new BooleanResponse ();

            Server.Application application = null;


            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                // ENSURE VALID ENTITY RECORD 

                if (entityContactInformation.Entity == null) { throw new ApplicationException ("Permission Denied. Unable to determine appropriate Entity for Contact Information."); }


                // ENSURE THAT USER HAS TERMINATE RIGHTS IF EXISTING ADDRESS

                if (((entityContactInformation.Id != 0) && (!application.HasEnvironmentPermission ("Environment." + entityContactInformation.Entity.EntityType.ToString () + ".ContactInformation.Terminate")))

                    && ((entityContactInformation.Id != 0) && (!application.HasEnvironmentPermission ("Environment." + entityContactInformation.Entity.EntityType.ToString () + ".ContactInformation.Modify")))) {

                    throw new ApplicationException ("Permission Denied. Unable to terminate existing Contact Information for " + entityContactInformation.Entity.EntityType.ToString () + ".");

                }

                entityContactInformation.Application = application;

                response.Result = entityContactInformation.Terminate (terminationDate);

                if (!response.Result) {

                    response.SetException (application.LastException);

                }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion 


        #region Entity Correspondence

        public Server.Core.Entity.EntityCorrespondence EntityCorrespondenceGet (String token, Int64 entityCorrespondenceId) {

            Server.Core.Entity.EntityCorrespondence entityCorrespondence = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                entityCorrespondence = application.EntityCorrespondenceGet (entityCorrespondenceId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return entityCorrespondence;

        }

        public ImageResponse EntityCorrespondenceImageGet (String token, Int64 entityCorrespondenceId, Boolean render) {

            System.IO.MemoryStream renderedContent = new System.IO.MemoryStream ();

            Server.Core.Entity.EntityCorrespondence entityCorrespondence = null;

            ImageResponse response = new ImageResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS NEEDED, OPEN TO ALL USERS

                response = new ImageResponse (application.EntityCorrespondenceImageGet (entityCorrespondenceId));

                if (String.IsNullOrWhiteSpace (response.ImageBase64)) { 

                    if (render) {

                        entityCorrespondence = application.EntityCorrespondenceGet (entityCorrespondenceId);

                        response = new ImageResponse (entityCorrespondence.Render ());

                    }

                }

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public ObjectSaveResponse EntityCorrespondenceSave (String token, Server.Core.Entity.EntityCorrespondence entityCorrespondence) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                entityCorrespondence.Application = application;

                response.Success = entityCorrespondence.Save ();

                response.Id = entityCorrespondence.Id;

                if (!response.Success) {

                    response.SetException (application.LastException);

                }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }


        public Int64 EntityDocumentsGetCount (String token, Int64 entityId) {

            Int64 itemCount = 0;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                itemCount = application.EntityDocumentsGetCount (entityId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return itemCount;

        }

        public EntityDocumentCollectionResponse EntityDocumentsGetByPage (String token, Int64 entityId, Int32 initialRow, Int32 count) {

            EntityDocumentCollectionResponse response = new EntityDocumentCollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.EntityDocumentsGetByPage (entityId, initialRow, count);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion 


        #region Entity Note

        public Server.Core.Entity.EntityNote EntityNoteGet (String token, Int64 entityNoteId) {

            Mercury.Server.Core.Entity.EntityNote entityNote = null;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                entityNote = application.EntityNoteGet (entityNoteId);

                if (entityNote.Entity == null) { throw new ApplicationException ("Permission Denied. Unable to determine appropriate Entity for Note."); }

                // ENSURE THAT USER HAS READ RIGHTS TO GET AN INDIVIDUAL NOTE

                if (!application.HasEnvironmentPermission ("Environment." + entityNote.Entity.EntityType.ToString () + ".Note.Read")) {

                    throw new ApplicationException ("Permission Denied.");

                }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return entityNote;

        }

        public ObjectSaveResponse EntityNoteSave (String token, Server.Core.Entity.EntityNote entityNote) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                entityNote.Application = application;


                // ENSURE VALID ENTITY RECORD 

                if (entityNote.Entity == null) { throw new ApplicationException ("Permission Denied. Unable to determine appropriate Entity for Note."); }


                // ENSURE THAT USER HAS MODIFY RIGHTS IF EXISTING NOTE

                if ((entityNote.Id != 0) && (!application.HasEnvironmentPermission ("Environment." + entityNote.Entity.EntityType.ToString () + ".Note.Modify"))) {

                    throw new ApplicationException ("Permission Denied. Unable to modify existing Note for " + entityNote.Entity.EntityType.ToString () + ".");

                }


                // ENSURE THAT USER HAS ADD RIGHTS IF NEW NOTE

                if ((entityNote.Id == 0) && (!application.HasEnvironmentPermission ("Environment." + entityNote.Entity.EntityType.ToString () + ".Note.Add"))) {

                    throw new ApplicationException ("Permission Denied. Unable to add new Note for " + entityNote.Entity.EntityType.ToString () + ".");

                }


                response.Success = entityNote.Save ();

                response.Id = entityNote.Id;

                if (!response.Success) {

                    response.SetException (application.LastException);

                }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public BooleanResponse EntityNoteTerminate (String token, Server.Core.Entity.EntityNote entityNote, DateTime terminationDate) {

            BooleanResponse response = new BooleanResponse ();

            Mercury.Server.Application application = null;


            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                // ENSURE VALID ENTITY RECORD 

                if (entityNote.Entity == null) { throw new ApplicationException ("Permission Denied. Unable to determine appropriate Entity for Note."); }


                // ENSURE THAT USER HAS TERMINATE RIGHTS IF EXISTING NOTE

                if (((entityNote.Id != 0) && (!application.HasEnvironmentPermission ("Environment." + entityNote.Entity.EntityType.ToString () + ".Note.Terminate")))

                    && ((entityNote.Id != 0) && (!application.HasEnvironmentPermission ("Environment." + entityNote.Entity.EntityType.ToString () + ".Note.Modify")))) {

                    throw new ApplicationException ("Permission Denied. Unable to terminate existing Note for " + entityNote.Entity.EntityType.ToString () + ".");

                }

                entityNote.Application = application;

                response.Result = entityNote.Terminate (terminationDate);

                if (!response.Result) {

                    response.SetException (application.LastException);

                }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }


        public EntityNoteContentCollectionResponse EntityNoteContentsGet (String token, Int64 entityNoteId) {

            EntityNoteContentCollectionResponse response = new EntityNoteContentCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.EntityNoteContentsGet (entityNoteId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return response;

        }

        public ObjectSaveResponse EntityNoteContentAppend (String token, Server.Core.Entity.EntityNote entityNote, String content) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Mercury.Server.Application application = null;

            Server.Core.Entity.EntityNoteContent noteContent = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                noteContent = new Mercury.Server.Core.Entity.EntityNoteContent (application);

                noteContent.EntityNoteId = entityNote.Id;

                noteContent.Content = content;


                // ENSURE VALID ENTITY RECORD 

                if (entityNote.Entity == null) { throw new ApplicationException ("Permission Denied. Unable to determine appropriate Entity for Note."); }


                // ENSURE THAT USER HAS MODIFY RIGHTS IF EXISTING NOTE

                if ((entityNote.Id != 0) && (!application.HasEnvironmentPermission ("Environment." + entityNote.Entity.EntityType.ToString () + ".Note.Append"))) {

                    throw new ApplicationException ("Permission Denied. Unable to append to existing Note for " + entityNote.Entity.EntityType.ToString () + ".");

                }


                response.Success = noteContent.Save ();

                response.Id = entityNote.Id;

                if (!response.Success) {

                    response.SetException (application.LastException);

                }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }


        public Int64 EntityNotesGetCount (String token, Int64 entityId) {

            Int64 itemCount = 0;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                itemCount = application.EntityNotesGetCount (entityId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return itemCount;

        }

        public EntityNoteCollectionResponse EntityNotesGetByPage (String token, Int64 entityId, Int32 initialRow, Int32 count) {

            EntityNoteCollectionResponse response = new EntityNoteCollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.EntityNotesGetByPage (entityId, initialRow, count);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Entity.EntityNote EntityNoteGetMostRecentByImportance (String token, Int64 entityId, Server.Core.Enumerations.NoteImportance importance) {

            Mercury.Server.Core.Entity.EntityNote entityNote = null;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                entityNote = application.EntityNoteGetMostRecentByImportance (entityId, importance);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return entityNote;

        }

        public EntityNoteCollectionResponse EntityNoteGetMostRecentByAllImportances (String token, Int64 entityId) {

            EntityNoteCollectionResponse response = new EntityNoteCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                Server.Core.Entity.EntityNote entityNote;
                
                
                entityNote = application.EntityNoteGetMostRecentByImportance (entityId, Server.Core.Enumerations.NoteImportance.Informational);

                if (entityNote != null) { response.Collection.Add (entityNote); }

                entityNote = application.EntityNoteGetMostRecentByImportance (entityId, Server.Core.Enumerations.NoteImportance.Warning);

                if (entityNote != null) { response.Collection.Add (entityNote); }

                entityNote = application.EntityNoteGetMostRecentByImportance (entityId, Server.Core.Enumerations.NoteImportance.Critical);

                if (entityNote != null) { response.Collection.Add (entityNote); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return response;

        }

        public Server.Core.Entity.EntityNote EntityNoteGetMostRecentByType (String token, Int64 entityId, Int64 noteTypeId) {

            Mercury.Server.Core.Entity.EntityNote entityNote = null;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                entityNote = application.EntityNoteGetMostRecentByType (entityId, noteTypeId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return entityNote;

        }

        #endregion 

        #endregion


        #region Core - Forms

        #region Form

        public SearchResultFormHeaderCollectionResponse FormsAvailable (String token) {

            SearchResultFormHeaderCollectionResponse response = new SearchResultFormHeaderCollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.FormsAvailable ();

                if (application.LastException != null) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Forms.Form FormGet (String token, Int64 formId) {

            Server.Core.Forms.Form form = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                form = application.FormGet (formId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return form;

        }

        public Server.Core.Forms.Form FormGetByName (String token, String formName) {

            Server.Core.Forms.Form form = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                form = application.FormGet (formName);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return form;

        }

        public Server.Core.Forms.Form FormGetByEntityFormId (String token, Int64 entityFormId) {

            Server.Core.Forms.Form form = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                form = new Server.Core.Forms.Form (application, entityFormId, true);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return form;

        }


        public FormCompileMessageCollectionResponse FormCompile (String token, Server.Core.Forms.Form form) {

            FormCompileMessageCollectionResponse response = new FormCompileMessageCollectionResponse ();

            List<Server.Core.Forms.CompileMessage> compileMessages = new List<Server.Core.Forms.CompileMessage> ();


            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                form.ResetForm (application);
                
                response.Collection = form.Compile ();

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }
        
        public FormSubmitResponse FormSubmit (String token, Server.Core.Forms.Form form) {

            FormSubmitResponse response = new FormSubmitResponse ();


            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                form.ResetForm (application);

                response.Collection = form.Submit ();

                response.Form = form;

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public ObjectSaveResponse FormSave (String token, Server.Core.Forms.Form form) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            FormCompileMessageCollectionResponse compileResponse = new FormCompileMessageCollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                form.ResetForm (application);

                compileResponse = FormCompile (token, form);

                if (!compileResponse.HasException) {

                    response.Success = form.Save ();

                    response.Id = form.Id;

                    if (!response.Success) {

                        response.SetException (form.LastException);

                    }

                }

                else {

                    response.SetException (compileResponse.Exception);

                }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);


            }


            return response;

        }

        #endregion


        #region Form Control - Data Binding Extensions

        public Dictionary<String, String> FormControl_DataBindableProperties (String token, Server.Core.Forms.Form form, Guid controlId) {

            Dictionary<String, String> bindableProperties = new Dictionary<String, String> ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                form.ResetForm (application);

                Server.Core.Forms.Control control = form.FindControlById (controlId);

                if (control != null) {

                    bindableProperties = control.DataBindableProperties;

                }

            }

            catch (Exception applicationException) {

                System.Diagnostics.Debug.WriteLine (applicationException.Message);

            }

            return bindableProperties;

        }

        public Dictionary<String, String> FormControl_DataBindingContexts (String token, Server.Core.Forms.Form form, Guid controlId) {

            Dictionary<String, String> bindingContexts = new Dictionary<String, String> ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                form.ResetForm (application);

                Server.Core.Forms.Control control = form.FindControlById (controlId);

                if (control != null) {

                    bindingContexts = control.DataBindingContexts;

                }

            }

            catch (Exception applicationException) {

                System.Diagnostics.Debug.WriteLine (applicationException.Message);

            }

            return bindingContexts;

        }

        public BooleanResponse FormControl_DataBindingAllowed (String token, Server.Core.Forms.Form form, Guid controlId, String bindableProperty, String forDataType) {

            BooleanResponse response = new BooleanResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                form.ResetForm (application);

                Server.Core.Forms.Control control = form.FindControlById (controlId);

                if (control != null) {

                    response.Result = control.DataBindingAllowed (bindableProperty, forDataType);

                }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public String FormControl_EvaluateDataBinding (String token, Server.Core.Forms.Form form, Guid controlId, Server.Core.Forms.Structures.DataBinding dataBinding) {

            String dataValue = String.Empty;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                form.ResetForm (application);

                Server.Core.Forms.Control control = form.FindControlById (dataBinding.DataSourceControlId);

                if (control != null) {

                    dataValue = control.EvaluateDataBinding (dataBinding);

                }

            }

            catch (Exception applicationException) {

                System.Diagnostics.Debug.WriteLine (applicationException.Message);

            }

            return dataValue;

        }

        public Server.Core.Forms.Form Form_OnDataSourceChanged (String token, Server.Core.Forms.Form form, Guid controlId) {

            Server.Core.Forms.Form responseForm = form;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                form.ResetForm (application);

                responseForm.ResetForm (application);

                Server.Core.Forms.Control dataSourceControl = responseForm.FindControlById (controlId);

                if (dataSourceControl != null) {

                    responseForm.OnDataSourceChanged (dataSourceControl, true);

                }

            }

            catch (Exception applicationException) {

                if (application != null) { application.SetLastException (applicationException); }

                System.Diagnostics.Debug.WriteLine (applicationException.Message);

            }

            return responseForm;

        }

        #endregion


        #region Form Control - Event Handlers

        public List<String> FormControl_Events (String token, Server.Core.Forms.Form form, Guid controlId) {

            List<String> events = new List<String> ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                form.ResetForm (application);

                Server.Core.Forms.Control control = form.FindControlById (controlId);

                if (control != null) {

                    events = control.Events;

                }

            }

            catch (Exception applicationException) {

                if (application != null) { application.SetLastException (applicationException); }

                System.Diagnostics.Debug.WriteLine (applicationException.Message);

            }

            return events;

        }

        public FormCompileMessageCollectionResponse FormControl_EventHandler_Compile (String token, Server.Core.Forms.Form form, Guid controlId, String eventName) {

            FormCompileMessageCollectionResponse response = new FormCompileMessageCollectionResponse ();


            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                form.ResetForm (application);

                Server.Core.Forms.Control control = form.FindControlById (controlId);

                if (control != null) {

                    response.Collection = control.EventHandler_Compile (eventName);

                }

            }

            catch (Exception applicationException) {

                if (application != null) { application.SetLastException (applicationException); }

                System.Diagnostics.Debug.WriteLine (applicationException.Message);

            }

            return response;

        }


        public FormControlRaiseEventResponse FormControl_RaiseEvent (String token, Server.Core.Forms.Form form, Guid controlId, String eventName) {

            FormControlRaiseEventResponse response = new FormControlRaiseEventResponse ();

            response.Form = form;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                response.Form.ResetForm (application);

                Server.Core.Forms.Control eventControl = response.Form.FindControlById (controlId);

                if (eventControl != null) {

                    eventControl.RaiseEvent (eventName);

                }

            }

            catch (Exception applicationException) {

                if (application != null) { application.SetLastException (applicationException); }

                System.Diagnostics.Debug.WriteLine (applicationException.Message);

            }

            return response;

        }

        public FormControlValueChangedResponse FormControl_ValueChanged (String token, Server.Core.Forms.Form form, Guid controlId) {

            FormControlValueChangedResponse response = new FormControlValueChangedResponse ();

            response.Form = form;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                response.Form.ResetForm (application);

                Server.Core.Forms.Control changedControl = response.Form.FindControlById (controlId);

                if (changedControl != null) {

                    changedControl.ValueChanged ();

                }

            }

            catch (Exception applicationException) {

                if (application != null) { application.SetLastException (applicationException); }

                System.Diagnostics.Debug.WriteLine (applicationException.Message);

            }

            return response;

        }

        #endregion


        #region Form Control Specific Methods

        public Dictionary<String, String> FormControlSelection_ReferenceGetPage (String token, Server.Core.Forms.Form form, Guid controlId, String text, Int32 initialRow, Int32 pageSize) {

            Dictionary<String, String> referencePage = new Dictionary<String, String> ();

            System.Data.DataTable pageTable;


            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                form.ResetForm (application);

                Server.Core.Forms.Control control = form.FindControlById (controlId);

                if (control != null) {

                    if (control is Server.Core.Forms.Controls.Selection) {

                        pageTable = ((Server.Core.Forms.Controls.Selection)control).ReferenceGetPage (text, initialRow, pageSize);

                        foreach (System.Data.DataRow currentRow in pageTable.Rows) {

                            referencePage.Add (currentRow["Value"].ToString (), currentRow["Text"].ToString ());

                        }

                    }

                }

            }

            catch (Exception applicationException) {

                System.Diagnostics.Debug.WriteLine (applicationException.Message);

            }



            return referencePage;

        }

        #endregion 

        #endregion


        #region Core - Individual

        #region Care Level

        public CareLevelCollectionResponse CareLevelsAvailable (String token) {

            CareLevelCollectionResponse response = new CareLevelCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.CareLevelsAvailable ();

                if (application.LastException != null) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Individual.CareLevel CareLevelGet (String token, Int64 careLevelId) {

            Mercury.Server.Core.Individual.CareLevel careLevel = null;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                careLevel = new Mercury.Server.Core.Individual.CareLevel (application, careLevelId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return careLevel;

        }

        public ObjectSaveResponse CareLevelSave (String token, Mercury.Server.Core.Individual.CareLevel careLevel) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                careLevel.Application = application;

                response.Success = careLevel.Save (application);

                response.Id = careLevel.Id;

                if (!response.Success) {

                    response.SetException (application.LastException);

                }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion


        #region Care Measure Scale

        public CareMeasureScaleCollectionResponse CareMeasureScalesAvailable (String token) {

            CareMeasureScaleCollectionResponse response = new CareMeasureScaleCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.CareMeasureScalesAvailable ();

                if (application.LastException != null) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Individual.CareMeasureScale CareMeasureScaleGet (String token, Int64 careMeasureScaleId) {

            Mercury.Server.Core.Individual.CareMeasureScale careMeasureScale = null;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                careMeasureScale = new Mercury.Server.Core.Individual.CareMeasureScale (application, careMeasureScaleId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return careMeasureScale;

        }

        public ObjectSaveResponse CareMeasureScaleSave (String token, Mercury.Server.Core.Individual.CareMeasureScale careMeasureScale) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                careMeasureScale.Application = application;

                response.Success = careMeasureScale.Save (application);

                response.Id = careMeasureScale.Id;

                if (!response.Success) {

                    response.SetException (application.LastException);

                }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion


        #region Care Measure 

        public CareMeasureDomainCollectionResponse CareMeasureDomainsAvailable (String token) {

            CareMeasureDomainCollectionResponse response = new CareMeasureDomainCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.CareMeasureDomainsAvailable ();

                if (application.LastException != null) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public CareMeasureClassCollectionResponse CareMeasureClassesAvailable (String token) {

            CareMeasureClassCollectionResponse response = new CareMeasureClassCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.CareMeasureClassesAvailable ();

                if (application.LastException != null) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public CareMeasureCollectionResponse CareMeasuresAvailable (String token) {

            CareMeasureCollectionResponse response = new CareMeasureCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.CareMeasuresAvailable ();

                if (application.LastException != null) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Individual.CareMeasure CareMeasureGet (String token, Int64 careMeasureId) {

            Mercury.Server.Core.Individual.CareMeasure careMeasure = null;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                careMeasure = new Mercury.Server.Core.Individual.CareMeasure (application, careMeasureId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return careMeasure;

        }

        public ObjectSaveResponse CareMeasureSave (String token, Mercury.Server.Core.Individual.CareMeasure careMeasure) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                careMeasure.Application = application;

                response.Success = careMeasure.Save (application);

                response.Id = careMeasure.Id;

                if (!response.Success) {

                    response.SetException (application.LastException);

                }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion


        #region Care Interventions

        public CareInterventionCollectionResponse CareInterventionsAvailable (String token) {

            CareInterventionCollectionResponse response = new CareInterventionCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.CareInterventionsAvailable ();

                if (application.LastException != null) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Individual.CareIntervention CareInterventionGet (String token, Int64 careInterventionId) {

            Mercury.Server.Core.Individual.CareIntervention careIntervention = null;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                careIntervention = new Mercury.Server.Core.Individual.CareIntervention (application, careInterventionId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return careIntervention;

        }

        public ObjectSaveResponse CareInterventionSave (String token, Mercury.Server.Core.Individual.CareIntervention careIntervention) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                careIntervention.Application = application;

                response.Success = careIntervention.Save (application);

                response.Id = careIntervention.Id;

                if (!response.Success) {

                    response.SetException (application.LastException);

                }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion


        #region Care Plan

        public CarePlanCollectionResponse CarePlansAvailable (String token) {

            CarePlanCollectionResponse response = new CarePlanCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.CarePlansAvailable ();

                if (application.LastException != null) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Individual.CarePlan CarePlanGet (String token, Int64 carePlanId) {

            Mercury.Server.Core.Individual.CarePlan carePlan = null;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                carePlan = new Mercury.Server.Core.Individual.CarePlan (application, carePlanId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return carePlan;

        }

        public ObjectSaveResponse CarePlanSave (String token, Mercury.Server.Core.Individual.CarePlan carePlan) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                carePlan.Application = application;

                response.Success = carePlan.Save (application);

                response.Id = carePlan.Id;

                if (!response.Success) {

                    response.SetException (application.LastException);

                }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion


        #region Problem Statement

        public ProblemDomainCollectionResponse ProblemDomainsAvailable (String token) {

            ProblemDomainCollectionResponse response = new ProblemDomainCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.ProblemDomainsAvailable ();

                if (application.LastException != null) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public ProblemClassCollectionResponse ProblemClassesAvailable (String token) {

            ProblemClassCollectionResponse response = new ProblemClassCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.ProblemClassesAvailable ();

                if (application.LastException != null) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public ProblemStatementCollectionResponse ProblemStatementsAvailable (String token) {

            ProblemStatementCollectionResponse response = new ProblemStatementCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.ProblemStatementsAvailable ();

                if (application.LastException != null) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Individual.ProblemStatement ProblemStatementGet (String token, Int64 problemStatementId) {

            Mercury.Server.Core.Individual.ProblemStatement problemStatement = null;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                problemStatement = new Mercury.Server.Core.Individual.ProblemStatement (application, problemStatementId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return problemStatement;

        }

        public ObjectSaveResponse ProblemStatementSave (String token, Mercury.Server.Core.Individual.ProblemStatement problemStatement) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                problemStatement.Application = application;

                response.Success = problemStatement.Save (application);

                response.Id = problemStatement.Id;

                if (!response.Success) {

                    response.SetException (application.LastException);

                }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public MemberProblemStatementIdentifiedCollectionResponse MemberProblemStatementIdentifiedAvailable (String token, Int64 memberId, Boolean includeCompleted) {

            MemberProblemStatementIdentifiedCollectionResponse response = new MemberProblemStatementIdentifiedCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.MemberProblemStatementIdentifiedAvailable (memberId, includeCompleted);

                if (application.LastException != null) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion


        #region Care - Care Outcome

        public CareOutcomeCollectionResponse CareOutcomesAvailable (String token) {

            CareOutcomeCollectionResponse response = new CareOutcomeCollectionResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Collection = application.CareOutcomesAvailable ();

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public DictionaryResponse CareOutcomeDictionary (String token) {

            DictionaryResponse response = new DictionaryResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Dictionary = application.CoreObjectDictionary ("CareOutcome");

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Individual.CareOutcome CareOutcomeGet (String token, Int64 careOutcomeId) {

            Server.Core.Individual.CareOutcome careOutcome = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS NEEDED, OPEN TO ALL USERS

                careOutcome = application.CareOutcomeGet (careOutcomeId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return careOutcome;

        }

        public Server.Core.Individual.CareOutcome CareOutcomeGetByName (String token, String careOutcomeName) {

            Server.Core.Individual.CareOutcome careOutcome = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS NEEDED, OPEN TO ALL USERS

                careOutcome = application.CareOutcomeGet (careOutcomeName);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return careOutcome;

        }

        public ObjectSaveResponse CareOutcomeSave (String token, Server.Core.Individual.CareOutcome careOutcome) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                // VALIDATE PERMISSIONS

                Boolean hasPermission = application.HasEnvironmentPermission (Server.EnvironmentPermissions.CareOutcomeManage);

                if (hasPermission) {

                    response.Success = careOutcome.Save (application);

                    response.Id = careOutcome.Id;

                    if (!response.Success) { response.SetException (application.LastException); }

                }

                else { response.SetException (new ApplicationException ("Permission Denied.")); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion


        #region Case - Member Case

        public MemberCaseCreateResponse MemberCaseCreate (String token, Int64 memberId, Boolean ignoreExisting) {

            MemberCaseCreateResponse response = new MemberCaseCreateResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.MemberCase = application.MemberCaseCreate (memberId, ignoreExisting, true);

                if (application.LastException != null) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return response;

        }

        public Server.Core.Individual.Case.MemberCase MemberCaseGet (String token, Int64 memberCaseId) {

            Mercury.Server.Core.Individual.Case.MemberCase memberCase = null;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                memberCase = new Mercury.Server.Core.Individual.Case.MemberCase (application, memberCaseId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return memberCase;

        }


        /* TODO: DAVID: ADDED ON 9/13/2011 (START) */

        public MemberCaseAuditCollectionResponse MemberCaseAuditHistoryGetByMemberCaseIdPage (String token, Int64 memberCaseId, Int64 initialRow, Int64 count) {

            MemberCaseAuditCollectionResponse response = new MemberCaseAuditCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.MemberCaseAuditHistoryGetByMemberCaseIdPage (memberCaseId, initialRow, count);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return response;

        }

        public Int64 MemberCaseAuditHistoryGetCount (String token, Int64 memberCaseId) {

            Int64 itemCount = 0;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                itemCount = application.MemberCaseAuditHistoryGetCount (memberCaseId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                itemCount = -1;

            }

            return itemCount;

        }

        /* TODO: DAVID: ADDED ON 9/13/2011 ( END ) */


        /* ADDED ON 10/25/2011 (START) */

        public MemberCaseModificationResponse MemberCaseProblemClass_AssignToUser (String token, Server.Core.Individual.Case.MemberCase memberCase, Int64 memberCaseProblemClassId, Int64 assignToSecurityAuthorityId, String assignToUserAccountId, String assignToUserAccountName, String assignToUserDisplayName) {

            // GET REFERENCE TO MEMBER CASE MODIFICATION RESPONSE

            MemberCaseModificationResponse response = new MemberCaseModificationResponse ();

            // DEFAULT REFERENCE TO APPLICATION AS NULL

            Mercury.Server.Application application = null;

            try {

                // GET AND SET REFERENCE TO APPLICATION FROM CACHE MANAGER

                application = cacheManager.GetApplication (token);

                // IF APPLICATION IS NULL, THROW EXCEPTION

                if (application == null) { throw cacheManager.LastException; }

                // RESET APPLICATION REFERENCE OF MEMBER CASE

                memberCase.Application = application;

                // TRY TO ASSIGN MEMBER CASE PROBLEM CLASS TO USER, SET MODIFICATION OUTCOME OF RESPONSE TO ASSIGNMENT OUTCOME

                response.ModificationOutcome = memberCase.AssignMemberCaseProblemClassToUser (memberCaseProblemClassId, assignToSecurityAuthorityId, assignToUserAccountId, assignToUserAccountName, assignToUserDisplayName);

                // IF MODIFICATION OUTCOME OF RESPONSE IS NOT SUCCESS, THEN SET EXCEPTION

                if (response.ModificationOutcome != Server.Core.Individual.Enumerations.MemberCaseActionOutcome.Success) { response.SetException (application.LastException); }


                // RELOAD MEMBER CASE (TO SEND UPDATED CASE IF SUCCESSFULL OR UNSUCCESSFUL)

                response.MemberCase = application.MemberCaseGet (memberCase.Id);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            // RETURN RESPONSE

            return response;

        }

        public MemberCaseModificationResponse MemberCaseProblemClass_AssignToProvider (String token, Server.Core.Individual.Case.MemberCase memberCase, Int64 memberCaseProblemClassId, Int64 assignToProviderId) {

            // GET REFERENCE TO MEMBER CASE MODIFICATION RESPONSE

            MemberCaseModificationResponse response = new MemberCaseModificationResponse ();

            // DEFAULT APPLICATION REFERENCE TO NULL

            Mercury.Server.Application application = null;

            try {

                // SET AND GET APPLICATION FROM CACHE MANAGER

                application = cacheManager.GetApplication (token);

                // IF APPLICATION IS NULL, THEN THROW EXCEPTION

                if (application == null) { throw cacheManager.LastException; }

                // RESET APPLICATION OF MEMBER CASE

                memberCase.Application = application;

                // TRY TO ASSIGN MEMBER CASE PROBLEM CLASS TO PROVIDER

                response.ModificationOutcome = memberCase.AssignMemberCaseProblemClassToProvider (memberCaseProblemClassId, assignToProviderId);

                // IF MODIFICATION OUTCOME OF RESPONSE IS NOT SUCCESS, THEN SET EXCEPTION

                if (response.ModificationOutcome != Server.Core.Individual.Enumerations.MemberCaseActionOutcome.Success) { response.SetException (application.LastException); }


                // RELOAD MEMBER CASE (TO SEND UPDATED CASE IF SUCCESSFULL OR UNSUCCESSFUL)

                response.MemberCase = application.MemberCaseGet (memberCase.Id);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            // RETURN RESPONSE

            return response;

        }

        /* ADDED ON 10/25/2011 ( END ) */


        /* ADDED ON 11/10/11 (START) */

        public MemberCaseCarePlanSummaryCollectionResponse MemberCaseCarePlanSummaryGetByMemberCase (String token, Int64 memberCaseId, Boolean useCaching) {

            MemberCaseCarePlanSummaryCollectionResponse response = new MemberCaseCarePlanSummaryCollectionResponse ();

            Mercury.Server.Application application = null;
            
            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.MemberCaseCarePlanSummaryGetByMemberCase (memberCaseId);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        /* ADDED ON 11/10/11 ( END ) */


        public MemberCaseSummaryCollectionResponse MemberCaseSummaryGetByMemberPage (String token, Int64 memberId, Int32 initialRow, Int32 count, Boolean showClosed) {

            MemberCaseSummaryCollectionResponse response = new MemberCaseSummaryCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.MemberCaseSummaryGetByMemberPage (memberId, initialRow, count, showClosed);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public MemberCaseSummaryCollectionResponse MemberCaseSummaryGetByUserWorkTeamsPage (String token, Int64 securityAuthorityId, String userAccountId, Int32 initialRow, Int32 count, Boolean showClosed) {

            MemberCaseSummaryCollectionResponse response = new MemberCaseSummaryCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.MemberCaseSummaryGetByUserWorkTeamsPage (securityAuthorityId, userAccountId, initialRow, count, showClosed);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public MemberCaseSummaryCollectionResponse MemberCaseSummaryGetByAssignedToUserPage (String token, Int64 securityAuthorityId, String userAccountId, Int32 initialRow, Int32 count, Boolean showClosed) {

            MemberCaseSummaryCollectionResponse response = new MemberCaseSummaryCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.MemberCaseSummaryGetByAssignedToUserPage (securityAuthorityId, userAccountId, initialRow, count, showClosed);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }


        public MemberCaseLoadSummaryCollectionResponse MemberCaseLoadSummaryGetByUser (String token, Int64 securityAuthorityId, String userAccountId, Boolean showClosed) {

            MemberCaseLoadSummaryCollectionResponse response = new MemberCaseLoadSummaryCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.MemberCaseLoadSummaryGetByUser (securityAuthorityId, userAccountId, showClosed);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public MemberCaseLoadSummaryCollectionResponse MemberCaseLoadSummaryGetByWorkTeam (String token, Int64 workTeamId, Boolean showClosed) {

            MemberCaseLoadSummaryCollectionResponse response = new MemberCaseLoadSummaryCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.MemberCaseLoadSummaryGetByWorkTeam (workTeamId, showClosed);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }


        public MemberCaseModificationResponse MemberCase_SetDescription (String token, Server.Core.Individual.Case.MemberCase memberCase, String description) {

            MemberCaseModificationResponse response = new MemberCaseModificationResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                memberCase.Application = application; // RESET APPLICATION REFERENCE

                response.ModificationOutcome = memberCase.SetDescription (description); // SET PROPERTY VALUE AND SAVE CHANGES

                if (response.ModificationOutcome != Server.Core.Individual.Enumerations.MemberCaseActionOutcome.Success) { response.SetException (application.LastException); }


                // RELOAD MEMBER CASE (TO SEND UPDATED CASE IF SUCCESSFULL OR UNSUCCESSFUL)

                response.MemberCase = application.MemberCaseGet (memberCase.Id);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public MemberCaseModificationResponse MemberCase_SetReferenceNumber (String token, Server.Core.Individual.Case.MemberCase memberCase, String referenceNumber) {

            MemberCaseModificationResponse response = new MemberCaseModificationResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                memberCase.Application = application; // RESET APPLICATION REFERENCE

                response.ModificationOutcome = memberCase.SetReferenceNumber (referenceNumber); // SET PROPERTY VALUE AND SAVE CHANGES

                if (response.ModificationOutcome != Server.Core.Individual.Enumerations.MemberCaseActionOutcome.Success) { response.SetException (application.LastException); }


                // RELOAD MEMBER CASE (TO SEND UPDATED CASE IF SUCCESSFULL OR UNSUCCESSFUL)

                response.MemberCase = application.MemberCaseGet (memberCase.Id);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public MemberCaseModificationResponse MemberCase_Lock (String token, Server.Core.Individual.Case.MemberCase memberCase) {

            MemberCaseModificationResponse response = new MemberCaseModificationResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                memberCase.Application = application; // RESET APPLICATION REFERENCE

                response.ModificationOutcome = memberCase.Lock (); // ATTEMPT LOCK

                if (response.ModificationOutcome != Server.Core.Individual.Enumerations.MemberCaseActionOutcome.Success) { response.SetException (application.LastException); }


                // RELOAD MEMBER CASE (TO SEND UPDATED CASE IF SUCCESSFULL OR UNSUCCESSFUL)

                response.MemberCase = application.MemberCaseGet (memberCase.Id);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public MemberCaseModificationResponse MemberCase_Unlock (String token, Server.Core.Individual.Case.MemberCase memberCase) {

            MemberCaseModificationResponse response = new MemberCaseModificationResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                memberCase.Application = application; // RESET APPLICATION REFERENCE

                response.ModificationOutcome = memberCase.Unlock (); // ATTEMPT LOCK

                if (response.ModificationOutcome != Server.Core.Individual.Enumerations.MemberCaseActionOutcome.Success) { response.SetException (application.LastException); }


                // RELOAD MEMBER CASE (TO SEND UPDATED CASE IF SUCCESSFULL OR UNSUCCESSFUL)

                response.MemberCase = application.MemberCaseGet (memberCase.Id);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public MemberCaseModificationResponse MemberCase_AssignToWorkTeam (String token, Server.Core.Individual.Case.MemberCase memberCase, Int64 workTeamId) {

            MemberCaseModificationResponse response = new MemberCaseModificationResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                memberCase.Application = application; // RESET APPLICATION REFERENCE

                response.ModificationOutcome = memberCase.AssignToWorkTeam (workTeamId);

                if (response.ModificationOutcome != Server.Core.Individual.Enumerations.MemberCaseActionOutcome.Success) { response.SetException (application.LastException); }


                // RELOAD MEMBER CASE (TO SEND UPDATED CASE IF SUCCESSFULL OR UNSUCCESSFUL)

                response.MemberCase = application.MemberCaseGet (memberCase.Id);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public MemberCaseModificationResponse MemberCase_AssignToUser (String token, Server.Core.Individual.Case.MemberCase memberCase, Int64 securityAuthorityId, String userAccountId, String userAccountName, String userDisplayName) {

            MemberCaseModificationResponse response = new MemberCaseModificationResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                memberCase.Application = application; // RESET APPLICATION REFERENCE

                response.ModificationOutcome = memberCase.AssignToUser (securityAuthorityId, userAccountId, userAccountName, userDisplayName);

                if (response.ModificationOutcome != Server.Core.Individual.Enumerations.MemberCaseActionOutcome.Success) { response.SetException (application.LastException); }


                // RELOAD MEMBER CASE (TO SEND UPDATED CASE IF SUCCESSFULL OR UNSUCCESSFUL)

                response.MemberCase = application.MemberCaseGet (memberCase.Id);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }


        public MemberCaseModificationResponse MemberCase_AddProblemStatement (String token, Server.Core.Individual.Case.MemberCase memberCase, Int64 problemStatementId, Boolean isSingleInstance) {

            MemberCaseModificationResponse response = new MemberCaseModificationResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                memberCase.Application = application; // RESET APPLICATION REFERENCE

                response.ModificationOutcome = memberCase.AddProblemStatement (problemStatementId, isSingleInstance);

                if (response.ModificationOutcome != Server.Core.Individual.Enumerations.MemberCaseActionOutcome.Success) { response.SetException (application.LastException); }


                // RELOAD MEMBER CASE (TO SEND UPDATED CASE IF SUCCESSFULL OR UNSUCCESSFUL)

                response.MemberCase = application.MemberCaseGet (memberCase.Id);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public MemberCaseModificationResponse MemberCase_DeleteProblemStatement (String token, Server.Core.Individual.Case.MemberCase memberCase, Int64 memberCaseProblemCarePlanId) {

            MemberCaseModificationResponse response = new MemberCaseModificationResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                memberCase.Application = application; // RESET APPLICATION REFERENCE

                response.ModificationOutcome = memberCase.DeleteProblemStatement (memberCaseProblemCarePlanId);

                if (response.ModificationOutcome != Server.Core.Individual.Enumerations.MemberCaseActionOutcome.Success) { response.SetException (application.LastException); }


                // RELOAD MEMBER CASE (TO SEND UPDATED CASE IF SUCCESSFULL OR UNSUCCESSFUL)

                response.MemberCase = application.MemberCaseGet (memberCase.Id);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }


        public MemberCaseModificationResponse MemberCaseCarePlanGoal_Delete (String token, Server.Core.Individual.Case.MemberCase memberCase, Int64 memberCaseCarePlanGoalId) {

            MemberCaseModificationResponse response = new MemberCaseModificationResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                memberCase.Application = application; // RESET APPLICATION REFERENCE

                response.ModificationOutcome = memberCase.FindMemberCaseCarePlanByGoalId (memberCaseCarePlanGoalId).DeleteGoal (memberCaseCarePlanGoalId);

                if (response.ModificationOutcome != Server.Core.Individual.Enumerations.MemberCaseActionOutcome.Success) { response.SetException (application.LastException); }


                // RELOAD MEMBER CASE (TO SEND UPDATED CASE IF SUCCESSFULL OR UNSUCCESSFUL)

                response.MemberCase = application.MemberCaseGet (memberCase.Id);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public MemberCaseModificationResponse MemberCaseCarePlanGoal_Add (String token, Server.Core.Individual.Case.MemberCase memberCase, Int64 memberCaseCarePlanId, Int64 copyCarePlanGoalId, String carePlanGoalName, Int64 careMeasureId) {

            MemberCaseModificationResponse response = new MemberCaseModificationResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                memberCase.Application = application; // RESET APPLICATION REFERENCE

                response.ModificationOutcome = memberCase.FindMemberCaseCarePlan (memberCaseCarePlanId).AddGoal (copyCarePlanGoalId, carePlanGoalName, careMeasureId);

                if (response.ModificationOutcome != Server.Core.Individual.Enumerations.MemberCaseActionOutcome.Success) { response.SetException (application.LastException); }


                // RELOAD MEMBER CASE (TO SEND UPDATED CASE IF SUCCESSFULL OR UNSUCCESSFUL)

                response.MemberCase = application.MemberCaseGet (memberCase.Id);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public MemberCaseModificationResponse MemberCaseCarePlanGoal_Update (String token, Server.Core.Individual.Case.MemberCase memberCase, Int64 memberCaseCarePlanGoalId) {

            MemberCaseModificationResponse response = new MemberCaseModificationResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                memberCase.Application = application; // RESET APPLICATION REFERENCE

                response.ModificationOutcome = memberCase.FindMemberCaseCarePlanGoal (memberCaseCarePlanGoalId).SaveUpdate ();

                if (response.ModificationOutcome != Server.Core.Individual.Enumerations.MemberCaseActionOutcome.Success) { response.SetException (application.LastException); }


                // RELOAD MEMBER CASE (TO SEND UPDATED CASE IF SUCCESSFULL OR UNSUCCESSFUL)

                response.MemberCase = application.MemberCaseGet (memberCase.Id);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public MemberCaseModificationResponse MemberCaseCarePlanGoal_AddCareIntervention (String token, Server.Core.Individual.Case.MemberCase memberCase, Int64 memberCaseCarePlanGoalId, Int64 careInterventionId, Boolean isSingleInstance) {

            MemberCaseModificationResponse response = new MemberCaseModificationResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                memberCase.Application = application; // RESET APPLICATION REFERENCE

                response.ModificationOutcome = memberCase.FindMemberCaseCarePlanGoal (memberCaseCarePlanGoalId).AddCareIntervention (careInterventionId, isSingleInstance);

                if (response.ModificationOutcome != Server.Core.Individual.Enumerations.MemberCaseActionOutcome.Success) { response.SetException (application.LastException); }


                // RELOAD MEMBER CASE (TO SEND UPDATED CASE IF SUCCESSFULL OR UNSUCCESSFUL)

                response.MemberCase = application.MemberCaseGet (memberCase.Id);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }


        public MemberCaseModificationResponse MemberCaseCarePlanIntervention_Delete (String token, Server.Core.Individual.Case.MemberCase memberCase, Int64 memberCaseCarePlanInterventionId) {

            MemberCaseModificationResponse response = new MemberCaseModificationResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                memberCase.Application = application; // RESET APPLICATION REFERENCE

                //response.ModificationOutcome = memberCase.FindMemberCaseCarePlanByInterventionId (memberCaseCarePlanInterventionId).DeleteIntervention (memberCaseCarePlanInterventionId);

                //if (response.ModificationOutcome != Server.Core.Individual.Enumerations.MemberCaseActionOutcome.Success) { response.SetException (application.LastException); }


                // RELOAD MEMBER CASE (TO SEND UPDATED CASE IF SUCCESSFULL OR UNSUCCESSFUL)

                response.MemberCase = application.MemberCaseGet (memberCase.Id);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public MemberCaseModificationResponse MemberCaseCarePlanIntervention_Add (String token, Server.Core.Individual.Case.MemberCase memberCase, Int64 memberCaseCarePlanId, Int64 copyCareInterventionId, String carePlanInterventionName) {

            MemberCaseModificationResponse response = new MemberCaseModificationResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                memberCase.Application = application; // RESET APPLICATION REFERENCE

                response.ModificationOutcome = memberCase.FindMemberCaseCarePlan (memberCaseCarePlanId).AddIntervention (copyCareInterventionId, carePlanInterventionName);

                if (response.ModificationOutcome != Server.Core.Individual.Enumerations.MemberCaseActionOutcome.Success) { response.SetException (application.LastException); }


                // RELOAD MEMBER CASE (TO SEND UPDATED CASE IF SUCCESSFULL OR UNSUCCESSFUL)

                response.MemberCase = application.MemberCaseGet (memberCase.Id);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        public MemberCaseModificationResponse MemberCaseCarePlanIntervention_Update (String token, Server.Core.Individual.Case.MemberCase memberCase, Int64 memberCaseCarePlanInterventionId) {

            MemberCaseModificationResponse response = new MemberCaseModificationResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                memberCase.Application = application; // RESET APPLICATION REFERENCE

                //response.ModificationOutcome = memberCase.FindMemberCaseCarePlanIntervention (memberCaseCarePlanInterventionId).SaveUpdate ();

                //if (response.ModificationOutcome != Server.Core.Individual.Enumerations.MemberCaseActionOutcome.Success) { response.SetException (application.LastException); }


                // RELOAD MEMBER CASE (TO SEND UPDATED CASE IF SUCCESSFULL OR UNSUCCESSFUL)

                response.MemberCase = application.MemberCaseGet (memberCase.Id);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

                application.SetLastException (applicationException);

            }

            return response;

        }

        #endregion


        #region Case - Member Case Care Plan Assessment

        public ObjectSaveResponse MemberCaseCarePlanAssessmentSave (String token, Server.Core.Individual.Case.MemberCaseCarePlanAssessment assessment) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                assessment.Application = application;

                response.Success = assessment.Save (application);

                response.Id = assessment.Id;

                if (!response.Success) {

                    response.SetException (application.LastException);

                }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion 

        #endregion


        #region Core - Insurer

        #region Benefit Plan

        public BenefitPlanCollectionResponse BenefitPlansAvailable (String token) {

            BenefitPlanCollectionResponse response = new BenefitPlanCollectionResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Collection = application.BenefitPlansAvailable ();

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public DictionaryResponse BenefitPlanDictionary (String token) {

            DictionaryResponse response = new DictionaryResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Dictionary = application.CoreObjectDictionary ("BenefitPlan");

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Insurer.BenefitPlan BenefitPlanGet (String token, Int64 benefitPlanId) {

            Server.Core.Insurer.BenefitPlan benefitPlan = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS NEEDED, OPEN TO ALL USERS

                benefitPlan = application.BenefitPlanGet (benefitPlanId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return benefitPlan;

        }

        #endregion


        #region Contract 

        public ContractCollectionResponse ContractsAvailable (String token) {

            ContractCollectionResponse response = new ContractCollectionResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Collection = application.ContractsAvailable ();

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public DictionaryResponse ContractDictionary (String token) {

            DictionaryResponse response = new DictionaryResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Dictionary = application.CoreObjectDictionary ("Contract");

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Insurer.Contract ContractGet (String token, Int64 benefitId) {

            Server.Core.Insurer.Contract benefit = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS NEEDED, OPEN TO ALL USERS

                benefit = application.ContractGet (benefitId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return benefit;

        }

        #endregion


        #region Coverage Level

        public CoverageLevelCollectionResponse CoverageLevelsAvailable (String token) {

            CoverageLevelCollectionResponse response = new CoverageLevelCollectionResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Collection = application.CoverageLevelsAvailable ();

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public DictionaryResponse CoverageLevelDictionary (String token) {

            DictionaryResponse response = new DictionaryResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Dictionary = application.CoreObjectDictionary ("CoverageLevel");

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Insurer.CoverageLevel CoverageLevelGet (String token, Int64 coverageLevelId) {

            Server.Core.Insurer.CoverageLevel coverageLevel = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS NEEDED, OPEN TO ALL USERS

                coverageLevel = application.CoverageLevelGet (coverageLevelId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return coverageLevel;

        }

        #endregion


        #region Coverage Type

        public CoverageTypeCollectionResponse CoverageTypesAvailable (String token) {

            CoverageTypeCollectionResponse response = new CoverageTypeCollectionResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Collection = application.CoverageTypesAvailable ();

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public DictionaryResponse CoverageTypeDictionary (String token) {

            DictionaryResponse response = new DictionaryResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Dictionary = application.CoreObjectDictionary ("CoverageType");

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Insurer.CoverageType CoverageTypeGet (String token, Int64 coverageTypeId) {

            Server.Core.Insurer.CoverageType coverageType = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS NEEDED, OPEN TO ALL USERS

                coverageType = application.CoverageTypeGet (coverageTypeId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return coverageType;

        }

        #endregion


        #region Insurance Type

        public InsuranceTypeCollectionResponse InsuranceTypesAvailable (String token) {

            InsuranceTypeCollectionResponse response = new InsuranceTypeCollectionResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Collection = application.InsuranceTypesAvailable ();

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public DictionaryResponse InsuranceTypeDictionary (String token) {

            DictionaryResponse response = new DictionaryResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Dictionary = application.CoreObjectDictionary ("InsuranceType");

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Insurer.InsuranceType InsuranceTypeGet (String token, Int64 insuranceTypeId) {

            Server.Core.Insurer.InsuranceType insuranceType = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS NEEDED, OPEN TO ALL USERS

                insuranceType = application.InsuranceTypeGet (insuranceTypeId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return insuranceType;

        }

        #endregion 

        
        #region Insurer

        public InsurerCollectionResponse InsurersAvailable (String token) {

            InsurerCollectionResponse response = new InsurerCollectionResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Collection = application.InsurersAvailable ();

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public DictionaryResponse InsurerDictionary (String token) {

            DictionaryResponse response = new DictionaryResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Dictionary = application.CoreObjectDictionary ("Insurer");

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Insurer.Insurer InsurerGet (String token, Int64 insurerId) {

            Server.Core.Insurer.Insurer insurer = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS NEEDED, OPEN TO ALL USERS

                insurer = application.InsurerGet (insurerId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return insurer;

        }

        #endregion

        
        #region Program 

        public ProgramCollectionResponse ProgramsAvailable (String token) {

            ProgramCollectionResponse response = new ProgramCollectionResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Collection = application.ProgramsAvailable ();

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public DictionaryResponse ProgramDictionary (String token) {

            DictionaryResponse response = new DictionaryResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Dictionary = application.CoreObjectDictionary ("Program");

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Insurer.Program ProgramGet (String token, Int64 programId) {

            Server.Core.Insurer.Program program = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS NEEDED, OPEN TO ALL USERS

                program = application.ProgramGet (programId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return program;

        }

        #endregion

        #endregion 

        
        #region Core - Medical Services

        public SearchResultMedicalServiceHeaderCollectionResponse MedicalServiceHeadersGet (String token) {

            SearchResultMedicalServiceHeaderCollectionResponse response = new SearchResultMedicalServiceHeaderCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.MedicalServiceHeadersGet ();

                if (application.LastException != null) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public SearchResultMedicalServiceHeaderCollectionResponse MedicalServiceHeadersGetByType (String token, Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType serviceType) {

            SearchResultMedicalServiceHeaderCollectionResponse response = new SearchResultMedicalServiceHeaderCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.MedicalServiceHeadersGetByType (serviceType);

                if (application.LastException != null) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException); 

            }

            return response;

        }

        public Mercury.Server.Core.MedicalServices.Service MedicalServiceGet (String token, Int64 serviceId) {

            Mercury.Server.Core.MedicalServices.Service medicalService = null;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                medicalService = application.MedicalServiceGet (serviceId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return medicalService;

        }

        public Int64 MedicalServiceGetIdByName (String token, String serviceName) {

            Int64 medicalServiceId = 0;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                medicalServiceId = application.CoreObjectGetIdByName ("Service", serviceName);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return medicalServiceId;

        }

        public BooleanResponse MedicalServiceDelete (String token, Int64 serviceId) {

            BooleanResponse response = new BooleanResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                Mercury.Server.Core.MedicalServices.Service medicalService = application.MedicalServiceGet (serviceId);

                switch (medicalService.ServiceType) {

                    case Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType.Singleton:

                        medicalService = application.MedicalServiceSingletonGet (serviceId);

                        break;

                    case Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType.Set:

                        medicalService = application.MedicalServiceSetGet (serviceId);

                        break;

                    case Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType.Sequence:

                        // TODO

                        break;

                }

                response.Result = medicalService.Delete ();

                if (!response.Result) {

                    response.SetException (application.LastException);

                }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public Mercury.Server.Core.MedicalServices.ServiceSingleton MedicalServiceSingletonGet (String token, Int64 serviceId) {

            Mercury.Server.Core.MedicalServices.ServiceSingleton medicalService = null;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                medicalService = application.MedicalServiceSingletonGet (serviceId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return medicalService;

        }

        public ObjectSaveResponse MedicalServiceSingletonSave (String token, Mercury.Server.Core.MedicalServices.ServiceSingleton singleton) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                singleton.Application = application;

                response.Success = singleton.Save ();

                response.Id = singleton.Id;

                if (!response.Success) {

                    response.SetException (application.LastException);

                }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public Dictionary<String, String> MedicalServiceSingletonDefinitionValidate (String token, Mercury.Server.Core.MedicalServices.Definitions.ServiceSingletonDefinition singletonDefinition) {

            Dictionary<String, String> validationResponse = new Dictionary<String, String> ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                singletonDefinition.Application = application;

                validationResponse = singletonDefinition.Validate ();

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return validationResponse;

        }

        public MedicalServiceDetailSingletonCollectionResponse MedicalServiceSingletonPreview (String token, Mercury.Server.Core.MedicalServices.ServiceSingleton serviceSingleton) {

            MedicalServiceDetailSingletonCollectionResponse response = new MedicalServiceDetailSingletonCollectionResponse ();


            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                serviceSingleton.Application = application;

                response.Collection = serviceSingleton.Preview (application);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public MedicalServiceDetailSetCollectionResponse MedicalServiceSetPreview (String token, Mercury.Server.Core.MedicalServices.ServiceSet serviceSet) {

            MedicalServiceDetailSetCollectionResponse response = new MedicalServiceDetailSetCollectionResponse ();


            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                serviceSet.Application = application;

                response.Collection = serviceSet.Preview (application);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public Mercury.Server.Core.MedicalServices.ServiceSet MedicalServiceSetGet (String token, Int64 serviceId) {

            Mercury.Server.Core.MedicalServices.ServiceSet medicalService = null;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                medicalService = application.MedicalServiceSetGet (serviceId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return medicalService;

        }

        public Dictionary<String, String> MedicalServiceSetDefinitionValidate (String token, Mercury.Server.Core.MedicalServices.Definitions.ServiceSetDefinition setDefinition) {

            Dictionary<String, String> response = new Dictionary<String, String> ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                setDefinition.Application = application;

                response = setDefinition.Validate ();

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.Add ("Exception", applicationException.Message);

            }

            return response;

        }

        public ObjectSaveResponse MedicalServiceSetSave (String token, Mercury.Server.Core.MedicalServices.ServiceSet serviceSet) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                serviceSet.Application = application;

                response.Success = serviceSet.Save ();

                response.Id = serviceSet.Id;

                if (!response.Success) {

                    response.SetException (application.LastException);

                }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }


        public ObjectSaveResponse MemberServiceAddManual (String token, Int64 memberId, Int64 serviceId, DateTime eventDate) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Success = application.MemberServiceAddManual (memberId, serviceId, eventDate, true);

                if (!response.Success) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public BooleanResponse MemberServiceRemoveManual (String token, Int64 memberServiceId) {

            BooleanResponse response = new BooleanResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Result = application.MemberServiceRemoveManual (memberServiceId);

                if (!response.Result) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }


        public Server.Core.MedicalServices.MemberService MemberServiceGet (String token, Int64 memberServiceId) {

            Server.Core.MedicalServices.MemberService response = null;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response = application.MemberServiceGet (memberServiceId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return response;

        }


        public Int64 MemberServicesGetCount (String token, Int64 memberId, Boolean showHidden) {

            Int64 servicesCount = 0;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                servicesCount = application.MemberServicesGetCount (memberId, showHidden);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return servicesCount;

        }

        public MemberServiceCollectionResponse MemberServicesGetByPage (String token, Int64 memberId, Int32 initialRow, Int32 count, Boolean showHidden) {

            MemberServiceCollectionResponse response = new MemberServiceCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.MemberServicesGetByPage (memberId, initialRow, count, showHidden);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public MemberServiceDetailSingletonCollectionResponse MemberServiceDetailSingletonGet (String token, Int64 memberServiceId) {

            MemberServiceDetailSingletonCollectionResponse response = new MemberServiceDetailSingletonCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.MemberServiceDetailSingletonGet (memberServiceId);

                if (application.LastException != null) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public MemberServiceDetailSetCollectionResponse MemberServiceDetailSetGet (String token, Int64 memberServiceId) {

            MemberServiceDetailSetCollectionResponse response = new MemberServiceDetailSetCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.MemberServiceDetailSetGet (memberServiceId);

                if (application.LastException != null) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion


        #region Core - Member

        #region Member

        public Server.Core.Member.Member MemberGet (String token, Int64 memberId) {

            Server.Core.Member.Member member = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                member = application.MemberGet (memberId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return member;

        }

        public Server.Core.Member.Member MemberGetByEntityId (String token, Int64 entityId) {

            Server.Core.Member.Member member = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                member = application.MemberGetByEntityId (entityId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return member;

        }


        public MemberDemographicsResponse MemberGetDemographics (String token, Int64 memberId) {

            MemberDemographicsResponse demographics = new MemberDemographicsResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                demographics = new MemberDemographicsResponse (application, memberId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                demographics.SetException (applicationException);

            }

            return demographics;

        }

        public MemberDemographicsResponse MemberGetDemographicsByEntityId (String token, Int64 entityId) {

            MemberDemographicsResponse demographics = new MemberDemographicsResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                demographics = new MemberDemographicsResponse ();

                demographics.Member = application.MemberGetByEntityId (entityId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                demographics.SetException (applicationException);

            }

            return demographics;

        }

        #endregion 
        

        #region Member Enrollment

        public Server.Core.Member.MemberEnrollment MemberEnrollmentGet (String token, Int64 memberEnrollmentId) {

            Server.Core.Member.MemberEnrollment memberEnrollment = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                memberEnrollment = application.MemberEnrollmentGet (memberEnrollmentId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return memberEnrollment;

        }

        public MemberEnrollmentCollectionResponse MemberEnrollmentsGet (String token, Int64 memberId) {

            MemberEnrollmentCollectionResponse response = new MemberEnrollmentCollectionResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                response.Collection = application.MemberEnrollmentsGet (memberId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }


        public Server.Core.Member.MemberEnrollmentCoverage MemberEnrollmentCoverageGet (String token, Int64 memberEnrollmentCoverageId) {

            Server.Core.Member.MemberEnrollmentCoverage memberEnrollmentCoverage = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                memberEnrollmentCoverage = application.MemberEnrollmentCoverageGet (memberEnrollmentCoverageId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return memberEnrollmentCoverage;

        }

        public MemberEnrollmentCoverageCollectionResponse MemberEnrollmentCoveragesGet (String token, Int64 memberEnrollmentId) {

            MemberEnrollmentCoverageCollectionResponse response = new MemberEnrollmentCoverageCollectionResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                response.Collection = application.MemberEnrollmentCoveragesGet (memberEnrollmentId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }


        public Server.Core.Member.MemberEnrollmentPcp MemberEnrollmentPcpGet (String token, Int64 memberEnrollmentPcpId) {

            Server.Core.Member.MemberEnrollmentPcp memberEnrollmentPcp = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                memberEnrollmentPcp = application.MemberEnrollmentPcpGet (memberEnrollmentPcpId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return memberEnrollmentPcp;

        }

        public MemberEnrollmentPcpCollectionResponse MemberEnrollmentPcpsGet (String token, Int64 memberEnrollmentId) {

            MemberEnrollmentPcpCollectionResponse response = new MemberEnrollmentPcpCollectionResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                response.Collection = application.MemberEnrollmentPcpsGet (memberEnrollmentId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }


        public Server.Core.Member.MemberEnrollmentTplCob MemberEnrollmentTplCobGet (String token, Int64 memberEnrollmentTplCobId) {

            Server.Core.Member.MemberEnrollmentTplCob memberEnrollmentTplCob = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                memberEnrollmentTplCob = application.MemberEnrollmentTplCobGet (memberEnrollmentTplCobId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return memberEnrollmentTplCob;

        }

        public MemberEnrollmentTplCobCollectionResponse MemberEnrollmentTplCobsGet (String token, Int64 memberId) {

            MemberEnrollmentTplCobCollectionResponse response = new MemberEnrollmentTplCobCollectionResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                response.Collection = application.MemberEnrollmentTplCobsGet (memberId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion 


        #region Member Relationship

        public Server.Core.Member.MemberRelationship MemberRelationshipGet (String token, Int64 memberRelationshipId) {

            Server.Core.Member.MemberRelationship memberRelationship = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                memberRelationship = application.MemberRelationshipGet (memberRelationshipId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return memberRelationship;

        }

        public MemberRelationshipCollectionResponse MemberRelationshipsGet (String token, Int64 memberId) {

            MemberRelationshipCollectionResponse response = new MemberRelationshipCollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.MemberRelationshipsGet (memberId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion

        #endregion


        #region Core - Metrics

        public MetricCollectionResponse MetricsAvailable (String token) {

            MetricCollectionResponse response = new MetricCollectionResponse ();


            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.MetricsAvailable ();

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Metrics.Metric MetricGet (String token, Int64 metricId) {

            Server.Core.Metrics.Metric metric = null;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                metric = application.MetricGet (metricId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return metric;

        }

        public ObjectSaveResponse MetricSave (String token, Server.Core.Metrics.Metric metric) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                metric.Application = application;

                response.Success = metric.Save ();

                response.Id = metric.Id;

                if (!response.Success) {

                    response.SetException (application.LastException);

                }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }


        public ObjectSaveResponse MemberMetricAddManual (String token, Int64 memberId, Int64 metricId, DateTime eventDate, Decimal value) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Success = application.MemberMetricAddManual (memberId, metricId, eventDate, value, true);

                if (!response.Success) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public BooleanResponse MemberMetricRemoveManual (String token, Int64 memberMetricId) {

            BooleanResponse response = new BooleanResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Result = application.MemberMetricRemoveManual (memberMetricId);

                if (!response.Result) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }


        public Int64 MemberMetricsGetCount (String token, Int64 memberId, Boolean showHidden) {

            Int64 servicesCount = 0;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                servicesCount = application.MemberMetricsGetCount (memberId, showHidden);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return servicesCount;

        }

        public MemberMetricCollectionResponse MemberMetricsGetByPage (String token, Int64 memberId, Int32 initialRow, Int32 count, Boolean showHidden) {

            MemberMetricCollectionResponse response = new MemberMetricCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.MemberMetricsGetByPage (memberId, initialRow, count, showHidden);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion


        #region Core - Population

        #region Population Type

        public PopulationTypeCollectionResponse PopulationTypesAvailable (String token) {

            PopulationTypeCollectionResponse response = new PopulationTypeCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.PopulationTypesAvailable ();

                if (application.LastException != null) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Population.PopulationType PopulationTypeGet (String token, Int64 populationTypeId) {

            Mercury.Server.Core.Population.PopulationType populationType = null;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                populationType = new Mercury.Server.Core.Population.PopulationType (application, populationTypeId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return populationType;

        }

        public ObjectSaveResponse PopulationTypeSave (String token, Mercury.Server.Core.Population.PopulationType populationType) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                populationType.Application = application;

                response.Success = populationType.Save (application);

                response.Id = populationType.Id;

                if (!response.Success) {

                    response.SetException (application.LastException);

                }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion 


        public PopulationHeadersCollectionResponse PopulationsAvailable (String token) {

            PopulationHeadersCollectionResponse response = new PopulationHeadersCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.PopulationsAvailable ();

                if (application.LastException != null) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Population.Population PopulationGet (String token, Int64 populationId) {

            Mercury.Server.Core.Population.Population population = null;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                population = new Mercury.Server.Core.Population.Population (application, populationId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return population;

        }

        public Server.Core.Population.PopulationEvents.PopulationServiceEvent PopulationServiceEventGet (String token, Int64 populationServiceEventId) {

            Mercury.Server.Core.Population.PopulationEvents.PopulationServiceEvent populationServiceEvent = null;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                populationServiceEvent = application.PopulationServiceEventGet (populationServiceEventId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return populationServiceEvent;

        }


        public ObjectSaveResponse PopulationSave (String token, Mercury.Server.Core.Population.Population population) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                population.Application = application;

                response.Success = population.Save (application);

                response.Id = population.Id;

                if (!response.Success) {

                    response.SetException (application.LastException);

                }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public BooleanResponse PopulationDelete (String token, Int64 populationId) {

            BooleanResponse response = new BooleanResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                Mercury.Server.Core.Population.Population population = new Mercury.Server.Core.Population.Population (application, populationId);

                response.Result = population.Delete (application);

                if (!response.Result) {

                    response.SetException (application.LastException);

                }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public Dictionary<String, String> Population_DataBindingContexts (String token, Server.Core.Population.Population population) {

            Dictionary<String, String> bindingContexts = new Dictionary<String, String> ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                population.Application = application;

                bindingContexts = population.DataBindingContexts;

            }

            catch (Exception applicationException) {

                System.Diagnostics.Debug.WriteLine (applicationException.Message);

            }

            return bindingContexts;

        }


        #region Population Membership

        public Server.Core.Population.PopulationMembership PopulationMembershipGet (String token, Int64 populationMembershipId) {

            Mercury.Server.Core.Population.PopulationMembership populationMembership = null;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                populationMembership = application.PopulationMembershipGet (populationMembershipId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return populationMembership;

        }

        public PopulationMembershipServiceEventCollectionResponse PopulationMembershipServiceEventsGetByPopulationMembership (String token, Int64 populationMembershipId) {

            PopulationMembershipServiceEventCollectionResponse response = new PopulationMembershipServiceEventCollectionResponse ();

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.PopulationMembershipServiceEventsGet (populationMembershipId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Population.PopulationEvents.PopulationMembershipServiceEvent PopulationMembershipServiceEventGet (String token, Int64 populationMembershipServiceEventId) {

            Mercury.Server.Core.Population.PopulationEvents.PopulationMembershipServiceEvent populationMembershipServiceEvent = null;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                populationMembershipServiceEvent = application.PopulationMembershipServiceEventGet (populationMembershipServiceEventId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return populationMembershipServiceEvent;

        }


        public PopulationMembershipCollectionResponse PopulationMembershipGetByMember (String token, Int64 memberId) {

            PopulationMembershipCollectionResponse response = new PopulationMembershipCollectionResponse ();


            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.PopulationMembershipGetByMember (memberId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public PopulationMembershipSummaryCollectionResponse PopulationMembershipSummaryByMember (String token, Int64 memberId) {

            PopulationMembershipSummaryCollectionResponse response = new PopulationMembershipSummaryCollectionResponse ();


            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.PopulationMembershipSummaryByMember (memberId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public PopulationMembershipServiceEventDataViewCollectionResponse PopulationMembershipServiceEventsByMembershipDataView (String token, Int64 membershipId) {

            PopulationMembershipServiceEventDataViewCollectionResponse response = new PopulationMembershipServiceEventDataViewCollectionResponse ();


            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.PopulationMembershipServiceEventsByMembershipDataView (membershipId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public PopulationMembershipTriggerEventDataViewCollectionResponse PopulationMembershipTriggerEventsByMembershipDataView (String token, Int64 membershipId) {

            PopulationMembershipTriggerEventDataViewCollectionResponse response = new PopulationMembershipTriggerEventDataViewCollectionResponse ();


            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.PopulationMembershipTriggerEventsByMembershipDataView (membershipId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public Int64 PopulationMembershipGetCountByName (String token, Int64 populationId, String namePrefix) {

            Int64 itemCount = 0;

            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                itemCount = application.PopulationMembershipGetCountByNamePrefix (populationId, namePrefix);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return itemCount;

        }

        public PopulationMembershipEntryStatusDataViewCollectionResponse PopulationMembershipGetByMembershipPage (String token, Int64 populationId, String namePrefix, Int64 initialRow, Int32 count) {

            PopulationMembershipEntryStatusDataViewCollectionResponse response = new PopulationMembershipEntryStatusDataViewCollectionResponse ();


            Mercury.Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.PopulationMembershipGetByMembershipPage (populationId, namePrefix, initialRow, count);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion 

        #endregion


        #region Core - Provider

        #region Provider

        public Server.Core.Provider.Provider ProviderGet (String token, Int64 providerId) {

            Server.Core.Provider.Provider provider = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                provider = application.ProviderGet (providerId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return provider;

        }

        public Server.Core.Provider.Provider ProviderGetByEntityId (String token, Int64 entityId) {

            Server.Core.Provider.Provider provider = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                provider = application.ProviderGetByEntityId (entityId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return provider;

        }

        #endregion 
        
        

        public ProviderEnrollmentCollectionResponse ProviderEnrollmentsGet (String token, Int64 providerId) {

            ProviderEnrollmentCollectionResponse response = new ProviderEnrollmentCollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.ProviderEnrollmentsGet (providerId);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Provider.ProviderEnrollment ProviderEnrollmentGet (String token, Int64 providerEnrollmentId) {

            Server.Core.Provider.ProviderEnrollment providerEnrollment = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                providerEnrollment = application.ProviderEnrollmentGet (providerEnrollmentId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return providerEnrollment;

        }


        public ProviderAffiliationCollectionResponse ProviderAffiliationsGet (String token, Int64 providerId) {

            ProviderAffiliationCollectionResponse response = new ProviderAffiliationCollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.ProviderAffiliationsGet (providerId);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Provider.ProviderAffiliation ProviderAffiliationGet (String token, Int64 providerAffiliationId) {

            Server.Core.Provider.ProviderAffiliation providerAffiliation = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                providerAffiliation = application.ProviderAffiliationGet (providerAffiliationId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return providerAffiliation;

        }


        public ProviderContractCollectionResponse ProviderContractsGet (String token, Int64 providerId) {

            ProviderContractCollectionResponse response = new ProviderContractCollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.ProviderContractsGet (providerId);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Provider.ProviderContract ProviderContractGet (String token, Int64 providerContractId) {

            Server.Core.Provider.ProviderContract providerContract = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                providerContract = application.ProviderContractGet (providerContractId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return providerContract;

        }


        public ProviderServiceLocationCollectionResponse ProviderServiceLocationsGet (String token, Int64 providerId) {

            ProviderServiceLocationCollectionResponse response = new ProviderServiceLocationCollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.ProviderServiceLocationsGet (providerId);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }


        #endregion 


        #region Core - Sponsor

        public Server.Core.Sponsor.Sponsor SponsorGet (String token, Int64 sponsorId) {

            Server.Core.Sponsor.Sponsor sponsor = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                sponsor = application.SponsorGet (sponsorId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return sponsor;

        }

        #endregion 


        #region Core.Work

        #region Work - Routing Rules

        public RoutingRuleCollectionResponse RoutingRulesAvailable (String token) {

            RoutingRuleCollectionResponse response = new RoutingRuleCollectionResponse ();


            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.RoutingRulesAvailable ();

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Work.RoutingRule RoutingRuleGet (String token, Int64 routingRuleId) {

            Server.Core.Work.RoutingRule routingRule = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                routingRule = new Server.Core.Work.RoutingRule (application, routingRuleId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return routingRule;

        }

        public ObjectSaveResponse RoutingRuleSave (String token, Server.Core.Work.RoutingRule routingRule) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Success = routingRule.Save (application);

                response.Id = routingRule.Id;

                if (!response.Success) {

                    response.SetException (application.LastException);

                }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion


        #region Work - Workflow

        public WorkflowCollectionResponse WorkflowsAvailable (String token) {

            WorkflowCollectionResponse response = new WorkflowCollectionResponse ();

            Server.Application application = null;

            try { 
                

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                // 2010-06-16 [DQ]: DISABLE PERMISSION VALIDATION TO ALLOW FOR PRECACHING THE WORKFLOWS
                
                //#region VALIDATE PERMISSION: ENTERPRISE MANAGEMENT, FILTER BY WORK QUEUE, ASSIGNED TO CURRENT SESSION, OR FOR SPECIFIC ITEM OBJECT ID

                //Boolean hasPermission = false;

                //hasPermission |= application.HasEnvironmentPermission (Server.EnvironmentPermissions.EnvironmentAdministrator);

                //hasPermission |= application.HasEnvironmentPermission (Server.EnvironmentPermissions.WorkflowReview);

                //hasPermission |= application.HasEnvironmentPermission (Server.EnvironmentPermissions.WorkflowManage);
                

                //if (!hasPermission) { throw new ApplicationException ("Permission Denied."); }
                
                //#endregion 
                

                response.Collection = application.WorkflowsAvailable ();

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }


        /// <summary>
        /// Retreives a Workflow from the Database by the Workflow's ID. 
        /// </summary>
        /// <remarks>
        /// This will validate the caller has the appropriate permissions to retreive the Workflow.
        /// 1. Environment Permission for Workflow Review
        /// 2. Environment Permission for Workflow Manage
        /// 3. Session Environment Permission, assigned to Work Queue that user has access. (where not Denied)
        /// </remarks>
        /// <param name="token"></param>
        /// <param name="workflowId"></param>
        /// <returns>Server.Core.Work.Workflow Object, or NULL if Work flow doesn't exist or Permission is denied.</returns>
        public Server.Core.Work.Workflow WorkflowGet (String token, Int64 workflowId) {

            Server.Core.Work.Workflow workflow = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                workflow = application.WorkflowGet (workflowId);


                // 2010-06-22 [DQ]: REMOVED PERMISSION CHECK, AVAILABLE TO ALL USERS

                //// VALIDATE PERMISSION (MUST HAVE SPECIFIC PERMISSION TO WORK QUEUE OR VIEW/MANAGE PERMISSION FOR WORK QUEUES)

                //Boolean hasPermission = application.HasEnvironmentPermission (Server.EnvironmentPermissions.WorkflowManage);

                //hasPermission |= application.HasEnvironmentPermission (Server.EnvironmentPermissions.WorkflowReview);

                //if (hasPermission) {

                //    workflow = application.WorkflowGet (workflowId);

                //}


                //if (!hasPermission) { 

                //    // CHECK FOR PERMISSION BY WORK QUEUE ASSIGNMENT

                //    Dictionary<Int64, Server.Core.Work.Workflow> workflows = new Dictionary<Int64, Server.Core.Work.Workflow> ();

                //    foreach (Int64 currentWorkQueueId in application.Session.WorkQueuePermissions.Keys) {

                //        Server.Core.Work.Workflow currentWorkflow = application.WorkflowGetByWorkQueueId (currentWorkQueueId);

                //        if (currentWorkflow.Id == workflowId) {

                //            hasPermission = true;

                //            workflow = currentWorkflow;

                //            break;

                //        }

                //    }

                //}

                //if (!hasPermission) {

                //    // CHECK FOR WORKFLOW THROUGH WORKFLOW PERMISSIONS

                //    workflow = application.WorkflowGet (workflowId);

                //    hasPermission = workflow.HasPermissionForSession ();

                //}

                //if (!hasPermission) { workflow = null; } // IF NO PERMISSION, CLEAR WORKFLOW OBJECT

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return workflow;

        }

        public Server.Core.Work.Workflow WorkflowGetByName (String token, String workflowName) {

            Server.Core.Work.Workflow workflow = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                Int64 workflowId = application.CoreObjectGetIdByName ("Workflow", workflowName);

                workflow = WorkflowGet (token, workflowId);

            }

            catch (Exception applicationException) {

                if (application != null) { application.SetLastException (applicationException); }

            }

            return workflow;

        }

        public Server.Core.Work.Workflow WorkflowGetByWorkQueueId (String token, Int64 workQueueId) {

            Server.Core.Work.Workflow workflow = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                workflow = application.WorkflowGetByWorkQueueId (workQueueId);


                // PERFORM SECOND RETREIVAL TO FORCE PERMISSION CHECK TO WORKFLOW

                if (workflow != null) { workflow = WorkflowGet (token, workflow.Id); }
                
            }

            catch (Exception applicationException) {

                if (application != null) { application.SetLastException (applicationException); }

            }

            return workflow;

        }


        public ObjectSaveResponse WorkflowSave (String token, Server.Core.Work.Workflow workflow) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                // VALIDATE PERMISSIONS

                Boolean hasPermission = application.HasEnvironmentPermission (Server.EnvironmentPermissions.WorkflowManage);

                if (hasPermission) {

                    response.Success = workflow.Save (application);

                    response.Id = workflow.Id;

                    if (!response.Success) { response.SetException (application.LastException); }

                }

                else { response.SetException (new ApplicationException ("Permission Denied.")); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion


        #region Work - Workflow Flow Control

        public WorkflowResponse WorkflowStart (String token, Server.Workflows.WorkflowStartRequest startRequest) {

            WorkflowResponse response = new WorkflowResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response = new WorkflowResponse (application.WorkflowStart (startRequest));

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public WorkflowResponse WorkflowContinue (String token, String workflowName, Guid workflowInstanceId, Server.Workflows.UserInteractions.Response.ResponseBase userInteractionResponse) {

            DateTime startTime = DateTime.Now;

            WorkflowResponse workflowResponse = new WorkflowResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                workflowResponse = new WorkflowResponse (application.WorkflowContinue (workflowName, workflowInstanceId, userInteractionResponse));

            }

            catch (Exception applicationException) {

                workflowResponse.SetException (applicationException);

            }

            System.Diagnostics.Debug.WriteLine ("[Server] WorkflowContinue: " + DateTime.Now.Subtract (startTime).TotalMilliseconds.ToString ());

            return workflowResponse;

        }

        #endregion 


        #region Work - Work Outcome

        public WorkOutcomeCollectionResponse WorkOutcomesAvailable (String token) {

            WorkOutcomeCollectionResponse response = new WorkOutcomeCollectionResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Collection = application.WorkOutcomesAvailable ();

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public DictionaryResponse WorkOutcomeDictionary (String token) {

            DictionaryResponse response = new DictionaryResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Dictionary = application.CoreObjectDictionary ("WorkOutcome");

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.Work.WorkOutcome WorkOutcomeGet (String token, Int64 workOutcomeId) {

            Server.Core.Work.WorkOutcome workOutcome = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS NEEDED, OPEN TO ALL USERS

                workOutcome = application.WorkOutcomeGet (workOutcomeId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return workOutcome;

        }

        public Server.Core.Work.WorkOutcome WorkOutcomeGetByName (String token, String workOutcomeName) {

            Server.Core.Work.WorkOutcome workOutcome = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS NEEDED, OPEN TO ALL USERS

                workOutcome = application.WorkOutcomeGet (workOutcomeName);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return workOutcome;

        }

        public ObjectSaveResponse WorkOutcomeSave (String token, Server.Core.Work.WorkOutcome workOutcome) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                // VALIDATE PERMISSIONS

                Boolean hasPermission = application.HasEnvironmentPermission (Server.EnvironmentPermissions.WorkOutcomeManage);

                if (hasPermission) {

                    response.Success = workOutcome.Save (application);

                    response.Id = workOutcome.Id;

                    if (!response.Success) { response.SetException (application.LastException); }

                }

                else { response.SetException (new ApplicationException ("Permission Denied.")); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion


        #region Work - Work Queue

        public WorkQueueCollectionResponse WorkQueuesAvailable (String token) {

            WorkQueueCollectionResponse response = new WorkQueueCollectionResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // 2010-06-16 [DQ]: DISABLE PERMISSION VALIDATION TO ALLOW FOR PRECACHING THE WORK QUEUES

                response.Collection = application.WorkQueuesAvailable ();

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public DictionaryResponse WorkQueueDictionary (String token) {

            DictionaryResponse response = new DictionaryResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Dictionary = application.CoreObjectDictionary ("WorkQueue");

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }


        /// <summary>
        /// Retreives a Work Queue from the Database by the Work Queue's ID. 
        /// </summary>
        /// <remarks>
        /// This will validate the caller has the appropriate permissions to retreive the Work Queue.
        /// 1. Environment Permission for Work Queue Review
        /// 2. Environment Permission for Work Queue Manage
        /// 3. Session Environment Permission (where not Denied)
        /// </remarks>
        /// <param name="token"></param>
        /// <param name="workQueueId"></param>
        /// <returns>Server.Core.Work.WorkQueue Object, or NULL if Work Queue doesn't exist or Permission is denied.</returns>
        public Server.Core.Work.WorkQueue WorkQueueGet (String token, Int64 workQueueId) {

            Server.Core.Work.WorkQueue workQueue = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                // 2010-06-22 [DQ]: REMOVED PERMISSION VALIDATION, OPEN TO ALL USERS

                //// VALIDATE PERMISSION (MUST HAVE SPECIFIC PERMISSION TO WORK QUEUE OR VIEW/MANAGE PERMISSION FOR WORK QUEUES)

                //Boolean hasPermission = application.HasEnvironmentPermission (Server.EnvironmentPermissions.WorkQueueManage);

                //hasPermission |= application.HasEnvironmentPermission (Server.EnvironmentPermissions.WorkQueueReview);

                //hasPermission |= application.Session.WorkQueuePermissions.ContainsKey (workQueueId);


                //if (hasPermission) {

                //    workQueue = application.WorkQueueGet (workQueueId);

                //}

                workQueue = application.WorkQueueGet (workQueueId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return workQueue;

        }

        public Server.Core.Work.WorkQueue WorkQueueGetByName (String token, String workQueueName) {

            Server.Core.Work.WorkQueue workQueue = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                Int64 workQueueId = application.CoreObjectGetIdByName ("WorkQueue", workQueueName);

                workQueue = WorkQueueGet (token, workQueueId);

            }

            catch (Exception applicationException) {

                if (application != null) { application.SetLastException (applicationException); }

            }

            return workQueue;

        }

        public Server.Core.Work.WorkQueue WorkQueueGetByWorkQueueItem (String token, Int64 workQueueItemId) {

            Server.Core.Work.WorkQueue workQueue = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                Int64 workQueueId = application.WorkQueueGetIdByWorkQueueItem (workQueueItemId);

                workQueue = WorkQueueGet (token, workQueueId);

            }

            catch (Exception applicationException) {

                if (application != null) { application.SetLastException (applicationException); }

            }

            return workQueue;

        }


        public ObjectSaveResponse WorkQueueSave (String token, Server.Core.Work.WorkQueue workQueue) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                // VALIDATE PERMISSIONS

                Boolean hasPermission = application.HasEnvironmentPermission (Server.EnvironmentPermissions.WorkQueueManage);

                if (hasPermission) {

                    response.Success = workQueue.Save (application);

                    response.Id = workQueue.Id;

                    if (!response.Success) { response.SetException (application.LastException); }

                }

                else { response.SetException (new ApplicationException ("Permission Denied.")); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public ObjectSaveResponse WorkQueueSaveGetWork (String token, Server.Core.Work.WorkQueue workQueue) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                // VALIDATE PERMISSIONS

                Boolean hasPermission = application.SessionWorkQueueHasManagePermission (workQueue.Id);

                if (hasPermission) {

                    workQueue.Application = application;

                    response.Success = workQueue.UpdateGetWork ();

                    response.Id = workQueue.Id;

                    if (!response.Success) { response.SetException (application.LastException); }

                }

                else { response.SetException (new ApplicationException ("Permission Denied.")); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public GetWorkResponse WorkQueueGetWork (String token, Int64 workQueueId) {

            GetWorkResponse response = new GetWorkResponse ();

            Server.Application application = null;


            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                Server.Core.Work.WorkQueue workQueue = WorkQueueGet (token, workQueueId);

                if (workQueue == null) { throw application.LastException; }


                response.WorkQueueItem = workQueue.GetWork ();

                response.SetException (application.LastException);

                if ((response.WorkQueueItem != null) && (!response.HasException)) {

                    response.WorkQueue = workQueue;

                    response.Workflow = application.WorkflowGet (workQueue.WorkflowId);

                }

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public BooleanResponse WorkQueueInsertEntity (String token, Int64 workQueueId, Int64 entityId, Server.Core.CoreObject sender, Server.Core.CoreObject eventObject, Int64 eventInstanceId, String eventDescription, Int32 priority) {

            BooleanResponse response = new BooleanResponse ();

            Server.Application application = null;

            Server.Core.Work.WorkQueue workQueue = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                workQueue = application.WorkQueueGet (workQueueId);

                response.Result = workQueue.InsertEntity (entityId, String.Empty, sender, eventObject, eventInstanceId, eventDescription, priority);

                if (!response.Result) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion


        #region Work - Work Queue Item

        public Server.Core.Work.WorkQueueItem WorkQueueItemGet (String token, Int64 workQueueItemId) {

            Server.Core.Work.WorkQueueItem workQueueItem = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                // TODO: VALIDATE PERMISSION (OWNER OF ITEM, OR AT LEAST VIEW RIGHTS TO WORK QUEUE)

                workQueueItem = application.WorkQueueItemGet (workQueueItemId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return workQueueItem;

        }


        public BooleanResponse WorkQueueItemAssignTo (String token, Int64 workQueueItemId, Int64 securityAuthorityId, String userAccountId, String userAccountName, String userDisplayName, String assignmentSource) {

            BooleanResponse response = new BooleanResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                Server.Core.Work.WorkQueueItem workQueueItem = new Server.Core.Work.WorkQueueItem (application, workQueueItemId);

                response.Result = workQueueItem.AssignTo (securityAuthorityId, userAccountId, userAccountName, userDisplayName, assignmentSource, true);

                if (!response.Result) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (application.LastException);

            }

            return response;

        }

        public BooleanResponse WorkQueueItemMoveToQueue (String token, Int64 workQueueItemId, Int64 workQueueId) {

            BooleanResponse response = new BooleanResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                Server.Core.Work.WorkQueueItem workQueueItem = new Server.Core.Work.WorkQueueItem (application, workQueueItemId);

                response.Result = workQueueItem.MoveToQueue (workQueueId, true);

                if (!response.Result) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (application.LastException);

            }

            return response;

        }

        public BooleanResponse WorkQueueItemClose (String token, Int64 workQueueItemId, Int64 workOutcomeId) {

            BooleanResponse response = new BooleanResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                Server.Core.Work.WorkQueueItem workQueueItem = new Server.Core.Work.WorkQueueItem (application, workQueueItemId);

                response.Result = workQueueItem.Close (workOutcomeId, true);

                if (!response.Result) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (application.LastException);

            }

            return response;

        }

        public BooleanResponse WorkQueueItemSuspend (String token, Int64 workQueueItemId, String lastStep, String nextStep, Int32 constraintDays, Int32 milestoneDays, Boolean releaseItem) {

            BooleanResponse response = new BooleanResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                Server.Core.Work.WorkQueueItem workQueueItem = new Server.Core.Work.WorkQueueItem (application, workQueueItemId);

                response.Result = workQueueItem.Suspend (lastStep, nextStep, constraintDays, milestoneDays, releaseItem);

                if (!response.Result) { response.SetException (application.LastException); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (application.LastException);

            }

            return response;

        }


        public WorkQueueItemSenderCollectionResponse WorkQueueItemSendersGet (String token, Int64 workQueueItemId) {

            WorkQueueItemSenderCollectionResponse response = new WorkQueueItemSenderCollectionResponse ();


            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                // TODO: VALIDATE PERMISSION TO WORK QUEUE ITEM 

                response.Collection = application.WorkQueueItemSendersGet (workQueueItemId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public WorkQueueItemAssignmentHistoryCollectionResponse WorkQueueItemAssignmentHistoryGet (String token, Int64 workQueueItemId) {

            WorkQueueItemAssignmentHistoryCollectionResponse response = new WorkQueueItemAssignmentHistoryCollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.WorkQueueItemAssignmentHistoryGet (workQueueItemId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public WorkQueueItemWorkflowStepCollectionResponse WorkQueueItemWorkflowStepsGet (String token, Int64 workQueueItemId) {

            WorkQueueItemWorkflowStepCollectionResponse response = new WorkQueueItemWorkflowStepCollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.WorkQueueItemWorkflowStepsGet (workQueueItemId);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion 
        

        #region Work - Work Queue Items (By Views)

        public Int64 WorkQueueItemsGetCount (String token, List<Server.Data.FilterDescriptor> filters) {

            Int64 itemCount = 0;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                itemCount = application.WorkQueueItemsGetCount (filters);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                itemCount = -1;

            }

            return itemCount;

        }

        public Int64 WorkQueueItemsGetCountByView (String token, Server.Core.Work.WorkQueueView workQueueView, List<Server.Data.FilterDescriptor> filters) {

            Int64 itemCount = 0;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                itemCount = application.WorkQueueItemsGetCount (workQueueView, filters);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                itemCount = -1;

            }

            return itemCount;

        }

        public WorkQueueItemCollectionResponse WorkQueueItemsGetByViewPage (String token, Server.Core.Work.WorkQueueView workQueueView, List<Server.Data.FilterDescriptor> filters, List<Server.Data.SortDescriptor> sorts, Int32 initialRow, Int32 count) {

            WorkQueueItemCollectionResponse response = new WorkQueueItemCollectionResponse ();


            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                #region VALIDATE PERMISSION: ENTERPRISE MANAGEMENT, FILTER BY WORK QUEUE, ASSIGNED TO CURRENT SESSION, OR FOR SPECIFIC ITEM OBJECT ID

                Boolean hasPermission = false;

                hasPermission |= application.HasEnvironmentPermission (Server.EnvironmentPermissions.EnvironmentAdministrator);


                // CHECK PERMISSIONS BY FILTERS
                
                Int64 assignedToSecurityAuthorityId = 0;

                String assignedToUserAccountId = String.Empty;

                foreach (Server.Data.FilterDescriptor currentFilter in filters) {

                    if (currentFilter.Operator == Data.Enumerations.DataFilterOperator.IsEqualTo) {

                        switch (currentFilter.PropertyPath) {

                            case "WorkQueueId": hasPermission |= application.Session.WorkQueuePermissions.ContainsKey (Convert.ToInt64 (currentFilter.Parameter.Value)); break;

                            case "AssignedToSecurityAuthorityId": assignedToSecurityAuthorityId = Convert.ToInt64 (currentFilter.Parameter.Value); break;

                            case "AssignedToUserAccountId": assignedToUserAccountId = (String) currentFilter.Parameter.Value; break;

                            case "ItemObjectId": hasPermission = true; break;

                        } // switch (currentFilter.PropertyPath)

                    } 

                } // foreach


                // HAS FILTER FOR ASSIGNED TO AND MATCHES SESSION

                hasPermission |= ((assignedToSecurityAuthorityId == application.Session.SecurityAuthorityId) && (assignedToUserAccountId == application.Session.UserAccountId));



                if (!hasPermission) { throw new ApplicationException ("Permission Denied."); }
                
                #endregion 


                response.Collection = application.WorkQueueItemsGetByViewPage (workQueueView, filters, sorts, initialRow, count);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion 
        

        #region Work - Work Team

        public WorkTeamCollectionResponse WorkTeamsAvailable (String token) {

            WorkTeamCollectionResponse response = new WorkTeamCollectionResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                // 2010-06-17 [DQ]: REMOVED REQUIRED PERMISSION, OPEN TO ALL USERS

                #region VALIDATE PERMISSION: ENTERPRISE MANAGEMENT, FILTER BY WORK QUEUE, ASSIGNED TO CURRENT SESSION, OR FOR SPECIFIC ITEM OBJECT ID

                //Boolean hasPermission = false;

                //hasPermission |= application.HasEnvironmentPermission (Server.EnvironmentPermissions.EnvironmentAdministrator);

                //hasPermission |= application.HasEnvironmentPermission (Server.EnvironmentPermissions.WorkTeamReview);

                //hasPermission |= application.HasEnvironmentPermission (Server.EnvironmentPermissions.WorkTeamManage);


                //if (!hasPermission) { throw new ApplicationException ("Permission Denied."); }

                #endregion


                response.Collection = application.WorkTeamsAvailable ();

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public WorkTeamCollectionResponse WorkTeamsForSession (String token) {

            WorkTeamCollectionResponse response = new WorkTeamCollectionResponse ();


            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Collection = application.WorkTeamsForSession ();

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }


        /// <summary>
        /// Retreives a Work Team from the Database by the Work Team's ID. 
        /// </summary>
        /// <remarks>
        /// This will validate the caller has the appropriate permissions to retreive the Work Team.
        /// 1. Environment Permission for Work Team Review
        /// 2. Environment Permission for Work Team Manage
        /// 3. Session Environment Permission (where not Denied)
        /// </remarks>
        /// <param name="token"></param>
        /// <param name="workTeamId"></param>
        /// <returns>Server.Core.Work.WorkTeam Object, or NULL if Work Team doesn't exist or Permission is denied.</returns>
        public Server.Core.Work.WorkTeam WorkTeamGet (String token, Int64 workTeamId) {

            Server.Core.Work.WorkTeam workTeam = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                // 2010-06-17 [DQ]: REMOVED PERMISSIONS 

                //// VALIDATE PERMISSION (MUST HAVE SPECIFIC PERMISSION TO WORK TEAM OR VIEW/MANAGE PERMISSION FOR WORK TEAMS), OR BE IN THE TEAM

                //Boolean hasPermission = application.HasEnvironmentPermission (Server.EnvironmentPermissions.WorkTeamManage);

                //hasPermission |= application.HasEnvironmentPermission (Server.EnvironmentPermissions.WorkTeamReview);

                //foreach (Server.Core.Work.WorkTeam currentWorkTeam in application.WorkTeamsForSession ()) {

                //    if (currentWorkTeam.Id == workTeamId) {

                //        hasPermission = true;

                //        // workTeam = currentWorkTeam; // REMOVED TO FORCE RELOAD, WHICH WILL INCLUDE LOADING THE MEMBERSHIP 

                //        break;

                //    }

                //}


                //if ((hasPermission) && (workTeam == null)) {

                //    workTeam = application.WorkTeamGet (workTeamId);

                //}

                workTeam = application.WorkTeamGet (workTeamId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return workTeam;

        }

        public Server.Core.Work.WorkTeam WorkTeamGetByName (String token, String workTeamName) {

            Server.Core.Work.WorkTeam workTeam = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                Int64 workTeamId = application.CoreObjectGetIdByName ("WorkTeam", workTeamName);

                workTeam = WorkTeamGet (token, workTeamId);

            }

            catch (Exception applicationException) {

                if (application != null) { application.SetLastException (applicationException); }

            }

            return workTeam;

        }


        public ObjectSaveResponse WorkTeamSave (String token, Server.Core.Work.WorkTeam workTeam) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                // VALIDATE PERMISSIONS

                Boolean hasPermission = application.HasEnvironmentPermission (Server.EnvironmentPermissions.WorkTeamManage);

                if (hasPermission) {
                    
                    response.Success = workTeam.Save (application);

                    response.Id = workTeam.Id;

                    if (!response.Success) { response.SetException (application.LastException); }

                }

                else { response.SetException (new ApplicationException ("Permission Denied.")); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion


        #region Work - Work Queue View

        public WorkQueueViewCollectionResponse WorkQueueViewsAvailable (String token) {

            WorkQueueViewCollectionResponse response = new WorkQueueViewCollectionResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Collection = application.WorkQueueViewsAvailable ();

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public DictionaryResponse WorkQueueViewDictionary (String token) {

            DictionaryResponse response = new DictionaryResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Dictionary = application.CoreObjectDictionary ("WorkQueueView");

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }


        public Server.Core.Work.WorkQueueView WorkQueueViewGet (String token, Int64 workQueueViewId) {

            Server.Core.Work.WorkQueueView workQueueView = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                workQueueView = application.WorkQueueViewGet (workQueueViewId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return workQueueView;

        }

        public Server.Core.Work.WorkQueueView WorkQueueViewGetByName (String token, String workQueueViewName) {

            Server.Core.Work.WorkQueueView workQueueView = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                Int64 workQueueViewId = application.CoreObjectGetIdByName ("WorkQueueView", workQueueViewName);

                workQueueView = WorkQueueViewGet (token, workQueueViewId);

            }

            catch (Exception applicationException) {

                if (application != null) { application.SetLastException (applicationException); }

            }

            return workQueueView;

        }


        public ObjectSaveResponse WorkQueueViewSave (String token, Server.Core.Work.WorkQueueView workQueueView) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                // VALIDATE PERMISSIONS

                Boolean hasPermission = application.HasEnvironmentPermission (Server.EnvironmentPermissions.WorkQueueViewManage);

                if (hasPermission) {

                    response.Success = workQueueView.Save (application);

                    response.Id = workQueueView.Id;

                    if (!response.Success) { response.SetException (application.LastException); }

                }

                else { response.SetException (new ApplicationException ("Permission Denied.")); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }


        public DictionaryStringResponse WorkQueueView_ValidateFieldDefinition (String token, Server.Core.Work.WorkQueueView workQueueView, Server.Core.Work.WorkQueueViewFieldDefinition fieldDefinition) {

            DictionaryStringResponse response = new DictionaryStringResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                workQueueView.Application = application;

                response.Dictionary = workQueueView.ValidateFieldDefinition (fieldDefinition);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public Dictionary<String, Boolean> WorkQueueView_WellKnownFields (String token, Server.Core.Work.WorkQueueView workQueueView) {

            Dictionary<String, Boolean> wellKnownFields = new Dictionary<String, Boolean> ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                workQueueView.Application = application;

                wellKnownFields = workQueueView.WellKnownFields;

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return wellKnownFields;

        }

        #endregion


        #region Work Queue Monitor

        public WorkQueueSummaryCollectionResponse WorkQueueMonitorSummary (String token) {

            WorkQueueSummaryCollectionResponse response = new WorkQueueSummaryCollectionResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS, PERMISSIONS ARE VALIDATED ON A PER WORK QUEUE BASIS

                response.Collection = application.WorkQueueMonitorSummary ();

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }
        
        public DictionaryKeyCountResponse WorkQueueMonitorAgingByWorkQueue (String token, Int64 workQueueId) {

            DictionaryKeyCountResponse response = new DictionaryKeyCountResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                // MUST HAVE MANAGE PERMISSION OF REQUESTED WORK QUEUE

                Boolean hasPermission = application.Session.WorkQueuePermissions.ContainsKey (workQueueId);

                if (hasPermission) {

                    hasPermission = (application.Session.WorkQueuePermissions[workQueueId] == Server.Core.Work.Enumerations.WorkQueueTeamPermission.Manage);

                }

                if (hasPermission) { 
                    
                    response.Dictionary = application.WorkQueueMonitorAging (workQueueId); 
                
                }

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public DictionaryKeyCountResponse WorkQueueMonitorAgingAvailableByWorkQueue (String token, Int64 workQueueId) {

            DictionaryKeyCountResponse response = new DictionaryKeyCountResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                // MUST HAVE MANAGE PERMISSION OF REQUESTED WORK QUEUE

                Boolean hasPermission = application.Session.WorkQueuePermissions.ContainsKey (workQueueId);

                if (hasPermission) {

                    hasPermission = (application.Session.WorkQueuePermissions[workQueueId] == Server.Core.Work.Enumerations.WorkQueueTeamPermission.Manage);

                }

                if (hasPermission) {

                    response.Dictionary = application.WorkQueueMonitorAgingAvailable (workQueueId);

                }

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion 

        #endregion


        #region Data Explorer
        
        public DataExplorerCollectionResponse DataExplorersAvailable (String token) {

            DataExplorerCollectionResponse response = new DataExplorerCollectionResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();
                
                response.Collection = application.DataExplorersAvailable ();

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public Server.Core.DataExplorer.DataExplorer DataExplorerGet (String token, Int64 dataExplorerId) {

            Server.Core.DataExplorer.DataExplorer dataExplorer = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();


                dataExplorer = application.DataExplorerGet (dataExplorerId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return dataExplorer;

        }

        public ObjectSaveResponse DataExplorerSave (String token, Server.Core.DataExplorer.DataExplorer dataExplorer) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                // VALIDATE PERMISSIONS

                Boolean hasPermission = application.HasEnvironmentPermission (Server.EnvironmentPermissions.DataExplorerManage);

                if (hasPermission) {

                    dataExplorer.Application = application;

                    response.Success = dataExplorer.Save ();

                    response.Id = dataExplorer.Id;

                    if (!response.Success) { response.SetException (application.LastException); }

                }

                else { response.SetException (new ApplicationException ("Permission Denied.")); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public DataExplorerNodeExecutionResponse DataExplorerNodeExecute (String token, Server.Core.DataExplorer.DataExplorer dataExplorer, Guid nodeInstanceId) {

            DataExplorerNodeExecutionResponse response = new DataExplorerNodeExecutionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                // VALIDATE PERMISSIONS

                dataExplorer.Application = application;

                Server.Core.DataExplorer.DataExplorerNode dataExplorerNode = dataExplorer.FindNode (nodeInstanceId);


                // TODO: RETURN RESULTS

                response.RowCount = dataExplorerNode.Execute ();
                

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public Int64CollectionResponse DataExplorerNodeResultsGet (String token, Guid nodeInstanceId, Int32 initialRow, Int32 count) {

            Int64CollectionResponse response = new Int64CollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                response.Collection = application.DataExplorerNodeResultsGet (nodeInstanceId, initialRow, count);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public MemberCollectionResponse DataExplorerNodeResultsGetForMember (String token, Guid nodeInstanceId, Int32 initialRow, Int32 count) {

            MemberCollectionResponse response = new MemberCollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                response.Collection = application.DataExplorerNodeResultsGetForMember (nodeInstanceId, initialRow, count);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public EntityAddressCollectionResponse DataExplorerNodeResultsGetForMemberEntityCurrentAddress (String token, Guid nodeInstanceId, Int32 initialRow, Int32 count) {

            EntityAddressCollectionResponse response = new EntityAddressCollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                response.Collection = application.DataExplorerNodeResultsGetForMemberEntityCurrentAddress (nodeInstanceId, initialRow, count);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public EntityContactInformationCollectionResponse DataExplorerNodeResultsGetForMemberEntityCurrentContactInformation (String token, Guid nodeInstanceId, Int32 initialRow, Int32 count) {

            EntityContactInformationCollectionResponse response = new EntityContactInformationCollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                response.Collection = application.DataExplorerNodeResultsGetForMemberEntityCurrentContactInformation (nodeInstanceId, initialRow, count);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public MemberEnrollmentCollectionResponse DataExplorerNodeResultsGetForMemberCurrentEnrollment (String token, Guid nodeInstanceId, Int32 initialRow, Int32 count) {

            MemberEnrollmentCollectionResponse response = new MemberEnrollmentCollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                response.Collection = application.DataExplorerNodeResultsGetForMemberCurrentEnrollment (nodeInstanceId, initialRow, count);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public MemberEnrollmentCoverageCollectionResponse DataExplorerNodeResultsGetForMemberCurrentEnrollmentCoverage (String token, Guid nodeInstanceId, Int32 initialRow, Int32 count) {

            MemberEnrollmentCoverageCollectionResponse response = new MemberEnrollmentCoverageCollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                response.Collection = application.DataExplorerNodeResultsGetForMemberCurrentEnrollmentCoverage (nodeInstanceId, initialRow, count);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public MemberEnrollmentPcpCollectionResponse DataExplorerNodeResultsGetForMemberCurrentEnrollmentPcp (String token, Guid nodeInstanceId, Int32 initialRow, Int32 count) {

            MemberEnrollmentPcpCollectionResponse response = new MemberEnrollmentPcpCollectionResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                response.Collection = application.DataExplorerNodeResultsGetForMemberCurrentEnrollmentPcp (nodeInstanceId, initialRow, count);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion 


        #region Reporting - Reporting Server

        public ReportingServerCollectionResponse ReportingServersAvailable (String token) {

            ReportingServerCollectionResponse response = new ReportingServerCollectionResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Collection = application.ReportingServersAvailable ();

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }
        
        public DictionaryResponse ReportingServerDictionary (String token) {

            DictionaryResponse response = new DictionaryResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Dictionary = application.CoreObjectDictionary ("ReportingServer");

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }
        
        public Server.Reporting.ReportingServer ReportingServerGet (String token, Int64 reportingServerId) {

            Server.Reporting.ReportingServer reportingServer = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS NEEDED, OPEN TO ALL USERS

                reportingServer = application.ReportingServerGet (reportingServerId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return reportingServer;

        }
        
        public Server.Reporting.ReportingServer ReportingServerGetByName (String token, String reportingServerName) {

            Server.Reporting.ReportingServer reportingServer = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS NEEDED, OPEN TO ALL USERS

                reportingServer = application.ReportingServerGet (reportingServerName);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return reportingServer;

        }
        
        public ObjectSaveResponse ReportingServerSave (String token, Server.Reporting.ReportingServer reportingServer) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                // VALIDATE PERMISSIONS

                Boolean hasPermission = application.HasEnvironmentPermission (Server.EnvironmentPermissions.ReportingServerManage);

                if (hasPermission) {

                    response.Success = reportingServer.Save (application);

                    response.Id = reportingServer.Id;

                    if (!response.Success) { response.SetException (application.LastException); }

                }

                else { response.SetException (new ApplicationException ("Permission Denied.")); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion


        #region Faxing - Faxing Server

        public FaxServerCollectionResponse FaxServersAvailable (String token) {

            FaxServerCollectionResponse response = new FaxServerCollectionResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Collection = application.FaxServersAvailable ();

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public DictionaryResponse FaxServerDictionary (String token) {

            DictionaryResponse response = new DictionaryResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Dictionary = application.CoreObjectDictionary ("FaxServer");

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }


        public Server.Faxing.FaxServer FaxServerGet (String token, Int64 faxServerId) {

            Server.Faxing.FaxServer faxServer = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS NEEDED, OPEN TO ALL USERS

                faxServer = application.FaxServerGet (faxServerId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return faxServer;

        }

        public Server.Faxing.FaxServer FaxServerGetByName (String token, String faxServerName) {

            Server.Faxing.FaxServer faxServer = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS NEEDED, OPEN TO ALL USERS

                faxServer = application.FaxServerGet (faxServerName);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return faxServer;

        }

        public ObjectSaveResponse FaxServerSave (String token, Server.Faxing.FaxServer faxServer) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                // VALIDATE PERMISSIONS

                Boolean hasPermission = application.HasEnvironmentPermission (Server.EnvironmentPermissions.FaxServerManage);

                if (hasPermission) {

                    response.Success = faxServer.Save (application);

                    response.Id = faxServer.Id;

                    if (!response.Success) { response.SetException (application.LastException); }

                }

                else { response.SetException (new ApplicationException ("Permission Denied.")); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion


        #region Printing - Printing Server

        public PrinterCollectionResponse PrintersAvailable (String token) {

            PrinterCollectionResponse response = new PrinterCollectionResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Collection = application.PrintersAvailable ();

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public DictionaryResponse PrinterDictionary (String token) {

            DictionaryResponse response = new DictionaryResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Dictionary = application.CoreObjectDictionary ("Printer");

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        public DictionaryStringResponse PrintQueuesAvailable (String token, String printServerName) {

            DictionaryStringResponse response = new DictionaryStringResponse ();

            Server.Application application = null;

            try {


                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS REQUIRED, OPEN TO ALL USERS

                response.Dictionary = new Dictionary<String, String> (application.PrintQueuesAvailable (printServerName));

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }


        public Server.Printing.Printer PrinterGet (String token, Int64 printerId) {

            Server.Printing.Printer printer = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS NEEDED, OPEN TO ALL USERS

                printer = application.PrinterGet (printerId);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return printer;

        }

        public Server.Printing.Printer PrinterGetByName (String token, String printerName) {

            Server.Printing.Printer printer = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS NEEDED, OPEN TO ALL USERS

                printer = application.PrinterGet (printerName);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return printer;

        }

        public Server.Printing.PrinterCapabilities PrinterCapabilitiesGet (String token, String printServerName, String printQueueName) {

            Server.Printing.PrinterCapabilities capabilities = null;

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                application.ClearLastException ();

                // NO PERMISSIONS NEEDED, OPEN TO ALL USERS

                capabilities = application.PrinterCapabilities (printServerName, printQueueName);

            }

            catch (Exception applicationException) {

                if (application == null) { application = new Server.Application (); }

                application.SetLastException (applicationException);

            }

            return capabilities;

        }

        public ObjectSaveResponse PrinterSave (String token, Server.Printing.Printer printer) {

            ObjectSaveResponse response = new ObjectSaveResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                // VALIDATE PERMISSIONS

                Boolean hasPermission = application.HasEnvironmentPermission (Server.EnvironmentPermissions.PrinterManage);

                if (hasPermission) {

                    response.Success = printer.Save (application);

                    response.Id = printer.Id;

                    if (!response.Success) { response.SetException (application.LastException); }

                }

                else { response.SetException (new ApplicationException ("Permission Denied.")); }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion


        #region Search Operations

        public SearchResultsGlobalResponse SearchGlobal (String token, String criteria) {

            SearchResultsGlobalResponse response = new SearchResultsGlobalResponse ();


            SearchResultsMemberResponse memberResponse;

            SearchResultsProviderResponse providerResponse;


            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }


                memberResponse = SearchMember (token, criteria, null, criteria);

                if (memberResponse.HasException) { throw new ApplicationException (memberResponse.Exception.Message); }

                foreach (Server.Core.Search.SearchResultMember currentMemberResult in memberResponse.Results) {

                    response.Collection.Add (new Server.Core.Search.SearchResultGlobal (currentMemberResult));

                }


                providerResponse = SearchProvider (token, criteria, criteria);

                if (providerResponse.HasException) { throw new ApplicationException (providerResponse.Exception.Message); }

                foreach (Server.Core.Search.SearchResultProvider currentProviderResult in providerResponse.Results) {

                    response.Collection.Add (new Server.Core.Search.SearchResultGlobal (currentProviderResult));

                }


            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }


            return response;

        }

        public SearchResultsMemberResponse SearchMemberByName (String token, String memberName, DateTime? birthDate) {

            SearchResultsMemberResponse response = new SearchResultsMemberResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Results = application.SearchMemberByName (memberName, birthDate);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public SearchResultsMemberResponse SearchMemberById (String token, String memberId) {

            SearchResultsMemberResponse response = new SearchResultsMemberResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Results = application.SearchMemberById (memberId);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        public SearchResultsMemberResponse SearchMember (String token, String memberName, DateTime? birthDate, String memberId) {

            SearchResultsMemberResponse response = new SearchResultsMemberResponse ();

            if (!String.IsNullOrEmpty (memberId)) {

                SearchResultsMemberResponse idResponse = SearchMemberById (token, memberId);

                response.Results.AddRange (idResponse.Results);

                if (idResponse.HasException) {

                    response.SetException (idResponse.Exception);

                }

            }

            if (!String.IsNullOrEmpty (memberName)) {

                SearchResultsMemberResponse nameResponse = SearchMemberByName (token, memberName, birthDate);

                response.Results.AddRange (nameResponse.Results);

                if (nameResponse.HasException) {

                    response.HasException = true;

                    nameResponse.Exception.InnerException = response.Exception;

                    response.Exception = nameResponse.Exception;

                }

            }

            return response;

        }

        public SearchResultsProviderResponse SearchProvider (String token, String providerName, String providerId) {

            SearchResultsProviderResponse response = null;

            if (!String.IsNullOrEmpty (providerId)) {

                // DO NOTHING, TODO: ADD ID SEARCH

                response = SearchProviderByName (token, providerName);

            }

            else { response = SearchProviderByName (token, providerName); }

            return response;

        }

        public SearchResultsProviderResponse SearchProviderByName (String token, String providerName) {

            SearchResultsProviderResponse response = new SearchResultsProviderResponse ();

            Server.Application application = null;

            try {

                application = cacheManager.GetApplication (token);

                if (application == null) { throw cacheManager.LastException; }

                response.Results = application.SearchProviderByName (providerName);

            }

            catch (Exception applicationException) {

                response.SetException (applicationException);

            }

            return response;

        }

        #endregion

    }

}
