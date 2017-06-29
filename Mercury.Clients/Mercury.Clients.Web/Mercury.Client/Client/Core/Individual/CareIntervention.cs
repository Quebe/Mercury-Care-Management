using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual {

    [Serializable]
    public class CareIntervention : CoreConfigurationObject {
        
        #region Private Properties

        private List<CareInterventionActivity> activities = new List<CareInterventionActivity> ();

        #endregion


        #region Public Properties

        public List<CareInterventionActivity> Activities { get { return activities; } set { activities = value; } }

        #endregion 

        
        #region Constructors

        protected CareIntervention () { /* DO NOTHING, FOR INHERITANCE */ }

        public CareIntervention (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public CareIntervention (Application applicationReference, Mercury.Server.Application.CareIntervention serverObject) {

            BaseConstructor (applicationReference, serverObject); 


            activities.Clear ();

            if (serverObject.Activities != null) {

                foreach (Mercury.Server.Application.CareInterventionActivity currentActivity in serverObject.Activities) {

                    activities.Add (new CareInterventionActivity (applicationReference, currentActivity));

                }

            }

            return;

        }

        #endregion


        #region Public Methods

        public void MapFromServerObject (Mercury.Server.Application.CareIntervention serverObject) {


            return;

        }

        public virtual void MapToServerObject (Server.Application.CareIntervention serverObject) {

            base.MapToServerObject ((Server.Application.CoreConfigurationObject)serverObject);


            serverObject.Activities = new Server.Application.CareInterventionActivity[activities.Count];

            Int32 currentIndex = 0;

            while (currentIndex < activities.Count) {

                serverObject.Activities[currentIndex] = (Server.Application.CareInterventionActivity) activities[currentIndex].ToServerObject ();

                currentIndex = currentIndex + 1;

            }


            return;

        }

        public override Object ToServerObject () {

            Server.Application.CareIntervention serverObject = new Server.Application.CareIntervention ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public CareIntervention Copy () {

            Server.Application.CareIntervention serverObject = (Server.Application.CareIntervention)ToServerObject ();

            CareIntervention copiedCareIntervention = new CareIntervention (application, serverObject);

            return copiedCareIntervention;

        }

        public Boolean IsEqual (CareIntervention compareObject) {

            Boolean isEqual = base.IsEqual ((CoreConfigurationObject)compareObject);


            isEqual &= (activities.Count == compareObject.Activities.Count);

            if (isEqual) {

                for (Int32 currentIndex = 0; currentIndex < activities.Count; currentIndex++) {

                    isEqual &= activities[currentIndex].IsEqual (compareObject.Activities[currentIndex]);

                }

            }


            return isEqual;

        }

        #endregion 
    }

}
