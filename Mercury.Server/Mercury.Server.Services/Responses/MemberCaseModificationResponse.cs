using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses {

    [DataContract (Name = "MemberCaseModificationResponse")]
    public class MemberCaseModificationResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "MemberCase")]
        private Server.Core.Individual.Case.MemberCase memberCase = null;

        [DataMember (Name = "SaveOutcome")]
        private Server.Core.Individual.Enumerations.MemberCaseActionOutcome modificationOutcome = Server.Core.Individual.Enumerations.MemberCaseActionOutcome.UnknownError;

        #endregion


        #region Public Properties

        public Server.Core.Individual.Case.MemberCase MemberCase { get { return memberCase; } set { memberCase = value; } }

        public Server.Core.Individual.Enumerations.MemberCaseActionOutcome ModificationOutcome { get { return modificationOutcome; } set { modificationOutcome = value; } }

        #endregion 

    }

}