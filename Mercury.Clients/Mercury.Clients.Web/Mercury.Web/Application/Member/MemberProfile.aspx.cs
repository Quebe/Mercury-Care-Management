using System;
using System.Collections;
using System.Collections.Generic;
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

namespace Mercury.Web.Application.MemberProfile {

    public partial class MemberProfile : System.Web.UI.Page {

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

        public Client.Core.Member.Member Member {

            get { return (Client.Core.Member.Member) Session[SessionCachePrefix + "Member"]; }

            set {

                Client.Core.Member.Member member = (Client.Core.Member.Member) Session[SessionCachePrefix + "Member"];

                if (member != value) {

                    Session[SessionCachePrefix + "Member"] = value;

                    MemberId.Text = value.Id.ToString ();

                }

            }

        }

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            Int64 forMemberId = 0;

            Int64 forEntityId = 0;


            if (MercuryApplication == null) { return; }


            if (!Page.IsPostBack) {

                #region Initial Page Load

                if (Request.QueryString["MemberId"] != null) { 

                    if (Int64.TryParse ((String) Request.QueryString ["MemberId"], out forMemberId)) {

                        Member = MercuryApplication.MemberGetDemographics (forMemberId, true); // FORCE LOAD OF ALL DEMOGRAPHICS INFORMATION DURING ONE REQUEST

                        if (Member == null) { forMemberId = 0; }

                    }

                }

                else if (Request.QueryString["EntityId"] != null) {

                    if (Int64.TryParse ((String) Request.QueryString["EntityId"], out forEntityId)) {

                        Member = MercuryApplication.MemberGetDemographicsByEntityId (forEntityId, true); // FORCE LOAD OF ALL DEMOGRAPHICS INFORMATION DURING ONE REQUEST

                        if (Member == null) { forMemberId = 0; }

                        else { forMemberId = Member.Id; }

                    }

                }
               

                if (forMemberId == 0) { Server.Transfer ("/PermissionDenied.aspx"); return; }
                                 
                Page.Title = "Member Profile: " + Member.Name;

                MemberDemographicHeaderLabel.Text = Member.Name + " (" + Member.CurrentAge + " | " + Member.GenderDescription + ((Member.MostRecentEnrollment != null) ? " | " + Member.MostRecentEnrollment.ProgramMemberId : String.Empty) + ")";


                #region Note Alert Icons

                Dictionary<Mercury.Server.Application.NoteImportance, Client.Core.Entity.EntityNote> entityNotes;

                entityNotes = MercuryApplication.EntityNoteGetMostRecentByAllImportances (Member.EntityId, true);


                Client.Core.Entity.EntityNote entityNote = null;

                //entityNote = MercuryApplication.EntityNoteGetMostRecentByImportance (Member.EntityId, Mercury.Server.Application.NoteImportance.Warning, false);

                if (entityNotes.ContainsKey (Mercury.Server.Application.NoteImportance.Warning)) { entityNote = entityNotes[Mercury.Server.Application.NoteImportance.Warning]; }

                if (entityNote != null) {

                    if (entityNote.TerminationDate >= DateTime.Today) {

                        EntityNoteWarning.Style.Clear ();

                        EntityNoteWarning.Attributes.Add ("title", "[" + entityNote.NoteTypeName + "] " + entityNote.Subject);

                        EntityNoteWarning.Visible = true;

                    }

                }

                //entityNote = MercuryApplication.EntityNoteGetMostRecentByImportance (Member.EntityId, Mercury.Server.Application.NoteImportance.Critical, false);

                entityNote = null;

                if (entityNotes.ContainsKey (Mercury.Server.Application.NoteImportance.Critical)) { entityNote = entityNotes[Mercury.Server.Application.NoteImportance.Critical]; }

                if (entityNote != null) {

                    if (entityNote.TerminationDate >= DateTime.Today) {

                        EntityNoteCritical.Style.Clear ();

                        EntityNoteCritical.Attributes.Add ("title", "[" + entityNote.NoteTypeName + "] " + entityNote.Subject);

                        EntityNoteCritical.Visible = true;

                    }

                }

                #endregion 


                MemberDemographicsControl.InstanceId = SessionCachePrefix + "MemberDemographicsControl";

                MemberDemographicsControl.AllowUserInteraction = true;

                MemberDemographicsControl.InitializeMemberDemographics (Member.Id);


                MemberServicesControl.InstanceId = SessionCachePrefix + "MemberServicesControl";

                MemberMetricsControl.InstanceId = SessionCachePrefix + "MemberMetricsControl";

                MemberAuthorizedServicesControl.InstanceId = SessionCachePrefix + "MemberAuthorizedServicesControl";


                EntityContactHistoryControl.InstanceId = SessionCachePrefix + "EntityContactHistoryControl";

                EntityContactHistoryControl.AllowUserInteraction = true;


                EntityDocumentHistoryControl.InstanceId = SessionCachePrefix + "MemberDocumentHistoryControl";

                EntityDocumentHistoryControl.AllowUserInteraction = true;
                
                

                EntityNoteHistoryControl.InstanceId = SessionCachePrefix + "EntityNoteHistoryControl";

                EntityNoteHistoryControl.AllowUserInteraction = true;


                
                MemberWorkHistoryControl.InstanceId = SessionCachePrefix + "MemberWorkHistoryControl";

                MemberWorkHistoryControl.AllowUserInteraction = true;
                

                MemberClaimHistoryControl.InstanceId = SessionCachePrefix + "MemberClaimHistoryControl";

                MemberAuthorizationHistoryControl.InstanceId = SessionCachePrefix + "MemberAuthorizationHistoryControl";


                MemberCaseViewControl.InstanceId = SessionCachePrefix + "MemberCaseViewControl";

                MemberCaseViewControl.InitializeMember (Member);


                InitializeToolbar ();

                InitializeMemberEnrollment ();

                InitializeMemberEnrollmentTplCob ();

                InitializePopulationCareManagement ();

                #endregion

            } // Initial Page Load

