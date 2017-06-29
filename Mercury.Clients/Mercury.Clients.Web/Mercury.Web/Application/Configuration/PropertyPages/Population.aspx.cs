using System;
using System.Collections;
using System.Collections.Generic;
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

namespace Mercury.Web.Application.Configuration.Windows {

    public partial class Population : System.Web.UI.Page {

        #region Private Properties

        private Mercury.Client.Core.Population.Population population = null;

        #endregion 


        #region Private Session Properties

        private String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (PageInstanceId.Text)) { PageInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return Form.Name + PageInstanceId.Text + ".";
            }

        }

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application) Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        private Mercury.Client.Core.Population.PopulationEvents.PopulationServiceEvent EditServiceEvent {

            get {

                Mercury.Client.Core.Population.PopulationEvents.PopulationServiceEvent serviceEvent = (Mercury.Client.Core.Population.PopulationEvents.PopulationServiceEvent) Session[SessionCachePrefix + "ServiceEvent"];

                if (serviceEvent == null) {

                    serviceEvent = new Mercury.Client.Core.Population.PopulationEvents.PopulationServiceEvent (MercuryApplication);

                    Session[SessionCachePrefix + "ServiceEvent"] = serviceEvent;

                }

                return serviceEvent;

            }

            set { Session[SessionCachePrefix + "ServiceEvent"] = value; }

        }

        private Mercury.Client.Core.Population.PopulationEvents.PopulationTriggerEvent EditTriggerEvent {

            get {

                Mercury.Client.Core.Population.PopulationEvents.PopulationTriggerEvent serviceEvent = (Mercury.Client.Core.Population.PopulationEvents.PopulationTriggerEvent) Session[SessionCachePrefix + "TriggerEvent"];

                if (serviceEvent == null) {

                    serviceEvent = new Mercury.Client.Core.Population.PopulationEvents.PopulationTriggerEvent (MercuryApplication);

                    Session[SessionCachePrefix + "TriggerEvent"] = serviceEvent;

                }

                return serviceEvent;

            }

            set { Session[SessionCachePrefix + "TriggerEvent"] = value; }

        }

        private Mercury.Client.Core.Population.PopulationEvents.PopulationActivityEvent EditActivityEvent {

            get {

                Mercury.Client.Core.Population.PopulationEvents.PopulationActivityEvent serviceEvent = (Mercury.Client.Core.Population.PopulationEvents.PopulationActivityEvent) Session[SessionCachePrefix + "ActivityEvent"];

                if (serviceEvent == null) {

                    serviceEvent = new Mercury.Client.Core.Population.PopulationEvents.PopulationActivityEvent (MercuryApplication);

                    Session[SessionCachePrefix + "ActivityEvent"] = serviceEvent;

                }

                return serviceEvent;

            }

            set { Session[SessionCachePrefix + "ActivityEvent"] = value; }

        }

        #endregion 


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            Int64 forPopulationId = 0;


            if (MercuryApplication == null) { return; }


            if ((!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.PopulationReview))
                && (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.PopulationManage)))

                { Response.Redirect ("/PermissionDenied.aspx", true); return; }

            if (!Page.IsPostBack) {

                #region Initial Page Load

                if (Request.QueryString["PopulationId"] != null) {

                    forPopulationId = Int64.Parse (Request.QueryString["PopulationId"]);

                }

                if (forPopulationId != 0) {

                    population = MercuryApplication.PopulationGet (forPopulationId, false);

                    if (population == null) {

                        population = new Mercury.Client.Core.Population.Population (MercuryApplication);

                    }

                    Page.Title = "Population - " + population.Name;

                }

                else {

                    population = new Mercury.Client.Core.Population.Population (MercuryApplication);

                }

                InitializeAll ();

                Session[SessionCachePrefix + "Population"] = population;

                Session[SessionCachePrefix + "PopulationUnmodified"] = population.Copy ();

                #endregion

            } // Initial Page Load

            else { // Postback

                population = (Mercury.Client.Core.Population.Population) Session[SessionCachePrefix + "Population"];

            }

            ApplySecurity ();

            return;

        }

        protected void Page_Unload (object sender, EventArgs e) {

            MercuryApplication.ApplicationClientClose ();

            return;

        }

        #endregion


        #region Initialization

        protected void InitializeAll () {

            InitializeGeneralPage ();


            // All Initializations

            InitializeCriteriaEnrollmentGrid ();

            InitializeCriteriaEnrollmentSelection ();

            InitializeCriteriaDemographic ();

            InitializeCriteriaDemographicSelection ();

            InitializeCriteriaGeographicGrid ();

            InitializeCriteriaGeographicStateCityCountySelection ();

            InitializeCriteriaEvent ();

            InitializePopulationEventsGrid ();

            InitializeServiceEventsGrid ();

            InitializeServiceEventsSelection ();

            InitializeServiceEventsThresholds ();

            InitializeTriggerEventsGrid ();

            InitializeTriggerEventsSelection ();

            InitializeTriggerEventsActionParametersGrid ();

            InitializeActivityEventsGrid ();

            InitializeActivityEventsSelection ();

            InitializeActivityEventsActionParametersGrid ();

            InitializeExtendedPropertiesGrid ();

            return;

        }

        protected void InitializeGeneralPage () {

            if (!String.IsNullOrEmpty (population.Name)) { Page.Title = "Population - " + population.Name; } else { Page.Title = "New Population"; }

            PopulationName.Text = population.Name;

            PopulationDescription.Text = population.Description;

            foreach (Client.Core.Population.PopulationType currentPopulationType in MercuryApplication.PopulationTypesAvailable (false)) {

                if (currentPopulationType.Enabled) {

                    PopulationTypeSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentPopulationType.Name, currentPopulationType.Id.ToString ()));

                }

            }

            PopulationTypeSelection.SelectedValue = population.PopulationTypeId.ToString ();

            PopulationInitialAnchorDate.SelectedValue = ((Int32) population.InitialAnchorDate).ToString ();

            PopulationAllowProspective.Checked = population.AllowProspective;

            PopulationEnabled.Checked = population.Enabled;

            PopulationVisible.Checked = population.Visible;


            PopulationCreateAuthorityName.Text = population.CreateAccountInfo.SecurityAuthorityName;

            PopulationCreateAccountId.Text = population.CreateAccountInfo.UserAccountId;

            PopulationCreateAccountName.Text = population.CreateAccountInfo.UserAccountName;

            PopulationCreateDate.MinDate = DateTime.MinValue;

            PopulationCreateDate.SelectedDate = population.CreateAccountInfo.ActionDate;


            PopulationModifiedAuthorityName.Text = population.ModifiedAccountInfo.SecurityAuthorityName;

            PopulationModifiedAccountId.Text = population.ModifiedAccountInfo.UserAccountId;

            PopulationModifiedAccountName.Text = population.ModifiedAccountInfo.UserAccountName;

            PopulationModifiedDate.MinDate = DateTime.MinValue;

            PopulationModifiedDate.SelectedDate = population.ModifiedAccountInfo.ActionDate;

            return;

        }

        protected void InitializeCriteriaEnrollmentGrid () {

            System.Data.DataTable criteriaTable = new DataTable ();

            criteriaTable.Columns.Add ("CriteriaId");

            criteriaTable.Columns.Add ("InsurerName");

            criteriaTable.Columns.Add ("ProgramName");

            criteriaTable.Columns.Add ("BenefitPlanName");


            foreach (Mercury.Client.Core.Population.PopulationCriteria.PopulationCriteriaEnrollment criteria in population.EnrollmentCriteria) {

                String insurerName = MercuryApplication.CoreObjectGetNameById ("Insurer", criteria.InsurerId);

                String programName = MercuryApplication.CoreObjectGetNameById  ("Program", criteria.ProgramId);

                String benefitPlanName = MercuryApplication.CoreObjectGetNameById ("BenefitPlan", criteria.BenefitPlanId);


                if (String.IsNullOrEmpty (programName)) { programName = "* All Programs"; }

                if (String.IsNullOrEmpty (benefitPlanName)) { benefitPlanName = "* All Benefit Plans"; }

                criteriaTable.Rows.Add (criteria.Id.ToString (), insurerName, programName, benefitPlanName);

            }

            CriteriaEnrollmentGrid.DataSource = criteriaTable;

            CriteriaEnrollmentGrid.DataBind ();

            return;

        }

        protected void InitializeCriteriaEnrollmentSelection () {

            System.Collections.Generic.Dictionary<Int64, String> insurerReference = MercuryApplication.CoreObjectDictionary ("Insurer", true);

            System.Collections.Generic.Dictionary<Int64, String> programReference = new System.Collections.Generic.Dictionary<Int64, String> ();

            System.Collections.Generic.Dictionary<Int64, String> benefitPlanReference = new System.Collections.Generic.Dictionary<Int64, String> ();


            CriteriaEnrollmentInsurerSelection.Items.Clear ();

            CriteriaEnrollmentProgramSelection.Items.Clear ();

            CriteriaEnrollmentBenefitPlanSelection.Items.Clear ();


            if (insurerReference != null) {

                foreach (Int64 currentInsurerId in insurerReference.Keys) {

                    CriteriaEnrollmentInsurerSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (insurerReference[currentInsurerId], currentInsurerId.ToString ()));

                }

            }

            if (CriteriaEnrollmentInsurerSelection.SelectedItem != null) { 

                programReference = MercuryApplication.ProgramDictionaryByInsurer (Int64.Parse (CriteriaEnrollmentInsurerSelection.SelectedItem.Value), true);

            }

            CriteriaEnrollmentProgramSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* All Programs", "0"));

            if (programReference != null) {

                foreach (Int64 currentProgramId in programReference.Keys) {

                    CriteriaEnrollmentProgramSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (programReference[currentProgramId], currentProgramId.ToString ()));

                }

            }

            CriteriaEnrollmentBenefitPlanSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* All Benefit Plans", "0"));

            if (benefitPlanReference != null) {

                foreach (Int64 currentBenefitPlanId in benefitPlanReference.Keys) {

                    CriteriaEnrollmentBenefitPlanSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (benefitPlanReference[currentBenefitPlanId], currentBenefitPlanId.ToString ()));

                }

            }

            return;

        }

        protected void InitializeCriteriaDemographic () {

            System.Data.DataTable criteriaTable = new DataTable ();

            criteriaTable.Columns.Add ("CriteriaId");

            criteriaTable.Columns.Add ("Gender");

            criteriaTable.Columns.Add ("AgeMinimum");

            criteriaTable.Columns.Add ("AgeMaximum");

            criteriaTable.Columns.Add ("Ethnicity");


            foreach (Mercury.Client.Core.Population.PopulationCriteria.PopulationCriteriaDemographic criteria in population.DemographicCriteria) {

                String ethnicityName = MercuryApplication.CoreObjectGetNameById ("Ethnicity", criteria.EthnicityId, true);

                if (criteria.UseAgeCriteria) {

                    criteriaTable.Rows.Add (criteria.Id, criteria.Gender, criteria.AgeMinimum, criteria.AgeMaximum, ethnicityName);

                }

                else {

                    criteriaTable.Rows.Add (criteria.Id, criteria.Gender, String.Empty, String.Empty, ethnicityName);

                }

            }

            CriteriaDemographicGrid.DataSource = criteriaTable;

            CriteriaDemographicGrid.DataBind ();


            // TODO: Ethnicity


            return;

        }

        protected void InitializeCriteriaDemographicSelection () {

            System.Collections.Generic.Dictionary<Int64, String> ethnicityReference = MercuryApplication.CoreObjectDictionary ("Ethnicity", true);

            CriteriaDemographicEthnicitySelection.Items.Clear ();

            CriteriaDemographicEthnicitySelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* All Ethnicities", "0"));

            foreach (Int64 currentEthnicityId in ethnicityReference.Keys) {

                String ethnicityName = ethnicityReference[currentEthnicityId];

                ethnicityName = ethnicityName.Replace ("/", " / ");

                if (ethnicityName.Length > 60) { ethnicityName = ethnicityName.Substring (0, 56) + " ..."; }

                CriteriaDemographicEthnicitySelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (ethnicityName, currentEthnicityId.ToString ()));

            }
            
            return; 

        }

        protected void InitializeCriteriaGeographicGrid () {

            System.Data.DataTable criteriaTable = new DataTable ();

            criteriaTable.Columns.Add ("CriteriaId");

            criteriaTable.Columns.Add ("State");

            criteriaTable.Columns.Add ("City");

            criteriaTable.Columns.Add ("County");

            criteriaTable.Columns.Add ("ZipCode");


            foreach (Mercury.Client.Core.Population.PopulationCriteria.PopulationCriteriaGeographic criteria in population.GeographicCriteria) {

                criteriaTable.Rows.Add (criteria.Id, criteria.State, criteria.City, criteria.County, criteria.ZipCode);

            }

            CriteriaGeographicGrid.DataSource = criteriaTable;

            CriteriaGeographicGrid.DataBind ();

            return;

        }

        protected void InitializeCriteriaGeographicStateCityCountySelection () {

            if (CriteriaGeographicStateSelection.Items.Count == 0) { 

                CriteriaGeographicStateSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* All", String.Empty));

                foreach (String currentState in MercuryApplication.StateReference (true)) { 

                    CriteriaGeographicStateSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentState, currentState));

                }

                CriteriaGeographicCitySelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* All Cities", String.Empty));

                CriteriaGeographicCountySelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* All Counties", String.Empty));

            }

            return;

        }

        protected void InitializeCriteriaEvent () {

            System.Data.DataTable criteriaTable = new DataTable ();

            criteriaTable.Columns.Add ("CriteriaId");

            criteriaTable.Columns.Add ("EventType");

            criteriaTable.Columns.Add ("ServiceId");

            criteriaTable.Columns.Add ("ServiceName");


            foreach (Mercury.Client.Core.Population.PopulationCriteria.PopulationCriteriaEvent criteria in population.EventCriteria) {

                Client.Core.MedicalServices.Service medicalService = MercuryApplication.MedicalServiceGet (criteria.ServiceId, false);

                if (medicalService != null) {

                    criteriaTable.Rows.Add (criteria.Id, criteria.EventType.ToString (), criteria.ServiceId.ToString (), medicalService.Name);

                }

                else {

                    criteriaTable.Rows.Add (criteria.Id, criteria.EventType.ToString (), criteria.ServiceId.ToString (), "Unable to retreive Service.");

                }

            }

            CriteriaEventGrid.DataSource = criteriaTable;

            CriteriaEventGrid.DataBind ();
            

            CriteriaEventMedicalServiceSelection.Items.Clear ();

            foreach (Mercury.Server.Application.SearchResultMedicalServiceHeader serviceHeader in MercuryApplication.MedicalServiceHeadersGet (false)) {

                // SERVICE MUST BE ENABLED FOR SELECTION (VISIBILTIY DOES NOT MATTER, THAT IS UI ONLY)

                if (serviceHeader.Enabled) {

                    CriteriaEventMedicalServiceSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (serviceHeader.Name, serviceHeader.Id.ToString ()));

                }

            }

            return;

        }

        protected void InitializePopulationEventsGrid () {

            System.Data.DataTable eventTable = new DataTable ();

            eventTable.Columns.Add ("EventName");

            eventTable.Columns.Add ("Action");

            foreach (String eventName in population.Events.Keys) {

                eventTable.Rows.Add (eventName, population.Events[eventName].Description);

            }

            Session[SessionCachePrefix + "PopulationEventsGrid.EventTable"] = eventTable;

            PopulationEventsGrid.DataSource = eventTable;

            PopulationEventsGrid.DataBind ();


            System.Data.DataTable parameterTable = new DataTable ();

            parameterTable.Columns.Add ("EventName");

            parameterTable.Columns.Add ("ParameterName");

            parameterTable.Columns.Add ("ParameterValue");


            Session[SessionCachePrefix + "PopulationEventsGrid.ParameterTable"] = parameterTable;

            PopulationEventsGrid.MasterTableView.DetailTables[0].DataSource = parameterTable;

            PopulationEventsGrid.MasterTableView.DetailTables[0].DataBind ();

            return;

        }

        protected void InitializeServiceEventsGrid () {

            System.Data.DataTable eventTable = new DataTable ();
            
            eventTable.Columns.Add ("ServiceEventId");

            eventTable.Columns.Add ("ServiceId");

            eventTable.Columns.Add ("ServiceName");
            
            eventTable.Columns.Add ("ServiceType");

            eventTable.Columns.Add ("ExclusionServiceId");

            eventTable.Columns.Add ("ExclusionServiceName");

            eventTable.Columns.Add ("Anchor");

            eventTable.Columns.Add ("Schedule");
            
            eventTable.Columns.Add ("Thresholds");

            foreach (Mercury.Client.Core.Population.PopulationEvents.PopulationServiceEvent currentServiceEvent in population.ServiceEvents) {

                Mercury.Client.Core.MedicalServices.Service service = MercuryApplication.MedicalServiceGet (currentServiceEvent.ServiceId, false);

                Mercury.Client.Core.MedicalServices.Service exclusionService = MercuryApplication.MedicalServiceGet (currentServiceEvent.ExclusionServiceId, false);

                String serviceName = String.Empty;

                String serviceType = String.Empty;

                String exclusionName = String.Empty;

                if (service != null) { serviceName = service.Name; serviceType = service.ServiceType.ToString (); }

                if (exclusionService != null) { exclusionName = exclusionService.Name; }

                eventTable.Rows.Add (currentServiceEvent.Id, currentServiceEvent.ServiceId, serviceName, serviceType, currentServiceEvent.ExclusionServiceId, exclusionName, currentServiceEvent.AnchorText, currentServiceEvent.ScheduleText, currentServiceEvent.ThresholdText (MercuryApplication));

            }

            ServiceEventsGrid.DataSource = eventTable;

            ServiceEventsGrid.DataBind ();

            return;

        }

        protected void InitializeServiceEventsSelection () {

            List<Mercury.Server.Application.SearchResultMedicalServiceHeader> availableServices = MercuryApplication.MedicalServiceHeadersGet (false);



            ServiceEventsServiceSelection.Items.Clear ();

            foreach (Mercury.Server.Application.SearchResultMedicalServiceHeader serviceHeader in availableServices) {

                // SERVICE MUST BE ENABLED FOR SELECTION (VISIBILTIY DOES NOT MATTER, THAT IS UI ONLY)

                if (serviceHeader.Enabled) {

                    ServiceEventsServiceSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (serviceHeader.Name, serviceHeader.Id.ToString ()));

                }

            }

            ServiceEventsExclusionSelection.Items.Clear ();

            ServiceEventsExclusionSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("None", "0"));

            foreach (Mercury.Server.Application.SearchResultMedicalServiceHeader serviceHeader in availableServices) {
                
                // SERVICE MUST BE ENABLED FOR SELECTION (VISIBILTIY DOES NOT MATTER, THAT IS UI ONLY)

                if (serviceHeader.Enabled) {

                    ServiceEventsExclusionSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (serviceHeader.Name, serviceHeader.Id.ToString ()));

                }

            }

            return;

        }

        protected void InitializeServiceEventsThresholds () {

            System.Data.DataTable thresholdTable = new DataTable ();

            thresholdTable.Columns.Add ("ThresholdId");

            thresholdTable.Columns.Add ("RelativeValue");

            thresholdTable.Columns.Add ("RelativeQualifier");

            thresholdTable.Columns.Add ("Status");

            thresholdTable.Columns.Add ("Action");


            Session[SessionCachePrefix + "ServiceEventsThresholdsGrid.ThresholdTable"] = thresholdTable;

            ServiceEventsThresholdsGrid.DataSource = thresholdTable;

            ServiceEventsThresholdsGrid.DataBind ();


            System.Data.DataTable parameterTable = (DataTable) Session[SessionCachePrefix + "ServiceEventsThresholdsGrid.ParameterTable"];

            parameterTable = new DataTable ();

            parameterTable.Columns.Add ("ThresholdKey");

            parameterTable.Columns.Add ("ThresholdId");

            parameterTable.Columns.Add ("ParameterName");

            parameterTable.Columns.Add ("ParameterValue");


            Session[SessionCachePrefix + "ServiceEventsThresholdsGrid.ParameterTable"] = parameterTable;

            ServiceEventsThresholdsGrid.MasterTableView.DetailTables[0].DataSource = parameterTable;

            ServiceEventsThresholdsGrid.MasterTableView.DetailTables[0].DataBind ();


            return;

        }

        protected void InitializeTriggerEventsGrid () {

            System.Data.DataTable eventTable = new DataTable ();

            eventTable.Columns.Add ("TriggerEventId");

            eventTable.Columns.Add ("EventType");

            eventTable.Columns.Add ("Trigger");

            eventTable.Columns.Add ("ProblemStatement");

            eventTable.Columns.Add ("Action");

            foreach (Mercury.Client.Core.Population.PopulationEvents.PopulationTriggerEvent currentTriggerEvent in population.TriggerEvents) {

                String triggerText = currentTriggerEvent.TriggerText;

                String actionText = currentTriggerEvent.Action.Description;

                eventTable.Rows.Add (
                    
                    currentTriggerEvent.Id, 
                    
                    currentTriggerEvent.EventType.ToString (), 
                    
                triggerText,

                (currentTriggerEvent.ProblemStatementId == 0) ? "* Not Assigned" : MercuryApplication.ProblemStatementGet (currentTriggerEvent.ProblemStatementId, true).ClassificationWithName,
                    
                    currentTriggerEvent.Action.Description
                    
                    );

            }

            TriggerEventsGrid.DataSource = eventTable;

            TriggerEventsGrid.DataBind ();

            return;

        }

        protected void InitializeTriggerEventsSelection () {

            TriggerEventsServiceSelection.Items.Clear ();

            foreach (Mercury.Server.Application.SearchResultMedicalServiceHeader serviceHeader in MercuryApplication.MedicalServiceHeadersGet (true)) {

                if (serviceHeader.Enabled) {

                    TriggerEventsServiceSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (serviceHeader.Name, serviceHeader.Id.ToString ()));

                }

            }

            TriggerEventsMetricSelection.Items.Clear ();

            foreach (Client.Core.Metrics.Metric currentMetric in MercuryApplication.MetricsAvailable (false)) {

                if (currentMetric.Enabled) {

                    TriggerEventsMetricSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentMetric.MetricType.ToString () + ": " + currentMetric.Name, ((Int32) currentMetric.MetricType).ToString () + "|" + currentMetric.Id.ToString ()));

                }

            }

            TriggerEventsAuthorizedServiceSelection.Items.Clear ();

            foreach (Client.Core.AuthorizedServices.AuthorizedService currentAuthorizedService in MercuryApplication.AuthorizedServicesAvailable (false)) {

                if (currentAuthorizedService.Enabled) {

                    TriggerEventsAuthorizedServiceSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentAuthorizedService.Name, currentAuthorizedService.Id.ToString ()));

                }

            }


            TriggerEventsActionSelection.Items.Clear ();

            TriggerEventsActionSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* No Action Selected", "0"));

            foreach (Mercury.Server.Application.Action currentAction in MercuryApplication.ActionsAvailable (false)) {

                TriggerEventsActionSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentAction.Name, currentAction.Id.ToString ()));

            }


            InitializeTriggerEventsProblemStatementSelection (String.Empty, String.Empty);

            return;

        }

        protected void InitializeTriggerEventsProblemStatementSelection (String itemText, String itemValue) {

            TriggerEventsProblemStatementSelection.Items.Clear ();

            TriggerEventsProblemStatementSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (itemText, itemValue));

            return;

        }

        protected void InitializeTriggerEventsActionParametersGrid () {

            System.Data.DataTable parameterTable = new DataTable ();

            parameterTable.Columns.Add ("ParameterName");

            parameterTable.Columns.Add ("ParameterValue");

            foreach (String parameterName in EditTriggerEvent.Action.ActionParameters.Keys) {

                parameterTable.Rows.Add (parameterName, EditTriggerEvent.Action.ActionParameters[parameterName].ValueDescription);

            }


            Session[SessionCachePrefix + "TriggerEventsActionParametersGrid.ParameterTable"] = parameterTable;

            TriggerEventsActionParametersGrid.DataSource = parameterTable;

            TriggerEventsActionParametersGrid.DataBind ();

            return;

        }

        protected void InitializeActivityEventsGrid () {

            System.Data.DataTable eventTable = new DataTable ();

            eventTable.Columns.Add ("ActivityEventId");

            eventTable.Columns.Add ("Schedule");

            eventTable.Columns.Add ("Action");

            foreach (Mercury.Client.Core.Population.PopulationEvents.PopulationActivityEvent currentActivity in population.ActivityEvents) {

                eventTable.Rows.Add (currentActivity.Id, currentActivity.Description, currentActivity.Action.Description);

            }

            ActivityEventsGrid.DataSource = eventTable;

            ActivityEventsGrid.DataBind ();

            return;

        }

        protected void InitializeActivityEventsSelection () {

            ActivityEventsActionSelection.Items.Clear ();

            ActivityEventsActionSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* No Action Selected", "0"));

            foreach (Mercury.Server.Application.Action currentAction in MercuryApplication.ActionsAvailable (false)) {

                ActivityEventsActionSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentAction.Name, currentAction.Id.ToString ()));

            }

            return;

        }

        protected void InitializeActivityEventsActionParametersGrid () {

            System.Data.DataTable parameterTable = new DataTable ();

            parameterTable.Columns.Add ("ParameterName");

            parameterTable.Columns.Add ("ParameterValue");

            foreach (String parameterName in EditActivityEvent.Action.ActionParameters.Keys) {

                parameterTable.Rows.Add (parameterName, EditActivityEvent.Action.ActionParameters[parameterName].ValueDescription);

            }


            Session[SessionCachePrefix + "ActivityEventsActionParametersGrid.ParameterTable"] = parameterTable;

            ActivityEventsActionParametersGrid.DataSource = parameterTable;

            ActivityEventsActionParametersGrid.DataBind ();

            return;

        }

        protected void InitializeExtendedPropertiesGrid () {

            System.Data.DataTable propertiesTable = new DataTable ();

            propertiesTable.Columns.Add ("ExtendedPropertyName");

            propertiesTable.Columns.Add ("ExtendedPropertyValue");

            foreach (String currentPropertyName in population.ExtendedProperties.Keys) {

                propertiesTable.Rows.Add (

                    currentPropertyName,

                    population.ExtendedProperties[currentPropertyName]

                );

            }

            ExtendedPropertiesGrid.DataSource = propertiesTable;

            ExtendedPropertiesGrid.DataBind ();

            return;

        }

        protected void ApplySecurity () {

            Boolean hasManagePermission = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.PopulationManage);

            PopulationName.ReadOnly = !hasManagePermission;

            PopulationDescription.ReadOnly = !hasManagePermission;

            PopulationTypeSelection.Enabled = hasManagePermission;

            PopulationInitialAnchorDate.Enabled = hasManagePermission;

            PopulationEnabled.Enabled = hasManagePermission;

            PopulationVisible.Enabled = hasManagePermission;

            ButtonCancel.Visible = hasManagePermission;

            ButtonApply.Visible = hasManagePermission;

            return;

        }

        #endregion


        #region Criteria Control Events

        protected void CriteriaEnrollmentInsurerSelection_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            System.Collections.Generic.Dictionary<Int64, String> programReference = new System.Collections.Generic.Dictionary<Int64, String> ();


            if (MercuryApplication == null) { return; }


            if (CriteriaEnrollmentInsurerSelection.SelectedItem == null) { return; }


            CriteriaEnrollmentProgramSelection.Items.Clear ();
    
            programReference = MercuryApplication.ProgramDictionaryByInsurer (Int64.Parse (CriteriaEnrollmentInsurerSelection.SelectedItem.Value), true);

            CriteriaEnrollmentProgramSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* All Programs", "0"));

            foreach (Int64 currentProgramId in programReference.Keys) {

                CriteriaEnrollmentProgramSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (programReference[currentProgramId], currentProgramId.ToString ()));

            }

            return;

        }

        protected void CriteriaEnrollmentProgramSelection_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            System.Collections.Generic.Dictionary<Int64, String> benefitPlanReference = new System.Collections.Generic.Dictionary<Int64, String> ();


            if (MercuryApplication == null) { return; }


            if (CriteriaEnrollmentProgramSelection.SelectedItem == null) { return; }


            CriteriaEnrollmentBenefitPlanSelection.Items.Clear ();

            benefitPlanReference = new Dictionary<long, string> ();

            foreach (Client.Core.Insurer.BenefitPlan currentBenefitPlan in MercuryApplication.BenefitPlansAvailableByProgram (Int64.Parse (CriteriaEnrollmentProgramSelection.SelectedItem.Value), true)) {

                if ((currentBenefitPlan.IsActive) && (currentBenefitPlan.Enabled)) {

                    benefitPlanReference.Add (currentBenefitPlan.Id, currentBenefitPlan.Name);

                }

            }


            CriteriaEnrollmentBenefitPlanSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* All Benefit Plans", "0"));

            foreach (Int64 currentBenefitPlanId in benefitPlanReference.Keys) {
                
                CriteriaEnrollmentBenefitPlanSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (benefitPlanReference[currentBenefitPlanId], currentBenefitPlanId.ToString ()));

            }

            return;

        }

        protected void CriteriaGeographicStateSelection_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            System.Collections.Generic.Dictionary<Int64, String> programReference = new System.Collections.Generic.Dictionary<Int64, String> ();


            if (MercuryApplication == null) { return; }


            if (CriteriaGeographicStateSelection.SelectedItem == null) { return; }


            if (CriteriaGeographicStateSelection.SelectedValue == String.Empty) {

                InitializeCriteriaGeographicStateCityCountySelection ();

            }

            else { 

                CriteriaGeographicCitySelection.Items.Clear ();

                CriteriaGeographicCitySelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* All Cities", String.Empty));

                foreach (Mercury.Server.Application.CityStateZipCodeView currentCity in MercuryApplication.CityReferenceByState (CriteriaGeographicStateSelection.SelectedValue, true)) {

                    CriteriaGeographicCitySelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentCity.City, currentCity.City));

                }


                CriteriaGeographicCountySelection.Items.Clear ();

                CriteriaGeographicCountySelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* All Counties", String.Empty));

                foreach (String currentCounty in MercuryApplication.CountyReferenceByState (CriteriaGeographicStateSelection.SelectedValue)) {

                    CriteriaGeographicCountySelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentCounty, currentCounty));

                }

            }

            return;

        }

        #endregion


        #region Add Criteria Events

        protected void ButtonAddCriteriaEnrollment_OnClick (Object sender, EventArgs eventArgs) {

            Boolean existingCriteriaFound = false;

            Client.Core.Population.PopulationCriteria.PopulationCriteriaEnrollment criteria = null;

            Mercury.Server.Application.BooleanResponse validationResponse;


            if (MercuryApplication == null) { return; }


            criteria = new Client.Core.Population.PopulationCriteria.PopulationCriteriaEnrollment (MercuryApplication);

            criteria.PopulationId = population.Id;

            if (CriteriaEnrollmentInsurerSelection.SelectedItem != null) { criteria.InsurerId = Int64.Parse (CriteriaEnrollmentInsurerSelection.SelectedItem.Value); }

            if (CriteriaEnrollmentProgramSelection.SelectedItem != null) { criteria.ProgramId = Int64.Parse (CriteriaEnrollmentProgramSelection.SelectedItem.Value); }

            if (CriteriaEnrollmentBenefitPlanSelection.SelectedItem != null) { criteria.BenefitPlanId = Int64.Parse (CriteriaEnrollmentBenefitPlanSelection.SelectedItem.Value); }

            // TODO: Validation Response

            validationResponse = new Mercury.Server.Application.BooleanResponse ();

            validationResponse.Result = true;


            SaveResponseLabel.Text = String.Empty;

            if (validationResponse.Result) {

                existingCriteriaFound = false;

                foreach (Client.Core.Population.PopulationCriteria.PopulationCriteriaEnrollment currentCriteria in population.EnrollmentCriteria) {

                    if (currentCriteria.IsEqual (criteria)) { 
                        
                        existingCriteriaFound = true; 

                        SaveResponseLabel.Text = "Duplicate Criteria Found.";

                        break;
                    
                    }

                }

                if (!existingCriteriaFound) {

                    population.EnrollmentCriteria.Add (criteria);

                }

            }

            else {

                if (validationResponse.HasException) {

                    SaveResponseLabel.Text = validationResponse.Exception.Message;

                }

                else {

                    SaveResponseLabel.Text = "Unable to validate the Criteria.";

                }

            }

            InitializeCriteriaEnrollmentGrid ();

            return;

        }

        protected void CriteriaEnrollmentGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            Int32 deleteIndex = eventArgs.Item.DataSetIndex;

            population.EnrollmentCriteria.RemoveAt (deleteIndex);

            InitializeCriteriaEnrollmentGrid ();

            return;

        }

        protected void ButtonAddCriteriaDemographic_OnClick (Object sender, EventArgs eventArgs) {

            Boolean existingCriteriaFound = false;

            Client.Core.Population.PopulationCriteria.PopulationCriteriaDemographic criteria = null;

            Mercury.Server.Application.BooleanResponse validationResponse;

            Int32 ageValue;


            if (MercuryApplication == null) { return; }


            criteria = new Mercury.Client.Core.Population.PopulationCriteria.PopulationCriteriaDemographic (MercuryApplication);

            criteria.PopulationId = population.Id;

            criteria.Gender = (Mercury.Server.Application.Gender) (Int32.Parse (CriteriaDemographicGender.SelectedItem.Value));

            if ((Int32.TryParse (CriteriaDemographicAgeMinimum.Text, out ageValue)) || (Int32.TryParse (CriteriaDemographicAgeMaximum.Text, out ageValue))) {

                criteria.UseAgeCriteria = true; 

                if (Int32.TryParse (CriteriaDemographicAgeMinimum.Text, out ageValue)) { criteria.AgeMinimum = ageValue; }

                if (Int32.TryParse (CriteriaDemographicAgeMaximum.Text, out ageValue)) { criteria.AgeMaximum = ageValue; }

            }

            else { criteria.UseAgeCriteria = false; }

            if (CriteriaDemographicEthnicitySelection.SelectedItem != null) { criteria.EthnicityId = Int64.Parse (CriteriaDemographicEthnicitySelection.SelectedItem.Value); }

            // TODO: Validation Response

            validationResponse = new Mercury.Server.Application.BooleanResponse ();

            validationResponse.Result = true;


            SaveResponseLabel.Text = String.Empty;

            if (validationResponse.Result) {

                existingCriteriaFound = false;

                foreach (Client.Core.Population.PopulationCriteria.PopulationCriteriaDemographic currentCriteria in population.DemographicCriteria) {

                    if (currentCriteria.IsEqual (criteria)) {

                        existingCriteriaFound = true;

                        SaveResponseLabel.Text = "Duplicate Criteria Found.";

                        break; 

                    }

                }

                if (!existingCriteriaFound) {

                    if ((criteria.UseAgeCriteria) && (criteria.AgeMinimum <= criteria.AgeMaximum)) {

                        population.DemographicCriteria.Add (criteria);

                    }

                    else {

                        SaveResponseLabel.Text = "Invalid Age Range.";

                    }

                }

            }

            else {

                if (validationResponse.HasException) {

                    SaveResponseLabel.Text = validationResponse.Exception.Message;

                }

                else {

                    SaveResponseLabel.Text = "Unable to validate the Criteria.";

                }

            }

            InitializeCriteriaDemographic ();

            return;

        }

        protected void CriteriaDemographicGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            Int32 deleteIndex = eventArgs.Item.DataSetIndex;

            population.DemographicCriteria.RemoveAt (deleteIndex);

            InitializeCriteriaDemographic ();

            return;

        }

        protected void ButtonAddCriteriaEvent_OnClick (Object sender, EventArgs eventArgs) {

            Boolean existingCriteriaFound = false;

            Client.Core.Population.PopulationCriteria.PopulationCriteriaEvent criteria = null;

            Mercury.Server.Application.BooleanResponse validationResponse;


            if (MercuryApplication == null) { return; }


            criteria = new Mercury.Client.Core.Population.PopulationCriteria.PopulationCriteriaEvent (MercuryApplication);

            criteria.PopulationId = population.Id;

            if (CriteriaEventMedicalServiceSelection.SelectedItem == null) { return; }

            criteria.EventType = (Mercury.Server.Application.PopulationCriteriaEventType)(Int32.Parse (CriteriaEventType.SelectedItem.Value));

            criteria.ServiceId = Int64.Parse (CriteriaEventMedicalServiceSelection.SelectedItem.Value);


            // TODO: Validation Response

            validationResponse = new Mercury.Server.Application.BooleanResponse ();

            validationResponse.Result = true;


            SaveResponseLabel.Text = String.Empty;

            if (validationResponse.Result) {

                existingCriteriaFound = false;

                foreach (Client.Core.Population.PopulationCriteria.PopulationCriteriaEvent currentCriteria in population.EventCriteria) {

                    if (currentCriteria.IsEqual (criteria)) { 
                        
                        existingCriteriaFound = true;

                        SaveResponseLabel.Text = "Duplicate Criteria Found.";
                        
                        break; 
                    
                    }

                }

                if (!existingCriteriaFound) {

                    population.EventCriteria.Add (criteria);

                }

            }

            else {

                if (validationResponse.HasException) {

                    SaveResponseLabel.Text = validationResponse.Exception.Message;

                }

                else {

                    SaveResponseLabel.Text = "Unable to validate the Criteria.";

                }

            }

            InitializeCriteriaEvent ();

            return;

        }

        protected void CriteriaEventGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            Int32 deleteIndex = eventArgs.Item.DataSetIndex;

            population.EventCriteria.RemoveAt (deleteIndex);

            InitializeCriteriaEvent ();

            return;

        }

        protected void ButtonAddCriteriaGeographic_OnClick (Object sender, EventArgs eventArgs) { 
            
            Boolean existingCriteriaFound = false;

            Client.Core.Population.PopulationCriteria.PopulationCriteriaGeographic criteria = null;

            Mercury.Server.Application.BooleanResponse validationResponse;


            if (MercuryApplication == null) { return; }


            criteria = new Mercury.Client.Core.Population.PopulationCriteria.PopulationCriteriaGeographic (MercuryApplication);

            criteria.PopulationId = population.Id;

            criteria.State = CriteriaGeographicStateSelection.SelectedValue;

            criteria.City = CriteriaGeographicCitySelection.SelectedValue;

            criteria.County = CriteriaGeographicCountySelection.SelectedValue;

            criteria.ZipCode = CriteriaGeographicZipCode.Text;

            // TODO: Validation Response

            validationResponse = new Mercury.Server.Application.BooleanResponse ();

            validationResponse.Result = true;


            Int64 zipCodeNumeric = 0;

            if ((criteria.State == String.Empty) && (criteria.City == String.Empty) && (criteria.County == String.Empty) && (criteria.ZipCode == String.Empty)) {

                validationResponse.Result = false;

                validationResponse.HasException = true;

                validationResponse.Exception = new Mercury.Server.Application.ServiceException ();

                validationResponse.Exception.Message = "No valid criteria selected.";

            }

            else if ((!String.IsNullOrEmpty (criteria.ZipCode) && ((criteria.ZipCode.Length != 5) || (!Int64.TryParse (criteria.ZipCode, out zipCodeNumeric))))) {

                validationResponse.Result = false;

                validationResponse.HasException = true;

                validationResponse.Exception = new Mercury.Server.Application.ServiceException ();

                validationResponse.Exception.Message = "Invalid Zip Code.";

            }


            SaveResponseLabel.Text = String.Empty;

            if (validationResponse.Result) {

                existingCriteriaFound = false;

                foreach (Client.Core.Population.PopulationCriteria.PopulationCriteriaGeographic currentCriteria in population.GeographicCriteria) {

                    if (currentCriteria.IsEqual (criteria)) { 
                        
                        existingCriteriaFound = true;

                        SaveResponseLabel.Text = "Duplicate Criteria Found.";

                        break; 
                    
                    }

                }

                if (!existingCriteriaFound) {

                    population.GeographicCriteria.Add (criteria);

                }

                else { 

                    SaveResponseLabel.Text = "Duplicate Criteria Found.";

                }

            }

            else {

                if (validationResponse.HasException) {

                    SaveResponseLabel.Text = validationResponse.Exception.Message;

                }

                else {

                    SaveResponseLabel.Text = "Unable to validate the Criteria.";

                }

            }

            InitializeCriteriaGeographicGrid ();

            return;
        }

        protected void CriteriaGeographicGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            Int32 deleteIndex = eventArgs.Item.DataSetIndex;

            population.GeographicCriteria.RemoveAt (deleteIndex);

            InitializeCriteriaGeographicGrid ();

            return;

        }

        #endregion


        #region Population Events

        protected void PopulationEventsGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            switch (eventArgs.RebindReason) {

                case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                    System.Data.DataTable eventTable = (DataTable) Session[SessionCachePrefix + "PopulationEventsGrid.EventTable"];

                    eventTable.Rows.Clear ();

                    foreach (String eventName in population.Events.Keys) {

                        eventTable.Rows.Add (eventName, population.Events[eventName].Description);

                    }

                    PopulationEventsGrid.DataSource = eventTable;

                    break;

                default:

                    if ((eventArgs.RebindReason & Telerik.Web.UI.GridRebindReason.DetailTableBinding) == Telerik.Web.UI.GridRebindReason.DetailTableBinding) {

                        System.Data.DataTable parameterTable = (DataTable) Session[SessionCachePrefix + "PopulationEventsGrid.ParameterTable"];

                        parameterTable.Rows.Clear ();

                        foreach (String eventName in population.Events.Keys) {

                            foreach (String parameterName in population.Events[eventName].ActionParameters.Keys) {

                                parameterTable.Rows.Add (eventName, parameterName, population.Events[eventName].ActionParameters[parameterName].ValueDescription);

                            }

                        }

                        PopulationEventsGrid.MasterTableView.DetailTables[0].DataSource = parameterTable;

                    }

                    break;

            }

            return;

        }

        protected void PopulationEventsGrid_OnItemDataBound (Object sender, Telerik.Web.UI.GridItemEventArgs eventArgs) {

            Telerik.Web.UI.RadComboBox populationEventActionSelection;

            Telerik.Web.UI.RadComboBox populationEventParameterValueSelection;

            Telerik.Web.UI.RadTextBox populationEventParameterFixedValue;

            System.Collections.Generic.Dictionary<String, String> bindingContexts;

            String eventName;

            String parameterName;


            if (MercuryApplication == null) { return; }

            if ((eventArgs.Item is Telerik.Web.UI.GridEditableItem) && (eventArgs.Item.IsInEditMode)) {

                Telerik.Web.UI.GridEditableItem editItem = (Telerik.Web.UI.GridEditableItem) eventArgs.Item;


                switch (eventArgs.Item.OwnerTableView.Name) {

                    case "PopulationEvent":

                        populationEventActionSelection = (Telerik.Web.UI.RadComboBox) editItem.FindControl ("PopulationEventActionSelection");

                        populationEventActionSelection.Items.Clear ();

                        populationEventActionSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* No Action Selected", "0"));

                        foreach (Mercury.Server.Application.Action currentAction in MercuryApplication.ActionsAvailable (false)) {

                            populationEventActionSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentAction.Name, currentAction.Id.ToString ()));

                        }

                        
                        if (eventArgs.Item.ItemIndex != -1) {

                            eventName = (String) editItem.OwnerTableView.DataKeyValues[eventArgs.Item.ItemIndex]["EventName"];

                            populationEventActionSelection.SelectedValue = population.Events[eventName].Id.ToString ();

                        }

                        break;


                    case "PopulationEventParameters":

                        eventName = (String) editItem.OwnerTableView.DataKeyValues[eventArgs.Item.ItemIndex]["EventName"];

                        parameterName = (String) editItem.OwnerTableView.DataKeyValues[eventArgs.Item.ItemIndex]["ParameterName"];

                        populationEventParameterValueSelection = (Telerik.Web.UI.RadComboBox) eventArgs.Item.FindControl ("PopulationEventParameterValueSelection");

                        populationEventParameterValueSelection.Items.Clear ();

                        if (!population.Events[eventName].ActionParameters[parameterName].Required) {

                            populationEventParameterValueSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* Not Assigned", "0"));

                        }

                        bindingContexts = population.ParameterValueSelection (population.Events[eventName].ActionParameters[parameterName].DataType);

                        foreach (String currentBindingContextName in bindingContexts.Keys) {

                            populationEventParameterValueSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentBindingContextName, bindingContexts[currentBindingContextName]));

                        }

                        populationEventParameterFixedValue = (Telerik.Web.UI.RadTextBox) eventArgs.Item.FindControl ("PopulationEventParameterFixedValue");

                        populationEventParameterFixedValue.Enabled = population.Events[eventName].ActionParameters[parameterName].AllowFixedValue;

                        populationEventParameterFixedValue.EmptyMessage = (population.Events[eventName].ActionParameters[parameterName].AllowFixedValue) ? String.Empty : "(Not Available)";

                        populationEventParameterFixedValue.Text = String.Empty;

                        break;

                }

            }

            return;

        }

        protected void PopulationEventsGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            String eventName;

            Telerik.Web.UI.RadComboBox populationEventActionSelection;

            Int64 actionId;

            String parameterName;

            Telerik.Web.UI.RadComboBox parameterValueSelection;

            Telerik.Web.UI.RadTextBox parameterFixedValue;


            if (MercuryApplication == null) { return; }

            switch (eventArgs.CommandName) {

                case Telerik.Web.UI.RadGrid.EditCommandName:

                    #region Edit Command

                    switch (eventArgs.Item.OwnerTableView.Name) {

                        case "PopulationEvent":

                            Telerik.Web.UI.GridEditCommandColumn editColumn = (Telerik.Web.UI.GridEditCommandColumn) PopulationEventsGrid.MasterTableView.GetColumn ("EditCommandColumn");

                            if (!editColumn.Visible) { editColumn.Visible = true; }

                            Telerik.Web.UI.GridEditableItem editItem = (Telerik.Web.UI.GridEditableItem) eventArgs.Item;

                            break;

                        case "PopulationEventParameters":

                            editColumn = (Telerik.Web.UI.GridEditCommandColumn) eventArgs.Item.OwnerTableView.GetColumn ("EditCommandColumn");

                            if (!editColumn.Visible) { editColumn.Visible = true; }

                            break;

                    }

                    #endregion

                    break;

                case Telerik.Web.UI.RadGrid.UpdateCommandName:

                    #region Update Command

                    Telerik.Web.UI.GridEditableItem updatedItem = (Telerik.Web.UI.GridEditableItem) eventArgs.Item;

                    switch (eventArgs.Item.OwnerTableView.Name) {

                        case "PopulationEvent":

                            eventName = (String) updatedItem.OwnerTableView.DataKeyValues[eventArgs.Item.ItemIndex]["EventName"];

                            populationEventActionSelection = (Telerik.Web.UI.RadComboBox) updatedItem.FindControl ("PopulationEventActionSelection");

                            actionId = Int64.Parse (populationEventActionSelection.SelectedValue);

                            population.Events[eventName] = MercuryApplication.ActionById (actionId);

                            if (population.Events[eventName] == null) { population.Events[eventName] = new Mercury.Client.Core.Action.Action (MercuryApplication); }

                            break;

                        case "PopulationEventParameters":

                            parameterValueSelection = (Telerik.Web.UI.RadComboBox) eventArgs.Item.FindControl ("PopulationEventParameterValueSelection");
                            
                            parameterFixedValue = (Telerik.Web.UI.RadTextBox) eventArgs.Item.FindControl ("PopulationEventParameterFixedValue");

                            if ((parameterValueSelection.SelectedItem != null) || (!String.IsNullOrEmpty (parameterFixedValue.Text))) {

                                eventName = (String) updatedItem.OwnerTableView.DataKeyValues[eventArgs.Item.ItemIndex]["EventName"];

                                parameterName = (String) ((Telerik.Web.UI.GridEditableItem) eventArgs.Item).OwnerTableView.DataKeyValues[eventArgs.Item.ItemIndex]["ParameterName"];


                                if ((population.Events[eventName].ActionParameters[parameterName].AllowFixedValue) && (!String.IsNullOrEmpty (parameterFixedValue.Text))) {

                                    population.Events[eventName].ActionParameters[parameterName].ValueType = Mercury.Server.Application.ActionParameterValueType.FixedValue;

                                    population.Events[eventName].ActionParameters[parameterName].Value = parameterFixedValue.Text;

                                    population.Events[eventName].ActionParameters[parameterName].ValueDescription = parameterFixedValue.Text;

                                }

                                else {

                                    population.Events[eventName].ActionParameters[parameterName].ValueType = Mercury.Server.Application.ActionParameterValueType.DataMapping;

                                    population.Events[eventName].ActionParameters[parameterName].Value = parameterValueSelection.SelectedItem.Value;

                                    population.Events[eventName].ActionParameters[parameterName].ValueDescription = parameterValueSelection.SelectedItem.Text;

                                }

                                if ((population.Events[eventName].Name == "Workflow") && (parameterName == "Workflow")) {

                                    population.Events[eventName].RebindActionParameters (MercuryApplication);

                                }

                            }

                            break;

                    }

                    #endregion

                    break;

            }


            return;

        }

        #endregion


        #region Service Events Control Events

        protected void ButtonAddUpdateServiceEvent_OnClick (Object sender, EventArgs eventArgs) {

            Boolean existingServiceEventFound = false;

            Client.Core.Population.PopulationEvents.PopulationServiceEvent newServiceEvent = null;

            Dictionary<String, String> validationResponse;

            SaveResponseLabel.Text = String.Empty;


            if (MercuryApplication == null) { return; }


            newServiceEvent = new Mercury.Client.Core.Population.PopulationEvents.PopulationServiceEvent (MercuryApplication);

            newServiceEvent.PopulationId = population.Id;

            if (ServiceEventsServiceSelection.SelectedItem != null) { newServiceEvent.ServiceId = Int64.Parse (ServiceEventsServiceSelection.SelectedItem.Value); }

            if (ServiceEventsExclusionSelection.SelectedItem != null) { newServiceEvent.ExclusionServiceId = Int64.Parse (ServiceEventsExclusionSelection.SelectedItem.Value); }

            if (ServiceEventsAnchorSelection.SelectedItem != null) { newServiceEvent.AnchorDate = (Mercury.Server.Application.PopulationServiceEventAnchorDate)Int32.Parse (ServiceEventsAnchorSelection.SelectedItem.Value); }

            if (ServiceEventsAnchorDateValue.Value.HasValue) { newServiceEvent.AnchorDateValue = Int32.Parse (ServiceEventsAnchorDateValue.Value.Value.ToString ()); }

            if (ServiceEventsScheduleValue.Value.HasValue) {

                newServiceEvent.ScheduleDateValue = Int32.Parse (ServiceEventsScheduleValue.Value.Value.ToString ());

                newServiceEvent.ScheduleDateQualifier = (Mercury.Server.Application.DateQualifier) Int32.Parse (ServiceEventsScheduleQualifier.SelectedItem.Value); 

            }

            newServiceEvent.Reoccurring = ServiceEventsReoccurring.Checked;


            foreach (Client.Core.Population.PopulationEvents.PopulationServiceEventThreshold currentThreshold in EditServiceEvent.SortedThresholds.Values) {

                newServiceEvent.Thresholds.Add (currentThreshold);

            }


            validationResponse = MercuryApplication.CoreObject_Validate ((Mercury.Server.Application.CoreObject) newServiceEvent.ToServerObject ());

            if (validationResponse.Count == 0) {

                existingServiceEventFound = false;

                foreach (Client.Core.Population.PopulationEvents.PopulationServiceEvent currentEvent in population.ServiceEvents) {

                    if ((currentEvent.ServiceId == newServiceEvent.ServiceId) && (newServiceEvent.AnchorDate != Mercury.Server.Application.PopulationServiceEventAnchorDate.PreviousServiceEvent)) {
                        
                        existingServiceEventFound = true; break; 
                
                    }

                    else if ((currentEvent.ServiceId == newServiceEvent.ServiceId) && (newServiceEvent.AnchorDate == Mercury.Server.Application.PopulationServiceEventAnchorDate.PreviousServiceEvent)) {

                        if ((currentEvent.AnchorDate == Mercury.Server.Application.PopulationServiceEventAnchorDate.PreviousServiceEvent) && (currentEvent.AnchorDateValue == newServiceEvent.AnchorDateValue)) {

                            existingServiceEventFound = true; break;

                        }

                    }

                }


                switch (((System.Web.UI.WebControls.Button) sender).ID) {

                    case "ButtonAddServiceEvent":

                        if (!existingServiceEventFound) {

                            population.ServiceEvents.Add (newServiceEvent);

                            SaveResponseLabel.Text = String.Empty;

                        }

                        else { SaveResponseLabel.Text = "Duplicate Service."; }

                        break;


                    case "ButtonUpdateServiceEvent":

                        if ((existingServiceEventFound) && (newServiceEvent.ServiceId != population.ServiceEvents[ServiceEventsGrid.SelectedItems[0].DataSetIndex].ServiceId)) {

                            SaveResponseLabel.Text = "Duplicate Service.";

                        }

                        else {

                            if (ServiceEventsGrid.SelectedItems[0] != null) {

                                newServiceEvent.CoreObjectId = population.ServiceEvents[ServiceEventsGrid.SelectedItems[0].DataSetIndex].Id;

                                population.ServiceEvents.RemoveAt (ServiceEventsGrid.SelectedItems[0].DataSetIndex);

                                population.ServiceEvents.Add (newServiceEvent);

                            }

                            else { SaveResponseLabel.Text = "No Service Event Selected."; }

                        }

                        break;

                }

            }

            else {

                foreach (String validationKey in validationResponse.Keys) {

                    SaveResponseLabel.Text = "Invalid [" + validationKey + "]: " + validationResponse[validationKey];

                    break;

                }

            }

            InitializeServiceEventsGrid ();

            return;

        }

        protected void ServiceEventsGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            Int32 deleteIndex = eventArgs.Item.DataSetIndex;

            population.ServiceEvents.RemoveAt (deleteIndex);

            InitializeServiceEventsGrid ();

            return;

        }

        protected void ServiceEventsGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            switch (eventArgs.CommandName) {

                case Telerik.Web.UI.RadGrid.EditCommandName:

                    EditServiceEvent = population.ServiceEvents[eventArgs.Item.ItemIndex].Copy ();

                    ServiceEventsServiceSelection.SelectedValue = EditServiceEvent.ServiceId.ToString ();

                    ServiceEventsExclusionSelection.SelectedValue = EditServiceEvent.ExclusionServiceId.ToString ();

                    ServiceEventsAnchorSelection.SelectedValue = ((Int32) EditServiceEvent.AnchorDate).ToString ();

                    ServiceEventsAnchorDateValue.Enabled = ((Int32) EditServiceEvent.AnchorDate >= 2);

                    ServiceEventsAnchorDateValue.Value = EditServiceEvent.AnchorDateValue;

                    ServiceEventsScheduleValue.Value = EditServiceEvent.ScheduleDateValue;

                    ServiceEventsScheduleQualifier.SelectedValue = ((Int32) EditServiceEvent.ScheduleDateQualifier).ToString ();

                    ServiceEventsReoccurring.Checked = EditServiceEvent.Reoccurring;

                    ServiceEventsThresholdsGrid.Rebind ();

                    eventArgs.Canceled = true;

                    break;

                default:

                    System.Diagnostics.Debug.WriteLine (eventArgs.CommandName);

                    break;

            }

            InitializeServiceEventsGrid ();

            ServiceEventsGrid.SelectedIndexes.Add (eventArgs.Item.ItemIndex);

            return;

        }


        protected void ServiceEventsThresholdsGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            Mercury.Client.Core.Population.PopulationEvents.PopulationServiceEventThreshold threshold;

            if (MercuryApplication == null) { return; }

            switch (eventArgs.RebindReason) {

                case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                    System.Diagnostics.Debug.WriteLine (eventArgs.RebindReason);

                    System.Data.DataTable thresholdTable = (DataTable) Session[SessionCachePrefix + "ServiceEventsThresholdsGrid.ThresholdTable"];

                    thresholdTable.Rows.Clear ();


                    Int32 currentThresholdIndex = 0;

                    foreach (Int64 thresholdKey in EditServiceEvent.SortedThresholds.Keys) {

                        Mercury.Client.Core.Population.PopulationEvents.PopulationServiceEventThreshold currentThreshold = EditServiceEvent.SortedThresholds[thresholdKey];

                        String actionName = String.Empty;

                        thresholdTable.Rows.Add (

                            currentThresholdIndex,

                            currentThreshold.RelativeDateValue,

                            currentThreshold.RelativeDateQualifier.ToString (),

                            currentThreshold.StatusText,

                            currentThreshold.Action.Description

                        );

                        currentThresholdIndex = currentThresholdIndex + 1;

                    }

                    ServiceEventsThresholdsGrid.DataSource = thresholdTable;

                    break;

                default:

                    if ((eventArgs.RebindReason & Telerik.Web.UI.GridRebindReason.DetailTableBinding) == Telerik.Web.UI.GridRebindReason.DetailTableBinding) {

                        System.Data.DataTable parameterTable = (DataTable) Session[SessionCachePrefix + "ServiceEventsThresholdsGrid.ParameterTable"];

                        parameterTable.Rows.Clear ();

                        currentThresholdIndex = 0;

                        foreach (Int64 thresholdKey in EditServiceEvent.SortedThresholds.Keys) {

                            threshold = EditServiceEvent.SortedThresholds[thresholdKey];

                            foreach (String parameterName in threshold.Action.ActionParameters.Keys) {

                                parameterTable.Rows.Add (thresholdKey, currentThresholdIndex, parameterName, threshold.Action.ActionParameters[parameterName].ValueDescription);

                            }

                            currentThresholdIndex = currentThresholdIndex + 1;

                        }

                        ServiceEventsThresholdsGrid.MasterTableView.DetailTables[0].DataSource = parameterTable;

                    }

                    break;

            }

            return;


        }

        protected void ServiceEventsThresholdsGrid_OnItemDataBound (Object sender, Telerik.Web.UI.GridItemEventArgs eventArgs) {

            Telerik.Web.UI.RadNumericTextBox thresholdRelativeDateValue;

            Telerik.Web.UI.RadComboBox thresholdRelativeDateQualifier;

            Telerik.Web.UI.RadComboBox thresholdStatusSelection;

            Telerik.Web.UI.RadComboBox thresholdActionSelection;

            Client.Core.Population.PopulationEvents.PopulationServiceEventThreshold threshold;

            System.Collections.Generic.Dictionary<String, String> bindingContexts;


            if (MercuryApplication == null) { return; }


            if ((eventArgs.Item is Telerik.Web.UI.GridEditableItem) && (eventArgs.Item.IsInEditMode)) {

                Telerik.Web.UI.GridEditableItem editItem = (Telerik.Web.UI.GridEditableItem) eventArgs.Item;


                switch (eventArgs.Item.OwnerTableView.Name) {
                        
                    case "Thresholds":

                        thresholdRelativeDateValue = (Telerik.Web.UI.RadNumericTextBox) editItem.FindControl ("ServiceEventsThresholdRelativeDateValue");

                        thresholdRelativeDateQualifier = (Telerik.Web.UI.RadComboBox) editItem.FindControl ("ServiceEventsThresholdRelativeDateQualifier");

                        thresholdStatusSelection = (Telerik.Web.UI.RadComboBox) editItem.FindControl ("ServiceEventsThresholdStatusSelection");

                        thresholdActionSelection = (Telerik.Web.UI.RadComboBox) editItem.FindControl ("ServiceEventsThresholdActionSelection");


                        thresholdActionSelection.Items.Clear ();

                        thresholdActionSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* No Action Selected", "0"));

                        foreach (Mercury.Server.Application.Action currentAction in MercuryApplication.ActionsAvailable (false)) {

                            thresholdActionSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentAction.Name, currentAction.Id.ToString ()));

                        }

                        if (eventArgs.Item.ItemIndex != -1) {

                            threshold = EditServiceEvent.SortedThresholds.Values[eventArgs.Item.ItemIndex];

                            if (threshold != null) {

                                thresholdRelativeDateValue.Value = threshold.RelativeDateValue;

                                thresholdRelativeDateQualifier.SelectedValue = threshold.RelativeDateQualifier.ToString ();

                                thresholdStatusSelection.SelectedValue = ((Int32)threshold.Status).ToString ();

                                thresholdActionSelection.SelectedValue = threshold.Action.Id.ToString ();

                            }

                        }

                        break;

                    case "ThresholdParameters":

                        Int64 thresholdKey = Int64.Parse ((String) ((Telerik.Web.UI.GridEditableItem) eventArgs.Item).OwnerTableView.DataKeyValues[eventArgs.Item.ItemIndex]["ThresholdKey"]);

                        threshold = EditServiceEvent.SortedThresholds[thresholdKey];

                        String parameterName = (String) editItem.OwnerTableView.DataKeyValues[eventArgs.Item.ItemIndex]["ParameterName"];

                        Telerik.Web.UI.RadComboBox parameterValueSelection = (Telerik.Web.UI.RadComboBox) eventArgs.Item.FindControl ("ServiceEventsThresholdParameterValue");

                        Telerik.Web.UI.RadTextBox parameterFixedValue = (Telerik.Web.UI.RadTextBox) eventArgs.Item.FindControl ("ServiceEventsThresholdParameterFixedValue");


                        parameterValueSelection.Items.Clear ();

                        if (!threshold.Action.ActionParameters[parameterName].Required) {

                            parameterValueSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* Not Assigned", "0"));

                        }


                        bindingContexts = population.ParameterValueSelection(threshold.Action.ActionParameters[parameterName].DataType);

                        foreach (String currentBindingContextName in bindingContexts.Keys) {

                            parameterValueSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentBindingContextName, bindingContexts[currentBindingContextName]));

                        }

                        parameterFixedValue.Enabled = threshold.Action.ActionParameters[parameterName].AllowFixedValue;

                        parameterFixedValue.EmptyMessage = (threshold.Action.ActionParameters[parameterName].AllowFixedValue) ? String.Empty : "(Not Available)";

                        parameterFixedValue.Text = String.Empty;

                        break;

                }

            }

            return;

        }

        protected void ServiceEventsThresholdsGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            Client.Core.Population.PopulationEvents.PopulationServiceEvent serviceEvent = EditServiceEvent;

            Mercury.Client.Core.Population.PopulationEvents.PopulationServiceEventThreshold threshold;

            String parameterName;

            Telerik.Web.UI.RadNumericTextBox thresholdRelativeDateValue;

            Telerik.Web.UI.RadComboBox thresholdRelativeDateQualifier;
            
            Telerik.Web.UI.RadComboBox thresholdStatusSelection;

            Telerik.Web.UI.RadComboBox thresholdActionSelection;
            
            Telerik.Web.UI.RadComboBox parameterValueSelection;

            Telerik.Web.UI.RadTextBox parameterFixedValue;

            Int64 actionId;


            if (MercuryApplication == null) { return; }


            SaveResponseLabel.Text = String.Empty;

            switch (eventArgs.CommandName) {

                case Telerik.Web.UI.RadGrid.EditCommandName:

                    #region Edit Command

                    switch (eventArgs.Item.OwnerTableView.Name) {

                        case "Thresholds":

                            Telerik.Web.UI.GridEditCommandColumn editColumn = (Telerik.Web.UI.GridEditCommandColumn) ServiceEventsThresholdsGrid.MasterTableView.GetColumn ("EditCommandColumn");

                            if (!editColumn.Visible) { editColumn.Visible = true; }

                            Telerik.Web.UI.GridEditableItem editItem = (Telerik.Web.UI.GridEditableItem) eventArgs.Item;

                            break;


                        case "ThresholdParameters":

                            editColumn = (Telerik.Web.UI.GridEditCommandColumn) eventArgs.Item.OwnerTableView.GetColumn ("EditCommandColumn");

                            if (!editColumn.Visible) { editColumn.Visible = true; }

                            break;

                    }

                    #endregion

                    break;

                case Telerik.Web.UI.RadGrid.InitInsertCommandName:

                    break;

                case Telerik.Web.UI.RadGrid.PerformInsertCommandName:

                    #region Perform Insert Command

                    Telerik.Web.UI.GridEditableItem insertedItem = (Telerik.Web.UI.GridEditableItem) eventArgs.Item;

                    thresholdRelativeDateValue = (Telerik.Web.UI.RadNumericTextBox) insertedItem.FindControl ("ServiceEventsThresholdRelativeDateValue");

                    thresholdRelativeDateQualifier = (Telerik.Web.UI.RadComboBox) insertedItem.FindControl ("ServiceEventsThresholdRelativeDateQualifier");
                    
                    thresholdStatusSelection = (Telerik.Web.UI.RadComboBox) insertedItem.FindControl ("ServiceEventsThresholdStatusSelection");

                    thresholdActionSelection = (Telerik.Web.UI.RadComboBox) insertedItem.FindControl ("ServiceEventsThresholdActionSelection");


                    try {

                        if (thresholdRelativeDateValue.Value == null) { throw new ApplicationException ("No Threshold Date Specified."); }


                        threshold = serviceEvent.GetNewThreshold ();

                        threshold.RelativeDateValue = Int32.Parse (thresholdRelativeDateValue.Value.ToString ());

                        threshold.RelativeDateQualifier = (Mercury.Server.Application.DateQualifier) Int32.Parse (thresholdRelativeDateQualifier.SelectedItem.Value);

                        threshold.Status = (Mercury.Server.Application.PopulationServiceEventStatus) Int32.Parse (thresholdStatusSelection.SelectedValue);

                        actionId = Int64.Parse (thresholdActionSelection.SelectedValue);


                        threshold.Action = MercuryApplication.ActionById (actionId);

                        if (threshold.Action == null) { threshold.Action = new Mercury.Client.Core.Action.Action (MercuryApplication); }


                        if (!EditServiceEvent.HasThreshold (threshold)) {

                            serviceEvent.Thresholds.Add (threshold);

                            EditServiceEvent = serviceEvent;
                        
                        }

                        else { throw new ApplicationException ("Duplicate Threshold"); }

                    }

                    catch (Exception MercuryApplicationException) {

                        SaveResponseLabel.Text = "Error: " + MercuryApplicationException.Message;

                    }

                    #endregion

                    break;

                case Telerik.Web.UI.RadGrid.UpdateCommandName:

                    #region Update Command

                    Telerik.Web.UI.GridEditableItem updatedItem = (Telerik.Web.UI.GridEditableItem) eventArgs.Item;

                    switch (eventArgs.Item.OwnerTableView.Name) {

                        case "Thresholds":

                            thresholdRelativeDateValue = (Telerik.Web.UI.RadNumericTextBox) updatedItem.FindControl ("ServiceEventsThresholdRelativeDateValue");

                            thresholdRelativeDateQualifier = (Telerik.Web.UI.RadComboBox) updatedItem.FindControl ("ServiceEventsThresholdRelativeDateQualifier");

                            thresholdStatusSelection = (Telerik.Web.UI.RadComboBox) updatedItem.FindControl ("ServiceEventsThresholdStatusSelection");

                            thresholdActionSelection = (Telerik.Web.UI.RadComboBox) updatedItem.FindControl ("ServiceEventsThresholdActionSelection");

                            threshold = serviceEvent.SortedThresholds.Values[updatedItem.ItemIndex];

                            threshold.RelativeDateValue = Int32.Parse (thresholdRelativeDateValue.Value.ToString ());

                            threshold.RelativeDateQualifier = (Mercury.Server.Application.DateQualifier) Int32.Parse (thresholdRelativeDateQualifier.SelectedItem.Value);

                            threshold.Status = (Mercury.Server.Application.PopulationServiceEventStatus) Int32.Parse (thresholdStatusSelection.SelectedValue);


                            actionId = Int64.Parse (thresholdActionSelection.SelectedValue);


                            if (threshold.Action.Id != actionId) {

                                threshold.Action = MercuryApplication.ActionById (actionId);

                                if (threshold.Action == null) { threshold.Action = new Mercury.Client.Core.Action.Action (MercuryApplication); }

                            }

                            break;

                        case "ThresholdParameters":

                            Int64 thresholdKey = Int64.Parse ((String) ((Telerik.Web.UI.GridEditableItem) eventArgs.Item).OwnerTableView.DataKeyValues[eventArgs.Item.ItemIndex]["ThresholdKey"]);

                            threshold = serviceEvent.SortedThresholds[thresholdKey];

                            parameterName = (String) ((Telerik.Web.UI.GridEditableItem) eventArgs.Item).OwnerTableView.DataKeyValues[eventArgs.Item.ItemIndex]["ParameterName"];

                            if (threshold.Action.ActionParameters.ContainsKey (parameterName)) {

                                parameterValueSelection = (Telerik.Web.UI.RadComboBox) eventArgs.Item.FindControl ("ServiceEventsThresholdParameterValue");

                                parameterFixedValue = (Telerik.Web.UI.RadTextBox) eventArgs.Item.FindControl ("ServiceEventsThresholdParameterFixedValue");

                                if ((parameterValueSelection.SelectedItem != null) || (!String.IsNullOrEmpty (parameterFixedValue.Text))) {


                                    if ((threshold.Action.ActionParameters[parameterName].AllowFixedValue) && (!String.IsNullOrEmpty (parameterFixedValue.Text))) {
                                        
                                        threshold.Action.ActionParameters[parameterName].ValueType = Mercury.Server.Application.ActionParameterValueType.FixedValue;

                                        threshold.Action.ActionParameters[parameterName].Value = parameterFixedValue.Text;

                                        threshold.Action.ActionParameters[parameterName].ValueDescription = parameterFixedValue.Text;

                                    }

                                    else { 

                                        threshold.Action.ActionParameters[parameterName].ValueType = Mercury.Server.Application.ActionParameterValueType.DataMapping;

                                        threshold.Action.ActionParameters[parameterName].Value = parameterValueSelection.SelectedItem.Value;

                                        threshold.Action.ActionParameters[parameterName].ValueDescription = parameterValueSelection.SelectedItem.Text;

                                    }


                                    if ((threshold.Action.Name == "Workflow") && (parameterName == "Workflow")) {

                                        threshold.Action.RebindActionParameters (MercuryApplication);

                                    }

                                }

                            }

                            break;

                    }

                    #endregion

                    break;

                case Telerik.Web.UI.RadGrid.ExpandCollapseCommandName:

                    #region Expand/Collapse Command

                    System.Data.DataTable parameterTable = (DataTable) Session[SessionCachePrefix + "ServiceEventsThresholdsGrid.ParameterTable"];

                    parameterTable.Rows.Clear ();

                    Int32 currentThresholdIndex = 0;

                    foreach (Mercury.Client.Core.Population.PopulationEvents.PopulationServiceEventThreshold currentThreshold in serviceEvent.SortedThresholds.Values) {

                        foreach (String currentParameterName in currentThreshold.Action.ActionParameters.Keys) {

                            parameterTable.Rows.Add (currentThresholdIndex, currentParameterName, currentThreshold.Action.ActionParameters[currentParameterName].ValueDescription);

                        }

                        currentThresholdIndex = currentThresholdIndex + 1;

                    }

                    ServiceEventsThresholdsGrid.MasterTableView.DetailTables[0].DataSource = parameterTable;

                    ServiceEventsThresholdsGrid.MasterTableView.DetailTables[0].DataBind ();

                    #endregion

                    break;

                case Telerik.Web.UI.RadGrid.DeleteCommandName: 

                    Int32 deleteIndex = eventArgs.Item.DataSetIndex;

                    if ((deleteIndex > -1) && (deleteIndex < serviceEvent.Thresholds.Count)) {

                        threshold = serviceEvent.SortedThresholds.Values[deleteIndex];

                        serviceEvent.Thresholds.Remove (threshold);

                    }

                    break;

                default: 

                    System.Diagnostics.Debug.WriteLine (eventArgs.CommandName);

                    break;

            }


            EditServiceEvent = serviceEvent;

            return;

        }

        #endregion


        #region Trigger Events Control Events

        protected void TriggerEventsProblemStatementSelection_OnItemCreated (Object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e) {

            Telerik.Web.UI.RadTreeView TriggerEventsProblemStatementTreeView = (Telerik.Web.UI.RadTreeView)e.Item.FindControl ("TriggerEventsProblemStatementTreeView");

            if (TriggerEventsProblemStatementTreeView == null) { return; }

            Telerik.Web.UI.RadComboBoxItem comboBoxItem = (Telerik.Web.UI.RadComboBoxItem)e.Item;

            if (comboBoxItem == null) { return; }


            TriggerEventsProblemStatementTreeView.Nodes.Clear ();

            TriggerEventsProblemStatementTreeView.Nodes.Add (new Telerik.Web.UI.RadTreeNode ("* No Problem Statement Assigned", "0"));

            TriggerEventsProblemStatementTreeView.Nodes[0].Selected = true;


            var sortedProblemStatements =

                from currentProblemStatement in MercuryApplication.ProblemStatementsAvailable (true)

                orderby currentProblemStatement.ProblemDomainName, currentProblemStatement.ProblemClassName, currentProblemStatement.Name

                select currentProblemStatement;

                


            foreach (Client.Core.Individual.ProblemStatement currentProblemStatement in sortedProblemStatements) {

                Telerik.Web.UI.RadTreeNode domainNode = TriggerEventsProblemStatementTreeView.Nodes.FindNodeByValue ("ProblemDomain_" + currentProblemStatement.ProblemDomainId.ToString ());

                Telerik.Web.UI.RadTreeNode problemNode = new Telerik.Web.UI.RadTreeNode (currentProblemStatement.Name, currentProblemStatement.Id.ToString ());

                // CHECK FOR DOMAIN NODE

                if (domainNode == null) {

                    domainNode = new Telerik.Web.UI.RadTreeNode (currentProblemStatement.ProblemDomainName, "ProblemDomain_" + currentProblemStatement.ProblemDomainId);

                    TriggerEventsProblemStatementTreeView.Nodes.Add (domainNode);

                }

                Telerik.Web.UI.RadTreeNode classNode = domainNode.Nodes.FindNodeByValue ("ProblemClass_" + currentProblemStatement.ProblemClassId.ToString ());

                if (classNode == null) {

                    classNode = new Telerik.Web.UI.RadTreeNode (currentProblemStatement.ProblemClassName, "ProblemClass_" + currentProblemStatement.ProblemClassId);

                    domainNode.Nodes.Add (classNode);

                }

                problemNode.Selected = (currentProblemStatement.Id.ToString() == TriggerEventsProblemStatementSelection.SelectedValue);

                if (problemNode.Selected) {

                    classNode.Expanded = true;

                    domainNode.Expanded = true;

                }

                classNode.Nodes.Add (problemNode);
                

            }

            comboBoxItem.Text = TriggerEventsProblemStatementTreeView.SelectedNode.Text;

            if (TriggerEventsProblemStatementTreeView.SelectedNode.ParentNode != null) {

                comboBoxItem.Text = TriggerEventsProblemStatementTreeView.SelectedNode.ParentNode.ParentNode.Text + " - ";

                comboBoxItem.Text += TriggerEventsProblemStatementTreeView.SelectedNode.ParentNode.Text + " - ";

                comboBoxItem.Text += TriggerEventsProblemStatementTreeView.SelectedNode.Text;

            }

            comboBoxItem.Value = TriggerEventsProblemStatementTreeView.SelectedNode.Value;

            comboBoxItem.Selected = true;

            return;

        }


        protected void ButtonAddUpdateTriggerEvent_OnClick (Object sender, EventArgs eventArgs) {

            Boolean existingTriggerEventFound = false;

            Client.Core.Population.PopulationEvents.PopulationTriggerEvent newTriggerEvent = null;

            Dictionary<String, String> validationResponse;

            SaveResponseLabel.Text = String.Empty;


            if (MercuryApplication == null) { return; }


            newTriggerEvent = new Mercury.Client.Core.Population.PopulationEvents.PopulationTriggerEvent (MercuryApplication);

            newTriggerEvent.PopulationId = population.Id;

            newTriggerEvent.EventType = (Mercury.Server.Application.PopulationTriggerEventType) Int32.Parse (TriggerEventsTypeSelection.SelectedValue);


            if (TriggerEventsServiceSelection.SelectedItem != null) { newTriggerEvent.ServiceId = Int64.Parse (TriggerEventsServiceSelection.SelectedItem.Value); }

            if (TriggerEventsMetricSelection.SelectedItem != null) {

                newTriggerEvent.MetricType = (Mercury.Server.Application.MetricType) Int32.Parse (TriggerEventsMetricSelection.SelectedItem.Value.Split ('|')[0]);

                newTriggerEvent.MetricId = Int64.Parse (TriggerEventsMetricSelection.SelectedItem.Value.Split ('|')[1]);

                if (TriggerEventsMetricMinimum.Value.HasValue) { newTriggerEvent.MetricMinimum = Convert.ToDecimal (TriggerEventsMetricMinimum.Value.Value); }

                if (TriggerEventsMetricMaximum.Value.HasValue) { newTriggerEvent.MetricMaximum = Convert.ToDecimal (TriggerEventsMetricMaximum.Value.Value); }

            }

            if (TriggerEventsAuthorizedServiceSelection.SelectedItem != null) { newTriggerEvent.AuthorizedServiceId = Int64.Parse (TriggerEventsAuthorizedServiceSelection.SelectedItem.Value); }

            if (TriggerEventsProblemStatementSelection.SelectedItem != null) { newTriggerEvent.ProblemStatementId = Int64.Parse (TriggerEventsProblemStatementSelection.SelectedValue); }

            newTriggerEvent.Action = EditTriggerEvent.Action.Copy ();


            validationResponse = newTriggerEvent.Validate ();

            if (validationResponse.Count == 0) {

                existingTriggerEventFound = false;

                foreach (Client.Core.Population.PopulationEvents.PopulationTriggerEvent currentEvent in population.TriggerEvents) {

                    if (currentEvent.IsEqual (newTriggerEvent)) {  

                        existingTriggerEventFound = true;

                        SaveResponseLabel.Text = "Duplicate Trigger Found.";
                        
                        break;

                    }

                }



                switch (((System.Web.UI.WebControls.Button) sender).ID) {

                    case "ButtonAddTriggerEvent":
                        
                        if (((newTriggerEvent.EventType == Mercury.Server.Application.PopulationTriggerEventType.Service) && (population.HasTriggerEventForService (newTriggerEvent.ServiceId)))
                            
                            || ((newTriggerEvent.EventType == Mercury.Server.Application.PopulationTriggerEventType.Metric) && (population.HasTriggerEventForMetric (newTriggerEvent.MetricId)))) {

                            existingTriggerEventFound = true;
                            
                            SaveResponseLabel.Text = "Duplicate Trigger Found.";

                        }

                        if (!existingTriggerEventFound) {

                            population.TriggerEvents.Add (newTriggerEvent);

                            SaveResponseLabel.Text = String.Empty;

                        }

                        else { SaveResponseLabel.Text = "Duplicate Trigger Found."; }

                        break;


                    case "ButtonUpdateTriggerEvent":

                        if (TriggerEventsGrid.SelectedItems.Count == 1) {

                            if ((existingTriggerEventFound) && (newTriggerEvent.Id != population.TriggerEvents[TriggerEventsGrid.SelectedItems[0].DataSetIndex].Id)) {

                                SaveResponseLabel.Text = "Duplicate Trigger.";

                            }

                            else {

                                if (TriggerEventsGrid.SelectedItems[0] != null) {

                                    newTriggerEvent.CoreObjectId = population.TriggerEvents[TriggerEventsGrid.SelectedItems[0].DataSetIndex].Id;

                                    population.TriggerEvents[TriggerEventsGrid.SelectedItems[0].DataSetIndex] = newTriggerEvent.Copy ();

                                }

                                else { SaveResponseLabel.Text = "No Trigger Event Selected."; }

                            }

                        }

                        else { SaveResponseLabel.Text = "No Existing Trigger Selected for Update."; }

                        break;

                }

            }

            else {

                foreach (String validationKey in validationResponse.Keys) {

                    SaveResponseLabel.Text = "Invalid [" + validationKey + "]: " + validationResponse[validationKey];

                    break;

                }

            }

            InitializeTriggerEventsGrid ();

            return;

        }

        protected void TriggerEventsGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            Int32 deleteIndex = eventArgs.Item.DataSetIndex;

            population.TriggerEvents.RemoveAt (deleteIndex);

            InitializeTriggerEventsGrid ();

            return;

        }

        protected void TriggerEventsGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            Client.Core.Population.PopulationEvents.PopulationTriggerEvent triggerEvent = EditTriggerEvent;

            if (MercuryApplication == null) { return; }

            switch (eventArgs.CommandName) {

                case Telerik.Web.UI.RadGrid.EditCommandName:

                    triggerEvent = population.TriggerEvents[eventArgs.Item.ItemIndex].Copy ();

                    TriggerEventsTypeSelection.SelectedValue = ((Int32) triggerEvent.EventType).ToString ();

                    TriggerEventsServiceSelection.SelectedValue = triggerEvent.ServiceId.ToString ();

                    TriggerEventsMetricSelection.SelectedValue = ((Int32) triggerEvent.MetricType).ToString () + "|" + triggerEvent.MetricId.ToString ();

                    TriggerEventsMetricMinimum.Value = Convert.ToDouble (triggerEvent.MetricMinimum);

                    TriggerEventsMetricMaximum.Value = Convert.ToDouble (triggerEvent.MetricMaximum);


                    if (triggerEvent.ProblemStatement == null) { InitializeTriggerEventsProblemStatementSelection (String.Empty, String.Empty); }

                    else { InitializeTriggerEventsProblemStatementSelection (triggerEvent.ProblemStatement.ClassificationWithName, triggerEvent.ProblemStatement.Id.ToString ()); }

                    TriggerEventsActionSelection.SelectedValue = triggerEvent.Action.Id.ToString ();

                    Session[SessionCachePrefix + "TriggerEvent"] = triggerEvent;

                    eventArgs.Canceled = true;

                    break;

            }

            EditTriggerEvent = triggerEvent;

            InitializeTriggerEventsGrid ();

            InitializeTriggerEventsActionParametersGrid ();

            TriggerEventsGrid.SelectedIndexes.Add (eventArgs.Item.ItemIndex);

            return;

        }

        protected void TriggerEventsActionSelection_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            Client.Core.Population.PopulationEvents.PopulationTriggerEvent triggerEvent = EditTriggerEvent;

            if (MercuryApplication == null) { return; }

            if (TriggerEventsActionSelection.SelectedItem != null) {

                Int64 actionId = Int64.Parse (TriggerEventsActionSelection.SelectedValue);

                triggerEvent.Action = MercuryApplication.ActionById (actionId);

                if (triggerEvent.Action == null) { triggerEvent.Action = new Mercury.Client.Core.Action.Action (MercuryApplication); }

            }

            EditTriggerEvent = triggerEvent;

            InitializeTriggerEventsActionParametersGrid ();

            return;

        }


        protected void TriggerEventsActionParametersGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            switch (eventArgs.RebindReason) {

                case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                    System.Data.DataTable parameterTable = (DataTable) Session[SessionCachePrefix + "TriggerEventsActionParametersGrid.ParameterTable"];

                    parameterTable.Rows.Clear ();

                    foreach (String parameterName in EditTriggerEvent.Action.ActionParameters.Keys) {

                        parameterTable.Rows.Add (parameterName, EditTriggerEvent.Action.ActionParameters[parameterName].ValueDescription);

                    }


                    Session[SessionCachePrefix + "TriggerEventsActionParametersGrid.ParameterTable"] = parameterTable;

                    TriggerEventsActionParametersGrid.DataSource = parameterTable;

                    break;

            }
            
            return;

        }

        protected void TriggerEventsActionParametersGrid_OnItemDataBound (Object sender, Telerik.Web.UI.GridItemEventArgs eventArgs) {

            Client.Core.Population.PopulationEvents.PopulationTriggerEvent triggerEvent = EditTriggerEvent;

            Telerik.Web.UI.RadComboBox triggerEventParameterValueSelection;

            Telerik.Web.UI.RadTextBox triggerEventParameterFixedValue;

            System.Collections.Generic.Dictionary<String, String> bindingContexts;

            String parameterName;


            if (MercuryApplication == null) { return; }


            if ((eventArgs.Item is Telerik.Web.UI.GridEditableItem) && (eventArgs.Item.IsInEditMode)) {

                Telerik.Web.UI.GridEditableItem editItem = (Telerik.Web.UI.GridEditableItem) eventArgs.Item;


                if (TriggerEventsActionSelection.SelectedItem != null) {

                    parameterName = (String) editItem.OwnerTableView.DataKeyValues[eventArgs.Item.ItemIndex]["ParameterName"];

                    triggerEventParameterValueSelection = (Telerik.Web.UI.RadComboBox) eventArgs.Item.FindControl ("TriggerEventParameterValueSelection");

                    triggerEventParameterValueSelection.Items.Clear ();

                    if (!triggerEvent.Action.ActionParameters[parameterName].Required) {

                        triggerEventParameterValueSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* Not Assigned", "0"));

                    }


                    bindingContexts = population.ParameterValueSelection(triggerEvent.Action.ActionParameters[parameterName].DataType);

                    foreach (String currentBindingContextName in bindingContexts.Keys) {

                        triggerEventParameterValueSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentBindingContextName, bindingContexts[currentBindingContextName]));

                    }

                    triggerEventParameterFixedValue = (Telerik.Web.UI.RadTextBox) eventArgs.Item.FindControl ("TriggerEventParameterFixedValue");

                    triggerEventParameterFixedValue.Enabled = triggerEvent.Action.ActionParameters[parameterName].AllowFixedValue;

                    triggerEventParameterFixedValue.EmptyMessage = (triggerEvent.Action.ActionParameters[parameterName].AllowFixedValue) ? String.Empty : "(Not Available)";

                    triggerEventParameterFixedValue.Text = String.Empty;

                }

            }

            EditTriggerEvent = triggerEvent;

            return;

        }

        protected void TriggerEventsActionParametersGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            Client.Core.Population.PopulationEvents.PopulationTriggerEvent triggerEvent = EditTriggerEvent;

            String parameterName;

            Telerik.Web.UI.RadComboBox parameterValueSelection;

            Telerik.Web.UI.RadTextBox parameterFixedValue;


            if (MercuryApplication == null) { return; }


            switch (eventArgs.CommandName) {

                case Telerik.Web.UI.RadGrid.EditCommandName:

                    #region Edit Command

                    Telerik.Web.UI.GridEditCommandColumn editColumn = (Telerik.Web.UI.GridEditCommandColumn) ServiceEventsThresholdsGrid.MasterTableView.GetColumn ("EditCommandColumn");

                    if (!editColumn.Visible) { editColumn.Visible = true; }

                    Telerik.Web.UI.GridEditableItem editItem = (Telerik.Web.UI.GridEditableItem) eventArgs.Item;

                    #endregion

                    break;

                case Telerik.Web.UI.RadGrid.UpdateCommandName:

                    #region Update Command

                    parameterValueSelection = (Telerik.Web.UI.RadComboBox) eventArgs.Item.FindControl ("TriggerEventParameterValueSelection");

                    parameterFixedValue = (Telerik.Web.UI.RadTextBox) eventArgs.Item.FindControl ("TriggerEventParameterFixedValue");

                    parameterName = (String) ((Telerik.Web.UI.GridEditableItem) eventArgs.Item).OwnerTableView.DataKeyValues[eventArgs.Item.ItemIndex]["ParameterName"];

                    if ((parameterValueSelection.SelectedItem != null) || (!String.IsNullOrEmpty (parameterFixedValue.Text))) {

                        if ((triggerEvent.Action.ActionParameters[parameterName].AllowFixedValue) && (!String.IsNullOrEmpty (parameterFixedValue.Text))) {

                            triggerEvent.Action.ActionParameters[parameterName].ValueType = Mercury.Server.Application.ActionParameterValueType.FixedValue;

                            triggerEvent.Action.ActionParameters[parameterName].Value = parameterFixedValue.Text;

                            triggerEvent.Action.ActionParameters[parameterName].ValueDescription = parameterFixedValue.Text;

                        }

                        else {

                            triggerEvent.Action.ActionParameters[parameterName].ValueType = Mercury.Server.Application.ActionParameterValueType.DataMapping;

                            triggerEvent.Action.ActionParameters[parameterName].Value = parameterValueSelection.SelectedItem.Value;

                            triggerEvent.Action.ActionParameters[parameterName].ValueDescription = parameterValueSelection.SelectedItem.Text;

                        }

                        if ((triggerEvent.Action.Name == "Workflow") && (parameterName == "Workflow")) {

                            triggerEvent.Action.RebindActionParameters (MercuryApplication);

                        }

                    }

                    #endregion

                    break;

            }

            EditTriggerEvent = triggerEvent;

            return;

        }

        #endregion


        #region Activity Events Control Events

        protected void ButtonAddUpdateActivityEvent_OnClick (Object sender, EventArgs eventArgs) {

            Boolean existingActivityEventFound = false;

            Client.Core.Population.PopulationEvents.PopulationActivityEvent newActivityEvent = null;

            Dictionary<String, String> validationResponse;

            SaveResponseLabel.Text = String.Empty;


            if (MercuryApplication == null) { return; }


            newActivityEvent = new Mercury.Client.Core.Population.PopulationEvents.PopulationActivityEvent (MercuryApplication);

            newActivityEvent.PopulationId = population.Id;

            newActivityEvent.ScheduleType = (Mercury.Server.Application.PopulationActivityScheduleType) Int32.Parse (ActivityScheduleTypeSelection.SelectedValue);

            newActivityEvent.ScheduleValue = (ActivityScheduleValue.Value.HasValue) ? Convert.ToInt32 (ActivityScheduleValue.Value.Value) : 0;

            newActivityEvent.ScheduleQualifier = (Mercury.Server.Application.DateQualifier) Int32.Parse (ActivityScheduleQualifierSelection.SelectedValue);

            newActivityEvent.AnchorDate = (Mercury.Server.Application.PopulationActivityEventAnchorDate) Int32.Parse (ActivityAnchorDate.SelectedValue);

            newActivityEvent.Reoccurring = ActivityReoccurring.Checked;

            newActivityEvent.PerformActionDateType = (Mercury.Server.Application.PopulationActivityPerformActionDateType)Int32.Parse (ActivityActionDateTypeSelection.SelectedValue);

            newActivityEvent.Action = EditActivityEvent.Action.Copy ();

            
            validationResponse = newActivityEvent.Validate ();

            if (validationResponse.Count == 0) {

                existingActivityEventFound = false;

                foreach (Client.Core.Population.PopulationEvents.PopulationActivityEvent currentEvent in population.ActivityEvents) {

                    if (currentEvent.IsEqual (newActivityEvent)) {

                        existingActivityEventFound = true;

                        SaveResponseLabel.Text = "Duplicate Activity Found.";

                        break;

                    }

                }


                switch (((System.Web.UI.WebControls.Button) sender).ID) {

                    case "ButtonAddActivityEvent":

                        if (!existingActivityEventFound) {

                            population.ActivityEvents.Add (newActivityEvent);

                            SaveResponseLabel.Text = String.Empty;

                        }

                        else { SaveResponseLabel.Text = "Duplicate Activity Found."; }

                        break;


                    case "ButtonUpdateActivityEvent":

                        if ((existingActivityEventFound) && (newActivityEvent.Id != population.ActivityEvents[ActivityEventsGrid.SelectedItems[0].DataSetIndex].Id)) {

                            SaveResponseLabel.Text = "Duplicate Activity Found.";

                        }

                        else {

                            if (ActivityEventsGrid.SelectedItems[0] != null) {

                                newActivityEvent.CoreObjectId = population.ActivityEvents[ActivityEventsGrid.SelectedItems[0].DataSetIndex].Id;

                                population.ActivityEvents[ActivityEventsGrid.SelectedItems[0].DataSetIndex] = newActivityEvent.Copy ();

                            }

                            else { SaveResponseLabel.Text = "No Activity Event Selected."; }

                        }

                        break;

                }

            }

            else {

                foreach (String validationKey in validationResponse.Keys) {

                    SaveResponseLabel.Text = "Invalid [" + validationKey + "]: " + validationResponse[validationKey];

                    break;

                }

            }

            InitializeActivityEventsGrid ();

            return;

        }

        protected void ActivityEventsGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }


            Int32 deleteIndex = eventArgs.Item.DataSetIndex;

            population.ActivityEvents.RemoveAt (deleteIndex);

            InitializeActivityEventsGrid ();

            return;

        }

        protected void ActivityEventsGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            Client.Core.Population.PopulationEvents.PopulationActivityEvent activityEvent = EditActivityEvent;

            switch (eventArgs.CommandName) {

                case Telerik.Web.UI.RadGrid.EditCommandName:

                    activityEvent = population.ActivityEvents[eventArgs.Item.ItemIndex].Copy ();

                    ActivityScheduleTypeSelection.SelectedValue = ((Int32) activityEvent.ScheduleType).ToString ();

                    ActivityScheduleValue.Value = activityEvent.ScheduleValue;

                    ActivityScheduleQualifierSelection.SelectedValue = ((Int32) activityEvent.ScheduleQualifier).ToString ();

                    ActivityAnchorDate.SelectedValue = ((Int32) activityEvent.AnchorDate).ToString ();

                    ActivityReoccurring.Checked = activityEvent.Reoccurring;

                    ActivityActionDateTypeSelection.SelectedValue = ((Int32) activityEvent.PerformActionDateType).ToString ();

                    ActivityEventsActionSelection.SelectedValue = activityEvent.Action.Id.ToString ();

                    eventArgs.Canceled = true;

                    break;

            }

            EditActivityEvent = activityEvent;

            InitializeActivityEventsGrid ();

            InitializeActivityEventsActionParametersGrid ();

            ActivityEventsGrid.SelectedIndexes.Add (eventArgs.Item.ItemIndex);

            return;

        }

        protected void ActivityEventsActionSelection_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            Client.Core.Population.PopulationEvents.PopulationActivityEvent activityEvent = EditActivityEvent;

            if (ActivityEventsActionSelection.SelectedItem != null) {

                Int64 actionId = Int64.Parse (ActivityEventsActionSelection.SelectedValue);

                activityEvent.Action = MercuryApplication.ActionById (actionId);

                if (activityEvent.Action == null) { activityEvent.Action = new Mercury.Client.Core.Action.Action (MercuryApplication); }

            }

            EditActivityEvent = activityEvent;

            InitializeActivityEventsActionParametersGrid ();

            return;

        }


        protected void ActivityEventsActionParametersGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            switch (eventArgs.RebindReason) {

                case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                    System.Data.DataTable parameterTable = (DataTable) Session[SessionCachePrefix + "ActivityEventsActionParametersGrid.ParameterTable"];

                    parameterTable.Rows.Clear ();

                    foreach (String parameterName in EditActivityEvent.Action.ActionParameters.Keys) {

                        parameterTable.Rows.Add (parameterName, EditActivityEvent.Action.ActionParameters[parameterName].ValueDescription);

                    }


                    Session[SessionCachePrefix + "ActivityEventsActionParametersGrid.ParameterTable"] = parameterTable;

                    ActivityEventsActionParametersGrid.DataSource = parameterTable;

                    break;

            }

            return;

        }

        protected void ActivityEventsActionParametersGrid_OnItemDataBound (Object sender, Telerik.Web.UI.GridItemEventArgs eventArgs) {
            
            Telerik.Web.UI.RadComboBox activityEventParameterValueSelection;

            Telerik.Web.UI.RadTextBox activityEventParameterFixedValue;

            System.Collections.Generic.Dictionary<String, String> bindingContexts;

            String parameterName;


            if (MercuryApplication == null) { return; }


            if ((eventArgs.Item is Telerik.Web.UI.GridEditableItem) && (eventArgs.Item.IsInEditMode)) {

                Telerik.Web.UI.GridEditableItem editItem = (Telerik.Web.UI.GridEditableItem) eventArgs.Item;


                if (ActivityEventsActionSelection.SelectedItem != null) {

                    parameterName = (String) editItem.OwnerTableView.DataKeyValues[eventArgs.Item.ItemIndex]["ParameterName"];

                    activityEventParameterValueSelection = (Telerik.Web.UI.RadComboBox) eventArgs.Item.FindControl ("ActivityEventParameterValueSelection");

                    activityEventParameterValueSelection.Items.Clear ();

                    if (!EditActivityEvent.Action.ActionParameters[parameterName].Required) {

                        activityEventParameterValueSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* Not Assigned", "0"));

                    }

                    bindingContexts = population.ParameterValueSelection (EditActivityEvent.Action.ActionParameters[parameterName].DataType);

                    foreach (String currentBindingContextName in bindingContexts.Keys) {

                        activityEventParameterValueSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentBindingContextName, bindingContexts[currentBindingContextName]));

                    }


                    activityEventParameterFixedValue = (Telerik.Web.UI.RadTextBox) eventArgs.Item.FindControl ("ActivityEventParameterFixedValue");

                    activityEventParameterFixedValue.Enabled = EditActivityEvent.Action.ActionParameters[parameterName].AllowFixedValue;

                    activityEventParameterFixedValue.EmptyMessage = (EditActivityEvent.Action.ActionParameters[parameterName].AllowFixedValue) ? String.Empty : "(Not Available)";

                    activityEventParameterFixedValue.Text = String.Empty;

                }

            }

            return;

        }

        protected void ActivityEventsActionParametersGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            String parameterName;

            Telerik.Web.UI.RadComboBox parameterValueSelection;

            Telerik.Web.UI.RadTextBox parameterFixedValue;

            Client.Core.Population.PopulationEvents.PopulationActivityEvent activityEvent = EditActivityEvent;


            if (MercuryApplication == null) { return; }


            switch (eventArgs.CommandName) {

                case Telerik.Web.UI.RadGrid.EditCommandName:

                    #region Edit Command

                    Telerik.Web.UI.GridEditCommandColumn editColumn = (Telerik.Web.UI.GridEditCommandColumn) ServiceEventsThresholdsGrid.MasterTableView.GetColumn ("EditCommandColumn");

                    if (!editColumn.Visible) { editColumn.Visible = true; }

                    Telerik.Web.UI.GridEditableItem editItem = (Telerik.Web.UI.GridEditableItem) eventArgs.Item;

                    #endregion

                    break;

                case Telerik.Web.UI.RadGrid.UpdateCommandName:

                    #region Update Command

                    parameterValueSelection = (Telerik.Web.UI.RadComboBox) eventArgs.Item.FindControl ("ActivityEventParameterValueSelection");
            
                    parameterFixedValue = (Telerik.Web.UI.RadTextBox) eventArgs.Item.FindControl ("ActivityEventParameterFixedValue");
                    
                    if ((parameterValueSelection.SelectedItem != null) || (!String.IsNullOrEmpty (parameterFixedValue.Text))) {
                        
                        parameterName = (String) ((Telerik.Web.UI.GridEditableItem) eventArgs.Item).OwnerTableView.DataKeyValues[eventArgs.Item.ItemIndex]["ParameterName"];

                        if ((activityEvent.Action.ActionParameters[parameterName].AllowFixedValue) && (!String.IsNullOrEmpty (parameterFixedValue.Text))) {

                            activityEvent.Action.ActionParameters[parameterName].ValueType = Mercury.Server.Application.ActionParameterValueType.FixedValue;

                            activityEvent.Action.ActionParameters[parameterName].Value = parameterFixedValue.Text;

                            activityEvent.Action.ActionParameters[parameterName].ValueDescription = parameterFixedValue.Text;

                        }

                        else { 

                            activityEvent.Action.ActionParameters[parameterName].ValueType = Mercury.Server.Application.ActionParameterValueType.DataMapping;

                            activityEvent.Action.ActionParameters[parameterName].Value = parameterValueSelection.SelectedItem.Value;

                            activityEvent.Action.ActionParameters[parameterName].ValueDescription = parameterValueSelection.SelectedItem.Text;

                        }

                        if ((activityEvent.Action.Name == "Workflow") && (parameterName == "Workflow")) {

                            activityEvent.Action.RebindActionParameters (MercuryApplication);

                        }

                    }

                    #endregion

                    break;

            }

            EditActivityEvent = activityEvent;

            return;

        }

        #endregion


        #region Extended Properties Events

        protected void ButtonAddExtendedProperty_OnClick (Object Sender, EventArgs eventArgs) {

            if (MercuryApplication == null) { return; }


            if (!String.IsNullOrEmpty (PopulationExtendedPropertyName.Text)) {

                if (!population.ExtendedProperties.ContainsKey (PopulationExtendedPropertyName.Text)) {

                    population.ExtendedProperties.Add (PopulationExtendedPropertyName.Text, PopulationExtendedPropertyValue.Text);

                }

                else { SaveResponseLabel.Text = "Cannot add duplicate Extended Property to Work Queue."; }

            }

            InitializeExtendedPropertiesGrid ();

            return;

        }

        protected void ExtendedPropertiesGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }


            Int32 deleteIndex = eventArgs.Item.DataSetIndex;

            String extendedPropertyName = (String) eventArgs.Item.OwnerTableView.DataKeyValues[deleteIndex]["ExtendedPropertyName"];

            population.ExtendedProperties.Remove (extendedPropertyName);


            InitializeExtendedPropertiesGrid ();

            return;

        }

        #endregion 


        #region Dialog Button Event Handlers

        protected Boolean ApplyChanges () {

            Boolean success = false;

            Boolean isModified = false;

            Boolean isValid = false;

            Dictionary<String, String> validationResponse;


            if (MercuryApplication == null) { return false; }



            Mercury.Client.Core.Population.Population populationUnmodified = (Mercury.Client.Core.Population.Population) Session[SessionCachePrefix + "PopulationUnmodified"];


            population.Name = PopulationName.Text;

            population.Description = PopulationDescription.Text;

            population.PopulationTypeId = Convert.ToInt64 (PopulationTypeSelection.SelectedValue);

            population.AllowProspective = PopulationAllowProspective.Checked;

            population.InitialAnchorDate = (Mercury.Server.Application.PopulationInitialAnchorDate) Convert.ToInt32 (PopulationInitialAnchorDate.SelectedValue);

            population.Enabled = PopulationEnabled.Checked;

            population.Visible = PopulationVisible.Checked;


            if (populationUnmodified.Id == 0) { isModified = true; }

            if (!isModified) { isModified = !population.IsEqual (populationUnmodified); }


            validationResponse = population.Validate ();

            isValid = (validationResponse.Count == 0);


            if ((isModified) && (isValid)) {

                if (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.PopulationManage)) {

                    SaveResponseLabel.Text = "Permission Denied.";

                    return false;

                }

                success = MercuryApplication.PopulationSave (population);

                if (success) {

                    population = MercuryApplication.PopulationGet (population.Id, false);

                    Session[SessionCachePrefix + "Population"] = population;

                    Session[SessionCachePrefix + "PopulationUnmodified"] = population.Copy ();

                    SaveResponseLabel.Text = "Save Successful.";

                    InitializeAll ();

                }

                else {

                    SaveResponseLabel.Text = "Unable to Save Population.";

                    if (MercuryApplication.LastException != null) { SaveResponseLabel.Text = SaveResponseLabel.Text + " [" + MercuryApplication.LastException.Message + "]"; }

                    success = false;

                }

            }

            else if (!isModified) { SaveResponseLabel.Text = "No Changes Detected."; success = true; }

            else if (!isValid) {

                foreach (String validationKey in validationResponse.Keys) {

                    SaveResponseLabel.Text = "Invalid [" + validationKey + "]: " + validationResponse[validationKey];

                    break;

                }

                success = false;

            }

            return success;

        }

        protected void ButtonOk_OnClick (Object sender, EventArgs eventArgs) {

            if (ApplyChanges ()) {  Server.Transfer ("/WindowClose.aspx"); }

            return;

        }

        protected void ButtonApply_OnClick (Object sender, EventArgs eventArgs) {

            ApplyChanges ();

            return;

        }

        protected void ButtonCancel_OnClick (Object sender, EventArgs eventArgs) {

            Server.Transfer ("/WindowClose.aspx");

            return;

        }

        #endregion

    }

}
