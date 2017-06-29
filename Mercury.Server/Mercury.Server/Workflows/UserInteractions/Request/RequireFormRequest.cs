using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Workflows.UserInteractions.Request {

    [Serializable]
    [DataContract (Name = "WorkflowUserInteractionRequestRequireForm")]
    public class RequireFormRequest : RequestBase {
        
        #region Private Properties

        [DataMember (Name = "EntityType")]
        private Server.Core.Enumerations.EntityType entityType = Mercury.Server.Core.Enumerations.EntityType.NotSpecified;

        [DataMember (Name = "EntityObjectId")]
        private Int64 entityId;

        [DataMember (Name = "Form")]
        private Core.Forms.Form form = new Mercury.Server.Core.Forms.Form (null);

        [DataMember (Name = "AllowSaveAsDraft")]
        private Boolean allowSaveAsDraft = false;

        #endregion


        #region Public Properties

        public Server.Core.Enumerations.EntityType EntityType { get { return entityType; } set { entityType = value; } }

        public Int64 EntityObjectId { get { return entityId; } set { entityId = value; } }

        public Server.Core.Forms.Form Form { get { return form; } set { form = value; } }

        public Boolean AllowSaveAsDraft { get { return allowSaveAsDraft; } set { allowSaveAsDraft = value; } }

        #endregion


        #region Constructor

        public RequireFormRequest () {

            base.userInteractionType = Mercury.Server.Workflows.UserInteractions.Enumerations.UserInteractionType.RequireForm;

            return;
        
        }

        public RequireFormRequest (Server.Core.Enumerations.EntityType requiredEntityType) {

            base.userInteractionType = Mercury.Server.Workflows.UserInteractions.Enumerations.UserInteractionType.RequireForm;

            entityType = requiredEntityType;

            return;

        }

        public RequireFormRequest (Server.Core.Enumerations.EntityType requiredEntityType, String forMessage) {

            base.userInteractionType = Mercury.Server.Workflows.UserInteractions.Enumerations.UserInteractionType.RequireForm;

            entityType = requiredEntityType;

            Message = forMessage;

            return;

        }

        #endregion
   
    }

}
