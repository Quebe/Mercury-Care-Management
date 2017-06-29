using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Mercury.Client.Core.Member {

    public class MemberEnrollmentCoverage : CoreObject {

        #region Private Properties

        private Int64 memberEnrollmentId;

        private Int64 benefitPlanId;

        private Int64 coverageTypeId;

        private Int64 coverageLevelId;

        private String rateCode;

        private DateTime effectiveDate;

        private DateTime terminationDate;


        private Server.Application.BenefitPlan benefitPlan = null;

        private Server.Application.CoverageType coverageType = null;

        private Server.Application.CoverageLevel coverageLevel = null;        

        #endregion


        #region Public Properties

        public Int64 MemberEnrollmentId { get { return memberEnrollmentId; } set { memberEnrollmentId = value; } }

        public Int64 BenefitPlanId { get { return benefitPlanId; } set { benefitPlanId = value; } }

        public Int64 CoverageTypeId { get { return coverageTypeId; } set { coverageTypeId = value; } }

        public Int64 CoverageLevelId { get { return coverageLevelId; } set { coverageLevelId = value; } }

        public String RateCode { get { return rateCode; } set { rateCode = value; } }


        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }

        public String EffectiveDateDescription { get { return EffectiveDate.ToString ("MM/dd/yyyy"); } }

        public String TerminationDateDescription { get { return ((TerminationDate == new DateTime (9999, 12, 31)) ? "< active >" : TerminationDate.ToString ("MM/dd/yyyy")); } }

        #endregion


        #region Public Object Properties

        public Server.Application.BenefitPlan BenefitPlan {

            get {

                if (benefitPlan == null) {

                    GlobalProgressBarShow ("BenefitPlan");

                    Application.BenefitPlanGet (benefitPlanId, true, BenefitPlanGetCompleted);

                }

                return benefitPlan;

            }

        }

        public Server.Application.CoverageType CoverageType {

            get {

                if (coverageType == null) {

                    GlobalProgressBarShow ("CoverageType");

                    Application.CoverageTypeGet (coverageTypeId, true, CoverageTypeGetCompleted);

                }

                return coverageType;

            }

        }

        public Server.Application.CoverageLevel CoverageLevel {

            get {

                if (coverageLevel == null) {

                    GlobalProgressBarShow ("CoverageLevel");

                    Application.CoverageLevelGet (coverageLevelId, true, CoverageLevelGetCompleted);

                }

                return coverageLevel;

            }

        }

        #endregion


        #region Property Data Binding Callbacks

        private void BenefitPlanGetCompleted (Object sender, Server.Application.BenefitPlanGetCompletedEventArgs e) {

            GlobalProgressBarHide ("BenefitPlan");

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                benefitPlan = e.Result;

                NotifyPropertyChanged ("Name");

                NotifyPropertyChanged ("BenefitPlan");

            }

            return;

        }

        private void CoverageTypeGetCompleted (Object sender, Server.Application.CoverageTypeGetCompletedEventArgs e) {

            GlobalProgressBarHide ("CoverageType");

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                coverageType = e.Result;

                NotifyPropertyChanged ("Name");

                NotifyPropertyChanged ("CoverageType");

            }

            return;

        }

        private void CoverageLevelGetCompleted (Object sender, Server.Application.CoverageLevelGetCompletedEventArgs e) {

            GlobalProgressBarHide ("CoverageLevel");

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                coverageLevel = e.Result;

                NotifyPropertyChanged ("Name");

                NotifyPropertyChanged ("CoverageLevel");

            }

            return;

        }

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
