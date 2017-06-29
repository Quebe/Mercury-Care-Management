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
using System.Collections.Generic;

namespace Mercury.Client.Core.Forms.Controls {

    public class Selection : Control {
        
        #region Private Properties

        protected Server.Application.FormControlSelectionType selectionType = Server.Application.FormControlSelectionType.DropDownList;


        protected Int32 columns = 1;

        protected Int32 rows = 4;

        protected Server.Application.FormControlSelectionDirection direction = Server.Application.FormControlSelectionDirection.Vertical;

        protected Boolean wrap = false;         // Drop Down List

        protected Int32 maxLength = 8000;       // Drop Down List


        protected Server.Application.FormControlSelectionMode selectionMode = Server.Application.FormControlSelectionMode.Single;

        protected Server.Application.FormControlDataSource dataSource = Server.Application.FormControlDataSource.ItemList;

        protected Server.Application.FormControlSelectionReferenceSource referenceSource = Server.Application.FormControlSelectionReferenceSource.Program;

        protected System.Collections.ObjectModel.ObservableCollection<Structures.SelectionItem> items = new System.Collections.ObjectModel.ObservableCollection<Mercury.Client.Core.Forms.Structures.SelectionItem> ();


        private Boolean allowCustomText = false;

        private String customText = String.Empty;

        #endregion


        #region Public Properties

        public Server.Application.FormControlSelectionType SelectionType {

            get { return selectionType; }

            set {

                selectionType = value;

                switch (selectionType) {

                    case Server.Application.FormControlSelectionType.ListBox:

                        break;

                    case Server.Application.FormControlSelectionType.DropDownList:

                        selectionMode = Server.Application.FormControlSelectionMode.Single;

                        break;

                    case Server.Application.FormControlSelectionType.CheckBox:

                        selectionMode = Server.Application.FormControlSelectionMode.Multiple;

                        break;

                    case Server.Application.FormControlSelectionType.RadioButton:

                        selectionMode = Server.Application.FormControlSelectionMode.Single;

                        break;

                }

            }

        }


        public Int32 Columns { get { return columns; } set { columns = value; } }

        public Int32 Rows { get { return rows; } set { rows = value; } }

        public Server.Application.FormControlSelectionDirection Direction { get { return direction; } set { direction = value; } }

        public Boolean Wrap { get { return wrap; } set { wrap = value; } }

        public Int32 MaxLength { get { return maxLength; } set { maxLength = value; } }


        public Server.Application.FormControlSelectionMode SelectionMode { get { return selectionMode; } set { selectionMode = value; } }


        public Server.Application.FormControlDataSource DataSource { get { return dataSource; } set { dataSource = value; dataBindablePropertiesIsLoaded = false; } }

        public Server.Application.FormControlSelectionReferenceSource ReferenceSource { get { return referenceSource; } set { referenceSource = value; dataBindablePropertiesIsLoaded = false; } }

        public System.Collections.ObjectModel.ObservableCollection<Structures.SelectionItem> Items { get { return items; } set { items = value; } }


        public Boolean AllowCustomText { get { return allowCustomText; } set { allowCustomText = value; } }

