using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.DataExplorer.Evaluations {

    [DataContract (Name = "DataExplorerNodeEvaluationMemberMetric")]
    public class DataExplorerNodeEvaluationMemberMetric : DataExplorerNodeEvaluation {

        #region Private Properties

        [DataMember (Name = "MetricId")]
        private Int64 metricId = 0;

        [DataMember (Name = "ValueMinimum")]
        private Decimal valueMinimum = 0;

        [DataMember (Name = "ValueMaximum")]
        private Decimal valueMaximum = 0;

        [DataMember (Name = "CountOf")]
        private Int32 countOf = 1;

        [DataMember (Name = "AgeCriteria")]
        private DataExplorerNodeEvaluationAge ageCriteria = null;

        [DataMember (Name = "DateCriteria")]
        private DataExplorerNodeEvaluationDate dateCriteria = null;

        #endregion


        #region Public Properties

        public override Core.DataExplorer.Enumerations.DataExplorerNodeResultDataType ResultDataType { get { return Core.DataExplorer.Enumerations.DataExplorerNodeResultDataType.Member; } }

        public override Core.DataExplorer.Enumerations.DataExplorerNodeResultDataType ResultDetailDataType { get { return Core.DataExplorer.Enumerations.DataExplorerNodeResultDataType.MemberMetric; } }

        public Int64 MetricId { get { return metricId; } set { metricId = value; } }

        public Decimal ValueMinimum { get { return valueMinimum; } set { valueMinimum = value; } }

        public Decimal ValueMaximum { get { return valueMaximum; } set { valueMaximum = value; } }

        public Int32 CountOf { get { return countOf; } set { countOf = value; } }

        public DataExplorerNodeEvaluationAge AgeCriteria { get { return ageCriteria; } set { ageCriteria = value; } }

        public DataExplorerNodeEvaluationDate DateCriteria { get { return dateCriteria; } set { dateCriteria = value; } }

        #endregion


        #region Constructors

        protected void BaseConstructorLocal () {

            NodeType = Core.DataExplorer.Enumerations.DataExplorerNodeType.Evaluation;

            EvaluationType = Core.DataExplorer.Enumerations.DataExplorerEvaluationType.MemberMetric;

            ageCriteria = new DataExplorerNodeEvaluationAge (this);

            dateCriteria = new DataExplorerNodeEvaluationDate (this);

            return;

        }

        protected DataExplorerNodeEvaluationMemberMetric () {

            /* DO NOTHING, FOR INHERITANCE */

            BaseConstructorLocal ();

            return;

        }

        public DataExplorerNodeEvaluationMemberMetric (Application applicationReference) {

            BaseConstructor (applicationReference);

            BaseConstructorLocal ();

            return;

        }

        public DataExplorerNodeEvaluationMemberMetric (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference, forId);

            BaseConstructorLocal ();

            return;

        }

        #endregion


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow, "DataExplorerNode");


            if (ExtendedProperties.ContainsKey ("MetricId")) { MetricId = Convert.ToInt64 (ExtendedProperties["MetricId"]); }

            if (ExtendedProperties.ContainsKey ("ValueMinimum")) { ValueMinimum = Convert.ToDecimal (ExtendedProperties["ValueMinimum"]); }

            if (ExtendedProperties.ContainsKey ("ValueMaximum")) { ValueMaximum = Convert.ToDecimal (ExtendedProperties["ValueMaximum"]); }

            if (ExtendedProperties.ContainsKey ("CountOf")) { CountOf = Convert.ToInt32 (ExtendedProperties["CountOf"]); }


            AgeCriteria.MapFromExtendedProperties (ExtendedProperties);

            DateCriteria.MapFromExtendedProperties (ExtendedProperties);

            return;

        }

        #endregion 


        #region Public Methods

        public override void RecreateExtendedProperties () {

            base.RecreateExtendedProperties ();


            ExtendedProperties.Add ("MetricId", MetricId.ToString ());

            ExtendedProperties.Add ("ValueMinimum", ValueMinimum.ToString ());

            ExtendedProperties.Add ("ValueMaximum", ValueMaximum.ToString ());

            ExtendedProperties.Add ("CountOf", CountOf.ToString ());


            foreach (String currentKey in AgeCriteria.MapToExtendedProperties ().Keys) { ExtendedProperties.Add (currentKey, AgeCriteria.MapToExtendedProperties ()[currentKey]); }

            foreach (String currentKey in DateCriteria.MapToExtendedProperties ().Keys) { ExtendedProperties.Add (currentKey, DateCriteria.MapToExtendedProperties ()[currentKey]); }


            return;

        }

        public override Int32 Execute () {

            Int32 rowCount = 0;



            System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("dal.DataExplorerNodeEvaluation_MemberMetric_Execute");

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@nodeInstanceId", NodeInstanceId);

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@metricId", MetricId);

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@valueMinimum", ValueMinimum);

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@valueMaximum", ValueMaximum);

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@countOf", CountOf);


            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@useAgeCriteria", ageCriteria.UseAgeCriteria);

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@ageMinimum", ageCriteria.AgeMinimum);

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@ageMaximum", ageCriteria.AgeMaximum);

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@ageQualifier", ((Int32)ageCriteria.AgeQualifier));


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
