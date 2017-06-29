using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Condition {

    [Serializable]
    public class Condition : CoreConfigurationObject {
        
        #region Private Properties

        private Int64 conditionClassId = 0;

        private String conditionClassName = String.Empty;


        private List<ConditionCriteria.ConditionCriteriaDemographic> demographicCriteria = new List<Mercury.Client.Core.Condition.ConditionCriteria.ConditionCriteriaDemographic> ();

        private List<ConditionCriteria.ConditionCriteriaEvent> eventCriteria = new List<Mercury.Client.Core.Condition.ConditionCriteria.ConditionCriteriaEvent> ();

        private Dictionary<String, Core.Action.Action> events = new Dictionary<String, Mercury.Client.Core.Action.Action> ();

        #endregion


        #region Public Properties

        public Int64 ConditionClassId { get { return conditionClassId; } set { conditionClassId = value; } }

        public String ConditionClassName { get { return conditionClassName; } set { conditionClassName = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        
        public List<ConditionCriteria.ConditionCriteriaDemographic> DemographicCriteria { get { return demographicCriteria; } set { demographicCriteria = value; } }

        public List<ConditionCriteria.ConditionCriteriaEvent> EventCriteria { get { return eventCriteria; } set { eventCriteria = value; } }

        public Dictionary<String, Core.Action.Action> Events { get { return events; } set { events = value; } }

        #endregion

        
        #region Constructors

        override protected void BaseConstructor (Application applicationReference) {

            base.BaseConstructor (applicationReference);

            events.Clear ();

            //events.Add ("OnBeforeMembershipAdd", applicationReference.ActionById (1)); // WORKFLOW

            //events.Add ("OnMembershipAdd", new Mercury.Client.Core.Action.Action (application));

            //events.Add ("OnBeforeMembershipTerminate", applicationReference.ActionById (1)); // WORKFLOW

            //events.Add ("OnMembershipTerminate", new Mercury.Client.Core.Action.Action (application));

            return;

        }

        public Condition (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public Condition (Application applicationReference, Mercury.Server.Application.Condition serverCondition) {

            BaseConstructor (applicationReference, serverCondition); 


            conditionClassId = serverCondition.ConditionClassId;

            conditionClassName = serverCondition.ConditionClassName;


            demographicCriteria.Clear ();

            foreach (Mercury.Server.Application.ConditionCriteriaDemographic currentCriteria in serverCondition.DemographicCriteria) {

                demographicCriteria.Add (new Mercury.Client.Core.Condition.ConditionCriteria.ConditionCriteriaDemographic (application, currentCriteria));

            }


            eventCriteria.Clear ();

            foreach (Mercury.Server.Application.ConditionCriteriaEvent currentCriteria in serverCondition.EventCriteria) {

                eventCriteria.Add (new Mercury.Client.Core.Condition.ConditionCriteria.ConditionCriteriaEvent (application, currentCriteria));

            }


            events.Clear ();

            foreach (String eventName in serverCondition.Events.Keys) {

                events.Add (eventName, new Mercury.Client.Core.Action.Action (application, serverCondition.Events[eventName]));

            }

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.Condition serverObject) {

            base.MapToServerObject ((Server.Application.CoreConfigurationObject)serverObject);


            serverObject.ConditionClassId = conditionClassId;

            serverObject.ConditionClassName = conditionClassName;


            serverObject.DemographicCriteria = new Mercury.Server.Application.ConditionCriteriaDemographic[demographicCriteria.Count];

            for (Int32 currentCriteriaIndex = 0; currentCriteriaIndex < demographicCriteria.Count; currentCriteriaIndex++) {

                serverObject.DemographicCriteria[currentCriteriaIndex] = (Server.Application.ConditionCriteriaDemographic)demographicCriteria[currentCriteriaIndex].ToServerObject ();

            }

            serverObject.EventCriteria = new Mercury.Server.Application.ConditionCriteriaEvent[eventCriteria.Count];

            for (Int32 currentCriteriaIndex = 0; currentCriteriaIndex < eventCriteria.Count; currentCriteriaIndex++) {

                serverObject.EventCriteria[currentCriteriaIndex] = (Server.Application.ConditionCriteriaEvent)eventCriteria[currentCriteriaIndex].ToServerObject ();

            }


            serverObject.Events = new Dictionary<String, Mercury.Server.Application.Action> ();

            foreach (String eventName in events.Keys) {

                serverObject.Events.Add (eventName, (Server.Application.Action)events[eventName].ToServerObject ());

            }


            return;

        }

        public override Object ToServerObject () {

            Server.Application.Condition serverObject = new Server.Application.Condition();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public Condition Copy () {

            Server.Application.Condition serverObject = (Server.Application.Condition)ToServerObject ();

            Condition copiedObject = new Condition (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (Condition compareCondition) {

            Boolean isEqual = base.IsEqual (compareCondition);


            if (conditionClassId != compareCondition.ConditionClassId) { isEqual = false; }


            foreach (String eventName in events.Keys) {

                isEqual = isEqual && (events[eventName].IsEqual (compareCondition.Events[eventName]));

                if (!isEqual) { break; }

            }


            #region Criteria Comparisons

            if (demographicCriteria.Count != compareCondition.DemographicCriteria.Count) { isEqual = false; }

            if (isEqual) {

                for (Int32 currentCriteriaIndex = 0; currentCriteriaIndex < demographicCriteria.Count; currentCriteriaIndex++) {

                    isEqual = isEqual && (demographicCriteria[currentCriteriaIndex].IsEqual (compareCondition.DemographicCriteria[currentCriteriaIndex]));

                    if (!isEqual) { break; }

                }

            }

            if (eventCriteria.Count != compareCondition.EventCriteria.Count) { isEqual = false; }

            if (isEqual) {

                for (Int32 currentCriteriaIndex = 0; currentCriteriaIndex < eventCriteria.Count; currentCriteriaIndex++) {

                    isEqual = isEqual && (eventCriteria[currentCriteriaIndex].IsEqual (compareCondition.EventCriteria[currentCriteriaIndex]));

                    if (!isEqual) { break; }

                }

            }

            #endregion


            return isEqual;

        }

        #endregion 

    }

}
