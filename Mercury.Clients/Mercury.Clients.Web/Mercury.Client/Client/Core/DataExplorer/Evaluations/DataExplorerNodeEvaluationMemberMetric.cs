using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.DataExplorer.Evaluations {

    [Serializable]
    public class DataExplorerNodeEvaluationMemberMetric : DataExplorerNodeEvaluation, IDataExplorerNodeEvaluationAge, IDataExplorerNodeEvaluationDate {

        #region Private Properties

        private Int64 metricId = 0;

        private Decimal valueMinimum = 0;

        private Decimal valueMaximum = 0;

        private Int32 countOf = 1;

        private DataExplorerNodeEvaluationAge ageCriteria = null;

        private DataExplorerNodeEvaluationDate dateCriteria = null;

        #endregion


        #region Public Properties

        public override Server.Application.DataExplorerNodeResultDataType ResultDataType { get { return Server.Application.DataExplorerNodeResultDataType.Member; } }

        public override Server.Application.DataExplorerNodeResultDataType ResultDetailDataType { get { return Server.Application.DataExplorerNodeResultDataType.MemberMetric; } }

        public Int64 MetricId { get { return metricId; } set { metricId = value; } }

        public Decimal ValueMinimum { get { return valueMinimum; } set { valueMinimum = value; } }

        public Decimal ValueMaximum { get { return valueMaximum; } set { valueMaximum = value; } }

        public Int32 CountOf { get { return countOf; } set { countOf = value; } }

        public DataExplorerNodeEvaluationAge AgeCriteria { get { return ageCriteria; } set { ageCriteria = value; } }

        public DataExplorerNodeEvaluationDate DateCriteria { get { return dateCriteria; } set { dateCriteria = value; } }

        #endregion



        #region Constructors

        protected void BaseConstructorMemberMetric () {

            NodeType = Server.Application.DataExplorerNodeType.Evaluation;

            EvaluationType = Server.Application.DataExplorerEvaluationType.MemberMetric;

            Evaluation = Server.Application.DataFilterOperator.IsEqualTo;

            AgeCriteria = new DataExplorerNodeEvaluationAge (this);

            DateCriteria = new DataExplorerNodeEvaluationDate (this);

            return;

        }

        public DataExplorerNodeEvaluationMemberMetric (Application applicationReference) {

            BaseConstructor (applicationReference);

            BaseConstructorMemberMetric ();

            return;

        }

        public DataExplorerNodeEvaluationMemberMetric (Application applicationReference, Mercury.Server.Application.DataExplorerNodeEvaluationMemberMetric serverObject) {

            BaseConstructor (applicationReference, serverObject);

            BaseConstructorMemberMetric ();

            MapFromServerObject (serverObject);

            return;

        }

        #endregion


        #region Public Methods

        public void MapFromServerObject (Server.Application.DataExplorerNodeEvaluationMemberMetric serverObject) {

            base.MapFromServerObject ((Server.Application.DataExplorerNodeEvaluation)serverObject);


            metricId = serverObject.MetricId;

            ValueMinimum = serverObject.ValueMinimum;

            ValueMaximum = serverObject.ValueMaximum;

            countOf = serverObject.CountOf;


            AgeCriteria = new DataExplorerNodeEvaluationAge (this, serverObject.AgeCriteria);

            DateCriteria = new DataExplorerNodeEvaluationDate (this, serverObject.DateCriteria);


            return;

        }
        public virtual void MapToServerObject (Server.Application.DataExplorerNodeEvaluationMemberMetric serverObject) {

            base.MapToServerObject ((Server.Application.DataExplorerNodeEvaluation)serverObject);


            serverObject.MetricId = metricId;

            serverObject.ValueMinimum = ValueMinimum;

            serverObject.ValueMaximum = ValueMaximum;

            serverObject.CountOf = countOf;


            serverObject.AgeCriteria = (Server.Application.DataExplorerNodeEvaluationAge)AgeCriteria.ToServerObject ();

            serverObject.DateCriteria = (Server.Application.DataExplorerNodeEvaluationDate)DateCriteria.ToServerObject ();


            return;

        }

        public override Object ToServerObject () {

            Server.Application.DataExplorerNodeEvaluationMemberMetric serverObject = new Server.Application.DataExplorerNodeEvaluationMemberMetric ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public new DataExplorerNodeEvaluationMemberMetric Copy () {

            Server.Application.DataExplorerNodeEvaluationMemberMetric serverObject = (Server.Application.DataExplorerNodeEvaluationMemberMetric)ToServerObject ();

            DataExplorerNodeEvaluationMemberMetric copiedObject = new DataExplorerNodeEvaluationMemberMetric (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (DataExplorerNodeEvaluationMemberMetric compareObject) {

            Boolean isEqual = base.IsEqual ((DataExplorerNodeEvaluation)compareObject);


            isEqual &= (MetricId == compareObject.MetricId);

            isEqual &= (ValueMinimum == compareObject.ValueMinimum);

            isEqual &= (ValueMaximum == compareObject.ValueMaximum);

            isEqual &= (CountOf == compareObject.CountOf);


            isEqual &= (AgeCriteria.IsEqual (compareObject.AgeCriteria));

            isEqual &= (DateCriteria.IsEqual (compareObject.DateCriteria));


            return isEqual;

        }

        #endregion

    }

}
