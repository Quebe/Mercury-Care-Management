using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "ProviderContractCollectionResponse")]
    public class ProviderContractCollectionResponse : ResponseBase {

        [DataMember (Name = "Collection")]
        private List<Mercury.Server.Core.Provider.ProviderContract> collection;

        public List<Mercury.Server.Core.Provider.ProviderContract> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

    }

}
