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
    public partial class WorkQueueItemClose {
        #region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        private void InitializeComponent () {
            this.CanModifyActivities = true;
            this.WorkQueueItemCloseAction = new System.Workflow.Activities.CodeActivity ();
            // 
            // WorkQueueItemCloseAction
            // 
            this.WorkQueueItemCloseAction.Name = "WorkQueueItemCloseAction";
            this.WorkQueueItemCloseAction.ExecuteCode += new System.EventHandler (this.WorkQueueItemClose_CodeExecution);
            // 
            // WorkQueueItemClose
            // 
            this.Activities.Add (this.WorkQueueItemCloseAction);
            this.Name = "WorkQueueItemClose";
            this.CanModifyActivities = false;

        }

        #endregion

        private CodeActivity WorkQueueItemCloseAction;
    }
}
