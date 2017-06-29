using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "ConditionCollectionResponse")]
    public class ConditionCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]

        private List<Server.Core.Condition.Condition> collection;

        #endregion


        #region Public Properties

        public List<Server.Core.Condition.Condition> Collection { get { return collection; } set { collection = value; } } 

        #endregion

    }

}
