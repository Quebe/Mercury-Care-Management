﻿#pragma checksum "C:\Mercury.Development\Mercury.Clients\Mercury.Clients.Silverlight\Mercury.Silverlight\Workflow\UserInteractions\PromptUser.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "88BCA166969007F4841515934BF8BF24"
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


namespace Mercury.Silverlight.Workflow.UserInteractions {
    
    
    public partial class PromptUser : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBlock PromptTitle;
        
        internal System.Windows.Controls.Image PromptImage;
        
        internal System.Windows.Controls.TextBlock PromptMessage;
        
        internal Telerik.Windows.Controls.RadComboBox PromptSelectionItemsSelection;
        
        internal Telerik.Windows.Controls.RadButton ButtonOk;
        
        internal Telerik.Windows.Controls.RadButton ButtonCancel;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Mercury.Silverlight;component/Workflow/UserInteractions/PromptUser.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.PromptTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PromptTitle")));
            this.PromptImage = ((System.Windows.Controls.Image)(this.FindName("PromptImage")));
            this.PromptMessage = ((System.Windows.Controls.TextBlock)(this.FindName("PromptMessage")));
            this.PromptSelectionItemsSelection = ((Telerik.Windows.Controls.RadComboBox)(this.FindName("PromptSelectionItemsSelection")));
            this.ButtonOk = ((Telerik.Windows.Controls.RadButton)(this.FindName("ButtonOk")));
            this.ButtonCancel = ((Telerik.Windows.Controls.RadButton)(this.FindName("ButtonCancel")));
        }
    }
}

