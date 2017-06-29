using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

namespace Mercury.Server.Core.AuthorizedServices {

    [DataContract (Name = "AuthorizedService")]
    public class AuthorizedService : CoreConfigurationObject  {

        #region Private Properties

        [DataMember (Name = "Definitions")]
        List<AuthorizedServiceDefinition> definitions = new List<AuthorizedServiceDefinition> ();


        private Int64 processLogId = 0;

        private Int64 processStepId = 0;

        #endregion
        

        #region Public Properties

        public List<AuthorizedServiceDefinition> Definitions { get { return definitions; } set { definitions = value; } }

        public List<AuthorizedServiceDefinition> ActiveDefinitions {

            get {

                if (definitions == null) { definitions = new List<AuthorizedServiceDefinition> (); }


                var activeDefinitions =

                    from authorizedServiceDefinition in definitions

                    where authorizedServiceDefinition.Enabled

                    select authorizedServiceDefinition;

                return activeDefinitions.ToList ();

            }

        }

        #endregion


        #region Constructors

        public AuthorizedService () { /* DO NOTHING */ }

        public AuthorizedService (Application applicationReference) { BaseConstructor (applicationReference); return; }

        public AuthorizedService (Application applicationReference, Int64 forAuthorizedServiceId) {

            BaseConstructor (applicationReference, forAuthorizedServiceId);

            return;

        }

        #endregion

        
        #region XML Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode definitionsNode = document.CreateElement ("Definitions");

            document.LastChild.AppendChild (definitionsNode);


            foreach (AuthorizedServiceDefinition currentDefinition in definitions) {

                definitionsNode.AppendChild (document.ImportNode (currentDefinition.XmlSerialize ().LastChild, true)); 

            }


            return document;

        }

        public override List<ImportExport.Result> XmlImport (System.Xml.XmlNode objectNode) {

            List<ImportExport.Result> response = base.XmlImport (objectNode);


            try {

                foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {

                    switch (currentNode.Name) {

                        case "Properties":
                            
                            break;

                        case "Definitions":

                            foreach (System.Xml.XmlNode currentDefinitionNode in currentNode.ChildNodes) {

                                AuthorizedServiceDefinition definition = new AuthorizedServiceDefinition (application);

                                response.AddRange (definition.XmlImport (currentDefinitionNode));

                                definitions.Add (definition);

                            }

                            break;

                    } // switch (currentNode.Attributes["Name"].InnerText) {

                } // foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {


                // SAVE IMPORTED CLASS

                if (!Save ()) { throw new ApplicationException ("Unable to save Authorized Service: " + Name + "."); }

            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return response;

        }

        #endregion 

       
        #region Database Functions

        override public Boolean Load (Int64 forId) {

            Boolean success = false;

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable tableService;

            if (base.application.EnvironmentDatabase == null) { return false; }

            selectStatement.Append ("SELECT * FROM AuthorizedService WHERE AuthorizedServiceId = " + forId.ToString ());

            tableService = base.application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            if (tableService.Rows.Count == 1) {

                MapDataFields (tableService.Rows[0]);


                selectStatement = new StringBuilder ();

                selectStatement.Append ("SELECT * FROM AuthorizedServiceDefinition WHERE AuthorizedServiceId = " + forId);

                System.Data.DataTable definitionTable = base.application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

                foreach (System.Data.DataRow currentRow in definitionTable.Rows) {

                    AuthorizedServiceDefinition currentDefinition = new AuthorizedServiceDefinition (application);

                    currentDefinition.MapDataFields (currentRow);

                    definitions.Add (currentDefinition);

                }

                success = true;

            }

            else {

                success = false;

            }

            return success;

        }

        override public void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            // TODO: ? SHOULD DEFINITION LOAD BE HERE?
            
            return;

        }

        override public Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement;


