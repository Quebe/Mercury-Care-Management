using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Member {

    [Serializable]
    [DataContract (Name = "MemberRelationship")]
    public class MemberRelationship : CoreObject {

        #region Private Properties
        
        [DataMember (Name = "MemberId")]
        private Int64 memberId;

        [DataMember (Name = "FamilyId")]
        private String familyId;

        [DataMember (Name = "RelatedMemberId")]
        private Int64 relatedMemberId;

        [DataMember (Name = "RelatedMemberName")]
        private String relatedMemberName;

        [DataMember (Name = "RelatedMemberGender")]
        private String relatedMemberGender;

        [DataMember (Name = "RelatedMemberBirthDate")]
        private DateTime relatedMemberBirthDate;

        [DataMember (Name = "RelatedMemberCurrentAgeText")]
        private String relatedMemberCurrentAgeText;

        [DataMember (Name = "RelationshipId")]
        private Int64 relationshipId;

        [DataMember (Name = "RelationshipName")]
        private String relationshipName;

        [DataMember (Name = "EffectiveDate")]
        private DateTime effectiveDate;

        [DataMember (Name = "TerminationDate")]
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

        public MemberRelationship (Application applicationReference, Int64 memberRelationshipId) {

            BaseConstructor (applicationReference, memberRelationshipId);

            return;

        }

        #endregion



        #region Database Functions

        public override Boolean Load (Int64 forId) { return base.LoadFromDalSp (forId); }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            memberId = (Int64) currentRow["MemberId"];

            familyId = (String) currentRow["FamilyId"];

            relatedMemberId = (Int64) currentRow["RelatedMemberId"];

            relatedMemberName = (String) currentRow["RelatedMemberName"];

            relatedMemberGender = (String) currentRow["RelatedMemberGender"];

            relatedMemberBirthDate = (DateTime) currentRow["RelatedMemberBirthDate"];

            relatedMemberCurrentAgeText = (String) currentRow["RelatedMemberCurrentAge"];
            
            relationshipId = (Int64) currentRow["RelationshipId"];

            relationshipName = (String) currentRow["RelationshipName"];

            effectiveDate = (DateTime) currentRow["EffectiveDate"];

            terminationDate = (DateTime) currentRow["TerminationDate"];


            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        #endregion



    }

}
