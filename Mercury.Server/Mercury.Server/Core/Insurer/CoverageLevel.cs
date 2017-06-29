using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Insurer {

    [DataContract (Name = "CoverageLevel")]
    public class CoverageLevel : CoreObject {
        
        #region Constructors

        public CoverageLevel (Application applicationReference) {

            base.BaseConstructor (applicationReference);

            return;

        }

        public CoverageLevel (Application applicationReference, Int64 forId) {

            base.BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion

    }

}
