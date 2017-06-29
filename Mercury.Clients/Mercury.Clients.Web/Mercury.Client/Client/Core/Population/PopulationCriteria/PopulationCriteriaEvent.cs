using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Population.PopulationCriteria {

    [Serializable]
    public class PopulationCriteriaEvent : CoreObject {

        #region Private Properties

        private Int64 populationId;

        private Mercury.Server.Application.PopulationCriteriaEventType eventType = Mercury.Server.Application.PopulationCriteriaEventType.Identifying;

        private Int64 serviceId;

        #endregion


        #region Public Properties

        public Int64 PopulationId { get { return populationId; } set { populationId = value; } }

        public Mercury.Server.Application.PopulationCriteriaEventType EventType { get { return eventType; } set { eventType = value; } }

        public Int64 ServiceId { get { return serviceId; } set { serviceId = value; } }

        #endregion


        #region Constructors
        
        public PopulationCriteriaEvent (Application applicationReference) { BaseConstructor (applicationReference); }

        public PopulationCriteriaEvent (Application applicationReference, Mercury.Server.Application.PopulationCriteriaEvent serverCriteria) {

            BaseConstructor (applicationReference, serverCriteria);


            populationId = serverCriteria.PopulationId;

            eventType = serverCriteria.EventType;

            serviceId = serverCriteria.ServiceId;

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.PopulationCriteriaEvent serverPopulationCriteria) {

            base.MapToServerObject ((Server.Application.CoreObject)serverPopulationCriteria);


            serverPopulationCriteria.PopulationId = populationId;

            serverPopulationCriteria.EventType = eventType;

            serverPopulationCriteria.ServiceId = serviceId;
            


            return;

        }

        public override Object ToServerObject () {

            Server.Application.PopulationCriteriaEvent serverPopulationCriteria = new Server.Application.PopulationCriteriaEvent ();

            MapToServerObject (serverPopulationCriteria);

            return serverPopulationCriteria;

        }

        public PopulationCriteriaEvent Copy () {

            Server.Application.PopulationCriteriaEvent serverPopulationCriteria = (Server.Application.PopulationCriteriaEvent)ToServerObject ();

            PopulationCriteriaEvent copiedPopulationCriteria = new PopulationCriteriaEvent (application, serverPopulationCriteria);

            return copiedPopulationCriteria;

        }


        public Boolean IsEqual (PopulationCriteriaEvent compareCriteria) {

            Boolean isEqual = true;

            if (this.eventType != compareCriteria.EventType) { isEqual = false; }

            if (this.serviceId != compareCriteria.ServiceId) { isEqual = false; }

            return isEqual;

        }

        #endregion

    }

}
