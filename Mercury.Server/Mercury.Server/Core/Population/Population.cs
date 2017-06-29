using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Population {

    [DataContract (Name = "Population")]
    public class Population : CoreConfigurationObject {

        #region Private Properties

        [DataMember (Name = "PopulationTypeId")]
        private Int64 populationTypeId;

        [DataMember (Name = "AllowProspective")]
        private Boolean allowProspective = false;

        [DataMember (Name = "InitialAnchorDate")]
        private Enumerations.PopulationInitialAnchorDate initialAnchorDate = Mercury.Server.Core.Population.Enumerations.PopulationInitialAnchorDate.ProcessDate;


        [DataMember (Name = "EnrollmentCriteria")]
        private List<PopulationCriteria.PopulationCriteriaEnrollment> enrollmentCriteria = new List<Mercury.Server.Core.Population.PopulationCriteria.PopulationCriteriaEnrollment> ();

        [DataMember (Name = "DemographicCriteria")]
        private List<PopulationCriteria.PopulationCriteriaDemographic> demographicCriteria = new List<Mercury.Server.Core.Population.PopulationCriteria.PopulationCriteriaDemographic> ();

        [DataMember (Name = "GeographicCriteria")]
        private List<PopulationCriteria.PopulationCriteriaGeographic> geographicCriteria = new List<Mercury.Server.Core.Population.PopulationCriteria.PopulationCriteriaGeographic> ();

        [DataMember (Name = "EventCriteria")]
        private List<PopulationCriteria.PopulationCriteriaEvent> eventCriteria = new List<Mercury.Server.Core.Population.PopulationCriteria.PopulationCriteriaEvent> ();

        [DataMember (Name = "Events")]
        private Dictionary<String, Core.Action.Action> events = new Dictionary<string, Mercury.Server.Core.Action.Action> ();

        [DataMember (Name = "ServiceEvents")]
        private List<PopulationEvents.PopulationServiceEvent> serviceEvents = new List<Mercury.Server.Core.Population.PopulationEvents.PopulationServiceEvent> ();

        [DataMember (Name = "TriggerEvents")]
        private List<PopulationEvents.PopulationTriggerEvent> triggerEvents = new List<Mercury.Server.Core.Population.PopulationEvents.PopulationTriggerEvent> ();

        [DataMember (Name = "ActivityEvents")]
        private List<PopulationEvents.PopulationActivityEvent> activityEvents = new List<Mercury.Server.Core.Population.PopulationEvents.PopulationActivityEvent> ();


        private Boolean processDebug = false;

        private Int64 processLogId = 0;

        private Int64 processStepId = 0;


        private PopulationType populationType = null;

        #endregion 


        #region Public Properties

        public Int64 PopulationTypeId { get { return populationTypeId; } set { populationTypeId = value; populationType = null; } }

        public Boolean AllowProspective { get { return allowProspective; } set { allowProspective = value; } }

        public Enumerations.PopulationInitialAnchorDate InitialAnchorDate { get { return initialAnchorDate; } set { initialAnchorDate = value; } }


        public List<PopulationCriteria.PopulationCriteriaEnrollment> EnrollmentCriteria { get { return enrollmentCriteria; } set { enrollmentCriteria = value; } }

        public List<PopulationCriteria.PopulationCriteriaDemographic> DemographicCriteria { get { return demographicCriteria; } set { demographicCriteria = value; } }

        public List<PopulationCriteria.PopulationCriteriaGeographic> GeographicCriteria { get { return geographicCriteria; } set { geographicCriteria = value; } }

        public List<PopulationCriteria.PopulationCriteriaEvent> EventCriteria { get { return eventCriteria; } set { eventCriteria = value; } }

        public Dictionary<String, Core.Action.Action> Events { get { return events; } set { events = value; } }

        public List<PopulationEvents.PopulationServiceEvent> ServiceEvents { get { return serviceEvents; } set { serviceEvents = value; } }

        public List<PopulationEvents.PopulationTriggerEvent> TriggerEvents { get { return triggerEvents; } set { triggerEvents = value; } }

        public List<PopulationEvents.PopulationActivityEvent> ActivityEvents { get { return activityEvents; } set { activityEvents = value; } }


        public override Application Application {
            
            set {
                
                base.Application = value;


                // PROPOGATE: SET ALL CHILD REFERENCES

                foreach (PopulationCriteria.PopulationCriteriaEnrollment currentCriteria in EnrollmentCriteria) { currentCriteria.Application = value; }

                foreach (PopulationCriteria.PopulationCriteriaDemographic currentCriteria in DemographicCriteria) { currentCriteria.Application = value; }

                foreach (PopulationCriteria.PopulationCriteriaEvent currentCriteria in EventCriteria) { currentCriteria.Application = value; }

                foreach (PopulationCriteria.PopulationCriteriaGeographic currentCriteria in GeographicCriteria) { currentCriteria.Application = value; }

                foreach (String eventName in events.Keys) { events[eventName].Application = value; }

                foreach (PopulationEvents.PopulationServiceEvent currentEvent in serviceEvents) { currentEvent.Application = value; }

                foreach (PopulationEvents.PopulationTriggerEvent currentEvent in triggerEvents) { currentEvent.Application = value; }

                foreach (PopulationEvents.PopulationActivityEvent currentEvent in activityEvents) { currentEvent.Application = value; }
    
            }

        }

        public PopulationType PopulationType {

            get {

                if (populationType != null) { return populationType; }

                if (application == null) { return null; }

                populationType = application.PopulationTypeGet (populationTypeId);

                return populationType;

            }

        }


        public Int64 OnBeforeMembershipAddWorkflowId {

            get {

                Int64 workflowId = 0;

                if (events.ContainsKey ("OnBeforeMembershipAdd")) {

                    if (events["OnBeforeMembershipAdd"].ActionParameters.ContainsKey ("Workflow")) {

                        Int64.TryParse (events["OnBeforeMembershipAdd"].ActionParameters["Workflow"].Value, out workflowId);

                    }

                }

                return workflowId;

            }
            
            set {

                if (events.ContainsKey ("OnBeforeMembershipAdd")) {

                    if (events["OnBeforeMembershipAdd"].ActionParameters.ContainsKey ("Workflow")) {

                        events["OnBeforeMembershipAdd"].ActionParameters["Workflow"].Value = value.ToString ();

                    }

                }

            }

        }

        public Server.Core.Action.Action OnBeforeMembershipAddEventAction {

            get {

                Server.Core.Action.Action action = application.ActionById (1);

                action.ActionParameters["Workflow"].Value = OnBeforeMembershipAddWorkflowId.ToString ();

                action.AddParameter ("MemberId", Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.Id, true);

                action.AddParameter ("PopulationId", Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.Id, true);

                action.AddParameter ("IdentifyingEventMemberServiceId", Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.Id, true);

                return action;

            }

        }

        public Int64 OnBeforeMembershipTerminateWorkflowId {

            get {

                Int64 workflowId = 0;

                if (events.ContainsKey ("OnBeforeMembershipTerminate")) {

                    if (events["OnBeforeMembershipTerminate"].ActionParameters.ContainsKey ("Workflow")) {

                        Int64.TryParse (events["OnBeforeMembershipTerminate"].ActionParameters["Workflow"].Value, out workflowId);

                    }

                }

                return workflowId;

            }

            set {

                if (events.ContainsKey ("OnBeforeMembershipTerminate")) {

                    if (events["OnBeforeMembershipTerminate"].ActionParameters.ContainsKey ("Workflow")) {

                        events["OnBeforeMembershipTerminate"].ActionParameters["Workflow"].Value = value.ToString ();

                    }

                }

            }

        }

        public Server.Core.Action.Action OnBeforeMembershipTerminateEventAction {

            get {

                Server.Core.Action.Action action = application.ActionById (1);

                action.ActionParameters["Workflow"].Value = OnBeforeMembershipTerminateWorkflowId.ToString ();

                action.AddParameter ("PopulationMembershipId", Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.Id, true);

                action.AddParameter ("PopulationId", Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.Id, true);

                action.AddParameter ("TerminatingEventMemberServiceId", Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.Id, true);

                return action;

            }

        }


        public Boolean HasEventCriteriaExcludedService {

            get {

                Boolean hasExcludedService = false;

                foreach (PopulationCriteria.PopulationCriteriaEvent currentCriteria in eventCriteria) {

                    if (currentCriteria.EventType == Mercury.Server.Core.Population.Enumerations.PopulationCriteriaEventType.Exclusion) {

                        hasExcludedService = true;

                        break;

                    }

                }

                return hasExcludedService;

            }

        }

        public Boolean HasEventCriteriaIdentifyingService {

            get {

                Boolean hasIdentifyingService = false;

                foreach (PopulationCriteria.PopulationCriteriaEvent currentCriteria in eventCriteria) {

                    if (currentCriteria.EventType == Mercury.Server.Core.Population.Enumerations.PopulationCriteriaEventType.Identifying) {

                        hasIdentifyingService = true;

                        break;

                    }

                }

                return hasIdentifyingService;

            }

        }

        public Boolean HasEventCriteriaTerminatingService {

            get {

                Boolean hasTerminatingService = false;

                foreach (PopulationCriteria.PopulationCriteriaEvent currentCriteria in eventCriteria) {

                    if (currentCriteria.EventType == Mercury.Server.Core.Population.Enumerations.PopulationCriteriaEventType.Terminating) {

                        hasTerminatingService = true;

                        break;

                    }

                }

                return hasTerminatingService;

            }

        }

        #endregion


        #region Constructors

        private void PopulationBaseConstructor (Application applicationReference) {

            base.BaseConstructor (applicationReference);

            events.Clear ();


            events.Add ("OnBeforeMembershipAdd", application.ActionById ((Int64) Core.Action.Enumerations.StandardActions.Workflow));

            events.Add ("OnMembershipAdd", new Mercury.Server.Core.Action.Action (application));

            events.Add ("OnBeforeMembershipTerminate", application.ActionById ((Int64) Core.Action.Enumerations.StandardActions.Workflow));

            events.Add ("OnMembershipTerminate", new Mercury.Server.Core.Action.Action (application));

            return;

        }

        public Population (Application applicationReference) { PopulationBaseConstructor (applicationReference); return; }

        public Population (Application applicationReference, Int64 forPopulationId) {

            PopulationBaseConstructor (applicationReference);

            BaseConstructor (applicationReference, forPopulationId);

            return;

        }

        #endregion

        
        #region XML Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];


            #region Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "PopulationTypeId", populationTypeId.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "PopulationTypeName", ((PopulationType != null) ? PopulationType.Name : String.Empty));

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "AllowProspective", allowProspective.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "InitialAnchorDateInt32", ((Int32)initialAnchorDate).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "InitialAnchorDate", initialAnchorDate.ToString ());
            
            #endregion

            
            #region Criteria

            System.Xml.XmlNode criteriaNode = document.CreateElement ("PopulationCriteria");

            document.LastChild.AppendChild (criteriaNode);

            foreach (PopulationCriteria.PopulationCriteriaEnrollment currentEnrollmentCriteria in enrollmentCriteria) {

                criteriaNode.AppendChild (document.ImportNode (currentEnrollmentCriteria.XmlSerialize ().LastChild, true));

            }

            foreach (PopulationCriteria.PopulationCriteriaDemographic currentDemographicCriteria in demographicCriteria) {

                criteriaNode.AppendChild (document.ImportNode (currentDemographicCriteria.XmlSerialize ().LastChild, true));

            }

            foreach (PopulationCriteria.PopulationCriteriaEvent currentEventCriteria in eventCriteria) {

                criteriaNode.AppendChild (document.ImportNode (currentEventCriteria.XmlSerialize ().LastChild, true));

            }

            foreach (PopulationCriteria.PopulationCriteriaGeographic currentGeographicCriteria in geographicCriteria) {

                criteriaNode.AppendChild (document.ImportNode (currentGeographicCriteria.XmlSerialize ().LastChild, true));

            }

            #endregion 


            #region Population Events

            System.Xml.XmlNode populationEventNode = document.CreateElement ("PopulationEvents");

            document.LastChild.AppendChild (populationEventNode);

            foreach (String currentPopulationEventName in events.Keys) {

                if (events[currentPopulationEventName] != null) {

                    System.Xml.XmlNode populationEventActionNode = document.CreateElement (currentPopulationEventName);

                    populationEventActionNode.AppendChild (document.ImportNode (events[currentPopulationEventName].XmlSerialize ().LastChild, true)); 

                    populationEventNode.AppendChild (populationEventActionNode);

                }

            }

            #endregion 

            
            #region Service Events

            System.Xml.XmlNode serviceEventNode = document.CreateElement ("ServiceEvents");

            document.LastChild.AppendChild (serviceEventNode);


            foreach (PopulationEvents.PopulationServiceEvent currentServiceEvent in serviceEvents) {

                serviceEventNode.AppendChild (document.ImportNode (currentServiceEvent.XmlSerialize ().LastChild, true));

            }
            
            #endregion 

            
            #region Trigger Events

            System.Xml.XmlNode triggerEventNode = document.CreateElement ("TriggerEvents");

            document.LastChild.AppendChild (triggerEventNode);


            foreach (PopulationEvents.PopulationTriggerEvent currentTriggerEvent in triggerEvents) {

                triggerEventNode.AppendChild (document.ImportNode (currentTriggerEvent.XmlSerialize ().LastChild, true));

            }

            #endregion 

            
            #region Activity Events

            System.Xml.XmlNode activityEventNode = document.CreateElement ("ActivityEvents");

            document.LastChild.AppendChild (activityEventNode);


            foreach (PopulationEvents.PopulationActivityEvent currentActivityEvent in activityEvents) {

                activityEventNode.AppendChild (document.ImportNode (currentActivityEvent.XmlSerialize ().LastChild, true));

            }

            #endregion 


            #region Dependencies Nodes

            System.Xml.XmlNode dependenciesNode = document.CreateElement ("Dependencies");

            document.LastChild.InsertBefore (dependenciesNode, propertiesNode);

            if (PopulationType != null) { dependenciesNode.AppendChild (document.ImportNode (PopulationType.XmlSerialize ().LastChild, true)); }

            #endregion 


            return document;

        }

        
        public override List<ImportExport.Result> XmlImport (System.Xml.XmlNode objectNode) {

            List<ImportExport.Result> response = base.XmlImport (objectNode);

            try {

                foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {

                    switch (currentNode.Name) {

                        case "Dependencies":

                            #region Dependencies

                            foreach (System.Xml.XmlNode currentDependencyNode in currentNode.ChildNodes) {

                                switch (currentDependencyNode.Name) {

                                    case "PopulationType":

                                        PopulationType populationType = application.PopulationTypeGet (currentDependencyNode.Attributes[Name].Value);

                                        if (populationType == null) {

                                            populationType = new Core.Population.PopulationType (application);

                                            response.AddRange (populationType.XmlImport (currentDependencyNode));

                                        }

                                        break;

                                }

                            }

                            #endregion 

                            break;

                        case "Properties":

                            #region Properties

                            foreach (System.Xml.XmlNode currentPropertyNode in currentNode.ChildNodes) {

                                switch (currentPropertyNode.Attributes["Name"].Value) {

                                    case "PopulationTypeName":

                                        populationTypeId = application.PopulationTypeGetIdByName (currentPropertyNode.InnerText);

                                        if (populationTypeId == 0) { throw new ApplicationException ("Unable to retreive Population Type: " + currentPropertyNode.InnerText); }

                                        break;

                                    case "AllowProspective": AllowProspective = Convert.ToBoolean (currentPropertyNode.InnerText); break;

                                    case "InitialAnchorDateInt32": InitialAnchorDate = (Enumerations.PopulationInitialAnchorDate)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                                }

                            }

                            #endregion 

                            break;

                        case "PopulationCriteria":

                            #region Population Criteria

                            foreach (System.Xml.XmlNode currentCriteriaNode in currentNode.ChildNodes) {

                                switch (currentCriteriaNode.Name) {

                                    case "PopulationCriteriaEnrollment":

                                        PopulationCriteria.PopulationCriteriaEnrollment populationCriteriaEnrollment = new PopulationCriteria.PopulationCriteriaEnrollment (application);

                                        response.AddRange (populationCriteriaEnrollment.XmlImport (currentCriteriaNode));

                                        enrollmentCriteria.Add (populationCriteriaEnrollment);

                                        break;

                                    case "PopulationCriteriaDemographic":

                                        PopulationCriteria.PopulationCriteriaDemographic populationCriteriaDemographic = new PopulationCriteria.PopulationCriteriaDemographic (application);

                                        response.AddRange (populationCriteriaDemographic.XmlImport (currentCriteriaNode));

                                        demographicCriteria.Add (populationCriteriaDemographic);

                                        break;

                                    case "PopulationCriteriaEvent":

                                        PopulationCriteria.PopulationCriteriaEvent populationCriteriaEvent = new PopulationCriteria.PopulationCriteriaEvent (application);

                                        response.AddRange (populationCriteriaEvent.XmlImport (currentCriteriaNode));

                                        eventCriteria.Add (populationCriteriaEvent);

                                        break;

                                    case "PopulationCriteriaGeographic":

                                        PopulationCriteria.PopulationCriteriaGeographic populationCriteriaGeographic = new PopulationCriteria.PopulationCriteriaGeographic (application);

                                        response.AddRange (populationCriteriaGeographic.XmlImport (currentCriteriaNode));

                                        geographicCriteria.Add (populationCriteriaGeographic);

                                        break;

                                }

                            }

                            #endregion

                            break;

                        case "PopulationEvents":

                            #region Population Events

                            foreach (System.Xml.XmlNode currentEventNode in currentNode.ChildNodes) {

                                String eventName = currentEventNode.Name;

                                Core.Action.Action eventAction = new Action.Action (application);

                                response.AddRange (eventAction.XmlImport (currentEventNode.FirstChild));

                                if (events.ContainsKey (eventName)) { events.Remove (eventName); }

                                events.Add (eventName, eventAction);

                            }

                            #endregion   

                            break;

                        case "ServiceEvents":

                            #region Population Service Events

                            foreach (System.Xml.XmlNode currentServiceEventNode in currentNode.ChildNodes) {

                                PopulationEvents.PopulationServiceEvent populationServiceEvent = new PopulationEvents.PopulationServiceEvent (application);

                                response.AddRange (populationServiceEvent.XmlImport (currentServiceEventNode));

                                serviceEvents.Add (populationServiceEvent);

                            }

                            #endregion 

                            break;

                        case "TriggerEvents":

                            #region Population Trigger Events

                            foreach (System.Xml.XmlNode currentTriggerEventNode in currentNode.ChildNodes) {

                                PopulationEvents.PopulationTriggerEvent populationTriggerEvent = new PopulationEvents.PopulationTriggerEvent (application);

                                response.AddRange (populationTriggerEvent.XmlImport (currentTriggerEventNode));

                                triggerEvents.Add (populationTriggerEvent);

                            }

                            #endregion

                            break;

                        case "ActivityEvents":

                            #region Population Activity Events

                            foreach (System.Xml.XmlNode currentActivityEventNode in currentNode.ChildNodes) {

                                PopulationEvents.PopulationActivityEvent populationActivityEvent = new PopulationEvents.PopulationActivityEvent (application);

                                response.AddRange (populationActivityEvent.XmlImport (currentActivityEventNode));

                                activityEvents.Add (populationActivityEvent);

                            }

                            #endregion

                            break;

                    } // switch (currentNode.Name) {

                } // foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {

                // SAVE IMPORTED OBJECT 

                if (!Save ()) { throw new ApplicationException ("Unable to save " + ObjectType + ": " + Name + "."); }

            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return response;

        }

        //                    case "Criteria":

        //                        #region Population Criteria

        //                        foreach (System.Xml.XmlNode currentCriteriaNode in currentParentNode.ChildNodes) {

        //                            Boolean validCriteria = true;

        //                            String criteriaType = currentCriteriaNode.Attributes["Type"].InnerText;

        //                            switch (criteriaType) {

        //                                case "Enrollment":

        //                                    #region Enrollment Criteria

        //                                    PopulationCriteria.EnrollmentCriteria importEnrollmentCriteria = new Mercury.Server.Core.Population.PopulationCriteria.EnrollmentCriteria ();

        //                                    importEnrollmentCriteria.PopulationId = id;

        //                                    String insurerName = String.Empty;

        //                                    String programName = String.Empty;

        //                                    String benefitPlanName = String.Empty;

        //                                    foreach (System.Xml.XmlNode currentProperty in currentCriteriaNode.ChildNodes[0].ChildNodes) {

        //                                        switch (currentProperty.Attributes["Name"].InnerText) {

        //                                            case "InsurerId": insurerName = currentProperty.InnerText; break;

        //                                            case "ProgramId": programName = currentProperty.InnerText; break;

        //                                            case "BenefitPlanId": benefitPlanName = currentProperty.InnerText; break;

        //                                        }

        //                                    }


        //                                    importEnrollmentCriteria.InsurerId = base.application.ObjectGetIdByName ("dal.Insurer", insurerName);

        //                                    importEnrollmentCriteria.ProgramId = base.application.ProgramReferenceIdByName (programName);

        //                                    importEnrollmentCriteria.BenefitPlanId = base.application.BenefitPlanReferenceIdByName (benefitPlanName);


        //                                    if ((importEnrollmentCriteria.InsurerId == 0) && (!String.IsNullOrEmpty (insurerName))) { validCriteria = false; }

        //                                    if ((importEnrollmentCriteria.ProgramId == 0) && (!String.IsNullOrEmpty (programName))) { validCriteria = false; }

        //                                    if ((importEnrollmentCriteria.BenefitPlanId == 0) && (!String.IsNullOrEmpty (benefitPlanName))) { validCriteria = false; }


        //                                    if (!validCriteria) { throw new ApplicationException ("Unable to import Population Enrollment Criteria [" + insurerName + ", " + programName + "]"); }


        //                                    enrollmentCriteria.Add (importEnrollmentCriteria);

        //                                    #endregion

        //                                    break;

        //                                case "Demographic":

        //                                    #region Demographic Criteria

        //                                    PopulationCriteria.DemographicCriteria importDemographicCriteria = new Mercury.Server.Core.Population.PopulationCriteria.DemographicCriteria ();

        //                                    importDemographicCriteria.PopulationId = id;

        //                                    String ethnicityName = String.Empty;


        //                                    foreach (System.Xml.XmlNode currentProperty in currentCriteriaNode.ChildNodes[0].ChildNodes) {

        //                                        switch (currentProperty.Attributes["Name"].InnerText) {

        //                                            case "Gender": importDemographicCriteria.Gender = (Mercury.Server.Core.Enumerations.Gender) Convert.ToInt32 (currentProperty.InnerText); break;

        //                                            case "UseAgeCriteria": importDemographicCriteria.UseAgeCriteria = Convert.ToBoolean (currentProperty.InnerText); break;

        //                                            case "AgeMinimum": importDemographicCriteria.AgeMinimum = Convert.ToInt32 (currentProperty.InnerText); break;

        //                                            case "AgeMaximum": importDemographicCriteria.AgeMaximum = Convert.ToInt32 (currentProperty.InnerText); break;

        //                                            case "EthnicityId": ethnicityName = currentProperty.InnerText; break;

        //                                        }

        //                                    }


        //                                    importDemographicCriteria.EthnicityId = base.application.ObjectGetIdByName ("dal.Ethnicity", ethnicityName);

        //                                    if ((importDemographicCriteria.EthnicityId == 0) && (!string.IsNullOrEmpty (ethnicityName))) { validCriteria = false; }


        //                                    if (!validCriteria) { throw new ApplicationException ("Unable to import Population Demographic Criteria [" + ethnicityName + "]"); }


        //                                    demographicCriteria.Add (importDemographicCriteria);

        //                                    #endregion

        //                                    break;

        //                                case "Geographic":

        //                                    #region Geographic Criteria

        //                                    PopulationCriteria.GeographicCriteria importGeographicCriteria = new Mercury.Server.Core.Population.PopulationCriteria.GeographicCriteria ();

        //                                    importGeographicCriteria.PopulationId = id;


        //                                    foreach (System.Xml.XmlNode currentProperty in currentCriteriaNode.ChildNodes[0].ChildNodes) {

        //                                        switch (currentProperty.Attributes["Name"].InnerText) {

        //                                            case "State": importGeographicCriteria.State = currentProperty.InnerText; break;

        //                                            case "City": importGeographicCriteria.City = currentProperty.InnerText; break;

        //                                            case "County": importGeographicCriteria.County = currentProperty.InnerText; break;

        //                                            case "ZipCode": importGeographicCriteria.ZipCode = currentProperty.InnerText; break;

        //                                        }

        //                                    }


        //                                    geographicCriteria.Add (importGeographicCriteria);

        //                                    #endregion

        //                                    break;

        //                                case "Event":

        //                                    #region Event Criteria

        //                                    PopulationCriteria.EventCriteria importEventCriteria = new Mercury.Server.Core.Population.PopulationCriteria.EventCriteria ();

        //                                    importEventCriteria.PopulationId = id;


        //                                    foreach (System.Xml.XmlNode currentProperty in currentCriteriaNode.ChildNodes[0].ChildNodes) {

        //                                        switch (currentProperty.Attributes["Name"].InnerText) {

        //                                            case "EventType": importEventCriteria.EventType = (Mercury.Server.Core.Population.Enumerations.EventType) Convert.ToInt32 (currentProperty.InnerText); break;

        //                                            case "Service":

        //                                                System.Xml.XmlNode eventServiceNode = currentProperty.ChildNodes[0];

        //                                                String eventServiceName = eventServiceNode.Attributes["Name"].InnerText;

        //                                                Core.MedicalServices.Service medicalService = base.application.MedicalServiceGet (eventServiceName);

        //                                                if (medicalService != null) {

        //                                                    importEventCriteria.ServiceId = medicalService.Id;

        //                                                }

        //                                                else {

        //                                                    Core.MedicalServices.Enumerations.MedicalServiceType eventServiceType = (Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType) Convert.ToInt32 (eventServiceNode.Attributes["ServiceType"].InnerText);

        //                                                    switch (eventServiceType) {

        //                                                        case Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType.Singleton:

        //                                                            Core.MedicalServices.ServiceSingleton singleton = new Core.MedicalServices.ServiceSingleton (base.application);

        //                                                            response.AddRange (singleton.XmlImport (eventServiceNode));

        //                                                            importEventCriteria.ServiceId = singleton.ServiceId;

        //                                                            break;

        //                                                        case Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType.Set:

        //                                                            Core.MedicalServices.ServiceSet serviceSet = new Core.MedicalServices.ServiceSet (base.application);

        //                                                            response.AddRange (serviceSet.XmlImport (eventServiceNode));

        //                                                            importEventCriteria.ServiceId = serviceSet.ServiceId;

        //                                                            break;

        //                                                    }

        //                                                }

        //                                                if (importEventCriteria.ServiceId == 0) { validCriteria = false; }

        //                                                break;

        //                                        }

        //                                    }


        //                                    if (!validCriteria) { throw new ApplicationException ("Unable to import Population Event Criteria."); }


        //                                    eventCriteria.Add (importEventCriteria);


        //                                    #endregion

        //                                    break;

        //                            }

        //                        }

        //                        #endregion

        //                        break;

        //                    case "Events":

        //                        #region Population Events

        //                        System.Xml.XmlNode populationEventsNode = currentParentNode;

        //                        foreach (System.Xml.XmlNode currentEvent in populationEventsNode.ChildNodes) {

        //                            String populationEventName = currentEvent.Attributes["Name"].InnerText;

        //                            System.Xml.XmlNode eventActionNode = currentEvent.ChildNodes[0];

        //                            Int32 populationEventActionId = Convert.ToInt32 (eventActionNode.Attributes["ActionId"].InnerText);

        //                            String populationEventActionName = eventActionNode.Attributes["Name"].InnerText;

        //                            if (populationEventActionId > 0) {

        //                                if (events.ContainsKey (populationEventName)) {

        //                                    events[populationEventName] = new Mercury.Server.Core.Action.Action (base.application, populationEventActionId, populationEventActionName);

        //                                    response.AddRange (events[populationEventName].XmlImport (eventActionNode));

        //                                }

        //                            }

        //                        }

        //                        #endregion

        //                        break;

        //                    case "ServiceEvents":

        //                        #region Population Service Events

        //                        System.Xml.XmlNode serviceEventsNode = currentParentNode;

        //                        foreach (System.Xml.XmlNode currentServiceEvent in serviceEventsNode.ChildNodes) {

        //                            PopulationEvents.ServiceEvent importServiceEvent = new Mercury.Server.Core.Population.PopulationEvents.ServiceEvent (base.application);

        //                            importServiceEvent.PopulationId = id;

        //                            response.AddRange (importServiceEvent.XmlImport (currentServiceEvent));

        //                            serviceEvents.Add (importServiceEvent);

        //                        }

        //                        #endregion

        //                        break;

        //                    case "TriggerEvents":

        //                        #region Trigger Events

        //                        System.Xml.XmlNode triggerEventsNode = currentParentNode;

        //                        foreach (System.Xml.XmlNode currentTriggerEvent in triggerEventsNode.ChildNodes) {

        //                            PopulationEvents.TriggerEvent importTriggerEvent = new Mercury.Server.Core.Population.PopulationEvents.TriggerEvent (base.application);

        //                            importTriggerEvent.PopulationId = id;

        //                            response.AddRange (importTriggerEvent.XmlImport (currentTriggerEvent));

        //                            triggerEvents.Add (importTriggerEvent);

        //                        }

        //                        #endregion

        //                        break;

        //                    case "ActivityEvents":

        //                        #region Activity Events

        //                        System.Xml.XmlNode activityEventsNode = currentParentNode;

        //                        foreach (System.Xml.XmlNode currentActivityEvent in activityEventsNode.ChildNodes) {

        //                            PopulationEvents.ActivityEvent importActivityEvent = new Mercury.Server.Core.Population.PopulationEvents.ActivityEvent (base.application);

        //                            importActivityEvent.PopulationId = id;

        //                            response.AddRange (importActivityEvent.XmlImport (currentActivityEvent));

        //                            activityEvents.Add (importActivityEvent);

        //                        }

        //                        #endregion

        //                        break;

        //                }

        //            }


        //            importResponse.Success = this.Save (base.application);

        //            importResponse.Id = Id;

        //            if (!importResponse.Success) { importResponse.SetException (base.application.LastException); }


        //        }

        //        catch (Exception importException) {

        //            importResponse.SetException (importException);

        //        }


        //    }

        //    else { importResponse.SetException (new ApplicationException ("Invalid Object Type Parsed as Population.")); }


        //    response.Add (importResponse);

        //    return response;

        //}

        #endregion


        #region Data Functions

        public override Boolean Load (Int64 forId) {

            Boolean success = base.Load (forId);

            String selectStatement;

            System.Data.DataTable criteriaTable;


            if (success) {

                selectStatement = "SELECT * FROM dbo.PopulationCriteriaEnrollment WHERE PopulationId = " + forId;

                criteriaTable = application.EnvironmentDatabase.SelectDataTable (selectStatement, 0);

                foreach (System.Data.DataRow currentRow in criteriaTable.Rows) {

                    PopulationCriteria.PopulationCriteriaEnrollment criteria = new Mercury.Server.Core.Population.PopulationCriteria.PopulationCriteriaEnrollment (application);

                    criteria.MapDataFields (currentRow);

                    enrollmentCriteria.Add (criteria);

                }

                selectStatement = "SELECT * FROM dbo.PopulationCriteriaDemographic WHERE PopulationId = " + forId;

                criteriaTable = application.EnvironmentDatabase.SelectDataTable (selectStatement, 0);

                foreach (System.Data.DataRow currentRow in criteriaTable.Rows) {

                    PopulationCriteria.PopulationCriteriaDemographic criteria = new Mercury.Server.Core.Population.PopulationCriteria.PopulationCriteriaDemographic (application);

                    criteria.MapDataFields (currentRow);

                    demographicCriteria.Add (criteria);

                }

                selectStatement = "SELECT * FROM dbo.PopulationCriteriaGeographic WHERE PopulationId = " + forId;

                criteriaTable = application.EnvironmentDatabase.SelectDataTable (selectStatement, 0);

                foreach (System.Data.DataRow currentRow in criteriaTable.Rows) {

                    PopulationCriteria.PopulationCriteriaGeographic criteria = new Mercury.Server.Core.Population.PopulationCriteria.PopulationCriteriaGeographic (application);

                    criteria.MapDataFields (currentRow);

                    geographicCriteria.Add (criteria);

                }

                selectStatement = "SELECT * FROM dbo.PopulationCriteriaEvent WHERE PopulationId = " + forId;

                criteriaTable = application.EnvironmentDatabase.SelectDataTable (selectStatement, 0);

                foreach (System.Data.DataRow currentRow in criteriaTable.Rows) {

                    PopulationCriteria.PopulationCriteriaEvent criteria = new Mercury.Server.Core.Population.PopulationCriteria.PopulationCriteriaEvent (application);

                    criteria.MapDataFields (currentRow);

                    eventCriteria.Add (criteria);

                }

                selectStatement = "SELECT * FROM dbo.PopulationServiceEvent WHERE PopulationId = " + forId;

                criteriaTable = application.EnvironmentDatabase.SelectDataTable (selectStatement, 0);

                foreach (System.Data.DataRow currentRow in criteriaTable.Rows) {

                    PopulationEvents.PopulationServiceEvent serviceEvent = new Mercury.Server.Core.Population.PopulationEvents.PopulationServiceEvent (base.application);

                    serviceEvent.Population = this;

                    serviceEvent.MapDataFields (currentRow);

                    serviceEvents.Add (serviceEvent);

                }

                selectStatement = "SELECT * FROM dbo.PopulationTriggerEvent WHERE PopulationId = " + forId;

                criteriaTable = application.EnvironmentDatabase.SelectDataTable (selectStatement, 0);

                foreach (System.Data.DataRow currentRow in criteriaTable.Rows) {

                    PopulationEvents.PopulationTriggerEvent triggerEvent = new Mercury.Server.Core.Population.PopulationEvents.PopulationTriggerEvent (base.application);

                    triggerEvent.Population = this;

                    triggerEvent.MapDataFields (currentRow);

                    triggerEvents.Add (triggerEvent);

                }


                selectStatement = "SELECT * FROM dbo.PopulationActivityEvent WHERE PopulationId = " + forId;

                criteriaTable = application.EnvironmentDatabase.SelectDataTable (selectStatement, 0);

                foreach (System.Data.DataRow currentRow in criteriaTable.Rows) {

                    PopulationEvents.PopulationActivityEvent activityEvent = new Mercury.Server.Core.Population.PopulationEvents.PopulationActivityEvent (base.application);

                    activityEvent.Population = this;

                    activityEvent.MapDataFields (currentRow);

                    activityEvents.Add (activityEvent);

                }


            }

            return success;

        }

        override public void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            populationTypeId = (Int64) currentRow["PopulationTypeId"];

            allowProspective = (Boolean) currentRow ["AllowProspective"];

            initialAnchorDate = (Mercury.Server.Core.Population.Enumerations.PopulationInitialAnchorDate) (Int32) currentRow ["InitialAnchorDate"];

            
            try {

                events["OnBeforeMembershipAdd"].ActionParameters["Workflow"].Value = (IdFromSql (currentRow, "OnBeforeMembershipAddWorkflowId")).ToString ();

                events["OnBeforeMembershipAdd"].ActionParameters["Workflow"].ValueDescription = application.CoreObjectGetNameById ("Workflow", Convert.ToInt64 (events["OnBeforeMembershipAdd"].ActionParameters["Workflow"].Value));

            }

            catch { /* DO NOTHING */ }



            try { events["OnMembershipAdd"].MapDataFields ("OnMembershipAdd", currentRow); }

            catch { /* DO NOTHING */ }


            try {

                events["OnBeforeMembershipTerminate"].ActionParameters["Workflow"].Value = (IdFromSql (currentRow, "OnBeforeMembershipTerminateWorkflowId")).ToString ();

                events["OnBeforeMembershipTerminate"].ActionParameters["Workflow"].ValueDescription = application.CoreObjectGetNameById ("Workflow", Convert.ToInt64 (events["OnBeforeMembershipTerminate"].ActionParameters["Workflow"].Value));

            }

            catch { /* DO NOTHING */ }

            try { events["OnMembershipTerminate"].MapDataFields ("OnMembershipTerminate", currentRow); }

            catch { /* DO NOTHING */ }


            createAccountInfo.MapDataFields (currentRow, "Create");

            modifiedAccountInfo.MapDataFields (currentRow, "Modified");

            return;

        }

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement;
            
            String criteriaIds;

            if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.PopulationManage)) { throw new ApplicationException ("Permission Denied"); }


            modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);
            

            try {

                application.EnvironmentDatabase.BeginTransaction ();

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dbo.Population_InsertUpdate ");

                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append ("'" + NameSql + "', ");

                sqlStatement.Append ("'" + DescriptionSql + "', ");

                sqlStatement.Append (populationTypeId.ToString () + ", ");

                sqlStatement.Append (Convert.ToInt32 (allowProspective).ToString () + ", ");

                sqlStatement.Append (((Int32) initialAnchorDate).ToString () + ", ");

                sqlStatement.Append (Convert.ToInt32 (Enabled).ToString () + ", ");

                sqlStatement.Append (Convert.ToInt32 (Visible).ToString () + ", ");


                sqlStatement.Append (IdSqlAllowNull (OnBeforeMembershipAddWorkflowId) + ", ");

                sqlStatement.Append (events["OnMembershipAdd"].Id.ToString () + ", ");

                sqlStatement.Append ("'" + events["OnMembershipAdd"].ActionParametersXmlSqlParsedString + "', ");

                sqlStatement.Append ("'" + events["OnMembershipAdd"].Description + "', ");


                sqlStatement.Append (IdSqlAllowNull (OnBeforeMembershipTerminateWorkflowId) + ", ");

                sqlStatement.Append (events["OnMembershipTerminate"].Id.ToString () + ", ");

                sqlStatement.Append ("'" + events["OnMembershipTerminate"].ActionParametersXmlSqlParsedString + "', ");

                sqlStatement.Append ("'" + events["OnMembershipTerminate"].Description + "', ");

                
                sqlStatement.Append ("'" + ExtendedPropertiesSql + "', ");
                
                sqlStatement.Append ("'" + modifiedAccountInfo.SecurityAuthorityNameSql + "', '" + modifiedAccountInfo.UserAccountIdSql + "', '" + modifiedAccountInfo.UserAccountNameSql + "'");


                success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }


                SetIdentity ();


                #region Enrollment Criteria

                if (success) {

                    criteriaIds = String.Empty;

                    foreach (PopulationCriteria.PopulationCriteriaEnrollment currentCriteria in enrollmentCriteria) {

                        currentCriteria.PopulationId = id;

                        success = success && currentCriteria.Save (application);

                        criteriaIds = criteriaIds + currentCriteria.Id.ToString () + ",";

                        if (!success) { break; }

                    }

                    if (success) {

                        sqlStatement = new StringBuilder ("DELETE FROM PopulationCriteriaEnrollment WHERE PopulationId = " + id.ToString ());

                        if (criteriaIds.Length != 0) {

                            criteriaIds = criteriaIds.Substring (0, criteriaIds.Length - 1);

                            sqlStatement.Append (" AND PopulationCriteriaEnrollmentId NOT IN (" + criteriaIds + ")");

                        }

                        success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                    }

                }

                #endregion


                #region Demographic Criteria

                if (success) {

                    criteriaIds = String.Empty;

                    foreach (PopulationCriteria.PopulationCriteriaDemographic currentCriteria in demographicCriteria) {

                        currentCriteria.PopulationId = id;

                        success = success && currentCriteria.Save (application);

                        criteriaIds = criteriaIds + currentCriteria.Id.ToString () + ",";

                        if (!success) { break; }

                    }

                    if (success) {

                        sqlStatement = new StringBuilder ("DELETE FROM PopulationCriteriaDemographic WHERE PopulationId = " + id.ToString ());

                        if (criteriaIds.Length != 0) {

                            criteriaIds = criteriaIds.Substring (0, criteriaIds.Length - 1);

                            sqlStatement.Append (" AND PopulationCriteriaDemographicId NOT IN (" + criteriaIds + ")");

                        }

                        success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                    }

                }

                #endregion


                #region Geographic Criteria

                if (success) {

                    criteriaIds = String.Empty;

                    foreach (PopulationCriteria.PopulationCriteriaGeographic currentCriteria in geographicCriteria) {

                        currentCriteria.PopulationId = id;

                        success = success && currentCriteria.Save (application);

                        criteriaIds = criteriaIds + currentCriteria.Id.ToString () + ",";

                        if (!success) { break; }

                    }

                    if (success) {

                        sqlStatement = new StringBuilder ("DELETE FROM PopulationCriteriaGeographic WHERE PopulationId = " + id.ToString ());

                        if (criteriaIds.Length != 0) {

                            criteriaIds = criteriaIds.Substring (0, criteriaIds.Length - 1);

                            sqlStatement.Append (" AND PopulationCriteriaGeographicId NOT IN (" + criteriaIds + ")");

                        }

                        success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                    }

                }

                #endregion


                #region Event Criteria

                if (success) {

                    criteriaIds = String.Empty;

                    foreach (PopulationCriteria.PopulationCriteriaEvent currentCriteria in eventCriteria) {

                        currentCriteria.PopulationId = id;

                        success = success && currentCriteria.Save (application);

                        criteriaIds = criteriaIds + currentCriteria.Id.ToString () + ",";

                        if (!success) { break; }

                    }

                    if (success) {

                        sqlStatement = new StringBuilder ("DELETE FROM PopulationCriteriaEvent WHERE PopulationId = " + id.ToString ());

                        if (criteriaIds.Length != 0) {

                            criteriaIds = criteriaIds.Substring (0, criteriaIds.Length - 1);

                            sqlStatement.Append (" AND PopulationCriteriaEventId NOT IN (" + criteriaIds + ")");

                        }

                        success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                    }

                }

                #endregion


                #region Service Event

                if (success) {

                    String serviceEventIds = String.Empty;

                    foreach (PopulationEvents.PopulationServiceEvent currentEvent in serviceEvents) {

                        currentEvent.PopulationId = id;

                        success = success && currentEvent.Save (application);

                        serviceEventIds = serviceEventIds + currentEvent.Id.ToString () + ",";

                        if (!success) { break; }

                    }

                    if (success) {

                        sqlStatement = new StringBuilder ("DELETE FROM PopulationServiceEvent WHERE PopulationId = " + id.ToString ());

                        if (serviceEventIds.Length != 0) {

                            serviceEventIds = serviceEventIds.Substring (0, serviceEventIds.Length - 1);

                            sqlStatement.Append (" AND PopulationServiceEventId NOT IN (" + serviceEventIds + ")");

                        }

                        success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                    }

                }

                #endregion


                #region Trigger Event

                if (success) {

                    String triggerEventIds = String.Empty;

                    foreach (PopulationEvents.PopulationTriggerEvent currentEvent in triggerEvents) {

                        currentEvent.PopulationId = id;

                        success = success && currentEvent.Save ();

                        triggerEventIds = triggerEventIds + currentEvent.Id.ToString () + ",";

                        if (!success) { break; }

                    }

                    if (success) {

                        sqlStatement = new StringBuilder ("DELETE FROM PopulationTriggerEvent WHERE PopulationId = " + id.ToString ());

                        if (triggerEventIds.Length != 0) {

                            triggerEventIds = triggerEventIds.Substring (0, triggerEventIds.Length - 1);

                            sqlStatement.Append (" AND PopulationTriggerEventId NOT IN (" + triggerEventIds + ")");

                        }

                        success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                    }

                }

                #endregion


                #region Activity Event

                if (success) {

                    String activityEventIds = String.Empty;

                    foreach (PopulationEvents.PopulationActivityEvent currentEvent in activityEvents) {

                        currentEvent.PopulationId = id;

                        success = success && currentEvent.Save (application);

                        activityEventIds = activityEventIds + currentEvent.Id.ToString () + ",";

                        if (!success) { break; }

                    }

                    if (success) {

                        sqlStatement = new StringBuilder ("DELETE FROM PopulationActivityEvent WHERE PopulationId = " + id.ToString ());

                        if (activityEventIds.Length != 0) {

                            activityEventIds = activityEventIds.Substring (0, activityEventIds.Length - 1);

                            sqlStatement.Append (" AND PopulationActivityEventId NOT IN (" + activityEventIds + ")");

                        }

                        success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                    }

                }

                #endregion


                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }


                success = true;

                application.EnvironmentDatabase.CommitTransaction ();

            }

            catch (Exception applicationException) {

                success = false;

                application.EnvironmentDatabase.RollbackTransaction();

                application.SetLastException (applicationException);

            }

            return success;

        }

        virtual public Boolean Delete (Mercury.Server.Application application) {

            Boolean success = false;
            
            try {

                success = application.EnvironmentDatabase.ExecuteSqlStatement ("EXEC Population_Delete " + id.ToString (), 0);

            }

            catch (Exception applicationException) {

                success = false;

                application.SetLastException (applicationException);

            }

            return success;
        }

        #endregion


        #region Public Methods

        public PopulationEvents.PopulationServiceEvent ServiceEvent (Int64 serviceEventId) {

            PopulationEvents.PopulationServiceEvent serviceEvent = null;

            foreach (PopulationEvents.PopulationServiceEvent currentServiceEvent in serviceEvents) {

                if (currentServiceEvent.Id == serviceEventId) {

                    serviceEvent = currentServiceEvent;

                    break;

                }

            }

            return serviceEvent;

        }

        public PopulationEvents.PopulationServiceEventThreshold ServiceEventThreshold (Int64 serviceEventThresholdId) {

            PopulationEvents.PopulationServiceEventThreshold serviceEventThreshold = null;

            foreach (PopulationEvents.PopulationServiceEvent currentServiceEvent in serviceEvents) {

                foreach (PopulationEvents.PopulationServiceEventThreshold currentThreshold in currentServiceEvent.Thresholds) {

                    if (currentThreshold.Id == serviceEventThresholdId) {

                        serviceEventThreshold = currentThreshold;

                        break;

                    }

                }

            }

            return serviceEventThreshold;

        }

        public PopulationEvents.PopulationTriggerEvent TriggerEvent (Int64 triggerEventId) {

            PopulationEvents.PopulationTriggerEvent triggerEvent = null;

            foreach (PopulationEvents.PopulationTriggerEvent currentTriggerEvent in triggerEvents) {

                if (currentTriggerEvent.Id == triggerEventId) {

                    triggerEvent = currentTriggerEvent;

                    break;

                }

            }

            return triggerEvent;

        }

        public PopulationEvents.PopulationActivityEvent ActivityEvent (Int64 activityEventId) {

            PopulationEvents.PopulationActivityEvent activityEvent = null;

            foreach (PopulationEvents.PopulationActivityEvent currentActivityEvent in activityEvents) {

                if (currentActivityEvent.Id == activityEventId) {

                    activityEvent = currentActivityEvent;

                    break;

                }

            }

            return activityEvent;

        }

        #endregion


        #region Private Methods - Process Logs

        private void ProcessLog_StartProcess () {

            String insertStatement = String.Empty;

            Boolean success;

            Mercury.Server.Data.AuthorityAccountStamp accountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application);

            try {

                base.application.EnvironmentDatabase.BeginTransaction ();

                insertStatement = "INSERT INTO logs.PopulationProcess (PopulationId, PopulationName, StartDate, ";

                insertStatement = insertStatement + "CreateAuthorityName, CreateAccountId, CreateAccountName, CreateDate, ";

                insertStatement = insertStatement + "ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate) ";
                
                insertStatement = insertStatement + "VALUES (";

                insertStatement = insertStatement + id.ToString () + ", '" + NameSql + "', '" + DateTime.Now.ToString () + "', ";

                insertStatement = insertStatement + "'" + accountInfo.SecurityAuthorityNameSql + "', '" + accountInfo.UserAccountIdSql + "', '" + accountInfo.UserAccountNameSql + "', '" + DateTime.Now.ToString () + "', ";

                insertStatement = insertStatement + "'" + accountInfo.SecurityAuthorityNameSql + "', '" + accountInfo.UserAccountIdSql + "', '" + accountInfo.UserAccountNameSql + "', '" + DateTime.Now.ToString () + "'";

                insertStatement = insertStatement + ")";


                success = base.application.EnvironmentDatabase.ExecuteSqlStatement (insertStatement.ToString (), 0);

                if (!success) { throw base.application.EnvironmentDatabase.LastException; }


                processLogId = 0;

                if (processLogId == 0) { // RESET DOCUMENT ID CRITERIA

                    Object identity = base.application.EnvironmentDatabase.ExecuteScalar ("SELECT @@IDENTITY").ToString ();

                    if (!Int64.TryParse ((String) identity, out processLogId)) {

                        throw new ApplicationException ("Unable to retreive unique id.");

                    }

                }

                base.application.EnvironmentDatabase.CommitTransaction ();

            }

            catch (Exception logException) {

                System.Diagnostics.Trace.WriteLine (insertStatement);

                System.Diagnostics.Trace.WriteLine (logException);

                System.Diagnostics.Trace.Flush ();

                base.application.EnvironmentDatabase.RollbackTransaction ();

            }

            return;

        }

        private void ProcessLog_StopProcess (String outcome, String exceptionMessage) {

            String updateStatement = String.Empty;

            Boolean success;

            Mercury.Server.Data.AuthorityAccountStamp accountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application);

            try {

                updateStatement = "UPDATE logs.PopulationProcess \r\n  SET ";

                updateStatement = updateStatement + "\r\n    EndDate = '" + DateTime.Now.ToString () + "', ";

                updateStatement = updateStatement + "\r\n     Outcome = '" + outcome + "', ";

                updateStatement = updateStatement + "\r\n     Exception = '" + exceptionMessage + "'";

                updateStatement = updateStatement + "\r\n  WHERE ProcessLogId = " + processLogId.ToString ();


                success = base.application.EnvironmentDatabase.ExecuteSqlStatement (updateStatement.ToString (), 0);

                if (!success) { throw base.application.EnvironmentDatabase.LastException; }

            }

            catch (Exception logException) {

                System.Diagnostics.Trace.WriteLine (logException);

                System.Diagnostics.Trace.Flush ();

            }

            return;

        }

        private void ProcessStep_StartStep (String stepName, String stepDescription, String debug) {

            String insertStatement = String.Empty;

            Boolean success;

            Mercury.Server.Data.AuthorityAccountStamp accountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application);

            try {

                base.application.EnvironmentDatabase.BeginTransaction ();

                insertStatement = "INSERT INTO logs.PopulationProcessStep (ProcessLogId, StepName, StepDescription, StartDate, Debug, ";

                insertStatement = insertStatement + "CreateAuthorityName, CreateAccountId, CreateAccountName, CreateDate, ";

                insertStatement = insertStatement + "ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate) ";

                insertStatement = insertStatement + "VALUES (";

                insertStatement = insertStatement + processLogId.ToString () + ", '" + CommonFunctions.SetValueMaxLength (stepName, 60) + "', '" + CommonFunctions.SetValueMaxLength (stepDescription, 120) + "', '" + DateTime.Now.ToString () + "', '" + CommonFunctions.SetValueMaxLength (debug.Replace ("'", "''"), 4000) + "', ";

                insertStatement = insertStatement + "'" + accountInfo.SecurityAuthorityNameSql + "', '" + accountInfo.UserAccountIdSql + "', '" + accountInfo.UserAccountNameSql + "', '" + DateTime.Now.ToString () + "', ";

                insertStatement = insertStatement + "'" + accountInfo.SecurityAuthorityNameSql + "', '" + accountInfo.UserAccountIdSql + "', '" + accountInfo.UserAccountNameSql + "', '" + DateTime.Now.ToString () + "'";

                insertStatement = insertStatement + ")";


                success = base.application.EnvironmentDatabase.ExecuteSqlStatement (insertStatement.ToString (), 0);

                if (!success) { throw base.application.EnvironmentDatabase.LastException; }


                processStepId = 0;

                if (processStepId == 0) { // RESET DOCUMENT ID CRITERIA

                    Object identity = base.application.EnvironmentDatabase.ExecuteScalar ("SELECT @@IDENTITY").ToString ();

                    if (!Int64.TryParse ((String) identity, out processStepId)) {

                        throw new ApplicationException ("Unable to retreive unique id.");

                    }

                }

                base.application.EnvironmentDatabase.CommitTransaction ();

            }

            catch (Exception logException) {

                System.Diagnostics.Trace.WriteLine (insertStatement);

                System.Diagnostics.Trace.WriteLine (logException);

                System.Diagnostics.Trace.Flush ();

                base.application.EnvironmentDatabase.RollbackTransaction ();

            }

            return;

        }

        private void ProcessStep_StartStep (String stepName, String stepDescription) {

            ProcessStep_StartStep (stepName, stepDescription, String.Empty);

            return;

        }

        private void ProcessStep_StopStep (Int32 recordCount, String outcome, String exceptionMessage) {

            String updateStatement = String.Empty;

            Boolean success;

            Mercury.Server.Data.AuthorityAccountStamp accountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application);

            try {

                updateStatement = "UPDATE logs.PopulationProcessStep \r\n  SET ";

                updateStatement = updateStatement + "\r\n    EndDate = '" + DateTime.Now.ToString () + "', ";

                updateStatement = updateStatement + "\r\n    Records = " + recordCount.ToString () + ", ";

                updateStatement = updateStatement + "\r\n     Outcome = '" + CommonFunctions.SetValueMaxLength (outcome.Replace ("'", "''"), 60) + "', ";

                updateStatement = updateStatement + "\r\n     Exception = '" + CommonFunctions.SetValueMaxLength (exceptionMessage.Replace ("'", "''"), 999) + "'";

                updateStatement = updateStatement + "\r\n  WHERE ProcessStepId = " + processStepId.ToString ();


                success = base.application.EnvironmentDatabase.ExecuteSqlStatement (updateStatement.ToString (), 0);

                if (!success) { throw base.application.EnvironmentDatabase.LastException; }

            }

            catch (Exception logException) {

                System.Diagnostics.Trace.WriteLine (updateStatement);

                System.Diagnostics.Trace.WriteLine (logException);

                System.Diagnostics.Trace.Flush ();

                base.application.EnvironmentDatabase.RollbackTransaction ();

            }

            return;

        }

        #endregion


        #region Process Methods - Population Add/Terminate

        protected String SelectStatementMembershipAdd () {

            String procedureStatement = String.Empty;

            String procedureDeclaration = String.Empty;

            String selectStatement = String.Empty;

            String selectClause = String.Empty;

            String fromClause = String.Empty;

            String whereClause = String.Empty;

            String criteriaClause = String.Empty;


            #region Procedure Statement Header



            procedureDeclaration = "DECLARE @membershipTable TABLE (";
//            procedureDeclaration = procedureDeclaration + "\r\n        PopulationMembershipId                      BIGINT,";
            procedureDeclaration = procedureDeclaration + "\r\n        PopulationId                                BIGINT,";
            procedureDeclaration = procedureDeclaration + "\r\n        MemberId                                    BIGINT,";
    
            procedureDeclaration = procedureDeclaration + "\r\n        AnchorDate                                DATETIME,";
            procedureDeclaration = procedureDeclaration + "\r\n        TerminationDate                           DATETIME,";
    
            procedureDeclaration = procedureDeclaration + "\r\n        IdentifyingEventMemberServiceId             BIGINT,";
            procedureDeclaration = procedureDeclaration + "\r\n        IdentifyingEventServiceId                   BIGINT,";
            procedureDeclaration = procedureDeclaration + "\r\n        IdentifyingEventDate                      DATETIME,";

            procedureDeclaration = procedureDeclaration + "\r\n        MaxTerminatingEventDate                   DATETIME,";
/*    
            procedureDeclaration = procedureDeclaration + "\r\n        TerminatingEventMemberServiceId             BIGINT,";
            procedureDeclaration = procedureDeclaration + "\r\n        TerminatingEventServiceId                   BIGINT,";
            procedureDeclaration = procedureDeclaration + "\r\n        TerminatingEventDate                      DATETIME,";
*/
            procedureDeclaration = procedureDeclaration + "\r\n        EmptyField VARCHAR (1)";
            procedureDeclaration = procedureDeclaration + "\r\n    )\r\n";

            procedureStatement = procedureStatement + "INSERT INTO @membershipTable ";
        
            #endregion


            #region Select Clause

            selectClause = "SELECT DISTINCT "; 
            
            selectClause = selectClause + "\r\n    CAST (" + id.ToString () + " AS BIGINT) AS PopulationId, ";
            
            selectClause = selectClause + "\r\n    ActiveMembership.MemberId, ";

            switch (initialAnchorDate) {

                case Mercury.Server.Core.Population.Enumerations.PopulationInitialAnchorDate.MemberBirthDate:

                    // selectClause = selectClause + "\r\n    CASE WHEN (PreviousMembership.MemberId IS NULL) THEN ActiveMembership.BirthDate ELSE CAST (CONVERT (CHAR (010), GETDATE (), 101) AS DATETIME) END AS EffectiveDate, ";

                    selectClause = selectClause + "\r\n    ActiveMembership.BirthDate AS AnchorDate, ";

                    break;

                case Mercury.Server.Core.Population.Enumerations.PopulationInitialAnchorDate.MemberEnrollmentDate:

                    // selectClause = selectClause + "\r\n    CASE WHEN (PreviousMembership.MemberId IS NULL) THEN ActiveMembership.EnrollmentEffectiveDate ELSE CAST (CONVERT (CHAR (010), GETDATE (), 101) AS DATETIME) END AS EffectiveDate, ";

                    selectClause = selectClause + "\r\n    ActiveMembership.EnrollmentEffectiveDate AS AnchorDate, ";

                    break;

                case Mercury.Server.Core.Population.Enumerations.PopulationInitialAnchorDate.IdentifyingServiceDate:

                    if (HasEventCriteriaIdentifyingService) {

                        selectClause = selectClause + "\r\n    CASE WHEN (IdentifyingEvent.EventDate IS NOT NULL) THEN IdentifyingEvent.EventDate ELSE CAST (CONVERT (CHAR (010), GETDATE (), 101) AS DATETIME) END AS AnchorDate, ";

                    }

                    else { selectClause = selectClause + "\r\n    CAST (CONVERT (CHAR (010), GETDATE (), 101) AS DATETIME) AS AnchorDate, "; }

                    break;

                default:

                    selectClause = selectClause + "\r\n    CAST (CONVERT (CHAR (010), GETDATE (), 101) AS DATETIME) AS AnchorDate, ";

                    break;

            }
    
            selectClause = selectClause + "\r\n    CAST ('12/31/9999' AS DATETIME) AS TerminationDate, ";

            if (HasEventCriteriaIdentifyingService) {

                selectClause = selectClause + "\r\n    IdentifyingEvent.MemberServiceId AS IdentifyingEventMemberServiceId,";

                selectClause = selectClause + "\r\n    IdentifyingEvent.ServiceId       AS IdentifyingEventServiceId,";

                selectClause = selectClause + "\r\n    IdentifyingEvent.EventDate       AS IdentifyingEventDate,";

            }

            else {

                selectClause = selectClause + "\r\n    CAST (0 AS BIGINT)      AS IdentifyingEventMemberServiceId,";

                selectClause = selectClause + "\r\n    CAST (0 AS BIGINT)      AS IdentifyingEventServiceId,";

                selectClause = selectClause + "\r\n    CAST (NULL AS DATETIME) AS IdentifyingEventDate,";

            }

            if (HasEventCriteriaTerminatingService) {

                selectClause = selectClause + "\r\n    MaxTerminatingEvent.EventDate AS MaxTerminatingEventDate,";

            }

            else {

                selectClause = selectClause + "\r\n    CAST (NULL AS DATETIME) AS MaxTerminatingEventDate,";

            }

/*            
            selectClause = selectClause + "\r\n    CAST (0 AS BIGINT)      AS TerminatingEventMemberServiceId,";

            selectClause = selectClause + "\r\n    CAST (0 AS BIGINT)      AS TerminatingEventServiceId,";

            selectClause = selectClause + "\r\n    CAST (NULL AS DATETIME) AS TerminatingEventDate,";


            selectClause = selectClause + "\r\n    '" + application.Session.AuthorityName + "' AS CreateAuthorityName,";

            selectClause = selectClause + "\r\n    '" + application.Session.UserAccountId + "' AS CreateAccountId,";

            selectClause = selectClause + "\r\n    '" + application.Session.UserAccountName + "' AS CreateAccountName,";

            selectClause = selectClause + "\r\n    '" + lastRunDate.ToString ("MM/dd/yyy hh:mm:ss") + "' AS CreateDate, ";


            selectClause = selectClause + "\r\n    '" + application.Session.AuthorityName + "' AS ModifiedAuthorityName,";

            selectClause = selectClause + "\r\n    '" + application.Session.UserAccountId + "' AS ModifiedAccountId,";

            selectClause = selectClause + "\r\n    '" + application.Session.UserAccountName + "' AS ModifiedAccountName,";

            selectClause = selectClause + "\r\n    '" + lastRunDate.ToString ("MM/dd/yyy hh:mm:ss") + "' AS ModifiedDate";
*/

            selectClause = selectClause + "\r\n    '' AS EmptyField";

            #endregion


            #region From Clause

            fromClause = "\r\n\r\n  FROM";

            if (!allowProspective) { fromClause = fromClause + "\r\n    dal.ActiveMembership AS ActiveMembership "; }

            else { fromClause = fromClause + "\r\n    dal.ActiveProspectiveMembership AS ActiveMembership "; }

            fromClause = fromClause + "\r\n";

            fromClause = fromClause + "\r\n      LEFT JOIN PopulationMembership";

            fromClause = fromClause + "\r\n        ON (ActiveMembership.MemberId = PopulationMembership.MemberId)";

            fromClause = fromClause + "\r\n        AND (PopulationMembership.PopulationId = " + id.ToString () + ")";

            fromClause = fromClause + "\r\n        AND (GETDATE () BETWEEN PopulationMembership.EffectiveDate AND PopulationMembership.TerminationDate)";

            fromClause = fromClause + "\r\n";


            #region Initial Effective Date

            //switch (initialAnchorDate) {

            //    case Mercury.Server.Core.Population.Enumerations.PopulationInitialAnchorDate.MemberBirthDate:

            //    case Mercury.Server.Core.Population.Enumerations.PopulationInitialAnchorDate.MemberEnrollmentDate:

            //        fromClause = fromClause + "\r\n      LEFT JOIN (";

            //        fromClause = fromClause + "\r\n        SELECT PopulationMembership.PopulationId, PopulationMembership.MemberId, MAX (TerminationDate) AS TerminationDate";

            //        fromClause = fromClause + "\r\n          FROM  PopulationMembership ";

            //        fromClause = fromClause + "\r\n          WHERE  (PopulationMembership.PopulationId = " + id.ToString () + ")";

            //        fromClause = fromClause + "\r\n          GROUP BY PopulationMembership.PopulationId, PopulationMembership.MemberId ";

            //        fromClause = fromClause + "\r\n        ) AS PreviousMembership ";

            //        fromClause = fromClause + "\r\n        ON (ActiveMembership.MemberId = PreviousMembership.MemberId) ";

            //        fromClause = fromClause + "\r\n";

            //        break;

            //}

            #endregion


            #region Criteria Events

            if (HasEventCriteriaExcludedService) {

                fromClause = fromClause + "\r\n      LEFT JOIN MemberService AS ExcludedService";

                fromClause = fromClause + "\r\n        ON (ActiveMembership.MemberId = ExcludedService.MemberId) ";


                criteriaClause = String.Empty;

                foreach (PopulationCriteria.PopulationCriteriaEvent populationCriteria in eventCriteria) {

                    if (populationCriteria.EventType == Mercury.Server.Core.Population.Enumerations.PopulationCriteriaEventType.Exclusion) {

                        criteriaClause = criteriaClause + "{" + populationCriteria.ServiceId.ToString () + "}";

                    }

                }

                criteriaClause = criteriaClause.Replace ("}{", ", ");

                criteriaClause = criteriaClause.Replace ('{', '(');

                criteriaClause = criteriaClause.Replace ('}', ')');

                fromClause = fromClause + "\r\n        AND (ExcludedService.ServiceId IN " + criteriaClause + ")";

                fromClause = fromClause + "\r\n";

            }

            if (HasEventCriteriaIdentifyingService) {

                criteriaClause = String.Empty;

                foreach (PopulationCriteria.PopulationCriteriaEvent populationCriteria in eventCriteria) {

                    if (populationCriteria.EventType == Mercury.Server.Core.Population.Enumerations.PopulationCriteriaEventType.Identifying) {

                        criteriaClause = criteriaClause + "{" + populationCriteria.ServiceId.ToString () + "}";

                    }

                }

                criteriaClause = criteriaClause.Replace ("}{", ", ");

                criteriaClause = criteriaClause.Replace ('{', '(');

                criteriaClause = criteriaClause.Replace ('}', ')');

                fromClause = fromClause + "\r\n      JOIN MemberService AS IdentifyingEvent";

                fromClause = fromClause + "\r\n        ON (ActiveMembership.MemberId = IdentifyingEvent.MemberId)";

                fromClause = fromClause + "\r\n        AND (IdentifyingEvent.ServiceId IN " + criteriaClause + ")";

            }

            if (HasEventCriteriaTerminatingService) {

                criteriaClause = String.Empty;

                foreach (PopulationCriteria.PopulationCriteriaEvent populationCriteria in eventCriteria) {

                    if (populationCriteria.EventType == Mercury.Server.Core.Population.Enumerations.PopulationCriteriaEventType.Terminating) {

                        criteriaClause = criteriaClause + "{" + populationCriteria.ServiceId.ToString () + "}";

                    }

                }

                criteriaClause = criteriaClause.Replace ("}{", ", ");

                criteriaClause = criteriaClause.Replace ('{', '(');

                criteriaClause = criteriaClause.Replace ('}', ')');

                fromClause = fromClause + "\r\n      LEFT JOIN (SELECT MemberId, MAX (EventDate) AS EventDate FROM MemberService WHERE ServiceId IN " + criteriaClause + " GROUP BY MemberId) AS MaxTerminatingEvent";

                fromClause = fromClause + "\r\n        ON (ActiveMembership.MemberId = MaxTerminatingEvent.MemberId)";

            }

            #endregion

            #endregion // FROM CLAUSE


            #region Where Clause

            whereClause = whereClause + "\r\n\r\n  WHERE {PopulationMembership.MemberId IS NULL}";

            if (HasEventCriteriaExcludedService) { whereClause = whereClause + "{ExcludedService.MemberId IS NULL}"; }

            if (HasEventCriteriaTerminatingService) {

                if (HasEventCriteriaIdentifyingService) {

                    whereClause = whereClause + "{(MaxTerminatingEvent.MemberId IS NULL) OR (IdentifyingEvent.EventDate > MaxTerminatingEvent.EventDate)}";

                }

                else {

                    whereClause = whereClause + "{MaxTerminatingEvent.MemberId IS NULL}";

                }

            }


            #region Enrollment Criteria Filtering

            criteriaClause = String.Empty;

            foreach (PopulationCriteria.PopulationCriteriaEnrollment populationCriteria in enrollmentCriteria) {

                if (!String.IsNullOrEmpty (populationCriteria.CriteriaClause)) {

                    criteriaClause = criteriaClause + "{" + populationCriteria.CriteriaClause + "}";

                }

            }

            criteriaClause = criteriaClause.Replace ("}{", ") \r\n      OR (");

            criteriaClause = criteriaClause.Replace ('{', '(');

            criteriaClause = criteriaClause.Replace ('}', ')');

            if (!String.IsNullOrEmpty (criteriaClause)) { whereClause = whereClause + "{" + criteriaClause + "}"; }
    
            #endregion


            #region Demographic Criteria Filtering

            criteriaClause = String.Empty;

            foreach (PopulationCriteria.PopulationCriteriaDemographic populationCriteria in demographicCriteria) {

                if (!String.IsNullOrEmpty (populationCriteria.CriteriaClause)) {

                    criteriaClause = criteriaClause + "{" + populationCriteria.CriteriaClause + "}";

                }

            }

            criteriaClause = criteriaClause.Replace ("}{", ") \r\n      OR (");

            criteriaClause = criteriaClause.Replace ('{', '(');

            criteriaClause = criteriaClause.Replace ('}', ')');

            if (!String.IsNullOrEmpty (criteriaClause)) { whereClause = whereClause + "{" + criteriaClause + "}"; }

            #endregion 


            whereClause = whereClause.Replace ("}{", ") \r\n    AND (");

            whereClause = whereClause.Replace ('{', '(');

            whereClause = whereClause.Replace ('}', ')');

            #endregion


            selectStatement = selectClause + fromClause + whereClause +"\r\n  "; // ORDER BY ActiveMembership.MemberId, IdentifyingEventDate";


            procedureStatement = procedureDeclaration + procedureStatement + selectStatement;

            procedureStatement = procedureStatement + "\r\n SELECT * FROM @membershipTable ORDER BY MemberId, IdentifyingEventDate";


            // System.Diagnostics.Debug.WriteLine (procedureStatement);

            return procedureStatement;

        }

        protected String SelectStatementMembershipTerminate () {

            String selectStatement = String.Empty;

            String executeStatement = String.Empty;

            String innerSelect = String.Empty;

            String selectClause = String.Empty;

            String fromClause = String.Empty;

            String whereClause = String.Empty;

            String orderByClause = String.Empty;

            String criteriaClause = String.Empty;




            #region Inner Select for Criteria

            #region Select Clause

            selectClause = "SELECT DISTINCT ActiveMembership.MemberId ";

            #endregion


            #region From Clause

            fromClause = "\r\n\r\n  FROM";

//             fromClause = fromClause + "\r\n    dal.ActiveMembershipByPopulation (" + id.ToString () + ") AS ActiveMembership ";

            if (!allowProspective) { fromClause = fromClause + "\r\n    dal.ActiveMembership AS ActiveMembership "; }

            else { fromClause = fromClause + "\r\n    dal.ActiveProspectiveMembership AS ActiveMembership "; }

            // fromClause = fromClause + "\r\n    dal.ActiveMembership ";

            fromClause = fromClause + "\r\n";


            #endregion // FROM CLAUSE


            #region Where Clause

            #region Enrollment Criteria

            criteriaClause = String.Empty;

            foreach (PopulationCriteria.PopulationCriteriaEnrollment populationCriteria in enrollmentCriteria) {

                if (!String.IsNullOrEmpty (populationCriteria.CriteriaClause)) {

                    criteriaClause = criteriaClause + "{" + populationCriteria.CriteriaClause + "}";

                }

            }

            criteriaClause = criteriaClause.Replace ("}{", ") \r\n      OR (");

            criteriaClause = criteriaClause.Replace ('{', '(');

            criteriaClause = criteriaClause.Replace ('}', ')');

            if (!String.IsNullOrEmpty (criteriaClause)) { whereClause = whereClause + "{" + criteriaClause + "}"; }

            #endregion


            #region Demographic Criteria

            criteriaClause = String.Empty;

            foreach (PopulationCriteria.PopulationCriteriaDemographic populationCriteria in demographicCriteria) {

                if (!String.IsNullOrEmpty (populationCriteria.CriteriaClause)) {

                    criteriaClause = criteriaClause + "{" + populationCriteria.CriteriaClause + "}";

                }

            }

            criteriaClause = criteriaClause.Replace ("}{", ") \r\n      OR (");

            criteriaClause = criteriaClause.Replace ('{', '(');

            criteriaClause = criteriaClause.Replace ('}', ')');

            if (!String.IsNullOrEmpty (criteriaClause)) { whereClause = whereClause + "{" + criteriaClause + "}"; }

            #endregion


            whereClause = whereClause.Replace ("}{", ") \r\n    AND (");

            whereClause = whereClause.Replace ('{', '(');

            whereClause = whereClause.Replace ('}', ')');

            whereClause = whereClause.Trim ();

            if (whereClause.Length > 0) { whereClause = "\r\n  WHERE \r\n " + whereClause; }

            #endregion

            innerSelect = selectClause + fromClause + whereClause;

            #endregion


            executeStatement = "DECLARE @ValidPopulation AS TABLE (MemberId BIGINT) \r\n";

            executeStatement = executeStatement + "INSERT INTO @ValidPopulation \r\n ";

            executeStatement = executeStatement + innerSelect + " \r\n  \r\n ";


            selectClause = "SELECT PopulationMembership.PopulationMembershipId, ";

            //if (HasEventCriteriaTerminatingService) {

            //    selectClause = selectClause + "\r\n    TerminatingEvent.MemberServiceId AS TerminatingEventMemberServiceId,";

            //    selectClause = selectClause + "\r\n    TerminatingEvent.ServiceId       AS TerminatingEventServiceId,";

            //    selectClause = selectClause + "\r\n    TerminatingEvent.EventDate       AS TerminatingEventDate,";

            //}

            //else {

                selectClause = selectClause + "\r\n    CAST (0 AS BIGINT)      AS TerminatingEventMemberServiceId,";

                selectClause = selectClause + "\r\n    CAST (0 AS BIGINT)      AS TerminatingEventServiceId,";

                selectClause = selectClause + "\r\n    CAST (NULL AS DATETIME) AS TerminatingEventDate,";

            //}

            selectClause = selectClause + "\r\n    '' AS EmptyField";


            fromClause = "\r\n  FROM PopulationMembership \r\n    ";

            fromClause = fromClause + "  LEFT JOIN @ValidPopulation AS ValidPopulation \r\n";

            fromClause = fromClause + "    ON PopulationMembership.MemberId = ValidPopulation.MemberId \r\n";

            //fromClause = fromClause + "  LEFT JOIN (\r\n" + innerSelect + "\r\n    ) AS ValidPopulation";

            //fromClause = fromClause + "\r\n    ON dal.ConvertMemberIdToSource (PopulationMembership.MemberId) = ValidPopulation.ExternalMemberId";


            #region Terminating and Exclusion Events (FROM CLAUSE)

            //if (HasEventCriteriaTerminatingService) {

            //    criteriaClause = String.Empty;

            //    foreach (PopulationCriteria.EventCriteria populationCriteria in eventCriteria) {

            //        if (populationCriteria.EventType == Mercury.Server.Core.Population.Enumerations.EventType.Terminating) {

            //            criteriaClause = criteriaClause + "{" + populationCriteria.ServiceId.ToString () + "}";

            //        }

            //    }

            //    criteriaClause = criteriaClause.Replace ("}{", ", ");

            //    criteriaClause = criteriaClause.Replace ('{', '(');

            //    criteriaClause = criteriaClause.Replace ('}', ')');

            //    fromClause = fromClause + "\r\n      LEFT JOIN MemberService AS TerminatingEvent";

            //    fromClause = fromClause + "\r\n        ON (PopulationMembership.MemberId = TerminatingEvent.MemberId)";

            //    fromClause = fromClause + "\r\n        AND (TerminatingEvent.ServiceId IN " + criteriaClause + ")";

            //    fromClause = fromClause + "\r\n        AND (TerminatingEvent.EventDate BETWEEN PopulationMembership.EffectiveDate AND PopulationMembership.TerminationDate)";

            //}


            //if (HasEventCriteriaExcludedService) {

            //    criteriaClause = String.Empty;

            //    foreach (PopulationCriteria.EventCriteria populationCriteria in eventCriteria) {

            //        if (populationCriteria.EventType == Mercury.Server.Core.Population.Enumerations.EventType.Exclusion) {

            //            criteriaClause = criteriaClause + "{" + populationCriteria.ServiceId.ToString () + "}";

            //        }

            //    }

            //    criteriaClause = criteriaClause.Replace ("}{", ", ");

            //    criteriaClause = criteriaClause.Replace ('{', '(');

            //    criteriaClause = criteriaClause.Replace ('}', ')');

            //    fromClause = fromClause + "\r\n      LEFT JOIN MemberService AS ExcludedService";

            //    fromClause = fromClause + "\r\n        ON (PopulationMembership.MemberId = ExcludedService.MemberId) ";

            //    fromClause = fromClause + "\r\n        AND (ExcludedService.ServiceId IN " + criteriaClause + ")";

            //    fromClause = fromClause + "\r\n";

            //}

            #endregion 


            whereClause = "\r\n  WHERE ";

            whereClause = whereClause + "\r\n    ((PopulationMembership.PopulationId = " + id.ToString () + ")";

            whereClause = whereClause + "\r\n      AND (PopulationMembership.TerminationDate > GETDATE ()))";

            #region Terminating and Exclusion Events (FROM CLAUSE)

            //if ((HasEventCriteriaExcludedService) && (HasEventCriteriaTerminatingService)) {

            //    whereClause = whereClause + "\r\n    AND ((ValidPopulation.ExternalMemberId IS NULL) OR (TerminatingEvent.MemberId IS NOT NULL) OR ExcludedService.MemberId IS NOT NULL)";

            //    orderByClause = "\r\n    ORDER BY PopulationMembership.PopulationMembershipId, TerminatingEvent.EventDate DESC";

            //}

            //else if (HasEventCriteriaExcludedService) {

            //    whereClause = whereClause + "\r\n    AND ((ValidPopulation.ExternalMemberId IS NULL) OR (ExcludedService.MemberId IS NOT NULL))";

            //    orderByClause = "\r\n    ORDER BY PopulationMembership.PopulationMembershipId";

            //}

            //else if (HasEventCriteriaTerminatingService) {

            //    whereClause = whereClause + "\r\n    AND ((ValidPopulation.ExternalMemberId IS NULL) OR (TerminatingEvent.MemberId IS NOT NULL))";

            //    orderByClause = "\r\n    ORDER BY PopulationMembership.PopulationMembershipId, TerminatingEvent.EventDate DESC";

            //}

            #endregion

            //else {

                whereClause = whereClause + "\r\n    AND (ValidPopulation.MemberId IS NULL)";

            //}


            // selectStatement = selectClause + fromClause + whereClause + orderByClause;

            selectStatement = executeStatement + selectClause + fromClause + whereClause + orderByClause;

            // System.Diagnostics.Debug.WriteLine (selectStatement);

            return selectStatement;

        }

        protected Boolean ProcessMembershipAdd () {

            Boolean success = true;

            String selectStatement = SelectStatementMembershipAdd ();

            System.Data.DataTable membershipTable;

            Int64 memberId = 0;

            List<Int64> processedMemberIds = new List<Int64> ();


            Mercury.Server.Data.AuthorityAccountStamp accountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application);


            ProcessStep_StartStep ("Membership Add", Name, (processDebug) ? selectStatement : String.Empty);


            membershipTable = base.application.EnvironmentDatabase.SelectDataTable (selectStatement, 0);

            foreach (System.Data.DataRow currentRow in membershipTable.Rows) {

                memberId = (Int64) currentRow["MemberId"];

                if (!processedMemberIds.Contains (memberId)) {

                    Boolean allowMemberAdd = true;

                    if (OnBeforeMembershipAddWorkflowId != 0) {

                        Core.Action.Action eventAction = OnBeforeMembershipAddEventAction;


                        eventAction.ActionParameters["MemberId"].ValueType = Mercury.Server.Core.Action.Enumerations.ActionParameterValueType.FixedValue;

                        eventAction.ActionParameters["MemberId"].Value = memberId.ToString ();



                        eventAction.ActionParameters["PopulationId"].ValueType = Mercury.Server.Core.Action.Enumerations.ActionParameterValueType.FixedValue;

                        eventAction.ActionParameters["PopulationId"].Value = id.ToString ();



                        eventAction.ActionParameters["IdentifyingEventMemberServiceId"].ValueType = Mercury.Server.Core.Action.Enumerations.ActionParameterValueType.FixedValue;

                        eventAction.ActionParameters["IdentifyingEventMemberServiceId"].Value = ((Int64) currentRow["IdentifyingEventMemberServiceId"]).ToString ();


                        allowMemberAdd = eventAction.Process (this, this, memberId, null, "Population.OnBeforeMembershipAdd");

                        allowMemberAdd &= !eventAction.LastWorkflowResponse.HasException;

                        if (allowMemberAdd) {

                            if (eventAction.LastWorkflowResponse.OutputParameters.ContainsKey ("PopulationEventCancel")) {

                                Boolean populationEventCancel = true;

                                Boolean.TryParse (eventAction.LastWorkflowResponse.OutputParameters["PopulationEventCancel"].ToString (), out populationEventCancel);

                                allowMemberAdd &= !populationEventCancel;

                            }

                        }

                    }

                    if (allowMemberAdd) {

                        StringBuilder insertStatement = new StringBuilder ();

                        insertStatement.Append ("EXEC PopulationProcess_MembershipAdd ");

                        insertStatement.Append (id.ToString () + ", ");

                        insertStatement.Append (memberId.ToString () + ", ");

                        insertStatement.Append ("'" + ((DateTime) currentRow["AnchorDate"]).ToString ("MM/dd/yyyy") + "', ");

                        if (!currentRow["IdentifyingEventDate"].Equals (System.DBNull.Value)) {

                            insertStatement.Append (((Int64) currentRow["IdentifyingEventMemberServiceId"]).ToString () + ", ");

                            insertStatement.Append (((Int64) currentRow["IdentifyingEventServiceId"]).ToString () + ", ");

                            insertStatement.Append ("'" + ((DateTime) currentRow["IdentifyingEventDate"]).ToString ("MM/dd/yyyy") + "', ");

                        }

                        else { insertStatement.Append ("NULL, NULL, NULL, "); }

                        insertStatement.Append ("'" + accountInfo.SecurityAuthorityNameSql + "', '" + accountInfo.UserAccountIdSql + "', '" + accountInfo.UserAccountNameSql + "'");

                        success = success && base.application.EnvironmentDatabase.ExecuteSqlStatement (insertStatement.ToString (), 0);

                        if (!success) { break; }

                        processedMemberIds.Add (memberId);

                    }

                }

            }


            if (success) { ProcessStep_StopStep (processedMemberIds.Count, "Success", String.Empty); }

            else { ProcessStep_StopStep (processedMemberIds.Count, "Failure", base.application.EnvironmentDatabase.LastException.Message); }


            return success;

        }

        protected Boolean ProcessMembershipTerminate () {

            Boolean success = true;

            String selectStatement = SelectStatementMembershipTerminate ();

            String executeStatement = String.Empty;

            System.Data.DataTable membershipTable;

            Int64 membershipId = 0;

            Int32 processedCount = 0;


            Mercury.Server.Data.AuthorityAccountStamp accountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);


            // 2010-03-06 (DQ): REMOVED TO MOVE THE PROCESSING LOGIC FROM THE STORED PROCEDURE INTO THE CODE BASE 
            //   TO SUPPORT THE ONBEFOREMEMBERSHIPTERMINATE EVENT FOR THE POPULATION, THE SP NOW RETURNS THE RESULT SET

            //executeStatement = "EXEC PopulationProcess_MembershipTerminateByEvent " + id.ToString () + ", '" + accountInfo.SecurityAuthorityNameSql + "', '" + accountInfo.UserAccountIdSql + "', '" + accountInfo.UserAccountNameSql + "'";

            //ProcessStep_StartStep ("Membership Terminate By Event", name, (processDebug) ? executeStatement : String.Empty);

            //success = application.EnvironmentDatabase.ExecuteSqlStatement (executeStatement, 0);

            //if (success) { ProcessStep_StopStep (processedCount, "Success", String.Empty); }

            //else { ProcessStep_StopStep (processedCount, "Failure", base.application.EnvironmentDatabase.LastException.Message); }


            #region Terminate by Event

            // 2010-03-06 (DQ) [MCM-1052]: BELOW SECTION MOVES PROCESSING FROM SP TO CODE

            selectStatement = "EXEC PopulationProcess_MembershipTerminateByEvent " + id.ToString ();

            ProcessStep_StartStep ("Membership Terminate By Event", name, (processDebug) ? selectStatement : String.Empty);

            membershipTable = base.application.EnvironmentDatabase.SelectDataTable (selectStatement, 0);

            foreach (System.Data.DataRow currentRow in membershipTable.Rows) {

                membershipId = (Int64) currentRow["PopulationMembershipId"];


                #region On Before Membership Terminate Event

                Boolean allowMemberTerminate = true;

                if (OnBeforeMembershipTerminateWorkflowId != 0) {

                    Core.Action.Action eventAction = OnBeforeMembershipTerminateEventAction;


                    eventAction.ActionParameters["PopulationMembershipId"].ValueType = Mercury.Server.Core.Action.Enumerations.ActionParameterValueType.FixedValue;

                    eventAction.ActionParameters["PopulationMembershipId"].Value = membershipId.ToString ();



                    eventAction.ActionParameters["PopulationId"].ValueType = Mercury.Server.Core.Action.Enumerations.ActionParameterValueType.FixedValue;

                    eventAction.ActionParameters["PopulationId"].Value = id.ToString ();



                    eventAction.ActionParameters["TerminatingEventMemberServiceId"].ValueType = Mercury.Server.Core.Action.Enumerations.ActionParameterValueType.FixedValue;

                    eventAction.ActionParameters["TerminatingEventMemberServiceId"].Value = ((Int64) currentRow["TerminatingEventMemberServiceId"]).ToString ();


                    allowMemberTerminate = eventAction.Process (this, this, membershipId, null, "Population.OnBeforeMembershipTerminate");

                    allowMemberTerminate &= !eventAction.LastWorkflowResponse.HasException;

                    if (allowMemberTerminate) {

                        if (eventAction.LastWorkflowResponse.OutputParameters.ContainsKey ("PopulationEventCancel")) {

                            Boolean populationEventCancel = true;

                            Boolean.TryParse (eventAction.LastWorkflowResponse.OutputParameters["PopulationEventCancel"].ToString (), out populationEventCancel);

                            allowMemberTerminate &= !populationEventCancel;

                        }

                    }

                }

                #endregion


                if (allowMemberTerminate) {

                    StringBuilder insertStatement = new StringBuilder ();

                    insertStatement.Append ("EXEC PopulationProcess_MembershipTerminate ");

                    insertStatement.Append (membershipId.ToString () + ", ");

                    if (!currentRow["TerminatingEventDate"].Equals (System.DBNull.Value)) {

                        insertStatement.Append (((Int64) currentRow["TerminatingEventMemberServiceId"]).ToString () + ", ");

                        insertStatement.Append (((Int64) currentRow["TerminatingEventServiceId"]).ToString () + ", ");

                        insertStatement.Append ("'" + ((DateTime) currentRow["TerminatingEventDate"]).ToString ("MM/dd/yyyy") + "', ");

                    }

                    else { insertStatement.Append ("NULL, NULL, NULL, "); }

                    insertStatement.Append ("'" + accountInfo.SecurityAuthorityNameSql + "', '" + accountInfo.UserAccountIdSql + "', '" + accountInfo.UserAccountNameSql + "'");

                    success = success && base.application.EnvironmentDatabase.ExecuteSqlStatement (insertStatement.ToString (), 0);

                    if (!success) { break; } else { processedCount = processedCount + 1; }

                }

            }


            if (success) { ProcessStep_StopStep (processedCount, "Success", String.Empty); }

            else { ProcessStep_StopStep (processedCount, "Failure", base.application.EnvironmentDatabase.LastException.Message); }

            #endregion 


            #region Terminate by Criteria

            processedCount = 0;

            selectStatement = SelectStatementMembershipTerminate ();
            
            ProcessStep_StartStep ("Membership Terminate", name, (processDebug) ? selectStatement : String.Empty);

            membershipTable = base.application.EnvironmentDatabase.SelectDataTable (selectStatement, 0);

            foreach (System.Data.DataRow currentRow in membershipTable.Rows) {

                membershipId = (Int64) currentRow["PopulationMembershipId"];


                #region On Before Membership Terminate Event

                Boolean allowMemberTerminate = true;

                if (OnBeforeMembershipTerminateWorkflowId != 0) {

                    Core.Action.Action eventAction = OnBeforeMembershipTerminateEventAction;


                    eventAction.ActionParameters["PopulationMembershipId"].ValueType = Mercury.Server.Core.Action.Enumerations.ActionParameterValueType.FixedValue;

                    eventAction.ActionParameters["PopulationMembershipId"].Value = membershipId.ToString ();



                    eventAction.ActionParameters["PopulationId"].ValueType = Mercury.Server.Core.Action.Enumerations.ActionParameterValueType.FixedValue;

                    eventAction.ActionParameters["PopulationId"].Value = id.ToString ();



                    eventAction.ActionParameters["TerminatingEventMemberServiceId"].ValueType = Mercury.Server.Core.Action.Enumerations.ActionParameterValueType.FixedValue;

                    eventAction.ActionParameters["TerminatingEventMemberServiceId"].Value = ((Int64) currentRow["TerminatingEventMemberServiceId"]).ToString ();


                    allowMemberTerminate = eventAction.Process (this, this, membershipId, null, "Population.OnBeforeMembershipTerminate");

                    allowMemberTerminate &= !eventAction.LastWorkflowResponse.HasException;

                    if (allowMemberTerminate) {

                        if (eventAction.LastWorkflowResponse.OutputParameters.ContainsKey ("PopulationEventCancel")) {

                            Boolean populationEventCancel = true;

                            Boolean.TryParse (eventAction.LastWorkflowResponse.OutputParameters["PopulationEventCancel"].ToString (), out populationEventCancel);

                            allowMemberTerminate &= !populationEventCancel;

                        }

                    }

                }

                #endregion 


                if (allowMemberTerminate) {

                    StringBuilder insertStatement = new StringBuilder ();

                    insertStatement.Append ("EXEC PopulationProcess_MembershipTerminate ");

                    insertStatement.Append (membershipId.ToString () + ", ");

                    if (!currentRow["TerminatingEventDate"].Equals (System.DBNull.Value)) {

                        insertStatement.Append (((Int64) currentRow["TerminatingEventMemberServiceId"]).ToString () + ", ");

                        insertStatement.Append (((Int64) currentRow["TerminatingEventServiceId"]).ToString () + ", ");

                        insertStatement.Append ("'" + ((DateTime) currentRow["TerminatingEventDate"]).ToString ("MM/dd/yyyy") + "', ");

                    }

                    else { insertStatement.Append ("NULL, NULL, NULL, "); }

                    insertStatement.Append ("'" + accountInfo.SecurityAuthorityNameSql + "', '" + accountInfo.UserAccountIdSql + "', '" + accountInfo.UserAccountNameSql + "'");

                    success = success && base.application.EnvironmentDatabase.ExecuteSqlStatement (insertStatement.ToString (), 0);

                    if (!success) { break; } else { processedCount = processedCount + 1; }

                }

            }


            if (success) { ProcessStep_StopStep (processedCount, "Success", String.Empty); }

            else { ProcessStep_StopStep (processedCount, "Failure", base.application.EnvironmentDatabase.LastException.Message); }

            #endregion


            return success;
            
        }

        #endregion


        #region Process Methods - Service Events

        protected List<PopulationEvents.PopulationServiceEvent> ServiceEventsProcessSorted () {

            String sortOrder = "|";

            List <PopulationEvents.PopulationServiceEvent> sortedEvents = new List<Mercury.Server.Core.Population.PopulationEvents.PopulationServiceEvent> ();

            Dictionary<Int64, Int32> serviceEventPassed = new Dictionary<Int64, Int32 > ();

            while (sortOrder.Split ('|').Length != (serviceEvents.Count + 2)) {

                foreach (PopulationEvents.PopulationServiceEvent currentServiceEvent in serviceEvents) {

                    if (!sortOrder.Contains ("|" + currentServiceEvent.Id + "|")) {

                        switch (currentServiceEvent.AnchorDate) {

                            case Mercury.Server.Core.Population.Enumerations.PopulationServiceEventAnchorDate.PreviousServiceEvent:

                                Int64 serviceEventId = currentServiceEvent.AnchorDateValue;

                                Boolean addServiceEvent = false;

                                if (ServiceEvent (serviceEventId) != null) {

                                    if (sortOrder.Contains ("|" + serviceEventId.ToString () + "|")) { addServiceEvent = true; }

                                }

                                else { addServiceEvent = true; }


                                // ENSURE THAT SELF REFERENCING LOOPS WILL BE PROCESSED

                                if (!addServiceEvent) {

                                    if (serviceEventPassed.ContainsKey (currentServiceEvent.Id)) {

                                        if (serviceEventPassed[currentServiceEvent.Id] < 99) {

                                            serviceEventPassed[currentServiceEvent.Id] = serviceEventPassed[currentServiceEvent.Id] + 1;

                                        }

                                        else { addServiceEvent = true; }

                                    }

                                    else { serviceEventPassed.Add (currentServiceEvent.Id, 1); }

                                }


                                if (addServiceEvent) { sortOrder = sortOrder + currentServiceEvent.Id.ToString () + "|"; }

                                break;

                            default:

                                sortOrder = sortOrder + currentServiceEvent.Id.ToString () + "|";

                                break;

                        } // switch (currentServiceEvent.AnchorDate) {

                    } // if (!sortOrder.Contains ("|" + currentServiceEvent.ServiceEventId + "|")) {

                } // foreach (PopulationEvents.ServiceEvent currentServiceEvent in serviceEvents) {

            } // while ((sortOrder.Split ('|').Length + 2) != serviceEvents.Count) {

            if (sortOrder.Length > 1) {

                sortOrder = sortOrder.Substring (1, sortOrder.Length - 2);

                foreach (String currentServiceEventId in sortOrder.Split ('|')) {

                    sortedEvents.Add (ServiceEvent (Int64.Parse (currentServiceEventId)));

                }

            }

            return sortedEvents; 

        }

        protected Boolean ProcessServiceEventsRemovedFromPopulation () {

            Boolean success = true;

            String sqlStatement = String.Empty;


            ProcessStep_StartStep ("Service Events Removed from Population", name);


            sqlStatement = "EXEC PopulationProcess_ServiceEvents_RemovedFromPopulation " + id.ToString ();

            success = base.application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement, 0);


            if (success) { ProcessStep_StopStep (0, "Success", String.Empty); }

            else { ProcessStep_StopStep (0, "Failure", base.application.EnvironmentDatabase.LastException.Message); }


            return success;

        }

        protected Boolean ProcessServiceEvents () {

            Boolean success = true;

            String sqlStatement = String.Empty;


            Mercury.Server.Data.AuthorityAccountStamp accountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application);


            #region Add New Events and Check for Compliance

            foreach (PopulationEvents.PopulationServiceEvent currentServiceEvent in ServiceEventsProcessSorted ()) {


                #region Add New Service Events

                // This will have the affect of adding new instances for dependent service events that
                // have a dependency met - using Previous Member Service Id to ensure that it was not previously used.

                ProcessStep_StartStep ("Service Events Add Initial", name + ": " + currentServiceEvent.Id.ToString ());


                sqlStatement = "EXEC PopulationProcess_ServiceEvents_AddAnchor" + ((Int32) currentServiceEvent.AnchorDate).ToString () + " ";

                sqlStatement = sqlStatement + currentServiceEvent.Id.ToString () + ", ";

                sqlStatement = sqlStatement + "'" + modifiedAccountInfo.SecurityAuthorityNameSql + "', '" + modifiedAccountInfo.UserAccountIdSql + "', '" + modifiedAccountInfo.UserAccountNameSql + "'";

                success = base.application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement, 0);


                if (success) { ProcessStep_StopStep (0, "Success", String.Empty); }

                else { ProcessStep_StopStep (0, "Failure", base.application.EnvironmentDatabase.LastException.Message); }

                if (!success) { break; }

                #endregion


                #region Check for Compliance

                ProcessStep_StartStep ("Service Events Compliance", name + ": " + currentServiceEvent.Id.ToString ());


                sqlStatement = "EXEC PopulationProcess_ServiceEvents_ComplianceAnchor" + ((Int32) currentServiceEvent.AnchorDate).ToString () + " ";

                sqlStatement = sqlStatement + currentServiceEvent.Id.ToString () + ", ";

                sqlStatement = sqlStatement + "'" + modifiedAccountInfo.SecurityAuthorityNameSql + "', '" + modifiedAccountInfo.UserAccountIdSql + "', '" + modifiedAccountInfo.UserAccountNameSql + "'";

                success = base.application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement, 0);


                if (success) { ProcessStep_StopStep (0,  "Success", String.Empty); }

                else { ProcessStep_StopStep (0, "Failure", base.application.EnvironmentDatabase.LastException.Message); }

                if (!success) { break; }

                #endregion


            } // foreach (PopulationEvents.ServiceEvent currentServiceEvent in ServiceEventsProcessSorted ()) {

            #endregion


            #region Check for Thresholds 

            if (success) {

                ProcessStep_StartStep ("Service Events Thresholds", name);


                sqlStatement = "EXEC PopulationProcess_ServiceEvents_Thresholds ";

                sqlStatement = sqlStatement + id.ToString () + ", ";

                sqlStatement = sqlStatement + "'" + modifiedAccountInfo.SecurityAuthorityNameSql + "', '" + modifiedAccountInfo.UserAccountIdSql + "', '" + modifiedAccountInfo.UserAccountNameSql + "'";

                success = base.application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement, 0);


                if (success) { ProcessStep_StopStep (0, "Success", String.Empty); }

                else { ProcessStep_StopStep (0, "Failure", base.application.EnvironmentDatabase.LastException.Message); }

            }

            #endregion


            return success;

        }

        #endregion


        #region Process Methods - Trigger and Activity Events

        protected Boolean ProcessTriggerEvents () {

            Boolean success = true;

            String executeStatment = String.Empty; 


            Mercury.Server.Data.AuthorityAccountStamp accountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application);


            executeStatment = executeStatment + "EXEC PopulationProcess_TriggerEvents " + id + ", ";

            executeStatment = executeStatment + "'" + accountInfo.SecurityAuthorityNameSql + "', '" + accountInfo.UserAccountIdSql + "', '" + accountInfo.UserAccountNameSql + "'";


            ProcessStep_StartStep ("Trigger Events", name, (processDebug) ? executeStatment : String.Empty);


            success = base.application.EnvironmentDatabase.ExecuteSqlStatement (executeStatment);


            if (success) { ProcessStep_StopStep (0, "Success", String.Empty); }

            else { ProcessStep_StopStep (0, "Failure", base.application.EnvironmentDatabase.LastException.Message); }


            return success;

        }

        protected Boolean ProcessActivityEvents () {

            Boolean success = true;

            String executeStatment = String.Empty;


            Mercury.Server.Data.AuthorityAccountStamp accountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application);


            executeStatment = executeStatment + "EXEC PopulationProcess_ActivityEvents " + id + ", ";

            executeStatment = executeStatment + "'" + accountInfo.SecurityAuthorityNameSql + "', '" + accountInfo.UserAccountIdSql + "', '" + accountInfo.UserAccountNameSql + "'";


            ProcessStep_StartStep ("Activity Events", name, (processDebug) ? executeStatment : String.Empty);


            success = base.application.EnvironmentDatabase.ExecuteSqlStatement (executeStatment);


            if (success) { ProcessStep_StopStep (0, "Success", String.Empty); }

            else { ProcessStep_StopStep (0, "Failure", base.application.EnvironmentDatabase.LastException.Message); }


            return success;

        }

        #endregion


        #region Process Actions

        protected Boolean ProcessActions () {

            Boolean success = true;

            String selectStatement;

            String updateStatement;

            System.Data.DataTable actionTable;


            CoreObject sender;

            CoreObject eventObject;

            Int64 eventInstanceId = 0;

            Action.EventArguments.PopulationEventArguments eventArguments;


            DateTime timeStart;

            DateTime timeEnd;

            Int32 rowCount = 0;

            String serviceName;


            selectStatement = "SELECT * FROM PopulationAction WHERE PopulationId = " + id.ToString () + " AND ScheduleDate <= GETDATE () AND ActionDate IS NULL ORDER BY ScheduleDate, PopulationActionId";

            ProcessStep_StartStep ("Population Actions", name, (processDebug) ? selectStatement : String.Empty);

            actionTable = base.application.EnvironmentDatabase.SelectDataTable (selectStatement, 0);

            timeStart = DateTime.Now;

            foreach (System.Data.DataRow currentRow in actionTable.Rows) {

                try {

                    rowCount = rowCount + 1;

                    Core.Action.Action populationAction = new Mercury.Server.Core.Action.Action (base.application);

                    populationAction.MapDataFields (String.Empty, currentRow);

                    sender = new PopulationMembership (base.application, (Int64) currentRow["PopulationMembershipId"]);

                    eventInstanceId = (Int64) currentRow["EventInstanceId"];

                    String eventDescription = String.Empty;

                    switch ((String) currentRow["SenderObjectType"]) {

                        case "Mercury.Server.Core.Population.PopulationEvents.PopulationActivityEvent":

                        case "Mercury.Server.Core.Population.PopulationEvents.ActivityEvent":

                        case "QuickSilver.Server.Core.PopulationManagement.PopulationEvents.ActivityEvent": // BACKWARDS COMPATIBILITY                        

                            PopulationEvents.PopulationActivityEvent actionActivityEvent = ActivityEvent ((Int64) currentRow["SenderId"]);

                            eventObject = actionActivityEvent;

                            eventDescription = "Population [" + name + "] Activity Event [" + actionActivityEvent.Description + "]";

                            break;


                        case "Mercury.Server.Core.Population.PopulationEvents.PopulationServiceEvent":

                        case "Mercury.Server.Core.Population.PopulationEvents.ServiceEvent":

                        case "QuickSilver.Server.Core.PopulationManagement.PopulationEvents.ServiceEvent": // BACKWARDS COMPATIBILITY

                            PopulationEvents.PopulationServiceEvent actionServiceEvent = ServiceEvent ((Int64) currentRow["SenderId"]);

                            eventObject = actionServiceEvent;

                            eventDescription = "Population [" + name + "] Service Event [" + actionServiceEvent.Service.ServiceName + "]";

                            break;


                        case "Mercury.Server.Core.Population.PopulationEvents.PopulationServiceEventThreshold": 

                        case "Mercury.Server.Core.Population.PopulationEvents.ServiceEventThreshold":

                        case "QuickSilver.Server.Core.PopulationManagement.PopulationEvents.ServiceEventThreshold": // BACKWARDS COMPATIBILITY

                            PopulationEvents.PopulationServiceEventThreshold actionThreshold = ServiceEventThreshold ((Int64) currentRow["SenderId"]);

                            serviceName = "(** Unknown Service)";

                            if (actionThreshold != null) { serviceName = ServiceEvent (actionThreshold.PopulationServiceEventId).ServiceName; }

                            eventObject = actionThreshold;

                            eventDescription = "Population [" + name + "] Service Event [" + serviceName + "] ";

                            if (actionThreshold != null) {

                                eventDescription = eventDescription + "Threshold [" + actionThreshold.RelativeDateValue.ToString () + " " + actionThreshold.RelativeDateQualifier.ToString () + "]";

                            }

                            break;


                        case "Mercury.Server.Core.Population.PopulationEvents.PopulationTriggerEvent":

                        case "Mercury.Server.Core.Population.PopulationEvents.TriggerEvent":

                        case "QuickSilver.Server.Core.PopulationManagement.PopulationEvents.TriggerEvent": // BACKWARDS COMPATIBILITY

                            PopulationEvents.PopulationTriggerEvent actionTrigger = TriggerEvent ((Int64) currentRow["SenderId"]);

                            eventObject = actionTrigger;

                            eventDescription = "Population [" + name + "] Trigger Event [" + actionTrigger.Description + "]";

                            break;


                        case "PopulationEvent":

                            eventObject = this;

                            switch ((Int64) currentRow["SenderId"]) {

                                case 1: eventDescription = "Population [" + name + "] On Membership Add Event"; break;

                                case 2: eventDescription = "Population [" + name + "] On Membership Terminate Event"; break;

                            }

                            break;

                        default:

                            eventObject = this;
                           
                            break;

                    }

                    eventArguments = new Mercury.Server.Core.Action.EventArguments.PopulationEventArguments ();

                    eventArguments.PopulationId = id;

                    eventArguments.PopulationName = name;

                    eventArguments.PopulationMembershipId = (Int64) currentRow["PopulationMembershipId"];

                    eventArguments.SenderObjectType = (String) currentRow["SenderObjectType"];

                    eventArguments.SenderId = (Int64) currentRow["SenderId"];


                    success = populationAction.Process (sender, eventObject, eventInstanceId, eventArguments, eventDescription);


                    if (success) {

                        updateStatement = "UPDATE PopulationAction SET ActionDate = GETDATE () WHERE PopulationActionId = " + ((Int64) currentRow["PopulationActionId"]).ToString ();

                        success = base.application.EnvironmentDatabase.ExecuteSqlStatement (updateStatement, 0);

                    }

                    else { 

                        String actionExceptionMessage = (populationAction.LastProcessException != null) ? populationAction.LastProcessException.Message : "Unknown and unhandled Exception.";

                        actionExceptionMessage = actionExceptionMessage.Replace ("'", "''");

                        actionExceptionMessage = CommonFunctions.SetValueMaxLength (actionExceptionMessage, 7000);

                        updateStatement = "UPDATE PopulationAction SET ActionDate = GETDATE (), Exception = '" + actionExceptionMessage + "' WHERE PopulationActionId = " + ((Int64) currentRow ["PopulationActionId"]).ToString ();

                        base.application.EnvironmentDatabase.ExecuteSqlStatement (updateStatement, 0);

                    }

                    if ((rowCount % 1000) == 0) {

                        timeEnd = DateTime.Now;

                        System.Diagnostics.Debug.WriteLine ("Population.ProcessActions [" + Name + "] (" + rowCount.ToString () + "): " + timeEnd.Subtract (timeStart).ToString ());

                        timeStart = DateTime.Now;

                    }

                }

                catch (Exception processException) {

                    success = false;

                    System.Diagnostics.Trace.WriteLine (processException.Message);

                    System.Diagnostics.Trace.Flush ();

                }
                   
            } // foreach 


            if (success) { ProcessStep_StopStep (rowCount, "Success", String.Empty); }

            else if (application.EnvironmentDatabase.LastException != null) { ProcessStep_StopStep (rowCount, "Failure", base.application.EnvironmentDatabase.LastException.Message); }

            else if (application.LastException != null) { ProcessStep_StopStep (rowCount, "Failure", base.application.LastException.Message); }

            else { ProcessStep_StopStep (rowCount, "Failure", "An Unknown Exception has occurred."); }


            // application.EnvironmentDatabase.ExecuteSqlStatement ("DELETE FROM PopulationAction WHERE PopulationId = " + id.ToString () + " AND ActionDate IS NOT NULL");

            return success; 

        }

        #endregion 


        #region Process

        public Boolean Process () {

            Boolean success = true;

            try {

                processDebug = true;

                ProcessLog_StartProcess ();

                if (success) { success = success & ProcessMembershipAdd (); }

                if (success) { success = success & ProcessMembershipTerminate (); }

                if (success) { success = success & ProcessServiceEventsRemovedFromPopulation (); }

                if (success) { success = success & ProcessServiceEvents (); }

                if (success) { success = success & ProcessTriggerEvents (); }

                if (success) { success = success & ProcessActivityEvents (); }

                if (success) { success = success & ProcessActions (); }

                ProcessLog_StopProcess ("Success", String.Empty);

            }

            catch (Exception applicationException) {

                ProcessLog_StopProcess ("Failure", applicationException.Message);

                base.application.SetLastException (applicationException);

                success = false; 

            }
            
            return success;

        }

        #endregion


        #region Virtual - Data Bindings

        override public String EvaluateDataBinding (String bindingContext) {

            String dataValue = String.Empty;

            return dataValue;

        }

        override public Dictionary<String, String> DataBindingContexts { 
            
            get {

                Dictionary<String, String> dataBindings = new Dictionary<String, String> ();

                dataBindings = new PopulationMembership (application).DataBindingContexts;

                return dataBindings;

            }

        }

        #endregion


    }

}

