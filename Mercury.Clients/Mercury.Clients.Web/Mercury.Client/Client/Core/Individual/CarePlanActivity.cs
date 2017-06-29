using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual {

    [Serializable]
    public class CarePlanActivity  : Core.Activity.Activity {

        #region Private Properties

        private Int64 carePlanId;

        private Server.Application.CarePlanActivityType carePlanActivityType = Server.Application.CarePlanActivityType.Intervention;

        private String clinicalNarrative;

        private String commonNarrative;

        #endregion


        #region Public Properties

        public Int64 CarePlanId { get { return carePlanId; } set { carePlanId = value; } }

        public Server.Application.CarePlanActivityType CarePlanActivityType { get { return carePlanActivityType; } set { carePlanActivityType = value; } }

        public String ClinicalNarrative { get { return clinicalNarrative; } set { clinicalNarrative = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Description); } }

        public String CommonNarrative { get { return commonNarrative; } set { commonNarrative = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Description); } }

        #endregion
        

        #region Constructors

        protected CarePlanActivity () { /* DO NOTHING, FOR INHERITANCE */ }

        public CarePlanActivity (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public CarePlanActivity (Application applicationReference, Mercury.Server.Application.CarePlanActivity serverObject) {

            BaseConstructor (applicationReference, (Mercury.Server.Application.Activity) serverObject);


            carePlanId = serverObject.CarePlanId;

            carePlanActivityType = serverObject.CarePlanActivityType;

            clinicalNarrative = serverObject.ClinicalNarrative;

            commonNarrative = serverObject.CommonNarrative;

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.CarePlanActivity serverObject) {

            base.MapToServerObject ((Server.Application.Activity)serverObject);


            serverObject.CarePlanId = carePlanId;

            serverObject.CarePlanActivityType = carePlanActivityType;

            serverObject.ClinicalNarrative = clinicalNarrative;

            serverObject.CommonNarrative = commonNarrative;

            return;

        }

        public override Object ToServerObject () {

            Server.Application.CarePlanActivity serverObject = new Server.Application.CarePlanActivity ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public new CarePlanActivity Copy () {

            Server.Application.CarePlanActivity serverObject = (Server.Application.CarePlanActivity)ToServerObject ();

            CarePlanActivity copiedObject = new CarePlanActivity (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (CarePlanActivity compareObject) {

            Boolean isEqual = base.IsEqual ((CoreObject)compareObject);


            isEqual &= (carePlanId == compareObject.carePlanId);

            isEqual &= (carePlanActivityType == compareObject.carePlanActivityType);

            isEqual &= (clinicalNarrative == compareObject.ClinicalNarrative);

            isEqual &= (commonNarrative == compareObject.CommonNarrative);


            return isEqual;

        }

        #endregion 
    }

}
