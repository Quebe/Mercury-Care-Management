﻿#pragma checksum "C:\Mercury.Development\Mercury.Clients\Mercury.Clients.Silverlight\Mercury.Silverlight\Workspace\Workspace.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D53DC24C546C2EF474DA1482DF1CB02B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Mercury.Silverlight.Workspace;
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


namespace Mercury.Silverlight.Workspace {
    
    
    public partial class Workspace : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Border ApplicationTitleBarBorder;
        
        internal System.Windows.Controls.Grid ApplicationTitleBar;
        
        internal Telerik.Windows.Controls.RadMenu ApplicationMenu;
        
        internal System.Windows.Controls.Image ApplicationButtonImage;
        
        internal System.Windows.Controls.HyperlinkButton ApplicationTitle;
        
        internal Telerik.Windows.Controls.RadButton ApplicationLinkHome;
        
        internal Telerik.Windows.Controls.RadButton ApplicationLinkSearch;
        
        internal Telerik.Windows.Controls.RadButton ApplicationLinkLogOff;
        
        internal Telerik.Windows.Controls.RadRibbonSplitButton ApplicationSwitchWindows;
        
        internal Telerik.Windows.Controls.RadContextMenu ApplicationSwitchWindowsContextMenu;
        
        internal Telerik.Windows.Controls.RadMenuItem ApplicationSwitchWindowsContextMenuWorkspaceSeparator;
        
        internal Mercury.Silverlight.Workspace.Work Work;
        
        internal System.Windows.Controls.Border ActiveWindowContent;
        
        internal System.Windows.Controls.Border StatusBarContent;
        
        internal System.Windows.Controls.Grid ApplicationStatusBarGrid;
        
        internal System.Windows.Controls.TextBlock ApplicationStatusText;
        
        internal Telerik.Windows.Controls.RadProgressBar ApplicationStatusProgressBar;
        
        internal Mercury.Silverlight.Workspace.SwitchWindowPreview WindowPreview;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Mercury.Silverlight;component/Workspace/Workspace.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ApplicationTitleBarBorder = ((System.Windows.Controls.Border)(this.FindName("ApplicationTitleBarBorder")));
            this.ApplicationTitleBar = ((System.Windows.Controls.Grid)(this.FindName("ApplicationTitleBar")));
            this.ApplicationMenu = ((Telerik.Windows.Controls.RadMenu)(this.FindName("ApplicationMenu")));
            this.ApplicationButtonImage = ((System.Windows.Controls.Image)(this.FindName("ApplicationButtonImage")));
            this.ApplicationTitle = ((System.Windows.Controls.HyperlinkButton)(this.FindName("ApplicationTitle")));
            this.ApplicationLinkHome = ((Telerik.Windows.Controls.RadButton)(this.FindName("ApplicationLinkHome")));
            this.ApplicationLinkSearch = ((Telerik.Windows.Controls.RadButton)(this.FindName("ApplicationLinkSearch")));
            this.ApplicationLinkLogOff = ((Telerik.Windows.Controls.RadButton)(this.FindName("ApplicationLinkLogOff")));
            this.ApplicationSwitchWindows = ((Telerik.Windows.Controls.RadRibbonSplitButton)(this.FindName("ApplicationSwitchWindows")));
            this.ApplicationSwitchWindowsContextMenu = ((Telerik.Windows.Controls.RadContextMenu)(this.FindName("ApplicationSwitchWindowsContextMenu")));
            this.ApplicationSwitchWindowsContextMenuWorkspaceSeparator = ((Telerik.Windows.Controls.RadMenuItem)(this.FindName("ApplicationSwitchWindowsContextMenuWorkspaceSeparator")));
            this.Work = ((Mercury.Silverlight.Workspace.Work)(this.FindName("Work")));
            this.ActiveWindowContent = ((System.Windows.Controls.Border)(this.FindName("ActiveWindowContent")));
            this.StatusBarContent = ((System.Windows.Controls.Border)(this.FindName("StatusBarContent")));
            this.ApplicationStatusBarGrid = ((System.Windows.Controls.Grid)(this.FindName("ApplicationStatusBarGrid")));
            this.ApplicationStatusText = ((System.Windows.Controls.TextBlock)(this.FindName("ApplicationStatusText")));
            this.ApplicationStatusProgressBar = ((Telerik.Windows.Controls.RadProgressBar)(this.FindName("ApplicationStatusProgressBar")));
            this.WindowPreview = ((Mercury.Silverlight.Workspace.SwitchWindowPreview)(this.FindName("WindowPreview")));
        }
    }
}

