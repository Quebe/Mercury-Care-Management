using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Provider {

    [Serializable]
    public class ProviderServiceLocation : CoreObject {

        #region Private Properties

        private Int64 providerServiceLocationId = 0;

        private Int64 providerId = 0;

        private Int64 providerAffiliationId = 0;

        private Int64 entityAddressId = 0;

        private Int64 providerEnrollmentId = 0;

        private DateTime effectiveDate = new DateTime (1900, 1, 1);

        private DateTime terminationDate = new DateTime (9999, 12, 31);

        private String serviceLocationNumber = String.Empty;


        private Boolean isPcp = false;

        private Boolean isAcceptingNewPatients = false;

        private Int32 panelSizeMaximum = 0;

        private Int32 ageMinimum = 0;

        private Int32 ageMaximum = 0;

        private Boolean hasHandicapAccess = false;

        private String officeHoursSunday = String.Empty;

        private String officeHoursMonday = String.Empty;

        private String officeHoursTuesday = String.Empty;

        private String officeHoursWednesday = String.Empty;

        private String officeHoursThursday = String.Empty;

        private String officeHoursFriday = String.Empty;

        private String officeHoursSaturday = String.Empty;

        #endregion


        #region Public Properties

        override public Int64 Id { get { return providerServiceLocationId; } }

        public Int64 ProviderServiceLocationId { get { return providerServiceLocationId; } }

        public Int64 ProviderId { get { return providerId; } }

        public Int64 ProviderAffiliationId { get { return providerAffiliationId; } }

        public Int64 EntityAddressId { get { return entityAddressId; } }

        public Int64 ProviderEnrollmentId { get { return providerEnrollmentId; } }


        public DateTime EffectiveDate { get { return effectiveDate; } }

        public DateTime TerminationDate { get { return terminationDate; } }


        public String ServiceLocationNumber { get { return serviceLocationNumber; } }

        public Boolean IsPcp { get { return isPcp; } }

        public Boolean IsAcceptingNewPatients { get { return isAcceptingNewPatients; } }

        public Int32 PanelSizeMaximum { get { return panelSizeMaximum; } }

        public Int32 AgeMinimum { get { return ageMinimum; } }

        public Int32 AgeMaximum { get { return ageMaximum; } }

        public Boolean HasHandicapAccess { get { return hasHandicapAccess; } }

        public String OfficeHoursSunday { get { return officeHoursSunday; } }

        public String OfficeHoursMonday { get { return officeHoursMonday; } }

        public String OfficeHoursTuesday { get { return officeHoursTuesday; } }

        public String OfficeHoursWednesday { get { return officeHoursWednesday; } }

        public String OfficeHoursThursday { get { return officeHoursThursday; } }

        public String OfficeHoursFriday { get { return officeHoursFriday; } }

        public String OfficeHoursSaturday { get { return officeHoursSaturday; } }


        public Provider Provider { get { return application.ProviderGet (providerId, true); } }

        public String ProviderName { get { return (Provider != null) ? Provider.Name : String.Empty; } }


        public ProviderAffiliation ProviderAffiliation { get { return application.ProviderAffiliationGet (providerAffiliationId, true); } }

        public String AffiliateProviderName { get { return (ProviderAffiliation != null) ? ProviderAffiliation.AffiliateProviderName : String.Empty; } }


        public Entity.EntityAddress EntityAddress { get { return application.EntityAddressGet (entityAddressId, true); } }

        public ProviderEnrollment ProviderEnrollment { get { return application.ProviderEnrollmentGet (providerEnrollmentId, true); } }

        #endregion


        #region Constructor

        public ProviderServiceLocation (Application application) {

            BaseConstructor (application);

            return;

        }

        public ProviderServiceLocation (Application application, Server.Application.ProviderServiceLocation serverObject) {

            BaseConstructor (application, serverObject);


            providerId = serverObject.ProviderId;

            providerAffiliationId = serverObject.ProviderAffiliationId;

            entityAddressId = serverObject.EntityAddressId;

            providerEnrollmentId = serverObject.ProviderEnrollmentId;


            effectiveDate = serverObject.EffectiveDate;

            terminationDate = serverObject.TerminationDate;


            serviceLocationNumber = serverObject.ServiceLocationNumber;

            isPcp = serverObject.IsPcp;

            isAcceptingNewPatients = serverObject.IsAcceptingNewPatients;

            panelSizeMaximum = serverObject.PanelSizeMaximum;

            ageMinimum = serverObject.AgeMinimum;

            ageMaximum = serverObject.AgeMaximum;

            hasHandicapAccess = serverObject.HasHandicapAccess;

            officeHoursSunday = serverObject.OfficeHoursSunday;

            officeHoursMonday = serverObject.OfficeHoursMonday;

            officeHoursTuesday = serverObject.OfficeHoursTuesday;

            officeHoursWednesday = serverObject.OfficeHoursWednesday;

            officeHoursThursday = serverObject.OfficeHoursThursday;

            officeHoursFriday = serverObject.OfficeHoursFriday;

            officeHoursSaturday = serverObject.OfficeHoursSaturday;

            return;

        }

        #endregion


    }

}
