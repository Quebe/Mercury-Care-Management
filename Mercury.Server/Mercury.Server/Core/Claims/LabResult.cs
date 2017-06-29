using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Claims {

    [DataContract (Name = "LabResult")]
    public class LabResult : CoreObject {

        #region Private Properties

        [DataMember (Name = "LabReferenceNumber")]
        private String labReferenceNumber;

        [DataMember (Name = "MemberId")]
        private Int64 memberId;

        [DataMember (Name = "ProviderId")]
        private Int64 providerId;

        [DataMember (Name = "ClaimId")]
        private Int64 claimId;

        [DataMember (Name = "ServiceDate")]
        private DateTime serviceDate;

        [DataMember (Name = "Loinc")]
        private String loinc = String.Empty;

        [DataMember (Name = "LabTestName")]
        private String labTestName = String.Empty;

        [DataMember (Name = "LabValue")]
        private Decimal labValue = 0;

        [DataMember (Name = "LabUnitType")]
        private String labUnitType = String.Empty;

        [DataMember (Name = "LabResultText")]
        private String labResultText = String.Empty;

        #endregion


        #region Public Properties

        public String LabReferenceNumber { get { return labReferenceNumber; } set { labReferenceNumber = value; } }

        public Int64 MemberId { get { return memberId; } set { memberId = value; } }

        public Int64 ProviderId { get { return providerId; } set { providerId = value; } }

        public Int64 ClaimId { get { return claimId; } set { claimId = value; } }

        public DateTime ServiceDate { get { return serviceDate; } set { serviceDate = value; } }

        public String Loinc { get { return loinc; } set { loinc = value; } }

        public String LabTestName { get { return labTestName; } set { labTestName = value; } }

        public Decimal LabValue { get { return labValue; } set { labValue = value; } }

        public String LabUnitType { get { return labUnitType; } set { labUnitType = value; } }

        public String LabResultText { get { return labResultText; } set { labResultText = value; } }

        #endregion


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            LabReferenceNumber = (String)currentRow["LabReferenceNumber"];

            memberId = (Int64)currentRow["MemberId"];

            providerId = base.IdFromSql (currentRow, "ProviderId");

            claimId = base.IdFromSql (currentRow, "ClaimId");

            serviceDate = (DateTime)currentRow["ServiceDate"];

            loinc = (String)currentRow["Loinc"];

            labTestName = (String)currentRow["LabTestName"];

            labValue = base.DecimalFromSql (currentRow, "LabValue");

            labUnitType = (String)currentRow["LabUnitType"];

            labResultText = (String)currentRow["LabResultText"];


            return;

        }

        #endregion 


    }

}
