using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Mercury.Silverlight.WindowManager {

    public partial class WindowOld : UserControl {

        #region Private Properties

        private Guid windowHandle = Guid.NewGuid ();

        protected String title = String.Empty;

        private Dictionary<String, Object> parameters = new Dictionary<String, Object> ();

        private Boolean isMinimized = false;

        private System.Windows.Media.Imaging.WriteableBitmap screenCapture;

        #endregion 


        #region Public Properties

        public Guid WindowHandle { get { return windowHandle; } }

        virtual public String WindowType { get { return "Window"; } }

        public String Title { get { return title; } }

        virtual public Dictionary<String, Object> Parameters { get { return parameters; } set { parameters = value; } }

        public Boolean IsMinimized { get { return isMinimized; } }

        public System.Windows.Media.Imaging.WriteableBitmap ScreenCapture { 
            
            get { return screenCapture; } 

            //get {

            //    Measure (new Size (800, 600));

            //    Arrange (new Rect (0, 0, 800, 600));

            //    System.Windows.Media.Imaging.WriteableBitmap capture = new System.Windows.Media.Imaging.WriteableBitmap (this, null);

            //    return capture;

            //}
            
            
            set { screenCapture = value; } }

        #endregion 
        

        #region Public Events

        public event EventHandler<System.EventArgs> OnClose;

        public event EventHandler<System.EventArgs> OnMinimize;

        public event EventHandler<System.EventArgs> OnMaximize;

        public event EventHandler<System.EventArgs> OnGlobalProgressBarShow;

        public event EventHandler<System.EventArgs> OnGlobalProgressBarHide;

        #endregion 


        #region Constructors

        public WindowOld () {

            InitializeComponent ();

            return;

        }

        #endregion 


        #region Public Methods

        public virtual void WindowCommand_Activated () {

            return;

        }

        public virtual void WindowCommand_Deactivated () {

            return;

        }

        public void WindowCommand_Close () {

            if (OnClose != null) {

                OnClose (this, new EventArgs ());

            }

            GlobalProgressBarHide ();

            return;

        }

        public void WindowCommand_Minimize () {

            isMinimized = true;

            if (OnMinimize != null) {

                OnMinimize (this, new EventArgs ());

            }

            return;

        }

        public void WindowCommand_Maximize () {

            isMinimized = false;

            if (OnMaximize != null) {

                OnMaximize (this, new EventArgs ());

            }

            return;

        }

        public void GlobalProgressBarShow () {

            if (OnGlobalProgressBarShow != null) {

                OnGlobalProgressBarShow (this, new EventArgs ());

            }

            return;

        }

        public void GlobalProgressBarHide () {

            if (OnGlobalProgressBarHide != null) {

                OnGlobalProgressBarHide (this, new EventArgs ());

            }

            return;

        }

        #endregion 
        

        #region Exception Handling

        public void SetExceptionMessage (String forMessage) {

            Border ExceptionContainer = (Border) FindName ("ExceptionContainer");

            TextBlock ExceptionMessage = (TextBlock) FindName ("ExceptionMessage");

            if ((ExceptionContainer == null) || (ExceptionMessage == null)) { return; }


            ExceptionContainer.Visibility = (!String.IsNullOrEmpty (forMessage)) ? Visibility.Visible : Visibility.Collapsed;

            ExceptionMessage.Text = forMessage;

            return;

        }

        public Boolean SetExceptionMessage (System.ComponentModel.AsyncCompletedEventArgs e) {

            String message = String.Empty;

            if (e.Cancelled) { message = "Server Communication Canceled"; }

            if (e.Error != null) {

                message = ((message.Length > 0) ? message : "Server Communication Error") + ": " + e.Error.Message;

            }

            if (!String.IsNullOrEmpty (message)) { SetExceptionMessage (message); }

            return (!String.IsNullOrEmpty (message));

        }

        public Boolean SetExceptionMessage (Server.Application.ResponseBase response) {

            String message = String.Empty;

            if (response.HasException) { message = response.Exception.Message; }

            if (!String.IsNullOrEmpty (message)) { SetExceptionMessage (message); }

            return (!String.IsNullOrEmpty (message));

        }

        //public Boolean SetExceptionMessage (Server.Application.ServiceException serviceException) {

        //    String message = String.Empty;

        //    if (serviceException != null) { message = serviceException.Message; }

        //    if (!String.IsNullOrEmpty (message)) { SetExceptionMessage (message); }

        //    return (!String.IsNullOrEmpty (message));

        //}

        public Boolean SetExceptionMessage (Object forObject, String description) {

            String message = String.Empty;

            if (forObject == null) { message = "Unable to successfully retreive " + description + "."; }

            if (!String.IsNullOrEmpty (message)) { SetExceptionMessage (message); }

            return (!String.IsNullOrEmpty (message));

        }

        #endregion 

    }

}
