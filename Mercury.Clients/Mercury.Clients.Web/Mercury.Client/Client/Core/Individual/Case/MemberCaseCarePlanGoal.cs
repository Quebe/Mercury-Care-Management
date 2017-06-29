using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual.Case {

    [Serializable]
    public class MemberCaseCarePlanGoal : CarePlanGoal {

        #region Private Properties

        private Int64 memberCaseCarePlanId = 0;

        private MemberCaseCarePlan memberCaseCarePlan = null; // PARENT REFERENCE, LOCAL ONLY

        private Int64 carePlanGoalId = 0;

        private Server.Application.CaseItemStatus status = Server.Application.CaseItemStatus.UnderDevelopment;

        private Decimal initialValue = 0;

        private Decimal lastValue = 0;

        private Decimal targetValue = 0;

        private List<MemberCaseCarePlanGoalIntervention> interventions = new List<MemberCaseCarePlanGoalIntervention> (); // OVERRIDE BASE INTERVENTIONS COLLECTION 

        #endregion


        #region Public Properties

        public Int64 MemberCaseCarePlanId { get { return memberCaseCarePlanId; } set { memberCaseCarePlanId = value; } }

        public MemberCaseCarePlan MemberCaseCarePlan { get { return memberCaseCarePlan; } set { memberCaseCarePlan = value; } }

        public Int64 CarePlanGoalId { get { return carePlanGoalId; } set { carePlanGoalId = value; } }

        public Server.Application.CaseItemStatus Status { get { return status; } set { status = value; } }

        public Decimal InitialValue { get { return initialValue; } }

        public Decimal LastValue { get { return lastValue; } }

        public Decimal TargetValue { get { return targetValue; } }

        public new List<MemberCaseCarePlanGoalIntervention> Interventions { get { return interventions; } set { interventions = value; } } // OVERRIDE BASE INTERVENTIONS COLLECTION 

        #endregion
                

        #region Constructors
        
        public MemberCaseCarePlanGoal (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public MemberCaseCarePlanGoal (Application applicationReference, Mercury.Server.Application.MemberCaseCarePlanGoal serverObject) {

            BaseConstructor (applicationReference, serverObject);
            
            MapFromServerObject (serverObject);

            return;

        }

        #endregion


        #region Public Methods

        public void MapFromServerObject (Mercury.Server.Application.MemberCaseCarePlanGoal serverObject) {

            base.MapFromServerObject ((Mercury.Server.Application.CarePlanGoal)serverObject);


            memberCaseCarePlanId = serverObject.MemberCaseCarePlanId;

            carePlanGoalId = serverObject.CarePlanGoalId;

            status = serverObject.Status;

            initialValue = serverObject.InitialValue;

            lastValue = serverObject.LastValue;

            targetValue = serverObject.TargetValue;

            interventions.Clear ();

            foreach (Server.Application.MemberCaseCarePlanGoalIntervention currentServerCareIntervention in serverObject.GoalInterventions) {

                MemberCaseCarePlanGoalIntervention intervention = new MemberCaseCarePlanGoalIntervention (Application, currentServerCareIntervention);

                intervention.MemberCaseCarePlanGoal = this;

                interventions.Add (intervention);

            }


            return;

        }

        public void MapToServerObject (Server.Application.MemberCaseCarePlanGoal serverObject) {

            base.MapToServerObject ((Server.Application.CarePlanGoal)serverObject);


            serverObject.MemberCaseCarePlanId = memberCaseCarePlanId;

            serverObject.CarePlanGoalId = carePlanGoalId;

            serverObject.Status = status;

            serverObject.InitialValue = initialValue;

            serverObject.LastValue = lastValue;

            serverObject.TargetValue = targetValue;

            return;

        }

        public override Object ToServerObject () {

            Server.Application.MemberCaseCarePlanGoal serverObject = new Server.Application.MemberCaseCarePlanGoal ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public new MemberCaseCarePlanGoal Copy () {

            Server.Application.MemberCaseCarePlanGoal serverObject = (Server.Application.MemberCaseCarePlanGoal)ToServerObject ();

            MemberCaseCarePlanGoal copiedObject = new MemberCaseCarePlanGoal (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (MemberCaseCarePlanGoal compareObject) {

            Boolean isEqual = base.IsEqual ((CarePlanGoal)compareObject);

            
            isEqual &= (compareObject.CarePlanGoalId == CarePlanGoalId);

            isEqual &= (compareObject.MemberCaseCarePlanId == MemberCaseCarePlanId);

            isEqual &= (compareObject.Status == Status);


            return isEqual;

        }

        #endregion

    }

}
