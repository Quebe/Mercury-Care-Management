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

namespace Mercury.Web.Application.MemberProfile {

    public partial class MemberProfileDemographics : System.Web.UI.UserControl {

        #region Session Properties

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application) Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        public String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (UserControlInstanceId.Text)) { UserControlInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return UserControlInstanceId.Text + ".";

            }

        }

        public Client.Core.Member.Member Member {

            get { return (Client.Core.Member.Member) Session[SessionCachePrefix + "Member"]; }

            set {

                Client.Core.Member.Member member = (Client.Core.Member.Member) Session[SessionCachePrefix + "Member"];

                if (member != value) {

                    Session[SessionCachePrefix + "Member"] = value;
 
                    InitializeMemberDemographics ();

                }

            }

        }

        #endregion


        #region Public Properties

        public String InstanceId { 
            
            get { return UserControlInstanceId.Text; } 
            
            set { 
                
                UserControlInstanceId.Text = value;

                EntityAddressHistoryControl.InstanceId = value + "EntityAddressHistoryControl";

                EntityContactInformationHistoryControl.InstanceId = value + "EntityContactInformationHistoryControl";
            
            } 
        
        }

        public Boolean AllowUserInteraction {

            get {

                Boolean allowUserInteraction = false;

                if (Session[SessionCachePrefix + "AllowUserInteraction"] != null) {

                    allowUserInteraction = (Boolean) Session[SessionCachePrefix + "AllowUserInteraction"];

                }

                return allowUserInteraction;

            }

            set {

                Session[SessionCachePrefix + "AllowUserInteraction"] = value;

                EntityAddressHistoryControl.AllowUserInteraction = value;

                EntityContactInformationHistoryControl.AllowUserInteraction = value;

            }

        }

        public Boolean AllowAddressAction { get { return ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MemberAddressManage)) && (AllowUserInteraction)); } }

        public Boolean AllowContactInformationAction { get { return ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MemberContactInformationManage)) && (AllowUserInteraction)); } }

        #endregion 


        #region Page Events

        public void Page_Load (Object sender, EventArgs e) {

            EntityAddressHistoryControl.InstanceId = InstanceId + "EntityAddressHistoryControl";

            EntityContactInformationHistoryControl.InstanceId = InstanceId + "EntityContactInformationHistoryControl";

            return;

        }

        #endregion 


        #region Initialization

        public void InitializeMemberDemographics (Int64 memberId) {

            if (memberId == 0) { return; }

            Member = MercuryApplication.MemberGetDemographics (memberId, false);

            return;

        }

        public void InitializeMemberDemographicsByEntityId (Int64 entityId) {

            if (entityId == 0) { return; }

            Member = MercuryApplication.MemberGetDemographicsByEntityId (entityId, true);

            return;

        }

        private void InitializeMemberDemographics () {

            if (Member == null) { return; }

//             MemberDemographicHeaderLabel.Text = member.Entity.Name + " (" + member.CurrentAge + " | " + member.GenderDescription + ") ";

            MemberDemographicUniqueId.Text = Member.Entity.UniqueId;


            MemberDemographicName.Text = CommonFunctions.MemberProfileAnchor (Member.Id, Member.Name);

            MemberDemographicGender.Text = Member.GenderDescription;

            MemberDemographicBirthDate.Text = Member.BirthDate.ToString ("MM/dd/yyyy");

            MemberDemographicDeathDate.Text = Member.DeathDateDescription;

            MemberDemographicCurrentAge.Text = Member.CurrentAgeDescription;

            MemberDemographicsFederalTaxId.Text = Member.Entity.FederalTaxId;

            
            MemberDemographicEthnicity.Text = Member.EthnicityDescription;

            MemberDemographicLanguage.Text = Member.LanguageDescription;

            MemberDemographicCitizenship.Text = Member.CitizenshipDescription;

            MemberDemographicMaritalStatus.Text = Member.MaritalStatusDescription;


            if (Member.Entity != null) {

                InitializeMemberContacts (Member);

                // InitializeMemberAddresses (Member, true);

                EntityAddressHistoryControl.Entity = Member.Entity;

                EntityContactInformationHistoryControl.Entity = Member.Entity;

            }


            InitializeMemberCurrentEnrollment (Member);

            InitializeMemberRelationships (Member);

            return;

        }

        private void InitializeMemberContacts (Mercury.Client.Core.Member.Member member) {

            MemberDemographicTelephone.Text = (member.Entity.CurrentContactInformation (Mercury.Server.Application.EntityContactType.Telephone) != null) ? member.Entity.CurrentContactInformation (Mercury.Server.Application.EntityContactType.Telephone).NumberFormatted : "&nbsp";

            MemberDemographicEmail.Text = (member.Entity.CurrentContactInformation (Mercury.Server.Application.EntityContactType.Email) != null) ? member.Entity.CurrentContactInformation (Mercury.Server.Application.EntityContactType.Email).Email : "&nbsp";

            MemberDemographicEmergencyPhone.Text = (member.Entity.CurrentContactInformation (Mercury.Server.Application.EntityContactType.EmergencyPhone) != null) ? member.Entity.CurrentContactInformation (Mercury.Server.Application.EntityContactType.EmergencyPhone).NumberFormatted : "&nbsp";

            MemberDemographicFax.Text = (member.Entity.CurrentContactInformation (Mercury.Server.Application.EntityContactType.Facsimile) != null) ? member.Entity.CurrentContactInformation (Mercury.Server.Application.EntityContactType.Facsimile).NumberFormatted : "&nbsp";

            MemberDemographicPager.Text = (member.Entity.CurrentContactInformation (Mercury.Server.Application.EntityContactType.Pager) != null) ? member.Entity.CurrentContactInformation (Mercury.Server.Application.EntityContactType.Pager).NumberFormatted : "&nbsp";
            
            return;

        }

        //public void InitializeMemberAddresses (Mercury.Client.Core.Member.Member member, Boolean useCaching) {

        //    System.Data.DataTable addressTable = new DataTable ();

        //    addressTable.Columns.Add ("EntityAddressId");

        //    addressTable.Columns.Add ("AddressType");

        //    addressTable.Columns.Add ("Line1");

        //    addressTable.Columns.Add ("Line2");

        //    addressTable.Columns.Add ("CityStateZipCode");

        //    addressTable.Columns.Add ("EffectiveDate");

        //    addressTable.Columns.Add ("TerminationDate");

        //    addressTable.Columns.Add ("IsActive");

        //    addressTable.Columns.Add ("Action");

        //    addressTable.Columns.Add ("ActionDisplay");

        //    System.Collections.Generic.List<Mercury.Client.Core.Entity.EntityAddress> entityAddresses = new System.Collections.Generic.List<Mercury.Client.Core.Entity.EntityAddress> ();

        //    entityAddresses = MercuryApplication.EntityAddressesGet  (member.EntityId, false);

        //    foreach (Client.Core.Entity.EntityAddress currentAddress in entityAddresses) {

        //        // START

        //        String anchorText = String.Empty;

        //        String parameterString = String.Empty;

        //        if ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MemberAddressManage)) && (currentAddress.IsActive) && (AllowUserInteraction)) {

        //            parameterString = String.Empty;

        //            parameterString = parameterString + "'" + currentAddress.Id.ToString () + "'" + ", ";

        //            parameterString = parameterString + "'" + currentAddress.AddressTypeDescription.Replace ("'", @"'\") + "'" + ", ";

        //            parameterString = parameterString + "'" + currentAddress.Line1.Replace ("'", @"'\") + "'" + ", ";

        //            parameterString = parameterString + "'" + currentAddress.Line2.Replace ("'", @"'\") + "'" + ", ";

        //            parameterString = parameterString + "'" + currentAddress.CityStateZipCode.Replace ("'", @"'\") + "'" + ", ";

        //            parameterString = parameterString + "'" + currentAddress.EffectiveDate.ToString ("MM/dd/yyyy") + "'" + ",";

        //            parameterString = parameterString + "'0||'";

        //            anchorText = "<a href=\"javascript:EntityAddressTerminate (" + parameterString + ");" + "\">(terminate)</a>";

        //        }

        //        // END

        //        addressTable.Rows.Add (

        //            currentAddress.Id,

        //            currentAddress.AddressTypeDescription,

        //            currentAddress.Line1,

        //            currentAddress.Line2,

        //            currentAddress.CityStateZipCode,

        //            currentAddress.EffectiveDate.ToString ("MM/dd/yyyy"),

        //            (currentAddress.TerminationDate.ToString ("MM/dd/yyyy") == "12/31/9999") ? "< active > " : currentAddress.TerminationDate.ToString ("MM/dd/yyyy"),

        //            currentAddress.IsActive,

        //            anchorText, 

        //            (AllowAddressAction) ? ";" : "none;"

        //        );

        //    }  /* END FOREACH */

        //    if (entityAddresses.Count == 0) {

        //        addressTable.Rows.Add ("** No Active Addresses", String.Empty, String.Empty, String.Empty);

        //    }

        //    MemberAddressRepeater.DataSource = addressTable;

        //    MemberAddressRepeater.DataBind ();


        //    return;

        //}

        private void InitializeMemberCurrentEnrollment (Mercury.Client.Core.Member.Member member) {

            if (member.HasCurrentEnrollment) {

                MemberDemographicEnrollmentInsurer.Text = member.CurrentEnrollment.InsurerName;

                MemberDemographicEnrollmentProgram.Text = member.CurrentEnrollment.ProgramName;

                MemberDemographicEnrollmentInsuranceType.Text = (member.CurrentEnrollment.Program != null) ? member.CurrentEnrollment.Program.InsuranceTypeName : String.Empty;


                MemberDemographicEnrollmentSponsor.Text = member.CurrentEnrollment.SponsorName;

                MemberDemographicEnrollmentSubscriber.Text = member.CurrentEnrollment.SubscriberName;

                MemberDemographicEnrollmentMemberProgramId.Text = member.CurrentEnrollment.ProgramMemberId;


                MemberDemographicEnrollmentEffective.Text = member.CurrentEnrollment.EffectiveDate.ToString ("MM/dd/yyyy");

                MemberDemographicEnrollmentTermination.Text = (member.CurrentEnrollment.TerminationDate > DateTime.Today) ? "active" : member.CurrentEnrollment.TerminationDate.ToString ("MM/dd/yyyy");


                if (member.CurrentEnrollment.HasCurrentCoverage) {

                    MemberDemographicCoverageBenefitPlan.Text = member.CurrentEnrollmentCoverage.BenefitPlanName;

                    MemberDemographicCoverageType.Text = member.CurrentEnrollmentCoverage.CoverageTypeName;

                    MemberDemographicCoverageLevel.Text = member.CurrentEnrollmentCoverage.CoverageLevelName;

                    MemberDemographicCoverageRateCode.Text = member.CurrentEnrollmentCoverage.RateCode;

                    MemberDemographicCoverageEffectiveDate.Text = member.CurrentEnrollmentCoverage.EffectiveDate.ToString ("MM/dd/yyyy");

                    MemberDemographicCoverageTerminationDate.Text = (member.CurrentEnrollmentCoverage.TerminationDate > DateTime.Today) ? "active" : member.CurrentEnrollmentCoverage.TerminationDate.ToString ("MM/dd/yyyy");

                }
                
                if (member.CurrentEnrollment.HasCurrentPcp) {

                    MemberDemographicPcpName.Text = CommonFunctions.ProviderProfileAnchor (member.CurrentEnrollment.CurrentPcp.PcpProvider.Id, member.CurrentEnrollment.CurrentPcp.PcpProvider.Name);

                    MemberDemographicPcpAffiliateName.Text = member.CurrentEnrollment.CurrentPcp.PcpAffiliateProvider.Entity.Name;

                    MemberDemographicPcpEffectiveDate.Text = member.CurrentEnrollment.CurrentPcp.EffectiveDate.ToString ("MM/dd/yyyy");

                    MemberDemographicPcpTerminationDate.Text = (member.CurrentEnrollment.CurrentPcp.TerminationDate > DateTime.Today) ? "active" : member.CurrentEnrollment.CurrentPcp.TerminationDate.ToString ("MM/dd/yyyy");

                }

            }

            else {

                // ENROLLMENT

                MemberDemographicEnrollmentSponsor.Text = "** No Current Enrollment";

                MemberDemographicEnrollmentSubscriber.Text = "** No Current Enrollment";

                MemberDemographicEnrollmentInsurer.Text = "** No Current Enrollment";

                MemberDemographicEnrollmentProgram.Text = "** No Current Enrollment";

                MemberDemographicEnrollmentEffective.Text = String.Empty;

                MemberDemographicEnrollmentTermination.Text = String.Empty;


                // NO CHILD COVERAGE 

                MemberDemographicCoverageBenefitPlan.Text = "** No Current Coverage";

                MemberDemographicCoverageLevel.Text = "** No Current Coverage";

                MemberDemographicCoverageRateCode.Text = "** No Current Coverage";

                MemberDemographicCoverageEffectiveDate.Text = String.Empty;

                MemberDemographicCoverageTerminationDate.Text = String.Empty;

                MemberDemographicPcpName.Text = "** No Current PCP Assignment";

                MemberDemographicPcpAffiliateName.Text = "** No Current PCP Assignment";

                MemberDemographicPcpEffectiveDate.Text = String.Empty;

                MemberDemographicPcpTerminationDate.Text = String.Empty;

            }

            return;

        }

        private void InitializeMemberRelationships (Mercury.Client.Core.Member.Member member) {

            System.Data.DataTable relationshipTable = new System.Data.DataTable ();

            relationshipTable.Columns.Add ("FamilyId");

            relationshipTable.Columns.Add ("RelatedMemberName");

            relationshipTable.Columns.Add ("RelatedMemberGender");

            relationshipTable.Columns.Add ("RelatedMemberBirthDate");

            relationshipTable.Columns.Add ("RelatedMemberCurrentAge");

            relationshipTable.Columns.Add ("Relationship");

            relationshipTable.Columns.Add ("EffectiveDate");

            relationshipTable.Columns.Add ("TerminationDate");


            foreach (Client.Core.Member.MemberRelationship currentRelationship in member.Relationships) {

                String relatedMemberAnchor = CommonFunctions.MemberProfileAnchor (currentRelationship.RelatedMemberId, currentRelationship.RelatedMemberName);

                if (currentRelationship.RelationshipId == 18) { relatedMemberAnchor = currentRelationship.RelatedMemberName; }

                relationshipTable.Rows.Add (

                    currentRelationship.FamilyId,

                    CommonFunctions.MemberProfileAnchor (currentRelationship.RelatedMemberId, currentRelationship.RelatedMemberName),

                    currentRelationship.RelatedMemberGender,

                    currentRelationship.RelatedMemberBirthDate.ToString ("MM/dd/yyyy"),

                    currentRelationship.RelatedMemberCurrentAgeText,

                    currentRelationship.RelationshipName,

                    currentRelationship.EffectiveDate.ToString ("MM/dd/yyyy"),

                    currentRelationship.TerminationDate.ToString ("MM/dd/yyyy")

                );

            }

            if (member.Relationships.Count == 0) {

                relationshipTable.Rows.Add ("** No Relationships", String.Empty, String.Empty, String.Empty, String.Empty);

            }


            MemberRelationshipRepeater.DataSource = relationshipTable;

            MemberRelationshipRepeater.DataBind ();

            return;

        }

        #endregion 


        #region Dialog Button Events

        protected void TerminationDialogueButtonSet_OnClick (Object sender, EventArgs eventArgs) {

            if (!TerminationDialogueSetAddressTerminationDate.SelectedDate.HasValue) {

                TerminationDialogueSetAddressTerminationDate.SelectedDate = null;

                return;

            }

            long terminatedEntityAddressId = Convert.ToInt64 (TerminationDialogueTerminatedEntityAddressId.Text);

            Client.Core.Entity.EntityAddress currentEntityAddress = MercuryApplication.EntityAddressGet (terminatedEntityAddressId, false);

            if (Convert.ToDateTime (TerminationDialogueSetAddressTerminationDate.SelectedDate) < currentEntityAddress.EffectiveDate) {

                TerminationDialogueSetAddressTerminationDate.SelectedDate = null;

                return;

            }

            DateTime newTerminationDate = Convert.ToDateTime (TerminationDialogueSetAddressTerminationDate.SelectedDate);

            MercuryApplication.EntityAddressTerminate (currentEntityAddress, newTerminationDate);

            Mercury.Client.Core.Member.Member member = MercuryApplication.MemberGetByEntityId (currentEntityAddress.EntityId, true);

            

            //InitializeMemberAddresses (member, false);

            TerminationDialogueSetAddressTerminationDate.SelectedDate = null;

            return;

        }

        protected void TerminationDialogueButtonCancel_OnClick (Object sender, EventArgs eventArgs) {

            TerminationDialogueSetAddressTerminationDate.SelectedDate = null;

            return;

        }

        #endregion 

    }

}