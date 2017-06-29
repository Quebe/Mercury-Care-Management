using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Workflow.Activities;

using Mercury.Server.Workflows.EventArguments;
using Mercury.Server.Workflows.UserInteractions;

namespace Mercury.Server.Workflows {

    public class WorkflowService : IWorkflowService {

        public event EventHandler <UserInteractions.Request.RequestEventArgs> OnUserInteractionRequest;

        public event EventHandler <UserInteractions.Response.ResponseEventArgs> OnUserInteractionResponse;


        public void UserInteractionResponse (Object sender, Guid workflowInstanceId, UserInteractions.Response.ResponseBase response) {

            if (OnUserInteractionResponse != null) {

                OnUserInteractionResponse (sender, new Mercury.Server.Workflows.UserInteractions.Response.ResponseEventArgs (workflowInstanceId, response));

            }

            return;

        }

        public void UserInteractionRequest (UserInteractions.Request.RequestBase request) {

            if (OnUserInteractionRequest != null) {

                OnUserInteractionRequest (this, new Mercury.Server.Workflows.UserInteractions.Request.RequestEventArgs (request));

            }

            return;

        }

    }

}
