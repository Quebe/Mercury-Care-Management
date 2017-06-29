using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.DataExplorer.Evaluations {

    [Serializable]
    public class DataExplorerNodeEvaluationDate {
        
        #region Private Properties

        private DataExplorerNodeEvaluation parentEvaluation = null;


        private Server.Application.DataExplorerEvaluationDateType dateType = Server.Application.DataExplorerEvaluationDateType.AsOfDateRelative;

        private DateTime? startDate = null;     // ABSOLUATE DATES (AS OF DATE, BETWEEN START DATE)

        private DateTime? endDate = null;       // ABSOLUATE DATES (BETWEEN END DATE)

        
        private String startDateVariableName = String.Empty; // VARIABLE REFERENCE NAME OR FUNCTION 

        private Int32 startDateRelativeValue = 0;

        private Server.Application.DateQualifier startDateRelativeQualifier = Server.Application.DateQualifier.Days;


        private String endDateVariableName = String.Empty; // VARIABLE REFERENCE NAME OR FUNCTION 

        private Int32 endDateRelativeValue = 0;

        private Server.Application.DateQualifier endDateRelativeQualifier = Server.Application.DateQualifier.Days;

        #endregion 


        #region Public Properties

        public DataExplorerNodeEvaluation ParentEvaluation { get { return parentEvaluation; } set { parentEvaluation = value; } }

        
        public Server.Application.DataExplorerEvaluationDateType DateType { get { return dateType; } set { dateType = value; } }

        public DateTime? StartDate { get { return startDate; } set { startDate = value; } }

        public DateTime? EndDate { get { return endDate; } set { endDate = value; } }


        public String StartDateVariableName { get { return startDateVariableName; } set { startDateVariableName = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public Int32 StartDateRelativeValue { get { return startDateRelativeValue; } set { startDateRelativeValue = value; } }

        public Server.Application.DateQualifier StartDateRelativeQualifier { get { return startDateRelativeQualifier; } set { startDateRelativeQualifier = value; } }


        public String EndDateVariableName { get { return endDateVariableName; } set { endDateVariableName = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public Int32 EndDateRelativeValue { get { return endDateRelativeValue; } set { endDateRelativeValue = value; } }

        public Server.Application.DateQualifier EndDateRelativeQualifier { get { return endDateRelativeQualifier; } set { endDateRelativeQualifier = value; } }

        #endregion 


        #region Constructors

        public DataExplorerNodeEvaluationDate (DataExplorerNodeEvaluation forParentEvaluation, Server.Application.DataExplorerNodeEvaluationDate serverObject) {

            MapFromServerObject (serverObject);

            parentEvaluation = forParentEvaluation;

            return;

        }

        public DataExplorerNodeEvaluationDate (DataExplorerNodeEvaluation forParentEvaluation) {

            parentEvaluation = forParentEvaluation;

            return;

        }

        #endregion 
        

        #region Public Methods

        public void MapFromServerObject (Server.Application.DataExplorerNodeEvaluationDate serverObject) {

            DateType = serverObject.DateType;

            StartDate = serverObject.StartDate;

            EndDate = serverObject.EndDate;


            StartDateVariableName = serverObject.StartDateVariableName;

            StartDateRelativeValue = serverObject.StartDateRelativeValue;

            StartDateRelativeQualifier = serverObject.StartDateRelativeQualifier;


            EndDateVariableName = serverObject.EndDateVariableName;

            EndDateRelativeValue = serverObject.EndDateRelativeValue;

            EndDateRelativeQualifier = serverObject.EndDateRelativeQualifier;

            return;

        }

        public void MapToServerObject (Server.Application.DataExplorerNodeEvaluationDate serverObject) {

            serverObject.DateType = DateType;

            serverObject.StartDate = StartDate;

            serverObject.EndDate = EndDate;


            serverObject.StartDateVariableName = StartDateVariableName;

            serverObject.StartDateRelativeValue = StartDateRelativeValue;

            serverObject.StartDateRelativeQualifier = StartDateRelativeQualifier;


            serverObject.EndDateVariableName = EndDateVariableName;

            serverObject.EndDateRelativeValue = EndDateRelativeValue;

            serverObject.EndDateRelativeQualifier = EndDateRelativeQualifier;

            return;

        }

        public Object ToServerObject () {

            Server.Application.DataExplorerNodeEvaluationDate serverObject = new Server.Application.DataExplorerNodeEvaluationDate ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public DataExplorerNodeEvaluationDate Copy () {

            Server.Application.DataExplorerNodeEvaluationDate serverObject = (Server.Application.DataExplorerNodeEvaluationDate)ToServerObject ();

            DataExplorerNodeEvaluationDate copiedObject = new DataExplorerNodeEvaluationDate (parentEvaluation, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (DataExplorerNodeEvaluationDate compareObject) {

            Boolean isEqual = false;




            return isEqual;

        }

        #endregion 

    }

}
