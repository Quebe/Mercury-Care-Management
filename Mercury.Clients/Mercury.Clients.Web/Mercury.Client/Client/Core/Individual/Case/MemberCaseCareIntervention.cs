using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual.Case {

    [Serializable]
    public class MemberCaseCareIntervention : CareIntervention {

        #region Private Properties

        private Int64 memberCaseId = 0;

        private Int64 careInterventionId = 0;

        private Server.Application.CaseItemStatus status = Server.Application.CaseItemStatus.UnderDevelopment;

        private List<MemberCaseCareInterventionActivity> activities = new List<MemberCaseCareInterventionActivity> ();


        private MemberCase memberCase = null; // PARENT REFERENCE, LOCAL ONLY

        #endregion


        #region Public Properties - Encapsulated

        public Int64 MemberCaseCareId { get { return memberCaseId; } set { memberCaseId = value; } }

        public Int64 CareInterventionId { get { return careInterventionId; } set { careInterventionId = value; } }

        public Server.Application.CaseItemStatus Status { get { return status; } set { status = value; } }

        public new List<MemberCaseCareInterventionActivity> Activities { get { return activities; } set { activities = value; } }

        public MemberCase MemberCase { get { return memberCase; } set { memberCase = value; } }

        public override Application Application {

            get { return base.Application; }

            set {

                base.Application = value;

                foreach (MemberCaseCareInterventionActivity currentActivity in activities) {

                    currentActivity.Application = value;

                    currentActivity.MemberCaseCareIntervention = this;

                }

            }

        }

        #endregion


        #region Public Properties

        public CareIntervention CareIntervention { get { return application.CareInterventionGet (careInterventionId, true); } }

        public String StatusDescription { get { return Server.CommonFunctions.EnumerationToString (status); } }

        public List<MemberCaseCarePlanGoalIntervention> GoalInterventions {

            get {

                List<MemberCaseCarePlanGoalIntervention> goalInterventions =

                    (from currentCarePlan in MemberCase.CarePlans

                     from currentCarePlanGoal in currentCarePlan.Goals

                     from currentCarePlanGoalIntervention in currentCarePlanGoal.Interventions

                     where currentCarePlanGoalIntervention.MemberCaseCareInterventionId == Id

                     select currentCarePlanGoalIntervention).ToList ();


                return goalInterventions;

            }

        }

        #endregion


        #region Constructors

        public MemberCaseCareIntervention (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public MemberCaseCareIntervention (Application applicationReference, Mercury.Server.Application.MemberCaseCareIntervention serverObject) {

            BaseConstructor (applicationReference, serverObject);

            MapFromServerObject (serverObject);

            return;

        }

        #endregion


        #region Public Methods

        public void MapFromServerObject (Mercury.Server.Application.MemberCaseCareIntervention serverObject) {

            base.MapFromServerObject ((Mercury.Server.Application.CareIntervention)serverObject);


            memberCaseId = serverObject.MemberCaseId;

            careInterventionId = serverObject.CareInterventionId;

            status = serverObject.Status;


            activities = new List<MemberCaseCareInterventionActivity> ();

            foreach (Server.Application.MemberCaseCareInterventionActivity currentActivity in serverObject.CareInterventionActivities) {

                MemberCaseCareInterventionActivity activity = new MemberCaseCareInterventionActivity (application, currentActivity);

                activity.MemberCaseCareIntervention = this;

                activities.Add (activity);

            }



            return;

        }

        public void MapToServerObject (Server.Application.MemberCaseCareIntervention serverObject) {

            base.MapToServerObject ((Server.Application.MemberCaseCareIntervention)serverObject);


            serverObject.MemberCaseId = memberCaseId;

            serverObject.CareInterventionId = careInterventionId;

            serverObject.Status = status;


            return;

        }

        public override Object ToServerObject () {

            Server.Application.MemberCaseCareIntervention serverObject = new Server.Application.MemberCaseCareIntervention ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public new MemberCaseCareIntervention Copy () {

            Server.Application.MemberCaseCareIntervention serverObject = (Server.Application.MemberCaseCareIntervention)ToServerObject ();

            MemberCaseCareIntervention copiedCareIntervention = new MemberCaseCareIntervention (application, serverObject);

            return copiedCareIntervention;

        }

        public Boolean IsEqual (MemberCaseCareIntervention compareObject) {

            Boolean isEqual = base.IsEqual ((CareIntervention)compareObject);


            isEqual &= (activities.Count == compareObject.Activities.Count);

            if (isEqual) {

                for (Int32 currentIndex = 0; currentIndex < activities.Count; currentIndex++) {

                    isEqual &= activities[currentIndex].IsEqual (compareObject.Activities[currentIndex]);

                }

            }


            return isEqual;

        }

        #endregion


        #region Public Methods


        #endregion 

    }

}
