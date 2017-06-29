using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application {

    public partial class Application : System.Web.UI.MasterPage {

        #region Private Properties

        private Boolean isPageUnloading = false;

        #endregion 


        #region Public State Properties

        public String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (PageInstanceId.Text)) { PageInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return "Mercury" + PageInstanceId.Text;

            }

        }

        public Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application) Session["Mercury.Application"];

                if ((application == null) && (!isPageUnloading)) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        public String PageInstanceIdText { get { return PageInstanceId.Text; } }

        public Telerik.Web.UI.RadAjaxManager TelerikAjaxManagerControl { get { return TelerikAjaxManager; } }



        public String TourContentControlName {

            get {

                String contentControlName = (String)Session[SessionCachePrefix + "TourContentControlName"];

                if (String.IsNullOrEmpty (contentControlName)) { contentControlName = String.Empty; }

                return contentControlName;

            }

            set {

                Session[SessionCachePrefix + "TourContentControlName"] = value;

            }

        }

        public String TourContentControlId {

            get {

                String contentControlId = (String)Session[SessionCachePrefix + "TourContentControlId"];

                if (String.IsNullOrEmpty (contentControlId)) { contentControlId = String.Empty; }

                return contentControlId;

            }

            set {

                Session[SessionCachePrefix + "TourContentControlId"] = value;

            }

        }

        #endregion 


        #region Private Properties

        private System.Data.DataTable SearchResultsTable {

            get {

                System.Data.DataTable resultsTable = (System.Data.DataTable)Session[SessionCachePrefix + "SearchResultsTable"];

                if (resultsTable == null) {

                    resultsTable = new System.Data.DataTable ();

                    resultsTable.Columns.Add ("ObjectType");

                    resultsTable.Columns.Add ("Id");

                    resultsTable.Columns.Add ("Name");

                    resultsTable.Columns.Add ("Details");

                    resultsTable.Columns.Add ("EffectiveDate");

                    resultsTable.Columns.Add ("TerminationDate");

                    Session[SessionCachePrefix + "SearchResultsTable"] = resultsTable;

                }

                return resultsTable;

            }

            set { Session[SessionCachePrefix + "SearchResultsTable"] = value; }

        }

        #endregion 


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            
            // SET APPLICATION TITLE LABEL

            ApplicationTitle.InnerText = MercuryApplication.Session.UserDisplayName + " (" + MercuryApplication.Session.EnvironmentName + ")";


            InitializeNavigation ();

            if (!String.IsNullOrWhiteSpace (TourContentControlName)) { TourReloadUserControl (); }

            return;

        }

        protected void Page_Unload (object sender, EventArgs e) {

            isPageUnloading = true;

            if (MercuryApplication != null) { MercuryApplication.ApplicationClientClose (); }

            return;

        }

        #endregion 


        #region Initializations

        private void InitializeNavigation () {

            Boolean hasNavigation = false;


            NavigationLinkEnterprise.Visible = MercuryApplication.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.EnterpriseManagement);

            NavigationLinkConfiguraiton.Visible = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ConfigurationManagement);

            NavigationLinkFormDesigner.Visible = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.FormDesigner);

            NavigationLinkAutomation.Visible = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.Automation);

            NavigationLinkDataExplorer.Visible = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.DataExplorer);


#if (!DEBUG)

            // TODO: REMOVED FOR RELEASE, AVAILABLE FOR DEBUG ONLY

            NavigationLinkAutomation.Visible = false;

            NavigationLinkDataExplorer.Visible = false;

