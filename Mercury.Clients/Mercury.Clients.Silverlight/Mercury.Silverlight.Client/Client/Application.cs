using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mercury.Client {

    public class Application {

        #region Private Properties

        private System.Windows.Threading.Dispatcher mainDispatcher = null;

        private Caching.CacheManager cacheManager = new Mercury.Client.Caching.CacheManager ();

        private Session session = null;

        private Server.Enterprise.AuthenticationResponse authenticationResponse = new Server.Enterprise.AuthenticationResponse ();

        private Exception lastException;


        private String versionClient = String.Empty;

        private String versionServer = String.Empty;


        #region Service Client References

        private TimeSpan serviceTimeout = new TimeSpan (0, 20, 0);

        private Mercury.Client.Enumerations.ServiceBindingType serviceBindingType = Mercury.Client.Enumerations.ServiceBindingType.BasicHttpBinding;

        private String serviceHostAddress = String.Empty;

        private String serviceHostPort = String.Empty;

        private Boolean impersonateUser = true;


        private Int32 serviceAttempts = 0; // SERVICE RETRY ACCOUNT (FOR FAULTS)

        private Server.Enterprise.SecurityClient securityClient;

        private Server.Application.ApplicationClient applicationClient;

        #endregion
        
        #endregion 
        

        #region Public Properties

        public System.Windows.Threading.Dispatcher MainDispatcher { get { return mainDispatcher; } set { mainDispatcher = value; } }

        public Mercury.Client.Session Session { get { return session; } }

        public Server.Enterprise.AuthenticationResponse AuthenticationResponse { get { return authenticationResponse; } }


        public Caching.CacheManager CacheManager { get { return cacheManager; } set { cacheManager = value; } }

        public String ServiceHostAddress { get { return serviceHostAddress; } }

        public String ServiceHostPort { get { return serviceHostPort; } }


        public String VersionClient {

            get {

                if (String.IsNullOrEmpty (versionClient)) {

                    versionClient = System.Reflection.Assembly.GetExecutingAssembly ().FullName.Split (',')[1].Replace ("Version=", "");

                }

                return versionClient;

            }

        }


        public Exception LastException { get { return lastException; } }

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


        public void Authenticate (String environment, AuthenticateWindowsCompleted eventHandler) {

            ClearLastException ();

            try {

                Server.Enterprise.SecurityClient client = SecurityClient;

                client.AuthenticateWindowsCompleted += new EventHandler<Server.Enterprise.AuthenticateWindowsCompletedEventArgs> (AuthenticateWindows_Completed);

                client.AuthenticateWindowsCompleted += new EventHandler<Server.Enterprise.AuthenticateWindowsCompletedEventArgs> (eventHandler);

                client.AuthenticateWindowsAsync (environment);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void AuthenticateWindowsCompleted (Object sender, Server.Enterprise.AuthenticateWindowsCompletedEventArgs e);

        private void AuthenticateWindows_Completed (Object sender, Server.Enterprise.AuthenticateWindowsCompletedEventArgs e) {

            if ((e.Error == null) && (!e.Cancelled)) {

                if ((e.Result.AuthenticationError == Server.Enterprise.AuthenticationError.NoError) && (!String.IsNullOrWhiteSpace (e.Result.Token))) {

                    session = new Session (e.Result);

                }

            }

            return;

        }


        public Application (String token, String forServiceHostAddress, String forServiceHostPort, SessionGetFromServerCompleted sessionInitializedHandler) {

            serviceHostAddress = forServiceHostAddress;

            serviceHostPort = forServiceHostPort;

            session = new Session (token);

            SessionGetFromServer (token, sessionInitializedHandler);

            return;

        }

        private void SessionGetFromServer (String token, SessionGetFromServerCompleted sessionInitializedHandler) {

            Server.Application.ApplicationClient client = ApplicationClient;

            client.SessionGetCompleted += new EventHandler<Mercury.Server.Application.SessionGetCompletedEventArgs> (SessionGetFromServer_Completed);

            if (sessionInitializedHandler != null) {

                client.SessionGetCompleted += new EventHandler<Mercury.Server.Application.SessionGetCompletedEventArgs> (sessionInitializedHandler);

            }

            client.SessionGetAsync (token);

            return;

        }

        public delegate void SessionGetFromServerCompleted (Object sender, Server.Application.SessionGetCompletedEventArgs e);

        private void SessionGetFromServer_Completed (Object sender, Server.Application.SessionGetCompletedEventArgs e) {

            session = new Session (e.Result);

            return;

        }

        #endregion
        

        #region Support Functions

        public void ClearLastException () {

            lastException = null;

            return;

        }

        public void SetLastException (Exception exception) {

            //lastException = exception;

            //if (lastException != null) {

            //    System.Diagnostics.Trace.WriteLineIf (TraceSwitchGeneral.TraceError, "[" + DateTime.Now.ToString () + "] Client.Application [" + lastException.Source + "] " + lastException.Message);

            //    if (lastException.InnerException != null) {

            //        System.Diagnostics.Trace.WriteLineIf (TraceSwitchGeneral.TraceError, "Client.Application [" + lastException.InnerException.Source + "] " + lastException.InnerException.Message);

            //    }

            //    System.Diagnostics.Trace.WriteLineIf (TraceSwitchGeneral.TraceError, "** Stack Trace **");

            //    System.Diagnostics.StackTrace debugStack = new System.Diagnostics.StackTrace ();

            //    foreach (System.Diagnostics.StackFrame currentStackFrame in debugStack.GetFrames ()) {

            //        System.Diagnostics.Trace.WriteLineIf (TraceSwitchGeneral.TraceError, "    [" + currentStackFrame.GetMethod ().Module.Assembly.FullName + "] " + currentStackFrame.GetMethod ().Name);

            //    }

            //    System.Diagnostics.Trace.Flush ();

            //} // if (lastException != null) 

            lastException = exception;

            if (lastException != null) {

                System.Windows.Browser.HtmlPage.Window.Alert ("Client.Application: " + lastException.Message);

                System.Diagnostics.Debug.WriteLine ("Client.Application: " + lastException.Message);

                if (lastException.InnerException != null) {

                    System.Diagnostics.Debug.WriteLine ("Client.Application: " + lastException.InnerException.Message);

                }

                System.Diagnostics.Debug.WriteLine ("** Stack Trace **");

                System.Diagnostics.StackTrace debugStack = new System.Diagnostics.StackTrace ();

                foreach (System.Diagnostics.StackFrame currentStackFrame in debugStack.GetFrames ()) {

                    System.Diagnostics.Debug.WriteLine ("    [" + currentStackFrame.GetMethod ().Module.Assembly.FullName + "] " + currentStackFrame.GetMethod ().Name);

                }

            } // if (lastException != null) 

            return;

        }

        public void SetLastExceptionQuite (Exception exception) {

            lastException = exception;

            return;

        }

        public void SetLastException (Server.Enterprise.ServiceException serviceException) {

            Exception innerException = (serviceException.InnerException != null) ? new Exception (serviceException.InnerException.Message) : null;

            Exception applicationException = new Exception (serviceException.Message, innerException);            

            SetLastException (applicationException);

            return;

        }

        public void SetLastException (Server.Application.ServiceException serviceException) {

            Exception innerException = (serviceException.InnerException != null) ? new Exception (serviceException.InnerException.Message) : null;

            Exception applicationException = new Exception (serviceException.Message, innerException);

            SetLastException (applicationException);

            return;

        }

        public void SetLastExceptionQuite (Server.Application.ServiceException serviceException) {

            Exception innerException = (serviceException.InnerException != null) ? new Exception (serviceException.InnerException.Message) : null;

            Exception applicationException = new Exception (serviceException.Message, innerException);
            
            SetLastExceptionQuite (applicationException);

            return;

        }

        #endregion


        #region Initialization

        private void InitializeBinding () {

            try {

                serviceBindingType = Mercury.Client.Enumerations.ServiceBindingType.BasicHttpBinding;

                // TODO: UPDATE SILVERLIGHT

                //switch (((String)System.Configuration.ConfigurationManager.AppSettings.GetValues ("Mercury.Server.ServiceHost.BindingType")[0]).ToLower (new System.Globalization.CultureInfo (""))) {

                //    case "basic": serviceBindingType = Mercury.Client.Enumerations.ServiceBindingType.BasicHttpBinding; break;

                //    case "wshttp": serviceBindingType = Mercury.Client.Enumerations.ServiceBindingType.WsHttpBinding; break;

                //    case "nettcp": serviceBindingType = Mercury.Client.Enumerations.ServiceBindingType.NetTcpBinding; break;

                //    default: serviceBindingType = Mercury.Client.Enumerations.ServiceBindingType.BasicHttpBinding; break;

                //}

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

                // TODO: UPDATE SILVERLIGHT

                //case Mercury.Client.Enumerations.ServiceBindingType.NetTcpBinding:

                //    securityClient = new Server.Enterprise.SecurityClient (NetTcpBinding, EndpointAddressNetTcp ("Enterprise", "Security"));

                //    securityClient.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;

                //    applicationClient = new Server.Application.ApplicationClient (NetTcpBinding, EndpointAddressNetTcp ("Core", "Application"));

                //    applicationClient.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;

                //    break;

                case Enumerations.ServiceBindingType.Silverlight:
                    
                    securityClient = new Server.Enterprise.SecurityClient (SilverlightBasicHttpBindingNtlm, EndpointAddressSilverlight ("Enterprise", "Security"));

                    applicationClient = new Server.Application.ApplicationClient (BasicHttpBinding, EndpointAddress ("Core", "Application"));

                    break;

                case Mercury.Client.Enumerations.ServiceBindingType.BasicHttpBinding:

                default:

                    securityClient = new Server.Enterprise.SecurityClient (BasicHttpBinding, EndpointAddress ("Enterprise", "Security"));

                    applicationClient = new Server.Application.ApplicationClient (BasicHttpBinding, EndpointAddress ("Core", "Application"));

                    break;

            } // switch (serviceBindingType) 


            // TODO: UPDATE SILVERLIGHT

            // OPEN UP THE MAX OBJECTS IN GRAPH TO MAXIMUM

            //foreach (System.ServiceModel.Description.OperationDescription currentOperation in applicationClient.Endpoint.Contract.Operations) {

            //    System.ServiceModel.Description.DataContractSerializerOperationBehavior dataContractSerializer =

            //        (System.ServiceModel.Description.DataContractSerializerOperationBehavior)

            //        currentOperation.Behaviors.Find<System.ServiceModel.Description.DataContractSerializerOperationBehavior> ();

            //    if (dataContractSerializer != null) {

            //        dataContractSerializer.MaxItemsInObjectGraph = Int32.MaxValue;

            //    }

            //}

            return;

        }

        protected void SecurityClientSetToAnonymousWs () {

            if (securityClient != null) {

                securityClient.CloseAsync ();

                securityClient = null;

            }

            securityClient = new Server.Enterprise.SecurityClient (WSHttpBindingAnonymous, EndpointAddressWs ("Enterprise", "Security"));

        }

        protected void SecurityClientSetToWindowsWs () {

            if (securityClient != null) {

                securityClient.CloseAsync ();

                securityClient = null;

            }

            securityClient = new Server.Enterprise.SecurityClient (WSHttpBinding, EndpointAddressWs ("Enterprise", "Security"));

            return;

        }

        protected void SecurityClientSetToWindowsNetTcp () {

            if (securityClient != null) {

                securityClient.CloseAsync ();

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


                // TODO: UPDATE SILVERLIGHT

                //newBinding.Security.Transport.ClientCredentialType = System.ServiceModel.HttpClientCredentialType.None;
                //newBinding.Security.Transport.ProxyCredentialType = System.ServiceModel.HttpProxyCredentialType.None;
                //newBinding.Security.Transport.Realm = string.Empty;

                //newBinding.Security.Message.ClientCredentialType = System.ServiceModel.BasicHttpMessageCredentialType.UserName;
                //newBinding.Security.Message.AlgorithmSuite = System.ServiceModel.Security.SecurityAlgorithmSuite.Default;

                //newBinding.MaxReceivedMessageSize = Int32.MaxValue;
                //newBinding.MaxBufferPoolSize = Int32.MaxValue;
                //newBinding.MaxReceivedMessageSize = Int32.MaxValue;

                newBinding.MaxBufferSize = Int32.MaxValue;

                newBinding.MaxReceivedMessageSize = Int32.MaxValue;

                //newBinding.ReaderQuotas.MaxDepth = Int32.MaxValue;
                //newBinding.ReaderQuotas.MaxStringContentLength = Int32.MaxValue;
                //newBinding.ReaderQuotas.MaxArrayLength = Int32.MaxValue;
                //newBinding.ReaderQuotas.MaxNameTableCharCount = Int32.MaxValue;
                //newBinding.ReaderQuotas.MaxBytesPerRead = Int32.MaxValue;

                return newBinding;

            }

        }

        protected System.ServiceModel.Channels.Binding SilverlightBasicHttpBindingNtlm {

            get {

                System.ServiceModel.BasicHttpBinding newBinding = new System.ServiceModel.BasicHttpBinding ();

                newBinding.Name = "BasicHttpBindingNtlm";

                BindingTimeoutsSet (newBinding);

                newBinding.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.TransportCredentialOnly;                

                newBinding.MaxBufferSize = Int32.MaxValue;

                newBinding.MaxReceivedMessageSize = Int32.MaxValue;

                return newBinding;

            }

        }

        protected System.ServiceModel.Channels.Binding WSHttpBinding {

            get {

                // TODO: UPDATE SILVERLIGHT

                System.ServiceModel.BasicHttpBinding binding = new System.ServiceModel.BasicHttpBinding ();

                binding.Name = "WSHttpBinding";

                BindingTimeoutsSet (binding);

                binding.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.TransportCredentialOnly;                

                return binding;

                //System.ServiceModel.WSHttpBinding newBinding = new System.ServiceModel.WSHttpBinding ();

                //newBinding.Name = "WSHttpBinding";

                //BindingTimeoutsSet (newBinding);

                //newBinding.Security.Mode = System.ServiceModel.SecurityMode.Message;

                //newBinding.Security.Transport.ClientCredentialType = System.ServiceModel.HttpClientCredentialType.Windows;
                //newBinding.Security.Transport.ProxyCredentialType = System.ServiceModel.HttpProxyCredentialType.None;
                //newBinding.Security.Transport.Realm = string.Empty;

                //newBinding.Security.Message.ClientCredentialType = System.ServiceModel.MessageCredentialType.Windows;
                //newBinding.Security.Message.AlgorithmSuite = System.ServiceModel.Security.SecurityAlgorithmSuite.Default;

                //newBinding.MaxReceivedMessageSize = Int32.MaxValue;
                //newBinding.MaxBufferPoolSize = Int32.MaxValue;
                //newBinding.MaxReceivedMessageSize = Int32.MaxValue;

                //newBinding.ReaderQuotas.MaxDepth = Int32.MaxValue;
                //newBinding.ReaderQuotas.MaxStringContentLength = Int32.MaxValue;
                //newBinding.ReaderQuotas.MaxArrayLength = Int32.MaxValue;
                //newBinding.ReaderQuotas.MaxNameTableCharCount = Int32.MaxValue;
                //newBinding.ReaderQuotas.MaxBytesPerRead = Int32.MaxValue;

                //return newBinding;

            }

        }

        protected System.ServiceModel.Channels.Binding WSHttpBindingAnonymous {
            get {

                // TODO: UPDATE SILVERLIGHT

                return null;

                //System.ServiceModel.WSHttpBinding newBinding = new System.ServiceModel.WSHttpBinding ();

                //newBinding.Name = "WSHttpBinding";

                //BindingTimeoutsSet (newBinding);

                //newBinding.Security.Mode = System.ServiceModel.SecurityMode.Message;

                //newBinding.Security.Transport.ClientCredentialType = System.ServiceModel.HttpClientCredentialType.None;
                //newBinding.Security.Transport.ProxyCredentialType = System.ServiceModel.HttpProxyCredentialType.None;
                //newBinding.Security.Transport.Realm = string.Empty;

                //newBinding.Security.Message.ClientCredentialType = System.ServiceModel.MessageCredentialType.None;
                //newBinding.Security.Message.AlgorithmSuite = System.ServiceModel.Security.SecurityAlgorithmSuite.Default;

                //newBinding.MaxReceivedMessageSize = Int32.MaxValue;
                //newBinding.MaxBufferPoolSize = Int32.MaxValue;
                //newBinding.MaxReceivedMessageSize = Int32.MaxValue;

                //newBinding.ReaderQuotas.MaxDepth = Int32.MaxValue;
                //newBinding.ReaderQuotas.MaxStringContentLength = Int32.MaxValue;
                //newBinding.ReaderQuotas.MaxArrayLength = Int32.MaxValue;
                //newBinding.ReaderQuotas.MaxNameTableCharCount = Int32.MaxValue;
                //newBinding.ReaderQuotas.MaxBytesPerRead = Int32.MaxValue;

                //return newBinding;

            }

        }

        protected System.ServiceModel.Channels.Binding NetTcpBinding {

            get {

                // TODO: UPDATE SILVERLIGHT

                return null;

                //System.ServiceModel.NetTcpBinding binding = new System.ServiceModel.NetTcpBinding ();

                //binding.Name = "NetTcpBinding";

                //BindingTimeoutsSet (binding);



                //binding.ReliableSession.Ordered = true;

                //binding.ReliableSession.Enabled = true;

                //binding.ReliableSession.InactivityTimeout = new TimeSpan (00, 60, 0);


                //binding.Security.Mode = System.ServiceModel.SecurityMode.Message;

                //binding.Security.Transport.ClientCredentialType = System.ServiceModel.TcpClientCredentialType.Windows;

                //binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;


                //binding.Security.Message.ClientCredentialType = System.ServiceModel.MessageCredentialType.Windows;

                //binding.Security.Message.AlgorithmSuite = System.ServiceModel.Security.SecurityAlgorithmSuite.Default;


                //binding.MaxReceivedMessageSize = Int32.MaxValue;

                //binding.MaxBufferPoolSize = Int32.MaxValue;

                //binding.MaxReceivedMessageSize = Int32.MaxValue;

                //binding.ReaderQuotas.MaxDepth = Int32.MaxValue;

                //binding.ReaderQuotas.MaxStringContentLength = Int32.MaxValue;

                //binding.ReaderQuotas.MaxArrayLength = Int32.MaxValue;

                //binding.ReaderQuotas.MaxNameTableCharCount = Int32.MaxValue;

                //binding.ReaderQuotas.MaxBytesPerRead = Int32.MaxValue;


                //return binding;

            }

        }


        protected String EndpointBaseAddress (String namespacePath, String serviceName) {

            try {

                // TODO: UPDATE SILVERLIGHT

                // REMOVE BELOW WHEN FIXED

                if (String.IsNullOrEmpty (serviceHostAddress)) { serviceHostAddress = "localhost"; }

                if (String.IsNullOrEmpty (serviceHostPort)) { serviceHostPort = "8080"; }


                //if ((String.IsNullOrEmpty (serviceHostAddress)) || (String.IsNullOrEmpty (serviceHostPort))) {

                //    serviceHostAddress = System.Configuration.ConfigurationManager.AppSettings.GetValues ("Mercury.Server.ServiceHost.Address")[0].ToString ();

                //    serviceHostPort = System.Configuration.ConfigurationManager.AppSettings.GetValues ("Mercury.Server.ServiceHost.Port")[0].ToString ();

                //}

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

        protected System.ServiceModel.EndpointAddress EndpointAddressSilverlight (String namespacePath, String serviceName) {

            String endpointAddress = EndpointBaseAddress (namespacePath, serviceName) + @"/silverlight";

            return new System.ServiceModel.EndpointAddress (endpointAddress);

        }

        protected System.ServiceModel.EndpointAddress EndpointAddressWs (String namespacePath, String serviceName) {

            return new System.ServiceModel.EndpointAddress (EndpointBaseAddress (namespacePath, serviceName) + "/ws");

        }

        protected System.ServiceModel.EndpointAddress EndpointAddressNetTcp (String namespacePath, String serviceName) {

            try {

                // TODO: UPDATE SILVERLIGHT

                // REMOVE BELOW WHEN FIXED

                if (String.IsNullOrEmpty (serviceHostAddress)) { serviceHostAddress = "localhost"; }

                if (String.IsNullOrEmpty (serviceHostPort)) { serviceHostPort = "808"; }


                //if ((String.IsNullOrEmpty (serviceHostAddress)) || (String.IsNullOrEmpty (serviceHostPort))) {

                //    serviceHostAddress = System.Configuration.ConfigurationManager.AppSettings.GetValues ("Mercury.Server.ServiceHost.Address")[0].ToString ();

                //    serviceHostPort = System.Configuration.ConfigurationManager.AppSettings.GetValues ("Mercury.Server.ServiceHost.Port")[0].ToString ();

                //}

            }

            catch {

                if (String.IsNullOrEmpty (serviceHostAddress)) { serviceHostAddress = "localhost"; }

                if (String.IsNullOrEmpty (serviceHostPort)) { serviceHostPort = "808"; }

            }

            String endpointAddress = @"net.tcp://" + serviceHostAddress + ":" + serviceHostPort + @"/" + namespacePath + @"/" + serviceName + ".svc";

            return new System.ServiceModel.EndpointAddress (endpointAddress);

        }

        #endregion


        #region Service Clients

        public Server.Enterprise.SecurityClient SecurityClient {

            get {

                try {

                    if (securityClient == null) {

                        InitializeServices ();

                    }

                    switch (securityClient.State) {

                        case System.ServiceModel.CommunicationState.Created:

                            securityClient.OpenAsync ();

                            break;

                        case System.ServiceModel.CommunicationState.Opening:

                        case System.ServiceModel.CommunicationState.Opened:

                            /* DO NOTHING */

                            break;

                        case System.ServiceModel.CommunicationState.Closing:

                        case System.ServiceModel.CommunicationState.Closed:

                            securityClient = null;

                            InitializeServices ();

                            securityClient.OpenAsync ();

                            break;

                        case System.ServiceModel.CommunicationState.Faulted:

                            throw new Exception ("Application Client Faulted.");

                        default:

                            SetLastException (new Exception ("Unknown Application Client State: " + securityClient.State.ToString ()));

                            break;

                    }

                }

                catch (Exception applicationException) {

                    // RECORD ANY NON-CUSTOM APPLICATION EXCEPTIONS TO THE LOG

                    if (!(applicationException is Exception)) { SetLastException (applicationException); }


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

                    System.Diagnostics.Debug.WriteLine ("----> Security Request: " + (new System.Diagnostics.StackTrace ().GetFrames ()[1].GetMethod ().Name));

                    System.Diagnostics.Debug.WriteLine ("   <-- FROM -->   " + (new System.Diagnostics.StackTrace ().GetFrames ()[2].GetMethod ().Name));

                    System.Diagnostics.Debug.WriteLine ("   <-- FROM -->   " + (new System.Diagnostics.StackTrace ().GetFrames ()[3].GetMethod ().Name));

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

                    securityClient.CloseAsync ();

                    break;

                case System.ServiceModel.CommunicationState.Faulted:

                    securityClient.Abort ();

                    break;

            }

#if DEBUG

            // WRITE OUT DEBUG CHATTER 

            if (securityClient != null) {

                System.Diagnostics.Debug.WriteLine ("----> Security Close: " + (new System.Diagnostics.StackTrace ().GetFrames ()[1].GetMethod ().Name));

                System.Diagnostics.Debug.WriteLine ("   <-- FROM -->   " + (new System.Diagnostics.StackTrace ().GetFrames ()[2].GetMethod ().Name));

                System.Diagnostics.Debug.WriteLine ("   <-- FROM -->   " + (new System.Diagnostics.StackTrace ().GetFrames ()[3].GetMethod ().Name));

                System.Diagnostics.Debug.WriteLine (String.Empty);

            }

#endif

            return;

        }

        public Server.Application.ApplicationClient ApplicationClient {

            get {

                Server.Application.ApplicationClient client = null;

                try {

                    // CREATE A NEW INSTANCE FOR EACH REQUEST

                    client = new Server.Application.ApplicationClient (BasicHttpBinding, EndpointAddress ("Core", "Application"));

                    client.OpenAsync ();
                    
                }

                catch (Exception applicationException) {

                    // RECORD ANY NON-CUSTOM APPLICATION EXCEPTIONS TO THE LOG

                    if (!(applicationException is Exception)) { SetLastException (applicationException); }


                    // INCREMENT ATTEMPTS COUNTER 

                    serviceAttempts = serviceAttempts + 1;

                    // ANOTHER ATTEMPT (UP TO 3 TRIES)

                    if (serviceAttempts < 3) { client = ApplicationClient; }

                }


#if DEBUG

                // WRITE OUT DEBUG CHATTER 

                if (applicationClient != null) {

                    String serviceDebug = "----> Application Request: " + (new System.Diagnostics.StackTrace ().GetFrames ()[1].GetMethod ().Name);

                    serviceDebug += "   <-- FROM -->   " + (new System.Diagnostics.StackTrace ().GetFrames ()[2].GetMethod ().Name);

                    serviceDebug += "   <-- FROM -->   " + (new System.Diagnostics.StackTrace ().GetFrames ()[3].GetMethod ().Name);

                    System.Diagnostics.Debug.WriteLine (serviceDebug);

                }

#endif

                // RESET SERVICE ATTEMPTS FOR NEXT CALL

                serviceAttempts = 0;

                return client;

            }

        }

        public void ApplicationClientClose () {

            if (applicationClient == null) { return; }


            switch (applicationClient.State) {

                case System.ServiceModel.CommunicationState.Created:

                case System.ServiceModel.CommunicationState.Opened:

                    applicationClient.CloseAsync ();

                    break;

                case System.ServiceModel.CommunicationState.Faulted:

                    applicationClient.Abort ();

                    break;

            }

#if DEBUG

            // WRITE OUT DEBUG CHATTER 

            if (securityClient != null) {

                System.Diagnostics.Debug.WriteLine ("----> Application Close: " + (new System.Diagnostics.StackTrace ().GetFrames ()[1].GetMethod ().Name));

                System.Diagnostics.Debug.WriteLine ("   <-- FROM -->   " + (new System.Diagnostics.StackTrace ().GetFrames ()[2].GetMethod ().Name));

                System.Diagnostics.Debug.WriteLine ("   <-- FROM -->   " + (new System.Diagnostics.StackTrace ().GetFrames ()[3].GetMethod ().Name));

                System.Diagnostics.Debug.WriteLine (String.Empty);

            }

#endif
            return;

        }

        #endregion


        #region Silverlight Support - Public Events

        public event EventHandler<System.EventArgs> GlobalProgressBarShow;

        public event EventHandler<System.EventArgs> GlobalProgressBarHide;

        public void ProgressBarShow (Object sender) {

            if (GlobalProgressBarShow != null) { GlobalProgressBarShow (sender, new EventArgs ()); }

            return;

        }

        public void ProgressBarHide (Object sender) {

            if (GlobalProgressBarHide != null) { GlobalProgressBarHide (sender, new EventArgs ()); }

            return;

        }

        #endregion


        #region Server Version

        public void VersionServer (Boolean useCaching, VersionServerCompleted eventHandler) {

            String cacheKey = "Core.VersionServer";

            Server.Application.VersionServerCompletedEventArgs e;

            ClearLastException ();

            try {

                e = (Server.Application.VersionServerCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.VersionServerCompleted += new EventHandler<Server.Application.VersionServerCompletedEventArgs> (VersionServerCompleted_CacheIntercept);

                    client.VersionServerCompleted += new EventHandler<Server.Application.VersionServerCompletedEventArgs> (eventHandler);

                    client.VersionServerAsync (session.Token);

                }

                else {

                    eventHandler (this, e);

                }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        private void VersionServerCompleted_CacheIntercept (Object sender, Server.Application.VersionServerCompletedEventArgs e) {

            String cacheKey = "Core.VersionServer";

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

            }

            return;

        }

        public delegate void VersionServerCompleted (Object sender, Server.Application.VersionServerCompletedEventArgs e);

        #endregion 

        
        #region Authorization Permissions

        public Boolean HasEnterprisePermission (String permissionName) {

            if (session.EnterprisePermissionSet.ContainsKey (Server.EnterprisePermissions.EnterpriseAdministrator)) { return true; }

            return session.EnterprisePermissionSet.ContainsKey (permissionName);

        }

        public Boolean HasEnvironmentPermission (String permissionName) {

            if (session.EnterprisePermissionSet.ContainsKey (Server.EnterprisePermissions.EnterpriseAdministrator)) { return true; }

            if (session.EnvironmentPermissionSet.ContainsKey (Server.EnvironmentPermissions.EnvironmentAdministrator)) { return true; }

            return session.EnvironmentPermissionSet.ContainsKey (permissionName);

        }

        #endregion 

        
        #region Security Authorities

        public void SecurityAuthoritiesAvailable (SecurityAuthoritiesAvailableCompleted eventHandler) {

            ClearLastException ();

            try {

                Server.Application.ApplicationClient client = ApplicationClient;

                client.SecurityAuthoritiesAvailableCompleted += new EventHandler<Server.Application.SecurityAuthoritiesAvailableCompletedEventArgs> (eventHandler);

                client.SecurityAuthoritiesAvailableAsync (session.Token);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void SecurityAuthoritiesAvailableCompleted (Object sender, Server.Application.SecurityAuthoritiesAvailableCompletedEventArgs e);


        public void SecurityAuthoritySecurityGroupDictionary (Int64 securityAuthorityId, SecurityAuthoritySecurityGroupDictionaryCompleted eventHandler) {

            ClearLastException ();

            try {

                Server.Application.ApplicationClient client = ApplicationClient;

                client.SecurityAuthoritySecurityGroupDictionaryCompleted += new EventHandler<Server.Application.SecurityAuthoritySecurityGroupDictionaryCompletedEventArgs> (eventHandler);

                client.SecurityAuthoritySecurityGroupDictionaryAsync (session.Token, securityAuthorityId);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void SecurityAuthoritySecurityGroupDictionaryCompleted (Object sender, Server.Application.SecurityAuthoritySecurityGroupDictionaryCompletedEventArgs e);


        public void SecurityAuthoritySecurityGroups (Int64 securityAuthorityId, Boolean useCaching, SecurityAuthoritySecurityGroupsCompleted eventHandler) {

            String cacheKey = "SecurityAuthoritySecurityGroups." + securityAuthorityId.ToString ();

            Server.Application.SecurityAuthoritySecurityGroupsCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.SecurityAuthoritySecurityGroupsCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.SecurityAuthoritySecurityGroupsCompleted += new EventHandler<Mercury.Server.Application.SecurityAuthoritySecurityGroupsCompletedEventArgs> (SecurityAuthoritySecurityGroupsCompleted_CacheIntercept);

                    client.SecurityAuthoritySecurityGroupsCompleted += new EventHandler<Mercury.Server.Application.SecurityAuthoritySecurityGroupsCompletedEventArgs> (eventHandler);

                    client.SecurityAuthoritySecurityGroupsAsync (session.Token, securityAuthorityId);

                }

                else { eventHandler (this, e); }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        private void SecurityAuthoritySecurityGroupsCompleted_CacheIntercept (Object sender, Server.Application.SecurityAuthoritySecurityGroupsCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                if (e.Result.Collection.Count > 0) {

                    String cacheKey = "SecurityAuthoritySecurityGroups." + e.Result.Collection[0].SecurityAuthorityId.ToString ();

                    cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

                }

            }

            return;

        }

        public delegate void SecurityAuthoritySecurityGroupsCompleted (Object sender, Server.Application.SecurityAuthoritySecurityGroupsCompletedEventArgs e);



        public void SecurityAuthoritySecurityGroupMembership (String securityAuthorityName, String securityGroupId, Boolean useCaching, SecurityAuthoritySecurityGroupMembershipCompleted eventHandler) {

            String cacheKey = "SecurityAuthoritySecurityGroupMembership." + securityAuthorityName + "." + securityGroupId;

            Server.Application.SecurityAuthoritySecurityGroupMembershipCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.SecurityAuthoritySecurityGroupMembershipCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    // client.SecurityAuthoritySecurityGroupMembershipCompleted += new EventHandler<Mercury.Server.Application.SecurityAuthoritySecurityGroupMembershipCompletedEventArgs> (SecurityAuthoritySecurityGroupMembershipCompleted_CacheIntercept);

                    client.SecurityAuthoritySecurityGroupMembershipCompleted += new EventHandler<Mercury.Server.Application.SecurityAuthoritySecurityGroupMembershipCompletedEventArgs> (eventHandler);

                    client.SecurityAuthoritySecurityGroupMembershipAsync (session.Token, securityAuthorityName, securityGroupId);

                }

                else { eventHandler (this, e); }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void SecurityAuthoritySecurityGroupMembershipCompleted (Object sender, Server.Application.SecurityAuthoritySecurityGroupMembershipCompletedEventArgs e);

        #endregion 
        
        
        #region Core Object Methods

        public void CoreObject_Validate (Server.Application.CoreObject coreObject, CoreObject_ValidateCompleted eventHandler) {

            ClearLastException ();

            try {

                Server.Application.ApplicationClient client = ApplicationClient;

                client.CoreObject_ValidateCompleted += new EventHandler<Server.Application.CoreObject_ValidateCompletedEventArgs> (eventHandler);

                client.CoreObject_ValidateAsync (session.Token, coreObject);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void CoreObject_ValidateCompleted (Object sender, Server.Application.CoreObject_ValidateCompletedEventArgs e);

        public void CoreObject_DataBindingContexts (Server.Application.CoreObject coreObject, CoreObject_DataBindingContextsCompleted eventHandler) {

            ClearLastException ();

            try {

                Server.Application.ApplicationClient client = ApplicationClient;

                client.CoreObject_DataBindingContextsCompleted += new EventHandler<Server.Application.CoreObject_DataBindingContextsCompletedEventArgs> (eventHandler);

                client.CoreObject_DataBindingContextsAsync (session.Token, coreObject);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void CoreObject_DataBindingContextsCompleted (Object sender, Server.Application.CoreObject_DataBindingContextsCompletedEventArgs e);

        #endregion 

        
        #region Correspondence

        public void CorrespondencesAvailable (Boolean useCaching, CorrespondencesAvailableCompleted eventHandler) {

            String cacheKey = "Core.Correspondences.";

            Server.Application.CorrespondencesAvailableCompletedEventArgs e;

            ClearLastException ();

            try {

                e = (Server.Application.CorrespondencesAvailableCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.CorrespondencesAvailableCompleted += new EventHandler<Server.Application.CorrespondencesAvailableCompletedEventArgs> (CorrespondencesAvailableCompleted_CacheIntercept);

                    client.CorrespondencesAvailableCompleted += new EventHandler<Server.Application.CorrespondencesAvailableCompletedEventArgs> (eventHandler);

                    client.CorrespondencesAvailableAsync (session.Token);

                }

                else {

                    eventHandler (this, e);

                }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        private void CorrespondencesAvailableCompleted_CacheIntercept (Object sender, Server.Application.CorrespondencesAvailableCompletedEventArgs e) {

            String cacheKey = "Core.Correspondences.";

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

            }

            return;

        }

        public delegate void CorrespondencesAvailableCompleted (Object sender, Server.Application.CorrespondencesAvailableCompletedEventArgs e);


        public void CorrespondenceGet (Int64 correspondenceId, Boolean useCaching, CorrespondenceGetCompleted eventHandler) {

            String cacheKey = "Core.Correspondence." + correspondenceId.ToString ();

            Server.Application.CorrespondenceGetCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.CorrespondenceGetCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.CorrespondenceGetCompleted += new EventHandler<Server.Application.CorrespondenceGetCompletedEventArgs> (CorrespondenceGetCompleted_CacheIntercept);

                    client.CorrespondenceGetCompleted += new EventHandler<Server.Application.CorrespondenceGetCompletedEventArgs> (eventHandler);

                    client.CorrespondenceGetAsync (session.Token, correspondenceId);

                }

                else {

                    eventHandler (this, e);

                }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        private void CorrespondenceGetCompleted_CacheIntercept (Object sender, Server.Application.CorrespondenceGetCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                String cacheKey = "Core.Correspondence." + e.Result.Id.ToString ();

                cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

            }

            return;

        }

        public delegate void CorrespondenceGetCompleted (Object sender, Server.Application.CorrespondenceGetCompletedEventArgs e);

        #endregion 


        #region Core - Entity

        #region Entity Objects

        public void EntityGet (Int64 entityId, Boolean useCaching, EntityGetCompleted eventHandler) {

            String cacheKey = "Core.Entity." + entityId.ToString ();

            Server.Application.EntityGetCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.EntityGetCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.EntityGetCompleted += new EventHandler<Server.Application.EntityGetCompletedEventArgs> (EntityGetCompleted_CacheIntercept);

                    client.EntityGetCompleted += new EventHandler<Server.Application.EntityGetCompletedEventArgs> (eventHandler);

                    client.EntityGetAsync (session.Token, entityId);

                }

                else {

                    eventHandler (this, e);

                }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        private void EntityGetCompleted_CacheIntercept (Object sender, Server.Application.EntityGetCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                String cacheKey = "Core.Entity." + e.Result.Id.ToString ();

                cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

            }

            return;

        }

        public delegate void EntityGetCompleted (Object sender, Server.Application.EntityGetCompletedEventArgs e);


        public void EntityAddressGet (Int64 entityAddressId, Boolean useCaching, EntityAddressGetCompleted eventHandler) {

            String cacheKey = "Core.EntityAddress." + entityAddressId.ToString ();

            Server.Application.EntityAddressGetCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.EntityAddressGetCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.EntityAddressGetCompleted += new EventHandler<Server.Application.EntityAddressGetCompletedEventArgs> (EntityAddressGetCompleted_CacheIntercept);

                    client.EntityAddressGetCompleted += new EventHandler<Server.Application.EntityAddressGetCompletedEventArgs> (eventHandler);

                    client.EntityAddressGetAsync (session.Token, entityAddressId);

                }

                else {

                    eventHandler (this, e);

                }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        private void EntityAddressGetCompleted_CacheIntercept (Object sender, Server.Application.EntityAddressGetCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                String cacheKey = "Core.EntityAddress." + e.Result.Id.ToString ();

                cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

            }

            return;

        }

        public delegate void EntityAddressGetCompleted (Object sender, Server.Application.EntityAddressGetCompletedEventArgs e);


        public void EntityAddressesGet (Int64 entityId, Boolean useCaching, EntityAddressesGetCompleted eventHandler) {

            String cacheKey = "Core.EntityAddresses." + entityId.ToString ();

            Server.Application.EntityAddressesGetCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.EntityAddressesGetCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.EntityAddressesGetCompleted += new EventHandler<Server.Application.EntityAddressesGetCompletedEventArgs> (EntityAddressesGetCompleted_CacheIntercept);

                    client.EntityAddressesGetCompleted += new EventHandler<Server.Application.EntityAddressesGetCompletedEventArgs> (eventHandler);

                    client.EntityAddressesGetAsync (session.Token, entityId);

                }

                else {

                    eventHandler (this, e);

                }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        private void EntityAddressesGetCompleted_CacheIntercept (Object sender, Server.Application.EntityAddressesGetCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                if (e.Result.Collection.Count > 0) {

                    String cacheKey = "Core.EntityAddresses." + e.Result.Collection[0].EntityId.ToString ();

                    cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

                }

            }

            return;

        }

        public delegate void EntityAddressesGetCompleted (Object sender, Server.Application.EntityAddressesGetCompletedEventArgs e);


        public void EntityAddressGetByTypeDate (Int64 entityId, Server.Application.EntityAddressType addressType, DateTime forDate, Boolean useCaching, EntityAddressGetByTypeDateCompleted eventHandler) {

            String cacheKey = "Core.EntityAddress." + entityId.ToString () + "." + addressType + "." + forDate.ToString ();

            Server.Application.EntityAddressGetByTypeDateCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.EntityAddressGetByTypeDateCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.EntityAddressGetByTypeDateCompleted += new EventHandler<Server.Application.EntityAddressGetByTypeDateCompletedEventArgs> (EntityAddressGetByTypeDateCompleted_CacheIntercept);

                    client.EntityAddressGetByTypeDateCompleted += new EventHandler<Server.Application.EntityAddressGetByTypeDateCompletedEventArgs> (eventHandler);

                    client.EntityAddressGetByTypeDateAsync (session.Token, entityId, addressType, forDate);

                }

                else {

                    eventHandler (this, e);

                }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        private void EntityAddressGetByTypeDateCompleted_CacheIntercept (Object sender, Server.Application.EntityAddressGetByTypeDateCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                String cacheKey = "Core.EntityAddress." + e.Result.EntityId.ToString () + "." + e.Result.AddressType + "." + e.Result.TerminationDate.ToString ();

                cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

            }

            return;

        }

        public delegate void EntityAddressGetByTypeDateCompleted (Object sender, Server.Application.EntityAddressGetByTypeDateCompletedEventArgs e);


        public void EntityContactInformationGet (Int64 entityContactInformationId, Boolean useCaching, EntityContactInformationGetCompleted eventHandler) {

            String cacheKey = "Core.EntityContactInformation." + entityContactInformationId.ToString ();

            Server.Application.EntityContactInformationGetCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.EntityContactInformationGetCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.EntityContactInformationGetCompleted += new EventHandler<Server.Application.EntityContactInformationGetCompletedEventArgs> (EntityContactInformationGetCompleted_CacheIntercept);

                    client.EntityContactInformationGetCompleted += new EventHandler<Server.Application.EntityContactInformationGetCompletedEventArgs> (eventHandler);

                    client.EntityContactInformationGetAsync (session.Token, entityContactInformationId);

                }

                else {

                    eventHandler (this, e);

                }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        private void EntityContactInformationGetCompleted_CacheIntercept (Object sender, Server.Application.EntityContactInformationGetCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                String cacheKey = "Core.EntityContactInformation." + e.Result.Id.ToString ();

                cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

            }

            return;

        }

        public delegate void EntityContactInformationGetCompleted (Object sender, Server.Application.EntityContactInformationGetCompletedEventArgs e);


        public void EntityContactInformationsGet (Int64 entityId, Boolean useCaching, EntityContactInformationsGetCompleted eventHandler) {

            String cacheKey = "Core.EntityContactInformation.." + entityId.ToString ();            

            Server.Application.EntityContactInformationsGetCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.EntityContactInformationsGetCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.EntityContactInformationsGetCompleted += new EventHandler<Server.Application.EntityContactInformationsGetCompletedEventArgs> (EntityContactInformationsGetCompleted_CacheIntercept);

                    client.EntityContactInformationsGetCompleted += new EventHandler<Server.Application.EntityContactInformationsGetCompletedEventArgs> (eventHandler);

                    client.EntityContactInformationsGetAsync (session.Token, entityId);

                }

                else {

                    eventHandler (this, e);

                }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        private void EntityContactInformationsGetCompleted_CacheIntercept (Object sender, Server.Application.EntityContactInformationsGetCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                if (e.Result.Collection.Count > 0) {

                    String cacheKey = "Core.EntityContactInformation.." + e.Result.Collection[0].EntityId.ToString ();

                    cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

                }

            }

            return;

        }

        public delegate void EntityContactInformationsGetCompleted (Object sender, Server.Application.EntityContactInformationsGetCompletedEventArgs e);

        #endregion 

        #endregion 

        
        #region Core - Insurer

        #region Insurer 

        public void InsurerGet (Int64 insurerId, Boolean useCaching, InsurerGetCompleted eventHandler) {

            String cacheKey = "Core.Insurer." + insurerId.ToString ();

            Server.Application.InsurerGetCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.InsurerGetCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.InsurerGetCompleted += new EventHandler<Server.Application.InsurerGetCompletedEventArgs> (InsurerGetCompleted_CacheIntercept);

                    client.InsurerGetCompleted += new EventHandler<Server.Application.InsurerGetCompletedEventArgs> (eventHandler);

                    client.InsurerGetAsync (session.Token, insurerId);

                }

                else {

                    eventHandler (this, e);

                }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        private void InsurerGetCompleted_CacheIntercept (Object sender, Server.Application.InsurerGetCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                String cacheKey = "Core.Insurer." + e.Result.Id.ToString ();

                cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

            }

            return;

        }

        public delegate void InsurerGetCompleted (Object sender, Server.Application.InsurerGetCompletedEventArgs e);

        #endregion 
        

        #region Insurer - Insurance Type

        public void InsuranceTypesAvailable (Boolean useCaching, InsuranceTypesAvailableCompleted eventHandler) {

            String cacheKey = "InsuranceTypesAvailable";

            Server.Application.InsuranceTypesAvailableCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.InsuranceTypesAvailableCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.InsuranceTypesAvailableCompleted += new EventHandler<Mercury.Server.Application.InsuranceTypesAvailableCompletedEventArgs> (eventHandler);

                    client.InsuranceTypesAvailableCompleted += new EventHandler<Mercury.Server.Application.InsuranceTypesAvailableCompletedEventArgs> (InsuranceTypesAvailableCompleted_CacheIntercept);

                    client.InsuranceTypesAvailableAsync (session.Token);

                }

                else { eventHandler (this, e); }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public void InsuranceTypesAvailableCompleted_CacheIntercept (Object sender, Server.Application.InsuranceTypesAvailableCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                if (e.Result.Collection.Count > 0) {

                    String cacheKey = "InsuranceTypesAvailable";

                    cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

                    foreach (Server.Application.InsuranceType currentInsuranceType in e.Result.Collection) {

                        cacheKey = "InsuranceType." + currentInsuranceType.Id.ToString ();

                        Server.Application.InsuranceTypeGetCompletedEventArgs cacheEventArgs = new Mercury.Server.Application.InsuranceTypeGetCompletedEventArgs (new Object[] { currentInsuranceType }, null, false, e.UserState);

                        cacheManager.CacheObject (cacheKey, cacheEventArgs, Mercury.Client.Caching.CacheDuration.Medium, true);

                    }

                }

            }

            return;

        }

        public delegate void InsuranceTypesAvailableCompleted (Object sender, Server.Application.InsuranceTypesAvailableCompletedEventArgs e);


        public void InsuranceTypeDictionary (Boolean useCaching, InsuranceTypeDictionaryCompleted eventHandler) {

            String cacheKey = "InsuranceTypeDictionary";

            Server.Application.InsuranceTypeDictionaryCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.InsuranceTypeDictionaryCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.InsuranceTypeDictionaryCompleted += new EventHandler<Mercury.Server.Application.InsuranceTypeDictionaryCompletedEventArgs> (eventHandler);

                    client.InsuranceTypeDictionaryCompleted += new EventHandler<Mercury.Server.Application.InsuranceTypeDictionaryCompletedEventArgs> (InsuranceTypeDictionaryCompleted_CacheIntercept);

                    client.InsuranceTypeDictionaryAsync (session.Token);

                }

                else { eventHandler (this, e); }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public void InsuranceTypeDictionaryCompleted_CacheIntercept (Object sender, Server.Application.InsuranceTypeDictionaryCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                if (e.Result.Dictionary.Count > 0) {

                    String cacheKey = "InsuranceTypeDictionary";

                    cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

                }

            }

            return;

        }

        public delegate void InsuranceTypeDictionaryCompleted (Object sender, Server.Application.InsuranceTypeDictionaryCompletedEventArgs e);


        public void InsuranceTypeGet (Int64 insuranceTypeId, Boolean useCaching, InsuranceTypeGetCompleted eventHandler) {

            String cacheKey = "InsuranceType." + insuranceTypeId.ToString ();

            Server.Application.InsuranceTypeGetCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.InsuranceTypeGetCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.InsuranceTypeGetCompleted += new EventHandler<Mercury.Server.Application.InsuranceTypeGetCompletedEventArgs> (eventHandler);

                    client.InsuranceTypeGetCompleted += new EventHandler<Mercury.Server.Application.InsuranceTypeGetCompletedEventArgs> (InsuranceTypeGetCompleted_CacheIntercept);

                    client.InsuranceTypeGetAsync (session.Token, insuranceTypeId);

                }

                else { eventHandler (this, e); }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public void InsuranceTypeGetCompleted_CacheIntercept (Object sender, Server.Application.InsuranceTypeGetCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                if (e.Result != null) {

                    String cacheKey = "InsuranceType." + e.Result.Id.ToString ();

                    cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

                }

            }

            return;

        }

        public delegate void InsuranceTypeGetCompleted (Object sender, Server.Application.InsuranceTypeGetCompletedEventArgs e);

        #endregion 

        
        #region Program 

        public void ProgramGet (Int64 programId, Boolean useCaching, ProgramGetCompleted eventHandler) {

            String cacheKey = "Core.Program." + programId.ToString ();

            Server.Application.ProgramGetCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.ProgramGetCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.ProgramGetCompleted += new EventHandler<Server.Application.ProgramGetCompletedEventArgs> (ProgramGetCompleted_CacheIntercept);

                    client.ProgramGetCompleted += new EventHandler<Server.Application.ProgramGetCompletedEventArgs> (eventHandler);

                    client.ProgramGetAsync (session.Token, programId);

                }

                else {

                    eventHandler (this, e);

                }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        private void ProgramGetCompleted_CacheIntercept (Object sender, Server.Application.ProgramGetCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                String cacheKey = "Core.Program." + e.Result.Id.ToString ();

                cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

            }

            return;

        }

        public delegate void ProgramGetCompleted (Object sender, Server.Application.ProgramGetCompletedEventArgs e);

        #endregion 
        

        #region BenefitPlan

        public void BenefitPlanGet (Int64 benefitPlanId, Boolean useCaching, BenefitPlanGetCompleted eventHandler) {

            String cacheKey = "Core.BenefitPlan." + benefitPlanId.ToString ();

            Server.Application.BenefitPlanGetCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.BenefitPlanGetCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.BenefitPlanGetCompleted += new EventHandler<Server.Application.BenefitPlanGetCompletedEventArgs> (BenefitPlanGetCompleted_CacheIntercept);

                    client.BenefitPlanGetCompleted += new EventHandler<Server.Application.BenefitPlanGetCompletedEventArgs> (eventHandler);

                    client.BenefitPlanGetAsync (session.Token, benefitPlanId);

                }

                else {

                    eventHandler (this, e);

                }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        private void BenefitPlanGetCompleted_CacheIntercept (Object sender, Server.Application.BenefitPlanGetCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                String cacheKey = "Core.BenefitPlan." + e.Result.Id.ToString ();

                cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

            }

            return;

        }

        public delegate void BenefitPlanGetCompleted (Object sender, Server.Application.BenefitPlanGetCompletedEventArgs e);

        #endregion 
        

        #region CoverageType

        public void CoverageTypeGet (Int64 coverageTypeId, Boolean useCaching, CoverageTypeGetCompleted eventHandler) {

            String cacheKey = "Core.CoverageType." + coverageTypeId.ToString ();

            Server.Application.CoverageTypeGetCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.CoverageTypeGetCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.CoverageTypeGetCompleted += new EventHandler<Server.Application.CoverageTypeGetCompletedEventArgs> (CoverageTypeGetCompleted_CacheIntercept);

                    client.CoverageTypeGetCompleted += new EventHandler<Server.Application.CoverageTypeGetCompletedEventArgs> (eventHandler);

                    client.CoverageTypeGetAsync (session.Token, coverageTypeId);

                }

                else {

                    eventHandler (this, e);

                }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        private void CoverageTypeGetCompleted_CacheIntercept (Object sender, Server.Application.CoverageTypeGetCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                String cacheKey = "Core.CoverageType." + e.Result.Id.ToString ();

                cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

            }

            return;

        }

        public delegate void CoverageTypeGetCompleted (Object sender, Server.Application.CoverageTypeGetCompletedEventArgs e);

        #endregion 

        
        #region CoverageLevel

        public void CoverageLevelGet (Int64 coverageLevelId, Boolean useCaching, CoverageLevelGetCompleted eventHandler) {

            String cacheKey = "Core.CoverageLevel." + coverageLevelId.ToString ();

            Server.Application.CoverageLevelGetCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.CoverageLevelGetCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.CoverageLevelGetCompleted += new EventHandler<Server.Application.CoverageLevelGetCompletedEventArgs> (CoverageLevelGetCompleted_CacheIntercept);

                    client.CoverageLevelGetCompleted += new EventHandler<Server.Application.CoverageLevelGetCompletedEventArgs> (eventHandler);

                    client.CoverageLevelGetAsync (session.Token, coverageLevelId);

                }

                else {

                    eventHandler (this, e);

                }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        private void CoverageLevelGetCompleted_CacheIntercept (Object sender, Server.Application.CoverageLevelGetCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                String cacheKey = "Core.CoverageLevel." + e.Result.Id.ToString ();

                cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

            }

            return;

        }

        public delegate void CoverageLevelGetCompleted (Object sender, Server.Application.CoverageLevelGetCompletedEventArgs e);

        #endregion 

        #endregion


        #region Core - Member

        public void MemberGet (Int64 memberId, Boolean useCaching, MemberGetCompleted eventHandler) {

            String cacheKey = "Core.Member." + memberId.ToString ();

            Server.Application.MemberGetCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.MemberGetCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.MemberGetCompleted += new EventHandler<Server.Application.MemberGetCompletedEventArgs> (MemberGetCompleted_CacheIntercept);

                    client.MemberGetCompleted += new EventHandler<Server.Application.MemberGetCompletedEventArgs> (eventHandler);

                    client.MemberGetAsync (session.Token, memberId);

                }

                else {

                    eventHandler (this, e);

                }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        private void MemberGetCompleted_CacheIntercept (Object sender, Server.Application.MemberGetCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                String cacheKey = "Core.Member." + e.Result.Id.ToString ();

                cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

            }

            return;

        }

        public delegate void MemberGetCompleted (Object sender, Server.Application.MemberGetCompletedEventArgs e);


        public void MemberGetDemographics (Int64 memberId, Boolean useCaching, MemberGetDemographicsCompleted eventHandler) {

            String cacheKey = "Core.MemberDemographics." + memberId.ToString ();

            Server.Application.MemberGetDemographicsCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.MemberGetDemographicsCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.MemberGetDemographicsCompleted += new EventHandler<Server.Application.MemberGetDemographicsCompletedEventArgs> (MemberGetDemographicsCompleted_CacheIntercept);

                    client.MemberGetDemographicsCompleted += new EventHandler<Server.Application.MemberGetDemographicsCompletedEventArgs> (eventHandler);

                    client.MemberGetDemographicsAsync (session.Token, memberId);

                }

                else {

                    eventHandler (this, e);

                }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        private void MemberGetDemographicsCompleted_CacheIntercept (Object sender, Server.Application.MemberGetDemographicsCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                String cacheKey = "Core.MemberDemographics." + e.Result.Member.Id.ToString ();

                cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

                cacheKey = "Core.MemberDemographics.ByEntityId" + e.Result.Entity.Id.ToString ();

                cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);
            }

            return;

        }

        public delegate void MemberGetDemographicsCompleted (Object sender, Server.Application.MemberGetDemographicsCompletedEventArgs e);


        public void MemberGetDemographicsByEntityId (Int64 entityId, Boolean useCaching, MemberGetDemographicsByEntityIdCompleted eventHandler) {

            String cacheKey = "Core.MemberDemographicsByEntityId." + entityId.ToString ();

            Server.Application.MemberGetDemographicsByEntityIdCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.MemberGetDemographicsByEntityIdCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.MemberGetDemographicsByEntityIdCompleted += new EventHandler<Server.Application.MemberGetDemographicsByEntityIdCompletedEventArgs> (MemberGetDemographicsByEntityIdCompleted_CacheIntercept);

                    client.MemberGetDemographicsByEntityIdCompleted += new EventHandler<Server.Application.MemberGetDemographicsByEntityIdCompletedEventArgs> (eventHandler);

                    client.MemberGetDemographicsByEntityIdAsync (session.Token, entityId);

                }

                else {

                    eventHandler (this, e);

                }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        private void MemberGetDemographicsByEntityIdCompleted_CacheIntercept (Object sender, Server.Application.MemberGetDemographicsByEntityIdCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                String cacheKey = "Core.MemberDemographicsByEntityId." + e.Result.Member.Id.ToString ();

                cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

                cacheKey = "Core.MemberDemographicsByEntityId.ByEntityId" + e.Result.Entity.Id.ToString ();

                cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);
            }

            return;

        }

        public delegate void MemberGetDemographicsByEntityIdCompleted (Object sender, Server.Application.MemberGetDemographicsByEntityIdCompletedEventArgs e);


        #region Member - Enrollment

        public void MemberEnrollmentsGet (Int64 memberId, Boolean useCaching, MemberEnrollmentsGetCompleted eventHandler) {

            String cacheKey = "Core.Member.Enrollment." + memberId.ToString ();

            Server.Application.MemberEnrollmentsGetCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.MemberEnrollmentsGetCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.MemberEnrollmentsGetCompleted += new EventHandler<Server.Application.MemberEnrollmentsGetCompletedEventArgs> (MemberEnrollmentsGetCompleted_CacheIntercept);

                    client.MemberEnrollmentsGetCompleted += new EventHandler<Server.Application.MemberEnrollmentsGetCompletedEventArgs> (eventHandler);

                    client.MemberEnrollmentsGetAsync (session.Token, memberId);

                }

                else {

                    eventHandler (this, e);

                }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        private void MemberEnrollmentsGetCompleted_CacheIntercept (Object sender, Server.Application.MemberEnrollmentsGetCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                if (e.Result.Collection.Count > 0) {

                    String cacheKey = "Core.Member.Enrollment." + e.Result.Collection[0].MemberId.ToString ();

                    cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

                }

            }

            return;

        }

        public delegate void MemberEnrollmentsGetCompleted (Object sender, Server.Application.MemberEnrollmentsGetCompletedEventArgs e);


        public void MemberEnrollmentCoveragesGet (Int64 memberEnrollmentId, Boolean useCaching, MemberEnrollmentCoveragesGetCompleted eventHandler) {

            String cacheKey = "Core.Member.EnrollmentCoverage." + memberEnrollmentId.ToString ();

            Server.Application.MemberEnrollmentCoveragesGetCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.MemberEnrollmentCoveragesGetCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.MemberEnrollmentCoveragesGetCompleted += new EventHandler<Server.Application.MemberEnrollmentCoveragesGetCompletedEventArgs> (MemberEnrollmentCoveragesGetCompleted_CacheIntercept);

                    client.MemberEnrollmentCoveragesGetCompleted += new EventHandler<Server.Application.MemberEnrollmentCoveragesGetCompletedEventArgs> (eventHandler);

                    client.MemberEnrollmentCoveragesGetAsync (session.Token, memberEnrollmentId);

                }

                else {

                    eventHandler (this, e);

                }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        private void MemberEnrollmentCoveragesGetCompleted_CacheIntercept (Object sender, Server.Application.MemberEnrollmentCoveragesGetCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                if (e.Result.Collection.Count > 0) {

                    String cacheKey = "Core.Member.EnrollmentCoverage." + e.Result.Collection[0].MemberEnrollmentId.ToString ();

                    cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

                }

            }

            return;

        }

        public delegate void MemberEnrollmentCoveragesGetCompleted (Object sender, Server.Application.MemberEnrollmentCoveragesGetCompletedEventArgs e);


        public void MemberEnrollmentPcpsGet (Int64 memberEnrollmentId, Boolean useCaching, MemberEnrollmentPcpsGetCompleted eventHandler) {

            String cacheKey = "Core.Member.EnrollmentPcp." + memberEnrollmentId.ToString ();

            Server.Application.MemberEnrollmentPcpsGetCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.MemberEnrollmentPcpsGetCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.MemberEnrollmentPcpsGetCompleted += new EventHandler<Server.Application.MemberEnrollmentPcpsGetCompletedEventArgs> (MemberEnrollmentPcpsGetCompleted_CacheIntercept);

                    client.MemberEnrollmentPcpsGetCompleted += new EventHandler<Server.Application.MemberEnrollmentPcpsGetCompletedEventArgs> (eventHandler);

                    client.MemberEnrollmentPcpsGetAsync (session.Token, memberEnrollmentId);

                }

                else {

                    eventHandler (this, e);

                }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        private void MemberEnrollmentPcpsGetCompleted_CacheIntercept (Object sender, Server.Application.MemberEnrollmentPcpsGetCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                if (e.Result.Collection.Count > 0) {

                    String cacheKey = "Core.Member.EnrollmentPcp." + e.Result.Collection[0].MemberEnrollmentId.ToString ();

                    cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

                }

            }

            return;

        }

        public delegate void MemberEnrollmentPcpsGetCompleted (Object sender, Server.Application.MemberEnrollmentPcpsGetCompletedEventArgs e);

        #endregion 


        public void MemberRelationshipsGet (Int64 memberId, Boolean useCaching, MemberRelationshipsGetCompleted eventHandler) {

            String cacheKey = "Core.Member.Relationship." + memberId.ToString ();

            Server.Application.MemberRelationshipsGetCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.MemberRelationshipsGetCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.MemberRelationshipsGetCompleted += new EventHandler<Server.Application.MemberRelationshipsGetCompletedEventArgs> (MemberRelationshipsGetCompleted_CacheIntercept);

                    client.MemberRelationshipsGetCompleted += new EventHandler<Server.Application.MemberRelationshipsGetCompletedEventArgs> (eventHandler);

                    client.MemberRelationshipsGetAsync (session.Token, memberId);

                }

                else {

                    eventHandler (this, e);

                }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        private void MemberRelationshipsGetCompleted_CacheIntercept (Object sender, Server.Application.MemberRelationshipsGetCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                if (e.Result.Collection.Count > 0) {

                    String cacheKey = "Core.Member.Relationship." + e.Result.Collection[0].MemberId.ToString ();

                    cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

                }

            }

            return;

        }

        public delegate void MemberRelationshipsGetCompleted (Object sender, Server.Application.MemberRelationshipsGetCompletedEventArgs e);

        #endregion 


        #region Core - Provider

        public void ProviderGet (Int64 entityId, Boolean useCaching, ProviderGetCompleted eventHandler) {

            String cacheKey = "Core.Provider." + entityId.ToString ();

            Server.Application.ProviderGetCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.ProviderGetCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.ProviderGetCompleted += new EventHandler<Server.Application.ProviderGetCompletedEventArgs> (ProviderGetCompleted_CacheIntercept);

                    client.ProviderGetCompleted += new EventHandler<Server.Application.ProviderGetCompletedEventArgs> (eventHandler);

                    client.ProviderGetAsync (session.Token, entityId);

                }

                else {

                    eventHandler (this, e);

                }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        private void ProviderGetCompleted_CacheIntercept (Object sender, Server.Application.ProviderGetCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                String cacheKey = "Core.Provider." + e.Result.Id.ToString ();

                cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

            }

            return;

        }

        public delegate void ProviderGetCompleted (Object sender, Server.Application.ProviderGetCompletedEventArgs e);


        public void ProviderAffiliationGet (Int64 providerAffiliationId, Boolean useCaching, ProviderAffiliationGetCompleted eventHandler) {

            String cacheKey = "Core.ProviderAffiliation." + providerAffiliationId.ToString ();

            Server.Application.ProviderAffiliationGetCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.ProviderAffiliationGetCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.ProviderAffiliationGetCompleted += new EventHandler<Server.Application.ProviderAffiliationGetCompletedEventArgs> (ProviderAffiliationGetCompleted_CacheIntercept);

                    client.ProviderAffiliationGetCompleted += new EventHandler<Server.Application.ProviderAffiliationGetCompletedEventArgs> (eventHandler);

                    client.ProviderAffiliationGetAsync (session.Token, providerAffiliationId);

                }

                else {

                    eventHandler (this, e);

                }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        private void ProviderAffiliationGetCompleted_CacheIntercept (Object sender, Server.Application.ProviderAffiliationGetCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                String cacheKey = "Core.ProviderAffiliation." + e.Result.Id.ToString ();

                cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

            }

            return;

        }

        public delegate void ProviderAffiliationGetCompleted (Object sender, Server.Application.ProviderAffiliationGetCompletedEventArgs e);

        #endregion 


        #region Core - Sponsor

        public void SponsorGet (Int64 sponsorId, Boolean useCaching, SponsorGetCompleted eventHandler) {

            String cacheKey = "Core.Sponsor." + sponsorId.ToString ();

            Server.Application.SponsorGetCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.SponsorGetCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.SponsorGetCompleted += new EventHandler<Server.Application.SponsorGetCompletedEventArgs> (SponsorGetCompleted_CacheIntercept);

                    client.SponsorGetCompleted += new EventHandler<Server.Application.SponsorGetCompletedEventArgs> (eventHandler);

                    client.SponsorGetAsync (session.Token, sponsorId);

                }

                else {

                    eventHandler (this, e);

                }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        private void SponsorGetCompleted_CacheIntercept (Object sender, Server.Application.SponsorGetCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                String cacheKey = "Core.Sponsor." + e.Result.Id.ToString ();

                cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

            }

            return;

        }

        public delegate void SponsorGetCompleted (Object sender, Server.Application.SponsorGetCompletedEventArgs e);

        #endregion 


        #region Forms

        public void FormsAvailable (FormsAvailableCompleted eventHandler) {

            ClearLastException ();

            try {

                Server.Application.ApplicationClient client = ApplicationClient;

                client.FormsAvailableCompleted += new EventHandler<Server.Application.FormsAvailableCompletedEventArgs> (eventHandler);

                client.FormsAvailableAsync (session.Token);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void FormsAvailableCompleted (Object sender, Server.Application.FormsAvailableCompletedEventArgs e);

        public void FormGetByName (String formName, FormGetByNameCompleted eventHandler) {

            ClearLastException ();

            try {

                Server.Application.ApplicationClient client = ApplicationClient;

                client.FormGetByNameCompleted += new EventHandler<Server.Application.FormGetByNameCompletedEventArgs> (eventHandler);

                client.FormGetByNameAsync (session.Token, formName);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void FormGetByNameCompleted (Object sender, Server.Application.FormGetByNameCompletedEventArgs e);

        public void FormGet (Int64 formId, FormGetCompleted eventHandler) {

            ClearLastException ();

            try {

                Server.Application.ApplicationClient client = ApplicationClient;

                client.FormGetCompleted += new EventHandler<Server.Application.FormGetCompletedEventArgs> (eventHandler);

                client.FormGetAsync (session.Token, formId);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void FormGetCompleted (Object sender, Server.Application.FormGetCompletedEventArgs e);

        public void FormSubmit (Server.Application.Form form, FormSubmitCompleted eventHandler) {

            ClearLastException ();

            try {

                Server.Application.ApplicationClient client = ApplicationClient;

                client.FormSubmitCompleted += new EventHandler<Server.Application.FormSubmitCompletedEventArgs> (eventHandler);

                client.FormSubmitAsync (session.Token, form);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void FormSubmitCompleted (Object sender, Server.Application.FormSubmitCompletedEventArgs e);

        #endregion 

        
        #region Form Control Methods

        public void Form_OnDataSourceChanged (Server.Application.Form form, Guid controlId, Form_OnDataSourceChanged_Completed eventHandler) {

            ClearLastException ();

            try {

                Server.Application.ApplicationClient client = ApplicationClient;

                client.Form_OnDataSourceChangedCompleted += new EventHandler<Server.Application.Form_OnDataSourceChangedCompletedEventArgs> (eventHandler);

                client.Form_OnDataSourceChangedAsync (session.Token, form, controlId);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void Form_OnDataSourceChanged_Completed (Object sender, Server.Application.Form_OnDataSourceChangedCompletedEventArgs e);

        public void FormControl_DataBindableProperties (Server.Application.Form form, Guid controlId, FormControl_DataBindableProperties_Completed eventHandler) {

            ClearLastException ();

            try {

                Server.Application.ApplicationClient client = ApplicationClient;

                client.FormControl_DataBindablePropertiesCompleted += new EventHandler<Server.Application.FormControl_DataBindablePropertiesCompletedEventArgs> (eventHandler);

                client.FormControl_DataBindablePropertiesAsync (session.Token, form, controlId);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void FormControl_DataBindableProperties_Completed (Object sender, Server.Application.FormControl_DataBindablePropertiesCompletedEventArgs e);

        public void FormControl_DataBindingContexts (Server.Application.Form form, Guid controlId, FormControl_DataBindingContexts_Completed eventHandler) {

            ClearLastException ();

            try {

                Server.Application.ApplicationClient client = ApplicationClient;

                client.FormControl_DataBindingContextsCompleted += new EventHandler<Server.Application.FormControl_DataBindingContextsCompletedEventArgs> (eventHandler);

                client.FormControl_DataBindingContextsAsync (session.Token, form, controlId);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void FormControl_DataBindingContexts_Completed (Object sender, Server.Application.FormControl_DataBindingContextsCompletedEventArgs e);

        public void FormControl_EvaluateDataBinding (Server.Application.Form form, Guid controlId, Server.Application.FormControlDataBinding dataBinding, FormControl_EvaluateDataBinding_Completed eventHandler) {

            ClearLastException ();

            try {

                Server.Application.ApplicationClient client = ApplicationClient;

                client.FormControl_EvaluateDataBindingCompleted += new EventHandler<Server.Application.FormControl_EvaluateDataBindingCompletedEventArgs> (eventHandler);

                client.FormControl_EvaluateDataBindingAsync (session.Token, form, controlId, dataBinding);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void FormControl_EvaluateDataBinding_Completed (Object sender, Server.Application.FormControl_EvaluateDataBindingCompletedEventArgs e);

        public void FormControl_Events (Server.Application.Form form, Guid controlId, FormControl_Events_Completed eventHandler) {

            ClearLastException ();

            try {

                Server.Application.ApplicationClient client = ApplicationClient;

                client.FormControl_EventsCompleted += new EventHandler<Server.Application.FormControl_EventsCompletedEventArgs> (eventHandler);

                client.FormControl_EventsAsync (session.Token, form, controlId);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void FormControl_Events_Completed (Object sender, Server.Application.FormControl_EventsCompletedEventArgs e);


        public void FormControl_RaiseEvent (Server.Application.Form form, Guid controlId, String eventName, FormControl_RaiseEventCompleted eventHandler) {

            ClearLastException ();

            try {

                Server.Application.ApplicationClient client = ApplicationClient;

                client.FormControl_RaiseEventCompleted += new EventHandler<Server.Application.FormControl_RaiseEventCompletedEventArgs> (eventHandler);

                client.FormControl_RaiseEventAsync (session.Token, form, controlId, eventName);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void FormControl_RaiseEventCompleted (Object sender, Server.Application.FormControl_RaiseEventCompletedEventArgs e);

        public void FormControl_ValueChanged (Server.Application.Form form, Guid controlId, FormControl_ValueChangedCompleted eventHandler) {

            ClearLastException ();

            try {

                Server.Application.ApplicationClient client = ApplicationClient;

                client.FormControl_ValueChangedCompleted += new EventHandler<Server.Application.FormControl_ValueChangedCompletedEventArgs> (eventHandler);

                client.FormControl_ValueChangedAsync (session.Token, form, controlId);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void FormControl_ValueChangedCompleted (Object sender, Server.Application.FormControl_ValueChangedCompletedEventArgs e);

        #endregion


        #region Core.Work

        #region Work - Work Outcomes

        public void WorkOutcomesAvailable (Boolean useCaching, WorkOutcomesAvailableCompleted eventHandler) {

            String cacheKey = "WorkOutcomesAvailable";

            Server.Application.WorkOutcomesAvailableCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.WorkOutcomesAvailableCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.WorkOutcomesAvailableCompleted += new EventHandler<Mercury.Server.Application.WorkOutcomesAvailableCompletedEventArgs> (eventHandler);

                    client.WorkOutcomesAvailableCompleted += new EventHandler<Mercury.Server.Application.WorkOutcomesAvailableCompletedEventArgs> (WorkOutcomesAvailableCompleted_CacheIntercept);

                    client.WorkOutcomesAvailableAsync (session.Token);

                }

                else { eventHandler (this, e); }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public void WorkOutcomesAvailableCompleted_CacheIntercept (Object sender, Server.Application.WorkOutcomesAvailableCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                if (e.Result.Collection.Count > 0) {

                    String cacheKey = "WorkOutcomesAvailable";

                    cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

                    foreach (Server.Application.WorkOutcome currentWorkOutcome in e.Result.Collection) {

                        cacheKey = "WorkOutcome." + currentWorkOutcome.Id.ToString ();

                        Server.Application.WorkOutcomeGetCompletedEventArgs cacheEventArgs = new Mercury.Server.Application.WorkOutcomeGetCompletedEventArgs (new Object[] { currentWorkOutcome }, null, false, e.UserState);

                        cacheManager.CacheObject (cacheKey, cacheEventArgs, Mercury.Client.Caching.CacheDuration.Medium, true);

                    }

                }

            }

            return;

        }

        public delegate void WorkOutcomesAvailableCompleted (Object sender, Server.Application.WorkOutcomesAvailableCompletedEventArgs e);


        public void WorkOutcomeDictionary (Boolean useCaching, WorkOutcomeDictionaryCompleted eventHandler) {

            String cacheKey = "WorkOutcomeDictionary";

            Server.Application.WorkOutcomeDictionaryCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.WorkOutcomeDictionaryCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.WorkOutcomeDictionaryCompleted += new EventHandler<Mercury.Server.Application.WorkOutcomeDictionaryCompletedEventArgs> (eventHandler);

                    client.WorkOutcomeDictionaryCompleted += new EventHandler<Mercury.Server.Application.WorkOutcomeDictionaryCompletedEventArgs> (WorkOutcomeDictionaryCompleted_CacheIntercept);

                    client.WorkOutcomeDictionaryAsync (session.Token);

                }

                else { eventHandler (this, e); }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public void WorkOutcomeDictionaryCompleted_CacheIntercept (Object sender, Server.Application.WorkOutcomeDictionaryCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                if (e.Result.Dictionary.Count > 0) {

                    String cacheKey = "WorkOutcomeDictionary";

                    cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

                }

            }

            return;

        }

        public delegate void WorkOutcomeDictionaryCompleted (Object sender, Server.Application.WorkOutcomeDictionaryCompletedEventArgs e);


        public void WorkOutcomeGet (Int64 workOutcomeId, Boolean useCaching, WorkOutcomeGetCompleted eventHandler) {

            String cacheKey = "WorkOutcome." + workOutcomeId.ToString ();

            Server.Application.WorkOutcomeGetCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.WorkOutcomeGetCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.WorkOutcomeGetCompleted += new EventHandler<Mercury.Server.Application.WorkOutcomeGetCompletedEventArgs> (eventHandler);

                    client.WorkOutcomeGetCompleted += new EventHandler<Mercury.Server.Application.WorkOutcomeGetCompletedEventArgs> (WorkOutcomeGetCompleted_CacheIntercept);

                    client.WorkOutcomeGetAsync (session.Token, workOutcomeId);

                }

                else { eventHandler (this, e); }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public void WorkOutcomeGetCompleted_CacheIntercept (Object sender, Server.Application.WorkOutcomeGetCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                if (e.Result != null) {

                    String cacheKey = "WorkOutcome." + e.Result.Id.ToString ();

                    cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

                }

            }

            return;

        }

        public delegate void WorkOutcomeGetCompleted (Object sender, Server.Application.WorkOutcomeGetCompletedEventArgs e);

        #endregion 


        #region Work - Work Queues

        public void WorkQueuesAvailable (Boolean useCaching, WorkQueuesAvailableCompleted eventHandler) {

            String cacheKey = "WorkQueuesAvailable";

            Server.Application.WorkQueuesAvailableCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.WorkQueuesAvailableCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.WorkQueuesAvailableCompleted += new EventHandler<Mercury.Server.Application.WorkQueuesAvailableCompletedEventArgs> (eventHandler);

                    client.WorkQueuesAvailableCompleted += new EventHandler<Mercury.Server.Application.WorkQueuesAvailableCompletedEventArgs> (WorkQueuesAvailableCompleted_CacheIntercept);

                    client.WorkQueuesAvailableAsync (session.Token);

                }

                else { eventHandler (this, e); }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public void WorkQueuesAvailableCompleted_CacheIntercept (Object sender, Server.Application.WorkQueuesAvailableCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                if (e.Result.Collection.Count > 0) {

                    String cacheKey = "WorkQueuesAvailable";

                    cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

                    foreach (Server.Application.WorkQueue currentWorkQueue in e.Result.Collection) {

                        cacheKey = "WorkQueue." + currentWorkQueue.Id.ToString ();

                        Server.Application.WorkQueueGetCompletedEventArgs cacheEventArgs = new Mercury.Server.Application.WorkQueueGetCompletedEventArgs (new Object[] { currentWorkQueue }, null, false, e.UserState);

                        cacheManager.CacheObject (cacheKey, cacheEventArgs, Mercury.Client.Caching.CacheDuration.Medium, true);

                    }

                }

            }

            return;

        }

        public delegate void WorkQueuesAvailableCompleted (Object sender, Server.Application.WorkQueuesAvailableCompletedEventArgs e);


        public void WorkQueueDictionary (Boolean useCaching, WorkQueueDictionaryCompleted eventHandler) {

            String cacheKey = "WorkQueueDictionary";

            Server.Application.WorkQueueDictionaryCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.WorkQueueDictionaryCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.WorkQueueDictionaryCompleted += new EventHandler<Mercury.Server.Application.WorkQueueDictionaryCompletedEventArgs> (eventHandler);

                    client.WorkQueueDictionaryCompleted += new EventHandler<Mercury.Server.Application.WorkQueueDictionaryCompletedEventArgs> (WorkQueueDictionaryCompleted_CacheIntercept);

                    client.WorkQueueDictionaryAsync (session.Token);

                }

                else { eventHandler (this, e); }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public void WorkQueueDictionaryCompleted_CacheIntercept (Object sender, Server.Application.WorkQueueDictionaryCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                if (e.Result.Dictionary.Count > 0) {

                    String cacheKey = "WorkQueueDictionary";

                    cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

                }

            }

            return;

        }

        public delegate void WorkQueueDictionaryCompleted (Object sender, Server.Application.WorkQueueDictionaryCompletedEventArgs e);


        public void WorkQueueGet (Int64 workQueueId, Boolean useCaching, WorkQueueGetCompleted eventHandler) {

            String cacheKey = "WorkQueue." + workQueueId.ToString ();

            Server.Application.WorkQueueGetCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.WorkQueueGetCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.WorkQueueGetCompleted += new EventHandler<Mercury.Server.Application.WorkQueueGetCompletedEventArgs> (eventHandler);

                    client.WorkQueueGetCompleted += new EventHandler<Mercury.Server.Application.WorkQueueGetCompletedEventArgs> (WorkQueueGetCompleted_CacheIntercept);

                    client.WorkQueueGetAsync (session.Token, workQueueId);

                }

                else { eventHandler (this, e); }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public void WorkQueueGetCompleted_CacheIntercept (Object sender, Server.Application.WorkQueueGetCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                if (e.Result != null) {

                    String cacheKey = "WorkQueue." + e.Result.Id.ToString ();

                    cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

                }

            }

            return;

        }

        public delegate void WorkQueueGetCompleted (Object sender, Server.Application.WorkQueueGetCompletedEventArgs e);


        public void WorkQueueGetWork (Int64 workQueueId, WorkQueueGetWorkCompleted eventHandler) {

            ClearLastException ();

            try {

                Server.Application.ApplicationClient client = ApplicationClient;

                client.WorkQueueGetWorkCompleted += new EventHandler<Server.Application.WorkQueueGetWorkCompletedEventArgs> (eventHandler);

                client.WorkQueueGetWorkAsync (session.Token, workQueueId);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public void WorkQueueGetWork (Server.Application.WorkQueue workQueue, WorkQueueGetWorkCompleted eventHandler) {

            WorkQueueGetWork (workQueue.Id, eventHandler); 

            return;

        }

        public delegate void WorkQueueGetWorkCompleted (Object sender, Server.Application.WorkQueueGetWorkCompletedEventArgs e);

        #endregion 

        
        #region Work - Workflows

        public void WorkflowsAvailable (Boolean useCaching, WorkflowsAvailableCompleted eventHandler) {

            String cacheKey = "WorkflowsAvailable";

            Server.Application.WorkflowsAvailableCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.WorkflowsAvailableCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.WorkflowsAvailableCompleted += new EventHandler<Mercury.Server.Application.WorkflowsAvailableCompletedEventArgs> (eventHandler);

                    client.WorkflowsAvailableCompleted += new EventHandler<Mercury.Server.Application.WorkflowsAvailableCompletedEventArgs> (WorkflowsAvailableCompleted_CacheIntercept);

                    client.WorkflowsAvailableAsync (session.Token);

                }

                else { eventHandler (this, e); }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public void WorkflowsAvailableCompleted_CacheIntercept (Object sender, Server.Application.WorkflowsAvailableCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                if (e.Result.Collection.Count > 0) {

                    String cacheKey = "WorkflowsAvailable";

                    cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

                    foreach (Server.Application.Workflow currentWorkflow in e.Result.Collection) {

                        cacheKey = "Workflow." + currentWorkflow.Id.ToString ();

                        Server.Application.WorkflowGetCompletedEventArgs cacheEventArgs = new Mercury.Server.Application.WorkflowGetCompletedEventArgs (new Object[] { currentWorkflow }, null, false, e.UserState);

                        cacheManager.CacheObject (cacheKey, cacheEventArgs, Mercury.Client.Caching.CacheDuration.Medium, true);

                    }

                }

            }

            return;

        }

        public delegate void WorkflowsAvailableCompleted (Object sender, Server.Application.WorkflowsAvailableCompletedEventArgs e);


        public void WorkflowGet (Int64 workflowId, Boolean useCaching, WorkflowGetCompleted eventHandler) {

            String cacheKey = "Workflow." + workflowId.ToString ();

            Server.Application.WorkflowGetCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.WorkflowGetCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.WorkflowGetCompleted += new EventHandler<Mercury.Server.Application.WorkflowGetCompletedEventArgs> (eventHandler);

                    client.WorkflowGetCompleted += new EventHandler<Mercury.Server.Application.WorkflowGetCompletedEventArgs> (WorkflowGetCompleted_CacheIntercept);

                    client.WorkflowGetAsync (session.Token, workflowId);

                }

                else { eventHandler (this, e); }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public void WorkflowGetCompleted_CacheIntercept (Object sender, Server.Application.WorkflowGetCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                if (e.Result != null) {

                    String cacheKey = "Workflow." + e.Result.Id.ToString ();

                    cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

                }

            }

            return;

        }

        public delegate void WorkflowGetCompleted (Object sender, Server.Application.WorkflowGetCompletedEventArgs e);


        public void WorkflowGetByName (String workflowName, Boolean useCaching, WorkflowGetByNameCompleted eventHandler) {

            String cacheKey = "Workflow." + workflowName;

            Server.Application.WorkflowGetByNameCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.WorkflowGetByNameCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.WorkflowGetByNameCompleted += new EventHandler<Mercury.Server.Application.WorkflowGetByNameCompletedEventArgs> (eventHandler);

                    client.WorkflowGetByNameCompleted += new EventHandler<Mercury.Server.Application.WorkflowGetByNameCompletedEventArgs> (WorkflowGetByNameCompleted_CacheIntercept);

                    client.WorkflowGetByNameAsync (session.Token, workflowName);

                }

                else { eventHandler (this, e); }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public void WorkflowGetByNameCompleted_CacheIntercept (Object sender, Server.Application.WorkflowGetByNameCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                if (e.Result != null) {

                    String cacheKey = "Workflow." + e.Result.Name;

                    cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

                }

            }

            return;

        }

        public delegate void WorkflowGetByNameCompleted (Object sender, Server.Application.WorkflowGetByNameCompletedEventArgs e);


        public void WorkflowGetByWorkQueueId (Int64 workQueueId, WorkflowGetByWorkQueueIdCompleted eventHandler) {

            ClearLastException ();

            try {

                Server.Application.ApplicationClient client = ApplicationClient;

                client.WorkflowGetByWorkQueueIdCompleted += new EventHandler<Mercury.Server.Application.WorkflowGetByWorkQueueIdCompletedEventArgs> (eventHandler);

                client.WorkflowGetByWorkQueueIdAsync (session.Token, workQueueId);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void WorkflowGetByWorkQueueIdCompleted (Object sender, Server.Application.WorkflowGetByWorkQueueIdCompletedEventArgs e);


        public void WorkflowStart (Server.Application.WorkflowStartRequest startRequest, WorkflowStartCompleted eventHandler) {

            ClearLastException ();

            try {

                Server.Application.ApplicationClient client = ApplicationClient;

                client.WorkflowStartCompleted += new EventHandler<Mercury.Server.Application.WorkflowStartCompletedEventArgs> (eventHandler);

                client.WorkflowStartAsync (session.Token, startRequest);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void WorkflowStartCompleted (Object sender, Server.Application.WorkflowStartCompletedEventArgs e);


        public void WorkflowContinue (String workflowName, Guid workflowInstanceId, Server.Application.WorkflowUserInteractionResponseBase response, WorkflowContinueCompleted eventHandler) {

            ClearLastException ();

            try {

                Server.Application.ApplicationClient client = ApplicationClient;

                client.WorkflowContinueCompleted += new EventHandler<Mercury.Server.Application.WorkflowContinueCompletedEventArgs> (eventHandler);

                client.WorkflowContinueAsync (session.Token, workflowName, workflowInstanceId, response);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void WorkflowContinueCompleted (Object sender, Server.Application.WorkflowContinueCompletedEventArgs e);

        #endregion 


        #region Work - Work Queue Item

        public void WorkQueueItemGet (Int64 workQueueItemId, WorkQueueItemGetCompleted eventHandler) {

            ClearLastException ();

            try {

                Server.Application.ApplicationClient client = ApplicationClient;

                client.WorkQueueItemGetCompleted += new EventHandler<Server.Application.WorkQueueItemGetCompletedEventArgs> (eventHandler);

                client.WorkQueueItemGetAsync (session.Token, workQueueItemId);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void WorkQueueItemGetCompleted (Object sender, Server.Application.WorkQueueItemGetCompletedEventArgs e);


        public void WorkQueueItemGetSenders (Int64 workQueueItemId, WorkQueueItemGetSendersCompleted eventHandler) {

            ClearLastException ();

            try {

                Server.Application.ApplicationClient client = ApplicationClient;

                client.WorkQueueItemSendersGetCompleted += new EventHandler<Server.Application.WorkQueueItemSendersGetCompletedEventArgs> (eventHandler);

                client.WorkQueueItemSendersGetAsync (session.Token, workQueueItemId);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void WorkQueueItemGetSendersCompleted (Object sender, Server.Application.WorkQueueItemSendersGetCompletedEventArgs e);


        public void WorkQueueItemGetWorkflowSteps (Int64 workQueueItemId, WorkQueueItemGetWorkflowStepsCompleted eventHandler) {

            ClearLastException ();

            try {

                Server.Application.ApplicationClient client = ApplicationClient;

                client.WorkQueueItemWorkflowStepsGetCompleted += new EventHandler<Server.Application.WorkQueueItemWorkflowStepsGetCompletedEventArgs> (eventHandler);

                client.WorkQueueItemWorkflowStepsGetAsync (session.Token, workQueueItemId);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void WorkQueueItemGetWorkflowStepsCompleted (Object sender, Server.Application.WorkQueueItemWorkflowStepsGetCompletedEventArgs e);


        public void WorkQueueItemGetAssignmentHistory (Int64 workQueueItemId, WorkQueueItemGetAssignmentHistoryCompleted eventHandler) {

            ClearLastException ();

            try {

                Server.Application.ApplicationClient client = ApplicationClient;

                client.WorkQueueItemAssignmentHistoryGetCompleted += new EventHandler<Server.Application.WorkQueueItemAssignmentHistoryGetCompletedEventArgs> (eventHandler);

                client.WorkQueueItemAssignmentHistoryGetAsync (session.Token, workQueueItemId);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void WorkQueueItemGetAssignmentHistoryCompleted (Object sender, Server.Application.WorkQueueItemAssignmentHistoryGetCompletedEventArgs e);



        public void WorkQueueItemAssignTo (Int64 workQueueItemId, Int64 securityAuthorityId, String userAccountId, String userAccountName, String userDisplayName, String assignmentSource, WorkQueueItemAssignToCompleted eventHandler) {

            ClearLastException ();

            try {

                Server.Application.ApplicationClient client = ApplicationClient;

                client.WorkQueueItemAssignToCompleted += new EventHandler<Server.Application.WorkQueueItemAssignToCompletedEventArgs> (eventHandler);

                client.WorkQueueItemAssignToAsync (session.Token, workQueueItemId, securityAuthorityId, userAccountId, userAccountName, userDisplayName, assignmentSource);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void WorkQueueItemAssignToCompleted (Object sender, Server.Application.WorkQueueItemAssignToCompletedEventArgs e);


        public void WorkQueueItemSuspend (Int64 workQueueItemId, String lastStep, String nextStep, Int32 constraintDays, Int32 milestoneDays, Boolean releaseItem, WorkQueueItemSuspendCompleted eventHandler) {

            ClearLastException ();

            try {

                Server.Application.ApplicationClient client = ApplicationClient;

                client.WorkQueueItemSuspendCompleted += new EventHandler<Server.Application.WorkQueueItemSuspendCompletedEventArgs> (eventHandler);

                client.WorkQueueItemSuspendAsync (session.Token, workQueueItemId, lastStep, nextStep, constraintDays, milestoneDays, releaseItem);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void WorkQueueItemSuspendCompleted (Object sender, Server.Application.WorkQueueItemSuspendCompletedEventArgs e);


        public void WorkQueueItemClose (Int64 workQueueItemId, Int64 workOutcomeId, WorkQueueItemCloseCompleted eventHandler) {

            ClearLastException ();

            try {

                Server.Application.ApplicationClient client = ApplicationClient;

                client.WorkQueueItemCloseCompleted += new EventHandler<Server.Application.WorkQueueItemCloseCompletedEventArgs> (eventHandler);

                client.WorkQueueItemCloseAsync (session.Token, workQueueItemId, workOutcomeId);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void WorkQueueItemCloseCompleted (Object sender, Server.Application.WorkQueueItemCloseCompletedEventArgs e);


        #endregion 


        #region Work - Work Queue Items (By Views)

        public void WorkQueueItemsGetCount (ObservableCollection<Mercury.Server.Application.DataFilterDescriptor> filters, Boolean useCaching, WorkQueueItemsGetCountCompleted eventHandler) {
            
            // RESET FILTERS SO THAT NULL COLLECTIONS ARE NOT PASSED TO SERVER

            if (filters == null) { filters = new ObservableCollection<Mercury.Server.Application.DataFilterDescriptor> (); }


            // CREATE THE CACHE KEY

            String cacheKey = "Application." + session.EnvironmentId + ".Core.Work.WorkQueueItems.GetCount.ByFilters";

            foreach (Mercury.Server.Application.DataFilterDescriptor currentFilter in filters) {

                cacheKey = cacheKey + "." + currentFilter.Parameter.Name + "." + currentFilter.Parameter.Value + "." + currentFilter.Operator.ToString ();

                cacheKey = cacheKey + currentFilter.IsCaseSensitive.ToString () + "." + currentFilter.PropertyPath;

            }



            ClearLastException ();

            try {

                // TODO: ADD CACHING LATER

                Server.Application.ApplicationClient client = ApplicationClient;

                client.WorkQueueItemsGetCountCompleted += new EventHandler<Server.Application.WorkQueueItemsGetCountCompletedEventArgs>(eventHandler);

                client.WorkQueueItemsGetCountAsync (session.Token, filters);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return;

        }

        public delegate void WorkQueueItemsGetCountCompleted (Object sender, Server.Application.WorkQueueItemsGetCountCompletedEventArgs e);

        public void WorkQueueItemsGetByViewPage (Mercury.Server.Application.WorkQueueView workQueueView, ObservableCollection<Mercury.Server.Application.DataFilterDescriptor> filters, ObservableCollection<Mercury.Server.Application.DataSortDescriptor> sorts, Int32 initialRow, Int32 count, WorkQueueItemsGetByViewPageCompleted eventHandler) {

            // RESET FILTERS AND SORTS SO THAT NULL COLLECTIONS ARE NOT PASSED TO SERVER

            if (filters == null) { filters = new ObservableCollection<Mercury.Server.Application.DataFilterDescriptor> (); }

            if (sorts == null) { sorts = new ObservableCollection<Mercury.Server.Application.DataSortDescriptor> (); }


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

                Server.Application.ApplicationClient client = ApplicationClient;

                client.WorkQueueItemsGetByViewPageCompleted += new EventHandler<Server.Application.WorkQueueItemsGetByViewPageCompletedEventArgs>(eventHandler);

                client.WorkQueueItemsGetByViewPageAsync (session.Token, workQueueView, filters, sorts, initialRow, count);

            }

            catch (Exception applicationException) {

                SetLastException (applicationException);

            }

            return;

        }

        public delegate void WorkQueueItemsGetByViewPageCompleted (Object sender, Server.Application.WorkQueueItemsGetByViewPageCompletedEventArgs e);

        #endregion 

        #endregion


        #region Geographic References

        public void StateReference (Boolean useCaching, StateReferenceCompleted eventHandler) {

            String cacheKey = "StateReference";

            Server.Application.StateReferenceCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.StateReferenceCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.StateReferenceCompleted += new EventHandler<Mercury.Server.Application.StateReferenceCompletedEventArgs> (eventHandler);

                    client.StateReferenceCompleted += new EventHandler<Mercury.Server.Application.StateReferenceCompletedEventArgs> (StateReferenceCompleted_CacheIntercept);

                    client.StateReferenceAsync (session.Token);

                }

                else { eventHandler (this, e); }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public void StateReferenceCompleted_CacheIntercept (Object sender, Server.Application.StateReferenceCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                if (e.Result.ResultList.Count > 0) {

                    String cacheKey = "StateReference";

                    cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

                }

            }

            return;

        }

        public delegate void StateReferenceCompleted (Object sender, Server.Application.StateReferenceCompletedEventArgs e);


        public void CityReferenceByState (String state, Boolean useCaching, CityReferenceByStateCompleted eventHandler) {

            String cacheKey = "CityReferenceByState." + state;

            Server.Application.CityReferenceByStateCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.CityReferenceByStateCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.CityReferenceByStateCompleted += new EventHandler<Mercury.Server.Application.CityReferenceByStateCompletedEventArgs> (eventHandler);

                    client.CityReferenceByStateCompleted += new EventHandler<Mercury.Server.Application.CityReferenceByStateCompletedEventArgs> (CityReferenceByStateCompleted_CacheIntercept);

                    client.CityReferenceByStateAsync (session.Token, state);

                }

                else { eventHandler (this, e); }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public void CityReferenceByStateCompleted_CacheIntercept (Object sender, Server.Application.CityReferenceByStateCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                if (e.Result.Collection.Count > 0) {

                    String cacheKey = "CityReferenceByState." + e.Result.Collection[0].State;

                    cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

                }

            }

            return;

        }

        public delegate void CityReferenceByStateCompleted (Object sender, Server.Application.CityReferenceByStateCompletedEventArgs e);


        public void CityStateReferenceByZipCode (String zipCode, Boolean useCaching, CityStateReferenceByZipCodeCompleted eventHandler) {

            String cacheKey = "CityStateReferenceByZipCode." + zipCode;

            Server.Application.CityStateReferenceByZipCodeCompletedEventArgs e;

            ClearLastException ();

            try {

                if (!useCaching) { cacheManager.RemoveObject (cacheKey); }

                e = (Server.Application.CityStateReferenceByZipCodeCompletedEventArgs)cacheManager.GetObject (cacheKey);

                if (e == null) {

                    Server.Application.ApplicationClient client = ApplicationClient;

                    client.CityStateReferenceByZipCodeCompleted += new EventHandler<Mercury.Server.Application.CityStateReferenceByZipCodeCompletedEventArgs> (eventHandler);

                    client.CityStateReferenceByZipCodeCompleted += new EventHandler<Mercury.Server.Application.CityStateReferenceByZipCodeCompletedEventArgs> (CityStateReferenceByZipCodeCompleted_CacheIntercept);

                    client.CityStateReferenceByZipCodeAsync (session.Token, zipCode);

                }

                else { eventHandler (this, e); }

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public void CityStateReferenceByZipCodeCompleted_CacheIntercept (Object sender, Server.Application.CityStateReferenceByZipCodeCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                String cacheKey = "CityStateReferenceByZipCode." + e.Result.ZipCode;

                cacheManager.CacheObject (cacheKey, e, Mercury.Client.Caching.CacheDuration.Medium, true);

            }

            return;

        }

        public delegate void CityStateReferenceByZipCodeCompleted (Object sender, Server.Application.CityStateReferenceByZipCodeCompletedEventArgs e);

        #endregion 


        #region Searches

        public void SearchGlobal (String criteria, SearchGlobalCompleted eventHandler) {

            ClearLastException ();

            try {

                Server.Application.ApplicationClient client = ApplicationClient;

                client.SearchGlobalCompleted += new EventHandler<Server.Application.SearchGlobalCompletedEventArgs> (eventHandler);

                client.SearchGlobalAsync (session.Token, criteria);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void SearchGlobalCompleted (Object sender, Server.Application.SearchGlobalCompletedEventArgs e);


        public void SearchMember (String memberName, DateTime? birthDate, String memberId, SearchMemberCompleted eventHandler) {

            ClearLastException ();

            try {

                Server.Application.ApplicationClient client = ApplicationClient;

                client.SearchMemberCompleted += new EventHandler<Server.Application.SearchMemberCompletedEventArgs> (eventHandler);

                client.SearchMemberAsync (session.Token, memberName, birthDate, memberId);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void SearchMemberCompleted (Object sender, Server.Application.SearchMemberCompletedEventArgs e);


        public void SearchProvider (String providerName, String providerId, SearchProviderCompleted eventHandler) {

            ClearLastException ();

            try {

                Server.Application.ApplicationClient client = ApplicationClient;

                client.SearchProviderCompleted += new EventHandler<Server.Application.SearchProviderCompletedEventArgs> (eventHandler);

                client.SearchProviderAsync (session.Token, providerName, providerId);

            }

            catch (Exception serviceException) { SetLastException (serviceException); }

            return;

        }

        public delegate void SearchProviderCompleted (Object sender, Server.Application.SearchProviderCompletedEventArgs e);

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

        public Server.Application.WorkQueueViewSortDefinition CopyWorkQueueViewSortDefinition (Server.Application.WorkQueueViewSortDefinition sourceSortDefinition) {

            // MAKE COPY OF COLLECTION, NOT DIRECT ASSIGNMENT

            Server.Application.WorkQueueViewSortDefinition copiedSortDefinition = new Server.Application.WorkQueueViewSortDefinition ();


            copiedSortDefinition.WorkQueueViewId = sourceSortDefinition.WorkQueueViewId;

            copiedSortDefinition.Sequence = sourceSortDefinition.Sequence;

            copiedSortDefinition.FieldName = sourceSortDefinition.FieldName;

            copiedSortDefinition.SortDirection = sourceSortDefinition.SortDirection;


            return copiedSortDefinition;
        }

        #endregion 
        

        #region Silverlight Support

        public Color ColorFromWebColor (String webColor) {

            Color color = new Color ();

            byte a = 255;

            String htmlColor = webColor.Replace ("#", String.Empty);

            if (htmlColor.Length == 8) {

                a = System.Convert.ToByte (htmlColor.Substring (0, 2), 16);

                htmlColor = htmlColor.Substring (2, 6);

            }

            if (htmlColor.Length == 6) {

                byte r = System.Convert.ToByte (htmlColor.Substring (0, 2), 16);

                byte g = System.Convert.ToByte (htmlColor.Substring (2, 2), 16);

                byte b = System.Convert.ToByte (htmlColor.Substring (4, 2), 16);

                color = System.Windows.Media.Color.FromArgb (a, r, g, b);

            }

            return color;

        }

        public GradientStop GradientStop (String webColor, Double offset) {

            GradientStop stop = new GradientStop ();

            stop.Color = ColorFromWebColor (webColor);

            stop.Offset = offset;

            return stop;

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
