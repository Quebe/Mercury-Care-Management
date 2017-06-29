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
    public partial class ContactEntity {
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
            this.ContactResponse = new System.Workflow.Activities.HandleExternalEventActivity ();
            this.ResponseTimeout = new System.Workflow.Activities.EventDrivenActivity ();
            this.ReceivedResponse = new System.Workflow.Activities.EventDrivenActivity ();
            this.ListenForContactResponse = new System.Workflow.Activities.ListenActivity ();
            this.ContactEntityRequest = new System.Workflow.Activities.CallExternalMethodActivity ();
            this.ContactRequestResponse = new System.Workflow.Activities.SequenceActivity ();
            this.WhileWaitingforContact = new System.Workflow.Activities.WhileActivity ();
            this.InitializeActivity = new System.Workflow.Activities.CodeActivity ();
            // 
            // TimeoutException
            // 
            this.TimeoutException.Name = "TimeoutException";
            this.TimeoutException.TimeoutDuration = System.TimeSpan.Parse ("23:59:59");
            // 
            // ContactResponse
            // 
            this.ContactResponse.EventName = "OnUserInteractionResponse";
            this.ContactResponse.InterfaceType = typeof (Mercury.Server.Workflows.IWorkflowService);
            this.ContactResponse.Name = "ContactResponse";
            activitybind1.Name = "ContactEntity";
            activitybind1.Path = "Application";
            workflowparameterbinding1.ParameterName = "sender";
            workflowparameterbinding1.SetBinding (System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind) (activitybind1)));
            activitybind2.Name = "ContactEntity";
            activitybind2.Path = "UserInteractionResponseEventArgs";
            workflowparameterbinding2.ParameterName = "e";
            workflowparameterbinding2.SetBinding (System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind) (activitybind2)));
            this.ContactResponse.ParameterBindings.Add (workflowparameterbinding1);
            this.ContactResponse.ParameterBindings.Add (workflowparameterbinding2);
            this.ContactResponse.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs> (this.ContactResponse_Invoked);
            // 
            // ResponseTimeout
            // 
            this.ResponseTimeout.Activities.Add (this.TimeoutException);
            this.ResponseTimeout.Name = "ResponseTimeout";
            // 
            // ReceivedResponse
            // 
            this.ReceivedResponse.Activities.Add (this.ContactResponse);
            this.ReceivedResponse.Name = "ReceivedResponse";
            // 
            // ListenForContactResponse
            // 
            this.ListenForContactResponse.Activities.Add (this.ReceivedResponse);
            this.ListenForContactResponse.Activities.Add (this.ResponseTimeout);
            this.ListenForContactResponse.Name = "ListenForContactResponse";
            // 
            // ContactEntityRequest
            // 
            this.ContactEntityRequest.InterfaceType = typeof (Mercury.Server.Workflows.IWorkflowService);
            this.ContactEntityRequest.MethodName = "UserInteractionRequest";
            this.ContactEntityRequest.Name = "ContactEntityRequest";
            activitybind3.Name = "ContactEntity";
            activitybind3.Path = "UserInteractionRequest";
            workflowparameterbinding3.ParameterName = "request";
            workflowparameterbinding3.SetBinding (System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind) (activitybind3)));
            this.ContactEntityRequest.ParameterBindings.Add (workflowparameterbinding3);
            this.ContactEntityRequest.MethodInvoking += new System.EventHandler (this.ContactRequest_OnInvoking);
            // 
            // ContactRequestResponse
            // 
            this.ContactRequestResponse.Activities.Add (this.ContactEntityRequest);
            this.ContactRequestResponse.Activities.Add (this.ListenForContactResponse);
            this.ContactRequestResponse.Name = "ContactRequestResponse";
            // 
            // WhileWaitingforContact
            // 
            this.WhileWaitingforContact.Activities.Add (this.ContactRequestResponse);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs> (this.WhileWaitingForContact_OnEvaluation);
            this.WhileWaitingforContact.Condition = codecondition1;
            this.WhileWaitingforContact.Name = "WhileWaitingforContact";
            // 
            // InitializeActivity
            // 
            this.InitializeActivity.Name = "InitializeActivity";
            this.InitializeActivity.ExecuteCode += new System.EventHandler (this.InitializeActivity_OnCodeExecute);
            // 
            // ContactEntity
            // 
            this.Activities.Add (this.InitializeActivity);
            this.Activities.Add (this.WhileWaitingforContact);
            this.Name = "ContactEntity";
            this.CanModifyActivities = false;

        }

        #endregion

        private DelayActivity TimeoutException;
        private EventDrivenActivity ResponseTimeout;
        private EventDrivenActivity ReceivedResponse;
        private ListenActivity ListenForContactResponse;
        private HandleExternalEventActivity ContactResponse;
        private SequenceActivity ContactRequestResponse;
        private CallExternalMethodActivity ContactEntityRequest;
        private CodeActivity InitializeActivity;
        private WhileActivity WhileWaitingforContact;





















    }
}
