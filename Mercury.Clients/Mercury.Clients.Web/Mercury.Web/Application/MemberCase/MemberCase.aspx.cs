using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.MemberCase {

    public partial class MemberCase : System.Web.UI.Page {

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

                    InitializeAll ();


                    // PROPAGATE TO ALL CHILD CONTROLS 

                    MemberCaseCarePlanControl.Case = value;

                    MemberCaseAuditHistoryControl.Case = value;
                    
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

                
                MemberDemographicsControl.InstanceId = SessionCachePrefix + "MemberDemographicsControl";

                MemberDemographicsControl.AllowUserInteraction = true;

                MemberDemographicsControl.InitializeMemberDemographics (Case.Member.Id);


                MemberCaseViewControl.InstanceId = SessionCachePrefix + "MemberCaseViewControl";

                MemberCaseViewControl.InitializeMember (Case.Member);


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

            InitializeCaseOverview ();


            // FIRST TAB PROPERTIES

            InitializeCaseProperties ();

            InitializeCaseLocking ();

            InitializeCaseStatus ();

            InitializeCaseCareLevel ();

            InitializeWorkTeamAssignment ();

            InitializeUserAssignment ();


            // REBIND DATA GRIDS

            ProblemStatementsGrid.Rebind ();

            MemberCaseInterventionsGrid.Rebind ();

            return;

        }

        private void InitializeMember () {

            Client.Core.Member.Member member = Case.Member;

            if (member == null) { return; }


            EntityInformationMemberControl.Member = member;

            ApplicationTitle.InnerText = member.Name + " (" + member.CurrentAge + " | " + member.GenderDescription + ((member.MostRecentEnrollment != null) ? " | " + member.MostRecentEnrollment.ProgramMemberId : String.Empty) + ")";

            ApplicationTitle.HRef = @"/Application/Member/MemberProfile.aspx?MemberId=" + member.Id.ToString ();

            return;

        }

        private void InitializeCaseOverview () {

            // INITIALIZE CASE OVERVIEW INFORMATION

            CaseId.Text = Case.Id.ToString ();

            CaseReferenceNumberLabel.Text = Case.ReferenceNumber;

            CaseReferenceNumber.Text = Case.ReferenceNumber;

            CaseLockedBy.Text = (Case.LockedByDate.HasValue) ? Case.LockedByUserDisplayName : "Not Locked"; 


            CaseStatus.Text = Mercury.Server.CommonFunctions.EnumerationToString (Case.Status);

            CaseEffectiveDate.Text = Case.EffectiveDateDescription;

            CaseTerminationDate.Text = Case.TerminationDateDescription;


            CaseDescriptionLess.Text = (Case.Description.Length > 120) ? Case.Description.Substring (0, 120) + "..." : Case.Description;

            String caseDescriptionMoreText = Case.Description.Replace ("\r\n", "<br />"); // CATCH THE FULL RETURN/NEW LINE

            caseDescriptionMoreText = caseDescriptionMoreText.Replace ("\r", "<br />"); // STAND-ALONE RETURN

            caseDescriptionMoreText = caseDescriptionMoreText.Replace ("\n", "<br />"); // STAND-ALONE NEW LINE

            CaseDescriptionMore.Text = caseDescriptionMoreText;

            CaseDescription.Text = Case.Description;


            // INITIALIZE NOTIFICATIONS

            InitializeCaseNotifications ();

            return;

        }

        private void InitializeCaseNotifications () {

            List<String> notificationMessages = new List<String> ();

            if (MercuryApplication.MemberProblemStatementIdentifiedAvailable (Case.MemberId, false, false).Count > 0) {

                notificationMessages.Add ("Member has Identified Problem Statements availble.");

            }

            NotificationSection.Visible = (notificationMessages.Count > 0);

            CaseNotifications.DataSource = notificationMessages;

            CaseNotifications.DataBind ();

            return;

        }


        private void InitializeCaseProperties () {

            Boolean isLocked = ((Case.LockedBySecurityAuthorityId != 0) && (!Case.LockedByThisSession));

            Boolean isAssignedTo = Case.AssignedToThisSession;

            Boolean isAssignedToTeam = Case.AssignedToThisSessionTeam;

            Boolean isReadOnly = ((isLocked) || ((Case.HasAssignment) && (!isAssignedTo))) || (Case.IsReadOnly);


            // READ ONLY STATUS IS DETERMINE IN GENERAL FOR THE CASE HEADER INFORMATION BY 

            // THE CASE BEING LOCKED BY ANOTHER USER BESIDES THE CURRENT USER 

            // OR THE CASE IS ASSIGNED TO ANOTHER USER BESIDES THE CURRENT USER

            // OR THE CASE STATUS IS NOT ACTIVE OR UNDER DEVELOPMENT


            
            CaseReferenceNumberEditLink.Style.Add ("display", ((isReadOnly) ? "none" : "inline")); 


            return;

        }

        private void InitializeCaseLocking () {

            Boolean isLockVisible = false;

            if (Case.LockedByThisSession) { isLockVisible = true; } // ALWAYS AVAILABLE WHEN LOCKED BY CURRENT SESSION 

            else if (Case.LockedByDate.HasValue) { // LOCKED, AND USER IS NOT THE OWNER OF THE LOCK, MUST BE MANAGER 

                if (Case.AssignedToThisSessionTeamManager) { isLockVisible = true; } // MANAGER CAN UNLOCK

            }

            else { // NOT LOCKED, MUST BE ASSIGNED TO OR MANAGER OF TEAM TO LOCK

                if (((!Case.HasWorkTeamAssignment) && (!Case.HasAssignment)) || (Case.AssignedToThisSession)) { isLockVisible = true; } // CASE IS NOT ASSIGNED TO USER OR TEAM OR ASSIGNED TO THIS SESSION 

                else if ((!Case.HasAssignment) && (Case.AssignedToThisSessionTeam)) { isLockVisible = true; } // CASE IS NOT ASSIGNED, USER IS PART OF THE TEAM 

            }

            CaseLockToggleLink.Visible = isLockVisible && (!Case.IsReadOnly);

            CaseLockToggleLink.Text = (Case.LockedByDate.HasValue) ? "(unlock)" : "(lock)";

            return;

        }

        
        private void InitializeCaseStatus () {

            return;

        }

        private void InitializeCaseCareLevel () {

            Boolean isLocked = ((Case.LockedBySecurityAuthorityId != 0) && (!Case.LockedByThisSession));

            Boolean isAssignedTo = Case.AssignedToThisSession;

            Boolean isAssignedToTeam = Case.AssignedToThisSessionTeam;

            Boolean isReadOnly = ((isLocked) || ((Case.HasAssignment) && (!isAssignedTo))) || (Case.IsReadOnly);


            // CHANGING CARE LEVEL IS ONLY AVAILABLE IN THESE INSTANCES

            // 1. CASE STATUS = 'ACTIVE'

            // 2. MUST BE USER DIRECTLY ASSIGNED TO CASE, MANAGERS WILL TO TAKE OWNERSHIP

            Boolean isCareLevelVisible = false;

            if (Case.Status == Mercury.Server.Application.CaseItemStatus.Active) {

                isCareLevelVisible = ((Case.AssignedToThisSession) && (!isReadOnly));

            }


            #region Map Care Levels into Control

            // CLEAR ITEMS OF CASE CARE LEVEL SELECTION RAD COMBO BOX

            CaseCareLevelSelection.Items.Clear ();

            // SET CARE LEVELS AVAILABLE AS CARE LEVELS AVAILABLE

            List<Client.Core.Individual.CareLevel> careLevelsAvailable = MercuryApplication.CareLevelsAvailable (true);

            // LOOP THROUGH EACH CARE LEVEL IN CARE LEVELS AVAILABLE

            foreach (Client.Core.Individual.CareLevel currentCareLevel in careLevelsAvailable) {

                // IF CURRENT CARE LEVEL IS ENABLED AND VISIBLE, THEN ADD NEW RAD COMBO BOX ITEM WITH TO ITEMS OF CASE CARE LEVEL SELECTION RAD COMBO BOX

                if ((currentCareLevel.Enabled) && (currentCareLevel.Visible)) {

                    // CREATE REFERENCE TO NEW RAD COMBO BOX ITEM WITH TEXT AS NAME OF CURRENT CARE LEVEL AND VALUE AS ID OF CURRENT CARE LEVEL

                    Telerik.Web.UI.RadComboBoxItem newRadComboBoxItem = new Telerik.Web.UI.RadComboBoxItem (currentCareLevel.Name, currentCareLevel.Id.ToString ());

                    // ADD NEW RAD COMBO BOX ITEM TO ITEMS OF CASE CARE LEVEL SELECTION RAD COMBO BOX

                    CaseCareLevelSelection.Items.Add (newRadComboBoxItem);

                }

            } /* END FOREACH */

            /* TODO: DAVID: SET THE CURRENT MEMBER CASE CARE LEVEL AS SELECTED */

            #endregion


            //CaseLevelChangeLink.Visible = isCareLevelVisible;

            CaseLevelChangeLink.Style.Add ("display", ((isCareLevelVisible) ? "inline" : "none"));

            return;

        }


        private void InitializeWorkTeamAssignment () {


            // IF WE REACH THIS POINT, THE RECORD IS UNLOCKED OR LOCKED BY REQUESTING USER, 

            // TO ASSIGN, ONE OF THE FOLLOWING MUST BE TRUE

            // 1. CASE IS NOT CURRENTLY ASSIGNED TO CASE AND REQUESTING USER IS MEMBER OF DESTINATION TEAM (ANY ROLE)

            // 2. CASE IS CURRENTLY ASSIGNED, REQUEST IS TO REMOVE ASSIGNMENT FROM TEAM, USER IS MANAGER OF TEAM

            // 3. CASE IS CURRENTLY ASSIGNED, REQUEST IS TO MOVE TO ANOTHER TEAM, USER IS MANAGER OF BOTH TEAMS

            // TODO: CHECK FOR CARE PLAN ASSIGNMENTS, AS THIS WILL REPLACE THEM


            CaseAssignedToWorkTeamLabel.Text = ((Case.HasWorkTeamAssignment) ? Case.AssignedToWorkTeamName : String.Empty);


            Boolean isAssignedToWorkTeamChangeVisible = false;

            // IF ASSIGNED TO WORK TEAM EMPTY, THE USER MUST BE A MANAGER OF A CARE TEAM

            if (!Case.HasWorkTeamAssignment) {

                // CYCLE THROUGH ALL CARE TEAMS AND FIND ONE THAT THE USER IS A MEMBER OF

                foreach (Client.Core.Work.WorkTeam currentWorkTeam in MercuryApplication.WorkTeamsForSession (true)) {

                    if (currentWorkTeam.WorkTeamType == Mercury.Server.Application.WorkTeamType.CareTeam) {

                        Mercury.Server.Application.WorkTeamMembership membership = currentWorkTeam.MembershipGetForSession ();

                        if (membership != null) {

                            isAssignedToWorkTeamChangeVisible = true;

                        }

                    }

                }

            }

            else {

                // IF A WORK TEAM IS ASSIGNED, THE USER MUST BE A MANAGER OF THE WORK TEAM TO CHANGE IT

                isAssignedToWorkTeamChangeVisible = Case.AssignedToThisSessionTeamManager;

            }

            CaseAssignedToWorkTeamChangeLink.Style.Add ("display", ((isAssignedToWorkTeamChangeVisible) ? "inline" : "none"));


            // ADD AVAILABLE CARE TEAMS TO SELECTION AVAILABILITY

            if (isAssignedToWorkTeamChangeVisible) {

                CaseAssignedToWorkTeamSelection.Items.Clear ();

                CaseAssignedToWorkTeamSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** Not Assigned", "0"));

                foreach (Client.Core.Work.WorkTeam currentWorkTeam in MercuryApplication.WorkTeamsForSession (true)) {

                    if (currentWorkTeam.WorkTeamType == Mercury.Server.Application.WorkTeamType.CareTeam) {

                        Mercury.Server.Application.WorkTeamMembership membership = currentWorkTeam.MembershipGetForSession ();

                        if (membership != null) {

                            if ((membership.WorkTeamRole == Mercury.Server.Application.WorkTeamRole.Manager) || (!Case.HasWorkTeamAssignment)) {

                                CaseAssignedToWorkTeamSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentWorkTeam.Name, currentWorkTeam.Id.ToString ()));

                            }

                        }

                    }

                }

                CaseAssignedToWorkTeamSelection.SelectedValue = Case.AssignedToWorkTeamId.ToString ();

            }



            return;

        }

        private void InitializeUserAssignment () {
            
            Boolean isAssignedToUserChangeVisible = false;

            CaseAssignedToUserLabel.Text = ((Case.AssignedToDate.HasValue) ? Case.AssignedToUserDisplayName : "** Not Assigned");

            // IF ASSIGNED TO THE CURRENT USER, THE USER CAN UNASSIGN THEMSELVES

            if (Case.AssignedToThisSession) {

                isAssignedToUserChangeVisible = true;

            }

            else {

                // IF CASE IS ASSIGNED TO A WORK TEAM, AND MEMBER IS PART OF THAT WORK TEAM, THEY CAN SELF ASSIGN (OR A MANAGER MAY ASSIGN TO ANY USER)

                if (Case.HasWorkTeamAssignment) {

                    if (Case.AssignedToThisSessionTeamManager) { // IF A MANAGER

                        isAssignedToUserChangeVisible = true;

                    }

                    else if (Case.AssignedToThisSessionTeam) { // IF ASSIGNED TO TEAM THAT USER IS A MEMBER 

                        isAssignedToUserChangeVisible = true;

                    }

                }

            }

            CaseAssignedToUserChangeLink.Style.Add ("display", ((isAssignedToUserChangeVisible) ? "inline" : "none")); 


            // ADD AVAILABLE CARE TEAM MEMBERS TO SELECTION 

            if (isAssignedToUserChangeVisible) {

                CaseAssignedToUserSelection.Items.Clear ();

                CaseAssignedToUserSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** Not Assigned", "0|"));

                if (Case.AssignedToWorkTeam != null) {

                    foreach (Mercury.Server.Application.WorkTeamMembership currentMembership in Case.AssignedToWorkTeam.Membership) {

                        Boolean canAddMembership = Case.AssignedToThisSessionTeamManager; // IF USER IS TEAM MANAGER, CAN ASSIGN TO ANY USER

                        // IF THE CASE IS NOT ASSIGNED TO ANYONE, AND USER IS MEMBER OF TEAM, CAN "SELF-ASSIGN";                       

                        canAddMembership |= ((!Case.AssignedToDate.HasValue) && ((currentMembership.SecurityAuthorityId == MercuryApplication.Session.SecurityAuthorityId) && (currentMembership.UserAccountId == MercuryApplication.Session.UserAccountId)));

                        if (canAddMembership) { 

                            CaseAssignedToUserSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentMembership.UserDisplayName, currentMembership.SecurityAuthorityId.ToString () + "|" + currentMembership.UserAccountId));

                        }
                        
                    }

                    CaseAssignedToUserSelection.SelectedValue = Case.AssignedToSecurityAuthorityId.ToString () + "|" + Case.AssignedToUserAccountId;

                }

            }

            return;

        }

        #endregion 


        #region Property Changes

        public void CaseReferenceNumberSaveLink_OnClick (Object sender, EventArgs e) {

            Server.Application.MemberCaseModificationResponse response;


            response = MercuryApplication.MemberCase_SetReferenceNumber (Case, CaseReferenceNumber.Text);

            if (response.HasException) { ExceptionMessage = response.Exception.Message; }

            // UPDATE FORM FROM UPDATED CASE RECEVIED THROUGH RESPONSE

            if (response.MemberCase != null) {

                Case = new Client.Core.Individual.Case.MemberCase (MercuryApplication, response.MemberCase);

            }

            return;

        }

        public void CaseLockToggleLink_OnClick (Object sender, EventArgs e) {

            Server.Application.MemberCaseModificationResponse response = null;

            if (((LinkButton)sender).Text == "(lock)") {

                response = MercuryApplication.MemberCase_Lock (Case);

            }

            else {

                response = MercuryApplication.MemberCase_Unlock (Case);

            }

            if (response == null) { return; }

            if (response.HasException) { ExceptionMessage = response.Exception.Message; }


            // UPDATE FORM FROM UPDATED CASE RECEVIED THROUGH RESPONSE

            if (response.MemberCase != null) {

                Case = new Client.Core.Individual.Case.MemberCase (MercuryApplication, response.MemberCase);

            }

            return;

        }

        public void CaseAssignedToWorkTeamSaveLink_OnClick (Object sender, EventArgs e) {

            Server.Application.MemberCaseModificationResponse response;


            Int64 selectedWorkTeamId = Convert.ToInt64 (CaseAssignedToWorkTeamSelection.SelectedValue);

            if (Case.AssignedToWorkTeamId != selectedWorkTeamId) { 

                response = MercuryApplication.MemberCase_AssignToWorkTeam (Case, selectedWorkTeamId);

                if (response.HasException) { ExceptionMessage = response.Exception.Message; }

                // UPDATE FORM FROM UPDATED CASE RECEVIED THROUGH RESPONSE

                if (response.MemberCase != null) {

                    Case = new Client.Core.Individual.Case.MemberCase (MercuryApplication, response.MemberCase);

                }

            }

            return;

        }

        public void CaseAssignedToUserSaveLink_OnClick (Object sender, EventArgs e) {

            Server.Application.MemberCaseModificationResponse response = null;


            Int64 securityAuthorityId = Convert.ToInt64 (CaseAssignedToUserSelection.SelectedValue.Split ('|')[0]);

            String userAccountId = CaseAssignedToUserSelection.SelectedValue.Split ('|')[1];


            // DETERMINE IF THERE WAS AN ACTUAL CHANGE

            if (!((Case.AssignedToSecurityAuthorityId == securityAuthorityId) && (Case.AssignedToUserAccountId == userAccountId))) {

                if (securityAuthorityId != 0) {

                    // FIND WORK TEAM MEMBERSHIP RECORD THAT IS SELETED

                    Mercury.Server.Application.WorkTeamMembership membership = null;

                    foreach (Mercury.Server.Application.WorkTeamMembership currentMembership in Case.AssignedToWorkTeam.Membership) {

                        if ((currentMembership.SecurityAuthorityId == securityAuthorityId) && (currentMembership.UserAccountId == userAccountId)) {

                            membership = currentMembership;

                        }

                    }

                    if (membership != null) {

                        response = MercuryApplication.MemberCase_AssignToUser (Case,

                            membership.SecurityAuthorityId,

                            membership.UserAccountId,

                            membership.UserAccountName,

                            membership.UserDisplayName

                            );

                    }

                }

                else { // REQUEST TO UNASSIGN FROM CURRENT USER

                    response = MercuryApplication.MemberCase_AssignToUser (Case, 0, String.Empty, String.Empty, String.Empty);

                }

            }

            if (response != null) {

                if (response.HasException) { ExceptionMessage = response.Exception.Message; }

                // UPDATE FORM FROM UPDATED CASE RECEVIED THROUGH RESPONSE

                if (response.MemberCase != null) {

                    Case = new Client.Core.Individual.Case.MemberCase (MercuryApplication, response.MemberCase);

                }

            }

            return;

        }

        public void CaseDescriptionSaveLink_OnClick (Object sender, EventArgs e) {

            Server.Application.MemberCaseModificationResponse response;

            String modifiedCaseDescription = CaseDescription.Text;


            response = MercuryApplication.MemberCase_SetDescription (Case, CaseDescription.Text);

            // UPDATE FORM FROM UPDATED CASE RECEVIED THROUGH RESPONSE

            if (response.MemberCase != null) {

                Case = new Client.Core.Individual.Case.MemberCase (MercuryApplication, response.MemberCase);

            }

            if (response.HasException) { 
                
                ExceptionMessage = response.Exception.Message;

                CaseDescription.Text = modifiedCaseDescription;

            }

            return;

        }

        public void CaseCareLevelSaveLink_OnClick (Object sender, EventArgs e) {

            return;

        }

        #endregion 


        #region Problem Statement Grid Events

        protected void ProblemStatementsGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e) {

            List<Client.Core.Individual.Case.MemberCaseProblemClass> problemClasses =

                (from currentProblemClass in Case.ProblemClasses

                 where currentProblemClass.HasActiveCarePlans

                 orderby currentProblemClass.Classification

                 select currentProblemClass).ToList ();


            if (!e.IsFromDetailTable) {

                switch (e.RebindReason) {

                    case Telerik.Web.UI.GridRebindReason.InitialLoad:

                    case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                    case Telerik.Web.UI.GridRebindReason.PostBackEvent:

                        ProblemStatementsGrid.DataSource = problemClasses;

                        var problemCarePlans1 =

                            (from currentMemberCaseProblemClass in problemClasses

                             from currentProblemCarePlan in currentMemberCaseProblemClass.ProblemCarePlans

                             orderby currentProblemCarePlan.ProblemStatementClassificationWithName

                             select currentProblemCarePlan).ToList ();

                        break;

                    case Telerik.Web.UI.GridRebindReason.DetailTableBinding:

                        break;

                }

            }

            else { // DETAIL TABLE BINDING

                var problemCarePlans =

                    (from currentMemberCaseProblemClass in problemClasses

                     from currentProblemCarePlan in currentMemberCaseProblemClass.ProblemCarePlans

                     orderby currentProblemCarePlan.ProblemStatementClassificationWithName

                     select currentProblemCarePlan).ToList ();

                ProblemStatementsGrid.MasterTableView.DetailTables[0].DataSource = problemCarePlans;

            }

            return;

        }

        protected void ProblemStatementsGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs e) {

            Telerik.Web.UI.RadGrid grid = (Telerik.Web.UI.RadGrid)sender;

            Telerik.Web.UI.GridDataItem item = (Telerik.Web.UI.GridDataItem)e.Item;

            Mercury.Server.Application.MemberCaseModificationResponse response;

            Int64 memberCaseProblemCarePlanId = 0;


            switch (e.CommandName) {

                case "DeleteProblemStatement":

                    memberCaseProblemCarePlanId = Convert.ToInt64 (item.GetDataKeyValue ("Id"));

                    response = MercuryApplication.MemberCase_DeleteProblemStatement (Case, memberCaseProblemCarePlanId);

                    Case = new Client.Core.Individual.Case.MemberCase (MercuryApplication, response.MemberCase);

                    if (response.HasException) { ExceptionMessage = response.Exception.Message; }

                    break;

            }

            return;

        }

        #endregion 
        

        #region Interventions Grid Events

        protected void MemberCaseInterventionsGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e) {

            List<Client.Core.Individual.Case.MemberCaseCareIntervention> interventions =

                (from currentIntervention in Case.CareInterventions

                 where ((currentIntervention.Status == Mercury.Server.Application.CaseItemStatus.Active)

                    || (currentIntervention.Status == Mercury.Server.Application.CaseItemStatus.UnderDevelopment))

                 orderby currentIntervention.Name

                 select currentIntervention).ToList ();


            if (!e.IsFromDetailTable) {

                switch (e.RebindReason) {

                    case Telerik.Web.UI.GridRebindReason.InitialLoad:

                    case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                    case Telerik.Web.UI.GridRebindReason.PostBackEvent:


                        MemberCaseInterventionsGrid.DataSource = interventions;

                        break;

                }

            }

            else { // DETAIL TABLE BINDING

                List<Client.Core.Individual.Case.MemberCaseCarePlanGoalIntervention> goalInterventions =

                    (from currentIntervention in interventions

                     from currentGoalIntervention in currentIntervention.GoalInterventions

                     where ((currentGoalIntervention.MemberCaseCarePlanGoal.Status == Mercury.Server.Application.CaseItemStatus.UnderDevelopment)

                        || (currentGoalIntervention.MemberCaseCarePlanGoal.Status == Mercury.Server.Application.CaseItemStatus.Active)

                        // ADDED NOT SPECIFIED

                        || (currentGoalIntervention.MemberCaseCarePlanGoal.Status == Mercury.Server.Application.CaseItemStatus.NotSpecified))

                     orderby currentGoalIntervention.MemberCaseCarePlanGoal.Name

                     select currentGoalIntervention).Distinct ().ToList ();

                MemberCaseInterventionsGrid.MasterTableView.DetailTables[0].DataSource = goalInterventions;
                 
            }
             
            return;
            
        }

        #endregion 

    }

}