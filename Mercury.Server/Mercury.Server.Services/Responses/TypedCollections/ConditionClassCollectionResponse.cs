using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "ConditionClassCollectionResponse")]
    public class ConditionClassCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]

        private List<Server.Core.Condition.ConditionClass> collection;

        #endregion


        #region Public Properties

        public List<Server.Core.Condition.ConditionClass> Collection { get { return collection; } set { collection = value; } }

        #endregion

    }

}
