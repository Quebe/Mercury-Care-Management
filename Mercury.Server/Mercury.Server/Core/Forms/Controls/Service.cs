using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Forms.Controls {

    [Serializable]
    [DataContract (Name = "FormControlService")]
    public class Service : Mercury.Server.Core.Forms.Control {

        #region Properties

        [DataMember (Name = "MemberId")]
        private Int64 memberId;

        [DataMember (Name = "MemberServiceId")]
        private Int64 memberServiceId;

        [DataMember (Name = "ServiceId")]
        private Int64 serviceId = 0;

        [DataMember (Name = "ServiceName")]
        private String serviceName;

        [DataMember (Name = "ServiceDate")]
        private DateTime? serviceDate = null;

        [DataMember (Name = "ServiceDateVisible")]
        private Boolean serviceDateVisible = true;

        [DataMember (Name = "MostRecentMemberServiceId")]
        private Int64 mostRecentMemberServiceId;

        [DataMember (Name = "MostRecentMemberServiceDate")]
        private DateTime? mostRecentMemberServiceDate;

        [DataMember (Name = "MostRecentMemberServiceDateVisible")]
        private Boolean mostRecentMemberServiceDateVisible = true;


        [NonSerialized]
        private Member.Member member = null;

        [NonSerialized]
        private Core.MedicalServices.Service service = null;

        #endregion


        #region Public Properties

        public Int64 MemberId { 
            
            get { return memberId; } 
            
            set {

                memberId = value;

                member = null;

                mostRecentMemberServiceId = 0;

                mostRecentMemberServiceDate = null;

                if ((MedicalService != null) && (Member != null)) {

                    Core.MedicalServices.MemberService memberService = member.MostRecentMemberService (MedicalService.Name);

                    if (memberService != null) {

                        mostRecentMemberServiceId = memberService.Id;

                        mostRecentMemberServiceDate = memberService.EventDate;

                    }

                }
            
            } 
        
        }

        public Int64 MemberServiceId { get { return memberServiceId; } set { memberServiceId = value; } }

        public Int64 ServiceId {

            get { return serviceId; }

            set {

                if (serviceId != value) {

                    serviceId = value;

                    service = null;

                    if (application != null) {

                        serviceName = application.CoreObjectGetNameById ("Service", serviceId);

                    }

                    else { serviceName = String.Empty; }

                }

            }

        }

        public String ServiceName {

            get {

                if (String.IsNullOrEmpty (serviceName)) {

                    if (MedicalService != null) { serviceName = MedicalService.Name; }

                    else { serviceName = String.Empty; }

                }

                return serviceName;

            }


            set { serviceName = value; }
        }

        public DateTime? ServiceDate { 
            
            get { return serviceDate; }

            set {

                if ((!serviceDate.HasValue) && (value == null)) { return; }

                if (serviceDate != value) {

                    serviceDate = value;

                    ValueChanged ();

                }

            }
            
        }

        public Boolean ServiceDateVisible { get { return serviceDateVisible; } set { serviceDateVisible = value; } }

        public DateTime ServiceDateValue { get { return (HasService) ? serviceDate.Value : new DateTime (); } }

        public Int64 MostRecentMemberServiceId { get { return mostRecentMemberServiceId; } set { mostRecentMemberServiceId = value; } }

        public DateTime? MostRecentMemberServiceDate { get { return mostRecentMemberServiceDate; } set { mostRecentMemberServiceDate = value; } }

        public Boolean MostRecentMemberServiceDateVisible { get { return mostRecentMemberServiceDateVisible; } set { mostRecentMemberServiceDateVisible = value; } }


        public Boolean HasService { get { return (serviceDate.HasValue); } }

        public Boolean HasMostRecentMemberService { get { return (mostRecentMemberServiceDate.HasValue); } }


        public override System.Xml.XmlDocument ExtendedPropertiesXml {

            get {

                System.Xml.XmlDocument extendedPropertiesXml = base.ExtendedPropertiesXml;

                ExtendedProperties_AddProperty (extendedPropertiesXml, "MemberId", memberId.ToString ());

                ExtendedProperties_AddProperty (extendedPropertiesXml, "MemberServiceId", memberServiceId.ToString ());

                ExtendedProperties_AddProperty (extendedPropertiesXml, "ServiceId", serviceId.ToString ());

                ExtendedProperties_AddProperty (extendedPropertiesXml, "ServiceName", ServiceName);

                ExtendedProperties_AddProperty (extendedPropertiesXml, "ServiceDate", (HasService) ? ServiceDateValue.ToString ("MM/dd/yyyy") : String.Empty);

                ExtendedProperties_AddProperty (extendedPropertiesXml, "ServiceDateVisible", serviceDateVisible.ToString ());

                ExtendedProperties_AddProperty (extendedPropertiesXml, "MostRecentMemberServiceId", mostRecentMemberServiceId.ToString ());

                ExtendedProperties_AddProperty (extendedPropertiesXml, "MostRecentMemberServiceDate", (mostRecentMemberServiceDate.HasValue) ? mostRecentMemberServiceDate.Value.ToString ("MM/dd/yyyy") : String.Empty);

                ExtendedProperties_AddProperty (extendedPropertiesXml, "MostRecentMemberServiceDateVisible", mostRecentMemberServiceDateVisible.ToString ());


                System.Xml.XmlNode rootNode = extendedPropertiesXml.GetElementsByTagName ("ExtendedProperties")[0];


                return extendedPropertiesXml;

            }

        }

        public override void ExtendedPropertiesDeserialize (System.Xml.XmlNode extendedPropertiesXml) {

            DateTime parsedDateTime;

            base.ExtendedPropertiesDeserialize (extendedPropertiesXml);

            // foreach (System.Xml.XmlNode currentPropertyNode in extendedPropertiesXml.SelectNodes ("./Property")) {


            foreach (System.Xml.XmlNode currentPropertyNode in extendedPropertiesXml.ChildNodes) {

                switch (currentPropertyNode.Attributes["Name"].InnerText) {

                    case "MemberId": memberId = Convert.ToInt64 (currentPropertyNode.InnerText); break;

                    case "MemberServiceId": memberServiceId = Convert.ToInt64 (currentPropertyNode.InnerText); break;

                    case "ServiceId":

                        ServiceId = Convert.ToInt64 (currentPropertyNode.InnerText);

                        break;

                    case "ServiceName":

                        ServiceId = application.CoreObjectGetIdByName ("Service", currentPropertyNode.InnerText);

                        if (serviceId != 0) { serviceName = currentPropertyNode.InnerText; }

                        break;

                    case "ServiceDate":

                        if (DateTime.TryParse (currentPropertyNode.InnerText, out parsedDateTime)) {

                            serviceDate = parsedDateTime;
                        }

                        else { serviceDate = null; }

                        break;

                    case "ServiceDateVisible": serviceDateVisible = Convert.ToBoolean (currentPropertyNode.InnerText); break;

                    case "MostRecentMemberServiceId": mostRecentMemberServiceId = Convert.ToInt64 (currentPropertyNode.InnerText); break;

                    case "MostRecentMemberServiceDate":

                        if (DateTime.TryParse (currentPropertyNode.InnerText, out parsedDateTime)) {

                            mostRecentMemberServiceDate = parsedDateTime;
                        }

                        else { mostRecentMemberServiceDate = null; }

                        break;

                    case "MostRecentMemberServiceDateVisible": Convert.ToBoolean (currentPropertyNode.InnerText); break;

                }

            }

            return;

        }


        public override Boolean HasValue { get { return HasService; } }

        public override String Value { get { return (HasValue) ? serviceDate.Value.ToString ("MM/dd/yyyy") : String.Empty; } }


        public override Application Application {

            set {

                base.Application = value;

                if (label != null) { label.Application = value; }

            }

        }


        public Member.Member Member { 

            get { 

                if (member != null) { return member; }

                if (application == null) { return null; }

                member = application.MemberGet (memberId);

                return member;

            }

        }

        public Core.MedicalServices.Service MedicalService {

            get {

                if (service != null) { return service; }

                if (application == null) { return null; }

                service = application.MedicalServiceGet (serviceId);

                return service;

            }

        }

        #endregion  
        

        #region Constructors

        protected void InitializeControl (Application applicationReference) {

            BaseConstructor (applicationReference);

            controlType = Mercury.Server.Core.Forms.Enumerations.FormControlType.Service;

            capabilities.HasValue = true;

            capabilities.HasLabel = true;

            capabilities.IsDataSource = true;

            capabilities.CanDataBind = true;

            return;            

        }

        public Service (Application applicationReference) {

            InitializeControl (applicationReference);

        }

        public Service (Application applicationReference, String labelText) {

            InitializeControl (applicationReference);

            label = new Label (application, labelText);

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

                events.Add ("ServiceDateChanged");

                return events;

            }

        }

        public override void ValueChanged () {

            RaiseEvent ("ServiceDateChanged");

            DataSourceChanged ();

            return;

        }

        #endregion 


        #region Data Bindings

        public override Dictionary<String, String> DataBindingContexts {

            get {

                Dictionary<String, String> bindingContexts = new Dictionary<String, String> ();

                bindingContexts.Add ("MemberId", "Id|Member");

                bindingContexts.Add ("MemberServiceId", "Id|MemberService");

                bindingContexts.Add ("MostRecentMemberServiceId", "Id|MemberService");

                bindingContexts.Add ("MostRecentMemberServiceDate", "DateTime");

                bindingContexts.Add ("ServiceDate", "DateTime");

                bindingContexts.Add ("ServiceId", "Id|Service");

                bindingContexts.Add ("ServiceName", "String");

                return bindingContexts;

            }

        }

        public override String EvaluateDataBinding (Structures.DataBinding dataBinding) {

            String dataValue = String.Empty;

            String bindingContextPart = dataBinding.BindingContext.Split ('.')[0];

            switch (bindingContextPart) {

                case "MemberId": dataValue = memberId.ToString (); break;

                case "MemberServiceId": dataValue = memberServiceId.ToString (); break;

                case "MostRecentMemberServiceId": dataValue = mostRecentMemberServiceId.ToString (); break;

                case "MostRecentMemberServiceDate": dataValue = (mostRecentMemberServiceDate.HasValue) ? mostRecentMemberServiceDate.Value.ToString ("MM/dd/yyyy") : String.Empty; break;

                case "ServiceDate": dataValue = (serviceDate.HasValue) ? serviceDate.Value.ToString ("MM/dd/yyyy") : String.Empty; break;

                case "ServiceId": dataValue = serviceId.ToString (); break;

                case "ServiceName": dataValue = ServiceName; break;

                default: dataValue = "!Error"; break;

            }

            return dataValue;

        }

        public override Dictionary<String, String> DataBindableProperties {

            get {

                Dictionary<String, String> bindableProperties = new Dictionary<String, String> ();

                bindableProperties.Add ("MemberId", "Id|Member");


                // BELOW WERE REMOVED IN V1

//                bindableProperties.Add ("MemberServiceId", "Id|MemberService");

//                bindableProperties.Add ("MostRecentMemberServiceId", "Id|MemberService");

//                bindableProperties.Add ("MostRecentMemberServiceDate", "DateTime");

//                bindableProperties.Add ("ServiceDate", "DateTime");

                bindableProperties.Add ("ServiceId", "Id|Service");

                bindableProperties.Add ("ServiceName", "String");

                return bindableProperties;

            }

        }

        public override void OnDataSourceChanged (Control dataSourceControl, Boolean propogate) {

            String dataValue = String.Empty;

            Int64 idValue = 0;

            base.OnDataSourceChanged (dataSourceControl, propogate);

            foreach (Mercury.Server.Core.Forms.Structures.DataBinding currentBinding in GetDataBindings (dataSourceControl.ControlId)) {

                switch (currentBinding.BoundProperty) {

                    case "MemberId":

                        #region Member Id

                        dataValue = dataSourceControl.EvaluateDataBinding (currentBinding);

                        if (Int64.TryParse (dataValue, out idValue)) {

                            MemberId = idValue;

                        }

                        #endregion

                        break;

                } // switch (currentBinding.BoundProperty) {

            } // foreach (Mercury.Services.Core.Forms.DataBinding currentBinding in GetDataBindings (dataSourceControl.Id)) {


            return;

        }

        #endregion

    
    }

}
