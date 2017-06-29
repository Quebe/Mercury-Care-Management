using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.MedicalServices.Definitions {

    [Serializable]
    public class ServiceSetDefinition : CoreObject {

        #region Private Properties
        
        private Int64 serviceId;

        private Int64 definitionServiceId;

        private Boolean enabled = true;

        #endregion


        #region Public Properties

        public Int64 ServiceId { get { return serviceId; } set { serviceId = value; } }

        public Int64 DefinitionServiceId { get { return definitionServiceId; } set { definitionServiceId = value; } }

        public Boolean Enabled { get { return enabled; } set { enabled = value; } }

        #endregion


        #region Constructors 

        public ServiceSetDefinition () { return; }

        public ServiceSetDefinition (Application applicationReference, Mercury.Server.Application.ServiceSetDefinition serverDefinition) {

            base.BaseConstructor (applicationReference, serverDefinition);


            serviceId = serverDefinition.ServiceId;

            definitionServiceId = serverDefinition.DefinitionServiceId;

            enabled = serverDefinition.Enabled;

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.ServiceSetDefinition serverObject) {

            base.MapToServerObject ((Server.Application.CoreObject)serverObject);


            serverObject.ServiceId = serviceId;

            serverObject.DefinitionServiceId = definitionServiceId;

            serverObject.Enabled = enabled;
            
            return;

        }

        public override Object ToServerObject () {

            Server.Application.ServiceSetDefinition serverObject = new Server.Application.ServiceSetDefinition ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public ServiceSetDefinition Copy () {

            Server.Application.ServiceSetDefinition serverObject = (Server.Application.ServiceSetDefinition)ToServerObject ();

            ServiceSetDefinition copiedObject = new ServiceSetDefinition (application, serverObject);

            return copiedObject;

        }

        #endregion


        #region Public Methods

        public Boolean IsEqual (ServiceSetDefinition compareDefinition) {

            Boolean isEqual = true;

            if (this.serviceId != compareDefinition.ServiceId) { isEqual = false; }

            if (this.definitionServiceId != compareDefinition.DefinitionServiceId) { isEqual = false; }

            if (this.enabled != compareDefinition.Enabled) { isEqual = false; }

            return isEqual;

        }

        #endregion

    }

}
