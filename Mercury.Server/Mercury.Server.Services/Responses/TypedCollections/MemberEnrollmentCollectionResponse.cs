using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "MemberEnrollmentCollectionResponse")]
    public class MemberEnrollmentCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Member.MemberEnrollment> collection = new List<Server.Core.Member.MemberEnrollment> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Member.MemberEnrollment> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}