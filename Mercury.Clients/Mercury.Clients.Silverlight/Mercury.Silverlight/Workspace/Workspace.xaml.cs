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

namespace Mercury.Silverlight.Workspace {

    public partial class Workspace : UserControl {

        #region Private Properties

        private Client.Application MercuryApplication {

            get { return ((App)Application.Current).MercuryApplication; }

            set { ((App)Application.Current).MercuryApplication = value; }

        }

        private WindowManager.WindowManager WindowManager = ((App)Application.Current).WindowManager;

        private List<Object> progessBarReferences = new List<Object> ();


        private Boolean keydownControl = false;

        private Boolean keydownShift = false;

        private Boolean keydownAlt = false;

        #endregion 
        

        #region Public Properties

        public Boolean WorkspaceVisible { get { return LayoutRoot.RowDefinitions[1].Height.Value != 0; } }

        #endregion 


        #region Constructors

        public Workspace () {

            InitializeComponent ();


            WindowManager.OnWindowActivated += new EventHandler<EventArgs> (WindowManager_OnWindowActivated);


            if (MercuryApplication != null) {

                ShowWorkspace ();

                WireEvents ();

            }

            else {

                WindowManager.Window authenticationWindow = WindowManager.OpenWindow ("Workspace.Authentication", new Dictionary<String, Object> ());

                authenticationWindow.OnClose += new EventHandler<EventArgs>(AuthenticationWindow_OnClose);

            }

            return;

        }

        #endregion 


        #region Initializations

        private void AuthenticationWindow_OnClose (Object sender, EventArgs e) {

            if (MercuryApplication != null) {

                Work.InitializeAll ();

                ShowWorkspace ();

                WireEvents ();

            }

            else {

                WindowManager.Window authenticationWindow = WindowManager.OpenWindow ("Workspace.Authentication", new Dictionary<String, Object> ());

                authenticationWindow.OnClose += new EventHandler<EventArgs> (AuthenticationWindow_OnClose);

            }

            return;

        }

        private void WireEvents () {

            MercuryApplication.GlobalProgressBarShow += new EventHandler<EventArgs> (WindowManager_GlobalProgressBarShow);

            MercuryApplication.GlobalProgressBarHide += new EventHandler<EventArgs> (WindowManager_GlobalProgressBarHide);


            // WindowManager.OnWindowActivated += new EventHandler<EventArgs> (WindowManager_OnWindowActivated); // MOVED TO CONSTRUCTOR

            WindowManager.GlobalProgressBarShow += new EventHandler<EventArgs> (WindowManager_GlobalProgressBarShow);

            WindowManager.GlobalProgressBarHide += new EventHandler<EventArgs> (WindowManager_GlobalProgressBarHide);

            this.KeyDown += new KeyEventHandler (Workspace_KeyDown);

            this.KeyUp += new KeyEventHandler (Workspace_KeyUp);
            

            return;
            
        }

        #endregion


        #region Application Bar Properties and Events

        public void SetApplicationButtonImage (String source) {

            ApplicationButtonImage.Source = new System.Windows.Media.Imaging.BitmapImage (new Uri ("../Images/Common32/" + source + ".png", UriKind.Relative));

            return;

        }

        public void SetApplicationTitle (String title) {

            // ApplicationTitle.Text = title;

            ApplicationTitle.Content = title;

            return;

        }

        private void ApplicationTitle_Click (object sender, RoutedEventArgs e) {

            WindowManager.OpenWindow ("Workspace.SessionInformation", new Dictionary<String, Object> ());

            return;

        }

        private void ApplicationLinkHome_Click (object sender, RoutedEventArgs e) {

            ShowWorkspace ();

            return;

        }

        private void ApplicationLinkSearch_Click (object sender, RoutedEventArgs e) {

            WindowManager.OpenWindow ("Workspace.GlobalSearch", new Dictionary<String, Object> ());

            return;
        }

        #endregion 


        #region Application and Window Manager Events

        private void Workspace_KeyDown (object sender, KeyEventArgs e) {

            if (e.Key == Key.Ctrl) { keydownControl = true; }

            if (e.Key == Key.Shift) { keydownShift = true; }

            if (e.Key == Key.Alt) { keydownAlt = true; }

            return;

        }

