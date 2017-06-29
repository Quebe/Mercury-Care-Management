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

namespace Mercury.Silverlight.Forms.FormEditor {

    public partial class FormEditor : UserControl {

        #region Private Properties

        private Client.Application MercuryApplication = ((App) Application.Current).MercuryApplication;

        private Client.Core.Forms.Form form = null;

        private FormRenderEngine renderEngine;

        #endregion 


        #region Public Properties

        public Server.Application.Form Form {

            set {

                form = new Mercury.Client.Core.Forms.Form (MercuryApplication, value);

                RenderForm ();

            }

            get { return (Server.Application.Form) form.ToServerObject (); }

        }

        public event EventHandler<RoutedEventArgs> ScrollToControl;

        #endregion 


        #region Constructors

        public FormEditor () {

            InitializeComponent ();

            renderEngine = new FormRenderEngine (this);

            return;

        }

        #endregion 


        #region Public Methods

        public void RenderForm () {

            FormContent.Child = renderEngine.RenderForm (form);

            return;

        }

        #endregion 


        #region Control Events

        private FrameworkElement FindControlPanel (FrameworkElement forControl) {

            if (forControl == null) { return null; }

            if (forControl.Name.EndsWith ("_Panel")) { return forControl; }

            else { return FindControlPanel ((FrameworkElement) forControl.Parent); }

        }

        public void Control_GotFocus (Object sender, RoutedEventArgs e) {

            if (ScrollToControl != null) { ScrollToControl (sender, e); }

            return;

        }

        public void FormControlAddress_StateReferenceCompleted (Object sender, Server.Application.StateReferenceCompletedEventArgs e) {

            // FINISH LINKING REFERENCE CONTROLS

            foreach (Client.Core.Forms.Control currentControl in form.GetAllControls ()) {

                if (currentControl.ControlType == Mercury.Server.Application.FormControlType.Address) {

                    Client.Core.Forms.Controls.Address addressControl = (Client.Core.Forms.Controls.Address) currentControl;

                    Telerik.Windows.Controls.RadComboBox stateComboBox = (Telerik.Windows.Controls.RadComboBox) FindName (addressControl.ControlId.ToString () + "_State");

                    if (stateComboBox != null) {

                        stateComboBox.ItemsSource = e.Result.ResultList;

                        // stateComboBox.SelectedValue = addressControl.State;

                        stateComboBox.SetBinding (Telerik.Windows.Controls.RadComboBox.SelectedValueProperty, renderEngine.PropertyDataBinding ("State", addressControl, System.Windows.Data.BindingMode.OneWay));

                    }

                }

            }

            return;

        }

        public void FormControlAddress_CityReferenceByStateCompleted (Object sender, Server.Application.CityReferenceByStateCompletedEventArgs e) {

            // RELINK CITY REFERENCES 

            foreach (Client.Core.Forms.Control currentControl in form.GetAllControls ()) {

                if (currentControl.ControlType == Mercury.Server.Application.FormControlType.Address) {

                    Client.Core.Forms.Controls.Address addressControl = (Client.Core.Forms.Controls.Address) currentControl;

                    Telerik.Windows.Controls.RadComboBox cityComboBox = (Telerik.Windows.Controls.RadComboBox) FindName (addressControl.ControlId.ToString () + "_City");

                    Telerik.Windows.Controls.RadComboBox stateComboBox = (Telerik.Windows.Controls.RadComboBox) FindName (addressControl.ControlId.ToString () + "_State");

                    if ((stateComboBox != null) && (cityComboBox != null)) {

                        cityComboBox.ItemsSource = e.Result.Collection;

                        cityComboBox.DisplayMemberPath = "City";

                        cityComboBox.SelectedValuePath = "City";

                        cityComboBox.SetBinding (Telerik.Windows.Controls.RadComboBox.SelectedValueProperty, renderEngine.PropertyDataBinding ("City", addressControl, System.Windows.Data.BindingMode.OneWay));

                    }

                }

            }

            return;

        }

