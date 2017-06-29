using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace Mercury.Server.Workflows.Activities.RequireForm {

    public sealed class RequireFormProcessResponse : CodeActivity {

        #region Input Arguments

        public InArgument<Workflows.WorkflowManager4> WorkflowManager { get; set; }

        public InArgument<Workflows.UserInteractions.Response.ResponseBase> UserInteractionResponse { get; set; }

        public InArgument<Int64> WorkQueueItemId { get; set; }

        public InOutArgument<List<WorkflowStep>> WorkflowSteps { get; set; }


        public InArgument<Boolean> AllowSaveAsDraft { get; set; }

        public InArgument<Boolean> AllowCancel { get; set; }
        
        #endregion


        #region Output Arguments

        public OutArgument<Boolean> ActivityCompleted { get; set; }

        public OutArgument<Boolean> ActivityCanceled { get; set; }

        public OutArgument<Boolean> SaveAsDraft { get; set; }

        public OutArgument<Boolean> FormReceived { get; set; }

        public OutArgument<Core.Forms.Form> Form { get; set; }

        #endregion


        #region Activity

        protected override void Execute (CodeActivityContext context) {

            ActivityCompleted.Set (context, false);

            ActivityCanceled.Set (context, false);


            if (UserInteractionResponse.Get (context).UserInteractionType == UserInteractions.Enumerations.UserInteractionType.RequireForm) {

                Workflows.UserInteractions.Response.RequireFormResponse response = (UserInteractions.Response.RequireFormResponse)UserInteractionResponse.Get (context);


                if ((response.Form != null) && (!response.SaveAsDraft) && (!response.Cancel)) {

                    Form.Set (context, response.Form);

                    Form.Get (context).ResetForm (WorkflowManager.Get (context).Application);

                    FormReceived.Set (context, true);

                    ActivityCompleted.Set (context, true);
                    
                    Activities.CommonFunctions.WorkflowStepsAdd (

                        WorkflowManager.Get (context).Application,

                        1,

                        WorkQueueItemId.Get (context),

                        WorkflowSteps.Get (context),

                        Form.Get (context).Name

                        );


                }

                else if (response.SaveAsDraft) {

                    Form.Set (context, response.Form);

                    Form.Get (context).ResetForm (WorkflowManager.Get (context).Application);

                }

                else if (response.Cancel) {

                    Activities.CommonFunctions.WorkflowStepsAdd (

                        WorkflowManager.Get (context).Application,

                        1,

                        WorkQueueItemId.Get (context),

                        WorkflowSteps.Get (context),

                        "Form Canceled."

                        );


                    ActivityCompleted.Set (context, true);

                    ActivityCanceled.Set (context, true);

                }

                SaveAsDraft.Set (context, response.SaveAsDraft);

            }

        }

        #endregion
    
    }

}
