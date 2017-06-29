using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.DataExplorer.Evaluations {

    [Serializable]
    public class DataExplorerNodeEvaluationPopulationMembership : DataExplorerNodeEvaluation, IDataExplorerNodeEvaluationDate {

        #region Private Properties

        private Server.Application.DataExplorerNodeEvaluationPopulationEvaluationType populationEvaluationType = Server.Application.DataExplorerNodeEvaluationPopulationEvaluationType.Population;

        private Int64 populationId = 0;

        private Int64 populationTypeId = 0;

        private Evaluations.DataExplorerNodeEvaluationDate dateCriteria = null;

        #endregion


        #region Public Properties

        public override Server.Application.DataExplorerNodeResultDataType ResultDataType { get { return Server.Application.DataExplorerNodeResultDataType.Member; } }

        public override Server.Application.DataExplorerNodeResultDataType ResultDetailDataType { get { return Server.Application.DataExplorerNodeResultDataType.PopulationMembership; } }

        public Server.Application.DataExplorerNodeEvaluationPopulationEvaluationType PopulationEvaluationType { get { return populationEvaluationType; } set { populationEvaluationType = value; } }

        public Int64 PopulationId { get { return populationId; } set { populationId = value; } }

        public Int64 PopulationTypeId { get { return populationTypeId; } set { populationTypeId = value; } }

        public Evaluations.DataExplorerNodeEvaluationDate DateCriteria { get { return dateCriteria; } set { dateCriteria = value; } }
        
        #endregion 
        
        
        #region Constructors

        protected void BaseConstructorPopulation () {

            NodeType = Server.Application.DataExplorerNodeType.Evaluation;

            EvaluationType = Server.Application.DataExplorerEvaluationType.PopulationMembership;

            Evaluation = Server.Application.DataFilterOperator.IsContainedIn;

            DateCriteria = new DataExplorerNodeEvaluationDate (this);

            return;

        }

        public DataExplorerNodeEvaluationPopulationMembership (Application applicationReference) {

            BaseConstructor (applicationReference);

            BaseConstructorPopulation ();

            return;

        }

        public DataExplorerNodeEvaluationPopulationMembership (Application applicationReference, Mercury.Server.Application.DataExplorerNodeEvaluationPopulationMembership serverObject) {

            BaseConstructor (applicationReference, serverObject);

            BaseConstructorPopulation ();

            MapFromServerObject (serverObject);
           
            return;

        }

        #endregion


        #region Public Methods

        public void MapFromServerObject (Server.Application.DataExplorerNodeEvaluationPopulationMembership serverObject) {

            base.MapFromServerObject ((Server.Application.DataExplorerNodeEvaluation)serverObject);


            PopulationEvaluationType = serverObject.PopulationEvaluationType;

            PopulationId = serverObject.PopulationId;

            PopulationTypeId = serverObject.PopulationTypeId;

            DateCriteria = new DataExplorerNodeEvaluationDate (this, serverObject.DateCriteria);


            return;

        }

        public virtual void MapToServerObject (Server.Application.DataExplorerNodeEvaluationPopulationMembership serverObject) {

            base.MapToServerObject ((Server.Application.DataExplorerNodeEvaluation)serverObject);


            serverObject.PopulationEvaluationType = PopulationEvaluationType;

            serverObject.PopulationId = PopulationId;

            serverObject.PopulationTypeId = PopulationTypeId;

            serverObject.DateCriteria = (Server.Application.DataExplorerNodeEvaluationDate) DateCriteria.ToServerObject ();

            
            return;

        }

        public override Object ToServerObject () {

            Server.Application.DataExplorerNodeEvaluationPopulationMembership serverObject = new Server.Application.DataExplorerNodeEvaluationPopulationMembership ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public new DataExplorerNodeEvaluationPopulationMembership Copy () {

            Server.Application.DataExplorerNodeEvaluationPopulationMembership serverObject = (Server.Application.DataExplorerNodeEvaluationPopulationMembership)ToServerObject ();

            DataExplorerNodeEvaluationPopulationMembership copiedObject = new DataExplorerNodeEvaluationPopulationMembership (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (DataExplorerNodeEvaluationPopulationMembership compareObject) {

            Boolean isEqual = base.IsEqual ((DataExplorerNodeEvaluation)compareObject);


            isEqual &= (PopulationEvaluationType == compareObject.PopulationEvaluationType);

            isEqual &= (PopulationId == compareObject.PopulationId);

            isEqual &= (PopulationTypeId == compareObject.PopulationTypeId);

            isEqual &= (DateCriteria.IsEqual (compareObject.DateCriteria));


            return isEqual;

        }

        #endregion 

    }

}
