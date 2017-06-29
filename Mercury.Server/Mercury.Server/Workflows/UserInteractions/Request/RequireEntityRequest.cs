using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Workflows.UserInteractions.Request {

    [Serializable]
    [DataContract (Name = "WorkflowUserInteractionRequestRequireEntity")]
    public class RequireEntityRequest : RequestBase {
        
        #region Private Properties

        [DataMember (Name = "EntityType")]
        private Server.Core.Enumerations.EntityType entityType = Mercury.Server.Core.Enumerations.EntityType.NotSpecified;

        [DataMember (Name = "InitialEntityObjectId")]
        private Int64 initialEntityObjectId = 0;

        [DataMember (Name = "AllowOpenProfile")]
        private Boolean allowOpenProfile = false;

        #endregion


        #region Public Properties

        public Server.Core.Enumerations.EntityType EntityType { get { return entityType; } set { entityType = value; } }

        public Int64 InitialEntityObjectId { get { return initialEntityObjectId; } set { initialEntityObjectId = value; } }

        public Boolean AllowOpenProfile { get { return allowOpenProfile; } set { allowOpenProfile = value; } }

        #endregion


        #region Constructor

        public RequireEntityRequest () {

            base.userInteractionType = Mercury.Server.Workflows.UserInteractions.Enumerations.UserInteractionType.RequireEntity;

            return;
        
        }

        public RequireEntityRequest (Server.Core.Enumerations.EntityType requiredEntityType) {

            base.userInteractionType = Mercury.Server.Workflows.UserInteractions.Enumerations.UserInteractionType.RequireEntity;

            entityType = requiredEntityType;

            return;

        }

        public RequireEntityRequest (Server.Core.Enumerations.EntityType requiredEntityType, String forMessage) {

            base.userInteractionType = Mercury.Server.Workflows.UserInteractions.Enumerations.UserInteractionType.RequireEntity;

            entityType = requiredEntityType;

            Message = forMessage;

            return;

        }

        #endregion

    }

}
