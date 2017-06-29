using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;



namespace Mercury.Server.Workflows {

    [Serializable]
    [DataContract (Name = "WorkflowResponse")]
    public class WorkflowResponse {

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

        [DataMember (Name = "HasException")]
        private Boolean hasException = false;

        [NonSerialized]
        private Exception lastException = null;

        [NonSerialized]
        private Dictionary<String, Object> outputParameters = new Dictionary<String, Object> ();

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

        public Boolean HasException { get { return hasException; } }
         
        public void SetException (Exception forException) {

            hasException = (forException != null);

            lastException = forException;

            return;

        }

        public Exception LastException { get { return lastException; } }

        public Dictionary<String, Object> OutputParameters { get { return outputParameters; } set { if (value != null) { outputParameters = value; } else { outputParameters = new Dictionary<String, Object> (); } } }

        #endregion


        #region Constructor

        public WorkflowResponse () {

            // DO NOTHING

        }

        #endregion


    }

}
