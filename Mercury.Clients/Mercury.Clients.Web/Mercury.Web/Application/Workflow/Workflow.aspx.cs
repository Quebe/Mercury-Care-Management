using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Workflow {

    public partial class Workflow : System.Web.UI.Page {

        #region Private Properties

        private Boolean isPageUnloading = false;

        private const String ResponseScriptWorkflowStart = "<script language=\"JavaScript\">setTimeout (\"document.getElementById ('WorkflowStart').click ();\", 125); </script>";

        private const String ResponseScriptWorkflowContinue = "setTimeout (\"document.getElementById ('WorkflowContinue').click ();\", 10);";

        private const String ResponseScriptWorkflowResume = "setTimeout (\"document.getElementById ('WorkflowResume').click ();\", 10);";

        private const String ResponseScriptWorkflowPaint = "setTimeout (\"Workflow_OnPaint ();\", 100);";

        #endregion


        #region Public State Properties

        public String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (PageInstanceId.Text)) { PageInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return "Mercury" + PageInstanceId.Text;

            }

        }

        public Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application)Session["Mercury.Application"];

                if ((application == null) && (!isPageUnloading)) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }


        public Mercury.Client.Core.Work.Workflow CurrentWorkflow {

            get {

                Mercury.Client.Core.Work.Workflow currentWorkflow = (Mercury.Client.Core.Work.Workflow)Session[SessionCachePrefix + "Workflow"];

                return currentWorkflow;

            }

            set {

                Session[SessionCachePrefix + "Workflow"] = value;

            }

        }

        public Guid WorkflowInstanceId {

            get {

                Guid workflowInstanceId = (Guid)Session[SessionCachePrefix + "WorkflowInstanceId"];

                return workflowInstanceId;

            }

            set {

                Session[SessionCachePrefix + "WorkflowInstanceId"] = value;

            }

        }


        public Server.Application.WorkflowStartRequest WorkflowStartRequest {

            get {

                Server.Application.WorkflowStartRequest workflowStartRequest = (Server.Application.WorkflowStartRequest)Session[SessionCachePrefix + "WorkflowStartRequest"];

                if (workflowStartRequest == null) {

                    workflowStartRequest = new Mercury.Server.Application.WorkflowStartRequest ();

                    workflowStartRequest.Arguments = new System.Collections.Generic.Dictionary<String, Object> ();

                    workflowStartRequest.WorkflowId = 0;

                    workflowStartRequest.WorkflowInstanceId = Guid.Empty;

                    workflowStartRequest.WorkflowName = String.Empty;

                }

                return workflowStartRequest;

            }

            set { Session[SessionCachePrefix + "WorkflowStartRequest"] = value; }

        }

        public Mercury.Server.Application.WorkflowResponse WorkflowResponse {

            get {

                Mercury.Server.Application.WorkflowResponse workflowResponse = (Mercury.Server.Application.WorkflowResponse)Session[SessionCachePrefix + "WorkflowResponse"];

                return workflowResponse;

            }

            set {

                Session[SessionCachePrefix + "WorkflowResponse"] = value;

            }

        }

        public Mercury.Server.Application.WorkflowUserInteractionRequestBase UserInteractionRequest {

            get { return (WorkflowResponse == null) ? null : WorkflowResponse.UserInteractionRequest; }

        }

        public Mercury.Server.Application.WorkflowUserInteractionResponseBase UserInteractionResponse {

            get {

                Mercury.Server.Application.WorkflowUserInteractionResponseBase userInteractionResponse = (Mercury.Server.Application.WorkflowUserInteractionResponseBase)Session[SessionCachePrefix + "WorkflowUserInteractionResponse"];

                return userInteractionResponse;

            }

            set {

                Session[SessionCachePrefix + "WorkflowUserInteractionResponse"] = value;

            }

        }



        public String ContentControlName {

            get {

                String contentControlName = (String)Session[SessionCachePrefix + "ContentControlName"];

                if (String.IsNullOrEmpty (contentControlName)) { contentControlName = String.Empty; }

                return contentControlName;

            }

            set {

                Session[SessionCachePrefix + "ContentControlName"] = value;

            }

        }

        public String ContentControlId {

            get {

                String contentControlId = (String)Session[SessionCachePrefix + "ContentControlId"];

                if (String.IsNullOrEmpty (contentControlId)) { contentControlId = String.Empty; }

                return contentControlId;

            }

            set {

                Session[SessionCachePrefix + "ContentControlId"] = value;

            }

        }


        public String ReferrerUrl {

            get {

                String referringUrl = String.Empty;

                if (Session[SessionCachePrefix + "ReferrerUrl"] != null) { // RETURN URL IF CACHED FOR THIS PAGE

                    referringUrl = (String)Session[SessionCachePrefix + "ReferrerUrl"];

                }

                else if (!String.IsNullOrWhiteSpace (Request.QueryString["ReferrerUrlSessionCachePrefix"])) { // TRY TO BRING IN PREVIOUS REFERRER AFTER NEXT CLICK

                    referringUrl = Request.QueryString["ReferrerUrlSessionCachePrefix"] + "ReferrerUrl"; // CREATE CACHE KEY REFERENCE

                    referringUrl = (String)Session[referringUrl]; // GET CACHED REFERRER URL FROM PREVIOUS PAGE CACHE 

                    ReferrerUrl = referringUrl; // SET THROUGH PUBLIC PROPERTY SO THAT ALL LINKS GET UPDATED PROPERLY

                }

                return referringUrl;

            }

            set {

                // SAVE REFERRER URL INTO SESSION STATE

                Session[SessionCachePrefix + "ReferrerUrl"] = value;

                                
                // UPDATE THE CLIENT-SIDE REFERENCE FOR THE URL (USED BY THE CANCEL LINK CONFIRMATION DIALOG)

                WorkflowReferrerUrl.Text = value;


                // UPDATE THE CLOSE LINK HREF

                WorkflowCloseLink.Attributes.Remove ("href");

                WorkflowCloseLink.Attributes.Add ("href", value);


            }

        }

        public Telerik.Web.UI.RadAjaxManager WorkflowAjaxManager { get { return TelerikAjaxManager; } }

        #endregion 


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }


            if (!IsPostBack) { InitialPageLoadInitialization (); }

            if (!String.IsNullOrWhiteSpace (ContentControlName)) { ReloadUserControl (); }


            return;

        }

        protected void Page_Unload (object sender, EventArgs e) {

            isPageUnloading = true;

            if (MercuryApplication != null) { MercuryApplication.ApplicationClientClose (); }

            return;

        }

        #endregion


        #region Initializations

        private void InitialPageLoadInitialization () {

            // SAVE REFERRER SO THAT THE APPLICATION CAN RETURN TO IT AFTER COMPLETING THE WORKFLOW

            if (String.IsNullOrWhiteSpace (ReferrerUrl)) { // ONLY RELOAD IF CURRENT SAVE SESSION IS EMPTY REFERRER

                if (Request.UrlReferrer != null) { ReferrerUrl = Request.UrlReferrer.ToString (); }

                else { ReferrerUrl = "/Application/Workspace/Workspace.aspx"; }

            }


            // INITIALIZE WORKFLOW

            String workflowName = (String)Request.QueryString["Workflow"];

            Int64 currentWorkflowId = 0;


            if (!String.IsNullOrWhiteSpace (workflowName)) { CurrentWorkflow = MercuryApplication.WorkflowGet (workflowName, false); }

            else {

                if (Int64.TryParse ((String)Request.QueryString["WorkflowId"], out currentWorkflowId)) {

                    CurrentWorkflow = MercuryApplication.WorkflowGet (currentWorkflowId, true);

                    if (CurrentWorkflow != null) { workflowName = CurrentWorkflow.Name; }

                }

            }


            if (!String.IsNullOrEmpty ((String)Request.QueryString["WorkflowInstanceId"])) {

                // RESUME EXISTING WORKFLOW

                WorkflowInstanceId = new Guid ((String)Request.QueryString["WorkflowInstanceId"]);

                CurrentWorkflow = MercuryApplication.WorkflowGet (workflowName, false);

                if (CurrentWorkflow == null) { CurrentWorkflow = MercuryApplication.WorkflowGet (currentWorkflowId, true); }

                if (CurrentWorkflow != null) {

                    WorkflowTitleLabel.Text = "Workflow: " + CurrentWorkflow.Name + ((!String.IsNullOrEmpty (CurrentWorkflow.Description)) ? " [" + CurrentWorkflow.Description + "]" : String.Empty);

                    TelerikAjaxManager.ResponseScripts.Add (ResponseScriptWorkflowResume);

                }

                else {

                    WorkflowTitleLabel.Text = "Workflow Engine";

                    WorkflowExceptionMessageRow.Style.Clear ();

                    WorkflowExceptionMessage.Text = "Workflow Exception: Unable to Load Workflow.";

                }

            }

            else {

                Boolean canWork = true;

                Int64 workQueueItemId;

                if (Int64.TryParse ((String)Request.QueryString["WorkQueueItemId"], out workQueueItemId)) {

                    canWork = false;

                    Client.Core.Work.WorkQueueItem workQueueItem = MercuryApplication.WorkQueueItemGet (workQueueItemId);

                    if (workQueueItem != null) {

                        if ((workQueueItem.AssignedToSecurityAuthorityId == MercuryApplication.Session.SecurityAuthorityId)

                            && (workQueueItem.AssignedToUserAccountId == MercuryApplication.Session.UserAccountId)) { canWork = true; }

                        else if (workQueueItem.AssignTo (MercuryApplication.Session.SecurityAuthorityId, MercuryApplication.Session.UserAccountId, MercuryApplication.Session.UserAccountName, MercuryApplication.Session.UserDisplayName, "Member Profile - Work History")) {

                            canWork = true;

                        }

                    }

                    if (canWork) {

                        Server.Application.WorkflowStartRequest startRequest = WorkflowStartRequest;

                        startRequest.WorkQueueItemId = workQueueItemId;

                        WorkflowStartRequest = startRequest;

                    }

                }

                if ((canWork) && (CurrentWorkflow != null)) {

                    WorkflowTitleLabel.Text = "Workflow: " + CurrentWorkflow.Name + ((!String.IsNullOrEmpty (CurrentWorkflow.Description)) ? " [" + CurrentWorkflow.Description + "]" : String.Empty);

                    Page.ClientScript.RegisterStartupScript (this.GetType (), "StartWorkflow", ResponseScriptWorkflowStart);

                } 

                else if (!canWork) {

                    WorkflowTitleLabel.Text = "Workflow Engine";

                    WorkflowExceptionMessageRow.Style.Clear ();

                    WorkflowExceptionMessage.Text = "Permission Denied: Unable to perform work on the selected Work Queue Item. This Item might already be owned. Please try again.";

                }

                else {

                    WorkflowTitleLabel.Text = "Workflow Engine";

                    WorkflowExceptionMessageRow.Style.Clear ();

                    WorkflowExceptionMessage.Text = "Workflow Exception: " + MercuryApplication.LastException;

                }

            }

            return;

        }

        private void InitializeEntityInformation (Client.Core.Entity.Entity entity) {

            switch (entity.EntityType) {

                case Mercury.Server.Application.EntityType.Member:

                    WorkQueueItemInformationMember.Style.Clear ();

                    InitializeMemberInformationByEntityId (entity.Id);

                    break;

                case Mercury.Server.Application.EntityType.Provider:

                    WorkQueueItemInformationProvider.Style.Clear ();

                    Client.Core.Provider.Provider provider = MercuryApplication.ProviderGetByEntityId (entity.Id, true);

                    if (provider != null) { InitializeProviderInformation (provider.Id); }

                    break;

            }

            return;

        }

        private void InitializeEntityInformation (Int64 entityId) {

            Client.Core.Entity.Entity entity = MercuryApplication.EntityGet (entityId, true);

            if (entity == null) { return; }

            InitializeEntityInformation (entity);

            return;

        }


        private void InitializeEntityInformationFromWorkQueueItem (Int64 workQueueItemId) {

            Client.Core.Work.WorkQueueItem workQueueItem = MercuryApplication.WorkQueueItemGet (WorkflowResponse.WorkQueueItemId);

            if (workQueueItem == null) { return; }


            switch (workQueueItem.ItemObjectType) {

                case "Member": InitializeMemberInformation (workQueueItem.ItemObjectId); break;

            }


            return;

        }


        private void InitializeMemberInformation (Client.Core.Member.Member member) {
            
            WorkQueueItemInformationMember.Style.Clear ();


            #region Note Alert Icons

            Dictionary<Mercury.Server.Application.NoteImportance, Client.Core.Entity.EntityNote> entityNotes;

            entityNotes = MercuryApplication.EntityNoteGetMostRecentByAllImportances (member.EntityId, true);


            Client.Core.Entity.EntityNote entityNote = null;

            // entityNote = MercuryApplication.EntityNoteGetMostRecentByImportance (member.EntityId, Mercury.Server.Application.NoteImportance.Warning, false);

            if (entityNotes.ContainsKey (Mercury.Server.Application.NoteImportance.Warning)) { entityNote = entityNotes[Mercury.Server.Application.NoteImportance.Warning]; }

            if (entityNote != null) {

                if (entityNote.TerminationDate >= DateTime.Today) {

                    UserInteractionEntityInformationMemberNoteWarning.Style.Clear ();

                    UserInteractionEntityInformationMemberNoteWarning.Attributes.Add ("title", "[" + entityNote.NoteTypeName + "] " + entityNote.Subject);

                    UserInteractionEntityInformationMemberNoteWarning.Visible = true;

                }

            }

            // entityNote = MercuryApplication.EntityNoteGetMostRecentByImportance (member.EntityId, Mercury.Server.Application.NoteImportance.Critical, false);

            entityNote = null;

            if (entityNotes.ContainsKey (Mercury.Server.Application.NoteImportance.Critical)) { entityNote = entityNotes[Mercury.Server.Application.NoteImportance.Critical]; }

            if (entityNote != null) {

                if (entityNote.TerminationDate >= DateTime.Today) {

                    UserInteractionEntityInformationMemberNoteCritical.Style.Clear ();

                    UserInteractionEntityInformationMemberNoteCritical.Attributes.Add ("title", "[" + entityNote.NoteTypeName + "] " + entityNote.Subject);

                    UserInteractionEntityInformationMemberNoteCritical.Visible = true;

                }

            }

            #endregion


            UserInteractionEntityInformationMemberName.Text = Web.CommonFunctions.MemberProfileAnchor (member.Id, member.Name);

            UserInteractionEntityInformationMemberBirthDate.Text = member.BirthDate.ToString ("MM/dd/yyy");

            UserInteractionEntityInformationMemberAge.Text = member.CurrentAge.ToString ();

            UserInteractionEntityInformationMemberGender.Text = member.GenderDescription;

            UserInteractionEntityInformationMemberProgram.Text = "** Not Enrolled";

            UserInteractionEntityInformationMemberProgramMemberId.Text = "**Not Enrolled";

            if (member.HasCurrentEnrollment) {

                // UserInteractionEntityInformationMemberProgram.Text = member.CurrentEnrollment.Program.Name;

                //String anchor = String.Empty;

                //anchor = "<a href=\"#\" onclick=\"javascript:MemberInformationCoverage_Toggle()\"' title=\"Toggle Coverage Information\" alt=\"Toggle Coverage Information\">" + member.CurrentEnrollment.ProgramName + "</a>";

                //UserInteractionEntityInformationMemberProgram.Text = anchor;

                UserInteractionEntityInformationMemberProgram.Text = member.CurrentEnrollment.ProgramName;

                UserInteractionEntityInformationMemberProgramMemberId.Text = member.CurrentEnrollment.ProgramMemberId;


                if (member.CurrentEnrollment.HasCurrentCoverage) {

                    UserInteractionEntityInformationMemberCoverageBenefitPlan.Text = member.CurrentEnrollment.CurrentCoverage.BenefitPlanName;

                    UserInteractionEntityInformationMemberCoverageType.Text = member.CurrentEnrollment.CurrentCoverage.CoverageTypeName;

                    UserInteractionEntityInformationMemberCoverageLevel.Text = member.CurrentEnrollment.CurrentCoverage.CoverageLevelName;

                    UserInteractionEntityInformationMemberCoverageRateCode.Text = member.CurrentEnrollment.CurrentCoverage.RateCode;

                }

                if (member.CurrentEnrollment.HasCurrentPcp) {

                    UserInteractionEntityInformationMemberPcpName.Text = Web.CommonFunctions.ProviderProfileAnchor (

                        member.CurrentEnrollmentPcp.PcpProviderId, member.CurrentEnrollmentPcp.PcpProvider.Name);

                    UserInteractionEntityInformationMemberPcpAffiliateName.Text = Web.CommonFunctions.ProviderProfileAnchor (

                        member.CurrentEnrollmentPcp.PcpAffiliateProvider.Id, member.CurrentEnrollmentPcp.PcpAffiliateProvider.Name);

                }

            }

            return;

        }

        private void InitializeMemberInformationByEntityId (Int64 entityId) {

            Client.Core.Member.Member member = MercuryApplication.MemberGetDemographicsByEntityId (entityId, true);

            if (member == null) { return; }

            InitializeMemberInformation (member);

            return;

        }

        private void InitializeMemberInformation (Int64 memberId) {

            Client.Core.Member.Member member = MercuryApplication.MemberGetDemographics (memberId, true);

            if (member == null) { return; }

            InitializeMemberInformation (member);

            return;

        }


        private void InitializeProviderInformation (Int64 providerId) {

            Client.Core.Provider.Provider provider = MercuryApplication.ProviderGet (providerId, true);

            if (provider == null) { return; }


            WorkQueueItemInformationProvider.Style.Clear ();


            #region Note Alert Icons

            Dictionary<Mercury.Server.Application.NoteImportance, Client.Core.Entity.EntityNote> entityNotes;

            entityNotes = MercuryApplication.EntityNoteGetMostRecentByAllImportances (provider.EntityId, true);


            Client.Core.Entity.EntityNote entityNote = null;

            // entityNote = MercuryApplication.EntityNoteGetMostRecentByImportance (provider.EntityId, Mercury.Server.Application.NoteImportance.Warning, false);

            if (entityNotes.ContainsKey (Mercury.Server.Application.NoteImportance.Warning)) { entityNote = entityNotes[Mercury.Server.Application.NoteImportance.Warning]; }

            if (entityNote != null) {

                if (entityNote.TerminationDate >= DateTime.Today) {

                    UserInteractionEntityInformationProviderNoteWarning.Style.Clear ();

                    UserInteractionEntityInformationProviderNoteWarning.Attributes.Add ("title", "[" + entityNote.NoteTypeName + "] " + entityNote.Subject);

                    UserInteractionEntityInformationProviderNoteWarning.Visible = true;

                }

            }

            // entityNote = MercuryApplication.EntityNoteGetMostRecentByImportance (provider.EntityId, Mercury.Server.Application.NoteImportance.Critical, false);

            entityNote = null;

            if (entityNotes.ContainsKey (Mercury.Server.Application.NoteImportance.Critical)) { entityNote = entityNotes[Mercury.Server.Application.NoteImportance.Critical]; }

            if (entityNote != null) {

                if (entityNote.TerminationDate >= DateTime.Today) {

                    UserInteractionEntityInformationProviderNoteCritical.Style.Clear ();

                    UserInteractionEntityInformationProviderNoteCritical.Attributes.Add ("title", "[" + entityNote.NoteTypeName + "] " + entityNote.Subject);

                    UserInteractionEntityInformationProviderNoteCritical.Visible = true;

                }

            }

            #endregion


            UserInteractionEntityInformationProviderName.Text = Web.CommonFunctions.ProviderProfileAnchor (providerId, provider.Name);

            UserInteractionEntityInformationProviderNpi.Text = (String.IsNullOrEmpty (provider.NationalProviderId)) ? "** Not Assigned" : provider.NationalProviderId;

            UserInteractionEntityInformationProviderProgram.Text = "** Not Enrolled";

            UserInteractionEntityInformationProviderProgramProviderId.Text = "**Not Enrolled";

            if (provider.HasCurrentEnrollment) {

                UserInteractionEntityInformationProviderProgram.Text = provider.CurrentEnrollment.Program.Name;

                UserInteractionEntityInformationProviderProgramProviderId.Text = provider.CurrentEnrollment.ProgramProviderId;

            }

            return;

        }

        private void InitializeEntityObjectInformation (Server.Application.EntityType entityType, Int64 entityObjectId) {

            switch (entityType) {

                case Mercury.Server.Application.EntityType.Member: InitializeMemberInformation (entityObjectId); break;

                case Mercury.Server.Application.EntityType.Provider: InitializeProviderInformation (entityObjectId); break;

                default: throw new ApplicationException ("[Workflow.aspx:InitializeEntityObjectInformation] Not Implemented: " + entityType.ToString ());

            }

            return;

        }

        #endregion 


        #region Load User Control

        public String LoadUserControl (String controlName) {

            DateTime startTime = DateTime.Now;

            Control targetControl = WorkflowContentPanel.FindControl (ContentControlId);

            if (targetControl == null) {

                WorkflowContentPanel.Controls.Clear ();

                UserControl contentControl = (UserControl)LoadControl (controlName);

#if DEBUG

                System.Diagnostics.Debug.WriteLine ("User Control Load (1): " + DateTime.Now.Subtract (startTime).TotalMilliseconds.ToString ());

#endif

                ContentControlId = SessionCachePrefix + controlName.Split ('.')[0].Replace ("/", "").Replace ("~", "");

                contentControl.ID = ContentControlId;

                WorkflowContentPanel.Controls.Add (contentControl);


#if DEBUG

                System.Diagnostics.Debug.WriteLine ("User Control Load (2): " + DateTime.Now.Subtract (startTime).TotalMilliseconds.ToString ());

#endif

                ContentControlName = controlName;

            }


#if DEBUG

            System.Diagnostics.Debug.WriteLine ("User Control Load (3): " + DateTime.Now.Subtract (startTime).TotalMilliseconds.ToString ());

#endif

            TelerikAjaxManager.ResponseScripts.Add (ResponseScriptWorkflowPaint);

            return ContentControlId;

        }

        public void ReloadUserControl () {

            UserControl contentControl = (UserControl)LoadControl (ContentControlName);

            contentControl.ID = ContentControlId;

            WorkflowContentPanel.Controls.Add (contentControl);

            TelerikAjaxManager.ResponseScripts.Add (ResponseScriptWorkflowPaint);

            return;

        }

        #endregion


        #region Workflow Control Events

        protected void WorkflowStart_OnClick (Object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }


            System.DateTime startTime = DateTime.Now;

            Server.Application.WorkflowStartRequest startRequest = WorkflowStartRequest;

            startRequest.Arguments = new System.Collections.Generic.Dictionary<String, Object> ();

            foreach (String currentKey in Request.QueryString.Keys) {

                startRequest.Arguments.Add (currentKey, Request.QueryString[currentKey]);

            }

            startRequest.WorkflowId = CurrentWorkflow.Id;

            startRequest.WorkflowName = CurrentWorkflow.Name;

            WorkflowStartRequest = startRequest;


            WorkflowResponse = MercuryApplication.WorkflowStart (startRequest);

            System.Diagnostics.Debug.WriteLine ("WorkflowStart: " + DateTime.Now.Subtract (startTime).TotalMilliseconds.ToString ());

            WorkflowInstanceId = WorkflowResponse.WorkflowInstanceId;

            startTime = DateTime.Now;

            HandleWorkflowResponse ();

            System.Diagnostics.Debug.WriteLine ("WorkflowStart_HandleResponse: " + DateTime.Now.Subtract (startTime).TotalMilliseconds.ToString ());

            return;

        }

        protected void WorkflowContinue_OnClick (Object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }


            System.DateTime startTime = DateTime.Now;

            WorkflowResponse = MercuryApplication.WorkflowContinue (CurrentWorkflow.Name, WorkflowInstanceId, UserInteractionResponse);

            System.Diagnostics.Debug.WriteLine ("WorkflowContinue: " + DateTime.Now.Subtract (startTime).TotalMilliseconds.ToString ());


            startTime = DateTime.Now;

            UserInteractionResponse = null;

            ContentControlName = String.Empty;

            System.Diagnostics.Debug.WriteLine ("WorkflowContinue_Clear: " + DateTime.Now.Subtract (startTime).TotalMilliseconds.ToString ());


            startTime = DateTime.Now;

            HandleWorkflowResponse ();

            System.Diagnostics.Debug.WriteLine ("WorkflowContinue_HandleResponse: " + DateTime.Now.Subtract (startTime).TotalMilliseconds.ToString ());

            return;

        }

        protected void WorkflowResume_OnClick (Object sender, EventArgs e) {

            WorkflowContinue_OnClick (sender, e);
            
            return;

        }

        #endregion


        #region Workflow Handle Responses

        private void HandleWorkflowResponse () {

            if (MercuryApplication == null) { return; }


            System.DateTime startTime = DateTime.Now;

            WorkflowContentPanel.Controls.Clear ();

            TelerikAjaxManager.ResponseScripts.Add (ResponseScriptWorkflowPaint);

            
            System.Diagnostics.Debug.WriteLine ("WorkflowHandleResponse_Clear: " + DateTime.Now.Subtract (startTime).TotalMilliseconds.ToString ());


            if (WorkflowResponse.HasException) {

                WorkflowExceptionMessageRow.Style.Clear ();


                if (WorkflowResponse.Exception != null) { WorkflowExceptionMessage.Text = WorkflowResponse.Exception.Message; }

                else { WorkflowExceptionMessage.Text = "Unknown and unhandled Workflow Exception Occurred. Please check Server Logs."; }


                WorkflowExitContainer.Style.Clear ();

                WorkflowCancelContainer.Style.Add ("display", "none");


                if (WorkflowResponse.WorkflowSteps != null) {

                    if (WorkflowResponse.WorkflowSteps.Length > 0) {

                        LoadUserControl ("WorkflowControls/WorkflowSummary.ascx");

                        WorkflowControls.WorkflowSummary workflowSummaryControl = (WorkflowControls.WorkflowSummary)Page.FindControl (ContentControlId);

                        workflowSummaryControl.SetWorkflowSteps (WorkflowResponse.WorkflowSteps);

                    }

                }
            
                return;

            }


            try { 

                if ((WorkflowResponse.WorkflowStatus == Mercury.Server.Application.WorkflowStatus.Unloaded) 

                    && (WorkflowResponse.UserInteractionRequest != null)) { 

                    // USER INTERACTION REQUESTED

                    WorkflowActionMessage.Text = "[" + Mercury.Server.CommonFunctions.EnumerationToString (WorkflowResponse.UserInteractionRequest.InteractionType) + "] ";

                    WorkflowActionMessage.Text += WorkflowResponse.UserInteractionRequest.Message;

                    switch (WorkflowResponse.UserInteractionRequest.InteractionType) {

                        //case Mercury.Server.Application.UserInteractionType.UserInterfaceUpdate: HandleWorkflowResponse_UserInterfaceUpdate (); break;

                        case Mercury.Server.Application.UserInteractionType.Prompt: HandleWorkflowResponse_PromptUser (); break;

                        //case Mercury.Server.Application.UserInteractionType.RequireEntity: HandleWorkflowResponse_RequireEntity (); break;

                        case Mercury.Server.Application.UserInteractionType.ContactEntity: HandleWorkflowResponse_ContactEntity (); break;

                        case Mercury.Server.Application.UserInteractionType.RequireForm: HandleWorkflowResponse_RequireForm (); break;

                        case Mercury.Server.Application.UserInteractionType.SendCorrespondence: HandleWorkflowResponse_SendCorrespondence (); break;

                        //case Mercury.Server.Application.UserInteractionType.CreateModifyMemberCase: HandleWorkflowResponse_CreateModifyMemberCase (); break;

                        case Mercury.Server.Application.UserInteractionType.OpenImage: HandleWorkflowResponse_OpenImage (); break;

                        default: throw new ApplicationException ("Unable to handle User Interaction Request Type: " + WorkflowResponse.UserInteractionRequest.InteractionType.ToString ());

                    }

                }

                else { 

                    // NON-USER INTERACTION REQUESTED

                    switch (WorkflowResponse.WorkflowStatus) {

                        case Mercury.Server.Application.WorkflowStatus.Completed:

                            WorkflowActionMessage.Text = "Workflow has completed.";

                            WorkflowIcon.Src = "/Images/Common16/Ok.png";

                            break;

                        case Mercury.Server.Application.WorkflowStatus.Suspended:

                            WorkflowActionMessage.Text = "Workflow has been suspended.";

                            WorkflowIcon.Src = "/Images/Common16/Suspend.png";

                            break;


                        case Mercury.Server.Application.WorkflowStatus.Terminated:

                            WorkflowActionMessage.Text = "Workflow has been terminated.";

                            WorkflowIcon.Src = "/Images/Common16/Critical.png";

                            break;

                    }

                    LoadUserControl ("WorkflowControls/WorkflowSummary.ascx");

                    WorkflowControls.WorkflowSummary workflowSummaryControl = (WorkflowControls.WorkflowSummary)Page.FindControl (ContentControlId);



                    List<Mercury.Server.Application.WorkflowStep> workflowSteps = new List<Mercury.Server.Application.WorkflowStep>();

                    if (WorkflowResponse.WorkQueueItemId != 0) {

                        workflowSteps = MercuryApplication.WorkQueueItemWorkflowStepsGet (WorkflowResponse.WorkQueueItemId, false);
                        
                        InitializeEntityInformationFromWorkQueueItem (WorkflowResponse.WorkQueueItemId);
                  
                    }

                    else { workflowSteps.AddRange (WorkflowResponse.WorkflowSteps); }

                    workflowSummaryControl.SetWorkflowSteps (workflowSteps);


                    #region Append Warnings and Errors

                    Boolean completionHasWarnings = false;

                    Boolean completionHasCriticals = false;


                    foreach (Mercury.Server.Application.WorkflowStep currentWorkflow in workflowSteps) {

                        completionHasWarnings |= (currentWorkflow.StepStatus == Mercury.Server.Application.WorkflowStepStatus.Warning);

                        completionHasCriticals |= (currentWorkflow.StepStatus == Mercury.Server.Application.WorkflowStepStatus.Critical);

                    }


                    if (completionHasWarnings) {

                        WorkflowActionMessage.Text += " (with warnings)";

                        WorkflowIcon.Src = "/Images/Common16/Warning.png";

                    }

                    if (completionHasCriticals) {

                        WorkflowActionMessage.Text += " (with errors)";

                        WorkflowIcon.Src = "/Images/Common16/Critical.png";

                    }

                    #endregion 


                    #region Append Last Step Message

                    if (workflowSteps != null) {

                        if (workflowSteps.Count > 0) {

                            WorkflowLastMessageIcon.Src = "/Images/Common16/" + workflowSteps[workflowSteps.Count - 1].StepStatus.ToString () + ".png";

                            WorkflowLastMessage.Text = workflowSteps[workflowSteps.Count - 1].Description;

                            WorkflowLastMessageContainer.Style.Clear ();

                        }

                    }

                    #endregion 


                    WorkflowCancelContainer.Style.Add ("display", "none");

                    WorkflowExitContainer.Style.Clear ();

                    if (ReferrerUrl.Contains ("Workspace.aspx")) { // ONLY ALLOW NEXT ITEM FROM THE ORIGINAL WORKSPACE

                        WorkflowNextItemContainer.Style.Clear ();

                    }

                }

            }

            catch (Exception responseHandlerException) {
                
                WorkflowExceptionMessageRow.Style.Clear ();

                WorkflowExceptionMessage.Text = "[Workflow Handle Response] " + responseHandlerException.Message;

                WorkflowCancelContainer.Style.Add ("display", "none");

                WorkflowExitContainer.Style.Clear ();

                return;

            }
            
            return;

        }

        #endregion 


        #region Workflow Handle Response - Specialized Methods

        protected void HandleWorkflowResponse_ContactEntity () {

            Server.Application.WorkflowUserInteractionRequestContactEntity contactEntity;

            contactEntity = (Server.Application.WorkflowUserInteractionRequestContactEntity)WorkflowResponse.UserInteractionRequest;


            if (contactEntity.Entity == null) {

                // NO ENTITY SPECIFIED FOR CONTACT - ERROR

                WorkflowExceptionMessageRow.Style.Clear ();

                WorkflowExceptionMessage.Text = "Contact Entity Request has no Entity assigned to it to contact. Unable to continue."; 


                WorkflowExitContainer.Style.Clear ();

                WorkflowCancelContainer.Style.Add ("display", "none");

                return;

            }

            switch (contactEntity.Entity.EntityType) {

                case Mercury.Server.Application.EntityType.Member: InitializeMemberInformationByEntityId (contactEntity.Entity.Id); break;

                default: InitializeEntityInformation (contactEntity.Entity.Id); break;

            }


            LoadUserControl ("UserInteractions/ContactEntity.ascx");

            UserInteractions.ContactEntity contactEntityControl = (UserInteractions.ContactEntity)Page.FindControl (ContentControlId);

            contactEntityControl.ResponseScript = ResponseScriptWorkflowContinue;

            return;

        }

        protected void HandleWorkflowResponse_RequireForm () {

            Server.Application.WorkflowUserInteractionRequestRequireForm requireForm;

            requireForm = (Server.Application.WorkflowUserInteractionRequestRequireForm)WorkflowResponse.UserInteractionRequest;

            InitializeEntityObjectInformation (requireForm.EntityType, requireForm.EntityObjectId);


            LoadUserControl ("UserInteractions/RequireForm.ascx");

            UserInteractions.RequireForm requireFormControl = (UserInteractions.RequireForm)Page.FindControl (ContentControlId);


            Client.Core.Forms.Form form = new Mercury.Client.Core.Forms.Form (MercuryApplication, requireForm.Form);

            form.EntityType = requireForm.EntityType;

            form.EntityObjectId = requireForm.EntityObjectId;

            requireFormControl.AllowSaveAsDraft = requireForm.AllowSaveAsDraft;

            requireFormControl.AllowCancel = requireForm.AllowCancel;

            requireFormControl.SetForm (form);

            requireFormControl.ResponseScript = ResponseScriptWorkflowContinue;


            requireFormControl.ResponseScript = ResponseScriptWorkflowContinue;

            return;

        }

        protected void HandleWorkflowResponse_SendCorrespondence () {

            Server.Application.WorkflowUserInteractionRequestSendCorrespondence sendCorrespondence;

            sendCorrespondence = (Server.Application.WorkflowUserInteractionRequestSendCorrespondence)WorkflowResponse.UserInteractionRequest;

            InitializeEntityInformation (new Client.Core.Entity.Entity (MercuryApplication, sendCorrespondence.Entity));


            LoadUserControl ("UserInteractions/SendCorrespondence.ascx");

            UserInteractions.SendCorrespondence sendCorrespondenceControl = (UserInteractions.SendCorrespondence)Page.FindControl (ContentControlId);

            sendCorrespondenceControl.ResponseScript = ResponseScriptWorkflowContinue;

            return;

        }

        protected void HandleWorkflowResponse_PromptUser () {

            Server.Application.WorkflowUserInteractionRequestPromptUser promptUser;

            promptUser = (Mercury.Server.Application.WorkflowUserInteractionRequestPromptUser)WorkflowResponse.UserInteractionRequest;


            LoadUserControl ("UserInteractions/PromptUser.ascx");

            UserInteractions.PromptUser promptUserControl = (UserInteractions.PromptUser)Page.FindControl (ContentControlId);

            promptUserControl.ResponseScript = ResponseScriptWorkflowContinue;

            return;

        }

        protected void HandleWorkflowResponse_OpenImage () {

            Server.Application.WorkflowUserInteractionRequestOpenImage openImage;

            openImage = (Mercury.Server.Application.WorkflowUserInteractionRequestOpenImage)WorkflowResponse.UserInteractionRequest;


            LoadUserControl ("UserInteractions/OpenImage.ascx");

            UserInteractions.OpenImage interactionControl = (UserInteractions.OpenImage)Page.FindControl (ContentControlId);

            interactionControl.ResponseScript = ResponseScriptWorkflowContinue;

            return;

        }

        #endregion 


        #region Button Events

        protected void WorkflowNextItemButton_OnClick (Object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            String alertMessage = String.Empty;


            Client.Core.Work.WorkQueue workQueue = MercuryApplication.WorkQueueGetByWorkQueueItem (WorkflowResponse.WorkQueueItemId);

            if (workQueue == null) {

                TelerikAjaxManager.ResponseScripts.Add ("alert ('Get Next Work Queue Item is not Available in this Context (No Work Queue Found).');");

                WorkflowNextItemContainer.Style.Add ("display", "none");

            }

            else {

                Mercury.Server.Application.GetWorkResponse response;

                response = MercuryApplication.WorkQueueGetWork (workQueue.Id);


                if (response.HasException) {

                    // EXCEPTION OCCURRED WHILE GETTING WORK QUEUE ITEM

                    alertMessage = "[" + response.Exception.Source + "] " + response.Exception.Message;

                    alertMessage.Replace ("'", "");


                    TelerikAjaxManager.ResponseScripts.Add ("alert ('" + alertMessage + "');");

                    WorkflowNextItemContainer.Style.Add ("display", "none");


                    MercuryApplication.SetLastException (response.Exception);

                }

                else if (response.WorkQueueItem == null) {

                    // NO WORK QUEUE ITEM AVAILABLE

                    alertMessage = "No Work Queue Items Available in the selected Work Queue.";

                    alertMessage.Replace ("'", "");


                    TelerikAjaxManager.ResponseScripts.Add ("alert ('" + alertMessage + "');");

                    WorkflowNextItemContainer.Style.Add ("display", "none");

                }

                else {

                    // VALID WORK QUEUE ITEM FOUND AND RETURNED

                    if (response.Workflow != null) {

                        // KICK-OFF WORKFLOW PROCESS

                        String parameterString = String.Empty;

                        parameterString = parameterString + "?WorkflowId=" + workQueue.WorkflowId.ToString ();

                        parameterString = parameterString + "&WorkQueueItemId=" + response.WorkQueueItem.Id.ToString ();

                        parameterString = parameterString + "&" + response.WorkQueueItem.ItemObjectType + "Id=" + response.WorkQueueItem.ItemObjectId.ToString ();

                        parameterString = parameterString + "&ReferrerUrlSessionCachePrefix=" + SessionCachePrefix;


                        if (response.WorkQueueItem.WorkflowInstanceId != Guid.Empty) {

                            parameterString = parameterString + "&WorkflowInstanceId=" + response.WorkQueueItem.WorkflowInstanceId.ToString ();

                        }

                        Response.RedirectLocation = "/Application/Workflow/Workflow.aspx" + parameterString;

                    }

                    else { // MANUAL WORKFLOW, JUST HARD REFRESH

                        alertMessage = "Manual Workflow, unable to continue.";

                        alertMessage.Replace ("'", "");


                        TelerikAjaxManager.ResponseScripts.Add ("alert ('" + alertMessage + "');");

                        WorkflowNextItemContainer.Style.Add ("display", "none");

                    }

                }

            }

            return;

        }

        #endregion 

    }

}