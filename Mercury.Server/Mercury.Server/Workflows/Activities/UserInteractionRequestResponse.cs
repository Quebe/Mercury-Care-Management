using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace Mercury.Server.Workflows.Activities {

    public sealed class UserInteractionRequestResponse : NativeActivity {

        #region Public Properties

        protected override Boolean CanInduceIdle { get { return true; } }

        #endregion 


        #region Input Arguments

        public InOutArgument<Server.Workflows.WorkflowManager4> WorkflowManager { get; set; }

        public InArgument<Int64> WorkQueueItemId { get; set; }

        public InOutArgument<List<Workflows.WorkflowStep>> WorkflowSteps { get; set; }

        public InArgument<Server.Workflows.UserInteractions.Request.RequestBase> Request { get; set; }

        #endregion 


        #region Output Arguments

        public OutArgument<Server.Workflows.UserInteractions.Response.ResponseBase> Response { get; set; }

        #endregion 


        #region Constructors 


        #endregion 


        #region Workflow Steps

        protected override void Execute (NativeActivityContext context) {

            // RECORD WORKFLOW STEPS

            Workflows.Activities.CommonFunctions.WorkflowStepsAdd (

                WorkflowManager.Get (context).Application,

                1,

                WorkQueueItemId.Get (context),

                WorkflowSteps.Get (context),

                "User Interaction Request: " + Server.CommonFunctions.EnumerationToString (Request.Get (context).UserInteractionType)

                );

            // SAVE WORKFLOW STEPS 

            WorkflowManager.Get (context).Application.WorkQueueItemWorkflowStepsSave (WorkQueueItemId.Get (context), WorkflowSteps.Get (context));


            // SET USER INTERACTION REQUEST IN THE WORKFLOW MANAGER
            
            WorkflowManager.Get (context).UserInteractionRequest = Request.Get (context);


            // CREATE A BOOKMARK SET TO THE WORKFLOW INSTANCE ID TO MAKE IT UNIQUE

            context.CreateBookmark (context.WorkflowInstanceId.ToString (), new BookmarkCallback (this.ReceiveResponse));

            return;

        }

        public void ReceiveResponse (NativeActivityContext context, Bookmark bookmark, Object workflowManager) {

            // REMOVE THE BOOKMARK SO THAT IT CAN BE USED AGAIN
            
            context.RemoveBookmark (context.WorkflowInstanceId.ToString ());


            // RESET WORKFLOW MANAGER INSTANCE

            WorkflowManager.Set (context, workflowManager);

            Response.Set (context, WorkflowManager.Get (context).UserInteractionResponse);


            // RECORD WORKFLOW STEPS

            if (Response.Get (context) != null) {

                Workflows.Activities.CommonFunctions.WorkflowStepsAdd (

                    WorkflowManager.Get (context).Application,

                    1,

                    WorkQueueItemId.Get (context),

                    WorkflowSteps.Get (context),

                    "User Interaction Response: " + Server.CommonFunctions.EnumerationToString (Response.Get (context).UserInteractionType)

                    );

            }

            else {

                Workflows.Activities.CommonFunctions.WorkflowStepsAdd (

                    WorkflowManager.Get (context).Application,

                    1,

                    WorkQueueItemId.Get (context),

                    WorkflowSteps.Get (context),

                    "User Interaction Response: Unknown Response"

                    );

            }
            return;

        }

        #endregion 


    }

}
