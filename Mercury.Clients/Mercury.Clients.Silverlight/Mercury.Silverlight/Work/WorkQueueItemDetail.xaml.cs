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

namespace Mercury.Silverlight.Work {
    public partial class WorkQueueItemDetail : WindowManager.Window {

        #region Private Properties

        private Client.Application MercuryApplication = ((App) Application.Current).MercuryApplication;

        private WindowManager.WindowManager WindowManager = ((App) Application.Current).WindowManager;


        private Int64 workQueueItemId = 0;

        private Client.Core.Work.WorkQueueItem workQueueItem = null;

        #endregion


        #region Public Properties

        public override string WindowType { get { return "Work.WorkQueueItemDetail"; } }

        public override Dictionary<String, Object> Parameters {

            get { return base.Parameters; }

            set {

                base.Parameters = value;

                if (Parameters.ContainsKey ("WorkQueueItemId")) {

                    workQueueItemId = Convert.ToInt64 (Parameters["WorkQueueItemId"]);

                }

                if (workQueueItemId != 0) { LoadWorkQueueItem (workQueueItemId); }

                else { WindowTitleUpdate ("Invalid or no Work Queue Item Id Specified."); }

            }

        }

        #endregion


        #region Constructors

        public WorkQueueItemDetail () {

            InitializeComponent ();

            title = "";

            WindowTitle.Text = title;

            return;

        }

        private void WorkQueueItemDetailScrollViewer_MouseWheel (object sender, MouseWheelEventArgs e) {

            // ENABLE MOUSE WHEEL SCROLLING

            WorkQueueItemDetailScrollViewer.ScrollToVerticalOffset (WorkQueueItemDetailScrollViewer.VerticalOffset - e.Delta);

            return;

        }

        #endregion


        //#region Exception Handling

        //private void SetExceptionMessage (String forMessage) {

        //    ExceptionContainer.Visibility = (!String.IsNullOrEmpty (forMessage)) ? Visibility.Visible : Visibility.Collapsed;

        //    ExceptionMessage.Text = forMessage;

        //    return;

        //}

        //private Boolean SetExceptionMessage (System.ComponentModel.AsyncCompletedEventArgs e) {

        //    String message = String.Empty;

        //    if (e.Cancelled) { message = "Server Communication Canceled"; }

        //    if (e.Error != null) {

        //        message = ((message.Length > 0) ? message : "Server Communication Error") + ": " + e.Error.Message;

        //    }

        //    if (!String.IsNullOrEmpty (message)) { SetExceptionMessage (message); }

        //    return (!String.IsNullOrEmpty (message));

        //}

        //private Boolean SetExceptionMessage (Server.Application.ResponseBase response) {

        //    String message = String.Empty;

        //    if (response.HasException) { message = response.Exception.Message; }

        //    if (!String.IsNullOrEmpty (message)) { SetExceptionMessage (message); }

        //    return (!String.IsNullOrEmpty (message));

        //}

        //private Boolean SetExceptionMessage (Server.Application.ServiceException serviceException) {

        //    String message = String.Empty;

        //    if (serviceException != null) { message = serviceException.Message; }

        //    if (!String.IsNullOrEmpty (message)) { SetExceptionMessage (message); }

        //    return (!String.IsNullOrEmpty (message));

        //}

        //private Boolean SetExceptionMessage (Object forObject, String description) {

        //    String message = String.Empty;

        //    if (forObject == null) { message = "Unable to successfully retreive " + description + "."; }

        //    if (!String.IsNullOrEmpty (message)) { SetExceptionMessage (message); }

        //    return (!String.IsNullOrEmpty (message));

        //}

        //#endregion


        #region Window Events

        private void WindowRefresh_Click (object sender, RoutedEventArgs e) {

            LoadWorkQueueItem (workQueueItemId);

            return;

        }

        private void WindowClose_Click (object sender, RoutedEventArgs e) {

            WindowCommand_Close ();

            return;

        }

