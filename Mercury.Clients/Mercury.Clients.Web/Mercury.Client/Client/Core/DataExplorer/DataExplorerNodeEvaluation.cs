using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.DataExplorer {

    [Serializable]
    public class DataExplorerNodeEvaluation : DataExplorerNode {

        #region Private Properties

        private Server.Application.DataExplorerEvaluationType evaluationType = Server.Application.DataExplorerEvaluationType.NotSpecified;

        private Server.Application.DataFilterOperator evaluation = Server.Application.DataFilterOperator.IsEqualTo;

        #endregion 


        #region Public Properties

        public Server.Application.DataExplorerEvaluationType EvaluationType { get { return evaluationType; } set { evaluationType = value; } }

        public Server.Application.DataFilterOperator Evaluation { get { return evaluation; } set { evaluation = value; } }

        #endregion 

        
        #region Constructors

        protected DataExplorerNodeEvaluation () { /* DO NOTHING */ }
        
        public DataExplorerNodeEvaluation (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public DataExplorerNodeEvaluation (Application applicationReference, Mercury.Server.Application.DataExplorerNodeEvaluation serverObject) {

            BaseConstructor (applicationReference, serverObject);


            EvaluationType = serverObject.EvaluationType;

            Evaluation = serverObject.Evaluation;
            
            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.DataExplorerNodeEvaluation serverObject) {

            base.MapToServerObject ((Server.Application.DataExplorerNodeEvaluation)serverObject);


            serverObject.EvaluationType = EvaluationType;

            serverObject.Evaluation = Evaluation;


            return;

        }

        public override Object ToServerObject () {

            Server.Application.DataExplorerNodeEvaluation serverObject = new Server.Application.DataExplorerNodeEvaluation ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public new DataExplorerNodeEvaluation Copy () {

            Server.Application.DataExplorerNodeEvaluation serverObject = (Server.Application.DataExplorerNodeEvaluation)ToServerObject ();

            DataExplorerNodeEvaluation copiedObject = new DataExplorerNodeEvaluation (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (DataExplorerNodeEvaluation compareObject) {

            Boolean isEqual = base.IsEqual ((DataExplorerNode)compareObject);


            isEqual &= (EvaluationType == compareObject.EvaluationType);

            isEqual &= (Evaluation == compareObject.Evaluation);


            return isEqual;

        }

        #endregion 


    }

}