            else { // Postback

                Page.Title = "Member Profile: " + Member.Entity.Name;

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

        protected void InitializeToolbar () {

            if (Member == null) { return; }


            #region Member Actions

            MercuryApplication.WorkTeamsForSession (false); // CLEAR CACHE OF SESSION WORK TEAMS

            List<Client.Core.Work.Workflow> workflowsAvailable = MercuryApplication.WorkflowsAvailable (true);

            if (workflowsAvailable.Count == 0) { workflowsAvailable = MercuryApplication.WorkflowsAvailable (false); }

            foreach (Client.Core.Work.Workflow currentWorkflow in workflowsAvailable) {

                Boolean isGrantedPermission = false;

                Boolean isDeniedPermission = ((!currentWorkflow.Visible) || (!currentWorkflow.Enabled) || (currentWorkflow.EntityType != Mercury.Server.Application.EntityType.Member));

                if (!isDeniedPermission) {

                    if (currentWorkflow.Application == null) { currentWorkflow.Application = MercuryApplication; }

                    isGrantedPermission = currentWorkflow.SessionGrantedPermission; 
                
                }

                if ((isGrantedPermission) && (!isDeniedPermission)) {

                    MemberActionSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentWorkflow.Name, currentWorkflow.Id.ToString ()));

                }

            }
            
            if (MemberActionSelection.Items.Count == 0) {

                MemberActionSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** No Action Available", String.Empty));

                MemberActionGo.Enabled = false;

            }

            #endregion 


            Telerik.Web.UI.RadToolBarButton buttonContact = (Telerik.Web.UI.RadToolBarButton) MemberProfileToolbar.Items.FindItemByValue ("Contact");

            buttonContact.Enabled = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MemberActionContact);

            buttonContact.NavigateUrl = "/Application/Actions/EntityContact.aspx?EntityId=" + Member.EntityId.ToString ();


            Telerik.Web.UI.RadToolBarButton buttonSendCorrespondence = (Telerik.Web.UI.RadToolBarButton) MemberProfileToolbar.Items.FindItemByValue ("SendCorrespondence");

