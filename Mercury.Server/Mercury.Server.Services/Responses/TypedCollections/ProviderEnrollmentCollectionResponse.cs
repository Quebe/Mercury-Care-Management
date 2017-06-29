using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "ProviderEnrollmentCollectionResponse")]
    public class ProviderEnrollmentCollectionResponse : ResponseBase {

        [DataMember (Name = "Collection")]
        private List<Mercury.Server.Core.Provider.ProviderEnrollment> collection;

        public List<Mercury.Server.Core.Provider.ProviderEnrollment> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

    }

}
