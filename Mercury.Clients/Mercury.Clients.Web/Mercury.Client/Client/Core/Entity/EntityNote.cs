using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Entity {

    [Serializable]
    public class EntityNote : CoreObject {

        #region Private Properties

        private Int64 entityId;


        private Int64 relatedEntityId = 0;

        private Server.Application.EntityType relatedEntityType = Server.Application.EntityType.NotSpecified;

        private Int64 relatedEntityObjectId = 0;

        private String relatedObjectType = String.Empty;

        private Int64 relatedObjectId = 0;


        private String dataSource = String.Empty;

        private Server.Application.NoteImportance importance = Server.Application.NoteImportance.NotSpecified;

        private Int64 noteTypeId = 0;

        private String subject = String.Empty;

        private DateTime effectiveDate = DateTime.Now;

        private DateTime terminationDate = new DateTime (9999, 1, 1);


        private List<Mercury.Server.Application.EntityNoteContent> contents = new List<Server.Application.EntityNoteContent> ();

        #endregion


        #region Public Properties

        public override String Name { get { return subject; } }


        public Int64 EntityId { get { return entityId; } set { entityId = value; } }

        public Int64 RelatedEntityId { get { return relatedEntityId; } set { relatedEntityId = value; } }

        public Server.Application.EntityType RelatedEntityType { get { return relatedEntityType; } set { relatedEntityType = value; } }

        public Int64 RelatedEntityObjectId { get { return relatedEntityObjectId; } set { relatedEntityObjectId = value; } }

        public String RelatedObjectType { get { return relatedObjectType; } set { relatedObjectType = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.ObjectType); } }

        public Int64 RelatedObjectId { get { return relatedObjectId; } set { relatedObjectId = value; } }

        public String DataSource { get { return dataSource; } set { dataSource = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataSource); } }

        public Server.Application.NoteImportance Importance { get { return importance; } set { importance = value; } }

        public Int64 NoteTypeId { get { return noteTypeId; } set { noteTypeId = value; } }

        public String Subject { get { return subject; } set { subject = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Subject); } }

        public DateTime EffectiveDate {

            get { return effectiveDate; }

            set { if (value <= terminationDate) { effectiveDate = value; } }

        }

        public DateTime TerminationDate {

            get { return terminationDate; }

            set { if (value >= effectiveDate) { terminationDate = value; } }

        }


        public List<Mercury.Server.Application.EntityNoteContent> Contents { get { return contents; } set { contents = value; } }

        #endregion 


        #region Public Reference Properties

        public Entity Entity { get { return application.EntityGet (entityId, true); } }

        public Reference.NoteType NoteType { get { return application.NoteTypeGet (noteTypeId, true); } }

        public String NoteTypeName { get { return ((NoteType == null) ? String.Empty : NoteType.Name); } }
        
        #endregion
        

        #region Constructors

        public EntityNote (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public EntityNote (Application applicationReference, Server.Application.EntityNote serverEntityNote) {

            BaseConstructor (applicationReference, serverEntityNote);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.EntityNote serverEntityNote) {

            base.BaseConstructor (applicationReference, serverEntityNote);

            
            entityId= serverEntityNote.EntityId;

            relatedEntityId = serverEntityNote.RelatedEntityId;

            relatedEntityType = serverEntityNote.RelatedEntityType;

            relatedEntityObjectId = serverEntityNote.RelatedEntityObjectId;
            
            relatedObjectType = serverEntityNote.RelatedObjectType;

            relatedObjectId = serverEntityNote.RelatedObjectId;


            dataSource = serverEntityNote.DataSource;

            importance = serverEntityNote.Importance;

            noteTypeId = serverEntityNote.NoteTypeId;

            subject = serverEntityNote.Subject;

            effectiveDate = serverEntityNote.EffectiveDate;

            terminationDate = serverEntityNote.TerminationDate;


            contents = new List<Mercury.Server.Application.EntityNoteContent> ();

            contents.AddRange (serverEntityNote.Contents);

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.EntityNote serverObject) {

            base.MapToServerObject ((Server.Application.CoreObject)serverObject);


            serverObject.EntityId = entityId;

            serverObject.RelatedEntityId = relatedEntityId;

            serverObject.RelatedEntityType = relatedEntityType;

            serverObject.RelatedEntityObjectId = relatedEntityObjectId;

            serverObject.RelatedObjectType = relatedObjectType;

            serverObject.RelatedObjectId = relatedObjectId;

            serverObject.DataSource = dataSource;

            serverObject.Importance = importance;

            serverObject.NoteTypeId = noteTypeId;

            serverObject.Subject = subject;

            serverObject.EffectiveDate = effectiveDate;

            serverObject.TerminationDate = terminationDate;


            serverObject.Contents = new Mercury.Server.Application.EntityNoteContent[contents.Count];

            contents.CopyTo (serverObject.Contents, 0);


            return;

        }

        public override Object ToServerObject () {

            Server.Application.EntityNote serverObject = new Server.Application.EntityNote ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public EntityNote Copy () {

            Server.Application.EntityNote serverObject = (Server.Application.EntityNote)ToServerObject ();

            EntityNote copiedObject = new EntityNote (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (EntityNote compareObject) {

            Boolean isEqual = base.IsEqual ((CoreObject)compareObject);


            isEqual &= (EntityId == compareObject.EntityId);

            isEqual &= (RelatedEntityId == compareObject.RelatedEntityId);


            isEqual &= (RelatedEntityType == compareObject.RelatedEntityType);

            isEqual &= (RelatedEntityObjectId == compareObject.RelatedEntityObjectId);


            isEqual &= (RelatedObjectType == compareObject.RelatedObjectType);

            isEqual &= (RelatedObjectId == compareObject.RelatedObjectId);


            isEqual &= (DataSource == compareObject.DataSource);

            isEqual &= (Importance == compareObject.Importance);


            isEqual &= (NoteTypeId == compareObject.NoteTypeId);

            isEqual &= (Subject == compareObject.Subject);


            isEqual &= (EffectiveDate == compareObject.EffectiveDate);

            isEqual &= (TerminationDate == compareObject.TerminationDate);


            return isEqual;

        }

        #endregion 

    }

}
