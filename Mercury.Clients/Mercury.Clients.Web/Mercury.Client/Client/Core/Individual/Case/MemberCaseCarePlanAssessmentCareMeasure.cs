using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual.Case {

    [Serializable]
    public class MemberCaseCarePlanAssessmentCareMeasure : CoreObject {

        #region Private Properties

        private Int64 memberCaseCarePlanAssessmentId = 0;

        private MemberCaseCarePlanAssessment memberCaseCarePlanAssessment = null; // LOCAL PARENT REFEERENCE ONLY

        private Int64 careMeasureDomainId = 0;

        private String careMeasureDomainName = String.Empty;

        private Int64 careMeasureClassId = 0;

        private String careMeasureClassName = String.Empty;

        private Int64 careMeasureId = 0;

        private Decimal targetValue = 0;


        private List<MemberCaseCarePlanAssessmentCareMeasureGoal> goals = new List<MemberCaseCarePlanAssessmentCareMeasureGoal> ();

        private List<MemberCaseCarePlanAssessmentCareMeasureComponent> components = new List<MemberCaseCarePlanAssessmentCareMeasureComponent> ();

        #endregion

        
        #region Public Properties - Encapsulated

        public Int64 MemberCaseCarePlanAssessmentId { get { return memberCaseCarePlanAssessmentId; } set { memberCaseCarePlanAssessmentId = value; } }

        public MemberCaseCarePlanAssessment MemberCaseCarePlanAssessment { get { return memberCaseCarePlanAssessment; } set { memberCaseCarePlanAssessment = value; } }


        public Int64 CareMeasureDomainId { get { return careMeasureDomainId; } set { careMeasureDomainId = value; } }

        public String CareMeasureDomainName { get { return careMeasureDomainName; } set { careMeasureDomainName = value; } }


        public Int64 CareMeasureClassId { get { return careMeasureClassId; } set { careMeasureClassId = value; } }

        public String CareMeasureClassName { get { return careMeasureClassName; } set { careMeasureClassName = value; } }


        public Int64 CareMeasureId { get { return careMeasureId; } set { careMeasureId = value; } }

        public Decimal TargetValue { get { return targetValue; } set { targetValue = ((value >= 0) && (value <= 5)) ? value : 0; } }


        public List<MemberCaseCarePlanAssessmentCareMeasureGoal> Goals { get { return goals; } set { goals = value; } }

        public List<MemberCaseCarePlanAssessmentCareMeasureComponent> Components { get { return components; } set { components = value; } }

        #endregion


        #region Public Properties

        public List<CareMeasureScale> CareMeasureScales {

            get {

                List<CareMeasureScale> careMeasureScales =

                    (from currentComponent in components

                     orderby currentComponent.CareMeasureScaleName

                     select currentComponent.CareMeasureScale).Distinct ().ToList ();

                return careMeasureScales;

            }

        }

        public Decimal ComponentScore {

            get {

                Decimal componentScore = 0;

                Decimal componentTotal = 0;

                Int32 componentCount = 0;

                foreach (MemberCaseCarePlanAssessmentCareMeasureComponent currentComponent in components) {

                    if (currentComponent.ComponentValue > 0) {

                        componentCount = componentCount + 1;

                        componentTotal = componentTotal + currentComponent.ComponentValue;

                    }

                }

                if (componentCount > 0) { componentScore = componentTotal / componentCount; }

                return componentScore;

            }

        }

        #endregion 


        #region Constructors

        public MemberCaseCarePlanAssessmentCareMeasure (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public MemberCaseCarePlanAssessmentCareMeasure (Application applicationReference, Server.Application.MemberCaseCarePlanAssessmentCareMeasure serverObject) {

            base.BaseConstructor (applicationReference, serverObject);

            MapFromServerObject (serverObject);

            return;

        }

        #endregion 

        
        #region Public Methods

        public void MapFromServerObject (Server.Application.MemberCaseCarePlanAssessmentCareMeasure serverObject) {

            base.MapFromServerObject ((Server.Application.CoreObject)serverObject);


            MemberCaseCarePlanAssessmentId = serverObject.MemberCaseCarePlanAssessmentId;

            CareMeasureDomainId = serverObject.CareMeasureDomainId;

            CareMeasureDomainName = serverObject.CareMeasureDomainName;

            CareMeasureClassId = serverObject.CareMeasureClassId;

            CareMeasureClassName = serverObject.CareMeasureClassName;

            CareMeasureId = serverObject.CareMeasureId;

            TargetValue = serverObject.TargetValue;


            Goals = new List<MemberCaseCarePlanAssessmentCareMeasureGoal> ();

            foreach (Server.Application.MemberCaseCarePlanAssessmentCareMeasureGoal currentServerGoal in serverObject.Goals) {

                MemberCaseCarePlanAssessmentCareMeasureGoal assessmentCareGoal = new MemberCaseCarePlanAssessmentCareMeasureGoal (application, currentServerGoal);

                assessmentCareGoal.MemberCaseCarePlanAssessmentCareMeasure = this;

                Goals.Add (assessmentCareGoal);

            }

            Components = new List<MemberCaseCarePlanAssessmentCareMeasureComponent> ();

            foreach (Server.Application.MemberCaseCarePlanAssessmentCareMeasureComponent currentServerComponent in serverObject.Components) {

                MemberCaseCarePlanAssessmentCareMeasureComponent assessmentCareComponent = new MemberCaseCarePlanAssessmentCareMeasureComponent (application, currentServerComponent);

                assessmentCareComponent.MemberCaseCarePlanAssessmentCareMeasure = this;

                Components.Add (assessmentCareComponent);

            }

            return;

        }

        public virtual void MapToServerObject (Server.Application.MemberCaseCarePlanAssessmentCareMeasure serverObject) {

            base.MapToServerObject ((Server.Application.CoreObject)serverObject);


            serverObject.MemberCaseCarePlanAssessmentId = MemberCaseCarePlanAssessmentId;

            serverObject.CareMeasureDomainId = CareMeasureDomainId;

            serverObject.CareMeasureDomainName = CareMeasureDomainName;

            serverObject.CareMeasureClassId = CareMeasureClassId;

            serverObject.CareMeasureClassName = CareMeasureClassName;

            serverObject.CareMeasureId = CareMeasureId;

            serverObject.TargetValue = TargetValue;



            serverObject.Goals = new Server.Application.MemberCaseCarePlanAssessmentCareMeasureGoal[Goals.Count];

            foreach (MemberCaseCarePlanAssessmentCareMeasureGoal currentAssessmentGoal in Goals) {

                Server.Application.MemberCaseCarePlanAssessmentCareMeasureGoal serverAssessmentGoal = (Server.Application.MemberCaseCarePlanAssessmentCareMeasureGoal)currentAssessmentGoal.ToServerObject ();

                serverObject.Goals[Goals.IndexOf (currentAssessmentGoal)] = serverAssessmentGoal;

            }

            serverObject.Components = new Server.Application.MemberCaseCarePlanAssessmentCareMeasureComponent[Components.Count];

            foreach (MemberCaseCarePlanAssessmentCareMeasureComponent currentAssessmentComponent in Components) {

                Server.Application.MemberCaseCarePlanAssessmentCareMeasureComponent serverAssessmentComponent = (Server.Application.MemberCaseCarePlanAssessmentCareMeasureComponent)currentAssessmentComponent.ToServerObject ();

                serverObject.Components[Components.IndexOf (currentAssessmentComponent)] = serverAssessmentComponent;

            }

            return;

        }

        public override Object ToServerObject () {

            Server.Application.MemberCaseCarePlanAssessmentCareMeasure serverObject = new Server.Application.MemberCaseCarePlanAssessmentCareMeasure ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public MemberCaseCarePlanAssessmentCareMeasure Copy () {

            Server.Application.MemberCaseCarePlanAssessmentCareMeasure serverObject = (Server.Application.MemberCaseCarePlanAssessmentCareMeasure)ToServerObject ();

            MemberCaseCarePlanAssessmentCareMeasure copiedObject = new MemberCaseCarePlanAssessmentCareMeasure (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (MemberCaseCarePlanAssessmentCareMeasure compareObject) {

            Boolean isEqual = base.IsEqual ((CoreObject)compareObject);
            
            // TODO:

            return isEqual;

        }

        #endregion


        #region Public Methods

        public void SetCareMeasure (CareMeasure forCareMeasure) {

            careMeasureDomainId = forCareMeasure.CareMeasureDomainId;

            careMeasureDomainName = forCareMeasure.CareMeasureDomainName;

            careMeasureClassId = forCareMeasure.CareMeasureClassId;

            careMeasureClassName = forCareMeasure.CareMeasureClassName;

            careMeasureId = forCareMeasure.Id;

            Name = forCareMeasure.Name;

            Description = forCareMeasure.Description;


            // CREATE COMPONENTS HERE
            
            foreach (CareMeasureComponent currentComponent in forCareMeasure.Components) {

                MemberCaseCarePlanAssessmentCareMeasureComponent careMeasureComponent = new MemberCaseCarePlanAssessmentCareMeasureComponent (application);

                careMeasureComponent.MemberCaseCarePlanAssessmentCareMeasureId = Id;

                careMeasureComponent.MemberCaseCarePlanAssessmentCareMeasure = this;

                careMeasureComponent.SetCareMeasureComponent (currentComponent);

                components.Add (careMeasureComponent);

            }
            
            return;

        }

        public void AddMemberCaseCarePlanGoal (MemberCaseCarePlanGoal forMemberCaseCarePlanGoal) {

            foreach (MemberCaseCarePlanAssessmentCareMeasureGoal currentGoal in goals) {

                if (currentGoal.MemberCaseCarePlanGoalId == forMemberCaseCarePlanGoal.Id) { return; }

            }

            MemberCaseCarePlanAssessmentCareMeasureGoal goal = new MemberCaseCarePlanAssessmentCareMeasureGoal (application);

            goal.MemberCaseCarePlanAssessmentCareMeasureId = Id;

            goal.MemberCaseCarePlanAssessmentCareMeasure = this;

            goal.SetMemberCaseCarePlanGoal (forMemberCaseCarePlanGoal);

            goals.Add (goal);

            return;

        }

        public List<MemberCaseCarePlanAssessmentCareMeasureComponent> ComponentsByScale (Int64 forCareMeasureScaleId) {

            List<MemberCaseCarePlanAssessmentCareMeasureComponent> scaleComponents =

                (from currentComponent in components

                    where currentComponent.CareMeasureScaleId == forCareMeasureScaleId

                    select currentComponent).ToList ();

            return scaleComponents;

        }

        #endregion 

    }

}
