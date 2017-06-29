using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Forms.Controls {

    [Serializable]
    [DataContract (Name = "FormControlInput")]
    public class Input : Mercury.Server.Core.Forms.Control {

        #region  Private Properties

        private const Int64 radNumericLimitation = 70368744177663;

        [DataMember (Name = "InputType")]
        protected Mercury.Server.Core.Forms.Enumerations.FormControlInputType inputType = Mercury.Server.Core.Forms.Enumerations.FormControlInputType.Text;

        [DataMember (Name = "Text")]
        protected String text = String.Empty;

        [DataMember (Name = "TextMode")]
        protected Mercury.Server.Core.Forms.Enumerations.FormControlTextMode textMode = Mercury.Server.Core.Forms.Enumerations.FormControlTextMode.SingleLine;

        [DataMember (Name = "Columns")]
        protected Int32 columns = 40;

        [DataMember (Name = "Rows")]
        protected Int32 rows = 1;

        [DataMember (Name = "Wrap")]
        protected Boolean wrap = false;

        [DataMember (Name = "MaxLength")]
        protected Int32 maxLength = 8000;

        [DataMember (Name = "EmptyMessage")]
        protected String emptyMessage = String.Empty;

        [DataMember (Name = "Validation")]
        protected String validation = String.Empty;

        [DataMember (Name = "SelectionOnFocus")]
        protected Mercury.Server.Core.Forms.Enumerations.FormControlSelectionOnFocus selectionOnFocus = Mercury.Server.Core.Forms.Enumerations.FormControlSelectionOnFocus.None;

        [DataMember (Name = "Mask")]
        protected String mask = String.Empty;

        // Numeric Specific Functions

        [DataMember (Name = "NumericType")]
        protected Mercury.Server.Core.Forms.Enumerations.FormControlNumericType numericType = Mercury.Server.Core.Forms.Enumerations.FormControlNumericType.Number;

        [DataMember (Name = "MinValue")]
        private Double minValue = (radNumericLimitation * -1);

        [DataMember (Name = "MaxValue")]
        private Double maxValue = (radNumericLimitation);
       
        [DataMember (Name = "ShowSpinButtons")]
        protected Boolean showSpinButtons = false;

        [DataMember (Name = "ButtonPosition")]
        protected Mercury.Server.Core.Forms.Enumerations.FormControlSpinnerButtonPosition buttonPosition = Mercury.Server.Core.Forms.Enumerations.FormControlSpinnerButtonPosition.Right;

        // Date Specific Functions

        [DataMember (Name = "DateFormat")]
        protected String dateFormat = String.Empty;

        [DataMember (Name = "DisplayDateFormat")]
        protected String displayDateFormat = string.Empty;

        [DataMember (Name = "MinDate")]
        protected DateTime minDate = new DateTime (1900, 01, 01);

        [DataMember (Name = "MaxDate")]
        protected DateTime maxDate = new DateTime (2999, 12, 31);

        #endregion


        #region Public Properties

        public Mercury.Server.Core.Forms.Enumerations.FormControlInputType InputType { get { return inputType; } set { inputType = value; } }

        public String Text {

            get { return text; }

            set {

                if (text == value) { return; }


                Boolean validInput = true;

                if (!String.IsNullOrEmpty (value)) {

                    #region Specific Value, Determine if Valid by Input Type

                    switch (inputType) {

                        case Mercury.Server.Core.Forms.Enumerations.FormControlInputType.Text:

                            text = value;

                            if (!String.IsNullOrEmpty (validation)) {

                                System.Text.RegularExpressions.Regex validator = new System.Text.RegularExpressions.Regex (validation);

                                validInput = validator.IsMatch (text);

                            }

                            break;

                        case Mercury.Server.Core.Forms.Enumerations.FormControlInputType.DateTime:

                            DateTime resultDateTime;

                            if (DateTime.TryParse (value, out resultDateTime)) {

                                text = value;

                            }

                            else if (String.IsNullOrEmpty (value)) {

                                text = String.Empty;

                            }

                            else { validInput = false; }

                            break;

                        case Mercury.Server.Core.Forms.Enumerations.FormControlInputType.Numeric:

                            switch (numericType) {

                                case Mercury.Server.Core.Forms.Enumerations.FormControlNumericType.Percent:

                                case Mercury.Server.Core.Forms.Enumerations.FormControlNumericType.Number:

                                case Mercury.Server.Core.Forms.Enumerations.FormControlNumericType.Currency:

                                    Double resultDouble;

                                    if (Double.TryParse (value, out resultDouble)) {

                                        if ((resultDouble >= minValue) && (resultDouble <= maxValue)) {

                                            text = value;
                                        }

                                        else if (String.IsNullOrEmpty (value)) {

                                            text = String.Empty;

                                        }

                                        else { validInput = false; }

                                    }

                                    else { validInput = false; }

                                    break;

                                case Mercury.Server.Core.Forms.Enumerations.FormControlNumericType.Integer:

                                    Int64 resultInteger;

                                    if (Int64.TryParse (value, out resultInteger)) {

                                        if ((resultInteger >= minValue) && (resultInteger <= maxValue)) {

                                            text = value;

                                        }

                                        else { validInput = false; }

                                    }

                                    else if (String.IsNullOrEmpty (value)) {

                                        text = String.Empty;

                                    }

                                    else { validInput = false; }

                                    break;

                            }

                            break;

                    }

                    #endregion

                }

                else { text = String.Empty; }


                if ((GetEventHandler ("TextChanged") != null) && (validInput)) {

                    if (validInput) { RaiseEvent ("TextChanged"); }

                }

                DataSourceChanged (); 

            }

        }


        public Mercury.Server.Core.Forms.Enumerations.FormControlTextMode TextMode { get { return textMode; } set { textMode = value; } }

        public Int32 Columns { get { return columns; } set { columns = value; } }

        public Int32 Rows { get { return rows; } set { rows = value; } }

        public Boolean Wrap { get { return wrap; } set { wrap = value; } }

        public Int32 MaxLength { get { return maxLength; } set { maxLength = value; } }

        public String EmptyMessage { get { return emptyMessage; } set { emptyMessage = value; } }

        public String Validation { get { return validation; } set { validation = value; } }

        public Mercury.Server.Core.Forms.Enumerations.FormControlSelectionOnFocus SelectionOnFocus { get { return selectionOnFocus; } set { selectionOnFocus = value; } }

        public String Mask { get { return mask; } set { mask = value; } }

        public Mercury.Server.Core.Forms.Enumerations.FormControlNumericType NumericType { get { return numericType; } set { numericType = value; } }

        public Double MinValue {

            get {

                if (minValue > (radNumericLimitation * -1)) { return minValue; }

                return (radNumericLimitation * -1);

            }

            set {

                if (value > (radNumericLimitation * -1)) { minValue = value; } else { minValue = (radNumericLimitation * -1); }

            }

        }

        public Double MaxValue {

            get {

                if (maxValue < radNumericLimitation) { return maxValue; }

                return radNumericLimitation;

            }

            set {

                if (value < radNumericLimitation) { maxValue = value; } else { maxValue = radNumericLimitation; }

            }

        }

        public Boolean ShowSpinButtons { get { return showSpinButtons; } set { showSpinButtons = value; } }

        public Mercury.Server.Core.Forms.Enumerations.FormControlSpinnerButtonPosition ButtonPosition { get { return buttonPosition; } set { buttonPosition = value; } }

        public Double NumericValue {

            get {

                Double numericValue = 0;

                if (!String.IsNullOrEmpty (text)) {

                    Double.TryParse (text, out numericValue);

                }

                return numericValue;

            }

        }


        public String DateFormat { get { return dateFormat; } set { dateFormat = value; } }

        public String DisplayDateFormat { get { return displayDateFormat; } set { displayDateFormat = value; } }

        public DateTime MinDate { get { return minDate; } set { minDate = value; } }

        public DateTime MaxDate { get { return maxDate; } set { maxDate = value; } }

        public DateTime? SelectedDate {

            get {

                DateTime? selectedDate = null;

                DateTime dateConversion;

                if (!String.IsNullOrEmpty (text)) {

                    if (DateTime.TryParse (text, out dateConversion)) {

                        selectedDate = dateConversion;

                    }

                }

                return selectedDate;

            }

        }


        public override System.Xml.XmlDocument ExtendedPropertiesXml {

            get {

                System.Xml.XmlDocument extendedProperties = base.ExtendedPropertiesXml;

                ExtendedProperties_AddProperty (extendedProperties, "InputType", ((Int32) inputType).ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "Text", text);

                ExtendedProperties_AddProperty (extendedProperties, "TextMode", ((Int32) textMode).ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "Columns", columns.ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "Rows", rows.ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "Wrap", wrap.ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "MaxLength", maxLength.ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "EmptyMessage", emptyMessage);

                ExtendedProperties_AddProperty (extendedProperties, "Validation", validation);


                ExtendedProperties_AddProperty (extendedProperties, "SelectionOnFocus", ((Int32) selectionOnFocus).ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "Mask", mask);

                ExtendedProperties_AddProperty (extendedProperties, "NumericType", ((Int32) numericType).ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "MinValue", minValue.ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "MaxValue", maxValue.ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "ShowSpinButtons", showSpinButtons.ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "ButtonPosition", ((Int32) buttonPosition).ToString ());


                ExtendedProperties_AddProperty (extendedProperties, "DateFormat", dateFormat);

                ExtendedProperties_AddProperty (extendedProperties, "DisplayDateFormat", displayDateFormat);

                ExtendedProperties_AddProperty (extendedProperties, "MinDate", minDate.ToString ("MM/dd/yyyy"));

                ExtendedProperties_AddProperty (extendedProperties, "MaxDate", maxDate.ToString ("MM/dd/yyyy"));

                return extendedProperties;

            }

        }

        public override void ExtendedPropertiesDeserialize (System.Xml.XmlNode extendedPropertiesXml) {

            base.ExtendedPropertiesDeserialize (extendedPropertiesXml);

            // foreach (System.Xml.XmlNode currentPropertyNode in extendedPropertiesXml.SelectNodes ("./Property")) {


            foreach (System.Xml.XmlNode currentPropertyNode in extendedPropertiesXml.ChildNodes) {

                switch (currentPropertyNode.Attributes["Name"].InnerText) {

                    case "InputType": inputType = (Mercury.Server.Core.Forms.Enumerations.FormControlInputType) Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "Text": text = currentPropertyNode.InnerText; break;

                    case "TextMode": textMode = (Mercury.Server.Core.Forms.Enumerations.FormControlTextMode) Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "Columns": columns = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "Rows": rows = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "Wrap": wrap = Convert.ToBoolean (currentPropertyNode.InnerText); break;

                    case "MaxLength": maxLength = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "EmptyMessage": emptyMessage = currentPropertyNode.InnerText; break;

                    case "Validation": validation = currentPropertyNode.InnerText; break;

                    case "SelectionOnFocus": selectionOnFocus = (Mercury.Server.Core.Forms.Enumerations.FormControlSelectionOnFocus) Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "Mask": mask = currentPropertyNode.InnerText; break;

                    case "NumericType": numericType = (Mercury.Server.Core.Forms.Enumerations.FormControlNumericType) Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "MinValue": MinValue = Convert.ToDouble (currentPropertyNode.InnerText); break;

                    case "MaxValue": MaxValue = Convert.ToDouble (currentPropertyNode.InnerText); break;

                    case "ShowSpinButtons": showSpinButtons = Convert.ToBoolean (currentPropertyNode.InnerText); break;

                    case "ButtonPosition": buttonPosition = (Mercury.Server.Core.Forms.Enumerations.FormControlSpinnerButtonPosition) Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "DateFormat": dateFormat = currentPropertyNode.InnerText; break;

                    case "DisplayDateFormat": displayDateFormat = currentPropertyNode.InnerText; break;

                    case "MinDate": DateTime.TryParse (currentPropertyNode.InnerText, out minDate); break;

                    case "MaxDate": DateTime.TryParse (currentPropertyNode.InnerText, out maxDate); break;
                    
                }

            }

            return;

        }


        public override System.Xml.XmlDocument ValuesXml {

            get {

                System.Xml.XmlDocument values = base.ValuesXml;

                Values_AddValue (values, "Text", text);

                return values;

            }

        }

        public override Boolean HasValue { get { return (!String.IsNullOrEmpty (text)); } }

        public override String Value { get { return (HasValue) ? text.ToString () : String.Empty; } }


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

            controlType = Mercury.Server.Core.Forms.Enumerations.FormControlType.Input;

            capabilities.HasValue = true;

            capabilities.HasLabel = true;

            capabilities.CanDataBind = true;

            capabilities.IsDataSource = true;

            label = new Label (applicationReference);

            return;

        }

        public Input (Application applicationReference) {

            InitializeControl (applicationReference);

            return;

        }

        public Input (Application applicationReference, String labelText) {

            InitializeControl (applicationReference);

            label = new Label (applicationReference, labelText);

            return;

        }

        #endregion


        #region Compile Methods

        public override List<CompileMessage> Compile () {

            List<CompileMessage> compileMessages = new List<CompileMessage> ();

            if ((label.Visible) && (String.IsNullOrEmpty (label.Text))) {

                compileMessages.Add (new CompileMessage (Mercury.Server.Core.Forms.Enumerations.FormCompileMessageType.Warning, "Label is set to visible without label text specified.", this));

            }

            compileMessages.AddRange (base.Compile ());

            return compileMessages;

        }

        #endregion


        #region Event Handlers

        public override List<String> Events {

            get {

                List<String> events = new List<String> ();

                events.Add ("TextChanged");

                return events;

            }

        }

        #endregion


        #region Data Bindings

        public override Dictionary<String, String> DataBindingContexts {

            get {

                Dictionary<String, String> bindingContexts = new Dictionary<String, String> ();

                bindingContexts.Add ("Text", "String");

                return bindingContexts;

            }

        }

        public override String EvaluateDataBinding (Structures.DataBinding dataBinding) {

            String dataValue = String.Empty;

            String bindingContextPart = dataBinding.BindingContext.Split ('.')[0];

            switch (bindingContextPart) {

                case "Text": dataValue = text; break;

                default: dataValue = "!Error"; break;

            }

            return dataValue;

        }

        public override Dictionary<String, String> DataBindableProperties {

            get {

                Dictionary<String, String> bindableProperties = new Dictionary<String, String> ();

                switch (inputType) {

                    case Mercury.Server.Core.Forms.Enumerations.FormControlInputType.Text: bindableProperties.Add ("Text", "String"); break;

                    case Mercury.Server.Core.Forms.Enumerations.FormControlInputType.DateTime: bindableProperties.Add ("Text", "DateTime"); break;

                    case Mercury.Server.Core.Forms.Enumerations.FormControlInputType.Numeric: bindableProperties.Add ("Text", "Numeric"); break;

                    default: bindableProperties.Add ("Text", "String"); break;

                }

                return bindableProperties;

            }

        }

        public override void OnDataSourceChanged (Control dataSourceControl, Boolean propogate) {

            base.OnDataSourceChanged (dataSourceControl, propogate);

            if (ContainsDataBinding (dataSourceControl.ControlId)) {

                foreach (Structures.DataBinding currentDataBinding in GetDataBindings (dataSourceControl.ControlId)) {

                    switch (currentDataBinding.BoundProperty) {

                        case "Text": Text = dataSourceControl.EvaluateDataBinding (currentDataBinding); break;

                        default: Text = "!Data Binding Error"; break;

                    }

                }

            }

            else if (dataSourceControl.ControlId == Form.ControlId) {

                foreach (Forms.Structures.DataBinding currentDataBinding in DataBindings) {

                    if (currentDataBinding.DataBindingType != Mercury.Server.Core.Forms.Enumerations.FormControlDataBindingType.Control) {

                        switch (currentDataBinding.BoundProperty) {

                            case "Text": Text = base.EvaluateDataBinding (currentDataBinding); break;

                            default: Text = "!Data Binding Error"; break;

                        }

                    }

                }

            }

        }

        #endregion


    }

}
