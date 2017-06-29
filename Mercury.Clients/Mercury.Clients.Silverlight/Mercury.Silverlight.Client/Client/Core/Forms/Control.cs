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
using System.Collections.ObjectModel;

namespace Mercury.Client.Core.Forms {

    public class Control : CoreObject {

        #region Private Properties

        protected Guid controlId = Guid.NewGuid ();

        protected Server.Application.FormControlType controlType = Server.Application.FormControlType.Undefined;


        protected Int16 tabIndex = 0;

        protected Boolean enabled = true;

        protected Boolean visible = true;

        protected Boolean readOnly = false;

        protected Boolean required = false;

        protected Server.Application.FormControlPosition position = Server.Application.FormControlPosition.Left;

        protected Server.Application.FormControlCapabilities capabilities = new Server.Application.FormControlCapabilities ();

        protected Structures.Style style = null;

        protected System.Collections.ObjectModel.ObservableCollection<Server.Application.FormControlEventHandler> eventHandlers = new ObservableCollection<Mercury.Server.Application.FormControlEventHandler> ();

        protected Controls.Label label = null;

        protected Control parent = null;

        protected List<Control> controls = new List<Control> ();

        #endregion 


        #region Public Properties

        public Guid ControlId { get { return controlId; } set { controlId = value; } }

        public Server.Application.FormControlType ControlType { get { return controlType; } set { controlType = value; } }

        public Int16 TabIndex {

            get { return (Int16) (tabIndex); }

            set {

                if (tabIndex != value) {

                    tabIndex = (value > 3200) ? (Int16) 3200 : value;

                    NotifyPropertyChanged ("TabIndex");

                }

            }

        }

        public Boolean Enabled {

            get { return enabled; }

            set {

                if (enabled != value) {

                    enabled = value; 
                    
                    NotifyPropertyChanged ("Enabled");

                    NotifyPropertyChanged ("EnabledAndNotReadOnly");

                    NotifyPropertyChanged ("ReadOnlyOrDisabled");

                }

            }

        }

        public Boolean Visible {

            get { return visible; }

            set {

                if (visible != value) {

                    visible = value;

                    NotifyPropertyChanged ("Visible");

                    NotifyPropertyChanged ("Visibility");

                }

            }

        }

        public Boolean ReadOnly {

            get { return readOnly; }

            set {

                if (readOnly != value) {

                    readOnly = value;

                    NotifyPropertyChanged ("ReadOnly");

                    NotifyPropertyChanged ("EnabledAndNotReadOnly");

                    NotifyPropertyChanged ("ReadOnlyOrDisabled");

                }

            }

        }

        public Boolean Required { get { return required; } set { required = value; } }

        public Server.Application.FormControlPosition Position { get { return position; } set { position = value; } }

        public Server.Application.FormControlCapabilities Capabilities { get { return capabilities; } set { capabilities = value; } }

        public Structures.Style Style { get { return style; } set { style = value; } }

        public Controls.Label Label { get { return label; } set { label = value; } }

        public Control Parent { get { return parent; } set { parent = value; } }

        public List<Control> Controls { get { return controls; } set { controls = value; } }


        public virtual Boolean HasValue { get { return false; } }

        public virtual String Value { get { return String.Empty; } }

        #endregion


        #region Silverlight Framework Element Support 
        
        public virtual void NotifyStylePropretyChangedToChildren (Guid sourceControlId, String propertyName) {

            if (controlId != sourceControlId) {

                style.NotifyPropertyChangedReceivedFromParent (propertyName);

                if (label != null) {

                    label.Style.NotifyPropertyChangedReceivedFromParent (propertyName);

                }

            }

            foreach (Control currentChild in controls) {

                currentChild.NotifyStylePropretyChangedToChildren (sourceControlId, propertyName);

            }

            return;

        }


        public Visibility Visibility {

            get {

                if (visible) { return Visibility.Visible; }

                return Visibility.Collapsed;

            }

            set {

                if (value == Visibility.Visible) { Visible = true; }

                else { Visible = false; }

            }

        }

        public Boolean EnabledAndNotReadOnly { get { return ((enabled) && (!readOnly)); } }

        public Boolean ReadOnlyOrDisabled { get { return ((ReadOnly) || (!Enabled)); } }

        #endregion 


        #region Constructors

