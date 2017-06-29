using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace Mercury.Server.Workflows.Activities.SendCorrespondence {

    public sealed class SendCorrespondenceProcessResponse : CodeActivity {

        #region Input Arguments

        public InOutArgument<Workflows.WorkflowManager4> WorkflowManager { get; set; }

        public InArgument<Workflows.UserInteractions.Response.ResponseBase> UserInteractionResponse { get; set; }

        public InArgument<Int64> WorkQueueItemId { get; set; }

        public InOutArgument<List<WorkflowStep>> WorkflowSteps { get; set; }


        public InArgument<Boolean> AllowCancel { get; set; }

        public InArgument<Boolean> AutoSaveCorrespondence { get; set; }

        #endregion


        #region Output Arguments

        public OutArgument<Boolean> ActivityCompleted { get; set; }

        public OutArgument<Boolean> ActivityCanceled { get; set; }

        public OutArgument<Core.Entity.EntityCorrespondence> EntityCorrespondence { get; set; }

        #endregion


        #region Activity

        protected override void Execute (CodeActivityContext context) {

            ActivityCompleted.Set (context, false);

            ActivityCanceled.Set (context, false);


            if (UserInteractionResponse.Get (context).UserInteractionType == UserInteractions.Enumerations.UserInteractionType.SendCorrespondence) {

                Workflows.UserInteractions.Response.SendCorrespondenceResponse response = (UserInteractions.Response.SendCorrespondenceResponse)UserInteractionResponse.Get (context);


                if ((AllowCancel.Get (context)) && (response.Cancel)) {

                    EntityCorrespondence = null;

                    ActivityCompleted.Set (context, true);

                    ActivityCanceled.Set (context, true);

                    return;

                }


                ActivityCanceled.Set (context, false);

                if (response.EntityCorrespondence == null) { return; }


                response.EntityCorrespondence.Application = WorkflowManager.Get (context).Application;

                EntityCorrespondence.Set (context, response.EntityCorrespondence);


                if (AutoSaveCorrespondence.Get (context)) { ActivityCompleted.Set (context, EntityCorrespondence.Get (context).Save ()); }

                else { ActivityCompleted.Set (context, true); }


                if (ActivityCompleted.Get (context)) {

                    CommonFunctions.WorkflowStepsAdd (

                        WorkflowManager.Get (context).Application, 

                        1, 

                        WorkQueueItemId.Get (context),

                        WorkflowSteps.Get (context),
                        
                        "Send Correspondence: " + EntityCorrespondence.Get (context).CorrespondenceName
                        
                        );

                    EntityCorrespondence = EntityCorrespondence; // UPDATE TO SELF TO ALLOW FOR CAPTURING THE NEW UNIQUE ID

                }

                else { 
                    
                    CommonFunctions.RaiseActivityException (
                        
                        WorkflowManager.Get (context).Application, 

                        1,

                        WorkQueueItemId.Get (context),

                        WorkflowSteps.Get (context),

                        "Unable to save entity correspondence information."
                        
                        ); 
                
                }


            }


        }

        #endregion

    }

}
