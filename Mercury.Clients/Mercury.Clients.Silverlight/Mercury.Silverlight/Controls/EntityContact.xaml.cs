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

    public partial class EntityContact : UserControl {

        #region Private Properties

        private Client.Application MercuryApplication = null;

        private WindowManager.WindowManager WindowManager = null;

        private WindowManager.Window window = null;


        private Server.Application.Entity entity = null;

        private Server.Application.EntityContact entityContactInstance = null;

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


        public Int64 EntityId {

            set {

                MercuryApplication.EntityGet (value, true, SetEntityFromId);

            }

        }

        public Server.Application.Entity Entity {

            get { return entity; }

            set {

                if (entity != value) {

                    entity = value;

                    InitializeEntity ();

                }

            }

        }


        public Boolean AllowEditContactDateTime {

            get { return (ContactDateTimeLabel.Visibility == Visibility.Visible); }

            set {

                ContactDateTimeLabel.Visibility = (value) ? Visibility.Visible : Visibility.Collapsed;

                ContactDateTimePanel.Visibility = (value) ? Visibility.Visible : Visibility.Collapsed;

                ContactDate.IsEnabled = value;

                ContactTime.IsEnabled = value;

            }

        }

        public Boolean AllowEditRegarding { get { return !ContactRegardingSelection.IsReadOnly; } set { ContactRegardingSelection.IsReadOnly = !value; } }

        public Boolean AllowCustomRegardingText { get { return ContactRegardingSelection.IsEditable; } set { ContactRegardingSelection.IsEditable = value; } }

        public Boolean AllowCancel { get { return ButtonCancel.IsEnabled; } set { ButtonCancel.IsEnabled = value; } }

        public String RegardingMessage { get { return ContactRegardingSelection.Text; } set { ContactRegardingSelection.Text = value; } }

        public String IntroductionScript { get { return ContactIntroductionScript.Text; } set { ContactIntroductionScript.Text = value; } }

        public Server.Application.EntityContact EntityContactInstance { get { return entityContactInstance; } }

        public event EventHandler<EntityContactEventArgs> Contact;

        #endregion 


        #region Constructors

        public EntityContact () {

            InitializeComponent ();

            ContactDate.SelectedDate = DateTime.Today;

            ContactTime.SelectedTime = DateTime.Now.TimeOfDay;


            // PUT ASSIGNMENT INTO PROTECTED CONSTRUCTOR SO THAT PREVIEW IS AVAILABLE IN DESIGN WINDOW

            if (Application.Current is Mercury.Silverlight.App) {

                MercuryApplication = ((App)Application.Current).MercuryApplication;

                WindowManager = ((Mercury.Silverlight.App)Application.Current).WindowManager;

            }

            return;

        }

        #endregion 


        #region Initializations

        private void SetEntityFromId (Object sender, Server.Application.EntityGetCompletedEventArgs e) {

            Entity = e.Result;

            return;

        }

        private void InitializeEntity () {

            if (entity != null) {

                // EntityName.Text = "Loading Contact Information for: " + entity.Name;

                EntityContactInformationGrid.IsBusy = true;

                MercuryApplication.EntityContactInformationsGet (entity.Id, true, InitializeEntityContactInformations);

            }

            else {

                // EntityName.Text = "Error while loading entity to contact (unknown entity).";

            }

            return;

        }

        private void InitializeEntityContactInformations (Object sender, Server.Application.EntityContactInformationsGetCompletedEventArgs e) {

            if (!e.Result.HasException) {

                System.Collections.ObjectModel.ObservableCollection<Server.Application.EntityContactInformation> itemSource = new System.Collections.ObjectModel.ObservableCollection<Mercury.Server.Application.EntityContactInformation> ();

                foreach (Server.Application.EntityContactInformation currentInformation in e.Result.Collection) {

                    itemSource.Add (currentInformation);

                }


                Server.Application.EntityContactInformation contactInPerson = new Mercury.Server.Application.EntityContactInformation ();

                contactInPerson.ContactType = Mercury.Server.Application.EntityContactType.InPerson;

                itemSource.Add (contactInPerson);


                Server.Application.EntityContactInformation contactByMail = new Mercury.Server.Application.EntityContactInformation ();

                contactByMail.ContactType = Mercury.Server.Application.EntityContactType.ByMail;

                itemSource.Add (contactByMail);


                EntityContactInformationGrid.ItemsSource = itemSource;

                EntityContactInformationGrid.SelectedItem = itemSource[0];


                EntityContactInformationGrid.IsBusy = false;

                // EntityName.Text = "Contact " + Entity.EntityType.ToString () + ": " + Entity.Name;

                ButtonOk.IsEnabled = true;

            }

            else {

                SetValidationMessage (e.Result.Exception.Message);

                ButtonCancel.IsEnabled = true;

            }

            return;

        }

        #endregion 


        #region Button Events

        private void SetValidationMessage (String message) {

            ValidationMessage.Text = message;

            ValidationMessagePanel.Visibility = (!String.IsNullOrEmpty (message)) ? Visibility.Visible : Visibility.Collapsed;

            return;

        }

        private void ButtonOk_Click (object sender, RoutedEventArgs e) {

            SetValidationMessage (String.Empty);


            if (String.IsNullOrEmpty (ContactRegardingSelection.Text)) {

                SetValidationMessage ("** Contact Regarding is a required field.");

                return;

            }

            if ((!ContactDate.SelectedDate.HasValue) || (!ContactTime.SelectedTime.HasValue)) {

                SetValidationMessage ("** Contact Date/Time is a required field.");

                return;

            }

            // TODO: VALIDATE NOT FUTURE DATE/TIME

            if (EntityContactInformationGrid.SelectedItem == null) { 

                SetValidationMessage ("** Contact Method is a required selection.");

                return;

            }


            Server.Application.EntityContactInformation selectedInformation = (Server.Application.EntityContactInformation ) EntityContactInformationGrid.SelectedItem;

            entityContactInstance = new Mercury.Server.Application.EntityContact ();

            entityContactInstance.EntityId = Entity.Id;

            entityContactInstance.EntityContactInformationId = selectedInformation.Id;

            entityContactInstance.ContactDate = ContactDate.SelectedDate.Value;

            entityContactInstance.ContactedByName = MercuryApplication.Session.UserDisplayName;

            entityContactInstance.ContactType = selectedInformation.ContactType;

            entityContactInstance.Direction = (Mercury.Server.Application.ContactDirection) Convert.ToInt32 (((FrameworkElement) ContactDirectionSelection.SelectedItem).Tag);

            entityContactInstance.Regarding = ContactRegardingSelection.Text;

            entityContactInstance.Remarks = ContactRemarks.Text;

            entityContactInstance.Successful = (Convert.ToInt32 (((FrameworkElement) ContactOutcomeSelection.SelectedItem).Tag) == 1);

            entityContactInstance.ContactOutcome = (Mercury.Server.Application.ContactOutcome) Convert.ToInt32 (((FrameworkElement) ContactOutcomeSelection.SelectedItem).Tag);


            if (Contact != null) {

                Contact (this, new EntityContactEventArgs (entityContactInstance));

            }

            return;

        }

        private void ButtonCancel_Click (object sender, RoutedEventArgs e) {

            if (Contact != null) {

                Contact (this, new EntityContactEventArgs ());

            }

            return;

        }

        #endregion

    }

    public class EntityContactEventArgs : EventArgs {

        private Server.Application.EntityContact entityContact = null;

        private Boolean cancel = false;

        public Server.Application.EntityContact EntityContact { get { return entityContact; } }

        public Boolean Cancel { get { return cancel; } }

        public EntityContactEventArgs (Server.Application.EntityContact forEntityContact) { entityContact = forEntityContact; return; }

        public EntityContactEventArgs () { cancel = true; return; }

    }

}
