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

namespace Mercury.Client.Core.Forms.Controls {

    public class Address : Control {

        #region Private Properties

        private Int64 entityAddressId;

        private Server.Application.EntityAddressType addressType = Mercury.Server.Application.EntityAddressType.NotSpecified;

        private DateTime effectiveDate;

        private DateTime terminationDate;

        private String line1 = String.Empty;

        private String line2 = String.Empty;

        private String city = String.Empty;

        private String state = String.Empty;

        private String zipCode = String.Empty;

        private String zipPlus4 = String.Empty;

        private String postalCode = String.Empty;

        #endregion


        #region Public Properties

        public Int64 AddressId { get { return EntityAddressId; } set { EntityAddressId = value; } } // LEFT IN FOR BACKWARDS COMPATIBILITY WITH EVENT HANDLERS

        public Int64 EntityAddressId {

            get { return entityAddressId; }

            set {

                if (entityAddressId != value) {

                    entityAddressId = value;

                    isLoaded = false;

                    if (entityAddressId != 0) {   // WHEN SETTING TO CUSTOM ADDRESS "0", DON'T RESET EXISTING PROPERTIES

                        AddressType = Mercury.Server.Application.EntityAddressType.NotSpecified;

                        EffectiveDate = new DateTime ();

                        TerminationDate = new DateTime ();

                        Line1 = String.Empty;

                        Line2 = String.Empty;

                        City = String.Empty;

                        State = String.Empty;

                        ZipCode = String.Empty;

                        ZipPlus4 = String.Empty;

                        PostalCode = String.Empty;

                    }

                    if (entityAddressId == 0) { isLoaded = true; }

                    NotifyPropertyChanged ("AddressId");

                    NotifyPropertyChanged ("EntityAddressId");

                    if ((Application != null) && (entityAddressId != 0)) {

                        Application.EntityAddressGet (entityAddressId, true, Application_EntityAddressGet_Completed);

                    }

                }

            }

        }

        public Server.Application.EntityAddressType AddressType { get { return addressType; } set { addressType = value; } }

        public String AddressTypeDescription {

            get {

                if (Application != null) { return Server.CommonFunctions.EnumerationToString (addressType); }

                return String.Empty;

            }

        }
        
        public DateTime EffectiveDate { 
            
            get { return effectiveDate; }

            set {

                if (effectiveDate != value) {

                    effectiveDate = value;

                    NotifyPropertyChanged ("EffectiveDate");

                }

            }
        
        }

        public DateTime TerminationDate { 
            
            get { return terminationDate; } 
            
            set {

                if (terminationDate != value) {

                    terminationDate = value;

                    NotifyPropertyChanged ("TerminationDate");

                }

            }

        }

        public String Line1 {

            get { return line1; }

            set {

                if (line1 != value) {

                    line1 = Server.CommonFunctions.SetValueMaxLength (value.Trim (), Server.Data.DataTypeConstants.AddressLine);

                    NotifyPropertyChanged ("Line1");

                }

            }

        }

        public String Line2 {

            get { return line2; }

            set {

                if (line2 != value) {

                    line2 = Server.CommonFunctions.SetValueMaxLength (value.Trim (), Server.Data.DataTypeConstants.AddressLine);

                    NotifyPropertyChanged ("Line2");

                }

            }

        }

        public String City { 
            
            get { return city; } 
            
            set {

                if (city != value) {

                    city = Server.CommonFunctions.SetValueMaxLength (value.Trim (), Server.Data.DataTypeConstants.AddressCity);

                    NotifyPropertyChanged ("City");

                    NotifyPropertyChanged ("CityStateZipCode");

                }

            }

        }

        public String State { 
            
            get { return state; } 
            
            set {

                if (state != value) {

                    state = Server.CommonFunctions.SetValueMaxLength (value.Trim (), Server.Data.DataTypeConstants.AddressState);

                    NotifyPropertyChanged ("State");

                    NotifyPropertyChanged ("CityStateZipCode");

                }

            }

        }

