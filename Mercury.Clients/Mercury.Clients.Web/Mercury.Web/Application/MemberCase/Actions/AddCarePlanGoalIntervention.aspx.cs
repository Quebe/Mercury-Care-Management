using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.MemberCase.Actions {

    public partial class AddCarePlanGoalIntervention : System.Web.UI.Page {

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

        public Int64 MemberCaseCarePlanGoalId {

            get {

                if (Session[SessionCachePrefix + "MemberCaseCarePlanGoalId"] == null) { return 0; }
            
                return (Int64) Session[SessionCachePrefix + "MemberCaseCarePlanGoalId"]; 
            
            }

            set {

                if (MemberCaseCarePlanGoalId != value) {

                    Session[SessionCachePrefix + "MemberCaseCarePlanGoalId"] = value;

                }

            }

        }

        public Client.Core.Individual.Case.MemberCaseCarePlanGoal MemberCaseCarePlanGoal { get { return Case.FindMemberCaseCarePlanGoal (MemberCaseCarePlanGoalId); } }

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

            Int64 forMemberCaseCarePlanGoalId = 0;


            if (MercuryApplication == null) { return; }


            if (!Page.IsPostBack) {

                #region Initial Page Load

                if (Request.QueryString["MemberCaseId"] != null) {

                    if (Int64.TryParse ((String)Request.QueryString["MemberCaseId"], out forMemberCaseId)) {

                        Case = MercuryApplication.MemberCaseGet (forMemberCaseId, false);

                    }

                }

                if (Request.QueryString["MemberCaseCarePlanGoalId"] != null) {

                    if (!Int64.TryParse ((String)Request.QueryString["MemberCaseCarePlanGoalId"], out forMemberCaseCarePlanGoalId)) {

                        Server.Transfer ("/PermissionDenied.aspx"); return;

                    }

                }

                if (Case == null) { Server.Transfer ("/PermissionDenied.aspx"); return; }

                MemberCaseCarePlanGoalId = forMemberCaseCarePlanGoalId;

                if (Case.FindMemberCaseCarePlanGoal (forMemberCaseCarePlanGoalId) == null) { Server.Transfer ("/PermissionDenied.aspx"); return; }

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

            InitializeCarePlanGoal ();

            InitializeCareInterventionTreeView ();

            return;

        }

        private void InitializeCarePlanGoal () {

            if (MemberCaseCarePlanGoal == null) { return; }

            MemberCaseCarePlanGoalName.Text = MemberCaseCarePlanGoal.Name;

            MemberCaseCarePlanGoalClinicalNarrative.Text = MemberCaseCarePlanGoal.ClinicalNarrative;



            List<Client.Core.Individual.Case.MemberCaseProblemCarePlan> carePlanProblems =

                (from currentProblem in MemberCaseCarePlanGoal.MemberCaseCarePlan.Problems

                 select currentProblem).ToList ();

            CarePlanProblemsListView.DataSource = carePlanProblems;

            CarePlanProblemsListView.DataBind ();

            return;

        }

        private void InitializeMember () {

            Client.Core.Member.Member member = Case.Member;

            if (member == null) { return; }


            ApplicationTitle.InnerText = member.Name + " (" + member.CurrentAge + " | " + member.GenderDescription + ((member.MostRecentEnrollment != null) ? " | " + member.MostRecentEnrollment.ProgramMemberId : String.Empty) + ")";

            ApplicationTitle.HRef = @"/Application/Member/MemberProfile.aspx?MemberId=" + member.Id.ToString ();

            return;

        }

        private void InitializeCareInterventionTreeView () {

            Client.Core.Individual.CarePlanGoal originalCarePlanGoal = MemberCaseCarePlanGoal.MemberCaseCarePlan.CarePlan.CarePlanGoal (MemberCaseCarePlanGoal.CarePlanGoalId);
            
            Telerik.Web.UI.RadTreeNode careInterventionNode;


            #region Create Root Nodes

            Telerik.Web.UI.RadTreeNode rootCurrentNode = new Telerik.Web.UI.RadTreeNode ("Current", "RootCurrent");

            rootCurrentNode.Checkable = false;

            rootCurrentNode.Expanded = false;

            CareInterventionTreeView.Nodes.Add (rootCurrentNode);


            Telerik.Web.UI.RadTreeNode rootRequiredNode = new Telerik.Web.UI.RadTreeNode ("Required", "RootRequired");

            rootRequiredNode.Checkable = false;

            rootRequiredNode.Expanded = true;

            CareInterventionTreeView.Nodes.Add (rootRequiredNode);


            Telerik.Web.UI.RadTreeNode rootSuggestedNode = new Telerik.Web.UI.RadTreeNode ("Suggested", "RootSuggested");

            rootSuggestedNode.Checkable = false;

            rootSuggestedNode.Expanded = true;

            CareInterventionTreeView.Nodes.Add (rootSuggestedNode);


            Telerik.Web.UI.RadTreeNode rootOptionalNode = new Telerik.Web.UI.RadTreeNode ("Optional", "RootOptional");

            rootOptionalNode.Checkable = false;

            rootOptionalNode.Expanded = false;

            CareInterventionTreeView.Nodes.Add (rootOptionalNode);


            Telerik.Web.UI.RadTreeNode rootAllNode = new Telerik.Web.UI.RadTreeNode ("All Others", "RootAll");

            rootAllNode.Checkable = false;

            rootAllNode.Expanded = false;

            CareInterventionTreeView.Nodes.Add (rootAllNode);

            #endregion 


            List<Client.Core.Individual.CareIntervention> careInterventionsCurrent =

                (from currentCarePlanGoalIntervention in MemberCaseCarePlanGoal.Interventions

                 where

                    // ALTERED TO REMOVE THOSE WHERE CARE INTERVENTION IS NULL

                    (currentCarePlanGoalIntervention.CareIntervention != null) && (

                    (currentCarePlanGoalIntervention.CareIntervention != null ?
                 
                   ((currentCarePlanGoalIntervention.CareIntervention.Status == Mercury.Server.Application.CaseItemStatus.UnderDevelopment)

                    || (currentCarePlanGoalIntervention.CareIntervention.Status == Mercury.Server.Application.CaseItemStatus.Active))

                    : (currentCarePlanGoalIntervention.Id == currentCarePlanGoalIntervention.Id)))

                    select currentCarePlanGoalIntervention.CareIntervention.CareIntervention
                 
                 ).Distinct ().ToList ();


            foreach (Client.Core.Individual.CareIntervention currentCareIntervention in careInterventionsCurrent) {

                // EXISTING CARE INTERVENTIONS WILL HAVE "Checked" TRUE AND "ForeColor" System.Drawing.Color.Black

                // IF THE CARE INTERVENTION EXISTS AND IS ACTIVE IN THE CASE ALREADY, DO NOT ALLOW IT TO BE ADDED AGAIN

                careInterventionNode = new Telerik.Web.UI.RadTreeNode (currentCareIntervention.Name, currentCareIntervention.Id.ToString ());

                careInterventionNode.Checkable = false;

                careInterventionNode.ImageUrl = "/Images/Common16/Check.png";

                careInterventionNode.ForeColor = System.Drawing.Color.Black;


                // ADD EARLY SO THAT EXPAND TO NODE WORKS (PARENT RELATIONSHIP REQUIRED)

                rootCurrentNode.Nodes.Add (careInterventionNode);


            }
            
            
            List<Client.Core.Individual.CareIntervention> careInterventionsAvailable = MercuryApplication.CareInterventionsAvailable (true);

            careInterventionsAvailable =

                (from currentCareIntervention in careInterventionsAvailable

                 where (currentCareIntervention.Enabled == true)

                 orderby currentCareIntervention.Name

                 select currentCareIntervention).ToList ();



            foreach (Client.Core.Individual.CareIntervention currentCareIntervention in careInterventionsAvailable) {

                // MAKE SURE THAT THE NODE DOES NOT ALREADY EXIST (FROM THE "CURRENT" SELECTION)

                if (CareInterventionTreeView.FindNodeByValue (currentCareIntervention.Id.ToString ()) == null) {

                    Telerik.Web.UI.RadTreeNode parentNode = rootAllNode;


                    if (originalCarePlanGoal != null) { // IT IS POSSIBLE THAT THE GOAL WAS A CUSTOM CREATED GOAL

                        // USE THE ORIGINAL CARE PLAN OF THE CARE PLAN GOAL TO CHECK TO SEE IF THE INTERVENTION IS REQUIRED/SUGGESTED/OPTIONAL IN THE ORIGINAL    

                        Client.Core.Individual.CarePlanIntervention originalCarePlanIntervention = originalCarePlanGoal.CarePlanIntervention (currentCareIntervention.Id);


                        if (originalCarePlanIntervention != null) {

                            switch (originalCarePlanIntervention.Inclusion) {

                                case Mercury.Server.Application.CarePlanItemInclusion.Required: parentNode = rootRequiredNode; break;

                                case Mercury.Server.Application.CarePlanItemInclusion.Suggested: parentNode = rootSuggestedNode; break;

                                case Mercury.Server.Application.CarePlanItemInclusion.Optional: parentNode = rootOptionalNode; break;

                            }

                        }

                    }

                    careInterventionNode = new Telerik.Web.UI.RadTreeNode (currentCareIntervention.Name, currentCareIntervention.Id.ToString ());

                    parentNode.Nodes.Add (careInterventionNode);


                    // IDENTIFY CARE INTERVENTIONS THAT ARE ALREADY IN USE BY CURRENT CARE PLAN OR OTHER CARE PLAN

                    List<Client.Core.Individual.Case.MemberCaseProblemCarePlan> memberCaseProblemCarePlans = 

                        (from currentMemberCaseCarePlan in Case.CarePlans

                         from currentMemberCaseCarePlanGoal in currentMemberCaseCarePlan.Goals

                         from currentMemberCaseCarePlanGoalIntervention in currentMemberCaseCarePlanGoal.Interventions

                         from currentProblems in currentMemberCaseCarePlanGoalIntervention.MemberCaseCarePlanGoal.MemberCaseCarePlan.Problems

                         // ALTERED TO CHECK IF CARE INTERVENTION IS NULL

                         where ((currentMemberCaseCarePlanGoalIntervention.CareIntervention != null) ? (currentMemberCaseCarePlanGoalIntervention.CareIntervention.CareInterventionId == currentCareIntervention.Id)

                            && ((currentMemberCaseCarePlanGoalIntervention.CareIntervention.Status == Mercury.Server.Application.CaseItemStatus.UnderDevelopment))

                                || (currentMemberCaseCarePlanGoalIntervention.CareIntervention.Status == Mercury.Server.Application.CaseItemStatus.Active) : (currentMemberCaseCarePlanGoalIntervention.Id == currentMemberCaseCarePlanGoalIntervention.Id))

                         select currentProblems).Distinct ().ToList ();

                    foreach (Client.Core.Individual.Case.MemberCaseProblemCarePlan currentMemberCaseProblemCarePlan in memberCaseProblemCarePlans) {

                        Telerik.Web.UI.RadTreeNode problemNode = new Telerik.Web.UI.RadTreeNode (currentMemberCaseProblemCarePlan.ProblemStatementClassificationWithName, currentCareIntervention.Id.ToString () + "|" + currentMemberCaseProblemCarePlan.Id.ToString ());

                        problemNode.Checkable = false;

                        careInterventionNode.Nodes.Add (problemNode);

                    }

                }

            }

            return;

        }

        #endregion


        #region Problem Statement Tree View Events

        private void RadTreeView_ExpandToNode (Telerik.Web.UI.RadTreeNode forNode) {

            forNode.Expanded = true;

            if (forNode.ParentNode != null) { RadTreeView_ExpandToNode (forNode.ParentNode); }

            return;

        }

        private void RadTreeView_MakeVisibleToNode (Telerik.Web.UI.RadTreeNode forNode) {

            forNode.Visible = true;

            if (forNode.ParentNode != null) { RadTreeView_MakeVisibleToNode (forNode.ParentNode); }

            return;

        }


        private void RadTreeView_MakeChildrenVisibleToNode (Telerik.Web.UI.RadTreeNode forNode) {

            if (forNode == null) { return; }

            foreach (Telerik.Web.UI.RadTreeNode currentChildNode in forNode.Nodes) {

                currentChildNode.Visible = true;

                RadTreeView_MakeChildrenVisibleToNode (currentChildNode);

            }

            return;

        }

        protected void CareInterventionTreeView_OnNodeClick (Object sender, Telerik.Web.UI.RadTreeNodeEventArgs e) {

            Int64 careInterventionId = 0;

            if (Int64.TryParse (e.Node.Value, out careInterventionId)) {

                Client.Core.Individual.CareIntervention careIntervention = MercuryApplication.CareInterventionGet (careInterventionId, true);


                CareInterventionName.Text = careIntervention.Name;

                CareInterventionDescription.Text = careIntervention.Description;


                // IDENTIFY CARE INTERVENTIONS THAT ARE ALREADY IN USE BY CURRENT CARE PLAN OR OTHER CARE PLAN

                List<Client.Core.Individual.Case.MemberCaseProblemCarePlan> memberCaseProblemCarePlans =

                    (from currentMemberCaseCarePlan in Case.CarePlans

                     from currentMemberCaseCarePlanGoal in currentMemberCaseCarePlan.Goals

                     from currentMemberCaseCarePlanGoalIntervention in currentMemberCaseCarePlanGoal.Interventions

                     from currentProblems in currentMemberCaseCarePlanGoalIntervention.MemberCaseCarePlanGoal.MemberCaseCarePlan.Problems

                     where

                     // ALTERED TO CHECK IF CARE INTERVENTION IS NULL

                     ((currentMemberCaseCarePlanGoalIntervention.CareIntervention != null) &&

                     ((currentMemberCaseCarePlanGoalIntervention.CareIntervention != null) ?
                     
                     (currentMemberCaseCarePlanGoalIntervention.CareIntervention.CareInterventionId == careIntervention.Id)

                        && ((currentMemberCaseCarePlanGoalIntervention.CareIntervention.Status == Mercury.Server.Application.CaseItemStatus.UnderDevelopment)

                            || (currentMemberCaseCarePlanGoalIntervention.CareIntervention.Status == Mercury.Server.Application.CaseItemStatus.Active))

                      : (currentMemberCaseCarePlanGoalIntervention.Id == currentMemberCaseCarePlanGoalIntervention.Id))

                    )

                     select currentProblems).Distinct ().ToList ();


                CareInterventionProblemsListView.DataSource = memberCaseProblemCarePlans;

                CareInterventionProblemsListView.DataBind ();


                // LINK UP ACTIVITIES 

                CareInterventionActivitiesListView.DataSource = careIntervention.Activities;

                CareInterventionActivitiesListView.DataBind ();

                return;


            }

            return;

        }

        protected void CareInterventionFilter_OnClick (Object sender, EventArgs e) {

            CareInterventionTreeView.UnselectAllNodes ();

            foreach (Telerik.Web.UI.RadTreeNode currentNode in CareInterventionTreeView.GetAllNodes ()) {

                if (currentNode.Text.ToUpper ().Contains (CareInterventionFilterText.Text.ToUpper ())) {

                    currentNode.Visible = true;

                    currentNode.Selected = true;
                    
                    RadTreeView_ExpandToNode (currentNode);

                    RadTreeView_MakeVisibleToNode (currentNode);

                }

                else {

                    currentNode.Visible = false;

                }

            }

            // SECOND CYCLE THROUGH, MAKE CHILDREN VISIBLE FROM VISIBLE PARENTS

            foreach (Telerik.Web.UI.RadTreeNode currentNode in CareInterventionTreeView.GetAllNodes ()) {

                if (currentNode.Visible) {

                    RadTreeView_MakeChildrenVisibleToNode (currentNode);

                }

                // REMOVE THOSE NODES FROM VIEW THAT HAVE CHILDREN BUT NONE ARE EXPANDED

                if ((!currentNode.Expanded) && (currentNode.Nodes.Count > 0)) {

                    currentNode.Visible = false;

                }

            }

            return;

        }

        protected void CareInterventionFilterClear_OnClick (Object sender, EventArgs e) {

            foreach (Telerik.Web.UI.RadTreeNode currentNode in CareInterventionTreeView.GetAllNodes ()) {

                currentNode.Visible = true;

                currentNode.Selected = false;

            }

            return;

        }

        #endregion 


        #region Button Event Handlers

        protected Boolean ApplyChanges () {

            Boolean isModified = false;

            Boolean success = false;

            List<Int64> careInterventionIds = new List<Int64> (); // CARE INTERVENTIONS TO ADD

            Int64 careInterventionId = 0;

            Mercury.Server.Application.MemberCaseModificationResponse response;


            // IDENTIFY ALL REQUESTED CARE INTERVENTIONS TO ADD

            foreach (Telerik.Web.UI.RadTreeNode currentNode in CareInterventionTreeView.GetAllNodes ()) {

                // VALIDATE THAT THE NODE IS CHECKED AND REQUESTED TO BE ADDED TO THE GOAL

                if (currentNode.Checked) {

                    // VALIDATE THAT THE NODE IS A CARE INTERVENTION NODE

                    if (Int64.TryParse (currentNode.Value, out careInterventionId)) {

                        careInterventionIds.Add (careInterventionId);

                    }

                }

            }

            isModified = (careInterventionIds.Count > 0);

            if (isModified) {

                success = true;

                foreach (Int64 currentCareInterventionId in careInterventionIds) {

                    // TODO: ADD SINGLE INSTANCE SUPPORT HERE

                    response = MercuryApplication.MemberCaseCarePlanGoal_AddCareIntervention (Case, MemberCaseCarePlanGoal.Id, currentCareInterventionId, false);

                    Case = new Client.Core.Individual.Case.MemberCase (MercuryApplication, response.MemberCase);

                    if (response.HasException) {

                        ExceptionMessage = response.Exception.Message;

                        success = false;

                        // REBUILD TREE AND SET ERROR NODE ACTIVE 

                        CareInterventionTreeView.Nodes.Clear ();

                        InitializeCareInterventionTreeView ();

                        Telerik.Web.UI.RadTreeNode careInterventionNode = CareInterventionTreeView.FindNodeByValue (currentCareInterventionId.ToString ());

                        if (careInterventionNode != null) {

                            careInterventionNode.Expanded = true;

                            careInterventionNode.ParentNode.Expanded = true; // ONLY TWO LEVELS DEEP

                            careInterventionNode.Selected = true;

                        }

                        break;

                    }

                }

                if (success) { // IF ALL PROBLEMS WERE ADDED WITHOUT PROBLEMS, REFRESH TREE

                    CareInterventionTreeView.Nodes.Clear ();

                    InitializeCareInterventionTreeView ();

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