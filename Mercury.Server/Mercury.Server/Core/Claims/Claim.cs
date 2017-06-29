using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Claims {

    [DataContract (Name = "Claim")]
    public class Claim : CoreObject {

        #region Private Properties

        [DataMember (Name = "ClaimNumber")]
        private String claimNumber;

        [DataMember (Name = "MemberId")]
        private Int64 memberId;

        [DataMember (Name = "ServiceProviderId")]
        private Int64 serviceProviderId;
        
        [DataMember (Name = "PayToProviderAffiliationId")]
        private Int64 payToProviderAffiliationId;
        
        [DataMember (Name = "PayToProviderId")]
        private Int64 payToProviderId;

        [DataMember (Name = "ClaimType")]
        private Enumerations.ClaimType claimType = Enumerations.ClaimType.NotSpecified;

        [DataMember (Name = "ClaimFromDate")]
        private DateTime claimFromDate;

        [DataMember (Name = "ClaimThruDate")]
        private DateTime claimThruDate;

        [DataMember (Name = "AdmissionDate")]
        private DateTime? admissionDate = null;

        [DataMember (Name = "Status")]
        private Enumerations.ClaimStatus status = Enumerations.ClaimStatus.NotSpecified;

        [DataMember (Name = "BillType")]
        private String billType = String.Empty;

        [DataMember (Name = "PrincipalDiagnosisCode")]
        private String principalDiagnosisCode = String.Empty;

        [DataMember (Name = "PrincipalDiagnosisVersion")]
        private Int32 principalDiagnosisVersion = 9;

        [DataMember (Name = "PrincipalDiagnosisDescription")]
        private String principalDiagnosisDescription = String.Empty;

        [DataMember (Name = "BilledAmount")]
        private Decimal billedAmount = 0;

        [DataMember (Name = "PaidAmount")]
        private Decimal paidAmount = 0;

        [DataMember (Name = "PaidDate")]
        private DateTime? paidDate = null;

        [DataMember (Name = "DenialReason")]
        private String denialReason = String.Empty;

        #endregion 


        #region Public Properties

        public String ClaimNumber { get { return claimNumber; } set { claimNumber = value; } }

        public Int64 MemberId { get { return memberId; } set { memberId = value; } }

        public Int64 ServiceProviderId { get { return serviceProviderId; } set { serviceProviderId = value; } }

        public Int64 PayToProviderAffiliationId { get { return payToProviderAffiliationId; } set { payToProviderAffiliationId = value; } }

        public Int64 PayToProviderId { get { return payToProviderId; } set { payToProviderId = value; } }

        public Enumerations.ClaimType ClaimType { get { return claimType; } set { claimType = value; } }

        public DateTime ClaimFromDate { get { return claimFromDate; } set { claimFromDate = value; } }

        public DateTime ClaimThruDate { get { return claimThruDate; } set { claimThruDate = value; } }

        public DateTime? AdmissionDate { get { return admissionDate; } set { admissionDate = value; } }

        public Enumerations.ClaimStatus Status { get { return status; } set { status = value; } }

        public String BillType { get { return billType; } set { billType = value; } }

        public String PrincipalDiagnosisCode { get { return principalDiagnosisCode; } set { principalDiagnosisCode = value; } }

        public Int32 PrincipalDiagnosisVersion { get { return principalDiagnosisVersion; } set { principalDiagnosisVersion = value; } }

        public String PrincipalDiagnosisDescription { get { return principalDiagnosisDescription; } set { principalDiagnosisDescription = value; } }

        public Decimal BilledAmount { get { return billedAmount; } set { billedAmount = value; } }

        public Decimal PaidAmount { get { return paidAmount; } set { paidAmount = value; } }

        public DateTime? PaidDate { get { return paidDate; } set { paidDate = value; } }

        public String DenialReason { get { return denialReason; } set { denialReason = value; } }

        #endregion 


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            claimNumber = (String) currentRow["ClaimNumber"];
            
            memberId = (Int64) currentRow["MemberId"];

            serviceProviderId = base.IdFromSql (currentRow, "ServiceProviderId");

            payToProviderId = base.IdFromSql (currentRow, "PayToProviderId");

            payToProviderAffiliationId = base.IdFromSql (currentRow, "PayToProviderAffiliationId");

            claimType = (Enumerations.ClaimType)Convert.ToInt32 (currentRow["ClaimType"]);

            claimFromDate = (DateTime) currentRow["ClaimDateFrom"];

            claimThruDate = (DateTime) currentRow["ClaimDateThru"];
            
            admissionDate = (currentRow["AdmissionDate"] is System.DBNull) ? null : admissionDate = Convert.ToDateTime (currentRow["AdmissionDate"]);


            status = (Enumerations.ClaimStatus)Convert.ToInt32 (currentRow["ClaimStatus"]);

            billType = (String) currentRow["BillType"];

            principalDiagnosisCode = (String) currentRow["PrincipalDiagnosisCode"];

            principalDiagnosisVersion = Convert.ToInt32 (currentRow["PrincipalDiagnosisVersion"]);

            principalDiagnosisDescription = (String) currentRow["PrincipalDiagnosisName"];


            billedAmount = (Decimal) currentRow["BilledAmount"];

            paidAmount = (Decimal) currentRow["PaidAmount"];

            paidDate = (currentRow["PaidDate"] is System.DBNull) ? null : paidDate = Convert.ToDateTime (currentRow["PaidDate"]);


            denialReason = (String) currentRow["DenialReason"];

            return;

        }

        #endregion 

    }

}
