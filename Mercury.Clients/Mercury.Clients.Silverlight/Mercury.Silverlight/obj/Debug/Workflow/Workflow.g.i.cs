﻿#pragma checksum "C:\Mercury.Development\Mercury.Clients\Mercury.Clients.Silverlight\Mercury.Silverlight\Workflow\Workflow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C87EB776883AEEB04C011EA9F7B77009"
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


namespace Mercury.Silverlight.Workflow {
    
    
    public partial class Workflow : Mercury.Silverlight.WindowManager.Window {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Border WindowBarBorder;
        
        internal System.Windows.Controls.Grid WindowBarGrid;
        
        internal System.Windows.Controls.TextBlock WindowTitle;
        
        internal Telerik.Windows.Controls.RadButton WindowMinimize;
        
        internal Telerik.Windows.Controls.RadButton WindowClose;
        
        internal Telerik.Windows.Controls.RadButton WorkflowNextItem;
        
        internal System.Windows.Controls.Border WindowContentBorder;
        
        internal System.Windows.Controls.Grid WorkflowInformationMember;
        
        internal System.Windows.Controls.Image WorkflowInformationMemberNoteWarning;
        
        internal System.Windows.Controls.Image WorkflowInformationMemberNoteCritical;
        
        internal System.Windows.Controls.TextBlock WorkflowInformationMemberName;
        
        internal System.Windows.Controls.TextBlock WorkflowInformationMemberBirthDate;
        
        internal System.Windows.Controls.TextBlock WorkflowInformationMemberAge;
        
        internal System.Windows.Controls.TextBlock WorkflowInformationMemberGender;
        
        internal System.Windows.Controls.TextBlock WorkflowInformationMemberProgramName;
        
        internal System.Windows.Controls.TextBlock WorkflowInformationMemberProgramMemberId;
        
        internal System.Windows.Controls.Grid WorkflowInformationProvider;
        
        internal System.Windows.Controls.Image WorkflowInformationProviderNoteWarning;
        
        internal System.Windows.Controls.Image WorkflowInformationProviderNoteCritical;
        
        internal System.Windows.Controls.TextBlock WorkflowInformationProviderName;
        
        internal System.Windows.Controls.TextBlock WorkflowInformationProviderGender;
        
        internal System.Windows.Controls.TextBlock WorkflowInformationProviderProgramName;
        
        internal System.Windows.Controls.TextBlock WorkflowInformationProviderProviderProgramProviderId;
        
        internal System.Windows.Controls.Image WorkflowIcon;
        
        internal System.Windows.Controls.TextBlock WorkflowActionMessage;
        
        internal System.Windows.Controls.Grid WorkflowLastMessageContainer;
        
        internal System.Windows.Controls.Image WorkflowLastMessageIcon;
        
        internal System.Windows.Controls.TextBlock WorkflowLastMessage;
        
        internal System.Windows.Controls.Grid WorkflowExceptionMessageContainer;
        
        internal System.Windows.Controls.Image WorkflowExceptionMessageIcon;
        
        internal System.Windows.Controls.TextBlock WorkflowExceptionMessage;
        
        internal System.Windows.Controls.ScrollViewer WorkflowContent;
        
        internal System.Windows.Controls.Primitives.Popup WorkflowCloseConfirmationPopup;
        
        internal System.Windows.Controls.Grid WorkflowCloseConfirmationContainer;
        
        internal Telerik.Windows.Controls.RadButton WorkflowCloseConfirmationClose;
        
        internal Telerik.Windows.Controls.RadButton WorkflowCloseConfirmationOk;
        
