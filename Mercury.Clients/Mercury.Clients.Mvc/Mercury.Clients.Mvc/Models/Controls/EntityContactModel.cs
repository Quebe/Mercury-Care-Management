using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercury.Clients.Mvc.Models.Controls {

    public class EntityContactModel : ApplicationModel {

        #region Private Properties

        private Mercury.Client.Core.Entity.Entity entity = null;

        private List<Client.Core.Entity.EntityContactInformation> entityContactInformations = new List<Client.Core.Entity.EntityContactInformation> ();

        private Mercury.Client.Core.Member.Member member = null;

        private Mercury.Client.Core.Provider.Provider provider = null;


        private Mercury.Client.Core.Entity.Entity relatedEntity = null;

        private Mercury.Client.Core.Member.Member relatedMember = null;

        private Mercury.Client.Core.Provider.Provider relatedProvider = null;


        private Boolean allowEditContactDateTime = false;

        private Boolean allowEditRegarding = false;

        private Boolean allowEditRelatedEntity = false;


        private Int32 attempt = 0;

        private Boolean allowCancel = false;

        private Boolean canceled = false; 


        private ClientSideControls.jqGrid.DataGrid entityContactInformationGrid = new ClientSideControls.jqGrid.DataGrid ("EntityContactInformationGrid");

        private Int32 selectedRowId = 1;


        private Mercury.Server.Application.ContactDirection contactDirection = Server.Application.ContactDirection.Outbound;

        private Mercury.Server.Application.ContactOutcome contactOutcome = Server.Application.ContactOutcome.Success;


        private Mercury.Client.Core.Entity.EntityContact entityContact = null;

        #endregion 


        #region Public Properties

        public Mercury.Client.Core.Entity.Entity Entity { get { return entity; } }

        public List<Mercury.Client.Core.Entity.EntityContactInformation> EntityContactInformations { get { return entityContactInformations; } }

        public Mercury.Client.Core.Member.Member Member {

            get {

                if (member != null) { return member; }

                if (entity == null) { return null; }

                if (entity.EntityType != Server.Application.EntityType.Member) { return null; }

                member = MercuryApplication.MemberGetByEntityId (entity.Id, true);

                return member;

            }

        }

        public Mercury.Client.Core.Provider.Provider Provider {

            get {

                if (provider != null) { return provider; }

                if (entity == null) { return null; }

                if (entity.EntityType != Server.Application.EntityType.Provider) { return null; }

                provider = MercuryApplication.ProviderGetByEntityId (entity.Id, true);

                return provider;

            }

        }


        public Mercury.Client.Core.Entity.Entity RelatedEntity { get { return relatedEntity; } }

        public Mercury.Client.Core.Member.Member RelatedMember {

            get {

                if (relatedMember != null) { return relatedMember; }

                if (relatedEntity == null) { return null; }

                if (relatedEntity.EntityType != Server.Application.EntityType.Member) { return null; }

                relatedMember = MercuryApplication.MemberGetByEntityId (relatedEntity.Id, true);

                return relatedMember;

            }

        }

        public Mercury.Client.Core.Provider.Provider RelatedProvider {

            get {

                if (relatedProvider != null) { return provider; }

                if (relatedEntity == null) { return null; }

                if (relatedEntity.EntityType != Server.Application.EntityType.Provider) { return null; }

                relatedProvider = MercuryApplication.ProviderGetByEntityId (relatedEntity.Id, true);

                return relatedProvider;

            }

        }


        public String ContactRegarding { get; set; }

        public String IntroductionScript { get; set; }

        public String ContactRemarks { get; set; }

        public DateTime? ContactDateTime { get; set; }


        public Boolean AllowEditContactDateTime { get { return allowEditContactDateTime; } set { allowEditContactDateTime = value; } }

        public Boolean AllowEditRegarding { get { return allowEditRegarding; } set { allowEditRegarding = value; } }

        public Boolean AllowEditRelatedEntity { get { return allowEditRelatedEntity; } set { allowEditRelatedEntity = value; } }


        public Int32 Attempt { get { return attempt; } set { attempt = value; } }

        public Boolean AllowCancel { get { return allowCancel; } set { allowCancel = value; } }

//        public Boolean AllowCancel { get { return true; } set { return; } } // FOR TESTING ONLY

        public Boolean Canceled { get { return canceled; } set { canceled = value; } }


        public String ActionResponseLabel { get; set; }

        public ClientSideControls.jqGrid.DataGrid EntityContactInformationGrid { get { return entityContactInformationGrid; } }


        public Mercury.Server.Application.ContactDirection ContactDirection { get { return contactDirection; } set { contactDirection = value; } }

        public Mercury.Server.Application.ContactOutcome ContactOutcome { get { return contactOutcome; } set { contactOutcome = value; } }


        public Mercury.Client.Core.Entity.EntityContact EntityContact { get { return entityContact; } }

        #endregion 


        #region Constructors

        public EntityContactModel (System.Collections.Specialized.NameValueCollection form) {

            // RESTORE STATE FROM WEB FORM 

            Int64 entityId = 0;

            Int64.TryParse (form["EntityContactModel.Entity.Id"], out entityId);

            entity = MercuryApplication.EntityGet (entityId, true);


            Int64 relatedEntityId = 0;

            Int64.TryParse (form["EntityContactModel.RelatedEntity.Id"], out relatedEntityId);

            relatedEntity = MercuryApplication.EntityGet (relatedEntityId, true);


            Boolean.TryParse (form["EntityContactModel.AllowCancel"], out allowCancel);

            Boolean.TryParse (form["EntityContactModel.AllowEditContactDateTime"], out allowEditContactDateTime);

            Boolean.TryParse (form["EntityContactModel.AllowEditRegarding"], out allowEditRegarding);

            Boolean.TryParse (form["EntityContactModel.AllowEditRelatedEntity"], out allowEditRelatedEntity);


            Int32.TryParse (form["EntityContactModel.Attempt"], out attempt);

            ContactRemarks = form["EntityContactModel.ContactRemarks"];

            IntroductionScript = form["EntityContactModel.IntroductionScript"];


            UpdateValues (form);

            return;

        }

        public EntityContactModel (Mercury.Client.Core.Entity.Entity forEntity) {

            entity = forEntity;

            InitializeEntityContactInformations ();

            return;

        }

        public EntityContactModel (Mercury.Server.Application.Entity forServerEntity, Mercury.Server.Application.Entity forServerRelatedEntity) {

            entity = new Client.Core.Entity.Entity (MercuryApplication, forServerEntity);

            if (forServerRelatedEntity != null) { relatedEntity = new Client.Core.Entity.Entity (MercuryApplication, forServerRelatedEntity); }

            InitializeEntityContactInformations ();

            return;

        }

        private void InitializeEntityContactInformations () {

            // INITIALIZE AND REBIND ENTITY CONTACT INFORMATION

            if (entity != null) {

                // MAKE COPY TO EXTEND IT

                entityContactInformations = new List<Client.Core.Entity.EntityContactInformation> ();

                foreach (Client.Core.Entity.EntityContactInformation currentInformation in entity.ContactInformations) {

                    if ((currentInformation.EffectiveDate <= DateTime.Now) && (currentInformation.TerminationDate >= DateTime.Now)) {

                        Client.Core.Entity.EntityContactInformation copiedInformation = currentInformation.Copy ();

                        entityContactInformations.Add (copiedInformation);

                    }

                }


                Client.Core.Entity.EntityContactInformation inPerson = new Client.Core.Entity.EntityContactInformation (MercuryApplication);

                inPerson.Id = 0;

                inPerson.EntityId = entity.Id;

                inPerson.ContactType = Mercury.Server.Application.EntityContactType.InPerson;

                entityContactInformations.Add (inPerson);


                Client.Core.Entity.EntityContactInformation byMail = new Client.Core.Entity.EntityContactInformation (MercuryApplication);

                inPerson.Id = 1;

                byMail.EntityId = entity.Id;

                byMail.ContactType = Mercury.Server.Application.EntityContactType.ByMail;

                entityContactInformations.Add (byMail);

            }


            // INITIALIZE GRID

            entityContactInformationGrid.WidthResizeToContainer = true;

            entityContactInformationGrid.HeightResizeToContainer = true;

            entityContactInformationGrid.HasPager = false;

            entityContactInformationGrid.IsRowSelectEnabled = true;

            entityContactInformationGrid.AllowMultiselect = false;

            entityContactInformationGrid.SelectedRowId = selectedRowId;


            // INITIALIZE GRID COLUMNS

            ClientSideControls.jqGrid.DataColumn column;

            column = new ClientSideControls.jqGrid.DataColumn ("Id", String.Empty, "Id", false);

            entityContactInformationGrid.Columns.Add (column);

            column = new ClientSideControls.jqGrid.DataColumn ("EntityId", String.Empty, "EntityId", false);

            entityContactInformationGrid.Columns.Add (column);

            column = new ClientSideControls.jqGrid.DataColumn ("ContactSequence", "Sequence", "ContactSequence", false);

            entityContactInformationGrid.Columns.Add (column);

            column = new ClientSideControls.jqGrid.DataColumn ("ContactTypeDescription", "Type", "ContactTypeDescription", true);

            entityContactInformationGrid.Columns.Add (column);

            column = new ClientSideControls.jqGrid.DataColumn ("NumberFormatted", "Number", "NumberFormatted", true);

            entityContactInformationGrid.Columns.Add (column);

            column = new ClientSideControls.jqGrid.DataColumn ("Extension", "Extension", "Extension", true);

            entityContactInformationGrid.Columns.Add (column);

            column = new ClientSideControls.jqGrid.DataColumn ("Email", "Email", "Email", true);

            entityContactInformationGrid.Columns.Add (column);


            entityContactInformationGrid.DataSource = entityContactInformations;
            
            return;

        }

        #endregion 


        #region Public Methods

        public override void UpdateValues (System.Collections.Specialized.NameValueCollection form) {

            ContactRegarding = form["EntityContactModel.ContactRegarding"];

            Int32 contactDirectionSelected = 0;

            Int32.TryParse (form["EntityContactModel.ContactDirection"], out contactDirectionSelected);

            contactDirection = (Server.Application.ContactDirection)contactDirectionSelected;


            Int32 contactOutcomeSelected = 0;

            Int32.TryParse (form["EntityContactModel.ContactOutcome"], out contactOutcomeSelected);

            contactOutcome = (Server.Application.ContactOutcome)contactOutcomeSelected;


            Int32.TryParse (form["EntityContactInformationGridSelectedRowId"], out selectedRowId);


            InitializeEntityContactInformations ();

            return;

        }

        public override void RaiseEvent (string eventTarget, string eventArguments) {

            switch (eventTarget) {

                case "ButtonOk":

                    if (String.IsNullOrWhiteSpace (ContactRegarding)) {

                        ActionResponseLabel = "** Contact Regarding is required.";

                        return;

                    }

                    // TODO: CONTACT DATE TIME SELECTION


                    ActionResponseLabel = String.Empty;

                    entityContact = new Client.Core.Entity.EntityContact (MercuryApplication);

                    entityContact.EntityId = Entity.Id;

                    entityContact.RelatedEntityId = (RelatedEntity != null) ? RelatedEntity.Id : 0; 


                    if (selectedRowId <= EntityContactInformations.Count) {

                        entityContact.EntityContactInformationId = EntityContactInformations[selectedRowId - 1].Id;

                        entityContact.ContactType = EntityContactInformations[selectedRowId - 1].ContactType;

                    }

                    // TODO: DATETIMEE

                    // entityContact.ContactDate = (AllowEditContactDateTime) ? ContactDateTime.SelectedDate.Value : DateTime.Now;

                    entityContact.ContactedByName = MercuryApplication.Session.UserDisplayName;


                    entityContact.Direction = ContactDirection;


                    // TODO: CONTACT REGARDING
                    // entityContact.ContactRegardingId = (!String.IsNullOrWhiteSpace (ContactRegarding.SelectedValue)) ? Convert.ToInt64 (ContactRegarding.SelectedValue) : 0;               

                    entityContact.Regarding = ContactRegarding;

                    entityContact.Remarks = ContactRemarks;


                    entityContact.Successful = (ContactOutcome == Server.Application.ContactOutcome.Success);

                    entityContact.ContactOutcome = ContactOutcome;

                    break;

                case "ButtonCancel":

                    if (AllowCancel) { Canceled = true; }

                    else { ActionResponseLabel = "Cancel is not enabled."; }

                    break;

            }

            return;

        }

        #endregion 

    }

}