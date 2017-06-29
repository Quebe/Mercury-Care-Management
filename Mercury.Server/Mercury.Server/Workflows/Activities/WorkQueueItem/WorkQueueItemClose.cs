using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace Mercury.Server.Workflows.Activities.WorkQueueItem {

    public sealed class WorkQueueItemClose : CodeActivity {


        
        #region Input Arguments

        public InOutArgument<Server.Workflows.WorkflowManager4> WorkflowManager { get; set; }

        public InOutArgument<Server.Core.Work.WorkQueueItem> WorkQueueItem { get; set; }

        public InOutArgument<List<Workflows.WorkflowStep>> WorkflowSteps { get; set; }


        public InArgument<String> WorkOutcomeName { get; set; }

        public InArgument<String> WorkflowLastStep { get; set; }

        #endregion 


        #region Output Arguments

        public OutArgument<Boolean> Success { get; set; }

        #endregion 
        
        
        #region Activity

        protected override void Execute (CodeActivityContext context) {


            // VALIDATE THAT WORK QUEUE ITEM IS AN OBJECT

            if (WorkQueueItem.Get (context) == null) {  // THROW EXCEPTION 

                CommonFunctions.RaiseActivityException (

                    WorkflowManager.Get (context).Application,

                    1,

                    0,

                    WorkflowSteps.Get (context),

                    "No Work Queue Item specified for the Close function."

                    );

            }


            // RESET APPLICATION REFERENCE ON WORK QUEUE ITEM

            WorkQueueItem.Get (context).Application = WorkflowManager.Get (context).Application;



            // MAKE SURE THAT WORKFLOW STEPS IS A VALID COLLECTION 

            if (WorkflowSteps.Get (context) == null) { 

                WorkflowSteps.Set (context, new List<WorkflowStep> ());

            }


            // GET WORK OUT COME FROM DATABASE BY NAME

            Int64 workOutcomeId = WorkflowManager.Get (context).Application.WorkOutcomeGetIdByName (WorkOutcomeName.Get (context));

            if (workOutcomeId == 0) {  // THROW EXCEPTION 

                CommonFunctions.RaiseActivityException (

                    WorkflowManager.Get (context).Application,

                    1,

                    WorkQueueItem.Get (context).Id,

                    WorkflowSteps.Get (context),

                    "Unable to retreive Work Outcome \"" + WorkOutcomeName.Get (context) + "\"."

                    );

            }


            // CLOSE WORK QUEUE ITEM AND WRITE OUTCOME

            WorkQueueItem.Get (context).WorkflowLastStep = WorkflowLastStep.Get (context);

            if (WorkQueueItem.Get (context).Close (workOutcomeId, false)) {

                CommonFunctions.WorkflowStepsAdd (

                    WorkflowManager.Get (context).Application,

                    1,

                    WorkQueueItem.Get (context).Id,

                    WorkflowSteps.Get (context),

                    WorkOutcomeName.Get (context)

                    );

            }

            else { 

                CommonFunctions.RaiseActivityException (

                    WorkflowManager.Get (context).Application,

                    1,

                    WorkQueueItem.Get (context).Id,

                    WorkflowSteps.Get (context),

                    ((WorkflowManager.Get (context).Application.LastException != null) ? 

                        WorkflowManager.Get (context).Application.LastException.Message : 
                            
                        "Unable to successfully close Work Queue Item, might already be marked as closed.")

                    );

            }

            
            // SAVE WORKFLOW STEPS

            WorkflowManager.Get (context).Application.WorkQueueItemWorkflowStepsSave (WorkQueueItem.Get (context).Id, WorkflowSteps.Get (context));


            return;
        
        }

        #endregion 

    }

}
