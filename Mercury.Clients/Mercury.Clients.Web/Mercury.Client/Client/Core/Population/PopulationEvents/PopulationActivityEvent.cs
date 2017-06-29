using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Population.PopulationEvents {

    [Serializable]
    public class PopulationActivityEvent : CoreObject {

        #region Private Properties
        
        private Int64 populationId;

        private Mercury.Server.Application.PopulationActivityScheduleType scheduleType = Mercury.Server.Application.PopulationActivityScheduleType.ByFrequency;

        private Int32 scheduleValue;

        private Mercury.Server.Application.DateQualifier scheduleQualifier = Mercury.Server.Application.DateQualifier.Months;

        private Mercury.Server.Application.PopulationActivityEventAnchorDate anchorDate = Mercury.Server.Application.PopulationActivityEventAnchorDate.PopulationEffectiveDate;

        private Boolean reoccurring = false;

        private Mercury.Server.Application.PopulationActivityPerformActionDateType performActionDateType = Mercury.Server.Application.PopulationActivityPerformActionDateType.Immediately;

        private Core.Action.Action action = new Mercury.Client.Core.Action.Action (null);

        #endregion


        #region Public Properties

        public Int64 CoreObjectId { set { id = value; } }

        public Int64 PopulationId { get { return populationId; } set { populationId = value; } }

        public Mercury.Server.Application.PopulationActivityScheduleType ScheduleType { get { return scheduleType; } set { scheduleType = value; } }

        public Int32 ScheduleValue { get { return scheduleValue; } set { scheduleValue = value; } }

        public Mercury.Server.Application.DateQualifier ScheduleQualifier { get { return scheduleQualifier; } set { scheduleQualifier = value; } }

        public Mercury.Server.Application.PopulationActivityEventAnchorDate AnchorDate { get { return anchorDate; } set { anchorDate = value; } }

        public Boolean Reoccurring { get { return reoccurring; } set { reoccurring = value; } }

        public Mercury.Server.Application.PopulationActivityPerformActionDateType PerformActionDateType { get { return performActionDateType; } set { performActionDateType = value; } }

        public Core.Action.Action Action { get { return action; } set { action = value; } }

        public override String Description {

            get {

                String description = String.Empty;

                switch (scheduleType) {

                    case Mercury.Server.Application.PopulationActivityScheduleType.BirthMonth:

                    case Mercury.Server.Application.PopulationActivityScheduleType.Monthly:

                    case Mercury.Server.Application.PopulationActivityScheduleType.Quarterly:

                        description = "On " + scheduleType.ToString ();

                        break;

                    case Mercury.Server.Application.PopulationActivityScheduleType.ByFrequency:

                        description = scheduleValue.ToString () + " " + scheduleQualifier.ToString ();

                        break;

                    case Mercury.Server.Application.PopulationActivityScheduleType.CalendarMonth:

                        description = "Calendar Month of " + scheduleValue.ToString ();

                        break;

                }

                if (reoccurring) { description = description + " Reoccurring"; }

                description = description + " take action on " + performActionDateType.ToString ();

                description = description + " starting from " + Server.CommonFunctions.EnumerationToString (anchorDate);


                return description;

            }

        }

        #endregion


        #region Constructors

        override protected void BaseConstructor (Application applicationReference) {

            base.BaseConstructor (applicationReference);

            action = new Action.Action (applicationReference);

            return;

        }

        public PopulationActivityEvent (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public PopulationActivityEvent (Application applicationReference, Mercury.Server.Application.PopulationActivityEvent serverEvent) {

            BaseConstructor (applicationReference, serverEvent);


            populationId = serverEvent.PopulationId;

            scheduleType = serverEvent.ScheduleType;

            scheduleValue = serverEvent.ScheduleValue;

            scheduleQualifier = serverEvent.ScheduleQualifier;


            anchorDate = serverEvent.AnchorDate;

            reoccurring = serverEvent.Reoccurring;

            performActionDateType = serverEvent.PerformActionDateType;


            action = new Mercury.Client.Core.Action.Action (applicationReference, serverEvent.Action);

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.PopulationActivityEvent serverEvent) {

            base.MapToServerObject ((Server.Application.CoreObject)serverEvent);


            serverEvent.PopulationId = populationId;

            serverEvent.ScheduleType = scheduleType;

            serverEvent.ScheduleValue = scheduleValue;

            serverEvent.ScheduleQualifier = scheduleQualifier;

            serverEvent.AnchorDate = anchorDate;

            serverEvent.Reoccurring = reoccurring;

            serverEvent.PerformActionDateType = performActionDateType;

            serverEvent.Action = (Server.Application.Action) action.ToServerObject ();

            
            return;

        }

        public override Object ToServerObject () {

            Server.Application.PopulationActivityEvent serverEvent = new Server.Application.PopulationActivityEvent ();

            MapToServerObject (serverEvent);

            return serverEvent;

        }

        public PopulationActivityEvent Copy () {

            Server.Application.PopulationActivityEvent serverEvent = (Server.Application.PopulationActivityEvent)ToServerObject ();

            PopulationActivityEvent copiedEvent = new PopulationActivityEvent (application, serverEvent);

            return copiedEvent;

        }

        
        public Boolean IsEqual (PopulationActivityEvent compareEvent) {

            Boolean isEqual = true;

            if (this.scheduleType != compareEvent.ScheduleType) { isEqual = false; }

            if (this.scheduleValue != compareEvent.ScheduleValue) { isEqual = false; }

            if (this.scheduleQualifier != compareEvent.ScheduleQualifier) { isEqual = false; }


            isEqual &= (anchorDate == compareEvent.AnchorDate);


            if (this.reoccurring != compareEvent.Reoccurring) { isEqual = false; }

            if (this.performActionDateType != compareEvent.PerformActionDateType) { isEqual = false; }

            if (isEqual) { isEqual = isEqual && action.IsEqual (compareEvent.action); }


            return isEqual;

        }


        #endregion

    }

}
