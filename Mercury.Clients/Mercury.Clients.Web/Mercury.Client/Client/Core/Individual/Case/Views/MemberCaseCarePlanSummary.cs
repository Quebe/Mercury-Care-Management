using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual.Case.Views {

    [Serializable]
    public class MemberCaseCarePlanSummary : CoreObject {


        #region Private Properties

        //private Int64 memberCaseId;

        private Int64 memberCarePlanId;

        private Int64 problemStatementId;

        private Int64 carePlanId;

        private Int64 providerId;

        private String problemStatementText = String.Empty;

        private String carePlanName = String.Empty;

        private String providerName = String.Empty;

        // ...

        private String nextObjectiveName = String.Empty;

        private String nextInterventionName = String.Empty;

        // ...

        private DateTime effectiveDate = new DateTime (1900, 1, 1);

        private DateTime terminationDate = new DateTime (9999, 12, 31);

        #endregion


        #region Public Properties - Encapsulated

        //public Int64 MemberCaseId { get { return memberCaseId; } set { memberCaseId = value; } }

        public Int64 MemberCarePlanId { get { return memberCarePlanId; } set { memberCarePlanId = value; } }

        public Int64 ProblemStatementId { get { return problemStatementId; } set { problemStatementId = value; } }

        public Int64 CarePlanId { get { return carePlanId; } set { carePlanId = value; } }

        public Int64 ProviderId { get { return providerId; } set { providerId = value; } }

        public String ProblemStatementText { get { return problemStatementText; } set { problemStatementText = value; } }

        public String CarePlanName { get { return carePlanName; } set { carePlanName = value; } }

        public String ProviderName { get { return providerName; } set { providerName = value; } }

        // ...

        public String NextObjectiveName { get { return nextObjectiveName; } set { nextObjectiveName = value; } }

        public String NextInterventionName { get { return nextInterventionName; } set { nextInterventionName = value; } }

        // ...

        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }

        #endregion


        #region Constructors

        public MemberCaseCarePlanSummary (Application applicationReference, Server.Application.MemberCaseCarePlanSummary serverObject) {

            BaseConstructor (applicationReference, serverObject);

            Id = serverObject.Id;

            MemberCarePlanId = serverObject.MemberCarePlanId;

            ProblemStatementId = serverObject.ProblemStatementId;

            CarePlanId = serverObject.CarePlanId;

            ProviderId = serverObject.ProviderId;

            ProblemStatementText = serverObject.ProblemStatementText;

            CarePlanName = serverObject.CarePlanName;

            ProviderName = serverObject.ProviderName;

            // ...

            NextObjectiveName = serverObject.NextObjectiveName;

            NextInterventionName = serverObject.NextInterventionName;

            // ...

            EffectiveDate = serverObject.EffectiveDate;

            TerminationDate = serverObject.TerminationDate;

            return;

        }

        #endregion


    }

}