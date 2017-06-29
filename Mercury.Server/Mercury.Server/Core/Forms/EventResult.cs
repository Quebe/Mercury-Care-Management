using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Forms {

    [Serializable]
    [DataContract (Name = "FormControlEventResult")]
    public class EventResult {

        #region Private Properties

        [DataMember (Name = "ControlId")]
        private Guid controlId = Guid.Empty;

        [DataMember (Name = "EventName")]
        private String eventName = String.Empty;

        [DataMember (Name = "Success")]
        private Boolean success = false;

        [DataMember (Name = "HasException")]
        private Boolean hasException = false;

        //[DataMember (Name = "Exception")]
        //private Mercury.Server.Services.ServiceException serviceException = null;

        [DataMember (Name = "ListenerOutput")]
        private String listenerOutput;

        #endregion 


        #region Public Properties

        public Guid ControlId { get { return controlId; } set { controlId = value; } }

        public String EventName { get { return eventName; } set { eventName = value; } }

        public Boolean Success { get { return success; } set { success = value; } }

        public Boolean HasException { get { return hasException; } set { hasException = value; } } // Property: HasException

        //public Mercury.Server.Services.ServiceException Exception { get { return serviceException; } set { serviceException = value; } } // Property: Exception

        public String ListenerOutput { get { return listenerOutput; } set { listenerOutput = value; } }

        #endregion


        #region Constructors

        public EventResult (Guid forControlId, String forEventName) {

            controlId = forControlId;

            eventName = forEventName;

            return;

        }

        public EventResult (Guid forControlId, String forEventName, Boolean forSuccess) {

            controlId = forControlId;

            eventName = forEventName;

            success = forSuccess;

            return;

        }

        public EventResult (Guid forControlId, String forEventName, Exception exception) {

            controlId = forControlId;

            eventName = forEventName;

            success = false;

            SetException (exception);

            return;

        }

        #endregion 


        #region Public Methods

        virtual public void SetException (Exception exception) {

            hasException = true;

            //serviceException = new Mercury.Server.Services.ServiceException (exception);

            return;

        }

        #endregion


    }

}
