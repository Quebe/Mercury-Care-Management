using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.AuthorizedServices {

    [Serializable]
    public class AuthorizedServiceDefinition : CoreObject {
        
        #region Private Properties

        private Int64 authorizedServiceId;


        private String category = String.Empty;

        private String subcategory = String.Empty;

        private String serviceType = String.Empty;


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

        private String ndcCodeCriteria = String.Empty;

        private String drugNameCriteria = String.Empty;

        private String deaClassificationCriteria = String.Empty;

        private String therapeuticClassificationCriteria = String.Empty;


        private String labLoincCodeCriteria = String.Empty;

        private Boolean enabled = true;

        #endregion


        #region Public Properties

        public Int64 AuthorizedServiceId { get { return authorizedServiceId; } set { authorizedServiceId = value; } }


        public String Category { get { return category; } set { category = Mercury.Server.CommonFunctions.SetValueMaxLength (value, Mercury.Server.Data.DataTypeConstants.DataCriteria); } }

        public String Subcategory { get { return subcategory; } set { subcategory = Mercury.Server.CommonFunctions.SetValueMaxLength (value, Mercury.Server.Data.DataTypeConstants.DataCriteria); } }

        public String ServiceType { get { return serviceType; } set { serviceType = Mercury.Server.CommonFunctions.SetValueMaxLength (value, Mercury.Server.Data.DataTypeConstants.DataCriteria); } }

        public String PrincipalDiagnosisCriteria { get { return principalDiagnosisCriteria; } set { principalDiagnosisCriteria = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public Int32 PrincipalDiagnosisVersion { get { return principalDiagnosisVersion; } set { principalDiagnosisVersion = value; } }

        public String DiagnosisCriteria { get { return diagnosisCriteria; } set { diagnosisCriteria = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public Int32 DiagnosisVersion { get { return diagnosisVersion; } set { diagnosisVersion = value; } }

        public String DrgCriteria { get { return drgCriteria; } set { drgCriteria = Mercury.Server.CommonFunctions.SetValueMaxLength (value, Mercury.Server.Data.DataTypeConstants.DataCriteria); } }

        public String Icd9ProcedureCodeCriteria { get { return icd9ProcedureCodeCriteria; } set { icd9ProcedureCodeCriteria = Mercury.Server.CommonFunctions.SetValueMaxLength (value, Mercury.Server.Data.DataTypeConstants.DataCriteria); } }

        public String BillTypeCriteria { get { return billTypeCriteria; } set { billTypeCriteria = Mercury.Server.CommonFunctions.SetValueMaxLength (value, Mercury.Server.Data.DataTypeConstants.DataCriteria); } }

        public String LocationCodeCriteria { get { return locationCodeCriteria; } set { locationCodeCriteria = Mercury.Server.CommonFunctions.SetValueMaxLength (value, Mercury.Server.Data.DataTypeConstants.DataCriteria); } }

        public String RevenueCodeCriteria { get { return revenueCodeCriteria; } set { revenueCodeCriteria = Mercury.Server.CommonFunctions.SetValueMaxLength (value, Mercury.Server.Data.DataTypeConstants.DataCriteria); } }

        public String ProcedureCodeCriteria { get { return procedureCodeCriteria; } set { procedureCodeCriteria = Mercury.Server.CommonFunctions.SetValueMaxLength (value, Mercury.Server.Data.DataTypeConstants.DataCriteria); } }

        public String ModifierCodeCriteria { get { return modifierCodeCriteria; } set { modifierCodeCriteria = Mercury.Server.CommonFunctions.SetValueMaxLength (value, Mercury.Server.Data.DataTypeConstants.DataCriteria); } }

        public String ProviderSpecialtyCriteria { get { return providerSpecialtyCriteria; } set { providerSpecialtyCriteria = Mercury.Server.CommonFunctions.SetValueMaxLength (value, Mercury.Server.Data.DataTypeConstants.DataCriteria); } }


        public String NdcCodeCriteria { get { return ndcCodeCriteria; } set { ndcCodeCriteria = Mercury.Server.CommonFunctions.SetValueMaxLength (value, Mercury.Server.Data.DataTypeConstants.DataCriteria); } }

        public String DrugNameCriteria { get { return drugNameCriteria; } set { drugNameCriteria = Mercury.Server.CommonFunctions.SetValueMaxLength (value, Mercury.Server.Data.DataTypeConstants.DataCriteria); } }

        public String DeaClassificationCriteria { get { return deaClassificationCriteria; } set { deaClassificationCriteria = Mercury.Server.CommonFunctions.SetValueMaxLength (value, Mercury.Server.Data.DataTypeConstants.DataCriteria); } }

        public String TherapeuticClassificationCriteria { get { return therapeuticClassificationCriteria; } set { therapeuticClassificationCriteria = Mercury.Server.CommonFunctions.SetValueMaxLength (value, Mercury.Server.Data.DataTypeConstants.DataCriteria); } }

        public String LabLoincCodeCriteria { get { return labLoincCodeCriteria; } set { labLoincCodeCriteria = Mercury.Server.CommonFunctions.SetValueMaxLength (value, Mercury.Server.Data.DataTypeConstants.DataCriteria); } }


        public Boolean Enabled { get { return enabled ; } set { enabled = value; } }

        #endregion


        #region Public Properties

        public String CriteriaDescription {

            get {

                StringBuilder criteria = new StringBuilder ();

                if (category.Length != 0) { criteria.Append ("Category: " + category + "\r\n"); }

                if (subcategory.Length != 0) { criteria.Append ("Subcategory: " + subcategory + "\r\n"); }

                if (serviceType.Length != 0) { criteria.Append ("Service Type: " + serviceType + "\r\n"); }

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


                if (ndcCodeCriteria.Length != 0) { criteria.Append ("NDC Codes: " + ndcCodeCriteria + "\r\n"); }

                if (drugNameCriteria.Length != 0) { criteria.Append ("Drug Names: " + drugNameCriteria + "\r\n"); }

                if (deaClassificationCriteria.Length != 0) { criteria.Append ("DEA Class: " + deaClassificationCriteria + "\r\n"); }

                if (therapeuticClassificationCriteria.Length != 0) { criteria.Append ("Therapeutic Class: " + therapeuticClassificationCriteria + "\r\n"); }

                if (labLoincCodeCriteria.Length != 0) { criteria.Append ("Lab LOINC: " + labLoincCodeCriteria + "\r\n"); }


                criteria.Append ("Enabled: " + enabled.ToString ());

                return criteria.ToString ();

            }

        }

        #endregion


        #region Constructors

        public AuthorizedServiceDefinition (Application applicationReference) { base.BaseConstructor (applicationReference); return; }

        public AuthorizedServiceDefinition (Application applicationReference, Server.Application.AuthorizedServiceDefinition serverDefinition) {

            base.BaseConstructor (applicationReference, serverDefinition);


            authorizedServiceId = serverDefinition.AuthorizedServiceId;


            category = serverDefinition.Category;

            subcategory = serverDefinition.Subcategory;

            serviceType = serverDefinition.ServiceType;


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


            ndcCodeCriteria = serverDefinition.NdcCodeCriteria;

            drugNameCriteria = serverDefinition.DrugNameCriteria;

            deaClassificationCriteria = serverDefinition.DeaClassificationCriteria;

            therapeuticClassificationCriteria = serverDefinition.TherapeuticClassificationCriteria;


            labLoincCodeCriteria = serverDefinition.LabLoincCodeCriteria;


            enabled = serverDefinition.Enabled;

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.AuthorizedServiceDefinition serverAuthorizedServiceDefinition) {

            base.MapToServerObject ((Server.Application.CoreObject)serverAuthorizedServiceDefinition);


            serverAuthorizedServiceDefinition.AuthorizedServiceId = authorizedServiceId;


            serverAuthorizedServiceDefinition.Category = category;

            serverAuthorizedServiceDefinition.Subcategory = subcategory;

            serverAuthorizedServiceDefinition.ServiceType = serviceType;

            serverAuthorizedServiceDefinition.PrincipalDiagnosisCriteria = principalDiagnosisCriteria;

            serverAuthorizedServiceDefinition.PrincipalDiagnosisVersion = PrincipalDiagnosisVersion;

            serverAuthorizedServiceDefinition.DiagnosisCriteria = diagnosisCriteria;

            serverAuthorizedServiceDefinition.DiagnosisVersion = DiagnosisVersion;

            serverAuthorizedServiceDefinition.DrgCriteria = drgCriteria;

            serverAuthorizedServiceDefinition.Icd9ProcedureCodeCriteria = icd9ProcedureCodeCriteria;

            serverAuthorizedServiceDefinition.BillTypeCriteria = billTypeCriteria;

            serverAuthorizedServiceDefinition.LocationCodeCriteria = locationCodeCriteria;

            serverAuthorizedServiceDefinition.RevenueCodeCriteria = revenueCodeCriteria;

            serverAuthorizedServiceDefinition.ProcedureCodeCriteria = procedureCodeCriteria;

            serverAuthorizedServiceDefinition.ModifierCodeCriteria = modifierCodeCriteria;

            serverAuthorizedServiceDefinition.ProviderSpecialtyCriteria = providerSpecialtyCriteria;


            serverAuthorizedServiceDefinition.NdcCodeCriteria = ndcCodeCriteria;

            serverAuthorizedServiceDefinition.DrugNameCriteria = drugNameCriteria;

            serverAuthorizedServiceDefinition.DeaClassificationCriteria = deaClassificationCriteria;

            serverAuthorizedServiceDefinition.TherapeuticClassificationCriteria = therapeuticClassificationCriteria;


            serverAuthorizedServiceDefinition.LabLoincCodeCriteria = labLoincCodeCriteria;


            serverAuthorizedServiceDefinition.Enabled = enabled;


            return;

        }

        public override Object ToServerObject () {

            Server.Application.AuthorizedServiceDefinition serverAuthorizedServiceDefinition = new Server.Application.AuthorizedServiceDefinition ();

            MapToServerObject (serverAuthorizedServiceDefinition);

            return serverAuthorizedServiceDefinition;

        }

        public AuthorizedServiceDefinition Copy () {

            Server.Application.AuthorizedServiceDefinition serverAuthorizedServiceDefinition = (Server.Application.AuthorizedServiceDefinition)ToServerObject ();

            AuthorizedServiceDefinition copiedAuthorizedServiceDefinition = new AuthorizedServiceDefinition (application, serverAuthorizedServiceDefinition);

            return copiedAuthorizedServiceDefinition;

        }

        public Boolean IsEqual (AuthorizedServiceDefinition compareDefinition) {

            Boolean isEqual = true;


            if (this.category != compareDefinition.Category) { isEqual = false; }

            if (this.subcategory != compareDefinition.Subcategory) { isEqual = false; }

            if (this.serviceType != compareDefinition.ServiceType) { isEqual = false; }

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


            if (this.modifierCodeCriteria != compareDefinition.ModifierCodeCriteria) { isEqual = false; }

            if (this.providerSpecialtyCriteria != compareDefinition.ProviderSpecialtyCriteria) { isEqual = false; }

            if (this.ndcCodeCriteria != compareDefinition.NdcCodeCriteria) { isEqual = false; }

            if (this.drugNameCriteria != compareDefinition.DrugNameCriteria) { isEqual = false; }

            if (this.deaClassificationCriteria != compareDefinition.DeaClassificationCriteria) { isEqual = false; }

            if (this.therapeuticClassificationCriteria != compareDefinition.TherapeuticClassificationCriteria) { isEqual = false; }


            if (this.labLoincCodeCriteria != compareDefinition.LabLoincCodeCriteria) { isEqual = false; }


            if (this.enabled != compareDefinition.Enabled) { isEqual = false; }

            return isEqual;

        }

        public void SetAuthorizedServiceDefinitionId (Int64 forId) { base.SetId (forId); }

        #endregion 

    }

}
