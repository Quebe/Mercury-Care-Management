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

namespace Mercury.Silverlight.Controls {

    public partial class EntityContactInformationHistory : UserControl {

        #region Private Properties

        private Client.Application MercuryApplication = null; 

        private WindowManager.WindowManager WindowManager = null;

        private WindowManager.Window window = null;


        private Int64 entityId;

        #endregion


        #region Public Properties

        public WindowManager.Window Window {

            get {

                if (window != null) { return window; }

                return new WindowManager.Window ();

            }

            set {

                window = value;

            }

        }

        // PUBLIC ACCESSIBLE MEMBER ID PROPERTY FOR SETTING THE MEMBER, THIS CAUSES THE CONTROL TO TRUELY INITIALIZE

        public Int64 EntityId {

            get { return entityId; }

            set {

                // DO NOT COMPARE VALUES, ALLOW RE-ASSIGNMENT TO "REFRESH" DATA

                entityId = value;

                Window.SetExceptionMessage (String.Empty);

                MercuryApplication.EntityContactInformationsGet (entityId, true, InitializeEntityContactInformations);

            }

        }

        #endregion 


        #region Constructors

        public EntityContactInformationHistory () {

            InitializeComponent ();


            // PUT ASSIGNMENT INTO PROTECTED CONSTRUCTOR SO THAT PREVIEW IS AVAILABLE IN DESIGN WINDOW

            if (Application.Current is Mercury.Silverlight.App) {

                MercuryApplication = ((App)Application.Current).MercuryApplication;

                WindowManager = ((Mercury.Silverlight.App)Application.Current).WindowManager;

            }

            return;

        }

        #endregion

        #region Initiailization

        protected void InitializeEntityContactInformations (Object sender, Server.Application.EntityContactInformationsGetCompletedEventArgs e) {

            if (Window.SetExceptionMessage (e)) { return; }

            EntityContactInformationGrid.ItemsSource = Client.Converters.ServerCollectionToClient.EntityContactInformationCollection (MercuryApplication, e.Result.Collection);

            return;

        }

        #endregion

    }

}

