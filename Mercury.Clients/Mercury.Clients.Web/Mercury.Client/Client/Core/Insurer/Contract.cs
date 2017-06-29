using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Insurer {

    [Serializable]
    public class Contract : CoreConfigurationObject {

        #region Private Properties

        private DateTime effectiveDate;

        private DateTime terminationDate;

        #endregion


        #region Public Properties

        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }

        #endregion

        
        #region Constructor

        public Contract (Application application) {

            BaseConstructor (application);

            return;

        }

        public Contract (Application application, Server.Application.Contract serverContract) {

            BaseConstructor (application, serverContract);

           
            effectiveDate = serverContract.EffectiveDate;

            terminationDate = serverContract.TerminationDate;

            return;

        }

        #endregion

    }

}
