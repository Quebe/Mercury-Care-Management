using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Forms.Structures {
    
    [Serializable]
    [DataContract (Name = "FormControlSelectionItem")]
    public class SelectionItem {

        #region Private Properties

        [DataMember (Name = "Text")]
        protected String text = String.Empty;

        [DataMember (Name = "Value")]
        protected String value = String.Empty;

        [DataMember (Name = "Enabled")]
        protected Boolean enabled = true;

        [DataMember (Name = "Selected")]
        protected Boolean selected = false;


        private Controls.Selection selectionControl = null; // OWNER

        #endregion


        #region Public Properties

        public String Text { get { return text; } set { text = value; } }

        public String Value { get { return value; } set { this.value = value; } }

        public Boolean Enabled { get { return enabled; } set { enabled = value; } }

        public Boolean Selected { get { return selected; } set { selected = value; } }


        public Controls.Selection SelectionControl { get { return selectionControl; } set { selectionControl = value; } }

        #endregion

    }

}
