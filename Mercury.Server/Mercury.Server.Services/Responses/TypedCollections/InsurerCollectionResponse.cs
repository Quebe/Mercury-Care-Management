using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "InsurerCollectionResponse")]
    public class InsurerCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Insurer.Insurer> collection = new List<Server.Core.Insurer.Insurer> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Insurer.Insurer> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}