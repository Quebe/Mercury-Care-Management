using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


namespace Mercury.Server.Core.DataExplorer.Evaluations {

    [DataContract (Name = "DataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate")]
    public class DataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate : DataExplorerNodeEvaluation {

        #region Private Properties

        [DataMember (Name = "InsurerId")]
        private Int64 insurerId = 0;

        [DataMember (Name = "ProgramId")]
        private Int64 programId = 0;

        [DataMember (Name = "BenefitPlanId")]
        private Int64 benefitPlanId = 0;


        [DataMember (Name = "ContinuousAllowedGaps")]
        private Int32 continuousAllowedGaps = 1;

        [DataMember (Name = "ContinuousAllowedGapDays")]
        private Int32 continuousAllowedGapDays = 45;

        [DataMember (Name = "ContinuousUntilAge")]
        private Int32 continuousUntilAge = 0;

        #endregion


        #region Public Properties

        public override Core.DataExplorer.Enumerations.DataExplorerNodeResultDataType ResultDataType { get { return Core.DataExplorer.Enumerations.DataExplorerNodeResultDataType.Member; } }

        public override Core.DataExplorer.Enumerations.DataExplorerNodeResultDataType ResultDetailDataType { get { return Core.DataExplorer.Enumerations.DataExplorerNodeResultDataType.NotSpecified; } }

        public Int64 InsurerId { get { return insurerId; } set { insurerId = value; } }

        public Int64 ProgramId { get { return programId; } set { programId = value; } }

        public Int64 BenefitPlanId { get { return benefitPlanId; } set { benefitPlanId = value; } }


        public Int32 ContinuousAllowedGaps { get { return continuousAllowedGaps; } set { continuousAllowedGaps = value; } }

        public Int32 ContinuousAllowedGapDays { get { return continuousAllowedGapDays; } set { continuousAllowedGapDays = value; } }

        public Int32 ContinuousUntilAge { get { return continuousUntilAge; } set { continuousUntilAge = value; } }

        #endregion


        #region Constructors

        protected void BaseConstructorNode () {

            NodeType = Core.DataExplorer.Enumerations.DataExplorerNodeType.Evaluation;

            EvaluationType = Core.DataExplorer.Enumerations.DataExplorerEvaluationType.MemberEnrollmentContinuousFromBirthDate;

            return;

        }

        protected DataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate () {

            /* DO NOTHING, FOR INHERITANCE */

            BaseConstructorNode ();

            return;

        }

        public DataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate (Application applicationReference) {

            BaseConstructor (applicationReference);

            BaseConstructorNode ();

            return;

        }

        public DataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference, forId);

            BaseConstructorNode ();

            return;

        }

        #endregion


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow, "DataExplorerNode");


            if (ExtendedProperties.ContainsKey ("InsurerId")) { InsurerId = Convert.ToInt64 (ExtendedProperties["InsurerId"]); }

            if (ExtendedProperties.ContainsKey ("ProgramId")) { ProgramId = Convert.ToInt64 (ExtendedProperties["ProgramId"]); }

            if (ExtendedProperties.ContainsKey ("BenefitPlanId")) { BenefitPlanId = Convert.ToInt64 (ExtendedProperties["BenefitPlanId"]); }


            if (ExtendedProperties.ContainsKey ("ContinuousAllowedGaps")) { ContinuousAllowedGaps = Convert.ToInt32 (ExtendedProperties["ContinuousAllowedGaps"]); }

            if (ExtendedProperties.ContainsKey ("ContinuousAllowedGapDays")) { ContinuousAllowedGapDays = Convert.ToInt32 (ExtendedProperties["ContinuousAllowedGapDays"]); }

            if (ExtendedProperties.ContainsKey ("ContinuousUntilAge")) { ContinuousUntilAge = Convert.ToInt32 (ExtendedProperties["ContinuousUntilAge"]); }


            return;

        }

        #endregion 


        #region Public Methods

        public override void RecreateExtendedProperties () {

            base.RecreateExtendedProperties ();


            ExtendedProperties.Add ("InsurerId", InsurerId.ToString ());

            ExtendedProperties.Add ("ProgramId", ProgramId.ToString ());

            ExtendedProperties.Add ("BenefitPlanId", BenefitPlanId.ToString ());


            ExtendedProperties.Add ("ContinuousAllowedGaps", ContinuousAllowedGaps.ToString ());

            ExtendedProperties.Add ("ContinuousAllowedGapDays", ContinuousAllowedGapDays.ToString ());

            ExtendedProperties.Add ("ContinuousUntilAge", ContinuousUntilAge.ToString ());

            return;

        }

        public override Int32 Execute () {

            Int32 rowCount = 0;



            System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("dal.DataExplorerNodeEvaluation_MemberEnrollmentContinuousFromBirthDate_Execute");

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@nodeInstanceId", NodeInstanceId);

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@insurerId", InsurerId);

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@programId", ProgramId);

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@benefitPlanId", BenefitPlanId);

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@continuousAllowedGaps", ContinuousAllowedGaps);

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@continuousAllowedGapDays", ContinuousAllowedGapDays);

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@continuousUntilAge", ContinuousUntilAge);


            // RETURNED ROWS AFFECTED  

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@rowCount", ((Int32)0));

            ((System.Data.IDbDataParameter)sqlCommand.Parameters["@rowCount"]).Direction = System.Data.ParameterDirection.Output;


            sqlCommand.CommandTimeout = 0;

            sqlCommand.ExecuteNonQuery ();

            rowCount = Convert.ToInt32 (((System.Data.IDbDataParameter)sqlCommand.Parameters["@rowCount"]).Value);


            return rowCount;

        }

        #endregion

    }

}
