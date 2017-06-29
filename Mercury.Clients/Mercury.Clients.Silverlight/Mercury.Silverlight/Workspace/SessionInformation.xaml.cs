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

    public partial class SessionInformation : WindowManager.Window {

        #region Private Properties

        private Client.Application MercuryApplication = ((App) Application.Current).MercuryApplication;

        private WindowManager.WindowManager WindowManager = ((App) Application.Current).WindowManager;

        #endregion         
        

        #region Public Properties

        public override string WindowType { get { return "Workspace.SessionInformation"; } }

        #endregion 


        #region Constructors

        public SessionInformation () {

            InitializeComponent ();

            title = "Session Information: " + MercuryApplication.Session.SecurityAuthorityName + "." + MercuryApplication.Session.UserAccountName;

            WindowTitle.Text = Title;

            InitializeAll ();

            return;

        }

        private void InitializeAll () {

            // TODO


            // DATA BIND WORK QUEUES INTO WORK QUEUES SECTION

            //MercuryApplication.WorkQueuesAvailable (true, InitializeWorkQueuesAvailableCompleted);

            // DATA BIND WORK TEAMS INTO WORK TEAMS SECTION

            //MercuryApplication.WorkTeamsForSession (true, InitializeWorkTeamsAvailableCompleted);

            // MAP SESSION CONNECTION INFORMATION INTO SESSION CONNECTION INFORMATION SECTION
            
            SecurityAuthorityId.Text = MercuryApplication.Session.SecurityAuthorityId.ToString ();

            SecurityAuthorityName.Text = MercuryApplication.Session.SecurityAuthorityName;

            SecurityAuthorityType.Text = MercuryApplication.Session.SecurityAuthorityType.ToString ();


            UserAccountName.Text = MercuryApplication.Session.UserAccountName;

            UserAccountId.Text = MercuryApplication.Session.UserAccountId;

            UserDisplayName.Text = MercuryApplication.Session.UserDisplayName;


            EnvironmentName.Text = MercuryApplication.Session.EnvironmentName;

            //ClientVersion.Text = MercuryApplication.Version;

            ServerVersion.Text = "Retreiving ...";

            MercuryApplication.VersionServer (true, VersionServerCompleted);



            #region Enterprise Permissions

            if (MercuryApplication.Session.EnterprisePermissionSet.Count > 0) {

                // BIND ENTERPRISE PERMISSIONS INTO ENTERPRISE PERMISSIONS SECTION

                EnterprisePermissionsGrid.ItemsSource = MercuryApplication.Session.EnterprisePermissionSet;

            }

            else {

                // IF NO ENTERPRISE PERMISSIONS SET ENTERPIRISE PERMISSIONS TO "NO PERMISSIONS GRANTED", "NO PERMISSIONS GRANTED"

                EnterprisePermissionsGrid.ItemsSource = new Dictionary<String, String> () { { "No Permissions Granted", "No Permissions Granted" } };

            }

            #endregion


            #region Environment Permissions

            if (MercuryApplication.Session.EnvironmentPermissionSet.Count > 0) {

                // BIND ENVIRONMENT PERMISSIONS INTO ENVIRONMENT PERMISSIONS SECTION

                EnvironmentPermissionsGrid.ItemsSource = MercuryApplication.Session.EnvironmentPermissionSet;

            }

            else {

                // IF NO ENVIRONMENT PERMISSIONS SET ENVIRONMENT PERMISSIONS TO "NO PERMISSIONS GRANTED", "NO PERMISSIONS GRANTED"

                EnvironmentPermissionsGrid.ItemsSource = new Dictionary<String, String> () { { "No Permissions Granted", "No Permissions Granted" } };

            }

            #endregion


            #region Security Group Membership

            if (MercuryApplication.Session.GroupMembership.Count > 0) {

                // BIND SECURITY GROUP MEMBERSHIPS INTO SECURITY GROUP MEMBERSHIP SECTION

                SecurityGroupMembershipGrid.ItemsSource = MercuryApplication.Session.GroupMembership;

            }

            else {

                // IF NO SECURITY GROUP MEMBERSHIPS, SET SECURITY GROUP MEMBERSHIPS TO "NO GROUP MEMBERSHIPS"

                SecurityGroupMembershipGrid.ItemsSource = new System.Collections.ObjectModel.ObservableCollection<String> () { "No Group Membership" };

            }

            #endregion


            #region Role Membership

            if (MercuryApplication.Session.RoleMembership.Count > 0) {

                // BIND ROLE MEMBERSHIPS INTO ROLE MEMBERSHIP SECTION

                RoleMembershipGrid.ItemsSource = MercuryApplication.Session.RoleMembership;

            }

            else {

                // IF NO ROLE MEMBERSHIPS, SET ROLE MEMBERSHIPS TO "NO ROLE MEMBERSHIPS"

                RoleMembershipGrid.ItemsSource = new System.Collections.ObjectModel.ObservableCollection<String> () { "No Role Memberships" };

            }

            #endregion


            return;

        }

        private void InitializeWorkTeamsAvailableCompleted (Object sender, Server.Application.WorkTeamsForSessionCompletedEventArgs e) {

            // TODO

            //System.Collections.ObjectModel.ObservableCollection<Client.Core.Work.WorkTeam> clientWorkTeams = Client.Converters.ObjectServerToClient.WorkTeamCollection (MercuryApplication, e.Result.Collection);

            //WorkTeamsGrid.ItemsSource = clientWorkTeams;

            return;

        }

        private void InitializeWorkQueuesAvailableCompleted (Object sender, Server.Application.WorkQueuesAvailableCompletedEventArgs e) {

            // TODO

            //System.Collections.ObjectModel.ObservableCollection<Client.Core.Work.WorkQueue> clientWorkQueues = Client.Converters.ObjectServerToClient.WorkQueueCollection (MercuryApplication, e.Result.Collection);

            //WorkQueuesGrid.ItemsSource = clientWorkQueues;

            return;

        }

        private void VersionServerCompleted (Object sender, Server.Application.VersionServerCompletedEventArgs e) {

            ServerVersion.Text = e.Result;

            return;

        }

        private void SessionInformationScrollViewer_MouseWheel (object sender, MouseWheelEventArgs e) {

            // ENABLE MOUSE WHEEL SCROLLING

            SessionInformationScrollViewer.ScrollToVerticalOffset (SessionInformationScrollViewer.VerticalOffset - e.Delta);

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
