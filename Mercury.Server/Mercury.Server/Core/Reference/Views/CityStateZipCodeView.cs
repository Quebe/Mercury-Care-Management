using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Reference.Views {

    [DataContract (Name = "CityStateZipCodeView")]
    public class CityStateZipCodeView {
        
        #region Private Properties

        [DataMember (Name = "City")]
        private String city;

        [DataMember (Name = "State")]
        private String state;

        [DataMember (Name = "ZipCode")]
        private String zipCode;

        [DataMember (Name = "ZipPlus4")]
        private String zipPlus4;

        [DataMember (Name = "PostalCode")]
        private String postalCode;

        [DataMember (Name = "County")]
        private String county;


        #endregion 


        #region Public Properties

        public String City { get { return city; } set { city = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.AddressCity); } }

        public String State { get { return state; } set { state = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.AddressState); } }

        public String ZipCode { get { return zipCode; } set { zipCode = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.AddressZipCode); } }

        public String ZipPlus4 { get { return zipPlus4; } set { zipPlus4 = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.AddressZipPlus4); } }

        public String PostalCode { get { return postalCode; } set { postalCode = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.AddressPostalCode); } }

        public String County { get { return county; } set { county = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.AddressCounty); } }

        #endregion


        #region Constructors

        public CityStateZipCodeView () { /* DO NOTHING */ }

        public CityStateZipCodeView (String forCity, String forState, String forZipCode, String forZipPlus4, String forPostalCode, String forCounty) {

            City = forCity;

            State = forState;

            ZipCode = forZipCode;

            ZipPlus4 = forZipPlus4;

            PostalCode = forPostalCode;

            County = forCounty;

            return;

        }

        #endregion
    }

}
