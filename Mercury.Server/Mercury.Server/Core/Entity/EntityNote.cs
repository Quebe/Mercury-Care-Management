using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Entity {


    [Serializable]
    [DataContract (Name = "EntityNote")]
    public class EntityNote : CoreObject {

        #region Private Properties

        [DataMember (Name = "EntityId")]
        private Int64 entityId;


        [DataMember (Name = "RelatedEntityId")]
        private Int64 relatedEntityId = 0;


        [DataMember (Name = "RelatedEntityType")]
        private Enumerations.EntityType relatedEntityType = Mercury.Server.Core.Enumerations.EntityType.NotSpecified;

        [DataMember (Name = "RelatedEntityObjectId")]
        private Int64 relatedEntityObjectId = 0;


        [DataMember (Name = "RelatedObjectType")]
        private String relatedObjectType = String.Empty;

        [DataMember (Name = "RelatedObjectId")]
        private Int64 relatedObjectId = 0;


        [DataMember (Name = "DataSource")]
        private String dataSource = String.Empty;

        [DataMember (Name = "Importance")]
        private Enumerations.NoteImportance importance = Mercury.Server.Core.Enumerations.NoteImportance.NotSpecified;

        [DataMember (Name = "NoteTypeId")]
        private Int64 noteTypeId = 0;

        [DataMember (Name = "Subject")]
        private String subject = String.Empty;

        [DataMember (Name = "EffectiveDate")]
        private DateTime effectiveDate = DateTime.Now;

        [DataMember (Name = "TerminationDate")]
        private DateTime terminationDate = new DateTime (9999, 1, 1);


        [DataMember (Name = "Contents")]
        private List<EntityNoteContent> contents = new List<EntityNoteContent> ();


        private Entity entity = null;

        #endregion


        #region Public Properties

        public override String Name { get { return subject; } }


        public Int64 EntityId { get { return entityId; } set { entityId = value; } }

        public Int64 RelatedEntityId { get { return relatedEntityId; } set { relatedEntityId = value; } }

        public Enumerations.EntityType RelatedEntityType { get { return relatedEntityType; } set { relatedEntityType = value; } }

        public Int64 RelatedEntityObjectId { get { return relatedEntityObjectId; } set { relatedEntityObjectId = value; } }

        public String RelatedObjectType { get { return relatedObjectType; } set { relatedObjectType = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.ObjectType); } }

        public Int64 RelatedObjectId { get { return relatedObjectId; } set { relatedObjectId = value; } }

        public String DataSource { get { return dataSource; } set { dataSource = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.DataSource); } }

        public Enumerations.NoteImportance Importance { get { return importance; } set { importance = value; } }

        public Int64 NoteTypeId { get { return noteTypeId; } set { noteTypeId = value; } }

        public String Subject { get { return subject; } set { subject = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Subject); } }

        public DateTime EffectiveDate { 
            
            get { return effectiveDate; }

            set { if (value <= terminationDate) { effectiveDate = value; } }
        
        }

        public DateTime TerminationDate { 
            
            get { return terminationDate; }

            set { if (value >= effectiveDate) { terminationDate = value; } }
        
        }

        public List<EntityNoteContent> Contents { get { return contents; } set { contents = value; } }


        public Entity Entity {

            get {

                if (entity != null) { return entity; }

                if (base.application == null) { return new Entity (base.application); }

                entity = base.application.EntityGet (entityId);

                return entity;

            }

        }

        public override Application Application {

            set {

                base.Application = value;

                if (contents == null) { contents = new List<EntityNoteContent> (); }

                foreach (EntityNoteContent currentContent in contents) {

                    currentContent.Application = value;

                }

            }

        }

        #endregion
        

        #region Constructors

        public EntityNote (Application applicationReference) {

            BaseConstructor (applicationReference);

            return; 
        
        }

        public EntityNote (Application applicationReference, Int64 forEntityNoteId) {

            BaseConstructor (applicationReference, forEntityNoteId);

            return;

        }

        #endregion


        #region Data Functions

        public override Boolean Load (long forId) { return LoadFromDalSp (forId); }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            entityId = (Int64) currentRow["EntityId"];


            relatedEntityId = (Int64)currentRow["RelatedEntityId"];

            relatedEntityType = (Mercury.Server.Core.Enumerations.EntityType) ((Int32) currentRow ["RelatedEntityType"]);

            relatedEntityObjectId = (Int64) currentRow ["RelatedEntityObjectId"];

            relatedObjectType = (String) currentRow ["RelatedObjectType"];

            relatedObjectId = (Int64) currentRow ["RelatedObjectId"];


            dataSource = (String) currentRow ["DataSource"];

            importance = (Mercury.Server.Core.Enumerations.NoteImportance) ((Int32) currentRow ["Importance"]);

            noteTypeId = (Int64) currentRow["NoteTypeId"];

            subject = (String) currentRow ["Subject"];

            
            effectiveDate = (DateTime) currentRow ["EffectiveDate"];

            terminationDate = (DateTime) currentRow ["TerminationDate"];


            if (application != null) {

                contents = application.EntityNoteContentsGet (Id);

            }

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();


            ModifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application);

            try {

                base.application.EnvironmentDatabase.BeginTransaction ();

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dal.EntityNote_InsertUpdate ");


                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append (entityId.ToString () + ", ");


                sqlStatement.Append (relatedEntityId.ToString () + ", ");

                sqlStatement.Append (((Int32) relatedEntityType).ToString () + ", ");

                sqlStatement.Append (relatedEntityObjectId.ToString () + ", ");

                sqlStatement.Append ("'" + relatedObjectType.Replace ("'", "''") + "', ");

                sqlStatement.Append (relatedObjectId.ToString () + ", ");


                sqlStatement.Append (((Int32) importance).ToString () + ", ");

                sqlStatement.Append (noteTypeId.ToString () + ", ");

                sqlStatement.Append ("'" + subject.Replace ("'", "''") + "', ");


                sqlStatement.Append ("'" + effectiveDate.ToString ("MM/dd/yyyy") + "', ");

                sqlStatement.Append ("'" + terminationDate.ToString ("MM/dd/yyyy") + "', ");


                sqlStatement.Append ("'" + modifiedAccountInfo.SecurityAuthorityNameSql + "', '" + modifiedAccountInfo.UserAccountIdSql + "', '" + modifiedAccountInfo.UserAccountNameSql + "'");


                success = base.application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                if (!success) {

                    base.application.SetLastException (base.application.EnvironmentDatabase.LastException);

                    throw base.application.EnvironmentDatabase.LastException;

                }

                SetIdentity ();

                if (contents == null) { contents = new List<EntityNoteContent> (); }

                foreach (EntityNoteContent currentContent in contents) {

                    currentContent.Application = application;

                    currentContent.EntityNoteId = Id;

                    success = currentContent.Save ();

                    if (!success) { throw new ApplicationException ("Unable to Save Entity Note Content."); }

                }

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


        #region Public Functions

        public Boolean Terminate (DateTime forTerminationDate) {

            Boolean success = false;
            
            StringBuilder sqlStatement = new StringBuilder ();



            if (forTerminationDate < EffectiveDate) {

                application.SetLastException (new ApplicationException ("Permission Denied. Termination Date cannot be set before Effective Date."));

                return false;

            }


            ModifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application);

            try {

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dal.EntityNote_Terminate ");

                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append ("'" + forTerminationDate.ToString ("MM/dd/yyyy") + "', ");

                sqlStatement.Append ("'" + modifiedAccountInfo.SecurityAuthorityNameSql + "', '" + modifiedAccountInfo.UserAccountIdSql + "', '" + modifiedAccountInfo.UserAccountNameSql + "'");

                success = base.application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                if (!success) {

                    throw base.application.EnvironmentDatabase.LastException;

                }

            }

            catch (Exception applicationException) {

                success = false;

                base.application.SetLastException (applicationException);

            }

            return success;

        }

        #endregion 

    }

}
