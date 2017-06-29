using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Member {

    [Serializable]
    [DataContract (Name = "MemberEnrollmentCoverage")]
    public class MemberEnrollmentCoverage : CoreObject {

        #region Private Properties

        [DataMember (Name = "MemberEnrollmentId")]
        private Int64 memberEnrollmentId;

        [DataMember (Name = "BenefitPlanId")]
        private Int64 benefitPlanId;

        [DataMember (Name = "CoverageTypeId")]
        private Int64 coverageTypeId;

        [DataMember (Name = "CoverageLevelId")]
        private Int64 coverageLevelId;

        [DataMember (Name = "RateCode")]
        private String rateCode;

        [DataMember (Name = "EffectiveDate")]
        private DateTime effectiveDate;

        [DataMember (Name = "TerminationDate")]
        private DateTime terminationDate;

        [NonSerialized]
        private Insurer.BenefitPlan benefitPlan = null;

        #endregion


        #region Public Properties

        public Int64 MemberEnrollmentId { get { return memberEnrollmentId; } set { memberEnrollmentId = value; } }

        public Int64 BenefitPlanId { get { return benefitPlanId; } set { benefitPlanId = value; } }

        public Int64 CoverageTypeId { get { return coverageTypeId; } set { coverageTypeId = value; } }

        public Int64 CoverageLevelId { get { return coverageLevelId; } set { coverageLevelId = value; } }
        
        public String RateCode { get { return rateCode; } set { rateCode = value; } }

        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }


        public Insurer.BenefitPlan BenefitPlan {

            get {

                if (benefitPlan != null) { return benefitPlan; }

                if (base.application == null) { return new Insurer.BenefitPlan (base.application); }

                benefitPlan = base.application.BenefitPlanGet (benefitPlanId);

                return benefitPlan;

            }

        }

        public String BenefitPlanName { get { return ((BenefitPlan != null) ? BenefitPlan.Name : String.Empty); } }

        public Insurer.CoverageType CoverageType { get { return application.CoverageTypeGet (coverageTypeId); } }

        public String CoverageTypeName { get { return ((CoverageType != null) ? CoverageType.Name : String.Empty); } }

        public Insurer.CoverageLevel CoverageLevel { get { return application.CoverageLevelGet (coverageLevelId); } }

        public String CoverageLevelName { get { return ((CoverageLevel != null) ? CoverageLevel.Name : String.Empty); } }

        #endregion

        
        #region Constructors 

        public MemberEnrollmentCoverage (Application applicationReference) {

            BaseConstructor (applicationReference);
        
            return; 
        
        }

        public MemberEnrollmentCoverage (Application applicationReference, Int64 forEnrollmentCoverageId) {

            BaseConstructor (applicationReference);

            if (!Load (forEnrollmentCoverageId)) {

                throw new ApplicationException ("Unable to load Enrollment Coverage from the database for " + forEnrollmentCoverageId.ToString () + ".");

            }

        }

        #endregion


        #region Database Functions

        override public Boolean Load (Int64 forId) {

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable tableEnrollmentCoverage;

            if (base.application.EnvironmentDatabase == null) { return false; }

            selectStatement.Append ("EXEC dal.EnrollmentCoverage_Select " + forId.ToString ());

            tableEnrollmentCoverage = base.application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            if (tableEnrollmentCoverage.Rows.Count == 1) {

                MapDataFields (tableEnrollmentCoverage.Rows[0]);

                return true;

            }

            else {

                return false;

            }

        }

        override public void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            memberEnrollmentId = (Int64) currentRow["MemberEnrollmentId"];

            benefitPlanId = (Int64) currentRow["BenefitPlanId"];

            coverageTypeId = (Int64)currentRow["CoverageTypeId"];

            coverageLevelId = (Int64)currentRow["CoverageLevelId"];

            rateCode = (String)currentRow["RateCode"];

            effectiveDate = (DateTime) currentRow["EffectiveDate"];

            terminationDate = (DateTime) currentRow["TerminationDate"];

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        #endregion


        #region Public Methods - Data Bindings

        public override Dictionary<String, String> DataBindingContexts {

            get {

                Dictionary<String, String> bindingContexts = new Dictionary<String, String> ();

                bindingContexts.Add ("Id", "Id|MemberEnrollmentCoverageId");

                bindingContexts.Add ("MemberEnrollmentId", "Id|MemberEnrollment");

                bindingContexts.Add ("BenefitPlanId", "Id|BenefitPlan");

                bindingContexts.Add ("CoverageTypeId", "Id|CoverageType");
                
                bindingContexts.Add ("CoverageLevelId", "Id|CoverageLevel"); 
                
                bindingContexts.Add ("RateCode", "String");

                bindingContexts.Add ("EffectiveDate", "DateTime");

                bindingContexts.Add ("TerminationDate", "DateTime");

                return bindingContexts;

            }

        }

        override public String EvaluateDataBinding (String bindingContext) {

            String dataValue = String.Empty;

            String bindingContextPart = bindingContext.Split ('.')[0];

            switch (bindingContextPart) {

                case "Id": dataValue = Id.ToString (); break;

                case "EnrollmentId": // BACKWARDS COMPATIBILITY
                    
                case "MemberEnrollmentId": 

                    dataValue = memberEnrollmentId.ToString (); 
                    
                    break;

                case "BenefitPlanId": dataValue = benefitPlanId.ToString (); break;

                case "CoverageTypeId": dataValue = coverageTypeId.ToString (); break;

                case "CoverageLevelId": dataValue = coverageLevelId.ToString (); break;

                case "RateCode": dataValue = rateCode.ToString (); break;


                case "EffectiveDate": dataValue = effectiveDate.ToString ("MM/dd/yyyy"); break;

                case "TerminationDate": dataValue = terminationDate.ToString ("MM/dd/yyyy"); break;

                default: dataValue = "!Error"; break;

            }

            return dataValue;

        }

        #endregion

    }

}
