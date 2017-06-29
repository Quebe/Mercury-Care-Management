using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Tour {

    public partial class Introduction : System.Web.UI.UserControl, ITourControl {

        #region Interface Public Properties

        public Boolean HasNext { get { return true; } }

        public Boolean HasPrevious { get { return false; } }
        
        #endregion


        #region Interface Events

        public event Tour.TourControlPageChangedEventHandler TourPageChanged;
        
        #endregion 


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            switch (Page.GetType ().ToString ()) {

                case "ASP.application_workspace_workspace_aspx":

                case "ASP.application_work_workqueuemanagement_aspx":

                    PageSpecificTourNotAvailable.Style.Add ("display", "none");

                    PageSpecificTourContainer.Style.Clear ();

                    break;

                case "ASP.application_work_workqueuemonitor_aspx":

                    PageSpecificTourNotAvailable.Style.Add ("display", "none");

                    PageSpecificTourContainer.Style.Clear ();

                    break;

            }

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

            return;

        }

        public void TourNext_OnClick (Object sender, EventArgs e) {

            String tourUserControl = "/Tour/Overview/NavigationHome.ascx";

            String focusControlClientId = "ApplicationTitleBarHomeLink";

            RaiseTourPageChanged (tourUserControl, focusControlClientId);

            return;

        }

        public void PageSpecificTour_OnClick (Object sender, EventArgs e) {

            String tourUserControl = String.Empty;

            String focusControlClientId = String.Empty;


            switch (Page.GetType ().ToString ()) {

                case "ASP.application_workspace_workspace_aspx":

                    tourUserControl = "/Tour/Workspace/Introduction.ascx";

                    focusControlClientId = "ApplicationContentControl";

                    break;
                    
                case "ASP.application_work_workqueuemanagement_aspx":
                    
                    tourUserControl = "/Tour/WorkQueueManagement/Introduction.ascx";

                    focusControlClientId = "ApplicationContentControl";

                    break;

                case "ASP.application_work_workqueuemonitor_aspx":

                    tourUserControl = "/Tour/WorkQueueMonitor/Introduction.ascx";

                    focusControlClientId = "ApplicationContentControl";

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