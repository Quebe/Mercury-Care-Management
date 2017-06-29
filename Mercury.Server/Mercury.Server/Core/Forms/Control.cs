using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Forms {

    [Serializable]
    [DataContract (Name = "FormControl")]
    [KnownType (typeof (Form))]
    [KnownType (typeof (Controls.Button))]
    [KnownType (typeof (Controls.Section))]
    [KnownType (typeof (Controls.SectionColumn))]
    [KnownType (typeof (Controls.Label))]
    [KnownType (typeof (Controls.Text))]
    [KnownType (typeof (Controls.Input))]
    [KnownType (typeof (Controls.Selection))]
    [KnownType (typeof (Controls.Entity))]
    [KnownType (typeof (Controls.Collection))]
    [KnownType (typeof (Controls.Address))]
    [KnownType (typeof (Controls.Metric))]
    [KnownType (typeof (Controls.Service))]
    public class Control : CoreObject {

        #region Private Properties

        [DataMember (Name = "ControlId")]
        protected Guid controlId = Guid.NewGuid ();

        // Control Name is now CoreObject.Name

        // Control Title is now CoreObject.Description

        [DataMember (Name = "ControlType")]
        protected Enumerations.FormControlType controlType = Enumerations.FormControlType.Undefined;

        [DataMember (Name = "TabIndex")]
        protected Int16 tabIndex = 0;

        [DataMember (Name = "Enabled")]
        protected Boolean enabled = true;

        [DataMember (Name = "Visible")]
        protected Boolean visible = true;

        [DataMember (Name = "ReadOnly")]
        protected Boolean readOnly = false;

        [DataMember (Name = "Required")]
        protected Boolean required = false;

        [DataMember (Name = "Position")]
        protected Forms.Enumerations.FormControlPosition position = Mercury.Server.Core.Forms.Enumerations.FormControlPosition.Left;

        [DataMember (Name = "Style")]
        protected Structures.Style style = new Mercury.Server.Core.Forms.Structures.Style ();

        [DataMember (Name = "Capabilities")]
        protected Structures.Capabilities capabilities = new Mercury.Server.Core.Forms.Structures.Capabilities ();

        [DataMember (Name = "EventHandlers")]
        protected List<Structures.EventHandler> eventHandlers = new List<Mercury.Server.Core.Forms.Structures.EventHandler> ();

        [DataMember (Name = "DataBindings")]
        protected List<Structures.DataBinding> dataBindings = new List<Structures.DataBinding> ();

        [NonSerialized]
        private Control parent = null;

        [DataMember (Name = "Controls")]
        private List<Control> controls = new List<Control> ();

        [DataMember (Name = "Label")]
        protected Controls.Label label = null;

        #endregion 

        
        #region Public Properties

        public Guid ControlId { get { return controlId; } set { controlId = value; } }

        public Enumerations.FormControlType ControlType { get { return controlType; } set { controlType = value; } }

        public Int16 TabIndex { get { return tabIndex; } set { tabIndex = (value > 3200) ? (Int16)3200 : value; } }

        public Boolean Enabled { get { return enabled; } set { enabled = value; } }

        public Boolean Visible { get { return visible; } set { visible = value; } }

        public Boolean ReadOnly { get { return readOnly; } set { readOnly = value; } }

        public Boolean Required { get { return required; } set { required = value; } }

        public Enumerations.FormControlPosition Position { get { return position; } set { position = value; } }

        public Structures.Style Style { get { return style; } set { style = value; } }

        public Structures.Capabilities Capabilities { get { return capabilities; } set { capabilities = value; } }

        public List<Structures.EventHandler> EventHandlers {

            get {

                if (eventHandlers == null) { eventHandlers = new List<Mercury.Server.Core.Forms.Structures.EventHandler> (); }

                return eventHandlers;

            }


            set {

                if (value == null) { eventHandlers = new List<Mercury.Server.Core.Forms.Structures.EventHandler> (); }

                else { eventHandlers = value; }

            }

        }

        public virtual Boolean HasValue { get { return false; } }

        public virtual String Value { get { return String.Empty; } }


        public Control Parent { get { return parent; } set { parent = value; } }

        public List<Control> Controls { get { return controls; } set { controls = value; } }

        public Controls.Label Label { get { return label; } set { label = value; } }

        public override Application Application {

            set {

                base.Application = value;

                foreach (Control currentChildControl in Controls) {

                    currentChildControl.Application = value;

                }

            }

        }

        #endregion
        

        #region Constructors

        protected Control () { return; }

        public Control (Application applicationReference) {

            Application = applicationReference;

            return;
            
        }

        #endregion 
        

        #region Xml Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlElement controlRoot = (System.Xml.XmlElement)document.ChildNodes[1];

            System.Xml.XmlNode propertiesNode = controlRoot.FirstChild;


            #region Extended Attributes on Root

            controlRoot.SetAttribute ("ControlId", controlId.ToString ());

            controlRoot.SetAttribute ("ControlType", ((Int32)controlType).ToString ());

            controlRoot.SetAttribute ("ControlTypeName", controlType.ToString ());

            #endregion



            #region Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ControlId", controlId.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ControlType", ((Int32)controlType).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ControlTypeName", controlType.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "TabIndex", tabIndex.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "Enabled", enabled.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "Visible", visible.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ReadOnly", readOnly.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "Required", required.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "Position", ((Int32)position).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "PositionName", position.ToString ());


            // IMPORT STYLE XML 

            System.Xml.XmlElement styleNode = CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "Style", String.Empty);

            styleNode.AppendChild (document.ImportNode (style.StyleProperties.LastChild, true));


            // LABEL

            System.Xml.XmlElement labelNode = CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "Label", String.Empty);

            if (label != null) { labelNode.AppendChild (document.ImportNode (Label.XmlSerialize ().LastChild, true)); }

            #endregion


            // EXTENDED PROPERTIES, EVENT HANDLERS, AND DATA BINDINGS

            controlRoot.AppendChild (document.ImportNode (ExtendedPropertiesXml.LastChild, true));

            controlRoot.AppendChild (document.ImportNode (EventHandlersXml.LastChild, true));

            controlRoot.AppendChild (document.ImportNode (DataBindingsXml.LastChild, true));


            // CHILD CONTROLS 

            System.Xml.XmlElement childControlsNode = document.CreateElement ("Children");

            controlRoot.AppendChild (childControlsNode);

            foreach (Control currentChildControl in controls) {

                childControlsNode.AppendChild (document.ImportNode (currentChildControl.XmlSerialize ().LastChild, true));

            }


            return document;

        }

        public virtual List<ImportExport.Result> XmlImport (System.Xml.XmlNode objectNode, ref Dictionary<Guid, Guid> controlDictionary) {

            List<ImportExport.Result> response = new List<ImportExport.Result> (); // DO NOT USE BASE COREOBJECT IMPORT

            System.Xml.XmlNode propertiesNode;

            String exceptionMessage = String.Empty;


            if (ObjectType != objectNode.Name) {

                exceptionMessage = "Mismatch Object Types during import. Expected '" + ObjectType + "', but found '" + objectNode.Name + "'.";

                response.Add (new ImportExport.Result (ObjectType, objectNode.Name, new ApplicationException (exceptionMessage)));

                return response;

            }


            try {

                #region Standard Properties

                propertiesNode = objectNode.SelectSingleNode ("Properties");

                foreach (System.Xml.XmlNode currentPropertyNode in propertiesNode) {

                    switch (currentPropertyNode.Attributes["Name"].InnerText) {

                        // INCLUDE CORE OBJECT PROPERTIES SINCE BASE IS NOT USED

                        case "Name": Name = currentPropertyNode.InnerText; break;

                        case "Description": Description = currentPropertyNode.InnerText; break;

                        // CONTROL PROPERTIES

                        case "ControlId":

                            if (controlDictionary == null) {   // OVERWRITE, NOT NEW IMPORT 

                                controlId = Guid.Parse (currentPropertyNode.InnerText);

                            }

                            else { // NEW IMPORT, CREATE NEW CONTROL IDS AND MAPPINGS

                                Guid importControlId = Guid.Parse (currentPropertyNode.InnerText);

                                // CHECK FOR MAPPING IN CONTROL DICTIONARY (TO MAP TO NEW CONFLICTING CONTROL ID)

                                if (controlDictionary.ContainsKey (importControlId)) {

                                    controlId = controlDictionary[importControlId]; // MAP IMPORT (OLD) TO NEW ID

                                }

                                else {

                                    // CONTINUE USING NEW AND CREATE MAPPING BETWEEN IMPORT (OLD) AND NEW

                                    controlDictionary.Add (importControlId, controlId);

                                }

                            }

                            break;


                        case "TabIndex": TabIndex = Convert.ToInt16 (currentPropertyNode.InnerText); break;

                        case "Enabled": Enabled = Convert.ToBoolean (currentPropertyNode.InnerText); break;

                        case "Visible": Visible = Convert.ToBoolean (currentPropertyNode.InnerText); break;

                        case "Style":

                            System.Xml.XmlDocument styleProperties = new System.Xml.XmlDocument ();

                            styleProperties.LoadXml (currentPropertyNode.InnerXml);

                            style = new Core.Forms.Structures.Style (styleProperties);

                            break;

                        case "Label":

                            if (currentPropertyNode.FirstChild != null) {

                                    label = new Server.Core.Forms.Controls.Label (application);

                                    label.Parent = this;

                                    response.AddRange (label.XmlImport (currentPropertyNode.FirstChild, ref controlDictionary));

                                }

                                break;

                    }

                }

                #endregion 


                ExtendedPropertiesDeserialize (objectNode.SelectSingleNode ("ExtendedProperties"));

                EventHandlersXmlDeserialize (objectNode.SelectSingleNode ("EventHandlers"));


                #region Data Bindings

                DataBindingsXmlDeserialize (objectNode.SelectSingleNode ("DataBindings"));

                if (controlDictionary != null) { // REMAP DATA BINDINGS BASED ON NEW IMPORT AND NEW IDS

                    foreach (Structures.DataBinding currentDataBinding in dataBindings) {

                        if (!controlDictionary.ContainsKey (currentDataBinding.DataSourceControlId)) {

                            // DOES NOT EXIST IN MAPPING DICTIONARY, CREATE NEW ID AND MAP

                            controlDictionary.Add (currentDataBinding.DataSourceControlId, Guid.NewGuid ());
                        }

                        // EXISTS IN MAPPING DICTIONARY, UPDATE

                        currentDataBinding.DataSourceControlId = controlDictionary[currentDataBinding.DataSourceControlId];

                    }

                }
                            

                #endregion 

                #region Child Controls

                System.Xml.XmlNode childrenRoot = objectNode.SelectSingleNode ("Children");

                foreach (System.Xml.XmlNode currentChildNode in childrenRoot) {

                    Control childControl = null;

                    Enumerations.FormControlType formControlType = (Server.Core.Forms.Enumerations.FormControlType)Convert.ToInt32 (currentChildNode.Attributes["ControlType"].InnerText);

                    switch (formControlType) {

                        case Server.Core.Forms.Enumerations.FormControlType.Label: childControl = new Controls.Label (application); break;

                        case Server.Core.Forms.Enumerations.FormControlType.Section: childControl = new Controls.Section (application); break;

                        case Server.Core.Forms.Enumerations.FormControlType.SectionColumn: childControl = new Controls.SectionColumn (application); break;

                        case Server.Core.Forms.Enumerations.FormControlType.Text: childControl = new Controls.Text (application); break;

                        case Server.Core.Forms.Enumerations.FormControlType.Input: childControl = new Controls.Input (application); break;

                        case Server.Core.Forms.Enumerations.FormControlType.Selection: childControl = new Controls.Selection (application); break;

                        case Server.Core.Forms.Enumerations.FormControlType.Button: childControl = new Controls.Button (application); break;

                        case Server.Core.Forms.Enumerations.FormControlType.Entity: childControl = new Controls.Entity (application); break;

                        case Server.Core.Forms.Enumerations.FormControlType.Address: childControl = new Controls.Address (application); break;

                        case Server.Core.Forms.Enumerations.FormControlType.Collection: childControl = new Controls.Collection (application); break;

                        case Server.Core.Forms.Enumerations.FormControlType.Service: childControl = new Controls.Service (application); break;

                        case Server.Core.Forms.Enumerations.FormControlType.Metric: childControl = new Controls.Metric (application); break;

                        default:

                            System.Diagnostics.Trace.WriteLine ("Unable to load control. Unknown control type: " + currentChildNode.Attributes["ControlType"].InnerText + ".");

                            System.Diagnostics.Trace.Flush ();

                            break;

                    }

                    if (childControl != null) {

                        response.AddRange (childControl.XmlImport (currentChildNode, ref controlDictionary));

                        childControl.Parent = this;

                        controls.Add (childControl);

                    }

                }

                #endregion 

            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return response;

        }

        #endregion 


        #region Xml Serialization of Properties

        virtual public System.Xml.XmlDocument ExtendedPropertiesXml {

            get {

                System.Xml.XmlDocument extendedProperties = new System.Xml.XmlDocument ();

                System.Xml.XmlDeclaration xmlDeclaration = extendedProperties.CreateXmlDeclaration ("1.0", "utf-8", null);

                System.Xml.XmlElement rootNode = extendedProperties.CreateElement ("ExtendedProperties");

                extendedProperties.InsertBefore (xmlDeclaration, extendedProperties.DocumentElement);

                extendedProperties.AppendChild (rootNode);


                ExtendedProperties_AddProperty (extendedProperties, "ReadOnly", readOnly.ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "Required", required.ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "Position", ((Int32)position).ToString ());

                return extendedProperties;

            }

        }

        virtual public void ExtendedPropertiesDeserialize (System.Xml.XmlNode extendedPropertiesXml) {

            // foreach (System.Xml.XmlNode currentPropertyNode in extendedPropertiesXml.SelectNodes ("./Property")) {

            foreach (System.Xml.XmlNode currentPropertyNode in extendedPropertiesXml.ChildNodes) {

                switch (currentPropertyNode.Attributes["Name"].InnerText) {

                    case "ReadOnly": readOnly = Convert.ToBoolean (currentPropertyNode.InnerText); break;

                    case "Required": required = Convert.ToBoolean (currentPropertyNode.InnerText); break;

                    case "Position": position = (Mercury.Server.Core.Forms.Enumerations.FormControlPosition)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                }

            }

            return;

        }

        virtual public System.Xml.XmlDocument DataBindingsXml {

            get {

                System.Xml.XmlDocument dataBindingsXml = new System.Xml.XmlDocument ();

                System.Xml.XmlDeclaration xmlDeclaration = dataBindingsXml.CreateXmlDeclaration ("1.0", "utf-8", null);

                System.Xml.XmlElement rootNode = dataBindingsXml.CreateElement ("DataBindings");

                dataBindingsXml.InsertBefore (xmlDeclaration, dataBindingsXml.DocumentElement);

                dataBindingsXml.AppendChild (rootNode);


                foreach (Structures.DataBinding currentBinding in DataBindings) {

                    System.Xml.XmlElement bindingElement = dataBindingsXml.CreateElement ("DataBinding");

                    bindingElement.SetAttribute ("BoundProperty", currentBinding.BoundProperty);

                    bindingElement.SetAttribute ("DataBindingType", ((Int32)currentBinding.DataBindingType).ToString ());

                    bindingElement.SetAttribute ("DataSourceControlId", currentBinding.DataSourceControlId.ToString ());

                    bindingElement.SetAttribute ("BindingContext", currentBinding.BindingContext);

                    rootNode.AppendChild (bindingElement);

                }

                return dataBindingsXml;

            }

        }

        virtual public void DataBindingsXmlDeserialize (System.Xml.XmlNode dataBindingsXml) {

            dataBindings = new List<Mercury.Server.Core.Forms.Structures.DataBinding> ();

            foreach (System.Xml.XmlNode bindingNode in dataBindingsXml.ChildNodes) {

                if (bindingNode.Name == "DataBinding") {

                    Structures.DataBinding dataBinding = new Mercury.Server.Core.Forms.Structures.DataBinding ();

                    dataBinding.BoundProperty = bindingNode.Attributes["BoundProperty"].Value;

                    dataBinding.DataBindingType = (Mercury.Server.Core.Forms.Enumerations.FormControlDataBindingType)(Int32.Parse (bindingNode.Attributes["DataBindingType"].Value));

                    dataBinding.DataSourceControlId = new Guid ((String)bindingNode.Attributes["DataSourceControlId"].Value);

                    dataBinding.BindingContext = bindingNode.Attributes["BindingContext"].Value;

                    dataBindings.Add (dataBinding);

                }

            }

            return;

        }

        virtual public System.Xml.XmlDocument EventHandlersXml {

            get {

                System.Xml.XmlDocument eventHandlersXml = new System.Xml.XmlDocument ();

                System.Xml.XmlDeclaration xmlDeclaration = eventHandlersXml.CreateXmlDeclaration ("1.0", "utf-8", null);

                System.Xml.XmlElement rootNode = eventHandlersXml.CreateElement ("EventHandlers");

                eventHandlersXml.InsertBefore (xmlDeclaration, eventHandlersXml.DocumentElement);

                eventHandlersXml.AppendChild (rootNode);


                foreach (Structures.EventHandler currentEvent in eventHandlers) {

                    System.Xml.XmlElement eventElement = eventHandlersXml.CreateElement ("EventHandler");

                    eventElement.SetAttribute ("EventName", currentEvent.EventName);

                    eventElement.SetAttribute ("MethodSource", currentEvent.MethodSource);

                    eventElement.SetAttribute ("ExecuteClientSide", currentEvent.ExecuteClientSide.ToString ());

                    eventElement.SetAttribute ("SmartEvent", currentEvent.SmartEvent.ToString ());

                    rootNode.AppendChild (eventElement);

                }

                return eventHandlersXml;

            }

        }

        virtual public void EventHandlersXmlDeserialize (System.Xml.XmlNode eventHandlersXml) {

            eventHandlers = new List<Mercury.Server.Core.Forms.Structures.EventHandler> ();

            foreach (System.Xml.XmlNode eventHandlerNode in eventHandlersXml.ChildNodes) {

                if (eventHandlerNode.Name == "EventHandler") {

                    Structures.EventHandler eventHandler = new Mercury.Server.Core.Forms.Structures.EventHandler ();

                    foreach (System.Xml.XmlAttribute currentAttribute in eventHandlerNode.Attributes) {

                        switch (currentAttribute.Name) {

                            case "EventName": eventHandler.EventName = currentAttribute.Value; break;

                            case "MethodSource": eventHandler.MethodSource = currentAttribute.Value; break;

                            case "ExecuteClientSide": eventHandler.ExecuteClientSide = Convert.ToBoolean (currentAttribute.Value); break;

                            case "SmartEvent": eventHandler.SmartEvent = Convert.ToBoolean (currentAttribute.Value); break;

                        }

                    }

                    eventHandlers.Add (eventHandler);

                }

            }

            return;

        }

        virtual public System.Xml.XmlDocument ValuesXml {

            get {

                System.Xml.XmlDocument values = new System.Xml.XmlDocument ();

                System.Xml.XmlDeclaration xmlDeclaration = values.CreateXmlDeclaration ("1.0", "utf-8", null);

                System.Xml.XmlElement rootNode = values.CreateElement ("Values");

                values.InsertBefore (xmlDeclaration, values.DocumentElement);

                values.AppendChild (rootNode);


                return values;

            }

        }

        public void ExtendedProperties_AddProperty (System.Xml.XmlDocument extendedProperties, String propertyName, String propertyValue) {

            System.Xml.XmlNode rootNode = extendedProperties.GetElementsByTagName ("ExtendedProperties")[0];

            System.Xml.XmlElement propertyNode;


            propertyNode = extendedProperties.CreateElement ("Property");

            propertyNode.SetAttribute ("Name", propertyName);

            propertyNode.InnerText = propertyValue;

            rootNode.AppendChild (propertyNode);

        }

        //public String ExtendedProperties_ReadProperty (System.Xml.XmlDocument extendedProperties, String propertyName) {

        //    System.Xml.XmlNode property;

        //    property = extendedProperties.SelectSingleNode ("./Property[@Name='" + propertyName + "']");

        //    if (property != null) {

        //        return property.InnerText;

        //    }

        //    return String.Empty;

        //}

        //public String ExtendedProperties_ReadProperty (System.Xml.XmlDocument extendedProperties, String propertyName, String defaultValue) {

        //    System.Xml.XmlNode property;

        //    property = extendedProperties.SelectSingleNode ("./Property[@Name='" + propertyName + "']");

        //    if (property != null) {

        //        return property.InnerText;

        //    }

        //    return defaultValue;

        //}

        public void Values_AddValue (System.Xml.XmlDocument values, String valueName, String value) {

            System.Xml.XmlNode rootNode = values.GetElementsByTagName ("Values")[0];

            System.Xml.XmlElement valueNode;


            valueNode = values.CreateElement ("Value");

            valueNode.SetAttribute ("Name", valueName);

            valueNode.InnerText = value;

            rootNode.AppendChild (valueNode);

        }

        //public override List<Services.Responses.ConfigurationImportResponse> XmlImport (System.Xml.XmlNode objectNode) {

        //    List<Services.Responses.ConfigurationImportResponse> response = new List<Mercury.Server.Services.Responses.ConfigurationImportResponse> ();

        //    Services.Responses.ConfigurationImportResponse importResponse = new Mercury.Server.Services.Responses.ConfigurationImportResponse ();


        //    importResponse.ObjectType = objectNode.Name;

        //    importResponse.ObjectName = objectNode.Attributes["Name"].InnerText;

        //    importResponse.Success = true;


        //    if ((objectNode.Name == "FormControl") && (objectNode.Attributes["ControlType"].InnerText == controlType.ToString ())) {

        //        try {

        //            #region Properties Node

        //            foreach (System.Xml.XmlNode currentProperty in objectNode.ChildNodes[0]) {

        //                switch (currentProperty.Attributes["Name"].InnerText) {

        //                    case "ControlId": controlId = new Guid (currentProperty.InnerText); break;

        //                    case "ControlName": controlName = currentProperty.InnerText; break;

        //                    case "ControlTitle": controlTitle = currentProperty.InnerText; break;

        //                    case "TabIndex": tabIndex = Convert.ToInt16 (currentProperty.InnerText); break;

        //                    case "Enabled": enabled = Convert.ToBoolean (currentProperty.InnerText); break;

        //                    case "Visible": visible = Convert.ToBoolean (currentProperty.InnerText); break;

        //                    case "Style":

        //                        System.Xml.XmlDocument styleProperties = new System.Xml.XmlDocument ();

        //                        styleProperties.LoadXml (currentProperty.InnerXml);

        //                        style = new Mercury.Server.Core.Forms.Structures.Style (styleProperties);

        //                        break;

        //                    case "ExtendedProperties": ExtendedPropertiesDeserialize (currentProperty.FirstChild); break;

        //                    case "DataBindings": DataBindingsXmlDeserialize (currentProperty.FirstChild); break;

        //                    case "EventHandlers": EventHandlersXmlDeserialize (currentProperty.FirstChild); break;

        //                    case "Label":

        //                        if (currentProperty.FirstChild != null) {

        //                            label = new Mercury.Server.Core.Forms.Controls.Label (application);

        //                            label.Parent = this;

        //                            response.AddRange (label.XmlImport (currentProperty.FirstChild));

        //                        }

        //                        break;

        //                }

        //            }

        //            #endregion


        //            #region Child Controls

        //            foreach (System.Xml.XmlNode currentChild in objectNode.SelectSingleNode ("./Children").ChildNodes) {

        //                Control childControl = null;

        //                Enumerations.FormControlType formControlType = (Mercury.Server.Core.Forms.Enumerations.FormControlType)Convert.ToInt32 (currentChild.Attributes["ControlTypeInt32"].InnerText);

        //                switch (formControlType) {

        //                    case Mercury.Server.Core.Forms.Enumerations.FormControlType.Label:

        //                        childControl = new Controls.Label (application);

        //                        break;

        //                    case Mercury.Server.Core.Forms.Enumerations.FormControlType.Section:

        //                        childControl = new Controls.Section (application);

        //                        break;

        //                    case Mercury.Server.Core.Forms.Enumerations.FormControlType.SectionColumn:

        //                        childControl = new Controls.SectionColumn (application);

        //                        break;

        //                    case Mercury.Server.Core.Forms.Enumerations.FormControlType.Text:

        //                        childControl = new Controls.Text (application);

        //                        break;

        //                    case Mercury.Server.Core.Forms.Enumerations.FormControlType.Input:

        //                        childControl = new Controls.Input (application);

        //                        break;

        //                    case Mercury.Server.Core.Forms.Enumerations.FormControlType.Selection:

        //                        childControl = new Controls.Selection (application);

        //                        break;

        //                    case Mercury.Server.Core.Forms.Enumerations.FormControlType.Button:

        //                        childControl = new Controls.Button (application);

        //                        break;

        //                    case Mercury.Server.Core.Forms.Enumerations.FormControlType.Entity:

        //                        childControl = new Controls.Entity (application);

        //                        break;

        //                    case Mercury.Server.Core.Forms.Enumerations.FormControlType.Address:

        //                        childControl = new Controls.Address (application);

        //                        break;

        //                    case Mercury.Server.Core.Forms.Enumerations.FormControlType.Collection:

        //                        childControl = new Controls.Collection (application);

        //                        break;

        //                    case Mercury.Server.Core.Forms.Enumerations.FormControlType.Service:

        //                        childControl = new Controls.Service (application);

        //                        break;

        //                    case Mercury.Server.Core.Forms.Enumerations.FormControlType.Metric:

        //                        childControl = new Controls.Metric (application);

        //                        break;

        //                    default:

        //                        System.Diagnostics.Trace.WriteLine ("Unable to load control. Unknown control type: " + currentChild.Attributes["ControlType"].InnerText + ".");

        //                        System.Diagnostics.Trace.Flush ();

        //                        break;

        //                }

        //                if (childControl != null) {

        //                    response.AddRange (childControl.XmlImport (currentChild));

        //                    childControl.Parent = this;

        //                    controls.Add (childControl);

        //                }

        //            }

        //            #endregion


        //            if (objectNode.Attributes["ControlType"].InnerText == "Form") {

        //                // OVERWRITE/UPDATE EXISTING FORM

        //                Int64 existingFormId = application.FormGetIdByName (controlName);

        //                if (existingFormId != 0) {

        //                    Form existingForm = application.FormGet (controlName);

        //                    controlId = existingForm.ControlId;

        //                    ((Form)this).FormId = existingForm.FormId;

        //                }

        //                importResponse.Success = Save ();

        //            }

        //        }

        //        catch (Exception importException) {

        //            importResponse.SetException (importException);

        //        }


        //    }

        //    else { importResponse.SetException (new ApplicationException ("Invalid Object Type Parsed. Expected: " + controlType.ToString ())); }


        //    response.Add (importResponse);

        //    return response;

        //}

        #endregion


        #region Public Methods

        public Mercury.Server.Core.Forms.Form Form {

            get {

                Mercury.Server.Core.Forms.Form form = null;

                if (controlType == Mercury.Server.Core.Forms.Enumerations.FormControlType.Form) {

                    return (Form)this;

                }

                if (parent == null) { return null; }

                if (controlId != parent.controlId) {

                    form = parent.Form;

                }

                return form;

            }

        }

        virtual public Mercury.Server.Core.Forms.Control FindControlById (Guid id) {

            Mercury.Server.Core.Forms.Control control = null;

            if (controlId == id) { return this; }

            foreach (Mercury.Server.Core.Forms.Control currentControl in controls) {

                if (currentControl.ControlId == id) {

                    control = currentControl;

                    break;

                }

                else {

                    control = currentControl.FindControlById (id);

                    if (control != null) { break; }

                }

            }

            return control;

        }

        virtual public Mercury.Server.Core.Forms.Control FindControlByName (String findName) {

            Mercury.Server.Core.Forms.Control control = null;

            if (name == findName) { return this; }

            foreach (Mercury.Server.Core.Forms.Control currentControl in controls) {

                currentControl.Parent = this; // RESET PARENT REFERENCE IN CASE OF CHANGE OR LOST REFERENCE

                if (currentControl.Name == findName) {

                    control = currentControl;

                    break;

                }

                else {

                    control = currentControl.FindControlByName (findName);

                    if (control != null) { break; }

                }

            }

            return control;

        }

        public Int32 ControlIndex (Guid forControlId) {

            Int32 controlIndex = -1;

            for (Int32 currentControlIndex = 0; currentControlIndex < Controls.Count; currentControlIndex++) {

                if (Controls[currentControlIndex].ControlId == forControlId) {

                    controlIndex = currentControlIndex;

                    break;

                }

            }

            return controlIndex;

        }

        public void ResetParentOnChildControls () {

            foreach (Control currentChildControl in controls) {

                currentChildControl.parent = this;

                currentChildControl.ResetParentOnChildControls ();

            }

            return;

        }


        virtual public List<Control> GetAllControls () {

            List<Control> controlList = new List<Control> ();

            controlList.Add (this);

            foreach (Control currentChild in Controls) {

                controlList.AddRange (currentChild.GetAllControls ());

            }

            return controlList;

        }

        virtual public List<Control> GetAllDataBoundControls () {

            List<Control> controlList = new List<Control> ();

            if (dataBindings.Count > 0) { controlList.Add (this); }

            controlList.Add (this);

            foreach (Control currentChild in Controls) {

                controlList.AddRange (currentChild.GetAllControls ());

            }

            return controlList;

        }

        #endregion


        #region Compile Methods

        public List<CompileMessage> ValidateControl () {

            List<CompileMessage> validationMessages = new List<CompileMessage> ();

            if ((required) && (!HasValue)) {

                validationMessages.Add (new CompileMessage (Mercury.Server.Core.Forms.Enumerations.FormCompileMessageType.Error, "Required Field Missing Value: " + Description, this));

            }

            foreach (Control currentChildControl in controls) {

                validationMessages.AddRange (currentChildControl.ValidateControl ());

            }

            return validationMessages;

        }

        public Boolean ValidateName () {

            System.Text.RegularExpressions.Match matchResults;

            String excludedCharacters = @"\\|/|\.|,|'|~|`|!|@|#|\$|%|\^|&|\*|\(|\)|\-|\+|\=|\{|\}|\[|\]|:|;|\?|<|>";


            if (String.IsNullOrWhiteSpace (name)) { return false; }

            matchResults = System.Text.RegularExpressions.Regex.Match (name, excludedCharacters);

            if (matchResults.Length > 0) { return false; }


            return true;

        }

        virtual public List<CompileMessage> Compile () {

            List<CompileMessage> compileMessages = new List<CompileMessage> ();


            #region Validation - Control Properties

            if (!ValidateName ()) {

                compileMessages.Add (new CompileMessage (Mercury.Server.Core.Forms.Enumerations.FormCompileMessageType.Error, "Invalid name. Must be greater than 0 characters and not contain symbols.", this));

            }

            #endregion


            #region Events

            List<CompileMessage> eventMessages = new List<CompileMessage> ();

            foreach (String currentEventName in Events) {

                Structures.EventHandler eventHandler = GetEventHandler (currentEventName);

                if (eventHandler != null) {

                    EventHandler_Compile (currentEventName, ref eventMessages);

                    foreach (CompileMessage currentMessage in eventMessages) {

                        currentMessage.ControlName = this.Name + "." + currentEventName;

                        currentMessage.ControlId = controlId.ToString ();

                    }

                    compileMessages.AddRange (eventMessages);

                }

            }

            #endregion


            #region Validation - Duplicate Child Control Names

            foreach (Control currentChildControl in Controls) {

                Int32 duplicateCount = 0;

                foreach (Control duplicateCheckControl in Controls) {

                    if (currentChildControl.Name == duplicateCheckControl.Name) {

                        duplicateCount = duplicateCount + 1;

                    }

                }

                if (duplicateCount > 1) {

                    compileMessages.Add (new CompileMessage (Enumerations.FormCompileMessageType.Error, "Multiple child controls with the same name (" + currentChildControl.Name + ")", currentChildControl));

                }

            } // foreach

            #endregion


            #region Compile Child Controls

            foreach (Control currentChildControl in Controls) {

                compileMessages.AddRange (currentChildControl.Compile ());

            }

            #endregion


            return compileMessages;

        }

        #endregion


        #region Database Functions

        virtual public Boolean LoadFromDatabase (Int64 entityFormId, Guid controlId, Control parentControl) {

            Boolean success = false;

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable tableFormControl;

            Control childControl;


            selectStatement.Append ("SELECT * FROM " + ((entityFormId != 0) ? "Entity" : String.Empty) + "FormControl WHERE ControlId = '" + controlId.ToString () + "'");

            if (entityFormId != 0) { selectStatement.Append (" AND EntityFormId = '" + entityFormId.ToString () + "'"); }

            tableFormControl = application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            if (tableFormControl.Rows.Count == 1) {

                this.controlId = controlId;

                this.parent = parentControl;

                MapDataFields (tableFormControl.Rows[0]);

                success = true;


                if (Capabilities.HasLabel) {

                    String selectCriteria = "ParentId = '" + controlId.ToString () + "' AND ControlType = " + ((Int32)Mercury.Server.Core.Forms.Enumerations.FormControlType.Label).ToString ();

                    if (entityFormId != 0) { selectCriteria = selectCriteria + " AND EntityFormId = " + entityFormId.ToString (); }

                    Guid labelControlId = (Guid)application.EnvironmentDatabase.LookupValue (((entityFormId != 0) ? "Entity" : String.Empty) + "FormControl", "ControlId", selectCriteria, Guid.Empty);

                    label = new Controls.Label (application);

                    if (labelControlId != Guid.Empty) {

                        success = label.LoadFromDatabase (entityFormId, labelControlId, this);

                    }

                }


                #region Load Child Controls

                selectStatement = new StringBuilder ();

                selectStatement.Append ("SELECT * FROM " + ((entityFormId != 0) ? "Entity" : String.Empty) + "FormControl WHERE ParentId = '" + controlId.ToString () + "' ");

                if (entityFormId != 0) { selectStatement.Append (" AND EntityFormId = '" + entityFormId.ToString () + "'"); }


                selectStatement.Append ("AND (ParentId != ControlId) ");

                selectStatement.Append ("AND (ControlType != " + ((Int32)Mercury.Server.Core.Forms.Enumerations.FormControlType.Label).ToString () + ") ");

                selectStatement.Append ("ORDER BY ControlIndex, ControlName");

                tableFormControl = application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

                foreach (System.Data.DataRow currentDataRow in tableFormControl.Rows) {

                    childControl = null;

                    switch ((Mercury.Server.Core.Forms.Enumerations.FormControlType)currentDataRow["ControlType"]) {

                        case Mercury.Server.Core.Forms.Enumerations.FormControlType.Label: childControl = new Controls.Label (application); break;

                        case Mercury.Server.Core.Forms.Enumerations.FormControlType.Section: childControl = new Controls.Section (application); break;

                        case Mercury.Server.Core.Forms.Enumerations.FormControlType.SectionColumn: childControl = new Controls.SectionColumn (application); break;

                        case Mercury.Server.Core.Forms.Enumerations.FormControlType.Text: childControl = new Controls.Text (application); break;

                        case Mercury.Server.Core.Forms.Enumerations.FormControlType.Input: childControl = new Controls.Input (application); break;

                        case Mercury.Server.Core.Forms.Enumerations.FormControlType.Selection: childControl = new Controls.Selection (application); break;

                        case Mercury.Server.Core.Forms.Enumerations.FormControlType.Button: childControl = new Controls.Button (application); break;

                        case Mercury.Server.Core.Forms.Enumerations.FormControlType.Entity: childControl = new Controls.Entity (application); break;

                        case Mercury.Server.Core.Forms.Enumerations.FormControlType.Collection: childControl = new Controls.Collection (application); break;

                        case Mercury.Server.Core.Forms.Enumerations.FormControlType.Address: childControl = new Controls.Address (application); break;

                        case Mercury.Server.Core.Forms.Enumerations.FormControlType.Service: childControl = new Controls.Service (application); break;

                        case Mercury.Server.Core.Forms.Enumerations.FormControlType.Metric: childControl = new Controls.Metric (application); break;

                        default:

                            System.Diagnostics.Trace.WriteLine ("Unable to load control. Unknown control type " + currentDataRow["ControlType"] + " for " + currentDataRow["ControlId"] + ".");

                            System.Diagnostics.Trace.Flush ();

                            break;

                    }

                    if (childControl != null) {

                        childControl.ControlId = (Guid)currentDataRow["ControlId"];

                        success = childControl.LoadFromDatabase (entityFormId, childControl.ControlId, this);

                        if (success) {

                            childControl.Parent = this;

                            controls.Add (childControl);

                        }

                    }

                    if (!success) { break; }

                }

                #endregion

            }

            return success;

        }

        public Boolean LoadFromDatabaseQuickLoad (Control parentControl, System.Data.DataView controlsView) {

            Boolean success = false;

            Int32 rowIndex = 0;

            Control childControl;


            // MAP SELF

            #region Map Self (Old)

            //controlsView.RowFilter = String.Empty;

            //controlsView.Sort = "ControlId";

            //rowIndex = controlsView.Find (controlId);

            //System.Diagnostics.Debug.WriteLine ("!Form Control Load (FIND): " + DateTime.Now.Subtract (startTime).TotalMilliseconds);

            #endregion 


            #region Map Self 

            // USE DEFAULT CONTROLS VIEW WHICH HAS BEEN SORTED BY CONTROL ID WHEN INITIALLY LOADED, NO LONGER NEED TO RESORT

            rowIndex = controlsView.Find (controlId);

            #endregion 


            if (rowIndex >= 0) {

                this.parent = parentControl;

                System.Data.DataRow selfReferenceRow = controlsView[rowIndex].Row;

                MapDataFields (selfReferenceRow);

                success = true;


                #region Label Old (now part of the child process) 

                //#region Label

                //if (Capabilities.HasLabel) {

                //    label = new Mercury.Server.Core.Forms.Controls.Label (application);


                //    controlsView.Sort = "ParentId, ControlType";

                //    rowIndex = controlsView.Find (new object[2] { controlId, ((Int32)Enumerations.FormControlType.Label) });

                //    if (rowIndex >= 0) {

                //        label.MapDataFields (controlsView[rowIndex].Row);

                //    }

                //}

                //#endregion

                #endregion 


                #region Load Child Controls


                #region Old Sort and Filter

                //startTime = DateTime.Now;

                //controlsView.RowFilter = "ParentId = '" + controlId.ToString () + "' "

                //        + "AND (ParentId <> ControlId) "

                //        + "AND (ControlType <> " + ((Int32)Mercury.Server.Core.Forms.Enumerations.FormControlType.Label).ToString () + ")";

                //controlsView.Sort = "ControlIndex";

                //System.Diagnostics.Debug.WriteLine ("!Form Control Load (FILTER): " + DateTime.Now.Subtract (startTime).TotalMilliseconds);


                //foreach (System.Data.DataRowView currentDataRowView in controlsView) {

                #endregion 


                System.Data.DataRow [] childControlRows = controlsView.Table.Select ("ParentId = '" + controlId.ToString () + "' ", "ControlIndex");

                // System.Diagnostics.Debug.WriteLine ("!Form Control Load (SELECT): {0:0.0000}", DateTime.Now.Subtract (startTime).TotalMilliseconds);

                 foreach (System.Data.DataRow currentDataRow in childControlRows) {

                    // System.Data.DataRow currentDataRow = currentDataRowView.Row;

                    childControl = null;

                    switch ((Mercury.Server.Core.Forms.Enumerations.FormControlType)currentDataRow["ControlType"]) {

                        case Mercury.Server.Core.Forms.Enumerations.FormControlType.Label: 

                            if (Convert.ToInt32 (currentDataRow ["ControlIndex"]) == -1) { // SELF LABEL 
                        
                                label = new Controls.Label (application);

                                label.MapDataFields (currentDataRow);

                                // DON'T ASSIGN LABEL AS CHILD CONTROL BECAUSE IT SHOULDN'T BE ADDED TO THE CHILD CONTROL COLLECTIONS

                            }

                            else { childControl = label;} // CHILD LABEL
                            
                            break;

                        case Mercury.Server.Core.Forms.Enumerations.FormControlType.Section: childControl = new Controls.Section (application); break;

                        case Mercury.Server.Core.Forms.Enumerations.FormControlType.SectionColumn: childControl = new Controls.SectionColumn (application); break;

                        case Mercury.Server.Core.Forms.Enumerations.FormControlType.Text: childControl = new Controls.Text (application); break;

                        case Mercury.Server.Core.Forms.Enumerations.FormControlType.Input: childControl = new Controls.Input (application); break;

                        case Mercury.Server.Core.Forms.Enumerations.FormControlType.Selection: childControl = new Controls.Selection (application); break;

                        case Mercury.Server.Core.Forms.Enumerations.FormControlType.Button: childControl = new Controls.Button (application); break;

                        case Mercury.Server.Core.Forms.Enumerations.FormControlType.Entity: childControl = new Controls.Entity (application); break;

                        case Mercury.Server.Core.Forms.Enumerations.FormControlType.Collection: childControl = new Controls.Collection (application); break;

                        case Mercury.Server.Core.Forms.Enumerations.FormControlType.Address: childControl = new Controls.Address (application); break;

                        case Mercury.Server.Core.Forms.Enumerations.FormControlType.Service: childControl = new Controls.Service (application); break;

                        case Mercury.Server.Core.Forms.Enumerations.FormControlType.Metric: childControl = new Controls.Metric (application); break;

                        case Enumerations.FormControlType.Form: /* DO NOTHING */ break;

                        default:

                            System.Diagnostics.Trace.WriteLine ("Unable to load control. Unknown control type " + ((Mercury.Server.Core.Forms.Enumerations.FormControlType)currentDataRow["ControlType"]) + " for " + currentDataRow["ControlId"] + ".");

                            System.Diagnostics.Trace.Flush ();

                            break;

                    }

                    if (childControl != null) {

                        childControl.ControlId = (Guid)currentDataRow["ControlId"];

                        success = childControl.LoadFromDatabaseQuickLoad (this, controlsView);

                        if (success) {

                            childControl.Parent = this;

                            controls.Add (childControl);

                        }

                    }

                    if (!success) { break; }

                }

                #endregion

            }

            return success;

        }

        public String SaveSql () {

            StringBuilder sqlStatement;


            Int32 controlIndex = 0;

            String styleProperties;

            String extendedProperties;

            String dataBindingsXml;

            String eventHandlersXml;

            String values;


            sqlStatement = new StringBuilder ();

            sqlStatement.Append ("EXEC FormControl_Insert ");

            if (Parent != null) { controlIndex = Parent.ControlIndex (controlId); }

            sqlStatement.Append (Form.FormId.ToString () + ", " + Form.EntityFormId.ToString () + ", '" + controlId.ToString () + "', '" + parent.ControlId.ToString () + "', " + controlIndex.ToString () + ", '" + NameSql + "', '" + DescriptionSql + "',");

            sqlStatement.Append (((Int32)controlType).ToString () + ", " + tabIndex.ToString () + ", " + enabled + ", " + visible);


            if (style == null) { style = new Mercury.Server.Core.Forms.Structures.Style (); }

            styleProperties = style.StyleProperties.InnerXml;

            styleProperties = styleProperties.Replace ("'", "''");

            styleProperties = styleProperties.Replace ((char)0xA0, (char)0x20);

            styleProperties = styleProperties.Replace ((char)0xB7, (char)0x20);

            sqlStatement.Append (", '" + styleProperties + "'");


            extendedProperties = ExtendedPropertiesXml.InnerXml;

            extendedProperties = extendedProperties.Replace ("'", "''");

            extendedProperties = extendedProperties.Replace ((char)0xA0, (char)0x20);

            extendedProperties = extendedProperties.Replace ((char)0xB7, (char)0x20);

            sqlStatement.Append (", '" + extendedProperties + "'");


            dataBindingsXml = DataBindingsXml.InnerXml;

            dataBindingsXml = dataBindingsXml.Replace ("'", "''");

            dataBindingsXml = dataBindingsXml.Replace ((char)0xA0, (char)0x20);

            dataBindingsXml = dataBindingsXml.Replace ((char)0xB7, (char)0x20);

            sqlStatement.Append (", '" + dataBindingsXml + "'");


            eventHandlersXml = EventHandlersXml.InnerXml;

            eventHandlersXml = eventHandlersXml.Replace ("'", "''");

            eventHandlersXml = eventHandlersXml.Replace ((char)0xA0, (char)0x20);

            eventHandlersXml = eventHandlersXml.Replace ((char)0xB7, (char)0x20);

            sqlStatement.Append (", '" + eventHandlersXml + "'");


            values = ValuesXml.InnerXml;

            values = values.Replace ("'", "''");

            values = values.Replace ((char)0xA0, (char)0x20);

            values = values.Replace ((char)0xB7, (char)0x20);

            sqlStatement.Append (", '" + values + "'");


            return sqlStatement.ToString ();

        }


        protected String SaveBatchSql () {

            StringBuilder executeBatchStatement = new StringBuilder ();


            executeBatchStatement.AppendLine (SaveSql ());

            if ((capabilities.HasLabel) && (label != null)) {

                label.Parent = this;

                executeBatchStatement.AppendLine (label.SaveSql ());

            }

            foreach (Control currentControl in Controls) {

                currentControl.Parent = this;

                executeBatchStatement.AppendLine (currentControl.SaveBatchSql ());

            }

            return executeBatchStatement.ToString ();

        }

        public override Boolean Save () {

            // FAST METHOD

            String executeStatement = String.Empty;

            Boolean success = false;


            executeStatement = SaveBatchSql ();

            success = application.EnvironmentDatabase.ExecuteSqlStatement (executeStatement);


            return success;


            // SLOW METHOD

            //Boolean success = false;

            //try {

            //    success = application.EnvironmentDatabase.ExecuteSqlStatement (SaveSql ().ToString ());

            //    if ((success) && (capabilities.HasLabel) && (label != null)) {

            //        label.Parent = this;

            //        success = label.Save ();

            //    }

            //    if (!success) { throw application.EnvironmentDatabase.LastException; }

            //    foreach (Control currentControl in Controls) {

            //        currentControl.parent = this;

            //        success = currentControl.Save ();

            //        if (!success) { throw application.EnvironmentDatabase.LastException; }

            //    }

            //}

            //catch (Exception applicationException) {

            //    application.SetLastException (applicationException);

            //    return false;

            //}

            //return true;

        }

        override public void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            // CHECK FOR FIELDS, THESE MIGHT CHANGE IN FUTURE RELEASE TO FormControlName and FormControlDescription, WHICH WOULD BE MAPPED THROUGH BASE CORE OBJECT

            if (currentRow.Table.Columns.Contains ("ControlName")) { name = (String)currentRow["ControlName"]; }

            if (currentRow.Table.Columns.Contains ("ControlTitle")) { description = (String)currentRow["ControlTitle"]; }


            tabIndex = (Int16)currentRow["TabIndex"];

            enabled = (Boolean)currentRow["Enabled"];

            visible = (Boolean)currentRow["Visible"];


            if (!String.IsNullOrEmpty ((String)currentRow["Style"])) {

                System.Xml.XmlDocument stylePropertiesXml = new System.Xml.XmlDocument ();

                stylePropertiesXml.LoadXml ((String)currentRow["Style"]);

                style = new Mercury.Server.Core.Forms.Structures.Style (stylePropertiesXml);

            }

            if (!String.IsNullOrEmpty ((String)currentRow["ExtendedProperties"])) {

                System.Xml.XmlDocument extendedPropertiesXml = new System.Xml.XmlDocument ();

                extendedPropertiesXml.LoadXml ((String)currentRow["ExtendedProperties"]);

                ExtendedPropertiesDeserialize (extendedPropertiesXml.LastChild);

            }


            if (!String.IsNullOrEmpty ((String)currentRow["DataBindings"])) {

                System.Xml.XmlDocument dataBindingsXml = new System.Xml.XmlDocument ();

                dataBindingsXml.LoadXml ((String)currentRow["DataBindings"]);

                DataBindingsXmlDeserialize (dataBindingsXml.LastChild);

            }

            if (!String.IsNullOrEmpty ((String)currentRow["EventHandlers"])) {

                System.Xml.XmlDocument eventHandlersXml = new System.Xml.XmlDocument ();

                eventHandlersXml.LoadXml ((String)currentRow["EventHandlers"]);

                EventHandlersXmlDeserialize (eventHandlersXml.LastChild);

            }

            return;

        }

        #endregion


        #region Event Handlers

        public virtual List<String> Events { get { return new List<String> (); } }

        public virtual Boolean HasEvents { get { return (Events.Count > 0); } }

        public Boolean HasEventHandler (String eventName) {

            foreach (Mercury.Server.Core.Forms.Structures.EventHandler currentEventHandler in EventHandlers) {

                if (currentEventHandler.EventName == eventName) {

                    return true;

                }

            }

            return false;

        }

        public virtual Mercury.Server.Core.Forms.Structures.EventHandler GetEventHandler (String eventName) {

            foreach (Mercury.Server.Core.Forms.Structures.EventHandler currentEventHandler in EventHandlers) {

                if (currentEventHandler.EventName == eventName) {

                    return currentEventHandler;

                }

            }

            return null;

        }

        private String EventHandler_Source (String eventName) {

            StringBuilder codeSource = new StringBuilder ();

            Core.Forms.Structures.EventHandler eventHandler = GetEventHandler (eventName);

            if (eventHandler == null) { return String.Empty; }

            if (String.IsNullOrEmpty (eventHandler.MethodSource)) { return String.Empty; }



            codeSource.Append ("using System; \r\n ");

            codeSource.Append ("using System.IO; \r\n");

            codeSource.Append ("using Enumerations = Mercury.Server.Core.Enumerations; \r\n \r\n"); // LOCALIZE ENUMERATIONS FOR ENTITY

            codeSource.Append ("using Core = Mercury.Server.Core; \r\n \r\n");
            
            codeSource.Append ("namespace Mercury.Server.EventHandlerNamespace { \r\n \r\n");

            codeSource.Append ("public class EventHandlerClass { \r\n \r\n");

            codeSource.Append ("public void OnEvent (ref Core.Forms.Form form, ref Core.Forms.Control sender, System.Diagnostics.TraceListener traceListener) { \r\n \r\n");

            codeSource.Append (eventHandler.MethodSource.Replace ("\r", "\r\n"));

            codeSource.Append (" \r\n  return; ");

            codeSource.Append (" \r\n } } }");


            String source = codeSource.ToString ().Replace ("\r\n", "\r");

            return source;

        }

        public System.Reflection.Assembly EventHandler_Compile (String eventName, ref List<CompileMessage> compileMessages) {

            compileMessages = new List<CompileMessage> ();

            CompileMessage message;

            String codeSource = EventHandler_Source (eventName);

            if (String.IsNullOrEmpty (codeSource)) {

                message = new CompileMessage (Enumerations.FormCompileMessageType.Error, "No Event Handler found for Control.", this);

                compileMessages.Add (message);

            }

            else {

                Microsoft.CSharp.CSharpCodeProvider compiler = new Microsoft.CSharp.CSharpCodeProvider ();

                System.CodeDom.Compiler.CompilerParameters compilerParameters = new System.CodeDom.Compiler.CompilerParameters ();

                compilerParameters.ReferencedAssemblies.Add ("System.dll");


                foreach (System.Reflection.Assembly loadedAssembly in AppDomain.CurrentDomain.GetAssemblies ()) {

                    if (loadedAssembly.FullName.Contains ("Mercury.Server,")) {

                        compilerParameters.ReferencedAssemblies.Add (loadedAssembly.Location);

                        break;

                    }

                }

                System.CodeDom.Compiler.CompilerResults compiledResults = compiler.CompileAssemblyFromSource (compilerParameters, codeSource);

                if (compiledResults.Errors.HasErrors) {

                    application.SetLastException (new ApplicationException ("Unable to Compile Event Handler: " + name + "." + eventName));

                    foreach (System.CodeDom.Compiler.CompilerError currentError in compiledResults.Errors) {

                        message = new CompileMessage (Enumerations.FormCompileMessageType.Error, currentError.Line - 10, currentError.Column, currentError.ErrorText, this);

                        compileMessages.Add (message);

                    }

                }


                try {

                    const String findControlByNameDefinition = "FindControlByName(";

                    String eventHandlerSource = codeSource.Replace ("FindControlByName (", "FindControlByName(");

                    while (eventHandlerSource.Contains (findControlByNameDefinition)) {

                        eventHandlerSource = eventHandlerSource.Substring (eventHandlerSource.IndexOf (findControlByNameDefinition) + findControlByNameDefinition.Length).Trim ();

                        Int32 positionInitial = 0;

                        Int32 positionFinal = eventHandlerSource.IndexOf (")");

                        if (positionFinal == -1) { break; }


                        String referencedControlName = eventHandlerSource.Substring (positionInitial, (positionFinal - positionInitial));

                        if ((referencedControlName[0] == '"') && (referencedControlName[referencedControlName.Length - 1] == '"')) {

                            referencedControlName = referencedControlName.Substring (1, referencedControlName.Length - 2);

                            if (!referencedControlName.Contains ("\"")) {

                                if (Form.FindControlByName (referencedControlName) == null) {

                                    message = new CompileMessage (Mercury.Server.Core.Forms.Enumerations.FormCompileMessageType.Error, 0, 0, "Unable to find control: " + referencedControlName, this);

                                    compileMessages.Add (message);

                                }

                            }

                        }

                        eventHandlerSource = eventHandlerSource.Substring (positionFinal);

                    }

                }

                catch (Exception smartEventException) {

                    System.Diagnostics.Debug.WriteLine ("Exception during Smart Event: " + smartEventException.Message);

                }


                if (compileMessages.Count > 0) { return null; }


                return compiledResults.CompiledAssembly;

            }

            return null;

        }

        public List<CompileMessage> EventHandler_Compile (String eventName) {

            List<CompileMessage> compileMessages = new List<CompileMessage> ();

            Structures.EventHandler eventHandler = GetEventHandler (eventName);

            if (eventHandler != null) {

                EventHandler_Compile (eventName, ref compileMessages);

            }

            return compileMessages;

        }

        private EventResult EventHandler_Execute (String eventName) {

            EventResult eventResult = new EventResult (controlId, eventName, true);

            List<CompileMessage> compileMessages = new List<CompileMessage> ();

            System.Reflection.Assembly compiledAssembly = null;


            String cacheKey = Form.FormId.ToString () + controlId.ToString () + eventName + Form.ModifiedAccountInfo.ActionDate.ToString ("MMddyyyy");

            if (application.UseFormControlEventHandlerCaching) {

                compiledAssembly = (System.Reflection.Assembly)application.CacheManager.GetObject (cacheKey);

            }

            if (compiledAssembly == null) {

                compiledAssembly = EventHandler_Compile (eventName, ref compileMessages);

                application.CacheManager.CacheObject (cacheKey, compiledAssembly, Caching.CacheDuration.Indefinite, true);

            }

            if (compiledAssembly == null) {

                application.SetLastException (new ApplicationException ("Unable to obtain compiled assembly reference."));

                eventResult = new EventResult (controlId, eventName, new ApplicationException ("Unable to obtain compiled assembly reference."));

                return eventResult;

            }

            if (compileMessages.Count > 0) {

                application.SetLastException (new ApplicationException ("Unable to compile Event Handler. " + compileMessages.Count.ToString () + " error(s) found."));

                eventResult = new EventResult (controlId, eventName, new ApplicationException ("Unable to compile Event Handler. " + compileMessages.Count.ToString () + " error(s) found."));

                return eventResult;

            }


            Object functionInstance = compiledAssembly.CreateInstance ("Mercury.Server.EventHandlerNamespace.EventHandlerClass");

            if (functionInstance == null) {

                application.SetLastException (new ApplicationException ("Unable to create an instance of the Event Handler."));

                eventResult = new EventResult (controlId, eventName, new ApplicationException ("Unable to create an instance of the Event Handler."));

            }

            else {

                System.IO.StringWriter stringWriter = new System.IO.StringWriter ();

                System.Diagnostics.TextWriterTraceListener traceListener = new System.Diagnostics.TextWriterTraceListener (stringWriter);

                try {

                    Object[] functionArguments = new Object[3];

                    functionArguments[0] = Form;

                    functionArguments[1] = (Control)this;

                    functionArguments[2] = traceListener;

                    functionInstance.GetType ().InvokeMember ("OnEvent", System.Reflection.BindingFlags.InvokeMethod, null, functionInstance, functionArguments);

                    eventResult.ListenerOutput = stringWriter.ToString ();

                }

                catch (System.Reflection.TargetInvocationException invocationException) {

                    if (invocationException.InnerException != null) {

                        application.SetLastException (invocationException.InnerException);

                        eventResult = new EventResult (controlId, eventName, invocationException.InnerException);

                        eventResult.ListenerOutput = stringWriter.ToString ();

                    }

                    else {

                        application.SetLastException (invocationException);

                        eventResult = new EventResult (controlId, eventName, invocationException);

                        eventResult.ListenerOutput = stringWriter.ToString ();

                    }

                }

                catch (Exception eventHandlerException) {

                    String exceptionMessage = "Form Event Handler Exception [" + Form.Id + "]: " + Form.Name + "." + this.Name + "." + eventName;

                    ApplicationException applicationException = new ApplicationException (exceptionMessage, eventHandlerException);

                    application.SetLastException (applicationException);

                    eventResult = new EventResult (controlId, eventName, applicationException);

                    eventResult.ListenerOutput = stringWriter.ToString ();

                }

            }

            if (eventResult != null) {

                if (!String.IsNullOrEmpty (eventResult.ListenerOutput.Trim ())) {

                    System.Diagnostics.Debug.WriteLine (name + " (" + eventName + ") Listener Output \r\n" + eventResult.ListenerOutput);

                }

            }


            return eventResult;

        }

        public virtual EventResult RaiseEvent (String eventName) {

            EventResult eventResult = new EventResult (controlId, eventName, true);

            Structures.EventHandler eventHandler = GetEventHandler (eventName);

            if (eventHandler != null) {

                eventResult = EventHandler_Execute (eventName);

                if (Form != null) {

                    Form.EventResults.Add (eventResult);

                }

            }

            return eventResult;

        }

        public virtual void ValueChanged () {

            return;

        }

        #endregion


        #region Data Bindings

        public virtual Boolean IsDataSource { get { return (DataBindingContexts.Count > 0); } }

        public virtual Dictionary<String, String> DataBindableProperties { get { return new Dictionary<String, String> (); } }

        public virtual Boolean DataBindingAllowed (String bindableProperty, String forDataType) {

            // FOR PERFORMANCE REASONS (AND TO REDUCE ROUNDTRIPS), THIS CODE IS DUPLICATED BETWEEN SERVER AND CLIENT
            // ANY CHANGES TO THE BELOW CODE MUST BE COPIED TO THE OTHER PLACE

            Boolean bindingAllowed = false;

            Dictionary<String, String> bindableProperties = DataBindableProperties;

            foreach (String currentKey in bindableProperties.Keys) {

                if (currentKey == bindableProperty) {

                    String propertyDataType = bindableProperties[currentKey].Split ('|')[0];

                    String sourceDataType = forDataType.Split ('|')[0];


                    switch (propertyDataType) {

                        case "Id": if (bindableProperties[currentKey] == forDataType) { bindingAllowed = true; } break;

                        case "String": if (sourceDataType != "Collection") { bindingAllowed = true; } break;

                        case "Int16": if ((sourceDataType == "Int16") || (sourceDataType == "Numeric")) { bindingAllowed = true; } break;

                        case "Int32": if ((sourceDataType == "Int16") || (sourceDataType == "Int32") || (sourceDataType == "Numeric")) { bindingAllowed = true; } break;

                        case "Int64":

                        case "Numeric":

                            if ((sourceDataType == "Int16") || (sourceDataType == "Int32") || (sourceDataType == "Int64") || (sourceDataType == "Numeric")) { bindingAllowed = true; } break;

                        case "DateTime": if (sourceDataType == "DateTime") { bindingAllowed = true; } break;

                        case "Collection": if (sourceDataType == "Collection") { bindingAllowed = true; } break;

                        default: bindingAllowed = false; break;

                    }

                    break;

                }

            }

            return bindingAllowed;

        }

        public List<Structures.DataBinding> DataBindings {

            get {

                if (dataBindings == null) { dataBindings = new List<Mercury.Server.Core.Forms.Structures.DataBinding> (); }

                return dataBindings;

            }

            set { dataBindings = value; }
        }

        public Boolean ContainsDataBinding (Guid forControlId) {

            foreach (Structures.DataBinding currentDataBinding in DataBindings) {

                if (currentDataBinding.DataSourceControlId == forControlId) { return true; }

            }

            return false;

        }

        public List<Structures.DataBinding> GetDataBindings (Guid forControlId) {

            List<Structures.DataBinding> bindingSubset = new List<Mercury.Server.Core.Forms.Structures.DataBinding> ();

            foreach (Structures.DataBinding currentDataBinding in DataBindings) {

                if (currentDataBinding.DataSourceControlId == forControlId) {

                    bindingSubset.Add (currentDataBinding);

                }

            }

            return bindingSubset;

        }

        public Structures.DataBinding GetDataBinding (String forProperty) {

            if (DataBindings == null) { return null; }

            foreach (Structures.DataBinding currentBinding in DataBindings) {

                if (currentBinding.BoundProperty == forProperty) { return currentBinding; }

            }

            return null;

        }

        public String GetDataBindingContextDataType (String context) {

            String dataType = String.Empty;

            Dictionary<String, String> dataBindingContexts = DataBindingContexts;

            foreach (String currentContext in dataBindingContexts.Keys) {

                if (currentContext == context) {

                    dataType = dataBindingContexts[currentContext];

                    break;

                }

            }

            return dataType;

        }

        public virtual void OnDataSourceChanged (Control dataSourceControl, Boolean propogate) {

            if (propogate) {

                foreach (Control currentControl in Controls) {

                    currentControl.Parent = this;  // RESET PARENT REFERENCE IN CASE OF CHANGE OR LOST REFERENCE

                    currentControl.OnDataSourceChanged (dataSourceControl, propogate);

                }

            }

            return;

        }

        public void DataSourceChanged () {

            if (Form != null) {

                Form.OnDataSourceChanged (this, true);

            }

        }

        virtual public String EvaluateDataBinding (Structures.DataBinding dataBinding) {

            String dataValue = String.Empty;

            switch (dataBinding.DataBindingType) {

                case Mercury.Server.Core.Forms.Enumerations.FormControlDataBindingType.Control: dataValue = String.Empty; break;

                case Mercury.Server.Core.Forms.Enumerations.FormControlDataBindingType.FixedValue: dataValue = dataBinding.BindingContext; break;

                case Mercury.Server.Core.Forms.Enumerations.FormControlDataBindingType.Function:

                    Microsoft.CSharp.CSharpCodeProvider compiler = new Microsoft.CSharp.CSharpCodeProvider ();

                    System.CodeDom.Compiler.CompilerParameters compilerParameters = new System.CodeDom.Compiler.CompilerParameters ();

                    compilerParameters.ReferencedAssemblies.Add ("System.dll");

                    String codeSource = String.Empty;

                    codeSource = codeSource + @"using System; using System.IO; namespace DataBinding { public class FunctionBinding { ";

                    codeSource = codeSource + @"public String EvaluateDataBindingContext () {";

                    codeSource = codeSource + @"  return Convert.ToString (" + dataBinding.BindingContext + ");";

                    codeSource = codeSource + @"} } }";

                    System.CodeDom.Compiler.CompilerResults compiledResults = compiler.CompileAssemblyFromSource (compilerParameters, codeSource);

                    if (compiledResults.Errors.HasErrors) {

                        dataValue = "!Error";

                        foreach (System.CodeDom.Compiler.CompilerError currentError in compiledResults.Errors) {

                            System.Diagnostics.Trace.WriteLine ("Line: " + currentError.Line.ToString () + ": " + currentError.ErrorText);

                        }

                        System.Diagnostics.Trace.Flush ();

                    }

                    else {

                        System.Reflection.Assembly functionAssembly = compiledResults.CompiledAssembly;

                        Object functionInstance = functionAssembly.CreateInstance ("DataBinding.FunctionBinding");

                        if (functionInstance == null) { dataValue = "!Error"; }

                        else {

                            try {

                                dataValue = (String)functionInstance.GetType ().InvokeMember ("EvaluateDataBindingContext", System.Reflection.BindingFlags.InvokeMethod, null, functionInstance, null);

                            }

                            catch (Exception applicationException) {

                                dataValue = "!Error";

                                application.SetLastException (applicationException);

                            }

                        }

                    }

                    break;

            }

            return dataValue;

        }


        public Boolean HasDependencyDataBinding {

            get {

                Boolean hasDependencyDataBinding = false;

                foreach (Control currentControl in Form.GetAllDataBoundControls ()) {

                    if (currentControl.ContainsDataBinding (this.controlId)) {

                        hasDependencyDataBinding = true;

                        break;

                    }

                }

                return hasDependencyDataBinding;

            }

        }

        #endregion

    }

}
