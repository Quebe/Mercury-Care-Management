using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses {

    [DataContract (Name = "WorkflowResponse")]
    public class WorkflowResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "WorkQueueItemId")]
        private Int64 workQueueItemId = 0;

        [DataMember (Name = "Workflow")]
        private Mercury.Server.Core.Work.Workflow workflow = null;

        [DataMember (Name = "WorkflowInstanceId")]
        private Guid workflowInstanceId;

        [DataMember (Name = "WorkflowStatus")]
        private Mercury.Server.Workflows.WorkflowStatus workflowStatus = Mercury.Server.Workflows.WorkflowStatus.Unloaded;

        [DataMember (Name = "WorkflowMessage")]
        private String workflowMessage = String.Empty;

        [DataMember (Name = "ChainWorkQueueItemId")]
        private Int64 chainWorkQueueItemId = 0;

        [DataMember (Name = "WorkflowSteps")]
        private List<Server.Workflows.WorkflowStep> workflowSteps = new List<Mercury.Server.Workflows.WorkflowStep> ();

        [DataMember (Name = "UserInteractionRequest")]
        private Mercury.Server.Workflows.UserInteractions.Request.RequestBase userInteractionRequest = new Mercury.Server.Workflows.UserInteractions.Request.RequestBase ();

        #endregion


        #region Public Properties

        public Int64 WorkQueueItemId { get { return workQueueItemId; } set { workQueueItemId = value; } }

        public Mercury.Server.Core.Work.Workflow Workflow { get { return workflow; } set { workflow = value; } }

        public Guid WorkflowInstanceId { get { return workflowInstanceId; } set { workflowInstanceId = value; } }

        public Mercury.Server.Workflows.WorkflowStatus WorkflowStatus { get { return workflowStatus; } set { workflowStatus = value; } }

        public String WorkflowMessage { get { return workflowMessage; } set { workflowMessage = value; } }

        public Int64 ChainWorkQueueItemId { get { return chainWorkQueueItemId; } set { chainWorkQueueItemId = value; } }

        public List<Server.Workflows.WorkflowStep> WorkflowSteps { get { return workflowSteps; } set { workflowSteps = value; } }

        public Mercury.Server.Workflows.UserInteractions.Request.RequestBase UserInteractionRequest { get { return userInteractionRequest; } set { userInteractionRequest = value; } }

        #endregion


        #region Constructor

        public WorkflowResponse () { /* DO NOTHING */ }

        public WorkflowResponse (Server.Workflows.WorkflowResponse response) {

            workQueueItemId = response.WorkQueueItemId;

            workflow = response.Workflow;

            workflowInstanceId = response.WorkflowInstanceId;

            workflowStatus = response.WorkflowStatus;
                    
            workflowMessage = response.WorkflowMessage;

            chainWorkQueueItemId = response.ChainWorkQueueItemId;

            workflowSteps = response.WorkflowSteps;

            userInteractionRequest = response.UserInteractionRequest;

            SetException (response.LastException);

            return;

        }

        #endregion

    }

}