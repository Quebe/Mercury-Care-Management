using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Population.DataViews {

    [DataContract (Name = "PopulationMembershipServiceEventDataView")]
    public class PopulationMembershipServiceEvent {

        #region Private Properties

        [DataMember (Name = "PopulationMembershipId")]
        private Int64 populationMembershipId;

        [DataMember (Name = "ServiceId")]
        private Int64 serviceId;

        [DataMember (Name = "ServiceName")]
        private String serviceName;

        [DataMember (Name = "ExpectedEventDate")]
        private DateTime? expectedEventDate;

        [DataMember (Name = "EventDate")]
        private DateTime? eventDate;

        [DataMember (Name = "PreviousThresholdDate")]
        private DateTime? previousThresholdDate;

        [DataMember (Name = "NextThresholdDate")]
        private DateTime? nextThresholdDate;

        [DataMember (Name = "Status")]
        private Enumerations.PopulationServiceEventStatus status = Mercury.Server.Core.Population.Enumerations.PopulationServiceEventStatus.CompliantOrNoChange;

        [DataMember (Name = "StatusText")]
        private String statusText = String.Empty;

        [DataMember (Name = "ScheduleValue")]
        private Int32 scheduleValue;

        [DataMember (Name = "ScheduleQualifier")]
        private Core.Enumerations.DateQualifier scheduleQualifier = Mercury.Server.Core.Enumerations.DateQualifier.Months;

        [DataMember (Name = "Reoccurring")]
        private Boolean reoccurring = false;

        #endregion


        #region Public Properties

        public Int64 PopulationMembershipId { get { return populationMembershipId; } set { populationMembershipId = value; } }

        public Int64 ServiceId { get { return serviceId; } set { serviceId = value; } }

        public String ServiceName { get { return serviceName; } set { serviceName = value; } }

        public DateTime? ExpectedEventDate { get { return expectedEventDate; } set { expectedEventDate = value; } }

        public DateTime? EventDate { get { return eventDate; } set { eventDate = value; } }

        public DateTime? PreviousThresholdDate { get { return previousThresholdDate; } set { previousThresholdDate = value; } }

        public DateTime? NextThresholdDate { get { return nextThresholdDate; } set { nextThresholdDate = value; } }

        public Enumerations.PopulationServiceEventStatus Status { get { return status; } set { status = value; } }

        public String StatusText { get { return statusText; } set { statusText = value; } }

        public Int32 ScheduleValue { get { return scheduleValue; } set { scheduleValue = value; } }

        public Core.Enumerations.DateQualifier ScheduleQualifier { get { return scheduleQualifier; } set { scheduleQualifier = value; } }

        public Boolean Reoccurring { get { return reoccurring; } set { reoccurring = value; } }

        #endregion


        #region Data Functions

        public void MapDataFields (System.Data.DataRow currentRow) {

            populationMembershipId = (Int64) currentRow["PopulationMembershipId"];

            serviceId = (Int64) currentRow["ServiceId"];

            serviceName = (String) currentRow["ServiceName"];

            if (currentRow["ExpectedEventDate"] is DBNull) { expectedEventDate = null; } else { expectedEventDate = (DateTime) currentRow["ExpectedEventDate"]; }

            if (currentRow["EventDate"] is DBNull) { eventDate = null; } else { eventDate = (DateTime) currentRow["EventDate"]; }

            if (currentRow["PreviousThresholdDate"] is DBNull) { previousThresholdDate = null; } else { previousThresholdDate = (DateTime) currentRow["PreviousThresholdDate"]; }

            if (currentRow["NextThresholdDate"] is DBNull) { nextThresholdDate = null; } else { nextThresholdDate = (DateTime) currentRow["NextThresholdDate"]; }

            status = (Mercury.Server.Core.Population.Enumerations.PopulationServiceEventStatus) (Int32) currentRow["Status"];

            switch (status) {

                case Enumerations.PopulationServiceEventStatus.CompliantOrNoChange: statusText = "Compliant"; break;

                case Enumerations.PopulationServiceEventStatus.Open: statusText = "Open"; break;

                case Enumerations.PopulationServiceEventStatus.OpenInformational: statusText = "Open - Informational"; break;

                case Enumerations.PopulationServiceEventStatus.OpenWarning: statusText = "Open - Warning"; break;

                case Enumerations.PopulationServiceEventStatus.OpenCritical: statusText = "Open - Critical"; break;

                default: statusText = "Open - Unknown"; break;

            }

            scheduleValue = (Int32) currentRow["ScheduleValue"];

            scheduleQualifier = (Mercury.Server.Core.Enumerations.DateQualifier) (Int32) currentRow["ScheduleQualifier"];

            Reoccurring = (Boolean) currentRow["Reoccurring"];

            return;

        }

        #endregion

    }

}
