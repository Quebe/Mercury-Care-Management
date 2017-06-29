using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Population.PopulationEvents {

    [Serializable]
    public class PopulationServiceEvent : CoreObject {

        #region Private Properties

        private Int64 populationId;

        private Int64 serviceId;

        private Int64 exclusionServiceId;

        private Mercury.Server.Application.PopulationServiceEventAnchorDate anchorDate = Mercury.Server.Application.PopulationServiceEventAnchorDate.PopulationAnchorDate;

        private Int32 anchorDateValue;

        private Int32 scheduleDateValue;

        private Mercury.Server.Application.DateQualifier scheduleDateQualifier = Mercury.Server.Application.DateQualifier.Months;

        private Boolean reoccurring = false;

        private List<PopulationServiceEventThreshold> thresholds = new List<PopulationServiceEventThreshold> ();


        private MedicalServices.Service service = null;

        #endregion


        #region Public Properties

        public Int64 CoreObjectId { set { id = value; } }

        public Int64 PopulationId { get { return populationId; } set { populationId = value; } }

        public Int64 ServiceId { 
            
            get { return serviceId; } 
            
            set {

                if (serviceId != value) {

                    serviceId = value;

                    service = null;

                }

            }

        }

        public Int64 ExclusionServiceId { get { return exclusionServiceId; } set { exclusionServiceId = value; } }

        public Mercury.Server.Application.PopulationServiceEventAnchorDate AnchorDate { get { return anchorDate; } set { anchorDate = value; } }

        public Int32 AnchorDateValue { get { return anchorDateValue; } set { anchorDateValue = value; } }

        public Int32 ScheduleDateValue { get { return scheduleDateValue; } set { scheduleDateValue = value; } }

        public Mercury.Server.Application.DateQualifier ScheduleDateQualifier { get { return scheduleDateQualifier; } set { scheduleDateQualifier = value; } }

        public Boolean Reoccurring { get { return reoccurring; } set { reoccurring = value; } }

        public List<PopulationServiceEventThreshold> Thresholds { get { return thresholds; } set { thresholds = value; } }

        public SortedList<Int64, PopulationServiceEventThreshold> SortedThresholds {

            get {

                SortedList<Int64, PopulationServiceEventThreshold> sortedThresholds = new SortedList<Int64, PopulationServiceEventThreshold> ();

                Int64 thresholdKey = 0;

                foreach (PopulationServiceEventThreshold currentThreshold in thresholds) {

                    switch (currentThreshold.RelativeDateQualifier) {

                        case Mercury.Server.Application.DateQualifier.Months: thresholdKey = currentThreshold.RelativeDateValue * 30; break;

                        case Mercury.Server.Application.DateQualifier.Years: thresholdKey = currentThreshold.RelativeDateValue * 365; break;

                        case Mercury.Server.Application.DateQualifier.Days: default: thresholdKey = currentThreshold.RelativeDateValue; break;

                    }

                    while (sortedThresholds.Keys.Contains (thresholdKey)) {

                        thresholdKey = thresholdKey + 1;

                    }

                    sortedThresholds.Add (thresholdKey, currentThreshold);

                }

                return sortedThresholds;

            }

        }


        public String AnchorText {

            get {

                String anchorText = String.Empty;

                switch (anchorDate) {

                    case Mercury.Server.Application.PopulationServiceEventAnchorDate.PopulationAnchorDate:

                        anchorText = "Population Anchor Date";

                        break;

                    case Mercury.Server.Application.PopulationServiceEventAnchorDate.PreviousServiceDate:

                        anchorText = "Previous Service";

                        break;

                    case Mercury.Server.Application.PopulationServiceEventAnchorDate.PreviousServiceEvent:

                        anchorText = "Previous Service Event: " + anchorDateValue.ToString ();

                        break;

                    case Mercury.Server.Application.PopulationServiceEventAnchorDate.AgeByYears:

                        anchorText = "Age by Years: " + anchorDateValue.ToString ();

                        break;

                    case Mercury.Server.Application.PopulationServiceEventAnchorDate.AgeByMonths:

                        anchorText = "Age by Months: " + anchorDateValue.ToString ();

                        break;

                }

                return anchorText;

            }

        }

        public String ScheduleText {

            get {

                String scheduleText = String.Empty;

                if (scheduleDateValue != 0) {

                    if (reoccurring) {

                        scheduleText = "Reoccurring Every " + scheduleDateValue.ToString () + " " + scheduleDateQualifier.ToString ();

                    }

                    else {

                        scheduleText = "Once within " + scheduleDateValue.ToString () + " " + scheduleDateQualifier.ToString ();

                    }

                }

                else { scheduleText = "No Schedule"; }

                return scheduleText;

            }

        }

        public String ThresholdText (Application application) {

            String thresholdText = String.Empty;

            foreach (PopulationServiceEventThreshold currentThreshold in SortedThresholds.Values) { 

                thresholdText = thresholdText + Math.Abs (currentThreshold.RelativeDateValue) + " " + currentThreshold.RelativeDateQualifier.ToString () + " ";

                if (currentThreshold.RelativeDateValue <= 0) { thresholdText = thresholdText + "Before "; } else { thresholdText = thresholdText + "After "; }

                thresholdText = thresholdText + "Status: " + currentThreshold.StatusText + "; ";

                if (currentThreshold.Action != null) { thresholdText = thresholdText + currentThreshold.Action.Description + "; "; } else { thresholdText = thresholdText + "No Action; "; }

            }

            return thresholdText;

        }

        public String ServiceName {

            get {

                if ((service == null) && (Application != null)) {

                    service = Application.MedicalServiceGet (serviceId, true);

                }

                if (service != null) { return service.Name; }

                else { return String.Empty; }

            }

        }

        #endregion


        #region Constructors
        
        public PopulationServiceEvent (Application applicationReference) { BaseConstructor (applicationReference); }

        public PopulationServiceEvent (Application applicationReference, Mercury.Server.Application.PopulationServiceEvent serverObject) {

            BaseConstructor (applicationReference, serverObject);

           
            populationId = serverObject.PopulationId;

            serviceId = serverObject.ServiceId;

            exclusionServiceId = serverObject.ExclusionServiceId;

            anchorDate = serverObject.AnchorDate;

            anchorDateValue = serverObject.AnchorDateValue;

            scheduleDateValue = serverObject.ScheduleDateValue;

            scheduleDateQualifier = serverObject.ScheduleDateQualifier;

            reoccurring = serverObject.Reoccurring;

            foreach (Mercury.Server.Application.PopulationServiceEventThreshold currentThreshold in serverObject.Thresholds) {

                thresholds.Add (new PopulationServiceEventThreshold (application, currentThreshold));

            }


            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.PopulationServiceEvent serverObject) {

            base.MapToServerObject ((Server.Application.CoreObject)serverObject);


            serverObject.PopulationId = populationId;

            serverObject.ServiceId = serviceId;

            serverObject.ExclusionServiceId = exclusionServiceId;

            serverObject.AnchorDate = anchorDate;

            serverObject.AnchorDateValue = anchorDateValue;

            serverObject.ScheduleDateValue = scheduleDateValue;

            serverObject.ScheduleDateQualifier = scheduleDateQualifier;

            serverObject.Reoccurring = reoccurring;

            serverObject.Thresholds = new Mercury.Server.Application.PopulationServiceEventThreshold[thresholds.Count];

            for (Int32 currentThresholdIndex = 0; currentThresholdIndex < thresholds.Count; currentThresholdIndex++) {

                serverObject.Thresholds[currentThresholdIndex] = (Server.Application.PopulationServiceEventThreshold) thresholds[currentThresholdIndex].ToServerObject ();

            }


            return;

        }

        public override Object ToServerObject () {

            Server.Application.PopulationServiceEvent serverObject = new Server.Application.PopulationServiceEvent ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public PopulationServiceEvent Copy () {

            Server.Application.PopulationServiceEvent serverObject = (Server.Application.PopulationServiceEvent)ToServerObject ();

            PopulationServiceEvent copiedObject = new PopulationServiceEvent (application, serverObject);

            return copiedObject;

        }
        

        public Boolean IsEqual (PopulationServiceEvent compareEvent) {

            Boolean isEqual = true;

            if (this.serviceId != compareEvent.ServiceId) { isEqual = false; }

            if (this.exclusionServiceId != compareEvent.ExclusionServiceId) { isEqual = false; }

            if (this.anchorDate != compareEvent.AnchorDate) { isEqual = false; }

            if (this.anchorDateValue != compareEvent.AnchorDateValue) { isEqual = false; }

            if (this.reoccurring != compareEvent.Reoccurring) { isEqual = false; }

            if (this.scheduleDateValue != compareEvent.ScheduleDateValue) { isEqual = false; }

            if (this.scheduleDateQualifier != compareEvent.ScheduleDateQualifier) { isEqual = false; }


            if (isEqual) {

                foreach (PopulationServiceEventThreshold currentThreshold in thresholds) {

                    if (!compareEvent.HasThreshold (currentThreshold)) { isEqual = false; break; }

                }

            }

            return isEqual;

        }

        public Boolean HasThreshold (PopulationServiceEventThreshold forThreshold) {

            Boolean hasThreshold = false;

            foreach (PopulationServiceEventThreshold currentThreshold in thresholds) {

                if (currentThreshold.IsEqual (forThreshold)) { hasThreshold = true; break; }

            }

            return hasThreshold;

        }

        public PopulationServiceEventThreshold GetNewThreshold () {

            PopulationServiceEventThreshold threshold = new PopulationServiceEventThreshold (application);

            threshold.PopulationServiceEventId = Id; 

            threshold.PopulationId = populationId;

            threshold.RelativeDateValue = 0;

            threshold.RelativeDateQualifier = Mercury.Server.Application.DateQualifier.Months;

            return threshold;

        }

        #endregion

    }

}
