using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Provider {

    [Serializable]
    public class ProviderAffiliation : CoreObject   {

        #region Private Properties

        private Int64 providerId = 0;

        private Int64 affiliateProviderId = 0;


        private DateTime effectiveDate = new DateTime (1900, 1, 1);

        private DateTime terminationDate = new DateTime (9999, 12, 31);

        #endregion


        #region Public Properties

        public Int64 ProviderId { get { return providerId; } }

        public Int64 AffiliateProviderId { get { return affiliateProviderId; } }


        public DateTime EffectiveDate { get { return effectiveDate; } }

        public DateTime TerminationDate { get { return terminationDate; } }

        #endregion 

        
        #region Public Properties

        public Provider Provider { get { return application.ProviderGet (providerId, true); } }

        public String ProviderName { get { return (Provider != null) ? Provider.Name : String.Empty; } }


        public Provider AffiliateProvider { get { return application.ProviderGet (affiliateProviderId, true); } } 

        public String AffiliateProviderName { get { return (AffiliateProvider != null) ? AffiliateProvider.Name : String.Empty; } }


        public Boolean HasPreviousAffiliation { get { return (PreviousAffiliation != null); } }

        public ProviderAffiliation PreviousAffiliation {

            get {

                ProviderAffiliation previousAffiliation = null;

                List<ProviderAffiliation> providerAffiliations = base.Application.ProviderAffiliationsGet (providerId, true);

                foreach (ProviderAffiliation currentAffiliation in providerAffiliations) {

                    if (currentAffiliation.Id != Id) {

                        if (currentAffiliation.TerminationDate < effectiveDate) {

                            if (previousAffiliation == null) { previousAffiliation = currentAffiliation; }

                            else {

                                if (currentAffiliation.TerminationDate > previousAffiliation.TerminationDate) {

                                    previousAffiliation = currentAffiliation;

                                }

                            }

                        }

                    }

                }

                return previousAffiliation;

            }

        }

        #endregion


        #region Constructor

        public ProviderAffiliation (Application application) {

            BaseConstructor (application);

            return;

        }

        public ProviderAffiliation (Application application, Server.Application.ProviderAffiliation serverProviderAffiliation) {

            BaseConstructor (application, serverProviderAffiliation);


            providerId = serverProviderAffiliation.ProviderId;

            affiliateProviderId = serverProviderAffiliation.AffiliateProviderId;


            effectiveDate = serverProviderAffiliation.EffectiveDate;

            terminationDate = serverProviderAffiliation.TerminationDate;

            return;

        }

        #endregion


    }

}
