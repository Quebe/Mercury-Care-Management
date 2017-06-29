using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.DataExplorer {

    [DataContract (Name = "DataExplorerVariable")]
    public class DataExplorerVariable : CoreObject {

        #region Private Properties

        [DataMember (Name = "VariableType")]
        private Enumerations.DataExplorerVariableType variableType = Enumerations.DataExplorerVariableType.Text;

        [DataMember (Name = "TextValue")]
        private String textValue = String.Empty;

        [DataMember (Name = "NumericValue")]
        private Decimal numericValue = 0;

        [DataMember (Name = "DateValue")]
        private DateTime? dateValue = null;

        #endregion 


        #region Public Properties

        public Enumerations.DataExplorerVariableType VariableType { get { return variableType; } set { variableType = value; } }

        public String TextValue { get { return textValue; } set { textValue = value; } }

        public Decimal NumericValue { get { return numericValue; } set { numericValue = value; } }

        public DateTime? DateValue { get { return dateValue; } set { dateValue = value; } }

        #endregion 


        #region Constructors

        public DataExplorerVariable (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        #endregion 

    }

}
