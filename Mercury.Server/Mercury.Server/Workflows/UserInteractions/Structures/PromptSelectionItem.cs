using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Workflows.UserInteractions.Structures {

    [Serializable]
    [DataContract (Name= "WorkflowUserInteractionRequestPromptSelectionItem")]
    public class PromptSelectionItem {

        #region Private Properties

        [DataMember (Name = "Text")]
        protected String text = String.Empty;

        [DataMember (Name = "Value")]
        protected String value = String.Empty;

        [DataMember (Name = "Enabled")]
        protected Boolean enabled = true;

        [DataMember (Name = "Selected")]
        protected Boolean selected = false;

        #endregion


        #region Public Properties

        public String Text { get { return text; } set { text = value; } }

        public String Value { get { return value; } set { this.value = value; } }

        public Boolean Enabled { get { return enabled; } set { enabled = value; } }

        public Boolean Selected { get { return selected; } set { selected = value; } }

        #endregion


        #region Constructors

        public PromptSelectionItem (String forText, String forValue) {

            text = forText;

            value = forValue;

            enabled = true;

            selected = false;

            return;

        }

        public PromptSelectionItem (String forText, String forValue, Boolean forEnabled) {

            text = forText;

            value = forValue;

            enabled = forEnabled;

            selected = false;

            return;

        }

        #endregion 

    }

}
