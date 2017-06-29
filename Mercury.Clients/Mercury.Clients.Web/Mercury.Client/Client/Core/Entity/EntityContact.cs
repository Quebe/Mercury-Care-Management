using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Entity {

    [Serializable]
    public class EntityContact : CoreExtensibleObject {

        #region Private Properties

        private Int64 entityId;

        private Int64 entityContactInformationId;


        private Int64 relatedEntityId = 0;

        private String relatedObjectType = String.Empty;

        private Int64 relatedObjectId = 0;

        private Int64 scriptEntityFormId = 0;


        private String dataSource = String.Empty;

        private DateTime contactDate = DateTime.Now;

        private Server.Application.ContactDirection direction = Mercury.Server.Application.ContactDirection.Outbound;

        private Server.Application.EntityContactType contactType = Mercury.Server.Application.EntityContactType.NotSpecified;

        private Boolean successful = false;

        private Server.Application.ContactOutcome contactOutcome = Mercury.Server.Application.ContactOutcome.NotSpecified;


        private Int64 contactRegardingId = 0;

        private String regarding = String.Empty;

        private String remarks = String.Empty;

        private String contactedByName = String.Empty;

        #endregion


        #region Public Properties

        public Int64 EntityId { get { return entityId; } set { entityId = value; } }

        public Int64 EntityContactInformationId { get { return entityContactInformationId; } set { entityContactInformationId = value; } }


        public Int64 RelatedEntityId { get { return relatedEntityId; } set { relatedEntityId = value; } }

        public String RelatedObjectType { get { return relatedObjectType; } set { relatedObjectType = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.ObjectType); } }

        public Int64 RelatedObjectId { get { return relatedObjectId; } set { relatedObjectId = value; } }

        public Int64 ScriptEntityFormId { get { return scriptEntityFormId; } set { scriptEntityFormId = value; } }


        public String DataSource { get { return dataSource; } set { dataSource = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataSource); } }

        public DateTime ContactDate { get { return contactDate; } set { contactDate = value; } }

        public Server.Application.ContactDirection Direction { get { return direction; } set { direction = value; } }

        public Server.Application.EntityContactType ContactType { get { return contactType; } set { contactType = value; } }

        public Boolean Successful { get { return successful; } set { successful = value; } }

        public Server.Application.ContactOutcome ContactOutcome { get { return contactOutcome; } set { contactOutcome = value; } }


        public Int64 ContactRegardingId {

            get { return contactRegardingId; }

            set {

                if (contactRegardingId != value) {

                    contactRegardingId = value;

                    Regarding = Application.CoreObjectGetNameById ("ContactRegarding", contactRegardingId);

                }

            }

        }

        public String Regarding { get { return regarding; } set { regarding = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public String Remarks { get { return remarks; } set { remarks = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Description); } }

        public String ContactedByName { get { return contactedByName; } set { contactedByName = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }


        //public EntityContactInformation EntityContactInformation {

        //    get {

        //        if (application == null) { return null; }

        //        if (entityContactInformation != null) { return entityContactInformation; }

        //        entityContactInformation = application.EntityContactInformationGet (entityContactInformationId);

        //        return entityContactInformation;

        //    }

        //}

        #endregion


        #region Public Properties

        public Entity RelatedEntity { get { return application.EntityGet (relatedEntityId, true); } }

        #endregion 


        #region Constructors

        public EntityContact (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public EntityContact (Application applicationReference, Server.Application.EntityContact serverEntityContact) {

            BaseConstructor (applicationReference, serverEntityContact);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.EntityContact serverEntityContact) {

            base.BaseConstructor (applicationReference, serverEntityContact);

            
            entityId = serverEntityContact.EntityId;

            entityContactInformationId = serverEntityContact.EntityContactInformationId;


            relatedEntityId = serverEntityContact.RelatedEntityId;
                        
            relatedObjectType = serverEntityContact.RelatedObjectType;

            relatedObjectId = serverEntityContact.RelatedObjectId;

            scriptEntityFormId = serverEntityContact.ScriptEntityFormId;


            dataSource = serverEntityContact.DataSource;
            
            contactDate = serverEntityContact.ContactDate;

            direction = serverEntityContact.Direction;

            contactType = serverEntityContact.ContactType;

            successful = serverEntityContact.Successful;

            contactOutcome = serverEntityContact.ContactOutcome;


            contactRegardingId = serverEntityContact.ContactRegardingId;

            regarding = serverEntityContact.Regarding;

            remarks = serverEntityContact.Remarks;

            contactedByName = serverEntityContact.ContactedByName;

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.EntityContact serverObject) {

            base.MapToServerObject ((Server.Application.CoreObject)serverObject);


            serverObject.EntityId = entityId;

            serverObject.EntityContactInformationId = entityContactInformationId;


            serverObject.RelatedEntityId = relatedEntityId;

            serverObject.RelatedObjectType = relatedObjectType;

            serverObject.RelatedObjectId = relatedObjectId;

            serverObject.ScriptEntityFormId = scriptEntityFormId;


            serverObject.DataSource = dataSource;

            serverObject.ContactDate = contactDate;

            serverObject.Direction = direction;

            serverObject.ContactType = contactType;

            serverObject.Successful = successful;

            serverObject.ContactOutcome = ContactOutcome;


            serverObject.ContactRegardingId = contactRegardingId;

            serverObject.Regarding = regarding;

            serverObject.Remarks = remarks;

            serverObject.ContactedByName = contactedByName;

            
            return;

        }

        public override Object ToServerObject () {

            Server.Application.EntityContact serverObject = new Server.Application.EntityContact ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public EntityContact Copy () {

            Server.Application.EntityContact serverObject = (Server.Application.EntityContact)ToServerObject ();

            EntityContact copiedObject = new EntityContact (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (EntityContact compareObject) {

            Boolean isEqual = base.IsEqual ((CoreObject)compareObject);


            isEqual &= (EntityId == compareObject.EntityId);

            isEqual &= (EntityContactInformationId == compareObject.EntityContactInformationId);


            isEqual &= (RelatedEntityId == compareObject.RelatedEntityId);

            isEqual &= (RelatedObjectType == compareObject.RelatedObjectType);

            isEqual &= (RelatedObjectId == compareObject.RelatedObjectId);

            isEqual &= (ScriptEntityFormId == compareObject.ScriptEntityFormId);


            isEqual &= (DataSource == compareObject.DataSource);

            isEqual &= (ContactDate == compareObject.ContactDate);


            isEqual &= (Direction == compareObject.Direction);

            isEqual &= (ContactType == compareObject.ContactType);


            isEqual &= (Successful == compareObject.Successful);

            isEqual &= (ContactOutcome == compareObject.ContactOutcome);


            isEqual &= (ContactRegardingId == compareObject.ContactRegardingId);

            isEqual &= (Regarding == compareObject.Regarding);


            isEqual &= (Remarks == compareObject.Remarks);

            isEqual &= (ContactedByName == compareObject.ContactedByName);

            return isEqual;

        }

        #endregion 

    }

}

