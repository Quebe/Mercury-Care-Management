using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Forms.Structures {

    [Serializable]
    public class SelectionItem {

        #region Private Properties

        private String text = String.Empty;

        private String value = String.Empty;

        private Boolean enabled = true;

        private Boolean selected = false;

        #endregion


        #region Public Properties

        public String Text { get { return text; } set { text = value; } }

        public String Value { get { return value; } set { this.value = value; } }

        public Boolean Enabled { get { return enabled; } set { enabled = value; } }

        public Boolean Selected { get { return selected; } set { selected = value; } }

        #endregion


        #region Constructors

        public SelectionItem () { return; }

        public SelectionItem (Mercury.Server.Application.FormControlSelectionItem serverSelectionItem) {

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

        public Mercury.Server.Application.FormControlSelectionItem ToServerItem () {

            Mercury.Server.Application.FormControlSelectionItem serverItem = new Mercury.Server.Application.FormControlSelectionItem ();

            serverItem.Text = text;

            serverItem.Value = value;

            serverItem.Enabled = enabled;

            serverItem.Selected = selected;

            return serverItem;

        }

        #endregion 

    }

}
