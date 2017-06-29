using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses {

    [DataContract (Name = "MemberCaseCreateResponse")]
    public class MemberCaseCreateResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "MemberCase")]
        private Server.Core.Individual.Case.MemberCase memberCase = null;

        #endregion


        #region Public Properties

        public Server.Core.Individual.Case.MemberCase MemberCase { get { return memberCase; } set { memberCase = value; } }

        #endregion 

    }

}