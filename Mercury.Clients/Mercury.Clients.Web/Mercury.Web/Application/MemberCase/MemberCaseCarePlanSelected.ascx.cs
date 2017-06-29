using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.MemberCase {

    public partial class MemberCaseCarePlanSelected : System.Web.UI.UserControl {

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

        public Client.Core.Individual.Case.MemberCaseCarePlan MemberCaseCarePlan {

            get { return (Client.Core.Individual.Case.MemberCaseCarePlan)Session[SessionCachePrefix + this.ClientID + "MemberCaseCarePlan"]; }

            set {

                Client.Core.Individual.Case.MemberCaseCarePlan carePlan = (Client.Core.Individual.Case.MemberCaseCarePlan)Session[SessionCachePrefix + this.ClientID + "MemberCaseCarePlan"];

                carePlan = value;

                Session[SessionCachePrefix + this.ClientID + "MemberCaseCarePlan"] = value;

                InitializeCarePlan ();

            }

        }

        public MemberCase ParentMemberCasePage { get { return (MemberCase)Page; } }

        private List<Client.Core.Individual.Case.MemberCaseCareIntervention> DataSourceCarePlanInterventionGrid {

            get {

                if (MemberCaseCarePlan == null) { return new List<Client.Core.Individual.Case.MemberCaseCareIntervention> (); }

                List<Client.Core.Individual.Case.MemberCaseCareIntervention> careInterventions = new List<Client.Core.Individual.Case.MemberCaseCareIntervention> ();

                foreach (Client.Core.Individual.Case.MemberCaseCarePlanGoal currentCarePlanGoal in MemberCaseCarePlan.Goals) {

                    foreach (Client.Core.Individual.Case.MemberCaseCarePlanGoalIntervention currentCarePlanGoalIntervention in currentCarePlanGoal.Interventions) {

                        if (currentCarePlanGoalIntervention.CareIntervention != null) {

                            careInterventions.Add (currentCarePlanGoalIntervention.CareIntervention);

                        }

                    } /* END FOREACH */

                } /* END FOREACH */

                careInterventions = (from currentCareIntervention in careInterventions orderby currentCareIntervention.Name select currentCareIntervention).Distinct ().ToList ();

                return careInterventions;

            }

        }

        //private List<Client.Core.Individual.Case.MemberCaseCareIntervention> DataSourceCarePlanInterventionGrid {

        //    get {

        //        if (MemberCaseCarePlan == null) { return new List<Client.Core.Individual.Case.MemberCaseCareIntervention> (); }

        //        List<Client.Core.Individual.Case.MemberCaseCareIntervention> careInterventions =

        //             (from currentCarePlanGoal in MemberCaseCarePlan.Goals

        //              from currentCarePlanGoalIntervention in currentCarePlanGoal.Interventions

        //              /* TODO: DAVID: WILL CRASH WHEN CARE INTERVENTION IS NULL */

        //              orderby currentCarePlanGoalIntervention.CareIntervention.Name

        //              //orderby (currentCarePlanGoalIntervention.CareIntervention != null) ? currentCarePlanGoalIntervention.CareIntervention.Name : currentCarePlanGoalIntervention.Name

        //              select currentCarePlanGoalIntervention.CareIntervention).Distinct ().ToList ();

        //        return careInterventions;

        //    }

        //}


        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            return;

        }

        #endregion 

    
        #region Initializations

        private void InitializeCarePlan () {

            if (MemberCaseCarePlan == null) { return; }


            MemberCaseCarePlanStatus.Text = MemberCaseCarePlan.StatusDescription;

            PeformAssessmentHyperLink.NavigateUrl = "/Application/MemberCase/Actions/MemberCaseCarePlanAssessment.aspx?MemberCaseId=" + MemberCaseCarePlan.MemberCaseId.ToString () + "&MemberCaseCarePlanId=" + MemberCaseCarePlan.Id.ToString ();


            // INITIALIZE SELECTED CARE PLAN SEVERITY RAD COMBO BOX WITH ENBALED VISIBLE CARE LEVELS

            InitializeSelectedCarePlanSeverity ();

            //System.Web.UI.HtmlControls.HtmlControl titlePanel = (System.Web.UI.HtmlControls.HtmlControl)FindControl ("TitlePanel_" + carePlanGoal.MemberCaseCarePlan.Status.ToString ());

            //if (titlePanel != null) { titlePanel.Visible = true; }


            //// MAP PROPERTY VALUES INTO CONTROLS 

            //CarePlanGoalEditName.Text = CarePlanGoalName.Text = carePlanGoal.Name;

            //// CARE PLAN GOAL STATUS

            //CarePlanGoalEditClinicalNarrative.Text = CarePlanGoalClinicalNarrative.Text = carePlanGoal.ClinicalNarrative;

            //CarePlanGoalEditCommonNarrative.Text = CarePlanGoalCommonNarrative.Text = carePlanGoal.CommonNarrative;

            //CarePlanGoalMeasureName.Text = carePlanGoal.CareMeasureName;

            //CarePlanGoalMeasureName.ToolTip = (carePlanGoal.CareMeasure != null) ? carePlanGoal.CareMeasure.Description : String.Empty;


            //// EDIT PANELS BASED ON CARE PLAN GOAL STATUS (GOAL STATUS IS MORE DETAILED THAN CARE PLAN STATUS)

            //System.Web.UI.HtmlControls.HtmlControl carePlanGoalEditPanel = (System.Web.UI.HtmlControls.HtmlControl)FindControl ("CarePlanGoalEditPanel_" + carePlanGoal.MemberCaseCarePlan.Status.ToString ());

            //if (carePlanGoalEditPanel != null) { carePlanGoalEditPanel.Visible = true; }


            //#region EDIT PANEL - UNDER DEVELOPMENT

            //CarePlanGoalTimeframeSelection.SelectedValue = ((Int32)carePlanGoal.GoalTimeframe).ToString ();

            //CarePlanGoalScheduleValue.Value = carePlanGoal.ScheduleValue;

            //CarePlanGoalScheduleQualifierSelection.SelectedValue = ((Int32)carePlanGoal.ScheduleQualifier).ToString ();

            //CarePlanGoalCareMeasureSelection.DataSource = MercuryApplication.CareMeasuresAvailable (true);

            //CarePlanGoalCareMeasureSelection.DataTextField = "Name";

            //CarePlanGoalCareMeasureSelection.DataValueField = "Id";

            //CarePlanGoalCareMeasureSelection.SelectedValue = carePlanGoal.CareMeasureId.ToString ();

            //#endregion


            CarePlanGoalGrid.DataSource = MemberCaseCarePlan.Goals;

            CarePlanGoalGrid.DataBind ();


            CarePlanInterventionGrid.DataSource = DataSourceCarePlanInterventionGrid;

            CarePlanInterventionGrid.DataBind ();

            return;

        }

        private void InitializeSelectedCarePlanSeverity () {

            // GET REFERENCE TO CURRENT CASE CARE LEVEL SEVERITY SELECTION RAD COMBO BOX

            Telerik.Web.UI.RadComboBox currentCaseCareLevelSeveritySelection = (Telerik.Web.UI.RadComboBox) CaseCareLevelSeveritySelection;

            // ADD NOT SPECIFIED SELECTED CARE PLAN SEVERITY RAD COMBO BOX ITEM

            Telerik.Web.UI.RadComboBoxItem notSpecifiedRadComboBoxItem = new Telerik.Web.UI.RadComboBoxItem ("** Not Specified", "0");

            // ADD NEW RAD COMBO BOX ITEM TO ITEMS OF CURRENT CASE CARE LEVEL SEVERITY SELECTION RAD COMBO BOX

            currentCaseCareLevelSeveritySelection.Items.Add (notSpecifiedRadComboBoxItem);

            // LOOP THROUGH EACH CARE LEVEL IN CARE LEVELS AVAILABLE

            foreach (Client.Core.Individual.CareLevel currentCareLevel in MercuryApplication.CareLevelsAvailable (false)) {

                // IF CURRENT CARE LEVEL IS ENABLED AND VISIBLE, THEN CREATE RAD COMBO BOX ITEM FOR CURRENT CARE LEVEL AND ADD TO ITEMS OF CURRENT CASE CARE LEVEL SEVERITY SELECTION RAD COMBO BOX

                if (currentCareLevel.Enabled && currentCareLevel.Visible) {

                    // CREATE REFERENCE TO NEW RAD COMBO BOX ITEM WITH VALUE AS ID OF CURRENT CARE LEVEL AND TEXT AS NAME OF CURRENT CARE LEVEL

                    Telerik.Web.UI.RadComboBoxItem newRadComboBoxItem = new Telerik.Web.UI.RadComboBoxItem (currentCareLevel.Name, currentCareLevel.Id.ToString ());

                    // ADD NEW RAD COMBO BOX ITEM TO ITEMS OF CURRENT CASE CARE LEVEL SEVERITY SELECTION RAD COMBO BOX

                    currentCaseCareLevelSeveritySelection.Items.Add (newRadComboBoxItem);

                }

            } /* END FOREACH */

            // SET CURRENT CASE LEVEL SEVERITY OF SELECTED MEMBER CASE CARE PLAN

            //String severityValue = MemberCaseCarePlan.Severity.Value.ToString ();

            //String severityName = MemberCaseCarePlan.Severity.Text;

            

            return;

        }

        #endregion 


        #region Care Plan Goal Grid Events

        protected void CarePlanGoalGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e) {

            if (MemberCaseCarePlan == null) { return; }


            switch (e.RebindReason) {

                case Telerik.Web.UI.GridRebindReason.InitialLoad:

                case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                    CarePlanGoalGrid.DataSource = MemberCaseCarePlan.Goals;

                    break;

                case Telerik.Web.UI.GridRebindReason.PostBackEvent:

                    break;

            }

            return;

        }

        protected void CarePlanGoalGrid_OnItemCreated (Object sender, Telerik.Web.UI.GridItemEventArgs e) {

            if ((e.Item is Telerik.Web.UI.GridEditableItem) && (e.Item.IsInEditMode)) {

                if (MemberCaseCarePlan == null) { return; }

                Telerik.Web.UI.GridEditableItem item = (Telerik.Web.UI.GridEditableItem)e.Item;


                Telerik.Web.UI.RadComboBox AddCarePlanGoalExistingSelection = (Telerik.Web.UI.RadComboBox)item.FindControl ("AddCarePlanGoalExistingSelection");

                if (AddCarePlanGoalExistingSelection == null) { return; }

                AddCarePlanGoalExistingSelection.DataSource = MemberCaseCarePlan.CarePlan.Goals;

                AddCarePlanGoalExistingSelection.DataTextField = "Name";

                AddCarePlanGoalExistingSelection.DataValueField = "Id";


                Telerik.Web.UI.RadComboBox AddCarePlanGoalCareMeasureSelection = (Telerik.Web.UI.RadComboBox)item.FindControl ("AddCarePlanGoalCareMeasureSelection");

                if (AddCarePlanGoalCareMeasureSelection == null) { return; }

                AddCarePlanGoalCareMeasureSelection.DataSource = MercuryApplication.CareMeasuresAvailableEnabledVisible (true);

                AddCarePlanGoalCareMeasureSelection.DataTextField = "Name";

                AddCarePlanGoalCareMeasureSelection.DataValueField = "Id";

            }

            return;

        }

        protected void CarePlanGoalGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs e) {

            if (MemberCaseCarePlan == null) { return; }

            Mercury.Server.Application.MemberCaseModificationResponse response;

            switch (e.CommandName) {

                case Telerik.Web.UI.RadGrid.InitInsertCommandName:

                    break;

                case Telerik.Web.UI.RadGrid.PerformInsertCommandName:

                    #region Perform Insert of New Goal


                    // RETREIVE REFERENCES TO ALL TEMPLATED CONTROLS

                    RadioButtonList AddCarePlanGoalTypeSelection = (RadioButtonList)e.Item.FindControl ("AddCarePlanGoalTypeSelection");

                    if (AddCarePlanGoalTypeSelection == null) { return; }

                    Telerik.Web.UI.RadTextBox AddCarePlanGoalName = (Telerik.Web.UI.RadTextBox)e.Item.FindControl ("AddCarePlanGoalName");

                    if (AddCarePlanGoalName == null) { return; }

                    Telerik.Web.UI.RadComboBox AddCarePlanGoalExistingSelection = (Telerik.Web.UI.RadComboBox)e.Item.FindControl ("AddCarePlanGoalExistingSelection");

                    if (AddCarePlanGoalExistingSelection == null) { return; }

                    Telerik.Web.UI.RadComboBox AddCarePlanGoalCareMeasureSelection = (Telerik.Web.UI.RadComboBox)e.Item.FindControl ("AddCarePlanGoalCareMeasureSelection");

                    if (AddCarePlanGoalCareMeasureSelection == null) { return; }



                    Int64 selectedBaselineGoalId = (AddCarePlanGoalTypeSelection.SelectedValue == "0") ? Convert.ToInt64 (AddCarePlanGoalExistingSelection.SelectedValue) : 0;

                    Int64 selectedCareMeasureId = (AddCarePlanGoalTypeSelection.SelectedValue == "1") ? Convert.ToInt64 (AddCarePlanGoalCareMeasureSelection.SelectedValue) : 0;

                    String carePlanGoalName = (AddCarePlanGoalTypeSelection.SelectedValue == "1") ? AddCarePlanGoalName.Text : String.Empty;


                    response = MercuryApplication.MemberCaseCarePlanGoal_Add (ParentMemberCasePage.Case, MemberCaseCarePlan.Id, selectedBaselineGoalId, carePlanGoalName, selectedCareMeasureId);

                    if (response.HasException) { ParentMemberCasePage.ExceptionMessage = response.Exception.Message; }

                    else { ParentMemberCasePage.Case = new Client.Core.Individual.Case.MemberCase (MercuryApplication, response.MemberCase); }
                    
                    CarePlanGoalGrid.DataSource = null;

                    CarePlanGoalGrid.Rebind ();

                    #endregion

                    break;

                case Telerik.Web.UI.RadGrid.DeleteCommandName:

                    response = MercuryApplication.MemberCaseCarePlanGoal_Delete (ParentMemberCasePage.Case, MemberCaseCarePlan.Goals [e.Item.ItemIndex].Id);

                    if (response.HasException) { ParentMemberCasePage.ExceptionMessage = response.Exception.Message; }

                    else { ParentMemberCasePage.Case = new Client.Core.Individual.Case.MemberCase (MercuryApplication, response.MemberCase); }

                    CarePlanGoalGrid.DataSource = null;

                    CarePlanGoalGrid.Rebind ();

                    break;

                case Telerik.Web.UI.RadGrid.CancelCommandName:

                case Telerik.Web.UI.RadGrid.RebindGridCommandName:
                    
                    CarePlanGoalGrid.DataSource = MemberCaseCarePlan.Goals;

                    CarePlanGoalGrid.DataBind ();

                    break;

                default:

                    break;

            }

            return;

        }

        #endregion 
        

        #region Care Plan Intervention Grid Events

        protected void CarePlanInterventionGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e) {

            if (MemberCaseCarePlan == null) { return; }


            switch (e.RebindReason) {

                case Telerik.Web.UI.GridRebindReason.InitialLoad:

                case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                    //List<Client.Core.Individual.Case.MemberCaseCareIntervention> careInterventions =

                    //     (from currentCarePlanGoal in MemberCaseCarePlan.Goals

                    //      from currentCarePlanGoalIntervention in currentCarePlanGoal.Interventions

                    //      orderby currentCarePlanGoalIntervention.CareIntervention.Name

                    //      select currentCarePlanGoalIntervention.CareIntervention).Distinct ().ToList ();

                    CarePlanInterventionGrid.DataSource = DataSourceCarePlanInterventionGrid;

                    break;

                case Telerik.Web.UI.GridRebindReason.PostBackEvent:

                    break;

            }

            return;

        }

        protected void CarePlanInterventionGrid_OnItemCreated (Object sender, Telerik.Web.UI.GridItemEventArgs e) {

            if ((e.Item is Telerik.Web.UI.GridEditableItem) && (e.Item.IsInEditMode)) {

                if (MemberCaseCarePlan == null) { return; }

                Telerik.Web.UI.GridEditableItem item = (Telerik.Web.UI.GridEditableItem)e.Item;


                Telerik.Web.UI.RadComboBox AddCarePlanInterventionExistingSelection = (Telerik.Web.UI.RadComboBox)item.FindControl ("AddCarePlanInterventionExistingSelection");

                if (AddCarePlanInterventionExistingSelection == null) { return; }

                AddCarePlanInterventionExistingSelection.DataSource = MercuryApplication.CareInterventionsAvailable (true);

                AddCarePlanInterventionExistingSelection.DataTextField = "Name";

                AddCarePlanInterventionExistingSelection.DataValueField = "Id";


                Telerik.Web.UI.RadComboBox AddCarePlanInterventionCareMeasureSelection = (Telerik.Web.UI.RadComboBox)item.FindControl ("AddCarePlanInterventionCareMeasureSelection");

                if (AddCarePlanInterventionCareMeasureSelection == null) { return; }

                AddCarePlanInterventionCareMeasureSelection.DataSource = MercuryApplication.CareMeasuresAvailableEnabledVisible (true);

                AddCarePlanInterventionCareMeasureSelection.DataTextField = "Name";

                AddCarePlanInterventionCareMeasureSelection.DataValueField = "Id";

            }

            return;

        }

        protected void CarePlanInterventionGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs e) {

            if (MemberCaseCarePlan == null) { return; }

            Mercury.Server.Application.MemberCaseModificationResponse response;

            switch (e.CommandName) {

                case Telerik.Web.UI.RadGrid.InitInsertCommandName:

                    break;

                case Telerik.Web.UI.RadGrid.PerformInsertCommandName:

                    #region Perform Insert of New Intervention


                    // RETREIVE REFERENCES TO ALL TEMPLATED CONTROLS

                    RadioButtonList AddCarePlanInterventionTypeSelection = (RadioButtonList)e.Item.FindControl ("AddCarePlanInterventionTypeSelection");

                    if (AddCarePlanInterventionTypeSelection == null) { return; }

                    Telerik.Web.UI.RadTextBox AddCarePlanInterventionName = (Telerik.Web.UI.RadTextBox)e.Item.FindControl ("AddCarePlanInterventionName");

                    if (AddCarePlanInterventionName == null) { return; }

                    Telerik.Web.UI.RadComboBox AddCarePlanInterventionExistingSelection = (Telerik.Web.UI.RadComboBox)e.Item.FindControl ("AddCarePlanInterventionExistingSelection");

                    if (AddCarePlanInterventionExistingSelection == null) { return; }


                    Int64 selectedBaselineInterventionId = (AddCarePlanInterventionTypeSelection.SelectedValue == "0") ? Convert.ToInt64 (AddCarePlanInterventionExistingSelection.SelectedValue) : 0;

                    String carePlanInterventionName = (AddCarePlanInterventionTypeSelection.SelectedValue == "1") ? AddCarePlanInterventionName.Text : String.Empty;


                    response = MercuryApplication.MemberCaseCarePlanIntervention_Add (ParentMemberCasePage.Case, MemberCaseCarePlan.Id, selectedBaselineInterventionId, carePlanInterventionName);

                    if (response.HasException) { ParentMemberCasePage.ExceptionMessage = response.Exception.Message; }

                    else { ParentMemberCasePage.Case = new Client.Core.Individual.Case.MemberCase (MercuryApplication, response.MemberCase); }


                    #endregion

                    break;

                case Telerik.Web.UI.RadGrid.DeleteCommandName:

                    //response = MercuryApplication.MemberCaseCarePlanIntervention_Delete (ParentMemberCasePage.Case, CarePlan.Interventions[e.Item.ItemIndex].Id);

                    //if (response.HasException) { ParentMemberCasePage.ExceptionMessage = response.Exception.Message; }

                    //else { ParentMemberCasePage.Case = new Client.Core.Individual.Case.MemberCase (MercuryApplication, response.MemberCase); }

                    break;

                case Telerik.Web.UI.RadGrid.CancelCommandName:

                case Telerik.Web.UI.RadGrid.RebindGridCommandName:

                    //CarePlanInterventionGrid.DataSource = CarePlan.Interventions;

                    //CarePlanInterventionGrid.DataBind ();

                    CarePlanInterventionGrid.Rebind ();

                    break;

                default:

                    break;

            }

            return;

        }

        #endregion


        #region Property Changes

        public void CaseCareLevelSeveritySaveLink_OnClick (Object sender, EventArgs e) {

            // GET REFERENCE TO CURRENT LINK BUTTON WEB CONTROL SAVE LINK FROM SENDER OBJECT

            System.Web.UI.WebControls.LinkButton currentSaveLink = (System.Web.UI.WebControls.LinkButton)sender;

            // GET REFERENCE TO CURRENT WEB CONTROL FROM PARENT OF CURRENT SAVE LINK LINK BUTTON WEB CONTROL

            System.Web.UI.Control currentWebControl = (System.Web.UI.Control)currentSaveLink.Parent;

            // GET REFERENCE TO CURRENT CASE CARE LEVEL SEVERITY CHANGE SELECTION RAD COMBO BOX

            Telerik.Web.UI.RadComboBox currentCaseCareLevelSeverityChangeSelection = (Telerik.Web.UI.RadComboBox)currentWebControl.FindControl ("CaseCareLevelSeveritySelection");

            // CREATE REFERENCE TO PARSED SELECTED CARE LEVEL ID

            Int64 parsedselectedCareLevelId;

            // TRY TO PARSE SELECTED VALUE OF CURRENT CASE CARE LEVEL SEVERITY CHANGE SELECTION RAD COMBO BOX AS INT 64

            if (Int64.TryParse (currentCaseCareLevelSeverityChangeSelection.SelectedValue, out parsedselectedCareLevelId)) {

                // SET SELECTED CARE LEVEL ID AS INT 64 OF SELECTED VALUE OF CURRENT CASE CARE LEVEL SEVERITY CHANGE SELECTION RAD COMBO BOX

                Int64 selectedCareLevelId = Convert.ToInt64 (currentCaseCareLevelSeverityChangeSelection.SelectedValue);

            }

            // SET MEMBER CASE CARE PLAN SEVERITY LEVEL

            /* TODO: DAVID: SEVERITY (CARE LEVEL) IS NOT YET A PROPERTY OF MEMEBR CASE CARE PLAN IN CODE OR DATABASE */

            return;

        }

        #endregion

    }

}