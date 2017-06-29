using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "MetricCollectionResponse")]
    public class MetricCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Metrics.Metric> collection = new List<Server.Core.Metrics.Metric> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Metrics.Metric> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}