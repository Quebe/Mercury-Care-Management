using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual {

    [Serializable]
    public class CareLevel : CoreConfigurationObject {

        #region Private Properties

        private List<CareLevelActivity> activities = new List<CareLevelActivity> ();

        #endregion


        #region Public Properties

        public List<CareLevelActivity> Activities { get { return activities; } set { activities = value; } }

        #endregion 

        
        #region Constructors

        public CareLevel (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public CareLevel (Application applicationReference, Mercury.Server.Application.CareLevel serverObject) {

            BaseConstructor (applicationReference, serverObject); 


            activities.Clear ();

            if (serverObject.Activities != null) {

                foreach (Mercury.Server.Application.CareLevelActivity currentActivity in serverObject.Activities) {

                    activities.Add (new CareLevelActivity (applicationReference, currentActivity));

                }

            }

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.CareLevel serverObject) {

            base.MapToServerObject ((Server.Application.CoreConfigurationObject)serverObject);


            serverObject.Activities = new Server.Application.CareLevelActivity[activities.Count];

            Int32 currentIndex = 0;

            while (currentIndex < activities.Count) {

                serverObject.Activities[currentIndex] = (Server.Application.CareLevelActivity) activities[currentIndex].ToServerObject ();

                currentIndex = currentIndex + 1;

            }


            return;

        }

        public override Object ToServerObject () {

            Server.Application.CareLevel serverObject = new Server.Application.CareLevel ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public CareLevel Copy () {

            Server.Application.CareLevel serverObject = (Server.Application.CareLevel)ToServerObject ();

            CareLevel copiedCareLevel = new CareLevel (application, serverObject);

            return copiedCareLevel;

        }

        public Boolean IsEqual (CareLevel compareObject) {

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
