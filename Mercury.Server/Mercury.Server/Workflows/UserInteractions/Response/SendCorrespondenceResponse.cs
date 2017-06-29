using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Workflows.UserInteractions.Response {

    [Serializable]
    [DataContract (Name = "WorkflowUserInteractionResponseSendCorrespondence")]
    public class SendCorrespondenceResponse : ResponseBase {

        #region Private Properties

        //[DataMember (Name = "EntityType")]
        //private Server.Core.Enumerations.EntityType entityType = Mercury.Server.Core.Enumerations.EntityType.NotSpecified;

        //[DataMember (Name = "EntityId")]
        //private Int64 entityId = 0;


        //[DataMember (Name = "ContactType")]
        //private Core.Enumerations.EntityContactType contactType = Mercury.Server.Core.Enumerations.EntityContactType.ByMail;

        //[DataMember (Name = "EntityAddressId")]
        //private Int64 entityAddressId = 0;

        //[DataMember (Name = "EntityContactInformationId")]
        //private Int64 entityContactInformationId = 0;

        //[DataMember (Name = "CorrespondenceId")]
        //private Int64 correspondenceId = 0;

        //[DataMember (Name = "Attention")]
        //private String attention = String.Empty;


        //[DataMember (Name = "UseAlternateAddress")]
        //private Boolean useAlternateAddress = false;

        //[DataMember (Name = "AlternateAddressLine1")]
        //private String alternateAddressLine1 = String.Empty;

        //[DataMember (Name = "AlternateAddressLine2")]
        //private String alternateAddressLine2 = String.Empty;

        //[DataMember (Name = "AlternateAddressCity")]
        //private String alternateAddressCity = String.Empty;

        //[DataMember (Name = "AlternateAddressState")]
        //private String alternateAddressState = String.Empty;

        //[DataMember (Name = "AlternateAddressZipCode")]
        //private String alternateAddressZipCode = String.Empty;


        //[DataMember (Name = "UseAlternateContactFaxNumber")]
        //private Boolean useAlternateContactFaxNumber = false;

        //[DataMember (Name = "AlternateContactFaxNumber")]
        //private String alternateContactFaxNumber = String.Empty;


        //[DataMember (Name = "UseAlternateContactEmail")]
        //private Boolean useAlternateContactEmail = false;

        //[DataMember (Name = "AlternateContactEmail")]
        //private String alternateContactEmail = String.Empty;


        //[DataMember (Name = "SendDate")]
        //private DateTime sendDate = DateTime.Now;


        [DataMember (Name = "EntityCorrespondence")]
        private Core.Entity.EntityCorrespondence entityCorrespondence = null;

        [DataMember (Name = "Send")]
        private Boolean send = true;

        #endregion


        #region Public Properties

        //public Server.Core.Enumerations.EntityType EntityType { get { return entityType; } set { entityType = value; } }

        //public Int64 EntityId { get { return entityId; } set { entityId = value; } }


        //public Core.Enumerations.EntityContactType ContactType { get { return contactType; } set { contactType = value; } }

        //public Int64 EntityAddressId { get { return entityAddressId; } set { entityAddressId = value; } }

        //public Int64 EntityContactInformationId { get { return entityContactInformationId; } set { entityContactInformationId = value; } }


        //public Int64 CorrespondenceId { get { return correspondenceId; } set { correspondenceId = value; } }

        //public String Attention { get { return attention; } set { attention = value; } }


        //public Boolean UseAlternateAddress { get { return useAlternateAddress; } set { useAlternateAddress = value; } }

        //public String AlternateAddressLine1 { get { return alternateAddressLine1; } set { alternateAddressLine1 = value; } }

        //public String AlternateAddressLine2 { get { return alternateAddressLine2; } set { alternateAddressLine2 = value; } }

        //public String AlternateAddressCity { get { return alternateAddressCity; } set { alternateAddressCity = value; } }

        //public String AlternateAddressState { get { return alternateAddressState; } set { alternateAddressState = value; } }

        //public String AlternateAddressZipCode { get { return alternateAddressZipCode; } set { alternateAddressZipCode = value; } }


        //public Boolean UseAlternateContactFaxNumber { get { return useAlternateContactFaxNumber; } set { useAlternateContactFaxNumber = value; } }

        //public String AlternateContactFaxNumber { get { return alternateContactFaxNumber; } set { alternateContactFaxNumber = value; } }


        //public Boolean UseAlternateContactEmail { get { return useAlternateContactEmail; } set { useAlternateContactEmail = value; } }

        //public String AlternateContactEmail { get { return alternateContactEmail; } set { alternateContactEmail = value; } }


        //public DateTime SendDate { get { return sendDate; } set { sendDate = value; } }


        public Core.Entity.EntityCorrespondence EntityCorrespondence { get { return entityCorrespondence; } set { entityCorrespondence = value; } }

        public Boolean Send { get { return send; } set { send = value; } }

        #endregion


        #region Constructor

        public SendCorrespondenceResponse () {

            base.userInteractionType = Mercury.Server.Workflows.UserInteractions.Enumerations.UserInteractionType.SendCorrespondence;

            return;

        }

        #endregion

    }

}
