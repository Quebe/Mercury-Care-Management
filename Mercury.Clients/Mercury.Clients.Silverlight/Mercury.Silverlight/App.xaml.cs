using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Mercury.Silverlight {

    public partial class App : Application {

        #region Private Properties

        private Mercury.Client.Application mercuryApplication = null;

        private WindowManager.WindowManager windowManager = null;
        
        #endregion 

        
        #region Public Properties

        public Client.Application MercuryApplication { get { return mercuryApplication; } set { mercuryApplication = value; } }

        public WindowManager.WindowManager WindowManager { get { return windowManager; } }

        public System.Windows.Threading.Dispatcher MainDispatcher { get { return ((this.RootVisual != null) ? this.RootVisual.Dispatcher : null); } }

        public UIElement AppRootVisual { get { return this.RootVisual; } set { this.RootVisual = value; } }

        #endregion 


        #region Constructors

        public App () {

            this.Startup += this.Application_Startup;

            this.Exit += this.Application_Exit;

            this.UnhandledException += this.Application_UnhandledException;

            InitializeComponent ();

            return;

        }

        #endregion 


        #region Application Events

        private void Application_Startup (object sender, StartupEventArgs e) {

            windowManager = new Mercury.Silverlight.WindowManager.WindowManager ();

            Telerik.Windows.Controls.StyleManager.ApplicationTheme = new Telerik.Windows.Controls.Office_BlueTheme ();


            #region Set Mercury Application References

            String sessionToken = String.Empty;

            String serviceHostAddress = String.Empty;

            String serviceHostPort = String.Empty;


            foreach (String currentParameterKey in e.InitParams.Keys) {

                switch (currentParameterKey.ToUpper ()) {

                    case "SESSIONTOKEN": sessionToken = e.InitParams[currentParameterKey]; break;

                    case "SERVICEHOSTADDRESS": serviceHostAddress = e.InitParams[currentParameterKey]; break;

                    case "SERVICEHOSTPORT": serviceHostPort = e.InitParams[currentParameterKey]; break;

                    default:

                        System.Diagnostics.Debug.WriteLine ("Unknown Initialization Parameter [" + currentParameterKey + "]: " + e.InitParams[currentParameterKey]);

                        break;

                }

            }

            if (String.IsNullOrWhiteSpace (sessionToken)) {

                this.RootVisual = new Workspace.Workspace ();

            }

            else {

                mercuryApplication = new global::Mercury.Client.Application (sessionToken, serviceHostAddress, serviceHostPort, MercuryApplication_SessionInitialized);

            }            

            #endregion 
            
            return;

        }


        private void MercuryApplication_SessionInitialized (Object sender, Server.Application.SessionGetCompletedEventArgs e) {

            mercuryApplication.MainDispatcher = MainDispatcher;

            this.RootVisual = new Workspace.Workspace ();

            return;

        }

        private void Application_Exit (object sender, EventArgs e) {

            return;

        }

        private void Application_UnhandledException (object sender, ApplicationUnhandledExceptionEventArgs e) {

            System.Diagnostics.Debug.WriteLine ("Mercury Unhandled Exception Occured.");

            Exception lastException = e.ExceptionObject;

            while (lastException != null) {

                System.Diagnostics.Debug.WriteLine ("Mercury.Silverlight: " + lastException.Message);

                System.Diagnostics.Debug.WriteLine ("** Stack Trace **");

                System.Diagnostics.Debug.WriteLine (lastException.StackTrace);

                System.Diagnostics.StackTrace debugStack = new System.Diagnostics.StackTrace ();

                foreach (System.Diagnostics.StackFrame currentStackFrame in debugStack.GetFrames ()) {

                    System.Diagnostics.Debug.WriteLine ("    [" + currentStackFrame.GetMethod ().Module.Assembly.FullName + "] " + currentStackFrame.GetMethod ().Name);

                }

                lastException = lastException.InnerException;

            }


            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if (!System.Diagnostics.Debugger.IsAttached) {

                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                System.Windows.Browser.HtmlPage.Window.Alert ("Mercury Unhandled Exception Occured.");

                System.Windows.Browser.HtmlPage.Window.Alert (e.ExceptionObject.Message);

                if (e.ExceptionObject.InnerException != null) {

                    System.Windows.Browser.HtmlPage.Window.Alert (e.ExceptionObject.InnerException.Message);

                }

                System.Windows.Browser.HtmlPage.Window.Alert (e.ExceptionObject.StackTrace);

                e.Handled = true;

                Deployment.Current.Dispatcher.BeginInvoke (delegate { ReportErrorToDOM (e); });

            }

            e.Handled = true;

            return;

        }

        private void ReportErrorToDOM (ApplicationUnhandledExceptionEventArgs e) {

            try {

                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;

                errorMsg = errorMsg.Replace ('"', '\'').Replace ("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval ("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");

            }

            catch (Exception) { /* DO NOTHING */ }

            return;

        }

        #endregion 

    }

}
