using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual {

    [Serializable]
    public class CareMeasureComponent : CoreConfigurationObject {

        #region Private Properties

        private Int64 careMeasureId = 0;

        private Int64 careMeasureScaleId = 0;

        private String tag = String.Empty;

        #endregion

        
        #region Public Properties - Encapsulated

        public override String Name { get { return base.Name; } set { name = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Description99); } }

        public Int64 CareMeasureId { get { return careMeasureId; } set { careMeasureId = value; } }

        public Int64 CareMeasureScaleId { get { return careMeasureScaleId; } set { careMeasureScaleId = value; } }

        public String Tag { get { return tag; } set { tag = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.UniqueId); } }

        #endregion 
        

        #region Public Properties 

        public CareMeasureScale CareMeasureScale { get { return application.CareMeasureScaleGet (careMeasureScaleId, true); } }

        public String CareMeasureScaleName { get { return ((CareMeasureScale != null) ? CareMeasureScale.Name : String.Empty); } }
            
        #endregion 

        
        #region Constructors

        public CareMeasureComponent (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public CareMeasureComponent (Application applicationReference, Mercury.Server.Application.CareMeasureComponent serverObject) {

            BaseConstructor (applicationReference, serverObject);


            CareMeasureId = serverObject.CareMeasureId;

            CareMeasureScaleId = serverObject.CareMeasureScaleId;

            Tag = serverObject.Tag;


            return;

        }

        #endregion

        
        #region Public Methods

        public virtual void MapToServerObject (Server.Application.CareMeasureComponent serverObject) {

            base.MapToServerObject ((Server.Application.CoreConfigurationObject)serverObject);


            serverObject.CareMeasureId = CareMeasureId;

            serverObject.CareMeasureScaleId = CareMeasureScaleId;

            serverObject.Tag = Tag;


            return;

        }

        public override Object ToServerObject () {

            Server.Application.CareMeasureComponent serverObject = new Server.Application.CareMeasureComponent ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public CareMeasureComponent Copy () {

            Server.Application.CareMeasureComponent serverObject = (Server.Application.CareMeasureComponent)ToServerObject ();

            CareMeasureComponent copiedObject = new CareMeasureComponent (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (CareMeasureComponent compareObject) {

            Boolean isEqual = base.IsEqual ((CoreConfigurationObject)compareObject);


            isEqual &= (CareMeasureId == compareObject.CareMeasureId);

            isEqual &= (CareMeasureScaleId == compareObject.CareMeasureScaleId);

            isEqual &= (Tag == compareObject.Tag);


            return isEqual;

        }

        #endregion 

    }

}
