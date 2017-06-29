using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Mercury.Silverlight.Member {

    public partial class MemberProfile : WindowManager.Window {

        #region Private Properties

        private Client.Application MercuryApplication = ((App)Application.Current).MercuryApplication;

        private WindowManager.WindowManager WindowManager = ((App)Application.Current).WindowManager;


        private Int64 memberId = 0;

        private Client.Core.Member.Member member = null;

        private Server.Application.Entity entity = null;

        #endregion


        #region Public Properties

        public override string WindowType { get { return "Member.MemberProfile"; } }

        public override Dictionary<String, Object> Parameters {

            get { return base.Parameters; }

            set {

                base.Parameters = value;

                if (Parameters.ContainsKey ("Id")) {

                    memberId = Convert.ToInt64 (Parameters["Id"]);

                }

                if (memberId != 0) { LoadMember (memberId); }

                else { WindowTitleUpdate ("Invalid or no Member Id Specified."); }

            }

        }

        #endregion 

        
        #region Constructors

        public MemberProfile () {

            InitializeComponent ();

            MemberDemographics.Window = this;

            WindowTitleUpdate (String.Empty);

            return;

        }

        #endregion 
        
        
        #region Window Events

        private void WindowRefresh_Click (object sender, RoutedEventArgs e) {

            LoadMember (memberId);

            // MemberDemographics.MemberId = memberId;

            return;

        }

        private void WindowClose_Click (object sender, RoutedEventArgs e) {

            WindowCommand_Close ();

            return;

        }

        private void WindowMinimize_Click (object sender, RoutedEventArgs e) {

            WindowCommand_Minimize ();

            return;

        }

        private void WindowTitleUpdate (String forTitle) {

            String workingTitle = "Member Profile";

            if (!String.IsNullOrEmpty (forTitle)) {

                workingTitle = workingTitle + " - " + forTitle;

            }

            else { workingTitle = workingTitle + " - Untitled"; }


            title = workingTitle;

            WindowTitle.Text = Title;

            return;

        }

        #endregion 
        
        
        #region Initialization

        private void LoadMember (Int64 memberId) {

            SetExceptionMessage (String.Empty);

            GlobalProgressBarShow ();

            WindowTitleUpdate ("Loading");

            MercuryApplication.MemberGet (memberId, false, InitializeMember);

            return;

        }

        private void InitializeMember (Object sender, Server.Application.MemberGetCompletedEventArgs e) {

            if ((!SetExceptionMessage (e)) && (!SetExceptionMessage (e.Result, "Member"))) {

                member = new Mercury.Client.Core.Member.Member (MercuryApplication, e.Result);

                MercuryApplication.EntityGet (member.EntityId, true, InitializeEntity);

            }

            return;

        }

        private void InitializeEntity (Object sender, Server.Application.EntityGetCompletedEventArgs e) {

            GlobalProgressBarHide ();

            if ((!SetExceptionMessage (e)) && (!SetExceptionMessage (e.Result, "Member (Entity)"))) {

                entity = e.Result;

                InitializeAll ();

            }

            return;

        }

        private void InitializeAll () {

            if (member == null) { return; }

            if (entity == null) { return; }


            WindowTitleUpdate (entity.Name);

            MemberDemographics.MemberId = member.Id;

            return;

        }

        #endregion


        #region Grid Support Methods

        private Telerik.Windows.Controls.GridViewDataColumn CreateGridViewDataColumn (String header, String dataBindingMember, Double width, Int32 maxWidth, Boolean wrapText) {

            Telerik.Windows.Controls.GridViewDataColumn column = new Telerik.Windows.Controls.GridViewDataColumn ();

            column.Header = header;

            column.DataMemberBinding = new System.Windows.Data.Binding (dataBindingMember);

            if (width != 0) { column.Width = new Telerik.Windows.Controls.GridViewLength (width); }

            if (maxWidth != 0) { column.MaxWidth = maxWidth; }

            column.TextWrapping = (wrapText) ? TextWrapping.Wrap : TextWrapping.NoWrap;

            column.Background = new SolidColorBrush (MercuryApplication.ColorFromWebColor ("EEEEEE"));

            return column;

        }

        private Telerik.Windows.Controls.GridViewColumn CreateGridViewHyperlinkButton (String header, String dataBindingMember, String tagDataBindingMember, String toolTip, Double width, Int32 maxWidth, Boolean wrapText) {

            Telerik.Windows.Controls.GridViewColumn column = new Telerik.Windows.Controls.GridViewColumn ();

            column.Header = header;

            if (width != 0) { column.Width = new Telerik.Windows.Controls.GridViewLength (width); }

            if (maxWidth != 0) { column.MaxWidth = maxWidth; }

            column.TextWrapping = (wrapText) ? TextWrapping.Wrap : TextWrapping.NoWrap;

            column.Background = new SolidColorBrush (MercuryApplication.ColorFromWebColor ("EEEEEE"));


            column.CellTemplate = (DataTemplate)System.Windows.Markup.XamlReader.Load (

               @"<DataTemplate xmlns=""http://schemas.microsoft.com/client/2007"">
       
                    <HyperlinkButton Grid.Column=""1\"" Grid.Row=""1"" Content=""{Binding " + dataBindingMember + @"}"" Tag=""{Binding RelatedMemberId}"" VerticalAlignment=""Center"" Foreground=""#0066CC"" ToolTipService.ToolTip=""" + toolTip + @"""></HyperlinkButton>

                </DataTemplate>"

                );




            return column;

        }

        #endregion


        #region Control Events

        private void RadTabItemScrollViewer_MouseWheel (object sender, MouseWheelEventArgs e) {

            if (!(sender is ScrollViewer)) { return; }


            ScrollViewer viewer = (ScrollViewer)sender;

            viewer.ScrollToVerticalOffset (viewer.VerticalOffset - e.Delta);

            return;

        }

        private void MemberInformationTabControl_SelectionChanged (object sender, RoutedEventArgs e) {

            Telerik.Windows.Controls.RadTabItem selectedTab = (Telerik.Windows.Controls.RadTabItem)MemberInformationTabControl.SelectedItem;

            if (selectedTab == null) { return; }

            switch (selectedTab.DropDownContent.ToString ()) {

                case "Enrollment":

                    MemberEnrollmentHistoryControl.MemberId = member.Id;

                    //if (MemberEnrollmentGrid.ItemsSource == null) {

                    //    MemberEnrollmentGrid.IsBusy = true;

                    //    this.Dispatcher.BeginInvoke (delegate {

                    //        MemberEnrollmentGrid.SetBinding (Telerik.Windows.Controls.RadGridView.ItemsSourceProperty, MercuryApplication.PropertyDataBinding ("Enrollments", member, System.Windows.Data.BindingMode.OneWay));

                    //        MemberEnrollmentTplCobGrid.SetBinding (Telerik.Windows.Controls.RadGridView.ItemsSourceProperty, MercuryApplication.PropertyDataBinding ("EnrollmentTplCobs", member, System.Windows.Data.BindingMode.OneWay));

                    //    });

                    //}

                    break;

                case "Services":

                    // MemberServicesControl.MemberId = member.MemberId;

                    break;

            }

            return;

        }

        private void MemberEnrollmentGrid_DataLoading (object sender, Telerik.Windows.Controls.GridView.GridViewDataLoadingEventArgs e) {

            Telerik.Windows.Controls.GridView.GridViewDataControl dataControl = (Telerik.Windows.Controls.GridView.GridViewDataControl)sender;

            Telerik.Windows.Controls.GridViewDataColumn dataColumn = null;


            if (dataControl.ParentRow != null) {

                dataControl.ShowGroupPanel = false;

                dataControl.AutoGenerateColumns = false;

                dataControl.ColumnWidth = new Telerik.Windows.Controls.GridViewLength (1, Telerik.Windows.Controls.GridViewLengthUnitType.Star);

                dataControl.SelectionMode = SelectionMode.Single;

                dataControl.IsReadOnly = true;

                // TODO: SILVERLIGHT UPDATE


                //if (dataControl.ItemsSource is System.Collections.ObjectModel.ObservableCollection<Client.Core.Member.EnrollmentCoverage>) {

                //    #region Enrollment Coverage Table

                //    dataControl.Columns.Add (CreateGridViewDataColumn ("Benefit Plan", "BenefitPlan.Name", 0, 0, true));

                //    dataControl.Columns.Add (CreateGridViewDataColumn ("Coverage Code", "CoverageCodeId", 0, 125, true));

                //    dataControl.Columns.Add (CreateGridViewDataColumn ("Rate Code", "RateCode", 80, 0, true));


                //    dataColumn = CreateGridViewDataColumn ("Effective", "EffectiveDate", 80, 0, true);

                //    dataColumn.DataMemberBinding.Converter = new Client.ValueConverters.DateToStringFormatter ();

                //    dataColumn.IsFilterable = false;

                //    dataControl.Columns.Add (dataColumn);


                //    dataColumn = CreateGridViewDataColumn ("Termination", "TerminationDate", 80, 0, true);

                //    dataColumn.DataMemberBinding.Converter = new Client.ValueConverters.TerminationDateToStringFormatter ();

                //    dataColumn.IsFilterable = false;

                //    dataColumn.SortingState = Telerik.Windows.Controls.SortingState.Descending;

                //    dataControl.Columns.Add (dataColumn);

                //    #endregion

                //}

                //else if (dataControl.ItemsSource is System.Collections.ObjectModel.ObservableCollection<Client.Core.PcpAssignment>) {

                //    #region PCP Assignment Table

                //    dataControl.Columns.Add (CreateGridViewHyperlinkButton ("PCP Provider", "PcpProvider.Entity.Name", "PcpProvider.Id", "Open Provider Profile", 0, 0, true));

                //    dataControl.Columns.Add (CreateGridViewHyperlinkButton ("PCP Affiliate", "PcpAffiliate.Entity.Name", "PcpAffiliateId", "Open Provider Profile", 0, 0, true));


                //    dataColumn = CreateGridViewDataColumn ("Effective", "EffectiveDate", 80, 0, true);

                //    dataColumn.DataMemberBinding.Converter = new Client.ValueConverters.DateToStringFormatter ();

                //    dataColumn.IsFilterable = false;

                //    dataControl.Columns.Add (dataColumn);


                //    dataColumn = CreateGridViewDataColumn ("Termination", "TerminationDate", 80, 0, true);

                //    dataColumn.DataMemberBinding.Converter = new Client.ValueConverters.TerminationDateToStringFormatter ();

                //    dataColumn.IsFilterable = false;

                //    dataColumn.SortingState = Telerik.Windows.Controls.SortingState.Descending;

                //    dataControl.Columns.Add (dataColumn);


                //    #endregion

                //}

            }

            return;

        }

        private void MemberEnrollmentGrid_DataLoaded (object sender, EventArgs e) {

            //MemberEnrollmentGrid.IsBusy = false;

            return;

        }

        #endregion


    }

}

