﻿#pragma checksum "C:\Mercury.Development\Mercury.Clients\Mercury.Clients.Silverlight\Mercury.Silverlight\Workflow\UserInteractions\RequireForm.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "54D0FC69A54AA50825CA1A89C65DC70F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Mercury.Silverlight.Forms.FormEditor;
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


namespace Mercury.Silverlight.Workflow.UserInteractions {
    
    
    public partial class RequireForm : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Border LayoutRoot;
        
        internal Mercury.Silverlight.Forms.FormEditor.FormEditor FormEditorControl;
        
        internal System.Windows.Controls.Border SubmitBar;
        
        internal Telerik.Windows.Controls.RadButton ButtonSubmit;
        
        internal Telerik.Windows.Controls.RadButton ButtonSaveAsDraft;
        
        internal Telerik.Windows.Controls.RadGridView FormValidationGrid;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Mercury.Silverlight;component/Workflow/UserInteractions/RequireForm.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Border)(this.FindName("LayoutRoot")));
            this.FormEditorControl = ((Mercury.Silverlight.Forms.FormEditor.FormEditor)(this.FindName("FormEditorControl")));
            this.SubmitBar = ((System.Windows.Controls.Border)(this.FindName("SubmitBar")));
            this.ButtonSubmit = ((Telerik.Windows.Controls.RadButton)(this.FindName("ButtonSubmit")));
            this.ButtonSaveAsDraft = ((Telerik.Windows.Controls.RadButton)(this.FindName("ButtonSaveAsDraft")));
            this.FormValidationGrid = ((Telerik.Windows.Controls.RadGridView)(this.FindName("FormValidationGrid")));
        }
    }
}

