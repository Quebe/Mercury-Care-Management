using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client {

    /// <summary>
    /// Encapsulates all Mercury Application operations between the client and the 
    /// server (through Web Services). Is responsible for making and maintaining Web Service
    /// References.
    /// </summary>     
    /// <remarks>
    /// <para>The Application class provides a rich set of methods and properties for working with 
    /// Mercury through the Web Services. It allows you to perform synchronous web requests with the 
    /// server. 
    /// </para>
    /// <para>
    /// Most methods support caching the results locally for a preset amount of time so that the request
    /// is not repeated. This is important in ASP.NET applications that might query the database 
    /// on postback that was already queried when the page was first created.
    /// </para>
    /// </remarks>
    [Serializable]
    public class Application {

        #region Private Properties

        private Session session = null;

        private Server.Enterprise.AuthenticationResponse authenticationResponse = new Server.Enterprise.AuthenticationResponse ();


        private CacheManager cacheManager = new CacheManager ();

        private TimeSpan? cacheExpirationData = null;

        private TimeSpan? cacheExpirationReference = null;


        private TimeSpan serviceTimeout = TimeSpan.MaxValue;

        private Enumerations.ServiceBindingType serviceBindingType = Enumerations.ServiceBindingType.BasicHttpBinding;

        private String serviceHostAddress = String.Empty;

        private String serviceHostPort = String.Empty;

        private Boolean impersonateUser = true;


        [NonSerialized]
        private Server.Enterprise.SecurityClient securityClient = null;

        [NonSerialized]
        private Server.Application.ApplicationClient applicationClient = null;

        [NonSerialized]
        private Int32 serviceAttempts = 0; // SERVICE RETRY ACCOUNT (FOR FAULTS)


        private String versionClient = String.Empty;

        private String versionServer = String.Empty;

        private Boolean? useFormControlEventHandlerCaching = null;

        [NonSerialized]
        private Exception lastException = null;

        [NonSerialized]
        private System.Diagnostics.TraceSwitch traceSwitchGeneral = new System.Diagnostics.TraceSwitch ("General", "General Diagnostics Messages");

        [NonSerialized]
        private System.Diagnostics.TraceSwitch traceSwitchSecurity = new System.Diagnostics.TraceSwitch ("Security", "Messages related to the Enterprise and Security");

        [NonSerialized]
        private System.Diagnostics.TraceSwitch traceSwitchService = new System.Diagnostics.TraceSwitch ("Service", "Messages related to Web Service Calls");

        [NonSerialized]
        private System.Diagnostics.TraceSwitch traceSwitchWorkflow = new System.Diagnostics.TraceSwitch ("Workflow", "Workflow Process Messages");

        #endregion


        #region Public Properties

        public Mercury.Client.Session Session { get { return session; } }

        public Server.Enterprise.AuthenticationResponse AuthenticationResponse { get { return authenticationResponse; } }


        public CacheManager CacheManager { get { return cacheManager; } set { cacheManager = value; } }

        public TimeSpan CacheExpirationData {

            get {

                if (cacheExpirationData.HasValue) { return cacheExpirationData.Value; }

                cacheExpirationData = new TimeSpan (0, 0, 0);

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

                cacheExpirationReference = new TimeSpan (0, 0, 0);

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

        public String ServiceHostAddress { get { return serviceHostAddress; } }

        public String ServiceHostPort { get { return serviceHostPort; } }


        public String VersionClient {

            get {

                if (String.IsNullOrEmpty (versionClient)) {

                    versionClient = System.Reflection.Assembly.GetAssembly (this.GetType ()).GetName ().Version.ToString ();

                }

                return versionClient;

            }

        }

        public String VersionServer {

            get {

                if (String.IsNullOrEmpty (versionServer)) {

                    ClearLastException ();

                    try {

                        // versionServer = ApplicationClient.VersionServer (session.Token);

                    }

                    catch (Exception applicationException) {

                        SetLastException (applicationException);

                    }

                }

                return versionServer;

            }

        }

        public Boolean UseFormControlEventHandlerCaching {

            get {

                if (useFormControlEventHandlerCaching.HasValue) { return useFormControlEventHandlerCaching.Value; }

                useFormControlEventHandlerCaching = true;

                try {

                    String configurationValueString = (((String)System.Configuration.ConfigurationManager.AppSettings.GetValues ("UseFormControlEventHandlerCaching")[0]).ToLower (new System.Globalization.CultureInfo ("")));

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


        public Exception LastException { get { return lastException; } }

        public String LastExceptionMessage { get { return ((lastException != null) ? lastException.Message : String.Empty); } }

        public System.Diagnostics.TraceSwitch TraceSwitchGeneral { 
            
            get { 
                
                if (traceSwitchGeneral == null) { traceSwitchGeneral = new System.Diagnostics.TraceSwitch ("General", "General Diagnostics Messages"); }

                return traceSwitchGeneral; 
            
            } 
        
        }

        public System.Diagnostics.TraceSwitch TraceSwitchSecurity { 
            
            get { 

                if (traceSwitchSecurity == null) { traceSwitchSecurity = new System.Diagnostics.TraceSwitch ("Security", "Messages related to the Enterprise and Security"); }

                return traceSwitchSecurity; 
            
            } 
        
        }

        public System.Diagnostics.TraceSwitch TraceSwitchService { 
            
            get { 

                if (traceSwitchService == null) { traceSwitchService = new System.Diagnostics.TraceSwitch ("Service", "Messages related to Web Service Calls"); }

                return traceSwitchService; 
            
            } 
        
        }

        public System.Diagnostics.TraceSwitch TraceSwitchWorkflow { 
            
            get {

                if (traceSwitchWorkflow == null) { traceSwitchWorkflow = new System.Diagnostics.TraceSwitch ("Workflow", "Workflow Process Messages"); }

                return traceSwitchWorkflow; 
            
            } 
        
        }

        #endregion


        #region Constructor

        public Application () {

            InitializeBinding ();

            InitializeServices ();

            authenticationResponse.IsAuthenticated = false;

            return;

        }

        public Application (Enumerations.ServiceBindingType bindingType, String hostAddress, Int32 hostPort, Boolean impersonate) {

            serviceBindingType = bindingType;

            serviceHostAddress = hostAddress;

            serviceHostPort = hostPort.ToString ();

            impersonateUser = impersonate;


            InitializeServices ();

            authenticationResponse.IsAuthenticated = false;

            return;

        }

        #endregion


        #region Support Functions

        public void ClearLastException () {

            lastException = null;

            return;

        }

        public void SetLastException (Exception exception) {

            lastException = exception;

            if (lastException != null) {

                System.Diagnostics.Trace.WriteLineIf (TraceSwitchGeneral.TraceError, "[" + DateTime.Now.ToString () + "] Client.Application [" + lastException.Source + "] " + lastException.Message);

                if (lastException.InnerException != null) {

                    System.Diagnostics.Trace.WriteLineIf (TraceSwitchGeneral.TraceError, "Client.Application [" + lastException.InnerException.Source + "] " + lastException.InnerException.Message);

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

        public void SetLastException (Server.Enterprise.ServiceException serviceException) {

            Exception innerException = (serviceException.InnerException != null) ? new ApplicationException (serviceException.InnerException.Message) : null;

            Exception applicationException = new ApplicationException (serviceException.Message, innerException);

            applicationException.Source = serviceException.Source;

            SetLastException (applicationException);

            return;

        }

        public void SetLastException (Server.Application.ServiceException serviceException) {

            Exception innerException = (serviceException.InnerException != null) ? new ApplicationException (serviceException.InnerException.Message) : null;

            Exception applicationException = new ApplicationException (serviceException.Message, innerException);

            applicationException.Source = serviceException.Source;

            SetLastException (applicationException);

            return;

        }

        public void SetLastExceptionQuite (Server.Application.ServiceException serviceException) {

            Exception innerException = (serviceException.InnerException != null) ? new ApplicationException (serviceException.InnerException.Message) : null;

            Exception applicationException = new ApplicationException (serviceException.Message, innerException);

            applicationException.Source = serviceException.Source;

            SetLastExceptionQuite (applicationException);

            return;

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


        #region Initialization

        private void InitializeBinding () {

            try {

                switch (((String)System.Configuration.ConfigurationManager.AppSettings.GetValues ("Mercury.Server.ServiceHost.BindingType")[0]).ToLower (new System.Globalization.CultureInfo (""))) {

                    case "basic": serviceBindingType = Mercury.Client.Enumerations.ServiceBindingType.BasicHttpBinding; break;

                    case "wshttp": serviceBindingType = Mercury.Client.Enumerations.ServiceBindingType.WsHttpBinding; break;

                    case "nettcp": serviceBindingType = Mercury.Client.Enumerations.ServiceBindingType.NetTcpBinding; break;

                    default: serviceBindingType = Mercury.Client.Enumerations.ServiceBindingType.BasicHttpBinding; break;

                }

            } // end try

            catch {

                serviceBindingType = Mercury.Client.Enumerations.ServiceBindingType.BasicHttpBinding;

            }

            return;

        }

        private void InitializeServices () {

            // initialize WCF Service Clients

            switch (serviceBindingType) {

                case Mercury.Client.Enumerations.ServiceBindingType.WsHttpBinding:

                    securityClient = new Server.Enterprise.SecurityClient (WSHttpBinding, EndpointAddressWs ("Enterprise", "Security"));

                    applicationClient = new Server.Application.ApplicationClient (WSHttpBinding, EndpointAddressWs ("Core", "Application"));


                    break;

                case Mercury.Client.Enumerations.ServiceBindingType.NetTcpBinding:

                    securityClient = new Server.Enterprise.SecurityClient (NetTcpBinding, EndpointAddressNetTcp ("Enterprise", "Security"));

                    securityClient.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;

                    applicationClient = new Server.Application.ApplicationClient (NetTcpBinding, EndpointAddressNetTcp ("Core", "Application"));

                    applicationClient.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;

                    break;

                case Mercury.Client.Enumerations.ServiceBindingType.BasicHttpBinding:

                default:

                    securityClient = new Server.Enterprise.SecurityClient (BasicHttpBinding, EndpointAddress ("Enterprise", "Security"));

                    applicationClient = new Server.Application.ApplicationClient (BasicHttpBinding, EndpointAddress ("Core", "Application"));

                    break;

            } // switch (serviceBindingType) 


            // OPEN UP THE MAX OBJECTS IN GRAPH TO MAXIMUM

            foreach (System.ServiceModel.Description.OperationDescription currentOperation in applicationClient.Endpoint.Contract.Operations) {

                System.ServiceModel.Description.DataContractSerializerOperationBehavior dataContractSerializer =

                    (System.ServiceModel.Description.DataContractSerializerOperationBehavior)

                    currentOperation.Behaviors.Find<System.ServiceModel.Description.DataContractSerializerOperationBehavior> ();

                if (dataContractSerializer != null) {

                    dataContractSerializer.MaxItemsInObjectGraph = Int32.MaxValue;

                }


            }

            return;

        }

        protected void SecurityClientSetToAnonymousWs () {

            if (securityClient != null) {

                securityClient.Close ();

                securityClient = null;

            }

            securityClient = new Server.Enterprise.SecurityClient (WSHttpBindingAnonymous, EndpointAddressWs ("Enterprise", "Security"));

        }

        protected void SecurityClientSetToWindowsWs () {

            if (securityClient != null) {

                securityClient.Close ();

                securityClient = null;

            }

            securityClient = new Server.Enterprise.SecurityClient (WSHttpBinding, EndpointAddressWs ("Enterprise", "Security"));

            return;

        }

        protected void SecurityClientSetToWindowsNetTcp () {

            if (securityClient != null) {

                securityClient.Close ();

                securityClient = null;

            }

            securityClient = new Server.Enterprise.SecurityClient (NetTcpBinding, EndpointAddressNetTcp ("Enterprise", "Security"));

            return;

        }

        #endregion


        #region Bindings and Endpoints

        protected void BindingTimeoutsSet (System.ServiceModel.Channels.Binding binding) {

            BindingTimeoutsSet (binding, 10);

            return;
        }

        protected void BindingTimeoutsSet (System.ServiceModel.Channels.Binding binding, int minutes) {

            binding.CloseTimeout = new TimeSpan (0, 1, 0);

            binding.OpenTimeout = new TimeSpan (0, 1, 0);

            binding.ReceiveTimeout = new TimeSpan (0, minutes, 0);

            binding.SendTimeout = new TimeSpan (0, minutes, 0);

            return;

        }


        protected System.ServiceModel.Channels.Binding BasicHttpBinding {
            get {

                System.ServiceModel.BasicHttpBinding newBinding = new System.ServiceModel.BasicHttpBinding ();

                newBinding.Name = "BasicHttpBinding";

                BindingTimeoutsSet (newBinding);

                newBinding.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.None;

                newBinding.Security.Transport.ClientCredentialType = System.ServiceModel.HttpClientCredentialType.None;
                newBinding.Security.Transport.ProxyCredentialType = System.ServiceModel.HttpProxyCredentialType.None;
                newBinding.Security.Transport.Realm = string.Empty;

                newBinding.Security.Message.ClientCredentialType = System.ServiceModel.BasicHttpMessageCredentialType.UserName;
                newBinding.Security.Message.AlgorithmSuite = System.ServiceModel.Security.SecurityAlgorithmSuite.Default;

                newBinding.MaxReceivedMessageSize = Int32.MaxValue;
                newBinding.MaxBufferPoolSize = Int32.MaxValue;
                newBinding.MaxReceivedMessageSize = Int32.MaxValue;

                newBinding.ReaderQuotas.MaxDepth = Int32.MaxValue;
                newBinding.ReaderQuotas.MaxStringContentLength = Int32.MaxValue;
                newBinding.ReaderQuotas.MaxArrayLength = Int32.MaxValue;
                newBinding.ReaderQuotas.MaxNameTableCharCount = Int32.MaxValue;
                newBinding.ReaderQuotas.MaxBytesPerRead = Int32.MaxValue;

                return newBinding;

            }

        }

        protected System.ServiceModel.Channels.Binding WSHttpBinding {
            get {

                System.ServiceModel.WSHttpBinding newBinding = new System.ServiceModel.WSHttpBinding ();

                newBinding.Name = "WSHttpBinding";

                BindingTimeoutsSet (newBinding);

                newBinding.Security.Mode = System.ServiceModel.SecurityMode.Message;

                newBinding.Security.Transport.ClientCredentialType = System.ServiceModel.HttpClientCredentialType.Windows;
                newBinding.Security.Transport.ProxyCredentialType = System.ServiceModel.HttpProxyCredentialType.None;
                newBinding.Security.Transport.Realm = string.Empty;

                newBinding.Security.Message.ClientCredentialType = System.ServiceModel.MessageCredentialType.Windows;
                newBinding.Security.Message.AlgorithmSuite = System.ServiceModel.Security.SecurityAlgorithmSuite.Default;

                newBinding.MaxReceivedMessageSize = Int32.MaxValue;
                newBinding.MaxBufferPoolSize = Int32.MaxValue;
                newBinding.MaxReceivedMessageSize = Int32.MaxValue;

                newBinding.ReaderQuotas.MaxDepth = Int32.MaxValue;
                newBinding.ReaderQuotas.MaxStringContentLength = Int32.MaxValue;
                newBinding.ReaderQuotas.MaxArrayLength = Int32.MaxValue;
                newBinding.ReaderQuotas.MaxNameTableCharCount = Int32.MaxValue;
                newBinding.ReaderQuotas.MaxBytesPerRead = Int32.MaxValue;

                return newBinding;

            }

        }

        protected System.ServiceModel.Channels.Binding WSHttpBindingAnonymous {
            get {

                System.ServiceModel.WSHttpBinding newBinding = new System.ServiceModel.WSHttpBinding ();

                newBinding.Name = "WSHttpBinding";

                BindingTimeoutsSet (newBinding);

                newBinding.Security.Mode = System.ServiceModel.SecurityMode.Message;

                newBinding.Security.Transport.ClientCredentialType = System.ServiceModel.HttpClientCredentialType.None;
                newBinding.Security.Transport.ProxyCredentialType = System.ServiceModel.HttpProxyCredentialType.None;
                newBinding.Security.Transport.Realm = string.Empty;

                newBinding.Security.Message.ClientCredentialType = System.ServiceModel.MessageCredentialType.None;
                newBinding.Security.Message.AlgorithmSuite = System.ServiceModel.Security.SecurityAlgorithmSuite.Default;

                newBinding.MaxReceivedMessageSize = Int32.MaxValue;
                newBinding.MaxBufferPoolSize = Int32.MaxValue;
                newBinding.MaxReceivedMessageSize = Int32.MaxValue;

                newBinding.ReaderQuotas.MaxDepth = Int32.MaxValue;
                newBinding.ReaderQuotas.MaxStringContentLength = Int32.MaxValue;
                newBinding.ReaderQuotas.MaxArrayLength = Int32.MaxValue;
                newBinding.ReaderQuotas.MaxNameTableCharCount = Int32.MaxValue;
                newBinding.ReaderQuotas.MaxBytesPerRead = Int32.MaxValue;

                return newBinding;

            }

        }

        protected System.ServiceModel.Channels.Binding NetTcpBinding {

            get {

                System.ServiceModel.NetTcpBinding binding = new System.ServiceModel.NetTcpBinding ();

                binding.Name = "NetTcpBinding";

                BindingTimeoutsSet (binding);



                binding.ReliableSession.Ordered = true;

                binding.ReliableSession.Enabled = true;

                binding.ReliableSession.InactivityTimeout = new TimeSpan (00, 60, 0);


                binding.Security.Mode = System.ServiceModel.SecurityMode.Message;

                binding.Security.Transport.ClientCredentialType = System.ServiceModel.TcpClientCredentialType.Windows;

                binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;


                binding.Security.Message.ClientCredentialType = System.ServiceModel.MessageCredentialType.Windows;

                binding.Security.Message.AlgorithmSuite = System.ServiceModel.Security.SecurityAlgorithmSuite.Default;


                binding.MaxReceivedMessageSize = Int32.MaxValue;

                binding.MaxBufferPoolSize = Int32.MaxValue;

                binding.MaxReceivedMessageSize = Int32.MaxValue;

                binding.ReaderQuotas.MaxDepth = Int32.MaxValue;

                binding.ReaderQuotas.MaxStringContentLength = Int32.MaxValue;

                binding.ReaderQuotas.MaxArrayLength = Int32.MaxValue;

                binding.ReaderQuotas.MaxNameTableCharCount = Int32.MaxValue;

                binding.ReaderQuotas.MaxBytesPerRead = Int32.MaxValue;


                return binding;

            }

        }


        protected String EndpointBaseAddress (String namespacePath, String serviceName) {

            try {

                if ((String.IsNullOrEmpty (serviceHostAddress)) || (String.IsNullOrEmpty (serviceHostPort))) {

                    serviceHostAddress = System.Configuration.ConfigurationManager.AppSettings.GetValues ("Mercury.Server.ServiceHost.Address")[0].ToString ();

                    serviceHostPort = System.Configuration.ConfigurationManager.AppSettings.GetValues ("Mercury.Server.ServiceHost.Port")[0].ToString ();

                }

            }

            catch {

                if (String.IsNullOrEmpty (serviceHostAddress)) { serviceHostAddress = "localhost"; }

                if (String.IsNullOrEmpty (serviceHostPort)) { serviceHostPort = "8080"; }

            }

            return @"http://" + serviceHostAddress + ":" + serviceHostPort + @"/" + namespacePath + @"/" + serviceName + ".svc";

        }

        protected System.ServiceModel.EndpointAddress EndpointAddress (String namespacePath, String serviceName) {

            return new System.ServiceModel.EndpointAddress (EndpointBaseAddress (namespacePath, serviceName));

        }

        protected System.ServiceModel.EndpointAddress EndpointAddressWs (String namespacePath, String serviceName) {

            return new System.ServiceModel.EndpointAddress (EndpointBaseAddress (namespacePath, serviceName) + "/ws");

        }

        protected System.ServiceModel.EndpointAddress EndpointAddressNetTcp (String namespacePath, String serviceName) {

            try {

                if ((String.IsNullOrEmpty (serviceHostAddress)) || (String.IsNullOrEmpty (serviceHostPort))) {

                    serviceHostAddress = System.Configuration.ConfigurationManager.AppSettings.GetValues ("Mercury.Server.ServiceHost.Address")[0].ToString ();

                    serviceHostPort = System.Configuration.ConfigurationManager.AppSettings.GetValues ("Mercury.Server.ServiceHost.Port")[0].ToString ();

                }

            }

            catch {

                if (String.IsNullOrEmpty (serviceHostAddress)) { serviceHostAddress = "localhost"; }

                if (String.IsNullOrEmpty (serviceHostPort)) { serviceHostPort = "808"; }

            }

            String endpointAddress = @"net.tcp://" + serviceHostAddress + ":" + serviceHostPort + @"/" + namespacePath + @"/" + serviceName + ".svc";

            TraceWriteLineVerbose (TraceSwitchService, "net.tcp Endpoint Address: " + endpointAddress);

            return new System.ServiceModel.EndpointAddress (endpointAddress);

        }

        #endregion


        #region Service Clients

        public Server.Enterprise.SecurityClient SecurityClient {

            get {

                try {

                    if (securityClient == null) {

                        InitializeServices ();

                        TraceWriteLineVerbose (TraceSwitchService, "Security Client Binding Type: " + serviceBindingType.ToString ());

                    }

                    switch (securityClient.State) {

                        case System.ServiceModel.CommunicationState.Created:

                            securityClient.Open ();

                            TraceWriteLineVerbose (TraceSwitchService, "Security Client Binding Type: " + serviceBindingType.ToString ());

                            break;

                        case System.ServiceModel.CommunicationState.Opening:

                        case System.ServiceModel.CommunicationState.Opened:

                            /* DO NOTHING */

                            break;

                        case System.ServiceModel.CommunicationState.Closing:

                        case System.ServiceModel.CommunicationState.Closed:

                            securityClient = null;

                            InitializeServices ();

                            TraceWriteLineVerbose (TraceSwitchService, "Security Client Binding Type: " + serviceBindingType.ToString ());

                            securityClient.Open ();

                            break;

                        case System.ServiceModel.CommunicationState.Faulted:

                            throw new ApplicationException ("Application Client Faulted.");

                        default:

                            SetLastException (new ApplicationException ("Unknown Application Client State: " + securityClient.State.ToString ()));

                            break;

                    }

                }

                catch (Exception applicationException) {

                    // RECORD ANY NON-CUSTOM APPLICATION EXCEPTIONS TO THE LOG

                    if (!(applicationException is ApplicationException)) { SetLastException (applicationException); }


                    // INCREMENT ATTEMPTS COUNTER 

                    serviceAttempts = serviceAttempts + 1;

                    if (securityClient != null) {

                        if (securityClient.State == System.ServiceModel.CommunicationState.Faulted) {

                            securityClient.Abort ();

                        }

                        securityClient = null;

                    }

                    // ANOTHER ATTEMPT (UP TO 3 TRIES)

                    if (serviceAttempts < 3) { securityClient = SecurityClient; }

                }


#if DEBUG

                // WRITE OUT DEBUG CHATTER 

                if (securityClient != null) {

                    System.Diagnostics.Debug.Write ("----> Security Request: " + (new System.Diagnostics.StackTrace ().GetFrames ()[1].GetMethod ().Name));

                    System.Diagnostics.Debug.Write ("   <-- FROM -->   " + (new System.Diagnostics.StackTrace ().GetFrames ()[2].GetMethod ().Name));

                    System.Diagnostics.Debug.Write ("   <-- FROM -->   " + (new System.Diagnostics.StackTrace ().GetFrames ()[3].GetMethod ().Name));

                    System.Diagnostics.Debug.WriteLine (String.Empty);

                }

#endif

                // RESET SERVICE ATTEMPTS FOR NEXT CALL

                serviceAttempts = 0;

                return securityClient;

            }

        }

        public void SecurityClientClose () {

            if (securityClient == null) { return; }


            switch (securityClient.State) {

                case System.ServiceModel.CommunicationState.Created:

                case System.ServiceModel.CommunicationState.Opened:

                    securityClient.Close ();

                    break;

                case System.ServiceModel.CommunicationState.Faulted:

                    securityClient.Abort ();

                    break;

            }

#if DEBUG

            // WRITE OUT DEBUG CHATTER 

            if (securityClient != null) {

                System.Diagnostics.Debug.Write ("----> Security Close: " + (new System.Diagnostics.StackTrace ().GetFrames ()[1].GetMethod ().Name));

                System.Diagnostics.Debug.Write ("   <-- FROM -->   " + (new System.Diagnostics.StackTrace ().GetFrames ()[2].GetMethod ().Name));

                System.Diagnostics.Debug.Write ("   <-- FROM -->   " + (new System.Diagnostics.StackTrace ().GetFrames ()[3].GetMethod ().Name));

                System.Diagnostics.Debug.WriteLine (String.Empty);

            }

#endif

            return;

        }

        public Server.Application.ApplicationClient ApplicationClient {

            get {

                try {

                    if (applicationClient == null) {

                        InitializeServices ();

                    }

                    switch (applicationClient.State) {

                        case System.ServiceModel.CommunicationState.Created:

                            applicationClient.Open ();

                            break;

                        case System.ServiceModel.CommunicationState.Opening:

                        case System.ServiceModel.CommunicationState.Opened:

                            /* DO NOTHING */

                            break;

                        case System.ServiceModel.CommunicationState.Closing:

                        case System.ServiceModel.CommunicationState.Closed:

                            applicationClient = null;

                            InitializeServices ();

                            applicationClient.Open ();

                            break;

                        case System.ServiceModel.CommunicationState.Faulted:

                            throw new ApplicationException ("Application Client Faulted.");

                        default:

                            SetLastException (new ApplicationException ("Unknown Application Client State: " + applicationClient.State.ToString ()));

                            break;

                    }

                }

                catch (Exception applicationException) {

                    // RECORD ANY NON-CUSTOM APPLICATION EXCEPTIONS TO THE LOG

                    if (!(applicationException is ApplicationException)) { SetLastException (applicationException); }


                    // INCREMENT ATTEMPTS COUNTER 

                    serviceAttempts = serviceAttempts + 1;

                    if (applicationClient != null) {

                        if (applicationClient.State == System.ServiceModel.CommunicationState.Faulted) {

                            applicationClient.Abort ();

                        }

                        applicationClient = null;

                    }

                    // ANOTHER ATTEMPT (UP TO 3 TRIES)

                    if (serviceAttempts < 3) { applicationClient = ApplicationClient; }

                }

                    
#if DEBUG

                // WRITE OUT DEBUG CHATTER 

                if (applicationClient != null) {

                    System.Diagnostics.Debug.Write ("----> Application Request: " + (new System.Diagnostics.StackTrace ().GetFrames ()[1].GetMethod ().Name));

                    System.Diagnostics.Debug.Write ("   <-- FROM -->   " + (new System.Diagnostics.StackTrace ().GetFrames ()[2].GetMethod ().Name));

                    System.Diagnostics.Debug.Write ("   <-- FROM -->   " + (new System.Diagnostics.StackTrace ().GetFrames ()[3].GetMethod ().Name));

                    System.Diagnostics.Debug.WriteLine (String.Empty);

                }

#endif

                // RESET SERVICE ATTEMPTS FOR NEXT CALL

                serviceAttempts = 0; 
                
                return applicationClient;

            }

        }

        public void ApplicationClientClose () {

            if (applicationClient == null) { return; }


            switch (applicationClient.State) {

                case System.ServiceModel.CommunicationState.Created:
                   
                case System.ServiceModel.CommunicationState.Opened:

                    applicationClient.Close ();

                    break;

                case System.ServiceModel.CommunicationState.Faulted:

                    applicationClient.Abort ();

                    break;

            }

#if DEBUG

            // WRITE OUT DEBUG CHATTER 

            if (securityClient != null) {

                System.Diagnostics.Debug.Write ("----> Application Close: " + (new System.Diagnostics.StackTrace ().GetFrames ()[1].GetMethod ().Name));

                System.Diagnostics.Debug.Write ("   <-- FROM -->   " + (new System.Diagnostics.StackTrace ().GetFrames ()[2].GetMethod ().Name));

                System.Diagnostics.Debug.Write ("   <-- FROM -->   " + (new System.Diagnostics.StackTrace ().GetFrames ()[3].GetMethod ().Name));

                System.Diagnostics.Debug.WriteLine (String.Empty);

            }

#endif
            return;

        }

        #endregion


        #region Session Authentication and Logoff

        public Boolean Authenticate (String securityAuthorityName, String accountType, String accountName, String password, String newPassword, String environment) {

            try {

                authenticationResponse = SecurityClient.Authenticate (securityAuthorityName, accountType, accountName, password, newPassword, environment);

                if (authenticationResponse.IsAuthenticated) {

                    session = new Session (authenticationResponse);

                }

            }

            catch (Exception authenticationException) {

                SetLastException (authenticationException);

                authenticationResponse.HasException = true;

                authenticationResponse.Exception = new Server.Enterprise.ServiceException ();

                authenticationResponse.Exception.Message = authenticationException.Message;

                authenticationResponse.IsAuthenticated = false;

            }

            return authenticationResponse.IsAuthenticated;

        }

        public Boolean Authenticate (String environment) {

            System.Security.Principal.WindowsImpersonationContext impersonationContext = null;

            if (serviceBindingType == Mercury.Client.Enumerations.ServiceBindingType.BasicHttpBinding) {

                authenticationResponse.IsAuthenticated = false;

                authenticationResponse.HasException = true;

                authenticationResponse.Exception = new Server.Enterprise.ServiceException ();

                authenticationResponse.Exception.Message = "Windows Integrated Authentication not supported with Basic HTTP Communications. You must enter account/password.";

                return false;

            }

            try {

                System.Diagnostics.Trace.WriteLineIf (TraceSwitchSecurity.TraceVerbose, "\r\n[Mercury.Client.Application.WindowsAuthentication] Current Credentials");

                System.Diagnostics.Trace.WriteLineIf (TraceSwitchSecurity.TraceVerbose, "Client Thread Principal: " + System.Threading.Thread.CurrentPrincipal.Identity.Name);

                System.Diagnostics.Trace.WriteLineIf (TraceSwitchSecurity.TraceVerbose, "Client System Principal: " + System.Security.Principal.WindowsIdentity.GetCurrent ().Name);

                System.Diagnostics.Trace.WriteLineIf (TraceSwitchSecurity.TraceVerbose, "Client Thread Authentication Type: " + System.Threading.Thread.CurrentPrincipal.Identity.AuthenticationType);


                if (impersonateUser) {

                    impersonationContext = ((System.Security.Principal.WindowsIdentity)System.Threading.Thread.CurrentPrincipal.Identity).Impersonate ();

                    System.Diagnostics.Trace.WriteLineIf (TraceSwitchSecurity.TraceVerbose, "\r\n[Mercury.Client.Application.WindowsAuthentication] Impersonated Credentials");

                    System.Diagnostics.Trace.WriteLineIf (TraceSwitchSecurity.TraceVerbose, "Client Impresonation Thread Principal: " + System.Threading.Thread.CurrentPrincipal.Identity.Name);

                    System.Diagnostics.Trace.WriteLineIf (TraceSwitchSecurity.TraceVerbose, "Client Impresonation System Principal: " + System.Security.Principal.WindowsIdentity.GetCurrent ().Name);

                    System.Diagnostics.Trace.WriteLineIf (TraceSwitchSecurity.TraceVerbose, "Client Impresonation Thread Authentication Type: " + System.Threading.Thread.CurrentPrincipal.Identity.AuthenticationType);

                }


                // Release any previously acquired Client to refresh calling Credentials for authentication

                if (serviceBindingType == Mercury.Client.Enumerations.ServiceBindingType.WsHttpBinding) {

                    SecurityClientSetToWindowsWs ();

                }

                else if (serviceBindingType == Mercury.Client.Enumerations.ServiceBindingType.NetTcpBinding) {

                    SecurityClientSetToWindowsNetTcp ();

                }

                authenticationResponse = SecurityClient.AuthenticateWindows (environment);

                if (authenticationResponse.IsAuthenticated) {

                    session = new Session (authenticationResponse);

                }

                if (authenticationResponse.HasException) {

                    if (authenticationResponse.AuthenticationError == Server.Enterprise.AuthenticationError.MustSelectEnvironment) {

                        // DIRECT WRITE TO EXCEPTION SO THAT NO OUTPUT IS SENT TO DEBUG (NORMAL, ACCEPTABLE ERROR STATE)

                        lastException = new ApplicationException (authenticationResponse.Exception.Message);

                    }

                    else { SetLastException (authenticationResponse.Exception); }
                
                }

            }

            catch (Exception authenticationException) {

                SetLastException (authenticationException);

                authenticationResponse.HasException = true;

                authenticationResponse.Exception = new Server.Enterprise.ServiceException ();

                authenticationResponse.Exception.Message = authenticationException.Message;

                authenticationResponse.IsAuthenticated = false;

            }

            finally {

                if (impersonationContext != null) {

                    impersonationContext.Undo ();

                }

            }

            return authenticationResponse.IsAuthenticated;

        }

        public void LogOff () {

            ClearLastException ();

            try {

                Server.Enterprise.BooleanResponse response = null;

                response = SecurityClient.LogOff (session.Token);

            }

            catch (Exception logOffException) {

                SetLastException (logOffException);

            }

            return;

        }

        #endregion


        #region Unauthenticated Methods - Security Client

        public Dictionary<Int64, String> SecurityClient_SecurityAuthorityDictionary (Boolean useCaching) {

            String cacheKey = "Application.SecurityAuthority.Dictionary";

            Dictionary<Int64, String> securityAuthorities = new Dictionary<Int64, String> ();

            Server.Enterprise.DictionaryResponse response;


            ClearLastException ();

            try {

                // TRY CACHE FIRST

                securityAuthorities = (Dictionary<Int64, String>)cacheManager.GetObject (cacheKey);
                
                if (!useCaching) { securityAuthorities = null; }

                if (securityAuthorities == null) {

                    securityAuthorities = new Dictionary<Int64, String> (); // RESET DICTIONARY FOR RETURN RESPONSE


                    // TRY ATTACHING WITH DEFAULT SERVICE CONFIGURATION

                    response = SecurityClient.SecurityAuthorityDictionary ();

                    if (!response.HasException) { 
                        
                        securityAuthorities = response.Dictionary;

                        if (securityAuthorities.Count > 0) { cacheManager.CacheObject (cacheKey, securityAuthorities, CacheExpirationData); }
                    
                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception handledException) {

                SetLastException (handledException);

                // TRY ATTACHING WITH NEW ANONYMOUS CONNECTION 

                try {

                    Server.Enterprise.SecurityClient anonymousSecurityClient = new Server.Enterprise.SecurityClient (BasicHttpBinding, EndpointAddress ("Enterprise", "Security"));

                    response = anonymousSecurityClient.SecurityAuthorityDictionary ();

                    if (!response.HasException) { 
                        
                        securityAuthorities = response.Dictionary;

                        if (securityAuthorities.Count > 0) { cacheManager.CacheObject (cacheKey, securityAuthorities, CacheExpirationData); }
                    
                    }

                    else { SetLastException (response.Exception); }

                }

                catch (Exception applicationException) {

                    SetLastException (applicationException);

                }

            }

            return securityAuthorities;

        }

        #endregion


        #region Authorization Validation

        public Boolean HasEnterprisePermission (String permissionName) {

            Boolean hasPermission = false;

            return true;

            //if (session.EnterprisePermissionSet.ContainsKey (Server.EnterprisePermissions.EnterpriseAdministrator)) { hasPermission = true; }

            //else { hasPermission = session.EnterprisePermissionSet.ContainsKey (permissionName); }

            //// TODO: AUDIT PERMISSION USAGE

            //return hasPermission;

        }

        public Boolean HasEnvironmentPermission (String permissionName) {

            return true;

            //Boolean hasPermission = false;

            //if (session.EnterprisePermissionSet.ContainsKey (Server.EnterprisePermissions.EnterpriseAdministrator)) { hasPermission = true; }

            //else if (session.EnvironmentPermissionSet.ContainsKey (Server.EnvironmentPermissions.EnvironmentAdministrator)) { hasPermission = true; }

            //else { hasPermission = session.EnvironmentPermissionSet.ContainsKey (permissionName); }

            //// TODO: AUDIT PERMISSION USAGE

            //return hasPermission;

        }

        public Boolean HasEnvironmentPermission (String environmentName, String permissionName) {

            if (session.EnterprisePermissionSet.ContainsKey (Mercury.Server.EnterprisePermissions.EnterpriseAdministrator)) { return true; }

            if (session.EnvironmentName == environmentName) { return HasEnvironmentPermission (permissionName); }

            Mercury.Server.Application.BooleanResponse response = new Server.Application.BooleanResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.HasEnvironmentPermissionByEnvironment (session.Token, environmentName, permissionName);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return response.Result;

        }

        #endregion


        #region Audit

        public List<Mercury.Server.Application.AuditAuthentication> ActiveSessionsAvailable () {

            List<Mercury.Server.Application.AuditAuthentication> activeSessions = new List<Mercury.Server.Application.AuditAuthentication> ();

            Mercury.Server.Application.AuditAuthenticationCollectionResponse response = new Mercury.Server.Application.AuditAuthenticationCollectionResponse ();


            ClearLastException ();

            try {

                response = ApplicationClient.ActiveSessionsAvailable (session.Token);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                activeSessions.AddRange (response.Collection);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return activeSessions;

        }

        #endregion


        #region Enterprise Permissions
        
        public List<String> EnterprisePermissionList () {

            Mercury.Server.Application.StringListResponse response = new Mercury.Server.Application.StringListResponse ();

            List<String> permissionList = new List<String> ();

            ClearLastException ();

            try {

                response = ApplicationClient.EnterprisePermissionList (session.Token);

                permissionList.AddRange (response.ResultList);

            }

            catch (Exception serviceException) {

                SetLastException (serviceException);

            }

            return permissionList;

        }

        public Dictionary<Int64, String> EnterprisePermissionDictionary () {

            Mercury.Server.Application.DictionaryResponse response = new Server.Application.DictionaryResponse ();

            Dictionary<Int64, String> permissionDictionary = new Dictionary<Int64, String> ();

            ClearLastException ();

            try {

                response = ApplicationClient.EnterprisePermissionDictionary (session.Token);

                if (!response.HasException) { permissionDictionary = response.Dictionary; }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return permissionDictionary;

        }

        public List<Mercury.Server.Application.Permission> EnterprisePermissionsAvailable () {

            Mercury.Server.Application.PermissionCollectionResponse permissionCollection;

            List<Mercury.Server.Application.Permission> permissions = new List<Mercury.Server.Application.Permission> ();

            ClearLastException ();

            try {

                permissionCollection = ApplicationClient.EnterprisePermissionsAvailable (session.Token);

                for (Int32 currentPermissionIndex = 0; currentPermissionIndex < permissionCollection.Collection.Length; currentPermissionIndex++) {

                    permissions.Add (permissionCollection.Collection[currentPermissionIndex]);

                }

            }

            catch (Exception serviceException) {

                SetLastException (serviceException);

            }

            return permissions;

        }


        public List<Mercury.Server.Application.SecurityGroupPermission> SecurityGroupEnterprisePermissionsGet (String securityAuthorityName, String securityGroupId) {

            Mercury.Server.Application.SecurityGroupPermissionCollectionResponse groupPermissionResponse;

            List<Mercury.Server.Application.SecurityGroupPermission> securityGroupEnterprisePermission = new List<Mercury.Server.Application.SecurityGroupPermission> ();

            ClearLastException ();

            try {

                groupPermissionResponse = ApplicationClient.SecurityGroupEnterprisePermissionsGet (session.Token, securityAuthorityName, securityGroupId);

                for (Int32 currentGroupPermissionIndex = 0; currentGroupPermissionIndex < groupPermissionResponse.Collection.Length; currentGroupPermissionIndex++) {

                    securityGroupEnterprisePermission.Add (groupPermissionResponse.Collection[currentGroupPermissionIndex]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return securityGroupEnterprisePermission;

        }

        public Boolean SecurityGroupEnterprisePermissionSave (Int64 securityAuthorityId, String securityGroupId, Int64 permissionId, Boolean isGranted, Boolean isDenied) {

            Mercury.Server.Application.BooleanResponse response = new Server.Application.BooleanResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.SecurityGroupEnterprisePermissionSave (session.Token, securityAuthorityId, securityGroupId, permissionId, isGranted, isDenied);

                if (response.HasException) {

                    throw new ApplicationException (response.Exception.Message);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

                response.Result = false;

            }

            return response.Result;

        }

        #endregion


        #region Security Authority

        public List<Mercury.Server.Application.SecurityAuthority> SecurityAuthoritiesAvailable (Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".SecurityAuthority.Available";

            Mercury.Server.Application.SecurityAuthorityCollectionResponse response;

            List<Mercury.Server.Application.SecurityAuthority> securityAuthorities = new List<Mercury.Server.Application.SecurityAuthority> ();

            ClearLastException ();

            try {

                securityAuthorities = (List<Mercury.Server.Application.SecurityAuthority>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { securityAuthorities = null; }

                if (securityAuthorities == null) {

                    securityAuthorities = new List<Server.Application.SecurityAuthority> ();

                    response = ApplicationClient.SecurityAuthoritiesAvailable (session.Token);

                    if (!response.HasException) {

                        securityAuthorities.AddRange (response.Collection);

                        if (securityAuthorities.Count > 0) {

                            cacheManager.CacheObject (cacheKey, securityAuthorities, CacheExpirationData);

                        }

                    }

                    else { SetLastException (response.Exception); }


                }

            }

            catch (Exception serviceException) {

                SetLastException (serviceException);

            }

            return securityAuthorities;

        }

        public Dictionary<Int64, String> SecurityAuthorityDictionary (Boolean useCaching) {

            Dictionary<Int64, String> securityAuthorities = new Dictionary<Int64, String> ();

            Mercury.Server.Application.DictionaryResponse response;

            String cacheKey = "Application." + session.EnvironmentId + ".SecurityAuthority.Dictionary";


            ClearLastException ();

            try {

                securityAuthorities = (Dictionary<Int64, String>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { securityAuthorities = null; }

                if (securityAuthorities == null) {

                    securityAuthorities = new Dictionary<Int64, String> ();

                    response = ApplicationClient.SecurityAuthorityDictionary (session.Token);

                    if (!response.HasException) {

                        securityAuthorities = response.Dictionary;

                        if (securityAuthorities.Count > 0) { cacheManager.CacheObject (cacheKey, securityAuthorities, CacheExpirationData); }

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return securityAuthorities;

        }


        public Int64 SecurityAuthorityGetIdByName (String forObjectName, Boolean useCaching) {

            if (String.IsNullOrWhiteSpace (forObjectName)) { return 0; }

            Int64 objectId = 0;

            Dictionary<Int64, String> objectDictionary = SecurityAuthorityDictionary (useCaching);


            foreach (Int64 currentObjectId in objectDictionary.Keys) {

                if (objectDictionary[currentObjectId] == forObjectName) {

                    objectId = currentObjectId;

                    break;

                }

            }

            return objectId;

        }

        public String SecurityAuthorityGetNameById (Int64 forObjectId, Boolean useCaching) {

            if (forObjectId == 0) { return String.Empty; }

            String objectName = String.Empty;

            Dictionary<Int64, String> objectDictionary = SecurityAuthorityDictionary (useCaching);


            foreach (Int64 currentObjectId in objectDictionary.Keys) {

                if (currentObjectId == forObjectId) {

                    objectName = objectDictionary[currentObjectId];

                    break;

                }

            }

            return objectName;

        }


        public Mercury.Server.Application.SecurityAuthority SecurityAuthorityGet (String securityAuthorityName, Boolean useCaching) {

            String cacheKey = "Application.SecurityAuthorityByName" + securityAuthorityName;

            Mercury.Server.Application.SecurityAuthority securityAuthority = new Mercury.Server.Application.SecurityAuthority ();

            ClearLastException ();

            try {

                securityAuthority = (Mercury.Server.Application.SecurityAuthority)cacheManager.GetObject (cacheKey);

                if (!useCaching) { securityAuthority = null; }

                if (securityAuthority == null) {

                    securityAuthority = ApplicationClient.SecurityAuthorityGetByName (session.Token, securityAuthorityName);

                    cacheManager.CacheObject (cacheKey, securityAuthority, CacheExpirationData);

                }

            }

            catch (Exception serviceException) {

                SetLastException (serviceException);

            }

            return securityAuthority;

        }

        public Mercury.Server.Application.SecurityAuthority SecurityAuthorityGet (Int64 securityAuthorityId, Boolean useCaching) {

            String cacheKey = CachePrefix.SecurityAuthority + securityAuthorityId.ToString ();

            Mercury.Server.Application.SecurityAuthority securityAuthority = new Mercury.Server.Application.SecurityAuthority ();

            ClearLastException ();

            try {

                securityAuthority = (Mercury.Server.Application.SecurityAuthority)cacheManager.GetObject (cacheKey);

                if (!useCaching) { securityAuthority = null; }

                if (securityAuthority == null) {

                    securityAuthority = ApplicationClient.SecurityAuthorityGet (session.Token, securityAuthorityId);

                    cacheManager.CacheObject (cacheKey, securityAuthority, CacheExpirationData);

                }

            }

            catch (Exception serviceException) {

                SetLastException (serviceException);

            }

            return securityAuthority;

        }


        public Boolean SecurityAuthoritySave (Mercury.Server.Application.SecurityAuthority securityAuthority) {

            Mercury.Server.Application.BooleanResponse response = new Mercury.Server.Application.BooleanResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.SecurityAuthoritySave (session.Token, securityAuthority);

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".SecurityAuthority.Available");

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".SecurityAuthority.Dictionary");

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".SecurityAuthority." + securityAuthority.Id.ToString ());

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".SecurityAuthority." + securityAuthority.Name.ToString ());

                if (response.HasException) { SetLastException (response.Exception); }

            }


            catch (Exception serviceException) {

                SetLastException (serviceException);

            }

            return response.Result;

        }

        public Boolean SecurityAuthorityDelete (String securityAuthorityName) {

            Mercury.Server.Application.BooleanResponse response = new Mercury.Server.Application.BooleanResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.SecurityAuthorityDelete (session.Token, securityAuthorityName);

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".SecurityAuthority.Available");

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".SecurityAuthority.Dictionary");

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".SecurityAuthority." + securityAuthorityName);

                if (response.HasException) { SetLastException (response.Exception); }

            }

            catch (Exception serviceException) {

                SetLastException (serviceException);

            }

            return response.Result;

        }


        #region Security Authority Provider

        public List<Mercury.Server.Application.SecurityAuthorityDirectoryEntry> SecurityAuthorityProviderBrowseDirectory (String securityAuthorityName, String directoryPath) {

            Mercury.Server.Application.DirectoryEntryCollectionResponse directoryEntriesResponse = new Server.Application.DirectoryEntryCollectionResponse ();

            List<Mercury.Server.Application.SecurityAuthorityDirectoryEntry> directoryEntries = new List<Mercury.Server.Application.SecurityAuthorityDirectoryEntry> ();

            ClearLastException ();

            try {

                directoryEntriesResponse = ApplicationClient.SecurityAuthorityProviderBrowseDirectory (session.Token, securityAuthorityName, directoryPath);

                foreach (Mercury.Server.Application.SecurityAuthorityDirectoryEntry currentDirectoryEntry in directoryEntriesResponse.Collection) {

                    directoryEntries.Add (currentDirectoryEntry);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return directoryEntries;

        }

        #endregion


        #region Security Authority Groups

        public Dictionary<String, String> SecurityAuthoritySecurityGroupDictionary (Int64 securityAuthorityId) {

            Mercury.Server.Application.DictionaryStringResponse response = new Server.Application.DictionaryStringResponse ();

            Dictionary<String, String> groupDictionary = new Dictionary<String, String> ();

            ClearLastException ();

            try {

                response = ApplicationClient.SecurityAuthoritySecurityGroupDictionary (session.Token, securityAuthorityId);

                if (!response.HasException) { groupDictionary = response.Dictionary; }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return groupDictionary;

        }

        public Dictionary<String, String> SecurityAuthoritySecurityGroupDictionary (String securityAuthorityName) {

            return SecurityAuthoritySecurityGroupDictionary (SecurityAuthorityGetIdByName (securityAuthorityName, true));

        }


        public Mercury.Server.Application.SecurityGroup SecurityAuthoritySecurityGroupGet (String securityAuthorityName, String securityGroupId) {

            Mercury.Server.Application.SecurityGroup securityGroup = null;

            ClearLastException ();

            try {

                securityGroup = ApplicationClient.SecurityAuthoritySecurityGroupGet (session.Token, securityAuthorityName, securityGroupId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return securityGroup;

        }

        public List<Server.Application.SecurityAuthorityDirectoryEntry> SecurityAuthoritySecurityGroupGetMembership (Int64 securityAuthorityId, String securityGroupId, Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".EnvironmentAccess." + securityAuthorityId + ".SecurityGroupId." + securityGroupId + ".Membership";

            List<Server.Application.SecurityAuthorityDirectoryEntry> entries = new List<Server.Application.SecurityAuthorityDirectoryEntry> ();

            Server.Application.DirectoryEntryCollectionResponse response;



            ClearLastException ();

            try {

                entries = (List<Server.Application.SecurityAuthorityDirectoryEntry>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { entries = null; }

                if (entries == null) {

                    entries = new List<Server.Application.SecurityAuthorityDirectoryEntry> ();

                    response = ApplicationClient.SecurityAuthorityGroupGetMembership (session.Token, securityAuthorityId, securityGroupId);

                    if (!response.HasException) {

                        entries.AddRange (response.Collection);

                        cacheManager.CacheObject (cacheKey, entries, CacheExpirationData);

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return entries;

        }

        #endregion

        #endregion


        #region Environment

        public List<Server.Application.EnvironmentAccess> EnvironmentAccessGetByEnvironmentName (String environmentName, Boolean useCaching) {

            if (String.IsNullOrWhiteSpace (environmentName)) { return new List<Server.Application.EnvironmentAccess> (); }


            String cacheKey = "Application." + session.EnvironmentId + ".EnvironmentAccess." + environmentName;

            List<Server.Application.EnvironmentAccess> environmentAccess = new List<Server.Application.EnvironmentAccess> ();

            Server.Application.EnvironmentAccessCollectionResponse response;

            ClearLastException ();


            try {

                environmentAccess = (List<Server.Application.EnvironmentAccess>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { environmentAccess = null; }

                if (environmentAccess == null) {

                    environmentAccess = new List<Server.Application.EnvironmentAccess> ();

                    response = ApplicationClient.EnvironmentAccessGetByEnvironmentName (session.Token, environmentName);

                    if (!response.HasException) {

                        environmentAccess.AddRange (response.Collection);

                        cacheManager.CacheObject (cacheKey, environmentAccess, CacheExpirationData);

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return environmentAccess;

        }

        public List<Server.Application.EnvironmentAccess> SecurityGroupEnvironmentAccessGet (String securityAuthorityName, String securityGroupId) {

            Mercury.Server.Application.EnvironmentAccessCollectionResponse response;

            List<Server.Application.EnvironmentAccess> environmentAccess = new List<Server.Application.EnvironmentAccess> ();

            ClearLastException ();

            try {

                response = ApplicationClient.SecurityGroupEnvironmentAccessGet (session.Token, securityAuthorityName, securityGroupId);

                for (Int32 currentGroupAccessIndex = 0; currentGroupAccessIndex < response.Collection.Length; currentGroupAccessIndex++) {

                    environmentAccess.Add (response.Collection[currentGroupAccessIndex]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return environmentAccess;

        }

        public Boolean SecurityGroupEnvironmentAccessSave (Int64 securityAuthorityId, String securityGroupId, Int64 environmentId, Boolean isGranted, Boolean isDenied) {

            Mercury.Server.Application.BooleanResponse response = new Server.Application.BooleanResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.SecurityGroupEnvironmentAccessSave (session.Token, securityAuthorityId, securityGroupId, environmentId, isGranted, isDenied);

                if (response.HasException) {

                    throw new ApplicationException (response.Exception.Message);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

                response.Result = false;

            }

            return response.Result;

        }


        public Dictionary<Int64, String> EnvironmentDictionary (Boolean useCaching) {

            String cacheKey = CachePrefix.EnvironmentDictionary;

            Dictionary<Int64, String> environments = new Dictionary<Int64, String> ();

            Mercury.Server.Application.DictionaryResponse response;

            ClearLastException ();

            try {

                environments = (Dictionary<Int64, String>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { environments = null; }

                if (environments == null) {

                    environments = new Dictionary<Int64, String> ();

                    response = ApplicationClient.EnvironmentDictionary (session.Token);

                    environments = response.Dictionary;

                    cacheManager.CacheObject (cacheKey, environments, CacheExpirationData);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return environments;

        }

        public String EnvironmentGetNameById (Int64 environmentId) {

            String environmentName = String.Empty;

            Dictionary<Int64, String> environments = EnvironmentDictionary (true);

            if (environments.ContainsKey (environmentId)) {

                environmentName = environments[environmentId];

            }

            return environmentName;

        }

        public Int64 EnvironmentGetIdByName (String environmentName) {

            Int64 environmentId = 0;

            Dictionary<Int64, String> environments = EnvironmentDictionary (true);

            foreach (Int64 currentEnvironmentId in environments.Keys) {

                if (environments[currentEnvironmentId] == environmentName) {

                    environmentId = currentEnvironmentId;

                    break;

                }

            }

            return environmentId;

        }


        public Mercury.Server.Application.Environment EnvironmentGet (Int64 environmentId) {

            Mercury.Server.Application.Environment environment = new Mercury.Server.Application.Environment ();

            ClearLastException ();

            try {

                environment = ApplicationClient.EnvironmentGet (session.Token, environmentId);

            }

            catch (Exception serviceException) {

                SetLastException (serviceException);

            }

            return environment;

        }

        public Mercury.Server.Application.Environment EnvironmentGet (String environmentName) {

            Mercury.Server.Application.Environment environment = new Mercury.Server.Application.Environment ();

            ClearLastException ();

            try {

                environment = ApplicationClient.EnvironmentGetByName (session.Token, environmentName);

            }

            catch (Exception serviceException) {

                SetLastException (serviceException);

            }

            return environment;

        }


        public Boolean EnvironmentSave (Mercury.Server.Application.Environment environment) {

            Mercury.Server.Application.BooleanResponse response = new Mercury.Server.Application.BooleanResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.EnvironmentSave (session.Token, environment);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

            }


            catch (Exception serviceException) {

                SetLastException (serviceException);

            }

            return response.Result;

        }

        public Boolean EnvironmentDelete (String environmentName) {

            Mercury.Server.Application.BooleanResponse response = new Mercury.Server.Application.BooleanResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.EnvironmentDelete (session.Token, environmentName);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

            }

            catch (Exception serviceException) {

                SetLastException (serviceException);

            }

            return response.Result;

        }


        public List<String> EnvironmentList () {

            Mercury.Server.Application.StringListResponse response = new Mercury.Server.Application.StringListResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.EnvironmentList (session.Token);

                if (response.HasException) { SetLastException (new ApplicationException (response.Exception.Message)); }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return response.ResultList.ToList<String> ();

        }

        public List<Mercury.Server.Application.Environment> EnvironmentsAvailable () {

            Mercury.Server.Application.EnvironmentCollectionResponse environmentCollection;

            List<Mercury.Server.Application.Environment> environments = new List<Mercury.Server.Application.Environment> ();

            ClearLastException ();

            try {

                environmentCollection = ApplicationClient.EnvironmentsAvailable (session.Token);

                for (Int32 currentEnvironmentIndex = 0; currentEnvironmentIndex < environmentCollection.Collection.Length; currentEnvironmentIndex++) {

                    environments.Add (environmentCollection.Collection[currentEnvironmentIndex]);

                }

            }

            catch (Exception serviceException) {

                SetLastException (serviceException);

            }

            return environments;

        }


        #region Environment Roles

        public List<String> EnvironmentRoleListGet (String environmentName) {

            Mercury.Server.Application.StringListResponse response = new Mercury.Server.Application.StringListResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.EnvironmentRoleList (session.Token, environmentName);

                if (response.HasException) { SetLastException (new ApplicationException (response.Exception.Message)); }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return response.ResultList.ToList<String> ();

        }

        public List<String> EnvironmentRoleListGet () {

            return EnvironmentRoleListGet (session.EnvironmentName);

        }

        public Dictionary<Int64, String> EnvironmentRoleDictionaryGet (String environmentName) {

            List<Mercury.Server.Application.EnvironmentRole> availableRoles = new List<Server.Application.EnvironmentRole> ();

            Dictionary<Int64, String> roleDictionary = new Dictionary<Int64, String> ();

            Mercury.Server.Application.StringListResponse response = new Mercury.Server.Application.StringListResponse ();

            ClearLastException ();

            try {

                availableRoles = EnvironmentRolesAvailable (environmentName);

                foreach (Server.Application.EnvironmentRole currentRole in availableRoles) {

                    roleDictionary.Add (currentRole.RoleId, currentRole.Name);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return roleDictionary;

        }

        public Dictionary<Int64, String> EnvironmentRoleDictionaryGet () {

            return EnvironmentRoleDictionaryGet (session.EnvironmentName);

        }

        public List<Mercury.Server.Application.EnvironmentRole> EnvironmentRolesGet (String environmentName) {

            List<Mercury.Server.Application.EnvironmentRole> roles = new List<Mercury.Server.Application.EnvironmentRole> ();

            Mercury.Server.Application.RoleCollectionResponse response = new Mercury.Server.Application.RoleCollectionResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.EnvironmentRolesAvailable(session.Token, environmentName);

                for (Int32 currentRoleIndex = 0; currentRoleIndex < response.Collection.Length; currentRoleIndex++) {

                    roles.Add (response.Collection[currentRoleIndex]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return roles;

        }

        public List<Mercury.Server.Application.EnvironmentRole> EnvironmentRolesGet () {

            return EnvironmentRolesGet (session.EnvironmentName);

        }

        public Environment.Role EnvironmentRoleGet (String environmentName, String roleName) {

            Environment.Role environmentRole = null;

            Mercury.Server.Application.EnvironmentRole serverRole = null;

            ClearLastException ();

            try {

                serverRole = ApplicationClient.EnvironmentRoleGetByEnvironment (session.Token, environmentName, roleName);

                if (serverRole != null) { environmentRole = new Mercury.Client.Environment.Role (this, serverRole); }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return environmentRole;

        }

        public Boolean EnvironmentRoleSave (String environmentName, Mercury.Server.Application.EnvironmentRole environmentRole) {

            Mercury.Server.Application.ObjectSaveResponse response = new Mercury.Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.EnvironmentRoleSaveByEnvironment (session.Token, environmentName, environmentRole);

                if (response.HasException) {

                    SetLastException (new ApplicationException (response.Exception.Message));

                }

                else { if (environmentRole.RoleId == 0) { environmentRole.RoleId = response.Id; } }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return response.Success;

        }

        #endregion 


        #region Environment Permissions

        public List<String> GetEnvironmentPermissionList (String environmentName) {

            Mercury.Server.Application.StringListResponse response = new Mercury.Server.Application.StringListResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.EnvironmentPermissionList (session.Token, environmentName);

                if (response.HasException) { SetLastException (new ApplicationException (response.Exception.Message)); }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return response.ResultList.ToList<String> ();

        }

        public Dictionary<Int64, String> EnvironmentPermissionDictionary (String environmentName) {

            List<Mercury.Server.Application.Permission> environmentPermissions;

            Dictionary<Int64, String> permissions = new Dictionary<Int64, String> ();

            ClearLastException ();

            try {

                environmentPermissions = EnvironmentPermissionsAvailable (environmentName);

                foreach (Mercury.Server.Application.Permission currentPermission in environmentPermissions) {

                    permissions.Add (currentPermission.Id, currentPermission.Name);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return permissions;

        }

        public List<Mercury.Server.Application.Permission> EnvironmentPermissionsAvailable (String environmentName) {

            Mercury.Server.Application.PermissionCollectionResponse response;

            List<Mercury.Server.Application.Permission> environmentPermissions = new List<Mercury.Server.Application.Permission> ();

            ClearLastException ();

            try {

                response = ApplicationClient.EnvironmentPermissionsAvailable (session.Token, environmentName);

                for (Int32 currentPermissionIndex = 0; currentPermissionIndex < response.Collection.Length; currentPermissionIndex++) {

                    environmentPermissions.Add (response.Collection[currentPermissionIndex]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return environmentPermissions;

        }

        #endregion 

        #endregion


        #region Environments - Roles and Permissions

        public List<String> EnvironmentRoleList (String environmentName) {

            Mercury.Server.Application.StringListResponse response = new Mercury.Server.Application.StringListResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.EnvironmentRoleList (session.Token, environmentName);

                if (response.HasException) { SetLastException (new ApplicationException (response.Exception.Message)); }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return response.ResultList.ToList<String> ();

        }

        public List<String> EnvironmentRoleList () {

            return EnvironmentRoleList (session.EnvironmentName);

        }

        public Dictionary<Int64, String> EnvironmentRoleDictionary (String environmentName) {

            Dictionary<Int64, String> roleDictionary = new Dictionary<Int64, String> ();

            Mercury.Server.Application.DictionaryResponse response = new Mercury.Server.Application.DictionaryResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.EnvironmentRoleDictionary (session.Token, environmentName);

                if (!response.HasException) { roleDictionary = response.Dictionary; }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return roleDictionary;

        }

        public Dictionary<Int64, String> EnvironmentRoleDictionary () {

            return EnvironmentRoleDictionary (session.EnvironmentName);

        }


        public List<Mercury.Server.Application.EnvironmentRole> EnvironmentRolesAvailable (String environmentName) {

            List<Mercury.Server.Application.EnvironmentRole> roles = new List<Mercury.Server.Application.EnvironmentRole> ();

            Mercury.Server.Application.RoleCollectionResponse response = new Mercury.Server.Application.RoleCollectionResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.EnvironmentRolesAvailable (session.Token, environmentName);

                for (Int32 currentRoleIndex = 0; currentRoleIndex < response.Collection.Length; currentRoleIndex++) {

                    roles.Add (response.Collection[currentRoleIndex]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return roles;

        }

        public List<Mercury.Server.Application.EnvironmentRole> EnvironmentRolesAvailable() {

            return EnvironmentRolesAvailable (session.EnvironmentName);

        }


        public List<Mercury.Server.Application.EnvironmentRolePermission> EnvironmentRoleGetPermissions (String environmentName, String roleName) {

            List<Mercury.Server.Application.EnvironmentRolePermission> permissions = new List<Mercury.Server.Application.EnvironmentRolePermission> ();

            Mercury.Server.Application.RolePermissionCollectionResponse response;

            ClearLastException ();

            try {

                response = ApplicationClient.EnvironmentRoleGetPermissions (session.Token, environmentName, roleName);

                permissions.AddRange (response.Collection);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return permissions;

        }

        public List<Mercury.Server.Application.EnvironmentRoleMembership> EnvironmentRoleGetMembership (String environmentName, String roleName) {

            List<Mercury.Server.Application.EnvironmentRoleMembership> membership = new List<Mercury.Server.Application.EnvironmentRoleMembership> ();

            Mercury.Server.Application.RoleMembershipCollectionResponse response;

            ClearLastException ();

            try {

                response = ApplicationClient.EnvironmentRoleGetMembership (session.Token, environmentName, roleName);

                membership.AddRange (response.Collection);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return membership;

        }


        public Boolean EnvironmentRoleSetPermission (String environmentName, String roleName, Int64 permissionId, Boolean isGranted, Boolean isDenied) {

            Mercury.Server.Application.BooleanResponse response = new Mercury.Server.Application.BooleanResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.EnvironmentRoleSetPermission (session.Token, environmentName, roleName, permissionId, isGranted, isDenied);

                if (response.HasException) {

                    throw new ApplicationException (response.Exception.Message);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

                response.Result = false;

            }

            return response.Result;

        }

        public Boolean EnvironmentRoleSetMembership (String environmentName, String roleName, List<Mercury.Server.Application.EnvironmentRoleMembership> roleMembership) {

            Mercury.Server.Application.BooleanResponse response = new Mercury.Server.Application.BooleanResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.EnvironmentRoleSetMembership (session.Token, environmentName, roleName, roleMembership.ToArray ());

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

            }

            catch (Exception applicationExcpetion) {

                SetLastException (applicationExcpetion);

                response.Result = false;

            }

            return response.Result;

        }

        #endregion


        #region Core Reference

        #region Core Reference - Contact Regarding

        public List<Core.Reference.ContactRegarding> ContactRegardingsAvailable (Boolean useCaching) {

            List<Core.Reference.ContactRegarding> contactRegardings = new List<Core.Reference.ContactRegarding> ();

            Mercury.Server.Application.ContactRegardingCollectionResponse response;

            String cacheKey = "Application." + session.EnvironmentId + ".ContactRegarding.Available";


            ClearLastException ();

            try {

                contactRegardings = (List<Core.Reference.ContactRegarding>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { contactRegardings = null; }

                if (contactRegardings == null) {

                    contactRegardings = new List<Core.Reference.ContactRegarding> ();

                    response = ApplicationClient.ContactRegardingsAvailable (session.Token);

                    if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                    foreach (Server.Application.ContactRegarding currentServerContactRegarding in response.Collection) {

                        Core.Reference.ContactRegarding contactRegarding = new Core.Reference.ContactRegarding (this, currentServerContactRegarding);

                        contactRegardings.Add (contactRegarding);

                    }

                    if (contactRegardings.Count > 0) { cacheManager.CacheObject (cacheKey, contactRegardings, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }




            return contactRegardings;

        }


        public Core.Reference.ContactRegarding ContactRegardingGet (Int64 contactRegardingId, Boolean useCaching) {

            if (contactRegardingId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + "." + typeof (Core.Reference.ContactRegarding).ToString () + "." + contactRegardingId.ToString ();

            Core.Reference.ContactRegarding contactRegarding = null;

            ClearLastException ();


            try {

                contactRegarding = (Core.Reference.ContactRegarding)cacheManager.GetObject (cacheKey);

                if (!useCaching) { contactRegarding = null; }

                if (contactRegarding == null) {

                    Server.Application.ContactRegarding serverContactRegarding = ApplicationClient.ContactRegardingGet (session.Token, contactRegardingId);

                    if (serverContactRegarding != null) { contactRegarding = new Core.Reference.ContactRegarding (this, serverContactRegarding); }

                    if (contactRegarding != null) { cacheManager.CacheObject (cacheKey, contactRegarding, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return contactRegarding;

        }

        public Core.Reference.ContactRegarding ContactRegardingGet (String contactRegardingName, Boolean useCaching) {

            if (String.IsNullOrWhiteSpace (contactRegardingName)) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + "." + typeof (Core.Reference.ContactRegarding).ToString () + "." + contactRegardingName.ToString ();

            Core.Reference.ContactRegarding contactRegarding = null;

            ClearLastException ();


            try {

                contactRegarding = (Core.Reference.ContactRegarding)cacheManager.GetObject (cacheKey);

                if (!useCaching) { contactRegarding = null; }

                if (contactRegarding == null) {

                    Server.Application.ContactRegarding serverContactRegarding = ApplicationClient.ContactRegardingGetByName (session.Token, contactRegardingName);

                    if (serverContactRegarding != null) { contactRegarding = new Core.Reference.ContactRegarding (this, serverContactRegarding); }

                    if (contactRegarding != null) { cacheManager.CacheObject (cacheKey, contactRegarding, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return contactRegarding;

        }

        public Boolean ContactRegardingSave (Core.Reference.ContactRegarding contactRegarding) {
            
            String cacheKey = "Application." + session.EnvironmentId + "." + typeof (Core.Reference.ContactRegarding).ToString () + "." + contactRegarding.Id.ToString ();

            Server.Application.ObjectSaveResponse response = new Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                cacheManager.RemoveObject (cacheKey); // FLUSH CACHE FOR SAVE

                response = ApplicationClient.ContactRegardingSave (session.Token, (Server.Application.ContactRegarding)contactRegarding.ToServerObject ());


                if (!response.HasException) { contactRegarding.SetId (response.Id); }

                else { SetLastException (new ApplicationException (response.Exception.Message)); }

            }

            catch (Exception applicationException) {

                response.Success = false;

                SetLastException (applicationException);

            }

            return response.Success;

        }

        #endregion


        #region Core Reference - Correspondence

        public List<Core.Reference.Correspondence> CorrespondencesAvailable (Boolean useCaching) {

            List<Core.Reference.Correspondence> correspondences = new List<Core.Reference.Correspondence> ();

            Mercury.Server.Application.CorrespondenceCollectionResponse response;

            String cacheKey = "Application." + session.EnvironmentId + ".Correspondence.Available";


            ClearLastException ();

            try {

                correspondences = (List<Core.Reference.Correspondence>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { correspondences = null; }

                if (correspondences == null) {

                    correspondences = new List<Core.Reference.Correspondence> ();

                    response = ApplicationClient.CorrespondencesAvailable (session.Token);

                    if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                    foreach (Server.Application.Correspondence currentServerCorrespondence in response.Collection) {

                        Core.Reference.Correspondence correspondence = new Core.Reference.Correspondence (this, currentServerCorrespondence);

                        correspondences.Add (correspondence);

                    }

                    if (correspondences.Count > 0) { cacheManager.CacheObject (cacheKey, correspondences, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }




            return correspondences;

        }


        public Core.Reference.Correspondence CorrespondenceGet (Int64 correspondenceId, Boolean useCaching) {

            if (correspondenceId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".Correspondence." + correspondenceId.ToString ();

            Core.Reference.Correspondence correspondence = null;

            ClearLastException ();


            try {

                correspondence = (Core.Reference.Correspondence)cacheManager.GetObject (cacheKey);

                if (!useCaching) { correspondence = null; }

                if (correspondence == null) {

                    Server.Application.Correspondence serverCorrespondence = ApplicationClient.CorrespondenceGet (session.Token, correspondenceId);

                    if (serverCorrespondence != null) { correspondence = new Core.Reference.Correspondence (this, serverCorrespondence); }

                    if (correspondence != null) { cacheManager.CacheObject (cacheKey, correspondence, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return correspondence;

        }

        public Core.Reference.Correspondence CorrespondenceGet (String correspondenceName, Boolean useCaching) {

            if (String.IsNullOrWhiteSpace (correspondenceName)) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".Correspondence." + correspondenceName.ToString ();

            Core.Reference.Correspondence correspondence = null;

            ClearLastException ();


            try {

                correspondence = (Core.Reference.Correspondence)cacheManager.GetObject (cacheKey);

                if (!useCaching) { correspondence = null; }

                if (correspondence == null) {

                    Server.Application.Correspondence serverCorrespondence = ApplicationClient.CorrespondenceGetByName (session.Token, correspondenceName);

                    if (serverCorrespondence != null) { correspondence = new Core.Reference.Correspondence (this, serverCorrespondence); }

                    if (correspondence != null) { cacheManager.CacheObject (cacheKey, correspondence, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return correspondence;

        }

        public Boolean CorrespondenceSave (Core.Reference.Correspondence correspondence) {

            String cacheKey = "Application." + session.EnvironmentId + ".Correspondence." + correspondence.Id.ToString ();

            Server.Application.ObjectSaveResponse response = new Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                cacheManager.RemoveObject (cacheKey); // FLUSH CACHE FOR SAVE

                response = ApplicationClient.CorrespondenceSave (session.Token, (Server.Application.Correspondence)correspondence.ToServerObject ());


                if (!response.HasException) { correspondence.SetId (response.Id); }

                else { SetLastException (new ApplicationException (response.Exception.Message)); }

            }

            catch (Exception applicationException) {

                response.Success = false;

                SetLastException (applicationException);

            }

            return response.Success;

        }


        public Core.Reference.CorrespondenceContent CorrespondenceContentGet (Int64 correspondenceContentId, Boolean useCaching) {

            if (correspondenceContentId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".CorrespondenceContent." + correspondenceContentId.ToString ();

            Core.Reference.CorrespondenceContent correspondenceContent = null;

            ClearLastException ();


            try {

                correspondenceContent = (Core.Reference.CorrespondenceContent)cacheManager.GetObject (cacheKey);

                if (!useCaching) { correspondenceContent = null; }

                if (correspondenceContent == null) {

                    Server.Application.CorrespondenceContent serverCorrespondenceContent = ApplicationClient.CorrespondenceContentGet (session.Token, correspondenceContentId);

                    if (serverCorrespondenceContent != null) { 
                        
                        correspondenceContent = new Core.Reference.CorrespondenceContent (this, serverCorrespondenceContent);

                        correspondenceContent.IsAttachmentLoaded = true;
                    
                    }

                    if (correspondenceContent != null) { cacheManager.CacheObject (cacheKey, correspondenceContent, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return correspondenceContent;

        }

        #endregion


        #region Core Reference - Note Type

        public List<Core.Reference.NoteType> NoteTypesAvailable (Boolean useCaching) {

            List<Core.Reference.NoteType> noteTypes = new List<Core.Reference.NoteType> ();

            Mercury.Server.Application.NoteTypeCollectionResponse response;

            String cacheKey = "Application." + session.EnvironmentId + ".NoteType.Available";

            Dictionary<Int64, String> coreObjectDictionary = new Dictionary<Int64, String> ();


            ClearLastException ();

            try {

                noteTypes = (List<Core.Reference.NoteType>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { noteTypes = null; }

                if (noteTypes == null) {

                    noteTypes = new List<Core.Reference.NoteType> ();

                    response = ApplicationClient.NoteTypesAvailable (session.Token);

                    if (!response.HasException) {

                        foreach (Server.Application.NoteType currentServerNoteType in response.Collection) {

                            Core.Reference.NoteType noteType = new Core.Reference.NoteType (this, currentServerNoteType);

                            noteTypes.Add (noteType);


                            // CACHE EACH INDIVIDUAL WORK QUEUE LOADED FROM SERVER (BY ID AND NAME)

                            coreObjectDictionary.Add (noteType.Id, noteType.Name);

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".NoteType." + noteType.Id.ToString (), noteType, CacheExpirationData);

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".NoteType." + noteType.Name, noteType, CacheExpirationData);

                        }

                        if (noteTypes.Count > 0) {

                            cacheManager.CacheObject (cacheKey, noteTypes, CacheExpirationData);

                            // CACHE THE AVAILABILITY LIST

                            cacheManager.CacheObject (cacheKey, noteTypes, CacheExpirationData);

                            // CACHE THE DICTIONARY THAT WAS CREATED

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".WorkQueue.Dictionary", coreObjectDictionary, CacheExpirationData);

                        }

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }




            return noteTypes;

        }


        public Core.Reference.NoteType NoteTypeGet (Int64 noteTypeId, Boolean useCaching) {

            if (noteTypeId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + "." + typeof (Core.Reference.NoteType).ToString () + "." + noteTypeId.ToString ();

            Core.Reference.NoteType noteType = null;

            ClearLastException ();


            try {

                noteType = (Core.Reference.NoteType)cacheManager.GetObject (cacheKey);

                if (!useCaching) { noteType = null; }

                if (noteType == null) {

                    Server.Application.NoteType serverNoteType = ApplicationClient.NoteTypeGet (session.Token, noteTypeId);

                    if (serverNoteType != null) { noteType = new Core.Reference.NoteType (this, serverNoteType); }

                    if (noteType != null) { cacheManager.CacheObject (cacheKey, noteType, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return noteType;

        }

        public Core.Reference.NoteType NoteTypeGet (String noteTypeName, Boolean useCaching) {

            if (String.IsNullOrWhiteSpace (noteTypeName)) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + "." + typeof (Core.Reference.NoteType).ToString () + "." + noteTypeName.ToString ();

            Core.Reference.NoteType noteType = null;

            ClearLastException ();


            try {

                noteType = (Core.Reference.NoteType)cacheManager.GetObject (cacheKey);

                if (!useCaching) { noteType = null; }

                if (noteType == null) {

                    Server.Application.NoteType serverNoteType = ApplicationClient.NoteTypeGetByName (session.Token, noteTypeName);

                    if (serverNoteType != null) { noteType = new Core.Reference.NoteType (this, serverNoteType); }

                    if (noteType != null) { cacheManager.CacheObject (cacheKey, noteType, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return noteType;

        }

        public Boolean NoteTypeSave (Core.Reference.NoteType noteType) {

            String cacheKey = "Application." + session.EnvironmentId + "." + typeof (Core.Reference.NoteType).ToString () + "." + noteType.Id.ToString ();

            Server.Application.ObjectSaveResponse response = new Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                cacheManager.RemoveObject (cacheKey);

                response = ApplicationClient.NoteTypeSave (session.Token, (Server.Application.NoteType)noteType.ToServerObject ());


                // REMOVE CACHING FOR ALL RELATED OBJECTS

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".NoteType.Available");

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".NoteType.Dictionary");

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".NoteType." + noteType.Id.ToString ());

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".NoteType." + noteType.Name);


                if (!response.HasException) { noteType.SetId (response.Id); }

                else { SetLastException (new ApplicationException (response.Exception.Message)); }

            }

            catch (Exception applicationException) {

                response.Success = false;

                SetLastException (applicationException);

            }

            return response.Success;

        }

        #endregion


        #region Geographic Reference

        public List<String> StateReference (Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".Core.StateReference.";


            List<String> referenceData = new List<String> ();

            Server.Application.StringListResponse response = new Server.Application.StringListResponse ();


            ClearLastException ();

            try {

                referenceData = (List<String>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { referenceData = null; }

                if (referenceData == null) {

                    referenceData = new List<String> ();

                    response = ApplicationClient.StateReference (session.Token);

                    if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                    referenceData.AddRange (response.ResultList);

                    cacheManager.CacheObject (cacheKey, referenceData, CacheExpirationReference);

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return referenceData;

        }

        public String StateReferenceByZipCode (String zipCode) {

            String referenceData = String.Empty;

            ClearLastException ();

            try {

                referenceData = ApplicationClient.StateReferenceByZipCode (session.Token, zipCode);

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return referenceData;

        }

        public List<Server.Application.CityStateZipCodeView> CityReferenceByState (String state, Boolean useCaching) {

            if (String.IsNullOrWhiteSpace (state)) { return new List<Server.Application.CityStateZipCodeView> (); }


            String cacheKey = "Application." + session.EnvironmentId + ".CityReferenceByState." + state;

            List<Server.Application.CityStateZipCodeView> referenceData = new List<Server.Application.CityStateZipCodeView> ();
            
            Server.Application.CityStateZipCodeViewCollectionResponse response = new Server.Application.CityStateZipCodeViewCollectionResponse (); 



            ClearLastException ();

            try {

                referenceData = (List<Server.Application.CityStateZipCodeView>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { referenceData = null; }

                if (referenceData == null) {

                    referenceData = new List<Server.Application.CityStateZipCodeView> ();

                    response = ApplicationClient.CityReferenceByState (session.Token, state);

                    if (!response.HasException) {

                        referenceData.AddRange (response.Collection);

                        if (referenceData.Count > 0) {

                            cacheManager.CacheObject (cacheKey, referenceData, CacheExpirationReference);

                        }

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return referenceData;

        }

        public List<String> CityReferenceByState (String state, String cityName) {

            List<String> referenceData = new List<String> ();

            Server.Application.StringListResponse response = new Server.Application.StringListResponse ();


            ClearLastException ();

            try {

                response = ApplicationClient.CityReferenceByStateCityName (session.Token, state, cityName);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                referenceData.AddRange (response.ResultList);

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return referenceData;

        }

        public String CityReferenceByZipCode (String zipCode) {

            String referenceData = String.Empty;

            ClearLastException ();

            try {

                referenceData = ApplicationClient.CityReferenceByZipCode (session.Token, zipCode);

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return referenceData;

        }

        public List<String> CountyReferenceByState (String state) {

            List<String> referenceData = new List<String> ();

            Server.Application.StringListResponse response = new Server.Application.StringListResponse ();


            ClearLastException ();

            try {

                response = ApplicationClient.CountyReferenceByState (session.Token, state);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                referenceData.AddRange (response.ResultList);

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return referenceData;

        }

        public String CountyReferenceByZipCode (String zipCode) {

            String referenceData = String.Empty;


            ClearLastException ();

            try {

                referenceData = ApplicationClient.CountyReferenceByZipCode (session.Token, zipCode);

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return referenceData;

        }

        #endregion 

        #endregion


        #region References - Old

        public System.Collections.Generic.Dictionary<String, String> DiagnosisDictionary (String diagnosisPrefix, Int32 version = 9) {

            String cacheKey = "Application." + session.EnvironmentId + ".Core.Reference.DiagnosisDictionary." + diagnosisPrefix;

            System.Collections.Generic.Dictionary<String, String> collection = new Dictionary<String, String> ();


            ClearLastException ();

            try {

                collection = (System.Collections.Generic.Dictionary<String, String>)cacheManager.GetObject (cacheKey);

                if (collection == null) {

                    collection = ApplicationClient.DiagnosisDictionary (session.Token, diagnosisPrefix, version);

                    cacheManager.CacheObject (cacheKey, collection, CacheExpirationData);

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return collection;

        }

        public String DiagnosisDescription (String diagnosisCode, Int32 version = 9) {

            if (String.IsNullOrEmpty (diagnosisCode.Trim ())) { return String.Empty; }

            String description = String.Empty;

            String diagnosisLookupPrefix = diagnosisCode.Trim ();

            if (diagnosisLookupPrefix.Length > 3) {

                diagnosisLookupPrefix = diagnosisLookupPrefix.Substring (0, 3);

            }

            System.Collections.Generic.Dictionary<String, String> codes = DiagnosisDictionary (diagnosisLookupPrefix, version);

            if (codes.ContainsKey (diagnosisCode.Trim ())) {

                description = codes[diagnosisCode.Trim ()];

            }

            return description;

        }

        public System.Collections.Generic.Dictionary<String, String> ProcedureCodeDictionary (String procedureCodePrefix) {

            String cacheKey = "Application." + session.EnvironmentId + ".Core.Reference.ProcedureCodeDictionary." + procedureCodePrefix;
            
            System.Collections.Generic.Dictionary<String, String> collection = new Dictionary<String, String> ();


            ClearLastException ();

            try {

                collection = (System.Collections.Generic.Dictionary<String, String>)cacheManager.GetObject (cacheKey);

                if (collection == null) {

                    collection = ApplicationClient.ProcedureCodeDictionary (session.Token, procedureCodePrefix);

                    cacheManager.CacheObject (cacheKey, collection, CacheExpirationReference);

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return collection;

        }

        public String ProcedureCodeDescription (String procedureCode) {

            if (String.IsNullOrEmpty (procedureCode.Trim ())) { return String.Empty; }

            String description = String.Empty;

            String procedureCodeLookupPrefix = procedureCode.Trim ();

            if (procedureCodeLookupPrefix.Length > 3) {

                procedureCodeLookupPrefix = procedureCodeLookupPrefix.Substring (0, 3);

            }

            System.Collections.Generic.Dictionary<String, String> codes = ProcedureCodeDictionary (procedureCodeLookupPrefix);

            if (codes.ContainsKey (procedureCode.Trim ())) {

                description = codes[procedureCode.Trim ()];

            }

            return description;

        }

        public System.Collections.Generic.Dictionary<String, String> RevenueCodeDictionary (String revenueCodePrefix) {

            String cacheKey = "Application." + session.EnvironmentId + ".Core.Reference.RevenueCodeDictionary." + revenueCodePrefix; 

            System.Collections.Generic.Dictionary<String, String> collection = new Dictionary<String, String> ();


            ClearLastException ();

            try {

                collection = (System.Collections.Generic.Dictionary<String, String>)cacheManager.GetObject (cacheKey);

                if (collection == null) {

                    collection = ApplicationClient.RevenueCodeDictionary (session.Token, revenueCodePrefix);

                    cacheManager.CacheObject (cacheKey, collection, CacheExpirationReference);

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return collection;

        }

        public String RevenueCodeDescription (String revenueCode) {

            if (String.IsNullOrEmpty (revenueCode.Trim ())) { return String.Empty; }

            String description = String.Empty;

            String revenueCodeLookupPrefix = revenueCode.Trim ();

            if (revenueCodeLookupPrefix.Length > 2) {

                revenueCodeLookupPrefix = revenueCodeLookupPrefix.Substring (0, 2);

            }

            System.Collections.Generic.Dictionary<String, String> codes = RevenueCodeDictionary (revenueCodeLookupPrefix);

            if (codes.ContainsKey (revenueCode.Trim ())) {

                description = codes[revenueCode.Trim ()];

            }

            return description;

        }

        public System.Collections.Generic.Dictionary<String, String> BillTypeDictionary (String billTypePrefix) {

            String cacheKey = "Application." + session.EnvironmentId + ".Core.Reference.BillTypeDictionary." + billTypePrefix;

            System.Collections.Generic.Dictionary<String, String> collection = new Dictionary<String, String> ();


            ClearLastException ();

            try {

                collection = (System.Collections.Generic.Dictionary<String, String>)cacheManager.GetObject (cacheKey);

                if (collection == null) {

                    collection = ApplicationClient.BillTypeDictionary (session.Token, billTypePrefix);

                    cacheManager.CacheObject (cacheKey, collection, CacheExpirationReference);

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return collection;

        }

        public String BillTypeDescription (String billType) {

            if (String.IsNullOrEmpty (billType.Trim ())) { return String.Empty; }

            String description = String.Empty;

            System.Collections.Generic.Dictionary<String, String> codes = BillTypeDictionary (String.Empty);

            if (codes.ContainsKey (billType.Trim ())) {

                description = codes[billType.Trim ()];

            }

            return description;

        }

        public System.Collections.Generic.Dictionary<String, String> Icd9ProcedureCodeDictionary (String icd9Icd9ProcedureCodePrefix) {

            String cacheKey = "Application." + session.EnvironmentId + ".Core.Reference.Icd9ProcedureCodeDictionary." + icd9Icd9ProcedureCodePrefix;

            System.Collections.Generic.Dictionary<String, String> collection = new Dictionary<String, String> ();


            ClearLastException ();

            try {

                collection = (System.Collections.Generic.Dictionary<String, String>)cacheManager.GetObject (cacheKey);

                if (collection == null) {

                    collection = ApplicationClient.Icd9ProcedureCodeDictionary (session.Token, icd9Icd9ProcedureCodePrefix);

                    cacheManager.CacheObject (cacheKey, collection, CacheExpirationReference);

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return collection;

        }

        public String Icd9ProcedureCodeDescription (String icd9Icd9ProcedureCode) {

            if (String.IsNullOrEmpty (icd9Icd9ProcedureCode.Trim ())) { return String.Empty; }

            String description = String.Empty;

            String icd9Icd9ProcedureCodeLookupPrefix = icd9Icd9ProcedureCode.Trim ();

            if (icd9Icd9ProcedureCodeLookupPrefix.Length > 3) {

                icd9Icd9ProcedureCodeLookupPrefix = icd9Icd9ProcedureCodeLookupPrefix.Substring (0, 3);

            }

            System.Collections.Generic.Dictionary<String, String> codes = Icd9ProcedureCodeDictionary (icd9Icd9ProcedureCodeLookupPrefix);

            if (codes.ContainsKey (icd9Icd9ProcedureCode.Trim ())) {

                description = codes[icd9Icd9ProcedureCode.Trim ()];

            }

            return description;

        }

        #endregion 


        #region Core Objects

        public Dictionary<Int64, String> CoreObjectDictionary (String forObjectType, Boolean useCaching) {

            // String cacheKey = "Application." + session.EnvironmentId + "." + forObjectType + ".Dictionary";

            String cacheKey = "Application." + "removethislater" + "." + forObjectType + ".Dictionary";

            Dictionary<Int64, String> objectDictionary = new Dictionary<long, string> ();

            Server.Application.DictionaryResponse response;

            ClearLastException ();

            try {

                objectDictionary = (Dictionary<Int64, String>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { objectDictionary = null; }

                if (objectDictionary == null) {

                    objectDictionary = new Dictionary<Int64, String> ();

                    response = ApplicationClient.CoreObjectDictionary (session.Token, forObjectType);

                    if (!response.HasException) { 
                        
                        objectDictionary = response.Dictionary;

                        if (objectDictionary.Keys.Count > 0) {

                            TimeSpan cacheExpirationTime = CacheExpirationData; // DEFAULT TO THE DATA EXPIRATION TIME

                            switch (forObjectType) { // SELECT THE REFERENCE EXPIRATION FOR REFERENCE DATA TYPES

                                case "Ethnicity":

                                case "Language":

                                case "MaritalStatus":

                                    cacheExpirationTime = CacheExpirationReference;

                                    break;

                            }

                            cacheManager.CacheObject (cacheKey, objectDictionary, cacheExpirationTime);

                        }
                    
                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return objectDictionary;

        }

        public Dictionary<Int64, String> CoreObjectDictionary (String forObjectType) { return CoreObjectDictionary (forObjectType, true); } // MATCH WITH SERVER-SIDE FUNCTION

        public Int64 CoreObjectGetIdByName (String forObjectType, String forObjectName, Boolean useCaching) {

            if (String.IsNullOrWhiteSpace (forObjectName)) { return 0; }

            Int64 objectId = 0;

            Dictionary<Int64, String> objectDictionary = CoreObjectDictionary (forObjectType, useCaching);


            foreach (Int64 currentObjectId in objectDictionary.Keys) {

                if (objectDictionary[currentObjectId] == forObjectName) {

                    objectId = currentObjectId;

                    break;

                }

            }

            return objectId;

        }

        public Int64 CoreObjectGetIdByName (String forObjectType, String forObjectName) { return CoreObjectGetIdByName (forObjectType, forObjectName, true); } // MATCH WITH SERVER-SIDE FUNCTION

        public String CoreObjectGetNameById (String forObjectType, Int64 forObjectId, Boolean useCaching) {

            if (forObjectId == 0) { return String.Empty; }

            String objectName = String.Empty;

            Dictionary<Int64, String> objectDictionary = CoreObjectDictionary (forObjectType, useCaching);


            foreach (Int64 currentObjectId in objectDictionary.Keys) {

                if (currentObjectId == forObjectId) {

                    objectName = objectDictionary[currentObjectId];

                    break;

                }

            }

            return objectName;

        }

        public String CoreObjectGetNameById (String forObjectType, Int64 forObjectId) { return CoreObjectGetNameById (forObjectType, forObjectId, true); } // MATCH WITH SERVER-SIDE FUNCTION


        public Dictionary<String, String> CoreObject_Validate (Server.Application.CoreObject coreObject) {

            Dictionary<String, String> validationMessage = new Dictionary<String, String> ();

            Server.Application.DictionaryStringResponse response;

            ClearLastException ();

            try {

                response = ApplicationClient.CoreObject_Validate (session.Token, coreObject);

                if (!response.HasException) { validationMessage = response.Dictionary; }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return validationMessage;

        }

        public Dictionary<String, String> CoreConfigurationObject_Validate (Server.Application.CoreConfigurationObject coreConfigurationObject) {

            Dictionary<String, String> validationMessage = new Dictionary<String, String> ();

            Server.Application.DictionaryStringResponse response;

            ClearLastException ();

            try {

                response = ApplicationClient.CoreConfigurationObject_Validate (session.Token, coreConfigurationObject);

                if (!response.HasException) { validationMessage = response.Dictionary; }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return validationMessage;

        }

        public Dictionary<String, String> CoreObject_DataBindingContexts (Server.Application.CoreObject coreObject, Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".CoreObject.DataBindingContexts." +coreObject.GetType () + "." + coreObject.Id.ToString ();

            Dictionary<String, String> bindingContexts = new Dictionary<String, String> ();

            Server.Application.DictionaryStringResponse response;

            ClearLastException ();

            try {

                bindingContexts = (Dictionary<String, String>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { bindingContexts = null; }

                if (bindingContexts == null) {

                    bindingContexts = new Dictionary<String, String> ();

                    response = ApplicationClient.CoreObject_DataBindingContexts (session.Token, coreObject);

                    if (!response.HasException) {

                        bindingContexts = response.Dictionary;

                        cacheManager.CacheObject (cacheKey, bindingContexts, CacheExpirationData);

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return bindingContexts;

        }

        public String CoreObject_EvaluateDataBinding (Server.Application.CoreObject coreObject, String bindingContext) {

            Server.Application.StringResponse response = new Server.Application.StringResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.CoreObject_EvaluateDataBinding (session.Token, coreObject, bindingContext);

                if (response.HasException) {

                    response.Value = "!EXCEPTION";

                    SetLastException (response.Exception);

                }

            }

            catch (Exception applicationException) {

                response.Value = "!EXCEPTION";

                SetLastException (applicationException);

            }

            return response.Value;

        }

        public System.Xml.XmlDocument CoreObject_XmlSerialize (Server.Application.CoreObject coreObject) {

            System.Xml.XmlDocument serializedObject = null;

            Server.Application.StringResponse response;

            ClearLastException ();

            try {

                response = ApplicationClient.CoreObject_XmlSerialize (session.Token, coreObject);

                if (!response.HasException) {

                    serializedObject = new System.Xml.XmlDocument ();

                    serializedObject.LoadXml (response.Value);

                }

                else { SetLastException (response.Exception); }


            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return serializedObject;

        }

        public Server.Application.ImportExportResponse CoreObject_XmlImport (String serializedObject) {

            Server.Application.ImportExportResponse response = new Server.Application.ImportExportResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.CoreObject_XmlImport (session.Token, serializedObject);

                if (response.HasException) { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return response;

        }

        #endregion


        #region Core - Actions
        
        public List<Mercury.Server.Application.Action> ActionsAvailable (Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".Action.Available";

            Mercury.Server.Application.ActionCollectionResponse response = new Mercury.Server.Application.ActionCollectionResponse ();

            List<Mercury.Server.Application.Action> actions = new List<Server.Application.Action> ();


            ClearLastException ();

            try {

                actions = (List<Mercury.Server.Application.Action>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { actions = null; }

                if (actions == null) {

                    response = ApplicationClient.ActionsAvailable (session.Token);

                    if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                    actions = new List<Mercury.Server.Application.Action> (response.Collection);

                    if (actions.Count > 0) { cacheManager.CacheObject (cacheKey, actions, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return actions;

        }

        public Core.Action.Action ActionById (Int64 actionId) {

            List<Mercury.Server.Application.Action> availableActions = ActionsAvailable (true);

            Core.Action.Action action = null;


            foreach (Mercury.Server.Application.Action currentAction in availableActions) {

                if (currentAction.Id == actionId) { action = new Mercury.Client.Core.Action.Action (this, currentAction); break; }

            }

            return action;

        }

        #endregion


        #region Core - Authorizations

        public List<Core.Authorizations.AuthorizationType> AuthorizationTypesAvailable (Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".Core.AuthorizationType.Available.";


            List<Core.Authorizations.AuthorizationType> result = new List<Core.Authorizations.AuthorizationType> ();

            Server.Application.AuthorizationTypeCollectionResponse response = new Server.Application.AuthorizationTypeCollectionResponse ();

            ClearLastException ();

            try {

                result = (List<Core.Authorizations.AuthorizationType>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { result = null; }

                if (result == null) { 

                    response = ApplicationClient.AuthorizationTypesAvailable (session.Token);

                    if (!response.HasException) {

                        result = new List<Core.Authorizations.AuthorizationType> ();

                        foreach (Server.Application.AuthorizationType serverAuthorizationType in response.Collection) {

                            Client.Core.Authorizations.AuthorizationType authorizationType = new Mercury.Client.Core.Authorizations.AuthorizationType (this, serverAuthorizationType);

                            result.Add (authorizationType);

                        }

                        if (result.Count > 0) { cacheManager.CacheObject (cacheKey, result, CacheExpirationData); }

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return result;

        }

        public Core.Authorizations.AuthorizationType AuthorizationTypeGet (Int64 authorizationTypeId, Boolean useCaching) {

            if (authorizationTypeId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".Core.AuthorizationType." + authorizationTypeId.ToString ();

            Core.Authorizations.AuthorizationType authorizationType = null;

            Server.Application.AuthorizationType serverAuthorizationType = null;


            ClearLastException ();

            try {

                if (useCaching) { authorizationType = (Core.Authorizations.AuthorizationType)cacheManager.GetObject (cacheKey); }

                if (authorizationType == null) {

                    serverAuthorizationType = ApplicationClient.AuthorizationTypeGet (session.Token, authorizationTypeId);

                    if (serverAuthorizationType != null) { authorizationType = new Mercury.Client.Core.Authorizations.AuthorizationType (this, serverAuthorizationType); }

                    if (authorizationType != null) { cacheManager.CacheObject (cacheKey, authorizationType, CacheExpirationData); }

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return authorizationType;

        }


        #region Member Authorizations

        public Int64 MemberAuthorizationsGetCount (Int64 memberId) {

            Int64 itemCount = 0;


            ClearLastException ();

            try {

                itemCount = ApplicationClient.MemberAuthorizationsGetCount (session.Token, memberId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return itemCount;

        }

        public List<Mercury.Server.Application.Authorization> MemberAuthorizationsGetByPage (Int64 memberId, Int32 initialRow, Int32 count) {

            Mercury.Server.Application.AuthorizationCollectionResponse response;

            List<Mercury.Server.Application.Authorization> memberAuthorizations = null;


            ClearLastException ();


            try {

                response = ApplicationClient.MemberAuthorizationsGetByPage (session.Token, memberId, initialRow, count);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                memberAuthorizations = new List<Mercury.Server.Application.Authorization> ();

                memberAuthorizations.AddRange (response.Collection);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberAuthorizations;

        }

        public List<Mercury.Server.Application.AuthorizationLine> AuthorizationLinesGet (Int64 authorizationId) {

            Mercury.Server.Application.AuthorizationLineCollectionResponse response;

            List<Mercury.Server.Application.AuthorizationLine> authorizationLines = null;


            ClearLastException ();


            try {

                response = ApplicationClient.AuthorizationLineGetByAuthorization (session.Token, authorizationId);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                authorizationLines = new List<Mercury.Server.Application.AuthorizationLine> ();

                authorizationLines.AddRange (response.Collection);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return authorizationLines;

        }

        #endregion

        #endregion


        #region Core - Authorized Services

        public List<Core.AuthorizedServices.AuthorizedService> AuthorizedServicesAvailable (Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".Core.AuthorizedService.Available";

            Mercury.Server.Application.AuthorizedServiceCollectionResponse response = new Mercury.Server.Application.AuthorizedServiceCollectionResponse ();

            List<Core.AuthorizedServices.AuthorizedService> authorizedServices = new List<Mercury.Client.Core.AuthorizedServices.AuthorizedService> ();


            ClearLastException ();

            try {

                authorizedServices = (List<Core.AuthorizedServices.AuthorizedService>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { authorizedServices = null; }

                if (authorizedServices == null) {

                    authorizedServices = new List<Mercury.Client.Core.AuthorizedServices.AuthorizedService> ();

                    response = ApplicationClient.AuthorizedServicesAvailable (session.Token);

                    if (!response.HasException) {

                        foreach (Mercury.Server.Application.AuthorizedService currentAuthorizedService in response.Collection) {

                            authorizedServices.Add (new Mercury.Client.Core.AuthorizedServices.AuthorizedService (this, currentAuthorizedService));

                        }

                        if (authorizedServices.Count > 0) { cacheManager.CacheObject (cacheKey, authorizedServices, CacheExpirationData); }

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return authorizedServices;

        }

        public Core.AuthorizedServices.AuthorizedService AuthorizedServiceGet (Int64 authorizedServiceId) {

            Core.AuthorizedServices.AuthorizedService authorizedService = null;

            Mercury.Server.Application.AuthorizedService serverAuthorizedService = null;

            ClearLastException ();

            try {

                serverAuthorizedService = ApplicationClient.AuthorizedServiceGet (session.Token, authorizedServiceId);

                if (serverAuthorizedService != null) { authorizedService = new Mercury.Client.Core.AuthorizedServices.AuthorizedService (this, serverAuthorizedService); }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return authorizedService;

        }

        public Boolean AuthorizedServiceSave (Client.Core.AuthorizedServices.AuthorizedService authorizedService) {

            Mercury.Server.Application.ObjectSaveResponse response = new Mercury.Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.AuthorizedServiceSave (session.Token, (Mercury.Server.Application.AuthorizedService)authorizedService.ToServerObject ());

                if (!response.HasException) {

                    authorizedService.SetId (response.Id);

                }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                response.Success = false;

                SetLastException (applicationException);

            }

            return response.Success;

        }

        public List<Mercury.Server.Application.MemberAuthorizedServiceDetail> AuthorizedServicePreview (Mercury.Server.Application.AuthorizedService authorizedService) {

            List<Mercury.Server.Application.MemberAuthorizedServiceDetail> result = new List<Mercury.Server.Application.MemberAuthorizedServiceDetail> ();

            Mercury.Server.Application.MemberAuthorizedServiceDetailCollectionResponse response = new Mercury.Server.Application.MemberAuthorizedServiceDetailCollectionResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.AuthorizedServicePreview (session.Token, authorizedService);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                for (Int32 currentIndex = 0; currentIndex < response.Collection.Length; currentIndex++) {

                    result.Add (response.Collection[currentIndex]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return result;

        }

        public Int64 MemberAuthorizedServicesGetCount (Int64 memberId, Boolean showHidden) {

            Int64 servicesCount = 0;


            ClearLastException ();

            try {

                servicesCount = ApplicationClient.MemberAuthorizedServicesGetCount (session.Token, memberId, showHidden);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return servicesCount;

        }

        public List<Mercury.Server.Application.MemberAuthorizedService> MemberAuthorizedServicesGetByPage (Int64 memberId, Int32 initialRow, Int32 count, Boolean showHidden) {

            Mercury.Server.Application.MemberAuthorizedServiceCollectionResponse response;

            List<Mercury.Server.Application.MemberAuthorizedService> memberAuthorizedServices = null;


            ClearLastException ();


            try {

                response = ApplicationClient.MemberAuthorizedServicesGetByPage (session.Token, memberId, initialRow, count, showHidden);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                memberAuthorizedServices = new List<Mercury.Server.Application.MemberAuthorizedService> ();

                memberAuthorizedServices.AddRange (response.Collection);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberAuthorizedServices;

        }

        public List<Mercury.Server.Application.MemberAuthorizedServiceDetail> MemberAuthorizedServiceDetailsGet (Int64 memberServiceId) {

            Mercury.Server.Application.MemberAuthorizedServiceDetailCollectionResponse response;

            List<Mercury.Server.Application.MemberAuthorizedServiceDetail> detail = null;


            ClearLastException ();


            try {

                response = ApplicationClient.MemberAuthorizedServiceDetailsGet (session.Token, memberServiceId);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                detail = new List<Mercury.Server.Application.MemberAuthorizedServiceDetail> ();

                detail.AddRange (response.Collection);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return detail;

        }

        #endregion


        #region Core - Claims

        public Int64 MemberClaimsGetCount (Int64 memberId) {

            Int64 itemCount = 0;

            ClearLastException ();

            try {

                itemCount = ApplicationClient.MemberClaimsGetCount (session.Token, memberId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return itemCount;

        }

        public List<Server.Application.Claim> MemberClaimsGetByPage (Int64 memberId, Int32 initialRow, Int32 count) {

            Server.Application.ClaimCollectionResponse response;

            List<Server.Application.Claim> memberClaims = null;


            ClearLastException ();


            try {

                response = ApplicationClient.MemberClaimsGetByPage (session.Token, memberId, initialRow, count);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                memberClaims = new List<Server.Application.Claim> ();

                memberClaims.AddRange (response.Collection);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberClaims;

        }

        public List<Server.Application.ClaimLine> ClaimLinesGet (Int64 claimId) {

            Server.Application.ClaimLineCollectionResponse response;

            List<Server.Application.ClaimLine> claimLines = null;


            ClearLastException ();


            try {

                response = ApplicationClient.ClaimLinesGet (session.Token, claimId);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                claimLines = new List<Server.Application.ClaimLine> ();

                claimLines.AddRange (response.Collection);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return claimLines;

        }

        public Int64 MemberPharmacyClaimsGetCount (Int64 memberId) {

            Int64 itemCount = 0;


            ClearLastException ();

            try {

                itemCount = ApplicationClient.MemberPharmacyClaimsGetCount (session.Token, memberId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return itemCount;

        }

        public List<Server.Application.PharmacyClaim> MemberPharmacyClaimsGetByPage (Int64 memberId, Int32 initialRow, Int32 count) {

            Server.Application.PharmacyClaimCollectionResponse response;

            List<Server.Application.PharmacyClaim> memberClaims = null;


            ClearLastException ();


            try {

                response = ApplicationClient.MemberPharmacyClaimsGetByPage (session.Token, memberId, initialRow, count);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                memberClaims = new List<Server.Application.PharmacyClaim> ();

                memberClaims.AddRange (response.Collection);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberClaims;

        }


        public Int64 MemberLabResultsGetCount (Int64 memberId) {

            Int64 itemCount = 0;


            ClearLastException ();

            try {

                itemCount = ApplicationClient.MemberLabResultsGetCount (session.Token, memberId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return itemCount;

        }

        public List<Server.Application.LabResult> MemberLabResultsGetByPage (Int64 memberId, Int32 initialRow, Int32 count) {

            Server.Application.LabResultCollectionResponse response;

            List<Server.Application.LabResult> memberClaims = null;


            ClearLastException ();


            try {

                response = ApplicationClient.MemberLabResultsGetByPage (session.Token, memberId, initialRow, count);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                memberClaims = new List<Server.Application.LabResult> ();

                memberClaims.AddRange (response.Collection);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberClaims;

        }

        #endregion 


        #region Core - Condition

        public List<Server.Application.ConditionClass> ConditionClassesAvailable (Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".ConditionClass.Available";

            Mercury.Server.Application.ConditionClassCollectionResponse response = new Mercury.Server.Application.ConditionClassCollectionResponse ();

            List<Server.Application.ConditionClass> conditionClasses = new List<Server.Application.ConditionClass> ();

            Dictionary<Int64, String> coreObjectDictionary = new Dictionary<Int64, String> ();


            ClearLastException ();

            try {

                conditionClasses = (List<Server.Application.ConditionClass>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { conditionClasses = null; }

                if (conditionClasses == null) {

                    conditionClasses = new List<Server.Application.ConditionClass> ();

                    response = ApplicationClient.ConditionClassesAvailable (session.Token);

                    if (!response.HasException) {

                        conditionClasses.AddRange (response.Collection);

                        cacheManager.CacheObject (cacheKey, conditionClasses, CacheExpirationReference);


                        // RESERVED FOR CACHING IF THE OBJECT IS MOVED CLIENT-SIDE

                        //foreach (Server.Application.ConditionClass currentServerConditionClass in response.Collection) {

                        //    Server.Application.ConditionClass conditionClass = new Server.Application.ConditionClass (this, currentServerConditionClass);

                        //    conditionClasses.Add (conditionClass);


                        //    // CACHE EACH INDIVIDUAL WORK QUEUE LOADED FROM SERVER (BY ID AND NAME)

                        //    coreObjectDictionary.Add (conditionClass.Id, conditionClass.Name);

                        //    cacheManager.CacheObject ("Application." + session.EnvironmentId + ".ConditionClass." + conditionClass.Id.ToString (), conditionClass, CacheExpirationData);

                        //    cacheManager.CacheObject ("Application." + session.EnvironmentId + ".ConditionClass." + conditionClass.Name, conditionClass, CacheExpirationData);

                        //}

                        //if (conditionClasses.Count > 0) {

                        //    cacheManager.CacheObject (cacheKey, conditionClasses, CacheExpirationData);

                        //    // CACHE THE AVAILABILITY LIST

                        //    cacheManager.CacheObject (cacheKey, conditionClasses, CacheExpirationData);

                        //    // CACHE THE DICTIONARY THAT WAS CREATED

                        //    cacheManager.CacheObject ("Application." + session.EnvironmentId + ".WorkQueue.Dictionary", coreObjectDictionary, CacheExpirationData);

                        //}

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return conditionClasses;

        }

        public List<Core.Condition.Condition> ConditionsAvailable (Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".Condition.Available";

            Mercury.Server.Application.ConditionCollectionResponse response = new Mercury.Server.Application.ConditionCollectionResponse ();

            List<Core.Condition.Condition> conditions = new List<Core.Condition.Condition> ();

            Dictionary<Int64, String> coreObjectDictionary = new Dictionary<Int64, String> ();


            ClearLastException ();

            try {

                conditions = (List<Core.Condition.Condition>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { conditions = null; }

                if (conditions == null) {

                    conditions = new List<Core.Condition.Condition> ();

                    response = ApplicationClient.ConditionsAvailable (session.Token);

                    if (!response.HasException) {

                        foreach (Server.Application.Condition currentServerCondition in response.Collection) {

                            Core.Condition.Condition condition = new Core.Condition.Condition (this, currentServerCondition);

                            conditions.Add (condition);


                            // CACHE EACH INDIVIDUAL WORK QUEUE LOADED FROM SERVER (BY ID AND NAME)

                            coreObjectDictionary.Add (condition.Id, condition.Name);

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".Condition." + condition.Id.ToString (), condition, CacheExpirationData);

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".Condition." + condition.Name, condition, CacheExpirationData);

                        }

                        if (conditions.Count > 0) {

                            cacheManager.CacheObject (cacheKey, conditions, CacheExpirationData);

                            // CACHE THE AVAILABILITY LIST

                            cacheManager.CacheObject (cacheKey, conditions, CacheExpirationData);

                            // CACHE THE DICTIONARY THAT WAS CREATED

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".Condition.Dictionary", coreObjectDictionary, CacheExpirationData);

                        }

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return conditions;

        }

        public Server.Application.ConditionClass ConditionClassGet (Int64 conditionClassId, Boolean useCaching) {

            if (conditionClassId == 0) { return null; }

            Server.Application.ConditionClass conditionClass = (

                from currentConditionClass in ConditionClassesAvailable (useCaching)

                where currentConditionClass.Id == conditionClassId

                select currentConditionClass).First ();

            return conditionClass;

        }

        public Core.Condition.Condition ConditionGet (Int64 conditionId, Boolean useCaching) {

            if (conditionId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".Condition." + conditionId.ToString ();


            Core.Condition.Condition condition = null;

            Mercury.Server.Application.Condition serverCondition = null;

            ClearLastException ();

            try {

                if (useCaching) { condition = (Core.Condition.Condition)cacheManager.GetObject (cacheKey); }

                if (condition == null) {

                    serverCondition = ApplicationClient.ConditionGet (session.Token, conditionId);

                    if (serverCondition != null) { condition = new Mercury.Client.Core.Condition.Condition (this, serverCondition); }

                    if (condition != null) { cacheManager.CacheObject (cacheKey, condition, CacheExpirationData); }

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return condition;

        }

        public Boolean ConditionSave (Core.Condition.Condition condition) {

            Mercury.Server.Application.ObjectSaveResponse response = new Mercury.Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.ConditionSave (session.Token, (Mercury.Server.Application.Condition)condition.ToServerObject ());

                if (!response.HasException) {

                    condition.SetId (response.Id);

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".Condition.Available");

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".Condition.Dictionary");

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".Condition." + condition.Id.ToString ());

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".Condition." + condition.Name);

                }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                response.Success = false;

                SetLastException (applicationException);

            }

            return response.Success;

        }

        #endregion 


        #region Core - Entity

        #region Entity

        public Core.Entity.Entity EntityGet (Int64 entityId, Boolean useCaching) {

            if (entityId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".Entity." + entityId.ToString ();

            Core.Entity.Entity entity = null;

            ClearLastException ();


            try {

                entity = (Core.Entity.Entity)cacheManager.GetObject (cacheKey);

                if (!useCaching) { entity = null; }

                if (entity == null) {

                    Server.Application.Entity serverEntity = ApplicationClient.EntityGet (session.Token, entityId);

                    if (serverEntity != null) { entity = new Core.Entity.Entity (this, serverEntity); }

                    if (entity != null) { cacheManager.CacheObject (cacheKey, entity, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entity;

        }

        #endregion 
        

        #region Entity Address

        public Mercury.Client.Core.Entity.EntityAddress EntityAddressGet (Int64 entityAddressId, Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".EntityAddress." + entityAddressId.ToString ();

            Mercury.Client.Core.Entity.EntityAddress entityAddress = null;

            ClearLastException ();

            try {

                entityAddress = (Core.Entity.EntityAddress)cacheManager.GetObject (cacheKey);

                if (!useCaching) { entityAddress = null; }

                if (entityAddress == null) {

                    Server.Application.EntityAddress serverEntityAddress = ApplicationClient.EntityAddressGet (session.Token, entityAddressId);

                    if (serverEntityAddress != null) {

                        entityAddress = new Core.Entity.EntityAddress (this, serverEntityAddress);

                        cacheManager.CacheObject (cacheKey, entityAddress, CacheExpirationData);

                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entityAddress;

        }

        public Mercury.Client.Core.Entity.EntityAddress EntityAddressGet (Int64 entityId, Server.Application.EntityAddressType addressType, DateTime forDate) {

            String cacheKey = "Application." + session.EnvironmentId + ".EntityAddress.ByEntityId." + entityId.ToString () + addressType + forDate.ToString ("MMddyyyy");

            Mercury.Client.Core.Entity.EntityAddress entityAddress = null;

            ClearLastException ();

            try {

                entityAddress = (Core.Entity.EntityAddress)cacheManager.GetObject (cacheKey);

                if (entityAddress == null) {

                    Server.Application.EntityAddress serverEntityAddress = ApplicationClient.EntityAddressGetByTypeDate (session.Token, entityId, addressType, forDate);

                    if (serverEntityAddress != null) {

                        entityAddress = new Mercury.Client.Core.Entity.EntityAddress (this, serverEntityAddress);

                        cacheManager.CacheObject (cacheKey, entityAddress, CacheExpirationData);

                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entityAddress;

        }

        public List<Core.Entity.EntityAddress> EntityAddressesGet (Int64 entityId, Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".EntityAddress.ByEntityId." + entityId.ToString ();

            List<Mercury.Client.Core.Entity.EntityAddress> entityAddresses = null;

            Server.Application.EntityAddressCollectionResponse response;

            ClearLastException ();

            try {

                entityAddresses = (List<Core.Entity.EntityAddress>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { entityAddresses = null; }

                if (entityAddresses == null) {

                    entityAddresses = new List<Core.Entity.EntityAddress> ();

                    response = ApplicationClient.EntityAddressesGet (session.Token, entityId);


                    if (!response.HasException) {

                        foreach (Server.Application.EntityAddress currentServerObject in response.Collection) {

                            Core.Entity.EntityAddress clientObject = new Core.Entity.EntityAddress (this, currentServerObject);

                            entityAddresses.Add (clientObject);

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".EntityAddress." + clientObject.Id, clientObject, CacheExpirationData);

                            // CACHE OUT CURRENT ADDRESSES TO MATCH THE GET BY DATE FUNCTION (ABOVE)

                            if ((clientObject.EffectiveDate <= DateTime.Today) && (clientObject.TerminationDate >= DateTime.Today)) { // CACHE OUT CURRENT

                                String currentCacheKey = "Application." + session.EnvironmentId + ".EntityAddress.ByEntityId." + entityId.ToString () + clientObject.AddressType + DateTime.Today.ToString ("MMddyyyy");

                                cacheManager.CacheObject (currentCacheKey, clientObject, CacheExpirationData);

                            }

                        }

                        if (entityAddresses.Count > 0) {

                            cacheManager.CacheObject (cacheKey, entityAddresses, CacheExpirationData);

                        }

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entityAddresses;

        }


        public Boolean EntityAddressSave (Core.Entity.EntityAddress entityAddress) {

            Mercury.Server.Application.ObjectSaveResponse response = new Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.EntityAddressSave (session.Token, (Mercury.Server.Application.EntityAddress)entityAddress.ToServerObject ());

                if (!response.HasException) {

                    entityAddress.SetId (response.Id);

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".EntityAddress." + entityAddress.Id.ToString ());

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".EntityAddress.ByEntityId." + entityAddress.EntityId.ToString ());

                    // REMOVE CACHE FOR TODAY CURRENT ADDRESS OF SAME TYPE

                    if ((entityAddress.EffectiveDate <= DateTime.Today) && (entityAddress.TerminationDate >= DateTime.Today)) { // CACHE OUT CURRENT

                        String currentCacheKey = "Application." + session.EnvironmentId + ".EntityAddress.ByEntityId." + entityAddress.EntityId.ToString () + entityAddress.AddressType + DateTime.Today.ToString ("MMddyyyy");

                        cacheManager.RemoveObject (currentCacheKey);

                    }

                }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                response.Success = false;

                SetLastException (applicationException);

            }

            return response.Success;

        }

        public Boolean EntityAddressTerminate (Core.Entity.EntityAddress entityAddress, DateTime terminationDate) {

            Mercury.Server.Application.BooleanResponse response = new Server.Application.BooleanResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.EntityAddressTerminate (session.Token, (Mercury.Server.Application.EntityAddress)entityAddress.ToServerObject (), terminationDate);

                if (!response.HasException) {

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".EntityAddress." + entityAddress.Id.ToString ());

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".EntityAddress.ByEntityId." + entityAddress.EntityId.ToString ());

                    // REMOVE CACHE FOR TODAY CURRENT ADDRESS OF SAME TYPE

                    if ((entityAddress.EffectiveDate <= DateTime.Today) && (entityAddress.TerminationDate >= DateTime.Today)) { // CACHE OUT CURRENT

                        String currentCacheKey = "Application." + session.EnvironmentId + ".EntityAddress.ByEntityId." + entityAddress.EntityId.ToString () + entityAddress.AddressType + DateTime.Today.ToString ("MMddyyyy");

                        cacheManager.RemoveObject (currentCacheKey);

                    }

                    entityAddress.TerminationDate = terminationDate;

                }

                else if (response.Exception.Message.Contains ("Permission Denied.")) {

                    SetLastExceptionQuite (response.Exception);

                }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                response.Result = false;

                SetLastException (applicationException);

            }

            return response.Result;

        }

        #endregion 


        #region Entity Contact

        public Core.Entity.EntityContact EntityContactGet (Int64 entityContactId, Boolean useCaching) {

            if (entityContactId == 0) { return null; }
            

            String cacheKey = "Application." + session.EnvironmentId + ".Core.EntityContact." + entityContactId.ToString ();
            
            Mercury.Client.Core.Entity.EntityContact entityContact = null;


            ClearLastException ();

            try {

                entityContact = (Core.Entity.EntityContact)cacheManager.GetObject (cacheKey);

                if (!useCaching) { entityContact = null; }

                if (entityContact == null) {

                    Server.Application.EntityContact serverEntityContact = applicationClient.EntityContactGet (session.Token, entityContactId);

                    if (serverEntityContact != null) {

                        entityContact = new Core.Entity.EntityContact (this, serverEntityContact);

                        cacheManager.CacheObject (cacheKey, entityContact, CacheExpirationData);

                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entityContact;

        }

        public Int64 EntityContactsGetCount (Int64 entityId) {

            Int64 itemCount = 0;


            ClearLastException ();

            try {

                itemCount = ApplicationClient.EntityContactsGetCount (session.Token, entityId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return itemCount;

        }

        public List<Core.Entity.EntityContact> EntityContactsGetByPage (Int64 entityId, Int32 initialRow, Int32 count) {

            Server.Application.EntityContactCollectionResponse response;

            List<Core.Entity.EntityContact> entityContacts = null;


            ClearLastException ();


            try {

                response = ApplicationClient.EntityContactsGetByPage (session.Token, entityId, initialRow, count);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                entityContacts = new List<Mercury.Client.Core.Entity.EntityContact> ();

                foreach (Mercury.Server.Application.EntityContact currentContact in response.Collection) {

                    Core.Entity.EntityContact entityContact = new Mercury.Client.Core.Entity.EntityContact (this, currentContact);

                    entityContacts.Add (entityContact);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entityContacts;

        }

        public Boolean EntityContactSave (Core.Entity.EntityContact entityContact) {

            Server.Application.ObjectSaveResponse response = new Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.EntityContactSave (session.Token, (Server.Application.EntityContact)entityContact.ToServerObject ());

                if (!response.HasException) { entityContact.SetId (response.Id); }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

                response.Success = false;

            }

            return response.Success;

        }

        #endregion 


        #region Entity Contact Information

        public Mercury.Client.Core.Entity.EntityContactInformation EntityContactInformationGet (Int64 entityContactInformationId, Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId.ToString () + ".EntityContactInformation." + entityContactInformationId.ToString ();

            Mercury.Client.Core.Entity.EntityContactInformation entityContactInformation = null;

            ClearLastException ();

            try {

                entityContactInformation = (Core.Entity.EntityContactInformation)cacheManager.GetObject (cacheKey);

                if (!useCaching) { entityContactInformation = null; }

                if (entityContactInformation == null) {

                    Server.Application.EntityContactInformation serverEntityContactInformation = ApplicationClient.EntityContactInformationGet (session.Token, entityContactInformationId);

                    if (serverEntityContactInformation != null) {

                        entityContactInformation = new Core.Entity.EntityContactInformation (this, serverEntityContactInformation);

                        cacheManager.CacheObject (cacheKey, entityContactInformation, CacheExpirationData);

                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entityContactInformation;

        }

        public Mercury.Client.Core.Entity.EntityContactInformation EntityContactInformationGet (Int64 entityId, Server.Application.EntityContactType contactType, DateTime forDate) {

            String cacheKey = "Application." + session.EnvironmentId.ToString () + ".EntityContactInformation.ByEntityId." + entityId.ToString () + contactType + forDate.ToString ("MMddyyyy");

            Mercury.Client.Core.Entity.EntityContactInformation entityContact = null;

            ClearLastException ();

            try {

                entityContact = (Core.Entity.EntityContactInformation)cacheManager.GetObject (cacheKey);

                if (entityContact == null) {

                    Server.Application.EntityContactInformation serverEntityContact = ApplicationClient.EntityContactInformationGetByTypeDate (session.Token, entityId, contactType, forDate);

                    if (serverEntityContact != null) {

                        entityContact = new Mercury.Client.Core.Entity.EntityContactInformation (this, serverEntityContact);

                        cacheManager.CacheObject (cacheKey, entityContact, CacheExpirationData);

                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entityContact;

        }

        public List<Core.Entity.EntityContactInformation> EntityContactInformationsGet (Int64 entityId, Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId.ToString () + ".EntityContactInformation.ByEntityId." + entityId.ToString ();

            List<Mercury.Client.Core.Entity.EntityContactInformation> entityContacts = null;

            Server.Application.EntityContactInformationCollectionResponse response;

            ClearLastException ();

            try {

                entityContacts = (List<Core.Entity.EntityContactInformation>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { entityContacts = null; }

                if (entityContacts == null) {

                    entityContacts = new List<Core.Entity.EntityContactInformation> ();

                    response = ApplicationClient.EntityContactInformationsGet (session.Token, entityId);


                    if (!response.HasException) {

                        entityContacts = new List<Mercury.Client.Core.Entity.EntityContactInformation> ();

                        foreach (Server.Application.EntityContactInformation currentContact in response.Collection) {

                            Core.Entity.EntityContactInformation entityContactInformation = new Core.Entity.EntityContactInformation (this, currentContact);

                            entityContacts.Add (entityContactInformation);

                            // CACHE OUT CURRENT CONTACT INFORMATIONS TO MATCH THE GET BY DATE FUNCTION (ABOVE)

                            if ((entityContactInformation.EffectiveDate <= DateTime.Today) && (entityContactInformation.TerminationDate >= DateTime.Today)) { // CACHE OUT CURRENT

                                String currentCacheKey = "Application." + session.EnvironmentId + ".EntityContactInformation.ByEntityId." + entityId.ToString () + entityContactInformation.ContactType + DateTime.Today.ToString ("MMddyyyy");

                                cacheManager.CacheObject (currentCacheKey, entityContactInformation, CacheExpirationData);

                            }

                        }

                        if (entityContacts.Count > 0) {

                            cacheManager.CacheObject (cacheKey, entityContacts, CacheExpirationData);

                        }


                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entityContacts;

        }


        public Boolean EntityContactInformationSave (Core.Entity.EntityContactInformation entityContactInformation) {

            Mercury.Server.Application.ObjectSaveResponse response = new Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.EntityContactInformationSave (session.Token, (Mercury.Server.Application.EntityContactInformation)entityContactInformation.ToServerObject ());

                if (!response.HasException) {

                    entityContactInformation.SetId (response.Id);

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".EntityContactInformation." + entityContactInformation.Id.ToString ());

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".EntityContactInformation.ByEntityId." + entityContactInformation.EntityId.ToString ());

                    // REMOVE CACHE FOR TODAY CURRENT CONTACT INFORMATION OF SAME TYPE

                    if ((entityContactInformation.EffectiveDate <= DateTime.Today) && (entityContactInformation.TerminationDate >= DateTime.Today)) { // CACHE OUT CURRENT

                        String currentCacheKey = "Application." + session.EnvironmentId + ".EntityContactInformation.ByEntityId." + entityContactInformation.EntityId.ToString () + entityContactInformation.ContactType + DateTime.Today.ToString ("MMddyyyy");

                        cacheManager.RemoveObject (currentCacheKey);

                    }

                }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                response.Success = false;

                SetLastException (applicationException);

            }

            return response.Success;

        }

        public Boolean EntityContactInformationTerminate (Core.Entity.EntityContactInformation entityContactInformation, DateTime terminationDate) {

            Mercury.Server.Application.BooleanResponse response = new Server.Application.BooleanResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.EntityContactInformationTerminate (session.Token, (Mercury.Server.Application.EntityContactInformation)entityContactInformation.ToServerObject (), terminationDate);

                if (!response.HasException) {

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".EntityContactInformation." + entityContactInformation.Id.ToString ());

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".EntityContactInformation.ByEntityId." + entityContactInformation.EntityId.ToString ());

                    // REMOVE CACHE FOR TODAY CURRENT CONTACT INFORMATION OF SAME TYPE

                    if ((entityContactInformation.EffectiveDate <= DateTime.Today) && (entityContactInformation.TerminationDate >= DateTime.Today)) { // CACHE OUT CURRENT

                        String currentCacheKey = "Application." + session.EnvironmentId + ".EntityContactInformation.ByEntityId." + entityContactInformation.EntityId.ToString () + entityContactInformation.ContactType + DateTime.Today.ToString ("MMddyyyy");

                        cacheManager.RemoveObject (currentCacheKey);

                    }

                    entityContactInformation.TerminationDate = terminationDate;

                }

                else if (response.Exception.Message.Contains ("Permission Denied.")) {

                    SetLastExceptionQuite (response.Exception);

                }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                response.Result = false;

                SetLastException (applicationException);

            }

            return response.Result;

        }

        #endregion 


        #region Entity Correspondence

        public Core.Entity.EntityCorrespondence EntityCorrespondenceGet (Int64 entityCorrespondenceId, Boolean useCaching) {

            if (entityCorrespondenceId == 0) { return null; }

            String cacheKey = "Application." + session.EnvironmentId + ".EntityCorrespondence." + entityCorrespondenceId.ToString ();

            Mercury.Client.Core.Entity.EntityCorrespondence entityCorrespondence = null;

            ClearLastException ();

            try {

                entityCorrespondence = (Core.Entity.EntityCorrespondence)cacheManager.GetObject (cacheKey);

                if (!useCaching) { entityCorrespondence = null; }

                if (entityCorrespondence == null) {

                    entityCorrespondence = new Mercury.Client.Core.Entity.EntityCorrespondence (this, ApplicationClient.EntityCorrespondenceGet (session.Token, entityCorrespondenceId));

                    cacheManager.CacheObject (cacheKey, entityCorrespondence, CacheExpirationData);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entityCorrespondence;

        }

        public Server.Application.ImageResponse EntityCorrespondenceImageGet (Int64 entityCorrespondenceId, Boolean render) {

            if (entityCorrespondenceId == 0) { return null; }

            Server.Application.ImageResponse response = null;


            ClearLastException ();

            try {

                response = ApplicationClient.EntityCorrespondenceImageGet (session.Token, entityCorrespondenceId, render);

                if (response.HasException) { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return response;

        }

        public Boolean EntityCorrespondenceSave (Core.Entity.EntityCorrespondence entityCorrespondence) {

            Mercury.Server.Application.ObjectSaveResponse response = new Mercury.Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.EntityCorrespondenceSave (session.Token, (Mercury.Server.Application.EntityCorrespondence)entityCorrespondence.ToServerObject ());

                if (!response.HasException) {

                    entityCorrespondence.SetId (response.Id);

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".EntityCorrespondence." + response.Id.ToString ());

                }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                response.Success = false;

                SetLastException (applicationException);

            }

            return response.Success;

        }


        public Int64 EntityDocumentsGetCount (Int64 entityId) {

            Int64 itemCount = 0;

            ClearLastException ();

            try {

                itemCount = ApplicationClient.EntityDocumentsGetCount (session.Token, entityId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return itemCount;

        }

        public List<Mercury.Server.Application.EntityDocumentDataView> EntityDocumentsGetByPage (Int64 entityId, Int32 initialRow, Int32 count) {

            Mercury.Server.Application.EntityDocumentCollectionResponse response;

            List<Mercury.Server.Application.EntityDocumentDataView> entityDocuments = new List<Mercury.Server.Application.EntityDocumentDataView> ();


            ClearLastException ();


            try {

                response = ApplicationClient.EntityDocumentsGetByPage (session.Token, entityId, initialRow, count);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                entityDocuments.AddRange (response.Collection);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entityDocuments;

        }

        #endregion 


        #region Entity Note

        public Core.Entity.EntityNote EntityNoteGet (Int64 entityNoteId, Boolean useCaching) {

            if (entityNoteId == 0) { return null; }

            String cacheKey = "Application." + session.EnvironmentId + ".EntityNote." + entityNoteId.ToString ();

            Mercury.Client.Core.Entity.EntityNote entityNote = null;

            ClearLastException ();

            try {

                entityNote = (Core.Entity.EntityNote)cacheManager.GetObject (cacheKey);

                if (!useCaching) { entityNote = null; }

                if (entityNote == null) {

                    Server.Application.EntityNote serverNote = ApplicationClient.EntityNoteGet (session.Token, entityNoteId);

                    if (serverNote != null) {

                        entityNote = new Mercury.Client.Core.Entity.EntityNote (this, serverNote);

                        cacheManager.CacheObject (cacheKey, entityNote, CacheExpirationData);

                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entityNote;

        }

        public Boolean EntityNoteSave (Core.Entity.EntityNote entityNote) {

            Server.Application.ObjectSaveResponse response = new Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.EntityNoteSave (session.Token, (Server.Application.EntityNote)entityNote.ToServerObject ());

                if (!response.HasException) { 
                    
                    entityNote.SetId (response.Id);

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".EntityNote." + response.Id);

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".EntityNote.ByEntityId." + entityNote.EntityId + "." + entityNote.Importance.ToString ());

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".EntityNote.ByEntityId." + entityNote.EntityId + ".All");
                
                }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

                response.Success = false;

            }

            return response.Success;

        }

        public Boolean EntityNoteTerminate (Core.Entity.EntityNote entityNote, DateTime terminationDate) {

            Server.Application.BooleanResponse response = new Mercury.Server.Application.BooleanResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.EntityNoteTerminate (session.Token, (Server.Application.EntityNote)entityNote.ToServerObject (), terminationDate);


                if (!response.HasException) { 
                    
                    entityNote.TerminationDate = terminationDate; 
                
                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".EntityNote." + entityNote.Id);

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".EntityNote.ByEntityId." + entityNote.EntityId + "." + entityNote.Importance.ToString ());

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".EntityNote.ByEntityId." + entityNote.EntityId + ".All");
                
                }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

                response.Result = false;

            }

            return response.Result;

        }


        public List<Server.Application.EntityNoteContent> EntityNoteContentsGet (Int64 entityNoteId, Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".Core.Entity.EntityNoteContents." + entityNoteId.ToString ();

            List<Server.Application.EntityNoteContent> contents = new List<Server.Application.EntityNoteContent> ();


            Server.Application.EntityNoteContentCollectionResponse response;

            ClearLastException ();

            try {

                contents = (List<Server.Application.EntityNoteContent>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { contents = null; }

                if (contents == null) {

                    contents = new List<Server.Application.EntityNoteContent> ();


                    response = ApplicationClient.EntityNoteContentsGet (session.Token, entityNoteId);

                    if (!response.HasException) {

                        contents.AddRange (response.Collection);

                        cacheManager.CacheObject (cacheKey, contents, CacheExpirationData);

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return contents;


        }

        public Boolean EntityNoteContentAppend (Core.Entity.EntityNote entityNote, String content) {

            Server.Application.ObjectSaveResponse response = new Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.EntityNoteContentAppend (session.Token, (Server.Application.EntityNote)entityNote.ToServerObject (), content);

                if (!response.HasException) { entityNote.SetId (response.Id); }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

                response.Success = false;

            }

            return response.Success;

        }


        public Int64 EntityNotesGetCount (Int64 entityId) {

            Int64 itemCount = 0;


            ClearLastException ();

            try {

                itemCount = ApplicationClient.EntityNotesGetCount (session.Token, entityId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return itemCount;

        }

        public List<Core.Entity.EntityNote> EntityNotesGetByPage (Int64 entityId, Int32 initialRow, Int32 count) {

            Server.Application.EntityNoteCollectionResponse response;

            List<Core.Entity.EntityNote> entityNotes = null;


            ClearLastException ();


            try {

                response = ApplicationClient.EntityNotesGetByPage (session.Token, entityId, initialRow, count);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                entityNotes = new List<Mercury.Client.Core.Entity.EntityNote> ();

                foreach (Mercury.Server.Application.EntityNote currentNote in response.Collection) {

                    Core.Entity.EntityNote entityNote = new Mercury.Client.Core.Entity.EntityNote (this, currentNote);

                    entityNotes.Add (entityNote);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entityNotes;

        }

        public Core.Entity.EntityNote EntityNoteGetMostRecentByImportance (Int64 entityId, Server.Application.NoteImportance importance, Boolean useCaching) {

            if (entityId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".EntityNote.ByEntityId." + entityId + "." + importance.ToString ();

            Mercury.Client.Core.Entity.EntityNote entityNote = null;


            ClearLastException ();

            try {

                entityNote = (Core.Entity.EntityNote)cacheManager.GetObject (cacheKey);

                if (!useCaching) { entityNote = null; }

                if (entityNote == null) {

                    Server.Application.EntityNote serverNote = ApplicationClient.EntityNoteGetMostRecentByImportance (session.Token, entityId, importance);

                    if (serverNote != null) { entityNote = new Mercury.Client.Core.Entity.EntityNote (this, serverNote); }

                    cacheManager.CacheObject (cacheKey, entityNote, CacheExpirationData);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entityNote;

        }

        public Dictionary<Server.Application.NoteImportance, Core.Entity.EntityNote> EntityNoteGetMostRecentByAllImportances (Int64 entityId, Boolean useCaching) {

            if (entityId == 0) { return new Dictionary<Server.Application.NoteImportance, Core.Entity.EntityNote> (); }


            String cacheKey = "Application." + session.EnvironmentId + ".EntityNote.ByEntityId." + entityId + ".All";

            Dictionary<Server.Application.NoteImportance, Core.Entity.EntityNote> entityNotes = new Dictionary<Server.Application.NoteImportance, Core.Entity.EntityNote> ();

            Server.Application.EntityNoteCollectionResponse response;


            ClearLastException ();

            try {

                entityNotes = (Dictionary<Server.Application.NoteImportance, Core.Entity.EntityNote>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { entityNotes = null; }

                if (entityNotes == null) {

                    entityNotes = new Dictionary<Server.Application.NoteImportance, Core.Entity.EntityNote> ();

                    response = ApplicationClient.EntityNoteGetMostRecentByAllImportances (session.Token, entityId);

                    foreach (Server.Application.EntityNote currentServerEntityNote in response.Collection) {

                        if (!entityNotes.ContainsKey (currentServerEntityNote.Importance)) {

                            entityNotes.Add (currentServerEntityNote.Importance, new Mercury.Client.Core.Entity.EntityNote (this, currentServerEntityNote));

                        }

                    }

                    if (entityNotes.Count > 0) { cacheManager.CacheObject (cacheKey, entityNotes, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entityNotes;

        }

        public Core.Entity.EntityNote EntityNoteGetMostRecentByType (Int64 entityId, Int64 noteTypeId, Boolean useCaching) {

            if (entityId == 0) { return null; }

            
            String cacheKey = "Application." + session.EnvironmentId + ".Core.Entity.EntityNote." + entityId.ToString () + "." + noteTypeId.ToString ();

            Mercury.Client.Core.Entity.EntityNote entityNote = null;


            ClearLastException ();

            try {

                entityNote = (Core.Entity.EntityNote)cacheManager.GetObject (cacheKey);

                if (!useCaching) { entityNote = null; }

                if (entityNote == null) {

                    Server.Application.EntityNote serverNote = ApplicationClient.EntityNoteGetMostRecentByType (session.Token, entityId, noteTypeId);

                    if (serverNote != null) { entityNote = new Mercury.Client.Core.Entity.EntityNote (this, serverNote); }

                    cacheManager.CacheObject (cacheKey, entityNote, CacheExpirationData);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return entityNote;

        }
        
        #endregion 

        #endregion


        #region Core - Forms

        #region Forms

        public List<Server.Application.SearchResultFormHeader> FormsAvailable (Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".Form.Available";

            List<Server.Application.SearchResultFormHeader> results = new List<Server.Application.SearchResultFormHeader> ();

            Mercury.Server.Application.SearchResultFormHeaderCollectionResponse response = new Server.Application.SearchResultFormHeaderCollectionResponse ();

            ClearLastException ();

            try {

                results = (List<Server.Application.SearchResultFormHeader>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { results = null; }

                if (results == null) {

                    results = new List<Server.Application.SearchResultFormHeader> ();


                    response = ApplicationClient.FormsAvailable (session.Token);

                    if (!response.HasException) { 
                        
                        results.AddRange (response.Collection);

                        if (results.Count > 0) {

                            cacheManager.CacheObject (cacheKey, results, CacheExpirationData);

                        }
                    
                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return results;

        }

        public Mercury.Client.Core.Forms.Form FormGet (Int64 formId) {

            Server.Application.Form serverForm;

            Mercury.Client.Core.Forms.Form localForm = null;

            ClearLastException ();

            try {

                serverForm = ApplicationClient.FormGet (session.Token, formId);

                if (serverForm != null) {

                    localForm = new Mercury.Client.Core.Forms.Form (this, serverForm);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return localForm;

        }

        public Mercury.Client.Core.Forms.Form FormGet (String formName) {

            Server.Application.Form serverForm;

            Mercury.Client.Core.Forms.Form localForm = null;

            ClearLastException ();

            try {

                serverForm = ApplicationClient.FormGetByName (session.Token, formName);

                if (serverForm != null) {

                    localForm = new Mercury.Client.Core.Forms.Form (this, serverForm);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return localForm;

        }

        public Mercury.Client.Core.Forms.Form FormGetByEntityFormId (Int64 entityFormId) {

            Server.Application.Form serverForm;

            Mercury.Client.Core.Forms.Form localForm = null;

            ClearLastException ();

            try {

                serverForm = ApplicationClient.FormGetByEntityFormId (session.Token, entityFormId);

                if (serverForm != null) {

                    localForm = new Mercury.Client.Core.Forms.Form (this, serverForm);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return localForm;

        }


        public Server.Application.FormCompileMessageCollectionResponse FormCompile (Core.Forms.Form form) {

            Server.Application.FormCompileMessageCollectionResponse response = new Server.Application.FormCompileMessageCollectionResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.FormCompile (session.Token, (Server.Application.Form)form.ToServerObject ());

                if (response.HasException) {

                    SetLastException (new ApplicationException (response.Exception.Message));

                }

            }

            catch (Exception applicationException) {

                response.HasException = true;

                response.Exception = new Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;

                SetLastException (applicationException);

            }

            return response;

        }

        public Server.Application.FormSubmitResponse FormSubmit (Core.Forms.Form form) {

            Server.Application.FormSubmitResponse response = new Server.Application.FormSubmitResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.FormSubmit (session.Token, (Server.Application.Form)form.ToServerObject ());

                if (response.HasException) {

                    SetLastException (new ApplicationException (response.Exception.Message));

                }

            }

            catch (Exception applicationException) {

                response.HasException = true;

                response.Exception = new Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;

                SetLastException (applicationException);

            }

            return response;

        }

        public Server.Application.ObjectSaveResponse FormSave (Core.Forms.Form form) {

            Server.Application.ObjectSaveResponse response = new Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.FormSave (session.Token, (Server.Application.Form)form.ToServerObject ());

                if (response.HasException) {

                    SetLastException (new ApplicationException (response.Exception.Message));

                }

                else {

                    if (form.FormType == Server.Application.FormType.Instance) {

                        form.EntityFormId = response.Id;

                    }

                    else { form.FormId = response.Id; }

                }

            }

            catch (Exception applicationException) {

                response.HasException = true;

                response.Exception = new Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;

                SetLastException (applicationException);

            }

            return response;

        }

        #endregion


        #region Form Control - Data Binding Extensions

        public Dictionary<String, String> FormControl_DataBindableProperties (Core.Forms.Form form, Guid controlId) {

            String cacheKey = "Application." + session.EnvironmentId + ".Core.FormControl.DataBindableProperties." + controlId.ToString ();

            Dictionary<String, String> bindableProperties = new Dictionary<String, String> ();

            ClearLastException ();

            try {

                bindableProperties = (Dictionary<String, String>)cacheManager.GetObject (cacheKey);

                if (bindableProperties == null) {

                    bindableProperties = ApplicationClient.FormControl_DataBindableProperties (session.Token, (Server.Application.Form)form.ToServerObject (), controlId);

                    cacheManager.CacheObject (cacheKey, bindableProperties, CacheExpirationData);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return bindableProperties;

        }

        public Dictionary<String, String> FormControl_DataBindingContexts (Core.Forms.Form form, Guid controlId) {

            String cacheKey = "Application." + session.EnvironmentId + ".Core.FormControl.DataBindingContexts." + controlId.ToString ();

            Dictionary<String, String> bindingContexts = new Dictionary<String, String> ();

            ClearLastException ();

            try {

                bindingContexts = (Dictionary<String, String>)cacheManager.GetObject (cacheKey);

                if (bindingContexts == null) {

                    bindingContexts = ApplicationClient.FormControl_DataBindingContexts (session.Token, (Server.Application.Form)form.ToServerObject (), controlId);

                    cacheManager.CacheObject (cacheKey, bindingContexts, CacheExpirationData);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return bindingContexts;

        }

        public Boolean FormControl_DataBindingAllowed (Core.Forms.Form form, Guid controlId, String bindableProperty, String forDataType) {

            String cacheKey = "Application." + session.EnvironmentId + ".Core.FormControl.DataBindingAllowed."+ controlId.ToString () + "." + bindableProperty + "." + forDataType;

            Server.Application.BooleanResponse response = new Server.Application.BooleanResponse ();

            Boolean bindingAllowed = false;

            ClearLastException ();

            try {

                if (!cacheManager.IsObjectCached (cacheKey)) {

                    response = ApplicationClient.FormControl_DataBindingAllowed (session.Token, (Server.Application.Form)form.ToServerObject (), controlId, bindableProperty, forDataType);

                    if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                    bindingAllowed = response.Result;

                    cacheManager.CacheObject (cacheKey, bindingAllowed, CacheExpirationData);

                }

                else { bindingAllowed = (Boolean)cacheManager.GetObject (cacheKey); }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return bindingAllowed;

        }

        public String FormControl_EvaluateDataBinding (Core.Forms.Form form, Guid controlId, Server.Application.FormControlDataBinding dataBinding) {

            String dataValue = String.Empty;

            ClearLastException ();

            try {

                dataValue = ApplicationClient.FormControl_EvaluateDataBinding (session.Token, (Server.Application.Form)form.ToServerObject (), controlId, dataBinding);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return dataValue;

        }

        public Core.Forms.Form FormControl_RaiseEvent (Core.Forms.Form form, Guid controlId, String eventName) {

            Server.Application.FormControlRaiseEventResponse response = new Server.Application.FormControlRaiseEventResponse ();

            Core.Forms.Form updatedForm;

            ClearLastException ();

            try {

                response = ApplicationClient.FormControl_RaiseEvent (session.Token, (Server.Application.Form)form.ToServerObject (), controlId, eventName);

                if (response.HasException) {

                    SetLastException (new ApplicationException (response.Exception.Message));

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            updatedForm = new Mercury.Client.Core.Forms.Form (this, response.Form);

            return updatedForm;

        }

        public Core.Forms.Form Form_OnDataSourceChanged (Core.Forms.Form form, Guid controlId) {

            Server.Application.Form responseForm = (Server.Application.Form)form.ToServerObject ();

            Core.Forms.Form updatedForm;

            ClearLastException ();

            try {

                responseForm = ApplicationClient.Form_OnDataSourceChanged (session.Token, responseForm, controlId);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            updatedForm = new Mercury.Client.Core.Forms.Form (this, responseForm);

            return updatedForm;

        }

        #endregion


        #region Form Control - Event Handlers Extensions

        public List<String> FormControl_Events (Core.Forms.Form form, Core.Forms.Control control) {

            String cacheKey = "Application." + session.EnvironmentId + ".Core.FormControl.Events." + control.ControlType.ToString ();  // CACHE BY CONTROL TYPE AND NOT SPECIFIC CONTROL 

            List<String> events = new List<String> ();

            ClearLastException ();

            try {

                events = (List<String>)cacheManager.GetObject (cacheKey);

                if (events == null) {

                    events = new List<String> ();

                    String[] serverEvents = ApplicationClient.FormControl_Events (session.Token, (Server.Application.Form)form.ToServerObject (), control.ControlId);

                    foreach (String currentEvent in serverEvents) {

                        events.Add (currentEvent);

                    }

                    cacheManager.CacheObject (cacheKey, events, CacheExpirationData);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return events;

        }

        public List<Server.Application.FormCompileMessage> FormControl_EventHandler_Compile (Core.Forms.Control control, String eventName) {

            List<Server.Application.FormCompileMessage> compileMessages = new List<Server.Application.FormCompileMessage> ();

            Server.Application.FormCompileMessageCollectionResponse response;

            try {

                response = ApplicationClient.FormControl_EventHandler_Compile (session.Token, (Server.Application.Form)control.Form.ToServerObject (), control.ControlId, eventName);

                if (response.HasException) {

                    throw new ApplicationException (response.Exception.Message);

                }

                compileMessages.AddRange (response.Collection);

            }

            catch (Exception applicationException) {

                Server.Application.FormCompileMessage message = new Server.Application.FormCompileMessage ();

                message.ControlId = control.ControlId.ToString ();

                message.ControlName = control.Name;

                message.ControlType = control.ControlType.ToString ();

                message.Description = applicationException.Message;

                message.MessageType = Server.Application.FormCompileMessageType.Error;

                compileMessages.Add (message);

            }

            return compileMessages;

        }

        #endregion


        #region Form Control - Specific Control Methods

        public Dictionary<String, String> FormControlSelection_ReferenceGetPage (Core.Forms.Form form, Guid controlId, String text, Int32 initialRow, Int32 pageSize) {

            String cacheKey = "Application." + session.EnvironmentId + ".Core.FormControl.Selection.Reference." + controlId.ToString ();

            Dictionary<String, String> referencePage = new Dictionary<String, String> ();

            ClearLastException ();

            try {

                // referencePage = (Dictionary<String, String>) cacheManager.GetObject (cacheKey);

                // NO CACHING, DISABLED

                referencePage = null;

                if (referencePage == null) {

                    referencePage = ApplicationClient.FormControlSelection_ReferenceGetPage (session.Token, (Server.Application.Form)form.ToServerObject (), controlId, text, initialRow, pageSize);

                    cacheManager.CacheObject (cacheKey, referencePage, CacheExpirationData);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return referencePage;

        }

        #endregion 

        #endregion 


        #region Core - Individual

        #region Care Levels

        public List<Core.Individual.CareLevel> CareLevelsAvailable (Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".CareLevel.Available";

            Mercury.Server.Application.CareLevelCollectionResponse response = new Mercury.Server.Application.CareLevelCollectionResponse ();

            List<Core.Individual.CareLevel> careLevels = new List<Core.Individual.CareLevel> ();

            Dictionary<Int64, String> coreObjectDictionary = new Dictionary<Int64, String> ();


            ClearLastException ();

            try {

                careLevels = (List<Core.Individual.CareLevel>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { careLevels = null; }

                if (careLevels == null) {

                    careLevels = new List<Core.Individual.CareLevel> ();

                    response = ApplicationClient.CareLevelsAvailable (session.Token);

                    if (!response.HasException) {

                        foreach (Server.Application.CareLevel currentServerCareLevel in response.Collection) {

                            Core.Individual.CareLevel careLevel = new Core.Individual.CareLevel (this, currentServerCareLevel);

                            careLevels.Add (careLevel);


                            // CACHE EACH INDIVIDUAL WORK QUEUE LOADED FROM SERVER (BY ID AND NAME)

                            coreObjectDictionary.Add (careLevel.Id, careLevel.Name);

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".CareLevel." + careLevel.Id.ToString (), careLevel, CacheExpirationData);

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".CareLevel." + careLevel.Name, careLevel, CacheExpirationData);

                        }

                        if (careLevels.Count > 0) {

                            cacheManager.CacheObject (cacheKey, careLevels, CacheExpirationData);

                            // CACHE THE AVAILABILITY LIST

                            cacheManager.CacheObject (cacheKey, careLevels, CacheExpirationData);

                            // CACHE THE DICTIONARY THAT WAS CREATED

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".WorkQueue.Dictionary", coreObjectDictionary, CacheExpirationData);

                        }

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return careLevels;

        }

        public Core.Individual.CareLevel CareLevelGet (Int64 careLevelId, Boolean useCaching) {

            if (careLevelId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".CareLevel." + careLevelId.ToString ();


            Core.Individual.CareLevel careLevel = null;

            Mercury.Server.Application.CareLevel serverCareLevel = null;

            ClearLastException ();

            try {

                if (useCaching) { careLevel = (Core.Individual.CareLevel)cacheManager.GetObject (cacheKey); }

                if (careLevel == null) {

                    serverCareLevel = ApplicationClient.CareLevelGet (session.Token, careLevelId);

                    if (serverCareLevel != null) { careLevel = new Mercury.Client.Core.Individual.CareLevel (this, serverCareLevel); }

                    if (careLevel != null) { cacheManager.CacheObject (cacheKey, careLevel, CacheExpirationData); }

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return careLevel;

        }

        public Boolean CareLevelSave (Core.Individual.CareLevel careLevel) {

            Mercury.Server.Application.ObjectSaveResponse response = new Mercury.Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.CareLevelSave (session.Token, (Mercury.Server.Application.CareLevel)careLevel.ToServerObject ());

                if (!response.HasException) {

                    careLevel.SetId (response.Id);

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".CareLevel.Available");

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".CareLevel.Dictionary");

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".CareLevel." + careLevel.Id.ToString ());

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".CareLevel." + careLevel.Name);

                }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                response.Success = false;

                SetLastException (applicationException);

            }

            return response.Success;

        }

        #endregion 
        

        #region Care Measure Scales

        public List<Core.Individual.CareMeasureScale> CareMeasureScalesAvailable (Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".CareMeasureScale.Available";

            Mercury.Server.Application.CareMeasureScaleCollectionResponse response = new Mercury.Server.Application.CareMeasureScaleCollectionResponse ();

            List<Core.Individual.CareMeasureScale> careMeasureScales = new List<Core.Individual.CareMeasureScale> ();

            Dictionary<Int64, String> coreObjectDictionary = new Dictionary<Int64, String> ();


            ClearLastException ();

            try {

                careMeasureScales = (List<Core.Individual.CareMeasureScale>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { careMeasureScales = null; }

                if (careMeasureScales == null) {

                    careMeasureScales = new List<Core.Individual.CareMeasureScale> ();

                    response = ApplicationClient.CareMeasureScalesAvailable (session.Token);

                    if (!response.HasException) {

                        foreach (Server.Application.CareMeasureScale currentServerCareMeasureScale in response.Collection) {

                            Core.Individual.CareMeasureScale careMeasureScale = new Core.Individual.CareMeasureScale (this, currentServerCareMeasureScale);

                            careMeasureScales.Add (careMeasureScale);


                            // CACHE EACH INDIVIDUAL WORK QUEUE LOADED FROM SERVER (BY ID AND NAME)

                            coreObjectDictionary.Add (careMeasureScale.Id, careMeasureScale.Name);

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".CareMeasureScale." + careMeasureScale.Id.ToString (), careMeasureScale, CacheExpirationData);

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".CareMeasureScale." + careMeasureScale.Name, careMeasureScale, CacheExpirationData);

                        }

                        if (careMeasureScales.Count > 0) {

                            cacheManager.CacheObject (cacheKey, careMeasureScales, CacheExpirationData);

                            // CACHE THE AVAILABILITY LIST

                            cacheManager.CacheObject (cacheKey, careMeasureScales, CacheExpirationData);

                            // CACHE THE DICTIONARY THAT WAS CREATED

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".WorkQueue.Dictionary", coreObjectDictionary, CacheExpirationData);

                        }

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return careMeasureScales;

        }

        public Core.Individual.CareMeasureScale CareMeasureScaleGet (Int64 careMeasureScaleId, Boolean useCaching) {

            if (careMeasureScaleId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".CareMeasureScale." + careMeasureScaleId.ToString ();


            Core.Individual.CareMeasureScale careMeasureScale = null;

            Mercury.Server.Application.CareMeasureScale serverCareMeasureScale = null;

            ClearLastException ();

            try {

                if (useCaching) { careMeasureScale = (Core.Individual.CareMeasureScale)cacheManager.GetObject (cacheKey); }

                if (careMeasureScale == null) {

                    serverCareMeasureScale = ApplicationClient.CareMeasureScaleGet (session.Token, careMeasureScaleId);

                    if (serverCareMeasureScale != null) { careMeasureScale = new Mercury.Client.Core.Individual.CareMeasureScale (this, serverCareMeasureScale); }

                    if (careMeasureScale != null) { cacheManager.CacheObject (cacheKey, careMeasureScale, CacheExpirationData); }

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return careMeasureScale;

        }

        public Boolean CareMeasureScaleSave (Core.Individual.CareMeasureScale careMeasureScale) {

            Mercury.Server.Application.ObjectSaveResponse response = new Mercury.Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.CareMeasureScaleSave (session.Token, (Mercury.Server.Application.CareMeasureScale)careMeasureScale.ToServerObject ());

                if (!response.HasException) {

                    careMeasureScale.SetId (response.Id);

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".CareMeasureScale.Available");

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".CareMeasureScale.Dictionary");

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".CareMeasureScale." + careMeasureScale.Id.ToString ());

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".CareMeasureScale." + careMeasureScale.Name);

                }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                response.Success = false;

                SetLastException (applicationException);

            }

            return response.Success;

        }

        #endregion 

        
        #region Care Measures

        public List<Server.Application.CareMeasureDomain> CareMeasureDomainsAvailable (Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".CareMeasureDomain.Available";

            Mercury.Server.Application.CareMeasureDomainCollectionResponse response = new Mercury.Server.Application.CareMeasureDomainCollectionResponse ();

            List<Server.Application.CareMeasureDomain> careMeasureDomains = new List<Server.Application.CareMeasureDomain> ();

            Dictionary<Int64, String> coreObjectDictionary = new Dictionary<Int64, String> ();


            ClearLastException ();

            try {

                careMeasureDomains = (List<Server.Application.CareMeasureDomain>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { careMeasureDomains = null; }

                if (careMeasureDomains == null) {

                    careMeasureDomains = new List<Server.Application.CareMeasureDomain> ();

                    response = ApplicationClient.CareMeasureDomainsAvailable (session.Token);

                    if (!response.HasException) {

                        careMeasureDomains.AddRange (response.Collection);

                        cacheManager.CacheObject (cacheKey, careMeasureDomains, CacheExpirationReference);


                        // RESERVED FOR CACHING IF THE OBJECT IS MOVED CLIENT-SIDE

                        //foreach (Server.Application.CareMeasureDomain currentServerCareMeasureDomain in response.Collection) {

                        //    Server.Application.CareMeasureDomain careMeasureDomain = new Server.Application.CareMeasureDomain (this, currentServerCareMeasureDomain);

                        //    careMeasureDomains.Add (careMeasureDomain);


                        //    // CACHE EACH INDIVIDUAL WORK QUEUE LOADED FROM SERVER (BY ID AND NAME)

                        //    coreObjectDictionary.Add (careMeasureDomain.Id, careMeasureDomain.Name);

                        //    cacheManager.CacheObject ("Application." + session.EnvironmentId + ".CareMeasureDomain." + careMeasureDomain.Id.ToString (), careMeasureDomain, CacheExpirationData);

                        //    cacheManager.CacheObject ("Application." + session.EnvironmentId + ".CareMeasureDomain." + careMeasureDomain.Name, careMeasureDomain, CacheExpirationData);

                        //}

                        //if (careMeasureDomains.Count > 0) {

                        //    cacheManager.CacheObject (cacheKey, careMeasureDomains, CacheExpirationData);

                        //    // CACHE THE AVAILABILITY LIST

                        //    cacheManager.CacheObject (cacheKey, careMeasureDomains, CacheExpirationData);

                        //    // CACHE THE DICTIONARY THAT WAS CREATED

                        //    cacheManager.CacheObject ("Application." + session.EnvironmentId + ".WorkQueue.Dictionary", coreObjectDictionary, CacheExpirationData);

                        //}

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return careMeasureDomains;

        }

        public List<Server.Application.CareMeasureClass> CareMeasureClassesAvailable (Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".CareMeasureClass.Available";

            Mercury.Server.Application.CareMeasureClassCollectionResponse response = new Mercury.Server.Application.CareMeasureClassCollectionResponse ();

            List<Server.Application.CareMeasureClass> careMeasureClasses = new List<Server.Application.CareMeasureClass> ();

            Dictionary<Int64, String> coreObjectDictionary = new Dictionary<Int64, String> ();


            ClearLastException ();

            try {

                careMeasureClasses = (List<Server.Application.CareMeasureClass>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { careMeasureClasses = null; }

                if (careMeasureClasses == null) {

                    careMeasureClasses = new List<Server.Application.CareMeasureClass> ();

                    response = ApplicationClient.CareMeasureClassesAvailable (session.Token);

                    if (!response.HasException) {

                        careMeasureClasses.AddRange (response.Collection);

                        cacheManager.CacheObject (cacheKey, careMeasureClasses, CacheExpirationReference);


                        // RESERVED FOR CACHING IF THE OBJECT IS MOVED CLIENT-SIDE

                        //foreach (Server.Application.CareMeasureClass currentServerCareMeasureClass in response.Collection) {

                        //    Server.Application.CareMeasureClass careMeasureClass = new Server.Application.CareMeasureClass (this, currentServerCareMeasureClass);

                        //    careMeasureClasses.Add (careMeasureClass);


                        //    // CACHE EACH INDIVIDUAL WORK QUEUE LOADED FROM SERVER (BY ID AND NAME)

                        //    coreObjectDictionary.Add (careMeasureClass.Id, careMeasureClass.Name);

                        //    cacheManager.CacheObject ("Application." + session.EnvironmentId + ".CareMeasureClass." + careMeasureClass.Id.ToString (), careMeasureClass, CacheExpirationData);

                        //    cacheManager.CacheObject ("Application." + session.EnvironmentId + ".CareMeasureClass." + careMeasureClass.Name, careMeasureClass, CacheExpirationData);

                        //}

                        //if (careMeasureClasses.Count > 0) {

                        //    cacheManager.CacheObject (cacheKey, careMeasureClasses, CacheExpirationData);

                        //    // CACHE THE AVAILABILITY LIST

                        //    cacheManager.CacheObject (cacheKey, careMeasureClasses, CacheExpirationData);

                        //    // CACHE THE DICTIONARY THAT WAS CREATED

                        //    cacheManager.CacheObject ("Application." + session.EnvironmentId + ".WorkQueue.Dictionary", coreObjectDictionary, CacheExpirationData);

                        //}

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return careMeasureClasses;

        }

        public List<Core.Individual.CareMeasure> CareMeasuresAvailable (Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".CareMeasure.Available";

            Mercury.Server.Application.CareMeasureCollectionResponse response = new Mercury.Server.Application.CareMeasureCollectionResponse ();

            List<Core.Individual.CareMeasure> careMeasures = new List<Core.Individual.CareMeasure> ();

            Dictionary<Int64, String> coreObjectDictionary = new Dictionary<Int64, String> ();


            ClearLastException ();

            try {

                careMeasures = (List<Core.Individual.CareMeasure>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { careMeasures = null; }

                if (careMeasures == null) {

                    careMeasures = new List<Core.Individual.CareMeasure> ();

                    response = ApplicationClient.CareMeasuresAvailable (session.Token);

                    if (!response.HasException) {

                        foreach (Server.Application.CareMeasure currentServerCareMeasure in response.Collection) {

                            Core.Individual.CareMeasure careMeasure = new Core.Individual.CareMeasure (this, currentServerCareMeasure);

                            careMeasures.Add (careMeasure);


                            // CACHE EACH INDIVIDUAL WORK QUEUE LOADED FROM SERVER (BY ID AND NAME)

                            coreObjectDictionary.Add (careMeasure.Id, careMeasure.Name);

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".CareMeasure." + careMeasure.Id.ToString (), careMeasure, CacheExpirationData);

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".CareMeasure." + careMeasure.Name, careMeasure, CacheExpirationData);

                        }

                        if (careMeasures.Count > 0) {

                            cacheManager.CacheObject (cacheKey, careMeasures, CacheExpirationData);

                            // CACHE THE AVAILABILITY LIST

                            cacheManager.CacheObject (cacheKey, careMeasures, CacheExpirationData);

                            // CACHE THE DICTIONARY THAT WAS CREATED

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".CareMeasure.Dictionary", coreObjectDictionary, CacheExpirationData);

                        }

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return careMeasures;

        }

        public List<Core.Individual.CareMeasure> CareMeasuresAvailableEnabledVisible (Boolean useCaching) {

            List<Core.Individual.CareMeasure> careMeasures = CareMeasuresAvailable (useCaching);

            careMeasures = 

                (from currentCareMeasure in careMeasures

                where ((currentCareMeasure.Enabled) && (currentCareMeasure.Visible))

                select currentCareMeasure).ToList ();

            return careMeasures;

        }

        public Core.Individual.CareMeasure CareMeasureGet (Int64 careMeasureId, Boolean useCaching) {

            if (careMeasureId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".CareMeasure." + careMeasureId.ToString ();


            Core.Individual.CareMeasure careMeasure = null;

            Mercury.Server.Application.CareMeasure serverCareMeasure = null;

            ClearLastException ();

            try {

                if (useCaching) { careMeasure = (Core.Individual.CareMeasure)cacheManager.GetObject (cacheKey); }

                if (careMeasure == null) {

                    serverCareMeasure = ApplicationClient.CareMeasureGet (session.Token, careMeasureId);

                    if (serverCareMeasure != null) { careMeasure = new Mercury.Client.Core.Individual.CareMeasure (this, serverCareMeasure); }

                    if (careMeasure != null) { cacheManager.CacheObject (cacheKey, careMeasure, CacheExpirationData); }

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return careMeasure;

        }

        public Boolean CareMeasureSave (Core.Individual.CareMeasure careMeasure) {

            Mercury.Server.Application.ObjectSaveResponse response = new Mercury.Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.CareMeasureSave (session.Token, (Mercury.Server.Application.CareMeasure)careMeasure.ToServerObject ());

                if (!response.HasException) {

                    careMeasure.SetId (response.Id);

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".CareMeasure.Available");

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".CareMeasure.Dictionary");

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".CareMeasure." + careMeasure.Id.ToString ());

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".CareMeasure." + careMeasure.Name);

                }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                response.Success = false;

                SetLastException (applicationException);

            }

            return response.Success;

        }

        #endregion 


        #region Care Interventions

        public List<Core.Individual.CareIntervention> CareInterventionsAvailable (Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".CareIntervention.Available";

            Mercury.Server.Application.CareInterventionCollectionResponse response = new Mercury.Server.Application.CareInterventionCollectionResponse ();

            List<Core.Individual.CareIntervention> careInterventions = new List<Core.Individual.CareIntervention> ();

            Dictionary<Int64, String> coreObjectDictionary = new Dictionary<Int64, String> ();


            ClearLastException ();

            try {

                careInterventions = (List<Core.Individual.CareIntervention>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { careInterventions = null; }

                if (careInterventions == null) {

                    careInterventions = new List<Core.Individual.CareIntervention> ();

                    response = ApplicationClient.CareInterventionsAvailable (session.Token);

                    if (!response.HasException) {

                        foreach (Server.Application.CareIntervention currentServerCareIntervention in response.Collection) {

                            Core.Individual.CareIntervention careIntervention = new Core.Individual.CareIntervention (this, currentServerCareIntervention);

                            careInterventions.Add (careIntervention);


                            // CACHE EACH INDIVIDUAL WORK QUEUE LOADED FROM SERVER (BY ID AND NAME)

                            coreObjectDictionary.Add (careIntervention.Id, careIntervention.Name);

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".CareIntervention." + careIntervention.Id.ToString (), careIntervention, CacheExpirationData);

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".CareIntervention." + careIntervention.Name, careIntervention, CacheExpirationData);

                        }

                        if (careInterventions.Count > 0) {

                            cacheManager.CacheObject (cacheKey, careInterventions, CacheExpirationData);

                            // CACHE THE AVAILABILITY LIST

                            cacheManager.CacheObject (cacheKey, careInterventions, CacheExpirationData);

                            // CACHE THE DICTIONARY THAT WAS CREATED

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".CareIntervention.Dictionary", coreObjectDictionary, CacheExpirationData);

                        }

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return careInterventions;

        }
        
        public List<Core.Individual.CareIntervention> CareInterventionsAvailableEnabledVisible (Boolean useCaching) {

            List<Core.Individual.CareIntervention> careInterventions = CareInterventionsAvailable (useCaching);

            careInterventions =

                (from currentCareIntervention in careInterventions

                 where ((currentCareIntervention.Enabled) && (currentCareIntervention.Visible))

                 select currentCareIntervention).ToList ();

            return careInterventions;

        }

        public Core.Individual.CareIntervention CareInterventionGet (Int64 careInterventionId, Boolean useCaching) {

            if (careInterventionId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".CareIntervention." + careInterventionId.ToString ();


            Core.Individual.CareIntervention careIntervention = null;

            Mercury.Server.Application.CareIntervention serverCareIntervention = null;

            ClearLastException ();

            try {

                if (useCaching) { careIntervention = (Core.Individual.CareIntervention)cacheManager.GetObject (cacheKey); }

                if (careIntervention == null) {

                    serverCareIntervention = ApplicationClient.CareInterventionGet (session.Token, careInterventionId);

                    if (serverCareIntervention != null) { careIntervention = new Mercury.Client.Core.Individual.CareIntervention (this, serverCareIntervention); }

                    if (careIntervention != null) { cacheManager.CacheObject (cacheKey, careIntervention, CacheExpirationData); }

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return careIntervention;

        }

        public Boolean CareInterventionSave (Core.Individual.CareIntervention careIntervention) {

            Mercury.Server.Application.ObjectSaveResponse response = new Mercury.Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.CareInterventionSave (session.Token, (Mercury.Server.Application.CareIntervention)careIntervention.ToServerObject ());

                if (!response.HasException) {

                    careIntervention.SetId (response.Id);

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".CareIntervention.Available");

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".CareIntervention.Dictionary");

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".CareIntervention." + careIntervention.Id.ToString ());

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".CareIntervention." + careIntervention.Name);

                }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                response.Success = false;

                SetLastException (applicationException);

            }

            return response.Success;

        }

        #endregion 


        #region Care Plans

        public List<Core.Individual.CarePlan> CarePlansAvailable (Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".CarePlan.Available";

            Mercury.Server.Application.CarePlanCollectionResponse response = new Mercury.Server.Application.CarePlanCollectionResponse ();

            List<Core.Individual.CarePlan> carePlans = new List<Core.Individual.CarePlan> ();

            Dictionary<Int64, String> coreObjectDictionary = new Dictionary<Int64, String> ();


            ClearLastException ();

            try {

                carePlans = (List<Core.Individual.CarePlan>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { carePlans = null; }

                if (carePlans == null) {

                    carePlans = new List<Core.Individual.CarePlan> ();

                    response = ApplicationClient.CarePlansAvailable (session.Token);

                    if (!response.HasException) {

                        foreach (Server.Application.CarePlan currentServerCarePlan in response.Collection) {

                            Core.Individual.CarePlan carePlan = new Core.Individual.CarePlan (this, currentServerCarePlan);

                            carePlans.Add (carePlan);


                            // CACHE EACH INDIVIDUAL WORK QUEUE LOADED FROM SERVER (BY ID AND NAME)

                            coreObjectDictionary.Add (carePlan.Id, carePlan.Name);

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".CarePlan." + carePlan.Id.ToString (), carePlan, CacheExpirationData);

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".CarePlan." + carePlan.Name, carePlan, CacheExpirationData);

                        }

                        if (carePlans.Count > 0) {

                            cacheManager.CacheObject (cacheKey, carePlans, CacheExpirationData);

                            // CACHE THE AVAILABILITY LIST

                            cacheManager.CacheObject (cacheKey, carePlans, CacheExpirationData);

                            // CACHE THE DICTIONARY THAT WAS CREATED

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".WorkQueue.Dictionary", coreObjectDictionary, CacheExpirationData);

                        }

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return carePlans;

        }

        public Core.Individual.CarePlan CarePlanGet (Int64 carePlanId, Boolean useCaching) {

            if (carePlanId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".CarePlan." + carePlanId.ToString ();


            Core.Individual.CarePlan carePlan = null;

            Mercury.Server.Application.CarePlan serverCarePlan = null;

            ClearLastException ();

            try {

                if (useCaching) { carePlan = (Core.Individual.CarePlan)cacheManager.GetObject (cacheKey); }

                if (carePlan == null) {

                    serverCarePlan = ApplicationClient.CarePlanGet (session.Token, carePlanId);

                    if (serverCarePlan != null) { carePlan = new Mercury.Client.Core.Individual.CarePlan (this, serverCarePlan); }

                    if (carePlan != null) { cacheManager.CacheObject (cacheKey, carePlan, CacheExpirationData); }

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return carePlan;

        }

        public Boolean CarePlanSave (Core.Individual.CarePlan carePlan) {

            Mercury.Server.Application.ObjectSaveResponse response = new Mercury.Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.CarePlanSave (session.Token, (Mercury.Server.Application.CarePlan)carePlan.ToServerObject ());

                if (!response.HasException) {

                    carePlan.SetId (response.Id);

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".CarePlan.Available");

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".CarePlan.Dictionary");

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".CarePlan." + carePlan.Id.ToString ());

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".CarePlan." + carePlan.Name);

                }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                response.Success = false;

                SetLastException (applicationException);

            }

            return response.Success;

        }

        #endregion 


        #region Problem Statements

        public List<Server.Application.ProblemDomain> ProblemDomainsAvailable (Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".ProblemDomain.Available";

            Mercury.Server.Application.ProblemDomainCollectionResponse response = new Mercury.Server.Application.ProblemDomainCollectionResponse ();

            List<Server.Application.ProblemDomain> problemDomains = new List<Server.Application.ProblemDomain> ();

            Dictionary<Int64, String> coreObjectDictionary = new Dictionary<Int64, String> ();


            ClearLastException ();

            try {

                problemDomains = (List<Server.Application.ProblemDomain>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { problemDomains = null; }

                if (problemDomains == null) {

                    problemDomains = new List<Server.Application.ProblemDomain> ();

                    response = ApplicationClient.ProblemDomainsAvailable (session.Token);

                    if (!response.HasException) {

                        problemDomains.AddRange (response.Collection);

                        cacheManager.CacheObject (cacheKey, problemDomains, CacheExpirationReference);


                        // RESERVED FOR CACHING IF THE OBJECT IS MOVED CLIENT-SIDE

                        //foreach (Server.Application.ProblemDomain currentServerProblemDomain in response.Collection) {

                        //    Server.Application.ProblemDomain problemDomain = new Server.Application.ProblemDomain (this, currentServerProblemDomain);

                        //    problemDomains.Add (problemDomain);


                        //    // CACHE EACH INDIVIDUAL WORK QUEUE LOADED FROM SERVER (BY ID AND NAME)

                        //    coreObjectDictionary.Add (problemDomain.Id, problemDomain.Name);

                        //    cacheManager.CacheObject ("Application." + session.EnvironmentId + ".ProblemDomain." + problemDomain.Id.ToString (), problemDomain, CacheExpirationData);

                        //    cacheManager.CacheObject ("Application." + session.EnvironmentId + ".ProblemDomain." + problemDomain.Name, problemDomain, CacheExpirationData);

                        //}

                        //if (problemDomains.Count > 0) {

                        //    cacheManager.CacheObject (cacheKey, problemDomains, CacheExpirationData);

                        //    // CACHE THE AVAILABILITY LIST

                        //    cacheManager.CacheObject (cacheKey, problemDomains, CacheExpirationData);

                        //    // CACHE THE DICTIONARY THAT WAS CREATED

                        //    cacheManager.CacheObject ("Application." + session.EnvironmentId + ".WorkQueue.Dictionary", coreObjectDictionary, CacheExpirationData);

                        //}

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return problemDomains;

        }
        
        public List<Server.Application.ProblemClass> ProblemClassesAvailable (Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".ProblemClass.Available";

            Mercury.Server.Application.ProblemClassCollectionResponse response = new Mercury.Server.Application.ProblemClassCollectionResponse ();

            List<Server.Application.ProblemClass> problemClasses = new List<Server.Application.ProblemClass> ();

            Dictionary<Int64, String> coreObjectDictionary = new Dictionary<Int64, String> ();


            ClearLastException ();

            try {

                problemClasses = (List<Server.Application.ProblemClass>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { problemClasses = null; }

                if (problemClasses == null) {

                    problemClasses = new List<Server.Application.ProblemClass> ();

                    response = ApplicationClient.ProblemClassesAvailable (session.Token);

                    if (!response.HasException) {

                        problemClasses.AddRange (response.Collection);

                        cacheManager.CacheObject (cacheKey, problemClasses, CacheExpirationReference);


                        // RESERVED FOR CACHING IF THE OBJECT IS MOVED CLIENT-SIDE

                        //foreach (Server.Application.ProblemClass currentServerProblemClass in response.Collection) {

                        //    Server.Application.ProblemClass problemClass = new Server.Application.ProblemClass (this, currentServerProblemClass);

                        //    problemClasses.Add (problemClass);


                        //    // CACHE EACH INDIVIDUAL WORK QUEUE LOADED FROM SERVER (BY ID AND NAME)

                        //    coreObjectDictionary.Add (problemClass.Id, problemClass.Name);

                        //    cacheManager.CacheObject ("Application." + session.EnvironmentId + ".ProblemClass." + problemClass.Id.ToString (), problemClass, CacheExpirationData);

                        //    cacheManager.CacheObject ("Application." + session.EnvironmentId + ".ProblemClass." + problemClass.Name, problemClass, CacheExpirationData);

                        //}

                        //if (problemClasses.Count > 0) {

                        //    cacheManager.CacheObject (cacheKey, problemClasses, CacheExpirationData);

                        //    // CACHE THE AVAILABILITY LIST

                        //    cacheManager.CacheObject (cacheKey, problemClasses, CacheExpirationData);

                        //    // CACHE THE DICTIONARY THAT WAS CREATED

                        //    cacheManager.CacheObject ("Application." + session.EnvironmentId + ".WorkQueue.Dictionary", coreObjectDictionary, CacheExpirationData);

                        //}

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return problemClasses;

        }

        public List<Core.Individual.ProblemStatement> ProblemStatementsAvailable (Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".ProblemStatement.Available";

            Mercury.Server.Application.ProblemStatementCollectionResponse response = new Mercury.Server.Application.ProblemStatementCollectionResponse ();

            List<Core.Individual.ProblemStatement> problemStatements = new List<Core.Individual.ProblemStatement> ();

            Dictionary<Int64, String> coreObjectDictionary = new Dictionary<Int64, String> ();


            ClearLastException ();

            try {

                problemStatements = (List<Core.Individual.ProblemStatement>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { problemStatements = null; }

                if (problemStatements == null) {

                    problemStatements = new List<Core.Individual.ProblemStatement> ();

                    response = ApplicationClient.ProblemStatementsAvailable (session.Token);

                    if (!response.HasException) {

                        foreach (Server.Application.ProblemStatement currentServerProblemStatement in response.Collection) {

                            Core.Individual.ProblemStatement problemStatement = new Core.Individual.ProblemStatement (this, currentServerProblemStatement);

                            problemStatements.Add (problemStatement);


                            // CACHE EACH INDIVIDUAL WORK QUEUE LOADED FROM SERVER (BY ID AND NAME)

                            coreObjectDictionary.Add (problemStatement.Id, problemStatement.Name);

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".ProblemStatement." + problemStatement.Id.ToString (), problemStatement, CacheExpirationData);

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".ProblemStatement." + problemStatement.Name, problemStatement, CacheExpirationData);

                        }

                        if (problemStatements.Count > 0) {

                            cacheManager.CacheObject (cacheKey, problemStatements, CacheExpirationData);

                            // CACHE THE AVAILABILITY LIST

                            cacheManager.CacheObject (cacheKey, problemStatements, CacheExpirationData);

                            // CACHE THE DICTIONARY THAT WAS CREATED

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".ProblemStatement.Dictionary", coreObjectDictionary, CacheExpirationData);

                        }

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return problemStatements;

        }

        public Server.Application.ProblemDomain ProblemDomainGet (Int64 problemDomainId, Boolean useCaching) {

            if (problemDomainId == 0) { return null; }

            Server.Application.ProblemDomain problemDomain = (

                from currentProblemDomain in ProblemDomainsAvailable (useCaching)

                where currentProblemDomain.Id == problemDomainId

                select currentProblemDomain).First ();

            return problemDomain;

        }

        public Server.Application.ProblemClass ProblemClassGet (Int64 problemClassId, Boolean useCaching) {

            if (problemClassId == 0) { return null; }

            Server.Application.ProblemClass problemClass = (

                from currentProblemClass in ProblemClassesAvailable (useCaching)

                where currentProblemClass.Id == problemClassId

                select currentProblemClass).First ();

            return problemClass;

        }

        public Core.Individual.ProblemStatement ProblemStatementGet (Int64 problemStatementId, Boolean useCaching) {

            if (problemStatementId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".ProblemStatement." + problemStatementId.ToString ();


            Core.Individual.ProblemStatement problemStatement = null;

            Mercury.Server.Application.ProblemStatement serverProblemStatement = null;

            ClearLastException ();

            try {

                if (useCaching) { problemStatement = (Core.Individual.ProblemStatement)cacheManager.GetObject (cacheKey); }

                if (problemStatement == null) {

                    serverProblemStatement = ApplicationClient.ProblemStatementGet (session.Token, problemStatementId);

                    if (serverProblemStatement != null) { problemStatement = new Mercury.Client.Core.Individual.ProblemStatement (this, serverProblemStatement); }

                    if (problemStatement != null) { cacheManager.CacheObject (cacheKey, problemStatement, CacheExpirationData); }

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return problemStatement;

        }

        public Boolean ProblemStatementSave (Core.Individual.ProblemStatement problemStatement) {

            Mercury.Server.Application.ObjectSaveResponse response = new Mercury.Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.ProblemStatementSave (session.Token, (Mercury.Server.Application.ProblemStatement)problemStatement.ToServerObject ());

                if (!response.HasException) {

                    problemStatement.SetId (response.Id);

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".ProblemStatement.Available");

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".ProblemStatement.Dictionary");

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".ProblemStatement." + problemStatement.Id.ToString ());

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".ProblemStatement." + problemStatement.Name);

                }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                response.Success = false;

                SetLastException (applicationException);

            }

            return response.Success;

        }

        public List<Core.Individual.MemberProblemStatementIdentified> MemberProblemStatementIdentifiedAvailable (Int64 memberId, Boolean includeCompleted, Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".MemberProblemStatementIdentified.Available." + memberId.ToString () + "." + includeCompleted.ToString ();

            Mercury.Server.Application.MemberProblemStatementIdentifiedCollectionResponse response = new Mercury.Server.Application.MemberProblemStatementIdentifiedCollectionResponse ();

            List<Core.Individual.MemberProblemStatementIdentified> problemStatements = new List<Core.Individual.MemberProblemStatementIdentified> ();


            ClearLastException ();

            try {

                problemStatements = (List<Core.Individual.MemberProblemStatementIdentified>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { problemStatements = null; }

                if (problemStatements == null) {

                    problemStatements = new List<Core.Individual.MemberProblemStatementIdentified> ();

                    response = ApplicationClient.MemberProblemStatementIdentifiedAvailable (session.Token, memberId, includeCompleted);

                    if (!response.HasException) {

                        foreach (Server.Application.MemberProblemStatementIdentified currentServerMemberProblemStatementIdentified in response.Collection) {

                            Core.Individual.MemberProblemStatementIdentified problemStatement = new Core.Individual.MemberProblemStatementIdentified (this, currentServerMemberProblemStatementIdentified);

                            problemStatements.Add (problemStatement);

                        }

                        // CACHE EMPTY AND FULL RESULTS

                        cacheManager.CacheObject (cacheKey, problemStatements, CacheExpirationData);

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return problemStatements;

        }

        #endregion 

        
        #region Care - Care Outcome

        public List<Core.Individual.CareOutcome> CareOutcomesAvailable (Boolean useCaching) {

            List<Core.Individual.CareOutcome> careOutcomes = new List<Core.Individual.CareOutcome> ();

            Mercury.Server.Application.CareOutcomeCollectionResponse response;

            String cacheKey = "Application." + session.EnvironmentId + ".CareOutcome.Available";


            ClearLastException ();

            try {

                careOutcomes = (List<Core.Individual.CareOutcome>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { careOutcomes = null; }

                if (careOutcomes == null) {

                    careOutcomes = new List<Core.Individual.CareOutcome> ();

                    response = ApplicationClient.CareOutcomesAvailable (session.Token);

                    if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                    foreach (Server.Application.CareOutcome currentServerCareOutcome in response.Collection) {

                        Core.Individual.CareOutcome careOutcome = new Core.Individual.CareOutcome (this, currentServerCareOutcome);

                        careOutcomes.Add (careOutcome);

                    }

                    if (careOutcomes.Count > 0) { cacheManager.CacheObject (cacheKey, careOutcomes, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }




            return careOutcomes;

        }

        public List<Core.Individual.CareOutcome> CareOutcomesAvailable (Boolean useCaching, Boolean filterEnabledVisible) {

            List<Core.Individual.CareOutcome> availableCareOutcomes = CareOutcomesAvailable (useCaching);

            List<Core.Individual.CareOutcome> filteredCareOutcomes = new List<Core.Individual.CareOutcome> ();


            if (filterEnabledVisible) {

                foreach (Core.Individual.CareOutcome currentCareOutcome in availableCareOutcomes) {

                    if ((currentCareOutcome.Enabled) && (currentCareOutcome.Visible)) {

                        filteredCareOutcomes.Add (currentCareOutcome);

                    }

                }

            }

            else { filteredCareOutcomes = availableCareOutcomes; }


            return filteredCareOutcomes;

        }


        public Core.Individual.CareOutcome CareOutcomeGet (Int64 careOutcomeId, Boolean useCaching) {

            if (careOutcomeId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".CareOutcome." + careOutcomeId.ToString ();

            Core.Individual.CareOutcome careOutcome = null;

            ClearLastException ();


            try {

                careOutcome = (Core.Individual.CareOutcome)cacheManager.GetObject (cacheKey);

                if (!useCaching) { careOutcome = null; }

                if (careOutcome == null) {

                    Server.Application.CareOutcome serverCareOutcome = ApplicationClient.CareOutcomeGet (session.Token, careOutcomeId);

                    if (serverCareOutcome != null) { careOutcome = new Core.Individual.CareOutcome (this, serverCareOutcome); }

                    if (careOutcome != null) { cacheManager.CacheObject (cacheKey, careOutcome, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return careOutcome;

        }

        public Core.Individual.CareOutcome CareOutcomeGet (String careOutcomeName, Boolean useCaching) {

            if (String.IsNullOrWhiteSpace (careOutcomeName)) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".CareOutcome." + careOutcomeName.ToString ();

            Core.Individual.CareOutcome careOutcome = null;

            ClearLastException ();


            try {

                careOutcome = (Core.Individual.CareOutcome)cacheManager.GetObject (cacheKey);

                if (!useCaching) { careOutcome = null; }

                if (careOutcome == null) {

                    Server.Application.CareOutcome serverCareOutcome = ApplicationClient.CareOutcomeGetByName (session.Token, careOutcomeName);

                    if (serverCareOutcome != null) { careOutcome = new Core.Individual.CareOutcome (this, serverCareOutcome); }

                    if (careOutcome != null) { cacheManager.CacheObject (cacheKey, careOutcome, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return careOutcome;

        }

        public Boolean CareOutcomeSave (Core.Individual.CareOutcome careOutcome) {

            Server.Application.ObjectSaveResponse response = new Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.CareOutcomeSave (session.Token, (Server.Application.CareOutcome)careOutcome.ToServerObject ());

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".CareOutcome.Available");

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".CareOutcome." + careOutcome.Id.ToString ());

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".CareOutcome." + careOutcome.Name);


                if (!response.HasException) { careOutcome.SetId (response.Id); }

                else { SetLastException (new ApplicationException (response.Exception.Message)); }

            }

            catch (Exception applicationException) {

                response.Success = false;

                SetLastException (applicationException);

            }

            return response.Success;

        }

        #endregion


        #region Member Case

        public Core.Individual.Case.MemberCase MemberCaseGet (Int64 memberCaseId, Boolean useCaching) {

            if (memberCaseId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + "." + typeof (Core.Individual.Case.MemberCase).ToString () + "." + memberCaseId.ToString ();

            Core.Individual.Case.MemberCase memberCase = null;

            ClearLastException ();


            try {

                memberCase = (Core.Individual.Case.MemberCase)cacheManager.GetObject (cacheKey);

                if (!useCaching) { memberCase = null; }

                if (memberCase == null) {

                    Server.Application.MemberCase serverMemberCase = ApplicationClient.MemberCaseGet (session.Token, memberCaseId);

                    if (serverMemberCase != null) { memberCase = new Core.Individual.Case.MemberCase (this, serverMemberCase); }

                    if (memberCase != null) { cacheManager.CacheObject (cacheKey, memberCase, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberCase;

        }

        public Core.Individual.Case.MemberCase MemberCaseCreate (Int64 memberId, Boolean ignoreExisting) {

            Core.Individual.Case.MemberCase memberCase = null;

            Server.Application.MemberCaseCreateResponse response;

            ClearLastException ();

            try {

                response = ApplicationClient.MemberCaseCreate (session.Token, memberId, ignoreExisting);

                if (!response.HasException) {

                    memberCase = new Core.Individual.Case.MemberCase (this, response.MemberCase);

                }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberCase;

        }


        public List<Core.Individual.Case.Views.MemberCaseSummary> MemberCaseSummaryGetByMemberPage (Int64 memberId, Int32 initialRow, Int32 count, Boolean showClosed) {
 
            Mercury.Server.Application.MemberCaseSummaryCollectionResponse response;

            List<Core.Individual.Case.Views.MemberCaseSummary> results = null;


            ClearLastException ();


            try {

                response = ApplicationClient.MemberCaseSummaryGetByMemberPage (session.Token, memberId, initialRow, count, showClosed);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                results = new List<Core.Individual.Case.Views.MemberCaseSummary> ();

                if (!response.HasException) {

                    foreach (Server.Application.MemberCaseSummary currentServerObject in response.Collection) {

                        Core.Individual.Case.Views.MemberCaseSummary summary = new Core.Individual.Case.Views.MemberCaseSummary (this, currentServerObject);

                        results.Add (summary);

                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return results;

        }

        public List<Core.Individual.Case.Views.MemberCaseSummary> MemberCaseSummaryGetByUserWorkTeamsPage (Int32 initialRow, Int32 count, Boolean showClosed) {

            return MemberCaseSummaryGetByUserWorkTeamsPage (session.SecurityAuthorityId, session.UserAccountId, initialRow, count, showClosed);

        }


        public List<Core.Individual.Case.Views.MemberCaseSummary> MemberCaseSummaryGetByUserWorkTeamsPage (Int64 securityAuthorityId, String userAccountId, Int32 initialRow, Int32 count, Boolean showClosed) {
            
            Mercury.Server.Application.MemberCaseSummaryCollectionResponse response;

            List<Core.Individual.Case.Views.MemberCaseSummary> results = null;


            ClearLastException ();


            try {

                response = ApplicationClient.MemberCaseSummaryGetByUserWorkTeamsPage (session.Token, securityAuthorityId, userAccountId, initialRow, count, showClosed);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                results = new List<Core.Individual.Case.Views.MemberCaseSummary> ();

                if (!response.HasException) {

                    foreach (Server.Application.MemberCaseSummary currentServerObject in response.Collection) {

                        Core.Individual.Case.Views.MemberCaseSummary summary = new Core.Individual.Case.Views.MemberCaseSummary (this, currentServerObject);

                        results.Add (summary);

                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return results;

        }

        public List<Core.Individual.Case.Views.MemberCaseSummary> MemberCaseSummaryGetByAssignedToUserPage (Int64 securityAuthorityId, String userAccountId, Int32 initialRow, Int32 count, Boolean showClosed) {

            Mercury.Server.Application.MemberCaseSummaryCollectionResponse response;

            List<Core.Individual.Case.Views.MemberCaseSummary> results = null;


            ClearLastException ();


            try {

                response = ApplicationClient.MemberCaseSummaryGetByAssignedToUserPage (session.Token, securityAuthorityId, userAccountId, initialRow, count, showClosed);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                results = new List<Core.Individual.Case.Views.MemberCaseSummary> ();

                if (!response.HasException) {

                    foreach (Server.Application.MemberCaseSummary currentServerObject in response.Collection) {

                        Core.Individual.Case.Views.MemberCaseSummary summary = new Core.Individual.Case.Views.MemberCaseSummary (this, currentServerObject);

                        results.Add (summary);

                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return results;

        }


        public List<Core.Individual.Case.Views.MemberCaseSummary> MemberCaseSummaryGetByAssignedToUserPage (Int32 initialRow, Int32 count, Boolean showClosed) {

            return MemberCaseSummaryGetByAssignedToUserPage (session.SecurityAuthorityId, session.UserAccountId, initialRow, count, showClosed);

        }


        public List<Core.Individual.Case.Views.MemberCaseLoadSummary> MemberCaseLoadSummaryGetByUser (Int64 securityAuthorityId, String userAccountId, Boolean showClosed) {

            Mercury.Server.Application.MemberCaseLoadSummaryCollectionResponse response;

            List<Core.Individual.Case.Views.MemberCaseLoadSummary> results = null;


            ClearLastException ();


            try {

                response = ApplicationClient.MemberCaseLoadSummaryGetByUser (session.Token, securityAuthorityId, userAccountId, showClosed);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                results = new List<Core.Individual.Case.Views.MemberCaseLoadSummary> ();

                if (!response.HasException) {

                    foreach (Server.Application.MemberCaseLoadSummary currentServerObject in response.Collection) {

                        Core.Individual.Case.Views.MemberCaseLoadSummary summary = new Core.Individual.Case.Views.MemberCaseLoadSummary (this, currentServerObject);

                        results.Add (summary);

                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return results;

        }

        public List<Core.Individual.Case.Views.MemberCaseLoadSummary> MemberCaseLoadSummaryGetByWorkTeam (Int64 workTeamId, Boolean showClosed) {

            Mercury.Server.Application.MemberCaseLoadSummaryCollectionResponse response;

            List<Core.Individual.Case.Views.MemberCaseLoadSummary> results = null;


            ClearLastException ();


            try {

                response = ApplicationClient.MemberCaseLoadSummaryGetByWorkTeam (session.Token, workTeamId, showClosed);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                results = new List<Core.Individual.Case.Views.MemberCaseLoadSummary> ();

                if (!response.HasException) {

                    foreach (Server.Application.MemberCaseLoadSummary currentServerObject in response.Collection) {

                        Core.Individual.Case.Views.MemberCaseLoadSummary summary = new Core.Individual.Case.Views.MemberCaseLoadSummary (this, currentServerObject);

                        results.Add (summary);

                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return results;

        }

        /* DAVID: TODO: ADDED ON 9/13/2011 (START) */

        //public Core.Individual.Case.MemberCaseAudit MemberCaseAuditGet (Int64 memberCaseAuditHistoryId, Boolean useCaching) {

        //    if (memberCaseAuditHistoryId == 0) { return null; }


        //    String cacheKey = "Application." + session.EnvironmentId + "." + typeof (Core.Individual.Case.MemberCaseAudit).ToString () + "." + memberCaseAuditHistoryId.ToString ();

        //    Core.Individual.Case.MemberCaseAudit memberCaseAudit = null;

        //    ClearLastException ();

        //    try {

        //        memberCaseAudit = (Core.Individual.Case.MemberCaseAudit)cacheManager.GetObject (cacheKey);

        //        if (!useCaching) { memberCaseAudit = null; }

        //        if (memberCaseAudit == null) {

        //            Server.Application.MemberCaseAudit serverMemberCaseAudit = ApplicationClient.MemberCaseAuditGet (session.Token, memberCaseAuditHistoryId);

        //            if (serverMemberCaseAudit != null) { memberCaseAudit = new Mercury.Client.Core.Individual.Case.MemberCaseAudit (this, serverMemberCaseAudit); }

        //            if (memberCaseAudit != null) { cacheManager.CacheObject (cacheKey, memberCaseAudit, CacheExpirationData); }

        //        }

        //    }

        //    catch (Exception applicationException) {

        //        SetLastException (applicationException);

        //    }

        //    return memberCaseAudit;

        //}

        public List<Core.Individual.Case.MemberCaseAudit> MemberCaseAuditHistoryGetByMemberCaseIdPage (Int64 memberCaseId, Int64 initialRow, Int64 count, Boolean useCaching) {

            Mercury.Server.Application.MemberCaseAuditCollectionResponse response;

            List<Core.Individual.Case.MemberCaseAudit> results = null;


            ClearLastException ();


            try {

                response = ApplicationClient.MemberCaseAuditHistoryGetByMemberCaseIdPage (session.Token, memberCaseId, initialRow, count);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                results = new List<Core.Individual.Case.MemberCaseAudit> ();

                if (!response.HasException) {

                    foreach (Server.Application.MemberCaseAudit curentServerObject in response.Collection) {

                        Core.Individual.Case.MemberCaseAudit memberCaseAudit = new Core.Individual.Case.MemberCaseAudit (this, curentServerObject);

                        results.Add (memberCaseAudit);

                    } /* END FOREACH */

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return results;

        }

        public Int64 MemberCaseAuditHistoryGetCount (Int64 memberCaseId, Boolean useCaching) {

            Int64 itemCount = 0;


            // CREATE THE CACHE KEY

            String cacheKey = "Application." + session.EnvironmentId + ".Core.Individual.Case.MemberCaseAudit.GetCount";

            cacheKey = cacheKey + ".MemberCaseId." + memberCaseId.ToString ();


            ClearLastException ();

            try {

                Int64.TryParse (Convert.ToString (cacheManager.GetObject (cacheKey)), out itemCount);

                if (!useCaching) { itemCount = 0; }

                if (itemCount == 0) {

                    itemCount = ApplicationClient.MemberCaseAuditHistoryGetCount (session.Token, memberCaseId);

                    cacheManager.CacheObject (cacheKey, itemCount, CacheExpirationData);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return itemCount;

        }

        //public Boolean MemberCaseAuditSave (Core.Individual.Case.MemberCaseAudit memberCaseAudit) {

        //    String cacheKey = "Application." + session.EnvironmentId + "." + typeof (Core.Individual.Case.MemberCaseAudit).ToString () + "." + memberCaseAudit.Id.ToString ();

        //    Server.Application.ObjectSaveResponse response = new Server.Application.ObjectSaveResponse ();

        //    ClearLastException ();

        //    try {

        //        cacheManager.RemoveObject (cacheKey); // FLUSH CACHE FOR SAVE

        //        response = ApplicationClient.MemberCaseAuditSave (session.Token, (Server.Application.MemberCaseAudit)memberCaseAudit.ToServerObject ());


        //        if (!response.HasException) { memberCaseAudit.SetId (response.Id); }

        //        else { SetLastException (new ApplicationException (response.Exception.Message)); }

        //    }

        //    catch (Exception applicationException) {

        //        response.Success = false;

        //        SetLastException (applicationException);

        //    }

        //    return response.Success;

        //}

        /* DAVID: TODO: ADDED ON 9/13/2011 ( END ) */


        /* ADDED ON 10/25/2011 (START) */

        public Server.Application.MemberCaseModificationResponse MemberCaseProblemClass_AssignToUser (Core.Individual.Case.MemberCase memberCase, Int64 memberCaseProblemClassId, Int64 assignToSecurityAuthorityId, String assignToUserAccountId, String assignToUserAccountName, String assignToUserDisplayName) {

            // GET REFERENCE TO MEMBER CASE MODIFICATION RESPONSE

            Server.Application.MemberCaseModificationResponse response = new Server.Application.MemberCaseModificationResponse ();

            // CLEAR LAST EXCEPTION

            ClearLastException ();

            // TRY TO ASSIGN MEMBER CASE PROBLEM CLASS TO USER

            try {

                // ASSIGN MEMBER CASE PROBLEM CLASS TO USER

                response = ApplicationClient.MemberCaseProblemClass_AssignToUser (session.Token, ((Server.Application.MemberCase)memberCase.ToServerObject ()),

                    memberCaseProblemClassId, assignToSecurityAuthorityId, assignToUserAccountId, assignToUserAccountName, assignToUserDisplayName);

            }

            // CATCH APPLICATION EXCEPTION

            catch (Exception applicationException) {

                response.HasException = true;

                response.Exception = new Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;


                SetLastException (applicationException);

            }

            // RETURN RESPONSE

            return response;

        }

        public Server.Application.MemberCaseModificationResponse MemberCaseProblemClass_AssignToProvider (Core.Individual.Case.MemberCase memberCase, Int64 memberCaseProblemClassId, Int64 assignToProviderId) {

            // GET REFERENCE TO MEMBER CASE MODIFICATION RESPONSE

            Server.Application.MemberCaseModificationResponse response = new Server.Application.MemberCaseModificationResponse ();

            // CLEAR LAST EXCEPTION

            ClearLastException ();

            // TRY TO ASSIGN MEMBER CASE PROBLEM CLASS TO PROVIDER

            try {

                // ASSIGN MEMBER CASE PROBLEM CLASS TO PROVIDER

                response = ApplicationClient.MemberCaseProblemClass_AssignToProvider (session.Token, ((Server.Application.MemberCase)memberCase.ToServerObject ()),

                    memberCaseProblemClassId, assignToProviderId);

            }

            // CATCH APPLICATION EXCEPTION

            catch (Exception applicationException) {

                response.HasException = true;

                response.Exception = new Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;


                SetLastException (applicationException);

            }

            // RETURN RESPONSE

            return response;

        }

        /* ADDED ON 10/25/2011 ( END ) */


        /* ADDED ON 11/10/11 (START) */

        public List<Client.Core.Individual.Case.Views.MemberCaseCarePlanSummary> MemberCaseCarePlanSummaryGetByMemberCase (Int64 memberCaseId, Boolean useCaching) {

            if (memberCaseId == 0) { return null; }

            Mercury.Server.Application.MemberCaseCarePlanSummaryCollectionResponse response;

            List<Client.Core.Individual.Case.Views.MemberCaseCarePlanSummary> results = null;


            ClearLastException ();


            try {

                response = ApplicationClient.MemberCaseCarePlanSummaryGetByMemberCase (session.Token, memberCaseId, useCaching);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                results = new List<Core.Individual.Case.Views.MemberCaseCarePlanSummary> ();

                if (!response.HasException) {

                    foreach (Server.Application.MemberCaseCarePlanSummary currentServerObject in response.Collection) {

                        Core.Individual.Case.Views.MemberCaseCarePlanSummary summary = new Core.Individual.Case.Views.MemberCaseCarePlanSummary (this, currentServerObject);

                        results.Add (summary);

                    } /* END FOREACH */

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return results;

        }

        /* ADDED ON 11/10/11 ( END ) */


        public Server.Application.MemberCaseModificationResponse MemberCase_SetDescription (Core.Individual.Case.MemberCase memberCase, String description) {

            Server.Application.MemberCaseModificationResponse response = new Server.Application.MemberCaseModificationResponse (); ;

            ClearLastException ();

            try {

                response = ApplicationClient.MemberCase_SetDescription (session.Token, ((Server.Application.MemberCase)memberCase.ToServerObject ()), description);

            }

            catch (Exception applicationException) {

                response.HasException = true;

                response.Exception = new Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;


                SetLastException (applicationException);

            }

            return response;

        }

        public Server.Application.MemberCaseModificationResponse MemberCase_SetReferenceNumber (Core.Individual.Case.MemberCase memberCase, String referenceNumber) {

            Server.Application.MemberCaseModificationResponse response = new Server.Application.MemberCaseModificationResponse (); ;

            ClearLastException ();

            try {

                response = ApplicationClient.MemberCase_SetReferenceNumber (session.Token, ((Server.Application.MemberCase)memberCase.ToServerObject ()), referenceNumber);

            }

            catch (Exception applicationException) {

                response.HasException = true;

                response.Exception = new Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;


                SetLastException (applicationException);

            }

            return response;

        }

        public Server.Application.MemberCaseModificationResponse MemberCase_Lock (Core.Individual.Case.MemberCase memberCase) {

            Server.Application.MemberCaseModificationResponse response = new Server.Application.MemberCaseModificationResponse (); ;

            ClearLastException ();

            try {

                response = ApplicationClient.MemberCase_Lock (session.Token, ((Server.Application.MemberCase)memberCase.ToServerObject ()));

            }

            catch (Exception applicationException) {

                response.HasException = true;

                response.Exception = new Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;


                SetLastException (applicationException);

            }

            return response;

        }

        public Server.Application.MemberCaseModificationResponse MemberCase_Unlock (Core.Individual.Case.MemberCase memberCase) {

            Server.Application.MemberCaseModificationResponse response = new Server.Application.MemberCaseModificationResponse (); ;

            ClearLastException ();

            try {

                response = ApplicationClient.MemberCase_Unlock (session.Token, ((Server.Application.MemberCase)memberCase.ToServerObject ()));

            }

            catch (Exception applicationException) {

                response.HasException = true;

                response.Exception = new Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;


                SetLastException (applicationException);

            }

            return response;

        }

        public Server.Application.MemberCaseModificationResponse MemberCase_AssignToWorkTeam (Core.Individual.Case.MemberCase memberCase, Int64 workTeamId) {

            Server.Application.MemberCaseModificationResponse response = new Server.Application.MemberCaseModificationResponse (); 

            ClearLastException ();

            try {

                response = ApplicationClient.MemberCase_AssignToWorkTeam (session.Token, ((Server.Application.MemberCase)memberCase.ToServerObject ()), workTeamId);

            }

            catch (Exception applicationException) {

                response.HasException = true;

                response.Exception = new Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;


                SetLastException (applicationException);

            }

            return response;

        }

        public Server.Application.MemberCaseModificationResponse MemberCase_AssignToUser (Core.Individual.Case.MemberCase memberCase, Int64 securityAuthorityId, String userAccountId, String userAccountName, String userDisplayName) {

            Server.Application.MemberCaseModificationResponse response = new Server.Application.MemberCaseModificationResponse (); 

            ClearLastException ();

            try {

                response = ApplicationClient.MemberCase_AssignToUser (session.Token, ((Server.Application.MemberCase)memberCase.ToServerObject ()),

                    securityAuthorityId, userAccountId, userAccountName, userDisplayName);

            }

            catch (Exception applicationException) {

                response.HasException = true;

                response.Exception = new Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;


                SetLastException (applicationException);

            }

            return response;

        }


        public Server.Application.MemberCaseModificationResponse MemberCase_AddProblemStatement (Core.Individual.Case.MemberCase memberCase, Int64 problemStatementId, Boolean isSingleInstance) {

            Server.Application.MemberCaseModificationResponse response = new Server.Application.MemberCaseModificationResponse (); ;

            ClearLastException ();

            try {

                response = ApplicationClient.MemberCase_AddProblemStatement (session.Token, ((Server.Application.MemberCase)memberCase.ToServerObject ()), problemStatementId, isSingleInstance);

            }

            catch (Exception applicationException) {

                response.HasException = true;

                response.Exception = new Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;


                SetLastException (applicationException);

            }

            return response;

        }

        public Server.Application.MemberCaseModificationResponse MemberCase_DeleteProblemStatement (Core.Individual.Case.MemberCase memberCase, Int64 memberCaseProblemCarePlanId) {

            Server.Application.MemberCaseModificationResponse response = new Server.Application.MemberCaseModificationResponse (); ;

            ClearLastException ();

            try {

                response = ApplicationClient.MemberCase_DeleteProblemStatement (session.Token, ((Server.Application.MemberCase)memberCase.ToServerObject ()), memberCaseProblemCarePlanId);

            }

            catch (Exception applicationException) {

                response.HasException = true;

                response.Exception = new Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;


                SetLastException (applicationException);

            }

            return response;

        }


        public Server.Application.MemberCaseModificationResponse MemberCaseCarePlanGoal_Delete (Core.Individual.Case.MemberCase memberCase, Int64 memberCaseCarePlanGoalId) {

            Server.Application.MemberCaseModificationResponse response = new Server.Application.MemberCaseModificationResponse (); ;

            ClearLastException ();

            try {

                response = ApplicationClient.MemberCaseCarePlanGoal_Delete (session.Token, ((Server.Application.MemberCase)memberCase.ToServerObject ()), memberCaseCarePlanGoalId);

            }

            catch (Exception applicationException) {

                response.HasException = true;

                response.Exception = new Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;


                SetLastException (applicationException);

            }

            return response;

        }

        public Server.Application.MemberCaseModificationResponse MemberCaseCarePlanGoal_Add (Core.Individual.Case.MemberCase memberCase, Int64 memberCaseCarePlanId, Int64 copyCarePlanGoalId, String carePlanGoalName, Int64 careMeasureId) {

            Server.Application.MemberCaseModificationResponse response = new Server.Application.MemberCaseModificationResponse (); ;

            ClearLastException ();

            try {

                response = ApplicationClient.MemberCaseCarePlanGoal_Add (session.Token, ((Server.Application.MemberCase)memberCase.ToServerObject ()), memberCaseCarePlanId, copyCarePlanGoalId, carePlanGoalName, careMeasureId);

            }

            catch (Exception applicationException) {

                response.HasException = true;

                response.Exception = new Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;


                SetLastException (applicationException);

            }

            return response;

        }

        public Server.Application.MemberCaseModificationResponse MemberCaseCarePlanGoal_Update (Core.Individual.Case.MemberCase memberCase, Int64 memberCaseCarePlanGoalId) { 

            Server.Application.MemberCaseModificationResponse response = new Server.Application.MemberCaseModificationResponse (); ;

            ClearLastException ();

            try {

                response = ApplicationClient.MemberCaseCarePlanGoal_Update (session.Token, ((Server.Application.MemberCase)memberCase.ToServerObject ()), memberCaseCarePlanGoalId);

            }

            catch (Exception applicationException) {

                response.HasException = true;

                response.Exception = new Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;


                SetLastException (applicationException);

            }

            return response;

        }

        public Server.Application.MemberCaseModificationResponse MemberCaseCarePlanGoal_AddCareIntervention (Core.Individual.Case.MemberCase memberCase, Int64 memberCaseCarePlanGoalId, Int64 careInterventionId, Boolean isSingleInstance) {

            Server.Application.MemberCaseModificationResponse response = new Server.Application.MemberCaseModificationResponse (); ;

            ClearLastException ();

            try {

                response = ApplicationClient.MemberCaseCarePlanGoal_AddCareIntervention (session.Token, ((Server.Application.MemberCase)memberCase.ToServerObject ()), memberCaseCarePlanGoalId, careInterventionId, isSingleInstance);

            }

            catch (Exception applicationException) {

                response.HasException = true;

                response.Exception = new Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;


                SetLastException (applicationException);

            }

            return response;

        }


        public Server.Application.MemberCaseModificationResponse MemberCaseCarePlanIntervention_Delete (Core.Individual.Case.MemberCase memberCase, Int64 memberCaseCarePlanInterventionId) {

            Server.Application.MemberCaseModificationResponse response = new Server.Application.MemberCaseModificationResponse (); ;

            ClearLastException ();

            try {

                response = ApplicationClient.MemberCaseCarePlanIntervention_Delete (session.Token, ((Server.Application.MemberCase)memberCase.ToServerObject ()), memberCaseCarePlanInterventionId);

            }

            catch (Exception applicationException) {

                response.HasException = true;

                response.Exception = new Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;


                SetLastException (applicationException);

            }

            return response;

        }

        public Server.Application.MemberCaseModificationResponse MemberCaseCarePlanIntervention_Add (Core.Individual.Case.MemberCase memberCase, Int64 memberCaseCarePlanId, Int64 copyCareInterventionId, String carePlanInterventionName) {

            Server.Application.MemberCaseModificationResponse response = new Server.Application.MemberCaseModificationResponse (); ;

            ClearLastException ();

            try {

                response = ApplicationClient.MemberCaseCarePlanIntervention_Add (session.Token, ((Server.Application.MemberCase)memberCase.ToServerObject ()), memberCaseCarePlanId, copyCareInterventionId, carePlanInterventionName);

            }

            catch (Exception applicationException) {

                response.HasException = true;

                response.Exception = new Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;


                SetLastException (applicationException);

            }

            return response;

        }

        public Server.Application.MemberCaseModificationResponse MemberCaseCarePlanIntervention_Update (Core.Individual.Case.MemberCase memberCase, Int64 memberCaseCarePlanInterventionId) {

            Server.Application.MemberCaseModificationResponse response = new Server.Application.MemberCaseModificationResponse (); ;

            ClearLastException ();

            try {

                response = ApplicationClient.MemberCaseCarePlanIntervention_Update (session.Token, ((Server.Application.MemberCase)memberCase.ToServerObject ()), memberCaseCarePlanInterventionId);

            }

            catch (Exception applicationException) {

                response.HasException = true;

                response.Exception = new Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;


                SetLastException (applicationException);

            }

            return response;

        }

        #endregion 


        #region Case - Member Case Care Plan Assessment 
        
        public Boolean MemberCaseCarePlanAssessmentSave (Core.Individual.Case.MemberCaseCarePlanAssessment memberCaseCarePlanAssessment) {

            Mercury.Server.Application.ObjectSaveResponse response = new Mercury.Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.MemberCaseCarePlanAssessmentSave (session.Token, (Mercury.Server.Application.MemberCaseCarePlanAssessment)memberCaseCarePlanAssessment.ToServerObject ());

                if (!response.HasException) {

                    memberCaseCarePlanAssessment.SetId (response.Id);

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".MemberCaseCarePlanAssessment.Available");

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".MemberCaseCarePlanAssessment.Dictionary");

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".MemberCaseCarePlanAssessment." + memberCaseCarePlanAssessment.Id.ToString ());

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".MemberCaseCarePlanAssessment." + memberCaseCarePlanAssessment.Name);

                }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                response.Success = false;

                SetLastException (applicationException);

            }

            return response.Success;

        }

        #endregion 

        #endregion


        #region Core - Insurer

        public Core.Insurer.Insurer InsurerGet (Int64 insurerId, Boolean useCaching) {

            if (insurerId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + "." + typeof (Core.Insurer.Insurer).ToString () + "." + insurerId.ToString ();

            Core.Insurer.Insurer insurer = null;

            ClearLastException ();


            try {

                insurer = (Core.Insurer.Insurer)cacheManager.GetObject (cacheKey);

                if (!useCaching) { insurer = null; }

                if (insurer == null) {

                    Server.Application.Insurer serverInsurer = ApplicationClient.InsurerGet (session.Token, insurerId);

                    if (serverInsurer != null) { insurer = new Core.Insurer.Insurer (this, serverInsurer); }

                    if (insurer != null) { cacheManager.CacheObject (cacheKey, insurer, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return insurer;

        }
        
        #region Benefit Plan

        public List<Core.Insurer.BenefitPlan> BenefitPlansAvailable (Boolean useCaching) {

            List<Core.Insurer.BenefitPlan> benefitPlans = new List<Core.Insurer.BenefitPlan> ();

            Mercury.Server.Application.BenefitPlanCollectionResponse response;

            String cacheKey = "Application." + session.EnvironmentId + ".BenefitPlan.Available";


            ClearLastException ();

            try {

                benefitPlans = (List<Core.Insurer.BenefitPlan>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { benefitPlans = null; }

                if (benefitPlans == null) {

                    benefitPlans = new List<Core.Insurer.BenefitPlan> ();

                    response = ApplicationClient.BenefitPlansAvailable (session.Token);

                    if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                    foreach (Server.Application.BenefitPlan currentServerBenefitPlan in response.Collection) {

                        Core.Insurer.BenefitPlan benefitPlan = new Core.Insurer.BenefitPlan (this, currentServerBenefitPlan);

                        benefitPlans.Add (benefitPlan);

                    }

                    if (benefitPlans.Count > 0) { cacheManager.CacheObject (cacheKey, benefitPlans, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return benefitPlans;

        }

        public List<Core.Insurer.BenefitPlan> BenefitPlansAvailableByProgram (Int64 programId, Boolean useCaching) {

            List<Core.Insurer.BenefitPlan> available = BenefitPlansAvailable (useCaching);

            List<Core.Insurer.BenefitPlan> filtered = new List<Core.Insurer.BenefitPlan> ();


            foreach (Core.Insurer.BenefitPlan currentObject in available) {

                if (currentObject.ProgramId == programId) {

                    filtered.Add (currentObject);

                }

            }


            return filtered;

        }

        public Dictionary<Int64, String> BenefitPlanDictionaryByProgram (Int64 programId, Boolean useCaching) {

            List<Core.Insurer.BenefitPlan> available = BenefitPlansAvailable (useCaching);

            Dictionary<Int64, String> filtered = new Dictionary<Int64, String> ();


            foreach (Core.Insurer.BenefitPlan currentObject in available) {

                if (currentObject.ProgramId == programId) {

                    filtered.Add (currentObject.Id, currentObject.Name);

                }

            }


            return filtered;

        }

        public Core.Insurer.BenefitPlan BenefitPlanGet (Int64 benefitPlanId, Boolean useCaching) {

            if (benefitPlanId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + "." + typeof (Core.Insurer.BenefitPlan).ToString () + "." + benefitPlanId.ToString ();

            Core.Insurer.BenefitPlan benefitPlan = null;

            ClearLastException ();


            try {

                benefitPlan = (Core.Insurer.BenefitPlan)cacheManager.GetObject (cacheKey);

                if (!useCaching) { benefitPlan = null; }

                if (benefitPlan == null) {

                    Server.Application.BenefitPlan serverBenefitPlan = ApplicationClient.BenefitPlanGet (session.Token, benefitPlanId);

                    if (serverBenefitPlan != null) { benefitPlan = new Core.Insurer.BenefitPlan (this, serverBenefitPlan); }

                    if (benefitPlan != null) { cacheManager.CacheObject (cacheKey, benefitPlan, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return benefitPlan;

        }

        public Core.Insurer.BenefitPlan BenefitPlanGet (String benefitPlanName, Boolean useCaching) {

            if (String.IsNullOrWhiteSpace (benefitPlanName)) { return null; }

            Core.Insurer.BenefitPlan benefitPlan = null;


            foreach (Core.Insurer.BenefitPlan currentBenefitPlan in BenefitPlansAvailable (true)) {

                if (currentBenefitPlan.Name == benefitPlanName) {

                    benefitPlan = currentBenefitPlan;

                    break;

                }

            }

            return benefitPlan;

        }

        #endregion 


        #region Contract

        public Mercury.Client.Core.Insurer.Contract ContractGet (Int64 contractId, Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".Contract." + contractId.ToString ();

            Mercury.Client.Core.Insurer.Contract contract = null;


            ClearLastException ();

            try {

                contract = (Core.Insurer.Contract)cacheManager.GetObject (cacheKey);

                if (!useCaching) { contract = null; }

                if (contract == null) {

                    Server.Application.Contract serverContract;

                    serverContract = ApplicationClient.ContractGet (session.Token, contractId);

                    if (serverContract != null) {

                        contract = new Mercury.Client.Core.Insurer.Contract (this, serverContract);

                        cacheManager.CacheObject (cacheKey, contract, CacheExpirationData);

                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return contract;

        }

        #endregion 


        #region Coverage Level

        public List<Core.Insurer.CoverageLevel> CoverageLevelsAvailable (Boolean useCaching) {

            List<Core.Insurer.CoverageLevel> coverageLevels = new List<Core.Insurer.CoverageLevel> ();

            Mercury.Server.Application.CoverageLevelCollectionResponse response;

            String cacheKey = "Application." + session.EnvironmentId + "." + typeof (Core.Insurer.CoverageLevel).ToString () + ".Available";


            ClearLastException ();

            try {

                coverageLevels = (List<Core.Insurer.CoverageLevel>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { coverageLevels = null; }

                if (coverageLevels == null) {

                    coverageLevels = new List<Core.Insurer.CoverageLevel> ();

                    response = ApplicationClient.CoverageLevelsAvailable (session.Token);

                    if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                    foreach (Server.Application.CoverageLevel currentServerCoverageLevel in response.Collection) {

                        Core.Insurer.CoverageLevel coverageLevel = new Core.Insurer.CoverageLevel (this, currentServerCoverageLevel);

                        coverageLevels.Add (coverageLevel);

                    }

                    if (coverageLevels.Count > 0) { cacheManager.CacheObject (cacheKey, coverageLevels, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }




            return coverageLevels;

        }

        public Core.Insurer.CoverageLevel CoverageLevelGet (Int64 coverageLevelId, Boolean useCaching) {

            if (coverageLevelId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + "." + typeof (Core.Insurer.CoverageLevel).ToString () + "." + coverageLevelId.ToString ();

            Core.Insurer.CoverageLevel coverageLevel = null;

            ClearLastException ();


            try {

                coverageLevel = (Core.Insurer.CoverageLevel)cacheManager.GetObject (cacheKey);

                if (!useCaching) { coverageLevel = null; }

                if (coverageLevel == null) {

                    Server.Application.CoverageLevel serverCoverageLevel = ApplicationClient.CoverageLevelGet (session.Token, coverageLevelId);

                    if (serverCoverageLevel != null) { coverageLevel = new Core.Insurer.CoverageLevel (this, serverCoverageLevel); }

                    if (coverageLevel != null) { cacheManager.CacheObject (cacheKey, coverageLevel, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return coverageLevel;

        }

        public Core.Insurer.CoverageLevel CoverageLevelGet (String coverageLevelName, Boolean useCaching) {

            if (String.IsNullOrWhiteSpace (coverageLevelName)) { return null; }

            Core.Insurer.CoverageLevel coverageLevel = null;


            foreach (Core.Insurer.CoverageLevel currentCoverageLevel in CoverageLevelsAvailable (true)) {

                if (currentCoverageLevel.Name == coverageLevelName) {

                    coverageLevel = currentCoverageLevel;

                    break;

                }

            }

            return coverageLevel;

        }

        #endregion 


        #region Coverage Type

        public List<Core.Insurer.CoverageType> CoverageTypesAvailable (Boolean useCaching) {

            List<Core.Insurer.CoverageType> coverageTypes = new List<Core.Insurer.CoverageType> ();

            Mercury.Server.Application.CoverageTypeCollectionResponse response;

            String cacheKey = "Application." + session.EnvironmentId + "." + typeof (Core.Insurer.CoverageType).ToString () + ".Available";


            ClearLastException ();

            try {

                coverageTypes = (List<Core.Insurer.CoverageType>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { coverageTypes = null; }

                if (coverageTypes == null) {

                    coverageTypes = new List<Core.Insurer.CoverageType> ();

                    response = ApplicationClient.CoverageTypesAvailable (session.Token);

                    if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                    foreach (Server.Application.CoverageType currentServerCoverageType in response.Collection) {

                        Core.Insurer.CoverageType coverageType = new Core.Insurer.CoverageType (this, currentServerCoverageType);

                        coverageTypes.Add (coverageType);

                    }

                    if (coverageTypes.Count > 0) { cacheManager.CacheObject (cacheKey, coverageTypes, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }




            return coverageTypes;

        }

        public Core.Insurer.CoverageType CoverageTypeGet (Int64 coverageTypeId, Boolean useCaching) {

            if (coverageTypeId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + "." + typeof (Core.Insurer.CoverageType).ToString () + "." + coverageTypeId.ToString ();

            Core.Insurer.CoverageType coverageType = null;

            ClearLastException ();


            try {

                coverageType = (Core.Insurer.CoverageType)cacheManager.GetObject (cacheKey);

                if (!useCaching) { coverageType = null; }

                if (coverageType == null) {

                    Server.Application.CoverageType serverCoverageType = ApplicationClient.CoverageTypeGet (session.Token, coverageTypeId);

                    if (serverCoverageType != null) { coverageType = new Core.Insurer.CoverageType (this, serverCoverageType); }

                    if (coverageType != null) { cacheManager.CacheObject (cacheKey, coverageType, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return coverageType;

        }

        public Core.Insurer.CoverageType CoverageTypeGet (String coverageTypeName, Boolean useCaching) {

            if (String.IsNullOrWhiteSpace (coverageTypeName)) { return null; }

            Core.Insurer.CoverageType coverageType = null;


            foreach (Core.Insurer.CoverageType currentCoverageType in CoverageTypesAvailable (true)) {

                if (currentCoverageType.Name == coverageTypeName) {

                    coverageType = currentCoverageType;

                    break;

                }

            }

            return coverageType;

        }

        #endregion 


        #region Insurance Type

        public List<Core.Insurer.InsuranceType> InsuranceTypesAvailable (Boolean useCaching) {

            List<Core.Insurer.InsuranceType> insuranceTypes = new List<Core.Insurer.InsuranceType> ();

            Mercury.Server.Application.InsuranceTypeCollectionResponse response;

            String cacheKey = "Application." + session.EnvironmentId + "." + typeof (Core.Insurer.InsuranceType).ToString () + ".Available";


            ClearLastException ();

            try {

                insuranceTypes = (List<Core.Insurer.InsuranceType>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { insuranceTypes = null; }

                if (insuranceTypes == null) {

                    insuranceTypes = new List<Core.Insurer.InsuranceType> ();

                    response = ApplicationClient.InsuranceTypesAvailable (session.Token);

                    if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                    foreach (Server.Application.InsuranceType currentServerInsuranceType in response.Collection) {

                        Core.Insurer.InsuranceType insuranceType = new Core.Insurer.InsuranceType (this, currentServerInsuranceType);

                        insuranceTypes.Add (insuranceType);

                    }

                    if (insuranceTypes.Count > 0) { cacheManager.CacheObject (cacheKey, insuranceTypes, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }




            return insuranceTypes;

        }

        public Core.Insurer.InsuranceType InsuranceTypeGet (Int64 insuranceTypeId, Boolean useCaching) {

            if (insuranceTypeId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + "." + typeof (Core.Insurer.InsuranceType).ToString () + "." + insuranceTypeId.ToString ();

            Core.Insurer.InsuranceType insuranceType = null;

            ClearLastException ();


            try {

                insuranceType = (Core.Insurer.InsuranceType)cacheManager.GetObject (cacheKey);

                if (!useCaching) { insuranceType = null; }

                if (insuranceType == null) {

                    Server.Application.InsuranceType serverInsuranceType = ApplicationClient.InsuranceTypeGet (session.Token, insuranceTypeId);

                    if (serverInsuranceType != null) { insuranceType = new Core.Insurer.InsuranceType (this, serverInsuranceType); }

                    if (insuranceType != null) { cacheManager.CacheObject (cacheKey, insuranceType, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return insuranceType;

        }

        public Core.Insurer.InsuranceType InsuranceTypeGet (String insuranceTypeName, Boolean useCaching) {

            if (String.IsNullOrWhiteSpace (insuranceTypeName)) { return null; }

            Core.Insurer.InsuranceType insuranceType = null;


            foreach (Core.Insurer.InsuranceType currentInsuranceType in InsuranceTypesAvailable (true)) {

                if (currentInsuranceType.Name == insuranceTypeName) {

                    insuranceType = currentInsuranceType;

                    break;

                }
                
            }

            return insuranceType;

        }

        #endregion 


        #region Programs

        public List<Core.Insurer.Program> ProgramsAvailable (Boolean useCaching) {

            List<Core.Insurer.Program> available = new List<Core.Insurer.Program> ();
            
            Mercury.Server.Application.ProgramCollectionResponse response;

            String cacheKey = "Application." + session.EnvironmentId + ".Program.Available";


            ClearLastException ();

            try {

                available = (List<Core.Insurer.Program>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { available = null; }

                if (available == null) {

                    available = new List<Core.Insurer.Program> ();

                    response = ApplicationClient.ProgramsAvailable (session.Token);


                    if (!response.HasException) {

                        foreach (Server.Application.Program currentServerObject in response.Collection) {

                            Core.Insurer.Program clientObject = new Core.Insurer.Program (this, currentServerObject);

                            available.Add (clientObject);

                        }

                        if (available.Count > 0) { cacheManager.CacheObject (cacheKey, available, CacheExpirationData); }

                    }

                    else { SetLastException (response.Exception); }
                    
                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return available;

        }

        public List<Core.Insurer.Program> ProgramsAvailableByInsurer (Int64 insurerId, Boolean useCaching) {

            List<Core.Insurer.Program> available = ProgramsAvailable (useCaching); 
            
            List<Core.Insurer.Program> filtered = new List<Core.Insurer.Program> ();


            foreach (Core.Insurer.Program currentObject in available) {

                if (currentObject.InsurerId == insurerId) {

                    filtered.Add (currentObject);

                }

            }


            return filtered;

        }

        public Dictionary<Int64, String> ProgramDictionaryByInsurer (Int64 insurerId, Boolean useCaching) {

            List<Core.Insurer.Program> available = ProgramsAvailable (useCaching); 

            Dictionary<Int64, String> filtered = new Dictionary<Int64, String> ();


            foreach (Core.Insurer.Program currentObject in available) {

                if (currentObject.InsurerId == insurerId) {

                    filtered.Add (currentObject.Id, currentObject.Name);

                }

            }


            return filtered;

        }

        public Core.Insurer.Program ProgramGet (Int64 programId, Boolean useCaching) {

            if (programId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + "." + typeof (Core.Insurer.Program).ToString () + "." + programId.ToString ();

            Core.Insurer.Program program = null;

            ClearLastException ();


            try {

                program = (Core.Insurer.Program)cacheManager.GetObject (cacheKey);

                if (!useCaching) { program = null; }

                if (program == null) {

                    Server.Application.Program serverProgram = ApplicationClient.ProgramGet (session.Token, programId);

                    if (serverProgram != null) { program = new Core.Insurer.Program (this, serverProgram); }

                    if (program != null) { cacheManager.CacheObject (cacheKey, program, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return program;

        }

        #endregion 

        #endregion


        #region Core - Medical Services

        public List<Mercury.Server.Application.SearchResultMedicalServiceHeader> MedicalServiceHeadersGet (Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".Core.MedicalServiceHeadersGet.";


            List<Mercury.Server.Application.SearchResultMedicalServiceHeader> result = new List<Server.Application.SearchResultMedicalServiceHeader> ();

            Mercury.Server.Application.SearchResultMedicalServiceHeaderCollectionResponse response = new Server.Application.SearchResultMedicalServiceHeaderCollectionResponse ();

            ClearLastException ();

            try {

                result = (List<Mercury.Server.Application.SearchResultMedicalServiceHeader>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { result = null; }


                if (result == null) {

                    result = new List<Server.Application.SearchResultMedicalServiceHeader> ();

                    response = ApplicationClient.MedicalServiceHeadersGet (session.Token);


                    if (!response.HasException) { 
                        
                        result.AddRange (response.Collection);

                        if (result.Count > 0) { cacheManager.CacheObject (cacheKey, result, CacheExpirationData); }
                    
                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return result;

        }

        public List<Mercury.Server.Application.SearchResultMedicalServiceHeader> MedicalServiceHeadersGetByType (Mercury.Server.Application.MedicalServiceType serviceType) {

            List<Mercury.Server.Application.SearchResultMedicalServiceHeader> result = new List<Server.Application.SearchResultMedicalServiceHeader> ();

            Mercury.Server.Application.SearchResultMedicalServiceHeaderCollectionResponse response = new Server.Application.SearchResultMedicalServiceHeaderCollectionResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.MedicalServiceHeadersGetByType (session.Token, serviceType);

                if (!response.HasException) { result.AddRange (response.Collection); }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return result;

        }

        public Core.MedicalServices.Service MedicalServiceGet (Int64 serviceId, Boolean useCaching) {

            if (serviceId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".Core.MedicalService." + serviceId.ToString ();

            Core.MedicalServices.Service medicalService = null;

            Mercury.Server.Application.Service serverService = null;


            ClearLastException ();

            try {

                if (useCaching) { medicalService = (Core.MedicalServices.Service)cacheManager.GetObject (cacheKey); }

                if (medicalService == null) {

                    serverService = ApplicationClient.MedicalServiceGet (session.Token, serviceId);

                    if (serverService != null) {

                        switch (serverService.ServiceType) {

                            case Mercury.Server.Application.MedicalServiceType.Singleton:

                                Mercury.Server.Application.ServiceSingleton serverSingleton = (Mercury.Server.Application.ServiceSingleton)serverService;

                                medicalService = new Mercury.Client.Core.MedicalServices.ServiceSingleton (this, serverSingleton);

                                break;

                            case Mercury.Server.Application.MedicalServiceType.Set:

                                Mercury.Server.Application.ServiceSet serverSet = (Mercury.Server.Application.ServiceSet)serverService;

                                medicalService = new Mercury.Client.Core.MedicalServices.ServiceSet (this, serverSet);

                                break;

                            default:

                                medicalService = new Mercury.Client.Core.MedicalServices.Service (this, serverService);

                                break;

                        }

                    }

                    if (medicalService != null) { cacheManager.CacheObject (cacheKey, medicalService, CacheExpirationData); }

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return medicalService;

        }

        public Int64 MedicalServiceGetIdByName (String serviceName) {

            Int64 medicalServiceId = 0;

            ClearLastException ();

            try {

                medicalServiceId = ApplicationClient.MedicalServiceGetIdByName (session.Token, serviceName);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return medicalServiceId;

        }

        public Boolean MedicalServiceDelete (Int64 serviceId) {

            Mercury.Server.Application.BooleanResponse response = new Mercury.Server.Application.BooleanResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.MedicalServiceDelete (session.Token, serviceId);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return response.Result;

        }

        public Core.MedicalServices.ServiceSingleton MedicalServiceSingletonGet (Int64 serviceId) {

            Core.MedicalServices.ServiceSingleton medicalService = null;

            Mercury.Server.Application.ServiceSingleton serverSingleton = null;

            ClearLastException ();

            try {

                serverSingleton = ApplicationClient.MedicalServiceSingletonGet (session.Token, serviceId);

                if (serverSingleton != null) {

                    medicalService = new Mercury.Client.Core.MedicalServices.ServiceSingleton (this, serverSingleton);

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return medicalService;

        }

        public Server.Application.ObjectSaveResponse MedicalServiceSave (Core.MedicalServices.ServiceSingleton serviceSingleton) {

            Mercury.Server.Application.ObjectSaveResponse response = new Mercury.Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.MedicalServiceSingletonSave (session.Token, (Mercury.Server.Application.ServiceSingleton)serviceSingleton.ToServerObject ());

                if (!response.HasException) { serviceSingleton.SetId (response.Id); }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                response.HasException = true;

                response.Exception = new Mercury.Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;

                SetLastException (applicationException);

            }

            return response;

        }

        public Server.Application.ObjectSaveResponse MedicalServiceSave (Core.MedicalServices.ServiceSet serviceSet) {

            Mercury.Server.Application.ObjectSaveResponse response = new Mercury.Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.MedicalServiceSetSave (session.Token, (Mercury.Server.Application.ServiceSet)serviceSet.ToServerObject ());

                if (!response.HasException) { serviceSet.SetId (response.Id); }

                else { SetLastException (response.Exception); } 
            }

            catch (Exception applicationException) {

                response.HasException = true;

                response.Exception = new Mercury.Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;

                SetLastException (applicationException);

            }

            return response;

        }


        public Dictionary<String, String> MedicalServiceSingletonDefinitionValidate (Mercury.Server.Application.ServiceSingletonDefinition singletonDefinition) {

            Dictionary<String, String> response = new Dictionary<String, String> ();

            ClearLastException ();

            try {

                response = ApplicationClient.MedicalServiceSingletonDefinitionValidate (session.Token, singletonDefinition);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return response;

        }

        public Dictionary<String, String> MedicalServiceSingletonDefinitionValidate (Core.MedicalServices.Definitions.ServiceSingletonDefinition singletonDefinition) {

            return MedicalServiceSingletonDefinitionValidate ((Server.Application.ServiceSingletonDefinition) singletonDefinition.ToServerObject ());

        }

        public Dictionary<String, String> MedicalServiceSetDefinitionValidate (Mercury.Server.Application.ServiceSetDefinition setDefinition) {

            Dictionary<String, String> response = new Dictionary<String, String> ();

            ClearLastException ();

            try {

                response = ApplicationClient.MedicalServiceSetDefinitionValidate (session.Token, setDefinition);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return response;

        }

        public Dictionary<String, String> MedicalServiceSetDefinitionValidate (Core.MedicalServices.Definitions.ServiceSetDefinition setDefinition) {

            return MedicalServiceSetDefinitionValidate ((Server.Application.ServiceSetDefinition)setDefinition.ToServerObject ());

        }


        public List<Mercury.Server.Application.MemberServiceDetailSingleton> MedicalServiceSingletonPreview (Mercury.Server.Application.ServiceSingleton serviceSingleton) {

            List<Mercury.Server.Application.MemberServiceDetailSingleton> result = new List<Mercury.Server.Application.MemberServiceDetailSingleton> ();

            Mercury.Server.Application.MedicalServiceDetailSingletonCollectionResponse response = new Mercury.Server.Application.MedicalServiceDetailSingletonCollectionResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.MedicalServiceSingletonPreview (session.Token, serviceSingleton);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                for (Int32 currentIndex = 0; currentIndex < response.Collection.Length; currentIndex++) {

                    result.Add (response.Collection[currentIndex]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return result;

        }

        public List<Mercury.Server.Application.MemberServiceDetailSet> MedicalServiceSetPreview (Mercury.Server.Application.ServiceSet serviceSet) {

            List<Mercury.Server.Application.MemberServiceDetailSet> result = new List<Mercury.Server.Application.MemberServiceDetailSet> ();

            Mercury.Server.Application.MedicalServiceDetailSetCollectionResponse response = new Mercury.Server.Application.MedicalServiceDetailSetCollectionResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.MedicalServiceSetPreview (session.Token, serviceSet);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                for (Int32 currentIndex = 0; currentIndex < response.Collection.Length; currentIndex++) {

                    result.Add (response.Collection[currentIndex]);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return result;

        }


        public Core.MedicalServices.ServiceSet MedicalServiceSetGet (Int64 serviceId) {

            Core.MedicalServices.ServiceSet medicalService = null;

            Mercury.Server.Application.ServiceSet serverSet = null;

            ClearLastException ();

            try {

                serverSet = ApplicationClient.MedicalServiceSetGet (session.Token, serviceId);

                if (serverSet != null) {

                    medicalService = new Mercury.Client.Core.MedicalServices.ServiceSet (this, serverSet);

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return medicalService;

        }


        public Boolean MemberServiceAddManual (Int64 memberId, Int64 serviceId, DateTime eventDate) {

            Mercury.Server.Application.ObjectSaveResponse response = new Mercury.Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.MemberServiceAddManual (session.Token, memberId, serviceId, eventDate);

                if (response.HasException) {

                    SetLastException (new ApplicationException (response.Exception.Message));

                }

            }

            catch (Exception applicationException) {

                response.Success = false;

                response.HasException = true;

                response.Exception = new Mercury.Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;

                SetLastException (applicationException);

            }

            return response.Success;

        }

        public Boolean MemberServiceRemoveManual (Int64 memberServiceId) {

            Mercury.Server.Application.BooleanResponse response = new Mercury.Server.Application.BooleanResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.MemberServiceRemoveManual (session.Token, memberServiceId);

                if (response.HasException) {

                    SetLastException (new ApplicationException (response.Exception.Message));

                }

            }

            catch (Exception applicationException) {

                response.Result = false;

                response.HasException = true;

                response.Exception = new Mercury.Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;

                SetLastException (applicationException);

            }

            return response.Result;

        }


        public Core.MedicalServices.MemberService MemberServiceGet (Int64 memberServiceId) {

            if (memberServiceId == 0) { return null; }

            
            String cacheKey = "Application." + session.EnvironmentId + ".Core.MemberService." + memberServiceId.ToString ();

            Core.MedicalServices.MemberService memberService = null;


            ClearLastException ();
            
            try {

                memberService = (Core.MedicalServices.MemberService)cacheManager.GetObject (cacheKey);

                if (memberService == null) {

                    Mercury.Server.Application.MemberService serverMemberService;

                    serverMemberService = ApplicationClient.MemberServiceGet (session.Token, memberServiceId);

                    if (serverMemberService != null) {

                        memberService = new Mercury.Client.Core.MedicalServices.MemberService (this, serverMemberService);

                        cacheManager.CacheObject (cacheKey, memberService, CacheExpirationData);

                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberService;

        }


        public List<Mercury.Server.Application.MemberService> MemberServicesGetByPage (Int64 memberId, Int32 initialRow, Int32 count, Boolean showHidden) {

            Mercury.Server.Application.MemberServiceCollectionResponse response;

            List<Mercury.Server.Application.MemberService> memberServices = null;


            ClearLastException ();


            try {

                response = ApplicationClient.MemberServicesGetByPage (session.Token, memberId, initialRow, count, showHidden);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                memberServices = new List<Mercury.Server.Application.MemberService> ();

                memberServices.AddRange (response.Collection);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberServices;

        }

        public Int64 MemberServicesGetCount (Int64 memberId, Boolean showHidden) {

            Int64 servicesCount = 0;


            ClearLastException ();

            try {

                servicesCount = ApplicationClient.MemberServicesGetCount (session.Token, memberId, showHidden);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return servicesCount;

        }

        public List<Mercury.Server.Application.MemberServiceDetailSingleton> MemberServiceDetailSingletonGet (Int64 memberServiceId) {

            Mercury.Server.Application.MemberServiceDetailSingletonCollectionResponse response;

            List<Mercury.Server.Application.MemberServiceDetailSingleton> detail = null;


            ClearLastException ();


            try {

                response = ApplicationClient.MemberServiceDetailSingletonGet (session.Token, memberServiceId);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                detail = new List<Mercury.Server.Application.MemberServiceDetailSingleton> ();

                detail.AddRange (response.Collection);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return detail;

        }

        public List<Mercury.Server.Application.MemberServiceDetailSet> MemberServiceDetailSetGet (Int64 memberServiceId) {

            Mercury.Server.Application.MemberServiceDetailSetCollectionResponse response;

            List<Mercury.Server.Application.MemberServiceDetailSet> detail = null;


            ClearLastException ();


            try {

                response = ApplicationClient.MemberServiceDetailSetGet (session.Token, memberServiceId);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                detail = new List<Mercury.Server.Application.MemberServiceDetailSet> ();

                detail.AddRange (response.Collection);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return detail;

        }

        #endregion 


        #region Core - Member

        #region Member

        public Core.Member.Member MemberGet (Int64 memberId, Boolean useCaching) {

            if (memberId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".Member." + memberId.ToString ();

            Core.Member.Member member = null;

            ClearLastException ();


            try {

                member = (Core.Member.Member)cacheManager.GetObject (cacheKey);

                if (!useCaching) { member = null; }

                if (member == null) {

                    Server.Application.Member serverMember = ApplicationClient.MemberGet (session.Token, memberId);

                    if (serverMember != null) { member = new Core.Member.Member (this, serverMember); }

                    if (member != null) { cacheManager.CacheObject (cacheKey, member, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return member;

        }

        public Core.Member.Member MemberGetByEntityId (Int64 entityId, Boolean useCaching) {

            if (entityId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".Member.ByEntityId." + entityId.ToString ();

            Core.Member.Member member = null;

            ClearLastException ();


            try {

                member = (Core.Member.Member)cacheManager.GetObject (cacheKey);

                if (!useCaching) { member = null; }

                if (member == null) {

                    Server.Application.Member serverMember = ApplicationClient.MemberGetByEntityId (session.Token, entityId);

                    if (serverMember != null) { member = new Core.Member.Member (this, serverMember); }

                    if (member != null) { cacheManager.CacheObject (cacheKey, member, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return member;

        }


        private Client.Core.Member.Member MemberDemographicsHandleResponse (Server.Application.MemberDemographicsResponse response) {

            if (response.HasException) { return null; }

            if (response.Member == null) { return null; }


            Mercury.Client.Core.Member.Member member = new Mercury.Client.Core.Member.Member (this, response.Member);


            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".Member." + member.Id.ToString (), member, CacheExpirationData);

            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".Member.ByEntityId." + member.EntityId.ToString (), member, CacheExpirationData);

            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".MemberDemographics." + member.Id.ToString (), member, CacheExpirationData);

            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".MemberDemographics.ByEntityId." + member.EntityId.ToString (), member, CacheExpirationData);


            #region Cache Entity Objects

            if (response.Entity != null) {

                Core.Entity.Entity entity = new Mercury.Client.Core.Entity.Entity (this, response.Entity);

                cacheManager.CacheObject ("Application." + session.EnvironmentId + ".Entity." + entity.Id.ToString (), entity, CacheExpirationData);


                if (response.EntityAddresses != null) {

                    List<Core.Entity.EntityAddress> entityAddresses = new List<Mercury.Client.Core.Entity.EntityAddress> ();

                    foreach (Mercury.Server.Application.EntityAddress currentServerAddress in response.EntityAddresses) {

                        Core.Entity.EntityAddress entityAddress = new Mercury.Client.Core.Entity.EntityAddress (this, currentServerAddress);

                        entityAddresses.Add (entityAddress);

                        cacheManager.CacheObject ("Application." + session.EnvironmentId + ".EntityAddress." + entityAddress.Id.ToString (), entityAddress, CacheExpirationData);

                    }

                    cacheManager.CacheObject ("Application." + session.EnvironmentId + ".EntityAddress.ByEntityId." + entity.Id.ToString (), entityAddresses, CacheExpirationData);
                                              
                }

                if (response.EntityContactInformations != null) {

                    List<Core.Entity.EntityContactInformation> entityContactInformations = new List<Mercury.Client.Core.Entity.EntityContactInformation> ();

                    foreach (Mercury.Server.Application.EntityContactInformation currentServerContactInformation in response.EntityContactInformations) {

                        Core.Entity.EntityContactInformation entityContactInformation = new Mercury.Client.Core.Entity.EntityContactInformation (this, currentServerContactInformation);

                        entityContactInformations.Add (entityContactInformation);

                        cacheManager.CacheObject ("Application." + session.EnvironmentId + ".EntityContactInformation." + entityContactInformation.Id.ToString (), entityContactInformation, CacheExpirationData);

                    }

                    cacheManager.CacheObject ("Application." + session.EnvironmentId + ".EntityContactInformation.ByEntityId." + entity.Id.ToString (), entityContactInformations, CacheExpirationData);

                }

            }

            #endregion


            #region Cache Member Objects

            if (member != null) {

                // ENROLLMENT

                if (response.MemberEnrollments != null) {

                    List<Core.Member.MemberEnrollment> enrollments = new List<Mercury.Client.Core.Member.MemberEnrollment> ();

                    foreach (Server.Application.MemberEnrollment currentServerMemberEnrollment in response.MemberEnrollments) {

                        Core.Member.MemberEnrollment enrollment = new Mercury.Client.Core.Member.MemberEnrollment (this, currentServerMemberEnrollment);

                        enrollments.Add (enrollment);

                        cacheManager.CacheObject ("Application." + session.EnvironmentId + ".MemberEnrollment." + enrollment.Id.ToString (), enrollment, CacheExpirationData);

                    }

                    cacheManager.CacheObject ("Application." + session.EnvironmentId + ".MemberEnrollment.ByMemberId." + member.Id.ToString (), enrollments, CacheExpirationData);
                            
                }

                // ENROLLMENT COVERAGES

                if (response.MemberCurrentEnrollmentCoverages != null) {

                    if (response.MemberCurrentEnrollmentCoverages.Length > 0) {

                        List<Core.Member.MemberEnrollmentCoverage> enrollmentCoverages = new List<Mercury.Client.Core.Member.MemberEnrollmentCoverage> ();

                        foreach (Mercury.Server.Application.MemberEnrollmentCoverage currentServerEnrollmentCoverage in response.MemberCurrentEnrollmentCoverages) {

                            Core.Member.MemberEnrollmentCoverage enrollmentCoverage = new Mercury.Client.Core.Member.MemberEnrollmentCoverage (this, currentServerEnrollmentCoverage);

                            enrollmentCoverages.Add (enrollmentCoverage);

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".MemberEnrollmentCoverage." + enrollmentCoverage.Id.ToString (), enrollmentCoverage, CacheExpirationData);

                        }

                        cacheManager.CacheObject ("Application." + session.EnvironmentId + ".MemberEnrollmentCoverage.ByEnrollmentId." + response.MemberCurrentEnrollmentCoverages[0].MemberEnrollmentId.ToString (), enrollmentCoverages, CacheExpirationData);
                         
                    }

                }

                // ENROLLMENT PCP ASSIGNMENTS

                if (response.MemberCurrentEnrollmentPcps != null) {

                    if (response.MemberCurrentEnrollmentPcps.Length > 0) {

                        List<Core.Member.MemberEnrollmentPcp> pcpAssignments = new List<Mercury.Client.Core.Member.MemberEnrollmentPcp> ();

                        foreach (Mercury.Server.Application.MemberEnrollmentPcp currentServerPcpAssignment in response.MemberCurrentEnrollmentPcps) {

                            Core.Member.MemberEnrollmentPcp pcpAssignment = new Mercury.Client.Core.Member.MemberEnrollmentPcp (this, currentServerPcpAssignment);

                            pcpAssignments.Add (pcpAssignment);

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".MemberEnrollmentPcp." + pcpAssignment.Id.ToString (), pcpAssignment, CacheExpirationData);

                        }

                        cacheManager.CacheObject ("Application." + session.EnvironmentId + ".MemberEnrollmentPcp.ByEnrollmentId." + response.MemberCurrentEnrollmentPcps[0].MemberEnrollmentId.ToString (), pcpAssignments, CacheExpirationData);

                    }

                }

                // MEMBER RELATIONSHIPS

                if (response.MemberRelationships != null) {

                    List<Core.Member.MemberRelationship> memberRelationships = new List<Mercury.Client.Core.Member.MemberRelationship> ();

                    foreach (Mercury.Server.Application.MemberRelationship currentServerContactInformation in response.MemberRelationships) {

                        Core.Member.MemberRelationship memberRelationship = new Mercury.Client.Core.Member.MemberRelationship (this, currentServerContactInformation);

                        memberRelationships.Add (memberRelationship);

                        cacheManager.CacheObject ("Application." + session.EnvironmentId + ".MemberRelationship." + memberRelationship.Id.ToString (), memberRelationship, CacheExpirationData);

                    }

                    cacheManager.CacheObject ("Application." + session.EnvironmentId + ".MemberRelationship.ByMemberId." + member.Id.ToString (), memberRelationships, CacheExpirationData);

                }

            }

            #endregion


            #region Cache Provider Objects

            foreach (Server.Application.Provider currentServerProvider in response.Providers) {

                Client.Core.Provider.Provider clientProvider = new Client.Core.Provider.Provider (this, currentServerProvider);

                cacheManager.CacheObject ("Application." + session.EnvironmentId + ".Provider." + clientProvider.Id, clientProvider, CacheExpirationData);

                if (currentServerProvider.Entity != null) {

                    Client.Core.Entity.Entity clientProviderEntity = new Core.Entity.Entity (this, currentServerProvider.Entity);

                    cacheManager.CacheObject ("Application." + session.EnvironmentId + ".Entity." + clientProviderEntity.Id, clientProviderEntity, CacheExpirationData);

                }
            
            }

            
            foreach (Server.Application.ProviderAffiliation currentServerProviderAffiliation in response.ProviderAffiliations) {

                Client.Core.Provider.ProviderAffiliation clientProviderAffiliation = new Core.Provider.ProviderAffiliation (this, currentServerProviderAffiliation);

                cacheManager.CacheObject ("Application." + session.EnvironmentId + ".ProviderAffiliation." + clientProviderAffiliation.Id, clientProviderAffiliation, CacheExpirationData);

            }

            #endregion 


            return member;

        }

        public Mercury.Client.Core.Member.Member MemberGetDemographics (Int64 memberId, Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".MemberDemographics." + memberId.ToString ();

            Mercury.Client.Core.Member.Member member = null;

            ClearLastException ();

            try {

                member = (Core.Member.Member)cacheManager.GetObject (cacheKey);

                if (member == null) {

                    Server.Application.MemberDemographicsResponse response = ApplicationClient.MemberGetDemographics (session.Token, memberId);

                    if (!response.HasException) {

                        member = MemberDemographicsHandleResponse (response);

                    }

                    else { SetLastException (response.Exception); }

                }

                else { member.Application = this; }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return member;

        }

        public Mercury.Client.Core.Member.Member MemberGetDemographicsByEntityId (Int64 entityId, Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".MemberDemographics.ByEntityId." + entityId.ToString ();

            Mercury.Client.Core.Member.Member member = null;

            ClearLastException ();

            try {

                member = (Core.Member.Member)cacheManager.GetObject (cacheKey);

                if (member == null) {

                    Server.Application.MemberDemographicsResponse response = ApplicationClient.MemberGetDemographicsByEntityId (session.Token, entityId);

                    if (!response.HasException) {

                        member = MemberDemographicsHandleResponse (response);

                    }

                    else { SetLastException (response.Exception); }

                }

                else { member.Application = this; }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return member;

        }

        #endregion 

        
        #region Member Enrollment

        public Core.Member.MemberEnrollment MemberEnrollmentGet (Int64 memberEnrollmentId, Boolean useCaching) {

            if (memberEnrollmentId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".MemberEnrollment." + memberEnrollmentId.ToString ();

            Core.Member.MemberEnrollment memberEnrollment = null;

            ClearLastException ();


            try {

                memberEnrollment = (Core.Member.MemberEnrollment)cacheManager.GetObject (cacheKey);

                if (!useCaching) { memberEnrollment = null; }

                if (memberEnrollment == null) {

                    Server.Application.MemberEnrollment serverMemberEnrollment = ApplicationClient.MemberEnrollmentGet (session.Token, memberEnrollmentId);

                    if (serverMemberEnrollment != null) { memberEnrollment = new Core.Member.MemberEnrollment (this, serverMemberEnrollment); }

                    if (memberEnrollment != null) { cacheManager.CacheObject (cacheKey, memberEnrollment, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberEnrollment;

        }

        public List<Core.Member.MemberEnrollment> MemberEnrollmentsGet (Int64 memberId, Boolean useCaching) {

            List<Core.Member.MemberEnrollment> memberEnrollments = new List<Core.Member.MemberEnrollment> ();

            Mercury.Server.Application.MemberEnrollmentCollectionResponse response;

            String cacheKey = "Application." + session.EnvironmentId + ".MemberEnrollment.ByMemberId." + memberId.ToString ();


            ClearLastException ();

            try {

                 memberEnrollments = (List<Core.Member.MemberEnrollment>)cacheManager.GetObject (cacheKey);

                 if (!useCaching) { memberEnrollments = null; }

                 if (memberEnrollments == null) {

                     memberEnrollments = new List<Core.Member.MemberEnrollment> ();

                    response = ApplicationClient.MemberEnrollmentsGet (session.Token, memberId);

                    if (!response.HasException) {

                        foreach (Server.Application.MemberEnrollment currentServerMemberEnrollment in response.Collection) {

                            Core.Member.MemberEnrollment memberEnrollment = new Core.Member.MemberEnrollment (this, currentServerMemberEnrollment);

                            memberEnrollments.Add (memberEnrollment);

                        }

                        if (memberEnrollments.Count > 0) { cacheManager.CacheObject (cacheKey, memberEnrollments, CacheExpirationData); }

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberEnrollments;

        }


        public Core.Member.MemberEnrollmentCoverage MemberEnrollmentCoverageGet (Int64 memberEnrollmentCoverageId, Boolean useCaching) {

            if (memberEnrollmentCoverageId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".MemberEnrollmentCoverage." + memberEnrollmentCoverageId.ToString ();

            Core.Member.MemberEnrollmentCoverage memberEnrollmentCoverage = null;

            ClearLastException ();


            try {

                memberEnrollmentCoverage = (Core.Member.MemberEnrollmentCoverage)cacheManager.GetObject (cacheKey);

                if (!useCaching) { memberEnrollmentCoverage = null; }

                if (memberEnrollmentCoverage == null) {

                    Server.Application.MemberEnrollmentCoverage serverMemberEnrollmentCoverage = ApplicationClient.MemberEnrollmentCoverageGet (session.Token, memberEnrollmentCoverageId);

                    if (serverMemberEnrollmentCoverage != null) { memberEnrollmentCoverage = new Core.Member.MemberEnrollmentCoverage (this, serverMemberEnrollmentCoverage); }

                    if (memberEnrollmentCoverage != null) { cacheManager.CacheObject (cacheKey, memberEnrollmentCoverage, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberEnrollmentCoverage;

        }

        public List<Core.Member.MemberEnrollmentCoverage> MemberEnrollmentCoveragesGet (Int64 memberEnrollmentId, Boolean useCaching) {

            List<Core.Member.MemberEnrollmentCoverage> memberEnrollmentCoverages = new List<Core.Member.MemberEnrollmentCoverage> ();

            Mercury.Server.Application.MemberEnrollmentCoverageCollectionResponse response;

            String cacheKey = "Application." + session.EnvironmentId + ".MemberEnrollmentCoverage.ByEnrollmentId." + memberEnrollmentId.ToString ();


            ClearLastException ();

            try {

                memberEnrollmentCoverages = (List<Core.Member.MemberEnrollmentCoverage>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { memberEnrollmentCoverages = null; }

                if (memberEnrollmentCoverages == null) {

                    memberEnrollmentCoverages = new List<Core.Member.MemberEnrollmentCoverage> ();

                    response = ApplicationClient.MemberEnrollmentCoveragesGet (session.Token, memberEnrollmentId);

                    if (!response.HasException) {

                        foreach (Server.Application.MemberEnrollmentCoverage currentServerMemberEnrollmentCoverage in response.Collection) {

                            Core.Member.MemberEnrollmentCoverage memberEnrollmentCoverage = new Core.Member.MemberEnrollmentCoverage (this, currentServerMemberEnrollmentCoverage);

                            memberEnrollmentCoverages.Add (memberEnrollmentCoverage);

                        }

                        if (memberEnrollmentCoverages.Count > 0) { cacheManager.CacheObject (cacheKey, memberEnrollmentCoverages, CacheExpirationData); }

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberEnrollmentCoverages;

        }


        public Core.Member.MemberEnrollmentPcp MemberEnrollmentPcpGet (Int64 memberEnrollmentPcpId, Boolean useCaching) {

            if (memberEnrollmentPcpId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".MemberEnrollmentPcp." + memberEnrollmentPcpId.ToString ();

            Core.Member.MemberEnrollmentPcp memberEnrollmentPcp = null;

            ClearLastException ();


            try {

                memberEnrollmentPcp = (Core.Member.MemberEnrollmentPcp)cacheManager.GetObject (cacheKey);

                if (!useCaching) { memberEnrollmentPcp = null; }

                if (memberEnrollmentPcp == null) {

                    Server.Application.MemberEnrollmentPcp serverMemberEnrollmentPcp = ApplicationClient.MemberEnrollmentPcpGet (session.Token, memberEnrollmentPcpId);

                    if (serverMemberEnrollmentPcp != null) { memberEnrollmentPcp = new Core.Member.MemberEnrollmentPcp (this, serverMemberEnrollmentPcp); }

                    if (memberEnrollmentPcp != null) { cacheManager.CacheObject (cacheKey, memberEnrollmentPcp, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberEnrollmentPcp;

        }

        public List<Core.Member.MemberEnrollmentPcp> MemberEnrollmentPcpsGet (Int64 memberEnrollmentId, Boolean useCaching) {

            List<Core.Member.MemberEnrollmentPcp> memberEnrollmentPcps = new List<Core.Member.MemberEnrollmentPcp> ();

            Mercury.Server.Application.MemberEnrollmentPcpCollectionResponse response;

            String cacheKey = "Application." + session.EnvironmentId + ".MemberEnrollmentPcp.ByEnrollmentId." + memberEnrollmentId.ToString ();


            ClearLastException ();

            try {

                memberEnrollmentPcps = (List<Core.Member.MemberEnrollmentPcp>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { memberEnrollmentPcps = null; }

                if (memberEnrollmentPcps == null) {

                    memberEnrollmentPcps = new List<Core.Member.MemberEnrollmentPcp> ();

                    response = ApplicationClient.MemberEnrollmentPcpsGet (session.Token, memberEnrollmentId);

                    if (!response.HasException) {

                        foreach (Server.Application.MemberEnrollmentPcp currentServerMemberEnrollmentPcp in response.Collection) {

                            Core.Member.MemberEnrollmentPcp memberEnrollmentPcp = new Core.Member.MemberEnrollmentPcp (this, currentServerMemberEnrollmentPcp);

                            memberEnrollmentPcps.Add (memberEnrollmentPcp);

                        }

                        if (memberEnrollmentPcps.Count > 0) { cacheManager.CacheObject (cacheKey, memberEnrollmentPcps, CacheExpirationData); }

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberEnrollmentPcps;

        }


        public Core.Member.MemberEnrollmentTplCob MemberEnrollmentTplCobGet (Int64 memberEnrollmentTplCobId, Boolean useCaching) {

            if (memberEnrollmentTplCobId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".MemberEnrollmentTplCob." + memberEnrollmentTplCobId.ToString ();

            Core.Member.MemberEnrollmentTplCob memberEnrollmentTplCob = null;

            ClearLastException ();


            try {

                memberEnrollmentTplCob = (Core.Member.MemberEnrollmentTplCob)cacheManager.GetObject (cacheKey);

                if (!useCaching) { memberEnrollmentTplCob = null; }

                if (memberEnrollmentTplCob == null) {

                    Server.Application.MemberEnrollmentTplCob serverMemberEnrollmentTplCob = ApplicationClient.MemberEnrollmentTplCobGet (session.Token, memberEnrollmentTplCobId);

                    if (serverMemberEnrollmentTplCob != null) { memberEnrollmentTplCob = new Core.Member.MemberEnrollmentTplCob (this, serverMemberEnrollmentTplCob); }

                    if (memberEnrollmentTplCob != null) { cacheManager.CacheObject (cacheKey, memberEnrollmentTplCob, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberEnrollmentTplCob;

        }

        public List<Core.Member.MemberEnrollmentTplCob> MemberEnrollmentTplCobsGet (Int64 memberId, Boolean useCaching) {

            List<Core.Member.MemberEnrollmentTplCob> memberEnrollmentTplCobs = new List<Core.Member.MemberEnrollmentTplCob> ();

            Mercury.Server.Application.MemberEnrollmentTplCobCollectionResponse response;

            String cacheKey = "Application." + session.EnvironmentId + ".MemberEnrollmentTplCob.ByMemberId." + memberId.ToString ();


            ClearLastException ();

            try {

                memberEnrollmentTplCobs = (List<Core.Member.MemberEnrollmentTplCob>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { memberEnrollmentTplCobs = null; }

                if (memberEnrollmentTplCobs == null) {

                    memberEnrollmentTplCobs = new List<Core.Member.MemberEnrollmentTplCob> ();

                    response = ApplicationClient.MemberEnrollmentTplCobsGet (session.Token, memberId);

                    if (!response.HasException) {

                        foreach (Server.Application.MemberEnrollmentTplCob currentServerMemberEnrollmentTplCob in response.Collection) {

                            Core.Member.MemberEnrollmentTplCob memberEnrollmentTplCob = new Core.Member.MemberEnrollmentTplCob (this, currentServerMemberEnrollmentTplCob);

                            memberEnrollmentTplCobs.Add (memberEnrollmentTplCob);

                        }

                        cacheManager.CacheObject (cacheKey, memberEnrollmentTplCobs, CacheExpirationData);

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberEnrollmentTplCobs;

        }

        #endregion 


        #region Member Relationship

        public Mercury.Client.Core.Member.MemberRelationship MemberRelationshipGet (Int64 memberRelationshipId) {

            String cacheKey = "Application." + session.EnvironmentId + ".MemberRelationship." + memberRelationshipId.ToString ();

            Mercury.Client.Core.Member.MemberRelationship memberRelationship = null;

            ClearLastException ();

            try {

                memberRelationship = (Core.Member.MemberRelationship)cacheManager.GetObject (cacheKey);

                if (memberRelationship == null) {

                    Server.Application.MemberRelationship serverMemberRelationship = ApplicationClient.MemberRelationshipGet (session.Token, memberRelationshipId);

                    if (serverMemberRelationship != null) { memberRelationship = new Mercury.Client.Core.Member.MemberRelationship (this, serverMemberRelationship); }

                    cacheManager.CacheObject (cacheKey, memberRelationship, CacheExpirationData);

                }

                else { memberRelationship.Application = this; }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberRelationship;

        }

        public List<Mercury.Client.Core.Member.MemberRelationship> MemberRelationshipsGet (Int64 memberId, Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".MemberRelationship.ByMemberId." + memberId.ToString ();

            List<Mercury.Client.Core.Member.MemberRelationship> memberRelationships = null;

            Server.Application.MemberRelationshipCollectionResponse response;

            ClearLastException ();

            try {

                memberRelationships = (List<Core.Member.MemberRelationship>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { memberRelationships = null; }

                if (memberRelationships == null) {

                    response = ApplicationClient.MemberRelationshipsGet (session.Token, memberId);

                    if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                    memberRelationships = new List<Mercury.Client.Core.Member.MemberRelationship> ();

                    foreach (Server.Application.MemberRelationship currentServerCoverage in response.Collection) {

                        memberRelationships.Add (new Mercury.Client.Core.Member.MemberRelationship (this, currentServerCoverage));

                    }

                    cacheManager.CacheObject (cacheKey, memberRelationships, CacheExpirationData);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberRelationships;

        }

        #endregion

        #endregion


        #region Metrics

        public List<Core.Metrics.Metric> MetricsAvailable (Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".Metric.Available";

            Mercury.Server.Application.MetricCollectionResponse response = new Mercury.Server.Application.MetricCollectionResponse ();

            List<Core.Metrics.Metric> metrics = new List<Mercury.Client.Core.Metrics.Metric> ();


            ClearLastException ();

            try {

                metrics = (List<Core.Metrics.Metric>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { metrics = null; }

                if (metrics == null) {

                    metrics = new List<Mercury.Client.Core.Metrics.Metric> ();

                    response = ApplicationClient.MetricsAvailable (session.Token);

                    if (!response.HasException) {

                        foreach (Mercury.Server.Application.Metric currentMetric in response.Collection) {

                            metrics.Add (new Mercury.Client.Core.Metrics.Metric (this, currentMetric));

                        }

                        if (metrics.Count > 0) { cacheManager.CacheObject (cacheKey, metrics, CacheExpirationData); }

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return metrics;

        }

        public Core.Metrics.Metric MetricGet (Int64 metricId, Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".Metric." + metricId.ToString ();

            Core.Metrics.Metric metric = null;

            Mercury.Server.Application.Metric serverMetric = null;

            ClearLastException ();

            try {

                metric = (Core.Metrics.Metric)cacheManager.GetObject (cacheKey);

                if (!useCaching) { metric = null; }

                if (metric == null) {

                    serverMetric = ApplicationClient.MetricGet (session.Token, metricId);

                    if (serverMetric != null) {

                        metric = new Mercury.Client.Core.Metrics.Metric (this, serverMetric);

                        cacheManager.CacheObject (cacheKey, metric, CacheExpirationData);

                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return metric;

        }

        public Boolean MetricSave (Core.Metrics.Metric metric) {

            Mercury.Server.Application.ObjectSaveResponse response = new Mercury.Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".Metric.Available");

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".Metric.Dictionary");

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".Metric." + metric.Id.ToString ());

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".Metric." + metric.Name);

                response = ApplicationClient.MetricSave (session.Token, (Mercury.Server.Application.Metric)metric.ToServerObject ());

                if (!response.HasException) { metric.SetId (response.Id); }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                response.HasException = true;

                response.Exception = new Mercury.Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;

                SetLastException (applicationException);

            }

            return response.Success;

        }


        public Boolean MemberMetricAddManual (Int64 memberId, Int64 metricId, DateTime eventDate, Decimal value) {

            Mercury.Server.Application.ObjectSaveResponse response = new Mercury.Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.MemberMetricAddManual (session.Token, memberId, metricId, eventDate, value);

                if (response.HasException) {

                    SetLastException (new ApplicationException (response.Exception.Message));

                }

            }

            catch (Exception applicationException) {

                response.Success = false;

                response.HasException = true;

                response.Exception = new Mercury.Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;

                SetLastException (applicationException);

            }

            return response.Success;

        }

        public Boolean MemberMetricRemoveManual (Int64 memberMetricId) {

            Mercury.Server.Application.BooleanResponse response = new Mercury.Server.Application.BooleanResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.MemberMetricRemoveManual (session.Token, memberMetricId);

                if (response.HasException) {

                    SetLastException (new ApplicationException (response.Exception.Message));

                }

            }

            catch (Exception applicationException) {

                response.Result = false;

                response.HasException = true;

                response.Exception = new Mercury.Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;

                SetLastException (applicationException);

            }

            return response.Result;

        }


        public Int64 MemberMetricsGetCount (Int64 memberId, Boolean showHidden) {

            Int64 servicesCount = 0;


            ClearLastException ();

            try {

                servicesCount = ApplicationClient.MemberMetricsGetCount (session.Token, memberId, showHidden);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return servicesCount;

        }

        public List<Mercury.Server.Application.MemberMetric> MemberMetricsGetByPage (Int64 memberId, Int32 initialRow, Int32 count, Boolean showHidden) {

            Mercury.Server.Application.MemberMetricCollectionResponse response;

            List<Mercury.Server.Application.MemberMetric> memberMetrics = null;


            ClearLastException ();


            try {

                response = ApplicationClient.MemberMetricsGetByPage (session.Token, memberId, initialRow, count, showHidden);

                if (!response.HasException) {

                    memberMetrics = new List<Mercury.Server.Application.MemberMetric> ();

                    memberMetrics.AddRange (response.Collection);

                }

                else { SetLastException (response.Exception); }
                    
            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return memberMetrics;

        }

        #endregion


        #region Populations

        #region Population Type

        public List<Core.Population.PopulationType> PopulationTypesAvailable (Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".PopulationType.Available";

            Mercury.Server.Application.PopulationTypeCollectionResponse response = new Mercury.Server.Application.PopulationTypeCollectionResponse ();

            List<Core.Population.PopulationType> populationTypes = new List<Core.Population.PopulationType> ();
            
            Dictionary<Int64, String> coreObjectDictionary = new Dictionary<Int64, String> ();


            ClearLastException ();

            try {

                populationTypes = (List<Core.Population.PopulationType>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { populationTypes = null; }

                if (populationTypes == null) {

                    populationTypes = new List<Core.Population.PopulationType> ();

                    response = ApplicationClient.PopulationTypesAvailable (session.Token);

                    if (!response.HasException) {

                        foreach (Server.Application.PopulationType currentServerPopulationType in response.Collection) {

                            Core.Population.PopulationType populationType = new Core.Population.PopulationType (this, currentServerPopulationType);

                            populationTypes.Add (populationType);


                            // CACHE EACH INDIVIDUAL WORK QUEUE LOADED FROM SERVER (BY ID AND NAME)

                            coreObjectDictionary.Add (populationType.Id, populationType.Name);

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".PopulationType." + populationType.Id.ToString (), populationType, CacheExpirationData);

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".PopulationType." + populationType.Name, populationType, CacheExpirationData);

                        }

                        if (populationTypes.Count > 0) {

                            cacheManager.CacheObject (cacheKey, populationTypes, CacheExpirationData);

                            // CACHE THE AVAILABILITY LIST

                            cacheManager.CacheObject (cacheKey, populationTypes, CacheExpirationData);

                            // CACHE THE DICTIONARY THAT WAS CREATED

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".WorkQueue.Dictionary", coreObjectDictionary, CacheExpirationData);

                        }

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return populationTypes;

        }

        public Core.Population.PopulationType PopulationTypeGet (Int64 populationTypeId, Boolean useCaching) {

            if (populationTypeId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".PopulationType." + populationTypeId.ToString ();


            Core.Population.PopulationType populationType = null;

            Mercury.Server.Application.PopulationType serverPopulationType = null;

            ClearLastException ();

            try {

                if (useCaching) { populationType = (Core.Population.PopulationType)cacheManager.GetObject (cacheKey); }

                if (populationType == null) {

                    serverPopulationType = ApplicationClient.PopulationTypeGet (session.Token, populationTypeId);

                    if (serverPopulationType != null) { populationType = new Mercury.Client.Core.Population.PopulationType (this, serverPopulationType); }

                    if (populationType != null) { cacheManager.CacheObject (cacheKey, populationType, CacheExpirationData); }

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return populationType;

        }

        public Boolean PopulationTypeSave (Core.Population.PopulationType populationType) {

            Mercury.Server.Application.ObjectSaveResponse response = new Mercury.Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.PopulationTypeSave (session.Token, (Mercury.Server.Application.PopulationType)populationType.ToServerObject ());

                if (!response.HasException) {

                    populationType.SetId (response.Id);

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".PopulationType.Available");

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".PopulationType.Dictionary");

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".PopulationType." + populationType.Id.ToString ());

                    cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".PopulationType." + populationType.Name);

                }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                response.Success = false;

                SetLastException (applicationException);

            }

            return response.Success;

        }

        #endregion 


        public List<Mercury.Server.Application.SearchResultPopulationHeader> PopulationsAvailable (Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".Core.PopulationsAvailable.";


            List<Mercury.Server.Application.SearchResultPopulationHeader> result = new List<Mercury.Server.Application.SearchResultPopulationHeader> ();

            Mercury.Server.Application.PopulationHeadersCollectionResponse response = new Mercury.Server.Application.PopulationHeadersCollectionResponse ();

            ClearLastException ();

            try {

                if (useCaching) { result = (List<Mercury.Server.Application.SearchResultPopulationHeader>)cacheManager.GetObject (cacheKey); }

                Boolean refreshData = (result == null) ? true : (result.Count == 0);

                if (refreshData) {

                    response = ApplicationClient.PopulationsAvailable (session.Token);

                    if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                    else {

                        result = new List<Mercury.Server.Application.SearchResultPopulationHeader> ();

                        result.AddRange (response.Collection);

                        if (result.Count > 0) { cacheManager.CacheObject (cacheKey, result, CacheExpirationData); }

                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return result;

        }

        public Core.Population.Population PopulationGet (Int64 populationId, Boolean useCaching) {

            if (populationId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".Core.Population." + populationId.ToString ();

            Core.Population.Population population = null;

            Mercury.Server.Application.Population serverPopulation = null;


            ClearLastException ();

            try {

                if (useCaching) { population = (Core.Population.Population)cacheManager.GetObject (cacheKey); }

                if (population == null) {

                    serverPopulation = ApplicationClient.PopulationGet (session.Token, populationId);

                    if (serverPopulation != null) { population = new Mercury.Client.Core.Population.Population (this, serverPopulation); }

                    if (population != null) { cacheManager.CacheObject (cacheKey, population, CacheExpirationData); }

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return population;

        }

        public Core.Population.PopulationEvents.PopulationServiceEvent PopulationServiceEventGet (Int64 populationServiceEventId, Boolean useCaching) {

            if (populationServiceEventId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".Core.Population.ServiceEvent." + populationServiceEventId.ToString ();


            Core.Population.PopulationEvents.PopulationServiceEvent populationServiceEvent = null;

            Mercury.Server.Application.PopulationServiceEvent serverPopulationServiceEvent = null;

            ClearLastException ();

            try {

                if (useCaching) { populationServiceEvent = (Core.Population.PopulationEvents.PopulationServiceEvent)cacheManager.GetObject (cacheKey); }

                if (populationServiceEvent == null) {

                    serverPopulationServiceEvent = ApplicationClient.PopulationServiceEventGet (session.Token, populationServiceEventId);

                    if (serverPopulationServiceEvent != null) { populationServiceEvent = new Mercury.Client.Core.Population.PopulationEvents.PopulationServiceEvent (this, serverPopulationServiceEvent); }

                    if (populationServiceEvent != null) { cacheManager.CacheObject (cacheKey, populationServiceEvent, CacheExpirationData); }

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return populationServiceEvent;

        }

        public Boolean PopulationSave (Core.Population.Population population) {

            Mercury.Server.Application.ObjectSaveResponse response = new Mercury.Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.PopulationSave (session.Token, (Mercury.Server.Application.Population)population.ToServerObject ());

                if (response.HasException) {

                    SetLastException (new ApplicationException (response.Exception.Message));

                }

                else {

                    population.SetId (response.Id);

                }

            }

            catch (Exception applicationException) {

                response.HasException = true;

                response.Exception = new Mercury.Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;

                SetLastException (applicationException);

            }

            return response.Success;

        }

        public Boolean PopulationDelete (Int64 populationId) {

            Mercury.Server.Application.BooleanResponse response = new Mercury.Server.Application.BooleanResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.PopulationDelete (session.Token, populationId);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return response.Result;

        }

        public Dictionary<String, String> Population_DataBindingContexts (Mercury.Server.Application.Population serverPopulation) {

            String cacheKey = "Application." + session.EnvironmentId + ".Core.Population.Population.DataBindingContexts." + serverPopulation.Id.ToString ();

            Dictionary<String, String> bindingContexts = new Dictionary<String, String> ();

            ClearLastException ();

            try {

                bindingContexts = (Dictionary<String, String>)cacheManager.GetObject (cacheKey);

                if ((bindingContexts == null) || (serverPopulation.Id != 0)) {

                    bindingContexts = ApplicationClient.Population_DataBindingContexts (session.Token, serverPopulation);

                    cacheManager.CacheObject (cacheKey, bindingContexts, CacheExpirationData);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return bindingContexts;

        }


        #region Population Membership

        public Core.Population.PopulationMembership PopulationMembershipGet (Int64 populationMembershipId, Boolean useCaching) {

            if (populationMembershipId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".Core.PopulationMembership." + populationMembershipId.ToString ();


            Core.Population.PopulationMembership populationMembership = null;

            Mercury.Server.Application.PopulationMembership serverPopulationMembership = null;

            ClearLastException ();

            try {

                populationMembership = (Core.Population.PopulationMembership)cacheManager.GetObject (cacheKey);

                if (!useCaching) { populationMembership = null; }

                if (populationMembership == null) {

                    serverPopulationMembership = ApplicationClient.PopulationMembershipGet (session.Token, populationMembershipId);

                    if (serverPopulationMembership != null) { populationMembership = new Mercury.Client.Core.Population.PopulationMembership (this, serverPopulationMembership); }

                    if (populationMembership != null) { cacheManager.CacheObject (cacheKey, populationMembership, CacheExpirationData); }

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return populationMembership;

        }

        public Core.Population.PopulationEvents.PopulationMembershipServiceEvent PopulationMembershipServiceEventGet (Int64 populationMembershipServiceEventId) {

            if (populationMembershipServiceEventId == 0) { return null; }


            Boolean useCaching = false;

            String cacheKey = "Application." + session.EnvironmentId + ".Core.PopulationMembershipServiceEvent." + populationMembershipServiceEventId.ToString ();


            Core.Population.PopulationEvents.PopulationMembershipServiceEvent populationMembershipServiceEvent = null;

            Mercury.Server.Application.PopulationMembershipServiceEvent serverPopulationMembershipServiceEvent = null;

            ClearLastException ();

            try {

                if (useCaching) { populationMembershipServiceEvent = (Core.Population.PopulationEvents.PopulationMembershipServiceEvent)cacheManager.GetObject (cacheKey); }

                if (populationMembershipServiceEvent == null) {

                    serverPopulationMembershipServiceEvent = ApplicationClient.PopulationMembershipServiceEventGet (session.Token, populationMembershipServiceEventId);

                    if (serverPopulationMembershipServiceEvent != null) { populationMembershipServiceEvent = new Mercury.Client.Core.Population.PopulationEvents.PopulationMembershipServiceEvent (this, serverPopulationMembershipServiceEvent); }

                    if (populationMembershipServiceEvent != null) { cacheManager.CacheObject (cacheKey, populationMembershipServiceEvent, CacheExpirationData); }

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return populationMembershipServiceEvent;

        }

        public List<Core.Population.PopulationEvents.PopulationMembershipServiceEvent> PopulationMembershipServiceEventsGet (Int64 populationMembershipId, Boolean useCaching) {

            if (populationMembershipId == 0) { return new List<Mercury.Client.Core.Population.PopulationEvents.PopulationMembershipServiceEvent> (); }


            String cacheKey = "Application." + session.EnvironmentId + ".Core.PopulationMembershipServiceEvents." + populationMembershipId.ToString ();


            List<Core.Population.PopulationEvents.PopulationMembershipServiceEvent> serviceEvents = new List<Mercury.Client.Core.Population.PopulationEvents.PopulationMembershipServiceEvent> ();



            ClearLastException ();

            try {

                serviceEvents = (List<Core.Population.PopulationEvents.PopulationMembershipServiceEvent>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { serviceEvents = null; }

                if (serviceEvents == null) {

                    serviceEvents = new List<Mercury.Client.Core.Population.PopulationEvents.PopulationMembershipServiceEvent> ();

                    Mercury.Server.Application.PopulationMembershipServiceEventCollectionResponse response;

                    response = ApplicationClient.PopulationMembershipServiceEventsGetByPopulationMembership (session.Token, populationMembershipId);

                    foreach (Mercury.Server.Application.PopulationMembershipServiceEvent currentServerServiceEvent in response.Collection) {

                        Core.Population.PopulationEvents.PopulationMembershipServiceEvent populationMembershipServiceEvent;

                        populationMembershipServiceEvent = new Mercury.Client.Core.Population.PopulationEvents.PopulationMembershipServiceEvent (this, currentServerServiceEvent);

                        serviceEvents.Add (populationMembershipServiceEvent);

                    }

                    if (serviceEvents != null) { cacheManager.CacheObject (cacheKey, serviceEvents, CacheExpirationData); }

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return serviceEvents;

        }


        public List<Mercury.Server.Application.PopulationMembershipTriggerEventDataView> PopulationMembershipTriggerEventsGetDataView (Int64 populationMembershipId, Boolean useCaching) {

            if (populationMembershipId == 0) { return new List<Mercury.Server.Application.PopulationMembershipTriggerEventDataView> (); }


            String cacheKey = "Application." + session.EnvironmentId + ".Core.PopulationMembershipTriggerEvents.DataView" + populationMembershipId.ToString ();


            List<Mercury.Server.Application.PopulationMembershipTriggerEventDataView> triggerEvents = new List<Mercury.Server.Application.PopulationMembershipTriggerEventDataView> ();



            ClearLastException ();

            try {

                triggerEvents = (List<Mercury.Server.Application.PopulationMembershipTriggerEventDataView>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { triggerEvents = null; }

                if (triggerEvents == null) {

                    triggerEvents = new List<Mercury.Server.Application.PopulationMembershipTriggerEventDataView> ();

                    Mercury.Server.Application.PopulationMembershipTriggerEventDataViewCollectionResponse response;

                    response = ApplicationClient.PopulationMembershipTriggerEventsByMembershipDataView (session.Token, populationMembershipId);

                    if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                    triggerEvents.AddRange (response.Collection);

                    if (triggerEvents != null) { cacheManager.CacheObject (cacheKey, triggerEvents, CacheExpirationData); }

                }

            }


            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return triggerEvents;

        }

        public Int64 PopulationMembershipGetCountByName (Int64 populationId, String namePrefix, Boolean useCaching) {

            if (populationId == 0) { return 0; }


            String cacheKey = "Application." + session.EnvironmentId + ".Core.PopulationMembership.CountByPopulationNamePrefix" + populationId.ToString () + "." + namePrefix;

            Int64 itemCount = 0;


            ClearLastException ();

            try {

                if ((useCaching) && (cacheManager.GetObject (cacheKey) != null)) {

                    itemCount = (Int64)cacheManager.GetObject (cacheKey);

                }

                if (itemCount == 0) {

                    itemCount = ApplicationClient.PopulationMembershipGetCountByName (session.Token, populationId, namePrefix);

                    if (itemCount != 0) { cacheManager.CacheObject (cacheKey, itemCount, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return itemCount;

        }

        public List<Mercury.Server.Application.PopulationMembershipEntryStatusDataView> PopulationMembershipGetByMembershipPage (Int64 populationId, String namePrefix, Int64 initialRow, Int32 count) {

            if (populationId == 0) { return new List<Mercury.Server.Application.PopulationMembershipEntryStatusDataView> (); }


            String cacheKey = "Application." + session.EnvironmentId + ".Core.PopulationMembership.ByPopulationNamePrefix" + populationId.ToString () + "." + namePrefix + "." + initialRow.ToString () + "." + count.ToString ();


            List<Mercury.Server.Application.PopulationMembershipEntryStatusDataView> membership = null;

            Mercury.Server.Application.PopulationMembershipEntryStatusDataViewCollectionResponse response;

            ClearLastException ();


            try {

                membership = (List<Mercury.Server.Application.PopulationMembershipEntryStatusDataView>)cacheManager.GetObject (cacheKey);

                if (membership == null) {

                    membership = new List<Mercury.Server.Application.PopulationMembershipEntryStatusDataView> ();

                    response = ApplicationClient.PopulationMembershipGetByMembershipPage (session.Token, populationId, namePrefix, initialRow, count);

                    if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                    membership.AddRange (response.Collection);

                    cacheManager.CacheObject (cacheKey, membership, CacheExpirationData);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return membership;

        }


        public List<Client.Core.Population.PopulationMembership> PopulationMembershipGetByMember (Int64 memberId, Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".PopulationMembership.ByMemberId" + memberId;

            Server.Application.PopulationMembershipCollectionResponse response;

            List<Core.Population.PopulationMembership> membership = new List<Core.Population.PopulationMembership> ();


            ClearLastException ();

            try {

                membership = (List<Core.Population.PopulationMembership>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { membership = null; }

                if (membership == null) {

                    membership = new List<Core.Population.PopulationMembership> ();

                    response = ApplicationClient.PopulationMembershipGetByMember (session.Token, memberId);

                    if (!response.HasException) {

                        foreach (Server.Application.PopulationMembership currentServerMembership in response.Collection) {

                            Core.Population.PopulationMembership populationMembership = new Mercury.Client.Core.Population.PopulationMembership (this, currentServerMembership);

                            membership.Add (populationMembership);

                        }

                        if (membership.Count > 0) {

                            cacheManager.CacheObject (cacheKey, membership, CacheExpirationData);

                        }

                    }

                    else { SetLastException (response.Exception); }

                }
                
            }

            catch (Exception ApplicationException) {

                SetLastException (ApplicationException);

            }

            return membership;
        }

        public List<Server.Application.PopulationMembershipSummaryDataView> PopulationMembershipSummary (Int64 memberId, Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".PopulationMembershipSummary.ByMemberId." + memberId;

            Server.Application.PopulationMembershipSummaryCollectionResponse response;

            List<Server.Application.PopulationMembershipSummaryDataView> summary = new List<Server.Application.PopulationMembershipSummaryDataView> ();


            ClearLastException ();


            try {

                summary = (List<Server.Application.PopulationMembershipSummaryDataView>) cacheManager.GetObject (cacheKey);

                if (!useCaching) { summary = null; }

                if (summary == null) {

                    summary = new List<Server.Application.PopulationMembershipSummaryDataView> ();

                    response = ApplicationClient.PopulationMembershipSummaryByMember (session.Token, memberId);

                    if (!response.HasException) {

                        summary.AddRange (response.Collection);

                        if (summary.Count > 0) { cacheManager.CacheObject (cacheKey, summary, CacheExpirationData); }

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception ApplicationException) {

                SetLastException (ApplicationException);

            }

            return summary;

        }

        public List<Server.Application.PopulationMembershipServiceEventDataView> PopulationMembershipServiceEventsDataView (Int64 membershipId) {

            Server.Application.PopulationMembershipServiceEventDataViewCollectionResponse response;

            List<Server.Application.PopulationMembershipServiceEventDataView> serviceEvents = new List<Server.Application.PopulationMembershipServiceEventDataView> ();

            ClearLastException ();

            try {

                response = ApplicationClient.PopulationMembershipServiceEventsByMembershipDataView (Session.Token, membershipId);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                serviceEvents.AddRange (response.Collection);

            }

            catch (Exception ApplicationException) {

                SetLastException (ApplicationException);

            }

            return serviceEvents;

        }

        public List<Server.Application.PopulationMembershipTriggerEventDataView> PopulationMembershipTriggerEventsDataView (Int64 membershipId) {

            Server.Application.PopulationMembershipTriggerEventDataViewCollectionResponse response;

            List<Server.Application.PopulationMembershipTriggerEventDataView> triggerEvents = new List<Server.Application.PopulationMembershipTriggerEventDataView> ();

            ClearLastException ();

            try {

                response = ApplicationClient.PopulationMembershipTriggerEventsByMembershipDataView (Session.Token, membershipId);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                triggerEvents.AddRange (response.Collection);

            }

            catch (Exception ApplicationException) {

                SetLastException (ApplicationException);

            }

            return triggerEvents;

        }

        #endregion 

        #endregion


        #region Core - Provider

        #region Provider

        public Core.Provider.Provider ProviderGet (Int64 providerId, Boolean useCaching) {

            if (providerId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".Provider." + providerId.ToString ();

            Core.Provider.Provider provider = null;

            ClearLastException ();


            try {

                provider = (Core.Provider.Provider)cacheManager.GetObject (cacheKey);

                if (!useCaching) { provider = null; }

                if (provider == null) {

                    Server.Application.Provider serverProvider = ApplicationClient.ProviderGet (session.Token, providerId);

                    if (serverProvider != null) { provider = new Core.Provider.Provider (this, serverProvider); }

                    if (provider != null) { cacheManager.CacheObject (cacheKey, provider, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return provider;

        }

        public Core.Provider.Provider ProviderGetByEntityId (Int64 entityId, Boolean useCaching) {

            if (entityId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + "." + typeof (Core.Provider.Provider).ToString () + ".ByEntityId." + entityId.ToString ();

            Core.Provider.Provider provider = null;

            ClearLastException ();


            try {

                provider = (Core.Provider.Provider)cacheManager.GetObject (cacheKey);

                if (!useCaching) { provider = null; }

                if (provider == null) {

                    Server.Application.Provider serverProvider = ApplicationClient.ProviderGetByEntityId (session.Token, entityId);

                    if (serverProvider != null) { provider = new Core.Provider.Provider (this, serverProvider); }

                    if (provider != null) { cacheManager.CacheObject (cacheKey, provider, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return provider;

        }

        #endregion 
        

        public List<Mercury.Client.Core.Provider.ProviderEnrollment> ProviderEnrollmentsGet (Int64 providerId, Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".ProviderEnrollments." + providerId.ToString ();

            List<Mercury.Client.Core.Provider.ProviderEnrollment> enrollments = new List<Mercury.Client.Core.Provider.ProviderEnrollment> ();

            Mercury.Server.Application.ProviderEnrollmentCollectionResponse response;

            ClearLastException ();

            try {

                enrollments = (List<Core.Provider.ProviderEnrollment>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { enrollments = null; }

                if (enrollments == null) {

                    enrollments = new List<Core.Provider.ProviderEnrollment> ();

                    response = ApplicationClient.ProviderEnrollmentsGet (session.Token, providerId);


                    if (!response.HasException) {

                        foreach (Server.Application.ProviderEnrollment currentServerObject in response.Collection) {

                            Core.Provider.ProviderEnrollment clientObject = new Core.Provider.ProviderEnrollment (this, currentServerObject);

                            enrollments.Add (clientObject);


                            // CACHE SINGLE INSTANCE

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".ProviderEnrollment." + clientObject.Id.ToString (), clientObject, CacheExpirationData);

                        }

                        // CACHE COLLECTION 

                        if (enrollments.Count > 0) { cacheManager.CacheObject (cacheKey, enrollments, CacheExpirationData); }

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return enrollments;

        }

        public List<Mercury.Client.Core.Provider.ProviderEnrollment> ProviderEnrollmentsGet (Int64 providerId) { return ProviderEnrollmentsGet (providerId, true); }

        public Mercury.Client.Core.Provider.ProviderEnrollment ProviderEnrollmentGet (Int64 providerEnrollmentId, Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".ProviderEnrollment." + providerEnrollmentId.ToString ();

            Mercury.Client.Core.Provider.ProviderEnrollment providerEnrollment = null;

            ClearLastException ();

            try {

                providerEnrollment = (Core.Provider.ProviderEnrollment)cacheManager.GetObject (cacheKey);

                if (!useCaching) { providerEnrollment = null; }

                if (providerEnrollment == null) {

                    Mercury.Server.Application.ProviderEnrollment serverProviderEnrollment = ApplicationClient.ProviderEnrollmentGet (session.Token, providerEnrollmentId);

                    if (serverProviderEnrollment != null) {

                        providerEnrollment = new Mercury.Client.Core.Provider.ProviderEnrollment (this, serverProviderEnrollment);

                        cacheManager.CacheObject (cacheKey, providerEnrollment, CacheExpirationData);

                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return providerEnrollment;

        }

        public Mercury.Client.Core.Provider.ProviderEnrollment ProviderEnrollmentGet (Int64 providerEnrollmentId) { return ProviderEnrollmentGet (providerEnrollmentId, true); }


        public List<Mercury.Client.Core.Provider.ProviderAffiliation> ProviderAffiliationsGet (Int64 providerId, Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".ProviderAffiliations." + providerId.ToString ();

            List<Mercury.Client.Core.Provider.ProviderAffiliation> affiliations = new List<Mercury.Client.Core.Provider.ProviderAffiliation> ();

            Mercury.Server.Application.ProviderAffiliationCollectionResponse response;


            ClearLastException ();

            try {


                affiliations = (List<Core.Provider.ProviderAffiliation>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { affiliations = null; }

                if (affiliations == null) {

                    affiliations = new List<Core.Provider.ProviderAffiliation> ();

                    response = ApplicationClient.ProviderAffiliationsGet (session.Token, providerId);


                    if (!response.HasException) {

                        foreach (Server.Application.ProviderAffiliation currentServerObject in response.Collection) {

                            Core.Provider.ProviderAffiliation clientObject = new Core.Provider.ProviderAffiliation (this, currentServerObject);

                            affiliations.Add (clientObject);


                            // CACHE SINGLE INSTANCE

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".ProviderAffiliation." + clientObject.Id.ToString (), clientObject, CacheExpirationData);

                        }

                        // CACHE COLLECTION 

                        if (affiliations.Count > 0) { cacheManager.CacheObject (cacheKey, affiliations, CacheExpirationData); }

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return affiliations;

        }

        public List<Mercury.Client.Core.Provider.ProviderAffiliation> ProviderAffiliationsGet (Int64 providerId) { return ProviderAffiliationsGet (providerId, true); }

        public Mercury.Client.Core.Provider.ProviderAffiliation ProviderAffiliationGet (Int64 providerAffiliationId, Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".ProviderAffiliation." + providerAffiliationId.ToString ();

            Mercury.Client.Core.Provider.ProviderAffiliation providerAffiliation = null;

            ClearLastException ();

            try {

                providerAffiliation = (Core.Provider.ProviderAffiliation)cacheManager.GetObject (cacheKey);

                if (!useCaching) { providerAffiliation = null; }

                if (providerAffiliation == null) {

                    Mercury.Server.Application.ProviderAffiliation serverProviderAffiliation = ApplicationClient.ProviderAffiliationGet (session.Token, providerAffiliationId);

                    if (serverProviderAffiliation != null) {

                        providerAffiliation = new Mercury.Client.Core.Provider.ProviderAffiliation (this, serverProviderAffiliation);

                        cacheManager.CacheObject (cacheKey, providerAffiliation, CacheExpirationData);

                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return providerAffiliation;

        }

        public Mercury.Client.Core.Provider.ProviderAffiliation ProviderAffiliationGet (Int64 providerAffiliationId) { return ProviderAffiliationGet (providerAffiliationId, true); }



        public List<Mercury.Client.Core.Provider.ProviderContract> ProviderContractsGet (Int64 providerId, Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".ProviderContracts." + providerId.ToString ();

            List<Mercury.Client.Core.Provider.ProviderContract> contracts = new List<Mercury.Client.Core.Provider.ProviderContract> ();

            Mercury.Server.Application.ProviderContractCollectionResponse response;

            ClearLastException ();

            try {

                contracts = (List<Core.Provider.ProviderContract>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { contracts = null; }

                if (contracts == null) {

                    contracts = new List<Core.Provider.ProviderContract> ();

                    response = ApplicationClient.ProviderContractsGet (session.Token, providerId);


                    if (!response.HasException) {

                        foreach (Server.Application.ProviderContract currentServerObject in response.Collection) {

                            Core.Provider.ProviderContract clientObject = new Core.Provider.ProviderContract (this, currentServerObject);

                            contracts.Add (clientObject);


                            // CACHE SINGLE INSTANCE

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".ProviderContract." + clientObject.Id.ToString (), clientObject, CacheExpirationData);

                        }

                        // CACHE COLLECTION 

                        if (contracts.Count > 0) { cacheManager.CacheObject (cacheKey, contracts, CacheExpirationData); }

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return contracts;

        }

        public List<Mercury.Client.Core.Provider.ProviderContract> ProviderContractsGet (Int64 providerId) { return ProviderContractsGet (providerId, true); }

        public Core.Provider.ProviderContract ProviderContractGet (Int64 providerContractId, Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".ProviderContract." + providerContractId.ToString ();

            Mercury.Client.Core.Provider.ProviderContract providerContract = null;

            ClearLastException ();

            try {

                providerContract = (Core.Provider.ProviderContract)cacheManager.GetObject (cacheKey);

                if (useCaching) { providerContract = null; }

                if (providerContract == null) {

                    Mercury.Server.Application.ProviderContract serverProviderContract = ApplicationClient.ProviderContractGet (session.Token, providerContractId);

                    if (serverProviderContract != null) {

                        providerContract = new Mercury.Client.Core.Provider.ProviderContract (this, serverProviderContract);

                        cacheManager.CacheObject (cacheKey, providerContract, CacheExpirationData);

                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return providerContract;

        }

        public Core.Provider.ProviderContract ProviderContractGet (Int64 providerContractId) { return ProviderContractGet (providerContractId, true); }


        public List<Mercury.Client.Core.Provider.ProviderServiceLocation> ProviderServiceLocationsGet (Int64 providerId, Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".ProviderServiceLocations." + providerId.ToString ();

            List<Mercury.Client.Core.Provider.ProviderServiceLocation> serviceLocations = new List<Mercury.Client.Core.Provider.ProviderServiceLocation> ();

            Mercury.Server.Application.ProviderServiceLocationCollectionResponse response;

            ClearLastException ();

            try {

                serviceLocations = (List<Core.Provider.ProviderServiceLocation>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { serviceLocations = null; }

                if (serviceLocations == null) {

                    serviceLocations = new List<Core.Provider.ProviderServiceLocation> ();

                    response = ApplicationClient.ProviderServiceLocationsGet (session.Token, providerId);


                    if (!response.HasException) {

                        foreach (Server.Application.ProviderServiceLocation currentServerObject in response.Collection) {

                            Core.Provider.ProviderServiceLocation clientObject = new Core.Provider.ProviderServiceLocation (this, currentServerObject);

                            serviceLocations.Add (clientObject);


                            // CACHE SINGLE INSTANCE

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".ProviderServiceLocation." + clientObject.Id.ToString (), clientObject, CacheExpirationData);

                        }

                        // CACHE COLLECTION 

                        if (serviceLocations.Count > 0) { cacheManager.CacheObject (cacheKey, serviceLocations, CacheExpirationData); }

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return serviceLocations;
        }

        public List<Mercury.Client.Core.Provider.ProviderServiceLocation> ProviderServiceLocationsGet (Int64 providerId) { return ProviderServiceLocationsGet (providerId, true); }

        #endregion 


        #region Core - Sponsor

        public Core.Sponsor.Sponsor SponsorGet (Int64 sponsorId, Boolean useCaching) {

            if (sponsorId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + "." + typeof (Core.Sponsor.Sponsor).ToString () + "." + sponsorId.ToString ();

            Core.Sponsor.Sponsor sponsor = null;

            ClearLastException ();


            try {

                sponsor = (Core.Sponsor.Sponsor)cacheManager.GetObject (cacheKey);

                if (!useCaching) { sponsor = null; }

                if (sponsor == null) {

                    Server.Application.Sponsor serverSponsor = ApplicationClient.SponsorGet (session.Token, sponsorId);

                    if (serverSponsor != null) { sponsor = new Core.Sponsor.Sponsor (this, serverSponsor); }

                    if (sponsor != null) { cacheManager.CacheObject (cacheKey, sponsor, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return sponsor;

        }

        #endregion 


        #region Core.Work

        #region Routing Rules

        public List<Core.Work.RoutingRule> RoutingRulesAvailable (Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".RoutingRule.Available";
                        
            Server.Application.RoutingRuleCollectionResponse response = new Server.Application.RoutingRuleCollectionResponse ();

            List<Core.Work.RoutingRule> routingRules = new List<Mercury.Client.Core.Work.RoutingRule> ();


            ClearLastException ();

            try {

                routingRules = (List<Core.Work.RoutingRule>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { routingRules = null; }

                if (routingRules == null) {

                    routingRules = new List<Mercury.Client.Core.Work.RoutingRule> ();

                    response = ApplicationClient.RoutingRulesAvailable (session.Token);

                    if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                    foreach (Server.Application.RoutingRule currentRoutingRule in response.Collection) {

                        routingRules.Add (new Mercury.Client.Core.Work.RoutingRule (this, currentRoutingRule));

                    }

                    cacheManager.CacheObject (cacheKey, routingRules, CacheExpirationData);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return routingRules;

        }

        public Core.Work.RoutingRule RoutingRuleGet (Int64 routingRuleId) {

            Core.Work.RoutingRule routingRule = null;

            Server.Application.RoutingRule serverRoutingRule = null;

            ClearLastException ();

            try {

                serverRoutingRule = ApplicationClient.RoutingRuleGet (session.Token, routingRuleId);

                if (serverRoutingRule != null) { routingRule = new Mercury.Client.Core.Work.RoutingRule (this, serverRoutingRule); }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return routingRule;

        }

        public Boolean RoutingRuleSave (Core.Work.RoutingRule routingRule) {

            Server.Application.ObjectSaveResponse response = new Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.RoutingRuleSave (session.Token, (Server.Application.RoutingRule)routingRule.ToServerObject ());

                if (!response.HasException) {

                    // CLEAR CACHE

                    routingRule.SetId (response.Id);

                }

                else { SetLastException (response.Exception); }


            }

            catch (Exception applicationException) {

                response.Success = false;

                SetLastException (applicationException);

            }

            return response.Success;

        }

        #endregion


        #region Work - Workflow

        public List<Core.Work.Workflow> WorkflowsAvailable (Boolean useCaching) {

            List<Core.Work.Workflow> workflows = new List<Core.Work.Workflow> ();

            Mercury.Server.Application.WorkflowCollectionResponse response;

            String cacheKey = "Application." + session.EnvironmentId + ".Workflow.Available";


            ClearLastException ();

            try {

                workflows = (List<Core.Work.Workflow>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { workflows = null; }

                if (workflows == null) {

                    workflows = new List<Core.Work.Workflow> ();

                    Dictionary<Int64, String> objectDictionary = new Dictionary<Int64, String> ();

                    response = ApplicationClient.WorkflowsAvailable (session.Token);

                    if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                    foreach (Server.Application.Workflow currentServerWorkflow in response.Collection) {

                        Core.Work.Workflow workflow = new Core.Work.Workflow (this, currentServerWorkflow);

                        workflows.Add (workflow);

                        objectDictionary.Add (workflow.Id, workflow.Name);


                        // CACHE EACH INDIVIDUAL WORK QUEUE LOADED FROM SERVER (BY ID AND NAME)

                        cacheManager.CacheObject ("Application." + session.EnvironmentId + ".Workflow." + workflow.Id.ToString (), workflow, CacheExpirationData);

                        cacheManager.CacheObject ("Application." + session.EnvironmentId + ".Workflow." + workflow.Name, workflow, CacheExpirationData);

                    }

                    if (workflows.Count > 0) {

                        // CACHE THE AVAILABILITY LIST

                        cacheManager.CacheObject (cacheKey, workflows, CacheExpirationData);


                        // CACHE THE DICTIONARY THAT WAS CREATED

                        cacheManager.CacheObject ("Application." + session.EnvironmentId + ".Workflow.Dictionary", objectDictionary, CacheExpirationData);
                        
                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workflows;

        }


        public Core.Work.Workflow WorkflowGet (Int64 workflowId, Boolean useCaching) {

            if (workflowId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".Workflow." + workflowId.ToString ();

            Core.Work.Workflow workflow = null;

            ClearLastException ();


            try {

                workflow = (Core.Work.Workflow)cacheManager.GetObject (cacheKey);

                if (!useCaching) { workflow = null; }

                if (workflow == null) {

                    Server.Application.Workflow serverWorkflow = ApplicationClient.WorkflowGet (session.Token, workflowId);

                    if (serverWorkflow != null) { workflow = new Core.Work.Workflow (this, serverWorkflow); }

                    if (workflow != null) { cacheManager.CacheObject (cacheKey, workflow, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workflow;

        }

        public Core.Work.Workflow WorkflowGet (String workflowName, Boolean useCaching) {

            if (String.IsNullOrWhiteSpace (workflowName)) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".Workflow." + workflowName.ToString ();

            Core.Work.Workflow workflow = null;

            ClearLastException ();


            try {

                workflow = (Core.Work.Workflow)cacheManager.GetObject (cacheKey);

                if (!useCaching) { workflow = null; }

                if (workflow == null) {

                    Server.Application.Workflow serverWorkflow = ApplicationClient.WorkflowGetByName (session.Token, workflowName);

                    if (serverWorkflow != null) { workflow = new Core.Work.Workflow (this, serverWorkflow); }

                    if (workflow != null) { cacheManager.CacheObject (cacheKey, workflow, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workflow;

        }

        public Core.Work.Workflow WorkflowGetByWorkQueueId (Int64 workQueueId, Boolean useCaching) {

            if (workQueueId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".Workflow.ByWorkQueueId." + workQueueId.ToString ();

            Core.Work.Workflow workflow = null;

            ClearLastException ();


            try {

                workflow = (Core.Work.Workflow)cacheManager.GetObject (cacheKey);

                if ((workflow == null) && (useCaching)) {

                    // TRY CHECKING INDIVIDUAL CACHING 

                    Client.Core.Work.WorkQueue workQueue = (Core.Work.WorkQueue)cacheManager.GetObject ("Application." + session.EnvironmentId + ".WorkQueue." + workQueueId);

                    if (workQueue != null) {

                        workflow = (Core.Work.Workflow)cacheManager.GetObject ("Application." + session.EnvironmentId + ".Workflow." + workQueue.WorkflowId);

                        // CACHE WITH SHORTCUT CACHE KEY

                        if (workflow != null) { cacheManager.CacheObject (cacheKey, workflow, CacheExpirationData); }

                    }

                }
                
                if (!useCaching) { workflow = null; }

                if (workflow == null) {

                    Server.Application.Workflow serverWorkflow = ApplicationClient.WorkflowGetByWorkQueueId (session.Token, workQueueId);

                    if (serverWorkflow != null) { workflow = new Core.Work.Workflow (this, serverWorkflow); }

                    if (workflow != null) { cacheManager.CacheObject (cacheKey, workflow, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workflow;

        }


        public Boolean WorkflowSave (Core.Work.Workflow workflow) {

            Server.Application.ObjectSaveResponse response = new Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {
                
                response = ApplicationClient.WorkflowSave (session.Token, (Server.Application.Workflow)workflow.ToServerObject ());

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".Workflow.Available");

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".Workflow.Dictionary");

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".Workflow." + workflow.Id.ToString ());

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".Workflow." + workflow.Name);


                if (!response.HasException) { workflow.SetId (response.Id); }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                response.Success = false;

                SetLastException (applicationException);

            }

            return response.Success;

        }

        #endregion


        #region Work - Workflow Flow Control

        #region Workflow

        private void WorkflowResponseCacheData (Server.Application.WorkflowResponse response) {

            if (response == null) { return; }

            if (response.HasException) { return; }

            if (response.UserInteractionRequest == null) { return; }


            String cacheKey = String.Empty;

            Core.Entity.Entity entity = null;


            switch (response.UserInteractionRequest.InteractionType) {

                case Mercury.Server.Application.UserInteractionType.ContactEntity:

                    Server.Application.WorkflowUserInteractionRequestContactEntity contactResponse;

                    contactResponse = (Mercury.Server.Application.WorkflowUserInteractionRequestContactEntity)response.UserInteractionRequest;

                    if (contactResponse.Entity != null) {

                        cacheKey = "Application." + session.EnvironmentId + ".Core.Entity." + contactResponse.Entity.Id.ToString ();

                        entity = new Mercury.Client.Core.Entity.Entity (this, contactResponse.Entity);

                        cacheManager.CacheObject (cacheKey, entity, CacheExpirationData);


                        //if (contactResponse.EntityContactInformation != null) {

                        //    List<Mercury.Client.Core.EntityContactInformation> entityContacts = new List<Mercury.Client.Core.EntityContactInformation> ();

                        //    foreach (Server.Application.EntityContactInformation currentContact in contactResponse.EntityContactInformation) {

                        //        entityContacts.Add (new Mercury.Client.Core.EntityContactInformation (this, currentContact));

                        //    }

                        //    cacheKey = "Application." + session.EnvironmentId + ".Core.EntityContactInformation.ByEntityId." + contactResponse.Entity.EntityId.ToString ();

                        //    cacheManager.CacheObject (cacheKey, entityContacts, dataCacheExpirationLong);

                        //}

                    }

                    break;

            }

            return;

        }

        public Server.Application.WorkflowResponse WorkflowStart (Server.Application.WorkflowStartRequest startRequest) {

            Server.Application.WorkflowResponse response = new Mercury.Server.Application.WorkflowResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.WorkflowStart (session.Token, startRequest);

                //if (response.HasException) {

                //    SetLastException (new ApplicationException (response.LastException.Message));

                //}

                WorkflowResponseCacheData (response);

            }

            catch (Exception applicationException) {

                response.HasException = true;

                //response.LastException = new Mercury.Server.Application.ServiceException ();

                //response.LastException.Message = applicationException.Message;

                SetLastException (applicationException);

            }

            return response;

        }

        public Server.Application.WorkflowResponse WorkflowContinue (String workflowName, Guid workflowInstanceId, Server.Application.WorkflowUserInteractionResponseBase userInteractionResponse) {

            Server.Application.WorkflowResponse response = new Mercury.Server.Application.WorkflowResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.WorkflowContinue (session.Token, workflowName, workflowInstanceId, userInteractionResponse);

                if (!response.HasException) { WorkflowResponseCacheData (response); }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                response.HasException = true;

                response.Exception = new Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;

                SetLastException (applicationException);

            }

            return response;

        }

        #endregion

        #endregion


        #region Work - Work Outcome

        public List<Core.Work.WorkOutcome> WorkOutcomesAvailable (Boolean useCaching) {

            List<Core.Work.WorkOutcome> workOutcomes = new List<Core.Work.WorkOutcome> ();

            Mercury.Server.Application.WorkOutcomeCollectionResponse response;

            String cacheKey = "Application." + session.EnvironmentId + ".WorkOutcome.Available";


            ClearLastException ();

            try {

                workOutcomes = (List<Core.Work.WorkOutcome>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { workOutcomes = null; }

                if (workOutcomes == null) {

                    workOutcomes = new List<Core.Work.WorkOutcome> ();

                    response = ApplicationClient.WorkOutcomesAvailable (session.Token);

                    if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                    foreach (Server.Application.WorkOutcome currentServerWorkOutcome in response.Collection) {

                        Core.Work.WorkOutcome workOutcome = new Core.Work.WorkOutcome (this, currentServerWorkOutcome);

                        workOutcomes.Add (workOutcome);

                    }

                    if (workOutcomes.Count > 0) { cacheManager.CacheObject (cacheKey, workOutcomes, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }




            return workOutcomes;

        }

        public List<Core.Work.WorkOutcome> WorkOutcomesAvailable (Boolean useCaching, Boolean filterEnabledVisible) {

            List<Core.Work.WorkOutcome> availableWorkOutcomes = WorkOutcomesAvailable (useCaching);

            List<Core.Work.WorkOutcome> filteredWorkOutcomes = new List<Core.Work.WorkOutcome> ();


            if (filterEnabledVisible) {

                foreach (Core.Work.WorkOutcome currentWorkOutcome in availableWorkOutcomes) {

                    if ((currentWorkOutcome.Enabled) && (currentWorkOutcome.Visible)) {

                        filteredWorkOutcomes.Add (currentWorkOutcome);

                    }

                }

            }

            else { filteredWorkOutcomes = availableWorkOutcomes; }


            return filteredWorkOutcomes;

        }


        public Core.Work.WorkOutcome WorkOutcomeGet (Int64 workOutcomeId, Boolean useCaching) {

            if (workOutcomeId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".WorkOutcome." + workOutcomeId.ToString ();

            Core.Work.WorkOutcome workOutcome = null;

            ClearLastException ();


            try {

                workOutcome = (Core.Work.WorkOutcome)cacheManager.GetObject (cacheKey);

                if (!useCaching) { workOutcome = null; }

                if (workOutcome == null) {

                    Server.Application.WorkOutcome serverWorkOutcome = ApplicationClient.WorkOutcomeGet (session.Token, workOutcomeId);

                    if (serverWorkOutcome != null) { workOutcome = new Core.Work.WorkOutcome (this, serverWorkOutcome); }

                    if (workOutcome != null) { cacheManager.CacheObject (cacheKey, workOutcome, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workOutcome;

        }

        public Core.Work.WorkOutcome WorkOutcomeGet (String workOutcomeName, Boolean useCaching) {

            if (String.IsNullOrWhiteSpace (workOutcomeName)) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".WorkOutcome." + workOutcomeName.ToString ();

            Core.Work.WorkOutcome workOutcome = null;

            ClearLastException ();


            try {

                workOutcome = (Core.Work.WorkOutcome)cacheManager.GetObject (cacheKey);

                if (!useCaching) { workOutcome = null; }

                if (workOutcome == null) {

                    Server.Application.WorkOutcome serverWorkOutcome = ApplicationClient.WorkOutcomeGetByName (session.Token, workOutcomeName);

                    if (serverWorkOutcome != null) { workOutcome = new Core.Work.WorkOutcome (this, serverWorkOutcome); }

                    if (workOutcome != null) { cacheManager.CacheObject (cacheKey, workOutcome, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workOutcome;

        }

        public Boolean WorkOutcomeSave (Core.Work.WorkOutcome workOutcome) {
            
            Server.Application.ObjectSaveResponse response = new Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {
               
                response = ApplicationClient.WorkOutcomeSave (session.Token, (Server.Application.WorkOutcome)workOutcome.ToServerObject ());

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".WorkOutcome.Available");

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".WorkOutcome." + workOutcome.Id.ToString ());

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".WorkOutcome." + workOutcome.Name);


                if (!response.HasException) { workOutcome.SetId (response.Id); }

                else { SetLastException (new ApplicationException (response.Exception.Message)); }

            }

            catch (Exception applicationException) {

                response.Success = false;

                SetLastException (applicationException);

            }

            return response.Success;

        }

        #endregion


        #region Work - Work Queue

        public List<Core.Work.WorkQueue> WorkQueuesAvailable (Boolean useCaching) {

            List<Core.Work.WorkQueue> workQueues = new List<Core.Work.WorkQueue> ();

            Mercury.Server.Application.WorkQueueCollectionResponse response;

            String cacheKey = "Application." + session.EnvironmentId + ".WorkQueue.Available";


            ClearLastException ();

            try {

                workQueues = (List<Core.Work.WorkQueue>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { workQueues = null; }

                if (workQueues == null) {

                    workQueues = new List<Core.Work.WorkQueue> ();

                    response = ApplicationClient.WorkQueuesAvailable (session.Token);

                    if (!response.HasException) {

                        Dictionary<Int64, String> workQueueDictionary = new Dictionary<Int64, String> ();

                        foreach (Server.Application.WorkQueue currentServerWorkQueue in response.Collection) {

                            Core.Work.WorkQueue workQueue = new Core.Work.WorkQueue (this, currentServerWorkQueue);

                            workQueues.Add (workQueue);

                            workQueueDictionary.Add (workQueue.Id, workQueue.Name);


                            // CACHE EACH INDIVIDUAL WORK QUEUE LOADED FROM SERVER (BY ID AND NAME)

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".WorkQueue." + workQueue.Id.ToString (), workQueue, CacheExpirationData);

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".WorkQueue." + workQueue.Name, workQueue, CacheExpirationData);

                        }

                        if (workQueues.Count > 0) { 
                            
                            // CACHE THE AVAILABILITY LIST

                            cacheManager.CacheObject (cacheKey, workQueues, CacheExpirationData); 

                            
                            // CACHE THE DICTIONARY THAT WAS CREATED

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".WorkQueue.Dictionary", workQueueDictionary, CacheExpirationData);
                        
                        }

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return workQueues;

        }

        public List<Core.Work.WorkQueue> WorkQueuesAvailable (Boolean useCaching, Boolean filterEnabledVisible) {

            List<Core.Work.WorkQueue> availableWorkQueues = WorkQueuesAvailable (useCaching);

            List<Core.Work.WorkQueue> filteredWorkQueues = new List<Core.Work.WorkQueue> ();


            if (filterEnabledVisible) {

                foreach (Core.Work.WorkQueue currentWorkQueue in availableWorkQueues) {

                    if ((currentWorkQueue.Enabled) && (currentWorkQueue.Visible)) {

                        filteredWorkQueues.Add (currentWorkQueue);

                    }

                }

            }

            else { filteredWorkQueues = availableWorkQueues; }


            return filteredWorkQueues;

        }


        public Core.Work.WorkQueue WorkQueueGet (Int64 workQueueId, Boolean useCaching) {

            if (workQueueId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".WorkQueue." + workQueueId.ToString ();

            Core.Work.WorkQueue workQueue = null;

            ClearLastException ();


            try {

                workQueue = (Core.Work.WorkQueue)cacheManager.GetObject (cacheKey);

                if (!useCaching) { workQueue = null; }

                if (workQueue == null) {

                    Server.Application.WorkQueue serverWorkQueue = ApplicationClient.WorkQueueGet (session.Token, workQueueId);

                    if (serverWorkQueue != null) { workQueue = new Core.Work.WorkQueue (this, serverWorkQueue); }

                    if (workQueue != null) { cacheManager.CacheObject (cacheKey, workQueue, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workQueue;

        }

        public Core.Work.WorkQueue WorkQueueGet (String workQueueName, Boolean useCaching) {

            if (String.IsNullOrWhiteSpace (workQueueName)) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".WorkQueue." + workQueueName.ToString ();

            Core.Work.WorkQueue workQueue = null;

            ClearLastException ();


            try {

                workQueue = (Core.Work.WorkQueue)cacheManager.GetObject (cacheKey);

                if (!useCaching) { workQueue = null; }

                if (workQueue == null) {

                    Server.Application.WorkQueue serverWorkQueue = ApplicationClient.WorkQueueGetByName (session.Token, workQueueName);

                    if (serverWorkQueue != null) { workQueue = new Core.Work.WorkQueue (this, serverWorkQueue); }

                    if (workQueue != null) { cacheManager.CacheObject (cacheKey, workQueue, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workQueue;

        }

        public Core.Work.WorkQueue WorkQueueGetByWorkQueueItem (Int64 workQueueItemId) {

            if (workQueueItemId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".WorkQueue.ByWorkQueueItem." + workQueueItemId.ToString ();

            Core.Work.WorkQueue workQueue = null;

            ClearLastException ();


            try {

                if (workQueue == null) { // LEFT IN IN CASE WE WANT TO CACHE RESULTS LATER (DON'T NEED THE EVALUATION, ALWAYS TRUE AT THIS POINT)

                    Server.Application.WorkQueue serverWorkQueue = ApplicationClient.WorkQueueGetByWorkQueueItem (session.Token, workQueueItemId);

                    if (serverWorkQueue != null) { workQueue = new Core.Work.WorkQueue (this, serverWorkQueue); }

                    // if (workQueue != null) { cacheManager.CacheObject (cacheKey, workQueue, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workQueue;

        }

        public Server.Application.GetWorkResponse WorkQueueGetWork (Int64 workQueueId) {

            Server.Application.GetWorkResponse response = new Server.Application.GetWorkResponse ();


            ClearLastException ();

            try {

                response = ApplicationClient.WorkQueueGetWork (session.Token, workQueueId);

                // DO NOT RE-THROW INBOUND EXCEPTION AS IT MIGHT BE 

                // USER-LEVEL EXCEPTION. PASS BACK TO THE CLIENT FOR PROCESSING

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);


                response.HasException = true;

                response.Exception = new Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;

                response.Exception.Source = applicationException.Source;

            }

            return response;

        }

        public Boolean WorkQueueSave (Core.Work.WorkQueue workQueue) {

            Server.Application.ObjectSaveResponse response = new Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.WorkQueueSave (session.Token, (Server.Application.WorkQueue)workQueue.ToServerObject ());

                InvalidateCache ("WorkQueue", workQueue.Id, workQueue.Name);


                if (!response.HasException) { workQueue.SetId (response.Id); }

                else { SetLastException (new ApplicationException (response.Exception.Message)); }

            }

            catch (Exception applicationException) {

                response.Success = false;

                SetLastException (applicationException);

            }

            return response.Success;

        }

        public Boolean WorkQueueSaveGetWork (Core.Work.WorkQueue workQueue) {

            Server.Application.ObjectSaveResponse response = new Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.WorkQueueSaveGetWork (session.Token, (Server.Application.WorkQueue)workQueue.ToServerObject ());

                InvalidateCache ("WorkQueue", workQueue.Id, workQueue.Name);


                if (!response.HasException) { workQueue.SetId (response.Id); }

                else { SetLastException (new ApplicationException (response.Exception.Message)); }

            }

            catch (Exception applicationException) {

                response.Success = false;

                SetLastException (applicationException);

            }

            return response.Success;

        }

        public Boolean WorkQueueInsertEntity (Int64 workQueueId, Int64 entityId, Core.CoreObject sender, Core.CoreObject eventObject, Int64 eventInstanceId, String eventDescription, Int32 priority) {

            Boolean success = false;

            Server.Application.BooleanResponse response = new Server.Application.BooleanResponse ();

            ClearLastException ();

            try {

                Server.Application.CoreObject serverSender = (sender != null) ? (Server.Application.CoreObject)sender.ToServerObject () : null;

                Server.Application.CoreObject serverEventObject = (eventObject != null) ? (Server.Application.CoreObject)eventObject.ToServerObject () : null;

                response = ApplicationClient.WorkQueueInsertEntity (session.Token, workQueueId, entityId, serverSender, serverEventObject, eventInstanceId, eventDescription, priority);

                if (response.HasException) { SetLastException (response.Exception); }

                else { success = response.Result; }

            }

            catch (Exception applicationException) {

                success = false;

                SetLastException (applicationException);

            }

            return success;

        }

        #endregion


        #region Work - Work Queue Item

        public Core.Work.WorkQueueItem WorkQueueItemGet (Int64 workQueueItemId) {

            if (workQueueItemId == 0) { return null; }


            Core.Work.WorkQueueItem workQueueItem = null;

            ClearLastException ();

            try {

                Server.Application.WorkQueueItem serverWorkQueueItem = ApplicationClient.WorkQueueItemGet (session.Token, workQueueItemId);

                if (serverWorkQueueItem != null) { workQueueItem = new Core.Work.WorkQueueItem (this, serverWorkQueueItem); }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workQueueItem;

        }


        public Boolean WorkQueueItemAssignTo (Int64 workQueueItemId, Int64 securityAuthorityId, String userAccountId, String userAccountName, String userDisplayName, String assignmentSource) {

            Server.Application.BooleanResponse response = new Server.Application.BooleanResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.WorkQueueItemAssignTo (session.Token, workQueueItemId, securityAuthorityId, userAccountId, userAccountName, userDisplayName, assignmentSource);

                if (response.HasException) { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                response.Result = false;

                SetLastException (applicationException);

            }

            return response.Result;

        }

        public Boolean WorkQueueItemMoveToQueue (Int64 workQueueItemId, Int64 workQueueId) {

            Server.Application.BooleanResponse response = new Server.Application.BooleanResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.WorkQueueItemMoveToQueue (session.Token, workQueueItemId, workQueueId);

                if (response.HasException) { SetLastException (response.Exception); }


            }

            catch (Exception applicationException) {

                response.Result = false;

                SetLastException (applicationException);

            }

            return response.Result;

        }

        public Boolean WorkQueueItemClose (Int64 workQueueItemId, Int64 workOutcomeId) {

            Server.Application.BooleanResponse response = new Server.Application.BooleanResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.WorkQueueItemClose (session.Token, workQueueItemId, workOutcomeId);

                if (response.HasException) { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                response.Result = false;

                SetLastException (applicationException);

            }

            return response.Result;

        }

        public Boolean WorkQueueItemSuspend (Int64 workQueueItemId, String lastStep, String nextStep, Int32 constraintDays, Int32 milestoneDays, Boolean releaseItem) {

            Server.Application.BooleanResponse response = new Server.Application.BooleanResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.WorkQueueItemSuspend (session.Token, workQueueItemId, lastStep, nextStep, constraintDays, milestoneDays, releaseItem);

                if (response.HasException) { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                response.Result = false;

                SetLastException (applicationException);

            }

            return response.Result;

        }


        public List<Core.Work.WorkQueueItemSender> WorkQueueItemSendersGet (Int64 workQueueItemId, Boolean useCaching) {

            List<Core.Work.WorkQueueItemSender> workQueueItemSenders = new List<Mercury.Client.Core.Work.WorkQueueItemSender> ();

            Mercury.Server.Application.WorkQueueItemSenderCollectionResponse response;


            // CREATE THE CACHE KEY

            String cacheKey = "Work.WorkQueueItemSenders.ByWorkQueueItemId." + workQueueItemId.ToString ();

            ClearLastException ();

            try {

                workQueueItemSenders = (List<Core.Work.WorkQueueItemSender>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { workQueueItemSenders = null; }

                if (workQueueItemSenders == null) {

                    workQueueItemSenders = new List<Mercury.Client.Core.Work.WorkQueueItemSender> ();


                    response = ApplicationClient.WorkQueueItemSendersGet (session.Token, workQueueItemId);

                    if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                    foreach (Mercury.Server.Application.WorkQueueItemSender currentServerItemSender in response.Collection) {

                        Core.Work.WorkQueueItemSender workQueueItemSender = new Mercury.Client.Core.Work.WorkQueueItemSender (this, currentServerItemSender);

                        workQueueItemSenders.Add (workQueueItemSender);

                    }

                    cacheManager.CacheObject (cacheKey, workQueueItemSenders, CacheExpirationData);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workQueueItemSenders;

        }

        public List<Server.Application.WorkQueueItemAssignmentHistory> WorkQueueItemAssignmentHistoryGet (Int64 workQueueItemId, Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".Core.Work.WorkQueueItem.AssignmentHistory" + workQueueItemId.ToString ();

            Server.Application.WorkQueueItemAssignmentHistoryCollectionResponse response = new Server.Application.WorkQueueItemAssignmentHistoryCollectionResponse ();

            List<Server.Application.WorkQueueItemAssignmentHistory> history = new List<Server.Application.WorkQueueItemAssignmentHistory> ();


            ClearLastException ();

            try {

                history = (List<Server.Application.WorkQueueItemAssignmentHistory>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { history = null; }

                if (history == null) {

                    history = new List<Server.Application.WorkQueueItemAssignmentHistory> ();

                    response = ApplicationClient.WorkQueueItemAssignmentHistoryGet (session.Token, workQueueItemId);

                    if (!response.HasException) { 
                        
                        history.AddRange (response.Collection);

                        cacheManager.CacheObject (cacheKey, history, CacheExpirationData);

                    }

                    else { SetLastException (response.Exception); }
                    
                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return history;

        }

        public List<Server.Application.WorkflowStep> WorkQueueItemWorkflowStepsGet (Int64 workQueueItemId, Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".Core.Work.WorkQueueItem.WorkflowSteps." + workQueueItemId.ToString ();

            Server.Application.WorkQueueItemWorkflowStepCollectionResponse response = new Server.Application.WorkQueueItemWorkflowStepCollectionResponse ();

            List<Server.Application.WorkflowStep> workflowSteps = new List<Server.Application.WorkflowStep> ();


            ClearLastException ();

            try {

                workflowSteps = (List<Server.Application.WorkflowStep>)cacheManager.GetObject (cacheKey);

                if (workflowSteps == null) {

                    workflowSteps = new List<Server.Application.WorkflowStep> ();

                    response = ApplicationClient.WorkQueueItemWorkflowStepsGet (session.Token, workQueueItemId);

                    if (!response.HasException) {

                        workflowSteps.AddRange (response.Collection);

                        cacheManager.CacheObject (cacheKey, workflowSteps, CacheExpirationData);

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workflowSteps;

        }

        #endregion


        #region Work - Work Queue Items (By Views)

        public Int64 WorkQueueItemsGetCount (List<Mercury.Server.Application.DataFilterDescriptor> filters, Boolean useCaching) {

            Int64 itemCount = 0;


            // RESET FILTERS SO THAT NULL COLLECTIONS ARE NOT PASSED TO SERVER

            if (filters == null) { filters = new List<Mercury.Server.Application.DataFilterDescriptor> (); }


            // CREATE THE CACHE KEY

            String cacheKey = "Application." + session.EnvironmentId + ".Core.Work.WorkQueueItems.GetCount.ByFilters";

            foreach (Mercury.Server.Application.DataFilterDescriptor currentFilter in filters) {

                cacheKey = cacheKey + "." + currentFilter.Parameter.Name + "." + currentFilter.Parameter.Value + "." + currentFilter.Operator.ToString ();

                cacheKey = cacheKey + currentFilter.IsCaseSensitive.ToString () + "." + currentFilter.PropertyPath;

            }



            ClearLastException ();

            try {

                Int64.TryParse (Convert.ToString (cacheManager.GetObject (cacheKey)), out itemCount);

                if (!useCaching) { itemCount = 0; }

                if (itemCount == 0) {

                    itemCount = ApplicationClient.WorkQueueItemsGetCount (session.Token, filters.ToArray ());

                    cacheManager.CacheObject (cacheKey, itemCount, CacheExpirationData);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return itemCount;

        }

        public Int64 WorkQueueItemsGetCount (Core.Work.WorkQueueView workQueueView, List<Mercury.Server.Application.DataFilterDescriptor> filters, Boolean useCaching) {

            Int64 itemCount = 0;

            Server.Application.WorkQueueView serverWorkQueueView = (workQueueView != null) ? (Server.Application.WorkQueueView)workQueueView.ToServerObject () : null;


            // RESET FILTERS SO THAT NULL COLLECTIONS ARE NOT PASSED TO SERVER

            if (filters == null) { filters = new List<Mercury.Server.Application.DataFilterDescriptor> (); }


            // CREATE THE CACHE KEY

            String cacheKey = "Application." + session.EnvironmentId + ".Core.Work.WorkQueueItems.GetCount.ByFilters.ByView." + ((workQueueView != null) ? workQueueView.Id : 0).ToString ();

            foreach (Mercury.Server.Application.DataFilterDescriptor currentFilter in filters) {

                cacheKey = cacheKey + "." + currentFilter.Parameter.Name + "." + currentFilter.Parameter.Value + "." + currentFilter.Operator.ToString ();

                cacheKey = cacheKey + currentFilter.IsCaseSensitive.ToString () + "." + currentFilter.PropertyPath;

            }



            ClearLastException ();

            try {

                Int64.TryParse (Convert.ToString (cacheManager.GetObject (cacheKey)), out itemCount);

                if (!useCaching) { itemCount = 0; }

                if (itemCount == 0) {

                    itemCount = ApplicationClient.WorkQueueItemsGetCountByView (session.Token, serverWorkQueueView, filters.ToArray ());

                    cacheManager.CacheObject (cacheKey, itemCount, CacheExpirationData);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return itemCount;

        }

        public List<Core.Work.WorkQueueItem> WorkQueueItemsGetByViewPage (Mercury.Server.Application.WorkQueueView workQueueView, List<Mercury.Server.Application.DataFilterDescriptor> filters, List<Mercury.Server.Application.DataSortDescriptor> sorts, Int32 initialRow, Int32 count, Boolean useCaching) {

            List<Core.Work.WorkQueueItem> workQueueItems = new List<Mercury.Client.Core.Work.WorkQueueItem> ();

            Mercury.Server.Application.WorkQueueItemCollectionResponse response;


            // RESET FILTERS AND SORTS SO THAT NULL COLLECTIONS ARE NOT PASSED TO SERVER

            if (filters == null) { filters = new List<Mercury.Server.Application.DataFilterDescriptor> (); }

            if (sorts == null) { sorts = new List<Mercury.Server.Application.DataSortDescriptor> (); }


            // CREATE THE CACHE KEY

            String cacheKey = "Application." + session.EnvironmentId + ".WorkQueueItemsGetByViewPage." + ((workQueueView != null) ? workQueueView.Id.ToString () : String.Empty) + ".";

            cacheKey = cacheKey + initialRow.ToString () + "." + count.ToString ();

            foreach (Mercury.Server.Application.DataFilterDescriptor currentFilter in filters) {

                cacheKey = cacheKey + "." + currentFilter.Parameter.Name + "." + currentFilter.Parameter.Value + "." + currentFilter.Operator.ToString ();

                cacheKey = cacheKey + currentFilter.IsCaseSensitive.ToString () + "." + currentFilter.PropertyPath;

            }

            foreach (Mercury.Server.Application.DataSortDescriptor currentSort in sorts) {

                cacheKey = cacheKey + "." + currentSort.FieldName + "." + currentSort.SortDirection.ToString ();

            }


            ClearLastException ();

            try {

                workQueueItems = (List<Core.Work.WorkQueueItem>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { workQueueItems = null; }

                if (workQueueItems == null) {

                    workQueueItems = new List<Mercury.Client.Core.Work.WorkQueueItem> ();


                    response = ApplicationClient.WorkQueueItemsGetByViewPage (session.Token, workQueueView, filters.ToArray (), sorts.ToArray (), initialRow, count);

                    if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                    foreach (Mercury.Server.Application.WorkQueueItem currentServerItem in response.Collection) {

                        Core.Work.WorkQueueItem item = new Mercury.Client.Core.Work.WorkQueueItem (this, currentServerItem);

                        workQueueItems.Add (item);

                    }

                    cacheManager.CacheObject (cacheKey, workQueueItems, CacheExpirationData);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workQueueItems;

        }

        public List<Core.Work.WorkQueueItem> WorkQueueItemsGetByViewPage (Core.Work.WorkQueueView workQueueView, List<Mercury.Server.Application.DataFilterDescriptor> filters, List<Mercury.Server.Application.DataSortDescriptor> sorts, Int32 initialRow, Int32 count, Boolean useCaching) {

            if (workQueueView != null) {

                return WorkQueueItemsGetByViewPage ((Mercury.Server.Application.WorkQueueView)workQueueView.ToServerObject (), filters, sorts, initialRow, count, useCaching);

            }

            return WorkQueueItemsGetByViewPage ((Mercury.Server.Application.WorkQueueView)null, filters, sorts, initialRow, count, useCaching);

        }

        #endregion


        #region Work - Work Team

        public List<Core.Work.WorkTeam> WorkTeamsAvailable (Boolean useCaching) {

            List<Core.Work.WorkTeam> workTeams = new List<Core.Work.WorkTeam> ();

            Mercury.Server.Application.WorkTeamCollectionResponse response;

            String cacheKey = "Application." + session.EnvironmentId + ".WorkTeam.Available";


            ClearLastException ();

            try {

                workTeams = (List<Core.Work.WorkTeam>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { workTeams = null; }

                if (workTeams == null) {

                    workTeams = new List<Core.Work.WorkTeam> ();

                    response = ApplicationClient.WorkTeamsAvailable (session.Token);

                    if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                    foreach (Server.Application.WorkTeam currentServerWorkTeam in response.Collection) {

                        Core.Work.WorkTeam workTeam = new Core.Work.WorkTeam (this, currentServerWorkTeam);

                        workTeams.Add (workTeam);

                    }

                    if (workTeams.Count > 0) { cacheManager.CacheObject (cacheKey, workTeams, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }




            return workTeams;

        }


        public List<Core.Work.WorkTeam> WorkTeamsForSession (Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".Session.WorkTeams." + session.Token;

            Mercury.Server.Application.WorkTeamCollectionResponse response = new Mercury.Server.Application.WorkTeamCollectionResponse ();

            List<Core.Work.WorkTeam> workTeams = new List<Mercury.Client.Core.Work.WorkTeam> ();


            ClearLastException ();

            try {

                workTeams = (List<Core.Work.WorkTeam>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { workTeams = null; }

                if (workTeams == null) {

                    workTeams = new List<Mercury.Client.Core.Work.WorkTeam> ();

                    response = ApplicationClient.WorkTeamsForSession (session.Token);

                    if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                    foreach (Mercury.Server.Application.WorkTeam currentWorkTeam in response.Collection) {

                        workTeams.Add (new Mercury.Client.Core.Work.WorkTeam (this, currentWorkTeam));

                    }

                    cacheManager.CacheObject (cacheKey, workTeams, CacheExpirationReference);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workTeams;

        }


        public Core.Work.WorkTeam WorkTeamGet (Int64 workTeamId, Boolean useCaching) {

            if (workTeamId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".WorkTeam." + workTeamId.ToString ();

            Core.Work.WorkTeam workTeam = null;

            ClearLastException ();


            try {

                workTeam = (Core.Work.WorkTeam)cacheManager.GetObject (cacheKey);

                if (!useCaching) { workTeam = null; }

                if (workTeam == null) {

                    Server.Application.WorkTeam serverWorkTeam = ApplicationClient.WorkTeamGet (session.Token, workTeamId);

                    if (serverWorkTeam != null) { workTeam = new Core.Work.WorkTeam (this, serverWorkTeam); }

                    if (workTeam != null) { cacheManager.CacheObject (cacheKey, workTeam, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workTeam;

        }

        public Core.Work.WorkTeam WorkTeamGet (String workTeamName, Boolean useCaching) {

            if (String.IsNullOrWhiteSpace (workTeamName)) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + "." + typeof (Core.Work.WorkTeam).ToString () + "." + workTeamName.ToString ();

            Core.Work.WorkTeam workTeam = null;

            ClearLastException ();


            try {

                workTeam = (Core.Work.WorkTeam)cacheManager.GetObject (cacheKey);

                if (!useCaching) { workTeam = null; }

                if (workTeam == null) {

                    Server.Application.WorkTeam serverWorkTeam = ApplicationClient.WorkTeamGetByName (session.Token, workTeamName);

                    if (serverWorkTeam != null) { workTeam = new Core.Work.WorkTeam (this, serverWorkTeam); }

                    if (workTeam != null) { cacheManager.CacheObject (cacheKey, workTeam, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workTeam;

        }


        public Boolean WorkTeamSave (Core.Work.WorkTeam workTeam) {

            Server.Application.ObjectSaveResponse response = new Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.WorkTeamSave (session.Token, (Server.Application.WorkTeam)workTeam.ToServerObject ());

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".WorkTeam.Available");

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".WorkTeam." + workTeam.Id.ToString ());

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".WorkTeam." + workTeam.Name);


                if (!response.HasException) { workTeam.SetId (response.Id); }

                else { SetLastException (new ApplicationException (response.Exception.Message)); }

            }

            catch (Exception applicationException) {

                response.Success = false;

                SetLastException (applicationException);

            }

            return response.Success;

        }

        #endregion


        #region Work - Work Queue View

        public List<Core.Work.WorkQueueView> WorkQueueViewsAvailable (Boolean useCaching) {

            List<Core.Work.WorkQueueView> workQueueViews = new List<Core.Work.WorkQueueView> ();

            Mercury.Server.Application.WorkQueueViewCollectionResponse response;

            String cacheKey = "Application." + session.EnvironmentId + ".WorkQueueView.Available";


            ClearLastException ();

            try {

                workQueueViews = (List<Core.Work.WorkQueueView>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { workQueueViews = null; }

                if (workQueueViews == null) {

                    workQueueViews = new List<Core.Work.WorkQueueView> ();

                    response = ApplicationClient.WorkQueueViewsAvailable (session.Token);

                    if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                    foreach (Server.Application.WorkQueueView currentServerWorkQueueView in response.Collection) {

                        Core.Work.WorkQueueView workQueueView = new Core.Work.WorkQueueView (this, currentServerWorkQueueView);

                        workQueueViews.Add (workQueueView);

                    }

                    if (workQueueViews.Count > 0) { cacheManager.CacheObject (cacheKey, workQueueViews, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }




            return workQueueViews;

        }

        public List<Core.Work.WorkQueueView> WorkQueueViewsAvailable (Boolean useCaching, Boolean filterEnabledVisible) {

            List<Core.Work.WorkQueueView> availableWorkQueueViews = WorkQueueViewsAvailable (useCaching);

            List<Core.Work.WorkQueueView> filteredWorkQueueViews = new List<Core.Work.WorkQueueView> ();


            if (filterEnabledVisible) {

                foreach (Core.Work.WorkQueueView currentWorkQueueView in availableWorkQueueViews) {

                    if ((currentWorkQueueView.Enabled) && (currentWorkQueueView.Visible)) {

                        filteredWorkQueueViews.Add (currentWorkQueueView);

                    }

                }

            }

            else { filteredWorkQueueViews = availableWorkQueueViews; }


            return filteredWorkQueueViews;

        }


        public Core.Work.WorkQueueView WorkQueueViewGet (Int64 workQueueViewId, Boolean useCaching) {

            if (workQueueViewId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".WorkQueueView." + workQueueViewId.ToString ();

            Core.Work.WorkQueueView workQueueView = null;

            ClearLastException ();


            try {

                workQueueView = (Core.Work.WorkQueueView)cacheManager.GetObject (cacheKey);

                if (!useCaching) { workQueueView = null; }

                if (workQueueView == null) {

                    Server.Application.WorkQueueView serverWorkQueueView = ApplicationClient.WorkQueueViewGet (session.Token, workQueueViewId);

                    if (serverWorkQueueView != null) { workQueueView = new Core.Work.WorkQueueView (this, serverWorkQueueView); }

                    if (workQueueView != null) { cacheManager.CacheObject (cacheKey, workQueueView, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workQueueView;

        }

        public Core.Work.WorkQueueView WorkQueueViewGet (String workQueueViewName, Boolean useCaching) {

            if (String.IsNullOrWhiteSpace (workQueueViewName)) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".WorkQueueView." + workQueueViewName.ToString ();

            Core.Work.WorkQueueView workQueueView = null;

            ClearLastException ();


            try {

                workQueueView = (Core.Work.WorkQueueView)cacheManager.GetObject (cacheKey);

                if (!useCaching) { workQueueView = null; }

                if (workQueueView == null) {

                    Server.Application.WorkQueueView serverWorkQueueView = ApplicationClient.WorkQueueViewGetByName (session.Token, workQueueViewName);

                    if (serverWorkQueueView != null) { workQueueView = new Core.Work.WorkQueueView (this, serverWorkQueueView); }

                    if (workQueueView != null) { cacheManager.CacheObject (cacheKey, workQueueView, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return workQueueView;

        }


        public Boolean WorkQueueViewSave (Core.Work.WorkQueueView workQueueView) {

            Server.Application.ObjectSaveResponse response = new Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.WorkQueueViewSave (session.Token, (Server.Application.WorkQueueView)workQueueView.ToServerObject ());

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".WorkQueueView.Available");

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".WorkQueueView." + workQueueView.Id.ToString ());

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".WorkQueueView." + workQueueView.Name.ToString ());


                if (!response.HasException) { workQueueView.SetId (response.Id); }

                else { SetLastException (new ApplicationException (response.Exception.Message)); }

            }

            catch (Exception applicationException) {

                response.Success = false;

                SetLastException (applicationException);

            }

            return response.Success;

        }


        public Dictionary<String, String> WorkQueueView_ValidateFieldDefinition (Server.Application.WorkQueueView workQueueView, Server.Application.WorkQueueViewFieldDefinition fieldDefinition) {

            Server.Application.DictionaryStringResponse response;

            Dictionary<String, String> validationResponse = new Dictionary<String, String> ();

            ClearLastException ();

            try {

                response = ApplicationClient.WorkQueueView_ValidateFieldDefinition (session.Token, workQueueView, fieldDefinition);


                if (!response.HasException) { validationResponse = response.Dictionary; }

                else { 
                    
                    SetLastException (response.Exception);

                    validationResponse.Add ("Exception", response.Exception.Message);
                
                }

            }

            catch (Exception applicationException) {
                
                validationResponse.Add ("Exception", applicationException.Message);

                SetLastException (applicationException);

            }

            return validationResponse;

        }

        public Dictionary<String, Boolean> WorkQueueView_WellKnownFields (Server.Application.WorkQueueView workQueueView) {

            Dictionary<String, Boolean> wellKnownFields = new Dictionary<String, Boolean> ();

            ClearLastException ();

            try {

                wellKnownFields = ApplicationClient.WorkQueueView_WellKnownFields (session.Token, workQueueView);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return wellKnownFields;

        }

        public Dictionary<String, Boolean> WorkQueueView_WellKnownFields (Core.Work.WorkQueueView workQueueView) {

            return WorkQueueView_WellKnownFields ((Server.Application.WorkQueueView)workQueueView.ToServerObject ());

        }

        #endregion


        #region Work Queue Monitor

        public List<Server.Application.WorkQueueSummary> WorkQueueMonitorSummary () {

            List<Server.Application.WorkQueueSummary> summary = new List<Server.Application.WorkQueueSummary> ();

            Mercury.Server.Application.WorkQueueSummaryCollectionResponse response;

            
            ClearLastException ();

            try {

                response = ApplicationClient.WorkQueueMonitorSummary (session.Token);
                
                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                if (response.Collection != null) { summary.AddRange (response.Collection); }
                
            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return summary;

        }

        public Dictionary<String, Int64> WorkQueueMonitorAging (Int64 workQueueId) {

            Dictionary<String, Int64> aging = new Dictionary<String, Int64> ();

            Mercury.Server.Application.DictionaryKeyCountResponse response;


            ClearLastException ();

            try {

                response = ApplicationClient.WorkQueueMonitorAgingByWorkQueue (session.Token, workQueueId);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                if (response.Dictionary != null) { aging = response.Dictionary; }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return aging;

        }

        public Dictionary<String, Int64> WorkQueueMonitorAgingAvailable (Int64 workQueueId) {

            Dictionary<String, Int64> aging = new Dictionary<String, Int64> ();

            Mercury.Server.Application.DictionaryKeyCountResponse response;


            ClearLastException ();

            try {

                response = ApplicationClient.WorkQueueMonitorAgingAvailableByWorkQueue (session.Token, workQueueId);

                if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                if (response.Dictionary != null) { aging = response.Dictionary; }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return aging;

        }

        #endregion 

        #endregion


        #region Data Explorer

        public List<Core.DataExplorer.DataExplorer> DataExplorersAvailable (Boolean useCaching) {

            List<Core.DataExplorer.DataExplorer> dataExplorers = new List<Core.DataExplorer.DataExplorer> ();

            Mercury.Server.Application.DataExplorerCollectionResponse response;

            String cacheKey = "Application." + session.EnvironmentId + ".DataExplorer.Available";


            ClearLastException ();

            try {

                dataExplorers = (List<Core.DataExplorer.DataExplorer>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { dataExplorers = null; }

                if (dataExplorers == null) {

                    dataExplorers = new List<Core.DataExplorer.DataExplorer> ();

                    Dictionary<Int64, String> objectDictionary = new Dictionary<Int64, String> ();

                    response = ApplicationClient.DataExplorersAvailable (session.Token);

                    if (response.HasException) { throw new ApplicationException (response.Exception.Message); }

                    foreach (Server.Application.DataExplorer currentServerDataExplorer in response.Collection) {

                        Core.DataExplorer.DataExplorer dataExplorer = new Core.DataExplorer.DataExplorer (this, currentServerDataExplorer);

                        dataExplorers.Add (dataExplorer);

                        objectDictionary.Add (dataExplorer.Id, dataExplorer.Name);


                        // CACHE EACH INDIVIDUAL WORK QUEUE LOADED FROM SERVER (BY ID AND NAME)

                        cacheManager.CacheObject ("Application." + session.EnvironmentId + ".DataExplorer." + dataExplorer.Id.ToString (), dataExplorer, CacheExpirationData);

                        cacheManager.CacheObject ("Application." + session.EnvironmentId + ".DataExplorer." + dataExplorer.Name, dataExplorer, CacheExpirationData);

                    }

                    if (dataExplorers.Count > 0) {

                        // CACHE THE AVAILABILITY LIST

                        cacheManager.CacheObject (cacheKey, dataExplorers, CacheExpirationData);


                        // CACHE THE DICTIONARY THAT WAS CREATED

                        cacheManager.CacheObject ("Application." + session.EnvironmentId + ".DataExplorer.Dictionary", objectDictionary, CacheExpirationData);

                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return dataExplorers;

        }

        public Core.DataExplorer.DataExplorer DataExplorerGet (Int64 dataExplorerId, Boolean useCaching) {

            if (dataExplorerId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".DataExplorer." + dataExplorerId.ToString ();

            Core.DataExplorer.DataExplorer dataExplorer = null;

            ClearLastException ();


            try {

                dataExplorer = (Core.DataExplorer.DataExplorer)cacheManager.GetObject (cacheKey);

                if (!useCaching) { dataExplorer = null; }

                if (dataExplorer == null) {

                    Server.Application.DataExplorer serverDataExplorer = ApplicationClient.DataExplorerGet (session.Token, dataExplorerId);

                    if (serverDataExplorer != null) { dataExplorer = new Core.DataExplorer.DataExplorer (this, serverDataExplorer); }

                    if (dataExplorer != null) { cacheManager.CacheObject (cacheKey, dataExplorer, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return dataExplorer;

        }

        public Boolean DataExplorerSave (Core.DataExplorer.DataExplorer dataExplorer) {

            Server.Application.ObjectSaveResponse response = new Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.DataExplorerSave (session.Token, (Server.Application.DataExplorer)dataExplorer.ToServerObject ());

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".DataExplorer.Available");

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".DataExplorer.Dictionary");

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".DataExplorer." + dataExplorer.Id.ToString ());

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".DataExplorer." + dataExplorer.Name);


                if (!response.HasException) { dataExplorer.SetId (response.Id); }

                else { SetLastException (response.Exception); }

            }

            catch (Exception applicationException) {

                response.Success = false;

                SetLastException (applicationException);

            }

            return response.Success;

        }

        public Server.Application.DataExplorerNodeExecutionResponse DataExplorerNodeExecute (Core.DataExplorer.DataExplorer dataExplorer, Guid nodeInstanceId) {

            Server.Application.DataExplorerNodeExecutionResponse response = new Server.Application.DataExplorerNodeExecutionResponse ();

            ClearLastException ();

            try {

                Server.Application.DataExplorer serverDataExplorer = (Server.Application.DataExplorer) dataExplorer.ToServerObject ();

                response = ApplicationClient.DataExplorerNodeExecute (Session.Token, serverDataExplorer, nodeInstanceId);

            }

            catch (Exception applicationException) {

                response.HasException = true;

                response.Exception = new Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;

                SetLastException (applicationException);

            }

            return response;

        }

        public List<Int64> DataExplorerNodeResultsGet (Guid nodeInstanceId, Int32 initialRow, Int32 count) {

            Server.Application.Int64CollectionResponse response = new Server.Application.Int64CollectionResponse ();

            List<Int64> results = new List<Int64> ();


            ClearLastException ();

            try {

                response = ApplicationClient.DataExplorerNodeResultsGet (Session.Token, nodeInstanceId, initialRow, count);

                results.AddRange (response.Collection);

            }

            catch (Exception applicationException) {
                
                SetLastException (applicationException);

            }


            return results;

        }

        public List<Client.Core.Member.Member> DataExplorerNodeResultsGetForMember (Guid nodeInstanceId, Int32 initialRow, Int32 count) {

            Server.Application.MemberCollectionResponse response = new Server.Application.MemberCollectionResponse ();

            List<Client.Core.Member.Member> results = new List<Client.Core.Member.Member> ();


            ClearLastException ();

            try {

                response = ApplicationClient.DataExplorerNodeResultsGetForMember (Session.Token, nodeInstanceId, initialRow, count);

                foreach (Server.Application.Member currentServerMember in response.Collection) {

                    String cacheKey = String.Empty;


                    // CACHE ENTITY THAT WAS RETREIVED 

                    Client.Core.Entity.Entity entity = new Core.Entity.Entity (this, currentServerMember.Entity);

                    cacheKey = "Application." + session.EnvironmentId + ".Entity." + entity.Id.ToString ();

                    cacheManager.CacheObject (cacheKey, entity, CacheExpirationData); 


                    // CACHE MEMBER THAT WAS RETREIVED

                    Client.Core.Member.Member member = new Core.Member.Member (this, currentServerMember);

                    cacheKey = "Application." + session.EnvironmentId + ".Member." + member.Id.ToString ();

                    cacheManager.CacheObject (cacheKey, member, CacheExpirationData); 


                    results.Add (member);


                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return results;

        }

        public List<Client.Core.Entity.EntityAddress> DataExplorerNodeResultsGetForMemberEntityCurrentAddress (Guid nodeInstanceId, Int32 initialRow, Int32 count) {

            Server.Application.EntityAddressCollectionResponse response = new Server.Application.EntityAddressCollectionResponse ();

            List<Client.Core.Entity.EntityAddress> results = new List<Client.Core.Entity.EntityAddress> ();


            ClearLastException ();

            try {

                response = ApplicationClient.DataExplorerNodeResultsGetForMemberEntityCurrentAddress (Session.Token, nodeInstanceId, initialRow, count);

                foreach (Server.Application.EntityAddress currentServerEntityAddress in response.Collection) {

                    String cacheKey = String.Empty;


                    // CACHE ENTITY ADDRESS THAT WAS RETREIVED 

                    Client.Core.Entity.EntityAddress entityAddress = new Core.Entity.EntityAddress (this, currentServerEntityAddress);


                    // CACHE OUT CURRENT ADDRESSES TO MATCH THE GET BY DATE FUNCTION (ABOVE)

                    if ((entityAddress.EffectiveDate <= DateTime.Today) && (entityAddress.TerminationDate >= DateTime.Today)) { // CACHE OUT CURRENT

                        String currentCacheKey = "Application." + session.EnvironmentId + ".EntityAddress.ByEntityId." + entityAddress.EntityId.ToString () + entityAddress.AddressType + DateTime.Today.ToString ("MMddyyyy");

                        cacheManager.CacheObject (currentCacheKey, entityAddress, CacheExpirationData);

                    }

                    results.Add (entityAddress);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return results;

        }

        public List<Client.Core.Entity.EntityContactInformation> DataExplorerNodeResultsGetForMemberEntityCurrentContactInformation (Guid nodeInstanceId, Int32 initialRow, Int32 count) {

            Server.Application.EntityContactInformationCollectionResponse response = new Server.Application.EntityContactInformationCollectionResponse ();

            List<Client.Core.Entity.EntityContactInformation> results = new List<Client.Core.Entity.EntityContactInformation> ();


            ClearLastException ();

            try {

                response = ApplicationClient.DataExplorerNodeResultsGetForMemberEntityCurrentContactInformation (Session.Token, nodeInstanceId, initialRow, count);

                foreach (Server.Application.EntityContactInformation currentServerEntityContactInformation in response.Collection) {

                    String cacheKey = String.Empty;


                    // CACHE ENTITY ADDRESS THAT WAS RETREIVED 

                    Client.Core.Entity.EntityContactInformation entityContactInformation = new Core.Entity.EntityContactInformation (this, currentServerEntityContactInformation);


                    // CACHE OUT CURRENT ADDRESSES TO MATCH THE GET BY DATE FUNCTION (ABOVE)

                    if ((entityContactInformation.EffectiveDate <= DateTime.Today) && (entityContactInformation.TerminationDate >= DateTime.Today)) { // CACHE OUT CURRENT

                        String currentCacheKey = "Application." + session.EnvironmentId + ".EntityContactInformation.ByEntityId." + entityContactInformation.EntityId.ToString () + entityContactInformation.ContactType + DateTime.Today.ToString ("MMddyyyy");

                        cacheManager.CacheObject (currentCacheKey, entityContactInformation, CacheExpirationData);

                    }

                    results.Add (entityContactInformation);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return results;

        }

        public List<Client.Core.Member.MemberEnrollment> DataExplorerNodeResultsGetForMemberCurrentEnrollment (Guid nodeInstanceId, Int32 initialRow, Int32 count) {

            Server.Application.MemberEnrollmentCollectionResponse response = new Server.Application.MemberEnrollmentCollectionResponse ();

            List<Client.Core.Member.MemberEnrollment> results = new List<Client.Core.Member.MemberEnrollment> ();


            ClearLastException ();

            try {

                response = ApplicationClient.DataExplorerNodeResultsGetForMemberCurrentEnrollment (Session.Token, nodeInstanceId, initialRow, count);

                foreach (Server.Application.MemberEnrollment currentServerMemberEnrollment in response.Collection) {

                    Client.Core.Member.MemberEnrollment memberEnrollment = new Core.Member.MemberEnrollment (this, currentServerMemberEnrollment);

                    results.Add (memberEnrollment);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return results;

        }

        public List<Client.Core.Member.MemberEnrollmentCoverage> DataExplorerNodeResultsGetForMemberCurrentEnrollmentCoverage (Guid nodeInstanceId, Int32 initialRow, Int32 count) {

            Server.Application.MemberEnrollmentCoverageCollectionResponse response = new Server.Application.MemberEnrollmentCoverageCollectionResponse ();

            List<Client.Core.Member.MemberEnrollmentCoverage> results = new List<Client.Core.Member.MemberEnrollmentCoverage> ();


            ClearLastException ();

            try {

                response = ApplicationClient.DataExplorerNodeResultsGetForMemberCurrentEnrollmentCoverage (Session.Token, nodeInstanceId, initialRow, count);

                foreach (Server.Application.MemberEnrollmentCoverage currentServerMemberEnrollmentCoverage in response.Collection) {

                    Client.Core.Member.MemberEnrollmentCoverage memberEnrollmentCoverage = new Core.Member.MemberEnrollmentCoverage (this, currentServerMemberEnrollmentCoverage);

                    results.Add (memberEnrollmentCoverage);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return results;

        }

        public List<Client.Core.Member.MemberEnrollmentPcp> DataExplorerNodeResultsGetForMemberCurrentEnrollmentPcp (Guid nodeInstanceId, Int32 initialRow, Int32 count) {

            Server.Application.MemberEnrollmentPcpCollectionResponse response = new Server.Application.MemberEnrollmentPcpCollectionResponse ();

            List<Client.Core.Member.MemberEnrollmentPcp> results = new List<Client.Core.Member.MemberEnrollmentPcp> ();


            ClearLastException ();

            try {

                response = ApplicationClient.DataExplorerNodeResultsGetForMemberCurrentEnrollmentPcp (Session.Token, nodeInstanceId, initialRow, count);

                foreach (Server.Application.MemberEnrollmentPcp currentServerMemberEnrollmentPcp in response.Collection) {

                    Client.Core.Member.MemberEnrollmentPcp memberEnrollmentPcp = new Core.Member.MemberEnrollmentPcp (this, currentServerMemberEnrollmentPcp);

                    results.Add (memberEnrollmentPcp);

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }


            return results;

        }

        #endregion 


        #region Reporting - Reporting Servers

        public List<Reporting.ReportingServer> ReportingServersAvailable (Boolean useCaching) {

            List<Reporting.ReportingServer> reportingServers = new List<Reporting.ReportingServer> ();

            Mercury.Server.Application.ReportingServerCollectionResponse response;

            String cacheKey = "Application." + session.EnvironmentId + ".ReportingServer.Available";

            Dictionary<Int64, String> coreObjectDictionary = new Dictionary<Int64, String> ();


            ClearLastException ();

            try {

                reportingServers = (List<Reporting.ReportingServer>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { reportingServers = null; }

                if (reportingServers == null) {

                    reportingServers = new List<Reporting.ReportingServer> ();

                    response = ApplicationClient.ReportingServersAvailable (session.Token);

                    if (!response.HasException) {

                        foreach (Server.Application.ReportingServer currentServerReportingServer in response.Collection) {

                            Reporting.ReportingServer reportingServer = new Reporting.ReportingServer (this, currentServerReportingServer);

                            reportingServers.Add (reportingServer);


                            // CACHE EACH INDIVIDUAL WORK QUEUE LOADED FROM SERVER (BY ID AND NAME)

                            coreObjectDictionary.Add (reportingServer.Id, reportingServer.Name);

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".ReportingServer." + reportingServer.Id.ToString (), reportingServer, CacheExpirationData);

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".ReportingServer." + reportingServer.Name, reportingServer, CacheExpirationData);

                        }

                        if (reportingServers.Count > 0) {

                            cacheManager.CacheObject (cacheKey, reportingServers, CacheExpirationData);

                            // CACHE THE AVAILABILITY LIST

                            cacheManager.CacheObject (cacheKey, reportingServers, CacheExpirationData);

                            // CACHE THE DICTIONARY THAT WAS CREATED

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".WorkQueue.Dictionary", coreObjectDictionary, CacheExpirationData);

                        }

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }




            return reportingServers;

        }


        public Reporting.ReportingServer ReportingServerGet (Int64 reportingServerId, Boolean useCaching) {

            if (reportingServerId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + "." + typeof (Reporting.ReportingServer).ToString () + "." + reportingServerId.ToString ();

            Reporting.ReportingServer reportingServer = null;

            ClearLastException ();


            try {

                reportingServer = (Reporting.ReportingServer)cacheManager.GetObject (cacheKey);

                if (!useCaching) { reportingServer = null; }

                if (reportingServer == null) {

                    Server.Application.ReportingServer serverReportingServer = ApplicationClient.ReportingServerGet (session.Token, reportingServerId);

                    if (serverReportingServer != null) { reportingServer = new Reporting.ReportingServer (this, serverReportingServer); }

                    if (reportingServer != null) { cacheManager.CacheObject (cacheKey, reportingServer, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return reportingServer;

        }

        public Reporting.ReportingServer ReportingServerGet (String reportingServerName, Boolean useCaching) {

            if (String.IsNullOrWhiteSpace (reportingServerName)) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + "." + typeof (Reporting.ReportingServer).ToString () + "." + reportingServerName.ToString ();

            Reporting.ReportingServer reportingServer = null;

            ClearLastException ();


            try {

                reportingServer = (Reporting.ReportingServer)cacheManager.GetObject (cacheKey);

                if (!useCaching) { reportingServer = null; }

                if (reportingServer == null) {

                    Server.Application.ReportingServer serverReportingServer = ApplicationClient.ReportingServerGetByName (session.Token, reportingServerName);

                    if (serverReportingServer != null) { reportingServer = new Reporting.ReportingServer (this, serverReportingServer); }

                    if (reportingServer != null) { cacheManager.CacheObject (cacheKey, reportingServer, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return reportingServer;

        }

        public Boolean ReportingServerSave (Reporting.ReportingServer reportingServer) {

            String cacheKey = "Application." + session.EnvironmentId + "." + typeof (Reporting.ReportingServer).ToString () + "." + reportingServer.Id.ToString ();

            Server.Application.ObjectSaveResponse response = new Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                cacheManager.RemoveObject (cacheKey);

                response = ApplicationClient.ReportingServerSave (session.Token, (Server.Application.ReportingServer)reportingServer.ToServerObject ());


                // REMOVE CACHING FOR ALL RELATED OBJECTS

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".ReportingServer.Available");

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".ReportingServer.Dictionary");

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".ReportingServer." + reportingServer.Id.ToString ());

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".ReportingServer." + reportingServer.Name);


                if (!response.HasException) { reportingServer.SetId (response.Id); }

                else { SetLastException (new ApplicationException (response.Exception.Message)); }

            }

            catch (Exception applicationException) {

                response.Success = false;

                SetLastException (applicationException);

            }

            return response.Success;

        }

        #endregion


        #region Faxing

        public List<Faxing.FaxServer> FaxServersAvailable (Boolean useCaching) {

            List<Faxing.FaxServer> faxServers = new List<Faxing.FaxServer> ();

            Mercury.Server.Application.FaxServerCollectionResponse response;

            String cacheKey = "Application." + session.EnvironmentId + ".FaxServer.Available";

            Dictionary<Int64, String> coreObjectDictionary = new Dictionary<Int64, String> ();


            ClearLastException ();

            try {

                faxServers = (List<Faxing.FaxServer>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { faxServers = null; }

                if (faxServers == null) {

                    faxServers = new List<Faxing.FaxServer> ();

                    response = ApplicationClient.FaxServersAvailable (session.Token);

                    if (!response.HasException) {

                        foreach (Server.Application.FaxServer currentServerFaxServer in response.Collection) {

                            Faxing.FaxServer faxServer = new Faxing.FaxServer (this, currentServerFaxServer);

                            faxServers.Add (faxServer);


                            // CACHE EACH INDIVIDUAL WORK QUEUE LOADED FROM SERVER (BY ID AND NAME)

                            coreObjectDictionary.Add (faxServer.Id, faxServer.Name);

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".FaxServer." + faxServer.Id.ToString (), faxServer, CacheExpirationData);

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".FaxServer." + faxServer.Name, faxServer, CacheExpirationData);

                        }

                        if (faxServers.Count > 0) {

                            cacheManager.CacheObject (cacheKey, faxServers, CacheExpirationData);

                            // CACHE THE AVAILABILITY LIST

                            cacheManager.CacheObject (cacheKey, faxServers, CacheExpirationData);

                            // CACHE THE DICTIONARY THAT WAS CREATED

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".WorkQueue.Dictionary", coreObjectDictionary, CacheExpirationData);

                        }

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }




            return faxServers;

        }


        public Faxing.FaxServer FaxServerGet (Int64 faxServerId, Boolean useCaching) {

            if (faxServerId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + "." + typeof (Faxing.FaxServer).ToString () + "." + faxServerId.ToString ();

            Faxing.FaxServer faxServer = null;

            ClearLastException ();


            try {

                faxServer = (Faxing.FaxServer)cacheManager.GetObject (cacheKey);

                if (!useCaching) { faxServer = null; }

                if (faxServer == null) {

                    Server.Application.FaxServer serverFaxServer = ApplicationClient.FaxServerGet (session.Token, faxServerId);

                    if (serverFaxServer != null) { faxServer = new Faxing.FaxServer (this, serverFaxServer); }

                    if (faxServer != null) { cacheManager.CacheObject (cacheKey, faxServer, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return faxServer;

        }

        public Faxing.FaxServer FaxServerGet (String faxServerName, Boolean useCaching) {

            if (String.IsNullOrWhiteSpace (faxServerName)) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + "." + typeof (Faxing.FaxServer).ToString () + "." + faxServerName.ToString ();

            Faxing.FaxServer faxServer = null;

            ClearLastException ();


            try {

                faxServer = (Faxing.FaxServer)cacheManager.GetObject (cacheKey);

                if (!useCaching) { faxServer = null; }

                if (faxServer == null) {

                    Server.Application.FaxServer serverFaxServer = ApplicationClient.FaxServerGetByName (session.Token, faxServerName);

                    if (serverFaxServer != null) { faxServer = new Faxing.FaxServer (this, serverFaxServer); }

                    if (faxServer != null) { cacheManager.CacheObject (cacheKey, faxServer, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return faxServer;

        }

        public Boolean FaxServerSave (Faxing.FaxServer faxServer) {

            String cacheKey = "Application." + session.EnvironmentId + "." + typeof (Faxing.FaxServer).ToString () + "." + faxServer.Id.ToString ();

            Server.Application.ObjectSaveResponse response = new Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                cacheManager.RemoveObject (cacheKey);

                response = ApplicationClient.FaxServerSave (session.Token, (Server.Application.FaxServer)faxServer.ToServerObject ());


                // REMOVE CACHING FOR ALL RELATED OBJECTS

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".FaxServer.Available");

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".FaxServer.Dictionary");

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".FaxServer." + faxServer.Id.ToString ());

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".FaxServer." + faxServer.Name);


                if (!response.HasException) { faxServer.SetId (response.Id); }

                else { SetLastException (new ApplicationException (response.Exception.Message)); }

            }

            catch (Exception applicationException) {

                response.Success = false;

                SetLastException (applicationException);

            }

            return response.Success;

        }

        #endregion


        #region Printing

        public List<Printing.Printer> PrintersAvailable (Boolean useCaching) {

            List<Printing.Printer> printers = new List<Printing.Printer> ();

            Mercury.Server.Application.PrinterCollectionResponse response;

            String cacheKey = "Application." + session.EnvironmentId + ".Printer.Available";

            Dictionary<Int64, String> coreObjectDictionary = new Dictionary<Int64, String> ();


            ClearLastException ();

            try {

                printers = (List<Printing.Printer>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { printers = null; }

                if (printers == null) {

                    printers = new List<Printing.Printer> ();

                    response = ApplicationClient.PrintersAvailable (session.Token);

                    if (!response.HasException) {

                        foreach (Server.Application.Printer currentServerPrinter in response.Collection) {

                            Printing.Printer printer = new Printing.Printer (this, currentServerPrinter);

                            printers.Add (printer);


                            // CACHE EACH INDIVIDUAL WORK QUEUE LOADED FROM SERVER (BY ID AND NAME)

                            coreObjectDictionary.Add (printer.Id, printer.Name);

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".Printer." + printer.Id.ToString (), printer, CacheExpirationData);

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".Printer." + printer.Name, printer, CacheExpirationData);

                        }

                        if (printers.Count > 0) {

                            cacheManager.CacheObject (cacheKey, printers, CacheExpirationData);

                            // CACHE THE AVAILABILITY LIST

                            cacheManager.CacheObject (cacheKey, printers, CacheExpirationData);

                            // CACHE THE DICTIONARY THAT WAS CREATED

                            cacheManager.CacheObject ("Application." + session.EnvironmentId + ".WorkQueue.Dictionary", coreObjectDictionary, CacheExpirationData);

                        }

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }




            return printers;

        }

        public Dictionary<String, String> PrintQueuesAvailable (String printServerName, Boolean useCaching) {

            Dictionary<String, String> printQueues = new Dictionary<String, String> ();

            Mercury.Server.Application.DictionaryStringResponse response;

            String cacheKey = "Application." + session.EnvironmentId + ".PrinterQueues.Available." + printServerName;

            
            ClearLastException ();

            try {

                printQueues = (Dictionary<String, String>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { printQueues = null; }

                if (printQueues == null) {

                    printQueues = new Dictionary<String, String> ();

                    response = ApplicationClient.PrintQueuesAvailable (session.Token, printServerName);

                    if (!response.HasException) {

                        printQueues = response.Dictionary;

                        if (printQueues.Count > 0) {

                            cacheManager.CacheObject (cacheKey, printQueues, CacheExpirationData);

                        }

                    }

                    else { SetLastException (response.Exception); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return printQueues;

        }


        public Printing.Printer PrinterGet (Int64 printerId, Boolean useCaching) {

            if (printerId == 0) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + "." + typeof (Printing.Printer).ToString () + "." + printerId.ToString ();

            Printing.Printer printer = null;

            ClearLastException ();


            try {

                printer = (Printing.Printer)cacheManager.GetObject (cacheKey);

                if (!useCaching) { printer = null; }

                if (printer == null) {

                    Server.Application.Printer serverPrinter = ApplicationClient.PrinterGet (session.Token, printerId);

                    if (serverPrinter != null) { printer = new Printing.Printer (this, serverPrinter); }

                    if (printer != null) { cacheManager.CacheObject (cacheKey, printer, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return printer;

        }

        public Printing.Printer PrinterGet (String printerName, Boolean useCaching) {

            if (String.IsNullOrWhiteSpace (printerName)) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + "." + typeof (Printing.Printer).ToString () + "." + printerName.ToString ();

            Printing.Printer printer = null;

            ClearLastException ();


            try {

                printer = (Printing.Printer)cacheManager.GetObject (cacheKey);

                if (!useCaching) { printer = null; }

                if (printer == null) {

                    Server.Application.Printer serverPrinter = ApplicationClient.PrinterGetByName (session.Token, printerName);

                    if (serverPrinter != null) { printer = new Printing.Printer (this, serverPrinter); }

                    if (printer != null) { cacheManager.CacheObject (cacheKey, printer, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return printer;

        }

        public Server.Application.PrinterCapabilities PrinterCapabilitiesGet (String printServerName, String printQueueName, Boolean useCaching) {

            if ((String.IsNullOrWhiteSpace (printServerName)) || (String.IsNullOrWhiteSpace (printQueueName))) { return null; }


            String cacheKey = "Application." + session.EnvironmentId + ".PrinterCapabilities." + printServerName + "." + printQueueName;

            Server.Application.PrinterCapabilities capabilities = null;

            ClearLastException ();


            try {

                capabilities = (Server.Application.PrinterCapabilities)cacheManager.GetObject (cacheKey);

                if (!useCaching) { capabilities = null; }

                if (capabilities == null) {

                    capabilities = ApplicationClient.PrinterCapabilitiesGet (session.Token, printServerName, printQueueName);

                    if (capabilities != null) { cacheManager.CacheObject (cacheKey, capabilities, CacheExpirationData); }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return capabilities;

        }

        public Boolean PrinterSave (Printing.Printer printer) {

            String cacheKey = "Application." + session.EnvironmentId + ".Printer." + printer.Id.ToString ();

            Server.Application.ObjectSaveResponse response = new Server.Application.ObjectSaveResponse ();

            ClearLastException ();

            try {

                cacheManager.RemoveObject (cacheKey);

                response = ApplicationClient.PrinterSave (session.Token, (Server.Application.Printer)printer.ToServerObject ());


                // REMOVE CACHING FOR ALL RELATED OBJECTS

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".Printer.Available");

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".Printer.Dictionary");

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".Printer." + printer.Id.ToString ());

                cacheManager.RemoveObject ("Application." + session.EnvironmentId + ".Printer." + printer.Name);


                if (!response.HasException) { printer.SetId (response.Id); }

                else { SetLastException (new ApplicationException (response.Exception.Message)); }

            }

            catch (Exception applicationException) {

                response.Success = false;

                SetLastException (applicationException);

            }

            return response.Success;

        }

        #endregion


        #region Searches

        public List<Mercury.Server.Application.SearchResultMember> SearchMemberByName (String name, DateTime? birthDate) {

            List<Mercury.Server.Application.SearchResultMember> results = new List<Server.Application.SearchResultMember> ();

            Mercury.Server.Application.SearchResultsMemberResponse response;

            ClearLastException ();

            try {

                if (name.Length >= 3) {

                    response = ApplicationClient.SearchMemberByName (session.Token, name, birthDate);

                    if (!response.HasException) {

                        for (Int32 currentResult = 0; currentResult < response.Results.Length; currentResult++) {

                            results.Add (response.Results[currentResult]);

                        }

                    }

                    else {

                        // LET THE CALLER HANDLE THE EXCEPTION, DO NOT RECORD IN LOGS

                        SetLastExceptionQuite (response.Exception);

                    }

                }

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return results;

        }

        public Mercury.Server.Application.SearchResultsMemberResponse SearchMember (String name, DateTime? birthDate, String id) {

            Mercury.Server.Application.SearchResultsMemberResponse response = new Server.Application.SearchResultsMemberResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.SearchMember (session.Token, name, birthDate, id);

            }

            catch (Exception applicationException) {

                response.HasException = true;

                response.Exception = new Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;

                SetLastException (applicationException);

            }

            return response;

        }

        public List<Mercury.Server.Application.SearchResultMember> SearchMemberById (String id) {

            List<Mercury.Server.Application.SearchResultMember> results = new List<Mercury.Server.Application.SearchResultMember> ();

            results.AddRange (SearchMember (String.Empty, null, id).Results);

            return results;

        }


        public Mercury.Server.Application.SearchResultsProviderResponse SearchProvider (String name, String id) {

            Mercury.Server.Application.SearchResultsProviderResponse response = new Mercury.Server.Application.SearchResultsProviderResponse ();

            ClearLastException ();

            try {

                response = ApplicationClient.SearchProvider (session.Token, name, id);

            }

            catch (Exception applicationException) {

                response.HasException = true;

                response.Exception = new Server.Application.ServiceException ();

                response.Exception.Message = applicationException.Message;

                SetLastException (applicationException);

            }

            return response;

        }

        public List<Mercury.Server.Application.SearchResultProvider> SearchProviderByName (String name, Boolean useCaching) {

            String cacheKey = "Application." + session.EnvironmentId + ".Core.Provider.Search." + name;
            
            List<Mercury.Server.Application.SearchResultProvider> results = new List<Server.Application.SearchResultProvider> ();

            Mercury.Server.Application.SearchResultsProviderResponse response;

            ClearLastException ();

            if (name.Length < 3) { return results; }

            try {

                results = null;

                results = (List<Mercury.Server.Application.SearchResultProvider>)cacheManager.GetObject (cacheKey);

                if (!useCaching) { results = null; }


                if (results == null) {

                    results = new List<Mercury.Server.Application.SearchResultProvider> ();

                    response = ApplicationClient.SearchProviderByName (session.Token, name);

                    if (!response.HasException) {

                        results.AddRange (response.Results);

                        cacheManager.CacheObject (cacheKey, results, CacheExpirationData);

                    }

                    else {

                        // LET THE CALLER HANDLE THE EXCEPTION, DO NOT RECORD IN LOGS

                        SetLastExceptionQuite (response.Exception);

                    }

                }

            }

            catch (Exception applicationException) {

                results = new List<Mercury.Server.Application.SearchResultProvider> ();

                SetLastException (applicationException);

            }

            return results;

        }

        #endregion


        #region Session Work Queue Permissions

        public Boolean SessionWorkQueueHasManagePermission (Int64 workQueueId) {

            Boolean hasPermission = false;

            if (Session.WorkQueuePermissions.ContainsKey (workQueueId)) {

                hasPermission = (Session.WorkQueuePermissions[workQueueId] == Server.Application.WorkQueueTeamPermission.Manage);

            }

            return hasPermission;

        }

        public Boolean SessionWorkQueueHasSelfAssignPermission (Int64 workQueueId) {

            Boolean hasPermission = false;

            if (Session.WorkQueuePermissions.ContainsKey (workQueueId)) {

                hasPermission = hasPermission || (Session.WorkQueuePermissions[workQueueId] == Server.Application.WorkQueueTeamPermission.Manage);

                hasPermission = hasPermission || (Session.WorkQueuePermissions[workQueueId] == Server.Application.WorkQueueTeamPermission.SelfAssign);

            }

            return hasPermission;

        }

        public Boolean SessionWorkQueueHasWorkPermission (Int64 workQueueId) {

            Boolean hasPermission = false;

            if (Session.WorkQueuePermissions.ContainsKey (workQueueId)) {

                hasPermission = hasPermission || (Session.WorkQueuePermissions[workQueueId] == Server.Application.WorkQueueTeamPermission.Manage);

                hasPermission = hasPermission || (Session.WorkQueuePermissions[workQueueId] == Server.Application.WorkQueueTeamPermission.SelfAssign);

                hasPermission = hasPermission || (Session.WorkQueuePermissions[workQueueId] == Server.Application.WorkQueueTeamPermission.Work);

            }

            return hasPermission;

        }

        public Boolean SessionWorkQueueHasViewPermission (Int64 workQueueId) {

            Boolean hasPermission = false;

            if (Session.WorkQueuePermissions.ContainsKey (workQueueId)) {

                hasPermission = hasPermission || (Session.WorkQueuePermissions[workQueueId] == Server.Application.WorkQueueTeamPermission.Manage);

                hasPermission = hasPermission || (Session.WorkQueuePermissions[workQueueId] == Server.Application.WorkQueueTeamPermission.SelfAssign);

                hasPermission = hasPermission || (Session.WorkQueuePermissions[workQueueId] == Server.Application.WorkQueueTeamPermission.Work);

                hasPermission = hasPermission || (Session.WorkQueuePermissions[workQueueId] == Server.Application.WorkQueueTeamPermission.View);

            }

            return hasPermission;

        }

        #endregion 


        #region Object Factory

        public Mercury.Server.Application.DataFilterDescriptor CreateFilterDescriptor (String forPropertyPath, Mercury.Server.Application.DataFilterOperator forFilterOperator, Object forFilterValue) {

            Mercury.Server.Application.DataFilterDescriptor filter = new Mercury.Server.Application.DataFilterDescriptor ();

            filter.PropertyPath = forPropertyPath;

            filter.Operator = forFilterOperator;

            filter.Parameter = new Server.Application.DataContract ();

            filter.Parameter.Name = "Value";

            filter.Parameter.Value = forFilterValue;

            return filter;

        }

        public Mercury.Server.Application.DataSortDescriptor CreateSortDescription (String forFieldName, Mercury.Server.Application.DataSortDirection forSortDireciton) {

            Mercury.Server.Application.DataSortDescriptor sort = new Server.Application.DataSortDescriptor ();

            sort.FieldName = forFieldName;

            sort.SortDirection = forSortDireciton;

            return sort;

        }

        public Mercury.Server.Application.WebServiceHostConfiguration CreateWebServiceHostConfiguration () {

            Mercury.Server.Application.WebServiceHostConfiguration webServiceHostConfiguration = new Server.Application.WebServiceHostConfiguration ();


            webServiceHostConfiguration.BindingConfiguration = new Server.Application.WebServiceHostBindingConfiguration ();

            webServiceHostConfiguration.ClientCredentials = new Server.Application.WebServiceHostClientCredentials ();


            // BUFFER

            webServiceHostConfiguration.BindingConfiguration.BufferSizeMaximum = Int32.MaxValue;

            webServiceHostConfiguration.BindingConfiguration.BufferPoolSizeMaximum = Int32.MaxValue;

            webServiceHostConfiguration.BindingConfiguration.ReceivedMessageSizeMaximum = Int32.MaxValue;


            // READER QUOTAS 

            webServiceHostConfiguration.BindingConfiguration.ReaderQuotasDepthMaximum = Int32.MaxValue;

            webServiceHostConfiguration.BindingConfiguration.ReaderQuotasStringContentLengthMaximum = Int32.MaxValue;

            webServiceHostConfiguration.BindingConfiguration.ReaderQuotasArrayLengthMaximum = Int32.MaxValue;

            webServiceHostConfiguration.BindingConfiguration.ReaderQuotasNameTableCharCountMaximum = Int32.MaxValue;

            webServiceHostConfiguration.BindingConfiguration.ReaderQuotasBytesPerReadMaximum = Int32.MaxValue;


            return webServiceHostConfiguration;

        }


        public Server.Application.ActionParameter CopyActionParameter (Server.Application.ActionParameter sourceParameter) {

            Server.Application.ActionParameter copiedParameter = new Server.Application.ActionParameter ();

            copiedParameter.ParameterName = sourceParameter.ParameterName;

            copiedParameter.DataType = sourceParameter.DataType;

            copiedParameter.AllowFixedValue = sourceParameter.AllowFixedValue;

            copiedParameter.Required = sourceParameter.Required;

            copiedParameter.ValueType = sourceParameter.ValueType;

            copiedParameter.Value = sourceParameter.Value;

            copiedParameter.ValueDescription = sourceParameter.ValueDescription;

            return copiedParameter;

        }

        public Server.Application.AuthorityAccountStamp CopyAuthorityAccountStamp (Server.Application.AuthorityAccountStamp sourceAccountStamp) {

            Server.Application.AuthorityAccountStamp copiedAccountStamp = new Server.Application.AuthorityAccountStamp ();


            if (sourceAccountStamp != null) {

                copiedAccountStamp.SecurityAuthorityName = sourceAccountStamp.SecurityAuthorityName;

                copiedAccountStamp.UserAccountId = sourceAccountStamp.UserAccountId;

                copiedAccountStamp.UserAccountName = sourceAccountStamp.UserAccountName;

                copiedAccountStamp.ActionDate = sourceAccountStamp.ActionDate;

            }

            else { // THIS OCCURS IF THE SOURCE OBJECT IS A LOCALLY CREATE SERVER.APPLICATION OBJECT (THE PROPERTIES ARE NULL)
                
                copiedAccountStamp.SecurityAuthorityName = session.SecurityAuthorityName;

                copiedAccountStamp.UserAccountId = session.UserAccountId;

                copiedAccountStamp.UserAccountName = session.UserAccountName;

                copiedAccountStamp.ActionDate = DateTime.Now;

            }

            return copiedAccountStamp;

        }

        public Server.Application.WorkflowPermission CopyWorkflowPermission (Server.Application.WorkflowPermission sourceWorkflowPermission) {

            Server.Application.WorkflowPermission copiedPermission = new Server.Application.WorkflowPermission ();


            copiedPermission.Id = sourceWorkflowPermission.Id;

            copiedPermission.Name = sourceWorkflowPermission.Name;

            copiedPermission.Description = sourceWorkflowPermission.Description;


            copiedPermission.WorkflowId = sourceWorkflowPermission.WorkflowId;

            copiedPermission.WorkTeamId = sourceWorkflowPermission.WorkTeamId;

            copiedPermission.IsGranted = sourceWorkflowPermission.IsGranted;

            copiedPermission.IsDenied = sourceWorkflowPermission.IsDenied;


            copiedPermission.CreateAccountInfo = CopyAuthorityAccountStamp (sourceWorkflowPermission.CreateAccountInfo);

            copiedPermission.ModifiedAccountInfo = CopyAuthorityAccountStamp (sourceWorkflowPermission.ModifiedAccountInfo);


            return copiedPermission;

        }

        public Server.Application.WorkQueueGetWorkUserView CopyWorkQueueGetWorkUserView (Server.Application.WorkQueueGetWorkUserView sourceGetWorkUserView) {

            // MAKE COPY OF COLLECTION, NOT DIRECT ASSIGNMENT

            Server.Application.WorkQueueGetWorkUserView copiedGetWorkUserView = new Server.Application.WorkQueueGetWorkUserView ();

            copiedGetWorkUserView.Id = sourceGetWorkUserView.Id;

            copiedGetWorkUserView.Name = sourceGetWorkUserView.Name;

            copiedGetWorkUserView.Description = sourceGetWorkUserView.Description;


            copiedGetWorkUserView.WorkQueueId = sourceGetWorkUserView.WorkQueueId;

            copiedGetWorkUserView.SecurityAuthorityId = sourceGetWorkUserView.SecurityAuthorityId;

            copiedGetWorkUserView.SecurityAuthorityName = sourceGetWorkUserView.SecurityAuthorityName;

            copiedGetWorkUserView.UserAccountId = sourceGetWorkUserView.UserAccountId;

            copiedGetWorkUserView.UserAccountName = sourceGetWorkUserView.UserAccountName;

            copiedGetWorkUserView.UserDisplayName = sourceGetWorkUserView.UserDisplayName;

            copiedGetWorkUserView.WorkQueueViewId = sourceGetWorkUserView.WorkQueueViewId;


            return copiedGetWorkUserView;

        }

        public Server.Application.WorkQueueTeam CopyWorkQueueTeam (Server.Application.WorkQueueTeam sourceWorkQueueTeam) {

            // MAKE COPY OF COLLECTION, NOT DIRECT ASSIGNMENT

            Server.Application.WorkQueueTeam copiedWorkQueueTeam = new Server.Application.WorkQueueTeam ();

            copiedWorkQueueTeam.Id = sourceWorkQueueTeam.Id;

            copiedWorkQueueTeam.Name = sourceWorkQueueTeam.Name;

            copiedWorkQueueTeam.Description = sourceWorkQueueTeam.Description;


            copiedWorkQueueTeam.CreateAccountInfo = CopyAuthorityAccountStamp (sourceWorkQueueTeam.CreateAccountInfo);

            copiedWorkQueueTeam.ModifiedAccountInfo = CopyAuthorityAccountStamp (sourceWorkQueueTeam.ModifiedAccountInfo);


            copiedWorkQueueTeam.Permission = sourceWorkQueueTeam.Permission;

            copiedWorkQueueTeam.WorkQueueId = sourceWorkQueueTeam.WorkQueueId;

            copiedWorkQueueTeam.WorkTeamId = sourceWorkQueueTeam.WorkTeamId;

            copiedWorkQueueTeam.WorkTeamName = sourceWorkQueueTeam.WorkTeamName;


            return copiedWorkQueueTeam;

        }

        public Server.Application.WorkTeamMembership CopyWorkTeamMembership (Server.Application.WorkTeamMembership sourceMembership) {

            // MAKE COPY OF COLLECTION, NOT DIRECT ASSIGNMENT

            Server.Application.WorkTeamMembership copiedMembership = new Server.Application.WorkTeamMembership ();

            copiedMembership.Id = sourceMembership.Id;

            copiedMembership.Name = sourceMembership.Name;

            copiedMembership.Description = sourceMembership.Description;


            copiedMembership.WorkTeamId = sourceMembership.WorkTeamId;

            copiedMembership.SecurityAuthorityId = sourceMembership.SecurityAuthorityId;

            copiedMembership.SecurityAuthorityName = sourceMembership.SecurityAuthorityName;

            copiedMembership.UserAccountId = sourceMembership.UserAccountId;

            copiedMembership.UserAccountName = sourceMembership.UserAccountName;

            copiedMembership.UserDisplayName = sourceMembership.UserDisplayName;

            copiedMembership.WorkTeamRole = sourceMembership.WorkTeamRole;


            return copiedMembership;

        }

        public Server.Application.WorkQueueViewFieldDefinition CopyWorkQueueViewFieldDefinition (Server.Application.WorkQueueViewFieldDefinition sourceFieldDefinition) {

            // MAKE COPY OF COLLECTION, NOT DIRECT ASSIGNMENT

            Server.Application.WorkQueueViewFieldDefinition copiedFieldDefinition = new Server.Application.WorkQueueViewFieldDefinition ();


            copiedFieldDefinition.WorkQueueViewId = sourceFieldDefinition.WorkQueueViewId;

            copiedFieldDefinition.PropertyName = sourceFieldDefinition.PropertyName;

            copiedFieldDefinition.DataType = sourceFieldDefinition.DataType;

            copiedFieldDefinition.DefaultValue = sourceFieldDefinition.DefaultValue;

            copiedFieldDefinition.DisplayName = sourceFieldDefinition.DisplayName;


            return copiedFieldDefinition;

        }

        public Server.Application.WorkQueueViewFilterDefinition CopyWorkQueueViewFilterDefinition (Server.Application.WorkQueueViewFilterDefinition sourceFilterDefinition) {

            // MAKE COPY OF COLLECTION, NOT DIRECT ASSIGNMENT

            Server.Application.WorkQueueViewFilterDefinition copiedFilterDefinition = new Server.Application.WorkQueueViewFilterDefinition ();


            copiedFilterDefinition.WorkQueueViewId = sourceFilterDefinition.WorkQueueViewId;

            copiedFilterDefinition.Sequence = sourceFilterDefinition.Sequence;


            copiedFilterDefinition.IgnoreValue = sourceFilterDefinition.IgnoreValue;

            copiedFilterDefinition.IsCaseSensitive = sourceFilterDefinition.IsCaseSensitive;

            copiedFilterDefinition.Operator = sourceFilterDefinition.Operator;

            copiedFilterDefinition.Parameter = new Server.Application.DataContract ();

            copiedFilterDefinition.Parameter.Name = sourceFilterDefinition.Parameter.Name;

            copiedFilterDefinition.Parameter.Value = sourceFilterDefinition.Parameter.Value;

            copiedFilterDefinition.PropertyPath = sourceFilterDefinition.PropertyPath;


            return copiedFilterDefinition;
        }

        public Server.Application.WorkQueueViewSortDefinition CopyWorkQueueViewSortDefinition (Server.Application.WorkQueueViewSortDefinition sourceSortDefinition) {

            // MAKE COPY OF COLLECTION, NOT DIRECT ASSIGNMENT

            Server.Application.WorkQueueViewSortDefinition copiedSortDefinition = new Server.Application.WorkQueueViewSortDefinition ();


            copiedSortDefinition.WorkQueueViewId = sourceSortDefinition.WorkQueueViewId;

            copiedSortDefinition.Sequence = sourceSortDefinition.Sequence;

            copiedSortDefinition.FieldName = sourceSortDefinition.FieldName;

            copiedSortDefinition.SortDirection= sourceSortDefinition.SortDirection;


            return copiedSortDefinition;
        }

        

        public Server.Application.FaxServerConfiguration CopyFaxServerConfiguration (Server.Application.FaxServerConfiguration sourceConfiguration) {

            Server.Application.FaxServerConfiguration copiedConfiguration = new Server.Application.FaxServerConfiguration ();


            copiedConfiguration.FaxUrl = sourceConfiguration.FaxUrl;

            copiedConfiguration.FaxQueueName = sourceConfiguration.FaxQueueName;

            copiedConfiguration.MonitorInterval = sourceConfiguration.MonitorInterval;

            copiedConfiguration.MonitorTimeout = sourceConfiguration.MonitorTimeout;

            copiedConfiguration.SenderEmailAddress = sourceConfiguration.SenderEmailAddress;


            return copiedConfiguration;

        }

        public Server.Application.WebServiceHostConfiguration CopyWebServiceHostConfiguration (Server.Application.WebServiceHostConfiguration sourceConfiguration) {

            Server.Application.WebServiceHostConfiguration copiedConfiguration = new Server.Application.WebServiceHostConfiguration ();


            copiedConfiguration.Server = sourceConfiguration.Server;

            copiedConfiguration.ServicePath = sourceConfiguration.ServicePath;

            copiedConfiguration.ServiceName = sourceConfiguration.ServiceName;

            copiedConfiguration.Port = sourceConfiguration.Port;


            copiedConfiguration.BindingConfiguration = new Server.Application.WebServiceHostBindingConfiguration ();

            copiedConfiguration.BindingConfiguration.BindingName = sourceConfiguration.BindingConfiguration.BindingName;

            copiedConfiguration.BindingConfiguration.BindingType = sourceConfiguration.BindingConfiguration.BindingType;

            copiedConfiguration.BindingConfiguration.BufferPoolSizeMaximum = sourceConfiguration.BindingConfiguration.BufferPoolSizeMaximum;

            copiedConfiguration.BindingConfiguration.BufferSizeMaximum = sourceConfiguration.BindingConfiguration.BufferSizeMaximum;

            copiedConfiguration.BindingConfiguration.MessageCredentialType = sourceConfiguration.BindingConfiguration.MessageCredentialType;

            copiedConfiguration.BindingConfiguration.ProtectionLevel = sourceConfiguration.BindingConfiguration.ProtectionLevel;

            copiedConfiguration.BindingConfiguration.Protocol = sourceConfiguration.BindingConfiguration.Protocol;

            copiedConfiguration.BindingConfiguration.ReaderQuotasArrayLengthMaximum = sourceConfiguration.BindingConfiguration.ReaderQuotasArrayLengthMaximum;

            copiedConfiguration.BindingConfiguration.ReaderQuotasBytesPerReadMaximum = sourceConfiguration.BindingConfiguration.ReaderQuotasBytesPerReadMaximum;

            copiedConfiguration.BindingConfiguration.ReaderQuotasDepthMaximum = sourceConfiguration.BindingConfiguration.ReaderQuotasDepthMaximum;

            copiedConfiguration.BindingConfiguration.ReaderQuotasNameTableCharCountMaximum = sourceConfiguration.BindingConfiguration.ReaderQuotasNameTableCharCountMaximum;

            copiedConfiguration.BindingConfiguration.ReaderQuotasStringContentLengthMaximum = sourceConfiguration.BindingConfiguration.ReaderQuotasStringContentLengthMaximum;

            copiedConfiguration.BindingConfiguration.ReceivedMessageSizeMaximum = sourceConfiguration.BindingConfiguration.ReceivedMessageSizeMaximum;

            copiedConfiguration.BindingConfiguration.SecurityMode = sourceConfiguration.BindingConfiguration.SecurityMode;

            copiedConfiguration.BindingConfiguration.TimeoutClose = sourceConfiguration.BindingConfiguration.TimeoutClose;

            copiedConfiguration.BindingConfiguration.TimeoutOpen = sourceConfiguration.BindingConfiguration.TimeoutOpen;

            copiedConfiguration.BindingConfiguration.TimeoutReceive = sourceConfiguration.BindingConfiguration.TimeoutReceive;

            copiedConfiguration.BindingConfiguration.TimeoutSend = sourceConfiguration.BindingConfiguration.TimeoutSend;

            copiedConfiguration.BindingConfiguration.TransportCredentialType = sourceConfiguration.BindingConfiguration.TransportCredentialType;


            copiedConfiguration.ClientCredentials = new Server.Application.WebServiceHostClientCredentials ();

            copiedConfiguration.ClientCredentials.UserName = (!String.IsNullOrEmpty (sourceConfiguration.ClientCredentials.UserName)) ? sourceConfiguration.ClientCredentials.UserName : String.Empty;

            copiedConfiguration.ClientCredentials.Password = (!String.IsNullOrEmpty (sourceConfiguration.ClientCredentials.Password)) ? sourceConfiguration.ClientCredentials.Password : String.Empty;

            copiedConfiguration.ClientCredentials.Domain = (!String.IsNullOrEmpty (sourceConfiguration.ClientCredentials.Domain)) ? sourceConfiguration.ClientCredentials.Domain : String.Empty;

            copiedConfiguration.ClientCredentials.WindowsImpersonationLevel = sourceConfiguration.ClientCredentials.WindowsImpersonationLevel;


            return copiedConfiguration;

        }
        
        #endregion

    }

}