        public String ZipCode { 
            
            get { return zipCode; } 
            
            set {

                if (zipCode != value) {

                    zipCode = Server.CommonFunctions.SetValueMaxLength (value.Trim (), Server.Data.DataTypeConstants.AddressZipCode);

                    NotifyPropertyChanged ("ZipCode");

                    NotifyPropertyChanged ("CityStateZipCode");

                }

            }

        }

        public String ZipPlus4 { 
            
            get { return zipPlus4; } 
            
            set {

                if (zipPlus4 != value) {

                    zipPlus4 = Server.CommonFunctions.SetValueMaxLength (value.Trim (), Server.Data.DataTypeConstants.AddressZipPlus4);

                    NotifyPropertyChanged ("ZipPlus4");

                    NotifyPropertyChanged ("CityStateZipCode");

                }

            }

        }

        public String PostalCode { 
            
            get { return postalCode; } 
            
            set {

                if (postalCode != value) {

                    postalCode = Server.CommonFunctions.SetValueMaxLength (value.Trim (), Server.Data.DataTypeConstants.AddressPostalCode);

                    NotifyPropertyChanged ("PostalCode");

                }

            }

        }

        public String CityStateZipCode {

            get {

                String description = String.Empty;

                description = city + ", " + state + " " + zipCode + ((zipPlus4.Trim ().Length != 0) ? "-" + zipPlus4 : String.Empty);

                return description;

            }

        }


        public override Boolean HasValue { get { return ((!String.IsNullOrEmpty (Line1)) && (!String.IsNullOrEmpty (ZipCode))); } }

        public override String Value { get { return (HasValue) ? Line1 + ((!String.IsNullOrEmpty (Line2)) ? ", " + Line2 : String.Empty) + ", " + CityStateZipCode : String.Empty; } }

        #endregion


        #region Constructors

        virtual protected void InitializeControl (Application applicationReference) {

            BaseConstructor (applicationReference);

            ControlType = Server.Application.FormControlType.Address;

            capabilities.HasValue = true;

            capabilities.HasLabel = false;

            capabilities.CanDataBind = true;

            capabilities.IsDataSource = true;

            label = new Label (applicationReference, this);

            return;

        }

        override public void BaseConstructor (Mercury.Client.Core.Forms.Control parentControl, Server.Application.FormControl serverControl) {

            base.BaseConstructor (parentControl, serverControl);


            Server.Application.FormControlAddress serverAddress = (Server.Application.FormControlAddress) serverControl;

            EntityAddressId = serverAddress.EntityAddressId;

            AddressType = serverAddress.AddressType;

            EffectiveDate = serverAddress.EffectiveDate;

            TerminationDate = serverAddress.TerminationDate;

            Line1 = serverAddress.Line1;

            Line2 = serverAddress.Line2;

            City = serverAddress.City;

            State = serverAddress.State;

            ZipCode = serverAddress.ZipCode;

            ZipPlus4 = serverAddress.ZipPlus4;

            PostalCode = serverAddress.PostalCode;


            label = new Label (Application, this, serverAddress.Label);

            return;

        }


        public Address (Application applicationReference) {

            InitializeControl (applicationReference);

        }

        public Address (Application applicationReference, String labelText) {

            InitializeControl (applicationReference);

            label.Text = labelText;

        }

        public Address (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Server.Application.FormControlAddress serverAddress) {

            InitializeControl (applicationReference);

            BaseConstructor (parentControl, serverAddress);

            ChildServerControlsToLocal (this, serverAddress);

        }

        #endregion


        #region Silverlight Data Bindings and Async Operations

