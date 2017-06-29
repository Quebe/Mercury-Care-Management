using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.MedicalServices {

    [DataContract (Name = "MemberServiceDetailSingleton")]
    public class MemberServiceDetailSingleton {

        #region Private Properties
      
        [DataMember (Name = "MemberServiceId")]
        private Int64 memberServiceId;

        [DataMember (Name = "SingletonDefinitionId")]
        private Int64 singletonDefinitionId;

        [DataMember (Name = "EventDate")]
        private DateTime eventDate;

        [DataMember (Name = "ClaimId")]
        private Int64 claimId;

        [DataMember (Name = "ExternalClaimId")]
        private String externalClaimId;

        [DataMember (Name = "ClaimLine")]
        private Int32 claimLine;

        [DataMember (Name = "MemberId")]
        private Int64 memberId;

        [DataMember (Name = "ProviderId")]
        private Int64 providerId;

        [DataMember (Name = "ClaimType")]
        private Core.Claims.Enumerations.ClaimType claimType = Claims.Enumerations.ClaimType.NotSpecified;

        [DataMember (Name = "ClaimStatus")]
        private Core.Claims.Enumerations.ClaimStatus claimStatus = Claims.Enumerations.ClaimStatus.NotSpecified;

        [DataMember (Name = "ClaimDateFrom")]
        private DateTime? claimDateFrom;

        [DataMember (Name = "ClaimDateThru")]
        private DateTime? claimDateThru;

        [DataMember (Name = "ServiceDateFrom")]
        private DateTime? serviceDateFrom;

        [DataMember (Name = "ServiceDateThru")]
        private DateTime? serviceDateThru;

        [DataMember (Name = "AdmissionDate")]
        private DateTime? admissionDate;

        [DataMember (Name = "DischargeDate")]
        private DateTime? dischargeDate;

        [DataMember (Name = "ReceivedDate")]
        private DateTime? receivedDate;

        [DataMember (Name = "PaidDate")]
        private DateTime? paidDate;

        [DataMember (Name = "BillType")]
        private String billType;

        [DataMember (Name = "PrincipalDiagnosisCode")]
        private String principalDiagnosisCode;

        [DataMember (Name = "PrincipalDiagnosisVersion")]
        private Int32 principalDiagnosisVersion = 9;

        [DataMember (Name = "DiagnosisCode")]
        private String diagnosisCode;

        [DataMember (Name = "DiagnosisVersion")]
        private Int32 diagnosisVersion = 9;

        [DataMember (Name = "Icd9ProcedureCode")]
        private String icd9ProcedureCode;

        [DataMember (Name = "LocationCode")]
        private String locationCode;

        [DataMember (Name = "RevenueCode")]
        private String revenueCode;

        [DataMember (Name = "ProcedureCode")]
        private String procedureCode;

        [DataMember (Name = "ModifierCode")]
        private String modifierCode;

        [DataMember (Name = "SpecialtyName")]
        private String specialtyName;

        [DataMember (Name = "IsPcpClaim")]
        private Boolean isPcpClaim;

        [DataMember (Name = "NdcCode")]
        private String ndcCode;

        [DataMember (Name = "Units")]
        private Decimal units;

        [DataMember (Name = "DeaClassification")]
        private String deaClassification;

        [DataMember (Name = "TherapeuticClassification")]
        private String therapeuticClassification;

        [DataMember (Name = "LabLoincCode")]
        private String labLoincCode;

        [DataMember (Name = "LabName")]
        private String labName = String.Empty;

        [DataMember (Name = "LabValue")]
        private Decimal labValue;

        [DataMember (Name = "Description")]
        private String description;

        #endregion


        #region Public Properties

        public Int64 MemberServiceId { get { return memberServiceId; } set { memberServiceId = value; } }

        public Int64 SingletonDefinitionId { get { return singletonDefinitionId; } set { singletonDefinitionId = value; } }

        public DateTime EventDate { get { return eventDate; } set { eventDate = value; } }

        public Int64 ClaimId { get { return claimId; } set { claimId = value; } }

        public String ExternalClaimId { get { return externalClaimId; } set { externalClaimId = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.UniqueId); } }

        public Int32 ClaimLine { get { return claimLine; } set { claimLine = value; } }

        public Int64 MemberId { get { return memberId; } set { memberId = value; } }

        public Int64 ProviderId { get { return providerId; } set { providerId = value; } }

        public Core.Claims.Enumerations.ClaimType ClaimType { get { return claimType; } set { claimType = value; } }

        public Core.Claims.Enumerations.ClaimStatus ClaimStatus { get { return claimStatus; } set { claimStatus = value; } }

        public DateTime? ClaimDateFrom { get { return claimDateFrom; } set { claimDateFrom = value; } }

        public DateTime? ClaimDateThru { get { return claimDateThru; } set { claimDateThru = value; } }

        public DateTime? ServiceDateFrom { get { return serviceDateFrom; } set { serviceDateFrom = value; } }

        public DateTime? ServiceDateThru { get { return serviceDateThru; } set { serviceDateThru = value; } }

        public DateTime? AdmissionDate { get { return admissionDate; } set { admissionDate = value; } }

        public DateTime? DischargeDate { get { return dischargeDate; } set { dischargeDate = value; } }

        public DateTime? ReceivedDate { get { return receivedDate; } set { receivedDate = value; } }

        public DateTime? PaidDate { get { return paidDate; } set { paidDate = value; } }

        public String BillType { get { return billType; } set { billType = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.BillType); } }

        public String PrincipalDiagnosisCode { get { return principalDiagnosisCode; } set { principalDiagnosisCode = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.DiagnosisCode); } }

        public Int32 PrincipalDiagnosisVersion { get { return principalDiagnosisVersion; } set { principalDiagnosisVersion = value; } }

        public String DiagnosisCode { get { return diagnosisCode; } set { diagnosisCode = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.DiagnosisCode); } }

        public Int32 DiagnosisVersion { get { return diagnosisVersion; } set { diagnosisVersion = value; } }
        
        public String Icd9ProcedureCode { get { return icd9ProcedureCode; } set { icd9ProcedureCode = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Icd9ProcedureCode); } }

        public String LocationCode { get { return locationCode; } set { locationCode = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.LocationCode); } }

        public String RevenueCode { get { return revenueCode; } set { revenueCode = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.RevenueCode); } }

        public String ProcedureCode { get { return procedureCode; } set { procedureCode = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.ProcedureCode); } }

        public String ModifierCode { get { return modifierCode; } set { modifierCode = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.ModifierCode); } }

        public String SpecialtyName { get { return specialtyName; } set { specialtyName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public Boolean IsPcpClaim { get { return isPcpClaim; } set { isPcpClaim = value; } }

        public String NdcCode { get { return ndcCode; } set { ndcCode = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.NdcCode); } }

        public Decimal Units { get { return units; } set { units = value; } }

        public String DeaClassification { get { return deaClassification; } set { deaClassification = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.DeaClassification); } }

        public String TherapeuticClassification { get { return therapeuticClassification; } set { therapeuticClassification = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.TherapeuticClassification); } }

        public String LabLoincCode { get { return labLoincCode; } set { labLoincCode = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.LabLoincCode); } }

        public String LabName { get { return labName; } set { labName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public Decimal LabValue { get { return labValue; } set { labValue = value; } }

        public String Description { get { return description; } set { description = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        #endregion


        #region Constructors 

        public MemberServiceDetailSingleton (Int64 forMemberServiceId, Int64 forSingletonDefinitionId) {

            memberServiceId = forMemberServiceId;

            singletonDefinitionId = forSingletonDefinitionId;

            return;

        }

        #endregion 


        #region Public Methods

        public void MapDataFields (System.Data.DataRow currentRow) {

            eventDate = (DateTime) currentRow["EventDate"];

            claimId = (Int64) currentRow ["ClaimId"];

            externalClaimId = Convert.ToString (currentRow ["ExternalClaimId"]);

            claimLine = (Int32) currentRow ["ClaimLine"];

            memberId = (Int64) currentRow ["MemberId"];

            providerId = (currentRow ["ProviderId"] is DBNull) ? 0 : (Int64) currentRow ["ProviderId"];

            claimType = (Claims.Enumerations.ClaimType) Convert.ToInt32 (currentRow ["ClaimType"]);

            claimStatus = (Claims.Enumerations.ClaimStatus)Convert.ToInt32 (currentRow["ClaimStatus"]);


            claimDateFrom = (currentRow ["ClaimDateFrom"] is System.DBNull) ? null : (DateTime?) currentRow ["ClaimDateFrom"];

            claimDateThru = (currentRow["ClaimDateThru"] is System.DBNull) ? null : (DateTime?) currentRow["ClaimDateThru"];

            serviceDateFrom = (currentRow["ServiceDateFrom"] is System.DBNull) ? null : (DateTime?) currentRow["ServiceDateFrom"];

            serviceDateThru = (currentRow["ServiceDateThru"] is System.DBNull) ? null : (DateTime?) currentRow["ServiceDateThru"];

            admissionDate = (currentRow["AdmissionDate"] is System.DBNull) ? null : (DateTime?) currentRow["AdmissionDate"];

            dischargeDate = (currentRow["DischargeDate"] is System.DBNull) ? null : (DateTime?) currentRow["DischargeDate"];

            receivedDate = (currentRow["ReceivedDate"] is System.DBNull) ? null : (DateTime?) currentRow["ReceivedDate"];

            paidDate = (currentRow["PaidDate"] is System.DBNull) ? null : (DateTime?) currentRow["PaidDate"];


            billType = (String) currentRow ["BillType"];

            principalDiagnosisCode = Convert.ToString (currentRow["PrincipalDiagnosisCode"]);

            PrincipalDiagnosisVersion = (currentRow["PrincipalDiagnosisVersion"] is DBNull) ? 9 : Convert.ToInt32 (currentRow["PrincipalDiagnosisVersion"]);

            diagnosisCode = Convert.ToString (currentRow ["DiagnosisCode"]);

            DiagnosisVersion = Convert.ToInt32 (currentRow["DiagnosisVersion"]);
            
            icd9ProcedureCode = (String) currentRow ["Icd9ProcedureCode"];

            locationCode = (String) currentRow ["LocationCode"];

            revenueCode = (String) currentRow ["RevenueCode"];

            procedureCode = (String) currentRow ["ProcedureCode"];

            modifierCode = (String) currentRow ["ModifierCode"];


            specialtyName = (String) currentRow["SpecialtyName"];

            isPcpClaim = Convert.ToBoolean (currentRow["IsPcpClaim"]);


            ndcCode = (String) currentRow ["NdcCode"];

            Decimal.TryParse (currentRow["Units"].ToString (), out units);

            deaClassification = (String) currentRow["DeaClassification"];

            therapeuticClassification = (String) currentRow["TherapeuticClassification"];


            labLoincCode = (String) currentRow["LabLoincCode"];

            labName = (String) currentRow["LabName"];

            labValue = (currentRow["LabValue"] is DBNull) ? 0 :  Convert.ToDecimal (currentRow["LabValue"]);

            
            if (currentRow.Table.Columns.Contains ("ServiceDescription")) { description = (String)currentRow["ServiceDescription"]; }

            else if (currentRow.Table.Columns.Contains ("Description")) { description = (String)currentRow["Description"]; } // BACKWARDS COMPATIBILITY ISSUE

            return;

        }

        virtual public Boolean Save (Mercury.Server.Application application) {

            Boolean success = false;


            try {

                application.EnvironmentDatabase.BeginTransaction ();


                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("MemberServiceDetailSingleton_InsertUpdate");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberServiceId", memberServiceId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@singletonDefinitionId", singletonDefinitionId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@eventDate", eventDate);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@claimId", claimId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@externalClaimId", externalClaimId, Server.Data.DataTypeConstants.UniqueId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@claimLine", claimLine);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberId", memberId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@providerId", providerId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@claimType", ((Int32) claimType).ToString (), Server.Data.DataTypeConstants.TypeName);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@claimStatus", ((Int32) claimStatus).ToString (), Server.Data.DataTypeConstants.TypeName);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@claimDateFrom", claimDateFrom);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@claimDateThru", claimDateThru);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@serviceDateFrom", serviceDateFrom);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@serviceDateThru", serviceDateThru);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@admissionDate", admissionDate);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@dischargeDate", dischargeDate);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@receivedDate", receivedDate);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@paidDate", paidDate);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@billType", billType, Server.Data.DataTypeConstants.BillType);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@principalDiagnosisCode", principalDiagnosisCode, Server.Data.DataTypeConstants.DiagnosisCode);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@principalDiagnosisVersion", principalDiagnosisVersion);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@diagnosisCode", diagnosisCode, Server.Data.DataTypeConstants.DiagnosisCode);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@diagnosisVersion", diagnosisVersion);
                
                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@icd9ProcedureCode", icd9ProcedureCode, Server.Data.DataTypeConstants.Icd9ProcedureCode);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@locationCode", locationCode, Server.Data.DataTypeConstants.LocationCode);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@revenueCode", revenueCode, Server.Data.DataTypeConstants.RevenueCode);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@procedureCode", procedureCode, Server.Data.DataTypeConstants.ProcedureCode);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifierCode", modifierCode, Server.Data.DataTypeConstants.ModifierCode);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@specialtyName", specialtyName, Server.Data.DataTypeConstants.Name);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@isPcpClaim", isPcpClaim);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@ndcCode", ndcCode, Server.Data.DataTypeConstants.NdcCode);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@units", units);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@deaClassification", deaClassification, Server.Data.DataTypeConstants.DeaClassification);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@therapeuticClassification", therapeuticClassification, Server.Data.DataTypeConstants.TherapeuticClassification);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@labLoincCode", labLoincCode, Server.Data.DataTypeConstants.LabLoincCode);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@labName", labName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@labValue", labValue);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@description", description, Server.Data.DataTypeConstants.Name);
                

                // DO NOT EVALUATE THE NUMBER OF ROWS AFFECTED BECAUSE THE CHECK FOR DUPLICATES 

                // IS HANDLED IN THE STORED PROCEDURE, AND THUS COULD RETURN 0 OR 1

                sqlCommand.ExecuteNonQuery ();

                success = true;


                #region Old Method

                //sqlStatement = new StringBuilder ();

                //sqlStatement.Append ("EXEC dbo.MemberServiceDetailSingleton_InsertUpdate ");

                //sqlStatement.Append (memberServiceId.ToString () + ", ");

                //sqlStatement.Append (singletonDefinitionId.ToString () + ", ");

                //sqlStatement.Append ("'" + eventDate.ToString ("MM/dd/yyyy") + "', ");

                //sqlStatement.Append (claimId.ToString () + ", ");

                //sqlStatement.Append ("'" + externalClaimId.Replace ("'", "''") + "', ");

                //sqlStatement.Append (claimLine.ToString () + ", ");

                //sqlStatement.Append (memberId.ToString () + ", ");

                //sqlStatement.Append (providerId.ToString () + ", ");

                //sqlStatement.Append ("'" + claimType + "', ");

                //sqlStatement.Append ("'" + claimStatus + "', ");

                //if (claimDateFrom.HasValue) { sqlStatement.Append ("'" + claimDateFrom.Value.ToString ("MM/dd/yyyy") + "', "); } else { sqlStatement.Append ("NULL, "); }

                //if (claimDateThru.HasValue) { sqlStatement.Append ("'" + claimDateThru.Value.ToString ("MM/dd/yyyy") + "', "); } else { sqlStatement.Append ("NULL, "); }

                //if (serviceDateFrom.HasValue) { sqlStatement.Append ("'" + serviceDateFrom.Value.ToString ("MM/dd/yyyy") + "', "); } else { sqlStatement.Append ("NULL, "); }

                //if (serviceDateThru.HasValue) { sqlStatement.Append ("'" + serviceDateThru.Value.ToString ("MM/dd/yyyy") + "', "); } else { sqlStatement.Append ("NULL, "); }

                //if (admissionDate.HasValue) { sqlStatement.Append ("'" + admissionDate.Value.ToString ("MM/dd/yyyy") + "', "); } else { sqlStatement.Append ("NULL, "); }

                //if (dischargeDate.HasValue) { sqlStatement.Append ("'" + dischargeDate.Value.ToString ("MM/dd/yyyy") + "', "); } else { sqlStatement.Append ("NULL, "); }

                //if (receivedDate.HasValue) { sqlStatement.Append ("'" + receivedDate.Value.ToString ("MM/dd/yyyy") + "', "); } else { sqlStatement.Append ("NULL, "); }

                //if (paidDate.HasValue) { sqlStatement.Append ("'" + paidDate.Value.ToString ("MM/dd/yyyy") + "', "); } else { sqlStatement.Append ("NULL, "); }


                //sqlStatement.Append ("'" + billType + "', ");

                //sqlStatement.Append ("'" + principalDiagnosisCode + "', ");

                //sqlStatement.Append ("'" + diagnosisCode + "', ");

                //sqlStatement.Append ("'" + icd9ProcedureCode + "', ");

                //sqlStatement.Append ("'" + locationCode + "', ");

                //sqlStatement.Append ("'" + revenueCode + "', ");

                //sqlStatement.Append ("'" + procedureCode + "', ");

                //sqlStatement.Append ("'" + modifierCode + "',");

                //sqlStatement.Append ("'" + specialtyName + "',");

                //sqlStatement.Append (Convert.ToInt32 (isPcpClaim).ToString () + ", ");

                //sqlStatement.Append ("'" + ndcCode + "', ");

                //sqlStatement.Append ("" + units.ToString () + ", ");

                //sqlStatement.Append ("'" + deaClassification + "', ");

                //sqlStatement.Append ("'" + therapeuticClassification + "', ");

                //sqlStatement.Append ("'" + labLoincCode + "', ");

                //sqlStatement.Append ("'" + labName + "', ");

                //sqlStatement.Append (labValue.ToString () + ", ");

                //sqlStatement.Append ("'" + description.Replace ("'", "''").Trim () + "' ");

                //success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

                #endregion 


                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }

                success = true;

                application.EnvironmentDatabase.CommitTransaction ();

            }

            catch (Exception applicationException) {

                application.EnvironmentDatabase.RollbackTransaction ();

                application.SetLastException (applicationException);

            }

            return success;

        }

        #endregion

    }

}
