using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Work {

    [Serializable]
    public class WorkOutcome : CoreConfigurationObject {
        
        #region Constructors

        public WorkOutcome (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public WorkOutcome (Application applicationReference, Server.Application.WorkOutcome serverWorkOutcome) {

            BaseConstructor (applicationReference, serverWorkOutcome);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.WorkOutcome serverWorkOutcome) {

            base.BaseConstructor (applicationReference, serverWorkOutcome);
            
            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.WorkOutcome serverWorkOutcome) {

            base.MapToServerObject ((Server.Application.CoreConfigurationObject)serverWorkOutcome);


            return;

        }

        public override Object ToServerObject () {

            Server.Application.WorkOutcome serverWorkOutcome = new Server.Application.WorkOutcome ();

            MapToServerObject (serverWorkOutcome);

            return serverWorkOutcome;

        }

        public WorkOutcome Copy () {

            Server.Application.WorkOutcome serverWorkOutcome = (Server.Application.WorkOutcome)ToServerObject ();

            WorkOutcome copiedWorkOutcome = new WorkOutcome (application, serverWorkOutcome);

            return copiedWorkOutcome;
            
        }

        public Boolean IsEqual (WorkOutcome compareWorkOutcome) {

            Boolean isEqual = base.IsEqual ((CoreConfigurationObject)compareWorkOutcome);


            return isEqual;

        }

        #endregion 

    }

}
