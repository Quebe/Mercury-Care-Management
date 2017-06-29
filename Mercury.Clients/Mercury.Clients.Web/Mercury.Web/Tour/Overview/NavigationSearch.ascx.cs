using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Tour.Overview {

    public partial class NavigationSearch : System.Web.UI.UserControl, ITourControl {


        #region Interface Public Properties

        public Boolean HasNext { 
            
            get {

                Boolean nextPageAvailable = false;

                switch (Page.GetType ().ToString ()) {

                    case "ASP.application_workspace_workspace_aspx":

                        nextPageAvailable = true;

                        break;

                }
                
                return nextPageAvailable; 
            
            } 
        
        }

        public Boolean HasPrevious { get { return true; } }

        public Boolean HasPageSpecificTour { get { return false; } }

        #endregion


        #region Interface Events

        public event Tour.TourControlPageChangedEventHandler TourPageChanged;

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            return;

        }

        #endregion


        #region Tour Events

        private void RaiseTourPageChanged (String tourUserControl, String focusControlClientId) {

            Tour.TourControlPageChangedEventArgs e = new TourControlPageChangedEventArgs (tourUserControl, focusControlClientId);

            if (TourPageChanged != null) { TourPageChanged (this, e); }

            return;

        }

        public void TourPrevious_OnClick (Object sender, EventArgs e) {

            String tourUserControl = "/Tour/Overview/NavigationLogout.ascx";

            String focusControlClientId = "ApplicationTitleBarLogoutLink";

            RaiseTourPageChanged (tourUserControl, focusControlClientId);

            return;

        }

        public void TourNext_OnClick (Object sender, EventArgs e) {

            String tourUserControl = String.Empty;

            String focusControlClientId = String.Empty;


            switch (Page.GetType ().ToString ()) {

                case "ASP.application_workspace_workspace_aspx":
                    
                    tourUserControl = "/Tour/Workspace/Introduction.ascx";

                    focusControlClientId = "ApplicationContent";

                    break;

            }


            if (!String.IsNullOrWhiteSpace (tourUserControl)) {

                RaiseTourPageChanged (tourUserControl, focusControlClientId);

            }

            return;

        }

        #endregion

    }

}