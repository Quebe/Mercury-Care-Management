using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Insurer {

    [DataContract (Name = "CoverageType")]
    public class CoverageType : CoreObject {
        
        #region Constructors

        public CoverageType (Application applicationReference) {

            base.BaseConstructor (applicationReference);

            return;

        }

        public CoverageType (Application applicationReference, Int64 forId) {

            base.BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion

    }

}
