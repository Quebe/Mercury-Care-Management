using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace Mercury.Workflows.ActivityLibrary
{
    public partial class RequireForm {
        #region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        private void InitializeComponent () {
            this.CanModifyActivities = true;
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind ();
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind ();
            System.Workflow.ComponentModel.ActivityBind activitybind3 = new System.Workflow.ComponentModel.ActivityBind ();
            System.Workflow.ComponentModel.ActivityBind activitybind4 = new System.Workflow.ComponentModel.ActivityBind ();
            System.Workflow.ComponentModel.ActivityBind activitybind5 = new System.Workflow.ComponentModel.ActivityBind ();
            System.Workflow.ComponentModel.ActivityBind activitybind6 = new System.Workflow.ComponentModel.ActivityBind ();
            System.Workflow.ComponentModel.ActivityBind activitybind7 = new System.Workflow.ComponentModel.ActivityBind ();
            System.Workflow.Activities.Rules.RuleConditionReference ruleconditionreference1 = new System.Workflow.Activities.Rules.RuleConditionReference ();
            System.Workflow.ComponentModel.ActivityBind activitybind8 = new System.Workflow.ComponentModel.ActivityBind ();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding1 = new System.Workflow.ComponentModel.WorkflowParameterBinding ();
            System.Workflow.ComponentModel.ActivityBind activitybind9 = new System.Workflow.ComponentModel.ActivityBind ();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding2 = new System.Workflow.ComponentModel.WorkflowParameterBinding ();
            System.Workflow.ComponentModel.ActivityBind activitybind10 = new System.Workflow.ComponentModel.ActivityBind ();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding3 = new System.Workflow.ComponentModel.WorkflowParameterBinding ();
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition ();
            this.SuspendSaveAsDraft = new Mercury.Workflows.ActivityLibrary.WorkQueueItemSuspend ();
            this.ifElseBranchActivity2 = new System.Workflow.Activities.IfElseBranchActivity ();
            this.IfSaveAsDraftAndAllowed = new System.Workflow.Activities.IfElseBranchActivity ();
            this.TimeoutException = new System.Workflow.Activities.DelayActivity ();
            this.EvaluateSaveAsDraft = new System.Workflow.Activities.IfElseActivity ();
            this.FormResponse = new System.Workflow.Activities.HandleExternalEventActivity ();
            this.ResponseTimeout = new System.Workflow.Activities.EventDrivenActivity ();
            this.ReceiveFormResponse = new System.Workflow.Activities.EventDrivenActivity ();
            this.ListenForResponse = new System.Workflow.Activities.ListenActivity ();
            this.FormRequest = new System.Workflow.Activities.CallExternalMethodActivity ();
            this.RequestResponseSequence = new System.Workflow.Activities.SequenceActivity ();
            this.WhileWaitingForForm = new System.Workflow.Activities.WhileActivity ();
            this.InitializeActivity = new System.Workflow.Activities.CodeActivity ();
            // 
            // SuspendSaveAsDraft
            // 
            activitybind1.Name = "RequireForm";
            activitybind1.Path = "Application";
            this.SuspendSaveAsDraft.ConstraintDays = 0;
            this.SuspendSaveAsDraft.MilestoneDays = 0;
            this.SuspendSaveAsDraft.Name = "SuspendSaveAsDraft";
            activitybind2.Name = "RequireForm";
            activitybind2.Path = "WorkQueueItemReleaseOnSuspend";
            this.SuspendSaveAsDraft.Success = false;
            activitybind3.Name = "RequireForm";
            activitybind3.Path = "SuspendMessage";
            activitybind4.Name = "RequireForm";
            activitybind4.Path = "FormName";
            activitybind5.Name = "RequireForm";
            activitybind5.Path = "FormName";
            activitybind6.Name = "RequireForm";
            activitybind6.Path = "WorkflowSteps";
            activitybind7.Name = "RequireForm";
            activitybind7.Path = "WorkQueueItem";
            this.SuspendSaveAsDraft.SetBinding (Mercury.Workflows.ActivityLibrary.WorkQueueItemSuspend.ApplicationProperty, ((System.Workflow.ComponentModel.ActivityBind) (activitybind1)));
            this.SuspendSaveAsDraft.SetBinding (Mercury.Workflows.ActivityLibrary.WorkQueueItemSuspend.SuspendMessageProperty, ((System.Workflow.ComponentModel.ActivityBind) (activitybind3)));
            this.SuspendSaveAsDraft.SetBinding (Mercury.Workflows.ActivityLibrary.WorkQueueItemSuspend.WorkflowLastStepProperty, ((System.Workflow.ComponentModel.ActivityBind) (activitybind4)));
            this.SuspendSaveAsDraft.SetBinding (Mercury.Workflows.ActivityLibrary.WorkQueueItemSuspend.WorkflowNextStepProperty, ((System.Workflow.ComponentModel.ActivityBind) (activitybind5)));
            this.SuspendSaveAsDraft.SetBinding (Mercury.Workflows.ActivityLibrary.WorkQueueItemSuspend.WorkflowStepsProperty, ((System.Workflow.ComponentModel.ActivityBind) (activitybind6)));
            this.SuspendSaveAsDraft.SetBinding (Mercury.Workflows.ActivityLibrary.WorkQueueItemSuspend.WorkQueueItemProperty, ((System.Workflow.ComponentModel.ActivityBind) (activitybind7)));
            this.SuspendSaveAsDraft.SetBinding (Mercury.Workflows.ActivityLibrary.WorkQueueItemSuspend.ReleaseItemProperty, ((System.Workflow.ComponentModel.ActivityBind) (activitybind2)));
            // 
            // ifElseBranchActivity2
            // 
            this.ifElseBranchActivity2.Name = "ifElseBranchActivity2";
            // 
            // IfSaveAsDraftAndAllowed
            // 
            this.IfSaveAsDraftAndAllowed.Activities.Add (this.SuspendSaveAsDraft);
            ruleconditionreference1.ConditionName = "SaveAsDraftAndAllowed";
            this.IfSaveAsDraftAndAllowed.Condition = ruleconditionreference1;
            this.IfSaveAsDraftAndAllowed.Name = "IfSaveAsDraftAndAllowed";
            // 
            // TimeoutException
            // 
            this.TimeoutException.Name = "TimeoutException";
            this.TimeoutException.TimeoutDuration = System.TimeSpan.Parse ("23:59:59");
            // 
            // EvaluateSaveAsDraft
            // 
            this.EvaluateSaveAsDraft.Activities.Add (this.IfSaveAsDraftAndAllowed);
            this.EvaluateSaveAsDraft.Activities.Add (this.ifElseBranchActivity2);
            this.EvaluateSaveAsDraft.Name = "EvaluateSaveAsDraft";
            // 
            // FormResponse
            // 
            this.FormResponse.EventName = "OnUserInteractionResponse";
            this.FormResponse.InterfaceType = typeof (Mercury.Server.Workflows.IWorkflowService);
            this.FormResponse.Name = "FormResponse";
            activitybind8.Name = "RequireForm";
            activitybind8.Path = "Application";
            workflowparameterbinding1.ParameterName = "sender";
            workflowparameterbinding1.SetBinding (System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind) (activitybind8)));
            activitybind9.Name = "RequireForm";
            activitybind9.Path = "UserInteractionResponseEventArgs";
            workflowparameterbinding2.ParameterName = "e";
            workflowparameterbinding2.SetBinding (System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind) (activitybind9)));
            this.FormResponse.ParameterBindings.Add (workflowparameterbinding1);
            this.FormResponse.ParameterBindings.Add (workflowparameterbinding2);
            this.FormResponse.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs> (this.FormResponse_OnInvoking);
            // 
            // ResponseTimeout
            // 
            this.ResponseTimeout.Activities.Add (this.TimeoutException);
            this.ResponseTimeout.Name = "ResponseTimeout";
            // 
            // ReceiveFormResponse
            // 
            this.ReceiveFormResponse.Activities.Add (this.FormResponse);
            this.ReceiveFormResponse.Activities.Add (this.EvaluateSaveAsDraft);
            this.ReceiveFormResponse.Name = "ReceiveFormResponse";
            // 
            // ListenForResponse
            // 
            this.ListenForResponse.Activities.Add (this.ReceiveFormResponse);
            this.ListenForResponse.Activities.Add (this.ResponseTimeout);
            this.ListenForResponse.Name = "ListenForResponse";
            // 
            // FormRequest
            // 
            this.FormRequest.InterfaceType = typeof (Mercury.Server.Workflows.IWorkflowService);
            this.FormRequest.MethodName = "UserInteractionRequest";
            this.FormRequest.Name = "FormRequest";
            activitybind10.Name = "RequireForm";
            activitybind10.Path = "UserInteractionRequest";
            workflowparameterbinding3.ParameterName = "request";
            workflowparameterbinding3.SetBinding (System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind) (activitybind10)));
            this.FormRequest.ParameterBindings.Add (workflowparameterbinding3);
            this.FormRequest.MethodInvoking += new System.EventHandler (this.FormRequest_OnInvoking);
            // 
            // RequestResponseSequence
            // 
            this.RequestResponseSequence.Activities.Add (this.FormRequest);
            this.RequestResponseSequence.Activities.Add (this.ListenForResponse);
            this.RequestResponseSequence.Name = "RequestResponseSequence";
            // 
            // WhileWaitingForForm
            // 
            this.WhileWaitingForForm.Activities.Add (this.RequestResponseSequence);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs> (this.WhileWaitingForForm_OnEvaluation);
            this.WhileWaitingForForm.Condition = codecondition1;
            this.WhileWaitingForForm.Name = "WhileWaitingForForm";
            // 
            // InitializeActivity
            // 
            this.InitializeActivity.Name = "InitializeActivity";
            this.InitializeActivity.ExecuteCode += new System.EventHandler (this.InitializeActivity_OnExecuteCode);
            // 
            // RequireForm
            // 
            this.Activities.Add (this.InitializeActivity);
            this.Activities.Add (this.WhileWaitingForForm);
            this.Name = "RequireForm";
            this.CanModifyActivities = false;

        }

        #endregion

        private WorkQueueItemSuspend SuspendSaveAsDraft;
        private IfElseBranchActivity ifElseBranchActivity2;
        private IfElseBranchActivity IfSaveAsDraftAndAllowed;
        private IfElseActivity EvaluateSaveAsDraft;
        private CodeActivity InitializeActivity;
        private SequenceActivity RequestResponseSequence;
        private WhileActivity WhileWaitingForForm;
        private EventDrivenActivity ResponseTimeout;
        private EventDrivenActivity ReceiveFormResponse;
        private ListenActivity ListenForResponse;
        private HandleExternalEventActivity FormResponse;
        private DelayActivity TimeoutException;
        private CallExternalMethodActivity FormRequest;







































    }
}
