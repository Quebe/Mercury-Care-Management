using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.MemberCase.Actions {
    public partial class MemberCaseCarePlanAssessment : System.Web.UI.Page {

        #region Private Properties

        private Boolean isPageUnloading = false;

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

                if ((application == null) && (!isPageUnloading)) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        public Client.Core.Individual.Case.MemberCase Case {

            get { return (Client.Core.Individual.Case.MemberCase)Session[SessionCachePrefix + "MemberCase"]; }

            set {

                Client.Core.Individual.Case.MemberCase memberCase = (Client.Core.Individual.Case.MemberCase)Session[SessionCachePrefix + "MemberCase"];

                if (memberCase != value) {

                    memberCase = value;

                    Session[SessionCachePrefix + "MemberCase"] = value;

                }

            }

        }

        public Int64 MemberCaseCarePlanId {

            get {

                if (Session[SessionCachePrefix + "MemberCaseCarePlanId"] == null) { return 0; }

                return (Int64)Session[SessionCachePrefix + "MemberCaseCarePlanId"];

            }

            set {

                if (MemberCaseCarePlanId != value) {

                    Session[SessionCachePrefix + "MemberCaseCarePlanId"] = value;

                }

            }

        }

        public Client.Core.Individual.Case.MemberCaseCarePlan MemberCaseCarePlan { get { return Case.FindMemberCaseCarePlan (MemberCaseCarePlanId); } }

        public Client.Core.Individual.Case.MemberCaseCarePlanAssessment Assessment {

            get { return (Client.Core.Individual.Case.MemberCaseCarePlanAssessment)Session[SessionCachePrefix + "Assessment"]; }

            set {

                Client.Core.Individual.Case.MemberCaseCarePlanAssessment assessment = (Client.Core.Individual.Case.MemberCaseCarePlanAssessment)Session[SessionCachePrefix + "Assessment"];

                if (assessment != value) {

                    assessment = value;

                    Session[SessionCachePrefix + "Assessment"] = value;

                }

            }

        }
        

        public String ExceptionMessage {

            get { return ExceptionMessageLabel.Text; }

            set {

                ExceptionMessageRow.Style.Clear ();

                if (!String.IsNullOrWhiteSpace (value)) {

                    // UNSUCCESSFUL UPDATE

                    ExceptionMessageLabel.Text = value;

                }

                else {

                    ExceptionMessageRow.Style.Add ("display", "none");

                }

            }

        }

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            Int64 forMemberCaseId = 0;

            Int64 forMemberCaseCarePlanId = 0;


            if (MercuryApplication == null) { return; }


            if (!Page.IsPostBack) {

                #region Initial Page Load

                if (Request.QueryString["MemberCaseId"] != null) {

                    if (Int64.TryParse ((String)Request.QueryString["MemberCaseId"], out forMemberCaseId)) {

                        Case = MercuryApplication.MemberCaseGet (forMemberCaseId, false);

                    }

                }

                if (Request.QueryString["MemberCaseCarePlanId"] != null) {

                    if (!Int64.TryParse ((String)Request.QueryString["MemberCaseCarePlanId"], out forMemberCaseCarePlanId)) {

                        Server.Transfer ("/PermissionDenied.aspx"); return;

                    }

                }

                if (Case == null) { Server.Transfer ("/PermissionDenied.aspx"); return; }

                MemberCaseCarePlanId = forMemberCaseCarePlanId;

                if (Case.FindMemberCaseCarePlan (forMemberCaseCarePlanId) == null) { Server.Transfer ("/PermissionDenied.aspx"); return; }


                // DETERMINE IF CARE PLAN IS UNDER DEVELOPMENT, CREATE/EDIT 

                Assessment = MemberCaseCarePlan.CreateAssessment ();


                InitializeAll ();

                #endregion


            } // Initial Page Load

            else { // Postback

            }

            // ApplySecurity ();

            if (Case.Member != null) {

                Page.Title = "Member Case: " + Case.Member.Name + " (" + Case.Member.CurrentAge + " | " + Case.Member.GenderDescription + ((Case.Member.MostRecentEnrollment != null) ? " | " + Case.Member.MostRecentEnrollment.ProgramMemberId : String.Empty) + ")";

            }

            return;

        }

        #endregion


        #region Initializations

        private void InitializeAll () {

            InitializeMember ();

            AssessmentToolbar_UpdateCount ();

            return;

        }

        private void InitializeCarePlan () {

            if (MemberCaseCarePlan == null) { return; }


            
            return;

        }

        private void InitializeMember () {

            Client.Core.Member.Member member = Case.Member;

            if (member == null) { return; }


            ApplicationTitle.InnerText = member.Name + " (" + member.CurrentAge + " | " + member.GenderDescription + ((member.MostRecentEnrollment != null) ? " | " + member.MostRecentEnrollment.ProgramMemberId : String.Empty) + ")";

            ApplicationTitle.HRef = @"/Application/Member/MemberProfile.aspx?MemberId=" + member.Id.ToString ();

            return;

        }

        #endregion


        #region Control Events

        protected void AssessmentToolbar_UpdateCount () {

            String toolbarCountMessage = "Unknown";

            Int32 totalMeasures = 0;

            Int32 completedMeasures = 0;

            if (Assessment != null) {

                foreach (Client.Core.Individual.Case.MemberCaseCarePlanAssessmentCareMeasure currentMeasure in Assessment.Measures) {

                    // ONLY COUNT IN TOTAL AND COMPLETED IF MEASURE IS NOT MARKED SKIP

                    totalMeasures = totalMeasures + 1;

                    if ((currentMeasure.ComponentScore > 0) && (currentMeasure.TargetValue > 0)) {

                        completedMeasures = completedMeasures + 1;

                    }

                }

                if (totalMeasures == completedMeasures) { toolbarCountMessage = "Ready"; }

                else { toolbarCountMessage = (totalMeasures - completedMeasures).ToString () + " Measures Left"; }

            }

            ((Telerik.Web.UI.RadToolBarItem)AssessmentToolbar.Items[0]).Enabled = ((totalMeasures == completedMeasures) && (totalMeasures > 0));

            ((Telerik.Web.UI.RadToolBarItem)AssessmentToolbar.Items[2]).Text = toolbarCountMessage;

            return;

        }

        protected void AssessmentToolbar_OnItemCreated (Object sender, Telerik.Web.UI.RadToolBarEventArgs e) {

            return;

        }

        protected void MemberCaseCarePlanListViewProblemsGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e) {

            switch (e.RebindReason) {

                case Telerik.Web.UI.GridRebindReason.InitialLoad:

                case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                case Telerik.Web.UI.GridRebindReason.PostBackEvent:

                    MemberCaseCarePlanListViewProblemsGrid.DataSource = MemberCaseCarePlan.Problems;

                    break;

            }

            return;

        }

        protected void AssessmentCareMeasuresListView_OnItemCreated (Object sender, Telerik.Web.UI.RadListViewItemEventArgs e) {

            // ASSIGN INITIAL VALUES 

            Label AssessmentMeasureScore = (Label)e.Item.FindControl ("AssessmentMeasureScore");

            Telerik.Web.UI.RadNumericTextBox AssessmentMeasureTarget = (Telerik.Web.UI.RadNumericTextBox)e.Item.FindControl ("AssessmentMeasureTarget");


            if (Assessment != null) {

                Telerik.Web.UI.RadListViewDataItem dataItem = ((Telerik.Web.UI.RadListViewDataItem)e.Item);

                Int32 careMeasureIndex = dataItem.DataItemIndex;

                AssessmentMeasureScore.Text = Assessment.Measures[careMeasureIndex].ComponentScore.ToString ("0.##");

                AssessmentMeasureTarget.MinValue = Convert.ToDouble (Assessment.Measures[careMeasureIndex].ComponentScore);

                AssessmentMeasureTarget.Value = Convert.ToDouble (Assessment.Measures[careMeasureIndex].TargetValue);

            }


            // USE THIS TO CREATE THE AJAX BINDINGS FOR POST BACK, PRIMARILY TO UPDATE THE SCORE 

            TelerikAjaxManager.AjaxSettings.AddAjaxSetting (AssessmentMeasureTarget, AssessmentMeasureTarget, AjaxLoadingPanelWhiteout);

            TelerikAjaxManager.AjaxSettings.AddAjaxSetting (AssessmentMeasureTarget, AssessmentToolbar, AjaxLoadingPanelWhiteout);
            
            return;

        }

        protected void AssessmentCareMeasuresListView_OnNeedDataSource (Object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e) {

            if (MemberCaseCarePlan == null) { return; } 

            switch (e.RebindReason) {

                case Telerik.Web.UI.RadListViewRebindReason.InitialLoad:

                    AssessmentCareMeasuresListView.DataSource = Assessment.Measures;

                    // MemberCaseCarePlanAssessmentCareMeasureTreeList.DataSource = assessment.Measures;

                    break;

                case Telerik.Web.UI.RadListViewRebindReason.ExplicitRebind:

                    break;

                case Telerik.Web.UI.RadListViewRebindReason.PostBackEvent:

                    break;

                case Telerik.Web.UI.RadListViewRebindReason.PostbackViewStateNotPersisted:

                    break;

            }

            return;

        }

        protected void AssessmentCareMeasureComponentsListView_OnItemCreated (Object sender, Telerik.Web.UI.RadListViewItemEventArgs e) {

            // USE THIS TO CREATE THE AJAX BINDINGS FOR POST BACK, PRIMARILY TO UPDATE THE SCORE 

            Telerik.Web.UI.RadListView ComponentsListView = (Telerik.Web.UI.RadListView)sender;

            RadioButtonList componentValueSelection = (RadioButtonList)e.Item.FindControl ("ComponentValueSelection");

            Label assessmentMeasureScore = (Label)((Telerik.Web.UI.RadListView)sender).Parent.Parent.Parent.Parent.FindControl ("AssessmentMeasureScore");

            Telerik.Web.UI.RadNumericTextBox assessmentMeasureTarget = (Telerik.Web.UI.RadNumericTextBox) ((Telerik.Web.UI.RadListView)sender).Parent.Parent.Parent.Parent.FindControl ("AssessmentMeasureTarget");

            TelerikAjaxManager.AjaxSettings.AddAjaxSetting (componentValueSelection, componentValueSelection, AjaxLoadingPanelWhiteout);

            TelerikAjaxManager.AjaxSettings.AddAjaxSetting (componentValueSelection, assessmentMeasureScore, AjaxLoadingPanelWhiteout);

            TelerikAjaxManager.AjaxSettings.AddAjaxSetting (componentValueSelection, assessmentMeasureTarget, AjaxLoadingPanelWhiteout);

            TelerikAjaxManager.AjaxSettings.AddAjaxSetting (componentValueSelection, AssessmentToolbar, AjaxLoadingPanelWhiteout);
           
            return;

        }

        protected void AssessmentCareMeasureComponentsListView_OnItemDataBound (Object sender, Telerik.Web.UI.RadListViewItemEventArgs e) {

            Telerik.Web.UI.RadListView ComponentsListView = (Telerik.Web.UI.RadListView)sender;

            RadioButtonList componentValueSelection = (RadioButtonList)e.Item.FindControl ("ComponentValueSelection");

            Telerik.Web.UI.RadListViewDataItem dataItem = (Telerik.Web.UI.RadListViewDataItem)e.Item;

            Client.Core.Individual.Case.MemberCaseCarePlanAssessmentCareMeasureComponent component = (Client.Core.Individual.Case.MemberCaseCarePlanAssessmentCareMeasureComponent)dataItem.DataItem;

            if (component == null) { return; }

            componentValueSelection.SelectedValue = component.ComponentValue.ToString ();

            return;

        }

        protected void AssessmentCareMeasureComponentsListView_OnNeedDataSource (Object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e) {

            if (MemberCaseCarePlan == null) { return; }

            Telerik.Web.UI.RadListView AssessmentCareMeasureComponentsListView = (Telerik.Web.UI.RadListView)sender;

            Client.Core.Individual.CareMeasureScale careMeasureScale = (Client.Core.Individual.CareMeasureScale)((Telerik.Web.UI.RadListViewDataItem)AssessmentCareMeasureComponentsListView.Parent).DataItem;

            Client.Core.Individual.Case.MemberCaseCarePlanAssessmentCareMeasure assessmentCareMeasure = (Client.Core.Individual.Case.MemberCaseCarePlanAssessmentCareMeasure) ((Telerik.Web.UI.RadListViewDataItem)((Telerik.Web.UI.RadListViewDataItem)AssessmentCareMeasureComponentsListView.Parent).Parent.Parent.Parent).DataItem;

            if (careMeasureScale == null) { return; }

            if (assessmentCareMeasure == null) { return; }


            switch (e.RebindReason) {

                case Telerik.Web.UI.RadListViewRebindReason.InitialLoad:

                    AssessmentCareMeasureComponentsListView.DataSource = assessmentCareMeasure.ComponentsByScale (careMeasureScale.Id);

                    break;

                case Telerik.Web.UI.RadListViewRebindReason.ExplicitRebind:

                    break;

                case Telerik.Web.UI.RadListViewRebindReason.PostBackEvent:

                    break;

                case Telerik.Web.UI.RadListViewRebindReason.PostbackViewStateNotPersisted:

                    break;

            }

            return;

        }

        protected void ComponentValueSelection_OnSelectedIndexChanged (Object sender, EventArgs e) {

            RadioButtonList ComponentValueSelection = (RadioButtonList)sender;

            
            // COMPONENTS <- SCALES <- MEASURE

            Int32 careMeasureIndex = ((Telerik.Web.UI.RadListViewDataItem)ComponentValueSelection.Parent.Parent.Parent.Parent.Parent.Parent.Parent).DataItemIndex;
            
            Int32 scaleIndex = ((Telerik.Web.UI.RadListViewDataItem)ComponentValueSelection.Parent.Parent.Parent.Parent).DataItemIndex;

            Int32 componentIndex = ((Telerik.Web.UI.RadListViewDataItem)ComponentValueSelection.Parent).DataItemIndex;


            Client.Core.Individual.CareMeasureScale careMeasureScale = Assessment.Measures[careMeasureIndex].CareMeasureScales[scaleIndex];

            Client.Core.Individual.Case.MemberCaseCarePlanAssessmentCareMeasureComponent component = Assessment.Measures[careMeasureIndex].ComponentsByScale (careMeasureScale.Id)[componentIndex];

            component.ComponentValue = Convert.ToInt32 (ComponentValueSelection.SelectedValue);


            ((Label)ComponentValueSelection.Parent.Parent.Parent.Parent.Parent.Parent.Parent.FindControl ("AssessmentMeasureScore")).Text =

            Assessment.Measures[careMeasureIndex].ComponentScore.ToString ("#.00");


            Telerik.Web.UI.RadNumericTextBox AssessmentMeasureTarget = ((Telerik.Web.UI.RadNumericTextBox)ComponentValueSelection.Parent.Parent.Parent.Parent.Parent.Parent.Parent.FindControl ("AssessmentMeasureTarget"));

            AssessmentMeasureTarget.MinValue = ((Assessment.Measures[careMeasureIndex].ComponentScore == 0) ? 0.1 : Convert.ToDouble (Assessment.Measures[careMeasureIndex].ComponentScore));

            if (Assessment.Measures[careMeasureIndex].TargetValue < Assessment.Measures[careMeasureIndex].ComponentScore) { 

                Assessment.Measures[careMeasureIndex].TargetValue = Assessment.Measures[careMeasureIndex].ComponentScore;

            }


            AssessmentToolbar_UpdateCount ();

            return;

        }

        protected void AssessmentMeasureTarget_OnTextChanged (Object sender, EventArgs e) {

            // CONTAINED IN AssessmentCareMeasuresListView

            Telerik.Web.UI.RadNumericTextBox AssessmentMeasureTarget = (Telerik.Web.UI.RadNumericTextBox)sender;

            Int32 careMeasureIndex = ((Telerik.Web.UI.RadListViewDataItem)AssessmentMeasureTarget.Parent).DataItemIndex;

            Assessment.Measures[careMeasureIndex].TargetValue = Convert.ToDecimal((AssessmentMeasureTarget.Value.HasValue) ? AssessmentMeasureTarget.Value : 0);

            AssessmentToolbar_UpdateCount ();

            return;

        }

        protected void AssessmentToolbar_OnButtonClick (Object sender, Telerik.Web.UI.RadToolBarEventArgs e) {

            switch (e.Item.Text) {

                case "Save Assessment":

                    Boolean saveAssessmentSuccess = MercuryApplication.MemberCaseCarePlanAssessmentSave (Assessment);

                    if (!saveAssessmentSuccess) {

                        // SET ERROR

                        ExceptionMessageRow.Style.Clear ();

                        ExceptionMessageLabel.Text = MercuryApplication.LastExceptionMessage;

                    }

                    else {

                        Response.Redirect ("/Application/MemberCase/MemberCase.aspx?MemberCaseId=" + Case.Id.ToString ());

                    }

                    break;

            }

            return;

        }

        #endregion 


        #region Button Event Handlers

        protected Boolean ApplyChanges () {

            Boolean isModified = false;

            Boolean success = false;


            Mercury.Server.Application.MemberCaseModificationResponse response;



            isModified = false;

            if (isModified) {

                success = true;

                if (success) { // IF ALL PROBLEMS WERE ADDED WITHOUT PROBLEMS, REFRESH TREE


                }

            }

            else {

                ExceptionMessage = "No Changes Detected";

                success = true;

            }


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

                Response.Redirect ("/Application/MemberCase/MemberCase.aspx?MemberCaseId=" + Case.Id.ToString ());

            }

            return;

        }

        protected void ButtonApply_OnClick (Object sender, EventArgs eventArgs) {

            Boolean success = ApplyChanges ();

            return;

        }

        protected void ButtonCancel_OnClick (Object sender, EventArgs eventArgs) {

            Response.Redirect ("/Application/MemberCase/MemberCase.aspx?MemberCaseId=" + Case.Id.ToString ());

            return;

        }

        #endregion

    }

}