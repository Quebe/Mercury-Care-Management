using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Data {

    [DataContract (Name = "DataContract")]
    public class Parameter {

        #region Private Properties

        [DataMember (Name = "Name")]
        private String parameterName = String.Empty;

        [DataMember (Name = "Value")]
        private Object parameterValue = null;

        #endregion 


        #region Public Properties

        public string Name { get { return parameterName; } set { parameterName = value; } }

        // public string ParameterName { get { return parameterName; } set { parameterName = value; } }

        public virtual Object Value { get { return parameterValue; } set { parameterValue = value; } }

        #endregion


        #region Constructors

        public Parameter () { /* DO NOTHING */ }

        public Parameter (String forName, Object forValue) {

            parameterName = forName;

            parameterValue = forValue;

            return;

        }

        #endregion 


    }
}
