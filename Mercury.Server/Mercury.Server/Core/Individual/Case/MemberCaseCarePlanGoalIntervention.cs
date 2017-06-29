using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual.Case {

    [DataContract (Name = "MemberCaseCarePlanGoalIntervention")]
    public class MemberCaseCarePlanGoalIntervention : CoreObject {
        
        #region Private Properties

        [DataMember (Name = "MemberCaseCarePlanGoalId")]
        private Int64 memberCaseCarePlanGoalId = 0;

        [DataMember (Name = "MemberCaseCareInterventionId")]
        private Int64 memberCaseCareInterventionId = 0;

        [DataMember (Name = "Inclusion")]
        private Enumerations.CarePlanItemInclusion inclusion = Enumerations.CarePlanItemInclusion.Suggested;

        [DataMember (Name = "IsSingleInstance")]
        private Boolean isSingleInstance = false;

        private MemberCaseCarePlanGoal memberCaseCarePlanGoal = null;

        #endregion


        #region Public Properties - Encapsulated

        public Int64 MemberCaseCarePlanGoalId { get { return memberCaseCarePlanGoalId; } set { memberCaseCarePlanGoalId = value; } }

        public Int64 MemberCaseCareInterventionId { get { return memberCaseCareInterventionId; } set { memberCaseCareInterventionId = value; } }

        public Enumerations.CarePlanItemInclusion Inclusion { get { return inclusion; } set { inclusion = value; } }

        public Boolean IsSingleInstance { get { return isSingleInstance; } set { isSingleInstance = value; } }

        public MemberCaseCarePlanGoal MemberCaseCarePlanGoal { get { return memberCaseCarePlanGoal; } set { memberCaseCarePlanGoal = value; } }

        #endregion 
        
        
        #region Constructors
        
        public MemberCaseCarePlanGoalIntervention (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public MemberCaseCarePlanGoalIntervention (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference);


            base.BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion


        #region Data Functions
        
        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            MemberCaseCarePlanGoalId = Convert.ToInt64 (currentRow["MemberCaseCarePlanGoalId"]);

            MemberCaseCareInterventionId = base.IdFromSql (currentRow, "MemberCaseCareInterventionId");

            Inclusion = (Enumerations.CarePlanItemInclusion)Convert.ToInt32 (currentRow["Inclusion"]);

            IsSingleInstance = Convert.ToBoolean (currentRow["IsSingleInstance"]);

            return;

        }

        #endregion 

    }

}
