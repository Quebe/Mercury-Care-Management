using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "DataExplorerCollectionResponse")]
    public class DataExplorerCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.DataExplorer.DataExplorer> collection = new List<Server.Core.DataExplorer.DataExplorer> ();

        #endregion


        #region Public Properties

        public List<Server.Core.DataExplorer.DataExplorer> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}