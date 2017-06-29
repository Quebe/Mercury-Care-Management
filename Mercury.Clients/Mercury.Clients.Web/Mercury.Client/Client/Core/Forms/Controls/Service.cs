using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Forms.Controls {

    [Serializable]
    public class Service : Mercury.Client.Core.Forms.Control {

        #region Private Properties

        private Int64 memberId;

        private Int64 memberServiceId;

        private Int64 serviceId;

        private String serviceName;

        private DateTime? serviceDate = null;

        private Boolean serviceDateVisible = true;

        private Int64 mostRecentMemberServiceId;

        private DateTime? mostRecentMemberServiceDate;

        private Boolean mostRecentMemberServiceDateVisible = true;


        private Client.Core.MedicalServices.Service medicalService = null; 

        #endregion


        #region Public Properties

        public Int64 MemberId { 
            
            get { return memberId; } 
            
            set {

                if (memberId != value) {

                    memberId = value;

                    DataSourceChanged ();

                }

            } 
        
        }

        public Int64 MemberServiceId { get { return memberServiceId; } set { memberServiceId = value; } }

        public Int64 ServiceId { 
            
            get { return serviceId; } 
            
            set {

                if (serviceId != value) {

                    serviceId = value;

                    serviceName = String.Empty;

                    if (Application != null) {

                        Core.MedicalServices.Service service = Application.MedicalServiceGet (serviceId, true);

                        if (service != null) { serviceName = service.Name; }

                    }

                    DataSourceChanged ();

                }
                
            } 
        
        }

        public String ServiceName { get { return serviceName; } set { serviceName = value; } }

        public DateTime? ServiceDate {

            get { return serviceDate; }

            set {

                if ((!serviceDate.HasValue) && (value == null)) { return; }

                if (serviceDate != value) {

                    serviceDate = value;

                    if (GetEventHandler ("ServiceDateChanged") != null) { RaiseEvent ("ServiceDateChanged"); }

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


        public override Boolean HasValue { get { return HasService; } }
        
        public override String Value { get { return (HasValue) ? serviceDate.Value.ToString ("MM/dd/yyyy") : String.Empty; } }

        public Client.Core.MedicalServices.Service MedicalService { get { if (medicalService == null) { medicalService = application.MedicalServiceGet (serviceId, true); } return medicalService; } }


        public override String JsonExtendedProperties {

            get {

                StringBuilder jsonBuilder = new StringBuilder ();


                jsonBuilder.Append (", " + JsonObjectProperty ("MemberId", memberId));

                jsonBuilder.Append (", " + JsonObjectProperty ("MemberServiceId", memberServiceId));

                jsonBuilder.Append (", " + JsonObjectProperty ("ServiceId", serviceId));

                jsonBuilder.Append (", " + JsonObjectProperty ("ServiceName", serviceName));

                if (serviceDate.HasValue) { jsonBuilder.Append (", " + JsonObjectProperty ("ServiceDate", serviceDate.Value)); }

                jsonBuilder.Append (", " + JsonObjectProperty ("ServiceDateVisible", serviceDateVisible));

                jsonBuilder.Append (", " + JsonObjectProperty ("MostRecentMemberServiceId", mostRecentMemberServiceId));

                if (mostRecentMemberServiceDate.HasValue) { jsonBuilder.Append (", " + JsonObjectProperty ("MostRecentMemberServiceDate", mostRecentMemberServiceDate.Value)); }

                jsonBuilder.Append (", " + JsonObjectProperty ("MostRecentMemberServiceDateVisible", mostRecentMemberServiceDateVisible));

                if (MedicalService != null) { jsonBuilder.Append (", " + JsonObjectProperty ("MedicalServiceName", MedicalService.Name)); }

                return jsonBuilder.ToString ();

            }

        }

        #endregion

        
        #region Constructors

        virtual protected void InitializeControl (Application applicationReference) {

            BaseConstructor (applicationReference);

            ControlType = Mercury.Server.Application.FormControlType.Service;

            capabilities.HasValue = true;

            capabilities.HasLabel = true;

            capabilities.CanDataBind = true;

            capabilities.IsDataSource = true;

            label = new Label (Application, this);

            return;

        }

        override public void BaseConstructor (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Mercury.Server.Application.FormControl serverControl) {

            base.BaseConstructor (applicationReference, parentControl, serverControl);


            Mercury.Server.Application.FormControlService serverService = (Mercury.Server.Application.FormControlService) serverControl;


            memberId = serverService.MemberId;

            memberServiceId = serverService.MemberServiceId;

            serviceId = serverService.ServiceId;

            serviceName = serverService.ServiceName;

            serviceDate = serverService.ServiceDate;

            serviceDateVisible = serverService.ServiceDateVisible;

            mostRecentMemberServiceId = serverService.MostRecentMemberServiceId;

            mostRecentMemberServiceDate = serverService.MostRecentMemberServiceDate;

            mostRecentMemberServiceDateVisible = serverService.MostRecentMemberServiceDateVisible;
            

            label = new Label (Application, this, serverService.Label);

            return;

        }


        public Service (Application applicationReference) {

            InitializeControl (applicationReference);

            return;

        }

        public Service (Application applicationReference, String labelText) {

            InitializeControl (applicationReference);

            label.Text = labelText;

            return;

        }

        public Service (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Mercury.Server.Application.FormControlService serverService) {

            InitializeControl (applicationReference);

            BaseConstructor (applicationReference, parentControl, serverService);

            ChildServerControlsToLocal (this, serverService);

            return;

        }

        #endregion


        #region Virtual Overrides

        public override void LocalControlToServer (Mercury.Server.Application.FormControl parentControl, Mercury.Server.Application.FormControl serverControl) {

            base.LocalControlToServer (parentControl, serverControl);

            ((Mercury.Server.Application.FormControlService) serverControl).MemberId = MemberId;

            ((Mercury.Server.Application.FormControlService) serverControl).MemberServiceId = MemberServiceId;

            ((Mercury.Server.Application.FormControlService) serverControl).ServiceId = serviceId;

            ((Mercury.Server.Application.FormControlService) serverControl).ServiceName = serviceName;

            ((Mercury.Server.Application.FormControlService) serverControl).ServiceDate = serviceDate;

            ((Mercury.Server.Application.FormControlService) serverControl).ServiceDateVisible = serviceDateVisible;

            ((Mercury.Server.Application.FormControlService) serverControl).MostRecentMemberServiceId = MostRecentMemberServiceId;

            ((Mercury.Server.Application.FormControlService) serverControl).MostRecentMemberServiceDate = MostRecentMemberServiceDate;

            ((Mercury.Server.Application.FormControlService) serverControl).MostRecentMemberServiceDateVisible = mostRecentMemberServiceDateVisible;


            ((Mercury.Server.Application.FormControlService) serverControl).Label = new Mercury.Server.Application.FormControlLabel ();

            label.LocalControlToServer (serverControl, ((Mercury.Server.Application.FormControlService) serverControl).Label);

            return;

        }

        #endregion

    }

}
