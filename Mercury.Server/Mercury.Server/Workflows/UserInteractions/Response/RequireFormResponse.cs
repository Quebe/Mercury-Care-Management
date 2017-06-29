using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Workflows.UserInteractions.Response {

    [Serializable]
    [DataContract (Name = "WorkflowUserInteractionResponseRequireForm")]
    public class RequireFormResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Form")]
        private Core.Forms.Form form = new Mercury.Server.Core.Forms.Form (null);

        [DataMember (Name = "SaveAsDraft")]
        private Boolean saveAsDraft = false;

        #endregion


        #region Public Properties

        public Server.Core.Forms.Form Form { get { return form; } set { form = value; } }

        public Boolean SaveAsDraft { get { return saveAsDraft; } set { saveAsDraft = value; } }

        #endregion


        #region Constructor

        #endregion
    
    }

}
