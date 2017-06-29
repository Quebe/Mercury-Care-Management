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

namespace Mercury.Client.Core.Member {

    public class MemberEnrollmentPcp : CoreObject {

        #region Private Properties

        private Int64 memberEnrollmentId;

        private Int64 pcpProviderId;

        private Int64 providerAffiliationId;

        private Int64 pcpServiceLocationId;

        private DateTime effectiveDate;

        private DateTime terminationDate;


        private Provider.Provider pcpProvider = null;

        private Provider.ProviderAffiliation providerAffiliation = null;

        #endregion


        #region Public Properties

        public Int64 MemberEnrollmentId { get { return memberEnrollmentId; } set { memberEnrollmentId = value; } }

        public Int64 PcpProviderId { get { return pcpProviderId; } set { pcpProviderId = value; } }

        public Int64 ProviderAffiliationId { get { return providerAffiliationId; } set { providerAffiliationId = value; } }

        public Int64 PcpServiceLocationId { get { return pcpServiceLocationId; } set { pcpServiceLocationId = value; } }


        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }

        public String EffectiveDateDescription { get { return EffectiveDate.ToString ("MM/dd/yyyy"); } }

        public String TerminationDateDescription { get { return ((TerminationDate == new DateTime (9999, 12, 31)) ? "< active >" : TerminationDate.ToString ("MM/dd/yyyy")); } }

        #endregion


        #region Public Properties

        // TODO: SILVERLIGHT UPDATE

        public Provider.Provider PcpProvider {

            get {

                if ((pcpProvider == null) && (!serverRequests.Contains ("PcpProvider"))) {

                    serverRequests.Add ("PcpProvider");

                    GlobalProgressBarShow ("PcpProvider");

                    Application.ProviderGet (pcpProviderId, true, PcpProviderGetCompleted);

                }

                return pcpProvider;

            }

        }

        public Provider.ProviderAffiliation ProviderAffiliation {

            get {

                if ((providerAffiliation == null) && (!serverRequests.Contains ("ProviderAffiliation"))) {

                    serverRequests.Add ("ProviderAffiliation");

                    GlobalProgressBarShow ("ProviderAffiliation");

                    Application.ProviderAffiliationGet (providerAffiliationId, true, ProviderAffiliationGetCompleted);

                }

                return providerAffiliation;

            }

        }
        //public Provider.ProviderAffiliation ProviderAffilation { get { return application.ProviderAffiliationGet (providerAffiliationId, true); } }

        //public Provider.Provider PcpAffiliateProvider { get { return ((ProviderAffilation != null) ? ProviderAffilation.AffiliateProvider : null); } }

        #endregion


        #region Property Data Binding Callbacks

        private void PcpProviderGetCompleted (Object sender, Server.Application.ProviderGetCompletedEventArgs e) {

            serverRequests.Remove ("PcpProvider");

            GlobalProgressBarHide ("PcpProvider");

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                pcpProvider = new Provider.Provider (Application, e.Result);

                NotifyPropertyChanged ("Name");

                NotifyPropertyChanged ("PcpProvider");

            }

            return;

        }

        private void ProviderAffiliationGetCompleted (Object sender, Server.Application.ProviderAffiliationGetCompletedEventArgs e) {

            serverRequests.Remove ("ProviderAffiliation");

            GlobalProgressBarHide ("ProviderAffiliation");

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                providerAffiliation = new Provider.ProviderAffiliation (Application, e.Result);

                NotifyPropertyChanged ("Name");

                NotifyPropertyChanged ("ProviderAffiliation");

            }

            return;

        }
        #endregion 


        #region Constructors

        public MemberEnrollmentPcp (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public MemberEnrollmentPcp (Application applicationReference, Server.Application.MemberEnrollmentPcp serverMemberEnrollmentPcp) {

            BaseConstructor (applicationReference, serverMemberEnrollmentPcp);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.MemberEnrollmentPcp serverMemberEnrollmentPcp) {

            base.BaseConstructor (applicationReference, serverMemberEnrollmentPcp);


            memberEnrollmentId = serverMemberEnrollmentPcp.MemberEnrollmentId;

            pcpProviderId = serverMemberEnrollmentPcp.PcpProviderId;

            providerAffiliationId = serverMemberEnrollmentPcp.ProviderAffiliationId;

            pcpServiceLocationId = serverMemberEnrollmentPcp.PcpServiceLocationId;


            effectiveDate = serverMemberEnrollmentPcp.EffectiveDate;

            terminationDate = serverMemberEnrollmentPcp.TerminationDate;

            return;

        }

        #endregion

    }

}
