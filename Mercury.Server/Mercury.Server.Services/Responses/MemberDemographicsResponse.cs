using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses {

    [DataContract (Name = "MemberDemographicsResponse")]
    public class MemberDemographicsResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Member")]
        private Server.Core.Member.Member member = null;


        [DataMember (Name = "Entity")]
        private Server.Core.Entity.Entity entity = null;

        [DataMember (Name = "EntityContactInformations")]
        private List<Server.Core.Entity.EntityContactInformation> entityContactInformations = null;

        [DataMember (Name = "EntityAddresses")]
        private List<Server.Core.Entity.EntityAddress> entityAddresses = null;


        [DataMember (Name = "MemberEnrollments")]
        private List<Server.Core.Member.MemberEnrollment> memberEnrollments = null;

        [DataMember (Name = "MemberCurrentEnrollmentCoverages")]
        private List<Server.Core.Member.MemberEnrollmentCoverage> memberCurrentEnrollmentCoverages = null;

        [DataMember (Name = "MemberCurrentEnrollmentPcps")]
        private List<Server.Core.Member.MemberEnrollmentPcp> memberCurrentEnrollmentPcps = null;

        [DataMember (Name = "MemberRelationships")]
        private List<Server.Core.Member.MemberRelationship> memberRelationships = null;


        [DataMember (Name = "Providers")]
        private List<Server.Core.Provider.Provider> providers = new List<Server.Core.Provider.Provider> ();

        [DataMember (Name = "ProviderAffiliations")]
        private List<Server.Core.Provider.ProviderAffiliation> providerAffiliations = new List<Server.Core.Provider.ProviderAffiliation> ();

        #endregion


        #region Public Properties

        public Server.Core.Member.Member Member {

            get { return member; }

            set {

                if (member != value) {

                    member = value;


                    entity = (value != null) ? value.Entity : null;

                    entityContactInformations = (entity != null) ? entity.ContactInformations : null;

                    entityAddresses = (entity != null) ? entity.Addresses : null;


                    memberEnrollments = (member != null) ? member.Enrollments : null;

                    memberCurrentEnrollmentCoverages = (member != null) ? ((member.CurrentEnrollment != null) ? member.CurrentEnrollment.Coverages : null) : null;

                    memberCurrentEnrollmentPcps = (member != null) ? ((member.CurrentEnrollment != null) ? member.CurrentEnrollment.Pcps : null) : null;

                    memberRelationships = (member != null) ? member.Relationships : null;


                    #region Related Providers

                    providers = new List<Server.Core.Provider.Provider> ();

                    if (memberCurrentEnrollmentPcps != null) {

                        foreach (Server.Core.Member.MemberEnrollmentPcp currentMemberEnrollmentPcp in memberCurrentEnrollmentPcps) {

                            if ((DateTime.Today >= currentMemberEnrollmentPcp.EffectiveDate) && (DateTime.Today <= currentMemberEnrollmentPcp.TerminationDate)) {

                                if (currentMemberEnrollmentPcp.PcpProvider.Entity == null) { /* DO NOTHING */ } // FORCE DATABASE READ OF ENTITY OBJECT

                                providers.Add (currentMemberEnrollmentPcp.PcpProvider);

                                if (currentMemberEnrollmentPcp.ProviderAffiliation != null) {

                                    providerAffiliations.Add (currentMemberEnrollmentPcp.ProviderAffiliation);

                                    if (currentMemberEnrollmentPcp.ProviderAffiliation.AffiliateProvider != null) {

                                        if (currentMemberEnrollmentPcp.ProviderAffiliation.AffiliateProvider.Entity == null) { /* DO NOTHING */ } // FORCE DATABASE READ OF ENTITY OBJECT

                                        providers.Add (currentMemberEnrollmentPcp.ProviderAffiliation.AffiliateProvider);

                                    }

                                }

                            }

                        }

                    }

                    #endregion 

                }

            }

        }

        #endregion


        #region Constructors

        public MemberDemographicsResponse () { /* DO NOTHING */ }

        public MemberDemographicsResponse (Server.Core.Member.Member forMember) {

            if (forMember == null) { return; }


            Member = forMember;

            return;

        }

        public MemberDemographicsResponse (Application application, Int64 forMemberId) {

            Member = application.MemberGet (forMemberId);

            return;

        }

        #endregion 

    }

}