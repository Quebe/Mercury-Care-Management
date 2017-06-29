using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Population {

    [Serializable]
    [DataContract (Name = "PopulationMembership")]
    public class PopulationMembership : CoreObject {

        #region Private Properties

        [DataMember (Name = "PopulationId")]
        private Int64 populationId;

        [DataMember (Name = "MemberId")]
        private Int64 memberId;

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

        [DataMember (Name = "IdentifyingEventDate")]
        private DateTime? identifyingEventDate;

        [DataMember (Name = "TerminatingEventMemberServiceId")]
        private Int64 terminatingEventMemberServiceId;

        [DataMember (Name = "TerminatingEventServiceId")]
        private Int64 terminatingEventServiceId;

        [DataMember (Name = "TerminatingEventDate")]
        private DateTime? terminatingEventDate;

        [NonSerialized]
        private Member.Member member = null;

        [NonSerialized]
        private Population population = null;

        #endregion


        #region Public Properties

        public Int64 PopulationId { get { return populationId; } set { populationId = value; population = null; } }

        public Int64 MemberId { get { return memberId; } set { memberId = value; } }

        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }

        public DateTime AnchorDate { get { return anchorDate; } set { anchorDate = value; } }

        public Int64 IdentifyingEventMemberServiceId { get { return identifyingEventMemberServiceId; } set { identifyingEventMemberServiceId = value; } }

        public Int64 IdentifyingEventServiceId { get { return identifyingEventServiceId; } set { identifyingEventServiceId = value; } }

        public DateTime? IdentifyingEventDate { get { return identifyingEventDate; } set { identifyingEventDate = value; } }

        public Int64 TerminatingEventMemberServiceId { get { return terminatingEventMemberServiceId; } set { terminatingEventMemberServiceId = value; } }

        public Int64 TerminatingEventServiceId { get { return terminatingEventServiceId; } set { terminatingEventServiceId = value; } }

        public DateTime? TerminatingEventDate { get { return terminatingEventDate; } set { terminatingEventDate = value; } }


        public Member.Member Member {

            get {

                if (member != null) { return member; }

                if (base.application == null) { member = new Member.Member (base.application); return member; }

                if (memberId == 0) { member = new Member.Member (base.application); return member; }

                member = new Member.Member (base.application, memberId);

                return member;

            }

        }

        public Population Population {

            get {

                if (population != null) { return population; }

                if (application == null) { return null; }

                population = application.PopulationGet (populationId);

                return population;

            }

        }

        public String PopulationName {

            get {

                String populationName = String.Empty;

                if (Population != null) { populationName = Population.Name; }

                return populationName;

            }

        }

        #endregion


        #region Constructors

        public PopulationMembership (Application applicationReference) { base.BaseConstructor (applicationReference); return; }

        public PopulationMembership (Application applicationReference, Int64 forPopulationMembershipId) {

            base.BaseConstructor (applicationReference, forPopulationMembershipId);

            
            return; 

        }

        #endregion


        #region Data Functions

        override public void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            populationId = (Int64) currentRow["PopulationId"];

            memberId = (Int64) currentRow["MemberId"];

            effectiveDate = (DateTime) currentRow["EffectiveDate"];

            terminationDate = (DateTime) currentRow["TerminationDate"];

            anchorDate = (DateTime)currentRow["AnchorDate"];

            
            identifyingEventMemberServiceId = IdFromSql (currentRow, "IdentifyingEventMemberServiceId");

            identifyingEventServiceId = IdFromSql (currentRow, "IdentifyingEventServiceId");

            identifyingEventDate = DateTimeFromSql (currentRow, "IdentifyingEventDate");


            terminatingEventMemberServiceId = IdFromSql (currentRow, "TerminatingEventMemberServiceId");

            terminatingEventServiceId = IdFromSql (currentRow, "TerminatingEventServiceId");

            terminatingEventDate = DateTimeFromSql (currentRow, "TerminatingEventDate");
                        
            return;

        }

        #endregion


        #region Virtual - Data Bindings

        override public Dictionary<String, String> DataBindingContexts {

            get {

                Dictionary<String, String> dataBindings = new Dictionary<String, String> ();

                Dictionary<String, String> childBindings;


                dataBindings.Add ("PopulationMembershipServiceEvents", "Collection|PopulationMembershipServiceEvent");


                childBindings = new Member.Member (base.application).DataBindingContexts;

                foreach (String bindingName in childBindings.Keys) {

                    dataBindings.Add ("PopulationMembership.Member." + bindingName, childBindings[bindingName]);

                }


                childBindings = new Member.MemberEnrollment (base.application).DataBindingContexts;

                foreach (String bindingName in childBindings.Keys) {

                    dataBindings.Add ("PopulationMembership.Member.CurrentEnrollment." + bindingName, childBindings[bindingName]);

                }


                childBindings = new Member.MemberEnrollmentPcp (base.application).DataBindingContexts;

                foreach (String bindingName in childBindings.Keys) {

                    dataBindings.Add ("PopulationMembership.Member.CurrentPcpAssignment." + bindingName, childBindings[bindingName]);

                }

                return dataBindings;

            }

        }

        override public String EvaluateDataBinding (String bindingContext) {

            String dataValue = String.Empty;

            String bindingContextPart = bindingContext.Split ('.')[0];

            switch (bindingContextPart) {

                case "PopulationMembership":

                    bindingContextPart = bindingContext.Split ('.')[1];

                    switch (bindingContextPart) {

                        case "Member":

                            if (bindingContext == "PopulationMembership.Member.Id") { dataValue = memberId.ToString (); }

                            else {

                                bindingContextPart = bindingContext.Replace ("PopulationMembership.Member.", "");

                                dataValue = Member.EvaluateDataBinding (bindingContextPart);

                            }

                            break;


                        default: dataValue = "!Error"; break;

                    }

                    break;

                case "PopulationMembershipServiceEvents":

                    dataValue = "PopulationMembershipServiceEvent";

                    List<Core.Population.PopulationEvents.PopulationMembershipServiceEvent> populationMembershipServiceEvents = application.PopulationMembershipServiceEventsGet (id);

                    foreach (Core.Population.PopulationEvents.PopulationMembershipServiceEvent currentServiceEvent in populationMembershipServiceEvents) {

                        dataValue = dataValue + "|" + currentServiceEvent.Id.ToString ();

                    }

                    break;

                default: dataValue = "!Error"; break;

            }

            return dataValue;

        }

        #endregion


        #region Public Methods

        public List<PopulationEvents.PopulationMembershipServiceEvent> PopulationMembershipServiceEvents () {

            return application.PopulationMembershipServiceEventsGet (id);

        }

        public PopulationEvents.PopulationMembershipServiceEvent PopulationMembershipServiceEvent (String serviceName) {

            PopulationEvents.PopulationMembershipServiceEvent serviceEvent = null;

            foreach (PopulationEvents.PopulationMembershipServiceEvent currentServiceEvent in PopulationMembershipServiceEvents ()) {

                if ((currentServiceEvent.ServiceName == serviceName)  && (!currentServiceEvent.EventDate.HasValue)) {

                    serviceEvent = currentServiceEvent;

                    break;

                }

            }

            return serviceEvent;

        }

        public List<PopulationEvents.PopulationMembershipTriggerEvent> PopulationMembershipTriggerEvents () {

            return application.PopulationMembershipTriggerEventsGet (id);

        }

        #endregion 

    }

}
