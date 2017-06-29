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

    public partial class SwitchWindowPreview : UserControl {

        #region Private Properties

        private Client.Application MercuryApplication = ((App) Application.Current).MercuryApplication;

        private WindowManager.WindowManager WindowManager = ((App) Application.Current).WindowManager;

        private Int64 doubleClickTicks = 0;

        #endregion 


        #region Constructors

        public SwitchWindowPreview () {

            InitializeComponent ();

            return;

        }

        #endregion 


        #region Public Methods

        public void UpdatePreviews () {

            this.Dispatcher.BeginInvoke (delegate { UpdatePreviewsProcess (); });

            return;

        }

        private void UpdatePreviewsProcess () {

            WindowPreviewCoverFlow.Items.Clear ();

            WindowPreviews.Children.Clear ();

            foreach (Silverlight.WindowManager.Window currentWindow in WindowManager.Windows) {

                Telerik.Windows.Controls.RadCoverFlowItem item = new Telerik.Windows.Controls.RadCoverFlowItem ();

                Image previewImage = new Image ();

                previewImage.Source = currentWindow.ScreenCapture;

                previewImage.Height = 300;

                previewImage.Width = 300;

                item.Content = previewImage;

                item.Tag = currentWindow.WindowHandle.ToString ();

                item.MouseLeftButtonUp += new MouseButtonEventHandler (RadCoverFlowItem_MouseLeftButtonUp);

                WindowPreviews.Children.Add (previewImage);

                WindowPreviewCoverFlow.Items.Add (item);

            }

            WindowPreviewCoverFlow.InvalidateArrange ();

            WindowPreviewCoverFlow.InvalidateMeasure ();

            if (WindowPreviewCoverFlow.Items.Count > 0) {

                WindowPreviewCoverFlow.SelectedIndex = WindowPreviewCoverFlow.Items.Count - 1;

            }

            return;

        }

        #endregion 


        #region Events

        private void WindowRefresh_Click (object sender, RoutedEventArgs e) {

            UpdatePreviews ();

            return;

        }

        public void WindowClose_Click (object sender, RoutedEventArgs e) {

            Visibility = Visibility.Collapsed;

            return;

        }

        private void WindowPreviewCoverFlow_SelectionChanged (object sender, SelectionChangedEventArgs e) {

            Int32 selectedIndex = WindowPreviewCoverFlow.SelectedIndex;

            if (selectedIndex >= 0) {

                Silverlight.WindowManager.Window selectedWindow = WindowManager.Windows[selectedIndex];

                WindowPreviewTitle.Text = selectedWindow.Title;

            }

            else { WindowPreviewTitle.Text = "No Windows Available"; }

            return;

        }

        private void RadCoverFlowItem_MouseLeftButtonUp (object sender, MouseButtonEventArgs e) {

            Telerik.Windows.Controls.RadCoverFlowItem item = (Telerik.Windows.Controls.RadCoverFlowItem) sender;

            if ((DateTime.Now.Ticks - doubleClickTicks) < 2310000) {

                if (WindowPreviewCoverFlow.SelectedItem == item) {

                    WindowClose_Click (null, null);

                    WindowManager.ActivateWindow (item.Tag.ToString ());

                }

            }

            doubleClickTicks = DateTime.Now.Ticks;

        }

        #endregion 


    }

}
