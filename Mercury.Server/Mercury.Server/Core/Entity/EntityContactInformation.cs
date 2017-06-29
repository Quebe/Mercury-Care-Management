using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Entity {

    [Serializable]
    [DataContract (Name = "EntityContactInformation")]
    public class EntityContactInformation : CoreObject {

        #region Private Properties

        [DataMember (Name = "EntityId")]
        private Int64 entityId;

        [DataMember (Name = "ContactType")]
        private Enumerations.EntityContactType contactType = Mercury.Server.Core.Enumerations.EntityContactType.NotSpecified;

        [DataMember (Name = "ContactSequence")]
        private Int32 contactSequence;

        [DataMember (Name = "Number")]
        private String number;

        [DataMember (Name = "NumberExtension")]
        private String numberExtension;

        [DataMember (Name = "Email")]
        private String email;

        [DataMember (Name = "EffectiveDate")]
        private DateTime effectiveDate;

        [DataMember (Name = "TerminationDate")]
        private DateTime terminationDate;


        private Entity entity = null;

        #endregion


        #region Public Properties

        public Int64 EntityId { get { return entityId; } set { entityId = value; } }

        public Enumerations.EntityContactType ContactType { get { return contactType; } set { contactType = value; } }

        public String ContactTypeDescription { get { return contactType.ToString (); } }

        public Int32 ContactSequence { get { return contactSequence; } set { contactSequence = value; } }

        public String Number { get { return number; } set { number = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.ContactNumber); } }

        public String NumberExtension { get { return numberExtension; } set { numberExtension = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.ContactExtension); } }

        public String NumberFormatted {

            get {

                String numberFormatted = number;

                numberFormatted = numberFormatted.Replace (" ", "");

                numberFormatted = numberFormatted.Replace ("(", "");

                numberFormatted = numberFormatted.Replace (")", "");

                numberFormatted = numberFormatted.Replace ("-", "");

                if (numberFormatted.Length < 10) { numberFormatted = "000" + numberFormatted; }

                String formatPattern = @"(\d{3})(\d{3})(\d{4})";

                numberFormatted = System.Text.RegularExpressions.Regex.Replace (numberFormatted, formatPattern, "($1) $2-$3");

                if (numberExtension.Trim ().Length > 0) {

                    numberFormatted = numberFormatted + " " + numberExtension.Trim ();

                }

                return numberFormatted;

            }

        }

        public String Email { get { return email; } set { email = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.ContactEmail); } }

        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }


        public Entity Entity {

            get {

                if (entity != null) { return entity; }

                if (base.application == null) { return new Entity (base.application); }

                entity = base.application.EntityGet (entityId);

                return entity;

            }

        }

        #endregion
        

        #region Constructors

        public EntityContactInformation (Application applicationReference) {

            BaseConstructor (applicationReference);

            return; 
        
        }

        public EntityContactInformation (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion


        #region Validation Functions

        public override Dictionary<String, String> Validate () {

            Dictionary<String, String> validationResponse = new Dictionary<String, String> ();

            System.Text.RegularExpressions.Regex expressionValidator;

            switch (contactType) {

                case Enumerations.EntityContactType.AlternateTelephone:

                case Enumerations.EntityContactType.EmergencyPhone:

                case Enumerations.EntityContactType.Facsimile:

                case Enumerations.EntityContactType.Mobile:

                case Enumerations.EntityContactType.Pager:

                case Enumerations.EntityContactType.Telephone:

                    if (!String.IsNullOrWhiteSpace (email)) { validationResponse.Add ("Email", "Email address not valid in this context."); }

                    // REGEX TO CHECH PHONE NUMBER

                    expressionValidator = new System.Text.RegularExpressions.Regex (@"^\d{3}\d{3}\d{4}$");

                    if (!expressionValidator.IsMatch (number)) { validationResponse.Add ("Number", "Number is not valid."); }

                    break;

                case Enumerations.EntityContactType.Email:

                    if (!String.IsNullOrWhiteSpace (number)) { validationResponse.Add ("Number", "Number is not valid in this context."); }

                    if (!String.IsNullOrWhiteSpace (numberExtension)) { validationResponse.Add ("Extension", "Extension is not valid in this context."); }

                    // CHECK EMAIL ADDRESS WITH REGEX

                    expressionValidator = new System.Text.RegularExpressions.Regex (@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");

                    if (!expressionValidator.IsMatch (email)) { validationResponse.Add ("Email", "Email address is not valid."); }
                    
                    break;

            }

            if (terminationDate < effectiveDate) { validationResponse.Add ("Termination Date", "Termination Date must be greater or equal to the Effective Date."); }

            return validationResponse;

        }

        #endregion


        #region Data Functions

        public override Boolean Load (Int64 forId) { return LoadFromDalSp (forId); }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            entityId = (Int64) currentRow["EntityId"];

            contactType = (Mercury.Server.Core.Enumerations.EntityContactType) (Int32) currentRow["ContactType"];

            contactSequence = (Int32) currentRow["ContactSequence"];

            number = (String) currentRow["ContactNumber"];

            numberExtension = (String) currentRow["ContactExtension"];

            email = (String) currentRow["ContactEmail"];

            effectiveDate = (DateTime) currentRow["EffectiveDate"];

            terminationDate = (DateTime) currentRow["TerminationDate"];

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 


        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            Boolean usingTransaction = true;


            try {

                Dictionary<String, String> validationResponse = Validate ();

                if (validationResponse.Count != 0) {

                    foreach (String validationKey in validationResponse.Keys) {

                        throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                    }

                }

                modifiedAccountInfo = new Data.AuthorityAccountStamp (application.Session);


                if (application.EnvironmentDatabase.OpenTransactions == 0) {

                    usingTransaction = true;

                    base.application.EnvironmentDatabase.BeginTransaction ();

                }

                // DETERMINE IF THERE ARE ANY OVERLAPPING SEGMENTS

                List<EntityContactInformation> overlappingContactInformationes = application.EntityContactInformationsGetByEntityTypeOverlap (entityId, contactType, contactSequence, effectiveDate, terminationDate, Id);

                if (overlappingContactInformationes.Count > 1) {

                    application.TraceWriteLineError (application.TraceSwitchGeneral, "[EntityContactInformation.Save] " + "Entity Id: " + entityId.ToString () + ", Contact Information Type: " + contactType.ToString () + ", Effective Date: " + effectiveDate.ToString ("MM/dd/yyyy") + ", Termination Date: " + terminationDate.ToString ("MM/dd/yyyy"));

                    throw new ApplicationException ("Unable to save due to multiple overlapping contact information.");

                }

                if (overlappingContactInformationes.Count == 1) {

                    // TERMINATE OVERLAPPAING SEGMENTS IF POSSIBLE, TERMINATION DATE IS EFFECTIVE DATE -1

                    success = overlappingContactInformationes[0].Terminate (effectiveDate.AddDays (-1));

                    if (!success) {

                        application.TraceWriteLineError (application.TraceSwitchGeneral, "[EntityContactInformation.Save] " + "Entity Id: " + entityId.ToString () + ", Contact Information Type: " + contactType.ToString () + ", Effective Date: " + effectiveDate.ToString ("MM/dd/yyyy") + ", Termination Date: " + terminationDate.ToString ("MM/dd/yyyy"));

                        throw new ApplicationException ("Permission Denied. Unable to terminate previous overlapping contact information.");

                    }

                }


                // ATTEMPT TO SAVE NEW OR UPDATE EXISTING ENTITY ADDRESS

                ModifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application);


                System.Data.SqlClient.SqlCommand sqlCommand = application.EnvironmentDatabase.CreateSqlCommand ("dal.EntityContactInformation_InsertUpdate");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                sqlCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@entityContactInformationId", System.Data.SqlDbType.BigInt));

                sqlCommand.Parameters["@entityContactInformationId"].Value = Id;

                sqlCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@entityId", System.Data.SqlDbType.BigInt));

                sqlCommand.Parameters["@entityId"].Value = entityId;

                sqlCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@contactType", System.Data.SqlDbType.Int));

                sqlCommand.Parameters["@contactType"].Value = contactType;

                sqlCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@contactSequence", System.Data.SqlDbType.Int));

                sqlCommand.Parameters["@contactSequence"].Value = contactSequence;


                sqlCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@contactNumber", System.Data.SqlDbType.VarChar, Server.Data.DataTypeConstants.ContactNumber));

                sqlCommand.Parameters["@contactNumber"].Value = number;

                sqlCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@contactExtension", System.Data.SqlDbType.VarChar, Server.Data.DataTypeConstants.ContactExtension));

                sqlCommand.Parameters["@contactExtension"].Value = numberExtension;

                sqlCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@contactEmail", System.Data.SqlDbType.VarChar, Server.Data.DataTypeConstants.ContactEmail));

                sqlCommand.Parameters["@contactEmail"].Value = email;


                sqlCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@effectiveDate", System.Data.SqlDbType.DateTime));

                sqlCommand.Parameters["@effectiveDate"].Value = effectiveDate;

                sqlCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@terminationDate", System.Data.SqlDbType.DateTime));

                sqlCommand.Parameters["@terminationDate"].Value = terminationDate;


                sqlCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@modifiedAuthorityName", System.Data.SqlDbType.VarChar, 60));

                sqlCommand.Parameters["@modifiedAuthorityName"].Value = modifiedAccountInfo.SecurityAuthorityName;

                sqlCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@modifiedAccountId", System.Data.SqlDbType.VarChar, 60));

                sqlCommand.Parameters["@modifiedAccountId"].Value = modifiedAccountInfo.UserAccountId;

                sqlCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@modifiedAccountName", System.Data.SqlDbType.VarChar, 60));

                sqlCommand.Parameters["@modifiedAccountName"].Value = modifiedAccountInfo.UserAccountName;


                if (sqlCommand.ExecuteNonQuery () != 1) {

                    if (application.EnvironmentDatabase.LastException == null) { throw new ApplicationException ("Permission Denied. Unable to Save Contact Information."); }

                    base.application.SetLastException (base.application.EnvironmentDatabase.LastException);

                    throw base.application.EnvironmentDatabase.LastException;

                }

                SetIdentity ();

                success = true;

                if (usingTransaction) { base.application.EnvironmentDatabase.CommitTransaction (); }

            }

            catch (Exception applicationException) {

                success = false;

                if (usingTransaction) { base.application.EnvironmentDatabase.RollbackTransaction (); }

                base.application.SetLastException (applicationException);

            }

            return success;

        }

        #endregion


        #region Public Methods - Data Binding

        public override Dictionary<String, String> DataBindingContexts {

            get {

                Dictionary<String, String> bindingContexts = new Dictionary<String, String> ();

                bindingContexts.Add ("Id", "Id|EntityContact");

                bindingContexts.Add ("EntityId", "String");

                bindingContexts.Add ("ContactType", "Integer");

                bindingContexts.Add ("ContactTypeDescription", "String");

                bindingContexts.Add ("ContactSequence", "Integer");

                bindingContexts.Add ("EffectiveDate", "DateTime");

                bindingContexts.Add ("TerminationDate", "DateTime");

                bindingContexts.Add ("Number", "String");

                bindingContexts.Add ("NumberExtension", "String");

                bindingContexts.Add ("NumberFormatted", "String");

                bindingContexts.Add ("Email", "String");

                return bindingContexts;

            }

        }

        override public String EvaluateDataBinding (String bindingContext) {

            String dataValue = String.Empty;

            String bindingContextPart = bindingContext.Split ('.')[0];

            switch (bindingContextPart) {

                case "Id": dataValue = Id.ToString (); break;

                case "EntityContactInformationId": dataValue = Id.ToString (); break;

                case "EntityId": dataValue = entityId.ToString (); break;

                case "ContactType": dataValue = ((Int32) contactType).ToString (); break;

                case "ContactTypeDescription": dataValue = ContactTypeDescription; break;

                case "ContactSequence": dataValue = contactSequence.ToString (); break;

                case "EffectiveDate": dataValue = effectiveDate.ToString ("MM/dd/yyyy"); break;

                case "TerminationDate": dataValue = terminationDate.ToString ("MM/dd/yyyy"); break;

                case "Number": dataValue = number; break;

                case "NumberExtension": dataValue = numberExtension; break;

                case "NumberFormatted": dataValue = NumberFormatted; break;

                case "Email": dataValue = email; break;

                default: dataValue = "!Error"; break;

            }

            return dataValue;

        }

        #endregion


        #region Public Functions

        public Boolean Terminate (DateTime forTerminationDate) {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            Boolean usingTransaction = true;


            try {

                if (Id == 0) { throw new ApplicationException ("Permission Denied. Contact Information ID not available."); }


                ModifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application);


                if (application.EnvironmentDatabase.OpenTransactions == 0) {

                    usingTransaction = true;

                    base.application.EnvironmentDatabase.BeginTransaction ();

                }

                System.Data.SqlClient.SqlCommand insertCommand = application.EnvironmentDatabase.CreateSqlCommand ("dal.EntityContactInformation_Terminate");

                insertCommand.CommandType = System.Data.CommandType.StoredProcedure;


                insertCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@entityContactInformationId", System.Data.SqlDbType.BigInt));

                insertCommand.Parameters["@entityContactInformationId"].Value = Id;


                insertCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@terminationDate", System.Data.SqlDbType.DateTime));

                insertCommand.Parameters["@terminationDate"].Value = forTerminationDate;


                insertCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@modifiedAuthorityName", System.Data.SqlDbType.VarChar, 60));

                insertCommand.Parameters["@modifiedAuthorityName"].Value = modifiedAccountInfo.SecurityAuthorityName;

                insertCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@modifiedAccountId", System.Data.SqlDbType.VarChar, 60));

                insertCommand.Parameters["@modifiedAccountId"].Value = modifiedAccountInfo.UserAccountId;

                insertCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@modifiedAccountName", System.Data.SqlDbType.VarChar, 60));

                insertCommand.Parameters["@modifiedAccountName"].Value = modifiedAccountInfo.UserAccountName;


                if (insertCommand.ExecuteNonQuery () != 1) {

                    if (application.EnvironmentDatabase.LastException == null) { throw new ApplicationException ("Permission Denied. Unable to Terminate Contact Information."); }

                    base.application.SetLastException (base.application.EnvironmentDatabase.LastException);

                    throw base.application.EnvironmentDatabase.LastException;

                }

                success = true;

                if (usingTransaction) { base.application.EnvironmentDatabase.CommitTransaction (); }

            }

            catch (Exception applicationException) {

                success = false;

                if (usingTransaction) { base.application.EnvironmentDatabase.RollbackTransaction (); }

                base.application.SetLastException (applicationException);

            }

            return success;

        }

        #endregion 

    }

}
