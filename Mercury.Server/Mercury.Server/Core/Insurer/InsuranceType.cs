using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Insurer {

    [DataContract (Name = "InsuranceType")]
    public class InsuranceType : CoreObject {
        
        #region Constructors

        public InsuranceType (Application applicationReference) {

            base.BaseConstructor (applicationReference);

            return;

        }

        public InsuranceType (Application applicationReference, Int64 forInsuranceTypeId) {

            base.BaseConstructor (applicationReference, forInsuranceTypeId);

            return;

        }

        #endregion


    }

}
