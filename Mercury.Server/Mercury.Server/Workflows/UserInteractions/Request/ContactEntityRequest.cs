using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Workflows.UserInteractions.Request {

    [Serializable]
    [DataContract (Name = "WorkflowUserInteractionRequestContactEntity")]
    public class ContactEntityRequest : RequestBase {

        #region Private Properties
        
        [DataMember (Name = "Entity")]
        private Server.Core.Entity.Entity entity = null;

        [DataMember (Name = "EntityContactInformation")]
        private List<Server.Core.Entity.EntityContactInformation> entityContactInformations = null;

        [DataMember (Name = "Attempt")]
        private Int32 attempt = 1;

        [DataMember (Name = "Regarding")]
        private String regarding = String.Empty;

        [DataMember (Name = "RelatedEntity")]
        private Server.Core.Entity.Entity relatedEntity = null;


        [DataMember (Name = "AllowEditRelatedEntity")]
        private Boolean allowEditRelatedEntity = false;

        [DataMember (Name = "AllowEditRegarding")]
        private Boolean allowEditRegarding = true;

        [DataMember (Name = "AllowEditContactDateTime")]
        private Boolean allowEditContactDateTime = false;

        [DataMember (Name = "IntroductionScript")]
        private String introductionScript = String.Empty;

        #endregion


        #region Public Properties

        public Server.Core.Entity.Entity Entity { get { return entity; } set { entity = value; } }

        public List<Server.Core.Entity.EntityContactInformation> EntityContactInformations { get { return entityContactInformations; } set { entityContactInformations = value; } }

        public Int32 Attempt { get { return attempt; } set { attempt = value; } }

        public String Regarding { get { return regarding; } set { regarding = value; } }

        public Server.Core.Entity.Entity RelatedEntity { get { return relatedEntity; } set { relatedEntity = value; } }

        public Boolean AllowEditRelatedEntity { get { return allowEditRelatedEntity; } set { allowEditRelatedEntity = value; } }

        public Boolean AllowEditRegarding { get { return allowEditRegarding; } set { allowEditRegarding = value; } }

        public Boolean AllowEditContactDateTime { get { return allowEditContactDateTime; } set { allowEditContactDateTime = value; } }

        public String IntroductionScript { get { return introductionScript; } set { introductionScript = value; } }

        #endregion


        #region Constructor

        public ContactEntityRequest () {

            base.userInteractionType = Mercury.Server.Workflows.UserInteractions.Enumerations.UserInteractionType.ContactEntity;

            return;
        
        }

        //public ContactEntityRequest (Server.Core.Enumerations.EntityType forEntityType) {

        //    base.userInteractionType = Mercury.Server.Workflows.UserInteractions.Enumerations.UserInteractionType.ContactEntity;

        //    entityType = forEntityType;

        //    return;

        //}

        //public ContactEntityRequest (Server.Core.Enumerations.EntityType forEntityType, Int64 forEntityId, String regardingMessage, String forIntroductionScript) {

        //    base.userInteractionType = Mercury.Server.Workflows.UserInteractions.Enumerations.UserInteractionType.ContactEntity;

        //    EntityType = forEntityType;

        //    EntityId = forEntityId;

        //    Regarding = regardingMessage;

        //    IntroductionScript = forIntroductionScript;


        //    base.Message = "Contact " + entityType.ToString () + " regarding: " + regardingMessage;

        //    return;

        //}

        public ContactEntityRequest (Server.Core.Entity.Entity forEntity, String regardingMessage, String forIntroductionScript) {

            base.userInteractionType = Mercury.Server.Workflows.UserInteractions.Enumerations.UserInteractionType.ContactEntity;


            //EntityType = (forEntity != null) ? forEntity.EntityType : Mercury.Server.Core.Enumerations.EntityType.NotSpecified;

            //EntityId = (forEntity != null) ? forEntity.Id : 0;

            entity = forEntity;

            entityContactInformations = (forEntity != null) ? forEntity.ContactInformations : null; 

            Regarding = regardingMessage;

            IntroductionScript = forIntroductionScript;


            base.Message = "Contact " + entity.EntityType.ToString () + " regarding: " + regardingMessage;


            return;

        }

        public ContactEntityRequest (Server.Core.Entity.Entity forEntity, Server.Core.Entity.Entity forRelatedEntity, String regardingMessage, String forIntroductionScript) {

            base.userInteractionType = Mercury.Server.Workflows.UserInteractions.Enumerations.UserInteractionType.ContactEntity;


            entity = forEntity;

            relatedEntity = forRelatedEntity;

            entityContactInformations = (forEntity != null) ? forEntity.ContactInformations : null;

            Regarding = regardingMessage;

            IntroductionScript = forIntroductionScript;


            base.Message = "Contact " + entity.EntityType.ToString () + " regarding: " + regardingMessage;


            return;

        }

        #endregion

    }

}
