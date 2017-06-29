using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual.Case {

    [DataContract (Name = "MemberCaseCareInterventionActivity ")]
    public class MemberCaseCareInterventionActivity : CareInterventionActivity {

        #region Private Properties

        [DataMember (Name = "MemberCaseCareInterventionId")]
        private Int64 memberCaseCareInterventionId = 0;

        [DataMember (Name = "CareInterventionActivityId")]
        private Int64 careInterventionActivityId = 0;

        private MemberCaseCareIntervention memberCaseCareIntervention = null;

        #endregion


        #region Public Properties - Encapsulated

        public Int64 MemberCaseCareInterventionId { get { return memberCaseCareInterventionId; } set { memberCaseCareInterventionId = value; } }

        public Int64 CareInterventionActivityId { get { return careInterventionActivityId; } set { careInterventionActivityId = value; } }

        public MemberCaseCareIntervention MemberCaseCareIntervention { get { return memberCaseCareIntervention; } set { memberCaseCareIntervention = value; } }

        #endregion


        #region Constructors

        public MemberCaseCareInterventionActivity (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public MemberCaseCareInterventionActivity (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference);


            base.BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            MemberCaseCareInterventionId = Convert.ToInt64 (currentRow["MemberCaseCareInterventionId"]);

            CareInterventionActivityId = base.IdFromSql (currentRow, "CareInterventionActivityId");


            return;

        }

        #endregion

    }

}
