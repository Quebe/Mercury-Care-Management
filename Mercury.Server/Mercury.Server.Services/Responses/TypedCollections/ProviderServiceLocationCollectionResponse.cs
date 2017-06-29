using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "ProviderServiceLocationCollectionResponse")]
    public class ProviderServiceLocationCollectionResponse : ResponseBase {

        [DataMember (Name = "Collection")]
        private List<Mercury.Server.Core.Provider.ProviderServiceLocation> collection;

        public List<Mercury.Server.Core.Provider.ProviderServiceLocation> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

    }

}
