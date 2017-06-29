using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Population.DataViews {

    [DataContract (Name = "PopulationMembershipTriggerEventDataView")]
    public class PopulationMembershipTriggerEvent : CoreObject {

        #region Private Properties

        [DataMember (Name = "PopulationMembershipTriggerEventId")]
        private Int64 populationMembershipTriggerEventId;

        [DataMember (Name = "PopulationMembershipId")]
        private Int64 populationMembershipId;

        [DataMember (Name = "PopulationTriggerEventId")]
        private Int64 populationTriggerEventId;

        [DataMember (Name = "TriggerDate")]
        private DateTime triggerDate;

        [DataMember (Name = "EventDate")]
        private DateTime eventDate;


        [DataMember (Name = "EventType")]
        private Enumerations.PopulationTriggerEventType eventType = Mercury.Server.Core.Population.Enumerations.PopulationTriggerEventType.Service;

        [DataMember (Name = "IsTriggerDeleted")]
        private Boolean isTriggerDeleted = false;

        [DataMember (Name = "ServiceId")]
        private Int64 serviceId;

        [DataMember (Name = "ServiceName")]
        private String serviceName;

        [DataMember (Name = "MetricId")]
        private Int64 metricId;

        [DataMember (Name = "MetricName")]
        private String metricName;

        [DataMember (Name = "AuthorizedServiceId")]
        private Int64 authorizedServiceId;

        [DataMember (Name = "AuthorizedServiceName")]
        private String authorizedServiceName;


        [DataMember (Name = "MemberServiceId")]
        private Int64 memberServiceId = 0;

        [DataMember (Name = "MemberMetricId")]
        private Int64 memberMetricId = 0;

        [DataMember (Name = "MemberAuthorizedServiceId")]
        private Int64 memberAuthorizedServiceId = 0;

        [DataMember (Name = "ProblemStatementId")]
        private Int64 problemStatementId = 0;

        [DataMember (Name = "ActionDescription")]
        private String actionDescription;

        #endregion


        #region Public Properties

        public Int64 PopulationMembershipTriggerEventId { get { return populationMembershipTriggerEventId; } set { populationMembershipTriggerEventId = value; } }

        public Int64 PopulationMembershipId { get { return populationMembershipId; } set { populationMembershipId = value; } }

        public Int64 PopulationTriggerEventId { get { return populationTriggerEventId; } set { populationTriggerEventId = value; } }

        public DateTime TriggerDate { get { return triggerDate; } set { triggerDate = value; } }

        public DateTime EventDate { get { return eventDate; } set { eventDate = value; } }


        public Enumerations.PopulationTriggerEventType EventType { get { return eventType; } set { eventType = value; } }

        public Boolean IsTriggerDeleted { get { return isTriggerDeleted; } set { isTriggerDeleted = value; } }

        public Int64 ServiceId { get { return serviceId; } set { serviceId = value; } }

        public String ServiceName { get { return serviceName; } set { serviceName = CommonFunctions.SetValueMaxLength (value.Trim (), Data.DataTypeConstants.Name); } }

        public Int64 MetricId { get { return metricId; } set { metricId = value; } }

        public String MetricName { get { return metricName; } set { metricName = CommonFunctions.SetValueMaxLength (value.Trim (), Data.DataTypeConstants.NameLast); } }

        public Int64 AuthorizedServiceId { get { return authorizedServiceId; } set { authorizedServiceId = value; } }

        public String AuthorizedServiceName { get { return authorizedServiceName; } set { authorizedServiceName = CommonFunctions.SetValueMaxLength (value.Trim (), Data.DataTypeConstants.Name); } }


        public Int64 MemberServiceId { get { return memberServiceId; } set { memberServiceId = value; } }

        public Int64 MemberMetricId { get { return memberMetricId; } set { memberMetricId = value; } }

        public Int64 MemberAuthorizedServiceId { get { return memberAuthorizedServiceId; } set { memberAuthorizedServiceId = value; } }

        public Int64 ProblemStatementId { get { return problemStatementId; } set { problemStatementId = value; } }

        public String ActionDescription { get { return actionDescription; } set { actionDescription = value; } }

        #endregion


        #region Database Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            PopulationMembershipTriggerEventId = (Int64) currentRow["PopulationMembershipTriggerEventId"];

            PopulationMembershipId = (Int64) currentRow["PopulationMembershipId"];

            PopulationTriggerEventId = (Int64) currentRow["PopulationTriggerEventId"];

            TriggerDate = (DateTime) currentRow["TriggerDate"];

            EventDate = (DateTime) currentRow["EventDate"];


            if (!(currentRow ["EventType"] is DBNull)) {

                EventType = (Mercury.Server.Core.Population.Enumerations.PopulationTriggerEventType)(Int32)currentRow["EventType"];

            }

            isTriggerDeleted = (Boolean) currentRow["IsTriggerDeleted"];


            serviceId = IdFromSql (currentRow, "ServiceId");

            serviceName = StringFromSql (currentRow, "ServiceName");

            metricId = IdFromSql (currentRow, "MetricId");

            metricName = StringFromSql (currentRow, "MetricName");

            authorizedServiceId = IdFromSql (currentRow, "AuthorizedServiceId");

            authorizedServiceName = StringFromSql (currentRow, "AuthorizedServiceName");


            MemberServiceId = IdFromSql (currentRow, "MemberServiceId");

            MemberMetricId = IdFromSql (currentRow, "MemberMetricId");

            MemberAuthorizedServiceId = IdFromSql (currentRow, "MemberAuthorizedServiceId");


            ProblemStatementId = IdFromSql (currentRow, "ProblemStatementId");

            ActionDescription = StringFromSql (currentRow, "ActionDescription");

            return;

        }

        #endregion

    }

}
