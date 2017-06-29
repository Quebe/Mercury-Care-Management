using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Entity {

    [Serializable]
    public class Entity : CoreObject {

        #region Private Properties

        private Server.Application.EntityType entityType = Server.Application.EntityType.NotSpecified;

        
        private String nameLast;

        private String nameFirst;

        private String nameMiddle;

        private String namePrefix;

        private String nameSuffix;


        private String federalTaxId;

        private String idCodeQualifier;

        private String uniqueId;

        #endregion


        #region Public Properties

        public Server.Application.EntityType EntityType { get { return entityType; } set { entityType = value; } }


        public String NameLast { get { return nameLast; } set { nameLast = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.NameLast); } }

        public String NameFirst { get { return nameFirst; } set { nameFirst = value.Substring (0, (value.Length > Server.Data.DataTypeConstants.NameFirst) ? Server.Data.DataTypeConstants.NameFirst : value.Length); } }

        public String NameMiddle { get { return nameMiddle; } set { nameMiddle = value.Substring (0, (value.Length > Server.Data.DataTypeConstants.NameMiddle) ? Server.Data.DataTypeConstants.NameMiddle : value.Length); } }

        public String NamePrefix {

            get { return namePrefix; }

            set { namePrefix = value.Substring (0, (value.Length > Server.Data.DataTypeConstants.NameSuffix) ? Server.Data.DataTypeConstants.NameSuffix : value.Length); }

        }

        public String NameSuffix {

            get { return nameSuffix; }

            set { nameSuffix = value.Substring (0, (value.Length > Server.Data.DataTypeConstants.NamePrefix) ? Server.Data.DataTypeConstants.NamePrefix : value.Length); }

        }

        public String FederalTaxId {

            get { return federalTaxId; }

            set { federalTaxId = value.Substring (0, (value.Length > Server.Data.DataTypeConstants.FederalTaxId) ? Server.Data.DataTypeConstants.FederalTaxId : value.Length); }

        }

        public String IdCodeQualifier { get { return idCodeQualifier; } set { idCodeQualifier = value; } }

        public String UniqueId { get { return uniqueId; } set { uniqueId = value.Substring (0, (value.Length > Server.Data.DataTypeConstants.UniqueId) ? Server.Data.DataTypeConstants.UniqueId : value.Length); } }

        #endregion


        #region Public Properties - Child Objects

        public List<EntityAddress> Addresses { get { return application.EntityAddressesGet (id, true); } }

        public EntityAddress CurrentMailingAddress { get { return  application.EntityAddressGet (Id, Server.Application.EntityAddressType.MailingAddress, DateTime.Today); } }

        public EntityAddress CurrentPhysicalAddress {

            get {

                EntityAddress currentPhysicalAddress = application.EntityAddressGet (Id, Server.Application.EntityAddressType.PhysicalAddress, DateTime.Today);

                //EntityAddress currentPhysicalAddress = null;

                //foreach (EntityAddress currentAddress in Addresses) {

                //    if (currentAddress.AddressType == Server.Application.EntityAddressType.PhysicalAddress) {

                //        if ((DateTime.Now >= currentAddress.EffectiveDate) && (DateTime.Now <= currentAddress.TerminationDate)) {

                //            currentPhysicalAddress = currentAddress;

                //            break;

                //        }

                //    }

                //}

                return currentPhysicalAddress;

            }

        }

        public EntityAddress CurrentResidentialAddress { get { return CurrentPhysicalAddress; } } // 2010-03-11: BACKWARDS COMPATIBILITY, REMOVED RESIDENTIAL

        public EntityAddress CurrentAddress (Server.Application.EntityAddressType forAddressType) {

            EntityAddress entityAddress = null;

            foreach (EntityAddress currentAddress in Addresses) {

                if (currentAddress.AddressType == forAddressType) {

                    if ((DateTime.Today >= currentAddress.EffectiveDate) && (DateTime.Today <= currentAddress.TerminationDate)) {

                        entityAddress = currentAddress;

                        break;

                    }

                }

            }

            return entityAddress;

        }


        public List<EntityContactInformation> ContactInformations { get { return application.EntityContactInformationsGet (id, true); } }

        public EntityContactInformation CurrentContactInformation (Server.Application.EntityContactType forContactType) {

            EntityContactInformation contactInformation = null;

            foreach (EntityContactInformation currentContactInformation in ContactInformations) {

                if (currentContactInformation.ContactType == forContactType) {

                    if ((DateTime.Today >= currentContactInformation.EffectiveDate) && (DateTime.Today <= currentContactInformation.TerminationDate)) {

                        contactInformation = currentContactInformation;

                        break;

                    }

                }

            }

            return contactInformation;
            
        }

        public EntityContactInformation CurrentContactInformationTelephone { get { return application.EntityContactInformationGet (Id, Server.Application.EntityContactType.Telephone, DateTime.Today); } }

        public EntityContactInformation CurrentContactInformationAlternateTelephone { get { return application.EntityContactInformationGet (Id, Server.Application.EntityContactType.AlternateTelephone, DateTime.Today); } }

        #endregion 


        #region Constructors

        public Entity (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public Entity (Application applicationReference, Server.Application.Entity serverEntity) {

            BaseConstructor (applicationReference, serverEntity);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.Entity serverEntity) {

            base.BaseConstructor (applicationReference, serverEntity);


            entityType = serverEntity.EntityType;


            nameLast = serverEntity.NameLast;

            nameFirst = serverEntity.NameFirst;

            nameMiddle = serverEntity.NameMiddle;

            namePrefix = serverEntity.NamePrefix;

            nameSuffix = serverEntity.NameSuffix;
            

            federalTaxId = serverEntity.FederalTaxId;

            idCodeQualifier = serverEntity.IdCodeQualifier;

            uniqueId = serverEntity.UniqueId;


            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.Entity serverObject) {

            base.MapToServerObject ((Server.Application.CoreObject)serverObject);


            serverObject.EntityType = entityType;


            serverObject.NameLast = nameLast;

            serverObject.NameFirst = nameFirst;

            serverObject.NameMiddle = nameMiddle;

            serverObject.NamePrefix = namePrefix;

            serverObject.NameSuffix = nameSuffix;


            serverObject.FederalTaxId = federalTaxId;

            serverObject.IdCodeQualifier = idCodeQualifier;

            serverObject.UniqueId = uniqueId;

           
            return;

        }

        public override Object ToServerObject () {

            Server.Application.Entity serverObject = new Server.Application.Entity ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        #endregion 

    }

}
