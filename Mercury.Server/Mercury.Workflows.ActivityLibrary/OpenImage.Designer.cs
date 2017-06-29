using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Reflection;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace Mercury.Workflows.ActivityLibrary {
    public partial class OpenImage {
        #region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        [System.CodeDom.Compiler.GeneratedCode ("", "")]
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
            this.OpenImageResponse = new System.Workflow.Activities.HandleExternalEventActivity ();
            this.ResponseTimeout = new System.Workflow.Activities.EventDrivenActivity ();
            this.ReceiveResponse = new System.Workflow.Activities.EventDrivenActivity ();
            this.ListenForResponse = new System.Workflow.Activities.ListenActivity ();
            this.OpenImageRequest = new System.Workflow.Activities.CallExternalMethodActivity ();
            this.OpenRequestResponse = new System.Workflow.Activities.SequenceActivity ();
            this.WhileWaitingForResponse = new System.Workflow.Activities.WhileActivity ();
            this.InitializeActivity = new System.Workflow.Activities.CodeActivity ();
            // 
            // TimeoutException
            // 
            this.TimeoutException.Name = "TimeoutException";
            this.TimeoutException.TimeoutDuration = System.TimeSpan.Parse ("23:59:59");
            // 
            // OpenImageResponse
            // 
            this.OpenImageResponse.EventName = "OnUserInteractionResponse";
            this.OpenImageResponse.InterfaceType = typeof (Mercury.Server.Workflows.IWorkflowService);
            this.OpenImageResponse.Name = "OpenImageResponse";
            activitybind1.Name = "OpenImage";
            activitybind1.Path = "Application";
            workflowparameterbinding1.ParameterName = "sender";
            workflowparameterbinding1.SetBinding (System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            activitybind2.Name = "OpenImage";
            activitybind2.Path = "UserInteractionResponseEventArgs";
            workflowparameterbinding2.ParameterName = "e";
            workflowparameterbinding2.SetBinding (System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            this.OpenImageResponse.ParameterBindings.Add (workflowparameterbinding1);
            this.OpenImageResponse.ParameterBindings.Add (workflowparameterbinding2);
            this.OpenImageResponse.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs> (this.OpenImage_Invoked);
            // 
            // ResponseTimeout
            // 
            this.ResponseTimeout.Activities.Add (this.TimeoutException);
            this.ResponseTimeout.Name = "ResponseTimeout";
            // 
            // ReceiveResponse
            // 
            this.ReceiveResponse.Activities.Add (this.OpenImageResponse);
            this.ReceiveResponse.Name = "ReceiveResponse";
            // 
            // ListenForResponse
            // 
            this.ListenForResponse.Activities.Add (this.ReceiveResponse);
            this.ListenForResponse.Activities.Add (this.ResponseTimeout);
            this.ListenForResponse.Name = "ListenForResponse";
            // 
            // OpenImageRequest
            // 
            this.OpenImageRequest.InterfaceType = typeof (Mercury.Server.Workflows.IWorkflowService);
            this.OpenImageRequest.MethodName = "UserInteractionRequest";
            this.OpenImageRequest.Name = "OpenImageRequest";
            activitybind3.Name = "OpenImage";
            activitybind3.Path = "UserInteractionRequest";
            workflowparameterbinding3.ParameterName = "request";
            workflowparameterbinding3.SetBinding (System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            this.OpenImageRequest.ParameterBindings.Add (workflowparameterbinding3);
            this.OpenImageRequest.MethodInvoking += new System.EventHandler (this.OpenImage_OnInvoking);
            // 
            // OpenRequestResponse
            // 
            this.OpenRequestResponse.Activities.Add (this.OpenImageRequest);
            this.OpenRequestResponse.Activities.Add (this.ListenForResponse);
            this.OpenRequestResponse.Name = "OpenRequestResponse";
            // 
            // WhileWaitingForResponse
            // 
            this.WhileWaitingForResponse.Activities.Add (this.OpenRequestResponse);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs> (this.WhileWaitingForContact_OnEvaluation);
            this.WhileWaitingForResponse.Condition = codecondition1;
            this.WhileWaitingForResponse.Name = "WhileWaitingForResponse";
            // 
            // InitializeActivity
            // 
            this.InitializeActivity.Name = "InitializeActivity";
            this.InitializeActivity.ExecuteCode += new System.EventHandler (this.InitializeActivity_OnCodeExecute);
            // 
            // OpenImage
            // 
            this.Activities.Add (this.InitializeActivity);
            this.Activities.Add (this.WhileWaitingForResponse);
            this.Name = "OpenImage";
            this.CanModifyActivities = false;

        }

        #endregion

        private EventDrivenActivity ResponseTimeout;

        private EventDrivenActivity ReceiveResponse;

        private ListenActivity ListenForResponse;

        private HandleExternalEventActivity OpenImageResponse;

        private DelayActivity TimeoutException;

        private SequenceActivity OpenRequestResponse;

        private CallExternalMethodActivity OpenImageRequest;

        private WhileActivity WhileWaitingForResponse;

        private CodeActivity InitializeActivity;


















    }
}
