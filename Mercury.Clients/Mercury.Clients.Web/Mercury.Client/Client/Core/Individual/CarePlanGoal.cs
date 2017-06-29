using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual {

    [Serializable]
    public class CarePlanGoal : CoreConfigurationObject {
        
        #region Private Properties

        private Int64 carePlanId = 0;

        private Server.Application.CarePlanItemInclusion inclusion = Server.Application.CarePlanItemInclusion.Optional;

        private String clinicalNarrative;

        private String commonNarrative;


        private Server.Application.CarePlanGoalTimeframe goalTimeframe = Server.Application.CarePlanGoalTimeframe.ShortTerm;

        private Int32 scheduleValue;

        private Server.Application.DateQualifier scheduleQualifier = Server.Application.DateQualifier.Months;

        private Int64 careMeasureId = 0;

        private List<CarePlanIntervention> interventions = new List<CarePlanIntervention> ();        

        #endregion


        #region Public Properties - Encapsulated

        public Int64 CarePlanId { get { return carePlanId; } set { carePlanId = value; } }

        public Server.Application.CarePlanItemInclusion Inclusion { get { return inclusion; } set { inclusion = value; } }


        public String ClinicalNarrative { get { return clinicalNarrative; } set { clinicalNarrative = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Description); } }

        public String CommonNarrative { get { return commonNarrative; } set { commonNarrative = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Description); } }


        public Server.Application.CarePlanGoalTimeframe GoalTimeframe { get { return goalTimeframe; } set { goalTimeframe = value; } }

        public Int32 ScheduleValue { get { return scheduleValue; } set { scheduleValue = value; } }

        public Server.Application.DateQualifier ScheduleQualifier { get { return scheduleQualifier; } set { scheduleQualifier = value; } }

        public Int64 CareMeasureId { get { return careMeasureId; } set { careMeasureId = value; } }

        public List<CarePlanIntervention> Interventions { get { return interventions; } set { interventions = value; } }

        #endregion


        #region Public Properties

        public String GoalTimeframeDescription {

            get {

                String description = String.Empty;

                description += Server.CommonFunctions.EnumerationToString (goalTimeframe).Replace (" ", "-") + ": "; 

                description += scheduleValue.ToString () + " " + scheduleQualifier.ToString ();

                return description;

            }

        }

        public String ScheduleDescription {

            get {

                String description = String.Empty;

                description += scheduleValue.ToString () + " " + scheduleQualifier.ToString ();

                return description;

            }

        }

        public CareMeasure CareMeasure { get { return application.CareMeasureGet (careMeasureId, true); } }

        public String CareMeasureName { get { return ((CareMeasure != null) ? CareMeasure.Name : String.Empty); } }

        #endregion 


        #region Constructors

        protected CarePlanGoal () { /* DO NOTHING, FOR INHERITANCE */ }

        public CarePlanGoal (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public CarePlanGoal (Application applicationReference, Mercury.Server.Application.CarePlanGoal serverObject) {

            BaseConstructor (applicationReference, serverObject);

            MapFromServerObject (serverObject);

            return;

        }

        #endregion


        #region Public Methods

        public void MapFromServerObject (Mercury.Server.Application.CarePlanGoal serverObject) {

            carePlanId = serverObject.CarePlanId;

            inclusion = serverObject.Inclusion;

            clinicalNarrative = serverObject.ClinicalNarrative;

            commonNarrative = serverObject.CommonNarrative;

            goalTimeframe = serverObject.GoalTimeframe;

            scheduleValue = serverObject.ScheduleValue;

            scheduleQualifier = serverObject.ScheduleQualifier;

            careMeasureId = serverObject.CareMeasureId;

            interventions.Clear ();

            if (serverObject.Interventions != null) {

                foreach (Mercury.Server.Application.CarePlanIntervention currentIntervention in serverObject.Interventions) {

                    CarePlanIntervention intervention = new CarePlanIntervention (application, currentIntervention);

                    intervention.CarePlanGoal = this;

                    interventions.Add (intervention);

                }

            }

            return;

        }

        public virtual void MapToServerObject (Server.Application.CarePlanGoal serverObject) {

            base.MapToServerObject ((Server.Application.CoreConfigurationObject)serverObject);


            serverObject.CarePlanId = carePlanId;

            serverObject.Inclusion = inclusion;

            serverObject.ClinicalNarrative = clinicalNarrative;

            serverObject.CommonNarrative = commonNarrative;

            serverObject.GoalTimeframe = goalTimeframe;

            serverObject.ScheduleValue = scheduleValue;

            serverObject.ScheduleQualifier = scheduleQualifier;

            serverObject.CareMeasureId = careMeasureId;


            serverObject.Interventions = new Server.Application.CarePlanIntervention[interventions.Count];

            Int32 currentIndex = 0;

            while (currentIndex < interventions.Count) {

                serverObject.Interventions[currentIndex] = (Server.Application.CarePlanIntervention)interventions[currentIndex].ToServerObject ();

                currentIndex = currentIndex + 1;

            }

            return;

        }

        public override Object ToServerObject () {

            Server.Application.CarePlanGoal serverObject = new Server.Application.CarePlanGoal ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public CarePlanGoal Copy () {

            Server.Application.CarePlanGoal serverObject = (Server.Application.CarePlanGoal)ToServerObject ();

            CarePlanGoal copiedObject = new CarePlanGoal (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (CarePlanGoal compareObject) {

            Boolean isEqual = base.IsEqual ((CoreConfigurationObject)compareObject);



            isEqual &= (compareObject.CarePlanId == carePlanId);

            isEqual &= (compareObject.Inclusion == Inclusion);

            isEqual &= (compareObject.ClinicalNarrative == clinicalNarrative);

            isEqual &= (compareObject.CommonNarrative == commonNarrative);

            isEqual &= (compareObject.GoalTimeframe == goalTimeframe);

            isEqual &= (compareObject.ScheduleValue == scheduleValue);

            isEqual &= (compareObject.ScheduleQualifier == scheduleQualifier);

            isEqual &= (compareObject.CareMeasureId == CareMeasureId);

            isEqual &= (interventions.Count == compareObject.Interventions.Count);


            if (isEqual) {

                for (Int32 currentIndex = 0; currentIndex < interventions.Count; currentIndex++) {

                    isEqual &= interventions[currentIndex].IsEqual (compareObject.Interventions[currentIndex]);

                }

            }

            
            return isEqual;

        }

        #endregion


        #region Public Methods

        public CarePlanIntervention CarePlanIntervention (Int64 careInterventionId) {

            foreach (CarePlanIntervention currentCarePlanIntervention in interventions) {

                if (currentCarePlanIntervention.CareInterventionId == careInterventionId) {

                    return currentCarePlanIntervention;

                }

            }

            return null;

        }


        public CarePlanIntervention CarePlanIntervention (String careInterventionName) {

            foreach (CarePlanIntervention currentCarePlanIntervention in interventions) {

                if (currentCarePlanIntervention.Name == careInterventionName) {

                    return currentCarePlanIntervention;

                }

            }

            return null;

        }

        public Boolean ContainsCareIntervention (Int64 careInterventionId) {

            foreach (CarePlanIntervention currentCarePlanIntervention in interventions) {

                if (currentCarePlanIntervention.CareInterventionId == careInterventionId) {

                    return true;

                }

            }

            return false;

        }

        #endregion
    
    }

}

