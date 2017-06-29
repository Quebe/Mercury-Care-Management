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

namespace Mercury.Silverlight.Workspace {

    public partial class GlobalSearch : Mercury.Silverlight.WindowManager.Window {

        #region Private Properties

        private Client.Application MercuryApplication = ((App) Application.Current).MercuryApplication;

        private WindowManager.WindowManager WindowManager = ((App) Application.Current).WindowManager;

        #endregion         
        

        #region Public Properties

        public override string WindowType { get { return "Workspace.GlobalSearch"; } }

        #endregion 


        #region Constructors

        public GlobalSearch () {

            InitializeComponent ();

            title = "Global Search";

            WindowTitle.Text = Title;

            return;

        }

        #endregion 

       

        #region Window Events

        public override void WindowCommand_Activated () {

            base.WindowCommand_Activated ();

            SearchCriteria.Focus ();

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

        #endregion 


        #region Control Events

        private void SearchCriteria_KeyUp (object sender, KeyEventArgs e) {

            if (e.Key == Key.Enter) {

                if (SearchCriteria.IsEnabled) {

                    SearchButton_Click (sender, e);

                }

            }

            return;

        }

        private void SearchButton_Click (object sender, RoutedEventArgs e) {

            SetExceptionMessage (String.Empty);

            if (SearchCriteria.Text.Length >= 3) {

                SearchCriteria.IsEnabled = false;

                SearchButton.IsEnabled = false;


                SearchResultsGrid.IsBusy = true;


                this.Dispatcher.BeginInvoke (delegate {

                    MercuryApplication.SearchGlobal (SearchCriteria.Text, SearchGlobalCompleted);

                });

            }

            return;

        }

        private void SearchGlobalCompleted (Object sender, Server.Application.SearchGlobalCompletedEventArgs e) {

            if ((!SetExceptionMessage (e)) && (!SetExceptionMessage (e.Result))) {

                SearchResultsGrid.ItemsSource = e.Result.Collection;

            }

            else {

                SearchResultsGrid.ItemsSource = null;

            }


            SearchCriteria.IsEnabled = true;

            SearchButton.IsEnabled = true;

            SearchResultsGrid.IsBusy = false;

            return;

        }

        private void SearchResultName_Click (object sender, RoutedEventArgs e) {

            if (!(e.OriginalSource is HyperlinkButton)) { return; }


            HyperlinkButton buttonClicked = (HyperlinkButton) e.OriginalSource;

            String objectType = buttonClicked.TargetName;

            Int64 objectId = Convert.ToInt64 (buttonClicked.Tag.ToString ());

            Dictionary<String, Object> windowParameters = new Dictionary<String, Object> ();


            switch (objectType) {

                case "Member":

                    windowParameters.Add ("Id", objectId);

                    WindowManager.OpenWindow ("Member.MemberProfile", windowParameters);

                    break;

                // TODO: PROVIDER 

            }

            return;

        }

        #endregion 


    }
}
