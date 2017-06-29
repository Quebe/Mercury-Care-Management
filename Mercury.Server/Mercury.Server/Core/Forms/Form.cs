using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Forms {

    [Serializable]
    [DataContract (Name = "Form")]
    public class Form : Control {

        #region Private Properties

        [DataMember (Name = "FormId")]
        private Int64 formId = 0;

        [DataMember (Name = "EntityFormId")]
        private Int64 entityFormId = 0;

        [DataMember (Name = "FormType")]
        private Enumerations.FormType formType;


        [DataMember (Name = "EntityType")]
        private Mercury.Server.Core.Enumerations.EntityType entityType = Mercury.Server.Core.Enumerations.EntityType.Member;

        [DataMember (Name = "EntityObjectId")]
        private Int64 entityObjectId = 0;

        [DataMember (Name = "Orientation")]
        private Enumerations.FormControlOrientation orientation = Mercury.Server.Core.Forms.Enumerations.FormControlOrientation.Portrait;

        [DataMember (Name = "PaperSize")]
        private Enumerations.FormControlPaperSize paperSize = Mercury.Server.Core.Forms.Enumerations.FormControlPaperSize.Letter;

        [DataMember (Name = "AllowPrecompileEvents")]
        private Boolean allowPrecompileEvents = false;

        [DataMember (Name = "EventResults")]
        private List<EventResult> eventResults = new List<EventResult> ();

        #endregion


        #region Public Properties

        public override Int64 Id { get { return ((formType == Mercury.Server.Core.Forms.Enumerations.FormType.Template) ? formId : entityFormId); } }


        public Int64 FormId { get { return formId; } set { formId = value; } }

        public Int64 EntityFormId { get { return entityFormId; } set { entityFormId = value; } }

        public String FormName { get { return Name; } set { Name = value; } }


        public Enumerations.FormType FormType { get { return formType; } set { formType = value; } }

        public Mercury.Server.Core.Enumerations.EntityType EntityType { get { return entityType; } set { entityType = value; DataSourceChanged (); } }

        public Int64 EntityObjectId {

            get { return entityObjectId; }

            set {

                if (entityObjectId != value) {

                    if (value != 0) { formType = Mercury.Server.Core.Forms.Enumerations.FormType.Instance; }

                    entityObjectId = value;

                    DataSourceChanged ();

                }

            }

        }

        public Enumerations.FormControlOrientation Orientation { get { return orientation; } set { orientation = value; } }

        public Enumerations.FormControlPaperSize PaperSize { get { return paperSize; } set { paperSize = value; } }

        public Boolean AllowPrecompileEvents { get { return allowPrecompileEvents; } set { allowPrecompileEvents = value; } }


        public List<EventResult> EventResults {

            get {

                if (eventResults == null) { eventResults = new List<EventResult> (); }

                return eventResults;

            }

            set { eventResults = value; }

        }

        public Exception LastException { get { return (application != null) ? application.LastException : null; } }


        public override System.Xml.XmlDocument ExtendedPropertiesXml {

            get {

                System.Xml.XmlDocument extendedProperties = base.ExtendedPropertiesXml;

                ExtendedProperties_AddProperty (extendedProperties, "FormId", formId.ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "entityFormId", entityFormId.ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "FormName", Name);

                ExtendedProperties_AddProperty (extendedProperties, "FormType", formType.ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "FormTypeInt32", ((Int32)formType).ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "entityType", entityType.ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "EntityTypeInt32", ((Int32)entityType).ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "EntityObjectId", entityObjectId.ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "Orientation", ((Int32)orientation).ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "PaperSize", ((Int32)paperSize).ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "AllowPrecompileEvents", allowPrecompileEvents.ToString ());

                return extendedProperties;

            }

        }

        public override void ExtendedPropertiesDeserialize (System.Xml.XmlNode extendedPropertiesXml) {

            base.ExtendedPropertiesDeserialize (extendedPropertiesXml);

            foreach (System.Xml.XmlNode currentPropertyNode in extendedPropertiesXml.SelectNodes ("./Property")) {

                switch (currentPropertyNode.Attributes["Name"].InnerText) {

                    case "FormName": FormName = currentPropertyNode.InnerText; break;

                    case "FormTypeInt32": formType = (Mercury.Server.Core.Forms.Enumerations.FormType)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "EntityTypeInt32": entityType = (Mercury.Server.Core.Enumerations.EntityType)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "EntityObjectId": entityObjectId = Convert.ToInt64 (currentPropertyNode.InnerText); break;

                    case "Orientation": orientation = (Mercury.Server.Core.Forms.Enumerations.FormControlOrientation)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "PaperSize": paperSize = (Mercury.Server.Core.Forms.Enumerations.FormControlPaperSize)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "AllowPrecompileEvents": allowPrecompileEvents = Convert.ToBoolean (currentPropertyNode.InnerText); break;

                }

            }

            return;

        }


        public Data.AuthorityAccountStamp CurrentUser {

            get {

                Data.AuthorityAccountStamp currentUser = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);

                return currentUser;

            }

        }

        #endregion


        #region Constructors

        protected void InitializeControl () {

            formId = 0;

            entityFormId = 0;

            controlType = Mercury.Server.Core.Forms.Enumerations.FormControlType.Form;

            capabilities.IsDataSource = true;

            return;

        }

        public Form (Application applicationReference) {

            Application = applicationReference;

            InitializeControl ();

            return;

        }

        public Form (Application applicationReference, String formName) {

            Application = applicationReference;

            InitializeControl ();

            formType = Mercury.Server.Core.Forms.Enumerations.FormType.Template;

            if (!LoadFromDatabase (formName)) {

                throw new Exception ("Unable to load Form from the database for " + formName + ".");

            }

            return;

        }

        public Form (Application applicationReference, Int64 formId) {

            this.application = applicationReference;

            InitializeControl ();

            formType = Mercury.Server.Core.Forms.Enumerations.FormType.Template;

            if (!LoadFromDatabaseQuickLoad (formId)) {

                throw new Exception ("Unable to load Form from the database for " + formId + ".");

            }

            return;

        }

        public Form (Application applicationReference, Int64 forId, Boolean forInstance) {

            this.application = applicationReference;

            InitializeControl ();


            if (forInstance) {

                entityFormId = forId;

                formType = Mercury.Server.Core.Forms.Enumerations.FormType.Instance;


                // NEW FAST METHOD

                if (!LoadFromDatabaseQuickLoadBaseForInstance (forId)) {

                    throw new Exception ("Unable to load Form from the database for " + formId.ToString () + ".");

                }

                // BELOW IS THE OLDER, SLOW METHOD

                //if (!LoadFromDatabaseWithCriteria ("WHERE EntityFormId = '" + forId.ToString () + "'")) {

                //    throw new Exception ("Unable to load Form from the database for " + formId.ToString () + ".");

                //}

            }

            else {

                formType = Mercury.Server.Core.Forms.Enumerations.FormType.Template;

                if (!LoadFromDatabaseWithCriteria ("WHERE FormId = " + formId.ToString ())) {

                    throw new Exception ("Unable to load Form from the database for " + formId.ToString () + ".");

                }

            }


            return;

        }

        public void ResetForm (Application applicationReference) {

            Application = applicationReference;

            ResetParentOnChildControls ();

            return;

        }

        #endregion


        #region Xml Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];



            #region Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "FormId", formId.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "EntityFormId", entityFormId.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "FormType", ((Int32)formType).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "FormTypeName", formType.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "EntityType", ((Int32)entityType).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "EntityTypeName", entityType.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "EntityObjectId", entityObjectId.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "Orientation", ((Int32)orientation).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "OrientationName", orientation.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "PaperSize", ((Int32)paperSize).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "PaperSizeName", paperSize.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "AllowPrecompileEvents", allowPrecompileEvents.ToString ());

            #endregion


            return document;

        }

        public override List<ImportExport.Result> XmlImport (System.Xml.XmlNode objectNode) {

            List<ImportExport.Result> response = new List<ImportExport.Result> ();

            System.Xml.XmlNode propertiesNode;

            Dictionary<Guid, Guid> controlDictionary = new Dictionary<Guid, Guid> ();


            try {

                // DETERMINE IF THIS IS OVERWRITE OR NEW IMPORT BASED ON DUPLICATE, EXISTING NAME

                String formName = objectNode.Attributes["Name"].InnerText;

                formId = application.CoreObjectGetIdByName ("Form", formName);

                if (formId != 0) { controlDictionary = null; } // OVERWRITE, NOT NEW IMPORT



                propertiesNode = objectNode.SelectSingleNode ("Properties");

                foreach (System.Xml.XmlNode currentPropertyNode in propertiesNode) {

                    switch (currentPropertyNode.Attributes["Name"].InnerText) {

                        // FORM TYPE DEFAULTS TO TEMPLATE

                        case "EntityType": EntityType = (Core.Enumerations.EntityType)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                        case "Orientation": Orientation = (Enumerations.FormControlOrientation)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                        case "PaperSize": PaperSize = (Enumerations.FormControlPaperSize)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                        case "AllowPrecompileEvents": allowPrecompileEvents = Convert.ToBoolean (currentPropertyNode.InnerText); break;

                    }

                }

            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return base.XmlImport (objectNode, ref controlDictionary);

        }

        #endregion 


        #region Database Functions

        protected Boolean LoadFromDatabaseWithCriteria (String criteria) {

            DateTime startTime = DateTime.Now;

            Boolean success = false;

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable tableForm;

            selectStatement.Append ("SELECT * FROM " + ((entityFormId != 0) ? "Entity" : String.Empty) + "Form " + criteria);

            tableForm = application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            if (tableForm.Rows.Count == 1) {

                MapFormDataFields (tableForm.Rows[0]);

                success = base.LoadFromDatabase (this.entityFormId, this.ControlId, null);

            }

#if DEBUG

            System.Diagnostics.Debug.WriteLine ("Form Load from Database Time (SLOW): " + DateTime.Now.Subtract (startTime).Milliseconds.ToString ());

#endif

            return success;

        }

        public Boolean LoadFromDatabase (String formName) {

            //LoadFromDatabaseWithCriteria ("WHERE FormName = '" + formName + "'");

            //Controls.Clear ();

            return LoadFromDatabaseQuickLoad (formName);

        }

        private Boolean LoadFromDatabaseQuickLoadBase (String selectStatement) {

            DateTime startTime = DateTime.Now;

            Boolean success = false;

            System.Data.DataTable tableForm;


            tableForm = application.EnvironmentDatabase.SelectDataTable (selectStatement);

            if (tableForm.Rows.Count == 1) {

                MapFormDataFields (tableForm.Rows[0]); // MAP FORM CONTROL

                DateTime startTimeDbLoad = DateTime.Now;


                selectStatement = "SELECT * FROM FormControl WHERE FormId = " + formId.ToString () + " ORDER BY ParentId, ControlIndex"; // DEFAULT TABLE SORTED FOR CHILD CONTROLS

                System.Data.DataView formControls = application.EnvironmentDatabase.SelectDataView (selectStatement, 0);

                formControls.Sort = "ControlId";


#if DEBUG

                System.Diagnostics.Debug.WriteLine ("Form Load from Database Time (FCTL): " + DateTime.Now.Subtract (startTimeDbLoad).TotalMilliseconds.ToString ());

#endif


                startTimeDbLoad = DateTime.Now;

                success = LoadFromDatabaseQuickLoad (this, formControls);


#if DEBUG

                System.Diagnostics.Debug.WriteLine ("Form Load from Database Time (MAPP): " + DateTime.Now.Subtract (startTimeDbLoad).TotalMilliseconds.ToString ());

#endif

            }


#if DEBUG

            System.Diagnostics.Debug.WriteLine ("Form Load from Database Time (FAST): " + DateTime.Now.Subtract (startTime).TotalMilliseconds.ToString ());

#endif

            return success;

        }

        public Boolean LoadFromDatabaseQuickLoad (Int64 formId) {

            return LoadFromDatabaseQuickLoadBase ("SELECT * FROM Form WHERE FormId = " + formId.ToString ());

        }

        public Boolean LoadFromDatabaseQuickLoad (String formName) {

            return LoadFromDatabaseQuickLoadBase ("SELECT * FROM Form WHERE FormName = '" + formName + "'");
            
        }

        private Boolean LoadFromDatabaseQuickLoadBaseForInstance (Int64 entityFormId) {

            DateTime startTime = DateTime.Now;

            Boolean success = false;

            System.Data.DataTable tableForm;

            String selectStatement = String.Empty;


            tableForm = application.EnvironmentDatabase.SelectDataTable ("SELECT * FROM EntityForm WHERE EntityFormId = " + entityFormId.ToString ());

            if (tableForm.Rows.Count == 1) {

                MapFormDataFields (tableForm.Rows[0]); // MAP FORM CONTROL

                DateTime startTimeDbLoad = DateTime.Now;


                selectStatement = "SELECT * FROM EntityFormControl WHERE EntityFormId = " + entityFormId.ToString () + " ORDER BY ParentId, ControlIndex"; // DEFAULT TABLE SORTED FOR CHILD CONTROLS

                System.Data.DataView formControls = application.EnvironmentDatabase.SelectDataView (selectStatement, 0);

                formControls.Sort = "ControlId";

#if DEBUG

                System.Diagnostics.Debug.WriteLine ("Form Load from Database Time (FCTL): " + DateTime.Now.Subtract (startTimeDbLoad).TotalMilliseconds.ToString ());

#endif

                startTimeDbLoad = DateTime.Now;

                success = LoadFromDatabaseQuickLoad (this, formControls);


#if DEBUG

                System.Diagnostics.Debug.WriteLine ("Form Load from Database Time (MAPP): " + DateTime.Now.Subtract (startTimeDbLoad).TotalMilliseconds.ToString ());

#endif

            }

#if DEBUG

            System.Diagnostics.Debug.WriteLine ("Form Load from Database Time (FAST): " + DateTime.Now.Subtract (startTime).TotalMilliseconds.ToString ());

#endif

            return success;

        }

        override public Boolean Save () {

#if DEBUG

            DateTime startTime = DateTime.Now;

#endif

            Boolean success = false;

            String schemaName = String.Empty;

            String formControlCriteria = String.Empty;

            String formIdentifier = String.Empty;

            StringBuilder sqlStatement;


            modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);


            this.Parent = this;

            if (entityObjectId != 0) { formType = Mercury.Server.Core.Forms.Enumerations.FormType.Instance; }

            switch (formType) {

                case Mercury.Server.Core.Forms.Enumerations.FormType.Template:

                    formControlCriteria = "FormId = " + formId.ToString ();

                    formIdentifier = formId.ToString ();

                    break;

                case Mercury.Server.Core.Forms.Enumerations.FormType.Instance:

                default:

                    formControlCriteria = "EntityFormId = " + entityFormId.ToString ();

                    formIdentifier = entityFormId.ToString ();

                    break;

            }

            try {

                if (formType == Mercury.Server.Core.Forms.Enumerations.FormType.Template) {

                    Int32 existingForm = (Int32)application.EnvironmentDatabase.LookupValue ("Form", "COUNT (1) AS CountOf", "FormName = '" + Name + "' AND FormControlId != '" + controlId + "'");

                    if (existingForm != 0) {

                        throw new ApplicationException ("Form already exists with the name: " + Name + ".");

                    }

                }



                application.EnvironmentDatabase.BeginTransaction ();

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC Form_InsertUpdate ");

                sqlStatement.Append (formId.ToString () + ", ");

                if (formType == Mercury.Server.Core.Forms.Enumerations.FormType.Template) { sqlStatement.Append ("NULL, "); }

                else { sqlStatement.Append (application.EntityIdGet (entityType, entityObjectId).ToString () + ", "); }

                if (formType == Mercury.Server.Core.Forms.Enumerations.FormType.Template) { sqlStatement.Append ("NULL, "); }

                else { sqlStatement.Append (entityFormId.ToString () + ", "); }

                sqlStatement.Append ("'" + base.NameSql + "', '" + base.ControlId.ToString () + "', '" + base.DescriptionSql + "',");

                sqlStatement.Append (((Int32)entityType).ToString () + ", " + entityObjectId.ToString () + ", ");

                sqlStatement.Append ("'" + modifiedAccountInfo.SecurityAuthorityNameSql + "', '" + modifiedAccountInfo.UserAccountIdSql + "', '" + modifiedAccountInfo.UserAccountNameSql + "'");

                success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

                if (!success) { throw application.EnvironmentDatabase.LastException; }

                if (formId == 0) { // RESET DOCUMENT ID CRITERIA

                    Object identity = application.EnvironmentDatabase.ExecuteScalar ("SELECT @@IDENTITY").ToString ();

                    if (identity != null) {

                        formId = Int64.Parse (identity.ToString ());

                    }

                }

                else if ((entityFormId == 0) && (formType == Mercury.Server.Core.Forms.Enumerations.FormType.Instance)) {

                    Object identity = application.EnvironmentDatabase.ExecuteScalar ("SELECT @@IDENTITY").ToString ();

                    if (identity != null) {

                        entityFormId = Int64.Parse (identity.ToString ());

                    }

                    createAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (modifiedAccountInfo.SecurityAuthorityName, modifiedAccountInfo.UserAccountId, modifiedAccountInfo.UserAccountName, modifiedAccountInfo.ActionDate);

                }

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("DELETE FROM " + ((formType == Mercury.Server.Core.Forms.Enumerations.FormType.Instance) ? "Entity" : String.Empty) + "FormControl ");

                sqlStatement.Append ("  WHERE " + ((formType == Mercury.Server.Core.Forms.Enumerations.FormType.Instance) ? "EntityFormId = " + entityFormId.ToString () : "FormId = " + formId.ToString ()));

                success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

                if (!success) { throw application.EnvironmentDatabase.LastException; }

                success = base.Save ();

                if (!success) { throw application.LastException; }

                application.EnvironmentDatabase.CommitTransaction ();

            }

            catch (Exception applicationException) {

                application.EnvironmentDatabase.RollbackTransaction ();

                application.SetLastException (applicationException);

                return false;

            }

#if DEBUG

            // System.Diagnostics.Debug.WriteLine ("Form Save from Database Time (SLOW): " + DateTime.Now.Subtract (startTime).TotalMilliseconds.ToString ());

            System.Diagnostics.Debug.WriteLine ("Form Save from Database Time (FAST): " + DateTime.Now.Subtract (startTime).TotalMilliseconds.ToString ());

#endif

            return true;

        }

        public void MapFormDataFields (System.Data.DataRow currentRow) {

            // base.MapDataFields (currentRow); // ONLY MAP FORM-LEVEL MEMBERS, BASE MEMBERS ARE MAPPED THROUGH THE CONTROL LOAD PROCESS


            formId = (Int64)currentRow["FormId"];

            switch (formType) {

                case Mercury.Server.Core.Forms.Enumerations.FormType.Template: entityFormId = 0; break;

                case Mercury.Server.Core.Forms.Enumerations.FormType.Instance:

                    entityFormId = (Int64)currentRow["EntityFormId"];

                    entityObjectId = (Int64)currentRow["EntityObjectId"];

                    break;

            }

            Description = (String)currentRow["FormDescription"];

            entityType = (Mercury.Server.Core.Enumerations.EntityType)currentRow["EntityType"];

            createAccountInfo.MapDataFields (currentRow, "Create");

            modifiedAccountInfo.MapDataFields (currentRow, "Modified");

            this.ControlId = (Guid)currentRow["FormControlId"];

            return;

        }

        #endregion


        #region Event Handlers

        public override List<String> Events {

            get {

                List<String> events = new List<String> ();

                events.Add ("Submit");

                return events;

            }

        }

        #endregion


        #region Data Bindings

        public override Dictionary<String, String> DataBindingContexts {

            get {

                Dictionary<String, String> bindingContexts = new Dictionary<String, String> ();

                bindingContexts.Add ("CreateDate", "DateTime");

                switch (entityType) {

                    case Mercury.Server.Core.Enumerations.EntityType.Member: bindingContexts.Add ("MemberId", "Id|Member"); break;

                    case Mercury.Server.Core.Enumerations.EntityType.Provider: bindingContexts.Add ("ProviderId", "Id|Provider"); break;

                    case Mercury.Server.Core.Enumerations.EntityType.Sponsor: bindingContexts.Add ("SponsorId", "Id|Sponsor"); break;

                    case Mercury.Server.Core.Enumerations.EntityType.Insurer: bindingContexts.Add ("InsurerId", "Id|Insurer"); break;

                }

                return bindingContexts;

            }

        }

        public override String EvaluateDataBinding (Structures.DataBinding dataBinding) {

            String dataValue = String.Empty;

            try {

                switch (dataBinding.BindingContext) {

                    case "CreateDate": dataValue = createAccountInfo.ActionDate.ToString ("MM/dd/yyyy hh:mm:ss"); break;

                    case "MemberId":

                    case "ProviderId":

                    case "SponsorId":

                    case "InsurerId":

                        dataValue = entityObjectId.ToString ();

                        break;

                } // end switch

            } // end try

            catch (Exception applicationException) {

                System.Diagnostics.Trace.WriteLine (Name + ".EvaluateDataBinding: " + applicationException.Message);

                System.Diagnostics.Trace.Flush ();

                dataValue = "!Error";

            }

            return dataValue;

        }

        #endregion


        #region Validate and Submit Form

        public List<CompileMessage> Submit () {

            List<CompileMessage> validationMessages = new List<CompileMessage> ();

            //validationMessages = ValidateControl ();

            //if (validationMessages.Count == 0) {

            RaiseEvent ("Submit");

            validationMessages = ValidateControl ();

            //}

            return validationMessages;

        }

        #endregion

    }

}
