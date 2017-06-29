using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Forms.Controls {

    [Serializable]
    public class Selection : Mercury.Client.Core.Forms.Control {

        #region Private Properties

        protected Mercury.Server.Application.FormControlSelectionType selectionType = Mercury.Server.Application.FormControlSelectionType.DropDownList;


        protected Int32 columns = 1;

        protected Int32 rows = 4;

        protected Mercury.Server.Application.FormControlSelectionDirection direction = Mercury.Server.Application.FormControlSelectionDirection.Vertical;

        protected Boolean wrap = false;         // Drop Down List

        protected Int32 maxLength = 8000;       // Drop Down List


        protected Mercury.Server.Application.FormControlSelectionMode selectionMode = Mercury.Server.Application.FormControlSelectionMode.Single;

        protected Mercury.Server.Application.FormControlDataSource dataSource = Mercury.Server.Application.FormControlDataSource.ItemList;

        protected Mercury.Server.Application.FormControlSelectionReferenceSource referenceSource = Mercury.Server.Application.FormControlSelectionReferenceSource.Program;

        protected List<Structures.SelectionItem> items = new List<Mercury.Client.Core.Forms.Structures.SelectionItem> ();
        

        private Boolean allowCustomText = false;
        
        private String customText = String.Empty;

        #endregion


        #region Public Properties

        public Mercury.Server.Application.FormControlSelectionType SelectionType { 
            
            get { return selectionType; } 
            
            set { 
                
                selectionType = value;

                switch (selectionType) {

                    case Mercury.Server.Application.FormControlSelectionType.ListBox:

                        break;

                    case Mercury.Server.Application.FormControlSelectionType.DropDownList:

                        selectionMode = Mercury.Server.Application.FormControlSelectionMode.Single;

                        break;

                    case Mercury.Server.Application.FormControlSelectionType.CheckBox:

                        selectionMode = Mercury.Server.Application.FormControlSelectionMode.Multiple;

                        break;

                    case Mercury.Server.Application.FormControlSelectionType.RadioButton:

                        selectionMode = Mercury.Server.Application.FormControlSelectionMode.Single;

                        break;

                }
           
            } 
        
        }


        public Int32 Columns { get { return columns; } set { columns = value; } }

        public Int32 Rows { get { return rows; } set { rows = value; } }

        public Mercury.Server.Application.FormControlSelectionDirection Direction { get { return direction; } set { direction = value; } }

        public Boolean Wrap { get { return wrap; } set { wrap = value; } }

        public Int32 MaxLength { get { return maxLength; } set { maxLength = value; } }


        public Mercury.Server.Application.FormControlSelectionMode SelectionMode { get { return selectionMode; } set { selectionMode = value; } }


        public Mercury.Server.Application.FormControlDataSource DataSource { get { return dataSource; } set { dataSource = value; bindableProperties = null; } }

        public Mercury.Server.Application.FormControlSelectionReferenceSource ReferenceSource { get { return referenceSource; } set { referenceSource = value; bindableProperties = null; } }

        public List<Structures.SelectionItem> Items { get { return items; } set { items = value; } }


        public Boolean AllowCustomText { get { return allowCustomText; } set { allowCustomText = value; } }

        public String CustomText {

            get { return customText; }

            set {

                if ((allowCustomText) && (customText != value)) {

                    customText = value;

                    foreach (Structures.SelectionItem currentItem in items) {

                        currentItem.Selected = false;

                        RaiseEvent ("SelectionChanged");

                        DataSourceChanged ();

                    }

                }

            }

        }


        public Structures.SelectionItem SelectedItem {

            get {

                Structures.SelectionItem selectedItem = null;

                foreach (Structures.SelectionItem currentItem in items) {

                    if (currentItem.Selected) {

                        selectedItem = currentItem;

                        break;

                    }

                }

                return selectedItem;

            }

        }

        public String SelectedValue {

            get {

                String selectedValue = String.Empty;

                if ((selectionType == Mercury.Server.Application.FormControlSelectionType.DropDownList)

                    && (allowCustomText) && (!String.IsNullOrEmpty (customText))) {

                    selectedValue = customText;

                }

                else {

                    foreach (Core.Forms.Structures.SelectionItem currentItem in items) {

                        if (currentItem.Selected) {

                            selectedValue = currentItem.Value;

                            break;

                        }

                    }

                }

                return selectedValue;

            }

        }

        public Boolean HasCustomTextValue { get { return ((allowCustomText) && (!String.IsNullOrEmpty (customText))); } }

        public override Boolean HasValue {

            get {

                switch (selectionType) {

                    case Mercury.Server.Application.FormControlSelectionType.DropDownList:

                    case Mercury.Server.Application.FormControlSelectionType.ListBox:

                    case Mercury.Server.Application.FormControlSelectionType.CheckBox:

                    case Mercury.Server.Application.FormControlSelectionType.RadioButton:

                        if ((selectionType == Mercury.Server.Application.FormControlSelectionType.DropDownList)

                            && (allowCustomText) && (!String.IsNullOrEmpty (customText))) { return true; }


                        foreach (Core.Forms.Structures.SelectionItem currentItem in items) {

                            if (currentItem.Selected) { return !String.IsNullOrEmpty (currentItem.Value); }

                        }

                        return false;

                    default: return false;

                }

            }

        }

        public override string Value { get { return (HasValue) ? SelectedValue : String.Empty; } }
        

        public override String JsonExtendedProperties {

            get {

                StringBuilder jsonBuilder = new StringBuilder ();

                jsonBuilder.Append (", " + JsonObjectProperty ("SelectionType", Convert.ToInt32 (selectionType)));

                if ((selectionType == Server.Application.FormControlSelectionType.CheckBox) || (selectionType == Server.Application.FormControlSelectionType.RadioButton)) {

                    jsonBuilder.Append (", " + JsonObjectProperty ("Columns", columns));

                    jsonBuilder.Append (", " + JsonObjectProperty ("Rows", rows));

                    if (direction != Server.Application.FormControlSelectionDirection.Horizontal) { jsonBuilder.Append (", " + JsonObjectProperty ("Direction", Convert.ToInt32 (direction))); }

                }

                if (selectionMode != Server.Application.FormControlSelectionMode.Single) { jsonBuilder.Append (", " + JsonObjectProperty ("SelectionMode", Convert.ToInt32 (selectionMode))); }


                if (dataSource != Server.Application.FormControlDataSource.ItemList) {

                    jsonBuilder.Append (", " + JsonObjectProperty ("DataSource", Convert.ToInt32 (dataSource)));

                    jsonBuilder.Append (", " + JsonObjectProperty ("ReferenceSource", Convert.ToInt32 (referenceSource)));

                }

                if (allowCustomText) { 
                    
                    jsonBuilder.Append (", " + JsonObjectProperty ("AllowCustomText", allowCustomText));

                    jsonBuilder.Append (", " + JsonObjectProperty ("CustomText", customText));

                    if (maxLength != 8000) { jsonBuilder.Append (", " + JsonObjectProperty ("MaxLength", maxLength)); }

                    jsonBuilder.Append (", " + JsonObjectProperty ("Wrap", wrap));

                }


                Boolean isFirstItem = true;

                jsonBuilder.Append (", \"Items\": [");

                foreach (Structures.SelectionItem currentSelectionItem in items) {

                    if (!isFirstItem) { jsonBuilder.Append (", "); }

                    else { isFirstItem = false; }

                    jsonBuilder.Append (" { ");

                    jsonBuilder.Append (JsonObjectProperty ("Text", currentSelectionItem.Text));

                    jsonBuilder.Append (", " + JsonObjectProperty ("Value", currentSelectionItem.Value));

                    if (!currentSelectionItem.Enabled) { jsonBuilder.Append (", " + JsonObjectProperty ("Enabled", currentSelectionItem.Enabled)); }

                    if (currentSelectionItem.Selected) { jsonBuilder.Append (", " + JsonObjectProperty ("Selected", currentSelectionItem.Selected)); }

                    jsonBuilder.Append (" }");

                }

                jsonBuilder.Append ("]");


                return jsonBuilder.ToString ();

            }

        }

        #endregion


        #region Constructors

        virtual protected void InitializeControl (Application applicationReference) {

            BaseConstructor (applicationReference);

            ControlType = Mercury.Server.Application.FormControlType.Selection;

            capabilities.HasValue = true;

            capabilities.HasLabel = true;

            capabilities.IsDataSource = true;

            capabilities.CanDataBind = true;

            label = new Label (Application, this);

            return;

        }

        override public void BaseConstructor (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Mercury.Server.Application.FormControl serverControl) {

            base.BaseConstructor (applicationReference, parentControl, serverControl);


            Mercury.Server.Application.FormControlSelection serverSelection = (Mercury.Server.Application.FormControlSelection) serverControl;

            selectionType = serverSelection.SelectionType;


            columns = serverSelection.Columns;

            rows = serverSelection.Rows;

            direction = serverSelection.Direction;

            wrap = serverSelection.Wrap;

            maxLength = serverSelection.MaxLength;

            readOnly = serverSelection.ReadOnly;

            selectionMode = serverSelection.SelectionMode;


            dataSource = serverSelection.DataSource;

            referenceSource = serverSelection.ReferenceSource;


            allowCustomText = serverSelection.AllowCustomText;

            customText = serverSelection.CustomText;


            items = new List<Mercury.Client.Core.Forms.Structures.SelectionItem> ();

            foreach (Mercury.Server.Application.FormControlSelectionItem currentServerItem in serverSelection.Items) {

                Structures.SelectionItem selectionItem = new Mercury.Client.Core.Forms.Structures.SelectionItem (currentServerItem);

                items.Add (selectionItem);

            }

            //items = new List<Mercury.Server.Application.FormControlSelectionItem> ();

            //items.AddRange (serverSelection.Items);
           

            label = new Label (Application, this, serverSelection.Label);

            return;

        }


        public Selection (Application applicationReference) {

            InitializeControl (applicationReference);

            return;

        }

        public Selection (Application applicationReference, String labelText) {

            InitializeControl (applicationReference);

            label.Text = labelText;

            return;

        }

        public Selection (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Mercury.Server.Application.FormControlSelection serverSelection) {

            InitializeControl (applicationReference);

            BaseConstructor (applicationReference, parentControl, serverSelection);

            ChildServerControlsToLocal (this, serverSelection);

            return;

        }

        #endregion


        #region Virtual Overrides

        public override void LocalControlToServer (Mercury.Server.Application.FormControl parentControl, Mercury.Server.Application.FormControl serverControl) {

            base.LocalControlToServer (parentControl, serverControl);

            ((Mercury.Server.Application.FormControlSelection) serverControl).SelectionType = selectionType;


            ((Mercury.Server.Application.FormControlSelection) serverControl).Columns = columns;

            ((Mercury.Server.Application.FormControlSelection) serverControl).Rows = rows;

            ((Mercury.Server.Application.FormControlSelection) serverControl).Direction = direction;

            ((Mercury.Server.Application.FormControlSelection) serverControl).Wrap = wrap;

            ((Mercury.Server.Application.FormControlSelection) serverControl).MaxLength = maxLength;

            ((Mercury.Server.Application.FormControlSelection) serverControl).ReadOnly = readOnly;

            ((Mercury.Server.Application.FormControlSelection) serverControl).SelectionMode = selectionMode;

            ((Mercury.Server.Application.FormControlSelection) serverControl).DataSource = dataSource;

            ((Mercury.Server.Application.FormControlSelection) serverControl).ReferenceSource = referenceSource;


            ((Mercury.Server.Application.FormControlSelection) serverControl).AllowCustomText = allowCustomText;

            ((Mercury.Server.Application.FormControlSelection) serverControl).CustomText = customText;


            ((Mercury.Server.Application.FormControlSelection) serverControl).Items = new Mercury.Server.Application.FormControlSelectionItem[items.Count];

            Int32 currentItemIndex = 0;

            foreach (Structures.SelectionItem currentItem in items) {

                ((Mercury.Server.Application.FormControlSelection) serverControl).Items[currentItemIndex] = currentItem.ToServerItem ();

                currentItemIndex = currentItemIndex + 1;

            }

            //foreach (Mercury.Server.Application.FormControlSelectionItem currentItem in items) {

            //    Mercury.Server.Application.FormControlSelectionItem copiedItem = new Mercury.Server.Application.FormControlSelectionItem ();

            //    copiedItem.Text = currentItem.Text;

            //    copiedItem.Value = currentItem.Value;

            //    copiedItem.Enabled = currentItem.Enabled;

            //    copiedItem.Selected = currentItem.Selected;

            //    ((Mercury.Server.Application.FormControlSelection) serverControl).Items[currentItemIndex] = copiedItem;

            //    currentItemIndex = currentItemIndex + 1;

            //}

            ((Mercury.Server.Application.FormControlSelection) serverControl).Label = new Mercury.Server.Application.FormControlLabel ();

            label.LocalControlToServer (serverControl, ((Mercury.Server.Application.FormControlSelection) serverControl).Label);

            return;

        }

        #endregion


        #region Public Methods

        public Structures.SelectionItem Item (String value) {

            Structures.SelectionItem item = null;

            for (Int32 currentItemIndex = 0; currentItemIndex < items.Count; currentItemIndex++) {

                Structures.SelectionItem currentItem = (Structures.SelectionItem) items[currentItemIndex];

                if (currentItem.Value == value) {

                    item = currentItem;

                    break;

                }

            }

            return item;

        }

        public Boolean ItemExists (String value) {

            Boolean itemFound = false;

            for (Int32 currentItemIndex = 0; currentItemIndex < items.Count; currentItemIndex++) {

                Structures.SelectionItem currentItem = (Structures.SelectionItem) items[currentItemIndex];

                if (currentItem.Value == value) {

                    itemFound = true;

                    break;

                }

            }

            return itemFound;

        }

        public Boolean ItemSelected (String value) {

            Boolean itemSelected = false;

            for (Int32 currentItemIndex = 0; currentItemIndex < items.Count; currentItemIndex++) {

                Structures.SelectionItem currentitem = (Structures.SelectionItem) items[currentItemIndex];

                if (currentitem.Value == value) {

                    itemSelected = currentitem.Selected;

                    break;

                }

            }

            return itemSelected;

        }

        public Structures.SelectionItem InsertNewItem (Int32 index) {

            Structures.SelectionItem item = new Structures.SelectionItem ();

            String itemPrefix = "Item";

            Int32 itemSuffix = 1;


            if (index == -1) { index = items.Count; }

            while (ItemExists (itemPrefix + itemSuffix.ToString ())) {

                itemSuffix = itemSuffix + 1;

            }

            item.Text = itemPrefix + itemSuffix.ToString ();

            item.Value = itemPrefix + itemSuffix.ToString ();

            item.Enabled = true;

            item.Selected = false;

            items.Insert (index, item);

            return item;

        }

        public void SetItemSelection (String value, String text, Boolean isSelected) {

            Boolean selectionChanged = false;

            if ((selectionType == Mercury.Server.Application.FormControlSelectionType.DropDownList) &&
                (DataSource == Mercury.Server.Application.FormControlDataSource.Reference)) {

                items.Clear ();

                Structures.SelectionItem selectionItem = InsertNewItem (0);

                selectionItem.Enabled = true;

                selectionItem.Selected = true;

                selectionItem.Text = text;

                selectionItem.Value = value;

                selectionChanged = true;


            }

            else {

                if ((String.IsNullOrEmpty (value)) && (!Required)) {

                    foreach (Structures.SelectionItem currentItem in items) {

                        currentItem.Selected = false;

                    }

                }

                if (ItemExists (value)) {

                    foreach (Structures.SelectionItem currentItem in items) {

                        if (currentItem.Value == value) {

                            currentItem.Selected = isSelected;

                        }

                        else if ((isSelected) && (selectionMode == Mercury.Server.Application.FormControlSelectionMode.Single)) {

                            currentItem.Selected = false;

                        }

                    }

                    customText = String.Empty;

                    selectionChanged = true;

                }

                else if (AllowCustomText) {

                    if (CustomText != text) {

                        CustomText = text;

                        selectionChanged = true;

                    }

                }

            }

            if (selectionChanged) {

                RaiseEvent ("SelectionChanged");

                DataSourceChanged ();

            }
               
            return;

        }

        public void SetItemSelectionManual (String value, Boolean isSelected) {

            if (ItemExists (value)) {

                foreach (Structures.SelectionItem currentItem in items) {

                    if (currentItem.Value == value) {

                        currentItem.Selected = isSelected;

                    }

                    else if ((isSelected) && (selectionMode == Mercury.Server.Application.FormControlSelectionMode.Single)) {

                        currentItem.Selected = false;

                    }

                }

                customText = String.Empty;

            }

            return;

        }

        public System.Data.DataTable ReferenceGetPage (String text, Int32 initialRow, Int32 pageSize) {

            Dictionary<String, String> referencePage = new Dictionary<String, String> ();


            System.Data.DataTable pageTable = new System.Data.DataTable ();

            pageTable.Columns.Add ("Text");

            pageTable.Columns.Add ("Value");


            if ((selectionType == Mercury.Server.Application.FormControlSelectionType.DropDownList) &&
                (dataSource == Mercury.Server.Application.FormControlDataSource.Reference)) {



                if (Application != null) {

                    referencePage = Application.FormControlSelection_ReferenceGetPage (Form, controlId, text, initialRow, pageSize);

                    if (referencePage != null) {

                        foreach (String currentKey in referencePage.Keys) {

                            pageTable.Rows.Add (referencePage[currentKey], currentKey);

                        }

                    }

                }

            }

            return pageTable;

        }

        public void SortItemsByText () {

            SortedDictionary<String, String> sortedItemText = new SortedDictionary<String, String> ();

            foreach (Structures.SelectionItem currentItem in items) {

                sortedItemText.Add (currentItem.Text + "." + currentItem.Value, currentItem.Value);

            }

            List<Structures.SelectionItem> sortedItems = new List<Structures.SelectionItem> ();

            foreach (String currentItemTextKey in sortedItemText.Keys) {

                sortedItems.Add (Item (sortedItemText[currentItemTextKey]));

            }

            items = sortedItems;

            return;

        }

        #endregion

    }

}
