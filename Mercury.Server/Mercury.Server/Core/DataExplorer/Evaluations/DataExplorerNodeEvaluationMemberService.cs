using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.DataExplorer.Evaluations {

    [DataContract (Name = "DataExplorerNodeEvaluationMemberService")]
    public class DataExplorerNodeEvaluationMemberService : DataExplorerNodeEvaluation {

        #region Private Properties

        [DataMember (Name = "ServiceId")]
        private Int64 serviceId = 0;

        [DataMember (Name = "CountOf")]
        private Int32 countOf = 1;

        [DataMember (Name = "AgeCriteria")]
        private DataExplorerNodeEvaluationAge ageCriteria = null;

        [DataMember (Name = "DateCriteria")]
        private DataExplorerNodeEvaluationDate dateCriteria = null;

        #endregion


        #region Public Properties

        public override Core.DataExplorer.Enumerations.DataExplorerNodeResultDataType ResultDataType { get { return Core.DataExplorer.Enumerations.DataExplorerNodeResultDataType.Member; } }

        public override Core.DataExplorer.Enumerations.DataExplorerNodeResultDataType ResultDetailDataType { get { return Core.DataExplorer.Enumerations.DataExplorerNodeResultDataType.MemberService; } }

        public Int64 ServiceId { get { return serviceId; } set { serviceId = value; } }

        public Int32 CountOf { get { return countOf; } set { countOf = value; } }

        public DataExplorerNodeEvaluationAge AgeCriteria { get { return ageCriteria; } set { ageCriteria = value; } }

        public DataExplorerNodeEvaluationDate DateCriteria { get { return dateCriteria; } set { dateCriteria = value; } }

        #endregion


        #region Constructors

        protected void BaseConstructorLocal () {

            NodeType = Core.DataExplorer.Enumerations.DataExplorerNodeType.Evaluation;

            EvaluationType = Core.DataExplorer.Enumerations.DataExplorerEvaluationType.MemberService;

            ageCriteria = new DataExplorerNodeEvaluationAge (this);

            dateCriteria = new DataExplorerNodeEvaluationDate (this);

            return;

        }

        protected DataExplorerNodeEvaluationMemberService () {

            /* DO NOTHING, FOR INHERITANCE */

            BaseConstructorLocal ();

            return;

        }

        public DataExplorerNodeEvaluationMemberService (Application applicationReference) {

            BaseConstructor (applicationReference);

            BaseConstructorLocal ();

            return;

        }

        public DataExplorerNodeEvaluationMemberService (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference, forId);

            BaseConstructorLocal ();

            return;

        }

        #endregion


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow, "DataExplorerNode");


            if (ExtendedProperties.ContainsKey ("ServiceId")) { ServiceId = Convert.ToInt64 (ExtendedProperties["ServiceId"]); }

            if (ExtendedProperties.ContainsKey ("CountOf")) { CountOf = Convert.ToInt32 (ExtendedProperties["CountOf"]); }


            AgeCriteria.MapFromExtendedProperties (ExtendedProperties);

            DateCriteria.MapFromExtendedProperties (ExtendedProperties);

            return;

        }

        #endregion 


        #region Public Methods

        public override void RecreateExtendedProperties () {

            base.RecreateExtendedProperties ();


            ExtendedProperties.Add ("ServiceId", ServiceId.ToString ());

            ExtendedProperties.Add ("CountOf", CountOf.ToString ());


            foreach (String currentKey in AgeCriteria.MapToExtendedProperties ().Keys) { ExtendedProperties.Add (currentKey, AgeCriteria.MapToExtendedProperties ()[currentKey]); }

            foreach (String currentKey in DateCriteria.MapToExtendedProperties ().Keys) { ExtendedProperties.Add (currentKey, DateCriteria.MapToExtendedProperties ()[currentKey]); }


            return;

        }

        public override Int32 Execute () {

            Int32 rowCount = 0;



            System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("dal.DataExplorerNodeEvaluation_MemberService_Execute");

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@nodeInstanceId", NodeInstanceId);

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@serviceId", ServiceId);

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
