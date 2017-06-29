using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Claims {

    [DataContract (Name = "ClaimLine")]
    public class ClaimLine : CoreObject {

        #region Private Properties

        [DataMember (Name = "ClaimId")]
        private Int64 claimId;

        [DataMember (Name = "Line")]
        private Int32 line;

        [DataMember (Name = "ServiceDateFrom")]
        private DateTime serviceDateFrom;

        [DataMember (Name = "ServiceDateThru")]
        private DateTime serviceDateThru;

        [DataMember (Name = "Status")]
        private Enumerations.ClaimStatus status = Enumerations.ClaimStatus.NotSpecified;

        [DataMember (Name = "ServicePlace")]
        private String servicePlace = String.Empty;

        [DataMember (Name = "RevenueCode")]
        private String revenueCode = String.Empty;

        [DataMember (Name = "RevenueCodeName")]
        private String revenueCodeName = String.Empty;

        [DataMember (Name = "ProcedureCode")]
        private String procedureCode = String.Empty;

        [DataMember (Name = "ProcedureCodeName")]
        private String procedureCodeName = String.Empty;

        [DataMember (Name = "ModifierCode1")]
        private String modifierCode1 = String.Empty;

        [DataMember (Name = "ModifierCode2")]
        private String modifierCode2 = String.Empty;

        [DataMember (Name = "ModifierCode3")]
        private String modifierCode3 = String.Empty;

        [DataMember (Name = "ModifierCode4")]
        private String modifierCode4 = String.Empty;
        
        [DataMember (Name = "Units")]
        private Decimal units = 0;

        [DataMember (Name = "IsEmergency")]
        private Boolean isEmergency = false;

        [DataMember (Name = "IsEpsdt")]
        private Boolean isEpsdt = false;

        [DataMember (Name = "IsFamilyPlanning")]
        private Boolean isFamilyPlanning = false;

        [DataMember (Name = "BilledAmount")]
        private Decimal billedAmount = 0;

        [DataMember (Name = "PaidAmount")]
        private Decimal paidAmount = 0;

        [DataMember (Name = "DenialReason")]
        private String denialReason = String.Empty;

        #endregion


        #region Public Properties

        public Int64 ClaimId { get { return claimId; } set { claimId = value; } }

        public Int32 LineNumber { get { return line; } set { line = value; } }

        
        public DateTime ServiceDateFrom { get { return serviceDateFrom; } set { serviceDateFrom = value; } }

        public DateTime ServiceDateThru { get { return serviceDateThru; } set { serviceDateThru = value; } }

        public Enumerations.ClaimStatus Status { get { return status; } set { status = value; } }

        public String ServicePlace { get { return servicePlace; } set { servicePlace = value; } }

        public String RevenueCode { get { return revenueCode; } set { revenueCode = value; } }

        public String RevenueCodeName { get { return revenueCodeName; } set { revenueCodeName = value; } }

        public String ProcedureCode { get { return procedureCode; } set { procedureCode = value; } }

        public String ProcedureCodeName { get { return procedureCodeName; } set { procedureCodeName = value; } }

        public String ModifierCode1 { get { return modifierCode1; } set { modifierCode1 = value; } }

        public String ModifierCode2 { get { return modifierCode2; } set { modifierCode2 = value; } }

        public String ModifierCode3 { get { return modifierCode3; } set { modifierCode3 = value; } }

        public String ModifierCode4 { get { return modifierCode4; } set { modifierCode4 = value; } }
        
        public Decimal Units { get { return units; } set { units = value; } }

        public Boolean IsEmergency { get { return isEmergency; } set { isEmergency = value; } }

        public Boolean IsEpsdt { get { return isEpsdt; } set { isEpsdt = value; } }

        public Boolean IsFamilyPlanning { get { return isFamilyPlanning; } set { isFamilyPlanning = value; } }

        public Decimal BilledAmount { get { return billedAmount; } set { billedAmount = value; } }

        public Decimal PaidAmount { get { return paidAmount; } set { paidAmount = value; } }

        public String DenialReason { get { return denialReason; } set { denialReason = value; } }

        #endregion


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);

            claimId = (Int64) currentRow["ClaimId"];

            line = (Int32) currentRow["Line"];

            status = (Enumerations.ClaimStatus)Convert.ToInt32 (currentRow["LineStatus"]);


            serviceDateFrom = (DateTime) currentRow["ServiceDateFrom"];

            serviceDateThru = (DateTime) currentRow["ServiceDateThru"];


            servicePlace = (String) currentRow["ServicePlace"];

            revenueCode = (String) currentRow["RevenueCode"];

            revenueCodeName = (String) currentRow["RevenueCodeName"];

            procedureCode = (String) currentRow["ProcedureCode"];

            procedureCodeName = (String) currentRow["ProcedureCodeName"];

            modifierCode1 = (String) currentRow["ModifierCode1"];

            modifierCode2 = (String)currentRow["ModifierCode2"];

            modifierCode3 = (String)currentRow["ModifierCode3"];

            modifierCode4 = (String)currentRow["ModifierCode4"];


            units = (Decimal) currentRow["Units"];


            IsEmergency = Convert.ToBoolean (currentRow["IsEmergency"]);

            IsEpsdt = Convert.ToBoolean (currentRow ["IsEpsdt"]);

            IsFamilyPlanning = Convert.ToBoolean (currentRow["IsFamilyPlanning"]);


            billedAmount = (Decimal) currentRow["BilledAmount"];

            paidAmount = (Decimal) currentRow["PaidAmount"];


            denialReason = (String) currentRow["DenialReason"];

            return;

        }

        #endregion

    }

}
