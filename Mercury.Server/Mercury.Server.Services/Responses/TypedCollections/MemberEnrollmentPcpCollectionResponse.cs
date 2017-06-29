using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "MemberEnrollmentPcpCollectionResponse")]
    public class MemberEnrollmentPcpCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Member.MemberEnrollmentPcp> collection = new List<Server.Core.Member.MemberEnrollmentPcp> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Member.MemberEnrollmentPcp> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}