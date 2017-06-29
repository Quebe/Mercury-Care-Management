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

namespace Mercury.Client.Core.Entity {

    public class EntityAddress : CoreObject {

        #region Private Properties

        private Int64 entityId;


        private Server.Application.EntityAddressType addressType = Server.Application.EntityAddressType.NotSpecified;

        private String line1 = String.Empty;

        private String line2 = String.Empty;

        private String city = String.Empty;

        private String state = String.Empty;

        private String zipCode = String.Empty;

        private String zipPlus4 = String.Empty;

        private String postalCode = String.Empty;

        private String county = String.Empty;


        private Decimal longitude = 0.0m;

        private Decimal latitude = 0.0m;


        private DateTime effectiveDate;

        private DateTime terminationDate;


        private Entity entity = null;

        #endregion


        #region Public Properties

        public Int64 EntityId { get { return entityId; } set { entityId = value; } }

        public Server.Application.EntityAddressType AddressType { get { return addressType; } set { addressType = value; } }

        public String AddressTypeDescription { get { return Server.CommonFunctions.EnumerationToString (addressType); } }


        public String Line1 { get { return line1; } set { line1 = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.AddressLine); } }

        public String Line2 { get { return line2; } set { line2 = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.AddressLine); } }

        public String City { get { return city; } set { city = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.AddressCity); } }

        public String State { get { return state; } set { state = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.AddressState); } }

        public String ZipCode { get { return zipCode; } set { zipCode = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.AddressZipCode); } }

        public String ZipPlus4 { get { return zipPlus4; } set { zipPlus4 = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.AddressZipPlus4); } }

        public String PostalCode { get { return postalCode; } set { postalCode = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.AddressPostalCode); } }

        public String County { get { return county; } set { county = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.AddressCounty); } }


        public Decimal Longitude { get { return longitude; } set { longitude = value; } }

        public Decimal Latitude { get { return latitude; } set { latitude = value; } }


        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }


        public String EffectiveDateDescription { get { return EffectiveDate.ToString ("MM/dd/yyyy"); } }

        public String TerminationDateDescription { get { return ((TerminationDate == new DateTime (9999, 12, 31)) ? "< active >" : TerminationDate.ToString ("MM/dd/yyyy")); } }


        public String CityStateZipCode {

            get {

                String description = String.Empty;

                description = city + ", " + state + " " + zipCode + ((!String.IsNullOrWhiteSpace (zipPlus4)) ? "-" + zipPlus4 : String.Empty);

                return description;

            }

        }

        public String AddressSingleLine {

            get {

                String singleLine;

                singleLine = line1 + " " + ((line2.Trim ().Length != 0) ? ", " + line2 : String.Empty) + ", " + CityStateZipCode;

                return singleLine;

            }

        }


        public Boolean IsActive { get { return ((DateTime.Today >= effectiveDate) && (DateTime.Today <= terminationDate)); } }

        #endregion


        #region Public Data Binding Properties

        public Entity Entity {

            get {

                if (entity == null) {

                    GlobalProgressBarShow ("Entity");

                    Application.EntityGet (entityId, true, EntityGetCompleted);

                }

                return entity;

            }

        }

        #endregion 
        
        #region Property Data Binding Callbacks

        private void EntityGetCompleted (Object sender, Server.Application.EntityGetCompletedEventArgs e) {

            GlobalProgressBarHide ("Entity");

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                entity = new Entity (Application, e.Result);

                NotifyPropertyChanged ("Name");

                NotifyPropertyChanged ("Entity");

            }

            return;

        }

        #endregion 

        #region Constructors

        public EntityAddress (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public EntityAddress (Application applicationReference, Server.Application.EntityAddress serverEntityAddress) {

            BaseConstructor (applicationReference, serverEntityAddress);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.EntityAddress serverEntityAddress) {

            base.BaseConstructor (applicationReference, serverEntityAddress);


            entityId = serverEntityAddress.EntityId;

            addressType = serverEntityAddress.AddressType;

            line1 = serverEntityAddress.Line1;

            line2 = serverEntityAddress.Line2;

            city = serverEntityAddress.City;

            state = serverEntityAddress.State;

            zipCode = serverEntityAddress.ZipCode;

            zipPlus4 = serverEntityAddress.ZipPlus4;

            postalCode = serverEntityAddress.PostalCode;

            county = serverEntityAddress.County;


            longitude = serverEntityAddress.Longitude;

            latitude = serverEntityAddress.Latitude;


            effectiveDate = serverEntityAddress.EffectiveDate;

            terminationDate = serverEntityAddress.TerminationDate;

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.EntityAddress serverEntityAddress) {

            base.MapToServerObject ((Server.Application.CoreObject)serverEntityAddress);


            serverEntityAddress.EntityId = entityId;

            serverEntityAddress.AddressType = addressType;

            serverEntityAddress.Line1 = line1;

            serverEntityAddress.Line2 = line2;

            serverEntityAddress.City = city;

            serverEntityAddress.State = state;

            serverEntityAddress.ZipCode = zipCode;

            serverEntityAddress.ZipPlus4 = zipPlus4;

            serverEntityAddress.PostalCode = postalCode;

            serverEntityAddress.County = county;


            serverEntityAddress.Longitude = longitude;

            serverEntityAddress.Latitude = latitude;


            serverEntityAddress.EffectiveDate = effectiveDate;

            serverEntityAddress.TerminationDate = terminationDate;

            return;

        }

        public override Object ToServerObject () {

            Server.Application.EntityAddress serverEntityAddress = new Server.Application.EntityAddress ();

            MapToServerObject (serverEntityAddress);

            return serverEntityAddress;

        }

        public EntityAddress Copy () {

            Server.Application.EntityAddress serverEntityAddress = (Server.Application.EntityAddress)ToServerObject ();

            EntityAddress copiedEntityAddress = new EntityAddress (application, serverEntityAddress);

            return copiedEntityAddress;

        }

        public Boolean IsEqual (EntityAddress compareEntityAddress) {

            Boolean isEqual = base.IsEqual ((CoreObject)compareEntityAddress);


            isEqual &= (entityId == compareEntityAddress.EntityId);

            isEqual &= (addressType == compareEntityAddress.AddressType);


            isEqual &= (line1 == compareEntityAddress.Line1);

            isEqual &= (line2 == compareEntityAddress.Line2);

            isEqual &= (city == compareEntityAddress.City);

            isEqual &= (state == compareEntityAddress.State);

            isEqual &= (zipCode == compareEntityAddress.ZipCode);

            isEqual &= (zipPlus4 == compareEntityAddress.ZipPlus4);

            isEqual &= (postalCode == compareEntityAddress.PostalCode);

            isEqual &= (county == compareEntityAddress.County);


            isEqual &= (longitude == compareEntityAddress.Longitude);

            isEqual &= (latitude == compareEntityAddress.Latitude);


            isEqual &= (effectiveDate == compareEntityAddress.EffectiveDate);

            isEqual &= (terminationDate == compareEntityAddress.TerminationDate);


            return isEqual;

        }

        #endregion 

    }

}
