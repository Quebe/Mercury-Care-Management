using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Entity {

    [Serializable]
    public class EntityCorrespondence : CoreExtensibleObject {

        #region Private Properties

        private Int64 entityId;


        private Int64 correspondenceId;

        private String correspondenceName;

        private Double correspondenceVersion = 1.0;


        private Int64 entityFormId = 0;

        private Int64 relatedEntityId = 0;

        private String relatedObjectType = String.Empty;

        private Int64 relatedObjectId = 0;


        private DateTime readyToSendDate;

        private DateTime? sentDate;

        private DateTime? receivedDate;

        private DateTime? returnedDate;


        private Server.Application.EntityContactType contactType = Server.Application.EntityContactType.ByMail;

        private Int64 entityAddressId = 0;

        private Int64 entityContactInformationId = 0;


        private String attention = String.Empty;

        private String addressLine1 = String.Empty;

        private String addressLine2 = String.Empty;

        private String addressCity = String.Empty;

        private String addressState = String.Empty;

        private String addressZipCode = String.Empty;

        private String addressZipPlus4 = String.Empty;

        private String addressPostalCode = String.Empty;


        private String contactFaxNumber = String.Empty;

        private String contactEmail = String.Empty;

        private String remarks = String.Empty;


        private Guid automationId = Guid.Empty;

        private Server.Application.AutomationStatus automationStatus = Server.Application.AutomationStatus.NotSpecified;

        private DateTime? automationDate;

        private String automationException;


        private Boolean hasImage = false;

        #endregion


        #region Public Properties

        public Server.Application.EntityContactType ContactType { get { return contactType; } set { contactType = value; } }

        public Int64 EntityId { get { return entityId; } set { entityId = value; } }


        public Int64 CorrespondenceId { get { return correspondenceId; } set { correspondenceId = value; } }

        public String CorrespondenceName { get { return correspondenceName; } set { correspondenceName = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public Double CorrespondenceVersion { get { return correspondenceVersion; } set { correspondenceVersion = value; } }


        public Int64 EntityFormId { get { return entityFormId; } set { entityFormId = value; } }

        public Int64 RelatedEntityId { get { return relatedEntityId; } set { relatedEntityId = value; } }

        public String RelatedObjectType { get { return relatedObjectType; } set { relatedObjectType = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.ObjectType); } }

        public Int64 RelatedObjectId { get { return relatedObjectId; } set { relatedObjectId = value; } }


        public DateTime ReadyToSendDate { get { return readyToSendDate; } set { readyToSendDate = value; } }

        public DateTime? SentDate { get { return sentDate; } set { sentDate = value; } }

        public DateTime? ReceivedDate { get { return receivedDate; } set { receivedDate = value; } }

        public DateTime? ReturnedDate { get { return returnedDate; } set { returnedDate = value; } }


        public Int64 EntityAddressId { get { return entityAddressId; } set { entityAddressId = value; } }

        public Int64 EntityContactInformationId { get { return entityContactInformationId; } set { entityContactInformationId = value; } }

        public String Attention { get { return attention; } set { attention = value; } }

        public String AddressLine1 { get { return addressLine1; } set { addressLine1 = value; } }

        public String AddressLine2 { get { return addressLine2; } set { addressLine2 = value; } }

        public String AddressCity { get { return addressCity; } set { addressCity = value; } }

        public String AddressState { get { return addressState; } set { addressState = value; } }

        public String AddressZipCode { get { return addressZipCode; } set { addressZipCode = value; } }

        public String AddressZipPlus4 { get { return addressZipPlus4; } set { addressZipPlus4 = value; } }

        public String AddressPostalCode { get { return addressPostalCode; } set { addressPostalCode = value; } }


        public String ContactFaxNumber { get { return contactFaxNumber; } set { contactFaxNumber = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.ContactNumber); } }

        public String ContactFaxNumberFormatted {

            get {

                String numberFormatted = contactFaxNumber;

                numberFormatted = numberFormatted.Replace (" ", "");

                numberFormatted = numberFormatted.Replace ("(", "");

                numberFormatted = numberFormatted.Replace (")", "");

                numberFormatted = numberFormatted.Replace ("-", "");

                if (numberFormatted.Length < 10) { numberFormatted = "000" + numberFormatted; }

                String formatPattern = @"(\d{3})(\d{3})(\d{4})";

                numberFormatted = System.Text.RegularExpressions.Regex.Replace (numberFormatted, formatPattern, "($1) $2-$3");

                return numberFormatted;

            }

        }

        public String ContactEmail { get { return contactEmail; } set { contactEmail = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public String Remarks { get { return remarks; } set { remarks = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Description); } }


        public Guid AutomationId { get { return automationId; } set { automationId = value; } }

        public Server.Application.AutomationStatus AutomationStatus { get { return automationStatus; } set { automationStatus = value; } }

        public DateTime? AutomationDate { get { return automationDate; } set { automationDate = value; } }

        public String AutomationException { get { return automationException; } set { automationException = value; } }

        #endregion


        #region Public Properties

        public Entity RelatedEntity { get { return application.EntityGet (relatedEntityId, true); } }

        #endregion 


        #region Constructors

        public EntityCorrespondence (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public EntityCorrespondence (Application applicationReference, Server.Application.EntityCorrespondence serverEntityCorrespondence) {

            BaseConstructor (applicationReference, serverEntityCorrespondence);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.EntityCorrespondence serverEntityCorrespondence) {

            base.BaseConstructor (applicationReference, serverEntityCorrespondence);

            
            entityId = serverEntityCorrespondence.EntityId;
            

            correspondenceId = serverEntityCorrespondence.CorrespondenceId;

            correspondenceName = serverEntityCorrespondence.CorrespondenceName;

            correspondenceVersion = serverEntityCorrespondence.CorrespondenceVersion;


            entityFormId = serverEntityCorrespondence .EntityFormId;

            relatedEntityId = serverEntityCorrespondence.RelatedEntityId;

            relatedObjectType = serverEntityCorrespondence.RelatedObjectType;

            relatedObjectId = serverEntityCorrespondence.RelatedObjectId;


            readyToSendDate = serverEntityCorrespondence.ReadyToSendDate;

            sentDate = serverEntityCorrespondence.SentDate;

            receivedDate = serverEntityCorrespondence.ReceivedDate;

            returnedDate = serverEntityCorrespondence.ReturnedDate;


            contactType = serverEntityCorrespondence.ContactType;

            entityAddressId = serverEntityCorrespondence.EntityAddressId;

            entityContactInformationId = serverEntityCorrespondence.EntityContactInformationId;


            attention = serverEntityCorrespondence.Attention;

            addressLine1 = serverEntityCorrespondence.AddressLine1;

            addressLine2 = serverEntityCorrespondence.AddressLine2;

            addressCity = serverEntityCorrespondence.AddressCity;

            addressState = serverEntityCorrespondence.AddressState;

            addressZipCode = serverEntityCorrespondence.AddressZipCode;

            addressZipPlus4 = serverEntityCorrespondence.AddressZipPlus4;

            addressPostalCode = serverEntityCorrespondence.AddressPostalCode;


            contactFaxNumber = serverEntityCorrespondence.ContactFaxNumber;

            contactEmail = serverEntityCorrespondence.ContactEmail;

            remarks = serverEntityCorrespondence.Remarks;


            automationId = serverEntityCorrespondence.AutomationId;

            automationStatus = serverEntityCorrespondence.AutomationStatus;

            automationDate = serverEntityCorrespondence.AutomationDate;

            automationException = serverEntityCorrespondence.AutomationException;


            hasImage = serverEntityCorrespondence.HasImage;

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.EntityCorrespondence serverEntityCorrespondence) {

            base.MapToServerObject ((Server.Application.CoreExtensibleObject)serverEntityCorrespondence);


            serverEntityCorrespondence.EntityId = entityId;

            serverEntityCorrespondence.CorrespondenceId = correspondenceId;

            serverEntityCorrespondence.CorrespondenceName = correspondenceName;

            serverEntityCorrespondence.CorrespondenceVersion = correspondenceVersion;


            serverEntityCorrespondence.EntityFormId = entityFormId;

            serverEntityCorrespondence.RelatedEntityId = relatedEntityId;

            serverEntityCorrespondence.RelatedObjectType = relatedObjectType;

            serverEntityCorrespondence.RelatedObjectId = relatedObjectId;


            serverEntityCorrespondence.ReadyToSendDate = readyToSendDate;

            serverEntityCorrespondence.SentDate = sentDate;

            serverEntityCorrespondence.ReceivedDate = receivedDate;

            serverEntityCorrespondence.ReturnedDate = returnedDate;


            serverEntityCorrespondence.ContactType = contactType;

            serverEntityCorrespondence.EntityAddressId = entityAddressId;

            serverEntityCorrespondence.EntityContactInformationId = entityContactInformationId;


            serverEntityCorrespondence.Attention = attention;

            serverEntityCorrespondence.AddressLine1 = addressLine1;

            serverEntityCorrespondence.AddressLine2 = addressLine2;

            serverEntityCorrespondence.AddressCity = addressCity;

            serverEntityCorrespondence.AddressState = addressState;

            serverEntityCorrespondence.AddressZipCode = addressZipCode;

            serverEntityCorrespondence.AddressZipPlus4 = addressZipPlus4;

            serverEntityCorrespondence.AddressPostalCode = addressPostalCode;


            serverEntityCorrespondence.ContactFaxNumber = contactFaxNumber;

            serverEntityCorrespondence.ContactEmail = contactEmail;

            serverEntityCorrespondence.Remarks = remarks;


            serverEntityCorrespondence.AutomationId = automationId;

            serverEntityCorrespondence.AutomationStatus = automationStatus;

            serverEntityCorrespondence.AutomationDate = automationDate;

            serverEntityCorrespondence.AutomationException = automationException;


            serverEntityCorrespondence.HasImage = hasImage;

            return;

        }

        public override Object ToServerObject () {

            Server.Application.EntityCorrespondence serverEntityCorrespondence = new Server.Application.EntityCorrespondence ();

            MapToServerObject (serverEntityCorrespondence);

            return serverEntityCorrespondence;

        }

        public EntityCorrespondence Copy () {

            Server.Application.EntityCorrespondence serverEntityCorrespondence = (Server.Application.EntityCorrespondence)ToServerObject ();

            EntityCorrespondence copiedEntityCorrespondence = new EntityCorrespondence (application, serverEntityCorrespondence);

            return copiedEntityCorrespondence;

        }

        public Boolean IsEqual (EntityCorrespondence compareEntityCorrespondence) {

            Boolean isEqual = base.IsEqual ((CoreExtensibleObject)compareEntityCorrespondence);


            // TODO: UPDATE V2


            return isEqual;

        }

        #endregion 

    }

}
