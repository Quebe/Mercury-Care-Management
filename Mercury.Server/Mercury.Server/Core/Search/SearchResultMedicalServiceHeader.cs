using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Search {

    [DataContract (Name = "SearchResultMedicalServiceHeader")]
    public class SearchResultMedicalServiceHeader {

        #region Private Properties

        [DataMember (Name = "ObjectType")]
        private String objectType = "Service";
        
        [DataMember (Name = "Id")]
        private Int64 serviceId = 0;

        [DataMember (Name = "ServiceType")]
        private Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType serviceType = Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType.NotSpecified;

        [DataMember (Name = "Name")]
        private String serviceName = String.Empty;

        [DataMember (Name = "Description")]
        private String description;

        [DataMember (Name = "Enabled")]
        private Boolean enabled = false;

        [DataMember (Name = "Visible")]
        private Boolean visible = false;

        [DataMember (Name = "LastPaidDate")]
        private DateTime lastPaidDate;

        [DataMember (Name = "SetType")]
        private Core.MedicalServices.Enumerations.ServiceSetType setType = MedicalServices.Enumerations.ServiceSetType.Intersection;

        [DataMember (Name = "WithinDays")]
        private Int32 withinDays = 0;

        [DataMember (Name = "CreateAccountInfo")]
        private Mercury.Server.Data.AuthorityAccountStamp createAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp ();

        [DataMember (Name = "ModifiedAccountInfo")]
        private Mercury.Server.Data.AuthorityAccountStamp modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp ();

        #endregion


        #region Public Properities

        public String ObjectType { get { return objectType; } }

        public Int64 ServiceId { get { return serviceId; } }

        public Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType ServiceType { get { return serviceType; } }

        public String Name { get { return serviceName; } }

        public Boolean Enabled { get { return enabled; } }

        public Boolean Visible { get { return visible; } }

        public DateTime LastPaidDate { get { return lastPaidDate; } }

        public Mercury.Server.Data.AuthorityAccountStamp CreateAccountInfo { get { return createAccountInfo; } }

        public Mercury.Server.Data.AuthorityAccountStamp ModifiedAccountInfo { get { return modifiedAccountInfo; } }

        #endregion


        #region Database Functions

        public void MapDataFields (System.Data.DataRow currentRow) {

            serviceId = (Int64) currentRow["ServiceId"];

            serviceName = (String) currentRow["ServiceName"];

            serviceType = (Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType) (Int32) currentRow["ServiceType"];

            description = (String) currentRow["ServiceDescription"];

            enabled = (Boolean) currentRow["Enabled"];

            visible = (Boolean) currentRow["Visible"];

            lastPaidDate = (DateTime) currentRow ["LastPaidDate"];



            setType = (Core.MedicalServices.Enumerations.ServiceSetType)((Int32)currentRow["SetType"]); 

            withinDays = (Int32)currentRow["SetWithinDays"]; 


            createAccountInfo.MapDataFields (currentRow, "Create");

            modifiedAccountInfo.MapDataFields (currentRow, "Modified");

            return;

        }

        #endregion

    }

}
