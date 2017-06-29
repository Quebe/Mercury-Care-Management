using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace Mercury.Server.Workflows.Activities {

    public sealed class WorkflowStepsAppend : CodeActivity {


        #region In Arguments

        public InArgument<Server.Application> Application { get; set; }

        public InArgument<Int32> StackDepth { get; set; }

        public InArgument<Int64> WorkQueueItemId { get; set; }

        public InOutArgument<List<WorkflowStep>> WorkflowSteps { get; set; }

        public InArgument<Workflows.Enumerations.WorkflowStepStatus> Status { get; set; }

        public InArgument<String> Description { get; set; }

        #endregion 


        #region Activity

        protected override void Execute (CodeActivityContext context) {

            CommonFunctions.WorkflowStepsAdd (

                Application.Get (context),

                StackDepth.Get (context),

                WorkQueueItemId.Get (context),

                WorkflowSteps.Get (context),

                Status.Get (context),

                Description.Get (context)

                );


            return;

        }

        #endregion 

    }

}
