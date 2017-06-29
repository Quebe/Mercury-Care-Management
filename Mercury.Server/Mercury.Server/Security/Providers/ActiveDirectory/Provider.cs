using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mercury.Server.Public.Interfaces.Security;

namespace Mercury.Server.Security.Providers.ActiveDirectory {

    public class Provider : Mercury.Server.Public.Interfaces.Security.IProvider {
        
        #region Private Properties

        private Mercury.Server.Public.Interfaces.Security.Capabilities capabilities = new Capabilities ();

        Exception lastException;

        private Credentials credentials = new Credentials ();

        private String windowsDomain;

        private String windowsAccount;
        
        private System.Diagnostics.TraceSwitch traceSwitchSecurity = new System.Diagnostics.TraceSwitch ("Security", "Messages related to the Enterprise and Security");

        #endregion


        #region DllImport Declarations

        [System.Runtime.InteropServices.DllImport ("kernel32.dll", EntryPoint = "CloseHandle", SetLastError = true)]
        private static extern Boolean CloseHandle (IntPtr allocatedHandle);

        [System.Runtime.InteropServices.DllImport ("netapi32.dll", EntryPoint = "NetUserChangePassword", SetLastError = true)]
        private static extern Int32 NetUserChangePassword (
            [System.Runtime.InteropServices.MarshalAs (System.Runtime.InteropServices.UnmanagedType.LPWStr)] String domainName,
            [System.Runtime.InteropServices.MarshalAs (System.Runtime.InteropServices.UnmanagedType.LPWStr)] String userName,
            [System.Runtime.InteropServices.MarshalAs (System.Runtime.InteropServices.UnmanagedType.LPWStr)] String password,
            [System.Runtime.InteropServices.MarshalAs (System.Runtime.InteropServices.UnmanagedType.LPWStr)] String newPassword);
     
		[System.Runtime.InteropServices.DllImport ("advapi32.dll", EntryPoint = "LogonUser", SetLastError=true)]
		private static extern Boolean LogonUser (String lpszUsername, String lpszDomain, String lpszPassword, 
            ApiLogonUser.LogonType dwLogonType,
            ApiLogonUser.LogonProvider dwLogonProvider, 
            out IntPtr phToken);

        #endregion


        #region Constructors

        public Provider () {

            SetProviderCapabilities ();

        }

        public Provider (Public.Interfaces.Security.Credentials userCredentials) {

            SetProviderCapabilities ();

            this.credentials = userCredentials;

        }

        private void SetProviderCapabilities () {

            capabilities.CanBrowseDirectory = true;

            capabilities.CanManageUserAccounts = true;

            capabilities.CanGetSecurityGroups = true;

        }

        #endregion


        #region Interface Public Properties

        public Mercury.Server.Public.Interfaces.Security.Capabilities Capabilities {
            get { return capabilities; }

        }

        public Credentials Credentials {
            get { return credentials; }
            set { credentials = value; }

        }

        public Exception LastException {
            get { return lastException; }

        }

        #endregion


        #region Protected Properties

        protected String Protocol {

            get {

                String protocol;

                if (Credentials == null) {
                    return String.Empty;

                }

                if (credentials.Protocol != string.Empty) {
                    protocol = credentials.Protocol + "://";

                }

                else {
                    protocol = @"LDAP://";

                }

                if (credentials.ServerName != String.Empty) {
                    protocol = protocol + credentials.ServerName + @"/";

                }

                return protocol;

            }

        }

        protected String UserNameQualified {

            get {

                String userNameQualified;
                Int32  nameComponentsCount;

                nameComponentsCount = credentials.UserName.Split ('\\').Length;

                userNameQualified = @"CN=" + credentials.UserName.Split ('\\') [(nameComponentsCount - 1)];

                for (Int32 currentUnit = (nameComponentsCount - 2); currentUnit >= 0; currentUnit--) {

                    userNameQualified = userNameQualified + ", OU=" + credentials.UserName.Split ('\\') [currentUnit];

                }

                return userNameQualified;

            }

        }

        protected String DistinguishedName {

            get {

                String distinguishedName;

                if (Credentials == null) {
                    return String.Empty;

                }

                distinguishedName = UserNameQualified;

                if (credentials.Context != String.Empty) {
                    distinguishedName = distinguishedName + "," + credentials.Context;

                }

                if (credentials.Domain != String.Empty) {
                    distinguishedName = distinguishedName + ", " + credentials.Domain;

                }

                return distinguishedName;

            }

        }

