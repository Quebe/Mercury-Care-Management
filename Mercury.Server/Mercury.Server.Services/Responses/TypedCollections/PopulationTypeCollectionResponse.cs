using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "PopulationTypeCollectionResponse")]
    public class PopulationTypeCollectionResponse : ResponseBase {

        [DataMember (Name = "Collection")]
        private List<Mercury.Server.Core.Population.PopulationType> collection;

        public List<Mercury.Server.Core.Population.PopulationType> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

    }

}
