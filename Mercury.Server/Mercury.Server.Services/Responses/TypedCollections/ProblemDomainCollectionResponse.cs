using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "ProblemDomainCollectionResponse")]
    public class ProblemDomainCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Individual.ProblemDomain> collection = new List<Server.Core.Individual.ProblemDomain> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Individual.ProblemDomain> Collection { get { return collection; } set { collection = value; } } 

        #endregion

    }

}