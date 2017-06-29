using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

using Server = Mercury.Server;
using UserInteractions = Mercury.Server.Workflows.UserInteractions;

namespace Mercury.Workflows.ActivityLibrary {

    public partial class PromptUser : SequenceActivity {

        #region Dependency Properties

        public static readonly DependencyProperty ApplicationProperty = DependencyProperty.Register ("Application", typeof (Server.Application), typeof (PromptUser));

        public Server.Application Application { get { return (Server.Application) GetValue (ApplicationProperty); } set { SetValue (ApplicationProperty, value); } }

        public static readonly DependencyProperty WorkflowStepsProperty = DependencyProperty.Register ("WorkflowSteps", typeof (System.Collections.Generic.List<Server.Workflows.WorkflowStep>), typeof (PromptUser));

        public System.Collections.Generic.List<Server.Workflows.WorkflowStep> WorkflowSteps { get { return (System.Collections.Generic.List<Server.Workflows.WorkflowStep>) GetValue (WorkflowStepsProperty); } set { SetValue (WorkflowStepsProperty, value); } }


        public static readonly DependencyProperty PromptTypeProperty = DependencyProperty.Register ("PromptType", typeof (Server.Workflows.UserInteractions.Enumerations.UserPromptType), typeof (PromptUser));

        public Server.Workflows.UserInteractions.Enumerations.UserPromptType PromptType { get { return (Mercury.Server.Workflows.UserInteractions.Enumerations.UserPromptType) GetValue (PromptTypeProperty); } set { SetValue (PromptTypeProperty, value); } }

        public static readonly DependencyProperty PromptImageProperty = DependencyProperty.Register ("PromptImage", typeof (Server.Workflows.UserInteractions.Enumerations.UserPromptImage), typeof (PromptUser));

        public Server.Workflows.UserInteractions.Enumerations.UserPromptImage PromptImage { get { return (Mercury.Server.Workflows.UserInteractions.Enumerations.UserPromptImage) GetValue (PromptImageProperty); } set { SetValue (PromptImageProperty, value); } }

        public static readonly DependencyProperty PromptTitleProperty = DependencyProperty.Register ("PromptTitle", typeof (String), typeof (PromptUser));

        public String PromptTitle { get { return (String) GetValue (PromptTitleProperty); } set { SetValue (PromptTitleProperty, value); } }

        public static readonly DependencyProperty PromptMessageProperty = DependencyProperty.Register ("PromptMessage", typeof (String), typeof (PromptUser));

        public String PromptMessage { get { return (String) GetValue (PromptMessageProperty); } set { SetValue (PromptMessageProperty, value); } }



        public static readonly DependencyProperty ResponseReceivedProperty = DependencyProperty.Register ("ResponseReceived", typeof (Boolean), typeof (PromptUser));

        public Boolean ResponseReceived { get { return (Boolean) GetValue (ResponseReceivedProperty); } set { SetValue (ResponseReceivedProperty, value); } }

        public static readonly DependencyProperty AllowCancelProperty = DependencyProperty.Register ("AllowCancel", typeof (Boolean), typeof (PromptUser));

        public Boolean AllowCancel { get { return (Boolean) GetValue (AllowCancelProperty); } set { SetValue (AllowCancelProperty, value); } }

        public static readonly DependencyProperty ButtonClickedProperty = DependencyProperty.Register ("ButtonClicked", typeof (Server.Workflows.UserInteractions.Enumerations.UserPromptButtonClicked), typeof (PromptUser));

        public Server.Workflows.UserInteractions.Enumerations.UserPromptButtonClicked ButtonClicked { get { return (Server.Workflows.UserInteractions.Enumerations.UserPromptButtonClicked) GetValue (ButtonClickedProperty); } set { SetValue (ButtonClickedProperty, value); } }


        public static readonly DependencyProperty InputTextProperty = DependencyProperty.Register ("InputText", typeof (String), typeof (PromptUser));

        public String InputText { get { return (String) GetValue (InputTextProperty); } set { SetValue (InputTextProperty, value); } }

        public static readonly DependencyProperty PromptSelectionItemsProperty = DependencyProperty.Register ("PromptSelectionItems", typeof (List<UserInteractions.Structures.PromptSelectionItem>), typeof (PromptUser));

        public List<UserInteractions.Structures.PromptSelectionItem> PromptSelectionItems { get { return (List<UserInteractions.Structures.PromptSelectionItem>) GetValue (PromptSelectionItemsProperty); } set { SetValue (PromptSelectionItemsProperty, value); } }

        public static readonly DependencyProperty SelectedValueProperty = DependencyProperty.Register ("SelectedValue", typeof (String), typeof (PromptUser));

        public String SelectedValue { get { return (String) GetValue (SelectedValueProperty); } set { SetValue (SelectedValueProperty, value); } }

        public static readonly DependencyProperty SelectedTextProperty = DependencyProperty.Register ("SelectedText", typeof (String), typeof (PromptUser));

        public String SelectedText { get { return (String) GetValue (SelectedTextProperty); } set { SetValue (SelectedTextProperty, value); } }



