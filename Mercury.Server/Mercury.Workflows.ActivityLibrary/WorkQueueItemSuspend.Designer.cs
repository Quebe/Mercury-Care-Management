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
    public partial class WorkQueueItemSuspend {
        #region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        private void InitializeComponent () {
            this.CanModifyActivities = true;
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind ();
            this.Suspend = new System.Workflow.ComponentModel.SuspendActivity ();
            this.WorkQueueItemSuspendAction = new System.Workflow.Activities.CodeActivity ();
            activitybind1.Name = "WorkQueueItemSuspend";
            activitybind1.Path = "SuspendMessage";
            // 
            // Suspend
            // 
            this.Suspend.Name = "Suspend";
            this.Suspend.SetBinding (System.Workflow.ComponentModel.SuspendActivity.ErrorProperty, ((System.Workflow.ComponentModel.ActivityBind) (activitybind1)));
            // 
            // WorkQueueItemSuspendAction
            // 
            this.WorkQueueItemSuspendAction.Name = "WorkQueueItemSuspendAction";
            this.WorkQueueItemSuspendAction.ExecuteCode += new System.EventHandler (this.WorkQueueItemSuspend_OnExecuteCode);
            // 
            // WorkQueueItemSuspend
            // 
            this.Activities.Add (this.WorkQueueItemSuspendAction);
            this.Activities.Add (this.Suspend);
            this.Name = "WorkQueueItemSuspend";
            this.CanModifyActivities = false;

        }

        #endregion

        private SuspendActivity Suspend;
        private CodeActivity WorkQueueItemSuspendAction;





    }
}
