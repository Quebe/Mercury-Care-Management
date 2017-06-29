using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Workflows.UserInteractions.Request {

    [Serializable]
    public class RequestEventArgs : System.EventArgs {

        #region Private Properties

        private RequestBase userInteractionRequest = new RequestBase ();

        #endregion


        #region Public Properties

        public RequestBase Request { get { return userInteractionRequest; } set { userInteractionRequest = value; } }

        #endregion


        #region Constructors

        public RequestEventArgs (RequestBase request) {

            userInteractionRequest = request;

            return;

        }

        #endregion

    }

}
