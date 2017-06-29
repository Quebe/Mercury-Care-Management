using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Mercury.Client.Core.Provider {

    public class ProviderAffiliation : CoreObject {

        #region Private Properties

        private Int64 providerId = 0;

        private Int64 affiliateProviderId = 0;


        private DateTime effectiveDate = new DateTime (1900, 1, 1);

        private DateTime terminationDate = new DateTime (9999, 12, 31);


        private Provider provider = null;

        private Provider affiliateProvider = null;

        #endregion


        #region Public Properties

        public Int64 ProviderId { get { return providerId; } }

        public Int64 AffiliateProviderId { get { return affiliateProviderId; } }


        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }

        public String EffectiveDateDescription { get { return EffectiveDate.ToString ("MM/dd/yyyy"); } }

        public String TerminationDateDescription { get { return ((TerminationDate == new DateTime (9999, 12, 31)) ? "< active >" : TerminationDate.ToString ("MM/dd/yyyy")); } }


        #endregion


        #region Public Properties

        // TODO: SILVERLIGHT UPDATE

        public Provider Provider {

            get {

                if ((provider == null) && (!serverRequests.Contains ("Provider"))) {

                    serverRequests.Add ("Provider");

                    GlobalProgressBarShow ("Provider");

                    Application.ProviderGet (providerId, true, ProviderGetCompleted);

                }

                return provider;

            }

        }

        public Provider AffiliateProvider {

            get {

                if ((affiliateProvider == null) && (!serverRequests.Contains ("AffiliateProvider"))) {

                    serverRequests.Add ("AffiliateProvider");

                    GlobalProgressBarShow ("AffiliateProvider");

                    Application.ProviderGet (affiliateProviderId, true, AffiliateProviderGetCompleted);

                }

                return affiliateProvider;

            }

        }


        #endregion


        #region Property Data Binding Callbacks

        private void ProviderGetCompleted (Object sender, Server.Application.ProviderGetCompletedEventArgs e) {

            serverRequests.Remove ("Provider");

            GlobalProgressBarHide ("Provider");

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                provider = new Provider (Application, e.Result);

                NotifyPropertyChanged ("Provider");

            }

            return;

        }

        private void AffiliateProviderGetCompleted (Object sender, Server.Application.ProviderGetCompletedEventArgs e) {

            serverRequests.Remove ("AffiliateProvider");

            GlobalProgressBarHide ("AffiliateProvider");

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                affiliateProvider = new Provider (Application, e.Result);

                NotifyPropertyChanged ("AffiliateProvider");

            }

            return;

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