            if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.ProblemStatementManage)) { throw new ApplicationException ("PermissionDenied"); }


            ModifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application);

            try {

                application.EnvironmentDatabase.BeginTransaction ();

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC AuthorizedService_InsertUpdate ");

                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append ("'" + NameSql + "', ");

                sqlStatement.Append ("'" + DescriptionSql + "', ");

                sqlStatement.Append (Convert.ToInt32 (Enabled).ToString () + ", ");

                sqlStatement.Append (Convert.ToInt32 (Visible).ToString () + ", ");


                sqlStatement.Append ("'" + ExtendedPropertiesSql + "', ");

                sqlStatement.Append ("'" + modifiedAccountInfo.SecurityAuthorityNameSql + "', '" + modifiedAccountInfo.UserAccountIdSql + "', '" + modifiedAccountInfo.UserAccountNameSql + "'");


                success = base.application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                if (!success) {

                    application.SetLastException (base.application.EnvironmentDatabase.LastException);

                    throw new ApplicationException (base.application.EnvironmentDatabase.LastException.Message);

                }

                SetIdentity ();

                // SAVE DEFINITIONS

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("DELETE FROM AuthorizedServiceDefinition WHERE AuthorizedServiceId = " + Id.ToString ());

                if (definitions.Count > 0) {

                    String definitionIdString = String.Empty;

                    foreach (AuthorizedServiceDefinition currentDefinition in definitions) {

                        definitionIdString = definitionIdString + "{" + currentDefinition.Id.ToString () + "}";

                    }

                    if (definitionIdString.Length != 0) {

                        definitionIdString = definitionIdString.Replace ("}{", ", ");

                        definitionIdString = definitionIdString.Replace ("{", "(");

                        definitionIdString = definitionIdString.Replace ("}", ")");

                        definitionIdString = " AND AuthorizedServiceDefinitionId NOT IN " + definitionIdString;

                    }

                    sqlStatement.Append (definitionIdString);

                }

                success = base.application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

                if (!success) { throw base.application.EnvironmentDatabase.LastException; }


                foreach (AuthorizedServiceDefinition currentDefinition in definitions) {

                    currentDefinition.Application = base.application;

                    currentDefinition.AuthorizedServiceId = Id;

                    success = currentDefinition.Save (application);

                    if (!success) { throw base.application.EnvironmentDatabase.LastException; }

                }

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

        virtual public Boolean Delete () {

            throw new ApplicationException ("Not Implemented.");

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

                insertStatement = "INSERT INTO logs.ServiceProcess (ServiceId, ServiceName, StartDate, ";

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

                if (processLogId == 0) { // RESET ID 

                    Object identity = base.application.EnvironmentDatabase.ExecuteScalar ("SELECT @@IDENTITY").ToString ();

                    if (!Int64.TryParse ((String) identity, out processLogId)) {

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

                updateStatement = "UPDATE logs.ServiceProcess \r\n  SET ";

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

                insertStatement = "INSERT INTO logs.ServiceProcessStep (ProcessLogId, StepName, StepDescription, StartDate, Debug, ";

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

                    if (!Int64.TryParse ((String) identity, out processStepId)) {

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

                updateStatement = "UPDATE logs.ServiceProcessStep \r\n  SET ";

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


        #region Public Methods

        public List<MemberAuthorizedServiceDetail> Preview (Application application) {

            List<MemberAuthorizedServiceDetail> previewResults = new List<MemberAuthorizedServiceDetail> ();

            MemberAuthorizedServiceDetail detailResult;

            System.Data.DataTable resultsTable;

            String sqlStatement = String.Empty;

            foreach (AuthorizedServiceDefinition currentDefinition in definitions) {

                if (currentDefinition.Enabled) {

                    sqlStatement = currentDefinition.SqlStatement;

                    if (!String.IsNullOrEmpty (sqlStatement)) {

                        sqlStatement = sqlStatement.Replace ("SELECT", "SELECT TOP 10");

                        sqlStatement = sqlStatement.Replace ("AND (([Authorization].TerminationDate > DATEADD (DAY, -2, GETDATE ()))) ", "");

                        resultsTable = application.EnvironmentDatabase.SelectDataTable (sqlStatement, 0);

                        foreach (System.Data.DataRow currentRow in resultsTable.Rows) {

                            detailResult = new MemberAuthorizedServiceDetail (0, currentDefinition.Id);

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

            Int64 memberAuthorizedServiceId;

            MemberAuthorizedService memberAuthorizedService;

            MemberAuthorizedServiceDetail memberAuthorizedServiceDetail;


            System.Data.DataTable resultsTable;

            String selectStatement = String.Empty;

            String procedureStatement = String.Empty;

            ProcessLog_StartProcess ();

            foreach (AuthorizedServiceDefinition currentDefinition in definitions) {

                if (currentDefinition.Enabled) {

                    selectStatement = currentDefinition.SqlStatement;

                    if (!String.IsNullOrEmpty (selectStatement)) {

                        resultsTable = application.EnvironmentDatabase.SelectDataTable (selectStatement, 0);

                        foreach (System.Data.DataRow currentRow in resultsTable.Rows) {

                            memberAuthorizedServiceId = application.MemberAuthorizedServiceGetId ((Int64) currentRow["MemberId"], Id, (DateTime) currentRow ["EventDate"]);

                            if (memberAuthorizedServiceId == 0) {

                                memberAuthorizedService = new MemberAuthorizedService (application);

                                memberAuthorizedService.MemberId = (Int64) currentRow["MemberId"];

                                memberAuthorizedService.AuthorizedServiceId = Id;

                                memberAuthorizedService.EventDate = (DateTime) currentRow["EventDate"];

                                memberAuthorizedService.InitialIdentifiedDate = DateTime.Today;

                                memberAuthorizedService.AddedManually = false;

                                success = memberAuthorizedService.Save (application);

                                memberAuthorizedServiceId = memberAuthorizedService.Id;

                            }

                            if ((success) && (memberAuthorizedServiceId != 0)) {

                                memberAuthorizedServiceDetail = new MemberAuthorizedServiceDetail (memberAuthorizedServiceId, currentDefinition.Id);

                                memberAuthorizedServiceDetail.MapDataFields (currentRow);

                                memberAuthorizedServiceDetail.MemberAuthorizedServiceId = memberAuthorizedServiceId;

                                success = memberAuthorizedServiceDetail.Save (application);

                            }

                            if (!success) { break; }

                        }

                    }

                }

                if (!success) { break; }

            }

            ProcessLog_StopProcess ((success) ? "Success" : "Failure", "");

            return success;

        }

        #endregion

    }

}
