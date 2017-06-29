using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Workflows.UserInteractions.Request {

    [Serializable]
    [DataContract (Name = "WorkflowUserInteractionRequestSendCorrespondence")]
    public class SendCorrespondenceRequest : RequestBase {

        #region Private Properties

        //[DataMember (Name = "EntityType")]
        //Server.Core.Enumerations.EntityType entityType = Mercury.Server.Core.Enumerations.EntityType.NotSpecified;

        //[DataMember (Name = "EntityId")]
        //Int64 entityId = 0;


        [DataMember (Name = "Entity")]
        Server.Core.Entity.Entity entity = null;

        [DataMember (Name = "RelatedEntity")]
        Server.Core.Entity.Entity relatedEntity = null;


        [DataMember (Name = "CorrespondenceId")]
        private Int64 correspondenceId = 0;

        [DataMember (Name = "Attention")]
        private String attention = String.Empty;


        [DataMember (Name = "AllowEditRelatedEntity")]
        private Boolean allowEditRelatedEntity = false;

        [DataMember (Name = "AllowUserSelection")]
        private Boolean allowUserSelection = false;

        [DataMember (Name = "AllowAlternateAddress")]
        private Boolean allowAlternateAddress = true;

        [DataMember (Name = "AllowHistoricalSendDate")]
        private Boolean allowHistoricalSendDate = false;

        [DataMember (Name = "AllowFutureSendDate")]
        private Boolean allowFutureSendDate = false;

        [DataMember (Name = "AllowSendByFacsimile")]
        private Boolean allowSendByFacsimile = false;

        [DataMember (Name = "AllowSendByEmail")]
        private Boolean allowSendByEmail = false;

        [DataMember (Name = "AllowSendByInPerson")]
        private Boolean allowSendByInPerson = false;
        

        [DataMember (Name = "SendDate")]
        private DateTime sendDate = DateTime.Today;

        [DataMember (Name = "AlternateAddress")]
        private Server.Core.Entity.EntityAddress alternateAddress = null;

        [DataMember (Name = "AlternateFaxNumber")]
        private String alternateFaxNumber = String.Empty;

        [DataMember (Name = "AlternateEmail")]
        private String alternateEmail = String.Empty;   

        #endregion


        #region Public Properties

        //public Server.Core.Enumerations.EntityType EntityType { get { return entityType; } set { entityType = value; } }

        //public Int64 EntityId { get { return entityId; } set { entityId = value; } }

        public Core.Entity.Entity Entity { get { return entity; } set { entity = value; } }

        public Core.Entity.Entity RelatedEntity { get { return relatedEntity; } set { relatedEntity = value; } }


        public Int64 CorrespondenceId { get { return correspondenceId; } set { correspondenceId = value; } }

        public String Attention { get { return attention; } set { attention = value; } }


        public Boolean AllowEditRelatedEntity { get { return allowEditRelatedEntity; } set { allowEditRelatedEntity = value; } }

        public Boolean AllowUserSelection { get { return allowUserSelection; } set { allowUserSelection = value; } }

        public Boolean AllowAlternateAddress { get { return allowAlternateAddress; } set { allowAlternateAddress = value; } }

        public Boolean AllowHistoricalSendDate { get { return allowHistoricalSendDate; } set { allowHistoricalSendDate = value; } }

        public Boolean AllowFutureSendDate { get { return allowFutureSendDate; } set { allowFutureSendDate = value; } }

        public Boolean AllowSendByFacsimile { get { return allowSendByFacsimile; } set { allowSendByFacsimile = value; } }

        public Boolean AllowSendByEmail { get { return allowSendByEmail; } set { allowSendByEmail = value; } }

        public Boolean AllowSendByInPerson { get { return allowSendByInPerson; } set { allowSendByInPerson = value; } }
        
        public DateTime SendDate { get { return sendDate; } set { sendDate = value; } }

        public Core.Entity.EntityAddress AlternateAddress { get { return alternateAddress; } set { alternateAddress = value; } }

        public String AlternateFaxNumber { get { return alternateFaxNumber; } set { alternateFaxNumber = value; } }

        public String AlternateEmail { get { return alternateEmail; } set { alternateEmail = value; } }

        #endregion


        #region Constructor

        public SendCorrespondenceRequest () {

            base.userInteractionType = Mercury.Server.Workflows.UserInteractions.Enumerations.UserInteractionType.SendCorrespondence;

            return;

        }

        public SendCorrespondenceRequest (Server.Core.Entity.Entity forEntity, Core.Entity.Entity forRelatedEntity, Int64 forCorrespondenceId, Boolean forAllowUserSelection) {

            base.userInteractionType = Mercury.Server.Workflows.UserInteractions.Enumerations.UserInteractionType.SendCorrespondence;

            entity = forEntity;

            relatedEntity = forRelatedEntity;

            correspondenceId = forCorrespondenceId;

            allowUserSelection = forAllowUserSelection;

            return;

        }

        public SendCorrespondenceRequest (Server.Core.Entity.Entity forEntity, Core.Entity.Entity forRelatedEntity, Int64 forCorrespondenceId, Boolean forAllowUserSelection, Boolean forAllowAlternateAddress) {

            base.userInteractionType = Mercury.Server.Workflows.UserInteractions.Enumerations.UserInteractionType.SendCorrespondence;

            entity = forEntity;

            relatedEntity = forRelatedEntity;

            correspondenceId = forCorrespondenceId;

            allowUserSelection = forAllowUserSelection;

            allowAlternateAddress = forAllowAlternateAddress;

            return;

        }

        public SendCorrespondenceRequest (Server.Core.Entity.Entity forEntity, Core.Entity.Entity forRelatedEntity, Int64 forCorrespondenceId, Boolean forAllowUserSelection, Boolean forAllowAlternateAddress, Boolean forAllowCancel) {

            base.userInteractionType = Mercury.Server.Workflows.UserInteractions.Enumerations.UserInteractionType.SendCorrespondence;

            entity = forEntity;

            relatedEntity = forRelatedEntity;

            correspondenceId = forCorrespondenceId;

            allowUserSelection = forAllowUserSelection;

            allowAlternateAddress = forAllowAlternateAddress;

            AllowCancel = forAllowCancel;

            return;

        }

        #endregion

    }

}
