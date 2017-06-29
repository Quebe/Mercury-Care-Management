using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Activity {

    [Serializable]
    public class Activity : CoreObject {

        #region Private Properties

        private Server.Application.ActivityType activityType = Server.Application.ActivityType.Manual;


        private Server.Application.ActivityAnchorDate initialAnchorDate = Server.Application.ActivityAnchorDate.OwnerObjectEffectiveDate;

        private Server.Application.ActivityAnchorDate anchorDate = Server.Application.ActivityAnchorDate.ActivityCreateDate;


        private Server.Application.ActivityScheduleType scheduleType = Server.Application.ActivityScheduleType.ByFrequency;

        private Int32 scheduleValue;

        private Server.Application.DateQualifier scheduleQualifier = Server.Application.DateQualifier.Months;

        private Int32 constraintValue;

        private Server.Application.DateQualifier constraintQualifier = Server.Application.DateQualifier.Months;

        private Boolean reoccurring = false;


        private Server.Application.ActivityPerformActionDate performActionDate = Server.Application.ActivityPerformActionDate.Immediately;

        private Core.Action.Action action = null;
        
        private List<ActivityThreshold> thresholds = new List<ActivityThreshold> ();

        #endregion


        #region Public Properties

        public Int64 CoreObjectId { set { base.id = value; } }

        public override String Name {

            get {

                String nameValue = base.Name;

                switch (activityType) {

                    case Server.Application.ActivityType.Manual:

                        break;

                    case Server.Application.ActivityType.Automated:

                    case Server.Application.ActivityType.Workflow:

                        nameValue = Description;
                                                
                        break;

                    case Server.Application.ActivityType.Monitor:

                        break;

                }

                return nameValue;

            }

            set {

                base.Name = value;

            }

        }

        public override String Description {

            get {

                String descriptionValue = base.Description;


                switch (activityType) {

                    case Server.Application.ActivityType.Manual:

                        break;

                    case Server.Application.ActivityType.Automated:

                    case Server.Application.ActivityType.Workflow:

                        if (action != null) {

                            descriptionValue = action.Description;

                        }
                        
                        break;

                    case Server.Application.ActivityType.Monitor:

                        break;

                }

                return descriptionValue;


            }

            set {

                base.Description = value;

            }

        }

        public Server.Application.ActivityType ActivityType { get { return activityType; } set { activityType = value; } }


        public Server.Application.ActivityAnchorDate InitialAnchorDate { get { return initialAnchorDate; } set { initialAnchorDate = value; } }

        public Server.Application.ActivityAnchorDate AnchorDate { get { return anchorDate; } set { anchorDate = value; } }


        public Server.Application.ActivityScheduleType ScheduleType { get { return scheduleType; } set { scheduleType = value; } }

        public Int32 ScheduleValue { get { return scheduleValue; } set { scheduleValue = value; } }

        public Server.Application.DateQualifier ScheduleQualifier { get { return scheduleQualifier; } set { scheduleQualifier = value; } }

        public Int32 ConstraintValue { get { return constraintValue; } set { constraintValue = value; } }

        public Server.Application.DateQualifier ConstraintQualifier { get { return constraintQualifier; } set { constraintQualifier = value; } }

        public Boolean Reoccurring { get { return reoccurring; } set { reoccurring = value; } }


        public Server.Application.ActivityPerformActionDate PerformActionDate { get { return performActionDate; } set { performActionDate = value; } }

        public Core.Action.Action Action { get { return action; } set { action = value; } }

        public List<ActivityThreshold> Thresholds { get { return thresholds; } set { thresholds = value; } }

        #endregion


        #region Public Properties - Calculated and Extended

        public String AnchorDescription {

            get {

                String description = String.Empty;

                description = description + "Starting from " + Server.CommonFunctions.EnumerationToString (anchorDate);

                return description;

            }

        }

        public String ScheduleDescription {

            get {

                String description = String.Empty;

                switch (scheduleType) {

                    case Mercury.Server.Application.ActivityScheduleType.BirthMonth:

                        if (!reoccurring) { description += "Once on the Birth Month"; }

                        else { description += "Every Birth Month"; }

                        break;

                    case Mercury.Server.Application.ActivityScheduleType.Monthly:

                        if (!reoccurring) { description += "Once on the Month"; }

                        else { description += "Monthly"; }
                        
                        break;

                    case Mercury.Server.Application.ActivityScheduleType.Quarterly:

                        if (!reoccurring) { description += "Once on the Quarter"; }

                        else { description += "Quarterly"; }

                        break;

                    case Mercury.Server.Application.ActivityScheduleType.ByFrequency:

                        if (!reoccurring) { description += "Once at "; }

                        else { description += "Every "; }

                        description += scheduleValue.ToString () + " " + scheduleQualifier.ToString ();

                        break;

                    case Mercury.Server.Application.ActivityScheduleType.CalendarMonth:

                        if (!reoccurring) { description += "Once on "; }

                        else { description += "Every "; }

                        description += "Calendar Month of " + scheduleValue.ToString ();

                        break;

                }


                // description = description + " take action on " + Server.CommonFunctions.EnumerationToString (performActionDate); // RESERVED FOR FUTURE USE


                description = description + "; activity is available ";

                if (constraintValue == 0) { description += "immediately"; }

                else { description += Math.Abs (constraintValue).ToString () + " " + constraintQualifier.ToString () + " before scheduled"; }
                

                return description;

            }

        }

        public String ThresholdsDescription {

            get {

                String thresholdText = String.Empty;

                foreach (ActivityThreshold currentThreshold in SortedThresholds.Values) {

                    thresholdText = thresholdText + Math.Abs (currentThreshold.RelativeDateValue) + " " + currentThreshold.RelativeDateQualifier.ToString () + " ";

                    if (currentThreshold.RelativeDateValue <= 0) { thresholdText = thresholdText + "Before "; } else { thresholdText = thresholdText + "After "; }

                    thresholdText = thresholdText + "Status: " + Server.CommonFunctions.EnumerationToString (currentThreshold.Status) + ", ";

                    if (currentThreshold.Action != null) { thresholdText = thresholdText + currentThreshold.Action.Description + "; "; } else { thresholdText = thresholdText + "No Action; "; }

                }

                return thresholdText;

            }

        }

        public SortedList<Int64, ActivityThreshold> SortedThresholds {

            get {

                SortedList<Int64, ActivityThreshold> sortedThresholds = new SortedList<Int64, ActivityThreshold> ();

                Int64 thresholdKey = 0;

                foreach (ActivityThreshold currentThreshold in thresholds) {

                    switch (currentThreshold.RelativeDateQualifier) {

                        case Server.Application.DateQualifier.Months: thresholdKey = currentThreshold.RelativeDateValue * 30; break;

                        case Server.Application.DateQualifier.Years: thresholdKey = currentThreshold.RelativeDateValue * 365; break;

                        case Server.Application.DateQualifier.Days:

                        default: thresholdKey = currentThreshold.RelativeDateValue; break;

                    }

                    while (sortedThresholds.Keys.Contains (thresholdKey)) {

                        thresholdKey = thresholdKey + 1;

                    }

                    sortedThresholds.Add (thresholdKey, currentThreshold);

                }

                return sortedThresholds;

            }

        }

        #endregion 

        
        #region Constructors

        public void BaseConstructor (Application applicationReference, Mercury.Server.Application.Activity serverObject) {

            BaseConstructor (applicationReference, ((Server.Application.CoreObject)serverObject));


            activityType = serverObject.ActivityType;

            initialAnchorDate = serverObject.InitialAnchorDate;

            anchorDate = serverObject.AnchorDate;

            scheduleType = serverObject.ScheduleType;

            scheduleValue = serverObject.ScheduleValue;

            scheduleQualifier = serverObject.ScheduleQualifier;

            constraintValue = serverObject.ConstraintValue;

            constraintQualifier = serverObject.ConstraintQualifier;

            reoccurring = serverObject.Reoccurring;

            performActionDate = serverObject.PerformActionDate;


            action = null;

            if (serverObject.Action != null) { action = new Action.Action (applicationReference, serverObject.Action); }


            foreach (Server.Application.ActivityThreshold currentServerThreshold in serverObject.Thresholds) {

                ActivityThreshold activityThreshold = new ActivityThreshold (applicationReference, currentServerThreshold);

                thresholds.Add (activityThreshold);

            }

            return;

        }

        public Activity () { /* DO NOTHING */ }

        public Activity (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public Activity (Application applicationReference, Mercury.Server.Application.Activity serverObject) {

            BaseConstructor (applicationReference, serverObject);

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.Activity serverObject) {

            base.MapToServerObject ((Server.Application.CoreObject)serverObject);


            serverObject.ActivityType = activityType;

            serverObject.InitialAnchorDate = initialAnchorDate;

            serverObject.AnchorDate = anchorDate;

            serverObject.ScheduleType = scheduleType;

            serverObject.ScheduleValue = scheduleValue;

            serverObject.ScheduleQualifier = scheduleQualifier;

            serverObject.ConstraintValue = constraintValue;

            serverObject.ConstraintQualifier = constraintQualifier;

            serverObject.Reoccurring = reoccurring;

            serverObject.PerformActionDate = performActionDate;


            if (action != null) { serverObject.Action = (Server.Application.Action)action.ToServerObject (); }


            serverObject.Thresholds = new Server.Application.ActivityThreshold[thresholds.Count];

            Int32 thresholdIndex = 0;

            while (thresholdIndex < thresholds.Count) {

                serverObject.Thresholds[thresholdIndex] = (Server.Application.ActivityThreshold) thresholds[thresholdIndex].ToServerObject ();

                thresholdIndex = thresholdIndex + 1;

            }


            return;

        }

        public override Object ToServerObject () {

            Server.Application.Activity serverObject = new Server.Application.Activity ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public Activity Copy () {

            Server.Application.Activity serverObject = (Server.Application.Activity)ToServerObject ();

            Activity copiedActivity = new Activity (application, serverObject);

            return copiedActivity;

        }

        public Boolean IsEqual (Activity compareObject) {

            Boolean isEqual = base.IsEqual ((CoreObject)compareObject);


            return isEqual;

        }

        #endregion 


        #region Public Methods

        public Boolean HasThreshold (ActivityThreshold forThreshold) {

            Boolean hasThreshold = false;

            foreach (ActivityThreshold currentThreshold in thresholds) {

                if (currentThreshold.IsEqual (forThreshold)) { hasThreshold = true; break; }

            }

            return hasThreshold;

        }

        public ActivityThreshold GetNewThreshold () {

            ActivityThreshold threshold = new ActivityThreshold (application);

            
            threshold.RelativeDateValue = 0;

            threshold.RelativeDateQualifier = Mercury.Server.Application.DateQualifier.Months;

            return threshold;

        }

        #endregion 

    }

}
