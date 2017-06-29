using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.DataExplorer.Evaluations {

    [Serializable]
    public class DataExplorerNodeEvaluationAge {

        #region Private Properties

        private Boolean useAgeCriteria = false;

        private Int32 ageMinimum = 0;

        private Int32 ageMaximum = 0;

        private Server.Application.DateQualifier ageQualifier = Server.Application.DateQualifier.Years;

        private DataExplorerNodeEvaluation parentEvaluation = null;

        #endregion


        #region Public Properties

        public Boolean UseAgeCriteria { get { return useAgeCriteria; } set { useAgeCriteria = value; } }

        public Int32 AgeMinimum { get { return ageMinimum; } set { ageMinimum = value; } }

        public Int32 AgeMaximum { get { return ageMaximum; } set { ageMaximum = value; } }

        public Server.Application.DateQualifier AgeQualifier { get { return ageQualifier; } set { ageQualifier = value; } }

        public DataExplorerNodeEvaluation ParentEvaluation { get { return parentEvaluation; } set { parentEvaluation = value; } }

        #endregion


        #region Constructors
        
        public DataExplorerNodeEvaluationAge (DataExplorerNodeEvaluation forParentEvaluation, Server.Application.DataExplorerNodeEvaluationAge serverObject) {

            MapFromServerObject (serverObject);

            parentEvaluation = forParentEvaluation;

            return;

        }

        public DataExplorerNodeEvaluationAge (DataExplorerNodeEvaluation forParentEvaluation) {

            parentEvaluation = forParentEvaluation;

            return;

        }

        #endregion 
        

        #region Public Methods

        public void MapFromServerObject (Server.Application.DataExplorerNodeEvaluationAge serverObject) {

            UseAgeCriteria = serverObject.UseAgeCriteria;

            AgeMinimum = serverObject.AgeMinimum;

            AgeMaximum = serverObject.AgeMaximum;

            AgeQualifier = serverObject.AgeQualifier;

            return;

        }

        public void MapToServerObject (Server.Application.DataExplorerNodeEvaluationAge serverObject) {

            serverObject.UseAgeCriteria = UseAgeCriteria;

            serverObject.AgeMinimum = AgeMinimum;

            serverObject.AgeMaximum = AgeMaximum;

            serverObject.AgeQualifier = AgeQualifier;

            return;

        }

        public Object ToServerObject () {

            Server.Application.DataExplorerNodeEvaluationAge serverObject = new Server.Application.DataExplorerNodeEvaluationAge ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public DataExplorerNodeEvaluationAge Copy () {

            Server.Application.DataExplorerNodeEvaluationAge serverObject = (Server.Application.DataExplorerNodeEvaluationAge)ToServerObject ();

            DataExplorerNodeEvaluationAge copiedObject = new DataExplorerNodeEvaluationAge (parentEvaluation, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (DataExplorerNodeEvaluationAge compareObject) {

            Boolean isEqual = false;


            isEqual &= (UseAgeCriteria == compareObject.UseAgeCriteria);

            isEqual &= (AgeMinimum == compareObject.AgeMinimum);

            isEqual &= (AgeMaximum == compareObject.AgeMaximum);

            isEqual &= (AgeQualifier == compareObject.AgeQualifier);


            return isEqual;

        }

        #endregion 

    }

}
