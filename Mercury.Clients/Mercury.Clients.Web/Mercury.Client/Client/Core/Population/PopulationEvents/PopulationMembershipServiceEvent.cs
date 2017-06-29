using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Population.PopulationEvents {

    [Serializable]
    public class PopulationMembershipServiceEvent : CoreObject {

        #region Private Properties

        private Int64 populationMembershipId;

        private Int64 populationServiceEventId;

        private DateTime expectedEventDate;

        private Int64 memberServiceId;

        private DateTime? eventDate = null;

        private Int64? previousMemberServiceId = null;

        private DateTime? previousEventDate = null;

        private Int64 parentMembershipServiceEventId;

        private DateTime? parentMembershipServiceEventDate;

        private Int64? previousThresholdId;

        private DateTime? previousThresholdDate;

        private DateTime? nextThresholdDate;

        private Mercury.Server.Application.PopulationServiceEventStatus status = Mercury.Server.Application.PopulationServiceEventStatus.CompliantOrNoChange;

        #endregion


        #region Public Properties

        public Int64 PopulationMembershipId { get { return populationMembershipId; } set { populationMembershipId = value; } }

        public Int64 PopulationServiceEventId { get { return populationServiceEventId; } set { populationServiceEventId = value; } }

        public DateTime ExpectedEventDate { get { return expectedEventDate; } set { expectedEventDate = value; } }

        public Int64 MemberServiceId { get { return memberServiceId; } set { memberServiceId = value; } }

        public DateTime? EventDate { get { return eventDate; } set { eventDate = value; } }

        public Int64? PreviousMemberServiceId { get { return previousMemberServiceId; } set { previousMemberServiceId = value; } }

        public DateTime? PreviousEventDate { get { return previousEventDate; } set { previousEventDate = value; } }

        public Int64 ParentMembershipServiceEventId { get { return parentMembershipServiceEventId; } set { parentMembershipServiceEventId = value; } }

        public DateTime? ParentMembershipServiceEventDate { get { return parentMembershipServiceEventDate; } set { parentMembershipServiceEventDate = value; } }

        public Int64? PreviousThresholdId { get { return previousThresholdId; } set { previousThresholdId = value; } }

        public DateTime? PreviousThresholdDate { get { return previousThresholdDate; } set { previousThresholdDate = value; } }

        public DateTime? NextThresholdDate { get { return nextThresholdDate; } set { nextThresholdDate = value; } }

        public Mercury.Server.Application.PopulationServiceEventStatus Status { get { return status; } set { status = value; } }


        public String ServiceName {

            get {

                if (Application == null) { return String.Empty; }

                PopulationServiceEvent serviceEvent = Application.PopulationServiceEventGet (populationServiceEventId, true);

                if (serviceEvent == null) { return String.Empty; }

                return serviceEvent.ServiceName;

            }

        }

        public String StatusText {

            get {

                string statusText = String.Empty;

                switch (status) {

                    case Mercury.Server.Application.PopulationServiceEventStatus.CompliantOrNoChange: statusText = "Compliant"; break;

                    case Mercury.Server.Application.PopulationServiceEventStatus.Open: statusText = "Open"; break;

                    case Mercury.Server.Application.PopulationServiceEventStatus.OpenInformational: statusText = "Open - Informational"; break;

                    case Mercury.Server.Application.PopulationServiceEventStatus.OpenWarning: statusText = "Open - Warning"; break;

                    case Mercury.Server.Application.PopulationServiceEventStatus.OpenCritical: statusText = "Open - Critical"; break;

                    default: statusText = "Open - Unknown"; break;

                }

                return statusText;

            }

        }

        #endregion


        #region Constructors

        public PopulationMembershipServiceEvent (Application application) {

            BaseConstructor (application);

            return;

        }

        public PopulationMembershipServiceEvent (Application application, Mercury.Server.Application.PopulationMembershipServiceEvent serverObject) {

            BaseConstructor (application);

            
            PopulationMembershipId = serverObject.PopulationMembershipId;

            PopulationServiceEventId = serverObject.PopulationServiceEventId;

            ExpectedEventDate = serverObject.ExpectedEventDate;

            MemberServiceId = serverObject.MemberServiceId;

            EventDate = serverObject.EventDate;

            PreviousMemberServiceId = serverObject.PreviousMemberServiceId;

            PreviousEventDate = serverObject.PreviousEventDate;

            ParentMembershipServiceEventId = serverObject.ParentMembershipServiceEventId;

            ParentMembershipServiceEventDate = serverObject.ParentMembershipServiceEventDate;

            PreviousThresholdId = serverObject.PreviousThresholdId;
            

            PreviousThresholdDate = serverObject.PreviousThresholdDate;

            NextThresholdDate = serverObject.NextThresholdDate;

            Status = serverObject.Status;            

            return;

        }

        #endregion


    }

}
