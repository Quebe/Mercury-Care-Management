using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Mercury.Silverlight.WindowManager {

    public class WindowManager {

        #region Private Properties

        private List<Window> windows = new List<Window> ();

        #endregion 


        #region Public Properties

        public List<Window> Windows { get { return windows; } }

        #endregion 


        #region Public Events

        public event EventHandler<System.EventArgs> OnWindowActivated;

        public event EventHandler<System.EventArgs> GlobalProgressBarShow;

        public event EventHandler<System.EventArgs> GlobalProgressBarHide;

        #endregion 

        
        #region Window Event Handlers

        private void Window_OnClose (Object sender, EventArgs e) {

            Window closedWindow = (Window) sender;

            if (closedWindow != null) {

                if (windows.Contains (closedWindow)) {

                    windows.Remove (closedWindow);

                    PopNextAvailableWindow ();

                }

            }

            return;

        }

        private void Window_OnMinimize (Object sender, EventArgs e) {

            PopNextAvailableWindow ();

            return;

        }

        private void Window_OnMaximize (Object sender, EventArgs e) {

            Window activeWindow = (Window) sender;

            if (activeWindow != null) {

                windows.Remove (activeWindow);

                windows.Add (activeWindow);

                PopNextAvailableWindow ();

            }

            return;

        }

        public void Window_OnGlobalProgressBarShow (object sender, EventArgs e) {

            if (GlobalProgressBarShow != null) { GlobalProgressBarShow (sender, new EventArgs ()); }

            return;

        }

        public void Window_OnGlobalProgressBarHide (object sender, EventArgs e) {

            if (GlobalProgressBarHide != null) { GlobalProgressBarHide (sender, new EventArgs ()); }

            return;
            
        }

        #endregion

        
        #region Public Methods

        public String ParametersString (Dictionary<String, Object> parameters) {

            String parametersString = String.Empty;

            foreach (String currentParameterName in parameters.Keys) {

                parametersString = parametersString + "|" + currentParameterName + "=" + Convert.ToString (parameters[currentParameterName]);

            }

            if (parametersString.Length > 0) {

                parametersString = parametersString.Substring (1, parametersString.Length - 1);

            }

            return parametersString;

        }

        public Window GetWindow (String windowType, Dictionary<String, Object> parameters) {

            Window window = null;

            foreach (Window currentWindow in windows) {

                if (currentWindow.WindowType == windowType) {

                    if (ParametersString (currentWindow.Parameters) == ParametersString (parameters)) {

                        window = currentWindow;

                        break;

                    }

                }

            }
           
            return window;

        }

        public Window OpenWindow (String windowType, Dictionary<String, Object> parameters) {

            Window window = GetWindow (windowType, parameters);

            if (window == null) {

                switch (windowType) {

                    case "Workspace.Authentication": window = new Workspace.Authentication (); break;

                    case "Workspace.GlobalSearch": window = new Workspace.GlobalSearch (); break;

                    case "Workspace.SessionInformation": window = new Workspace.SessionInformation (); break;

                    //case "Enterprise.EnterpriseManagement": window = new Enterprise.EnterpriseManagement (); break;

                    //case "Configuration.ConfigurationManagement": window = new Configuration.ConfigurationManagement (); break;

                    //case "Configuration.AuthorizedService": window = new Configuration.Windows.AuthorizedService (); break;

                    //case "Configuration.Correspondence": window = new Configuration.Windows.Correspondence (); break;

                    //case "Configuration.Workflow": window = new Configuration.Windows.Workflow (); break;

                    //case "Configuration.WorkTeam": window = new Configuration.Windows.WorkTeam (); break;

                    //case "Configuration.WorkOutcome": window = new Configuration.Windows.WorkOutcome (); break;

                    //case "Forms.FormDesigner.FormDesigner": window = new Forms.FormDesigner.FormDesigner (); break;

                    case "Workflow.Workflow": window = new Workflow.Workflow (); break;                     

                    case "Work.WorkQueueItemDetail": window = new Work.WorkQueueItemDetail (); break;

                    case "Member.MemberProfile": window = new Member.MemberProfile (); break;

                    default: window = null; break;

                }

                if (window != null) {

                    window.OnClose += new EventHandler<EventArgs> (Window_OnClose);

                    window.OnMinimize += new EventHandler<EventArgs> (Window_OnMinimize);

                    window.OnMaximize += new EventHandler<EventArgs> (Window_OnMaximize);

                    window.OnGlobalProgressBarShow += new EventHandler<EventArgs> (Window_OnGlobalProgressBarShow);

                    window.OnGlobalProgressBarHide += new EventHandler<EventArgs> (Window_OnGlobalProgressBarHide);

                    window.Parameters = parameters;

                    windows.Add (window);

                }

            }

            if ((OnWindowActivated != null) && (window != null)) {

                OnWindowActivated (window, new EventArgs ());

            }

            return window;

        }

        public void ActivateWindow (String windowHandle) {

            foreach (Window currentWindow in windows) {

                if (currentWindow.WindowHandle.ToString () == windowHandle) {

                    if (OnWindowActivated != null) {

                        OnWindowActivated (currentWindow, new EventArgs ());

                    }

                    break;

                }

            }

            return;

        }

        public void PopNextAvailableWindow () {

            Window activeWindow = null;

            for (Int32 windowIndex = windows.Count - 1; windowIndex >= 0; windowIndex--) {

                if (!windows[windowIndex].IsMinimized) {

                    activeWindow = windows[windowIndex];

                    break;

                }

            }

            if (OnWindowActivated != null) {

                OnWindowActivated (activeWindow, new EventArgs ());

            }

            return;

        }
        
        #endregion 


    }

}
