using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Tour.Overview {

    public partial class NavigationHome : System.Web.UI.UserControl, ITourControl {
        
        #region Public State Properties

        public Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application)Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        #endregion 


        #region Interface Public Properties

        public Boolean HasNext { get { return true; } }

        public Boolean HasPrevious { get { return true; } }

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

            String tourUserControl = "/Tour/Introduction.ascx";

            String focusControlClientId = "ApplicationTitle";

            RaiseTourPageChanged (tourUserControl, focusControlClientId);

            return;

        }

        public void TourNext_OnClick (Object sender, EventArgs e) {

            Boolean hasNavigation = false;

            String tourUserControl = "/Tour/Overview/NavigationLogout.ascx";

            String focusControlClientId = "ApplicationTitleBarLogoutLink";
            

            hasNavigation |= MercuryApplication.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.EnterpriseManagement);

            hasNavigation |= MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ConfigurationManagement);

            hasNavigation |= MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.FormDesigner);

            if (hasNavigation) {

                tourUserControl = "/Tour/Overview/NavigationMenu.ascx";

                focusControlClientId = "NavigationLink";

            }

            RaiseTourPageChanged (tourUserControl, focusControlClientId);

            return;

        }

        #endregion

    }

}