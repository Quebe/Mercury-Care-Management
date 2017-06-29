using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Forms.Controls {

    [Serializable]
    public class Address : Forms.Control {

        #region Private Properties

        private Int64 entityAddressId;

        private Mercury.Server.Application.EntityAddressType addressType = Mercury.Server.Application.EntityAddressType.NotSpecified;

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

                    if (entityAddressId != 0) {

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

                    if (Application != null) {

                        Core.Entity.EntityAddress entityAddress = Application.EntityAddressGet (value, true);

                        if (entityAddress != null) {

                            AddressType = entityAddress.AddressType;

                            EffectiveDate = entityAddress.EffectiveDate;

                            TerminationDate = entityAddress.TerminationDate;

                            Line1 = entityAddress.Line1;

                            Line2 = entityAddress.Line2;

                            City = entityAddress.City;

                            State = entityAddress.State;

                            ZipCode = entityAddress.ZipCode;

                            ZipPlus4 = entityAddress.ZipPlus4;

                            PostalCode = entityAddress.PostalCode;

                        }

                    }

                }

            }

        }

        public Mercury.Server.Application.EntityAddressType AddressType { get { return addressType; } set { addressType = value; } }

        public String AddressTypeDescription {

            get {

                if (Application != null) { return Mercury.Server.CommonFunctions.EnumerationToString (addressType); }

                return String.Empty;

            }

        }

        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }

        public String Line1 { get { return line1; } set { line1 = Server.CommonFunctions.SetValueMaxLength (value.Trim (), Server.Data.DataTypeConstants.AddressLine); } }

        public String Line2 { get { return line2; } set { line2 = Server.CommonFunctions.SetValueMaxLength (value.Trim (), Server.Data.DataTypeConstants.AddressLine); } }

        public String City { get { return city; } set { city = Server.CommonFunctions.SetValueMaxLength (value.Trim (), Server.Data.DataTypeConstants.AddressCity); } }

        public String State { get { return state; } set { state = Server.CommonFunctions.SetValueMaxLength (value.Trim (), Server.Data.DataTypeConstants.AddressState); } }

        public String ZipCode { get { return zipCode; } set { zipCode = Server.CommonFunctions.SetValueMaxLength (value.Trim (), Server.Data.DataTypeConstants.AddressZipCode); } }

        public String ZipPlus4 { get { return zipPlus4; } set { zipPlus4 = Server.CommonFunctions.SetValueMaxLength (value.Trim (), Server.Data.DataTypeConstants.AddressZipPlus4); } }

        public String PostalCode { get { return postalCode; } set { postalCode = Server.CommonFunctions.SetValueMaxLength (value.Trim (), Server.Data.DataTypeConstants.AddressPostalCode); } }

        public String CityStateZipCode {

            get {

                String description = String.Empty;

                description = city + ", " + state + " " + zipCode + ((zipPlus4.Trim ().Length != 0) ? "-" + zipPlus4 : String.Empty);

                return description;

            }

        }


        public override Boolean HasValue { get { return ((!String.IsNullOrEmpty (Line1)) && (!String.IsNullOrEmpty (ZipCode))); } }

        public override String Value { get { return (HasValue) ? Line1 + ((!String.IsNullOrEmpty (Line2)) ? ", " + Line2 : String.Empty) + ", " + CityStateZipCode : String.Empty; } }


        public override String JsonExtendedProperties {

            get {

                StringBuilder jsonBuilder = new StringBuilder ();

                jsonBuilder.Append (", " + JsonObjectProperty ("EntityAddressId", entityAddressId));

                jsonBuilder.Append (", " + JsonObjectProperty ("AddressType", Convert.ToInt32 (addressType)));

                jsonBuilder.Append (", " + JsonObjectProperty ("EffectiveDate", effectiveDate));

                jsonBuilder.Append (", " + JsonObjectProperty ("TerminationDate", terminationDate));

                jsonBuilder.Append (", " + JsonObjectProperty ("Line1", line1));

                jsonBuilder.Append (", " + JsonObjectProperty ("Line2", line2));

                jsonBuilder.Append (", " + JsonObjectProperty ("City", city));

                jsonBuilder.Append (", " + JsonObjectProperty ("State", state));

                jsonBuilder.Append (", " + JsonObjectProperty ("ZipCode", zipCode));

                jsonBuilder.Append (", " + JsonObjectProperty ("ZipPlus4", zipPlus4));

                jsonBuilder.Append (", " + JsonObjectProperty ("PostalCode", postalCode));

                return jsonBuilder.ToString ();

            }

        }

        #endregion
        

        #region Constructors

        virtual protected void InitializeControl (Application applicationReference) {

            BaseConstructor (applicationReference);

            ControlType = Mercury.Server.Application.FormControlType.Address;

            capabilities.HasValue = true;

            capabilities.HasLabel = false;

            capabilities.CanDataBind = true;

            capabilities.IsDataSource = true;

            label = new Label (applicationReference, this);            

            return;

        }

        override public void BaseConstructor (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Mercury.Server.Application.FormControl serverControl) {

            base.BaseConstructor (applicationReference, parentControl, serverControl);


            Mercury.Server.Application.FormControlAddress serverAddress = (Mercury.Server.Application.FormControlAddress) serverControl;

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

        public Address (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Mercury.Server.Application.FormControlAddress serverAddress) {

            InitializeControl (applicationReference);

            BaseConstructor (applicationReference, parentControl, serverAddress);

            ChildServerControlsToLocal (this, serverAddress);

        }

        #endregion


        #region Virtual Overrides

        public override void LocalControlToServer (Mercury.Server.Application.FormControl parentControl, Mercury.Server.Application.FormControl serverControl) {

            base.LocalControlToServer (parentControl, serverControl);

            ((Mercury.Server.Application.FormControlAddress) serverControl).EntityAddressId = EntityAddressId;

            ((Mercury.Server.Application.FormControlAddress) serverControl).AddressType = AddressType;

            ((Mercury.Server.Application.FormControlAddress) serverControl).EffectiveDate = EffectiveDate;

            ((Mercury.Server.Application.FormControlAddress) serverControl).TerminationDate = TerminationDate;

            ((Mercury.Server.Application.FormControlAddress) serverControl).Line1 = Line1;

            ((Mercury.Server.Application.FormControlAddress) serverControl).Line2 = Line2;

            ((Mercury.Server.Application.FormControlAddress) serverControl).City = City;

            ((Mercury.Server.Application.FormControlAddress) serverControl).State = State;

            ((Mercury.Server.Application.FormControlAddress) serverControl).ZipCode = ZipCode;

            ((Mercury.Server.Application.FormControlAddress) serverControl).ZipPlus4 = ZipPlus4;

            ((Mercury.Server.Application.FormControlAddress) serverControl).PostalCode = PostalCode;


            ((Mercury.Server.Application.FormControlAddress) serverControl).Label = new Mercury.Server.Application.FormControlLabel ();

            label.LocalControlToServer (serverControl, ((Mercury.Server.Application.FormControlAddress) serverControl).Label);

        }

        #endregion

    }

}
