using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Population {

    [Serializable]
    public class Population : CoreConfigurationObject {

        #region Private Properties

        private Int64 populationTypeId = 0;

        private Boolean allowProspective = false;

        private Mercury.Server.Application.PopulationInitialAnchorDate initialAnchorDate = Mercury.Server.Application.PopulationInitialAnchorDate.ProcessDate;


        private List<PopulationCriteria.PopulationCriteriaEnrollment> enrollmentCriteria = new List<Mercury.Client.Core.Population.PopulationCriteria.PopulationCriteriaEnrollment> ();

        private List<PopulationCriteria.PopulationCriteriaDemographic> demographicCriteria = new List<Mercury.Client.Core.Population.PopulationCriteria.PopulationCriteriaDemographic> ();

        private List<PopulationCriteria.PopulationCriteriaGeographic> geographicCriteria = new List<Mercury.Client.Core.Population.PopulationCriteria.PopulationCriteriaGeographic> ();

        private List<PopulationCriteria.PopulationCriteriaEvent> eventCriteria = new List<Mercury.Client.Core.Population.PopulationCriteria.PopulationCriteriaEvent> ();

        private Dictionary<String, Core.Action.Action> events = new Dictionary<String, Mercury.Client.Core.Action.Action> ();

        private List<PopulationEvents.PopulationServiceEvent> serviceEvents = new List<Mercury.Client.Core.Population.PopulationEvents.PopulationServiceEvent> ();

        private List<PopulationEvents.PopulationTriggerEvent> triggerEvents = new List<Mercury.Client.Core.Population.PopulationEvents.PopulationTriggerEvent> ();

        private List<PopulationEvents.PopulationActivityEvent> activityEvents = new List<Mercury.Client.Core.Population.PopulationEvents.PopulationActivityEvent> ();

        #endregion


        #region Public Properties

        public Int64 PopulationTypeId { get { return populationTypeId; } set { populationTypeId = value; } }

        public Boolean AllowProspective { get { return allowProspective; } set { allowProspective = value; } }

        public Mercury.Server.Application.PopulationInitialAnchorDate InitialAnchorDate { get { return initialAnchorDate; } set { initialAnchorDate = value; } }


        public List<PopulationCriteria.PopulationCriteriaEnrollment> EnrollmentCriteria { get { return enrollmentCriteria; } set { enrollmentCriteria = value; } }

        public List<PopulationCriteria.PopulationCriteriaDemographic> DemographicCriteria { get { return demographicCriteria; } set { demographicCriteria = value; } }

        public List<PopulationCriteria.PopulationCriteriaGeographic> GeographicCriteria { get { return geographicCriteria; } set { geographicCriteria = value; } }

        public List<PopulationCriteria.PopulationCriteriaEvent> EventCriteria { get { return eventCriteria; } set { eventCriteria = value; } }

        public Dictionary<String, Core.Action.Action> Events { get { return events; } set { events = value; } }

        public List<PopulationEvents.PopulationServiceEvent> ServiceEvents { get { return serviceEvents; } set { serviceEvents = value; } }

        public List<PopulationEvents.PopulationTriggerEvent> TriggerEvents { get { return triggerEvents; } set { triggerEvents = value; } }

        public List<PopulationEvents.PopulationActivityEvent> ActivityEvents { get { return activityEvents; } set { activityEvents = value; } }

        #endregion


        #region Public Properties

        public PopulationType PopulationType { get { return application.PopulationTypeGet (populationTypeId, true); } }

        #endregion 


        #region Constructors

        override protected void BaseConstructor (Application applicationReference) {

            base.BaseConstructor (applicationReference);

            events.Clear ();

            events.Add ("OnBeforeMembershipAdd", applicationReference.ActionById (1)); // WORKFLOW

            events.Add ("OnMembershipAdd", new Mercury.Client.Core.Action.Action (application));

            events.Add ("OnBeforeMembershipTerminate", applicationReference.ActionById (1)); // WORKFLOW

            events.Add ("OnMembershipTerminate", new Mercury.Client.Core.Action.Action (application));

            return;

        }

        public Population (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public Population (Application applicationReference, Mercury.Server.Application.Population serverPopulation) {

            BaseConstructor (applicationReference, serverPopulation); 


            populationTypeId = serverPopulation.PopulationTypeId;

            allowProspective = serverPopulation.AllowProspective;

            initialAnchorDate = serverPopulation.InitialAnchorDate;


            enrollmentCriteria.Clear ();

            foreach (Mercury.Server.Application.PopulationCriteriaEnrollment currentCriteria in serverPopulation.EnrollmentCriteria) {

                enrollmentCriteria.Add (new Mercury.Client.Core.Population.PopulationCriteria.PopulationCriteriaEnrollment (application, currentCriteria));

            }


            demographicCriteria.Clear ();

            foreach (Mercury.Server.Application.PopulationCriteriaDemographic currentCriteria in serverPopulation.DemographicCriteria) {

                demographicCriteria.Add (new Mercury.Client.Core.Population.PopulationCriteria.PopulationCriteriaDemographic (application, currentCriteria));

            }


            geographicCriteria.Clear ();

            foreach (Mercury.Server.Application.PopulationCriteriaGeographic currentCriteria in serverPopulation.GeographicCriteria) {

                geographicCriteria.Add (new Mercury.Client.Core.Population.PopulationCriteria.PopulationCriteriaGeographic (application, currentCriteria));

            }


            eventCriteria.Clear ();

            foreach (Mercury.Server.Application.PopulationCriteriaEvent currentCriteria in serverPopulation.EventCriteria) {

                eventCriteria.Add (new Mercury.Client.Core.Population.PopulationCriteria.PopulationCriteriaEvent (application, currentCriteria));

            }


            events.Clear ();

            foreach (String eventName in serverPopulation.Events.Keys) {

                events.Add (eventName, new Mercury.Client.Core.Action.Action (application, serverPopulation.Events[eventName]));

            }


            serviceEvents.Clear ();

            foreach (Mercury.Server.Application.PopulationServiceEvent currentEvent in serverPopulation.ServiceEvents) {

                serviceEvents.Add (new Mercury.Client.Core.Population.PopulationEvents.PopulationServiceEvent (Application, currentEvent));

            }


            triggerEvents.Clear ();

            foreach (Mercury.Server.Application.PopulationTriggerEvent currentEvent in serverPopulation.TriggerEvents) {

                triggerEvents.Add (new Mercury.Client.Core.Population.PopulationEvents.PopulationTriggerEvent (Application, currentEvent));

            }


            activityEvents.Clear ();

            foreach (Mercury.Server.Application.PopulationActivityEvent currentEvent in serverPopulation.ActivityEvents) {

                activityEvents.Add (new Mercury.Client.Core.Population.PopulationEvents.PopulationActivityEvent (Application, currentEvent));

            }

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.Population serverObject) {

            base.MapToServerObject ((Server.Application.CoreConfigurationObject)serverObject);


            serverObject.PopulationTypeId = populationTypeId;

            serverObject.AllowProspective = allowProspective;

            serverObject.InitialAnchorDate = initialAnchorDate;


            serverObject.EnrollmentCriteria = new Mercury.Server.Application.PopulationCriteriaEnrollment[enrollmentCriteria.Count];

            for (Int32 currentCriteriaIndex = 0; currentCriteriaIndex < enrollmentCriteria.Count; currentCriteriaIndex++) {

                serverObject.EnrollmentCriteria[currentCriteriaIndex] = (Server.Application.PopulationCriteriaEnrollment)enrollmentCriteria[currentCriteriaIndex].ToServerObject ();

            }


            serverObject.DemographicCriteria = new Mercury.Server.Application.PopulationCriteriaDemographic[demographicCriteria.Count];

            for (Int32 currentCriteriaIndex = 0; currentCriteriaIndex < demographicCriteria.Count; currentCriteriaIndex++) {

                serverObject.DemographicCriteria[currentCriteriaIndex] = (Server.Application.PopulationCriteriaDemographic)demographicCriteria[currentCriteriaIndex].ToServerObject ();

            }

            serverObject.GeographicCriteria = new Mercury.Server.Application.PopulationCriteriaGeographic[geographicCriteria.Count];

            for (Int32 currentCriteriaIndex = 0; currentCriteriaIndex < geographicCriteria.Count; currentCriteriaIndex++) {

                serverObject.GeographicCriteria[currentCriteriaIndex] = (Server.Application.PopulationCriteriaGeographic)geographicCriteria[currentCriteriaIndex].ToServerObject ();

            }


            serverObject.EventCriteria = new Mercury.Server.Application.PopulationCriteriaEvent[eventCriteria.Count];

            for (Int32 currentCriteriaIndex = 0; currentCriteriaIndex < eventCriteria.Count; currentCriteriaIndex++) {

                serverObject.EventCriteria[currentCriteriaIndex] = (Server.Application.PopulationCriteriaEvent)eventCriteria[currentCriteriaIndex].ToServerObject ();

            }


            serverObject.Events = new Dictionary<String, Mercury.Server.Application.Action> ();

            foreach (String eventName in events.Keys) {

                serverObject.Events.Add (eventName, (Server.Application.Action)events[eventName].ToServerObject ());

            }


            serverObject.ServiceEvents = new Mercury.Server.Application.PopulationServiceEvent[serviceEvents.Count];

            for (Int32 currentEventIndex = 0; currentEventIndex < serviceEvents.Count; currentEventIndex++) {

                serverObject.ServiceEvents[currentEventIndex] = (Mercury.Server.Application.PopulationServiceEvent)serviceEvents[currentEventIndex].ToServerObject ();

            }

            serverObject.TriggerEvents = new Mercury.Server.Application.PopulationTriggerEvent[triggerEvents.Count];

            for (Int32 currentEventIndex = 0; currentEventIndex < triggerEvents.Count; currentEventIndex++) {

                serverObject.TriggerEvents[currentEventIndex] = (Mercury.Server.Application.PopulationTriggerEvent)triggerEvents[currentEventIndex].ToServerObject ();

            }

            serverObject.ActivityEvents = new Mercury.Server.Application.PopulationActivityEvent[activityEvents.Count];

            for (Int32 currentEventIndex = 0; currentEventIndex < activityEvents.Count; currentEventIndex++) {

                serverObject.ActivityEvents[currentEventIndex] = (Mercury.Server.Application.PopulationActivityEvent)activityEvents[currentEventIndex].ToServerObject ();

            }


            return;

        }

        public override Object ToServerObject () {

            Server.Application.Population serverObject = new Server.Application.Population();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public Population Copy () {

            Server.Application.Population serverObject = (Server.Application.Population)ToServerObject ();

            Population copiedObject = new Population (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (Population comparePopulation) {

            Boolean isEqual = base.IsEqual (comparePopulation);


            if (populationTypeId != comparePopulation.PopulationTypeId) { isEqual = false; }

            isEqual &= (allowProspective == comparePopulation.AllowProspective);

            if (initialAnchorDate != comparePopulation.InitialAnchorDate) { isEqual = false; }


            foreach (String eventName in events.Keys) {

                isEqual = isEqual && (events[eventName].IsEqual (comparePopulation.Events[eventName]));

                if (!isEqual) { break; }

            }


            #region Criteria Comparisons

            if (enrollmentCriteria.Count != comparePopulation.EnrollmentCriteria.Count) { isEqual = false; }

            if (isEqual) {

                for (Int32 currentCriteriaIndex = 0; currentCriteriaIndex < enrollmentCriteria.Count; currentCriteriaIndex++) {

                    isEqual = isEqual && (enrollmentCriteria[currentCriteriaIndex].IsEqual (comparePopulation.EnrollmentCriteria[currentCriteriaIndex]));

                    if (!isEqual) { break; }

                }

            }

            if (demographicCriteria.Count != comparePopulation.DemographicCriteria.Count) { isEqual = false; }

            if (isEqual) {

                for (Int32 currentCriteriaIndex = 0; currentCriteriaIndex < demographicCriteria.Count; currentCriteriaIndex++) {

                    isEqual = isEqual && (demographicCriteria[currentCriteriaIndex].IsEqual (comparePopulation.DemographicCriteria[currentCriteriaIndex]));

                    if (!isEqual) { break; }

                }

            }

            if (geographicCriteria.Count != comparePopulation.GeographicCriteria.Count) { isEqual = false; }

            if (isEqual) {

                for (Int32 currentCriteriaIndex = 0; currentCriteriaIndex < geographicCriteria.Count; currentCriteriaIndex++) {

                    isEqual = isEqual && (geographicCriteria[currentCriteriaIndex].IsEqual (comparePopulation.GeographicCriteria[currentCriteriaIndex]));

                    if (!isEqual) { break; }

                }

            }

            if (eventCriteria.Count != comparePopulation.EventCriteria.Count) { isEqual = false; }

            if (isEqual) {

                for (Int32 currentCriteriaIndex = 0; currentCriteriaIndex < eventCriteria.Count; currentCriteriaIndex++) {

                    isEqual = isEqual && (eventCriteria[currentCriteriaIndex].IsEqual (comparePopulation.EventCriteria[currentCriteriaIndex]));

                    if (!isEqual) { break; }

                }

            }

            #endregion


            if (serviceEvents.Count != comparePopulation.ServiceEvents.Count) { isEqual = false; }

            if (isEqual) {

                for (Int32 currentEventIndex = 0; currentEventIndex < serviceEvents.Count; currentEventIndex++) {

                    isEqual = isEqual && (serviceEvents[currentEventIndex].IsEqual (comparePopulation.ServiceEvents[currentEventIndex]));

                    if (!isEqual) { break; }

                }

            }

            if (triggerEvents.Count != comparePopulation.TriggerEvents.Count) { isEqual = false; }

            if (isEqual) {

                for (Int32 currentEventIndex = 0; currentEventIndex < triggerEvents.Count; currentEventIndex++) {

                    isEqual = isEqual && (triggerEvents[currentEventIndex].IsEqual (comparePopulation.TriggerEvents[currentEventIndex]));

                    if (!isEqual) { break; }

                }

            }


            if (activityEvents.Count != comparePopulation.ActivityEvents.Count) { isEqual = false; }

            if (isEqual) {

                for (Int32 currentEventIndex = 0; currentEventIndex < activityEvents.Count; currentEventIndex++) {

                    isEqual = isEqual && (activityEvents[currentEventIndex].IsEqual (comparePopulation.ActivityEvents[currentEventIndex]));

                    if (!isEqual) { break; }

                }

            }

            return isEqual;

        }

        #endregion 


        #region Public Methods

        public Boolean HasTriggerEventForService (Int64 serviceId) {

            Boolean duplicateFound = false;

            if (serviceId != 0) {

                foreach (PopulationEvents.PopulationTriggerEvent currentTriggerEvent in triggerEvents) {

                    duplicateFound = duplicateFound || (currentTriggerEvent.ServiceId == serviceId);

                    if (duplicateFound) { break; }

                }

            }

            return duplicateFound;

        }

        public Boolean HasTriggerEventForMetric (Int64 metricId) {

            Boolean duplicateFound = false;

            if (metricId != 0) {

                foreach (PopulationEvents.PopulationTriggerEvent currentTriggerEvent in triggerEvents) {

                    duplicateFound = duplicateFound || (currentTriggerEvent.MetricId == metricId);

                    if (duplicateFound) { break; }

                }

            }

            return duplicateFound;

        }

        #endregion

    }

}
