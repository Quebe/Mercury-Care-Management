using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "MemberProblemStatementIdentifiedCollectionResponse")]
    public class MemberProblemStatementIdentifiedCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Individual.MemberProblemStatementIdentified> collection = new List<Server.Core.Individual.MemberProblemStatementIdentified> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Individual.MemberProblemStatementIdentified> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}