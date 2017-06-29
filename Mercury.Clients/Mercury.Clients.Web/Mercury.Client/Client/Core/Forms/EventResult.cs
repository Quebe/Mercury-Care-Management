using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Forms {

    [Serializable]
    public class EventResult {

        #region Private Properties

        private Guid controlId = Guid.Empty;

        private String eventName = String.Empty;

        private Boolean success = false;

        private Boolean hasException = false;

        private Exception lastException = null;

        private String listenerOutput;

        #endregion


        #region Public Properties

        public Guid ControlId { get { return controlId; } set { controlId = value; } }

        public String EventName { get { return eventName; } set { eventName = value; } }

        public Boolean Success { get { return success; } set { success = value; } }

        public Boolean HasException { get { return hasException; } set { hasException = value; } } // Property: HasException

        public Exception LastException { get { return lastException; } set { lastException = value; } } // Property: Exception

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

        public EventResult (Application application, Server.Application.FormControlEventResult serverResult) {

            controlId = serverResult.ControlId;

            eventName = serverResult.EventName;

            success = serverResult.Success;

            hasException = serverResult.HasException;

            lastException = null;

            // TODO: FIX EXCEPTION
            
            //if (serverResult.Exception != null) {

            //    lastException = new ApplicationException (serverResult.Exception.Message);

            //}

            listenerOutput = serverResult.ListenerOutput;

            return;

        }

        #endregion


        #region Public Methods

        virtual public void SetException (Exception exception) {

            hasException = true;

            lastException = exception;

            return;

        }

        #endregion


    }

}