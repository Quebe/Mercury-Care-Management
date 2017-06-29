using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "EntityNoteContentCollectionResponse")]
    public class EntityNoteContentCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Entity.EntityNoteContent> collection = new List<Server.Core.Entity.EntityNoteContent> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Entity.EntityNoteContent> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}