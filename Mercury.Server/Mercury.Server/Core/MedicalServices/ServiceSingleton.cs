using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.MedicalServices {

    [DataContract (Name = "ServiceSingleton")]
    public class ServiceSingleton : Service {

        #region Private Properties

        [DataMember (Name = "Definitions")]
        List<Definitions.ServiceSingletonDefinition> definitions = new List<Mercury.Server.Core.MedicalServices.Definitions.ServiceSingletonDefinition> ();

        private List<Definitions.ServiceSingletonDefinition> activeDefinitions = new List<Mercury.Server.Core.MedicalServices.Definitions.ServiceSingletonDefinition> ();

        #endregion


        #region Public Properties

        public List<Definitions.ServiceSingletonDefinition> Definitions { get { return definitions; } set { definitions = value; } }

        public List<Definitions.ServiceSingletonDefinition> ActiveDefinitions {

            get {

                if (activeDefinitions != null) {

                    if (activeDefinitions.Count > 0) { return activeDefinitions; }

                }

                activeDefinitions = new List<Mercury.Server.Core.MedicalServices.Definitions.ServiceSingletonDefinition> ();

                foreach (Definitions.ServiceSingletonDefinition currentDefinition in definitions) {

                    if (currentDefinition.Enabled) { activeDefinitions.Add (currentDefinition); }

                }

                return activeDefinitions;

            }

        }


        public override Application Application {

            set {

                 base.Application = value;

                 foreach (MedicalServices.Definitions.ServiceSingletonDefinition currentDefinition in definitions) {

                     currentDefinition.Application = value;

                 }

            }

        } 

        #endregion 


        #region Constructors

        public ServiceSingleton (Application application) { BaseConstructor (application); return; }

        public ServiceSingleton (Application application, Int64 forServiceId) {

            BaseConstructor (application, forServiceId);

            return;

        }

        #endregion


        #region Xml Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode definitionsNode = document.CreateElement ("Definitions");

            document.ChildNodes[1].AppendChild (definitionsNode);


            foreach (MedicalServices.Definitions.ServiceSingletonDefinition currentDefinition in definitions) {

                try {

                    currentDefinition.Application = base.application;

                    System.Xml.XmlNode definitionNode = currentDefinition.XmlSerialize ().LastChild;

                    definitionsNode.AppendChild (document.ImportNode (definitionNode, true));

                }

                catch (Exception applicationException) {

                    System.Diagnostics.Debug.WriteLine (applicationException.Message);

                }

            }


            return document;

        }

        public override List<ImportExport.Result> XmlImport (System.Xml.XmlNode objectNode) {

            List<ImportExport.Result> response = base.XmlImport (objectNode);

            System.Xml.XmlNode definitionsNode;

            String exceptionMessage = String.Empty;


            try {

                definitionsNode = objectNode.SelectSingleNode ("Definitions");

                foreach (System.Xml.XmlNode currentDefinitionNode in definitionsNode.ChildNodes) {

                    MedicalServices.Definitions.ServiceSingletonDefinition definition = new Mercury.Server.Core.MedicalServices.Definitions.ServiceSingletonDefinition (application);

                    response.AddRange (definition.XmlImport (currentDefinitionNode));

                    definition.ServiceId = this.Id;

                    definitions.Add (definition);

                }

                if (!Save ()) { throw new ApplicationException ("Unable to import Service Singleton."); }

            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return response;

        }

        #endregion 


        #region Public Database Methods

        override public Boolean Load (Int64 forId) {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            System.Data.DataTable definitionTable;


            success = base.Load (forId);

            if (success) {

                sqlStatement.Append ("SELECT * FROM ServiceSingletonDefinition WHERE ServiceId = " + forId);

                definitionTable = base.application.EnvironmentDatabase.SelectDataTable (sqlStatement.ToString ());

                foreach (System.Data.DataRow currentRow in definitionTable.Rows) {

                    MedicalServices.Definitions.ServiceSingletonDefinition singletonDefinition = new Mercury.Server.Core.MedicalServices.Definitions.ServiceSingletonDefinition (base.application);

                    singletonDefinition.MapDataFields (currentRow);

                    definitions.Add (singletonDefinition);

                }

            }


            return success;

        }

        //public override void MapDataFields (System.Data.DataRow currentRow) {

        //    base.MapDataFields (currentRow, "Service");

        //    return;

        //}

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement;


            try {

                base.application.EnvironmentDatabase.BeginTransaction ();

                success = base.Save ();

                if (!success) { throw new ApplicationException (base.application.LastException.Message); }



                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("DELETE FROM ServiceSingletonDefinition WHERE ServiceId = " + Id.ToString ());

                if (definitions.Count > 0) {

                    String definitionIdString = String.Empty;

                    foreach (MedicalServices.Definitions.ServiceSingletonDefinition currentDefinition in definitions) {

                        definitionIdString = definitionIdString + "{" + currentDefinition.Id.ToString () + "}";

                    }

                    if (definitionIdString.Length != 0) {

                        definitionIdString = definitionIdString.Replace ("}{", ", ");

                        definitionIdString = definitionIdString.Replace ("{", "(");

                        definitionIdString = definitionIdString.Replace ("}", ")");

                        definitionIdString = " AND ServiceSingletonDefinitionId NOT IN " + definitionIdString;

                    }

                    sqlStatement.Append (definitionIdString);

                }

                success = base.application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

                if (!success) { throw base.application.EnvironmentDatabase.LastException; }


                foreach (MedicalServices.Definitions.ServiceSingletonDefinition singletonDefinition in definitions) {

                    singletonDefinition.Application = base.application;

                    singletonDefinition.ServiceId = Id;

                    success = singletonDefinition.Save (application);

                    if (!success) { throw base.application.EnvironmentDatabase.LastException; }

                }

                base.application.EnvironmentDatabase.CommitTransaction ();

                success = true;

            }

            catch (Exception applicationException) {

                base.application.EnvironmentDatabase.RollbackTransaction ();

                base.application.SetLastException (applicationException);

            }

            return success;

        }

        public override Boolean Delete () {

            Boolean success = false;
            
            StringBuilder sqlStatement;


            try {

                base.application.EnvironmentDatabase.BeginTransaction ();


                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("DELETE FROM ServiceSingletonDefinition WHERE ServiceId = " + Id.ToString ());

                success = base.application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

                if (!success) { throw base.application.EnvironmentDatabase.LastException; }


                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("DELETE FROM Service WHERE ServiceId = " + Id.ToString ());

                success = base.application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

                if (!success) { throw base.application.EnvironmentDatabase.LastException; }


                base.application.EnvironmentDatabase.CommitTransaction ();

                success = true;

            }

            catch (Exception applicationException) {

                base.application.EnvironmentDatabase.RollbackTransaction ();

                base.application.SetLastException (applicationException);

            }

            return success;

            
        }

        #endregion


        #region Public Methods

        public List<MemberServiceDetailSingleton> Preview (Application application) {

            List<MemberServiceDetailSingleton> previewResults = new List<MemberServiceDetailSingleton> ();

            MemberServiceDetailSingleton detailResult;

            System.Data.DataTable resultsTable;

            String sqlStatement = String.Empty;

            foreach (MedicalServices.Definitions.ServiceSingletonDefinition currentDefinition in definitions) {

                if ((currentDefinition.Enabled) && (currentDefinition.DataSourceType != Mercury.Server.Core.MedicalServices.Enumerations.ServiceDataSourceType.Custom)) {

                    sqlStatement = currentDefinition.SqlStatement;

                    if (!String.IsNullOrEmpty (sqlStatement)) {

                        sqlStatement = sqlStatement.Replace ("SELECT", "SELECT TOP 10");

                        resultsTable = application.EnvironmentDatabase.SelectDataTable (sqlStatement, 0);

                        foreach (System.Data.DataRow currentRow in resultsTable.Rows) {

                            detailResult = new MemberServiceDetailSingleton (0, currentDefinition.Id);

                            detailResult.MapDataFields (currentRow);

                            previewResults.Add (detailResult);

                        }

                    }

                }

            }

            return previewResults;

        }

        public Boolean Process (Application application) {

            Boolean success = true;

            DateTime newLastPaidDate = LastPaidDate;

            Boolean lastPaidDateChanged = false;

            Int64 memberServiceId;

            MemberService memberService;

            MemberServiceDetailSingleton memberServiceDetail;

            
            Int64 memberMetricId;

            Metrics.MemberMetric memberMetric = null;

            
            System.Data.DataTable resultsTable;

            String selectStatement = String.Empty;

            String procedureStatement = String.Empty;

            Metrics.Metric labMetric = null;


            base.ProcessLog_StartProcess ();

            foreach (MedicalServices.Definitions.ServiceSingletonDefinition currentDefinition in definitions) {

                if (currentDefinition.Enabled) {

                    if (currentDefinition.DataSourceType == Enumerations.ServiceDataSourceType.Lab) {

                        labMetric = application.MetricGet (currentDefinition.LabMetricId);

                    }

                    selectStatement = currentDefinition.SqlStatement;

                    if (!String.IsNullOrEmpty (selectStatement)) {

                        if (currentDefinition.DataSourceType != Mercury.Server.Core.MedicalServices.Enumerations.ServiceDataSourceType.Custom) {

                            selectStatement = selectStatement + "  AND (Claim.PaidDate >= '" + LastPaidDate.ToString ("MM/dd/yyyy") + "') AND (Claim.MemberId IS NOT NULL) ORDER BY PaidDate";

                        }

                        else {

                            selectStatement = selectStatement + " '" + LastPaidDate.ToString ("MM/dd/yyyy") + "'";

                        }

                        resultsTable = application.EnvironmentDatabase.SelectDataTable (selectStatement, 0);

                        foreach (System.Data.DataRow currentRow in resultsTable.Rows) {

                            memberServiceId = application.MemberServiceGetId ((Int64) currentRow["MemberId"], Id, (DateTime) currentRow["EventDate"]);

                            if (memberServiceId == 0) {

                                memberService = new MemberService (application);

                                memberService.MemberId = (Int64) currentRow["MemberId"];

                                memberService.ServiceId = Id;

                                memberService.EventDate = (DateTime) currentRow["EventDate"];

                                memberService.AddedManually = false;

                                success = memberService.Save (application);

                                memberServiceId = memberService.Id;

                            }

                            if ((success) && (memberServiceId != 0)) {

                                memberServiceDetail = new MemberServiceDetailSingleton (memberServiceId, currentDefinition.Id);

                                memberServiceDetail.MapDataFields (currentRow);

                                // THE STORED PROCEDURE IS RESPONSIBLE FOR ENSURING NO 

                                // DUPLICATE DETAIL ROWS EXISTS UPON INSERT

                                success = memberServiceDetail.Save (application);

                                if (newLastPaidDate < (DateTime) currentRow["PaidDate"]) { 
                                    
                                    newLastPaidDate = (DateTime) currentRow["PaidDate"]; 

                                    lastPaidDateChanged = true;
                                
                                }

                            }

                            // IF THIS IS LAB AND THE LAB HAS AN ASSOCIATED METRIC VALUE, TRY TO CREATE THE MEMBER METRIC

                            if ((currentDefinition.DataSourceType == Enumerations.ServiceDataSourceType.Lab) && (labMetric != null) && !(currentRow ["LabValue"] is DBNull)) {

                                memberMetricId = application.MemberMetricGetId ((Int64)currentRow["MemberId"], labMetric.Id, (DateTime)currentRow["EventDate"]);

                                if (memberMetricId == 0) {

                                    memberMetric = new Metrics.MemberMetric (application);

                                    memberMetric.MemberId = (Int64)currentRow["MemberId"];

                                    memberMetric.MetricId = labMetric.Id;

                                    memberMetric.EventDate = (DateTime)currentRow["EventDate"];

                                    memberMetric.MetricValue = Convert.ToDecimal (currentRow["LabValue"]);

                                    memberMetric.AddedManually = false;

                                    success = memberMetric.Save (application);

                                    memberMetricId = memberMetric.Id;

                                }

                                else {

                                    System.Diagnostics.Debug.WriteLine (labMetric.Name);

                                }

                            }

                            if (!success) { break; }

                        }

                    }

                }

                if (!success) { break; }

            }

            if ((success) && (lastPaidDateChanged)) { 

                // TAKE LAST FOUND PAID DATE AND ADD ONE DAY 

                // SINCE THE QUERY LOOKS FOR PAID DATE >= 

                LastPaidDate = newLastPaidDate.AddDays (1);

                UpdateLastPaidDate (); 
            
            }

            base.ProcessLog_StopProcess ((success) ? "Success" : "Failure", "");

            return success;

        }

        #endregion

    }

}
