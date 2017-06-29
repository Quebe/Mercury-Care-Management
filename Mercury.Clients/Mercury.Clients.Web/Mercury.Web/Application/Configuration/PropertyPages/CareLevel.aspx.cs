using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Configuration.PropertyPages {

    public partial class CareLevel : System.Web.UI.Page {

        #region Private Properties

        private Boolean isPageUnloading = false;

        private Client.Core.Individual.CareLevel careLevel;

        #endregion


        #region Session Properties

        public String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (PageInstanceId.Text)) { PageInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return Form.Name + PageInstanceId.Text + ".";

            }

        }

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application)Session["Mercury.Application"];

                if ((application == null) && (!isPageUnloading)) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        private Mercury.Client.Core.Individual.CareLevelActivity EditCareLevelActivity { 

            get {

                Mercury.Client.Core.Individual.CareLevelActivity activity = (Mercury.Client.Core.Individual.CareLevelActivity)Session[SessionCachePrefix + "EditCareLevelActivity"];

                if (activity == null) {

                    activity = new Client.Core.Individual.CareLevelActivity (MercuryApplication);

                    Session[SessionCachePrefix + "EditCareLevelActivity"] = activity;

                }

                return activity;

            }

            set { Session[SessionCachePrefix + "EditCareLevelActivity"] = value; }

        }

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            Int64 forCareLevelId = 0;


            if (MercuryApplication == null) { return; }

            if ((!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareLevelReview))

                && (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareLevelManage))) { Response.Redirect ("/PermissionDenied.aspx", true); return; }


            if (!Page.IsPostBack) {

                #region Initial Page Load

                if (Request.QueryString["CareLevelId"] != null) {

                    forCareLevelId = Int64.Parse (Request.QueryString["CareLevelId"]);

                }

                if (forCareLevelId != 0) {

                    careLevel = MercuryApplication.CareLevelGet (forCareLevelId, false);

                    if (careLevel == null) {

                        careLevel = new Mercury.Client.Core.Individual.CareLevel (MercuryApplication);

                    }

                }

                else {

                    careLevel = new Mercury.Client.Core.Individual.CareLevel (MercuryApplication);

                }

                InitializeAll ();

                Session[SessionCachePrefix + "CareLevel"] = careLevel;

                Session[SessionCachePrefix + "CareLevelUnmodified"] = careLevel.Copy ();

                #endregion

            } // Initial Page Load

            else { // Postback

                careLevel = (Mercury.Client.Core.Individual.CareLevel)Session[SessionCachePrefix + "CareLevel"];

            }

            ApplySecurity ();

            if (!String.IsNullOrEmpty (careLevel.Name)) { Page.Title = "Care Level - " + careLevel.Name; } else { Page.Title = "Care Level"; }

            return;

        }

        protected void Page_Unload (object sender, EventArgs e) {

            isPageUnloading = true;

            if (MercuryApplication != null) { MercuryApplication.ApplicationClientClose (); }

            return;

        }

        #endregion


        #region Initialization

        protected void InitializeAll () {

            InitializeGeneralPage ();

            InitializeActivitiesPage ();

            return;

        }


        protected void InitializeGeneralPage () {

            CareLevelName.Text = careLevel.Name;

            CareLevelDescription.Text = careLevel.Description;


            CareLevelEnabled.Checked = careLevel.Enabled;

            CareLevelVisible.Checked = careLevel.Visible;


            CareLevelCreateAuthorityName.Text = careLevel.CreateAccountInfo.SecurityAuthorityName;

            CareLevelCreateAccountId.Text = careLevel.CreateAccountInfo.UserAccountId;

            CareLevelCreateAccountName.Text = careLevel.CreateAccountInfo.UserAccountName;

            CareLevelCreateDate.MinDate = DateTime.MinValue;

            CareLevelCreateDate.SelectedDate = careLevel.CreateAccountInfo.ActionDate;


            CareLevelModifiedAuthorityName.Text = careLevel.ModifiedAccountInfo.SecurityAuthorityName;

            CareLevelModifiedAccountId.Text = careLevel.ModifiedAccountInfo.UserAccountId;

            CareLevelModifiedAccountName.Text = careLevel.ModifiedAccountInfo.UserAccountName;

            CareLevelModifiedDate.MinDate = DateTime.MinValue;

            CareLevelModifiedDate.SelectedDate = careLevel.ModifiedAccountInfo.ActionDate;

            return;

        }

        protected void InitializeActivitiesPage () {

            InitializeActivitiesGrid ();

            InitializeActivityParametersGrid ();

            InitializeActivityThresholds ();

            return;

        }

        protected void InitializeActivitiesGrid () {

            ActivitiesGrid.DataSource = careLevel.Activities;

            ActivitiesGrid.DataBind ();

            return;

        }

        protected void InitializeActivitySelectionForAutomation () {

            ActivityActionSelection.Items.Clear ();

            ActivityActionSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* No Action Selected", "0"));

            foreach (Mercury.Server.Application.Action currentAction in MercuryApplication.ActionsAvailable (false)) {

                ActivityActionSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentAction.Name, currentAction.Id.ToString ()));

            }

            ActivityActionSelection.Enabled = true;

            InitializeActivityParametersGrid ();

            return;

        }

        protected void InitializeActivitySelectionForWorkflow () {

            ActivityActionSelection.Items.Clear ();

            ActivityActionSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* No Workflow Selected", "0"));

            foreach (Mercury.Client.Core.Work.Workflow currentWorkflow in MercuryApplication.WorkflowsAvailable (false)) {

                if ((currentWorkflow.Enabled) && (currentWorkflow.Visible)) {

                    ActivityActionSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentWorkflow.Name, currentWorkflow.Id.ToString ()));

                }

            }

            ActivityActionSelection.Enabled = true;

            InitializeActivityParametersGrid ();

            return;

        }

        protected void InitializeActivityParametersGrid () {

            System.Data.DataTable parameterTable = new System.Data.DataTable ();

            parameterTable.Columns.Add ("ParameterName");

            parameterTable.Columns.Add ("ParameterValue");

            if (EditCareLevelActivity != null) {

                if (EditCareLevelActivity.Action != null) {

                    foreach (String parameterName in EditCareLevelActivity.Action.ActionParameters.Keys) {

                        parameterTable.Rows.Add (parameterName, EditCareLevelActivity.Action.ActionParameters[parameterName].ValueDescription);

                    }

                }

            }


            Session[SessionCachePrefix + "ActivityParametersGrid.ParameterTable"] = parameterTable;

            
            ActivityParametersGrid.DataSource = null;

            ActivityParametersGrid.Rebind ();

            ActivityParametersGrid.DataSource = parameterTable;

            return;

        }

        protected void InitializeActivityThresholds () {

            System.Data.DataTable thresholdTable = new System.Data.DataTable ();

            thresholdTable.Columns.Add ("ThresholdId");

            thresholdTable.Columns.Add ("RelativeValue");

            thresholdTable.Columns.Add ("RelativeQualifier");

            thresholdTable.Columns.Add ("Status");

            thresholdTable.Columns.Add ("Action");


            Session[SessionCachePrefix + "ActivityThresholdsGrid.ThresholdTable"] = thresholdTable;

            ActivityThresholdsGrid.DataSource = thresholdTable;

            ActivityThresholdsGrid.DataBind ();


            System.Data.DataTable parameterTable = (System.Data.DataTable)Session[SessionCachePrefix + "ActivityThresholdsGrid.ParameterTable"];

            parameterTable = new System.Data.DataTable ();

            parameterTable.Columns.Add ("ThresholdKey");

            parameterTable.Columns.Add ("ThresholdId");

            parameterTable.Columns.Add ("ParameterName");

            parameterTable.Columns.Add ("ParameterValue");


            Session[SessionCachePrefix + "ActivityThresholdsGrid.ParameterTable"] = parameterTable;

            ActivityThresholdsGrid.MasterTableView.DetailTables[0].DataSource = parameterTable;

            ActivityThresholdsGrid.MasterTableView.DetailTables[0].DataBind ();
            
            return;

        }

        protected void ApplySecurity () {

            Boolean hasManagePermission = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareLevelManage);

            CareLevelName.ReadOnly = !hasManagePermission;

            CareLevelDescription.ReadOnly = !hasManagePermission;


            CareLevelEnabled.Enabled = hasManagePermission;

            CareLevelVisible.Enabled = hasManagePermission;


            ButtonCancel.Visible = hasManagePermission;

            ButtonApply.Visible = hasManagePermission;

            return;

        }

        #endregion


        #region Activities Grid Events

        protected void ButtonAddUpdateActivity_OnClick (Object sender, EventArgs eventArgs) {
            
            Boolean existingActivityFound = false;

            Client.Core.Individual.CareLevelActivity newActivity = null;

            Dictionary<String, String> validationResponse;

            SaveResponseLabel.Text = String.Empty;


            if (MercuryApplication == null) { return; }


            newActivity = new Client.Core.Individual.CareLevelActivity (MercuryApplication);

            newActivity.CareLevelId = careLevel.Id;


            newActivity.ActivityType = (Mercury.Server.Application.ActivityType)Convert.ToInt32 (ActivityTypeSelection.SelectedValue);


            switch (newActivity.ActivityType) {

                case Mercury.Server.Application.ActivityType.Manual:

                    newActivity.Name = ActivityName.Text;

                    newActivity.Description = ActivityDescription.Text;

                    break;

                case Mercury.Server.Application.ActivityType.Automated:

                case Mercury.Server.Application.ActivityType.Workflow:

                    newActivity.Action = EditCareLevelActivity.Action.Copy ();

                    break;

            }


            // RESERVED: INITIAL ANCHOR DATE, ANCHOR DATE SETTINGS

            newActivity.ScheduleType = (Mercury.Server.Application.ActivityScheduleType) Convert.ToInt32 (ActivityScheduleTypeSelection.SelectedValue);

            newActivity.ScheduleValue = Convert.ToInt32 (ActivityScheduleValue.Value);

            newActivity.ScheduleQualifier = (Mercury.Server.Application.DateQualifier) Convert.ToInt32 (ActivityScheduleQualifierSelection.SelectedValue);

            newActivity.ConstraintValue = Convert.ToInt32 (ActivityConstraintValue.Value);

            newActivity.ConstraintQualifier = (Mercury.Server.Application.DateQualifier)Convert.ToInt32 (ActivityConstraintQualifierSelection.SelectedValue);

            newActivity.Reoccurring = ActivityReoccurring.Checked;

            // RESERVED: PERFORM ACTION DATE

            
            foreach (Client.Core.Activity.ActivityThreshold currentThreshold in EditCareLevelActivity.SortedThresholds.Values) {

                newActivity.Thresholds.Add (currentThreshold);

            }


            validationResponse = MercuryApplication.CoreObject_Validate ((Mercury.Server.Application.CoreObject)newActivity.ToServerObject ());

            if (validationResponse.Count == 0) {

                existingActivityFound = false;

                foreach (Client.Core.Individual.CareLevelActivity currentActivity in careLevel.Activities) {

                    existingActivityFound = (newActivity.IsEqual (currentActivity));

                    if (existingActivityFound) { break; }
                    
                }


                switch (((System.Web.UI.WebControls.Button)sender).ID) {

                    case "ButtonAddActivity":

                        if (!existingActivityFound) {

                            careLevel.Activities.Add (newActivity);

                            SaveResponseLabel.Text = String.Empty;

                        }

                        else { SaveResponseLabel.Text = "Duplicate Activity."; }

                        break;


                    case "ButtonUpdateActivity":

                        if (ActivitiesGrid.SelectedItems.Count != 0) {

                            newActivity.CoreObjectId = careLevel.Activities[ActivitiesGrid.SelectedItems[0].DataSetIndex].Id;

                            careLevel.Activities.RemoveAt (ActivitiesGrid.SelectedItems[0].DataSetIndex);

                            careLevel.Activities.Add (newActivity);

                        }

                        else { SaveResponseLabel.Text = "No Activity Selected."; }

                        break;

                }

            }

            else {

                foreach (String validationKey in validationResponse.Keys) {

                    SaveResponseLabel.Text = "Invalid [" + validationKey + "]: " + validationResponse[validationKey];

                    break;

                }

            }

            InitializeActivitiesGrid ();

            return;

        }

        protected void ActivitiesGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            Int32 deleteIndex = eventArgs.Item.DataSetIndex;

            careLevel.Activities.RemoveAt (deleteIndex);

            InitializeActivitiesGrid ();


            // RESET TYPE SELECTION

            Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs selectedIndexChangedEventArgs;

            selectedIndexChangedEventArgs = new Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs (ActivityTypeSelection.Text, String.Empty, ActivityTypeSelection.SelectedValue, String.Empty);

            ActivityTypeSelection_OnSelectedIndexChanged (ActivityTypeSelection, selectedIndexChangedEventArgs);

            return;

        }

        protected void ActivitiesGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs selectedIndexChangedEventArgs;

            switch (eventArgs.CommandName) {

                case Telerik.Web.UI.RadGrid.EditCommandName:

                    // SET ACTIVITY SELECTION FIRST AS IT WILL RESET THE ACTION ON CHANGE
                    
                    ActivityTypeSelection.SelectedValue = ((Int32)careLevel.Activities[eventArgs.Item.ItemIndex].ActivityType).ToString ();

                    selectedIndexChangedEventArgs = new Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs (ActivityTypeSelection.Text, String.Empty, ActivityTypeSelection.SelectedValue, String.Empty);

                    ActivityTypeSelection_OnSelectedIndexChanged (ActivityTypeSelection, selectedIndexChangedEventArgs);


                    // MAKE COPY OF SELECTED ROW FOR EDITING
                    
                    EditCareLevelActivity = careLevel.Activities[eventArgs.Item.ItemIndex].Copy ();

                    
                    if (EditCareLevelActivity.Action != null) {

                        if (EditCareLevelActivity.ActivityType == Mercury.Server.Application.ActivityType.Automated) {

                            ActivityActionSelection.SelectedValue = EditCareLevelActivity.Action.Id.ToString ();

                        }

                        else if (EditCareLevelActivity.ActivityType == Mercury.Server.Application.ActivityType.Workflow) {

                            ActivityActionSelection.SelectedValue = EditCareLevelActivity.Action.ActionParameters["Workflow"].Value;

                        }

                    }




                    ActivityName.Text = EditCareLevelActivity.Name;

                    ActivityDescription.Text = EditCareLevelActivity.Description;


                    ActivityScheduleValue.Value = EditCareLevelActivity.ScheduleValue;

                    ActivityScheduleQualifierSelection.SelectedValue = ((Int32)EditCareLevelActivity.ScheduleQualifier).ToString ();

                    ActivityConstraintValue.Value = EditCareLevelActivity.ConstraintValue;

                    ActivityConstraintQualifierSelection.SelectedValue = ((Int32)EditCareLevelActivity.ConstraintQualifier).ToString ();


                    ActivityReoccurring.Checked = EditCareLevelActivity.Reoccurring;

                    InitializeActivityParametersGrid (); // REBIND PARAMETERS FOR ACTION 

                    ActivityThresholdsGrid.Rebind (); // REBIND THRESHOLDS

                    eventArgs.Canceled = true;

                    break;

                default:

                    System.Diagnostics.Debug.WriteLine (eventArgs.CommandName);

                    break;

            }

            InitializeActivitiesGrid ();

            ActivitiesGrid.SelectedIndexes.Add (eventArgs.Item.ItemIndex);

            return;

        }


        protected void ActivityThresholdsGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {
            
            if (MercuryApplication == null) { return; }

            Client.Core.Activity.ActivityThreshold threshold;

            switch (eventArgs.RebindReason) {

                case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                    System.Data.DataTable thresholdTable = (System.Data.DataTable)Session[SessionCachePrefix + "ActivityThresholdsGrid.ThresholdTable"];

                    thresholdTable.Rows.Clear ();


                    Int32 currentThresholdIndex = 0;

                    foreach (Int64 thresholdKey in EditCareLevelActivity.SortedThresholds.Keys) {

                        Client.Core.Activity.ActivityThreshold currentThreshold = EditCareLevelActivity.SortedThresholds[thresholdKey];

                        String actionName = String.Empty;

                        thresholdTable.Rows.Add (

                            currentThresholdIndex,

                            currentThreshold.RelativeDateValue,

                            currentThreshold.RelativeDateQualifier.ToString (),

                            Mercury.Server.CommonFunctions.EnumerationToString (currentThreshold.Status),

                            ((currentThreshold.Action != null) ? currentThreshold.Action.Description : String.Empty)

                        );

                        currentThresholdIndex = currentThresholdIndex + 1;

                    }

                    ActivityThresholdsGrid.DataSource = thresholdTable;

                    break;

                default:

                    if ((eventArgs.RebindReason & Telerik.Web.UI.GridRebindReason.DetailTableBinding) == Telerik.Web.UI.GridRebindReason.DetailTableBinding) {

                        System.Data.DataTable parameterTable = (System.Data.DataTable)Session[SessionCachePrefix + "ActivityThresholdsGrid.ParameterTable"];

                        parameterTable.Rows.Clear ();

                        currentThresholdIndex = 0;

                        foreach (Int64 thresholdKey in EditCareLevelActivity.SortedThresholds.Keys) {

                            threshold = EditCareLevelActivity.SortedThresholds[thresholdKey];

                            if (threshold.Action != null) {

                                foreach (String parameterName in threshold.Action.ActionParameters.Keys) {

                                    parameterTable.Rows.Add (thresholdKey, currentThresholdIndex, parameterName, threshold.Action.ActionParameters[parameterName].ValueDescription);

                                }

                            }

                            currentThresholdIndex = currentThresholdIndex + 1;

                        }

                        ActivityThresholdsGrid.MasterTableView.DetailTables[0].DataSource = parameterTable;

                    }

                    break;

            }

            return;


        }

        protected void ActivityThresholdsGrid_OnItemDataBound (Object sender, Telerik.Web.UI.GridItemEventArgs eventArgs) {

            Telerik.Web.UI.RadNumericTextBox thresholdRelativeDateValue;

            Telerik.Web.UI.RadComboBox thresholdRelativeDateQualifier;

            Telerik.Web.UI.RadComboBox thresholdStatusSelection;

            Telerik.Web.UI.RadComboBox thresholdActionSelection;

            Client.Core.Activity.ActivityThreshold threshold;

            System.Collections.Generic.Dictionary<String, String> bindingContexts;


            if (MercuryApplication == null) { return; }


            if ((eventArgs.Item is Telerik.Web.UI.GridEditableItem) && (eventArgs.Item.IsInEditMode)) {

                Telerik.Web.UI.GridEditableItem editItem = (Telerik.Web.UI.GridEditableItem)eventArgs.Item;


                switch (eventArgs.Item.OwnerTableView.Name) {

                    case "Thresholds":

                        thresholdRelativeDateValue = (Telerik.Web.UI.RadNumericTextBox)editItem.FindControl ("ActivityThresholdRelativeDateValue");

                        thresholdRelativeDateQualifier = (Telerik.Web.UI.RadComboBox)editItem.FindControl ("ActivityThresholdRelativeDateQualifier");

                        thresholdStatusSelection = (Telerik.Web.UI.RadComboBox)editItem.FindControl ("ActivityThresholdStatusSelection");


                        // RESERVED IF ACTIONS ARE ADDED TO CARE LEVEL ACTIVITY THRESHOLDS

                        //thresholdActionSelection = (Telerik.Web.UI.RadComboBox)editItem.FindControl ("ActivityThresholdActionSelection");


                        //thresholdActionSelection.Items.Clear ();

                        //thresholdActionSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* No Action Selected", "0"));

                        //foreach (Mercury.Server.Application.Action currentAction in MercuryApplication.ActionsAvailable (false)) {

                        //    thresholdActionSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentAction.Name, currentAction.Id.ToString ()));

                        //}


                        if (eventArgs.Item.ItemIndex != -1) {

                            threshold = EditCareLevelActivity.SortedThresholds.Values[eventArgs.Item.ItemIndex];

                            if (threshold != null) {

                                thresholdRelativeDateValue.Value = threshold.RelativeDateValue;

                                thresholdRelativeDateQualifier.SelectedValue = threshold.RelativeDateQualifier.ToString ();

                                thresholdStatusSelection.SelectedValue = ((Int32)threshold.Status).ToString ();


                                // RESERVED IF ACTIONS ARE ADDED TO CARE LEVEL ACTIVITY THRESHOLDS

                                // thresholdActionSelection.SelectedValue = threshold.Action.Id.ToString ();

                            }

                        }

                        break;

                    case "ThresholdParameters": // RESERVED IF ACTIONS ARE ADDED TO CARE LEVEL ACTIVITY THRESHOLDS

                        Int64 thresholdKey = Int64.Parse ((String)((Telerik.Web.UI.GridEditableItem)eventArgs.Item).OwnerTableView.DataKeyValues[eventArgs.Item.ItemIndex]["ThresholdKey"]);

                        threshold = EditCareLevelActivity.SortedThresholds[thresholdKey];

                        String parameterName = (String)editItem.OwnerTableView.DataKeyValues[eventArgs.Item.ItemIndex]["ParameterName"];

                        Telerik.Web.UI.RadComboBox parameterValueSelection = (Telerik.Web.UI.RadComboBox)eventArgs.Item.FindControl ("ActivityThresholdParameterValue");

                        Telerik.Web.UI.RadTextBox parameterFixedValue = (Telerik.Web.UI.RadTextBox)eventArgs.Item.FindControl ("ActivityThresholdParameterFixedValue");


                        parameterValueSelection.Items.Clear ();

                        if (!threshold.Action.ActionParameters[parameterName].Required) {

                            parameterValueSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* Not Assigned", "0"));

                        }


                        
                        bindingContexts = (new Client.Core.Individual.Case.MemberCase (MercuryApplication)).ParameterValueSelection (threshold.Action.ActionParameters[parameterName].DataType);

                        foreach (String currentBindingContextName in bindingContexts.Keys) {

                            parameterValueSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentBindingContextName, bindingContexts[currentBindingContextName]));

                        }

                        parameterFixedValue.Enabled = threshold.Action.ActionParameters[parameterName].AllowFixedValue;

                        parameterFixedValue.EmptyMessage = (threshold.Action.ActionParameters[parameterName].AllowFixedValue) ? String.Empty : "(Not Available)";

                        parameterFixedValue.Text = String.Empty;

                        break;

                }

            }

            return;

        }

        protected void ActivityThresholdsGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            Client.Core.Individual.CareLevelActivity careLevelActivity = EditCareLevelActivity;

            Mercury.Client.Core.Activity.ActivityThreshold threshold;

            String parameterName;

            Telerik.Web.UI.RadNumericTextBox thresholdRelativeDateValue;

            Telerik.Web.UI.RadComboBox thresholdRelativeDateQualifier;

            Telerik.Web.UI.RadComboBox thresholdStatusSelection;

            Telerik.Web.UI.RadComboBox thresholdActionSelection;

            Telerik.Web.UI.RadComboBox parameterValueSelection;

            Telerik.Web.UI.RadTextBox parameterFixedValue;

            Int64 actionId = 0;


            if (MercuryApplication == null) { return; }


            SaveResponseLabel.Text = String.Empty;

            switch (eventArgs.CommandName) {

                case Telerik.Web.UI.RadGrid.EditCommandName:

                    #region Edit Command

                    switch (eventArgs.Item.OwnerTableView.Name) {

                        case "Thresholds":

                            Telerik.Web.UI.GridEditCommandColumn editColumn = (Telerik.Web.UI.GridEditCommandColumn)ActivityThresholdsGrid.MasterTableView.GetColumn ("EditCommandColumn");

                            if (!editColumn.Visible) { editColumn.Visible = true; }

                            Telerik.Web.UI.GridEditableItem editItem = (Telerik.Web.UI.GridEditableItem)eventArgs.Item;

                            break;


                        case "ThresholdParameters":

                            editColumn = (Telerik.Web.UI.GridEditCommandColumn)eventArgs.Item.OwnerTableView.GetColumn ("EditCommandColumn");

                            if (!editColumn.Visible) { editColumn.Visible = true; }

                            break;

                    }

                    #endregion

                    break;

                case Telerik.Web.UI.RadGrid.InitInsertCommandName:

                    break;

                case Telerik.Web.UI.RadGrid.PerformInsertCommandName:

                    #region Perform Insert Command

                    Telerik.Web.UI.GridEditableItem insertedItem = (Telerik.Web.UI.GridEditableItem)eventArgs.Item;

                    thresholdRelativeDateValue = (Telerik.Web.UI.RadNumericTextBox)insertedItem.FindControl ("ActivityThresholdRelativeDateValue");

                    thresholdRelativeDateQualifier = (Telerik.Web.UI.RadComboBox)insertedItem.FindControl ("ActivityThresholdRelativeDateQualifier");

                    thresholdStatusSelection = (Telerik.Web.UI.RadComboBox)insertedItem.FindControl ("ActivityThresholdStatusSelection");

                    thresholdActionSelection = (Telerik.Web.UI.RadComboBox)insertedItem.FindControl ("ActivityThresholdActionSelection");


                    try {

                        if (thresholdRelativeDateValue.Value == null) { throw new ApplicationException ("No Threshold Date Specified."); }


                        threshold = careLevelActivity.GetNewThreshold ();

                        threshold.RelativeDateValue = Int32.Parse (thresholdRelativeDateValue.Value.ToString ());

                        threshold.RelativeDateQualifier = (Mercury.Server.Application.DateQualifier)Int32.Parse (thresholdRelativeDateQualifier.SelectedItem.Value);

                        threshold.Status = (Mercury.Server.Application.ActivityStatus)Int32.Parse (thresholdStatusSelection.SelectedValue);


                        // RESERVED IF ACTIONS ARE ADDED TO CARE LEVEL ACTIVITY THRESHOLDS

                        //actionId = Int64.Parse (thresholdActionSelection.SelectedValue);

                        //threshold.Action = MercuryApplication.ActionById (actionId);

                        //if (threshold.Action == null) { threshold.Action = new Mercury.Client.Core.Action.Action (MercuryApplication); }


                        if (!EditCareLevelActivity.HasThreshold (threshold)) {

                            careLevelActivity.Thresholds.Add (threshold);

                            EditCareLevelActivity = careLevelActivity;

                        }

                        else { throw new ApplicationException ("Duplicate Threshold"); }

                    }

                    catch (Exception MercuryApplicationException) {

                        SaveResponseLabel.Text = "Error: " + MercuryApplicationException.Message;

                    }

                    #endregion

                    break;

                case Telerik.Web.UI.RadGrid.UpdateCommandName:

                    #region Update Command

                    Telerik.Web.UI.GridEditableItem updatedItem = (Telerik.Web.UI.GridEditableItem)eventArgs.Item;

                    switch (eventArgs.Item.OwnerTableView.Name) {

                        case "Thresholds":

                            thresholdRelativeDateValue = (Telerik.Web.UI.RadNumericTextBox)updatedItem.FindControl ("ActivityThresholdRelativeDateValue");

                            thresholdRelativeDateQualifier = (Telerik.Web.UI.RadComboBox)updatedItem.FindControl ("ActivityThresholdRelativeDateQualifier");

                            thresholdStatusSelection = (Telerik.Web.UI.RadComboBox)updatedItem.FindControl ("ActivityThresholdStatusSelection");

                            thresholdActionSelection = (Telerik.Web.UI.RadComboBox)updatedItem.FindControl ("ActivityThresholdActionSelection");

                            threshold = careLevelActivity.SortedThresholds.Values[updatedItem.ItemIndex];

                            threshold.RelativeDateValue = Int32.Parse (thresholdRelativeDateValue.Value.ToString ());

                            threshold.RelativeDateQualifier = (Mercury.Server.Application.DateQualifier)Int32.Parse (thresholdRelativeDateQualifier.SelectedItem.Value);

                            threshold.Status = (Mercury.Server.Application.ActivityStatus)Int32.Parse (thresholdStatusSelection.SelectedValue);


                            // RESERVED IF ACTIONS ARE ADDED TO CARE LEVEL ACTIVITY THRESHOLDS

                            //actionId = Int64.Parse (thresholdActionSelection.SelectedValue);
                            
                            //if (threshold.Action.Id != actionId) {

                            //    threshold.Action = MercuryApplication.ActionById (actionId);

                            //    if (threshold.Action == null) { threshold.Action = new Mercury.Client.Core.Action.Action (MercuryApplication); }

                            //}

                            break;

                        case "ThresholdParameters": // RESERVED IF ACTIONS ARE ADDED TO CARE LEVEL ACTIVITY THRESHOLDS

                            Int64 thresholdKey = Int64.Parse ((String)((Telerik.Web.UI.GridEditableItem)eventArgs.Item).OwnerTableView.DataKeyValues[eventArgs.Item.ItemIndex]["ThresholdKey"]);

                            threshold = careLevelActivity.SortedThresholds[thresholdKey];

                            parameterName = (String)((Telerik.Web.UI.GridEditableItem)eventArgs.Item).OwnerTableView.DataKeyValues[eventArgs.Item.ItemIndex]["ParameterName"];

                            if (threshold.Action.ActionParameters.ContainsKey (parameterName)) {

                                parameterValueSelection = (Telerik.Web.UI.RadComboBox)eventArgs.Item.FindControl ("ActivityThresholdParameterValue");

                                parameterFixedValue = (Telerik.Web.UI.RadTextBox)eventArgs.Item.FindControl ("ActivityThresholdParameterFixedValue");

                                if ((parameterValueSelection.SelectedItem != null) || (!String.IsNullOrEmpty (parameterFixedValue.Text))) {


                                    if ((threshold.Action.ActionParameters[parameterName].AllowFixedValue) && (!String.IsNullOrEmpty (parameterFixedValue.Text))) {

                                        threshold.Action.ActionParameters[parameterName].ValueType = Mercury.Server.Application.ActionParameterValueType.FixedValue;

                                        threshold.Action.ActionParameters[parameterName].Value = parameterFixedValue.Text;

                                        threshold.Action.ActionParameters[parameterName].ValueDescription = parameterFixedValue.Text;

                                    }

                                    else {

                                        threshold.Action.ActionParameters[parameterName].ValueType = Mercury.Server.Application.ActionParameterValueType.DataMapping;

                                        threshold.Action.ActionParameters[parameterName].Value = parameterValueSelection.SelectedItem.Value;

                                        threshold.Action.ActionParameters[parameterName].ValueDescription = parameterValueSelection.SelectedItem.Text;

                                    }


                                    if ((threshold.Action.Name == "Workflow") && (parameterName == "Workflow")) {

                                        threshold.Action.RebindActionParameters (MercuryApplication);

                                    }

                                }

                            }

                            break;

                    }

                    #endregion

                    break;

                case Telerik.Web.UI.RadGrid.ExpandCollapseCommandName:

                    #region Expand/Collapse Command

                    System.Data.DataTable parameterTable = (System.Data.DataTable)Session[SessionCachePrefix + "ActivityThresholdsGrid.ParameterTable"];

                    parameterTable.Rows.Clear ();

                    Int32 currentThresholdIndex = 0;

                    foreach (Mercury.Client.Core.Activity.ActivityThreshold currentThreshold in careLevelActivity.SortedThresholds.Values) {

                        if (currentThreshold.Action != null) {

                            foreach (String currentParameterName in currentThreshold.Action.ActionParameters.Keys) {

                                parameterTable.Rows.Add (currentThresholdIndex, currentParameterName, currentThreshold.Action.ActionParameters[currentParameterName].ValueDescription);

                            }

                        }

                        currentThresholdIndex = currentThresholdIndex + 1;

                    }

                    ActivityThresholdsGrid.MasterTableView.DetailTables[0].DataSource = parameterTable;

                    ActivityThresholdsGrid.MasterTableView.DetailTables[0].DataBind ();

                    #endregion

                    break;

                case Telerik.Web.UI.RadGrid.DeleteCommandName:

                    Int32 deleteIndex = eventArgs.Item.DataSetIndex;

                    if ((deleteIndex > -1) && (deleteIndex < careLevelActivity.Thresholds.Count)) {

                        threshold = careLevelActivity.SortedThresholds.Values[deleteIndex];

                        careLevelActivity.Thresholds.Remove (threshold);

                    }

                    break;

                default:

                    System.Diagnostics.Debug.WriteLine (eventArgs.CommandName);

                    break;

            }


            EditCareLevelActivity = careLevelActivity;

            return;

        }

        #endregion


        #region Activity Property Events

        protected void ActivityTypeSelection_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }


            // CLEAR ASSIGNED ACTION ON CHANGE (FOR ALL CHANGES)

            Client.Core.Individual.CareLevelActivity careLevelActivity = EditCareLevelActivity;

            careLevelActivity.Action = null;

            EditCareLevelActivity = careLevelActivity;



            switch (eventArgs.Value) {

                case "0": // MANUAL

                    ActivityActionSelection.Items.Clear ();

                    ActivityActionSelection.Enabled = false;


                    ActivityTypeManualNameDescription.Style.Clear ();

                    ActivityParameters.Style.Add ("display", "none");


                    break;

                case "1": // AUTOMATION

                    InitializeActivitySelectionForAutomation ();


                    ActivityTypeManualNameDescription.Style.Add ("display", "none");

                    ActivityParameters.Style.Clear ();

                    break;


                case "2": // WORKFLOW

                    InitializeActivitySelectionForWorkflow ();


                    ActivityTypeManualNameDescription.Style.Add ("display", "none");

                    ActivityParameters.Style.Clear ();

                    break;

                case "3": // MONITOR
                    
                    ActivityActionSelection.Items.Clear ();

                    ActivityActionSelection.Enabled = false;


                    ActivityTypeManualNameDescription.Style.Add ("display", "none");

                    ActivityParameters.Style.Add ("display", "none");


                    break;

            }


            return;

        }

        protected void ActivityActionSelection_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }


            Client.Core.Individual.CareLevelActivity careLevelActivity = EditCareLevelActivity;

            if (ActivityActionSelection.SelectedItem != null) {

                switch (ActivityTypeSelection.SelectedValue) {

                    case "1": // AUTOMATION

                        Int64 actionId = Convert.ToInt64 (ActivityActionSelection.SelectedValue);

                        careLevelActivity.Action = MercuryApplication.ActionById (actionId);     
                   

                        
                        break;

                    case "2": // WORKFLOW

                        careLevelActivity.Action = MercuryApplication.ActionById (1); // WORKFLOW

                        careLevelActivity.Action.ActionParameters["Workflow"].Value = ActivityActionSelection.SelectedValue;

                        careLevelActivity.Action.ActionParameters["Workflow"].ValueDescription = ActivityActionSelection.SelectedItem.Text;

                        careLevelActivity.Action.RebindActionParameters (MercuryApplication);

                        break;

                }

            }

            EditCareLevelActivity = careLevelActivity;

            InitializeActivityParametersGrid ();

            return;

        }

        #endregion 


        #region Activity Parameters Grid Events
        
        protected void ActivityParametersGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            switch (eventArgs.RebindReason) {

                case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                    System.Data.DataTable parameterTable = (System.Data.DataTable)Session[SessionCachePrefix + "ActivityParametersGrid.ParameterTable"];

                    ActivityParametersGrid.DataSource = parameterTable;

                    break;

                default:

                    System.Diagnostics.Debug.WriteLine ("ActivityParametersGrid_OnNeedDataSource: " + eventArgs.RebindReason.ToString ());

                    break;

            }

            return;

        }

        protected void ActivityParametersGrid_OnItemDataBound (Object sender, Telerik.Web.UI.GridItemEventArgs eventArgs) {

            Telerik.Web.UI.RadComboBox activityParameterValueSelection;

            Telerik.Web.UI.RadTextBox activityParameterFixedValue;

            System.Collections.Generic.Dictionary<String, String> bindingContexts = new Dictionary<String, String> ();

            String parameterName;


            if (MercuryApplication == null) { return; }


            if ((eventArgs.Item is Telerik.Web.UI.GridEditableItem) && (eventArgs.Item.IsInEditMode)) {

                Telerik.Web.UI.GridEditableItem editItem = (Telerik.Web.UI.GridEditableItem)eventArgs.Item;


                if ((ActivityActionSelection.SelectedItem != null) && (ActivityActionSelection.SelectedValue != "0")) {

                    parameterName = (String)editItem.OwnerTableView.DataKeyValues[eventArgs.Item.ItemIndex]["ParameterName"];

                    activityParameterValueSelection = (Telerik.Web.UI.RadComboBox)eventArgs.Item.FindControl ("ActivityParameterValueSelection");

                    activityParameterValueSelection.Items.Clear ();

                    if (!EditCareLevelActivity.Action.ActionParameters[parameterName].Required) {

                        activityParameterValueSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* Not Assigned", "0"));

                    }


                    bindingContexts = (new Client.Core.Individual.Case.MemberCase (MercuryApplication)).ParameterValueSelection (EditCareLevelActivity.Action.ActionParameters[parameterName].DataType);

                    foreach (String currentBindingContextName in bindingContexts.Keys) {

                        activityParameterValueSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentBindingContextName, bindingContexts[currentBindingContextName]));

                    }


                    activityParameterFixedValue = (Telerik.Web.UI.RadTextBox)eventArgs.Item.FindControl ("ActivityParameterFixedValue");

                    activityParameterFixedValue.Enabled = EditCareLevelActivity.Action.ActionParameters[parameterName].AllowFixedValue;

                    activityParameterFixedValue.EmptyMessage = (EditCareLevelActivity.Action.ActionParameters[parameterName].AllowFixedValue) ? String.Empty : "(Not Available)";

                    activityParameterFixedValue.Text = String.Empty;

                }

            }

            return;

        }

        protected void ActivityParametersGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            String parameterName;

            Telerik.Web.UI.RadComboBox parameterValueSelection;

            Telerik.Web.UI.RadTextBox parameterFixedValue;

            Client.Core.Individual.CareLevelActivity careLevelActivity = EditCareLevelActivity;


            if (MercuryApplication == null) { return; }


            switch (eventArgs.CommandName) {

                case Telerik.Web.UI.RadGrid.EditCommandName:

                    #region Edit Command

                    Telerik.Web.UI.GridEditCommandColumn editColumn = (Telerik.Web.UI.GridEditCommandColumn)ActivityParametersGrid.MasterTableView.GetColumn ("EditCommandColumn");

                    if (!editColumn.Visible) { editColumn.Visible = true; }

                    Telerik.Web.UI.GridEditableItem editItem = (Telerik.Web.UI.GridEditableItem)eventArgs.Item;

                    #endregion

                    break;

                case Telerik.Web.UI.RadGrid.UpdateCommandName:

                    #region Update Command

                    parameterValueSelection = (Telerik.Web.UI.RadComboBox)eventArgs.Item.FindControl ("ActivityParameterValueSelection");

                    parameterFixedValue = (Telerik.Web.UI.RadTextBox)eventArgs.Item.FindControl ("ActivityParameterFixedValue");

                    if ((parameterValueSelection.SelectedItem != null) || (!String.IsNullOrEmpty (parameterFixedValue.Text))) {

                        parameterName = (String)((Telerik.Web.UI.GridEditableItem)eventArgs.Item).OwnerTableView.DataKeyValues[eventArgs.Item.ItemIndex]["ParameterName"];

                        if ((careLevelActivity.Action.ActionParameters[parameterName].AllowFixedValue) && (!String.IsNullOrEmpty (parameterFixedValue.Text))) {

                            careLevelActivity.Action.ActionParameters[parameterName].ValueType = Mercury.Server.Application.ActionParameterValueType.FixedValue;

                            careLevelActivity.Action.ActionParameters[parameterName].Value = parameterFixedValue.Text;

                            careLevelActivity.Action.ActionParameters[parameterName].ValueDescription = parameterFixedValue.Text;

                        }

                        else {

                            careLevelActivity.Action.ActionParameters[parameterName].ValueType = Mercury.Server.Application.ActionParameterValueType.DataMapping;

                            careLevelActivity.Action.ActionParameters[parameterName].Value = parameterValueSelection.SelectedItem.Value;

                            careLevelActivity.Action.ActionParameters[parameterName].ValueDescription = parameterValueSelection.SelectedItem.Text;

                        }

                        if ((careLevelActivity.Action.Name == "Workflow") && (parameterName == "Workflow")) {

                            careLevelActivity.Action.RebindActionParameters (MercuryApplication);

                        }

                    }

                    #endregion

                    break;

            }

            EditCareLevelActivity = careLevelActivity;

            InitializeActivityParametersGrid ();

            return;

        }

        #endregion


        #region Dialog Button Event Handlers

        protected Boolean ApplyChanges () {

            Boolean isModified = false;

            Boolean success = false;

            Boolean isValid = false;

            System.Collections.Generic.Dictionary<String, String> validationResponse;



            Mercury.Client.Core.Individual.CareLevel careLevelUnmodified = (Mercury.Client.Core.Individual.CareLevel)Session[SessionCachePrefix + "CareLevelUnmodified"];

            if (careLevelUnmodified.Id == 0) { isModified = true; }


            careLevel.Name = CareLevelName.Text.Trim ();

            careLevel.Description = CareLevelDescription.Text.Trim ();

            careLevel.Enabled = CareLevelEnabled.Checked;

            careLevel.Visible = CareLevelVisible.Checked;

            if (!isModified) { isModified = !careLevel.IsEqual (careLevelUnmodified); }


            validationResponse = careLevel.Validate ();

            isValid = (validationResponse.Count == 0);


            if ((isModified) && (isValid)) {

                success = MercuryApplication.CareLevelSave (careLevel);

                if (success) {

                    careLevel = MercuryApplication.CareLevelGet (careLevel.Id, false);

                    Session[SessionCachePrefix + "CareLevel"] = careLevel;

                    Session[SessionCachePrefix + "CareLevelUnmodified"] = careLevel.Copy ();

                    SaveResponseLabel.Text = "Save Successful";

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

            Boolean success = false;

            if (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareLevelManage)) {

                success = ApplyChanges ();

            }

            else {

                success = true;

            }


            if (success) {

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