        private void WindowMinimize_Click (object sender, RoutedEventArgs e) {

            WindowCommand_Minimize ();

            return;

        }

        private void WindowTitleUpdate (String forTitle) {

            String workingTitle = "Work Queue Item Information";

            if (!String.IsNullOrEmpty (forTitle)) {

                workingTitle = workingTitle + " - " + forTitle;

            }

            else { workingTitle = workingTitle + " - Untitled"; }


            title = workingTitle;

            WindowTitle.Text = Title;

            return;

        }

        #endregion


        #region Initialization

        private Int64 LoadWorkQueueItem (Int64 workQueueItemId) {

            SetExceptionMessage (String.Empty);

            GlobalProgressBarShow ();

            WindowTitleUpdate ("Loading");

            MercuryApplication.WorkQueueItemGet (workQueueItemId, InitializeWorkQueueItem);

            return workQueueItemId;

        }

        private void InitializeWorkQueueItem (Object sender, Server.Application.WorkQueueItemGetCompletedEventArgs e) {

            GlobalProgressBarHide ();

            if ((!SetExceptionMessage (e)) && (!SetExceptionMessage (e.Result, "Work Queue Item"))) {

                workQueueItem = new Mercury.Client.Core.Work.WorkQueueItem (MercuryApplication, e.Result);

                InitializeAll ();

            }

            return;

        }

