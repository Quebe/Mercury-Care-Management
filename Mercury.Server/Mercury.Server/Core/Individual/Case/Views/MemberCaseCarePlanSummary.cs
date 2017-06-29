using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual.Case.Views {

    [Serializable]
    [DataContract (Name = "MemberCaseCarePlanSummary")]
    public class MemberCaseCarePlanSummary : CoreExtensibleObject {


        #region Private Properties

        [DataMember (Name = "MemberCaseId")]
        private Int64 memberCaseId;

        [DataMember (Name = "MemberCarePlanId")]
        private Int64 memberCarePlanId;

        [DataMember (Name = "ProblemStatementId")]
        private Int64 problemStatementId;

        [DataMember (Name = "CarePlanId")]
        private Int64 carePlanId;

        [DataMember (Name = "ProviderId")]
        private Int64 providerId;

        [DataMember (Name = "ProblemStatementText")]
        private String problemStatementText;

        [DataMember (Name = "CarePlanName")]
        private String carePlanName;

        [DataMember (Name = "ProviderName")]
        private String providerName;

        // ...

        [DataMember (Name = "NextObjectiveName")]
        private String nextObjectiveName;

        [DataMember (Name = "NextInterventionName")]
        private String nextInterventionName;

        // ...

        [DataMember (Name = "EffectiveDate")]
        private DateTime effectiveDate;

        [DataMember (Name = "TerminationDate")]
        private DateTime terminationDate;

        #endregion


        #region Public Properties

        public Int64 MemberCaseId { get { return memberCaseId; } set { memberCaseId = value; } }

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

        public MemberCaseCarePlanSummary (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        #endregion


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            id = base.IdFromSql (currentRow, "MemberCaseId");

            memberCarePlanId = base.IdFromSql (currentRow, "MemberCarePlanId");

            problemStatementId = base.IdFromSql (currentRow, "ProblemStatementId");

            carePlanId = base.IdFromSql (currentRow, "CarePlanId");

            providerId = base.IdFromSql (currentRow, "ProviderId");

            problemStatementText = (String)currentRow["ProblemStatementText"];

            carePlanName = (String)currentRow["CarePlanName"];

            providerName = (String)currentRow["ProviderName"];

            // ...

            nextObjectiveName = (String)currentRow["NextObjectiveName"];

            nextInterventionName = (String)currentRow["NextInterventionName"];

            // ...

            effectiveDate = (DateTime)currentRow["EffectiveDate"];

            terminationDate = (DateTime)currentRow["TerminationDate"];

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        #endregion


    }

}