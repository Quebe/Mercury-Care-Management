using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Authorizations {

    [DataContract (Name = "Authorization")]
    public class Authorization : CoreObject {

        #region Private Properties

        [DataMember (Name = "AuthorizationNumber")]
        private String authorizationNumber;

        [DataMember (Name = "MemberId")]
        private Int64 memberId;

        [DataMember (Name = "ReferringProviderId")]
        private Int64 referringProviderId;

        [DataMember (Name = "ReferringProviderName")]
        private String referringProviderName;

        [DataMember (Name = "ServiceProviderId")]
        private Int64 serviceProviderId;

        [DataMember (Name = "ServiceProviderName")]
        private String serviceProviderName;

        [DataMember (Name = "ServiceProviderSpecialtyName")]
        private String serviceProviderSpecialtyName;


        [DataMember (Name = "AuthorizationCategory")]
        private String authorizationCategory = String.Empty;

        [DataMember (Name = "AuthorizationSubcategory")]
        private String authorizationSubcategory = String.Empty;

        [DataMember (Name = "AuthorizationServiceType")]
        private String authorizationServiceType = String.Empty;


        [DataMember (Name = "ReceivedDate")]
        private DateTime receivedDate;

        [DataMember (Name = "ReferralDate")]
        private DateTime referralDate;


        [DataMember (Name = "EffectiveDate")]
        private DateTime effectiveDate;

        [DataMember (Name = "TerminationDate")]
        private DateTime terminationDate;


        [DataMember (Name = "AuthorizationStatus")]
        private String authorizationStatus = String.Empty;


        [DataMember (Name = "PrincipalDiagnosisCode")]
        private String principalDiagnosisCode = String.Empty;

        [DataMember (Name = "PrincipalDiagnosisVersion")]
        private Int32 principalDiagnosisVersion = 9;

        [DataMember (Name = "PrincipalDiagnosisDescription")]
        private String principalDiagnosisDescription = String.Empty;

        [DataMember (Name = "AdmittingDiagnosisCode")]
        private String admittingDiagnosisCode = String.Empty;

        [DataMember (Name = "AdmittingDiagnosisVersion")]
        private Int32 admittingDiagnosisVersion = 9;

        [DataMember (Name = "AdmittingDiagnosisDescription")]
        private String admittingDiagnosisDescription = String.Empty;

        [DataMember (Name = "DischargeDiagnosisCode")]
        private String dischargeDiagnosisCode = String.Empty;

        [DataMember (Name = "DischargeDiagnosisVersion")]
        private Int32 dischargeDiagnosisVersion = 9;

        [DataMember (Name = "DischargeDiagnosisDescription")]
        private String dischargeDiagnosisDescription = String.Empty;


        [DataMember (Name = "AccidentDate")]
        private DateTime? accidentDate = null;

        [DataMember (Name = "LastMenstrualDate")]
        private DateTime? lastMenstrualDate = null;

        [DataMember (Name = "EstimatedBirthDate")]
        private DateTime? estimatedBirthDate = null;

        [DataMember (Name = "OnsetDate")]
        private DateTime? onsetDate = null;

        [DataMember (Name = "AssignedToUserAccountName")]
        private String assignedToUserAccountName;

        #endregion


        #region Public Properties

        public Int64 MemberId { get { return memberId; } set { memberId = value; } }

        public Int64 ReferringProviderId { get { return referringProviderId; } set { referringProviderId = value; } }

        public Int64 ServiceProviderId { get { return serviceProviderId; } set { serviceProviderId = value; } }

        public String ServiceProviderSpecialtyName { get { return serviceProviderSpecialtyName; } set { serviceProviderSpecialtyName = value; } }


        public String AuthorizationCategory { get { return authorizationCategory; } set { authorizationCategory = value; } }

        public String AuthorizationSubcategory { get { return authorizationSubcategory; } set { authorizationSubcategory = value; } }
        
        public String AuthorizationServiceType { get { return authorizationServiceType; } set { authorizationServiceType = value; } }


        public DateTime ReceivedDate { get { return receivedDate; } set { receivedDate = value; } }

        public DateTime ReferralDate { get { return referralDate; } set { referralDate = value; } }

        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }

        public String AuthorizationStatus { get { return authorizationStatus; } set { authorizationStatus = value; } }


        public String PrincipalDiagnosisCode { get { return principalDiagnosisCode; } set { principalDiagnosisCode = value; } }

        public Int32 PrincipalDiagnosisVersion { get { return principalDiagnosisVersion; } set { principalDiagnosisVersion = value; } }

        public String PrincipalDiagnosisDescription { get { return principalDiagnosisDescription; } set { principalDiagnosisDescription = value; } }

        public String AdmittingDiagnosisCode { get { return admittingDiagnosisCode; } set { admittingDiagnosisCode = value; } }

        public Int32 AdmittingDiagnosisVersion { get { return admittingDiagnosisVersion; } set { admittingDiagnosisVersion = value; } }

        public String AdmittingDiagnosisDescription { get { return admittingDiagnosisDescription; } set { admittingDiagnosisDescription = value; } }

        public String DischargeDiagnosisCode { get { return dischargeDiagnosisCode; } set { dischargeDiagnosisCode = value; } }

        public Int32 DischargeDiagnosisVersion { get { return dischargeDiagnosisVersion; } set { dischargeDiagnosisVersion = value; } }

        public String DischargeDiagnosisDescription { get { return dischargeDiagnosisDescription; } set { dischargeDiagnosisDescription = value; } }


        public DateTime? AccidentDate { get { return accidentDate; } set { accidentDate = value; } }

        public DateTime? LastMenstrualDate { get { return lastMenstrualDate; } set { lastMenstrualDate = value; } }

        public DateTime? EstimatedBirthDate { get { return estimatedBirthDate; } set { estimatedBirthDate = value; } }

        public DateTime? OnsetDate { get { return onsetDate; } set { onsetDate = value; } }


        public String AssignedToUserAccountName { get { return assignedToUserAccountName; } set { assignedToUserAccountName = value; } }

        #endregion


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);
            

            authorizationNumber = (String) currentRow["AuthorizationNumber"];

            memberId = (Int64) currentRow["MemberId"];

            referringProviderId = (Int64) currentRow["ReferringProviderId"];

            referringProviderName = (String) currentRow["ReferringProviderName"];

            serviceProviderId = (Int64) currentRow["ServiceProviderId"];

            serviceProviderName = (String) currentRow["ServiceProviderName"];

            serviceProviderSpecialtyName = (String) currentRow["ServiceProviderSpecialtyName"];


            authorizationCategory = (String) currentRow["AuthorizationCategory"];

            authorizationSubcategory = (String) currentRow["AuthorizationSubcategory"];
            
            authorizationServiceType = (String) currentRow["AuthorizationServiceType"];


            receivedDate = (DateTime) currentRow["ReceivedDate"];

            referralDate = (DateTime) currentRow["ReferralDate"];

            effectiveDate = (DateTime) currentRow["EffectiveDate"];

            terminationDate = (DateTime) currentRow["TerminationDate"];

            authorizationStatus = (String) currentRow["AuthorizationStatus"];


            principalDiagnosisCode = (String) currentRow["PrincipalDiagnosisCode"];

            principalDiagnosisVersion = Convert.ToInt32 (currentRow["PrincipalDiagnosisVersion"]);

            principalDiagnosisDescription = (String) currentRow["PrincipalDiagnosisDescription"];

            admittingDiagnosisCode = (String) currentRow["AdmittingDiagnosisCode"];

            admittingDiagnosisVersion = Convert.ToInt32 (currentRow["AdmittingDiagnosisVersion"]);

            admittingDiagnosisDescription = (String)currentRow["AdmittingDiagnosisDescription"];

            dischargeDiagnosisCode = (String) currentRow["DischargeDiagnosisCode"];

            dischargeDiagnosisVersion = Convert.ToInt32 (currentRow["DischargeDiagnosisVersion"]);

            dischargeDiagnosisDescription = (String)currentRow["DischargeDiagnosisDescription"];


            AccidentDate = base.DateTimeFromSql (currentRow, "AccidentDate");

            LastMenstrualDate = base.DateTimeFromSql (currentRow, "LastMenstrualDate");

            EstimatedBirthDate = base.DateTimeFromSql (currentRow, "EstimatedBirthDate");

            OnsetDate = base.DateTimeFromSql (currentRow, "OnsetDate");


            assignedToUserAccountName = (String) currentRow["AssignedToUserAccountName"];

            return;

        }

        #endregion

    }

}
