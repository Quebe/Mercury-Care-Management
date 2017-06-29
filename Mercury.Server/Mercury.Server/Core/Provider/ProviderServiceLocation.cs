using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Provider {

    [DataContract (Name="ProviderServiceLocation")]
    public class ProviderServiceLocation : CoreObject {
        
        #region Private Properties
        
        [DataMember (Name = "ProviderId")]
        private Int64 providerId = 0;

        [DataMember (Name = "ProviderAffiliationId")]
        private Int64 providerAffiliationId = 0;

        [DataMember (Name = "EntityAddressId")]
        private Int64 entityAddressId = 0;

        [DataMember (Name = "ProviderEnrollmentId")]
        private Int64 providerEnrollmentId = 0;

        [DataMember (Name = "EffectiveDate")]
        private DateTime effectiveDate = new DateTime (1900, 1, 1);

        [DataMember (Name = "TerminationDate")]
        private DateTime terminationDate = new DateTime (9999, 12, 31);

        [DataMember (Name = "ServiceLocationNumber")]
        private String serviceLocationNumber = String.Empty;


        [DataMember (Name = "IsPcp")]
        private Boolean isPcp = false;

        [DataMember (Name = "IsAcceptingNewPatients")]
        private Boolean isAcceptingNewPatients = false;

        [DataMember (Name = "PanelSizeMaximum")]
        private Int32 panelSizeMaximum = 0;

        [DataMember (Name = "AgeMinimum")]
        private Int32 ageMinimum = 0;

        [DataMember (Name = "AgeMaximum")]
        private Int32 ageMaximum = 0;

        [DataMember (Name = "HasHandicapAccess")]
        private Boolean hasHandicapAccess = false;

        [DataMember (Name = "OfficeHoursSunday")]
        private String officeHoursSunday = String.Empty;

        [DataMember (Name = "OfficeHoursMonday")]
        private String officeHoursMonday = String.Empty;

        [DataMember (Name = "OfficeHoursTuesday")]
        private String officeHoursTuesday = String.Empty;

        [DataMember (Name = "OfficeHoursWednesday")]
        private String officeHoursWednesday = String.Empty;
        
        [DataMember (Name = "OfficeHoursThursday")]
        private String officeHoursThursday = String.Empty;

        [DataMember (Name = "OfficeHoursFriday")]
        private String officeHoursFriday = String.Empty;
        
        [DataMember (Name = "OfficeHoursSaturday")]
        private String officeHoursSaturday = String.Empty;

        #endregion


        #region Public Properties

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

        #endregion

        
        #region Constructors 

        public ProviderServiceLocation (Application applicationReference) {

            BaseConstructor (applicationReference);
        
            return; 
        
        }

        public ProviderServiceLocation (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference);

            if (!Load (forId)) {

                throw new ApplicationException ("Unable to load Provider Service Location from the database for " + forId.ToString () + ".");

            }

        }

        #endregion


        #region Database Functions

        override public Boolean Load (Int64 forId) {

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable tableEnrollment;

            if (base.application.EnvironmentDatabase == null) { return false; }

            selectStatement.Append ("EXEC dal.ProviderServiceLocation_Select " + forId.ToString ());

            tableEnrollment = base.application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            if (tableEnrollment.Rows.Count == 1) {

                MapDataFields (tableEnrollment.Rows[0]);

                return true;

            }

            else {

                return false;

            }

        }

        override public void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            providerId = (Int64) currentRow["ProviderId"];

            providerAffiliationId = (Int64) currentRow ["ProviderAffiliationId"];

            entityAddressId = (Int64) currentRow ["EntityAddressId"];

            providerEnrollmentId = (Int64) currentRow["ProviderEnrollmentId"];

            
            effectiveDate = (DateTime) currentRow["EffectiveDate"];

            terminationDate = (DateTime) currentRow["TerminationDate"];


            serviceLocationNumber = (String) currentRow ["ServiceLocationNumber"];

            isPcp = (Boolean) currentRow["IsPcp"];

            isAcceptingNewPatients = (Boolean) currentRow["IsAcceptingNewPatients"];

            panelSizeMaximum = (Int32) currentRow["PanelSizeMaximum"];

            ageMinimum = (Int32) currentRow["AgeMinimum"];

            ageMaximum = (Int32) currentRow["AgeMaximum"];

            hasHandicapAccess = (Boolean) currentRow["HasHandicapAccess"];

            officeHoursSunday = (String) currentRow["OfficeHoursSunday"];

            officeHoursMonday = (String) currentRow["OfficeHoursMonday"];

            officeHoursTuesday = (String) currentRow["OfficeHoursTuesday"];

            officeHoursWednesday = (String) currentRow["OfficeHoursWednesday"];

            officeHoursThursday = (String) currentRow["OfficeHoursThursday"];

            officeHoursFriday = (String) currentRow["OfficeHoursFriday"];

            officeHoursSaturday = (String) currentRow["OfficeHoursSaturday"];
            
            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        #endregion


    }

}
