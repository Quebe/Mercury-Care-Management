using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "ProblemStatementCollectionResponse")]
    public class ProblemStatementCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]

        private List<Server.Core.Individual.ProblemStatement> collection;

        #endregion


        #region Public Properties

        public List<Server.Core.Individual.ProblemStatement> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}
