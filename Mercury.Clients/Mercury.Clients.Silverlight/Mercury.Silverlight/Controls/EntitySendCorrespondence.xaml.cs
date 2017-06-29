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

    public partial class EntitySendCorrespondence : UserControl {

        #region Private Properties

        private Client.Application MercuryApplication = null;

        private WindowManager.WindowManager WindowManager = null;

        private WindowManager.Window window = null;


        private Server.Application.Entity entity = null;

        private Server.Application.EntityCorrespondence entityCorrespondence = null;

        private Int64 correspondenceId = 0;

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

        public Int64 CorrespondenceId { 
            
            get { return correspondenceId; } 
            
            set {

                correspondenceId = value;

                CorrespondenceSelection.SelectedValue = value; 
            
            } 
        
        }


        public String Attention { get { return CorrespondenceAttention.Text; } set { CorrespondenceAttention.Text = value; } }

        public Boolean AllowFutureSendDate {

            get { return (CorrespondenceSendDate.SelectableDateEnd == DateTime.Today); }

            set {

                CorrespondenceSendDate.SelectableDateEnd = (value) ? new DateTime (9999, 12, 31) : DateTime.Today;

                CorrespondenceSendDate.IsReadOnly = !value;

                if (!value) { CorrespondenceSendDate.SelectedDate = DateTime.Today; }

            }

        }

        public Boolean AllowAlternateAddress { get { return CorrespondenceUseAlternativeAddress.IsEnabled; } set { CorrespondenceUseAlternativeAddress.IsEnabled = value; } }

        public DateTime SendDate { get { return (CorrespondenceSendDate.SelectedDate.HasValue) ? CorrespondenceSendDate.SelectedDate.Value : new DateTime (); } set { CorrespondenceSendDate.SelectedDate = value; } }

        public Boolean AllowCancel { get { return ButtonCancel.IsEnabled; } set { ButtonCancel.IsEnabled = value; } }

        public Boolean AllowUserSelection { get { return CorrespondenceSelection.IsEnabled; } set { CorrespondenceSelection.IsEnabled = value; } }

        public Client.Core.Entity.EntityAddress AlternateAddress {

            set {

                if (value == null) {

                    CorrespondenceUseAlternativeAddress.IsChecked = false;

                }

                else {

                    CorrespondenceAlternateAddressLine1.Text = value.Line1;

                    CorrespondenceAlternateAddressLine2.Text = value.Line2;

                    CorrespondenceAlternateAddressCity.Text = value.City;

                    CorrespondenceAlternateAddressState.Text = value.State;

                    CorrespondenceAlternateAddressZipCode.Text = value.ZipCode;

                    if (AllowAlternateAddress) {

                        CorrespondenceUseAlternativeAddress.IsChecked = true;

                    }

                }

            }

        }

        public Server.Application.EntityCorrespondence EntityCorrespondence { get { return entityCorrespondence; } }

        public event EventHandler<EntitySendCorrespondenceEventArgs> SendCorrespondence;

        #endregion 


        #region Constructors

        public EntitySendCorrespondence () {

            InitializeComponent ();


            // PUT ASSIGNMENT INTO PROTECTED CONSTRUCTOR SO THAT PREVIEW IS AVAILABLE IN DESIGN WINDOW

            if (Application.Current is Mercury.Silverlight.App) {

                MercuryApplication = ((App)Application.Current).MercuryApplication;

                WindowManager = ((Mercury.Silverlight.App)Application.Current).WindowManager;

            }


            CorrespondenceSendDate.SelectedDate = DateTime.Today;

            MercuryApplication.StateReference (true, InitializeStateReference);

            MercuryApplication.CorrespondencesAvailable (true, InitializeCorrespondenceSelection);

            return;

        }

        #endregion 


        #region Initializations

        private void InitializeStateReference (Object sender, Server.Application.StateReferenceCompletedEventArgs e) {

            CorrespondenceAlternateAddressState.ItemsSource = e.Result.ResultList;

            return;

        }

        private void InitializeCorrespondenceSelection (Object sender, Server.Application.CorrespondencesAvailableCompletedEventArgs e) {

            if (e.Result.HasException) {

                SetValidationMessage (e.Result.Exception.Message);

                ButtonCancel.IsEnabled = true;

                return;

            }


            CorrespondenceSelection.ItemsSource = e.Result.Collection;

            CorrespondenceSelection.DisplayMemberPath = "Name";

            CorrespondenceSelection.SelectedValuePath = "CorrespondenceId";

            CorrespondenceSelection.SelectedValue = correspondenceId;

            return;

        }

        private void SetEntityFromId (Object sender, Server.Application.EntityGetCompletedEventArgs e) {

            Entity = e.Result;

            return;

        }

        private void InitializeEntity () {

            if (entity != null) {

                EntityName.Text = "Loading Address Information for: " + entity.Name;

                EntityAddressGrid.IsBusy = true;

                MercuryApplication.EntityAddressesGet (entity.Id, true, InitializeEntityAddress);

            }

            else {

                EntityName.Text = "Error while loading entity to contact (unknown entity).";

            }

            return;

        }

        private void InitializeEntityAddress (Object sender, Server.Application.EntityAddressesGetCompletedEventArgs e) {

            if (!e.Result.HasException) {

                System.Collections.ObjectModel.ObservableCollection<Client.Core.Entity.EntityAddress> itemSource = new System.Collections.ObjectModel.ObservableCollection<Mercury.Client.Core.Entity.EntityAddress> ();

                foreach (Server.Application.EntityAddress currentAddress in e.Result.Collection) {

                    if ((currentAddress.EffectiveDate <= DateTime.Today) && (currentAddress.TerminationDate >= DateTime.Today)) {

                        itemSource.Add (new Client.Core.Entity.EntityAddress (MercuryApplication, currentAddress));

                    }

                }

                EntityAddressGrid.ItemsSource = itemSource;

                if (itemSource.Count > 0) { EntityAddressGrid.SelectedItem = itemSource[0]; }

                EntityAddressGrid.IsBusy = false;

                EntityName.Text = "Send Correspondence: " + Entity.Name;

                ButtonOk.IsEnabled = true;

            }

            else {

                SetValidationMessage (e.Result.Exception.Message);

                ButtonCancel.IsEnabled = true;

            }

            return;

        }

        #endregion


        #region Control Events

        private void CorrespondenceUseAlternativeAddress_Toggle (object sender, RoutedEventArgs e) {

            CorrespondenceAlternateAddressDetail.Visibility = (CorrespondenceUseAlternativeAddress.IsChecked.Value) ? Visibility.Visible : Visibility.Collapsed;

            return;

        }

        private void CorrespondenceUseAlternativeAddressLabel_MouseLeftButtonUp (object sender, MouseButtonEventArgs e) {

            CorrespondenceUseAlternativeAddress.IsChecked = !CorrespondenceUseAlternativeAddress.IsChecked;

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


            if ((!CorrespondenceSendDate.SelectedDate.HasValue)) {

                SetValidationMessage ("** Correspondence Send Date is a required field.");

                return;

            }

            // TODO: VALIDATE NOT FUTURE DATE/TIME

            if (EntityAddressGrid.SelectedItem == null) {

                SetValidationMessage ("** Address is a required selection.");

                return;

            }


            entityCorrespondence = new Mercury.Server.Application.EntityCorrespondence ();

            entityCorrespondence.EntityId = entity.Id;

            entityCorrespondence.CorrespondenceId = Convert.ToInt64 (CorrespondenceSelection.SelectedValue);

            entityCorrespondence.CorrespondenceName = ((Server.Application.Correspondence) CorrespondenceSelection.SelectedItem).Name;

            entityCorrespondence.ReadyToSendDate = CorrespondenceSendDate.SelectedDate.Value;

            entityCorrespondence.Attention = CorrespondenceAttention.Text;

            if (!CorrespondenceUseAlternativeAddress.IsChecked.Value) {

                Client.Core.Entity.EntityAddress selectedAddress = ((Client.Core.Entity.EntityAddress)EntityAddressGrid.SelectedItem);

                entityCorrespondence.EntityAddressId = selectedAddress.Id;

                entityCorrespondence.AddressLine1 = selectedAddress.Line1;

                entityCorrespondence.AddressLine2 = selectedAddress.Line2;

                entityCorrespondence.AddressCity = selectedAddress.City;

                entityCorrespondence.AddressState = selectedAddress.State;

                entityCorrespondence.AddressZipCode = selectedAddress.ZipCode;
                
            }

            else {

                entityCorrespondence.EntityAddressId = 0;

                entityCorrespondence.AddressLine1 = CorrespondenceAlternateAddressLine1.Text;

                entityCorrespondence.AddressLine2 = CorrespondenceAlternateAddressLine2.Text;

                entityCorrespondence.AddressCity = CorrespondenceAlternateAddressCity.Text;

                entityCorrespondence.AddressState = CorrespondenceAlternateAddressState.Text;

                entityCorrespondence.AddressZipCode = CorrespondenceAlternateAddressZipCode.Text;

            }

            if (SendCorrespondence != null) {

                SendCorrespondence (this, new EntitySendCorrespondenceEventArgs (entityCorrespondence));

            }

            return;

        }

        private void ButtonCancel_Click (object sender, RoutedEventArgs e) {

            if (SendCorrespondence != null) {

                SendCorrespondence (this, new EntitySendCorrespondenceEventArgs ());

            }

            return;

        }

        #endregion

    }

    public class EntitySendCorrespondenceEventArgs : EventArgs {

        private Server.Application.EntityCorrespondence entityCorrespondence = null;

        private Boolean cancel = false;

        public Server.Application.EntityCorrespondence EntityCorrespondence { get { return entityCorrespondence; } }

        public Boolean Cancel { get { return cancel; } }

        public EntitySendCorrespondenceEventArgs (Server.Application.EntityCorrespondence forEntityCorrespondence) { entityCorrespondence = forEntityCorrespondence; return; }

        public EntitySendCorrespondenceEventArgs () { cancel = true; return; }

    }

}