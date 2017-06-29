using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual {

    [Serializable]
    public class CareMeasure : CoreConfigurationObject {

        #region Private Properties

        private Int64 careMeasureDomainId = 0;

        private String careMeasureDomainName = String.Empty;

        private Int64 careMeasureClassId = 0;

        private String careMeasureClassName = String.Empty;

        private List<CareMeasureComponent> components = new List<CareMeasureComponent> ();

        #endregion


        #region Public Properties - Encapsulated

        public Int64 CareMeasureDomainId { get { return careMeasureDomainId; } set { careMeasureDomainId = value; } }

        public String CareMeasureDomainName { get { return careMeasureDomainName; } set { careMeasureDomainName = value; } }


        public Int64 CareMeasureClassId { get { return careMeasureClassId; } set { careMeasureClassId = value; } }

        public String CareMeasureClassName { get { return careMeasureClassName; } set { careMeasureClassName = value; } }

        public List<CareMeasureComponent> Components { get { return components; } set { components = value; } }

        #endregion


        #region Public Properties

        public String Classification {

            get {

                String classification = careMeasureDomainName;

                if (!String.IsNullOrWhiteSpace (careMeasureClassName)) { classification += " - " + careMeasureClassName; }

                return classification;

            }

        }

        #endregion


        #region Constructors

        public CareMeasure (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public CareMeasure (Application applicationReference, Mercury.Server.Application.CareMeasure serverObject) {

            BaseConstructor (applicationReference, serverObject);


            careMeasureDomainId = serverObject.CareMeasureDomainId;

            careMeasureDomainName = serverObject.CareMeasureDomainName;

            careMeasureClassId = serverObject.CareMeasureClassId;

            careMeasureClassName = serverObject.CareMeasureClassName;


            components = new List<CareMeasureComponent> ();

            foreach (Server.Application.CareMeasureComponent currentServerComponent in serverObject.Components) {

                CareMeasureComponent component = new CareMeasureComponent (application, currentServerComponent);

                components.Add (component);

            }

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.CareMeasure serverObject) {

            base.MapToServerObject ((Server.Application.CoreConfigurationObject)serverObject);

            serverObject.CareMeasureDomainId = careMeasureDomainId;

            serverObject.CareMeasureDomainName = careMeasureDomainName;

            serverObject.CareMeasureClassId = careMeasureClassId;

            serverObject.CareMeasureClassName = careMeasureClassName;


            serverObject.Components = new Server.Application.CareMeasureComponent[components.Count];

            Int32 componentIndex = 0;

            foreach (CareMeasureComponent currentComponent in components) {

                Server.Application.CareMeasureComponent serverComponent = (Server.Application.CareMeasureComponent) currentComponent.ToServerObject ();

                serverObject.Components[componentIndex] = serverComponent;

                componentIndex = componentIndex + 1;

            }

            return;

        }

        public override Object ToServerObject () {

            Server.Application.CareMeasure serverObject = new Server.Application.CareMeasure ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public CareMeasure Copy () {

            Server.Application.CareMeasure serverObject = (Server.Application.CareMeasure)ToServerObject ();

            CareMeasure copiedCareMeasure = new CareMeasure (application, serverObject);

            return copiedCareMeasure;

        }

        public Boolean IsEqual (CareMeasure compareObject) {

            Boolean isEqual = base.IsEqual ((CoreConfigurationObject)compareObject);


            isEqual &= (careMeasureDomainId == compareObject.CareMeasureDomainId);

            isEqual &= (careMeasureDomainName == compareObject.CareMeasureDomainName);

            isEqual &= (careMeasureClassId == compareObject.CareMeasureClassId);

            isEqual &= (careMeasureClassName == compareObject.CareMeasureClassName);


            isEqual &= (components.Count == compareObject.Components.Count);

            if (isEqual) {

                for (Int32 currentComponentIndex = 0; currentComponentIndex < components.Count; currentComponentIndex++) {

                    isEqual &= (components[currentComponentIndex].IsEqual (compareObject.Components[currentComponentIndex]));

                    if (!isEqual) { break; }

                }

            }


            return isEqual;

        }

        #endregion

    }

}
