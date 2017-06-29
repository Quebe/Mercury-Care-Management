using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Workflow.Activities;

namespace Mercury.Server.Workflows.UserInteractions.Response {

    [Serializable]
    public class ResponseEventArgs : ExternalDataEventArgs {

        #region Private Properties

        private ResponseBase response;

        #endregion


        #region Public Properties

        public ResponseBase Response { get { return response; } set { response = value; } }

        #endregion


        #region Constructors

        public ResponseEventArgs (Guid workflowInstanceId, Workflows.UserInteractions.Response.ResponseBase userInteractionResponse) : base (workflowInstanceId) {

            response = userInteractionResponse;

            return;

        }

        #endregion 

    }

}
