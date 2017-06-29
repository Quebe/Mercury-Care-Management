using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Workflows.Activities {

    public class CommonFunctions {

        public static void WorkflowStepsAdd (Server.Application application, Int32 stackDepth, Int64 workQueueItemId, List<WorkflowStep> workflowSteps, String stepDescription) {

            WorkflowStepsAdd (application, stackDepth + 1, workQueueItemId, workflowSteps, Enumerations.WorkflowStepStatus.Informational, stepDescription);

            return;

        }

        public static void WorkflowStepsAdd (Server.Application application, Int32 stackDepth, Int64 workQueueItemId, List<WorkflowStep> workflowSteps, Workflows.Enumerations.WorkflowStepStatus status, String stepDescription) {

            if (workflowSteps != null) {

                String stepName =

                    Server.CommonFunctions.PascalString ((new System.Diagnostics.StackFrame (stackDepth)).GetMethod ().DeclaringType.Name)

                    + " . " +

                    Server.CommonFunctions.PascalString ((new System.Diagnostics.StackFrame (stackDepth)).GetMethod ().Name);

                Server.Workflows.WorkflowStep workflowStep = new Mercury.Server.Workflows.WorkflowStep (application, status, stepName, stepDescription);

                workflowSteps.Add (workflowStep);

                // REMOVED FROM SAVING EACH TIME, SAVE WILL NO OCCUR BEFORE USER INTERACTION 

                // AND WHEN SUSPENDING OR CLOSING THE WORK QUEUE ITEM

                //if (workQueueItemId != 0) {

                //    application.WorkQueueItemWorkflowStepsSave (workQueueItemId, workflowSteps);

                //}

            }

            return;

        }

        public static void RaiseActivityException (Server.Application application, Int32 stackDepth, Int64 workQueueItemId, List<WorkflowStep> workflowSteps, String exceptionMessage) {

            String caller = (new System.Diagnostics.StackFrame (stackDepth)).GetMethod ().Name;

            WorkflowStepsAdd (application, stackDepth + 1, workQueueItemId, workflowSteps, exceptionMessage);

            application.WorkQueueItemWorkflowStepsSave (workQueueItemId, workflowSteps);

            throw new ApplicationException (caller + ": " + exceptionMessage);

        }

    }

}
