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

    public partial class MemberDemographics : UserControl {

        #region Private Properties

        private Client.Application MercuryApplication = null;

        private WindowManager.WindowManager WindowManager = null;

        private WindowManager.Window window = null;


        private Int64 memberId = 0;

        private Server.Application.Member serverMember = null;

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

                EntityAddressHistoryControl.Window = value;
            
            }
        
        }

        // PUBLIC ACCESSIBLE MEMBER ID PROPERTY FOR SETTING THE MEMBER, THIS CAUSES THE CONTROL TO TRUELY INITIALIZE

        public Int64 MemberId {

            get { return memberId; }

            set {

                // DO NOT COMPARE VALUES, ALLOW RE-ASSIGNMENT TO "REFRESH" DATA

                memberId = value;

                Window.SetExceptionMessage (String.Empty);

                MercuryApplication.MemberGet (memberId, true, InitializeMember);

            }

        }

        #endregion 


        #region Constructors

        public MemberDemographics () {

            InitializeComponent ();

            // PUT ASSIGNMENT INTO PROTECTED CONSTRUCTOR SO THAT PREVIEW IS AVAILABLE IN DESIGN WINDOW

            if (Application.Current is Mercury.Silverlight.App) {

                MercuryApplication = ((App)Application.Current).MercuryApplication;

                WindowManager = ((Mercury.Silverlight.App)Application.Current).WindowManager;

            }

            return;

        }

        #endregion


        #region Initialization

        private void InitializeMember (Object sender, Server.Application.MemberGetCompletedEventArgs e) {

            if (!Window.SetExceptionMessage (e)) {

                serverMember = e.Result;

                member = new Mercury.Client.Core.Member.Member (MercuryApplication, serverMember);

                if (member == null) { Window.SetExceptionMessage ("Unable to find member."); return; }


                // MercuryApplication.MemberEnrollmentsGet (member.MemberId, true, InitializeMemberCurrentEnrollment);


                #region Member Information - Row 1

                MemberUniqueId.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("Entity.UniqueId", member, System.Windows.Data.BindingMode.OneWay));

                MemberName.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("Entity.Name", member, System.Windows.Data.BindingMode.OneWay));

                MemberBirthDate.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("BirthDate", member, System.Windows.Data.BindingMode.OneWay, new Client.ValueConverters.DateToStringFormatter ()));

                MemberDeathDate.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("DeathDateDescription", member, System.Windows.Data.BindingMode.OneWay));

                MemberAge.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("BirthDate", member, System.Windows.Data.BindingMode.OneWay, new Client.ValueConverters.DateToAgeInYearsMonthsString ()));

                MemberGender.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("Gender", member, System.Windows.Data.BindingMode.OneWay, new Client.ValueConverters.GenderDescriptionFormatter ()));

                MemberFederalTaxId.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("Entity.FederalTaxId", member, System.Windows.Data.BindingMode.OneWay));

                #endregion


                #region Member Information - Row 2

                MemberEthnicity.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("EthnicityDescription", member, System.Windows.Data.BindingMode.OneWay));

                #endregion 


                EntityAddressHistoryControl.EntityId = member.EntityId;

                EntityContactInformationHistoryControl.EntityId = member.EntityId;


                #region Member Current Enrollment - Row 1

                MemberCurrentEnrollmentInsurerName.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("CurrentEnrollment.Program.Insurer.Name", member, System.Windows.Data.BindingMode.OneWay));

                MemberCurrentEnrollmentProgramName.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("CurrentEnrollment.Program.Name", member, System.Windows.Data.BindingMode.OneWay));

                MemberCurrentEnrollmentInsuranceType.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("CurrentEnrollment.Program.InsuranceType.Name", member, System.Windows.Data.BindingMode.OneWay));

                MemberCurrentEnrollmentProgramMemberId.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("CurrentEnrollment.ProgramMemberId", member, System.Windows.Data.BindingMode.OneWay));

                #endregion


                #region Member Current Enrollment - Row 2

                MemberCurrentEnrollmentSponsorName.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("CurrentEnrollment.Sponsor.Name", member, System.Windows.Data.BindingMode.OneWay));

                MemberCurrentEnrollmentSubscriberName.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("CurrentEnrollment.Subscriber.Name", member, System.Windows.Data.BindingMode.OneWay));

                MemberCurrentEnrollmentEffectiveDate.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("CurrentEnrollment.EffectiveDateDescription", member, System.Windows.Data.BindingMode.OneWay));

                MemberCurrentEnrollmentTerminationDate.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("CurrentEnrollment.TerminationDateDescription", member, System.Windows.Data.BindingMode.OneWay));

                #endregion


                #region Member Current Coverage - Row 1

                MemberCurrentCoverageBenefitPlanName.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("CurrentEnrollment.CurrentEnrollmentCoverage.BenefitPlan.Name", member, System.Windows.Data.BindingMode.OneWay));

                MemberCurrentCoverageType.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("CurrentEnrollment.CurrentEnrollmentCoverage.CoverageType.Name", member, System.Windows.Data.BindingMode.OneWay));

                MemberCurrentCoverageLevel.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("CurrentEnrollment.CurrentEnrollmentCoverage.CoverageLevel.Name", member, System.Windows.Data.BindingMode.OneWay));

                MemberCurrentCoverageRateCode.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("CurrentEnrollment.CurrentEnrollmentCoverage.RateCode", member, System.Windows.Data.BindingMode.OneWay));

                MemberCurrentCoverageEffectiveDate.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("CurrentEnrollment.CurrentEnrollmentCoverage.EffectiveDateDescription", member, System.Windows.Data.BindingMode.OneWay));

                MemberCurrentCoverageTerminationDate.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("CurrentEnrollment.CurrentEnrollmentCoverage.TerminationDateDescription", member, System.Windows.Data.BindingMode.OneWay));

                #endregion


                #region Member Current Coverage - Row 1

                MemberCurrentEnrollmentPcpName.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("CurrentEnrollment.CurrentEnrollmentPcp.PcpProvider.Name", member, System.Windows.Data.BindingMode.OneWay));

                MemberCurrentEnrollmentPcpAffiliateName.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("CurrentEnrollment.CurrentEnrollmentPcp.ProviderAffiliation.AffiliateProvider.Name", member, System.Windows.Data.BindingMode.OneWay));

                MemberCurrentEnrollmentPcpEffectiveDate.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("CurrentEnrollment.CurrentEnrollmentPcp.EffectiveDateDescription", member, System.Windows.Data.BindingMode.OneWay));

                MemberCurrentEnrollmentPcpTerminationDate.SetBinding (TextBlock.TextProperty, MercuryApplication.PropertyDataBinding ("CurrentEnrollment.CurrentEnrollmentPcp.TerminationDateDescription", member, System.Windows.Data.BindingMode.OneWay));

                #endregion


                MercuryApplication.MemberRelationshipsGet (member.Id, true, InitializeMemberRelationships);

            }

            return;

        }

        private void InitializeMemberRelationships (Object sender, Server.Application.MemberRelationshipsGetCompletedEventArgs e) {

            if ((!Window.SetExceptionMessage (e)) && (!Window.SetExceptionMessage (e.Result))) {

                MemberRelationshipGrid.ItemsSource = Client.Converters.ServerCollectionToClient.MemberRelationshipCollection (MercuryApplication, e.Result.Collection);

            }

            return;

        }


        #endregion 
    }

}
