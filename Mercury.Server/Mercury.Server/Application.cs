using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server {

    [Serializable]
    public class Application {

        #region Private Properties

        [NonSerialized]
        Caching.CacheManager cacheManager = new Caching.CacheManager ();

        private TimeSpan? cacheExpirationData = null;

        private TimeSpan? cacheExpirationReference = null;


        [NonSerialized]
        private Mercury.Server.Data.SqlDatabase enterpriseDatabase = null;

        [NonSerialized]
        private Mercury.Server.Data.SqlDatabase environmentDatabase = null;

        private Dictionary<String, Data.SqlConfiguration> wellKnownEnvironments = new Dictionary<String, Data.SqlConfiguration> ();

        [NonSerialized]
        private Mercury.Server.Data.SqlConfiguration enterpriseConfiguration = null;

        private Session session;

        [NonSerialized]
        private System.Diagnostics.TraceSwitch traceSwitchGeneral = null;

        [NonSerialized]
        private System.Diagnostics.TraceSwitch traceSwitchSecurity = null;

        [NonSerialized]
        private System.Diagnostics.TraceSwitch traceSwitchWorkflow = null;

        private Exception lastException;

        private String version;


        // CONFIGURABLE PROPERTIES 

        private Int32? sessionLastActivityUpdateMinutes = null;

        private Int32? sessionTimeoutMinutes = null;

        private Boolean? useFormControlEventHandlerCaching = null;

        #endregion


        #region Public Properties

        public Caching.CacheManager CacheManager {

            get {

                if (cacheManager == null) { cacheManager = new Mercury.Server.Caching.CacheManager (); }

                return cacheManager;

            }

            set { cacheManager = value; }

        }

        public TimeSpan CacheExpirationData {

            get {

                if (cacheExpirationData.HasValue) { return cacheExpirationData.Value; }

                cacheExpirationData = TimeSpan.Zero;

                try {

                    String configurationValueString = (((String)System.Configuration.ConfigurationManager.AppSettings.GetValues ("CacheExpirationDataSeconds")[0]));

                    Int32 configurationValueSeconds;

                    if (Int32.TryParse (configurationValueString, out configurationValueSeconds)) {

                        cacheExpirationData = new TimeSpan (0, 0, configurationValueSeconds);

                    }

                }

                catch { /* DO NOTHING */ }

                return cacheExpirationData.Value;

            }

        }

        public TimeSpan CacheExpirationReference {

            get {

                if (cacheExpirationReference.HasValue) { return cacheExpirationReference.Value; }

                cacheExpirationReference = TimeSpan.Zero;

                try {

                    String configurationValueString = (((String)System.Configuration.ConfigurationManager.AppSettings.GetValues ("CacheExpirationReferenceSeconds")[0]));

                    Int32 configurationValueSeconds;

                    if (Int32.TryParse (configurationValueString, out configurationValueSeconds)) {

                        cacheExpirationReference = new TimeSpan (0, 0, configurationValueSeconds);

                    }

                }

                catch { /* DO NOTHING */ }

                return cacheExpirationReference.Value;

            }

        }

        public Mercury.Server.Data.SqlDatabase EnterpriseDatabase {
            get {

                if (enterpriseDatabase == null) {


                    System.Security.Principal.WindowsImpersonationContext impersonationContext = null;

                    try {

                        System.Diagnostics.Trace.WriteLineIf (TraceSwitchSecurity.TraceVerbose, "\r\n[Mercury.Server.Application.EnterpriseDatabase] Connection Current Credentials");

                        System.Diagnostics.Trace.WriteLineIf (TraceSwitchSecurity.TraceVerbose, "Server Thread Principal: " + System.Threading.Thread.CurrentPrincipal.Identity.Name);

                        System.Diagnostics.Trace.WriteLineIf (TraceSwitchSecurity.TraceVerbose, "Server System Principal: " + System.Security.Principal.WindowsIdentity.GetCurrent ().Name);

                        System.Diagnostics.Trace.WriteLineIf (TraceSwitchSecurity.TraceVerbose, "Server Thread Authentication Type: " + System.Threading.Thread.CurrentPrincipal.Identity.AuthenticationType);


                        impersonationContext = System.Security.Principal.WindowsIdentity.GetCurrent ().Impersonate ();


                        System.Diagnostics.Trace.WriteLineIf (TraceSwitchSecurity.TraceVerbose, "\r\n[Mercury.Server.Application.EnterpriseDatabase] Connection Impersonated Credentials");

                        System.Diagnostics.Trace.WriteLineIf (TraceSwitchSecurity.TraceVerbose, "Server Impresonation Thread Principal: " + System.Threading.Thread.CurrentPrincipal.Identity.Name);

                        System.Diagnostics.Trace.WriteLineIf (TraceSwitchSecurity.TraceVerbose, "Server Impresonation System Principal: " + System.Security.Principal.WindowsIdentity.GetCurrent ().Name);


                        if (enterpriseConfiguration == null) {

                            enterpriseDatabase = new Mercury.Server.Data.SqlDatabase (new Mercury.Server.Data.SqlConfiguration ("MercuryEnterprise.SqlConnection"));

                        }

                        else {

                            enterpriseDatabase = new Mercury.Server.Data.SqlDatabase (enterpriseConfiguration);

                        }

                    }

                    catch (System.Data.SqlClient.SqlException databaseException) {

                        SetLastException (databaseException);

                        enterpriseDatabase = null;

                    }

                    catch (Exception unhandledException) {

                        SetLastException (unhandledException);

                        enterpriseDatabase = null;

                    }

                    finally {

                        if (impersonationContext != null) {

                            impersonationContext.Undo ();

                        }

                    }

                    return enterpriseDatabase;

                }

                else {


                    return enterpriseDatabase;

                }
            }
        }

        public Mercury.Server.Data.SqlDatabase EnvironmentDatabase {

            get {

                if (environmentDatabase == null) {

                    System.Security.Principal.WindowsImpersonationContext impersonationContext = null;

                    try {

                        System.Diagnostics.Trace.WriteLineIf (TraceSwitchSecurity.TraceVerbose, "\r\n[Mercury.Server.Application.Environment] Connection Current Credentials");

                        System.Diagnostics.Trace.WriteLineIf (TraceSwitchSecurity.TraceVerbose, "Server Thread Principal: " + System.Threading.Thread.CurrentPrincipal.Identity.Name);

                        System.Diagnostics.Trace.WriteLineIf (TraceSwitchSecurity.TraceVerbose, "Server System Principal: " + System.Security.Principal.WindowsIdentity.GetCurrent ().Name);

                        System.Diagnostics.Trace.WriteLineIf (TraceSwitchSecurity.TraceVerbose, "Server Thread Authentication Type: " + System.Threading.Thread.CurrentPrincipal.Identity.AuthenticationType);


                        impersonationContext = System.Security.Principal.WindowsIdentity.GetCurrent ().Impersonate ();


                        System.Diagnostics.Trace.WriteLineIf (TraceSwitchSecurity.TraceVerbose, "\r\n[Mercury.Server.Application.Environment] Connection Impersonated Credentials");

                        System.Diagnostics.Trace.WriteLineIf (TraceSwitchSecurity.TraceVerbose, "Server Impresonation Thread Principal: " + System.Threading.Thread.CurrentPrincipal.Identity.Name);

                        System.Diagnostics.Trace.WriteLineIf (TraceSwitchSecurity.TraceVerbose, "Server Impresonation System Principal: " + System.Security.Principal.WindowsIdentity.GetCurrent ().Name);


                        Data.SqlConfiguration sqlConfiguration = null;

                        if (wellKnownEnvironments.ContainsKey (session.EnvironmentName)) {

                            sqlConfiguration = wellKnownEnvironments[session.EnvironmentName];

                        }

                        if (sqlConfiguration == null) {

                            sqlConfiguration = (new Mercury.Server.Environment.Environment (this, session.EnvironmentId)).SqlConfiguration;

                            wellKnownEnvironments.Add (session.EnvironmentName, sqlConfiguration);

                            ApplicationUpdate ();

                        }

                        environmentDatabase = new Mercury.Server.Data.SqlDatabase (sqlConfiguration);

                    }

                    catch (System.Data.SqlClient.SqlException databaseException) {

                        SetLastException (databaseException);

                        environmentDatabase = null;

                    }

                    catch (Exception unhandledException) {

                        SetLastException (unhandledException);

                        environmentDatabase = null;

                    }

                    finally {

                        if (impersonationContext != null) {

                            impersonationContext.Undo ();

                        }

                    }

                    return environmentDatabase;

                }

                else {

                    return environmentDatabase;

                }
            }

        }

        public Mercury.Server.Data.SqlDatabase EnvironmentDatabaseByName (String environmentName) {

            Mercury.Server.Data.SqlDatabase database;


            System.Security.Principal.WindowsImpersonationContext impersonationContext = null;

            try {

                System.Diagnostics.Trace.WriteLineIf (TraceSwitchSecurity.TraceVerbose, "\r\n[Mercury.Server.Application.EnterpriseDatabase] Connection Current Credentials");

                System.Diagnostics.Trace.WriteLineIf (TraceSwitchSecurity.TraceVerbose, "Server Thread Principal: " + System.Threading.Thread.CurrentPrincipal.Identity.Name);

                System.Diagnostics.Trace.WriteLineIf (TraceSwitchSecurity.TraceVerbose, "Server System Principal: " + System.Security.Principal.WindowsIdentity.GetCurrent ().Name);

                System.Diagnostics.Trace.WriteLineIf (TraceSwitchSecurity.TraceVerbose, "Server Thread Authentication Type: " + System.Threading.Thread.CurrentPrincipal.Identity.AuthenticationType);


                impersonationContext = System.Security.Principal.WindowsIdentity.GetCurrent ().Impersonate ();


                System.Diagnostics.Trace.WriteLineIf (TraceSwitchSecurity.TraceVerbose, "\r\n[Mercury.Server.Application.EnterpriseDatabase] Connection Impersonated Credentials");

                System.Diagnostics.Trace.WriteLineIf (TraceSwitchSecurity.TraceVerbose, "Server Impresonation Thread Principal: " + System.Threading.Thread.CurrentPrincipal.Identity.Name);

                System.Diagnostics.Trace.WriteLineIf (TraceSwitchSecurity.TraceVerbose, "Server Impresonation System Principal: " + System.Security.Principal.WindowsIdentity.GetCurrent ().Name);


                Data.SqlConfiguration sqlConfiguration = null;

                if (wellKnownEnvironments.ContainsKey (environmentName)) {

                    sqlConfiguration = wellKnownEnvironments[environmentName];

                }

                if (sqlConfiguration == null) {

                    Int64 environmentId = EnvironmentGetIdByName (environmentName);

                    sqlConfiguration = (new Mercury.Server.Environment.Environment (this, environmentId)).SqlConfiguration;

                    wellKnownEnvironments.Add (environmentName, sqlConfiguration);

                    ApplicationUpdate ();

                }

                database = new Mercury.Server.Data.SqlDatabase (sqlConfiguration);

            }

            catch (System.Data.SqlClient.SqlException databaseException) {

                SetLastException (databaseException);

                database = null;

            }

            catch (Exception unhandledException) {

                SetLastException (unhandledException);

                database = null;

            }

            finally {

                if (impersonationContext != null) {

                    impersonationContext.Undo ();

                }

            }

            return database;

        }

        public Mercury.Server.Data.SqlDatabase EnvironmentDatabaseById (Int64 environmentId) {

            String environmentName = this.EnvironmentGet (environmentId).Name;

            return EnvironmentDatabaseByName (environmentName);

        }

        public Session Session { get { return session; } }

        public System.Diagnostics.TraceSwitch TraceSwitchGeneral {

            get {

                if (traceSwitchGeneral != null) { return traceSwitchGeneral; }

                traceSwitchGeneral = new System.Diagnostics.TraceSwitch ("General", "General Diagnostics Messages");

                return traceSwitchGeneral;

            }

        }

        public System.Diagnostics.TraceSwitch TraceSwitchSecurity {

            get {

                if (traceSwitchSecurity != null) { return traceSwitchSecurity; }

                traceSwitchSecurity = new System.Diagnostics.TraceSwitch ("Security", "Messages related to the Enterprise and Security");

                return traceSwitchSecurity;

            }

        }

        public System.Diagnostics.TraceSwitch TraceSwitchWorkflow {

            get {

                if (traceSwitchWorkflow != null) { return traceSwitchWorkflow; }

                traceSwitchWorkflow = new System.Diagnostics.TraceSwitch ("Workflow", "Workflow Process Messages");

                return traceSwitchWorkflow;

            }

        }

        public Exception LastException { get { return lastException; } }

        public String Version {

            get {

                if (String.IsNullOrEmpty (version)) {

                    version = System.Reflection.Assembly.GetAssembly (this.GetType ()).GetName ().Version.ToString ();

                }

                return version;

            }

        }

        public Boolean UseFormControlEventHandlerCaching {

            get {

                if (useFormControlEventHandlerCaching.HasValue) { return useFormControlEventHandlerCaching.Value; }

                useFormControlEventHandlerCaching = true;

                try {

                    String configurationValueString = (((String) System.Configuration.ConfigurationManager.AppSettings.GetValues ("UseFormControlEventHandlerCaching")[0]).ToLower (new System.Globalization.CultureInfo ("")));

                    Boolean configurationValueBoolean;

                    if (Boolean.TryParse (configurationValueString, out configurationValueBoolean)) {

                        useFormControlEventHandlerCaching = configurationValueBoolean;

                    }

                    else { useFormControlEventHandlerCaching = true; }

                }

                catch { /* DO NOTHING */ }

                return useFormControlEventHandlerCaching.Value;

            }

        }

        public Int32 SessionLastActivityUpdateMinutes {

            get {

                if (sessionLastActivityUpdateMinutes.HasValue) { return sessionLastActivityUpdateMinutes.Value; }

                sessionLastActivityUpdateMinutes = 1; // DEFAULT 1

                try { // READ TRUE VALUE FROM CONFIG IF IT EXISTS

                    String configurationValueString = (((String) System.Configuration.ConfigurationManager.AppSettings.GetValues ("SessionLastActivityUpdateMinutes")[0]));

                    Int32 configurationValueInt32;

                    if (Int32.TryParse (configurationValueString, out configurationValueInt32)) {

                        sessionLastActivityUpdateMinutes = (configurationValueInt32 >= 1) ? configurationValueInt32 : 1;  // MINIMUM VALUE OF 1

                    }

                }

                catch { /* DO NOTHING */ }

                return sessionLastActivityUpdateMinutes.Value;

            }

        }

        public Int32 SessionTimeoutMinutes {

            get {

                if (sessionTimeoutMinutes.HasValue) { return sessionTimeoutMinutes.Value; }

                sessionTimeoutMinutes = 20; // DEFAULT 20

                try { // READ TRUE VALUE FROM CONFIG IF IT EXISTS

                    if (System.Configuration.ConfigurationManager.AppSettings.GetValues ("SessionTimeoutMinutes") != null) {

                        String configurationValueString = (((String)System.Configuration.ConfigurationManager.AppSettings.GetValues ("SessionTimeoutMinutes")[0]));

                        Int32 configurationValueInt32;

                        if (Int32.TryParse (configurationValueString, out configurationValueInt32)) {

                            sessionTimeoutMinutes = (configurationValueInt32 >= 1) ? configurationValueInt32 : 1;  // MINIMUM VALUE OF 1

                        }

                    }

                }

                catch { /* DO NOTHING */ }

                return sessionTimeoutMinutes.Value;

            }

        }

        #endregion


        #region Public Events

        public event EventHandler ApplicationUpdated;

        public void ApplicationUpdate () {

            if (ApplicationUpdated != null) { ApplicationUpdated (this, new EventArgs ()); }

        }

        #endregion 

        
        #region Constructors

        public Application () {

            // STANDARD CONSTRUCTOR, DO NOTHING

        }

        public Application (Mercury.Server.Data.SqlConfiguration forEnterpriseConfiguration) {

            enterpriseConfiguration = forEnterpriseConfiguration;

            return;

        }

        public Application (Mercury.Server.Data.SqlConfiguration forEnterpriseConfiguration, String token) {

            enterpriseConfiguration = forEnterpriseConfiguration;

            session = new Session (this, token);

            return;

        }

        public Application (String token) {

            // INITIALIZE THROUGH LEVEL 2 CACHE

            session = new Session (this, token);

            return;

        }

        public Application (Session cachedSession) {

            // INITIALIZE THROUGH LEVEL 1 CACHE

            session = cachedSession;

            // UPDATE LEVEL 2 CACHE LAST ACCESSED 

        }

        #endregion


        #region Support Methods

        public void ClearLastException () {

            lastException = null;

            return;

        }

        public void SetLastException (Exception exception) {

            lastException = exception;

            if (lastException != null) {

                if (session != null) {

                    System.Diagnostics.Trace.WriteLineIf (TraceSwitchGeneral.TraceError, session.UserAccountId + " [" + session.EnvironmentName + "]: " + session.UserDisplayName);

                }


                System.Diagnostics.Trace.WriteLineIf (TraceSwitchGeneral.TraceError, "Server.Application [" + DateTime.Now.ToString () + "] [" + lastException.Source + "] " + lastException.Message);

                if (lastException.InnerException != null) {

                    System.Diagnostics.Trace.WriteLineIf (TraceSwitchGeneral.TraceError, "Server.Application [" + lastException.InnerException.Source + "] " + lastException.InnerException.Message);

                }

                System.Diagnostics.Trace.WriteLineIf (TraceSwitchGeneral.TraceError, "** Stack Trace **");

                System.Diagnostics.StackTrace debugStack = new System.Diagnostics.StackTrace ();

                foreach (System.Diagnostics.StackFrame currentStackFrame in debugStack.GetFrames ()) {

                    System.Diagnostics.Trace.WriteLineIf (TraceSwitchGeneral.TraceError, "    [" + currentStackFrame.GetMethod ().Module.Assembly.FullName + "] " + currentStackFrame.GetMethod ().Name);

                }

                System.Diagnostics.Trace.Flush ();

            } // if (lastException != null) 

            return;

        }

        public void SetLastExceptionQuite (Exception exception) {

            lastException = exception;

            return;

        }

        public String GetAssemblyReference (String assemblyName) {

            String assemblyPath = (String) EnvironmentDatabase.LookupValue ("Workflow", "AssemblyPath", "AssemblyName = '" + assemblyName + "'", String.Empty);

            assemblyPath = assemblyPath.Trim ();

            assemblyPath = (!assemblyPath.EndsWith ("\\")) ? assemblyPath + "\\" : assemblyPath;

            return assemblyPath + assemblyName;

        }

        public void InvalidateCache (String objectType, Int64 id = 0, String name = "") {

            CacheManager.RemoveObject ("Application." + session.EnvironmentId + "." + objectType + ".Available");

            CacheManager.RemoveObject ("Application." + session.EnvironmentId + "." + objectType + ".Dictionary");

            CacheManager.RemoveObject ("Application." + session.EnvironmentId + "." + objectType + ".VisibleEnabled");

            CacheManager.RemoveObject ("Application." + session.EnvironmentId + "." + objectType + "." + id.ToString ());

            CacheManager.RemoveObject ("Application." + session.EnvironmentId + "." + objectType + "." + name);

            return;

        }

        #endregion 


        #region Trace Support

        /// <summary>
        /// Trace Switch General, Trace Verbose
        /// </summary>
        /// <param name="line"></param>
        public void TraceWriteLine (String line) {

            System.Diagnostics.Trace.WriteLineIf (TraceSwitchGeneral.TraceVerbose, line);

            System.Diagnostics.Trace.Flush ();

            return;

        }

        public void TraceWriteLineError (System.Diagnostics.TraceSwitch traceSwitch, String outputline) {

            System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceError, outputline);

            System.Diagnostics.Trace.Flush ();

            return;

        }
        
        public void TraceWriteLineWarning (System.Diagnostics.TraceSwitch traceSwitch, String outputline) {

            System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, outputline);

            System.Diagnostics.Trace.Flush ();

            return;

        }

        public void TraceWriteLineInfo (System.Diagnostics.TraceSwitch traceSwitch, String outputline) {

            System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceInfo, outputline);

            System.Diagnostics.Trace.Flush ();

            return;

        }

        public void TraceWriteLineVerbose (System.Diagnostics.TraceSwitch traceSwitch, String outputline) {

            System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, outputline);

            System.Diagnostics.Trace.Flush ();

            return;

        }

        #endregion



        #region Session Management

        public Session CreateSession (Mercury.Server.Public.Interfaces.Security.Credentials credentials) {

            session = new Session (this, credentials);


            try {

                String insertStatement = "INSERT INTO Audit.Authentication VALUES (";

                insertStatement = insertStatement + "'" + session.Token.ToString () + "', ";

                insertStatement = insertStatement + "GETDATE (), "; // LOGON DATE

                insertStatement = insertStatement + "NULL, "; // LOGOFF DATE

                insertStatement = insertStatement + credentials.AuthenticationTime.ToString () + ", "; //

                insertStatement = insertStatement + "GETDATE (), "; // LAST ACTIVITY DATE

                insertStatement = insertStatement + session.EnvironmentId.ToString () + ", ";

                insertStatement = insertStatement + session.SecurityAuthorityId.ToString () + ", ";

                insertStatement = insertStatement + "'" + session.UserAccountId.Replace ("'", "''") + "', ";

                insertStatement = insertStatement + "'" + session.UserAccountName.Replace ("'", "''") + "', ";

                insertStatement = insertStatement + "'" + session.UserDisplayName.Replace ("'", "''") + "', ";



                Data.AuthorityAccountStamp modifiedAccountInfo = new Data.AuthorityAccountStamp (session);

                insertStatement = insertStatement + "'" + modifiedAccountInfo.SecurityAuthorityNameSql + "', '" + modifiedAccountInfo.UserAccountIdSql + "', '" + modifiedAccountInfo.UserAccountNameSql + "', GETDATE (), ";

                insertStatement = insertStatement + "'" + modifiedAccountInfo.SecurityAuthorityNameSql + "', '" + modifiedAccountInfo.UserAccountIdSql + "', '" + modifiedAccountInfo.UserAccountNameSql + "', GETDATE ()";


                insertStatement = insertStatement + ")";

                EnterpriseDatabase.ExecuteSqlStatement (insertStatement);

            }

            catch { /* DO NOTHING */ }



            // TODO: CACHE LEVEL 2 SESSION

            return session;

        }

        public void SessionUpdateLastActivity () {

            if (session == null) { return; }

            if (EnterpriseDatabase == null) { return; }


            try {

                String updateStatement = "UPDATE Audit.Authentication SET LastActivityDateTime = GETDATE () WHERE SessionToken = '" + session.Token.ToString () + "'";

                EnterpriseDatabase.ExecuteSqlStatement (updateStatement);

                session.LastActivityDate = DateTime.Now;

            }

            catch { /* DO NOTHING */ }

            return;

        }

        public void LogOff () {


            String auditStatement = String.Empty;


            try {

                auditStatement = String.Empty;

                auditStatement += "SET TRANSACTION ISOLATION LEVEL REPEATABLE READ \r\n";

                auditStatement += "BEGIN TRANSACTION \r\n";

                auditStatement += "UPDATE Audit.Authentication \r\n";

                auditStatement += "  SET LogoffDateTime = GETDATE () \r\n";

                auditStatement += "  WHERE SessionToken = '" + Session.Token + "' \r\n";

                auditStatement += "DELETE FROM SessionCache WHERE SessionToken = '" + Session.Token + "' \r\n";

                auditStatement += "COMMIT TRANSACTION \r\n";

                EnterpriseDatabase.ExecuteSqlStatement (auditStatement);



                auditStatement = String.Empty;

                auditStatement += "SET TRANSACTION ISOLATION LEVEL REPEATABLE READ \r\n";

                auditStatement += "BEGIN TRANSACTION \r\n";

                auditStatement += "UPDATE Audit.Authentication \r\n";

                auditStatement += "  SET LogoffDateTime = GETDATE () \r\n";

                auditStatement += "  WHERE SessionToken IN (SELECT SessionToken FROM SessionCache WHERE (ExpirationTime < GETDATE ())) \r\n";

                auditStatement += "DELETE FROM SessionCache WHERE (ExpirationTime < GETDATE ()) \r\n";

                auditStatement += "COMMIT TRANSACTION \r\n";

                EnterpriseDatabase.ExecuteSqlStatement (auditStatement);

            }

            catch { /* DO NOTHING */ }

            return;

        }

        #endregion


        #region Permission Sets

        public List<String> EnterprisePermissionsGet (Int64 securityAuthorityId, List<String> securityGroups) {

            List<String> permissionSet = new List<String> ();

            System.Data.DataTable permissionResults;

            try {

                StringBuilder sqlStatement = new StringBuilder ();

                sqlStatement.Append ("SELECT Permission.PermissionId, Permission.PermissionName, IsGranted, IsDenied");

                sqlStatement.Append ("  FROM SecurityGroupPermission");

                sqlStatement.Append ("    JOIN Permission ON SecurityGroupPermission.PermissionId = Permission.PermissionId");

                sqlStatement.Append ("  WHERE SecurityAuthorityId = " + securityAuthorityId + " AND SecurityGroupId IN (");


                foreach (String currentGroup in securityGroups) {

                    sqlStatement.Append ("'");

                    sqlStatement.Append (currentGroup);

                    sqlStatement.Append ("', ");

                }

                sqlStatement.Append (" '') ORDER BY IsDenied");

                permissionResults = EnterpriseDatabase.SelectDataTable (sqlStatement.ToString ());

                foreach (System.Data.DataRow currentPermission in permissionResults.Rows) {

                    if ((!(Boolean) currentPermission["IsDenied"]) && ((Boolean) currentPermission["IsGranted"])) {

                        if (!permissionSet.Contains ((String) currentPermission["PermissionName"])) {

                            permissionSet.Add (((String) currentPermission["PermissionName"]));

                        }

                    }

                    else {

                        if (permissionSet.Contains ((String) currentPermission["PermissionName"])) {

                            permissionSet.Remove (((String) currentPermission["PermissionName"]));

                        }

                    }

                } // end foreach currentPermission

            } // end try

            catch {

                // DO NOTHING, FAILURE TO GET PERMISSIONS MEANS NO PERMISSIONS                

            }


            return permissionSet;


        }

        public List<String> EnvironmentPermissionsGet (String environmentName, Int64 securityAuthorityId, List<String> environmentRoles) {

            List<String> permissionSet = new List<String> ();

            System.Data.DataTable permissionResults;

            StringBuilder sqlStatement = new StringBuilder ();

            sqlStatement.Append ("SELECT Permission.PermissionId, Permission.PermissionName, IsGranted, IsDenied");

            sqlStatement.Append ("  FROM RolePermission JOIN Permission ON RolePermission.PermissionId = Permission.PermissionId");

            sqlStatement.Append ("  WHERE RoleId IN (");

            foreach (String currentRoleId in environmentRoles) {

                sqlStatement.Append (currentRoleId + ", ");

            }

            sqlStatement.Append ("-1) ORDER BY IsDenied");

            permissionResults = EnvironmentDatabaseByName (environmentName).SelectDataTable (sqlStatement.ToString ());

            foreach (System.Data.DataRow currentPermission in permissionResults.Rows) {

                if (!(Boolean) currentPermission["IsDenied"]) {

                    if (!permissionSet.Contains ((String) currentPermission["PermissionName"])) {

                        permissionSet.Add (((String) currentPermission["PermissionName"]));

                    }

                }

                else {

                    if (permissionSet.Contains ((String) currentPermission["PermissionName"])) {

                        permissionSet.Remove (((String) currentPermission["PermissionName"]));

                    }

                }

            } // foreach 

            return permissionSet;

        }

        #endregion 


        #region Authorization Validation

        public Boolean HasEnterprisePermission (String permissionName) {

            Boolean hasPermission = false;

            if (session.EnterprisePermissionSet.Contains (EnterprisePermissions.EnterpriseAdministrator)) { hasPermission = true; }

            else { hasPermission = session.EnterprisePermissionSet.Contains (permissionName); }

            // TODO: AUDIT PERMISSION USAGE

            return hasPermission;

        }

        public Boolean HasEnvironmentPermission (String permissionName) {

            Boolean hasPermission = false;

            if (session.EnterprisePermissionSet.Contains (EnterprisePermissions.EnterpriseAdministrator)) { hasPermission = true; }

            else if (session.EnvironmentPermissionSet.Contains (EnvironmentPermissions.EnvironmentAdministrator)) { hasPermission = true; }

            else { hasPermission = session.EnvironmentPermissionSet.Contains (permissionName); }

            // TODO: AUDIT PERMISSION USAGE

            return hasPermission;

        }

        public Boolean HasEnvironmentPermission (String environmentName, String permissionName) {

            Boolean hasPermission = false;

            if (session.EnterprisePermissionSet.Contains (EnterprisePermissions.EnterpriseAdministrator)) { hasPermission = true; }

            if (session.EnvironmentName == environmentName) { hasPermission = HasEnvironmentPermission (permissionName); }

            if (!hasPermission) {

                List<String> environmentRoles = EnvironmentRoleMembershipListGet (EnvironmentGet (environmentName).Id, Session.SecurityAuthorityId, Session.GroupMembership);

                List<String> environmentPermissions = EnvironmentPermissionsGet (environmentName, session.SecurityAuthorityId, environmentRoles);

                if (environmentPermissions.Contains (EnvironmentPermissions.EnvironmentAdministrator)) { hasPermission = true; }

                else { hasPermission = environmentPermissions.Contains (permissionName); }

            }

            // TODO: AUDIT PERMISSION USAGE

            return hasPermission;

        }

        #endregion


        #region Audit

        public List<Audit.Authentication> ActiveSessionsAvailable () {

            List<Audit.Authentication> activeSessions = new List<Mercury.Server.Audit.Authentication> ();


            String selectStatement = "SELECT * FROM Audit.Authentication WHERE LogoffDateTime IS NULL ORDER BY EnvironmentId, UserDisplayName";

            System.Data.DataTable authenticationTable = EnterpriseDatabase.SelectDataTable (selectStatement, 0);

            foreach (System.Data.DataRow currentRow in authenticationTable.Rows) {

                Audit.Authentication activeSession = new Mercury.Server.Audit.Authentication ();

                activeSession.MapDataFields (currentRow);

                activeSessions.Add (activeSession);

            }


            return activeSessions;

        }

        #endregion 


        #region Enterprise

        public Mercury.Server.Security.Permission EnterprisePermission (String permission) {

            Mercury.Server.Security.Permission enterprisePermission = null;

            try {

                enterprisePermission = new Mercury.Server.Security.Permission (EnterpriseDatabase, permission);

            }

            catch (Exception domainObjectException) {

                SetLastException (domainObjectException);

                permission = null;

            }

            return enterprisePermission;

        }

        public List<String> EnterprisePermissionList () {

            List<String> permissionList = new List<string> ();

            SetLastException (null);

            if (HasEnterprisePermission (EnterprisePermissions.EnterprisePermissionReview)) {

                try {

                    StringBuilder sqlStatement = new StringBuilder ("SELECT PermissionName FROM Permission ORDER BY PermissionName");

                    System.Data.DataTable permissionTable = EnterpriseDatabase.SelectDataTable (sqlStatement.ToString ());

                    foreach (System.Data.DataRow currentPermission in permissionTable.Rows) {

                        permissionList.Add ((String)currentPermission["PermissionName"]);

                    }

                }

                catch (Exception applicationException) {

                    SetLastException (applicationException);

                }

            }

            return permissionList;

        }

        public Dictionary<Int64, String> EnterprisePermissionDictionary () {

            Dictionary<Int64, String> permissionDictionary = new Dictionary<Int64, String> ();

            SetLastException (null);

            if (HasEnterprisePermission (EnterprisePermissions.EnterprisePermissionReview)) {

                try {

                    StringBuilder sqlStatement = new StringBuilder ("SELECT PermissionId, PermissionName FROM Permission ORDER BY PermissionName");

                    System.Data.DataTable permissionTable = EnterpriseDatabase.SelectDataTable (sqlStatement.ToString ());

                    foreach (System.Data.DataRow currentPermission in permissionTable.Rows) {

                        permissionDictionary.Add ((Int64)currentPermission["PermissionId"], (String)currentPermission["PermissionName"]);

                    }

                }

                catch (Exception applicationException) {

                    SetLastException (applicationException);

                }

            }

            return permissionDictionary;

        }

        public List<Security.Permission> EnterprisePermissionsAvailable () {

            List<String> permissionList = EnterprisePermissionList (); // SECURITY PERMISSION CHECKED ON THIS CALL

            List<Security.Permission> permissions = new List<Security.Permission> ();

            foreach (String currentPermission in permissionList) {

                permissions.Add (EnterprisePermission (currentPermission));

            }

            return permissions;

        }

        public List<Mercury.Server.Security.SecurityGroupPermission> SecurityGroupEnterprisePermissionsGet (String securityAuthorityName, String securityGroupId) {


            List<Mercury.Server.Security.SecurityGroupPermission> securityGroupPermissions = new List<Mercury.Server.Security.SecurityGroupPermission> ();

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable groupPermissionTable;

            Mercury.Server.Security.SecurityGroupPermission securityGroupPermission;


            SetLastException (null);

            if (HasEnterprisePermission (EnterprisePermissions.SecurityAuthorityReview)) {

                try {

                    selectStatement.Append ("SecurityGroupPermission_SelectByGroupId ");
                    selectStatement.Append ("'" + securityAuthorityName + "', ");
                    selectStatement.Append ("'" + securityGroupId + "'");

                    groupPermissionTable = EnterpriseDatabase.SelectDataTable (selectStatement.ToString ());

                    foreach (System.Data.DataRow currentPermissionRow in groupPermissionTable.Rows) {

                        securityGroupPermission = new Mercury.Server.Security.SecurityGroupPermission ();
                        securityGroupPermission.MapDataFields (currentPermissionRow);

                        securityGroupPermissions.Add (securityGroupPermission);

                    }

                }

                catch (Exception applicationException) {

                    SetLastException (applicationException);

                }

            }

            return securityGroupPermissions;

        }

        public Boolean SecurityGroupEnterprisePermissionSave (Int64 securityAuthorityId, String securityGroupId, Int64 permissionId, Boolean isGranted, Boolean isDenied) {

            Mercury.Server.Security.SecurityGroupPermission securityGroupPermission;


            try {

                securityGroupPermission = new Mercury.Server.Security.SecurityGroupPermission (EnterpriseDatabase, securityAuthorityId, securityGroupId, permissionId);

            }

            catch {

                securityGroupPermission = new Mercury.Server.Security.SecurityGroupPermission ();

                securityGroupPermission.SecurityAuthorityId = securityAuthorityId;

                securityGroupPermission.SecurityGroupId = securityGroupId;

                securityGroupPermission.PermissionId = permissionId;

            }

            if (isDenied) {

                securityGroupPermission.IsGranted = false;

                securityGroupPermission.IsDenied = true;

            }

            else if (isGranted) {

                securityGroupPermission.IsGranted = true;

                securityGroupPermission.IsDenied = false;

            }

            else {

                securityGroupPermission.IsGranted = false;

                securityGroupPermission.IsDenied = false;

            }

            securityGroupPermission.ModifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (this);

            return securityGroupPermission.Save (EnterpriseDatabase);

        }

        #endregion 


        #region Security Authorities

        public List<Mercury.Server.Security.SecurityAuthority> SecurityAuthoritiesAvailable (Boolean validatePermissions) {

            List<Mercury.Server.Security.SecurityAuthority> results = new List<Mercury.Server.Security.SecurityAuthority> ();

            StringBuilder selectStatement = new StringBuilder ();


            SetLastException (null);


            if ((validatePermissions) && (!HasEnterprisePermission (EnterprisePermissions.SecurityAuthorityReview))) {

                throw new ApplicationException ("Permission Denied for Security Authorities.");

            }


            selectStatement.Append ("SELECT * FROM SecurityAuthority ORDER BY SecurityAuthorityName");

            System.Data.DataTable table = EnterpriseDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in table.Rows) {

                Mercury.Server.Security.SecurityAuthority securityAuthority = new Mercury.Server.Security.SecurityAuthority (this);

                securityAuthority.MapDataFields (currentRow);

                results.Add (securityAuthority);

            }

            return results;

        }

        public Dictionary<Int64, String> SecurityAuthorityDictionary () {

            Dictionary<Int64, String> securityAuthorityDictionary = new Dictionary<Int64, String> ();

            SetLastException (null);

            if (HasEnterprisePermission (EnterprisePermissions.SecurityAuthorityReview)) {

                try {

                    StringBuilder selectStatement = new StringBuilder ("SELECT SecurityAuthorityId, SecurityAuthorityName FROM SecurityAuthority ORDER BY SecurityAuthorityName");

                    System.Data.DataTable securityAuthorityTable = EnterpriseDatabase.SelectDataTable (selectStatement.ToString ());

                    foreach (System.Data.DataRow currentAuthority in securityAuthorityTable.Rows) {

                        securityAuthorityDictionary.Add ((Int64) currentAuthority["SecurityAuthorityId"], (String) currentAuthority["SecurityAuthorityName"]);

                    }

                }

                catch (Exception applicationException) {

                    SetLastException (applicationException);

                }

            }

            return securityAuthorityDictionary;

        }


        public Mercury.Server.Security.SecurityAuthority SecurityAuthorityGet (Int64 securityAuthorityId) {

            if (securityAuthorityId == 0) { return null; }


            Mercury.Server.Security.SecurityAuthority securityAuthority = null;

            SetLastException (null);

            try {

                securityAuthority = new Mercury.Server.Security.SecurityAuthority (this, securityAuthorityId);

            }

            catch (Exception domainObjectException) {

                SetLastException (domainObjectException);

            }

            return securityAuthority;

        }

        public Mercury.Server.Security.SecurityAuthority SecurityAuthorityGet (String securityAuthorityName) {

            if (String.IsNullOrEmpty (securityAuthorityName)) { return null; }

            Int64 securityAuthorityId = SecurityAuthorityGetIdByName (securityAuthorityName);

            Mercury.Server.Security.SecurityAuthority securityAuthority = SecurityAuthorityGet (securityAuthorityId);

            return securityAuthority;

        }


        public String SecurityAuthorityGetNameById (Int64 securityAuthorityId) {

            if (securityAuthorityId == 0) { return String.Empty; }

            String securityAuthorityName = String.Empty;


            SetLastException (null);

            try {

                securityAuthorityName = (String) EnterpriseDatabase.LookupValue ("SecurityAuthority", "SecurityAuthorityName", "SecurityAuthorityId = " + securityAuthorityId.ToString (), String.Empty);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return securityAuthorityName;

        }

        public Int64 SecurityAuthorityGetIdByName (String securityAuthorityName) {

            if (String.IsNullOrEmpty (securityAuthorityName)) { return 0; }

            Int64 securityAuthorityId = 0;


            SetLastException (null);

            try {

                securityAuthorityId = Convert.ToInt64 (EnterpriseDatabase.LookupValue ("SecurityAuthority", "SecurityAuthorityId", "SecurityAuthorityName = '" + securityAuthorityName.Replace ("'", "''") + "'", 0));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return securityAuthorityId;

        }


        public Server.Public.Interfaces.Security.IProvider SecurityAuthorityProviderGet (Server.Security.SecurityAuthority securityAuthority) {

            Server.Public.Interfaces.Security.IProvider provider;


            switch (securityAuthority.SecurityAuthorityType) {

                case Security.Enumerations.SecurityAuthorityType.ActiveDirectory: provider = new Security.Providers.ActiveDirectory.Provider (); break;

                case Security.Enumerations.SecurityAuthorityType.WindowsIntegrated: provider = new Security.Providers.WindowsIntegrated.Provider (); break;

                case Security.Enumerations.SecurityAuthorityType.Custom:

                default:

                    provider = null; // TODO: GET CUSTOM PROVIDERS

                    break;

            }


            provider.Credentials.SecurityAuthorityId = securityAuthority.Id;

            provider.Credentials.SecurityAuthorityName = securityAuthority.Name;

            provider.Credentials.Protocol = securityAuthority.Protocol;

            provider.Credentials.Domain = securityAuthority.Domain;

            provider.Credentials.ServerName = securityAuthority.ServerName;

            provider.Credentials.SetAgentCredentials (securityAuthority.AgentName, securityAuthority.AgentPassword);          


            return provider;

        }

        public Server.Public.Interfaces.Security.IProvider SecurityAuthorityProviderGet (String securityAuthorityName) {

            Mercury.Server.Security.SecurityAuthority securityAuthority = this.SecurityAuthorityGet (securityAuthorityName);

            return SecurityAuthorityProviderGet (securityAuthority);

        }


        public Dictionary<String, String> SecurityAuthoritySecurityGroupDictionary (Int64 securityAuthorityId) {

            Dictionary<String, String> dictionary = new Dictionary<String, String> ();


            SetLastException (null);


            Mercury.Server.Public.Interfaces.Security.IProvider provider;

            Mercury.Server.Security.SecurityAuthority securityAuthority = SecurityAuthorityGet (securityAuthorityId);

            provider = SecurityAuthorityProviderGet (securityAuthority);

            provider.Credentials.Protocol = securityAuthority.Protocol;

            provider.Credentials.Domain = securityAuthority.Domain;

            provider.Credentials.ServerName = securityAuthority.ServerName;

            provider.Credentials.SetAgentCredentials (securityAuthority.AgentName, securityAuthority.AgentPassword);

            provider.Credentials.UserAccountId = Session.UserAccountId;

            provider.Credentials.UserAccountName = Session.UserAccountName;

            dictionary = GetSecurityAuthorityGroupDictionary (provider);


            return dictionary;

        }

        public List<Server.Public.Interfaces.Security.SecurityGroup> SecurityAuthoritySecurityGroups (Int64 securityAuthorityId) {

            SortedList<String, Server.Public.Interfaces.Security.SecurityGroup> sortedList = new SortedList<String, Mercury.Server.Public.Interfaces.Security.SecurityGroup> ();

            Mercury.Server.Public.Interfaces.Security.IProvider provider;

            Mercury.Server.Security.SecurityAuthority securityAuthority = SecurityAuthorityGet (securityAuthorityId);

            String securityAuthorityName = securityAuthority.Name;


            SetLastException (null);


            provider = SecurityAuthorityProviderGet (securityAuthority);

            provider.Credentials.Protocol = securityAuthority.Protocol;

            provider.Credentials.Domain = securityAuthority.Domain;

            provider.Credentials.ServerName = securityAuthority.ServerName;

            provider.Credentials.SetAgentCredentials (securityAuthority.AgentName, securityAuthority.AgentPassword);

            provider.Credentials.UserAccountId = Session.UserAccountId;

            provider.Credentials.UserAccountName = Session.UserAccountName;

            Dictionary<String, String> dictionary = GetSecurityAuthorityGroupDictionary (provider);

            foreach (String currentGroupId in dictionary.Keys) {

                Server.Public.Interfaces.Security.SecurityGroup securityGroup = new Mercury.Server.Public.Interfaces.Security.SecurityGroup ();

                securityGroup.SecurityAuthorityId = securityAuthorityId;

                securityGroup.SecurityAuthorityName = securityAuthorityName;

                securityGroup.SecurityGroupId = currentGroupId;

                securityGroup.SecurityGroupName = dictionary[currentGroupId];

                sortedList.Add (dictionary[currentGroupId] + "|" + currentGroupId, securityGroup);

            }

            List<Server.Public.Interfaces.Security.SecurityGroup> response = new List<Mercury.Server.Public.Interfaces.Security.SecurityGroup> ();

            response.AddRange (sortedList.Values);

            return response;

        }


        public Server.Public.Interfaces.Security.SecurityGroup GetSecurityGroup (Server.Public.Interfaces.Security.IProvider provider, String securityGroupId) {

            // Valid Security Authority Provider Specified
            if (provider == null) { return null; }

            // Security Authority Support Security Group Lookup
            if (!provider.Capabilities.CanGetSecurityGroups) { return null; }

            return provider.GetSecurityGroup (securityGroupId);

        }

        public Dictionary<String, String> GetSecurityAuthorityGroupDictionary (Server.Public.Interfaces.Security.IProvider provider) {

            Dictionary<String, String> groupDictionary = new Dictionary<String, String> ();

            if (provider == null) {

                groupDictionary.Add ("0", "Unable to obtain capabilities from Security Authority Provider.");

            }

            else {

                if (provider.Capabilities.CanGetSecurityGroups) {

                    groupDictionary = provider.GetSecurityGroupDictionary ();

                }

                else {

                    groupDictionary.Add ("0", "Not Supported by Security Authority Provider.");

                }

            }

            return groupDictionary;

        }

        #endregion 


        #region Environment

        public Mercury.Server.Environment.EnvironmentType EnvironmentTypeGet (Int64 environmentTypeId) {

            Mercury.Server.Environment.EnvironmentType environmentType = null;

            try {

                environmentType = new Mercury.Server.Environment.EnvironmentType (this, environmentTypeId);

            }

            catch (Exception domainObjectException) {

                SetLastException (domainObjectException);

                environmentType = null;

            }

            return environmentType;

        }

        public Mercury.Server.Environment.EnvironmentType EnvironmentTypeGet (String environmentTypeName) {

            Int64 environmentTypeId = EnvironmentTypeGetIdByName (environmentTypeName);

            return EnvironmentTypeGet (environmentTypeId);

        }

        public Int64 EnvironmentTypeGetIdByName (String environmentTypeName) {

            if (String.IsNullOrWhiteSpace (environmentTypeName)) { return 0; }

            Int64 environmentTypeId = 0;


            ClearLastException ();

            try {

                environmentTypeId = Convert.ToInt64 (EnterpriseDatabase.LookupValue ("EnvironmentType", "EnvironmentTypeId", "EnvironmentTypeName = '" + environmentTypeName.Replace ("'", "''") + "'", 0));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return environmentTypeId;

        }


        public List<Environment.Environment> EnvironmentsAvailable () {

            List<Environment.Environment> results = new List<Mercury.Server.Environment.Environment> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("SELECT * FROM Environment");

            selectStatement.Append ("  ORDER BY EnvironmentName");


            System.Data.DataTable environmentTable = EnterpriseDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in environmentTable.Rows) {

                Mercury.Server.Environment.Environment environment = new Mercury.Server.Environment.Environment (this);

                environment.MapDataFields (currentRow);

                results.Add (environment);

            }

            return results;

        }

        public Dictionary<Int64, String> EnvironmentDictionary () {

            Dictionary<Int64, String> environmentDictionary = new Dictionary<Int64, String> ();

            SetLastException (null);

            if (HasEnterprisePermission (EnterprisePermissions.EnvironmentReview)) {

                try {

                    StringBuilder selectStatement = new StringBuilder ("SELECT EnvironmentId, EnvironmentName FROM Environment ORDER BY EnvironmentName");

                    System.Data.DataTable environmentTable = EnterpriseDatabase.SelectDataTable (selectStatement.ToString ());

                    foreach (System.Data.DataRow currentAuthority in environmentTable.Rows) {

                        environmentDictionary.Add ((Int64)currentAuthority["EnvironmentId"], (String)currentAuthority["EnvironmentName"]);

                    }

                }

                catch (Exception applicationException) {

                    SetLastException (applicationException);

                }

            }

            return environmentDictionary;

        }

        public Mercury.Server.Environment.Environment EnvironmentGet (Int64 environmentId) {

            if (environmentId == 0) { return null; }

            Mercury.Server.Environment.Environment environment = null;

            try {

                environment = new Mercury.Server.Environment.Environment (this, environmentId);

            }

            catch (Exception domainObjectException) {

                SetLastException (domainObjectException);

                environment = null;

            }

            return environment;

        }

        public Mercury.Server.Environment.Environment EnvironmentGet (String environmentName) {

            Int64 environmentId = EnvironmentGetIdByName (environmentName);

            return EnvironmentGet (environmentId);

        }

        public Int64 EnvironmentGetIdByName (String environmentName) {

            if (String.IsNullOrWhiteSpace (environmentName)) { return 0; }

            Int64 environmentId = 0;


            ClearLastException ();

            try {

                environmentId = Convert.ToInt64 (EnterpriseDatabase.LookupValue ("Environment", "EnvironmentId", "EnvironmentName = '" + environmentName.Replace ("'", "''") + "'", 0));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return environmentId;

        }


        public Mercury.Server.Security.Permission EnterprisePermissionGet (String permission) {

            Mercury.Server.Security.Permission enterprisePermission = null;

            try {

                enterprisePermission = new Mercury.Server.Security.Permission (EnterpriseDatabase, permission);

            }

            catch (Exception domainObjectException) {

                SetLastException (domainObjectException);

                permission = null;

            }

            return enterprisePermission;

        }

        public Mercury.Server.Security.EnvironmentAccess EnvironmentAccessGet (Int64 environmentId, Int64 securityAuthorityId, String securityGroupId) {

            Mercury.Server.Security.EnvironmentAccess environmentAccess = null;

            try {

                environmentAccess = new Mercury.Server.Security.EnvironmentAccess (this, environmentId, securityAuthorityId, securityGroupId);

            }

            catch (Exception domainObjectException) {

                SetLastException (domainObjectException);

                environmentAccess = null;

            }

            return environmentAccess;

        }

        public List<Server.Security.EnvironmentAccess> EnvironmentAccessGetByEnvironmentName (String environmentName) {

            List<Server.Security.EnvironmentAccess> environmentAccess = new List<Security.EnvironmentAccess> ();


            ClearLastException ();

            try {

                String selectStatement = "SELECT EnvironmentAccess.*";

                selectStatement += "  FROM Environment JOIN EnvironmentAccess ON Environment.EnvironmentId = EnvironmentAccess.EnvironmentId";

                selectStatement += "    JOIN SecurityAuthority ON EnvironmentAccess.SecurityAuthorityId = SecurityAuthority.SecurityAuthorityId";

                selectStatement += "  WHERE Environment.EnvironmentName = '" + environmentName.Replace ("'", "''") + "'";

                selectStatement += "  ORDER BY SecurityAuthority.SecurityAuthorityName, EnvironmentAccess.SecurityGroupId";


                System.Data.DataTable environmentAccessTable = EnterpriseDatabase.SelectDataTable (selectStatement.ToString ());

                foreach (System.Data.DataRow currentRow in environmentAccessTable.Rows) {

                    Security.EnvironmentAccess environmentAccessEntry = new Security.EnvironmentAccess (this);

                    environmentAccessEntry.MapDataFields (currentRow);

                    environmentAccess.Add (environmentAccessEntry);

                }

            }
            

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return environmentAccess;

        }


        public List<Mercury.Server.Security.EnvironmentAccess> SecurityGroupEnvironmentAccessGet (String securityAuthorityName, String securityGroupId) {

            List<Mercury.Server.Security.EnvironmentAccess> environmentAccess = new List<Mercury.Server.Security.EnvironmentAccess> ();

            StringBuilder selectStatement = new StringBuilder ("EXEC EnvironmentAccess_SelectByGroupId '" + securityAuthorityName + "', '" + securityGroupId + "'");

            System.Data.DataTable environmentAccessTable;

            Mercury.Server.Security.EnvironmentAccess environmentAccessEntry;


            SetLastException (null);

            if (HasEnterprisePermission (EnterprisePermissions.EnvironmentReview)) {

                try {

                    environmentAccessTable = EnterpriseDatabase.SelectDataTable (selectStatement.ToString ());

                    foreach (System.Data.DataRow currentAccessRow in environmentAccessTable.Rows) {

                        environmentAccessEntry = new Mercury.Server.Security.EnvironmentAccess (this);

                        environmentAccessEntry.MapDataFields (currentAccessRow);

                        environmentAccess.Add (environmentAccessEntry);

                    }

                }

                catch (Exception applicationException) {

                    SetLastException (applicationException);

                }

            }

            return environmentAccess;

        }

        public Boolean SecurityGroupEnvironmentAccessSave (Int64 securityAuthorityId, String securityGroupId, Int64 environmentId, Boolean isGranted, Boolean isDenied) {

            Mercury.Server.Security.EnvironmentAccess environmentAccess;


            try { 
                
                environmentAccess = new Mercury.Server.Security.EnvironmentAccess (this, environmentId, securityAuthorityId, securityGroupId);

            }

            catch {

                environmentAccess = new Mercury.Server.Security.EnvironmentAccess (this);

                environmentAccess.EnvironmentId = environmentId;

                environmentAccess.SecurityAuthorityId = securityAuthorityId;

                environmentAccess.SecurityGroupId = securityGroupId;

            }

            if (isDenied) {

                environmentAccess.IsGranted = false;

                environmentAccess.IsDenied = true;

            }

            else if (isGranted) {

                environmentAccess.IsGranted = true;

                environmentAccess.IsDenied = false;

            }

            else {

                environmentAccess.IsGranted = false;

                environmentAccess.IsDenied = false;

            }

            environmentAccess.ModifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (this);

            return environmentAccess.Save ();

        }


        #region Permission Sets

        public List<String> EnvironmentPermissionList (String environmentName) {

            List<String> permissions = new List<String> ();

            Mercury.Server.Data.SqlDatabase database = EnvironmentDatabaseByName (environmentName);

            System.Data.DataTable permissionTable;


            permissionTable = database.SelectDataTable ("SELECT * FROM Permission ORDER BY PermissionName");

            foreach (System.Data.DataRow currentPermission in permissionTable.Rows) {

                permissions.Add ((String)currentPermission["PermissionName"]);

            }

            database = null;

            return permissions;

        }

        public List<Security.Permission> EnvironmentPermissionsAvailable (String environmentName) {

            List<Security.Permission> permissions = new List<Security.Permission> ();

            Mercury.Server.Data.SqlDatabase database = EnvironmentDatabaseByName (environmentName);

            System.Data.DataTable permissionTable;


            permissionTable = database.SelectDataTable ("SELECT * FROM Permission ORDER BY PermissionName");

            foreach (System.Data.DataRow currentPermission in permissionTable.Rows) {

                Mercury.Server.Security.Permission permission = new Mercury.Server.Security.Permission ();

                permission.MapDataFields (currentPermission);

                permissions.Add (permission);

            }

            database = null;

            return permissions;

        }

        public List<String> EnterprisePermissionSet (Int64 securityAuthorityId, List<String> securityGroups) {

            List<String> permissionSet = new List<String> ();

            System.Data.DataTable permissionResults;

            try {

                StringBuilder sqlStatement = new StringBuilder ();

                sqlStatement.Append ("SELECT PermissionName, IsGranted, IsDenied");
                sqlStatement.Append ("  FROM SecurityGroupPermission");
                sqlStatement.Append ("    JOIN Permission ON SecurityGroupPermission.PermissionId = Permission.PermissionId");
                sqlStatement.Append ("  WHERE SecurityAuthorityId = " + securityAuthorityId + " AND SecurityGroupId IN (");

                foreach (String currentGroup in securityGroups) {

                    sqlStatement.Append ("'");
                    sqlStatement.Append (currentGroup);
                    sqlStatement.Append ("', ");

                }

                sqlStatement.Append (" '') ORDER BY IsDenied");

                permissionResults = EnterpriseDatabase.SelectDataTable (sqlStatement.ToString ());

                foreach (System.Data.DataRow currentPermission in permissionResults.Rows) {

                    if ((!(Boolean)currentPermission["IsDenied"]) && ((Boolean)currentPermission["IsGranted"])) {

                        if (!permissionSet.Contains ((String)currentPermission["PermissionName"])) {

                            permissionSet.Add (((String)currentPermission["PermissionName"]));

                        }

                    }

                    else {

                        if (permissionSet.Contains ((String)currentPermission["PermissionName"])) {

                            permissionSet.Remove (((String)currentPermission["PermissionName"]));

                        }

                    }

                } // end foreach currentPermission

            } // end try

            catch {

                // DO NOTHING, FAILURE TO GET PERMISSIONS MEANS NO PERMISSIONS                

            }


            return permissionSet;


        }

        public List<String> EnvironmentRoleMembershipList (Int64 environmentId, Int64 securityAuthorityId, List<String> securityGroups) {

            List<String> roles = new List<String> ();

            System.Data.DataTable roleMembershipTable;

            StringBuilder sqlStatement = new StringBuilder ();

            sqlStatement.Append ("SELECT DISTINCT Role.RoleId FROM Role JOIN RoleMembership ON Role.RoleId = RoleMembership.RoleId ");

            sqlStatement.Append ("  WHERE SecurityAuthorityId = " + securityAuthorityId.ToString () + " AND SecurityGroupId IN (");


            foreach (String currentGroup in securityGroups) {

                sqlStatement.Append ("'");
                sqlStatement.Append (currentGroup);
                sqlStatement.Append ("', ");

            }

            sqlStatement.Append (" '') ORDER BY RoleId");

            roleMembershipTable = EnvironmentDatabaseById (environmentId).SelectDataTable (sqlStatement.ToString ());

            foreach (System.Data.DataRow currentRole in roleMembershipTable.Rows) {

                roles.Add (currentRole["RoleId"].ToString ());

            }

            return roles;

        }


        public List<Mercury.Server.Environment.RolePermission> EnvironmentRoleGetPermissions (String environmentName, String roleName) {

            List<Mercury.Server.Environment.RolePermission> permissions = new List<Mercury.Server.Environment.RolePermission> ();

            SetLastException (null);

            if (String.IsNullOrEmpty (roleName)) { return permissions; }

            try {

                Environment.Role environmentRole = new Mercury.Server.Environment.Role (this, roleName);

                System.Data.DataTable rolePermissionTable;

                rolePermissionTable = EnvironmentDatabaseByName (environmentName).SelectDataTable ("SELECT * FROM dbo.RolePermission WHERE RoleId = " + environmentRole.RoleId.ToString ());

                foreach (System.Data.DataRow currentRow in rolePermissionTable.Rows) {

                    Environment.RolePermission rolePermission = new Mercury.Server.Environment.RolePermission ();

                    rolePermission.MapDataFields (currentRow);

                    permissions.Add (rolePermission);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return permissions;

        }

        public List<Mercury.Server.Environment.RolePermission> EnvironmentRoleGetPermissions (Int64 roleId) {

            List<Mercury.Server.Environment.RolePermission> permissions = new List<Mercury.Server.Environment.RolePermission> ();

            SetLastException (null);

            try {

                System.Data.DataTable rolePermissionTable;

                rolePermissionTable = EnvironmentDatabase.SelectDataTable ("SELECT * FROM dbo.RolePermission WHERE RoleId = " + roleId.ToString ());

                foreach (System.Data.DataRow currentRow in rolePermissionTable.Rows) {

                    Environment.RolePermission rolePermission = new Mercury.Server.Environment.RolePermission ();

                    rolePermission.MapDataFields (currentRow);

                    permissions.Add (rolePermission);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return permissions;

        }


        public List<Environment.RoleMembership> EnvironmentRoleGetMembership (String environmentName, String roleName) {

            List<Environment.RoleMembership> membership = new List<Mercury.Server.Environment.RoleMembership> ();

            SetLastException (null);

            try {

                System.Data.DataTable membershipTable;

                StringBuilder sqlStatement = new StringBuilder ();

                sqlStatement.Append ("SELECT * FROM dbo.RoleMembership WHERE RoleId = " + EnvironmentRoleGet (environmentName, roleName).RoleId.ToString ());

                membershipTable = EnvironmentDatabaseByName (environmentName).SelectDataTable (sqlStatement.ToString ());

                foreach (System.Data.DataRow currentRow in membershipTable.Rows) {

                    Environment.RoleMembership roleMembership = new Mercury.Server.Environment.RoleMembership (this);

                    roleMembership.MapDataFields (currentRow);

                    membership.Add (roleMembership);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return membership;

        }

        public List<Environment.RoleMembership> EnvironmentRoleGetMembership (Int64 roleId) {

            List<Environment.RoleMembership> membership = new List<Mercury.Server.Environment.RoleMembership> ();

            SetLastException (null);

            try {

                System.Data.DataTable membershipTable;

                StringBuilder sqlStatement = new StringBuilder ();

                sqlStatement.Append ("SELECT * FROM dbo.RoleMembership WHERE RoleId = " + roleId.ToString ());

                membershipTable = EnvironmentDatabase.SelectDataTable (sqlStatement.ToString ());

                foreach (System.Data.DataRow currentRow in membershipTable.Rows) {

                    Environment.RoleMembership roleMembership = new Mercury.Server.Environment.RoleMembership (this);

                    roleMembership.MapDataFields (currentRow);

                    membership.Add (roleMembership);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return membership;

        }


        #endregion

        #endregion


        #region Environment Roles

        public List<Environment.Role> EnvironmentRolesAvailable (String environmentName) {

            List<Environment.Role> roles = new List<Environment.Role> ();


            SetLastException (null);

            try {

                StringBuilder sqlStatement = new StringBuilder ("SELECT * FROM Role ORDER BY RoleName");

                System.Data.DataTable roleTable = EnvironmentDatabaseByName (environmentName).SelectDataTable (sqlStatement.ToString ());

                foreach (System.Data.DataRow currentRole in roleTable.Rows) {

                    Environment.Role role = new Environment.Role ();

                    role.MapDataFields (currentRole);

                    roles.Add (role);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return roles;

        }

        public Dictionary<Int64, String> EnvironmentRoleDictionary (Int64 environmentId) {

            Dictionary<Int64, String> roleDictionary = new Dictionary<Int64, String> ();

            SetLastException (null);

            try {

                StringBuilder sqlStatement = new StringBuilder ("SELECT RoleId, RoleName FROM Role ORDER BY RoleName");

                System.Data.DataTable roleTable = EnvironmentDatabaseById (environmentId).SelectDataTable (sqlStatement.ToString ());

                foreach (System.Data.DataRow currentRole in roleTable.Rows) {

                    roleDictionary.Add ((Int64) currentRole["RoleId"], (String) currentRole["RoleName"]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return roleDictionary;

        }

        public Dictionary<Int64, String> EnvironmentRoleDictionary (String environmentName) {

            Dictionary<Int64, String> roleDictionary = new Dictionary<Int64, String> ();

            SetLastException (null);

            try {

                StringBuilder sqlStatement = new StringBuilder ("SELECT RoleId, RoleName FROM Role ORDER BY RoleName");

                System.Data.DataTable roleTable = EnvironmentDatabaseByName (environmentName).SelectDataTable (sqlStatement.ToString ());

                foreach (System.Data.DataRow currentRole in roleTable.Rows) {

                    roleDictionary.Add ((Int64) currentRole["RoleId"], (String) currentRole["RoleName"]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return roleDictionary;

        }


        public Mercury.Server.Environment.Role EnvironmentRoleGet (String environmentName, String roleName) {

            if (String.IsNullOrEmpty (roleName)) { return null; }

            Mercury.Server.Environment.Role environmentRole = null;

            SetLastException (null);

            try {

                if (HasEnvironmentPermission (environmentName, Server.EnvironmentPermissions.RoleReview)) {

                    environmentRole = new Mercury.Server.Environment.Role (this, roleName);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return environmentRole;

        }


        public List<String> EnvironmentRoleMembershipListGet (Int64 environmentId, Int64 securityAuthorityId, List<String> securityGroups) {

            List<String> roles = new List<String> ();

            System.Data.DataTable roleMembershipTable;

            StringBuilder sqlStatement = new StringBuilder ();

            sqlStatement.Append ("SELECT DISTINCT Role.RoleId FROM Role JOIN RoleMembership ON Role.RoleId = RoleMembership.RoleId ");

            sqlStatement.Append ("  WHERE SecurityAuthorityId = " + securityAuthorityId.ToString () + " AND SecurityGroupId IN (");


            foreach (String currentGroup in securityGroups) {

                sqlStatement.Append ("'");
                sqlStatement.Append (currentGroup);
                sqlStatement.Append ("', ");

            }

            sqlStatement.Append (" '') ORDER BY RoleId");

            roleMembershipTable = EnvironmentDatabaseById (environmentId).SelectDataTable (sqlStatement.ToString ());

            foreach (System.Data.DataRow currentRole in roleMembershipTable.Rows) {

                roles.Add (currentRole["RoleId"].ToString ());

            }

            return roles;

        }

        public List<Environment.RoleMembership> EnvironmentRoleMembershipGet (String environmentName, String roleName) {

            List<Environment.RoleMembership> membership = new List<Mercury.Server.Environment.RoleMembership> ();

            SetLastException (null);

            try {

                System.Data.DataTable membershipTable;

                StringBuilder sqlStatement = new StringBuilder ();

                sqlStatement.Append ("SELECT * FROM dbo.RoleMembership WHERE RoleId = " + EnvironmentRoleGet (environmentName, roleName).RoleId.ToString ());

                membershipTable = EnvironmentDatabaseByName (environmentName).SelectDataTable (sqlStatement.ToString ());

                foreach (System.Data.DataRow currentRow in membershipTable.Rows) {

                    Environment.RoleMembership roleMembership = new Mercury.Server.Environment.RoleMembership (this);

                    roleMembership.MapDataFields (currentRow);

                    membership.Add (roleMembership);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return membership;

        }

        public List<Environment.RoleMembership> EnvironmentRoleMembershipGet (Int64 roleId) {

            List<Environment.RoleMembership> membership = new List<Mercury.Server.Environment.RoleMembership> ();

            SetLastException (null);

            try {

                System.Data.DataTable membershipTable;

                StringBuilder sqlStatement = new StringBuilder ();

                sqlStatement.Append ("SELECT * FROM dbo.RoleMembership WHERE RoleId = " + roleId.ToString ());

                membershipTable = EnvironmentDatabase.SelectDataTable (sqlStatement.ToString ());

                foreach (System.Data.DataRow currentRow in membershipTable.Rows) {

                    Environment.RoleMembership roleMembership = new Mercury.Server.Environment.RoleMembership (this);

                    roleMembership.MapDataFields (currentRow);

                    membership.Add (roleMembership);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return membership;

        }


        public List<Mercury.Server.Environment.RolePermission> EnvironmentRolePermissionsGet (String environmentName, String roleName) {

            List<Mercury.Server.Environment.RolePermission> permissions = new List<Mercury.Server.Environment.RolePermission> ();

            SetLastException (null);

            if (String.IsNullOrEmpty (roleName)) { return permissions; }

            try {

                Environment.Role environmentRole = new Mercury.Server.Environment.Role (this, roleName);

                System.Data.DataTable rolePermissionTable;

                rolePermissionTable = EnvironmentDatabaseByName (environmentName).SelectDataTable ("SELECT * FROM dbo.RolePermission WHERE RoleId = " + environmentRole.RoleId.ToString ());

                foreach (System.Data.DataRow currentRow in rolePermissionTable.Rows) {

                    Environment.RolePermission rolePermission = new Mercury.Server.Environment.RolePermission ();

                    rolePermission.MapDataFields (currentRow);

                    permissions.Add (rolePermission);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return permissions;

        }

        public List<Mercury.Server.Environment.RolePermission> EnvironmentRolePermissionsGet (Int64 roleId) {

            List<Mercury.Server.Environment.RolePermission> permissions = new List<Mercury.Server.Environment.RolePermission> ();

            SetLastException (null);

            try {

                System.Data.DataTable rolePermissionTable;

                rolePermissionTable = EnvironmentDatabase.SelectDataTable ("SELECT * FROM dbo.RolePermission WHERE RoleId = " + roleId.ToString ());

                foreach (System.Data.DataRow currentRow in rolePermissionTable.Rows) {

                    Environment.RolePermission rolePermission = new Mercury.Server.Environment.RolePermission ();

                    rolePermission.MapDataFields (currentRow);

                    permissions.Add (rolePermission);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return permissions;

        }


        public Boolean EnvironmentRoleSetPermission (String environmentName, String roleName, Int64 permissionId, Boolean isGranted, Boolean isDenied) {

            Environment.RolePermission rolePermission;

            Mercury.Server.Data.SqlDatabase localEnvironmentDatabase;

            Boolean success = false;

            try {

                rolePermission = new Mercury.Server.Environment.RolePermission ();

                rolePermission.RoleId = EnvironmentRoleGet (environmentName, roleName).RoleId;

                rolePermission.PermissionId = permissionId;

                rolePermission.IsGranted = false;

                rolePermission.IsDenied = false;

                if (isDenied) {

                    rolePermission.IsDenied = true;

                }

                else if (isGranted) {

                    rolePermission.IsGranted = true;

                }

                rolePermission.ModifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (this);

                localEnvironmentDatabase = EnvironmentDatabaseByName (environmentName);

                success = rolePermission.Save (localEnvironmentDatabase);

                if ((!success) && (localEnvironmentDatabase.LastException != null)) { SetLastException (localEnvironmentDatabase.LastException); }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

                success = false;

            }

            return success;

        }

        public Boolean EnvironmentRoleSetMembership (String environmentName, String roleName, List<Environment.RoleMembership> roleMembership) {

            Boolean success = true;

            Boolean memberExists = false;

            Mercury.Server.Data.SqlDatabase localEnvironmentDatabase = EnvironmentDatabaseByName (environmentName);

            try {

                localEnvironmentDatabase.BeginTransaction ();

                List<Environment.RoleMembership> originalMembership;

                originalMembership = EnvironmentRoleMembershipGet (environmentName, roleName);

                foreach (Environment.RoleMembership currentOriginalMember in originalMembership) {

                    memberExists = false;

                    foreach (Environment.RoleMembership currentNewMember in roleMembership) {

                        if (currentOriginalMember.IsEqual (currentNewMember)) { memberExists = true; break; }

                    }

                    if (!memberExists) {

                        success = currentOriginalMember.Delete (localEnvironmentDatabase);

                    }

                    if (!success) { break; }

                }

                if (success) {

                    foreach (Environment.RoleMembership currentNewMember in roleMembership) {

                        memberExists = false;

                        foreach (Environment.RoleMembership currentOriginalMember in originalMembership) {

                            if (currentOriginalMember.IsEqual (currentNewMember)) { memberExists = true; } break;
                        }

                        if (!memberExists) {

                            currentNewMember.ModifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (this);

                            success = currentNewMember.Save (localEnvironmentDatabase);

                        }

                        if (!success) { break; }

                    }

                }

                if (success) {

                    localEnvironmentDatabase.CommitTransaction ();

                }

                else {

                    if (localEnvironmentDatabase.LastException != null) { throw new ApplicationException (localEnvironmentDatabase.LastException.Message); }

                    else { throw new ApplicationException ("Unable to update Role Membership."); }

                }

            }

            catch (Exception applicationException) {

                localEnvironmentDatabase.RollbackTransaction ();

                SetLastException (applicationException);

                success = false;

            }

            return success;

        }

        #endregion



        #region Core Objects

        private String CoreObjectTableName (String forObjectType) {

            String objectTableName = forObjectType;

            // SUPPORT DAL VERSUS DBO LOOK UPS 


            switch (forObjectType) {

                case "AuthorizationType":

                case "BenefitPlan":

                case "Citizenship":

                case "Contract":

                case "Ethnicity":

                case "Insurer":

                case "Language":

                case "MaritalStatus":

                case "Member":

                case "Program":

                case "Sponsor":

                    objectTableName = "dal." + forObjectType;

                    break;

                default: objectTableName = forObjectType; break;

            }

            return objectTableName;

        }

        public Dictionary<Int64, String> CoreObjectDictionary (String forObjectType) {

            Dictionary<Int64, String> objectDictionary = new Dictionary<Int64, String> ();

            ClearLastException ();


            try {

                String selectStatement = "SELECT " + forObjectType + "Id AS ObjectId, " + forObjectType + "Name AS ObjectName FROM " + CoreObjectTableName (forObjectType) + " ORDER BY ObjectName";

                System.Data.DataTable objectTable = EnvironmentDatabase.SelectDataTable (selectStatement);

                foreach (System.Data.DataRow currentRow in objectTable.Rows) {

                    objectDictionary.Add ((Int64) currentRow["ObjectId"], (String) currentRow["ObjectName"]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return objectDictionary;

        }

        public Dictionary<Int64, String> CoreObjectDictionaryByEnvironment (String forObjectType, Int64 environmentId) {

            Dictionary<Int64, String> objectDictionary = new Dictionary<Int64, String> ();

            ClearLastException ();


            try {

                String selectStatement = "SELECT " + forObjectType + "Id AS ObjectId, " + forObjectType + "Name AS ObjectName FROM " + CoreObjectTableName (forObjectType) + " ORDER BY ObjectName";

                System.Data.DataTable objectTable = EnvironmentDatabaseById (environmentId).SelectDataTable (selectStatement);

                foreach (System.Data.DataRow currentRow in objectTable.Rows) {

                    objectDictionary.Add ((Int64)currentRow["ObjectId"], (String)currentRow["ObjectName"]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return objectDictionary;

        }

        public Dictionary<Int64, String> CoreObjectDictionaryEnterprise (String forObjectType) {

            Dictionary<Int64, String> objectDictionary = new Dictionary<Int64, String> ();

            ClearLastException ();


            try {

                String selectStatement = "SELECT " + forObjectType + "Id AS ObjectId, " + forObjectType + "Name AS ObjectName FROM " + forObjectType + " ORDER BY ObjectName";

                System.Data.DataTable objectTable = EnterpriseDatabase.SelectDataTable (selectStatement);

                foreach (System.Data.DataRow currentRow in objectTable.Rows) {

                    objectDictionary.Add ((Int64) currentRow["ObjectId"], (String) currentRow["ObjectName"]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return objectDictionary;

        }

        public Dictionary<Int64, Core.CoreConfigurationObject> CoreConfigurationObjectDictionary (String forObjectType) {

            Dictionary<Int64, Core.CoreConfigurationObject> objectDictionary = new Dictionary<Int64, Core.CoreConfigurationObject> ();

            ClearLastException ();


            try {

                String selectStatement = "SELECT "; 
                
                selectStatement +=  forObjectType + "Id AS CoreConfigurationObjectId, ";
                
                selectStatement +=  forObjectType + "Name AS CoreConfigurationObjectName, ";

                selectStatement +=  forObjectType + "Description AS CoreConfigurationObjectDescription, ";

                selectStatement +=  "Enabled, ";

                selectStatement +=  "Visible ";

                selectStatement += "  FROM " + forObjectType + " ORDER BY CoreConfigurationObjectName";

                System.Data.DataTable objectTable = EnvironmentDatabase.SelectDataTable (selectStatement);

                foreach (System.Data.DataRow currentRow in objectTable.Rows) {

                    Core.CoreConfigurationObject configurationObject = new Core.CoreConfigurationObject ();

                    configurationObject.MapDataFields (currentRow);

                    objectDictionary.Add ((Int64) currentRow["CoreConfigurationObjectId"], configurationObject);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return objectDictionary;

        }


        public Int64 CoreObjectGetIdByName (String forObjectType, String forObjectName) {

            if (String.IsNullOrWhiteSpace (forObjectName)) { return 0; }


            Int64 objectId = 0;

            ClearLastException ();


            try {

                objectId = Convert.ToInt64 (EnvironmentDatabase.LookupValue (CoreObjectTableName (forObjectType), forObjectType + "Id", forObjectType + "Name = '" + forObjectName.Replace ("'", "''") + "'", 0));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return objectId;

        }

        public String CoreObjectGetNameById (String forObjectType, Int64 forObjectId) {

            if (forObjectId == 0) { return String.Empty; }


            String objectName = String.Empty;

            ClearLastException ();


            try {

                objectName = (String)EnvironmentDatabase.LookupValue (CoreObjectTableName (forObjectType), forObjectType + "Name", forObjectType + "Id = " + forObjectId.ToString (), String.Empty);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return objectName;

        }

        #endregion 


        #region Actions

        public List<Core.Action.Action> ActionsAvailable () {

            List<Core.Action.Action> actions = new List<Mercury.Server.Core.Action.Action> ();

            Core.Action.Action standardAction;


            #region Add Standard Actions

            standardAction = new Mercury.Server.Core.Action.Action (this, (Int64) Core.Action.Enumerations.StandardActions.Workflow, "Workflow");

            standardAction.DescribingParameterName = "Workflow";

            standardAction.AddParameter ("Workflow", Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.Workflow, false);

            actions.Add (standardAction);


            standardAction = new Mercury.Server.Core.Action.Action (this, (Int64) Core.Action.Enumerations.StandardActions.AddToWorkQueue, "Add to Work Queue");

            standardAction.DescribingParameterName = "WorkQueue";

            standardAction.AddParameter ("WorkQueue", Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.WorkQueue, false);

            standardAction.AddParameter ("EntityId", Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.EntityId, false);

            standardAction.AddParameter ("ItemGroupKey", Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.String, true, false);

            standardAction.AddParameter ("Priority", Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.Integer, true, false);

            actions.Add (standardAction);


            standardAction = new Mercury.Server.Core.Action.Action (this, (Int64) Core.Action.Enumerations.StandardActions.RouteByRules, "Route by Routing Rules");

            standardAction.DescribingParameterName = "RoutingRule";

            standardAction.AddParameter ("RoutingRule", Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.RoutingRule, false);

            standardAction.AddParameter ("MemberId", Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.MemberId, false);

            standardAction.AddParameter ("ItemGroupKey", Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.String, true, false);

            standardAction.AddParameter ("Priority", Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.Integer, true, false);

            actions.Add (standardAction);


            standardAction = new Mercury.Server.Core.Action.Action (this, (Int64) Core.Action.Enumerations.StandardActions.SendCorrespondence, "Send Correspondence");

            standardAction.DescribingParameterName = "Correspondence";

            standardAction.AddParameter ("Correspondence", Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.Correspondence, false);

            standardAction.AddParameter ("EntityId", Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.EntityId, false);

            actions.Add (standardAction);


            //standardAction = new Mercury.Server.Core.Action.Action (this, (Int64) Core.Action.Enumerations.StandardActions.SendCorrespondenceProvider, "Send Correspondence to Provider");

            //standardAction.DescribingParameterName = "Correspondence";

            //standardAction.AddParameter ("Correspondence", Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.Correspondence, false);

            //standardAction.AddParameter ("ProviderId", Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.ProviderId, false);

            //standardAction.AddParameter ("RegardingMemberId", Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.MemberId, false, false);

            //actions.Add (standardAction);


            standardAction = new Mercury.Server.Core.Action.Action (this, (Int64) Core.Action.Enumerations.StandardActions.SendCorrespondenceMemberAndRoute, "Send Correspondence to Member and Route by Routing Rules");

            standardAction.DescribingParameterName = "RoutingRule";

            standardAction.AddParameter ("Correspondence", Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.Correspondence, false);

            standardAction.AddParameter ("RoutingRule", Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.RoutingRule, false);

            standardAction.AddParameter ("MemberId", Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.MemberId, false);

            standardAction.AddParameter ("ItemGroupKey", Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.String, true, false);

            standardAction.AddParameter ("Priority", Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.Integer, true, false);

            actions.Add (standardAction);


            standardAction = new Mercury.Server.Core.Action.Action (this, (Int64) Core.Action.Enumerations.StandardActions.RemoveFromWorkQueue, "Remove from Work Queue");

            standardAction.DescribingParameterName = "WorkQueue";

            standardAction.AddParameter ("WorkQueue", Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.WorkQueue, false);

            standardAction.AddParameter ("EntityId", Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.EntityId, false);

            standardAction.AddParameter ("WorkOutcome", Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.WorkOutcome, false);

            actions.Add (standardAction);

            #endregion


            return actions;

        }

        public Core.Action.Action ActionById (Int64 actionId) {

            List<Core.Action.Action> availableActions = ActionsAvailable ();

            Core.Action.Action action = null;


            foreach (Core.Action.Action currentAction in availableActions) {

                if (currentAction.Id == actionId) { action = currentAction; break; }

            }

            return action;

        }

        #endregion


        #region Core.Reference

        #region Reference - Contact Regarding

        public List<Core.Reference.ContactRegarding> ContactRegardingsAvailable (Boolean useCaching = false) {

            String cacheKey = "Application." + session.EnvironmentId + ".ContactRegarding.Available";

            String itemCacheKey = "Application." + session.EnvironmentId + ".ContactRegarding.";

            List<Core.Reference.ContactRegarding> items = new List<Core.Reference.ContactRegarding> ();

            String selectStatement = "SELECT * FROM dbo.ContactRegarding ORDER BY ContactRegardingName";

            SetLastException (null);


            try {

                items = (List<Core.Reference.ContactRegarding>)CacheManager.GetObject (cacheKey);

                if (!useCaching) { items = null; }

                if (items == null) {

                    items = new List<Core.Reference.ContactRegarding> ();

                    System.Data.DataTable contactRegardingTable = EnvironmentDatabase.SelectDataTable (selectStatement);

                    foreach (System.Data.DataRow currentRow in contactRegardingTable.Rows) {

                        Core.Reference.ContactRegarding item = new Mercury.Server.Core.Reference.ContactRegarding (this);

                        item.MapDataFields (currentRow);

                        items.Add (item);


                        // CACHE ITEM BY ID AND NAME

                        CacheManager.CacheObject (itemCacheKey + item.Id.ToString (), item, CacheExpirationReference);

                        CacheManager.CacheObject (itemCacheKey + item.Name, item, CacheExpirationReference);

                    }

                    if (items.Count > 0) { CacheManager.CacheObject (cacheKey, items, CacheExpirationReference); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return items;

        }

        public List<Core.Reference.ContactRegarding> ContactRegardingsVisibleEnabled (Boolean useCaching = false) {

            String cacheKey = "Application." + session.EnvironmentId + ".ContactRegarding.VisibleEnabled";

            List<Core.Reference.ContactRegarding> items = new List<Core.Reference.ContactRegarding> ();


            ClearLastException ();            

            try {

                items = (List<Core.Reference.ContactRegarding>)CacheManager.GetObject (cacheKey);

                if (!useCaching) { items = null; }

                if (items == null) {

                    items = new List<Core.Reference.ContactRegarding> ();


                    // USE THE AVAILABLE FUNCTION TO CYCLE THROUGH ALL ITEMS (THIS ALLOWS THE CACHING TO PASSTHROUGH)

                    foreach (Core.Reference.ContactRegarding currentItem in ContactRegardingsAvailable (useCaching)) {

                        if ((currentItem.Visible) && (currentItem.Enabled)) {

                            items.Add (currentItem);

                        }

                    }

                }

                if (items.Count > 0) { CacheManager.CacheObject (cacheKey, items, CacheExpirationReference); }
                    
            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return items;

        }

        public Dictionary<Int64, String> ContactRegardingDictionary (Boolean useCaching = false) {

            String cacheKey = "Application." + session.EnvironmentId + ".ContactRegarding.Dictionary";

            Dictionary<Int64, String> itemDictionary = new Dictionary<Int64, String> ();

            
            ClearLastException ();

            try {

                itemDictionary = (Dictionary<Int64, String>)CacheManager.GetObject (cacheKey);

                if (!useCaching) { itemDictionary = null; }

                if (itemDictionary == null) {

                    itemDictionary = new Dictionary<Int64, String> ();

                   
                    // USE THE AVAILABLE FUNCTION TO CYCLE THROUGH ALL ITEMS (THIS ALLOWS THE CACHING TO PASSTHROUGH)

                    // THIS WILL BE SLOWER THAN A DIRECT DATA READ OF THE DATA DUE TO OBJECT CREATION, 
                    
                    // UNLESS THE OBJECTS ARE ALREADY CACHED THROUGH THE AVAILABLE FUNCTION

                    foreach (Core.Reference.ContactRegarding currentItem in ContactRegardingsAvailable (useCaching)) {

                        itemDictionary.Add (currentItem.Id, currentItem.Name);

                    }

                    if (itemDictionary.Count > 0) { CacheManager.CacheObject (cacheKey, itemDictionary, CacheExpirationReference); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return itemDictionary;

        }

        public Core.Reference.ContactRegarding ContactRegardingGet (Int64 id, Boolean useCaching = false) {

            if (id == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".ContactRegarding." + id.ToString ();

            Core.Reference.ContactRegarding contactRegarding = null;


            ClearLastException ();

            try {

                contactRegarding = (Core.Reference.ContactRegarding)CacheManager.GetObject (cacheKey);

                if (!useCaching) { contactRegarding = null; }

                if (contactRegarding == null) {

                    contactRegarding = new Core.Reference.ContactRegarding (this, id);

                    CacheManager.CacheObject (cacheKey, contactRegarding, CacheExpirationReference);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return contactRegarding;

        }

        public Core.Reference.ContactRegarding ContactRegardingGet (String name, Boolean useCaching = false) {

            if (String.IsNullOrWhiteSpace (name)) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".ContactRegarding." + name;

            Core.Reference.ContactRegarding contactRegarding = null;


            ClearLastException ();

            try {

                contactRegarding = (Core.Reference.ContactRegarding)CacheManager.GetObject (cacheKey);

                if (!useCaching) { contactRegarding = null; }

                if (contactRegarding == null) {

                    Int64 contactRegardingId = ContactRegardingGetIdByName (name);

                    if (contactRegardingId != 0) { 
                        
                        contactRegarding = ContactRegardingGet (contactRegardingId);

                        CacheManager.CacheObject (cacheKey, contactRegarding, CacheExpirationReference);
                    
                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return contactRegarding;

        }

        public Int64 ContactRegardingGetIdByName (String contactRegardingName) {

            if (String.IsNullOrWhiteSpace (contactRegardingName)) { return 0; }


            Int64 contactRegardingId = 0;

            SetLastException (null);

            try {

                contactRegardingId = Convert.ToInt64 (EnvironmentDatabase.LookupValue ("ContactRegarding", "ContactRegardingId", "ContactRegardingName = '" + contactRegardingName + "'"));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return contactRegardingId;

        }

        /// <summary>
        /// Get the Note Type Name by a given Note Type Id
        /// </summary>
        /// <param name="contactRegardingId">Unique Identifier for the Note Type.</param>
        /// <returns>Note Type Name</returns>
        public String ContactRegardingGetNameById (Int64 contactRegardingId) {

            if (contactRegardingId == 0) { return String.Empty; }


            String contactRegardingName = String.Empty;

            SetLastException (null);

            try {

                contactRegardingName = (String) EnvironmentDatabase.LookupValue ("ContactRegarding", "ContactRegardingName", "ContactRegardingId = " + contactRegardingId.ToString (), String.Empty);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return contactRegardingName;

        }
        
        #endregion 


        #region Reference - Correspondence

        public List<Core.Reference.Correspondence> CorrespondencesAvailable () {

            List<Core.Reference.Correspondence> results = new List<Mercury.Server.Core.Reference.Correspondence> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("SELECT * FROM Correspondence ");

            selectStatement.Append ("  ORDER BY CorrespondenceName");


            System.Data.DataTable correspondenceTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in correspondenceTable.Rows) {

                Mercury.Server.Core.Reference.Correspondence correspondence = new Mercury.Server.Core.Reference.Correspondence (this);

                correspondence.MapDataFields (currentRow);

                results.Add (correspondence);

            }

            return results;

        }

        public Core.Reference.Correspondence CorrespondenceGet (Int64 correspondenceId) {

            Core.Reference.Correspondence correspondence = null;

            SetLastException (null);

            try {

                correspondence = new Mercury.Server.Core.Reference.Correspondence (this, correspondenceId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return correspondence;

        }

        public Core.Reference.Correspondence CorrespondenceGet (String correspondenceName) {

            Int64 correspondenceId = CorrespondenceGetIdByName (correspondenceName);

            Core.Reference.Correspondence correspondence = null;

            if (correspondenceId != 0) { correspondence = CorrespondenceGet (correspondenceId); }

            return correspondence;

        }

        public Int64 CorrespondenceGetIdByName (String correspondenceName) {

            if (String.IsNullOrEmpty (correspondenceName)) { return 0; }

            Int64 correspondenceId = 0;

            SetLastException (null);

            try {

                correspondenceId = Convert.ToInt64 (EnvironmentDatabase.LookupValue ("Correspondence", "CorrespondenceId", "CorrespondenceName = '" + correspondenceName.Replace ("'", "''") + "'", 0));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return correspondenceId;

        }

        public String CorrespondenceGetNameById (Int64 correspondenceId) {

            if (correspondenceId == 0) { return String.Empty; }

            String correspondenceName = String.Empty;

            SetLastException (null);

            try {

                correspondenceName = (String) EnvironmentDatabase.LookupValue ("Correspondence", "CorrespondenceName", "CorrespondenceId = " + correspondenceId.ToString (), String.Empty);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return correspondenceName;

        }


        public Core.Reference.CorrespondenceContent CorrespondenceContentGet (Int64 correspondenceContentId) {

            Core.Reference.CorrespondenceContent correspondenceContent = null;

            SetLastException (null);

            try {

                correspondenceContent = new Mercury.Server.Core.Reference.CorrespondenceContent (this, correspondenceContentId);

                correspondenceContent.LoadAttachment ();

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return correspondenceContent;

        }

        #endregion 


        #region Reference - Note Type

        public List<Core.Reference.NoteType> NoteTypesAvailable () {

            List<Core.Reference.NoteType> results = new List<Core.Reference.NoteType> ();

            StringBuilder selectStatement = new StringBuilder ();

            ClearLastException ();


            selectStatement.Append ("SELECT * FROM dbo.NoteType ORDER BY NoteTypeName");


            System.Data.DataTable noteTypeTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in noteTypeTable.Rows) {

                Core.Reference.NoteType noteType = new Mercury.Server.Core.Reference.NoteType (this);

                noteType.MapDataFields (currentRow);

                results.Add (noteType);

            }

            return results;

        }

        public Int64 NoteTypeGetIdByName (String noteTypeName) { return CoreObjectGetIdByName ("NoteType", noteTypeName); }

        public String NoteTypeGetNameById (Int64 noteTypeId) { return CoreObjectGetNameById ("NoteType", noteTypeId); }


        public Core.Reference.NoteType NoteTypeGet (Int64 noteTypeId) {

            if (noteTypeId == 0) { return null; }


            Core.Reference.NoteType noteType = null;

            ClearLastException ();

            try {

                noteType = new Mercury.Server.Core.Reference.NoteType (this, noteTypeId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return noteType;

        }

        public Core.Reference.NoteType NoteTypeGet (String noteTypeName) {

            if (String.IsNullOrWhiteSpace (noteTypeName)) { return null; }


            Int64 noteTypeId = CoreObjectGetIdByName ("NoteType", noteTypeName);

            Core.Reference.NoteType noteType = NoteTypeGet (noteTypeId);

            return noteType;

        }

        #endregion 


        #region Geographic Reference

        public List<String> StateReference () {

            List<String> referenceResponse = new List<String> ();

            SetLastException (null);

            try {

                System.Data.DataTable referenceTable = EnvironmentDatabase.SelectDataTable ("SELECT DISTINCT State FROM dal.ZipCode AS ZipCode ORDER BY State");

                foreach (System.Data.DataRow currentRow in referenceTable.Rows) {

                    referenceResponse.Add ((String)currentRow[0]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return referenceResponse;

        }

        public String StateReferenceByZipCode (String zipCode) {

            String state = String.Empty;

            SetLastException (null);

            try {

                System.Data.DataTable referenceTable = EnvironmentDatabase.SelectDataTable ("SELECT DISTINCT TOP 1 State FROM dal.ZipCode AS ZipCode WHERE ZipCode = '" + zipCode + "' ORDER BY State");

                foreach (System.Data.DataRow currentRow in referenceTable.Rows) {

                    state = (String)currentRow[0];

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return state;

        }

        public String CityReferenceByZipCode (String zipCode) {

            String city = String.Empty;

            SetLastException (null);

            try {

                System.Data.DataTable referenceTable = EnvironmentDatabase.SelectDataTable ("SELECT DISTINCT TOP 1 City FROM dal.ZipCode AS ZipCode WHERE ZipCode = '" + zipCode + "' ORDER BY City");

                foreach (System.Data.DataRow currentRow in referenceTable.Rows) {

                    city = ((String)currentRow[0]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return city;

        }

        public List<String> CityReferenceByState (String state, String cityNamePrefix) {

            List<String> referenceResponse = new List<String> ();

            SetLastException (null);

            try {

                System.Data.DataTable referenceTable = EnvironmentDatabase.SelectDataTable ("SELECT DISTINCT City FROM dal.ZipCode AS ZipCode WHERE State = '" + state + "' AND City LIKE '" + cityNamePrefix + "%' ORDER BY City");

                foreach (System.Data.DataRow currentRow in referenceTable.Rows) {

                    referenceResponse.Add ((String)currentRow[0]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return referenceResponse;

        }

        public List<String> CountyReferenceByState (String state) {

            List<String> referenceResponse = new List<String> ();

            SetLastException (null);

            try {

                System.Data.DataTable referenceTable = EnvironmentDatabase.SelectDataTable ("SELECT DISTINCT County FROM dal.ZipCode AS ZipCode WHERE State = '" + state + "' ORDER BY County");

                foreach (System.Data.DataRow currentRow in referenceTable.Rows) {

                    referenceResponse.Add ((String)currentRow[0]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return referenceResponse;

        }

        public String CountyReferenceByZipCode (String zipCode) {

            String county = String.Empty;

            SetLastException (null);

            try {

                System.Data.DataTable referenceTable = EnvironmentDatabase.SelectDataTable ("SELECT DISTINCT TOP 1 County FROM dal.ZipCode AS ZipCode WHERE ZipCode = '" + zipCode + "' ORDER BY County");

                foreach (System.Data.DataRow currentRow in referenceTable.Rows) {

                    county = ((String)currentRow[0]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return county;

        }


        public Core.Reference.Views.CityStateZipCodeView CityStateReferenceByZipCode (String zipCode) {

            Core.Reference.Views.CityStateZipCodeView cityStateZipCode = new Core.Reference.Views.CityStateZipCodeView ();

            SetLastException (null);

            try {

                System.Data.DataTable referenceTable = EnvironmentDatabase.SelectDataTable ("SELECT DISTINCT TOP 1 * FROM dal.ZipCode AS ZipCode WHERE ZipCode = '" + zipCode + "' ORDER BY City");

                foreach (System.Data.DataRow currentRow in referenceTable.Rows) {

                    cityStateZipCode = new Core.Reference.Views.CityStateZipCodeView ((String)currentRow["City"], (String)currentRow["State"], (String)currentRow["ZipCode"], String.Empty, String.Empty, (String)currentRow["County"]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return cityStateZipCode;

        }

        public List<Core.Reference.Views.CityStateZipCodeView> CityReferenceByState (String state) {

            List<Core.Reference.Views.CityStateZipCodeView> referenceData = new List<Core.Reference.Views.CityStateZipCodeView> ();

            SetLastException (null);

            try {

                System.Data.DataTable referenceTable = EnvironmentDatabase.SelectDataTable ("SELECT DISTINCT City, State, '' AS ZipCode, '' AS County FROM dal.ZipCode WHERE State = '" + state + "' ORDER BY City");

                foreach (System.Data.DataRow currentRow in referenceTable.Rows) {

                    Core.Reference.Views.CityStateZipCodeView cityStateZipCode = new Core.Reference.Views.CityStateZipCodeView ((String)currentRow["City"], (String)currentRow["State"], (String)currentRow["ZipCode"], String.Empty, String.Empty, (String)currentRow["County"]);

                    referenceData.Add (cityStateZipCode);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return referenceData;

        }

        #endregion 

        #endregion


        #region Reference Data Old

        public System.Collections.Generic.Dictionary<String, String> DiagnosisDictionary (String diagnosisPrefix, Int32 version = 9) {

            System.Collections.Generic.Dictionary<String, String> collection = new Dictionary<String, String> ();

            SetLastException (null);

            try {

                System.Data.DataTable diagnosisTable = EnvironmentDatabase.SelectDataTable ("SELECT * FROM dal.DiagnosisCode WHERE DiagnosisCode LIKE '" + diagnosisPrefix + "%' AND DiagnosisVersion = " + version.ToString ());

                foreach (System.Data.DataRow currentRow in diagnosisTable.Rows) {

                    collection.Add ((String) currentRow["DiagnosisCode"], (String) currentRow["DiagnosisName"]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return collection;

        }

        public String DiagnosisDescription (String diagnosisCode, Int32 version = 9) {

            String description = String.Empty;

            SetLastException (null);

            try {

                description = (String) EnvironmentDatabase.LookupValue ("dal.DiagnosisCode", "DiagnosisName", "DiagnosisCode = '" + diagnosisCode + "' AND DiagnosisVersion = " + version.ToString ());

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return description;
        }

        public System.Collections.Generic.Dictionary<String, String> ProcedureCodeDictionary (String procedureCodePrefix) {

            System.Collections.Generic.Dictionary<String, String> collection = new Dictionary<String, String> ();

            SetLastException (null);

            try {

                System.Data.DataTable diagnosisTable = EnvironmentDatabase.SelectDataTable ("SELECT * FROM dal.ProcedureCode WHERE ProcedureCode LIKE '" + procedureCodePrefix + "%'");

                foreach (System.Data.DataRow currentRow in diagnosisTable.Rows) {

                    collection.Add ((String) currentRow["ProcedureCodeName"], (String) currentRow["ProcedureCodeName"]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return collection;

        }

        public System.Collections.Generic.Dictionary<String, String> RevenueCodeDictionary (String revenueCodePrefix) {

            System.Collections.Generic.Dictionary<String, String> collection = new Dictionary<String, String> ();

            SetLastException (null);

            try {

                System.Data.DataTable diagnosisTable = EnvironmentDatabase.SelectDataTable ("SELECT * FROM dal.RevenueCode WHERE RevenueCode LIKE '" + revenueCodePrefix + "%'");

                foreach (System.Data.DataRow currentRow in diagnosisTable.Rows) {

                    collection.Add ((String) currentRow["RevenueCode"], (String) currentRow["RevenueName"]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return collection;

        }

        public System.Collections.Generic.Dictionary<String, String> BillTypeDictionary (String billTypePrefix) {

            System.Collections.Generic.Dictionary<String, String> collection = new Dictionary<String, String> ();

            SetLastException (null);

            try {

                System.Data.DataTable diagnosisTable = EnvironmentDatabase.SelectDataTable ("SELECT * FROM dal.BillType WHERE BillType LIKE '" + billTypePrefix + "%'");

                foreach (System.Data.DataRow currentRow in diagnosisTable.Rows) {

                    collection.Add ((String) currentRow["BillType"], (String) currentRow["BillTypeName"]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return collection;

        }

        public System.Collections.Generic.Dictionary<String, String> Icd9ProcedureCodeDictionary (String icd9ProcedureCodePrefix) {

            System.Collections.Generic.Dictionary<String, String> collection = new Dictionary<String, String> ();

            SetLastException (null);

            try {

                System.Data.DataTable diagnosisTable = EnvironmentDatabase.SelectDataTable ("SELECT * FROM dal.Icd9ProcedureCode WHERE Icd9ProcedureCode LIKE '" + icd9ProcedureCodePrefix + "%'");

                foreach (System.Data.DataRow currentRow in diagnosisTable.Rows) {

                    collection.Add ((String) currentRow["Icd9ProcedureCode"], (String) currentRow["Icd9ProcedureName"]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return collection;

        }

        #endregion


        #region Authorizations

        public List<Core.Authorizations.AuthorizationType> AuthorizationTypesAvailable () {

            List<Core.Authorizations.AuthorizationType> results = new List<Core.Authorizations.AuthorizationType> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("SELECT ");

            selectStatement.Append ("    AuthorizationType.* ");

            selectStatement.Append ("  FROM dal.AuthorizationType AS AuthorizationType");

            selectStatement.Append ("  ORDER BY CategoryName, SubcategoryName, ServiceTypeName");


            System.Data.DataTable resultTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in resultTable.Rows) {

                Core.Authorizations.AuthorizationType authorizationType = new Server.Core.Authorizations.AuthorizationType (this);

                authorizationType.MapDataFields (currentRow);

                results.Add (authorizationType);

            }

            return results;

        }

        public Core.Authorizations.AuthorizationType AuthorizationTypeGet (Int64 authorizationTypeId) {

            if (authorizationTypeId == 0) { return null; }

            Core.Authorizations.AuthorizationType authorizationType = null;

            SetLastException (null);

            try {

                if (authorizationTypeId != 0) {

                    authorizationType = new Server.Core.Authorizations.AuthorizationType (this, authorizationTypeId);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return authorizationType;

        }


        #region Member Authorizations

        public Int64 MemberAuthorizationsGetCount (Int64 memberId) {

            String sqlStatement;

            Int64 authorizationCount = 0;

            SetLastException (null);

            try {

                sqlStatement = "dal.Authorization_CountByMember " + memberId.ToString () + " ";

                authorizationCount = Convert.ToInt64 (EnvironmentDatabase.StoredProcedureReturnValue (sqlStatement));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return authorizationCount;

        }

        public List<Core.Authorizations.Authorization> MemberAuthorizationsGetByPage (Int64 memberId, Int32 initialRow, Int32 count) {

            List<Core.Authorizations.Authorization> memberAuthorizations = new List<Server.Core.Authorizations.Authorization> ();

            System.Data.DataTable authorizationTable;

            String sqlStatement;


            SetLastException (null);

            try {

                sqlStatement = "EXEC dal.Authorization_SelectByMemberPage " + memberId.ToString () + ", " + initialRow.ToString () + ", " + count.ToString ();

                authorizationTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in authorizationTable.Rows) {

                    Core.Authorizations.Authorization authorization = new Server.Core.Authorizations.Authorization ();

                    authorization.MapDataFields (currentRow);

                    memberAuthorizations.Add (authorization);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberAuthorizations;

        }

        public List<Core.Authorizations.AuthorizationLine> AuthorizationLinesGet (Int64 authorizationId) {

            List<Core.Authorizations.AuthorizationLine> authorizationLines = new List<Server.Core.Authorizations.AuthorizationLine> ();

            System.Data.DataTable authorizationLineTable;

            String sqlStatement;


            SetLastException (null);

            try {

                sqlStatement = "EXEC dal.AuthorizationLine_SelectByAuthorizationId " + authorizationId.ToString ();

                authorizationLineTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in authorizationLineTable.Rows) {

                    Core.Authorizations.AuthorizationLine authorizationLine = new Server.Core.Authorizations.AuthorizationLine ();

                    authorizationLine.MapDataFields (currentRow);

                    authorizationLines.Add (authorizationLine);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return authorizationLines;

        }

        #endregion

        #endregion


        #region Core.Authorized Services

        public List<Core.AuthorizedServices.AuthorizedService> AuthorizedServicesAvailable () {

            List<Core.AuthorizedServices.AuthorizedService> results = new List<Mercury.Server.Core.AuthorizedServices.AuthorizedService> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("SELECT * FROM AuthorizedService");

            selectStatement.Append ("  ORDER BY AuthorizedServiceName");


            System.Data.DataTable authorizedServiceTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in authorizedServiceTable.Rows) {

                Mercury.Server.Core.AuthorizedServices.AuthorizedService authorizedService = new Mercury.Server.Core.AuthorizedServices.AuthorizedService (this);

                authorizedService.MapDataFields (currentRow);

                results.Add (authorizedService);

            }

            return results;

        }

        public Core.AuthorizedServices.AuthorizedService AuthorizedServiceGet (Int64 authorizedServiceId) {

            if (authorizedServiceId == 0) { return null; }

            Core.AuthorizedServices.AuthorizedService authorizedService = null;

            SetLastException (null);

            try {

                authorizedService = new Mercury.Server.Core.AuthorizedServices.AuthorizedService (this, authorizedServiceId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return authorizedService;

        }

        public Core.AuthorizedServices.AuthorizedService AuthorizedServiceGet (String authorizedServiceName) {

            Int64 authorizedServiceId = AuthorizedServiceGetIdByName (authorizedServiceName);

            Core.AuthorizedServices.AuthorizedService authorizedService = null;

            if (authorizedServiceId != 0) { authorizedService = AuthorizedServiceGet (authorizedServiceId); }

            return authorizedService;

        }

        public Int64 AuthorizedServiceGetIdByName (String authorizedServiceName) {

            if (String.IsNullOrEmpty (authorizedServiceName)) { return 0; }

            Int64 authorizedServiceId = 0;

            SetLastException (null);

            try {

                authorizedServiceId = Convert.ToInt64 (EnvironmentDatabase.LookupValue ("AuthorizedService", "AuthorizedServiceId", "AuthorizedServiceName= '" + authorizedServiceName + "'"));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return authorizedServiceId;

        }


        #region Member Authorized Services

        public Int64 MemberAuthorizedServiceGetId (Int64 forMemberId, Int64 forAuthorizedServiceId, DateTime eventDate) {

            StringBuilder selectStatement = new StringBuilder ();

            selectStatement.Append ("EXEC MemberAuthorizedService_SelectByMemberEvent ");

            selectStatement.Append (forMemberId.ToString () + ", ");

            selectStatement.Append (forAuthorizedServiceId.ToString () + ", ");

            selectStatement.Append ("'" + eventDate.ToString ("MM/dd/yyyy") + "'");

            System.Data.DataTable memberServiceTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            if (memberServiceTable.Rows.Count == 0) { return 0; }

            return (Int64)memberServiceTable.Rows[0][0];

        }

        public Int64 MemberAuthorizedServicesGetCount (Int64 memberId, Boolean showHidden) {

            String sqlStatement;

            Int64 itemCount = 0;

            SetLastException (null);

            try {

                sqlStatement = "MemberAuthorizedService_Count " + memberId.ToString () + ", " + showHidden.ToString ();

                itemCount = Convert.ToInt64 (EnvironmentDatabase.StoredProcedureReturnValue (sqlStatement));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return itemCount;

        }

        public List<Core.AuthorizedServices.MemberAuthorizedService> MemberAuthorizedServicesGetByPage (Int64 memberId, Int32 initialRow, Int32 count, Boolean showHidden) {

            List<Core.AuthorizedServices.MemberAuthorizedService> memberAuthorizedServices = new List<Mercury.Server.Core.AuthorizedServices.MemberAuthorizedService> ();

            System.Data.DataTable authorizedServiceTable;

            String sqlStatement;


            SetLastException (null);

            try {

                sqlStatement = "EXEC MemberAuthorizedService_SelectByMemberPage " + memberId.ToString () + ", " + initialRow.ToString () + ", " + count.ToString () + ", " + showHidden.ToString ();

                authorizedServiceTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in authorizedServiceTable.Rows) {

                    Core.AuthorizedServices.MemberAuthorizedService memberAuthorizedService = new Mercury.Server.Core.AuthorizedServices.MemberAuthorizedService (this);

                    memberAuthorizedService.MapDataFields (currentRow);

                    memberAuthorizedServices.Add (memberAuthorizedService);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberAuthorizedServices;

        }

        public List<Core.AuthorizedServices.MemberAuthorizedServiceDetail> MemberAuthorizedServiceDetailsGet (Int64 memberAuthorizedServiceId) {

            List<Core.AuthorizedServices.MemberAuthorizedServiceDetail> serviceDetails = new List<Mercury.Server.Core.AuthorizedServices.MemberAuthorizedServiceDetail> ();

            System.Data.DataTable detailTable;

            String sqlStatement;


            SetLastException (null);

            try {

                sqlStatement = "SELECT * FROM MemberAuthorizedServiceDetail WHERE MemberAuthorizedServiceId = " + memberAuthorizedServiceId.ToString () + " ORDER BY EventDate DESC, AuthorizationId, AuthorizationLine";

                detailTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in detailTable.Rows) {

                    Core.AuthorizedServices.MemberAuthorizedServiceDetail detail = new Mercury.Server.Core.AuthorizedServices.MemberAuthorizedServiceDetail (memberAuthorizedServiceId, (Int64)currentRow["AuthorizedServiceDefinitionId"]);

                    detail.MapDataFields (currentRow);

                    serviceDetails.Add (detail);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return serviceDetails;

        }


        private String MemberAuthorizedServicesSelectSqlStatement (List<Data.FilterDescriptor> filters) {

            String sqlStatement = String.Empty;


            String selectClause = String.Empty;

            String fromClause = String.Empty;

            String whereClause = String.Empty;

            String orderByClause = String.Empty;



            selectClause = "SELECT * \r\n";

            fromClause = "  FROM dbo.MemberAuthorizedService \r\n";

            whereClause = "  WHERE (MemberAuthorizedService.MemberAuthorizedServiceId <> 0) /*_CUSTOM_FILTER_INSERT_*/";


            if (filters == null) { filters = new List<Mercury.Server.Data.FilterDescriptor> (); }

            foreach (Data.FilterDescriptor currentFilter in filters) {

                String criteriaString = String.Empty;


                switch (currentFilter.PropertyPath) {

                    #region Core Object Properties

                    case "Id":

                        currentFilter.PropertyPath = "MemberAuthorizedService" + currentFilter.PropertyPath;

                        criteriaString = currentFilter.SqlCriteriaString (String.Empty);

                        break;

                    #endregion

                    #region Object Properties

                    case "MemberId":

                    case "AuthorizedServiceId":

                    case "EventDate":

                    case "InitialIdentifiedDate":

                        criteriaString = currentFilter.SqlCriteriaString (String.Empty);

                        break;

                    #endregion

                } // switch (currentFilter.PropertyPath) {

                if (!String.IsNullOrEmpty (criteriaString)) {

                    whereClause = whereClause + "  AND " + criteriaString;

                }

            }


            sqlStatement = selectClause + fromClause + whereClause + orderByClause;

            return sqlStatement;

        }

        private String MemberAuthorizedServicesSelectRowNumberSql (List<Data.SortDescriptor> sorts) {

            String sqlString = String.Empty;


            if (sorts == null) { sorts = new List<Data.SortDescriptor> (); }

            foreach (Data.SortDescriptor currentSort in sorts) {

                sqlString = sqlString + currentSort.SqlSortList + ", ";

            }

            sqlString = "    ROW_NUMBER () OVER (ORDER BY \r\n  " + sqlString;

            sqlString = sqlString + "MemberAuthorizedServiceId) AS RowNumber, \r\n  ";

            return sqlString;

        }

        public Int64 MemberAuthorizedServicesSelectCount (List<Data.FilterDescriptor> filters) {

            Int64 itemCount = 0;


            SetLastException (null);

            try {

                String sqlStatement = "SELECT COUNT (1) AS CountOf FROM (" + MemberAuthorizedServicesSelectSqlStatement (filters) + ") AS SourceTable";

                itemCount = Convert.ToInt64 (EnvironmentDatabase.ExecuteScalar (sqlStatement));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return itemCount;
        }

        public List<Core.AuthorizedServices.MemberAuthorizedService> MemberAuthorizedServicesSelect (List<Data.FilterDescriptor> filters, List<Data.SortDescriptor> sorts, Int64 initialRow, Int64 count) {

            List<Core.AuthorizedServices.MemberAuthorizedService> items = new List<Core.AuthorizedServices.MemberAuthorizedService> ();

            System.Data.DataTable itemTable;

            String sqlStatement = String.Empty;

            String customFields = String.Empty;


            ClearLastException ();

            try {

                sqlStatement = sqlStatement + "SELECT MemberAuthorizedServicePage.* FROM ( \r\n";

                sqlStatement = sqlStatement + "SELECT \r\n ";

                sqlStatement = sqlStatement + MemberAuthorizedServicesSelectRowNumberSql (sorts);

                sqlStatement = sqlStatement + "    MemberAuthorizedService.* \r\n";

                sqlStatement = sqlStatement + "  FROM (" + MemberAuthorizedServicesSelectSqlStatement (filters) + ") AS MemberAuthorizedService \r\n";

                sqlStatement = sqlStatement + ") AS MemberAuthorizedServicePage \r\n";


                if (count >= 0) {

                    sqlStatement = sqlStatement + "  WHERE MemberAuthorizedServicePage.RowNumber BETWEEN " + initialRow.ToString ();

                    sqlStatement = sqlStatement + " AND (" + initialRow.ToString () + " + " + count.ToString () + " - 1)";

                }


                itemTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in itemTable.Rows) {

                    Core.AuthorizedServices.MemberAuthorizedService item = new Core.AuthorizedServices.MemberAuthorizedService (this);

                    item.MapDataFields (currentRow);

                    items.Add (item);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return items;

        }

        #endregion 

        #endregion


        #region Claims

        #region Member Claims

        public Core.Claims.Claim ClaimGet (Int64 claimId) {

            Core.Claims.Claim claim = null;

            System.Data.DataTable claimTable;

            String sqlStatement;


            SetLastException (null);

            try {

                sqlStatement = "EXEC dal.Claim_Select " + claimId.ToString ();

                claimTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in claimTable.Rows) {

                    claim = new Server.Core.Claims.Claim ();

                    claim.MapDataFields (currentRow);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return claim;

        }


        public Int64 MemberClaimsGetCount (Int64 memberId) {

            String sqlStatement;

            Int64 claimCount = 0;

            SetLastException (null);

            try {

                sqlStatement = "dal.Claim_CountByMember " + memberId.ToString () + " ";

                claimCount = Convert.ToInt64 (EnvironmentDatabase.StoredProcedureReturnValue (sqlStatement));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return claimCount;

        }

        public List<Core.Claims.Claim> MemberClaimsGetByPage (Int64 memberId, Int32 initialRow, Int32 count) {

            List<Core.Claims.Claim> memberClaims = new List<Server.Core.Claims.Claim> ();

            System.Data.DataTable claimTable;

            String sqlStatement;


            SetLastException (null);

            try {

                sqlStatement = "EXEC dal.Claim_SelectByMemberPage " + memberId.ToString () + ", " + initialRow.ToString () + ", " + count.ToString ();

                claimTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in claimTable.Rows) {

                    Core.Claims.Claim claim = new Server.Core.Claims.Claim ();

                    claim.MapDataFields (currentRow);

                    memberClaims.Add (claim);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberClaims;

        }

        public List<Core.Claims.ClaimLine> ClaimLinesGet (Int64 claimId) {

            List<Core.Claims.ClaimLine> claimLines = new List<Server.Core.Claims.ClaimLine> ();

            System.Data.DataTable claimLineTable;

            String sqlStatement;


            SetLastException (null);

            try {

                sqlStatement = "EXEC dal.ClaimLine_SelectByClaimId " + claimId.ToString ();

                claimLineTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in claimLineTable.Rows) {

                    Core.Claims.ClaimLine claimLine = new Server.Core.Claims.ClaimLine ();

                    claimLine.MapDataFields (currentRow);

                    claimLines.Add (claimLine);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return claimLines;

        }


        #endregion


        #region Pharmacy Claims

        public Core.Claims.PharmacyClaim PharmacyClaimGet (Int64 pharmacyClaimId) {

            Core.Claims.PharmacyClaim claim = null;

            System.Data.DataTable claimTable;

            String sqlStatement;


            SetLastException (null);

            try {

                sqlStatement = "EXEC dal.PharmacyClaim_Select " + pharmacyClaimId.ToString ();

                claimTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in claimTable.Rows) {

                    claim = new Server.Core.Claims.PharmacyClaim ();

                    claim.MapDataFields (currentRow);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return claim;

        }


        public Int64 MemberPharmacyClaimsGetCount (Int64 memberId) {

            String sqlStatement;

            Int64 claimCount = 0;

            SetLastException (null);

            try {

                sqlStatement = "dal.PharmacyClaim_CountByMember " + memberId.ToString () + " ";

                claimCount = Convert.ToInt64 (EnvironmentDatabase.StoredProcedureReturnValue (sqlStatement));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return claimCount;

        }

        public List<Core.Claims.PharmacyClaim> MemberPharmacyClaimsGetByPage (Int64 memberId, Int32 initialRow, Int32 count) {

            List<Core.Claims.PharmacyClaim> memberClaims = new List<Server.Core.Claims.PharmacyClaim> ();

            System.Data.DataTable claimTable;

            String sqlStatement;


            SetLastException (null);

            try {

                sqlStatement = "EXEC dal.PharmacyClaim_SelectByMemberPage " + memberId.ToString () + ", " + initialRow.ToString () + ", " + count.ToString ();

                claimTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in claimTable.Rows) {

                    Core.Claims.PharmacyClaim claim = new Server.Core.Claims.PharmacyClaim ();

                    claim.MapDataFields (currentRow);

                    memberClaims.Add (claim);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberClaims;

        }

        #endregion 
        

        #region Lab Results

        public Core.Claims.LabResult LabResultGet (Int64 labResultId) {

            Core.Claims.LabResult labResult = null;

            System.Data.DataTable labResultTable;

            String sqlStatement;


            SetLastException (null);

            try {

                sqlStatement = "EXEC dal.LabResult_Select " + labResultId.ToString ();

                labResultTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in labResultTable.Rows) {

                    labResult = new Server.Core.Claims.LabResult ();

                    labResult.MapDataFields (currentRow);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return labResult;

        }


        public Int64 MemberLabResultsGetCount (Int64 memberId) {

            String sqlStatement;

            Int64 labResultCount = 0;

            SetLastException (null);

            try {

                sqlStatement = "dal.LabResult_CountByMember " + memberId.ToString () + " ";

                labResultCount = Convert.ToInt64 (EnvironmentDatabase.StoredProcedureReturnValue (sqlStatement));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return labResultCount;

        }

        public List<Core.Claims.LabResult> MemberLabResultsGetByPage (Int64 memberId, Int32 initialRow, Int32 count) {

            List<Core.Claims.LabResult> memberClaims = new List<Server.Core.Claims.LabResult> ();

            System.Data.DataTable labResultTable;

            String sqlStatement;


            SetLastException (null);

            try {

                sqlStatement = "EXEC dal.LabResult_SelectByMemberPage " + memberId.ToString () + ", " + initialRow.ToString () + ", " + count.ToString ();

                labResultTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in labResultTable.Rows) {

                    Core.Claims.LabResult labResult = new Server.Core.Claims.LabResult ();

                    labResult.MapDataFields (currentRow);

                    memberClaims.Add (labResult);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberClaims;

        }

        #endregion 

        #endregion


        #region Core.Condition

        public List<Core.Condition.ConditionClass> ConditionClassesAvailable () {

            List<Core.Condition.ConditionClass> results = new List<Core.Condition.ConditionClass> ();

            String selectStatement = "SELECT * FROM ConditionClass ORDER BY ConditionClassName";

            SetLastException (null);


            System.Data.DataTable dataTable = EnvironmentDatabase.SelectDataTable (selectStatement);

            foreach (System.Data.DataRow currentRow in dataTable.Rows) {

                Core.Condition.ConditionClass conditionClass = new Core.Condition.ConditionClass (this);

                conditionClass.MapDataFields (currentRow);

                results.Add (conditionClass);

            }

            return results;

        }

        public Core.Condition.ConditionClass ConditionClassGet (Int64 conditionClassId) {

            if (conditionClassId == 0) { return null; }

            Core.Condition.ConditionClass conditionClass = null;

            SetLastException (null);

            try {

                conditionClass = new Mercury.Server.Core.Condition.ConditionClass (this, conditionClassId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return conditionClass;

        }

        public Core.Condition.ConditionClass ConditionClassGet (String conditionClassName) {

            return ConditionClassGetByName (conditionClassName);

        }

        public Core.Condition.ConditionClass ConditionClassGetByName (String conditionClassName) {

            //if (conditionDomainId == 0) { return null; }

            if (String.IsNullOrWhiteSpace (conditionClassName)) { return null; }


            Core.Condition.ConditionClass conditionClass = null;

            SetLastException (null);

            try {

                Int64 conditionClassId = Convert.ToInt64 (EnvironmentDatabase.LookupValue ("ConditionClass", "ConditionClassId",

                    //"ConditionDomainId = " + conditionDomainId.ToString () + " AND " +

                    "ConditionClassName = '" + conditionClassName.Replace ("'", "''") + "'", 0));

                if (conditionClassId == 0) { return null; }

                conditionClass = new Mercury.Server.Core.Condition.ConditionClass (this, conditionClassId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return conditionClass;

        }

        public Int64 ConditionClassGetIdByName (String conditionClassName) {

            Core.Condition.ConditionClass conditionClass = ConditionClassGetByName (conditionClassName);

            if (conditionClass == null) { return 0; }

            return conditionClass.Id;

        }


        public List<Core.Condition.Condition> ConditionsAvailable () {

            List<Core.Condition.Condition> results = new List<Core.Condition.Condition> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("SELECT ");

            selectStatement.Append ("    ConditionId ");

            selectStatement.Append ("  FROM dbo.Condition ");

            selectStatement.Append ("  ORDER BY ConditionName");


            System.Data.DataTable serviceTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in serviceTable.Rows) {

                Core.Condition.Condition condition = new Core.Condition.Condition (this, Convert.ToInt64 (currentRow["ConditionId"]));

                results.Add (condition);

            }

            return results;

        }

        public Core.Condition.Condition ConditionGet (Int64 conditionId) {

            if (conditionId == 0) { return null; }

            Core.Condition.Condition condition = null;

            SetLastException (null);

            try {

                condition = new Mercury.Server.Core.Condition.Condition (this, conditionId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return condition;

        }

        #endregion 


        #region Core.Entity

        #region Entity

        public Int64 EntityIdGet (Core.Enumerations.EntityType entityType, Int64 forId) {

            Int64 entityId = 0;

            switch (entityType) {

                case Mercury.Server.Core.Enumerations.EntityType.Member:

                    Core.Member.Member member = MemberGet (forId);

                    if (member != null) { entityId = member.EntityId; }

                    break;


                case Mercury.Server.Core.Enumerations.EntityType.Provider:

                    Core.Provider.Provider provider = ProviderGet (forId);

                    if (provider != null) { entityId = provider.EntityId; }

                    break;

                case Mercury.Server.Core.Enumerations.EntityType.Sponsor:

                    Core.Sponsor.Sponsor sponsor = SponsorGet (forId);

                    if (sponsor != null) { entityId = sponsor.EntityId; }

                    break;

                case Mercury.Server.Core.Enumerations.EntityType.Insurer:

                    Core.Insurer.Insurer insurer = InsurerGet (forId);

                    if (insurer != null) { entityId = insurer.EntityId; }

                    break;

            }

            return entityId;

        }

        public Int64 EntityObjectIdGet (Core.Entity.Entity entity) {

            Int64 entityObjectId = 0;

            switch (entity.EntityType) {

                case Mercury.Server.Core.Enumerations.EntityType.Member:

                    Core.Member.Member member = MemberGetByEntityId (entity.Id);

                    if (member != null) { entityObjectId = member.Id; }

                    break;


                case Mercury.Server.Core.Enumerations.EntityType.Provider:

                    Core.Provider.Provider provider = ProviderGetByEntityId (entity.Id);

                    if (provider != null) { entityObjectId = provider.Id; }

                    break;

                case Mercury.Server.Core.Enumerations.EntityType.Sponsor:

                    throw new ApplicationException ("Not implemented.");

                case Mercury.Server.Core.Enumerations.EntityType.Insurer:

                    throw new ApplicationException ("Not implemented.");

                default:

                    throw new ApplicationException ("Unknown Entity Type: " + entity.EntityType + ".");

            }

            return entityObjectId;
            
        }

        public Mercury.Server.Core.Entity.Entity EntityGet (Int64 entityId, Boolean useCaching = false) {

            if (entityId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".Entity." + entityId.ToString ();

            Mercury.Server.Core.Entity.Entity entity = null;

            ClearLastException ();

            try {

                entity = (Core.Entity.Entity)CacheManager.GetObject (cacheKey);

                if (!useCaching) { entity = null; }

                if (entity == null) {

                    entity = new Mercury.Server.Core.Entity.Entity (this, entityId);

                    if (entity != null) { CacheManager.CacheObject (cacheKey, entity, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entity;

        }

        public Mercury.Server.Core.Entity.Entity EntityGetMember (Int64 entityId, Boolean useCaching = false) {

            if (entityId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".Entity." + entityId.ToString ();

            Mercury.Server.Core.Entity.Entity entity = null;

            ClearLastException ();

            try {

                entity = (Core.Entity.Entity)CacheManager.GetObject (cacheKey);

                if (!useCaching) { entity = null; }

                if (entity == null) {

                    System.Data.DataTable entityTable = EnvironmentDatabase.SelectDataTable ("EXEC dal.Entity_SelectMember " + entityId.ToString ());

                    if (entityTable.Rows.Count == 1) {

                        entity = new Mercury.Server.Core.Entity.Entity (this);

                        entity.MapDataFields (entityTable.Rows[0]);

                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entity;

        }

        #endregion 


        #region Entity Address

        public Core.Entity.EntityAddress EntityAddressGet (Int64 entityAddressId) {

            if (entityAddressId == 0) { return null; }

            Mercury.Server.Core.Entity.EntityAddress entityAddress = null;

            SetLastException (null);

            try {

                entityAddress = new Mercury.Server.Core.Entity.EntityAddress (this, entityAddressId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entityAddress;

        }

        public List<Mercury.Server.Core.Entity.EntityAddress> EntityAddressesGet (Int64 entityId) {

            List<Mercury.Server.Core.Entity.EntityAddress> entityAddresses = new List<Mercury.Server.Core.Entity.EntityAddress> ();

            SetLastException (null);

            try {

                System.Data.DataTable entityAddressTable = EnvironmentDatabase.SelectDataTable ("EXEC dal.EntityAddress_SelectByEntity " + entityId.ToString ());

                foreach (System.Data.DataRow currentRow in entityAddressTable.Rows) {

                    Core.Entity.EntityAddress entityAddress = new Mercury.Server.Core.Entity.EntityAddress (this);

                    entityAddress.MapDataFields (currentRow);

                    entityAddresses.Add (entityAddress);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entityAddresses;

        }

        public Core.Entity.EntityAddress EntityAddressGetByEntityTypeDate (Int64 entityId, Core.Enumerations.EntityAddressType addressType, DateTime forDate) {

            Mercury.Server.Core.Entity.EntityAddress entityAddress = null;

            SetLastException (null);

            try {

                entityAddress = new Mercury.Server.Core.Entity.EntityAddress (this, entityId, addressType, forDate);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entityAddress;

        }

        public List<Mercury.Server.Core.Entity.EntityAddress> EntityAddressesGetByEntityTypeOverlap (Int64 entityId, Core.Enumerations.EntityAddressType addressType, DateTime effectiveDate, DateTime terminationDate, Int64 excludeEntityAddressId) {

            List<Mercury.Server.Core.Entity.EntityAddress> entityAddresses = new List<Mercury.Server.Core.Entity.EntityAddress> ();

            SetLastException (null);

            try {

                System.Data.IDbCommand selectCommand = EnvironmentDatabase.CreateCommand ("dal.EntityAddress_SelectByEntityTypeOverlap");

                selectCommand.CommandType = System.Data.CommandType.StoredProcedure;


                
                EnvironmentDatabase.AppendCommandParameter (selectCommand, "@entityId", entityId);

                EnvironmentDatabase.AppendCommandParameter (selectCommand, "@addressType", ((Int32)addressType));

                EnvironmentDatabase.AppendCommandParameter (selectCommand, "@effectiveDate", effectiveDate);

                EnvironmentDatabase.AppendCommandParameter (selectCommand, "@terminationDate", terminationDate);

                EnvironmentDatabase.AppendCommandParameter (selectCommand, "@excludeEntityAddressId", excludeEntityAddressId);


                System.Data.IDataReader addressReader = selectCommand.ExecuteReader ();

                System.Data.DataTable addressTable = new System.Data.DataTable ();

                addressTable.Load (addressReader);

                addressReader.Close ();


                foreach (System.Data.DataRow currentRow in addressTable.Rows) {

                    Core.Entity.EntityAddress entityAddress = new Mercury.Server.Core.Entity.EntityAddress (this);

                    entityAddress.MapDataFields (currentRow);

                    entityAddresses.Add (entityAddress);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entityAddresses;

        }

        #endregion 


        #region Entity Contact Information

        public Core.Entity.EntityContactInformation EntityContactInformationGet (Int64 entityContactInformationId) {

            if (entityContactInformationId == 0) { return null; }

            Core.Entity.EntityContactInformation entityContactInformation = null;

            SetLastException (null);

            try {

                entityContactInformation = new Mercury.Server.Core.Entity.EntityContactInformation (this, entityContactInformationId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entityContactInformation;

        }

        public List<Mercury.Server.Core.Entity.EntityContactInformation> EntityContactInformationsGet (Int64 entityId) {

            List<Mercury.Server.Core.Entity.EntityContactInformation> entityContacts = new List<Mercury.Server.Core.Entity.EntityContactInformation> ();

            SetLastException (null);

            try {

                System.Data.DataTable entityContactTable = EnvironmentDatabase.SelectDataTable ("EXEC dal.EntityContactInformation_SelectByEntity " + entityId.ToString ());

                foreach (System.Data.DataRow currentRow in entityContactTable.Rows) {

                    Core.Entity.EntityContactInformation entityContact = new Mercury.Server.Core.Entity.EntityContactInformation (this);

                    entityContact.MapDataFields (currentRow);

                    entityContacts.Add (entityContact);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entityContacts;

        }

        public Core.Entity.EntityContactInformation EntityContactInformationGetByEntityTypeDate (Int64 entityId, Core.Enumerations.EntityContactType contactType, DateTime forDate) {

            Mercury.Server.Core.Entity.EntityContactInformation entityContactInformation = null;

            SetLastException (null);

            try {

                String selectStatement = "EXEC dal.EntityContactInformation_SelectByTypeAndDate " + entityId.ToString () + ", " + ((Int32) contactType).ToString () + ", '" + forDate.ToString ("MM/dd/yyyy") + "'";

                System.Data.DataTable tableEntityContact = EnvironmentDatabase.SelectDataTable (selectStatement);

                if (tableEntityContact.Rows.Count == 1) {

                    entityContactInformation = new Core.Entity.EntityContactInformation (this);

                    entityContactInformation.MapDataFields (tableEntityContact.Rows[0]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entityContactInformation;

        }

        public List<Mercury.Server.Core.Entity.EntityContactInformation> EntityContactInformationsGetByEntityTypeOverlap (Int64 entityId, Core.Enumerations.EntityContactType contactType, Int32 contactSequence, DateTime effectiveDate, DateTime terminationDate, Int64 excludeEntityContactInformationId) {

            List<Mercury.Server.Core.Entity.EntityContactInformation> entityAddresses = new List<Mercury.Server.Core.Entity.EntityContactInformation> ();

            SetLastException (null);

            try {

                System.Data.IDbCommand selectCommand = EnvironmentDatabase.CreateCommand ("dal.EntityContactInformation_SelectByEntityTypeOverlap");

                selectCommand.CommandType = System.Data.CommandType.StoredProcedure;


                EnvironmentDatabase.AppendCommandParameter (selectCommand, "@entityId", entityId);

                EnvironmentDatabase.AppendCommandParameter (selectCommand, "@contactType", ((Int32) contactType));

                EnvironmentDatabase.AppendCommandParameter (selectCommand, "@contactSequence", contactSequence);

                EnvironmentDatabase.AppendCommandParameter (selectCommand, "@effectiveDate", effectiveDate);

                EnvironmentDatabase.AppendCommandParameter (selectCommand, "@terminationDate", terminationDate);

                EnvironmentDatabase.AppendCommandParameter (selectCommand, "@excludeEntityContactInformationId", excludeEntityContactInformationId);

                EnvironmentDatabase.AppendCommandParameter (selectCommand, "@entityId", entityId);


                System.Data.IDataReader addressReader = selectCommand.ExecuteReader ();

                System.Data.DataTable addressTable = new System.Data.DataTable ();

                addressTable.Load (addressReader);

                addressReader.Close ();


                foreach (System.Data.DataRow currentRow in addressTable.Rows) {

                    Core.Entity.EntityContactInformation entityAddress = new Mercury.Server.Core.Entity.EntityContactInformation (this);

                    entityAddress.MapDataFields (currentRow);

                    entityAddresses.Add (entityAddress);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entityAddresses;

        }

        #endregion 


        #region Entity Contact

        public Core.Entity.EntityContact EntityContactGet (Int64 entityContactId) {

            if (entityContactId == 0) { return null; }

            Core.Entity.EntityContact entityContact = null;

            SetLastException (null);

            try {

                entityContact = new Mercury.Server.Core.Entity.EntityContact (this, entityContactId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entityContact;

        }

        public Int64 EntityContactsGetCount (Int64 entityId) {

            String sqlStatement;

            Int64 count = 0;

            SetLastException (null);

            try {

                sqlStatement = "dal.EntityContact_CountByEntity " + entityId.ToString () + " ";

                count = Convert.ToInt64 (EnvironmentDatabase.StoredProcedureReturnValue (sqlStatement));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return count;

        }

        public List<Core.Entity.EntityContact> EntityContactsGetByPage (Int64 entityId, Int32 initialRow, Int32 count) {

            List<Core.Entity.EntityContact> notes = new List<Mercury.Server.Core.Entity.EntityContact> ();

            System.Data.DataTable pageTable;

            String sqlStatement;


            SetLastException (null);

            try {

                sqlStatement = "EXEC dal.EntityContact_SelectByEntityPage " + entityId.ToString () + ", " + initialRow.ToString () + ", " + count.ToString ();

                pageTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in pageTable.Rows) {

                    Core.Entity.EntityContact entityContact = new Mercury.Server.Core.Entity.EntityContact (this);

                    entityContact.MapDataFields (currentRow);

                    notes.Add (entityContact);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return notes;

        }

        #endregion 

        
        #region Entity Correspondence

        public Core.Entity.EntityCorrespondence EntityCorrespondenceGet (Int64 entityCorrespondenceId) {

            if (entityCorrespondenceId == 0) { return null; }

            Core.Entity.EntityCorrespondence entityCorrespondence = null;

            SetLastException (null);

            try {

                entityCorrespondence = new Mercury.Server.Core.Entity.EntityCorrespondence (this, entityCorrespondenceId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entityCorrespondence;

        }


        public Int64 EntityDocumentsGetCount (Int64 entityId) {

            String sqlStatement;

            Int64 count = 0;

            SetLastException (null);

            try {

                sqlStatement = "dal.EntityDocument_CountByEntity " + entityId.ToString () + " ";

                count = Convert.ToInt64 (EnvironmentDatabase.StoredProcedureReturnValue (sqlStatement));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return count;

        }

        public List<Core.Entity.Views.EntityDocument> EntityDocumentsGetByPage (Int64 entityId, Int32 initialRow, Int32 count) {

            List<Core.Entity.Views.EntityDocument> documents = new List<Mercury.Server.Core.Entity.Views.EntityDocument> ();

            System.Data.DataTable pageTable;

            String sqlStatement;


            SetLastException (null);

            try {

                sqlStatement = "EXEC dal.EntityDocument_SelectByEntityPage " + entityId.ToString () + ", " + initialRow.ToString () + ", " + count.ToString ();

                pageTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in pageTable.Rows) {

                    Core.Entity.Views.EntityDocument entityDocument = new Mercury.Server.Core.Entity.Views.EntityDocument ();

                    entityDocument.MapDataFields (currentRow);

                    documents.Add (entityDocument);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return documents;

        }


        public Public.ImageStream EntityCorrespondenceImageGet (Int64 entityCorrespondenceId) {

            if (entityCorrespondenceId == 0) { return null; }

            Public.ImageStream renderedContent = new Public.ImageStream ();

            SetLastException (null);

            try {

                String selectStatement = "SELECT EntityCorrespondenceImageName, EntityCorrespondenceImageExtension, EntityCorrespondenceImageMimeType, EntityCorrespondenceImageIsCompressed";

                selectStatement += "  FROM dbo.EntityCorrespondenceImage WHERE EntityCorrespondenceId = " + entityCorrespondenceId.ToString ();

                System.Data.DataTable imageTable = EnvironmentDatabase.SelectDataTable (selectStatement);

                if (imageTable.Rows.Count == 1) {

                    renderedContent.MapDataFields ("EntityCorrespondence", imageTable.Rows[0]);

                    renderedContent.Image = EnvironmentDatabase.BlobRead ("dbo.EntityCorrespondenceImage", "EntityCorrespondenceImageData", "EntityCorrespondenceId = " + entityCorrespondenceId.ToString ());

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return renderedContent;

        }

        public Public.ImageStream EntityCorrespondenceRender (Int64 entityCorrespondenceId) {

            if (entityCorrespondenceId == 0) { return null; }

            Core.Entity.EntityCorrespondence entityCorrespondence = null;

            Public.ImageStream renderedContent = new Public.ImageStream ();

            SetLastException (null);

            try {

                entityCorrespondence = new Mercury.Server.Core.Entity.EntityCorrespondence (this, entityCorrespondenceId);

                entityCorrespondence.Render ();

                renderedContent = EntityCorrespondenceImageGet (entityCorrespondenceId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return renderedContent;

        }

        #endregion 


        #region Entity Note

        public Core.Entity.EntityNote EntityNoteGet (Int64 entityNoteId) {

            if (entityNoteId == 0) { return null; }

            Core.Entity.EntityNote entityNote = null;

            SetLastException (null);

            try {

                entityNote = new Mercury.Server.Core.Entity.EntityNote (this, entityNoteId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entityNote;

        }

        public Int64 EntityNotesGetCount (Int64 entityId) {

            String sqlStatement;

            Int64 count = 0;

            SetLastException (null);

            try {

                sqlStatement = "dal.EntityNote_CountByEntity " + entityId.ToString () + " ";

                count = Convert.ToInt64 (EnvironmentDatabase.StoredProcedureReturnValue (sqlStatement));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return count;

        }

        public List<Core.Entity.EntityNote> EntityNotesGetByPage (Int64 entityId, Int32 initialRow, Int32 count) {

            List<Core.Entity.EntityNote> notes = new List<Mercury.Server.Core.Entity.EntityNote> ();

            System.Data.DataTable pageTable;

            String sqlStatement;


            SetLastException (null);

            try {

                sqlStatement = "EXEC dal.EntityNote_SelectByEntityPage " + entityId.ToString () + ", " + initialRow.ToString () + ", " + count.ToString ();

                pageTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in pageTable.Rows) {

                    Core.Entity.EntityNote entityNote = new Mercury.Server.Core.Entity.EntityNote (this);

                    entityNote.MapDataFields (currentRow);

                    notes.Add (entityNote);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return notes;

        }

        /// <summary>
        /// Retreives a List of Entity Notes for a specific Entity that have a specific Subject. This does
        /// not support partial matches and the subject must be a one-for-one match.
        /// </summary>
        /// <param name="entityId">The unique Entity Id for the Entity you want to retrieve the notes.</param>
        /// <param name="subject">The subject text (120 characters) that you want to retreive.</param>
        /// <returns></returns>
        public List<Core.Entity.EntityNote> EntityNotesGetBySubject (Int64 entityId, String subject) {

            List<Core.Entity.EntityNote> notes = new List<Mercury.Server.Core.Entity.EntityNote> ();

            System.Data.DataTable pageTable;

            String sqlStatement;


            SetLastException (null);

            try {

                sqlStatement = "EXEC dal.EntityNote_SelectByEntitySubject " + entityId.ToString () + ", '" + subject.Replace ("'", "''") + "'";

                pageTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in pageTable.Rows) {

                    Core.Entity.EntityNote entityNote = new Mercury.Server.Core.Entity.EntityNote (this);

                    entityNote.MapDataFields (currentRow);

                    notes.Add (entityNote);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return notes;

        }

        public List<Core.Entity.EntityNoteContent> EntityNoteContentsGet (Int64 entityNoteId) {

            List<Core.Entity.EntityNoteContent> entityNoteContents = new List<Core.Entity.EntityNoteContent> ();

            SetLastException (null);

            try {

                System.Data.DataTable entityNoteContentTable = EnvironmentDatabase.SelectDataTable ("EXEC dal.EntityNoteContents_Select " + entityNoteId.ToString ());

                foreach (System.Data.DataRow currentRow in entityNoteContentTable.Rows) {

                    Core.Entity.EntityNoteContent entityNoteContent = new Mercury.Server.Core.Entity.EntityNoteContent (this);

                    entityNoteContent.MapDataFields (currentRow);

                    entityNoteContents.Add (entityNoteContent);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entityNoteContents;

        }

        public Core.Entity.EntityNote EntityNoteGetMostRecentByImportance (Int64 entityId, Core.Enumerations.NoteImportance importance) {

            Mercury.Server.Core.Entity.EntityNote entityNote = null;

            SetLastException (null);

            try {

                System.Data.DataTable entityNoteTable = EnvironmentDatabase.SelectDataTable ("EXEC dal.EntityNote_SelectByMostRecentImportance " + entityId.ToString () + ", " + ((Int32) importance).ToString ());

                if (entityNoteTable.Rows.Count == 1) {

                    entityNote = new Mercury.Server.Core.Entity.EntityNote (this);

                    entityNote.MapDataFields (entityNoteTable.Rows[0]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entityNote;

        }

        public Core.Entity.EntityNote EntityNoteGetMostRecentByType (Int64 entityId, Int64 noteTypeId) {

            Mercury.Server.Core.Entity.EntityNote entityNote = null;

            SetLastException (null);

            try {

                System.Data.DataTable entityNoteTable = EnvironmentDatabase.SelectDataTable ("EXEC dal.EntityNote_SelectByMostRecentType " + entityId.ToString () + ", " + noteTypeId.ToString ());

                if (entityNoteTable.Rows.Count == 1) {

                    entityNote = new Mercury.Server.Core.Entity.EntityNote (this);

                    entityNote.MapDataFields (entityNoteTable.Rows[0]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entityNote;

        }

        #endregion 


        #endregion


        #region Core.Forms

        public List<Core.Search.SearchResultFormHeader> FormsAvailable () {

            List<Core.Search.SearchResultFormHeader> results = new List<Core.Search.SearchResultFormHeader> ();

            StringBuilder selectStatement = new StringBuilder ();


            ClearLastException ();


            selectStatement.Append ("SELECT ");

            selectStatement.Append ("    Form.*, FormControl.ControlName, FormControl.ControlTitle, FormControl.Enabled, FormControl.Visible ");

            selectStatement.Append ("  FROM Form JOIN FormControl");

            selectStatement.Append ("    ON Form.FormControlId = FormControl.ControlId");

            selectStatement.Append ("      AND Form.FormId = FormControl.FormId");

            selectStatement.Append ("  ORDER BY FormControl.ControlName");


            System.Data.DataTable formTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in formTable.Rows) {

                Server.Core.Search.SearchResultFormHeader formHeader = new Server.Core.Search.SearchResultFormHeader ();

                formHeader.MapDataFields (currentRow);

                results.Add (formHeader);

            }

            return results;

        }


        public Mercury.Server.Core.Forms.Form FormGet (Int64 formId) {

            if (formId == 0) { return null; }

            Mercury.Server.Core.Forms.Form form = null;

            ClearLastException ();

            try {

                form = new Mercury.Server.Core.Forms.Form (this, formId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return form;

        }

        public Core.Forms.Form FormGet (String formName) {

            if (String.IsNullOrWhiteSpace (formName)) { return null; }

            Int64 formId = CoreObjectGetIdByName ("Form", formName);

            return FormGet (formId);

        }


        public Core.Forms.Form EntityFormGet (Int64 entityFormId) {

            if (entityFormId == 0) { return null; }

            Core.Forms.Form form = null;

            try {

                form = new Server.Core.Forms.Form (this, entityFormId, true);

            }

            catch { /* DO NOTHING */ }

            return form;

        }

        #endregion 


        #region Core.Individual

        #region Care Level

        public List<Core.Individual.CareLevel> CareLevelsAvailable() {

            List<Core.Individual.CareLevel> results = new List<Core.Individual.CareLevel> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("SELECT ");

            selectStatement.Append ("    CareLevelId ");

            selectStatement.Append ("  FROM dbo.CareLevel ");

            selectStatement.Append ("  ORDER BY CareLevelName");


            System.Data.DataTable serviceTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in serviceTable.Rows) {

                Core.Individual.CareLevel careLevel = new Core.Individual.CareLevel (this, Convert.ToInt64 (currentRow["CareLevelId"]));

                results.Add (careLevel);

            }

            return results;

        }

        public Core.Individual.CareLevel CareLevelGet (Int64 careLevelId) {

            if (careLevelId == 0) { return null; }

            Core.Individual.CareLevel careLevel = null;

            SetLastException (null);

            try {

                careLevel = new Mercury.Server.Core.Individual.CareLevel (this, careLevelId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return careLevel;

        }

        public Core.Individual.CareLevel CareLevelGet (String careLevelName) {

            if (String.IsNullOrEmpty (careLevelName)) { return null; }

            Core.Individual.CareLevel careLevel = null;

            Int64 careLevelId = 0;

            SetLastException (null);

            try {

                careLevelId = Convert.ToInt64 (EnvironmentDatabase.LookupValue ("CareLevel", "CareLevelId", "CareLevelName = '" + careLevelName + "'", 0));

                careLevel = CareLevelGet (careLevelId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return careLevel;

        }

        public String CareLevelGetNameById (Int64 careLevelId) {

            if (careLevelId == 0) { return String.Empty; }

            String name = String.Empty;

            name = Convert.ToString (EnvironmentDatabase.LookupValue ("CareLevel", "CareLevelName", "CareLevelId = " + careLevelId.ToString (), String.Empty));

            return name;

        }

        public Int64 CareLevelGetIdByName (String name) {

            if (String.IsNullOrEmpty (name)) { return 0; }

            Int64 id = 0;

            id = Convert.ToInt64 (EnvironmentDatabase.LookupValue ("CareLevel", "CareLevelId", "CareLevelName = '" + name.Replace ("'", "''") + "'", 0));

            return id;

        }

        #endregion


        #region Care Measures

        public List<Core.Individual.CareMeasureScale> CareMeasureScalesAvailable (Boolean useCaching = false) {

            String cacheKey = "Application." + session.EnvironmentId + ".CareMeasureScale.Available";

            Dictionary<Int64, String> objectDictionary = new Dictionary<Int64, String> ();

            List<Core.Individual.CareMeasureScale> availableItems = new List<Core.Individual.CareMeasureScale> ();

            ClearLastException ();


            try {

                if (!useCaching) { CacheManager.RemoveObject (cacheKey); }

                availableItems = (List<Core.Individual.CareMeasureScale>)CacheManager.GetObject (cacheKey);

                if (availableItems == null) {

                    availableItems = new List<Core.Individual.CareMeasureScale> ();

                    String selectStatement = "SELECT * FROM dbo.CareMeasureScale ORDER BY CareMeasureScaleName";

                    System.Data.DataTable availableTable = EnvironmentDatabase.SelectDataTable (selectStatement);

                    foreach (System.Data.DataRow currentRow in availableTable.Rows) {

                        Core.Individual.CareMeasureScale item = new Core.Individual.CareMeasureScale (this);

                        item.MapDataFields (currentRow);

                        availableItems.Add (item);


                        // CACHE EACH INDIVIDUAL ITEM LOADED FROM SERVER (BY ID AND NAME)

                        CacheManager.CacheObject ("Application." + session.EnvironmentId + "." + item.ObjectType + "." + item.Id.ToString (), item, CacheExpirationData);

                        CacheManager.CacheObject ("Application." + session.EnvironmentId + "." + item.ObjectType + "." + item.Name, item, CacheExpirationData);

                        objectDictionary.Add (item.Id, item.Name);

                    }

                    if (availableItems.Count > 0) {

                        // CACHE THE AVAILABILITY LIST

                        CacheManager.CacheObject (cacheKey, availableItems, CacheExpirationData);


                        // CACHE THE DICTIONARY THAT WAS CREATED

                        CacheManager.CacheObject ("Application." + session.EnvironmentId + "." + availableItems[0].ObjectType + ".Dictionary", objectDictionary, CacheExpirationData);

                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return availableItems;

        }

        public Core.Individual.CareMeasureScale CareMeasureScaleGet (Int64 careMeasureScaleId) {

            if (careMeasureScaleId == 0) { return null; }

            Core.Individual.CareMeasureScale careMeasureScale = null;

            SetLastException (null);

            try {

                careMeasureScale = new Mercury.Server.Core.Individual.CareMeasureScale (this, careMeasureScaleId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return careMeasureScale;

        }


        public List<Core.Individual.CareMeasureDomain> CareMeasureDomainsAvailable () {

            List<Core.Individual.CareMeasureDomain> results = new List<Core.Individual.CareMeasureDomain> ();

            String selectStatement = "SELECT * FROM CareMeasureDomain ORDER BY CareMeasureDomainName";

            SetLastException (null);


            System.Data.DataTable dataTable = EnvironmentDatabase.SelectDataTable (selectStatement);

            foreach (System.Data.DataRow currentRow in dataTable.Rows) {

                Core.Individual.CareMeasureDomain careMeasureDomain = new Core.Individual.CareMeasureDomain (this);

                careMeasureDomain.MapDataFields (currentRow);

                results.Add (careMeasureDomain);

            }

            return results;

        }

        public Core.Individual.CareMeasureDomain CareMeasureDomainGet (Int64 careMeasureDomainId) {

            if (careMeasureDomainId == 0) { return null; }

            Core.Individual.CareMeasureDomain careMeasureDomain = null;

            SetLastException (null);

            try {

                careMeasureDomain = new Mercury.Server.Core.Individual.CareMeasureDomain (this, careMeasureDomainId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return careMeasureDomain;

        }

        public List<Core.Individual.CareMeasureClass> CareMeasureClassesAvailable () {

            List<Core.Individual.CareMeasureClass> results = new List<Core.Individual.CareMeasureClass> ();

            String selectStatement = "SELECT * FROM CareMeasureClass ORDER BY CareMeasureClassName";

            SetLastException (null);


            System.Data.DataTable dataTable = EnvironmentDatabase.SelectDataTable (selectStatement);

            foreach (System.Data.DataRow currentRow in dataTable.Rows) {

                Core.Individual.CareMeasureClass careMeasureClass = new Core.Individual.CareMeasureClass (this);

                careMeasureClass.MapDataFields (currentRow);

                results.Add (careMeasureClass);

            }

            return results;

        }

        public Core.Individual.CareMeasureClass CareMeasureClassGet (Int64 careMeasureClassId) {

            if (careMeasureClassId == 0) { return null; }

            Core.Individual.CareMeasureClass careMeasureClass = null;

            SetLastException (null);

            try {

                careMeasureClass = new Mercury.Server.Core.Individual.CareMeasureClass (this, careMeasureClassId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return careMeasureClass;

        }

        public Core.Individual.CareMeasureClass CareMeasureClassGetByName (Int64 careMeasureDomainId, String careMeasureClassName) {

            if (careMeasureDomainId == 0) { return null; }

            if (String.IsNullOrWhiteSpace (careMeasureClassName)) { return null; }


            Core.Individual.CareMeasureClass careMeasureClass = null;

            SetLastException (null);

            try {

                Int64 careMeasureClassId = Convert.ToInt64 (EnvironmentDatabase.LookupValue ("CareMeasureClass", "CareMeasureClassId",

                    "CareMeasureDomainId = " + careMeasureDomainId.ToString () + " AND " +

                    "CareMeasureClassName = '" + careMeasureClassName.Replace ("'", "''") + "'", 0));

                if (careMeasureClassId == 0) { return null; }

                careMeasureClass = new Mercury.Server.Core.Individual.CareMeasureClass (this, careMeasureClassId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return careMeasureClass;

        }

        public Int64 CareMeasureClassGetIdByName (Int64 careMeasureDomainId, String careMeasureClassName) {

            Core.Individual.CareMeasureClass careMeasureClass = CareMeasureClassGetByName (careMeasureDomainId, careMeasureClassName);

            if (careMeasureClass == null) { return 0; }

            return careMeasureClass.Id;

        }


        public List<Core.Individual.CareMeasure> CareMeasuresAvailable () {

            List<Core.Individual.CareMeasure> results = new List<Core.Individual.CareMeasure> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("SELECT ");

            selectStatement.Append ("    CareMeasureId ");

            selectStatement.Append ("  FROM dbo.CareMeasure ");

            selectStatement.Append ("  ORDER BY CareMeasureName");


            System.Data.DataTable serviceTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in serviceTable.Rows) {

                Core.Individual.CareMeasure careMeasure = new Core.Individual.CareMeasure (this, Convert.ToInt64 (currentRow["CareMeasureId"]));

                results.Add (careMeasure);

            }

            return results;

        }

        public Core.Individual.CareMeasure CareMeasureGet (Int64 careMeasureId, Boolean useCaching = false) {

            if (careMeasureId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".CareMeasure." + careMeasureId.ToString ();

            Core.Individual.CareMeasure careMeasure = null;

            ClearLastException ();

            try {

                careMeasure = (Core.Individual.CareMeasure)CacheManager.GetObject (cacheKey);

                if (!useCaching) { careMeasure = null; }

                if (careMeasure == null) {

                    careMeasure = new Mercury.Server.Core.Individual.CareMeasure (this, careMeasureId);

                    if (careMeasure != null) { CacheManager.CacheObject (cacheKey, careMeasure, CacheExpirationData); }

                }
            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return careMeasure;

        }

        public Core.Individual.CareMeasure CareMeasureGet (String careMeasureName, Boolean useCaching = false) {

            if (String.IsNullOrWhiteSpace (careMeasureName)) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".CareMeasure." + careMeasureName;

            Core.Individual.CareMeasure careMeasure = null;

            Int64 careMeasureId = 0;

            ClearLastException ();

            try {

                careMeasure = (Core.Individual.CareMeasure)CacheManager.GetObject (cacheKey);

                if (!useCaching) { careMeasure = null; }

                if (careMeasure == null) {

                    careMeasureId = CoreObjectGetIdByName ("CareMeasure", careMeasureName);

                    careMeasure = CareMeasureGet (careMeasureId, useCaching);

                    if (careMeasure != null) { CacheManager.CacheObject (cacheKey, careMeasure, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return careMeasure;

        }

        #endregion 


        #region Care Interventions

        public List<Core.Individual.CareIntervention> CareInterventionsAvailable () {

            List<Core.Individual.CareIntervention> results = new List<Core.Individual.CareIntervention> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("SELECT ");

            selectStatement.Append ("    CareInterventionId ");

            selectStatement.Append ("  FROM dbo.CareIntervention ");

            selectStatement.Append ("  ORDER BY CareInterventionName");


            System.Data.DataTable serviceTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in serviceTable.Rows) {

                Core.Individual.CareIntervention careIntervention = new Core.Individual.CareIntervention (this, Convert.ToInt64 (currentRow["CareInterventionId"]));

                results.Add (careIntervention);

            }

            return results;

        }

        public Core.Individual.CareIntervention CareInterventionGet (Int64 careInterventionId, Boolean useCaching = false) {

            if (careInterventionId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".CareIntervention." + careInterventionId.ToString ();

            Core.Individual.CareIntervention careIntervention = null;

            ClearLastException ();

            try {

                careIntervention = (Core.Individual.CareIntervention)CacheManager.GetObject (cacheKey);

                if (!useCaching) { careIntervention = null; }

                if (careIntervention == null) {

                    careIntervention = new Mercury.Server.Core.Individual.CareIntervention (this, careInterventionId);

                    if (careIntervention != null) { CacheManager.CacheObject (cacheKey, careIntervention, CacheExpirationData); }

                }
            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return careIntervention;

        }

        public Core.Individual.CareIntervention CareInterventionGet (String careInterventionName, Boolean useCaching = false) {

            if (String.IsNullOrWhiteSpace (careInterventionName)) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".CareIntervention." + careInterventionName;

            Core.Individual.CareIntervention careIntervention = null;

            ClearLastException ();

            try {

                careIntervention = (Core.Individual.CareIntervention)CacheManager.GetObject (cacheKey);

                if (!useCaching) { careIntervention = null; }

                if (careIntervention == null) {

                    careIntervention = new Mercury.Server.Core.Individual.CareIntervention (this, CoreObjectGetIdByName ("CareIntervention", careInterventionName));

                    if (careIntervention != null) { CacheManager.CacheObject (cacheKey, careIntervention, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return careIntervention;

        }

        #endregion 


        #region Care Plan

        public List<Core.Individual.CarePlan> CarePlansAvailable () {

            List<Core.Individual.CarePlan> results = new List<Core.Individual.CarePlan> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("SELECT ");

            selectStatement.Append ("    CarePlanId ");

            selectStatement.Append ("  FROM dbo.CarePlan ");

            selectStatement.Append ("  ORDER BY CarePlanName");


            System.Data.DataTable serviceTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in serviceTable.Rows) {

                Core.Individual.CarePlan carePlan = new Core.Individual.CarePlan (this, Convert.ToInt64 (currentRow["CarePlanId"]));

                results.Add (carePlan);

            }

            return results;

        }

        public Core.Individual.CarePlan CarePlanGet (Int64 carePlanId) {

            if (carePlanId == 0) { return null; }

            Core.Individual.CarePlan carePlan = null;

            SetLastException (null);

            try {

                carePlan = new Mercury.Server.Core.Individual.CarePlan (this, carePlanId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return carePlan;

        }

        public Core.Individual.CarePlan CarePlanGet (String carePlanName) {

            if (String.IsNullOrEmpty (carePlanName)) { return null; }

            Core.Individual.CarePlan carePlan = null;

            Int64 carePlanId = 0;

            SetLastException (null);

            try {

                carePlanId = Convert.ToInt64 (EnvironmentDatabase.LookupValue ("CarePlan", "CarePlanId", "CarePlanName = '" + carePlanName + "'", 0));

                carePlan = CarePlanGet (carePlanId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return carePlan;

        }

        public String CarePlanGetNameById (Int64 carePlanId) {

            if (carePlanId == 0) { return String.Empty; }

            String name = String.Empty;

            name = Convert.ToString (EnvironmentDatabase.LookupValue ("CarePlan", "CarePlanName", "CarePlanId = " + carePlanId.ToString (), String.Empty));

            return name;

        }

        public Int64 CarePlanGetIdByName (String name) {

            if (String.IsNullOrEmpty (name)) { return 0; }

            Int64 id = 0;

            id = Convert.ToInt64 (EnvironmentDatabase.LookupValue ("CarePlan", "CarePlanId", "CarePlanName = '" + name.Replace ("'", "''") + "'", 0));

            return id;

        }

        #endregion


        #region Problem Statement

        public List<Core.Individual.ProblemDomain> ProblemDomainsAvailable () {

            List<Core.Individual.ProblemDomain> results = new List<Core.Individual.ProblemDomain> ();

            String selectStatement = "SELECT * FROM ProblemDomain ORDER BY ProblemDomainName";

            SetLastException (null);


            System.Data.DataTable dataTable = EnvironmentDatabase.SelectDataTable (selectStatement);

            foreach (System.Data.DataRow currentRow in dataTable.Rows) {

                Core.Individual.ProblemDomain problemDomain = new Core.Individual.ProblemDomain (this);

                problemDomain.MapDataFields (currentRow);

                results.Add (problemDomain);

            }

            return results;

        }

        public Core.Individual.ProblemDomain ProblemDomainGet (Int64 problemDomainId) {

            if (problemDomainId == 0) { return null; }

            Core.Individual.ProblemDomain problemDomain = null;

            SetLastException (null);

            try {

                problemDomain = new Mercury.Server.Core.Individual.ProblemDomain (this, problemDomainId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return problemDomain;

        }


        public List<Core.Individual.ProblemClass> ProblemClassesAvailable () {

            List<Core.Individual.ProblemClass> results = new List<Core.Individual.ProblemClass> ();

            String selectStatement = "SELECT * FROM ProblemClass ORDER BY ProblemClassName";

            SetLastException (null);


            System.Data.DataTable dataTable = EnvironmentDatabase.SelectDataTable (selectStatement);

            foreach (System.Data.DataRow currentRow in dataTable.Rows) {

                Core.Individual.ProblemClass problemClass = new Core.Individual.ProblemClass (this);

                problemClass.MapDataFields (currentRow);

                results.Add (problemClass);

            }

            return results;

        }

        public Core.Individual.ProblemClass ProblemClassGet (Int64 problemClassId) {

            if (problemClassId == 0) { return null; }

            Core.Individual.ProblemClass problemClass = null;

            SetLastException (null);

            try {

                problemClass = new Mercury.Server.Core.Individual.ProblemClass (this, problemClassId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return problemClass;

        }

        public Core.Individual.ProblemClass ProblemClassGetByName (Int64 problemDomainId, String problemClassName) {

            if (problemDomainId == 0) { return null; }

            if (String.IsNullOrWhiteSpace (problemClassName)) { return null; }


            Core.Individual.ProblemClass problemClass = null;

            SetLastException (null);

            try {

                Int64 problemClassId = Convert.ToInt64 (EnvironmentDatabase.LookupValue ("ProblemClass", "ProblemClassId", 
                    
                    "ProblemDomainId = " + problemDomainId.ToString () + " AND " + 
                    
                    "ProblemClassName = '" + problemClassName.Replace ("'", "''") + "'", 0));

                if (problemClassId == 0) { return null; }

                problemClass = new Mercury.Server.Core.Individual.ProblemClass (this, problemClassId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return problemClass;

        }

        public Int64 ProblemClassGetIdByName (Int64 problemDomainId, String problemClassName) {

            Core.Individual.ProblemClass problemClass = ProblemClassGetByName (problemDomainId, problemClassName);

            if (problemClass == null) { return 0; }

            return problemClass.Id;

        }


        public List<Core.Individual.ProblemStatement> ProblemStatementsAvailable () {

            List<Core.Individual.ProblemStatement> results = new List<Core.Individual.ProblemStatement> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("SELECT ProblemStatement.*, ");

            selectStatement.Append ("    ProblemDomain.ProblemDomainName, ProblemClass.ProblemClassName");

            selectStatement.Append ("  FROM ProblemStatement ");

            selectStatement.Append ("    LEFT JOIN ProblemDomain ON ProblemStatement.ProblemDomainId = ProblemDomain.ProblemDomainId");

            selectStatement.Append ("    LEFT JOIN ProblemClass ON ProblemStatement.ProblemClassId = ProblemClass.ProblemClassId");

            selectStatement.Append ("  ORDER BY ProblemStatementName");

            System.Data.DataTable serviceTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in serviceTable.Rows) {

                Core.Individual.ProblemStatement problemStatement = new Core.Individual.ProblemStatement (this);

                problemStatement.MapDataFields (currentRow);

                results.Add (problemStatement);

            }

            return results;

        }

        public Core.Individual.ProblemStatement ProblemStatementGet (Int64 problemStatementId) {

            if (problemStatementId == 0) { return null; }

            Core.Individual.ProblemStatement problemStatement = null;

            SetLastException (null);

            try {

                problemStatement = new Mercury.Server.Core.Individual.ProblemStatement (this, problemStatementId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return problemStatement;

        }


        public List<Core.Individual.MemberProblemStatementIdentified> MemberProblemStatementIdentifiedAvailable (Int64 memberId, Boolean includeCompleted = false) {

            List<Core.Individual.MemberProblemStatementIdentified> results = new List<Core.Individual.MemberProblemStatementIdentified> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("SELECT ");

            selectStatement.Append ("    MemberProblemStatementIdentified.* ");

            selectStatement.Append ("  FROM dbo.MemberProblemStatementIdentified ");

            selectStatement.Append ("   JOIN ProblemStatement ON MemberProblemStatementIdentified.ProblemStatementId = ProblemStatement.ProblemStatementId ");

            selectStatement.Append ("  WHERE MemberProblemStatementIdentified.MemberId = " + memberId.ToString ());

            if (!includeCompleted) { selectStatement.Append ("  AND MemberProblemStatementIdentified.CompletionDate IS NULL "); }

            selectStatement.Append ("  ORDER BY ProblemStatementName");


            System.Data.DataTable serviceTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in serviceTable.Rows) {

                Core.Individual.MemberProblemStatementIdentified problemStatement = new Core.Individual.MemberProblemStatementIdentified (this);

                problemStatement.MapDataFields (currentRow);

                results.Add (problemStatement);

            }

            return results;

        }

        #endregion 

        
        #region Care - Care Outcome

        public List<Core.Individual.CareOutcome> CareOutcomesAvailable () {

            List<Core.Individual.CareOutcome> results = new List<Core.Individual.CareOutcome> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("SELECT * FROM dbo.CareOutcome ORDER BY CareOutcomeName");


            System.Data.DataTable careOutcomeTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in careOutcomeTable.Rows) {

                Core.Individual.CareOutcome careOutcomeHeader = new Mercury.Server.Core.Individual.CareOutcome (this);

                careOutcomeHeader.MapDataFields (currentRow);

                results.Add (careOutcomeHeader);

            }

            return results;

        }


        public Core.Individual.CareOutcome CareOutcomeGet (Int64 forCareOutcomeId) {

            if (forCareOutcomeId == 0) { return null; }

            Core.Individual.CareOutcome careOutcome = null;

            SetLastException (null);

            try {

                careOutcome = new Mercury.Server.Core.Individual.CareOutcome (this, forCareOutcomeId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return careOutcome;

        }

        public Core.Individual.CareOutcome CareOutcomeGet (String careOutcomeName) {

            Int64 careOutcomeId = CareOutcomeGetIdByName (careOutcomeName);

            Core.Individual.CareOutcome careOutcome = null;

            if (careOutcomeId != 0) { careOutcome = CareOutcomeGet (careOutcomeId); }

            return careOutcome;

        }

        public Int64 CareOutcomeGetIdByName (String careOutcomeName) {

            Int64 careOutcomeId = 0;

            SetLastException (null);

            try {

                careOutcomeId = Convert.ToInt64 (EnvironmentDatabase.LookupValue ("CareOutcome", "CareOutcomeId", "CareOutcomeName = '" + careOutcomeName + "'"));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return careOutcomeId;

        }

        /// <summary>
        /// Get the Care Outcome Name by a given Care Outcome Id.
        /// </summary>
        /// <param name="careOutcomeId">Unique Identifier for the Care Outcome.</param>
        /// <returns>Care Outcome Name</returns>
        public String CareOutcomeGetNameById (Int64 careOutcomeId) {

            String careOutcomeName = String.Empty;

            SetLastException (null);

            try {

                careOutcomeName = (String)EnvironmentDatabase.LookupValue ("CareOutcome", "CareOutcomeName", "CareOutcomeId = " + careOutcomeId.ToString (), String.Empty);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return careOutcomeName;

        }

        #endregion


        #region Case - Member Case

        public Core.Individual.Case.MemberCase MemberCaseCreate (Int64 memberId, Boolean ignoreExisting, Boolean validatePermissions) {

            Core.Individual.Case.MemberCase memberCase = null;

            ClearLastException ();


            try {

                // VALIDATE MANAGER PERMISSION FOR MEMBER CASES

                if (!HasEnvironmentPermission (EnvironmentPermissions.MemberCaseManage)) { throw new ApplicationException ("Permission Denied. Cannot create a new member case."); }

                // VALIDATE MEMBER

                Core.Member.Member member = MemberGet (memberId);

                if (member == null) { throw new ApplicationException ("Permission Denied. Unable to load member from the database."); }


                System.Data.IDbCommand sqlCommand = environmentDatabase.CreateCommand ("dbo.MemberCase_Insert");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberId", memberId);

                EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@ignoreExisting", ignoreExisting);

                EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityName", session.SecurityAuthorityName, Server.Data.DataTypeConstants.Name);

                EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountId", session.UserAccountId, Server.Data.DataTypeConstants.UserAccountId);

                EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountName", session.UserAccountName, Server.Data.DataTypeConstants.Name);

                EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseId", ((Int64?)null));

                EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@RETURN", ((Int32)0));
                
                ((System.Data.IDbDataParameter) sqlCommand.Parameters["@memberCaseId"]).Direction = System.Data.ParameterDirection.Output;

                ((System.Data.IDbDataParameter) sqlCommand.Parameters["@RETURN"]).Direction = System.Data.ParameterDirection.ReturnValue;

                sqlCommand.ExecuteNonQuery ();

                if (Convert.ToInt32 (((System.Data.IDbDataParameter) sqlCommand.Parameters["@RETURN"]).Value) == 1) {

                    // LOOK FOR AN EXISTING OPEN MEMBER CASE 

                    throw new ApplicationException ("Existing Member Case found.");

                }

                else {

                    Int64 memberCaseId = Convert.ToInt64 (((System.Data.IDbDataParameter) sqlCommand.Parameters["@memberCaseId"]).Value);

                    memberCase = new Core.Individual.Case.MemberCase (this, memberCaseId);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberCase;

        }

        public Core.Individual.Case.MemberCase MemberCaseGet (Int64 memberCaseId) {

            if (memberCaseId == 0) { return null; }

            Core.Individual.Case.MemberCase memberCase = null;

            SetLastException (null);

            try {

                memberCase = new Mercury.Server.Core.Individual.Case.MemberCase (this, memberCaseId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberCase;

        }


        public List<Core.Individual.Case.Views.MemberCaseSummary> MemberCaseSummaryGetByMemberPage (Int64 memberId, Int32 initialRow, Int32 count, Boolean showClosed) {

            List<Core.Individual.Case.Views.MemberCaseSummary> results = new List<Core.Individual.Case.Views.MemberCaseSummary> ();

            System.Data.DataTable metricTable;

            String sqlStatement;


            SetLastException (null);

            try {

                sqlStatement = "EXEC MemberCaseSummary_SelectByMemberPage " + memberId.ToString () + ", " + initialRow.ToString () + ", " + count.ToString () + ", " + showClosed.ToString ();

                metricTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in metricTable.Rows) {

                    Core.Individual.Case.Views.MemberCaseSummary summary = new Core.Individual.Case.Views.MemberCaseSummary (this);

                    summary.MapDataFields (currentRow);

                    results.Add (summary);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return results;

        }
        
        public List<Core.Individual.Case.Views.MemberCaseSummary> MemberCaseSummaryGetByUserWorkTeamsPage (Int64 securityAuthorityId, String userAccountId, Int32 initialRow, Int32 count, Boolean showClosed) {

            List<Core.Individual.Case.Views.MemberCaseSummary> results = new List<Core.Individual.Case.Views.MemberCaseSummary> ();

            System.Data.DataTable metricTable;

            String sqlStatement;


            SetLastException (null);

            try {

                sqlStatement = "EXEC MemberCaseSummary_SelectByUserWorkTeamsPage " + securityAuthorityId.ToString () + ", '" + userAccountId + "', " + initialRow.ToString () + ", " + count.ToString () + ", " + showClosed.ToString ();

                metricTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in metricTable.Rows) {

                    Core.Individual.Case.Views.MemberCaseSummary summary = new Core.Individual.Case.Views.MemberCaseSummary (this);

                    summary.MapDataFields (currentRow);

                    results.Add (summary);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return results;

        }

        public List<Core.Individual.Case.Views.MemberCaseSummary> MemberCaseSummaryGetByAssignedToUserPage (Int64 securityAuthorityId, String userAccountId, Int32 initialRow, Int32 count, Boolean showClosed) {

            List<Core.Individual.Case.Views.MemberCaseSummary> results = new List<Core.Individual.Case.Views.MemberCaseSummary> ();

            System.Data.DataTable metricTable;

            String sqlStatement;


            SetLastException (null);

            try {

                sqlStatement = "EXEC MemberCaseSummary_SelectByAssignedToUserPage " + securityAuthorityId.ToString () + ", '" + userAccountId + "', " + initialRow.ToString () + ", " + count.ToString () + ", " + showClosed.ToString ();

                metricTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in metricTable.Rows) {

                    Core.Individual.Case.Views.MemberCaseSummary summary = new Core.Individual.Case.Views.MemberCaseSummary (this);

                    summary.MapDataFields (currentRow);

                    results.Add (summary);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return results;

        }


        public List<Core.Individual.Case.Views.MemberCaseLoadSummary> MemberCaseLoadSummaryGetByUser (Int64 securityAuthorityId, String userAccountId, Boolean showClosed) {

            List<Core.Individual.Case.Views.MemberCaseLoadSummary> results = new List<Core.Individual.Case.Views.MemberCaseLoadSummary> ();

            System.Data.DataTable dataTable;

            String sqlStatement;


            SetLastException (null);

            try {

                sqlStatement = "EXEC MemberCaseLoadSummary_SelectByUser " + securityAuthorityId.ToString () + ", '" + userAccountId.ToString () + "', " + showClosed.ToString ();

                dataTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in dataTable.Rows) {

                    Core.Individual.Case.Views.MemberCaseLoadSummary summary = new Core.Individual.Case.Views.MemberCaseLoadSummary ();

                    summary.MapDataFields (currentRow);

                    results.Add (summary);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return results;

        }

        public List<Core.Individual.Case.Views.MemberCaseLoadSummary> MemberCaseLoadSummaryGetByWorkTeam (Int64 workTeamId, Boolean showClosed) {

            List<Core.Individual.Case.Views.MemberCaseLoadSummary> results = new List<Core.Individual.Case.Views.MemberCaseLoadSummary> ();

            System.Data.DataTable dataTable;

            String sqlStatement;


            SetLastException (null);

            try {

                sqlStatement = "EXEC MemberCaseLoadSummary_SelectByWorkTeam " + workTeamId.ToString () + ", " + showClosed.ToString ();

                dataTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in dataTable.Rows) {

                    Core.Individual.Case.Views.MemberCaseLoadSummary summary = new Core.Individual.Case.Views.MemberCaseLoadSummary ();

                    summary.MapDataFields (currentRow);

                    results.Add (summary);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return results;

        }

        /* TODO: DAVID: ADDED ON 9/12/2011 (START) */

        public List<Core.Individual.Case.MemberCaseAudit> MemberCaseAuditHistoryGetByMemberCaseIdPage (Int64 memberCaseId, Int64 initialRow, Int64 count) {

            List<Core.Individual.Case.MemberCaseAudit> results = new List<Core.Individual.Case.MemberCaseAudit> ();

            System.Data.DataTable dataTable;

            String sqlStatement;


            SetLastException (null);

            try {

                sqlStatement = "SELECT * FROM (SELECT ROW_NUMBER () OVER (ORDER BY MemberCaseAudit.CreateDate DESC) AS RowNumber, ";

                sqlStatement = sqlStatement + "MemberCaseAudit.* FROM MemberCaseAudit ";

                sqlStatement = sqlStatement + "WHERE MemberCaseId = " + memberCaseId.ToString() + " ) AS MemberCaseAuditPage ";

                sqlStatement = sqlStatement + "WHERE MemberCaseAuditPage.RowNumber BETWEEN " + initialRow.ToString() + " AND " + (initialRow + count).ToString();

                dataTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in dataTable.Rows) {

                    Core.Individual.Case.MemberCaseAudit memberCaseAudit = new Core.Individual.Case.MemberCaseAudit (this);

                    memberCaseAudit.MapDataFields (currentRow);

                    results.Add (memberCaseAudit);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return results;

        }

        public Int64 MemberCaseAuditHistoryGetCount (Int64 memberCaseId) {

            Int64 itemCount = 0;

            SetLastException (null);

            try {

                String sqlStatement = "SELECT COUNT(1) AS CountOf FROM (SELECT * FROM MemberCaseAudit WHERE MemberCaseId = " + memberCaseId.ToString () + ") AS SourceTable";

                itemCount = Convert.ToInt64 (EnvironmentDatabase.ExecuteScalar (sqlStatement));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return itemCount;

        }

        /* TODO: DAVID: ADDED ON 9/12/2011 ( END ) */


        /* ADDED ON 11/10/11 (START) */

        public List<Server.Core.Individual.Case.Views.MemberCaseCarePlanSummary> MemberCaseCarePlanSummaryGetByMemberCase (Int64 memberCaseId) {

            List<Server.Core.Individual.Case.Views.MemberCaseCarePlanSummary> results = new List<Core.Individual.Case.Views.MemberCaseCarePlanSummary> ();

            System.Data.DataTable metricTable;

            String sqlStatement;


            SetLastException (null);

            try {

                sqlStatement = "EXEC MemberCaseCarePlanLoadSummary_SelectByMemberCase " + memberCaseId.ToString ();

                metricTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in metricTable.Rows) {

                    Core.Individual.Case.Views.MemberCaseCarePlanSummary summary = new Core.Individual.Case.Views.MemberCaseCarePlanSummary (this);

                    summary.MapDataFields (currentRow);

                    results.Add (summary);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return results;

        }

        /* ADDED ON 11/10/11 ( END ) */

        #endregion 

        #endregion


        #region Core.Insurer

        #region Insurer - Benefit Plan

        public List<Core.Insurer.BenefitPlan> BenefitPlansAvailable () {
            
            List<Core.Insurer.BenefitPlan> available = new List<Core.Insurer.BenefitPlan> ();

            String selectStatement = "SELECT * FROM dal.BenefitPlan ORDER BY BenefitPlanName";


            ClearLastException ();

            System.Data.DataTable objectTable = EnvironmentDatabase.SelectDataTable (selectStatement);

            foreach (System.Data.DataRow currentRow in objectTable.Rows) {

                Core.Insurer.BenefitPlan availableObject = new Mercury.Server.Core.Insurer.BenefitPlan (this);

                availableObject.MapDataFields (currentRow);

                available.Add (availableObject);

            }

            return available;

        }

        public Dictionary<Int64, String> BenefitPlanDictionary () { return CoreObjectDictionary ("BenefitPlan"); }

        public Core.Insurer.BenefitPlan BenefitPlanGet (Int64 forBenefitPlanId) {

            if (forBenefitPlanId == 0) { return null; }


            Core.Insurer.BenefitPlan benefitPlan = null;

            SetLastException (null);

            try {

                benefitPlan = new Mercury.Server.Core.Insurer.BenefitPlan (this, forBenefitPlanId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return benefitPlan;

        }

        public Core.Insurer.BenefitPlan BenefitPlanGet (String benefitPlanName) {

            if (String.IsNullOrWhiteSpace (benefitPlanName)) { return null; }


            Int64 benefitPlanId = CoreObjectGetIdByName ("BenefitPlan", benefitPlanName);

            Core.Insurer.BenefitPlan benefitPlan = null;

            if (benefitPlanId != 0) { benefitPlan = BenefitPlanGet (benefitPlanId); }

            return benefitPlan;

        }

        #endregion 
        

        #region Insurer - Contract 

        public List<Core.Insurer.Contract> ContractsAvailable () {

            List<Core.Insurer.Contract> results = new List<Core.Insurer.Contract> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("SELECT * FROM dbo.Contract ORDER BY ContractName");


            System.Data.DataTable contractTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in contractTable.Rows) {

                Core.Insurer.Contract contractHeader = new Mercury.Server.Core.Insurer.Contract (this);

                contractHeader.MapDataFields (currentRow);

                results.Add (contractHeader);

            }

            return results;

        }

        public Dictionary<Int64, String> ContractDictionary () {

            Dictionary<Int64, String> contractDictionary = new Dictionary<Int64, String> ();

            SetLastException (null);

            try {

                StringBuilder selectStatement = new StringBuilder ("SELECT ContractId, ContractName FROM Contract ORDER BY ContractName");

                System.Data.DataTable contractTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

                foreach (System.Data.DataRow currentContract in contractTable.Rows) {

                    contractDictionary.Add ((Int64)currentContract["ContractId"], (String)currentContract["ContractName"]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return contractDictionary;

        }

        public Core.Insurer.Contract ContractGet (Int64 forContractId) {

            if (forContractId == 0) { return null; }


            Core.Insurer.Contract contract = null;

            SetLastException (null);

            try {

                contract = new Mercury.Server.Core.Insurer.Contract (this, forContractId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return contract;

        }

        public Core.Insurer.Contract ContractGet (String contractName) {

            if (String.IsNullOrWhiteSpace (contractName)) { return null; }


            Int64 contractId = CoreObjectGetIdByName ("Contract", contractName);

            Core.Insurer.Contract contract = null;

            if (contractId != 0) { contract = ContractGet (contractId); }

            return contract;

        }

        #endregion 


        #region Insurer - Coverage Level

        public List<Core.Insurer.CoverageLevel> CoverageLevelsAvailable () {

            List<Core.Insurer.CoverageLevel> results = new List<Core.Insurer.CoverageLevel> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("SELECT * FROM dbo.CoverageLevel ORDER BY CoverageLevelName");


            System.Data.DataTable coverageLevelTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in coverageLevelTable.Rows) {

                Core.Insurer.CoverageLevel coverageLevelHeader = new Mercury.Server.Core.Insurer.CoverageLevel (this);

                coverageLevelHeader.MapDataFields (currentRow);

                results.Add (coverageLevelHeader);

            }

            return results;

        }

        public Dictionary<Int64, String> CoverageLevelDictionary () {

            Dictionary<Int64, String> coverageLevelDictionary = new Dictionary<Int64, String> ();

            SetLastException (null);

            try {

                StringBuilder selectStatement = new StringBuilder ("SELECT CoverageLevelId, CoverageLevelName FROM CoverageLevel ORDER BY CoverageLevelName");

                System.Data.DataTable coverageLevelTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

                foreach (System.Data.DataRow currentCoverageLevel in coverageLevelTable.Rows) {

                    coverageLevelDictionary.Add ((Int64)currentCoverageLevel["CoverageLevelId"], (String)currentCoverageLevel["CoverageLevelName"]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return coverageLevelDictionary;

        }

        public Core.Insurer.CoverageLevel CoverageLevelGet (Int64 forCoverageLevelId) {

            if (forCoverageLevelId == 0) { return null; }


            Core.Insurer.CoverageLevel coverageLevel = null;

            SetLastException (null);

            try {

                coverageLevel = new Mercury.Server.Core.Insurer.CoverageLevel (this, forCoverageLevelId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return coverageLevel;

        }

        public Core.Insurer.CoverageLevel CoverageLevelGet (String coverageLevelName) {

            if (String.IsNullOrWhiteSpace (coverageLevelName)) { return null; }


            Int64 coverageLevelId = CoreObjectGetIdByName ("CoverageLevel", coverageLevelName);

            Core.Insurer.CoverageLevel coverageLevel = null;

            if (coverageLevelId != 0) { coverageLevel = CoverageLevelGet (coverageLevelId); }

            return coverageLevel;

        }

        #endregion 


        #region Insurer - Coverage Type

        public List<Core.Insurer.CoverageType> CoverageTypesAvailable () {

            List<Core.Insurer.CoverageType> results = new List<Core.Insurer.CoverageType> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("SELECT * FROM dbo.CoverageType ORDER BY CoverageTypeName");


            System.Data.DataTable coverageTypeTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in coverageTypeTable.Rows) {

                Core.Insurer.CoverageType coverageTypeHeader = new Mercury.Server.Core.Insurer.CoverageType (this);

                coverageTypeHeader.MapDataFields (currentRow);

                results.Add (coverageTypeHeader);

            }

            return results;

        }

        public Dictionary<Int64, String> CoverageTypeDictionary () {

            Dictionary<Int64, String> coverageTypeDictionary = new Dictionary<Int64, String> ();

            SetLastException (null);

            try {

                StringBuilder selectStatement = new StringBuilder ("SELECT CoverageTypeId, CoverageTypeName FROM CoverageType ORDER BY CoverageTypeName");

                System.Data.DataTable coverageTypeTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

                foreach (System.Data.DataRow currentCoverageType in coverageTypeTable.Rows) {

                    coverageTypeDictionary.Add ((Int64)currentCoverageType["CoverageTypeId"], (String)currentCoverageType["CoverageTypeName"]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return coverageTypeDictionary;

        }

        public Core.Insurer.CoverageType CoverageTypeGet (Int64 forCoverageTypeId) {

            if (forCoverageTypeId == 0) { return null; }


            Core.Insurer.CoverageType coverageType = null;

            SetLastException (null);

            try {

                coverageType = new Mercury.Server.Core.Insurer.CoverageType (this, forCoverageTypeId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return coverageType;

        }

        public Core.Insurer.CoverageType CoverageTypeGet (String coverageTypeName) {

            if (String.IsNullOrWhiteSpace (coverageTypeName)) { return null; }


            Int64 coverageTypeId = CoreObjectGetIdByName ("CoverageType", coverageTypeName);

            Core.Insurer.CoverageType coverageType = null;

            if (coverageTypeId != 0) { coverageType = CoverageTypeGet (coverageTypeId); }

            return coverageType;

        }

        #endregion 


        #region Insurer - Insurance Type

        public List<Core.Insurer.InsuranceType> InsuranceTypesAvailable () {

            List<Core.Insurer.InsuranceType> results = new List<Core.Insurer.InsuranceType> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("SELECT * FROM dbo.InsuranceType ORDER BY InsuranceTypeName");


            System.Data.DataTable insuranceTypeTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in insuranceTypeTable.Rows) {

                Core.Insurer.InsuranceType insuranceTypeHeader = new Mercury.Server.Core.Insurer.InsuranceType (this);

                insuranceTypeHeader.MapDataFields (currentRow);

                results.Add (insuranceTypeHeader);

            }

            return results;

        }

        public Dictionary<Int64, String> InsuranceTypeDictionary () {

            Dictionary<Int64, String> insuranceTypeDictionary = new Dictionary<Int64, String> ();

            SetLastException (null);

            try {

                StringBuilder selectStatement = new StringBuilder ("SELECT InsuranceTypeId, InsuranceTypeName FROM InsuranceType ORDER BY InsuranceTypeName");

                System.Data.DataTable insuranceTypeTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

                foreach (System.Data.DataRow currentInsuranceType in insuranceTypeTable.Rows) {

                    insuranceTypeDictionary.Add ((Int64) currentInsuranceType["InsuranceTypeId"], (String) currentInsuranceType["InsuranceTypeName"]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return insuranceTypeDictionary;

        }

        public Core.Insurer.InsuranceType InsuranceTypeGet (Int64 forInsuranceTypeId) {

            if (forInsuranceTypeId == 0) { return null; }


            Core.Insurer.InsuranceType insuranceType = null;

            SetLastException (null);

            try {

                insuranceType = new Mercury.Server.Core.Insurer.InsuranceType (this, forInsuranceTypeId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return insuranceType;

        }

        public Core.Insurer.InsuranceType InsuranceTypeGet (String insuranceTypeName) {

            if (String.IsNullOrWhiteSpace (insuranceTypeName)) { return null; }


            Int64 insuranceTypeId = CoreObjectGetIdByName ("InsuranceType", insuranceTypeName);

            Core.Insurer.InsuranceType insuranceType = null;

            if (insuranceTypeId != 0) { insuranceType = InsuranceTypeGet (insuranceTypeId); }

            return insuranceType;

        }

        #endregion 
                
        
        #region Insurer - Insurer

        public List<Core.Insurer.Insurer> InsurersAvailable () {

            List<Core.Insurer.Insurer> results = new List<Core.Insurer.Insurer> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("SELECT * FROM dbo.Insurer ORDER BY InsurerName");


            System.Data.DataTable insurerTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in insurerTable.Rows) {

                Core.Insurer.Insurer insurerHeader = new Mercury.Server.Core.Insurer.Insurer (this);

                insurerHeader.MapDataFields (currentRow);

                results.Add (insurerHeader);

            }

            return results;

        }

        public Dictionary<Int64, String> InsurerDictionary () {

            Dictionary<Int64, String> insurerDictionary = new Dictionary<Int64, String> ();

            SetLastException (null);

            try {

                StringBuilder selectStatement = new StringBuilder ("SELECT InsurerId, InsurerName FROM Insurer ORDER BY InsurerName");

                System.Data.DataTable insurerTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

                foreach (System.Data.DataRow currentInsurer in insurerTable.Rows) {

                    insurerDictionary.Add ((Int64)currentInsurer["InsurerId"], (String)currentInsurer["InsurerName"]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return insurerDictionary;

        }

        public Core.Insurer.Insurer InsurerGet (Int64 forInsurerId) {

            if (forInsurerId == 0) { return null; }


            Core.Insurer.Insurer insurer = null;

            SetLastException (null);

            try {

                insurer = new Mercury.Server.Core.Insurer.Insurer (this, forInsurerId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return insurer;

        }

        public Core.Insurer.Insurer InsurerGet (String insurerName) {

            if (String.IsNullOrWhiteSpace (insurerName)) { return null; }


            Int64 insurerId = CoreObjectGetIdByName ("Insurer", insurerName);

            Core.Insurer.Insurer insurer = null;

            if (insurerId != 0) { insurer = InsurerGet (insurerId); }

            return insurer;

        }

        #endregion 


        #region Insurer - Program

        public List<Core.Insurer.Program> ProgramsAvailable () {

            List<Core.Insurer.Program> available = new List<Core.Insurer.Program> ();

            String selectStatement = "SELECT * FROM dal.Program ORDER BY ProgramName";


            ClearLastException ();

            System.Data.DataTable objectTable = EnvironmentDatabase.SelectDataTable (selectStatement);

            foreach (System.Data.DataRow currentRow in objectTable.Rows) {

                Core.Insurer.Program availableObject = new Mercury.Server.Core.Insurer.Program (this);

                availableObject.MapDataFields (currentRow);

                available.Add (availableObject);

            }

            return available;

        }

        public List<Core.Insurer.Program> ProgramsAvailableByInsurer (Int64 insurerId) {

            List<Core.Insurer.Program> availablePrograms = new List<Core.Insurer.Program> ();

            List<Core.Insurer.Program> filteredPrograms = new List<Core.Insurer.Program> ();


            foreach (Core.Insurer.Program currentProgram in availablePrograms) {

                if (currentProgram.InsurerId == insurerId) {

                    filteredPrograms.Add (currentProgram);

                }

            }


            return filteredPrograms;
            
        }

        public List<Core.Insurer.Program> ProgramsAvailableByInsurerProgramName (Int64 insurerId, String programNamePrefix) {

            List<Core.Insurer.Program> availablePrograms = new List<Core.Insurer.Program> ();

            List<Core.Insurer.Program> filteredPrograms = new List<Core.Insurer.Program> ();


            foreach (Core.Insurer.Program currentProgram in availablePrograms) {

                if ((currentProgram.InsurerId == insurerId) && (currentProgram.Name.StartsWith (programNamePrefix))) {

                    filteredPrograms.Add (currentProgram);

                }

            }


            return filteredPrograms;

        }


        public Dictionary<Int64, String> ProgramDictionary () {

            Dictionary<Int64, String> programDictionary = new Dictionary<Int64, String> ();

            SetLastException (null);

            try {

                StringBuilder selectStatement = new StringBuilder ("SELECT ProgramId, ProgramName FROM Program ORDER BY ProgramName");

                System.Data.DataTable programTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

                foreach (System.Data.DataRow currentProgram in programTable.Rows) {

                    programDictionary.Add ((Int64)currentProgram["ProgramId"], (String)currentProgram["ProgramName"]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return programDictionary;

        }

        public Core.Insurer.Program ProgramGet (Int64 forProgramId) {

            if (forProgramId == 0) { return null; }


            Core.Insurer.Program program = null;

            SetLastException (null);

            try {

                program = new Mercury.Server.Core.Insurer.Program (this, forProgramId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return program;

        }

        public Core.Insurer.Program ProgramGet (String programName) {

            if (String.IsNullOrWhiteSpace (programName)) { return null; }


            Int64 programId = CoreObjectGetIdByName ("Program", programName);

            Core.Insurer.Program program = null;

            if (programId != 0) { program = ProgramGet (programId); }

            return program;

        }

        

        #endregion 

        #endregion


        #region Core.MedicalServices

        public List<Core.Search.SearchResultMedicalServiceHeader> MedicalServiceHeadersGet () {

            List<Core.Search.SearchResultMedicalServiceHeader> results = new List<Core.Search.SearchResultMedicalServiceHeader> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("SELECT * FROM Service ORDER BY ServiceName ");


            System.Data.DataTable serviceTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in serviceTable.Rows) {

                Mercury.Server.Core.Search.SearchResultMedicalServiceHeader serviceHeader = new Mercury.Server.Core.Search.SearchResultMedicalServiceHeader ();

                serviceHeader.MapDataFields (currentRow);

                results.Add (serviceHeader);

            }

            return results;

        }

        public List<Core.Search.SearchResultMedicalServiceHeader> MedicalServiceHeadersGetByType (Core.MedicalServices.Enumerations.MedicalServiceType serviceType) {

            List<Core.Search.SearchResultMedicalServiceHeader> results = new List<Core.Search.SearchResultMedicalServiceHeader> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("SELECT * FROM Service");

            selectStatement.Append ("  WHERE ServiceType = " + ((Int32)serviceType).ToString ());

            selectStatement.Append ("  ORDER BY ServiceName");


            System.Data.DataTable serviceTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in serviceTable.Rows) {

                Mercury.Server.Core.Search.SearchResultMedicalServiceHeader serviceHeader = new Mercury.Server.Core.Search.SearchResultMedicalServiceHeader ();

                serviceHeader.MapDataFields (currentRow);

                results.Add (serviceHeader);

            }

            return results;

        }


        public Core.MedicalServices.Service MedicalServiceGet (Int64 serviceId) {

            if (serviceId == 0) { return null; }

            Core.MedicalServices.Service medicalService = null;

            SetLastException (null);

            try {

                if (serviceId != 0) {

                    medicalService = new Mercury.Server.Core.MedicalServices.Service (this, serviceId);

                    switch (medicalService.ServiceType) {

                        case Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType.Singleton:

                            medicalService = new Mercury.Server.Core.MedicalServices.ServiceSingleton (this, serviceId);

                            break;

                        case Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType.Set:

                            medicalService = new Mercury.Server.Core.MedicalServices.ServiceSet (this, serviceId);

                            break;

                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return medicalService;

        }

        public Core.MedicalServices.Service MedicalServiceGet (String serviceName) {

            if (String.IsNullOrEmpty (serviceName)) { return null; }

            Core.MedicalServices.Service medicalService = null;

            SetLastException (null);

            try {

                Int64 serviceId = CoreObjectGetIdByName ("Service", serviceName);

                medicalService = MedicalServiceGet (serviceId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return medicalService;

        }
    

        public Core.MedicalServices.ServiceSingleton MedicalServiceSingletonGet (Int64 serviceId) {

            if (serviceId == 0) { return null; }

            Core.MedicalServices.ServiceSingleton medicalService = null;

            SetLastException (null);

            try {

                medicalService = new Mercury.Server.Core.MedicalServices.ServiceSingleton (this, serviceId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return medicalService;

        }

        public Core.MedicalServices.ServiceSet MedicalServiceSetGet (Int64 serviceId) {

            if (serviceId == 0) { return null; }

            Core.MedicalServices.ServiceSet medicalService = null;

            SetLastException (null);

            try {

                medicalService = new Mercury.Server.Core.MedicalServices.ServiceSet (this, serviceId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return medicalService;

        }


        #region Member Service

        public Core.MedicalServices.MemberService MemberServiceGet (Int64 memberServiceId) {

            if (memberServiceId == 0) { return null; }

            Core.MedicalServices.MemberService memberService = null;

            SetLastException (null);

            try {

                memberService = new Mercury.Server.Core.MedicalServices.MemberService (this, memberServiceId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberService;

        }

        public List<Int64> MemberServiceGetIdListByMember (Int64 forMemberId) {

            List<Int64> memberServices = new List<Int64> ();

            System.Data.DataTable servicesTable = EnvironmentDatabase.SelectDataTable ("SELECT MemberServiceId FROM MemberService WHERE MemberId = " + forMemberId.ToString () + " ORDER BY EventDate DESC");

            foreach (System.Data.DataRow currentRow in servicesTable.Rows) {

                memberServices.Add ((Int64)currentRow[0]);

            }

            return memberServices;

        }

        public Int64 MemberServiceGetId (Int64 forMemberId, Int64 forServiceId, DateTime forEventDate) {

            StringBuilder selectStatement = new StringBuilder ();

            selectStatement.Append ("EXEC MemberService_SelectByMemberEvent ");

            selectStatement.Append (forMemberId.ToString () + ", ");

            selectStatement.Append (forServiceId.ToString () + ", ");

            selectStatement.Append ("'" + forEventDate.ToString ("MM/dd/yyyy") + "'");

            System.Data.DataTable memberServiceTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            if (memberServiceTable.Rows.Count == 0) { return 0; }

            return (Int64)memberServiceTable.Rows[0][0];

        }


        public Core.MedicalServices.MemberService MemberServiceGetByMemberMostRecent (Int64 forMemberId, String serviceName) {

            Core.MedicalServices.MemberService memberService = null;


            StringBuilder selectStatement = new StringBuilder ();

            selectStatement.Append ("EXEC MemberService_SelectByMemberMostRecent ");

            selectStatement.Append (forMemberId.ToString () + ", ");

            selectStatement.Append ("0, ");

            selectStatement.Append ("'" + serviceName.Replace ("'", "''") + "'");

            System.Data.DataTable memberServiceTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            if (memberServiceTable.Rows.Count == 0) { return null; }

            memberService = new Mercury.Server.Core.MedicalServices.MemberService (this);

            memberService.MapDataFields (memberServiceTable.Rows[0]);

            return memberService;

        }

        public Boolean MemberServiceAddManual (Int64 memberId, Int64 serviceId, DateTime eventDate, Boolean validatePermission) {

            Boolean success = false;

            String sqlStatement;


            SetLastException (null);

            try {

                if ((!HasEnvironmentPermission (EnvironmentPermissions.MemberServiceManage)) && (validatePermission)) {

                    throw new ApplicationException ("Permission Denied when adding Service to Member Manually.");

                }

                Data.AuthorityAccountStamp modified = new Mercury.Server.Data.AuthorityAccountStamp (session);

                sqlStatement = "MemberService_AddManual " + memberId.ToString () + ", " + serviceId.ToString () + ", '" + eventDate.ToString ("MM/dd/yyyy") + "', ";

                sqlStatement = sqlStatement + "'" + modified.SecurityAuthorityNameSql + "', '" + modified.UserAccountIdSql + "', '" + modified.UserAccountNameSql + "'";

                success = EnvironmentDatabase.ExecuteSqlStatement (sqlStatement, 0);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

                success = false;

            }

            return success;

        }

        public Boolean MemberServiceRemoveManual (Int64 memberServiceId) {

            Boolean success = false;

            String sqlStatement;


            SetLastException (null);

            try {

                if (!HasEnvironmentPermission (EnvironmentPermissions.MemberServiceManage)) {

                    throw new ApplicationException ("Permission Denied when trying to remove Service from Member.");

                }

                Data.AuthorityAccountStamp modified = new Mercury.Server.Data.AuthorityAccountStamp (session);

                sqlStatement = "MemberService_RemoveManual " + memberServiceId.ToString ();

                success = EnvironmentDatabase.ExecuteSqlStatement (sqlStatement, 0);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

                success = false;

            }

            return success;

        }


        public Int64 MemberServicesGetCount (Int64 memberId, Boolean showHidden) {

            String sqlStatement;

            Int64 itemCount = 0;

            SetLastException (null);

            try {

                sqlStatement = "MemberService_Count " + memberId.ToString () + ", " + showHidden.ToString ();

                itemCount = Convert.ToInt64 (EnvironmentDatabase.StoredProcedureReturnValue (sqlStatement));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return itemCount;

        }

        public List<Core.MedicalServices.MemberService> MemberServicesGetByPage (Int64 memberId, Int32 initialRow, Int32 count, Boolean showHidden) {

            List<Core.MedicalServices.MemberService> memberServices = new List<Mercury.Server.Core.MedicalServices.MemberService> ();

            System.Data.DataTable serviceTable;

            String sqlStatement;


            SetLastException (null);

            try {

                sqlStatement = "EXEC MemberService_SelectByMemberPage " + memberId.ToString () + ", " + initialRow.ToString () + ", " + count.ToString () + ", " + showHidden.ToString ();

                serviceTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in serviceTable.Rows) {

                    Core.MedicalServices.MemberService memberService = new Mercury.Server.Core.MedicalServices.MemberService (this);

                    memberService.MapDataFields (currentRow);

                    memberServices.Add (memberService);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberServices;

        }

        public List<Core.MedicalServices.MemberServiceDetailSingleton> MemberServiceDetailSingletonGet (Int64 memberServiceId) {

            List<Core.MedicalServices.MemberServiceDetailSingleton> serviceDetails = new List<Mercury.Server.Core.MedicalServices.MemberServiceDetailSingleton> ();

            System.Data.DataTable detailTable;

            String sqlStatement;


            SetLastException (null);

            try {

                sqlStatement = "SELECT * FROM MemberServiceDetailSingleton WHERE MemberServiceId = " + memberServiceId.ToString () + " ORDER BY EventDate DESC, ClaimId, ClaimLine";

                detailTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in detailTable.Rows) {

                    Core.MedicalServices.MemberServiceDetailSingleton detail = new Mercury.Server.Core.MedicalServices.MemberServiceDetailSingleton (memberServiceId, (Int64)currentRow["ServiceSingletonDefinitionId"]);

                    detail.MapDataFields (currentRow);

                    serviceDetails.Add (detail);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return serviceDetails;

        }

        public List<Core.MedicalServices.MemberServiceDetailSet> MemberServiceDetailSetGet (Int64 memberServiceId) {

            List<Core.MedicalServices.MemberServiceDetailSet> serviceDetails = new List<Mercury.Server.Core.MedicalServices.MemberServiceDetailSet> ();

            System.Data.DataTable detailTable;

            String sqlStatement;


            SetLastException (null);

            try {

                sqlStatement = "SELECT * FROM MemberServiceDetailSet WHERE MemberServiceId = " + memberServiceId.ToString () + " ORDER BY EventDate DESC, ServiceName";

                detailTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in detailTable.Rows) {

                    Core.MedicalServices.MemberServiceDetailSet detail = new Mercury.Server.Core.MedicalServices.MemberServiceDetailSet (memberServiceId, (Int64)currentRow["ServiceSetDefinitionId"]);

                    detail.MapDataFields (currentRow);

                    serviceDetails.Add (detail);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return serviceDetails;

        }


        private String MemberServicesSelectSqlStatement (List<Data.FilterDescriptor> filters) {
            
            String sqlStatement = String.Empty;


            String selectClause = String.Empty;

            String fromClause = String.Empty;

            String whereClause = String.Empty;

            String orderByClause = String.Empty;



            selectClause = "SELECT * \r\n";

            fromClause = "  FROM dbo.MemberService \r\n";

            whereClause = "  WHERE (MemberService.MemberServiceId <> 0) /*_CUSTOM_FILTER_INSERT_*/";
            

            if (filters == null) { filters = new List<Mercury.Server.Data.FilterDescriptor> (); }

            foreach (Data.FilterDescriptor currentFilter in filters) {

                String criteriaString = String.Empty;

                
                switch (currentFilter.PropertyPath) {

                    #region Core Object Properties

                    case "Id":

                        currentFilter.PropertyPath = "MemberService" + currentFilter.PropertyPath;

                        criteriaString = currentFilter.SqlCriteriaString (String.Empty);

                        break;

                    #endregion 

                    #region Object Properties

                    case "MemberId": 

                    case "ServiceId":

                    case "EventDate":

                        criteriaString = currentFilter.SqlCriteriaString (String.Empty); 
                        
                        break;

                    #endregion 

                } // switch (currentFilter.PropertyPath) {

                if (!String.IsNullOrEmpty (criteriaString)) {

                    whereClause = whereClause + "  AND " + criteriaString;

                }

            }


            sqlStatement = selectClause + fromClause + whereClause + orderByClause;

            return sqlStatement;

        }

        private String MemberServicesSelectRowNumberSql (List<Data.SortDescriptor> sorts) {

            String sqlString = String.Empty;


            if (sorts == null) { sorts = new List<Data.SortDescriptor> (); }

            foreach (Data.SortDescriptor currentSort in sorts) {

                sqlString = sqlString + currentSort.SqlSortList + ", ";

            }
            
            sqlString = "    ROW_NUMBER () OVER (ORDER BY \r\n  " + sqlString;

            sqlString = sqlString + "MemberServiceId) AS RowNumber, \r\n  ";

            return sqlString;

        }

        public Int64 MemberServicesSelectCount (List<Data.FilterDescriptor> filters) { 
        
            Int64 itemCount = 0;


            SetLastException (null);

            try {

                String sqlStatement = "SELECT COUNT (1) AS CountOf FROM (" + MemberServicesSelectSqlStatement (filters) + ") AS SourceTable";

                itemCount = Convert.ToInt64 (EnvironmentDatabase.ExecuteScalar (sqlStatement));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return itemCount;
        }

        public List<Core.MedicalServices.MemberService> MemberServicesSelect (List<Data.FilterDescriptor> filters, List<Data.SortDescriptor> sorts, Int64 initialRow, Int64 count) {
            
            List<Core.MedicalServices.MemberService> items = new List<Core.MedicalServices.MemberService> ();

            System.Data.DataTable itemTable;

            String sqlStatement = String.Empty;

            String customFields = String.Empty;


            ClearLastException ();

            try {

                sqlStatement = sqlStatement + "SELECT MemberServicePage.* FROM ( \r\n";

                sqlStatement = sqlStatement + "SELECT \r\n ";

                sqlStatement = sqlStatement + MemberServicesSelectRowNumberSql (sorts);

                sqlStatement = sqlStatement + "    MemberService.* \r\n";

                sqlStatement = sqlStatement + "  FROM (" + MemberServicesSelectSqlStatement (filters) + ") AS MemberService \r\n";

                sqlStatement = sqlStatement + ") AS MemberServicePage \r\n";


                if (count >= 0) {

                    sqlStatement = sqlStatement + "  WHERE MemberServicePage.RowNumber BETWEEN " + initialRow.ToString ();

                    sqlStatement = sqlStatement + " AND (" + initialRow.ToString () + " + " + count.ToString () + " - 1)";

                }


                itemTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in itemTable.Rows) {

                    Core.MedicalServices.MemberService item = new Core.MedicalServices.MemberService (this);

                    item.MapDataFields (currentRow);

                    items.Add (item);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return items;

        }

        #endregion 

        #endregion


        #region Core.Member

        #region Member

        public Mercury.Server.Core.Member.Member MemberGet (Int64 memberId, Boolean useCaching = false) {

            if (memberId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".Member." + memberId.ToString ();

            Mercury.Server.Core.Member.Member member = null;

            ClearLastException ();

            try {

                member = (Core.Member.Member)CacheManager.GetObject (cacheKey);

                if (!useCaching) { member = null; }

                if (member == null) {

                    member = new Mercury.Server.Core.Member.Member (this, memberId);

                    if (member != null) { CacheManager.CacheObject (cacheKey, member, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return member;

        }

        public Mercury.Server.Core.Member.Member MemberGetByEntityId (Int64 entityId) {

            if (entityId == 0) { return null; }

            Mercury.Server.Core.Member.Member member = null;

            SetLastException (null);

            try {

                System.Data.DataTable memberTable = EnvironmentDatabase.SelectDataTable ("EXEC dal.Member_SelectByEntityId " + entityId.ToString ());

                if (memberTable.Rows.Count == 1) {

                    member = new Mercury.Server.Core.Member.Member (this);

                    member.MapDataFields (memberTable.Rows[0]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return member;

        }

        #endregion 


        #region Member Enrollment

        public List<Core.Member.MemberEnrollment> MemberEnrollmentsGet (Int64 memberId) {

            List<Core.Member.MemberEnrollment> enrollments = new List<Core.Member.MemberEnrollment> ();

            SetLastException (null);


            try {

                StringBuilder selectStatement = new StringBuilder ();

                selectStatement.Append ("EXEC dal.MemberEnrollments_Select " + memberId.ToString ());


                System.Data.DataTable enrollmentTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

                foreach (System.Data.DataRow currentRow in enrollmentTable.Rows) {

                    Core.Member.MemberEnrollment enrollment = new Mercury.Server.Core.Member.MemberEnrollment (this);

                    enrollment.MapDataFields (currentRow);

                    enrollments.Add (enrollment);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return enrollments;

        }

        public Mercury.Server.Core.Member.MemberEnrollment MemberEnrollmentGet (Int64 memberEnrollmentId) {

            Mercury.Server.Core.Member.MemberEnrollment enrollment = null;

            SetLastException (null);

            try {

                enrollment = new Mercury.Server.Core.Member.MemberEnrollment (this, memberEnrollmentId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return enrollment;

        }


        public List<Core.Member.MemberEnrollmentCoverage> MemberEnrollmentCoveragesGet (Int64 memberEnrollmentId) {

            List<Core.Member.MemberEnrollmentCoverage> enrollmentCoverages = new List<Core.Member.MemberEnrollmentCoverage> ();

            SetLastException (null);


            try {

                StringBuilder selectStatement = new StringBuilder ();

                selectStatement.Append ("EXEC dal.MemberEnrollmentCoverages_Select " + memberEnrollmentId.ToString ());


                System.Data.DataTable enrollmentCoverageTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

                foreach (System.Data.DataRow currentRow in enrollmentCoverageTable.Rows) {

                    Core.Member.MemberEnrollmentCoverage enrollmentCoverage = new Mercury.Server.Core.Member.MemberEnrollmentCoverage (this);

                    enrollmentCoverage.MapDataFields (currentRow);

                    enrollmentCoverages.Add (enrollmentCoverage);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return enrollmentCoverages;

        }

        public Core.Member.MemberEnrollmentCoverage MemberEnrollmentCoverageGet (Int64 memberEnrollmentCoverageId) {

            Mercury.Server.Core.Member.MemberEnrollmentCoverage enrollmentCoverage = null;

            SetLastException (null);

            try {

                enrollmentCoverage = new Mercury.Server.Core.Member.MemberEnrollmentCoverage (this, memberEnrollmentCoverageId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return enrollmentCoverage;

        }


        public List<Core.Member.MemberEnrollmentPcp> MemberEnrollmentPcpsGet (Int64 memberEnrollmentId) {

            List<Core.Member.MemberEnrollmentPcp> pcps = new List<Core.Member.MemberEnrollmentPcp> ();

            SetLastException (null);


            try {

                StringBuilder selectStatement = new StringBuilder ();

                selectStatement.Append ("EXEC dal.MemberEnrollmentPcps_Select " + memberEnrollmentId.ToString ());


                System.Data.DataTable pcpTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

                foreach (System.Data.DataRow currentRow in pcpTable.Rows) {

                    Core.Member.MemberEnrollmentPcp pcp = new Mercury.Server.Core.Member.MemberEnrollmentPcp (this);

                    pcp.MapDataFields (currentRow);

                    pcps.Add (pcp);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return pcps;

        }

        public Mercury.Server.Core.Member.MemberEnrollmentPcp MemberEnrollmentPcpGet (Int64 memberEnrollmentPcpId) {

            Mercury.Server.Core.Member.MemberEnrollmentPcp pcp = null;

            SetLastException (null);

            try {

                pcp = new Mercury.Server.Core.Member.MemberEnrollmentPcp (this, memberEnrollmentPcpId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return pcp;

        }


        public List<Core.Member.MemberEnrollmentTplCob> MemberEnrollmentTplCobsGet (Int64 memberId) {

            List<Core.Member.MemberEnrollmentTplCob> enrollments = new List<Core.Member.MemberEnrollmentTplCob> ();

            SetLastException (null);


            try {

                StringBuilder selectStatement = new StringBuilder ();

                selectStatement.Append ("EXEC dal.MemberEnrollmentTplCobs_Select " + memberId.ToString ());


                System.Data.DataTable enrollmentTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

                foreach (System.Data.DataRow currentRow in enrollmentTable.Rows) {

                    Core.Member.MemberEnrollmentTplCob enrollment = new Mercury.Server.Core.Member.MemberEnrollmentTplCob (this);

                    enrollment.MapDataFields (currentRow);

                    enrollments.Add (enrollment);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return enrollments;

        }

        public Mercury.Server.Core.Member.MemberEnrollmentTplCob MemberEnrollmentTplCobGet (Int64 enrollmentTplCobId) {

            Mercury.Server.Core.Member.MemberEnrollmentTplCob enrollment = null;

            SetLastException (null);

            try {

                enrollment = new Mercury.Server.Core.Member.MemberEnrollmentTplCob (this, enrollmentTplCobId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return enrollment;

        }

        #endregion 


        #region Member Relationship

        public Core.Member.MemberRelationship MemberRelationshipGet (Int64 memberRelationshipId) {

            if (memberRelationshipId == 0) { return null; }


            Mercury.Server.Core.Member.MemberRelationship memberRelationship = null;

            SetLastException (null);

            try {

                memberRelationship = new Mercury.Server.Core.Member.MemberRelationship (this, memberRelationshipId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberRelationship;

        }

        public List<Core.Member.MemberRelationship> MemberRelationshipsGet (Int64 memberId) {

            List<Core.Member.MemberRelationship> memberRelationships = new List<Core.Member.MemberRelationship> ();

            SetLastException (null);


            try {

                StringBuilder selectStatement = new StringBuilder ();

                selectStatement.Append ("EXEC dal.MemberRelationship_SelectByMember " + memberId.ToString ());


                System.Data.DataTable enrollmentCoverageTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

                foreach (System.Data.DataRow currentRow in enrollmentCoverageTable.Rows) {

                    Core.Member.MemberRelationship relationship = new Mercury.Server.Core.Member.MemberRelationship (this);

                    relationship.MapDataFields (currentRow);

                    memberRelationships.Add (relationship);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberRelationships;

        }


        #endregion

        #endregion


        #region Core.Metrics

        public List<Core.Metrics.Metric> MetricsAvailable () {

            List<Core.Metrics.Metric> results = new List<Server.Core.Metrics.Metric> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("SELECT * FROM Metric");

            selectStatement.Append ("  ORDER BY MetricName");


            System.Data.DataTable metricTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in metricTable.Rows) {

                Server.Core.Metrics.Metric metric = new Server.Core.Metrics.Metric (this);

                metric.MapDataFields (currentRow);

                results.Add (metric);

            }

            return results;

        }

        public Core.Metrics.Metric MetricGet (Int64 metricId) {

            if (metricId == 0) { return null; }

            Core.Metrics.Metric metric = null;

            SetLastException (null);

            try {

                metric = new Server.Core.Metrics.Metric (this, metricId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return metric;

        }

        public Core.Metrics.Metric MetricGet (String metricName) {

            Int64 metricId = MetricGetIdByName (metricName);

            Core.Metrics.Metric metric = null;

            if (metricId != 0) { metric = MetricGet (metricId); }

            return metric;

        }

        public Int64 MetricGetIdByName (String metricName) { return CoreObjectGetIdByName ("Metric", metricName); } // BACKWARDS COMPATIBILITY

        public String MetricGetNameById (Int64 metricId) { return CoreObjectGetNameById ("Metric", metricId); } // BACKWARDS COMPATIBILITY


        public Int64 MemberMetricGetId (Int64 forMemberId, Int64 forMetricId, DateTime forEventDate) {

            StringBuilder selectStatement = new StringBuilder ();

            selectStatement.Append ("EXEC MemberMetric_SelectByMemberEvent ");

            selectStatement.Append (forMemberId.ToString () + ", ");

            selectStatement.Append (forMetricId.ToString () + ", ");

            selectStatement.Append ("'" + forEventDate.ToString ("MM/dd/yyyy") + "'");

            System.Data.DataTable memberServiceTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            if (memberServiceTable.Rows.Count == 0) { return 0; }

            return (Int64)memberServiceTable.Rows[0][0];

        }

        public Core.Metrics.MemberMetric MemberMetricGetByMemberMostRecent (Int64 forMemberId, String metricName) {

            Core.Metrics.MemberMetric memberMetric = null;


            StringBuilder selectStatement = new StringBuilder ();

            selectStatement.Append ("EXEC MemberMetric_SelectByMemberMostRecent ");

            selectStatement.Append (forMemberId.ToString () + ", ");

            selectStatement.Append ("0, ");

            selectStatement.Append ("'" + metricName.Replace ("'", "''") + "'");

            System.Data.DataTable memberMetricTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            if (memberMetricTable.Rows.Count == 0) { return null; }

            memberMetric = new Mercury.Server.Core.Metrics.MemberMetric (this);

            memberMetric.MapDataFields (memberMetricTable.Rows[0]);

            return memberMetric;

        }

        public Boolean MemberMetricAddManual (Int64 memberId, Int64 metricId, DateTime eventDate, Decimal value, Boolean validatePermission) {

            Boolean success = false;

            String sqlStatement;


            SetLastException (null);

            try {

                if ((!HasEnvironmentPermission (EnvironmentPermissions.MemberMetricManage)) && validatePermission) {

                    throw new ApplicationException ("Permission Denied when adding Metric to Member manually.");

                }

                Core.Metrics.Metric metric = MetricGet (metricId);

                if (metric == null) { throw new ApplicationException ("Invalid Metric Specified."); }

                if ((value < metric.MinimumValue) || (value > metric.MaximumValue)) { throw new ApplicationException ("Invalid Value Specified (" + value.ToString () + "). Must be between " + metric.MinimumValue.ToString () + " and " + metric.MaximumValue.ToString () + "."); }

                Data.AuthorityAccountStamp modified = new Server.Data.AuthorityAccountStamp (session);

                sqlStatement = "MemberMetric_AddManual " + memberId.ToString () + ", " + metricId.ToString () + ", '" + eventDate.ToString ("MM/dd/yyyy") + "', " + value.ToString () + ", ";

                sqlStatement = sqlStatement + "'" + modified.SecurityAuthorityNameSql + "', '" + modified.UserAccountIdSql + "', '" + modified.UserAccountNameSql + "'";

                success = EnvironmentDatabase.ExecuteSqlStatement (sqlStatement, 0);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

                success = false;

            }

            return success;

        }

        public Boolean MemberMetricRemoveManual (Int64 memberMetricId) {

            Boolean success = false;

            String sqlStatement;


            SetLastException (null);

            try {

                if (!HasEnvironmentPermission (EnvironmentPermissions.MemberMetricManage)) {

                    throw new ApplicationException ("Permission Denied when trying to remove Metric from Member.");

                }

                Data.AuthorityAccountStamp modified = new Server.Data.AuthorityAccountStamp (session);

                sqlStatement = "MemberMetric_RemoveManual " + memberMetricId.ToString ();

                success = EnvironmentDatabase.ExecuteSqlStatement (sqlStatement, 0);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

                success = false;

            }

            return success;

        }


        public Int64 MemberMetricsGetCount (Int64 memberId, Boolean showHidden) {

            String sqlStatement;

            Int64 itemCount = 0;

            SetLastException (null);

            try {

                sqlStatement = "MemberMetric_Count " + memberId.ToString () + ", " + showHidden.ToString ();

                itemCount = Convert.ToInt64 (EnvironmentDatabase.StoredProcedureReturnValue (sqlStatement));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return itemCount;

        }

        public List<Core.Metrics.MemberMetric> MemberMetricsGetByPage (Int64 memberId, Int32 initialRow, Int32 count, Boolean showHidden) {

            List<Core.Metrics.MemberMetric> memberMetrics = new List<Server.Core.Metrics.MemberMetric> ();

            System.Data.DataTable metricTable;

            String sqlStatement;


            SetLastException (null);

            try {

                sqlStatement = "EXEC MemberMetric_SelectByMemberPage " + memberId.ToString () + ", " + initialRow.ToString () + ", " + count.ToString () + ", " + showHidden.ToString ();

                metricTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in metricTable.Rows) {

                    Core.Metrics.MemberMetric memberMetric = new Server.Core.Metrics.MemberMetric (this);

                    memberMetric.MapDataFields (currentRow);

                    memberMetrics.Add (memberMetric);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberMetrics;

        }

        #endregion


        #region Core.Provider

        #region Provider

        public Mercury.Server.Core.Provider.Provider ProviderGet (Int64 providerId) {

            if (providerId == 0) { return null; }

            Mercury.Server.Core.Provider.Provider provider = null;

            SetLastException (null);

            try {

                provider = new Mercury.Server.Core.Provider.Provider (this, providerId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return provider;

        }

        public Mercury.Server.Core.Provider.Provider ProviderGetByEntityId (Int64 entityId) {

            if (entityId == 0) { return null; }

            Mercury.Server.Core.Provider.Provider provider = null;

            SetLastException (null);

            try {

                System.Data.DataTable providerTable = EnvironmentDatabase.SelectDataTable ("EXEC dal.Provider_SelectByEntityId " + entityId.ToString ());

                if (providerTable.Rows.Count == 1) {

                    provider = new Mercury.Server.Core.Provider.Provider (this);

                    provider.MapDataFields (providerTable.Rows[0]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return provider;

        }

        #endregion


        #region Provider Enrollment

        public List<Core.Provider.ProviderEnrollment> ProviderEnrollmentsGet (Int64 providerId) {

            List<Core.Provider.ProviderEnrollment> enrollments = new List<Core.Provider.ProviderEnrollment> ();

            SetLastException (null);


            try {

                StringBuilder selectStatement = new StringBuilder ();

                selectStatement.Append ("EXEC dal.ProviderEnrollments_Select " + providerId.ToString ());


                System.Data.DataTable enrollmentTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

                foreach (System.Data.DataRow currentRow in enrollmentTable.Rows) {

                    Core.Provider.ProviderEnrollment enrollment = new Mercury.Server.Core.Provider.ProviderEnrollment (this);

                    enrollment.MapDataFields (currentRow);

                    enrollments.Add (enrollment);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return enrollments;

        }

        public Core.Provider.ProviderEnrollment ProviderEnrollmentGet (Int64 providerEnrollmentId) {

            if (providerEnrollmentId == 0) { return null; }

            Mercury.Server.Core.Provider.ProviderEnrollment providerEnrollment = null;

            SetLastException (null);

            try {

                providerEnrollment = new Mercury.Server.Core.Provider.ProviderEnrollment (this, providerEnrollmentId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return providerEnrollment;

        }

        #endregion


        public List<Core.Provider.ProviderAffiliation> ProviderAffiliationsGet (Int64 providerId) {

            List<Core.Provider.ProviderAffiliation> affiliations = new List<Core.Provider.ProviderAffiliation> ();

            SetLastException (null);


            try {

                StringBuilder selectStatement = new StringBuilder ();

                selectStatement.Append ("EXEC dal.ProviderAffiliations_Select " + providerId.ToString ());


                System.Data.DataTable affiliationTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

                foreach (System.Data.DataRow currentRow in affiliationTable.Rows) {

                    Core.Provider.ProviderAffiliation affiliation = new Mercury.Server.Core.Provider.ProviderAffiliation (this);

                    affiliation.MapDataFields (currentRow);

                    affiliations.Add (affiliation);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return affiliations;

        }

        public Core.Provider.ProviderAffiliation ProviderAffiliationGet (Int64 providerAffiliationId) {

            if (providerAffiliationId == 0) { return null; }

            Mercury.Server.Core.Provider.ProviderAffiliation providerAffiliation = null;

            SetLastException (null);

            try {

                providerAffiliation = new Mercury.Server.Core.Provider.ProviderAffiliation (this, providerAffiliationId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return providerAffiliation;

        }


        public List<Core.Provider.ProviderContract> ProviderContractsGet (Int64 providerId) {

            List<Core.Provider.ProviderContract> providerContracts = new List<Core.Provider.ProviderContract> ();

            SetLastException (null);


            try {

                StringBuilder selectStatement = new StringBuilder ();

                selectStatement.Append ("EXEC dal.ProviderContracts_Select " + providerId.ToString ());


                System.Data.DataTable providerContractTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

                foreach (System.Data.DataRow currentRow in providerContractTable.Rows) {

                    Core.Provider.ProviderContract providerContract = new Mercury.Server.Core.Provider.ProviderContract (this);

                    providerContract.MapDataFields (currentRow);

                    providerContracts.Add (providerContract);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return providerContracts;

        }

        public Core.Provider.ProviderContract ProviderContractGet (Int64 providerContractId) {

            if (providerContractId == 0) { return null; }

            Mercury.Server.Core.Provider.ProviderContract providerContract = null;

            SetLastException (null);

            try {

                providerContract = new Mercury.Server.Core.Provider.ProviderContract (this, providerContractId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return providerContract;

        }


        public List<Core.Provider.ProviderServiceLocation> ProviderServiceLocationsGet (Int64 providerId) {

            List<Core.Provider.ProviderServiceLocation> enrollments = new List<Core.Provider.ProviderServiceLocation> ();

            SetLastException (null);


            try {

                StringBuilder selectStatement = new StringBuilder ();

                selectStatement.Append ("EXEC dal.ProviderServiceLocations_Select " + providerId.ToString ());


                System.Data.DataTable enrollmentTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

                foreach (System.Data.DataRow currentRow in enrollmentTable.Rows) {

                    Core.Provider.ProviderServiceLocation enrollment = new Mercury.Server.Core.Provider.ProviderServiceLocation (this);

                    enrollment.MapDataFields (currentRow);

                    enrollments.Add (enrollment);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return enrollments;

        }

        #endregion


        #region Core.Population

        public List<Core.Search.SearchResultPopulationHeader> PopulationsAvailable () {

            List<Core.Search.SearchResultPopulationHeader> results = new List<Core.Search.SearchResultPopulationHeader> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("SELECT ");

            selectStatement.Append ("    Population.* ");

            selectStatement.Append ("  FROM dbo.Population AS Population");

            selectStatement.Append ("  ORDER BY PopulationName");


            System.Data.DataTable serviceTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in serviceTable.Rows) {

                Mercury.Server.Core.Search.SearchResultPopulationHeader populationHeader = new Mercury.Server.Core.Search.SearchResultPopulationHeader ();

                populationHeader.MapDataFields (currentRow);

                results.Add (populationHeader);

            }

            return results;

        }

        public Core.Population.Population PopulationGet (Int64 populationId) {

            Core.Population.Population population = null;

            SetLastException (null);

            try {

                if (populationId != 0) {

                    population = new Mercury.Server.Core.Population.Population (this, populationId);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return population;

        }

        public Core.Population.Population PopulationGet (String populationName) {

            Core.Population.Population population = null;

            Int64 populationId = 0;

            SetLastException (null);

            try {

                populationId = Convert.ToInt64 (EnvironmentDatabase.LookupValue ("Population", "PopulationId", "PopulationName = '" + populationName + "'", 0));

                population = PopulationGet (populationId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return population;

        }

        public Core.Population.PopulationEvents.PopulationServiceEvent PopulationServiceEventGet (Int64 populationServiceEventId) {

            Core.Population.PopulationEvents.PopulationServiceEvent populationServiceEvent = null;

            SetLastException (null);

            try {

                if (populationServiceEventId != 0) {

                    populationServiceEvent = new Mercury.Server.Core.Population.PopulationEvents.PopulationServiceEvent (this, populationServiceEventId);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return populationServiceEvent;

        }

        public List<Core.Population.PopulationEvents.PopulationMembershipServiceEvent> PopulationMembershipServiceEventsGet (Int64 populationMembershipId) {

            List<Core.Population.PopulationEvents.PopulationMembershipServiceEvent> results = new List<Core.Population.PopulationEvents.PopulationMembershipServiceEvent> ();

            String sqlStatement;

            System.Data.DataTable eventTable;


            SetLastException (null);

            try {

                sqlStatement = "EXEC PopulationMembershipServiceEventsByMembership_Select " + populationMembershipId.ToString ();

                eventTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in eventTable.Rows) {

                    Core.Population.PopulationEvents.PopulationMembershipServiceEvent serviceEvent = new Mercury.Server.Core.Population.PopulationEvents.PopulationMembershipServiceEvent (this);

                    serviceEvent.MapDataFields (currentRow);

                    results.Add (serviceEvent);


                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return results;

        }

        public List<Core.Population.PopulationEvents.PopulationMembershipTriggerEvent> PopulationMembershipTriggerEventsGet (Int64 populationMembershipId) {

            List<Core.Population.PopulationEvents.PopulationMembershipTriggerEvent> results = new List<Core.Population.PopulationEvents.PopulationMembershipTriggerEvent> ();

            String sqlStatement;

            System.Data.DataTable eventTable;


            SetLastException (null);

            try {

                sqlStatement = "EXEC PopulationMembershipTriggerEventsByMembership_Select " + populationMembershipId.ToString ();

                eventTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in eventTable.Rows) {

                    Core.Population.PopulationEvents.PopulationMembershipTriggerEvent triggerEvent = new Mercury.Server.Core.Population.PopulationEvents.PopulationMembershipTriggerEvent (this);

                    triggerEvent.MapDataFields (currentRow);

                    results.Add (triggerEvent);


                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return results;

        }


        public List<Core.Population.PopulationType> PopulationTypesAvailable () {

            List<Core.Population.PopulationType> results = new List<Core.Population.PopulationType> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("SELECT ");

            selectStatement.Append ("    PopulationType.* ");

            selectStatement.Append ("  FROM dbo.PopulationType AS PopulationType");

            selectStatement.Append ("  ORDER BY PopulationTypeName");


            System.Data.DataTable resultTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in resultTable.Rows) {

                Core.Population.PopulationType populationType = new Mercury.Server.Core.Population.PopulationType (this);

                populationType.MapDataFields (currentRow);

                results.Add (populationType);

            }

            return results;

        }

        public Core.Population.PopulationType PopulationTypeGet (Int64 populationTypeId) {

            if (populationTypeId == 0) { return null; }

            Core.Population.PopulationType populationType = null;

            SetLastException (null);

            try {

                if (populationTypeId != 0) {

                    populationType = new Mercury.Server.Core.Population.PopulationType (this, populationTypeId);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return populationType;

        }

        public Core.Population.PopulationType PopulationTypeGet (String populationTypeName) {

            if (String.IsNullOrEmpty (populationTypeName)) { return null; }

            Core.Population.PopulationType populationType = null;

            Int64 populationTypeId = 0;

            SetLastException (null);

            try {

                populationTypeId = Convert.ToInt64 (EnvironmentDatabase.LookupValue ("PopulationType", "PopulationTypeId", "PopulationTypeName = '" + populationTypeName + "'", 0));

                populationType = PopulationTypeGet (populationTypeId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return populationType;

        }

        public String PopulationTypeGetNameById (Int64 populationTypeId) {

            if (populationTypeId == 0) { return String.Empty; }

            String name = String.Empty;

            name = Convert.ToString (EnvironmentDatabase.LookupValue ("PopulationType", "PopulationTypeName", "PopulationTypeId = " + populationTypeId.ToString (), String.Empty));

            return name;

        }

        public Int64 PopulationTypeGetIdByName (String name) {

            if (String.IsNullOrEmpty (name)) { return 0; }

            Int64 id = 0;

            id = Convert.ToInt64 (EnvironmentDatabase.LookupValue ("PopulationType", "PopulationTypeId", "PopulationTypeName = '" + name.Replace ("'", "''") + "'", 0));

            return id;

        }


        public Core.Population.PopulationMembership PopulationMembershipGet (Int64 populationMembershipId) {

            if (populationMembershipId == 0) { return null; }

            Core.Population.PopulationMembership populationMembership = null;

            SetLastException (null);

            try {

                if (populationMembershipId != 0) {

                    populationMembership = new Mercury.Server.Core.Population.PopulationMembership (this, populationMembershipId);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return populationMembership;

        }

        public Core.Population.PopulationEvents.PopulationMembershipServiceEvent PopulationMembershipServiceEventGet (Int64 populationMembershipServiceEventId) {

            if (populationMembershipServiceEventId == 0) { return null; }

            Core.Population.PopulationEvents.PopulationMembershipServiceEvent populationMembershipServiceEvent = null;

            SetLastException (null);

            try {

                if (populationMembershipServiceEventId != 0) {

                    populationMembershipServiceEvent = new Mercury.Server.Core.Population.PopulationEvents.PopulationMembershipServiceEvent (this, populationMembershipServiceEventId);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return populationMembershipServiceEvent;

        }

        public Core.Population.PopulationEvents.PopulationMembershipTriggerEvent PopulationMembershipTriggerEventGet (Int64 populationMembershipTriggerEventId) {

            if (populationMembershipTriggerEventId == 0) { return null; }

            Core.Population.PopulationEvents.PopulationMembershipTriggerEvent populationMembershipTriggerEvent = null;

            SetLastException (null);

            try {

                if (populationMembershipTriggerEventId != 0) {

                    populationMembershipTriggerEvent = new Mercury.Server.Core.Population.PopulationEvents.PopulationMembershipTriggerEvent (this, populationMembershipTriggerEventId);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return populationMembershipTriggerEvent;

        }


        public List<Core.Population.PopulationMembership> PopulationMembershipGetByMember (Int64 memberId) {

            List<Core.Population.PopulationMembership> results = new List<Core.Population.PopulationMembership> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("SELECT ");

            selectStatement.Append ("    PopulationMembership.*, Population.PopulationName ");

            selectStatement.Append ("  FROM dbo.PopulationMembership AS PopulationMembership");

            selectStatement.Append ("    JOIN dbo.Population AS Population ON PopulationMembership.PopulationId = Population.PopulationId");

            selectStatement.Append ("  WHERE PopulationMembership.MemberId = " + memberId.ToString ());

            selectStatement.Append ("  ORDER BY TerminationDate, EffectiveDate, PopulationName");


            System.Data.DataTable membershipTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in membershipTable.Rows) {

                Mercury.Server.Core.Population.PopulationMembership membership = new Mercury.Server.Core.Population.PopulationMembership (this);

                membership.MapDataFields (currentRow);

                results.Add (membership);

            }

            return results;

        }

        public List<Core.Population.DataViews.PopulationMembershipSummary> PopulationMembershipSummaryByMember (Int64 memberId) {

            List<Core.Population.DataViews.PopulationMembershipSummary> results = new List<Mercury.Server.Core.Population.DataViews.PopulationMembershipSummary> ();

            String sqlStatement;

            System.Data.DataTable summaryTable;

            List<Int64> populationIds = new List<Int64> ();


            SetLastException (null);

            try {

                sqlStatement = "EXEC PopulationMembershipSummaryByMember_Select " + memberId.ToString ();

                summaryTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in summaryTable.Rows) {

                    if (!populationIds.Contains ((Int64)currentRow["PopulationId"])) {

                        populationIds.Add ((Int64)currentRow["PopulationId"]);

                        Core.Population.DataViews.PopulationMembershipSummary summary = new Mercury.Server.Core.Population.DataViews.PopulationMembershipSummary ();

                        summary.MapDataFields (currentRow);

                        results.Add (summary);

                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return results;

        }

        public List<Core.Population.DataViews.PopulationMembershipServiceEvent> PopulationMembershipServiceEventsByMembershipDataView (Int64 membershipId) {

            List<Core.Population.DataViews.PopulationMembershipServiceEvent> results = new List<Mercury.Server.Core.Population.DataViews.PopulationMembershipServiceEvent> ();

            String sqlStatement;

            System.Data.DataTable eventTable;


            SetLastException (null);

            try {

                sqlStatement = "EXEC PopulationMembershipServiceEventsByMembership_Select " + membershipId.ToString ();

                eventTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in eventTable.Rows) {

                    Core.Population.DataViews.PopulationMembershipServiceEvent serviceEvent = new Mercury.Server.Core.Population.DataViews.PopulationMembershipServiceEvent ();

                    serviceEvent.MapDataFields (currentRow);

                    results.Add (serviceEvent);


                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return results;

        }

        public List<Core.Population.DataViews.PopulationMembershipTriggerEvent> PopulationMembershipTriggerEventsByMembershipDataView (Int64 membershipId) {

            List<Core.Population.DataViews.PopulationMembershipTriggerEvent> results = new List<Mercury.Server.Core.Population.DataViews.PopulationMembershipTriggerEvent> ();

            String sqlStatement;

            System.Data.DataTable eventTable;


            SetLastException (null);

            try {

                sqlStatement = "EXEC PopulationMembershipTriggerEventsByMembership_Select " + membershipId.ToString ();

                eventTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in eventTable.Rows) {

                    Core.Population.DataViews.PopulationMembershipTriggerEvent serviceEvent = new Mercury.Server.Core.Population.DataViews.PopulationMembershipTriggerEvent ();

                    serviceEvent.MapDataFields (currentRow);

                    results.Add (serviceEvent);


                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return results;

        }

        public Int64 PopulationMembershipGetCountByNamePrefix (Int64 populationId, String namePrefix) {

            String sqlStatement;

            Int64 itemCount = 0;

            SetLastException (null);

            try {

                sqlStatement = "EXEC PopulationMembership_SelectCountByName " + populationId.ToString () + ", '" + namePrefix + "'";

                itemCount = Convert.ToInt64 (EnvironmentDatabase.ExecuteScalar (sqlStatement));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return itemCount;

        }

        public List<Core.Population.DataViews.PopulationMembershipEntryStatus> PopulationMembershipGetByMembershipPage (Int64 populationId, String namePrefix, Int64 initialRow, Int32 count) {

            List<Core.Population.DataViews.PopulationMembershipEntryStatus> membership = new List<Mercury.Server.Core.Population.DataViews.PopulationMembershipEntryStatus> ();

            System.Data.DataTable membershipTable;

            String sqlStatement;


            SetLastException (null);

            try {

                sqlStatement = "EXEC PopulationMembership_SelectByMembershipPage " + populationId.ToString () + ", '" + namePrefix + "', " + initialRow.ToString () + ", " + count.ToString ();

                membershipTable = EnvironmentDatabase.SelectDataTable (sqlStatement, 0);

                foreach (System.Data.DataRow currentRow in membershipTable.Rows) {

                    Core.Population.DataViews.PopulationMembershipEntryStatus membershipEntry = new Mercury.Server.Core.Population.DataViews.PopulationMembershipEntryStatus ();

                    membershipEntry.MapDataFields (currentRow);

                    membership.Add (membershipEntry);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return membership;

        }

        #endregion


        #region Core.Sponsor

        public Mercury.Server.Core.Sponsor.Sponsor SponsorGet (Int64 sponsorId) {

            if (sponsorId == 0) { return null; }

            Mercury.Server.Core.Sponsor.Sponsor sponsor = null;

            SetLastException (null);

            try {

                sponsor = new Mercury.Server.Core.Sponsor.Sponsor (this, sponsorId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return sponsor;

        }

        #endregion


        #region Core.Work

        #region Work - Routing Rules

        public List<Core.Work.RoutingRule> RoutingRulesAvailable () {

            List<Core.Work.RoutingRule> results = new List<Mercury.Server.Core.Work.RoutingRule> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("SELECT ");

            selectStatement.Append ("    RoutingRule.* ");

            selectStatement.Append ("  FROM dbo.RoutingRule AS RoutingRule ");

            selectStatement.Append ("  ORDER BY RoutingRuleName");


            System.Data.DataTable routingRuleTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in routingRuleTable.Rows) {

                Mercury.Server.Core.Work.RoutingRule routingRule = new Mercury.Server.Core.Work.RoutingRule (this);

                routingRule.MapDataFields (currentRow);

                results.Add (routingRule);

            }

            return results;

        }


        public Core.Work.RoutingRule RoutingRuleGet (Int64 routingRuleId) {

            if (routingRuleId == 0) { return null; }

            Core.Work.RoutingRule routingRule = null;

            SetLastException (null);

            try {

                routingRule = new Mercury.Server.Core.Work.RoutingRule (this, routingRuleId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return routingRule;

        }

        public Core.Work.RoutingRule RoutingRuleGet (String routingRuleName) {

            if (String.IsNullOrEmpty (routingRuleName)) { return null; }

            Int64 routingRuleId = RoutingRuleGetIdByName (routingRuleName);

            Core.Work.RoutingRule routingRule = null;

            if (routingRuleId != 0) { routingRule = RoutingRuleGet (routingRuleId); }

            return routingRule;

        }

        public Int64 RoutingRuleGetIdByName (String routingRuleName) { return CoreObjectGetIdByName ("RoutingRule", routingRuleName); }

        public String RoutingRuleGetNameById (Int64 routingRuleId) { return CoreObjectGetNameById ("RoutingRule", routingRuleId); }
        
        #endregion


        #region Work - Workflow

        public List<Core.Work.Workflow> WorkflowsAvailable (Boolean useCaching = false) {

            String cacheKey = "Application." + session.EnvironmentId + ".Workflow.Available";

            Dictionary<Int64, String> objectDictionary = new Dictionary<Int64, String> ();

            List<Core.Work.Workflow> availableItems = new List<Core.Work.Workflow> ();

            ClearLastException ();


            try {

                if (!useCaching) { CacheManager.RemoveObject (cacheKey); }

                availableItems = (List<Core.Work.Workflow>)CacheManager.GetObject (cacheKey);

                if (availableItems == null) {

                    availableItems = new List<Core.Work.Workflow> ();

                    String selectStatement = "SELECT * FROM dbo.Workflow WHERE WorkflowId <> 0 ORDER BY WorkflowName";

                    System.Data.DataTable availableTable = EnvironmentDatabase.SelectDataTable (selectStatement);

                    foreach (System.Data.DataRow currentRow in availableTable.Rows) {

                        Core.Work.Workflow item = new Core.Work.Workflow (this);

                        item.MapDataFields (currentRow);

                        availableItems.Add (item);


                        // CACHE EACH INDIVIDUAL ITEM LOADED FROM SERVER (BY ID AND NAME)

                        CacheManager.CacheObject ("Application." + session.EnvironmentId + "." + item.ObjectType + "." + item.Id.ToString (), item, CacheExpirationData);

                        CacheManager.CacheObject ("Application." + session.EnvironmentId + "." + item.ObjectType + "." + item.Name, item, CacheExpirationData);

                        objectDictionary.Add (item.Id, item.Name);

                    }

                    if (availableItems.Count > 0) {

                        // CACHE THE AVAILABILITY LIST

                        CacheManager.CacheObject (cacheKey, availableItems, CacheExpirationData);


                        // CACHE THE DICTIONARY THAT WAS CREATED

                        CacheManager.CacheObject ("Application." + session.EnvironmentId + "." + availableItems[0].ObjectType + ".Dictionary", objectDictionary, CacheExpirationData);

                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return availableItems;

        }


        public Core.Work.Workflow WorkflowGet (Int64 workflowId, Boolean useCaching = false) {

            if (workflowId == 0) { return null; }

            
            String cacheKey = "Application." + session.EnvironmentId + ".Workflow." + workflowId.ToString ();

            Core.Work.Workflow workflow = null;

            ClearLastException ();


            try {

                workflow = (Core.Work.Workflow)CacheManager.GetObject (cacheKey);

                if (!useCaching) { workflow = null; }

                if (workflow == null) {

                    workflow = new Mercury.Server.Core.Work.Workflow (this, workflowId);

                    CacheManager.CacheObject (cacheKey, workflow, CacheExpirationData); 

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workflow;

        }

        public Core.Work.Workflow WorkflowGet (String workflowName) {

            if (String.IsNullOrWhiteSpace (workflowName)) { return null; }


            Int64 workflowId = CoreObjectGetIdByName ("Workflow", workflowName);

            Core.Work.Workflow workflow = WorkflowGet (workflowId);

            return workflow;

        }

        public Core.Work.Workflow WorkflowGetByWorkQueueId (Int64 workQueueId) {

            Core.Work.Workflow workflow = null;

            ClearLastException ();

            try {

                if (workQueueId != 0) {

                    Int64 workflowId = Convert.ToInt64 (EnvironmentDatabase.LookupValue ("WorkQueue", "WorkflowId", "WorkQueueId = " + workQueueId.ToString (), 0));

                    workflow = WorkflowGet (workflowId);
                    
                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workflow;

        }


        public Mercury.Server.Workflows.WorkflowResponse WorkflowStart35 (Workflows.WorkflowStartRequest request, Core.Work.WorkQueueItem workQueueItem, Core.Work.Workflow workflow) {

            Mercury.Server.Workflows.WorkflowResponse response = new Mercury.Server.Workflows.WorkflowResponse ();

            System.Threading.AutoResetEvent waitHandle = new System.Threading.AutoResetEvent (false);


            Workflows.WorkflowManager workflowManager = new Mercury.Server.Workflows.WorkflowManager (this, EnvironmentDatabase.Configuration.ConnectionString, waitHandle);

            try {
                
                workflowManager.WorkflowStart (workflow.AssemblyUrl, workflow.AssemblyClassName, request);

#if DEBUG

                DateTime startTime = DateTime.Now;

#endif

                waitHandle.WaitOne ();

#if DEBUG

                System.Diagnostics.Debug.WriteLine ("!---> Workflow [ RUN ] [" + DateTime.Now.Subtract (startTime).TotalMilliseconds.ToString () + "]");

                startTime = DateTime.Now;

#endif

                if (!workflowManager.WorkflowUnload ()) {

                    workflowManager.WorkflowResponse.SetException (new ApplicationException ("Unable to persist Workflow to database. Operation aborted."));

                }

                // RELOAD WORK QUEUE ITEM, IN CASE OF IN WORKFLOW CHANGES

                workQueueItem = WorkQueueItemGet (request.WorkQueueItemId);

                if (workQueueItem != null) {

                    if (workflowManager.WorkflowResponse.WorkflowStatus == Mercury.Server.Workflows.WorkflowStatus.Completed) {

                        workQueueItem.CompletionDate = DateTime.Now;

                    }

                    workQueueItem.WorkflowInstanceId = workflowManager.WorkflowResponse.WorkflowInstanceId;

                    workQueueItem.WorkflowStatus = workflowManager.WorkflowResponse.WorkflowStatus.ToString ();

                    workQueueItem.LastWorkedDate = DateTime.Now;

                    workQueueItem.Save ();

                }

            }

            catch (Exception workflowException) {

                SetLastException (workflowException);

                workflowManager.WorkflowResponse.SetException (workflowException);

            }

            finally {

                if (workflowManager != null) {

                    response = workflowManager.WorkflowResponse;

                }

                workflowManager.WorkflowStopManager ();

            }

            if (workQueueItem != null) { response.WorkQueueItemId = workQueueItem.Id; }

            return response;

        }

        public Mercury.Server.Workflows.WorkflowResponse WorkflowStart4 (Workflows.WorkflowStartRequest request, Core.Work.WorkQueueItem workQueueItem, Core.Work.Workflow workflow) {

            Mercury.Server.Workflows.WorkflowResponse response = new Workflows.WorkflowResponse ();

            System.Threading.AutoResetEvent waitHandle = new System.Threading.AutoResetEvent (false);


            Workflows.WorkflowManager4 workflowManager = new Workflows.WorkflowManager4 (this, EnvironmentDatabase.Configuration.ConnectionString, waitHandle);

            try {

                workflowManager.WorkflowStart (workflow.AssemblyUrl, workflow.AssemblyClassName, request);

#if DEBUG

                DateTime startTime = DateTime.Now;

#endif

                waitHandle.WaitOne ();

#if DEBUG

                System.Diagnostics.Debug.WriteLine ("!---> Workflow [ RUN ] [" + DateTime.Now.Subtract (startTime).TotalMilliseconds.ToString () + "]");

                startTime = DateTime.Now;

#endif
                // VALIDATE THAT THE WORKFLOW IS UNLOADED (OR CAN BE UNLOADED)

                //if (!workflowManager.WorkflowUnload ()) {

                //    workflowManager.WorkflowResponse.SetException (new ApplicationException ("Unable to persist Workflow to database. Operation aborted."));

                //}

                // RELOAD WORK QUEUE ITEM, IN CASE OF IN WORKFLOW CHANGES

                workQueueItem = WorkQueueItemGet (request.WorkQueueItemId);

                if (workQueueItem != null) {

                    if (workflowManager.WorkflowResponse.WorkflowStatus == Mercury.Server.Workflows.WorkflowStatus.Completed) {

                        workQueueItem.CompletionDate = DateTime.Now;

                    }

                    workQueueItem.WorkflowInstanceId = workflowManager.WorkflowResponse.WorkflowInstanceId;

                    workQueueItem.WorkflowStatus = workflowManager.WorkflowResponse.WorkflowStatus.ToString ();

                    workQueueItem.LastWorkedDate = DateTime.Now;

                    workQueueItem.Save ();

                }

            }

            catch (Exception workflowException) {

                SetLastException (workflowException);

                workflowManager.WorkflowResponse.SetException (workflowException);

            }

            finally {

                if (workflowManager != null) { response = workflowManager.WorkflowResponse; }

            }

            if (workQueueItem != null) { response.WorkQueueItemId = workQueueItem.Id; }


            return response;

        }

        public Mercury.Server.Workflows.WorkflowResponse WorkflowStart (Workflows.WorkflowStartRequest request) {

            Mercury.Server.Workflows.WorkflowResponse response = new Workflows.WorkflowResponse ();

            ClearLastException ();


            Core.Work.WorkQueueItem workQueueItem = WorkQueueItemGet (request.WorkQueueItemId);

            Core.Work.Workflow workflow = WorkflowGet (request.WorkflowId);

            if (workflow == null) { WorkflowGet (request.WorkflowName); }


            if (workflow != null) {

                // UPDATE LAST WORKED DATE OF WORK QUEUE ITEM

                if (workQueueItem != null) {

                    workQueueItem.LastWorkedDate = DateTime.Now;

                    workQueueItem.Save ();

                }


                if (request.Arguments == null) { request.Arguments = new Dictionary<String, Object> (); }

                request.Arguments.Add ("Application", this);


                switch (workflow.Framework) {

                    case Core.Work.Enumerations.WorkflowFramework.DotNet35:

                        response = WorkflowStart35 (request, workQueueItem, workflow);

                        break;

                    case Core.Work.Enumerations.WorkflowFramework.DotNet40:

                        response = WorkflowStart4 (request, workQueueItem, workflow);

                        break;

                }

            }

            else {

                response.SetException (new ApplicationException ("Unable to retreive valid Workflow to process."));

            }


            return response;

        }

        public Mercury.Server.Workflows.WorkflowResponse WorkflowContinue35 (String workflowName, Guid workflowInstanceId, Server.Workflows.UserInteractions.Response.ResponseBase userInteractionResponse) {

            Mercury.Server.Workflows.WorkflowResponse workflowResponse = new Mercury.Server.Workflows.WorkflowResponse ();

            Core.Work.WorkQueueItem workQueueItem = WorkQueueItemGet (workflowInstanceId);

            Int64 workQueueItemId = (workQueueItem != null) ? workQueueItem.Id : 0;


            SetLastException (null);


            System.Threading.AutoResetEvent waitHandle = new System.Threading.AutoResetEvent (false);

            Workflows.WorkflowManager workflowManager = new Mercury.Server.Workflows.WorkflowManager (this, EnvironmentDatabase.Configuration.ConnectionString, waitHandle);

            try {

                Core.Work.Workflow workflow = WorkflowGet (workflowName);

                String assemblyUrl = workflow.AssemblyPath + "\\" + workflow.AssemblyName;

                assemblyUrl = assemblyUrl.Replace ("\\\\", "\\");


                if (workQueueItem != null) {

                    if (workQueueItem.WorkflowStatus == "Suspended") {

                        workflowManager.WorkflowResume (this, workflow.Id, assemblyUrl, workflow.AssemblyClassName, workflowInstanceId);

                    }

                    else { workflowManager.WorkflowContinue (this, workflow.Id, assemblyUrl, workflow.AssemblyClassName, workflowInstanceId, userInteractionResponse); }

                }

                else { workflowManager.WorkflowContinue (this, workflow.Id, assemblyUrl, workflow.AssemblyClassName, workflowInstanceId, userInteractionResponse); }

#if DEBUG

                DateTime startTime = DateTime.Now;

#endif


                waitHandle.WaitOne ();

                
#if DEBUG

                System.Diagnostics.Debug.WriteLine ("!---> Workflow [ RUN ] [" + DateTime.Now.Subtract (startTime).TotalMilliseconds.ToString () + "]");

                startTime = DateTime.Now;


                System.Diagnostics.Trace.WriteLineIf (traceSwitchWorkflow.TraceVerbose, "[Mercury.Application] WorkflowContinue.Workflow: " + DateTime.Now.Subtract (startTime).Milliseconds.ToString ());

#endif

                if (!workflowManager.WorkflowUnload ()) {

                    workflowManager.WorkflowResponse.SetException (new ApplicationException ("Unable to persist Workflow to database. Operation aborted."));

                }



                // RELOAD WORK QUEUE ITEM, IN CASE OF IN WORKFLOW CHANGES

                workQueueItem = WorkQueueItemGet (workQueueItemId);

                if (workQueueItem != null) {

                    if (workflowManager.WorkflowResponse.WorkflowStatus == Mercury.Server.Workflows.WorkflowStatus.Completed) {

                        workQueueItem.CompletionDate = DateTime.Now;

                    }

                    workQueueItem.WorkflowInstanceId = workflowManager.WorkflowResponse.WorkflowInstanceId;

                    workQueueItem.WorkflowStatus = workflowManager.WorkflowResponse.WorkflowStatus.ToString ();

                    workQueueItem.LastWorkedDate = DateTime.Now;

                    workQueueItem.Save ();

                }
                
            }

            catch (System.IndexOutOfRangeException workflowVersionException) {

                SetLastException (workflowVersionException);

                workflowManager.WorkflowResponse.SetException (workflowVersionException);

                workQueueItem = WorkQueueItemGet (workQueueItemId);

                if (workQueueItem != null) {

                    workQueueItem.WorkflowInstanceId = Guid.Empty;

                    workQueueItem.Save ();

                    ApplicationException resetException = new ApplicationException ("Version change detected in saved Workflow. Work Queue Item has been Reset for Processing.", workflowVersionException);

                    workflowManager.WorkflowResponse.SetException (resetException);

                }

            }

            catch (Exception workflowException) {

                SetLastException (workflowException);

                workflowManager.WorkflowResponse.SetException (workflowException);

                //if (((workflowException.Source == "System.Workflow.Runtime") && (workflowException.Message.Contains ("not found in state persistence store.")))

                //    || ((workflowException.Source == "mscorlib") && (workflowException.Message.Contains ("Error binding to target method.")))) {

                workQueueItem = WorkQueueItemGet (workQueueItemId);

                if (workQueueItem != null) {

                    workQueueItem.WorkflowInstanceId = Guid.Empty;

                    workQueueItem.Save ();

                    ApplicationException resetException = new ApplicationException (workflowException.Message + " Work Queue Item has been Reset for Processing. This error may occur if a newer version of the Workflow has been deployed but there are still open Work Queue Items referencing the older version or an exception occurred during the previous execution.", workflowException);

                    workflowManager.WorkflowResponse.SetException (resetException);

                }

                //}

            }

            finally {

                if (workflowManager != null) {

                    workflowResponse = workflowManager.WorkflowResponse;

                    workflowManager.WorkflowStopManager ();

                }

            }


            if (workQueueItem != null) { workflowResponse.WorkQueueItemId = workQueueItem.Id; }
            
            return workflowResponse;

        }

        public Mercury.Server.Workflows.WorkflowResponse WorkflowContinue4 (Guid workflowInstanceId, Int64 workQueueItemId, Core.Work.Workflow workflow, Server.Workflows.UserInteractions.Response.ResponseBase userInteractionResponse) {

            Mercury.Server.Workflows.WorkflowResponse response = new Mercury.Server.Workflows.WorkflowResponse ();

            Core.Work.WorkQueueItem workQueueItem = null;

            System.Threading.AutoResetEvent waitHandle = new System.Threading.AutoResetEvent (false);
            
            Workflows.WorkflowManager4 workflowManager = new Workflows.WorkflowManager4 (this, EnvironmentDatabase.Configuration.ConnectionString, waitHandle);


            try {

                workflowManager.WorkflowContinue (this, workQueueItemId, workflow.AssemblyUrl, workflow.AssemblyClassName, workflowInstanceId, userInteractionResponse);

#if DEBUG

                DateTime startTime = DateTime.Now;

#endif


                waitHandle.WaitOne ();


#if DEBUG

                System.Diagnostics.Debug.WriteLine ("!---> Workflow [ RUN ] [" + DateTime.Now.Subtract (startTime).TotalMilliseconds.ToString () + "]");

#endif


                // RELOAD WORK QUEUE ITEM, IN CASE OF IN WORKFLOW CHANGES

                workQueueItem = WorkQueueItemGet (workQueueItemId);

                if (workQueueItem != null) {

                    if (workflowManager.WorkflowResponse.WorkflowStatus == Mercury.Server.Workflows.WorkflowStatus.Completed) {

                        workQueueItem.CompletionDate = DateTime.Now;

                    }

                    workQueueItem.WorkflowInstanceId = workflowManager.WorkflowResponse.WorkflowInstanceId;

                    workQueueItem.WorkflowStatus = workflowManager.WorkflowResponse.WorkflowStatus.ToString ();

                    workQueueItem.LastWorkedDate = DateTime.Now;

                    workQueueItem.Save ();

                }

            }

            catch (System.IndexOutOfRangeException workflowVersionException) {

                SetLastException (workflowVersionException);

                workflowManager.WorkflowResponse.SetException (workflowVersionException);

                workQueueItem = WorkQueueItemGet (workQueueItemId);

                if (workQueueItem != null) {

                    workQueueItem.WorkflowInstanceId = Guid.Empty;

                    workQueueItem.Save ();

                    ApplicationException resetException = new ApplicationException ("Version change detected in saved Workflow. Work Queue Item has been Reset for Processing.", workflowVersionException);

                    workflowManager.WorkflowResponse.SetException (resetException);

                }

            }

            catch (Exception workflowException) {

                SetLastException (workflowException);

                workflowManager.WorkflowResponse.SetException (workflowException);

                //if (((workflowException.Source == "System.Workflow.Runtime") && (workflowException.Message.Contains ("not found in state persistence store.")))

                //    || ((workflowException.Source == "mscorlib") && (workflowException.Message.Contains ("Error binding to target method.")))) {

                workQueueItem = WorkQueueItemGet (workQueueItemId);

                if (workQueueItem != null) {

                    workQueueItem.WorkflowInstanceId = Guid.Empty;

                    workQueueItem.Save ();

                    ApplicationException resetException = new ApplicationException (workflowException.Message + " Work Queue Item has been Reset for Processing. This error may occur if a newer version of the Workflow has been deployed but there are still open Work Queue Items referencing the older version or an exception occurred during the previous execution.", workflowException);

                    workflowManager.WorkflowResponse.SetException (resetException);

                }

            }

            finally {

                if (workflowManager != null) {

                    response = workflowManager.WorkflowResponse;

                }

            }


            if (workQueueItem != null) { response.WorkQueueItemId = workQueueItem.Id; }
            
            return response;

        }

        public Mercury.Server.Workflows.WorkflowResponse WorkflowContinue (String workflowName, Guid workflowInstanceId, Server.Workflows.UserInteractions.Response.ResponseBase userInteractionResponse) {

            Mercury.Server.Workflows.WorkflowResponse response = new Mercury.Server.Workflows.WorkflowResponse ();

            ClearLastException ();


            Core.Work.WorkQueueItem workQueueItem = WorkQueueItemGet (workflowInstanceId);

            Int64 workQueueItemId = (workQueueItem != null) ? workQueueItem.Id : 0;

            Core.Work.Workflow workflow = WorkflowGet (workflowName);
            

            if (workflow != null) {

                switch (workflow.Framework) {

                    case Core.Work.Enumerations.WorkflowFramework.DotNet35:

                        response = WorkflowContinue35 (workflowName, workflowInstanceId, userInteractionResponse);

                        break;

                    case Core.Work.Enumerations.WorkflowFramework.DotNet40:

                        response = WorkflowContinue4 (workflowInstanceId, workQueueItemId, workflow, userInteractionResponse);

                        break;

                }

            }

            else {

                response.SetException (new ApplicationException ("Unable to retreive valid Workflow to process."));

            }

            return response;

        }

        #endregion


        #region Work - Work Outcome

        public List<Core.Work.WorkOutcome> WorkOutcomesAvailable () {

            List<Core.Work.WorkOutcome> results = new List<Core.Work.WorkOutcome> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("SELECT * FROM dbo.WorkOutcome ORDER BY WorkOutcomeName");


            System.Data.DataTable workOutcomeTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in workOutcomeTable.Rows) {

                Core.Work.WorkOutcome workOutcomeHeader = new Mercury.Server.Core.Work.WorkOutcome (this);

                workOutcomeHeader.MapDataFields (currentRow);

                results.Add (workOutcomeHeader);

            }

            return results;

        }


        public Core.Work.WorkOutcome WorkOutcomeGet (Int64 forWorkOutcomeId) {

            if (forWorkOutcomeId == 0) { return null; }

            Core.Work.WorkOutcome workOutcome = null;

            SetLastException (null);

            try {

                workOutcome = new Mercury.Server.Core.Work.WorkOutcome (this, forWorkOutcomeId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workOutcome;

        }

        public Core.Work.WorkOutcome WorkOutcomeGet (String workOutcomeName) {

            Int64 workOutcomeId = WorkOutcomeGetIdByName (workOutcomeName);

            Core.Work.WorkOutcome workOutcome = null;

            if (workOutcomeId != 0) { workOutcome = WorkOutcomeGet (workOutcomeId); }

            return workOutcome;

        }

        public Int64 WorkOutcomeGetIdByName (String workOutcomeName) {

            Int64 workOutcomeId = 0;

            SetLastException (null);

            try {

                workOutcomeId = Convert.ToInt64 (EnvironmentDatabase.LookupValue ("WorkOutcome", "WorkOutcomeId", "WorkOutcomeName = '" + workOutcomeName + "'"));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workOutcomeId;

        }

        /// <summary>
        /// Get the Work Outcome Name by a given Work Outcome Id.
        /// </summary>
        /// <param name="workOutcomeId">Unique Identifier for the Work Outcome.</param>
        /// <returns>Work Outcome Name</returns>
        public String WorkOutcomeGetNameById (Int64 workOutcomeId) {

            String workOutcomeName = String.Empty;

            SetLastException (null);

            try {

                workOutcomeName = (String) EnvironmentDatabase.LookupValue ("WorkOutcome", "WorkOutcomeName", "WorkOutcomeId = " + workOutcomeId.ToString (), String.Empty);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workOutcomeName;

        }

        #endregion


        #region Work - Work Queue

        public List<Core.Work.WorkQueue> WorkQueuesAvailable (Boolean useCaching = false) {

            String cacheKey = "Application." + session.EnvironmentId + ".WorkQueue.Available";

            Dictionary<Int64, String> objectDictionary = new Dictionary<Int64, String> ();

            List<Core.Work.WorkQueue> availableItems = new List<Core.Work.WorkQueue> ();

            ClearLastException ();


            try {

                if (!useCaching) { CacheManager.RemoveObject (cacheKey); }

                availableItems = (List<Core.Work.WorkQueue>)CacheManager.GetObject (cacheKey);

                if (availableItems == null) {

                    availableItems = new List<Core.Work.WorkQueue> ();

                    String selectStatement = "SELECT * FROM dbo.WorkQueue WHERE WorkQueueId <> 0 ORDER BY WorkQueueName";

                    System.Data.DataTable availableTable = EnvironmentDatabase.SelectDataTable (selectStatement);

                    foreach (System.Data.DataRow currentRow in availableTable.Rows) {

                        Core.Work.WorkQueue item = new Core.Work.WorkQueue (this);

                        item.MapDataFields (currentRow);

                        availableItems.Add (item);


                        // CACHE EACH INDIVIDUAL ITEM LOADED FROM SERVER (BY ID AND NAME)

                        CacheManager.CacheObject ("Application." + session.EnvironmentId + "." + item.ObjectType + "." + item.Id.ToString (), item, CacheExpirationData);

                        CacheManager.CacheObject ("Application." + session.EnvironmentId + "." + item.ObjectType + "." + item.Name, item, CacheExpirationData);

                        objectDictionary.Add (item.Id, item.Name);

                    }

                    if (availableItems.Count > 0) {

                        // CACHE THE AVAILABILITY LIST

                        CacheManager.CacheObject (cacheKey, availableItems, CacheExpirationData);


                        // CACHE THE DICTIONARY THAT WAS CREATED

                        CacheManager.CacheObject ("Application." + session.EnvironmentId + "." + availableItems[0].ObjectType + ".Dictionary", objectDictionary, CacheExpirationData);

                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return availableItems;

        }


        public Int64 WorkQueueGetIdByName (String workQueueName) { return CoreObjectGetIdByName ("WorkQueue", workQueueName); }

        public String WorkQueueGetNameById (Int64 workQueueId) { return CoreObjectGetNameById ("WorkQueue", workQueueId); }


        public Core.Work.WorkQueue WorkQueueGet (Int64 workQueueId) {

            if (workQueueId == 0) { return null; }


            Core.Work.WorkQueue workQueue = null;

            ClearLastException ();

            try {

                workQueue = new Mercury.Server.Core.Work.WorkQueue (this, workQueueId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workQueue;

        }

        public Core.Work.WorkQueue WorkQueueGet (String workQueueName) {

            if (String.IsNullOrWhiteSpace (workQueueName)) { return null; }


            Int64 workQueueId = CoreObjectGetIdByName ("WorkQueue", workQueueName);

            Core.Work.WorkQueue workQueue = WorkQueueGet (workQueueId);

            return workQueue;

        }

        public Int64 WorkQueueGetIdByWorkQueueItem (Int64 workQueueItemId) {

            if (workQueueItemId == 0) { return 0; }

            Int64 workQueueId = 0;

            SetLastException (null);

            try {

                workQueueId = Convert.ToInt64 (EnvironmentDatabase.LookupValue ("WorkQueueItem", "WorkQueueId", "WorkQueueItemId = " + workQueueItemId.ToString (), 0));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workQueueId;

        }

        
        public List<Core.Work.WorkQueue> WorkQueuesForSession () {

            List<Core.Work.WorkQueue> results = new List<Core.Work.WorkQueue> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("EXEC WorkQueue_SelectForSession ");

            selectStatement.Append (session.SecurityAuthorityId.ToString () + ", '" + session.UserAccountId + "'");


            System.Data.DataTable workQueueTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in workQueueTable.Rows) {

                Mercury.Server.Core.Work.WorkQueue workQueue = new Mercury.Server.Core.Work.WorkQueue (this);

                workQueue.MapDataFields (currentRow);

                results.Add (workQueue);

            }

            return results;

        }

        public List<Core.Work.DataViews.WorkQueuePermission> WorkQueuePermissionsForSession () {

            List<Core.Work.DataViews.WorkQueuePermission> results = new List<Mercury.Server.Core.Work.DataViews.WorkQueuePermission> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("EXEC WorkQueue_SelectForSession ");

            selectStatement.Append (session.SecurityAuthorityId.ToString () + ", '" + session.UserAccountId + "'");


            System.Data.DataTable workQueueTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in workQueueTable.Rows) {

                Mercury.Server.Core.Work.DataViews.WorkQueuePermission workQueuePermission = new Mercury.Server.Core.Work.DataViews.WorkQueuePermission ();

                workQueuePermission.MapDataFields (currentRow);

                results.Add (workQueuePermission);

            }

            return results;

        }

        public List<Core.Work.DataViews.WorkQueuePermission> WorkQueuePermissionsForUser (Int64 securityAuthorityId, String userAccountId) {

            List<Core.Work.DataViews.WorkQueuePermission> results = new List<Mercury.Server.Core.Work.DataViews.WorkQueuePermission> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("EXEC WorkQueue_SelectForSession ");

            selectStatement.Append (securityAuthorityId.ToString () + ", '" + userAccountId + "'");


            System.Data.DataTable workQueueTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in workQueueTable.Rows) {

                Mercury.Server.Core.Work.DataViews.WorkQueuePermission workQueuePermission = new Mercury.Server.Core.Work.DataViews.WorkQueuePermission ();

                workQueuePermission.MapDataFields (currentRow);

                results.Add (workQueuePermission);

            }

            return results;

        }

        public List<Core.Work.DataViews.WorkQueuePermission> WorkQueuePermissionsForUserByEnvironment (Int64 securityAuthorityId, String userAccountId, String environmentName) {

            List<Core.Work.DataViews.WorkQueuePermission> results = new List<Mercury.Server.Core.Work.DataViews.WorkQueuePermission> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("EXEC WorkQueue_SelectForSession ");

            selectStatement.Append (securityAuthorityId.ToString () + ", '" + userAccountId + "'");


            System.Data.DataTable workQueueTable = EnvironmentDatabaseByName (environmentName).SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in workQueueTable.Rows) {

                Mercury.Server.Core.Work.DataViews.WorkQueuePermission workQueuePermission = new Mercury.Server.Core.Work.DataViews.WorkQueuePermission ();

                workQueuePermission.MapDataFields (currentRow);

                results.Add (workQueuePermission);

            }

            return results;

        }


        public Boolean WorkQueueInsertObject (Int64 workQueueId, String itemObjectType, Int64 itemObjectId, String itemName, String itemDescription, String itemGroupKey, Core.CoreObject sender, Core.CoreObject eventObject, Int64 eventInstanceId, String eventDescription, Int32 priority) {

            Boolean success = false;

            try {

                Data.AuthorityAccountStamp modified = new Mercury.Server.Data.AuthorityAccountStamp (session);


                StringBuilder workQueueItemInsert = new StringBuilder ();

                workQueueItemInsert.Append ("EXEC WorkQueueItem_Insert ");

                workQueueItemInsert.Append (workQueueId.ToString () + ", ");


                workQueueItemInsert.Append ("'" + itemObjectType + "', ");

                workQueueItemInsert.Append (itemObjectId.ToString () + ", ");

                workQueueItemInsert.Append ("'" + itemName.Replace ("'", "''") + "', ");

                workQueueItemInsert.Append ("'" + itemDescription.Replace ("'", "''") + "', ");

                workQueueItemInsert.Append ("'" + itemGroupKey.Replace ("'", "''") + "', ");


                workQueueItemInsert.Append ("'" + ((sender != null) ? sender.GetType ().ToString () : String.Empty) + "', ");

                workQueueItemInsert.Append (((sender != null) ? sender.Id.ToString () : "0") + ", ");

                workQueueItemInsert.Append ("'" + ((eventObject != null) ? eventObject.GetType ().ToString () : String.Empty) + "', ");

                workQueueItemInsert.Append (((eventObject != null) ? eventObject.Id.ToString () : "0") + ", ");

                workQueueItemInsert.Append (eventInstanceId.ToString () + ", ");

                workQueueItemInsert.Append ("'" + CommonFunctions.SetValueMaxLength (eventDescription.Replace ("'", "''"), Server.Data.DataTypeConstants.Description) + "', ");


                workQueueItemInsert.Append (priority.ToString () + ", ");

                workQueueItemInsert.Append ("NULL, "); // TODO: WORK TIME RESTRICTION


                workQueueItemInsert.Append ("'" + modified.SecurityAuthorityNameSql + "', ");

                workQueueItemInsert.Append ("'" + modified.UserAccountIdSql + "', ");

                workQueueItemInsert.Append ("'" + modified.UserAccountNameSql + "' ");

                success = EnvironmentDatabase.ExecuteSqlStatement (workQueueItemInsert.ToString (), 0);

                if ((!success) && (EnvironmentDatabase.LastException != null)) { throw EnvironmentDatabase.LastException; }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return success;

        }

        public Boolean WorkQueueInsertEntity (Int64 workQueueId, Int64 entityId, String itemGroupKey, Core.CoreObject sender, Core.CoreObject eventObject, Int64 eventInstanceId, String eventDescription, Int32 priority) {

            Boolean success = false;

            try {

                Core.Entity.Entity entity = EntityGet (entityId);

                if (entity == null) { throw new ApplicationException ("Unknown Entity Id: " + entityId.ToString ()); }

                String workQueueName = CoreObjectGetNameById ("WorkQueue", workQueueId);

                if (String.IsNullOrEmpty (workQueueName)) { throw new ApplicationException ("Unknown Work Queue Id: " + workQueueId.ToString ()); }

                if (entity != null) {

                    String objectType = entity.EntityType.ToString ().Split ('.')[entity.EntityType.ToString ().Split ('.').Length - 1];

                    Int64 objectId = 0;

                    String itemDescription = entity.Name;

                    switch (entity.EntityType) {

                        case Mercury.Server.Core.Enumerations.EntityType.Member: objectId = MemberGetByEntityId (entityId).Id; break;

                        case Mercury.Server.Core.Enumerations.EntityType.Provider: objectId = ProviderGetByEntityId (entityId).Id; break;

                    }

                    success = WorkQueueInsertObject (workQueueId, objectType, objectId, entity.Name, entity.Description, itemGroupKey, sender, eventObject, eventInstanceId, eventDescription, priority);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

                success = false;

            }

            return success;

        }

        public Boolean WorkQueueRemoveEntity (Int64 workQueueId, Int64 entityId, Int64 workOutcomeId, Core.CoreObject sender, Core.CoreObject eventObject, Int64 eventInstanceId, String eventDescription) {

            Boolean success = true;

            try {

                Core.Entity.Entity entity = EntityGet (entityId);

                if (entity != null) {

                    String objectType = entity.EntityType.ToString ().Split ('.')[entity.EntityType.ToString ().Split ('.').Length - 1];

                    Int64 objectId = 0;

                    String itemDescription = entity.Name;

                    switch (entity.EntityType) {

                        case Mercury.Server.Core.Enumerations.EntityType.Member: objectId = MemberGetByEntityId (entityId).Id; break;

                        case Mercury.Server.Core.Enumerations.EntityType.Provider: objectId = ProviderGetByEntityId (entityId).Id; break;

                    }

                    Core.Work.WorkQueueItem workQueueItem;

                    Int64 workQueueItemId;

                    String selectCriteria = "WorkQueueId = " + workQueueId + " AND ItemObjectType = '" + objectType + "' AND ItemObjectId = " + objectId + " AND CompletionDate IS NULL";

                    workQueueItemId = Convert.ToInt64 (EnvironmentDatabase.LookupValue ("WorkQueueItem", "WorkQueueItemId", selectCriteria, 0));

                    workQueueItem = WorkQueueItemGet (workQueueItemId);

                    if (workQueueItem != null) {

                        workQueueItem.WorkflowNextStep = String.Empty;

                        workQueueItem.CompletionDate = DateTime.Now;

                        workQueueItem.WorkOutcomeId = workOutcomeId;

                        success = workQueueItem.Save ();

                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

                success = false;

            }

            return success;

        }

        #endregion 


        #region Work - Work Queue Item

        public Core.Work.WorkQueueItem WorkQueueItemGet (Int64 workQueueItemId) {

            Core.Work.WorkQueueItem workQueueItem = null;

            SetLastException (null);

            try {

                if (workQueueItemId != 0) {

                    workQueueItem = new Mercury.Server.Core.Work.WorkQueueItem (this, workQueueItemId);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workQueueItem;

        }

        public Core.Work.WorkQueueItem WorkQueueItemGet (Guid workflowInstanceId) {

            Int64 workQueueItemId;

            Core.Work.WorkQueueItem workQueueItem = null;

            SetLastException (null);

            try {

                if (Int64.TryParse (Convert.ToString (EnvironmentDatabase.LookupValue ("WorkQueueItem", "WorkQueueItemId", "WorkflowInstanceId = '" + workflowInstanceId.ToString () + "'", 0)), out workQueueItemId)) {

                    workQueueItem = this.WorkQueueItemGet (workQueueItemId);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workQueueItem;

        }

        public Int64 WorkQueueItemGetIdByWorkflowInstance (Guid workflowInstanceId) {

            Int64 workQueueItemId = 0;

            SetLastException (null);

            try {

                Int64.TryParse (Convert.ToString (EnvironmentDatabase.LookupValue ("WorkQueueItem", "WorkQueueItemId", "WorkflowInstanceId = '" + workflowInstanceId.ToString () + "'", 0)), out workQueueItemId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workQueueItemId;

        }

        /// <summary>
        /// Retreives Open (non-completed) Work Queue Items from a specific Work Queue
        /// that contain the same Item Group Key. Filters by Assignment Status.
        /// </summary>
        /// <param name="workQueueId">Work Queue Id</param>
        /// <param name="itemGroupKey">Item Group Key (Empty Key will return 0 Items)</param>
        /// <param name="ignoreAssignment">True - returns all items regardless of assignment. Otherwise, returns only unassigned and session assigned items.</param>
        /// <returns>List of Work Queue Items</returns>
        public List<Core.Work.WorkQueueItem> WorkQueueItemsGetByItemGroupKey (Int64 workQueueId, String itemGroupKey, Boolean ignoreAssignment) {

            List<Core.Work.WorkQueueItem> workQueueItems = new List<Mercury.Server.Core.Work.WorkQueueItem> ();

            if (String.IsNullOrWhiteSpace (itemGroupKey)) { return workQueueItems; }


            SetLastException (null);

            try {

                String sqlStatement = "EXEC WorkQueueItem_SelectByItemGroupKey " + workQueueId.ToString () + ", '" + itemGroupKey.Replace ("'", "''") + "', ";

                sqlStatement = sqlStatement + session.SecurityAuthorityId.ToString () + ", '" + session.UserAccountId.Replace ("'", "''") + "', " + ignoreAssignment.ToString ();

                System.Data.DataTable itemsTable = EnvironmentDatabase.SelectDataTable (sqlStatement, 0);

                foreach (System.Data.DataRow currentRow in itemsTable.Rows) {

                    Core.Work.WorkQueueItem workQueueItem = new Mercury.Server.Core.Work.WorkQueueItem (this);

                    workQueueItem.MapDataFields (currentRow);

                    workQueueItems.Add (workQueueItem);

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }



            return workQueueItems;

        }


        public List<Core.Work.WorkQueueItemSender> WorkQueueItemSendersGet (Int64 workQueueItemId) {

            List<Core.Work.WorkQueueItemSender> senders = new List<Mercury.Server.Core.Work.WorkQueueItemSender> ();

            ClearLastException ();

            try {

                String sqlStatement = "SELECT * FROM WorkQueueItemSender WHERE WorkQueueItemId = " + workQueueItemId.ToString ();

                System.Data.DataTable senderTable = EnvironmentDatabase.SelectDataTable (sqlStatement, 0);

                foreach (System.Data.DataRow currentRow in senderTable.Rows) {

                    Core.Work.WorkQueueItemSender itemSender = new Mercury.Server.Core.Work.WorkQueueItemSender (this);

                    itemSender.MapDataFields (currentRow);

                    senders.Add (itemSender);

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return senders;

        }

        public List<Core.Work.WorkQueueItemAssignmentHistory> WorkQueueItemAssignmentHistoryGet (Int64 workQueueItemId) {

            List<Core.Work.WorkQueueItemAssignmentHistory> history = new List<Mercury.Server.Core.Work.WorkQueueItemAssignmentHistory> ();

            SetLastException (null);

            try {

                String sqlStatement = "SELECT * FROM WorkQueueItemAssignmentHistory WHERE WorkQueueItemId = " + workQueueItemId.ToString () + " ORDER BY AssignedToDate, WorkQueueItemAssignmentHistoryId";

                System.Data.DataTable senderTable = EnvironmentDatabase.SelectDataTable (sqlStatement, 0);

                foreach (System.Data.DataRow currentRow in senderTable.Rows) {

                    Core.Work.WorkQueueItemAssignmentHistory itemAssignmentHistory = new Mercury.Server.Core.Work.WorkQueueItemAssignmentHistory (this);

                    itemAssignmentHistory.MapDataFields (currentRow);

                    history.Add (itemAssignmentHistory);

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return history;

        }

        public Boolean WorkQueueItemSetException (Int64 workQueueItemId, String exceptionMessage) {

            Boolean success = false;

            Core.Work.WorkQueueItem workQueueItem = WorkQueueItemGet (workQueueItemId);

            Core.Work.WorkOutcome workOutcome = WorkOutcomeGet ("Unhandled Exception Occurred");

            if (workQueueItem != null) {

                workQueueItem.WorkflowLastStep = exceptionMessage;

                workQueueItem.WorkflowNextStep = "Unhandled Exception Occurred";

                workQueueItem.WorkOutcomeId = (workOutcome != null) ? workOutcome.Id : 0;

                workQueueItem.CompletionDate = DateTime.Now;

                success = workQueueItem.Save ();

            }

            return success;

        }


        public Boolean WorkQueueItemWorkflowStepsSave (Int64 workQueueItemId, List<Workflows.WorkflowStep> workflowSteps) {

            if (workQueueItemId == 0) { return false; }

            Boolean success = true;

            String executeStatement = String.Empty;

            SetLastException (null);

            try {
                
                executeStatement = "DELETE FROM WorkQueueItemWorkflowStep WHERE WorkQueueItemId = " + workQueueItemId.ToString () + ";";

                if ((success) && (workQueueItemId != 0)) {

                    DateTime cycleTime = DateTime.Now;

                    Int32 currentStepIndex = 0;

                    if (workflowSteps == null) { workflowSteps = new List<Mercury.Server.Workflows.WorkflowStep> (); }

                    foreach (Workflows.WorkflowStep currentWorkflowStep in workflowSteps) {

                        currentStepIndex = currentStepIndex + 1;


                        StringBuilder sqlStatement = new StringBuilder ();

                        sqlStatement.Append ("INSERT INTO WorkQueueItemWorkflowStep VALUES (");

                        sqlStatement.Append (workQueueItemId.ToString () + ", ");

                        sqlStatement.Append (currentStepIndex.ToString () + ", ");

                        sqlStatement.Append ("'" + currentWorkflowStep.StepDate.ToString () + "', ");

                        sqlStatement.Append (((Int32)currentWorkflowStep.StepStatus).ToString () + ", ");

                        sqlStatement.Append ("'" + currentWorkflowStep.Name.Replace ("'", "''") + "', ");

                        sqlStatement.Append ("'" + currentWorkflowStep.Description.Replace ("'", "''") + "', ");

                        sqlStatement.Append ("'" + currentWorkflowStep.UserDisplayName.Replace ("'", "''") + "', ");

                        sqlStatement.Append (currentWorkflowStep.CreateAccountInfo.AccountInfoSql);

                        sqlStatement.Append ( ", '" + currentWorkflowStep.CreateAccountInfo.ActionDate.ToString () + "' ");

                        sqlStatement.Append ("); \r\n  ");

                        executeStatement += sqlStatement.ToString ();

                    }

                    if (!String.IsNullOrWhiteSpace (executeStatement)) {

                        success = EnvironmentDatabase.ExecuteSqlStatement (executeStatement, 0);

                        if (!success) { throw EnvironmentDatabase.LastException; }

                    }

                }

            }

            catch (Exception saveException) {

                SetLastException (saveException);

                success = false;

            }

            return success;

        }

        public List<Workflows.WorkflowStep> WorkQueueItemWorkflowStepsGet (Int64 workQueueItemId) {

            List<Workflows.WorkflowStep> workflowSteps = new List<Mercury.Server.Workflows.WorkflowStep> ();

            String sqlStatement = String.Empty;


            SetLastException (null);

            try {

                sqlStatement = "SELECT * FROM WorkQueueItemWorkflowStep WHERE WorkQueueItemId = " + workQueueItemId.ToString () + " ORDER BY StepDate, StepSequence";

                System.Data.DataTable workflowStepsTable = EnvironmentDatabase.SelectDataTable (sqlStatement, 0);

                foreach (System.Data.DataRow currentRow in workflowStepsTable.Rows) {

                    Workflows.WorkflowStep workflowStep = new Mercury.Server.Workflows.WorkflowStep ();

                    workflowStep.MapDataFields (currentRow);

                    workflowSteps.Add (workflowStep);

                }

            }

            catch (Exception saveException) {

                SetLastException (saveException);

            }

            return workflowSteps;

        }


        public Core.Work.WorkQueueItem WorkQueueItemGetByWorkQueueObjectIdOpen (Int64 workQueueId, String itemObjectType, Int64 itemObjectId) {

            Int64 workQueueItemId;

            Core.Work.WorkQueueItem workQueueItem = null;

            SetLastException (null);

            try {

                String parameterString = "WorkQueueId = " + workQueueId;

                parameterString = parameterString + " AND ItemObjectType = '" + itemObjectType.Replace ("'", "''") + "'";

                parameterString = parameterString + " AND ItemObjectId = " + itemObjectId.ToString ();

                parameterString = parameterString + " AND CompletionDate IS NULL";

                if (Int64.TryParse (Convert.ToString (EnvironmentDatabase.LookupValue ("WorkQueueItem", "WorkQueueItemId", parameterString, 0)), out workQueueItemId)) {

                    workQueueItem = this.WorkQueueItemGet (workQueueItemId);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workQueueItem;

        }

        public Core.Work.WorkQueueItem WorkQueueItemGetByObjectIdMostRecent (Int64 workQueueId, String objectType, Int64 objectId) {

            if ((workQueueId == 0) || (objectId == 0)) { return null; }

            Core.Work.WorkQueueItem workQueueItem = null;

            SetLastException (null);

            try {

                String sqlStatement = "EXEC WorkQueueItem_SelectByObjectMostRecent " + workQueueId.ToString () + ", '" + objectType + "', " + objectId.ToString ();

                System.Data.DataTable workQueueItemTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                if (workQueueItemTable.Rows.Count == 1) {

                    workQueueItem = new Server.Core.Work.WorkQueueItem (this);

                    workQueueItem.MapDataFields (workQueueItemTable.Rows[0]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workQueueItem;

        }

        public Core.Work.WorkQueueItem WorkQueueItemGetByObjectIdMostRecent (String workQueueName, String objectType, Int64 objectId) {

            Int64 workQueueId = CoreObjectGetIdByName ("WorkQueue", workQueueName);

            return WorkQueueItemGetByObjectIdMostRecent (workQueueId, objectType, objectId);

        }

        #endregion 

   
        #region Work Queue Items (By Views)

        public String WorkQueueItemsGetSqlStatement (List<Data.FilterDescriptor> filters) {

            String sqlStatement = String.Empty;


            String selectClause = String.Empty;

            String fromClause = String.Empty;

            String whereClause = String.Empty;

            String orderByClause = String.Empty;


            selectClause = "SELECT WorkQueueItem.* /*_CUSTOM_FIELD_INSERT_*/ ";

            selectClause = selectClause + "\r\n /*_CALCULATED_FIELDS_BEGIN_*/ ";

            selectClause = selectClause + "\r\n    , CASE WHEN (GETDATE () > ConstraintDate) THEN 1 ELSE 0 END AS HasConstraintDatePassed";

            selectClause = selectClause + "\r\n    , CASE WHEN (GETDATE () > DueDate) THEN 1 ELSE 0 END AS HasDueDatePassed";

            selectClause = selectClause + "\r\n    , CASE WHEN (GETDATE () > MilestoneDate) THEN 1 ELSE 0 END AS HasMilestoneDatePassed";

            selectClause = selectClause + "\r\n    , CASE WHEN (GETDATE () > ThresholdDate) THEN 1 ELSE 0 END AS HasThresholdDatePassed";

            selectClause = selectClause + "\r\n    , CASE WHEN (CompletionDate IS NOT NULL) THEN 1 ELSE 0 END AS IsCompleted";

            selectClause = selectClause + "\r\n    , CASE WHEN (AssignedToSecurityAuthorityId = 0) THEN 0 ELSE 1 END AS IsAssigned";

            selectClause = selectClause + "\r\n    , CAST (CASE WHEN (WorkTimeRestrictions IS NULL) THEN 1 ";
            
            selectClause = selectClause + "ELSE CAST ((ISNULL (WorkTimeRestrictions.value ('(DayOfWeekTimes/";

            selectClause = selectClause + "Day[@DayOfWeek=\"" + ((Int32) DateTime.Today.DayOfWeek).ToString () + "\"]/";

            selectClause = selectClause + "Time[@StartTime <= \"" + DateTime.Now.ToString ("hh:mm:ss") + "\"";

            selectClause = selectClause + "and @EndTime >= \"" + DateTime.Now.ToString ("hh:mm:ss") + "\"])[1]', 'BIT'), 1) - 1) AS BIT) END AS BIT) AS WithinWorkTimeRestrictions";



            selectClause = selectClause + "\r\n /*_CALCULATED_FIELDS_END_*/ ";


            fromClause = "  FROM WorkQueueItem ";

            whereClause = "  WHERE (WorkQueueItemId <> 0) /*_CUSTOM_FILTER_INSERT_*/";


            if (filters == null) { filters = new List<Mercury.Server.Data.FilterDescriptor> (); }

            foreach (Data.FilterDescriptor currentFilter in filters) {

                String criteriaString = String.Empty;

                switch (currentFilter.PropertyPath) {

                    #region Core Object Properties

                    case "Id":

                    case "Name":

                    case "Description":

                        currentFilter.PropertyPath = "WorkQueueItem" + currentFilter.PropertyPath;

                        criteriaString = currentFilter.SqlCriteriaString (String.Empty);

                        break;

                    #endregion 

                    #region Work Queue Item Properties

                    case "WorkQueueItemId":

                    case "WorkQueueItemName":

                    case "WorkQueueItemDescription":


                    case "WorkQueueId":


                    case "ItemObjectType":

                    case "ItemObjectId":

                    case "ItemGroupKey":


                    case "WorkflowInstanceId":

                    case "WorkflowStatus":

                    case "WorkflowLastStep":

                    case "WorkflowNextStep":


                    case "AddedDate":

                    case "LastWorkedDate":

                    case "ConstraintDate":

                    case "MilestoneDate":

                    case "ThresholdDate":

                    case "DueDate":

                    case "CompletionDate":

                    case "WorkOutcomeId":


                    case "Priority":


                    case "AssignedToSecurityAuthorityId":

                    case "AssignedToUserAccountId":

                    case "AssignedToUserAccountName":

                    case "AssignedToUserDisplayName":

                    case "AssignedToDate":

                        criteriaString = currentFilter.SqlCriteriaString (String.Empty);

                        break;

                    #endregion

                    #region Calculated Fields

                    case "HasConstraintDatePassed":

                    case "HasDueDatePassed":

                    case "HasMilestoneDatePassed":

                    case "HasThresholdDatePassed":

                        if (currentFilter.Parameter.Value is Boolean) {

                            criteriaString = "(GETDATE () {} " + currentFilter.PropertyPath.Replace ("Has", "").Replace ("Passed", "") + ")";

                            if ((Boolean) currentFilter.Parameter.Value) { criteriaString = criteriaString.Replace ("{}", ">="); }

                            else { criteriaString = criteriaString.Replace ("{}", "<"); }

                        }

                        break;

                    case "IsCompleted":

                        if (currentFilter.Parameter.Value is Boolean) {

                            criteriaString = "(CompletionDate IS " + (((Boolean) currentFilter.Parameter.Value) ? "NOT " : String.Empty) + "NULL)";

                        }

                        break;

                    case "IsAssigned":

                        if (currentFilter.Parameter.Value is Boolean) {

                            criteriaString = "(AssignedToSecurityAuthorityId {} 0)";

                            if ((Boolean) currentFilter.Parameter.Value) { criteriaString = criteriaString.Replace ("{}", "<>"); }

                            else { criteriaString = criteriaString.Replace ("{}", "="); }

                        }

                        break;

                    case "WithinWorkTimeRestrictions":

                        if (currentFilter.Parameter.Value is Boolean) {
                            
                            criteriaString = "(CAST (CASE WHEN (WorkTimeRestrictions IS NULL) THEN 1 ";

                            criteriaString += "ELSE CAST ((ISNULL (WorkTimeRestrictions.value ('(DayOfWeekTimes/";

                            criteriaString += "Day[@DayOfWeek=\"" + ((Int32) DateTime.Today.DayOfWeek).ToString () + "\"]/";

                            criteriaString += "Time[@StartTime <= \"" + DateTime.Now.ToString ("HH:mm:ss") + "\"";

                            criteriaString += "and @EndTime >= \"" + DateTime.Now.ToString ("HH:mm:ss") + "\"])[1]', 'BIT'), 1) - 1) AS BIT) END AS BIT)) {} 0";

                            if ((Boolean) currentFilter.Parameter.Value) { criteriaString = criteriaString.Replace ("{}", "<>"); }

                            else { criteriaString = criteriaString.Replace ("{}", "="); }

                        }

                        break;

                    #endregion

                    default:

                        #region Related Object Properties

                        if (currentFilter.PropertyPath.Split ('.').Length > 1) {

                            switch (currentFilter.PropertyPath.Split ('.')[0]) {

                                case "WorkQueue":

                                    if (!fromClause.Contains (" JOIN WorkQueue ")) {

                                        fromClause = fromClause + "    JOIN WorkQueue ON WorkQueueItem.WorkQueueId = WorkQueue.WorkQueueId \r\n  ";

                                    }

                                    currentFilter.PropertyPath = currentFilter.PropertyPath.Replace (".Name", ".WorkQueueName");

                                    criteriaString = currentFilter.SqlCriteriaString (String.Empty);

                                    break;

                            }

                        }

                        #endregion

                        break;

                } // switch (currentFilter.PropertyPath) {

                if (!String.IsNullOrEmpty (criteriaString)) {

                    whereClause = whereClause + "  AND " + criteriaString;

                }

            }


            sqlStatement = selectClause + fromClause + whereClause + orderByClause;

            return sqlStatement;

        }

        public String WorkQueueItemsGetRowNumberSql (List<Data.SortDescriptor> sorts, Core.Work.WorkQueueView workQueueView) {

            String sqlString = String.Empty;


            // CUSTOM SORTS TAKE PRECEDENCE OVER WORK QUEUE VIEW AND DEFAULT SORT

            if (sorts == null) { sorts = new List<Data.SortDescriptor> (); }

            foreach (Data.SortDescriptor currentSort in sorts) {

                sqlString = sqlString + currentSort.SqlSortList + ", ";

            }


            if (workQueueView != null) {

                // CREATE WORK QUEUE VIEW SORT

                if (workQueueView.SortDefinitions == null) { workQueueView.SortDefinitions = new SortedList<Int32, Mercury.Server.Core.Work.WorkQueueViewSortDefinition> (); }

                foreach (Core.Work.WorkQueueViewSortDefinition currentSortDefinition in workQueueView.SortDefinitions.Values) {

                    sqlString = sqlString + currentSortDefinition.SqlSortList + ", ";

                }

            }

            else {

                // DEFAULT SORT 

                sqlString = sqlString + "    ISNULL (CompletionDate, '12/31/9999') DESC, \r\n  ";

                sqlString = sqlString + "    CASE WHEN (GETDATE () > ConstraintDate) THEN 0 ELSE 1 END, \r\n  ";

                sqlString = sqlString + "    CAST ((ISNULL (WorkTimeRestrictions.value ('(DayOfWeekTimes/";

                sqlString = sqlString + "Day[@DayOfWeek=\"" + ((Int32) DateTime.Today.DayOfWeek).ToString () + "\"]/";

                sqlString = sqlString + "Time[@StartTime <= \"" + DateTime.Now.ToString ("hh:mm:ss") + "\"";

                sqlString = sqlString + "and @EndTime >= \"" + DateTime.Now.ToString ("hh:mm:ss") + "\"])[1]', 'BIT'), 1) - 1) AS BIT) DESC, \r\n";

                sqlString = sqlString + "    CASE WHEN (GETDATE () > DueDate) THEN 0 ELSE 1 END, \r\n  ";

                sqlString = sqlString + "    CASE WHEN (GETDATE () > MilestoneDate) THEN 0 ELSE 1 END, \r\n  ";

                sqlString = sqlString + "    DueDate, MilestoneDate, LastWorkedDate, AddedDate, \r\n  ";

            }

            // APPEND WORK QUEUE ITEM ID AS ALWAYS LAST SORT OPTION

            sqlString = sqlString + "    WorkQueueItemId \r\n  ";


            sqlString = "    ROW_NUMBER () OVER (ORDER BY \r\n  " + sqlString;

            sqlString = sqlString + ") AS RowNumber, \r\n  ";

            return sqlString;

        }

        public String WorkQueueItemsGetJoinSql (List<Data.SortDescriptor> sorts, Core.Work.WorkQueueView workQueueView) {

            String joinStatement = String.Empty;


            // WORK QUEUE VIEW IS RESERVED FOR FUTURE FUNCTIONALITY AND NOT SUPPORTED AT THIS TIME 


            // CUSTOM SORTS TAKE PRECEDENCE OVER WORK QUEUE VIEW AND DEFAULT SORT

            if (sorts == null) { sorts = new List<Data.SortDescriptor> (); }


            // CREATE JOIN STATEMENT, STATEMENT IS AGAINST PAGE OUTER QUERY 

            foreach (Data.SortDescriptor currentSort in sorts) {

                switch (currentSort.FieldName.Split ('.')[0]) {

                    case "WorkQueue":

                        joinStatement += "  LEFT JOIN WorkQueue ON WorkQueueItem.WorkQueueId = WorkQueue.WorkQueueId \r\n";
                        
                        break;

                    case "Workflow":

                        joinStatement += "  LEFT JOIN WorkQueue AS WorkQueueWorkflow ON WorkQueueItem.WorkQueueId = WorkQueueWorkflow.WorkQueueId \r\n";

                        joinStatement += "  LEFT JOIN Workflow ON WorkQueueWorkflow.WorkflowId = Workflow.WorkflowId  \r\n";
                        
                        break;

                }

            }



            return joinStatement;

        }


        public Int64 WorkQueueItemsGetCount (List<Data.FilterDescriptor> filters) {

            Int64 itemCount = 0;


            SetLastException (null);

            try {

                String sqlStatement = "SELECT COUNT (1) AS CountOf FROM (" + WorkQueueItemsGetSqlStatement (filters) + ") AS SourceTable";

                itemCount = Convert.ToInt64 (EnvironmentDatabase.ExecuteScalar (sqlStatement));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return itemCount;

        }

        public Int64 WorkQueueItemsGetCount (Core.Work.WorkQueueView workQueueView, List<Data.FilterDescriptor> filters) {

            Int64 itemCount = 0;

            String sqlStatement = String.Empty;

            String customFields = String.Empty;

            String customFilters = String.Empty;


            SetLastException (null);

            try {

                if (workQueueView != null) {

                    // APPEND FILTERS THAT ARE FOR STANDARD FIELDS AND NOT CUSTOM FIELDS

                    foreach (Int32 currentFilterKey in workQueueView.FilterDefinitions.Keys) {

                        if (workQueueView.WellKnownFields.ContainsKey (workQueueView.FilterDefinitions[currentFilterKey].PropertyPath)) {

                            filters.Add ((Data.FilterDescriptor)workQueueView.FilterDefinitions[currentFilterKey]);

                        }

                    }

                }


                sqlStatement = WorkQueueItemsGetSqlStatement (filters);

                if (workQueueView != null) {

                    // ADD CUSTOM FIELDS

                    foreach (Core.Work.WorkQueueViewFieldDefinition currentFieldDefinition in workQueueView.FieldDefinitions) {

                        String extendedPropertyField = currentFieldDefinition.SqlSelectList;

                        if (!String.IsNullOrEmpty (extendedPropertyField)) { customFields = customFields + ", " + extendedPropertyField; }

                    }

                    sqlStatement = sqlStatement.Replace ("/*_CUSTOM_FIELD_INSERT_*/", customFields);


                    // APPEND FILTERS THAT ARE NOT STANDARD FIELDS 

                    foreach (Int32 currentFilterKey in workQueueView.FilterDefinitions.Keys) {

                        if (!workQueueView.WellKnownFields.ContainsKey (workQueueView.FilterDefinitions[currentFilterKey].PropertyPath)) {

                            foreach (Core.Work.WorkQueueViewFieldDefinition currentFieldDefinition in workQueueView.FieldDefinitions) {

                                if (currentFieldDefinition.DisplayName == workQueueView.FilterDefinitions[currentFilterKey].PropertyPath) {

                                    Data.FilterDescriptor filterDescriptor = new Data.FilterDescriptor (

                                        currentFieldDefinition.SqlDeclaration,

                                        workQueueView.FilterDefinitions[currentFilterKey].Operator,

                                        workQueueView.FilterDefinitions[currentFilterKey].Parameter.Value);

                                    customFilters = customFilters + " AND (" + filterDescriptor.SqlCriteriaString (String.Empty) + ")";

                                }

                            }

                        }

                    }


                    sqlStatement = sqlStatement.Replace ("/*_CUSTOM_FILTER_INSERT_*/", customFilters);


                }


                sqlStatement = "SELECT COUNT (1) AS CountOf FROM (" + sqlStatement + ") AS SourceTable";

                itemCount = Convert.ToInt64 (EnvironmentDatabase.ExecuteScalar (sqlStatement));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return itemCount;

        }

        public List<Core.Work.WorkQueueItem> WorkQueueItemsGetByViewPage (Core.Work.WorkQueueView workQueueView, List<Data.FilterDescriptor> filters, List<Data.SortDescriptor> sorts, Int64 initialRow, Int64 count) {

            List<Core.Work.WorkQueueItem> items = new List<Mercury.Server.Core.Work.WorkQueueItem> ();

            System.Data.DataTable itemTable;

            String sqlStatement = String.Empty;

            String customFilters = String.Empty;

            String customFields = String.Empty;

            String extendedPropertyField;


            ClearLastException ();

            try {

                if (workQueueView != null) {

                    // APPEND FILTERS THAT ARE FOR STANDARD FIELDS AND NOT CUSTOM FIELDS

                    foreach (Int32 currentFilterKey in workQueueView.FilterDefinitions.Keys) {

                        if (workQueueView.WellKnownFields.ContainsKey (workQueueView.FilterDefinitions[currentFilterKey].PropertyPath)) {

                            filters.Add ((Data.FilterDescriptor)workQueueView.FilterDefinitions[currentFilterKey]);

                        }

                    }

                }


                sqlStatement = sqlStatement + "SELECT WorkQueueItemPage.* FROM ( \r\n";

                sqlStatement = sqlStatement + "SELECT \r\n ";

                sqlStatement = sqlStatement + WorkQueueItemsGetRowNumberSql (sorts, workQueueView);

                sqlStatement = sqlStatement + "    WorkQueueItem.* \r\n";

                sqlStatement = sqlStatement + "  FROM (" + WorkQueueItemsGetSqlStatement (filters) + ") AS WorkQueueItem \r\n";

                // ADD JOINS THAT ARE REQUIRED FOR THE SORTING PARAMETERS

                sqlStatement = sqlStatement + WorkQueueItemsGetJoinSql (sorts, workQueueView);

                sqlStatement = sqlStatement + ") AS WorkQueueItemPage \r\n";


                sqlStatement = sqlStatement + "  WHERE WorkQueueItemPage.RowNumber BETWEEN " + initialRow.ToString ();

                sqlStatement = sqlStatement + " AND (" + initialRow.ToString () + " + " + count.ToString () + " - 1)";


                if (workQueueView != null) {

                    // ADD CUSTOM FIELDS

                    foreach (Core.Work.WorkQueueViewFieldDefinition currentFieldDefinition in workQueueView.FieldDefinitions) {

                        extendedPropertyField = currentFieldDefinition.SqlSelectList;

                        if (!String.IsNullOrEmpty (extendedPropertyField)) { customFields = customFields + ", " + extendedPropertyField; }

                    }

                    sqlStatement = sqlStatement.Replace ("/*_CUSTOM_FIELD_INSERT_*/", customFields);


                    // APPEND FILTERS THAT ARE NOT STANDARD FIELDS 

                    foreach (Int32 currentFilterKey in workQueueView.FilterDefinitions.Keys) {

                        if (!workQueueView.WellKnownFields.ContainsKey (workQueueView.FilterDefinitions[currentFilterKey].PropertyPath)) {

                            foreach (Core.Work.WorkQueueViewFieldDefinition currentFieldDefinition in workQueueView.FieldDefinitions) {

                                if (currentFieldDefinition.DisplayName == workQueueView.FilterDefinitions[currentFilterKey].PropertyPath) {

                                    Data.FilterDescriptor filterDescriptor = new Data.FilterDescriptor (

                                        currentFieldDefinition.SqlDeclaration,

                                        workQueueView.FilterDefinitions[currentFilterKey].Operator,

                                        workQueueView.FilterDefinitions[currentFilterKey].Parameter.Value);

                                    customFilters = customFilters + " AND (" + filterDescriptor.SqlCriteriaString (String.Empty) + ")";

                                }

                            }

                        }

                    }


                    sqlStatement = sqlStatement.Replace ("/*_CUSTOM_FILTER_INSERT_*/", customFilters);


                }


                itemTable = EnvironmentDatabase.SelectDataTable (sqlStatement);

                foreach (System.Data.DataRow currentRow in itemTable.Rows) {

                    Core.Work.WorkQueueItem item = new Mercury.Server.Core.Work.WorkQueueItem (this);

                    item.MapDataFields (currentRow);

                    items.Add (item);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return items;

        }

        #endregion 

        
        #region Work - Work Queue Views

        public List<Core.Work.WorkQueueView> WorkQueueViewsAvailable () {

            List<Core.Work.WorkQueueView> results = new List<Mercury.Server.Core.Work.WorkQueueView> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("SELECT ");

            selectStatement.Append ("    WorkQueueView.* ");

            selectStatement.Append ("  FROM dbo.WorkQueueView AS WorkQueueView ");

            selectStatement.Append ("  ORDER BY WorkQueueViewName");


            System.Data.DataTable workQueueViewTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in workQueueViewTable.Rows) {

                Mercury.Server.Core.Work.WorkQueueView workQueueView = new Mercury.Server.Core.Work.WorkQueueView (this);

                workQueueView.MapDataFields (currentRow);

                results.Add (workQueueView);

            }

            return results;

        }

        public Dictionary<Int64, String> WorkQueueViewDictionary () {

            Dictionary<Int64, String> workQueueViewDictionary = new Dictionary<Int64, String> ();

            SetLastException (null);

            try {

                StringBuilder selectStatement = new StringBuilder ("SELECT WorkQueueViewId, WorkQueueViewName FROM WorkQueueView ORDER BY WorkQueueViewName");

                System.Data.DataTable workQueueViewTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

                foreach (System.Data.DataRow currentWorkQueueView in workQueueViewTable.Rows) {

                    workQueueViewDictionary.Add ((Int64) currentWorkQueueView["WorkQueueViewId"], (String) currentWorkQueueView["WorkQueueViewName"]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workQueueViewDictionary;

        }


        public Core.Work.WorkQueueView WorkQueueViewGet (Int64 workQueueViewId) {

            if (workQueueViewId == 0) { return null; }

            Core.Work.WorkQueueView workQueueView = null;

            SetLastException (null);

            try {

                workQueueView = new Mercury.Server.Core.Work.WorkQueueView (this, workQueueViewId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workQueueView;

        }

        public Core.Work.WorkQueueView WorkQueueViewGet (String workQueueViewName) {

            if (String.IsNullOrEmpty (workQueueViewName)) { return null; }

            Int64 workQueueViewId = WorkQueueViewGetIdByName (workQueueViewName);

            Core.Work.WorkQueueView workQueueView = null;

            if (workQueueViewId != 0) { workQueueView = WorkQueueViewGet (workQueueViewId); }

            return workQueueView;

        }

        public Int64 WorkQueueViewGetIdByName (String workQueueViewName) {

            if (String.IsNullOrEmpty (workQueueViewName)) { return 0; }

            Int64 workQueueViewId = 0;

            SetLastException (null);

            try {

                workQueueViewId = Convert.ToInt64 (EnvironmentDatabase.LookupValue ("WorkQueueView", "WorkQueueViewId", "WorkQueueViewName = '" + workQueueViewName + "'"));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workQueueViewId;

        }

        public String WorkQueueViewGetNameById (Int64 workQueueViewId) {

            if (workQueueViewId == 0) { return String.Empty; }

            String workQueueViewName = String.Empty;

            SetLastException (null);

            try {

                workQueueViewName = (String) EnvironmentDatabase.LookupValue ("WorkQueueView", "WorkQueueViewName", "WorkQueueViewId = " + workQueueViewId.ToString (), String.Empty);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workQueueViewName;

        }

        #endregion 
        

        #region Work - Work Team

        public List<Core.Work.WorkTeam> WorkTeamsAvailable () {

            List<Core.Work.WorkTeam> results = new List<Mercury.Server.Core.Work.WorkTeam> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("SELECT ");

            selectStatement.Append ("    WorkTeam.* ");

            selectStatement.Append ("  FROM dbo.WorkTeam AS WorkTeam ");

            selectStatement.Append ("  ORDER BY WorkTeamName");


            System.Data.DataTable workTeamTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in workTeamTable.Rows) {

                Mercury.Server.Core.Work.WorkTeam workTeam = new Mercury.Server.Core.Work.WorkTeam (this);

                workTeam.MapDataFields (currentRow);

                results.Add (workTeam);

            }

            return results;

        }

        public List<Core.Work.WorkTeam> WorkTeamsForSession () {

            List<Core.Work.WorkTeam> results = new List<Mercury.Server.Core.Work.WorkTeam> ();

            StringBuilder selectStatement = new StringBuilder ();

            SetLastException (null);


            selectStatement.Append ("SELECT ");

            selectStatement.Append ("    WorkTeam.* ");

            selectStatement.Append ("  FROM WorkTeam ");

            selectStatement.Append ("    JOIN WorkTeamMembership ON WorkTeam.WorkTeamId = WorkTeamMembership.WorkTeamId");

            selectStatement.Append ("  WHERE SecurityAuthorityId = " + session.SecurityAuthorityId.ToString () + " AND UserAccountId = '" + session.UserAccountId + "'");

            selectStatement.Append ("  ORDER BY WorkTeamName");


            System.Data.DataTable workTeamTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in workTeamTable.Rows) {

                Mercury.Server.Core.Work.WorkTeam workTeam = new Mercury.Server.Core.Work.WorkTeam (this);

                workTeam.MapDataFields (currentRow);

                results.Add (workTeam);

            }

            return results;

        }


        public Core.Work.WorkTeam WorkTeamGet (Int64 workTeamId) {

            Core.Work.WorkTeam workTeam = null;

            SetLastException (null);

            try {

                workTeam = new Mercury.Server.Core.Work.WorkTeam (this, workTeamId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workTeam;

        }

        public Core.Work.WorkTeam WorkTeamGet (String workTeamName) {

            Int64 workTeamId = WorkTeamGetIdByName (workTeamName);

            Core.Work.WorkTeam workTeam = null;

            if (workTeamId != 0) { workTeam = WorkTeamGet (workTeamId); }

            return workTeam;

        }

        public Int64 WorkTeamGetIdByName (String workTeamName) {

            Int64 workTeamId = 0;

            SetLastException (null);

            try {

                workTeamId = Convert.ToInt64 (EnvironmentDatabase.LookupValue ("WorkTeam", "WorkTeamId", "WorkTeamName = '" + workTeamName + "'"));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workTeamId;

        }


        public List<Core.Work.WorkTeamMembership> WorkTeamMembershipsForSession () {

            List<Core.Work.WorkTeamMembership> results = new List<Mercury.Server.Core.Work.WorkTeamMembership> ();

            String selectStatement = String.Empty;

            SetLastException (null);


            selectStatement = "SELECT * FROM WorkTeamMembership ";
            
            selectStatement += "  WHERE SecurityAuthorityId = " + session.SecurityAuthorityId.ToString ();
            
            selectStatement += "   AND UserAccountId = '" + session.UserAccountId.Replace ("'", "''") + "'";


            System.Data.DataTable workTeamTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in workTeamTable.Rows) {

                Mercury.Server.Core.Work.WorkTeamMembership workTeamMembership = new Mercury.Server.Core.Work.WorkTeamMembership (this);

                workTeamMembership.MapDataFields (currentRow);

                results.Add (workTeamMembership);

            }

            return results;

        }

        public List<Core.Work.WorkTeamMembership> WorkTeamMembershipsForUser (Int64 securityAuthorityId, String userAccountId) {

            List<Core.Work.WorkTeamMembership> results = new List<Mercury.Server.Core.Work.WorkTeamMembership> ();

            String selectStatement = String.Empty;

            SetLastException (null);


            selectStatement = "SELECT * FROM WorkTeamMembership ";

            selectStatement += "  WHERE SecurityAuthorityId = " + securityAuthorityId.ToString ();

            selectStatement += "   AND UserAccountId = '" + userAccountId.Replace ("'", "''") + "'";


            System.Data.DataTable workTeamTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in workTeamTable.Rows) {

                Mercury.Server.Core.Work.WorkTeamMembership workTeamMembership = new Mercury.Server.Core.Work.WorkTeamMembership (this);

                workTeamMembership.MapDataFields (currentRow);

                results.Add (workTeamMembership);

            }

            return results;

        }

        public List<Core.Work.WorkTeamMembership> WorkTeamMembershipsForUserByEnvironment (Int64 securityAuthorityId, String userAccountId, String environmentName) {

            List<Core.Work.WorkTeamMembership> results = new List<Mercury.Server.Core.Work.WorkTeamMembership> ();

            String selectStatement = String.Empty;


            SetLastException (null);


            selectStatement = "SELECT * FROM WorkTeamMembership ";

            selectStatement += "  WHERE SecurityAuthorityId = " + securityAuthorityId.ToString ();

            selectStatement += "   AND UserAccountId = '" + userAccountId.Replace ("'", "''") + "'";


            System.Data.DataTable workTeamTable = EnvironmentDatabaseByName (environmentName).SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in workTeamTable.Rows) {

                Mercury.Server.Core.Work.WorkTeamMembership workTeamMembership = new Mercury.Server.Core.Work.WorkTeamMembership (this);

                workTeamMembership.MapDataFields (currentRow);

                results.Add (workTeamMembership);

            }

            return results;

        }

        #endregion 


        #region Work Queue Monitor

        public List<Core.Work.DataViews.WorkQueueSummary> WorkQueueMonitorSummary () {

            List<Core.Work.DataViews.WorkQueueSummary> summary = new List<Core.Work.DataViews.WorkQueueSummary> ();

            
            System.Data.DataTable summaryTable = EnvironmentDatabase.SelectDataTable ("EXEC dbo.WorkQueueMonitorSummary");

            foreach (System.Data.DataRow currentRow in summaryTable.Rows) {

                Int64 workQueueId = (Int64) currentRow ["WorkQueueId"];

                // VALIDATE USER HAS ANY PERMISSION TO WORK QUEUE

                if (Session.WorkQueuePermissions.Keys.Contains (workQueueId)) {

                    // VALIDATE SPECIFIC PERMISSION IS MANAGE PERMISSION

                    if (Session.WorkQueuePermissions[workQueueId] == Core.Work.Enumerations.WorkQueueTeamPermission.Manage) {

                        Core.Work.DataViews.WorkQueueSummary workQueueSummary = new Core.Work.DataViews.WorkQueueSummary ();

                        workQueueSummary.MapDataFields (currentRow);

                        summary.Add (workQueueSummary);

                    }

                }

            }

            return summary;

        }

        public Dictionary<String, Int64> WorkQueueMonitorAging (Int64 workQueueId) {

            Dictionary<String, Int64> aging = new Dictionary<String, Int64> ();

            System.Data.DataTable agingTable = EnvironmentDatabase.SelectDataTable ("EXEC dbo.WorkQueueMonitorAgingByWorkQueue " + workQueueId.ToString ());

            if (agingTable.Rows.Count == 1) {

                foreach (System.Data.DataColumn currentColumn in agingTable.Columns) {

                    aging.Add (currentColumn.ColumnName, Convert.ToInt64 (agingTable.Rows[0][currentColumn.ColumnName]));

                }
            
            }

            return aging;

        }

        public Dictionary<String, Int64> WorkQueueMonitorAgingAvailable (Int64 workQueueId) {

            Dictionary<String, Int64> aging = new Dictionary<String, Int64> ();

            System.Data.DataTable agingTable = EnvironmentDatabase.SelectDataTable ("EXEC dbo.WorkQueueMonitorAgingAvailableByWorkQueue " + workQueueId.ToString ());

            if (agingTable.Rows.Count == 1) {

                foreach (System.Data.DataColumn currentColumn in agingTable.Columns) {

                    aging.Add (currentColumn.ColumnName, Convert.ToInt64 (agingTable.Rows[0][currentColumn.ColumnName]));

                }

            }

            return aging;

        }

        #endregion 


        #region Session Work Queue Permissions

        public Boolean SessionWorkQueueHasManagePermission (Int64 workQueueId) {

            Boolean hasPermission = false;

            if (Session.WorkQueuePermissions.ContainsKey (workQueueId)) {

                hasPermission = (Session.WorkQueuePermissions[workQueueId] == Server.Core.Work.Enumerations.WorkQueueTeamPermission.Manage);

            }

            return hasPermission;

        }

        public Boolean SessionWorkQueueHasSelfAssignPermission (Int64 workQueueId) {

            Boolean hasPermission = false;

            if (Session.WorkQueuePermissions.ContainsKey (workQueueId)) {

                hasPermission = hasPermission || (Session.WorkQueuePermissions[workQueueId] == Server.Core.Work.Enumerations.WorkQueueTeamPermission.Manage);

                hasPermission = hasPermission || (Session.WorkQueuePermissions[workQueueId] == Server.Core.Work.Enumerations.WorkQueueTeamPermission.SelfAssign);

            }

            return hasPermission;

        }

        public Boolean SessionWorkQueueHasWorkPermission (Int64 workQueueId) {

            Boolean hasPermission = false;

            if (Session.WorkQueuePermissions.ContainsKey (workQueueId)) {

                hasPermission = hasPermission || (Session.WorkQueuePermissions[workQueueId] == Server.Core.Work.Enumerations.WorkQueueTeamPermission.Manage);

                hasPermission = hasPermission || (Session.WorkQueuePermissions[workQueueId] == Server.Core.Work.Enumerations.WorkQueueTeamPermission.SelfAssign);

                hasPermission = hasPermission || (Session.WorkQueuePermissions[workQueueId] == Server.Core.Work.Enumerations.WorkQueueTeamPermission.Work);

            }

            return hasPermission;

        }

        public Boolean SessionWorkQueueHasViewPermission (Int64 workQueueId) {

            Boolean hasPermission = false;

            if (Session.WorkQueuePermissions.ContainsKey (workQueueId)) {

                hasPermission = hasPermission || (Session.WorkQueuePermissions[workQueueId] == Server.Core.Work.Enumerations.WorkQueueTeamPermission.Manage);

                hasPermission = hasPermission || (Session.WorkQueuePermissions[workQueueId] == Server.Core.Work.Enumerations.WorkQueueTeamPermission.SelfAssign);

                hasPermission = hasPermission || (Session.WorkQueuePermissions[workQueueId] == Server.Core.Work.Enumerations.WorkQueueTeamPermission.Work);

                hasPermission = hasPermission || (Session.WorkQueuePermissions[workQueueId] == Server.Core.Work.Enumerations.WorkQueueTeamPermission.View);

            }

            return hasPermission;

        }

        #endregion 
        
        #endregion


        #region Data Explorer

        public List<Core.DataExplorer.DataExplorer> DataExplorersAvailable (Boolean useCaching = false) {

            String cacheKey = "Application." + session.EnvironmentId + ".DataExplorer.Available";

            Dictionary<Int64, String> objectDictionary = new Dictionary<Int64, String> ();

            List<Core.DataExplorer.DataExplorer> availableItems = new List<Core.DataExplorer.DataExplorer> ();

            ClearLastException ();


            try {

                if (!useCaching) { CacheManager.RemoveObject (cacheKey); }

                availableItems = (List<Core.DataExplorer.DataExplorer>)CacheManager.GetObject (cacheKey);

                if (availableItems == null) {

                    availableItems = new List<Core.DataExplorer.DataExplorer> ();

                    String selectStatement = "SELECT * FROM dbo.DataExplorer WHERE DataExplorerId <> 0 ORDER BY DataExplorerName";

                    System.Data.DataTable availableTable = EnvironmentDatabase.SelectDataTable (selectStatement);

                    foreach (System.Data.DataRow currentRow in availableTable.Rows) {

                        Core.DataExplorer.DataExplorer item = new Core.DataExplorer.DataExplorer (this);

                        item.MapDataFields (currentRow);

                        availableItems.Add (item);


                        // CACHE EACH INDIVIDUAL ITEM LOADED FROM SERVER (BY ID AND NAME)

                        CacheManager.CacheObject ("Application." + session.EnvironmentId + "." + item.ObjectType + "." + item.Id.ToString (), item, CacheExpirationData);

                        CacheManager.CacheObject ("Application." + session.EnvironmentId + "." + item.ObjectType + "." + item.Name, item, CacheExpirationData);

                        objectDictionary.Add (item.Id, item.Name);

                    }

                    if (availableItems.Count > 0) {

                        // CACHE THE AVAILABILITY LIST

                        CacheManager.CacheObject (cacheKey, availableItems, CacheExpirationData);


                        // CACHE THE DICTIONARY THAT WAS CREATED

                        CacheManager.CacheObject ("Application." + session.EnvironmentId + "." + availableItems[0].ObjectType + ".Dictionary", objectDictionary, CacheExpirationData);

                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return availableItems;

        }

        public Core.DataExplorer.DataExplorer DataExplorerGet (Int64 dataExplorerId, Boolean useCaching = false) {

            if (dataExplorerId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".DataExplorer." + dataExplorerId.ToString ();

            Core.DataExplorer.DataExplorer dataExplorer = null;

            ClearLastException ();


            try {

                dataExplorer = (Core.DataExplorer.DataExplorer)CacheManager.GetObject (cacheKey);

                if (!useCaching) { dataExplorer = null; }

                if (dataExplorer == null) {

                    dataExplorer = new Mercury.Server.Core.DataExplorer.DataExplorer (this, dataExplorerId);

                    CacheManager.CacheObject (cacheKey, dataExplorer, CacheExpirationData);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return dataExplorer;

        }

        public List<Int64> DataExplorerNodeResultsGet (Guid nodeInstanceId, Int32 initialRow, Int32 count) {

            List<Int64> results = new List<Int64> ();


            ClearLastException ();


            try {

                System.Data.DataTable resultTable = EnvironmentDatabase.SelectDataTable ("EXEC DataExplorerNodeResult_SelectByPage '" + nodeInstanceId.ToString () + "', " + initialRow + ", " + count);

                foreach (System.Data.DataRow currentRow in resultTable.Rows) {

                    results.Add ((Int64)currentRow["Id"]);

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return results;

        }

        public List<Core.Member.Member> DataExplorerNodeResultsGetForMember (Guid nodeInstanceId, Int32 initialRow, Int32 count) {

            List<Core.Member.Member> results = new List<Core.Member.Member> ();


            ClearLastException ();


            try {

                System.Data.DataTable resultTable = EnvironmentDatabase.SelectDataTable ("EXEC dal.DataExplorerNodeResult_SelectByPageForMember '" + nodeInstanceId.ToString () + "', " + initialRow + ", " + count);

                foreach (System.Data.DataRow currentRow in resultTable.Rows) {

                    Core.Member.Member member = new Core.Member.Member (this);

                    member.MapDataFields (currentRow);

                    results.Add (member);

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return results;

        }

        public List<Core.Entity.EntityAddress> DataExplorerNodeResultsGetForMemberEntityCurrentAddress (Guid nodeInstanceId, Int32 initialRow, Int32 count) {

            List<Core.Entity.EntityAddress> results = new List<Core.Entity.EntityAddress> ();


            ClearLastException ();


            try {

                System.Data.DataTable resultTable = EnvironmentDatabase.SelectDataTable ("EXEC dal.DataExplorerNodeResult_SelectByPageForMemberEntityCurrentAddress '" + nodeInstanceId.ToString () + "', " + initialRow + ", " + count);

                foreach (System.Data.DataRow currentRow in resultTable.Rows) {

                    Core.Entity.EntityAddress entityAddress = new Core.Entity.EntityAddress (this);

                    entityAddress.MapDataFields (currentRow);

                    results.Add (entityAddress);

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return results;

        }

        public List<Core.Entity.EntityContactInformation> DataExplorerNodeResultsGetForMemberEntityCurrentContactInformation (Guid nodeInstanceId, Int32 initialRow, Int32 count) {

            List<Core.Entity.EntityContactInformation> results = new List<Core.Entity.EntityContactInformation> ();


            ClearLastException ();


            try {

                System.Data.DataTable resultTable = EnvironmentDatabase.SelectDataTable ("EXEC dal.DataExplorerNodeResult_SelectByPageForMemberEntityCurrentContactInformation '" + nodeInstanceId.ToString () + "', " + initialRow + ", " + count);

                foreach (System.Data.DataRow currentRow in resultTable.Rows) {

                    Core.Entity.EntityContactInformation entityAddress = new Core.Entity.EntityContactInformation (this);

                    entityAddress.MapDataFields (currentRow);

                    results.Add (entityAddress);

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return results;

        }

        public List<Core.Member.MemberEnrollment> DataExplorerNodeResultsGetForMemberCurrentEnrollment (Guid nodeInstanceId, Int32 initialRow, Int32 count) {

            List<Core.Member.MemberEnrollment> results = new List<Core.Member.MemberEnrollment> ();


            ClearLastException ();


            try {

                System.Data.DataTable resultTable = EnvironmentDatabase.SelectDataTable ("EXEC dal.DataExplorerNodeResult_SelectByPageForMemberCurrentEnrollment '" + nodeInstanceId.ToString () + "', " + initialRow + ", " + count);

                foreach (System.Data.DataRow currentRow in resultTable.Rows) {

                    Core.Member.MemberEnrollment entityEnrollment = new Core.Member.MemberEnrollment (this);

                    entityEnrollment.MapDataFields (currentRow);

                    results.Add (entityEnrollment);

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return results;

        }

        public List<Core.Member.MemberEnrollmentCoverage> DataExplorerNodeResultsGetForMemberCurrentEnrollmentCoverage (Guid nodeInstanceId, Int32 initialRow, Int32 count) {

            List<Core.Member.MemberEnrollmentCoverage> results = new List<Core.Member.MemberEnrollmentCoverage> ();


            ClearLastException ();


            try {

                System.Data.DataTable resultTable = EnvironmentDatabase.SelectDataTable ("EXEC dal.DataExplorerNodeResult_SelectByPageForMemberCurrentEnrollmentCoverage '" + nodeInstanceId.ToString () + "', " + initialRow + ", " + count);

                foreach (System.Data.DataRow currentRow in resultTable.Rows) {

                    Core.Member.MemberEnrollmentCoverage entityEnrollmentCoverage = new Core.Member.MemberEnrollmentCoverage (this);

                    entityEnrollmentCoverage.MapDataFields (currentRow);

                    results.Add (entityEnrollmentCoverage);

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return results;

        }

        public List<Core.Member.MemberEnrollmentPcp> DataExplorerNodeResultsGetForMemberCurrentEnrollmentPcp (Guid nodeInstanceId, Int32 initialRow, Int32 count) {

            List<Core.Member.MemberEnrollmentPcp> results = new List<Core.Member.MemberEnrollmentPcp> ();


            ClearLastException ();


            try {

                System.Data.DataTable resultTable = EnvironmentDatabase.SelectDataTable ("EXEC dal.DataExplorerNodeResult_SelectByPageForMemberCurrentEnrollmentPcp '" + nodeInstanceId.ToString () + "', " + initialRow + ", " + count);

                foreach (System.Data.DataRow currentRow in resultTable.Rows) {

                    Core.Member.MemberEnrollmentPcp entityEnrollmentPcp = new Core.Member.MemberEnrollmentPcp (this);

                    entityEnrollmentPcp.MapDataFields (currentRow);

                    results.Add (entityEnrollmentPcp);

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return results;

        }

        #endregion 


        #region Faxing

        public List<Faxing.FaxServer> FaxServersAvailable () {

            List<Faxing.FaxServer> results = new List<Faxing.FaxServer> ();

            StringBuilder selectStatement = new StringBuilder ();

            ClearLastException ();


            selectStatement.Append ("SELECT * FROM dbo.FaxServer ORDER BY FaxServerName");


            System.Data.DataTable faxServerTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in faxServerTable.Rows) {

                Faxing.FaxServer faxServer = new Mercury.Server.Faxing.FaxServer (this);

                faxServer.MapDataFields (currentRow);

                results.Add (faxServer);

            }

            return results;

        }

        public List<Faxing.FaxServer> FaxServersAvailableByEnvironment (Int64 environmentId) {

            List<Faxing.FaxServer> results = new List<Faxing.FaxServer> ();

            StringBuilder selectStatement = new StringBuilder ();

            ClearLastException ();


            selectStatement.Append ("SELECT * FROM dbo.FaxServer ORDER BY FaxServerName");


            System.Data.DataTable faxServerTable = EnvironmentDatabaseById (environmentId).SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in faxServerTable.Rows) {

                Faxing.FaxServer faxServer = new Mercury.Server.Faxing.FaxServer (this);

                faxServer.MapDataFields (currentRow);

                results.Add (faxServer);

            }

            return results;

        }


        public Int64 FaxServerGetIdByName (String faxServerName) { return CoreObjectGetIdByName ("FaxServer", faxServerName); }

        public String FaxServerGetNameById (Int64 faxServerId) { return CoreObjectGetNameById ("FaxServer", faxServerId); }


        public Faxing.FaxServer FaxServerGet (Int64 faxServerId) {

            if (faxServerId == 0) { return null; }


            Faxing.FaxServer faxServer = null;

            ClearLastException ();

            try {

                faxServer = new Mercury.Server.Faxing.FaxServer (this, faxServerId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return faxServer;

        }

        public Faxing.FaxServer FaxServerGet (Int64 faxServerId, Int64 environmentId) {

            if (faxServerId == 0) { return null; }


            Faxing.FaxServer faxServer = null;

            ClearLastException ();

            try {

                faxServer = new Mercury.Server.Faxing.FaxServer (this, faxServerId, environmentId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return faxServer;

        }


        public Faxing.FaxServer FaxServerGet (String faxServerName) {

            Int64 faxServerId = CoreObjectGetIdByName ("FaxServer", faxServerName);

            Faxing.FaxServer faxServer = FaxServerGet (faxServerId);

            return faxServer;

        }

        public Faxing.FaxServer FaxServerGet (String faxServerName, Int64 environmentId) {

            if (String.IsNullOrWhiteSpace (faxServerName)) { return null; }


            Int64 faxServerId = 0;

            ClearLastException ();


            try {

                faxServerId = Convert.ToInt64 (EnvironmentDatabaseById (environmentId).LookupValue (CoreObjectTableName ("FaxServer"), "FaxServerId", "FaxServerName = '" + faxServerName.Replace ("'", "''") + "'", 0));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            Faxing.FaxServer faxServer = FaxServerGet (faxServerId);

            return faxServer;

        }

        #endregion


        #region Reporting

        public List<Reporting.ReportingServer> ReportingServersAvailable () {

            List<Reporting.ReportingServer> results = new List<Reporting.ReportingServer> ();

            StringBuilder selectStatement = new StringBuilder ();

            ClearLastException ();


            selectStatement.Append ("SELECT * FROM dbo.ReportingServer ORDER BY ReportingServerName");


            System.Data.DataTable reportingServerTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in reportingServerTable.Rows) {

                Reporting.ReportingServer reportingServer = new Mercury.Server.Reporting.ReportingServer (this);

                reportingServer.MapDataFields (currentRow);

                results.Add (reportingServer);

            }

            return results;

        }

        public List<Reporting.ReportingServer> ReportingServersAvailableByEnvironment (Int64 environmentId) {

            List<Reporting.ReportingServer> results = new List<Reporting.ReportingServer> ();

            StringBuilder selectStatement = new StringBuilder ();

            ClearLastException ();


            selectStatement.Append ("SELECT * FROM dbo.ReportingServer ORDER BY ReportingServerName");


            System.Data.DataTable reportingServerTable = EnvironmentDatabaseById (environmentId).SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in reportingServerTable.Rows) {

                Reporting.ReportingServer reportingServer = new Mercury.Server.Reporting.ReportingServer (this);

                reportingServer.MapDataFields (currentRow);

                results.Add (reportingServer);

            }

            return results;

        }


        public Int64 ReportingServerGetIdByName (String reportingServerName) { return CoreObjectGetIdByName ("ReportingServer", reportingServerName); }

        public String ReportingServerGetNameById (Int64 reportingServerId) { return CoreObjectGetNameById ("ReportingServer", reportingServerId); }


        public Reporting.ReportingServer ReportingServerGet (Int64 reportingServerId) {

            if (reportingServerId == 0) { return null; }


            Reporting.ReportingServer reportingServer = null;

            ClearLastException ();

            try {

                reportingServer = new Mercury.Server.Reporting.ReportingServer (this, reportingServerId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return reportingServer;

        }

        public Reporting.ReportingServer ReportingServerGet (Int64 reportingServerId, Int64 environmentId) {

            if (reportingServerId == 0) { return null; }


            Reporting.ReportingServer reportingServer = null;

            ClearLastException ();

            try {

                reportingServer = new Mercury.Server.Reporting.ReportingServer (this, reportingServerId, environmentId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return reportingServer;

        }


        public Reporting.ReportingServer ReportingServerGet (String reportingServerName) {

            Int64 reportingServerId = CoreObjectGetIdByName ("ReportingServer", reportingServerName);

            Reporting.ReportingServer reportingServer = ReportingServerGet (reportingServerId);

            return reportingServer;

        }

        public Reporting.ReportingServer ReportingServerGet (String reportingServerName, Int64 environmentId) {

            if (String.IsNullOrWhiteSpace (reportingServerName)) { return null; }


            Int64 reportingServerId  = 0;

            ClearLastException ();


            try {

                reportingServerId = Convert.ToInt64 (EnvironmentDatabaseById (environmentId).LookupValue (CoreObjectTableName ("ReportingServer"), "ReportingServerId", "ReportingServerName = '" + reportingServerName.Replace ("'", "''") + "'", 0));

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            Reporting.ReportingServer reportingServer = ReportingServerGet (reportingServerId);

            return reportingServer;

        }

        #endregion


        #region Printing

        public List<Printing.Printer> PrintersAvailable () {

            List<Printing.Printer> results = new List<Printing.Printer> ();

            StringBuilder selectStatement = new StringBuilder ();

            ClearLastException ();


            selectStatement.Append ("SELECT * FROM dbo.Printer ORDER BY PrinterName");


            System.Data.DataTable reportingServerTable = EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            foreach (System.Data.DataRow currentRow in reportingServerTable.Rows) {

                Printing.Printer reportingServer = new Mercury.Server.Printing.Printer (this);

                reportingServer.MapDataFields (currentRow);

                results.Add (reportingServer);

            }

            return results;

        }

        public SortedDictionary<String, String> PrintQueuesAvailable (String printServerName) {

            SortedDictionary<String, String> printQueuesAvailable = new SortedDictionary<String, String> ();


            try {

                System.Printing.PrintServer printServer = new System.Printing.PrintServer (printServerName);

                foreach (System.Printing.PrintQueue currentPrintQueue in printServer.GetPrintQueues ()) {

                    printQueuesAvailable.Add (currentPrintQueue.Name, currentPrintQueue.Description + ", " + currentPrintQueue.Comment);

                }

            }

            catch { /* DO NOTHING */ }


            return printQueuesAvailable;

        }

        public Printing.PrinterCapabilities PrinterCapabilities (String printServerName, String printQueueName) {

            Printing.PrinterCapabilities capabilities = null;


            try {

                System.Printing.PrintServer printServer = new System.Printing.PrintServer (printServerName);

                if (printServer != null) {

                    System.Printing.PrintQueue printQueue = printServer.GetPrintQueue (printQueueName);

                    if (printQueue != null) {

                        capabilities = new Printing.PrinterCapabilities (printQueue);

                    }

                }
                
            }

            catch { /* DO NOTHING */ }


            return capabilities;

        }


        public Int64 PrinterGetIdByName (String printerName) { return CoreObjectGetIdByName ("Printer", printerName); }

        public String PrinterGetNameById (Int64 printerId) { return CoreObjectGetNameById ("Printer", printerId); }


        public Printing.Printer PrinterGet (Int64 printerId) {

            if (printerId == 0) { return null; }


            Printing.Printer printer = null;

            ClearLastException ();

            try {

                printer = new Mercury.Server.Printing.Printer (this, printerId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return printer;

        }

        public Printing.Printer PrinterGet (String printerName) {

            Int64 printerId = CoreObjectGetIdByName ("Printer", printerName);

            Printing.Printer printer = PrinterGet (printerId);

            return printer;

        }

        #endregion


        #region Search Methods

        public List<Core.Search.SearchResultMember> SearchMemberByName (String memberName, DateTime? birthDate) {

            List<Core.Search.SearchResultMember> results = new List<Core.Search.SearchResultMember> ();

            Int32 resultsFound = 0;


            if (memberName.Length < 3) { throw new ApplicationException ("Not enough characters in the name."); }

            //if ((!HasEnvironmentPermission (EnvironmentPermissions.SearchMemberOptionalBirthDate)) && (birthDate == null)) {

            //    throw new ApplicationException ("Birth date is a required field.");

            //}


            String sqlStatement = "EXEC dal.SearchMemberByName '" + (memberName.Trim ()).Replace ("'", "''") + "', ";

            if (birthDate == null) { sqlStatement = sqlStatement + "NULL, "; } else { sqlStatement = sqlStatement + "'" + birthDate.ToString () + "', "; }


            System.Data.DataTable resultsCount = EnvironmentDatabase.SelectDataTable (sqlStatement + "0");

            resultsFound = (Int32)resultsCount.Rows[0][0];


            if (HasEnvironmentPermission (EnvironmentPermissions.SearchResultsExtraLarge)) {

                if (resultsFound > 250) { throw new ApplicationException ("Too many members returned. Please narrow search criteria."); }

            }

            else if (HasEnvironmentPermission (EnvironmentPermissions.SearchResultsLarge)) {

                if (resultsFound > 100) { throw new ApplicationException ("Too many members returned. Please narrow search criteria."); }

            }

            else {

                if (resultsFound > 25) { throw new ApplicationException ("Too many members returned. Please narrow search criteria."); }

            }


            System.Data.DataTable resultsTable = EnvironmentDatabase.SelectDataTable (sqlStatement + "1");

            foreach (System.Data.DataRow currentRow in resultsTable.Rows) {

                Mercury.Server.Core.Search.SearchResultMember memberResult = new Mercury.Server.Core.Search.SearchResultMember ();

                memberResult.MapDataFields (currentRow);

                results.Add (memberResult);

            }

            return results;

        }

        public List<Core.Search.SearchResultMember> SearchMemberById (String id) {

            List<Core.Search.SearchResultMember> results = new List<Core.Search.SearchResultMember> ();

            Int32 resultsFound = 0;


            String sqlStatement = "EXEC dal.SearchMemberById'" + (id.Trim ()).Replace ("'", "") + "', ";


            System.Data.DataTable resultsCount = EnvironmentDatabase.SelectDataTable (sqlStatement + "0");

            resultsFound = (Int32)resultsCount.Rows[0][0];


            if (HasEnvironmentPermission (EnvironmentPermissions.SearchResultsExtraLarge)) {

                if (resultsFound > 250) { throw new ApplicationException ("Too many members returned. Please narrow search criteria."); }

            }

            else if (HasEnvironmentPermission (EnvironmentPermissions.SearchResultsLarge)) {

                if (resultsFound > 100) { throw new ApplicationException ("Too many members returned. Please narrow search criteria."); }

            }

            else {

                if (resultsFound > 25) { throw new ApplicationException ("Too many members returned. Please narrow search criteria."); }

            }


            System.Data.DataTable resultsTable = EnvironmentDatabase.SelectDataTable (sqlStatement + "1");

            foreach (System.Data.DataRow currentRow in resultsTable.Rows) {

                Mercury.Server.Core.Search.SearchResultMember memberResult = new Mercury.Server.Core.Search.SearchResultMember ();

                memberResult.MapDataFields (currentRow);

                results.Add (memberResult);

            }

            return results;

        }

        public List<Mercury.Server.Core.Search.SearchResultProvider> SearchProviderByName (String providerName) {

            List<Mercury.Server.Core.Search.SearchResultProvider> results = new List<Mercury.Server.Core.Search.SearchResultProvider> ();

            Int32 resultsFound = 0;


            if (providerName.Length < 3) { throw new ApplicationException ("Not enough characters in the name."); }


            String sqlStatement = "EXEC dal.SearchProviderByName '" + (providerName.Trim ()).Replace ("'", "''") + "', ";


            System.Data.DataTable resultsCount = EnvironmentDatabase.SelectDataTable (sqlStatement + "0");

            resultsFound = (Int32)resultsCount.Rows[0][0];


            if (HasEnvironmentPermission (EnvironmentPermissions.SearchResultsExtraLarge)) {

                if (resultsFound > 250) { throw new ApplicationException ("Too many providers returned. Please narrow search criteria."); }

            }

            else if (HasEnvironmentPermission (EnvironmentPermissions.SearchResultsLarge)) {

                if (resultsFound > 100) { throw new ApplicationException ("Too many providers returned. Please narrow search criteria."); }

            }

            else {

                if (resultsFound > 25) { throw new ApplicationException ("Too many providers returned. Please narrow search criteria."); }

            }


            System.Data.DataTable resultsTable = EnvironmentDatabase.SelectDataTable (sqlStatement + "1");

            foreach (System.Data.DataRow currentRow in resultsTable.Rows) {

                Mercury.Server.Core.Search.SearchResultProvider providerResult = new Mercury.Server.Core.Search.SearchResultProvider ();

                providerResult.MapDataFields (currentRow);

                results.Add (providerResult);

            }

            return results;

        }

        #endregion


        #region Configuration - Xml Serialization

        public List<ImportExport.Result> XmlConfigurationImport (System.Xml.XmlDocument xmlConfiguration) {

            List<ImportExport.Result> response = new List<ImportExport.Result> ();

            List<ImportExport.Result> objectResponse = null;

            foreach (System.Xml.XmlNode currentNode in xmlConfiguration.ChildNodes) {

                Core.CoreObject coreObject = null;

                #region Create Core Object Reference

                switch (currentNode.Name) {

                    case "xml": break; /* IGNORE XML DECLARATION */

                    case "NoteType": coreObject = new Core.Reference.NoteType (this); break;

                    case "ContactRegarding": coreObject = new Core.Reference.ContactRegarding (this); break;

                    case "Correspondence": coreObject = new Core.Reference.Correspondence (this); break;


                    case "Form": coreObject = new Core.Forms.Form (this); break;


                    case "Workflow": coreObject = new Core.Work.Workflow (this); break;

                    case "WorkTeam": coreObject = new Core.Work.WorkTeam (this); break;

                    case "WorkQueue": coreObject = new Core.Work.WorkQueue (this); break;

                    case "WorkQueueView": coreObject = new Core.Work.WorkQueueView (this); break;

                    case "WorkOutcome": coreObject = new Core.Work.WorkOutcome (this); break;

                    case "RoutingRule": coreObject = new Core.Work.RoutingRule (this); break;


                    case "ServiceSingleton": coreObject = new Server.Core.MedicalServices.ServiceSingleton (this); break;

                    case "ServiceSet": coreObject = new Server.Core.MedicalServices.ServiceSet (this); break;

                        //switch ((Core.MedicalServices.Enumerations.MedicalServiceType)Convert.ToInt32 (currentNode.Attributes["ServiceType"].InnerText)) {

                        //    case Core.MedicalServices.Enumerations.MedicalServiceType.Singleton: 

                        //    case Core.MedicalServices.Enumerations.MedicalServiceType.Set: 

                        //}

                        //break;

                    case "Metric": coreObject = new Core.Metrics.Metric (this); break;

                    case "AuthorizedService": coreObject = new Core.AuthorizedServices.AuthorizedService (this); break;


                    case "PopulationType": coreObject = new Core.Population.PopulationType (this); break;

                    case "Population": coreObject = new Core.Population.Population (this); break;


                    case "FaxServer": coreObject = new Faxing.FaxServer (this); break;

                    case "ReportingServer": coreObject = new Reporting.ReportingServer (this); break;


                    case "CareMeasureScale": coreObject = new Core.Individual.CareMeasureScale (this); break;

                    case "CareMeasure": coreObject = new Core.Individual.CareMeasure (this); break;

                    case "CareOutcome": coreObject = new Core.Individual.CareOutcome (this); break;

                    case "CareIntervention": coreObject = new Core.Individual.CareIntervention (this); break;

                    case "CarePlan": coreObject = new Core.Individual.CarePlan (this); break;

                    case "ProblemStatement": coreObject = new Core.Individual.ProblemStatement (this); break;

                }

                #endregion 

                #region Process Xml Import

                if (coreObject != null) {

                    objectResponse = coreObject.XmlImport (currentNode);

                    Dictionary<String, String> validationMessages = coreObject.Validate ();

                    if (validationMessages.Count == 0) {

                        Boolean saveSuccess = coreObject.Save ();

                        if (saveSuccess) { objectResponse.Add (new ImportExport.Result (coreObject.ObjectType, coreObject.Name, coreObject.Id)); }
                        
                        else { objectResponse.Add (new ImportExport.Result (coreObject.ObjectType, coreObject.Name, LastException)); }

                    }

                    else {

                        foreach (String currentMessageKey in validationMessages.Keys) {

                            String validationMessage = "Validation Error [" + currentMessageKey + "]: " + validationMessages[currentMessageKey];

                            objectResponse.Add (new ImportExport.Result (coreObject.ObjectType, coreObject.Name, new ApplicationException (validationMessage)));

                        }

                    }

                }

                #endregion 

                if (objectResponse != null) { response.AddRange (objectResponse); }

            }

            return response;

        }

        #endregion


        #region Object Factory

        public Mercury.Server.Data.FilterDescriptor CreateFilterDescriptor (String forPropertyPath, Mercury.Server.Data.Enumerations.DataFilterOperator forFilterOperator, Object forFilterValue) {

            Mercury.Server.Data.FilterDescriptor filter = new Mercury.Server.Data.FilterDescriptor ();

            filter.PropertyPath = forPropertyPath;

            filter.Operator = forFilterOperator;

            filter.Parameter = new Data.Parameter ();

            filter.Parameter.Name = "Value";

            filter.Parameter.Value = forFilterValue;

            return filter;

        }

        public Mercury.Server.Data.SortDescriptor CreateSortDescription (String forFieldName, Mercury.Server.Data.Enumerations.DataSortDirection forSortDireciton) {

            Mercury.Server.Data.SortDescriptor sort = new Data.SortDescriptor ();

            sort.FieldName = forFieldName;

            sort.SortDirection = forSortDireciton;

            return sort;

        }


        public System.Windows.Data.Binding PropertyDataBinding (String propertyName, Object source, System.Windows.Data.BindingMode bindingMode) {

            System.Windows.Data.Binding dataBinding = new System.Windows.Data.Binding (propertyName);

            dataBinding.Source = source;

            dataBinding.Mode = bindingMode;

            return dataBinding;

        }

        public System.Windows.Data.Binding PropertyDataBinding (String propertyName, Object source, System.Windows.Data.BindingMode bindingMode, System.Windows.Data.IValueConverter converter) {

            System.Windows.Data.Binding dataBinding = PropertyDataBinding (propertyName, source, bindingMode);

            dataBinding.Converter = converter;

            return dataBinding;

        }

        #endregion 

    }

}
