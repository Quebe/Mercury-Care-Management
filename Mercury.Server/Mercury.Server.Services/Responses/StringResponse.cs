using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Mercury.Server.Services.Responses {

    [DataContract]
    public class StringResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Value")]
        private String stringValue = String.Empty;

        #endregion


        #region Public Properties

        public String Value { get { return stringValue; } set { stringValue = value; } } // Property: Result

        #endregion

    }

}