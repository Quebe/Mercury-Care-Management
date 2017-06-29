using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.AuthorizedServices {

    [DataContract (Name = "MemberAuthorizedServiceDetail")]
    public class MemberAuthorizedServiceDetail {
        
        #region Private Properties
      
        [DataMember (Name = "MemberAuthorizedServiceId")]
        private Int64 memberAuthorizedServiceId;

        [DataMember (Name = "AuthorizedServiceDefinitionId")]
        private Int64 authorizedServiceDefinitionId;

        [DataMember (Name = "EventDate")]
        private DateTime eventDate;

        [DataMember (Name = "AuthorizationId")]
        private Int64 authorizationId;

        [DataMember (Name = "AuthorizationNumber")]
        private String authorizationNumber;

        [DataMember (Name = "ExternalAuthorizationId")]
        private String externalAuthorizationId;

        [DataMember (Name = "AuthorizationLine")]
        private Int32 authorizationLine;

        [DataMember (Name = "MemberId")]
        private Int64 memberId;
        
        [DataMember (Name = "ReferringProviderId")]
        private Int64 referringProviderId;

        [DataMember (Name = "ServiceProviderId")]
        private Int64 serviceProviderId;

        [DataMember (Name = "AuthorizationCategory")]
        private String authorizationCategory;

        [DataMember (Name = "AuthorizationSubcategory")]
        private String authorizationSubcategory;

        [DataMember (Name = "AuthorizationServiceType")]
        private String authorizationServiceType;
        
        [DataMember (Name = "AuthorizationStatus")]
        private String authorizationStatus;

        [DataMember (Name = "ReceivedDate")]
        private DateTime? receivedDate;

        [DataMember (Name = "ReferralDate")]
        private DateTime? referralDate;

        [DataMember (Name = "EffectiveDate")]
        private DateTime effectiveDate;

        [DataMember (Name = "TerminationDate")]
        private DateTime terminationDate;

        [DataMember (Name = "ServiceDate")]
        private DateTime? serviceDate;

        [DataMember (Name = "PrincipalDiagnosisCode")]
        private String principalDiagnosisCode;

        [DataMember (Name = "PrincipalDiagnosisVersion")]
        private Int32 principalDiagnosisVersion = 9;

        [DataMember (Name = "DiagnosisCode")]
        private String diagnosisCode;

        [DataMember (Name = "DiagnosisVersion")]
        private Int32 diagnosisVersion = 9;

        [DataMember (Name = "RevenueCode")]
        private String revenueCode;

        [DataMember (Name = "ProcedureCode")]
        private String procedureCode;

        [DataMember (Name = "ModifierCode")]
        private String modifierCode;

        [DataMember (Name = "SpecialtyName")]
        private String specialtyName;

        [DataMember (Name = "NdcCode")]
        private String ndcCode;

        [DataMember (Name = "Description")]
        private String description = String.Empty;

        #endregion


        #region Public Properties

        public Int64 MemberAuthorizedServiceId { get { return memberAuthorizedServiceId; } set { memberAuthorizedServiceId = value; } }

        public Int64 AuthorizedServiceDefinitionId { get { return authorizedServiceDefinitionId; } set { authorizedServiceDefinitionId = value; } }

        public DateTime EventDate { get { return eventDate; } set { eventDate = value; } }

        public Int64 AuthorizationId { get { return authorizationId; } set { authorizationId = value; } }

        public String AuthorizationNumber { get { return authorizationNumber; } set { AuthorizationNumber = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.UniqueId); } }

        public String ExternalAuthorizationId { get { return externalAuthorizationId; } set { externalAuthorizationId = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.UniqueId); } }

        public Int32 AuthorizationLine { get { return authorizationLine; } set { authorizationLine = value; } }

        public Int64 MemberId { get { return memberId; } set { memberId = value; } }
        
        public Int64 ReferringProviderId { get { return referringProviderId; } set { referringProviderId = value; } }

        public Int64 ServiceProviderId { get { return serviceProviderId; } set { serviceProviderId = value; } }

        public String AuthorizationCategory { get { return authorizationCategory; } set { authorizationCategory = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public String AuthorizationSubcategory { get { return authorizationSubcategory; } set { authorizationSubcategory = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public String AuthorizationServiceType { get { return authorizationServiceType; } set { authorizationServiceType = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public String AuthorizationStatus { get { return authorizationStatus; } set { authorizationStatus = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.TypeName); } }

        public DateTime? ReceivedDate { get { return receivedDate; } set { receivedDate = value; } }

        public DateTime? ReferralDate { get { return referralDate; } set { referralDate = value; } }

        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }

        public DateTime? ServiceDate { get { return serviceDate; } set { serviceDate = value; } }

        public String PrincipalDiagnosisCode { get { return principalDiagnosisCode; } set { principalDiagnosisCode = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.DiagnosisCode); } }

        public Int32 PrincipalDiagnosisVersion { get { return principalDiagnosisVersion; } set { principalDiagnosisVersion = value; } }

        public String DiagnosisCode { get { return diagnosisCode; } set { diagnosisCode = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.DiagnosisCode); } }

        public Int32 DiagnosisVersion { get { return diagnosisVersion; } set { diagnosisVersion = value; } }

        public String RevenueCode { get { return revenueCode; } set { revenueCode = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.RevenueCode); } }

        public String ProcedureCode { get { return procedureCode; } set { procedureCode = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.ProcedureCode); } }

        public String ModifierCode { get { return modifierCode; } set { modifierCode = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.ModifierCode); } }

        public String SpecialtyName { get { return specialtyName; } set { specialtyName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public String NdcCode { get { return ndcCode; } set { ndcCode = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.NdcCode); } }

        public String Description { get { return description; } set { description = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        #endregion


        #region Constructors 

        public MemberAuthorizedServiceDetail (Int64 forMemberAuthorizedServiceId, Int64 forAuthorizedServiceDefinitionId) {

            memberAuthorizedServiceId = forMemberAuthorizedServiceId;

            authorizedServiceDefinitionId = forAuthorizedServiceDefinitionId;

            return;

        }

        #endregion 


        #region Public Methods

        public void MapDataFields (System.Data.DataRow currentRow) {

            memberAuthorizedServiceId = (Int64) currentRow["MemberAuthorizedServiceId"];

            authorizedServiceDefinitionId = (Int64) currentRow["AuthorizedServiceDefinitionId"];

            eventDate = (DateTime) currentRow["EventDate"];

            authorizationId = (Int64) currentRow ["AuthorizationId"];

            authorizationNumber = (String) currentRow["AuthorizationNumber"];

            externalAuthorizationId = (String) currentRow ["ExternalAuthorizationId"];

            authorizationLine = (Int32) currentRow ["AuthorizationLine"];

            memberId = (Int64) currentRow ["MemberId"];

            referringProviderId = (Int64) currentRow["ReferringProviderId"];

            serviceProviderId = (Int64) currentRow ["ServiceProviderId"];

            authorizationCategory = (String) currentRow ["Category"];

            authorizationSubcategory = (String) currentRow["Subcategory"];

            authorizationServiceType = (String) currentRow["ServiceType"];

            authorizationStatus = (String) currentRow ["Status"];


            receivedDate = (currentRow ["ReceivedDate"] is System.DBNull) ? null : (DateTime?) currentRow ["ReceivedDate"];

            referralDate = (currentRow["ReferralDate"] is System.DBNull) ? null : (DateTime?) currentRow["ReferralDate"];

            effectiveDate = (DateTime) currentRow["EffectiveDate"];

            terminationDate = (DateTime) currentRow["TerminationDate"];

            serviceDate = (currentRow["ServiceDate"] is System.DBNull) ? null : (DateTime?) currentRow["ServiceDate"];


            principalDiagnosisCode = (String) currentRow["PrincipalDiagnosisCode"];

            principalDiagnosisVersion = Convert.ToInt32 (currentRow["PrincipalDiagnosisVersion"]);

            diagnosisCode = (String)currentRow["DiagnosisCode"];

            diagnosisVersion = Convert.ToInt32 (currentRow["DiagnosisVersion"]);

            revenueCode = (String) currentRow ["RevenueCode"];

            procedureCode = (String) currentRow ["ProcedureCode"];

            modifierCode = (String) currentRow ["ModifierCode"];

            ndcCode = (String) currentRow["NdcCode"];

            specialtyName = (String) currentRow["SpecialtyName"];

            description = (String) currentRow["Description"];

            return;

        }

        virtual public Boolean Save (Mercury.Server.Application application) {

            Boolean success = false;

            
            try {

                application.EnvironmentDatabase.BeginTransaction ();



                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("MemberAuthorizedServiceDetail_InsertUpdate");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberAuthorizedServiceId", memberAuthorizedServiceId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@authorizedServiceDefinitionId", authorizedServiceDefinitionId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@eventDate", eventDate);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@authorizationId", authorizationId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@authorizationNumber", authorizationNumber, Server.Data.DataTypeConstants.UniqueId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@externalAuthorizationId", externalAuthorizationId, Server.Data.DataTypeConstants.UniqueId);



                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@authorizationLine", authorizationLine);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberId", memberId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@referringProviderId", referringProviderId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@serviceProviderId", serviceProviderId);

                
                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@category", authorizationCategory, Server.Data.DataTypeConstants.TypeName);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@subcategory", authorizationSubcategory, Server.Data.DataTypeConstants.TypeName);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@serviceType", authorizationServiceType, Server.Data.DataTypeConstants.TypeName);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@status", authorizationStatus, Server.Data.DataTypeConstants.TypeName);



                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@receivedDate", receivedDate);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@referralDate", referralDate);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@effectiveDate", effectiveDate);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@terminationDate", terminationDate);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@serviceDate", serviceDate);

                
                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@principalDiagnosisCode", principalDiagnosisCode, Server.Data.DataTypeConstants.DiagnosisCode);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@principalDiagnosisVersion", principalDiagnosisVersion);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@diagnosisCode", diagnosisCode, Server.Data.DataTypeConstants.DiagnosisCode);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@diagnosisVersion", diagnosisVersion);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@revenueCode", revenueCode, Server.Data.DataTypeConstants.RevenueCode);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@procedureCode", procedureCode, Server.Data.DataTypeConstants.ProcedureCode);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifierCode", modifierCode, Server.Data.DataTypeConstants.ModifierCode);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@specialtyName", specialtyName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@ndcCode", ndcCode, Server.Data.DataTypeConstants.NdcCode);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@description", description, Server.Data.DataTypeConstants.Name);


                // DO NOT EVALUATE THE NUMBER OF ROWS AFFECTED BECAUSE THE CHECK FOR DUPLICATES 

                // IS HANDLED IN THE STORED PROCEDURE, AND THUS COULD RETURN 0 OR 1

                sqlCommand.ExecuteNonQuery ();

                success = true;



                #region Old Method

                //sqlStatement = new StringBuilder ();

                //sqlStatement.Append ("EXEC dbo.MemberAuthorizedServiceDetail_InsertUpdate ");

                //sqlStatement.Append (memberAuthorizedServiceId.ToString () + ", ");

                //sqlStatement.Append (authorizedServiceDefinitionId.ToString () + ", ");

                //sqlStatement.Append ("'" + eventDate.ToString ("MM/dd/yyyy") + "', ");

                //sqlStatement.Append (authorizationId.ToString () + ", ");

                //sqlStatement.Append ("'" + authorizationNumber.Replace ("'", "''") + "', ");

                //sqlStatement.Append ("'" + externalAuthorizationId.Replace ("'", "''") + "', ");

                //sqlStatement.Append (authorizationLine.ToString () + ", ");

                //sqlStatement.Append (memberId.ToString () + ", ");

                //sqlStatement.Append (referringProviderId.ToString () + ", ");

                //sqlStatement.Append (serviceProviderId.ToString () + ", ");

                //sqlStatement.Append ("'" + authorizationCategory.Replace ("'", "''") + "', ");

                //sqlStatement.Append ("'" + authorizationSubcategory.Replace ("'", "''") + "', ");

                //sqlStatement.Append ("'" + authorizationServiceType.Replace ("'", "''") + "', ");

                //sqlStatement.Append ("'" + authorizationStatus + "', ");

                //if (receivedDate.HasValue) { sqlStatement.Append ("'" + receivedDate.Value.ToString ("MM/dd/yyyy") + "', "); } else { sqlStatement.Append ("NULL, "); }

                //if (referralDate.HasValue) { sqlStatement.Append ("'" + referralDate.Value.ToString ("MM/dd/yyyy") + "', "); } else { sqlStatement.Append ("NULL, "); }

                //sqlStatement.Append ("'" + effectiveDate.ToString ("MM/dd/yyyy") + "', ");

                //sqlStatement.Append ("'" + terminationDate.ToString ("MM/dd/yyyy") + "', ");

                //if (serviceDate.HasValue) { sqlStatement.Append ("'" + serviceDate.Value.ToString ("MM/dd/yyyy") + "', "); } else { sqlStatement.Append ("NULL, "); }


                //sqlStatement.Append ("'" + principalDiagnosisCode + "', ");

                //sqlStatement.Append (principalDiagnosisVersion.ToString () + ", ");

                //sqlStatement.Append ("'" + diagnosisCode + "', ");

                //sqlStatement.Append (diagnosisVersion.ToString () + ", ");

                //sqlStatement.Append ("'" + revenueCode + "', ");

                //sqlStatement.Append ("'" + procedureCode + "', ");

                //sqlStatement.Append ("'" + modifierCode + "',");

                //sqlStatement.Append ("'" + specialtyName + "',");

                //sqlStatement.Append ("'" + ndcCode + "', ");

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

