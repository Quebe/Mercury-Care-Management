using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "RoutingRuleCollectionResponse")]
    public class RoutingRuleCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Work.RoutingRule> collection = new List<Server.Core.Work.RoutingRule> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Work.RoutingRule> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}
