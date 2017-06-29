using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace Mercury.Client.Core.Entity{

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


        private EntityAddress currentMailingAddress = null;

        private EntityAddress currentPhysicalAddress = null;

        private ObservableCollection<EntityAddress> addresses = null;

        private ObservableCollection<EntityContactInformation> contactInformations = null;

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

        public ObservableCollection<EntityAddress> Addresses {

            get {

                if (addresses == null) { Application.EntityAddressesGet (Id, true, EntityAddressesGetCompleted); }

                return addresses;

            }

        }

        public EntityAddress CurrentResidentialAddress { get { return CurrentPhysicalAddress; } } // BACKWARDS COMPATIBILITY WITH FORM EVENTS

        public EntityAddress CurrentPhysicalAddress {

            get {

                if (currentPhysicalAddress == null) { Application.EntityAddressGetByTypeDate (Id, Mercury.Server.Application.EntityAddressType.PhysicalAddress, DateTime.Today, true, CurrentPhysicalAddressCompleted); }

                return currentPhysicalAddress;

            }

        }

        public EntityAddress CurrentMailingAddress {

            get {

                if (currentMailingAddress == null) { Application.EntityAddressGetByTypeDate (Id, Mercury.Server.Application.EntityAddressType.MailingAddress, DateTime.Today, true, CurrentMailingAddressCompleted); }

                return currentMailingAddress;

            }

        }


        public ObservableCollection<EntityContactInformation> ContactInformations {

            get {

                if (contactInformations == null) { Application.EntityContactInformationsGet (Id, true, ContactInformationsCompleted); }

                return contactInformations;

            }

        }

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

        #endregion


        #region Property Data Binding Callbacks

        private void EntityAddressesGetCompleted (Object sender, Server.Application.EntityAddressesGetCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null)) { 

                // TODO: SILVERLIGHT CONVERSION

                // addresses = e.Result.Collection;

                NotifyPropertyChanged ("Addresses");

            }

            return;

        }

        private void CurrentMailingAddressCompleted (Object sender, Server.Application.EntityAddressGetByTypeDateCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                currentMailingAddress = new EntityAddress (Application, e.Result);

                NotifyPropertyChanged ("CurrentMailingAddress");

            }

            return;

        }

        private void CurrentPhysicalAddressCompleted (Object sender, Server.Application.EntityAddressGetByTypeDateCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                currentPhysicalAddress = new EntityAddress (Application, e.Result);

                NotifyPropertyChanged ("CurrentResidentialAddress");

                NotifyPropertyChanged ("CurrentPhysicalAddress");

            }

            return;

        }

        private void ContactInformationsCompleted (Object sender, Server.Application.EntityContactInformationsGetCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null)) {

                // TODO: SILVERLIGHT CONVERSION

                // contactInformations = e.Result.Collection;


                NotifyPropertyChanged ("ContactInformation");


                NotifyPropertyChanged ("CurrentContactTelephone");

                NotifyPropertyChanged ("CurrentContactFacsimile");

                NotifyPropertyChanged ("CurrentContactEmail");

                NotifyPropertyChanged ("CurrentContactEmergencyPhone");

                NotifyPropertyChanged ("CurrentContactCellPhone");

                NotifyPropertyChanged ("CurrentContactPager");

            }

            return;

        }

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

    }
}
