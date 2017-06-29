using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Configuration.PropertyPages {

    public partial class Condition : System.Web.UI.Page {

        #region Private Properties

        private Mercury.Client.Core.Condition.Condition condition = null;

        #endregion


        #region Private Session Properties

        private String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (PageInstanceId.Text)) { PageInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return Form.Name + PageInstanceId.Text + ".";
            }

        }

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application)Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            Int64 forConditionId = 0;


            if (MercuryApplication == null) { return; }


            if ((!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ConditionReview))
                && (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ConditionManage))) { Response.Redirect ("/PermissionDenied.aspx", true); return; }

            if (!Page.IsPostBack) {

                #region Initial Page Load

                if (Request.QueryString["ConditionId"] != null) {

                    forConditionId = Int64.Parse (Request.QueryString["ConditionId"]);

                }

                if (forConditionId != 0) {

                    condition = MercuryApplication.ConditionGet (forConditionId, false);

                    if (condition == null) {

                        condition = new Mercury.Client.Core.Condition.Condition (MercuryApplication);

                    }

                    Page.Title = "Condition - " + condition.Name;

                }

                else {

                    condition = new Mercury.Client.Core.Condition.Condition (MercuryApplication);

                }

                InitializeAll ();

                Session[SessionCachePrefix + "Condition"] = condition;

                Session[SessionCachePrefix + "ConditionUnmodified"] = condition.Copy ();

                #endregion

            } // Initial Page Load

            else { // Postback

                condition = (Mercury.Client.Core.Condition.Condition)Session[SessionCachePrefix + "Condition"];

            }

            ApplySecurity ();

            return;

        }

        protected void Page_Unload (object sender, EventArgs e) {

            MercuryApplication.ApplicationClientClose ();

            return;

        }

        #endregion


        #region Initialization

        protected void InitializeAll () {

            InitializeGeneralPage ();


            // All Initializations

            InitializeCriteriaDemographic ();

            InitializeCriteriaDemographicSelection ();

            InitializeCriteriaEvent ();

            InitializeConditionEventsGrid ();
            
            InitializeExtendedPropertiesGrid ();

            return;

        }

        protected void InitializeGeneralPage () {

            if (!String.IsNullOrEmpty (condition.Name)) { Page.Title = "Condition - " + condition.Name; } else { Page.Title = "New Condition"; }

            ConditionName.Text = condition.Name;

            ConditionDescription.Text = condition.Description;

            foreach (Mercury.Server.Application.ConditionClass currentConditionClass in MercuryApplication.ConditionClassesAvailable (false)) {

                if (currentConditionClass.Enabled) {

                    ConditionClassSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentConditionClass.Name, currentConditionClass.Id.ToString ()));

                }

            }

            ConditionClassSelection.SelectedValue = condition.ConditionClassId.ToString ();

            
            ConditionEnabled.Checked = condition.Enabled;

            ConditionVisible.Checked = condition.Visible;


            ConditionCreateAuthorityName.Text = condition.CreateAccountInfo.SecurityAuthorityName;

            ConditionCreateAccountId.Text = condition.CreateAccountInfo.UserAccountId;

            ConditionCreateAccountName.Text = condition.CreateAccountInfo.UserAccountName;

            ConditionCreateDate.MinDate = DateTime.MinValue;

            ConditionCreateDate.SelectedDate = condition.CreateAccountInfo.ActionDate;


            ConditionModifiedAuthorityName.Text = condition.ModifiedAccountInfo.SecurityAuthorityName;

            ConditionModifiedAccountId.Text = condition.ModifiedAccountInfo.UserAccountId;

            ConditionModifiedAccountName.Text = condition.ModifiedAccountInfo.UserAccountName;

            ConditionModifiedDate.MinDate = DateTime.MinValue;

            ConditionModifiedDate.SelectedDate = condition.ModifiedAccountInfo.ActionDate;

            return;

        }

        protected void InitializeCriteriaDemographic () {

            System.Data.DataTable criteriaTable = new System.Data.DataTable ();

            criteriaTable.Columns.Add ("CriteriaId");

            criteriaTable.Columns.Add ("Gender");

            criteriaTable.Columns.Add ("AgeMinimum");

            criteriaTable.Columns.Add ("AgeMaximum");

            criteriaTable.Columns.Add ("Ethnicity");


            foreach (Mercury.Client.Core.Condition.ConditionCriteria.ConditionCriteriaDemographic criteria in condition.DemographicCriteria) {

                String ethnicityName = MercuryApplication.CoreObjectGetNameById ("Ethnicity", criteria.EthnicityId, true);

                if (criteria.UseAgeCriteria) {

                    criteriaTable.Rows.Add (criteria.Id, criteria.Gender, criteria.AgeMinimum, criteria.AgeMaximum, ethnicityName);

                }

                else {

                    criteriaTable.Rows.Add (criteria.Id, criteria.Gender, String.Empty, String.Empty, ethnicityName);

                }

            }

            CriteriaDemographicGrid.DataSource = criteriaTable;

            CriteriaDemographicGrid.DataBind ();


            // TODO: Ethnicity


            return;

        }

        protected void InitializeCriteriaDemographicSelection () {

            System.Collections.Generic.Dictionary<Int64, String> ethnicityReference = MercuryApplication.CoreObjectDictionary ("Ethnicity", true);

            CriteriaDemographicEthnicitySelection.Items.Clear ();

            CriteriaDemographicEthnicitySelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* All Ethnicities", "0"));

            foreach (Int64 currentEthnicityId in ethnicityReference.Keys) {

                String ethnicityName = ethnicityReference[currentEthnicityId];

                ethnicityName = ethnicityName.Replace ("/", " / ");

                if (ethnicityName.Length > 60) { ethnicityName = ethnicityName.Substring (0, 56) + " ..."; }

                CriteriaDemographicEthnicitySelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (ethnicityName, currentEthnicityId.ToString ()));

            }

            return;

        }
        
        protected void InitializeCriteriaEvent () {

            System.Data.DataTable criteriaTable = new System.Data.DataTable ();

            criteriaTable.Columns.Add ("CriteriaId");

            criteriaTable.Columns.Add ("EventType");

            criteriaTable.Columns.Add ("ServiceId");

            criteriaTable.Columns.Add ("ServiceName");


            foreach (Mercury.Client.Core.Condition.ConditionCriteria.ConditionCriteriaEvent criteria in condition.EventCriteria) {

                Client.Core.MedicalServices.Service medicalService = MercuryApplication.MedicalServiceGet (criteria.ServiceId, false);

                if (medicalService != null) {

                    criteriaTable.Rows.Add (criteria.Id, criteria.EventType.ToString (), criteria.ServiceId.ToString (), medicalService.Name);

                }

                else {

                    criteriaTable.Rows.Add (criteria.Id, criteria.EventType.ToString (), criteria.ServiceId.ToString (), "Unable to retreive Service.");

                }

            }

            CriteriaEventGrid.DataSource = criteriaTable;

            CriteriaEventGrid.DataBind ();


            CriteriaEventMedicalServiceSelection.Items.Clear ();

            foreach (Mercury.Server.Application.SearchResultMedicalServiceHeader serviceHeader in MercuryApplication.MedicalServiceHeadersGet (false)) {

                // SERVICE MUST BE ENABLED FOR SELECTION (VISIBILTIY DOES NOT MATTER, THAT IS UI ONLY)

                if (serviceHeader.Enabled) {

                    CriteriaEventMedicalServiceSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (serviceHeader.Name, serviceHeader.Id.ToString ()));

                }

            }

            return;

        }

        protected void InitializeConditionEventsGrid () {

            System.Data.DataTable eventTable = new System.Data.DataTable ();

            eventTable.Columns.Add ("EventName");

            eventTable.Columns.Add ("Action");

            foreach (String eventName in condition.Events.Keys) {

                eventTable.Rows.Add (eventName, condition.Events[eventName].Description);

            }

            Session[SessionCachePrefix + "ConditionEventsGrid.EventTable"] = eventTable;

            ConditionEventsGrid.DataSource = eventTable;

            ConditionEventsGrid.DataBind ();


            System.Data.DataTable parameterTable = new System.Data.DataTable ();

            parameterTable.Columns.Add ("EventName");

            parameterTable.Columns.Add ("ParameterName");

            parameterTable.Columns.Add ("ParameterValue");


            Session[SessionCachePrefix + "ConditionEventsGrid.ParameterTable"] = parameterTable;

            ConditionEventsGrid.MasterTableView.DetailTables[0].DataSource = parameterTable;

            ConditionEventsGrid.MasterTableView.DetailTables[0].DataBind ();

            return;

        }

        protected void InitializeExtendedPropertiesGrid () {

            System.Data.DataTable propertiesTable = new System.Data.DataTable ();

            propertiesTable.Columns.Add ("ExtendedPropertyName");

            propertiesTable.Columns.Add ("ExtendedPropertyValue");

            foreach (String currentPropertyName in condition.ExtendedProperties.Keys) {

                propertiesTable.Rows.Add (

                    currentPropertyName,

                    condition.ExtendedProperties[currentPropertyName]

                );

            }

            ExtendedPropertiesGrid.DataSource = propertiesTable;

            ExtendedPropertiesGrid.DataBind ();

            return;

        }

        protected void ApplySecurity () {

            Boolean hasManagePermission = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ConditionManage);

            ConditionName.ReadOnly = !hasManagePermission;

            ConditionDescription.ReadOnly = !hasManagePermission;

            ConditionClassSelection.Enabled = hasManagePermission;


            ConditionEnabled.Enabled = hasManagePermission;

            ConditionVisible.Enabled = hasManagePermission;

            ButtonCancel.Visible = hasManagePermission;

            ButtonApply.Visible = hasManagePermission;

            return;

        }

        #endregion

        
        #region Add Criteria Events

        protected void ButtonAddCriteriaDemographic_OnClick (Object sender, EventArgs eventArgs) {

            Boolean existingCriteriaFound = false;

            Client.Core.Condition.ConditionCriteria.ConditionCriteriaDemographic criteria = null;

            Mercury.Server.Application.BooleanResponse validationResponse;

            Int32 ageValue;


            if (MercuryApplication == null) { return; }


            criteria = new Mercury.Client.Core.Condition.ConditionCriteria.ConditionCriteriaDemographic (MercuryApplication);

            criteria.ConditionId = condition.Id;

            criteria.Gender = (Mercury.Server.Application.Gender)(Int32.Parse (CriteriaDemographicGender.SelectedItem.Value));

            if ((Int32.TryParse (CriteriaDemographicAgeMinimum.Text, out ageValue)) || (Int32.TryParse (CriteriaDemographicAgeMaximum.Text, out ageValue))) {

                criteria.UseAgeCriteria = true;

                if (Int32.TryParse (CriteriaDemographicAgeMinimum.Text, out ageValue)) { criteria.AgeMinimum = ageValue; }

                if (Int32.TryParse (CriteriaDemographicAgeMaximum.Text, out ageValue)) { criteria.AgeMaximum = ageValue; }

            }

            else { criteria.UseAgeCriteria = false; }

            if (CriteriaDemographicEthnicitySelection.SelectedItem != null) { criteria.EthnicityId = Int64.Parse (CriteriaDemographicEthnicitySelection.SelectedItem.Value); }

            // TODO: Validation Response

            validationResponse = new Mercury.Server.Application.BooleanResponse ();

            validationResponse.Result = true;


            SaveResponseLabel.Text = String.Empty;

            if (validationResponse.Result) {

                existingCriteriaFound = false;

                foreach (Client.Core.Condition.ConditionCriteria.ConditionCriteriaDemographic currentCriteria in condition.DemographicCriteria) {

                    if (currentCriteria.IsEqual (criteria)) {

                        existingCriteriaFound = true;

                        SaveResponseLabel.Text = "Duplicate Criteria Found.";

                        break;

                    }

                }

                if (!existingCriteriaFound) {

                    if ((criteria.UseAgeCriteria) && (criteria.AgeMinimum <= criteria.AgeMaximum)) {

                        condition.DemographicCriteria.Add (criteria);

                    }

                    else {

                        SaveResponseLabel.Text = "Invalid Age Range.";

                    }

                }

            }

            else {

                if (validationResponse.HasException) {

                    SaveResponseLabel.Text = validationResponse.Exception.Message;

                }

                else {

                    SaveResponseLabel.Text = "Unable to validate the Criteria.";

                }

            }

            InitializeCriteriaDemographic ();

            return;

        }

        protected void CriteriaDemographicGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            Int32 deleteIndex = eventArgs.Item.DataSetIndex;

            condition.DemographicCriteria.RemoveAt (deleteIndex);

            InitializeCriteriaDemographic ();

            return;

        }

        protected void ButtonAddCriteriaEvent_OnClick (Object sender, EventArgs eventArgs) {

            Boolean existingCriteriaFound = false;

            Client.Core.Condition.ConditionCriteria.ConditionCriteriaEvent criteria = null;

            Mercury.Server.Application.BooleanResponse validationResponse;


            if (MercuryApplication == null) { return; }


            criteria = new Mercury.Client.Core.Condition.ConditionCriteria.ConditionCriteriaEvent (MercuryApplication);

            criteria.ConditionId = condition.Id;

            if (CriteriaEventMedicalServiceSelection.SelectedItem == null) { return; }

            criteria.EventType = (Mercury.Server.Application.ConditionCriteriaEventType)(Int32.Parse (CriteriaEventType.SelectedItem.Value));

            criteria.ServiceId = Int64.Parse (CriteriaEventMedicalServiceSelection.SelectedItem.Value);


            // TODO: Validation Response

            validationResponse = new Mercury.Server.Application.BooleanResponse ();

            validationResponse.Result = true;


            SaveResponseLabel.Text = String.Empty;

            if (validationResponse.Result) {

                existingCriteriaFound = false;

                foreach (Client.Core.Condition.ConditionCriteria.ConditionCriteriaEvent currentCriteria in condition.EventCriteria) {

                    if (currentCriteria.IsEqual (criteria)) {

                        existingCriteriaFound = true;

                        SaveResponseLabel.Text = "Duplicate Criteria Found.";

                        break;

                    }

                }

                if (!existingCriteriaFound) {

                    condition.EventCriteria.Add (criteria);

                }

            }

            else {

                if (validationResponse.HasException) {

                    SaveResponseLabel.Text = validationResponse.Exception.Message;

                }

                else {

                    SaveResponseLabel.Text = "Unable to validate the Criteria.";

                }

            }

            InitializeCriteriaEvent ();

            return;

        }

        protected void CriteriaEventGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            Int32 deleteIndex = eventArgs.Item.DataSetIndex;

            condition.EventCriteria.RemoveAt (deleteIndex);

            InitializeCriteriaEvent ();

            return;

        }

        #endregion


        #region Condition Events

        protected void ConditionEventsGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            switch (eventArgs.RebindReason) {

                case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                    System.Data.DataTable eventTable = (System.Data.DataTable)Session[SessionCachePrefix + "ConditionEventsGrid.EventTable"];

                    eventTable.Rows.Clear ();

                    foreach (String eventName in condition.Events.Keys) {

                        eventTable.Rows.Add (eventName, condition.Events[eventName].Description);

                    }

                    ConditionEventsGrid.DataSource = eventTable;

                    break;

                default:

                    if ((eventArgs.RebindReason & Telerik.Web.UI.GridRebindReason.DetailTableBinding) == Telerik.Web.UI.GridRebindReason.DetailTableBinding) {

                        System.Data.DataTable parameterTable = (System.Data.DataTable)Session[SessionCachePrefix + "ConditionEventsGrid.ParameterTable"];

                        parameterTable.Rows.Clear ();

                        foreach (String eventName in condition.Events.Keys) {

                            foreach (String parameterName in condition.Events[eventName].ActionParameters.Keys) {

                                parameterTable.Rows.Add (eventName, parameterName, condition.Events[eventName].ActionParameters[parameterName].ValueDescription);

                            }

                        }

                        ConditionEventsGrid.MasterTableView.DetailTables[0].DataSource = parameterTable;

                    }

                    break;

            }

            return;

        }

        protected void ConditionEventsGrid_OnItemDataBound (Object sender, Telerik.Web.UI.GridItemEventArgs eventArgs) {

            Telerik.Web.UI.RadComboBox conditionEventActionSelection;

            Telerik.Web.UI.RadComboBox conditionEventParameterValueSelection;

            Telerik.Web.UI.RadTextBox conditionEventParameterFixedValue;

            System.Collections.Generic.Dictionary<String, String> bindingContexts;

            String eventName;

            String parameterName;


            if (MercuryApplication == null) { return; }

            if ((eventArgs.Item is Telerik.Web.UI.GridEditableItem) && (eventArgs.Item.IsInEditMode)) {

                Telerik.Web.UI.GridEditableItem editItem = (Telerik.Web.UI.GridEditableItem)eventArgs.Item;


                switch (eventArgs.Item.OwnerTableView.Name) {

                    case "ConditionEvent":

                        conditionEventActionSelection = (Telerik.Web.UI.RadComboBox)editItem.FindControl ("ConditionEventActionSelection");

                        conditionEventActionSelection.Items.Clear ();

                        conditionEventActionSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* No Action Selected", "0"));

                        foreach (Mercury.Server.Application.Action currentAction in MercuryApplication.ActionsAvailable (false)) {

                            conditionEventActionSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentAction.Name, currentAction.Id.ToString ()));

                        }


                        if (eventArgs.Item.ItemIndex != -1) {

                            eventName = (String)editItem.OwnerTableView.DataKeyValues[eventArgs.Item.ItemIndex]["EventName"];

                            conditionEventActionSelection.SelectedValue = condition.Events[eventName].Id.ToString ();

                        }

                        break;


                    case "ConditionEventParameters":

                        eventName = (String)editItem.OwnerTableView.DataKeyValues[eventArgs.Item.ItemIndex]["EventName"];

                        parameterName = (String)editItem.OwnerTableView.DataKeyValues[eventArgs.Item.ItemIndex]["ParameterName"];

                        conditionEventParameterValueSelection = (Telerik.Web.UI.RadComboBox)eventArgs.Item.FindControl ("ConditionEventParameterValueSelection");

                        conditionEventParameterValueSelection.Items.Clear ();

                        if (!condition.Events[eventName].ActionParameters[parameterName].Required) {

                            conditionEventParameterValueSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* Not Assigned", "0"));

                        }

                        bindingContexts = condition.ParameterValueSelection (condition.Events[eventName].ActionParameters[parameterName].DataType);

                        foreach (String currentBindingContextName in bindingContexts.Keys) {

                            conditionEventParameterValueSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentBindingContextName, bindingContexts[currentBindingContextName]));

                        }

                        conditionEventParameterFixedValue = (Telerik.Web.UI.RadTextBox)eventArgs.Item.FindControl ("ConditionEventParameterFixedValue");

                        conditionEventParameterFixedValue.Enabled = condition.Events[eventName].ActionParameters[parameterName].AllowFixedValue;

                        conditionEventParameterFixedValue.EmptyMessage = (condition.Events[eventName].ActionParameters[parameterName].AllowFixedValue) ? String.Empty : "(Not Available)";

                        conditionEventParameterFixedValue.Text = String.Empty;

                        break;

                }

            }

            return;

        }

        protected void ConditionEventsGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            String eventName;

            Telerik.Web.UI.RadComboBox conditionEventActionSelection;

            Int64 actionId;

            String parameterName;

            Telerik.Web.UI.RadComboBox parameterValueSelection;

            Telerik.Web.UI.RadTextBox parameterFixedValue;


            if (MercuryApplication == null) { return; }

            switch (eventArgs.CommandName) {

                case Telerik.Web.UI.RadGrid.EditCommandName:

                    #region Edit Command

                    switch (eventArgs.Item.OwnerTableView.Name) {

                        case "ConditionEvent":

                            Telerik.Web.UI.GridEditCommandColumn editColumn = (Telerik.Web.UI.GridEditCommandColumn)ConditionEventsGrid.MasterTableView.GetColumn ("EditCommandColumn");

                            if (!editColumn.Visible) { editColumn.Visible = true; }

                            Telerik.Web.UI.GridEditableItem editItem = (Telerik.Web.UI.GridEditableItem)eventArgs.Item;

                            break;

                        case "ConditionEventParameters":

                            editColumn = (Telerik.Web.UI.GridEditCommandColumn)eventArgs.Item.OwnerTableView.GetColumn ("EditCommandColumn");

                            if (!editColumn.Visible) { editColumn.Visible = true; }

                            break;

                    }

                    #endregion

                    break;

                case Telerik.Web.UI.RadGrid.UpdateCommandName:

                    #region Update Command

                    Telerik.Web.UI.GridEditableItem updatedItem = (Telerik.Web.UI.GridEditableItem)eventArgs.Item;

                    switch (eventArgs.Item.OwnerTableView.Name) {

                        case "ConditionEvent":

                            eventName = (String)updatedItem.OwnerTableView.DataKeyValues[eventArgs.Item.ItemIndex]["EventName"];

                            conditionEventActionSelection = (Telerik.Web.UI.RadComboBox)updatedItem.FindControl ("ConditionEventActionSelection");

                            actionId = Int64.Parse (conditionEventActionSelection.SelectedValue);

                            condition.Events[eventName] = MercuryApplication.ActionById (actionId);

                            if (condition.Events[eventName] == null) { condition.Events[eventName] = new Mercury.Client.Core.Action.Action (MercuryApplication); }

                            break;

                        case "ConditionEventParameters":

                            parameterValueSelection = (Telerik.Web.UI.RadComboBox)eventArgs.Item.FindControl ("ConditionEventParameterValueSelection");

                            parameterFixedValue = (Telerik.Web.UI.RadTextBox)eventArgs.Item.FindControl ("ConditionEventParameterFixedValue");

                            if ((parameterValueSelection.SelectedItem != null) || (!String.IsNullOrEmpty (parameterFixedValue.Text))) {

                                eventName = (String)updatedItem.OwnerTableView.DataKeyValues[eventArgs.Item.ItemIndex]["EventName"];

                                parameterName = (String)((Telerik.Web.UI.GridEditableItem)eventArgs.Item).OwnerTableView.DataKeyValues[eventArgs.Item.ItemIndex]["ParameterName"];


                                if ((condition.Events[eventName].ActionParameters[parameterName].AllowFixedValue) && (!String.IsNullOrEmpty (parameterFixedValue.Text))) {

                                    condition.Events[eventName].ActionParameters[parameterName].ValueType = Mercury.Server.Application.ActionParameterValueType.FixedValue;

                                    condition.Events[eventName].ActionParameters[parameterName].Value = parameterFixedValue.Text;

                                    condition.Events[eventName].ActionParameters[parameterName].ValueDescription = parameterFixedValue.Text;

                                }

                                else {

                                    condition.Events[eventName].ActionParameters[parameterName].ValueType = Mercury.Server.Application.ActionParameterValueType.DataMapping;

                                    condition.Events[eventName].ActionParameters[parameterName].Value = parameterValueSelection.SelectedItem.Value;

                                    condition.Events[eventName].ActionParameters[parameterName].ValueDescription = parameterValueSelection.SelectedItem.Text;

                                }

                                if ((condition.Events[eventName].Name == "Workflow") && (parameterName == "Workflow")) {

                                    condition.Events[eventName].RebindActionParameters (MercuryApplication);

                                }

                            }

                            break;

                    }

                    #endregion

                    break;

            }


            return;

        }

        #endregion


        #region Extended Properties Events

        protected void ButtonAddExtendedProperty_OnClick (Object Sender, EventArgs eventArgs) {

            if (MercuryApplication == null) { return; }


            if (!String.IsNullOrEmpty (ConditionExtendedPropertyName.Text)) {

                if (!condition.ExtendedProperties.ContainsKey (ConditionExtendedPropertyName.Text)) {

                    condition.ExtendedProperties.Add (ConditionExtendedPropertyName.Text, ConditionExtendedPropertyValue.Text);

                }

                else { SaveResponseLabel.Text = "Cannot add duplicate Extended Property to Work Queue."; }

            }

            InitializeExtendedPropertiesGrid ();

            return;

        }

        protected void ExtendedPropertiesGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }


            Int32 deleteIndex = eventArgs.Item.DataSetIndex;

            String extendedPropertyName = (String)eventArgs.Item.OwnerTableView.DataKeyValues[deleteIndex]["ExtendedPropertyName"];

            condition.ExtendedProperties.Remove (extendedPropertyName);


            InitializeExtendedPropertiesGrid ();

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



            Mercury.Client.Core.Condition.Condition conditionUnmodified = (Mercury.Client.Core.Condition.Condition)Session[SessionCachePrefix + "ConditionUnmodified"];


            condition.Name = ConditionName.Text;

            condition.Description = ConditionDescription.Text;


            if (ConditionClassSelection.SelectedItem != null) {

                condition.ConditionClassId = Convert.ToInt64 (ConditionClassSelection.SelectedItem.Value);

                condition.ConditionClassName = Convert.ToString (ConditionClassSelection.SelectedItem.Text);

            }

            else {

                condition.ConditionClassId = 0;

                condition.ConditionClassName = ConditionClassSelection.Text;

            }


            condition.Enabled = ConditionEnabled.Checked;

            condition.Visible = ConditionVisible.Checked;


            if (conditionUnmodified.Id == 0) { isModified = true; }

            if (!isModified) { isModified = !condition.IsEqual (conditionUnmodified); }


            validationResponse = condition.Validate ();

            isValid = (validationResponse.Count == 0);


            if ((isModified) && (isValid)) {

                if (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ConditionManage)) {

                    SaveResponseLabel.Text = "Permission Denied.";

                    return false;

                }

                success = MercuryApplication.ConditionSave (condition);

                if (success) {

                    condition = MercuryApplication.ConditionGet (condition.Id, false);

                    Session[SessionCachePrefix + "Condition"] = condition;

                    Session[SessionCachePrefix + "ConditionUnmodified"] = condition.Copy ();

                    SaveResponseLabel.Text = "Save Successful.";

                    InitializeAll ();

                }

                else {

                    SaveResponseLabel.Text = "Unable to Save Condition.";

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

            if (ApplyChanges ()) { Server.Transfer ("/WindowClose.aspx"); }

            return;

        }

        protected void ButtonApply_OnClick (Object sender, EventArgs eventArgs) {

            ApplyChanges ();

            return;

        }

        protected void ButtonCancel_OnClick (Object sender, EventArgs eventArgs) {

            Server.Transfer ("/WindowClose.aspx");

            return;

        }

        #endregion

    }

}