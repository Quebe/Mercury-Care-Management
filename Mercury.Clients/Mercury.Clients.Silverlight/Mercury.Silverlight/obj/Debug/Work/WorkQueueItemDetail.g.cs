﻿#pragma checksum "C:\Mercury.Development\Mercury.Clients\Mercury.Clients.Silverlight\Mercury.Silverlight\Work\WorkQueueItemDetail.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C91D8383B23CE9A015D2A41C3B9496D8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Mercury.Silverlight.WindowManager;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;
using Telerik.Windows.Controls;


namespace Mercury.Silverlight.Work {
    
    
    public partial class WorkQueueItemDetail : Mercury.Silverlight.WindowManager.Window {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Border WindowBarBorder;
        
        internal System.Windows.Controls.Grid WindowBarGrid;
        
        internal System.Windows.Controls.TextBlock WindowTitle;
        
        internal Telerik.Windows.Controls.RadButton WindowMinimize;
        
        internal Telerik.Windows.Controls.RadButton WindowClose;
        
        internal System.Windows.Controls.Border ExceptionContainer;
        
        internal System.Windows.Controls.TextBlock ExceptionMessage;
        
        internal System.Windows.Controls.Border WindowContent;
        
        internal System.Windows.Controls.ScrollViewer WorkQueueItemDetailScrollViewer;
        
        internal System.Windows.Controls.TextBox Id;
        
        internal System.Windows.Controls.TextBox Type;
        
        internal System.Windows.Controls.TextBox Item;
        
        internal System.Windows.Controls.TextBox Group;
        
        internal System.Windows.Controls.TextBox WorkQueue;
        
        internal System.Windows.Controls.TextBox Added;
        
        internal System.Windows.Controls.TextBox Worked;
        
        internal System.Windows.Controls.TextBox Constraint;
        
        internal System.Windows.Controls.TextBox Milestone;
        
        internal System.Windows.Controls.TextBox Threshold;
        
        internal System.Windows.Controls.TextBox DueDate;
        
        internal System.Windows.Controls.TextBox Outcome;
        
        internal System.Windows.Controls.TextBox Completion;
        
        internal System.Windows.Controls.TextBox Workflow;
        
        internal System.Windows.Controls.TextBox Instance;
        
        internal System.Windows.Controls.TextBox LastStep;
        
        internal System.Windows.Controls.TextBox NextStep;
        
        internal System.Windows.Controls.TextBox CreatedAuthority;
        
        internal System.Windows.Controls.TextBox ModifiedAuthority;
        
        internal System.Windows.Controls.TextBox AssignedToAuthority;
        
        internal System.Windows.Controls.TextBox CreatedAccountId;
        
        internal System.Windows.Controls.TextBox ModifiedAccountId;
        
        internal System.Windows.Controls.TextBox AssignedToAccountId;
        
        internal System.Windows.Controls.TextBox CreatedName;
        
        internal System.Windows.Controls.TextBox ModifiedName;
        
        internal System.Windows.Controls.TextBox AssignedToName;
        
        internal System.Windows.Controls.TextBox CreatedDate;
        
        internal System.Windows.Controls.TextBox ModifiedDate;
        
        internal System.Windows.Controls.TextBox AssignedToDate;
        
        internal Telerik.Windows.Controls.RadGridView ExtendedPropertiesGrid;
        
        internal Telerik.Windows.Controls.RadGridView SendersGrid;
        
        internal Telerik.Windows.Controls.RadGridView AssignmentHistoryGrid;
        
