using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Metrics {
    
    [DataContract (Name = "Metric")]
    public class Metric : CoreConfigurationObject  {

        #region Private Properties

        [DataMember (Name = "MetricType")]
        private Enumerations.MetricType metricType = Server.Core.Metrics.Enumerations.MetricType.Health;

        [DataMember (Name = "DataType")]
        private Enumerations.MetricDataType dataType = Server.Core.Metrics.Enumerations.MetricDataType.Decimal;

        [DataMember (Name = "MinimumValue")]
        private Decimal minimumValue = Decimal.MinValue;

        [DataMember (Name = "MaximumValue")]
        private Decimal maximumValue = Decimal.MaxValue;


        [DataMember (Name = "CostDataSource")]
        private Enumerations.MetricCostDataSource costDataSource = Server.Core.Metrics.Enumerations.MetricCostDataSource.AllClaims;

        [DataMember (Name = "CostClaimDateType")]
        private Enumerations.MetricCostClaimDateType costClaimDateType = Enumerations.MetricCostClaimDateType.PaidDate;

        [DataMember (Name = "CostReportingPeriod")]
        private Enumerations.MetricCostReportingPeriod costReportingPeriod = Server.Core.Metrics.Enumerations.MetricCostReportingPeriod.CalenderYear;

        [DataMember (Name = "CostReportingPeriodValue")]
        private Int32 costReportingPeriodValue = 0;

        [DataMember (Name = "CostReportingPeriodQualifier")]
        private Core.Enumerations.DateQualifier costReportingPeriodQualifier = Server.Core.Enumerations.DateQualifier.Months;

        [DataMember (Name = "CostWatermarkPeriod")]
        private Enumerations.MetricCostWatermarkPeriod costWatermarkPeriod = Server.Core.Metrics.Enumerations.MetricCostWatermarkPeriod.CalenderYear;

        [DataMember (Name = "CostWatermarkPeriodValue")]
        private Int32 costWatermarkPeriodValue = 0;

        [DataMember (Name = "CostWatermarkPeriodQualifier")]
        private Core.Enumerations.DateQualifier costWatermarkPeriodQualifier = Server.Core.Enumerations.DateQualifier.Months;


        [DataMember (Name = "CostServices")]
        private Dictionary<Int64, String> costServices = new Dictionary<Int64, String> ();


        private Int64 processLogId = 0;

        private Int64 processStepId = 0;

        #endregion


        #region Public Properties

        public Enumerations.MetricType MetricType { get { return metricType; } set { metricType = value; } }

        public Enumerations.MetricDataType DataType { get { return dataType; } set { dataType = value; } }

        public Decimal MinimumValue { get { return minimumValue; } set { minimumValue = (dataType == Metrics.Enumerations.MetricDataType.Integer) ? Decimal.Truncate (value) : value; } }

        public Decimal MaximumValue { get { return maximumValue; } set { maximumValue = (dataType == Metrics.Enumerations.MetricDataType.Integer) ? Decimal.Truncate (value) : value; } }


        public Enumerations.MetricCostDataSource CostDataSource { get { return costDataSource; } set { costDataSource = value; } }

        public Enumerations.MetricCostClaimDateType CostClaimDateType { get { return costClaimDateType; } set { costClaimDateType = value; } }

        public Enumerations.MetricCostReportingPeriod CostReportingPeriod { get { return costReportingPeriod; } set { costReportingPeriod = value; } }

        public Int32 CostReportingPeriodValue { get { return costReportingPeriodValue; } set { costReportingPeriodValue = value; } }

        public Core.Enumerations.DateQualifier CostReportingPeriodQualifier { get { return costReportingPeriodQualifier; } set { costReportingPeriodQualifier = value; } }

        public Enumerations.MetricCostWatermarkPeriod CostWatermarkPeriod { get { return costWatermarkPeriod; } set { costWatermarkPeriod = value; } }

        public Int32 CostWatermarkPeriodValue { get { return costWatermarkPeriodValue; } set { costWatermarkPeriodValue = value; } }

        public Core.Enumerations.DateQualifier CostWatermarkPeriodQualifier { get { return costWatermarkPeriodQualifier; } set { costWatermarkPeriodQualifier = value; } }

        public Dictionary<Int64, String> CostServices { get { return costServices; } set { costServices = value; } }

        #endregion
        

        #region Constructors

        public Metric (Application applicationReference) { base.BaseConstructor (applicationReference); }

        public Metric (Application applicationReference, Int64 forMetricId) { base.BaseConstructor (applicationReference, forMetricId); }

        #endregion



        #region Xml Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];



            #region Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "MetricType", ((Int32) metricType).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "MetricTypeName", metricType.ToString ());


            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "DataType", ((Int32)dataType).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "DataTypeName", dataType.ToString ());


            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "MinimumValue", minimumValue.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "MaximumValue", maximumValue.ToString ());


            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CostDataSource", ((Int32)costDataSource).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CostClaimDateType", ((Int32)costClaimDateType).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CostDataSourceName", costDataSource.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CostReportingPeriod", ((Int32)costReportingPeriod).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CostReportingPeriodName", costReportingPeriod.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CostReportingPeriodValue", costReportingPeriodValue.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CostReportingPeriodQualifier", ((Int32)costReportingPeriodQualifier).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CostReportingPeriodQualifierName", costReportingPeriodQualifier.ToString ());


            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CostWatermarkPeriod", ((Int32)costWatermarkPeriod).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CostWatermarkPeriodName", costWatermarkPeriod.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CostWatermarkPeriodValue", costWatermarkPeriodValue.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CostWatermarkPeriodQualifier", ((Int32)costWatermarkPeriodQualifier).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CostWatermarkPeriodQualifierName", costWatermarkPeriodQualifier.ToString ());

            #endregion


            // TODO: COST SERVICES EXPORT

            #region Cost Services

            //System.Xml.XmlElement CostServicesNode = CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CostServices", String.Empty);

            //foreach (WorkQueueTeam currentWorkQueueTeam in workTeams) {

            //    System.Xml.XmlElement workQueueTeamNode;


            //    workQueueTeamNode = document.CreateElement ("WorkQueueTeam");

            //    workQueueTeamNode.SetAttribute ("WorkTeamId", currentWorkQueueTeam.WorkTeamId.ToString ());

            //    workQueueTeamNode.SetAttribute ("WorkTeamName", application.CoreObjectGetNameById ("WorkTeam", currentWorkQueueTeam.WorkTeamId));

            //    workQueueTeamNode.SetAttribute ("Permission", ((Int32)currentWorkQueueTeam.Permission).ToString ());

            //    workQueueTeamNode.SetAttribute ("PermissionName", currentWorkQueueTeam.Permission.ToString ());


            //    workQueueTeamsNode.AppendChild (workQueueTeamNode);

            //}

            #endregion


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

                        case "MetricType": MetricType = (Enumerations.MetricType)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                        case "DataType": DataType = (Enumerations.MetricDataType)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                        case "MinimumValue": MinimumValue = Convert.ToDecimal (currentPropertyNode.InnerText); break;

                        case "MaximumValue": MaximumValue = Convert.ToDecimal (currentPropertyNode.InnerText); break;

                        case "CostDataSource": CostDataSource = (Enumerations.MetricCostDataSource)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                        case "CostClaimDateType": CostClaimDateType = (Enumerations.MetricCostClaimDateType)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                        case "CostReportingPeriod": CostReportingPeriod = (Enumerations.MetricCostReportingPeriod)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                        case "CostReportingPeriodValue": CostReportingPeriodValue = Convert.ToInt32 (currentPropertyNode.InnerText); break; 

                        case "CostReportingPeriodQualifier": CostReportingPeriodQualifier = (Core.Enumerations.DateQualifier)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                        case "CostWatermarkPeriod": CostWatermarkPeriod = (Enumerations.MetricCostWatermarkPeriod)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                        case "CostWatermarkPeriodValue": CostWatermarkPeriodValue = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                        case "CostWatermarkPeriodQualifier": CostWatermarkPeriodQualifier = (Core.Enumerations.DateQualifier)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    }

                }

            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return response;

        }

        #endregion 


        #region Validation Functions

        public override Dictionary<String, String> Validate () {

            Dictionary<String, String> validationResponse = base.Validate ();


            if (metricType == Enumerations.MetricType.Health) {

                // VALIDATE MIN/MAX VALUES 

                MinimumValue = MinimumValue;

                MaximumValue = MaximumValue;

                if (minimumValue > maximumValue) { validationResponse.Add ("Value Range", "Invalid minimum or maximum values."); }

            }


            return validationResponse;

        }

        #endregion


        #region Data Functions
        
        override public void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);

            metricType = (Server.Core.Metrics.Enumerations.MetricType) ((Int32) currentRow ["MetricType"]);

            dataType = (Server.Core.Metrics.Enumerations.MetricDataType) ((Int32) currentRow ["DataType"]);

            minimumValue = (Decimal) currentRow["MinimumValue"];

            maximumValue = (Decimal) currentRow["MaximumValue"];


            costDataSource = (Enumerations.MetricCostDataSource)Convert.ToInt32 (currentRow["CostDataSource"]);

            costClaimDateType = (Enumerations.MetricCostClaimDateType)Convert.ToInt32 (currentRow["CostClaimDateType"]);


            costReportingPeriod = (Enumerations.MetricCostReportingPeriod)Convert.ToInt32 (currentRow["CostReportingPeriod"]);

            costReportingPeriodValue = Convert.ToInt32 (currentRow["CostReportingPeriodValue"]);

            costReportingPeriodQualifier = (Core.Enumerations.DateQualifier)Convert.ToInt32 (currentRow["CostReportingPeriodQualifier"]);


            costWatermarkPeriod = (Enumerations.MetricCostWatermarkPeriod)Convert.ToInt32 (currentRow["CostWatermarkPeriod"]);

            costWatermarkPeriodValue = Convert.ToInt32 (currentRow["CostWatermarkPeriodValue"]);

            costWatermarkPeriodQualifier = (Core.Enumerations.DateQualifier)Convert.ToInt32 (currentRow["CostWatermarkPeriodQualifier"]);


            return;

        }

        override public Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();



            if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.MetricManage)) { throw new ApplicationException ("PermissionDenied"); }


            modifiedAccountInfo = new Server.Data.AuthorityAccountStamp (application.Session);

            try {

                Dictionary<String, String> validationResponse = Validate ();

                if (validationResponse.Count != 0) {

                    foreach (String validationKey in validationResponse.Keys) {

                        throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                    }

                }
                    

                application.EnvironmentDatabase.BeginTransaction ();

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC Metric_InsertUpdate ");

                
                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append ("'" + NameSql + "', ");

                sqlStatement.Append ("'" + DescriptionSql + "', ");

                sqlStatement.Append (((Int32) metricType).ToString () + ", ");

                sqlStatement.Append (((Int32) dataType).ToString () + ", ");

                sqlStatement.Append (minimumValue.ToString () + ", ");

                sqlStatement.Append (maximumValue.ToString () + ", ");


                sqlStatement.Append (((Int32)costDataSource).ToString () + ", ");

                sqlStatement.Append (((Int32)costClaimDateType).ToString () + ", ");


                sqlStatement.Append (((Int32)costReportingPeriod).ToString () + ", ");

                sqlStatement.Append (((Int32)costReportingPeriodValue).ToString () + ", ");

                sqlStatement.Append (((Int32)costReportingPeriodQualifier).ToString () + ", ");

                sqlStatement.Append (((Int32)costWatermarkPeriod).ToString () + ", ");

                sqlStatement.Append (((Int32)costWatermarkPeriodValue).ToString () + ", ");

                sqlStatement.Append (((Int32)costWatermarkPeriodQualifier).ToString () + ", ");


                sqlStatement.Append (Convert.ToInt32 (Enabled).ToString () + ", ");

                sqlStatement.Append (Convert.ToInt32 (Visible).ToString () + ", ");

                sqlStatement.Append ("'" + ExtendedPropertiesSql + "', ");

                sqlStatement.Append ("'" + modifiedAccountInfo.SecurityAuthorityNameSql + "', '" + modifiedAccountInfo.UserAccountIdSql + "', '" + modifiedAccountInfo.UserAccountNameSql + "'");


                success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }

                SetIdentity ();

                success = true;

                application.EnvironmentDatabase.CommitTransaction ();

            }

            catch (Exception applicationException) {

                success = false;

                application.EnvironmentDatabase.RollbackTransaction ();

                application.SetLastException (applicationException);

            }

            return success;

        }

        #endregion


        #region Public 
        
        public Boolean Process (Application application) {

            Boolean success = true;


            String executeStatement = String.Empty;

            
            // ProcessLog_StartProcess ();

            executeStatement = "EXEC dal.MetricProcess_CostMetric " + Id.ToString () + ", "; // STORED PROCEDURE STORED IN DAL TO ALLOW EASY/QUICK ACCESS TO CLAIM DATA

            executeStatement += "'" + modifiedAccountInfo.SecurityAuthorityNameSql + "', '" + modifiedAccountInfo.UserAccountIdSql + "', '" + modifiedAccountInfo.UserAccountNameSql + "'";


            success = application.EnvironmentDatabase.ExecuteSqlStatement (executeStatement);

            // ProcessLog_StopProcess ((success) ? "Success" : "Failure", "");

            return success;

        }

        #endregion 


        #region Private Methods - Process Logs

        protected void ProcessLog_StartProcess () {

            if (base.application == null) { return; }

            String insertStatement = String.Empty;

            Boolean success;

            Mercury.Server.Data.AuthorityAccountStamp accountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application);

            try {

                base.application.EnvironmentDatabase.BeginTransaction ();

                insertStatement = "INSERT INTO logs.MetricProcess (MetricId, MetricName, StartDate, ";

                insertStatement = insertStatement + "CreateAuthorityName, CreateAccountId, CreateAccountName, CreateDate, ";

                insertStatement = insertStatement + "ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate) ";

                insertStatement = insertStatement + "VALUES (";

                insertStatement = insertStatement + Id.ToString () + ", '" + NameSql + "', '" + DateTime.Now.ToString () + "', ";

                insertStatement = insertStatement + "'" + accountInfo.SecurityAuthorityNameSql + "', '" + accountInfo.UserAccountIdSql + "', '" + accountInfo.UserAccountNameSql + "', '" + DateTime.Now.ToString () + "', ";

                insertStatement = insertStatement + "'" + accountInfo.SecurityAuthorityNameSql + "', '" + accountInfo.UserAccountIdSql + "', '" + accountInfo.UserAccountNameSql + "', '" + DateTime.Now.ToString () + "'";

                insertStatement = insertStatement + ")";


                success = base.application.EnvironmentDatabase.ExecuteSqlStatement (insertStatement.ToString (), 0);

                if (!success) { throw base.application.EnvironmentDatabase.LastException; }


                processLogId = 0;

                if (processLogId == 0) { // RESET DOCUMENT ID CRITERIA

                    Object identity = base.application.EnvironmentDatabase.ExecuteScalar ("SELECT @@IDENTITY").ToString ();

                    if (!Int64.TryParse ((String)identity, out processLogId)) {

                        throw new ApplicationException ("Unable to retreive unique id.");

                    }

                }

                base.application.EnvironmentDatabase.CommitTransaction ();

            }

            catch (Exception logException) {

                System.Diagnostics.Trace.WriteLine (insertStatement);

                System.Diagnostics.Trace.WriteLine (logException);

                System.Diagnostics.Trace.Flush ();

                base.application.EnvironmentDatabase.RollbackTransaction ();

            }

            return;

        }

        protected void ProcessLog_StopProcess (String outcome, String exceptionMessage) {

            if (base.application == null) { return; }

            String updateStatement = String.Empty;

            Boolean success;

            Mercury.Server.Data.AuthorityAccountStamp accountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application);

            try {

                updateStatement = "UPDATE logs.MetricProcess \r\n  SET ";

                updateStatement = updateStatement + "\r\n     EndDate = '" + DateTime.Now.ToString () + "', ";

                updateStatement = updateStatement + "\r\n     Outcome = '" + outcome + "', ";

                updateStatement = updateStatement + "\r\n     Exception = '" + exceptionMessage + "'";

                updateStatement = updateStatement + "\r\n  WHERE ProcessLogId = " + processLogId.ToString ();


                success = base.application.EnvironmentDatabase.ExecuteSqlStatement (updateStatement.ToString (), 0);

                if (!success) { throw base.application.EnvironmentDatabase.LastException; }

            }

            catch (Exception logException) {

                System.Diagnostics.Trace.WriteLine (logException);

                System.Diagnostics.Trace.Flush ();

            }

            return;

        }

        protected void ProcessStep_StartStep (String stepName, String stepDescription, String debug) {

            String insertStatement = String.Empty;

            Boolean success;


            Mercury.Server.Data.AuthorityAccountStamp accountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application);

            try {

                base.application.EnvironmentDatabase.BeginTransaction ();

                insertStatement = "INSERT INTO logs.MetricProcessStep (ProcessLogId, StepName, StepDescription, StartDate, Debug, ";

                insertStatement = insertStatement + "CreateAuthorityName, CreateAccountId, CreateAccountName, CreateDate, ";

                insertStatement = insertStatement + "ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate) ";

                insertStatement = insertStatement + "VALUES (";

                insertStatement = insertStatement + processLogId.ToString () + ", '" + CommonFunctions.SetValueMaxLength (stepName, 60) + "', '" + CommonFunctions.SetValueMaxLength (stepDescription, 120) + "', '" + DateTime.Now.ToString () + "', ";

                insertStatement = insertStatement + "'" + CommonFunctions.SetValueMaxLength (debug.Replace ("'", "''"), 3000) + "', ";

                insertStatement = insertStatement + "'" + accountInfo.SecurityAuthorityNameSql + "', '" + accountInfo.UserAccountIdSql + "', '" + accountInfo.UserAccountNameSql + "', '" + DateTime.Now.ToString () + "', ";

                insertStatement = insertStatement + "'" + accountInfo.SecurityAuthorityNameSql + "', '" + accountInfo.UserAccountIdSql + "', '" + accountInfo.UserAccountNameSql + "', '" + DateTime.Now.ToString () + "'";

                insertStatement = insertStatement + ")";


                success = base.application.EnvironmentDatabase.ExecuteSqlStatement (insertStatement.ToString (), 0);

                if (!success) { throw base.application.EnvironmentDatabase.LastException; }


                processStepId = 0;

                if (processStepId == 0) { // RESET DOCUMENT ID CRITERIA

                    Object identity = base.application.EnvironmentDatabase.ExecuteScalar ("SELECT @@IDENTITY").ToString ();

                    if (!Int64.TryParse ((String)identity, out processStepId)) {

                        throw new ApplicationException ("Unable to retreive unique id.");

                    }

                }

                base.application.EnvironmentDatabase.CommitTransaction ();

            }

            catch (Exception logException) {

                System.Diagnostics.Trace.WriteLine (insertStatement);

                System.Diagnostics.Trace.WriteLine (logException);

                System.Diagnostics.Trace.Flush ();

                base.application.EnvironmentDatabase.RollbackTransaction ();

            }

            return;

        }

        protected void ProcessStep_StopStep (String outcome, String exceptionMessage) {

            String updateStatement = String.Empty;

            Boolean success;

            Mercury.Server.Data.AuthorityAccountStamp accountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application);

            try {

                updateStatement = "UPDATE logs.MetricProcessStep \r\n  SET ";

                updateStatement = updateStatement + "\r\n     EndDate = '" + DateTime.Now.ToString () + "', ";

                updateStatement = updateStatement + "\r\n     Outcome = '" + CommonFunctions.SetValueMaxLength (outcome.Replace ("'", "''"), 60) + "', ";

                updateStatement = updateStatement + "\r\n     Exception = '" + CommonFunctions.SetValueMaxLength (exceptionMessage.Replace ("'", "''"), 999) + "'";

                updateStatement = updateStatement + "\r\n  WHERE ProcessStepId = " + processStepId.ToString ();


                success = base.application.EnvironmentDatabase.ExecuteSqlStatement (updateStatement.ToString (), 0);

                if (!success) { throw base.application.EnvironmentDatabase.LastException; }

            }

            catch (Exception logException) {

                System.Diagnostics.Trace.WriteLine (updateStatement);

                System.Diagnostics.Trace.WriteLine (logException);

                System.Diagnostics.Trace.Flush ();

                base.application.EnvironmentDatabase.RollbackTransaction ();

            }

            return;

        }

        #endregion

    }

}
