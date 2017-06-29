using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mercury.Server.Public.Interfaces.Security;

namespace Mercury.Server.Security.Providers.WindowsIntegrated {

    public class Provider : Mercury.Server.Public.Interfaces.Security.IProvider {
        
        #region Private Properties

        private Mercury.Server.Public.Interfaces.Security.Capabilities capabilities = new Capabilities ();


        private Credentials credentials = new Credentials ();

        private System.Security.Principal.WindowsIdentity threadIdentity;


        private System.Diagnostics.TraceSwitch traceSwitchSecurity = new System.Diagnostics.TraceSwitch ("Security", "Messages related to the Enterprise and Security");

        private Exception lastException;

        #endregion


        #region Constructors

        public Provider () {

            SetProviderCapabilities ();

        }

        public Provider (Public.Interfaces.Security.Credentials userCredentials) {

            SetProviderCapabilities ();

            credentials = userCredentials;

        }

        private void SetProviderCapabilities () {

            capabilities.CanBrowseDirectory = true;

            capabilities.CanManageUserAccounts = false;

            capabilities.CanGetSecurityGroups = true;

        }

        #endregion


        #region Interface Public Properties

        public Mercury.Server.Public.Interfaces.Security.Capabilities Capabilities { get { return capabilities; } }

        public Credentials Credentials { get { return credentials; } set { credentials = value; } }

        public Exception LastException { get { return lastException; } }

        #endregion


        #region Support Functions

        protected void SetLastException (Exception exception) {

            lastException = exception;

            if (lastException != null) {

                System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceError, "/r/n[Mercury.Server.Security.WindowsIntegrated.Provider] Exception Occurred");

                System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceError, "[" + exception.Source + "] " + exception.Message);

                if (lastException.InnerException != null) {

                    System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceError, "[" + exception.InnerException.Source + "] " + exception.InnerException.Message);

                }

