using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.DataExplorer.Evaluations {

    [DataContract (Name = "DataExplorerNodeEvaluationMemberDemographic")]
    public class DataExplorerNodeEvaluationMemberDemographic : DataExplorerNodeEvaluation {

        #region Private Properties

        [DataMember (Name = "Gender")]
        private Core.Enumerations.Gender gender = Mercury.Server.Core.Enumerations.Gender.Both;

        [DataMember (Name = "EthnicityId")]
        private Int64 ethnicityId;

        [DataMember (Name = "AgeCriteria")]
        private DataExplorerNodeEvaluationAge ageCriteria = null;

        [DataMember (Name = "DateCriteria")]
        private DataExplorerNodeEvaluationDate dateCriteria = null;

        #endregion


        #region Public Properties

        public override Core.DataExplorer.Enumerations.DataExplorerNodeResultDataType ResultDataType { get { return Core.DataExplorer.Enumerations.DataExplorerNodeResultDataType.Member; } }

        public override Core.DataExplorer.Enumerations.DataExplorerNodeResultDataType ResultDetailDataType { get { return Core.DataExplorer.Enumerations.DataExplorerNodeResultDataType.NotSpecified; } }

        public Core.Enumerations.Gender Gender { get { return gender; } set { gender = value; } }

        public Int64 EthnicityId { get { return ethnicityId; } set { ethnicityId = value; } }

        public DataExplorerNodeEvaluationAge AgeCriteria { get { return ageCriteria; } set { ageCriteria = value; } }

        public DataExplorerNodeEvaluationDate DateCriteria { get { return dateCriteria; } set { dateCriteria = value; } }
        
        #endregion
        

        #region Constructors

        protected void BaseConstructorEvaluation () {

            NodeType = Core.DataExplorer.Enumerations.DataExplorerNodeType.Evaluation;

            EvaluationType = Core.DataExplorer.Enumerations.DataExplorerEvaluationType.MemberDemographic;

            ageCriteria = new DataExplorerNodeEvaluationAge (this);

            dateCriteria = new DataExplorerNodeEvaluationDate (this);

            return;

        }

        protected DataExplorerNodeEvaluationMemberDemographic () { 
            
            /* DO NOTHING, FOR INHERITANCE */

            BaseConstructorEvaluation ();

            return;

        }
        
        public DataExplorerNodeEvaluationMemberDemographic (Application applicationReference) {

            BaseConstructor (applicationReference);

            BaseConstructorEvaluation ();
            
            return; 
        
        }

        public DataExplorerNodeEvaluationMemberDemographic (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference, forId);

            BaseConstructorEvaluation ();

            return;

        }

        #endregion 
        

        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow, "DataExplorerNode");


            if (ExtendedProperties.ContainsKey ("GenderInt32")) { Gender = (Core.Enumerations.Gender)Convert.ToInt32 (ExtendedProperties["GenderInt32"]); }

            if (ExtendedProperties.ContainsKey ("EthnicityId")) { EthnicityId = Convert.ToInt64 (ExtendedProperties["EthnicityId"]); }


            AgeCriteria.MapFromExtendedProperties (ExtendedProperties);

            DateCriteria.MapFromExtendedProperties (ExtendedProperties);

            return;

        }

        #endregion 


        #region Public Methods

        public override void RecreateExtendedProperties () {

            base.RecreateExtendedProperties ();


            ExtendedProperties.Add ("GenderInt32", ((Int32)Gender).ToString ());

            ExtendedProperties.Add ("EthnicityId", EthnicityId.ToString ());


            foreach (String currentKey in AgeCriteria.MapToExtendedProperties ().Keys) { ExtendedProperties.Add (currentKey, AgeCriteria.MapToExtendedProperties ()[currentKey]); }

            foreach (String currentKey in DateCriteria.MapToExtendedProperties ().Keys) { ExtendedProperties.Add (currentKey, DateCriteria.MapToExtendedProperties ()[currentKey]); }


            return;
        
        }

        public override Int32 Execute () {

            Int32 rowCount = 0;



            System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("dal.DataExplorerNodeEvaluation_MemberDemographic_Execute");

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@nodeInstanceId", NodeInstanceId);


            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@gender", ((Int32)Gender));


            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@useAgeCriteria", ageCriteria.UseAgeCriteria);

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@ageMinimum", ageCriteria.AgeMinimum);

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@ageMaximum", ageCriteria.AgeMaximum);

            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@ageQualifier", ((Int32)ageCriteria.AgeQualifier));


            application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@ethnicityId", EthnicityId);


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
