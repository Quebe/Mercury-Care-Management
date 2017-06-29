using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Entity.Views{

    [DataContract (Name = "EntityDocumentDataView")]
    public class EntityDocument {

        #region Private Properties

        [DataMember (Name = "DocumentType")]
        private String documentType;

        [DataMember (Name = "EntityDocumentId")]
        private Int64 entityDocumentId;

        [DataMember (Name = "DocumentId")]
        private Int64 documentId;

        [DataMember (Name = "Name")]
        private String documentName;

        [DataMember (Name = "EntityFormId")]
        private Int64 entityFormId;

        [DataMember (Name = "EntityId")]
        private Int64 entityId;


        [DataMember (Name = "Version")]
        private Double version = 1.0;

        [DataMember (Name = "ContactType")]
        private Core.Enumerations.EntityContactType contactType = Server.Core.Enumerations.EntityContactType.NotSpecified;


        [DataMember (Name = "ReadyToSendDate")]
        private DateTime? readyToSendDate;

        [DataMember (Name = "SentDate")]
        private DateTime? sentDate;

        [DataMember (Name = "ReceivedDate")]
        private DateTime? receivedDate;

        [DataMember (Name = "ReturnedDate")]
        private DateTime? returnedDate;

        [DataMember (Name = "HasImage")]
        private Boolean hasImage = false;


        [DataMember (Name = "CreateAccountInfo")]
        private Server.Data.AuthorityAccountStamp createAccountInfo = new Server.Data.AuthorityAccountStamp ();

        [DataMember (Name = "ModifiedAccountInfo")]
        private Server.Data.AuthorityAccountStamp modifiedAccountInfo = new Server.Data.AuthorityAccountStamp ();

        #endregion


        #region Public Properties

        public String DocumentType { get { return documentType; } set { documentType = value; } }

        public Int64 EntityDocumentId { get { return entityDocumentId; } set { entityDocumentId = value; } }

        public Int64 DocumentId { get { return documentId; } set { documentId = value; } }

        public String Name { get { return documentName; } set { documentName = value; } }

        public Int64 EntityFormId { get { return entityFormId; } set { entityFormId = value; } }

        public Int64 EntityId { get { return entityId; } set { entityId = value; } }


        public Double Version { get { return version; } set { version = value; } }

        public Core.Enumerations.EntityContactType ContactType { get { return contactType; } set { contactType = value; } }


        public DateTime? ReadyToSendDate { get { return readyToSendDate; } set { readyToSendDate = value; } }

        public DateTime? SentDate { get { return sentDate; } set { sentDate = value; } }

        public DateTime? ReceivedDate { get { return receivedDate; } set { receivedDate = value; } }

        public DateTime? ReturnedDate { get { return returnedDate; } set { returnedDate = value; } }


        public Server.Data.AuthorityAccountStamp CreateAccountInfo { get { return createAccountInfo; } set { createAccountInfo = value; } }

        public Server.Data.AuthorityAccountStamp ModifiedAccountInfo { get { return modifiedAccountInfo; } set { modifiedAccountInfo = value; } }

        #endregion


        #region Data Functions

        public void MapDataFields (System.Data.DataRow currentRow) {

            documentType = (String) currentRow["DocumentType"];

            entityDocumentId = (Int64) currentRow["EntityDocumentId"];

            documentId = (Int64) currentRow["DocumentId"];

            documentName = (String) currentRow["DocumentName"];

            entityFormId = (currentRow["EntityFormId"] is System.DBNull) ? 0 : entityFormId = Convert.ToInt64 (currentRow["EntityFormId"]);

            entityId = (Int64) currentRow["EntityId"];


            version = Convert.ToDouble (currentRow["Version"]);

            contactType = (Server.Core.Enumerations.EntityContactType) (Int32) currentRow["ContactType"];


            readyToSendDate = (currentRow["ReadyToSendDate"] is System.DBNull) ? null : readyToSendDate = Convert.ToDateTime (currentRow["ReadyToSendDate"]);

            sentDate = (currentRow["SentDate"] is System.DBNull) ? null : sentDate = Convert.ToDateTime (currentRow["SentDate"]);

            receivedDate = (currentRow["ReceivedDate"] is System.DBNull) ? null : receivedDate = Convert.ToDateTime (currentRow["ReceivedDate"]);

            returnedDate = (currentRow["ReturnedDate"] is System.DBNull) ? null : returnedDate = Convert.ToDateTime (currentRow["ReturnedDate"]);


            hasImage = Convert.ToBoolean (currentRow["HasImage"]);

            createAccountInfo.MapDataFields (currentRow, "Create");

            modifiedAccountInfo.MapDataFields (currentRow, "Modified");


            return;

        }

        #endregion

    }


}
