using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
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

    public partial class RequireForm : SequenceActivity {

        #region Dependency Properties

        public static readonly DependencyProperty ApplicationProperty = DependencyProperty.Register ("Application", typeof (Server.Application), typeof (RequireForm));

        public Server.Application Application { get { return (Server.Application) GetValue (ApplicationProperty); } set { SetValue (ApplicationProperty, value); } }

        public static readonly DependencyProperty WorkflowStepsProperty = DependencyProperty.Register ("WorkflowSteps", typeof (System.Collections.Generic.List<Server.Workflows.WorkflowStep>), typeof (RequireForm));

        public System.Collections.Generic.List<Server.Workflows.WorkflowStep> WorkflowSteps { get { return (System.Collections.Generic.List<Server.Workflows.WorkflowStep>) GetValue (WorkflowStepsProperty); } set { SetValue (WorkflowStepsProperty, value); } }


        protected static readonly DependencyProperty ActivityCompletedProperty = DependencyProperty.Register ("ActivityCompleted", typeof (Boolean), typeof (RequireForm));

        protected Boolean ActivityCompleted { get { return (Boolean) GetValue (ActivityCompletedProperty); } set { SetValue (ActivityCompletedProperty, value); } }

        public static readonly DependencyProperty ActivityCanceledProperty = DependencyProperty.Register ("ActivityCanceled", typeof (Boolean), typeof (RequireForm));

        public Boolean ActivityCanceled { get { return (Boolean) GetValue (ActivityCanceledProperty); } set { SetValue (ActivityCanceledProperty, value); } }


        public static readonly DependencyProperty EntityTypeProperty = DependencyProperty.Register ("EntityType", typeof (Server.Core.Enumerations.EntityType), typeof (RequireForm));

        public Server.Core.Enumerations.EntityType EntityType { get { return (Mercury.Server.Core.Enumerations.EntityType) GetValue (EntityTypeProperty); } set { SetValue (EntityTypeProperty, value); } }


        public static readonly DependencyProperty EntityObjectIdProperty = DependencyProperty.Register ("EntityObjectId", typeof (Int64), typeof (RequireForm));

        public Int64 EntityObjectId { get { return (Int64) GetValue (EntityObjectIdProperty); } set { SetValue (EntityObjectIdProperty, value); } }


        public static readonly DependencyProperty FormNameProperty = DependencyProperty.Register ("FormName", typeof (String), typeof (RequireForm));

        public String FormName { get { return (String) GetValue (FormNameProperty); } set { SetValue (FormNameProperty, value); } }

        public static readonly DependencyProperty FormProperty = DependencyProperty.Register ("Form", typeof (Server.Core.Forms.Form), typeof (RequireForm));

        public Server.Core.Forms.Form Form { get { return (Server.Core.Forms.Form) GetValue (FormProperty); } set { SetValue (FormProperty, value); } }

        public static readonly DependencyProperty FormReceivedProperty = DependencyProperty.Register ("FormReceived", typeof (Boolean), typeof (RequireForm));

        public Boolean FormReceived { get { return (Boolean) GetValue (FormReceivedProperty); } set { SetValue (FormReceivedProperty, value); } }


        public static readonly DependencyProperty AllowSaveAsDraftProperty = DependencyProperty.Register ("AllowSaveAsDraft", typeof (Boolean), typeof (RequireForm));

        public Boolean AllowSaveAsDraft { get { return (Boolean) GetValue (AllowSaveAsDraftProperty); } set { SetValue (AllowSaveAsDraftProperty, value); } }

        protected static readonly DependencyProperty SaveAsDraftProperty = DependencyProperty.Register ("SaveAsDraft", typeof (Boolean), typeof (RequireForm));

        protected Boolean SaveAsDraft { get { return (Boolean) GetValue (SaveAsDraftProperty); } set { SetValue (SaveAsDraftProperty, value); } }

        public static readonly DependencyProperty AllowCancelProperty = DependencyProperty.Register ("AllowCancel", typeof (Boolean), typeof (RequireForm));

        public Boolean AllowCancel { get { return (Boolean) GetValue (AllowCancelProperty); } set { SetValue (AllowCancelProperty, value); } }

        public static readonly DependencyProperty WorkQueueItemProperty = DependencyProperty.Register ("WorkQueueItem", typeof (Server.Core.Work.WorkQueueItem), typeof (RequireForm));

        public Server.Core.Work.WorkQueueItem WorkQueueItem { get { return (Server.Core.Work.WorkQueueItem) GetValue (WorkQueueItemProperty); } set { SetValue (WorkQueueItemProperty, value); } }


        public static readonly DependencyProperty SuspendMessageProperty = DependencyProperty.Register ("SuspendMessage", typeof (String), typeof (RequireForm));

        public String SuspendMessage { get { return (String) GetValue (SuspendMessageProperty); } set { SetValue (SuspendMessageProperty, value); } }

        public static readonly DependencyProperty WorkQueueItemReleaseOnSuspendProperty = DependencyProperty.Register ("WorkQueueItemReleaseOnSuspend", typeof (Boolean), typeof (RequireForm));

        public Boolean WorkQueueItemReleaseOnSuspend { get { return (Boolean) GetValue (WorkQueueItemReleaseOnSuspendProperty); } set { SetValue (WorkQueueItemReleaseOnSuspendProperty, value); } }

        #endregion


        #region Event Parameters - Request/Response

        public UserInteractions.Request.RequestBase UserInteractionRequest = new Mercury.Server.Workflows.UserInteractions.Request.RequestBase ();

        public UserInteractions.Response.ResponseEventArgs UserInteractionResponseEventArgs;


        private UserInteractions.Response.ResponseBase UserInteractionResponse { get { return (UserInteractionResponseEventArgs == null) ? null : UserInteractionResponseEventArgs.Response; } }

        private UserInteractions.Enumerations.UserInteractionType UserInteractionResponseType { get { return (UserInteractionResponse == null) ? UserInteractions.Enumerations.UserInteractionType.NotSpecified : UserInteractionResponse.UserInteractionType; } }

        #endregion


        #region Constructors

        public RequireForm () {

            InitializeComponent ();

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

                if (WorkQueueItem != null) {

                    Application.WorkQueueItemWorkflowStepsSave (WorkQueueItem.Id, WorkflowSteps);

                }

            }

            return;

        }

        #endregion


        #region Require Form Process

        private void InitializeActivity_OnExecuteCode (object sender, EventArgs e) {

            FormReceived = false;

            ActivityCompleted = false;

            ActivityCanceled = false;

            return;

        }

        private void WhileWaitingForForm_OnEvaluation (Object Sender, ConditionalEventArgs eventArgs) {

            if (Application == null) { RaiseActivityException ("Invalid Application Reference Specified."); }

            if (EntityType == Mercury.Server.Core.Enumerations.EntityType.NotSpecified) { RaiseActivityException ("No Entity Type Specified."); }


            eventArgs.Result = !ActivityCompleted;

            return;

        }

        private void FormRequest_OnInvoking (object sender, EventArgs e) {

            Mercury.Server.Workflows.UserInteractions.Request.RequireFormRequest formRequest = new Mercury.Server.Workflows.UserInteractions.Request.RequireFormRequest (EntityType);

            if (Form == null) {

                formRequest.Form = new Mercury.Server.Core.Forms.Form (Application, FormName);

                if (formRequest.Form == null) { throw new ApplicationException ("Unable to load form for " + FormName + "."); }


                formRequest.Form.EntityType = EntityType;

                formRequest.Form.EntityObjectId = EntityObjectId;

            }

            else { formRequest.Form = Form; }


            formRequest.Form.FormType = Mercury.Server.Core.Forms.Enumerations.FormType.Instance;

            formRequest.EntityType = EntityType;

            formRequest.EntityObjectId = EntityObjectId;

            formRequest.Message = "Form Required: " + formRequest.Form.Name + " (Last Modified: " + formRequest.Form.ModifiedAccountInfo.ActionDate.ToString ("MM/dd/yyyy") + ")";

            formRequest.AllowSaveAsDraft = (AllowSaveAsDraft && (WorkQueueItem != null));

            formRequest.AllowCancel = AllowCancel;

            SuspendMessage = "Suspending at " + formRequest.Message;

            SaveAsDraft = false;

            UserInteractionRequest = formRequest;

            WorkflowStepsAdd (formRequest.Form.Name);

            return;

        }

        private void FormResponse_OnInvoking (object sender, ExternalDataEventArgs e) {

            if (Application == null) { RaiseActivityException ("Invalid Application Reference Specified."); }


            ActivityCompleted = false;

            ActivityCanceled = false;


            if (UserInteractionResponseEventArgs != null) {

                if (UserInteractionResponseEventArgs.Response != null) {

                    if (UserInteractionResponseEventArgs.Response.UserInteractionType == Mercury.Server.Workflows.UserInteractions.Enumerations.UserInteractionType.RequireForm) {

                        Server.Workflows.UserInteractions.Response.RequireFormResponse response = (Server.Workflows.UserInteractions.Response.RequireFormResponse) UserInteractionResponseEventArgs.Response;

                        if ((response.Form != null) && (!response.SaveAsDraft) && (!response.Cancel)) {

                            Form = response.Form;

                            Form.ResetForm (Application);

                            FormReceived = true;

                            ActivityCompleted = true;

                            WorkflowStepsAdd (Form.Name);

                        }

                        else if (response.SaveAsDraft) {

                            Form = response.Form;

                            Form.ResetForm (Application);

                        }

                        else if (response.Cancel) {

                            WorkflowStepsAdd ("Form Canceled.");

                            ActivityCompleted = true;

                            ActivityCanceled = true;

                        }

                        SaveAsDraft = response.SaveAsDraft;

                    }

                }

            }

            return;

        }

        #endregion


    }

}
