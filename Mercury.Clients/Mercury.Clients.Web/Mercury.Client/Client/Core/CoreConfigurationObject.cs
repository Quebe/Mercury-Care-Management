using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core {

    [Serializable]
    public class CoreConfigurationObject : CoreExtensibleObject {

        #region Private Properties

        private Boolean enabled = true;

        private Boolean visible = true;

        #endregion


        #region Public Properties

        virtual public Boolean Enabled { get { return enabled; } set { enabled = value; } }

        virtual public Boolean Visible { get { return visible; } set { visible = value; } }

        #endregion


        #region Constructors

        protected CoreConfigurationObject () { return; }

        public CoreConfigurationObject (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public CoreConfigurationObject (Application applicationReference, Server.Application.CoreConfigurationObject forCoreConfigurationObject) {

            BaseConstructor (applicationReference, forCoreConfigurationObject);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.CoreConfigurationObject forCoreConfigurationObject) {

            base.BaseConstructor (applicationReference, forCoreConfigurationObject);


            enabled = forCoreConfigurationObject.Enabled;

            visible = forCoreConfigurationObject.Visible;

            return;

        }

        #endregion  

        
        #region Public Methods

        public void MapToServerObject (Server.Application.CoreConfigurationObject coreConfigurationObject) {

            base.MapToServerObject ((Server.Application.CoreExtensibleObject) coreConfigurationObject);


            coreConfigurationObject.Enabled = enabled;

            coreConfigurationObject.Visible = visible;
            

            return;

        }

        public override Object ToServerObject () {

            Server.Application.CoreConfigurationObject coreConfigurationObject = new Server.Application.CoreConfigurationObject ();

            MapToServerObject (coreConfigurationObject);

            return coreConfigurationObject;

        }

        public Boolean IsEqual (CoreConfigurationObject compareCoreConfigurationObject) {

            Boolean isEqual = base.IsEqual ((CoreExtensibleObject)compareCoreConfigurationObject);


            isEqual &= (enabled == compareCoreConfigurationObject.Enabled);

            isEqual &= (visible == compareCoreConfigurationObject.Visible);


            return isEqual;

        }

        #endregion 

    }

}
