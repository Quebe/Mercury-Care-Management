using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Insurer {

    [Serializable]
    public class CoverageType : CoreObject {

        #region Constructors

        public CoverageType (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public CoverageType (Application applicationReference, Server.Application.CoverageType serverCoverageType) {

            BaseConstructor (applicationReference, serverCoverageType);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.CoverageType serverCoverageType) {

            base.BaseConstructor (applicationReference, serverCoverageType);

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.CoverageType serverCoverageType) {

            base.MapToServerObject ((Server.Application.CoreObject)serverCoverageType);


            return;

        }

        public override Object ToServerObject () {

            Server.Application.CoverageType serverCoverageType = new Server.Application.CoverageType ();

            MapToServerObject (serverCoverageType);

            return serverCoverageType;

        }

        public CoverageType Copy () {

            Server.Application.CoverageType serverCoverageType = (Server.Application.CoverageType)ToServerObject ();

            CoverageType copiedCoverageType = new CoverageType (application, serverCoverageType);

            return copiedCoverageType;

        }

        public Boolean IsEqual (CoverageType compareCoverageType) {

            Boolean isEqual = base.IsEqual ((CoreObject)compareCoverageType);


            return isEqual;

        }

        #endregion

    }

}
