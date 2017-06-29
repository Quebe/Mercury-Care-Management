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
    public partial class PromptUser {
        #region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        private void InitializeComponent () {
            this.CanModifyActivities = true;
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind ();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding1 = new System.Workflow.ComponentModel.WorkflowParameterBinding ();
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind ();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding2 = new System.Workflow.ComponentModel.WorkflowParameterBinding ();
            System.Workflow.ComponentModel.ActivityBind activitybind3 = new System.Workflow.ComponentModel.ActivityBind ();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding3 = new System.Workflow.ComponentModel.WorkflowParameterBinding ();
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition ();
            this.TimeoutException = new System.Workflow.Activities.DelayActivity ();
            this.PromptResponse = new System.Workflow.Activities.HandleExternalEventActivity ();
            this.ResponseTimeout = new System.Workflow.Activities.EventDrivenActivity ();
            this.ReceivedResponse = new System.Workflow.Activities.EventDrivenActivity ();
            this.ListenForPromptResponse = new System.Workflow.Activities.ListenActivity ();
            this.UserPromptRequest = new System.Workflow.Activities.CallExternalMethodActivity ();
            this.UserPromptRequestResponse = new System.Workflow.Activities.SequenceActivity ();
            this.WhileWaitingForResponse = new System.Workflow.Activities.WhileActivity ();
            this.InitializeActivity = new System.Workflow.Activities.CodeActivity ();
            // 
            // TimeoutException
            // 
            this.TimeoutException.Name = "TimeoutException";
            this.TimeoutException.TimeoutDuration = System.TimeSpan.Parse ("23:59:59");
            // 
            // PromptResponse
            // 
            this.PromptResponse.EventName = "OnUserInteractionResponse";
            this.PromptResponse.InterfaceType = typeof (Mercury.Server.Workflows.IWorkflowService);
            this.PromptResponse.Name = "PromptResponse";
            activitybind1.Name = "PromptUser";
            activitybind1.Path = "Application";
            workflowparameterbinding1.ParameterName = "sender";
            workflowparameterbinding1.SetBinding (System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind) (activitybind1)));
            activitybind2.Name = "PromptUser";
            activitybind2.Path = "UserInteractionResponseEventArgs";
            workflowparameterbinding2.ParameterName = "e";
            workflowparameterbinding2.SetBinding (System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind) (activitybind2)));
            this.PromptResponse.ParameterBindings.Add (workflowparameterbinding1);
            this.PromptResponse.ParameterBindings.Add (workflowparameterbinding2);
            this.PromptResponse.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs> (this.UserPromptResponse_Invoked);
            // 
            // ResponseTimeout
            // 
            this.ResponseTimeout.Activities.Add (this.TimeoutException);
            this.ResponseTimeout.Name = "ResponseTimeout";
            // 
            // ReceivedResponse
            // 
            this.ReceivedResponse.Activities.Add (this.PromptResponse);
            this.ReceivedResponse.Name = "ReceivedResponse";
            // 
            // ListenForPromptResponse
            // 
            this.ListenForPromptResponse.Activities.Add (this.ReceivedResponse);
            this.ListenForPromptResponse.Activities.Add (this.ResponseTimeout);
            this.ListenForPromptResponse.Name = "ListenForPromptResponse";
            // 
            // UserPromptRequest
            // 
            this.UserPromptRequest.InterfaceType = typeof (Mercury.Server.Workflows.IWorkflowService);
            this.UserPromptRequest.MethodName = "UserInteractionRequest";
            this.UserPromptRequest.Name = "UserPromptRequest";
            activitybind3.Name = "PromptUser";
            activitybind3.Path = "UserInteractionRequest";
            workflowparameterbinding3.ParameterName = "request";
            workflowparameterbinding3.SetBinding (System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind) (activitybind3)));
            this.UserPromptRequest.ParameterBindings.Add (workflowparameterbinding3);
            this.UserPromptRequest.MethodInvoking += new System.EventHandler (this.UserPromptRequest_OnInvoking);
            // 
            // UserPromptRequestResponse
            // 
            this.UserPromptRequestResponse.Activities.Add (this.UserPromptRequest);
            this.UserPromptRequestResponse.Activities.Add (this.ListenForPromptResponse);
            this.UserPromptRequestResponse.Name = "UserPromptRequestResponse";
            // 
            // WhileWaitingForResponse
            // 
            this.WhileWaitingForResponse.Activities.Add (this.UserPromptRequestResponse);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs> (this.WhileWaitingForResponse_OnEvaluation);
            this.WhileWaitingForResponse.Condition = codecondition1;
            this.WhileWaitingForResponse.Name = "WhileWaitingForResponse";
            // 
            // InitializeActivity
            // 
            this.InitializeActivity.Name = "InitializeActivity";
            this.InitializeActivity.ExecuteCode += new System.EventHandler (this.InitializeActivity_OnCodeExecute);
            // 
            // PromptUser
            // 
            this.Activities.Add (this.InitializeActivity);
            this.Activities.Add (this.WhileWaitingForResponse);
            this.Name = "PromptUser";
            this.CanModifyActivities = false;

        }

        #endregion

        private SequenceActivity UserPromptRequestResponse;
        private CallExternalMethodActivity UserPromptRequest;
        private EventDrivenActivity ResponseTimeout;
        private EventDrivenActivity ReceivedResponse;
        private ListenActivity ListenForPromptResponse;
        private DelayActivity TimeoutException;
        private HandleExternalEventActivity PromptResponse;
        private CodeActivity InitializeActivity;
        private WhileActivity WhileWaitingForResponse;

















    }
}
