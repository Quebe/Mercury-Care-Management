using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace Mercury.Server.Workflows.Activities.PromptUser {
            
    public sealed class PromptUserInitialize : CodeActivity {

        #region Input Arguments

        public InArgument<Workflows.UserInteractions.Enumerations.UserPromptType> PromptType { get; set; }

        public InArgument<Workflows.UserInteractions.Enumerations.UserPromptImage> PromptImage { get; set; }

        public InArgument<String> PromptTitle { get; set; }

        public InArgument<String> PromptMessage { get; set; }

        public InArgument<List<Workflows.UserInteractions.Structures.PromptSelectionItem>> PromptSelectionItems { get; set; }

        public InArgument<Boolean> AllowCancel { get; set; }

        #endregion 


        #region Output Arguments

        public OutArgument<Workflows.UserInteractions.Request.PromptUserRequest> UserInteractionRequest { get; set; }
        
        #endregion 


        #region Activity Steps

        protected override void Execute (CodeActivityContext context) {

            // TODO: VALIDATE ENTITY OR THROW EXCEPTION


            Mercury.Server.Workflows.UserInteractions.Request.PromptUserRequest request;

            request = new Mercury.Server.Workflows.UserInteractions.Request.PromptUserRequest (

                PromptType.Get (context),

                PromptImage.Get (context),

                PromptTitle.Get (context),

                PromptMessage.Get (context)
                
                );

            request.AllowCancel = AllowCancel.Get (context);

            if (PromptSelectionItems.Get (context) != null) {

                request.SelectionItems = PromptSelectionItems.Get (context);

            }


            UserInteractionRequest.Set (context, request);

            return;

        }

        #endregion 

    }

}
