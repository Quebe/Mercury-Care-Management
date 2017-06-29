using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace Mercury.Server.Workflows.Activities.ContactEntity {

    public sealed class ContactEntityInitialize : CodeActivity {

        #region Input Arguments

        public InArgument<Core.Entity.Entity> Entity { get; set; }

        public InArgument<Core.Entity.Entity> RelatedEntity { get; set; }


        public InArgument<Boolean> AllowEditRelatedEntity { get; set; }

        public InArgument<Boolean> AllowEditContactDateTime { get; set; }

        public InArgument<Boolean> AllowEditRegarding { get; set; }

        public InArgument<String> RegardingMessage { get; set; }

        public InArgument<String> IntroductionScript { get; set; }

        public InArgument<Boolean> AllowCancel { get; set; }

        #endregion 


        #region Output Arguments

        public OutArgument<Workflows.UserInteractions.Request.ContactEntityRequest> UserInteractionRequest { get; set; }
        
        #endregion 


        #region Activity Steps

        protected override void Execute (CodeActivityContext context) {

            // TODO: VALIDATE ENTITY OR THROW EXCEPTION


            Workflows.UserInteractions.Request.ContactEntityRequest request = new UserInteractions.Request.ContactEntityRequest (

                Entity.Get (context),

                RelatedEntity.Get (context),

                RegardingMessage.Get (context),

                IntroductionScript.Get (context)

                );


            request.AllowEditRelatedEntity = AllowEditRelatedEntity.Get (context);

            request.AllowEditContactDateTime = AllowEditContactDateTime.Get (context);

            request.AllowEditRegarding = AllowEditRegarding.Get (context);

            request.AllowCancel = AllowCancel.Get (context);


            UserInteractionRequest.Set (context, request);

            return;

        }

        #endregion 

    }

}
