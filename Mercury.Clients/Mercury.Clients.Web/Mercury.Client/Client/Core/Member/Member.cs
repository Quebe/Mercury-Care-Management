using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Member {

    [Serializable]
    public class Member : CoreObject {

        #region Private Properties

        private Int64 entityId;

        private DateTime birthDate;

        private DateTime? deathDate;

        private String gender;

        private Int64 ethnicityId;

        private Int64 citizenshipId;

        private Int64 languageId;

        private Int64 maritalStatusId;

        private String familyId;

        #endregion


        #region Public Properties

        public override String Name { get { return (Entity != null) ? Entity.Name : String.Empty; } set { if (Entity != null) { Entity.Name = value; } } }

        public override string Description { get { return Name + "[" + Gender + " | " + CurrentAge.ToString ().PadLeft (2, '0') + "]"; } }


        public Int64 EntityId { get { return entityId; } }

        public DateTime BirthDate { get { return birthDate; } set { birthDate = value; } }

        public DateTime? DeathDate { get { return deathDate; } set { deathDate = value; } }

        public String Gender { get { return gender; } set { gender = Server.CommonFunctions.SetValueInRange (value.ToUpper (), "M;F;U", "U"); } }

        public Int64 EthnicityId { get { return ethnicityId; } set { ethnicityId = value; } }

        public Int64 CitizenshipId { get { return citizenshipId; } set { citizenshipId = value; } }

        public Int64 LanguageId { get { return languageId; } set { languageId = value; } }

        public Int64 MaritalStatusId { get { return maritalStatusId; } set { maritalStatusId = value; } }

        public String FamilyId { get { return familyId; } set { familyId = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.UniqueId); } }

        #endregion 


        #region Public Object/Calculated Properties

        public String BirthDateDescription { get { return birthDate.ToString ("MM/dd/yyyy"); } }

        public String DeathDateDescription { get { return ((deathDate.HasValue) ? deathDate.Value.ToString ("MM/dd/yyyy") : "< alive >"); } }

        public String GenderDescription {

            get {

                switch (gender) {

                    case "M": return "Male";

                    case "F": return "Female";

                    default: return "Unknown";

                }

            }

        }

        public String EthnicityDescription { get { return application.CoreObjectGetNameById ("Ethnicity", ethnicityId, true); } }

        public String LanguageDescription { get { return application.CoreObjectGetNameById ("Language", languageId, true); } }

        public String CitizenshipDescription { get { return application.CoreObjectGetNameById ("Citizenship", citizenshipId, true); } }

        public String MaritalStatusDescription { get { return application.CoreObjectGetNameById ("MaritalStatus", maritalStatusId, true); } }


        public Int32 CurrentAge {

            get {

                Int32 currentAge = 0;

                DateTime birthDay;


                currentAge = DateTime.Today.Year - birthDate.Year;

                birthDay = new DateTime (DateTime.Today.Year, birthDate.Month, (((birthDate.Month == 2) && (birthDate.Day == 29)) ? 28 : birthDate.Day));

                if (DateTime.Today.CompareTo (birthDay) < 1) { currentAge = currentAge - 1; }

                return currentAge;

            }

        }

        public Int32 CurrentAgeInMonths {

            get {

                Int32 currentAge = 0;

                DateTime birthDay;


                currentAge = DateTime.Today.Month - birthDate.Month;

                currentAge = currentAge + (12 * (DateTime.Today.Year - birthDate.Year));

                birthDay = new DateTime (DateTime.Today.Year, birthDate.Month, (((birthDate.Month == 2) && (birthDate.Day == 29)) ? 28 : birthDate.Day));

                if (DateTime.Today.CompareTo (birthDay) < 1) { currentAge = currentAge - 1; }

                return currentAge;

            }

        }

        public String CurrentAgeDescription { get { return ((CurrentAge >= 2) ? CurrentAge.ToString () + "y" : CurrentAgeInMonths.ToString () + "m"); } }
            

        public Entity.Entity Entity { get { return ((application != null) ? application.EntityGet (entityId, true) : null); } }

        public List<MemberRelationship> Relationships { get { return application.MemberRelationshipsGet (Id, true); } }

        public List<MemberRelationship> RelationshipsActive {

            get {

                List<MemberRelationship> memberRelationships = new List<MemberRelationship> ();

                foreach (MemberRelationship currentRelationship in Relationships) {

                    if ((DateTime.Today >= currentRelationship.EffectiveDate) && (DateTime.Today <= currentRelationship.TerminationDate)) {

                        memberRelationships.Add (currentRelationship);

                    }

                }

                return memberRelationships;

            }

        }

        public MemberRelationship RelationshipSelf {

            get {

                MemberRelationship selfRelationship = null;

                foreach (MemberRelationship currentRelationship in Relationships) {

                    if ((DateTime.Today >= currentRelationship.EffectiveDate) && (DateTime.Today <= currentRelationship.TerminationDate)) {

                        if (currentRelationship.RelationshipId == 18) {

                            selfRelationship = currentRelationship;

                            break;

                        }

                    }

                }

                return selfRelationship;

            }

        }

        public String FamilyIdFromRelationship {

            get {

                String familyId = (RelationshipSelf != null) ? RelationshipSelf.FamilyId : String.Empty;

                return familyId;

            }

        }

        #endregion
        

        #region Enrollment Object Properties

        public MemberEnrollment Enrollment (Int64 enrollmentId) {

            MemberEnrollment enrollment = null;

            foreach (MemberEnrollment currentEnrollment in Enrollments) {

                if (currentEnrollment.Id == enrollmentId) { enrollment = currentEnrollment; break; }

            }

            return enrollment;

        }


        public List<MemberEnrollment> Enrollments { get { return application.MemberEnrollmentsGet (id, true); } }

        public Boolean HasCurrentEnrollment { get { return (CurrentEnrollment != null); } }

        public MemberEnrollment CurrentEnrollment {

            get {

                foreach (MemberEnrollment enrollment in Enrollments) {

                    if ((DateTime.Today >= enrollment.EffectiveDate) && (DateTime.Today <= enrollment.TerminationDate)) {

                        return enrollment;

                    }

                }

                return null;

            }

        }

        public MemberEnrollment MostRecentEnrollment {

            get {

                MemberEnrollment mostRecent = null;

                foreach (MemberEnrollment enrollment in Enrollments) {

                    if (mostRecent == null) { mostRecent = enrollment; }

                    if (enrollment.TerminationDate > mostRecent.TerminationDate) { mostRecent = enrollment; }

                    else if (enrollment.TerminationDate == mostRecent.TerminationDate) {

                        if (enrollment.EffectiveDate > mostRecent.EffectiveDate) { mostRecent = enrollment; }

                    }

                }

                return mostRecent;

            }

        }


        public Boolean HasCurrentEnrollmentCoverage { get { return (CurrentEnrollmentCoverage != null); } }

        public MemberEnrollmentCoverage CurrentEnrollmentCoverage {

            get {

                if (CurrentEnrollment != null) {

                    if (CurrentEnrollment.Coverages != null) {

                        foreach (MemberEnrollmentCoverage currentCoverage in CurrentEnrollment.Coverages) {

                            if ((DateTime.Today >= currentCoverage.EffectiveDate) && (DateTime.Today <= currentCoverage.TerminationDate)) {

                                return currentCoverage;

                            }

                        }

                    }

                }

                return null;

            }

        }


        public Boolean HasCurrentEnrollmentPcp { get { return (CurrentEnrollmentPcp != null); } }

        public MemberEnrollmentPcp CurrentEnrollmentPcp {

            get {

                if (CurrentEnrollment != null) {

                    if (CurrentEnrollment.Pcps != null) {

                        foreach (MemberEnrollmentPcp currentMemberEnrollmentPcp in CurrentEnrollment.Pcps) {

                            if ((DateTime.Today >= currentMemberEnrollmentPcp.EffectiveDate) && (DateTime.Today <= currentMemberEnrollmentPcp.TerminationDate)) {

                                return currentMemberEnrollmentPcp;

                            }

                        }

                    }

                }

                return null;

            }

        }


        public Boolean HasProspectiveEnrollment { get { return (ProspectiveEnrollment != null); } }

        public MemberEnrollment ProspectiveEnrollment {

            get {

                foreach (MemberEnrollment enrollment in Enrollments) {

                    if (DateTime.Today <= enrollment.EffectiveDate) {

                        return enrollment;

                    }

                }

                return null;

            }

        }


        public List<MemberEnrollmentTplCob> EnrollmentTplCobs { get { return application.MemberEnrollmentTplCobsGet (id, true); } }

        public Boolean HasCurrentEnrollmentTplCob { get { return (CurrentEnrollmentTplCob != null); } }

        public MemberEnrollmentTplCob CurrentEnrollmentTplCob {

            get {

                foreach (MemberEnrollmentTplCob enrollmentTplCob in EnrollmentTplCobs) {

                    if ((DateTime.Today >= enrollmentTplCob.EffectiveDate) && (DateTime.Today <= enrollmentTplCob.TerminationDate)) {

                        return enrollmentTplCob;

                    }

                }

                return null;

            }

        }

        public MemberEnrollmentTplCob MostRecentEnrollmentTplCob {

            get {

                MemberEnrollmentTplCob mostRecent = null;

                foreach (MemberEnrollmentTplCob enrollmentTplCob in EnrollmentTplCobs) {

                    if (mostRecent == null) { mostRecent = enrollmentTplCob; }

                    if (enrollmentTplCob.TerminationDate > mostRecent.TerminationDate) { mostRecent = enrollmentTplCob; }

                    else if (enrollmentTplCob.TerminationDate == mostRecent.TerminationDate) {

                        if (enrollmentTplCob.EffectiveDate > mostRecent.EffectiveDate) { mostRecent = enrollmentTplCob; }

                    }

                }

                return mostRecent;

            }

        }


        public List<Server.Application.PopulationMembershipSummaryDataView> PopulationMembershipSummary { get { return application.PopulationMembershipSummary (id, true); } }

        #endregion


        #region Constructors

        public Member (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public Member (Application applicationReference, Server.Application.Member serverMember) {

            BaseConstructor (applicationReference, serverMember);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.Member serverMember) {

            base.BaseConstructor (applicationReference, serverMember);


            entityId = serverMember.EntityId;

            birthDate = serverMember.BirthDate;

            deathDate = serverMember.DeathDate;

            gender = serverMember.Gender;


            ethnicityId = serverMember.EthnicityId;

            citizenshipId = serverMember.CitizenshipId;

            languageId = serverMember.LanguageId;

            maritalStatusId = serverMember.MaritalStatusId;

            FamilyId = serverMember.FamilyId;

            return;

        }

        #endregion


        #region Public Methods - Population Management

        public List<Population.PopulationMembership> PopulationMembership { get { return Application.PopulationMembershipGetByMember (id, true); } }


        public Core.Population.PopulationMembership CurrentPopulationMembership (String forPopulationName) {

            Core.Population.PopulationMembership populationMembership = null;


            foreach (Core.Population.PopulationMembership currentMembership in PopulationMembership) {

                if (currentMembership.PopulationName == forPopulationName) {

                    if ((DateTime.Today >= currentMembership.EffectiveDate) && (DateTime.Today <= currentMembership.TerminationDate)) {

                        populationMembership = currentMembership;

                        break;

                    }

                }

            }

            return populationMembership;

        }

        public Int64 CurrentPopulationMembershipId (String forPopulationName) {  return ((CurrentPopulationMembership (forPopulationName) != null) ? CurrentPopulationMembership (forPopulationName).Id : 0); }

        public Boolean InPopulation (String forPopulationName) { return (CurrentPopulationMembership (forPopulationName) != null); }


        public Population.PopulationMembership MostRecentPopulationMembership (String populationName) {

            Population.PopulationMembership populationMembership = null;

            foreach (Population.PopulationMembership currentMembership in PopulationMembership) {

                if (currentMembership.PopulationName == populationName) {

                    if (populationMembership == null) { populationMembership = currentMembership; }

                    else {

                        if (currentMembership.TerminationDate > populationMembership.TerminationDate) {

                            populationMembership = currentMembership;

                        }

                    }

                }

            }

            return populationMembership;

        }

        #endregion

    }

}
