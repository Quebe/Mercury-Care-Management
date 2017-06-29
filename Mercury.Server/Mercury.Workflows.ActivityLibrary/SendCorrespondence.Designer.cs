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
    public partial class SendCorrespondence {
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
            this.CorrespondenceResponse = new System.Workflow.Activities.HandleExternalEventActivity ();
            this.ResponseTimeout = new System.Workflow.Activities.EventDrivenActivity ();
            this.ReceiveCorrespondenceResponse = new System.Workflow.Activities.EventDrivenActivity ();
            this.ListenForResponse = new System.Workflow.Activities.ListenActivity ();
            this.SendCorrespondenceRequest = new System.Workflow.Activities.CallExternalMethodActivity ();
            this.CorrespondenceRequestResponse = new System.Workflow.Activities.SequenceActivity ();
            this.WhileWaitingForCorrespondence = new System.Workflow.Activities.WhileActivity ();
            this.InitializeActivity = new System.Workflow.Activities.CodeActivity ();
            // 
            // TimeoutException
            // 
            this.TimeoutException.Name = "TimeoutException";
            this.TimeoutException.TimeoutDuration = System.TimeSpan.Parse ("23:59:59");
            // 
            // CorrespondenceResponse
            // 
            this.CorrespondenceResponse.EventName = "OnUserInteractionResponse";
            this.CorrespondenceResponse.InterfaceType = typeof (Mercury.Server.Workflows.IWorkflowService);
            this.CorrespondenceResponse.Name = "CorrespondenceResponse";
            activitybind1.Name = "SendCorrespondence";
            activitybind1.Path = "UserInteractionResponseEventArgs";
            workflowparameterbinding1.ParameterName = "e";
            workflowparameterbinding1.SetBinding (System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind) (activitybind1)));
            activitybind2.Name = "SendCorrespondence";
            activitybind2.Path = "Application";
            workflowparameterbinding2.ParameterName = "sender";
            workflowparameterbinding2.SetBinding (System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind) (activitybind2)));
            this.CorrespondenceResponse.ParameterBindings.Add (workflowparameterbinding1);
            this.CorrespondenceResponse.ParameterBindings.Add (workflowparameterbinding2);
            this.CorrespondenceResponse.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs> (this.SendCorrespondenceResponse_OnInvoked);
            // 
            // ResponseTimeout
            // 
            this.ResponseTimeout.Activities.Add (this.TimeoutException);
            this.ResponseTimeout.Name = "ResponseTimeout";
            // 
            // ReceiveCorrespondenceResponse
            // 
            this.ReceiveCorrespondenceResponse.Activities.Add (this.CorrespondenceResponse);
            this.ReceiveCorrespondenceResponse.Name = "ReceiveCorrespondenceResponse";
            // 
            // ListenForResponse
            // 
            this.ListenForResponse.Activities.Add (this.ReceiveCorrespondenceResponse);
            this.ListenForResponse.Activities.Add (this.ResponseTimeout);
            this.ListenForResponse.Name = "ListenForResponse";
            // 
            // SendCorrespondenceRequest
            // 
            this.SendCorrespondenceRequest.InterfaceType = typeof (Mercury.Server.Workflows.IWorkflowService);
            this.SendCorrespondenceRequest.MethodName = "UserInteractionRequest";
            this.SendCorrespondenceRequest.Name = "SendCorrespondenceRequest";
            activitybind3.Name = "SendCorrespondence";
            activitybind3.Path = "UserInteractionRequest";
            workflowparameterbinding3.ParameterName = "request";
            workflowparameterbinding3.SetBinding (System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind) (activitybind3)));
            this.SendCorrespondenceRequest.ParameterBindings.Add (workflowparameterbinding3);
            this.SendCorrespondenceRequest.MethodInvoking += new System.EventHandler (this.SendCorrespondenceRequest_OnInvoking);
            // 
            // CorrespondenceRequestResponse
            // 
            this.CorrespondenceRequestResponse.Activities.Add (this.SendCorrespondenceRequest);
            this.CorrespondenceRequestResponse.Activities.Add (this.ListenForResponse);
            this.CorrespondenceRequestResponse.Name = "CorrespondenceRequestResponse";
            // 
            // WhileWaitingForCorrespondence
            // 
            this.WhileWaitingForCorrespondence.Activities.Add (this.CorrespondenceRequestResponse);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs> (this.WhileWaitingForCorrespondence_OnEvaluation);
            this.WhileWaitingForCorrespondence.Condition = codecondition1;
            this.WhileWaitingForCorrespondence.Name = "WhileWaitingForCorrespondence";
            // 
            // InitializeActivity
            // 
            this.InitializeActivity.Name = "InitializeActivity";
            this.InitializeActivity.ExecuteCode += new System.EventHandler (this.InitializeActivity_OnCodeExecute);
            // 
            // SendCorrespondence
            // 
            this.Activities.Add (this.InitializeActivity);
            this.Activities.Add (this.WhileWaitingForCorrespondence);
            this.Name = "SendCorrespondence";
            this.CanModifyActivities = false;

        }

        #endregion

        private CodeActivity InitializeActivity;
        private WhileActivity WhileWaitingForCorrespondence;
        private SequenceActivity CorrespondenceRequestResponse;
        private CallExternalMethodActivity SendCorrespondenceRequest;
        private EventDrivenActivity ResponseTimeout;
        private EventDrivenActivity ReceiveCorrespondenceResponse;
        private ListenActivity ListenForResponse;
        private HandleExternalEventActivity CorrespondenceResponse;
        private DelayActivity TimeoutException;








































    }
}
