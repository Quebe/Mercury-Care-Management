using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace Mercury.Server.Workflows.Activities.WorkQueueItem {

    public sealed class WorkQueueItemSuspend : NativeActivity {

        #region Public Properties

        protected override Boolean CanInduceIdle { get { return true; } }

        #endregion 


        #region Input Arguments

        public InOutArgument<Server.Workflows.WorkflowManager4> WorkflowManager { get; set; }

        public InOutArgument<Server.Core.Work.WorkQueueItem> WorkQueueItem { get; set; }

        public InOutArgument<List<Workflows.WorkflowStep>> WorkflowSteps { get; set; }


        public InArgument<String> SuspendMessage { get; set; }

        public InArgument<String> WorkflowLastStep { get; set; }

        public InArgument<String> WorkflowNextStep { get; set; }


        public InArgument<Int32> ConstraintDays { get; set; }

        public InArgument<Int32> MilestoneDays { get; set; }

        public InArgument<Boolean> ReleaseItem { get; set; }


        public InArgument<Boolean> SuspendWorkflow { get; set; }

        #endregion 


        #region Output Arguments

        public OutArgument<Boolean> Success { get; set; }

        #endregion 


        #region Constructors 


        #endregion 


        #region Workflow Steps

        protected override void Execute (NativeActivityContext context) {

            // RESET APPLICATION REFERENCE ON WORK QUEUE ITEM

            WorkQueueItem.Get (context).Application = WorkflowManager.Get (context).Application;


            // ENSURE THAT THE ITEM IS ASSIGNED TO USER

            WorkQueueItem.Get (context).SelfAssign ("Workflow Suspend Work Queue Item", false);


            // ATTEMPT TO SUSPEND THE ITEM

            Success.Set (context, WorkQueueItem.Get (context).Suspend (

                WorkflowLastStep.Get (context),

                WorkflowNextStep.Get (context),

                ConstraintDays.Get (context),

                MilestoneDays.Get (context),

                ReleaseItem.Get (context)

                ));


            if (!Success.Get (context)) { // THROW EXCEPTION IF UNABLE TO SUSPEND

                CommonFunctions.RaiseActivityException (WorkflowManager.Get (context).Application,

                    1,

                    WorkQueueItem.Get (context).Id,

                    WorkflowSteps.Get (context),

                    WorkflowManager.Get (context).Application.LastException.Message

                    );

            }

            // RECORD WORKFLOW STEPS

            Workflows.Activities.CommonFunctions.WorkflowStepsAdd (

                WorkflowManager.Get (context).Application,

                1,

                WorkQueueItem.Get (context).Id,

                WorkflowSteps.Get (context),

                "Suspend for Next Step: " + WorkflowNextStep.Get (context) + 
                
                    "  |  Constraint Days: " + ConstraintDays.Get(context).ToString () + 
                    
                    "  |  Milestone Days: " + MilestoneDays.Get(context).ToString ()

                );


            // SAVE WORKFLOW STEPS 

            WorkflowManager.Get (context).Application.WorkQueueItemWorkflowStepsSave (WorkQueueItem.Get (context).Id, WorkflowSteps.Get (context));


            // EVALUATE IF WE WANT TO SUSPEND THE WORKFLOW TOO, OR JUST THE WORKFLOW ITEM AND LET THE WORKFLOW CONTINUE

            if (SuspendWorkflow.Get (context)) {


                // MARK WORKFLOW AS SUSPEND USING WORKFLOW MANAGER

                WorkflowManager.Get (context).WorkflowStatus = WorkflowStatus.Suspended;


                // CREATE A BOOKMARK SET TO THE WORKFLOW INSTANCE ID TO MAKE IT UNIQUE

                context.CreateBookmark (context.WorkflowInstanceId.ToString (), new BookmarkCallback (this.ResumeWorkflow));

            }

            return;

        }

        public void ResumeWorkflow (NativeActivityContext context, Bookmark bookmark, Object workflowManager) {

            // REMOVE THE BOOKMARK SO THAT IT CAN BE USED AGAIN
            
            context.RemoveBookmark (context.WorkflowInstanceId.ToString ());


            // RESET WORKFLOW MANAGER INSTANCE

            WorkflowManager.Set (context, workflowManager);

            WorkflowManager.Get (context).WorkflowStatus = WorkflowStatus.Resumed;


            // RECORD WORKFLOW STEPS

            Workflows.Activities.CommonFunctions.WorkflowStepsAdd (

                WorkflowManager.Get (context).Application,

                1,

                WorkQueueItem.Get (context).Id,

                WorkflowSteps.Get (context),

                "Resume Workflow"

                );

            return;

        }

        #endregion 


    }

}
