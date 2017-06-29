using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual {

    [Serializable]
    public class CareLevelActivity : Core.Activity.Activity {

        #region Private Properties

        private Int64 careLevelId;

        #endregion


        #region Public Properties

        public Int64 CareLevelId { get { return careLevelId; } set { careLevelId = value; } }

        #endregion
        

        #region Constructors

        public CareLevelActivity (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public CareLevelActivity (Application applicationReference, Mercury.Server.Application.CareLevelActivity serverObject) {

            BaseConstructor (applicationReference, (Mercury.Server.Application.Activity) serverObject);

            careLevelId = serverObject.CareLevelId;

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.CareLevelActivity serverObject) {

            base.MapToServerObject ((Server.Application.Activity)serverObject);


            serverObject.CareLevelId = careLevelId;


            return;

        }

        public override Object ToServerObject () {

            Server.Application.CareLevelActivity serverObject = new Server.Application.CareLevelActivity ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public new CareLevelActivity Copy () {

            Server.Application.CareLevelActivity serverObject = (Server.Application.CareLevelActivity)ToServerObject ();

            CareLevelActivity copiedObject = new CareLevelActivity (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (CareLevelActivity compareObject) {

            Boolean isEqual = base.IsEqual ((CoreObject)compareObject);


            isEqual &= (careLevelId == compareObject.careLevelId);


            return isEqual;

        }

        #endregion 

    }

}
