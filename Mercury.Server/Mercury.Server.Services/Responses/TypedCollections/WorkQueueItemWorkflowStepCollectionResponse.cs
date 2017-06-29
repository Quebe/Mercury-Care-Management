using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "WorkQueueItemWorkflowStepCollectionResponse")]
    public class WorkQueueItemWorkflowStepCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Workflows.WorkflowStep> collection = new List<Server.Workflows.WorkflowStep> ();

        #endregion


        #region Public Properties

        public List<Server.Workflows.WorkflowStep> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}