        private void Workspace_KeyUp (object sender, KeyEventArgs e) {

            Boolean keyHandled = false;


            if (e.Key == Key.Ctrl) { keydownControl = false; }

            if (e.Key == Key.Shift) { keydownShift = false; }

            if (e.Key == Key.Alt) { keydownAlt = false; }


            keyHandled = ((e.Key == Key.Ctrl) || (e.Key == Key.Shift) || (e.Key == Key.Alt));


            if ((e.Key == Key.Escape) && (WindowPreview.Visibility == Visibility.Visible)) {

                WindowPreview.WindowClose_Click (null, null);

                keyHandled = true;

            }


            if (!keyHandled) {

                if ((keydownControl) && (!keydownAlt) && (!keydownShift)) { // ONLY CONTROL KEY IS DOWN

                    #region CONTROL KEY (KEY UP)

                    if (e.Key == Key.Unknown) {

                        switch (e.PlatformKeyCode) {

                            case 192:

                                this.Dispatcher.BeginInvoke (delegate { ApplicationSwitchWindows_Click (null, null); });

                                keyHandled = true;

                                break;

                        }

                    }

                    else {

                        switch (e.Key) {

                            case Key.Tab:

                                break;

                            case Key.D4:

                                if ((ActiveWindowContent.Child != null) && (LayoutRoot.RowDefinitions[1].Height == new GridLength (0))) {

                                    if (ActiveWindowContent.Child is Silverlight.WindowManager.Window) {

                                        Silverlight.WindowManager.Window activeWindow = (Silverlight.WindowManager.Window)ActiveWindowContent.Child;

                                        activeWindow.WindowCommand_Close ();

                                    }

                                }

                                break;

                            case Key.Q:

                                WindowManager.OpenWindow ("Workspace.GlobalSearch", new Dictionary<String, Object> ());

                                keyHandled = true;

                                break;

                        }

                    }

                    #endregion

                }

                else if ((keydownControl) && (!keydownAlt) && (keydownShift)) { // CONTROL-SHIFT (ONLY)

                    #region CONTROL-SHIFT (KEY UP)

                    if (e.Key == Key.Unknown) {

                        switch (e.PlatformKeyCode) {

                            case 192:

                                this.Dispatcher.BeginInvoke (delegate { ApplicationShowWorkspace_Click (null, null); });

                                keyHandled = true;

                                break;

                        }

                    }

                    else {

                        switch (e.Key) {

                            case Key.Tab:

                                break;

                        }

                    }

                    #endregion

                }

            }



            return;

        }

        public void ActiveWindowScreenCapture () {

            if (ActiveWindowContent.Child != null) {

                Silverlight.WindowManager.Window currentChild = (Silverlight.WindowManager.Window)ActiveWindowContent.Child;

                currentChild.ScreenCapture = new System.Windows.Media.Imaging.WriteableBitmap (ActiveWindowContent.Child, null);

            }

            return;

        }

        public void ShowWorkspace () {

            ActiveWindowScreenCapture ();

            SetApplicationButtonImage ("Mercury");

            SetApplicationTitle (MercuryApplication.Session.UserDisplayName + " [" + MercuryApplication.Session.EnvironmentName + "]");

            LayoutRoot.RowDefinitions[1].Height = new GridLength (1, GridUnitType.Star); // SHOW WORKSPACE

            LayoutRoot.RowDefinitions[2].Height = new GridLength (0); // HIDE ACTIVE WINDOW, SET WORKSPACE ACTIVE

            if (ActiveWindowContent.Child != null) {

                if (ActiveWindowContent.Child is Silverlight.WindowManager.Window) {

                    ((Silverlight.WindowManager.Window)ActiveWindowContent.Child).WindowCommand_Deactivated ();

                }

            }

            ActiveWindowContent.Child = null;

            return;

        }

        public void ShowActiveWindow () {

            ActiveWindowScreenCapture ();

            LayoutRoot.RowDefinitions[1].Height = new GridLength (0); // HIDE WORKSPACE

            LayoutRoot.RowDefinitions[2].Height = new GridLength (1, GridUnitType.Star); // SHOW ACTIVE WINDOW, SET WORKSPACE INACTIVE

            if (ActiveWindowContent.Child != null) {

                if (ActiveWindowContent.Child is Silverlight.WindowManager.Window) {

                    ((Silverlight.WindowManager.Window)ActiveWindowContent.Child).WindowCommand_Activated ();

                }

            }

            return;

        }


