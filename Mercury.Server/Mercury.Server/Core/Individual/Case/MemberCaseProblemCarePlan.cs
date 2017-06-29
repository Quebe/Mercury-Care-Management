using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual.Case {

    [DataContract (Name = "MemberCaseProblemCarePlan")]
    public class MemberCaseProblemCarePlan : CoreObject {

        #region Private Properties

        [DataMember (Name = "MemberCaseProblemClassId")]
        private Int64 memberCaseProblemClassId;

        private MemberCaseProblemClass memberCaseProblemClass = null;

        [DataMember (Name = "ProblemStatementId")]
        private Int64 problemStatementId;   // PROBLEM STATEMENT

        [DataMember (Name = "MemberCaseCarePlanId")]
        private Int64 memberCaseCarePlanId; // MEMBER CASE CARE PLAN ID

        [DataMember (Name = "IsSingleInstance")]
        private Boolean isSingleInstance = false;

        #endregion 


        #region Public Properties

        public Int64 MemberCaseProblemClassId { get { return memberCaseProblemClassId; } set { memberCaseProblemClassId = value; } }

        public MemberCaseProblemClass MemberCaseProblemClass { get { return memberCaseProblemClass; } set { memberCaseProblemClass = value; } }

        public Int64 ProblemStatementId { get { return problemStatementId; } set { problemStatementId = value; } }

        public Int64 MemberCaseCarePlanId { get { return memberCaseCarePlanId; } set { memberCaseCarePlanId = value; } }

        public Boolean IsSingleInstance { get { return isSingleInstance; } set { isSingleInstance = value; } }

        #endregion 

        
        #region Constructors

        public MemberCaseProblemCarePlan (Application applicationReference) {

            BaseConstructor (applicationReference);

            return; 
        
        }

        public MemberCaseProblemCarePlan (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion

        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            MemberCaseProblemClassId = base.IdFromSql (currentRow, "MemberCaseProblemClassId");

            ProblemStatementId = base.IdFromSql (currentRow, "ProblemStatementId");

            MemberCaseCarePlanId = base.IdFromSql (currentRow, "MemberCaseCarePlanId");

            IsSingleInstance = (Boolean)currentRow["IsSingleInstance"];

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        #endregion 


    }

}
