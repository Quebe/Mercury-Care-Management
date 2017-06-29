using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Controls {

    public partial class EntityContact : System.Web.UI.UserControl {

        #region Private Properties

        private Client.Core.Entity.EntityContact entityContact = null;

        #endregion


        #region State Properties

        public String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (UserControlInstanceId.Text)) { UserControlInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return UserControlInstanceId.Text + "_";

            }

        }

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application)Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        public Client.Core.Entity.Entity Entity {

            get { return (Client.Core.Entity.Entity)Session[SessionCachePrefix + "Entity"]; }

            set {

                Client.Core.Entity.Entity entity = (Client.Core.Entity.Entity)Session[SessionCachePrefix + "Entity"];

                if (entity != value) {

                    Session[SessionCachePrefix + "Entity"] = value;

                    entity = value;


                    // INITIALIZE AND REBIND ENTITY CONTACT INFORMATION

                    if (entity != null) {

                        // MAKE COPY TO EXTEND IT

                        List<Client.Core.Entity.EntityContactInformation> contactInformations = new List<Client.Core.Entity.EntityContactInformation> ();

                        foreach (Client.Core.Entity.EntityContactInformation currentInformation in entity.ContactInformations) {

                            if ((currentInformation.EffectiveDate <= DateTime.Now) && (currentInformation.TerminationDate >= DateTime.Now)) {

                                Client.Core.Entity.EntityContactInformation copiedInformation = currentInformation.Copy ();

                                contactInformations.Add (copiedInformation);

                            }

                        }


                        Client.Core.Entity.EntityContactInformation inPerson = new Client.Core.Entity.EntityContactInformation (MercuryApplication);

                        inPerson.EntityId = entity.Id;

                        inPerson.ContactType = Mercury.Server.Application.EntityContactType.InPerson;

                        contactInformations.Add (inPerson);


                        Client.Core.Entity.EntityContactInformation byMail = new Client.Core.Entity.EntityContactInformation (MercuryApplication);

                        byMail.EntityId = entity.Id;

                        byMail.ContactType = Mercury.Server.Application.EntityContactType.ByMail;

                        contactInformations.Add (byMail);


                        EntityContactInformationGrid.MasterTableView.DataKeyNames = new String[] { "Id", "EntityId", "ContactType" };

                        EntityContactInformationGrid.DataSource = contactInformations;

                        EntityContactInformationGrid.DataBind ();


                        if (EntityContactInformationGrid.Items.Count > 0) { EntityContactInformationGrid.Items[0].Selected = true; }

                    }
                    
                }

            }

        }

        public Client.Core.Entity.Entity RelatedEntity {

            get { return (Client.Core.Entity.Entity)Session[SessionCachePrefix + "RelatedEntity"]; }

            set {

                Client.Core.Entity.Entity relatedEntity = (Client.Core.Entity.Entity)Session[SessionCachePrefix + "RelatedEntity"];

                if (relatedEntity != value) {

                    Session[SessionCachePrefix + "RelatedEntity"] = value;

                    relatedEntity = value;


                    // INITIALIZE AND REBIND RELATED ENTITY INFORMATION

                    if (relatedEntity != null) {

                        InitializeRelatedEntityInformation (relatedEntity);

                    }

                }

            }

        }

        public Telerik.Web.UI.RadAjaxManager TelerikAjaxManager { get { return (Telerik.Web.UI.RadAjaxManager)Page.FindControl ("TelerikAjaxManager"); } }

        #endregion 


        #region Control Properties

        public Boolean AllowEditRelatedEntity { get; set; } // TODO: SETUP EDIT RELATED ENTITY

        public Boolean AllowEditContactDateTime {

            get { return ContactDateTime.Enabled; }

            set {

                ContactDateTimeLabel.Visible = value;

                ContactDateTime.Visible = value;

                ContactDateTime.Enabled = value;

            }

        }

        public Boolean AllowEditRegarding { 
            
            get { return ContactRegarding.Enabled; } 
            
            set { 
                
                ContactRegarding.Enabled = value;

                ContactRegarding.Visible = value;

                ContactRegardingLabel.Visible = !value;
            
            } 
        
        }

        public Boolean AllowCancel { get { return ButtonCancel.Enabled; } set { ButtonCancel.Enabled = value; } }

        public String RegardingMessage { 
            
            get { return ContactRegarding.Text; } 
            
            set {

                Int64 contactRegardingId = MercuryApplication.CoreObjectGetIdByName ("ContactRegarding", value);

                if (contactRegardingId != 0) { ContactRegarding.SelectedValue = contactRegardingId.ToString (); }

                else { ContactRegarding.Text = value; }

                ContactRegardingLabel.Text = value;
            
            } 
        
        }

        public String IntroductionScript { get { return ContactIntroductionScript.Text; } set { ContactIntroductionScript.Text = value; } }

        #endregion 


        #region Control Event 
        
        public Client.Core.Entity.EntityContact EntityContactInstance { get { return entityContact; } }

        public event EventHandler<ContactEntityEventArgs> Contact;

        #endregion 


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }


            if (!ContactDateTime.SelectedDate.HasValue) { ContactDateTime.SelectedDate = DateTime.Now; }


            foreach (Client.Core.Reference.ContactRegarding currentContactRegarding in MercuryApplication.ContactRegardingsAvailable (true)) {

                if (currentContactRegarding.Enabled && currentContactRegarding.Visible) {

                    ContactRegarding.Items.Add (CreateRadComboBoxItem (currentContactRegarding.Name, currentContactRegarding.Id.ToString (), false));

                }

            }

            ContactRegarding.AllowCustomText = true;


            if (TelerikAjaxManager != null) {

                TelerikAjaxManager.ResponseScripts.Add ("setTimeout('EntityContactControl_OnPaint()', 250);");
                
            }

            return;

        }

        private Telerik.Web.UI.RadComboBoxItem CreateRadComboBoxItem (String text, String value, Boolean isSelected) {

            Telerik.Web.UI.RadComboBoxItem item = new Telerik.Web.UI.RadComboBoxItem ();

            item = new Telerik.Web.UI.RadComboBoxItem ();

            item.Text = text;

            item.Value = value;

            item.Selected = isSelected;

            return item;

        }

        #endregion 


        #region Initializations

        private void InitializeRelatedEntityInformation (Client.Core.Entity.Entity relatedEntity) {

            switch (relatedEntity.EntityType) {

                case Mercury.Server.Application.EntityType.Member:

                    RelatedMemberInformation.Style.Clear ();

                    InitializeRelatedMemberInformationByEntityId (relatedEntity.Id);

                    break;

                case Mercury.Server.Application.EntityType.Provider:

                    RelatedProviderInformation.Style.Clear ();

                    Client.Core.Provider.Provider provider = MercuryApplication.ProviderGetByEntityId (relatedEntity.Id, true);

                    if (provider != null) { InitializeProviderInformation (provider.Id); }

                    break;

            }

            return;

        }


        private void InitializeRelatedMemberInformation (Client.Core.Member.Member member) {

            RelatedMemberInformation.Style.Clear ();


            #region Note Alert Icons

            Dictionary<Mercury.Server.Application.NoteImportance, Client.Core.Entity.EntityNote> entityNotes;

            entityNotes = MercuryApplication.EntityNoteGetMostRecentByAllImportances (member.EntityId, true);


            Client.Core.Entity.EntityNote entityNote = null;

            // entityNote = MercuryApplication.EntityNoteGetMostRecentByImportance (member.EntityId, Mercury.Server.Application.NoteImportance.Warning, false);

            if (entityNotes.ContainsKey (Mercury.Server.Application.NoteImportance.Warning)) { entityNote = entityNotes[Mercury.Server.Application.NoteImportance.Warning]; }

            if (entityNote != null) {

                if (entityNote.TerminationDate >= DateTime.Today) {

                    RelatedMemberInformationMemberNoteWarning.Style.Clear ();

                    RelatedMemberInformationMemberNoteWarning.Attributes.Add ("title", "[" + entityNote.NoteTypeName + "] " + entityNote.Subject);

                    RelatedMemberInformationMemberNoteWarning.Visible = true;

                }

            }

            // entityNote = MercuryApplication.EntityNoteGetMostRecentByImportance (member.EntityId, Mercury.Server.Application.NoteImportance.Critical, false);

            entityNote = null;

            if (entityNotes.ContainsKey (Mercury.Server.Application.NoteImportance.Critical)) { entityNote = entityNotes[Mercury.Server.Application.NoteImportance.Critical]; }

            if (entityNote != null) {

                if (entityNote.TerminationDate >= DateTime.Today) {

                    RelatedMemberInformationMemberNoteCritical.Style.Clear ();

                    RelatedMemberInformationMemberNoteCritical.Attributes.Add ("title", "[" + entityNote.NoteTypeName + "] " + entityNote.Subject);

                    RelatedMemberInformationMemberNoteCritical.Visible = true;

                }

            }

            #endregion


            RelatedMemberInformationMemberName.Text = Web.CommonFunctions.MemberProfileAnchor (member.Id, member.Name);

            RelatedMemberInformationMemberBirthDate.Text = member.BirthDate.ToString ("MM/dd/yyy");

            RelatedMemberInformationMemberAge.Text = member.CurrentAge.ToString ();

            RelatedMemberInformationMemberGender.Text = member.GenderDescription;

            RelatedMemberInformationMemberProgram.Text = "** Not Enrolled";

            RelatedMemberInformationMemberProgramMemberId.Text = "**Not Enrolled";

            if (member.HasCurrentEnrollment) {

                // RelatedMemberInformationMemberProgram.Text = member.CurrentEnrollment.Program.Name;

                //String anchor = String.Empty;

                //anchor = "<a href=\"#\" onclick=\"javascript:MemberInformationCoverage_Toggle()\"' title=\"Toggle Coverage Information\" alt=\"Toggle Coverage Information\">" + member.CurrentEnrollment.ProgramName + "</a>";

                //RelatedMemberInformationMemberProgram.Text = anchor;

                RelatedMemberInformationMemberProgram.Text = member.CurrentEnrollment.ProgramName;

                RelatedMemberInformationMemberProgramMemberId.Text = member.CurrentEnrollment.ProgramMemberId;


                if (member.CurrentEnrollment.HasCurrentCoverage) {

                    RelatedMemberInformationMemberCoverageBenefitPlan.Text = member.CurrentEnrollment.CurrentCoverage.BenefitPlanName;

                    RelatedMemberInformationMemberCoverageType.Text = member.CurrentEnrollment.CurrentCoverage.CoverageTypeName;

                    RelatedMemberInformationMemberCoverageLevel.Text = member.CurrentEnrollment.CurrentCoverage.CoverageLevelName;

                    RelatedMemberInformationMemberCoverageRateCode.Text = member.CurrentEnrollment.CurrentCoverage.RateCode;

                }

                if (member.CurrentEnrollment.HasCurrentPcp) {

                    RelatedMemberInformationMemberPcpName.Text = Web.CommonFunctions.ProviderProfileAnchor (

                        member.CurrentEnrollmentPcp.PcpProviderId, member.CurrentEnrollmentPcp.PcpProvider.Name);

                    RelatedMemberInformationMemberPcpAffiliateName.Text = Web.CommonFunctions.ProviderProfileAnchor (

                        member.CurrentEnrollmentPcp.PcpAffiliateProvider.Id, member.CurrentEnrollmentPcp.PcpAffiliateProvider.Name);

                }

            }

            return;

        }

        private void InitializeRelatedMemberInformationByEntityId (Int64 entityId) {

            Client.Core.Member.Member member = MercuryApplication.MemberGetDemographicsByEntityId (entityId, true);

            if (member == null) { return; }

            InitializeRelatedMemberInformation (member);

            return;

        }

        private void InitializeMemberInformation (Int64 memberId) {

            Client.Core.Member.Member member = MercuryApplication.MemberGetDemographics (memberId, true);

            if (member == null) { return; }

            InitializeRelatedMemberInformation (member);

            return;

        }


        private void InitializeProviderInformation (Int64 providerId) {

            Client.Core.Provider.Provider provider = MercuryApplication.ProviderGet (providerId, true);

            if (provider == null) { return; }


            RelatedProviderInformation.Style.Clear ();


            #region Note Alert Icons

            Dictionary<Mercury.Server.Application.NoteImportance, Client.Core.Entity.EntityNote> entityNotes;

            entityNotes = MercuryApplication.EntityNoteGetMostRecentByAllImportances (provider.EntityId, true);


            Client.Core.Entity.EntityNote entityNote = null;

            // entityNote = MercuryApplication.EntityNoteGetMostRecentByImportance (provider.EntityId, Mercury.Server.Application.NoteImportance.Warning, false);

            if (entityNotes.ContainsKey (Mercury.Server.Application.NoteImportance.Warning)) { entityNote = entityNotes[Mercury.Server.Application.NoteImportance.Warning]; }

            if (entityNote != null) {

                if (entityNote.TerminationDate >= DateTime.Today) {

                    RelatedProviderInformationProviderNoteWarning.Style.Clear ();

                    RelatedProviderInformationProviderNoteWarning.Attributes.Add ("title", "[" + entityNote.NoteTypeName + "] " + entityNote.Subject);

                    RelatedProviderInformationProviderNoteWarning.Visible = true;

                }

            }

            // entityNote = MercuryApplication.EntityNoteGetMostRecentByImportance (provider.EntityId, Mercury.Server.Application.NoteImportance.Critical, false);

            entityNote = null;

            if (entityNotes.ContainsKey (Mercury.Server.Application.NoteImportance.Critical)) { entityNote = entityNotes[Mercury.Server.Application.NoteImportance.Critical]; }

            if (entityNote != null) {

                if (entityNote.TerminationDate >= DateTime.Today) {

                    RelatedProviderInformationProviderNoteCritical.Style.Clear ();

                    RelatedProviderInformationProviderNoteCritical.Attributes.Add ("title", "[" + entityNote.NoteTypeName + "] " + entityNote.Subject);

                    RelatedProviderInformationProviderNoteCritical.Visible = true;

                }

            }

            #endregion


            RelatedProviderInformationProviderName.Text = Web.CommonFunctions.ProviderProfileAnchor (providerId, provider.Name);

            RelatedProviderInformationProviderNpi.Text = (String.IsNullOrEmpty (provider.NationalProviderId)) ? "** Not Assigned" : provider.NationalProviderId;

            RelatedProviderInformationProviderProgram.Text = "** Not Enrolled";

            RelatedProviderInformationProviderProgramProviderId.Text = "**Not Enrolled";

            if (provider.HasCurrentEnrollment) {

                RelatedProviderInformationProviderProgram.Text = provider.CurrentEnrollment.Program.Name;

                RelatedProviderInformationProviderProgramProviderId.Text = provider.CurrentEnrollment.ProgramProviderId;

            }

            return;

        }

        #endregion 


        #region Control Events

        protected void ButtonOk_OnClick (Object sender, EventArgs e) {

            Telerik.Web.UI.GridDataItem selectedGridItem = (Telerik.Web.UI.GridDataItem)EntityContactInformationGrid.SelectedItems[0];

            Telerik.Web.UI.DataKey selectedDataKey = selectedGridItem.OwnerTableView.DataKeyValues[selectedGridItem.ItemIndex];


            if (String.IsNullOrEmpty (ContactRegarding.Text)) {

                ActionResponseLabel.Text = "** Contact Regarding is required.";

                ActionResponseLabel.Visible = true;

                return;

            }

            if (!ContactDateTime.SelectedDate.HasValue) {

                ActionResponseLabel.Text = "** Contact Date/Time is required.";

                ActionResponseLabel.Visible = true;

                return;

            }

            else if (ContactDateTime.SelectedDate.Value > DateTime.Now) {

                ActionResponseLabel.Text = "** Contact Date/Time cannot be in the future.";

                ActionResponseLabel.Visible = true;

                return;

            }


            ActionResponseLabel.Text = String.Empty;

            ActionResponseLabel.Visible = false;


            entityContact = new Mercury.Client.Core.Entity.EntityContact (MercuryApplication);

            entityContact.EntityId = Convert.ToInt64 (selectedDataKey["EntityId"]);

            entityContact.RelatedEntityId = (RelatedEntity != null) ? RelatedEntity.Id : 0; 

            entityContact.EntityContactInformationId = Convert.ToInt64 (selectedDataKey["Id"]);


            entityContact.ContactDate = (AllowEditContactDateTime) ? ContactDateTime.SelectedDate.Value : DateTime.Now;

            entityContact.ContactedByName = MercuryApplication.Session.UserDisplayName;


            entityContact.ContactType = (Server.Application.EntityContactType)selectedDataKey["ContactType"];

            entityContact.Direction = (Mercury.Server.Application.ContactDirection)Int32.Parse (ContactDirection.SelectedValue);


            entityContact.ContactRegardingId = (!String.IsNullOrWhiteSpace (ContactRegarding.SelectedValue)) ? Convert.ToInt64 (ContactRegarding.SelectedValue) : 0;               

            entityContact.Regarding = ContactRegarding.Text;

            entityContact.Remarks = ContactRemarks.Text;


            entityContact.Successful = (Int32.Parse (ContactOutcome.SelectedValue) == 1);

            entityContact.ContactOutcome = (Server.Application.ContactOutcome)Int32.Parse (ContactOutcome.SelectedValue);

            if (Contact != null) {

                Contact (this, new ContactEntityEventArgs (Entity, RelatedEntity, entityContact));

            }

            return;

        }

        protected void ButtonCancel_OnClick (Object sender, EventArgs e) {

            if (Contact != null) {

                Contact (this, new ContactEntityEventArgs ());

            }

            return;

        }

        #endregion 

    }

    public class ContactEntityEventArgs : EventArgs {

        private Client.Core.Entity.Entity entity = null;

        private Client.Core.Entity.Entity relatedEntity = null;

        private Client.Core.Entity.EntityContact entityContact = null;

        private Boolean cancel = false;

        public Client.Core.Entity.Entity Entity { get { return entity; } }

        public Client.Core.Entity.Entity RelatedEntity { get { return relatedEntity; } set { relatedEntity = value; } }

        public Client.Core.Entity.EntityContact EntityContact { get { return entityContact; } }

        public Boolean Cancel { get { return cancel; } }

        public ContactEntityEventArgs (Client.Core.Entity.Entity forEntity, Client.Core.Entity.Entity forRelatedEntity, Client.Core.Entity.EntityContact forEntityContact) {

            entity = forEntity;

            relatedEntity = forRelatedEntity;

            entityContact = forEntityContact; 
            
            return; 
        
        }

        public ContactEntityEventArgs () { cancel = true; return; }

    }

}