        protected override void BaseConstructor (Application applicationReference) {

            style = new Mercury.Client.Core.Forms.Structures.Style (this);

            base.BaseConstructor (applicationReference);

            return;

        }

        virtual public void BaseConstructor (Mercury.Client.Core.Forms.Control parentControl, Server.Application.FormControl serverControl) {

            base.BaseConstructor (application, serverControl);


            controlId = serverControl.ControlId;

            controlType = serverControl.ControlType;

            tabIndex = serverControl.TabIndex;

            enabled = serverControl.Enabled;

            visible = serverControl.Visible;

            readOnly = serverControl.ReadOnly;

            required = serverControl.Required;

            position = serverControl.Position;

            capabilities = serverControl.Capabilities;



            eventHandlers = serverControl.EventHandlers;

            if (eventHandlers == null) { eventHandlers = new ObservableCollection<Mercury.Server.Application.FormControlEventHandler> (); }


            if (serverControl.Style != null) { style = new Mercury.Client.Core.Forms.Structures.Style (this, serverControl.Style); }

            else if (style == null) { style = new Mercury.Client.Core.Forms.Structures.Style (this); }


            dataBindings = serverControl.DataBindings;


            parent = parentControl;

            return;

        }

        public void LocalControlToServer (Server.Application.FormControl serverControl) {

            base.MapToServerObject ((Server.Application.CoreObject)serverControl);


            serverControl.ControlId = controlId;

            serverControl.ControlType = controlType;

            serverControl.TabIndex = tabIndex;

            serverControl.Enabled = enabled;

            serverControl.Visible = visible;

            serverControl.ReadOnly = readOnly;

            serverControl.Required = required;

            serverControl.Position = position;

            serverControl.Capabilities = capabilities;

            // serverControl.Style = style;


            serverControl.EventHandlers = eventHandlers;


            //serverControl.DataBindings = new Server.Application.FormControlDataBinding[dataBindings.Count];

            //dataBindings.CopyTo (serverControl.DataBindings);

            return;

        }

