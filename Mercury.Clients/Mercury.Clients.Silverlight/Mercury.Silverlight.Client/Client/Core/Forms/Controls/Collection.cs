using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace Mercury.Client.Core.Forms.Controls {

    public class Collection : Control {

        #region Private Properties

        protected Server.Application.FormControlCollectionType collectionType = Server.Application.FormControlCollectionType.NotSpecified;

        protected List<Int64> items = new List<Int64> ();

        protected Int64 selectedItem = 0;


        //private List<Mercury.Client.Core.EntityAddress> entityAddresses = new List<EntityAddress> ();

        //private List<Mercury.Client.Core.EntityContactInformation> entityContacts = new List<EntityContactInformation> ();

        //private List<Mercury.Client.Core.Enrollment> enrollments = new List<Enrollment> ();

        //private List<Mercury.Client.Core.EnrollmentCoverage> enrollmentCoverages = new List<EnrollmentCoverage> ();

        //private List<Mercury.Client.Core.PcpAssignment> pcpAssignments = new List<PcpAssignment> ();

        //private List<Mercury.Client.Core.PopulationManagement.PopulationMembership> populationMembership = new List<Mercury.Client.Core.PopulationManagement.PopulationMembership> ();

        //private List<Mercury.Client.Core.PopulationManagement.PopulationEvents.PopulationMembershipServiceEvent> populationMembershipServiceEvents = new List<Mercury.Client.Core.PopulationManagement.PopulationEvents.PopulationMembershipServiceEvent> ();

        //private List<Core.MedicalServices.MemberService> memberServices = new List<Mercury.Client.Core.MedicalServices.MemberService> ();

        #endregion


        #region Public Properties

        public Server.Application.FormControlCollectionType CollectionType {

            get {

                //SetCollectionType ();

                return collectionType;

            }

            set {

                if (collectionType != value) {

                    collectionType = value;

                    dataBindingContextsIsLoaded = false;

                    dataBindablePropertiesIsLoaded = false;

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

                //DataSourceChanged ();

            }

        }


        public override Boolean HasValue { get { return selectedItem != 0; } }

        public override String Value { get { return (HasValue) ? selectedItem.ToString () : String.Empty; } }


//        public List<EntityAddress> EntityAddresses {

//            get {

//                if (collectionType != Server.Application.FormControlCollectionType.EntityAddress) { return new List<EntityAddress> (); }

//                if (Application == null) { return new List<EntityAddress> (); }

//                if (entityAddresses.Count != 0) { return entityAddresses; }


//                entityAddresses = new List<EntityAddress> ();

//                foreach (Int64 currentId in items) {

//                    entityAddresses.Add (Application.EntityAddressGet (currentId, true));

//                }

//                return entityAddresses;

//            }

//        }

//        public List<EntityContactInformation> EntityContactInformations {

//            get {

//                if (collectionType != Server.Application.FormControlCollectionType.EntityContactInformation) { return new List<EntityContactInformation> (); }

//                if (Application == null) { return new List<EntityContactInformation> (); }

//                if (entityContacts.Count != 0) { return entityContacts; }


//                entityContacts = new List<EntityContactInformation> ();

//                foreach (Int64 currentId in items) {

//                    entityContacts.Add (Application.EntityContactInformationGet (currentId));

//                }

//                return entityContacts;

//            }

//        }

//        public List<Enrollment> Enrollments {

//            get {

//                if (collectionType != Server.Application.FormControlCollectionType.Enrollment) { return new List<Enrollment> (); }

//                if (Application == null) { return new List<Enrollment> (); }

//                if (enrollments.Count != 0) { return enrollments; }


//                enrollments = new List<Enrollment> ();

//                foreach (Int64 currentId in items) {

//                    enrollments.Add (Application.EnrollmentGet (currentId));

//                }

//                return enrollments;

//            }

//        }

//        public List<EnrollmentCoverage> EnrollmentCoverages {

//            get {

//                if (collectionType != Server.Application.FormControlCollectionType.EnrollmentCoverage) { return new List<EnrollmentCoverage> (); }

//                if (Application == null) { return new List<EnrollmentCoverage> (); }

//                if (enrollmentCoverages.Count != 0) { return enrollmentCoverages; }


//                enrollmentCoverages = new List<EnrollmentCoverage> ();

//                foreach (Int64 currentId in items) {

//                    enrollmentCoverages.Add (Application.EnrollmentCoverageGet (currentId));

//                }

//                return enrollmentCoverages;

//            }

//        }

//        public List<PcpAssignment> PcpAssignments {

//            get {

//                if (collectionType != Server.Application.FormControlCollectionType.PcpAssignment) { return new List<PcpAssignment> (); }

//                if (Application == null) { return new List<PcpAssignment> (); }

//                if (pcpAssignments.Count != 0) { return pcpAssignments; }


//                pcpAssignments = new List<PcpAssignment> ();

//                foreach (Int64 currentId in items) {

//                    pcpAssignments.Add (Application.PcpAssignmentGet (currentId));

//                }

//                return pcpAssignments;

//            }

//        }

//        public List<Core.PopulationManagement.PopulationMembership> PopulationMembership {

//            get {

//                if (collectionType != Server.Application.FormControlCollectionType.PopulationMembership) { return new List<Core.PopulationManagement.PopulationMembership> (); }

//                if (Application == null) { return new List<Core.PopulationManagement.PopulationMembership> (); }

//                if (populationMembership.Count != 0) { return populationMembership; }


//                populationMembership = new List<Core.PopulationManagement.PopulationMembership> ();

//                foreach (Int64 currentId in items) {

//                    populationMembership.Add (Application.PopulationMembershipGet (currentId));

//                }

//                return populationMembership;

//            }

//        }

//        public List<Core.PopulationManagement.PopulationEvents.PopulationMembershipServiceEvent> PopulationMembershipServiceEvents {

//            get {

//                if (collectionType != Server.Application.FormControlCollectionType.PopulationMembershipServiceEvent) { return new List<Core.PopulationManagement.PopulationEvents.PopulationMembershipServiceEvent> (); }

//                if (Application == null) { return new List<Core.PopulationManagement.PopulationEvents.PopulationMembershipServiceEvent> (); }

//                if (populationMembershipServiceEvents.Count != 0) { return populationMembershipServiceEvents; }


//                populationMembershipServiceEvents = new List<Core.PopulationManagement.PopulationEvents.PopulationMembershipServiceEvent> ();

//                foreach (Int64 currentId in items) {

//                    populationMembershipServiceEvents.Add (Application.PopulationMembershipServiceEventGet (currentId));

//                }

//                return populationMembershipServiceEvents;

//            }

//        }

//        public List<Core.MedicalServices.MemberService> MemberServices {

//            get {

//                if (collectionType != Server.Application.FormControlCollectionType.MemberService) { return new List<Core.MedicalServices.MemberService> (); }

//                if (Application == null) { return new List<Core.MedicalServices.MemberService> (); }

//                if (memberServices.Count != 0) { return memberServices; }


//                memberServices = new List<Core.MedicalServices.MemberService> ();

//                foreach (Int64 currentId in items) {

//                    memberServices.Add (Application.MemberServiceGet (currentId));

//                }

//                return memberServices;

//            }

//        }

//        public System.Data.DataTable DataTable {

//            get {

//                //DateTime startTime = DateTime.Now;

//                //System.Diagnostics.Debug.WriteLine ("Collection [" + Name + "] (BEGIN)");


//                SetCollectionType ();

//                System.Data.DataTable collectionTable = new System.Data.DataTable ();

//                switch (collectionType) {

//                    case Server.Application.FormControlCollectionType.EntityAddress:

//                        #region Entity Address Collection Type

//                        collectionTable.Columns.Add ("Id");

//                        collectionTable.Columns.Add ("Address Type");

//                        collectionTable.Columns.Add ("Effective");

//                        collectionTable.Columns.Add ("Termination");

//                        collectionTable.Columns.Add ("Line 1");

//                        collectionTable.Columns.Add ("Line 2");

//                        collectionTable.Columns.Add ("City, State Zip Code");

//                        // collectionTable.Columns.Add ("County");

//                        foreach (EntityAddress currentAddress in EntityAddresses) {

//                            collectionTable.Rows.Add (

//                                currentAddress.Id,

//                                currentAddress.AddressTypeDescription,

//                                currentAddress.EffectiveDate.ToString ("MM/dd/yyyy"),

//                                currentAddress.TerminationDate.ToString ("MM/dd/yyyy"),

//                                currentAddress.Line1,

//                                currentAddress.Line2,

//                                currentAddress.CityStateZipCode

////                                currentAddress.County


//                            );

//                        }

//                        #endregion

//                        break;

//                    case Server.Application.FormControlCollectionType.EntityContactInformation:

//                        #region Entity Contact Collection Type

//                        collectionTable.Columns.Add ("Id");

//                        collectionTable.Columns.Add ("Type");

//                        collectionTable.Columns.Add ("Sequence");


//                        collectionTable.Columns.Add ("Number");

//                        collectionTable.Columns.Add ("Extension");

//                        collectionTable.Columns.Add ("Email");


//                        collectionTable.Columns.Add ("Effective Date");

//                        collectionTable.Columns.Add ("Termination Date");

//                        foreach (EntityContactInformation currentContactInformation in EntityContactInformations) {

//                            collectionTable.Rows.Add (

//                                currentContactInformation.Id,

//                                currentContactInformation.ContactTypeDescription,

//                                currentContactInformation.ContactSequence,

//                                currentContactInformation.Number,

//                                currentContactInformation.NumberExtension,

//                                currentContactInformation.Email,

//                                currentContactInformation.EffectiveDate.ToString ("MM/dd/yyyy"),

//                                currentContactInformation.TerminationDate.ToString ("MM/dd/yyyy")

//                            );

//                        }

//                        #endregion

//                        break;

//                    case Server.Application.FormControlCollectionType.Enrollment:

//                        #region Enrollment Collection Type

//                        collectionTable.Columns.Add ("Id");

//                        collectionTable.Columns.Add ("Insurer Name");

//                        collectionTable.Columns.Add ("Program Name");

//                        collectionTable.Columns.Add ("Sponsor Name");

//                        collectionTable.Columns.Add ("Subscriber Name");

//                        collectionTable.Columns.Add ("Member Id");

//                        collectionTable.Columns.Add ("Effective Date");

//                        collectionTable.Columns.Add ("Termination Date");

//                        if (Enrollments != null) {

//                            foreach (Enrollment currentEnrollment in Enrollments) {

//                                collectionTable.Rows.Add (

//                                    currentEnrollment.EnrollmentId,

//                                    currentEnrollment.InsurerName,

//                                    currentEnrollment.ProgramName,

//                                    currentEnrollment.SponsorName,

//                                    currentEnrollment.SubscriberName,

//                                    currentEnrollment.ProgramMemberId,

//                                    currentEnrollment.EffectiveDate.ToShortDateString (),

//                                    currentEnrollment.TerminationDate.ToShortDateString ());

//                            }

//                        }

//                        #endregion

//                        break;

//                    case Server.Application.FormControlCollectionType.EnrollmentCoverage:

//                        #region Enrollment Coverage Collection Type

//                        collectionTable.Columns.Add ("Id");

//                        collectionTable.Columns.Add ("Benefit Plan");

//                        collectionTable.Columns.Add ("Rate Code");

//                        collectionTable.Columns.Add ("Coverage Code Id");

//                        collectionTable.Columns.Add ("Effective Date");

//                        collectionTable.Columns.Add ("Termination Date");

//                        if (EnrollmentCoverages != null) {

//                            foreach (EnrollmentCoverage currentEnrollmentCoverage in EnrollmentCoverages) {

//                                collectionTable.Rows.Add (

//                                    currentEnrollmentCoverage.EnrollmentCoverageId,

//                                    Application.BenefitPlanReferenceNameById (currentEnrollmentCoverage.BenefitPlanId),

//                                    currentEnrollmentCoverage.RateCode,

//                                    currentEnrollmentCoverage.CoverageCodeId,

//                                    currentEnrollmentCoverage.EffectiveDate.ToShortDateString (),

//                                    currentEnrollmentCoverage.TerminationDate.ToShortDateString ());

//                            }

//                        }

//                        #endregion

//                        break;

//                    case Server.Application.FormControlCollectionType.PcpAssignment:

//                        #region Pcp Assignment Collection Type

//                        collectionTable.Columns.Add ("Id");

//                        collectionTable.Columns.Add ("PCP Provider Name");

//                        collectionTable.Columns.Add ("PCP Affiliate");

//                        collectionTable.Columns.Add ("Effective Date");

//                        collectionTable.Columns.Add ("Termination Date");

//                        if (PcpAssignments != null) {

//                            foreach (PcpAssignment currentPcpAssignment in PcpAssignments) {

//                                collectionTable.Rows.Add (

//                                    currentPcpAssignment.PcpAssignmentId,

//                                    currentPcpAssignment.PcpProvider.ProviderName,

//                                    currentPcpAssignment.PcpAffiliate.ProviderName,

//                                    currentPcpAssignment.EffectiveDate.ToShortDateString (),

//                                    currentPcpAssignment.TerminationDate.ToShortDateString ());

//                            }

//                        }

//                        #endregion

//                        break;

//                    case Server.Application.FormControlCollectionType.PopulationMembership:

//                        #region Population Membership Collection Type

//                        collectionTable.Columns.Add ("Id");

//                        collectionTable.Columns.Add ("Name");

//                        collectionTable.Columns.Add ("Population Type");

//                        collectionTable.Columns.Add ("Effective");

//                        collectionTable.Columns.Add ("Termination");

//                        if (PopulationMembership != null) {

//                            foreach (Core.PopulationManagement.PopulationMembership currentPopulationMembership in PopulationMembership) {

//                                collectionTable.Rows.Add (

//                                    currentPopulationMembership.PopulationMembershipId,

//                                    currentPopulationMembership.Population.Name,

//                                    currentPopulationMembership.Population.PopulationType.Name,

//                                    currentPopulationMembership.EffectiveDate.ToShortDateString (),

//                                    (currentPopulationMembership.TerminationDate.Equals (new DateTime (9999, 12, 31)) ? "< active >" : currentPopulationMembership.TerminationDate.ToShortDateString ()));

//                            }

//                        }

//                        #endregion

//                        break;

//                    case Server.Application.FormControlCollectionType.PopulationMembershipServiceEvent:

//                        #region Population Membership Service Event Collection Type

//                        collectionTable.Columns.Add ("Id");

//                        collectionTable.Columns.Add ("Service Name");

//                        collectionTable.Columns.Add ("Expected Date");

//                        collectionTable.Columns.Add ("Actual Date");

//                        collectionTable.Columns.Add ("Status");

//                        if (PopulationMembership != null) {

//                            foreach (Core.PopulationManagement.PopulationEvents.PopulationMembershipServiceEvent currentPopulationMembershipServiceEvent in PopulationMembershipServiceEvents) {

//                                collectionTable.Rows.Add (

//                                    currentPopulationMembershipServiceEvent.PopulationMembershipId,

//                                    currentPopulationMembershipServiceEvent.ServiceName,

//                                    currentPopulationMembershipServiceEvent.ExpectedEventDate.ToString ("MM/dd/yyyy"),

//                                    (currentPopulationMembershipServiceEvent.EventDate.HasValue) ? currentPopulationMembershipServiceEvent.EventDate.Value.ToString ("MM/dd/yyyy") : String.Empty,

//                                    currentPopulationMembershipServiceEvent.StatusText

//                                    );

//                            }

//                        }

//                        #endregion

//                        break;

//                    case Server.Application.FormControlCollectionType.MemberService:

//                        #region Member Service Collection Type

//                        collectionTable.Columns.Add ("Id");

//                        collectionTable.Columns.Add ("Service Id");

//                        collectionTable.Columns.Add ("Name");

//                        collectionTable.Columns.Add ("Event Date");

//                        // collectionTable.Columns.Add ("County");

//                        foreach (Core.MedicalServices.MemberService currentService in MemberServices) {

//                            collectionTable.Rows.Add (

//                                currentService.MemberServiceId.ToString (),

//                                currentService.ServiceId.ToString (),

//                                currentService.ServiceName,

//                                currentService.EventDate.ToString ("MM/dd/yyyy")

//                            );

//                        }

//                        #endregion

//                        break;

//                    default:

//                        collectionTable.Columns.Add ("Id");

//                        break;

//                } // switch (collectionType)

//                //System.Diagnostics.Debug.WriteLine ("Collection [" + Name + "] ( END ): " + DateTime.Now.Subtract (startTime).Milliseconds.ToString ());

//                return collectionTable;

//            }

//        }

        #endregion


        #region Constructors

        virtual protected void InitializeControl (Application applicationReference) {

            BaseConstructor (applicationReference);

            ControlType = Server.Application.FormControlType.Collection;

            collectionType = Server.Application.FormControlCollectionType.NotSpecified;

            capabilities.HasValue = false;

            capabilities.HasLabel = false;

            capabilities.CanDataBind = true;

            capabilities.IsDataSource = true;

            label = new Label (Application, this);

            label.Visible = false;

            return;

        }

        override public void BaseConstructor (Mercury.Client.Core.Forms.Control parentControl, Server.Application.FormControl serverControl) {

            base.BaseConstructor (parentControl, serverControl);


            Server.Application.FormControlCollection serverCollection = (Server.Application.FormControlCollection) serverControl;

            collectionType = serverCollection.CollectionType;

            label = new Label (Application, this, serverCollection.Label);


            items.Clear ();

            items.AddRange (serverCollection.Items);

            SelectedItem = serverCollection.SelectedItem;

            return;

        }


        public Collection (Application applicationReference) {

            InitializeControl (applicationReference);

            return;

        }

        public Collection (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Server.Application.FormControlCollection serverControl) {

            InitializeControl (applicationReference);

            BaseConstructor (parentControl, serverControl);

            ChildServerControlsToLocal (this, serverControl);

            return;

        }

        #endregion


        #region Virtual Overrides

        public override void LocalControlToServer (Server.Application.FormControl parentControl, Server.Application.FormControl serverControl) {

            base.LocalControlToServer (parentControl, serverControl);


            ((Server.Application.FormControlCollection) serverControl).CollectionType = collectionType;

            //((Server.Application.FormControlCollection) serverControl).Items = new Int64[items.Count];

            //Items.CopyTo (((Server.Application.FormControlCollection) serverControl).Items, 0);

            ((Server.Application.FormControlCollection) serverControl).SelectedItem = selectedItem;


            ((Server.Application.FormControlCollection) serverControl).Label = new Server.Application.FormControlLabel ();

            label.LocalControlToServer (serverControl, ((Server.Application.FormControlCollection) serverControl).Label);

            return;

        }

        #endregion


        //#region Data Bindings

        //private void SetCollectionType () {

        //    Server.Application.FormControlDataBinding collectionBinding;

        //    collectionBinding = GetDataBinding ("Collection");

        //    if (collectionBinding != null) {

        //        Control dataSourceControl = Form.FindControlById (collectionBinding.DataSourceControlId);

        //        if (dataSourceControl != null) {

        //            try {

        //                String collectionDataType = dataSourceControl.GetDataBindingContextDataType (collectionBinding.BindingContext);

        //                if (!String.IsNullOrEmpty (collectionDataType)) {

        //                    collectionDataType = collectionDataType.Split ('|')[(collectionDataType.Split ('|').Length) - 1];

        //                }

        //                switch (collectionDataType) {

        //                    case "EntityAddress": collectionType = Server.Application.FormControlCollectionType.EntityAddress; break;

        //                    case "EntityContactInformation": collectionType = Server.Application.FormControlCollectionType.EntityContactInformation; break;

        //                    case "Enrollment": collectionType = Server.Application.FormControlCollectionType.Enrollment; break;

        //                    case "EnrollmentCoverage": collectionType = Server.Application.FormControlCollectionType.EnrollmentCoverage; break;

        //                    case "PcpAssignment": collectionType = Server.Application.FormControlCollectionType.PcpAssignment; break;

        //                    case "PopulationMembership": collectionType = Server.Application.FormControlCollectionType.PopulationMembership; break;

        //                    case "PopulationMembershipServiceEvent": collectionType = Server.Application.FormControlCollectionType.PopulationMembershipServiceEvent; break;

        //                    case "MemberService": collectionType = Server.Application.FormControlCollectionType.MemberService; break;

        //                    default: collectionType = Server.Application.FormControlCollectionType.NotSpecified; break;

        //                }

        //            }

        //            catch { /* DO NOTHING */ }

        //        }

        //    }

        //    return;

        //}

        //public override Dictionary<String, String> DataBindableProperties {

        //    get {

        //        SetCollectionType ();

        //        return base.DataBindableProperties;

        //    }

        //}

        //public override Dictionary<String, String> DataBindingContexts {

        //    get {

        //        SetCollectionType ();

        //        return base.DataBindingContexts;

        //    }

        //}

        //#endregion

    }

}
