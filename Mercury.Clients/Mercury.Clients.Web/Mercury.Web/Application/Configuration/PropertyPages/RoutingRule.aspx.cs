using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Configuration.PropertyPages {

    public partial class RoutingRule : System.Web.UI.Page {

        #region Private Propreties

        private Mercury.Client.Core.Work.RoutingRule routingRule;

        #endregion


        #region Private Session Properties

        public String SessionCachePrefix {

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

            Int64 forRoutingRuleId = 0;


            if (MercuryApplication == null) { return; }

            if ((!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.RoutingRuleReview))

                && (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.RoutingRuleManage))) { Response.Redirect ("/PermissionDenied.aspx", true); return; }


            if (!Page.IsPostBack) {

                #region Initial Page Load

                if (Request.QueryString["RoutingRuleId"] != null) {

                    forRoutingRuleId = Int64.Parse (Request.QueryString["RoutingRuleId"]);

                }

                if (forRoutingRuleId != 0) {

                    routingRule = MercuryApplication.RoutingRuleGet (forRoutingRuleId);

                    if (routingRule == null) {

                        routingRule = new Mercury.Client.Core.Work.RoutingRule (MercuryApplication);

                    }

                    Page.Title = "RoutingRule - " + routingRule.Name;

                }

                else {

                    routingRule = new Mercury.Client.Core.Work.RoutingRule (MercuryApplication);

                }

                InitializeAll ();

                Session[SessionCachePrefix + "RoutingRule"] = routingRule;

                Session[SessionCachePrefix + "RoutingRuleUnmodified"] = routingRule.Copy ();

                #endregion

            } // Initial Page Load

            else { // Postback

                routingRule = (Mercury.Client.Core.Work.RoutingRule)Session[SessionCachePrefix + "RoutingRule"];

            }

            SaveResponseLabel.Text = String.Empty;

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

            InitializeRoutingRulesPage ();

            InitializeRoutingRulesPageStateCityCountySelection ();

            InitializeRoutingRulesGrid ();

            return;

        }

        protected void InitializeGeneralPage () {

            if (!String.IsNullOrEmpty (routingRule.Name)) { Page.Title = "RoutingRule - " + routingRule.Name; } else { Page.Title = "New RoutingRule"; }

            RoutingRuleName.Text = routingRule.Name;

            RoutingRuleDescription.Text = routingRule.Description;


            RoutingRuleEnabled.Checked = routingRule.Enabled;

            RoutingRuleVisible.Checked = routingRule.Visible;


            RoutingRuleDefaultWorkQueue.Items.Clear ();

            RoutingRuleDefaultWorkQueue.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** Do Not Route", "0"));

            foreach (Client.Core.Work.WorkQueue currentWorkQueue in MercuryApplication.WorkQueuesAvailable (false)) {

                if (currentWorkQueue.Enabled) {

                    RoutingRuleDefaultWorkQueue.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentWorkQueue.Name, currentWorkQueue.Id.ToString ()));

                }

            }

            RoutingRuleDefaultWorkQueue.SelectedValue = routingRule.DefaultWorkQueueId.ToString ();


            RoutingRuleCreateAuthorityName.Text = routingRule.CreateAccountInfo.SecurityAuthorityName;

            RoutingRuleCreateAccountId.Text = routingRule.CreateAccountInfo.UserAccountId;

            RoutingRuleCreateAccountName.Text = routingRule.CreateAccountInfo.UserAccountName;

            RoutingRuleCreateDate.MinDate = DateTime.MinValue;

            RoutingRuleCreateDate.SelectedDate = routingRule.CreateAccountInfo.ActionDate;


            RoutingRuleModifiedAuthorityName.Text = routingRule.ModifiedAccountInfo.SecurityAuthorityName;

            RoutingRuleModifiedAccountId.Text = routingRule.ModifiedAccountInfo.UserAccountId;

            RoutingRuleModifiedAccountName.Text = routingRule.ModifiedAccountInfo.UserAccountName;

            RoutingRuleModifiedDate.MinDate = DateTime.MinValue;

            RoutingRuleModifiedDate.SelectedDate = routingRule.ModifiedAccountInfo.ActionDate;

            return;

        }

        protected void InitializeRoutingRulesPage () {

            System.Collections.Generic.Dictionary<Int64, String> insurerReference = MercuryApplication.CoreObjectDictionary ("Insurer");

            System.Collections.Generic.Dictionary<Int64, String> programReference = new System.Collections.Generic.Dictionary<Int64, String> ();

            System.Collections.Generic.Dictionary<Int64, String> benefitPlanReference = new System.Collections.Generic.Dictionary<Int64, String> ();


            RoutingRuleInsurerSelection.Items.Clear ();

            RoutingRuleProgramSelection.Items.Clear ();

            RoutingRuleBenefitPlanSelection.Items.Clear ();


            RoutingRuleInsurerSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* All Insurers", "0"));

            foreach (Int64 currentInsurerId in insurerReference.Keys) {

                RoutingRuleInsurerSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (insurerReference[currentInsurerId], currentInsurerId.ToString ()));

            }

            if (RoutingRuleInsurerSelection.SelectedItem != null) {

                programReference = MercuryApplication.ProgramDictionaryByInsurer (Int64.Parse (RoutingRuleInsurerSelection.SelectedItem.Value), true);

            }

            RoutingRuleProgramSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* All Programs", "0"));

            foreach (Int64 currentProgramId in programReference.Keys) {

                RoutingRuleProgramSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (programReference[currentProgramId], currentProgramId.ToString ()));

            }

            RoutingRuleBenefitPlanSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* All Benefit Plans", "0"));

            foreach (Int64 currentBenefitPlanId in benefitPlanReference.Keys) {

                RoutingRuleBenefitPlanSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (benefitPlanReference[currentBenefitPlanId], currentBenefitPlanId.ToString ()));

            }


            #region Ethnicity

            System.Collections.Generic.Dictionary<Int64, String> ethnicityReference = MercuryApplication.CoreObjectDictionary ("Ethnicity");

            RoutingRuleEthnicitySelection.Items.Clear ();

            RoutingRuleEthnicitySelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* All Ethnicities", "0"));

            foreach (Int64 currentEthnicityId in ethnicityReference.Keys) {

                String ethnicityName = ethnicityReference[currentEthnicityId];

                ethnicityName = ethnicityName.Replace ("/", " / ");

                if (ethnicityName.Length > 60) { ethnicityName = ethnicityName.Substring (0, 56) + " ..."; }

                RoutingRuleEthnicitySelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (ethnicityName, currentEthnicityId.ToString ()));

            }

            #endregion


            #region Language

            System.Collections.Generic.Dictionary<Int64, String> languageReference = MercuryApplication.CoreObjectDictionary ("Language");

            RoutingRuleLanguageSelection.Items.Clear ();

            RoutingRuleLanguageSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* All Languages", "0"));

            foreach (Int64 currentLanguageId in languageReference.Keys) {

                String languageName = languageReference[currentLanguageId];

                languageName = languageName.Replace ("/", " / ");

                if (languageName.Length > 60) { languageName = languageName.Substring (0, 56) + " ..."; }

                RoutingRuleLanguageSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (languageName, currentLanguageId.ToString ()));

            }

            #endregion


            #region Work Queues

            RoutingRuleWorkQueue.Items.Clear ();

            RoutingRuleWorkQueue.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** Do Not Route", "0"));

            foreach (Client.Core.Work.WorkQueue currentWorkQueue in MercuryApplication.WorkQueuesAvailable (false)) {

                if (currentWorkQueue.Enabled) {

                    RoutingRuleWorkQueue.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentWorkQueue.Name, currentWorkQueue.Id.ToString ()));

                }

            }

            #endregion


            return;

        }

        protected void InitializeRoutingRulesPageStateCityCountySelection () {

            if (RoutingRuleStateSelection.Items.Count == 0) {

                RoutingRuleStateSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* All", String.Empty));

                foreach (String currentState in MercuryApplication.StateReference (true)) {

                    RoutingRuleStateSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentState, currentState));

                }

                RoutingRuleCitySelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* All Cities", String.Empty));

                RoutingRuleCountySelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* All Counties", String.Empty));

            }

            return;

        }

        protected void InitializeRoutingRulesGrid () {

            System.Data.DataTable ruleTable = new System.Data.DataTable ();

            ruleTable.Columns.Add ("Sequence");

            ruleTable.Columns.Add ("Rule");

            ruleTable.Columns.Add ("WorkQueue");


            foreach (Int32 currentRuleSequence in routingRule.Rules.Keys) {

                Client.Core.Work.WorkQueue workQueue = MercuryApplication.WorkQueueGet (routingRule.Rules[currentRuleSequence].WorkQueueId, true);

                ruleTable.Rows.Add (

                    routingRule.Rules[currentRuleSequence].Sequence,

                    routingRule.Rules[currentRuleSequence].Description,

                    (workQueue != null) ? workQueue.Name : "Not Assigned"

                    );

            }

            RoutingRulesGrid.DataSource = ruleTable;

            RoutingRulesGrid.DataBind ();

            return;

        }

        protected void ApplySecurity () {

            Boolean hasManagePermission = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.RoutingRuleManage);

            RoutingRuleName.ReadOnly = !hasManagePermission;

            RoutingRuleDescription.ReadOnly = !hasManagePermission;

            RoutingRuleEnabled.Enabled = hasManagePermission;

            RoutingRuleVisible.Enabled = hasManagePermission;

            ButtonCancel.Visible = hasManagePermission;

            ButtonApply.Visible = hasManagePermission;

            return;

        }

        private void ValidateSession () {

            if (Session["Mercury.Application"] == null) { Server.Transfer ("/SessionExpired.aspx"); }

            return;

        }

        #endregion


        #region Control Events

        protected void RoutingRuleInsurerSelection_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            System.Collections.Generic.Dictionary<Int64, String> programReference = new System.Collections.Generic.Dictionary<Int64, String> ();


            if (RoutingRuleInsurerSelection.SelectedItem == null) { return; }


            RoutingRuleProgramSelection.Items.Clear ();

            programReference = MercuryApplication.ProgramDictionaryByInsurer (Int64.Parse (RoutingRuleInsurerSelection.SelectedItem.Value), true);

            RoutingRuleProgramSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* All Programs", "0"));

            foreach (Int64 currentProgramId in programReference.Keys) {

                RoutingRuleProgramSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (programReference[currentProgramId], currentProgramId.ToString ()));

            }

            return;

        }

        protected void RoutingRuleProgramSelection_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            System.Collections.Generic.Dictionary<Int64, String> benefitPlanReference = new System.Collections.Generic.Dictionary<Int64, String> ();


            if (RoutingRuleProgramSelection.SelectedItem == null) { return; }


            RoutingRuleBenefitPlanSelection.Items.Clear ();

            benefitPlanReference = MercuryApplication.BenefitPlanDictionaryByProgram (Int64.Parse (RoutingRuleProgramSelection.SelectedItem.Value), true);

            RoutingRuleBenefitPlanSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* All Benefit Plans", "0"));

            foreach (Int64 currentBenefitPlanId in benefitPlanReference.Keys) {

                RoutingRuleBenefitPlanSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (benefitPlanReference[currentBenefitPlanId], currentBenefitPlanId.ToString ()));

            }

            return;

        }

        protected void RoutingRuleStateSelection_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            System.Collections.Generic.Dictionary<Int64, String> programReference = new System.Collections.Generic.Dictionary<Int64, String> ();


            if (RoutingRuleStateSelection.SelectedItem == null) { return; }


            if (RoutingRuleStateSelection.SelectedValue == String.Empty) {

                InitializeRoutingRulesPageStateCityCountySelection ();

            }

            else {

                RoutingRuleCitySelection.Items.Clear ();

                RoutingRuleCitySelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* All Cities", String.Empty));

                foreach (Mercury.Server.Application.CityStateZipCodeView currentCity in MercuryApplication.CityReferenceByState (RoutingRuleStateSelection.SelectedValue, true)) {

                    RoutingRuleCitySelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentCity.City, currentCity.City));

                }


                RoutingRuleCountySelection.Items.Clear ();

                RoutingRuleCountySelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* All Counties", String.Empty));

                foreach (String currentCounty in MercuryApplication.CountyReferenceByState (RoutingRuleStateSelection.SelectedValue)) {

                    RoutingRuleCountySelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentCounty, currentCounty));

                }

            }

            return;

        }

        protected void ButtonAddRule_OnClick (Object Sender, EventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            Client.Core.Work.RoutingRuleDefinition newRule = new Mercury.Client.Core.Work.RoutingRuleDefinition (MercuryApplication);

            Int32 ageValue;


            SaveResponseLabel.Text = String.Empty;


            newRule.InsurerId = Convert.ToInt64 (RoutingRuleInsurerSelection.SelectedValue);

            newRule.ProgramId = Convert.ToInt64 (RoutingRuleProgramSelection.SelectedValue);

            newRule.BenefitPlanId = Convert.ToInt64 (RoutingRuleBenefitPlanSelection.SelectedValue);

            newRule.Gender = (Server.Application.Gender)Convert.ToInt32 (RoutingRuleGender.SelectedValue);


            if ((Int32.TryParse (RoutingRuleAgeMinimum.Text, out ageValue)) || (Int32.TryParse (RoutingRuleAgeMaximum.Text, out ageValue))) {

                newRule.UseAgeCriteria = true;

                if (Int32.TryParse (RoutingRuleAgeMinimum.Text, out ageValue)) { newRule.AgeMinimum = ageValue; }

                if (Int32.TryParse (RoutingRuleAgeMaximum.Text, out ageValue)) { newRule.AgeMaximum = ageValue; }

                newRule.IsAgeInMonths = RoutingRuleAgeInMonths.Checked;

            }

            else { newRule.UseAgeCriteria = false; }


            newRule.EthnicityId = Convert.ToInt64 (RoutingRuleEthnicitySelection.SelectedValue);

            newRule.LanguageId = Convert.ToInt64 (RoutingRuleLanguageSelection.SelectedValue);


            newRule.State = RoutingRuleStateSelection.SelectedValue;

            newRule.City = RoutingRuleCitySelection.SelectedValue;

            newRule.County = RoutingRuleCountySelection.SelectedValue;

            newRule.ZipCode = RoutingRuleZipCode.Text;


            if (RoutingRuleWorkQueue.SelectedItem != null) { newRule.WorkQueueId = Convert.ToInt64 (RoutingRuleWorkQueue.SelectedValue); }


            Dictionary<String, String> validationResponse;

            validationResponse = newRule.Validate ();

            if (validationResponse.Count == 0) {

                if (routingRule.RuleExists (newRule)) {

                    SaveResponseLabel.Text = "Invalid Rule: Duplicate Found";

                }

                else {

                    routingRule.AppendRule (newRule);

                }

            }

            else {

                foreach (String validationKey in validationResponse.Keys) {

                    SaveResponseLabel.Text = "Invalid [" + validationKey + "]: " + validationResponse[validationKey];

                    break;

                }

            }

            InitializeRoutingRulesGrid ();

            return;

        }

        protected void RoutingRulesGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            Int32 deleteIndex = eventArgs.Item.DataSetIndex;

            routingRule.Rules.RemoveAt (deleteIndex);


            SortedList<Int32, Client.Core.Work.RoutingRuleDefinition> newRules = new SortedList<int, Mercury.Client.Core.Work.RoutingRuleDefinition> ();

            Int32 newSequence = 0;

            foreach (Int32 currentSequence in routingRule.Rules.Keys) {

                newSequence = newSequence + 1;

                newRules.Add (newSequence, routingRule.Rules[currentSequence].Copy ());

                newRules[newSequence].Sequence = newSequence;

            }

            routingRule.Rules = newRules;


            InitializeRoutingRulesGrid ();

            return;

        }

        protected void RoutingRulesGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            Int32 itemIndex = eventArgs.Item.ItemIndex;

            Int32 sequence;

            String direction = eventArgs.CommandName;

            SortedList<Int32, Client.Core.Work.RoutingRuleDefinition> newRules = new SortedList<int, Mercury.Client.Core.Work.RoutingRuleDefinition> ();


            switch (direction.ToLowerInvariant ()) {

                case "moveup":

                    if (itemIndex != 0) {

                        for (Int32 currentIndex = 0; currentIndex < (itemIndex - 1); currentIndex++) {

                            sequence = currentIndex + 1;

                            newRules.Add (sequence, routingRule.Rules[sequence].Copy ());

                        }

                        // SWITCH THE TWO SPOTS

                        sequence = itemIndex + 1;

                        newRules.Add (itemIndex, routingRule.Rules[sequence].Copy ());

                        newRules[itemIndex].Sequence = itemIndex;

                        sequence = itemIndex;

                        newRules.Add (itemIndex + 1, routingRule.Rules[sequence].Copy ());

                        newRules[itemIndex + 1].Sequence = itemIndex + 1;


                        for (Int32 currentIndex = itemIndex + 1; currentIndex < routingRule.Rules.Count; currentIndex++) {

                            sequence = currentIndex + 1;

                            newRules.Add (sequence, routingRule.Rules[sequence].Copy ());

                        }

                        routingRule.Rules = newRules;

                    }

                    break;

                case "movedown":

                    if (itemIndex != (routingRule.Rules.Count - 1)) {

                        for (Int32 currentIndex = 0; currentIndex <= (itemIndex - 1); currentIndex++) {

                            sequence = currentIndex + 1;

                            newRules.Add (sequence, routingRule.Rules[sequence].Copy ());

                        }


                        // I = 1 / S = 2
                        // I = 2 / S = 3

                        // I = 1 / S = 3
                        //       / S = 2


                        // SWITCH THE TWO SPOTS

                        sequence = itemIndex + 1;

                        newRules.Add (sequence + 1, routingRule.Rules[sequence].Copy ());

                        newRules[sequence + 1].Sequence = itemIndex + 2;


                        sequence = itemIndex + 2;

                        newRules.Add (sequence - 1, routingRule.Rules[sequence].Copy ());

                        newRules[sequence - 1].Sequence = sequence - 1;


                        for (Int32 currentIndex = itemIndex + 2; currentIndex < routingRule.Rules.Count; currentIndex++) {

                            sequence = currentIndex + 1;

                            newRules.Add (sequence, routingRule.Rules[sequence].Copy ());

                        }

                        routingRule.Rules = newRules;

                    }

                    break;

            }


            InitializeRoutingRulesGrid ();

            return;

        }


        //protected void RoutingRuleCitySelection_OnItemsRequested (Object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs eventArgs) {

        //    Telerik.Web.UI.RadComboBox controlComboBox = (Telerik.Web.UI.RadComboBox) sender;


        //    if (MercuryApplication == null) { return; }

        //    List<String> cityReference = MercuryApplication.CityReferenceByState (RoutingRuleStateSelection.Text, eventArgs.Text);

        //    foreach (String cityName in cityReference) {

        //        RoutingRuleCitySelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (cityName, cityName));

        //    }

        //    eventArgs.EndOfItems = true;

        //    eventArgs.Message = (eventArgs.EndOfItems) ? "End of List" : "More Available";

        //    return;

        //}

        //protected void RoutingRuleStateSelection_OnTextChanged (Object sender, EventArgs eventArgs) {

        //    Telerik.Web.UI.RadComboBox controlComboBox = (Telerik.Web.UI.RadComboBox) sender;


        //    if (MercuryApplication == null) { return; }


        //    RoutingRuleCitySelection.Text = String.Empty;

        //    RoutingRuleZipCode.Text = String.Empty;

        //    return;

        //}

        //protected void RoutingRuleZipCode_OnTextChanged (Object sender, EventArgs eventArgs) {

        //    Telerik.Web.UI.RadMaskedTextBox controlZipCode = (Telerik.Web.UI.RadMaskedTextBox) sender;


        //    if (MercuryApplication == null) { return; }


        //    RoutingRuleCitySelection.Text = MercuryApplication.CityReferenceByZipCode (controlZipCode.Text);

        //    RoutingRuleStateSelection.Text = MercuryApplication.StateReferenceByZipCode (controlZipCode.Text);


        //    return;

        //}

        #endregion


        #region Dialog Button Event Handlers

        protected Boolean ApplyChanges () {

            Boolean success = false;

            Boolean isModified = false;

            Boolean isValid = false;

            Dictionary<String, String> validationResponse;


            ValidateSession ();

            Mercury.Client.Core.Work.RoutingRule routingRuleUnmodified = (Mercury.Client.Core.Work.RoutingRule)Session[SessionCachePrefix + "RoutingRuleUnmodified"];



            routingRule.Name = RoutingRuleName.Text;

            routingRule.Description = RoutingRuleDescription.Text;

            routingRule.DefaultWorkQueueId = (RoutingRuleDefaultWorkQueue.SelectedItem != null) ? Convert.ToInt64 (RoutingRuleDefaultWorkQueue.SelectedItem.Value) : 0;

            routingRule.Enabled = RoutingRuleEnabled.Checked;

            routingRule.Visible = RoutingRuleVisible.Checked;


            if (routingRuleUnmodified.Id == 0) { isModified = true; }

            if (!isModified) { isModified = !routingRule.IsEqual (routingRuleUnmodified); }

            validationResponse = routingRule.Validate ();

            isValid = (validationResponse.Count == 0);


            if ((isModified) && (isValid)) {

                if (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.RoutingRuleManage)) {

                    SaveResponseLabel.Text = "Permission Denied.";

                    return false;

                }

                success = MercuryApplication.RoutingRuleSave (routingRule);

                if (success) {

                    routingRule = MercuryApplication.RoutingRuleGet (routingRule.Id);

                    Session[SessionCachePrefix + "RoutingRule"] = routingRule;

                    Session[SessionCachePrefix + "RoutingRuleUnmodified"] = routingRule.Copy ();

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