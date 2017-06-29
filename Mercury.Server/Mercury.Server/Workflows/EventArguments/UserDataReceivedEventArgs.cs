using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Workflow.Activities;

namespace Mercury.Server.Workflows.EventArguments {

    [Serializable]
    public class UserDataReceivedEventArgs : ExternalDataEventArgs {

        private Dictionary<String, Object> arguments;

        public Dictionary<String, Object> Arguments {

            get { return arguments; }

            set { arguments = value; }

        }

        public UserDataReceivedEventArgs (Guid workflowInstanceId, Dictionary<String, Object> arguments) : base (workflowInstanceId) {

            this.arguments = arguments;

        }

    }

}
