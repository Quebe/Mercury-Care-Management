using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "ProblemClassCollectionResponse")]
    public class ProblemClassCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Individual.ProblemClass> collection = new List<Server.Core.Individual.ProblemClass> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Individual.ProblemClass> Collection { get { return collection; } set { collection = value; } }

        #endregion

    }

}