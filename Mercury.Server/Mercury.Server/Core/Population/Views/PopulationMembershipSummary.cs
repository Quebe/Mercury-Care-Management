using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Population.DataViews {

    [DataContract (Name = "PopulationMembershipSummaryDataView")]
    public class PopulationMembershipSummary {

        #region Private Properties

        [DataMember (Name = "PopulationMembershipId")]
        private Int64 populationMembershipId;

        [DataMember (Name = "MemberId")]
        private Int64 memberId;

        [DataMember (Name = "PopulationId")]
        private Int64 populationId;

        [DataMember (Name = "PopulationName")]
        private String populationName;

        [DataMember (Name = "EffectiveDate")]
        private DateTime effectiveDate;

        [DataMember (Name = "TerminationDate")]
        private DateTime terminationDate;

        [DataMember (Name = "AnchorDate")]
        private DateTime anchorDate;


        [DataMember (Name = "IdentifyingEventMemberServiceId")]
        private Int64 identifyingEventMemberServiceId;

        [DataMember (Name = "IdentifyingEventServiceId")]
        private Int64 identifyingEventServiceId;

        [DataMember (Name = "IdentifyingEventServiceName")]
        private String identifyingEventServiceName;

        [DataMember (Name = "IdentifyingEventDate")]
        private DateTime? identifyingEventDate;

        [DataMember (Name = "TerminatingEventMemberServiceId")]
        private Int64 terminatingEventMemberServiceId;

        [DataMember (Name = "TerminatingEventServiceId")]
        private Int64 terminatingEventServiceId;

        [DataMember (Name = "TerminatingEventServiceName")]
        private String terminatingEventServiceName;

        [DataMember (Name = "TerminatingEventDate")]
        private DateTime? terminatingEventDate;


        [DataMember (Name = "ServiceId")]
        private Int64 serviceId;

        [DataMember (Name = "ServiceName")]
        private String serviceName;

        [DataMember (Name = "ExpectedEventDate")]
        private DateTime? expectedEventDate;

        [DataMember (Name = "PreviousThresholdDate")]
        private DateTime? previousThresholdDate;

        [DataMember (Name = "NextThresholdDate")]
        private DateTime? nextThresholdDate;

        [DataMember (Name = "Status")]
        private Enumerations.PopulationServiceEventStatus status = Mercury.Server.Core.Population.Enumerations.PopulationServiceEventStatus.CompliantOrNoChange;

        [DataMember (Name = "StatusText")]
        private String statusText = String.Empty;

        [DataMember (Name = "PopulationEnabled")]
        private Boolean populationEnabled = true;

        [DataMember (Name = "PopulationVisible")]
        private Boolean populationVisible = true;

        #endregion


        #region Public Properties

        public Int64 PopulationMembershipId { get { return populationMembershipId; } set { populationMembershipId = value; } }

        public Int64 MemberId { get { return memberId; } set { memberId = value; } }

        public Int64 PopulationId { get { return populationId; } set { populationId = value; } }

        public String PopulationName { get { return populationName; } set { populationName = value; } }

        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }

        public DateTime AnchorDate { get { return anchorDate; } set { anchorDate = value; } }


        public Int64 IdentifyingEventMemberServiceId { get { return identifyingEventMemberServiceId; } set { identifyingEventMemberServiceId = value; } }

        public Int64 IdentifyingEventServiceId { get { return identifyingEventServiceId; } set { identifyingEventServiceId = value; } }

        public String IdentifyingEventServiceName { get { return identifyingEventServiceName; } set { identifyingEventServiceName = value; } }

        public DateTime? IdentifyingEventDate { get { return identifyingEventDate; } set { identifyingEventDate = value; } }

        public Int64 TerminatingEventMemberServiceId { get { return terminatingEventMemberServiceId; } set { terminatingEventMemberServiceId = value; } }

        public Int64 TerminatingEventServiceId { get { return terminatingEventServiceId; } set { terminatingEventServiceId = value; } }

        public String TerminatingEventServiceName { get { return terminatingEventServiceName; } set { terminatingEventServiceName = value; } }

        public DateTime? TerminatingEventDate { get { return terminatingEventDate; } set { terminatingEventDate = value; } }


        public Int64 ServiceId { get { return serviceId; } set { serviceId = value; } }

        public String ServiceName { get { return serviceName; } set { serviceName = value; } }

        public DateTime? ExpectedEventDate { get { return expectedEventDate; } set { expectedEventDate = value; } }

        public DateTime? PreviousThresholdDate { get { return previousThresholdDate; } set { previousThresholdDate = value; } }

        public DateTime? NextThresholdDate { get {return nextThresholdDate; } set { nextThresholdDate = value; } }

        public Enumerations.PopulationServiceEventStatus Status { get { return status; } set { status = value; } }

        public String StatusText { get { return statusText; } set { statusText = value; } }

        public Boolean PopulationEnabled { get { return populationEnabled; } set { populationEnabled = value; } }

        public Boolean PopulationVisible { get { return populationVisible; } set { populationVisible = value; } }

        #endregion


        #region Data Functions

        public void MapDataFields (System.Data.DataRow currentRow) {

            populationMembershipId = (Int64) currentRow["PopulationMembershipId"];

            memberId = (Int64) currentRow["MemberId"];

            populationId = (Int64) currentRow["PopulationId"];

            populationName = (String) currentRow["PopulationName"];

            effectiveDate = (DateTime) currentRow["EffectiveDate"];

            terminationDate = (DateTime) currentRow["TerminationDate"];

            anchorDate = (DateTime) currentRow["AnchorDate"];


            identifyingEventMemberServiceId = CommonFunctions.IdFromSql (currentRow, "IdentifyingEventMemberServiceId");

            identifyingEventServiceId = CommonFunctions.IdFromSql (currentRow, "IdentifyingEventServiceId");

            identifyingEventServiceName = (String) currentRow["IdentifyingEventServiceName"];

            identifyingEventDate = CommonFunctions.DateTimeFromSql (currentRow, "IdentifyingEventDate");

            
            terminatingEventMemberServiceId = CommonFunctions.IdFromSql (currentRow, "TerminatingEventMemberServiceId");

            terminatingEventServiceId = CommonFunctions.IdFromSql (currentRow, "TerminatingEventServiceId");

            terminatingEventServiceName = (String)currentRow["TerminatingEventServiceName"];

            terminatingEventDate = CommonFunctions.DateTimeFromSql (currentRow, "TerminatingEventDate");

            
            serviceId = CommonFunctions.IdFromSql (currentRow, "ServiceId");

            serviceName = (String) currentRow["ServiceName"];

            expectedEventDate = CommonFunctions.DateTimeFromSql (currentRow, "ExpectedEventDate");

            previousThresholdDate = CommonFunctions.DateTimeFromSql (currentRow, "PreviousThresholdDate");

            nextThresholdDate = CommonFunctions.DateTimeFromSql (currentRow, "NextThresholdDate");


            status = (Mercury.Server.Core.Population.Enumerations.PopulationServiceEventStatus) (Int32) currentRow["Status"];

            switch (status) {

                case Enumerations.PopulationServiceEventStatus.CompliantOrNoChange: statusText = "Compliant"; break;

                case Enumerations.PopulationServiceEventStatus.Open: statusText = "Open"; break;

                case Enumerations.PopulationServiceEventStatus.OpenInformational: statusText = "Open - Informational"; break;

                case Enumerations.PopulationServiceEventStatus.OpenWarning: statusText = "Open - Warning"; break;

                case Enumerations.PopulationServiceEventStatus.OpenCritical: statusText = "Open - Critical"; break;

                default: statusText = "Open - Unknown"; break;

            }

            populationEnabled = (Boolean) currentRow["PopulationEnabled"];

            populationVisible = (Boolean) currentRow["PopulationVisible"];

            return;

        }

        #endregion

    }

}
