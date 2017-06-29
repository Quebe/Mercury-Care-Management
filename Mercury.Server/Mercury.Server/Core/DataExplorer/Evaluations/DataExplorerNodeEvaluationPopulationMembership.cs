using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.DataExplorer.Evaluations {

    [DataContract (Name="DataExplorerNodeEvaluationPopulationMembership")]
    public class DataExplorerNodeEvaluationPopulationMembership : DataExplorerNodeEvaluation {

        #region Private Properties

        [DataMember (Name = "PopulationEvaluationType")]
        private Enumerations.DataExplorerNodeEvaluationPopulationEvaluationType populationEvaluationType = Enumerations.DataExplorerNodeEvaluationPopulationEvaluationType.Population;

        [DataMember (Name = "PopulationId")]
        private Int64 populationId = 0;

        [DataMember (Name = "PopulationTypeId")]
        private Int64 populationTypeId = 0;

        [DataMember (Name = "DateCriteria")]
        private DataExplorerNodeEvaluationDate dateCriteria = null;

        #endregion 


        #region Public Properties

        public override Core.DataExplorer.Enumerations.DataExplorerNodeResultDataType ResultDataType { get { return Core.DataExplorer.Enumerations.DataExplorerNodeResultDataType.Member; } }

        public override Core.DataExplorer.Enumerations.DataExplorerNodeResultDataType ResultDetailDataType { get { return Core.DataExplorer.Enumerations.DataExplorerNodeResultDataType.PopulationMembership; } }

        public Enumerations.DataExplorerNodeEvaluationPopulationEvaluationType PopulationEvaluationType { get { return populationEvaluationType; } set { populationEvaluationType = value; } }

        public Int64 PopulationId { get { return populationId; } set { populationId = value; } }

        public Int64 PopulationTypeId { get { return populationTypeId; } set { populationTypeId = value; } }

        public DataExplorerNodeEvaluationDate DateCriteria { get { return dateCriteria; } set { dateCriteria = value; } }
        
        #endregion 
        

        #region Constructors

        protected void BaseConstructorPopulation () {

            NodeType = Core.DataExplorer.Enumerations.DataExplorerNodeType.Evaluation;

            EvaluationType = Core.DataExplorer.Enumerations.DataExplorerEvaluationType.PopulationMembership;

            dateCriteria = new DataExplorerNodeEvaluationDate (this);

            return;

        }

        protected DataExplorerNodeEvaluationPopulationMembership () { 
            
            /* DO NOTHING, FOR INHERITANCE */

            BaseConstructorPopulation ();

            return;

        }
        
        public DataExplorerNodeEvaluationPopulationMembership (Application applicationReference) {

            BaseConstructor (applicationReference);

            BaseConstructorPopulation ();
            
            return; 
        
        }

        public DataExplorerNodeEvaluationPopulationMembership (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference, forId);

            BaseConstructorPopulation ();

            return;

        }

        #endregion 
        

        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow, "DataExplorerNode");


            if (ExtendedProperties.ContainsKey ("PopulationId")) { PopulationId = Convert.ToInt64 (ExtendedProperties["PopulationId"]); }

            if (ExtendedProperties.ContainsKey ("PopulationTypeId")) { PopulationTypeId = Convert.ToInt64 (ExtendedProperties["PopulationTypeId"]); }

            if (ExtendedProperties.ContainsKey ("PopulationEvaluationTypeInt32")) { PopulationEvaluationType = (Enumerations.DataExplorerNodeEvaluationPopulationEvaluationType) Convert.ToInt32 (ExtendedProperties["PopulationEvaluationTypeInt32"]); }


            DateCriteria.MapFromExtendedProperties (ExtendedProperties);

            return;

        }

        #endregion 


        #region Public Methods

        public override void RecreateExtendedProperties () {

            base.RecreateExtendedProperties ();


            ExtendedProperties.Add ("PopulationEvaluationTypeInt32", ((Int32)PopulationEvaluationType).ToString ());

            ExtendedProperties.Add ("PopulationId", PopulationId.ToString ());

            ExtendedProperties.Add ("PopulationTypeId", PopulationTypeId.ToString ());


            foreach (String currentKey in DateCriteria.MapToExtendedProperties ().Keys) { ExtendedProperties.Add (currentKey, DateCriteria.MapToExtendedProperties ()[currentKey]); }


            return;

        }

        public override Int32 Execute () {

            Int32 rowCount = 0;



            System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("dal.DataExplorerNodeEvaluation_PopulationMembership_Execute");

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@nodeInstanceId", NodeInstanceId);


            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@populationId", ((populationEvaluationType == Enumerations.DataExplorerNodeEvaluationPopulationEvaluationType.Population) ? populationId : 0));

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@populationTypeId", ((populationEvaluationType == Enumerations.DataExplorerNodeEvaluationPopulationEvaluationType.PopulationType) ? populationTypeId : 0));

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@startDate", dateCriteria.CalculateStartDate ());

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@endDate", dateCriteria.CalculateEndDate ());


            // RETURNED ROWS AFFECTED  

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@rowCount", ((Int32) 0));

            ((System.Data.IDbDataParameter)sqlCommand.Parameters["@rowCount"]).Direction = System.Data.ParameterDirection.Output;


            sqlCommand.CommandTimeout = 0;

            sqlCommand.ExecuteNonQuery ();
            
            rowCount = Convert.ToInt32 (((System.Data.IDbDataParameter) sqlCommand.Parameters["@rowCount"]).Value);
            
 
            return rowCount;

        }

        #endregion 

    }

}
