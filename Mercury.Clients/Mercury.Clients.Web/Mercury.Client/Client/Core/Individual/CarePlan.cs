using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual {

    [Serializable]
    public class CarePlan : CoreConfigurationObject {

        #region Private Properties

        private List<CarePlanGoal> goals = new List<CarePlanGoal> ();


        #endregion


        #region Public Properties

        public List<CarePlanGoal> Goals { get { return goals; } set { goals = value; } }


        #endregion


        #region Constructors

        protected CarePlan () { /* DO NOTHING, FOR INHERITANCE */ }

        public CarePlan (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public CarePlan (Application applicationReference, Mercury.Server.Application.CarePlan serverObject) {

            BaseConstructor (applicationReference, serverObject);


            goals.Clear ();

            if (serverObject.Goals != null) {

                foreach (Mercury.Server.Application.CarePlanGoal currentGoal in serverObject.Goals) {
                    
                    goals.Add (new CarePlanGoal (applicationReference, currentGoal));

                }

            }


            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.CarePlan serverObject) {

            base.MapToServerObject ((Server.Application.CoreConfigurationObject)serverObject);

            Int32 currentIndex = 0;


            serverObject.Goals = new Server.Application.CarePlanGoal[goals.Count];

            currentIndex = 0;

            while (currentIndex < goals.Count) {

                serverObject.Goals[currentIndex] = (Server.Application.CarePlanGoal)goals[currentIndex].ToServerObject ();

                currentIndex = currentIndex + 1;

            }


            return;

        }

        public override Object ToServerObject () {

            Server.Application.CarePlan serverObject = new Server.Application.CarePlan ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public CarePlan Copy () {

            Server.Application.CarePlan serverObject = (Server.Application.CarePlan)ToServerObject ();

            CarePlan copiedCarePlan = new CarePlan (application, serverObject);

            return copiedCarePlan;

        }

        public Boolean IsEqual (CarePlan compareObject) {

            Boolean isEqual = base.IsEqual ((CoreConfigurationObject)compareObject);


            isEqual &= (goals.Count == compareObject.Goals.Count);

            if (isEqual) {

                for (Int32 currentIndex = 0; currentIndex < goals.Count; currentIndex++) {

                    isEqual &= goals[currentIndex].IsEqual (compareObject.Goals[currentIndex]);

                }

            }


            return isEqual;

        }

        #endregion


        #region Public Methods

        public CarePlanGoal CarePlanGoal (String forName) {

            CarePlanGoal carePlanGoal = null;


            foreach (CarePlanGoal currentCarePlanGoal in goals) {

                if (currentCarePlanGoal.Name == forName) {

                    carePlanGoal = currentCarePlanGoal;

                    break;

                }

            }


            return carePlanGoal;

        }

        public CarePlanGoal CarePlanGoal (Int64 forId) {

            CarePlanGoal carePlanGoal = null;


            foreach (CarePlanGoal currentCarePlanGoal in goals) {

                if (currentCarePlanGoal.Id == forId) {

                    carePlanGoal = currentCarePlanGoal;

                    break;

                }

            }


            return carePlanGoal;

        }
        
        #endregion 

    }

}
