using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace Mercury.Server.Workflows.Activities.SendCorrespondence {

    public sealed class SendCorrespondenceInitialize : CodeActivity {

        #region Input Arguments

        public InArgument<Workflows.WorkflowManager4> WorkflowManager { get; set; }

        public InArgument<Core.Entity.Entity> Entity { get; set; }

        public InArgument<Core.Entity.Entity> RelatedEntity { get; set; }


        public InArgument<String> CorrespondenceName { get; set; }

        public InArgument<Boolean> AllowCancel { get; set; }
        

        public InArgument<String> Attention { get; set; }

        public InArgument<Boolean> AllowEditRelatedEntity { get; set; }

        public InArgument<Boolean> AllowUserSelection { get; set; }

        public InArgument<Boolean> AllowAlternateAddress { get; set; }

        public InArgument<Boolean> AllowHistoricalSendDate { get; set; }

        public InArgument<Boolean> AllowFutureSendDate { get; set; }

        public InArgument<Boolean> AllowSendByFacsimile { get; set; }

        public InArgument<Boolean> AllowSendByEmail { get; set; }

        public InArgument<Boolean> AllowSendByInPerson { get; set; }

        public InArgument<DateTime> SendDate { get; set; }

        public InArgument<Core.Entity.EntityAddress> AlternateAddress { get; set; }

        public InArgument<String> AlternateFaxNumber { get; set; }

        public InArgument<String> AlternateEmail { get; set; }

        #endregion 


        #region Output Arguments

        public OutArgument<Workflows.UserInteractions.Request.SendCorrespondenceRequest> UserInteractionRequest { get; set; }
        
        #endregion 


        #region Activity Steps

        protected override void Execute (CodeActivityContext context) {

            // TODO: VALIDATE ENTITY OR THROW EXCEPTION


            Workflows.UserInteractions.Request.SendCorrespondenceRequest request = new UserInteractions.Request.SendCorrespondenceRequest (

                Entity.Get (context),

                RelatedEntity.Get (context),

                WorkflowManager.Get (context).Application.CorrespondenceGetIdByName (CorrespondenceName.Get (context)),

               
                AllowUserSelection.Get (context),

                AllowAlternateAddress.Get (context)

                );



            request.AllowCancel = AllowCancel.Get (context);

            request.Attention = Attention.Get (context);

            request.AllowEditRelatedEntity = AllowEditRelatedEntity.Get (context);

            request.AllowHistoricalSendDate = AllowHistoricalSendDate.Get (context);

            request.AllowFutureSendDate = AllowFutureSendDate.Get (context);

            request.AllowSendByFacsimile = AllowSendByFacsimile.Get (context);

            request.AllowSendByEmail = AllowSendByEmail.Get (context);

            request.AllowSendByInPerson = AllowSendByInPerson.Get (context);

            request.SendDate = SendDate.Get (context);

            request.AlternateAddress = AlternateAddress.Get (context);

            request.AlternateFaxNumber = AlternateFaxNumber.Get (context);

            request.AlternateEmail = AlternateEmail.Get (context);


            UserInteractionRequest.Set (context, request);

            return;

        }

        #endregion 
    
    }

}
