using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "DirectoryEntryCollectionResponse")]
    public class DirectoryEntryCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Public.Interfaces.Security.DirectoryEntry> collection = new List<Server.Public.Interfaces.Security.DirectoryEntry> ();

        #endregion


        #region Public Properties

        public List<Server.Public.Interfaces.Security.DirectoryEntry> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}