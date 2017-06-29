using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Workflows.UserInteractions.Response {

    [Serializable]
    [DataContract (Name = "WorkflowUserInteractionResponsePromptUser")]
    public class PromptUserResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "ButtonClicked")]
        private Enumerations.UserPromptButtonClicked buttonClicked = Mercury.Server.Workflows.UserInteractions.Enumerations.UserPromptButtonClicked.None;

        [DataMember (Name = "InputText")]
        private String inputText = String.Empty;

        [DataMember (Name = "SelectedValue")]
        private String selectedValue = String.Empty;

        [DataMember (Name = "SelectedText")]
        private String selectedText = String.Empty;

        #endregion 


        #region Public Properties

        public Enumerations.UserPromptButtonClicked ButtonClicked { get { return buttonClicked; } set { buttonClicked = value; } }

        public String InputText { get { return inputText; } set { inputText = value; } }

        public String SelectedValue { get { return selectedValue; } set { selectedValue = value; } }

        public String SelectedText { get { return selectedText; } set { selectedText = value; } }

        #endregion 

    }

}
