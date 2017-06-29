using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Mercury.Web.Application.Configuration.PropertyPages {

    public partial class WorkQueueView : System.Web.UI.Page {

        #region Private Properties

        Mercury.Client.Core.Work.WorkQueueView workQueueView;

        #endregion


        #region Private Session Cache

        private String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (PageInstanceId.Text)) { PageInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return Form.Name + PageInstanceId.Text + ".";

            }

        }

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application) Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }


            Int64 forWorkQueueViewId = 0;


            if ((!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkQueueViewReview))

                && (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkQueueViewManage))) { 
                
                Response.Redirect ("/PermissionDenied.aspx", true); return; 
            
            }


            if (!Page.IsPostBack) {

                #region Initial Page Load

                if (Request.QueryString["WorkQueueViewId"] != null) {

                    forWorkQueueViewId = Int64.Parse (Request.QueryString["WorkQueueViewId"]);

                }

                if (forWorkQueueViewId != 0) {

                    workQueueView = MercuryApplication.WorkQueueViewGet (forWorkQueueViewId, false);

                    if (workQueueView == null) {

                        workQueueView = new Mercury.Client.Core.Work.WorkQueueView (MercuryApplication);

                    }

                    Page.Title = "Work Queue View - " + workQueueView.Name;

                }

                else {

                    workQueueView = new Mercury.Client.Core.Work.WorkQueueView (MercuryApplication);

                }

                InitializeAll ();

                Session[SessionCachePrefix + "WorkQueueView"] = workQueueView;

                Session[SessionCachePrefix + "WorkQueueViewUnmodified"] = workQueueView.Copy ();

                #endregion

            } // Initial Page Load

            else { // Postback

                workQueueView = (Mercury.Client.Core.Work.WorkQueueView) Session[SessionCachePrefix + "WorkQueueView"];

            }

            ApplySecurity ();

            if (!String.IsNullOrEmpty (workQueueView.Name)) { Page.Title = "Work Queue View - " + workQueueView.Name; } else { Page.Title = "New Work Queue View"; }

            return;

        }

        protected void Page_Unload (object sender, EventArgs e) {

            MercuryApplication.ApplicationClientClose ();

            return;

        }

        #endregion


        #region Initialization

        private void InitializeAll () {

            InitializeGeneralPage ();

            InitializeCustomFieldsPage ();

            InitializeFilteringPage ();

            InitializeSortingPage ();

            InitializePreviewPage ();

            return;

        }

        private void InitializeGeneralPage () {

            WorkQueueViewName.Text = workQueueView.Name;

            WorkQueueViewDescription.Text = workQueueView.Description;


            WorkQueueViewEnabled.Checked = workQueueView.Enabled;

            WorkQueueViewVisible.Checked = workQueueView.Visible;


            WorkQueueViewCreateAuthorityName.Text = workQueueView.CreateAccountInfo.SecurityAuthorityName;

            WorkQueueViewCreateAccountId.Text = workQueueView.CreateAccountInfo.UserAccountId;

            WorkQueueViewCreateAccountName.Text = workQueueView.CreateAccountInfo.UserAccountName;

            WorkQueueViewCreateDate.MinDate = DateTime.MinValue;

            WorkQueueViewCreateDate.SelectedDate = workQueueView.CreateAccountInfo.ActionDate;


            WorkQueueViewModifiedAuthorityName.Text = workQueueView.ModifiedAccountInfo.SecurityAuthorityName;

            WorkQueueViewModifiedAccountId.Text = workQueueView.ModifiedAccountInfo.UserAccountId;

            WorkQueueViewModifiedAccountName.Text = workQueueView.ModifiedAccountInfo.UserAccountName;

            WorkQueueViewModifiedDate.MinDate = DateTime.MinValue;

            WorkQueueViewModifiedDate.SelectedDate = workQueueView.ModifiedAccountInfo.ActionDate;

            return;

        }

        private void InitializeCustomFieldsPage () {

            System.Data.DataTable fieldsTable = new DataTable ();

            fieldsTable.Columns.Add ("DisplayName");

            fieldsTable.Columns.Add ("PropertyName");

            fieldsTable.Columns.Add ("DataType");

            fieldsTable.Columns.Add ("DefaultValue");


            foreach (Server.Application.WorkQueueViewFieldDefinition currentFieldDefinition in workQueueView.FieldDefinitions) {

                fieldsTable.Rows.Add (

                    currentFieldDefinition.DisplayName,

                    currentFieldDefinition.PropertyName,

                    currentFieldDefinition.DataType.ToString (),

                    currentFieldDefinition.DefaultValue

                );

            }

            WorkQueueViewFieldsGrid.DataSource = fieldsTable;

            WorkQueueViewFieldsGrid.DataBind ();

            return;

        }

        private void InitializeFilteringPage () {

            System.Data.DataTable filterTable = new DataTable ();

            filterTable.Columns.Add ("Sequence");

            filterTable.Columns.Add ("FieldName");

            filterTable.Columns.Add ("FilterOperator");

            filterTable.Columns.Add ("FilterValue");


            foreach (Int32 currentSequence in workQueueView.FilterDefinitions.Keys) {

                Server.Application.WorkQueueViewFilterDefinition currentFilterDefinition = workQueueView.FilterDefinitions[currentSequence];

                filterTable.Rows.Add (

                    currentSequence,

                    currentFilterDefinition.PropertyPath,

                    Mercury.Server.CommonFunctions.EnumerationToString (currentFilterDefinition.Operator),

                    currentFilterDefinition.Parameter.Value

                );

            }

            WorkQueueViewFilterGrid.DataSource = filterTable;

            WorkQueueViewFilterGrid.DataBind ();


            List<String> availableFilterFields = new List<String> ();

            Dictionary<String, Boolean> wellKnownFields = MercuryApplication.WorkQueueView_WellKnownFields (workQueueView);

            Boolean filterDefined = false;

            foreach (String currentField in wellKnownFields.Keys) {

                filterDefined = false;

                foreach (Server.Application.WorkQueueViewFilterDefinition currentExistingFilter in workQueueView.FilterDefinitions.Values) {

                    if (currentExistingFilter.PropertyPath == currentField) {

                        filterDefined = true;

                        break;

                    }

                }

                if ((!filterDefined) && (wellKnownFields[currentField])) { availableFilterFields.Add (currentField); }

            }

            foreach (Server.Application.WorkQueueViewFieldDefinition currentFieldDefinition in workQueueView.FieldDefinitions) {

                filterDefined = false;

                foreach (Server.Application.WorkQueueViewFilterDefinition currentExistingFilter in workQueueView.FilterDefinitions.Values) {

                    if (currentExistingFilter.PropertyPath == currentFieldDefinition.DisplayName) {

                        filterDefined = true;

                        break;

                    }

                }

                if (!filterDefined) { availableFilterFields.Add (currentFieldDefinition.DisplayName); }

            }

            availableFilterFields.Sort ();

            FilteringFieldSelection.DataSource = availableFilterFields;

            FilteringFieldSelection.DataBind ();

            return;

        }

        private void InitializeSortingPage () {

            System.Data.DataTable sortTable = new DataTable ();

            sortTable.Columns.Add ("Sequence");

            sortTable.Columns.Add ("FieldName");

            sortTable.Columns.Add ("SortDirection");


            foreach (Int32 currentSequence in workQueueView.SortDefinitions.Keys) {

                Server.Application.WorkQueueViewSortDefinition currentSortDefinition = workQueueView.SortDefinitions[currentSequence];

                sortTable.Rows.Add (

                    currentSequence, 

                    currentSortDefinition.FieldName,

                    currentSortDefinition.SortDirection.ToString ()

                );

            }

            WorkQueueViewSortGrid.DataSource = sortTable;

            WorkQueueViewSortGrid.DataBind ();


            List<String> availableSortFields = new List<String> ();

            Dictionary<String, Boolean> wellKnownFields = MercuryApplication.WorkQueueView_WellKnownFields (workQueueView);

            Boolean sortDefined = false;

            foreach (String currentField in wellKnownFields.Keys) {

                sortDefined = false;

                foreach (Server.Application.WorkQueueViewSortDefinition currentExistingSort in workQueueView.SortDefinitions.Values) {

                    if (currentExistingSort.FieldName == currentField) {

                        sortDefined = true;

                        break;

                    }

                }

                if ((!sortDefined) && (wellKnownFields [currentField])) { availableSortFields.Add (currentField); }

            }

            foreach (Server.Application.WorkQueueViewFieldDefinition currentFieldDefinition in workQueueView.FieldDefinitions) {

                sortDefined = false;

                foreach (Server.Application.WorkQueueViewSortDefinition currentExistingSort in workQueueView.SortDefinitions.Values) {

                    if (currentExistingSort.FieldName == currentFieldDefinition.DisplayName) {

                        sortDefined = true;

                        break;

                    }

                }

                if (!sortDefined) { availableSortFields.Add (currentFieldDefinition.DisplayName); }

            }

            availableSortFields.Sort ();

            SortingFieldSelection.DataSource = availableSortFields;

            SortingFieldSelection.DataBind ();
            
            return;

        }

        private void InitializePreviewPage () {

            PreviewWorkQueueSelection.DataSource = MercuryApplication.WorkQueuesAvailable (false);

            PreviewWorkQueueSelection.DataTextField = "Name";

            PreviewWorkQueueSelection.DataValueField = "Id";

            PreviewWorkQueueSelection.DataBind ();

            return;

        }

        private void ApplySecurity () {

            Boolean hasManagePermission = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkQueueViewManage);


            WorkQueueViewName.ReadOnly = !hasManagePermission;

            WorkQueueViewDescription.ReadOnly = !hasManagePermission;

            WorkQueueViewEnabled.Enabled = hasManagePermission;

            WorkQueueViewVisible.Enabled = hasManagePermission;


            ButtonCancel.Visible = hasManagePermission;

            ButtonApply.Visible = hasManagePermission;


            return;

        }

        #endregion


        #region Custom Fields, Filtering, and Sorting

        private void WorkQueueViewFilterDefinitions_Resequence () {

            SortedList<Int32, Server.Application.WorkQueueViewFilterDefinition> newFilter = new SortedList<Int32, Server.Application.WorkQueueViewFilterDefinition> ();

            Int32 newSequence = 0;

            foreach (Int32 currentSequence in workQueueView.FilterDefinitions.Keys) {

                newSequence = newSequence + 1;

                newFilter.Add (newSequence, workQueueView.FilterDefinitions[currentSequence]);

                newFilter[newSequence].Sequence = newSequence;

            }

            workQueueView.FilterDefinitions = newFilter;

            return;
        }

        private void WorkQueueViewSortDefinitions_Resequence () {

            SortedList<Int32, Server.Application.WorkQueueViewSortDefinition> newSort = new SortedList<Int32, Server.Application.WorkQueueViewSortDefinition> ();

            Int32 newSequence = 0;

            foreach (Int32 currentSequence in workQueueView.SortDefinitions.Keys) {

                newSequence = newSequence + 1;

                newSort.Add (newSequence, workQueueView.SortDefinitions[currentSequence]);

                newSort[newSequence].Sequence = newSequence;

            }

            workQueueView.SortDefinitions = newSort;

            return;
        }


        protected void ButtonAddCustomField_OnClick (Object sender, EventArgs e) {

            Boolean isValid = false;

            Dictionary<String, String> validationResponse;



            CustomFieldErrorLabel.Text = String.Empty;


            Server.Application.WorkQueueViewFieldDefinition fieldDefinition = new Server.Application.WorkQueueViewFieldDefinition ();

            fieldDefinition.DisplayName = CustomFieldDisplayName.Text;

            fieldDefinition.PropertyName = CustomFieldPropertyName.Text;

            fieldDefinition.DataType = (SqlDbType) Convert.ToInt32 (CustomFieldDataTypeSelection.SelectedValue);

            fieldDefinition.DefaultValue = CustomFieldDefaultValue.Text;



            validationResponse = MercuryApplication.WorkQueueView_ValidateFieldDefinition ((Server.Application.WorkQueueView) workQueueView.ToServerObject (), fieldDefinition);

            isValid = (validationResponse.Count == 0);



            if (isValid) {

                workQueueView.FieldDefinitions.Add (fieldDefinition);

                InitializeCustomFieldsPage ();

                InitializeFilteringPage ();

                InitializeSortingPage ();

            }

            else if (!isValid) {

                foreach (String validationKey in validationResponse.Keys) {

                    CustomFieldErrorLabel.Text = "Invalid [" + validationKey + "]: " + validationResponse[validationKey];

                    break;

                }

            }

            return;

        }

        protected void WorkQueueViewFieldsGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }


            Int32 deleteIndex = eventArgs.Item.DataSetIndex;

            //            Int64 workTeamId = Convert.ToInt64 (eventArgs.Item.OwnerTableView.DataKeyValues[deleteIndex]["WorkTeamId"]);

            foreach (Server.Application.WorkQueueViewFilterDefinition currentExistingFilter in workQueueView.FilterDefinitions.Values) {

                if (currentExistingFilter.PropertyPath == workQueueView.FieldDefinitions[deleteIndex].DisplayName) {

                    if (workQueueView.FilterDefinitions.ContainsKey (currentExistingFilter.Sequence)) {

                        workQueueView.FilterDefinitions.Remove (currentExistingFilter.Sequence);

                        WorkQueueViewFilterDefinitions_Resequence ();

                    }

                    break;

                }

            }

            foreach (Server.Application.WorkQueueViewSortDefinition currentExistingSort in workQueueView.SortDefinitions.Values) {

                if (currentExistingSort.FieldName == workQueueView.FieldDefinitions[deleteIndex].DisplayName) {

                    if (workQueueView.SortDefinitions.ContainsKey (currentExistingSort.Sequence)) {

                        workQueueView.SortDefinitions.Remove (currentExistingSort.Sequence);

                        WorkQueueViewSortDefinitions_Resequence ();

                    }

                    break;

                }

            }

            workQueueView.FieldDefinitions.RemoveAt (deleteIndex);


            InitializeCustomFieldsPage ();

            InitializeFilteringPage ();

            InitializeSortingPage ();

            return;

        }


        protected void ButtonAddFilter_OnClick (Object sender, EventArgs e) {

            if (!String.IsNullOrEmpty (FilteringFieldSelection.SelectedValue)) {

                Server.Application.WorkQueueViewFilterDefinition filterDefinition = new Mercury.Server.Application.WorkQueueViewFilterDefinition ();

                filterDefinition.Sequence = workQueueView.FilterDefinitions.Count + 1;


                filterDefinition.PropertyPath = FilteringFieldSelection.SelectedValue;

                filterDefinition.Parameter = new Mercury.Server.Application.DataContract ();

                filterDefinition.Parameter.Name = "Value";

                filterDefinition.Parameter.Value = FilteringValue.Text;

                filterDefinition.Operator = (Mercury.Server.Application.DataFilterOperator)Convert.ToInt32 (FilteringOperatorSelection.SelectedValue);


                // TODO: VALIDATE


                workQueueView.FilterDefinitions.Add (workQueueView.FilterDefinitions.Count + 1, filterDefinition);

            }

            InitializeFilteringPage ();

            return;

        }

        protected void WorkQueueViewFilterGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            Int32 deleteIndex = eventArgs.Item.DataSetIndex;

            workQueueView.FilterDefinitions.RemoveAt (deleteIndex);

            WorkQueueViewFilterDefinitions_Resequence ();

            InitializeFilteringPage ();

            return;

        }

        protected void WorkQueueViewFilterGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            Int32 itemIndex = eventArgs.Item.ItemIndex;

            Int32 sequence;

            String direction = eventArgs.CommandName;

            SortedList<Int32, Server.Application.WorkQueueViewFilterDefinition> newFilterDefinitions = new SortedList<int, Mercury.Server.Application.WorkQueueViewFilterDefinition> ();


            switch (direction.ToLowerInvariant ()) {

                case "moveup":

                    if (itemIndex != 0) {

                        for (Int32 currentIndex = 0; currentIndex < (itemIndex - 1); currentIndex++) {

                            sequence = currentIndex + 1;

                            newFilterDefinitions.Add (sequence, workQueueView.FilterDefinitions[sequence]);

                        }

                        // SWITCH THE TWO SPOTS

                        sequence = itemIndex + 1;

                        newFilterDefinitions.Add (itemIndex, workQueueView.FilterDefinitions[sequence]);

                        newFilterDefinitions[itemIndex].Sequence = itemIndex;

                        sequence = itemIndex;

                        newFilterDefinitions.Add (itemIndex + 1, workQueueView.FilterDefinitions[sequence]);

                        newFilterDefinitions[itemIndex + 1].Sequence = itemIndex + 1;


                        for (Int32 currentIndex = itemIndex + 1; currentIndex < workQueueView.FilterDefinitions.Count; currentIndex++) {

                            sequence = currentIndex + 1;

                            newFilterDefinitions.Add (sequence, workQueueView.FilterDefinitions[sequence]);

                        }

                        workQueueView.FilterDefinitions = newFilterDefinitions;

                    }

                    break;

                case "movedown":

                    if (itemIndex != (workQueueView.FilterDefinitions.Count - 1)) {

                        for (Int32 currentIndex = 0; currentIndex <= (itemIndex - 1); currentIndex++) {

                            sequence = currentIndex + 1;

                            newFilterDefinitions.Add (sequence, workQueueView.FilterDefinitions[sequence]);

                        }


                        // I = 1 / S = 2
                        // I = 2 / S = 3

                        // I = 1 / S = 3
                        //       / S = 2


                        // SWITCH THE TWO SPOTS

                        sequence = itemIndex + 1;

                        newFilterDefinitions.Add (sequence + 1, workQueueView.FilterDefinitions[sequence]);

                        newFilterDefinitions[sequence + 1].Sequence = itemIndex + 2;


                        sequence = itemIndex + 2;

                        newFilterDefinitions.Add (sequence - 1, workQueueView.FilterDefinitions[sequence]);

                        newFilterDefinitions[sequence - 1].Sequence = sequence - 1;


                        for (Int32 currentIndex = itemIndex + 2; currentIndex < workQueueView.FilterDefinitions.Count; currentIndex++) {

                            sequence = currentIndex + 1;

                            newFilterDefinitions.Add (sequence, workQueueView.FilterDefinitions[sequence]);

                        }

                        workQueueView.FilterDefinitions = newFilterDefinitions;

                    }

                    break;

            }


            InitializeFilteringPage ();

            return;

        }


        protected void ButtonAddSort_OnClick (Object sender, EventArgs e) {

            if (!String.IsNullOrEmpty (SortingFieldSelection.SelectedValue)) {

                Server.Application.WorkQueueViewSortDefinition sortDefinition = new Mercury.Server.Application.WorkQueueViewSortDefinition ();

                sortDefinition.Sequence = workQueueView.SortDefinitions.Count + 1;

                sortDefinition.FieldName = SortingFieldSelection.SelectedValue;

                sortDefinition.SortDirection = (Mercury.Server.Application.DataSortDirection) Convert.ToInt32 (SortingDirectionSelection.SelectedValue);

                workQueueView.SortDefinitions.Add (workQueueView.SortDefinitions.Count + 1, sortDefinition);

            }

            InitializeSortingPage ();

            return;

        }

        protected void WorkQueueViewSortGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            Int32 deleteIndex = eventArgs.Item.DataSetIndex;

            workQueueView.SortDefinitions.RemoveAt (deleteIndex);

            WorkQueueViewSortDefinitions_Resequence ();

            InitializeSortingPage ();

            return;

        }

        protected void WorkQueueViewSortGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            Int32 itemIndex = eventArgs.Item.ItemIndex;

            Int32 sequence;

            String direction = eventArgs.CommandName;

            SortedList<Int32, Server.Application.WorkQueueViewSortDefinition> newSortDefinitions = new SortedList<int, Mercury.Server.Application.WorkQueueViewSortDefinition> ();


            switch (direction.ToLowerInvariant ()) {

                case "moveup":

                    if (itemIndex != 0) {

                        for (Int32 currentIndex = 0; currentIndex < (itemIndex - 1); currentIndex++) {

                            sequence = currentIndex + 1;

                            newSortDefinitions.Add (sequence, workQueueView.SortDefinitions[sequence]);

                        }

                        // SWITCH THE TWO SPOTS

                        sequence = itemIndex + 1;

                        newSortDefinitions.Add (itemIndex, workQueueView.SortDefinitions[sequence]);

                        newSortDefinitions[itemIndex].Sequence = itemIndex;

                        sequence = itemIndex;

                        newSortDefinitions.Add (itemIndex + 1, workQueueView.SortDefinitions[sequence]);

                        newSortDefinitions[itemIndex + 1].Sequence = itemIndex + 1;


                        for (Int32 currentIndex = itemIndex + 1; currentIndex < workQueueView.SortDefinitions.Count; currentIndex++) {

                            sequence = currentIndex + 1;

                            newSortDefinitions.Add (sequence, workQueueView.SortDefinitions[sequence]);

                        }

                        workQueueView.SortDefinitions = newSortDefinitions;

                    }

                    break;

                case "movedown":

                    if (itemIndex != (workQueueView.SortDefinitions.Count - 1)) {

                        for (Int32 currentIndex = 0; currentIndex <= (itemIndex - 1); currentIndex++) {

                            sequence = currentIndex + 1;

                            newSortDefinitions.Add (sequence, workQueueView.SortDefinitions[sequence]);

                        }


                        // I = 1 / S = 2
                        // I = 2 / S = 3

                        // I = 1 / S = 3
                        //       / S = 2


                        // SWITCH THE TWO SPOTS

                        sequence = itemIndex + 1;

                        newSortDefinitions.Add (sequence + 1, workQueueView.SortDefinitions[sequence]);

                        newSortDefinitions[sequence + 1].Sequence = itemIndex + 2;


                        sequence = itemIndex + 2;

                        newSortDefinitions.Add (sequence - 1, workQueueView.SortDefinitions[sequence]);

                        newSortDefinitions[sequence - 1].Sequence = sequence - 1;


                        for (Int32 currentIndex = itemIndex + 2; currentIndex < workQueueView.SortDefinitions.Count; currentIndex++) {

                            sequence = currentIndex + 1;

                            newSortDefinitions.Add (sequence, workQueueView.SortDefinitions[sequence]);

                        }

                        workQueueView.SortDefinitions = newSortDefinitions;

                    }

                    break;

            }


            InitializeSortingPage ();

            return;

        }

        #endregion


        #region Preview

        protected void ButtonPreview_OnClick (Object sender, EventArgs e) {

            List<Client.Core.Work.WorkQueueItem> items;

            List<Server.Application.DataFilterDescriptor> filters = new List<Mercury.Server.Application.DataFilterDescriptor> ();


            if (!String.IsNullOrEmpty (PreviewWorkQueueSelection.SelectedValue)) {

                Server.Application.DataFilterDescriptor filter = new Mercury.Server.Application.DataFilterDescriptor ();

                filter.PropertyPath = "WorkQueueId";

                filter.Operator = Mercury.Server.Application.DataFilterOperator.IsEqualTo;

                filter.Parameter = new Mercury.Server.Application.DataContract ();

                filter.Parameter.Name = "Value";

                filter.Parameter.Value = PreviewWorkQueueSelection.SelectedValue;

                filters.Add (filter);

            }


            items = MercuryApplication.WorkQueueItemsGetByViewPage (workQueueView, filters, null, 1, 100, false);

            PreviewGrid.DataSource = items;

            PreviewGrid.DataBind ();

            return;

        }

        #endregion 


        #region Dialog Button Event Handlers

        protected Boolean ApplyChanges () {

            Boolean success = false;

            Boolean isModified = false;

            Boolean isValid = false;

            Dictionary<String, String> validationResponse;


            if (MercuryApplication == null) { return false; }


            Mercury.Client.Core.Work.WorkQueueView workQueueViewUnmodified = (Mercury.Client.Core.Work.WorkQueueView) Session[SessionCachePrefix + "WorkQueueViewUnmodified"];


            workQueueView.Name = WorkQueueViewName.Text;

            workQueueView.Description = WorkQueueViewDescription.Text;

            workQueueView.Enabled = WorkQueueViewEnabled.Checked;

            workQueueView.Visible = WorkQueueViewVisible.Checked;


            if (workQueueViewUnmodified.Id == 0) { isModified = true; }

            if (!isModified) { isModified = !workQueueView.IsEqual (workQueueViewUnmodified); }


            validationResponse = workQueueView.Validate ();

            isValid = (validationResponse.Count == 0);


            if ((isModified) && (isValid)) {

                if (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkQueueViewManage)) {

                    SaveResponseLabel.Text = "Permission Denied.";

                    return false;

                }

                success = MercuryApplication.WorkQueueViewSave (workQueueView);

                if (success) {

                    workQueueView = MercuryApplication.WorkQueueViewGet (workQueueView.Id, false);

                    Session[SessionCachePrefix + "WorkQueueView"] = workQueueView;

                    Session[SessionCachePrefix + "WorkQueueViewUnmodified"] = workQueueView.Copy ();

                    SaveResponseLabel.Text = "Save Successful.";

                    InitializeAll ();

                }

                else {

                    SaveResponseLabel.Text = "Unable to Save.";

                    if (MercuryApplication.LastException != null) { SaveResponseLabel.Text = SaveResponseLabel.Text + " [" + MercuryApplication.LastException.Message + "]"; }

                    success = false;

                }

            }

            else if (!isModified) { SaveResponseLabel.Text = "No Changes Detected."; success = true; }

            else if (!isValid) {

                foreach (String validationKey in validationResponse.Keys) {

                    SaveResponseLabel.Text = "Invalid [" + validationKey + "]: " + validationResponse[validationKey];

                    break;

                }

                success = false;

            }

            return success;

        }

        protected void ButtonOk_OnClick (Object sender, EventArgs eventArgs) {

            if (ApplyChanges ()) {

                Server.Transfer ("/WindowClose.aspx");

            }

            return;

        }

        protected void ButtonApply_OnClick (Object sender, EventArgs eventArgs) {

            Boolean success = ApplyChanges ();

            return;

        }

        protected void ButtonCancel_OnClick (Object sender, EventArgs eventArgs) {

            Server.Transfer ("/WindowClose.aspx");

            return;

        }

        #endregion

    }

}
