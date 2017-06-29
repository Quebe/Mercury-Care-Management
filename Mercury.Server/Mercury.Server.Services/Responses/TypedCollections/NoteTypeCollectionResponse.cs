using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "NoteTypeCollectionResponse")]
    public class NoteTypeCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Reference.NoteType> collection = new List<Server.Core.Reference.NoteType> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Reference.NoteType> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}