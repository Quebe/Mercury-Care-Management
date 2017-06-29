using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Work {

    [Serializable]
    [DataContract (Name = "WorkQueueViewSortDefinition")]
    public class WorkQueueViewSortDefinition {

        #region Private Properties

        [DataMember (Name = "WorkQueueViewId")]
        private Int64 workQueueViewId;

        [DataMember (Name = "Sequence")]
        private Int32 sequence;

        [DataMember (Name = "FieldName")]
        private String fieldName = String.Empty;

        [DataMember (Name = "SortDirection")]
        private Data.Enumerations.DataSortDirection sortDirection = Mercury.Server.Data.Enumerations.DataSortDirection.Ascending;

        #endregion


        #region Public Properties

        public Int64 WorkQueueViewId { get { return workQueueViewId; } set { workQueueViewId = value; } }

        public Int32 Sequence { get { return sequence; } set { sequence = value; } }

        public String FieldName { get { return fieldName; } set { fieldName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public Data.Enumerations.DataSortDirection SortDirection { get { return sortDirection; } set { sortDirection = value; } }


        public String SqlSortList {

            get {

                String sortItem = "[" + FieldName + "] ";

                switch (SortDirection) {

                    case Mercury.Server.Data.Enumerations.DataSortDirection.Ascending: sortItem = sortItem + "ASC"; break;

                    case Mercury.Server.Data.Enumerations.DataSortDirection.Descending: sortItem = sortItem + "DESC"; break;

                }

                return sortItem;

            }

        }

        #endregion


        #region Validation Functions

        public Dictionary<String, String> Validate () {

            Dictionary<String, String> validationResponse = new Dictionary<String, String> ();


            // VALIDATE NAME IS NOT EMPTY
            FieldName = fieldName;

            if (String.IsNullOrEmpty (FieldName)) { validationResponse.Add ("Sort Field Name", "Empty or Null"); }


            return validationResponse;

        }

        #endregion


        #region Data Functions

        public void MapDataFields (System.Data.DataRow currentRow) {

            WorkQueueViewId = (Int64) currentRow["WorkQueueViewId"];

            Sequence = (Int32) currentRow["Sequence"];

            FieldName = (String) currentRow["FieldName"];

            SortDirection = (Mercury.Server.Data.Enumerations.DataSortDirection) ((Int32) currentRow["SortDirection"]);

            return;

        }

        #endregion

    }

}