        protected String DomainRootPath {

            get {

                String domainRootPath;

                if (Credentials == null) {
                    return String.Empty;

                }

                domainRootPath = Protocol;

                if (credentials.Domain != String.Empty) {
                    domainRootPath = domainRootPath + credentials.Domain;

                }

                else {
                    domainRootPath = domainRootPath + "RSDRoot";

                }

                return domainRootPath;

            }

        }

        protected String UserAccountPath {

            get {

                String userAccountPath;

                if (Credentials == null) {
                    return String.Empty;

                }

                userAccountPath = Protocol + DistinguishedName;

                return userAccountPath;

            }

        }

        #endregion


        #region Support Functions

        protected String ObjectSidToString (Byte[] sidByteArray) {

            if (sidByteArray == null) { return String.Empty; }

            System.Security.Principal.SecurityIdentifier sid = new System.Security.Principal.SecurityIdentifier (sidByteArray, 0);

            return sid.ToString ();

        }

        protected String DirectoryPath (String distinguishedName) {

            return Protocol + distinguishedName;

        }

        protected System.DirectoryServices.DirectoryEntry NewDirectoryEntryByAgent (String directoryPath) {

            if ((credentials != null) && (credentials.AgentName != String.Empty)) {
                return new System.DirectoryServices.DirectoryEntry (directoryPath, credentials.AgentName, credentials.AgentPassword, System.DirectoryServices.AuthenticationTypes.Secure);

            }

            else {
                return new System.DirectoryServices.DirectoryEntry (directoryPath);

            }

        }

        protected System.DirectoryServices.DirectoryEntry NewDirectoryEntryByWindowsUser (String directoryPath) {

            return new System.DirectoryServices.DirectoryEntry (directoryPath, windowsAccount, credentials.Password, System.DirectoryServices.AuthenticationTypes.Secure);

        }

        protected void EnumerateDirectoryEntryProperties (System.DirectoryServices.DirectoryEntry directoryEntry) {

            foreach (String currentPropertyName in directoryEntry.Properties.PropertyNames) {

                System.Diagnostics.Debug.Write (currentPropertyName + ": ");

                System.Diagnostics.Debug.WriteLine (directoryEntry.Properties[currentPropertyName].Value.ToString ());

            }

        }

        protected Boolean GetWindowsAccountInfo () {

            System.DirectoryServices.DirectoryEntry directoryEntry = null;
            Boolean success = false;

            String securityAuthorityError = "A system error occurred while authenticating against the Security Authority [Retreiving Account Information]. Please contact your System Administrator. (Active Directory.Provider)";

            try {

                // Get Domain Root Path information to get the Windows Domain Name

                directoryEntry = NewDirectoryEntryByAgent (DomainRootPath);

                windowsDomain = directoryEntry.Properties ["Name"].Value.ToString ();

                directoryEntry.Close ();

                // Get the Windows Account Information

                directoryEntry = NewDirectoryEntryByAgent (UserAccountPath);

                windowsAccount = directoryEntry.Properties ["SamAccountName"].Value.ToString ();

                credentials.UserAccountId = ObjectSidToString (((byte[]) directoryEntry.Properties["objectSid"].Value));

                credentials.UserAccountName = windowsAccount;

                if (!String.IsNullOrEmpty (((String) directoryEntry.Properties["displayName"].Value))) {

                    credentials.UserDisplayName = directoryEntry.Properties["displayName"].Value.ToString ();

                }

                else {

                    credentials.UserDisplayName = credentials.UserName;

                }

                directoryEntry.Close ();

                success = true;

            }

            catch (System.Reflection.TargetInvocationException AuthenticationException) {

                credentials.IsAuthenticated = false;

                credentials.AuthenticationError = Mercury.Server.Public.Interfaces.Security.Enumerations.AuthenticationError.SecurityAuthorityError;

                if (AuthenticationException.InnerException != null) {

                    if (credentials.AuthenticationException == null) {
                        credentials.AuthenticationException = AuthenticationException.InnerException;

                    }

                    else {
                        credentials.AuthenticationException = new ApplicationException (securityAuthorityError + " (" + AuthenticationException.Message + ")", AuthenticationException.InnerException);

                    }

                }

                else {

                    if (credentials.AuthenticationException == null) {
                        credentials.AuthenticationException = AuthenticationException;

                    }

                    else {
                        credentials.AuthenticationException = new ApplicationException (securityAuthorityError + " (" + AuthenticationException.Message + ")", AuthenticationException);

                    }

                }

                SetLastException (credentials.AuthenticationException);

            } // catch (System.Reflection.TargetInvocationException AuthenticationException) 

            catch (Exception AuthenticationException) {

                credentials.IsAuthenticated = false;

                if (AuthenticationException.Message == "There is no such object on the server.\r\n") {
                    credentials.AuthenticationError = Mercury.Server.Public.Interfaces.Security.Enumerations.AuthenticationError.InvalidUserOrPassword;

                }

                else {
                    credentials.AuthenticationError = Mercury.Server.Public.Interfaces.Security.Enumerations.AuthenticationError.SecurityAuthorityError;

                }

                if (credentials.AuthenticationException == null) {
                    credentials.AuthenticationException = AuthenticationException;

                }

                else {
                    credentials.AuthenticationException = new ApplicationException (credentials.AuthenticationException.Message, AuthenticationException);

                }

                SetLastException (credentials.AuthenticationException);

            } // catch (Exception AuthenticationException) 

            finally {

                if (directoryEntry != null) {

                    directoryEntry.Close ();

                }

            }

            return success;

        }

