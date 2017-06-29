using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "ContractCollectionResponse")]
    public class ContractCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Insurer.Contract> collection = new List<Server.Core.Insurer.Contract> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Insurer.Contract> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}