using System;
using System.Collections.Generic;
using System.Activities;
using System.Text;

namespace Mercury.Server.Workflows.Activities.PromptUser {

    public class PromptUserProcessResponse : CodeActivity {

        #region Input Arguments

        public InOutArgument<Workflows.WorkflowManager4> WorkflowManager { get; set; }

        public InArgument<Workflows.UserInteractions.Response.ResponseBase> UserInteractionResponse { get; set; }

        public InArgument<Int64> WorkQueueItemId { get; set; }

        public InOutArgument<List<WorkflowStep>> WorkflowSteps { get; set; }


        public InArgument<Boolean> AllowCancel { get; set; }

        #endregion


        #region Output Arguments

        public OutArgument<Boolean> ActivityCompleted { get; set; }

        public OutArgument<Boolean> ActivityCanceled { get; set; }

        public OutArgument<Workflows.UserInteractions.Enumerations.UserPromptButtonClicked> ButtonClicked { get; set; }

        public OutArgument<String> InputText { get; set; }

        public OutArgument<String> SelectedValue { get; set; }

        public OutArgument<String> SelectedText { get; set; }

        #endregion


        #region Activity

        protected override void Execute (CodeActivityContext context) {

            ActivityCompleted.Set (context, false);

            ActivityCanceled.Set (context, false);


            if (UserInteractionResponse.Get (context).UserInteractionType == UserInteractions.Enumerations.UserInteractionType.Prompt) {

                Workflows.UserInteractions.Response.PromptUserResponse response = (UserInteractions.Response.PromptUserResponse)UserInteractionResponse.Get (context);


                if (response.ButtonClicked != UserInteractions.Enumerations.UserPromptButtonClicked.None) {

                    ButtonClicked.Set (context, response.ButtonClicked);



                    if (response.InputText == null) { response.InputText = String.Empty; }

                    InputText.Set (context, response.InputText);


                    if (response.SelectedValue == null) { response.SelectedValue = String.Empty; }

                    SelectedValue.Set (context, response.SelectedValue);


                    if (response.SelectedText == null) { response.SelectedText = String.Empty; }

                    SelectedText.Set (context, response.SelectedText);


                    Activities.CommonFunctions.WorkflowStepsAdd (

                        WorkflowManager.Get (context).Application,

                        1,

                        WorkQueueItemId.Get (context),

                        WorkflowSteps.Get (context),

                        "Button Clicked: " + ButtonClicked.Get (context).ToString () + "  |  Selected Text = " + SelectedText.Get (context).ToString ()

                        );


                    // IF USER DID NOT CANCEL, OR THEY DID CANCEL AND CANCEL ALLOWED, MARK COMPLETED

                    if ((response.ButtonClicked != UserInteractions.Enumerations.UserPromptButtonClicked.Cancel) 

                        || ((response.ButtonClicked == UserInteractions.Enumerations.UserPromptButtonClicked.Cancel) && (AllowCancel.Get (context)))) { 
                    
                        ActivityCompleted.Set (context, true);

                    }

                }
            
            }
            
        }

        #endregion

    }

}