        private void InitializeAll () {

            // SET WINDOW TITILE

            WindowTitleUpdate ("[" + workQueueItem.ObjectType + "] " + workQueueItem.Description + " : " + workQueueItem.ItemGroupKey + " (" + workQueueItemId.ToString () + ")");


            #region Work Queue Item Information

            // MAP WORK QUEUE ITEM INFORMATION INTO WORK QUEUE ITEM SECTION

            Id.Text = workQueueItemId.ToString ();

            Item.Text = workQueueItem.Description;

            Type.Text = workQueueItem.ObjectType;

            Group.Text = workQueueItem.ItemGroupKey;

            // DATA BIND WORK QUEUE ITEM WORK QUEUE NAME INTO WORK QUEUE TEXT BOX

            WorkQueue.SetBinding (TextBox.TextProperty, MercuryApplication.PropertyDataBinding ("WorkQueue.Name", workQueueItem, System.Windows.Data.BindingMode.OneWay));

            Added.Text = (workQueueItem.AddedDate == null) ? "" : workQueueItem.AddedDate.ToString ("MM/dd/yyyy");

            Worked.Text = (workQueueItem.LastWorkedDate == null) ? "" : Convert.ToDateTime (workQueueItem.LastWorkedDate).ToString ("MM/dd/yyyy");

            Constraint.Text = (workQueueItem.ConstraintDate == null) ? "" : workQueueItem.ConstraintDate.ToString ("MM/dd/yyyy");

            Milestone.Text = (workQueueItem.MilestoneDate == null) ? "" : workQueueItem.MilestoneDate.ToString ("MM/dd/yyyy");

            Threshold.Text = (workQueueItem.ThresholdDate == null) ? "" : workQueueItem.ThresholdDate.ToString ("MM/dd/yyyy");

            DueDate.Text = (workQueueItem.DueDate == null) ? "" : workQueueItem.DueDate.ToString ("MM/dd/yyyy");

            Completion.Text = (workQueueItem.CompletionDate == null) ? "" : Convert.ToDateTime (workQueueItem.CompletionDate).ToString ("MM/dd/yyyy");

            // DATA BIND WORK QUEUE ITEM WORK OUTCOME INTO OUTCOME TEXT BOX

            Outcome.SetBinding (TextBox.TextProperty, MercuryApplication.PropertyDataBinding ("WorkOutcome.Name", workQueueItem, System.Windows.Data.BindingMode.OneWay));

            // DATA BIND WORK QUEUE ITEM WORKFLOW INTO WORKFLOW TEXT BOX

            Workflow.SetBinding (TextBox.TextProperty, MercuryApplication.PropertyDataBinding ("Workflow.Name", workQueueItem.WorkQueue, System.Windows.Data.BindingMode.OneWay));

            Instance.Text = workQueueItem.WorkflowInstanceId.ToString ();

            LastStep.Text = workQueueItem.WorkflowLastStep;

            NextStep.Text = workQueueItem.WorkflowNextStep;

            #endregion


            #region Created, Modified, and Current Assignment Section

            // MAP CREATED INFORMATION INTO CREATED INFORMATION COLUMN IN ASSIGNEMENT SECTION

            CreatedAuthority.Text = workQueueItem.CreateAccountInfo.SecurityAuthorityName;

            CreatedAccountId.Text = workQueueItem.CreateAccountInfo.UserAccountId;

            CreatedName.Text = workQueueItem.CreateAccountInfo.UserAccountName;

            CreatedDate.Text = workQueueItem.CreateAccountInfo.ActionDate.ToString ("MM/dd/yyyy");

            // MAP MODIFIED INFORMATION INTO MODIFIED INFORMATION COLUMN IN ASSIGNMENT SECTION

            ModifiedAuthority.Text = workQueueItem.ModifiedAccountInfo.SecurityAuthorityName;

            ModifiedAccountId.Text = workQueueItem.ModifiedAccountInfo.UserAccountId;

            ModifiedName.Text = workQueueItem.ModifiedAccountInfo.UserAccountName;

            ModifiedDate.Text = workQueueItem.ModifiedAccountInfo.ActionDate.ToString ("MM/dd/yyyy");

            // MAO ASSIGNED TO INFORMATION INTO ASSIGNED TO COLUMN IN ASSIGNMENT SECTION

            AssignedToAuthority.Text = workQueueItem.AssignedToSecurityAuthorityId.ToString ();

            AssignedToAccountId.Text = workQueueItem.AssignedToUserAccountId;

            AssignedToName.Text = workQueueItem.AssignedToUserAccountName;

            AssignedToDate.Text = Convert.ToDateTime (workQueueItem.AssignedToDate).ToString ("MM/dd/yyyy");

            #endregion


            #region Extended Properties

            if (workQueueItem.ExtendedProperties.Count > 0) {

                // MAP EXTENDED PROPERTIES

                ExtendedPropertiesGrid.ItemsSource = workQueueItem.ExtendedProperties;

            }

            else {

                // IF NO EXTENDED PROPERTIES SET EXTENDED PROPERTIES TO "NO EXTENDED PROPERTIES", ""

                ExtendedPropertiesGrid.ItemsSource = new Dictionary<String, String> () { { "No Extended Properties", "" } };

            }

            #endregion


            #region Senders

            // DATA BIND WORK QUEUE ITEM SENDERS INTO SENDERS SECTION

            SendersGrid.SetBinding (Telerik.Windows.Controls.RadGridView.ItemsSourceProperty, MercuryApplication.PropertyDataBinding ("Senders", workQueueItem, System.Windows.Data.BindingMode.OneWay));

            #endregion


            #region Assignment History

            //DATA BIND WORK QUEUE ITEM ASSIGNEMENT HISTORY INTO ASSIGNEMENT HISTORY SECTION

            AssignmentHistoryGrid.SetBinding (Telerik.Windows.Controls.RadGridView.ItemsSourceProperty, MercuryApplication.PropertyDataBinding ("AssignmentHistory", workQueueItem, System.Windows.Data.BindingMode.OneWay));

            #endregion


            #region Workflow Steps

            // DATA BIND WORK QUEUE ITEM WORKFLOW STEPS INTO WORKFLOW STEPS SECTION

            WorkflowStepsGrid.SetBinding (Telerik.Windows.Controls.RadGridView.ItemsSourceProperty, MercuryApplication.PropertyDataBinding ("WorkflowSteps", workQueueItem, System.Windows.Data.BindingMode.OneWay));

            #endregion


            return;

        }

        #endregion


    }

}
