using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Mercury.Web.Application.Provider {

    public partial class ProviderProfile : System.Web.UI.Page {

        #region Private Properties

        private Boolean isPageUnloading = false;

        #endregion 


        #region Private Session Properties

        private String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (PageInstanceId.Text)) { PageInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return Form.Name + PageInstanceId.Text + ".";

            }

        }

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application) Session["Mercury.Application"];

                if ((application == null) && (!isPageUnloading)) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        public Client.Core.Provider.Provider Provider {

            get { return (Client.Core.Provider.Provider)Session[SessionCachePrefix + "Provider"]; }

            set {

                Client.Core.Provider.Provider member = (Client.Core.Provider.Provider)Session[SessionCachePrefix + "Provider"];

                if (member != value) {

                    Session[SessionCachePrefix + "Provider"] = value;

                }

            }

        }

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            Int64 forProviderId = 0;

            Int64 forEntityId = 0;


            if (MercuryApplication == null) { return; }


            if (!Page.IsPostBack) {

                #region Initial Page Load

                PageInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", "");


                if (Request.QueryString["ProviderId"] != null) {

                    if (Int64.TryParse ((String) Request.QueryString["ProviderId"], out forProviderId)) {

                        Provider = MercuryApplication.ProviderGet (forProviderId, true);

                        if (Provider == null) { forProviderId = 0; }

                    }

                }

                else if (Request.QueryString["EntityId"] != null) {

                    if (Int64.TryParse ((String) Request.QueryString["EntityId"], out forEntityId)) {

                        Provider = MercuryApplication.ProviderGetByEntityId (forEntityId, true);

                        if (Provider == null) { forProviderId = 0; }

                        else { forProviderId = Provider.Id; }

                    }

                }


                if (forProviderId == 0) { Server.Transfer ("/PermissionDenied.aspx"); return; }

                Page.Title = "Provider Profile: " + Provider.Entity.Name;

                ProviderDemographicHeaderLabel.Text = Provider.Name;


                #region Note Alert Icons

                Client.Core.Entity.EntityNote entityNote;

                entityNote = MercuryApplication.EntityNoteGetMostRecentByImportance (Provider.EntityId, Mercury.Server.Application.NoteImportance.Warning, false);

                if (entityNote != null) {

                    if (entityNote.TerminationDate >= DateTime.Today) {

                        EntityNoteWarning.Style.Clear ();

                        EntityNoteWarning.Attributes.Add ("title", "[" + entityNote.NoteTypeName + "] " + entityNote.Subject);

                        EntityNoteWarning.Visible = true;

                    }

                }

                entityNote = MercuryApplication.EntityNoteGetMostRecentByImportance (Provider.EntityId, Mercury.Server.Application.NoteImportance.Critical, false);

                if (entityNote != null) {

                    if (entityNote.TerminationDate >= DateTime.Today) {

                        EntityNoteCritical.Style.Clear ();

                        EntityNoteCritical.Attributes.Add ("title", "[" + entityNote.NoteTypeName + "] " + entityNote.Subject);

                        EntityNoteCritical.Visible = true;

                    }

                }

                #endregion 


                ProviderDemographicsControl.InstanceId = SessionCachePrefix + "ProviderDemographicsControl";

                ProviderDemographicsControl.AllowUserInteraction = true;

                ProviderDemographicsControl.InitializeProviderDemographics (Provider);


                ProviderAffiliationControl.InstanceId = SessionCachePrefix + "ProviderAffiliationControl";

                ProviderContractControl.InstanceId = SessionCachePrefix + "ProviderContractControl";

                // ProviderServiceLocationControl.InstanceId = SessionCachePrefix + "ProviderServiceLocationControl";


                EntityDocumentHistoryControl.InstanceId = SessionCachePrefix + "ProviderDocumentHistoryControl";

                EntityDocumentHistoryControl.AllowUserInteraction = true;

                // EntityDocumentHistoryControl.Entity = Provider.Entity;
                
                
                EntityContactHistoryControl.InstanceId = SessionCachePrefix + "EntityContactHistoryControl";

                // EntityContactHistoryControl.Entity = Provider.Entity;


                EntityNoteHistoryControl.InstanceId = SessionCachePrefix + "EntityNoteHistoryControl";

                // EntityNoteHistoryControl.Entity = Provider.Entity;


                InitializeAll ();

                #endregion

            } // Initial Page Load

            else { // Postback

                Page.Title = "Provider Profile: " + Provider.Entity.Name;

            }

            // ApplySecurity ();

            return;

        }

        protected void Page_Unload (object sender, EventArgs e) {

            isPageUnloading = true;

            if (MercuryApplication != null) { MercuryApplication.ApplicationClientClose (); }

            return;

        }

        #endregion 

        
        #region Initialization

        protected void InitializeAll () {

            InitializeToolbar ();

            InitializeProviderEnrollment ();

            return;

        }

        protected void InitializeToolbar () {

            if (Provider == null) { return; }


            #region Provider Actions

            // TODO: UPDATE V2

            //foreach (Client.Core.Work.Workflow currentWorkflow in MercuryApplication.WorkflowsAvailable (true)) {

            //    if (currentWorkflow.Enabled) {

            //        if (currentWorkflow.EntityType == Mercury.Server.Application.EntityType.Provider) {

            //            if (currentWorkflow.SessionHasPermission ()) {

            //                ProviderActionSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentWorkflow.Name, currentWorkflow.Id.ToString ()));

            //            }

            //        }

            //    }

            //}

            if (ProviderActionSelection.Items.Count == 0) {

                ProviderActionSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** No Action Available", String.Empty));

                ProviderActionGo.Enabled = false;

            }

            #endregion


            Telerik.Web.UI.RadToolBarButton buttonContact = (Telerik.Web.UI.RadToolBarButton) ProviderProfileToolbar.Items.FindItemByValue ("Contact");

            buttonContact.Enabled = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ProviderActionContact);

            buttonContact.NavigateUrl = "/Application/Actions/EntityContact.aspx?EntityId=" + Provider.EntityId.ToString ();


            Telerik.Web.UI.RadToolBarButton buttonSendCorrespondence = (Telerik.Web.UI.RadToolBarButton) ProviderProfileToolbar.Items.FindItemByValue ("SendCorrespondence");

            buttonSendCorrespondence.Enabled = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ProviderActionSendCorrespondence);

            buttonSendCorrespondence.NavigateUrl = "/Application/Actions/EntitySendCorrespondence.aspx?EntityId=" + Provider.EntityId.ToString ();


            Telerik.Web.UI.RadToolBarButton buttonNote = (Telerik.Web.UI.RadToolBarButton) ProviderProfileToolbar.Items.FindItemByValue ("CreateNote");

            buttonNote.Enabled = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ProviderNoteAdd);

            buttonNote.NavigateUrl = "/Application/Actions/EntityNote.aspx?EntityId=" + Provider.EntityId.ToString ();


            Telerik.Web.UI.RadToolBarButton buttonAddress = (Telerik.Web.UI.RadToolBarButton) ProviderProfileToolbar.Items.FindItemByValue ("CreateAddress");

            buttonAddress.Enabled = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ProviderAddressManage);

            buttonAddress.NavigateUrl = "/Application/Actions/EntityAddress.aspx?EntityId=" + Provider.EntityId.ToString ();

            return;

        }

        protected void InitializeProviderEnrollment () {

            Session[SessionCachePrefix + "ProviderEnrollmentGrid.CurrentPage"] = (Int32) 0;


            System.Data.DataTable enrollmentTable = new DataTable ();

            enrollmentTable.Columns.Add ("ProviderEnrollmentId");

            enrollmentTable.Columns.Add ("InsurerId");

            enrollmentTable.Columns.Add ("InsurerName");

            enrollmentTable.Columns.Add ("ProgramId");

            enrollmentTable.Columns.Add ("ProgramName");

            enrollmentTable.Columns.Add ("ProgramProviderId");

            enrollmentTable.Columns.Add ("EffectiveDate");

            enrollmentTable.Columns.Add ("TerminationDate");

            enrollmentTable.Columns.Add ("SortDateField");

            Session[SessionCachePrefix + "ProviderEnrollmentGrid.EnrollmentTable"] = enrollmentTable;


            foreach (Client.Core.Provider.ProviderEnrollment currentEnrollment in MercuryApplication.ProviderEnrollmentsGet (Provider.Id, true)) {

                enrollmentTable.Rows.Add (

                    currentEnrollment.Id.ToString (),

                    currentEnrollment.Program.InsurerId.ToString (),

                    (currentEnrollment.Program.Insurer != null) ? currentEnrollment.Program.Insurer.Entity.Name : "&nbsp",

                    currentEnrollment.ProgramId.ToString (),

                    (currentEnrollment.Program != null) ? currentEnrollment.Program.Name : "&nbsp",

                    currentEnrollment.ProgramProviderId,

                    currentEnrollment.EffectiveDate.ToString ("MM/dd/yyy"),

                    currentEnrollment.TerminationDate.ToString ("MM/dd/yyyy"),

                    currentEnrollment.TerminationDate.ToString ("yyyyMMdd") + currentEnrollment.EffectiveDate.ToString ("yyyyMMdd")

                    );

            }

            ProviderEnrollmentGrid.DataSource = enrollmentTable;

            ProviderEnrollmentGrid.DataBind ();

            return;

        }

        #endregion


        #region Toolbar Events

        protected void ProviderActionGo_OnClick (Object sender, EventArgs e) {

            Int64 workflowId = 0;

            if (Int64.TryParse (ProviderActionSelection.SelectedValue, out workflowId)) {

                Client.Core.Work.Workflow workflow = MercuryApplication.WorkflowGet (workflowId, true);

                if (workflow != null) {

                    Response.Redirect ("/Application/Workflow/Workflow.aspx?ProviderId=" + Provider.Id.ToString () + "&WorkflowId=" + workflow.Id.ToString (), true);

                }

            }

            return;

        }

        #endregion 


        #region Tab Strip Events

        protected void ProviderTabStrip_OnTabClick (Object sender, Telerik.Web.UI.RadTabStripEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            ProviderMultipage.FindPageViewByID (ProviderTabStrip.InnermostSelectedTab.PageViewID).Selected = true;

            switch (eventArgs.Tab.Text) {

                case "Affiliations": ProviderAffiliationControl.Provider = Provider; break;

                case "Contracts": ProviderContractControl.Provider = Provider; break;

                case "Service Locations": ProviderServiceLocationControl.Provider = Provider; break;

                case "Contact History": EntityContactHistoryControl.Entity = Provider.Entity; break;

                case "Documents": EntityDocumentHistoryControl.Entity = Provider.Entity; break;

                case "Notes": EntityNoteHistoryControl.Entity = Provider.Entity; break;

            }

            return;

        }

        #endregion

    }

}
