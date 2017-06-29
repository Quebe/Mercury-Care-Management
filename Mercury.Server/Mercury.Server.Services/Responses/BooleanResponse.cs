using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Mercury.Server.Services.Responses {

    [DataContract]
    public class BooleanResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Result")]
        private Boolean result = false;

        #endregion


        #region Public Properties

        public Boolean Result { get { return result; } set { result = value; } } // Property: Result

        #endregion

    }

}
