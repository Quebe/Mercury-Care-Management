using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "MemberServiceDetailSetCollectionResponse")]
    public class MemberServiceDetailSetCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.MedicalServices.MemberServiceDetailSet> collection = new List<Server.Core.MedicalServices.MemberServiceDetailSet> ();

        #endregion


        #region Public Properties

        public List<Server.Core.MedicalServices.MemberServiceDetailSet> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}