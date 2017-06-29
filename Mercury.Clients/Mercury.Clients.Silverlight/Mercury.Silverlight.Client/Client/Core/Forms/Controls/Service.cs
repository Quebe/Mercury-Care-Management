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
    public class Service : Control {

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

        #endregion


        #region Public Properties

        public Int64 MemberId {

            get { return memberId; }

            set {

                if (memberId != value) {

                    memberId = value;

                    NotifyPropertyChanged ("MemberId");

                    // TODO: DATA SOURCE CHANGED (RAISE EVENT)

                    //DataSourceChanged ();

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

                    NotifyPropertyChanged ("ServiceId");

                    // TODO: DATA SOURCE CHANGED (RAISE EVENT)


                    //if (Application != null) {

                    //    Core.MedicalServices.Service service = Application.MedicalServiceGet (serviceId, true);

                    //    if (service != null) { serviceName = service.Name; }

                    //}

                    //DataSourceChanged ();

                }

            }

        }

        public String ServiceName { 
            
            get {

                if (String.IsNullOrEmpty (serviceName)) { return "** No Service Available"; }

                return serviceName;
            
            } 
        
        }

        public DateTime? ServiceDate {

            get { return serviceDate; }

            set {

                if ((!serviceDate.HasValue) && (value == null)) { return; }

                if (serviceDate != value) {

                    serviceDate = value;

                    // if (GetEventHandler ("ServiceDateChanged") != null) { RaiseEvent ("ServiceDateChanged"); }

                }

            }

        }

        public Boolean ServiceDateVisible {

            get { return serviceDateVisible; }

            set {

                if (serviceDateVisible != value) {

                    serviceDateVisible = value;

                    NotifyPropertyChanged ("ServiceDateVisible");

                }

            }

        }


        public Int64 MostRecentMemberServiceId { get { return mostRecentMemberServiceId; } set { mostRecentMemberServiceId = value; } }

        public DateTime? MostRecentMemberServiceDate { get { return mostRecentMemberServiceDate; } set { mostRecentMemberServiceDate = value; } }

        public Boolean MostRecentMemberServiceDateVisible {

            get { return mostRecentMemberServiceDateVisible; }

            set {

                if (mostRecentMemberServiceDateVisible != value) {

                    mostRecentMemberServiceDateVisible = value;

                    NotifyPropertyChanged ("MostRecentMemberServiceDateVisible");

                }

            }

        }


        public Boolean HasService { get { return (serviceDate.HasValue); } }

        public Boolean HasMostRecentMemberService { get { return (mostRecentMemberServiceDate.HasValue); } }


        public DateTime ServiceDateValue { get { return (HasService) ? serviceDate.Value : new DateTime (); } }

        public override Boolean HasValue { get { return HasService; } }

        public override String Value { get { return (HasValue) ? serviceDate.Value.ToString ("MM/dd/yyyy") : String.Empty; } }

        #endregion


        #region Silverlight Public Properties

        public String ServiceLastDateText {

            get {

                if (!HasMostRecentMemberService) { return "{ never }"; }

                return mostRecentMemberServiceDate.Value.ToString ("MM/dd/yyyy");

            }

        }

        public Visibility ServiceLastDateVisibility {

            get {

                if (mostRecentMemberServiceDateVisible) { return Visibility.Visible; }

                return Visibility.Collapsed;

            }

        }

        public Visibility ServiceDateVisibility {

            get {

                if (serviceDateVisible) { return Visibility.Visible; }

                return Visibility.Collapsed;

            }

        }

        #endregion 
        

        #region Constructors

        virtual protected void InitializeControl (Application applicationReference) {

            BaseConstructor (applicationReference);

            ControlType = Server.Application.FormControlType.Service;

            capabilities.HasValue = true;

            capabilities.HasLabel = true;

            capabilities.CanDataBind = true;

            capabilities.IsDataSource = true;

            label = new Label (Application, this);

            return;

        }

        override public void BaseConstructor (Mercury.Client.Core.Forms.Control parentControl, Server.Application.FormControl serverControl) {

            base.BaseConstructor (parentControl, serverControl);


            Server.Application.FormControlService serverService = (Server.Application.FormControlService) serverControl;


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

        public Service (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Server.Application.FormControlService serverService) {

            InitializeControl (applicationReference);

            BaseConstructor (parentControl, serverService);

            ChildServerControlsToLocal (this, serverService);

            return;

        }

        #endregion


        #region Silverlight Data Bindings and Async Operations

        protected override void NotifyPropertyChanged (String propertyName) {

            if (String.IsNullOrEmpty (propertyName)) { return; }


            switch (propertyName) { 

                case "ServiceId": 

                    base.NotifyPropertyChanged ("ServiceId");

                    base.NotifyPropertyChanged ("ServiceName");

                    base.NotifyPropertyChanged ("ServiceNameText");

                    break;

                case "MostRecentMemberServiceDateVisible":

                    base.NotifyPropertyChanged ("MostRecentMemberServiceDateVisible");

                    base.NotifyPropertyChanged ("ServiceLastDateVisibility");

                    break;

                case "ServiceDateVisible":

                    base.NotifyPropertyChanged ("ServiceDateVisible");

                    base.NotifyPropertyChanged ("ServiceDateVisibility");

                    break;
           
                default:

                    base.NotifyPropertyChanged (propertyName);

                    break;

            }

            return;

        }

        #endregion 


        #region Virtual Overrides

        public override void LocalControlToServer (Server.Application.FormControl parentControl, Server.Application.FormControl serverControl) {

            base.LocalControlToServer (parentControl, serverControl);

            ((Server.Application.FormControlService) serverControl).MemberId = MemberId;

            ((Server.Application.FormControlService) serverControl).MemberServiceId = MemberServiceId;

            ((Server.Application.FormControlService) serverControl).ServiceId = serviceId;

            ((Server.Application.FormControlService) serverControl).ServiceName = serviceName;

            ((Server.Application.FormControlService) serverControl).ServiceDate = serviceDate;

            ((Server.Application.FormControlService) serverControl).ServiceDateVisible = serviceDateVisible;

            ((Server.Application.FormControlService) serverControl).MostRecentMemberServiceId = MostRecentMemberServiceId;

            ((Server.Application.FormControlService) serverControl).MostRecentMemberServiceDate = MostRecentMemberServiceDate;

            ((Server.Application.FormControlService) serverControl).MostRecentMemberServiceDateVisible = mostRecentMemberServiceDateVisible;


            ((Server.Application.FormControlService) serverControl).Label = new Server.Application.FormControlLabel ();

            label.LocalControlToServer (serverControl, ((Server.Application.FormControlService) serverControl).Label);

            return;

        }

        #endregion

    }
}
