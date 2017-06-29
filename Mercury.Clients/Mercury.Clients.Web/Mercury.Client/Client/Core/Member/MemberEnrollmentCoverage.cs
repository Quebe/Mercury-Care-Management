using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Member {

    [Serializable]
    public class MemberEnrollmentCoverage : CoreObject {

        #region Private Properties

        private Int64 memberEnrollmentId;

        private Int64 benefitPlanId;

        private Int64 coverageTypeId;

        private Int64 coverageLevelId;

        private String rateCode;

        private DateTime effectiveDate;

        private DateTime terminationDate;

        #endregion


        #region Public Properties

        public Int64 MemberEnrollmentId { get { return memberEnrollmentId; } set { memberEnrollmentId = value; } }

        public Int64 BenefitPlanId { get { return benefitPlanId; } set { benefitPlanId = value; } }

        public Int64 CoverageTypeId { get { return coverageTypeId; } set { coverageTypeId = value; } }

        public Int64 CoverageLevelId { get { return coverageLevelId; } set { coverageLevelId = value; } }

        public String RateCode { get { return rateCode; } set { rateCode = value; } }

        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }

        #endregion


        #region Public Object Properties

        public Insurer.BenefitPlan BenefitPlan { get { return application.BenefitPlanGet (benefitPlanId, true); } }

        public String BenefitPlanName { get { return ((BenefitPlan != null) ? BenefitPlan.Name : String.Empty); } }
        
        public Insurer.CoverageType CoverageType { get { return application.CoverageTypeGet (coverageTypeId, true); } }

        public String CoverageTypeName { get { return ((CoverageType != null) ? CoverageType.Name : String.Empty); } }

        public Insurer.CoverageLevel CoverageLevel { get { return application.CoverageLevelGet (coverageLevelId, true); } }

        public String CoverageLevelName { get { return ((CoverageLevel != null) ? CoverageLevel.Name : String.Empty); } }

        #endregion 


        #region Constructors

        public MemberEnrollmentCoverage (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public MemberEnrollmentCoverage (Application applicationReference, Server.Application.MemberEnrollmentCoverage serverMemberEnrollmentCoverage) {

            BaseConstructor (applicationReference, serverMemberEnrollmentCoverage);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.MemberEnrollmentCoverage serverMemberEnrollmentCoverage) {

            base.BaseConstructor (applicationReference, serverMemberEnrollmentCoverage);


            memberEnrollmentId = serverMemberEnrollmentCoverage.MemberEnrollmentId;

            benefitPlanId = serverMemberEnrollmentCoverage.BenefitPlanId;

            coverageTypeId = serverMemberEnrollmentCoverage.CoverageTypeId;

            coverageLevelId = serverMemberEnrollmentCoverage.CoverageLevelId;

            rateCode = serverMemberEnrollmentCoverage.RateCode;
            

            effectiveDate = serverMemberEnrollmentCoverage.EffectiveDate;

            terminationDate = serverMemberEnrollmentCoverage.TerminationDate;

            return;

        }

        #endregion

    }

}
