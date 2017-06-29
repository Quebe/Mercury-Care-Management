using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Workflows.UserInteractions.Response {

    [Serializable]
    [DataContract (Name = "WorkflowUserInteractionResponseBase")]
    [KnownType (typeof (RequireEntityResponse))]
    [KnownType (typeof (PromptUserResponse))]
    [KnownType (typeof (ContactEntityResponse))]
    [KnownType (typeof (RequireFormResponse))]
    [KnownType (typeof (SendCorrespondenceResponse))]
    [KnownType (typeof (OpenImageResponse))]
    public class ResponseBase {

        #region Private Properties

        [DataMember (Name = "InteractionType")]
        protected Enumerations.UserInteractionType userInteractionType = Mercury.Server.Workflows.UserInteractions.Enumerations.UserInteractionType.NotSpecified;

        [DataMember (Name = "Cancel")]
        private Boolean cancel = false;

        [DataMember (Name = "HasException")]
        private Boolean hasException = false;

        [NonSerialized]
        private Exception exception = null;

        #endregion


        #region Public Properties

        virtual public Enumerations.UserInteractionType UserInteractionType { get { return userInteractionType; } }

        public Boolean Cancel { get { return cancel; } set { cancel = value; } }

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