        private void WindowManager_OnWindowActivated (Object sender, EventArgs e) {

            ActiveWindowScreenCapture ();

            if (sender != null) {

                // DEACTIVATE THE CURRENT WINDOW BEFORE ACTIVATING THE NEW WINDOW (IN SHOW ACTIVE WINDOW FUNCTION)

                if (ActiveWindowContent.Child != null) {

                    if (ActiveWindowContent.Child is Silverlight.WindowManager.Window) {

                        ((Silverlight.WindowManager.Window)ActiveWindowContent.Child).WindowCommand_Deactivated ();

                    }

                }

                ActiveWindowContent.Child = ((Silverlight.WindowManager.Window)sender);

                ShowActiveWindow ();

            }

            else { ShowWorkspace (); }

            return;

        }

        private void WindowManager_GlobalProgressBarShow (Object sender, EventArgs e) {

            ((App)Application.Current).MainDispatcher.BeginInvoke (delegate {

                progessBarReferences.Add (sender);

                ToolTipService.SetToolTip (ApplicationStatusProgressBar, Convert.ToString (sender));

                ApplicationStatusProgressBar.Visibility = Visibility.Visible;

            });

            return;

        }

        private void WindowManager_GlobalProgressBarHide (Object sender, EventArgs e) {

            ((App)Application.Current).MainDispatcher.BeginInvoke (delegate {

                progessBarReferences.Remove (sender);

                if (progessBarReferences.Count > 0) {

                    ToolTipService.SetToolTip (ApplicationStatusProgressBar, Convert.ToString (progessBarReferences[0]));

                }

                ApplicationStatusProgressBar.Visibility = (progessBarReferences.Count == 0) ? Visibility.Collapsed : Visibility.Visible;

            });


#if DEBUG

            // BELOW IS USED WHEN TRACKING DOWN OPEN INSTANCES, ONLY UNCOMMENTED WHEN DEBUGGING PROGRESS BAR ISSUES

            //foreach (Object currentReference in progessBarReferences) {

            //    if (currentReference.ToString () != sender.ToString ()) {

            //        System.Diagnostics.Debug.WriteLine ("Global Progress Bar Open Reference: " + currentReference.ToString ());

            //    }

            //}

#endif

            return;

        }


        private void ApplicationMenu_ItemClick (object sender, Telerik.Windows.RadRoutedEventArgs e) {

            Telerik.Windows.Controls.RadMenuItem clickedItem = (Telerik.Windows.Controls.RadMenuItem)e.OriginalSource;

            if (clickedItem == null) { return; }


            switch (clickedItem.Header.ToString ()) {

                case "Global Search Query (Ctrl+Q)":

                    WindowManager.OpenWindow ("Workspace.GlobalSearch", new Dictionary<String, Object> ());

                    break;

                case "Session Information":

                    WindowManager.OpenWindow ("Workspace.SessionInformation", new Dictionary<String, Object> ());

                    break;

            }

            return;

        }

        private void ApplicationShowWorkspace_Click (object sender, RoutedEventArgs e) {

            ShowWorkspace ();

            return;

        }

        private void ApplicationSwitchWindows_Click (object sender, RoutedEventArgs e) {

            ActiveWindowScreenCapture ();

            WindowPreview.Visibility = Visibility.Visible;

            WindowPreview.UpdatePreviews ();

            return;

        }

        private void ApplicationSwitchWindows_DropDownOpened (object sender, RoutedEventArgs e) {

            while (ApplicationSwitchWindowsContextMenu.Items.Count > 2) {

                ApplicationSwitchWindowsContextMenu.Items.RemoveAt (2);

            }

            foreach (Silverlight.WindowManager.Window currentWindow in WindowManager.Windows) {

                Telerik.Windows.Controls.RadMenuItem windowMenuItem = new Telerik.Windows.Controls.RadMenuItem ();

                windowMenuItem.Header = currentWindow.Title;

                windowMenuItem.Tag = currentWindow.WindowHandle;

                ApplicationSwitchWindowsContextMenu.Items.Add (windowMenuItem);

            }

            ApplicationSwitchWindowsContextMenuWorkspaceSeparator.Visibility = (ApplicationSwitchWindowsContextMenu.Items.Count > 2) ? Visibility.Visible : Visibility.Collapsed;

            return;

        }

        private void ApplicationSwitchWindowsContextMenu_ItemClick (object sender, Telerik.Windows.RadRoutedEventArgs e) {

            if (e.Source is Telerik.Windows.Controls.RadMenuItem) {

                Telerik.Windows.Controls.RadMenuItem clickedItem = (Telerik.Windows.Controls.RadMenuItem)e.Source;

                if (clickedItem.Tag == null) {

                    ApplicationShowWorkspace_Click (sender, e);

                }

                else {

                    WindowManager.ActivateWindow (Convert.ToString (clickedItem.Tag));

                }

            }

            return;

        }

        #endregion 


    }

}
