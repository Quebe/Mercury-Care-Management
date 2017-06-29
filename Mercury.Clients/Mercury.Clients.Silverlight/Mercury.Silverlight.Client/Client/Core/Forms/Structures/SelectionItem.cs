using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Mercury.Client.Core.Forms.Structures {

    public class SelectionItem : System.ComponentModel.INotifyPropertyChanged {

        #region Private Properties

        private Controls.Selection selectionControl = null;

        private String text = String.Empty;

        private String value = String.Empty;

        private Boolean enabled = true;

        private Boolean selected = false;

        #endregion


        #region Public Properties

        public Controls.Selection SelectionControl { get { return selectionControl; } set { selectionControl = value; } }


        public String Text { 
            
            get { return text; } 
            
            set {

                if (text != value) {

                    text = value;

                    NotifyPropertyChanged ("Text");

                }
            
            } 
        
        }

        public String Value { 
            
            get { return value; } 
            
            set {

                if (this.value != value) {

                    this.value = value;

                    NotifyPropertyChanged ("Value");

                }
            
            } 
        
        }

        public Boolean Enabled { 
            
            get { return enabled; } 
            
            set {

                if (enabled != value) {

                    enabled = value;

                    NotifyPropertyChanged ("Enabled");

                    NotifyPropertyChanged ("IsEnabled");

                }
                
            } 
            
        }

        public Boolean Selected { 
            
            get { return selected; } 
            
            set {

                if (selected != value) {

                    selected = value;

                    NotifyPropertyChanged ("Selected");

                }
            
            } 
        
        }


        public Boolean IsEnabled {

            get {

                if (selectionControl == null) { return enabled; }

                return ((selectionControl.Enabled) && (!selectionControl.ReadOnly) && (enabled));

            }

        }

        #endregion


        #region Constructors

        public SelectionItem () { return; }

        public SelectionItem (Server.Application.FormControlSelectionItem serverSelectionItem) {

            text = serverSelectionItem.Text;

            value = serverSelectionItem.Value;

            enabled = serverSelectionItem.Enabled;

            selected = serverSelectionItem.Selected;

            return;

        }

        public SelectionItem (Controls.Selection forSelectionControl, Server.Application.FormControlSelectionItem serverSelectionItem) {

            selectionControl = forSelectionControl;

            text = serverSelectionItem.Text;

            value = serverSelectionItem.Value;

            enabled = serverSelectionItem.Enabled;

            selected = serverSelectionItem.Selected;

            return;

        }

        public SelectionItem (String forText, String forValue, Boolean forEnabled, Boolean forSelected) {

            Text = forText;

            Value = forValue;

            Enabled = forEnabled;

            Selected = forSelected;

            return;

        }

        #endregion


        #region Public Methods

        public Server.Application.FormControlSelectionItem ToServerItem () {

            Server.Application.FormControlSelectionItem serverItem = new Server.Application.FormControlSelectionItem ();

            serverItem.Text = text;

            serverItem.Value = value;

            serverItem.Enabled = enabled;

            serverItem.Selected = selected;

            return serverItem;

        }

        #endregion 
        

        #region System.ComponentModel.INotifyPropertyChanged

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged (String propertyName) {

            if (PropertyChanged != null) {

                PropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (propertyName));

            }

            return;

        }

        #endregion

    }

}
