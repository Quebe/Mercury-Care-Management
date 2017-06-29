using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Insurer {

    [Serializable]
    public class CoverageLevel : CoreObject {

        #region Constructors

        public CoverageLevel (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public CoverageLevel (Application applicationReference, Server.Application.CoverageLevel serverCoverageLevel) {

            BaseConstructor (applicationReference, serverCoverageLevel);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.CoverageLevel serverCoverageLevel) {

            base.BaseConstructor (applicationReference, serverCoverageLevel);

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.CoverageLevel serverCoverageLevel) {

            base.MapToServerObject ((Server.Application.CoreObject)serverCoverageLevel);


            return;

        }

        public override Object ToServerObject () {

            Server.Application.CoverageLevel serverCoverageLevel = new Server.Application.CoverageLevel ();

            MapToServerObject (serverCoverageLevel);

            return serverCoverageLevel;

        }

        public CoverageLevel Copy () {

            Server.Application.CoverageLevel serverCoverageLevel = (Server.Application.CoverageLevel)ToServerObject ();

            CoverageLevel copiedCoverageLevel = new CoverageLevel (application, serverCoverageLevel);

            return copiedCoverageLevel;

        }

        public Boolean IsEqual (CoverageLevel compareCoverageLevel) {

            Boolean isEqual = base.IsEqual ((CoreObject)compareCoverageLevel);


            return isEqual;

        }

        #endregion

    }

}
