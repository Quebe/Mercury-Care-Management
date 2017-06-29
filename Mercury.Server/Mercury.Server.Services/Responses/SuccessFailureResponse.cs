using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Mercury.Server.Services.Responses {

    [DataContract]
    public class SuccessFailureResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "IsSuccessful")]
        private Boolean isSuccessful;

        #endregion


        #region Public Properties

        public Boolean IsSuccessful { get { return isSuccessful; } set { isSuccessful = value; } } // Property: IsSuccessful

        #endregion

    }

}
