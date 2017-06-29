using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Work {

    [Serializable]
    [DataContract (Name = "WorkQueueView")]
    public class WorkQueueView : CoreConfigurationObject {

        #region Private Properties
        
        [DataMember (Name = "FieldDefinitions")]
        private List<WorkQueueViewFieldDefinition> fieldDefinitions = new List<WorkQueueViewFieldDefinition> ();

        [DataMember (Name = "FilterDefinitions")]
        private SortedList<Int32, WorkQueueViewFilterDefinition> filterDefinitions = new SortedList<Int32, WorkQueueViewFilterDefinition> ();

        [DataMember (Name = "SortDefinitions")]
        private SortedList<Int32, WorkQueueViewSortDefinition> sortDefinitions = new SortedList<Int32, WorkQueueViewSortDefinition> ();

        #endregion


        #region Public Properties

        public List<WorkQueueViewFieldDefinition> FieldDefinitions { get { return fieldDefinitions; } set { fieldDefinitions = value; } }

        public SortedList<Int32, WorkQueueViewFilterDefinition> FilterDefinitions { get { return filterDefinitions; } set { filterDefinitions = value; } }

        public SortedList<Int32, WorkQueueViewSortDefinition> SortDefinitions { get { return sortDefinitions; } set { sortDefinitions = value; } }

        public Dictionary<String, Boolean> WellKnownFields {

            get {

                Dictionary<String, Boolean> wellKnownFields = new Dictionary<String, Boolean> ();

                wellKnownFields.Add ("WorkQueueItemId", true);

                wellKnownFields.Add ("WorkQueueId", false);

                wellKnownFields.Add ("ObjectType", true);

                wellKnownFields.Add ("ObjectId", false);

                wellKnownFields.Add ("ItemDescription", true);

                wellKnownFields.Add ("ItemGroupKey", true);

                wellKnownFields.Add ("WorkflowInstanceId", false);

                wellKnownFields.Add ("WorkflowStatus", false);

                wellKnownFields.Add ("WorkflowLastStep", true);

                wellKnownFields.Add ("WorkflowNextStep", true);

                wellKnownFields.Add ("AddedDate", true);

                wellKnownFields.Add ("LastWorkedDate", true);

                wellKnownFields.Add ("ConstraintDate", true);

                wellKnownFields.Add ("MilestoneDate", true);

                wellKnownFields.Add ("ThresholdDate", true);

                wellKnownFields.Add ("DueDate", true);

                wellKnownFields.Add ("CompletionDate", true);

                wellKnownFields.Add ("WorkOutcomeId", false);

                wellKnownFields.Add ("Priority", true);

                wellKnownFields.Add ("AssignedToSecurityAuthorityId", false);

                wellKnownFields.Add ("AssignedToUserAccountId", false);

                wellKnownFields.Add ("AssignedToUserAccountName", false);

                wellKnownFields.Add ("AssignedToUserDisplayName", true);

                wellKnownFields.Add ("AssignedToDate", true);

                wellKnownFields.Add ("ExtendedProperties", false);

                
                wellKnownFields.Add ("CreateAuthorityName", false);

                wellKnownFields.Add ("CreateAccountId", false);

                wellKnownFields.Add ("CreateAccountName", false);

                wellKnownFields.Add ("CreateDate", false);


                wellKnownFields.Add ("ModifiedAuthorityName", false);

                wellKnownFields.Add ("ModifiedAccountId", false);

                wellKnownFields.Add ("ModifiedAccountName", false);

                wellKnownFields.Add ("ModifiedDate", false);


                wellKnownFields.Add ("IsAssigned", true);

                wellKnownFields.Add ("IsCompleted", true);

                wellKnownFields.Add ("HasConstraintDatePassed", true);

                wellKnownFields.Add ("HasMilestoneDatePassed", true);

                wellKnownFields.Add ("HasThresholdDatePassed", true);

                wellKnownFields.Add ("HasDueDatePassed", true);

                wellKnownFields.Add ("WithinWorkTimeRestrictions", true);


                return wellKnownFields;

            }

        }

        #endregion


        #region Constructors

        public WorkQueueView (Application applicationReference) {

            base.BaseConstructor (applicationReference);

            return;

        }

        public WorkQueueView (Application applicationReference, Int64 forId) {

            base.BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion


        #region XML Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();


            #region Field Definitions

            System.Xml.XmlNode fieldDefinitionsNodes = document.CreateElement ("FieldDefinitions");

            document.LastChild.AppendChild (fieldDefinitionsNodes);

            foreach (WorkQueueViewFieldDefinition currentFieldDefinition in fieldDefinitions) {

                // TODO: 

            }

            #endregion 
            

            #region Filter Definitions

            System.Xml.XmlNode filterDefinitionsNodes = document.CreateElement ("FilterDefinitions");

            document.LastChild.AppendChild (filterDefinitionsNodes);

            // TODO: 

            #endregion 
            

            #region Sort Definitions

            System.Xml.XmlNode sortDefinitionsNodes = document.CreateElement ("SortDefinitions");

            document.LastChild.AppendChild (sortDefinitionsNodes);

            // TODO: 

            #endregion 


            return document;

        }

        //public override List<Mercury.Server.Services.Responses.ConfigurationImportResponse> XmlImport (System.Xml.XmlNode objectNode) {

        //    List<Mercury.Server.Services.Responses.ConfigurationImportResponse> response = new List<Mercury.Server.Services.Responses.ConfigurationImportResponse> ();

        //    Services.Responses.ConfigurationImportResponse importResponse = new Mercury.Server.Services.Responses.ConfigurationImportResponse ();


        //    importResponse.ObjectType = objectNode.Name;

        //    importResponse.ObjectName = objectNode.Attributes["Name"].InnerText;

        //    importResponse.Success = true;


        //    if (importResponse.ObjectType == "WorkQueueView") {

        //        foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {

        //            switch (currentNode.Name) {

        //                case "Properties":

        //                    foreach (System.Xml.XmlNode currentProperty in currentNode.ChildNodes) {

        //                        switch (currentProperty.Attributes["Name"].InnerText) {

        //                            case "WorkQueueViewName": workQueueViewName = currentProperty.InnerText; break;

        //                            case "Description": description = currentProperty.InnerText; break;

        //                            case "Enabled": enabled = Convert.ToBoolean (currentProperty.InnerText); break;

        //                            case "Visible": visible = Convert.ToBoolean (currentProperty.InnerText); break;

        //                        }

        //                    }

        //                    break;

        //            }

        //        }

        //        importResponse.Success = Save ();

        //        importResponse.Id = Id;

        //        if (!importResponse.Success) { importResponse.SetException (base.application.LastException); }

        //    }

        //    else { importResponse.SetException (new ApplicationException ("Invalid Object Type Parsed as WorkQueueView.")); }

        //    response.Add (importResponse);

        //    return response;

        //}

        #endregion


        #region Validation Functions

        public Dictionary<String, String> ValidateFieldDefinition (WorkQueueViewFieldDefinition fieldDefinition) {

            Dictionary<String, String> validationResponse = new Dictionary<String, String> ();


            fieldDefinition.DisplayName = fieldDefinition.DisplayName.Trim ();

            fieldDefinition.PropertyName = fieldDefinition.PropertyName.Trim ();


            if (String.IsNullOrEmpty (fieldDefinition.DisplayName.Trim ())) { validationResponse.Add ("Display Name", "Empty or Null."); }

            if (String.IsNullOrEmpty (fieldDefinition.PropertyName.Trim ())) { validationResponse.Add ("Property Name", "Empty or Null."); }



            System.Text.RegularExpressions.Regex expressionSpecialCharacters;

            String excludedCharacters = String.Empty;

            excludedCharacters = excludedCharacters + @"\\|/|\.|,|'|~|`|!|@|#|\$|%|\^|&|\*|\(|\)|\-|\+|\=|\{|\}|\[|\]|:|;|\?|<|>";

            expressionSpecialCharacters = new System.Text.RegularExpressions.Regex (excludedCharacters);

            if (expressionSpecialCharacters.IsMatch (fieldDefinition.DisplayName)) {

                validationResponse.Add ("Display Name", "Special Characters not allowed.");

            }


            foreach (WorkQueueViewFieldDefinition currentFieldDefinition in fieldDefinitions) {

                if (currentFieldDefinition != fieldDefinition) {

                    if (currentFieldDefinition.DisplayName == fieldDefinition.DisplayName) {

                        validationResponse.Add ("Display Name", "Duplicate.");

                    }

                    if (currentFieldDefinition.PropertyName == fieldDefinition.PropertyName) {

                        validationResponse.Add ("Property Name", "Duplicate.");

                    }

                }

            }

            if (WellKnownFields.ContainsKey (fieldDefinition.DisplayName)) {

                if (!validationResponse.ContainsKey ("Display Name")) {

                    validationResponse.Add ("Display Name", "Reserved Name not allowed.");

                }

            }

            return validationResponse;

        }

        #endregion


        #region Data Functions

        public override Boolean Load (Int64 forId) {

            Boolean success = false;

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable dataTable;


            if (application.EnvironmentDatabase == null) { return false; }

            selectStatement.Append ("SELECT * FROM dbo.WorkQueueView WHERE WorkQueueViewId = " + forId.ToString ());

            dataTable = application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString (), 0);

            if (dataTable.Rows.Count == 1) {

                MapDataFields (dataTable.Rows[0]);

                success = true;

            }

            else { success = false; }


            if (success) {

                // LOAD CHILD OBJECTS 

            }

            return success;

        }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);

            String selectStatement;

            Int32 currentSequence = 0;


            selectStatement = "SELECT * FROM WorkQueueViewFieldDefinition WHERE WorkQueueViewId = " + Id.ToString ();

            foreach (System.Data.DataRow currentField in application.EnvironmentDatabase.SelectDataTable (selectStatement, 0).Rows) {

                WorkQueueViewFieldDefinition fieldDefinition = new WorkQueueViewFieldDefinition ();

                fieldDefinition.MapDataFields (currentField);

                fieldDefinitions.Add (fieldDefinition);

            }

            // LOAD FILTER DEFINITIONS

            selectStatement = "SELECT * FROM WorkQueueViewFilterDefinition WHERE WorkQueueViewId = " + Id.ToString () + " ORDER BY Sequence";

            currentSequence = 0;

            foreach (System.Data.DataRow currentFilter in application.EnvironmentDatabase.SelectDataTable (selectStatement, 0).Rows) {

                currentSequence = currentSequence + 1;

                WorkQueueViewFilterDefinition filterDefinition = new WorkQueueViewFilterDefinition ();

                filterDefinition.MapDataFields (currentFilter);

                filterDefinition.Sequence = currentSequence;

                filterDefinitions.Add (currentSequence, filterDefinition);

            }

            // LOAD SORT DEFINITIONS

            selectStatement = "SELECT * FROM WorkQueueViewSortDefinition WHERE WorkQueueViewId = " + Id.ToString () + " ORDER BY Sequence";

            currentSequence = 0;

            foreach (System.Data.DataRow currentSort in application.EnvironmentDatabase.SelectDataTable (selectStatement, 0).Rows) {

                currentSequence = currentSequence + 1;

                WorkQueueViewSortDefinition sortDefinition = new WorkQueueViewSortDefinition ();

                sortDefinition.MapDataFields (currentSort);

                sortDefinition.Sequence = currentSequence;

                sortDefinitions.Add (currentSequence, sortDefinition);

            }

            return;

        }

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            String childIds = String.Empty;

            Int32 currentSequence = 0;


            if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.WorkQueueViewManage)) { throw new ApplicationException ("Permission Denied."); }

            Dictionary<String, String> validationResponse = Validate ();

            if (validationResponse.Count != 0) {

                foreach (String validationKey in validationResponse.Keys) {

                    throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                }

            }

            modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);

            try {

                application.EnvironmentDatabase.BeginTransaction ();

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dbo.WorkQueueView_InsertUpdate ");

                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append ("'" + NameSql + "', ");

                sqlStatement.Append ("'" + DescriptionSql + "', ");


                sqlStatement.Append (Convert.ToInt32 (Enabled).ToString () + ", ");

                sqlStatement.Append (Convert.ToInt32 (Visible).ToString () + ", ");


                sqlStatement.Append (modifiedAccountInfo.AccountInfoSql);


                success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }

                SetIdentity ();

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }


                success = application.EnvironmentDatabase.ExecuteSqlStatement ("DELETE FROM WorkQueueViewFieldDefinition WHERE WorkQueueViewId = " + Id.ToString (), 0);

                if (!success) { throw new ApplicationException ("Unable to delete associated Work Queue View Field Definitions."); }

                foreach (WorkQueueViewFieldDefinition currentFieldDefinition in fieldDefinitions) {

                    sqlStatement = new StringBuilder ();

                    sqlStatement.Append ("INSERT INTO WorkQueueViewFieldDefinition (WorkQueueViewId, PropertyName, DisplayName, DefaultValue, DataType) VALUES (");

                    sqlStatement.Append (Id.ToString () + ", ");

                    sqlStatement.Append ("'" + currentFieldDefinition.PropertyName.Replace ("'", "''") + "', ");

                    sqlStatement.Append ("'" + currentFieldDefinition.DisplayName.Replace ("'", "''") + "', ");

                    sqlStatement.Append ("'" + currentFieldDefinition.DefaultValue.Replace ("'", "''") + "', ");

                    sqlStatement.Append (Convert.ToInt32 (currentFieldDefinition.DataType).ToString ());

                    sqlStatement.Append (")");

                    success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                    if (!success) { throw new ApplicationException ("Unable to insert associated Work Queue View Field Definition."); }

                }



                #region Save Filters

                success = application.EnvironmentDatabase.ExecuteSqlStatement ("DELETE FROM WorkQueueViewFilterDefinition WHERE WorkQueueViewId = " + Id.ToString (), 0);

                if (!success) { throw new ApplicationException ("Unable to delete associated Work Queue View Filter Definitions."); }

                currentSequence = 0;

                foreach (WorkQueueViewFilterDefinition currentFilterDefinition in filterDefinitions.Values) {

                    currentSequence = currentSequence + 1;

                    currentFilterDefinition.Sequence = currentSequence;


                    sqlStatement = new StringBuilder ();

                    sqlStatement.Append ("INSERT INTO WorkQueueViewFilterDefinition (WorkQueueViewId, Sequence, FieldName, FilterOperator, FilterValue) VALUES (");

                    sqlStatement.Append (Id.ToString () + ", ");

                    sqlStatement.Append (currentFilterDefinition.Sequence.ToString () + ", ");

                    sqlStatement.Append ("'" + currentFilterDefinition.PropertyPath.Replace ("'", "''") + "', ");

                    sqlStatement.Append (Convert.ToInt32 (currentFilterDefinition.Operator).ToString () + ", ");

                    sqlStatement.Append ("'" + currentFilterDefinition.Parameter.Value.ToString ().Replace ("'", "''") + "'");
                    
                    sqlStatement.Append (")");

                    success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                    if (!success) { throw new ApplicationException ("Unable to insert associated Work Queue View Filter Definition."); }

                }

                #endregion 


                #region Save Sorts
                
                success = application.EnvironmentDatabase.ExecuteSqlStatement ("DELETE FROM WorkQueueViewSortDefinition WHERE WorkQueueViewId = " + Id.ToString (), 0);

                if (!success) { throw new ApplicationException ("Unable to delete associated Work Queue View Sort Definitions."); }

                currentSequence = 0;

                foreach (WorkQueueViewSortDefinition currentSortDefinition in sortDefinitions.Values) {

                    currentSequence = currentSequence + 1;

                    currentSortDefinition.Sequence = currentSequence;


                    sqlStatement = new StringBuilder ();

                    sqlStatement.Append ("INSERT INTO WorkQueueViewSortDefinition (WorkQueueViewId, Sequence, FieldName, SortDirection) VALUES (");

                    sqlStatement.Append (Id.ToString () + ", ");

                    sqlStatement.Append (currentSortDefinition.Sequence.ToString () + ", ");

                    sqlStatement.Append ("'" + currentSortDefinition.FieldName.Replace ("'", "''") + "', ");

                    sqlStatement.Append (Convert.ToInt32 (currentSortDefinition.SortDirection).ToString ());

                    sqlStatement.Append (")");

                    success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                    if (!success) { throw new ApplicationException ("Unable to insert associated Work Queue View Sort Definition."); }

                }

                #endregion 


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

    }

}
