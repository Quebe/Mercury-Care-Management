using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.DataExplorer.Evaluations {

    [DataContract (Name = "DataExplorerNodeEvaluationAge")]
    public class DataExplorerNodeEvaluationAge {

        #region Private Properties

        [DataMember (Name = "UseAgeCriteria")]
        private Boolean useAgeCriteria = false;

        [DataMember (Name = "AgeMinimum")]
        private Int32 ageMinimum = 0;

        [DataMember (Name = "AgeMaximum")]
        private Int32 ageMaximum = 0;

        [DataMember (Name = "AgeQualifier")]
        private Core.Enumerations.DateQualifier ageQualifier = Core.Enumerations.DateQualifier.Years;

        private DataExplorerNodeEvaluation parentEvaluation = null;

        #endregion 


        #region Public Properties

        public Boolean UseAgeCriteria { get { return useAgeCriteria; } set { useAgeCriteria = value; } }

        public Int32 AgeMinimum { get { return ageMinimum; } set { ageMinimum = value; } }

        public Int32 AgeMaximum { get { return ageMaximum; } set { ageMaximum = value; } }

        public Core.Enumerations.DateQualifier AgeQualifier { get { return ageQualifier; } set { ageQualifier = value; } }

        public DataExplorerNodeEvaluation ParentEvaluation { get { return parentEvaluation; } set { parentEvaluation = value; }}

        #endregion 
        

        #region Constructors

        public DataExplorerNodeEvaluationAge (DataExplorerNodeEvaluation forParentEvaluation) {

            parentEvaluation = forParentEvaluation;

            return;

        }

        #endregion 


        #region Public Methods

        public void MapFromExtendedProperties (Dictionary<String, String> extendedProperties, String prefix = "AgeCriteria") {

            if (extendedProperties.ContainsKey (prefix + ".UseAgeCriteria")) { UseAgeCriteria = Convert.ToBoolean (extendedProperties[prefix + ".UseAgeCriteria"]); }

            if (extendedProperties.ContainsKey (prefix + ".AgeMinimum")) { AgeMinimum = Convert.ToInt32 (extendedProperties[prefix + ".AgeMinimum"]); }

            if (extendedProperties.ContainsKey (prefix + ".AgeMaximum")) { AgeMaximum = Convert.ToInt32 (extendedProperties[prefix + ".AgeMaximum"]); }

            if (extendedProperties.ContainsKey (prefix + ".AgeQualifierInt32")) { AgeQualifier = (Core.Enumerations.DateQualifier) Convert.ToInt32 (extendedProperties[prefix + ".AgeQualifierInt32"]); }

            return;

        }

        public Dictionary<String, String> MapToExtendedProperties (String prefix = "AgeCriteria") {

            Dictionary<String, String> extendedProperties = new Dictionary<String, String> ();


            extendedProperties.Add (prefix + ".UseAgeCriteria", UseAgeCriteria.ToString ());

            extendedProperties.Add (prefix + ".AgeMinimum", AgeMinimum.ToString ());

            extendedProperties.Add (prefix + ".AgeMaximum", AgeMaximum.ToString ());

            extendedProperties.Add (prefix + ".AgeQualifierInt32", ((Int32)AgeQualifier).ToString ());


            return extendedProperties;

        }

        #endregion 

    }
}
