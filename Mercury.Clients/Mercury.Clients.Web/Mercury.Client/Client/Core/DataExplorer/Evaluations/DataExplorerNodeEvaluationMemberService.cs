using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.DataExplorer.Evaluations {

    [Serializable]
    public class DataExplorerNodeEvaluationMemberService : DataExplorerNodeEvaluation, IDataExplorerNodeEvaluationAge, IDataExplorerNodeEvaluationDate {

        #region Private Properties

        private Int64 serviceId = 0;

        private Int32 countOf = 1;

        private DataExplorerNodeEvaluationAge ageCriteria = null;

        private DataExplorerNodeEvaluationDate dateCriteria = null;

        #endregion


        #region Public Properties

        public override Server.Application.DataExplorerNodeResultDataType ResultDataType { get { return Server.Application.DataExplorerNodeResultDataType.Member; } }

        public override Server.Application.DataExplorerNodeResultDataType ResultDetailDataType { get { return Server.Application.DataExplorerNodeResultDataType.MemberService; } }

        public Int64 ServiceId { get { return serviceId; } set { serviceId = value; } }

        public Int32 CountOf { get { return countOf; } set { countOf = value; } }

        public DataExplorerNodeEvaluationAge AgeCriteria { get { return ageCriteria; } set { ageCriteria = value; } }

        public DataExplorerNodeEvaluationDate DateCriteria { get { return dateCriteria; } set { dateCriteria = value; } }

        #endregion



        #region Constructors

        protected void BaseConstructorMemberService () {

            NodeType = Server.Application.DataExplorerNodeType.Evaluation;

            EvaluationType = Server.Application.DataExplorerEvaluationType.MemberService;

            Evaluation = Server.Application.DataFilterOperator.IsEqualTo;

            AgeCriteria = new DataExplorerNodeEvaluationAge (this);

            DateCriteria = new DataExplorerNodeEvaluationDate (this);

            return;

        }

        public DataExplorerNodeEvaluationMemberService (Application applicationReference) {

            BaseConstructor (applicationReference);

            BaseConstructorMemberService ();

            return;

        }

        public DataExplorerNodeEvaluationMemberService (Application applicationReference, Mercury.Server.Application.DataExplorerNodeEvaluationMemberService serverObject) {

            BaseConstructor (applicationReference, serverObject);

            BaseConstructorMemberService ();

            MapFromServerObject (serverObject);

            return;

        }

        #endregion


        #region Public Methods

        public void MapFromServerObject (Server.Application.DataExplorerNodeEvaluationMemberService serverObject) {

            base.MapFromServerObject ((Server.Application.DataExplorerNodeEvaluation)serverObject);


            serviceId = serverObject.ServiceId;

            countOf = serverObject.CountOf;


            AgeCriteria = new DataExplorerNodeEvaluationAge (this, serverObject.AgeCriteria);

            DateCriteria = new DataExplorerNodeEvaluationDate (this, serverObject.DateCriteria);


            return;

        }
        public virtual void MapToServerObject (Server.Application.DataExplorerNodeEvaluationMemberService serverObject) {

            base.MapToServerObject ((Server.Application.DataExplorerNodeEvaluation)serverObject);


            serverObject.ServiceId = serviceId;

            serverObject.CountOf = countOf;


            serverObject.AgeCriteria = (Server.Application.DataExplorerNodeEvaluationAge)AgeCriteria.ToServerObject ();

            serverObject.DateCriteria = (Server.Application.DataExplorerNodeEvaluationDate)DateCriteria.ToServerObject ();


            return;

        }

        public override Object ToServerObject () {

            Server.Application.DataExplorerNodeEvaluationMemberService serverObject = new Server.Application.DataExplorerNodeEvaluationMemberService ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public new DataExplorerNodeEvaluationMemberService Copy () {

            Server.Application.DataExplorerNodeEvaluationMemberService serverObject = (Server.Application.DataExplorerNodeEvaluationMemberService)ToServerObject ();

            DataExplorerNodeEvaluationMemberService copiedObject = new DataExplorerNodeEvaluationMemberService (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (DataExplorerNodeEvaluationMemberService compareObject) {

            Boolean isEqual = base.IsEqual ((DataExplorerNodeEvaluation)compareObject);


            isEqual &= (ServiceId == compareObject.ServiceId);

            isEqual &= (CountOf == compareObject.CountOf);


            isEqual &= (AgeCriteria.IsEqual (compareObject.AgeCriteria));

            isEqual &= (DateCriteria.IsEqual (compareObject.DateCriteria));


            return isEqual;

        }

        #endregion

    }

}
