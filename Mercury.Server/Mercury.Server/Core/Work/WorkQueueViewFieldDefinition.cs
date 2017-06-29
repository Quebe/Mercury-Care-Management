using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Work {


    [Serializable]
    [DataContract (Name = "WorkQueueViewFieldDefinition")]
    public class WorkQueueViewFieldDefinition  {

        #region Private Properties

        [DataMember (Name = "WorkQueueViewId")]
        private Int64 workQueueViewId;

        [DataMember (Name = "PropertyName")]
        private String propertyName = String.Empty;

        [DataMember (Name = "DataType")]
        private System.Data.SqlDbType dataType = System.Data.SqlDbType.VarChar;

        [DataMember (Name = "DefaultValue")]
        private String defaultValue;

        [DataMember (Name = "DisplayName")]
        private String displayName = String.Empty;

        #endregion 


        #region Public Properties

        public Int64 WorkQueueViewId { get { return workQueueViewId; } set { workQueueViewId = value; } }

        public String PropertyName { get { return propertyName; } set { propertyName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public System.Data.SqlDbType DataType { get { return dataType; } set { dataType = value; } }

        public String DefaultValue { get { return defaultValue; } set { defaultValue = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public String DisplayName { get { return displayName; } set { displayName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }


        public String SqlDeclaration { 

            get {

                String selectItem = String.Empty;

                switch (dataType) {

                    case System.Data.SqlDbType.TinyInt:

                    case System.Data.SqlDbType.SmallInt:

                    case System.Data.SqlDbType.Int:

                    case System.Data.SqlDbType.BigInt:

                        Int64 defaultValueInt64 = 0;

                        Int64.TryParse (defaultValue, out defaultValueInt64);


                        selectItem = selectItem + "CAST (ISNULL (";

                        selectItem = selectItem + "CASE WHEN (ISNUMERIC (ExtendedProperties.value ('(/ExtendedProperties/Property[@Name=\"" + propertyName + "\"])[1]', 'VARCHAR (030)')) = 1)";

                        selectItem = selectItem + "  THEN ExtendedProperties.value ('(/ExtendedProperties/Property[@Name=\"" + propertyName + "\"])[1]', 'BIGINT')";

                        selectItem = selectItem + "  ELSE " + defaultValueInt64.ToString ();

                        selectItem = selectItem + "  END, " + defaultValueInt64.ToString () + ") AS BIGINT)";

                        break;

                    case System.Data.SqlDbType.DateTime:

                        DateTime defaultedValueDateTime = DateTime.Today;

                        DateTime.TryParse (defaultValue, out defaultedValueDateTime);


                        selectItem = selectItem + "CAST (ISNULL (";

                        selectItem = selectItem + "CASE WHEN (ISDATE (ExtendedProperties.value ('(/ExtendedProperties/Property[@Name=\"" + propertyName + "\"])[1]', 'VARCHAR (030)')) = 1)";

                        selectItem = selectItem + "  THEN ExtendedProperties.value ('(/ExtendedProperties/Property[@Name=\"" + propertyName + "\"])[1]', 'DATE')";

                        selectItem = selectItem + "  ELSE " + "'" + defaultedValueDateTime.ToString ("MM/dd/yyyy") + "'";

                        selectItem = selectItem + "  END, " + "'" + defaultedValueDateTime.ToString ("MM/dd/yyyy") + "'" + ") AS DATE)";

                        break;

                    case System.Data.SqlDbType.Bit:

                        Boolean defaultValueBoolean = false;

                        if (defaultValue == "0") { defaultValueBoolean = false; }

                        else if (defaultValue == "1") { defaultValueBoolean = true; }

                        else {

                            Boolean.TryParse (defaultValue, out defaultValueBoolean);

                        }


                        selectItem = selectItem + "CAST (ISNULL (";

                        selectItem = selectItem + "CASE WHEN ((ExtendedProperties.value ('(/ExtendedProperties/Property[@Name=\"" + propertyName + "\"])[1]', 'VARCHAR (030)')) IN ('True', 'False'))";

                        selectItem = selectItem + "  THEN ExtendedProperties.value ('(/ExtendedProperties/Property[@Name=\"" + propertyName + "\"])[1]', 'VARCHAR (030)')";

                        selectItem = selectItem + "  ELSE " + "'" + defaultValueBoolean.ToString () + "'";

                        selectItem = selectItem + "  END, " + "'" + defaultValueBoolean.ToString () + "'" + ") AS VARCHAR (030))";

                        break;

                    case System.Data.SqlDbType.VarChar:

                        String defaultValueVarChar = String.Empty;

                        defaultValueVarChar = defaultValue.ToString ();


                        selectItem = selectItem + "CAST (ISNULL (";

                        selectItem = selectItem + " ExtendedProperties.value ('(/ExtendedProperties/Property[@Name=\"" + propertyName + "\"])[1]', 'VARCHAR (060)')";

                        selectItem = selectItem + "  ELSE " + "'" + defaultValueVarChar.ToString () + "'";

                        selectItem = selectItem + "  END, " + "'" + defaultValueVarChar.ToString () + "'" + ") AS VARCHAR (060))";

                        break;

                    case System.Data.SqlDbType.Decimal:

                        Decimal defaultValueDecimal = 0;

                        Decimal.TryParse (defaultValue, out defaultValueDecimal);


                        selectItem = selectItem + "CAST (ISNULL (";

                        selectItem = selectItem + "CASE WHEN (ISNUMERIC (ExtendedProperties.value ('(/ExtendedProperties/Property[@Name=\"" + propertyName + "\"])[1]', 'VARCHAR (030)')) = 1)";

                        selectItem = selectItem + "  THEN ExtendedProperties.value ('(/ExtendedProperties/Property[@Name=\"" + propertyName + "\"])[1]', 'DECIMAL')";

                        selectItem = selectItem + "  ELSE " + "'" + defaultValueDecimal.ToString () + "'";

                        selectItem = selectItem + "  END, " + "'" + defaultValueDecimal.ToString () + "'" + ") AS DECIMAL)";

                        break;


                }

                return selectItem;

            }

        }

        public String SqlSelectList {

            get {

                return SqlDeclaration + " AS [" + displayName + "]";

            }

        }

        #endregion 
        

        #region Validation Functions

        public Dictionary<String, String> Validate () {

            Dictionary<String, String> validationResponse = new Dictionary<String, String> ();


            // VALIDATE NAME IS NOT EMPTY
            PropertyName = propertyName;

            if (String.IsNullOrEmpty (PropertyName)) { validationResponse.Add ("Property Name", "Empty or Null"); }


            return validationResponse;

        }

        #endregion

        
        #region Data Functions

        public void MapDataFields (System.Data.DataRow currentRow) {

            WorkQueueViewId = (Int64) currentRow["WorkQueueViewId"];

            PropertyName = (String) currentRow["PropertyName"];

            DataType = (System.Data.SqlDbType) ((Int32) currentRow["DataType"]);

            DefaultValue = (String) currentRow["DefaultValue"];

            DisplayName = (String) currentRow["DisplayName"];

            return;

        }

        #endregion

    }

}