        internal Telerik.Windows.Controls.RadButton WorkflowCloseConfirmationCancel;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Mercury.Silverlight;component/Workflow/Workflow.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.WindowBarBorder = ((System.Windows.Controls.Border)(this.FindName("WindowBarBorder")));
            this.WindowBarGrid = ((System.Windows.Controls.Grid)(this.FindName("WindowBarGrid")));
            this.WindowTitle = ((System.Windows.Controls.TextBlock)(this.FindName("WindowTitle")));
            this.WindowMinimize = ((Telerik.Windows.Controls.RadButton)(this.FindName("WindowMinimize")));
            this.WindowClose = ((Telerik.Windows.Controls.RadButton)(this.FindName("WindowClose")));
            this.WorkflowNextItem = ((Telerik.Windows.Controls.RadButton)(this.FindName("WorkflowNextItem")));
            this.WindowContentBorder = ((System.Windows.Controls.Border)(this.FindName("WindowContentBorder")));
            this.WorkflowInformationMember = ((System.Windows.Controls.Grid)(this.FindName("WorkflowInformationMember")));
            this.WorkflowInformationMemberNoteWarning = ((System.Windows.Controls.Image)(this.FindName("WorkflowInformationMemberNoteWarning")));
            this.WorkflowInformationMemberNoteCritical = ((System.Windows.Controls.Image)(this.FindName("WorkflowInformationMemberNoteCritical")));
            this.WorkflowInformationMemberName = ((System.Windows.Controls.TextBlock)(this.FindName("WorkflowInformationMemberName")));
            this.WorkflowInformationMemberBirthDate = ((System.Windows.Controls.TextBlock)(this.FindName("WorkflowInformationMemberBirthDate")));
            this.WorkflowInformationMemberAge = ((System.Windows.Controls.TextBlock)(this.FindName("WorkflowInformationMemberAge")));
            this.WorkflowInformationMemberGender = ((System.Windows.Controls.TextBlock)(this.FindName("WorkflowInformationMemberGender")));
            this.WorkflowInformationMemberProgramName = ((System.Windows.Controls.TextBlock)(this.FindName("WorkflowInformationMemberProgramName")));
            this.WorkflowInformationMemberProgramMemberId = ((System.Windows.Controls.TextBlock)(this.FindName("WorkflowInformationMemberProgramMemberId")));
            this.WorkflowInformationProvider = ((System.Windows.Controls.Grid)(this.FindName("WorkflowInformationProvider")));
            this.WorkflowInformationProviderNoteWarning = ((System.Windows.Controls.Image)(this.FindName("WorkflowInformationProviderNoteWarning")));
            this.WorkflowInformationProviderNoteCritical = ((System.Windows.Controls.Image)(this.FindName("WorkflowInformationProviderNoteCritical")));
            this.WorkflowInformationProviderName = ((System.Windows.Controls.TextBlock)(this.FindName("WorkflowInformationProviderName")));
            this.WorkflowInformationProviderGender = ((System.Windows.Controls.TextBlock)(this.FindName("WorkflowInformationProviderGender")));
            this.WorkflowInformationProviderProgramName = ((System.Windows.Controls.TextBlock)(this.FindName("WorkflowInformationProviderProgramName")));
            this.WorkflowInformationProviderProviderProgramProviderId = ((System.Windows.Controls.TextBlock)(this.FindName("WorkflowInformationProviderProviderProgramProviderId")));
            this.WorkflowIcon = ((System.Windows.Controls.Image)(this.FindName("WorkflowIcon")));
            this.WorkflowActionMessage = ((System.Windows.Controls.TextBlock)(this.FindName("WorkflowActionMessage")));
            this.WorkflowLastMessageContainer = ((System.Windows.Controls.Grid)(this.FindName("WorkflowLastMessageContainer")));
            this.WorkflowLastMessageIcon = ((System.Windows.Controls.Image)(this.FindName("WorkflowLastMessageIcon")));
            this.WorkflowLastMessage = ((System.Windows.Controls.TextBlock)(this.FindName("WorkflowLastMessage")));
            this.WorkflowExceptionMessageContainer = ((System.Windows.Controls.Grid)(this.FindName("WorkflowExceptionMessageContainer")));
            this.WorkflowExceptionMessageIcon = ((System.Windows.Controls.Image)(this.FindName("WorkflowExceptionMessageIcon")));
            this.WorkflowExceptionMessage = ((System.Windows.Controls.TextBlock)(this.FindName("WorkflowExceptionMessage")));
            this.WorkflowContent = ((System.Windows.Controls.ScrollViewer)(this.FindName("WorkflowContent")));
            this.WorkflowCloseConfirmationPopup = ((System.Windows.Controls.Primitives.Popup)(this.FindName("WorkflowCloseConfirmationPopup")));
            this.WorkflowCloseConfirmationContainer = ((System.Windows.Controls.Grid)(this.FindName("WorkflowCloseConfirmationContainer")));
            this.WorkflowCloseConfirmationClose = ((Telerik.Windows.Controls.RadButton)(this.FindName("WorkflowCloseConfirmationClose")));
            this.WorkflowCloseConfirmationOk = ((Telerik.Windows.Controls.RadButton)(this.FindName("WorkflowCloseConfirmationOk")));
            this.WorkflowCloseConfirmationCancel = ((Telerik.Windows.Controls.RadButton)(this.FindName("WorkflowCloseConfirmationCancel")));
        }
    }
}

