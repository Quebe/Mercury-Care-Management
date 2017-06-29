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

namespace Mercury.Silverlight.Controls {

    public partial class MemberEnrollmentHistory : UserControl {

        #region Private Properties

        private Client.Application MercuryApplication = null;

        private WindowManager.WindowManager WindowManager = null;

        private WindowManager.Window window = null;


        private Int64 memberId;

        #endregion 
        
        
        #region Public Properties

        public WindowManager.Window Window {

            get {

                if (window != null) { return window; }

                return new WindowManager.Window ();

            }

            set {

                window = value;

            }

        }

        // PUBLIC ACCESSIBLE MEMBER ID PROPERTY FOR SETTING THE MEMBER, THIS CAUSES THE CONTROL TO TRUELY INITIALIZE

        public Int64 MemberId {

            get { return memberId; }

            set {

                // DO NOT COMPARE VALUES, ALLOW RE-ASSIGNMENT TO "REFRESH" DATA

                memberId = value;

                Window.SetExceptionMessage (String.Empty);

                MercuryApplication.MemberEnrollmentsGet (memberId, true, InitializeMemberEnrollments);

            }

        }

        #endregion 

        
        #region Constructors

        public MemberEnrollmentHistory () {

            InitializeComponent ();

            // PUT ASSIGNMENT INTO PROTECTED CONSTRUCTOR SO THAT PREVIEW IS AVAILABLE IN DESIGN WINDOW

            if (Application.Current is Mercury.Silverlight.App) {

                MercuryApplication = ((App)Application.Current).MercuryApplication;

                WindowManager = ((Mercury.Silverlight.App)Application.Current).WindowManager;

            }

            return;
        }

        #endregion


        #region Initialization

        protected void InitializeMemberEnrollments (Object sender, Server.Application.MemberEnrollmentsGetCompletedEventArgs e) {

            if (Window.SetExceptionMessage (e)) { return; }

            MemberEnrollmentGrid.ItemsSource = Client.Converters.ServerCollectionToClient.MemberEnrollmentCollection (MercuryApplication, e.Result.Collection);

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
       
                    <HyperlinkButton Grid.Column=""1\"" Grid.Row=""1"" Content=""{Binding " + dataBindingMember + @"}"" Tag=""{Binding " + tagDataBindingMember + @"}"" VerticalAlignment=""Center"" Foreground=""#0066CC"" ToolTipService.ToolTip=""" + toolTip + @"""></HyperlinkButton>

                </DataTemplate>"

                );




            return column;

        }

        #endregion

        private void MemberEnrollmentGrid_DataLoading (object sender, Telerik.Windows.Controls.GridView.GridViewDataLoadingEventArgs e) {

            Telerik.Windows.Controls.GridView.GridViewDataControl dataControl = (Telerik.Windows.Controls.GridView.GridViewDataControl)sender;

            Telerik.Windows.Controls.GridViewDataColumn dataColumn = null;


            if (dataControl.ParentRow != null) {

                dataControl.ShowGroupPanel = false;

                dataControl.AutoGenerateColumns = false;

                dataControl.RowIndicatorVisibility = System.Windows.Visibility.Collapsed;

                dataControl.ColumnWidth = new Telerik.Windows.Controls.GridViewLength (1, Telerik.Windows.Controls.GridViewLengthUnitType.Star);

                dataControl.SelectionMode = SelectionMode.Single;

                dataControl.IsReadOnly = true;


                if (dataControl.ItemsSource is System.Collections.ObjectModel.ObservableCollection<Client.Core.Member.MemberEnrollmentCoverage>) {

                    #region Enrollment Coverage Table

                    dataControl.Columns.Add (CreateGridViewDataColumn ("Benefit Plan", "BenefitPlan.Name", 0, 0, true));

                    dataControl.Columns.Add (CreateGridViewDataColumn ("Coverage Type", "CoverageType.Name", 0, 0, true));

                    dataControl.Columns.Add (CreateGridViewDataColumn ("Coverage Level", "CoverageLevel.Name", 0, 0, true));

                    dataControl.Columns.Add (CreateGridViewDataColumn ("Rate Code", "RateCode", 80, 0, true));


                    dataColumn = CreateGridViewDataColumn ("Effective", "EffectiveDate", 80, 0, true);

                    dataColumn.DataMemberBinding.Converter = new Client.ValueConverters.DateToStringFormatter ();

                    dataColumn.IsFilterable = false;

                    dataControl.Columns.Add (dataColumn);


                    dataColumn = CreateGridViewDataColumn ("Termination", "TerminationDate", 80, 0, true);

                    dataColumn.DataMemberBinding.Converter = new Client.ValueConverters.TerminationDateToStringFormatter ();

                    dataColumn.IsFilterable = false;

                    dataColumn.SortingState = Telerik.Windows.Controls.SortingState.Descending;

                    dataControl.Columns.Add (dataColumn);

                    #endregion

                }

                else if (dataControl.ItemsSource is System.Collections.ObjectModel.ObservableCollection<Client.Core.Member.MemberEnrollmentPcp>) {

                    #region PCP Assignment Table

                    dataControl.Columns.Add (CreateGridViewHyperlinkButton ("PCP Provider", "PcpProvider.Name", "PcpProvider.Id", "Open Provider Profile", 0, 0, true));

                    dataControl.Columns.Add (CreateGridViewHyperlinkButton ("PCP Affiliate", "ProviderAffiliation.AffiliateProvider.Name", "ProviderAffiliation.AffiliateProvider.Id", "Open Provider Profile", 0, 0, true));


                    dataColumn = CreateGridViewDataColumn ("Effective", "EffectiveDate", 80, 0, true);

                    dataColumn.DataMemberBinding.Converter = new Client.ValueConverters.DateToStringFormatter ();

                    dataColumn.IsFilterable = false;

                    dataControl.Columns.Add (dataColumn);


                    dataColumn = CreateGridViewDataColumn ("Termination", "TerminationDate", 80, 0, true);

                    dataColumn.DataMemberBinding.Converter = new Client.ValueConverters.TerminationDateToStringFormatter ();

                    dataColumn.IsFilterable = false;

                    dataColumn.SortingState = Telerik.Windows.Controls.SortingState.Descending;

                    dataControl.Columns.Add (dataColumn);


                    #endregion

                }

            }

            return;

        }

        private void MemberEnrollmentGrid_DataLoaded (object sender, EventArgs e) {

            //MemberEnrollmentGrid.IsBusy = false;

            return;

        }

    }

}

