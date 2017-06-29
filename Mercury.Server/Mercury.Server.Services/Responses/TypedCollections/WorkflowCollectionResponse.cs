using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections
{

    [DataContract(Name = "WorkflowCollectionResponse")]
    public class WorkflowCollectionResponse : ResponseBase
    {

        #region Private Properties

        [DataMember(Name = "Collection")]
        private List<Server.Core.Work.Workflow> collection = new List<Server.Core.Work.Workflow>();

        #endregion


        #region Public Properties

        public List<Server.Core.Work.Workflow> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}