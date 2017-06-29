using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core {

    [Serializable]
    [DataContract (Name = "CoreObject")]

    [KnownType (typeof (Server.Core.CoreExtensibleObject))]

    [KnownType (typeof (Server.Security.SecurityAuthority))]
    [KnownType (typeof (Server.Environment.EnvironmentType))]
    [KnownType (typeof (Server.Environment.Environment))]

    [KnownType (typeof (Server.Core.Action.Action))]
    [KnownType (typeof (Server.Core.Action.ActionThreshold))]

    [KnownType (typeof (Server.Core.Activity.Activity))]
    [KnownType (typeof (Server.Core.Activity.ActivityThreshold))]

    [KnownType (typeof (Server.Core.AuthorizedServices.AuthorizedServiceDefinition))]
    [KnownType (typeof (Server.Core.AuthorizedServices.MemberAuthorizedService))]
    
    [KnownType (typeof (Server.Core.Entity.Entity))]
    [KnownType (typeof (Server.Core.Entity.EntityAddress))]
    [KnownType (typeof (Server.Core.Entity.EntityContact))]
    [KnownType (typeof (Server.Core.Entity.EntityContactInformation))]
    [KnownType (typeof (Server.Core.Entity.EntityCorrespondence))]
    [KnownType (typeof (Server.Core.Entity.EntityNote))]
    [KnownType (typeof (Server.Core.Entity.EntityNoteContent))]

    [KnownType (typeof (Server.Core.Forms.Control))]

    [KnownType (typeof (Server.Core.Individual.CareMeasureScale))]
    [KnownType (typeof (Server.Core.Individual.CareMeasureDomain))]
    [KnownType (typeof (Server.Core.Individual.CareMeasureClass))]
    [KnownType (typeof (Server.Core.Individual.CareMeasure))]
    [KnownType (typeof (Server.Core.Individual.CareMeasureComponent))]

    [KnownType (typeof (Server.Core.Individual.CarePlan))]
    [KnownType (typeof (Server.Core.Individual.CarePlanGoal))]
    [KnownType (typeof (Server.Core.Individual.CarePlanActivity))]

    [KnownType (typeof (Server.Core.Individual.Case.MemberCaseAudit))]

    [KnownType (typeof (Server.Core.Insurer.InsuranceType))]

    [KnownType (typeof (Server.Core.MedicalServices.MemberService))]
    [KnownType (typeof (Server.Core.MedicalServices.Definitions.ServiceSetDefinition))]
    [KnownType (typeof (Server.Core.MedicalServices.Definitions.ServiceSingletonDefinition))]

    [KnownType (typeof (Server.Core.Member.Member))]
    [KnownType (typeof (Server.Core.Member.MemberEnrollment))]
    [KnownType (typeof (Server.Core.Member.MemberEnrollmentCoverage))]
    [KnownType (typeof (Server.Core.Member.MemberEnrollmentPcp))]
    [KnownType (typeof (Server.Core.Member.MemberEnrollmentTplCob))]
    [KnownType (typeof (Server.Core.Member.MemberRelationship))]

    [KnownType (typeof (Server.Core.Metrics.MemberMetric))]

    [KnownType (typeof (Server.Core.Population.PopulationMembership))]
    [KnownType (typeof (Server.Core.Population.PopulationEvents.PopulationActivityEvent))]
    [KnownType (typeof (Server.Core.Population.PopulationEvents.PopulationMembershipServiceEvent))]
    [KnownType (typeof (Server.Core.Population.PopulationEvents.PopulationMembershipTriggerEvent))]
    [KnownType (typeof (Server.Core.Population.PopulationEvents.PopulationServiceEvent))]
    [KnownType (typeof (Server.Core.Population.PopulationEvents.PopulationServiceEventThreshold))]
    [KnownType (typeof (Server.Core.Population.PopulationEvents.PopulationTriggerEvent))]

    [KnownType (typeof (Server.Core.Provider.Provider))]
    [KnownType (typeof (Server.Core.Provider.ProviderAffiliation))]
    [KnownType (typeof (Server.Core.Provider.ProviderContract))]
    [KnownType (typeof (Server.Core.Provider.ProviderEnrollment))]
    [KnownType (typeof (Server.Core.Provider.ProviderServiceLocation))]

    [KnownType (typeof (Server.Core.Reference.CorrespondenceContent))]

    [KnownType (typeof (Server.Core.Sponsor.Sponsor))]

    [KnownType (typeof (Server.Core.Work.RoutingRuleDefinition))]
    [KnownType (typeof (Server.Core.Work.WorkflowPermission))]
    [KnownType (typeof (Server.Core.Work.WorkQueueItemAssignmentHistory))]
    [KnownType (typeof (Server.Core.Work.WorkQueueItemSender))]
    [KnownType (typeof (Server.Core.Work.WorkQueueTeam))]
    [KnownType (typeof (Server.Core.Work.WorkTeamMembership))]
    public class CoreObject {

        #region Private Properties

        protected Application application = null;


        [DataMember (Name = "Id")]
        protected Int64 id = 0;

        [DataMember (Name = "Name")]
        protected String name = String.Empty;

        [DataMember (Name = "Description")]
        protected String description = String.Empty;


        [DataMember (Name = "CreateAccountInfo")]
        protected Mercury.Server.Data.AuthorityAccountStamp createAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp ();

        [DataMember (Name = "ModifiedAccountInfo")]
        protected Mercury.Server.Data.AuthorityAccountStamp modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp ();

        #endregion


        #region Public Properties

        public virtual Application Application { get { return application; } set { application = value; } }


        public virtual Int64 Id { get { return id; } }

        public virtual String Name { get { return name; } set { name = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public virtual String Description { get { return description; } set { description = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Description); } }


        protected String NameSql { get { return Name.Replace ("'", "''").Trim (); } }

        protected String DescriptionSql { get { return Description.Replace ("'", "''").Trim (); } }


        public Mercury.Server.Data.AuthorityAccountStamp CreateAccountInfo {

            get {

                if (createAccountInfo == null) { createAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (); }

                return createAccountInfo;

            }

            set { createAccountInfo = value; }

        } // Property: CreateAccountInfo

        public Mercury.Server.Data.AuthorityAccountStamp ModifiedAccountInfo {

            get {

                if (modifiedAccountInfo == null) { modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (); }

                return modifiedAccountInfo;

            }
            set { modifiedAccountInfo = value; }

        } // Property: ModifiedAccountInfo


        public String ObjectType {

            get {

                String objectType = String.Empty;

                String[] objectNamespace = GetType ().ToString ().Split ('.');

                if (objectNamespace.Length > 0) {

                    objectType = objectNamespace[objectNamespace.Length - 1];

                }

                return objectType;

            }

        }

        #endregion


        #region Constructors

        protected CoreObject () { return; }

        public CoreObject (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public void BaseConstructor (Application applicationReference) {

            application = applicationReference;

            return;

        }

        public void BaseConstructor (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference);

            if (!Load (forId)) { throw new ApplicationException ("Unable to load " + GetType ().ToString () + " from database for Id: " + forId.ToString ()); }

            return;

        }

        public void BaseConstructor (Application applicationReference, Int64 forId, Int64 forEnvironmentId) {

            BaseConstructor (applicationReference);

            if (!LoadFromEnvironment (forId, forEnvironmentId)) { throw new ApplicationException ("Unable to load " + GetType ().ToString () + " from database for Id: " + forId.ToString ()); }

            return;

        }

        #endregion


        #region XML Serialization

        public System.Xml.XmlDocument XmlEmptyDocument () {
            
            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument ();

            System.Xml.XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration ("1.0", "utf-8", String.Empty);

            xmlDocument.InsertBefore (xmlDeclaration, xmlDocument.DocumentElement);


            return xmlDocument;

        }

        public virtual System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument coreObjectDocument = new System.Xml.XmlDocument ();

            System.Xml.XmlDeclaration xmlDeclaration = coreObjectDocument.CreateXmlDeclaration ("1.0", "utf-8", String.Empty);

            System.Xml.XmlElement coreObjectNode = coreObjectDocument.CreateElement (ObjectType);

            System.Xml.XmlElement propertiesNode = coreObjectDocument.CreateElement ("Properties");



            #region Initialize Document Structure   

            coreObjectDocument.InsertBefore (xmlDeclaration, coreObjectDocument.DocumentElement);

            coreObjectDocument.AppendChild (coreObjectNode);

            coreObjectNode.SetAttribute ("Name", Name);

            coreObjectNode.SetAttribute ("Id", Id.ToString ());

            coreObjectNode.AppendChild (propertiesNode);


            // ROOT (OBJECT TYPE) [NAME/ID]

            // +-- PROPERTIES 

            // +-- +-- PROPERTY [NAME/VALUE]

            #endregion 
                        

            #region Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "Id", Id.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "Name", Name);

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "Description", Description);

            #endregion 

            
            return coreObjectDocument;

        }

        public virtual List<ImportExport.Result> XmlImport (System.Xml.XmlNode objectNode) {

            List<ImportExport.Result> response = new List<ImportExport.Result> ();

            System.Xml.XmlNode propertiesNode;

            String exceptionMessage = String.Empty;


            if (ObjectType != objectNode.Name) {

                exceptionMessage = "Mismatch Object Types during import. Expected '" + ObjectType + "', but found '" + objectNode.Name + "'.";
                
                response.Add (new ImportExport.Result (ObjectType, objectNode.Name, new ApplicationException (exceptionMessage))); 
                
                return response; 
            
            }


            try {

                propertiesNode = objectNode.SelectSingleNode ("Properties");

                foreach (System.Xml.XmlNode currentPropertyNode in propertiesNode) {

                    switch (currentPropertyNode.Attributes["Name"].InnerText) {

                        // ID DOES NOT GET SET FROM SOURCE AS IMPORT WILL BE NEW OBJECT

                        case "Name": Name = currentPropertyNode.InnerText; break;

                        case "Description": Description = currentPropertyNode.InnerText; break;

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

        public virtual Dictionary<String, String> Validate (CoreObject forCoreObject) {

            Dictionary<String, String> validationResponse = new Dictionary<string, string> ();


            // VALIDATE NAME IS NOT NULL OR EMPTY

            if (String.IsNullOrWhiteSpace (forCoreObject.Name)) { validationResponse.Add ("Name", "Empty or NULL."); }

            // else if (forCoreObject.Name.Contains ("'")) { validationResponse.Add ("Name", "Contains special/restricted character."); }

                
            // VALIDATE DESCRIPTION IS NOT NULL OR EMPTY

            if (String.IsNullOrWhiteSpace (forCoreObject.Description)) { validationResponse.Add ("Description", "Empty or NULL."); }


            return validationResponse;
        
        }

        public virtual Dictionary<String, String> Validate () { return Validate (this); }

        #endregion 


        #region Database Functions

        public Boolean Load (Application applicationReference, Int64 forId) {

            application = applicationReference;

            return Load (forId);

        }

        public virtual Boolean Load (Int64 forId) {

            if (forId == 0) { return false; }


            Boolean success = false;

            String selectStatement = "SELECT * FROM dbo." + ObjectType + " WHERE " + ObjectType + "Id = " + forId.ToString ();

            System.Data.DataTable objectTable = application.EnvironmentDatabase.SelectDataTable (selectStatement);

            success = (objectTable.Rows.Count == 1);


            if (success) { 
                
                MapDataFields (objectTable.Rows[0]);

                success = LoadChildObjects ();

            }


            return success;

        }

        public virtual Boolean LoadFromDal (Int64 forId) {

            if (forId == 0) { return false; }


            Boolean success = false;

            String selectStatement = "SELECT * FROM dal." + ObjectType + " WHERE " + ObjectType + "Id = " + forId.ToString ();

            System.Data.DataTable objectTable = application.EnvironmentDatabase.SelectDataTable (selectStatement);

            success = (objectTable.Rows.Count == 1);


            if (success) { 
                
                MapDataFields (objectTable.Rows[0]);

                success = LoadChildObjects ();
            
            }


            return success;

        }

        public virtual Boolean LoadFromDalSp (Int64 forId) {

            if (forId == 0) { return false; }


            Boolean success = false;

            String selectStatement = "EXEC dal." + ObjectType + "_Select " + forId.ToString ();

            System.Data.DataTable objectTable = application.EnvironmentDatabase.SelectDataTable (selectStatement);

            success = (objectTable.Rows.Count == 1);


            if (success) {

                MapDataFields (objectTable.Rows[0]);

                success = LoadChildObjects ();

            }


            return success;

        }

        public virtual Boolean LoadFromEnvironment (Int64 forId, Int64 environmentId) {

            if (forId == 0) { return false; }


            Boolean success = false;

            String selectStatement = "SELECT * FROM dbo." + ObjectType + " WHERE " + ObjectType + "Id = " + forId.ToString ();

            System.Data.DataTable objectTable = application.EnvironmentDatabaseById (environmentId).SelectDataTable (selectStatement);

            success = (objectTable.Rows.Count == 1);


            if (success) { 
                
                MapDataFields (objectTable.Rows[0]);

                success = LoadChildObjects ();
            
            }


            return success;

        }

        public virtual Boolean LoadFromTable (String tableName, String tableKeyName, Int64 forId) {

            if (forId == 0) { return false; }


            Boolean success = false;

            String selectStatement = "SELECT * FROM " + tableName + " WHERE " + tableKeyName + " = " + forId.ToString ();

            System.Data.DataTable objectTable = application.EnvironmentDatabase.SelectDataTable (selectStatement);

            success = (objectTable.Rows.Count == 1);


            if (success) { 
                
                MapDataFields (objectTable.Rows[0]);

                LoadChildObjects ();

            }


            return success;

        }

        public virtual Boolean LoadChildObjects () { return true; }

        public virtual Boolean Save () { return false; }

        public Boolean Save (Application applicationReference) {

            Application = applicationReference;

            return Save ();

        }

        public virtual void SetIdentity () {

            Boolean exceptionOccurred = false;

            if (id == 0) {

                Object identity = application.EnvironmentDatabase.ExecuteScalar ("SELECT @@IDENTITY");

                if (!(identity is DBNull)) { 

                    if (!Int64.TryParse (Convert.ToString (identity), out id)) {

                        exceptionOccurred = true;

                    }

                }

                else { exceptionOccurred = true; }

                if (exceptionOccurred) { throw new ApplicationException ("Unable to retreive unique id (IDENTITY) from database for Object: " + GetType ().ToString ());}

            }

            return;

        }


        virtual public void MapDataFields (System.Data.DataRow currentRow, String forColumnPrefix) {

            if (currentRow.Table.Columns.Contains (forColumnPrefix + "Id")) {

                id = (Int64)currentRow[forColumnPrefix + "Id"];

            }

            if (currentRow.Table.Columns.Contains (forColumnPrefix + "Name")) {

                Name = (String)currentRow[forColumnPrefix + "Name"];

            }


            if (currentRow.Table.Columns.Contains (forColumnPrefix + "Description")) {

                Description = (String)currentRow[forColumnPrefix + "Description"];

            }

            if (currentRow.Table.Columns.Contains ("CreateAuthorityName")) {

                createAccountInfo = new Data.AuthorityAccountStamp (currentRow, "Create");

            }

            if (currentRow.Table.Columns.Contains ("ModifiedAuthorityName")) {

                modifiedAccountInfo = new Data.AuthorityAccountStamp (currentRow, "Modified");

            }

            return; 
        
        }

        virtual public void MapDataFields (System.Data.DataRow currentRow) { MapDataFields (currentRow, ObjectType); }


        public Int64 IdFromSql (System.Data.DataRow currentRow, String fieldName) {

            Int64 forId = 0;


            if (currentRow.Table.Columns.Contains (fieldName)) {

                if (!(currentRow[fieldName] is DBNull)) {

                    forId = Convert.ToInt64 (currentRow[fieldName]);

                }

            }


            return forId;

        }

        public String IdSqlAllowNull (Int64 forId) { return ((forId == 0) ? "NULL" : forId.ToString ()); }

        public Int64? IdSqlAllowNullInt64 (Int64 forId) {

            Int64? value = null;

            if (forId != 0) { value = forId; }

            return value;

        }


        public String StringFromSql (System.Data.DataRow currentRow, String fieldName) {

            String forString = String.Empty;


            if (currentRow.Table.Columns.Contains (fieldName)) {

                if (!(currentRow[fieldName] is DBNull)) {

                    forString = Convert.ToString (currentRow[fieldName]);

                }

            }


            return forString;

        }

        public String StringSqlAllowNull (String forString) { return ((String.IsNullOrWhiteSpace (forString)) ? "NULL" : "'" + forString.Replace ("'", "''") + "'"); }

        public DateTime? DateTimeFromSql (System.Data.DataRow currentRow, String fieldName) {

            DateTime? forDateTime = null;


            if (currentRow.Table.Columns.Contains (fieldName)) {

                if (!(currentRow[fieldName] is DBNull)) {

                    forDateTime = Convert.ToDateTime (currentRow[fieldName]);

                }

            }


            return forDateTime;

        }

        public String DateTimeSqlAllowNull (DateTime? forDateTime) { return ((forDateTime.HasValue) ? "'" + forDateTime.Value.ToString () + "'" : "NULL"); }

        public Decimal DecimalFromSql (System.Data.DataRow currentRow, String fieldName) {

            Decimal forDecimal = 0;


            if (currentRow.Table.Columns.Contains (fieldName)) {

                if (!(currentRow[fieldName] is DBNull)) {

                    forDecimal = Convert.ToDecimal (currentRow[fieldName]);

                }

            }


            return forDecimal;

        }


        public Guid GuidFromSql (System.Data.DataRow currentRow, String fieldName) {

            Guid forGuid = Guid.Empty;


            if (currentRow.Table.Columns.Contains (fieldName)) {

                if (!(currentRow[fieldName] is DBNull)) {

                    try { 

                        forGuid = (Guid) currentRow[fieldName];

                    }

                    catch { /* DO NOTHING */ }

                }

            }


            return forGuid;

        }

        public String GuidSqlAllowNull (Guid forGuid) { return (((forGuid == Guid.Empty) || (forGuid == null)) ? "NULL" : "'" + forGuid.ToString () + "'"); }

        #endregion


        #region Virtual - Data Bindings

        public virtual Dictionary<String, String> DataBindingContexts {

            get {

                Dictionary<String, String> dataBindingContexts = new Dictionary<String, String> ();


                dataBindingContexts.Add ("Id", "Id|" + ObjectType);

                dataBindingContexts.Add ("Name", "String");

                dataBindingContexts.Add ("Description", "String");


                return dataBindingContexts;

            }

        }

        virtual public String EvaluateDataBinding (String bindingContext) {

            String dataValue = String.Empty;

            String bindingContextPart = bindingContext.Split ('.')[0];


            switch (bindingContextPart) {

                case "Id": dataValue = Id.ToString (); break;

                case "Name": dataValue = Name; break;

                case "Description": dataValue = Description; break;

                default: dataValue = "!ERROR"; break;

            }

            return dataValue;

        }

        #endregion

    }

}
