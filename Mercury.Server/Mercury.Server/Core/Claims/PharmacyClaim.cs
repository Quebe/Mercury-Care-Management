using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Claims {

    [DataContract (Name = "PharmacyClaim")]
    public class PharmacyClaim {

        #region Private Properties

        [DataMember (Name = "ClaimId")]
        private Int64 claimId;

        [DataMember (Name = "MemberId")]
        private Int64 memberId;

        [DataMember (Name = "ClaimType")]
        private String claimType = String.Empty;

        [DataMember (Name = "ClaimDateFrom")]
        private DateTime claimDateFrom;

        [DataMember (Name = "ClaimDateThru")]
        private DateTime claimDateThru;

        [DataMember (Name = "PrescribedDate")]
        private DateTime prescribedDate;

        [DataMember (Name = "PaidDate")]
        private DateTime paidDate;

        [DataMember (Name = "Status")]
        private String status = String.Empty;

        [DataMember (Name = "NdcCode")]
        private String ndcCode = String.Empty;

        [DataMember (Name = "DrugName")]
        private String drugName = String.Empty;

        [DataMember (Name = "DaysSupply")]
        private Decimal daysSupply = 0;

        [DataMember (Name = "Units")]
        private Decimal units = 0;

        [DataMember (Name = "Refills")]
        private Int32 refills = 0;

        [DataMember (Name = "Dosage")]
        private String dosage = String.Empty;

        [DataMember (Name = "DeaClassification")]
        private String deaClassification = String.Empty;

        [DataMember (Name = "TherapeuticClassification")]
        private String therapeuticClassification = String.Empty;

        [DataMember (Name = "PharmacyName")]
        private String pharmacyName = String.Empty;

        [DataMember (Name = "ServiceProviderName")]
        private String serviceProviderName = String.Empty;

        [DataMember (Name = "ServiceProviderSpecialtyName")]
        private String serviceProviderSpecialtyName = String.Empty;

        [DataMember (Name = "BilledAmount")]
        private Decimal billedAmount = 0;

        [DataMember (Name = "PaidAmount")]
        private Decimal paidAmount = 0;

        [DataMember (Name = "ExternalClaimId")]
        private String externalClaimId = String.Empty;

        [DataMember (Name = "ExternalMemberId")]
        private String externalMemberId = String.Empty;

        #endregion


        #region Public Properties

        public Int64 ClaimId { get { return claimId; } set { claimId = value; } }

        public Int64 MemberId { get { return memberId; } set { memberId = value; } }

        public String ClaimType { get { return claimType; } set { claimType = value; } }

        public DateTime ClaimDateFrom { get { return claimDateFrom; } set { claimDateFrom = value; } }

        public DateTime ClaimDateThru { get { return claimDateThru; } set { claimDateThru = value; } }

        public DateTime PrescribedDate { get { return prescribedDate; } set { prescribedDate = value; } }

        public DateTime PaidDate { get { return paidDate; } set { paidDate = value; } }

        public String Status { get { return status; } set { status = value; } }

        public String NdcCode { get { return ndcCode; } set { ndcCode = value; } }

        public String DrugName { get { return drugName; } set { drugName = value; } }

        public Decimal DaysSupply { get { return daysSupply; } set { daysSupply = value; } }

        public Decimal Units { get { return units; } set { units = value; } }

        public Int32 Refills { get { return refills; } set { refills = value; } }

        public String Dosage { get { return dosage; } set { dosage = value; } }

        public String DeaClassification { get { return deaClassification; } set { deaClassification = value; } }

        public String TherapeuticClassification { get { return therapeuticClassification; } set { therapeuticClassification = value; } }

        public String PharmacyName { get { return pharmacyName; } set { pharmacyName = value; } }

        public String ServiceProviderName { get { return serviceProviderName; } set { serviceProviderName = value; } }

        public String ServiceProviderSpecialtyName { get { return serviceProviderSpecialtyName; } set { serviceProviderSpecialtyName = value; } }

        public Decimal BilledAmount { get { return billedAmount; } set { billedAmount = value; } }

        public Decimal PaidAmount { get { return paidAmount; } set { paidAmount = value; } }

        public String ExternalClaimId { get { return externalClaimId; } set { externalClaimId = value; } }

        public String ExternalMemberId { get { return externalMemberId; } set { externalMemberId = value; } }

        #endregion


        #region Data Functions

        public void MapDataFields (System.Data.DataRow currentRow) {

            claimId = (Int64) currentRow["PharmacyClaimId"];

            memberId = (Int64) currentRow["MemberId"];

            // claim type            

            claimDateFrom = (DateTime) currentRow["ServiceDate"];

            claimDateThru = (DateTime) currentRow["ServiceDate"];

            // prescribedDate = (DateTime)currentRow["PrescribedDate"];

            paidDate = (DateTime)currentRow["PaidDate"];

            // status = (String)currentRow["ClaimStatus"];

            ndcCode = (String) currentRow["NationalDrugCode"];

            // drugName = (String) currentRow["DrugName"];

            daysSupply = Convert.ToDecimal (currentRow["DaysSupply"]);

            units = (Decimal) currentRow["Units"];

            refills = (Int32)currentRow["RefillCount"];

            // dosage = (String)currentRow["Dosage"];

            // deaClassification = (String) currentRow["DeaClassification"];

            // therapeuticClassification = (String) currentRow["TherapeuticClassification"];

            // pharmacyName = (String) currentRow["PharmacyName"];

            // serviceProviderName = (String) currentRow["ServiceProviderName"];

            // serviceProviderSpecialtyName = (String) currentRow["ServiceProviderSpecialtyName"];

            billedAmount = (Decimal) currentRow["GrossDueAmount"];

            paidAmount = (Decimal) currentRow["PaidAmount"];

            //externalClaimId = (String) currentRow["ExternalClaimId"];

            //externalMemberId = (String) currentRow["ExternalMemberId"];

            return;

        }

        #endregion

    }

}
