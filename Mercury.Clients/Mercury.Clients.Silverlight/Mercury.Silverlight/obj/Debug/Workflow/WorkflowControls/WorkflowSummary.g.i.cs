﻿#pragma checksum "C:\Mercury.Development\Mercury.Clients\Mercury.Clients.Silverlight\Mercury.Silverlight\Workflow\WorkflowControls\WorkflowSummary.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3D2D7B74F40A76794AB6E85CE8783B87"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace Mercury.Silverlight.Workflow.WorkflowControls {
    
    
    public partial class WorkflowSummary : System.Windows.Controls.UserControl {
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Mercury.Silverlight;component/Workflow/WorkflowControls/WorkflowSummary.xaml", System.UriKind.Relative));
            this.WorkflowStepsGrid = ((Telerik.Windows.Controls.RadGridView)(this.FindName("WorkflowStepsGrid")));
        }
    }
}