        virtual public void LocalControlToServer (Server.Application.FormControl parentControl, Server.Application.FormControl serverControl) {

            Int32 controlIndex = 0;

            LocalControlToServer (serverControl);

            serverControl.Controls = new System.Collections.ObjectModel.ObservableCollection<Server.Application.FormControl> ();

            foreach (Control currentLocalControl in Controls) {

                switch (currentLocalControl.ControlType) {

                    case Server.Application.FormControlType.Section:

                        Server.Application.FormControlSection serverSection = new Server.Application.FormControlSection ();

                        ((Mercury.Client.Core.Forms.Controls.Section) currentLocalControl).LocalControlToServer (serverControl, serverSection);

                        serverControl.Controls.Insert (controlIndex, serverSection);

                        controlIndex = controlIndex + 1;

                        break;

                    case Server.Application.FormControlType.SectionColumn:

                        Server.Application.FormControlSectionColumn serverSectionColumn = new Server.Application.FormControlSectionColumn ();

                        ((Mercury.Client.Core.Forms.Controls.SectionColumn) currentLocalControl).LocalControlToServer (serverControl, serverSectionColumn);

                        serverControl.Controls.Insert (controlIndex, serverSectionColumn);

                        controlIndex = controlIndex + 1;

                        break;

                    case Server.Application.FormControlType.Label:

                        Server.Application.FormControlLabel serverLabel = new Server.Application.FormControlLabel ();

                        ((Mercury.Client.Core.Forms.Controls.Label) currentLocalControl).LocalControlToServer (serverControl, serverLabel);

                        serverControl.Controls.Insert (controlIndex, serverLabel);

                        controlIndex = controlIndex + 1;

                        break;

                    case Server.Application.FormControlType.Text:

                        Server.Application.FormControlText serverText = new Server.Application.FormControlText ();

                        ((Mercury.Client.Core.Forms.Controls.Text) currentLocalControl).LocalControlToServer (serverControl, serverText);

                        serverControl.Controls.Insert (controlIndex, serverText);

                        controlIndex = controlIndex + 1;

                        break;

                    case Server.Application.FormControlType.Input:

                        Server.Application.FormControlInput serverInput = new Server.Application.FormControlInput ();

                        ((Mercury.Client.Core.Forms.Controls.Input) currentLocalControl).LocalControlToServer (serverControl, serverInput);

                        serverControl.Controls.Insert (controlIndex, serverInput);

                        controlIndex = controlIndex + 1;

                        break;

                    case Server.Application.FormControlType.Selection:

                        Server.Application.FormControlSelection serverSelection = new Server.Application.FormControlSelection ();

                        ((Mercury.Client.Core.Forms.Controls.Selection) currentLocalControl).LocalControlToServer (serverControl, serverSelection);

                        serverControl.Controls.Insert (controlIndex, serverSelection);

                        controlIndex = controlIndex + 1;

                        break;

                    case Server.Application.FormControlType.Button:

                        Server.Application.FormControlButton serverButton = new Server.Application.FormControlButton ();

                        ((Mercury.Client.Core.Forms.Controls.Button) currentLocalControl).LocalControlToServer (serverControl, serverButton);

                        serverControl.Controls.Insert (controlIndex, serverButton);

                        controlIndex = controlIndex + 1;

                        break;

                    case Server.Application.FormControlType.Entity:

                        Server.Application.FormControlEntity serverEntity = new Server.Application.FormControlEntity ();

                        ((Mercury.Client.Core.Forms.Controls.Entity) currentLocalControl).LocalControlToServer (serverControl, serverEntity);

                        serverControl.Controls.Insert (controlIndex, serverEntity);

                        controlIndex = controlIndex + 1;

                        break;

                    case Server.Application.FormControlType.Collection:

                        Server.Application.FormControlCollection serverCollection = new Server.Application.FormControlCollection ();

                        ((Mercury.Client.Core.Forms.Controls.Collection) currentLocalControl).LocalControlToServer (serverControl, serverCollection);

                        serverControl.Controls.Insert (controlIndex, serverCollection);

                        controlIndex = controlIndex + 1;

                        break;

                    case Server.Application.FormControlType.Address:

                        Server.Application.FormControlAddress serverAddress = new Server.Application.FormControlAddress ();

                        ((Mercury.Client.Core.Forms.Controls.Address) currentLocalControl).LocalControlToServer (serverControl, serverAddress);

                        serverControl.Controls.Insert (controlIndex, serverAddress);

                        controlIndex = controlIndex + 1;

                        break;

                    case Server.Application.FormControlType.Service:

                        Server.Application.FormControlService serverService = new Server.Application.FormControlService ();

                        ((Mercury.Client.Core.Forms.Controls.Service) currentLocalControl).LocalControlToServer (serverControl, serverService);

                        serverControl.Controls.Insert (controlIndex, serverService);

                        controlIndex = controlIndex + 1;

                        break;

                    case Server.Application.FormControlType.Metric:

                        Server.Application.FormControlMetric serverMetric = new Server.Application.FormControlMetric ();

                        ((Mercury.Client.Core.Forms.Controls.Metric) currentLocalControl).LocalControlToServer (serverControl, serverMetric);

                        serverControl.Controls.Insert (controlIndex, serverMetric);

                        controlIndex = controlIndex + 1;

                        break;

                    default:

                        System.Diagnostics.Debug.WriteLine ("Unable to process Local to Server: " + currentLocalControl.controlType.ToString ());

                        throw new Exception ("Local Control to Server. Unable to process Local to Server: " + currentLocalControl.controlType.ToString ());

                } // switch

            } // foreach

            return;

        }

