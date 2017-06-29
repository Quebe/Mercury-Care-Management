using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "MemberMetricCollectionResponse")]
    public class MemberMetricCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Metrics.MemberMetric> collection = new List<Server.Core.Metrics.MemberMetric> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Metrics.MemberMetric> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}