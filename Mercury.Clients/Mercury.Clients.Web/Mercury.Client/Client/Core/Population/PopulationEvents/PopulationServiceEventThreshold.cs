using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Population.PopulationEvents {

    [Serializable]
    public class PopulationServiceEventThreshold : CoreObject {

        #region Private Properties

        private Int64 populationServiceEventId;

        private Int64 populationId;

        private Int32 relativeDateValue;

        private Mercury.Server.Application.DateQualifier relativeDateQualifier = Mercury.Server.Application.DateQualifier.Months;

        private Mercury.Server.Application.PopulationServiceEventStatus status = Mercury.Server.Application.PopulationServiceEventStatus.CompliantOrNoChange;

        private Mercury.Client.Core.Action.Action action = new Core.Action.Action (null);

        #endregion


        #region Public Properties

        public Int64 PopulationServiceEventId { get { return populationServiceEventId; } set { populationServiceEventId = value; } }

        public Int64 PopulationId { get { return populationId; } set { populationId = value; } }

        public Int32 RelativeDateValue { get { return relativeDateValue; } set { relativeDateValue = value; } }

        public Mercury.Server.Application.DateQualifier RelativeDateQualifier { get { return relativeDateQualifier; } set { relativeDateQualifier = value; } }

        public Mercury.Server.Application.PopulationServiceEventStatus Status { get { return status; } set { status = value; } }

        public String StatusText {

            get {

                String statusText = String.Empty;

                switch (status) {

                    case Mercury.Server.Application.PopulationServiceEventStatus.CompliantOrNoChange: statusText = "No Change"; break;

                    case Mercury.Server.Application.PopulationServiceEventStatus.Open: statusText = "Open"; break;

                    case Mercury.Server.Application.PopulationServiceEventStatus.OpenInformational: statusText = "Open - Informational"; break;

                    case Mercury.Server.Application.PopulationServiceEventStatus.OpenWarning: statusText = "Open - Warning"; break;

                    case Mercury.Server.Application.PopulationServiceEventStatus.OpenCritical: statusText = "Open - Critical"; break;

                    default: statusText = "Open - Unknown"; break;

                }

                return statusText;

            }

        }

        public Core.Action.Action Action { get { return action; } set { action = value; } }

        #endregion

        
        #region Constructors
        
        public PopulationServiceEventThreshold (Application applicationReference) { BaseConstructor (applicationReference); }

        public PopulationServiceEventThreshold (Application applicationReference, Mercury.Server.Application.PopulationServiceEventThreshold serverObject) {

            BaseConstructor (applicationReference, serverObject);


            populationServiceEventId = serverObject.PopulationServiceEventId;

            populationId = serverObject.PopulationId;

            relativeDateValue = serverObject.RelativeDateValue;

            relativeDateQualifier = serverObject.RelativeDateQualifier;

            status = serverObject.Status;

            action = new Mercury.Client.Core.Action.Action (application, serverObject.Action);


            return;

        }

        #endregion

        
        #region Public Methods

        public virtual void MapToServerObject (Server.Application.PopulationServiceEventThreshold serverObject) {

            base.MapToServerObject ((Server.Application.CoreObject)serverObject);


            serverObject.PopulationServiceEventId = populationServiceEventId;

            serverObject.PopulationId = populationId;

            serverObject.RelativeDateValue = relativeDateValue;

            serverObject.RelativeDateQualifier = relativeDateQualifier;

            serverObject.Status = status;

            serverObject.Action = (Server.Application.Action) action.ToServerObject ();


            return;

        }

        public override Object ToServerObject () {

            Server.Application.PopulationServiceEventThreshold serverObject = new Server.Application.PopulationServiceEventThreshold ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public PopulationServiceEventThreshold Copy () {

            Server.Application.PopulationServiceEventThreshold serverObject = (Server.Application.PopulationServiceEventThreshold)ToServerObject ();

            PopulationServiceEventThreshold copiedObject = new PopulationServiceEventThreshold (application, serverObject);

            return copiedObject;

        }
        
        public Boolean IsEqual (PopulationServiceEventThreshold compareEvent) {

            Boolean isEqual = true;


            if (this.relativeDateValue != compareEvent.RelativeDateValue) { isEqual = false; }

            if (this.relativeDateQualifier != compareEvent.RelativeDateQualifier) { isEqual = false; }

            if (this.status != compareEvent.Status) { isEqual = false; }

            if (isEqual) { isEqual = isEqual && action.IsEqual (compareEvent.action); }


            return isEqual;

        }
        
        #endregion

    }

}
