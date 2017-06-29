using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Workflows {

    [DataContract (Name = "WorkflowStartRequest")]
    public class WorkflowStartRequest {

        #region Private Properties

        [DataMember (Name = "WorkQueueItemId")]
        private Int64 workQueueItemId = 0;

        [DataMember (Name = "WorkflowId")]
        private Int64 workflowId = 0;

        [DataMember (Name = "WorkflowName")]
        private String workflowName;

        [DataMember (Name = "WorkflowInstanceId")]
        private Guid workflowInstanceId = Guid.Empty;

        [DataMember (Name = "Arguments")]
        private Dictionary<String, Object> arguments = new Dictionary<String, Object> ();

        #endregion 


        #region Public Properties

        public Int64 WorkQueueItemId { get { return workQueueItemId; } set { workQueueItemId = value; } }

        public Int64 WorkflowId { get { return workflowId; } set { workflowId = value; } }

        public String WorkflowName { get { return workflowName; } set { workflowName = value; } }

        public Guid WorkflowInstanceId { get { return workflowInstanceId; } set { workflowInstanceId = value; } }

        public Dictionary<String, Object> Arguments { get { return arguments; } set { arguments = value; } }

        #endregion 


        #region Constructors

        public WorkflowStartRequest (Int64 forWorkflowId) { workflowId = forWorkflowId; return; }

        public WorkflowStartRequest (String forWorkflowName) { workflowName = forWorkflowName; return; }

        public WorkflowStartRequest (Core.Work.Workflow workflow) {

            workflowId = workflow.Id;

            workflowName = workflow.Name;

            return;

        }

        #endregion 

    }

}
