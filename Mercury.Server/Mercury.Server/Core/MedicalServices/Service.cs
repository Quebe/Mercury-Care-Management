using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.MedicalServices {

    [DataContract (Name = "Service")]
    [KnownType (typeof (ServiceSingleton))]
    [KnownType (typeof (ServiceSet))]
    public class Service : CoreConfigurationObject {

        #region Private Properties
        
        [DataMember (Name = "ServiceType")]
        private Enumerations.MedicalServiceType serviceType = Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType.Singleton;

        [DataMember (Name = "ServiceClassification")]
        private Enumerations.ServiceClassification serviceClassification = Mercury.Server.Core.MedicalServices.Enumerations.ServiceClassification.NotSpecified;

        [DataMember (Name = "LastPaidDate")]
        private DateTime lastPaidDate = new DateTime (1900, 01, 01);


        // SERVICE SET SPECIFIC PROPERTIES

        [DataMember (Name = "SetType")]
        private Enumerations.ServiceSetType setType = Enumerations.ServiceSetType.Intersection;

        [DataMember (Name = "WithinDays")]
        private Int32 withinDays = 0;


        private Int64 processLogId = 0;

        private Int64 processStepId = 0;

        #endregion


        #region Public Properties

        public String ServiceName { get { return Name; } set { Name = value; } } // BACKWARDS COMPATIBILITY 
        
        public Enumerations.MedicalServiceType ServiceType { get { return serviceType; } set { serviceType = value; } }

        public Enumerations.ServiceClassification ServiceClassification { get { return serviceClassification; } set { serviceClassification = value; } }

        public DateTime LastPaidDate { get { return lastPaidDate; } set { lastPaidDate = value; } }


        public Enumerations.ServiceSetType SetType { get { return setType; } set { setType = value; } }

        public Int32 WithinDays { get { return withinDays; } set { withinDays = value; } }

        #endregion


        #region Constructors

        protected Service () { /* DO NOTHING */ } // REQUIRED FOR INHERITANCE

        public Service (Application applicationReference) { BaseConstructor (applicationReference); return; }

        public Service (Application applicationReference, Int64 forServiceId) {

            BaseConstructor (applicationReference, forServiceId);

            return;

        }
        
        #endregion


        #region Xml Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];



            #region Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ServiceType", ((Int32) serviceType).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ServiceTypeDescription", serviceType.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ServiceClassification", ((Int32)ServiceClassification).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ServiceClassificationDescription", serviceClassification.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "LastPaidDate", lastPaidDate.ToString ("MM/dd/yyyy"));

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "SetType", ((Int32)setType).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "SetTypeDescription", setType.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "WithinDays", withinDays.ToString ());

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

                        case "ServiceType": ServiceType = (Enumerations.MedicalServiceType)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                        case "ServiceClassification": ServiceClassification = (Enumerations.ServiceClassification)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                        case "LastPaidDate": LastPaidDate = Convert.ToDateTime (currentPropertyNode.InnerText); break;

                        case "SetType": SetType = (Enumerations.ServiceSetType)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                        case "WithinDays": WithinDays = Convert.ToInt32 (currentPropertyNode.InnerText); break;

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

            Dictionary<String, String> validationResponse = Validate (this);


            // VALIDATE UNIQUE INSTANCE
            Int64 duplicateId = application.CoreObjectGetIdByName ("Service", Name);

            if ((duplicateId != 0) && (duplicateId != Id)) { validationResponse.Add ("Duplicate", "Duplicate Found."); }


            return validationResponse;

        }

        #endregion 


        #region Database Functions

        public override Boolean Load (Int64 forId) { return LoadFromTable ("Service", "ServiceId", forId); } 
    
        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow, "Service");


            serviceType = (Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType) ((Int32) currentRow["ServiceType"]);

            serviceClassification = (Mercury.Server.Core.MedicalServices.Enumerations.ServiceClassification) ((Int32) currentRow["ServiceClassification"]);

            lastPaidDate = (DateTime) (currentRow["LastPaidDate"]);



            setType = (Enumerations.ServiceSetType)Convert.ToInt32 (currentRow["SetType"]);

            withinDays = Convert.ToInt32 (currentRow["SetWithinDays"]);

            return;

        }

        override public Boolean Save () {
            
            Boolean success = false;

            StringBuilder sqlStatement;
            

            ModifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application);

            try {

                if (lastPaidDate < new DateTime (1900, 1, 1)) { lastPaidDate = new DateTime (1900, 1, 1); }

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC Service_InsertUpdate ");

                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append ("'" + NameSql + "', ");

                sqlStatement.Append ("'" + DescriptionSql + "', ");

                sqlStatement.Append (((Int32) serviceType).ToString () + ", ");

                sqlStatement.Append (((Int32) serviceClassification).ToString () + ", ");

                sqlStatement.Append ("'" + lastPaidDate.ToString () + "', ");


                sqlStatement.Append (((Int32)setType).ToString () + ", ");

                sqlStatement.Append (((Int32)withinDays).ToString () + ", ");

                
                sqlStatement.Append ("'" + ExtendedPropertiesSql + "', ");


                sqlStatement.Append (Convert.ToInt32 (Enabled).ToString () + ", ");

                sqlStatement.Append (Convert.ToInt32 (Visible).ToString () + ", ");


                sqlStatement.Append ("'" + modifiedAccountInfo.SecurityAuthorityNameSql + "', '" + modifiedAccountInfo.UserAccountIdSql + "', '" + modifiedAccountInfo.UserAccountNameSql + "'");


                success = base.application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

                if (!success) {

                    application.SetLastException (base.application.EnvironmentDatabase.LastException);

                    throw new ApplicationException (base.application.EnvironmentDatabase.LastException.Message);

                }

                else { SetIdentity (); }

                success = true;

            }

            catch (Exception applicationException) {

                base.application.SetLastException (applicationException);

            }

            return success;

        }

        virtual public Boolean Delete () {

            throw new ApplicationException ("Not Implemented.");

        }

        public void UpdateLastPaidDate () {

            StringBuilder sqlStatement;


            try {

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("UPDATE Service SET LastPaidDate = '" + LastPaidDate.ToString ("MM/dd/yyyy") + "' WHERE ServiceId = " + Id.ToString ());

                base.application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

            }

            catch (Exception applicationException) {

                base.application.SetLastException (applicationException);

            }

            return;

        }

        #endregion


        #region Public Methods

        virtual public Int32 Depth (Application application) { return 0; }

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

                if (processLogId == 0) { // RESET DOCUMENT ID CRITERIA

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

    }

}
