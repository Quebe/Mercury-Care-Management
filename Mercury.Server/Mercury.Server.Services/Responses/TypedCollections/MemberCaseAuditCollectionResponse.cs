using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "MemberCaseAuditCollectionResponse")]
    public class MemberCaseAuditCollectionResponse : ResponseBase {


        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Individual.Case.MemberCaseAudit> collection = new List<Server.Core.Individual.Case.MemberCaseAudit> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Individual.Case.MemberCaseAudit> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion


    }

}