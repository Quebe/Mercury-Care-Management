using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "ActionCollectionResponse")]
    public class ActionCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Action.Action> collection = new List<Server.Core.Action.Action> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Action.Action> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}
