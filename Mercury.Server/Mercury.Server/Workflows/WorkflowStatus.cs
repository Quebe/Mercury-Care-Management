using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Workflows {

    public enum WorkflowStatus {

        Started, Aborted, Completed, Created, Idled, Loaded, Persisted, Resumed, Suspended, Terminated, Unloaded

    }

}
