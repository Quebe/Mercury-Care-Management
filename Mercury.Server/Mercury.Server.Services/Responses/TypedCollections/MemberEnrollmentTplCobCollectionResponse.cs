using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "MemberEnrollmentTplCobCollectionResponse")]
    public class MemberEnrollmentTplCobCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Member.MemberEnrollmentTplCob> collection = new List<Server.Core.Member.MemberEnrollmentTplCob> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Member.MemberEnrollmentTplCob> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}