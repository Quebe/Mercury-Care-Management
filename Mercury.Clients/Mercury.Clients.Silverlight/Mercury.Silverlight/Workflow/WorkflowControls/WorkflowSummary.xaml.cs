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

namespace Mercury.Silverlight.Workflow.WorkflowControls {

    public partial class WorkflowSummary : UserControl {
        
        #region Private Properties

        private Client.Application MercuryApplication = ((App) Application.Current).MercuryApplication;

        private WindowManager.WindowManager WindowManager = ((App) Application.Current).WindowManager;

        #endregion 

        #region Public Properties

        public System.Collections.ObjectModel.ObservableCollection<Server.Application.WorkflowStep> WorkflowSteps {

            set {

                this.Dispatcher.BeginInvoke (delegate { WorkflowStepsGrid.ItemsSource = value; });

            }

        }

        #endregion 


        public WorkflowSummary () {

            InitializeComponent ();

        }

        public void InitializeSteps (Server.Application.WorkflowResponse response) {

            if (response.WorkQueueItemId == 0) { WorkflowSteps = response.WorkflowSteps; }

            else { MercuryApplication.WorkQueueItemGetWorkflowSteps (response.WorkQueueItemId, WorkQueueItemWorkflowStepsGetCompleted); }

        }

        public void WorkQueueItemWorkflowStepsGetCompleted (Object sender, Server.Application.WorkQueueItemWorkflowStepsGetCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) & (!e.Result.HasException)) {

                WorkflowSteps = e.Result.Collection;

            }

            return; 

        }

    }

}
