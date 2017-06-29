using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.DataExplorer.Evaluations {

    [Serializable]
    public class DataExplorerNodeEvaluationMemberEnrollment : DataExplorerNodeEvaluation, IDataExplorerNodeEvaluationDate {

        #region Private Properties
        
        private Int64 insurerId = 0;

        private Int64 programId = 0;

        private Int64 benefitPlanId = 0;

        private Boolean continuousEnrollment = false;

        private Int32 continuousAllowedGaps = 1;

        private Int32 continuousAllowedGapDays = 45;

        private DataExplorerNodeEvaluationDate dateCriteria = null;

        #endregion


        #region Public Properties

        public override Server.Application.DataExplorerNodeResultDataType ResultDataType { get { return Server.Application.DataExplorerNodeResultDataType.Member; } }

        public Int64 InsurerId { get { return insurerId; } set { insurerId = value; } }

        public Int64 ProgramId { get { return programId; } set { programId = value; } }

        public Int64 BenefitPlanId { get { return benefitPlanId; } set { benefitPlanId = value; } }

        public Boolean ContinuousEnrollment { get { return continuousEnrollment; } set { continuousEnrollment = value; } }

        public Int32 ContinuousAllowedGaps { get { return continuousAllowedGaps; } set { continuousAllowedGaps = value; } }

        public Int32 ContinuousAllowedGapDays { get { return continuousAllowedGapDays; } set { continuousAllowedGapDays = value; } }

        public DataExplorerNodeEvaluationDate DateCriteria { get { return dateCriteria; } set { dateCriteria = value; } }

        #endregion



        #region Constructors

        protected void BaseConstructorMemberEnrollment () {

            NodeType = Server.Application.DataExplorerNodeType.Evaluation;

            EvaluationType = Server.Application.DataExplorerEvaluationType.MemberEnrollment;

            Evaluation = Server.Application.DataFilterOperator.IsEqualTo;

            DateCriteria = new DataExplorerNodeEvaluationDate (this);

            return;

        }

        public DataExplorerNodeEvaluationMemberEnrollment (Application applicationReference) {

            BaseConstructor (applicationReference);

            BaseConstructorMemberEnrollment ();

            return;

        }

        public DataExplorerNodeEvaluationMemberEnrollment (Application applicationReference, Mercury.Server.Application.DataExplorerNodeEvaluationMemberEnrollment serverObject) {

            BaseConstructor (applicationReference, serverObject);

            BaseConstructorMemberEnrollment ();

            MapFromServerObject (serverObject);

            return;

        }

        #endregion


        #region Public Methods

        public void MapFromServerObject (Server.Application.DataExplorerNodeEvaluationMemberEnrollment serverObject) {

            base.MapFromServerObject ((Server.Application.DataExplorerNodeEvaluation)serverObject);


            InsurerId = serverObject.InsurerId;

            ProgramId = serverObject.ProgramId;

            BenefitPlanId = serverObject.BenefitPlanId;


            ContinuousEnrollment = serverObject.ContinuousEnrollment;

            ContinuousAllowedGaps = serverObject.ContinuousAllowedGaps;

            ContinuousAllowedGapDays = serverObject.ContinuousAllowedGapDays;


            DateCriteria = new DataExplorerNodeEvaluationDate (this, serverObject.DateCriteria);


            return;

        }
        public virtual void MapToServerObject (Server.Application.DataExplorerNodeEvaluationMemberEnrollment serverObject) {

            base.MapToServerObject ((Server.Application.DataExplorerNodeEvaluation)serverObject);


            serverObject.InsurerId = insurerId;

            serverObject.ProgramId = programId;

            serverObject.BenefitPlanId = benefitPlanId;


            serverObject.ContinuousEnrollment = ContinuousEnrollment;

            serverObject.ContinuousAllowedGaps = ContinuousAllowedGaps;

            serverObject.ContinuousAllowedGapDays = ContinuousAllowedGapDays;


            serverObject.DateCriteria = (Server.Application.DataExplorerNodeEvaluationDate)DateCriteria.ToServerObject ();


            return;

        }

        public override Object ToServerObject () {

            Server.Application.DataExplorerNodeEvaluationMemberEnrollment serverObject = new Server.Application.DataExplorerNodeEvaluationMemberEnrollment ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public new DataExplorerNodeEvaluationMemberEnrollment Copy () {

            Server.Application.DataExplorerNodeEvaluationMemberEnrollment serverObject = (Server.Application.DataExplorerNodeEvaluationMemberEnrollment)ToServerObject ();

            DataExplorerNodeEvaluationMemberEnrollment copiedObject = new DataExplorerNodeEvaluationMemberEnrollment (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (DataExplorerNodeEvaluationMemberEnrollment compareObject) {

            Boolean isEqual = base.IsEqual ((DataExplorerNodeEvaluation)compareObject);


            isEqual &= (InsurerId == compareObject.InsurerId);

            isEqual &= (ProgramId == compareObject.ProgramId);

            isEqual &= (BenefitPlanId == compareObject.BenefitPlanId);


            isEqual &= (ContinuousEnrollment == compareObject.ContinuousEnrollment);

            isEqual &= (ContinuousAllowedGaps == compareObject.ContinuousAllowedGaps);

            isEqual &= (ContinuousAllowedGapDays == compareObject.ContinuousAllowedGapDays);


            isEqual &= (DateCriteria.IsEqual (compareObject.DateCriteria));


            return isEqual;

        }

        #endregion

    }

}
