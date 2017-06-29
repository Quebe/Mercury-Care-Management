using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual {

    [Serializable]
    public class CareOutcome : CoreConfigurationObject {

        #region Constructors

        public CareOutcome (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public CareOutcome (Application applicationReference, Server.Application.CareOutcome serverCareOutcome) {

            BaseConstructor (applicationReference, serverCareOutcome);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.CareOutcome serverCareOutcome) {

            base.BaseConstructor (applicationReference, serverCareOutcome);

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.CareOutcome serverCareOutcome) {

            base.MapToServerObject ((Server.Application.CoreConfigurationObject)serverCareOutcome);


            return;

        }

        public override Object ToServerObject () {

            Server.Application.CareOutcome serverCareOutcome = new Server.Application.CareOutcome ();

            MapToServerObject (serverCareOutcome);

            return serverCareOutcome;

        }

        public CareOutcome Copy () {

            Server.Application.CareOutcome serverCareOutcome = (Server.Application.CareOutcome)ToServerObject ();

            CareOutcome copiedCareOutcome = new CareOutcome (application, serverCareOutcome);

            return copiedCareOutcome;

        }

        public Boolean IsEqual (CareOutcome compareCareOutcome) {

            Boolean isEqual = base.IsEqual ((CoreConfigurationObject)compareCareOutcome);


            return isEqual;

        }

        #endregion

    }

}
