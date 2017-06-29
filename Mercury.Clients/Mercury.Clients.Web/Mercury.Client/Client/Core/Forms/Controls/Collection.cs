using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Forms.Controls {

    [Serializable]
    public class Collection : Mercury.Client.Core.Forms.Control {

        #region Private Properties

        protected Mercury.Server.Application.FormControlCollectionType collectionType = Mercury.Server.Application.FormControlCollectionType.NotSpecified;

        protected List<Int64> items = new List<Int64> ();

        protected Int64 selectedItem = 0;


        private List<Mercury.Client.Core.Entity.EntityAddress> entityAddresses = new List<Core.Entity.EntityAddress> ();

        private List<Mercury.Client.Core.Entity.EntityContactInformation> entityContacts = new List<Core.Entity.EntityContactInformation> ();

        private List<Mercury.Client.Core.Member.MemberEnrollment> memberEnrollments = new List<Core.Member.MemberEnrollment> ();

        private List<Mercury.Client.Core.Member.MemberEnrollmentCoverage> memberEnrollmentCoverages = new List<Core.Member.MemberEnrollmentCoverage> ();

        private List<Mercury.Client.Core.Member.MemberEnrollmentPcp> memberEnrollmentPcps = new List<Core.Member.MemberEnrollmentPcp> ();

        private List<Mercury.Client.Core.Population.PopulationMembership> populationMembership = new List<Mercury.Client.Core.Population.PopulationMembership> ();

        private List<Mercury.Client.Core.Population.PopulationEvents.PopulationMembershipServiceEvent> populationMembershipServiceEvents = new List<Mercury.Client.Core.Population.PopulationEvents.PopulationMembershipServiceEvent> ();

        private List<Core.MedicalServices.MemberService> memberServices = new List<Mercury.Client.Core.MedicalServices.MemberService> ();

        private List<Mercury.Client.Core.Provider.ProviderContract> providerContracts = new List<Provider.ProviderContract> ();

        #endregion


        #region Public Properties

        public Mercury.Server.Application.FormControlCollectionType CollectionType { 
            
            get {

                SetCollectionType ();
                
                return collectionType; 
            
            } 
            
            set {

                if (collectionType != value) {

                    collectionType = value;

                    dataBindingContexts = null;

                    bindableProperties = null;

                }
            
            } 
        
        }

        public List<Int64> Items { get { return items; } set { items = value; } }

        public Int64 SelectedItem { 
            
            get { return selectedItem; } 
            
            set { 
                
                if (items.Contains (value)) { 
                    
                    selectedItem = value; 
                
                } 
                
                else { 
                    
                    selectedItem = 0; 
                
                }

                DataSourceChanged ();
            
            } 
        
        }


        public override Boolean HasValue { get { return selectedItem != 0; } }

        public override String Value { get { return (HasValue) ? selectedItem.ToString () : String.Empty; } }


        public List<Core.Entity.EntityAddress> EntityAddresses {

            get {

                if (collectionType != Mercury.Server.Application.FormControlCollectionType.EntityAddress) { return new List<Core.Entity.EntityAddress> (); }

                if (application == null) { return new List<Core.Entity.EntityAddress> (); }

                if (entityAddresses.Count != 0) { return entityAddresses; }


                entityAddresses = new List<Core.Entity.EntityAddress> ();

                foreach (Int64 currentId in items) {

                    entityAddresses.Add (Application.EntityAddressGet (currentId, true));

                }

                return entityAddresses;

            }                    
                
        }

        public List<Core.Entity.EntityContactInformation> EntityContactInformations {

            get {

                if (collectionType != Mercury.Server.Application.FormControlCollectionType.EntityContactInformation) { return new List<Core.Entity.EntityContactInformation> (); }

                if (application == null) { return new List<Core.Entity.EntityContactInformation> (); }

                if (entityContacts.Count != 0) { return entityContacts; }


                entityContacts = new List<Core.Entity.EntityContactInformation> ();

                foreach (Int64 currentId in items) {

                    entityContacts.Add (Application.EntityContactInformationGet (currentId, true));

                }

                return entityContacts;

            }

        }

        public List<Member.MemberEnrollment> Enrollments {

            get {

                if (collectionType != Mercury.Server.Application.FormControlCollectionType.MemberEnrollment) { return new List<Member.MemberEnrollment> (); }

                if (application == null) { return new List<Member.MemberEnrollment> (); }

                if (memberEnrollments.Count != 0) { return memberEnrollments; }


                memberEnrollments = new List<Member.MemberEnrollment> ();

                foreach (Int64 currentId in items) {

                    memberEnrollments.Add (Application.MemberEnrollmentGet (currentId, true));

                }

                return memberEnrollments;

            }

        }

        public List<Member.MemberEnrollmentCoverage> EnrollmentCoverages {

            get {

                if (collectionType != Mercury.Server.Application.FormControlCollectionType.MemberEnrollmentCoverage) { return new List<Member.MemberEnrollmentCoverage> (); }

                if (application == null) { return new List<Member.MemberEnrollmentCoverage> (); }

                if (memberEnrollmentCoverages.Count != 0) { return memberEnrollmentCoverages; }


                memberEnrollmentCoverages = new List<Member.MemberEnrollmentCoverage> ();

                foreach (Int64 currentId in items) {

                    memberEnrollmentCoverages.Add (Application.MemberEnrollmentCoverageGet (currentId, true));

                }

                return memberEnrollmentCoverages;

            }

        }

        public List<Member.MemberEnrollmentPcp> MemberEnrollmentPcps {

            get {

                if (collectionType != Mercury.Server.Application.FormControlCollectionType.MemberEnrollmentPcp) { return new List<Member.MemberEnrollmentPcp> (); }

                if (application == null) { return new List<Member.MemberEnrollmentPcp> (); }

                if (memberEnrollmentPcps.Count != 0) { return memberEnrollmentPcps; }


                memberEnrollmentPcps = new List<Member.MemberEnrollmentPcp> ();

                foreach (Int64 currentId in items) {

                    memberEnrollmentPcps.Add (Application.MemberEnrollmentPcpGet (currentId, true));

                }

                return memberEnrollmentPcps;

            }

        }

        public List<Core.Population.PopulationMembership> PopulationMembership {

            get {

                if (collectionType != Mercury.Server.Application.FormControlCollectionType.PopulationMembership) { return new List<Core.Population.PopulationMembership> (); }

                if (Application == null) { return new List<Core.Population.PopulationMembership> (); }

                if (populationMembership.Count != 0) { return populationMembership; }


                populationMembership = new List<Core.Population.PopulationMembership> ();

                foreach (Int64 currentId in items) {

                    populationMembership.Add (Application.PopulationMembershipGet (currentId, true));

                }

                return populationMembership;

            }

        }

        public List<Core.Population.PopulationEvents.PopulationMembershipServiceEvent> PopulationMembershipServiceEvents {

            get {

                if (collectionType != Mercury.Server.Application.FormControlCollectionType.PopulationMembershipServiceEvent) { return new List<Core.Population.PopulationEvents.PopulationMembershipServiceEvent> (); }

                if (Application == null) { return new List<Core.Population.PopulationEvents.PopulationMembershipServiceEvent> (); }

                if (populationMembershipServiceEvents.Count != 0) { return populationMembershipServiceEvents; }


                populationMembershipServiceEvents = new List<Core.Population.PopulationEvents.PopulationMembershipServiceEvent> ();

                foreach (Int64 currentId in items) {

                    populationMembershipServiceEvents.Add (Application.PopulationMembershipServiceEventGet (currentId));

                }

                return populationMembershipServiceEvents;

            }

        }

        public List<Core.MedicalServices.MemberService> MemberServices {

            get {

                if (collectionType != Mercury.Server.Application.FormControlCollectionType.MemberService) { return new List<Core.MedicalServices.MemberService> (); }

                if (Application == null) { return new List<Core.MedicalServices.MemberService> (); }

                if (memberServices.Count != 0) { return memberServices; }


                memberServices = new List<Core.MedicalServices.MemberService> ();

                foreach (Int64 currentId in items) {

                    memberServices.Add (Application.MemberServiceGet (currentId));

                }

                return memberServices;

            }

        }

        public List<Provider.ProviderContract> ProviderContracts {

            get {

                if (collectionType != Mercury.Server.Application.FormControlCollectionType.ProviderContract) { return new List<Provider.ProviderContract> (); }

                if (Application == null) { return new List<Provider.ProviderContract> (); }

                if (providerContracts.Count != 0) { return providerContracts; }


                providerContracts = new List<Provider.ProviderContract> ();

                foreach (Int64 currentId in items) {

                    providerContracts.Add (Application.ProviderContractGet (currentId, true));

                }

                return providerContracts;

            }

        }

        public System.Data.DataTable DataTable { 

            get {

                //DateTime startTime = DateTime.Now;

                //System.Diagnostics.Debug.WriteLine ("Collection [" + Name + "] (BEGIN)");


                SetCollectionType ();

                System.Data.DataTable collectionTable = new System.Data.DataTable ();

                switch (collectionType) {

                    case Mercury.Server.Application.FormControlCollectionType.EntityAddress:

                        #region Entity Address Collection Type

                        collectionTable.Columns.Add ("Id");

                        collectionTable.Columns.Add ("Address Type");

                        collectionTable.Columns.Add ("Effective");

                        collectionTable.Columns.Add ("Termination");

                        collectionTable.Columns.Add ("Line 1");

                        collectionTable.Columns.Add ("Line 2");

                        collectionTable.Columns.Add ("City, State Zip Code");

                        // collectionTable.Columns.Add ("County");

                        foreach (Core.Entity.EntityAddress currentAddress in EntityAddresses) {

                            collectionTable.Rows.Add (

                                currentAddress.Id,

                                currentAddress.AddressTypeDescription,

                                currentAddress.EffectiveDate.ToString ("MM/dd/yyyy"),

                                currentAddress.TerminationDate.ToString ("MM/dd/yyyy"),

                                currentAddress.Line1,

                                currentAddress.Line2,

                                currentAddress.CityStateZipCode

//                                currentAddress.County


                            );

                        }

                        #endregion

                        break;

                    case Mercury.Server.Application.FormControlCollectionType.EntityContactInformation:

                        #region Entity Contact Collection Type

                        collectionTable.Columns.Add ("Id");

                        collectionTable.Columns.Add ("Type");

                        collectionTable.Columns.Add ("Sequence");


                        collectionTable.Columns.Add ("Number");

                        collectionTable.Columns.Add ("Extension");

                        collectionTable.Columns.Add ("Email");


                        collectionTable.Columns.Add ("Effective Date");

                        collectionTable.Columns.Add ("Termination Date");

                        foreach (Core.Entity.EntityContactInformation currentContactInformation in EntityContactInformations) {

                            collectionTable.Rows.Add (

                                currentContactInformation.Id,

                                currentContactInformation.ContactTypeDescription,

                                currentContactInformation.ContactSequence,

                                currentContactInformation.NumberFormatted,

                                currentContactInformation.NumberExtension,

                                currentContactInformation.Email,

                                currentContactInformation.EffectiveDate.ToString ("MM/dd/yyyy"),

                                currentContactInformation.TerminationDate.ToString ("MM/dd/yyyy")

                            );

                        }

                        #endregion

                        break;

                    case Mercury.Server.Application.FormControlCollectionType.MemberEnrollment: 

                        #region Enrollment Collection Type

                        collectionTable.Columns.Add ("Id");

                        collectionTable.Columns.Add ("Insurer Name");

                        collectionTable.Columns.Add ("Program Name");

                        collectionTable.Columns.Add ("Sponsor Name");

                        collectionTable.Columns.Add ("Subscriber Name");

                        collectionTable.Columns.Add ("Member Id");

                        collectionTable.Columns.Add ("Effective Date");

                        collectionTable.Columns.Add ("Termination Date");

                        if (Enrollments != null) {

                            foreach (Member.MemberEnrollment currentEnrollment in Enrollments) {

                                collectionTable.Rows.Add (

                                    currentEnrollment.Id,

                                    (currentEnrollment.Program != null) ? ((currentEnrollment.Program.Insurer != null) ? currentEnrollment.Program.Insurer.Name : String.Empty) : String.Empty,

                                    currentEnrollment.ProgramName,

                                    currentEnrollment.SponsorName,

                                    currentEnrollment.SubscriberName,

                                    currentEnrollment.ProgramMemberId,

                                    currentEnrollment.EffectiveDate.ToShortDateString (),

                                    currentEnrollment.TerminationDate.ToShortDateString ());

                            }

                        }

                        #endregion

                        break;

                    case Mercury.Server.Application.FormControlCollectionType.MemberEnrollmentCoverage:

                        #region Enrollment Coverage Collection Type

                        collectionTable.Columns.Add ("Id");

                        collectionTable.Columns.Add ("Benefit Plan");

                        collectionTable.Columns.Add ("Rate Code");

                        collectionTable.Columns.Add ("Coverage Code Id");

                        collectionTable.Columns.Add ("Effective Date");

                        collectionTable.Columns.Add ("Termination Date");

                        if (EnrollmentCoverages != null) {

                            foreach (Member.MemberEnrollmentCoverage currentEnrollmentCoverage in EnrollmentCoverages) {

                                collectionTable.Rows.Add (

                                    currentEnrollmentCoverage.Id,

                                    "TODO: Benefit Plan Name",

                                    currentEnrollmentCoverage.RateCode,

                                    currentEnrollmentCoverage.CoverageTypeId,

                                    currentEnrollmentCoverage.CoverageLevelId,

                                    currentEnrollmentCoverage.EffectiveDate.ToShortDateString (),

                                    currentEnrollmentCoverage.TerminationDate.ToShortDateString ());

                            }

                        }

                        #endregion

                        break;

                    case Mercury.Server.Application.FormControlCollectionType.MemberEnrollmentPcp:

                        #region Pcp Assignment Collection Type

                        collectionTable.Columns.Add ("Id");
                        
                        collectionTable.Columns.Add ("PCP Provider Name");

                        collectionTable.Columns.Add ("PCP Affiliate");

                        collectionTable.Columns.Add ("Effective Date");

                        collectionTable.Columns.Add ("Termination Date");

                        if (MemberEnrollmentPcps != null) {

                            foreach (Member.MemberEnrollmentPcp currentMemberEnrollmentPcp in MemberEnrollmentPcps) {

                                collectionTable.Rows.Add (

                                    currentMemberEnrollmentPcp.Id,

                                    currentMemberEnrollmentPcp.PcpProvider.Name,

                                    currentMemberEnrollmentPcp.PcpAffiliateProvider.Name,

                                    currentMemberEnrollmentPcp.EffectiveDate.ToShortDateString (),

                                    currentMemberEnrollmentPcp.TerminationDate.ToShortDateString ());

                            }

                        }

                        #endregion

                        break;

                    case Mercury.Server.Application.FormControlCollectionType.PopulationMembership:

                        #region Population Membership Collection Type

                        collectionTable.Columns.Add ("Id");

                        collectionTable.Columns.Add ("Name");

                        collectionTable.Columns.Add ("Population Type");

                        collectionTable.Columns.Add ("Effective");

                        collectionTable.Columns.Add ("Termination");

                        if (PopulationMembership != null) {

                            foreach (Core.Population.PopulationMembership currentPopulationMembership in PopulationMembership) {

                                collectionTable.Rows.Add (

                                    currentPopulationMembership.Id,

                                    currentPopulationMembership.Population.Name,

                                    currentPopulationMembership.Population.PopulationType.Name,

                                    currentPopulationMembership.EffectiveDate.ToShortDateString (),

                                    (currentPopulationMembership.TerminationDate.Equals (new DateTime (9999, 12, 31)) ? "< active >" : currentPopulationMembership.TerminationDate.ToShortDateString ()));

                            }

                        }

                        #endregion

                        break;

                    case Mercury.Server.Application.FormControlCollectionType.PopulationMembershipServiceEvent:

                        #region Population Membership Service Event Collection Type

                        collectionTable.Columns.Add ("Id");

                        collectionTable.Columns.Add ("Service Name");

                        collectionTable.Columns.Add ("Expected Date");

                        collectionTable.Columns.Add ("Actual Date");

                        collectionTable.Columns.Add ("Status");

                        if (PopulationMembership != null) {

                            foreach (Core.Population.PopulationEvents.PopulationMembershipServiceEvent currentPopulationMembershipServiceEvent in PopulationMembershipServiceEvents) {

                                collectionTable.Rows.Add (

                                    currentPopulationMembershipServiceEvent.PopulationMembershipId,

                                    currentPopulationMembershipServiceEvent.ServiceName,

                                    currentPopulationMembershipServiceEvent.ExpectedEventDate.ToString ("MM/dd/yyyy"),

                                    (currentPopulationMembershipServiceEvent.EventDate.HasValue) ? currentPopulationMembershipServiceEvent.EventDate.Value.ToString ("MM/dd/yyyy") : String.Empty,

                                    currentPopulationMembershipServiceEvent.StatusText

                                    );

                            }

                        }

                        #endregion

                        break;

                    case Mercury.Server.Application.FormControlCollectionType.MemberService:

                        #region Member Service Collection Type

                        collectionTable.Columns.Add ("Id");

                        collectionTable.Columns.Add ("Service Id");

                        collectionTable.Columns.Add ("Name");

                        collectionTable.Columns.Add ("Event Date");

                        // collectionTable.Columns.Add ("County");

                        foreach (Core.MedicalServices.MemberService currentService in MemberServices) {

                            collectionTable.Rows.Add (

                                currentService.Id.ToString (),

                                currentService.ServiceId.ToString (),

                                currentService.ServiceName,

                                currentService.EventDate.ToString ("MM/dd/yyyy")

                            );

                        }

                        #endregion

                        break;

                    case Mercury.Server.Application.FormControlCollectionType.ProviderContract:

                        #region Provider Contract Collection Type

                        collectionTable.Columns.Add ("Id");

                        collectionTable.Columns.Add ("Insurer Name");

                        collectionTable.Columns.Add ("Program Name");

                        collectionTable.Columns.Add ("Affiliate Name");

                        collectionTable.Columns.Add ("Contract Name");

                        collectionTable.Columns.Add ("Is Participating");

                        collectionTable.Columns.Add ("Is Capitated");

                        collectionTable.Columns.Add ("Effective Date");

                        collectionTable.Columns.Add ("Termination Date");

                        if (ProviderContracts != null) {

                            foreach (Provider.ProviderContract currentProviderContract in ProviderContracts) {

                                collectionTable.Rows.Add (

                                    currentProviderContract.Id,

                                    currentProviderContract.Program.Insurer.Name,
                                    
                                    currentProviderContract.ProgramName,

                                    currentProviderContract.AffiliateProviderName,

                                    currentProviderContract.ContractName,

                                    currentProviderContract.IsParticipating.ToString (),

                                    currentProviderContract.IsCapitated.ToString (),

                                    currentProviderContract.EffectiveDate.ToShortDateString (),

                                    currentProviderContract.TerminationDate.ToShortDateString ());

                            }

                        }

                        #endregion

                        break;

                    default:

                        collectionTable.Columns.Add ("Id");

                        break;

                } // switch (collectionType)

                //System.Diagnostics.Debug.WriteLine ("Collection [" + Name + "] ( END ): " + DateTime.Now.Subtract (startTime).Milliseconds.ToString ());

                return collectionTable;

            }

        }

        #endregion

        
        #region Constructors

        virtual protected void InitializeControl (Application applicationReference) {

            BaseConstructor (applicationReference);

            ControlType = Mercury.Server.Application.FormControlType.Collection;

            collectionType = Mercury.Server.Application.FormControlCollectionType.NotSpecified;

            capabilities.HasValue = false;

            capabilities.HasLabel = false;

            capabilities.CanDataBind = true;

            capabilities.IsDataSource = true;

            label = new Label (Application, this);

            label.Visible = false;

            return;

        }

        override public void BaseConstructor (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Mercury.Server.Application.FormControl serverControl) {

            base.BaseConstructor (applicationReference, parentControl, serverControl);


            Mercury.Server.Application.FormControlCollection serverCollection = (Mercury.Server.Application.FormControlCollection) serverControl;

            collectionType = serverCollection.CollectionType;


            if (serverCollection.Label != null) {

                label = new Label (Application, this, serverCollection.Label);

            }


            items.Clear ();

            items.AddRange (serverCollection.Items);

            SelectedItem = serverCollection.SelectedItem; 

            return;

        }


        public Collection (Application applicationReference) {

            InitializeControl (applicationReference);

            return;

        }

        public Collection (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Mercury.Server.Application.FormControlCollection serverControl) {

            BaseConstructor (applicationReference, parentControl, serverControl);

            InitializeControl (applicationReference);

            ChildServerControlsToLocal (this, serverControl);

            return;

        }

        #endregion


        #region Virtual Overrides

        public override void LocalControlToServer (Mercury.Server.Application.FormControl parentControl, Mercury.Server.Application.FormControl serverControl) {

            base.LocalControlToServer (parentControl, serverControl);


            ((Mercury.Server.Application.FormControlCollection) serverControl).CollectionType = collectionType;

            ((Mercury.Server.Application.FormControlCollection) serverControl).Items = new Int64[items.Count];

            Items.CopyTo (((Mercury.Server.Application.FormControlCollection) serverControl).Items, 0);

            ((Mercury.Server.Application.FormControlCollection) serverControl).SelectedItem = selectedItem;


            ((Mercury.Server.Application.FormControlCollection) serverControl).Label = new Mercury.Server.Application.FormControlLabel ();

            label.LocalControlToServer (serverControl, ((Mercury.Server.Application.FormControlCollection) serverControl).Label);

            return;

        }

        #endregion


        #region Data Bindings


        private void SetCollectionType () {

            Mercury.Server.Application.FormControlDataBinding collectionBinding;

            collectionBinding = GetDataBinding ("Collection");

            if (collectionBinding != null) {

                Control dataSourceControl = Form.FindControlById (collectionBinding.DataSourceControlId); 

                if (dataSourceControl != null) {

                    try {

                        String collectionDataType = dataSourceControl.GetDataBindingContextDataType (collectionBinding.BindingContext);

                        if (!String.IsNullOrEmpty (collectionDataType)) {

                            collectionDataType = collectionDataType.Split ('|')[(collectionDataType.Split ('|').Length) - 1];

                        }

                        switch (collectionDataType) {

                            case "EntityAddress": collectionType = Mercury.Server.Application.FormControlCollectionType.EntityAddress; break;

                            case "EntityContactInformation": collectionType = Mercury.Server.Application.FormControlCollectionType.EntityContactInformation; break;

                            case "Enrollment": // BACKWARDS COMPATIBILITY

                            case "MemberEnrollment": 
                                
                                collectionType = Mercury.Server.Application.FormControlCollectionType.MemberEnrollment; break;

                            case "EnrollmentCoverage":  // BACKWARDS COMPATIBILITY

                            case "MemberEnrollmentCoverage":
                                                                
                                collectionType = Mercury.Server.Application.FormControlCollectionType.MemberEnrollmentCoverage; break;

                            case "PcpAssignment":   // BACKWARDS COMPATIBILITY

                            case "MemberEnrollmentPcp": 

                                collectionType = Mercury.Server.Application.FormControlCollectionType.MemberEnrollmentPcp; break;

                            case "PopulationMembership": collectionType = Mercury.Server.Application.FormControlCollectionType.PopulationMembership; break;

                            case "PopulationMembershipServiceEvent": collectionType = Mercury.Server.Application.FormControlCollectionType.PopulationMembershipServiceEvent; break;

                            case "MemberService": collectionType = Mercury.Server.Application.FormControlCollectionType.MemberService; break;

                            case "ProviderContract": collectionType = Mercury.Server.Application.FormControlCollectionType.ProviderContract; break;

                            default: collectionType = Mercury.Server.Application.FormControlCollectionType.NotSpecified; break;

                        }

                    }

                    catch { /* DO NOTHING */ }

                    DataBindingsResetCache ();

                }

            }

            return;

        }

        public override Dictionary<String, String> DataBindableProperties {
            
            get {

                SetCollectionType ();

                return base.DataBindableProperties;

            }

        }

        public override Dictionary<String, String> DataBindingContexts {
            
            get {

                SetCollectionType ();

                return base.DataBindingContexts;

            }

        }

        #endregion

    }
}
