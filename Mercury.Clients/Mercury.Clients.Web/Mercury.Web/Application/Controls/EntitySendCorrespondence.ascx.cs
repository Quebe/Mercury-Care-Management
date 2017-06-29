using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Controls {

    public partial class EntitySendCorrespondence : System.Web.UI.UserControl {

        #region Private Properties

        private Client.Core.Entity.EntityCorrespondence entityCorrespondence = null;

        private Boolean allowSendByFacsimile = true;

        private Boolean allowSendByEmail = true;

        private Boolean allowSendByInPerson = true;

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

                    InitializeAddressGrid ();

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


        #region Public Properties

        public Int64 CorrespondenceId {

            get {

                Int64 correspondenceId = 0;

                if (Session[SessionCachePrefix + "CorrespondenceId"] != null) {

                    correspondenceId = (Int64)Session[SessionCachePrefix + "CorrespondenceId"];

                }

                return correspondenceId;

            }

            set {

                Session[SessionCachePrefix + "CorrespondenceId"] = value;

                SendCorrespondenceSelection.SelectedValue = value.ToString ();

            }

        }

        public String Attention { get { return SendCorrespondenceAttention.Text; } set { SendCorrespondenceAttention.Text = value; } }

        public Boolean AllowUserSelection { get { return SendCorrespondenceSelection.Enabled; } set { SendCorrespondenceSelection.Enabled = value; } }


        public Boolean AllowAlternateAddress { get { return CorrespondenceUseAlternativeAddress.Enabled; } set { CorrespondenceUseAlternativeAddress.Enabled = value; } }

        public Boolean AllowSendByFacsimile {

            get { return allowSendByFacsimile; }

            set {

                if (allowSendByFacsimile != value) {

                    allowSendByFacsimile = value;

                    AlternativeFaxNumberDiv.Visible = value;

                    InitializeAddressGrid ();

                }

            }

        }

        public Boolean AllowSendByEmail {

            get { return allowSendByEmail; }

            set {

                if (allowSendByEmail != value) {

                    allowSendByEmail = value;

                    AlternativeEmailDiv.Visible = value;

                    InitializeAddressGrid ();

                }

            }

        }

        public Boolean AllowSendByInPerson {

            get { return allowSendByInPerson; }

            set {

                if (allowSendByInPerson != value) {

                    allowSendByInPerson = value;

                    InitializeAddressGrid ();

                }

            }

        }


        public Client.Core.Entity.EntityAddress AlternateAddress {

            set {

                if (value == null) {

                    CorrespondenceUseAlternativeAddress.Checked = false;

                }

                else {

                    CorrespondenceAlternateAddressLine1.Text = value.Line1;

                    CorrespondenceAlternateAddressLine2.Text = value.Line2;

                    CorrespondenceAlternateAddressCity.Text = value.City;

                    CorrespondenceAlternateAddressState.Text = value.State;

                    CorrespondenceAlternateAddressZipCode.Text = value.ZipCode;

                    if (AllowAlternateAddress) {

                        CorrespondenceUseAlternativeAddress.Checked = true;

                        AlternativeAddressDetail.Attributes.Add ("style", "width: 100%");
                    }

                }

            }

        }

        public String AlternateFaxNumber {

            set {

                if ((AllowSendByFacsimile) && (CorrespondenceAlternateFaxNumber.Text != value)) {

                    CorrespondenceAlternateFaxNumber.Text = value;

                    if (!String.IsNullOrEmpty (value)) {

                        CorrespondenceUseAlternativeAddress.Checked = false;

                        CorrespondenceUseAlternativeFaxNumber.Checked = true;

                        CorrespondenceUseAlternativeEmail.Checked = false;

                        UseAlternativeUpdate ();

                    }

                }

            }

        }

        public String AlternateEmail {

            set {

                if ((AllowSendByEmail) && (CorrespondenceAlternateEmail.Text != value)) {

                    CorrespondenceAlternateEmail.Text = value;

                    if (!String.IsNullOrEmpty (value)) {

                        CorrespondenceUseAlternativeAddress.Checked = false;

                        CorrespondenceUseAlternativeFaxNumber.Checked = false;

                        CorrespondenceUseAlternativeEmail.Checked = true;

                        UseAlternativeUpdate ();

                    }

                }

            }

        }


        public Boolean AllowCancel { get { return ButtonCancel.Enabled; } set { ButtonCancel.Enabled = value; } }

        public Boolean AllowHistoricalSendDate {

            get { return (SendCorrespondenceDate.MinDate != DateTime.Today); }

            set {

                SendCorrespondenceDate.MinDate = (value) ? new DateTime (1900, 01, 01) : DateTime.Today;

                SendCorrespondenceDate.Enabled = !((!value) && (!AllowFutureSendDate));

                if (!value) { SendCorrespondenceDate.SelectedDate = DateTime.Today; }

            }

        }

        public Boolean AllowFutureSendDate {

            get { return (SendCorrespondenceDate.MaxDate != DateTime.Today); }

            set {

                SendCorrespondenceDate.MaxDate = (value) ? new DateTime (9999, 12, 31) : DateTime.Today;

                SendCorrespondenceDate.Enabled = !((!value) && (!AllowHistoricalSendDate));

                if (!value) { SendCorrespondenceDate.SelectedDate = DateTime.Today; }

            }

        }


        public DateTime SendDate { get { return (SendCorrespondenceDate.SelectedDate.HasValue) ? SendCorrespondenceDate.SelectedDate.Value : new DateTime (); } set { SendCorrespondenceDate.SelectedDate = value; } }

        #endregion 


        #region Control Event

        public Client.Core.Entity.EntityCorrespondence EntityCorrespondenceInstance { get { return entityCorrespondence; } }

        public event EventHandler<SendCorrespondenceEntityEventArgs> SendCorrespondence;

        #endregion


        #region Page Events

        private void Page_Init (object sender, EventArgs e) {

            AllowHistoricalSendDate = false;

            AllowFutureSendDate = true;

            return;

        }

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }
            
            
            if (!SendCorrespondenceDate.SelectedDate.HasValue) { SendCorrespondenceDate.SelectedDate = DateTime.Now; }

            InitializeCorrespondenceSelection ();

            ActionResponseLabel.Text = String.Empty;


            if (TelerikAjaxManager != null) {

                TelerikAjaxManager.ResponseScripts.Add ("setTimeout('EntitySendCorrespondenceControl_OnPaint()', 500);");

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

        private void InitializeCorrespondenceSelection () {

            if (SendCorrespondenceSelection.Items.Count == 0) {

                List<Mercury.Client.Core.Reference.Correspondence> correspondences = MercuryApplication.CorrespondencesAvailable (true);

                SendCorrespondenceSelection.Items.Clear ();

                foreach (Client.Core.Reference.Correspondence currentCorrespondence in MercuryApplication.CorrespondencesAvailable (true)) {

                    if (currentCorrespondence.Enabled && currentCorrespondence.Visible) {

                        SendCorrespondenceSelection.Items.Add (CreateRadComboBoxItem (currentCorrespondence.Name, currentCorrespondence.Id.ToString (), false));

                    }

                }

                SendCorrespondenceSelection.SelectedValue = CorrespondenceId.ToString ();

            }

            return;

        }

        private void InitializeAddressGrid () {

            System.Data.DataTable addressTable = new System.Data.DataTable ();

            addressTable.Columns.Add ("EntityId");

            addressTable.Columns.Add ("AddressId");

            addressTable.Columns.Add ("ContactType");

            addressTable.Columns.Add ("AddressType");

            addressTable.Columns.Add ("Line1");

            addressTable.Columns.Add ("Line2");

            addressTable.Columns.Add ("CityStateZip");

            addressTable.Columns.Add ("EffectiveDate");

            addressTable.Columns.Add ("TerminationDate");


            if (Entity != null) {

                #region All Current Addresses

                foreach (Client.Core.Entity.EntityAddress currentAddress in Entity.Addresses) {

                    if ((DateTime.Today >= currentAddress.EffectiveDate) && (DateTime.Today <= currentAddress.TerminationDate)) {

                        addressTable.Rows.Add (

                            Entity.Id.ToString (),

                            currentAddress.Id.ToString (),

                            ((Int32) (Mercury.Server.Application.EntityContactType.ByMail)).ToString (),

                            currentAddress.AddressTypeDescription,

                            currentAddress.Line1,

                            currentAddress.Line2,

                            currentAddress.CityStateZipCode,

                            currentAddress.EffectiveDate.ToString ("MM/dd/yyyy"),

                            currentAddress.TerminationDate.ToString ("MM/dd/yyyy")

                            );

                    }

                }

                #endregion 

                #region All Electronic Correspondence Methods

                foreach (Client.Core.Entity.EntityContactInformation currentContactInformation in Entity.ContactInformations) {

                    if ((DateTime.Today >= currentContactInformation.EffectiveDate) && (DateTime.Today <= currentContactInformation.TerminationDate)) {

                        switch (currentContactInformation.ContactType) {

                            case Mercury.Server.Application.EntityContactType.Email:

                                if (allowSendByEmail)  {

                                    addressTable.Rows.Add (

                                        Entity.Id.ToString (),

                                        currentContactInformation.Id.ToString (),

                                        ((Int32)currentContactInformation.ContactType).ToString (),

                                        currentContactInformation.ContactType.ToString (),

                                        currentContactInformation.NumberFormatted,

                                        String.Empty,

                                        String.Empty,

                                        currentContactInformation.EffectiveDate.ToString ("MM/dd/yyyy"),

                                        currentContactInformation.TerminationDate.ToString ("MM/dd/yyyy")

                                        );

                                }

                                break;

                            case Mercury.Server.Application.EntityContactType.Facsimile:

                                if (allowSendByFacsimile) {

                                    addressTable.Rows.Add (

                                        Entity.Id.ToString (),

                                        currentContactInformation.Id.ToString (),

                                        ((Int32)currentContactInformation.ContactType).ToString (),

                                        currentContactInformation.ContactType.ToString (),

                                        currentContactInformation.NumberFormatted,

                                        String.Empty,

                                        String.Empty,

                                        currentContactInformation.EffectiveDate.ToString ("MM/dd/yyyy"),

                                        currentContactInformation.TerminationDate.ToString ("MM/dd/yyyy")

                                        );

                                }

                                break;

                        }

                    }

                }

                #endregion 


                if (allowSendByInPerson) {

                    addressTable.Rows.Add (

                        Entity.Id.ToString (),

                        "0",

                        ((Int32)Mercury.Server.Application.EntityContactType.InPerson).ToString (),

                        "In Person",

                        "&nbsp", "&nbsp", "&nbsp", "&nbsp", "&nbsp");

                }

            }

            EntitySendCorrespondenceInformationGrid.MasterTableView.DataKeyNames = new String[] { "EntityId", "AddressId", "ContactType", "AddressType" };

            EntitySendCorrespondenceInformationGrid.DataSource = addressTable;

            EntitySendCorrespondenceInformationGrid.Rebind ();


            if (EntitySendCorrespondenceInformationGrid.Items.Count > 0) {

                EntitySendCorrespondenceInformationGrid.Items[0].Selected = true;

            }

            return;

        }

        #endregion
        

        #region Initializations - Related Entity

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

            Telerik.Web.UI.GridDataItem selectedGridItem = (Telerik.Web.UI.GridDataItem)EntitySendCorrespondenceInformationGrid.SelectedItems[0];

            Telerik.Web.UI.DataKey selectedDataKey = selectedGridItem.OwnerTableView.DataKeyValues[selectedGridItem.ItemIndex];

            
            System.Text.RegularExpressions.Regex expressionValidator;

            Int64 correspondenceId = 0;

            Client.Core.Reference.Correspondence correspondence = null;

            Client.Core.Entity.EntityContactInformation entityContactInformation = null;


            #region Validation 

            if (String.IsNullOrEmpty (SendCorrespondenceSelection.SelectedValue)) {

                ActionResponseLabel.Text = "** No Correspondence selected to send.";

                return;

            }

            if (!Int64.TryParse (SendCorrespondenceSelection.SelectedValue, out correspondenceId)) {

                ActionResponseLabel.Text = "** No Correspondence Selected.";

                return;

            }

            correspondence = MercuryApplication.CorrespondenceGet (correspondenceId, true);

            if (correspondence == null) {

                ActionResponseLabel.Text = "** Unable to retreive correspondence.";

                return;

            }

            if (!SendCorrespondenceDate.SelectedDate.HasValue) {

                ActionResponseLabel.Text = "** No valid send date selected.";

                return;

            }

            if (CorrespondenceUseAlternativeFaxNumber.Checked) {

                String faxPattern = @"^([\(]{1}[0-9]{3}[\)]{1}[\.| |\-]{0,1}|^[0-9]{3}[\.|\-| ]?)?[0-9]{3}(\.|\-| )?[0-9]{4}$";

                expressionValidator = new System.Text.RegularExpressions.Regex (faxPattern);

                if (!expressionValidator.IsMatch (CorrespondenceAlternateFaxNumber.Text)) {

                    ActionResponseLabel.Text = "** No valid alternate FAX number provided, or not in correct format.";

                    return;

                }

            }

            if (CorrespondenceUseAlternativeEmail.Checked) {

                String emailPattern = @"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$";

                expressionValidator = new System.Text.RegularExpressions.Regex (emailPattern);

                if (!expressionValidator.IsMatch (CorrespondenceAlternateEmail.Text)) {

                    ActionResponseLabel.Text = "** No valid alternate email provided, or not in correct format.";

                    return;

                }

            }


            #endregion


            ActionResponseLabel.Text = String.Empty;

            
            entityCorrespondence = new Mercury.Client.Core.Entity.EntityCorrespondence (MercuryApplication);

            entityCorrespondence.EntityId = Int64.Parse (selectedDataKey["EntityId"].ToString ());

            entityCorrespondence.RelatedEntityId = (RelatedEntity != null) ? RelatedEntity.Id : 0; 


            entityCorrespondence.CorrespondenceId = correspondence.Id;

            entityCorrespondence.CorrespondenceName = correspondence.Name;

            entityCorrespondence.CorrespondenceVersion = correspondence.Version;


            entityCorrespondence.ReadyToSendDate = SendCorrespondenceDate.SelectedDate.Value;

            entityCorrespondence.Attention = SendCorrespondenceAttention.Text;

            entityCorrespondence.Remarks = SendCorrespondenceRemarks.Text;


            Boolean useAlternateInformation = false;

            useAlternateInformation |= CorrespondenceUseAlternativeAddress.Checked;

            useAlternateInformation |= CorrespondenceUseAlternativeFaxNumber.Checked;

            useAlternateInformation |= CorrespondenceUseAlternativeEmail.Checked;


            if (!useAlternateInformation) {

                Mercury.Server.Application.EntityContactType contactType;

                contactType = (Mercury.Server.Application.EntityContactType)Convert.ToInt32 (selectedDataKey["ContactType"].ToString ());

                switch (contactType) {

                    case Mercury.Server.Application.EntityContactType.ByMail:

                        entityCorrespondence.ContactType = contactType;

                        entityCorrespondence.EntityAddressId = Convert.ToInt64 (selectedDataKey["AddressId"].ToString ());

                        Client.Core.Entity.EntityAddress entityAddress = MercuryApplication.EntityAddressGet (entityCorrespondence.EntityAddressId, false);

                        if (entityAddress != null) {

                            entityCorrespondence.AddressLine1 = entityAddress.Line1;

                            entityCorrespondence.AddressLine2 = entityAddress.Line2;

                            entityCorrespondence.AddressCity = entityAddress.City;

                            entityCorrespondence.AddressState = entityAddress.State;

                            entityCorrespondence.AddressZipCode = entityAddress.ZipCode;

                        }

                        break;

                    case Mercury.Server.Application.EntityContactType.Facsimile:

                        entityCorrespondence.ContactType = contactType;

                        entityCorrespondence.EntityContactInformationId = Convert.ToInt64 (selectedDataKey["AddressId"].ToString ());

                        entityContactInformation = MercuryApplication.EntityContactInformationGet (entityCorrespondence.EntityContactInformationId, true);

                        if (entityContactInformation != null) {

                            entityCorrespondence.ContactFaxNumber = entityContactInformation.Number;

                        }

                        break;

                    case Mercury.Server.Application.EntityContactType.Email:

                        entityCorrespondence.ContactType = contactType;

                        entityCorrespondence.EntityContactInformationId = Convert.ToInt64 (selectedDataKey["AddressId"].ToString ());
                        
                        entityContactInformation = MercuryApplication.EntityContactInformationGet (entityCorrespondence.EntityContactInformationId, true);

                        if (entityContactInformation != null) {

                            entityCorrespondence.ContactEmail = entityContactInformation.Email;

                        }

                        break;

                    case Mercury.Server.Application.EntityContactType.InPerson:

                        entityCorrespondence.ContactType = contactType;

                        break;

                    default:

                        ActionResponseLabel.Text = "Check selection. Unknown or unhandled contact type: " + contactType.ToString ();

                        return;

                }

            }

            else if (CorrespondenceUseAlternativeAddress.Checked) {

                entityCorrespondence.ContactType = Mercury.Server.Application.EntityContactType.ByMail;

                entityCorrespondence.EntityAddressId = 0;

                entityCorrespondence.EntityContactInformationId = 0;

                entityCorrespondence.AddressLine1 = CorrespondenceAlternateAddressLine1.Text;

                entityCorrespondence.AddressLine2 = CorrespondenceAlternateAddressLine2.Text;

                entityCorrespondence.AddressCity = CorrespondenceAlternateAddressCity.Text;

                entityCorrespondence.AddressState = CorrespondenceAlternateAddressState.Text;

                entityCorrespondence.AddressZipCode = CorrespondenceAlternateAddressZipCode.Text;

            }

            else if (CorrespondenceUseAlternativeFaxNumber.Checked) {

                entityCorrespondence.ContactType = Mercury.Server.Application.EntityContactType.Facsimile;

                entityCorrespondence.EntityAddressId = 0;

                entityCorrespondence.EntityContactInformationId = 0;

                entityCorrespondence.ContactFaxNumber = CorrespondenceAlternateFaxNumber.Text;

            }

            else if (CorrespondenceUseAlternativeEmail.Checked) {

                entityCorrespondence.ContactType = Mercury.Server.Application.EntityContactType.Email;

                entityCorrespondence.EntityAddressId = 0;

                entityCorrespondence.EntityContactInformationId = 0;

                entityCorrespondence.ContactEmail = CorrespondenceAlternateEmail.Text;

            }

            else {

                ActionResponseLabel.Text = "Unable to determine method. Please check your selection.";

                return;

            }

            if (SendCorrespondence != null) {

                SendCorrespondence (this, new SendCorrespondenceEntityEventArgs (Entity, RelatedEntity, entityCorrespondence));

            }

            return;

        }

        protected void ButtonCancel_OnClick (Object sender, EventArgs e) {

            if (SendCorrespondence != null) {

                SendCorrespondence (this, new SendCorrespondenceEntityEventArgs ());

            }

            return;

        }

        public void CorrespondenceUseAlternativeAddress_OnCheckedChanged (Object sender, EventArgs e) {

            if (CorrespondenceUseAlternativeAddress.Checked) {

                CorrespondenceUseAlternativeFaxNumber.Checked = false;

                CorrespondenceUseAlternativeEmail.Checked = false;

            }

            UseAlternativeUpdate ();

            return;

        }

        public void CorrespondenceUseAlternativeFaxNumber_OnCheckedChanged (Object sender, EventArgs e) {

            if (CorrespondenceUseAlternativeFaxNumber.Checked) {

                CorrespondenceUseAlternativeAddress.Checked = false;

                CorrespondenceUseAlternativeEmail.Checked = false;

            }

            UseAlternativeUpdate ();

            return;

        }

        public void CorrespondenceUseAlternativeEmail_OnCheckedChanged (Object sender, EventArgs e) {

            if (CorrespondenceUseAlternativeEmail.Checked) {

                CorrespondenceUseAlternativeAddress.Checked = false;

                CorrespondenceUseAlternativeFaxNumber.Checked = false;

            }

            UseAlternativeUpdate ();

            return;

        }

        private void UseAlternativeUpdate () {

            AlternativeAddressDetail.Attributes.Add ("style", "width: 100%; display: " + ((CorrespondenceUseAlternativeAddress.Checked) ? "block" : "none"));

            AlternativeFaxNumberDetail.Attributes.Add ("style", "width: 100%; display: " + ((CorrespondenceUseAlternativeFaxNumber.Checked) ? "block" : "none"));

            AlternativeEmailDetail.Attributes.Add ("style", "width: 100%; display: " + ((CorrespondenceUseAlternativeEmail.Checked) ? "block" : "none"));

            return;

        }

        #endregion

    }

    public class SendCorrespondenceEntityEventArgs : EventArgs {

        private Client.Core.Entity.Entity entity = null;

        private Client.Core.Entity.Entity relatedEntity = null;

        private Client.Core.Entity.EntityCorrespondence entityCorrespondence = null;

        private Boolean cancel = false;

        public Client.Core.Entity.Entity Entity { get { return entity; } }

        public Client.Core.Entity.Entity RelatedEntity { get { return relatedEntity; } }

        public Client.Core.Entity.EntityCorrespondence EntityCorrespondence { get { return entityCorrespondence; } }

        public Boolean Cancel { get { return cancel; } }

        public SendCorrespondenceEntityEventArgs (Client.Core.Entity.Entity forEntity, Client.Core.Entity.Entity forRelatedEntity, Client.Core.Entity.EntityCorrespondence forEntityCorrespondence) {

            entity = forEntity;

            relatedEntity = forRelatedEntity;

            entityCorrespondence = forEntityCorrespondence;

            return;

        }

        public SendCorrespondenceEntityEventArgs () { cancel = true; return; }

    }

}