        internal Telerik.Windows.Controls.RadGridView WorkflowStepsGrid;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Mercury.Silverlight;component/Work/WorkQueueItemDetail.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.WindowBarBorder = ((System.Windows.Controls.Border)(this.FindName("WindowBarBorder")));
            this.WindowBarGrid = ((System.Windows.Controls.Grid)(this.FindName("WindowBarGrid")));
            this.WindowTitle = ((System.Windows.Controls.TextBlock)(this.FindName("WindowTitle")));
            this.WindowMinimize = ((Telerik.Windows.Controls.RadButton)(this.FindName("WindowMinimize")));
            this.WindowClose = ((Telerik.Windows.Controls.RadButton)(this.FindName("WindowClose")));
            this.ExceptionContainer = ((System.Windows.Controls.Border)(this.FindName("ExceptionContainer")));
            this.ExceptionMessage = ((System.Windows.Controls.TextBlock)(this.FindName("ExceptionMessage")));
            this.WindowContent = ((System.Windows.Controls.Border)(this.FindName("WindowContent")));
            this.WorkQueueItemDetailScrollViewer = ((System.Windows.Controls.ScrollViewer)(this.FindName("WorkQueueItemDetailScrollViewer")));
            this.Id = ((System.Windows.Controls.TextBox)(this.FindName("Id")));
            this.Type = ((System.Windows.Controls.TextBox)(this.FindName("Type")));
            this.Item = ((System.Windows.Controls.TextBox)(this.FindName("Item")));
            this.Group = ((System.Windows.Controls.TextBox)(this.FindName("Group")));
            this.WorkQueue = ((System.Windows.Controls.TextBox)(this.FindName("WorkQueue")));
            this.Added = ((System.Windows.Controls.TextBox)(this.FindName("Added")));
            this.Worked = ((System.Windows.Controls.TextBox)(this.FindName("Worked")));
            this.Constraint = ((System.Windows.Controls.TextBox)(this.FindName("Constraint")));
            this.Milestone = ((System.Windows.Controls.TextBox)(this.FindName("Milestone")));
            this.Threshold = ((System.Windows.Controls.TextBox)(this.FindName("Threshold")));
            this.DueDate = ((System.Windows.Controls.TextBox)(this.FindName("DueDate")));
            this.Outcome = ((System.Windows.Controls.TextBox)(this.FindName("Outcome")));
            this.Completion = ((System.Windows.Controls.TextBox)(this.FindName("Completion")));
            this.Workflow = ((System.Windows.Controls.TextBox)(this.FindName("Workflow")));
            this.Instance = ((System.Windows.Controls.TextBox)(this.FindName("Instance")));
            this.LastStep = ((System.Windows.Controls.TextBox)(this.FindName("LastStep")));
            this.NextStep = ((System.Windows.Controls.TextBox)(this.FindName("NextStep")));
            this.CreatedAuthority = ((System.Windows.Controls.TextBox)(this.FindName("CreatedAuthority")));
            this.ModifiedAuthority = ((System.Windows.Controls.TextBox)(this.FindName("ModifiedAuthority")));
            this.AssignedToAuthority = ((System.Windows.Controls.TextBox)(this.FindName("AssignedToAuthority")));
            this.CreatedAccountId = ((System.Windows.Controls.TextBox)(this.FindName("CreatedAccountId")));
            this.ModifiedAccountId = ((System.Windows.Controls.TextBox)(this.FindName("ModifiedAccountId")));
            this.AssignedToAccountId = ((System.Windows.Controls.TextBox)(this.FindName("AssignedToAccountId")));
            this.CreatedName = ((System.Windows.Controls.TextBox)(this.FindName("CreatedName")));
            this.ModifiedName = ((System.Windows.Controls.TextBox)(this.FindName("ModifiedName")));
            this.AssignedToName = ((System.Windows.Controls.TextBox)(this.FindName("AssignedToName")));
            this.CreatedDate = ((System.Windows.Controls.TextBox)(this.FindName("CreatedDate")));
            this.ModifiedDate = ((System.Windows.Controls.TextBox)(this.FindName("ModifiedDate")));
            this.AssignedToDate = ((System.Windows.Controls.TextBox)(this.FindName("AssignedToDate")));
            this.ExtendedPropertiesGrid = ((Telerik.Windows.Controls.RadGridView)(this.FindName("ExtendedPropertiesGrid")));
            this.SendersGrid = ((Telerik.Windows.Controls.RadGridView)(this.FindName("SendersGrid")));
            this.AssignmentHistoryGrid = ((Telerik.Windows.Controls.RadGridView)(this.FindName("AssignmentHistoryGrid")));
            this.WorkflowStepsGrid = ((Telerik.Windows.Controls.RadGridView)(this.FindName("WorkflowStepsGrid")));
        }
    }
}

