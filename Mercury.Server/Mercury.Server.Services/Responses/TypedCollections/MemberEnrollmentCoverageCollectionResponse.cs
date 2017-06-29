using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "MemberEnrollmentCoverageCollectionResponse")]
    public class MemberEnrollmentCoverageCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Member.MemberEnrollmentCoverage> collection = new List<Server.Core.Member.MemberEnrollmentCoverage> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Member.MemberEnrollmentCoverage> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}