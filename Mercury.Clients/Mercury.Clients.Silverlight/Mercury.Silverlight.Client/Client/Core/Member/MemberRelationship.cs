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

        public String RelatedMemberBirthDateDescription { get { return relatedMemberBirthDate.ToString ("MM/dd/yyyy"); } }

        public String RelatedMemberCurrentAgeText { get { return relatedMemberCurrentAgeText; } }

        public Int64 RelationshipId { get { return relationshipId; } }

        public String RelationshipName { get { return relationshipName; } }


        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }

        public String EffectiveDateDescription { get { return EffectiveDate.ToString ("MM/dd/yyyy"); } }

        public String TerminationDateDescription { get { return ((TerminationDate == new DateTime (9999, 12, 31)) ? "< active >" : TerminationDate.ToString ("MM/dd/yyyy")); } }

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