        protected override void NotifyPropertyChanged (String propertyName) {

            if (String.IsNullOrEmpty (propertyName)) { return; }

            if ((propertyName != "AddressId") || (propertyName != "EntityAddressId")) {

                base.NotifyPropertyChanged (propertyName);

            }

            else {

                base.NotifyPropertyChanged ("Id");

                base.NotifyPropertyChanged ("AddressId");

                base.NotifyPropertyChanged ("EntityAddressId");

                base.NotifyPropertyChanged ("Name");

                base.NotifyPropertyChanged ("AddressType");

                base.NotifyPropertyChanged ("AddressTypeDescription");

                base.NotifyPropertyChanged ("EffectiveDate");

                base.NotifyPropertyChanged ("TerminationDate");

                base.NotifyPropertyChanged ("Line1");

                base.NotifyPropertyChanged ("Line2");

                base.NotifyPropertyChanged ("City");

                base.NotifyPropertyChanged ("State");

                base.NotifyPropertyChanged ("ZipCode");

                base.NotifyPropertyChanged ("ZipPlus4");

                base.NotifyPropertyChanged ("PostalCode");

                base.NotifyPropertyChanged ("CityStateZipCode");

                base.NotifyPropertyChanged ("Value");

            }

            return;

        }

        public void Application_EntityAddressGet_Completed (Object sender, Server.Application.EntityAddressGetCompletedEventArgs e) {

            if (e.Result != null) {

                AddressType = e.Result.AddressType;

                EffectiveDate = e.Result.EffectiveDate;

                TerminationDate = e.Result.TerminationDate;

                Line1 = e.Result.Line1;

                Line2 = e.Result.Line2;

                City = e.Result.City;

                State = e.Result.State;

                ZipCode = e.Result.ZipCode;

                ZipPlus4 = e.Result.ZipPlus4;

                PostalCode = e.Result.PostalCode;

                isLoaded = true;

                NotifyPropertyChanged ("Id");

            }

            isLoaded = true;

            return;

        }

        #endregion 


        #region Virtual Overrides

        public override void LocalControlToServer (Server.Application.FormControl parentControl, Server.Application.FormControl serverControl) {

            base.LocalControlToServer (parentControl, serverControl);

            ((Server.Application.FormControlAddress) serverControl).EntityAddressId = EntityAddressId;

            ((Server.Application.FormControlAddress) serverControl).AddressType = AddressType;

            ((Server.Application.FormControlAddress) serverControl).EffectiveDate = EffectiveDate;

            ((Server.Application.FormControlAddress) serverControl).TerminationDate = TerminationDate;

            ((Server.Application.FormControlAddress) serverControl).Line1 = Line1;

            ((Server.Application.FormControlAddress) serverControl).Line2 = Line2;

            ((Server.Application.FormControlAddress) serverControl).City = City;

            ((Server.Application.FormControlAddress) serverControl).State = State;

            ((Server.Application.FormControlAddress) serverControl).ZipCode = ZipCode;

            ((Server.Application.FormControlAddress) serverControl).ZipPlus4 = ZipPlus4;

            ((Server.Application.FormControlAddress) serverControl).PostalCode = PostalCode;


            ((Server.Application.FormControlAddress) serverControl).Label = new Server.Application.FormControlLabel ();

            label.LocalControlToServer (serverControl, ((Server.Application.FormControlAddress) serverControl).Label);

        }

        public override void UpdateControl (Mercury.Server.Application.FormControl serverControl) {

            base.UpdateControl (serverControl);


            Server.Application.FormControlAddress serverAddressControl = (Server.Application.FormControlAddress) serverControl;

            if (entityAddressId != serverAddressControl.EntityAddressId) {

                entityAddressId = serverAddressControl.EntityAddressId;

                NotifyPropertyChanged ("AddressId");

                NotifyPropertyChanged ("EntityAddressId");

            }


            AddressType = serverAddressControl.AddressType;

            EffectiveDate = serverAddressControl.EffectiveDate;

            TerminationDate = serverAddressControl.TerminationDate;


            Line1 = serverAddressControl.Line1;

            Line2 = serverAddressControl.Line2;

            City = serverAddressControl.City;

            State = serverAddressControl.State;

            ZipCode = serverAddressControl.ZipCode;

            ZipPlus4 = serverAddressControl.ZipPlus4;

            PostalCode = serverAddressControl.PostalCode;


            return;

        }

        #endregion


    }

}
