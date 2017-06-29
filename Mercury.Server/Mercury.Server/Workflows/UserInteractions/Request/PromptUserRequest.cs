using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Workflows.UserInteractions.Request {


    [Serializable]
    [DataContract (Name = "WorkflowUserInteractionRequestPromptUser")]
    public class PromptUserRequest : RequestBase {

        #region Private Properties

        [DataMember (Name = "PromptType")]
        private Enumerations.UserPromptType promptType = Mercury.Server.Workflows.UserInteractions.Enumerations.UserPromptType.ConfirmationOkCancel;

        [DataMember (Name = "PromptImage")]
        private Enumerations.UserPromptImage promptImage = Mercury.Server.Workflows.UserInteractions.Enumerations.UserPromptImage.NoImage;

        [DataMember (Name = "PromptTitle")]
        private String promptTitle = String.Empty;

        [DataMember (Name = "PromptMessage")]
        private String promptMessage = String.Empty;


        #region Input Properties

        private const Int64 radNumericLimitation = 70368744177663;

        //[DataMember (Name = "InputType")]
        //protected Mercury.Server.Core.Forms.Enumerations.FormControlInputType inputType = Mercury.Server.Core.Forms.Enumerations.FormControlInputType.Text;

        [DataMember (Name = "Text")]
        protected String text = String.Empty;

        //[DataMember (Name = "TextMode")]
        //protected Mercury.Server.Core.Forms.Enumerations.FormControlTextMode textMode = Mercury.Server.Core.Forms.Enumerations.FormControlTextMode.SingleLine;

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

        //[DataMember (Name = "SelectionOnFocus")]
        //protected Mercury.Server.Core.Forms.Enumerations.FormControlSelectionOnFocus selectionOnFocus = Mercury.Server.Core.Forms.Enumerations.FormControlSelectionOnFocus.None;

        [DataMember (Name = "Mask")]
        protected String mask = String.Empty;

        //[DataMember (Name = "NumericType")]
        //protected Mercury.Server.Core.Forms.Enumerations.FormControlNumericType numericType = Mercury.Server.Core.Forms.Enumerations.FormControlNumericType.Number;

        [DataMember (Name = "MinValue")]
        private Double minValue = (radNumericLimitation * -1);

        [DataMember (Name = "MaxValue")]
        private Double maxValue = (radNumericLimitation);

        [DataMember (Name = "ShowSpinButtons")]
        protected Boolean showSpinButtons = false;

        //[DataMember (Name = "ButtonPosition")]
        //protected Mercury.Server.Core.Forms.Enumerations.FormControlSpinnerButtonPosition buttonPosition = Mercury.Server.Core.Forms.Enumerations.FormControlSpinnerButtonPosition.Right;

        [DataMember (Name = "DateFormat")]
        protected String dateFormat = String.Empty;

        [DataMember (Name = "DisplayDateFormat")]
        protected String displayDateFormat = string.Empty;

        #endregion 


        [DataMember (Name = "SelectionItems")]
        private List<Structures.PromptSelectionItem> selectionItems = new List<Mercury.Server.Workflows.UserInteractions.Structures.PromptSelectionItem> ();

        [DataMember (Name = "SelectedValue")]
        private String selectedValue = String.Empty;

        #endregion 


        #region Public Properties

        public Enumerations.UserPromptType PromptType { get { return promptType; } set { promptType = value; } }

        public Enumerations.UserPromptImage PromptImage { get { return promptImage; } set { promptImage = value; } }

        public String PromptTitle { get { return promptTitle; } set { promptTitle = value; } }

        public String PromptMessage { get { return promptMessage; } set { promptMessage = value; } }


        //public Mercury.Server.Core.Forms.Enumerations.FormControlInputType InputType { get { return inputType; } set { inputType = value; } }

        //public String Text {

        //    get { return text; }

        //    set {

        //        if (text == value) { return; }


        //        Boolean validInput = true;

        //        switch (inputType) {

        //            case Mercury.Server.Core.Forms.Enumerations.FormControlInputType.Text:

        //                text = value;

        //                if (!String.IsNullOrEmpty (validation)) {

        //                    System.Text.RegularExpressions.Regex validator = new System.Text.RegularExpressions.Regex (validation);

        //                    validInput = validator.IsMatch (text);

        //                }

        //                break;

        //            case Mercury.Server.Core.Forms.Enumerations.FormControlInputType.DateTime:

        //                DateTime resultDateTime;

        //                if (DateTime.TryParse (value, out resultDateTime)) {

        //                    text = value;

        //                }

        //                else if (String.IsNullOrEmpty (value)) {

        //                    text = String.Empty;

        //                }

        //                else { validInput = false; }

        //                break;

        //            case Mercury.Server.Core.Forms.Enumerations.FormControlInputType.Numeric:

        //                switch (numericType) {

        //                    case Mercury.Server.Core.Forms.Enumerations.FormControlNumericType.Percent:

        //                    case Mercury.Server.Core.Forms.Enumerations.FormControlNumericType.Number:

        //                    case Mercury.Server.Core.Forms.Enumerations.FormControlNumericType.Currency:

        //                        Double resultDouble;

        //                        if (Double.TryParse (value, out resultDouble)) {

        //                            if ((resultDouble >= minValue) && (resultDouble <= maxValue)) {

        //                                text = value;
        //                            }

        //                            else { validInput = false; }

        //                        }

        //                        else { validInput = false; }

        //                        break;

        //                    case Mercury.Server.Core.Forms.Enumerations.FormControlNumericType.Integer:

        //                        Int64 resultInteger;

        //                        if (Int64.TryParse (value, out resultInteger)) {

        //                            if (Int64.TryParse (value, out resultInteger)) {

        //                                if ((resultInteger >= minValue) && (resultInteger <= maxValue)) {

        //                                    text = value;
        //                                }

        //                                else { validInput = false; }

        //                            }

        //                        }

        //                        else { validInput = false; }

        //                        break;

        //                }

        //                break;

        //        }

        //    }

        //}


        //public Mercury.Server.Core.Forms.Enumerations.FormControlTextMode TextMode { get { return textMode; } set { textMode = value; } }

        public Int32 Columns { get { return columns; } set { columns = value; } }

        public Int32 Rows { get { return rows; } set { rows = value; } }

        public Boolean Wrap { get { return wrap; } set { wrap = value; } }

        public Int32 MaxLength { get { return maxLength; } set { maxLength = value; } }

        public String EmptyMessage { get { return emptyMessage; } set { emptyMessage = value; } }

        public String Validation { get { return validation; } set { validation = value; } }

        //public Mercury.Server.Core.Forms.Enumerations.FormControlSelectionOnFocus SelectionOnFocus { get { return selectionOnFocus; } set { selectionOnFocus = value; } }

        public String Mask { get { return mask; } set { mask = value; } }

        //public Mercury.Server.Core.Forms.Enumerations.FormControlNumericType NumericType { get { return numericType; } set { numericType = value; } }

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

        //public Mercury.Server.Core.Forms.Enumerations.FormControlSpinnerButtonPosition ButtonPosition { get { return buttonPosition; } set { buttonPosition = value; } }

        public String DateFormat { get { return dateFormat; } set { dateFormat = value; } }

        public String DisplayDateFormat { get { return displayDateFormat; } set { displayDateFormat = value; } }


        public List<Structures.PromptSelectionItem> SelectionItems { get { return selectionItems; } set { selectionItems = value; } }

        public String SelectedValue { get { return selectedValue; } set { selectedValue = value; } }

        #endregion 


        #region Constructors

        public PromptUserRequest (Enumerations.UserPromptType forPromptyType, Enumerations.UserPromptImage forPromptImage, String forPromptTitle, String forPromptMessage) {

            base.userInteractionType = Mercury.Server.Workflows.UserInteractions.Enumerations.UserInteractionType.Prompt;

            promptType = forPromptyType;

            promptImage = forPromptImage;

            promptTitle = forPromptTitle;

            promptMessage = forPromptMessage;

            Message = promptTitle;

            return;

        }

        #endregion 


    }

}
