using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Reflection;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace Mercury.Workflows.ActivityLibrary {
    partial class WorkQueueItemSuspendWithAssignment {
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
            this.Suspend = new System.Workflow.ComponentModel.SuspendActivity ();
            this.WorkQueueItemSuspendAction = new System.Workflow.Activities.CodeActivity ();
            activitybind1.Name = "WorkQueueItemSuspendWithAssignment";
            activitybind1.Path = "SuspendMessage";
            // 
            // Suspend
            // 
            this.Suspend.Name = "Suspend";
            this.Suspend.SetBinding (System.Workflow.ComponentModel.SuspendActivity.ErrorProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            // 
            // WorkQueueItemSuspendAction
            // 
            this.WorkQueueItemSuspendAction.Name = "WorkQueueItemSuspendAction";
            this.WorkQueueItemSuspendAction.ExecuteCode += new System.EventHandler (this.WorkQueueItemSuspendWithAssignment_OnExecuteCode);
            // 
            // WorkQueueItemSuspendWithAssignment
            // 
            this.Activities.Add (this.WorkQueueItemSuspendAction);
            this.Activities.Add (this.Suspend);
            this.Name = "WorkQueueItemSuspendWithAssignment";
            this.CanModifyActivities = false;

        }

        #endregion

        private SuspendActivity Suspend;

        private CodeActivity WorkQueueItemSuspendAction;



    }
}
