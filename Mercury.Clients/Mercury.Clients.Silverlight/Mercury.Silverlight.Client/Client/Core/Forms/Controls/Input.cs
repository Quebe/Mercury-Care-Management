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

    public class Input : Control {


        #region Private Properties

        private const Int64 radNumericLimitation = 70368744177663;

        private Server.Application.FormControlInputType inputType = Server.Application.FormControlInputType.Text;

        private String text = String.Empty;

        private Server.Application.FormControlTextMode textMode = Server.Application.FormControlTextMode.SingleLine;

        private Int32 columns = 40;

        private Int32 rows = 1;

        private Boolean wrap = false;

        private Int32 maxLength = 8000;

        private String emptyMessage = String.Empty;

        private String validation = String.Empty;


        private Server.Application.FormControlSelectionOnFocus selectionOnFocus = Server.Application.FormControlSelectionOnFocus.None;

        private String mask = String.Empty;

        // Numeric Specific Functions

        private Server.Application.FormControlNumericType numericType = Server.Application.FormControlNumericType.Number;

        private Double minValue = (radNumericLimitation * -1);

        private Double maxValue = (radNumericLimitation);

        private Boolean showSpinButtons = false;

        private Server.Application.FormControlSpinnerButtonPosition buttonPosition = Server.Application.FormControlSpinnerButtonPosition.Right;

        // Date Specific Functions

        private String dateFormat = "MM/dd/yyyy";

        private String displayDateFormat = "MM/dd/yyyy";

        private DateTime minDate = new DateTime (1900, 01, 01);

        private DateTime maxDate = new DateTime (2999, 12, 31);

        #endregion


        #region Public Properties

        public Server.Application.FormControlInputType InputType { 
            
            get { return inputType; } 
            
            set {

                if (inputType != value) {

                    inputType = value;

                    NotifyPropertyChanged ("InputType");

                }
            
            } 
        
        }

        public String Text {

            get { return text; }

            set {

                if (text == value) { return; }

                Boolean validInput = true;

                switch (inputType) {

                    case Server.Application.FormControlInputType.Text:

                        if (!String.IsNullOrEmpty (validation)) {

                            System.Text.RegularExpressions.Regex validator = new System.Text.RegularExpressions.Regex (validation);

                            validInput = validator.IsMatch (text);

                        }

                        text = value;

                        break;

                    case Server.Application.FormControlInputType.DateTime:

                        DateTime resultDateTime;

                        if (DateTime.TryParse (value, out resultDateTime)) {

                            text = value;

                        }

                        else if (String.IsNullOrEmpty (value)) {

                            text = String.Empty;

                        }

                        else { validInput = false; }

                        break;

                    case Server.Application.FormControlInputType.Numeric:

                        switch (numericType) {

                            case Server.Application.FormControlNumericType.Percent:

                            case Server.Application.FormControlNumericType.Number:

                            case Server.Application.FormControlNumericType.Currency:

                                Double resultDouble;

                                if (Double.TryParse (value, out resultDouble)) {

                                    if ((resultDouble >= minValue) && (resultDouble <= maxValue)) {

                                        text = value;
                                    }

                                    else { validInput = false; }

                                }

                                else { validInput = false; }

                                break;

                            case Server.Application.FormControlNumericType.Integer:

                                Int64 resultInteger;

                                if (Int64.TryParse (value, out resultInteger)) {

                                    if ((resultInteger >= minValue) && (resultInteger <= maxValue)) {

                                        text = value;
                                    }

                                    else { validInput = false; }

                                }

                                else { validInput = false; }

                                break;

                        }

                        break;

                }

                if (validInput) {

                    NotifyPropertyChanged ("Text");

                    NotifyPropertyChanged ("HasValue");

                    NotifyPropertyChanged ("Value");

                }

                //if ((GetEventHandler ("TextChanged") != null) && (validInput)) {

                //    RaiseEvent ("TextChanged");

                //    DataSourceChanged ();

                //}

            }

        }

        public Server.Application.FormControlTextMode TextMode { 
            
            get { return textMode; } 
            
            set { 
                
                if (textMode != value) {
                    
                    textMode = value; 

                    NotifyPropertyChanged ("TextMode");

                    NotifyPropertyChanged ("SlAcceptsReturns");

                    NotifyPropertyChanged ("SlVerticalScrollBarVisibility");

                }

            } 
        
        }

        public Int32 Columns {

            get { return columns; }

            set {

                if (columns != value) {

                    columns = value;

                    NotifyPropertyChanged ("TextBoxHeight");

                }

            }

        }

        public Int32 Rows { get { return rows; } set { rows = value; } }

        public Boolean Wrap { get { return wrap; } set { wrap = value; } }

        public Int32 MaxLength { 
            
            get { 
                
                return maxLength; 
            
            } 
            
            set {

                if (maxLength != value) {

                    maxLength = value;

                    NotifyPropertyChanged ("MaxLength");

                }
            
            } 
        
        }

        public String EmptyMessage { get { return emptyMessage; } set { emptyMessage = value; } }

        public String Validation { get { return validation; } set { validation = value; } }


        public Server.Application.FormControlSelectionOnFocus SelectionOnFocus { get { return selectionOnFocus; } set { selectionOnFocus = value; } }

        public String Mask { get { return mask; } set { mask = value; } }


        public Server.Application.FormControlNumericType NumericType { get { return numericType; } set { numericType = value; } }

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

        public Server.Application.FormControlSpinnerButtonPosition ButtonPosition { get { return buttonPosition; } set { buttonPosition = value; } }

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

        #endregion


        #region Silverlight Public Properties

        public Boolean SlEnabledAndNotReadOnly { get { return ((enabled) && (!readOnly)); } }

        public Boolean SlAcceptsReturns { get { return (textMode == Server.Application.FormControlTextMode.MultiLine); } }

        public ScrollBarVisibility SlVerticalScrollBarVisibility { get { return (SlAcceptsReturns) ? ScrollBarVisibility.Visible : ScrollBarVisibility.Hidden; } }

        #endregion 


        #region Constructors

        virtual protected void InitializeControl (Application applicationReference) {

            BaseConstructor (applicationReference);

            ControlType = Server.Application.FormControlType.Input;

            capabilities.HasValue = true;

            capabilities.HasLabel = true;

            capabilities.CanDataBind = true;

            capabilities.IsDataSource = true;

            label = new Label (applicationReference, this);

            return;

        }

        override public void BaseConstructor (Mercury.Client.Core.Forms.Control parentControl, Server.Application.FormControl serverControl) {

            base.BaseConstructor (parentControl, serverControl);


            Server.Application.FormControlInput serverInput = (Server.Application.FormControlInput) serverControl;

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

        public Input (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Server.Application.FormControlInput serverInput) {

            InitializeControl (applicationReference);

            BaseConstructor (parentControl, serverInput);

            ChildServerControlsToLocal (this, serverInput);

        }

        #endregion


        #region Virtual Overrides

        public override void LocalControlToServer (Server.Application.FormControl parentControl, Server.Application.FormControl serverControl) {

            base.LocalControlToServer (parentControl, serverControl);

            ((Server.Application.FormControlInput) serverControl).InputType = inputType;

            ((Server.Application.FormControlInput) serverControl).Text = text;

            ((Server.Application.FormControlInput) serverControl).TextMode = textMode;

            ((Server.Application.FormControlInput) serverControl).Columns = columns;

            ((Server.Application.FormControlInput) serverControl).Rows = rows;

            ((Server.Application.FormControlInput) serverControl).Wrap = wrap;

            ((Server.Application.FormControlInput) serverControl).MaxLength = maxLength;

            ((Server.Application.FormControlInput) serverControl).EmptyMessage = emptyMessage;

            ((Server.Application.FormControlInput) serverControl).Validation = validation;

            ((Server.Application.FormControlInput) serverControl).ReadOnly = readOnly;

            ((Server.Application.FormControlInput) serverControl).SelectionOnFocus = selectionOnFocus;

            ((Server.Application.FormControlInput) serverControl).Mask = mask;

            ((Server.Application.FormControlInput) serverControl).NumericType = numericType;

            ((Server.Application.FormControlInput) serverControl).MinValue = minValue;

            ((Server.Application.FormControlInput) serverControl).MaxValue = maxValue;

            ((Server.Application.FormControlInput) serverControl).ShowSpinButtons = showSpinButtons;

            ((Server.Application.FormControlInput) serverControl).ButtonPosition = buttonPosition;

            ((Server.Application.FormControlInput) serverControl).DateFormat = dateFormat;

            ((Server.Application.FormControlInput) serverControl).DisplayDateFormat = displayDateFormat;

            ((Server.Application.FormControlInput) serverControl).MinDate = minDate;

            ((Server.Application.FormControlInput) serverControl).MaxDate = maxDate;


            ((Server.Application.FormControlInput) serverControl).Label = new Server.Application.FormControlLabel ();

            label.LocalControlToServer (serverControl, ((Server.Application.FormControlInput) serverControl).Label);

            return;

        }

        #endregion

    }

}
