using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses {

    [DataContract (Name = "ResponseBase")]
    public class ResponseBase {

        #region Private Properties

        [DataMember (Name = "HasException")]
        private Boolean hasException = false;

        [DataMember (Name = "Exception")]
        private ServiceException serviceException;

        #endregion


        #region Public Properties

        public Boolean HasException { get { return hasException; } set { hasException = value; } } // Property: HasException

        public ServiceException Exception { get { return serviceException; } set { serviceException = value; } } // Property: Exception

        #endregion 


        #region Public Methods

        public virtual void SetException (Exception exception) {

            if (exception != null) {

                hasException = true;

                serviceException = new ServiceException (exception);

            }

            else {

                hasException = false;

                serviceException = null;

            }

            return;

        }

        public virtual void SetException (ServiceException exception) {

            hasException = (serviceException != null);

            serviceException = exception;

            return;

        }

        #endregion

    }

}
