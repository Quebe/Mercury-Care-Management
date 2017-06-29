using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Core.Action.EventArguments {
    
    public class EventArguments {

        #region Private Properties

        private Dictionary<String, Object> arguments = new Dictionary<String, Object> ();

        #endregion


        #region Public Properties

        public Dictionary<String, Object> Arguments { get { return arguments; } set { arguments = value; } }

        #endregion

    }

}
