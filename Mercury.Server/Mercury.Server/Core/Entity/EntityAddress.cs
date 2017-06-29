using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using Mercury.Server.Data;

namespace Mercury.Server.Core.Entity {

    [Serializable]
    [DataContract (Name = "EntityAddress")]
    public class EntityAddress : CoreObject {

        #region Private Properties

        [DataMember (Name = "EntityId")]
        private Int64 entityId;


        [DataMember (Name = "AddressType")]
        private Core.Enumerations.EntityAddressType addressType = Mercury.Server.Core.Enumerations.EntityAddressType.NotSpecified;

        [DataMember (Name = "Line1")]
        private String line1;

        [DataMember (Name = "Line2")]
        private String line2;

        [DataMember (Name = "City")]
        private String city;

        [DataMember (Name = "State")]
        private String state;

        [DataMember (Name = "ZipCode")]
        private String zipCode;

        [DataMember (Name = "ZipPlus4")]
        private String zipPlus4;

        [DataMember (Name = "PostalCode")]
        private String postalCode;

        [DataMember (Name = "County")]
        private String county;


        [DataMember (Name = "Longitude")]
        private Decimal longitude = 0.0m;

        [DataMember (Name = "Latitude")]
        private Decimal latitude = 0.0m;


        [DataMember (Name = "EffectiveDate")]
        private DateTime effectiveDate;

        [DataMember (Name = "TerminationDate")]
        private DateTime terminationDate;


        private Entity entity = null;

        #endregion


        #region Public Properties
        
        public Int64 EntityId { get { return entityId; } set { entityId = value; } }

        public Core.Enumerations.EntityAddressType AddressType { get { return addressType; } set { addressType = value; } }

        public String AddressTypeDescription { get { return CommonFunctions.EnumerationToString (addressType); } }


        public String Line1 { get { return line1; } set { line1 = CommonFunctions.SetValueMaxLength (value, DataTypeConstants.AddressLine); } }

        public String Line2 { get { return line2; } set { line2 = CommonFunctions.SetValueMaxLength (value, DataTypeConstants.AddressLine); } }

        public String City { get { return city; } set { city = CommonFunctions.SetValueMaxLength (value, DataTypeConstants.AddressCity); } }

        public String State { get { return state; } set { state = CommonFunctions.SetValueMaxLength (value, DataTypeConstants.AddressState); } }

        public String ZipCode { get { return zipCode; } set { zipCode = CommonFunctions.SetValueMaxLength (value, DataTypeConstants.AddressZipCode); } }

        public String ZipPlus4 { get { return zipPlus4; } set { zipPlus4 = CommonFunctions.SetValueMaxLength (value, DataTypeConstants.AddressZipPlus4); } }

        public String PostalCode { get { return postalCode; } set { postalCode = CommonFunctions.SetValueMaxLength (value, DataTypeConstants.AddressPostalCode); } }

        public String County { get { return county; } set { county = CommonFunctions.SetValueMaxLength (value, DataTypeConstants.AddressCounty); } }


        public Decimal Longitude { get { return longitude; } set { longitude = value; } }

        public Decimal Latitude { get { return latitude; } set { latitude = value; } }


        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }


        public String CityStateZipCode {

            get {

                String description = String.Empty;

                description = city + ", " + state + " " + zipCode + ((zipPlus4.Trim ().Length != 0) ? "-" + zipPlus4 : String.Empty);

                return description;

            }

        }

        public String AddressSingleLine {

            get {

                String singleLine;

                singleLine = line1 + " " + ((line2.Trim ().Length != 0) ? ", " + line2 : String.Empty) + ", " + CityStateZipCode;

                return singleLine;

            }

        }


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

        public EntityAddress (Application applicationReference) {

            BaseConstructor (applicationReference);

            return; 
        
        }

        public EntityAddress (Application applicationReference, Int64 entityAddressId) {

            BaseConstructor (applicationReference, entityAddressId);

            return;

        }

        public EntityAddress (Application applicationReference, Int64 entityId, Core.Enumerations.EntityAddressType addressType, DateTime forDate) {

            BaseConstructor (applicationReference);

            if (!LoadFromDatabase (entityId, addressType, forDate)) { 

                throw new ApplicationException ("Unable to load Entity Address.");

            }

        }

        #endregion


        #region Validation Functions

        public override Dictionary<String, String> Validate () {

            Dictionary<String, String> validationResponse = new Dictionary<String, String> ();

            // TODO: VALIDATE ADDRESS

            return validationResponse;

        }

        #endregion


        #region Data Functions

        public override Boolean Load (Int64 forId) { return base.LoadFromDalSp (forId); }

