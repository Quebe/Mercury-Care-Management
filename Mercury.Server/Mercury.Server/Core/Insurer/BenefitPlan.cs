using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Insurer {

    [DataContract (Name = "BenefitPlan")]
    public class BenefitPlan : CoreConfigurationObject {

        #region Private Properties

        [DataMember (Name = "ProgramId")]
        private Int64 programId;

        [DataMember (Name = "EffectiveDate")]
        private DateTime effectiveDate;

        [DataMember (Name = "TerminationDate")]
        private DateTime terminationDate;

        #endregion


        #region Public Properties

        public Int64 ProgramId { get { return programId; } }

        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }

        #endregion


        #region Constructors

        public BenefitPlan (Application applicationReference) { BaseConstructor (applicationReference); return; }

        public BenefitPlan (Application applicationReference, Int64 forBenefitPlanId) {

            BaseConstructor (applicationReference);

            if (!Load (forBenefitPlanId)) {

                throw new ApplicationException ("Unable to load Benefit Plan from the database for " + forBenefitPlanId.ToString () + ".");

            }

        }

        #endregion


        #region Database Functions

        override public Boolean Load (Int64 forId) {

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable tableBenefitPlan;

            if (base.application.EnvironmentDatabase == null) { return false; }

            selectStatement.Append ("EXEC dal.BenefitPlan_Select " + forId.ToString ());

            tableBenefitPlan = base.application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            if (tableBenefitPlan.Rows.Count == 1) {

                MapDataFields (tableBenefitPlan.Rows[0]);

                return true;

            }

            else {

                return false;

            }

        }

        override public void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);

            programId = (Int64) currentRow["ProgramId"];

            effectiveDate = (DateTime) currentRow["EffectiveDate"];

            terminationDate = (DateTime) currentRow["TerminationDate"];

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        #endregion

    }

}
