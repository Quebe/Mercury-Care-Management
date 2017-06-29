using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract]
    public class EnvironmentCollectionResponse : ResponseBase {

        [DataMember (Name = "Collection")]
        private List<Mercury.Server.Environment.Environment> collection = new List<Mercury.Server.Environment.Environment> ();


        public List<Mercury.Server.Environment.Environment> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

    }

}
