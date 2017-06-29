using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "EntityNoteCollectionResponse")]
    public class EntityNoteCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Entity.EntityNote> collection = new List<Server.Core.Entity.EntityNote> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Entity.EntityNote> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}