        protected Boolean GetUserGroupMembership () {

            System.DirectoryServices.DirectoryEntry userDirectoryEntry = null;
            System.DirectoryServices.DirectoryEntry groupDirectoryEntry = null;

            String  groupSid;

            Boolean success = false;

            String securityAuthorityError = "A system error occurred while authenticating against the Security Authority [Retrieving Group Membership]. Please contact your System Administrator.";

            try {

                // Get User Account Entry to determine Group Membership

                userDirectoryEntry = NewDirectoryEntryByAgent (UserAccountPath);

                // For each Group Membership
                foreach (String currentGroup in userDirectoryEntry.Properties ["memberOf"]) {

                    System.Diagnostics.Debug.WriteLine (currentGroup);

                    groupDirectoryEntry = NewDirectoryEntryByAgent (DirectoryPath (currentGroup));

                    groupSid = ObjectSidToString ((Byte []) groupDirectoryEntry.Properties ["objectSid"].Value);

                    if (!credentials.Groups.Contains (groupSid)) {
                        credentials.Groups.Add (groupSid);

                    }

                    System.Diagnostics.Debug.WriteLine (groupSid);

                } // foreach (String currentGroup in userDirectoryEntry.Properties ["memberOf"]) 

                success = true;

            }

            catch (Exception AuthenticationException) {

                success = false;

                credentials.IsAuthenticated = false;

                credentials.AuthenticationError = Mercury.Server.Public.Interfaces.Security.Enumerations.AuthenticationError.SecurityAuthorityError;

                if (credentials.AuthenticationException == null) {
                    credentials.AuthenticationException = AuthenticationException;

                }

                else {
                    credentials.AuthenticationException = new ApplicationException (securityAuthorityError + " (" + AuthenticationException.Message + ") " , AuthenticationException);

                }

                SetLastException (credentials.AuthenticationException);

            } // catch (Exception AuthenticationException) 

            finally {

                if (userDirectoryEntry != null) {
                    userDirectoryEntry.Close ();

                }

                if (groupDirectoryEntry != null) {
                    groupDirectoryEntry.Close ();

                }

            }

            return success;

        }

        protected void SetLastException (Exception exception) {

            lastException = exception;

            if (lastException != null) {

                System.Diagnostics.Trace.WriteLine ("/r/n[Mercury.Server.Security.AuthorityProvider.ActiveDirectory.Provider] Exception Occurred");

                System.Diagnostics.Trace.WriteLine ("[" + exception.Source + "] " + exception.Message);

                if (lastException.InnerException != null) {

                    System.Diagnostics.Trace.WriteLine ("[" + exception.InnerException.Source + "] " + exception.InnerException.Message);

                }

                System.Diagnostics.Trace.Flush ();

            } // if (lastException != null) 

        }

        #endregion


        #region Authentication Methods

        public Boolean Authenticate (Credentials userCredentials) {

            credentials = userCredentials;

            return Authenticate ();

        }

