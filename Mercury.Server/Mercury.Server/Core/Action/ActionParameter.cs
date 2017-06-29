using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Action {

    [Serializable]
    [DataContract (Name = "ActionParameter")]
    public class ActionParameter {

        #region Private Properties

        [DataMember (Name = "ParameterName")]
        private String parameterName;

        [DataMember (Name = "DataType")]
        private Enumerations.ActionParameterDataType dataType = Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.Id;

        [DataMember (Name = "AllowFixedValue")]
        private Boolean allowFixedValue = false;

        [DataMember (Name = "Required")]
        private Boolean required = true;

        [DataMember (Name = "ValueType")]
        private Enumerations.ActionParameterValueType valueType = Mercury.Server.Core.Action.Enumerations.ActionParameterValueType.FixedValue;

        [DataMember (Name = "Value")]
        private String parameterValue = String.Empty;

        [DataMember (Name = "ValueDescription")]
        private String parameterValueDescription = String.Empty;

        #endregion


        #region Public Properties

        public String Name { get { return parameterName; } set { parameterName = value; } }

        public Enumerations.ActionParameterDataType DataType { get { return dataType; } set { dataType = value; } }

        public Boolean AllowFixedValue { get { return allowFixedValue; } set { allowFixedValue = value; } }

        public Boolean Required { get { return required; } set { required = value; } }

        public Enumerations.ActionParameterValueType ValueType { get { return valueType; } set { valueType = value; } }

        public String Value { get { return parameterValue; } set { parameterValue = value; } }

        public String ValueDescription { get { return parameterValueDescription; } set { parameterValueDescription = value; } }

        #endregion

    }

}