            buttonSendCorrespondence.Enabled = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MemberActionSendCorrespondence);

            buttonSendCorrespondence.NavigateUrl = "/Application/Actions/EntitySendCorrespondence.aspx?EntityId=" + Member.EntityId.ToString ();


            Telerik.Web.UI.RadToolBarButton buttonNote = (Telerik.Web.UI.RadToolBarButton) MemberProfileToolbar.Items.FindItemByValue ("CreateNote");

            buttonNote.Enabled = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MemberNoteAdd);

            buttonNote.NavigateUrl = "/Application/Actions/EntityNote.aspx?EntityId=" + Member.EntityId.ToString ();

            Telerik.Web.UI.RadToolBarButton buttonAddress = (Telerik.Web.UI.RadToolBarButton) MemberProfileToolbar.Items.FindItemByValue ("CreateAddress");

            buttonAddress.Enabled = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MemberAddressManage);

            buttonAddress.NavigateUrl = "/Application/Actions/EntityAddress.aspx?EntityId=" + Member.EntityId.ToString ();

            return;

        }

        protected void InitializeMemberEnrollment () {

            Session[SessionCachePrefix + "MemberEnrollmentGrid.CurrentPage"] = (Int32) 0;


            System.Data.DataTable enrollmentTable = new DataTable ();

            enrollmentTable.Columns.Add ("EnrollmentId");

            enrollmentTable.Columns.Add ("InsurerId");

            enrollmentTable.Columns.Add ("InsurerName");

            enrollmentTable.Columns.Add ("ProgramId");

            enrollmentTable.Columns.Add ("ProgramName");

            enrollmentTable.Columns.Add ("SponsorId");

            enrollmentTable.Columns.Add ("SponsorName");

            enrollmentTable.Columns.Add ("SubscriberId");

            enrollmentTable.Columns.Add ("SubscriberName");

            enrollmentTable.Columns.Add ("ProgramMemberId");

            enrollmentTable.Columns.Add ("RateCode");

            enrollmentTable.Columns.Add ("PcpProviderName");

            enrollmentTable.Columns.Add ("EffectiveDate");

            enrollmentTable.Columns.Add ("TerminationDate");

            enrollmentTable.Columns.Add ("SortDateField");

            Session[SessionCachePrefix + "MemberEnrollmentGrid.EnrollmentTable"] = enrollmentTable;

            MemberEnrollmentGrid.DataSource = enrollmentTable;

            MemberEnrollmentGrid.DataBind ();


            System.Data.DataTable enrollmentCoverage = new DataTable ();

            enrollmentCoverage.Columns.Add ("EnrollmentId");

            enrollmentCoverage.Columns.Add ("EnrollmentCoverageId");

            enrollmentCoverage.Columns.Add ("BenefitPlan");

            enrollmentCoverage.Columns.Add ("CoverageLevel");

            enrollmentCoverage.Columns.Add ("RateCode");

            enrollmentCoverage.Columns.Add ("EffectiveDate");

            enrollmentCoverage.Columns.Add ("TerminationDate");

            enrollmentCoverage.Columns.Add ("SortDateField");

            Session[SessionCachePrefix + "MemberEnrollmentGrid.EnrollmentCoverageTable"] = enrollmentCoverage;

            MemberEnrollmentGrid.MasterTableView.DetailTables[0].DataSource = enrollmentCoverage;

            MemberEnrollmentGrid.MasterTableView.DetailTables[0].DataBind ();


            System.Data.DataTable enrollmentPcpAssignment = new DataTable ();

            enrollmentPcpAssignment.Columns.Add ("EnrollmentId");

            enrollmentPcpAssignment.Columns.Add ("PcpAssignmentId");

            enrollmentPcpAssignment.Columns.Add ("PcpProviderId");

            enrollmentPcpAssignment.Columns.Add ("PcpProviderName");

            enrollmentPcpAssignment.Columns.Add ("PcpAffiliateId");

            enrollmentPcpAssignment.Columns.Add ("PcpAffiliateName");

            enrollmentPcpAssignment.Columns.Add ("EffectiveDate");

            enrollmentPcpAssignment.Columns.Add ("TerminationDate");

            enrollmentPcpAssignment.Columns.Add ("SortDateField");

            Session[SessionCachePrefix + "MemberEnrollmentGrid.EnrollmentPcpAssignmentTable"] = enrollmentPcpAssignment;

            MemberEnrollmentGrid.MasterTableView.DetailTables[1].DataSource = enrollmentPcpAssignment;

            MemberEnrollmentGrid.MasterTableView.DetailTables[1].DataBind ();

            return;

        }

        protected void InitializeMemberEnrollmentTplCob () {

            Session[SessionCachePrefix + "MemberEnrollmentTplCobGrid.CurrentPage"] = (Int32) 0;


            System.Data.DataTable enrollmentTplCobTable = new DataTable ();

            enrollmentTplCobTable.Columns.Add ("EnrollmentTplCobId");

            enrollmentTplCobTable.Columns.Add ("InsurerId");

            enrollmentTplCobTable.Columns.Add ("InsurerName");

            enrollmentTplCobTable.Columns.Add ("ProgramId");

            enrollmentTplCobTable.Columns.Add ("ProgramName");

            enrollmentTplCobTable.Columns.Add ("SponsorId");

            enrollmentTplCobTable.Columns.Add ("SponsorName");

            enrollmentTplCobTable.Columns.Add ("SubscriberId");

            enrollmentTplCobTable.Columns.Add ("SubscriberName");

            enrollmentTplCobTable.Columns.Add ("BenefitPlanName");

            enrollmentTplCobTable.Columns.Add ("ProgramMemberId");

            enrollmentTplCobTable.Columns.Add ("EffectiveDate");

            enrollmentTplCobTable.Columns.Add ("TerminationDate");

            enrollmentTplCobTable.Columns.Add ("SortDateField");

            Session[SessionCachePrefix + "MemberEnrollmentTplCobGrid.EnrollmentTplCobTable"] = enrollmentTplCobTable;

            MemberEnrollmentTplCobGrid.DataSource = enrollmentTplCobTable;

            MemberEnrollmentTplCobGrid.DataBind ();

            return;

        }

        protected void InitializePopulationCareManagement () {

            System.Data.DataTable membershipTable = new DataTable ();

            membershipTable.Columns.Add ("PopulationMembershipId");

            membershipTable.Columns.Add ("PopulationId");

            membershipTable.Columns.Add ("PopulationName");

            membershipTable.Columns.Add ("EffectiveDate");

            membershipTable.Columns.Add ("TerminationDate");

            membershipTable.Columns.Add ("AnchorDate");

            membershipTable.Columns.Add ("ServiceName");

            membershipTable.Columns.Add ("ExpectedEventDate");

            membershipTable.Columns.Add ("PreviousThresholdDate");

            membershipTable.Columns.Add ("NextThresholdDate");

            membershipTable.Columns.Add ("Status");


            Session[SessionCachePrefix + "PopulationCareManagementGrid.MembershipTable"] = membershipTable;

            PopulationCareManagementGrid.DataSource = membershipTable;

            PopulationCareManagementGrid_OnNeedDataSource (this, new Telerik.Web.UI.GridNeedDataSourceEventArgs (Telerik.Web.UI.GridRebindReason.ExplicitRebind));

            PopulationCareManagementGrid.DataBind ();


            System.Data.DataTable serviceEventTable = new DataTable ();

            serviceEventTable.Columns.Add ("PopulationMembershipId");

            serviceEventTable.Columns.Add ("ServiceName");

            serviceEventTable.Columns.Add ("ExpectedEventDate");

            serviceEventTable.Columns.Add ("EventDate");

            serviceEventTable.Columns.Add ("PreviousThresholdDate");

            serviceEventTable.Columns.Add ("NextThresholdDate");

            serviceEventTable.Columns.Add ("Status");


            Session[SessionCachePrefix + "PopulationCareManagementGrid.ServiceEventTable"] = serviceEventTable;


            PopulationCareManagementGrid.MasterTableView.DetailTables[0].DataSource = serviceEventTable;

            PopulationCareManagementGrid.MasterTableView.DetailTables[0].DataBind ();


            System.Data.DataTable triggerEventTable = new DataTable ();

            triggerEventTable.Columns.Add ("PopulationMembershipId");

            triggerEventTable.Columns.Add ("PopulationMembershipTriggerEventId");

            triggerEventTable.Columns.Add ("TriggerType");

            triggerEventTable.Columns.Add ("TriggerName");

            triggerEventTable.Columns.Add ("TriggerDate");

            triggerEventTable.Columns.Add ("EventDate");

            triggerEventTable.Columns.Add ("ProblemStatement");

            triggerEventTable.Columns.Add ("ActionDescription");


            Session[SessionCachePrefix + "PopulationCareManagementGrid.TriggerEventTable"] = triggerEventTable;

            PopulationCareManagementGrid.MasterTableView.DetailTables[1].DataSource = triggerEventTable;

            PopulationCareManagementGrid.MasterTableView.DetailTables[1].DataBind ();

            return;

        }

        #endregion


        #region Toolbar Events

        protected void MemberActionGo_OnClick (Object sender, EventArgs e) {

            Int64 workflowId = 0;

            if (Int64.TryParse (MemberActionSelection.SelectedValue, out workflowId)) {

                Client.Core.Work.Workflow workflow = MercuryApplication.WorkflowGet (workflowId, true);

                if (workflow != null) {

                    Response.Redirect ("/Application/Workflow/Workflow.aspx?MemberId=" + Member.Id.ToString () + "&WorkflowId=" + workflow.Id.ToString (), true);

                }

            }

            return;

        }

        #endregion 


        #region Tab Strip Events

        protected void MemberTabStrip_OnTabClick (Object sender, Telerik.Web.UI.RadTabStripEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            MemberMultipage.FindPageViewByID (MemberTabStrip.InnermostSelectedTab.PageViewID).Selected = true;

            switch (eventArgs.Tab.Text) {

                case "Enrollment":

                    MemberEnrollmentGrid_OnNeedDataSource (sender, new Telerik.Web.UI.GridNeedDataSourceEventArgs (Telerik.Web.UI.GridRebindReason.ExplicitRebind));

                    MemberEnrollmentGrid.DataBind ();     

                    break;

                case "TPL/COB":

                    MemberEnrollmentTplCobGrid_OnNeedDataSource (sender, new Telerik.Web.UI.GridNeedDataSourceEventArgs (Telerik.Web.UI.GridRebindReason.ExplicitRebind));

                    MemberEnrollmentTplCobGrid.DataBind ();

                    break;

                case "Services": MemberServicesControl.Member = Member; break;

                case "Metrics": MemberMetricsControl.Member = Member; break;

                case "Authorized Services": MemberAuthorizedServicesControl.Member = Member; break;

                case "Contacts": EntityContactHistoryControl.Entity = Member.Entity; break;

                case "Documents": EntityDocumentHistoryControl.Entity = Member.Entity; break;

                case "Notes": EntityNoteHistoryControl.Entity = Member.Entity; break;

                case "Work History": MemberWorkHistoryControl.Member = Member; break;

                case "Claims": MemberClaimHistoryControl.Member = Member; break;

                case "Authorizations": MemberAuthorizationHistoryControl.Member = Member; break;

            }

            return;

        }

        #endregion


        #region Member Enrollment Grid Events

        protected void MemberEnrollmentGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            if ((eventArgs.RebindReason & Telerik.Web.UI.GridRebindReason.ExplicitRebind) == Telerik.Web.UI.GridRebindReason.ExplicitRebind) {

                System.Data.DataTable enrollmentTable = (System.Data.DataTable) Session[SessionCachePrefix + "MemberEnrollmentGrid.EnrollmentTable"];

                enrollmentTable.Rows.Clear ();


                foreach (Client.Core.Member.MemberEnrollment currentEnrollment in Member.Enrollments) {

                    enrollmentTable.Rows.Add (

                        currentEnrollment.Id.ToString (),

                        currentEnrollment.Program.InsurerId.ToString (),

                        (currentEnrollment.Insurer != null) ? ((currentEnrollment.Insurer.Entity != null) ? currentEnrollment.Insurer.Entity.Name : String.Empty) : String.Empty,

                        currentEnrollment.ProgramId.ToString (),

                        (currentEnrollment.Program != null) ? currentEnrollment.ProgramName : String.Empty,

                        currentEnrollment.SponsorId.ToString (),

                        (currentEnrollment.Sponsor != null) ? ((currentEnrollment.Sponsor.Entity != null) ? currentEnrollment.Sponsor.Entity.Name : String.Empty) : String.Empty,

                        currentEnrollment.SubscriberId.ToString (),

                        (currentEnrollment.Subscriber != null) ? ((currentEnrollment.Subscriber.Entity != null) ? currentEnrollment.Subscriber.Entity.Name : String.Empty) : String.Empty,

                        currentEnrollment.ProgramMemberId,

                        (currentEnrollment.MostRecentCoverage != null) ? currentEnrollment.MostRecentCoverage.RateCode : String.Empty,

                        (currentEnrollment.MostRecentPcp != null) ? (currentEnrollment.MostRecentPcp.PcpProvider != null) ? currentEnrollment.MostRecentPcp.PcpProvider.Entity.Name : String.Empty : String.Empty,

                        currentEnrollment.EffectiveDate.ToString ("MM/dd/yyyy"),

                        currentEnrollment.TerminationDate.ToString ("MM/dd/yyyy"),

                        currentEnrollment.TerminationDate.ToString ("yyyyMMdd") + currentEnrollment.EffectiveDate.ToString ("yyyyMMdd")

                    );

                }

                MemberEnrollmentGrid.DataSource = enrollmentTable;

                // MemberEnrollmentGrid.DataBind ();            

            }

            return;

        }

        protected void MemberEnrollmentGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            System.Data.DataTable enrollmentTable = (System.Data.DataTable) Session[SessionCachePrefix + "MemberEnrollmentGrid.EnrollmentTable"];

            System.Data.DataTable enrollmentCoverageTable = (System.Data.DataTable) Session[SessionCachePrefix + "MemberEnrollmentGrid.EnrollmentCoverageTable"];

            System.Data.DataTable enrollmentPcpAssignmentTable = (System.Data.DataTable) Session[SessionCachePrefix + "MemberEnrollmentGrid.EnrollmentPcpAssignmentTable"];


            if ((enrollmentCoverageTable == null) || (enrollmentPcpAssignmentTable == null)) { return; }


            if ((eventArgs.CommandName == "ExpandCollapse")) {

                Telerik.Web.UI.GridDataItem gridItem = (Telerik.Web.UI.GridDataItem) eventArgs.Item;

                Int64 enrollmentId;

                if (Int64.TryParse (gridItem["EnrollmentId"].Text, out enrollmentId)) {

                    enrollmentCoverageTable.Rows.Clear ();

                    MemberEnrollmentGrid.MasterTableView.DetailTables[0].DataSource = Member.Enrollment (enrollmentId).Coverages;

                    MemberEnrollmentGrid.MasterTableView.DetailTables[0].DataBind ();


                    enrollmentPcpAssignmentTable.Rows.Clear ();

                    foreach (Client.Core.Member.MemberEnrollmentPcp currentPcpAssignment in Member.Enrollment (enrollmentId).Pcps) {

                        enrollmentPcpAssignmentTable.Rows.Add (

                            currentPcpAssignment.MemberEnrollmentId.ToString (),

                            currentPcpAssignment.Id.ToString (),

                            currentPcpAssignment.PcpProviderId.ToString (),

                            currentPcpAssignment.PcpProvider.Entity.Name,

                            currentPcpAssignment.ProviderAffilation.AffiliateProviderId.ToString (),

                            currentPcpAssignment.PcpAffiliateProvider.Entity.Name,

                            currentPcpAssignment.EffectiveDate.ToString ("MM/dd/yyyy"),

                            currentPcpAssignment.TerminationDate.ToString ("MM/dd/yyyy"),

                            currentPcpAssignment.TerminationDate.ToString ("yyyyMMdd") + currentPcpAssignment.EffectiveDate.ToString ("yyyyMMdd")

                        );

                    }

                    MemberEnrollmentGrid.MasterTableView.DetailTables[1].DataSource = enrollmentPcpAssignmentTable;

                    MemberEnrollmentGrid.MasterTableView.DetailTables[1].DataBind ();


                }

            }

            return;

        }

        #endregion


        #region Member EnrollmentTplCob Grid Events

        protected void MemberEnrollmentTplCobGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            if ((eventArgs.RebindReason & Telerik.Web.UI.GridRebindReason.ExplicitRebind) == Telerik.Web.UI.GridRebindReason.ExplicitRebind) {

                System.Data.DataTable enrollmentTplCobTable = (System.Data.DataTable) Session[SessionCachePrefix + "MemberEnrollmentTplCobGrid.EnrollmentTplCobTable"];

                enrollmentTplCobTable.Rows.Clear ();

                foreach (Client.Core.Member.MemberEnrollmentTplCob currentEnrollmentTplCob in Member.EnrollmentTplCobs) {

                    enrollmentTplCobTable.Rows.Add (

                        currentEnrollmentTplCob.Id.ToString (),

                        ((currentEnrollmentTplCob.Insurer != null) ? currentEnrollmentTplCob.Insurer.Id : 0),

                        currentEnrollmentTplCob.InsurerName,

                        ((currentEnrollmentTplCob.Program != null) ? currentEnrollmentTplCob.Program.Id : 0),

                        currentEnrollmentTplCob.ProgramName,

                        ((currentEnrollmentTplCob.Sponsor != null) ? currentEnrollmentTplCob.Sponsor.Id : 0),

                        currentEnrollmentTplCob.SponsorName,

                        ((currentEnrollmentTplCob.Subscriber != null) ? currentEnrollmentTplCob.Subscriber.Id : 0),

                        currentEnrollmentTplCob.SubscriberName,

                        currentEnrollmentTplCob.BenefitPlanName,

                        currentEnrollmentTplCob.ProgramMemberId,

                        currentEnrollmentTplCob.EffectiveDate.ToString ("MM/dd/yyyy"),

                        currentEnrollmentTplCob.TerminationDate.ToString ("MM/dd/yyyy"),

                        currentEnrollmentTplCob.TerminationDate.ToString ("yyyyMMdd") + currentEnrollmentTplCob.EffectiveDate.ToString ("yyyyMMdd")

                    );

                }

                MemberEnrollmentTplCobGrid.DataSource = enrollmentTplCobTable;

                // MemberEnrollmentTplCobGrid.DataBind ();            

            }

            return;

        }

        #endregion


        #region Population Care Management - Population Membership Grid Events

        protected void PopulationCareManagementGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            System.Data.DataTable membershipTable = (System.Data.DataTable) Session[SessionCachePrefix + "PopulationCareManagementGrid.MembershipTable"];


            if ((eventArgs.RebindReason & Telerik.Web.UI.GridRebindReason.ExplicitRebind) == Telerik.Web.UI.GridRebindReason.ExplicitRebind) { 

                membershipTable.Rows.Clear ();



                foreach (Mercury.Server.Application.PopulationMembershipSummaryDataView currentSummary in Member.PopulationMembershipSummary) {

                    String effectiveDateInformation = "<span title=\"" + currentSummary.IdentifyingEventServiceName + ((currentSummary.IdentifyingEventDate.HasValue) ? " (" + currentSummary.IdentifyingEventDate.Value.ToString ("MM/dd/yyyy") + ")" : String.Empty) + "\">" + currentSummary.EffectiveDate.ToString ("MM/dd/yyyy") + "</span>";

                    String terminationDateInformation = "<span title=\"" + currentSummary.TerminatingEventServiceName + ((currentSummary.TerminatingEventDate.HasValue) ? " (" + currentSummary.TerminatingEventDate.Value.ToString ("MM/dd/yyyy") + ")" : String.Empty) + "\">" + ((currentSummary.TerminationDate > DateTime.Today) ? "active" : currentSummary.TerminationDate.ToString ("MM/dd/yyyy")) + "</span>";

                    if (currentSummary.PopulationVisible) {

                        membershipTable.Rows.Add (

                            currentSummary.PopulationMembershipId.ToString (),

                            currentSummary.PopulationId.ToString (),

                            currentSummary.PopulationName,

                            effectiveDateInformation,

                            terminationDateInformation,

                            currentSummary.AnchorDate.ToString ("MM/dd/yyy"),

                            //currentSummary.EffectiveDate.ToString ("MM/dd/yyyy"),

                            //(currentSummary.TerminationDate > DateTime.Today) ? "active" : currentSummary.TerminationDate.ToString ("MM/dd/yyyy"),

                            currentSummary.ServiceName,

                            (currentSummary.ExpectedEventDate.HasValue) ? currentSummary.ExpectedEventDate.Value.ToString ("MM/dd/yyyy") : String.Empty,

                            (currentSummary.PreviousThresholdDate.HasValue) ? currentSummary.PreviousThresholdDate.Value.ToString ("MM/dd/yyyy") : String.Empty,

                            (currentSummary.NextThresholdDate.HasValue) ? currentSummary.NextThresholdDate.Value.ToString ("MM/dd/yyyy") : String.Empty,

                            currentSummary.StatusText

                        );

                    }

                }

                PopulationCareManagementGrid.DataSource = membershipTable;

            }

            return;

        }

        protected void PopulationCareManagementGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            System.Data.DataTable membershipTable = (System.Data.DataTable) Session[SessionCachePrefix + "PopulationCareManagementGrid.MembershipTable"];

            System.Data.DataTable serviceEventTable = (System.Data.DataTable) Session[SessionCachePrefix + "PopulationCareManagementGrid.ServiceEventTable"];

            System.Data.DataTable triggerEventTable = (System.Data.DataTable) Session[SessionCachePrefix + "PopulationCareManagementGrid.TriggerEventTable"];

            if ((eventArgs.CommandName == "ExpandCollapse")) {

                Telerik.Web.UI.GridDataItem gridItem = (Telerik.Web.UI.GridDataItem) eventArgs.Item;

                Int64 populationMembershipId;

                if (Int64.TryParse (gridItem["PopulationMembershipId"].Text, out populationMembershipId)) {

                    #region Service Event Table

                    serviceEventTable.Rows.Clear ();

                    
                    foreach (Mercury.Server.Application.PopulationMembershipServiceEventDataView currentServiceEvent in MercuryApplication.PopulationMembershipServiceEventsDataView (populationMembershipId)) {

                        serviceEventTable.Rows.Add (

                            currentServiceEvent.PopulationMembershipId.ToString (),

                            currentServiceEvent.ServiceName,

                            (currentServiceEvent.ExpectedEventDate.HasValue) ? currentServiceEvent.ExpectedEventDate.Value.ToString ("MM/dd/yyyy") : String.Empty,

                            (currentServiceEvent.EventDate.HasValue) ? currentServiceEvent.EventDate.Value.ToString ("MM/dd/yyyy") : String.Empty,

                            (currentServiceEvent.PreviousThresholdDate.HasValue) ? currentServiceEvent.PreviousThresholdDate.Value.ToString ("MM/dd/yyyy") : String.Empty,

                            (currentServiceEvent.NextThresholdDate.HasValue) ? currentServiceEvent.NextThresholdDate.Value.ToString ("MM/dd/yyyy") : String.Empty,

                            currentServiceEvent.StatusText

                        );

                    }


                    PopulationCareManagementGrid.MasterTableView.DetailTables[0].DataSource = serviceEventTable;

                    PopulationCareManagementGrid.MasterTableView.DetailTables[0].DataBind ();

                    #endregion 
                    
                    #region Trigger Event Table

                    triggerEventTable.Rows.Clear ();

                    foreach (Mercury.Server.Application.PopulationMembershipTriggerEventDataView currentTriggerEvent in MercuryApplication.PopulationMembershipTriggerEventsDataView (populationMembershipId)) {

                        String triggerName = "** Trigger has been deleted.";

                        if (!currentTriggerEvent.IsTriggerDeleted) {

                            switch (currentTriggerEvent.EventType) {

                                case Mercury.Server.Application.PopulationTriggerEventType.Service:

                                    triggerName = "<span title=\"" + currentTriggerEvent.MemberServiceId.ToString () + "\">" + currentTriggerEvent.ServiceName + "</span>";

                                    break;

                                case Mercury.Server.Application.PopulationTriggerEventType.Metric:

                                    triggerName = "<span title=\"" + currentTriggerEvent.MemberMetricId.ToString () + "\">" + currentTriggerEvent.MetricName + "</span>";

                                    break;

                                case Mercury.Server.Application.PopulationTriggerEventType.AuthorizedService:

                                    triggerName = "<span title=\"" + currentTriggerEvent.MemberAuthorizedServiceId.ToString () + "\">" + currentTriggerEvent.AuthorizedServiceName + "</span>";

                                    break;

                            }

                        }

                        triggerEventTable.Rows.Add (

                            currentTriggerEvent.PopulationMembershipId.ToString (),

                            currentTriggerEvent.PopulationMembershipTriggerEventId.ToString (),

                            currentTriggerEvent.EventType.ToString (),

                            triggerName,

                            currentTriggerEvent.TriggerDate.ToString ("MM/dd/yyyy"),

                            currentTriggerEvent.EventDate.ToString ("MM/dd/yyyy"),

                            currentTriggerEvent.ProblemStatementId.ToString (),

                            currentTriggerEvent.ActionDescription

                        );

                    }


                    PopulationCareManagementGrid.MasterTableView.DetailTables[1].DataSource = triggerEventTable;

                    PopulationCareManagementGrid.MasterTableView.DetailTables[1].DataBind ();

                    #endregion 
                }

            }

            return;

        }

        #endregion


        #region Member Search Event

        protected void MemberSearchControl_OnMemberSelected (Object sender, System.EventArgs eventArgs) {

            // Response.Redirect ("/Application/MemberProfile/MemberProfile.aspx?MemberId=" + MemberSearchControl.SelectedMemberId.ToString (), true);

            return;

        }

        #endregion


    }

}


/*
 * 


* 
<%@ Register TagPrefix="MercuryUserControl" TagName="MemberCaseView" Src="~/Application/Controls/MemberCaseView.ascx"  %>
 * 
                    
                    <MercuryUserControl:MemberCaseView ID="MemberCaseViewControl" runat="server" />
 */