        public static readonly DependencyProperty SuccessProperty = DependencyProperty.Register ("Success", typeof (Boolean), typeof (PromptUser));

        public Boolean Success { get { return (Boolean) GetValue (SuccessProperty); } set { SetValue (SuccessProperty, value); } }

        public static readonly DependencyProperty WorkQueueItemIdProperty = DependencyProperty.Register ("WorkQueueItemId", typeof (Int64), typeof (PromptUser));

        public Int64 WorkQueueItemId { get { return (Int64) GetValue (WorkQueueItemIdProperty); } set { SetValue (WorkQueueItemIdProperty, value); } }

        #endregion


        #region Event Parameters - Request/Response

        public UserInteractions.Request.RequestBase UserInteractionRequest = new Mercury.Server.Workflows.UserInteractions.Request.RequestBase ();

        public UserInteractions.Response.ResponseEventArgs UserInteractionResponseEventArgs;


        private UserInteractions.Response.ResponseBase UserInteractionResponse { get { return (UserInteractionResponseEventArgs == null) ? null : UserInteractionResponseEventArgs.Response; } }

        private UserInteractions.Enumerations.UserInteractionType UserInteractionResponseType { get { return (UserInteractionResponse == null) ? UserInteractions.Enumerations.UserInteractionType.NotSpecified : UserInteractionResponse.UserInteractionType; } }

        #endregion


        #region Constructors

        public PromptUser () {

            InitializeComponent ();

            ResponseReceived = false;

            return;

        }

        #endregion


        #region Workflow Support Methods

        private void RaiseActivityException (String exceptionMessage) {

            String caller = (new System.Diagnostics.StackFrame (1)).GetMethod ().Name;

            WorkflowSteps.Add (new Mercury.Server.Workflows.WorkflowStep (Application, "Exception [" + caller + "]", exceptionMessage));

            throw new ApplicationException (this.GetType ().ToString () + ": " + exceptionMessage);

        }

        private void WorkflowStepsAdd (String stepDescription) {

            if (WorkflowSteps != null) {

                String stepName = (new System.Diagnostics.StackFrame (1)).GetMethod ().Name;

                Server.Workflows.WorkflowStep workflowStep = new Mercury.Server.Workflows.WorkflowStep (Application, stepName, stepDescription);

                WorkflowSteps.Add (workflowStep);

                if (WorkQueueItemId != 0) {

                    Application.WorkQueueItemWorkflowStepsSave (WorkQueueItemId, WorkflowSteps);

                }

            }

            return;

        }

        #endregion


        #region Workflow Steps

        private void InitializeActivity_OnCodeExecute (object sender, EventArgs e) {

            ResponseReceived = false;

            return;

        }

        private void WhileWaitingForResponse_OnEvaluation (object sender, ConditionalEventArgs eventArgs) {

            if (Application == null) { RaiseActivityException ("Invalid Application Reference Specified."); }

            eventArgs.Result = !ResponseReceived; // CONTINUE WHILE IF TRUE

            return;

        }

        private void UserPromptRequest_OnInvoking (Object sender, EventArgs eventArgs) {

            if (Application == null) { RaiseActivityException ("Invalid Application Reference Specified."); }


            Mercury.Server.Workflows.UserInteractions.Request.PromptUserRequest userPromptRequest;

            userPromptRequest = new Mercury.Server.Workflows.UserInteractions.Request.PromptUserRequest (

                PromptType,

                PromptImage,

                PromptTitle,

                PromptMessage);

            userPromptRequest.AllowCancel = AllowCancel;

            if (PromptSelectionItems != null) {

                userPromptRequest.SelectionItems = PromptSelectionItems;

            }

            WorkflowStepsAdd (PromptTitle);

            UserInteractionRequest = userPromptRequest;

            return;

        }

        private void UserPromptResponse_Invoked (Object sender, ExternalDataEventArgs eventArgs) {

            if (Application == null) { RaiseActivityException ("Invalid Application Reference Specified."); }


            if (UserInteractionResponseEventArgs != null) {

                if (UserInteractionResponseEventArgs.Response != null) {

                    if (UserInteractionResponseEventArgs.Response.UserInteractionType == Mercury.Server.Workflows.UserInteractions.Enumerations.UserInteractionType.Prompt) {

                        Mercury.Server.Workflows.UserInteractions.Response.PromptUserResponse response = (Mercury.Server.Workflows.UserInteractions.Response.PromptUserResponse) UserInteractionResponseEventArgs.Response;

                        if (response.ButtonClicked != Mercury.Server.Workflows.UserInteractions.Enumerations.UserPromptButtonClicked.None) {

                            ButtonClicked = response.ButtonClicked;

                            InputText = response.InputText;

                            SelectedValue = response.SelectedValue;

                            SelectedText = response.SelectedText;

                            ResponseReceived = true;

                            Success = true;

                            WorkflowStepsAdd ("Button Clicked: " + ButtonClicked.ToString () + "  |  Selected Text: " + SelectedText);

                        }

                    }

                }

            }

            return;

        }

        #endregion


    }

}
