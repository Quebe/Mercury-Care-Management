using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "WorkQueueViewCollectionResponse")]
    public class WorkQueueViewCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Work.WorkQueueView> collection = new List<Server.Core.Work.WorkQueueView> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Work.WorkQueueView> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}