        public void FormControlAddress_CityStateReferenceByZipCodeCompleted (Object sender, Server.Application.CityStateReferenceByZipCodeCompletedEventArgs e) {

            // RELINK CITY REFERENCES 

            foreach (Client.Core.Forms.Control currentControl in form.GetAllControls ()) {

                if (currentControl.ControlType == Mercury.Server.Application.FormControlType.Address) {

                    Client.Core.Forms.Controls.Address addressControl = (Client.Core.Forms.Controls.Address) currentControl;

                    if (e.Result.ZipCode == addressControl.ZipCode) {

                        Telerik.Windows.Controls.RadComboBox cityComboBox = (Telerik.Windows.Controls.RadComboBox) FindName (addressControl.ControlId.ToString () + "_City");

                        Telerik.Windows.Controls.RadComboBox stateComboBox = (Telerik.Windows.Controls.RadComboBox) FindName (addressControl.ControlId.ToString () + "_State");

                        if ((stateComboBox != null) && (cityComboBox != null) && (e.Result != null)) {

                            addressControl.State = e.Result.State;

                            addressControl.City = e.Result.City;

                        }
                        
                    }

                }

            }

            return;

        }

        public void FormControlAddress_LostFocus (Object sender, RoutedEventArgs e) {

            Client.Core.Forms.Controls.Address addressControl = (Client.Core.Forms.Controls.Address) ((FrameworkElement) sender).Tag;

            if (addressControl == null) { return; }


            Boolean valueChanged = false;


            #region Address Lines 1/2

            if (sender is TextBox) { // ADDRESS LINE 1/2

                TextBox addressLine = (TextBox) sender;

                if (addressLine.Name.Contains ("_Line1")) {

                    if (addressControl.Line1 != addressLine.Text) {

                        addressControl.Line1 = addressLine.Text;

                        addressControl.AddressId = 0;

                        valueChanged = true;

                    }

                }

                else if (addressLine.Name.Contains ("_Line2")) {

                    if (addressControl.Line2 != addressLine.Text) {

                        addressControl.Line2 = addressLine.Text;

                        addressControl.AddressId = 0;

                        valueChanged = true;

                    }

                }

            }

            #endregion


            #region Zip Code Change

            else if (sender is Telerik.Windows.Controls.RadMaskedTextBox) { // ZIP CODE 

                Telerik.Windows.Controls.RadMaskedTextBox addressZipCode = (Telerik.Windows.Controls.RadMaskedTextBox) sender;

                if (addressZipCode.Value.ToString () != addressControl.ZipCode) {

                    addressControl.ZipCode = addressZipCode.Value.ToString ();

                    addressControl.AddressId = 0;

                    if (addressControl.ZipCode.Length == 5) {

                        MercuryApplication.CityStateReferenceByZipCode (addressControl.ZipCode, true, FormControlAddress_CityStateReferenceByZipCodeCompleted);

                    }

                    addressZipCode.SetBinding (Telerik.Windows.Controls.RadMaskedTextBox.ValueProperty, renderEngine.PropertyDataBinding ("ZipCode", addressControl, System.Windows.Data.BindingMode.OneWay));

                    valueChanged = true;

                }


            }

            #endregion

            if (valueChanged) {

                if ((addressControl.HasEventHandler ("AddressChanged")) || (addressControl.HasDependencyDataBinding)) {

                    FormServerProcessing_ControlPanelDisable (addressControl, Mercury.Client.Core.Forms.ServerProcessRequestType.ValueChanged);

                    form.ValueChanged (addressControl, FormServerProcessCompleted);

                }

            }

            return;

        }