        public Boolean Authenticate () {

            // Windows NT, Windows 2000, Windows XP:  WinNT://path
            // Lightweight Directory Acess Protocol (LDAP): LDAP://path

            Int32 passwordChangeResult = 0;

            Int32 logonResult = 0;

            Boolean apiFunctionSuccess = false;

            IntPtr token;

            DateTime authenticationStateTime = DateTime.Now;


            System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceInfo, "\r\n[Mercury.Server.Security.AuthorityProvider.ActiveDirectory.Provider] Current Credentials");

            System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceInfo, "Server Thread Principal: " + System.Threading.Thread.CurrentPrincipal.Identity.Name);

            System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceInfo, "Server System Principal: " + System.Security.Principal.WindowsIdentity.GetCurrent ().Name);

            System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceInfo, "Server Thread Authentication Type: " + System.Threading.Thread.CurrentPrincipal.Identity.AuthenticationType);


            if (!GetWindowsAccountInfo ()) {

                credentials.IsAuthenticated = false;

                return false;

            }

/*                
            else {

                // DEBUG ONLY, ON FIND FORCE AUTO LOGIN WITHOUT PASSWORD CHECK

                credentials.IsAuthenticated = GetGroupMembership ();

                return credentials.IsAuthenticated;

            }
*/

            // attempt password change, if requested
            if ((credentials.NewPassword != String.Empty) && (credentials.Password != credentials.NewPassword)) {

                passwordChangeResult = NetUserChangePassword (windowsDomain, windowsAccount, credentials.Password, credentials.NewPassword);

                switch (passwordChangeResult) { 

                    case (Int32) ApiNetUserChangePassword.ResultCode.Success: 

                        credentials.Password = credentials.NewPassword;
                        credentials.NewPassword = "";

                        break;

                    case (Int32)ApiNetUserChangePassword.ResultCode.AccountLockedOut:

                        credentials.AuthenticationError = Mercury.Server.Public.Interfaces.Security.Enumerations.AuthenticationError.AccountLockedDisabledExpired;

                        break;

                    case (Int32)ApiNetUserChangePassword.ResultCode.InvalidPassword:

                        credentials.AuthenticationError = Mercury.Server.Public.Interfaces.Security.Enumerations.AuthenticationError.InvalidUserOrPassword;

                        break;

                    case (Int32)ApiNetUserChangePassword.ResultCode.PasswordRestriction:

                        credentials.AuthenticationError = Mercury.Server.Public.Interfaces.Security.Enumerations.AuthenticationError.PasswordRestriction;

                        break;

                    case (Int32)ApiNetUserChangePassword.ResultCode.UserNotFound:

                        credentials.AuthenticationError = Mercury.Server.Public.Interfaces.Security.Enumerations.AuthenticationError.InvalidUserOrPassword;

                        break;

                    default:

                        credentials.AuthenticationError = Mercury.Server.Public.Interfaces.Security.Enumerations.AuthenticationError.OtherUndefinedError;

                        credentials.AuthenticationException = new ApplicationException ("Unable to successfully change password (" + (new System.ComponentModel.Win32Exception (passwordChangeResult)).Message + ").");

                        credentials.IsAuthenticated = false;
                            
                        break;

                } // switch (passwordChangeResult) 

            }

            if (passwordChangeResult == 0) {  // SUCCESS OR NO CHANGE REQUESTED

                credentials.IsAuthenticated = LogonUser (windowsAccount, windowsDomain, credentials.Password, ApiLogonUser.LogonType.Network, ApiLogonUser.LogonProvider.Default, out token);

                if ((credentials.IsAuthenticated == false) || (logonResult != 0)) {

                    logonResult = System.Runtime.InteropServices.Marshal.GetLastWin32Error ();

                    switch (logonResult) {

                        case (Int32) Mercury.Server.Public.Interfaces.Security.Enumerations.ApiWin32Error.UnknownNameOrBadPassword:

                            credentials.AuthenticationError = Mercury.Server.Public.Interfaces.Security.Enumerations.AuthenticationError.InvalidUserOrPassword;

                            credentials.AuthenticationException = new System.ComponentModel.Win32Exception (logonResult);

                            break;

                        case (Int32) Mercury.Server.Public.Interfaces.Security.Enumerations.ApiWin32Error.AccountDisabled:
                        case (Int32) Mercury.Server.Public.Interfaces.Security.Enumerations.ApiWin32Error.AccountExpired:
                        case (Int32) Mercury.Server.Public.Interfaces.Security.Enumerations.ApiWin32Error.AccountLockedOut:

                            credentials.AuthenticationError = Mercury.Server.Public.Interfaces.Security.Enumerations.AuthenticationError.AccountLockedDisabledExpired;

                            credentials.AuthenticationException = new System.ComponentModel.Win32Exception (logonResult);

                            break;

                        case (Int32) Mercury.Server.Public.Interfaces.Security.Enumerations.ApiWin32Error.InvalidLogOnHours:

                            credentials.AuthenticationError = Mercury.Server.Public.Interfaces.Security.Enumerations.AuthenticationError.OtherUndefinedError;

                            credentials.AuthenticationException = new System.ComponentModel.Win32Exception (logonResult);

                            break;


                        case (Int32) Mercury.Server.Public.Interfaces.Security.Enumerations.ApiWin32Error.InvalidWorkStation:

                            credentials.AuthenticationError = Mercury.Server.Public.Interfaces.Security.Enumerations.AuthenticationError.OtherUndefinedError;

                            credentials.AuthenticationException = new System.ComponentModel.Win32Exception (logonResult);

                            break;

                        case (Int32) Mercury.Server.Public.Interfaces.Security.Enumerations.ApiWin32Error.MustChangePassword:

                            credentials.AuthenticationError = Mercury.Server.Public.Interfaces.Security.Enumerations.AuthenticationError.MustChangePassword;

                            credentials.AuthenticationException = new System.ComponentModel.Win32Exception (logonResult);

                            break;

                        case (Int32) Mercury.Server.Public.Interfaces.Security.Enumerations.ApiWin32Error.PasswordExpired:

                            credentials.AuthenticationError = Mercury.Server.Public.Interfaces.Security.Enumerations.AuthenticationError.MustChangePassword;

                            credentials.AuthenticationException = new System.ComponentModel.Win32Exception (logonResult);

                            break;

                        case (Int32) Mercury.Server.Public.Interfaces.Security.Enumerations.ApiWin32Error.AccountRestriction:

                            credentials.AuthenticationError = Mercury.Server.Public.Interfaces.Security.Enumerations.AuthenticationError.OtherUndefinedError;

                            credentials.AuthenticationException = new System.ComponentModel.Win32Exception (logonResult);

                            break;

                        default:

                            credentials.AuthenticationError = Mercury.Server.Public.Interfaces.Security.Enumerations.AuthenticationError.OtherUndefinedError;

                            credentials.AuthenticationException = new ApplicationException ("Unable to successfully authenticate (" + (new System.ComponentModel.Win32Exception (logonResult)).Message + ").");

                            break;

                    } // switch (System.Runtime.InteropServices.Marshal.GetLastWin32Error ()) {

                } // if (credentials.IsAuthenticated == false) {

                else {

                    apiFunctionSuccess = CloseHandle (token);

                }

            } // if (passwordChangeResult == 0) {  // SUCCESS OR NO CHANGE REQUESTED


            if (credentials.IsAuthenticated) {

                credentials.IsAuthenticated = GetUserGroupMembership ();

                credentials.AuthenticationTime = Convert.ToInt32 (DateTime.Now.Subtract (authenticationStateTime).TotalMilliseconds);

            }

            SetLastException (credentials.AuthenticationException);

            return credentials.IsAuthenticated;

        }

        #endregion


        #region Provider Methods

        public SecurityGroup GetSecurityGroup (String securityGroupId) {

            System.DirectoryServices.DirectoryEntry directoryEntry;

            SecurityGroup securityGroup = new SecurityGroup ();

            String objectSid;

            directoryEntry = NewDirectoryEntryByAgent (DomainRootPath).Children.Find ("CN=USERS");

            foreach (System.DirectoryServices.DirectoryEntry currentEntry in directoryEntry.Children) {

                try {

                    if (currentEntry.Properties["groupType"].Value != null) {

                        objectSid = ObjectSidToString (((byte[]) currentEntry.Properties["objectSid"].Value));

                        if (securityGroupId == objectSid) {

                            securityGroup.SecurityAuthorityId = credentials.SecurityAuthorityId;

                            securityGroup.SecurityAuthorityName = credentials.SecurityAuthorityName;

                            securityGroup.SecurityGroupId = securityGroupId;

                            securityGroup.SecurityGroupName = currentEntry.Properties["name"].Value.ToString ();

                            if (!String.IsNullOrEmpty ((String) currentEntry.Properties["Description"].Value)) {

                                securityGroup.Description = currentEntry.Properties["Description"].Value.ToString ();

                            }

                            break;

                        }


                    } // if (currentPropertyName == "groupType") {

                } // END TRY ENUMERATE DIRECTORY ENTRY PROPERTIES

                catch (Exception propertyException) {

                    // DO NOTHING

                    System.Diagnostics.Debug.WriteLine (propertyException.Message);

                }

            }

            return securityGroup;

        }

        public Dictionary<String, String> GetSecurityGroupDictionary () {

            Dictionary<String, String> groupDictionary = new Dictionary<String, String> ();

            System.DirectoryServices.DirectoryEntry directoryEntry;

            String objectSid;

            Int32 groupType;


            try {

                directoryEntry = NewDirectoryEntryByAgent (DomainRootPath).Children.Find ("CN=USERS");

                foreach (System.DirectoryServices.DirectoryEntry currentEntry in directoryEntry.Children) {

                    //                EnumerateDirectoryEntryProperties (currentEntry);

                    try {

                        if (currentEntry.Properties["groupType"].Value != null) {

                            groupType = (Int32) currentEntry.Properties["groupType"].Value;

                            if (groupType == -2147483646) { //2147483644 (LOCAL ONLY), -2147483646 (GLOBAL)

                                objectSid = ObjectSidToString (((byte[]) currentEntry.Properties["objectSid"].Value));

                                if (!groupDictionary.ContainsKey (objectSid)) {

                                    groupDictionary.Add (objectSid, currentEntry.Properties["name"].Value.ToString ());

                                } // if (!groupDictionary.ContainsKey (objectSid) {

                            }

                        } // if (currentPropertyName == "groupType") {

                    } // END TRY ENUMERATE DIRECTORY ENTRY PROPERTIES

                    catch (Exception propertyException) {

                        // DO NOTHING

                        System.Diagnostics.Debug.WriteLine (propertyException.Message);

                    }

                }

            }

            catch (Exception directoryException) {

                String exceptionMessage = "Mercury.Server.Security.AuthorityProvider.ActiveDirectory.Provider [" + DomainRootPath + "]: " + directoryException.Message;

                System.Diagnostics.Debug.WriteLine (exceptionMessage);

                throw new ApplicationException (exceptionMessage);

            }

            return groupDictionary;

        }

        public List<DirectoryEntry> BrowseDirectory (String directoryPath) {

            List<DirectoryEntry> directoryEntries = new List<DirectoryEntry> ();


            System.DirectoryServices.DirectoryEntry directory = null;

            System.DirectoryServices.DirectorySearcher directorySearcher;

            String pathSuffix = string.Empty;


            try {

                // Get Domain Root Path Information

                if (directoryPath.Split ('/')[0] == "{UserRoot}") {

                    directorySearcher = new System.DirectoryServices.DirectorySearcher ();

                    directorySearcher.SearchRoot = new System.DirectoryServices.DirectoryEntry (DomainRootPath);

                    directorySearcher.Filter = "(&(objectClass=user) (objectSid=" + credentials.UserAccountId + "))";

                    System.DirectoryServices.SearchResult directorySearchResult = directorySearcher.FindOne ();

                    if (directorySearchResult != null) {

                        // incoming path would be like OU/OU/OU or {UserRoot}/OU/OU, Result Path will be LDAP:\\SERVER\CN=User Account, OU, OU, OU

                        pathSuffix = directorySearchResult.Path.Substring (directorySearchResult.Path.IndexOf (@"/CN="));

                        pathSuffix = pathSuffix.Substring (pathSuffix.IndexOf (',') + 1);

                        System.Diagnostics.Debug.WriteLine (pathSuffix);

                    }

                }

                else {

                    pathSuffix = DomainRootPath;

                    pathSuffix = pathSuffix.Substring (pathSuffix.IndexOf (@"/DC=") + 1);

                    System.Diagnostics.Debug.WriteLine (pathSuffix);

                }


                foreach (String pathComponent in directoryPath.Split ('/')) {

                    if (pathComponent != "{UserRoot}") {

                        pathSuffix = "OU=" + pathComponent + "," + pathSuffix;

                        System.Diagnostics.Debug.WriteLine (pathComponent);

                    }

                }


                directory = new System.DirectoryServices.DirectoryEntry (Protocol + pathSuffix);

                foreach (System.DirectoryServices.DirectoryEntry currentDirectoryEntry in directory.Children) {

                    System.Diagnostics.Debug.WriteLine (currentDirectoryEntry.Name);


                    try {

                        DirectoryEntry directoryEntry = new DirectoryEntry ();

                        switch (currentDirectoryEntry.SchemaClassName.ToLowerInvariant ()) {

                            case "user":

                                directoryEntry.ObjectType = "User";

                                directoryEntry.ObjectSid = ObjectSidToString ((byte[]) currentDirectoryEntry.Properties["objectSid"].Value);

                                directoryEntry.Name = currentDirectoryEntry.Properties["Name"].Value.ToString ();


                                if (!String.IsNullOrEmpty ((String) currentDirectoryEntry.Properties ["DisplayName"].Value)) {
                                    
                                    directoryEntry.DisplayName = currentDirectoryEntry.Properties["DisplayName"].Value.ToString ();

                                }

                                else {

                                    directoryEntry.DisplayName = currentDirectoryEntry.Properties["Name"].Value.ToString ();

                                }


                                directoryEntry.DistinguishedName = currentDirectoryEntry.Properties["distinguishedName"].Value.ToString ();

                                directoryEntry.Path = currentDirectoryEntry.Properties["distinguishedName"].Value.ToString ();

                                directoryEntries.Add (directoryEntry);

                                break;

                            case "group":

                                directoryEntry.ObjectType = "Group";

                                directoryEntry.ObjectSid = ObjectSidToString ((byte[]) currentDirectoryEntry.Properties["objectSid"].Value);

                                directoryEntry.Name = currentDirectoryEntry.Properties["Name"].Value.ToString ();

                                directoryEntry.DisplayName = currentDirectoryEntry.Properties["Name"].Value.ToString ();

                                directoryEntry.DistinguishedName = currentDirectoryEntry.Properties["distinguishedName"].Value.ToString ();

                                directoryEntries.Add (directoryEntry);

                                break;

                            case "organizationalunit":

                                directoryEntry.ObjectType = "Organizational Unit";

                                directoryEntry.ObjectSid = ObjectSidToString ((byte[]) currentDirectoryEntry.Properties["objectSid"].Value);

                                directoryEntry.Name = currentDirectoryEntry.Properties["Name"].Value.ToString ();

                                directoryEntry.DisplayName = currentDirectoryEntry.Properties["Name"].Value.ToString ();

                                directoryEntry.DistinguishedName = currentDirectoryEntry.Properties["distinguishedName"].Value.ToString ();

                                directoryEntries.Add (directoryEntry);

                                break;

                            default:

                                System.Diagnostics.Debug.WriteLine (currentDirectoryEntry.SchemaClassName);

                                break;

                        }


                    } // END TRY ENUMERATE DIRECTORY ENTRY PROPERTIES

                    catch (Exception propertyException) {

                        // DO NOTHING

                        System.Diagnostics.Debug.WriteLine (propertyException.Message);

                    }

                }

            }

            catch (Exception browseException) {

                System.Diagnostics.Debug.WriteLine (browseException.Message);

                // DO NOTHING

            }


            return directoryEntries;


        }

        public List<DirectoryEntry> GetSecurityGroupMembership (String securityGroupId) {

            List<DirectoryEntry> membership = new List<DirectoryEntry> ();


            System.DirectoryServices.DirectoryEntry rootDirectoryEntry;

            SecurityGroup securityGroup = new SecurityGroup ();

            String objectSid;

            rootDirectoryEntry = NewDirectoryEntryByAgent (DomainRootPath).Children.Find ("CN=USERS");

            foreach (System.DirectoryServices.DirectoryEntry currentEntry in rootDirectoryEntry.Children) {

                try {

                    if (currentEntry.Properties["groupType"].Value != null) {

                        objectSid = ObjectSidToString (((byte[]) currentEntry.Properties["objectSid"].Value));

                        if (securityGroupId == objectSid) {

                            Object groupMembers = currentEntry.Invoke ("members", null);

                            foreach (Object currentMember in (System.Collections.IEnumerable) groupMembers) {

                                membership.Add (new DirectoryEntry (new System.DirectoryServices.DirectoryEntry (currentMember)));

                            }

                        }


                    } // if (currentPropertyName == "groupType") {

                } // END TRY ENUMERATE DIRECTORY ENTRY PROPERTIES

                catch (Exception propertyException) {

                    // DO NOTHING

                    System.Diagnostics.Debug.WriteLine (propertyException.Message);

                }

            }

            return membership;

        }

        #endregion 

    }

}
