using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

using Server = Mercury.Server;
using UserInteractions = Mercury.Server.Workflows.UserInteractions;

namespace Mercury.Workflows.ActivityLibrary {

    public partial class WorkQueueItemClose : SequenceActivity {


        #region Dependency Properties

        public static readonly DependencyProperty ApplicationProperty = DependencyProperty.Register ("Application", typeof (Server.Application), typeof (WorkQueueItemClose));

        public Server.Application Application { get { return (Server.Application) GetValue (ApplicationProperty); } set { SetValue (ApplicationProperty, value); } }

        public static readonly DependencyProperty WorkflowStepsProperty = DependencyProperty.Register ("WorkflowSteps", typeof (System.Collections.Generic.List<Server.Workflows.WorkflowStep>), typeof (WorkQueueItemClose));

        public System.Collections.Generic.List<Server.Workflows.WorkflowStep> WorkflowSteps { get { return (System.Collections.Generic.List<Server.Workflows.WorkflowStep>) GetValue (WorkflowStepsProperty); } set { SetValue (WorkflowStepsProperty, value); } }


        public static readonly DependencyProperty WorkQueueItemProperty = DependencyProperty.Register ("WorkQueueItem", typeof (Server.Core.Work.WorkQueueItem), typeof (WorkQueueItemClose));

        public Server.Core.Work.WorkQueueItem WorkQueueItem { get { return (Server.Core.Work.WorkQueueItem) GetValue (WorkQueueItemProperty); } set { SetValue (WorkQueueItemProperty, value); } }


        public static readonly DependencyProperty WorkOutcomeNameProperty = DependencyProperty.Register ("WorkOutcomeName", typeof (String), typeof (WorkQueueItemClose));

        public String WorkOutcomeName { get { return (String) GetValue (WorkOutcomeNameProperty); } set { SetValue (WorkOutcomeNameProperty, value); } }


        public static readonly DependencyProperty WorkflowLastStepProperty = DependencyProperty.Register ("WorkflowLastStep", typeof (String), typeof (WorkQueueItemClose));

        public String WorkflowLastStep { get { return (String) GetValue (WorkflowLastStepProperty); } set { SetValue (WorkflowLastStepProperty, value); } }

        public static readonly DependencyProperty WorkflowCloseStepStatusProperty = DependencyProperty.Register ("WorkflowCloseStepStatus", typeof (Server.Workflows.Enumerations.WorkflowStepStatus), typeof (WorkQueueItemClose));

        public Server.Workflows.Enumerations.WorkflowStepStatus WorkflowCloseStepStatus { get { return (Server.Workflows.Enumerations.WorkflowStepStatus)GetValue (WorkflowCloseStepStatusProperty); } set { SetValue (WorkflowCloseStepStatusProperty, value); } }


        public static readonly DependencyProperty SuccessProperty = DependencyProperty.Register ("Success", typeof (Boolean), typeof (WorkQueueItemClose));

        public Boolean Success { get { return (Boolean) GetValue (SuccessProperty); } set { SetValue (SuccessProperty, value); } }

        #endregion


        #region Constructors

        public WorkQueueItemClose () {

            InitializeComponent ();

        }

        #endregion 

        
        #region Workflow Support Methods

        private void RaiseActivityException (String exceptionMessage) {

            String caller = (new System.Diagnostics.StackFrame (1)).GetMethod ().Name;

            WorkflowSteps.Add (new Mercury.Server.Workflows.WorkflowStep (Application, "Exception [" + caller + "]", exceptionMessage));

            throw new ApplicationException (this.GetType ().ToString () + ": " + exceptionMessage);

        }

        private void WorkflowStepsAdd (Server.Workflows.Enumerations.WorkflowStepStatus stepStatus, String stepDescription) {

            if (WorkflowSteps != null) {

                String stepName = (new System.Diagnostics.StackFrame (1)).GetMethod ().Name;

                Server.Workflows.WorkflowStep workflowStep = new Mercury.Server.Workflows.WorkflowStep (Application, stepStatus, stepName, stepDescription);

                WorkflowSteps.Add (workflowStep);

                if (WorkQueueItem.Id != 0) {

                    Application.WorkQueueItemWorkflowStepsSave (WorkQueueItem.Id, WorkflowSteps);

                }

            }

            return;

        }

        #endregion


        #region Close Work Queue Item

        private void WorkQueueItemClose_CodeExecution (object sender, EventArgs e) {

            Success = false;

            if (WorkflowSteps == null) { WorkflowSteps = new System.Collections.Generic.List<Mercury.Server.Workflows.WorkflowStep> (); }


            //Server.Core.Work.WorkQueueItem workQueueItem = Application.WorkQueueItemGet (WorkQueueItemId);

            //if (workQueueItem == null) { RaiseActivityException ("Unable to retreive Work Queue Item [" + WorkQueueItemId.ToString () + "]."); }

            Server.Core.Work.WorkOutcome workOutcome = Application.WorkOutcomeGet (WorkOutcomeName);

            if (workOutcome == null) { RaiseActivityException ("Unable to retreive Work Outcome [" + WorkOutcomeName.ToString () + "]."); }


            if (!String.IsNullOrWhiteSpace (WorkflowLastStep)) { WorkQueueItem.WorkflowLastStep = WorkflowLastStep; }

            Success = WorkQueueItem.Close (workOutcome.Id, false);

            Exception lastException = Application.LastException; 

            Application.WorkQueueItemWorkflowStepsSave (WorkQueueItem.Id, WorkflowSteps); // CLEARS APPLICATION.LASTEXCEPTION

            if (!Success) {

                if (lastException != null) { RaiseActivityException (lastException.Message); }

                else { RaiseActivityException ("Unable to successfully close Work Queue Item, might already be marked as closed."); }
            
            }

            WorkflowStepsAdd (WorkflowCloseStepStatus, WorkOutcomeName);

            return;

        }

        #endregion

    }

}
