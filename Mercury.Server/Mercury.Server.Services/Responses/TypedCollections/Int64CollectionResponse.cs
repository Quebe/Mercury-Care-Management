using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {
    
    [DataContract (Name = "Int64CollectionResponse")]
    public class Int64CollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Int64> collection = new List<Int64> ();

        #endregion


        #region Public Properties

        public List<Int64> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}