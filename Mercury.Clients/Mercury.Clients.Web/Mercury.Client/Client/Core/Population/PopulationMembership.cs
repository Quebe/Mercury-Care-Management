using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Population {

    [Serializable]
    public class PopulationMembership : CoreObject {

        #region Private Properties

        private Int64 populationId;

        private Int64 memberId;

        private DateTime effectiveDate;

        private DateTime terminationDate;

        private DateTime anchorDate;

        private Int64 identifyingEventMemberServiceId;

        private Int64 identifyingEventServiceId;

        private DateTime? identifyingEventDate;

        private Int64 terminatingEventMemberServiceId;

        private Int64 terminatingEventServiceId;

        private DateTime? terminatingEventDate;


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


        public Population Population {

            get {

                if (population != null) { return population; }

                if (Application == null) { return null; }

                population = Application.PopulationGet (populationId, true);

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

        public PopulationMembership (Application application) {

            BaseConstructor (application);

            return;

        }

        public PopulationMembership (Application application, Mercury.Server.Application.PopulationMembership serverObject) {

            BaseConstructor (application, serverObject);


            populationId = serverObject.PopulationId;

            memberId = serverObject.MemberId;

            effectiveDate = serverObject.EffectiveDate;

            terminationDate = serverObject.TerminationDate;

            anchorDate = serverObject.AnchorDate;

            identifyingEventMemberServiceId = serverObject.IdentifyingEventMemberServiceId;

            identifyingEventServiceId = serverObject.IdentifyingEventServiceId;

            identifyingEventDate = serverObject.IdentifyingEventDate;

            terminatingEventMemberServiceId = serverObject.TerminatingEventMemberServiceId;

            terminatingEventServiceId = serverObject.TerminatingEventServiceId;

            terminatingEventDate = serverObject.TerminatingEventDate;


            createAccountInfo = serverObject.CreateAccountInfo;

            modifiedAccountInfo = serverObject.ModifiedAccountInfo;

            return;

        }

        #endregion


        #region Public Methods

        public List<PopulationEvents.PopulationMembershipServiceEvent> PopulationMembershipServiceEvents () {

            return Application.PopulationMembershipServiceEventsGet (Id, true);

        }

        public PopulationEvents.PopulationMembershipServiceEvent PopulationMembershipServiceEvent (String serviceName) {

            PopulationEvents.PopulationMembershipServiceEvent serviceEvent = null;

            foreach (PopulationEvents.PopulationMembershipServiceEvent currentServiceEvent in PopulationMembershipServiceEvents ()) {

                if ((currentServiceEvent.ServiceName == serviceName) && (!currentServiceEvent.EventDate.HasValue)) {

                    serviceEvent = currentServiceEvent;

                }

            }

            return serviceEvent;

        }

        #endregion 



    }

}