        public void ChildServerControlsToLocal (Mercury.Client.Core.Forms.Control parentControl, Server.Application.FormControl serverControl) {

            foreach (Server.Application.FormControl currentControl in serverControl.Controls) {

                switch (currentControl.ControlType) {

                    case Server.Application.FormControlType.Section:

                        Mercury.Client.Core.Forms.Controls.Section sectionControl;

                        sectionControl = new Mercury.Client.Core.Forms.Controls.Section (Application, this, (Server.Application.FormControlSection) currentControl);

                        Controls.Insert (Controls.Count, sectionControl);

                        break;

                    case Server.Application.FormControlType.SectionColumn:

                        Mercury.Client.Core.Forms.Controls.SectionColumn sectionColumnControl;

                        sectionColumnControl = new Mercury.Client.Core.Forms.Controls.SectionColumn (Application, this, (Server.Application.FormControlSectionColumn) currentControl);

                        Controls.Insert (Controls.Count, sectionColumnControl);

                        break;


                    case Server.Application.FormControlType.Label:

                        Mercury.Client.Core.Forms.Controls.Label labelControl;

                        labelControl = new Mercury.Client.Core.Forms.Controls.Label (Application, this, (Server.Application.FormControlLabel) currentControl);

                        Controls.Insert (Controls.Count, labelControl);

                        break;


                    case Server.Application.FormControlType.Text:

                        Mercury.Client.Core.Forms.Controls.Text textControl;

                        textControl = new Mercury.Client.Core.Forms.Controls.Text (Application, this, (Server.Application.FormControlText) currentControl);

                        Controls.Insert (Controls.Count, textControl);

                        break;

                    case Server.Application.FormControlType.Input:

                        Mercury.Client.Core.Forms.Controls.Input inputControl;

                        inputControl = new Mercury.Client.Core.Forms.Controls.Input (Application, this, (Server.Application.FormControlInput) currentControl);

                        Controls.Insert (Controls.Count, inputControl);

                        break;

                    case Server.Application.FormControlType.Selection:

                        Mercury.Client.Core.Forms.Controls.Selection selectionControl;

                        selectionControl = new Mercury.Client.Core.Forms.Controls.Selection (Application, this, (Server.Application.FormControlSelection) currentControl);

                        Controls.Insert (Controls.Count, selectionControl);

                        break;

                    case Server.Application.FormControlType.Button:

                        Mercury.Client.Core.Forms.Controls.Button buttonControl;

                        buttonControl = new Mercury.Client.Core.Forms.Controls.Button (Application, this, (Server.Application.FormControlButton) currentControl);

                        Controls.Insert (Controls.Count, buttonControl);

                        break;

                    case Server.Application.FormControlType.Entity:

                        Mercury.Client.Core.Forms.Controls.Entity entityControl;

                        entityControl = new Mercury.Client.Core.Forms.Controls.Entity (Application, this, (Server.Application.FormControlEntity) currentControl);

                        Controls.Insert (Controls.Count, entityControl);

                        break;

                    case Server.Application.FormControlType.Collection:

                        Mercury.Client.Core.Forms.Controls.Collection collectionControl;

                        collectionControl = new Mercury.Client.Core.Forms.Controls.Collection (Application, this, (Server.Application.FormControlCollection) currentControl);

                        Controls.Insert (Controls.Count, collectionControl);

                        break;

                    case Server.Application.FormControlType.Address:

                        Mercury.Client.Core.Forms.Controls.Address addressControl;

                        addressControl = new Mercury.Client.Core.Forms.Controls.Address (Application, this, (Server.Application.FormControlAddress) currentControl);

                        Controls.Insert (Controls.Count, addressControl);

                        break;

                    case Server.Application.FormControlType.Service:

                        Mercury.Client.Core.Forms.Controls.Service serviceControl;

                        serviceControl = new Mercury.Client.Core.Forms.Controls.Service (Application, this, (Server.Application.FormControlService) currentControl);

                        Controls.Insert (Controls.Count, serviceControl);

                        break;

                    case Server.Application.FormControlType.Metric:

                        Mercury.Client.Core.Forms.Controls.Metric metricControl;

                        metricControl = new Mercury.Client.Core.Forms.Controls.Metric (Application, this, (Server.Application.FormControlMetric) currentControl);

                        Controls.Insert (Controls.Count, metricControl);

                        break;

                    default:

                        System.Diagnostics.Debug.WriteLine ("Unable to map Server control to Local control for " + currentControl.ControlType.ToString () + ".");

                        throw new Exception ("Unable to map Server control to Local control for " + currentControl.ControlType.ToString () + ".");

                } // switch

            } // foreach 

        }

        #endregion


        #region Public Methods

        virtual public Boolean AllowChildControl (Server.Application.FormControlType childControlType) {

            return false;

        }

        virtual public void AddChildControl (Control childControl) {

            if (AllowChildControl (childControl.ControlType)) {

                childControl.Parent = this;

                childControl.Application = Application;

                controls.Add (childControl);

            }

            return;

        }


        public Int32 ControlIndex (Guid forControlId) {

            Int32 controlIndex = -1;

            for (Int32 currentControlIndex = 0; currentControlIndex < controls.Count; currentControlIndex++) {

                Control currentControl = (Control) controls[currentControlIndex];

                if (currentControl.ControlId == forControlId) {

                    controlIndex = currentControlIndex;

                    break;

                }

            }

            return controlIndex;

        }

