using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Work {

    [Serializable]
    [DataContract (Name = "WorkQueueViewFilterDefinition")]
    public class WorkQueueViewFilterDefinition : Data.FilterDescriptor {

        #region Private Properties

        [DataMember (Name = "WorkQueueViewId")]
        private Int64 workQueueViewId;

        [DataMember (Name = "Sequence")]
        private Int32 sequence;

        #endregion


        #region Public Properties

        public Int64 WorkQueueViewId { get { return workQueueViewId; } set { workQueueViewId = value; } }

        public Int32 Sequence { get { return sequence; } set { sequence = value; } }

        #endregion


        #region Validation Functions

        public Dictionary<String, String> Validate () {

            Dictionary<String, String> validationResponse = new Dictionary<String, String> ();


            // VALIDATE NAME IS NOT EMPTY
            if (Parameter == null) { validationResponse.Add ("Filter Field Name", "Empty or Null"); }

            else if (String.IsNullOrWhiteSpace (Parameter.Name)) { validationResponse.Add ("Filter Field Name", "Empty or Null"); }


            return validationResponse;

        }

        #endregion


        #region Data Functions

        public void MapDataFields (System.Data.DataRow currentRow) {

            WorkQueueViewId = (Int64)currentRow["WorkQueueViewId"];

            Sequence = (Int32)currentRow["Sequence"];

            
            // MAP BASE OBJECT PROPERTIES

            IgnoredValue = null;

            IsCaseSensitive = false;

            Operator = (Mercury.Server.Data.Enumerations.DataFilterOperator) Convert.ToInt32 (currentRow ["FilterOperator"]);

            PropertyPath = (String) currentRow ["FieldName"];

            Parameter = new Data.Parameter ();

            Parameter.Value = (String)currentRow["FilterValue"];

            return;

        }

        #endregion

    }

}

