using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "MemberServiceCollectionResponse")]
    public class MemberServiceCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.MedicalServices.MemberService> collection = new List<Server.Core.MedicalServices.MemberService> ();

        #endregion


        #region Public Properties

        public List<Server.Core.MedicalServices.MemberService> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}