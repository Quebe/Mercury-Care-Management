using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    public partial class Work : UserControl {

        #region Private Properties

        private Client.Application MercuryApplication {

            get { return ((App)Application.Current).MercuryApplication; }

            set { ((App)Application.Current).MercuryApplication = value; }

        }

        private WindowManager.WindowManager WindowManager = ((App)Application.Current).WindowManager;


        private Server.Application.WorkQueue WorkQueueSelected { get { return ((WorkQueueSelection.SelectedItem == null) ? null : (Server.Application.WorkQueue)((Telerik.Windows.Controls.RadComboBoxItem)WorkQueueSelection.SelectedItem).Tag); } }

        private Int64 WorkQueueSelectedId { get { return ((WorkQueueSelected != null) ? WorkQueueSelected.Id : 0); } }


        private Int64 queueTotalItems = 0;

        private Int64 queueAvailableItems = 0;

        private ObservableCollection<Client.Core.Work.WorkQueue> workQueuesAvailable = new ObservableCollection<Client.Core.Work.WorkQueue> ();

        #endregion 


        #region Constructors

        public Work () {

            InitializeComponent ();
            
            return;

        }

        #endregion


        #region Initialization

        public void InitializeAll () {

            MercuryApplication.WorkflowsAvailable (false, Initialize_WorkflowsAvailableCompleted); // CACHE WORKFLOWS

            return;

        }

        public void Initialize_WorkflowsAvailableCompleted (Object sender, Server.Application.WorkflowsAvailableCompletedEventArgs e) {

            // DO NOTHING RESULTS, ONLY USED FOR CACHING


            // CALL NEXT INITIALIZATIONS

            MercuryApplication.WorkQueuesAvailable (false, Initialize_WorkQueuesAvailableCompleted);

            return;

        }

        public void Initialize_WorkQueuesAvailableCompleted (Object sender, Server.Application.WorkQueuesAvailableCompletedEventArgs e) {

            if (SetExceptionMessage (e)) { return; }


            workQueuesAvailable = Client.Converters.ServerCollectionToClient.WorkQueueCollection (MercuryApplication, e.Result.Collection);


            // INITIALIZE WORK QUEUE SELECTION OPTIONS           

            WorkQueueSelection.SelectedValuePath = "Id"; // TAG WILL CONTAIN SERVER.APPLICATION.WORKQUEUE


            foreach (Server.Application.WorkQueue currentWorkQueue in e.Result.Collection) {

                if (MercuryApplication.Session.WorkQueuePermissions.ContainsKey (currentWorkQueue.Id)) {

                    Telerik.Windows.Controls.RadComboBoxItem item = new Telerik.Windows.Controls.RadComboBoxItem ();

                    item.Content = currentWorkQueue.Name;

                    item.Tag = currentWorkQueue;

                    WorkQueueSelection.Items.Add (item);

                }

            }

            if (WorkQueueSelection.Items.Count > 0) { WorkQueueSelection.SelectedIndex = 0; }


            // INITIALIZE GRID

            InitializeMyAssignedWorkGrid ();

            return;

        }

        private void InitializeMyWorkQueuesForSelection () {

            if (WorkQueueSelected == null) { return; }


            // UPDATE GET WORKFLOW

            WorkQueueGetWorkButton.Content = "Retreiving";

            WorkQueueGetWorkButton.Tag = null;

            MercuryApplication.WorkflowGet (WorkQueueSelected.WorkflowId, true, Initialize_WorkflowGetCompleted);
            

            // UPDATE MANAGE LINK

            WorkQueueManageLink.Visibility = System.Windows.Visibility.Collapsed;

            // MANAGE WORK QUEUE CAPABILITY DISABLED UNTIL DEVELOPED

            //if (MercuryApplication.Session.WorkQueuePermissions.ContainsKey (WorkQueueSelected.Id)) {

            //    if (MercuryApplication.Session.WorkQueuePermissions[WorkQueueSelected.Id] == Mercury.Server.Application.WorkQueueTeamPermission.Manage) {

            //        WorkQueueManageLink.Visibility = System.Windows.Visibility.Visible;
                    
            //        // TODO: PASS LINK IN SOME HOW

            //        // WorkQueueManageLink.HRef = @"/Application/Work/WorkQueueManagement.aspx?WorkQueueId=" + selectedWorkQueueId.ToString ();

            //    }

            //}

            return;

        }

        public void Initialize_WorkflowGetCompleted (Object sender, Server.Application.WorkflowGetCompletedEventArgs e) {

            if (SetExceptionMessage (e)) { return; }


            // ASSIGN WORKFLOW TO THE TAG PROPERTY

            WorkQueueGetWorkButton.Content = ((e.Result != null) ? e.Result.Name : "(manual)");

            WorkQueueGetWorkButton.Tag = e.Result;


            // UPDATE COUNT ON WORK QUEUE SELECTION 

            InitializeAvailableCount ();

            return;

        }

        private void InitializeAvailableCount () {

            // UPDATE COUNT

            queueTotalItems = 0;

            queueAvailableItems = 0;

            WorkQueueItemsAvailableCount.Text = "Retreiving";


            ObservableCollection<Mercury.Server.Application.DataFilterDescriptor> filters = new ObservableCollection<Mercury.Server.Application.DataFilterDescriptor> ();

            filters.Add (MercuryApplication.CreateFilterDescriptor ("WorkQueueId", Mercury.Server.Application.DataFilterOperator.IsEqualTo, WorkQueueSelectedId));

            filters.Add (MercuryApplication.CreateFilterDescriptor ("IsCompleted", Mercury.Server.Application.DataFilterOperator.IsEqualTo, false));

            MercuryApplication.WorkQueueItemsGetCount (filters, false, Initialize_WorkQueueItemsGetCount_Total_Completed);



            return;

        }

        public void Initialize_WorkQueueItemsGetCount_Total_Completed (Object sender, Server.Application.WorkQueueItemsGetCountCompletedEventArgs e) {

            if (SetExceptionMessage (e)) { return; }

            queueTotalItems = e.Result;

            WorkQueueItemsAvailableCount.Text = "Retreiving / " + queueTotalItems.ToString ();


            // RECREATE FILTERS FOR ASYNCHRONOUS CALL

            ObservableCollection<Mercury.Server.Application.DataFilterDescriptor> availableFilters = new ObservableCollection<Mercury.Server.Application.DataFilterDescriptor> ();

            availableFilters.Add (MercuryApplication.CreateFilterDescriptor ("WorkQueueId", Mercury.Server.Application.DataFilterOperator.IsEqualTo, WorkQueueSelectedId));

            availableFilters.Add (MercuryApplication.CreateFilterDescriptor ("IsCompleted", Mercury.Server.Application.DataFilterOperator.IsEqualTo, false));

            availableFilters.Add (MercuryApplication.CreateFilterDescriptor ("HasConstraintDatePassed", Mercury.Server.Application.DataFilterOperator.IsEqualTo, true));

            availableFilters.Add (MercuryApplication.CreateFilterDescriptor ("IsAssigned", Mercury.Server.Application.DataFilterOperator.IsEqualTo, false));

            availableFilters.Add (MercuryApplication.CreateFilterDescriptor ("WithinWorkTimeRestrictions", Mercury.Server.Application.DataFilterOperator.IsEqualTo, true));

            MercuryApplication.WorkQueueItemsGetCount (availableFilters, false, Initialize_WorkQueueItemsGetCount_Available_Completed);

            return;

        }

        public void Initialize_WorkQueueItemsGetCount_Available_Completed (Object sender, Server.Application.WorkQueueItemsGetCountCompletedEventArgs e) {

            if (SetExceptionMessage (e)) { return; }

            queueAvailableItems = e.Result;

            WorkQueueItemsAvailableCount.Text = queueAvailableItems.ToString () + " / " + queueTotalItems.ToString ();

            return;

        }

        private void InitializeMyAssignedWorkGrid () {

            MyAssignedWorkGrid.IsBusy = true;

            MercuryApplication.WorkQueueItemsGetByViewPage ((Server.Application.WorkQueueView)null, MyAssignedWork_ItemFilters (), null, 1, 999999, Initialize_MyAssignedWorkGrid_GetItemsCompleted);

            return;

        }

        public void Initialize_MyAssignedWorkGrid_GetItemsCompleted (Object sender, Server.Application.WorkQueueItemsGetByViewPageCompletedEventArgs e) {

            MyAssignedWorkGrid.IsBusy = false;

            if (SetExceptionMessage (e)) { return; }


            MyAssignedWorkGrid.ItemsSource = Client.Converters.ServerCollectionToClient.WorkQueueItemCollection (MercuryApplication, e.Result.Collection);

            return;

        }

        #endregion 

        
        #region Exception Handling

        private void SetExceptionMessage (String forMessage) {

            GetWorkExceptionMessagePanel.Visibility = (!String.IsNullOrEmpty (forMessage)) ? Visibility.Visible : Visibility.Collapsed;

            GetWorkExceptionMessage.Text = forMessage;

            return;

        }

        private Boolean SetExceptionMessage (System.ComponentModel.AsyncCompletedEventArgs e) {

            String message = String.Empty;

            if (e.Cancelled) { message = "Server Communication Canceled"; }

            if (e.Error != null) {

                message = ((message.Length > 0) ? message : "Server Communication Error") + ": " + e.Error.Message;

            }

            if (!String.IsNullOrEmpty (message)) { SetExceptionMessage (message); }

            return (!String.IsNullOrEmpty (message));

        }

        private Boolean SetExceptionMessage (Server.Application.ResponseBase response) {

            String message = String.Empty;

            if (response.HasException) { message = response.Exception.Message; }

            if (!String.IsNullOrEmpty (message)) { SetExceptionMessage (message); }

            return (!String.IsNullOrEmpty (message));

        }

        private Boolean SetExceptionMessage (Server.Application.ServiceException serviceException) {

            String message = String.Empty;

            if (serviceException != null) { message = serviceException.Message; }

            if (!String.IsNullOrEmpty (message)) { SetExceptionMessage (message); }

            return (!String.IsNullOrEmpty (message));

        }

        private Boolean SetExceptionMessage (Object forObject, String description) {

            String message = String.Empty;

            if (forObject == null) { message = "Unable to successfully retreive " + description + "."; }

            if (!String.IsNullOrEmpty (message)) { SetExceptionMessage (message); }

            return (!String.IsNullOrEmpty (message));

        }

        #endregion 


        #region Control Events

        private void WorkQueueSelection_SelectionChanged (object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e) {
            
            InitializeMyWorkQueuesForSelection ();

            return;

        }

        private void MyAssignedWorkRefresh_Click (object sender, RoutedEventArgs e) {

            InitializeMyAssignedWorkGrid ();

            return;
        }

        private void WorkQueueGetWorkButton_Click (object sender, RoutedEventArgs e) {

            if (WorkQueueSelection.SelectedItem != null) {

                Telerik.Windows.Controls.RadComboBoxItem selectedItem = (Telerik.Windows.Controls.RadComboBoxItem)WorkQueueSelection.SelectedItem;

                MercuryApplication.ProgressBarShow (this);

                MercuryApplication.WorkQueueGetWork ((Server.Application.WorkQueue)selectedItem.Tag, GetWorkCompleted);

            }

            return;

        }

        private void GetWorkCompleted (Object Sender, Server.Application.WorkQueueGetWorkCompletedEventArgs e) {

            if (SetExceptionMessage (e)) { return; }

            InitializeMyAssignedWorkGrid ();

            MercuryApplication.ProgressBarHide (this);

            if ((e.Result != null) && (WorkQueueSelection.SelectedItem != null)) {

                if (e.Result.WorkQueueItem != null) {

                    Telerik.Windows.Controls.RadComboBoxItem selectedItem = (Telerik.Windows.Controls.RadComboBoxItem)WorkQueueSelection.SelectedItem;

                    Server.Application.WorkQueue workQueue = (Server.Application.WorkQueue)selectedItem.Tag;

                    if (workQueue.WorkflowId != 0) {

                        Dictionary<String, Object> windowParameters = new Dictionary<String, Object> ();

                        windowParameters.Add ("WorkQueueItemId", e.Result.WorkQueueItem.Id);

                        WindowManager.OpenWindow ("Workflow.Workflow", windowParameters);

                    }

                }

                else { SetExceptionMessage ("No Work Queue Items Available in this Work Queue."); }

            }


            return;

        }

        #endregion 
        

        #region My Assigned Work Grid Events

        private ObservableCollection<Mercury.Server.Application.DataFilterDescriptor> MyAssignedWork_ItemFilters () {

            // BUILD FILTERS COLLECTION 

            ObservableCollection<Mercury.Server.Application.DataFilterDescriptor> filters = new ObservableCollection<Mercury.Server.Application.DataFilterDescriptor> ();


            filters.Add (MercuryApplication.CreateFilterDescriptor ("AssignedToSecurityAuthorityId", Mercury.Server.Application.DataFilterOperator.IsEqualTo, MercuryApplication.Session.SecurityAuthorityId));

            filters.Add (MercuryApplication.CreateFilterDescriptor ("AssignedToUserAccountId", Mercury.Server.Application.DataFilterOperator.IsEqualTo, MercuryApplication.Session.UserAccountId));

            filters.Add (MercuryApplication.CreateFilterDescriptor ("IsCompleted", Mercury.Server.Application.DataFilterOperator.IsEqualTo, false));


            return filters;

        }

        private void MyAssignedWorkGrid_RowLoaded (object sender, Telerik.Windows.Controls.GridView.RowLoadedEventArgs e) {

            //Telerik.Windows.Controls.GridView.GridViewCellBase statusCell = null;

            //Telerik.Windows.Controls.GridView.GridViewCellBase workflowCell = null;

            //Telerik.Windows.Controls.GridView.GridViewCellBase actionCell = null;


            //foreach (Telerik.Windows.Controls.GridView.GridViewCellBase currentCell in e.Row.Cells) {

            //    switch (currentCell.Column.UniqueName) {

            //        case "Workflow": workflowCell = currentCell; break;

            //        case "Action": actionCell = currentCell; break;

            //    }

            //}
            
            //if (e.Row is Telerik.Windows.Controls.GridView.GridViewRow) {

            //    if (e.DataElement is Client.Core.Work.WorkQueueItem) {

            //        Client.Core.Work.WorkQueueItem workQueueItem = (Client.Core.Work.WorkQueueItem) e.DataElement;

            //        Client.Core.Work.WorkQueue workQueue = workQueueItem.WorkQueue;

            //        if (workQueue == null) {

            //            foreach (Client.Core.Work.WorkQueue currentWorkQueue in workQueuesAvailable) {

            //                if (currentWorkQueue.Id == workQueueItem.WorkQueueId) {

            //                    workQueue = currentWorkQueue;

            //                    break;

            //                }

            //            }

            //        }
               

            //        #region Action

            //        //Telerik.Windows.Controls.RadComboBox actionSelection = new Telerik.Windows.Controls.RadComboBox ();

            //        //actionSelection.Tag = workQueueItem;

            //        //actionSelection.Items.Add ("<select>");


            //        // RELEASE ASSIGNMENT (SELF ASSIGNED OR THROUGH MANAGE)

            //        //if ((workQueueItem.AssignedToSecurityAuthorityId == MercuryApplication.Session.AuthorityId) && (workQueueItem.AssignedToUserAccountId == MercuryApplication.Session.UserAccountId)) {

            //        //    actionSelection.Items.Add ("Release");

            //        //}

            //        //if ((workQueue.WorkflowId == 0) || (MercuryApplication.SessionWorkQueueHasManagePermission (workQueueItem.WorkQueueId))) {

            //        //    actionSelection.Items.Add ("Suspend");

            //        //    actionSelection.Items.Add ("Close");

            //        //}

            //        //if (MercuryApplication.SessionWorkQueueHasManagePermission (workQueueItem.WorkQueueId)) {

            //        //    // TODO: IMPLEMENT RESET 

            //        //    // actionSelection.Items.Add ("Reset");

            //        //}

            //        //actionSelection.SelectedIndex = 0;

            //        //actionSelection.SelectionChanged += new Telerik.Windows.Controls.SelectionChangedEventHandler (ActionSelection_SelectionChanged);

            //        //actionCell.Content = actionSelection;

            //        #endregion

            //    }

            //}


            return;

        }


        private void WorkQueueItemsGrid_DetailButton_Click (object sender, RoutedEventArgs e) {

            FrameworkElement clickedObject = (FrameworkElement)sender;

            Int64 workQueueItemId = Convert.ToInt64 (clickedObject.Tag);

            Dictionary<String, Object> parameters = new Dictionary<String, Object> ();

            parameters.Add ("WorkQueueItemId", workQueueItemId);


            WindowManager.OpenWindow ("Work.WorkQueueItemDetail", parameters);

            return;
        }

        private void WorkQueueItemsGrid_NameButton_Click (object sender, RoutedEventArgs e) {

            HyperlinkButton button = (HyperlinkButton)sender;

            String itemObjectType = (String)button.TargetName;

            Int64 itemObjectId = Convert.ToInt64 (button.Tag);


            Dictionary<String, Object> windowParameters = new Dictionary<String, Object> ();

            switch (itemObjectType) {

                case "Member":

                    windowParameters.Add ("Id", itemObjectId);

                    WindowManager.OpenWindow ("Member.MemberProfile", windowParameters);

                    break;

            }

            return;

        }

        private void WorkQueueItemsGrid_WorkflowButton_Click (object sender, RoutedEventArgs e) {

            FrameworkElement clickedObject = (FrameworkElement)sender;

            Int64 workQueueItemId = Convert.ToInt64 (clickedObject.Tag);

            Dictionary<String, Object> parameters = new Dictionary<String, Object> ();

            parameters.Add ("WorkQueueItemId", workQueueItemId);


            WindowManager.OpenWindow ("Workflow.Workflow", parameters);

            return;

        }

        #endregion

    }

}
