using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "ProgramCollectionResponse")]
    public class ProgramCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Insurer.Program> collection = new List<Server.Core.Insurer.Program> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Insurer.Program> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}