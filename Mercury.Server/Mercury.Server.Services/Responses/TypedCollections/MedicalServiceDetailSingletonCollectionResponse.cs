using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "MedicalServiceDetailSingletonCollectionResponse")]
    public class MedicalServiceDetailSingletonCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.MedicalServices.MemberServiceDetailSingleton> collection = new List<Server.Core.MedicalServices.MemberServiceDetailSingleton> ();

        #endregion


        #region Public Properties

        public List<Server.Core.MedicalServices.MemberServiceDetailSingleton> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}