#endif

            hasNavigation = (NavigationLinkEnterprise.Visible || NavigationLinkConfiguraiton.Visible || NavigationLinkFormDesigner.Visible);


            ApplicationTitleBarNavigationLink.Visible = hasNavigation;

            return;

        }

        #endregion


        #region Control Events

        protected void GlobalSearchButton_OnClick (Object sender, EventArgs e) {

            Mercury.Server.Application.SearchResultsMemberResponse memberSearchResponse;

            Mercury.Server.Application.SearchResultsProviderResponse providerSearchResponse;


            memberSearchResponse = MercuryApplication.SearchMember (GlobalSearchText.Text, null, GlobalSearchText.Text);

            providerSearchResponse = MercuryApplication.SearchProvider (GlobalSearchText.Text, GlobalSearchText.Text);

            if ((memberSearchResponse.HasException) || (providerSearchResponse.HasException)) {

                if (memberSearchResponse.HasException) {

                    TelerikAjaxManager.ResponseScripts.Add ("alert ('" + memberSearchResponse.Exception.Message.Replace ("'", "''") + "');");

                }

                else if (providerSearchResponse.HasException) {

                    TelerikAjaxManager.ResponseScripts.Add ("alert ('" + providerSearchResponse.Exception.Message.Replace ("'", "''") + "');");

                }

                else { TelerikAjaxManager.ResponseScripts.Add ("alert ('Unable to complete search.');"); }

            }


            else {

                System.Data.DataTable resultsTable = SearchResultsTable;

                resultsTable.Rows.Clear ();

                foreach (Mercury.Server.Application.SearchResultMember currentMemberResult in memberSearchResponse.Results) {

                    Client.Core.Member.Member member = MercuryApplication.MemberGet (currentMemberResult.MemberId, true);


                    String memberEffectiveDate = (member.MostRecentEnrollment != null) ? member.MostRecentEnrollment.EffectiveDate.ToString ("MM/dd/yyyy") : "*";

                    String memberTerminationDate = "*";

                    if (member.MostRecentEnrollment != null) {

                        if (member.MostRecentEnrollment.TerminationDate == new DateTime (9999, 12, 31)) { memberTerminationDate = "< active >"; }

                        else { memberTerminationDate = member.MostRecentEnrollment.TerminationDate.ToString ("MM/dd/yyyy"); }

                    }


                    String programMemberId = (member.MostRecentEnrollment != null) ? member.MostRecentEnrollment.ProgramMemberId : "&nbsp";


                    resultsTable.Rows.Add (

                        "Member",

                        programMemberId,

                        CommonFunctions.MemberProfileAnchor (currentMemberResult.MemberId, currentMemberResult.Name),

                        currentMemberResult.BirthDate.ToString ("MM/dd/yyyy") + " (" + member.CurrentAge.ToString ().PadLeft (2, ' ') + ") | " + currentMemberResult.Gender,

                        memberEffectiveDate,

                        memberTerminationDate
                        
                        );

                }

                foreach (Mercury.Server.Application.SearchResultProvider currentProviderResult in providerSearchResponse.Results) {

                    Client.Core.Provider.Provider provider = MercuryApplication.ProviderGet (currentProviderResult.ProviderId, true);

                    String providerTerminationDate = "< active >";

                    if (!provider.HasCurrentEnrollment) { providerTerminationDate = String.Empty; }

                    else if (provider.CurrentEnrollment.TerminationDate != new DateTime (9999, 12, 31)) { providerTerminationDate = provider.CurrentEnrollment.TerminationDate.ToString ("MM/dd/yyyy"); }

                    resultsTable.Rows.Add (

                        "Provider",

                        ((provider.HasCurrentEnrollment) ? provider.CurrentEnrollment.ProgramProviderId : String.Empty),

                        CommonFunctions.ProviderProfileAnchor (currentProviderResult.ProviderId, currentProviderResult.Name),

                        currentProviderResult.FederalTaxId + " | " + currentProviderResult.NationalProviderId + " | " + currentProviderResult.PrimarySpecialtyName,

                        ((provider.HasCurrentEnrollment) ? provider.CurrentEnrollment.EffectiveDate.ToString ("MM/dd/yyyy") : String.Empty),

                        providerTerminationDate

                        );

                }

                SearchResultsTable = resultsTable;

                SearchResultsGrid.DataSource = resultsTable;

                SearchResultsGrid.DataBind ();


                String showDialogScript = "$find(\"" + GlobalSearchResultsWindow.ClientID + "\").show ();";

                TelerikAjaxManager.ResponseScripts.Add (showDialogScript);

            }

        }

        #endregion 
        

        #region Tour Events

        public String TourLoadUserControl (String controlName) {

            TourContentPanel.Controls.Clear ();

            UserControl contentControl = (UserControl)LoadControl (controlName);

            TourContentControlId = SessionCachePrefix + controlName.Split ('.')[0].Replace ("/", "").Replace ("~", "");

            contentControl.ID = TourContentControlId;

            TourContentPanel.Controls.Add (contentControl);

            TourContentControlName = controlName;

            if (contentControl is Tour.ITourControl) {

                TourSetProperties ((Tour.ITourControl)contentControl);

            }

            return TourContentControlId;

        }

        public void TourReloadUserControl () {

            UserControl contentControl = (UserControl)LoadControl (TourContentControlName);

            contentControl.ID = TourContentControlId;

            TourContentPanel.Controls.Add (contentControl);

            if (contentControl is Tour.ITourControl) {

                TourSetProperties ((Tour.ITourControl)contentControl);

            }


            return;

        }


        public void TourSetProperties (Tour.ITourControl contentControl) {

            if (!contentControl.HasPrevious) { TourPreviousContainer.Style.Add ("display", "none"); } else { TourPreviousContainer.Style.Clear (); }

            if (!contentControl.HasNext) { TourNextContainer.Style.Add ("display", "none"); } else { TourNextContainer.Style.Clear (); }

            contentControl.TourPageChanged += new Tour.TourControlPageChangedEventHandler (TourControl_OnPageChanged);

            return;

        }

        protected void TourStart_OnClick (Object sender, EventArgs e) {

            TourLoadUserControl ("/Tour/Introduction.ascx");

            String showTourToolTipScript = "setTimeout (\"$find ('" + TourToolTip.ClientID + "').show ();\", 10);";

            TelerikAjaxManager.ResponseScripts.Add (showTourToolTipScript);

            return;

        }

        protected void TourPrevious_OnClick (Object sender, EventArgs e) {

            UserControl contentControl = (UserControl)LoadControl (TourContentControlName);

            ((Tour.ITourControl)contentControl).TourPageChanged += new Tour.TourControlPageChangedEventHandler (TourControl_OnPageChanged);

            if (contentControl is Tour.ITourControl) {

                ((Tour.ITourControl)contentControl).TourPrevious_OnClick (sender, e);

            }

            return;

        }

        protected void TourNext_OnClick (Object sender, EventArgs e) {

            UserControl contentControl = (UserControl)LoadControl (TourContentControlName);

            ((Tour.ITourControl)contentControl).TourPageChanged += new Tour.TourControlPageChangedEventHandler (TourControl_OnPageChanged);

            if (contentControl is Tour.ITourControl) {

                ((Tour.ITourControl)contentControl).TourNext_OnClick (sender, e);

            }

            return;

        }

        protected void TourControl_OnPageChanged (Object sender, Tour.TourControlPageChangedEventArgs e) {

            TourLoadUserControl (e.TourUserControl); // LOAD NEW CONTROL REQUEST


            String showTourToolTipScript = "var tourToolTip = $find ('" + TourToolTip.ClientID + "');";

            showTourToolTipScript += "tourToolTip.hide ();"; // HIDE EXISTING TOOL TIP TO RESET TARGET CONTROL

            showTourToolTipScript += "tourToolTip.set_targetControlID ('" + e.FocusControlClientId + "');";

            showTourToolTipScript += "tourToolTip.show ();"; // SHOW NEW TOOL TIP 

            showTourToolTipScript = "setTimeout (\"" + showTourToolTipScript + "\", 10);";


            TelerikAjaxManager.ResponseScripts.Add (showTourToolTipScript);

            if (!String.IsNullOrWhiteSpace (e.ResponseScript)) { TelerikAjaxManager.ResponseScripts.Add (e.ResponseScript); }

            return;

        }

        #endregion 

    }

}