using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Authorizations {

    [DataContract (Name = "AuthorizationLine")]
    public class AuthorizationLine : CoreObject {

        #region Private Properties

        [DataMember (Name = "AuthorizationId")]
        private Int64 authorizationId;

        [DataMember (Name = "LineNumber")]
        private Int32 lineNumber = 0;

        [DataMember (Name = "Status")]
        private String status = String.Empty;

        [DataMember (Name = "ServiceDate")]
        private DateTime serviceDate;

        [DataMember (Name = "AdmissionDate")]
        private DateTime? admissionDate;

        [DataMember (Name = "DischargeDate")]
        private DateTime? dischargeDate;


        [DataMember (Name = "RevenueCode")]
        private String revenueCode = String.Empty;

        [DataMember (Name = "RevenueDescription")]
        private String revenueDescription = String.Empty;

        [DataMember (Name = "ServiceCode")]
        private String serviceCode = String.Empty;

        [DataMember (Name = "ServiceDescription")]
        private String serviceDescription = String.Empty;

        [DataMember (Name = "ModifierCode1")]
        private String modifierCode1 = String.Empty;

        [DataMember (Name = "UtilizedUnits")]
        private Decimal utilizedUnits = 0;

        [DataMember (Name = "Units")]
        private Decimal units = 0;

        #endregion


        #region Public Properties

        public Int64 AuthorizationId { get { return authorizationId; } set { authorizationId = value; } }

        public Int32 LineNumber { get { return lineNumber; } set { lineNumber = value; } }

        public String Status { get { return status; } set { status = value; } }


        public DateTime ServiceDate { get { return serviceDate; } set { serviceDate = value; } }

        public DateTime? AdmissionDate { get { return admissionDate; } set { admissionDate = value; } }

        public DateTime? DischargeDate { get { return dischargeDate; } set { dischargeDate = value; } }


        public String RevenueCode { get { return revenueCode; } set { revenueCode = value; } }

        public String RevenueDescription { get { return revenueDescription; } set { revenueDescription = value; } }

        public String ServiceCode { get { return serviceCode; } set { serviceCode = value; } }

        public String ServiceDescription { get { return serviceDescription; } set { serviceDescription = value; } }

        public String ModifierCode1 { get { return modifierCode1; } set { modifierCode1 = value; } }


        public Decimal UtilizedUnits { get { return utilizedUnits; } set { utilizedUnits = value; } }

        public Decimal Units { get { return units; } set { units = value; } }

        #endregion


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            authorizationId = (Int64) currentRow["AuthorizationId"];

            lineNumber = Convert.ToInt32 (currentRow["LineNumber"]);

            status = (String) currentRow["LineStatus"];


            serviceDate = (DateTime) currentRow["ServiceDate"];

            admissionDate = (currentRow["AdmissionDate"] is System.DBNull) ? null : admissionDate = Convert.ToDateTime (currentRow["AdmissionDate"]);

            dischargeDate = (currentRow["DischargeDate"] is System.DBNull) ? null : dischargeDate = Convert.ToDateTime (currentRow["DischargeDate"]);


            revenueCode = (String) currentRow["RevenueCode"];

            revenueDescription = (String) currentRow["RevenueCodeDescription"];

            serviceCode = (String) currentRow["ServiceCode"];

            serviceDescription = (String) currentRow["ServiceCodeDescription"];

            modifierCode1 = (String) currentRow["ModifierCode1"];


            utilizedUnits = (Decimal) currentRow["UtilizedUnits"];

            units = (Decimal) currentRow["ServiceUnits"];

            return;

        }

        #endregion

    }

}
