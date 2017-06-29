using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercury.Clients.Mvc.Models.Workflow {

    public class WorkflowModel : Models.ApplicationModel {

        #region Private Properties

        private ActionResultType actionResultType = ActionResultType.View;


        private Int64 workflowId = 0;

        private Guid workflowInstanceId = Guid.Empty;

        private Client.Core.Work.Workflow workflow = null;
        
        
        private Int64 workQueueItemId = 0;

        private Client.Core.Work.WorkQueueItem workQueueItem = null;


        private String workflowTitle = "Workflow: Not Available";

        private String workflowIcon = "gear.png";

        private String workflowActionMessage = String.Empty;

        private String lastWorkflowMessage = String.Empty;

        private String workflowInformationalMessage = String.Empty;
        
        
        private Mercury.Server.Application.WorkflowResponse workflowResponse;

        private String workflowControl = String.Empty;


        private Mercury.Client.Core.Entity.Entity entity = null;

        private Mercury.Client.Core.Member.Member member = null;


        private Models.Controls.EntityContactModel entityContactModel = null; 

        #endregion 


        #region Public Properties

        public override bool StoreModelStateInMemory { 
            
            get { return base.StoreModelStateInMemory; }

            set {

                base.StoreModelStateInMemory = value;

                if (entityContactModel != null) { entityContactModel.StoreModelStateInMemory = value; }

            }

        }

        public ActionResultType ActionResultType { get { return actionResultType; } }
        

        public String UrlReferrer { get; set; }

        public String UrlOriginal { get; set; }


        public Int64 WorkflowId { 
            
            get { return workflowId; }

            set {

                workflowId = value; 

                workflow = MercuryApplication.WorkflowGet (workflowId, true);

            }

        }

        public Guid WorkflowInstanceId { get { return workflowInstanceId; } set { workflowInstanceId = value; } }

        public Client.Core.Work.Workflow Workflow {

            get {

                if (workflow == null) { workflow = MercuryApplication.WorkflowGet (WorkflowId, true); }

                return workflow;

            }

        }

        public String WorkflowName { get { return ((Workflow != null) ? Workflow.Name : String.Empty); } }


        public Int64 WorkQueueItemId {

            get { return workQueueItemId; }

            set {

                workQueueItemId = value;

                workQueueItem = this.WorkQueueItem; // SELF REFERENCE TO FORCE UPDATE OF WORK QUEUE ITEM
                
            }

        }   
        
        public Client.Core.Work.WorkQueueItem WorkQueueItem {

            get {

                if (workQueueItem != null) { return workQueueItem; }

                workQueueItem = MercuryApplication.WorkQueueItemGet (workQueueItemId);

                return workQueueItem;

            }

        }


        public Boolean CanWork {

            get {

                if (workQueueItemId == 0) { return true; } // DIRECT REQUEST FOR WORK (NOT RELATED TO AN ITEM

                if (workQueueItem == null) { return true; }

                Boolean canWork = false;

                if ((workQueueItem.AssignedToSecurityAuthorityId == MercuryApplication.Session.SecurityAuthorityId)

                    && (workQueueItem.AssignedToUserAccountId == MercuryApplication.Session.UserAccountId)) { canWork = true; }

                else if (workQueueItem.AssignTo (MercuryApplication.Session.SecurityAuthorityId, MercuryApplication.Session.UserAccountId, MercuryApplication.Session.UserAccountName, MercuryApplication.Session.UserDisplayName, "Member Profile - Work History")) {

                    canWork = true;

                }

                return canWork;

            }

        }

        public String WorkflowTitle { get { return workflowTitle; } }

        public String WorkflowIcon { get { return workflowIcon; } }

        public String WorkflowIconUrl { get { return @"/images/common16/" + workflowIcon; } }

        public String WorkflowActionMessage { get { return workflowActionMessage; } }

        public String LastWorkflowMessage { get { return lastWorkflowMessage; } }


        public Mercury.Server.Application.WorkflowResponse WorkflowResponse { get { return workflowResponse; } }

        public String WorkflowControl { get { return workflowControl; } set { workflowControl = value; } }

        public String ResponseScriptWorkflowStart { get { return "<script language=\"JavaScript\">$(document).ready (function () { $('#WorkflowStart').click (); });</script>"; } }

        public String WorkflowControlResponseScript { get; set; }


        public Mercury.Client.Core.Entity.Entity Entity { get { return entity; } }

        public Mercury.Client.Core.Member.Member Member {

            get {

                if (member != null) { return member; }

                if (entity == null) { return null; }

                if (entity.EntityType != Server.Application.EntityType.Member) { return null; }

                member = MercuryApplication.MemberGetByEntityId (entity.Id, true);

                return member;

            }

        }

        public Models.Controls.EntityContactModel EntityContactModel { get { return entityContactModel; } set { entityContactModel = value; } }

        #endregion 


        #region Constructors

        public WorkflowModel (Int64 forWorkflowId, Int64 forWorkQueueItemId, String forUrlReferrer) {

            WorkflowId = forWorkflowId;

            WorkQueueItemId = forWorkQueueItemId;

            workflowActionMessage = "Workflow Starting";

            UrlReferrer = forUrlReferrer;


            Initialize ();

            return;

        }

        public WorkflowModel (Guid forWorkflowInstanceId) {

            WorkflowInstanceId = forWorkflowInstanceId;

            return;

        }

        public WorkflowModel (System.Collections.Specialized.NameValueCollection form) : base (form) {

            // CREATE MODEL AND RESTORE STATE FROM REQUEST FORM

            UrlOriginal = form["UrlOriginal"];

            UrlReferrer = form["UrlReferrer"];


            Int64.TryParse (form["WorkflowId"], out workflowId);

            WorkflowId = workflowId; // SELF REFERENCE TO FORCE UPDATE

            Guid.TryParse (form["WorkflowInstanceId"], out workflowInstanceId);
            
            Int64.TryParse (form["WorkQueueItemId"], out workQueueItemId);

            WorkQueueItemId = workQueueItemId; // SELF REFERENCE TO FORCE UPDATE


            workflowTitle = form["WorkflowTitle"];

            workflowActionMessage = form["WorkflowActionMessage"];

            lastWorkflowMessage = form["LastWorkflowMessage"];


            WorkflowControl = form["WorkflowControl"];


            Int64 entityId = 0;

            Int64.TryParse (form["Entity.Id"], out entityId);

            entity = MercuryApplication.EntityGet (entityId, true);


            UpdateValues (form);

            return;

        }

        #endregion 


        #region Public Methods

        public void Initialize () {
    
            if (workflowInstanceId != Guid.Empty) {

                // RESUMING EXISTING WORKFLOW 

                if (Workflow != null) {

                    workflowTitle = "Workflow: " + Workflow.Name + ((!String.IsNullOrWhiteSpace (Workflow.Description)) ? " [" + Workflow.Description + "]" : String.Empty);

                }

                else {

                    workflowTitle = "Workflow Engine";

                    SetException ("Workflow Exception: Unable to Load Workflow.");

                }

            }

            else {

                // STARTING NEW WORKFLOW

                workflowTitle = "Workflow Engine";

                if ((CanWork) && (Workflow != null)) {

                    workflowTitle = "Workflow: " + Workflow.Name + ((!String.IsNullOrWhiteSpace (Workflow.Description)) ? " [" + Workflow.Description + "]" : String.Empty);

                }

                else if (!CanWork) {

                    SetException ("Permission Denied: Unable to perform work on the selected Work Queue Item. This Item might already be owned. Please try again.");

                }

                else {

                    SetException ("Workflow Exception: " + MercuryApplication.LastException);

                }

            }

            return;

        }

        public override void UpdateValues (System.Collections.Specialized.NameValueCollection form) {

            if (entityContactModel != null) {

                entityContactModel.UpdateValues (form);

            }

            return;

        }

        public void StartWorkflow () {

            System.DateTime startTime = DateTime.Now;

            String originalQueryString = ((!String.IsNullOrWhiteSpace (UrlOriginal)) ? ((UrlOriginal.Split ('?').Length > 0) ? UrlOriginal.Split ('?')[1] : String.Empty) : String.Empty);


            // CREATE WORKFLOW START REQUEST 

            Mercury.Server.Application.WorkflowStartRequest workflowStartRequest = new Server.Application.WorkflowStartRequest ();

            workflowStartRequest.Arguments = new System.Collections.Generic.Dictionary<String, Object> ();

            workflowStartRequest.WorkflowId = WorkflowId;

            workflowStartRequest.WorkflowInstanceId = WorkflowInstanceId;

            workflowStartRequest.WorkflowName = WorkflowName;

            // LOAD ARGUMENTS FROM ORIGINAL QUERY STRING 

            for (Int32 currentIndex = 0; currentIndex < originalQueryString.Split ('&').Length; currentIndex++) {

                String parameter = originalQueryString.Split ('&')[currentIndex];

                String parameterName = ((parameter.Contains ("=")) ? parameter.Split ('=')[0] : String.Empty);

                String parameterValue = ((parameter.Contains ("=")) ? parameter.Split ('=')[1] : String.Empty);

                if (!String.IsNullOrWhiteSpace (parameterName)) {

                    workflowStartRequest.Arguments.Add (parameterName, parameterValue);

                }

            }


            // CALL WORKFLOW START ON MERCURY SERVER

            workflowResponse = MercuryApplication.WorkflowStart (workflowStartRequest);

            System.Diagnostics.Debug.WriteLine ("WorkflowStart: " + DateTime.Now.Subtract (startTime).TotalMilliseconds.ToString ());

            WorkflowInstanceId = workflowResponse.WorkflowInstanceId; // ASSIGN NEW WORKFLOW INSTANCE ID FOR FUTURE REFERENCE (CONTINUE AND RESUME)


            startTime = DateTime.Now;

            HandleWorkflowResponse ();

            actionResultType = Models.ActionResultType.Control;

            System.Diagnostics.Debug.WriteLine ("WorkflowStart_HandleResponse: " + DateTime.Now.Subtract (startTime).TotalMilliseconds.ToString ());

            return;

        }

        public void ContinueWorkflow (System.Collections.Specialized.NameValueCollection form) {
            
            System.DateTime startTime = DateTime.Now;

            Mercury.Server.Application.WorkflowUserInteractionResponseBase userInteractionResponse = null;


            // COMING BACK IN FROM A POSTBACK (AJAX REQUEST)

            // USE THE SELECTED WORKFLOW CONTROL TO RESTORE STATE AND VALIDATE 

            System.Diagnostics.Debug.WriteLine ("Workflow Control - Continue Workflow: " + workflowControl);


            // AT THIS POINT, THE WORKFLOW CONTROL HAS BEEN REBUILT AND UPDATED FROM THE CLIENT 

            if (workflowControl.Contains ("ContactEntity")) {

                userInteractionResponse = HandleUserInteraction_ContactEntity (form);

            }

            // IF USER INTERACTION RESPONSE AVAILABLE, CONTINUE WORKFLOW, OR REBUILD EXISTING PAGE 

            if (userInteractionResponse != null) {

                WorkflowControl = String.Empty;

                workflowResponse = MercuryApplication.WorkflowContinue (WorkflowName, WorkflowInstanceId, userInteractionResponse);

                HandleWorkflowResponse ();

                actionResultType = Models.ActionResultType.Control;

            }

            else {

                actionResultType = Models.ActionResultType.View;

            }


            System.Diagnostics.Debug.WriteLine ("WorkflowContinue_HandleResponse: " + DateTime.Now.Subtract (startTime).TotalMilliseconds.ToString ());

            return;

        }

        #endregion 


        #region Workflow Continue - Handle User Interaction 

        public Server.Application.WorkflowUserInteractionResponseBase HandleUserInteraction_ContactEntity (System.Collections.Specialized.NameValueCollection form) {

            Server.Application.WorkflowUserInteractionResponseContactEntity contactResponse = null;

            entityContactModel = new Controls.EntityContactModel (form); // CREATE MODEL, UPDATE VALUES FROM STORE (EITHER CLIENT OR SESSION)

            entityContactModel.RaiseEvent (form["EventTarget"], form["EventArguments"]); // RAISE EVENT, VALIDATE 


            // IF CANCEL WAS REQUESTED, OR A VALID ENTITY CONTACT CREATED, CREATE RESPONSE TO CONTINUE WORKFLOW 

            if ((entityContactModel.Canceled) || (entityContactModel.EntityContact != null)) {

                // CREATE CUSTOM CONTACT RESPONSE

                contactResponse = new Server.Application.WorkflowUserInteractionResponseContactEntity ();

                contactResponse.InteractionType = Server.Application.UserInteractionType.ContactEntity;

                if (entityContactModel.EntityContact != null) {

                    contactResponse.EntityContact = (Mercury.Server.Application.EntityContact)entityContactModel.EntityContact.ToServerObject ();

                }

                contactResponse.Cancel = entityContactModel.Canceled;

            }

            return contactResponse;
            
        }

        #endregion 


        #region Workflow Handle Responses

        private void HandleWorkflowResponse () {

            if (MercuryApplication == null) { return; }


            System.DateTime startTime = DateTime.Now;

            String exceptionMessage = String.Empty;

            //WorkflowContentPanel.Controls.Clear ();

            //TelerikAjaxManager.ResponseScripts.Add (ResponseScriptWorkflowPaint);


            System.Diagnostics.Debug.WriteLine ("WorkflowHandleResponse_Clear: " + DateTime.Now.Subtract (startTime).TotalMilliseconds.ToString ());


            if (workflowResponse.HasException) {

                if (workflowResponse.Exception != null) {

                    exceptionMessage = "[" + workflowResponse.Exception.Source + "] ";

                    exceptionMessage += workflowResponse.Exception.Message;

                    SetException (exceptionMessage);

                    // SetInformationMessage (workflowResponse.Exception.StackTrace);
                
                }

                else { SetException ("Unknown and unhandled Workflow Exception Occurred. Please check Server Logs."); }


                //WorkflowExitContainer.Style.Clear ();

                //WorkflowCancelContainer.Style.Add ("display", "none");


                if (workflowResponse.WorkflowSteps != null) {

                    if (workflowResponse.WorkflowSteps.Length > 0) {

                        workflowControl = "/Views/Workflow/WorkflowControls/WorkflowSummary.cshtml";

                        // LoadUserControl ("WorkflowControls/WorkflowSummary.ascx");

                        // WorkflowControls.WorkflowSummary workflowSummaryControl = (WorkflowControls.WorkflowSummary)Page.FindControl (ContentControlId);

                        // workflowSummaryControl.SetWorkflowSteps (WorkflowResponse.WorkflowSteps);

                    }

                }

                return;

            }


            try {

                if ((WorkflowResponse.WorkflowStatus == Mercury.Server.Application.WorkflowStatus.Unloaded)

                    && (WorkflowResponse.UserInteractionRequest != null)) {

                    // USER INTERACTION REQUESTED

                    workflowActionMessage = "[" + Mercury.Server.CommonFunctions.EnumerationToString (WorkflowResponse.UserInteractionRequest.InteractionType) + "] ";

                    workflowActionMessage += WorkflowResponse.UserInteractionRequest.Message;

                    switch (WorkflowResponse.UserInteractionRequest.InteractionType) {

                    //    //case Mercury.Server.Application.UserInteractionType.UserInterfaceUpdate: HandleWorkflowResponse_UserInterfaceUpdate (); break;

                    //    case Mercury.Server.Application.UserInteractionType.Prompt: HandleWorkflowResponse_PromptUser (); break;

                    //    //case Mercury.Server.Application.UserInteractionType.RequireEntity: HandleWorkflowResponse_RequireEntity (); break;

                        case Mercury.Server.Application.UserInteractionType.ContactEntity: HandleWorkflowResponse_ContactEntity (); break;

                        case Mercury.Server.Application.UserInteractionType.RequireForm: HandleWorkflowResponse_RequireForm (); break;

                    //    case Mercury.Server.Application.UserInteractionType.SendCorrespondence: HandleWorkflowResponse_SendCorrespondence (); break;

                    //    //case Mercury.Server.Application.UserInteractionType.CreateModifyMemberCase: HandleWorkflowResponse_CreateModifyMemberCase (); break;

                    //    case Mercury.Server.Application.UserInteractionType.OpenImage: HandleWorkflowResponse_OpenImage (); break;

                        default: throw new ApplicationException ("Unable to handle User Interaction Request Type: " + WorkflowResponse.UserInteractionRequest.InteractionType.ToString ());

                    }

                }

                else {

                    // NON-USER INTERACTION REQUESTED

                    switch (WorkflowResponse.WorkflowStatus) {

                        case Mercury.Server.Application.WorkflowStatus.Completed:

                            workflowActionMessage = "Workflow has completed.";

                            workflowIcon = "Ok.png";

                            break;

                        case Mercury.Server.Application.WorkflowStatus.Suspended:

                            workflowActionMessage = "Workflow has been suspended.";

                            workflowIcon = "Suspend.png";

                            break;


                        case Mercury.Server.Application.WorkflowStatus.Terminated:

                            workflowActionMessage = "Workflow has been terminated.";

                            workflowIcon = "Critical.png";

                            break;

                    }

                    //LoadUserControl ("WorkflowControls/WorkflowSummary.ascx");

                    //WorkflowControls.WorkflowSummary workflowSummaryControl = (WorkflowControls.WorkflowSummary)Page.FindControl (ContentControlId);



                    //List<Mercury.Server.Application.WorkflowStep> workflowSteps = new List<Mercury.Server.Application.WorkflowStep> ();

                    //if (WorkflowResponse.WorkQueueItemId != 0) {

                    //    workflowSteps = MercuryApplication.WorkQueueItemWorkflowStepsGet (WorkflowResponse.WorkQueueItemId, false);

                    //    InitializeEntityInformationFromWorkQueueItem (WorkflowResponse.WorkQueueItemId);

                    //}

                    //else { workflowSteps.AddRange (WorkflowResponse.WorkflowSteps); }

                    //workflowSummaryControl.SetWorkflowSteps (workflowSteps);


                    //#region Append Warnings and Errors

                    //Boolean completionHasWarnings = false;

                    //Boolean completionHasCriticals = false;


                    //foreach (Mercury.Server.Application.WorkflowStep currentWorkflow in workflowSteps) {

                    //    completionHasWarnings |= (currentWorkflow.StepStatus == Mercury.Server.Application.WorkflowStepStatus.Warning);

                    //    completionHasCriticals |= (currentWorkflow.StepStatus == Mercury.Server.Application.WorkflowStepStatus.Critical);

                    //}


                    //if (completionHasWarnings) {

                    //    WorkflowActionMessage.Text += " (with warnings)";

                    //    WorkflowIcon.Src = "/Images/Common16/Warning.png";

                    //}

                    //if (completionHasCriticals) {

                    //    WorkflowActionMessage.Text += " (with errors)";

                    //    WorkflowIcon.Src = "/Images/Common16/Critical.png";

                    //}

                    //#endregion


                    //#region Append Last Step Message

                    //if (workflowSteps != null) {

                    //    if (workflowSteps.Count > 0) {

                    //        WorkflowLastMessageIcon.Src = "/Images/Common16/" + workflowSteps[workflowSteps.Count - 1].StepStatus.ToString () + ".png";

                    //        WorkflowLastMessage.Text = workflowSteps[workflowSteps.Count - 1].Description;

                    //        WorkflowLastMessageContainer.Style.Clear ();

                    //    }

                    //}

                    //#endregion


                    //WorkflowCancelContainer.Style.Add ("display", "none");

                    //WorkflowExitContainer.Style.Clear ();

                    //if (ReferrerUrl.Contains ("Workspace.aspx")) { // ONLY ALLOW NEXT ITEM FROM THE ORIGINAL WORKSPACE

                    //    WorkflowNextItemContainer.Style.Clear ();

                    //}

                }

            }

            catch (Exception responseHandlerException) {

                SetException ("[Workflow Handle Response] " + responseHandlerException.Message);

                return;

            }

            return;

        }

        #endregion 
        

        #region Workflow Handle Response - Specialized Methods

        protected void HandleWorkflowResponse_ContactEntity () {

            Server.Application.WorkflowUserInteractionRequestContactEntity contactEntityRequest;

            contactEntityRequest = (Server.Application.WorkflowUserInteractionRequestContactEntity)WorkflowResponse.UserInteractionRequest;


            if (contactEntityRequest.Entity == null) {

                // NO ENTITY SPECIFIED FOR CONTACT - ERROR

                SetException ("Contact Entity Request has no Entity assigned to it to contact. Unable to continue.");

                return;

            }


            entity = new Client.Core.Entity.Entity (MercuryApplication, contactEntityRequest.Entity);

            entityContactModel = new Controls.EntityContactModel (contactEntityRequest.Entity, contactEntityRequest.RelatedEntity);

            entityContactModel.StoreModelStateInMemory = StoreModelStateInMemory;

            entityContactModel.AllowEditContactDateTime = contactEntityRequest.AllowEditContactDateTime;

            entityContactModel.AllowCancel = contactEntityRequest.AllowCancel;

            entityContactModel.AllowEditRegarding = contactEntityRequest.AllowEditRegarding;

            entityContactModel.AllowEditRelatedEntity = contactEntityRequest.AllowEditRelatedEntity;

            entityContactModel.Attempt = contactEntityRequest.Attempt;

            entityContactModel.IntroductionScript = contactEntityRequest.IntroductionScript;

            entityContactModel.ContactRegarding = contactEntityRequest.Regarding;


            switch (entity.EntityType) {

                case Server.Application.EntityType.Member:

                    WorkflowControlResponseScript = "WorkQueueItemInformation_SetMember ('" + Member.Id + "');";

                    break;

            }



            workflowControl = "/Views/Workflow/WorkflowControls/ContactEntity.cshtml";

            //contactEntityControl.ResponseScript = ResponseScriptWorkflowContinue;

            return;

        }

        protected void HandleWorkflowResponse_RequireForm () {

            Server.Application.WorkflowUserInteractionRequestRequireForm requireFormRequest;

            requireFormRequest = (Server.Application.WorkflowUserInteractionRequestRequireForm) WorkflowResponse.UserInteractionRequest;


            if (requireFormRequest.Form == null) {

                // NO FORM SPECIFIED FOR CONTACT - ERROR

                SetException ("Require Form has no Form assigned to it. Unable to continue.");

                return;

            }


            workflowControl = "/Views/Workflow/WorkflowControls/RequireForm.cshtml";

            return;

        }

        #endregion 

    }

}