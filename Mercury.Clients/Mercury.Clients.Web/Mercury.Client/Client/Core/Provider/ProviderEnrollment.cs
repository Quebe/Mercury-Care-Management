using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Provider {

    [Serializable]
    public class ProviderEnrollment : CoreObject {

        #region Private Properties

        private Int64 providerId = 0;

        private Int64 programId = 0;

        private String programProviderId = String.Empty;


        private DateTime effectiveDate = new DateTime (1900, 1, 1);

        private DateTime terminationDate = new DateTime (9999, 12, 31);

        #endregion


        #region Public Properties

        public Int64 ProviderId { get { return providerId; } }

        public Int64 ProgramId { get { return programId; } }

        public String ProgramProviderId { get { return programProviderId; } }


        public DateTime EffectiveDate { get { return effectiveDate; } }

        public DateTime TerminationDate { get { return terminationDate; } }

        #endregion


        #region Public Properties

        public Provider Provider { get { return application.ProviderGet (providerId, true); } }

        public Core.Insurer.Program Program { get { return application.ProgramGet (programId, true); } }

        #endregion


        #region Constructor

        public ProviderEnrollment (Application application) {

            BaseConstructor (application);

            return;

        }

        public ProviderEnrollment (Application application, Server.Application.ProviderEnrollment serverObject) {

            BaseConstructor (application, serverObject);


            providerId = serverObject.ProviderId;

            programId = serverObject.ProgramId;

            programProviderId = serverObject.ProgramProviderId;


            effectiveDate = serverObject.EffectiveDate;

            terminationDate = serverObject.TerminationDate;

            return;

        }

        #endregion


    }

}
