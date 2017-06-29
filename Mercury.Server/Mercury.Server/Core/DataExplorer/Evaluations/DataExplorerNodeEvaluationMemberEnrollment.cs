using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.DataExplorer.Evaluations {

    [DataContract (Name = "DataExplorerNodeEvaluationMemberEnrollment")]
    public class DataExplorerNodeEvaluationMemberEnrollment : DataExplorerNodeEvaluation {

        #region Private Properties

        [DataMember (Name = "InsurerId")]
        private Int64 insurerId = 0;

        [DataMember (Name = "ProgramId")]
        private Int64 programId = 0;

        [DataMember (Name = "BenefitPlanId")]
        private Int64 benefitPlanId = 0;


        [DataMember (Name = "ContinuousEnrollment")]
        private Boolean continuousEnrollment = false;

        [DataMember (Name = "ContinuousAllowedGaps")]
        private Int32 continuousAllowedGaps = 1;

        [DataMember (Name = "ContinuousAllowedGapDays")]
        private Int32 continuousAllowedGapDays = 45;


        [DataMember (Name = "DateCriteria")]
        private DataExplorerNodeEvaluationDate dateCriteria = null;

        #endregion


        #region Public Properties

        public override Core.DataExplorer.Enumerations.DataExplorerNodeResultDataType ResultDataType { get { return Core.DataExplorer.Enumerations.DataExplorerNodeResultDataType.Member; } }

        public override Core.DataExplorer.Enumerations.DataExplorerNodeResultDataType ResultDetailDataType { get { return Core.DataExplorer.Enumerations.DataExplorerNodeResultDataType.NotSpecified; } }

        public Int64 InsurerId { get { return insurerId; } set { insurerId = value; } }

        public Int64 ProgramId { get { return programId; } set { programId = value; } }

        public Int64 BenefitPlanId { get { return benefitPlanId; } set { benefitPlanId = value; } }


        public Boolean ContinuousEnrollment { get { return continuousEnrollment; } set { continuousEnrollment = value; } }

        public Int32 ContinuousAllowedGaps { get { return continuousAllowedGaps; } set { continuousAllowedGaps = value; } }

        public Int32 ContinuousAllowedGapDays { get { return continuousAllowedGapDays; } set { continuousAllowedGapDays = value; } }


        public DataExplorerNodeEvaluationDate DateCriteria { get { return dateCriteria; } set { dateCriteria = value; } }

        #endregion


        #region Constructors

        protected void BaseConstructorPopulation () {

            NodeType = Core.DataExplorer.Enumerations.DataExplorerNodeType.Evaluation;

            EvaluationType = Core.DataExplorer.Enumerations.DataExplorerEvaluationType.MemberEnrollment;

            dateCriteria = new DataExplorerNodeEvaluationDate (this);

            return;

        }

        protected DataExplorerNodeEvaluationMemberEnrollment () {

            /* DO NOTHING, FOR INHERITANCE */

            BaseConstructorPopulation ();

            return;

        }

        public DataExplorerNodeEvaluationMemberEnrollment (Application applicationReference) {

            BaseConstructor (applicationReference);

            BaseConstructorPopulation ();

            return;

        }

        public DataExplorerNodeEvaluationMemberEnrollment (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference, forId);

            BaseConstructorPopulation ();

            return;

        }

        #endregion


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow, "DataExplorerNode");


            if (ExtendedProperties.ContainsKey ("InsurerId")) { InsurerId = Convert.ToInt64 (ExtendedProperties["InsurerId"]); }

            if (ExtendedProperties.ContainsKey ("ProgramId")) { ProgramId = Convert.ToInt64 (ExtendedProperties["ProgramId"]); }

            if (ExtendedProperties.ContainsKey ("BenefitPlanId")) { BenefitPlanId = Convert.ToInt64 (ExtendedProperties["BenefitPlanId"]); }


            if (ExtendedProperties.ContainsKey ("ContinuousEnrollment")) { ContinuousEnrollment = Convert.ToBoolean (ExtendedProperties["ContinuousEnrollment"]); }

            if (ExtendedProperties.ContainsKey ("ContinuousAllowedGaps")) { ContinuousAllowedGaps = Convert.ToInt32 (ExtendedProperties["ContinuousAllowedGaps"]); }

            if (ExtendedProperties.ContainsKey ("ContinuousAllowedGapDays")) { ContinuousAllowedGapDays = Convert.ToInt32 (ExtendedProperties["ContinuousAllowedGapDays"]); }


            DateCriteria.MapFromExtendedProperties (ExtendedProperties);

            return;

        }

        #endregion 


        #region Public Methods

        public override void RecreateExtendedProperties () {

            base.RecreateExtendedProperties ();


            ExtendedProperties.Add ("InsurerId", InsurerId.ToString ());

            ExtendedProperties.Add ("ProgramId", ProgramId.ToString ());

            ExtendedProperties.Add ("BenefitPlanId", BenefitPlanId.ToString ());


            ExtendedProperties.Add ("ContinuousEnrollment", ContinuousEnrollment.ToString ());

            ExtendedProperties.Add ("ContinuousAllowedGaps", ContinuousAllowedGaps.ToString ());

            ExtendedProperties.Add ("ContinuousAllowedGapDays", ContinuousAllowedGapDays.ToString ());


            foreach (String currentKey in DateCriteria.MapToExtendedProperties ().Keys) { ExtendedProperties.Add (currentKey, DateCriteria.MapToExtendedProperties ()[currentKey]); }


            return;

        }

        public override Int32 Execute () {

            Int32 rowCount = 0;



            System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("dal.DataExplorerNodeEvaluation_MemberEnrollment_Execute");

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@nodeInstanceId", NodeInstanceId);

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@insurerId", InsurerId);

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@programId", ProgramId);

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@benefitPlanId", BenefitPlanId);

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@continuousEnrollment", ContinuousEnrollment);

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@continuousAllowedGaps", ContinuousAllowedGaps);

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@continuousAllowedGapDays", ContinuousAllowedGapDays);

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@startDate", dateCriteria.CalculateStartDate ());

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@endDate", dateCriteria.CalculateEndDate ());


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