        public Int32 ControlIndex (String controlName) {

            Int32 controlIndex = -1;

            for (Int32 currentControlIndex = 0; currentControlIndex < controls.Count; currentControlIndex++) {

                Control currentControl = (Control) controls[currentControlIndex];

                if (currentControl.Name == controlName) {

                    controlIndex = currentControlIndex;

                    break;

                }

            }

            return controlIndex;

        }

        public Int32 ControlIndexByName (String controlName) {

            Int32 controlIndex = -1;

            for (Int32 currentControlIndex = 0; currentControlIndex < controls.Count; currentControlIndex++) {

                Control currentControl = (Control) controls[currentControlIndex];

                if (currentControl.Name == controlName) {

                    controlIndex = currentControlIndex;

                    break;

                }

            }

            return controlIndex;

        }

        public Boolean ControlExists (Guid forControlId) {

            Boolean controlFound = false;

            for (Int32 currentControlIndex = 0; currentControlIndex < controls.Count; currentControlIndex++) {

                Control currentControl = (Control) controls[currentControlIndex];

                if (currentControl.ControlId == forControlId) {

                    controlFound = true;

                    break;

                }

            }

            return controlFound;

        }

        public Boolean ControlExistsByName (String controlName) {

            Boolean controlFound = false;

            for (Int32 currentControlIndex = 0; currentControlIndex < controls.Count; currentControlIndex++) {

                Control currentControl = (Control) controls[currentControlIndex];

                if (currentControl.Name == controlName) {

                    controlFound = true;

                    break;

                }

            }

            return controlFound;

        }

        public Mercury.Client.Core.Forms.Control GetChildControl (String controlName) {

            Int32 controlIndex = ControlIndex (controlName);

            if (controlIndex != -1) {

                return (Mercury.Client.Core.Forms.Control) controls[controlIndex];

            }

            return null;

        }

        public Mercury.Client.Core.Forms.Control GetChildControl (Int32 controlIndex) {

            return (Mercury.Client.Core.Forms.Control) controls[controlIndex];

        }

        public Mercury.Client.Core.Forms.Control GetChildControl (Guid forControlId) {

            Control childControl = null;

            foreach (Control currentChildControl in controls) {

                if (currentChildControl.ControlId == forControlId) {

                    childControl = currentChildControl;

                    break;

                }

            }

            return childControl;

        }

        virtual public Mercury.Client.Core.Forms.Control FindControlById (Guid forControlId) {

            Mercury.Client.Core.Forms.Control control = null;

            if (controlId == forControlId) { return this; }

            foreach (Mercury.Client.Core.Forms.Control currentControl in controls) {

                if (currentControl.ControlId == forControlId) {

                    control = currentControl;

                    break;

                }

                else {

                    control = currentControl.FindControlById (forControlId);

                    if (control != null) { break; }

                }

            }

            return control;

        }

        virtual public Mercury.Client.Core.Forms.Control FindControlByName (String forControlName) {

            Mercury.Client.Core.Forms.Control control = null;

            if (Name == forControlName) { return this; }

            foreach (Mercury.Client.Core.Forms.Control currentControl in controls) {

                control = currentControl.FindControlByName (forControlName);

                if (control != null) { break; }

            }

            return control;

        }

        public void InsertNewControl (Int32 index, Mercury.Client.Core.Forms.Control control) {

            String controlPrefix = control.controlType.ToString ();

            Int32 controlSuffix = 1;


            control.parent = this;


            if (index == -1) { index = controls.Count; }

            while (Form.FindControlByName (controlPrefix + controlSuffix.ToString ()) != null) {

                controlSuffix = controlSuffix + 1;

            }


            control.Name = controlPrefix + controlSuffix.ToString ();

            if (index > controls.Count) { index = controls.Count; }

            controls.Insert (index, control);

            return;

        }

