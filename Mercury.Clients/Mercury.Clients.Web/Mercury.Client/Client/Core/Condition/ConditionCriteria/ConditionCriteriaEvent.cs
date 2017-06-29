using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Condition.ConditionCriteria {

    [Serializable]
    public class ConditionCriteriaEvent : CoreObject {
        
        #region Private Properties

        private Int64 conditionId;

        private Mercury.Server.Application.ConditionCriteriaEventType eventType = Mercury.Server.Application.ConditionCriteriaEventType.Identifying;

        private Int64 serviceId;

        #endregion


        #region Public Properties

        public Int64 ConditionId { get { return conditionId; } set { conditionId = value; } }

        public Mercury.Server.Application.ConditionCriteriaEventType EventType { get { return eventType; } set { eventType = value; } }

        public Int64 ServiceId { get { return serviceId; } set { serviceId = value; } }

        #endregion


        #region Constructors
        
        public ConditionCriteriaEvent (Application applicationReference) { BaseConstructor (applicationReference); }

        public ConditionCriteriaEvent (Application applicationReference, Mercury.Server.Application.ConditionCriteriaEvent serverCriteria) {

            BaseConstructor (applicationReference, serverCriteria);


            conditionId = serverCriteria.ConditionId;

            eventType = serverCriteria.EventType;

            serviceId = serverCriteria.ServiceId;

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.ConditionCriteriaEvent serverConditionCriteria) {

            base.MapToServerObject ((Server.Application.CoreObject)serverConditionCriteria);


            serverConditionCriteria.ConditionId = conditionId;

            serverConditionCriteria.EventType = eventType;

            serverConditionCriteria.ServiceId = serviceId;
            


            return;

        }

        public override Object ToServerObject () {

            Server.Application.ConditionCriteriaEvent serverConditionCriteria = new Server.Application.ConditionCriteriaEvent ();

            MapToServerObject (serverConditionCriteria);

            return serverConditionCriteria;

        }

        public ConditionCriteriaEvent Copy () {

            Server.Application.ConditionCriteriaEvent serverConditionCriteria = (Server.Application.ConditionCriteriaEvent)ToServerObject ();

            ConditionCriteriaEvent copiedConditionCriteria = new ConditionCriteriaEvent (application, serverConditionCriteria);

            return copiedConditionCriteria;

        }


        public Boolean IsEqual (ConditionCriteriaEvent compareCriteria) {

            Boolean isEqual = true;

            if (this.eventType != compareCriteria.EventType) { isEqual = false; }

            if (this.serviceId != compareCriteria.ServiceId) { isEqual = false; }

            return isEqual;

        }

        #endregion



    }

}
