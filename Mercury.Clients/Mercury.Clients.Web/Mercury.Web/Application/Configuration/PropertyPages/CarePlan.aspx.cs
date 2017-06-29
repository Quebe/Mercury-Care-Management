using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Configuration.PropertyPages {

    public partial class CarePlan : System.Web.UI.Page {

        #region Private Properties

        private Boolean isPageUnloading = false;

        private Client.Core.Individual.CarePlan carePlan;

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

        private Mercury.Client.Core.Individual.CarePlanIntervention EditCarePlanIntervention {

            get {

                Mercury.Client.Core.Individual.CarePlanIntervention activity = (Mercury.Client.Core.Individual.CarePlanIntervention)Session[SessionCachePrefix + "EditCarePlanIntervention"];

                if (activity == null) {

                    activity = new Client.Core.Individual.CarePlanIntervention (MercuryApplication);

                    Session[SessionCachePrefix + "EditCarePlanIntervention"] = activity;

                }

                return activity;

            }

            set { Session[SessionCachePrefix + "EditCarePlanIntervention"] = value; }

        }
        
        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            Int64 forCarePlanId = 0;


            if (MercuryApplication == null) { return; }

            if ((!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CarePlanReview))

                && (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CarePlanManage))) { Response.Redirect ("/PermissionDenied.aspx", true); return; }


            if (!Page.IsPostBack) {

                #region Initial Page Load

                if (Request.QueryString["CarePlanId"] != null) {

                    forCarePlanId = Int64.Parse (Request.QueryString["CarePlanId"]);

                }

                if (forCarePlanId != 0) {

                    carePlan = MercuryApplication.CarePlanGet (forCarePlanId, false);

                    if (carePlan == null) {

                        carePlan = new Mercury.Client.Core.Individual.CarePlan (MercuryApplication);

                    }

                }

                else {

                    carePlan = new Mercury.Client.Core.Individual.CarePlan (MercuryApplication);

                }

                InitializeAll ();

                Session[SessionCachePrefix + "CarePlan"] = carePlan;

                Session[SessionCachePrefix + "CarePlanUnmodified"] = carePlan.Copy ();

                #endregion

            } // Initial Page Load

            else { // Postback

                carePlan = (Mercury.Client.Core.Individual.CarePlan)Session[SessionCachePrefix + "CarePlan"];

            }

            ApplySecurity ();

            if (!String.IsNullOrEmpty (carePlan.Name)) { Page.Title = "Care Plan - " + carePlan.Name; } else { Page.Title = "Care Plan"; }

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

            InitializeGoalsPage ();

            InitializeInterventionsPage ();

            return;

        }

        protected void InitializeGeneralPage () {

            CarePlanName.Text = carePlan.Name;

            CarePlanDescription.Text = carePlan.Description;


            CarePlanEnabled.Checked = carePlan.Enabled;

            CarePlanVisible.Checked = carePlan.Visible;


            CarePlanCreateAuthorityName.Text = carePlan.CreateAccountInfo.SecurityAuthorityName;

            CarePlanCreateAccountId.Text = carePlan.CreateAccountInfo.UserAccountId;

            CarePlanCreateAccountName.Text = carePlan.CreateAccountInfo.UserAccountName;

            CarePlanCreateDate.MinDate = DateTime.MinValue;

            CarePlanCreateDate.SelectedDate = carePlan.CreateAccountInfo.ActionDate;


            CarePlanModifiedAuthorityName.Text = carePlan.ModifiedAccountInfo.SecurityAuthorityName;

            CarePlanModifiedAccountId.Text = carePlan.ModifiedAccountInfo.UserAccountId;

            CarePlanModifiedAccountName.Text = carePlan.ModifiedAccountInfo.UserAccountName;

            CarePlanModifiedDate.MinDate = DateTime.MinValue;

            CarePlanModifiedDate.SelectedDate = carePlan.ModifiedAccountInfo.ActionDate;

            return;

        }

        protected void InitializeGoalsPage () {

            InitializeGoalsGrid ();


            var careMeasures =

                from currentCareMeasure in MercuryApplication.CareMeasuresAvailable (true)

                where ((currentCareMeasure.Enabled) && (currentCareMeasure.Visible))

                select currentCareMeasure;


            CarePlanGoalCareMeasureSelection.DataSource = careMeasures;

            CarePlanGoalCareMeasureSelection.DataTextField = "Name";

            CarePlanGoalCareMeasureSelection.DataValueField = "Id";

            CarePlanGoalCareMeasureSelection.DataBind ();

            return;

        }

        protected void InitializeGoalsGrid () {

            CarePlanGoalsGrid.DataSource = carePlan.Goals;

            CarePlanGoalsGrid.DataBind ();

            InterventionsGrid.Rebind ();

            InitializeInterventionGoalSelection ();

            return;

        }


        protected void InitializeInterventionsPage () {

            var careInterventionsAvailable =

                from currentCareIntervention in MercuryApplication.CareInterventionsAvailable (false)

                where ((currentCareIntervention.Enabled) && (currentCareIntervention.Visible))

                orderby currentCareIntervention.Name

                select currentCareIntervention;


            CarePlanCareInterventionSelection.DataSource = careInterventionsAvailable;

            CarePlanCareInterventionSelection.DataTextField = "Name";

            CarePlanCareInterventionSelection.DataValueField = "Id";

            CarePlanCareInterventionSelection.DataBind ();

            return;

        }
        
        protected void InitializeInterventionGoalSelection () {

            CarePlanCareInterventionGoalSelection.DataSource = carePlan.Goals;

            CarePlanCareInterventionGoalSelection.DataTextField = "Name";

            CarePlanCareInterventionGoalSelection.DataValueField = "Name";

            CarePlanCareInterventionGoalSelection.DataBind ();

            return;

        }

        protected void ApplySecurity () {

            Boolean hasManagePermission = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CarePlanManage);

            CarePlanName.ReadOnly = !hasManagePermission;

            CarePlanDescription.ReadOnly = !hasManagePermission;


            CarePlanEnabled.Enabled = hasManagePermission;

            CarePlanVisible.Enabled = hasManagePermission;


            ButtonCancel.Visible = hasManagePermission;

            ButtonApply.Visible = hasManagePermission;

            return;

        }

        #endregion


        #region Goal Grid Events

        protected void ButtonAddUpdateCarePlanGoal_OnClick (Object sender, EventArgs eventArgs) {

            if (MercuryApplication == null) { return; }


            Boolean existingGoalFound = false;

            Boolean duplicateName = false;

            Client.Core.Individual.CarePlanGoal carePlanGoal = null;

            Dictionary<String, String> validationResponse;


            // CREATE NEW GOAL OBJECT AN ASSOCIATE WITH PARENT GOAL

            carePlanGoal = new Client.Core.Individual.CarePlanGoal (MercuryApplication);

            carePlanGoal.CarePlanId = carePlan.Id;


            // ASSIGN PROPERTIES FROM CONTROLS INTO OBJECT

            carePlanGoal.Name = CarePlanGoalName.Text;

            carePlanGoal.Inclusion = (Mercury.Server.Application.CarePlanItemInclusion)Convert.ToInt32 (CarePlanGoalInclusion.SelectedValue);

            carePlanGoal.Description = CarePlanGoalName.Text;

            carePlanGoal.Enabled = CarePlanGoalEnabled.Checked;

            carePlanGoal.GoalTimeframe = (Mercury.Server.Application.CarePlanGoalTimeframe)Convert.ToInt32 (CarePlanGoalTimeframeSelection.SelectedValue);

            carePlanGoal.ScheduleValue = Convert.ToInt32 (CarePlanGoalScheduleValue.Value);

            carePlanGoal.ScheduleQualifier = (Mercury.Server.Application.DateQualifier)Convert.ToInt32 (CarePlanGoalScheduleQualifierSelection.SelectedValue);

            carePlanGoal.CareMeasureId = (CarePlanGoalCareMeasureSelection.SelectedItem != null) ? Convert.ToInt64 (CarePlanGoalCareMeasureSelection.SelectedValue) : 0;
            
            carePlanGoal.ClinicalNarrative = CarePlanGoalClinicalNarrative.Text;

            carePlanGoal.CommonNarrative = CarePlanGoalCommonNarrative.Text;


            validationResponse = carePlanGoal.Validate ();

            if (validationResponse.Count == 0) {
                
                SaveResponseLabel.Text = String.Empty;
                
                foreach (Client.Core.Individual.CarePlanGoal currentCarePlanGoal in carePlan.Goals) {

                    if (currentCarePlanGoal.IsEqual (carePlanGoal)) { existingGoalFound = true; break; }

                    if (currentCarePlanGoal.Name.ToUpper () == carePlanGoal.Name.ToUpper ()) {

                        duplicateName = true;

                    }

                }

                switch (((System.Web.UI.WebControls.Button)sender).ID) {

                    case "CarePlanGoalAdd":

                        if ((!existingGoalFound) && (!duplicateName)) {

                            carePlan.Goals.Add (carePlanGoal);

                        }

                        else { SaveResponseLabel.Text = "Duplicate Goal Exists."; }

                        break;

                    case "CarePlanGoalUpdate":

                        if (CarePlanGoalsGrid.SelectedItems.Count != 0) {

                            carePlanGoal.Id = carePlan.Goals[CarePlanGoalsGrid.SelectedItems[0].DataSetIndex].Id;

                            carePlan.Goals.RemoveAt (CarePlanGoalsGrid.SelectedItems[0].DataSetIndex);

                            carePlan.Goals.Add (carePlanGoal);

                        }

                        break;

                }

            }

            else {

                foreach (String validationKey in validationResponse.Keys) {

                    SaveResponseLabel.Text = "Invalid [" + validationKey + "]: " + validationResponse[validationKey];

                    break;

                }

            }


            InitializeGoalsGrid ();

            return;

        }

        protected void CarePlanGoalsGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            Int32 deleteIndex = eventArgs.Item.DataSetIndex;

            carePlan.Goals.RemoveAt (deleteIndex);

            InitializeGoalsGrid ();

            return;

        }

        protected void CarePlanGoalsGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            Int32 itemIndex = eventArgs.Item.DataSetIndex;

            switch (eventArgs.CommandName) {

                case "ToggleActive":

                    carePlan.Goals[itemIndex].Enabled = !carePlan.Goals[itemIndex].Enabled;

                    break;

                case Telerik.Web.UI.RadGrid.EditCommandName:

                    Client.Core.Individual.CarePlanGoal carePlanGoal = carePlan.Goals[eventArgs.Item.ItemIndex];


                    CarePlanGoalName.Text = carePlanGoal.Name;

                    CarePlanGoalInclusion.SelectedValue = ((Int32)carePlanGoal.Inclusion).ToString ();

                    CarePlanGoalEnabled.Checked = carePlanGoal.Enabled;

                    CarePlanGoalTimeframeSelection.SelectedValue = ((Int32)carePlanGoal.GoalTimeframe).ToString ();

                    CarePlanGoalScheduleValue.Value = carePlanGoal.ScheduleValue;

                    CarePlanGoalScheduleQualifierSelection.SelectedValue = ((Int32)carePlanGoal.ScheduleQualifier).ToString ();

                    CarePlanGoalCareMeasureSelection.SelectedValue = carePlanGoal.CareMeasureId.ToString ();


                    CarePlanGoalClinicalNarrative.Text = carePlanGoal.ClinicalNarrative;

                    CarePlanGoalCommonNarrative.Text = carePlanGoal.CommonNarrative;


                    eventArgs.Canceled = true;

                    break;

            }

            InitializeGoalsGrid ();

            CarePlanGoalsGrid.SelectedIndexes.Add (itemIndex);

            return;

        }
        
        #endregion 


        #region Interventions Grid Events

        protected void ButtonAddUpdateIntervention_OnClick (Object sender, EventArgs eventArgs) {

            Boolean existingInterventionFound = false;

            Client.Core.Individual.CarePlanIntervention newIntervention = null;

            Int64 careInterventionId = 0;

            Client.Core.Individual.CarePlanGoal carePlanGoal;

            Client.Core.Individual.CareIntervention careIntervention;

            Dictionary<String, String> validationResponse;

            SaveResponseLabel.Text = String.Empty;

            


            if (MercuryApplication == null) { return; }

            if (CarePlanCareInterventionGoalSelection.SelectedItem == null) { return; }

            if (CarePlanCareInterventionSelection.SelectedItem == null) { return; }


            // GOAL THAT IS TO BE THE PARENT OF THE INTERVENTION 

            carePlanGoal = carePlan.CarePlanGoal (CarePlanCareInterventionGoalSelection.SelectedValue);

            if (carePlanGoal == null) { return; }


            // CREATE INTERVENTION

            careInterventionId = Convert.ToInt64 (CarePlanCareInterventionSelection.SelectedValue);

            careIntervention = MercuryApplication.CareInterventionGet (careInterventionId, true);


            newIntervention = new Client.Core.Individual.CarePlanIntervention (MercuryApplication);

            newIntervention.CarePlanGoalId = carePlanGoal.Id;

            newIntervention.CarePlanGoal = carePlanGoal;

            newIntervention.Name = careIntervention.Name;

            newIntervention.Description = careIntervention.Description;

            newIntervention.CareInterventionId = careIntervention.Id;

            newIntervention.Inclusion = (Mercury.Server.Application.CarePlanItemInclusion)Convert.ToInt32 (CarePlanInteventionInclusionSelection.SelectedValue);
            

            validationResponse = MercuryApplication.CoreObject_Validate ((Mercury.Server.Application.CoreObject)newIntervention.ToServerObject ());

            if (validationResponse.Count == 0) {

                existingInterventionFound = carePlanGoal.ContainsCareIntervention (careInterventionId);


                switch (((System.Web.UI.WebControls.Button)sender).ID) {

                    case "ButtonAddIntervention":

                        if (!existingInterventionFound) {

                            carePlanGoal.Interventions.Add (newIntervention);

                            SaveResponseLabel.Text = String.Empty;

                        }

                        else { SaveResponseLabel.Text = "Duplicate Intervention."; }

                        break;


                    case "ButtonUpdateIntervention":

                        if (InterventionsGrid.SelectedItems.Count != 0) {

                            newIntervention.Id = carePlanGoal.Interventions[InterventionsGrid.SelectedItems[0].DataSetIndex].Id;

                            carePlanGoal.Interventions.RemoveAt (InterventionsGrid.SelectedItems[0].DataSetIndex);

                            carePlanGoal.Interventions.Add (newIntervention);

                        }

                        else { SaveResponseLabel.Text = "No Intervention Selected."; }

                        break;

                }

            }

            else {

                foreach (String validationKey in validationResponse.Keys) {

                    SaveResponseLabel.Text = "Invalid [" + validationKey + "]: " + validationResponse[validationKey];

                    break;

                }

            }

            InitializeInterventionsPage ();

            InterventionsGrid.Rebind ();

            return;

        }

        protected void InterventionsGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            switch (eventArgs.RebindReason) {

                case Telerik.Web.UI.GridRebindReason.InitialLoad:

                case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                case Telerik.Web.UI.GridRebindReason.PostbackViewStateNotPersisted:
                   
                    var careInterventions =

                        from currentCarePlanGoal in carePlan.Goals

                        from currentCarePlanIntervention in currentCarePlanGoal.Interventions

                        orderby currentCarePlanGoal.Name, currentCarePlanIntervention.Name

                        select currentCarePlanIntervention;


                    InterventionsGrid.DataSource = careInterventions;

                    InterventionsGrid.MasterTableView.DataKeyNames = new String[] { "CarePlanGoal.Name", "Name" };

                    break;

                default:

                    break;

            }

            return;

        }

        protected void InterventionsGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            if (!(eventArgs.Item is Telerik.Web.UI.GridDataItem)) { return; }


            String carePlanGoalName = String.Empty;

            Client.Core.Individual.CarePlanGoal carePlanGoal = null;

            String carePlanInterventionName = String.Empty;

            Client.Core.Individual.CarePlanIntervention carePlanIntervention = null;


            // GET VALUES OUT OF THE DATA ITEM

            Telerik.Web.UI.GridDataItem dataItem = (Telerik.Web.UI.GridDataItem)eventArgs.Item;

            carePlanGoalName = (String)dataItem.GetDataKeyValue ("CarePlanGoal.Name");

            carePlanGoal = carePlan.CarePlanGoal (carePlanGoalName);

            if (carePlanGoal != null) {

                carePlanInterventionName = (String)dataItem.GetDataKeyValue ("Name");

                carePlanIntervention = carePlanGoal.CarePlanIntervention (carePlanInterventionName);

                if (carePlanIntervention != null) { carePlanGoal.Interventions.Remove (carePlanIntervention); }

            }


            InitializeInterventionsPage ();

            InterventionsGrid.Rebind ();

            return;

        }

        protected void InterventionsGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            if (!(eventArgs.Item is Telerik.Web.UI.GridDataItem)) { return; }

            Int32 itemIndex = eventArgs.Item.DataSetIndex;


            String carePlanGoalName = String.Empty;

            Client.Core.Individual.CarePlanGoal carePlanGoal = null;

            String carePlanInterventionName = String.Empty;

            Client.Core.Individual.CarePlanIntervention carePlanIntervention = null;


            // GET VALUES OUT OF THE DATA ITEM

            Telerik.Web.UI.GridDataItem dataItem = (Telerik.Web.UI.GridDataItem)eventArgs.Item;

            carePlanGoalName = (String)dataItem.GetDataKeyValue ("CarePlanGoal.Name");

            carePlanGoal = carePlan.CarePlanGoal (carePlanGoalName);

            if (carePlanGoal != null) {

                carePlanInterventionName = (String)dataItem.GetDataKeyValue ("Name");

                carePlanIntervention = carePlanGoal.CarePlanIntervention (carePlanInterventionName);

            }


            switch (eventArgs.CommandName) {

                case "ToggleActive":

                    if (carePlanIntervention != null) { carePlanIntervention.Enabled = !carePlanIntervention.Enabled; }

                    break;

                case Telerik.Web.UI.RadGrid.EditCommandName:

                    // MAKE COPY OF SELECTED ROW FOR EDITING

                    EditCarePlanIntervention = carePlanIntervention.Copy ();

                    // EditCarePlanIntervention = carePlan.Interventions[activityIndex].Copy ();


                    // TODO: ASSIGN INTERVENTION MEMBERS


                    eventArgs.Canceled = true;

                    break;

                default:

                    System.Diagnostics.Debug.WriteLine (eventArgs.CommandName);

                    break;

            }

            InitializeInterventionsPage ();

            InterventionsGrid.Rebind ();

            InterventionsGrid.SelectedIndexes.Add (eventArgs.Item.ItemIndex);

            return;

        }
        
        #endregion


        #region Dialog Button Event Handlers

        protected Boolean ApplyChanges () {

            Boolean isModified = false;

            Boolean success = false;

            Boolean isValid = false;

            System.Collections.Generic.Dictionary<String, String> validationResponse;



            Mercury.Client.Core.Individual.CarePlan carePlanUnmodified = (Mercury.Client.Core.Individual.CarePlan)Session[SessionCachePrefix + "CarePlanUnmodified"];

            if (carePlanUnmodified.Id == 0) { isModified = true; }


            carePlan.Name = CarePlanName.Text.Trim ();

            carePlan.Description = CarePlanDescription.Text.Trim ();

            carePlan.Enabled = CarePlanEnabled.Checked;

            carePlan.Visible = CarePlanVisible.Checked;

            if (!isModified) { isModified = !carePlan.IsEqual (carePlanUnmodified); }


            validationResponse = carePlan.Validate ();

            isValid = (validationResponse.Count == 0);


            if ((isModified) && (isValid)) {

                success = MercuryApplication.CarePlanSave (carePlan);

                if (success) {

                    carePlan = MercuryApplication.CarePlanGet (carePlan.Id, false);

                    Session[SessionCachePrefix + "CarePlan"] = carePlan;

                    Session[SessionCachePrefix + "CarePlanUnmodified"] = carePlan.Copy ();

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

            if (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CarePlanManage)) {

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