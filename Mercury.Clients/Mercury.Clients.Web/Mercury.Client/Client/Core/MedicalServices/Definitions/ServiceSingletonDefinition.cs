using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.MedicalServices.Definitions {

    [Serializable]
    public class ServiceSingletonDefinition : CoreObject {

        #region Private Properties

        private Int64 serviceId;

        private Mercury.Server.Application.ServiceDataSourceType dataSourceType = Mercury.Server.Application.ServiceDataSourceType.Custom;

        private Mercury.Server.Application.EventDateOrder eventDateOrder = Mercury.Server.Application.EventDateOrder.ClaimFromDate;

        private String principalDiagnosisCriteria = String.Empty;

        private Int32 principalDiagnosisVersion = 9;

        private String diagnosisCriteria = String.Empty;

        private Int32 diagnosisVersion = 9;

        private String drgCriteria = String.Empty;

        private String icd9ProcedureCodeCriteria = String.Empty;

        private String billTypeCriteria = String.Empty;

        private String locationCodeCriteria = String.Empty;

        private String revenueCodeCriteria = String.Empty;

        private String procedureCodeCriteria = String.Empty;

        private String modifierCodeCriteria = String.Empty;


        private String providerSpecialtyCriteria = String.Empty;

        private Boolean isPcpRequiredCriteria = false;


        private Boolean useMemberAgeCriteria = false;

        private Mercury.Server.Application.DateQualifier memberAgeDateQualifier = Mercury.Server.Application.DateQualifier.Years;

        private Int32 memberAgeMinimum = 0;

        private Int32 memberAgeMaximum = 0;


        private String ndcCodeCriteria = String.Empty;

        private String drugNameCriteria = String.Empty;

        private String deaClassificationCriteria = String.Empty;

        private String therapeuticClassificationCriteria = String.Empty;


        private String labLoincCodeCriteria = String.Empty;

        private String labNameCriteria = String.Empty;

        private String labValueExpressionCriteria = String.Empty;

        private Int64 labMetricId = 0;


        private String customCriteria = String.Empty;

        private Boolean enabled = true;

        #endregion


        #region Public Properties

        public Int64 ServiceSingletonDefinitionId { set { id = value; } }


        public Int64 ServiceId { get { return serviceId; } set { serviceId = value; } }

        public Mercury.Server.Application.ServiceDataSourceType DataSourceType { get { return dataSourceType; } set { dataSourceType = value; } }

        public Mercury.Server.Application.EventDateOrder EventDateOrder { get { return eventDateOrder; } set { eventDateOrder = value; } }

        public String PrincipalDiagnosisCriteria { get { return principalDiagnosisCriteria; } set { principalDiagnosisCriteria = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public Int32 PrincipalDiagnosisVersion { get { return principalDiagnosisVersion; } set { principalDiagnosisVersion = value; } }

        public String DiagnosisCriteria { get { return diagnosisCriteria; } set { diagnosisCriteria = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public Int32 DiagnosisVersion { get { return diagnosisVersion; } set { diagnosisVersion = value; } }

        public String DrgCriteria { get { return drgCriteria; } set { drgCriteria = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String Icd9ProcedureCodeCriteria { get { return icd9ProcedureCodeCriteria; } set { icd9ProcedureCodeCriteria = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String BillTypeCriteria { get { return billTypeCriteria; } set { billTypeCriteria = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String LocationCodeCriteria { get { return locationCodeCriteria; } set { locationCodeCriteria = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String RevenueCodeCriteria { get { return revenueCodeCriteria; } set { revenueCodeCriteria = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String ProcedureCodeCriteria { get { return procedureCodeCriteria; } set { procedureCodeCriteria = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String ModifierCodeCriteria { get { return modifierCodeCriteria; } set { modifierCodeCriteria = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }


        public String ProviderSpecialtyCriteria { get { return providerSpecialtyCriteria; } set { providerSpecialtyCriteria = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public Boolean IsPcpRequiredCriteria { get { return isPcpRequiredCriteria; } set { isPcpRequiredCriteria = value; } }


        public Boolean UseMemberAgeCriteria { get { return useMemberAgeCriteria; } set { useMemberAgeCriteria = value; } }

        public Mercury.Server.Application.DateQualifier MemberAgeDateQualifier { get { return memberAgeDateQualifier; } set { memberAgeDateQualifier = value; } }

        public Int32 MemberAgeMinimum { get { return memberAgeMinimum; } set { memberAgeMinimum = value; } }

        public Int32 MemberAgeMaximum { get { return memberAgeMaximum; } set { memberAgeMaximum = value; } }


        public String NdcCodeCriteria { get { return ndcCodeCriteria; } set { ndcCodeCriteria = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String DrugNameCriteria { get { return drugNameCriteria; } set { drugNameCriteria = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String DeaClassificationCriteria { get { return deaClassificationCriteria; } set { deaClassificationCriteria = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String TherapeuticClassificationCriteria { get { return therapeuticClassificationCriteria; } set { therapeuticClassificationCriteria = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String LabLoincCodeCriteria { get { return labLoincCodeCriteria; } set { labLoincCodeCriteria = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String LabNameCriteria { get { return labNameCriteria; } set { labNameCriteria = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String LabValueExpressionCriteria { get { return labValueExpressionCriteria; } set { labValueExpressionCriteria = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public Int64 LabMetricId { get { return labMetricId; } set { labMetricId = value; } }

        public String CustomCriteria { get { return customCriteria; } set { customCriteria = value; } }

        public Boolean Enabled { get { return enabled ; } set { enabled = value; } }

        #endregion


        #region Constructors

        public ServiceSingletonDefinition () {

            return;

        }

        public ServiceSingletonDefinition (Application applicationReference) {

            base.BaseConstructor (applicationReference);

            return;

        }

        public ServiceSingletonDefinition (Application applicationReference, Mercury.Server.Application.ServiceSingletonDefinition serverDefinition) {

            base.BaseConstructor (applicationReference, serverDefinition);

            
            serviceId = serverDefinition.ServiceId;

            dataSourceType = serverDefinition.DataSourceType;

            eventDateOrder = serverDefinition.EventDateOrder;

            principalDiagnosisCriteria = serverDefinition.PrincipalDiagnosisCriteria;

            PrincipalDiagnosisVersion = serverDefinition.PrincipalDiagnosisVersion;

            diagnosisCriteria = serverDefinition.DiagnosisCriteria;

            DiagnosisVersion = serverDefinition.DiagnosisVersion;

            drgCriteria = serverDefinition.DrgCriteria;

            icd9ProcedureCodeCriteria = serverDefinition.Icd9ProcedureCodeCriteria;

            billTypeCriteria = serverDefinition.BillTypeCriteria;

            locationCodeCriteria = serverDefinition.LocationCodeCriteria;

            revenueCodeCriteria = serverDefinition.RevenueCodeCriteria;

            procedureCodeCriteria = serverDefinition.ProcedureCodeCriteria;

            modifierCodeCriteria = serverDefinition.ModifierCodeCriteria;


            providerSpecialtyCriteria = serverDefinition.ProviderSpecialtyCriteria;

            isPcpRequiredCriteria = serverDefinition.IsPcpRequiredCriteria;


            useMemberAgeCriteria = serverDefinition.UseMemberAgeCriteria;

            memberAgeDateQualifier = serverDefinition.MemberAgeDateQualifier;

            memberAgeMinimum = serverDefinition.MemberAgeMinimum;

            memberAgeMaximum = serverDefinition.MemberAgeMaximum;


            ndcCodeCriteria = serverDefinition.NdcCodeCriteria;

            drugNameCriteria = serverDefinition.DrugNameCriteria;

            deaClassificationCriteria = serverDefinition.DeaClassificationCriteria;

            therapeuticClassificationCriteria = serverDefinition.TherapeuticClassificationCriteria;


            labLoincCodeCriteria = serverDefinition.LabLoincCodeCriteria;

            labNameCriteria = serverDefinition.LabNameCriteria;

            labValueExpressionCriteria = serverDefinition.LabValueExpressionCriteria;

            labMetricId = serverDefinition.LabMetricId;


            customCriteria = serverDefinition.CustomCriteria;

            enabled = serverDefinition.Enabled;

            return;

        }

        #endregion
       

        #region Public Methods

        public virtual void MapToServerObject (Server.Application.ServiceSingletonDefinition serverObject) {

            base.MapToServerObject ((Server.Application.CoreObject)serverObject);

            serverObject.ServiceId = serviceId;

            serverObject.DataSourceType = dataSourceType;

            serverObject.EventDateOrder = eventDateOrder;

            serverObject.PrincipalDiagnosisCriteria = principalDiagnosisCriteria;

            serverObject.PrincipalDiagnosisVersion = PrincipalDiagnosisVersion;

            serverObject.DiagnosisCriteria = diagnosisCriteria;

            serverObject.DiagnosisVersion = DiagnosisVersion;

            serverObject.DrgCriteria = drgCriteria;

            serverObject.Icd9ProcedureCodeCriteria = icd9ProcedureCodeCriteria;

            serverObject.BillTypeCriteria = billTypeCriteria;

            serverObject.LocationCodeCriteria = locationCodeCriteria;

            serverObject.RevenueCodeCriteria = revenueCodeCriteria;

            serverObject.ProcedureCodeCriteria = procedureCodeCriteria;

            serverObject.ModifierCodeCriteria = modifierCodeCriteria;


            serverObject.ProviderSpecialtyCriteria = providerSpecialtyCriteria;

            serverObject.IsPcpRequiredCriteria = isPcpRequiredCriteria;


            serverObject.UseMemberAgeCriteria = useMemberAgeCriteria;

            serverObject.MemberAgeDateQualifier = memberAgeDateQualifier;

            serverObject.MemberAgeMinimum = memberAgeMinimum;

            serverObject.MemberAgeMaximum = MemberAgeMaximum;


            serverObject.NdcCodeCriteria = ndcCodeCriteria;

            serverObject.DrugNameCriteria = drugNameCriteria;

            serverObject.DeaClassificationCriteria = deaClassificationCriteria;

            serverObject.TherapeuticClassificationCriteria = therapeuticClassificationCriteria;


            serverObject.LabLoincCodeCriteria = labLoincCodeCriteria;

            serverObject.LabNameCriteria = labNameCriteria;

            serverObject.LabValueExpressionCriteria = labValueExpressionCriteria;

            serverObject.LabMetricId = labMetricId;


            serverObject.CustomCriteria = customCriteria;

            serverObject.Enabled = enabled;

            
            return;

        }

        public override Object ToServerObject () {

            Server.Application.ServiceSingletonDefinition serverObject = new Server.Application.ServiceSingletonDefinition ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public ServiceSingletonDefinition Copy () {

            Server.Application.ServiceSingletonDefinition serverObject = (Server.Application.ServiceSingletonDefinition)ToServerObject ();

            ServiceSingletonDefinition copiedObject = new ServiceSingletonDefinition (application, serverObject);

            return copiedObject;

        }
        
        #endregion


        #region Public Methods

        public String Criteria () { 

            StringBuilder criteria = new StringBuilder ();

            if (principalDiagnosisCriteria.Length != 0) { criteria.Append ("Principal Diagnosis Codes [" + PrincipalDiagnosisVersion.ToString () + "]: " + principalDiagnosisCriteria + "\r\n"); }

            if (diagnosisCriteria.Length != 0) { criteria.Append ("Diagnosis Codes [" + DiagnosisVersion.ToString () + "]: " + diagnosisCriteria + "\r\n"); }

            if (drgCriteria.Length != 0) { criteria.Append ("DRG: " + drgCriteria + "\r\n"); }

            if (icd9ProcedureCodeCriteria.Length != 0) { criteria.Append ("ICD-9: " + icd9ProcedureCodeCriteria + "\r\n"); }

            if (billTypeCriteria.Length != 0) { criteria.Append ("Bill Types: " + billTypeCriteria + "\r\n"); }

            if (locationCodeCriteria.Length != 0) { criteria.Append ("Locations: " + locationCodeCriteria + "\r\n"); }

            if (revenueCodeCriteria.Length != 0) { criteria.Append ("Revenue Codes: " + revenueCodeCriteria + "\r\n"); }

            if (procedureCodeCriteria.Length != 0) { criteria.Append ("Procedure Codes: " + procedureCodeCriteria + "\r\n"); }

            if (modifierCodeCriteria.Length != 0) { criteria.Append ("Modifier Codes: " + modifierCodeCriteria + "\r\n"); }


            if (providerSpecialtyCriteria.Length != 0) { criteria.Append ("Provider Specialties: " + providerSpecialtyCriteria + "\r\n"); }

            if (isPcpRequiredCriteria) { criteria.Append ("PCP Claim Required\r\n"); }


            if (useMemberAgeCriteria) { criteria.Append ("Member Age (in " + memberAgeDateQualifier.ToString () + ") between " + memberAgeMinimum.ToString () + " and " + memberAgeMaximum.ToString () + "\r\n"); }


            if (ndcCodeCriteria.Length != 0) { criteria.Append ("NDC Codes: " + ndcCodeCriteria + "\r\n"); }

            if (drugNameCriteria.Length != 0) { criteria.Append ("Drug Names: " + drugNameCriteria + "\r\n"); }

            if (deaClassificationCriteria.Length != 0) { criteria.Append ("DEA Class: " + deaClassificationCriteria + "\r\n"); }

            if (therapeuticClassificationCriteria.Length != 0) { criteria.Append ("Therapeutic Class: " + therapeuticClassificationCriteria + "\r\n"); }

            if (labLoincCodeCriteria.Length != 0) { criteria.Append ("Lab LOINC: " + labLoincCodeCriteria + "\r\n"); }

            if (labNameCriteria.Length != 0) { criteria.Append ("Lab Name: " + labNameCriteria + "\r\n"); }

            if (labValueExpressionCriteria.Length != 0) { criteria.Append ("Lab Value Expression: " + labValueExpressionCriteria + "\r\n"); }

            if (labMetricId != 0) { criteria.Append ("Lab Metric: " + labMetricId.ToString () + "\r\n"); }


            if (customCriteria.Length != 0) { criteria.Append ("Custom Criteria: " + customCriteria + "\r\n"); }

            criteria.Append ("Event Date Order: " + eventDateOrder.ToString () + "\r\n");

            criteria.Append ("Enabled: " + enabled.ToString ());

            return criteria.ToString ();

        }

        public Boolean IsEqual (ServiceSingletonDefinition compareDefinition) {

            Boolean isEqual = true;

            if (this.dataSourceType != compareDefinition.DataSourceType) { isEqual = false; }

            if (this.eventDateOrder != compareDefinition.EventDateOrder) { isEqual = false; }

            if (this.principalDiagnosisCriteria != compareDefinition.PrincipalDiagnosisCriteria) { isEqual = false; }

            isEqual &= (PrincipalDiagnosisVersion == compareDefinition.PrincipalDiagnosisVersion);

            if (this.diagnosisCriteria != compareDefinition.DiagnosisCriteria) { isEqual = false; }

            isEqual &= (DiagnosisVersion == compareDefinition.DiagnosisVersion);

            if (this.drgCriteria != compareDefinition.DrgCriteria) { isEqual = false; }

            if (this.icd9ProcedureCodeCriteria != compareDefinition.Icd9ProcedureCodeCriteria) { isEqual = false; }

            if (this.billTypeCriteria != compareDefinition.BillTypeCriteria) { isEqual = false; }

            if (this.locationCodeCriteria != compareDefinition.LocationCodeCriteria) { isEqual = false; }

            if (this.revenueCodeCriteria != compareDefinition.RevenueCodeCriteria) { isEqual = false; }

            if (this.procedureCodeCriteria != compareDefinition.ProcedureCodeCriteria) { isEqual = false; }

            if (this.isPcpRequiredCriteria != compareDefinition.IsPcpRequiredCriteria) { isEqual = false; }


            isEqual &= (useMemberAgeCriteria == compareDefinition.UseMemberAgeCriteria);

            isEqual &= (memberAgeDateQualifier == compareDefinition.MemberAgeDateQualifier);

            isEqual &= (memberAgeMinimum == compareDefinition.MemberAgeMinimum);

            isEqual &= (memberAgeMaximum == compareDefinition.MemberAgeMaximum);
                

            if (this.modifierCodeCriteria != compareDefinition.ModifierCodeCriteria) { isEqual = false; }

            if (this.providerSpecialtyCriteria != compareDefinition.ProviderSpecialtyCriteria) { isEqual = false; }

            if (this.ndcCodeCriteria != compareDefinition.NdcCodeCriteria) { isEqual = false; }

            if (this.drugNameCriteria != compareDefinition.DrugNameCriteria) { isEqual = false; }

            if (this.deaClassificationCriteria != compareDefinition.DeaClassificationCriteria) { isEqual = false; }

            if (this.therapeuticClassificationCriteria != compareDefinition.TherapeuticClassificationCriteria) { isEqual = false; }


            if (this.labLoincCodeCriteria != compareDefinition.LabLoincCodeCriteria) { isEqual = false; }

            if (this.labNameCriteria != compareDefinition.LabNameCriteria) { isEqual = false; }

            if (this.labValueExpressionCriteria != compareDefinition.LabValueExpressionCriteria) { isEqual = false; }

            if (this.labMetricId != compareDefinition.LabMetricId) { isEqual = false; }

           
            if (this.customCriteria != compareDefinition.CustomCriteria) { isEqual = false; }

            if (this.enabled != compareDefinition.Enabled) { isEqual = false; }

            return isEqual;

        }

        #endregion

    }

}
