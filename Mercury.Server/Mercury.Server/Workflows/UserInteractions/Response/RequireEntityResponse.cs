using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Workflows.UserInteractions.Response {

    [Serializable]
    [DataContract (Name = "WorkflowUserInteractionResponseRequireEntity")]
    public class RequireEntityResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "EntityType")]
        private Server.Core.Enumerations.EntityType entityType = Mercury.Server.Core.Enumerations.EntityType.NotSpecified;

        [DataMember (Name = "EntityId")]
        private Int64 entityId = 0;

        #endregion


        #region Public Properties

        public override Enumerations.UserInteractionType UserInteractionType { get { return Enumerations.UserInteractionType.RequireEntity; } }

        public Server.Core.Enumerations.EntityType EntityType { get { return entityType; } set { entityType = value; } }

        public Int64 EntityId { get { return entityId; } set { entityId = value; } }

        #endregion


        #region Constructor

        public RequireEntityResponse () {

            return;

        }

        public RequireEntityResponse (Server.Core.Enumerations.EntityType requiredEntityType) {

            entityType = requiredEntityType;

            return;

        }

        #endregion

    }

}
