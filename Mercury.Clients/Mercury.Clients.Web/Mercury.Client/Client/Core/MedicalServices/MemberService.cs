using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.MedicalServices {

    [Serializable]
    public class MemberService : CoreObject {

        #region Private Properties
        
        private Int64 memberId;

        private Int64 serviceId;

        private DateTime eventDate = new DateTime (1900, 01, 01);

        private Boolean addedManually = false;

        private Service service = null;

        #endregion

        

        #region Public Properties
        
        public Int64 MemberId { get { return memberId; } set { memberId = value; } }

        public Int64 ServiceId { get { return serviceId; } set { serviceId = value; } }

        public DateTime EventDate { get { return eventDate; } set { eventDate = value; } }

        public Boolean AddedManually { get { return addedManually; } set { addedManually = value; } }

        public Service Service {

            get {

                if (service != null) { return service; }

                if (Application == null) { return null; }

                service = Application.MedicalServiceGet (serviceId, true);

                return service;

            }

            set { service = value; }

        }

        public String ServiceName { get { return (Service != null) ? Service.Name : String.Empty; } }

        #endregion



        #region Constructors

        public MemberService () { return; }

        public MemberService (Application applicationReference) { base.BaseConstructor (applicationReference);  return; }

        public MemberService (Application applicationReference, Mercury.Server.Application.MemberService serverService) {

            base.BaseConstructor (applicationReference, serverService);

            
            memberId = serverService.MemberId;

            serviceId = serverService.ServiceId;

            eventDate = serverService.EventDate;

            addedManually = serverService.AddedManually;

            if (serverService.Service != null) {

                service = new Service (applicationReference, serverService.Service);

            }

            else { service = null; }


            createAccountInfo = serverService.CreateAccountInfo;

            modifiedAccountInfo = serverService.ModifiedAccountInfo;

            return;

        }

        #endregion


        #region Public Methods

        public Boolean IsEqual (MemberService compareService) {

            Boolean isEqual = true;

            isEqual = isEqual && (this.memberId == compareService.MemberId);

            isEqual = isEqual && (this.serviceId == compareService.ServiceId);

            isEqual = isEqual && (this.eventDate == compareService.EventDate);

            return isEqual;

        }

        #endregion

    }

}
