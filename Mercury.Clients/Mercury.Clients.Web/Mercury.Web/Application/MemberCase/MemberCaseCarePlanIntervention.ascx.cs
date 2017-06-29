using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.MemberCase {

    public partial class MemberCaseCarePlanIntervention : System.Web.UI.UserControl {

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

        public Client.Core.Individual.Case.MemberCaseCareIntervention CarePlanIntervention {

            get { return (Client.Core.Individual.Case.MemberCaseCareIntervention)Session[SessionCachePrefix + this.ClientID + ".MemberCaseCarePlanIntervention"]; }

            set {

                Client.Core.Individual.Case.MemberCaseCareIntervention carePlanIntervention = CarePlanIntervention;

                if (carePlanIntervention != value) {

                    carePlanIntervention = value;

                    Session[SessionCachePrefix + this.ClientID + ".MemberCaseCarePlanIntervention"] = value;

                }

                InitializeCarePlanIntervention (); // ALWAYS CALL THIS INCASE THE IN NEED OF RESET 

            }

        }

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            return;

        }

        #endregion


        #region Initializations

        private void InitializeCarePlanIntervention () {

            Client.Core.Individual.Case.MemberCaseCareIntervention carePlanIntervention = CarePlanIntervention;

            if (carePlanIntervention == null) { return; }


            //System.Web.UI.HtmlControls.HtmlControl titlePanel = (System.Web.UI.HtmlControls.HtmlControl)FindControl ("TitlePanel_" + carePlanIntervention.Status.ToString ());

            //if (titlePanel != null) { titlePanel.Visible = true; }


            // MAP PROPERTY VALUES INTO CONTROLS 

            // CarePlanInterventionEditName.Text = CarePlanInterventionName.Text = carePlanIntervention.Name;



            CareInterventionAssociationsGrid.Rebind ();

            CarePlanInterventionActivitiesGrid.Rebind ();

            return;

        }

        #endregion


        #region Events

        protected void CarePlanInterventionEditApply_OnClick (Object sender, EventArgs e) {

            Mercury.Server.Application.MemberCaseModificationResponse response;




            //if (response.HasException) { ParentMemberCasePage.ExceptionMessage = response.Exception.Message; }

            //else { ParentMemberCasePage.Case = new Client.Core.Individual.Case.MemberCase (MercuryApplication, response.MemberCase); }


            return;

        }

        #endregion



        #region Care Plan Intervention Grid Events

        protected void CareInterventionAssociationsGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e) {

            if (CarePlanIntervention == null) { return; }


            switch (e.RebindReason) {

                case Telerik.Web.UI.GridRebindReason.InitialLoad:

                case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                    CareInterventionAssociationsGrid.DataSource = CarePlanIntervention.GoalInterventions;

                    break;

                case Telerik.Web.UI.GridRebindReason.PostBackEvent:

                    break;

            }

            return;

        }

        #endregion 


        #region Care Plan Intervention Grid Events

        protected void CarePlanInterventionActivitiesGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e) {

            if (CarePlanIntervention == null) { return; }


            switch (e.RebindReason) {

                case Telerik.Web.UI.GridRebindReason.InitialLoad:

                case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                    CarePlanInterventionActivitiesGrid.DataSource = CarePlanIntervention.Activities;

                    break;

                case Telerik.Web.UI.GridRebindReason.PostBackEvent:

                    break;

            }

            return;

        }

        protected void CarePlanInterventionActivitiesGrid_OnItemCreated (Object sender, Telerik.Web.UI.GridItemEventArgs e) {

            if ((e.Item is Telerik.Web.UI.GridEditableItem) && (e.Item.IsInEditMode)) {

                if (CarePlanIntervention == null) { return; }

                Telerik.Web.UI.GridEditableItem item = (Telerik.Web.UI.GridEditableItem)e.Item;
                


            }

            return;

        }

        protected void CarePlanInterventionActivitiesGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs e) {

            if (CarePlanIntervention == null) { return; }

            Mercury.Server.Application.MemberCaseModificationResponse response;

            switch (e.CommandName) {

                case Telerik.Web.UI.RadGrid.InitInsertCommandName:

                    break;

                case Telerik.Web.UI.RadGrid.PerformInsertCommandName:

                    #region Perform Insert of New Intervention


                    // RETREIVE REFERENCES TO ALL TEMPLATED CONTROLS

                    //RadioButtonList AddCarePlanInterventionTypeSelection = (RadioButtonList)e.Item.FindControl ("AddCarePlanInterventionTypeSelection");

                    //if (AddCarePlanInterventionTypeSelection == null) { return; }

                    //Telerik.Web.UI.RadTextBox AddCarePlanInterventionName = (Telerik.Web.UI.RadTextBox)e.Item.FindControl ("AddCarePlanInterventionName");

                    //if (AddCarePlanInterventionName == null) { return; }

                    //Telerik.Web.UI.RadComboBox AddCarePlanInterventionExistingSelection = (Telerik.Web.UI.RadComboBox)e.Item.FindControl ("AddCarePlanInterventionExistingSelection");

                    //if (AddCarePlanInterventionExistingSelection == null) { return; }


                    //Int64 selectedBaselineInterventionId = (AddCarePlanInterventionTypeSelection.SelectedValue == "0") ? Convert.ToInt64 (AddCarePlanInterventionExistingSelection.SelectedValue) : 0;

                    //String carePlanInterventionName = (AddCarePlanInterventionTypeSelection.SelectedValue == "1") ? AddCarePlanInterventionName.Text : String.Empty;


                    //response = MercuryApplication.MemberCaseCarePlanIntervention_Add (ParentMemberCasePage.Case, CarePlan.Id, selectedBaselineInterventionId, carePlanInterventionName);

                    //if (response.HasException) { ParentMemberCasePage.ExceptionMessage = response.Exception.Message; }

                    //else { ParentMemberCasePage.Case = new Client.Core.Individual.Case.MemberCase (MercuryApplication, response.MemberCase); }


                    #endregion

                    break;

                case Telerik.Web.UI.RadGrid.DeleteCommandName:

                    //response = MercuryApplication.MemberCaseCarePlanIntervention_Delete (ParentMemberCasePage.Case, CarePlanIntervention.Activities[e.Item.ItemIndex].Id);

                    //if (response.HasException) { ParentMemberCasePage.ExceptionMessage = response.Exception.Message; }

                    //else { ParentMemberCasePage.Case = new Client.Core.Individual.Case.MemberCase (MercuryApplication, response.MemberCase); }

                    break;

                case Telerik.Web.UI.RadGrid.CancelCommandName:

                case Telerik.Web.UI.RadGrid.RebindGridCommandName:

                    CarePlanInterventionActivitiesGrid.DataSource = CarePlanIntervention.Activities;

                    CarePlanInterventionActivitiesGrid.DataBind ();

                    break;

                default:

                    break;

            }

            return;

        }

        #endregion
        
    }

}