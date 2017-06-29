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
    public partial class RequireEntity {
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
            this.EntityResponse = new System.Workflow.Activities.HandleExternalEventActivity ();
            this.ResponseTimeout = new System.Workflow.Activities.EventDrivenActivity ();
            this.ReceivedResponse = new System.Workflow.Activities.EventDrivenActivity ();
            this.ListenForEntityResponse = new System.Workflow.Activities.ListenActivity ();
            this.EntityRequest = new System.Workflow.Activities.CallExternalMethodActivity ();
            this.EntityRequestResponse = new System.Workflow.Activities.SequenceActivity ();
            this.WhileWaitingforEntity = new System.Workflow.Activities.WhileActivity ();
            this.InitializeActivity = new System.Workflow.Activities.CodeActivity ();
            // 
            // TimeoutException
            // 
            this.TimeoutException.Name = "TimeoutException";
            this.TimeoutException.TimeoutDuration = System.TimeSpan.Parse ("01:00:00");
            // 
            // EntityResponse
            // 
            this.EntityResponse.EventName = "OnUserInteractionResponse";
            this.EntityResponse.InterfaceType = typeof (Mercury.Server.Workflows.IWorkflowService);
            this.EntityResponse.Name = "EntityResponse";
            activitybind1.Name = "RequireEntity";
            activitybind1.Path = "Application";
            workflowparameterbinding1.ParameterName = "sender";
            workflowparameterbinding1.SetBinding (System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind) (activitybind1)));
            activitybind2.Name = "RequireEntity";
            activitybind2.Path = "UserInteractionResponseEventArgs";
            workflowparameterbinding2.ParameterName = "e";
            workflowparameterbinding2.SetBinding (System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind) (activitybind2)));
            this.EntityResponse.ParameterBindings.Add (workflowparameterbinding1);
            this.EntityResponse.ParameterBindings.Add (workflowparameterbinding2);
            this.EntityResponse.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs> (this.EntityResponse_Invoked);
            // 
            // ResponseTimeout
            // 
            this.ResponseTimeout.Activities.Add (this.TimeoutException);
            this.ResponseTimeout.Name = "ResponseTimeout";
            // 
            // ReceivedResponse
            // 
            this.ReceivedResponse.Activities.Add (this.EntityResponse);
            this.ReceivedResponse.Name = "ReceivedResponse";
            // 
            // ListenForEntityResponse
            // 
            this.ListenForEntityResponse.Activities.Add (this.ReceivedResponse);
            this.ListenForEntityResponse.Activities.Add (this.ResponseTimeout);
            this.ListenForEntityResponse.Name = "ListenForEntityResponse";
            // 
            // EntityRequest
            // 
            this.EntityRequest.InterfaceType = typeof (Mercury.Server.Workflows.IWorkflowService);
            this.EntityRequest.MethodName = "UserInteractionRequest";
            this.EntityRequest.Name = "EntityRequest";
            activitybind3.Name = "RequireEntity";
            activitybind3.Path = "UserInteractionRequest";
            workflowparameterbinding3.ParameterName = "request";
            workflowparameterbinding3.SetBinding (System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind) (activitybind3)));
            this.EntityRequest.ParameterBindings.Add (workflowparameterbinding3);
            this.EntityRequest.MethodInvoking += new System.EventHandler (this.EntityRequest_OnInvoking);
            // 
            // EntityRequestResponse
            // 
            this.EntityRequestResponse.Activities.Add (this.EntityRequest);
            this.EntityRequestResponse.Activities.Add (this.ListenForEntityResponse);
            this.EntityRequestResponse.Name = "EntityRequestResponse";
            // 
            // WhileWaitingforEntity
            // 
            this.WhileWaitingforEntity.Activities.Add (this.EntityRequestResponse);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs> (this.WhileWaitingForEntity_OnEvaluation);
            this.WhileWaitingforEntity.Condition = codecondition1;
            this.WhileWaitingforEntity.Name = "WhileWaitingforEntity";
            // 
            // InitializeActivity
            // 
            this.InitializeActivity.Name = "InitializeActivity";
            this.InitializeActivity.ExecuteCode += new System.EventHandler (this.InitializeActivity_OnCodeExecute);
            // 
            // RequireEntity
            // 
            this.Activities.Add (this.InitializeActivity);
            this.Activities.Add (this.WhileWaitingforEntity);
            this.Name = "RequireEntity";
            this.CanModifyActivities = false;

        }

        #endregion

        private DelayActivity TimeoutException;
        private HandleExternalEventActivity EntityResponse;
        private EventDrivenActivity ResponseTimeout;
        private EventDrivenActivity ReceivedResponse;
        private ListenActivity ListenForEntityResponse;
        private CallExternalMethodActivity EntityRequest;
        private SequenceActivity EntityRequestResponse;
        private WhileActivity WhileWaitingforEntity;
        private CodeActivity InitializeActivity;











    }
}
