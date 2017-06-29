using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.DataExplorer {

    public partial class DataExplorer : System.Web.UI.Page {

        #region Private Properties

        private Boolean isPageUnloading = false;

        #endregion


        #region Public Properties

        public String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (PageInstanceId.Text)) { PageInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return Form.Name + PageInstanceId.Text + ".";

            }

        }

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application)Session["Mercury.Application"];

                if ((application == null) && (!isPageUnloading)) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        private Client.Core.DataExplorer.DataExplorer Explorer { 
            
            get { return (Client.Core.DataExplorer.DataExplorer)Session[SessionCachePrefix + "DataExplorer"]; }

            set { Session[SessionCachePrefix + "DataExplorer"] = value; }

        }

        private Guid LastExecutedNodeInstanceId {

            get {

                if (Session[SessionCachePrefix + "LastExecutedNodeInstanceId"] == null) { return Guid.Empty; }

                return (Guid)Session[SessionCachePrefix + "LastExecutedNodeInstanceId"];

            }

            set { Session[SessionCachePrefix + "LastExecutedNodeInstanceId"] = value; }

        }

        private Int32 LastExecuteNodeInstanceCount {

            get {

                if (Session[SessionCachePrefix + "LastExecuteNodeInstanceCount"] == null) { return 0; }

                return (Int32)Session[SessionCachePrefix + "LastExecuteNodeInstanceCount"];

            }

            set { Session[SessionCachePrefix + "LastExecuteNodeInstanceCount"] = value; }

        }

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            Int64 forDataExplorerId = 0;


            if (MercuryApplication == null) { return; }

            if ((!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.DataExplorerReview))

                && (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.DataExplorerManage))) { Response.Redirect ("/PermissionDenied.aspx", true); return; }


            if (!Page.IsPostBack) {

                #region Initial Page Load

                if (Request.QueryString["DataExplorerId"] != null) {

                    forDataExplorerId = Int64.Parse (Request.QueryString["DataExplorerId"]);

                }

                if (forDataExplorerId != 0) {

                    Explorer = MercuryApplication.DataExplorerGet (forDataExplorerId, false);

                    if (Explorer == null) {

                        Explorer = new Mercury.Client.Core.DataExplorer.DataExplorer (MercuryApplication);

                    }

                }

                else {

                    Explorer = new Mercury.Client.Core.DataExplorer.DataExplorer (MercuryApplication);

                }

                InitializeAll ();
                
                #endregion

            } // Initial Page Load

            ApplySecurity ();

            if (!String.IsNullOrEmpty (Explorer.Name)) { Page.Title = "DataExplorer  - " + Explorer.Name; } else { Page.Title = "DataExplorer "; }

            AjaxScriptManager.AsyncPostBackTimeout = 1200;

            return;

        }

        protected void Page_Unload (object sender, EventArgs e) {

            isPageUnloading = true;

            if (MercuryApplication != null) { MercuryApplication.ApplicationClientClose (); }

            return;

        }

        #endregion


        #region Initializations

        private void InitializeAll () {

            if (Explorer == null) { Explorer = new Client.Core.DataExplorer.DataExplorer (MercuryApplication); }

            if (!String.IsNullOrEmpty (Explorer.Name)) { ApplicationTitle2.InnerText = "Data Explorer  - " + Explorer.Name; } else { Page.Title = "** Data Explorer"; }


            DataExplorerName.Text = Explorer.Name;

            DataExplorerDescription.Text = Explorer.Description;

            DataExplorerIsPublic.SelectedValue = Explorer.IsPublic.ToString ();


            InitializeDataExplorerTreeView ();

            return;

        }

        private void InitializeDataExplorerTreeView () {

            Telerik.Web.UI.RadTreeNode variableRoot = new Telerik.Web.UI.RadTreeNode ();

            variableRoot.Text = "Variables";

            variableRoot.Value = "VariableRoot";

            variableRoot.Checkable = false;

            variableRoot.ImageUrl = @"/Images/Common16/Variable.png";

            variableRoot.ContextMenuID = "ContextMenuDataExplorerVariable";


            Telerik.Web.UI.RadTreeNode rootNode = new Telerik.Web.UI.RadTreeNode ();

            DataExplorerTreeView_InitializeNode (rootNode, Explorer.RootNode);


            DataExplorerTreeView.Nodes.Clear ();

            DataExplorerTreeView.Nodes.Add (variableRoot);

            DataExplorerTreeView_InitializeVariables ();

            DataExplorerTreeView.Nodes.Add (rootNode);

            DataExplorerTreeView_AppendChildren (rootNode, Explorer.RootNode);

            
            DataExplorerTreeView.Nodes[1].Selected = true;

            InitializeProperties ();
                       
            return;

        }

        private void ApplySecurity () {

            return;

        }


        private void InitializeVariableProperties () {

            DateTime startTime = DateTime.Now;


            if (DataExplorerTreeView.SelectedNode == null) { return; }

            if (DataExplorerTreeView.SelectedNode.Value.Split ('_').Length < 2) { return; }

            String selectedVariableName = DataExplorerTreeView.SelectedNode.Value.Split ('_')[1];


            Mercury.Server.Application.DataExplorerVariable variable = Explorer.Variables[selectedVariableName];

            if (variable == null) { return; }


            #region Reset Properties

            PropertiesRow_VariablePropertiesVariableTextValue.Visible = false;

            VariablePropertiesVariableTextValue.Text = String.Empty;

            PropertiesRow_VariablePropertiesVariableNumericValue.Visible = false;

            VariablePropertiesVariableNumericValue.Value = 0;

            PropertiesRow_VariablePropertiesVariableDatePicker.Visible = false;

            VariablePropertiesVariableDatePicker.SelectedDate = null;

            PropertiesRow_VariablePropertiesVariableDateFunctionSelection.Visible = false;

            VariablePropertiesVariableDateFunctionSelection.SelectedValue = "DateTime.Today";

            #endregion 


            VariablePropertiesVariableName.Text = variable.Name;

            VariablePropertiesVariableDataType.SelectedValue = ((Int32)variable.VariableType).ToString ();

            switch (variable.VariableType) {

                case Mercury.Server.Application.DataExplorerVariableType.Text:

                    PropertiesRow_VariablePropertiesVariableTextValue.Visible = true;

                    VariablePropertiesVariableTextValue.Text = variable.TextValue;

                    break;

                case Mercury.Server.Application.DataExplorerVariableType.Numeric:
                    
                    PropertiesRow_VariablePropertiesVariableNumericValue.Visible = true;

                    VariablePropertiesVariableNumericValue.Value = Convert.ToDouble (variable.NumericValue);

                    break;

                case Mercury.Server.Application.DataExplorerVariableType.Date:
                            
                    PropertiesRow_VariablePropertiesVariableDatePicker.Visible = true;

                    VariablePropertiesVariableDatePicker.SelectedDate = variable.DateValue;

                    PropertiesRow_VariablePropertiesVariableDateFunctionSelection.Visible = true;

                    VariablePropertiesVariableDateFunctionSelection.SelectedValue = variable.TextValue;

                    break;

            }


            System.Diagnostics.Debug.WriteLine ("Initialize Properties: " + DateTime.Now.Subtract (startTime).TotalMilliseconds.ToString ());

            return;

        }

        private void InitializeProperties_AgeCriteria1 (Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationAge ageCriteria) {

            AgeCriteria1AgeMinimumRow.Visible = ageCriteria.UseAgeCriteria;

            AgeCriteria1AgeMaximumRow.Visible = ageCriteria.UseAgeCriteria;

            AgeCriteria1AgeQualifierRow.Visible = ageCriteria.UseAgeCriteria;


            AgeCriteria1AgeMinimumRow.Visible = ageCriteria.UseAgeCriteria;

            AgeCriteria1AgeMaximumRow.Visible = ageCriteria.UseAgeCriteria;

            AgeCriteria1AgeQualifierRow.Visible = ageCriteria.UseAgeCriteria;


            AgeCriteria1UseAgeCriteria.Checked = ageCriteria.UseAgeCriteria;

            AgeCriteria1AgeMinimum.Value = ageCriteria.AgeMinimum;

            AgeCriteria1AgeMaximum.Value = ageCriteria.AgeMaximum;

            AgeCriteria1AgeQualifierSelection.SelectedValue = ((Int32)ageCriteria.AgeQualifier).ToString ();

            return;

        }

        private void InitializeProperties_DateCriteria1 (Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationDate dateCriteria) {

            DateCriteria1DateTypeSelection.SelectedValue = ((Int32)dateCriteria.DateType).ToString ();


            // SET UP START DATE 

            DateCriteria1StartDatePicker.SelectedDate = dateCriteria.StartDate;

            DateCriteria1StartDateVariableSelection.Items.Clear ();

            // DateCriteria1StartDateVariableSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("Not Specified", ""));

            DateCriteria1StartDateVariableSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("DateTime.Today", "DateTime.Today"));

            DateCriteria1StartDateVariableSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("DateTime.FirstDayOfYear", "DateTime.FirstDayOfYear"));

            DateCriteria1StartDateVariableSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("DateTime.LastDayOfYear", "DateTime.LastDayOfYear"));

            // ADD VARIABLE NAMES HERE 

            foreach (Mercury.Server.Application.DataExplorerVariable currentVariable in Explorer.Variables.Values) {

                if (currentVariable.VariableType == Mercury.Server.Application.DataExplorerVariableType.Date) {

                    DateCriteria1StartDateVariableSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("Variable: " + currentVariable.Name, currentVariable.Name));

                }

            }


            DateCriteria1StartDateVariableSelection.SelectedValue = dateCriteria.StartDateVariableName;
            
            DateCriteria1StartDateRelativeValue.Value = dateCriteria.StartDateRelativeValue;

            DateCriteria1StartDateRelativeQualifier.SelectedValue = ((Int32) dateCriteria.StartDateRelativeQualifier).ToString ();

            
            // SET UP END DATE 

            DateCriteria1EndDatePicker.SelectedDate = dateCriteria.EndDate;

            DateCriteria1EndDateVariableSelection.Items.Clear ();

            // DateCriteria1EndDateVariableSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("Not Specified", ""));

            DateCriteria1EndDateVariableSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("DateTime.Today", "DateTime.Today"));

            DateCriteria1EndDateVariableSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("DateTime.FirstDayOfYear", "DateTime.FirstDayOfYear"));

            DateCriteria1EndDateVariableSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("DateTime.LastDayOfYear", "DateTime.LastDayOfYear"));

            // ADD VARIABLE NAMES HERE 

            foreach (Mercury.Server.Application.DataExplorerVariable currentVariable in Explorer.Variables.Values) {

                if (currentVariable.VariableType == Mercury.Server.Application.DataExplorerVariableType.Date) {

                    DateCriteria1EndDateVariableSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("Variable: " + currentVariable.Name, currentVariable.Name));

                }

            }

            DateCriteria1EndDateVariableSelection.SelectedValue = dateCriteria.EndDateVariableName;
            
            DateCriteria1EndDateRelativeValue.Value = dateCriteria.EndDateRelativeValue;

            DateCriteria1EndDateRelativeQualifier.SelectedValue = ((Int32) dateCriteria.EndDateRelativeQualifier).ToString ();


            NodePropertiesRow_DateCriteria1EndDatePanel.Visible = false;

            switch (dateCriteria.DateType) {

                case Mercury.Server.Application.DataExplorerEvaluationDateType.BetweenDatesAbsolute:

                case Mercury.Server.Application.DataExplorerEvaluationDateType.BetweenDatesRelative:

                    NodePropertiesRow_DateCriteria1EndDatePanel.Visible = true;

                    break;

            }

            return;

        }

        /// <summary>
        /// This method initializes the Node Properties Section based on the selected Explorer Node. It is 
        /// responsible for showing and hidding page elements and assigning values from the node.
        /// </summary>
        private void InitializeProperties () {

            DateTime startTime = DateTime.Now;


            if (DataExplorerTreeView.SelectedNode == null) { return; }

            Guid selectedNodeValue = Guid.Empty;

            Guid.TryParse (DataExplorerTreeView.SelectedNode.Value, out selectedNodeValue);

            if (selectedNodeValue == Guid.Empty) { return; }


            Client.Core.DataExplorer.DataExplorerNode dataExplorerNode = Explorer.FindNode (selectedNodeValue);

            if (dataExplorerNode == null) { return; }


            #region Reset Properties 

            NodePropertiesRow_SetType.Visible = false;


            MemberDemographicEvaluationPanel.Visible = false;
            
            MemberEnrollmentEvaluationPanel.Visible = false;

            MemberEnrollmentContinuousFromBirthDateEvaluationPanel.Visible = false;

            MemberMetricEvaluationPanel.Visible = false;

            MemberServiceEvaluationPanel.Visible = false;


            PopulationMembershipEvaluationPanel.Visible = false;

            PopulationMembershipEvalationPopulationSelectionRow.Visible = false;

            PopulationMembershipEvalationPopulationTypeSelectionRow.Visible = false;


            AgeCriteria1Panel.Visible = false;

            AgeCriteria1NameLabel.Text = String.Empty;


            DateCriteria1Panel.Visible = false;

            DateCriteria1NameLabel.Text = String.Empty;

            NodePropertiesRow_DateCriteria1EndDatePanel.Visible = false;

            #endregion 

            
            #region Set Specific Node Settings

            switch (dataExplorerNode.NodeType) {

                case Mercury.Server.Application.DataExplorerNodeType.Set:

                    Client.Core.DataExplorer.DataExplorerNodeSet dataExplorerNodeSet = (Client.Core.DataExplorer.DataExplorerNodeSet)dataExplorerNode;

                    NodePropertiesRow_SetType.Visible = true;

                    NodePropertiesSetType.SelectedValue = ((Int32)dataExplorerNodeSet.SetType).ToString ();

                    break;

                case Mercury.Server.Application.DataExplorerNodeType.Evaluation:

                    Client.Core.DataExplorer.DataExplorerNodeEvaluation dataExplorerNodeEvaluation = (Client.Core.DataExplorer.DataExplorerNodeEvaluation)dataExplorerNode;

                    switch (dataExplorerNodeEvaluation.EvaluationType) {

                        case Mercury.Server.Application.DataExplorerEvaluationType.MemberDemographic:

                            #region Member Demographic

                            Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationMemberDemographic dataExplorerNodeEvaluationMemberDemographic = (Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationMemberDemographic)dataExplorerNode;

                            MemberDemographicEvaluationPanel.Visible = true;

                            AgeCriteria1Panel.Visible = true;

                            DateCriteria1Panel.Visible = true;



                            MemberDemographicEvaluationGenderSelection.SelectedValue = ((Int32)dataExplorerNodeEvaluationMemberDemographic.Gender).ToString ();


                            MemberDemographicEthnicitySelection.SelectedValue = String.Empty;

                            MemberDemographicEthnicitySelection.DataSource = MercuryApplication.CoreObjectDictionary ("Ethnicity", true);

                            MemberDemographicEthnicitySelection.DataTextField = "Value";

                            MemberDemographicEthnicitySelection.DataValueField = "Key";

                            MemberDemographicEthnicitySelection.DataBind ();

                            MemberDemographicEthnicitySelection.SelectedValue = dataExplorerNodeEvaluationMemberDemographic.EthnicityId.ToString ();


                            InitializeProperties_AgeCriteria1 (dataExplorerNodeEvaluationMemberDemographic.AgeCriteria);


                            DateCriteria1NameLabel.Text = "Age";

                            InitializeProperties_DateCriteria1 (dataExplorerNodeEvaluationMemberDemographic.DateCriteria);

                            DateCriteria1Panel.Visible = dataExplorerNodeEvaluationMemberDemographic.AgeCriteria.UseAgeCriteria;

                            #endregion 

                            break;

                        case Mercury.Server.Application.DataExplorerEvaluationType.MemberEnrollment:

                            #region Member Enrollment

                            Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationMemberEnrollment dataExplorerNodeEvaluationMemberEnrollment = (Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationMemberEnrollment)dataExplorerNode;

                            MemberEnrollmentEvaluationPanel.Visible = true;

                            DateCriteria1Panel.Visible = true;



                            MemberEnrollmentEvaluationInsurerSelection.SelectedValue = String.Empty;

                            MemberEnrollmentEvaluationInsurerSelection.DataSource = MercuryApplication.CoreObjectDictionary ("Insurer", true);

                            MemberEnrollmentEvaluationInsurerSelection.DataTextField = "Value";

                            MemberEnrollmentEvaluationInsurerSelection.DataValueField = "Key";

                            MemberEnrollmentEvaluationInsurerSelection.DataBind ();

                            MemberEnrollmentEvaluationInsurerSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** Not Specified", "0"));

                            MemberEnrollmentEvaluationInsurerSelection.Sort = Telerik.Web.UI.RadComboBoxSort.Ascending;

                            MemberEnrollmentEvaluationInsurerSelection.SelectedValue = dataExplorerNodeEvaluationMemberEnrollment.InsurerId.ToString ();
                            

                            MemberEnrollmentEvaluationProgramSelection.SelectedValue = String.Empty;

                            MemberEnrollmentEvaluationProgramSelection.DataSource = MercuryApplication.ProgramDictionaryByInsurer (dataExplorerNodeEvaluationMemberEnrollment.InsurerId, true);

                            MemberEnrollmentEvaluationProgramSelection.DataTextField = "Value";

                            MemberEnrollmentEvaluationProgramSelection.DataValueField = "Key";

                            MemberEnrollmentEvaluationProgramSelection.DataBind ();

                            MemberEnrollmentEvaluationProgramSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** Not Specified", "0"));

                            MemberEnrollmentEvaluationProgramSelection.Sort = Telerik.Web.UI.RadComboBoxSort.Ascending;

                            MemberEnrollmentEvaluationProgramSelection.SelectedValue = dataExplorerNodeEvaluationMemberEnrollment.ProgramId.ToString ();

                            
                            MemberEnrollmentEvaluationBenefitPlanSelection.SelectedValue = String.Empty;

                            MemberEnrollmentEvaluationBenefitPlanSelection.DataSource = MercuryApplication.BenefitPlanDictionaryByProgram (dataExplorerNodeEvaluationMemberEnrollment.ProgramId, true);

                            MemberEnrollmentEvaluationBenefitPlanSelection.DataTextField = "Value";

                            MemberEnrollmentEvaluationBenefitPlanSelection.DataValueField = "Key";

                            MemberEnrollmentEvaluationBenefitPlanSelection.DataBind ();

                            MemberEnrollmentEvaluationBenefitPlanSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** Not Specified", "0"));

                            MemberEnrollmentEvaluationBenefitPlanSelection.Sort = Telerik.Web.UI.RadComboBoxSort.Ascending;

                            MemberEnrollmentEvaluationBenefitPlanSelection.SelectedValue = dataExplorerNodeEvaluationMemberEnrollment.BenefitPlanId.ToString ();


                            MemberEnrollmentEvaluationContinuousEnrollmentRow.Visible = (dataExplorerNodeEvaluationMemberEnrollment.ProgramId != 0);

                            MemberEnrollmentEvaluationContinuousEnrollment.Checked = dataExplorerNodeEvaluationMemberEnrollment.ContinuousEnrollment;

                            MemberEnrollmentEvaluationContinuousAllowedGapsRow.Visible = dataExplorerNodeEvaluationMemberEnrollment.ContinuousEnrollment && (dataExplorerNodeEvaluationMemberEnrollment.ProgramId != 0);

                            MemberEnrollmentEvaluationContinuousAllowedGapDaysRow.Visible = dataExplorerNodeEvaluationMemberEnrollment.ContinuousEnrollment && (dataExplorerNodeEvaluationMemberEnrollment.ProgramId != 0);

                            MemberEnrollmentEvaluationContinuousAllowedGaps.Value = Convert.ToDouble (dataExplorerNodeEvaluationMemberEnrollment.ContinuousAllowedGaps);

                            MemberEnrollmentEvaluationContinuousAllowedGapDays.Value = Convert.ToDouble (dataExplorerNodeEvaluationMemberEnrollment.ContinuousAllowedGapDays);


                            DateCriteria1NameLabel.Text = "Enrollment";

                            InitializeProperties_DateCriteria1 (dataExplorerNodeEvaluationMemberEnrollment.DateCriteria);

                            DateCriteria1Panel.Visible = true;

                            #endregion

                            break;

                        case Mercury.Server.Application.DataExplorerEvaluationType.MemberEnrollmentContinuousFromBirthDate:

                            #region Member EnrollmentContinuousFromBirthDate

                            Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate dataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate = (Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate)dataExplorerNode;

                            MemberEnrollmentContinuousFromBirthDateEvaluationPanel.Visible = true;


                            MemberEnrollmentContinuousFromBirthDateEvaluationInsurerSelection.SelectedValue = String.Empty;

                            MemberEnrollmentContinuousFromBirthDateEvaluationInsurerSelection.DataSource = MercuryApplication.CoreObjectDictionary ("Insurer", true);

                            MemberEnrollmentContinuousFromBirthDateEvaluationInsurerSelection.DataTextField = "Value";

                            MemberEnrollmentContinuousFromBirthDateEvaluationInsurerSelection.DataValueField = "Key";

                            MemberEnrollmentContinuousFromBirthDateEvaluationInsurerSelection.DataBind ();

                            MemberEnrollmentContinuousFromBirthDateEvaluationInsurerSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** Not Specified", "0"));

                            MemberEnrollmentContinuousFromBirthDateEvaluationInsurerSelection.Sort = Telerik.Web.UI.RadComboBoxSort.Ascending;

                            MemberEnrollmentContinuousFromBirthDateEvaluationInsurerSelection.SelectedValue = dataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate.InsurerId.ToString ();


                            MemberEnrollmentContinuousFromBirthDateEvaluationProgramSelection.SelectedValue = String.Empty;

                            MemberEnrollmentContinuousFromBirthDateEvaluationProgramSelection.DataSource = MercuryApplication.ProgramDictionaryByInsurer (dataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate.InsurerId, true);

                            MemberEnrollmentContinuousFromBirthDateEvaluationProgramSelection.DataTextField = "Value";

                            MemberEnrollmentContinuousFromBirthDateEvaluationProgramSelection.DataValueField = "Key";

                            MemberEnrollmentContinuousFromBirthDateEvaluationProgramSelection.DataBind ();

                            MemberEnrollmentContinuousFromBirthDateEvaluationProgramSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** Not Specified", "0"));

                            MemberEnrollmentContinuousFromBirthDateEvaluationProgramSelection.Sort = Telerik.Web.UI.RadComboBoxSort.Ascending;

                            MemberEnrollmentContinuousFromBirthDateEvaluationProgramSelection.SelectedValue = dataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate.ProgramId.ToString ();


                            MemberEnrollmentContinuousFromBirthDateEvaluationBenefitPlanSelection.SelectedValue = String.Empty;

                            MemberEnrollmentContinuousFromBirthDateEvaluationBenefitPlanSelection.DataSource = MercuryApplication.BenefitPlanDictionaryByProgram (dataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate.ProgramId, true);

                            MemberEnrollmentContinuousFromBirthDateEvaluationBenefitPlanSelection.DataTextField = "Value";

                            MemberEnrollmentContinuousFromBirthDateEvaluationBenefitPlanSelection.DataValueField = "Key";

                            MemberEnrollmentContinuousFromBirthDateEvaluationBenefitPlanSelection.DataBind ();

                            MemberEnrollmentContinuousFromBirthDateEvaluationBenefitPlanSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** Not Specified", "0"));

                            MemberEnrollmentContinuousFromBirthDateEvaluationBenefitPlanSelection.Sort = Telerik.Web.UI.RadComboBoxSort.Ascending;

                            MemberEnrollmentContinuousFromBirthDateEvaluationBenefitPlanSelection.SelectedValue = dataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate.BenefitPlanId.ToString ();


                            MemberEnrollmentContinuousFromBirthDateEvaluationContinuousUntilAge.Value = Convert.ToDouble (dataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate.ContinuousUntilAge);

                            MemberEnrollmentContinuousFromBirthDateEvaluationContinuousAllowedGaps.Value = Convert.ToDouble (dataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate.ContinuousAllowedGaps);

                            MemberEnrollmentContinuousFromBirthDateEvaluationContinuousAllowedGapDays.Value = Convert.ToDouble (dataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate.ContinuousAllowedGapDays);

                            #endregion

                            break;

                        case Mercury.Server.Application.DataExplorerEvaluationType.MemberMetric:

                            #region Member Metric

                            Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationMemberMetric dataExplorerNodeEvaluationMemberMetric = (Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationMemberMetric)dataExplorerNode;

                            MemberMetricEvaluationPanel.Visible = true;

                            AgeCriteria1Panel.Visible = true;

                            DateCriteria1Panel.Visible = true;



                            MemberMetricEvaluationMetricSelection.SelectedValue = String.Empty;

                            MemberMetricEvaluationMetricSelection.DataSource = MercuryApplication.MetricsAvailable (true);

                            MemberMetricEvaluationMetricSelection.DataTextField = "Name";

                            MemberMetricEvaluationMetricSelection.DataValueField = "Id";

                            MemberMetricEvaluationMetricSelection.DataBind ();

                            MemberMetricEvaluationMetricSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** Not Specified", "0"));

                            MemberMetricEvaluationMetricSelection.Sort = Telerik.Web.UI.RadComboBoxSort.Ascending;

                            MemberMetricEvaluationMetricSelection.SelectedValue = dataExplorerNodeEvaluationMemberMetric.MetricId.ToString ();


                            MemberMetricEvaluationValueMinimum.Value = Convert.ToDouble (dataExplorerNodeEvaluationMemberMetric.ValueMinimum);

                            MemberMetricEvaluationValueMaximum.Value = Convert.ToDouble (dataExplorerNodeEvaluationMemberMetric.ValueMaximum);

                            MemberMetricEvaluationCountOf.Value = dataExplorerNodeEvaluationMemberMetric.CountOf;


                            AgeCriteria1NameLabel.Text = "Age on Event Date";

                            InitializeProperties_AgeCriteria1 (dataExplorerNodeEvaluationMemberMetric.AgeCriteria);

                            DateCriteria1NameLabel.Text = "Event Date";

                            InitializeProperties_DateCriteria1 (dataExplorerNodeEvaluationMemberMetric.DateCriteria);

                            #endregion

                            break;

                        case Mercury.Server.Application.DataExplorerEvaluationType.MemberService:

                            #region Member Service

                            Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationMemberService dataExplorerNodeEvaluationMemberService = (Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationMemberService)dataExplorerNode;

                            MemberServiceEvaluationPanel.Visible = true;

                            AgeCriteria1Panel.Visible = true;

                            DateCriteria1Panel.Visible = true;



                            MemberServiceEvaluationServiceSelection.SelectedValue = String.Empty;

                            MemberServiceEvaluationServiceSelection.DataSource = MercuryApplication.MedicalServiceHeadersGet (true);

                            MemberServiceEvaluationServiceSelection.DataTextField = "Name";

                            MemberServiceEvaluationServiceSelection.DataValueField = "Id";

                            MemberServiceEvaluationServiceSelection.DataBind ();

                            MemberServiceEvaluationServiceSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** Not Specified", "0"));

                            MemberServiceEvaluationServiceSelection.Sort = Telerik.Web.UI.RadComboBoxSort.Ascending;

                            MemberServiceEvaluationServiceSelection.SelectedValue = dataExplorerNodeEvaluationMemberService.ServiceId.ToString ();


                            MemberServiceEvaluationCountOf.Value = dataExplorerNodeEvaluationMemberService.CountOf;


                            AgeCriteria1NameLabel.Text = "Age on Event Date";

                            InitializeProperties_AgeCriteria1 (dataExplorerNodeEvaluationMemberService.AgeCriteria);

                            DateCriteria1NameLabel.Text = "Event Date";

                            InitializeProperties_DateCriteria1 (dataExplorerNodeEvaluationMemberService.DateCriteria);

                            #endregion

                            break;

                        case Mercury.Server.Application.DataExplorerEvaluationType.PopulationMembership:

                            #region Population Membership

                            Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationPopulationMembership dataExplorerNodeEvaluationPopulationMembership = (Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationPopulationMembership) dataExplorerNode;

                            PopulationMembershipEvaluationPanel.Visible = true;

                            DateCriteria1Panel.Visible = true;


                            PopulationMembershipEvaluationFunction.SelectedValue = ((Int32)dataExplorerNodeEvaluationPopulationMembership.Evaluation).ToString ();

                            PopulationMembershipEvaluationTypeSelection.SelectedValue = ((Int32)dataExplorerNodeEvaluationPopulationMembership.PopulationEvaluationType).ToString ();


                            PopulationMembershipEvalationPopulationSelectionRow.Visible = (dataExplorerNodeEvaluationPopulationMembership.PopulationEvaluationType == Mercury.Server.Application.DataExplorerNodeEvaluationPopulationEvaluationType.Population);

                            PopulationMembershipEvalationPopulationTypeSelectionRow.Visible = (dataExplorerNodeEvaluationPopulationMembership.PopulationEvaluationType == Mercury.Server.Application.DataExplorerNodeEvaluationPopulationEvaluationType.PopulationType);



                            // ADD "NOT SPECIFIED" TO THE SELECTION LIST TO DENOTE UNSELECTED POPULATION

                            PopulationMembershipEvaluationPopulationSelection.SelectedValue = String.Empty;

                            PopulationMembershipEvaluationPopulationSelection.DataSource = MercuryApplication.PopulationsAvailable (true);

                            PopulationMembershipEvaluationPopulationSelection.DataTextField = "Name";

                            PopulationMembershipEvaluationPopulationSelection.DataValueField = "Id";

                            PopulationMembershipEvaluationPopulationSelection.DataBind ();

                            PopulationMembershipEvaluationPopulationSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** Not Specified", "0"));

                            PopulationMembershipEvaluationPopulationSelection.Sort = Telerik.Web.UI.RadComboBoxSort.Ascending;

                            PopulationMembershipEvaluationPopulationSelection.SelectedValue = dataExplorerNodeEvaluationPopulationMembership.PopulationId.ToString ();


                            PopulationMembershipEvaluationPopulationTypeSelection.DataSource = MercuryApplication.PopulationTypesAvailable (true);

                            PopulationMembershipEvaluationPopulationTypeSelection.DataTextField = "Name";

                            PopulationMembershipEvaluationPopulationTypeSelection.DataValueField = "Id";

                            PopulationMembershipEvaluationPopulationTypeSelection.DataBind ();

                            PopulationMembershipEvaluationPopulationTypeSelection.SelectedValue = dataExplorerNodeEvaluationPopulationMembership.PopulationTypeId.ToString ();


                            DateCriteria1NameLabel.Text = Mercury.Server.CommonFunctions.EnumerationToString (dataExplorerNodeEvaluationPopulationMembership.PopulationEvaluationType);

                            InitializeProperties_DateCriteria1 (dataExplorerNodeEvaluationPopulationMembership.DateCriteria);

                            #endregion 

                            break;

                    }

                    break;

            }

            #endregion 


            System.Diagnostics.Debug.WriteLine ("Initialize Properties: " + DateTime.Now.Subtract (startTime).TotalMilliseconds.ToString ());

            return;

        }

        #endregion


        #region Data Explorer Tree Support Methods

        private void DataExplorerTreeView_InitializeVariableNode (Telerik.Web.UI.RadTreeNode treeNode, Mercury.Server.Application.DataExplorerVariable variable) {

            treeNode.ImageUrl = "/Images/Common16/Variable" + variable.VariableType.ToString () + ".png";

            treeNode.Text = ((String.IsNullOrWhiteSpace (variable.Name)) ? "Unnamed " : variable.Name) + " [" + Mercury.Server.CommonFunctions.EnumerationToString (variable.VariableType) + "]";

            treeNode.Value = "Variable_" + variable.Name;

            treeNode.Checkable = false;

            treeNode.ContextMenuID = "ContextMenuDataExplorerVariable";

            return;

        }

        private void DataExplorerTreeView_InitializeVariables () {

            Telerik.Web.UI.RadTreeNode variableRootNode = DataExplorerTreeView.FindNodeByValue ("VariableRoot");

            variableRootNode.Nodes.Clear ();


            foreach (Mercury.Server.Application.DataExplorerVariable currentVariable in Explorer.Variables.Values) {

                Telerik.Web.UI.RadTreeNode treeNode = new Telerik.Web.UI.RadTreeNode ();

                DataExplorerTreeView_InitializeVariableNode (treeNode, currentVariable);

                variableRootNode.Nodes.Add (treeNode);
                
            }

            return;

        }

        private void DataExplorerTreeView_InitializeNode (Telerik.Web.UI.RadTreeNode treeNode, Client.Core.DataExplorer.DataExplorerNode explorerNode) {

            // ASSIGN COMMON VALUES FIRST

            treeNode.Value = explorerNode.NodeInstanceId.ToString (); // UNIQUE VALUE

            treeNode.AllowDrag = true;

            // ASSIGN NODE SPECIFIC VALUES NEXT

            switch (explorerNode.NodeType) {

                case Mercury.Server.Application.DataExplorerNodeType.Set:

                    Client.Core.DataExplorer.DataExplorerNodeSet dataExplorerNodeSet = (Client.Core.DataExplorer.DataExplorerNodeSet) explorerNode;

                    treeNode.ImageUrl = "/Images/Common16/DataExplorerSet" + dataExplorerNodeSet.SetType.ToString () + ".png";

                    treeNode.Text = ((String.IsNullOrWhiteSpace (dataExplorerNodeSet.Name)) ? "Unnamed " : dataExplorerNodeSet.Name) + " [" + Mercury.Server.CommonFunctions.EnumerationToString (dataExplorerNodeSet.SetType) + "]";

                    treeNode.Checkable = false;

                    treeNode.ContextMenuID = "ContextMenuDataExplorerNodeSet";

                    treeNode.AllowDrop = true;
                    
                    break;

                case Mercury.Server.Application.DataExplorerNodeType.Evaluation:

                    Client.Core.DataExplorer.DataExplorerNodeEvaluation dataExplorerNodeEvaluation = (Client.Core.DataExplorer.DataExplorerNodeEvaluation)explorerNode;
                    
                    treeNode.ImageUrl = "/Images/Common16/DataExplorerEvaluation.png";

                    treeNode.Text = ((String.IsNullOrWhiteSpace (dataExplorerNodeEvaluation.Name)) ? "Unnamed " : dataExplorerNodeEvaluation.Name) + " [" + Mercury.Server.CommonFunctions.EnumerationToString (dataExplorerNodeEvaluation.EvaluationType) + "]";

                    treeNode.Checkable = false;

                    treeNode.ContextMenuID = "ContextMenuDataExplorerNodeEvaluation";

                    break; 

            }

            return;

        }

        private void DataExplorerTreeView_AppendChildren (Telerik.Web.UI.RadTreeNode parentTreeNode, Client.Core.DataExplorer.DataExplorerNode parentExplorerNode) {

            if (parentExplorerNode.Children == null) { return; }


            foreach (Client.Core.DataExplorer.DataExplorerNode currentChildNode in parentExplorerNode.Children) {

                Telerik.Web.UI.RadTreeNode treeNode = new Telerik.Web.UI.RadTreeNode ();

                DataExplorerTreeView_InitializeNode (treeNode, currentChildNode);

                parentTreeNode.Nodes.Add (treeNode);

                DataExplorerTreeView_AppendChildren (treeNode, currentChildNode);

            }

            return;

        }

        #endregion 


        #region DataExplorerTreeView Events

        protected void DataExplorerTreeView_OnNodeClick_VariableNode (Object sender, Telerik.Web.UI.RadTreeNodeEventArgs e) {
            
            NodeVariablesDiv.Visible = true;

            NodePropertiesDiv.Visible = false;

            if (e.Node.Value == "VariableRoot") { NodeVariablesDiv.Visible = false; return; }

            InitializeVariableProperties ();

            return;

        }

        protected void DataExplorerTreeView_OnNodeClick (Object sender, Telerik.Web.UI.RadTreeNodeEventArgs e) {

            // HANDLE VARIABLE NODE TREE IN A SEPARATE METHOD 

            if (e.Node.Value.StartsWith ("Variable")) { DataExplorerTreeView_OnNodeClick_VariableNode (sender, e); return; }


            // SHOW NODE PROPERTIES, HIDE VARIABLE PROPERTIES

            NodeVariablesDiv.Visible = false;

            NodePropertiesDiv.Visible = true;


            // VALIDATE THAT THE SELECTED NODE VALUE IS A GUID

            Guid selectedNodeValue = Guid.Empty;

            Guid.TryParse (e.Node.Value, out selectedNodeValue);

            if (selectedNodeValue == Guid.Empty) { return; }


            // FIND THE ACTUAL NODE (BASE OBJECT)

            Client.Core.DataExplorer.DataExplorerNode dataExplorerNode = Explorer.FindNode (selectedNodeValue);

            if (dataExplorerNode == null) { return; }


            // ASSIGN NODE PROPERTIES

            NodePropertiesNodeType.Text = Mercury.Server.CommonFunctions.EnumerationToString (dataExplorerNode.NodeType);

            NodePropertiesName.Text = dataExplorerNode.Name;


            // ASSIGN NODE SPECIFIC VALUES NEXT

            switch (dataExplorerNode.NodeType) {

                case Mercury.Server.Application.DataExplorerNodeType.Set:
                    
                    Client.Core.DataExplorer.DataExplorerNodeSet dataExplorerNodeSet = (Client.Core.DataExplorer.DataExplorerNodeSet) dataExplorerNode;

                    NodePropertiesSetType.SelectedValue = ((Int32)dataExplorerNodeSet.SetType).ToString ();

                    break;

                case Mercury.Server.Application.DataExplorerNodeType.Evaluation:

                    break;

            }


            InitializeProperties ();

            return;

        }

        protected void DataExplorerTreeView_OnContextMenuItemClick_VariableNode (Object sender, Telerik.Web.UI.RadTreeViewContextMenuEventArgs e) {

            Telerik.Web.UI.RadTreeNode clickedNode = e.Node;

            switch (e.MenuItem.Value) {

                case "AddVariable":

                    Explorer.AddVariable ();

                    DataExplorerTreeView_InitializeVariables ();

                    clickedNode.Expanded = true;

                    if (clickedNode.Nodes.Count > 0) {

                        clickedNode.Nodes[clickedNode.Nodes.Count - 1].Selected = true;

                    }
                    
                    InitializeVariableProperties ();

                    break;

                case "Delete":

                    String variableName = e.Node.Value.Replace ("Variable_", "");

                    Explorer.RemoveVariable (variableName);

                    clickedNode.ParentNode.Selected = true;

                    DataExplorerTreeView_InitializeVariables ();

                    InitializeVariableProperties ();

                    break;

            }

            return;

        }

        protected void DataExplorerTreeView_OnContextMenuItemClick (Object sender, Telerik.Web.UI.RadTreeViewContextMenuEventArgs e) {

            // HANDLE VARIABLE NODE TREE IN A SEPARATE METHOD 

            if (e.Node.Value.StartsWith ("Variable")) { DataExplorerTreeView_OnContextMenuItemClick_VariableNode (sender, e); return; }


            Telerik.Web.UI.RadTreeNode clickedNode = e.Node;

            Client.Core.DataExplorer.DataExplorerNodeSet dataExplorerNodeSet;


            // VALIDATE THAT THE SELECTED NODE VALUE IS A GUID

            Guid selectedNodeValue = Guid.Empty;

            Guid.TryParse (e.Node.Value, out selectedNodeValue);

            if (selectedNodeValue == Guid.Empty) { return; }


            // FIND THE ACTUAL NODE (BASE OBJECT), VALIDATE THAT IT IS A SET NODE, CONVERT TO ACTUAL OBJECT

            Client.Core.DataExplorer.DataExplorerNode dataExplorerNode = Explorer.FindNode (selectedNodeValue);

            if (dataExplorerNode == null) { return; }


            switch (e.MenuItem.Value) {

                case "AddSet":

                    if (dataExplorerNode.NodeType != Mercury.Server.Application.DataExplorerNodeType.Set) { return; }
                    
                    dataExplorerNodeSet = (Client.Core.DataExplorer.DataExplorerNodeSet)dataExplorerNode;

                    dataExplorerNodeSet.AddNodeSet (Mercury.Server.Application.DataExplorerSetType.Intersection);

                    clickedNode.Nodes.Clear ();

                    DataExplorerTreeView_AppendChildren (clickedNode, dataExplorerNodeSet);

                    clickedNode.Expanded = true;

                    clickedNode.Selected = true;

                    InitializeProperties ();

                    break;

                case "AddEvaluationMemberDemographic":
                    
                    if (dataExplorerNode.NodeType != Mercury.Server.Application.DataExplorerNodeType.Set) { return; }
                    
                    dataExplorerNodeSet = (Client.Core.DataExplorer.DataExplorerNodeSet)dataExplorerNode;

                    dataExplorerNodeSet.AddNodeEvaluation (Mercury.Server.Application.DataExplorerEvaluationType.MemberDemographic);
                    
                    clickedNode.Nodes.Clear ();

                    DataExplorerTreeView_AppendChildren (clickedNode, dataExplorerNodeSet);

                    clickedNode.Expanded = true;

                    clickedNode.Selected = true;

                    InitializeProperties ();

                    break;

                case "AddEvaluationMemberEnrollment":

                    if (dataExplorerNode.NodeType != Mercury.Server.Application.DataExplorerNodeType.Set) { return; }

                    dataExplorerNodeSet = (Client.Core.DataExplorer.DataExplorerNodeSet)dataExplorerNode;

                    dataExplorerNodeSet.AddNodeEvaluation (Mercury.Server.Application.DataExplorerEvaluationType.MemberEnrollment);

                    clickedNode.Nodes.Clear ();

                    DataExplorerTreeView_AppendChildren (clickedNode, dataExplorerNodeSet);

                    clickedNode.Expanded = true;

                    clickedNode.Selected = true;

                    InitializeProperties ();

                    break;

                case "AddEvaluationMemberEnrollmentContinuousFromBirthDate":

                    if (dataExplorerNode.NodeType != Mercury.Server.Application.DataExplorerNodeType.Set) { return; }

                    dataExplorerNodeSet = (Client.Core.DataExplorer.DataExplorerNodeSet)dataExplorerNode;

                    dataExplorerNodeSet.AddNodeEvaluation (Mercury.Server.Application.DataExplorerEvaluationType.MemberEnrollmentContinuousFromBirthDate);

                    clickedNode.Nodes.Clear ();

                    DataExplorerTreeView_AppendChildren (clickedNode, dataExplorerNodeSet);

                    clickedNode.Expanded = true;

                    clickedNode.Selected = true;

                    InitializeProperties ();

                    break;

                case "AddEvaluationMemberMetric":

                    if (dataExplorerNode.NodeType != Mercury.Server.Application.DataExplorerNodeType.Set) { return; }

                    dataExplorerNodeSet = (Client.Core.DataExplorer.DataExplorerNodeSet)dataExplorerNode;

                    dataExplorerNodeSet.AddNodeEvaluation (Mercury.Server.Application.DataExplorerEvaluationType.MemberMetric);

                    clickedNode.Nodes.Clear ();

                    DataExplorerTreeView_AppendChildren (clickedNode, dataExplorerNodeSet);

                    clickedNode.Expanded = true;

                    clickedNode.Selected = true;

                    InitializeProperties ();

                    break;

                case "AddEvaluationMemberService":

                    if (dataExplorerNode.NodeType != Mercury.Server.Application.DataExplorerNodeType.Set) { return; }

                    dataExplorerNodeSet = (Client.Core.DataExplorer.DataExplorerNodeSet)dataExplorerNode;

                    dataExplorerNodeSet.AddNodeEvaluation (Mercury.Server.Application.DataExplorerEvaluationType.MemberService);

                    clickedNode.Nodes.Clear ();

                    DataExplorerTreeView_AppendChildren (clickedNode, dataExplorerNodeSet);

                    clickedNode.Expanded = true;

                    clickedNode.Selected = true;

                    InitializeProperties ();

                    break;

                case "AddEvaluationPopulationMembership":

                    if (dataExplorerNode.NodeType != Mercury.Server.Application.DataExplorerNodeType.Set) { return; }
                    
                    dataExplorerNodeSet = (Client.Core.DataExplorer.DataExplorerNodeSet)dataExplorerNode;

                    dataExplorerNodeSet.AddNodeEvaluation (Mercury.Server.Application.DataExplorerEvaluationType.PopulationMembership);
                    
                    clickedNode.Nodes.Clear ();

                    DataExplorerTreeView_AppendChildren (clickedNode, dataExplorerNodeSet);

                    clickedNode.Expanded = true;

                    clickedNode.Selected = true;

                    InitializeProperties ();

                    break;

                case "Execute":

                    Server.Application.DataExplorerNodeExecutionResponse response = MercuryApplication.DataExplorerNodeExecute (Explorer, dataExplorerNode.NodeInstanceId);

                    LastExecutedNodeInstanceId = dataExplorerNode.NodeInstanceId;

                    LastExecuteNodeInstanceCount = response.RowCount;

                    DataExplorerNodeResultsGrid.Rebind ();

                    break;

                case "Delete":

                    if (Explorer.RootNode == dataExplorerNode) {

                        // CANNOT DELETE ROOT NODE, JUST CLEAR CONTENTS

                        Explorer.RootNode.Children = new List<Client.Core.DataExplorer.DataExplorerNode> ();

                        InitializeDataExplorerTreeView ();

                    }

                    else {

                        if (dataExplorerNode.Parent == null) { return; }

                        dataExplorerNode.Parent.Children.Remove (dataExplorerNode);

                        clickedNode.ParentNode.Expanded = true;

                        clickedNode.ParentNode.Selected = true;
                        
                        clickedNode.Remove ();

                        InitializeProperties ();

                    }

                    break;

            }


            return;

        }

        protected void DataExplorerTreeView_OnNodeDrop (Object sender, Telerik.Web.UI.RadTreeNodeDragDropEventArgs e) {

            if (e.DraggedNodes.Count == 0) { return; }

            if (e.DestDragNode == null) { return; }

            if ((e.DraggedNodes[0].ContextMenuID != "ContextMenuDataExplorerNodeSet") && (e.DraggedNodes[0].ContextMenuID != "ContextMenuDataExplorerNodeEvaluation")) { return; }

            if ((e.DestDragNode.ContextMenuID != "ContextMenuDataExplorerNodeSet") && (e.DestDragNode.ContextMenuID != "ContextMenuDataExplorerNodeEvaluation")) { return; }

            
            Telerik.Web.UI.RadTreeNode sourceParentNode = e.DraggedNodes[0].ParentNode;

            Telerik.Web.UI.RadTreeNode destinationParentNode = e.DestDragNode.ParentNode;
            
            Guid nodeInstanceId = Guid.Empty;
            
            Client.Core.DataExplorer.DataExplorerNode dataExplorerNodeDestination = null;

            Client.Core.DataExplorer.DataExplorerNode dataExplorerNodeSource = null;

            Client.Core.DataExplorer.DataExplorerNode dataExplorerNodeSourceParent = null;


            Guid.TryParse (e.DestDragNode.Value, out nodeInstanceId);

            if (nodeInstanceId == Guid.Empty) { return; }

            dataExplorerNodeDestination = Explorer.FindNode (nodeInstanceId);


            Guid.TryParse (e.DraggedNodes[0].Value, out nodeInstanceId);

            if (nodeInstanceId == Guid.Empty) { return; }

            dataExplorerNodeSource = Explorer.FindNode (nodeInstanceId);

            dataExplorerNodeSourceParent = dataExplorerNodeSource.Parent;


            Int32 dropPositionIndex = 0;


            switch (e.DropPosition) {

                case Telerik.Web.UI.RadTreeViewDropPosition.Above:

                    dropPositionIndex = dataExplorerNodeDestination.Parent.Children.IndexOf (dataExplorerNodeDestination);

                    if (dropPositionIndex < 0) { dropPositionIndex = 0; }

                    dataExplorerNodeDestination = dataExplorerNodeDestination.Parent;

                    break;

                case Telerik.Web.UI.RadTreeViewDropPosition.Below:

                    if (dataExplorerNodeDestination.Parent == null) { return; }

                    dropPositionIndex = dataExplorerNodeDestination.Parent.Children.IndexOf (dataExplorerNodeDestination) + 1;

                    if (dataExplorerNodeDestination.Parent == dataExplorerNodeSourceParent) {

                        if (dropPositionIndex >= dataExplorerNodeDestination.Parent.Children.IndexOf (dataExplorerNodeSource)) { 
                            
                            dropPositionIndex = dropPositionIndex - 1; 
                        
                        }

                    }

                    dataExplorerNodeDestination = dataExplorerNodeDestination.Parent;

                    break;

                case Telerik.Web.UI.RadTreeViewDropPosition.Over:

                    if (e.DestDragNode.ContextMenuID != "ContextMenuDataExplorerNodeSet") { return; } // ONLY DROP ONTO SETS


                    // WHEN DROPPING ON A SET, ONLY CHILD ITEMS OR ITEMS NOT IN THE SAME BRANCH CAN BE DROPPED ON THE SET 

                    if (dataExplorerNodeSource.FindNode (dataExplorerNodeDestination.NodeInstanceId) != null) { return; }


                    dropPositionIndex = dataExplorerNodeDestination.Children.Count;

                    break;

            }

            // REMOVE THE SOURCE FROM THE PARENT COLLECTION

            dataExplorerNodeSourceParent.Children.Remove (dataExplorerNodeSource);

            Client.Core.DataExplorer.DataExplorerNode[] childCollection = new Client.Core.DataExplorer.DataExplorerNode [(dataExplorerNodeDestination.Children.Count + 1)]; 

            Int32 currentIndex = 0;

            while (currentIndex < childCollection.Length) {

                if (currentIndex == dropPositionIndex) { childCollection[currentIndex] = dataExplorerNodeSource; }

                else if (currentIndex < dropPositionIndex) { childCollection[currentIndex] = dataExplorerNodeDestination.Children[currentIndex]; }

                else { childCollection[currentIndex] = dataExplorerNodeDestination.Children[currentIndex - 1]; }

                childCollection[currentIndex].Parent = dataExplorerNodeDestination;

                currentIndex = currentIndex + 1;

            }

            dataExplorerNodeDestination.Children.Clear ();

            dataExplorerNodeDestination.Children.AddRange (childCollection);

            InitializeDataExplorerTreeView ();

            
            destinationParentNode = DataExplorerTreeView.FindNodeByValue (dataExplorerNodeSourceParent.NodeInstanceId.ToString ());

            destinationParentNode.Expanded = true;

            destinationParentNode.Selected = true;

            InitializeProperties ();

            return;

        }

        #endregion 


        #region Data Explorer Node Results Grid

        protected void DataExplorerNodeResultsGrid_OnItemCreated (Object sender, Telerik.Web.UI.GridItemEventArgs e) {

            if (e.Item is Telerik.Web.UI.GridCommandItem) {

                Telerik.Web.UI.GridCommandItem commandItem = (Telerik.Web.UI.GridCommandItem)e.Item;

                LinkButton DataExplorerNodeResultsGrid_Export = (LinkButton) commandItem.FindControl ("DataExplorerNodeResultsGrid_Export");

                if (DataExplorerNodeResultsGrid_Export != null) {
                    
                    if (LastExecutedNodeInstanceId == Guid.Empty) { return; }

                    Client.Core.DataExplorer.DataExplorerNode dataExplorerNode = Explorer.FindNode (LastExecutedNodeInstanceId);

                    if (dataExplorerNode == null) { return; }

                    
                    String scriptCommand = "window.open ('/Application/DataExplorer/Exports/DataExplorerNodeResultExport";

                    switch (dataExplorerNode.ResultDataType) {

                        case Mercury.Server.Application.DataExplorerNodeResultDataType.Member: scriptCommand += "Member.aspx?"; break;

                        default: scriptCommand += ".aspx?"; break;
                
                    }

                    scriptCommand += "NodeInstanceId=" + LastExecutedNodeInstanceId.ToString () + "&NodeInstanceCount=" + LastExecuteNodeInstanceCount.ToString () + "'";

                    scriptCommand += ", 'DataExplorerNodeResultExportMember_" + LastExecutedNodeInstanceId.ToString ().Replace ("-", "") + "', 'toolbar=0, location=0, directories=0, status=1, menubar=0, scrollbars=1, resizable=1'";
                    
                    scriptCommand += ");";

                    DataExplorerNodeResultsGrid_Export.OnClientClick = scriptCommand;

                }

            }
            

            return;

        }

        private Telerik.Web.UI.GridBoundColumn CreateGridBoundColumn (String dataField, String headerText, Boolean visible, Int32 width = 0) {

            Telerik.Web.UI.GridBoundColumn column = new Telerik.Web.UI.GridBoundColumn ();


            column.DataField = dataField;

            column.HeaderText = headerText;

            column.Visible = visible;


            if (width != 0) {

                column.HeaderStyle.Width = new Unit (width);

                column.ItemStyle.Width = new Unit (width);

            }

            return column;

        }

        protected void DataExplorerNodeResultsGrid_OnNeedDataSource_Member (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e) {

            if (!e.IsFromDetailTable) {

                // MASTER TABLE VIEW NEEDS DATA SOURCE

                switch (e.RebindReason) {

                    case Telerik.Web.UI.GridRebindReason.InitialLoad:

                    case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                    case Telerik.Web.UI.GridRebindReason.PostBackEvent:

                    case Telerik.Web.UI.GridRebindReason.PostbackViewStateNotPersisted:

                        // UPDATE COUNT 

                        DataExplorerNodeResultsGrid.VirtualItemCount = LastExecuteNodeInstanceCount;

                        Int32 initialRow = (DataExplorerNodeResultsGrid.CurrentPageIndex * DataExplorerNodeResultsGrid.PageSize) + 1;

                        List<Client.Core.Member.Member> members = MercuryApplication.DataExplorerNodeResultsGetForMember (LastExecutedNodeInstanceId, initialRow, DataExplorerNodeResultsGrid.PageSize);

                        DataExplorerNodeResultsGrid.Columns.Clear ();

                        DataExplorerNodeResultsGrid.Columns.Add (CreateGridBoundColumn ("Id", "Id", false));

                        DataExplorerNodeResultsGrid.Columns.Add (CreateGridBoundColumn ("Entity.UniqueId", "Identifier", true, 100));

                        Telerik.Web.UI.GridTemplateColumn memberNameColumn = new Telerik.Web.UI.GridTemplateColumn ();

                        memberNameColumn.ItemTemplate = new Templates.MemberLinkTemplate ();

                        DataExplorerNodeResultsGrid.Columns.Add (memberNameColumn);

                        DataExplorerNodeResultsGrid.Columns.Add (CreateGridBoundColumn ("CurrentAgeDescription", "Age", true, 40));

                        DataExplorerNodeResultsGrid.Columns.Add (CreateGridBoundColumn ("BirthDateDescription", "Birth Date", true, 80));

                        DataExplorerNodeResultsGrid.Columns.Add (CreateGridBoundColumn ("GenderDescription", "Gender", true, 60));

                        DataExplorerNodeResultsGrid.Columns.Add (CreateGridBoundColumn ("EthnicityDescription", "Ethnicity", true));

                        DataExplorerNodeResultsGrid.Columns.Add (CreateGridBoundColumn ("FamilyId", "Family Id", true, 100));

                        DataExplorerNodeResultsGrid.DataSource = members;

                        break;

                    default:

                        System.Diagnostics.Debug.WriteLine ("Unhandled Master Rebind Reason: " + e.RebindReason);

                        break;

                }

            }

            return;

        }

        protected void DataExplorerNodeResultsGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e) {

            if (LastExecutedNodeInstanceId == Guid.Empty) { return; }

            Client.Core.DataExplorer.DataExplorerNode dataExplorerNode = Explorer.FindNode (LastExecutedNodeInstanceId);

            if (dataExplorerNode == null) { return; }


            switch (dataExplorerNode.ResultDataType) {

                case Mercury.Server.Application.DataExplorerNodeResultDataType.Member: DataExplorerNodeResultsGrid_OnNeedDataSource_Member (sender, e); break;
                
            }

            return;

        }

        protected void DataExplorerNodeResultsGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs e) {

            DataExplorerNodeResultsGrid.Rebind ();

            return;

        }

        #endregion 


        #region Property Change Events

        protected void VariablePropertiesVariableDataType_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e) {

            if (DataExplorerTreeView.SelectedNode == null) { return; }

            if (DataExplorerTreeView.SelectedNode.Value.Split ('_').Length < 2) { return; }

            String selectedVariableName = DataExplorerTreeView.SelectedNode.Value.Split ('_')[1];

            if (!Explorer.Variables.ContainsKey (selectedVariableName)) { return; }

            Mercury.Server.Application.DataExplorerVariable variable = Explorer.Variables[selectedVariableName];

            if (variable != null) {

                variable.VariableType = (Mercury.Server.Application.DataExplorerVariableType)Convert.ToInt32 (VariablePropertiesVariableDataType.SelectedValue);

            }


            Telerik.Web.UI.RadTreeNode treeNode = DataExplorerTreeView.FindNodeByValue ("Variable_" + variable.Name);

            if (treeNode == null) { return; }

            DataExplorerTreeView_InitializeVariableNode (treeNode, variable);


            InitializeVariableProperties ();

            return;

        }

        protected void NodePropertiesSetType_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e) {

            if (DataExplorerTreeView.SelectedNode == null) { return; }


            // VALIDATE THAT THE SELECTED NODE VALUE IS A GUID

            Guid selectedNodeValue = Guid.Empty;

            Guid.TryParse (DataExplorerTreeView.SelectedNode.Value, out selectedNodeValue);

            if (selectedNodeValue == Guid.Empty) { return; }


            // FIND THE ACTUAL NODE (BASE OBJECT), VALIDATE THAT IT IS A SET NODE, CONVERT TO ACTUAL OBJECT

            Client.Core.DataExplorer.DataExplorerNode dataExplorerNode = Explorer.FindNode (selectedNodeValue);

            if (dataExplorerNode == null) { return; }

            if (dataExplorerNode.NodeType != Mercury.Server.Application.DataExplorerNodeType.Set) { return; }

            Client.Core.DataExplorer.DataExplorerNodeSet dataExplorerNodeSet = (Client.Core.DataExplorer.DataExplorerNodeSet)dataExplorerNode;

            dataExplorerNodeSet.SetType = (Mercury.Server.Application.DataExplorerSetType)Convert.ToInt32 (e.Value);


            // FIND THE NODE IN THE TREE VIEW AND UPDATE PROPERTIES

            Telerik.Web.UI.RadTreeNode treeNode = DataExplorerTreeView.FindNodeByValue (dataExplorerNode.NodeInstanceId.ToString (), true);

            if (treeNode == null) { return; }

            DataExplorerTreeView_InitializeNode (treeNode, dataExplorerNodeSet);

            return;

        }

        protected void Properties_OnTextChange_Variable (Object sender, EventArgs eventArgs) {

            if (DataExplorerTreeView.SelectedNode == null) { return; }

            if (DataExplorerTreeView.SelectedNode.Value.Split ('_').Length < 2) { return; }

            String selectedVariableName = DataExplorerTreeView.SelectedNode.Value.Split ('_')[1];


            Mercury.Server.Application.DataExplorerVariable variable = Explorer.Variables[selectedVariableName];

            if (variable == null) { return; }


            String propertyName = String.Empty;

            String comboBoxValue = String.Empty;

            String propertyValue = String.Empty;


            Boolean nodeNeedsUpdate = false;

            String originalName = variable.Name;


            #region Control Type, Initialize Property Value and Id

            if (sender is Telerik.Web.UI.RadComboBox) {

                Telerik.Web.UI.RadComboBox propertyComboBox = (Telerik.Web.UI.RadComboBox)sender;

                propertyValue = propertyComboBox.Text;

                comboBoxValue = propertyComboBox.SelectedValue;

                propertyName = propertyComboBox.ID;

            }

            else if (sender is Telerik.Web.UI.RadTextBox) {

                Telerik.Web.UI.RadTextBox propertyTextBox = (Telerik.Web.UI.RadTextBox)sender;

                propertyValue = propertyTextBox.Text;

                propertyName = propertyTextBox.ID;

            }

            else if (sender is Telerik.Web.UI.RadNumericTextBox) {

                Telerik.Web.UI.RadNumericTextBox propertyNumericTextBox = (Telerik.Web.UI.RadNumericTextBox)sender;

                propertyValue = propertyNumericTextBox.Value.ToString ();

                propertyName = propertyNumericTextBox.ID;

            }

            else { return; }

            #endregion


            switch (propertyName) {

                case "VariablePropertiesVariableName":

                    if (String.IsNullOrWhiteSpace (propertyValue)) { return; }

                    variable.Name = propertyValue;

                    variable.Description = propertyValue;

                    Explorer.Variables.Remove (originalName);

                    Explorer.Variables.Add (variable.Name, variable);

                    nodeNeedsUpdate = true;

                    break;

                default:

                    System.Diagnostics.Debug.WriteLine ("Data Explorer Unhandled Property Change: " + propertyName);

                    break;

            }

            if (nodeNeedsUpdate) {

                // FIND THE NODE IN THE TREE VIEW AND UPDATE PROPERTIES

                Telerik.Web.UI.RadTreeNode treeNode = DataExplorerTreeView.FindNodeByValue ("Variable_" + originalName);

                if (treeNode == null) { return; }

                DataExplorerTreeView_InitializeVariableNode (treeNode, variable);

            }

            InitializeProperties ();

            return;

        }

        protected void Properties_OnTextChange (Object sender, EventArgs eventArgs) {

            if (DataExplorerTreeView.SelectedNode == null) { return; }

            // HANDLE VARIABLE NODE TREE IN A SEPARATE METHOD 

            if (DataExplorerTreeView.SelectedNode.Value.StartsWith ("Variable")) { Properties_OnTextChange_Variable (sender, eventArgs); return; }


            Guid selectedNodeValue = Guid.Empty;

            Guid.TryParse (DataExplorerTreeView.SelectedNode.Value, out selectedNodeValue);

            if (selectedNodeValue == Guid.Empty) { return; }


            Client.Core.DataExplorer.DataExplorerNode dataExplorerNode = Explorer.FindNode (selectedNodeValue);

            Client.Core.DataExplorer.DataExplorerNodeEvaluation dataExplorerNodeEvaluation = (dataExplorerNode is Client.Core.DataExplorer.DataExplorerNodeEvaluation) ? (Client.Core.DataExplorer.DataExplorerNodeEvaluation)dataExplorerNode : null;

            if (dataExplorerNode == null) { return; }



            String propertyName = String.Empty;

            String comboBoxValue = String.Empty;

            String propertyValue = String.Empty;

            Boolean nodeNeedsUpdate = false;
            

            #region Control Type, Initialize Property Value and Id

            if (sender is Telerik.Web.UI.RadComboBox) {

                Telerik.Web.UI.RadComboBox propertyComboBox = (Telerik.Web.UI.RadComboBox)sender;

                propertyValue = propertyComboBox.Text;

                comboBoxValue = propertyComboBox.SelectedValue;

                propertyName = propertyComboBox.ID;

            }

            else if (sender is Telerik.Web.UI.RadTextBox) {

                Telerik.Web.UI.RadTextBox propertyTextBox = (Telerik.Web.UI.RadTextBox)sender;

                propertyValue = propertyTextBox.Text;

                propertyName = propertyTextBox.ID;

            }

            else if (sender is Telerik.Web.UI.RadNumericTextBox) {

                Telerik.Web.UI.RadNumericTextBox propertyNumericTextBox = (Telerik.Web.UI.RadNumericTextBox)sender;

                propertyValue = propertyNumericTextBox.Value.ToString ();

                propertyName = propertyNumericTextBox.ID;

            }

            else if (sender is CheckBox) {

                CheckBox propertyCheckBox = (CheckBox)sender;

                propertyValue = propertyCheckBox.Checked.ToString ();

                propertyName = propertyCheckBox.ID;

            }

            else { return; }

            #endregion


            #region Cast Out Incoming Node for Modification, Invalid Mappings will be NULL

            Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationMemberDemographic dataExplorerNodeEvaluationMemberDemographic =

                (dataExplorerNodeEvaluation is Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationMemberDemographic) ? (Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationMemberDemographic)dataExplorerNodeEvaluation : null;

            Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationMemberEnrollment dataExplorerNodeEvaluationMemberEnrollment =

                (dataExplorerNodeEvaluation is Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationMemberEnrollment) ? (Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationMemberEnrollment)dataExplorerNodeEvaluation : null;

            Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate dataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate =

                (dataExplorerNodeEvaluation is Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate) ? (Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate)dataExplorerNodeEvaluation : null;

            Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationMemberMetric dataExplorerNodeEvaluationMemberMetric =

                (dataExplorerNodeEvaluation is Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationMemberMetric) ? (Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationMemberMetric)dataExplorerNodeEvaluation : null;

            Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationMemberService dataExplorerNodeEvaluationMemberService =

                (dataExplorerNodeEvaluation is Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationMemberService) ? (Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationMemberService)dataExplorerNodeEvaluation : null;

            Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationPopulationMembership dataExplorerNodeEvaluationPopulationMembership =

                (dataExplorerNodeEvaluation is Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationPopulationMembership) ? (Client.Core.DataExplorer.Evaluations.DataExplorerNodeEvaluationPopulationMembership)dataExplorerNodeEvaluation : null;

            #endregion 


            switch (propertyName) {

                case "NodePropertiesName": 
                    
                    dataExplorerNode.Name = propertyValue;

                    dataExplorerNode.Description = propertyValue;

                    nodeNeedsUpdate = true;
                    
                    break;


                #region Evaluation - Member Demographic

                case "MemberDemographicEvaluationGenderSelection": dataExplorerNodeEvaluationMemberDemographic.Gender = (Mercury.Server.Application.Gender)Convert.ToInt32 (comboBoxValue); break;

                case "MemberDemographicEthnicitySelection": dataExplorerNodeEvaluationMemberDemographic.EthnicityId = Convert.ToInt64 (comboBoxValue); break;
 
                #endregion 

                    
                #region Evaluation - Member Enrollment

                case "MemberEnrollmentEvaluationInsurerSelection": dataExplorerNodeEvaluationMemberEnrollment.InsurerId = Convert.ToInt64 (comboBoxValue); break;

                case "MemberEnrollmentEvaluationProgramSelection": dataExplorerNodeEvaluationMemberEnrollment.ProgramId = Convert.ToInt64 (comboBoxValue); break;

                case "MemberEnrollmentEvaluationBenefitPlanSelection": dataExplorerNodeEvaluationMemberEnrollment.BenefitPlanId = Convert.ToInt64 (comboBoxValue); break;

                case "MemberEnrollmentEvaluationContinuousEnrollment": dataExplorerNodeEvaluationMemberEnrollment.ContinuousEnrollment = Convert.ToBoolean (propertyValue); break;

                case "MemberEnrollmentEvaluationContinuousAllowedGaps": dataExplorerNodeEvaluationMemberEnrollment.ContinuousAllowedGaps = Convert.ToInt32 (propertyValue); break;

                case "MemberEnrollmentEvaluationContinuousAllowedGapDays": dataExplorerNodeEvaluationMemberEnrollment.ContinuousAllowedGapDays = Convert.ToInt32 (propertyValue); break;

                #endregion 
                    
                #region Evaluation - Member Enrollment Continuous From Birth Date

                case "MemberEnrollmentContinuousFromBirthDateEvaluationInsurerSelection": dataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate.InsurerId = Convert.ToInt64 (comboBoxValue); break;

                case "MemberEnrollmentContinuousFromBirthDateEvaluationProgramSelection": dataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate.ProgramId = Convert.ToInt64 (comboBoxValue); break;

                case "MemberEnrollmentContinuousFromBirthDateEvaluationBenefitPlanSelection": dataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate.BenefitPlanId = Convert.ToInt64 (comboBoxValue); break;

                case "MemberEnrollmentContinuousFromBirthDateEvaluationContinuousUntilAge": dataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate.ContinuousUntilAge = Convert.ToInt32 (propertyValue); break;

                case "MemberEnrollmentContinuousFromBirthDateEvaluationContinuousAllowedGaps": dataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate.ContinuousAllowedGaps = Convert.ToInt32 (propertyValue); break;

                case "MemberEnrollmentContinuousFromBirthDateEvaluationContinuousAllowedGapDays": dataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate.ContinuousAllowedGapDays = Convert.ToInt32 (propertyValue); break;

                #endregion 
                    
                    
                #region Evaluation - Member Metric

                case "MemberMetricEvaluationMetricSelection": dataExplorerNodeEvaluationMemberMetric.MetricId = Convert.ToInt64 (comboBoxValue); break;

                case "MemberMetricEvaluationValueMinimum": dataExplorerNodeEvaluationMemberMetric.ValueMinimum = Convert.ToDecimal (propertyValue); break;

                case "MemberMetricEvaluationValueMaximum": dataExplorerNodeEvaluationMemberMetric.ValueMaximum = Convert.ToDecimal (propertyValue); break;

                case "MemberMetricEvaluationCountOf": dataExplorerNodeEvaluationMemberMetric.CountOf = Convert.ToInt32 (propertyValue); break;

                #endregion 


                #region Evaluation - Member Service

                case "MemberServiceEvaluationServiceSelection": dataExplorerNodeEvaluationMemberService.ServiceId = Convert.ToInt64 (comboBoxValue); break;

                case "MemberServiceEvaluationCountOf": dataExplorerNodeEvaluationMemberService.CountOf = Convert.ToInt32 (propertyValue); break;

                #endregion 


                #region Evaluation - Population Membership

                case "PopulationMembershipEvaluationFunction": dataExplorerNodeEvaluationPopulationMembership.Evaluation = (Mercury.Server.Application.DataFilterOperator)Convert.ToInt32 (comboBoxValue); break;

                case "PopulationMembershipEvaluationTypeSelection": dataExplorerNodeEvaluationPopulationMembership.PopulationEvaluationType = (Mercury.Server.Application.DataExplorerNodeEvaluationPopulationEvaluationType)Convert.ToInt32 (comboBoxValue); break;

                case "PopulationMembershipEvaluationPopulationSelection": dataExplorerNodeEvaluationPopulationMembership.PopulationId = Convert.ToInt64 (comboBoxValue); break;

                case "PopulationMembershipEvaluationPopulationTypeSelection": dataExplorerNodeEvaluationPopulationMembership.PopulationTypeId = Convert.ToInt64 (comboBoxValue); break;

                #endregion 

                    
                #region Evaluation - Age Criteria 1

                case "AgeCriteria1UseAgeCriteria": ((Client.Core.DataExplorer.Evaluations.IDataExplorerNodeEvaluationAge) dataExplorerNodeEvaluation).AgeCriteria.UseAgeCriteria = Convert.ToBoolean (propertyValue); break;

                case "AgeCriteria1AgeMinimum": ((Client.Core.DataExplorer.Evaluations.IDataExplorerNodeEvaluationAge)dataExplorerNodeEvaluation).AgeCriteria.AgeMinimum = Convert.ToInt32 (propertyValue); break;

                case "AgeCriteria1AgeMaximum": ((Client.Core.DataExplorer.Evaluations.IDataExplorerNodeEvaluationAge)dataExplorerNodeEvaluation).AgeCriteria.AgeMaximum = Convert.ToInt32 (propertyValue); break;

                case "AgeCriteria1AgeQualifierSelection": ((Client.Core.DataExplorer.Evaluations.IDataExplorerNodeEvaluationAge)dataExplorerNodeEvaluation).AgeCriteria.AgeQualifier = (Mercury.Server.Application.DateQualifier)Convert.ToInt32 (comboBoxValue); break;

                #endregion


                #region Date Criteria 1 Property Changes

                case "DateCriteria1DateTypeSelection": ((Client.Core.DataExplorer.Evaluations.IDataExplorerNodeEvaluationDate)dataExplorerNodeEvaluation).DateCriteria.DateType = (Mercury.Server.Application.DataExplorerEvaluationDateType)Convert.ToInt32 (comboBoxValue); break;

                case "DateCriteria1StartDateVariableSelection": ((Client.Core.DataExplorer.Evaluations.IDataExplorerNodeEvaluationDate)dataExplorerNodeEvaluation).DateCriteria.StartDateVariableName = comboBoxValue; break;

                case "DateCriteria1StartDateRelativeValue": ((Client.Core.DataExplorer.Evaluations.IDataExplorerNodeEvaluationDate)dataExplorerNodeEvaluation).DateCriteria.StartDateRelativeValue = Convert.ToInt32 (propertyValue); break;

                case "DateCriteria1StartDateRelativeQualifier": ((Client.Core.DataExplorer.Evaluations.IDataExplorerNodeEvaluationDate)dataExplorerNodeEvaluation).DateCriteria.StartDateRelativeQualifier = (Mercury.Server.Application.DateQualifier) Convert.ToInt32 (comboBoxValue); break;

                case "DateCriteria1EndDateVariableSelection": ((Client.Core.DataExplorer.Evaluations.IDataExplorerNodeEvaluationDate)dataExplorerNodeEvaluation).DateCriteria.EndDateVariableName = comboBoxValue; break;

                case "DateCriteria1EndDateRelativeValue": ((Client.Core.DataExplorer.Evaluations.IDataExplorerNodeEvaluationDate)dataExplorerNodeEvaluation).DateCriteria.EndDateRelativeValue = Convert.ToInt32 (propertyValue); break;

                case "DateCriteria1EndDateRelativeQualifier": ((Client.Core.DataExplorer.Evaluations.IDataExplorerNodeEvaluationDate)dataExplorerNodeEvaluation).DateCriteria.EndDateRelativeQualifier = (Mercury.Server.Application.DateQualifier)Convert.ToInt32 (comboBoxValue); break;

                #endregion 


                default:

                    System.Diagnostics.Debug.WriteLine ("Data Explorer Unhandled Property Change: " + propertyName);

                    break;

            }


            if (nodeNeedsUpdate) {

                // FIND THE NODE IN THE TREE VIEW AND UPDATE PROPERTIES

                Telerik.Web.UI.RadTreeNode treeNode = DataExplorerTreeView.FindNodeByValue (dataExplorerNode.NodeInstanceId.ToString (), true);

                if (treeNode == null) { return; }

                DataExplorerTreeView_InitializeNode (treeNode, dataExplorerNode);

            }

            InitializeProperties ();

            return;

        }

        #endregion 


        #region Data Explorer Menu Commands

        public void DataExplorerSave_OnClick (Object sender, EventArgs e) {

            Boolean success = false;

            Boolean isValid = false;

            System.Collections.Generic.Dictionary<String, String> validationResponse;



            ExceptionMessageRow.Visible = false; 

            Explorer.Name = DataExplorerName.Text.Trim ();

            Explorer.Description = DataExplorerDescription.Text.Trim ();

            Explorer.IsPublic = Convert.ToBoolean (DataExplorerIsPublic.SelectedValue);

            Explorer.Enabled = true;

            Explorer.Visible = true;


            validationResponse = Explorer.Validate ();

            isValid = (validationResponse.Count == 0);


            if (isValid) {

                success = MercuryApplication.DataExplorerSave (Explorer);

                if (success) {

                    Explorer = MercuryApplication.DataExplorerGet (Explorer.Id, false);

                    InitializeAll ();

                }

                else {

                    ExceptionMessage.Text = "Unable to Save.";

                    if (MercuryApplication.LastException != null) { ExceptionMessage.Text += " [" + MercuryApplication.LastException.Message + "]"; }

                    ExceptionMessageRow.Visible = true; 

                    success = false;

                }

            }


            else if (!isValid) {

                foreach (String validationKey in validationResponse.Keys) {

                    ExceptionMessage.Text = "Invalid [" + validationKey + "]: " + validationResponse[validationKey];

                    ExceptionMessageRow.Visible = true; 

                    break;

                }

                success = false;

            }
            
            return;

        }

        public void DataExplorerClone_OnClick (Object sender, EventArgs e) {

            Client.Core.DataExplorer.DataExplorer dataExplorer = (Client.Core.DataExplorer.DataExplorer) Explorer.Copy ();

            dataExplorer.Id = 0;

            Explorer = dataExplorer;

            InitializeAll ();

            return;

        }

        #endregion 

    }

}