        public Boolean LoadFromDatabase (Int64 entityId, Core.Enumerations.EntityAddressType addressType, DateTime forDate) {

            Boolean success = false;

            String selectStatment = "EXEC dal.EntityAddress_SelectByTypeAndDate " + entityId.ToString () + ", " + ((Int32) addressType).ToString () + ", '" + forDate.ToString ("MM/dd/yyyy") + "'";

            System.Data.DataTable tableEntityAddress;

            if (base.application.EnvironmentDatabase == null) { return false; }


            tableEntityAddress = base.application.EnvironmentDatabase.SelectDataTable (selectStatment);

            if (tableEntityAddress.Rows.Count == 1) {

                MapDataFields (tableEntityAddress.Rows[0]);

                success = true;

            }

            else {

                application.SetLastException (application.EnvironmentDatabase.LastException);

                success = false;

            }

            return success;

        }

        
        override public void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            entityId = (Int64) currentRow["EntityId"];


            addressType = (Core.Enumerations.EntityAddressType) ((Int32) currentRow["AddressType"]);

            line1 = (String) currentRow["Line1"];

            line2 = (String) currentRow["Line2"];

            city = (String) currentRow["City"];

            state = (String) currentRow["State"];

            zipCode = (String) currentRow["ZipCode"];

            zipPlus4 = (String) currentRow["ZipPlus4"];

            postalCode = (String) currentRow["PostalCode"];

            county = (String) currentRow["County"];


            longitude = (Decimal) currentRow["Longitude"];

            latitude = (Decimal) currentRow["Latitude"];


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

                List<EntityAddress> overlappingAddresses = application.EntityAddressesGetByEntityTypeOverlap (entityId, addressType, effectiveDate, terminationDate, Id);

                if (overlappingAddresses.Count > 1) {

                    application.TraceWriteLineError (application.TraceSwitchGeneral, "[EntityAddress.Save] " + "Entity Id: " + entityId.ToString () + ", Address Type: " + addressType.ToString () + ", Effective Date: " + effectiveDate.ToString ("MM/dd/yyyy") + ", Termination Date: " + terminationDate.ToString ("MM/dd/yyyy"));
                    
                    throw new ApplicationException ("Unable to save due to multiple overlapping addresses."); 
                
                }

                if (overlappingAddresses.Count == 1) {

                    // TERMINATE OVERLAPPAING SEGMENTS IF POSSIBLE, TERMINATION DATE IS EFFECTIVE DATE -1

                    success = overlappingAddresses[0].Terminate (effectiveDate.AddDays (-1));

                    if (!success) {

                        application.TraceWriteLineError (application.TraceSwitchGeneral, "[EntityAddress.Save] " + "Entity Id: " + entityId.ToString () + ", Address Type: " + addressType.ToString () + ", Effective Date: " + effectiveDate.ToString ("MM/dd/yyyy") + ", Termination Date: " + terminationDate.ToString ("MM/dd/yyyy"));
    
                        throw new ApplicationException ("Permission Denied. Unable to terminate previous overlapping address."); 
                    
                    }

                }


                // ATTEMPT TO SAVE NEW OR UPDATE EXISTING ENTITY ADDRESS

                ModifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application);


                System.Data.SqlClient.SqlCommand sqlCommand = application.EnvironmentDatabase.CreateSqlCommand ("dal.EntityAddress_InsertUpdate");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                sqlCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@entityAddressId", System.Data.SqlDbType.BigInt));

                sqlCommand.Parameters["@entityAddressId"].Value = Id;

                sqlCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@entityId", System.Data.SqlDbType.BigInt));

                sqlCommand.Parameters["@entityId"].Value = entityId;

                sqlCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@addressType", System.Data.SqlDbType.Int));

                sqlCommand.Parameters["@addressType"].Value = addressType;

                
                sqlCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@line1", System.Data.SqlDbType.VarChar, 55));

                sqlCommand.Parameters["@line1"].Value = line1;

                sqlCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@line2", System.Data.SqlDbType.VarChar, 55));

                sqlCommand.Parameters["@line2"].Value = line2;

                sqlCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@city", System.Data.SqlDbType.VarChar, 55));

                sqlCommand.Parameters["@city"].Value = city;

                sqlCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@state", System.Data.SqlDbType.VarChar, 55));

                sqlCommand.Parameters["@state"].Value = state;

                sqlCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@zipCode", System.Data.SqlDbType.VarChar, 55));

                sqlCommand.Parameters["@zipCode"].Value = zipCode;

                sqlCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@zipPlus4", System.Data.SqlDbType.VarChar, 55));

                sqlCommand.Parameters["@zipPlus4"].Value = zipPlus4;

                sqlCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@postalCode", System.Data.SqlDbType.VarChar, 55));

                sqlCommand.Parameters["@postalCode"].Value = postalCode;

                sqlCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@county", System.Data.SqlDbType.VarChar, 55));

                sqlCommand.Parameters["@county"].Value = county;


                sqlCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@longitude", System.Data.SqlDbType.Decimal));

                sqlCommand.Parameters["@longitude"].Value = longitude;

                sqlCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@latitude", System.Data.SqlDbType.Decimal));

                sqlCommand.Parameters["@latitude"].Value = latitude;


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

                    if (application.EnvironmentDatabase.LastException == null) { throw new ApplicationException ("Permission Denied. Unable to Save Address."); }

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

        public override Dictionary<String, String>  DataBindingContexts {

            get {

                Dictionary<String, String> bindingContexts = new Dictionary<String, String> ();

                bindingContexts.Add ("Id", "Id|EntityAddress");

                bindingContexts.Add ("EntityId", "String");


                bindingContexts.Add ("AddressType", "Integer");

                bindingContexts.Add ("AddressTypeDescription", "String");

                bindingContexts.Add ("Line1", "String");

                bindingContexts.Add ("Line2", "String");

                bindingContexts.Add ("City", "String");

                bindingContexts.Add ("State", "String");

                bindingContexts.Add ("ZipCode", "String");

                bindingContexts.Add ("ZipPlus4", "String");

                bindingContexts.Add ("PostalCode", "String");

                bindingContexts.Add ("County", "String");

                bindingContexts.Add ("CityStateZipCode", "String");


                bindingContexts.Add ("EffectiveDate", "DateTime");

                bindingContexts.Add ("TerminationDate", "DateTime");


                return bindingContexts;

            }

        }

        override public String EvaluateDataBinding (String bindingContext) {

            String dataValue = String.Empty;

            String bindingContextPart = bindingContext.Split ('.')[0];

            switch (bindingContextPart) {

                case "EntityId": dataValue = entityId.ToString (); break;


                case "AddressType": dataValue = ((Int32) addressType).ToString (); break;

                case "AddressTypeDescription": dataValue = AddressTypeDescription; break;
                    
                case "Line1": dataValue = line1; break;

                case "Line2": dataValue = line2; break;

                case "City": dataValue = city; break;

                case "State": dataValue = state; break;

                case "ZipCode": dataValue = zipCode; break;

                case "ZipPlus4": dataValue = zipPlus4; break;

                case "PostalCode": dataValue = postalCode; break;

                case "County": dataValue = county; break;

                case "CityStateZipCode": dataValue = CityStateZipCode; break;


                case "EffectiveDate": dataValue = effectiveDate.ToString ("MM/dd/yyyy"); break;

                case "TerminationDate": dataValue = terminationDate.ToString ("MM/dd/yyyy"); break;


                default: dataValue = base.EvaluateDataBinding (bindingContext); break;

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

                if (Id == 0) { throw new ApplicationException ("Permission Denied. Address ID not available."); }


                ModifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application);


                if (application.EnvironmentDatabase.OpenTransactions == 0) {

                    usingTransaction = true;

                    base.application.EnvironmentDatabase.BeginTransaction ();

                }

                System.Data.SqlClient.SqlCommand insertCommand = application.EnvironmentDatabase.CreateSqlCommand ("dal.EntityAddress_Terminate");

                insertCommand.CommandType = System.Data.CommandType.StoredProcedure;


                insertCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@entityAddressId", System.Data.SqlDbType.BigInt));

                insertCommand.Parameters["@entityAddressId"].Value = Id;


                insertCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@terminationDate", System.Data.SqlDbType.DateTime));

                insertCommand.Parameters["@terminationDate"].Value = forTerminationDate;


                insertCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@modifiedAuthorityName", System.Data.SqlDbType.VarChar, 60));

                insertCommand.Parameters["@modifiedAuthorityName"].Value = modifiedAccountInfo.SecurityAuthorityName;

                insertCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@modifiedAccountId", System.Data.SqlDbType.VarChar, 60));

                insertCommand.Parameters["@modifiedAccountId"].Value = modifiedAccountInfo.UserAccountId;

                insertCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@modifiedAccountName", System.Data.SqlDbType.VarChar, 60));

                insertCommand.Parameters["@modifiedAccountName"].Value = modifiedAccountInfo.UserAccountName;


                if (insertCommand.ExecuteNonQuery () != 1) {

                    if (application.EnvironmentDatabase.LastException == null) { throw new ApplicationException ("Permission Denied. Unable to Terminate Address."); }

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
