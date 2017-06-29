using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "ProviderAffiliationCollectionResponse")]
    public class ProviderAffiliationCollectionResponse : ResponseBase {

        [DataMember (Name = "Collection")]
        private List<Mercury.Server.Core.Provider.ProviderAffiliation> collection;

        public List<Mercury.Server.Core.Provider.ProviderAffiliation> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

    }

}
