using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Controls {

    public partial class EntityInformationMember : System.Web.UI.UserControl {

        #region Private Properties

        private Boolean isPageUnloading = false;

        #endregion 


        #region Public State Properties

        public String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (UserControlInstanceId.Text)) { UserControlInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return UserControlInstanceId.Text + ".";

            }

        }

        public Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application)Session["Mercury.Application"];

                if ((application == null) && (!isPageUnloading)) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        public Client.Core.Member.Member Member { set { InitializeMemberInformation (value); } }

        #endregion 


        #region Constructors

        protected void Page_Load (object sender, EventArgs e) {

            return;

        }

        #endregion 


        private void InitializeMemberInformation (Client.Core.Member.Member member) {

            MemberInformation.Style.Clear ();


            #region Note Alert Icons

            Dictionary<Mercury.Server.Application.NoteImportance, Client.Core.Entity.EntityNote> entityNotes;

            entityNotes = MercuryApplication.EntityNoteGetMostRecentByAllImportances (member.EntityId, true);


            Client.Core.Entity.EntityNote entityNote = null;

            // entityNote = MercuryApplication.EntityNoteGetMostRecentByImportance (member.EntityId, Mercury.Server.Application.NoteImportance.Warning, false);

            if (entityNotes.ContainsKey (Mercury.Server.Application.NoteImportance.Warning)) { entityNote = entityNotes[Mercury.Server.Application.NoteImportance.Warning]; }

            if (entityNote != null) {

                if (entityNote.TerminationDate >= DateTime.Today) {

                    MemberNoteWarning.Style.Clear ();

                    MemberNoteWarning.Style.Add ("padding-right", "4px");

                    MemberNoteWarning.Attributes.Add ("title", "[" + entityNote.NoteTypeName + "] " + entityNote.Subject);

                    MemberNoteWarning.Visible = true;

                }

            }

            // entityNote = MercuryApplication.EntityNoteGetMostRecentByImportance (member.EntityId, Mercury.Server.Application.NoteImportance.Critical, false);

            entityNote = null;

            if (entityNotes.ContainsKey (Mercury.Server.Application.NoteImportance.Critical)) { entityNote = entityNotes[Mercury.Server.Application.NoteImportance.Critical]; }

            if (entityNote != null) {

                if (entityNote.TerminationDate >= DateTime.Today) {

                    MemberNoteCritical.Style.Clear ();

                    MemberNoteCritical.Style.Add ("padding-right", "4px");

                    MemberNoteCritical.Attributes.Add ("title", "[" + entityNote.NoteTypeName + "] " + entityNote.Subject);

                    MemberNoteCritical.Visible = true;

                }

            }

            #endregion


            MemberName.Text = Web.CommonFunctions.MemberProfileAnchor (member.Id, member.Name);

            MemberBirthDate.Text = member.BirthDate.ToString ("MM/dd/yyy");

            MemberAge.Text = member.CurrentAge.ToString ();

            MemberGender.Text = member.GenderDescription;

            MemberProgram.Text = "** Not Enrolled";

            MemberProgramMemberId.Text = "**Not Enrolled";

            if (member.HasCurrentEnrollment) {

                // MemberProgram.Text = member.CurrentEnrollment.Program.Name;

                //String anchor = String.Empty;

                //anchor = "<a href=\"#\" onclick=\"javascript:MemberInformationCoverage_Toggle()\"' title=\"Toggle Coverage Information\" alt=\"Toggle Coverage Information\">" + member.CurrentEnrollment.ProgramName + "</a>";

                //MemberProgram.Text = anchor;

                MemberProgram.Text = member.CurrentEnrollment.ProgramName;

                MemberProgramMemberId.Text = member.CurrentEnrollment.ProgramMemberId;


                if (member.CurrentEnrollment.HasCurrentCoverage) {

                    MemberCoverageBenefitPlan.Text = member.CurrentEnrollment.CurrentCoverage.BenefitPlanName;

                    MemberCoverageType.Text = member.CurrentEnrollment.CurrentCoverage.CoverageTypeName;

                    MemberCoverageLevel.Text = member.CurrentEnrollment.CurrentCoverage.CoverageLevelName;

                    MemberCoverageRateCode.Text = member.CurrentEnrollment.CurrentCoverage.RateCode;

                }

                if (member.CurrentEnrollment.HasCurrentPcp) {

                    MemberPcpName.Text = Web.CommonFunctions.ProviderProfileAnchor (

                        member.CurrentEnrollmentPcp.PcpProviderId, member.CurrentEnrollmentPcp.PcpProvider.Name);

                    MemberPcpAffiliateName.Text = Web.CommonFunctions.ProviderProfileAnchor (

                        member.CurrentEnrollmentPcp.PcpAffiliateProvider.Id, member.CurrentEnrollmentPcp.PcpAffiliateProvider.Name);

                }

            }

            return;

        }

        private void InitializeMemberInformationByEntityId (Int64 entityId) {

            Client.Core.Member.Member member = MercuryApplication.MemberGetDemographicsByEntityId (entityId, true);

            if (member == null) { return; }

            InitializeMemberInformation (member);

            return;

        }

        private void InitializeMemberInformation (Int64 memberId) {

            Client.Core.Member.Member member = MercuryApplication.MemberGetDemographics (memberId, true);

            if (member == null) { return; }

            InitializeMemberInformation (member);

            return;

        }

    }

}