        public String CustomText {

            get { return customText; }

            set {

                if ((allowCustomText) && (customText != value)) {

                    customText = value;

                    foreach (Structures.SelectionItem currentItem in items) {

                        currentItem.Selected = false;

                        //RaiseEvent ("SelectionChanged");

                        //DataSourceChanged ();

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

                if ((selectionType == Server.Application.FormControlSelectionType.DropDownList)

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

                    case Server.Application.FormControlSelectionType.DropDownList:

                    case Server.Application.FormControlSelectionType.ListBox:

                    case Server.Application.FormControlSelectionType.CheckBox:

                    case Server.Application.FormControlSelectionType.RadioButton:

                        if ((selectionType == Server.Application.FormControlSelectionType.DropDownList)

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

        #endregion


        #region Constructors

        virtual protected void InitializeControl (Application applicationReference) {

            BaseConstructor (applicationReference);

            ControlType = Server.Application.FormControlType.Selection;

            capabilities.HasValue = true;

            capabilities.HasLabel = true;

            capabilities.IsDataSource = true;

            capabilities.CanDataBind = true;

            label = new Label (Application, this);

            return;

        }

        override public void BaseConstructor (Mercury.Client.Core.Forms.Control parentControl, Server.Application.FormControl serverControl) {

            base.BaseConstructor (parentControl, serverControl);


            Server.Application.FormControlSelection serverSelection = (Server.Application.FormControlSelection) serverControl;

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


            items = new System.Collections.ObjectModel.ObservableCollection<Mercury.Client.Core.Forms.Structures.SelectionItem> ();

            foreach (Server.Application.FormControlSelectionItem currentServerItem in serverSelection.Items) {

                Structures.SelectionItem selectionItem = new Mercury.Client.Core.Forms.Structures.SelectionItem (this, currentServerItem);

                items.Add (selectionItem);

            }


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

        public Selection (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Server.Application.FormControlSelection serverSelection) {

            InitializeControl (applicationReference);

            BaseConstructor (parentControl, serverSelection);

            ChildServerControlsToLocal (this, serverSelection);

            return;

        }

        #endregion


        #region Virtual Overrides

        public override void LocalControlToServer (Server.Application.FormControl parentControl, Server.Application.FormControl serverControl) {

            base.LocalControlToServer (parentControl, serverControl);

            ((Server.Application.FormControlSelection) serverControl).SelectionType = selectionType;


            ((Server.Application.FormControlSelection) serverControl).Columns = columns;

            ((Server.Application.FormControlSelection) serverControl).Rows = rows;

            ((Server.Application.FormControlSelection) serverControl).Direction = direction;

            ((Server.Application.FormControlSelection) serverControl).Wrap = wrap;

            ((Server.Application.FormControlSelection) serverControl).MaxLength = maxLength;

            ((Server.Application.FormControlSelection) serverControl).ReadOnly = readOnly;

            ((Server.Application.FormControlSelection) serverControl).SelectionMode = selectionMode;

            ((Server.Application.FormControlSelection) serverControl).DataSource = dataSource;

            ((Server.Application.FormControlSelection) serverControl).ReferenceSource = referenceSource;


            ((Server.Application.FormControlSelection) serverControl).AllowCustomText = allowCustomText;

            ((Server.Application.FormControlSelection) serverControl).CustomText = customText;


            ((Server.Application.FormControlSelection) serverControl).Items = new System.Collections.ObjectModel.ObservableCollection<Mercury.Server.Application.FormControlSelectionItem> ();

            foreach (Structures.SelectionItem currentSelectionItem in items) {

                Server.Application.FormControlSelectionItem serverItem = new Mercury.Server.Application.FormControlSelectionItem ();

                serverItem.Text = currentSelectionItem.Text;

                serverItem.Value = currentSelectionItem.Value;

                serverItem.Enabled = currentSelectionItem.Enabled;

                serverItem.Selected = currentSelectionItem.Selected;

                ((Server.Application.FormControlSelection) serverControl).Items.Add (serverItem);

            }

            ((Server.Application.FormControlSelection) serverControl).Label = new Server.Application.FormControlLabel ();

            label.LocalControlToServer (serverControl, ((Server.Application.FormControlSelection) serverControl).Label);

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

            item.SelectionControl = this;

            item.Text = itemPrefix + itemSuffix.ToString ();

            item.Value = itemPrefix + itemSuffix.ToString ();

            item.Enabled = true;

            item.Selected = false;

            items.Insert (index, item);

            return item;

        }

        public void SetItemSelection (String value, String text, Boolean isSelected) {

            if ((selectionType == Server.Application.FormControlSelectionType.DropDownList) &&
                (DataSource == Server.Application.FormControlDataSource.Reference)) {

                items.Clear ();

                Structures.SelectionItem selectionItem = InsertNewItem (0);

                selectionItem.Enabled = true;

                selectionItem.Selected = true;

                selectionItem.Text = text;

                selectionItem.Value = value;

                //RaiseEvent ("SelectionChanged");

                //DataSourceChanged ();

            }

            else {

                if (ItemExists (value)) {

                    foreach (Structures.SelectionItem currentItem in items) {

                        if (currentItem.Value == value) {

                            currentItem.Selected = isSelected;

                        }

                        else if ((isSelected) && (selectionMode == Server.Application.FormControlSelectionMode.Single)) {

                            currentItem.Selected = false;

                        }

                    }

                    customText = String.Empty;

                    //RaiseEvent ("SelectionChanged");

                    //DataSourceChanged ();

                }

                else if (AllowCustomText) {

                    CustomText = text;

                }

            }

            return;

        }

        public void SetItemSelectionManual (String value, Boolean isSelected) {

            if (ItemExists (value)) {

                foreach (Structures.SelectionItem currentItem in items) {

                    if (currentItem.Value == value) {

                        currentItem.Selected = isSelected;

                    }

                    else if ((isSelected) && (selectionMode == Server.Application.FormControlSelectionMode.Single)) {

                        currentItem.Selected = false;

                    }

                }

                customText = String.Empty;

            }

            return;

        }

        //public System.Data.DataTable ReferenceGetPage (String text, Int32 initialRow, Int32 pageSize) {

        //    Dictionary<String, String> referencePage = new Dictionary<String, String> ();


        //    System.Data.DataTable pageTable = new System.Data.DataTable ();

        //    pageTable.Columns.Add ("Text");

        //    pageTable.Columns.Add ("Value");


        //    if ((selectionType == Server.Application.FormControlSelectionType.DropDownList) &&
        //        (dataSource == Server.Application.FormControlDataSource.Reference)) {



        //        if (Application != null) {

        //            referencePage = Application.FormControlSelection_ReferenceGetPage (Form, controlId, text, initialRow, pageSize);

        //            if (referencePage != null) {

        //                foreach (String currentKey in referencePage.Keys) {

        //                    pageTable.Rows.Add (referencePage[currentKey], currentKey);

        //                }

        //            }

        //        }

        //    }

        //    return pageTable;

        //}

        #endregion



        #region Server Update - Update Control

        public Boolean SelectionItemsChangedAfterUpdate (Server.Application.FormControlSelection serverSelection) {

            Boolean hasChanged = false;

            if (items.Count != serverSelection.Items.Count) {

                hasChanged = true;

            }

            else {

                for (Int32 currentItemIndex = 0; currentItemIndex < items.Count; currentItemIndex++) {

                    hasChanged = hasChanged || (items[currentItemIndex].Text != serverSelection.Items[currentItemIndex].Text) || (items[currentItemIndex].Value != serverSelection.Items[currentItemIndex].Value);

                    if (hasChanged) { break; }

                }

            }


            return hasChanged; 

        }

        public override void UpdateControl (Server.Application.FormControl serverControl) {

            if (controlId != serverControl.ControlId) { return; }

            base.UpdateControl (serverControl);


            Server.Application.FormControlSelection serverSelection = (Server.Application.FormControlSelection) serverControl;

            // TODO: UPDATE SELECTION ITEMS (CAN YOU ADD ITEMS?)

            if (SelectionItemsChangedAfterUpdate (serverSelection)) {

                // TODO: NOTIFY FULL UPDATE OF ITEMS (NEEDS TO BE RE-RENDERED

            }

            else {

                for (Int32 currentItemIndex = 0; currentItemIndex < items.Count; currentItemIndex++) {

                    items[currentItemIndex].Selected = serverSelection.Items[currentItemIndex].Selected;

                    items[currentItemIndex].Enabled = serverSelection.Items[currentItemIndex].Enabled;

                }

            }

            return;

        }

        #endregion 

    }

}
