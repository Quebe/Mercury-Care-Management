using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Forms.Controls {

    [Serializable]
    [DataContract (Name = "FormControlCollection")]
    public class Collection : Mercury.Server.Core.Forms.Control {

        #region Private Properties

        [DataMember (Name = "CollectionType")]
        protected Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType collectionType = Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.NotSpecified;


        [DataMember (Name = "Items")]
        protected List<Int64> items = new List<Int64> ();

        [DataMember (Name = "SelectedItem")]
        protected Int64 selectedItem = 0;

        [NonSerialized]
        List<Core.Entity.EntityAddress> entityAddresses = new List<Core.Entity.EntityAddress> ();

        [NonSerialized]
        List<Core.Entity.EntityContactInformation> entityContacts = new List<Core.Entity.EntityContactInformation> ();

        [NonSerialized]
        List<Member.MemberEnrollment> enrollments = new List<Member.MemberEnrollment> ();

        [NonSerialized]
        List<Member.MemberEnrollmentPcp> pcpAssignments = new List<Member.MemberEnrollmentPcp> ();

        [NonSerialized]
        List<Member.MemberEnrollmentCoverage> enrollmentCoverages = new List<Member.MemberEnrollmentCoverage> ();

        //[NonSerialized]
        //List<PopulationManagement.PopulationMembership> populationMembership = new List<Mercury.Server.Core.Population.PopulationMembership> ();

        //[NonSerialized]
        //List<PopulationManagement.PopulationEvents.PopulationMembershipServiceEvent> populationMembershipServiceEvents = new List<Mercury.Server.Core.Population.PopulationEvents.PopulationMembershipServiceEvent> ();

        [NonSerialized]
        List<Core.MedicalServices.MemberService> memberServices = new List<Mercury.Server.Core.MedicalServices.MemberService> ();

        [NonSerialized]
        List<Core.Provider.ProviderContract> providerContracts = new List<Mercury.Server.Core.Provider.ProviderContract> ();

        #endregion 


        #region Public Properties

        public Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType CollectionType { get { return collectionType; } set { collectionType = value; } }


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


        public override System.Xml.XmlDocument ExtendedPropertiesXml {

            get {

                System.Xml.XmlDocument extendedProperties = base.ExtendedPropertiesXml;

                ExtendedProperties_AddProperty (extendedProperties, "CollectionType", ((Int32) collectionType).ToString ());

                String itemList = String.Empty;

                if (items == null) { items = new List<Int64> (); }

                foreach (Int64 currentItem in items) {

                    itemList = itemList + "{" + currentItem.ToString () + "}";

                }

                itemList = itemList.Replace ("}{", "|");

                itemList = itemList.Replace ("{", "");

                itemList = itemList.Replace ("}", "");

                ExtendedProperties_AddProperty (extendedProperties, "Items", itemList);

                ExtendedProperties_AddProperty (extendedProperties, "SelectedItem", selectedItem.ToString ());

                return extendedProperties;

            }

        }

        public override void ExtendedPropertiesDeserialize (System.Xml.XmlNode extendedPropertiesXml) {

            base.ExtendedPropertiesDeserialize (extendedPropertiesXml);

            // foreach (System.Xml.XmlNode currentPropertyNode in extendedPropertiesXml.SelectNodes ("./Property")) {

            foreach (System.Xml.XmlNode currentPropertyNode in extendedPropertiesXml.ChildNodes) {

                switch (currentPropertyNode.Attributes["Name"].InnerText) {

                    case "CollectionType": collectionType = (Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType) Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "Items":

                        foreach (String currentItem in currentPropertyNode.InnerText.Split ('|')) {

                            Int64 itemId;

                            if (Int64.TryParse (currentItem, out itemId)) { items.Add (itemId); }

                        }

                        break;

                    case "SelectedItem": selectedItem = Convert.ToInt64 (currentPropertyNode.InnerText); break;

                }

            }

            return;

        }


        public override Boolean HasValue { get { return selectedItem != 0; } }

        public override String Value { get { return (HasValue) ? selectedItem.ToString () : String.Empty; } }


        public override Application Application {

            set {

                base.Application = value;

                if (label != null) { label.Application = value; }

            }

        }

        #endregion


        #region Constructors

        protected void InitializeControl (Application applicationReference) {

            BaseConstructor (applicationReference);

            controlType = Mercury.Server.Core.Forms.Enumerations.FormControlType.Collection;

            capabilities.HasValue = true;

            capabilities.HasLabel = true;

            capabilities.IsDataSource = true;

            capabilities.CanDataBind = true;

            return;

        }

        public Collection (Application applicationReference) {

            InitializeControl (applicationReference);

            return;

        }

        public Collection (Application applicationReference, Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType forCollectionType) {

            InitializeControl (applicationReference);

            collectionType = forCollectionType;

            return;

        }

        #endregion


        #region Data Bindings

        private void SetCollectionType () {

            Mercury.Server.Core.Forms.Structures.DataBinding collectionBinding;

            collectionBinding = GetDataBinding ("Collection");

            if (collectionBinding != null) {

                if (Form == null) { return; }

                Control dataSourceControl = Form.FindControlById (collectionBinding.DataSourceControlId);

                if (dataSourceControl != null) {

                    try {

                        String collectionDataType = dataSourceControl.GetDataBindingContextDataType (collectionBinding.BindingContext);

                        if (!String.IsNullOrEmpty (collectionDataType)) {

                            collectionDataType = collectionDataType.Split ('|')[(collectionDataType.Split ('|').Length) - 1];

                        }

                        switch (collectionDataType) {

                            case "EntityAddress": collectionType = Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.EntityAddress; break;

                            case "EntityContactInformation": collectionType = Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.EntityContactInformation; break;

                            case "MemberRelationship": collectionType = Enumerations.FormControlCollectionType.MemberRelationship; break;


                            case "Enrollment": // BACKWARDS COMPATIBILITY

                            case "MemberEnrollment":
                                
                                collectionType = Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.MemberEnrollment; 
                                
                                break;


                            case "EnrollmentCoverage": // BACKWARDS COMPATIBILITY

                            case "MemberEnrollmentCoverage":

                                collectionType = Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.MemberEnrollmentCoverage; 
                                                                
                                break;


                            case "PcpAssignment": // BACKWARDS COMPATIBILITY

                            case "MemberEnrollmentPcp":

                                collectionType = Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.MemberEnrollmentPcp;
                                
                                break;


                            case "PopulationMembership": collectionType = Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.PopulationMembership; break;

                            case "MemberService": collectionType = Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.MemberService; break;

                            case "ProviderContract": collectionType = Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.ProviderContract; break;


                            default: collectionType = Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.NotSpecified; break;

                        }

                    }

                    catch { /* DO NOTHING */ }

                }

            }

            return;

        }

        public override Dictionary<String, String> DataBindableProperties {

            get {

                SetCollectionType ();

                Dictionary<String, String> bindableProperties = new Dictionary<String, String> ();

                bindableProperties.Add ("Collection", "Collection");

                switch (collectionType) {

                    case Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.EntityAddress:

                        #region Entity Address Collection Properties

                        // bindableProperties.Add ("EntityAddressId", "Id|EntityAddress");

                        bindableProperties.Add ("AddressType", "String");

                        bindableProperties.Add ("AddressTypeDescription", "String");

                        bindableProperties.Add ("EffectiveDate", "DateTime");

                        bindableProperties.Add ("TerminationDate", "DateTime");

                        bindableProperties.Add ("SegmentBetweenDate", "DateTime");

                        // bindableProperties.Add ("Line1", "String");

                        // bindableProperties.Add ("Line2", "String");

                        // bindableProperties.Add ("City", "String");

                        // bindableProperties.Add ("State", "String");

                        // bindableProperties.Add ("ZipCode", "String");

                        // bindableProperties.Add ("ZipPlus4", "String");

                        // bindableProperties.Add ("PostalCode", "String");

                        #endregion

                        break;

                    case Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.EntityContactInformation:

                        #region Entity Contact Collection Properties

                        bindableProperties.Add ("ContactType", "Integer");

                        bindableProperties.Add ("ContactTypeDescription", "String");

                        bindableProperties.Add ("ContactSequence", "Integer");

                        bindableProperties.Add ("EffectiveDate", "DateTime");

                        bindableProperties.Add ("TerminationDate", "DateTime");

                        bindableProperties.Add ("SegmentBetweenDate", "DateTime");

                        #endregion

                        break;

                    case Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.MemberRelationship:

                        #region Relationship Collection Properties


                        #endregion

                        break;

                    case Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.MemberEnrollment:

                        #region Enrollment Collection Properties

                        // bindableProperties.Add ("MemberId", "Id|Member");

                        // bindableProperties.Add ("SponsorId", "Id|Sponsor");

                        // bindableProperties.Add ("SubscriberId", "Id|Member");

                        bindableProperties.Add ("InsurerId", "Id|Insurer");

                        // bindableProperties.Add ("ProgramId", "Id|Program");

                        bindableProperties.Add ("EffectiveDate", "DateTime");

                        bindableProperties.Add ("TerminationDate", "DateTime");

                        bindableProperties.Add ("SegmentBetweenDate", "DateTime");

                        #endregion

                        break;

                    case Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.MemberEnrollmentCoverage:

                        #region Enrollment Coverage Collection Properties

                        // bindableProperties.Add ("EnrollmentCoverageId", "Id|EnrollmentCoverage");

                        // bindableProperties.Add ("EnrollmentId", "Id|Enrollment");

                        // bindableProperties.Add ("RateCode", "String");

                        // bindableProperties.Add ("CoverageCodeId", "String");

                        bindableProperties.Add ("EffectiveDate", "DateTime");

                        bindableProperties.Add ("TerminationDate", "DateTime");

                        bindableProperties.Add ("SegmentBetweenDate", "DateTime");

                        #endregion

                        break;

                    case Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.MemberEnrollmentPcp:

                        #region Enrollment Coverage Collection Properties

                        bindableProperties.Add ("EffectiveDate", "DateTime");

                        bindableProperties.Add ("TerminationDate", "DateTime");

                        bindableProperties.Add ("SegmentBetweenDate", "DateTime");

                        #endregion

                        break;

                    case Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.PopulationMembership:

                        #region Population Membership Collection Properties

                        bindableProperties.Add ("PopulationType", "String");

                        bindableProperties.Add ("EffectiveDate", "DateTime");

                        bindableProperties.Add ("TerminationDate", "DateTime");

                        bindableProperties.Add ("SegmentBetweenDate", "DateTime");

                        #endregion

                        break;

                    case Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.PopulationMembershipServiceEvent:

                        #region Population Membership Service Event Collection Properties

                        bindableProperties.Add ("ServiceName", "String");

                        bindableProperties.Add ("EventDate", "DateTime");

                        bindableProperties.Add ("ExpectedEventDate", "DateTime");

                        bindableProperties.Add ("IsOpenStatus", "Boolean");

                        #endregion

                        break;

                    case Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.MemberService:

                        #region Member Service Properties

                        bindableProperties.Add ("ServiceId", "Id|Service");

                        bindableProperties.Add ("ServiceName", "String");

                        bindableProperties.Add ("EventDate", "DateTime");

                        #endregion

                        break;

                    case Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.ProviderContract:

                        #region Provider Contract Collection Properties
                        
                        bindableProperties.Add ("InsurerId", "Id|Insurer");

                        bindableProperties.Add ("ProgramId", "Id|Program");

                        bindableProperties.Add ("EffectiveDate", "DateTime");

                        bindableProperties.Add ("TerminationDate", "DateTime");

                        bindableProperties.Add ("SegmentBetweenDate", "DateTime");

                        #endregion

                        break;

                }

                return bindableProperties;

            }

        }

        public override Dictionary<String, String> DataBindingContexts {

            get {

                SetCollectionType ();

                Dictionary<String, String> bindingContexts = new Dictionary<String, String> ();

                switch (collectionType) {

                    case Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.EntityAddress:

                        bindingContexts = new Core.Entity.EntityAddress (null).DataBindingContexts;

                        break;

                    case Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.EntityContactInformation:

                        bindingContexts = new Core.Entity.EntityContactInformation (null).DataBindingContexts;

                        break;

                    case Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.MemberEnrollment:

                        bindingContexts = new Member.MemberEnrollment (null).DataBindingContexts;

                        break;

                    case Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.MemberEnrollmentCoverage:

                        bindingContexts = new Member.MemberEnrollmentCoverage (null).DataBindingContexts;

                        break;

                    case Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.MemberEnrollmentPcp:

                        bindingContexts = new Member.MemberEnrollmentPcp (null).DataBindingContexts;

                        break;

                    //case Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.PopulationMembership:

                    //    bindingContexts = new Core.Population.PopulationMembership (null).DataBindingContexts;

                    //    break;

                    //case Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.PopulationMembershipServiceEvent:

                    //    bindingContexts = new Core.Population.PopulationEvents.PopulationMembershipServiceEvent (null).DataBindingContexts;

                    //    break;

                    //case Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.MemberService:

                    //    bindingContexts = new Core.MedicalServices.MemberService (null).DataBindingContexts;

                    //    break;

                    case Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.ProviderContract:

                        bindingContexts = new Provider.ProviderContract (null).DataBindingContexts;

                        break;

                } // switch (collectionType) {

                return bindingContexts;

            }

        }


        private Boolean FilterEntityAddressAllowed (Core.Entity.EntityAddress entityAddress) {

            Boolean addressAllowed = true; 
            
            Control dataSourceControl;

            String filterValue;

            DateTime compareDate;


            foreach (Structures.DataBinding currentBinding in DataBindings) {

                dataSourceControl = Form.FindControlById (currentBinding.DataSourceControlId);

                if (dataSourceControl != null) {

                    filterValue = dataSourceControl.EvaluateDataBinding (currentBinding);

                }

                else { filterValue = base.EvaluateDataBinding (currentBinding); }

                switch (currentBinding.BoundProperty) {

                    case "AddressType": addressAllowed = addressAllowed && (entityAddress.AddressType.Equals (filterValue)); break;

                    case "AddressTypeDescription": addressAllowed = addressAllowed && (entityAddress.AddressTypeDescription.Equals (filterValue)); break;


                    case "EffectiveDate":

                        if (DateTime.TryParse (filterValue, out compareDate)) {

                            if (!(entityAddress.TerminationDate.Equals (compareDate))) { addressAllowed = false; }

                        }

                        break;

                    case "TerminationDate":

                        if (DateTime.TryParse (filterValue, out compareDate)) {

                            if (!(entityAddress.TerminationDate.Equals (compareDate))) { addressAllowed = false; }

                        }

                        break;

                    case "SegmentBetweenDate":

                        if (DateTime.TryParse (filterValue, out compareDate)) {

                            if (!((compareDate >= entityAddress.EffectiveDate) && (compareDate <= entityAddress.TerminationDate))) { addressAllowed = false; }

                        }

                        break;

                } // switch (currentBinding.BoundProperty) {



            }

            return addressAllowed;

        }

        private Boolean FilterEntityContactInformationAllowed (Core.Entity.EntityContactInformation entityContactInformation) {

            Boolean contactAllowed = true;

            Control dataSourceControl;

            String filterValue;

            DateTime compareDate;

            Int32 compareInt32;


            foreach (Structures.DataBinding currentBinding in DataBindings) {

                dataSourceControl = Form.FindControlById (currentBinding.DataSourceControlId);

                if (dataSourceControl != null) {

                    filterValue = dataSourceControl.EvaluateDataBinding (currentBinding);

                }

                else { filterValue = base.EvaluateDataBinding (currentBinding); }

                switch (currentBinding.BoundProperty) {

                    case "ContactType":

                        if (Int32.TryParse (filterValue, out compareInt32)) {

                            if (!(((Int32) entityContactInformation.ContactType) == compareInt32)) contactAllowed = false;

                        }

                        break;

                    case "ContactTypeDescription": contactAllowed = contactAllowed && (entityContactInformation.ContactTypeDescription.Equals (filterValue)); break;

                    case "ContactSequence":

                        if (Int32.TryParse (filterValue, out compareInt32)) {

                            if (!(entityContactInformation.ContactSequence == compareInt32)) contactAllowed = false;

                        }

                        break;


                    case "EffectiveDate":

                        if (DateTime.TryParse (filterValue, out compareDate)) {

                            if (!(entityContactInformation.TerminationDate.Equals (compareDate))) { contactAllowed = false; }

                        }

                        break;

                    case "TerminationDate":

                        if (DateTime.TryParse (filterValue, out compareDate)) {

                            if (!(entityContactInformation.TerminationDate.Equals (compareDate))) { contactAllowed = false; }

                        }

                        break;

                    case "SegmentBetweenDate":

                        if (DateTime.TryParse (filterValue, out compareDate)) {

                            if (!((compareDate >= entityContactInformation.EffectiveDate) && (compareDate <= entityContactInformation.TerminationDate))) { contactAllowed = false; }

                        }

                        break;

                } // switch (currentBinding.BoundProperty) {



            }

            return contactAllowed;

        }

        private Boolean FilterEnrollmentAllowed (Member.MemberEnrollment enrollment) {

            Boolean enrollmentAllowed = true;

            Control dataSourceControl;

            String filterValue;

            DateTime compareDate;

            Int64 compareId;


            foreach (Structures.DataBinding currentBinding in DataBindings) {

                dataSourceControl = Form.FindControlById (currentBinding.DataSourceControlId); 

                if (dataSourceControl != null) {

                    filterValue = dataSourceControl.EvaluateDataBinding (currentBinding);

                }

                else { filterValue = base.EvaluateDataBinding (currentBinding); }

                switch (currentBinding.BoundProperty) {

                    //case "InsurerId": 

                    //    if (Int64.TryParse (filterValue, out compareId)) {

                    //        if (!(enrollment.InsurerId.Equals (compareId))) { enrollmentAllowed = false; }

                    //    }

                    //    break;

                    case "ProgramId":

                        if (Int64.TryParse (filterValue, out compareId)) {

                            if (!(enrollment.ProgramId.Equals (compareId))) { enrollmentAllowed = false; }

                        }

                        break;


                    case "EffectiveDate":

                        if (DateTime.TryParse (filterValue, out compareDate)) {

                            if (!(enrollment.TerminationDate.Equals (compareDate))) { enrollmentAllowed = false; }

                        }

                        break;

                    case "TerminationDate":

                        if (DateTime.TryParse (filterValue, out compareDate)) {

                            if (!(enrollment.TerminationDate.Equals (compareDate))) { enrollmentAllowed = false; }

                        }

                        break;

                    case "SegmentBetweenDate": 

                        if (DateTime.TryParse (filterValue, out compareDate)) {

                            if (!((compareDate >= enrollment.EffectiveDate) && (compareDate <= enrollment.TerminationDate))) { enrollmentAllowed = false; }

                        }

                        break;

                }

            }

            return enrollmentAllowed;

        }

        private Boolean FilterEnrollmentCoverageAllowed (Member.MemberEnrollmentCoverage enrollmentCoverage) {

            Boolean enrollmentCoverageAllowed = true;

            Control dataSourceControl;

            String filterValue;

            DateTime compareDate;


            foreach (Structures.DataBinding currentBinding in DataBindings) {

                dataSourceControl = Form.FindControlById (currentBinding.DataSourceControlId);

                if (dataSourceControl != null) {

                    filterValue = dataSourceControl.EvaluateDataBinding (currentBinding);

                }

                else { filterValue = base.EvaluateDataBinding (currentBinding); }

                switch (currentBinding.BoundProperty) {

                    case "EffectiveDate":

                        if (DateTime.TryParse (filterValue, out compareDate)) {

                            if (!(enrollmentCoverage.TerminationDate.Equals (compareDate))) { enrollmentCoverageAllowed = false; }

                        }

                        break;

                    case "TerminationDate":

                        if (DateTime.TryParse (filterValue, out compareDate)) {

                            if (!(enrollmentCoverage.TerminationDate.Equals (compareDate))) { enrollmentCoverageAllowed = false; }

                        }

                        break;

                    case "SegmentBetweenDate":

                        if (DateTime.TryParse (filterValue, out compareDate)) {

                            if (!((compareDate >= enrollmentCoverage.EffectiveDate) && (compareDate <= enrollmentCoverage.TerminationDate))) { enrollmentCoverageAllowed = false; }

                        }

                        break;

                }

            }

            return enrollmentCoverageAllowed;

        }

        private Boolean FilterPcpAssignmentAllowed (Member.MemberEnrollmentPcp pcpAssignment) {

            Boolean pcpAssignmentAllowed = true;

            Control dataSourceControl;

            String filterValue;

            DateTime compareDate;


            foreach (Structures.DataBinding currentBinding in DataBindings) {

                dataSourceControl = Form.FindControlById (currentBinding.DataSourceControlId);

                if (dataSourceControl != null) {

                    filterValue = dataSourceControl.EvaluateDataBinding (currentBinding);

                }

                else { filterValue = base.EvaluateDataBinding (currentBinding); }

                switch (currentBinding.BoundProperty) {

                    case "EffectiveDate":

                        if (DateTime.TryParse (filterValue, out compareDate)) {

                            if (!(pcpAssignment.TerminationDate.Equals (compareDate))) {

                                pcpAssignmentAllowed = false;

                            }

                        }

                        break;

                    case "TerminationDate":

                        if (DateTime.TryParse (filterValue, out compareDate)) {

                            if (!(pcpAssignment.TerminationDate.Equals (compareDate))) {

                                pcpAssignmentAllowed = false;

                            }

                        }

                        break;

                    case "SegmentBetweenDate":

                        if (DateTime.TryParse (filterValue, out compareDate)) {

                            if (!((compareDate >= pcpAssignment.EffectiveDate) && (compareDate <= pcpAssignment.TerminationDate))) { pcpAssignmentAllowed = false; }

                        }

                        break;
                }

            }

            return pcpAssignmentAllowed;

        }

        //private Boolean FilterPopulationMembershipAllowed (Core.Population.PopulationMembership populationMembership) {

        //    Boolean populationMembershipAllowed = true;

        //    FormControl dataSourceControl;

        //    String filterValue;

        //    DateTime compareDate;


        //    foreach (Structures.DataBinding currentBinding in DataBindings) {

        //        dataSourceControl = Form.FindControlById (currentBinding.DataSourceControlId);

        //        if (dataSourceControl != null) {

        //            filterValue = dataSourceControl.EvaluateDataBinding (currentBinding);

        //        }

        //        else { filterValue = base.EvaluateDataBinding (currentBinding); }

        //        switch (currentBinding.BoundProperty) {

        //            case "PopulationType":

        //                if (!populationMembership.Population.PopulationType.Name.Equals (filterValue)) {

        //                    populationMembershipAllowed = false;

        //                }

        //                break;

        //            case "EffectiveDate":

        //                if (DateTime.TryParse (filterValue, out compareDate)) {

        //                    if (!(populationMembership.TerminationDate.Equals (compareDate))) {

        //                        populationMembershipAllowed = false;

        //                    }

        //                }

        //                break;

        //            case "TerminationDate":

        //                if (DateTime.TryParse (filterValue, out compareDate)) {

        //                    if (!(populationMembership.TerminationDate.Equals (compareDate))) {

        //                        populationMembershipAllowed = false;

        //                    }

        //                }

        //                break;

        //            case "SegmentBetweenDate":

        //                if (DateTime.TryParse (filterValue, out compareDate)) {

        //                    if (!((compareDate >= populationMembership.EffectiveDate) && (compareDate <= populationMembership.TerminationDate))) { populationMembershipAllowed = false; }

        //                }

        //                break;
        //        }

        //    }

        //    return populationMembershipAllowed;

        //}

        //private Boolean FilterPopulationMembershipServiceEventAllowed (Core.Population.PopulationEvents.PopulationMembershipServiceEvent populationMembershipServiceEvent) {

        //    Boolean populationMembershipServiceEventAllowed = true;

        //    FormControl dataSourceControl;

        //    String filterValue;

        //    DateTime compareDate;


        //    foreach (Structures.DataBinding currentBinding in DataBindings) {

        //        dataSourceControl = Form.FindControlById (currentBinding.DataSourceControlId);

        //        if (dataSourceControl != null) {

        //            filterValue = dataSourceControl.EvaluateDataBinding (currentBinding);

        //        }

        //        else { filterValue = base.EvaluateDataBinding (currentBinding); }

        //        switch (currentBinding.BoundProperty) {

        //            case "EventDate":

        //                if (DateTime.TryParse (filterValue, out compareDate)) {

        //                    if (!(populationMembershipServiceEvent.EventDate.Equals (compareDate))) {

        //                        populationMembershipServiceEventAllowed = false;

        //                    }

        //                }

        //                break;

        //            case "ExpectedEventDate":

        //                if (DateTime.TryParse (filterValue, out compareDate)) {

        //                    if (!(populationMembershipServiceEvent.ExpectedEventDate.Equals (compareDate))) {

        //                        populationMembershipServiceEventAllowed = false;

        //                    }

        //                }

        //                break;

        //            case "IsOpenStatus":

        //                populationMembershipServiceEventAllowed = (Convert.ToBoolean (((Int32) populationMembershipServiceEvent.Status)) == Convert.ToBoolean (filterValue));

        //                break;

        //        }

        //    }

        //    return populationMembershipServiceEventAllowed;

        //}

        private Boolean FilterMemberServiceAllowed (Core.MedicalServices.MemberService memberService) {

            Boolean allowed = true;

            Control dataSourceControl;

            String filterValue;

            Int64 compareInt64;

            DateTime compareDate;


            if (memberService == null) { return false; }

            foreach (Structures.DataBinding currentBinding in DataBindings) {

                dataSourceControl = Form.FindControlById (currentBinding.DataSourceControlId);

                if (dataSourceControl != null) {

                    filterValue = dataSourceControl.EvaluateDataBinding (currentBinding);

                }

                else { filterValue = base.EvaluateDataBinding (currentBinding); }

                switch (currentBinding.BoundProperty) {

                    case "ServiceId":

                        if (Int64.TryParse (filterValue, out compareInt64)) {

                            if (memberService.ServiceId != compareInt64) { allowed = false; }

                        }

                        break;

                    case "ServiceName":

                        if (memberService.Name.Trim () != filterValue.Trim ()) { allowed = false; }

                        break;

                    case "EventDate":

                        if (DateTime.TryParse (filterValue, out compareDate)) {

                            if (!(memberService.EventDate.Equals (compareDate))) { allowed = false; }

                        }

                        break;

                }

            }

            return allowed;

        }

        private Boolean FilterProviderContractAllowed (Provider.ProviderContract providerContract) {

            Boolean providerContractAllowed = true;

            Control dataSourceControl;

            String filterValue;

            DateTime compareDate;

            Int64 compareId;


            foreach (Structures.DataBinding currentBinding in DataBindings) {

                dataSourceControl = Form.FindControlById (currentBinding.DataSourceControlId);

                if (dataSourceControl != null) {

                    filterValue = dataSourceControl.EvaluateDataBinding (currentBinding);

                }

                else { filterValue = base.EvaluateDataBinding (currentBinding); }

                switch (currentBinding.BoundProperty) {

                    case "InsurerId":

                        if (Int64.TryParse (filterValue, out compareId)) {

                            if (providerContract.Program != null) {

                                if (!(providerContract.Program.InsurerId.Equals (compareId))) { providerContractAllowed = false; }

                            }

                            else { providerContractAllowed = false; }

                        }

                        break;

                    case "ProgramId":

                        if (Int64.TryParse (filterValue, out compareId)) {

                            if (!(providerContract.ProgramId.Equals (compareId))) { providerContractAllowed = false; }

                        }

                        break;

                    case "EffectiveDate":

                        if (DateTime.TryParse (filterValue, out compareDate)) {

                            if (!(providerContract.TerminationDate.Equals (compareDate))) { providerContractAllowed = false; }

                        }

                        break;

                    case "TerminationDate":

                        if (DateTime.TryParse (filterValue, out compareDate)) {

                            if (!(providerContract.TerminationDate.Equals (compareDate))) { providerContractAllowed = false; }

                        }

                        break;

                    case "SegmentBetweenDate":

                        if (DateTime.TryParse (filterValue, out compareDate)) {

                            if (!((compareDate >= providerContract.EffectiveDate) && (compareDate <= providerContract.TerminationDate))) { providerContractAllowed = false; }

                        }

                        break;

                }

            }

            return providerContractAllowed;

        }

        public override void OnDataSourceChanged (Control dataSourceControl, Boolean propogate) {

            String collectionData;

            String collectionIdentifier;

            Int64 itemId;

            Boolean allowed;


            SetCollectionType ();

            base.OnDataSourceChanged (dataSourceControl, propogate);

            foreach (Mercury.Server.Core.Forms.Structures.DataBinding currentBinding in GetDataBindings (dataSourceControl.ControlId)) {

                switch (currentBinding.BoundProperty) {

                    case "Collection":

                        #region Collection Bound Property Data Source Changed

                        items.Clear ();

                        collectionData = dataSourceControl.EvaluateDataBinding (currentBinding);

                        collectionIdentifier = collectionData.Split ('|')[0];

                        switch (collectionIdentifier) {

                            case "EntityAddress":

                                #region Entity Address Collection

                                collectionType = Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.EntityAddress;

                                entityAddresses = new List<Core.Entity.EntityAddress> ();

                                foreach (String currentId in collectionData.Split ('|')) {

                                    if (Int64.TryParse (currentId, out itemId)) {

                                        Core.Entity.EntityAddress currentAddress = new Core.Entity.EntityAddress (application, itemId);

                                        Boolean addressAllowed = FilterEntityAddressAllowed (currentAddress);

                                        if (addressAllowed) {

                                            items.Add (itemId);

                                            entityAddresses.Add (currentAddress);

                                        }

                                    }

                                }
                                
                                #endregion

                                break;

                            case "EntityContactInformation":

                                #region Entity Contact Collection

                                collectionType = Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.EntityContactInformation;

                                entityContacts = new List<Core.Entity.EntityContactInformation> ();

                                foreach (String currentId in collectionData.Split ('|')) {

                                    if (Int64.TryParse (currentId, out itemId)) {

                                        Core.Entity.EntityContactInformation currentContact = new Core.Entity.EntityContactInformation (application, itemId);

                                        Boolean contactAllowed = FilterEntityContactInformationAllowed (currentContact);

                                        if (contactAllowed) {

                                            items.Add (itemId);

                                            entityContacts.Add (currentContact);

                                        }

                                    }

                                }

                                #endregion

                                break;

                            case "Enrollment":

                                #region Enrollment Collection

                                collectionType = Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.MemberEnrollment;

                                enrollments = new List<Member.MemberEnrollment> ();

                                foreach (String currentId in collectionData.Split ('|')) {

                                    if (Int64.TryParse (currentId, out itemId)) {

                                        Member.MemberEnrollment currentEnrollment = application.MemberEnrollmentGet (itemId);

                                        Boolean enrollmentAllowed = FilterEnrollmentAllowed (currentEnrollment);

                                        if (enrollmentAllowed) { 

                                            items.Add (itemId);

                                            enrollments.Add (currentEnrollment);

                                        }

                                    }

                                }

                                #endregion

                                break;

                            case "EnrollmentCoverage":

                                #region Enrollment Coverage Collection

                                collectionType = Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.MemberEnrollmentCoverage;

                                enrollmentCoverages = new List<Member.MemberEnrollmentCoverage> ();

                                foreach (String currentId in collectionData.Split ('|')) {

                                    if (Int64.TryParse (currentId, out itemId)) {

                                        Member.MemberEnrollmentCoverage currentEnrollmentCoverage = application.MemberEnrollmentCoverageGet (itemId);

                                        Boolean enrollmentCoverageAllowed = FilterEnrollmentCoverageAllowed (currentEnrollmentCoverage);

                                        if (enrollmentCoverageAllowed) {

                                            items.Add (itemId);

                                            enrollmentCoverages.Add (currentEnrollmentCoverage);

                                        }

                                    }

                                }

                                #endregion

                                break;

                            case "PcpAssignment":

                                #region Pcp Assignment Collection

                                collectionType = Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.MemberEnrollmentPcp;

                                pcpAssignments = new List<Member.MemberEnrollmentPcp> ();

                                foreach (String currentId in collectionData.Split ('|')) {

                                    if (Int64.TryParse (currentId, out itemId)) {

                                        Member.MemberEnrollmentPcp currentPcpAssignment = application.MemberEnrollmentPcpGet (itemId);

                                        Boolean pcpAssignmentAllowed = FilterPcpAssignmentAllowed (currentPcpAssignment);

                                        if (pcpAssignmentAllowed) {

                                            items.Add (itemId);

                                            pcpAssignments.Add (currentPcpAssignment);

                                        }

                                    }

                                }

                                #endregion

                                break;

                            //case "PopulationMembership":

                            //    #region Population Membership Collection 

                            //    collectionType = Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.PopulationMembership;

                            //    populationMembership = new List<Mercury.Server.Core.Population.PopulationMembership> ();

                            //    foreach (String currentId in collectionData.Split ('|')) {

                            //        if (Int64.TryParse (currentId, out itemId)) {

                            //            PopulationManagement.PopulationMembership currentPopulationMembership = application.PopulationMembershipGet (itemId);

                            //            Boolean populationMembershipAllowed = FilterPopulationMembershipAllowed (currentPopulationMembership);

                            //            if (populationMembershipAllowed) {

                            //                items.Add (itemId);

                            //                populationMembership.Add (currentPopulationMembership);

                            //            }

                            //        }

                            //    }

                            //    #endregion 

                            //    break;

                            //case "PopulationMembershipServiceEvent":

                            //    #region Population MembershipServiceEvent Collection

                            //    collectionType = Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.PopulationMembershipServiceEvent;

                            //    populationMembershipServiceEvents = new List<Mercury.Server.Core.Population.PopulationEvents.PopulationMembershipServiceEvent> ();

                            //    foreach (String currentId in collectionData.Split ('|')) {

                            //        if (Int64.TryParse (currentId, out itemId)) {

                            //            PopulationManagement.PopulationEvents.PopulationMembershipServiceEvent currentPopulationMembershipServiceEvent = application.PopulationMembershipServiceEventGet (itemId);

                            //            Boolean populationMembershipServiceEventAllowed = FilterPopulationMembershipServiceEventAllowed (currentPopulationMembershipServiceEvent);

                            //            if (populationMembershipServiceEventAllowed) {

                            //                items.Add (itemId);

                            //                populationMembershipServiceEvents.Add (currentPopulationMembershipServiceEvent);

                            //            }

                            //        }

                            //    }

                            //    #endregion

                            //    break;

                            case "MemberService":

                                #region Member Service Collection

                                collectionType = Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.MemberService;

                                memberServices = new List<Mercury.Server.Core.MedicalServices.MemberService> ();

                                foreach (String currentId in collectionData.Split ('|')) {

                                    if (Int64.TryParse (currentId, out itemId)) {

                                        Core.MedicalServices.MemberService currentMemberService = application.MemberServiceGet (itemId);

                                        allowed = FilterMemberServiceAllowed (currentMemberService);

                                        if (allowed) {

                                            items.Add (itemId);

                                            memberServices.Add (currentMemberService);

                                        }

                                    }

                                }

                                #endregion

                                break;

                            case "ProviderContract":

                                #region Enrollment Collection

                                collectionType = Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.ProviderContract;

                                providerContracts = new List<Provider.ProviderContract> ();

                                foreach (String currentId in collectionData.Split ('|')) {

                                    if (Int64.TryParse (currentId, out itemId)) {

                                        Provider.ProviderContract currentProviderContract = application.ProviderContractGet (itemId);

                                        Boolean providerContractAllowed = FilterProviderContractAllowed (currentProviderContract);

                                        if (providerContractAllowed) {

                                            items.Add (itemId);

                                            providerContracts.Add (currentProviderContract);

                                        }

                                    }

                                }

                                #endregion

                                break;

                        } // switch (collectionIdentifier) {

                        if (items.Count > 0) { SelectedItem = items[0]; } else { SelectedItem = 0; }

                        #endregion

                        break;

                } // switch (currentBinding.BoundProperty) {

            } // foreach (Mercury.Services.Core.Forms.DataBinding currentBinding in GetDataBindings (dataSourceControl.Id)) {

        }

        public override String EvaluateDataBinding (Structures.DataBinding dataBinding) {

            String dataValue = String.Empty;

            try {

                if (selectedItem == 0) { return String.Empty; }

                switch (collectionType) {

                    case Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.EntityAddress:

                        #region Entity Address

                        Core.Entity.EntityAddress selectedAddress = new Core.Entity.EntityAddress (application, selectedItem);

                        if (selectedAddress != null) {

                            dataValue = selectedAddress.EvaluateDataBinding (dataBinding.BindingContext);

                        }

                        else { dataValue = "!Error"; }

                        #endregion

                        break;

                    case Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.EntityContactInformation:

                        #region Entity Contact Information

                        Core.Entity.EntityContactInformation selectedContact = new Core.Entity.EntityContactInformation (application, selectedItem);

                        if (selectedContact != null) {

                            dataValue = selectedContact.EvaluateDataBinding (dataBinding.BindingContext);

                        }

                        else { dataValue = "!Error"; }

                        #endregion

                        break;

                    case Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.MemberEnrollment:

                        #region Enrollment

                        Member.MemberEnrollment selectedEnrollment = new Member.MemberEnrollment (application, selectedItem);

                        if (selectedEnrollment != null) {

                            dataValue = selectedEnrollment.EvaluateDataBinding (dataBinding.BindingContext);

                        }

                        else { dataValue = "!Error"; }

                        #endregion

                        break;

                    case Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.MemberEnrollmentCoverage:

                        #region Enrollment Coverage

                        Member.MemberEnrollmentCoverage selectedEnrollmentCoverage = new Member.MemberEnrollmentCoverage (application, selectedItem);

                        if (selectedEnrollmentCoverage != null) {

                            dataValue = selectedEnrollmentCoverage.EvaluateDataBinding (dataBinding.BindingContext);

                        }

                        else { dataValue = "!Error"; }

                        #endregion

                        break;

                    case Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.MemberEnrollmentPcp:

                        #region Pcp Assignment

                        Member.MemberEnrollmentPcp selectedPcpAssignment = new Member.MemberEnrollmentPcp (application, selectedItem);

                        if (selectedPcpAssignment != null) {

                            dataValue = selectedPcpAssignment.EvaluateDataBinding (dataBinding.BindingContext);

                        }

                        else { dataValue = "!Error"; }

                        #endregion

                        break;

                    //case Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.PopulationMembership:

                    //    #region PopulationMembership

                    //    Core.Population.PopulationMembership selectedPopulationMembership = application.PopulationMembershipGet (selectedItem);

                    //    if (selectedPopulationMembership != null) {

                    //        dataValue = selectedPopulationMembership.EvaluateDataBinding (dataBinding.BindingContext);

                    //    }

                    //    else { dataValue = "!Error"; }

                    //    #endregion

                    //    break;

                    //case Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.PopulationMembershipServiceEvent:

                    //    #region PopulationMembershipServiceEvent

                    //    Core.Population.PopulationEvents.PopulationMembershipServiceEvent selectedPopulationMembershipServiceEvent = application.PopulationMembershipServiceEventGet (selectedItem);

                    //    if (selectedPopulationMembershipServiceEvent != null) {

                    //        dataValue = selectedPopulationMembershipServiceEvent.EvaluateDataBinding (dataBinding.BindingContext);

                    //    }

                    //    else { dataValue = "!Error"; }

                    //    #endregion

                    //    break;

                    //case Mercury.Server.Core.Forms.Enumerations.FormControlCollectionType.MemberService:

                    //    #region Member Service

                    //    Core.MedicalServices.MemberService selectedMemberService = application.MemberServiceGet (selectedItem);

                    //    if (selectedMemberService != null) {

                    //        dataValue = selectedMemberService.EvaluateDataBinding (dataBinding.BindingContext);

                    //    }

                    //    else { dataValue = "!Error"; }

                    //    #endregion 

                    //    break;

                    default:

                        dataValue = "!Error";

                        break;

                } // end switch

            } // end try

            catch (Exception applicationException) {

                System.Diagnostics.Trace.WriteLine (Name + ".EvaluateDataBinding: " + applicationException.Message);

                System.Diagnostics.Trace.Flush ();

                dataValue = "!Error";

            }

            return dataValue;

        }

        #endregion

    }

}
