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

namespace Mercury.Silverlight.Workflow.UserInteractions {

    public partial class RequireForm : UserControl {

        #region Private Properties

        private Client.Application MercuryApplication = ((App) Application.Current).MercuryApplication;

        private WindowManager.WindowManager WindowManager = ((App) Application.Current).WindowManager;

        #endregion         


        #region Public Properties

        public Server.Application.Form Form { get { return FormEditorControl.Form; } set { FormEditorControl.Form = value; } }

        public Boolean AllowSaveAsDraft { get { return (ButtonSaveAsDraft.Visibility == Visibility.Visible); } set { ButtonSaveAsDraft.Visibility = (value) ? Visibility.Visible : Visibility.Collapsed; } }

        public event EventHandler<RequireFormEventArgs> FormSubmit;

        public event EventHandler<RoutedEventArgs> ScrollToControl;

        #endregion 


        #region Constructors

        public RequireForm () {

            InitializeComponent ();

            return;

        }

        #endregion


        #region Button Events

        private void ButtonSubmit_Click (object sender, RoutedEventArgs e) {

            WindowManager.Window_OnGlobalProgressBarShow (this, null);

            MercuryApplication.FormSubmit (FormEditorControl.Form, FormValidationCompleted);

            return;

        }

        private void FormValidationCompleted (Object sender, Server.Application.FormSubmitCompletedEventArgs e) {

            WindowManager.Window_OnGlobalProgressBarHide (this, null);

            FormValidationGrid.Visibility = Visibility.Collapsed;

            if ((e.Cancelled) || (e.Error != null) || (e.Result == null)) {

                // TODO: ERROR MESSAGE

                return;

            }

            if (e.Result.Collection.Count > 0) {

                FormValidationGrid.ItemsSource = e.Result.Collection;

                FormValidationGrid.Visibility = Visibility.Visible;

                if (ScrollToControl != null) { this.Dispatcher.BeginInvoke (delegate { ScrollToControl (FormValidationGrid, new RoutedEventArgs ()); }); }
                
            }

            else if (FormSubmit != null) {

                // TODO: UPDATE FORM WITH NEW FORM DATA

                RequireFormEventArgs eventArgs = new RequireFormEventArgs (FormEditorControl.Form, false);

                FormSubmit (this, eventArgs);

            }

            return;

        }

        private void ButtonSaveAsDraft_Click (object sender, RoutedEventArgs e) {

            if (FormSubmit != null) {

                RequireFormEventArgs eventArgs = new RequireFormEventArgs (FormEditorControl.Form, true);

                FormSubmit (this, eventArgs);

            }

            return;

        }

        #endregion 


        private void FormEditorControl_ScrollToControl (Object sender, RoutedEventArgs e) {

            if (ScrollToControl != null) { ScrollToControl (sender, e); }

        }

    }

    public class RequireFormEventArgs : EventArgs {

        private Server.Application.Form form = null;

        private Boolean saveAsDraft = false;

        public Server.Application.Form Form { get { return form; } }

        public Boolean SaveAsDraft { get { return saveAsDraft; } }

        public RequireFormEventArgs (Server.Application.Form forForm, Boolean asDraft) { 
            
            form = forForm;

            saveAsDraft = asDraft;
            
            return; 
        
        }

    }

}
