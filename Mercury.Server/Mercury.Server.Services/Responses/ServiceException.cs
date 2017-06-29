using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Mercury.Server.Services.Responses {

    [Serializable]
    [DataContract]
    public class ServiceException {

        #region Private Properties

        [DataMember (Name = "InnerException")]
        private ServiceException innerException;

        [DataMember (Name = "Message")]
        private String message;

        [DataMember (Name = "Source")]
        private String source;

        [DataMember (Name = "StackTrace")]
        private String stackTrace;

        [DataMember (Name = "TargetSite")]
        private String targetSite;

        #endregion 


        #region Public Properties

        public ServiceException InnerException { get { return innerException;  } set { innerException = value; } } // Property: InnerException

        public String Message { get { return message; } set { message = ((String.IsNullOrEmpty (value)) ? "** Not Available" : value); } } // Property: Message

        public String Source { get { return source ; } set { source = value; } } // Property: Source

        public String StackTrace{ get { return stackTrace;  } set { stackTrace = value; } } // Property: StackTrace

        public String TargetSite { get { return targetSite;  } set { targetSite = value; } } // Property: TargetSite

        #endregion 


        #region Constructors

        public ServiceException (Exception exception) {

            if (exception != null) {

                Message = exception.Message;

                Source = exception.Source;

                StackTrace = exception.StackTrace;

                if (exception.TargetSite != null) {

                    TargetSite = "[" + exception.TargetSite.Module.Assembly.FullName + "] " +

                                       exception.TargetSite.Module.Name + "." + exception.TargetSite.Name;

                }

                if (exception.InnerException != null) {

                    InnerException = new ServiceException (exception.InnerException);

                }

            }

            else {

                InnerException = null;

                Message = "Unknown Exception Occurred.";

                Source = "Unknown Source.";

                StackTrace = "Unknown Stack Trace.";

            }

            return;

        }

        #endregion

    }

}
