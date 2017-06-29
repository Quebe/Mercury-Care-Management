using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Provider {

    [Serializable]
    public class Provider : CoreObject {

        #region Private Properties

        private Int64 entityId;

        private Boolean isPerson = false;

        private DateTime? birthDate;

        private DateTime? deathDate;

        private String gender;

        private Int64 ethnicityId;

        private Int64 citizenshipId;

        // private String englishProficiency = String.Empty;

        private String nationalProviderId;
        
        #endregion


        #region Public Properties

        public override String Name { get { return (Entity != null) ? Entity.Name : String.Empty; } set { if (Entity != null) { Entity.Name = value; } } }


        public Int64 EntityId { get { return entityId; } }

        public Boolean IsPerson { get { return isPerson; } set { isPerson = value; } }

        public DateTime? BirthDate { get { return birthDate; } set { birthDate = value; } }

        public DateTime? DeathDate { get { return deathDate; } set { deathDate = value; } }

        public String Gender { get { return gender; } set { gender = Server.CommonFunctions.SetValueInRange (value.ToUpper (), "M;F;U", "U"); } }

        public Int64 EthnicityId { get { return ethnicityId; } set { ethnicityId = value; } }

        public Int64 CitizenshipId { get { return citizenshipId; } set { citizenshipId = value; } }

        public String NationalProviderId { get { return nationalProviderId; } set { nationalProviderId = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.UniqueId); } }

        #endregion


        #region Public Object/Calculated Properties

        public String BirthDateDescription { get { return ((birthDate.HasValue) ? birthDate.Value.ToString ("MM/dd/yyyy") : "N/A"); } }

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

        public String CitizenshipDescription { get { return application.CoreObjectGetNameById ("Citizenship", citizenshipId, true); } }


        public Int32 CurrentAge {

            get {

                if (!birthDate.HasValue) { return 0; }


                Int32 currentAge = 0;

                DateTime birthDay;


                currentAge = DateTime.Today.Year - birthDate.Value.Year;

                birthDay = new DateTime (DateTime.Today.Year, birthDate.Value.Month, (((birthDate.Value.Month == 2) && (birthDate.Value.Day == 29)) ? 28 : birthDate.Value.Day));

                if (DateTime.Today.CompareTo (birthDay) < 1) { currentAge = currentAge - 1; }

                return currentAge;

            }

        }

        public Int32 CurrentAgeInMonths {

            get {

                if (!birthDate.HasValue) { return 0; }


                Int32 currentAge = 0;

                DateTime birthDay;


                currentAge = DateTime.Today.Month - birthDate.Value.Month;

                currentAge = currentAge + (12 * (DateTime.Today.Year - birthDate.Value.Year));

                birthDay = new DateTime (DateTime.Today.Year, birthDate.Value.Month, (((birthDate.Value.Month == 2) && (birthDate.Value.Day == 29)) ? 28 : birthDate.Value.Day));

                if (DateTime.Today.CompareTo (birthDay) < 1) { currentAge = currentAge - 1; }

                return currentAge;

            }

        }

        public String CurrentAgeDescription { get { return ((CurrentAge >= 2) ? CurrentAge.ToString () + "y" : CurrentAgeInMonths.ToString () + "m"); } }
            

        public Entity.Entity Entity { get { return application.EntityGet (entityId, true); } }

        public List<ProviderContract> Contracts { get { return application.ProviderContractsGet (id, true); } }


        public ProviderEnrollment Enrollment (Int64 enrollmentId) {

            ProviderEnrollment enrollment = null;

            foreach (ProviderEnrollment currentEnrollment in Enrollments) {

                if (currentEnrollment.Id == enrollmentId) { enrollment = currentEnrollment; break; }

            }

            return enrollment;

        }

        public List<ProviderEnrollment> Enrollments { get { return application.ProviderEnrollmentsGet (id, true); } }

        public Boolean HasCurrentEnrollment { get { return (CurrentEnrollment != null); } }

        public ProviderEnrollment CurrentEnrollment {

            get {

                foreach (ProviderEnrollment enrollment in Enrollments) {

                    if ((DateTime.Today >= enrollment.EffectiveDate) && (DateTime.Today <= enrollment.TerminationDate)) {

                        return enrollment;

                    }

                }

                return null;

            }

        }

        public ProviderEnrollment MostRecentEnrollment {

            get {

                ProviderEnrollment mostRecent = null;

                foreach (ProviderEnrollment enrollment in Enrollments) {

                    if (mostRecent == null) { mostRecent = enrollment; }

                    if (enrollment.TerminationDate > mostRecent.TerminationDate) { mostRecent = enrollment; }

                    else if (enrollment.TerminationDate == mostRecent.TerminationDate) {

                        if (enrollment.EffectiveDate > mostRecent.EffectiveDate) { mostRecent = enrollment; }

                    }

                }

                return mostRecent;

            }

        }

        #endregion 


        #region Constructors

        public Provider (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public Provider (Application applicationReference, Server.Application.Provider serverProvider) {

            BaseConstructor (applicationReference, serverProvider);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.Provider serverProvider) {

            base.BaseConstructor (applicationReference, serverProvider);


            entityId  = serverProvider.EntityId;

            birthDate = serverProvider.BirthDate;

            deathDate = serverProvider.DeathDate;

            gender = serverProvider.Gender;


            ethnicityId = serverProvider.EthnicityId;

            citizenshipId = serverProvider.CitizenshipId;


            //englishProficiency = serverProvider.EnglishProficiency;

            nationalProviderId = serverProvider.NationalProviderId;
           
            return;

        }

        #endregion

    }

}
