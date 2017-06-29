using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Linq;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace Mercury.Workflows.ActivityLibrary {

    public sealed partial class WorkQueueItemSuspendWithAssignment : SequenceActivity {

        #region Dependency Properties

        public static readonly DependencyProperty ApplicationProperty = DependencyProperty.Register ("Application", typeof (Server.Application), typeof (WorkQueueItemSuspendWithAssignment));

        public Server.Application Application { get { return (Server.Application)GetValue (ApplicationProperty); } set { SetValue (ApplicationProperty, value); } }

        public static readonly DependencyProperty WorkflowStepsProperty = DependencyProperty.Register ("WorkflowSteps", typeof (System.Collections.Generic.List<Server.Workflows.WorkflowStep>), typeof (WorkQueueItemSuspendWithAssignment));

        public System.Collections.Generic.List<Server.Workflows.WorkflowStep> WorkflowSteps { get { return (System.Collections.Generic.List<Server.Workflows.WorkflowStep>)GetValue (WorkflowStepsProperty); } set { SetValue (WorkflowStepsProperty, value); } }


        public static readonly DependencyProperty WorkQueueItemProperty = DependencyProperty.Register ("WorkQueueItem", typeof (Server.Core.Work.WorkQueueItem), typeof (WorkQueueItemSuspendWithAssignment));

        public Server.Core.Work.WorkQueueItem WorkQueueItem { get { return (Server.Core.Work.WorkQueueItem)GetValue (WorkQueueItemProperty); } set { SetValue (WorkQueueItemProperty, value); } }


        public static readonly DependencyProperty SuspendMessageProperty = DependencyProperty.Register ("SuspendMessage", typeof (String), typeof (WorkQueueItemSuspendWithAssignment));

        public String SuspendMessage { get { return (String)GetValue (SuspendMessageProperty); } set { SetValue (SuspendMessageProperty, value); } }


        public static readonly DependencyProperty WorkflowLastStepProperty = DependencyProperty.Register ("WorkflowLastStep", typeof (String), typeof (WorkQueueItemSuspendWithAssignment));

        public String WorkflowLastStep { get { return (String)GetValue (WorkflowLastStepProperty); } set { SetValue (WorkflowLastStepProperty, value); } }


        public static readonly DependencyProperty WorkflowNextStepProperty = DependencyProperty.Register ("WorkflowNextStep", typeof (String), typeof (WorkQueueItemSuspendWithAssignment));

        public String WorkflowNextStep { get { return (String)GetValue (WorkflowNextStepProperty); } set { SetValue (WorkflowNextStepProperty, value); } }


        public static readonly DependencyProperty ConstraintDaysProperty = DependencyProperty.Register ("ConstraintDays", typeof (Int32), typeof (WorkQueueItemSuspendWithAssignment));

        public Int32 ConstraintDays { get { return (Int32)GetValue (ConstraintDaysProperty); } set { SetValue (ConstraintDaysProperty, value); } }


        public static readonly DependencyProperty MilestoneDaysProperty = DependencyProperty.Register ("MilestoneDays", typeof (Int32), typeof (WorkQueueItemSuspendWithAssignment));

        public Int32 MilestoneDays { get { return (Int32)GetValue (MilestoneDaysProperty); } set { SetValue (MilestoneDaysProperty, value); } }


        public static readonly DependencyProperty ReleaseItemProperty = DependencyProperty.Register ("ReleaseItem", typeof (Boolean), typeof (WorkQueueItemSuspendWithAssignment));

        public Boolean ReleaseItem { get { return (Boolean)GetValue (ReleaseItemProperty); } set { SetValue (ReleaseItemProperty, value); } }


        public static readonly DependencyProperty AssignedToSecurityAuthorityIdProperty = DependencyProperty.Register ("AssignedToSecurityAuthorityId", typeof (Int64), typeof (WorkQueueItemSuspendWithAssignment));

        public Int64 AssignedToSecurityAuthorityId { get { return (Int64)GetValue (AssignedToSecurityAuthorityIdProperty); } set { SetValue (AssignedToSecurityAuthorityIdProperty, value); } }

        public static readonly DependencyProperty AssignedToUserAccountIdProperty = DependencyProperty.Register ("AssignedToUserAccountId", typeof (String), typeof (WorkQueueItemSuspendWithAssignment));

        public String AssignedToUserAccountId { get { return (String)GetValue (AssignedToUserAccountIdProperty); } set { SetValue (AssignedToUserAccountIdProperty, value); } }

        public static readonly DependencyProperty AssignedToUserAccountNameProperty = DependencyProperty.Register ("AssignedToUserAccountName", typeof (String), typeof (WorkQueueItemSuspendWithAssignment));

        public String AssignedToUserAccountName { get { return (String)GetValue (AssignedToUserAccountNameProperty); } set { SetValue (AssignedToUserAccountNameProperty, value); } }

        public static readonly DependencyProperty AssignedToUserDisplayNameProperty = DependencyProperty.Register ("AssignedToUserDisplayName", typeof (String), typeof (WorkQueueItemSuspendWithAssignment));

        public String AssignedToUserDisplayName { get { return (String)GetValue (AssignedToUserDisplayNameProperty); } set { SetValue (AssignedToUserDisplayNameProperty, value); } }

        public static readonly DependencyProperty AssignmentSourceProperty = DependencyProperty.Register ("AssignmentSource", typeof (String), typeof (WorkQueueItemSuspendWithAssignment));

        public String AssignmentSource { get { return (String)GetValue (AssignmentSourceProperty); } set { SetValue (AssignmentSourceProperty, value); } }


        public static readonly DependencyProperty SuccessProperty = DependencyProperty.Register ("Success", typeof (Boolean), typeof (WorkQueueItemSuspendWithAssignment));

        public Boolean Success { get { return (Boolean)GetValue (SuccessProperty); } set { SetValue (SuccessProperty, value); } }


        #endregion


        #region Constructors

        public WorkQueueItemSuspendWithAssignment () {

            InitializeComponent ();

            return;

        }

        #endregion


        #region Workflow Support Methods

        private void RaiseActivityException (String exceptionMessage) {

            String caller = (new System.Diagnostics.StackFrame (1)).GetMethod ().Name;

            WorkflowSteps.Add (new Mercury.Server.Workflows.WorkflowStep (Application, "Exception [" + caller + "]", exceptionMessage));

            throw new ApplicationException (this.GetType ().ToString () + ": " + exceptionMessage);

        }

        private void WorkflowStepsAdd (String stepDescription) {

            if (WorkflowSteps != null) {

                String stepName = (new System.Diagnostics.StackFrame (1)).GetMethod ().Name;

                Server.Workflows.WorkflowStep workflowStep = new Mercury.Server.Workflows.WorkflowStep (Application, stepName, stepDescription);

                WorkflowSteps.Add (workflowStep);

                if (WorkQueueItem.Id != 0) {

                    Application.WorkQueueItemWorkflowStepsSave (WorkQueueItem.Id, WorkflowSteps);

                }

            }

            return;

        }

        #endregion


        #region Suspend Work Queue Item

        private void WorkQueueItemSuspendWithAssignment_OnExecuteCode (object sender, EventArgs e) {

            Success = false;

            if (WorkflowSteps == null) { WorkflowSteps = new System.Collections.Generic.List<Mercury.Server.Workflows.WorkflowStep> (); }


            //Server.Core.Work.WorkQueueItem workQueueItem = Application.WorkQueueItemGet (WorkQueueItemId);

            //if (workQueueItem == null) { RaiseActivityException ("Unable to retreive Work Queue Item [" + WorkQueueItemId.ToString () + "]."); }


            // SUSPEND 

            WorkQueueItem.SelfAssign ("Workflow Suspend Work Queue Item", false);

            Success = WorkQueueItem.Suspend (WorkflowLastStep, WorkflowNextStep, ConstraintDays, MilestoneDays, ReleaseItem);

            if (!Success) { RaiseActivityException (Application.LastException.Message); }


            // ASSIGNMENT

            Success = WorkQueueItem.AssignTo (AssignedToSecurityAuthorityId, AssignedToUserAccountId, AssignedToUserAccountName, AssignedToUserDisplayName, AssignmentSource, false);

            if (!Success) { RaiseActivityException (Application.LastException.Message); }

            WorkflowStepsAdd ("Assigned To: " + AssignedToUserDisplayName);


            WorkflowStepsAdd ("Suspend for Next Step: " + WorkflowNextStep + "  |  Constraint Days: " + ConstraintDays.ToString () + "  |  Milestone Days: " + MilestoneDays.ToString ());

            Application.WorkQueueItemWorkflowStepsSave (WorkQueueItem.Id, WorkflowSteps);

            // WorkflowSteps = WorkQueueItem.WorkflowStepsGet ();

            return;

        }

        #endregion

    }

}
