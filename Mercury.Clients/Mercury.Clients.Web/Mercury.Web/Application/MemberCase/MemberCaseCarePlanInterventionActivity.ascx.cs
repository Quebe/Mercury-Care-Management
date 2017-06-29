﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.MemberCase {

    public partial class MemberCaseCarePlanInterventionActivity : System.Web.UI.UserControl {

        #region Private Properties

        private Boolean isPageUnloading = false;

        #endregion


        #region Public Properties

        public TextBox PageInstanceId { get { return (TextBox)Page.FindControl ("PageInstanceId"); } }

        public String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (PageInstanceId.Text)) { PageInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return PageInstanceId.Text + ".";

            }

        }

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application)Session["Mercury.Application"];

                if ((application == null) && (!isPageUnloading)) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        public MemberCase ParentMemberCasePage { get { return (MemberCase)Page; } }

        public Client.Core.Individual.Case.MemberCaseCareInterventionActivity CareInterventionActivity {

            get { return (Client.Core.Individual.Case.MemberCaseCareInterventionActivity)Session[SessionCachePrefix + this.ClientID + ".MemberCaseCareInterventionActivity"]; }

            set {

                Client.Core.Individual.Case.MemberCaseCareInterventionActivity careInterventionActivity = CareInterventionActivity;

                if (careInterventionActivity != value) {

                    careInterventionActivity = value;

                    Session[SessionCachePrefix + this.ClientID + ".MemberCaseCareInterventionActivity"] = value;

                }

                InitializeCareInterventionActivity (); // ALWAYS CALL THIS INCASE THE IN NEED OF RESET 

            }

        }

        private Mercury.Client.Core.Individual.CareInterventionActivity EditCareInterventionActivity {

            get {

                Mercury.Client.Core.Individual.CareInterventionActivity activity = (Mercury.Client.Core.Individual.CareInterventionActivity)Session[SessionCachePrefix + this.ClientID + "EditCareInterventionActivity"];

                if (activity == null) {

                    activity = new Client.Core.Individual.CareInterventionActivity (MercuryApplication);

                    Session[SessionCachePrefix + this.ClientID + "EditCareInterventionActivity"] = activity;

                }

                return activity;

            }

            set { Session[SessionCachePrefix + this.ClientID + "EditCareInterventionActivity"] = value; }

        }

        private System.Data.DataTable ActivityThresholdTable {

            get {

                System.Data.DataTable table = (System.Data.DataTable)Session[SessionCachePrefix + this.ClientID + "ActivityThresholdsGrid.ThresholdTable"];

                if (table == null) {

                    table = new System.Data.DataTable ();


                    table.Columns.Add ("ThresholdId");

                    table.Columns.Add ("RelativeValue");

                    table.Columns.Add ("RelativeQualifier");

                    table.Columns.Add ("Status");

                    table.Columns.Add ("Action");

                }

                return table;

            }

            set { Session[SessionCachePrefix + this.ClientID + "ActivityThresholdsGrid.ThresholdTable"] = value; }

        }

        #endregion


        #region Initializations

        private void InitializeCareInterventionActivity () {

            if (CareInterventionActivity == null) { return; }

            InitializeActivityParametersGrid ();

            InitializeActivityThresholds ();

            return;

        }

        protected void InitializeActivitiesPageCareInterventionActivityTypeSelection () {

            switch (CareInterventionActivityType.SelectedValue) {

                case "0": // INTERVENTION

                    ActivityTypeSelection.Items[0].Enabled = true;

                    ActivityTypeSelection.Items[1].Enabled = true;

                    ActivityTypeSelection.Items[2].Enabled = true;

                    ActivityTypeSelection.Items[3].Enabled = false;

                    break;

                case "1": // MEMBER TASK

                    ActivityTypeSelection.Items[0].Enabled = true;

                    ActivityTypeSelection.Items[1].Enabled = true;

                    ActivityTypeSelection.Items[2].Enabled = false;

                    ActivityTypeSelection.Items[3].Enabled = false;

                    break;

            }

            // RESET SELECTION 

            ActivityTypeSelection.SelectedIndex = 0;

            InitializeActivitiesPageActivityType ();

            return;

        }

        protected void InitializeActivitiesPageActivityType () {

            // CLEAR ASSIGNED ACTION ON CHANGE (FOR ALL CHANGES)

            Client.Core.Individual.CareInterventionActivity careInterventionActivity = EditCareInterventionActivity;

            careInterventionActivity.Action = null;

            EditCareInterventionActivity = careInterventionActivity;



            switch (ActivityTypeSelection.SelectedValue) {

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

            if (EditCareInterventionActivity != null) {

                if (EditCareInterventionActivity.Action != null) {

                    foreach (String parameterName in EditCareInterventionActivity.Action.ActionParameters.Keys) {

                        parameterTable.Rows.Add (parameterName, EditCareInterventionActivity.Action.ActionParameters[parameterName].ValueDescription);

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

            ActivityThresholdsGrid.DataSource = ActivityThresholdTable;

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

        #endregion 


        #region Activities Grid Events

        protected void ButtonAddUpdateActivity_OnClick (Object sender, EventArgs eventArgs) {

            //Boolean existingActivityFound = false;

            //Client.Core.Individual.CareInterventionActivity newActivity = null;

            //Dictionary<String, String> validationResponse;

            //SaveResponseLabel.Text = String.Empty;


            //if (MercuryApplication == null) { return; }


            //newActivity = new Client.Core.Individual.CareInterventionActivity (MercuryApplication);

            //newActivity.CareInterventionId = careIntervention.Id;

            //newActivity.CareInterventionActivityType = (Mercury.Server.Application.CareInterventionActivityType)Convert.ToInt32 (CareInterventionActivityType.SelectedValue);

            //newActivity.ActivityType = (Mercury.Server.Application.ActivityType)Convert.ToInt32 (ActivityTypeSelection.SelectedValue);


            //switch (newActivity.ActivityType) {

            //    case Mercury.Server.Application.ActivityType.Manual:

            //        newActivity.Name = ActivityName.Text;

            //        newActivity.Description = ActivityDescription.Text;

            //        break;

            //    case Mercury.Server.Application.ActivityType.Automated:

            //    case Mercury.Server.Application.ActivityType.Workflow:

            //        if (EditCareInterventionActivity.Action != null) { newActivity.Action = EditCareInterventionActivity.Action.Copy (); }

            //        break;

            //}


            //// RESERVED: INITIAL ANCHOR DATE, ANCHOR DATE SETTINGS

            //newActivity.ScheduleType = (Mercury.Server.Application.ActivityScheduleType)Convert.ToInt32 (ActivityScheduleTypeSelection.SelectedValue);

            //newActivity.ScheduleValue = Convert.ToInt32 (ActivityScheduleValue.Value);

            //newActivity.ScheduleQualifier = (Mercury.Server.Application.DateQualifier)Convert.ToInt32 (ActivityScheduleQualifierSelection.SelectedValue);

            //newActivity.ConstraintValue = Convert.ToInt32 (ActivityConstraintValue.Value);

            //newActivity.ConstraintQualifier = (Mercury.Server.Application.DateQualifier)Convert.ToInt32 (ActivityConstraintQualifierSelection.SelectedValue);

            //newActivity.Reoccurring = ActivityReoccurring.Checked;

            //// RESERVED: PERFORM ACTION DATE


            //// NARRATIVES

            //newActivity.ClinicalNarrative = ActivityClinicalNarrative.Text;

            //newActivity.CommonNarrative = ActivityCommonNarrative.Text;

            //newActivity.Description = newActivity.Name; // SET DESCRIPTION LAST (ASSUME NAME)


            //foreach (Client.Core.Activity.ActivityThreshold currentThreshold in EditCareInterventionActivity.SortedThresholds.Values) {

            //    newActivity.Thresholds.Add (currentThreshold);

            //}


            //validationResponse = MercuryApplication.CoreObject_Validate ((Mercury.Server.Application.CoreObject)newActivity.ToServerObject ());

            //if (validationResponse.Count == 0) {

            //    existingActivityFound = false;

            //    foreach (Client.Core.Individual.CareInterventionActivity currentActivity in careIntervention.Activities) {

            //        existingActivityFound = (newActivity.IsEqual (currentActivity));

            //        if (existingActivityFound) { break; }

            //    }


            //    switch (((System.Web.UI.WebControls.Button)sender).ID) {

            //        case "ButtonAddActivity":

            //            if (!existingActivityFound) {

            //                careIntervention.Activities.Add (newActivity);

            //                SaveResponseLabel.Text = String.Empty;

            //            }

            //            else { SaveResponseLabel.Text = "Duplicate Activity."; }

            //            break;


            //        case "ButtonUpdateActivity":

            //            if (ActivitiesGrid.SelectedItems.Count != 0) {

            //                newActivity.CoreObjectId = careIntervention.Activities[ActivitiesGrid.SelectedItems[0].DataSetIndex].Id;

            //                careIntervention.Activities.RemoveAt (ActivitiesGrid.SelectedItems[0].DataSetIndex);

            //                careIntervention.Activities.Add (newActivity);

            //            }

            //            else { SaveResponseLabel.Text = "No Activity Selected."; }

            //            break;

            //    }

            //}

            //else {

            //    foreach (String validationKey in validationResponse.Keys) {

            //        SaveResponseLabel.Text = "Invalid [" + validationKey + "]: " + validationResponse[validationKey];

            //        break;

            //    }

            //}

            //InitializeActivitiesGrid ();

            return;

        }


        protected void ActivityThresholdsGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            Client.Core.Activity.ActivityThreshold threshold;

            switch (eventArgs.RebindReason) {

                case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                    System.Data.DataTable thresholdTable = ActivityThresholdTable;

                    thresholdTable.Rows.Clear ();


                    Int32 currentThresholdIndex = 0;

                    foreach (Int64 thresholdKey in EditCareInterventionActivity.SortedThresholds.Keys) {

                        Client.Core.Activity.ActivityThreshold currentThreshold = EditCareInterventionActivity.SortedThresholds[thresholdKey];

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

                    ActivityThresholdTable = thresholdTable;

                    ActivityThresholdsGrid.DataSource = thresholdTable;

                    break;

                default:

                    if ((eventArgs.RebindReason & Telerik.Web.UI.GridRebindReason.DetailTableBinding) == Telerik.Web.UI.GridRebindReason.DetailTableBinding) {

                        System.Data.DataTable parameterTable = (System.Data.DataTable)Session[SessionCachePrefix + "ActivityThresholdsGrid.ParameterTable"];

                        parameterTable.Rows.Clear ();

                        currentThresholdIndex = 0;

                        foreach (Int64 thresholdKey in EditCareInterventionActivity.SortedThresholds.Keys) {

                            threshold = EditCareInterventionActivity.SortedThresholds[thresholdKey];

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

                            threshold = EditCareInterventionActivity.SortedThresholds.Values[eventArgs.Item.ItemIndex];

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

                        threshold = EditCareInterventionActivity.SortedThresholds[thresholdKey];

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

            Client.Core.Individual.CareInterventionActivity careInterventionActivity = EditCareInterventionActivity;

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


            // SaveResponseLabel.Text = String.Empty;

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


                        threshold = careInterventionActivity.GetNewThreshold ();

                        threshold.RelativeDateValue = Int32.Parse (thresholdRelativeDateValue.Value.ToString ());

                        threshold.RelativeDateQualifier = (Mercury.Server.Application.DateQualifier)Int32.Parse (thresholdRelativeDateQualifier.SelectedItem.Value);

                        threshold.Status = (Mercury.Server.Application.ActivityStatus)Int32.Parse (thresholdStatusSelection.SelectedValue);


                        // RESERVED IF ACTIONS ARE ADDED TO CARE LEVEL ACTIVITY THRESHOLDS

                        //actionId = Int64.Parse (thresholdActionSelection.SelectedValue);

                        //threshold.Action = MercuryApplication.ActionById (actionId);

                        //if (threshold.Action == null) { threshold.Action = new Mercury.Client.Core.Action.Action (MercuryApplication); }


                        if (!EditCareInterventionActivity.HasThreshold (threshold)) {

                            careInterventionActivity.Thresholds.Add (threshold);

                            EditCareInterventionActivity = careInterventionActivity;

                        }

                        else { throw new ApplicationException ("Duplicate Threshold"); }

                    }

                    catch (Exception MercuryApplicationException) {

                        // SaveResponseLabel.Text = "Error: " + MercuryApplicationException.Message;

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

                            threshold = careInterventionActivity.SortedThresholds.Values[updatedItem.ItemIndex];

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

                            threshold = careInterventionActivity.SortedThresholds[thresholdKey];

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

                    foreach (Mercury.Client.Core.Activity.ActivityThreshold currentThreshold in careInterventionActivity.SortedThresholds.Values) {

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

                    if ((deleteIndex > -1) && (deleteIndex < careInterventionActivity.Thresholds.Count)) {

                        threshold = careInterventionActivity.SortedThresholds.Values[deleteIndex];

                        careInterventionActivity.Thresholds.Remove (threshold);

                    }

                    break;

                default:

                    System.Diagnostics.Debug.WriteLine (eventArgs.CommandName);

                    break;

            }


            EditCareInterventionActivity = careInterventionActivity;

            return;

        }

        #endregion


        #region Activity Property Events

        protected void CareInterventionActivityType_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            InitializeActivitiesPageCareInterventionActivityTypeSelection ();


            return;

        }

        protected void ActivityTypeSelection_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            InitializeActivitiesPageActivityType ();

            return;

        }

        protected void ActivityActionSelection_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }


            Client.Core.Individual.CareInterventionActivity careInterventionActivity = EditCareInterventionActivity;

            if (ActivityActionSelection.SelectedItem != null) {

                switch (ActivityTypeSelection.SelectedValue) {

                    case "1": // AUTOMATION

                        Int64 actionId = Convert.ToInt64 (ActivityActionSelection.SelectedValue);

                        careInterventionActivity.Action = MercuryApplication.ActionById (actionId);



                        break;

                    case "2": // WORKFLOW

                        careInterventionActivity.Action = MercuryApplication.ActionById (1); // WORKFLOW

                        careInterventionActivity.Action.ActionParameters["Workflow"].Value = ActivityActionSelection.SelectedValue;

                        careInterventionActivity.Action.ActionParameters["Workflow"].ValueDescription = ActivityActionSelection.SelectedItem.Text;

                        careInterventionActivity.Action.RebindActionParameters (MercuryApplication);

                        break;

                }

            }

            EditCareInterventionActivity = careInterventionActivity;

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

                    if (!EditCareInterventionActivity.Action.ActionParameters[parameterName].Required) {

                        activityParameterValueSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* Not Assigned", "0"));

                    }


                    bindingContexts = (new Client.Core.Individual.Case.MemberCase (MercuryApplication)).ParameterValueSelection (EditCareInterventionActivity.Action.ActionParameters[parameterName].DataType);

                    foreach (String currentBindingContextName in bindingContexts.Keys) {

                        activityParameterValueSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentBindingContextName, bindingContexts[currentBindingContextName]));

                    }


                    activityParameterFixedValue = (Telerik.Web.UI.RadTextBox)eventArgs.Item.FindControl ("ActivityParameterFixedValue");

                    activityParameterFixedValue.Enabled = EditCareInterventionActivity.Action.ActionParameters[parameterName].AllowFixedValue;

                    activityParameterFixedValue.EmptyMessage = (EditCareInterventionActivity.Action.ActionParameters[parameterName].AllowFixedValue) ? String.Empty : "(Not Available)";

                    activityParameterFixedValue.Text = String.Empty;

                }

            }

            return;

        }

        protected void ActivityParametersGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            String parameterName;

            Telerik.Web.UI.RadComboBox parameterValueSelection;

            Telerik.Web.UI.RadTextBox parameterFixedValue;

            Client.Core.Individual.CareInterventionActivity careInterventionActivity = EditCareInterventionActivity;


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

                        if ((careInterventionActivity.Action.ActionParameters[parameterName].AllowFixedValue) && (!String.IsNullOrEmpty (parameterFixedValue.Text))) {

                            careInterventionActivity.Action.ActionParameters[parameterName].ValueType = Mercury.Server.Application.ActionParameterValueType.FixedValue;

                            careInterventionActivity.Action.ActionParameters[parameterName].Value = parameterFixedValue.Text;

                            careInterventionActivity.Action.ActionParameters[parameterName].ValueDescription = parameterFixedValue.Text;

                        }

                        else {

                            careInterventionActivity.Action.ActionParameters[parameterName].ValueType = Mercury.Server.Application.ActionParameterValueType.DataMapping;

                            careInterventionActivity.Action.ActionParameters[parameterName].Value = parameterValueSelection.SelectedItem.Value;

                            careInterventionActivity.Action.ActionParameters[parameterName].ValueDescription = parameterValueSelection.SelectedItem.Text;

                        }

                        if ((careInterventionActivity.Action.Name == "Workflow") && (parameterName == "Workflow")) {

                            careInterventionActivity.Action.RebindActionParameters (MercuryApplication);

                        }

                    }

                    #endregion

                    break;

            }

            EditCareInterventionActivity = careInterventionActivity;

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




            return success;

        }

        protected void ButtonOk_OnClick (Object sender, EventArgs eventArgs) {

            Boolean success = false;

            if (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareInterventionManage)) {

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