        public void FormControlAddress_CitySelectionChanged (object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e) {

            Telerik.Windows.Controls.RadComboBox cityComboBox = (Telerik.Windows.Controls.RadComboBox) sender;

            if (cityComboBox == null) { return; }

            if (cityComboBox.SelectedValue == null) { return; }

            Client.Core.Forms.Controls.Address addressControl = (Client.Core.Forms.Controls.Address) cityComboBox.Tag;

            if (addressControl == null) { return; }


            Boolean valueChanged = (cityComboBox.SelectedValue.ToString () != addressControl.City);

            valueChanged = ((valueChanged) && (addressControl.EnabledAndNotReadOnly));


            if (valueChanged) {

                addressControl.City = cityComboBox.SelectedValue.ToString ();

                addressControl.AddressId = 0;

                cityComboBox.SetBinding (Telerik.Windows.Controls.RadComboBox.SelectedValueProperty, renderEngine.PropertyDataBinding ("City", addressControl, System.Windows.Data.BindingMode.OneWay));                

                if ((addressControl.HasEventHandler ("AddressChanged")) || (addressControl.HasDependencyDataBinding)) {

                    FormServerProcessing_ControlPanelDisable (addressControl, Mercury.Client.Core.Forms.ServerProcessRequestType.ValueChanged);

                    form.ValueChanged (addressControl, FormServerProcessCompleted);

                }

            }

            return;

        }

        public void FormControlAddress_StateSelectionChanged (object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e) {

            Telerik.Windows.Controls.RadComboBox stateComboBox = (Telerik.Windows.Controls.RadComboBox) sender;

            if (stateComboBox == null) { return; }

            if (stateComboBox.SelectedValue == null) { return; }

            Client.Core.Forms.Controls.Address addressControl = (Client.Core.Forms.Controls.Address) stateComboBox.Tag;

            if (addressControl == null) { return; }


            Boolean valueChanged = (stateComboBox.SelectedValue.ToString () != addressControl.State);

            valueChanged = ((valueChanged) && (addressControl.EnabledAndNotReadOnly));


            if (valueChanged) {

                addressControl.State = stateComboBox.SelectedValue.ToString ();

                addressControl.AddressId = 0;

                stateComboBox.SetBinding (Telerik.Windows.Controls.RadComboBox.SelectedValueProperty, renderEngine.PropertyDataBinding ("State", addressControl, System.Windows.Data.BindingMode.OneWay));

                if ((addressControl.HasEventHandler ("AddressChanged")) || (addressControl.HasDependencyDataBinding)) {

                    FormServerProcessing_ControlPanelDisable (addressControl, Mercury.Client.Core.Forms.ServerProcessRequestType.ValueChanged);

                    form.ValueChanged (addressControl, FormServerProcessCompleted);

                }

            }

            // MAKE SURE TO UPDATE CITY REFERENCE (INDEPENDENT OF CHANGE)

            // THIS COULD BE FROM A POSTBACK/SERVER-UPDATE THAT NEEDS TO RESET THE STATE TO MAINTAIN THE CITY INFORMATION

            MercuryApplication.CityReferenceByState (addressControl.State, true, FormControlAddress_CityReferenceByStateCompleted);

            return;

        }

        public void FormControlSelection_CheckRadioChanged (Object sender, RoutedEventArgs e) {

            System.Windows.Controls.Primitives.ToggleButton item = null;

            Boolean selectionChanged = false;

            if ((sender is System.Windows.Controls.Primitives.ToggleButton) && (sender != null)) {

                item = (System.Windows.Controls.Primitives.ToggleButton) sender;

                if ((item.Tag is Client.Core.Forms.Structures.SelectionItem) && (item.Tag != null)) {

                    Client.Core.Forms.Structures.SelectionItem selectionItem = (Client.Core.Forms.Structures.SelectionItem) item.Tag;

                    if (item.IsChecked.HasValue) {

                        selectionChanged = (selectionItem.Selected != item.IsChecked.Value);

                        selectionItem.SelectionControl.SetItemSelectionManual (selectionItem.Value, item.IsChecked.Value);

                    }

                    else {

                        selectionChanged = (selectionItem.Selected);

                        selectionItem.SelectionControl.SetItemSelectionManual (selectionItem.Value, item.IsChecked.Value);
                    
                    }

                    item.SetBinding (System.Windows.Controls.Primitives.ToggleButton.IsCheckedProperty, renderEngine.PropertyDataBinding ("Selected", selectionItem, System.Windows.Data.BindingMode.OneWay));

                    if (selectionChanged) {
                        
                        if ((selectionItem.SelectionControl.HasEventHandler ("SelectionChanged")) || (selectionItem.SelectionControl.HasDependencyDataBinding)) {

                            FormServerProcessing_ControlPanelDisable (selectionItem.SelectionControl, Mercury.Client.Core.Forms.ServerProcessRequestType.ValueChanged);

                            form.ValueChanged (selectionItem.SelectionControl, FormServerProcessCompleted);

                        }

                    }

                }

            }
            
            return;

        }

