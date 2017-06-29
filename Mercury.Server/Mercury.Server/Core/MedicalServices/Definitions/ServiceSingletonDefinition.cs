using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.MedicalServices.Definitions {

    [DataContract (Name = "ServiceSingletonDefinition")]
    public class ServiceSingletonDefinition : CoreObject {

        #region Private Properties

        [DataMember (Name = "ServiceId")]
        private Int64 serviceId;

        [DataMember (Name = "DataSourceType")]
        private Enumerations.ServiceDataSourceType dataSourceType = Mercury.Server.Core.MedicalServices.Enumerations.ServiceDataSourceType.Custom;

        [DataMember (Name = "EventDateOrder")]
        private Enumerations.EventDateOrder eventDateOrder = Mercury.Server.Core.MedicalServices.Enumerations.EventDateOrder.ClaimFromDate;

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

        [DataMember (Name = "IsPcpRequiredCriteria")]
        private Boolean isPcpRequiredCriteria = false;

        [DataMember (Name = "UseMemberAgeCriteria")]
        private Boolean useMemberAgeCriteria = false;

        [DataMember (Name = "MemberAgeDateQualifier")]
        private Core.Enumerations.DateQualifier memberAgeDateQualifier = Mercury.Server.Core.Enumerations.DateQualifier.Years;

        [DataMember (Name = "MemberAgeMinimum")]
        private Int32 memberAgeMinimum = 0;

        [DataMember (Name = "MemberAgeMaximum")]
        private Int32 memberAgeMaximum = 0;

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

        [DataMember (Name = "LabNameCriteria")]
        private String labNameCriteria = String.Empty;

        [DataMember (Name = "LabValueExpressionCriteria")]
        private String labValueExpressionCriteria = String.Empty;

        [DataMember (Name = "LabMetricId")]
        private Int64 labMetricId = 0;

        [DataMember (Name = "CustomCriteria")]
        private String customCriteria = String.Empty;

        [DataMember (Name = "Enabled")]
        private Boolean enabled = true;

        private String validationMessage = String.Empty;

        #endregion


        #region Public Properties - Encapsulated

        public Int64 ServiceId { get { return serviceId; } set { serviceId = value; } }

        public Enumerations.ServiceDataSourceType DataSourceType { get { return dataSourceType; } set { dataSourceType = value; } }

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

        public Boolean IsPcpRequiredCriteria { get { return isPcpRequiredCriteria; } set { isPcpRequiredCriteria = value; } }        

        public String NdcCodeCriteria { get { return ndcCodeCriteria; } set { ndcCodeCriteria = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String DrugNameCriteria { get { return drugNameCriteria; } set { drugNameCriteria = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String DeaClassificationCriteria { get { return deaClassificationCriteria; } set { deaClassificationCriteria = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String TherapeuticClassificationCriteria { get { return therapeuticClassificationCriteria; } set { therapeuticClassificationCriteria = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String LabLoincCodeCriteria { get { return labLoincCodeCriteria; } set { labLoincCodeCriteria = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String LabNameCriteria { get { return labNameCriteria; } set { labNameCriteria = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public String LabValueExpressionCriteria { get { return labValueExpressionCriteria; } set { labValueExpressionCriteria = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public Int64 LabMetricId { get { return labMetricId; } set { labMetricId = value; } }

        public String CustomCriteria { get { return customCriteria; } set { customCriteria = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.DataCriteria); } }

        public Boolean Enabled { get { return enabled; } set { enabled = value; } }


        public String LastValidationMessage { get { return validationMessage; } }

        #endregion


        #region Public Properties

        public Metrics.Metric LabMetric { get { return application.MetricGet (labMetricId); } }

        #endregion 


        #region Constructors

        public ServiceSingletonDefinition (Application application) { base.BaseConstructor (application); }

        #endregion


        #region XML Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];


            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ServiceId", serviceId.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "DataSourceType", ((Int32)dataSourceType).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "EventDateOrder", ((Int32)eventDateOrder).ToString ());


            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "PrincipalDiagnosisCriteria", principalDiagnosisCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "PrincipalDiagnosisVersion", principalDiagnosisVersion.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "DiagnosisCriteria", diagnosisCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "DiagnosisVersion", diagnosisVersion.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "DrgCriteria", drgCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "Icd9ProcedureCodeCriteria", icd9ProcedureCodeCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "BillTypeCriteria", billTypeCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "LocationCodeCriteria", locationCodeCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "RevenueCodeCriteria", revenueCodeCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ProcedureCodeCriteria", procedureCodeCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ModifierCodeCriteria", modifierCodeCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ProviderSpecialtyCriteria", providerSpecialtyCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "IsPcpRequiredCriteria", isPcpRequiredCriteria.ToString ());


            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "NdcCodeCriteria", ndcCodeCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "DrugNameCriteria", drugNameCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "DeaClassificationCriteria", deaClassificationCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "TherapeuticClassificationCriteria", therapeuticClassificationCriteria);


            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "LabLoincCodeCriteria", labLoincCodeCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "LabName", labNameCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "LabValueExpressionCriteria", labValueExpressionCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "LabMetricId", labMetricId.ToString ());


            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CustomCriteria", customCriteria);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "Enabled", enabled.ToString ());


            if ((dataSourceType == Enumerations.ServiceDataSourceType.Lab) && (labMetricId != 0) && (LabMetric != null)) {

                System.Xml.XmlElement metricPropertyNode = CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "LabMetric", String.Empty);

                metricPropertyNode.AppendChild (document.ImportNode (LabMetric.XmlSerialize ().ChildNodes[1], true));

            }
                

            return document;

        }
        
        public override List<ImportExport.Result> XmlImport (System.Xml.XmlNode objectNode) {

            List<ImportExport.Result> response = base.XmlImport (objectNode);

            System.Xml.XmlNode propertiesNode;

            String exceptionMessage = String.Empty;


            try {
                
                propertiesNode = objectNode.SelectSingleNode ("Properties");

                foreach (System.Xml.XmlNode currentPropertyNode in propertiesNode) {

                    switch (currentPropertyNode.Attributes["Name"].InnerText) {

                        case "DataSourceType": dataSourceType = (Mercury.Server.Core.MedicalServices.Enumerations.ServiceDataSourceType)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                        case "EventDateOrder": eventDateOrder = (Mercury.Server.Core.MedicalServices.Enumerations.EventDateOrder)Convert.ToInt32 (currentPropertyNode.InnerText); break;


                        case "PrincipalDiagnosisCriteria": principalDiagnosisCriteria = currentPropertyNode.InnerText; break;

                        case "PrincipalDiagnosisVersion": principalDiagnosisVersion = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                        case "DiagnosisCriteria": diagnosisCriteria = currentPropertyNode.InnerText; break;

                        case "DiagnosisVersion": diagnosisVersion = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                        case "DrgCriteria": drgCriteria = currentPropertyNode.InnerText; break;

                        case "Icd9ProcedureCodeCriteria": icd9ProcedureCodeCriteria = currentPropertyNode.InnerText; break;

                        case "BillTypeCriteria": billTypeCriteria = currentPropertyNode.InnerText; break;

                        case "LocationCodeCriteria": locationCodeCriteria = currentPropertyNode.InnerText; break;

                        case "RevenueCodeCriteria": revenueCodeCriteria = currentPropertyNode.InnerText; break;

                        case "ProcedureCodeCriteria": procedureCodeCriteria = currentPropertyNode.InnerText; break;

                        case "ModifierCodeCriteria": modifierCodeCriteria = currentPropertyNode.InnerText; break;

                        case "ProviderSpecialtyCriteria": providerSpecialtyCriteria = currentPropertyNode.InnerText; break;

                        case "IsPcpRequiredCriteria": IsPcpRequiredCriteria = Convert.ToBoolean (currentPropertyNode.InnerText); break;


                        case "NdcCodeCriteria": ndcCodeCriteria = currentPropertyNode.InnerText; break;

                        case "DrugNameCriteria": drugNameCriteria = currentPropertyNode.InnerText; break;

                        case "DeaClassificationCriteria": deaClassificationCriteria = currentPropertyNode.InnerText; break;

                        case "TherapeuticClassificationCriteria": therapeuticClassificationCriteria = currentPropertyNode.InnerText; break;


                        case "LabLoincCodeCriteria": labLoincCodeCriteria = currentPropertyNode.InnerText; break;

                        case "LabNameCriteria": labNameCriteria = currentPropertyNode.InnerText; break;

                        case "LabValueExpressionCriteria": labValueExpressionCriteria = currentPropertyNode.InnerText; break;


                        case "CustomCriteria": customCriteria = currentPropertyNode.InnerText; break;

                        case "Enabled": enabled = Convert.ToBoolean (currentPropertyNode.InnerText); break;

                        case "LabMetric":

                            if (currentPropertyNode.ChildNodes.Count > 0) {

                                System.Xml.XmlNode metricNode = currentPropertyNode.ChildNodes[0];

                                String metricName = metricNode.Attributes["Name"].InnerText;

                                labMetricId = base.application.MetricGetIdByName (metricName);

                                Core.Metrics.Metric metric = base.application.MetricGet (labMetricId);

                                if (metric == null) {

                                    metric = new Mercury.Server.Core.Metrics.Metric (base.application);

                                    response.AddRange (metric.XmlImport (metricNode));

                                    labMetricId = metric.Id;

                                }

                                if (labMetricId == 0) { throw new ApplicationException ("Unable to import Metric."); }

                            }
                            
                            break; 

                    }

                }

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


            if (useMemberAgeCriteria) {

                if (memberAgeMaximum < memberAgeMinimum) {

                    validationResponse.Add ("Member Age Maximum", "Value cannot be less than Member Age Minimum.");

                }

                else {

                    if (memberAgeMinimum < 0) {

                        validationResponse.Add ("Member Age Minimum", "Value cannot be less than 0.");

                    }

                    if (memberAgeMaximum < 0) {

                        validationResponse.Add ("Member Age Maximum", "Value cannot be less than 0.");

                    }

                }

            }


            return validationResponse;

        }

        #endregion 


        #region Database Functions

        public override Boolean Save () {

            Boolean success = false;


            try {

                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("ServiceSingletonDefinition_InsertUpdate");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@serviceSingletonDefinitionId", Id);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@serviceId", serviceId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@dataSourceType", ((Int32)dataSourceType));

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@eventDateOrder", ((Int32)eventDateOrder));

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@principalDiagnosisCriteria", principalDiagnosisCriteria, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@principalDiagnosisVersion", principalDiagnosisVersion);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@diagnosisCriteria", DiagnosisCriteria, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@diagnosisVersion", DiagnosisVersion);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@drgCriteria", drgCriteria, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@icd9ProcedureCodeCriteria", icd9ProcedureCodeCriteria, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@billTypeCriteria", billTypeCriteria, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@locationCodeCriteria", locationCodeCriteria, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@revenueCodeCriteria", revenueCodeCriteria, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@procedureCodeCriteria", procedureCodeCriteria, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifierCodeCriteria", modifierCodeCriteria, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@providerSpecialtyCriteria", providerSpecialtyCriteria, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@isPcpRequiredCriteria", isPcpRequiredCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@useMemberAgeCriteria", useMemberAgeCriteria);
                
                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberAgeDateQualifier", ((Int32)memberAgeDateQualifier));
                
                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberAgeMinimum", memberAgeMinimum);
                
                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberAgeMaximum", memberAgeMaximum);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@ndcCodeCriteria", ndcCodeCriteria, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@drugNameCriteria", drugNameCriteria, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@deaClassificationCriteria", deaClassificationCriteria, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@therapeuticClassificationCriteria", therapeuticClassificationCriteria, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@labLoincCodeCriteria", labLoincCodeCriteria, Server.Data.DataTypeConstants.DataCriteria);
                
                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@labNameCriteria", labNameCriteria, Server.Data.DataTypeConstants.DataCriteria);
                
                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@labValueExpressionCriteria", labValueExpressionCriteria, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@labMetricId", base.IdSqlAllowNullInt64 (labMetricId));
                
                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@customCriteria", customCriteria, Server.Data.DataTypeConstants.DataCriteria);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@enabled", enabled);


                success = (sqlCommand.ExecuteNonQuery () == 1);


                #region Old Method

                //sqlStatement = new StringBuilder ();

                //sqlStatement.Append ("EXEC ServiceSingletonDefinition_InsertUpdate ");

                //sqlStatement.Append (Id.ToString () + ", ");

                //sqlStatement.Append (serviceId.ToString () + ", ");

                //sqlStatement.Append (((Int32) dataSourceType).ToString () + ", ");

                //sqlStatement.Append (((Int32) eventDateOrder).ToString () + ", ");

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

                //sqlStatement.Append ("'" + Convert.ToInt32 (isPcpRequiredCriteria).ToString () + "', ");


                //sqlStatement.Append ("'" + Convert.ToInt32 (useMemberAgeCriteria).ToString () + "', ");

                //sqlStatement.Append (((Int32) memberAgeDateQualifier).ToString () + ", ");

                //sqlStatement.Append (memberAgeMinimum.ToString () + ", ");

                //sqlStatement.Append (memberAgeMaximum.ToString () + ", ");


                //sqlStatement.Append ("'" + ndcCodeCriteria.Replace ("'", "''") + "', ");

                //sqlStatement.Append ("'" + drugNameCriteria.Replace ("'", "''") + "', ");

                //sqlStatement.Append ("'" + deaClassificationCriteria.Replace ("'", "''") + "', ");

                //sqlStatement.Append ("'" + therapeuticClassificationCriteria.Replace ("'", "''") + "', ");


                //sqlStatement.Append ("'" + labLoincCodeCriteria.Replace ("'", "''") + "', ");

                //sqlStatement.Append ("'" + labNameCriteria.Replace ("'", "''") + "', ");

                //sqlStatement.Append ("'" + labValueExpressionCriteria.Replace ("'", "''") + "', ");

                //sqlStatement.Append (IdSqlAllowNull (labMetricId) + ", ");

                
                //sqlStatement.Append ("'" + customCriteria.Replace ("'", "''") + "', ");

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


            serviceId = (Int64) currentRow["ServiceId"];

            dataSourceType = (Mercury.Server.Core.MedicalServices.Enumerations.ServiceDataSourceType) ((Int32) currentRow["DataSourceType"]);

            eventDateOrder = (Mercury.Server.Core.MedicalServices.Enumerations.EventDateOrder) ((Int32) currentRow["EventDateOrder"]);

            principalDiagnosisCriteria = (String) currentRow["PrincipalDiagnosisCriteria"];

            principalDiagnosisVersion = Convert.ToInt32 (currentRow["PrincipalDiagnosisVersion"]);

            diagnosisCriteria = (String)currentRow["DiagnosisCriteria"];

            diagnosisVersion = Convert.ToInt32 (currentRow["DiagnosisVersion"]);

            drgCriteria = (String) currentRow["DrgCriteria"];

            icd9ProcedureCodeCriteria = (String) currentRow["Icd9ProcedureCodeCriteria"];

            billTypeCriteria = (String) currentRow["BillTypeCriteria"];

            locationCodeCriteria = (String) currentRow["LocationCodeCriteria"];

            revenueCodeCriteria = (String) currentRow["RevenueCodeCriteria"];

            procedureCodeCriteria = (String) currentRow["ProcedureCodeCriteria"];

            modifierCodeCriteria = (String) currentRow["ModifierCodeCriteria"];


            providerSpecialtyCriteria = (String) currentRow["ProviderSpecialtyCriteria"];

            isPcpRequiredCriteria = Convert.ToBoolean (currentRow["IsPcpRequiredCriteria"]);


            useMemberAgeCriteria = Convert.ToBoolean (currentRow["UseMemberAgeCriteria"]);

            memberAgeDateQualifier = (Mercury.Server.Core.Enumerations.DateQualifier) ((Int32) currentRow["MemberAgeDateQualifier"]);

            memberAgeMinimum = (Int32) currentRow["MemberAgeMinimum"];

            memberAgeMaximum = (Int32) currentRow["MemberAgeMaximum"];
            

            ndcCodeCriteria = (String) currentRow["NdcCodeCriteria"];
            
            drugNameCriteria = (String) currentRow["DrugNameCriteria"];

            deaClassificationCriteria = (String) currentRow["DeaClassificationCriteria"];

            therapeuticClassificationCriteria = (String) currentRow["TherapeuticClassificationCriteria"];


            labLoincCodeCriteria = (String) currentRow["LabLoincCodeCriteria"];

            labNameCriteria = (String) currentRow["LabNameCriteria"];

            labValueExpressionCriteria = (String) currentRow["LabValueExpressionCriteria"];

            labMetricId = IdFromSql (currentRow, "LabMetricId");


            customCriteria = (String) currentRow["CustomCriteria"];

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

        protected String ParsePrincipalDiagnosisCriteria () {

            String sqlCriteria = String.Empty;

            String diagnosisCode = String.Empty;


            if (principalDiagnosisCriteria.Length == 0) { return String.Empty; }


            foreach (String currentDiagnosisCode in principalDiagnosisCriteria.Replace (" ", "").Split (',')) {

                if (!currentDiagnosisCode.Contains ('-')) {

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

                    if (!endCode.Contains ('.')) { endCode = endCode + ".99"; }

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

                if (!currentDiagnosisCode.Contains ('-')) {

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

                    if (!endCode.Contains ('.')) { endCode = endCode + ".99"; }

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

                if (!currentProcedureCode.Contains ('-')) {

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

                if (currentProcedureCodeRange.Contains ('-')) {

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

                if ((currentBillType.Length == 3) && (!currentBillType.Contains ('X')) && (!currentBillType.Contains ('x'))) {

                    singleCriteria = singleCriteria + "'" + currentBillType + "'";

                }

                else if ((currentBillType.Contains ('X')) || (currentBillType.Contains ('x'))) {

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

                if ((currentLocationCode.Length == 2) && (!currentLocationCode.Contains ('X')) && (!currentLocationCode.Contains ('x'))) {

                    singleCriteria = singleCriteria + "'" + currentLocationCode + "'";

                }

                else if ((currentLocationCode.Contains ('X')) || (currentLocationCode.Contains ('x'))) {

                    rangeCriteria = rangeCriteria + "{ServicePlace LIKE '" + currentLocationCode.Replace ("x", "[0-9]").Replace ("X", "[0-9]") + "'}";

                }

            }

            if (singleCriteria.Length > 0) {

                singleCriteria = singleCriteria.Replace ("''", "', '");

                singleCriteria = "{ServicePlace IN (" + singleCriteria + ")}";

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

                if ((currentRevenueCode.Length == 4) && (!currentRevenueCode.Contains ('X')) && (!currentRevenueCode.Contains ('x'))) {

                    singleCriteria = singleCriteria + "'" + currentRevenueCode + "'";

                }

                else if ((currentRevenueCode.Contains ('X')) || (currentRevenueCode.Contains ('x')) && (!currentRevenueCode.Contains ('-'))) {

                    rangeCriteria = rangeCriteria + "{RevenueCode LIKE '" + currentRevenueCode.Replace ('x', '_').Replace ('X', '_') + "'}";

                }

                else if ((currentRevenueCode.Contains ('-')) && (!currentRevenueCode.ToUpper ().Contains ('X'))) {

                    rangeCriteria = rangeCriteria + "{RevenueCode BETWEEN '" + currentRevenueCode.Split ('-')[0].Trim () + "' AND '" + currentRevenueCode.Split ('-')[1].Trim () + "'}";

                }

                else if ((currentRevenueCode.Contains ('-')) && (currentRevenueCode.ToUpper ().Contains ('X'))) {

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

                if ((currentProcedureCodeRange.Contains ('-')) && (currentProcedureCodeRange.Length == 11)) {

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

            if (modifierCodeCriteria.Trim () == "!") { return "(ModifierCode1 = '')"; }


            foreach (String currentModifierCode in modifierCodeCriteria.Replace (" ", "").Split (',')) {

                if ((currentModifierCode.Length == 2) && (!currentModifierCode.Contains ('X')) && (!currentModifierCode.Contains ('x'))) {

                    singleCriteria = singleCriteria + "'" + currentModifierCode + "'";

                }

                else if ((currentModifierCode.Contains ('X')) || (currentModifierCode.Contains ('x'))) {

                    rangeCriteria = rangeCriteria + "{ModifierCode1 LIKE '" + currentModifierCode.Replace ('x', '_').Replace ('X', '_') + "'}";

                }

            }

            if (singleCriteria.Length > 0) {

                singleCriteria = singleCriteria.Replace ("''", "', '");

                singleCriteria = "{ModifierCode1 IN (" + singleCriteria + ")}";

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

        protected String ParseLoincCriteria () {

            string sqlCriteria = String.Empty;

            if (labLoincCodeCriteria.Trim ().Length == 0) { return String.Empty; }


            foreach (String currentLoinc in labLoincCodeCriteria.Replace (" ", "").Split (',')) {

                if (currentLoinc.Trim ().Length <= 7) { sqlCriteria = sqlCriteria + "'" + currentLoinc.Trim () + "'"; }

            }

            if (sqlCriteria.Length > 0) {

                sqlCriteria = sqlCriteria.Replace ("''", "', '");

                sqlCriteria = "{Loinc IN (" + sqlCriteria + ")}";

            }

            return sqlCriteria;

        }

        public String SqlStatement {

            get {

                String sqlStatement = String.Empty;

                String selectClause = String.Empty;

                String fromClause = String.Empty;

                String whereClause = String.Empty;

                String eventDateField = String.Empty;

                switch (dataSourceType) {

                    case Mercury.Server.Core.MedicalServices.Enumerations.ServiceDataSourceType.AllMedical:

                    case Mercury.Server.Core.MedicalServices.Enumerations.ServiceDataSourceType.Professional:

                    case Mercury.Server.Core.MedicalServices.Enumerations.ServiceDataSourceType.Institutional:

                        #region Select Clause

                        selectClause = "SELECT \r\n    ";


                        #region Select Event Date Order

                        switch (eventDateOrder) {

                            case Mercury.Server.Core.MedicalServices.Enumerations.EventDateOrder.ClaimFromDate:

                                eventDateField = "Claim.ClaimDateFrom";

                                selectClause = selectClause + eventDateField + " AS EventDate, \r\n    ";

                                break;

                            case Mercury.Server.Core.MedicalServices.Enumerations.EventDateOrder.ClaimThruDate:

                                eventDateField = "Claim.ClaimDateThru";

                                selectClause = selectClause + eventDateField + " AS EventDate, \r\n    ";

                                break;

                            case Mercury.Server.Core.MedicalServices.Enumerations.EventDateOrder.AdmissionClaimFromDate:

                                eventDateField = "ISNULL (Claim.AdmissionDate, Claim.ClaimDateFrom)";

                                selectClause = selectClause + eventDateField + " AS EventDate, \r\n    ";

                                break;

                            case Mercury.Server.Core.MedicalServices.Enumerations.EventDateOrder.DischargeClaimThruDate:

                                eventDateField = "ISNULL (Claim.DischargeDate, Claim.ClaimDateThru)";

                                selectClause = selectClause + eventDateField + " AS EventDate, \r\n    ";

                                break;

                            case Mercury.Server.Core.MedicalServices.Enumerations.EventDateOrder.ServiceClaimFromDate:

                                if ((locationCodeCriteria.Length != 0) || (revenueCodeCriteria.Length != 0) || (procedureCodeCriteria.Length != 0) || (modifierCodeCriteria.Length != 0)) {

                                    eventDateField = "ISNULL (ClaimLine.ServiceDateFrom, Claim.ClaimDateFrom)";

                                    selectClause = selectClause + eventDateField + " AS EventDate, \r\n    ";

                                }

                                else {

                                    eventDateField = "Claim.ClaimDateFrom";

                                    selectClause = selectClause + eventDateField + " AS EventDate, \r\n    "; 
                                
                                }

                                break;

                            case Mercury.Server.Core.MedicalServices.Enumerations.EventDateOrder.ServiceClaimThruDate:

                                if ((locationCodeCriteria.Length != 0) || (revenueCodeCriteria.Length != 0) || (procedureCodeCriteria.Length != 0) || (modifierCodeCriteria.Length != 0)) {

                                    eventDateField = "ISNULL (ClaimLine.ServiceDateThru, Claim.ClaimDateThru)";

                                    selectClause = selectClause + eventDateField + " AS EventDate, \r\n    ";

                                }

                                else {

                                    eventDateField = "Claim.ClaimDateThru";

                                    selectClause = selectClause + eventDateField + " AS EventDate, \r\n    "; 
                                
                                }

                                break;

                            case Mercury.Server.Core.MedicalServices.Enumerations.EventDateOrder.ServiceAdmissionClaimFromDate:


                                if ((locationCodeCriteria.Length != 0) || (revenueCodeCriteria.Length != 0) || (procedureCodeCriteria.Length != 0) || (modifierCodeCriteria.Length != 0)) {

                                    eventDateField = "ISNULL (ClaimLine.ServiceDateFrom, Claim.ClaimDateFrom)";

                                    selectClause = selectClause + eventDateField + " AS EventDate, \r\n    ";

                                }

                                else {

                                    eventDateField = "ISNULL (Claim.AdmissionDate, Claim.ClaimDateFrom)";
                                    
                                    selectClause = selectClause + eventDateField + " AS EventDate, \r\n    "; 
                                
                                }

                                break;

                            case Mercury.Server.Core.MedicalServices.Enumerations.EventDateOrder.ServiceDischargeClaimThrueDate:

                                if ((locationCodeCriteria.Length != 0) || (revenueCodeCriteria.Length != 0) || (procedureCodeCriteria.Length != 0) || (modifierCodeCriteria.Length != 0)) {

                                    eventDateField = "ISNULL (ClaimLine.ServiceDateThru, Claim.ClaimDateThru)";

                                    selectClause = selectClause + eventDateField + " AS EventDate, \r\n    ";

                                }

                                else { 
                                    
                                    eventDateField = "ISNULL (Claim.DischargeDate, Claim.ClaimDateThru)";
                                    
                                    selectClause = selectClause + eventDateField + " AS EventDate, \r\n    "; 
                                
                                }

                                break;

                        }

                        #endregion


                        selectClause = selectClause + "Claim.ClaimId, Claim.MemberId, Claim.ProviderId, Claim.ClaimType, Claim.ClaimStatus, \r\n    ";

                        selectClause = selectClause + "Claim.ClaimDateFrom, Claim.ClaimDateThru, Claim.AdmissionDate, Claim.DischargeDate, Claim.ReceivedDate, Claim.PaidDate, \r\n    ";

                        selectClause = selectClause + "Claim.BillType, Claim.PrincipalDiagnosisCode, Claim.PrincipalDiagnosisVersion, Claim.IsPcpClaim, Claim.ExternalClaimId, Claim.ExternalMemberId, Claim.ExternalProviderId, \r\n    ";

                        if (diagnosisCriteria.Length != 0) {

                            selectClause = selectClause + "ClaimDiagnosis.DiagnosisCode AS DiagnosisCode, \r\n    ";

                            selectClause = selectClause + "ClaimDiagnosis.DiagnosisVersion AS DiagnosisVersion, \r\n    ";

                        }

                        else { 
                            
                            selectClause = selectClause + "CAST ('' AS VARCHAR (006)) AS DiagnosisCode, \r\n    ";

                            selectClause = selectClause + "CAST (9 AS INT) AS DiagnosisVersion, \r\n    "; 

                        }


                        if (icd9ProcedureCodeCriteria.Length != 0) {

                            selectClause = selectClause + "ClaimIcd9Procedure.Icd9ProcedureCode AS Icd9ProcedureCode, \r\n    ";

                        }

                        else { selectClause = selectClause + "CAST ('' AS VARCHAR (006)) AS Icd9ProcedureCode, \r\n    "; }


                        if ((locationCodeCriteria.Length != 0) || (revenueCodeCriteria.Length != 0) || (procedureCodeCriteria.Length != 0) || (modifierCodeCriteria.Length != 0)) {

                            selectClause = selectClause + "ClaimLine.Line AS ClaimLine, ClaimLine.ServiceDateFrom, ClaimLine.ServiceDateThru, ClaimLine.ServiceLocation AS LocationCode, ClaimLine.RevenueCode, ClaimLine.ProcedureCode, ClaimLine.ModifierCode, CAST ('' AS CHAR (011)) AS NdcCode, ClaimLine.Units, \r\n    ";

                        }

                        else {

                            selectClause = selectClause + "CAST (0 AS INT) AS ClaimLine, CAST (NULL AS DATETIME) AS ServiceDateFrom, CAST (NULL AS DATETIME) AS ServiceDateThru, ";

                            selectClause = selectClause + "CAST ('' AS CHAR (002)) AS LocationCode, CAST ('' AS CHAR (004)) AS RevenueCode, CAST ('' AS CHAR (005)) AS ProcedureCode, ";

                            selectClause = selectClause + "CAST ('' AS CHAR (002)) AS ModifierCode, CAST ('' AS CHAR (011)) AS NdcCode, CAST (0 AS INT) AS Units, \r\n    ";

                        }

                        if (providerSpecialtyCriteria.Trim ().Length != 0) {

                            selectClause = selectClause + "ProviderSpecialty.SpecialtyName, \r\n    ";

                        }

                        else { selectClause = selectClause + "CAST ('' AS VARCHAR (060)) AS SpecialtyName,  \r\n    "; }


                        selectClause = selectClause + "CAST ('' AS CHAR (001)) AS DeaClassification, CAST ('' AS CHAR (006)) AS TherapeuticClassification, CAST ('' AS VARCHAR (060)) AS Description, \r\n    ";

                        selectClause = selectClause + "CAST ('' AS VARCHAR (007)) AS LabLoincCode, CAST ('' AS VARCHAR (060)) AS LabName, CAST (0 AS DECIMAL (20, 08)) AS LabValue, \r\n";

                        #endregion


                        #region From Clause

                        fromClause = "    '' AS TerminatorField \r\n  FROM \r\n    ";

                        fromClause = fromClause + "dal.ServiceAnalysisMedicalClaim AS Claim \r\n    ";

                        if (diagnosisCriteria.Length != 0) {
                            
                            fromClause = fromClause + "  JOIN dal.ClaimDiagnosis AS ClaimDiagnosis ON Claim.ExternalClaimId = ClaimDiagnosis.ExternalClaimId \r\n    "; 

                        }

                        if (icd9ProcedureCodeCriteria.Length != 0) {

                            fromClause = fromClause + "  JOIN dal.ClaimIcd9Procedure AS ClaimIcd9Procedure ON Claim.ExternalClaimId = ClaimIcd9Procedure.ExternalClaimId \r\n    ";

                        }

                        if ((locationCodeCriteria.Length != 0) || (revenueCodeCriteria.Length != 0) || (procedureCodeCriteria.Length != 0) || (modifierCodeCriteria.Length != 0)) {

                            fromClause = fromClause + "  JOIN dal.ServiceAnalysisMedicalClaimLine AS ClaimLine ON Claim.ExternalClaimId = ClaimLine.ExternalClaimId \r\n    ";

                        }

                        if (providerSpecialtyCriteria.Trim ().Length != 0) {

                            fromClause = fromClause + " JOIN dal.ProviderSpecialty AS ProviderSpecialty ON Claim.ExternalProviderId = ProviderSpecialty.ExternalProviderId \r\n    ";

                        }

                        if (useMemberAgeCriteria) {

                            fromClause = fromClause + " JOIN dal.Member AS Member ON Claim.ExternalMemberId = Member.ExternalMemberId \r\n    ";

                        }

                        #endregion


                        #region Where Clause

                        whereClause = "WHERE (Claim.ClaimStatus = " + ((Int32) Core.Claims.Enumerations.ClaimStatus.Paid).ToString () + ") AND \r\n      ";

                        whereClause = whereClause + "{" + ParsePrincipalDiagnosisCriteria () + "}";

                        whereClause = whereClause + "{" + ParseDiagnosisCriteria () + "}";

                        whereClause = whereClause + "{" + ParseIcd9ProcedureCriteria () + "}";

                        whereClause = whereClause + "{" + ParseBillTypeCriteria () + "}";

                        whereClause = whereClause + "{" + ParseLocationCodeCriteria () + "}";

                        whereClause = whereClause + "{" + ParseRevenueCodeCriteria () + "}";

                        whereClause = whereClause + "{" + ParseProcedureCodeCriteria () + "}";

                        whereClause = whereClause + "{" + ParseModifierCodeCriteria () + "}";

                        whereClause = whereClause + "{" + ParseProviderSpecialtyCriteria () + "}";

                        if (isPcpRequiredCriteria) { whereClause = whereClause + "{IsPcpClaim = 1}"; }


                        if (dataSourceType == Mercury.Server.Core.MedicalServices.Enumerations.ServiceDataSourceType.Professional) {

                            // whereClause = whereClause + "{ClaimType = 'Professional'}";

                            whereClause = whereClause + "{ClaimType = 1}";

                        }

                        else if (dataSourceType == Mercury.Server.Core.MedicalServices.Enumerations.ServiceDataSourceType.Institutional) {

                            // whereClause = whereClause + "{ClaimType = 'Institutional'}";

                            whereClause = whereClause + "{ClaimType = 2}";

                        }


                        if (useMemberAgeCriteria) {

                            whereClause = whereClause + "{dbo.AgeIn" + memberAgeDateQualifier.ToString () + "OnDate (Member.BirthDate, " + eventDateField + ") BETWEEN " + memberAgeMinimum.ToString () + " AND " + memberAgeMaximum.ToString () + "}";

                        }


                        whereClause = whereClause.Replace ("{}", "");

                        whereClause = whereClause.Replace ("}{", ") \r\n    AND (");

                        whereClause = whereClause.Replace ('{', '(');

                        whereClause = whereClause.Replace ('}', ')');

                        #endregion  

                        sqlStatement = selectClause + "\r\n" + fromClause + "\r\n" + whereClause + "\r\n";

                        break;

                    case Mercury.Server.Core.MedicalServices.Enumerations.ServiceDataSourceType.Pharmacy:

                        #region Select Clause

                        selectClause = "SELECT \r\n    ";

                        selectClause = selectClause + "\r\n    Claim.ServiceDateFrom AS EventDate, ";

                        selectClause = selectClause + "\r\n    Claim.ClaimId, Claim.MemberId, Claim.ProviderId, Claim.ClaimType, Claim.Status AS ClaimStatus, \r\n    ";

                        selectClause = selectClause + "\r\n    Claim.ClaimDateFrom, Claim.ClaimDateThru, CAST (NULL AS DATETIME) AS AdmissionDate, CAST (NULL AS DATETIME) AS DischargeDate, Claim.PaidDate AS ReceivedDate, Claim.PaidDate, \r\n    ";

                        selectClause = selectClause + "\r\n    CAST ('' AS CHAR (03)) AS BillType, CAST ('' AS VARCHAR (06)) AS PrincipalDiagnosisCode, CAST (9 AS INT) AS PrincipalDiagnosisVersion, ";
                        
                        selectClause = selectClause + "\r\n    Claim.ExternalClaimId, Claim.ExternalMemberId, Claim.ExternalProviderId, \r\n    ";

                        selectClause = selectClause + "\r\n    CAST (1 AS INT) AS ClaimLine, CAST (NULL AS DATETIME) AS ServiceDateFrom, CAST (NULL AS DATETIME) AS ServiceDateThru, ";

                        selectClause = selectClause + "\r\n    CAST ('' AS VARCHAR (006)) AS DiagnosisCode, ";

                        selectClause = selectClause + "\r\n    CAST (9 AS INT) AS DiagnosisVersion, ";

                        selectClause = selectClause + "\r\n    CAST ('' AS VARCHAR (006)) AS Icd9ProcedureCode, ";

                        selectClause = selectClause + "\r\n    CAST ('' AS CHAR (002)) AS LocationCode, CAST ('' AS CHAR (004)) AS RevenueCode, CAST ('' AS CHAR (005)) AS ProcedureCode, CAST ('' AS CHAR (002)) AS ModifierCode,";

                        selectClause = selectClause + "\r\n    CAST ('' AS VARCHAR (060)) AS SpecialtyName, \r\n    ";

                        selectClause = selectClause + "\r\n    Claim.NationalDrugCode AS NdcCode, Claim.Units, \r\n    ";

                        selectClause = selectClause + "\r\n    Claim.DeaClassification, Claim.TherapeuticClassification, CAST (NationalDrugCode.DrugName AS VARCHAR (060)) AS Description, \r\n    ";

                        selectClause = selectClause + "\r\n    CAST (0 AS BIT) AS IsPcpClaim, CAST ('' AS VARCHAR (007)) AS LabLoincCode, CAST ('' AS VARCHAR (060)) AS LabName, CAST (0 AS DECIMAL (20, 08)) AS LabValue, \r\n    ";

                        #endregion  
                        

                        #region From Clause

                        fromClause = "    '' AS TerminatorField \r\n  FROM \r\n    ";

                        fromClause = fromClause + "dal.ServiceAnalysisPharmacyClaim AS Claim \r\n    ";

                        fromClause = fromClause + "  LEFT JOIN dbo.NationalDrugCode ON Claim.NationalDrugCode = NationalDrugCode.NationalDrugCode \r\n";

                        
                        #endregion


                        #region Where Clause

                        whereClause = "WHERE (Claim.ClaimStatus = " + ((Int32)Core.Claims.Enumerations.ClaimStatus.Paid).ToString () + ") AND \r\n      ";

                        whereClause = whereClause + "{" + ParseNdcCodeCriteria () + "}";

                        whereClause = whereClause + "{" + ParseDrugNameCriteria () + "}";

                        whereClause = whereClause + "{" + ParseDeaClassificationCriteria () + "}";

                        whereClause = whereClause + "{" + ParseTherapeuticClassificationCriteria () + "}";


                        if ((ndcCodeCriteria.Trim ().Length == 0) &&
                            (drugNameCriteria.Trim ().Length == 0) &&
                            (deaClassificationCriteria.Trim ().Length == 0) &&
                            (therapeuticClassificationCriteria.Trim ().Length == 0)) {

                            whereClause = whereClause + "{(0 = 1)}";

                        }


                        whereClause = whereClause.Replace ("{}", "");

                        whereClause = whereClause.Replace ("}{", ") \r\n    AND (");

                        whereClause = whereClause.Replace ('{', '(');

                        whereClause = whereClause.Replace ('}', ')');

                        #endregion

                        sqlStatement = selectClause + "\r\n" + fromClause + "\r\n" + whereClause + "\r\n";

                        break;

                    case Mercury.Server.Core.MedicalServices.Enumerations.ServiceDataSourceType.Lab:

                        #region Select Clause

                        selectClause += "SELECT \r\n";

                        selectClause += "    Claim.ServiceDate AS EventDate, \r\n";

                        selectClause += "    Claim.LabResultId AS ClaimId, \r\n";

                        selectClause += "    Claim.MemberId, Claim.ProviderId, 5 AS ClaimType /* LAB */, 6 AS ClaimStatus /* PAID */, \r\n";

                        selectClause += "    Claim.ServiceDate AS ClaimDateFrom, \r\n";

                        selectClause += "    Claim.ServiceDate AS ClaimDateThru, CAST (NULL AS DATETIME) AS AdmissionDate, CAST (NULL AS DATETIME) AS DischargeDate, \r\n";

                        selectClause += "    Claim.ReportedDate AS ReceivedDate, Claim.PaidDate, \r\n";

                        selectClause = selectClause + "\r\n    CAST ('' AS CHAR (03)) AS BillType, CAST ('' AS VARCHAR (06)) AS PrincipalDiagnosisCode, CAST (9 AS INT) AS PrincipalDiagnosisVersion, ";
                        
                        selectClause = selectClause + "\r\n    Claim.LabReferenceNumber AS ExternalClaimId, '' AS ExternalMemberId, '' AS ExternalProviderId, \r\n    ";

                        selectClause = selectClause + "\r\n    CAST (1 AS INT) AS ClaimLine, Claim.ServiceDate AS ServiceDateFrom, Claim.ServiceDate AS ServiceDateThru, ";

                        selectClause = selectClause + "\r\n    CAST ('' AS VARCHAR (006)) AS DiagnosisCode, ";

                        selectClause = selectClause + "\r\n    CAST (9 AS INT) AS DiagnosisVersion, ";

                        selectClause = selectClause + "\r\n    CAST ('' AS VARCHAR (006)) AS Icd9ProcedureCode, ";

                        selectClause = selectClause + "\r\n    CAST ('' AS CHAR (002)) AS LocationCode, CAST ('' AS CHAR (004)) AS RevenueCode, CAST ('' AS CHAR (005)) AS ProcedureCode, CAST ('' AS CHAR (002)) AS ModifierCode,";

                        selectClause = selectClause + "\r\n    CAST ('' AS VARCHAR (060)) AS SpecialtyName, \r\n    ";

                        selectClause = selectClause + "\r\n    '' AS NdcCode, 0 AS Units, \r\n    ";

                        selectClause = selectClause + "\r\n    '' AS DeaClassification, '' AS TherapeuticClassification, Claim.LabTestName AS Description, \r\n    ";

                        selectClause = selectClause + "\r\n    CAST (0 AS BIT) AS IsPcpClaim, \r\n";

                        selectClause += "    Claim.Loinc AS LabLoincCode, Claim.LabTestName AS LabName, Claim.LabValue AS LabValue, \r\n    ";

                        #endregion 
                        

                        #region From Clause

                        fromClause = "    '' AS TerminatorField \r\n  FROM \r\n    ";

                        fromClause = fromClause + "dal.ServiceAnalysisLabResult AS Claim \r\n    ";
                        
                        #endregion


                        #region Where Clause

                        // whereClause = "WHERE (Claim.ClaimStatus = " + ((Int32)Core.Claims.Enumerations.ClaimStatus.Paid).ToString () + ") AND \r\n      ";

                        whereClause = "WHERE " + "{" + ParseLoincCriteria () + "}";


                        whereClause = whereClause.Replace ("{}", "");

                        whereClause = whereClause.Replace ("}{", ") \r\n    AND (");

                        whereClause = whereClause.Replace ('{', '(');

                        whereClause = whereClause.Replace ('}', ')');

                        #endregion

                        sqlStatement = selectClause + "\r\n" + fromClause + "\r\n" + whereClause + "\r\n";

                        break;

                    case Mercury.Server.Core.MedicalServices.Enumerations.ServiceDataSourceType.Custom:

                        sqlStatement = "EXEC " + customCriteria;

                        break;

                }

                return sqlStatement;

            }

        }

        #endregion

    }

}