        public void InsertNewControl (Int32 index, Server.Application.FormControlType forControlType) {

            #region Create Appropriate Child Control

            Client.Core.Forms.Control childControl = null;

            switch (forControlType) {

                case Server.Application.FormControlType.Address: childControl = new global::Mercury.Client.Core.Forms.Controls.Address (application); break;

                case Server.Application.FormControlType.Button: childControl = new global::Mercury.Client.Core.Forms.Controls.Button (application); break;

                case Server.Application.FormControlType.Collection: childControl = new global::Mercury.Client.Core.Forms.Controls.Collection (application); break;

                case Server.Application.FormControlType.Entity: childControl = new global::Mercury.Client.Core.Forms.Controls.Entity (application); break;

                case Server.Application.FormControlType.Input: childControl = new global::Mercury.Client.Core.Forms.Controls.Input (application); break;

                case Server.Application.FormControlType.Label: childControl = new global::Mercury.Client.Core.Forms.Controls.Label (application); break;

                case Server.Application.FormControlType.Metric: childControl = new global::Mercury.Client.Core.Forms.Controls.Metric (application); break;

                case Server.Application.FormControlType.Section: childControl = new global::Mercury.Client.Core.Forms.Controls.Section (application); break;

                case Server.Application.FormControlType.SectionColumn: childControl = new global::Mercury.Client.Core.Forms.Controls.SectionColumn (application); break;

                case Server.Application.FormControlType.Selection: childControl = new global::Mercury.Client.Core.Forms.Controls.Selection (application); break;

                case Server.Application.FormControlType.Service: childControl = new global::Mercury.Client.Core.Forms.Controls.Service (application); break;

                case Server.Application.FormControlType.Text: childControl = new global::Mercury.Client.Core.Forms.Controls.Text (application); break;

            }

            if ((childControl != null) && (AllowChildControl (forControlType))) {

                InsertNewControl (index, childControl);

            }

            #endregion 

            return;

        }

        public void InsertControl (Int32 index, Mercury.Client.Core.Forms.Control control) {

            if (index == -1) { index = controls.Count; }

            control.Parent = this;

            controls.Insert (index, control);

            return;

        }

        public Mercury.Client.Core.Forms.Form Form {

            get {

                Mercury.Client.Core.Forms.Form form = null;

                if (controlType == Server.Application.FormControlType.Form) { form = (Form) this; }

                else if (parent == null) { form = null; }

                else if (controlId != parent.controlId) { form = parent.Form; }

                return form;

            }

        }

