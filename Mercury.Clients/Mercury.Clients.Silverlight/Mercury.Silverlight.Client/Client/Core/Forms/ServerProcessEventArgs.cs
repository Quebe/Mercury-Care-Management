using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Mercury.Client.Core.Forms {

    public class ServerProcessEventArgs : EventArgs {

        #region Private Properties

        private ServerProcessRequestType requestType = ServerProcessRequestType.RaiseEvent;

        private Control sourceControl = null;

        private String eventName = String.Empty;

        private Form.ServerProcessCompleted completed = null;


        private DateTime processStartTime = DateTime.Now;

        private DateTime processEndTime = DateTime.Now;

        #endregion 


        #region Public Properties

        public ServerProcessRequestType RequestType { get { return requestType; } set { requestType = value; } }

        public Control SourceControl { get { return sourceControl; } set { sourceControl = value; } }

        public String EventName { get { return eventName; } set { eventName = value; } }

        public Form.ServerProcessCompleted Completed { get { return completed; } set { completed = value; } }


        public DateTime ProcessStartTime { get { return processStartTime; } set { processStartTime = value; } }

        public DateTime ProcessEndTime { get { return processEndTime; } set { processEndTime = value; } }

        #endregion 


        #region Public Events

        
        
        #endregion 



    }

    public enum ServerProcessRequestType { RaiseEvent, DataSourceChanged, ValueChanged }

}
