using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.MemberCase {

    public partial class MemberCaseCarePlanGoal : System.Web.UI.UserControl {

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

        public Client.Core.Individual.Case.MemberCaseCarePlanGoal CarePlanGoal {

            get { return (Client.Core.Individual.Case.MemberCaseCarePlanGoal)Session[SessionCachePrefix + this.ClientID + ".MemberCaseCarePlanGoal"]; }

            set {

                Client.Core.Individual.Case.MemberCaseCarePlanGoal carePlanGoal = CarePlanGoal;

                carePlanGoal = value;

                Session[SessionCachePrefix + this.ClientID + ".MemberCaseCarePlanGoal"] = value;

                InitializeCarePlanGoal (); // ALWAYS CALL THIS INCASE THE IN NEED OF RESET 

            }

        }

        #endregion 


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            return;

        }

        #endregion 


        #region Initializations

        private void InitializeCarePlanGoal () {

            Client.Core.Individual.Case.MemberCaseCarePlanGoal carePlanGoal = CarePlanGoal;

            if (carePlanGoal == null) { return; }


            System.Web.UI.HtmlControls.HtmlControl titlePanel = (System.Web.UI.HtmlControls.HtmlControl)FindControl ("TitlePanel_" + carePlanGoal.MemberCaseCarePlan.Status.ToString ());

            if (titlePanel != null) { titlePanel.Visible = true; }


            // MAP PROPERTY VALUES INTO CONTROLS 

            CarePlanGoalEditName.Text = CarePlanGoalName.Text = carePlanGoal.Name;

            // CARE PLAN GOAL STATUS

            CarePlanGoalEditClinicalNarrative.Text = CarePlanGoalClinicalNarrative.Text = carePlanGoal.ClinicalNarrative;

            CarePlanGoalEditCommonNarrative.Text = CarePlanGoalCommonNarrative.Text = carePlanGoal.CommonNarrative;

            CarePlanGoalMeasureName.Text = carePlanGoal.CareMeasureName;

            CarePlanGoalMeasureName.ToolTip = (carePlanGoal.CareMeasure != null) ? carePlanGoal.CareMeasure.Description : String.Empty;


            // EDIT PANELS BASED ON CARE PLAN GOAL STATUS (GOAL STATUS IS MORE DETAILED THAN CARE PLAN STATUS)

            System.Web.UI.HtmlControls.HtmlControl carePlanGoalEditPanel = (System.Web.UI.HtmlControls.HtmlControl)FindControl ("CarePlanGoalEditPanel_" + carePlanGoal.MemberCaseCarePlan.Status.ToString ());

            if (carePlanGoalEditPanel != null) { carePlanGoalEditPanel.Visible = true; }


            #region EDIT PANEL - UNDER DEVELOPMENT

            CarePlanGoalTimeframeSelection.SelectedValue = ((Int32)carePlanGoal.GoalTimeframe).ToString ();

            CarePlanGoalScheduleValue.Value = carePlanGoal.ScheduleValue;

            CarePlanGoalScheduleQualifierSelection.SelectedValue = ((Int32)carePlanGoal.ScheduleQualifier).ToString ();


            List<Client.Core.Individual.CareMeasure> careMeasuresAvailable =

                (from currentCareMeasure in MercuryApplication.CareMeasuresAvailable (true)

                 where (((currentCareMeasure.Enabled) && (currentCareMeasure.Visible)) || (currentCareMeasure.Id == carePlanGoal.CareMeasureId))

                 select currentCareMeasure).ToList ();


            CarePlanGoalCareMeasureSelection.DataSource = careMeasuresAvailable;

            CarePlanGoalCareMeasureSelection.DataTextField = "Name";

            CarePlanGoalCareMeasureSelection.DataValueField = "Id";

            CarePlanGoalCareMeasureSelection.SelectedValue = carePlanGoal.CareMeasureId.ToString ();

            #endregion 


            CarePlanGoalInterventionsGrid.Rebind ();

            return;

        }

        #endregion 

        
        #region Care Plan Intervention Grid Events

        protected void CarePlanGoalInterventionsGrid_OnItemCreated (Object sender, Telerik.Web.UI.GridItemEventArgs e) {

            if (CarePlanGoal == null) { return; }

            if (e.Item is Telerik.Web.UI.GridCommandItem) {

                Telerik.Web.UI.GridCommandItem commandItem = (Telerik.Web.UI.GridCommandItem)e.Item;

                HyperLink CarePlanGoalInterventionsGrid_CareInterventionAdd = (HyperLink)commandItem.FindControl ("CarePlanGoalInterventionsGrid_CareInterventionAdd");

                if (CarePlanGoalInterventionsGrid_CareInterventionAdd != null) {

                    String href = "/Application/MemberCase/Actions/AddCarePlanGoalIntervention.aspx?MemberCaseId=" + CarePlanGoal.MemberCaseCarePlan.MemberCase.Id.ToString () + "&MemberCaseCarePlanGoalId=" + CarePlanGoal.Id.ToString ();

                    CarePlanGoalInterventionsGrid_CareInterventionAdd.NavigateUrl = href;

                }

            }

            return;

        }

        protected void CarePlanGoalInterventionsGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e) {

            if (CarePlanGoal == null) { return; }


            switch (e.RebindReason) {

                case Telerik.Web.UI.GridRebindReason.InitialLoad:

                case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                    CarePlanGoalInterventionsGrid.DataSource = CarePlanGoal.Interventions;

                    break;

                case Telerik.Web.UI.GridRebindReason.PostBackEvent:

                    break;

            }

            return;

        }

        #endregion 


        #region Events

        protected void CarePlanGoalEditApply_OnClick (Object sender, EventArgs e) {

            Mercury.Server.Application.MemberCaseModificationResponse response;


            // GET RECENT REFERENCE 

            Client.Core.Individual.Case.MemberCaseCarePlanGoal carePlanGoal = ParentMemberCasePage.Case.FindMemberCaseCarePlanGoal (CarePlanGoal.Id);

            carePlanGoal.Name = CarePlanGoalEditName.Text;

            carePlanGoal.Description = CarePlanGoalEditName.Text;

            carePlanGoal.ClinicalNarrative = CarePlanGoalEditClinicalNarrative.Text;

            carePlanGoal.CommonNarrative = CarePlanGoalEditCommonNarrative.Text;

            carePlanGoal.GoalTimeframe = (Mercury.Server.Application.CarePlanGoalTimeframe) Convert.ToInt32 (CarePlanGoalTimeframeSelection.SelectedValue);

            carePlanGoal.ScheduleValue = Convert.ToInt32 (CarePlanGoalScheduleValue.Value);

            carePlanGoal.ScheduleQualifier = (Mercury.Server.Application.DateQualifier) Convert.ToInt32 (CarePlanGoalScheduleQualifierSelection.SelectedValue);

            carePlanGoal.CareMeasureId = Convert.ToInt64 (CarePlanGoalCareMeasureSelection.SelectedValue);


            response = MercuryApplication.MemberCaseCarePlanGoal_Update (ParentMemberCasePage.Case, CarePlanGoal.Id);

            if (response.HasException) { ParentMemberCasePage.ExceptionMessage = response.Exception.Message; }

            else { ParentMemberCasePage.Case = new Client.Core.Individual.Case.MemberCase (MercuryApplication, response.MemberCase); }


            return;

        }

        #endregion 

    }

}