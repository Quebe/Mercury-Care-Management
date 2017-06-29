using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Forms.Controls {

    [Serializable]
    [DataContract (Name = "FormControlEntity")]
    public class Entity : Mercury.Server.Core.Forms.Control {

        #region Private Members

        [DataMember (Name = "EntityType")]
        protected Mercury.Server.Core.Enumerations.EntityType entityType = Mercury.Server.Core.Enumerations.EntityType.NotSpecified;

        [DataMember (Name = "EntityObjectId")]
        protected Int64 entityObjectId = 0;

        [DataMember (Name = "DisplayAgeFormat")]
        protected Enumerations.FormControlEntityDisplayAgeFormat displayAgeFormat = Mercury.Server.Core.Forms.Enumerations.FormControlEntityDisplayAgeFormat.InYears;

        [DataMember (Name = "DisplayStyle")]
        protected Enumerations.FormControlEntityDisplayStyle displayStyle = Mercury.Server.Core.Forms.Enumerations.FormControlEntityDisplayStyle.NormalExpanded;

        [DataMember (Name = "AllowCustomEntityName")]
        private Boolean allowCustomEntityName = false;

        [DataMember (Name = "EntityName")]
        private String entityName = String.Empty;

        [NonSerialized]
        private Member.Member member = null;

        [NonSerialized]
        private Provider.Provider provider = null;
 
        #endregion


        #region Public Properties

        public Mercury.Server.Core.Enumerations.EntityType EntityType { 
            
            get { return entityType; } 
                       
            set {

                if (entityType == value) { return; }

                entityType = value; 
            
            }
        
        }
        
        public Int64 EntityObjectId { 
            
            get { return entityObjectId; } 
            
            set {

                if (entityObjectId == value) { return; }

                entityObjectId = value;

                member = null;

                provider = null;

                if (GetEventHandler ("EntityChanged") != null) {

                    RaiseEvent ("EntityChanged"); 

                }

                DataSourceChanged (); 
            
            } 

        }

        public Enumerations.FormControlEntityDisplayAgeFormat DisplayAgeFormat { get { return displayAgeFormat; } set { displayAgeFormat = value; } }

        public Enumerations.FormControlEntityDisplayStyle DisplayStyle { get { return displayStyle; } set { displayStyle = value; } }

        public Boolean AllowCustomEntityName { get { return allowCustomEntityName; } set { allowCustomEntityName = value; } }

        public String EntityName {

            get {

                if (!String.IsNullOrEmpty (entityName)) { return entityName; }

                Mercury.Server.Core.Forms.Structures.DataBinding nameBinding = new Mercury.Server.Core.Forms.Structures.DataBinding ();

                nameBinding.BindingContext = "Entity.Name";

                entityName = EvaluateDataBinding (nameBinding);

                return entityName;

            }

            set {

                if ((allowCustomEntityName) && (entityObjectId == 0)) { entityName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); }

            }

        }


        public override System.Xml.XmlDocument ExtendedPropertiesXml {

            get {

                System.Xml.XmlDocument extendedProperties = base.ExtendedPropertiesXml;

                ExtendedProperties_AddProperty (extendedProperties, "EntityType", ((Int32) entityType).ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "EntityObjectId", entityObjectId.ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "DisplayAgeFormat", ((Int32) displayAgeFormat).ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "DisplayStyle", ((Int32) displayStyle).ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "AllowCustomEntityName", allowCustomEntityName.ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "EntityName", EntityName);

                return extendedProperties;

            }

        }

        public override void ExtendedPropertiesDeserialize (System.Xml.XmlNode extendedPropertiesXml) {

            base.ExtendedPropertiesDeserialize (extendedPropertiesXml);

            // foreach (System.Xml.XmlNode currentPropertyNode in extendedPropertiesXml.SelectNodes ("./Property")) {

            foreach (System.Xml.XmlNode currentPropertyNode in extendedPropertiesXml.ChildNodes) {

                switch (currentPropertyNode.Attributes["Name"].InnerText) {

                    case "EntityType": entityType = (Mercury.Server.Core.Enumerations.EntityType) Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "EntityId": entityObjectId = Convert.ToInt64 (currentPropertyNode.InnerText); break; // BACKWARDS COMPATIBILITY

                    case "EntityObjectId": entityObjectId = Convert.ToInt64 (currentPropertyNode.InnerText); break;

                    case "DisplayAgeFormat": displayAgeFormat = (Mercury.Server.Core.Forms.Enumerations.FormControlEntityDisplayAgeFormat) Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "DisplayStyle": displayStyle = (Mercury.Server.Core.Forms.Enumerations.FormControlEntityDisplayStyle) Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "AllowCustomEntityName": allowCustomEntityName = Convert.ToBoolean (currentPropertyNode.InnerText); break;

                    case "EntityName": entityName = currentPropertyNode.InnerText; break;

                }

            }

            return;

        }


        public override Boolean HasValue { 
            
            get {

                Boolean hasValue = (entityObjectId != 0);

                if (!hasValue) { hasValue = ((allowCustomEntityName) && (!String.IsNullOrEmpty (entityName.Trim ()))); }

                return hasValue;
            
            } 
        
        }

        public override String Value { get { return (HasValue) ? entityObjectId.ToString () : String.Empty; } }


        public Member.Member Member {

            get {

                if (entityType != Mercury.Server.Core.Enumerations.EntityType.Member) { return null; }

                if (member != null) { return member; }

                if (entityObjectId == 0) { return null; }

                if (application == null) { return null; }

                member = application.MemberGet (entityObjectId);

                return member;

            }

        }

        public Provider.Provider Provider {

            get {

                if (entityType != Mercury.Server.Core.Enumerations.EntityType.Provider) { return null; }

                if (provider != null) { return provider; }

                if (entityObjectId == 0) { return null; }

                if (application == null) { return null; }

                provider = application.ProviderGet (entityObjectId);

                return provider;

            }

        }

        public override Application Application {

            set {

                base.Application = value;

                if (label != null) { label.Application = value; }

            }

        }

        #endregion


        #region Constructors

        protected void InitializeControl (Application applicationReference) {

            Application = applicationReference;

            controlType = Mercury.Server.Core.Forms.Enumerations.FormControlType.Entity;

            capabilities.HasValue = true;

            capabilities.HasLabel = true;

            capabilities.IsDataSource = true;

            capabilities.CanDataBind = false;

            label = new Label (application);

            label.Visible = false;

            return;

        }

        public Entity (Application applicationReference) {

            InitializeControl (applicationReference);

            return;

        }

        public Entity (Application applicationReference, Mercury.Server.Core.Enumerations.EntityType forEntityType) {

            InitializeControl (applicationReference);

            entityType = forEntityType;

            return;

        }

        public Entity (Application applicationReference, String labelText) {

            InitializeControl (applicationReference);

            label = new Label (application, labelText);

            return;

        }

        #endregion


        #region Event Handlers

        public override List<String> Events {

            get {

                List<String> events = new List<String> ();

                events.Add ("EntityChanged");

                return events;

            }

        }

        #endregion


        #region Public Methods

        #endregion


        #region Compile Methods

        public override List<CompileMessage> Compile () {

            List<CompileMessage> compileMessages = new List<CompileMessage> ();


            compileMessages.AddRange (base.Compile ());

            return compileMessages;

        }

        #endregion


        #region Data Bindings

        public override Dictionary<String, String> DataBindableProperties {

            get {

                Dictionary<String, String> bindableProperties = new Dictionary<String, String> ();

                bindableProperties.Add (entityType.ToString () + "Id", "Id|" + entityType.ToString ());

                return bindableProperties;

            }

        }

        public override Dictionary<String, String> DataBindingContexts {

            get {

                Dictionary<String, String> bindingContexts = new Dictionary<String, String> ();

                switch (entityType) {

                    case Mercury.Server.Core.Enumerations.EntityType.Member:

                        bindingContexts = new Member.Member (null).DataBindingContexts;

                        break;

                    case Mercury.Server.Core.Enumerations.EntityType.Provider:

                        bindingContexts = new Provider.Provider (null).DataBindingContexts;

                        break;

                }

                return bindingContexts;

            }

        }

        public override void OnDataSourceChanged (Control dataSourceControl, Boolean propogate) {

            String dataValue = String.Empty;

            base.OnDataSourceChanged (dataSourceControl, propogate);

            foreach (Mercury.Server.Core.Forms.Structures.DataBinding currentBinding in GetDataBindings (dataSourceControl.ControlId)) {

                switch (currentBinding.BoundProperty) {

                    case "MemberId":

                    case "ProviderId":

                    case "SponsorId":

                    case "InsurerId":

                        dataValue = dataSourceControl.EvaluateDataBinding (currentBinding);

                        Int64 forEntityId = 0;

                        Int64.TryParse (dataValue, out forEntityId);

                        EntityObjectId = forEntityId;

                        break;

                }

            }

            return;

        }

        public override String EvaluateDataBinding (Structures.DataBinding dataBinding) {

            String dataValue = String.Empty;

            try {

                if (entityObjectId == 0) { return String.Empty; }

                switch (entityType) {

                    case Mercury.Server.Core.Enumerations.EntityType.Member:

                        #region Member Entity

                        if (Member == null) { dataValue = "!Error"; }

                        else { dataValue = Member.EvaluateDataBinding (dataBinding.BindingContext); }

                        #endregion

                        break;

                    case Mercury.Server.Core.Enumerations.EntityType.Provider:

                        #region Provider Entity

                        if (Provider == null) { dataValue = "!Error"; }

                        else { dataValue = Provider.EvaluateDataBinding (dataBinding.BindingContext); }

                        #endregion

                        break;

                } // end switch

            } // end try

            catch (Exception applicationException) {

                System.Diagnostics.Trace.WriteLine (Name + ".EvaluateDataBinding: " + applicationException.Message);

                System.Diagnostics.Trace.Flush ();

                dataValue = "!Error";

            }

            return dataValue;

        }

        #endregion

    }

}
