using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Workflows.UserInteractions.Request {

    [Serializable]
    [DataContract (Name = "WorkflowUserInteractionRequestUserInterfaceUpdate")]
    public class UserInterfaceUpdateRequest : RequestBase {

        #region Private Properties

        [DataMember (Name = "UpdateType")]
        private Enumerations.UserInterfaceUpdateType updateType = Enumerations.UserInterfaceUpdateType.NotSpecified;

        [DataMember (Name = "InputParameters")]
        private Dictionary<String, String> inputParameters = new Dictionary<String, String> ();

        #endregion


        #region Public Properties

        public Enumerations.UserInterfaceUpdateType UpdateType { get { return updateType; } set { updateType = value; } }

        public Dictionary<String, String> InputParameters { get { return inputParameters; } set { inputParameters = value; } }

        #endregion


        #region Constructor

        public UserInterfaceUpdateRequest () {

            base.userInteractionType = Mercury.Server.Workflows.UserInteractions.Enumerations.UserInteractionType.UserInterfaceUpdate;

            return;
        
        }

        public UserInterfaceUpdateRequest (Enumerations.UserInterfaceUpdateType type) {

            base.userInteractionType = Mercury.Server.Workflows.UserInteractions.Enumerations.UserInteractionType.UserInterfaceUpdate;

            updateType = type;

            return;

        }

        #endregion

    }

}
