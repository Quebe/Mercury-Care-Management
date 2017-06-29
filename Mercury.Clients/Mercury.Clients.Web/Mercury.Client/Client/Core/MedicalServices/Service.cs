using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.MedicalServices {

    [Serializable]
    public class Service : CoreConfigurationObject {

        #region Private Properties
        
        private Mercury.Server.Application.MedicalServiceType serviceType = Mercury.Server.Application.MedicalServiceType.NotSpecified;

        private Mercury.Server.Application.ServiceClassification serviceClassification = Mercury.Server.Application.ServiceClassification.NotSpecified;

        private DateTime lastPaidDate = new DateTime (1900, 01, 01);


        // SET PROPERTIES

        private Mercury.Server.Application.ServiceSetType setType = Mercury.Server.Application.ServiceSetType.Intersection;

        private Int32 withinDays = 0;

        #endregion


        #region Public Properties

        public Mercury.Server.Application.MedicalServiceType ServiceType { get { return serviceType; } set { serviceType = value; } }

        public Mercury.Server.Application.ServiceClassification ServiceClassification { get { return serviceClassification; } set { serviceClassification = value; } }

        public DateTime LastPaidDate { get { return lastPaidDate; } set { lastPaidDate = value; } }


        public Mercury.Server.Application.ServiceSetType SetType { get { return setType; } set { setType = value; } }

        public Int32 WithinDays { get { return withinDays; } set { withinDays = value; } }

        #endregion


        #region Constructors

        public Service () { return; }

        public Service (Application applicationReference) { base.BaseConstructor (applicationReference);  return; }

        public Service (Application applicationReference, Mercury.Server.Application.Service serverService) {

            base.BaseConstructor (applicationReference, serverService);


            serviceType = serverService.ServiceType;

            serviceClassification = serverService.ServiceClassification;

            lastPaidDate = serverService.LastPaidDate;


            setType = serverService.SetType;

            withinDays = serverService.WithinDays;

            return;

        }

        protected void ServiceConstructor (Application applicationReference, Mercury.Server.Application.ServiceSingleton singleton) {

            base.BaseConstructor (applicationReference, singleton);


            serviceType = singleton.ServiceType;

            serviceClassification = singleton.ServiceClassification;

            lastPaidDate = singleton.LastPaidDate;

            return;

        }

        protected void ServiceConstructor (Application applicationReference, Mercury.Server.Application.ServiceSet serviceSet) {

            base.BaseConstructor (applicationReference, serviceSet);


            serviceType = serviceSet.ServiceType;

            serviceClassification = serviceSet.ServiceClassification;

            lastPaidDate = serviceSet.LastPaidDate;


            setType = serviceSet.SetType;

            withinDays = serviceSet.WithinDays;

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.Service serverService) {

            base.MapToServerObject ((Server.Application.CoreConfigurationObject)serverService);


            serverService.ServiceType = serviceType;

            serverService.ServiceClassification = serviceClassification;

            serverService.LastPaidDate = lastPaidDate;


            serverService.SetType = setType;

            serverService.WithinDays = withinDays;


            return;

        }

        public override Object ToServerObject () {

            Server.Application.Service serverService = new Server.Application.Service ();

            MapToServerObject (serverService);

            return serverService;

        }

        public Service Copy () {

            Server.Application.Service serverService = (Server.Application.Service)ToServerObject ();

            Service copiedService = new Service (application, serverService);

            return copiedService;

        }
        
        public Boolean IsEqual (Service compareService) {

            Boolean isEqual = base.IsEqual (compareService);


            isEqual = isEqual && (this.serviceType == compareService.ServiceType);

            isEqual = isEqual && (this.serviceClassification == compareService.ServiceClassification);
            
            isEqual = isEqual && (this.lastPaidDate == compareService.LastPaidDate);


            isEqual = isEqual && (this.setType == compareService.SetType);

            isEqual = isEqual && (this.withinDays == compareService.WithinDays);

            return isEqual;

        }

        #endregion

    }

}
