using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Population.PopulationEvents {

    [Serializable]
    public class PopulationTriggerEvent : CoreObject {

        #region Private Properties

        private Int64 populationId;

        private Mercury.Server.Application.PopulationTriggerEventType eventType = Mercury.Server.Application.PopulationTriggerEventType.Service;

        private Int64 serviceId;

        private Mercury.Server.Application.MetricType metricType = Mercury.Server.Application.MetricType.Health;

        private Int64 metricId;

        private Decimal metricMinimum;

        private Decimal metricMaximum;

        private Int64 authorizedServiceId;

        private Int64 problemStatementId;

        private Core.Action.Action action = new Mercury.Client.Core.Action.Action (null);

        #endregion


        #region Public Properties - Encapsulated

        public Int64 CoreObjectId { set { id = value; } }

        public Int64 PopulationId { get { return populationId; } set { populationId = value; } }

        public Mercury.Server.Application.PopulationTriggerEventType EventType { get { return eventType; } set { eventType = value; } }

        public Int64 ServiceId { get { return serviceId; } set { serviceId = value; } }

        public Mercury.Server.Application.MetricType MetricType { get { return metricType; } set { metricType = value; } }

        public Int64 MetricId { get { return metricId; } set { metricId = value; } }

        public Decimal MetricMinimum { get { return metricMinimum; } set { metricMinimum = value; } }

        public Decimal MetricMaximum { get { return metricMaximum; } set { metricMaximum = value; } }

        public Int64 AuthorizedServiceId { get { return authorizedServiceId; } set { authorizedServiceId = value; } }

        public Int64 ProblemStatementId { get { return problemStatementId; } set { problemStatementId = value; } }

        public Core.Action.Action Action { get { return action; } set { action = value; } }

        #endregion 


        #region Public Properties

        public String TriggerText {

            get {

                if (Application == null) { return String.Empty; }


                String triggerText = String.Empty;

                switch (eventType) {

                    case Mercury.Server.Application.PopulationTriggerEventType.Service:

                        Core.MedicalServices.Service medicalService = Application.MedicalServiceGet (serviceId, true);

                        if (medicalService != null) {

                            triggerText = medicalService.Name;

                        }

                        break;

                    case Mercury.Server.Application.PopulationTriggerEventType.Metric:

                        Core.Metrics.Metric metric = Application.MetricGet (metricId, false);

                        if (metric != null) {

                            triggerText = metric.MetricType.ToString () + ": " + metric.Name + " [" + metricMinimum.ToString () + " - " + MetricMaximum.ToString () + "]";

                        }

                        break;

                    case Mercury.Server.Application.PopulationTriggerEventType.AuthorizedService:

                        Core.AuthorizedServices.AuthorizedService authorizedService = Application.AuthorizedServiceGet (authorizedServiceId);

                        if (authorizedService != null) {

                            triggerText = authorizedService.Name;

                        }

                        break;


                }

                return triggerText;

            }

        }

        public Core.Individual.ProblemStatement ProblemStatement { get { return application.ProblemStatementGet (problemStatementId, true); } }

        #endregion
        

        #region Constructors
        
        public PopulationTriggerEvent (Application applicationReference) { BaseConstructor (applicationReference); }

        public PopulationTriggerEvent (Application applicationReference, Mercury.Server.Application.PopulationTriggerEvent serverObject) {

            BaseConstructor (applicationReference, serverObject);


            populationId = serverObject.PopulationId;

            eventType = serverObject.EventType;

            serviceId = serverObject.ServiceId;

            metricType = serverObject.MetricType;

            metricId = serverObject.MetricId;

            metricMinimum = serverObject.MetricMinimum;

            metricMaximum = serverObject.MetricMaximum;

            authorizedServiceId = serverObject.AuthorizedServiceId;

            problemStatementId = serverObject.ProblemStatementId;

            action = new Mercury.Client.Core.Action.Action (application, serverObject.Action);

            createAccountInfo = serverObject.CreateAccountInfo;

            modifiedAccountInfo = serverObject.ModifiedAccountInfo;

            return;

        }

        #endregion

        
        #region Public Methods

        public virtual void MapToServerObject (Server.Application.PopulationTriggerEvent serverObject) {

            base.MapToServerObject ((Server.Application.CoreObject)serverObject);


            serverObject.PopulationId = populationId;

            serverObject.EventType = eventType;

            serverObject.ServiceId = serviceId;

            serverObject.MetricType = metricType;

            serverObject.MetricId = metricId;

            serverObject.MetricMinimum = metricMinimum;

            serverObject.MetricMaximum = metricMaximum;

            serverObject.AuthorizedServiceId = authorizedServiceId;

            serverObject.ProblemStatementId = problemStatementId;

            serverObject.Action = (Server.Application.Action)action.ToServerObject ();


            return;

        }

        public override Object ToServerObject () {

            Server.Application.PopulationTriggerEvent serverObject = new Server.Application.PopulationTriggerEvent ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public PopulationTriggerEvent Copy () {

            Server.Application.PopulationTriggerEvent serverObject = (Server.Application.PopulationTriggerEvent)ToServerObject ();

            PopulationTriggerEvent copiedObject = new PopulationTriggerEvent (application, serverObject);

            return copiedObject;

        }
        
        public Boolean IsEqual (PopulationTriggerEvent compareEvent) {

            Boolean isEqual = true;

            if (this.eventType != compareEvent.EventType) { isEqual = false; }

            else {

                switch (this.eventType) {

                    case Mercury.Server.Application.PopulationTriggerEventType.Service:

                        if (this.serviceId != compareEvent.ServiceId) { isEqual = false; }

                        break;

                    case Mercury.Server.Application.PopulationTriggerEventType.Metric:

                        if (this.metricType != compareEvent.metricType) { isEqual = false; }

                        else {

                            if (this.metricId != compareEvent.MetricId) { isEqual = false; }

                            if (this.metricMaximum != compareEvent.MetricMaximum) { isEqual = false; }

                            if (this.metricMaximum != compareEvent.MetricMaximum) { isEqual = false; }

                        }

                        break;

                    case Mercury.Server.Application.PopulationTriggerEventType.AuthorizedService:

                        if (this.authorizedServiceId != compareEvent.AuthorizedServiceId) { isEqual = false; }

                        break;

                }

            }

            isEqual = isEqual && (problemStatementId == compareEvent.ProblemStatementId);

            if (isEqual) { isEqual = isEqual && action.IsEqual (compareEvent.action); }

            return isEqual;

        }

        #endregion

    }

}
