using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Workflow.Activities;

using Mercury.Server.Workflows.EventArguments;
using Mercury.Server.Workflows.UserInteractions;

namespace Mercury.Server.Workflows {

    [ExternalDataExchange]
    public interface IWorkflowService {

        // event EventHandler<UserDataReceivedEventArgs> OnUserDataReceived;

        //void RequestUserInteraction (UserInteraction userInteraction);

        //void RequestUserInterfaceUpdate (UserInterfaceUpdate userInterfaceUpdate);

        event EventHandler <UserInteractions.Request.RequestEventArgs> OnUserInteractionRequest;

        event EventHandler <UserInteractions.Response.ResponseEventArgs> OnUserInteractionResponse;


        void UserInteractionRequest (UserInteractions.Request.RequestBase request);

    }

}
