using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Forms.Controls {

    [Serializable]
    public class Input : Mercury.Client.Core.Forms.Control {

        #region Private Properties

        private const Int64 radNumericLimitation = 70368744177663;

        private Mercury.Server.Application.FormControlInputType inputType = Mercury.Server.Application.FormControlInputType.Text;

        private String text = String.Empty;

        private Mercury.Server.Application.FormControlTextMode textMode = Mercury.Server.Application.FormControlTextMode.SingleLine;

        private Int32 columns = 40;

        private Int32 rows = 1;

        private Boolean wrap = false;

        private Int32 maxLength = 8000;

        private String emptyMessage = String.Empty;

        private String validation = String.Empty;


        private Mercury.Server.Application.FormControlSelectionOnFocus selectionOnFocus = Mercury.Server.Application.FormControlSelectionOnFocus.None;

        private String mask = String.Empty;

        // Numeric Specific Functions

        private Mercury.Server.Application.FormControlNumericType numericType = Mercury.Server.Application.FormControlNumericType.Number;

        private Double minValue = (radNumericLimitation * -1);

        private Double maxValue = (radNumericLimitation);

        private Boolean showSpinButtons = false;

        private Mercury.Server.Application.FormControlSpinnerButtonPosition buttonPosition = Mercury.Server.Application.FormControlSpinnerButtonPosition.Right;

        // Date Specific Functions

        private String dateFormat = "MM/dd/yyyy";

        private String displayDateFormat = "MM/dd/yyyy";

        private DateTime minDate = new DateTime (1900, 01, 01);

        private DateTime maxDate = new DateTime (2999, 12, 31);

        #endregion


        #region Public Properties

        public Mercury.Server.Application.FormControlInputType InputType { get { return inputType; } set { inputType = value; } }

        public String Text {

            get { return text; }

            set {

                if (text == value) { return; }

                Boolean validInput = true;


                if (!String.IsNullOrEmpty (value)) {

                    #region Specific Value, Determine if Valid by Input Type

                    switch (inputType) {

                        case Mercury.Server.Application.FormControlInputType.Text:

                            if (!String.IsNullOrEmpty (validation)) {

                                System.Text.RegularExpressions.Regex validator = new System.Text.RegularExpressions.Regex (validation);

                                validInput = validator.IsMatch (text);

                            }

                            text = value;

                            break;

                        case Mercury.Server.Application.FormControlInputType.DateTime:

                            DateTime resultDateTime;

                            if (DateTime.TryParse (value, out resultDateTime)) {

                                text = value;

                            }

                            else if (String.IsNullOrEmpty (value)) {

                                text = String.Empty;

                            }

                            else { validInput = false; }

                            break;

                        case Mercury.Server.Application.FormControlInputType.Numeric:

                            switch (numericType) {

                                case Mercury.Server.Application.FormControlNumericType.Percent:

                                case Mercury.Server.Application.FormControlNumericType.Number:

                                case Mercury.Server.Application.FormControlNumericType.Currency:

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

                                case Mercury.Server.Application.FormControlNumericType.Integer:

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
                    
                    RaiseEvent ("TextChanged");

                    DataSourceChanged ();

                }

            }

        }

        public Mercury.Server.Application.FormControlTextMode TextMode { get { return textMode; } set { textMode = value; } }

        public Int32 Columns { get { return columns; } set { columns = value; } }

        public Int32 Rows { get { return rows; } set { rows = value; } }

        public Boolean Wrap { get { return wrap; } set { wrap = value; } }

        public Int32 MaxLength { get { return maxLength; } set { maxLength = value; } }

        public String EmptyMessage { get { return emptyMessage; } set { emptyMessage = value; } }

        public String Validation { get { return validation; } set { validation = value; } }


        public Mercury.Server.Application.FormControlSelectionOnFocus SelectionOnFocus { get { return selectionOnFocus; } set { selectionOnFocus = value; } }

        public String Mask { get { return mask; } set { mask = value; } }

        public Mercury.Server.Application.FormControlNumericType NumericType { get { return numericType; } set { numericType = value; } }

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

        public Mercury.Server.Application.FormControlSpinnerButtonPosition ButtonPosition { get { return buttonPosition; } set { buttonPosition = value; } }

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


        public override Boolean HasValue { get { return (!String.IsNullOrEmpty (text)); } }

        public override String Value { get { return (HasValue) ? text.ToString () : String.Empty; } }


        public override String JsonExtendedProperties {

            get {

                StringBuilder jsonBuilder = new StringBuilder ();

                jsonBuilder.Append (", " + JsonObjectProperty ("InputType", Convert.ToInt32 (inputType)));

                jsonBuilder.Append (", " + JsonObjectProperty ("Text", text));

                if (!String.IsNullOrWhiteSpace (emptyMessage)) { jsonBuilder.Append (", " + JsonObjectProperty ("EmptyMessage", emptyMessage)); }

                jsonBuilder.Append (", " + JsonObjectProperty ("SelectionOnFocus", Convert.ToInt32 (selectionOnFocus)));


                if (inputType == Server.Application.FormControlInputType.Text) {

                    if (textMode != Server.Application.FormControlTextMode.SingleLine) { jsonBuilder.Append (", " + JsonObjectProperty ("TextMode", Convert.ToInt32 (textMode))); }

                    if (maxLength != 8000) { jsonBuilder.Append (", " + JsonObjectProperty ("MaxLength", maxLength)); }

                    if (!String.IsNullOrWhiteSpace (validation)) { jsonBuilder.Append (", " + JsonObjectProperty ("Validation", validation)); }

                    if (!String.IsNullOrWhiteSpace (mask)) { jsonBuilder.Append (", " + JsonObjectProperty ("Mask", mask)); }

                    if (textMode == Server.Application.FormControlTextMode.MultiLine) {

                        jsonBuilder.Append (", " + JsonObjectProperty ("Columns", columns));

                        jsonBuilder.Append (", " + JsonObjectProperty ("Rows", rows));

                        jsonBuilder.Append (", " + JsonObjectProperty ("Wrap", wrap));

                    }

                }

                if (inputType == Server.Application.FormControlInputType.Numeric) {

                    jsonBuilder.Append (", " + JsonObjectProperty ("NumericType", Convert.ToInt32 (numericType)));

                    jsonBuilder.Append (", " + JsonObjectProperty ("MinValue", minValue));

                    jsonBuilder.Append (", " + JsonObjectProperty ("MaxValue", maxValue));

                    jsonBuilder.Append (", " + JsonObjectProperty ("ShowSpinButtons", showSpinButtons));

                    jsonBuilder.Append (", " + JsonObjectProperty ("ButtonPosition", Convert.ToInt32 (buttonPosition)));

                }

                if (inputType == Server.Application.FormControlInputType.DateTime) {

                    jsonBuilder.Append (", " + JsonObjectProperty ("DateFormat", dateFormat));

                    jsonBuilder.Append (", " + JsonObjectProperty ("DisplayDateFormat", displayDateFormat));

                    jsonBuilder.Append (", " + JsonObjectProperty ("MinDate", minDate));

                    jsonBuilder.Append (", " + JsonObjectProperty ("MaxDate", maxDate));

                }


                return jsonBuilder.ToString ();

            }

        }

        #endregion


        #region Constructors

        virtual protected void InitializeControl (Application applicationReference) {

            BaseConstructor (applicationReference);

            ControlType = Mercury.Server.Application.FormControlType.Input;

            capabilities.HasValue = true;

            capabilities.HasLabel = true;

            capabilities.CanDataBind = true;

            capabilities.IsDataSource = true;

            label = new Label (applicationReference, this);            

            return;

        }

        override public void BaseConstructor (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Mercury.Server.Application.FormControl serverControl) {

            base.BaseConstructor (applicationReference, parentControl, serverControl);


            Mercury.Server.Application.FormControlInput serverInput = (Mercury.Server.Application.FormControlInput) serverControl;

            inputType = serverInput.InputType; 

            text = serverInput.Text;

            textMode = serverInput.TextMode;

            columns = serverInput.Columns;

            rows = serverInput.Rows;

            wrap = serverInput.Wrap;

            maxLength = serverInput.MaxLength;

            emptyMessage = serverInput.EmptyMessage;

            validation = serverInput.Validation;

            readOnly = serverInput.ReadOnly;

            selectionOnFocus = serverInput.SelectionOnFocus;

            mask = serverInput.Mask;

            numericType = serverInput.NumericType;

            minValue = serverInput.MinValue;

            maxValue = serverInput.MaxValue; 

            showSpinButtons = serverInput.ShowSpinButtons; 

            buttonPosition = serverInput.ButtonPosition;

            dateFormat = serverInput.DateFormat;

            displayDateFormat = serverInput.DisplayDateFormat;

            minDate = serverInput.MinDate;

            maxDate = serverInput.MaxDate;


            label = new Label (Application, this, serverInput.Label);

            return;

        }


        public Input (Application applicationReference) {

            InitializeControl (applicationReference);

        }

        public Input (Application applicationReference, String labelText) {

            InitializeControl (applicationReference);

            label.Text = labelText;

        }

        public Input (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Mercury.Server.Application.FormControlInput serverInput) {

            InitializeControl (applicationReference);

            BaseConstructor (applicationReference, parentControl, serverInput);

            ChildServerControlsToLocal (this, serverInput);

        }

        #endregion


        #region Virtual Overrides

        public override void LocalControlToServer (Mercury.Server.Application.FormControl parentControl, Mercury.Server.Application.FormControl serverControl) {

            base.LocalControlToServer (parentControl, serverControl);

            ((Mercury.Server.Application.FormControlInput) serverControl).InputType = inputType;

            ((Mercury.Server.Application.FormControlInput) serverControl).Text = text;

            ((Mercury.Server.Application.FormControlInput) serverControl).TextMode = textMode;

            ((Mercury.Server.Application.FormControlInput) serverControl).Columns = columns;

            ((Mercury.Server.Application.FormControlInput) serverControl).Rows = rows;

            ((Mercury.Server.Application.FormControlInput) serverControl).Wrap = wrap;

            ((Mercury.Server.Application.FormControlInput) serverControl).MaxLength = maxLength;

            ((Mercury.Server.Application.FormControlInput) serverControl).EmptyMessage = emptyMessage;

            ((Mercury.Server.Application.FormControlInput) serverControl).Validation = validation;

            ((Mercury.Server.Application.FormControlInput) serverControl).ReadOnly = readOnly;

            ((Mercury.Server.Application.FormControlInput) serverControl).SelectionOnFocus = selectionOnFocus;

            ((Mercury.Server.Application.FormControlInput) serverControl).Mask = mask;

            ((Mercury.Server.Application.FormControlInput) serverControl).NumericType = numericType;

            ((Mercury.Server.Application.FormControlInput) serverControl).MinValue = minValue;

            ((Mercury.Server.Application.FormControlInput) serverControl).MaxValue = maxValue;

            ((Mercury.Server.Application.FormControlInput) serverControl).ShowSpinButtons = showSpinButtons;

            ((Mercury.Server.Application.FormControlInput) serverControl).ButtonPosition = buttonPosition;

            ((Mercury.Server.Application.FormControlInput) serverControl).DateFormat = dateFormat;

            ((Mercury.Server.Application.FormControlInput) serverControl).DisplayDateFormat = displayDateFormat;

            ((Mercury.Server.Application.FormControlInput) serverControl).MinDate = minDate;

            ((Mercury.Server.Application.FormControlInput) serverControl).MaxDate = maxDate;

            ((Mercury.Server.Application.FormControlInput) serverControl).Label = new Mercury.Server.Application.FormControlLabel ();

            label.LocalControlToServer (serverControl, ((Mercury.Server.Application.FormControlInput) serverControl).Label);

            return;

        }

        #endregion

    }

}
