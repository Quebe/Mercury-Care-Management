using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.MemberCase {

    public partial class MemberCaseCarePlan : System.Web.UI.UserControl {

        #region Private Properties

        private Boolean restoreSelection = false;

        #endregion


        #region Session Properties

        public String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (UserControlInstanceId.Text)) { UserControlInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return UserControlInstanceId.Text + ".";

            }

        }

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application)Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }


        public Client.Core.Individual.Case.MemberCase Case {

            get { return (Client.Core.Individual.Case.MemberCase)Session[SessionCachePrefix + "MemberCase"]; }

            set {

                Client.Core.Individual.Case.MemberCase memberCase = (Client.Core.Individual.Case.MemberCase)Session[SessionCachePrefix + "MemberCase"];

                memberCase = value;

                Session[SessionCachePrefix + "MemberCase"] = value;

                InitializeAll ();

            }

        }

        public List<Client.Core.Individual.Case.MemberCaseCarePlan> CarePlansDataSource {

            get {

                List<Client.Core.Individual.Case.MemberCaseCarePlan> dataSource = new List<Client.Core.Individual.Case.MemberCaseCarePlan> ();

                if (Case != null) {

                    dataSource = Case.CarePlansUnderDevelopmentActive;

                }

                return dataSource;

            }

        }

        public String InstanceId { get { return UserControlInstanceId.Text; } set { UserControlInstanceId.Text = value; } }

        public MemberCase ParentMemberCasePage { get { return (MemberCase)Page; } }

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            MemberCaseId.Text = Case.Id.ToString ();

            return;

        }

        #endregion


        #region Initializations

        private void InitializeAll () {

            if (Case == null) { return; }


            InitializeIdentifiedProblemStatments ();

            InitializeToolbar ();


            MemberCaseCarePlanListView.Rebind ();

            ProblemClassListView.Rebind ();


            // INITIALIZE MEMBER CASE PROBLEM CLASS USER ASSIGNMENT

            InitializeMemberCaseProblemClassUserAssignment ();

            // INITIALIZE MEMBER CAE PROBLEM CLASS PROVIDER ASSIGNMENT

            InitializeMemberCaseProblemClassProviderAssignment ();


            return;

        }

        private void InitializeIdentifiedProblemStatments () {

            List<Client.Core.Individual.MemberProblemStatementIdentified> problemStatements = MercuryApplication.MemberProblemStatementIdentifiedAvailable (Case.MemberId, false, true);

            IdentifiedProblemStatementSection.Visible = (problemStatements.Count > 0);

            IdentifiedProblemStatementsGrid.DataSource = problemStatements;

            IdentifiedProblemStatementsGrid.DataBind ();

            return;

        }

        protected void ProblemStatementSelection_OnInit (Object sender, EventArgs e) {

            InitializeToolbar ();

            return;

        }

        private void InitializeToolbar () {

            return;

        }

        private void InitializeMemberCaseProblemClassUserAssignment () {

            // LOOP THROUGH RAD LIST VIEW DATA ITEMS IN PROBLEM CLASS LIST VIEW RAD LIST VIEW

            foreach (Telerik.Web.UI.RadListViewDataItem currentProblemClassList in ProblemClassListView.Items) {

                // GET DATA KEY VALUE OF CURRENT PROBLEM CLASS LIST RAD LIST VIEW DATA ITEM

                Int64 dataKeyValue = (Int64)currentProblemClassList.GetDataKeyValue ("Id");

                // LOOP THROUGH MEMBER CASE PROBLEM CLASSES IN MEMBER CASE

                foreach (Client.Core.Individual.Case.MemberCaseProblemClass currentMemberCaseProblemClass in Case.ProblemClasses) {

                    // IF ID OF CURRENT MEMBER CASE PROBLEM CLASS EQUALS DATA KEY VALUE, THEN PROCESS

                    if (currentMemberCaseProblemClass.Id == dataKeyValue) {

                        // CREATE REFERENCE TO IS ASSIGNED TO USER CHANGE VISIBLE AND DEFAULT IT TO FALSE

                        Boolean isAssignedToUserChangeVisible = false;

                        // GET REFERENCE TO PROBLEM CLASS ASSIGNED TO USER LABEL

                        Label problemClassAssignedToUserLabel = (Label)currentProblemClassList.FindControl ("ProblemClassAssignedToUserLabel");

                        // SET TEXT OF PROBLEM CLASS ASSIGNED TO USER LABEL AS CURRENT ASSIGNED USER NAME OR IF NOT ASSIGNED AS NOT ASSIGNED

                        problemClassAssignedToUserLabel.Text = (!String.IsNullOrEmpty (currentMemberCaseProblemClass.AssignedToUserAccountId)) ? currentMemberCaseProblemClass.AssignedToUserDisplayName : "** Not Assigned";

                        // IF ASSIGNED TO THE CURRENT USER, THE USER CAN UNASSIGN THEMSELVES

                        if ((currentMemberCaseProblemClass.AssignedToSecurityAuthorityId == MercuryApplication.Session.SecurityAuthorityId) && (currentMemberCaseProblemClass.AssignedToUserAccountId == MercuryApplication.Session.UserAccountId)) {

                            // SET IS ASSIGNED TO USER CHANGE VISIBLE AS TRUE

                            isAssignedToUserChangeVisible = true;

                        }

                        // IF CURRENT MEMBER CASE PROBLEM CLASS IS NOT ASSIGNED TO CURRENT USER, EVALUATE MEMBER CASE ASSIGNMENT

                        else {

                            // IF CASE IS ASSIGNED TO A WORK TEAM, AND MEMBER IS PART OF THAT WORK TEAM, THEY CAN SELF ASSIGN (OR A MANAGER MAY ASSIGN TO ANY USER)

                            if (Case.HasWorkTeamAssignment) {

                                // IF THE CURRENT USER IS A MANAGER IN THE TEAM THAT IS ASSIGNED TO THE CURRENT MEMBER CASE, THEN SET IS ASSIGNED TO USER CHANGE VISIBLE AS TRUE

                                if (Case.AssignedToThisSessionTeamManager) {

                                    // SET IS ASSIGNED TO USER CHANGE VISIBLE AS TRUE

                                    isAssignedToUserChangeVisible = true;

                                }

                                // IF THE CURRENT USER IS A MEMBER IN THE TEAM THAT IS ASSIGNED TO THE CURRENT MEMBER CASE, THEN SET IS ASSIGNED TO USER CHANGE VISIBLE TO TRUE

                                else if (Case.AssignedToThisSessionTeam) {

                                    // SET IS ASSIGNED TO USER CHANGE VISIBLE TO TRUE

                                    isAssignedToUserChangeVisible = true;

                                }

                            }

                        }

                        // GET REFERENCE TO PROBLEM CLASS ASSIGNED TO USER CHANGE LINK

                        System.Web.UI.HtmlControls.HtmlGenericControl problemClassAssignedToUserChangeLink = (System.Web.UI.HtmlControls.HtmlGenericControl)currentProblemClassList.FindControl ("ProblemClassAssignedToUserChangeLink");

                        // SET DISPLAY STYLE OF PROBLEM CLASS ASSIGNED TO USER CHANGE LINK TO INLINE IF IS ASSIGNED TO USER CHANGE VISIBLE IS TRUE OR TO NONE IF IS ASSIGNED TO USER CHANGE LINK IS FALSE

                        problemClassAssignedToUserChangeLink.Style.Add ("display", ((isAssignedToUserChangeVisible) ? "inline" : "none"));

                        // GET REFERENCE TO PROBLEM CLASS ASSIGNED TO USER SELECTION RAD COMBO BOX

                        Telerik.Web.UI.RadComboBox problemClassAssignedToUserSelection = (Telerik.Web.UI.RadComboBox)currentProblemClassList.FindControl ("ProblemClassAssignedToUserSelection");


                        // IF IS ASSIGNED TO USER CHANGE VISIBLE IS TRUE, THEN ADD AVAILABLE CARE TEAM MEMBERS TO SELECTION 

                        if (isAssignedToUserChangeVisible) {

                            // CLEAR ITEMS OF PROBLEM CLASS ASSIGNED TO USER SELECTION RAD COMBO BOX

                            problemClassAssignedToUserSelection.Items.Clear ();

                            // ADD NOT ASIGNED RAD COMBO BOX ITEM TO PROBLEM CLASS ASSIGNED TO USER SELECTION RAD COMBO BOX

                            problemClassAssignedToUserSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** Not Assigned", "0|0|" + currentMemberCaseProblemClass.Id));

                            // IF CASE IS CURRENTLY ASSIGNED TO A WORK TEAM, ADD TEAM MEMBERS TO SELECTION

                            if (Case.AssignedToWorkTeam != null) {

                                // LOOP THROUGH WORK TEAM MEMBERSHIPS OF TEAM MEMBERSHIPS OF WORK TEAM CURRENTLY ASSIGNED TO MEMBER CASE

                                foreach (Mercury.Server.Application.WorkTeamMembership currentMembership in Case.AssignedToWorkTeam.Membership) {

                                    // IF USER IS TEAM MANAGER, CAN ASSIGN TO ANY USER

                                    Boolean canAddMembership = Case.AssignedToThisSessionTeamManager;

                                    // IF THE CASE IS NOT ASSIGNED TO ANYONE, AND USER IS MEMBER OF TEAM, CAN "SELF-ASSIGN";                       

                                    canAddMembership |= ((!currentMemberCaseProblemClass.AssignedToDate.HasValue) && ((currentMembership.SecurityAuthorityId == MercuryApplication.Session.SecurityAuthorityId) && (currentMembership.UserAccountId == MercuryApplication.Session.UserAccountId)));

                                    // IF CAN ADD MEMBERSHIP IS TRUE, THEN ADD NEW RAD COMBO BOX ITEM FOR CURRENT TEAM MEMBER TO ITEMS OF PROBLEM CLASS ASSIGNED TO USER SELECTION RAD COMBO BOX

                                    if (canAddMembership) {

                                        // ADD NEW RAD COMBO BOX ITEM FOR CURRENT TEAM MEMBER TO ITEMS OF PROBLEM CLASS ASSIGNED TO USER SELECTION RAD COMBO BOX

                                        problemClassAssignedToUserSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentMembership.UserDisplayName, currentMembership.SecurityAuthorityId.ToString () + "|" + currentMembership.UserAccountId + "|" + currentMemberCaseProblemClass.Id));

                                    }

                                } /* END FOREACH */

                                // SET SELECTED VALUE OF PROBLEM CLASS ASSIGNED TO USER SELECTION RAD COMBO BOX TO CURRENT MEMBER CASE PROBLEM CLASS ASSIGNED USER

                                problemClassAssignedToUserSelection.SelectedValue = currentMemberCaseProblemClass.AssignedToSecurityAuthorityId.ToString () + "|" + currentMemberCaseProblemClass.AssignedToUserAccountId + "|" + currentMemberCaseProblemClass.Id;

                            }

                        }

                    }

                } /* END FOREACH */

            } /* END FOREACH */

            return;

        }

        private void InitializeMemberCaseProblemClassProviderAssignment () {

            // LOOP THROUGH RAD LIST VIEW DATA ITEMS IN PROBLEM CLASS LIST VIEW RAD LIST VIEW

            foreach (Telerik.Web.UI.RadListViewDataItem currentProblemClassList in ProblemClassListView.Items) {

                // GET DATA KEY VALUE OF CURRENT PROBLEM CLASS LIST RAD LIST VIEW DATA ITEM

                Int64 dataKeyValue = (Int64)currentProblemClassList.GetDataKeyValue ("Id");

                // GET REFERENCE TO PROBLEM CLASS ASSIGNED TO USER SELECTION RAD COMBO BOX

                Telerik.Web.UI.RadComboBox currentProblemClassAssignedToProviderSelection = (Telerik.Web.UI.RadComboBox)currentProblemClassList.FindControl ("ProblemClassAssignedToProviderSelection");

                // LOOP THROUGH MEMBER CASE PROBLEM CLASSES IN MEMBER CASE

                foreach (Client.Core.Individual.Case.MemberCaseProblemClass currentMemberCaseProblemClass in Case.ProblemClasses) {

                    // IF ID OF CURRENT MEMBER CASE PROBLEM CLASS EQUALS DATA KEY VALUE, THEN PROCESS

                    if (currentMemberCaseProblemClass.Id == dataKeyValue) {


                        // SET IS ASSIGNED TO PROVIDER CHANGE VISIBLE TO TRUE IF CURRENT MEMBER CASE PROBLEM CLASS IS ASSIGNED TO PROVIDER AND FALSE IF IT IS NOT

                        Boolean isAssignedToProviderChangeVisible = true;

                        // GET REFERENCE TO PROBLEM CLASS ASSIGNED TO PROVIDER LABEL

                        Label problemClassAssignedToProviderLabel = (Label)currentProblemClassList.FindControl ("ProblemClassAssignedToProviderLabel");

                        // SET TEXT OF PROBLEM CLASS ASSIGNED TO PROVIDER LABEL AS CURRENT ASSIGNED PROVIDER NAME OR IF NOT ASSIGNED AS NOT ASSIGNED

                        problemClassAssignedToProviderLabel.Text = ((currentMemberCaseProblemClass.AssignedToProvider != null)) ? currentMemberCaseProblemClass.AssignedToProvider.Name : "** Not Assigned";

                        // GET REFERENCE TO PROBLEM CLASS ASSIGNED TO PROVIDER CHANGE LINK

                        System.Web.UI.HtmlControls.HtmlGenericControl problemClassAssignedToProviderChangeLink = (System.Web.UI.HtmlControls.HtmlGenericControl)currentProblemClassList.FindControl ("ProblemClassAssignedToProviderChangeLink");

                        // SET DISPLAY STYLE OF PROBLEM CLASS ASSIGNED TO PROVIDER CHANGE LINK TO INLINE IF IS ASSIGNED TO PROVIDER CHANGE VISIBLE IS TRUE OR TO NONE IF IS ASSIGNED TO PROVIDER CHANGE LINK IS FALSE

                        problemClassAssignedToProviderChangeLink.Style.Add ("display", ((isAssignedToProviderChangeVisible) ? "inline" : "none"));

                        // ADD NOT ASSIGNED RAD COMBO BOX ITEM TO CURRENT PROBLEM CLASS ASSIGNED TO PROVIDER SELECTION RAD COMBO BOX

                        currentProblemClassAssignedToProviderSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** Not Assigned", "0|" + currentMemberCaseProblemClass.Id));


                        // IF CURRENT MEMBER CASE PROBLEM CLASS DOES HAVE A PROVIDER ASSIGNED (ASSIGNED TO PROVIDER ID IS NOT 0), THEN ADD RAD COMBOX ITEM FOR ASSIGNED PROVIDER

                        if (currentMemberCaseProblemClass.AssignedToProviderId != 0) {

                            // CLEAR ITEMS OF CURRENT PROBLEM CLASS ASSIGNED TO PROVIDER SELECTION RAD COMBO BOX

                            currentProblemClassAssignedToProviderSelection.Items.Clear ();

                            // ADD NEW RAD COMBO BOX ITEM FOR CURRENT ASSIGNED TO PROVIDER TO ITEMS OF CURRENT PROBLEM CLASS ASSIGNED TO PROVIDER SELECTION RAD COMBO BOX

                            currentProblemClassAssignedToProviderSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentMemberCaseProblemClass.AssignedToProvider.Name, currentMemberCaseProblemClass.AssignedToProviderId.ToString () + "|" + currentMemberCaseProblemClass.Id));

                            // ADD NOT ASSIGNED RAD COMBO BOX ITEM TO CURRENT PROBLEM CLASS ASSIGNED TO PROVIDER SELECTION RAD COMBO BOX

                            currentProblemClassAssignedToProviderSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** Not Assigned", "0|" + currentMemberCaseProblemClass.Id));

                        }

                        // IF CURRENT MEMBER CASE PROBLEM CLASS DOES NOT HAVE A PROVIDER ASSIGNED (ASSIGNED TO PROVIDER ID IS 0), THEN DO NOTHING

                        else {

                            /* DO NOTHING */

                        }

                        break;

                    }

                } /* END FOREACH */

            } /* END FOREACH */

            return;

        }

        #endregion


        #region Toolbar Events

        protected void CarePlanToolbar_OnButtonClick (Object sender, Telerik.Web.UI.RadToolBarEventArgs e) {

            Telerik.Web.UI.RadToolBarButton button = (Telerik.Web.UI.RadToolBarButton)e.Item;

            if (button == null) { return; }


            switch (button.Text) {

                case "View by Problem Statement":

                    CarePlanViewMode.Text = "ProblemStatement";

                    button.Text = "View by Care Plan";


                    MemberCaseCarePlanListView.Visible = (CarePlanViewMode.Text == "CarePlan");

                    ProblemClassListView.Visible = (CarePlanViewMode.Text == "ProblemStatement");

                    ProblemClassListView.Rebind ();

                    break;

                case "View by Care Plan":

                    CarePlanViewMode.Text = "CarePlan";

                    button.Text = "View by Problem Statement";


                    MemberCaseCarePlanListView.Visible = (CarePlanViewMode.Text == "CarePlan");

                    ProblemClassListView.Visible = (CarePlanViewMode.Text == "ProblemStatement");

                    MemberCaseCarePlanListView.Rebind ();

                    break;

                default: break;

            }

            return;

        }

        #endregion


        #region Problem Member Case Care Plan List View Events

        protected void MemberCaseCarePlanListView_OnNeedDataSource (Object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e) {

            switch (e.RebindReason) {

                case Telerik.Web.UI.RadListViewRebindReason.InitialLoad:

                case Telerik.Web.UI.RadListViewRebindReason.ExplicitRebind:

                case Telerik.Web.UI.RadListViewRebindReason.PostBackEvent:

                    MemberCaseCarePlanListView.DataSource = CarePlansDataSource;

                    break;

            }

            return;

        }

        protected void MemberCaseCarePlanListViewProblemsGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e) {

            Telerik.Web.UI.RadGrid MemberCaseCarePlanListViewProblemsGrid = (Telerik.Web.UI.RadGrid)sender;

            Telerik.Web.UI.RadListViewDataItem listViewDataItem = (Telerik.Web.UI.RadListViewDataItem)MemberCaseCarePlanListViewProblemsGrid.Parent;

            Int32 selectedMemberCaseCarePlanIndex = listViewDataItem.DataItemIndex;

            Client.Core.Individual.Case.MemberCaseCarePlan selectedMemberCaseCarePlan = CarePlansDataSource[selectedMemberCaseCarePlanIndex];


            switch (e.RebindReason) {

                case Telerik.Web.UI.GridRebindReason.InitialLoad:

                case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                case Telerik.Web.UI.GridRebindReason.PostBackEvent:

                    MemberCaseCarePlanListViewProblemsGrid.DataSource = selectedMemberCaseCarePlan.Problems;

                    break;

            }

            return;

        }

        #endregion


        #region Problem Member Case Problem Class List View Events

        private List<Int64> SelectedCarePlans (Int64 forProblemClassId) {

            List<Int64> selectedCarePlans = (List<Int64>)Session[SessionCachePrefix + ".SelectedCarePlans." + forProblemClassId.ToString ()];

            if (selectedCarePlans == null) { selectedCarePlans = new List<Int64> (); }

            return selectedCarePlans;

        }

        private void SelectedCarePlans (Int64 forProblemClassId, List<Int64> forSelectedCarePlans) {

            Session[SessionCachePrefix + ".SelectedCarePlans." + forProblemClassId.ToString ()] = forSelectedCarePlans;

            return;

        }

        public void ProblemClassListView_ResetSelected () {

            foreach (Telerik.Web.UI.RadListViewDataItem currentProblemClassItem in ProblemClassListView.Items) {

                Int64 problemClassId = Convert.ToInt64 (currentProblemClassItem.GetDataKeyValue ("Id"));

                List<Int64> selectedCarePlans = SelectedCarePlans (problemClassId);

                Telerik.Web.UI.RadListView CarePlanListView = (Telerik.Web.UI.RadListView)currentProblemClassItem.FindControl ("CarePlanListView");

                foreach (Telerik.Web.UI.RadListViewDataItem currentCarePlanItem in CarePlanListView.Items) {

                    if (selectedCarePlans.Contains (Convert.ToInt64 (currentCarePlanItem.GetDataKeyValue ("Id")))) {

                        // ITEM WAS PREVIOUSLY EXPANDED BASED ON ID

                        currentCarePlanItem.Selected = true;

                    }

                }


            }

            return;

        }

        protected void ProblemClassListView_OnNeedDataSource (Object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e) {

            switch (e.RebindReason) {

                case Telerik.Web.UI.RadListViewRebindReason.InitialLoad:

                case Telerik.Web.UI.RadListViewRebindReason.ExplicitRebind:

                case Telerik.Web.UI.RadListViewRebindReason.PostBackEvent:

                    ProblemClassListView.DataSource = Case.ProblemClasses;

                    break;

            }

            return;

        }

        protected void CarePlanListView_OnSelectedIndexChanged (Object sender, EventArgs e) {

            // ASSIGN LOCAL REFERENCE FOR CARE PLAN LIST VIEW BECAUSE IT IS 

            // CONTAINED IN ANOTHER LIST VIEW, THAT LIST VIEW CONTAINS THE PROBLEM CLASS ITEMS

            Telerik.Web.UI.RadListView CarePlanListView = (Telerik.Web.UI.RadListView)sender;

            if (CarePlanListView.Items.Count == 0) { return; }

            Int64 problemClassId = 0;

            List<Int64> selectedCarePlans = new List<Int64> ();


            problemClassId = Convert.ToInt64 (ProblemClassListView.Items[((Telerik.Web.UI.RadListViewDataItem)CarePlanListView.Parent).DataItemIndex].GetDataKeyValue ("Id"));

            foreach (Telerik.Web.UI.RadListViewDataItem currentItem in CarePlanListView.SelectedItems) {

                selectedCarePlans.Add (Convert.ToInt64 (currentItem.GetDataKeyValue ("Id")));

            }


            // CACHE SELECTED CARE PLANS FOR PROBLEM CLASS

            SelectedCarePlans (problemClassId, selectedCarePlans);

            return;

        }

        #endregion


        #region Care Plan List View Events

        protected void CarePlanListView_OnNeedDataSource (Object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e) {

            Telerik.Web.UI.RadListView CarePlanListView = (Telerik.Web.UI.RadListView)sender;

            Telerik.Web.UI.RadListViewDataItem selectedProblemClassDataItem = (Telerik.Web.UI.RadListViewDataItem)CarePlanListView.Parent;

            Int32 selectedProblemClassIndex = selectedProblemClassDataItem.DataItemIndex;

            Client.Core.Individual.Case.MemberCaseProblemClass selectedProblemClass = Case.ProblemClasses[selectedProblemClassIndex];

            switch (e.RebindReason) {

                case Telerik.Web.UI.RadListViewRebindReason.InitialLoad:

                case Telerik.Web.UI.RadListViewRebindReason.ExplicitRebind:

                case Telerik.Web.UI.RadListViewRebindReason.PostBackEvent:

                    CarePlanListView.DataSource = selectedProblemClass.ProblemCarePlans;

                    restoreSelection = true;

                    break;

            }

            return;

        }

        protected void CarePlanListView_OnItemCommand (Object sender, Telerik.Web.UI.RadListViewCommandEventArgs e) {

            return;

        }

        protected void CarePlanListView_OnPreRender (Object sender, EventArgs e) {

            if (restoreSelection) {

                ProblemClassListView_ResetSelected ();

                ((Telerik.Web.UI.RadListView)sender).Rebind ();

            }

            return;

        }

        protected void CarePlanGoalGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e) {

            Telerik.Web.UI.RadGrid carePlanGoalGrid = (Telerik.Web.UI.RadGrid)sender;

            Telerik.Web.UI.RadListViewDataItem selectedCarePlanDataItem = (Telerik.Web.UI.RadListViewDataItem)carePlanGoalGrid.Parent;

            Int32 selectedCarePlanIndex = selectedCarePlanDataItem.DataItemIndex;


            Telerik.Web.UI.RadListView carePlanGoalListView = (Telerik.Web.UI.RadListView)selectedCarePlanDataItem.Parent.Parent;

            List<Client.Core.Individual.Case.MemberCaseProblemCarePlan> problemCarePlans = (List<Client.Core.Individual.Case.MemberCaseProblemCarePlan>)carePlanGoalListView.DataSource;

            switch (e.RebindReason) {

                case Telerik.Web.UI.GridRebindReason.InitialLoad:

                case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                    carePlanGoalGrid.DataSource = problemCarePlans[selectedCarePlanIndex].MemberCaseCarePlan.Goals;

                    break;

                case Telerik.Web.UI.GridRebindReason.PostBackEvent:

                    break;

            }

            return;

        }

        protected void CarePlanGoalGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs e) {

            Telerik.Web.UI.RadGrid carePlanGoalGrid = (Telerik.Web.UI.RadGrid)sender;

            if (carePlanGoalGrid == null) { return; }


            Telerik.Web.UI.RadListViewDataItem selectedCarePlanDataItem = (Telerik.Web.UI.RadListViewDataItem)carePlanGoalGrid.Parent;

            Int32 selectedCarePlanIndex = selectedCarePlanDataItem.DataItemIndex;

            Telerik.Web.UI.RadListView carePlanGoalListView = (Telerik.Web.UI.RadListView)selectedCarePlanDataItem.Parent.Parent;

            List<Client.Core.Individual.Case.MemberCaseProblemCarePlan> problemCarePlans = (List<Client.Core.Individual.Case.MemberCaseProblemCarePlan>)carePlanGoalListView.DataSource;


            switch (e.CommandName) {

                case "InitInsert": break;

                case "RebindGrid":
                    carePlanGoalGrid.DataSource = null;

                    carePlanGoalGrid.DataBind ();


                    break;

                case "Delete":

                    Int64 memberCaseCarePlanId = Convert.ToInt64 (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["MemberCaseCarePlanId"]);

                    Int64 memberCaseCarePlanGoalId = Convert.ToInt64 (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"]);


                    // problemCarePlans[selectedCarePlanIndex].CarePlan.Goals.RemoveAt (e.Item.ItemIndex);

                    carePlanGoalGrid.DataSource = null;

                    carePlanGoalGrid.DataBind ();

                    break;

                default: System.Diagnostics.Debug.WriteLine ("Item Command: " + e.CommandName); break;

            }

            return;

        }

        protected void CarePlanInterventionGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e) {

            Telerik.Web.UI.RadGrid carePlanInterventionGrid = (Telerik.Web.UI.RadGrid)sender;

            Telerik.Web.UI.RadListViewDataItem selectedCarePlanDataItem = (Telerik.Web.UI.RadListViewDataItem)carePlanInterventionGrid.Parent;

            Int32 selectedCarePlanIndex = selectedCarePlanDataItem.DataItemIndex;


            Telerik.Web.UI.RadListView carePlanInterventionListView = (Telerik.Web.UI.RadListView)selectedCarePlanDataItem.Parent.Parent;

            List<Client.Core.Individual.Case.MemberCaseProblemCarePlan> problemCarePlans = (List<Client.Core.Individual.Case.MemberCaseProblemCarePlan>)carePlanInterventionListView.DataSource;

            switch (e.RebindReason) {

                case Telerik.Web.UI.GridRebindReason.InitialLoad:

                    // carePlanInterventionGrid.DataSource = problemCarePlans[selectedCarePlanIndex].CarePlan.Interventions;

                    break;

                case Telerik.Web.UI.GridRebindReason.PostBackEvent:

                    break;

            }

            return;

        }

        #endregion


        #region Property Changes

        public void ProblemClassAssignedToUserSaveLink_OnClick (Object sender, EventArgs e) {

            // GET REFERENCE OF CURRENT SAVE LINK BUTTON

            System.Web.UI.WebControls.LinkButton currentSaveLinkButton = (System.Web.UI.WebControls.LinkButton)sender;

            // GET REFERENCE OF CURRENT PROBLEM CLASS RAD LIST VIEW ITEM

            Telerik.Web.UI.RadListViewItem currentProblemClassRadListViewItem = (Telerik.Web.UI.RadListViewItem)currentSaveLinkButton.Parent;

            // GET REFERENCE TO CURRENT PROBLEM CLASS ASSIGNED TO USER SELECTION

            Telerik.Web.UI.RadComboBox currentProblemClassAssignedToUserSelection = (Telerik.Web.UI.RadComboBox)currentProblemClassRadListViewItem.FindControl ("ProblemClassAssignedToUserSelection");

            // IF SELECTED VALUE OF CURRENT PROBLEM CLASS ASSIGNED TO USER SELECT IS NOT AN EMPTY STRING, THEN EVALUATE SELECTED VALUE

            if (currentProblemClassAssignedToUserSelection.SelectedValue != "") {

                // SET USER NAME AS TEXT OF SELECTED ITEM OF CURRENT PROBLEM CLASS ASSIGNED TO USER SELECTION

                String userName = currentProblemClassAssignedToUserSelection.SelectedItem.Text;

                // IF USER NAME IS NOT "NOT ASSIGNED", THEN GET ASSIGNED TO USER INFORMATION

                if (userName != "** Not Assigned") {

                    // SET SELECTED VALUE AS SELECTED VALUE OF CURRENT PROBLEM CLASS ASSIGNED TO USER SELECTION

                    String selectedValue = currentProblemClassAssignedToUserSelection.SelectedValue;

                    // SET ASSIGNED TO SECURITY AUTHORITY ID AS 0 INDEX OF SELECTED VALUE

                    Int64 assignedToSecurityAuthorityId = Convert.ToInt64 (selectedValue.Split ('|')[0]);

                    // SET ASSIGNED TO USER ACCOUNT ID AS 2 INDEX OF SELECTED VALUE

                    String assignedToUserAccountId = selectedValue.Split ('|')[1];

                    // SET MEMBER CASE PROBLEM CLASS ID AS 2 INDEX OF SELECTED VALUE

                    Int64 memberCaseProblemClassId = Convert.ToInt64 (selectedValue.Split ('|')[2]);

                    // SAVE MEMBER CASE PROBLEM CLASS ASSIGNMENT, ASSIGN USER TO MEMBER CASE PROBLEM CLASS

                    Mercury.Server.Application.MemberCaseModificationResponse response = new Mercury.Server.Application.MemberCaseModificationResponse ();

                    // ASSIGN MEMBER CASE PROBLEM CLASS TO USER

                    response = MercuryApplication.MemberCaseProblemClass_AssignToUser (Case, memberCaseProblemClassId,

                        assignedToSecurityAuthorityId, assignedToUserAccountId, userName, userName);

                    // IF RESPOSNE IS NOT NULL, THEN EVALUATE IF EXCEPTION EXISTS

                    if (response != null) {

                        // IF RESPONSE HAS EXCEPTION, THEN SET EXCEPTION MESSAGE OF PARENT MEMBER CASE PAGE AS MESAGE OF EXCEPTION OF RESPONSE

                        if (response.HasException) { ParentMemberCasePage.ExceptionMessage = response.Exception.Message; }

                        // UPDATE FORM FROM UPDATED CASE RECEVIED THROUGH RESPONSE

                        if (response.MemberCase != null) {

                            // RELOAD MEMBER CASE

                            Case = new Client.Core.Individual.Case.MemberCase (MercuryApplication, response.MemberCase);

                        }

                    }

                }

                // IF USER NAME IS NOT ASSIGNED, THEN CHECK IF CURRENT MEMBER PROBLEM CLASS IS CURRENTLY NOT ASSIGNED, IF NOT THEN SET CURRENT MEMBER PROBLEM CLASS AS NOT ASSIGNED

                else {

                    // SET SELECTED VALUE AS SELECTED VALUE OF CURRENT PROBLEM CLASS ASSIGNED TO USER SELECTION

                    String selectedValue = currentProblemClassAssignedToUserSelection.SelectedValue;

                    // SET MEMBER CASE PROBLEM CLASS ID AS 2 INDEX OF SELECTED VALUE

                    Int64 memberCaseProblemClassId = Convert.ToInt64 (selectedValue.Split ('|')[2]);

                    // SAVE MEMBER CASE PROBLEM CLASS ASSIGNMENT, UNASSIGN USER MEMBER CASE PROBLEM CLASS ASSIGNMENT

                    Mercury.Server.Application.MemberCaseModificationResponse response = new Server.Application.MemberCaseModificationResponse ();

                    // ASSIGN MEMBER CASE PROBLEM CLASS TO USER

                    response = MercuryApplication.MemberCaseProblemClass_AssignToUser (Case, memberCaseProblemClassId, 0, String.Empty, String.Empty, String.Empty);

                    // IF RESPOSNE IS NOT NULL, THEN EVALUATE IF EXCEPTION EXISTS

                    if (response != null) {

                        // IF RESPONSE HAS EXCEPTION, THEN SET EXCEPTION MESSAGE OF PARENT MEMBER CASE PAGE AS MESAGE OF EXCEPTION OF RESPONSE

                        if (response.HasException) { ParentMemberCasePage.ExceptionMessage = response.Exception.Message; }

                        // UPDATE FORM FROM UPDATED CASE RECEVIED THROUGH RESPONSE

                        if (response.MemberCase != null) {

                            // RELOAD MEMBER CASE

                            Case = new Client.Core.Individual.Case.MemberCase (MercuryApplication, response.MemberCase);

                        }

                    }

                }

            }

            return;

        }

        public void ProblemClassAssignedToProviderSaveLink_OnClick (Object sender, EventArgs e) {

            // GET REFERENCE TO CURRENT SAVE LINK BUTTON

            System.Web.UI.WebControls.LinkButton currentSaveLinkButton = (System.Web.UI.WebControls.LinkButton)sender;

            // GET REFERENCE TO CURRENT PROBLEM CLASS RAD LIST VIEW ITEM

            Telerik.Web.UI.RadListViewItem currentProblemClassRadListViewItem = (Telerik.Web.UI.RadListViewItem)currentSaveLinkButton.Parent;

            // GET REFERENCE TO CURRENT PROBLEM CLASS ASSIGNED TO PROVIDER SELECTION RAD COMBO BOX

            Telerik.Web.UI.RadComboBox currentProblemClassAssignedToProviderSelection = (Telerik.Web.UI.RadComboBox)currentProblemClassRadListViewItem.FindControl ("ProblemClassAssignedToProviderSelection");

            // CREATE REFERENCE TO SELECTED PROVIDER ID AND DEFAULT IT TO 0

            Int64 selectedProviderId = 0;

            // IF SELECTED VALUE OF CURRENT PROBLEM CLASS ASSIGNED TO PROVIDER SELECTION RAD COMBO BOX IS NOT AN EMPTY STRING, THEN TRY TO PARSE SELECTED VALUE TO INT64

            if (currentProblemClassAssignedToProviderSelection.SelectedValue != "") {

                // SET SELECTED VALUE AS SELECTED VALUE OF CURRENT PROBLEM CLASS ASSIGNED TO PROVIDER SELECTION

                String selectedValue = currentProblemClassAssignedToProviderSelection.SelectedValue;

                // CREATE REFERENCE TO PARSED SELECTED PROVIDER ID AND DEFAULT IT TO 0

                Int64 parsedSelectedProviderId = 0;

                // IF SELECTED VALUE OF CURRENT PROBLAM CLASS ASSIGNED TO PROVIDER SELECTION CAN BE PARSED AS AN INT64, THEN SET SELECTED PROVIDER ID TO PARSED VALUE OF SELECTED VALUE OF CURRENT PROBLAM CLASS ASSIGNED TO PROVIDER SELECTION

                if (Int64.TryParse (selectedValue.Split ('|')[0], out (parsedSelectedProviderId))) {

                    // SET SELECTED PROVIDER ID TO PARSED VALUE OF SELECTED VALUE OF CURRENT PROBLAM CLASS ASSIGNED TO PROVIDER SELECTION

                    selectedProviderId = Convert.ToInt64 (selectedValue.Split ('|')[0]);

                }

                // CREATE REFERENCE TO MEMBER CASE PROBLEM CLASS ID

                Int64 memberCaseProblemClassId = 0;

                // CREATE REFERENCE TO PARSED MEMBER CASE PROBLEM CLASS ID

                Int64 parsedMemberCaseProblemClassId = 0;

                // TRY TO PARSE FIRST INDEX OF SPIT STRING OF SELECTED VALUE AS INT64

                if (Int64.TryParse (selectedValue.Split ('|')[1], out (parsedMemberCaseProblemClassId))) {

                    // SET MEMBER CASE PROBLEM CLASS ID TO INT64 OF FIRST INDEX OF SPLIT STRING OF SELECTED VALUE

                    memberCaseProblemClassId = Convert.ToInt64 (selectedValue.Split ('|')[1]);
                }

                // IF UNASSIGNING PROVIDER FOR MEMBER CASE PROBLEM CLASS (SELECTED PTOVIDER ID IS 0), 

                if (selectedProviderId == 0) {

                    // CREATE REFERENCE TO MEMBER CASE MODIFICATION RESPONSE

                    Mercury.Server.Application.MemberCaseModificationResponse response = new Mercury.Server.Application.MemberCaseModificationResponse ();

                    // ASSIGN MEMBER CASE PROBLEM CLASS TO PROVIDER

                    response = MercuryApplication.MemberCaseProblemClass_AssignToProvider (Case, memberCaseProblemClassId, selectedProviderId);

                    // IF RESPOSNE IS NOT NULL, THEN EVALUATE IF EXCEPTION EXISTS

                    if (response != null) {

                        // IF RESPONSE HAS EXCEPTION, THEN SET EXCEPTION MESSAGE OF PARENT MEMBER CASE PAGE AS MESAGE OF EXCEPTION OF RESPONSE

                        if (response.HasException) { ParentMemberCasePage.ExceptionMessage = response.Exception.Message; }

                        // UPDATE FORM FROM UPDATED CASE RECEVIED THROUGH RESPONSE

                        if (response.MemberCase != null) {

                            // RELOAD MEMBER CASE

                            Case = new Client.Core.Individual.Case.MemberCase (MercuryApplication, response.MemberCase);

                        }

                    }

                }

                // IF ASSIGNING PROVIDER FOR MEMBER CASE PROBLEM CLASS (SELECTED PROVIDER ID IS NOT 0), 

                else {

                    // GET REFERENCE TO MEMBER CASE MODIFICATION REPONSE

                    Mercury.Server.Application.MemberCaseModificationResponse response = new Mercury.Server.Application.MemberCaseModificationResponse ();

                    // TRY TO ASSIGN MEMBER CASE PROBLEM CLASS TO PROVIDER

                    response = MercuryApplication.MemberCaseProblemClass_AssignToProvider (Case, memberCaseProblemClassId, selectedProviderId);

                    // IF RESPOSNE IS NOT NULL, THEN EVALUATE IF EXCEPTION EXISTS

                    if (response != null) {

                        // IF RESPONSE HAS EXCEPTION, THEN SET EXCEPTION MESSAGE OF PARENT MEMBER CASE PAGE AS MESAGE OF EXCEPTION OF RESPONSE

                        if (response.HasException) { ParentMemberCasePage.ExceptionMessage = response.Exception.Message; }

                        // UPDATE FORM FROM UPDATED CASE RECEVIED THROUGH RESPONSE

                        if (response.MemberCase != null) {

                            // RELOAD MEMBER CASE

                            Case = new Client.Core.Individual.Case.MemberCase (MercuryApplication, response.MemberCase);

                        }

                    }

                }

            }

            return;

        }

        public void ProblemClassAssignedToProviderSelection_OnItemsRequested (object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e) {

            // GET REFERENCE TO CURRENT PROBLEM CLASS ASSIGNED TO PROVIDER SELECT RAD COMBO BOX THROUGH SENDER ARGUMENT

            Telerik.Web.UI.RadComboBox currentProblemClassAssignedToProviderSelection = (Telerik.Web.UI.RadComboBox)sender;

            // DEFAULT CURRENT MEMBER CASE PROBLEM CLASS ID TO 0

            Int64 currentMemberCaseProblemClassId = 0;

            // SET MEMBER CASE PROBLEM CLASS ID TO ZERO INDEX OF SLPIT STRING OF VALUE OF FZERO INDEX ITEM OF CURRENT PROBLEM CLASS ASSIGNED TO PROVIDER SELECTION (GETS ID FROM VALUE OF NOT ASSIGNED ITEM)

            currentMemberCaseProblemClassId = Convert.ToInt64 (currentProblemClassAssignedToProviderSelection.Items[0].Value.Split ('|')[1]);

            // EVALUATE IF ENTERED TEXT IS GREATER THAN 3 IN LENGTH (HANDLED IN "ON CLIENT ITEM REQUESTING" CLIENT-SIDE EVENT)

            if (e.Text.Length >= 3) {

                // CREATE REFERENCE TO SEARCH RESULTS PROVIDER RESPONSE OBJECT

                Mercury.Server.Application.SearchResultsProviderResponse providerSearchResponse;

                // GET PROVIDER SEARCH RESPONSE THROUGH SEARCH PROVIDER METHOD

                providerSearchResponse = MercuryApplication.SearchProvider (e.Text, e.Text);

                // EVALUATE IF PROVIDER SEARCH RESPONSE HAS EXCEPTION, IF SO ADD RAD COMBO BOX ITEM TO CURRENT PROBLEM CLASS ASSIGNED TO PROVIDER SELECTION RAD COMBO BOX TO INFORM USER OF EXCEPTION

                if (providerSearchResponse.HasException) {

                    // ADD RAD COMBO BOX ITEM TO CURRENT PROBLEM CLASS ASSIGNED TO PROVIDER SELECTION RAD COMBO BOX TO INFORM USER OF EXCEPTION

                    currentProblemClassAssignedToProviderSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (("alert ('" + providerSearchResponse.Exception.Message.Replace ("'", "''") + "');"), "0"));

                }

                // IF PROVIDER SEARCH RESPONSE DOES NOT HAVE EXCEPTION, THAN ADD RAD COMBO BOX ITEMS TO CURRENT PROBLEM CLASS ASSIGNED TO PROVIDER SELECTION RAD COMBO BOX

                else {

                    // GET AND SET SEARCH RESULTS PROVIDERS ARRAY AS RESULTS OF PROVIDER SEARCH RESPONSE

                    Mercury.Server.Application.SearchResultProvider[] searchResultProviders = providerSearchResponse.Results;

                    // IF COUNT OF SEARCH RESULT PROVIDERS IS GREATER THAN OR EQUAL TO 1 AND LESS THAN OR EQUAL TO 25, THEN ADD RAD COMBO BOX ITEMS FOR EACH PROVIDER IN SEARCH RESULT PROVIDERS

                    if ((searchResultProviders.Count () >= 1) && (searchResultProviders.Count () <= 25)) {

                        // LOOP THROUGH EACH SEARCH RESULT PROVIDER IN SEARCH RESULTS PROVIDERS, THEN ADD RAD COMBO BOX ITEMS FOR EACH PROVIDER INTO ITEMS OF CURRENT PROBLEM CLASS ASSIGNED TO PROVIDER SELECTION RAD COMBO BOX

                        foreach (Mercury.Server.Application.SearchResultProvider currentSearchResultProvider in searchResultProviders) {

                            // CREATE REFERENCE TO NEW RAD COMBO BOX ITEM WITH TEXT AS NAME OF CURRENT PROVIDER AND VALUE AS PROVIDER ID OF CURRENT PROVIDER

                            Telerik.Web.UI.RadComboBoxItem newRadComboBoxItem = new Telerik.Web.UI.RadComboBoxItem (currentSearchResultProvider.Name, currentSearchResultProvider.ProviderId.ToString () + "|" + currentMemberCaseProblemClassId);

                            // ADD NEW RAD COMBO BOX ITEMS TO CURRENT PROBLEM CLASS ASSIGNED TO PROVIDER SELECTION RAD COMBO BOX

                            currentProblemClassAssignedToProviderSelection.Items.Add (newRadComboBoxItem);

                        } /* END FOREACH */

                    }

                    // IF COUNT OF SEARCH RESULT PROVIDERS IS GREATER THAN 25, THEN ADD RAD COMBO BOX ITEM THAT INDICATES TO USER THAT TOO MANY PROVIDERS WERE RETURNED

                    else if (searchResultProviders.Count () > 25) {

                        // CREATE REFERENCE TO NEW RAD COMBO BOX ITEM WITH TEXT AS EXPLANATION TO USER THAT TOO MANY PROVIDERS WERE RETURNED AND VALUE AS 0

                        Telerik.Web.UI.RadComboBoxItem newRadComboBoxItem = new Telerik.Web.UI.RadComboBoxItem ("**Too many providers returned (" + searchResultProviders.Count ().ToString () + ". Please narrow search by entering more letters.", "0|0");

                        // ADD NEW RAD COMBO BOX ITEMS TO CURRENT PROBLEM CLASS ASSIGNED TO PROVIDER SELECTION RAD COMBO BOX

                        currentProblemClassAssignedToProviderSelection.Items.Add (newRadComboBoxItem);

                    }

                    // IF COUNT OF SEARCH RESULT PROVIDERS IS EQUAL TO 0, THEN ADD RAD COMBO BOX ITEM THAT INDICATES TO USER THAT NO PROVIDERS WERE RETURNED

                    else if (searchResultProviders.Count () == 0) {

                        // CREATE REFERENCE TO NEW RAD COMBO BOX ITEM WITH TEXT AS EXPLANATION TO USER THAT NO PROVIDERS WERE RETURNED AND VALUE AS 0

                        Telerik.Web.UI.RadComboBoxItem newRadComboBoxItem = new Telerik.Web.UI.RadComboBoxItem ("**No providers returned. Please widen search by entering fewer letters.", "0|0");

                        // ADD NEW RAD COMBO BOX ITEMS TO CURRENT PROBLEM CLASS ASSIGNED TO PROVIDER SELECTION RAD COMBO BOX

                        currentProblemClassAssignedToProviderSelection.Items.Add (newRadComboBoxItem);

                    }

                }

            }

            return;

        }

        #endregion


    }

}