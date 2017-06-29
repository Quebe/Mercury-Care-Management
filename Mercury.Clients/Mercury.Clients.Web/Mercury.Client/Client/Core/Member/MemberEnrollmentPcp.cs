using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Member {

    [Serializable]
    public class MemberEnrollmentPcp : CoreObject {

        #region Private Properties

        private Int64 memberEnrollmentId;

        private Int64 pcpProviderId;

        private Int64 providerAffiliationId;

        private Int64 pcpServiceLocationId;

        private DateTime effectiveDate;

        private DateTime terminationDate;

        #endregion


        #region Public Properties

        public Int64 MemberEnrollmentId { get { return memberEnrollmentId; } set { memberEnrollmentId = value; } }

        public Int64 PcpProviderId { get { return pcpProviderId; } set { pcpProviderId = value; } }

        public Int64 ProviderAffiliationId { get { return providerAffiliationId; } set { providerAffiliationId = value; } }

        public Int64 PcpServiceLocationId { get { return pcpServiceLocationId; } set { pcpServiceLocationId = value; } }

        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }

        #endregion 

        
        #region Public Properties

        public Provider.Provider PcpProvider { get { return application.ProviderGet (pcpProviderId, true); } }

        public Provider.ProviderAffiliation ProviderAffilation { get { return application.ProviderAffiliationGet (providerAffiliationId, true); } }

        public Provider.Provider PcpAffiliateProvider { get { return ((ProviderAffilation != null) ? ProviderAffilation.AffiliateProvider : null); } }

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
