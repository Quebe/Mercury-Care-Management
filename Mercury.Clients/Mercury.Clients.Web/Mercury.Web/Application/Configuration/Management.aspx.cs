using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Configuration {

    public partial class Management : System.Web.UI.Page  {
        
        #region Private Session States

        private Mercury.Client.Application MercuryApplication { get { return Master.MercuryApplication; } }

        private String SessionCachePrefix { get { return Master.SessionCachePrefix; } }

        #endregion 

        
        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            if (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ConfigurationManagement)) { Response.Redirect ("/PermissionDenied.aspx", true); return; }


            //Page.Title = "Mercury - Configuration Management [" + MercuryApplication.Session.EnvironmentName + "]";


            Response.Cache.SetCacheability (HttpCacheability.NoCache);

            Response.Cache.SetExpires (DateTime.Now);


            if ((MercuryApplication != null) && (!Page.IsPostBack)) {

                InitializeToolbar ();

                Initialize_ConfigurationTree ();

                Session[SessionCachePrefix + "ConfigurationTreeState"] = ConfigurationTree.GetXml ();

            }

            else { // POSTBACK
                
                ConfigurationTree.LoadXmlString ((String)Session[SessionCachePrefix + "ConfigurationTreeState"]);

                if (Session[SessionCachePrefix + "ConfigurationGrid"] != null) {

                    ConfigurationGrid.DataSource = Session[SessionCachePrefix + "ConfigurationGrid"];

                }

            }

        }

        #endregion


        #region Control Initializations and Support Functions

        private void InitializeToolbar () {

            Boolean pageGeneralAvailable = false;

            Boolean pageWorkAvailable = false;

            Boolean pageServicesAvailable = false;

            Boolean pageCareManagementAvailable = false;

            Boolean pageImportAvailable = false;


            #region General Tab and Items

            pageGeneralAvailable = pageGeneralAvailable || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ReportingServerManage));

            pageGeneralAvailable = pageGeneralAvailable || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.FaxServerManage));

            pageGeneralAvailable = pageGeneralAvailable || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.PrinterManage));

            pageGeneralAvailable = pageGeneralAvailable || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.NoteTypeManage));

            pageGeneralAvailable = pageGeneralAvailable || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ContactRegardingManage));

            pageGeneralAvailable = pageGeneralAvailable || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CorrespondenceManage));

            pageGeneralAvailable = pageGeneralAvailable || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.FormDesigner));

            //            ToolbarTabStrip.Tabs.FindTabByText ("General").Visible = pageGeneralAvailable;

            //            ToolbarMultiPage.FindPageViewByID ("PageGeneral").Visible = pageGeneralAvailable;


            ToolbarGeneral.Items.FindItemByValue ("AddReportingServer").Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ReportingServerManage));

            ToolbarGeneral.Items.FindItemByValue ("AddFaxServer").Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.FaxServerManage));

            ToolbarGeneral.Items.FindItemByValue ("AddPrinter").Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.PrinterManage));

            ToolbarGeneral.Items.FindItemByValue ("AddNoteType").Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.NoteTypeManage));

            ToolbarGeneral.Items.FindItemByValue ("AddContactRegarding").Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ContactRegardingManage));

            ToolbarGeneral.Items.FindItemByValue ("AddCorrespondence").Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CorrespondenceManage));

            ToolbarGeneral.Items.FindItemByValue ("FormDesigner").Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.FormDesigner));

            #endregion


            #region Work Tab and Items

            pageWorkAvailable = pageWorkAvailable || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkflowManage));

            pageWorkAvailable = pageWorkAvailable || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkTeamManage));

            pageWorkAvailable = pageWorkAvailable || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkQueueManage));

            pageWorkAvailable = pageWorkAvailable || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkOutcomeManage));

            pageWorkAvailable = pageWorkAvailable || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.RoutingRuleManage));

            pageWorkAvailable = pageWorkAvailable || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WidgetManage));

            pageWorkAvailable = pageWorkAvailable || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.RoleManage));


            ToolbarWork.Items.FindItemByValue ("AddWorkflow").Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkflowManage));

            ToolbarWork.Items.FindItemByValue ("AddWorkTeam").Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkTeamManage));

            ToolbarWork.Items.FindItemByValue ("AddWorkQueue").Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkQueueManage));

            ToolbarWork.Items.FindItemByValue ("AddWorkOutcome").Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkOutcomeManage));

            ToolbarWork.Items.FindItemByValue ("AddRoutingRule").Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.RoutingRuleManage));

            #endregion



            #region Services Tab and Items

            pageServicesAvailable = pageServicesAvailable || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MedicalServiceManage));

            pageServicesAvailable = pageServicesAvailable || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MetricManage));

            pageServicesAvailable = pageServicesAvailable || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.AuthorizedServiceManage));

            ToolbarServices.Items.FindItemByValue ("AddSingleton").Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MedicalServiceManage));

            ToolbarServices.Items.FindItemByValue ("AddSet").Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MedicalServiceManage));

            ToolbarServices.Items.FindItemByValue ("AddMetric").Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MetricManage));

            ToolbarServices.Items.FindItemByValue ("AddAuthorizedService").Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.AuthorizedServiceManage));

            #endregion 


            #region Care Management Tab and Items

            pageCareManagementAvailable = pageCareManagementAvailable || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.PopulationTypeManage));

            pageCareManagementAvailable = pageCareManagementAvailable || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.PopulationManage));

            pageCareManagementAvailable = pageCareManagementAvailable || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareMeasureScaleManage));

            pageCareManagementAvailable = pageCareManagementAvailable || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ProblemStatementManage));

            pageCareManagementAvailable = pageCareManagementAvailable || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareLevelManage));

            pageCareManagementAvailable = pageCareManagementAvailable || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CarePlanManage));

            pageCareManagementAvailable = pageCareManagementAvailable || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareOutcomeManage));

            //            ToolbarTabStrip.Tabs.FindTabByText ("Care Management").Visible = pageCareManagementAvailable;

            //            ToolbarMultiPage.FindPageViewByID ("PageCareManagement").Visible = pageCareManagementAvailable;


            ToolbarCareManagement.Items.FindItemByValue ("AddPopulationType").Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.PopulationTypeManage));

            ToolbarCareManagement.Items.FindItemByValue ("AddPopulation").Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.PopulationManage));

            ToolbarCareManagement.Items.FindItemByValue ("AddCareMeasureScale").Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareMeasureScaleManage));

            ToolbarCareManagement.Items.FindItemByValue ("AddCareMeasure").Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareMeasureManage));

            ToolbarCareManagement.Items.FindItemByValue ("AddCareIntervention").Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareInterventionManage));

            ToolbarCareManagement.Items.FindItemByValue ("AddProblemStatement").Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ProblemStatementManage));

            ToolbarCareManagement.Items.FindItemByValue ("AddCareLevel").Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareLevelManage));

            ToolbarCareManagement.Items.FindItemByValue ("AddCarePlan").Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CarePlanManage));

            ToolbarCareManagement.Items.FindItemByValue ("AddCareOutcome").Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareOutcomeManage));

            #endregion


            #region Import Tab and Items

            pageImportAvailable = pageImportAvailable || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ConfigurationImportExport));

            //            ToolbarTabStrip.Tabs.FindTabByText ("Import").Visible = pageImportAvailable;

            //            ToolbarMultiPage.FindPageViewByID ("PageImport").Visible = pageImportAvailable;


            ToolbarImport.Items.FindItemByValue ("ConfigurationImport").Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ConfigurationImportExport));

            ToolbarImport.Items.FindItemByValue ("ConfigurationImportNcqaNdc").Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ConfigurationImportExport));

            #endregion


            if (pageImportAvailable) {

                ToolbarTabStrip.SelectedIndex = 4;

                ToolbarMultiPage.SelectedIndex = 4;

            }

            if (pageCareManagementAvailable) {

                ToolbarTabStrip.SelectedIndex = 3;

                ToolbarMultiPage.SelectedIndex = 3;

            }

            if (pageServicesAvailable) {

                ToolbarTabStrip.SelectedIndex = 2;

                ToolbarMultiPage.SelectedIndex = 2;

            }

            if (pageWorkAvailable) {

                ToolbarTabStrip.SelectedIndex = 1;

                ToolbarMultiPage.SelectedIndex = 1;

            }

            if (pageGeneralAvailable) {

                ToolbarTabStrip.SelectedIndex = 0;

                ToolbarMultiPage.SelectedIndex = 0;

            }

            return;

        }

        protected void Initialize_ConfigurationTree () {

            Telerik.Web.UI.RadTreeNode parentNode;

            ConfigurationTree.LoadingStatusPosition = Telerik.Web.UI.TreeViewLoadingStatusPosition.AfterNodeText;

            if ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ReportingServerReview))
              || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ReportingServerManage))) {

                parentNode = new Telerik.Web.UI.RadTreeNode ();

                parentNode.Text = "Reporting Servers";
                parentNode.Value = "ReportingServers";
                parentNode.Category = "Root";
                parentNode.ImageUrl = "/Images/Common16/ReportingServer.png";
                parentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                ConfigurationTree.Nodes.Add (parentNode);

            }


            if ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.FaxServerReview))
              || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.FaxServerManage))) {

                parentNode = new Telerik.Web.UI.RadTreeNode ();

                parentNode.Text = "Fax Servers";
                parentNode.Value = "FaxServers";
                parentNode.Category = "Root";
                parentNode.ImageUrl = "/Images/Common16/FaxServer.png";
                parentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                ConfigurationTree.Nodes.Add (parentNode);

            }


            if ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.PrinterReview))
              || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.PrinterManage))) {

                parentNode = new Telerik.Web.UI.RadTreeNode ();

                parentNode.Text = "Printers";
                parentNode.Value = "Printers";
                parentNode.Category = "Root";
                parentNode.ImageUrl = "/Images/Common16/Printer.png";
                parentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                ConfigurationTree.Nodes.Add (parentNode);

            }

            if ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.NoteTypeReview))
              || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.NoteTypeManage))) {

                parentNode = new Telerik.Web.UI.RadTreeNode ();

                parentNode.Text = "Note Types";
                parentNode.Value = "NoteTypes";
                parentNode.Category = "Root";
                parentNode.ImageUrl = "/Images/Common16/Note.png";
                parentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                ConfigurationTree.Nodes.Add (parentNode);

            }

            if ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ContactRegardingReview))
              || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ContactRegardingManage))) {

                parentNode = new Telerik.Web.UI.RadTreeNode ();

                parentNode.Text = "Contact Regardings";
                parentNode.Value = "ContactRegardings";
                parentNode.Category = "Root";
                parentNode.ImageUrl = "/Images/Common16/Phone.png";
                parentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                ConfigurationTree.Nodes.Add (parentNode);

            }

            if ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CorrespondenceReview))
              || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CorrespondenceManage))) {

                parentNode = new Telerik.Web.UI.RadTreeNode ();

                parentNode.Text = "Correspondences";
                parentNode.Value = "Correspondences";
                parentNode.Category = "Root";
                parentNode.ImageUrl = "/Images/Common16/Correspondence.png";
                parentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                ConfigurationTree.Nodes.Add (parentNode);

            }

            if ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.FormReview))
                || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.FormManage))) {

                parentNode = new Telerik.Web.UI.RadTreeNode ();

                parentNode.Text = "Forms";
                parentNode.Value = "Forms";
                parentNode.Category = "Root";
                parentNode.ImageUrl = "/Images/Common16/Document.Png";
                parentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                ConfigurationTree.Nodes.Add (parentNode);

            }

            if ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkflowReview))
                || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkflowManage))) {

                parentNode = new Telerik.Web.UI.RadTreeNode ();

                parentNode.Text = "Workflows";
                parentNode.Value = "Workflows";
                parentNode.Category = "Root";
                parentNode.ImageUrl = "/Images/Common16/Workflow.png";
                parentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                ConfigurationTree.Nodes.Add (parentNode);

            }

            if ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkTeamReview))
                || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkTeamManage))) {

                parentNode = new Telerik.Web.UI.RadTreeNode ();

                parentNode.Text = "Work Teams";
                parentNode.Value = "WorkTeams";
                parentNode.Category = "Root";
                parentNode.ImageUrl = "/Images/Common16/WorkTeam.png";
                parentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                ConfigurationTree.Nodes.Add (parentNode);

            }

            if ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkQueueReview))
                || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkQueueManage))) {

                parentNode = new Telerik.Web.UI.RadTreeNode ();

                parentNode.Text = "Work Queues";
                parentNode.Value = "WorkQueues";
                parentNode.Category = "Root";
                parentNode.ImageUrl = "/Images/Common16/WorkQueue.png";
                parentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                ConfigurationTree.Nodes.Add (parentNode);

            }

            if ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkQueueViewReview))
                || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkQueueViewManage))) {

                parentNode = new Telerik.Web.UI.RadTreeNode ();

                parentNode.Text = "Work Queue Views";
                parentNode.Value = "WorkQueueViews";
                parentNode.Category = "Root";
                parentNode.ImageUrl = "/Images/Common16/WorkQueueView.png";
                parentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                ConfigurationTree.Nodes.Add (parentNode);

            }

            if ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkOutcomeReview))
                || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkOutcomeManage))) {

                parentNode = new Telerik.Web.UI.RadTreeNode ();

                parentNode.Text = "Work Outcomes";
                parentNode.Value = "WorkOutcomes";
                parentNode.Category = "Root";
                parentNode.ImageUrl = "/Images/Common16/WorkOutcome.png";
                parentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                ConfigurationTree.Nodes.Add (parentNode);

            }

            if ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.RoutingRuleReview))
                || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.RoutingRuleManage))) {

                parentNode = new Telerik.Web.UI.RadTreeNode ();

                parentNode.Text = "Routing Rules";
                parentNode.Value = "RoutingRules";
                parentNode.Category = "Root";
                parentNode.ImageUrl = "/Images/Common16/RoutingRule.png";
                parentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                ConfigurationTree.Nodes.Add (parentNode);

            }

            if ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MedicalServiceReview))
                || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MedicalServiceManage))) {

                parentNode = new Telerik.Web.UI.RadTreeNode ();

                parentNode.Text = "Medical Services";
                parentNode.Value = "MedicalServices";
                parentNode.Category = "Root";
                parentNode.ImageUrl = "/Images/Common16/FolderOpenTabs.png";
                parentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                ConfigurationTree.Nodes.Add (parentNode);

            }

            if ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MetricReview))
                || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MetricManage))) {

                parentNode = new Telerik.Web.UI.RadTreeNode ();

                parentNode.Text = "Metrics";
                parentNode.Value = "Metrics";
                parentNode.Category = "Root";
                parentNode.ImageUrl = "/Images/Common16/Metric.png";
                parentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                ConfigurationTree.Nodes.Add (parentNode);

            }

            if ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.AuthorizedServiceReview))
                || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.AuthorizedServiceManage))) {

                parentNode = new Telerik.Web.UI.RadTreeNode ();

                parentNode.Text = "Authorized Services";
                parentNode.Value = "AuthorizedServices";
                parentNode.Category = "Root";
                parentNode.ImageUrl = "/Images/Common16/AuthorizedService.png";
                parentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                ConfigurationTree.Nodes.Add (parentNode);

            }


            if ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ConditionReview))
                || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ConditionManage))) {

                parentNode = new Telerik.Web.UI.RadTreeNode ();

                parentNode.Text = "Conditions";
                parentNode.Value = "Conditions";
                parentNode.Category = "Root";
                parentNode.ImageUrl = "/Images/Common16/Condition.png";
                parentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                ConfigurationTree.Nodes.Add (parentNode);

            }


            if ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.PopulationTypeReview))
                || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.PopulationTypeManage))) {

                parentNode = new Telerik.Web.UI.RadTreeNode ();

                parentNode.Text = "Population Types";
                parentNode.Value = "PopulationTypes";
                parentNode.Category = "Root";
                parentNode.ImageUrl = "/Images/Common16/PopulationType.png";
                parentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                ConfigurationTree.Nodes.Add (parentNode);

            }


            if ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.PopulationReview))
                || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.PopulationManage))) {

                parentNode = new Telerik.Web.UI.RadTreeNode ();

                parentNode.Text = "Populations";
                parentNode.Value = "Populations";
                parentNode.Category = "Root";
                parentNode.ImageUrl = "/Images/Common16/PopulationCareManagement.png";
                parentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                ConfigurationTree.Nodes.Add (parentNode);

            }


            if ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareMeasureScaleReview))
                || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareMeasureScaleManage))) {

                parentNode = new Telerik.Web.UI.RadTreeNode ();

                parentNode.Text = "Care Measure Scales";
                parentNode.Value = "CareMeasureScales";
                parentNode.Category = "Root";
                parentNode.ImageUrl = "/Images/Common16/CareMeasureScale.png";
                parentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                ConfigurationTree.Nodes.Add (parentNode);

            }

            if ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareMeasureReview))
                || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareMeasureManage))) {

                parentNode = new Telerik.Web.UI.RadTreeNode ();

                parentNode.Text = "Care Measures";
                parentNode.Value = "CareMeasures";
                parentNode.Category = "Root";
                parentNode.ImageUrl = "/Images/Common16/CareMeasure.png";
                parentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                ConfigurationTree.Nodes.Add (parentNode);

            }

            if ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareInterventionReview))
                || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareInterventionManage))) {

                parentNode = new Telerik.Web.UI.RadTreeNode ();

                parentNode.Text = "Care Interventions";
                parentNode.Value = "CareInterventions";
                parentNode.Category = "Root";
                parentNode.ImageUrl = "/Images/Common16/CareIntervention.png";
                parentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                ConfigurationTree.Nodes.Add (parentNode);

            }


            if ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareLevelReview))
                || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareLevelManage))) {

                parentNode = new Telerik.Web.UI.RadTreeNode ();

                parentNode.Text = "Care Levels";
                parentNode.Value = "CareLevels";
                parentNode.Category = "Root";
                parentNode.ImageUrl = "/Images/Common16/CareLevel.png";
                parentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                ConfigurationTree.Nodes.Add (parentNode);

            }

            if ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CarePlanReview))
                || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CarePlanManage))) {

                parentNode = new Telerik.Web.UI.RadTreeNode ();

                parentNode.Text = "Care Plans";
                parentNode.Value = "CarePlans";
                parentNode.Category = "Root";
                parentNode.ImageUrl = "/Images/Common16/CarePlan.png";
                parentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                ConfigurationTree.Nodes.Add (parentNode);

            }

            if ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ProblemStatementReview))
                || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ProblemStatementManage))) {

                parentNode = new Telerik.Web.UI.RadTreeNode ();

                parentNode.Text = "Problem Statements";
                parentNode.Value = "ProblemStatements";
                parentNode.Category = "Root";
                parentNode.ImageUrl = "/Images/Common16/ProblemStatement.png";
                parentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                ConfigurationTree.Nodes.Add (parentNode);

            }

            if ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareOutcomeReview))
                || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareOutcomeManage))) {

                parentNode = new Telerik.Web.UI.RadTreeNode ();

                parentNode.Text = "Care Outcomes";
                parentNode.Value = "CareOutcomes";
                parentNode.Category = "Root";
                parentNode.ImageUrl = "/Images/Common16/Case.png";
                parentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                ConfigurationTree.Nodes.Add (parentNode);

            }

            return;

        }

        protected void Initialize_ConfigurationGrid () {

            ConfigurationGrid.MasterTableView.Columns.Clear ();

            ConfigurationGrid.MasterTableView.DataKeyNames = new String[0];

            //ConfigurationGridAddColumn ("ContextMenu", "ContextMenu", false, false);

            //ConfigurationGrid.MasterTableView.SortExpressions.Clear ();

            //ConfigurationGrid.DataSource = new System.Data.DataTable ();

            //ConfigurationGrid.Rebind ();

            return;

        }

        protected void ConfigurationGridAddColumn (String columnName, String boundField, Boolean isVisible, Boolean hasImage) {

            Telerik.Web.UI.GridBoundColumn gridColumn;

            gridColumn = new Telerik.Web.UI.GridBoundColumn ();

            ConfigurationGrid.MasterTableView.Columns.Add (gridColumn);

            ConfigurationGrid.MasterTableView.Columns[ConfigurationGrid.MasterTableView.Columns.Count - 1].Visible = isVisible;

            gridColumn.UniqueName = columnName.Replace (" ", "");

            gridColumn.HeaderText = columnName;

            gridColumn.DataField = boundField;

            gridColumn.Visible = isVisible;

            if (!String.IsNullOrEmpty (boundField)) {

                String[] dataKeyNames = new String[(ConfigurationGrid.MasterTableView.DataKeyNames.Length + 1)];

                ConfigurationGrid.MasterTableView.DataKeyNames.CopyTo (dataKeyNames, 0);

                dataKeyNames[dataKeyNames.Length - 1] = boundField;

                ConfigurationGrid.MasterTableView.DataKeyNames = dataKeyNames;

            }

            return;

        }

        protected System.Data.DataTable CreateDataTable (String columnList) {

            System.Data.DataTable newTable = new System.Data.DataTable ();

            foreach (String currentColumn in columnList.Split (';')) {

                newTable.Columns.Add (currentColumn.Trim ());

            }

            return newTable;

        }

        #endregion


        #region Node Support Methods

        protected String GetNodeType (String nodeValue) {

            String controlType = String.Empty;

            controlType = nodeValue.Split ('/')[nodeValue.Split ('/').Length - 1];

            controlType = controlType.Split ('|')[0];

            return controlType;

        }

        protected String GetNodeId (String nodeValue) {

            String controlId = String.Empty;

            controlId = nodeValue.Split ('/')[nodeValue.Split ('/').Length - 1];

            controlId = controlId.Split ('|')[1];

            return controlId;

        }

        #endregion


        #region Configuration Tree Node Events

        protected void ConfigurationTree_OnNodeExpand (Object sender, Telerik.Web.UI.RadTreeNodeEventArgs eventArgs) {

            Telerik.Web.UI.RadTreeNode childNode;

            // System.Diagnostics.Debug.WriteLine ("Node Clicked Path: " + eventArgs.Node.Value);

            // System.Diagnostics.Debug.WriteLine ("Node Clicked Path Depth: " + eventArgs.Node.Value.Split ('/').Length.ToString ());


            if (MercuryApplication == null) { return; }

            ConfigurationTree.LoadXmlString ((String)Session[SessionCachePrefix + "ConfigurationTreeState"]);

            Telerik.Web.UI.RadTreeNode expandedNode = ConfigurationTree.FindNodeByValue (eventArgs.Node.Value);

            expandedNode.Nodes.Clear ();


            #region Evaluate Expanded Node

            switch (eventArgs.Node.Value.Split ('/')[0]) {

                case "MedicalServices":

                    #region Service Events Node

                    if (eventArgs.Node.Value.Split ('/').Length == 1) {

                        childNode = new Telerik.Web.UI.RadTreeNode ();
                        childNode.Text = "Service Singletons";
                        childNode.Value = "MedicalServices/ServiceSingletons";
                        childNode.Category = "MedicalServices.Singleton";
                        childNode.ImageUrl = "/Images/Common16/Document2.png";
                        childNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                        expandedNode.Nodes.Add (childNode);

                        childNode = new Telerik.Web.UI.RadTreeNode ();
                        childNode.Text = "Service Sets";
                        childNode.Value = "MedicalServices/ServiceSets";
                        childNode.Category = "MedicalServices.ServiceSet";
                        childNode.ImageUrl = "/Images/Common16/DocumentCollection.png";
                        childNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                        expandedNode.Nodes.Add (childNode);

                    }

                    #endregion

                    break;

            }

            #endregion


            expandedNode.Expanded = true;

            while (expandedNode.ParentNode != null) {

                expandedNode = expandedNode.ParentNode;

                expandedNode.Expanded = true;

            }

            Session[SessionCachePrefix + "ConfigurationTreeState"] = ConfigurationTree.GetXml ();

            return;

        }

        protected void ConfigurationTree_OnNodeCollapse (Object sender, Telerik.Web.UI.RadTreeNodeEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            ConfigurationTree.LoadXmlString ((String)Session[SessionCachePrefix + "ConfigurationTreeState"]);

            Telerik.Web.UI.RadTreeNode collapsedNode = ConfigurationTree.FindNodeByValue (eventArgs.Node.Value);

            collapsedNode.Expanded = false;

            Session[SessionCachePrefix + "ConfigurationTreeState"] = ConfigurationTree.GetXml ();

            return;

        }

        protected void ConfigurationTree_OnNodeClick (Object sender, Telerik.Web.UI.RadTreeNodeEventArgs eventArgs) {

            // System.Diagnostics.Debug.WriteLine ("OnNodeClick: " + eventArgs.Node.Value);

            if (MercuryApplication == null) { return; }

            ConfigurationTree.LoadXmlString ((String)Session[SessionCachePrefix + "ConfigurationTreeState"]);

            foreach (Telerik.Web.UI.RadTreeNode selectedNode in ConfigurationTree.SelectedNodes) {

                selectedNode.Selected = false;

            }

            Telerik.Web.UI.RadTreeNode clickedNode = ConfigurationTree.FindNodeByValue (eventArgs.Node.Value);

            clickedNode.Selected = true;

            Session[SessionCachePrefix + "ConfigurationTreeState"] = ConfigurationTree.GetXml ();

            Initialize_ConfigurationGrid ();

            System.Data.DataTable gridDataTable = new System.Data.DataTable ();

            switch (clickedNode.Value.Split ('/')[0].ToLower ()) {

                case "reportingservers":

                    #region Reporting Servers

                    ConfigurationGridAddColumn ("ObjectType", "ObjectType", false, false);

                    ConfigurationGridAddColumn ("Id", "Id", true, false);

                    ConfigurationGridAddColumn ("Name", "Name", true, false);
                    

                    ConfigurationGridAddColumn ("Assembly Path", "AssemblyPath", true, false);

                    ConfigurationGridAddColumn ("Assembly Name", "AssemblyName", true, false);

                    ConfigurationGridAddColumn ("Class Name", "AssemblyClassName", true, false);

                    
                    ConfigurationGridAddColumn ("Enabled", "Enabled", true, false);

                    ConfigurationGridAddColumn ("Visible", "Visible", true, false);


                    ConfigurationGrid.MasterTableView.ClientDataKeyNames = "ObjectType;Id".Split (';');

                    ConfigurationGrid.DataSource = MercuryApplication.ReportingServersAvailable (false);

                    ConfigurationGrid.Rebind ();

                    #endregion

                    break;

                case "faxservers":

                    #region Fax Servers

                    ConfigurationGridAddColumn ("ObjectType", "ObjectType", false, false);

                    ConfigurationGridAddColumn ("Id", "Id", true, false);

                    ConfigurationGridAddColumn ("Name", "Name", true, false);


                    ConfigurationGridAddColumn ("Assembly Path", "AssemblyPath", true, false);

                    ConfigurationGridAddColumn ("Assembly Name", "AssemblyName", true, false);

                    ConfigurationGridAddColumn ("Class Name", "AssemblyClassName", true, false);


                    ConfigurationGridAddColumn ("Enabled", "Enabled", true, false);

                    ConfigurationGridAddColumn ("Visible", "Visible", true, false);


                    ConfigurationGrid.MasterTableView.ClientDataKeyNames = "ObjectType;Id".Split (';');

                    ConfigurationGrid.DataSource = MercuryApplication.FaxServersAvailable (false);

                    ConfigurationGrid.Rebind ();

                    #endregion

                    break;

                case "printers":

                    #region Printer

                    ConfigurationGridAddColumn ("ObjectType", "ObjectType", false, false);

                    ConfigurationGridAddColumn ("Id", "Id", true, false);

                    ConfigurationGridAddColumn ("Name", "Name", true, false);


                    ConfigurationGridAddColumn ("Print Server", "PrintServerName", true, false);

                    ConfigurationGridAddColumn ("Print Queue", "PrintQueueName", true, false);

                    
                    ConfigurationGridAddColumn ("Enabled", "Enabled", true, false);

                    ConfigurationGridAddColumn ("Visible", "Visible", true, false);


                    ConfigurationGrid.MasterTableView.ClientDataKeyNames = "ObjectType;Id".Split (';');

                    ConfigurationGrid.DataSource = MercuryApplication.PrintersAvailable (false);

                    ConfigurationGrid.Rebind ();

                    #endregion

                    break;

                case "notetypes":
                    
                    #region Note Types
                    
                    ConfigurationGridAddColumn ("ObjectType", "ObjectType", false, false);
                    
                    ConfigurationGridAddColumn ("Id", "Id", true, false);

                    ConfigurationGridAddColumn ("Name", "Name", true, false);


                    ConfigurationGridAddColumn ("Enabled", "Enabled", true, false);

                    ConfigurationGridAddColumn ("Visible", "Visible", true, false);
                    

                    ConfigurationGrid.MasterTableView.ClientDataKeyNames = "ObjectType;Id".Split (';');

                    ConfigurationGrid.DataSource = MercuryApplication.NoteTypesAvailable (false);

                    ConfigurationGrid.Rebind ();

                    #endregion

                    break;

                case "contactregardings":

                    #region Contact Regardings
                    
                    ConfigurationGridAddColumn ("ObjectType", "ObjectType", false, false);
                    
                    ConfigurationGridAddColumn ("Id", "Id", true, false);

                    ConfigurationGridAddColumn ("Name", "Name", true, false);


                    ConfigurationGridAddColumn ("Enabled", "Enabled", true, false);

                    ConfigurationGridAddColumn ("Visible", "Visible", true, false);
                    

                    ConfigurationGrid.MasterTableView.ClientDataKeyNames = "ObjectType;Id".Split (';');

                    ConfigurationGrid.DataSource = MercuryApplication.ContactRegardingsAvailable (false);

                    ConfigurationGrid.Rebind ();

                    #endregion

                    break;

                case "correspondences":

                    #region Correspondences
                    
                    ConfigurationGridAddColumn ("ObjectType", "ObjectType", false, false);
                    
                    ConfigurationGridAddColumn ("Id", "Id", true, false);

                    ConfigurationGridAddColumn ("Name", "Name", true, false);

                    ConfigurationGridAddColumn ("Version", "Version", true, false);

                    ConfigurationGridAddColumn ("Enabled", "Enabled", true, false);

                    ConfigurationGridAddColumn ("Visible", "Visible", true, false);
                    

                    ConfigurationGrid.MasterTableView.ClientDataKeyNames = "ObjectType;Id".Split (';');

                    ConfigurationGrid.DataSource = MercuryApplication.CorrespondencesAvailable (false);

                    ConfigurationGrid.Rebind ();

                    #endregion

                    break;

                case "forms":

                    #region Forms

                    ConfigurationGridAddColumn ("ObjectType", "ObjectType", false, false);

                    ConfigurationGridAddColumn ("Id", "Id", true, false);

                    ConfigurationGridAddColumn ("Name", "Name", true, false);


                    ConfigurationGrid.MasterTableView.ClientDataKeyNames = "ObjectType;Id".Split (';');

                    ConfigurationGrid.DataSource = MercuryApplication.FormsAvailable (false);

                    ConfigurationGrid.Rebind ();

                    #endregion

                    break;

                case "workflows":

                    #region Workflows

                    ConfigurationGrid.MasterTableView.Columns.Clear ();


                    ConfigurationGridAddColumn ("ObjectType", "ObjectType", false, false);
                    
                    ConfigurationGridAddColumn ("Id", "Id", true, false);

                    ConfigurationGridAddColumn ("Name", "Name", true, false);


                    ConfigurationGridAddColumn ("Entity", "EntityType", true, false);

                    ConfigurationGridAddColumn ("Framework", "Framework", true, false);
                    
                    ConfigurationGridAddColumn ("Assembly Path", "AssemblyPath", true, false);

                    ConfigurationGridAddColumn ("Assembly Name", "AssemblyName", true, false);

                    ConfigurationGridAddColumn ("Class Name", "AssemblyClassName", true, false);


                    ConfigurationGridAddColumn ("Enabled", "Enabled", true, false);

                    ConfigurationGridAddColumn ("Visible", "Visible", true, false);


                    ConfigurationGrid.MasterTableView.ClientDataKeyNames = "ObjectType;Id".Split (';');

                    ConfigurationGrid.DataSource = MercuryApplication.WorkflowsAvailable (false);

                    ConfigurationGrid.Rebind ();

                    #endregion

                    break;

                case "workteams":

                    #region Work Teams
                    
                    ConfigurationGridAddColumn ("ObjectType", "ObjectType", false, false);
                    
                    ConfigurationGridAddColumn ("Id", "Id", true, false);

                    ConfigurationGridAddColumn ("Name", "Name", true, false);


                    ConfigurationGridAddColumn ("Enabled", "Enabled", true, false);

                    ConfigurationGridAddColumn ("Visible", "Visible", true, false);
                    

                    ConfigurationGrid.MasterTableView.ClientDataKeyNames = "ObjectType;Id".Split (';');

                    ConfigurationGrid.DataSource = MercuryApplication.WorkTeamsAvailable (false);

                    ConfigurationGrid.Rebind ();

                    #endregion

                    break;

                case "workqueues":

                    #region Work Queues
                 
                    ConfigurationGridAddColumn ("ObjectType", "ObjectType", false, false);
                    
                    ConfigurationGridAddColumn ("Id", "Id", true, false);

                    ConfigurationGridAddColumn ("Name", "Name", true, false);


                    ConfigurationGridAddColumn ("Workflow", "WorkflowName", true, false);

                    ConfigurationGridAddColumn ("Constraint", "InitialConstraintDescription", true, false);
                    
                    ConfigurationGridAddColumn ("Milestone", "InitialMilestoneDescription", true, false);

                    ConfigurationGridAddColumn ("Threshold", "ThresholdDescription", true, false);

                    ConfigurationGridAddColumn ("Schedule", "ScheduleDescription", true, false);


                    ConfigurationGridAddColumn ("Enabled", "Enabled", true, false);

                    ConfigurationGridAddColumn ("Visible", "Visible", true, false);


                    ConfigurationGrid.MasterTableView.ClientDataKeyNames = "ObjectType;Id".Split (';');

                    ConfigurationGrid.DataSource = MercuryApplication.WorkQueuesAvailable (false);

                    ConfigurationGrid.Rebind ();

                    #endregion

                    break;

                case "workqueueviews":
                    
                    #region Work Queues
                 
                    ConfigurationGridAddColumn ("ObjectType", "ObjectType", false, false);
                    
                    ConfigurationGridAddColumn ("Id", "Id", true, false);

                    ConfigurationGridAddColumn ("Name", "Name", true, false);

                    ConfigurationGridAddColumn ("Description", "Description", true, false);


                    ConfigurationGrid.MasterTableView.ClientDataKeyNames = "ObjectType;Id".Split (';');

                    ConfigurationGrid.DataSource = MercuryApplication.WorkQueueViewsAvailable (false);

                    ConfigurationGrid.Rebind ();

                    #endregion

                    break;

                case "workoutcomes":

                    #region Work Outcomes
                    
                    ConfigurationGridAddColumn ("ObjectType", "ObjectType", false, false);
                    
                    ConfigurationGridAddColumn ("Id", "Id", true, false);

                    ConfigurationGridAddColumn ("Name", "Name", true, false);


                    ConfigurationGridAddColumn ("Enabled", "Enabled", true, false);

                    ConfigurationGridAddColumn ("Visible", "Visible", true, false);
                    

                    ConfigurationGrid.MasterTableView.ClientDataKeyNames = "ObjectType;Id".Split (';');

                    ConfigurationGrid.DataSource = MercuryApplication.WorkOutcomesAvailable (false);

                    ConfigurationGrid.Rebind ();

                    #endregion

                    break;

                case "routingrules":

                    #region Routing Rules

                    ConfigurationGridAddColumn ("ObjectType", "ObjectType", false, false);
                    
                    ConfigurationGridAddColumn ("Id", "Id", true, false);

                    ConfigurationGridAddColumn ("Name", "Name", true, false);
                    
                    ConfigurationGridAddColumn ("Enabled", "Enabled", true, false);

                    ConfigurationGridAddColumn ("Visible", "Visible", true, false);

                    
                    ConfigurationGrid.MasterTableView.ClientDataKeyNames = "ObjectType;Id".Split (';');

                    ConfigurationGrid.DataSource = MercuryApplication.RoutingRulesAvailable (false);

                    ConfigurationGrid.Rebind ();

                    #endregion

                    break;

                case "medicalservices":

                    #region Medical Services

                    
                    ConfigurationGridAddColumn ("ObjectType", "ObjectType", false, false);
                    
                    ConfigurationGridAddColumn ("Id", "Id", true, false);

                    ConfigurationGridAddColumn ("Name", "Name", true, false);

                    ConfigurationGridAddColumn ("Type", "ServiceType", true, false);

                    ConfigurationGridAddColumn ("Enabled", "Enabled", true, false);

                    ConfigurationGridAddColumn ("Visible", "Visible", true, false);

                    ConfigurationGridAddColumn ("Last Paid Date", "LastPaidDate", true, false);

                    

                    ConfigurationGrid.MasterTableView.ClientDataKeyNames = "ObjectType;Id;ServiceType".Split (';');

                    ConfigurationGrid.DataSource = MercuryApplication.MedicalServiceHeadersGet (false);

                    ConfigurationGrid.Rebind ();


                    #endregion

                    break;

                case "metrics":

                    #region Metrics

                    ConfigurationGridAddColumn ("ObjectType", "ObjectType", true, false);

                    ConfigurationGridAddColumn ("Id", "Id", true, false);

                    ConfigurationGridAddColumn ("Name", "Name", true, false);

                    ConfigurationGridAddColumn ("Type", "MetricType", true, false);

                    ConfigurationGridAddColumn ("Data Type", "DataType", true, false);

                    ConfigurationGridAddColumn ("Range", "ValueRangeDescription", true, false);

                    ConfigurationGridAddColumn ("Enabled", "Enabled", true, false);

                    ConfigurationGridAddColumn ("Visible", "Visible", true, false);


                    ConfigurationGrid.MasterTableView.ClientDataKeyNames = "ObjectType;Id".Split (';');

                    ConfigurationGrid.DataSource = MercuryApplication.MetricsAvailable (false);

                    ConfigurationGrid.Rebind ();

                    #endregion

                    break;

                case "authorizedservices":

                    #region Authorized Services

                    ConfigurationGridAddColumn ("ObjectType", "ObjectType", false, false);

                    ConfigurationGridAddColumn ("Id", "Id", true, false);

                    ConfigurationGridAddColumn ("Name", "Name", true, false);

                    ConfigurationGridAddColumn ("Description", "Description", true, false);

                    ConfigurationGridAddColumn ("Enabled", "Enabled", true, false);

                    ConfigurationGridAddColumn ("Visible", "Visible", true, false);                                     
                    

                    ConfigurationGrid.MasterTableView.ClientDataKeyNames = "ObjectType;Id".Split (';');

                    ConfigurationGrid.DataSource = MercuryApplication.AuthorizedServicesAvailable (false);

                    ConfigurationGrid.Rebind ();

                    #endregion

                    break;

                case "conditions":

                    #region Conditions

                    ConfigurationGridAddColumn ("ObjectType", "ObjectType", false, false);

                    ConfigurationGridAddColumn ("Id", "Id", true, false);

                    ConfigurationGridAddColumn ("Class", "ConditionClassName", true, false);

                    ConfigurationGridAddColumn ("Name", "Name", true, false);


                    ConfigurationGridAddColumn ("Enabled", "Enabled", true, false);

                    ConfigurationGridAddColumn ("Visible", "Visible", true, false);


                    ConfigurationGrid.MasterTableView.ClientDataKeyNames = "ObjectType;Id".Split (';');

                    ConfigurationGrid.DataSource = MercuryApplication.ConditionsAvailable (false);

                    ConfigurationGrid.Rebind ();

                    #endregion

                    break;

                case "populationtypes":

                    #region PopulationTypes
                    
                    ConfigurationGridAddColumn ("ObjectType", "ObjectType", false, false);
                    
                    ConfigurationGridAddColumn ("Id", "Id", true, false);

                    ConfigurationGridAddColumn ("Name", "Name", true, false);


                    ConfigurationGridAddColumn ("Enabled", "Enabled", true, false);

                    ConfigurationGridAddColumn ("Visible", "Visible", true, false);
                    

                    ConfigurationGrid.MasterTableView.ClientDataKeyNames = "ObjectType;Id".Split (';');

                    ConfigurationGrid.DataSource = MercuryApplication.PopulationTypesAvailable (false);

                    ConfigurationGrid.Rebind ();

                    #endregion

                    break;

                case "populations":

                    #region Populations
                                        
                    ConfigurationGridAddColumn ("ObjectType", "ObjectType", false, false);
                    
                    ConfigurationGridAddColumn ("Id", "Id", true, false);

                    ConfigurationGridAddColumn ("Name", "Name", true, false);


                    ConfigurationGridAddColumn ("Enabled", "Enabled", true, false);

                    ConfigurationGridAddColumn ("Visible", "Visible", true, false);

                        
                    ConfigurationGrid.MasterTableView.ClientDataKeyNames = "ObjectType;Id".Split (';');

                    ConfigurationGrid.DataSource = MercuryApplication.PopulationsAvailable (false);

                    ConfigurationGrid.Rebind ();

                    #endregion

                    break;

                case "caremeasurescales":

                    #region Care Measure Scales

                    ConfigurationGridAddColumn ("ObjectType", "ObjectType", false, false);

                    ConfigurationGridAddColumn ("Id", "Id", true, false);

                    ConfigurationGridAddColumn ("Name", "Name", true, false);


                    ConfigurationGridAddColumn ("Enabled", "Enabled", true, false);

                    ConfigurationGridAddColumn ("Visible", "Visible", true, false);


                    ConfigurationGrid.MasterTableView.ClientDataKeyNames = "ObjectType;Id".Split (';');

                    ConfigurationGrid.DataSource = MercuryApplication.CareMeasureScalesAvailable (false);

                    ConfigurationGrid.Rebind ();

                    #endregion

                    break;

                case "caremeasures":

                    #region Care Measures

                    ConfigurationGridAddColumn ("ObjectType", "ObjectType", false, false);

                    ConfigurationGridAddColumn ("Id", "Id", true, false);


                    ConfigurationGridAddColumn ("Domain", "CareMeasureDomainName", true, false);

                    ConfigurationGridAddColumn ("Class", "CareMeasureClassName", true, false);

                    ConfigurationGridAddColumn ("Name", "Name", true, false);
                    

                    ConfigurationGridAddColumn ("Enabled", "Enabled", true, false);

                    ConfigurationGridAddColumn ("Visible", "Visible", true, false);


                    ConfigurationGrid.MasterTableView.ClientDataKeyNames = "ObjectType;Id".Split (';');

                    ConfigurationGrid.DataSource = MercuryApplication.CareMeasuresAvailable (false);

                    ConfigurationGrid.Rebind ();

                    #endregion

                    break;

                case "careinterventions":

                    #region Care Interventions

                    ConfigurationGridAddColumn ("ObjectType", "ObjectType", false, false);

                    ConfigurationGridAddColumn ("Id", "Id", true, false);


                    ConfigurationGridAddColumn ("Name", "Name", true, false);

                    ConfigurationGridAddColumn ("Description", "Description", true, false);


                    ConfigurationGridAddColumn ("Enabled", "Enabled", true, false);

                    ConfigurationGridAddColumn ("Visible", "Visible", true, false);


                    ConfigurationGrid.MasterTableView.ClientDataKeyNames = "ObjectType;Id".Split (';');

                    ConfigurationGrid.DataSource = MercuryApplication.CareInterventionsAvailable (false);

                    ConfigurationGrid.Rebind ();

                    #endregion

                    break;

                case "problemstatements":

                    #region Problem Statements

                    ConfigurationGridAddColumn ("ObjectType", "ObjectType", false, false);

                    ConfigurationGridAddColumn ("Id", "Id", true, false);

                    ConfigurationGridAddColumn ("Domain", "ProblemDomainName", true, false);

                    ConfigurationGridAddColumn ("Class", "ProblemClassName", true, false);

                    ConfigurationGridAddColumn ("Name", "Name", true, false);

                    ConfigurationGridAddColumn ("Default Care Plan", "DefaultCarePlanName", true, false);

                    ConfigurationGridAddColumn ("Enabled", "Enabled", true, false);

                    ConfigurationGridAddColumn ("Visible", "Visible", true, false);


                    var problemStatementsAvailable =

                        from currentProblemStatement in MercuryApplication.ProblemStatementsAvailable (false)

                        orderby currentProblemStatement.ProblemDomainName, currentProblemStatement.ProblemClassName, currentProblemStatement.Name

                        select currentProblemStatement;


                    ConfigurationGrid.MasterTableView.ClientDataKeyNames = "ObjectType;Id".Split (';');

                    // ConfigurationGrid.DataSource = MercuryApplication.ProblemStatementsAvailable (false);

                    ConfigurationGrid.DataSource = problemStatementsAvailable;

                    ConfigurationGrid.Rebind ();

                    #endregion

                    break;

                case "carelevels":

                    #region Care Levels
                    
                    ConfigurationGridAddColumn ("ObjectType", "ObjectType", false, false);
                    
                    ConfigurationGridAddColumn ("Id", "Id", true, false);

                    ConfigurationGridAddColumn ("Name", "Name", true, false);


                    ConfigurationGridAddColumn ("Enabled", "Enabled", true, false);

                    ConfigurationGridAddColumn ("Visible", "Visible", true, false);

                        
                    ConfigurationGrid.MasterTableView.ClientDataKeyNames = "ObjectType;Id".Split (';');

                    ConfigurationGrid.DataSource = MercuryApplication.CareLevelsAvailable (false);

                    ConfigurationGrid.Rebind ();
                    
                    #endregion

                    break;

                case "careplans":

                    #region Care Levels

                    ConfigurationGridAddColumn ("ObjectType", "ObjectType", false, false);

                    ConfigurationGridAddColumn ("Id", "Id", true, false);


                    ConfigurationGridAddColumn ("Name", "Name", true, false);
                    
                    ConfigurationGridAddColumn ("Description", "Description", true, false);


                    ConfigurationGridAddColumn ("Enabled", "Enabled", true, false);

                    ConfigurationGridAddColumn ("Visible", "Visible", true, false);


                    ConfigurationGrid.MasterTableView.ClientDataKeyNames = "ObjectType;Id".Split (';');

                    ConfigurationGrid.DataSource = MercuryApplication.CarePlansAvailable (false);

                    ConfigurationGrid.Rebind ();

                    #endregion

                    break;
                    
                case "careoutcomes":

                    #region Care Outcomes

                    ConfigurationGridAddColumn ("ObjectType", "ObjectType", false, false);

                    ConfigurationGridAddColumn ("Id", "Id", true, false);

                    ConfigurationGridAddColumn ("Name", "Name", true, false);


                    ConfigurationGridAddColumn ("Enabled", "Enabled", true, false);

                    ConfigurationGridAddColumn ("Visible", "Visible", true, false);


                    ConfigurationGrid.MasterTableView.ClientDataKeyNames = "ObjectType;Id".Split (';');

                    ConfigurationGrid.DataSource = MercuryApplication.CareOutcomesAvailable (false);

                    ConfigurationGrid.Rebind ();

                    #endregion

                    break;

            }

        }

        #endregion

    }

}