        virtual public List<Control> GetAllControls () {

            List<Control> controlList = new List<Control>();

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


        #region Data Bindings

        #region Private Properties

        protected Boolean dataBindablePropertiesIsLoaded = false;

        private Application.FormControl_DataBindableProperties_Completed dataBindablePropertiesCallback = null;

        protected Dictionary<String, String> dataBindableProperties = new Dictionary<String, String> ();


        private ObservableCollection<Server.Application.FormControlDataBinding> dataBindings = new ObservableCollection<Server.Application.FormControlDataBinding> ();

        #endregion


        #region Public Properties

        public Boolean DataBindablePropertiesIsLoaded { get { return dataBindablePropertiesIsLoaded; } }

        public ObservableCollection<Server.Application.FormControlDataBinding> DataBindings { get { return dataBindings; } set { dataBindings = value; } }

        #endregion


        public void DataBindableProperties (Application.FormControl_DataBindableProperties_Completed eventHandler) {

            dataBindablePropertiesIsLoaded = false;

            if (dataBindablePropertiesIsLoaded) {

                Server.Application.FormControl_DataBindablePropertiesCompletedEventArgs completedEventArgs;

                completedEventArgs = new Server.Application.FormControl_DataBindablePropertiesCompletedEventArgs (

                    new Object[] { dataBindableProperties }, null, false, null);

                if (eventHandler != null) { eventHandler (this, completedEventArgs); }

            }

            else {

                dataBindablePropertiesCallback = eventHandler;

                Server.Application.Form serverForm = (Server.Application.Form) Form.ToServerObject ();

                application.FormControl_DataBindableProperties (serverForm, controlId, DataBindableProperties_Completed);

            }

            return;

        }

        public void DataBindableProperties_Completed (Object sender, Server.Application.FormControl_DataBindablePropertiesCompletedEventArgs eventArgs) {

            dataBindableProperties = eventArgs.Result;

            dataBindablePropertiesIsLoaded = true;


            if (dataBindablePropertiesCallback != null) {

                dataBindablePropertiesCallback (sender, eventArgs);

            }

            return;

        }

        public void DataBindingContexts (Application.FormControl_DataBindingContexts_Completed eventHandler) {

            dataBindingContextsIsLoaded = false;

            if (dataBindingContextsIsLoaded) {

                Server.Application.FormControl_DataBindingContextsCompletedEventArgs completedEventArgs;

                completedEventArgs = new Server.Application.FormControl_DataBindingContextsCompletedEventArgs (

                    new Object[] { dataBindingContexts }, null, false, null);

                if (eventHandler != null) { eventHandler (this, completedEventArgs); }

            }

            else {

                Server.Application.ApplicationClient client = Application.ApplicationClient;

                client.FormControl_DataBindingContextsCompleted += new EventHandler<Server.Application.FormControl_DataBindingContextsCompletedEventArgs> (DataBindingContexts_Completed);

                client.FormControl_DataBindingContextsCompleted += new EventHandler<Server.Application.FormControl_DataBindingContextsCompletedEventArgs> (eventHandler);

                Server.Application.Form serverForm = (Server.Application.Form) Form.ToServerObject ();

                client.FormControl_DataBindingContextsAsync (Application.Session.Token, serverForm, controlId);

            }

            return;

        }

        public void DataBindingContexts_Completed (Object sender, Server.Application.FormControl_DataBindingContextsCompletedEventArgs eventArgs) {

            dataBindingContexts = eventArgs.Result;

            dataBindingContextsIsLoaded = true;

            return;

        }        


        public Boolean DataBindingAllowed (String bindablePropertyName, String forDataType) {

            Boolean bindingAllowed = false;

            if ((dataBindableProperties == null) || (!dataBindablePropertiesIsLoaded)) { return false; }

            if (dataBindableProperties.ContainsKey (bindablePropertyName)) {

                String propertyDataType = dataBindableProperties[bindablePropertyName].Split ('|')[0];

                String sourceDataType = forDataType.Split ('|')[0];


                switch (propertyDataType) {

                    case "Id": if (dataBindableProperties[bindablePropertyName] == forDataType) { bindingAllowed = true; } break;

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

            }
            
            return bindingAllowed;

        }

        public Boolean DataBindingExists (Guid dataSourceControlId) {

            foreach (Server.Application.FormControlDataBinding currentDataBinding in dataBindings) {

                if (currentDataBinding.DataSourceControlId == dataSourceControlId) {

                    return true;

                }

            }

            return false;

        }

        public List<Server.Application.FormControlDataBinding> DataBindingsForDataSource (Guid dataSourceControlId) {

            List<Server.Application.FormControlDataBinding> bindingSubset = new List<Server.Application.FormControlDataBinding> ();

            foreach (Server.Application.FormControlDataBinding currentDataBinding in dataBindings) {

                if (currentDataBinding.DataSourceControlId == dataSourceControlId) {

                    bindingSubset.Add (currentDataBinding);

                }

            }

            return bindingSubset;

        }

        public Server.Application.FormControlDataBinding DataBindingForProperty (String propertyName) {

            Server.Application.FormControlDataBinding propertyDataBinding = null;

            foreach (Server.Application.FormControlDataBinding currentDataBinding in dataBindings) {

                if (currentDataBinding.BoundProperty == propertyName) {

                    propertyDataBinding = currentDataBinding;

                    break;

                }

            }

            return propertyDataBinding;

        }

        public String DataBindingContextDataType (String context) {

            String dataType = String.Empty;

            foreach (String currentContext in dataBindingContexts.Keys) {

                if (currentContext == context) {

                    dataType = dataBindingContexts[currentContext];

                    break;

                }

            }

            return dataType;

        }

        public List<Control> DataBindingDependencies (Guid dataSourceControlId) {

            List<Control> dependencies = new List<Control> ();

            foreach (Server.Application.FormControlDataBinding currentDataBinding in dataBindings) {

                if (currentDataBinding.DataSourceControlId == dataSourceControlId) {

                    if (!dependencies.Contains (this)) {

                        dependencies.Add (this);

                        dependencies.AddRange (Form.DataBindingDependencies (controlId));

                    }

                }

            }

            foreach (Control currentChildControl in Controls) {

                dependencies.AddRange (currentChildControl.DataBindingDependencies (dataSourceControlId));

            }

            return dependencies;

        }


        virtual public List<Control> GetDataSources () {

            List<Control> dataSources = new List<Control> ();

            if (Capabilities.IsDataSource) {

                dataSources.Add (this);

                if (!dataBindingContextsIsLoaded) {

                    //DataBindingContexts ((Application.CoreObject_DataBindingContexts_Completed) null);

                }

            }

            foreach (Control currentControl in Controls) {

                List<Control> childDataSources = currentControl.GetDataSources ();

                if (childDataSources.Count > 0) {

                    dataSources.AddRange (childDataSources);

                }

            }

            return dataSources;

        }

        public List<Server.Application.FormControlDataBinding> GetDataBindings (Guid forControlId) {

            List<Server.Application.FormControlDataBinding> bindingSubset = new List<Server.Application.FormControlDataBinding> ();

            foreach (Server.Application.FormControlDataBinding currentDataBinding in dataBindings) {

                if (currentDataBinding.DataSourceControlId == forControlId) {

                    bindingSubset.Add (currentDataBinding);

                }

            }

            return bindingSubset;

        }

        public Server.Application.FormControlDataBinding GetDataBinding (String forProperty) {

            foreach (Server.Application.FormControlDataBinding currentBinding in DataBindings) {

                if (currentBinding.BoundProperty == forProperty) { return currentBinding; }

            }

            return null;

        }

        public virtual void OnDataSourceChanged (Control dataSourceControl, Boolean propogate) {

            if (propogate) {

                foreach (Control currentChildControl in Controls) {

                    currentChildControl.OnDataSourceChanged (dataSourceControl, propogate);

                }

            }

            return;

        }

        public void DataSourceChanged () {

            if (Form != null) {

                if (Form.DataBindingDependencies (controlId).Count != 0) {

                    Form.OnDataSourceChanged (this, true);

                }

            }

            return;

        }


        public Boolean ContainsDataBinding (Guid forControlId) {

            foreach (Server.Application.FormControlDataBinding currentDataBinding in dataBindings) {

                if (currentDataBinding.DataSourceControlId == forControlId) { return true; }

            }

            return false;

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


        #region Events

        #region Private Properties

        protected Boolean eventsIsLoaded = false;

        // private Boolean eventsIsWaiting = false;

        private Application.FormControl_Events_Completed eventsCallback = null;

        protected ObservableCollection<String> events = new ObservableCollection<String> ();

        #endregion 


        #region Public Properties

        public Boolean EventsIsLoaded { get { return eventsIsLoaded; } }

        #endregion 


        public void Events (Application.FormControl_Events_Completed eventHandler) {

            eventsIsLoaded = false;

            if (eventsIsLoaded) {

                Server.Application.FormControl_EventsCompletedEventArgs completedEventArgs;

                completedEventArgs = new Server.Application.FormControl_EventsCompletedEventArgs (

                    new Object[] { events}, null, false, null);

                if (eventHandler != null) { eventHandler (this, completedEventArgs); }

            }

            else {

                // eventsIsWaiting = true;

                eventsCallback = eventHandler;

                Server.Application.Form serverForm = (Server.Application.Form) Form.ToServerObject ();

                application.FormControl_Events (serverForm, controlId, Events_Completed);

            }

            return;

        }

        public void Events_Completed (Object sender, Server.Application.FormControl_EventsCompletedEventArgs eventArgs) {

            events = eventArgs.Result;

            eventsIsLoaded = true;

            if (eventsCallback != null) {

                eventsCallback (sender, eventArgs);

            }

            // eventsIsWaiting = false;

            return;

        }


        public Int32 EventHandlerCount { get { return eventHandlers.Count; } }

        public Boolean HasEventHandler (String eventName) {

            foreach (Server.Application.FormControlEventHandler currentEventHandler in eventHandlers) {

                if (currentEventHandler.EventName == eventName) {

                    return true;

                }

            }

            return false;

        }

        #endregion 


        #region Server Update - Update Control

        public virtual void UpdateControl (Server.Application.FormControl serverControl) {

            if (controlId != serverControl.ControlId) { return; }


            // ONLY UPDATE SUPPORTED PROPERTIES, SOME BASE PROPERTIES CANNOT BE UPDATED (NO STRUCTURE CHANGES SUPPORTED)

            TabIndex = serverControl.TabIndex;

            Enabled = serverControl.Enabled;

            Visible = serverControl.Visible;

            ReadOnly = serverControl.ReadOnly;

            Required = serverControl.Required;

            Position = serverControl.Position;

            Capabilities = serverControl.Capabilities;


            CreateAccountInfo = serverControl.CreateAccountInfo;

            ModifiedAccountInfo = serverControl.ModifiedAccountInfo;


            foreach (Server.Application.FormControl currentChildControl in serverControl.Controls) {

                Control childControl = GetChildControl (currentChildControl.ControlId);

                if (childControl != null) {

                    childControl.UpdateControl (currentChildControl);

                }

            }

            return;

        }

        #endregion 

    }

}
