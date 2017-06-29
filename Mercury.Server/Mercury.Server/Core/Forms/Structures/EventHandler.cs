using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Forms.Structures {

    [Serializable]
    [DataContract (Name = "FormControlEventHandler")]
    public class EventHandler {

        #region Private Properties

        [DataMember (Name = "EventName")]
        private String eventName = String.Empty;

        [DataMember (Name = "MethodSource")]
        private String methodSource;

        [DataMember (Name = "ExecuteClientSide")]
        private Boolean executeClientSide = true;

        [DataMember (Name = "SmartEvent")]
        private Boolean smartEvent = true;

        #endregion 


        #region Public Properties

        public String EventName { get { return eventName; } set { eventName = value; } }

        public String MethodSource { get { return methodSource; } set { methodSource = value; } }

        public Boolean ExecuteClientSide { get { return executeClientSide; } set { executeClientSide = value; } }

        public Boolean SmartEvent { get { return smartEvent; } set { smartEvent = value; } }

        #endregion

    }

}
