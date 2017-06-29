using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.MemberCase.Actions {

    public partial class AddProblemStatement : System.Web.UI.Page {


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


            if (MercuryApplication == null) { return; }


            if (!Page.IsPostBack) {

                #region Initial Page Load

                if (Request.QueryString["MemberCaseId"] != null) {

                    if (Int64.TryParse ((String)Request.QueryString["MemberCaseId"], out forMemberCaseId)) {

                        Case = MercuryApplication.MemberCaseGet (forMemberCaseId, false);

                    }

                }


                if (Case == null) { Server.Transfer ("/PermissionDenied.aspx"); return; }

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

            InitializeProblemStatementTreeView ();

            return;

        }

        private void InitializeMember () {

            Client.Core.Member.Member member = Case.Member;

            if (member == null) { return; }


            ApplicationTitle.InnerText = member.Name + " (" + member.CurrentAge + " | " + member.GenderDescription + ((member.MostRecentEnrollment != null) ? " | " + member.MostRecentEnrollment.ProgramMemberId : String.Empty) + ")";

            ApplicationTitle.HRef = @"/Application/Member/MemberProfile.aspx?MemberId=" + member.Id.ToString ();

            return;

        }

        private void InitializeProblemStatementTreeView () {

            List<Client.Core.Individual.ProblemStatement> problemStatementsAvailable = MercuryApplication.ProblemStatementsAvailable (true);

            List<Client.Core.Individual.MemberProblemStatementIdentified> memberProblemStatementidentifiedAvailable = MercuryApplication.MemberProblemStatementIdentifiedAvailable (Case.MemberId, false, false);

            problemStatementsAvailable =

                (from currentProblemStatement in problemStatementsAvailable

                orderby currentProblemStatement.Classification, currentProblemStatement.Name

                select currentProblemStatement).ToList ();


            Dictionary<Int64, Client.Core.Individual.ProblemStatement> problemStatementsActive = Case.ProblemStatementsActive;


            foreach (Client.Core.Individual.ProblemStatement currentProblemStatement in problemStatementsAvailable) {

                Telerik.Web.UI.RadTreeNode domainNode = ProblemStatementTreeView.FindNodeByValue ("ProblemDomain_" + currentProblemStatement.ProblemDomainId);

                Telerik.Web.UI.RadTreeNode classNode = ProblemStatementTreeView.FindNodeByValue ("ProblemClass_" + currentProblemStatement.ProblemDomainId + "_" + currentProblemStatement.ProblemClassId);

                Telerik.Web.UI.RadTreeNode problemStatementNode = new Telerik.Web.UI.RadTreeNode (currentProblemStatement.Name, currentProblemStatement.Id.ToString ());


                if (domainNode == null) {

                    domainNode = new Telerik.Web.UI.RadTreeNode (currentProblemStatement.ProblemDomainName, "ProblemDomain_" + currentProblemStatement.ProblemDomainId);

                    domainNode.Checkable = false;

                    ProblemStatementTreeView.Nodes.Add (domainNode);

                }

                if (classNode == null) {

                    classNode = new Telerik.Web.UI.RadTreeNode (currentProblemStatement.ProblemClassName, "ProblemClass_" + currentProblemStatement.ProblemDomainId + "_" + currentProblemStatement.ProblemClassId);

                    classNode.Checkable = false;
                    
                    domainNode.Nodes.Add (classNode);

                }


                // EXISTING PROBLEMS STATEMENTS WILL HAVE "Checked" TRUE AND "ForeColor" System.Drawing.Color.Black

                // IF THE PROBLEM STATEMENT EXISTS AND IS ACTIVE IN THE CASE ALREADY, DO NOT ALLOW IT TO BE ADDED AGAIN

                // IF THE PROBLEM STATEMENT IS AN IDENTIFIED PROBLEM STATEMENT, BOLD IT AND RECOMMEND IT

                if (problemStatementsActive.ContainsKey (currentProblemStatement.Id)) {

                    problemStatementNode.Checkable = false;

                    problemStatementNode.ImageUrl = "/Images/Common16/Check.png";
                    
                    problemStatementNode.ForeColor = System.Drawing.Color.Black;

                }

                // ADD EARLY SO THAT EXPAND TO NODE WORKS (PARENT RELATIONSHIP REQUIRED)

                classNode.Nodes.Add (problemStatementNode);

                // DETERMINE IF IT IS AN IDENTIFIED PROBLEM STATEMENT

                Boolean identifiedProblemStatement =

                    (from currentIdentifiedProblemStatement in memberProblemStatementidentifiedAvailable

                     where currentIdentifiedProblemStatement.ProblemStatementId == currentProblemStatement.Id

                     select currentIdentifiedProblemStatement).ToList ().Count > 0;

                if (identifiedProblemStatement) {

                    problemStatementNode.Text += " (recommended)";

                    problemStatementNode.Expanded = true;

                    RadTreeView_ExpandToNode (problemStatementNode);

                    problemStatementNode.Font.Bold = false;

                    problemStatementNode.ForeColor = System.Drawing.Color.Red;

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

        protected void ProblemStatementTreeView_OnNodeClick (Object sender, Telerik.Web.UI.RadTreeNodeEventArgs e) {

            Int64 problemStatementId = 0;

            if (Int64.TryParse (e.Node.Value, out problemStatementId)) {

                Client.Core.Individual.ProblemStatement problemStatement = MercuryApplication.ProblemStatementGet (problemStatementId, true);


                ProblemStatementDomainName.Text = (problemStatement != null) ? problemStatement.ProblemDomainName : String.Empty;

                ProblemStatementClassName.Text = (problemStatement != null) ? problemStatement.ProblemClassName : String.Empty;

                ProblemStatementName.Text = (problemStatement != null) ? problemStatement.Name : String.Empty;

                ProblemStatementDescription.Text = (problemStatement != null) ? problemStatement.Description : String.Empty;


                ProblemStatementDefiningCharacteristics.Text = (problemStatement != null) ? problemStatement.DefiningCharacteristics : String.Empty;

                ProblemStatementRelatedFactors.Text = (problemStatement != null) ? problemStatement.RelatedFactors : String.Empty;



                Client.Core.Individual.CarePlan defaultCarePlan = (problemStatement != null) ? problemStatement.DefaultCarePlan : null;


                DefaultCarePlanName.Text = (defaultCarePlan != null) ? defaultCarePlan.Name : String.Empty;

                DefaultCarePlanGoals.DataSource = (defaultCarePlan != null) ? defaultCarePlan.Goals : null;

                DefaultCarePlanGoals.DataBind ();


                DefaultCarePlanInterventions.DataSource = null;

                if (defaultCarePlan != null) {

                    var careInterventions =

                        (from currentCarePlanGoal in defaultCarePlan.Goals

                         from currentCarePlanIntervention in currentCarePlanGoal.Interventions

                         orderby currentCarePlanGoal.Name, currentCarePlanIntervention.Name

                         select currentCarePlanIntervention.CareIntervention).Distinct ();

                    DefaultCarePlanInterventions.DataSource = careInterventions;

                }

                DefaultCarePlanInterventions.DataBind ();

            }

            return;

        }

        protected void ProblemStatementFilter_OnClick (Object sender, EventArgs e) {

            ProblemStatementTreeView.UnselectAllNodes ();

            foreach (Telerik.Web.UI.RadTreeNode currentNode in ProblemStatementTreeView.GetAllNodes ()) {

                if (currentNode.Text.ToUpper ().Contains (ProblemStatementFilterText.Text.ToUpper ())) {

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

            foreach (Telerik.Web.UI.RadTreeNode currentNode in ProblemStatementTreeView.GetAllNodes ()) {

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

        protected void ProblemStatementFilterClear_OnClick (Object sender, EventArgs e) {

            foreach (Telerik.Web.UI.RadTreeNode currentNode in ProblemStatementTreeView.GetAllNodes ()) {

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

            List<Int64> problemStatementIds = new List<Int64> (); // PROBLEM STATEMENTS TO ADD

            Int64 problemStatementId = 0;

            Mercury.Server.Application.MemberCaseModificationResponse response;



            // IDENTIFY ALL REQUESTED PROBLEM STATEMENTS TO ADD

            foreach (Telerik.Web.UI.RadTreeNode currentNode in ProblemStatementTreeView.GetAllNodes ()) {

                // VALIDATE THAT THE NODE IS CHECKED AND REQUESTED TO BE ADDED TO CASE

                if (currentNode.Checked) {

                    // VALIDATE THAT THE NODE IS A PROBLEM STATEMENT NODE

                    if (Int64.TryParse (currentNode.Value, out problemStatementId)) {

                        problemStatementIds.Add (problemStatementId);

                    }
                    
                }
                
            }


            isModified = (problemStatementIds.Count > 0);

            if (isModified) {

                success = true;

                foreach (Int64 currentProblemStatementId in problemStatementIds) {

                    // TODO: ADD SINGLE INSTANCE SUPPORT HERE

                    response = MercuryApplication.MemberCase_AddProblemStatement (Case, currentProblemStatementId, false);

                    Case = new Client.Core.Individual.Case.MemberCase (MercuryApplication, response.MemberCase);

                    if (response.HasException) { 
                        
                        ExceptionMessage = response.Exception.Message;

                        success = false;

                        // REBUILD TREE AND SET ERROR NODE ACTIVE 

                        ProblemStatementTreeView.Nodes.Clear ();

                        InitializeProblemStatementTreeView ();

                        Telerik.Web.UI.RadTreeNode problemStatementNode = ProblemStatementTreeView.FindNodeByValue (currentProblemStatementId.ToString ());

                        if (problemStatementNode != null) {

                            problemStatementNode.Expanded = true;

                            problemStatementNode.ParentNode.Expanded = true;

                            problemStatementNode.ParentNode.ParentNode.Expanded = true;
                            
                            problemStatementNode.Selected = true;

                        }

                        break;
                    
                    }

                }

                if (success) { // IF ALL PROBLEMS WERE ADDED WITHOUT PROBLEMS, REFRESH TREE

                    ProblemStatementTreeView.Nodes.Clear ();

                    InitializeProblemStatementTreeView ();

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

            if (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ProblemStatementManage)) {

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