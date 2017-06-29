using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Workflows.UserInteractions.Request {

    [Serializable]
    [DataContract (Name = "WorkflowUserInteractionRequestBase")]
    [KnownType (typeof (UserInterfaceUpdateRequest))]
    [KnownType (typeof (PromptUserRequest))]
    [KnownType (typeof (RequireEntityRequest))]
    [KnownType (typeof (ContactEntityRequest))]
    [KnownType (typeof (RequireFormRequest))]
    [KnownType (typeof (SendCorrespondenceRequest))]
    [KnownType (typeof (OpenImageRequest))]
    public class RequestBase {

        #region Private Properties

        [DataMember (Name = "InteractionType")]
        protected Enumerations.UserInteractionType userInteractionType = Mercury.Server.Workflows.UserInteractions.Enumerations.UserInteractionType.NotSpecified;

        [DataMember (Name = "Message")]
        private String message = String.Empty;

        [DataMember (Name = "AllowCancel")]
        private Boolean allowCancel = false;

        [DataMember (Name = "HasException")]
        private Boolean hasException = false;

        [NonSerialized]
        private Exception exception = null;

        #endregion


        #region Public Properties

        public Enumerations.UserInteractionType UserInteractionType { get { return userInteractionType; } }

        public String Message { get { return message; } set { message = value; } }

        public Boolean AllowCancel { get { return allowCancel; } set { allowCancel = value; } }

        public Boolean HasException { get { return hasException; } set { hasException = value; } } // Property: HasException

        public Exception Exception { get { return exception; } set { exception = value; } } // Property: Exception
        
        #endregion


        #region Public Methods

        virtual public void SetException (Exception forException) {

            hasException = (forException != null);

            exception = forException;

            return;

        }

        #endregion

    }

}
