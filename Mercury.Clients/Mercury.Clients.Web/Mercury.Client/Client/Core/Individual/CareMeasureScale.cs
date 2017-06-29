using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual {

    [Serializable]
    public class CareMeasureScale : CoreConfigurationObject {
        
        #region Private Properties

        private String scaleLabel1 = String.Empty;

        private String scaleLabel2 = String.Empty;

        private String scaleLabel3 = String.Empty;

        private String scaleLabel4 = String.Empty;

        private String scaleLabel5 = String.Empty;
    
        #endregion


        #region Public Properties

        public String ScaleLabel1 { get { return scaleLabel1; } set { scaleLabel1 = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public String ScaleLabel2 { get { return scaleLabel2; } set { scaleLabel2 = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public String ScaleLabel3 { get { return scaleLabel3; } set { scaleLabel3 = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public String ScaleLabel4 { get { return scaleLabel4; } set { scaleLabel4 = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public String ScaleLabel5 { get { return scaleLabel5; } set { scaleLabel5 = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        #endregion 

        
        #region Constructors

        public CareMeasureScale (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public CareMeasureScale (Application applicationReference, Mercury.Server.Application.CareMeasureScale serverObject) {

            BaseConstructor (applicationReference, serverObject);


            ScaleLabel1 = serverObject.ScaleLabel1;

            ScaleLabel2 = serverObject.ScaleLabel2;

            ScaleLabel3 = serverObject.ScaleLabel3;

            ScaleLabel4 = serverObject.ScaleLabel4;

            ScaleLabel5 = serverObject.ScaleLabel5;


            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.CareMeasureScale serverObject) {

            base.MapToServerObject ((Server.Application.CoreConfigurationObject)serverObject);


            serverObject.ScaleLabel1 = ScaleLabel1;

            serverObject.ScaleLabel2 = ScaleLabel2;

            serverObject.ScaleLabel3 = ScaleLabel3;

            serverObject.ScaleLabel4 = ScaleLabel4;

            serverObject.ScaleLabel5 = ScaleLabel5;


            return;

        }

        public override Object ToServerObject () {

            Server.Application.CareMeasureScale serverObject = new Server.Application.CareMeasureScale ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public CareMeasureScale Copy () {

            Server.Application.CareMeasureScale serverObject = (Server.Application.CareMeasureScale)ToServerObject ();

            CareMeasureScale copiedCareMeasureScale = new CareMeasureScale (application, serverObject);

            return copiedCareMeasureScale;

        }

        public Boolean IsEqual (CareMeasureScale compareObject) {

            Boolean isEqual = base.IsEqual ((CoreConfigurationObject)compareObject);


            isEqual &= (ScaleLabel1 == compareObject.ScaleLabel1);

            isEqual &= (ScaleLabel2 == compareObject.ScaleLabel2);

            isEqual &= (ScaleLabel3 == compareObject.ScaleLabel3);

            isEqual &= (ScaleLabel4 == compareObject.ScaleLabel4);

            isEqual &= (ScaleLabel5 == compareObject.ScaleLabel5);


            return isEqual;

        }

        #endregion 

    }

}
