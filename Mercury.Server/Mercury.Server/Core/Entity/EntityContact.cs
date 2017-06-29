using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Entity {

    [Serializable]
    [DataContract (Name = "EntityContact")]
    public class EntityContact : CoreExtensibleObject {

        #region Private Properties

        [DataMember (Name = "EntityId")]
        private Int64 entityId;

        [DataMember (Name = "EntityContactInformationId")]
        private Int64 entityContactInformationId;


        [DataMember (Name = "RelatedEntityId")]
        private Int64 relatedEntityId = 0;

        [DataMember (Name = "RelatedObjectType")]
        private String relatedObjectType = String.Empty;

        [DataMember (Name = "RelatedObjectId")]
        private Int64 relatedObjectId = 0;

        [DataMember (Name = "ScriptEntityFormId")]
        private Int64 scriptEntityFormId = 0;


        [DataMember (Name = "DataSource")]
        private String dataSource = String.Empty;

        [DataMember (Name = "ContactDate")]
        private DateTime contactDate = DateTime.Now;

        [DataMember (Name = "Direction")]
        private Enumerations.ContactDirection direction = Mercury.Server.Core.Enumerations.ContactDirection.Outbound;

        [DataMember (Name = "ContactType")]
        private Enumerations.EntityContactType contactType = Mercury.Server.Core.Enumerations.EntityContactType.NotSpecified;

        [DataMember (Name = "Successful")]
        private Boolean successful = false;

        [DataMember (Name = "ContactOutcome")]
        private Enumerations.ContactOutcome contactOutcome = Mercury.Server.Core.Enumerations.ContactOutcome.NotSpecified;


        [DataMember (Name = "ContactRegardingId")]
        private Int64 contactRegardingId = 0;

        [DataMember (Name = "Regarding")]
        private String regarding = String.Empty;

        [DataMember (Name = "Remarks")]
        private String remarks = String.Empty;

        [DataMember (Name = "ContactedByName")]
        private String contactedByName = String.Empty;

        [NonSerialized]
        private EntityContactInformation entityContactInformation = null;

        #endregion


        #region Public Properties

        public Int64 EntityId { get { return entityId; } set { entityId = value; } }

        public Int64 EntityContactInformationId { get { return entityContactInformationId; } set { entityContactInformationId = value; entityContactInformation = null; } }


        public Int64 RelatedEntityId { get { return relatedEntityId; } set { relatedEntityId = value; } }

        public String RelatedObjectType { get { return relatedObjectType; } set { relatedObjectType = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.ObjectType); } }

        public Int64 RelatedObjectId { get { return relatedObjectId; } set { relatedObjectId = value; } }

        public Int64 ScriptEntityFormId { get { return scriptEntityFormId; } set { scriptEntityFormId = value; } }


        public String DataSource { get { return dataSource; } set { dataSource = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.DataSource); } }

        public DateTime ContactDate { get { return contactDate; } set { contactDate = value; } }

        public Enumerations.ContactDirection Direction { get { return direction; } set { direction = value; } }

        public Enumerations.EntityContactType ContactType { get { return contactType; } set { contactType = value; } }

        public Boolean Successful { get { return successful; } set { successful = value; } }

        public Enumerations.ContactOutcome ContactOutcome { get { return contactOutcome; } set { contactOutcome = value; } }


        public Int64 ContactRegardingId {

            get { return contactRegardingId; }

            set {

                contactRegardingId = value;

                if (contactRegardingId != 0) {

                    Regarding = Application.ContactRegardingGetNameById (contactRegardingId);

                }

            }

        }

        public String Regarding { get { return regarding; } set { regarding = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public String Remarks { get { return remarks; } set { remarks = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Description); } }

        public String ContactedByName { get { return contactedByName; } set { contactedByName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }


        public EntityContactInformation EntityContactInformation {

            get {

                if (application == null) { return null; }

                if (entityContactInformation != null) { return entityContactInformation; }

                entityContactInformation = application.EntityContactInformationGet (entityContactInformationId);

                return entityContactInformation;

            }

        }

        #endregion


        #region Constructors

        public EntityContact (Application applicationReference) {

            base.BaseConstructor (applicationReference);

            return;

        }

        public EntityContact (Application applicationReference, Int64 forEntityId, String regardingMessage) {

            base.BaseConstructor (applicationReference);

            entityId = forEntityId;

            contactDate = DateTime.Now;

            Regarding = regardingMessage;

            return;

        }

        public EntityContact (Application applicationReference, Int64 forEntityContactId) {

            BaseConstructor (applicationReference);

            if (!Load (forEntityContactId)) {

                throw new ApplicationException ("Unable to load Entity Contact from the database for " + forEntityContactId.ToString () + ".");

            }

            return;

        }

        #endregion


        #region Database Functions

        override public Boolean Load (Int64 forId) {

            Boolean success = false;

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable tableEntityContact;

            if (base.application.EnvironmentDatabase == null) { return false; }

            selectStatement.Append ("EXEC dal.EntityContact_Select " + forId.ToString ());


            tableEntityContact = base.application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            if (tableEntityContact.Rows.Count == 1) {

                MapDataFields (tableEntityContact.Rows[0]);

                success = true;

            }

            return success;

        }

        override public void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            entityId  = (Int64) currentRow["EntityId"];

            entityContactInformationId = base.IdFromSql (currentRow, "EntityContactInformationId");


            relatedEntityId = base.IdFromSql (currentRow, "RelatedEntityId");

            relatedObjectType = (String) currentRow["RelatedObjectType"];

            relatedObjectId = (Int64) currentRow["RelatedObjectId"];

            scriptEntityFormId = base.IdFromSql (currentRow, "ScriptEntityFormId");


            dataSource = (String) currentRow["DataSource"];

            contactDate = (DateTime) currentRow["ContactDate"];

            direction = (Mercury.Server.Core.Enumerations.ContactDirection) (Int32) currentRow["ContactDirection"];

            contactType = (Mercury.Server.Core.Enumerations.EntityContactType) (Int32) currentRow["ContactType"];

            successful = (Boolean) currentRow["Successful"];

            if (currentRow.Table.Columns.Contains ("ContactOutcome")) {

                contactOutcome = (Mercury.Server.Core.Enumerations.ContactOutcome)(Int32)currentRow["ContactOutcome"];

            }

            else if (currentRow.Table.Columns.Contains ("ContactOutcomeId")) {

                contactOutcome = (Mercury.Server.Core.Enumerations.ContactOutcome)(Int32)currentRow["ContactOutcomeId"];

            }


            contactRegardingId = base.IdFromSql (currentRow, "ContactRegardingId");

            regarding = (String) currentRow["Regarding"];

            remarks = (String) currentRow["Remarks"];

            contactedByName = (String) currentRow["ContactedByName"];

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();



            ModifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application);

            try {

                base.application.EnvironmentDatabase.BeginTransaction ();

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dal.EntityContact_InsertUpdate ");


                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append (entityId.ToString () + ", ");

                sqlStatement.Append (entityContactInformationId.ToString () + ", ");


                sqlStatement.Append (base.IdSqlAllowNull (relatedEntityId) + ", ");

                sqlStatement.Append ("'" + relatedObjectType.Replace ("'", "''") + "', ");

                sqlStatement.Append (relatedObjectId.ToString () + ", ");

                // sqlStatement.Append (base.IdSqlAllowNull (scriptEntityFormId) + ", ");

                sqlStatement.Append (scriptEntityFormId.ToString () + ", ");


                sqlStatement.Append ("'" + contactDate.ToString () + "', ");

                sqlStatement.Append (((Int32) direction).ToString () + ", ");

                sqlStatement.Append (((Int32) contactType).ToString () + ", ");

                sqlStatement.Append ((Convert.ToInt32 (successful)).ToString () + ", ");

                sqlStatement.Append (((Int32) contactOutcome).ToString () + ", ");


                sqlStatement.Append (base.IdSqlAllowNull (contactRegardingId) + ", ");

                sqlStatement.Append ("'" + regarding.Replace ("'", "''") + "', ");

                sqlStatement.Append ("'" + remarks.Replace ("'", "''") + "', ");

                sqlStatement.Append ("'" + contactedByName.Replace ("'", "''") + "', ");


                sqlStatement.Append ("'" + ExtendedPropertiesSql + "', ");

                sqlStatement.Append ("'" + modifiedAccountInfo.SecurityAuthorityNameSql + "', '" + modifiedAccountInfo.UserAccountIdSql + "', '" + modifiedAccountInfo.UserAccountNameSql + "'");


                success = base.application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                if (!success) {

                    base.application.SetLastException (base.application.EnvironmentDatabase.LastException);

                    throw base.application.EnvironmentDatabase.LastException;

                }

                SetIdentity ();

                success = true;

                base.application.EnvironmentDatabase.CommitTransaction ();

            }

            catch (Exception applicationException) {

                success = false;

                base.application.EnvironmentDatabase.RollbackTransaction ();

                base.application.SetLastException (applicationException);

            }

            return success;

        }

        #endregion


        #region Public Methods

        public EntityContact Clone () {

            EntityContact clone = new EntityContact (application);


            // CLONE ANY CORE OBJECT PROPERTIES 


            // CLONE STANDARD PROPERTIES

            clone.EntityId = entityId;

            clone.EntityContactInformationId = entityContactInformationId;


            clone.RelatedEntityId = relatedEntityId;

            clone.RelatedObjectType = relatedObjectType;

            clone.RelatedObjectId = relatedObjectId;

            clone.ScriptEntityFormId = scriptEntityFormId;


            clone.DataSource = dataSource;

            clone.ContactDate = contactDate;

            clone.Direction = direction;

            clone.ContactType = contactType;

            clone.Successful = successful;

            clone.ContactOutcome = contactOutcome;


            clone.ContactRegardingId = contactRegardingId;

            clone.Regarding = regarding;

            clone.Remarks = remarks;

            clone.ContactedByName = contactedByName;


            return clone;

        }

        #endregion 

    }

}