        public void FormControlService_ServiceDateChanged (Object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e) {

            Telerik.Windows.Controls.RadDatePicker serviceDatePicker = (Telerik.Windows.Controls.RadDatePicker) sender;

            Boolean dateChanged = false;

            if ((serviceDatePicker.Tag is Client.Core.Forms.Controls.Service) && (serviceDatePicker.Tag != null)) {

                Client.Core.Forms.Controls.Service serviceControl = (Client.Core.Forms.Controls.Service) serviceDatePicker.Tag;

                if ((serviceControl.ServiceDate.HasValue) && (serviceDatePicker.SelectedDate.HasValue)) {

                    dateChanged = (serviceControl.ServiceDate.Value != serviceDatePicker.SelectedDate.Value);

                }

                else { dateChanged = true; }


                if (dateChanged) {

                    serviceControl.ServiceDate = serviceDatePicker.SelectedDate;

                    if (serviceControl.HasEventHandler ("ServiceDateChanged")) {

                        FormServerProcessing_ControlPanelDisable (serviceControl, Mercury.Client.Core.Forms.ServerProcessRequestType.RaiseEvent);

                        form.RaiseEvent (serviceControl, "ServiceDateChanged", FormServerProcessCompleted);

                    }

                }

            }

            return;

        }

        #endregion 


        #region Form Event Handlers

        private void FormServerProcessing_ControlPanelDisable (Client.Core.Forms.Control control, Client.Core.Forms.ServerProcessRequestType requestType) {

            Grid controlPanel = (Grid) FindName ("FormControl_" + control.ControlId.ToString ().Replace ("-", "") + "_Panel");

            controlPanel.IsHitTestVisible = false;

            controlPanel.Opacity = .5;

            // ((App) App.Current).WindowManager.Window_OnGlobalProgressBarShow (control, new EventArgs ());

            ((App) App.Current).WindowManager.Window_OnGlobalProgressBarShow (control.ControlId.ToString ().Replace ("-", "") + requestType.ToString (), new EventArgs ());

            return;

        }

        private void FormServerProcessing_ControlPanelEnable (Client.Core.Forms.Control control, Client.Core.Forms.ServerProcessRequestType requestType) {

            Grid controlPanel = (Grid) FindName ("FormControl_" + control.ControlId.ToString ().Replace ("-", "") + "_Panel");

            controlPanel.IsHitTestVisible = true;

            controlPanel.Opacity = 1;

            // ((App) App.Current).WindowManager.Window_OnGlobalProgressBarHide (control, new EventArgs ());

            ((App) App.Current).WindowManager.Window_OnGlobalProgressBarHide (control.ControlId.ToString ().Replace ("-", "") + requestType.ToString (), new EventArgs ());

            return;

        }

        public void FormServerProcessCompleted (Object sender, Client.Core.Forms.ServerProcessEventArgs e) {

            FormServerProcessing_ControlPanelEnable (e.SourceControl, e.RequestType);

            return;

        }

        #endregion 

    }

}
