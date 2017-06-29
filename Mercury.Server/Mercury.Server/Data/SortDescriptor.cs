using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Data {

    [DataContract (Name = "DataSortDescriptor")]
    public class SortDescriptor {

        #region Private Properties

        [DataMember (Name = "FieldName")]
        private String fieldName = String.Empty;

        [DataMember (Name = "SortDirection")]
        private Data.Enumerations.DataSortDirection sortDirection = Mercury.Server.Data.Enumerations.DataSortDirection.Ascending;

        #endregion


        #region Public Properties

        public String FieldName { get { return fieldName; } set { fieldName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public Data.Enumerations.DataSortDirection SortDirection { get { return sortDirection; } set { sortDirection = value; } }


        public String SqlSortList {

            get {

                String sortItem = String.Empty;


                if (FieldName.Contains (".")) { sortItem = FieldName + " "; } 
                
                else { sortItem = "[" + FieldName + "] "; }


                switch (SortDirection) {

                    case Mercury.Server.Data.Enumerations.DataSortDirection.Ascending: sortItem = sortItem + "ASC"; break;

                    case Mercury.Server.Data.Enumerations.DataSortDirection.Descending: sortItem = sortItem + "DESC"; break;

                }

                return sortItem;

            }

        }

        #endregion
        

        #region Constructors 

        public SortDescriptor () { /* DO NOTHING */ }

        public SortDescriptor (String forFieldName, Data.Enumerations.DataSortDirection forSortDirection) {

            FieldName = forFieldName;

            SortDirection = forSortDirection;

            return;

        }
        
        #endregion
    }

}
