using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.DataExplorer.Evaluations {

    [DataContract (Name = "DataExplorerNodeEvaluationDate")]
    public class DataExplorerNodeEvaluationDate {

        #region Private Properties
        
        private DataExplorerNodeEvaluation parentEvaluation = null;


        [DataMember (Name = "DateType")]
        private Core.DataExplorer.Enumerations.DataExplorerEvaluationDateType dateType = Core.DataExplorer.Enumerations.DataExplorerEvaluationDateType.AsOfDateRelative;

        [DataMember (Name = "StartDate")]
        private DateTime? startDate = null;     // ABSOLUATE DATES (AS OF DATE, BETWEEN START DATE)

        [DataMember (Name = "EndDate")]
        private DateTime? endDate = null;       // ABSOLUATE DATES (BETWEEN END DATE)


        [DataMember (Name = "StartDateVariableName")]
        private String startDateVariableName = String.Empty; // VARIABLE REFERENCE NAME OR FUNCTION 

        [DataMember (Name = "StartDateRelativeValue")]
        private Int32 startDateRelativeValue = 0;

        [DataMember (Name = "StartDateRelativeQualifier")]
        private Core.Enumerations.DateQualifier startDateRelativeQualifier = Core.Enumerations.DateQualifier.Days;


        [DataMember (Name = "EndDateVariableName")]
        private String endDateVariableName = String.Empty; // VARIABLE REFERENCE NAME OR FUNCTION 

        [DataMember (Name = "EndDateRelativeValue")]
        private Int32 endDateRelativeValue = 0;

        [DataMember (Name = "EndDateRelativeQualifier")]
        private Core.Enumerations.DateQualifier endDateRelativeQualifier = Core.Enumerations.DateQualifier.Days;

        #endregion 


        #region Public Properties

        public DataExplorerNodeEvaluation ParentEvaluation { get { return parentEvaluation; } set { parentEvaluation = value; } }

        
        public Core.DataExplorer.Enumerations.DataExplorerEvaluationDateType DateType { get { return dateType; } set { dateType = value; } }

        public DateTime? StartDate { get { return startDate; } set { startDate = value; } }

        public DateTime? EndDate { get { return endDate; } set { endDate = value; } }


        public String StartDateVariableName { get { return startDateVariableName; } set { startDateVariableName = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public Int32 StartDateRelativeValue { get { return startDateRelativeValue; } set { startDateRelativeValue = value; } }

        public Core.Enumerations.DateQualifier StartDateRelativeQualifier { get { return startDateRelativeQualifier; } set { startDateRelativeQualifier = value; } }


        public String EndDateVariableName { get { return endDateVariableName; } set { endDateVariableName = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public Int32 EndDateRelativeValue { get { return endDateRelativeValue; } set { endDateRelativeValue = value; } }

        public Core.Enumerations.DateQualifier EndDateRelativeQualifier { get { return endDateRelativeQualifier; } set { endDateRelativeQualifier = value; } }

        #endregion 


        #region Constructors

        public DataExplorerNodeEvaluationDate (DataExplorerNodeEvaluation forParentEvaluation) {

            parentEvaluation = forParentEvaluation;

            return;

        }

        #endregion 


        #region Public Methods

        public void MapFromExtendedProperties (Dictionary<String, String> extendedProperties, String prefix = "DateCriteria") {

            if (extendedProperties.ContainsKey (prefix + ".DateTypeInt32")) { DateType = (Core.DataExplorer.Enumerations.DataExplorerEvaluationDateType)Convert.ToInt32 (extendedProperties[prefix + ".DateTypeInt32"]); }


            if (extendedProperties.ContainsKey (prefix + ".StartDate")) { StartDate = Convert.ToDateTime (extendedProperties[prefix + ".StartDate"]); }

            if (extendedProperties.ContainsKey (prefix + ".EndDate")) { EndDate = Convert.ToDateTime (extendedProperties[prefix + ".EndDate"]); }


            if (extendedProperties.ContainsKey (prefix + ".StartDateVariableName")) { StartDateVariableName = extendedProperties[prefix + ".StartDateVariableName"]; }

            if (extendedProperties.ContainsKey (prefix + ".StartDateRelativeValue")) { StartDateRelativeValue = Convert.ToInt32 (extendedProperties[prefix + ".StartDateRelativeValue"]); }

            if (extendedProperties.ContainsKey (prefix + ".StartDateRelativeQualifier")) { StartDateRelativeQualifier = (Core.Enumerations.DateQualifier) Convert.ToInt32 (extendedProperties[prefix + ".StartDateRelativeQualifier"]); }


            if (extendedProperties.ContainsKey (prefix + ".EndDateVariableName")) { EndDateVariableName = extendedProperties[prefix + ".EndDateVariableName"]; }

            if (extendedProperties.ContainsKey (prefix + ".EndDateRelativeValue")) { EndDateRelativeValue = Convert.ToInt32 (extendedProperties[prefix + ".EndDateRelativeValue"]); }

            if (extendedProperties.ContainsKey (prefix + ".EndDateRelativeQualifier")) { EndDateRelativeQualifier = (Core.Enumerations.DateQualifier)Convert.ToInt32 (extendedProperties[prefix + ".EndDateRelativeQualifier"]); }


            return;

        }

        public Dictionary<String, String> MapToExtendedProperties (String prefix = "DateCriteria") {

            Dictionary<String, String> extendedProperties = new Dictionary<String, String> ();


            extendedProperties.Add (prefix + ".DateTypeInt32", ((Int32)DateType).ToString ());

            if (StartDate.HasValue) { extendedProperties.Add (prefix + ".StartDate", StartDate.Value.ToString ("MM/dd/yyyy")); }

            if (EndDate.HasValue) { extendedProperties.Add (prefix + ".EndDate", EndDate.Value.ToString ("MM/dd/yyyy")); }


            extendedProperties.Add (prefix + ".StartDateVariableName", StartDateVariableName.ToString ());

            extendedProperties.Add (prefix + ".StartDateRelativeValue", StartDateRelativeValue.ToString ());

            extendedProperties.Add (prefix + ".StartDateRelativeQualifier", ((Int32)StartDateRelativeQualifier).ToString ());


            extendedProperties.Add (prefix + ".EndDateVariableName", EndDateVariableName.ToString ());

            extendedProperties.Add (prefix + ".EndDateRelativeValue", EndDateRelativeValue.ToString ());

            extendedProperties.Add (prefix + ".EndDateRelativeQualifier", ((Int32)EndDateRelativeQualifier).ToString ());


            return extendedProperties;

        }

        public DateTime? CalculateStartDate () {

            DateTime? calculatedDate = startDate;


            if (!startDate.HasValue) {

                if (String.IsNullOrWhiteSpace (StartDateVariableName)) { StartDateVariableName = "DateTime.Today"; }

                switch (StartDateVariableName) {

                    case "DateTime.Today": calculatedDate = DateTime.Today; break;

                    case "DateTime.FirstDayOfYear": calculatedDate = new DateTime (DateTime.Today.Year, 1, 1); break;

                    case "DateTime.LastDayOfYear": calculatedDate = new DateTime (DateTime.Today.Year, 12, 13); break;

                    default:

                        // VARIABLE NAME LOOK UP

                        break;

                    }

            }

            if (calculatedDate.HasValue) {

                switch (startDateRelativeQualifier) {

                    case Core.Enumerations.DateQualifier.Days: calculatedDate = calculatedDate.Value.AddDays (startDateRelativeValue); break;

                    case Core.Enumerations.DateQualifier.Months: calculatedDate = calculatedDate.Value.AddMonths (startDateRelativeValue); break;

                    case Core.Enumerations.DateQualifier.Years: calculatedDate = calculatedDate.Value.AddYears (startDateRelativeValue); break;

                }

            }

            return calculatedDate;

        }

        public DateTime? CalculateEndDate () {

            DateTime? calculatedDate = startDate;


            if (!endDate.HasValue) {

                if (String.IsNullOrWhiteSpace (EndDateVariableName)) { EndDateVariableName = "DateTime.Today"; }

                switch (EndDateVariableName) {

                    case "DateTime.Today": calculatedDate = DateTime.Today; break;

                    case "DateTime.FirstDayOfYear": calculatedDate = new DateTime (DateTime.Today.Year, 1, 1); break;

                    case "DateTime.LastDayOfYear": calculatedDate = new DateTime (DateTime.Today.Year, 12, 13); break;

                    default:

                        // VARIABLE NAME LOOK UP

                        break;

                }

            }

            if (calculatedDate.HasValue) {

                switch (endDateRelativeQualifier) {

                    case Core.Enumerations.DateQualifier.Days: calculatedDate = calculatedDate.Value.AddDays (endDateRelativeValue); break;

                    case Core.Enumerations.DateQualifier.Months: calculatedDate = calculatedDate.Value.AddMonths (endDateRelativeValue); break;

                    case Core.Enumerations.DateQualifier.Years: calculatedDate = calculatedDate.Value.AddYears (endDateRelativeValue); break;

                }

            }

            return calculatedDate;

        }

        #endregion 

    }

}
