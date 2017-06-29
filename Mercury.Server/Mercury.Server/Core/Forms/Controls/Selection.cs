using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Forms.Controls {

    [Serializable]
    [DataContract (Name = "FormControlSelection")]
    public class Selection : Mercury.Server.Core.Forms.Control {

        #region  Private Properties

        [DataMember (Name = "SelectionType")]
        protected Mercury.Server.Core.Forms.Enumerations.FormControlSelectionType selectionType = Mercury.Server.Core.Forms.Enumerations.FormControlSelectionType.DropDownList;


        [DataMember (Name = "Columns")]
        protected Int32 columns = 40;

        [DataMember (Name = "Rows")]
        protected Int32 rows = 1;

        [DataMember (Name = "Direction")]
        protected Mercury.Server.Core.Forms.Enumerations.FormControlSelectionDirection direction = Mercury.Server.Core.Forms.Enumerations.FormControlSelectionDirection.Vertical;

        [DataMember (Name = "Wrap")]
        protected Boolean wrap = false;

        [DataMember (Name = "MaxLength")]
        protected Int32 maxLength = 8000;


        [DataMember (Name = "SelectionMode")]
        protected Mercury.Server.Core.Forms.Enumerations.FormControlSelectionMode selectionMode = Mercury.Server.Core.Forms.Enumerations.FormControlSelectionMode.Single;

        [DataMember (Name = "DataSource")]
        protected Mercury.Server.Core.Forms.Enumerations.FormControlDataSource dataSource = Mercury.Server.Core.Forms.Enumerations.FormControlDataSource.ItemList;

        [DataMember (Name = "ReferenceSource")]
        protected Enumerations.FormControlSelectionReferenceSource referenceSource = Mercury.Server.Core.Forms.Enumerations.FormControlSelectionReferenceSource.Program;

        [DataMember (Name = "Items")]
        protected List<Mercury.Server.Core.Forms.Structures.SelectionItem> items = new List<Mercury.Server.Core.Forms.Structures.SelectionItem> ();


        [DataMember (Name = "AllowCustomText")]
        private Boolean allowCustomText = false;

        [DataMember (Name = "CustomText")]
        private String customText = String.Empty;
       

        #endregion


        #region Public Properties

        public Mercury.Server.Core.Forms.Enumerations.FormControlSelectionType SelectionType {

            get { return selectionType; }

            set {

                selectionType = value;

                switch (selectionType) {

                    case Mercury.Server.Core.Forms.Enumerations.FormControlSelectionType.ListBox:

                        break;

                    case Mercury.Server.Core.Forms.Enumerations.FormControlSelectionType.DropDownList:

                        selectionMode = Mercury.Server.Core.Forms.Enumerations.FormControlSelectionMode.Single;

                        break;

                    case Mercury.Server.Core.Forms.Enumerations.FormControlSelectionType.CheckBox:

                        selectionMode = Mercury.Server.Core.Forms.Enumerations.FormControlSelectionMode.Multiple;

                        break;

                    case Mercury.Server.Core.Forms.Enumerations.FormControlSelectionType.RadioButton:

                        selectionMode = Mercury.Server.Core.Forms.Enumerations.FormControlSelectionMode.Single;

                        break;

                }

            }

        }


        public Int32 Columns { get { return columns; } set { columns = value; } }

        public Int32 Rows { get { return rows; } set { rows = value; } }

        public Mercury.Server.Core.Forms.Enumerations.FormControlSelectionDirection Direction { get { return direction; } set { direction = value; } }

        public Boolean Wrap { get { return wrap; } set { wrap = value; } }

        public Int32 MaxLength { get { return maxLength; } set { maxLength = value; } }


        public Mercury.Server.Core.Forms.Enumerations.FormControlSelectionMode SelectionMode { get { return selectionMode; } set { selectionMode = value; } }

        public Mercury.Server.Core.Forms.Enumerations.FormControlDataSource DataSource { get { return dataSource; } set { dataSource = value; } }

        public Enumerations.FormControlSelectionReferenceSource ReferenceSource { get { return referenceSource; } set { referenceSource = value; } }

        public List<Mercury.Server.Core.Forms.Structures.SelectionItem> Items { 
            
            get {

                if (items == null) { items = new List<Mercury.Server.Core.Forms.Structures.SelectionItem> (); }
                
                return items; 
            
            }

            set {

                if (value == null) { items = new List<Mercury.Server.Core.Forms.Structures.SelectionItem> (); }

                else { 
                    
                    items = value;

                    foreach (Structures.SelectionItem currentItem in items) {

                        currentItem.SelectionControl = this;

                    }
                
                }

            }
        
        }


        public Boolean AllowCustomText { get { return allowCustomText; } set { allowCustomText = value; } }

        public String CustomText {

            get { return customText; }

            set {

                if ((allowCustomText) && (customText != value)) {

                    customText = value;

                    foreach (Structures.SelectionItem currentItem in Items) {

                        currentItem.Selected = false;

                    }

                }

            }

        }


        public Mercury.Server.Core.Forms.Structures.SelectionItem SelectedItem {

            get {

                Mercury.Server.Core.Forms.Structures.SelectionItem selectedItem = null;

                foreach (Mercury.Server.Core.Forms.Structures.SelectionItem currentItem in Items) {

                    if (currentItem.Selected) {

                        selectedItem = currentItem;

                        selectedItem.SelectionControl = this;

                        break;

                    }

                }

                return selectedItem;

            }

        }

        public String SelectedValue {

            get {

                String selectedValue = String.Empty;

                if ((selectionType == Mercury.Server.Core.Forms.Enumerations.FormControlSelectionType.DropDownList)

                    && (allowCustomText) && (!String.IsNullOrEmpty (customText))) {

                    selectedValue = customText;

                }

                else {

                    foreach (Mercury.Server.Core.Forms.Structures.SelectionItem currentItem in Items) {

                        if (currentItem.Selected) {

                            selectedValue = currentItem.Value;

                            break;

                        }

                    }

                }

                return selectedValue;

            }

        }


        public override System.Xml.XmlDocument ExtendedPropertiesXml {

            get {

                System.Xml.XmlDocument extendedProperties = base.ExtendedPropertiesXml;

                ExtendedProperties_AddProperty (extendedProperties, "SelectionType", ((Int32) selectionType).ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "ReferenceSource", ((Int32) referenceSource).ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "Columns", columns.ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "Rows", rows.ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "Direction", ((Int32) direction).ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "Wrap", wrap.ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "MaxLength", maxLength.ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "ReadOnly", readOnly.ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "SelectionMode", ((Int32) selectionMode).ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "DataSource", ((Int32) dataSource).ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "AllowCustomText", allowCustomText.ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "CustomText", customText.ToString ());

                
                System.Xml.XmlNode rootNode = extendedProperties.GetElementsByTagName ("ExtendedProperties")[0];

                System.Xml.XmlElement itemsNode;

                System.Xml.XmlElement propertyNode;

                itemsNode = extendedProperties.CreateElement ("Property");

                itemsNode.SetAttribute ("Name", "Items");

                rootNode.AppendChild (itemsNode);

                foreach (Mercury.Server.Core.Forms.Structures.SelectionItem currentItem in Items) {

                    propertyNode = extendedProperties.CreateElement ("Item");

                    propertyNode.SetAttribute ("Text", currentItem.Text);

                    propertyNode.SetAttribute ("Value", currentItem.Value);

                    propertyNode.SetAttribute ("Enabled", currentItem.Enabled.ToString ());

                    propertyNode.SetAttribute ("Selected", currentItem.Selected.ToString ());

                    itemsNode.AppendChild (propertyNode);

                }

                return extendedProperties;

            }

        }

        public override void ExtendedPropertiesDeserialize (System.Xml.XmlNode extendedPropertiesXml) {

            base.ExtendedPropertiesDeserialize (extendedPropertiesXml);

            // foreach (System.Xml.XmlNode currentPropertyNode in extendedPropertiesXml.SelectNodes ("./Property")) {

            foreach (System.Xml.XmlNode currentPropertyNode in extendedPropertiesXml.ChildNodes) {

                switch (currentPropertyNode.Attributes["Name"].InnerText) {

                    case "SelectionType": selectionType = (Mercury.Server.Core.Forms.Enumerations.FormControlSelectionType) Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "ReferenceSource": referenceSource = (Mercury.Server.Core.Forms.Enumerations.FormControlSelectionReferenceSource) Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "Columns": columns = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "Rows": rows = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "SelectionDirection": direction = (Mercury.Server.Core.Forms.Enumerations.FormControlSelectionDirection) Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "Wrap": wrap = Convert.ToBoolean (currentPropertyNode.InnerText); break;

                    case "MaxLength": maxLength = Convert.ToInt32 (currentPropertyNode.InnerText); break;                    

                    case "SelectionMode": selectionMode = (Mercury.Server.Core.Forms.Enumerations.FormControlSelectionMode) Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "DataSource": dataSource = (Mercury.Server.Core.Forms.Enumerations.FormControlDataSource) Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "AllowCustomText": allowCustomText = Convert.ToBoolean (currentPropertyNode.InnerText); break;

                    case "CustomText": customText = currentPropertyNode.InnerText; break;

                    case "Items": 

                        foreach (System.Xml.XmlNode currentItemNode in currentPropertyNode.ChildNodes) {

                            Structures.SelectionItem selectionItem = new Mercury.Server.Core.Forms.Structures.SelectionItem ();

                            selectionItem.SelectionControl = this;

                            selectionItem.Text = currentItemNode.Attributes ["Text"].Value;

                            selectionItem.Value = currentItemNode.Attributes ["Value"].Value;

                            selectionItem.Enabled = Convert.ToBoolean (currentItemNode.Attributes ["Enabled"].Value);

                            selectionItem.Selected = Convert.ToBoolean (currentItemNode.Attributes["Selected"].Value);

                            Items.Add (selectionItem);

                        }

                        break;

                }

            }

            return;

        }


        public override System.Xml.XmlDocument ValuesXml {

            get {

                System.Xml.XmlDocument values = base.ValuesXml;

                if ((selectionType == Mercury.Server.Core.Forms.Enumerations.FormControlSelectionType.DropDownList)

                    && (allowCustomText) && (!String.IsNullOrEmpty (customText))) {

                    Values_AddValue (values, customText, customText);

                }

                else {

                    foreach (Mercury.Server.Core.Forms.Structures.SelectionItem currentItem in Items) {

                        if (currentItem.Selected) {

                            Values_AddValue (values, currentItem.Text, currentItem.Value);

                        }

                    }

                }

                return values;

            }

        }

        public Boolean HasCustomTextValue { get { return ((allowCustomText) && (!String.IsNullOrEmpty (customText))); } }

        public override Boolean HasValue {

            get {

                switch (selectionType) {

                    case Mercury.Server.Core.Forms.Enumerations.FormControlSelectionType.DropDownList:

                    case Mercury.Server.Core.Forms.Enumerations.FormControlSelectionType.ListBox:

                    case Mercury.Server.Core.Forms.Enumerations.FormControlSelectionType.CheckBox:

                    case Mercury.Server.Core.Forms.Enumerations.FormControlSelectionType.RadioButton:

                        if ((selectionType == Mercury.Server.Core.Forms.Enumerations.FormControlSelectionType.DropDownList)

                            && (allowCustomText) && (!String.IsNullOrEmpty (customText))) { return true; }


                        foreach (Core.Forms.Structures.SelectionItem currentItem in Items) {

                            if (currentItem.Selected) { return !String.IsNullOrEmpty (currentItem.Value); }

                        }

                        return false;

                    default: return false;

                }

            }

        }

        public override string Value { get { return (HasValue) ? SelectedValue : String.Empty; } }


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

            controlType = Mercury.Server.Core.Forms.Enumerations.FormControlType.Selection;

            capabilities.HasValue = true;

            capabilities.HasLabel = true;

            capabilities.IsDataSource = true;

            capabilities.CanDataBind = true;

            return;            

        }

        public Selection (Application applicationReference) {

            InitializeControl (applicationReference);

            return;

        }

        public Selection (Application applicationReference, String labelText) {

            InitializeControl (applicationReference);

            label = new Label (application, labelText);

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

                switch (selectionType) {

                    case Mercury.Server.Core.Forms.Enumerations.FormControlSelectionType.ListBox:

                    case Mercury.Server.Core.Forms.Enumerations.FormControlSelectionType.DropDownList:

                    case Mercury.Server.Core.Forms.Enumerations.FormControlSelectionType.CheckBox:

                    case Mercury.Server.Core.Forms.Enumerations.FormControlSelectionType.RadioButton:

                        events.Add ("SelectionChanged");

                        break;

                }

                return events;

            }

        }

        public override void ValueChanged () {

            RaiseEvent ("SelectionChanged");

            DataSourceChanged ();

            return;

        }

        #endregion


        #region Data Bindings
        
        public override Dictionary<String, String> DataBindableProperties {

            get {

                Dictionary<String, String> bindableProperties = new Dictionary<String, String> ();

                if ((selectionType == Mercury.Server.Core.Forms.Enumerations.FormControlSelectionType.DropDownList) &&
                    (dataSource == Mercury.Server.Core.Forms.Enumerations.FormControlDataSource.Reference)) {

                    switch (referenceSource) {

                        default:

                            bindableProperties.Add (referenceSource.ToString () + "Id", "Id|" + referenceSource.ToString ());

                            break;

                    }

                }

                return bindableProperties;

            }

        }
        
        public override void OnDataSourceChanged (Control dataSourceControl, Boolean propogate) {

            String dataValue = String.Empty;

            base.OnDataSourceChanged (dataSourceControl, propogate);


            foreach (Mercury.Server.Core.Forms.Structures.DataBinding currentBinding in GetDataBindings (dataSourceControl.ControlId)) {

                if (currentBinding.BoundProperty.Replace ("Id", "") == referenceSource.ToString ()) {

                    dataValue = dataSourceControl.EvaluateDataBinding (currentBinding);

                    if ((selectionType == Mercury.Server.Core.Forms.Enumerations.FormControlSelectionType.DropDownList) &&
                        (dataSource == Mercury.Server.Core.Forms.Enumerations.FormControlDataSource.Reference)) {

                        Items.Clear ();

                        switch (referenceSource) {

                            case Mercury.Server.Core.Forms.Enumerations.FormControlSelectionReferenceSource.Ethnicity: 

                                Int64 ethnicityId;

                                if (Int64.TryParse (dataValue, out ethnicityId)) {

                                    String ethnicityName = application.CoreObjectGetNameById ("Ethnicity", ethnicityId);

                                    if (!String.IsNullOrEmpty (ethnicityName)) {

                                        Forms.Structures.SelectionItem item = new Mercury.Server.Core.Forms.Structures.SelectionItem ();

                                        item.SelectionControl = this;

                                        item.Text = ethnicityName;

                                        item.Value = ethnicityId.ToString ();

                                        item.Selected = true;

                                        Items.Add (item);

                                    }

                                }

                                break;

                            case Mercury.Server.Core.Forms.Enumerations.FormControlSelectionReferenceSource.Language:

                                Int64 languageId;

                                if (Int64.TryParse (dataValue, out languageId)) {

                                    String languageName = application.CoreObjectGetNameById ("Language", languageId);

                                    if (!String.IsNullOrEmpty (languageName)) {

                                        Forms.Structures.SelectionItem item = new Mercury.Server.Core.Forms.Structures.SelectionItem ();

                                        item.SelectionControl = this;

                                        item.Text = languageName;

                                        item.Value = languageId.ToString ();

                                        item.Selected = true;

                                        Items.Add (item);

                                    }

                                }

                                break;

                        }

                    }

                    else {

                        foreach (Forms.Structures.SelectionItem currentItem in Items) {

                            currentItem.Selected = (currentItem.Value == dataValue);

                            currentItem.SelectionControl = this;

                        }

                    }

                }
                
            }

            return;
        
        }
        
        public override Dictionary<String, String> DataBindingContexts {

            get {

                Dictionary<String, String> bindingContexts = new Dictionary<String, String> ();

                if ((selectionType == Mercury.Server.Core.Forms.Enumerations.FormControlSelectionType.DropDownList) &&
                    (dataSource == Mercury.Server.Core.Forms.Enumerations.FormControlDataSource.Reference)) {

                    bindingContexts.Add ("SelectedText", "String");

                    bindingContexts.Add ("SelectedValue", "Id|" + referenceSource.ToString ());

                }

                return bindingContexts;

            }

        }

        public override String EvaluateDataBinding (Structures.DataBinding dataBinding) {

            String dataValue = String.Empty;

            try {

                if ((selectionType == Mercury.Server.Core.Forms.Enumerations.FormControlSelectionType.DropDownList) &&
                    (dataSource == Mercury.Server.Core.Forms.Enumerations.FormControlDataSource.Reference)) {

                    if (SelectedValue != null) { dataValue = SelectedValue; }

                } // end switch

            } // end try

            catch (Exception applicationException) {

                System.Diagnostics.Trace.WriteLine (Name + ".EvaluateDataBinding: " + applicationException.Message);

                System.Diagnostics.Trace.Flush ();

                dataValue = "!Error";

            }

            return dataValue;

        }

        public System.Data.DataTable ReferenceGetPage (String text, Int32 initialRow, Int32 pageSize) {

            const String noSelectionText = "** No Selection";

            Dictionary<Int64, String> referenceDictionary = new Dictionary<Int64, String> ();

            Int32 currentRowIndex = 0;

            System.Data.DataTable pageTable = new System.Data.DataTable ();

            pageTable.Columns.Add ("Text");

            pageTable.Columns.Add ("Value");

            if (initialRow == 0) { pageTable.Rows.Add (noSelectionText, String.Empty); }


            String filterText = (text == noSelectionText) ? String.Empty : text;

            if ((selectionType == Enumerations.FormControlSelectionType.DropDownList) &&
                (dataSource == Enumerations.FormControlDataSource.Reference)) {

                switch (referenceSource) {

                    case Enumerations.FormControlSelectionReferenceSource.Program:

                        referenceDictionary = null;

                        Int64 programFilterInsurerId = 0;

                        foreach (Structures.DataBinding currentBinding in dataBindings) {

                            if (currentBinding.BoundProperty == "InsurerId") { 

                                String programFilterInsurerValue = Form.FindControlById (currentBinding.DataSourceControlId).EvaluateDataBinding (currentBinding);

                                if (Int64.TryParse (programFilterInsurerValue, out programFilterInsurerId)) {

                                    foreach (Core.Insurer.Program currentProgram in application.ProgramsAvailableByInsurerProgramName (programFilterInsurerId, filterText)) {

                                        referenceDictionary.Add (currentProgram.Id, currentProgram.Name);

                                    }

                                }

                            }

                        }

                        if (referenceDictionary == null) { 
                            
                            foreach (Core.Insurer.Program currentProgram in application.ProgramsAvailable ()) {
                            
                                if (currentProgram.Name.StartsWith (filterText)) {
                               
                                    referenceDictionary.Add (currentProgram.Id, currentProgram.Name);

                                }

                            }

                        }
                            
                        break;

                    case Mercury.Server.Core.Forms.Enumerations.FormControlSelectionReferenceSource.Ethnicity:

                        referenceDictionary = application.CoreObjectDictionary ("Ethnicity");

                        break;

                    case Mercury.Server.Core.Forms.Enumerations.FormControlSelectionReferenceSource.Language:

                        referenceDictionary = application.CoreObjectDictionary ("Language");

                        break;

                } // switch 

                if (referenceDictionary != null) {

                    currentRowIndex = 0;

                    foreach (Int64 currentKey in referenceDictionary.Keys) {

                        currentRowIndex = currentRowIndex + 1;

                        if ((currentRowIndex > initialRow) && (currentRowIndex <= (initialRow + pageSize))) {

                            pageTable.Rows.Add (referenceDictionary[currentKey], currentKey.ToString ());

                        }

                    }

                }

            }

            return pageTable;

        }

        #endregion


        #region Public Methods

        public Structures.SelectionItem Item (String value) {

            Structures.SelectionItem item = null;

            for (Int32 currentItemIndex = 0; currentItemIndex < Items.Count; currentItemIndex++) {

                Structures.SelectionItem currentItem = (Structures.SelectionItem) Items[currentItemIndex];

                if (currentItem.Value == value) {

                    item = currentItem;

                    item.SelectionControl = this;

                    break;

                }

            }

            return item;

        }

        public Boolean ItemExists (String value) {

            Boolean itemFound = false;

            for (Int32 currentItemIndex = 0; currentItemIndex < Items.Count; currentItemIndex++) {

                Mercury.Server.Core.Forms.Structures.SelectionItem currentitem = Items[currentItemIndex];

                if (currentitem.Value == value) {

                    itemFound = true;

                    break;

                }

            }

            return itemFound;

        }

        public Boolean ItemSelected (String value) {

            Boolean itemSelected = false;

            for (Int32 currentItemIndex = 0; currentItemIndex < Items.Count; currentItemIndex++) {

                Mercury.Server.Core.Forms.Structures.SelectionItem currentitem = Items[currentItemIndex];

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


            if (index == -1) { index = Items.Count; }

            while (ItemExists (itemPrefix + itemSuffix.ToString ())) {

                itemSuffix = itemSuffix + 1;

            }


            item.SelectionControl = this;

            item.Text = itemPrefix + itemSuffix.ToString ();

            item.Value = itemPrefix + itemSuffix.ToString ();

            item.Enabled = true;

            item.Selected = false;

            Items.Insert (index, item);

            return item;

        }

        public void SetItemSelection (String value, String text, Boolean isSelected) {

            Boolean selectionChanged = false;

            if ((selectionType == Enumerations.FormControlSelectionType.DropDownList) &&

                (DataSource == Enumerations.FormControlDataSource.Reference)) {

                items.Clear ();

                Structures.SelectionItem selectionItem = InsertNewItem (0);

                selectionItem.SelectionControl = this;

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

                        else if ((isSelected) && (selectionMode == Enumerations.FormControlSelectionMode.Single)) {

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

            if (selectionChanged) { ValueChanged (); }

            return;

        }

        public void SetItemSelection (String value, Boolean isSelected) {

            Structures.SelectionItem item = Item (value);

            if (item != null) { SetItemSelection (value, item.Text, isSelected); }

            else { SetItemSelection (value, String.Empty, isSelected); }

            return;

        }

        public void SetItemSelectionManual (String value, Boolean isSelected) {

            if (ItemExists (value)) {

                foreach (Structures.SelectionItem currentItem in items) {

                    if (currentItem.Value == value) {

                        currentItem.Selected = isSelected;

                    }

                    else if ((isSelected) && (selectionMode == Core.Forms.Enumerations.FormControlSelectionMode.Single)) {

                        currentItem.Selected = false;

                    }

                }

                customText = String.Empty;

            }

            return;

        }

        public void SortItemsByText () {

            SortedDictionary<String, String> sortedItemText = new SortedDictionary<String, String> ();

            foreach (Structures.SelectionItem currentItem in Items) {

                sortedItemText.Add (currentItem.Text + "." + currentItem.Value, currentItem.Value);

            }

            List<Structures.SelectionItem> sortedItems = new List<Mercury.Server.Core.Forms.Structures.SelectionItem> ();

            foreach (String currentItemTextKey in sortedItemText.Keys) {

                sortedItems.Add (Item (sortedItemText[currentItemTextKey]));

            }

            Items = sortedItems;

            return;

        }

        #endregion 

    }

}
