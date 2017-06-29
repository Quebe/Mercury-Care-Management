using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.DataExplorer {

    [KnownType (typeof (Evaluations.DataExplorerNodeEvaluationMemberDemographic))]
    [KnownType (typeof (Evaluations.DataExplorerNodeEvaluationMemberEnrollment))]
    [KnownType (typeof (Evaluations.DataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate))]
    [KnownType (typeof (Evaluations.DataExplorerNodeEvaluationMemberMetric))]
    [KnownType (typeof (Evaluations.DataExplorerNodeEvaluationMemberService))]
    [KnownType (typeof (Evaluations.DataExplorerNodeEvaluationPopulationMembership))]
    [DataContract (Name = "DataExplorerNodeEvaluation ")]
    public class DataExplorerNodeEvaluation : DataExplorerNode {
        
        #region Private Properties

        [DataMember (Name = "EvaluationType")]
        private Enumerations.DataExplorerEvaluationType evaluationType = Enumerations.DataExplorerEvaluationType.NotSpecified;

        [DataMember (Name = "Evaluation")]
        private Data.Enumerations.DataFilterOperator evaluation = Data.Enumerations.DataFilterOperator.IsEqualTo;
        
        #endregion 


        #region Public Properties

        public Enumerations.DataExplorerEvaluationType EvaluationType { get { return evaluationType; } set { evaluationType = value; } }

        public Data.Enumerations.DataFilterOperator Evaluation { get { return evaluation; } set { evaluation = value; } }

        #endregion 
        

        #region Constructors

        protected DataExplorerNodeEvaluation () { /* DO NOTHING, FOR INHERITANCE */ }
        
        public DataExplorerNodeEvaluation (Application applicationReference) {

            BaseConstructor (applicationReference);
            
            return; 
        
        }

        #endregion 


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            if (ExtendedProperties.ContainsKey ("EvaluationTypeInt32")) { EvaluationType = (Enumerations.DataExplorerEvaluationType)Convert.ToInt32 (ExtendedProperties["EvaluationTypeInt32"]); }

            if (ExtendedProperties.ContainsKey ("EvaluationInt32")) { Evaluation = (Data.Enumerations.DataFilterOperator)Convert.ToInt32 (ExtendedProperties["EvaluationInt32"]); }

            return;

        }

        #endregion 


        #region Public Methods

        public override void RecreateExtendedProperties () {

            base.RecreateExtendedProperties ();


            ExtendedProperties.Add ("EvaluationTypeInt32", ((Int32)EvaluationType).ToString ());

            ExtendedProperties.Add ("EvaluationInt32", ((Int32)Evaluation).ToString ());

            return;

        }

        #endregion 

    }

}
