using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Member {

    [Serializable]
    public class MemberEnrollmentTplCob : CoreObject {

        #region Private Properties

        private Int64 memberId = 0;

        private Int64 sponsorId = 0;

        private Int64 subscriberId = 0;

        private Int64 programId = 0;

        private String programMemberId = String.Empty;

        private Int64 benefitPlanId = 0;

        private DateTime effectiveDate = new DateTime (1900, 1, 1);

        private DateTime terminationDate = new DateTime (9999, 12, 31);

        #endregion


        #region Public Properties

        public Int64 MemberId { get { return memberId; } }

        public Int64 SponsorId { get { return sponsorId; } }

        public Int64 SubscriberId { get { return subscriberId; } }

        public Int64 ProgramId { get { return programId; } }

        public String ProgramMemberId { get { return programMemberId; } set { programMemberId = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.UniqueId); } }

        public Int64 BenefitPlanId { get { return benefitPlanId; } }

        public DateTime EffectiveDate { get { return effectiveDate; } }

        public DateTime TerminationDate { get { return terminationDate; } }


        public Member Member { get { return application.MemberGet (memberId, true); } }

        public String MemberName { get { return ((Member != null) ? Member.Name : String.Empty); } }


        public Sponsor.Sponsor Sponsor { get { return application.SponsorGet (sponsorId, true); } }

        public String SponsorName { get { return ((Sponsor != null) ? Sponsor.Name : String.Empty); } }


        public Member Subscriber { get { return application.MemberGet (subscriberId, true); } }

        public String SubscriberName { get { return ((Subscriber != null) ? Subscriber.Name : String.Empty); } }


        public Insurer.Insurer Insurer { get { return ((Program == null) ? null : Program.Insurer); } }

        public String InsurerName { get { return ((Insurer != null) ? Insurer.Name : String.Empty); } }


        public Insurer.Program Program { get { return application.ProgramGet (programId, true); } }

        public String ProgramName { get { return ((Program != null) ? Program.Name : String.Empty); } }


        public Insurer.BenefitPlan BenefitPlan { get { return application.BenefitPlanGet (benefitPlanId, true); } }

        public String BenefitPlanName { get { return ((BenefitPlan != null) ? BenefitPlan.Name : String.Empty); } }

        #endregion


        #region Constructors

        public MemberEnrollmentTplCob (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public MemberEnrollmentTplCob (Application applicationReference, Server.Application.MemberEnrollmentTplCob serverMemberEnrollmentTplCob) {

            BaseConstructor (applicationReference, serverMemberEnrollmentTplCob);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.MemberEnrollmentTplCob serverMemberEnrollmentTplCob) {

            base.BaseConstructor (applicationReference, serverMemberEnrollmentTplCob);


            memberId = serverMemberEnrollmentTplCob.MemberId;

            sponsorId = serverMemberEnrollmentTplCob.SponsorId;

            subscriberId = serverMemberEnrollmentTplCob.SubscriberId;

            programId = serverMemberEnrollmentTplCob.ProgramId;

            programMemberId = serverMemberEnrollmentTplCob.ProgramMemberId;


            benefitPlanId = serverMemberEnrollmentTplCob.BenefitPlanId;


            effectiveDate = serverMemberEnrollmentTplCob.EffectiveDate;

            terminationDate = serverMemberEnrollmentTplCob.TerminationDate;

            return;

        }

        #endregion

    }

}
