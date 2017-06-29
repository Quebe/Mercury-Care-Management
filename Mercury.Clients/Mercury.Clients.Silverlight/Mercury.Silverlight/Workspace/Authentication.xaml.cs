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

    public partial class Authentication : WindowManager.Window {

        #region Private Properties

        private Client.Application MercuryApplication {

            get { return ((App)Application.Current).MercuryApplication; }

            set { ((App)Application.Current).MercuryApplication = value; }

        }

        private WindowManager.WindowManager WindowManager = ((App)Application.Current).WindowManager;

        #endregion 


        #region Public Properties

        public override string WindowType { get { return "Workspace.Authentication"; } }

        #endregion 
        

        #region Constructors

        public Authentication () {

            InitializeComponent ();

            title = "Authentication";

            WindowTitle.Text = Title;

            InitializeAll ();

            Connect_Click (null, null);

            return;

        }

        private void InitializeAll () {

            HostPortAddress.NumberFormatInfo.NumberDecimalDigits = 0;

            HostPortAddress.NumberFormatInfo.NumberGroupSeparator = String.Empty;

            HostPortAddress.ShowButtons = false;


#if DEBUG

            HostServerName.Value = "localhost";

            HostPortAddress.Value = 10080;

#endif

            return;

        }

        #endregion
        

        #region Events

        private void MercuryApplication_AuthenticateCompleted (Object sender, Server.Enterprise.AuthenticateWindowsCompletedEventArgs e) {

            ConnectProgressBar.Visibility = System.Windows.Visibility.Collapsed;

            ConnectProgressText.Visibility = System.Windows.Visibility.Collapsed;


            if (e.Error != null) {

                AuthenticationResponse.Text = e.Error.Message;

                return;

            }

            Server.Enterprise.AuthenticationResponse response = e.Result;

            if ((response.AuthenticationError == Server.Enterprise.AuthenticationError.NoError) && (!String.IsNullOrWhiteSpace (response.Token))) {

                WindowCommand_Close ();

                return;

            }

            else {

                if (response.AuthenticationError == Server.Enterprise.AuthenticationError.MustSelectEnvironment) {

                    AuthenticationResponse.Text = String.Empty;

                    EnvironmentSelection.Items.Clear ();

                    String[] availableEnvironments = response.Environments.Split (';');

                    foreach (String currentEnvironment in availableEnvironments) {

                        Telerik.Windows.Controls.RadComboBoxItem item = new Telerik.Windows.Controls.RadComboBoxItem ();

                        item.Content = currentEnvironment;

                        EnvironmentSelection.Items.Add (item);

                    }

                    if (EnvironmentSelection.Items.Count == 0) {

                        AuthenticationResponse.Text = "Unable to access any environments with this account. You will not be able to use Mercury.";

                    }

                    else if (EnvironmentSelection.Items.Count == 1) {

                        EnvironmentSelection.SelectedIndex = 0;

                        Connect_Click (null, null);

                    }

                    else {

                        AuthenticationResponse.Text = "Multiple environments available. Please, select an environment.";

                        EnvironmentSelection.SelectedIndex = 0;

                    }

                }
                    
                else {

                    AuthenticationResponse.Text = response.AuthenticationError.ToString () + " ";

                    if (response.HasException) { AuthenticationResponse.Text += response.Exception.Message; }

                }

            }

            return;

        }

        private void Connect_Click (object sender, RoutedEventArgs e) {

            if (ConnectProgressBar.Visibility == System.Windows.Visibility.Visible) { return; }


            MercuryApplication = new Client.Application (Client.Enumerations.ServiceBindingType.Silverlight, HostServerName.Value.ToString (), Convert.ToInt32 (HostPortAddress.Value), true);

            MercuryApplication.MainDispatcher = ((App)Application.Current).MainDispatcher;


            String environmentName = ((Telerik.Windows.Controls.RadComboBoxItem) EnvironmentSelection.SelectedValue).Content.ToString ();

            MercuryApplication.Authenticate (environmentName, MercuryApplication_AuthenticateCompleted);


            ConnectProgressBar.Visibility = System.Windows.Visibility.Visible;

            ConnectProgressText.Visibility = System.Windows.Visibility.Visible;

            return;

        }

        #endregion 


        #region Window Events

        private void WindowClose_Click (object sender, RoutedEventArgs e) {

            WindowCommand_Close ();

            return;

        }

        private void WindowMinimize_Click (object sender, RoutedEventArgs e) {

            WindowCommand_Minimize ();

            return;

        }

        #endregion 

    }

}
