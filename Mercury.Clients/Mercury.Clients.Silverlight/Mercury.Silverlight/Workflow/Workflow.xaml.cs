using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Mercury.Silverlight.Workflow {

    public partial class Workflow : WindowManager.Window {
        
        #region Private Properties

        private Client.Application MercuryApplication = ((App) Application.Current).MercuryApplication;

        private WindowManager.WindowManager WindowManager = ((App) Application.Current).WindowManager;


        private Server.Application.WorkQueueItem workQueueItem = null;

        private Server.Application.Workflow workflow = null;

        private Boolean isWorkflowCompleted = false;

        private Server.Application.WorkflowResponse lastWorkflowResponse = null;

        #endregion         
        

        #region Public Properties

        public override string WindowType { get { return "Workflow.Workflow"; } }

        public override Dictionary<String, Object> Parameters {

            get { return base.Parameters; }

            set {

                base.Parameters = value;

                if (Parameters.ContainsKey ("WorkQueueItemId")) {

                    // LOAD WORK QUEUE ITEM

                    GlobalProgressBarShow ();

                    MercuryApplication.WorkQueueItemGet (Convert.ToInt64 (Parameters["WorkQueueItemId"]), InitializeWorkQueueItem);

                }

                else if (Parameters.ContainsKey ("WorkflowName")) {

                    // LOAD WORKFLOW THROUGH WORKFLOW NAME, NOT LINKED TO WORK QUEUE ITEM

                    title = "Workflow: Loading Workflow.";

                    WindowTitle.Text = Title;

                    GlobalProgressBarShow ();

                    MercuryApplication.WorkflowGetByName (Convert.ToString (Parameters["WorkflowName"]), true, InitializeWorkflowByName);

                }

                else {

                    title = "Workflow: Unable to find referenced Work Queue Item Id or Workflow Name (from Parameters).";

                    WindowTitle.Text = Title;

                }

            }

        }

        #endregion 


        #region Constructors

        public Workflow () {

            InitializeComponent ();

            title = "Workflow: Loading Work Queue Item";

            WindowTitle.Text = Title;

            return;

        }

        #endregion 


        #region Window Events

        private void WindowClose_Click (object sender, RoutedEventArgs e) {

            if (isWorkflowCompleted) { WindowCommand_Close (); }

            WorkflowCloseConfirmationPopup_Open ();

            return;

        }

        private void WindowMinimize_Click (object sender, RoutedEventArgs e) {

            WindowCommand_Minimize ();

            return;

        }

        #endregion 


        #region UI Updates and Events

        private void SetWorkflowInformationMessage (String message) {

            WorkflowLastMessageContainer.Visibility = (!String.IsNullOrEmpty (message)) ? Visibility.Visible : Visibility.Collapsed;

            WorkflowLastMessage.Text = message;

            return;

        }

        private void SetWorkflowExceptionMessage (String message, Boolean allowClose) {

            WorkflowExceptionMessageContainer.Visibility = (!String.IsNullOrEmpty (message)) ? Visibility.Visible : Visibility.Collapsed;

            WorkflowExceptionMessage.Text = message;

            isWorkflowCompleted = allowClose;

            WindowClose.Visibility = ((!String.IsNullOrEmpty (message)) && (allowClose)) ? Visibility.Visible : WindowClose.Visibility;

            return;

        }

        private void WorkflowContent_MouseWheel (object sender, MouseWheelEventArgs e) {

            WorkflowContent.ScrollToVerticalOffset (WorkflowContent.VerticalOffset - e.Delta);

            return;

        }

        #endregion


        #region Popup Windows

        private void WorkflowCloseConfirmationPopup_SizeChanged (Object sender, SizeChangedEventArgs e) {

            WorkflowCloseConfirmationContainer.Width = this.ActualWidth;

            WorkflowCloseConfirmationContainer.Height = this.ActualHeight;

            return;

        }

        private void WorkflowCloseConfirmationPopup_Open () {

            this.SizeChanged += new SizeChangedEventHandler (WorkflowCloseConfirmationPopup_SizeChanged);

            WorkflowCloseConfirmationPopup_SizeChanged (null, null);

            WorkflowCloseConfirmationPopup.IsOpen = true;

            return;

        }

        private void WorkflowCloseConfirmationPopup_Close () {

            this.SizeChanged -= new SizeChangedEventHandler (WorkflowCloseConfirmationPopup_SizeChanged);

            WorkflowCloseConfirmationPopup.IsOpen = false;

            return;

        }

        private void WorkflowCloseConfirmationCloseCancel_Click (object sender, RoutedEventArgs e) {

            WorkflowCloseConfirmationPopup_Close ();

            return;

        }

        private void WorkflowCloseConfirmationOk_Click (object sender, RoutedEventArgs e) {

            WorkflowCloseConfirmationPopup_Close ();

            WindowCommand_Close ();

            return;

        }

        #endregion 

        
        #region Initializations

        private void InitializeEntityInformation (Int64 entityId) {

            MercuryApplication.EntityGet (entityId, true, InitializeEntityInformation_EntityGetCompleted);
            
            return;

        }

        private void InitializeEntityInformation_EntityGetCompleted (Object sender, Server.Application.EntityGetCompletedEventArgs e) {

            if (SetExceptionMessage (e)) { return; }

            if (e.Result == null) { return; }
            
            Client.Core.Entity.Entity entity = new Client.Core.Entity.Entity (MercuryApplication, e.Result);


            switch (entity.EntityType) {

                case Mercury.Server.Application.EntityType.Member:

                    WorkflowInformationMember.Visibility = System.Windows.Visibility.Visible;

                    WorkflowInformationProvider.Visibility = System.Windows.Visibility.Collapsed;

                    //InitializeMemberInformationByEntityId (entityId);

                    break;

                case Mercury.Server.Application.EntityType.Provider:

                    WorkflowInformationMember.Visibility = System.Windows.Visibility.Collapsed;

                    WorkflowInformationProvider.Visibility = System.Windows.Visibility.Visible;

                    //Client.Core.Provider.Provider provider = MercuryApplication.ProviderGetByEntityId (entityId, true);

                    //if (provider != null) { InitializeProviderInformation (provider.Id); }

                    break;

            }

            return;

        }

        private void InitializeEntityInformationFromWorkQueueItem (Int64 workQueueItemId) {

            MercuryApplication.WorkQueueItemGet (workQueueItemId, InitializeEntityInformationFromWorkQueueItem_GetWorkQueueItemCompleted);

            return;

        }

        private void InitializeEntityInformationFromWorkQueueItem_GetWorkQueueItemCompleted (Object sender, Server.Application.WorkQueueItemGetCompletedEventArgs e) {

            if (SetExceptionMessage (e)) { return; }

            if (e.Result == null) { return; }

            Client.Core.Work.WorkQueueItem workQueueItem = new Client.Core.Work.WorkQueueItem (MercuryApplication, e.Result);

            switch (workQueueItem.ItemObjectType) {

                case "Member": InitializeMemberInformation (workQueueItem.ItemObjectId); break;

            }

            return;

        }


        private void InitializeMemberInformation (Client.Core.Member.Member member) {

            WorkflowInformationMember.Visibility = System.Windows.Visibility.Visible;


            #region Note Alert Icons

            //Dictionary<Mercury.Server.Application.NoteImportance, Client.Core.Entity.EntityNote> entityNotes;

            //entityNotes = MercuryApplication.EntityNoteGetMostRecentByAllImportances (member.EntityId, true);


            //Client.Core.Entity.EntityNote entityNote = null;

            //// entityNote = MercuryApplication.EntityNoteGetMostRecentByImportance (member.EntityId, Mercury.Server.Application.NoteImportance.Warning, false);

            //if (entityNotes.ContainsKey (Mercury.Server.Application.NoteImportance.Warning)) { entityNote = entityNotes[Mercury.Server.Application.NoteImportance.Warning]; }

            //if (entityNote != null) {

            //    if (entityNote.TerminationDate >= DateTime.Today) {

            //        UserInteractionEntityInformationMemberNoteWarning.Style.Clear ();

            //        UserInteractionEntityInformationMemberNoteWarning.Attributes.Add ("title", "[" + entityNote.NoteTypeName + "] " + entityNote.Subject);

            //        UserInteractionEntityInformationMemberNoteWarning.Visible = true;

            //    }

            //}

            //// entityNote = MercuryApplication.EntityNoteGetMostRecentByImportance (member.EntityId, Mercury.Server.Application.NoteImportance.Critical, false);

            //entityNote = null;

            //if (entityNotes.ContainsKey (Mercury.Server.Application.NoteImportance.Critical)) { entityNote = entityNotes[Mercury.Server.Application.NoteImportance.Critical]; }

            //if (entityNote != null) {

            //    if (entityNote.TerminationDate >= DateTime.Today) {

            //        UserInteractionEntityInformationMemberNoteCritical.Style.Clear ();

            //        UserInteractionEntityInformationMemberNoteCritical.Attributes.Add ("title", "[" + entityNote.NoteTypeName + "] " + entityNote.Subject);

            //        UserInteractionEntityInformationMemberNoteCritical.Visible = true;

            //    }

            //}

            #endregion


            WorkflowInformationMemberName.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("Name", member, System.Windows.Data.BindingMode.OneWay));

            WorkflowInformationMemberBirthDate.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("BirthDateDescription", member, System.Windows.Data.BindingMode.OneWay));

            WorkflowInformationMemberAge.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("CurrentAgeDescription", member, System.Windows.Data.BindingMode.OneWay));

            WorkflowInformationMemberGender.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("GenderDescription", member, System.Windows.Data.BindingMode.OneWay));

            WorkflowInformationMemberProgramName.Text = "** Not Enrolled";

            WorkflowInformationMemberProgramMemberId.Text = "**Not Enrolled";

            //if (member.HasCurrentEnrollment) {

            //    UserInteractionEntityInformationMemberProgram.Text = member.CurrentEnrollment.Program.Name;

            //    UserInteractionEntityInformationMemberProgramMemberId.Text = member.CurrentEnrollment.ProgramMemberId;

            //}

            return;

        }

        private void InitializeMemberInformationByEntityId (Int64 entityId) {

            MercuryApplication.MemberGetDemographicsByEntityId (entityId, true, InitializeMemberInformation_MemberGetDemographicsByEntityIdCompleted);

            return;

        }

        private void InitializeMemberInformation_MemberGetDemographicsByEntityIdCompleted (Object sender, Server.Application.MemberGetDemographicsByEntityIdCompletedEventArgs e) {

            if (SetExceptionMessage (e)) { return; }

            if (e.Result.Member == null) { return; }

            Client.Core.Member.Member member = new Client.Core.Member.Member (MercuryApplication, e.Result.Member);

            InitializeMemberInformation (member);

            return;

        }

        private void InitializeMemberInformation (Int64 memberId) {

            MercuryApplication.MemberGetDemographics (memberId, true, InitializeMemberInformation_MemberGetDemographicsCompleted);

            return;

        }

        private void InitializeMemberInformation_MemberGetDemographicsCompleted (Object sender, Server.Application.MemberGetDemographicsCompletedEventArgs e) {

            if (SetExceptionMessage (e)) { return; }

            if (e.Result.Member == null) { return; }

            Client.Core.Member.Member member = new Client.Core.Member.Member (MercuryApplication, e.Result.Member);

            InitializeMemberInformation (member);

            return;

        }



        //private void InitializeProviderInformation (Int64 providerId) {

        //    Client.Core.Provider.Provider provider = MercuryApplication.ProviderGet (providerId, true);

        //    if (provider == null) { return; }


        //    WorkQueueItemInformationProvider.Style.Clear ();


        //    #region Note Alert Icons

        //    Dictionary<Mercury.Server.Application.NoteImportance, Client.Core.Entity.EntityNote> entityNotes;

        //    entityNotes = MercuryApplication.EntityNoteGetMostRecentByAllImportances (provider.EntityId, true);


        //    Client.Core.Entity.EntityNote entityNote = null;

        //    // entityNote = MercuryApplication.EntityNoteGetMostRecentByImportance (provider.EntityId, Mercury.Server.Application.NoteImportance.Warning, false);

        //    if (entityNotes.ContainsKey (Mercury.Server.Application.NoteImportance.Warning)) { entityNote = entityNotes[Mercury.Server.Application.NoteImportance.Warning]; }

        //    if (entityNote != null) {

        //        if (entityNote.TerminationDate >= DateTime.Today) {

        //            UserInteractionEntityInformationProviderNoteWarning.Style.Clear ();

        //            UserInteractionEntityInformationProviderNoteWarning.Attributes.Add ("title", "[" + entityNote.NoteTypeName + "] " + entityNote.Subject);

        //            UserInteractionEntityInformationProviderNoteWarning.Visible = true;

        //        }

        //    }

        //    // entityNote = MercuryApplication.EntityNoteGetMostRecentByImportance (provider.EntityId, Mercury.Server.Application.NoteImportance.Critical, false);

        //    entityNote = null;

        //    if (entityNotes.ContainsKey (Mercury.Server.Application.NoteImportance.Critical)) { entityNote = entityNotes[Mercury.Server.Application.NoteImportance.Critical]; }

        //    if (entityNote != null) {

        //        if (entityNote.TerminationDate >= DateTime.Today) {

        //            UserInteractionEntityInformationProviderNoteCritical.Style.Clear ();

        //            UserInteractionEntityInformationProviderNoteCritical.Attributes.Add ("title", "[" + entityNote.NoteTypeName + "] " + entityNote.Subject);

        //            UserInteractionEntityInformationProviderNoteCritical.Visible = true;

        //        }

        //    }

        //    #endregion


        //    UserInteractionEntityInformationProviderName.Text = Web.CommonFunctions.ProviderProfileAnchor (providerId, provider.Name);

        //    UserInteractionEntityInformationProviderNpi.Text = (String.IsNullOrEmpty (provider.NationalProviderId)) ? "** Not Assigned" : provider.NationalProviderId;

        //    UserInteractionEntityInformationProviderProgram.Text = "** Not Enrolled";

        //    UserInteractionEntityInformationProviderProgramProviderId.Text = "**Not Enrolled";

        //    if (provider.HasCurrentEnrollment) {

        //        UserInteractionEntityInformationProviderProgram.Text = provider.CurrentEnrollment.Program.Name;

        //        UserInteractionEntityInformationProviderProgramProviderId.Text = provider.CurrentEnrollment.ProgramProviderId;

        //    }

        //    return;

        //}

        private void InitializeEntityObjectInformation (Server.Application.EntityType entityType, Int64 entityObjectId) {

            switch (entityType) {

                case Mercury.Server.Application.EntityType.Member: InitializeMemberInformation (entityObjectId); break;

                // case Mercury.Server.Application.EntityType.Provider: InitializeProviderInformation (entityObjectId); break;

                default: SetExceptionMessage ("[Workflow:InitializeEntityObjectInformation] Not Implemented: " + entityType.ToString ()); break;

            }

            return;

        }

        #endregion 


        #region Workflow

        private void InitializeWorkQueueItem (Object sender, Server.Application.WorkQueueItemGetCompletedEventArgs e) {

            GlobalProgressBarHide ();

            workQueueItem = e.Result;

            if (workQueueItem != null) {

                if (workQueueItem.CompletionDate.HasValue) {

                    SetWorkflowExceptionMessage ("This Work Queue Item has already been closed.", true);

                    return;

                }

                WindowTitle.Text = "Workflow: Loading Workflow";

                GlobalProgressBarShow ();

                MercuryApplication.WorkflowGetByWorkQueueId (workQueueItem.WorkQueueId, InitializeWorkflowByWorkQueueId);

            }

            else {

                WindowTitle.Text = "Workflow Exception: Unable to retreive Work Queue Item for Id (" + Parameters["WorkQueueItemId"] + ").";

            }

            return;

        }

        private void InitializeWorkflowByWorkQueueId (Object sender, Server.Application.WorkflowGetByWorkQueueIdCompletedEventArgs e) {

            GlobalProgressBarHide ();

            workflow = e.Result;

            if (workflow != null) {

                WorkflowStart ();

            }

            else {

                WindowTitle.Text = "Workflow Exception: Unable to retreive Workflow (" + Parameters["WorkflowName"] + ").";

            }

            return;

        }

        private void InitializeWorkflowByName (Object sender, Server.Application.WorkflowGetByNameCompletedEventArgs e) {

            GlobalProgressBarHide ();

            workflow = e.Result;

            if (workflow != null) {

                WorkflowStart ();

            }

            else { 

                WindowTitle.Text = "Workflow Exception: Unable to retreive Workflow (" + Parameters["WorkflowName"] + ").";

            }

            return;

        }

        private void InitializeWorkflowNextItem (Object sender, Server.Application.WorkQueueItemGetCompletedEventArgs e) {

            if ((e.Cancelled) && (e.Error != null)) { return; }

            if (e.Result == null) { return; }


            WorkflowNextItem.Visibility = (e.Result.WorkQueueId == 0) ? Visibility.Collapsed : Visibility.Visible;

            WorkflowNextItem.Tag = e.Result.WorkQueueId;

            return;

        }


        private void WorkflowStart () {

            Server.Application.WorkflowStartRequest startRequest = new Mercury.Server.Application.WorkflowStartRequest ();

            Dictionary<String, Object> workflowArguments = new Dictionary<String, Object> ();


            title = "Workflow: " + workflow.Name + " (" + workQueueItem.Description + ")";

            WindowTitle.Text = Title;
            

            if (workQueueItem != null) {

                if ((workQueueItem.WorkflowInstanceId == null) || (workQueueItem.WorkflowInstanceId == Guid.Empty)) {

                    workflowArguments.Add (workQueueItem.ItemObjectType + "Id", workQueueItem.ItemObjectId);

                    workflowArguments.Add ("WorkQueueItemId", workQueueItem.Id.ToString ());


                    startRequest.WorkflowId = workflow.Id;

                    startRequest.WorkflowInstanceId = workQueueItem.WorkflowInstanceId;

                    startRequest.WorkflowName = workflow.Name;

                    startRequest.WorkQueueItemId = workQueueItem.Id;

                    startRequest.Arguments = workflowArguments;

                }

                else { // RESUME A WORKFLOW THAT HAS A SAVED STATE

                    WorkflowContinue (null);

                    return;

                }

            }

            else { // NOT LOADED THROUGH WORK QUEUE ITEM

                foreach (String currentParameterName in Parameters.Keys) {

                    workflowArguments.Add (currentParameterName, Parameters[currentParameterName]);

                }

                startRequest.WorkflowId = workflow.Id;

                startRequest.WorkflowInstanceId = Guid.Empty;

                startRequest.WorkflowName = workflow.Name;

                startRequest.WorkQueueItemId = 0;

                startRequest.Arguments = workflowArguments;

            }

            
            GlobalProgressBarShow ();

            SetWorkflowInformationMessage ("Start Workflow: Sending Communication to Server.");

            MercuryApplication.WorkflowStart (startRequest, WorkflowStartCompleted);

            return;

        }
        
        private void WorkflowStartCompleted (Object sender, Server.Application.WorkflowStartCompletedEventArgs e) {

            GlobalProgressBarHide ();

            workQueueItem.WorkflowInstanceId = e.Result.WorkflowInstanceId;

            HandleWorkflowResponse (e.Result);

            return;

        }

        private void WorkflowContinue (Server.Application.WorkflowUserInteractionResponseBase response) {

            WorkflowContent.Content = null;

            GlobalProgressBarShow ();

            SetWorkflowInformationMessage ("Continuing Workflow: Sending Communication to Server.");

            MercuryApplication.WorkflowContinue (workflow.Name, workQueueItem.WorkflowInstanceId, response, WorkflowContinueCompleted);

            return;

        }

        private void WorkflowContinueCompleted (Object sender, Server.Application.WorkflowContinueCompletedEventArgs e) {

            GlobalProgressBarHide ();

            HandleWorkflowResponse (e.Result);

            return;

        }

        private void WorkflowNextItem_Click (object sender, RoutedEventArgs e) {

            if (MercuryApplication == null) { return; }


            Int64 workQueueId = Convert.ToInt64 (((FrameworkElement)sender).Tag);

            MercuryApplication.WorkQueueGetWork (workQueueId, WorkflowNextItem_GetWorkCompleted);


            return;

        }

        private void WorkflowNextItem_GetWorkCompleted (Object sender, Server.Application.WorkQueueGetWorkCompletedEventArgs e) {

            if (SetExceptionMessage (e)) { return; }


            Mercury.Server.Application.GetWorkResponse response = e.Result;


            if (response.WorkQueueItem == null) {

                // NO WORK QUEUE ITEM AVAILABLE

                SetWorkflowInformationMessage ("No Work Queue Items Available in the selected Work Queue.");

                WorkflowNextItem.Visibility = Visibility.Collapsed;

            }

            else {

                // VALID WORK QUEUE ITEM FOUND AND RETURNED

                if (response.Workflow != null) {

                    // KICK-OFF WORKFLOW PROCESS

                    Dictionary<String, Object> windowParameters = new Dictionary<String, Object> ();

                    windowParameters.Add ("WorkQueueItemId", response.WorkQueueItem.Id);

                    WindowManager.OpenWindow ("Workflow.Workflow", windowParameters);

                    WindowCommand_Close (); 

                }

            }

            return;

        }


        private void HandleWorkflowResponse (Server.Application.WorkflowResponse response) {

            lastWorkflowResponse = response;

            WorkflowContent.Content = null;

            

            // RESET ALL HEADER INFORMATION 

            WorkflowInformationMember.Visibility = Visibility.Collapsed;

            WorkflowInformationProvider.Visibility = Visibility.Collapsed;

            SetWorkflowInformationMessage (String.Empty);

            SetWorkflowExceptionMessage (String.Empty, false);



            if (response.HasException) {

                SetWorkflowExceptionMessage (response.Exception.Message, true);

                // TODO: WRITE OUT FULL STACK TRACE


                if (response.WorkflowSteps != null) {

                    if (response.WorkflowSteps.Count > 0) {

                        HandleWorkflowResponse_WorkflowSummary (response);

                    }

                }

                return;

            }

            try {

                if ((response.WorkflowStatus == Mercury.Server.Application.WorkflowStatus.Unloaded)

                    && (response.UserInteractionRequest != null)) {

                    
                    WorkflowActionMessage.Text = "[" + Mercury.Server.CommonFunctions.EnumerationToString (response.UserInteractionRequest.InteractionType) + "] ";

                    WorkflowActionMessage.Text += response.UserInteractionRequest.Message;

                    // SetWorkflowInformationMessage (response.UserInteractionRequest.Message);

                    switch (response.UserInteractionRequest.InteractionType) {

                        case Mercury.Server.Application.UserInteractionType.ContactEntity: HandleWorkflowResponse_ContactEntity (response); break;

                        case Mercury.Server.Application.UserInteractionType.RequireForm: HandleWorkflowResponse_RequireForm (response); break;

                        case Mercury.Server.Application.UserInteractionType.SendCorrespondence: HandleWorkflowResponse_SendCorrespondence (response); break;

                        case Server.Application.UserInteractionType.Prompt: HandleWorkflowResponse_PromptUser (response); break;

                        default:

                            SetWorkflowExceptionMessage ("Unknown and unhandled User Interaction Request Type: " + response.UserInteractionRequest.InteractionType.ToString () + ".", true);

                            break;

                    }

                }

                else {

                    switch (response.WorkflowStatus) {

                        case Mercury.Server.Application.WorkflowStatus.Completed:

                            WorkflowActionMessage.Text = "Workflow has completed.";

                            WorkflowIcon.Source = new System.Windows.Media.Imaging.BitmapImage (new Uri ("../Images/Common16/Ok.png", UriKind.Relative));

                            break;

                        case Mercury.Server.Application.WorkflowStatus.Suspended:

                            WorkflowActionMessage.Text = "Workflow has been suspended.";

                            WorkflowIcon.Source = new System.Windows.Media.Imaging.BitmapImage (new Uri ("../Images/Common16/Suspend.png", UriKind.Relative));

                            break;

                        case Mercury.Server.Application.WorkflowStatus.Terminated:

                            WorkflowActionMessage.Text = "Workflow has been terminated.";

                            WorkflowIcon.Source = new System.Windows.Media.Imaging.BitmapImage (new Uri ("../Images/Common16/Critical.png", UriKind.Relative));

                            break;

                        default:

                            throw new ArgumentException ("Unable to handle workflow status: " + response.WorkflowStatus.ToString ());

                    }

                    HandleWorkflowResponse_WorkflowSummary (response);


                    System.Collections.ObjectModel.ObservableCollection<Mercury.Server.Application.WorkflowStep> workflowSteps = response.WorkflowSteps;

                    if (workflowSteps == null) { workflowSteps = new System.Collections.ObjectModel.ObservableCollection<Server.Application.WorkflowStep> (); }


                    #region Append Warnings and Errors

                    Boolean completionHasWarnings = false;

                    Boolean completionHasCriticals = false;


                    foreach (Mercury.Server.Application.WorkflowStep currentWorkflow in workflowSteps) {

                        completionHasWarnings |= (currentWorkflow.StepStatus == Mercury.Server.Application.WorkflowStepStatus.Warning);

                        completionHasCriticals |= (currentWorkflow.StepStatus == Mercury.Server.Application.WorkflowStepStatus.Critical);

                    }


                    if (completionHasWarnings) {

                        WorkflowActionMessage.Text += " (with warnings)";

                        WorkflowIcon.Source = new System.Windows.Media.Imaging.BitmapImage (new Uri ("../Images/Common16/Warning.png", UriKind.Relative));

                    }

                    if (completionHasCriticals) {

                        WorkflowActionMessage.Text += " (with errors)";

                        WorkflowIcon.Source = new System.Windows.Media.Imaging.BitmapImage (new Uri ("../Images/Common16/Critical.png", UriKind.Relative));

                    }

                    #endregion


                    #region Append Last Step Message

                    if (workflowSteps != null) {

                        if (workflowSteps.Count > 0) {

                            String lastMessageIconSource = "../Images/Common16/" + workflowSteps[workflowSteps.Count - 1].StepStatus.ToString () + ".png";

                            WorkflowLastMessageIcon.Source = new System.Windows.Media.Imaging.BitmapImage (new Uri (lastMessageIconSource, UriKind.Relative));

                            SetWorkflowInformationMessage (workflowSteps[workflowSteps.Count - 1].Description);

                        }

                    }

                    #endregion 



                    isWorkflowCompleted = true;

                    MercuryApplication.WorkQueueItemGet (response.WorkQueueItemId, InitializeWorkflowNextItem);                    

                }

            }

            catch (Exception responseHandlerException) {

                isWorkflowCompleted = true;
                            
                SetWorkflowExceptionMessage ("(Response Handler Exception) " + responseHandlerException.Message, true);

                // TODO: WRITE OUT FULL STACK TRACE

                return;
            
            }

            return;

        }

        private void HandleWorkflowResponse_ContactEntity (Server.Application.WorkflowResponse response) {

            Server.Application.WorkflowUserInteractionRequestContactEntity contactEntityInteraction;

            contactEntityInteraction = (Server.Application.WorkflowUserInteractionRequestContactEntity) response.UserInteractionRequest;

            Controls.EntityContact contactEntityControl = new Controls.EntityContact ();

            WorkflowContent.Content = contactEntityControl;


            // SET CONTROL PROPERTIES 

            contactEntityControl.EntityId = contactEntityInteraction.Entity.Id;

            contactEntityControl.AllowEditContactDateTime = false;

            contactEntityControl.AllowEditRegarding = contactEntityInteraction.AllowEditRegarding;

            contactEntityControl.AllowCustomRegardingText = true;

            contactEntityControl.RegardingMessage = contactEntityInteraction.Regarding;

            contactEntityControl.IntroductionScript = contactEntityInteraction.IntroductionScript;

            contactEntityControl.Contact += new EventHandler<Mercury.Silverlight.Controls.EntityContactEventArgs> (EntityContact_OnContact);


            // SET WORKFLOW HEADER

            Mercury.Server.Application.EntityType entityType = (contactEntityInteraction.Entity != null) ? contactEntityInteraction.Entity.EntityType : contactEntityInteraction.Entity.EntityType;

            switch (entityType) {

                case Mercury.Server.Application.EntityType.Member: InitializeMemberInformationByEntityId (contactEntityInteraction.Entity.Id); break;

                default: InitializeEntityInformation (contactEntityInteraction.Entity.Id); break;

            }
            

            return;

        }

        private void EntityContact_OnContact (Object sender, Silverlight.Controls.EntityContactEventArgs e) {

            Controls.EntityContact contactEntityControl = (Controls.EntityContact) sender;

            Server.Application.WorkflowUserInteractionResponseContactEntity response = new Mercury.Server.Application.WorkflowUserInteractionResponseContactEntity ();

            response.InteractionType = Mercury.Server.Application.UserInteractionType.ContactEntity;

            if ((contactEntityControl != null) && (!e.Cancel)) {

                response.EntityContact = e.EntityContact;

                response.Cancel = e.Cancel;

            }

            else { response.Cancel = true; }

            WorkflowContinue (response);

            return;

        }

        private void HandleWorkflowResponse_RequireForm (Server.Application.WorkflowResponse response) {

            Server.Application.WorkflowUserInteractionRequestRequireForm formRequest;

            formRequest = (Server.Application.WorkflowUserInteractionRequestRequireForm) response.UserInteractionRequest;

            UserInteractions.RequireForm requireFormControl = new Mercury.Silverlight.Workflow.UserInteractions.RequireForm ();

            WorkflowContent.Content = requireFormControl;


            // SET CONTROL PROPERTIES

            requireFormControl.Form = formRequest.Form;

            requireFormControl.AllowSaveAsDraft = formRequest.AllowSaveAsDraft;

            requireFormControl.ScrollToControl += new EventHandler<RoutedEventArgs> (ScrollToControl);

            requireFormControl.FormSubmit += new EventHandler<Mercury.Silverlight.Workflow.UserInteractions.RequireFormEventArgs> (RequireForm_OnSubmit);


            // SET WORKFLOW HEADER

            Mercury.Server.Application.EntityType entityType = (formRequest.Form != null) ? formRequest.Form.EntityType : formRequest.EntityType;

            switch (entityType) {

                case Mercury.Server.Application.EntityType.Member: InitializeMemberInformation (formRequest.EntityObjectId); break;                

            }
            
            return;

        }

        private void RequireForm_OnSubmit (Object sender, UserInteractions.RequireFormEventArgs e) {

            Server.Application.WorkflowUserInteractionResponseRequireForm response = new Mercury.Server.Application.WorkflowUserInteractionResponseRequireForm ();

            response.InteractionType = Mercury.Server.Application.UserInteractionType.RequireForm;

            response.Form = e.Form;

            response.SaveAsDraft = e.SaveAsDraft;

            WorkflowContinue (response);

            return;

        }

        private void HandleWorkflowResponse_SendCorrespondence (Server.Application.WorkflowResponse response) {

            Server.Application.WorkflowUserInteractionRequestSendCorrespondence sendCorrespondenceInteraction;

            sendCorrespondenceInteraction = (Server.Application.WorkflowUserInteractionRequestSendCorrespondence) response.UserInteractionRequest;

            Controls.EntitySendCorrespondence sendCorrespondenceControl = new Controls.EntitySendCorrespondence ();

            WorkflowContent.Content = sendCorrespondenceControl;


            // SET CONTROL PROPERTIES 

            sendCorrespondenceControl.EntityId = sendCorrespondenceInteraction.Entity.Id;

            sendCorrespondenceControl.CorrespondenceId = sendCorrespondenceInteraction.CorrespondenceId;


            sendCorrespondenceControl.AllowAlternateAddress = sendCorrespondenceInteraction.AllowAlternateAddress;

            if (sendCorrespondenceInteraction.AlternateAddress != null) {

                sendCorrespondenceControl.AlternateAddress = new Mercury.Client.Core.Entity.EntityAddress (MercuryApplication, sendCorrespondenceInteraction.AlternateAddress);

            }
            

            sendCorrespondenceControl.Attention = sendCorrespondenceInteraction.Attention;

            sendCorrespondenceControl.AllowCancel = sendCorrespondenceInteraction.AllowCancel;

            sendCorrespondenceControl.AllowUserSelection = sendCorrespondenceInteraction.AllowUserSelection;

            sendCorrespondenceControl.AllowFutureSendDate = sendCorrespondenceInteraction.AllowFutureSendDate;

            sendCorrespondenceControl.SendDate = sendCorrespondenceInteraction.SendDate;

            sendCorrespondenceControl.SendCorrespondence += new EventHandler<Mercury.Silverlight.Controls.EntitySendCorrespondenceEventArgs> (EntitySendCorrespondence_OnSendCorrespondence);

            return;

        }

        private void EntitySendCorrespondence_OnSendCorrespondence (Object sender, Silverlight.Controls.EntitySendCorrespondenceEventArgs e) {

            Controls.EntitySendCorrespondence sendCorrespondenceControl = (Controls.EntitySendCorrespondence) sender;

            Server.Application.WorkflowUserInteractionResponseSendCorrespondence response = new Mercury.Server.Application.WorkflowUserInteractionResponseSendCorrespondence ();

            response.InteractionType = Mercury.Server.Application.UserInteractionType.SendCorrespondence;

            if ((sendCorrespondenceControl != null) && (!e.Cancel)) {

                response.InteractionType = Mercury.Server.Application.UserInteractionType.SendCorrespondence;

                response.EntityCorrespondence = e.EntityCorrespondence;

                response.Cancel = e.Cancel;

                response.Send = (!e.Cancel);
                

            }

            else { response.Send = false; }

            WorkflowContinue (response);

            return;

        }

        private void HandleWorkflowResponse_PromptUser (Server.Application.WorkflowResponse response) {

            Server.Application.WorkflowUserInteractionRequestPromptUser promptRequest;

            promptRequest = (Server.Application.WorkflowUserInteractionRequestPromptUser)response.UserInteractionRequest;

            UserInteractions.PromptUser promptUserControl = new UserInteractions.PromptUser ();

            WorkflowContent.Content = promptUserControl;


            // SET CONTROL PROPERTIES

            promptUserControl.Completed += new EventHandler<UserInteractions.PromptUserResponseEventArgs> (PromptUser_OnCompleted);
            
            promptUserControl.InitializePrompt (promptRequest);


            // SET WORKFLOW HEADER

            // NOTHING TO SET

            return;

        }

        private void PromptUser_OnCompleted (Object sender, UserInteractions.PromptUserResponseEventArgs e) {

            UserInteractions.PromptUser promptUserControl = (UserInteractions.PromptUser)sender;

            WorkflowContinue (e.PromptResponse);

            return;


        }

        private void HandleWorkflowResponse_WorkflowSummary (Server.Application.WorkflowResponse response) {

            if (response.ChainWorkQueueItemId == 0) {

                WindowClose.Visibility = Visibility;

            }

            WorkflowControls.WorkflowSummary summary = new Mercury.Silverlight.Workflow.WorkflowControls.WorkflowSummary ();

            WorkflowContent.Content = summary;

            summary.InitializeSteps (response);

            return;

        }

        #endregion 


        #region User Interaction Events

        private void ScrollToControl (Object sender, RoutedEventArgs e) {

            if (sender is FrameworkElement) { 

                FrameworkElement referenceControl = (FrameworkElement) sender;

                if (sender != null) { 

                    GeneralTransform positionTransform = referenceControl.TransformToVisual (WorkflowContent);

                    Rect elementBounds = new Rect (positionTransform.Transform (new Point ()), positionTransform.Transform (new Point (referenceControl.ActualWidth, referenceControl.ActualHeight)));

                    Double verticalDelta = 0;

                    Double scrollToPosition = WorkflowContent.VerticalOffset;

                    if (WorkflowContent.ViewportHeight < elementBounds.Bottom) {

                        verticalDelta = elementBounds.Bottom - WorkflowContent.ViewportHeight;

                        scrollToPosition = scrollToPosition + verticalDelta;

                    }

                    if ((elementBounds.Top - verticalDelta) < 0) {

                        scrollToPosition = scrollToPosition - (verticalDelta - elementBounds.Top);

                    }

                    WorkflowContent.ScrollToVerticalOffset (scrollToPosition);

                }

            }

            return;

        }

        #endregion 


    }

}
