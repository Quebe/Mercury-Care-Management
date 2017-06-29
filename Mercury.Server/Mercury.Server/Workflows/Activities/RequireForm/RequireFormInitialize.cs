using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace Mercury.Server.Workflows.Activities.RequireForm {

    public sealed class RequireFormInitialize : CodeActivity {

        #region Input Arguments

        public InArgument<Workflows.WorkflowManager4> WorkflowManager { get; set; }

        public InArgument<Int64> WorkQueueItemId { get; set; }

        public InArgument<List<Workflows.WorkflowStep>> WorkflowSteps { get; set; }


        public InArgument<Core.Entity.Entity> Entity { get; set; }

        public InArgument<Core.Forms.Form> Form { get; set; }

        public InArgument<String> FormName { get; set; }


        public InArgument<Boolean> AllowSaveAsDraft { get; set; }

        public InArgument<Boolean> AllowCancel { get; set; }

        #endregion


        #region Output Arguments

        public OutArgument<Workflows.UserInteractions.Request.RequireFormRequest> UserInteractionRequest { get; set; }

        #endregion


        #region Activity Steps

        protected override void Execute (CodeActivityContext context) {

            // TODO: VALIDATE ENTITY OR THROW EXCEPTION


            Workflows.UserInteractions.Request.RequireFormRequest request = new UserInteractions.Request.RequireFormRequest ();

            request.EntityType = Entity.Get (context).EntityType;

            request.EntityObjectId = WorkflowManager.Get (context).Application.EntityObjectIdGet (Entity.Get (context));


            if (Form.Get (context) == null) {

                // LOAD FORM BY NAME (NO FORM WAS PASSED FROM PARENT ACTIVITY)

                request.Form = new Core.Forms.Form (WorkflowManager.Get (context).Application, FormName.Get (context));

                if (request.Form == null) {

                    CommonFunctions.RaiseActivityException ( // THROW EXCEPTION

                        WorkflowManager.Get (context).Application,

                        1,

                        WorkQueueItemId.Get (context),

                        WorkflowSteps.Get (context),

                        "Unable to load form for " + FormName.Get (context) + "."

                        );

                }

                request.Form.EntityType = request.EntityType;

                request.Form.EntityObjectId = request.EntityObjectId;

            }

            else { request.Form = Form.Get (context); } // USE FORM PASSED IN FROM PARENT ACTIVITY

            request.Form.FormType = Core.Forms.Enumerations.FormType.Instance;


            request.Message = "Form Required: " + request.Form.Name + " (Last Modified: " + request.Form.ModifiedAccountInfo.ActionDate.ToString ("MM/dd/yyyy") + ")";
            
            request.AllowSaveAsDraft = AllowSaveAsDraft.Get (context);

            request.AllowCancel = AllowCancel.Get (context);


            UserInteractionRequest.Set (context, request);

            return;

        }

        #endregion 

    }

}