                System.Diagnostics.Trace.Flush ();

            } // if (lastException != null) 

        }

        protected String ObjectSidToString (Byte [] sidByteArray) {

            if (sidByteArray == null) { return String.Empty; }

            System.Security.Principal.SecurityIdentifier sid = new System.Security.Principal.SecurityIdentifier (sidByteArray, 0);

            return sid.ToString ();

        }

        protected void EnumerateDirectoryEntryProperties (System.DirectoryServices.DirectoryEntry directoryEntry) {

            foreach (String currentPropertyName in directoryEntry.Properties.PropertyNames) {

                System.Diagnostics.Trace.WriteIf (traceSwitchSecurity.TraceVerbose, currentPropertyName + ": ");

                System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceVerbose, directoryEntry.Properties[currentPropertyName].Value.ToString ());

            }

        }

        #endregion


        #region Authentication Methods

        public Boolean Authenticate () {

            System.Security.Principal.NTAccount userAccount;

            System.Security.Principal.NTAccount groupAccount;

            DateTime authenticationStartTime = DateTime.Now;

            if (((System.Threading.Thread.CurrentPrincipal.Identity.AuthenticationType == "NTLM")
                    || (System.Threading.Thread.CurrentPrincipal.Identity.AuthenticationType == "Kerberos")
                    || (System.Threading.Thread.CurrentPrincipal.Identity.AuthenticationType == "Negotiate"))
                && (System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated)
                && (!String.IsNullOrEmpty (System.Threading.Thread.CurrentPrincipal.Identity.Name))) {

                // When using Windows Integrated (Negotiated) Authentication, the calling identity
                //   is in the Thread.CurrentPrinicpal while the ASP.NET host identity is in the System.Security.Principal

                System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceVerbose, "\r\n[Mercury.Server.Security.WindowsIntegrated.Provider] Current Credentials");

                System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceVerbose, "Server Thread Principal: " + System.Threading.Thread.CurrentPrincipal.Identity.Name);

                System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceVerbose, "Server System Principal: " + System.Security.Principal.WindowsIdentity.GetCurrent ().Name);

                System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceVerbose, "Server Thread Authentication Type: " + System.Threading.Thread.CurrentPrincipal.Identity.AuthenticationType);             


                threadIdentity = (System.Security.Principal.WindowsIdentity) System.Threading.Thread.CurrentPrincipal.Identity;

                userAccount = new System.Security.Principal.NTAccount (threadIdentity.Name);

                credentials.UserAccountId = threadIdentity.User.Value;


                if (userAccount.Value.Contains (@"\")) {

                    credentials.UserAccountName = userAccount.Value.Split ('\\')[1];
                    
                }

                else {

                    credentials.UserAccountName = userAccount.Value;

                }

                credentials.UserDisplayName = GetUserDisplayName (credentials.UserAccountName);

                if (credentials.UserDisplayName == String.Empty) { credentials.UserDisplayName = credentials.UserAccountName; }


                System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceVerbose, "\r\n[Mercury.Server.Security.WindowsIntegrated.Provider] Thread Group Membership");

                foreach (System.Security.Principal.IdentityReference currentGroup in threadIdentity.Groups) {

                    groupAccount = (System.Security.Principal.NTAccount) currentGroup.Translate (typeof (System.Security.Principal.NTAccount));

                    if (!credentials.Groups.Contains (currentGroup.Value)) {

                        credentials.Groups.Add (currentGroup.Value);

                    }

                    System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceVerbose, "[" + currentGroup.ToString () + "] " + groupAccount.Value);

                }

                credentials.AuthenticationTime = Convert.ToInt32 (DateTime.Now.Subtract (authenticationStartTime).TotalMilliseconds);

                if (DateTime.Now.Subtract (authenticationStartTime).Seconds > 1) {

                    System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceWarning, "Windows Integrated Authentication Time [Warning]: " + credentials.AuthenticationTime.ToString ());

                }

                else {

                    System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceVerbose, "Windows Integrated Authentication Time: " + credentials.AuthenticationTime.ToString ());

                }


                return true;

            }

            else { // non-Windows account authentication in Thread.CurrentPrincipal

                return false;

            } 

        }

        public Boolean Authenticate (Credentials userCredentials) {

            credentials = userCredentials;

            return Authenticate ();

        }

        #endregion


        #region Provider Methods

        public SecurityGroup GetSecurityGroup (String securityGroupId) {

            System.DirectoryServices.DirectoryEntry rootDirectoryEntry;

            Boolean foundRootDirectoryEntry = false;

            SecurityGroup securityGroup = new SecurityGroup ();

            String objectSid;


            try { // TO CONNECT TO DIRECTORY SERVICE BY DOMAIN NAME ONLY 

                rootDirectoryEntry = new System.DirectoryServices.DirectoryEntry ("WinNT://" + credentials.Domain);

                foundRootDirectoryEntry = true;

            }

            catch (Exception directoryExceptionDomainOnly) {

                System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceError, directoryExceptionDomainOnly);

                try { // TO CONNECT TO DIRECTORY SERVICE BY DOMAIN NAME AND SERVER NAME

                    rootDirectoryEntry = new System.DirectoryServices.DirectoryEntry ("WinNT://" + credentials.Domain + "/" + credentials.ServerName);

                    foundRootDirectoryEntry = true;

                }

                catch (Exception directoryException) {

                    System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceError, directoryException);

                    throw new ApplicationException ("Unable to retreive group list for this Domain (" + credentials.Domain + ").");

                }

            } // END TRY: CONNECT TO DIRECTORY SERVICES 



            if (foundRootDirectoryEntry) {

                foreach (System.DirectoryServices.DirectoryEntry currentEntry in rootDirectoryEntry.Children) {

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

                        System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceError, propertyException.Message);

                    }

                }

            }

            return securityGroup;

        }

        public Dictionary<String, String> GetSecurityGroupDictionary () {

            Dictionary<String, String> groupDictionary = new Dictionary<String, String> ();

            System.DirectoryServices.DirectoryEntry directory = new System.DirectoryServices.DirectoryEntry ();

            Boolean foundDirectoryEntry = false;

            Int32 groupType;

            String objectSid;


            try { // TO CONNECT TO DIRECTORY SERVICE BY DOMAIN NAME ONLY 

                directory = new System.DirectoryServices.DirectoryEntry ("WinNT://" + credentials.Domain);

                foundDirectoryEntry = true;

            }

            catch (Exception directoryExceptionDomainOnly) {

                System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceError, directoryExceptionDomainOnly);

                try { // TO CONNECT TO DIRECTORY SERVICE BY DOMAIN NAME AND SERVER NAME

                    directory = new System.DirectoryServices.DirectoryEntry ("WinNT://" + credentials.Domain + "/" + credentials.ServerName);

                    foundDirectoryEntry = true;

                }

                catch (Exception directoryException) {

                    System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceError, directoryException);

                    groupDictionary.Add ("0", "Unable to retreive group list for this Domain (" + credentials.Domain + ").");

                }

            } // END TRY: CONNECT TO DIRECTORY SERVICES 


            if (foundDirectoryEntry) {

                foreach (System.DirectoryServices.DirectoryEntry currentEntry in directory.Children) {

                    try {

                        Boolean isGroup = false;

                        if (currentEntry.SchemaClassName != "Schema") {

                            if (currentEntry.Properties.Contains ("groupType")) {

                                if (currentEntry.Properties["groupType"].Value != null) { isGroup = true; }

                                if (isGroup) {

                                    groupType = (Int32)currentEntry.Properties["groupType"].Value;

                                    if ((groupType == 2) || (groupType == 4)) {

                                        objectSid = ObjectSidToString (((byte[])currentEntry.Properties["objectSid"].Value));

                                        if (!groupDictionary.ContainsKey (objectSid)) {

                                            String groupName = String.Empty;

                                            if (currentEntry.Properties["name"].Value != null) { groupName = currentEntry.Properties["name"].Value.ToString (); }

                                            groupDictionary.Add (objectSid, groupName);

                                        } // if (!groupDictionary.ContainsKey (objectSid) {

                                    }

                                } // if (currentPropertyName == "groupType") {


                            }

                        }

                    } // END TRY ENUMERATE DIRECTORY ENTRY PROPERTIES

                    catch (Exception propertyException) {

                        // DO NOTHING

                        System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceError, propertyException.Message);

                    }

                }

            } // if (foundDirectoryEntry)


            return groupDictionary;

        }

        protected String GetUserDisplayName (String userAccountName) {

            System.DirectoryServices.DirectoryEntry directory = new System.DirectoryServices.DirectoryEntry ();

            Boolean foundDirectoryEntry = false;

            String userDisplayName = String.Empty;


            try { // TO CONNECT TO DIRECTORY SERVICE BY DOMAIN NAME ONLY 

                directory = new System.DirectoryServices.DirectoryEntry ("WinNT://" + credentials.Domain);

                foundDirectoryEntry = true;

            }

            catch (Exception directoryExceptionDomainOnly) {

                System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceError, directoryExceptionDomainOnly);

                try { // TO CONNECT TO DIRECTORY SERVICE BY DOMAIN NAME AND SERVER NAME

                    directory = new System.DirectoryServices.DirectoryEntry ("WinNT://" + credentials.Domain + "/" + credentials.ServerName);

                    foundDirectoryEntry = true;

                }

                catch (Exception directoryException) {

                    System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceError, directoryException);

                    // groupDictionary.Add ("0", "Unable to retreive group list for this Domain (" + credentials.Domain + ").");

                }

            } // END TRY: CONNECT TO DIRECTORY SERVICES 


            if (foundDirectoryEntry) {

                try {

                    directory = new System.DirectoryServices.DirectoryEntry (directory.Path + "/" + userAccountName);

                    if (directory != null) {

                        DirectoryEntry directoryAccount = new DirectoryEntry (directory);

                        userDisplayName = directoryAccount.DisplayName;

                    }

                }

                catch (Exception directoryException) {

                    /* Unsupported function or user name not found. */

                    System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceError, "[" + this.GetType ().ToString () + "] " + directoryException.Message);

                }

            }

            return userDisplayName;

        }

        public List<DirectoryEntry> BrowseDirectory (String directoryPath) {

            List<DirectoryEntry> directoryEntries = new List<DirectoryEntry> ();


            System.DirectoryServices.DirectoryEntry directory = new System.DirectoryServices.DirectoryEntry ();

            Boolean foundDirectoryEntry = false;


            try { // TO CONNECT TO DIRECTORY SERVICE BY DOMAIN NAME ONLY 

                directory = new System.DirectoryServices.DirectoryEntry ("WinNT://" + credentials.Domain);

                foundDirectoryEntry = true;

            }

            catch (Exception directoryExceptionDomainOnly) {

                System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceError, directoryExceptionDomainOnly);

                try { // TO CONNECT TO DIRECTORY SERVICE BY DOMAIN NAME AND SERVER NAME

                    directory = new System.DirectoryServices.DirectoryEntry ("WinNT://" + credentials.Domain + "/" + credentials.ServerName);

                    foundDirectoryEntry = true;

                }

                catch (Exception directoryException) {

                    System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceError, directoryException);

                    // groupDictionary.Add ("0", "Unable to retreive group list for this Domain (" + credentials.Domain + ").");

                }

            } // END TRY: CONNECT TO DIRECTORY SERVICES 


            if (foundDirectoryEntry) {

                directory.Children.SchemaFilter.Add ("User");

                directory.Children.SchemaFilter.Add ("Group");

                foreach (System.DirectoryServices.DirectoryEntry currentEntry in directory.Children) {

                    if ((currentEntry.SchemaClassName.Equals ("Group")) || (currentEntry.SchemaClassName.Equals ("User"))) {

                        DirectoryEntry directoryEntry = new DirectoryEntry (currentEntry);

                        if ((directoryEntry.ObjectType == "Group") || (directoryEntry.ObjectType == "User")) {

                            directoryEntries.Add (directoryEntry);

                        }

                    }

                }

            } // if (foundDirectoryEntry)


            return directoryEntries;


        }

        public List<DirectoryEntry> GetSecurityGroupMembership (String securityGroupId) {
            
            System.DirectoryServices.DirectoryEntry rootDirectoryEntry;

            List<DirectoryEntry> membership = new List<DirectoryEntry> ();

            Boolean foundRootDirectoryEntry = false;

            SecurityGroup securityGroup = new SecurityGroup ();

            String objectSid;


            if ((String.IsNullOrWhiteSpace (credentials.Domain)) && (String.IsNullOrWhiteSpace (credentials.ServerName))) { return membership; }


            try { // TO CONNECT TO DIRECTORY SERVICE BY DOMAIN NAME ONLY 

                rootDirectoryEntry = new System.DirectoryServices.DirectoryEntry ("WinNT://" + ((String.IsNullOrWhiteSpace (credentials.Domain) ? credentials.ServerName : credentials.Domain)));

                foundRootDirectoryEntry = true;

            }

            catch (Exception directoryExceptionDomainOnly) {

                System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceError, directoryExceptionDomainOnly);

                try { // TO CONNECT TO DIRECTORY SERVICE BY DOMAIN NAME AND SERVER NAME

                    rootDirectoryEntry = new System.DirectoryServices.DirectoryEntry ("WinNT://" + credentials.Domain + "/" + credentials.ServerName);

                    foundRootDirectoryEntry = true;

                }

                catch (Exception directoryException) {

                    System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceError, directoryException);

                    throw new ApplicationException ("Unable to retreive group list for this Domain (" + credentials.Domain + ").");

                }

            } // END TRY: CONNECT TO DIRECTORY SERVICES 



            if (foundRootDirectoryEntry) {
                
                foreach (System.DirectoryServices.DirectoryEntry currentEntry in rootDirectoryEntry.Children) {

                    try {

                        if (currentEntry.SchemaClassName.ToLower () == "group") {

                            if (currentEntry.Properties["groupType"].Value != null) {

                                objectSid = ObjectSidToString (((byte[])currentEntry.Properties["objectSid"].Value));

                                if (securityGroupId == objectSid) {

                                    Object groupMembers = currentEntry.Invoke ("members", null);

                                    foreach (Object currentMember in (System.Collections.IEnumerable)groupMembers) {

                                        membership.Add (new DirectoryEntry (new System.DirectoryServices.DirectoryEntry (currentMember)));

                                    }

                                }

                            }

                        }

                    }

                    catch (Exception propertyException) {

                        // DO NOTHING

                        System.Diagnostics.Trace.WriteLineIf (traceSwitchSecurity.TraceError, propertyException.Message);

                    }

                }

            }

            return membership;

        }

        #endregion 

    }

}
