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

    public partial class EntityInformationMember : UserControl {

        #region Private Properties

        private Client.Application MercuryApplication = null;

        private WindowManager.WindowManager WindowManager = null;

        private WindowManager.Window window = null;


        private Client.Core.Member.Member member = null;

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

        public String EntityNameLabel { get { return EntityNameLabelControl.Text; } set { EntityNameLabelControl.Text = value; } }

        public Int64 EntityId {

            set {

                // SET BUSY, SPAWN BACKGROUND PROCESS

                Dispatcher.BeginInvoke ((Action)delegate { InitializeByEntityId (value); });

            }

        }

        #endregion


        #region Constructors

        public EntityInformationMember () {

            InitializeComponent ();


            // PUT ASSIGNMENT INTO PROTECTED CONSTRUCTOR SO THAT PREVIEW IS AVAILABLE IN DESIGN WINDOW

            if (Application.Current is Mercury.Silverlight.App) {

                MercuryApplication = ((App)Application.Current).MercuryApplication;

                WindowManager = ((Mercury.Silverlight.App)Application.Current).WindowManager;

            }

            return;

        }

        #endregion


        #region Initializations

        public void InitializeByEntityId (Int64 entityId) {

            WindowManager.Window_OnGlobalProgressBarShow (this, new EventArgs ());

            MercuryApplication.MemberGetDemographicsByEntityId (entityId, true, InitializeMemberInformation);

            return;

        }

        public void InitializeMemberInformation (Object sender, Server.Application.MemberGetDemographicsByEntityIdCompletedEventArgs e) {

            WindowManager.Window_OnGlobalProgressBarHide (this, new EventArgs ());

            if (member == null) { return; }


            //#region Note Alert Icons

            //Client.Core.Entity.EntityNote entityNote = null;

            //entityNote = MercuryApplication.EntityNoteGetMostRecentByImportance (member.EntityId, Server.Core.Enumerations.NoteImportance.Warning);

            //if (entityNote != null) {

            //    if (entityNote.TerminationDate >= DateTime.Today) {

            //        EntityNoteWarning.Visibility = Visibility.Visible;

            //        // TODO: SET TOOL TIP FOR NOTE

            //        // EntityNoteWarning.Attributes.Add ("title", "[" + entityNote.NoteTypeName + "] " + entityNote.Subject);

            //    }

            //}

            //entityNote = MercuryApplication.EntityNoteGetMostRecentByImportance (member.EntityId, Server.Core.Enumerations.NoteImportance.Critical);

            //if (entityNote != null) {

            //    if (entityNote.TerminationDate >= DateTime.Today) {

            //        EntityNoteCritical.Visibility = Visibility.Visible;

            //        // TODO: SET TOOL TIP FOR NOTE

            //        // EntityNoteWarning.Attributes.Add ("title", "[" + entityNote.NoteTypeName + "] " + entityNote.Subject);

            //    }

            //}

            //#endregion


            //MemberName.Text = member.Entity.Name;

            //MemberBirthDate.Text = member.BirthDateDescription;

            //MemberAge.Text = member.CurrentAgeDescription;

            //MemberGender.Text = member.GenderDescription;

            //MemberEnrollmentProgramName.Text = "** Not Enrolled";

            //MemberEnrollmentProgramMemberId.Text = "**Not Enrolled";

            //if (member.HasCurrentEnrollment) {

            //    MemberEnrollmentProgramName.Text = member.CurrentEnrollment.Program.Name;

            //    MemberEnrollmentProgramMemberId.Text = member.CurrentEnrollment.ProgramMemberId;

            //    if (member.HasCurrentEnrollmentCoverage) {

            //        MemberEnrollmentBenefitPlan.Text = member.CurrentEnrollmentCoverage.BenefitPlanName;

            //        MemberEnrollmentCoverageLevel.Text = member.CurrentEnrollmentCoverage.CoverageLevelName;

            //        MemberEnrollmentCoverageType.Text = member.CurrentEnrollmentCoverage.CoverageTypeName;

            //        MemberEnrollmentRateCode.Text = member.CurrentEnrollmentCoverage.RateCode;

            //    }

            //    if (member.HasCurrentEnrollmentPcp) {

            //        MemberEnrollmentPcpName.Text = member.CurrentEnrollmentPcp.PcpProvider.Name;

            //        MemberEnrollmentPcpAffiliateName.Text = member.CurrentEnrollmentPcp.ProviderAffiliation.AffiliateProvider.Name;

            //    }

            //}


            return;

        }

        #endregion


        #region Control Events

        private void ToggleEntityInformationMoreLink_Click (object sender, RoutedEventArgs e) {

            TextBlock content = new TextBlock ();

            switch (EntityInformationMore.Visibility) {

                case System.Windows.Visibility.Collapsed:

                    EntityInformationMore.Visibility = System.Windows.Visibility.Visible;

                    content.Text = "(less)";
                    
                    break;

                case System.Windows.Visibility.Visible:

                    EntityInformationMore.Visibility = System.Windows.Visibility.Collapsed;

                    content.Text = "(more)";

                    break;

            }

            ToggleEntityInformationMoreLink.Content = content;

            return;

        }

        #endregion

    }

}
