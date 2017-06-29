using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "WorkQueueCollectionResponse")]
    public class WorkQueueCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Work.WorkQueue> collection = new List<Server.Core.Work.WorkQueue> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Work.WorkQueue> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}