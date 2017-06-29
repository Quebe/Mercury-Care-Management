using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Member {

    [Serializable]
    public class MemberRelationship : CoreObject {
        
        #region Private Properties

        private Int64 memberId;

        private String familyId;

        private Int64 relatedMemberId;

        private String relatedMemberName;

        private String relatedMemberGender;

        private DateTime relatedMemberBirthDate;

        private String relatedMemberCurrentAgeText;

        private Int64 relationshipId;

        private String relationshipName;

        private DateTime effectiveDate;

        private DateTime terminationDate;

        #endregion


        #region Public Properties

        public Int64 MemberId { get { return memberId; } }

        public String FamilyId { get { return familyId; } }

        public Int64 RelatedMemberId { get { return relatedMemberId; } }

        public String RelatedMemberName { get { return relatedMemberName; } }

        public String RelatedMemberGender { get { return relatedMemberGender; } }

        public DateTime RelatedMemberBirthDate { get { return relatedMemberBirthDate; } }

        public String RelatedMemberCurrentAgeText { get { return relatedMemberCurrentAgeText; } }

        public Int64 RelationshipId { get { return relationshipId; } }

        public String RelationshipName { get { return relationshipName; } }

        public DateTime EffectiveDate { get { return effectiveDate; } }

        public DateTime TerminationDate { get { return terminationDate; } }

        #endregion


        #region Constructors

        public MemberRelationship (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public MemberRelationship (Application applicationReference, Server.Application.MemberRelationship serverRelationship) {

            BaseConstructor (applicationReference, serverRelationship);


            memberId = serverRelationship.MemberId;

            familyId = serverRelationship.FamilyId;

            relatedMemberId = serverRelationship.RelatedMemberId;

            relatedMemberName = serverRelationship.RelatedMemberName;

            relatedMemberGender = serverRelationship.RelatedMemberGender;

            relatedMemberBirthDate = serverRelationship.RelatedMemberBirthDate;

            relatedMemberCurrentAgeText = serverRelationship.RelatedMemberCurrentAgeText;

            relationshipId = serverRelationship.RelationshipId;

            relationshipName = serverRelationship.RelationshipName;

            effectiveDate = serverRelationship.EffectiveDate;

            terminationDate = serverRelationship.TerminationDate;

            return;

        }

        #endregion

    }

}
