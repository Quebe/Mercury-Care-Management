using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.DataExplorer.Evaluations {

    [Serializable]
    public class DataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate : DataExplorerNodeEvaluation {

        #region Private Properties

        private Int64 insurerId = 0;

        private Int64 programId = 0;

        private Int64 benefitPlanId = 0;

        private Int32 continuousAllowedGaps = 1;

        private Int32 continuousAllowedGapDays = 45;

        private Int32 continuousUntilAge = 0;

        #endregion


        #region Public Properties

        public override Server.Application.DataExplorerNodeResultDataType ResultDataType { get { return Server.Application.DataExplorerNodeResultDataType.Member; } }

        public Int64 InsurerId { get { return insurerId; } set { insurerId = value; } }

        public Int64 ProgramId { get { return programId; } set { programId = value; } }

        public Int64 BenefitPlanId { get { return benefitPlanId; } set { benefitPlanId = value; } }

        public Int32 ContinuousAllowedGaps { get { return continuousAllowedGaps; } set { continuousAllowedGaps = value; } }

        public Int32 ContinuousAllowedGapDays { get { return continuousAllowedGapDays; } set { continuousAllowedGapDays = value; } }

        public Int32 ContinuousUntilAge { get { return continuousUntilAge; } set { continuousUntilAge = value; } }

        #endregion



        #region Constructors

        protected void BaseConstructorMemberEnrollment () {

            NodeType = Server.Application.DataExplorerNodeType.Evaluation;

            EvaluationType = Server.Application.DataExplorerEvaluationType.MemberEnrollmentContinuousFromBirthDate;

            Evaluation = Server.Application.DataFilterOperator.IsEqualTo;

            return;

        }

        public DataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate (Application applicationReference) {

            BaseConstructor (applicationReference);

            BaseConstructorMemberEnrollment ();

            return;

        }

        public DataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate (Application applicationReference, Mercury.Server.Application.DataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate serverObject) {

            BaseConstructor (applicationReference, serverObject);

            BaseConstructorMemberEnrollment ();

            MapFromServerObject (serverObject);

            return;

        }

        #endregion


        #region Public Methods

        public void MapFromServerObject (Server.Application.DataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate serverObject) {

            base.MapFromServerObject ((Server.Application.DataExplorerNodeEvaluation)serverObject);


            InsurerId = serverObject.InsurerId;

            ProgramId = serverObject.ProgramId;

            BenefitPlanId = serverObject.BenefitPlanId;


            ContinuousUntilAge = serverObject.ContinuousUntilAge;

            ContinuousAllowedGaps = serverObject.ContinuousAllowedGaps;

            ContinuousAllowedGapDays = serverObject.ContinuousAllowedGapDays;

            return;

        }
        public virtual void MapToServerObject (Server.Application.DataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate serverObject) {

            base.MapToServerObject ((Server.Application.DataExplorerNodeEvaluation)serverObject);


            serverObject.InsurerId = insurerId;

            serverObject.ProgramId = programId;

            serverObject.BenefitPlanId = benefitPlanId;


            serverObject.ContinuousUntilAge = ContinuousUntilAge;

            serverObject.ContinuousAllowedGaps = ContinuousAllowedGaps;

            serverObject.ContinuousAllowedGapDays = ContinuousAllowedGapDays;


            return;

        }

        public override Object ToServerObject () {

            Server.Application.DataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate serverObject = new Server.Application.DataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public new DataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate Copy () {

            Server.Application.DataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate serverObject = (Server.Application.DataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate)ToServerObject ();

            DataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate copiedObject = new DataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (DataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate compareObject) {

            Boolean isEqual = base.IsEqual ((DataExplorerNodeEvaluation)compareObject);


            isEqual &= (InsurerId == compareObject.InsurerId);

            isEqual &= (ProgramId == compareObject.ProgramId);

            isEqual &= (BenefitPlanId == compareObject.BenefitPlanId);


            isEqual &= (ContinuousUntilAge == compareObject.ContinuousUntilAge);

            isEqual &= (ContinuousAllowedGaps == compareObject.ContinuousAllowedGaps);

            isEqual &= (ContinuousAllowedGapDays == compareObject.ContinuousAllowedGapDays);
            

            return isEqual;

        }

        #endregion

    }

}
