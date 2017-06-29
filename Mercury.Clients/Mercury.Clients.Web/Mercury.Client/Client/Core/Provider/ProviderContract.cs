using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Provider {
    
    [Serializable]
    public class ProviderContract : CoreObject {

        #region Private Properties

        private Int64 providerContractId = 0;
        
        private Int64 providerId = 0;

        private Int64 providerAffiliationId = 0;

        private Int64 programId = 0;

        private Int64 contractId = 0;


        private Boolean isContracted = false;

        private Boolean isParticipating = false;

        private Boolean isCapitated = false;


        private DateTime effectiveDate = new DateTime (1900, 1, 1);

        private DateTime terminationDate = new DateTime (9999, 12, 31);

        #endregion


        #region Public Properties

        override public Int64 Id { get { return providerContractId; } }


        public Int64 ProviderContractId { get { return providerContractId; } }

        public Int64 ProviderId { get { return providerId; } }

        public Int64 ProviderAffiliationId { get { return providerAffiliationId; } }

        public Int64 ProgramId { get { return programId; } }

        public Int64 ContractId { get { return contractId; } }


        public Boolean IsContracted { get { return isContracted; } }

        public Boolean IsParticipating { get { return isParticipating; } }

        public Boolean IsCapitated { get { return isCapitated; } }


        public DateTime EffectiveDate { get { return effectiveDate; } }

        public DateTime TerminationDate { get { return terminationDate; } }


        public Provider Provider { get { return application.ProviderGet (providerId, true); } }

        public String ProviderName { get { return (Provider != null) ? Provider.Name : String.Empty; } }


        public ProviderAffiliation ProviderAffiliation { get { return application.ProviderAffiliationGet (providerAffiliationId, true); } }

        public String AffiliateProviderName { get { return (ProviderAffiliation != null) ? ProviderAffiliation.AffiliateProviderName : String.Empty; } }


        public Core.Insurer.Program Program { get { return application.ProgramGet (programId, true); } }

        public String ProgramName { get { return (Program != null) ? Program.Name : String.Empty; } }


        public Core.Insurer.Contract Contract { get { return application.ContractGet (contractId, true); } }
        
        public String ContractName { get { return (Contract != null) ? Contract.Name : String.Empty; } }

        #endregion
        

        #region Constructor

        public ProviderContract (Application application) {

            BaseConstructor (application);

            return;

        }

        public ProviderContract (Application application, Server.Application.ProviderContract serverProviderContract) {

            BaseConstructor (application, serverProviderContract);


            providerId = serverProviderContract.ProviderId;

            providerAffiliationId = serverProviderContract.ProviderAffiliationId;
            
            programId = serverProviderContract.ProgramId;

            contractId = serverProviderContract.ContractId;


            isContracted = serverProviderContract.IsContracted;
            
            isParticipating = serverProviderContract.IsParticipating;

            isCapitated = serverProviderContract.IsCapitated;
            

            effectiveDate = serverProviderContract.EffectiveDate;

            terminationDate = serverProviderContract.TerminationDate;

            return;

        }

        #endregion

    }

}
