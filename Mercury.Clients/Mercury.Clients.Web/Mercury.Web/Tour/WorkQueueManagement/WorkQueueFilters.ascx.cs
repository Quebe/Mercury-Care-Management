using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Tour.WorkQueueManagement {

    public partial class WorkQueueFilters : System.Web.UI.UserControl, ITourControl {

        #region Interface Public Properties

        public Boolean HasPrevious { get { return true; } }

        public Boolean HasNext { get { return true; } }

        public Telerik.Web.UI.RadAjaxManager TelerikAjaxManager { get { return (Telerik.Web.UI.RadAjaxManager)Page.Controls[0].FindControl ("TelerikAjaxManager"); } }

        #endregion


        #region Interface Events

        public event Tour.TourControlPageChangedEventHandler TourPageChanged;

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            FiltersSelectionExpand ();

            return;

        }

        private void FiltersSelectionExpand () {

            if (TelerikAjaxManager != null) {

                String clientId = Page.Controls[0].FindControl ("ApplicationContentControl").FindControl ("BasicFiltersSelection").ClientID;

                String expandScript = "var filtersSelection = $find (\"" + clientId + "\");";

                expandScript += "if (filtersSelection != null) { filtersSelection.showDropDown (); }";

                TelerikAjaxManager.ResponseScripts.Add (expandScript);

            }

            return;

        }

        private void FiltersSelectionCollapse () {
            
            if (TelerikAjaxManager != null) {

                String clientId = Page.Controls[0].FindControl ("ApplicationContentControl").FindControl ("BasicFiltersSelection").ClientID;

                String expandScript = "var filtersSelection = $find (\"" + clientId + "\");";

                expandScript += "if (filtersSelection != null) { filtersSelection.hideDropDown (); }";

                TelerikAjaxManager.ResponseScripts.Add (expandScript);

            }

            return;

        }

        #endregion


        #region Tour Events

        private void RaiseTourPageChanged (String tourUserControl, String focusControlClientId) {

            FiltersSelectionCollapse ();

            Tour.TourControlPageChangedEventArgs e = new TourControlPageChangedEventArgs (tourUserControl, focusControlClientId);

            if (TourPageChanged != null) { TourPageChanged (this, e); }

            return;

        }

        public void TourPrevious_OnClick (Object sender, EventArgs e) {

            String tourUserControl = "/Tour/WorkQueueManagement/WorkQueueViewSelection.ascx";

            String focusControlClientId = "ApplicationContentControl";

            Control applicationContent = Page.Controls[0].FindControl (focusControlClientId);

            if (applicationContent != null) {

                applicationContent = applicationContent.FindControl ("WorkQueueViewSelection");

                if (applicationContent != null) {

                    focusControlClientId = applicationContent.ClientID;

                }

            }

            RaiseTourPageChanged (tourUserControl, focusControlClientId);

            return;

        }

        public void TourNext_OnClick (Object sender, EventArgs e) {

            String tourUserControl = "/Tour/WorkQueueManagement/WorkQueueItemGridRefresh.ascx";

            String focusControlClientId = "ApplicationContentControl";

            Control applicationContent = Page.Controls[0].FindControl (focusControlClientId);

            if (applicationContent != null) {

                applicationContent = applicationContent.FindControl ("WorkQueueItemsGridRefresh");

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