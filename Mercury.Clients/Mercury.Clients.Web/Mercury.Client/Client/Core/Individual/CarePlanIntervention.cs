using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual {

    [Serializable]
    public class CarePlanIntervention : CoreConfigurationObject {

        #region Private Properties

        private Int64 carePlanGoalId = 0;

        private Int64 careInterventionId = 0;

        private Server.Application.CarePlanItemInclusion inclusion = Server.Application.CarePlanItemInclusion.Required;


        private CarePlanGoal carePlanGoal = null;

        #endregion


        #region Public Properties - Encapsulated

        public Int64 CarePlanGoalId { get { return carePlanGoalId; } set { carePlanGoalId = value; } }

        public Int64 CareInterventionId { get { return careInterventionId; } set { careInterventionId = value; } }

        public Server.Application.CarePlanItemInclusion Inclusion { get { return inclusion; } set { inclusion = value; } }


        public CarePlanGoal CarePlanGoal { get { return carePlanGoal; } set { carePlanGoal = value; } }

        #endregion


        #region Public Properties
        
        public CareIntervention CareIntervention { get { return application.CareInterventionGet (careInterventionId, true); } }

        public String CareInterventionName { get { return ((CareIntervention != null) ? CareIntervention.Name : String.Empty); } }

        public String CareInterventionDescription { get { return ((CareIntervention != null) ? CareIntervention.Description : String.Empty); } }

        public String CarePlanGoalName { get { return ((CarePlanGoal != null) ? CarePlanGoal.Name : String.Empty); } }

        #endregion


        #region Constructors

        protected CarePlanIntervention () { /* DO NOTHING, FOR INHERITANCE */ }

        public CarePlanIntervention (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public CarePlanIntervention (Application applicationReference, Mercury.Server.Application.CarePlanIntervention serverObject) {

            BaseConstructor (applicationReference, serverObject);


            CarePlanGoalId = serverObject.CarePlanGoalId;

            CareInterventionId = serverObject.CareInterventionId;

            Inclusion = serverObject.Inclusion;


            return;

        }

        #endregion


        #region Public Methods

        public void MapFromServerObject (Mercury.Server.Application.CarePlanIntervention serverObject) {

            carePlanGoalId = serverObject.CarePlanGoalId;
            
            careInterventionId = serverObject.CareInterventionId;

            inclusion = serverObject.Inclusion;

            return;

        }

        public virtual void MapToServerObject (Server.Application.CarePlanIntervention serverObject) {

            base.MapToServerObject ((Server.Application.CoreConfigurationObject)serverObject);


            serverObject.CarePlanGoalId = carePlanGoalId;
    
            serverObject.CareInterventionId = careInterventionId;

            serverObject.Inclusion = inclusion;

            return;

        }

        public override Object ToServerObject () {

            Server.Application.CarePlanIntervention serverObject = new Server.Application.CarePlanIntervention ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public CarePlanIntervention Copy () {

            Server.Application.CarePlanIntervention serverObject = (Server.Application.CarePlanIntervention)ToServerObject ();

            CarePlanIntervention copiedObject = new CarePlanIntervention (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (CarePlanIntervention compareObject) {

            Boolean isEqual = base.IsEqual ((CoreConfigurationObject)compareObject);


            isEqual &= (compareObject.CarePlanGoalId == carePlanGoalId);
            
            isEqual &= (compareObject.CareInterventionId == CareInterventionId);

            isEqual &= (compareObject.Inclusion == Inclusion);


            return isEqual;

        }

        #endregion

    }

}

