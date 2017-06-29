using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.DataExplorer.Evaluations {

    [Serializable]
    public class DataExplorerNodeEvaluationMemberDemographic : DataExplorerNodeEvaluation, IDataExplorerNodeEvaluationAge, IDataExplorerNodeEvaluationDate {

        #region Private Properties

        private Server.Application.Gender gender = Server.Application.Gender.Both;

        private Int64 ethnicityId;

        private DataExplorerNodeEvaluationAge ageCriteria = null;

        private DataExplorerNodeEvaluationDate dateCriteria = null;

        #endregion


        #region Public Properties

        public override Server.Application.DataExplorerNodeResultDataType ResultDataType { get { return Server.Application.DataExplorerNodeResultDataType.Member; } }

        public Server.Application.Gender Gender { get { return gender; } set { gender = value; } }

        public Int64 EthnicityId { get { return ethnicityId; } set { ethnicityId = value; } }

        public DataExplorerNodeEvaluationAge AgeCriteria { get { return ageCriteria; } set { ageCriteria = value; } }

        public DataExplorerNodeEvaluationDate DateCriteria { get { return dateCriteria; } set { dateCriteria = value; } }

        #endregion
        
        
        
        #region Constructors

        protected void BaseConstructorMemberDemographic () {

            NodeType = Server.Application.DataExplorerNodeType.Evaluation;

            EvaluationType = Server.Application.DataExplorerEvaluationType.MemberDemographic;

            Evaluation = Server.Application.DataFilterOperator.IsEqualTo;

            AgeCriteria = new DataExplorerNodeEvaluationAge (this);

            DateCriteria = new DataExplorerNodeEvaluationDate (this);

            return;

        }

        public DataExplorerNodeEvaluationMemberDemographic (Application applicationReference) {

            BaseConstructor (applicationReference);

            BaseConstructorMemberDemographic ();

            return;

        }

        public DataExplorerNodeEvaluationMemberDemographic (Application applicationReference, Mercury.Server.Application.DataExplorerNodeEvaluationMemberDemographic serverObject) {

            BaseConstructor (applicationReference, serverObject);

            BaseConstructorMemberDemographic ();

            MapFromServerObject (serverObject);
           
            return;

        }

        #endregion


        #region Public Methods

        public void MapFromServerObject (Server.Application.DataExplorerNodeEvaluationMemberDemographic serverObject) {

            base.MapFromServerObject ((Server.Application.DataExplorerNodeEvaluation)serverObject);


            Gender = serverObject.Gender;

            EthnicityId = serverObject.EthnicityId;

            AgeCriteria = new DataExplorerNodeEvaluationAge (this, serverObject.AgeCriteria);

            DateCriteria = new DataExplorerNodeEvaluationDate (this, serverObject.DateCriteria);


            return;

        }
        public virtual void MapToServerObject (Server.Application.DataExplorerNodeEvaluationMemberDemographic serverObject) {

            base.MapToServerObject ((Server.Application.DataExplorerNodeEvaluation)serverObject);


            serverObject.Gender = Gender;

            serverObject.EthnicityId = EthnicityId;

            serverObject.AgeCriteria = (Server.Application.DataExplorerNodeEvaluationAge)AgeCriteria.ToServerObject ();

            serverObject.DateCriteria = (Server.Application.DataExplorerNodeEvaluationDate) DateCriteria.ToServerObject ();

            
            return;

        }

        public override Object ToServerObject () {

            Server.Application.DataExplorerNodeEvaluationMemberDemographic serverObject = new Server.Application.DataExplorerNodeEvaluationMemberDemographic ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public new DataExplorerNodeEvaluationMemberDemographic Copy () {

            Server.Application.DataExplorerNodeEvaluationMemberDemographic serverObject = (Server.Application.DataExplorerNodeEvaluationMemberDemographic)ToServerObject ();

            DataExplorerNodeEvaluationMemberDemographic copiedObject = new DataExplorerNodeEvaluationMemberDemographic (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (DataExplorerNodeEvaluationMemberDemographic compareObject) {

            Boolean isEqual = base.IsEqual ((DataExplorerNodeEvaluation)compareObject);


            isEqual &= (Gender == compareObject.Gender);

            isEqual &= (EthnicityId == compareObject.EthnicityId);

            isEqual &= (AgeCriteria.IsEqual (compareObject.AgeCriteria));

            isEqual &= (DateCriteria.IsEqual (compareObject.DateCriteria));


            return isEqual;

        }

        #endregion 

    }

}
