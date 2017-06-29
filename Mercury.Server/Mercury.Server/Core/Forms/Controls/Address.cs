using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Forms.Controls {

    [Serializable]
    [DataContract (Name = "FormControlAddress")]
    public class Address : Forms.Control {

        #region Private Properties

        [DataMember (Name = "EntityAddressId")]
        private Int64 entityAddressId;

        [DataMember (Name = "AddressType")]
        private Core.Enumerations.EntityAddressType addressType = Mercury.Server.Core.Enumerations.EntityAddressType.NotSpecified;

        [DataMember (Name = "EffectiveDate")]
        private DateTime effectiveDate;

        [DataMember (Name = "TerminationDate")]
        private DateTime terminationDate;

        [DataMember (Name = "Line1")]
        private String line1;

        [DataMember (Name = "Line2")]
        private String line2;

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

        #endregion 


        #region Public Properties
        
        public Int64 AddressId { get { return EntityAddressId; } set { EntityAddressId = value; } } // LEFT IN FOR BACKWARDS COMPATIBILITY WITH EVENT HANDLERS

        public Int64 EntityAddressId {

            get { return entityAddressId; } 
            
            set {

                if (entityAddressId != value) {

                    entityAddressId = value;

                    if (entityAddressId != 0) {

                        AddressType = Mercury.Server.Core.Enumerations.EntityAddressType.NotSpecified;

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

                    if (application != null) {

                        Core.Entity.EntityAddress entityAddress = application.EntityAddressGet (value);

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

        public Core.Enumerations.EntityAddressType AddressType { get { return addressType; } set { addressType = value; } }

        public String AddressTypeDescription {

            get {

                if (application != null) { return CommonFunctions.EnumerationToString (addressType); }

                return String.Empty;

            }

        }

        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }

        public String Line1 { get { return line1; } set { line1 = CommonFunctions.SetValueMaxLength (value.Trim (), Data.DataTypeConstants.AddressLine); } }

        public String Line2 { get { return line2; } set { line2 = CommonFunctions.SetValueMaxLength (value.Trim (), Data.DataTypeConstants.AddressLine); } }

        public String City { get { return city; } set { city = CommonFunctions.SetValueMaxLength (value.Trim (), Data.DataTypeConstants.AddressCity); } }

        public String State { get { return state; } set { state = CommonFunctions.SetValueMaxLength (value.Trim (), Data.DataTypeConstants.AddressState); } }

        public String ZipCode { get { return zipCode; } set { zipCode = CommonFunctions.SetValueMaxLength (value.Trim (), Data.DataTypeConstants.AddressZipCode); } }

        public String ZipPlus4 { get { return zipPlus4; } set { zipPlus4 = CommonFunctions.SetValueMaxLength (value.Trim (), Data.DataTypeConstants.AddressZipPlus4); } }

        public String PostalCode { get { return postalCode; } set { postalCode = CommonFunctions.SetValueMaxLength (value.Trim (), Data.DataTypeConstants.AddressPostalCode); } }

        public String CityStateZipCode {

            get {

                String description = String.Empty;

                description = city + ", " + state + " " + zipCode + ((zipPlus4.Trim ().Length != 0) ? "-" + zipPlus4 : String.Empty);

                return description;

            }

        }


        public override System.Xml.XmlDocument ExtendedPropertiesXml {

            get {

                System.Xml.XmlDocument extendedProperties = base.ExtendedPropertiesXml;

                ExtendedProperties_AddProperty (extendedProperties, "EntityAddressId", EntityAddressId.ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "AddressType", ((Int32) AddressType).ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "AddressTypeDescription", AddressTypeDescription);

                ExtendedProperties_AddProperty (extendedProperties, "EffectiveDate", EffectiveDate.ToString ("MM/dd/yyyy"));

                ExtendedProperties_AddProperty (extendedProperties, "TerminationDate", TerminationDate.ToString ("MM/dd/yyyy"));

                ExtendedProperties_AddProperty (extendedProperties, "Line1", Line1);

                ExtendedProperties_AddProperty (extendedProperties, "Line2", Line2);

                ExtendedProperties_AddProperty (extendedProperties, "City", City);

                ExtendedProperties_AddProperty (extendedProperties, "State", State);

                ExtendedProperties_AddProperty (extendedProperties, "ZipCode", ZipCode);

                ExtendedProperties_AddProperty (extendedProperties, "ZipPlus4", ZipPlus4);

                ExtendedProperties_AddProperty (extendedProperties, "PostalCode", PostalCode);

                return extendedProperties;

            }

        }

        public override void ExtendedPropertiesDeserialize (System.Xml.XmlNode extendedPropertiesXml) {

            base.ExtendedPropertiesDeserialize (extendedPropertiesXml);

            // foreach (System.Xml.XmlNode currentPropertyNode in extendedPropertiesXml.SelectNodes ("./Property")) {

            foreach (System.Xml.XmlNode currentPropertyNode in extendedPropertiesXml.ChildNodes) {

                switch (currentPropertyNode.Attributes["Name"].InnerText) {

                    // FIRST ONE IS FOR BACKWARDS COMPATIBILITY

                    case "AddressId": entityAddressId = Convert.ToInt64 (currentPropertyNode.InnerText); break; // NOTICE LOWER CASE PRIVATE VARIABLE SO NOT TO OVERRIDE INCOMING DATA

                    case "EntityAddressId": entityAddressId = Convert.ToInt64 (currentPropertyNode.InnerText); break; // NOTICE LOWER CASE PRIVATE VARIABLE SO NOT TO OVERRIDE INCOMING DATA

                    case "AddressType":

                        AddressType = Mercury.Server.Core.Enumerations.EntityAddressType.NotSpecified;

                        if (!String.IsNullOrEmpty (currentPropertyNode.InnerText)) {

                            switch (currentPropertyNode.InnerText) {

                                //// HISTORICAL CORRECTIONS -- 2010-03-11: NO LONGER NEEDED, CHANGED THE ENUMERATION TO NOT DEPEND UPON HIPAA/X12 STD

                                //case "01":

                                //case "1": addressType = Mercury.Server.Core.Enumerations.EntityAddressType.PhysicalAddress; break;


                                //case "31": addressType = Mercury.Server.Core.Enumerations.EntityAddressType.MailingAddress; break;

                                //case "77": addressType = Mercury.Server.Core.Enumerations.EntityAddressType.ServiceLocation; break;


                                // STANDARD MAPPING

                                default: AddressType = (Mercury.Server.Core.Enumerations.EntityAddressType) Convert.ToInt32 (currentPropertyNode.InnerText); break;

                            }

                        }

                        break;

                    case "EffectiveDate": EffectiveDate = Convert.ToDateTime (currentPropertyNode.InnerText); break;

                    case "TerminationDate": TerminationDate = Convert.ToDateTime (currentPropertyNode.InnerText); break;

                    case "Line1": Line1 = currentPropertyNode.InnerText; break;

                    case "Line2": Line2 = currentPropertyNode.InnerText; break;

                    case "City": City = currentPropertyNode.InnerText; break;

                    case "State": State = currentPropertyNode.InnerText; break;

                    case "ZipCode": ZipCode = currentPropertyNode.InnerText; break;

                    case "ZipPlus4": ZipPlus4 = currentPropertyNode.InnerText; break;

                    case "PostalCode": PostalCode = currentPropertyNode.InnerText; break;

                }

            }

            return;

        }

        public override System.Xml.XmlDocument ValuesXml {

            get {

                System.Xml.XmlDocument values = base.ValuesXml;

                Values_AddValue (values, "EntityAddressId", EntityAddressId.ToString ());

                Values_AddValue (values, "AddressType", ((Int32) AddressType).ToString ());

                Values_AddValue (values, "AddressTypeDescription", AddressTypeDescription);

                Values_AddValue (values, "EffectiveDate", EffectiveDate.ToString ("MM/dd/yyyy"));

                Values_AddValue (values, "TerminationDate", TerminationDate.ToString ("MM/dd/yyyy"));

                Values_AddValue (values, "Line1", Line1);

                Values_AddValue (values, "Line2", Line2);

                Values_AddValue (values, "City", City);

                Values_AddValue (values, "State", State);

                Values_AddValue (values, "ZipCode", ZipCode);

                Values_AddValue (values, "ZipPlus4", ZipPlus4);

                Values_AddValue (values, "PostalCode", PostalCode);
                
                return values;

            }

        }

        public override Boolean HasValue { get { return ((!String.IsNullOrEmpty (Line1)) && (!String.IsNullOrEmpty (ZipCode))); } }

        public override String Value { get { return (HasValue) ? Line1 + ((!String.IsNullOrEmpty (Line2)) ? ", " + Line2 : String.Empty) + ", " + CityStateZipCode : String.Empty; } }


        public override Application Application {

            set {

                base.Application = value;

                if (label != null) { label.Application = value; }

            }

        }

        #endregion


        #region Constructors
        
        protected void InitializeControl (Application applicationReference) {

            BaseConstructor (applicationReference);

            controlType = Mercury.Server.Core.Forms.Enumerations.FormControlType.Address;

            capabilities.HasValue = true;

            capabilities.HasLabel = false;

            capabilities.CanDataBind = true;

            capabilities.IsDataSource = true;

            label = new Label (applicationReference);

            return;

        }

        public Address (Application applicationReference) {

            InitializeControl (applicationReference);

            return;

        }

        public Address (Application applicationReference, String labelText) {

            InitializeControl (applicationReference);

            label = new Label (applicationReference, labelText);

            return;

        }

        #endregion 


        #region Compile Methods

        public override List<CompileMessage> Compile () {

            List<CompileMessage> compileMessages = new List<CompileMessage> ();


            compileMessages.AddRange (base.Compile ());

            return compileMessages;

        }

        #endregion 


        #region Event Handlers

        public override List<String> Events {

            get {

                List<String> events = new List<String> ();

                events.Add ("AddressChanged");

                return events;

            }

        }

        #endregion 


        #region Data Bindings

        public override Dictionary<String, String> DataBindableProperties {

            get {

                Dictionary<String, String> bindableProperties = new Dictionary<String, String> ();

                bindableProperties.Add ("EntityAddressId", "Id|EntityAddress");

                return bindableProperties;

            }

        }

        public override Dictionary<String, String> DataBindingContexts {

            get {

                Dictionary<String, String> bindingContexts = new Dictionary<String, String> ();

                bindingContexts.Add ("EntityAddressId", "Id|EntityAddress");

                bindingContexts.Add ("AddressType", "Integer");

                bindingContexts.Add ("AddressTypeDescription", "String");

                bindingContexts.Add ("EffectiveDate", "DateTime");

                bindingContexts.Add ("TerminationDate", "DateTime");

                bindingContexts.Add ("Line1", "String");

                bindingContexts.Add ("Line2", "String");

                bindingContexts.Add ("City", "String");

                bindingContexts.Add ("State", "String");

                bindingContexts.Add ("ZipCode", "String");

                bindingContexts.Add ("ZipPlus4", "String");

                bindingContexts.Add ("PostalCode", "String");

                bindingContexts.Add ("CityStateZipCode", "String");

                return bindingContexts;

            }

        }

        public override String EvaluateDataBinding (Structures.DataBinding dataBinding) {

            String dataValue = String.Empty;

            String bindingContextPart = dataBinding.BindingContext.Split ('.')[0];

            switch (bindingContextPart) {

                case "EntityAddressId": dataValue = entityAddressId.ToString (); break;

                case "AddressType": dataValue = ((Int32) AddressType).ToString (); break;

                case "AddressTypeDescription": dataValue = AddressTypeDescription; break;

                case "EffectiveDate": dataValue = EffectiveDate.ToString ("MM/dd/yyyy"); break;

                case "TerminationDate": dataValue = TerminationDate.ToString ("MM/dd/yyyy"); break;

                case "Line1": dataValue = Line1; break;

                case "Line2": dataValue = Line2; break;

                case "City": dataValue = City; break;

                case "State": dataValue = State; break;

                case "ZipCode": dataValue = ZipCode; break;

                case "ZipPlus4": dataValue = ZipPlus4; break;

                case "PostalCode": dataValue = PostalCode; break;

                case "CityStateZipCode": dataValue = CityStateZipCode; break;

                default: dataValue = "!Error"; break;

            }

            return dataValue;

        }

        public override void OnDataSourceChanged (Control dataSourceControl, Boolean propogate) {

            base.OnDataSourceChanged (dataSourceControl, propogate);

            try {

                if (ContainsDataBinding (dataSourceControl.ControlId)) {

                    foreach (Structures.DataBinding currentDataBinding in GetDataBindings (dataSourceControl.ControlId)) {

                        switch (currentDataBinding.BoundProperty) {

                            case "EntityAddressId": 
                                
                                

                                EntityAddressId = Convert.ToInt64 (dataSourceControl.EvaluateDataBinding (currentDataBinding)); 
                                
                                break;

                        }

                    }

                }

                else if (dataSourceControl.ControlId == Form.ControlId) {

                    foreach (Forms.Structures.DataBinding currentDataBinding in DataBindings) {

                        if (currentDataBinding.DataBindingType != Mercury.Server.Core.Forms.Enumerations.FormControlDataBindingType.Control) {

                            switch (currentDataBinding.BoundProperty) {

                                case "EntityAddressId": EntityAddressId = Convert.ToInt64 (dataSourceControl.EvaluateDataBinding (currentDataBinding)); break;

                            }

                        }

                    }

                }

            }

            catch { /* DO NOTHING */ }

        }

        #endregion

    }

}
