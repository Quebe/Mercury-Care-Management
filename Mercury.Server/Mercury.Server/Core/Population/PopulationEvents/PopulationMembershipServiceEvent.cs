using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Population.PopulationEvents {
    
    [DataContract (Name = "PopulationMembershipServiceEvent")]
    public class PopulationMembershipServiceEvent : CoreObject {

        #region Private Properties
        
        [DataMember (Name = "PopulationMembershipId")]
        private Int64 populationMembershipId;

        [DataMember (Name = "PopulationServiceEventId")]
        private Int64 populationServiceEventId;

        [DataMember (Name = "ExpectedEventDate")]
        private DateTime expectedEventDate;

        [DataMember (Name = "MemberServiceId")]
        private Int64 memberServiceId;

        [DataMember (Name = "EventDate")]
        private DateTime? eventDate = null;

        [DataMember (Name = "PreviousMemberServiceId")]
        private Int64? previousMemberServiceId = null;

        [DataMember (Name = "PreviousEventDate")]
        private DateTime? previousEventDate = null;

        [DataMember (Name = "ParentMembershipServiceEventId")]
        private Int64 parentMembershipServiceEventId;

        [DataMember (Name = "ParentMembershipServiceEventDate")]
        private DateTime? parentMembershipServiceEventDate;

        [DataMember (Name = "PreviousThresholdId")]
        private Int64? previousThresholdId;

        [DataMember (Name = "PreviousThresholdDate")]
        private DateTime? previousThresholdDate;

        [DataMember (Name = "NextThresholdDate")]
        private DateTime? nextThresholdDate;

        [DataMember (Name = "Status")]
        private Enumerations.PopulationServiceEventStatus status = Mercury.Server.Core.Population.Enumerations.PopulationServiceEventStatus.CompliantOrNoChange;

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

        public Enumerations.PopulationServiceEventStatus Status { get { return status; } set { status = value; } }


        public String ServiceName {

            get {

                if (application == null) { return String.Empty; }

                PopulationServiceEvent serviceEvent = application.PopulationServiceEventGet (populationServiceEventId);

                if (serviceEvent == null) { return String.Empty; }

                return serviceEvent.ServiceName;

            }

        }



        #endregion


        #region Constructors

        protected void ObjectConstructor (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public PopulationMembershipServiceEvent (Application applicationReference) { ObjectConstructor (applicationReference); return; }

        public PopulationMembershipServiceEvent (Application applicationReference, Int64 forPopulationMembershipServiceEventId) {

            ObjectConstructor (applicationReference);

            base.BaseConstructor (applicationReference, forPopulationMembershipServiceEventId);

            return;

        }

        #endregion


        #region Database Functions
        
        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            PopulationMembershipId = (Int64) currentRow["PopulationMembershipId"];

            PopulationServiceEventId = (Int64) currentRow["PopulationServiceEventId"];


            ExpectedEventDate = (DateTime) currentRow["ExpectedEventDate"];

            MemberServiceId = (Int64) currentRow["MemberServiceId"];

            EventDate = DateTimeFromSql (currentRow, "EventDate");


            PreviousMemberServiceId = IdFromSql (currentRow, "PreviousMemberServiceId");

            PreviousEventDate = DateTimeFromSql (currentRow, "PreviousEventDate");


            ParentMembershipServiceEventId = IdFromSql (currentRow, "ParentMembershipServiceEventId");

            ParentMembershipServiceEventDate = DateTimeFromSql (currentRow, "ParentMembershipServiceEventDate");


            PreviousEventDate = DateTimeFromSql (currentRow, "PreviousEventDate");

            PreviousThresholdId = IdFromSql (currentRow, "PreviousThresholdId");

            PreviousThresholdDate = DateTimeFromSql (currentRow, "PreviousThresholdDate");

            NextThresholdDate = DateTimeFromSql (currentRow, "NextThresholdDate");


            Status = (Mercury.Server.Core.Population.Enumerations.PopulationServiceEventStatus) ((Int32) currentRow["Status"]);

            return;

        }

        #endregion


        #region Virtual - Data Bindings

        override public String EvaluateDataBinding (String bindingContext) {

            String dataValue = String.Empty;

            String bindingContextPart = bindingContext.Split ('.')[0];

            switch (bindingContextPart) {

                case "PopulationMembershipServiceEvent":

                    bindingContextPart = bindingContext.Split ('.')[1];

                    switch (bindingContextPart) {

                        case "ServiceName": dataValue = ServiceName; break;

                        case "EventDate": dataValue = (eventDate.HasValue) ? eventDate.Value.ToString ("MM/dd/yyyy") : String.Empty; break;

                        case "ExpectedDate": dataValue = expectedEventDate.ToString ("MM/dd/yyyy"); break;

                        case "IsOpenStatus": dataValue = (status != Mercury.Server.Core.Population.Enumerations.PopulationServiceEventStatus.CompliantOrNoChange).ToString (); break;

                        default: dataValue = "!Error"; break;

                    }

                    break;

                default: dataValue = "!Error"; break;

            }

            return dataValue;

        }

        override public Dictionary<String, String> DataBindingContexts {

            get {

                Dictionary<String, String> dataBindings = new Dictionary<String, String> ();


                foreach (String bindingName in new Member.Member (base.application).DataBindingContexts.Keys) {

                    dataBindings.Add ("PopulationMembership.Member." + bindingName, new Member.Member (base.application).DataBindingContexts[bindingName]);

                }


                foreach (String bindingName in new Member.MemberEnrollment (base.application).DataBindingContexts.Keys) {

                    dataBindings.Add ("PopulationMembership.Member.CurrentEnrollment." + bindingName, new Member.MemberEnrollment (base.application).DataBindingContexts[bindingName]);

                }

                foreach (String bindingName in new Member.MemberEnrollmentPcp (base.application).DataBindingContexts.Keys) {

                    dataBindings.Add ("PopulationMembership.Member.CurrentPcpAssignment." + bindingName, new Member.MemberEnrollmentPcp (base.application).DataBindingContexts[bindingName]);

                }

                return dataBindings;

            }

        }

        #endregion

    }

}
