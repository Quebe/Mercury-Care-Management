using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.AuthorizedServices {

    [DataContract (Name = "AuthorizedServiceDefinition")]
    public class AuthorizedServiceDefinition : CoreObject {
        
        #region Private Properties
        
        [DataMember (Name = "AuthorizedServiceId")]
        private Int64 authorizedServiceId;

        [DataMember (Name = "Category")]
        private String category = String.Empty;

        [DataMember (Name = "Subcategory")]
        private String subcategory= String.Empty;

        [DataMember (Name = "ServiceType")]
        private String serviceType = String.Empty;

        [DataMember (Name = "PrincipalDiagnosisCriteria")]
        private String principalDiagnosisCriteria = String.Empty;

        [DataMember (Name = "PrincipalDiagnosisVersion")]
        private Int32 principalDiagnosisVersion = 9;

        [DataMember (Name = "DiagnosisCriteria")]
        private String diagnosisCriteria = String.Empty;

        [DataMember (Name = "DiagnosisVersion")]
        private Int32 diagnosisVersion = 9;

        [DataMember (Name = "DrgCriteria")]
        private String drgCriteria = String.Empty;

        [DataMember (Name = "Icd9ProcedureCodeCriteria")]
        private String icd9ProcedureCodeCriteria = String.Empty;

        [DataMember (Name = "BillTypeCriteria")]
        private String billTypeCriteria = String.Empty;

        [DataMember (Name = "LocationCodeCriteria")]
        private String locationCodeCriteria = String.Empty;

        [DataMember (Name = "RevenueCodeCriteria")]
        private String revenueCodeCriteria = String.Empty;

        [DataMember (Name = "ProcedureCodeCriteria")]
        private String procedureCodeCriteria = String.Empty;

        [DataMember (Name = "ModifierCodeCriteria")]
        private String modifierCodeCriteria = String.Empty;

        [DataMember (Name = "ProviderSpecialtyCriteria")]
        private String providerSpecialtyCriteria = String.Empty;

        [DataMember (Name = "NdcCodeCriteria")]
        private String ndcCodeCriteria = String.Empty;

        [DataMember (Name = "DrugNameCriteria")]
        private String drugNameCriteria = String.Empty;

        [DataMember (Name = "DeaClassificationCriteria")]
        private String deaClassificationCriteria = String.Empty;

        [DataMember (Name = "TherapeuticClassificationCriteria")]
        private String therapeuticClassificationCriteria = String.Empty;

        [DataMember (Name = "LabLoincCodeCriteria")]
        private String labLoincCodeCriteria = String.Empty;

        [DataMember (Name = "Enabled")]
        private Boolean enabled = true;

        private String validationMessage = String.Empty;

        #endregion


        #region Public Properties
        
        public Int64 AuthorizedServiceId { get { return authorizedServiceId; } set { authorizedServiceId = value; } }


        public String Category { get { return category; } set { category = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String Subcategory { get { return subcategory; } set { subcategory = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String ServiceType { get { return serviceType; } set { serviceType = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }


        public String PrincipalDiagnosisCriteria { get { return principalDiagnosisCriteria; } set { principalDiagnosisCriteria = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public Int32 PrincipalDiagnosisVersion { get { return principalDiagnosisVersion; } set { principalDiagnosisVersion = value; } }

        public String DiagnosisCriteria { get { return diagnosisCriteria; } set { diagnosisCriteria = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public Int32 DiagnosisVersion { get { return diagnosisVersion; } set { diagnosisVersion = value; } }

        public String DrgCriteria { get { return drgCriteria; } set { drgCriteria = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String Icd9ProcedureCodeCriteria { get { return icd9ProcedureCodeCriteria; } set { icd9ProcedureCodeCriteria = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String BillTypeCriteria { get { return billTypeCriteria; } set { billTypeCriteria = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String LocationCodeCriteria { get { return locationCodeCriteria; } set { locationCodeCriteria = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String RevenueCodeCriteria { get { return revenueCodeCriteria; } set { revenueCodeCriteria = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String ProcedureCodeCriteria { get { return procedureCodeCriteria; } set { procedureCodeCriteria = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String ModifierCodeCriteria { get { return modifierCodeCriteria; } set { modifierCodeCriteria = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String ProviderSpecialtyCriteria { get { return providerSpecialtyCriteria; } set { providerSpecialtyCriteria = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String NdcCodeCriteria { get { return ndcCodeCriteria; } set { ndcCodeCriteria = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String DrugNameCriteria { get { return drugNameCriteria; } set { drugNameCriteria = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String DeaClassificationCriteria { get { return deaClassificationCriteria; } set { deaClassificationCriteria = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String TherapeuticClassificationCriteria { get { return therapeuticClassificationCriteria; } set { therapeuticClassificationCriteria = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String LabLoincCodeCriteria { get { return labLoincCodeCriteria; } set { labLoincCodeCriteria = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public Boolean Enabled { get { return enabled; } set { enabled = value; } }

        public String LastValidationMessage { get { return validationMessage; } }

        #endregion


        #region Constructors 

        public AuthorizedServiceDefinition (Application application) { base.BaseConstructor (application); }

        #endregion


        #region XML Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];


            #region Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "AuthorizedServiceId", AuthorizedServiceId.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "Category", Category);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "Subcategory", Subcategory);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ServiceType", ServiceType);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "PrincipalDiagnosisCriteria", PrincipalDiagnosisCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "PrincipalDiagnosisVersion", PrincipalDiagnosisVersion.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "DiagnosisCriteria", DiagnosisCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "DiagnosisVersion", DiagnosisVersion.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "DrgCriteria", DrgCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "Icd9ProcedureCodeCriteria", Icd9ProcedureCodeCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "BillTypeCriteria", BillTypeCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "LocationCodeCriteria", LocationCodeCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "RevenueCodeCriteria", RevenueCodeCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ProcedureCodeCriteria", ProcedureCodeCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ModifierCodeCriteria", ModifierCodeCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ProviderSpecialtyCriteria", ProviderSpecialtyCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "NdcCodeCriteria", NdcCodeCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "DrugNameCriteria", DrugNameCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "TherapeuticClassificationCriteria", TherapeuticClassificationCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "LabLoincCodeCriteria", LabLoincCodeCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "Enabled", Enabled.ToString ());

            #endregion


            return document;

        }

        public override List<ImportExport.Result> XmlImport (System.Xml.XmlNode objectNode) {

            List<ImportExport.Result> response = base.XmlImport (objectNode);

            System.Xml.XmlNode propertiesNode;


            try {

                propertiesNode = objectNode.SelectSingleNode ("Properties");

                foreach (System.Xml.XmlNode currentPropertyNode in propertiesNode) {

                    switch (currentPropertyNode.Attributes["Name"].InnerText) {

                        case "Category": Category = currentPropertyNode.InnerText; break;

                        case "Subcategory": Subcategory = currentPropertyNode.InnerText; break;

                        case "ServiceType": ServiceType = currentPropertyNode.InnerText; break;

                        case "PrincipalDiagnosisCriteria": principalDiagnosisCriteria = currentPropertyNode.InnerText; break;

                        case "PrincipalDiagnosisVersion": principalDiagnosisVersion = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                        case "DiagnosisCriteria": diagnosisCriteria = currentPropertyNode.InnerText; break;

                        case "DiagnosisVersion": diagnosisVersion = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                        case "DrgCriteria": DrgCriteria = currentPropertyNode.InnerText; break;

                        case "Icd9ProcedureCodeCriteria": Icd9ProcedureCodeCriteria = currentPropertyNode.InnerText; break;

                        case "BillTypeCriteria": BillTypeCriteria = currentPropertyNode.InnerText; break;

                        case "LocationCodeCriteria": LocationCodeCriteria = currentPropertyNode.InnerText; break;

                        case "RevenueCodeCriteria": RevenueCodeCriteria = currentPropertyNode.InnerText; break;

                        case "ProcedureCodeCriteria": ProcedureCodeCriteria = currentPropertyNode.InnerText; break;

                        case "ModifierCodeCriteria": ModifierCodeCriteria = currentPropertyNode.InnerText; break;

                        case "ProviderSpecialtyCriteria": ProviderSpecialtyCriteria = currentPropertyNode.InnerText; break;


                        case "NdcCodeCriteria": NdcCodeCriteria = currentPropertyNode.InnerText; break;

                        case "DrugNameCriteria": DrugNameCriteria = currentPropertyNode.InnerText; break;

                        case "DeaClassificationCriteria": DeaClassificationCriteria = currentPropertyNode.InnerText; break;

                        case "TherapeuticClassificationCriteria": TherapeuticClassificationCriteria = currentPropertyNode.InnerText; break;


                        case "LabLoincCodeCriteria": LabLoincCodeCriteria = currentPropertyNode.InnerText; break;

                        case "Enabled": Enabled = Convert.ToBoolean (currentPropertyNode.InnerText); break;

                    }

                }

                // DO NOT SAVE, SAVE IS DONE BY PARENT OBJECT 

            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return response;

        }

        #endregion


        #region Validation

        public override Dictionary<String, String> Validate () {

            Dictionary<String, String> validationResponse = new Dictionary<String, String> ();


            String evaluationString = String.Empty;

            Int32 isNumberOutput = 0;


            if ((procedureCodeCriteria.Length > 0)) {

                evaluationString = procedureCodeCriteria.Replace (@" ", @"");

                foreach (String procedureCode in evaluationString.Split (',')) {

                    if ((procedureCode.Length != 5) && (procedureCode.Length != 11)) {

                        if (validationResponse.ContainsKey ("Procedure Code")) {

                            validationResponse["Procedure Code"] = validationResponse["Procedure Code"] + ", " + procedureCode;

                        }

                        else { validationResponse.Add ("Procedure Code", procedureCode); }

                        break;

                    }

                    else {

                        if (!Int32.TryParse (procedureCode.Substring (0, 1), out isNumberOutput)) {

                            if (!Int32.TryParse (procedureCode.Substring (1, 4), out isNumberOutput)) {

                                if (validationResponse.ContainsKey ("Procedure Code")) {

                                    validationResponse["Procedure Code"] = validationResponse["Procedure Code"] + ", " + procedureCode;

                                }

                                else { validationResponse.Add ("Procedure Code", procedureCode); }

                                break;

                            }

                        }

                        if (!Int32.TryParse (procedureCode.Substring (4, 1), out isNumberOutput)) {

                            if (!Int32.TryParse (procedureCode.Substring (0, 4), out isNumberOutput)) {

                                if (validationResponse.ContainsKey ("Procedure Code")) {

                                    validationResponse["Procedure Code"] = validationResponse["Procedure Code"] + ", " + procedureCode;

                                }

                                else { validationResponse.Add ("Procedure Code", procedureCode); }

                                break;

                            }

                        }

                        if (procedureCode.Length == 11) {

                            if ((!Int32.TryParse (procedureCode.Substring (7, 4), out isNumberOutput)) || (procedureCode.Substring (5, 1) != "-")) {

                                if (validationResponse.ContainsKey ("Procedure Code")) {

                                    validationResponse["Procedure Code"] = validationResponse["Procedure Code"] + ", " + procedureCode;

                                }

                                else { validationResponse.Add ("Procedure Code", procedureCode); }

                                break;

                            }

                        }

                    }

                } // foreach procedureCode

            } // end if procedureCodeCriteria

            return validationResponse;

        }

        #endregion


        #region Database Functions

        public override Boolean Save () {

            Boolean success = false;


            try {

                ModifiedAccountInfo = new Data.AuthorityAccountStamp (application);


                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("AuthorizedServiceDefinition_InsertUpdate");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@authorizedServiceDefinitionId", Id);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@authorizedServiceId", AuthorizedServiceId);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@category", Category, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@subcategory", Subcategory, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@serviceType", ServiceType, Server.Data.DataTypeConstants.DataCriteria);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@principalDiagnosisCriteria", principalDiagnosisCriteria, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@principalDiagnosisVersion", principalDiagnosisVersion);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@diagnosisCriteria", diagnosisCriteria, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@diagnosisVersion", diagnosisVersion);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@drgCriteria", drgCriteria, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@icd9ProcedureCodeCriteria", icd9ProcedureCodeCriteria, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@billTypeCriteria", billTypeCriteria, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@locationCodeCriteria", locationCodeCriteria, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@revenueCodeCriteria", revenueCodeCriteria, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@procedureCodeCriteria", procedureCodeCriteria, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifierCodeCriteria", modifierCodeCriteria, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@providerSpecialtyCriteria", providerSpecialtyCriteria, Server.Data.DataTypeConstants.DataCriteria);



                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@ndcCodeCriteria", ndcCodeCriteria, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@drugNameCriteria", drugNameCriteria, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@deaClassificationCriteria", deaClassificationCriteria, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@therapeuticClassificationCriteria", therapeuticClassificationCriteria, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@labLoincCodeCriteria", labLoincCodeCriteria, Server.Data.DataTypeConstants.DataCriteria);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@enabled", Enabled);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityName", ModifiedAccountInfo.SecurityAuthorityName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountId", ModifiedAccountInfo.UserAccountId, Server.Data.DataTypeConstants.UserAccountId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountName", ModifiedAccountInfo.UserAccountName, Server.Data.DataTypeConstants.Name);


                success = (sqlCommand.ExecuteNonQuery () == 1);


                #region Old Method

                //sqlStatement = new StringBuilder ();

                //sqlStatement.Append ("EXEC AuthorizedServiceDefinition_InsertUpdate ");

                //sqlStatement.Append (Id.ToString () + ", ");

                //sqlStatement.Append (authorizedServiceId.ToString () + ", ");

                //sqlStatement.Append ("'" + category.Replace ("'", "''") + "', ");

                //sqlStatement.Append ("'" + subcategory.Replace ("'", "''") + "', ");

                //sqlStatement.Append ("'" + serviceType.Replace ("'", "''") + "', ");

                //sqlStatement.Append ("'" + principalDiagnosisCriteria.Replace ("'", "''") + "', ");

                //sqlStatement.Append ("'" + diagnosisCriteria.Replace ("'", "''") + "', ");

                //sqlStatement.Append ("'" + drgCriteria.Replace ("'", "''") + "', ");

                //sqlStatement.Append ("'" + icd9ProcedureCodeCriteria.Replace ("'", "''") + "', ");

                //sqlStatement.Append ("'" + billTypeCriteria.Replace ("'", "''") + "', ");

                //sqlStatement.Append ("'" + locationCodeCriteria.Replace ("'", "''") + "', ");

                //sqlStatement.Append ("'" + revenueCodeCriteria.Replace ("'", "''") + "', ");

                //sqlStatement.Append ("'" + procedureCodeCriteria.Replace ("'", "''") + "', ");

                //sqlStatement.Append ("'" + modifierCodeCriteria.Replace ("'", "''") + "', ");

                //sqlStatement.Append ("'" + providerSpecialtyCriteria.Replace ("'", "''") + "', ");


                //sqlStatement.Append ("'" + ndcCodeCriteria.Replace ("'", "''") + "', ");

                //sqlStatement.Append ("'" + drugNameCriteria.Replace ("'", "''") + "', ");

                //sqlStatement.Append ("'" + deaClassificationCriteria.Replace ("'", "''") + "', ");

                //sqlStatement.Append ("'" + therapeuticClassificationCriteria.Replace ("'", "''") + "', ");


                //sqlStatement.Append ("'" + labLoincCodeCriteria.Replace ("'", "''") + "', ");

                //sqlStatement.Append ("'" + Convert.ToInt32 (enabled).ToString () + "'");

                //success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

                #endregion 

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }



                SetIdentity ();

                success = true;

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return success;

        }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            authorizedServiceId = (Int64) currentRow["AuthorizedServiceId"];

            category = (String) currentRow["category"];

            subcategory = (String) currentRow["Subcategory"];

            serviceType = (String) currentRow["ServiceType"];

            PrincipalDiagnosisCriteria = (String) currentRow["PrincipalDiagnosisCriteria"];

            PrincipalDiagnosisVersion = Convert.ToInt32 (currentRow["PrincipalDiagnosisVersion"]);

            DiagnosisCriteria = (String) currentRow["DiagnosisCriteria"];

            DiagnosisVersion = Convert.ToInt32 (currentRow["DiagnosisVersion"]);

            drgCriteria = (String) currentRow["DrgCriteria"];

            icd9ProcedureCodeCriteria = (String) currentRow["Icd9ProcedureCodeCriteria"];

            billTypeCriteria = (String) currentRow["BillTypeCriteria"];

            locationCodeCriteria = (String) currentRow["LocationCodeCriteria"];

            revenueCodeCriteria = (String) currentRow["RevenueCodeCriteria"];

            procedureCodeCriteria = (String) currentRow["ProcedureCodeCriteria"];

            modifierCodeCriteria = (String) currentRow["ModifierCodeCriteria"];

            providerSpecialtyCriteria = (String) currentRow["ProviderSpecialtyCriteria"];


            ndcCodeCriteria = (String) currentRow["NdcCodeCriteria"];

            drugNameCriteria = (String) currentRow["DrugNameCriteria"];

            deaClassificationCriteria = (String) currentRow["DeaClassificationCriteria"];

            therapeuticClassificationCriteria = (String) currentRow["TherapeuticClassificationCriteria"];


            labLoincCodeCriteria = (String) currentRow["LabLoincCodeCriteria"];

            enabled = (Boolean) currentRow["Enabled"];

            return;

        }

        #endregion


        #region Public Methods

        public Boolean ValidateOld () {

            Boolean success = true;

            validationMessage = String.Empty;

            String evaluationString = String.Empty;

            Int32 isNumberOutput = 0;


            if ((procedureCodeCriteria.Length > 0) && (success = true)) {

                evaluationString = procedureCodeCriteria.Replace (@" ", @"");

                foreach (String procedureCode in evaluationString.Split (',')) {

                    if ((procedureCode.Length != 5) && (procedureCode.Length != 11)) {

                        validationMessage = "Invalid Procedure Code: " + procedureCode;

                        success = false;

                        break;

                    }

                    else {

                        if (!Int32.TryParse (procedureCode.Substring (0, 1), out isNumberOutput)) {

                            if (!Int32.TryParse (procedureCode.Substring (1, 4), out isNumberOutput)) {

                                validationMessage = "Invalid Procedure Code: " + procedureCode;

                                success = false;

                                break;

                            }

                        }

                        if (!Int32.TryParse (procedureCode.Substring (4, 1), out isNumberOutput)) {

                            if (!Int32.TryParse (procedureCode.Substring (0, 4), out isNumberOutput)) {

                                validationMessage = "Invalid Procedure Code: " + procedureCode;

                                success = false;

                                break;

                            }

                        }

                        if (procedureCode.Length == 11) {

                            if ((!Int32.TryParse (procedureCode.Substring (7, 4), out isNumberOutput)) || (procedureCode.Substring (5, 1) != "-")) {

                                validationMessage = "Invalid Procedure Code: " + procedureCode;

                                success = false;

                                break;

                            }

                        }

                    }

                } // foreach procedureCode

            } // end if procedureCodeCriteria

            return success;

        }

        #endregion


        #region SQL Statement Constructor

        protected Boolean HasLineCriteria () {

            Boolean hasLineCriteria = false;

            hasLineCriteria = hasLineCriteria || (!String.IsNullOrEmpty (revenueCodeCriteria));

            hasLineCriteria = hasLineCriteria || (!String.IsNullOrEmpty (procedureCodeCriteria));

            hasLineCriteria = hasLineCriteria || (!String.IsNullOrEmpty (modifierCodeCriteria));

            hasLineCriteria = hasLineCriteria || (!String.IsNullOrEmpty (ndcCodeCriteria));

            return hasLineCriteria;

        }

        protected String ParseAuthorizationType () {

            string sqlCriteria = String.Empty;

            if (!String.IsNullOrEmpty (category)) { sqlCriteria = sqlCriteria + "{AuthorizationCategory = '" + category.Replace ("'", "''") + "'}"; }

            if (!String.IsNullOrEmpty (subcategory)) { sqlCriteria = sqlCriteria + "{AuthorizationSubcategory = '" + subcategory.Replace ("'", "''") + "'}"; }

            if (!String.IsNullOrEmpty (serviceType)) { sqlCriteria = sqlCriteria + "{AuthorizationServiceType = '" + serviceType.Replace ("'", "''") + "'}"; }

            if (sqlCriteria.Length > 0) {

                sqlCriteria = sqlCriteria.Replace ("}{", ") AND (");

            }

            return sqlCriteria;

        }

        protected String ParsePrincipalDiagnosisCriteria () {

            String sqlCriteria = String.Empty;

            String diagnosisCode = String.Empty;


            if (principalDiagnosisCriteria.Length == 0) { return String.Empty; }


            foreach (String currentDiagnosisCode in principalDiagnosisCriteria.Replace (" ", "").Split (',')) {

                if (!currentDiagnosisCode.Contains ("-")) {

                    diagnosisCode = currentDiagnosisCode;

                    diagnosisCode = diagnosisCode.Replace ('X', '_');

                    diagnosisCode = diagnosisCode.Replace ('x', '_');

                    diagnosisCode = diagnosisCode + "%";

                    diagnosisCode = diagnosisCode.Replace ("!%", "");

                    sqlCriteria = sqlCriteria + "{PrincipalDiagnosisCode  LIKE '" + diagnosisCode + "'}";

                }

                else {


                    String beginCode = currentDiagnosisCode.Split ('-')[0].Trim ().ToUpper ().Replace ('X', '0');

                    String endCode = currentDiagnosisCode.Split ('-')[1].Trim ().ToUpper ().Replace ('X', '9');

                    if (!endCode.Contains (".")) { endCode = endCode + ".99"; }

                    else if (endCode.Length < 6) { endCode = endCode + (".99").Substring (endCode.Length - 3, 6 - endCode.Length); }


                    diagnosisCode = "'" + beginCode + "' AND '" + endCode + "'";



                    sqlCriteria = sqlCriteria + "{PrincipalDiagnosisCode BETWEEN " + diagnosisCode + "}";

                }

            }

            if (sqlCriteria.Length > 0) {

                sqlCriteria = sqlCriteria.Replace ("}{", ") OR (");

                sqlCriteria = "((" + sqlCriteria + ") AND (PrincipalDiagnosisVersion = " + principalDiagnosisVersion.ToString () + "))";

            }

            return sqlCriteria;

        }

        protected String ParseDiagnosisCriteria () {

            String sqlCriteria = String.Empty;

            String diagnosisCode = String.Empty;


            if (diagnosisCriteria.Length == 0) { return String.Empty; }


            foreach (String currentDiagnosisCode in diagnosisCriteria.Replace (" ", "").Split (',')) {

                if (!currentDiagnosisCode.Contains ("-")) {

                    diagnosisCode = currentDiagnosisCode;

                    diagnosisCode = diagnosisCode.Replace ('X', '_');

                    diagnosisCode = diagnosisCode.Replace ('x', '_');

                    diagnosisCode = diagnosisCode + "%";

                    diagnosisCode = diagnosisCode.Replace ("!%", "");

                    sqlCriteria = sqlCriteria + "{DiagnosisCode LIKE '" + diagnosisCode + "'}";

                }

                else {


                    String beginCode = currentDiagnosisCode.Split ('-')[0].Trim ().ToUpper ().Replace ('X', '0');

                    String endCode = currentDiagnosisCode.Split ('-')[1].Trim ().ToUpper ().Replace ('X', '9');

                    if (!endCode.Contains (".")) { endCode = endCode + ".99"; }

                    else if (endCode.Length < 6) { endCode = endCode + (".99").Substring (endCode.Length - 3, 6 - endCode.Length); }


                    diagnosisCode = "'" + beginCode + "' AND '" + endCode + "'";



                    sqlCriteria = sqlCriteria + "{DiagnosisCode BETWEEN " + diagnosisCode + "}";

                }

            }

            if (sqlCriteria.Length > 0) {

                sqlCriteria = sqlCriteria.Replace ("}{", ") OR (");

                sqlCriteria = "((" + sqlCriteria + ") AND (DiagnosisVersion = " + diagnosisVersion.ToString () + "))";

            }

            return sqlCriteria;

        }

        protected String ParseIcd9ProcedureCriteria () {

            String sqlCriteria = String.Empty;

            String singleCriteria = String.Empty;

            String rangeCriteria = String.Empty;


            if (icd9ProcedureCodeCriteria.Length == 0) { return String.Empty; }


            #region Single Criteria

            foreach (String currentProcedureCode in icd9ProcedureCodeCriteria.Replace (" ", "").Split (',')) {

                if (!currentProcedureCode.Contains ("-")) {

                    singleCriteria = singleCriteria + "'" + currentProcedureCode + "'";

                }

            }

            if (singleCriteria.Length > 0) {

                singleCriteria = singleCriteria.Replace ("''", "', '");

                singleCriteria = "{Icd9ProcedureCode IN (" + singleCriteria + ")}";

            }


            #endregion


            #region Range Criteria

            foreach (String currentProcedureCodeRange in icd9ProcedureCodeCriteria.Replace (" ", "").Split (',')) {

                if (currentProcedureCodeRange.Contains ("-")) {

                    rangeCriteria = rangeCriteria + "{Icd9ProcedureCode BETWEEN '" + currentProcedureCodeRange.Split ('-')[0] + "' AND '" + currentProcedureCodeRange.Split ('-')[1] + "'}";

                }

            }

            if (rangeCriteria.Length > 0) {

                rangeCriteria = rangeCriteria.Replace ("}{", ") OR (");

            }

            #endregion


            sqlCriteria = singleCriteria + rangeCriteria;

            sqlCriteria = sqlCriteria.Replace ("}{", ") OR (");

            sqlCriteria = sqlCriteria.Replace ("{", "(");

            sqlCriteria = sqlCriteria.Replace ("}", ")");


            return sqlCriteria;

        }

        protected String ParseBillTypeCriteria () {

            String sqlCriteria = String.Empty;

            String singleCriteria = String.Empty;

            String rangeCriteria = String.Empty;

            if (billTypeCriteria.Length == 0) { return String.Empty; }


            foreach (String currentBillType in billTypeCriteria.Replace (" ", "").Split (',')) {

                if ((currentBillType.Length == 3) && (!currentBillType.Contains ("X")) && (!currentBillType.Contains ("x"))) {

                    singleCriteria = singleCriteria + "'" + currentBillType + "'";

                }

                else if ((currentBillType.Contains ("X")) || (currentBillType.Contains ("x"))) {

                    rangeCriteria = rangeCriteria + "{BillType LIKE '" + currentBillType.Replace ('x', '_').Replace ('X', '_') + "'}";

                }

            }

            if (singleCriteria.Length > 0) {

                singleCriteria = singleCriteria.Replace ("''", "', '");

                singleCriteria = "{BillType IN (" + singleCriteria + ")}";

            }

            sqlCriteria = singleCriteria + rangeCriteria;

            sqlCriteria = sqlCriteria.Replace ("}{", ") OR (");

            sqlCriteria = sqlCriteria.Replace ("{", "(");

            sqlCriteria = sqlCriteria.Replace ("}", ")");


            return sqlCriteria;

        }

        protected String ParseLocationCodeCriteria () {

            String sqlCriteria = String.Empty;

            String singleCriteria = String.Empty;

            String rangeCriteria = String.Empty;

            if (LocationCodeCriteria.Length == 0) { return String.Empty; }


            foreach (String currentLocationCode in LocationCodeCriteria.Replace (" ", "").Split (',')) {

                if ((currentLocationCode.Length == 2) && (!currentLocationCode.Contains ("X")) && (!currentLocationCode.Contains ("x"))) {

                    singleCriteria = singleCriteria + "'" + currentLocationCode + "'";

                }

                else if ((currentLocationCode.Contains ("X")) || (currentLocationCode.Contains ("x"))) {

                    rangeCriteria = rangeCriteria + "{ServiceLocation LIKE '" + currentLocationCode.Replace ("x", "[0-9]").Replace ("X", "[0-9]") + "'}";

                }

            }

            if (singleCriteria.Length > 0) {

                singleCriteria = singleCriteria.Replace ("''", "', '");

                singleCriteria = "{ServiceLocation IN (" + singleCriteria + ")}";

            }

            sqlCriteria = singleCriteria + rangeCriteria;

            sqlCriteria = sqlCriteria.Replace ("}{", ") OR (");

            sqlCriteria = sqlCriteria.Replace ("{", "(");

            sqlCriteria = sqlCriteria.Replace ("}", ")");


            return sqlCriteria;

        }

        protected String ParseRevenueCodeCriteria () {

            String sqlCriteria = String.Empty;

            String singleCriteria = String.Empty;

            String rangeCriteria = String.Empty;


            if (revenueCodeCriteria.Length == 0) { return String.Empty; }


            foreach (String currentRevenueCode in revenueCodeCriteria.Replace (" ", "").Split (',')) {

                if ((currentRevenueCode.Length == 3) && (!currentRevenueCode.ToUpper ().Contains ("X"))) {

                    singleCriteria = singleCriteria + "'0" + currentRevenueCode + "'";

                }

                if ((currentRevenueCode.Length == 4) && (!currentRevenueCode.Contains ("X")) && (!currentRevenueCode.Contains ("x"))) {

                    singleCriteria = singleCriteria + "'" + currentRevenueCode + "'";

                }

                else if ((currentRevenueCode.Contains ("X")) || (currentRevenueCode.Contains ("x")) && (!currentRevenueCode.Contains ("-"))) {

                    rangeCriteria = rangeCriteria + "{RevenueCode LIKE '" + currentRevenueCode.Replace ('x', '_').Replace ('X', '_') + "'}";

                }

                else if ((currentRevenueCode.Contains ("-")) && (!currentRevenueCode.ToUpper ().Contains ("X"))) {

                    rangeCriteria = rangeCriteria + "{RevenueCode BETWEEN '" + currentRevenueCode.Split ('-')[0].Trim () + "' AND '" + currentRevenueCode.Split ('-')[1].Trim () + "'}";

                }

                else if ((currentRevenueCode.Contains ("-")) && (currentRevenueCode.ToUpper ().Contains ("X"))) {

                    rangeCriteria = rangeCriteria + "{RevenueCode BETWEEN '" + currentRevenueCode.Split ('-')[0].Trim ().ToUpper ().Replace ('X', '0') + "'";

                    rangeCriteria = rangeCriteria + " AND '" + currentRevenueCode.Split ('-')[1].Trim ().ToUpper ().Replace ('X', '9') + "'}";

                }

            }

            if (singleCriteria.Length > 0) {

                singleCriteria = singleCriteria.Replace ("''", "', '");

                singleCriteria = "{RevenueCode IN (" + singleCriteria + ")}";

            }

            sqlCriteria = singleCriteria + rangeCriteria;

            sqlCriteria = sqlCriteria.Replace ("}{", ") OR (");

            sqlCriteria = sqlCriteria.Replace ("{", "(");

            sqlCriteria = sqlCriteria.Replace ("}", ")");


            return sqlCriteria;

        }

        protected String ParseProcedureCodeCriteria () {

            String sqlCriteria = String.Empty;

            String singleCriteria = String.Empty;

            String rangeCriteria = String.Empty;


            if (procedureCodeCriteria.Length == 0) { return String.Empty; }


            #region Single Criteria

            foreach (String currentProcedureCode in procedureCodeCriteria.Replace (" ", "").Split (',')) {

                if (currentProcedureCode.Length == 5) {

                    singleCriteria = singleCriteria + "'" + currentProcedureCode + "'";

                }

            }

            if (singleCriteria.Length > 0) {

                singleCriteria = singleCriteria.Replace ("''", "', '");

                singleCriteria = "{ProcedureCode IN (" + singleCriteria + ")}";

            }


            #endregion


            #region Range Criteria

            foreach (String currentProcedureCodeRange in procedureCodeCriteria.Replace (" ", "").Split (',')) {

                if ((currentProcedureCodeRange.Contains ("-")) && (currentProcedureCodeRange.Length == 11)) {

                    rangeCriteria = rangeCriteria + "{ProcedureCode BETWEEN '" + currentProcedureCodeRange.Substring (0, 5) + "' AND '" + currentProcedureCodeRange.Substring (6, 5) + "'}";

                }

            }

            if (rangeCriteria.Length > 0) {

                rangeCriteria = rangeCriteria.Replace ("}{", ") OR (");

            }

            #endregion


            sqlCriteria = singleCriteria + rangeCriteria;

            sqlCriteria = sqlCriteria.Replace ("}{", ") OR (");

            sqlCriteria = sqlCriteria.Replace ("{", "(");

            sqlCriteria = sqlCriteria.Replace ("}", ")");


            return sqlCriteria;

        }

        protected String ParseModifierCodeCriteria () {

            String sqlCriteria = String.Empty;

            String singleCriteria = String.Empty;

            String rangeCriteria = String.Empty;


            if (modifierCodeCriteria.Length == 0) { return String.Empty; }

            if (modifierCodeCriteria.Trim () == "!") { return "(ModifierCode = '')"; }


            foreach (String currentModifierCode in modifierCodeCriteria.Replace (" ", "").Split (',')) {

                if ((currentModifierCode.Length == 2) && (!currentModifierCode.Contains ("X")) && (!currentModifierCode.Contains ("x"))) {

                    singleCriteria = singleCriteria + "'" + currentModifierCode + "'";

                }

                else if ((currentModifierCode.Contains ("X")) || (currentModifierCode.Contains ("x"))) {

                    rangeCriteria = rangeCriteria + "{ModifierCode LIKE '" + currentModifierCode.Replace ('x', '_').Replace ('X', '_') + "'}";

                }

            }

            if (singleCriteria.Length > 0) {

                singleCriteria = singleCriteria.Replace ("''", "', '");

                singleCriteria = "{ModifierCode IN (" + singleCriteria + ")}";

            }

            sqlCriteria = singleCriteria + rangeCriteria;

            sqlCriteria = sqlCriteria.Replace ("}{", ") OR (");

            sqlCriteria = sqlCriteria.Replace ("{", "(");

            sqlCriteria = sqlCriteria.Replace ("}", ")");


            return sqlCriteria;

        }

        protected String ParseProviderSpecialtyCriteria () {

            string sqlCriteria = String.Empty;

            if (providerSpecialtyCriteria.Trim ().Length == 0) { return String.Empty; }


            foreach (String currentSpecialty in providerSpecialtyCriteria.Split (';')) {

                sqlCriteria = sqlCriteria + "'" + currentSpecialty.Replace ("'", "''").Trim () + "'";

            }

            if (sqlCriteria.Length > 0) {

                sqlCriteria = sqlCriteria.Replace ("''", "', '");

                sqlCriteria = "{SpecialtyName IN (" + sqlCriteria + ")}";

            }

            return sqlCriteria;

        }

        protected String ParseNdcCodeCriteria () {

            string sqlCriteria = String.Empty;

            if (ndcCodeCriteria.Trim ().Length == 0) { return String.Empty; }


            foreach (String currentNdcCode in ndcCodeCriteria.Replace (" ", "").Split (',')) {

                if (currentNdcCode.Trim ().Length == 11) { sqlCriteria = sqlCriteria + "'" + currentNdcCode.Trim () + "'"; }

            }

            if (sqlCriteria.Length > 0) {

                sqlCriteria = sqlCriteria.Replace ("''", "', '");

                sqlCriteria = "{NdcCode IN (" + sqlCriteria + ")}";

            }

            return sqlCriteria;

        }

        protected String ParseDrugNameCriteria () {

            string sqlCriteria = String.Empty;

            if (drugNameCriteria.Trim ().Length == 0) { return String.Empty; }


            foreach (String currentDrugName in drugNameCriteria.Split (',')) {

                sqlCriteria = sqlCriteria + "{Description LIKE '%" + currentDrugName.Replace ("'", "''").Trim () + "%'}";

            }

            if (sqlCriteria.Length > 0) {

                sqlCriteria = sqlCriteria.Replace ("}{", ") OR (");

            }

            return sqlCriteria;

        }

        protected String ParseDeaClassificationCriteria () {

            string sqlCriteria = String.Empty;

            if (deaClassificationCriteria.Trim ().Length == 0) { return String.Empty; }


            foreach (String currentDeaClassification in deaClassificationCriteria.Replace (" ", "").Split (',')) {

                if (currentDeaClassification.Trim ().Length == 1) { sqlCriteria = sqlCriteria + "'" + currentDeaClassification.Trim () + "'"; }

            }

            if (sqlCriteria.Length > 0) {

                sqlCriteria = sqlCriteria.Replace ("''", "', '");

                sqlCriteria = "{DeaClassification IN (" + sqlCriteria + ")}";

            }

            return sqlCriteria;

        }

        protected String ParseTherapeuticClassificationCriteria () {

            string sqlCriteria = String.Empty;

            if (therapeuticClassificationCriteria.Trim ().Length == 0) { return String.Empty; }


            foreach (String currentClassification in therapeuticClassificationCriteria.Replace (" ", "").Split (',')) {

                sqlCriteria = sqlCriteria + "{TherapeuticClassification LIKE '" + currentClassification.Replace ("'", "''") + "%'}";

            }

            if (sqlCriteria.Length > 0) {

                sqlCriteria = sqlCriteria.Replace ("}{", ") OR (");

            }

            return sqlCriteria;

        }

        public String SqlStatement {

            get {

                String sqlStatement = String.Empty;

                String selectClause = String.Empty;

                String fromClause = String.Empty;

                String whereClause = String.Empty;


                #region SELECT CLAUSE

                selectClause = selectClause + "SELECT \r\n";

                selectClause = selectClause + "    CAST (0 AS BIGINT) AS MemberAuthorizedServiceId, CAST (" + Id.ToString () + " AS BIGINT) AS AuthorizedServiceDefinitionId, \r\n";

                selectClause = selectClause + "    [Authorization].AuthorizationId, [Authorization].AuthorizationNumber, [Authorization].MemberId,";
                    
                selectClause = selectClause + "    [Authorization].ReferringProviderId, [Authorization].ServiceProviderId, \r\n";
                    
                selectClause = selectClause + "    [Authorization].AuthorizationCategory, [Authorization].AuthorizationSubcategory, [Authorization].AuthorizationServiceType,";

                selectClause = selectClause + "    [Authorization].AuthorizationCategory AS Category, [Authorization].AuthorizationSubcategory AS Subcategory, [Authorization].AuthorizationServiceType AS ServiceType,";

                selectClause = selectClause + "    [Authorization].ReceivedDate, [Authorization].ReferralDate, [Authorization].EffectiveDate, [Authorization].TerminationDate,";

                selectClause = selectClause + "    [Authorization].AuthorizationStatus, [Authorization].AuthorizationStatus AS Status, \r\n";

                selectClause = selectClause + "    [Authorization].PrincipalDiagnosisCode AS PrincipalDiagnosisCode, [Authorization].PrincipalDiagnosisCode, [Authorization].PrincipalDiagnosisVersion,";

                selectClause = selectClause + "    [Authorization].AdmittingDiagnosisCode, [Authorization].AdmittingDiagnosisVersion, [Authorization].DischargeDiagnosisCode, [Authorization].DischargeDiagnosisVersion,";
                    
                selectClause = selectClause + "    [Authorization].ExternalAuthorizationId, CAST ('' AS VARCHAR (060)) AS SpecialtyName, CAST ('' AS VARCHAR (060)) AS Description, \r\n";


                if (diagnosisCriteria.Trim ().Length != 0) {

                    selectClause = selectClause + "AuthorizationDiagnosis.DiagnosisCode AS DiagnosisCode, \r\n    ";

                    selectClause = selectClause + "AuthorizationDiagnosis.DiagnosisVersion AS DiagnosisVersion, \r\n    ";

                }

                else { 
                    
                    selectClause = selectClause + "CAST ('' AS VARCHAR (006)) AS DiagnosisCode, \r\n    ";

                    selectClause = selectClause + "CAST (9 AS INT) AS DiagnosisVersion, \r\n    "; 
                
                }


                if (HasLineCriteria ()) {

                    selectClause = selectClause + "    AuthorizationLine.LineNumber AS AuthorizationLine, AuthorizationLine.LineNumber, AuthorizationLine.LineStatus, AuthorizationLine.ServiceDate, \r\n";

                    selectClause = selectClause + "    AuthorizationLine.RevenueCode, AuthorizationLine.ProcedureCode, AuthorizationLine.ModifierCode, AuthorizationLine.NdcCode,";

                }

                else {

                    selectClause = selectClause + "    CAST (0 AS INT) AS AuthorizationLine, CAST (0 AS INT) AS LineNumber, CAST ('' AS VARCHAR (020)) AS LineStatus, CAST (NULL AS DATETIME) AS ServiceDate, \r\n";

                    selectClause = selectClause + "    CAST ('' AS VARCHAR (004)) AS RevenueCode, CAST ('' AS VARCHAR (006)) AS ProcedureCode, CAST ('' AS CHAR (002)) AS ModifierCode, CAST ('' AS CHAR (011)) AS NdcCode,";

                }
                    
                selectClause = selectClause + "    CAST (CONVERT (CHAR (10), [Authorization].EffectiveDate, 101) AS DATETIME) AS EventDate";

                #endregion 


                #region FROM CLAUSE

                fromClause = fromClause + "  FROM ";
                  
                fromClause = fromClause + "    dal.AuthorizedServiceAnalysisAuthorization AS [Authorization]";


                if (diagnosisCriteria.Trim ().Length != 0) {

                    fromClause = fromClause + "  JOIN dal.AuthorizationDiagnosis AS AuthorizationDiagnosis ON [Authorization].ExternalAuthorizationId = AuthorizationDiagnosis.ExternalAuthorizationId \r\n    ";

                }

                if (HasLineCriteria ()) {

                    fromClause = fromClause + "      JOIN dal.AuthorizedServiceAnalysisAuthorizationLine AS AuthorizationLine";

                    fromClause = fromClause + "        ON [Authorization].ExternalAuthorizationId = AuthorizationLine.ExternalAuthorizationId";

                }

                #endregion 


                #region WHERE CLAUSE

                whereClause = whereClause + "  WHERE {([Authorization].ExternalMemberId <> '')}";
                
                // 01/21/2010: REMOVED SEARCH BY TERMINATION CLAUSE AND OPENED UP THE AUTHORIZATIONS 
                //   TO ALL AVAILABLE AUTHORIZATIONS (HISTORICAL AND CURRENT). 

                // whereClause = whereClause + "{([Authorization].TerminationDate > DATEADD (DAY, -2, GETDATE ()))}";

                whereClause = whereClause + "{" + ParseAuthorizationType () + "}";

                whereClause = whereClause + "{" + ParsePrincipalDiagnosisCriteria () + "}";

                whereClause = whereClause + "{" + ParseDiagnosisCriteria () + "}";



                if (HasLineCriteria ()) {

                    whereClause = whereClause + "{" + ParseRevenueCodeCriteria () + "}";

                    whereClause = whereClause + "{" + ParseProcedureCodeCriteria () + "}";

                    whereClause = whereClause + "{" + ParseModifierCodeCriteria () + "}";

                    //                 whereClause = whereClause + "{" + ParseProviderSpecialtyCriteria () + "}";

                }

                whereClause = whereClause.Replace ("{}", "");

                whereClause = whereClause.Replace ("}{", ") \r\n    AND (");

                whereClause = whereClause.Replace ('{', '(');

                whereClause = whereClause.Replace ('}', ')');

                #endregion

                sqlStatement = selectClause + "\r\n" + fromClause + "\r\n" + whereClause + "\r\n";

                return sqlStatement;

            }

        }

        #endregion

    }

}
