using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Tour.WorkQueueManagement {

    public partial class WorkQueueItemGridRefresh : System.Web.UI.UserControl, ITourControl {

        #region Interface Public Properties

        public Boolean HasPrevious { get { return true; } }

        public Boolean HasNext { get { return true; } }

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

            String tourUserControl = "/Tour/WorkQueueManagement/WorkQueueFilters.ascx";

            String focusControlClientId = "ApplicationContentControl";

            Control applicationContent = Page.Controls[0].FindControl (focusControlClientId);

            if (applicationContent != null) {

                applicationContent = applicationContent.FindControl ("BasicFiltersSelection");

                if (applicationContent != null) {

                    focusControlClientId = applicationContent.ClientID;

                }

            }

            RaiseTourPageChanged (tourUserControl, focusControlClientId);

            return;

        }

        public void TourNext_OnClick (Object sender, EventArgs e) {

            String tourUserControl = "/Tour/WorkQueueManagement/WorkQueueItemGridCount.ascx";

            String focusControlClientId = "ApplicationContentControl";

            Control applicationContent = Page.Controls[0].FindControl (focusControlClientId);

            if (applicationContent != null) {

                applicationContent = applicationContent.FindControl ("WorkQueueItemsAvailableCount");

                if (applicationContent != null) {

                    focusControlClientId = applicationContent.ClientID;

                }

            }

            RaiseTourPageChanged (tourUserControl, focusControlClientId);

            return;

        }

        #endregion

    }

}