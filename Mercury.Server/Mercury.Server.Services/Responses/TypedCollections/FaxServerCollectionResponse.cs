using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "FaxServerCollectionResponse")]
    public class FaxServerCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Faxing.FaxServer> collection = new List<Server.Faxing.FaxServer> ();

        #endregion


        #region Public Properties

        public List<Server.Faxing.FaxServer> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}