using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.DataExplorer.Exports {

    public partial class DataExplorerNodeResultExportMember : System.Web.UI.Page {

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

        private Guid NodeInstanceId {

            get {

                if (Session[SessionCachePrefix + "NodeInstanceId"] == null) { return Guid.Empty; }

                return (Guid)Session[SessionCachePrefix + "NodeInstanceId"];

            }

            set { Session[SessionCachePrefix + "NodeInstanceId"] = value; }

        }

        private Int32 NodeInstanceCount {

            get {

                if (Session[SessionCachePrefix + "LastExecuteNodeInstanceCount"] == null) { return 0; }

                return (Int32)Session[SessionCachePrefix + "LastExecuteNodeInstanceCount"];

            }

            set { Session[SessionCachePrefix + "LastExecuteNodeInstanceCount"] = value; }

        }

        private String DataExplorerTreeViewState {

            get {

                if (Session[SessionCachePrefix + "DataExplorerTreeViewState"] == null) { return String.Empty; }

                return (String)Session[SessionCachePrefix + "DataExplorerTreeViewState"];

            }

            set { Session[SessionCachePrefix + "DataExplorerTreeViewState"] = value; }

        }

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            Guid forNodeInstanceId = Guid.Empty;

            Int32 forNodeInstanceCount = 0;


            if (MercuryApplication == null) { return; }

            if ((!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.DataExplorerReview))

                && (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.DataExplorerManage))) { Response.Redirect ("/PermissionDenied.aspx", true); return; }


            if (!Page.IsPostBack) {

                #region Initial Page Load

                if (Request.QueryString["NodeInstanceId"] != null) {

                    forNodeInstanceId = Guid.Parse (Request.QueryString["NodeInstanceId"]);

                    NodeInstanceId = forNodeInstanceId;

                }

                if (Request.QueryString["NodeInstanceCount"] != null) {

                    forNodeInstanceCount = Int32.Parse (Request.QueryString["NodeInstanceCount"]);

                    NodeInstanceCount = forNodeInstanceCount;

                }


                InitializeAll ();

                #endregion

            } // Initial Page Load

            AjaxScriptManager.AsyncPostBackTimeout = 1200;

            return;

        }

        protected void Page_Unload (object sender, EventArgs e) {

            isPageUnloading = true;

            if (MercuryApplication != null) { MercuryApplication.ApplicationClientClose (); }

            return;

        }

        #endregion


        #region Initialization

        private void InitializeAll () {

            InitializeTreeViewOfMember ();

            return;

        }

        private void InitializeTreeViewOfMember () {



            DataExplorerTreeView.Nodes.Clear ();


            Telerik.Web.UI.RadTreeNode rootNode = CreateTreeNode ("Member", "Member");

            rootNode.Checkable = false;

            rootNode.Expanded = false;

            DataExplorerTreeView.Nodes.Add (rootNode);


            rootNode.Nodes.Add (CreateTreeNode ("Id", "Id", false));

            rootNode.Nodes.Add (CreateTreeNode ("Name", "Name"));

            rootNode.Nodes.Add (CreateTreeNode ("Description", "Description"));



            rootNode.Nodes.Add (CreateTreeNode ("Birth Date", "BirthDateDescription"));

            rootNode.Nodes.Add (CreateTreeNode ("Death Date", "DeathDate"));
            
            rootNode.Nodes.Add (CreateTreeNode ("Gender Description", "GenderDescription"));

            rootNode.Nodes.Add (CreateTreeNode ("Ethnicity Description", "EthnicityDescription"));

            rootNode.Nodes.Add (CreateTreeNode ("Citizenship Description", "CitizenshipDescription"));

            rootNode.Nodes.Add (CreateTreeNode ("Language Description", "LanguageDescription"));

            rootNode.Nodes.Add (CreateTreeNode ("Marital Status Description", "MaritalStatusDescription"));

            rootNode.Nodes.Add (CreateTreeNode ("Family Id", "FamilyId"));



            rootNode.Nodes.Add (CreateTreeNode ("Current Age (Years)", "CurrentAge"));

            rootNode.Nodes.Add (CreateTreeNode ("Current Age (Months)", "CurrentAgeInMonths"));

            rootNode.Nodes.Add (CreateTreeNode ("Current Age Description", "CurrentAgeDescription"));



            Telerik.Web.UI.RadTreeNode entityNode = CreateTreeNode ("Entity", "Entity");

            entityNode.Checkable = false;

            DataExplorerTreeView.Nodes.Add (entityNode);


            entityNode.Nodes.Add (CreateTreeNode ("Id", "Entity.Id"));

            entityNode.Nodes.Add (CreateTreeNode ("Name", "Entity.Name"));

            entityNode.Nodes.Add (CreateTreeNode ("Description", "Entity.Description"));



            entityNode.Nodes.Add (CreateTreeNode ("Name Last", "Entity.NameLast"));

            entityNode.Nodes.Add (CreateTreeNode ("Name First", "Entity.NameFirst"));

            entityNode.Nodes.Add (CreateTreeNode ("Name Middle", "Entity.NameMiddle"));

            entityNode.Nodes.Add (CreateTreeNode ("Name Prefix", "Entity.NamePrefix"));

            entityNode.Nodes.Add (CreateTreeNode ("Name Suffix", "Entity.NameSuffix"));

            entityNode.Nodes.Add (CreateTreeNode ("Federal Tax Id", "Entity.FederalTaxId"));

            entityNode.Nodes.Add (CreateTreeNode ("Id Code Qualifier", "Entity.IdCodeQualifier"));

            entityNode.Nodes.Add (CreateTreeNode ("Unique Id", "Entity.UniqueId"));


            #region Entity.CurrentMailingAddress

            Telerik.Web.UI.RadTreeNode entityCurrentMailingAddress = CreateTreeNode ("Current Mailing Address", "Entity.CurrentMailingAddress");

            entityCurrentMailingAddress.Checkable = false;

            DataExplorerTreeView.Nodes.Add (entityCurrentMailingAddress);


            entityCurrentMailingAddress.Nodes.Add (CreateTreeNode ("Address Line 1", "Entity.CurrentMailingAddress.Line1"));

            entityCurrentMailingAddress.Nodes.Add (CreateTreeNode ("Address Line 2", "Entity.CurrentMailingAddress.Line2"));

            entityCurrentMailingAddress.Nodes.Add (CreateTreeNode ("City", "Entity.CurrentMailingAddress.City"));

            entityCurrentMailingAddress.Nodes.Add (CreateTreeNode ("State", "Entity.CurrentMailingAddress.State"));

            entityCurrentMailingAddress.Nodes.Add (CreateTreeNode ("ZIP Code", "Entity.CurrentMailingAddress.ZipCode"));

            entityCurrentMailingAddress.Nodes.Add (CreateTreeNode ("ZIP Plus 4", "Entity.CurrentMailingAddress.ZipPlus4"));

            entityCurrentMailingAddress.Nodes.Add (CreateTreeNode ("Address Single Line", "Entity.CurrentMailingAddress.AddressSingleLine"));

            entityCurrentMailingAddress.Nodes.Add (CreateTreeNode ("City, State ZIP", "Entity.CurrentMailingAddress.CityStateZipCode"));

            entityCurrentMailingAddress.Nodes.Add (CreateTreeNode ("Longitude", "Entity.CurrentMailingAddress.Longitude"));

            entityCurrentMailingAddress.Nodes.Add (CreateTreeNode ("Latitude", "Entity.CurrentMailingAddress.Latitude"));

            #endregion 

            
            #region Entity.CurrentPhysicalAddress

            Telerik.Web.UI.RadTreeNode entityCurrentPhysicalAddress = CreateTreeNode ("Current Physical Address", "Entity.CurrentPhysicalAddress");

            entityCurrentPhysicalAddress.Checkable = false;

            DataExplorerTreeView.Nodes.Add (entityCurrentPhysicalAddress);


            entityCurrentPhysicalAddress.Nodes.Add (CreateTreeNode ("Address Line 1", "Entity.CurrentPhysicalAddress.Line1"));

            entityCurrentPhysicalAddress.Nodes.Add (CreateTreeNode ("Address Line 2", "Entity.CurrentPhysicalAddress.Line2"));

            entityCurrentPhysicalAddress.Nodes.Add (CreateTreeNode ("City", "Entity.CurrentPhysicalAddress.City"));

            entityCurrentPhysicalAddress.Nodes.Add (CreateTreeNode ("State", "Entity.CurrentPhysicalAddress.State"));

            entityCurrentPhysicalAddress.Nodes.Add (CreateTreeNode ("ZIP Code", "Entity.CurrentPhysicalAddress.ZipCode"));

            entityCurrentPhysicalAddress.Nodes.Add (CreateTreeNode ("ZIP Plus 4", "Entity.CurrentPhysicalAddress.ZipPlus4"));

            entityCurrentPhysicalAddress.Nodes.Add (CreateTreeNode ("Address Single Line", "Entity.CurrentPhysicalAddress.AddressSingleLine"));

            entityCurrentPhysicalAddress.Nodes.Add (CreateTreeNode ("City, State ZIP", "Entity.CurrentPhysicalAddress.CityStateZipCode"));

            entityCurrentPhysicalAddress.Nodes.Add (CreateTreeNode ("Longitude", "Entity.CurrentPhysicalAddress.Longitude"));

            entityCurrentPhysicalAddress.Nodes.Add (CreateTreeNode ("Latitude", "Entity.CurrentPhysicalAddress.Latitude"));

            #endregion 


            #region Entity.CurrentContactInformationTelephone

            Telerik.Web.UI.RadTreeNode entityCurrentTelephone = CreateTreeNode ("Current Contact Information", "Entity.CurrentContactInformation");

            entityCurrentTelephone.Checkable = false;

            DataExplorerTreeView.Nodes.Add (entityCurrentTelephone);


            entityCurrentTelephone.Nodes.Add (CreateTreeNode ("Alternate Telephone Number", "Entity.CurrentContactInformationAlternateTelephone.NumberFormatted"));

            entityCurrentTelephone.Nodes.Add (CreateTreeNode ("Telephone Number", "Entity.CurrentContactInformationTelephone.NumberFormatted"));

            #endregion 


            #region Current Enrollment

            Telerik.Web.UI.RadTreeNode currentEnrollmentNode = CreateTreeNode ("Current Enrollment", "CurrentEnrollment");

            currentEnrollmentNode.Checkable = false;

            DataExplorerTreeView.Nodes.Add (currentEnrollmentNode);


            currentEnrollmentNode.Nodes.Add (CreateTreeNode ("Sponsor Id", "CurrentEnrollment.SponsorId"));

            currentEnrollmentNode.Nodes.Add (CreateTreeNode ("Sponsor Name", "CurrentEnrollment.Sponsor.Name"));


            currentEnrollmentNode.Nodes.Add (CreateTreeNode ("Subscriber Id", "CurrentEnrollment.SubscriberId"));

            currentEnrollmentNode.Nodes.Add (CreateTreeNode ("Subscriber Name", "CurrentEnrollment.Subscriber.Name"));

            currentEnrollmentNode.Nodes.Add (CreateTreeNode ("Insurer Id", "CurrentEnrollment.Insurer"));

            currentEnrollmentNode.Nodes.Add (CreateTreeNode ("Insurer Name", "CurrentEnrollment.Insurer.Name"));

            currentEnrollmentNode.Nodes.Add (CreateTreeNode ("Program Id", "CurrentEnrollment.ProgramId"));

            currentEnrollmentNode.Nodes.Add (CreateTreeNode ("Program Name", "CurrentEnrollment.Program.Name"));

            currentEnrollmentNode.Nodes.Add (CreateTreeNode ("Program Member Id", "CurrentEnrollment.ProgramMemberId"));

            currentEnrollmentNode.Nodes.Add (CreateTreeNode ("Effective Date", "CurrentEnrollment.EffectiveDate"));

            currentEnrollmentNode.Nodes.Add (CreateTreeNode ("Termination Date", "CurrentEnrollment.TerminationDate"));
            
            #endregion 

            
            #region Current Enrollment Coverage

            Telerik.Web.UI.RadTreeNode currentEnrollmentCoverageNode = CreateTreeNode ("Current Enrollment Coverage", "CurrentEnrollmentCoverage");

            currentEnrollmentCoverageNode.Checkable = false;

            DataExplorerTreeView.Nodes.Add (currentEnrollmentCoverageNode);


            currentEnrollmentCoverageNode.Nodes.Add (CreateTreeNode ("Benefit Plan Id", "CurrentEnrollmentCoverage.BenefitPlanId"));

            currentEnrollmentCoverageNode.Nodes.Add (CreateTreeNode ("Benefit Plan Name", "CurrentEnrollmentCoverage.BenefitPlanName"));

            currentEnrollmentCoverageNode.Nodes.Add (CreateTreeNode ("Coverage Type", "CurrentEnrollmentCoverage.CoverageTypeName"));

            currentEnrollmentCoverageNode.Nodes.Add (CreateTreeNode ("Coverage Level", "CurrentEnrollmentCoverage.CoverageLevelName"));

            currentEnrollmentCoverageNode.Nodes.Add (CreateTreeNode ("Rate Code", "CurrentEnrollmentCoverage.Insurer"));

            currentEnrollmentCoverageNode.Nodes.Add (CreateTreeNode ("Effective Date", "CurrentEnrollmentCoverage.EffectiveDate"));

            currentEnrollmentCoverageNode.Nodes.Add (CreateTreeNode ("Termination Date", "CurrentEnrollmentCoverage.TerminationDate"));

            #endregion 

            
            
            #region Current Enrollment Pcp

            Telerik.Web.UI.RadTreeNode currentEnrollmentPcpNode = CreateTreeNode ("Current Enrollment PCP", "CurrentEnrollmentPcp");

            currentEnrollmentPcpNode.Checkable = false;

            DataExplorerTreeView.Nodes.Add (currentEnrollmentPcpNode);


            #region Current Enrollment Pcp - Pcp Provider 
            
            Telerik.Web.UI.RadTreeNode currentEnrollmentPcpProviderNode = CreateTreeNode ("PCP Provider", "CurrentEnrollmentPcpProvider");

            currentEnrollmentPcpProviderNode.Checkable = false;

            currentEnrollmentPcpNode.Nodes.Add (currentEnrollmentPcpProviderNode);

            currentEnrollmentPcpProviderNode.Nodes.Add (CreateTreeNode ("PCP Provider Name", "CurrentEnrollmentPcp.PcpProvider.Name"));


            #region Entity.CurrentMailingAddress

            Telerik.Web.UI.RadTreeNode pcpProviderCurrentMailingAddress = CreateTreeNode ("Current Mailing Address", "CurrentEnrollmentPcp.PcpProvider.Entity.CurrentMailingAddress");

            pcpProviderCurrentMailingAddress.Checkable = false;

            currentEnrollmentPcpProviderNode.Nodes.Add (pcpProviderCurrentMailingAddress);


            pcpProviderCurrentMailingAddress.Nodes.Add (CreateTreeNode ("Address Line 1", "CurrentEnrollmentPcp.PcpProvider.Entity.CurrentMailingAddress.Line1"));

            pcpProviderCurrentMailingAddress.Nodes.Add (CreateTreeNode ("Address Line 2", "CurrentEnrollmentPcp.PcpProvider.Entity.CurrentMailingAddress.Line2"));

            pcpProviderCurrentMailingAddress.Nodes.Add (CreateTreeNode ("City", "CurrentEnrollmentPcp.PcpProvider.Entity.CurrentMailingAddress.City"));

            pcpProviderCurrentMailingAddress.Nodes.Add (CreateTreeNode ("State", "CurrentEnrollmentPcp.PcpProvider.Entity.CurrentMailingAddress.State"));

            pcpProviderCurrentMailingAddress.Nodes.Add (CreateTreeNode ("ZIP Code", "CurrentEnrollmentPcp.PcpProvider.Entity.CurrentMailingAddress.ZipCode"));

            pcpProviderCurrentMailingAddress.Nodes.Add (CreateTreeNode ("ZIP Plus 4", "CurrentEnrollmentPcp.PcpProvider.Entity.CurrentMailingAddress.ZipPlus4"));

            pcpProviderCurrentMailingAddress.Nodes.Add (CreateTreeNode ("Address Single Line", "CurrentEnrollmentPcp.PcpProvider.Entity.CurrentMailingAddress.AddressSingleLine"));

            pcpProviderCurrentMailingAddress.Nodes.Add (CreateTreeNode ("City, State ZIP", "CurrentEnrollmentPcp.PcpProvider.Entity.CurrentMailingAddress.CityStateZipCode"));

            pcpProviderCurrentMailingAddress.Nodes.Add (CreateTreeNode ("Longitude", "CurrentEnrollmentPcp.PcpProvider.Entity.CurrentMailingAddress.Longitude"));

            pcpProviderCurrentMailingAddress.Nodes.Add (CreateTreeNode ("Latitude", "CurrentEnrollmentPcp.PcpProvider.Entity.CurrentMailingAddress.Latitude"));

            #endregion 


            #endregion 


            currentEnrollmentPcpNode.Nodes.Add (CreateTreeNode ("PCP Affiliate Provider Name", "CurrentEnrollmentPcp.PcpAffiliateProvider.Name"));

            currentEnrollmentPcpNode.Nodes.Add (CreateTreeNode ("Effective Date", "CurrentEnrollmentPcp.EffectiveDate"));

            currentEnrollmentPcpNode.Nodes.Add (CreateTreeNode ("Termination Date", "CurrentEnrollmentPcp.TerminationDate"));

            #endregion 

            DataExplorerTreeViewState = DataExplorerTreeView.GetXml ();

            return;

        }

        protected Telerik.Web.UI.RadTreeNode CreateTreeNode (String nodeText, String nodeValue, Boolean isChecked = false) {

            Telerik.Web.UI.RadTreeNode treeNode = new Telerik.Web.UI.RadTreeNode ();


            treeNode.Text = nodeText;

            treeNode.Value = nodeValue;

            treeNode.Checked = isChecked;

            treeNode.Checkable = true;


            return treeNode;

        }

        #endregion 

        
        #region Data Explorer Tree View

        protected void DataExplorerTreeView_OnNodeCheck (Object sender, Telerik.Web.UI.RadTreeNodeEventArgs e) {

            DataExplorerNodeResultsGrid.DataSource = null;

            DataExplorerNodeResultsGrid.Rebind ();

            return;

        }

        #endregion 


        #region Data Explorer Node Results Grid

        protected void DataExplorerNodeResultsGrid_OnItemCreated (Object sender, Telerik.Web.UI.GridItemEventArgs e) {

            if (e.Item is Telerik.Web.UI.GridCommandItem) {

                Telerik.Web.UI.GridCommandItem commandItem = (Telerik.Web.UI.GridCommandItem)e.Item;

                LinkButton DataExplorerNodeResultsGrid_Export = (LinkButton)commandItem.FindControl ("DataExplorerNodeResultsGrid_Export");

                if (DataExplorerNodeResultsGrid_Export != null) {

                    if (NodeInstanceId == Guid.Empty) { return; }

                    Client.Core.DataExplorer.DataExplorerNode dataExplorerNode = Explorer.FindNode (NodeInstanceId);

                    if (dataExplorerNode == null) { return; }


                    String scriptCommand = "window.open ('/Application/DataExplorer/Exports/DataExplorerNodeResultExport";

                    switch (dataExplorerNode.ResultDataType) {

                        case Mercury.Server.Application.DataExplorerNodeResultDataType.Member: scriptCommand += "Member.aspx?"; break;

                        default: scriptCommand += ".aspx?"; break;

                    }

                    scriptCommand += "NodeInstanceId=" + NodeInstanceId.ToString () + "&NodeInstanceCount=" + NodeInstanceCount.ToString () + "'";

                    scriptCommand += ", 'DataExplorerNodeResultExportMember_" + NodeInstanceId.ToString ().Replace ("-", "") + "', 'toolbar=0, location=0, directories=0, status=1, menubar=0, scrollbars=1, resizable=1'";

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

            Boolean usesEntityCurrentMailingAddress = false;

            Boolean usesEntityCurrentContactInformation = false;

            Boolean usesMemberCurrentEnrollment = false;

            Boolean usesMemberCurrentEnrollmentCoverage = false;

            Boolean usesMemberCurrentEnrollmentPcp = false;


            if (!e.IsFromDetailTable) {

                // MASTER TABLE VIEW NEEDS DATA SOURCE

                switch (e.RebindReason) {

                    case Telerik.Web.UI.GridRebindReason.InitialLoad:

                    case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                    case Telerik.Web.UI.GridRebindReason.PostBackEvent:

                    case Telerik.Web.UI.GridRebindReason.PostbackViewStateNotPersisted:

                        // UPDATE COUNT 

                        DataExplorerNodeResultsGrid.Columns.Clear ();

                        DataExplorerNodeResultsGrid.Columns.Add (CreateGridBoundColumn ("Id", "Id", false));

                        if (DataExplorerTreeView.CheckedNodes.Count > 0) {

                            foreach (Telerik.Web.UI.RadTreeNode currentCheckedNode in DataExplorerTreeView.CheckedNodes) {

                                DataExplorerNodeResultsGrid.Columns.Add (CreateGridBoundColumn (currentCheckedNode.Value, currentCheckedNode.Text, true));

                                usesEntityCurrentMailingAddress |= currentCheckedNode.Value.StartsWith ("Entity.CurrentMailingAddress.");

                                usesEntityCurrentContactInformation |= currentCheckedNode.Value.StartsWith ("Entity.CurrentContactInformation");

                                usesMemberCurrentEnrollment |= currentCheckedNode.Value.StartsWith ("CurrentEnrollment.");

                                usesMemberCurrentEnrollmentCoverage |= currentCheckedNode.Value.StartsWith ("CurrentEnrollmentCoverage");

                                usesMemberCurrentEnrollmentPcp |= currentCheckedNode.Value.StartsWith ("CurrentEnrollmentPcp");

                            }

                        }

                        else { DataExplorerNodeResultsGrid.Columns.Add (CreateGridBoundColumn ("Id", "Id", true)); }

                                                
                        DataExplorerNodeResultsGrid.VirtualItemCount = NodeInstanceCount;

                        Int32 initialRow = (DataExplorerNodeResultsGrid.CurrentPageIndex * DataExplorerNodeResultsGrid.PageSize) + 1;

                        List<Client.Core.Member.Member> members = MercuryApplication.DataExplorerNodeResultsGetForMember (NodeInstanceId, initialRow, DataExplorerNodeResultsGrid.PageSize);

                        if (usesEntityCurrentMailingAddress) { MercuryApplication.DataExplorerNodeResultsGetForMemberEntityCurrentAddress (NodeInstanceId, initialRow, DataExplorerNodeResultsGrid.PageSize); }

                        if (usesEntityCurrentContactInformation) { MercuryApplication.DataExplorerNodeResultsGetForMemberEntityCurrentContactInformation (NodeInstanceId, initialRow, DataExplorerNodeResultsGrid.PageSize); }

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

            if (NodeInstanceId == Guid.Empty) { return; }

            DataExplorerNodeResultsGrid_OnNeedDataSource_Member (sender, e); 

            return;

        }

        protected void DataExplorerNodeResultsGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs e) {

            DataExplorerNodeResultsGrid.Rebind ();

            switch (e.CommandName) {

                case "CustomExportToExcel": DataExplorerNodeResultsGrid_ExportToExcel (); break;

            }

            return;

        }

        private System.Xml.XmlAttribute XmlDocument_CreateAttribute (System.Xml.XmlDocument document, String prefix, String localName, String namespaceURI, String content) {

            System.Xml.XmlAttribute attribute = document.CreateAttribute (prefix, localName, namespaceURI);

            attribute.InnerText = content;

            return attribute;

        }

        private void DataExplorerNodeResultsGrid_ExportToExcel () {

            if (DataExplorerTreeView.CheckedNodes.Count == 0) { return; }


            Boolean usesEntityCurrentMailingAddress = false;

            Boolean usesEntityCurrentContactInformation = false;

            Boolean usesMemberCurrentEnrollment = false;

            Boolean usesMemberCurrentEnrollmentCoverage = false;

            Boolean usesMemberCurrentEnrollmentPcp = false;


            System.Xml.XmlElement styleElement;

            System.Xml.XmlElement fontElement;


            System.Xml.XmlElement column;

            System.Xml.XmlElement row;

            System.Xml.XmlElement cell;

            System.Xml.XmlElement cellData;

            Int32 columnIndex = 0;


            System.Xml.XmlDocument excelDocument = new System.Xml.XmlDocument ();

            System.Xml.XmlDeclaration xmlDeclaration = excelDocument.CreateXmlDeclaration ("1.0", "utf-8", String.Empty);

            excelDocument.InsertBefore (xmlDeclaration, excelDocument.DocumentElement);

            excelDocument.AppendChild (excelDocument.CreateProcessingInstruction ("mso-application", "progid=\"Excel.Sheet\""));


            #region Create Workbook and Worksheet and Styles

            System.Xml.XmlElement workbookElement = excelDocument.CreateElement ("Workbook");

            workbookElement.SetAttribute ("xmlns", "urn:schemas-microsoft-com:office:spreadsheet");

            workbookElement.SetAttribute ("xmlns:o", "urn:schemas-microsoft-com:office:office");

            workbookElement.SetAttribute ("xmlns:ss", "urn:schemas-microsoft-com:office:spreadsheet");

            workbookElement.SetAttribute ("xmlns:x", "urn:schemas-microsoft-com:office:excel");

            excelDocument.AppendChild (workbookElement);


            System.Xml.XmlElement stylesCollectionElement = excelDocument.CreateElement ("Styles");

            workbookElement.AppendChild (stylesCollectionElement);

            
            styleElement = excelDocument.CreateElement ("Style");

            styleElement.SetAttributeNode (XmlDocument_CreateAttribute (excelDocument, "ss", "ID", "urn:schemas-microsoft-com:office:spreadsheet", "StyleNormal"));

            stylesCollectionElement.AppendChild (styleElement);

            fontElement = excelDocument.CreateElement ("Font");

            fontElement.SetAttributeNode (XmlDocument_CreateAttribute (excelDocument, "ss", "FontName", "urn:schemas-microsoft-com:office:spreadsheet", "Arial"));

            fontElement.SetAttributeNode (XmlDocument_CreateAttribute (excelDocument, "x", "Family", "urn:schemas-microsoft-com:office:excel", "Swiss"));

            fontElement.SetAttributeNode (XmlDocument_CreateAttribute (excelDocument, "ss", "Size", "urn:schemas-microsoft-com:office:spreadsheet", "8"));

            fontElement.SetAttributeNode (XmlDocument_CreateAttribute (excelDocument, "ss", "Color", "urn:schemas-microsoft-com:office:spreadsheet", "#000000"));

            styleElement.AppendChild (fontElement);



            styleElement = excelDocument.CreateElement ("Style");

            styleElement.SetAttributeNode (XmlDocument_CreateAttribute (excelDocument, "ss", "ID", "urn:schemas-microsoft-com:office:spreadsheet", "StyleBold"));

            stylesCollectionElement.AppendChild (styleElement);

            fontElement = excelDocument.CreateElement ("Font");

            fontElement.SetAttributeNode (XmlDocument_CreateAttribute (excelDocument, "ss", "FontName", "urn:schemas-microsoft-com:office:spreadsheet", "Arial"));

            fontElement.SetAttributeNode (XmlDocument_CreateAttribute (excelDocument, "x", "Family", "urn:schemas-microsoft-com:office:excel", "Swiss"));

            fontElement.SetAttributeNode (XmlDocument_CreateAttribute (excelDocument, "ss", "Size", "urn:schemas-microsoft-com:office:spreadsheet", "8"));

            fontElement.SetAttributeNode (XmlDocument_CreateAttribute (excelDocument, "ss", "Color", "urn:schemas-microsoft-com:office:spreadsheet", "#000000"));

            fontElement.SetAttributeNode (XmlDocument_CreateAttribute (excelDocument, "ss", "Bold", "urn:schemas-microsoft-com:office:spreadsheet", "1"));

            styleElement.AppendChild (fontElement);




            System.Xml.XmlElement worksheetElement = excelDocument.CreateElement ("Worksheet");

            worksheetElement.SetAttributeNode (XmlDocument_CreateAttribute (excelDocument, "ss", "Name", "urn:schemas-microsoft-com:office:spreadsheet", "Data"));

            workbookElement.AppendChild (worksheetElement);


            System.Xml.XmlElement worksheetTableElement = excelDocument.CreateElement ("Table");

            worksheetTableElement.SetAttributeNode (XmlDocument_CreateAttribute (excelDocument, "ss", "StyleID", "urn:schemas-microsoft-com:office:spreadsheet", "StyleNormal"));

            worksheetTableElement.SetAttributeNode (XmlDocument_CreateAttribute (excelDocument, "ss", "DefaultRowHeight", "urn:schemas-microsoft-com:office:spreadsheet", "15"));

            worksheetElement.AppendChild (worksheetTableElement);

            #endregion 


            #region Create Header Row

            System.Xml.XmlElement headerRow = excelDocument.CreateElement ("Row");


            columnIndex = 0;

            foreach (Telerik.Web.UI.RadTreeNode currentCheckedNode in DataExplorerTreeView.CheckedNodes) {

                columnIndex = columnIndex + 1;


                column = excelDocument.CreateElement ("Column");

                column.SetAttributeNode (XmlDocument_CreateAttribute (excelDocument, "ss", "Width", "urn:schemas-microsoft-com:office:spreadsheet", "100"));

                column.SetAttributeNode (XmlDocument_CreateAttribute (excelDocument, "ss", "StyleID", "urn:schemas-microsoft-com:office:spreadsheet", "StyleNormal"));

                worksheetTableElement.AppendChild (column);


                cell = excelDocument.CreateElement ("Cell");

                cell.SetAttributeNode (XmlDocument_CreateAttribute (excelDocument, "ss", "StyleID", "urn:schemas-microsoft-com:office:spreadsheet", "StyleBold"));

                cell.SetAttributeNode (XmlDocument_CreateAttribute (excelDocument, "ss", "Index", "urn:schemas-microsoft-com:office:spreadsheet", columnIndex.ToString ()));

                headerRow.AppendChild (cell);


                cellData = excelDocument.CreateElement ("Data");

                cellData.SetAttributeNode (XmlDocument_CreateAttribute (excelDocument, "ss", "Type", "urn:schemas-microsoft-com:office:spreadsheet", "String"));

                cellData.InnerText = currentCheckedNode.Text;

                cell.AppendChild (cellData);



                usesEntityCurrentMailingAddress |= currentCheckedNode.Value.StartsWith ("Entity.CurrentMailingAddress.");

                usesEntityCurrentContactInformation |= currentCheckedNode.Value.StartsWith ("Entity.CurrentContactInformation");

                usesMemberCurrentEnrollment |= currentCheckedNode.Value.StartsWith ("CurrentEnrollment.");

                usesMemberCurrentEnrollmentCoverage |= currentCheckedNode.Value.StartsWith ("CurrentEnrollmentCoverage");

                usesMemberCurrentEnrollmentPcp |= currentCheckedNode.Value.StartsWith ("CurrentEnrollmentPcp");

            }

            worksheetTableElement.AppendChild (headerRow);


            #endregion


            #region Create Data Rows

            // RETREIVE LIST OF MEMBERS AND PRE-CACHE RELATED DATA

            List<Client.Core.Member.Member> members = MercuryApplication.DataExplorerNodeResultsGetForMember (NodeInstanceId, 1, NodeInstanceCount);

            List<Client.Core.Member.MemberEnrollment> allMemberEnrollments = null;

            List<Client.Core.Member.MemberEnrollmentCoverage> allMemberEnrollmentCoverages = null;

            List<Client.Core.Member.MemberEnrollmentPcp> allMemberEnrollmentPcps = null;

            //  THESE ITEMS ARE AUTOMATICALLY CACHED AND AVAILABLE

            if (usesEntityCurrentMailingAddress) { MercuryApplication.DataExplorerNodeResultsGetForMemberEntityCurrentAddress (NodeInstanceId, 1, NodeInstanceCount); }

            if (usesEntityCurrentContactInformation) { MercuryApplication.DataExplorerNodeResultsGetForMemberEntityCurrentContactInformation (NodeInstanceId, 1, NodeInstanceCount); }

            // THESE ITEMS MUST BE STORED AND USED LOCALLY (COULD BE LARG RESULT SETS!)

            if ((usesMemberCurrentEnrollment) || (usesMemberCurrentEnrollmentCoverage) || (usesMemberCurrentEnrollmentPcp)) {

                // MUST GET CURRENT ENROLLMENTS TO WALK TO CHILD OBJECTS COVERAGE AND PCP

                allMemberEnrollments = MercuryApplication.DataExplorerNodeResultsGetForMemberCurrentEnrollment (NodeInstanceId, 1, NodeInstanceCount);

                if (usesMemberCurrentEnrollmentCoverage) { allMemberEnrollmentCoverages = MercuryApplication.DataExplorerNodeResultsGetForMemberCurrentEnrollmentCoverage (NodeInstanceId, 1, NodeInstanceCount); }

                if (usesMemberCurrentEnrollmentPcp) { allMemberEnrollmentPcps = MercuryApplication.DataExplorerNodeResultsGetForMemberCurrentEnrollmentPcp (NodeInstanceId, 1, NodeInstanceCount); }

            }

            foreach (Client.Core.Member.Member currentMember in members) {

                row = excelDocument.CreateElement ("Row");

                columnIndex = 0;


                #region Precache Data Elements

                // PRECACHE CURRENT MEMBER ENROLLMENT FOR MULTI-PROPERTY ACCESS

                // MUST GET CURRENT ENROLLMENTS TO WALK TO CHILD OBJECTS COVERAGE AND PCP

                Client.Core.Member.MemberEnrollment currentMemberEnrollment = null;

                if ((usesMemberCurrentEnrollment) || (usesMemberCurrentEnrollmentCoverage) || (usesMemberCurrentEnrollmentPcp)) {

                    List<Client.Core.Member.MemberEnrollment> filteredMemberEnrollment =

                        (from memberEnrollment in allMemberEnrollments

                         where memberEnrollment.MemberId == currentMember.Id

                         select memberEnrollment).ToList ();

                    if (filteredMemberEnrollment.Count > 0) { currentMemberEnrollment = filteredMemberEnrollment[0]; }

                }

                // PRECACHE CURRENT MEMBER ENROLLMENT COVERAGE FOR MULTI-PROPERTY ACCESS

                Client.Core.Member.MemberEnrollmentCoverage currentMemberEnrollmentCoverage = null;

                if ((usesMemberCurrentEnrollmentCoverage) && (currentMemberEnrollment != null)) {

                    List<Client.Core.Member.MemberEnrollmentCoverage> filteredMemberEnrollmentCoverage =

                        (from memberEnrollmentCoverage in allMemberEnrollmentCoverages

                         where memberEnrollmentCoverage.MemberEnrollmentId == currentMemberEnrollment.Id

                         select memberEnrollmentCoverage).ToList ();

                    if (filteredMemberEnrollmentCoverage.Count > 0) { currentMemberEnrollmentCoverage = filteredMemberEnrollmentCoverage[0]; }

                }

                Client.Core.Member.MemberEnrollmentPcp currentMemberEnrollmentPcp = null;

                if ((usesMemberCurrentEnrollmentPcp) && (currentMemberEnrollment != null)) {

                    List<Client.Core.Member.MemberEnrollmentPcp> filteredMemberEnrollmentPcp =

                        (from memberEnrollmentPcp in allMemberEnrollmentPcps

                         where memberEnrollmentPcp.MemberEnrollmentId == currentMemberEnrollment.Id

                         select memberEnrollmentPcp).ToList ();

                    if (filteredMemberEnrollmentPcp.Count > 0) { currentMemberEnrollmentPcp = filteredMemberEnrollmentPcp[0]; }

                }

                #endregion 


                #region Create Cells and Data Values

                foreach (Telerik.Web.UI.RadTreeNode currentCheckedNode in DataExplorerTreeView.CheckedNodes) {

                    columnIndex = columnIndex + 1;

                    String contentProperty = String.Empty;

                    Object contentValueObject = null;

                    String contentValue = String.Empty;

                    if (currentCheckedNode.Value.StartsWith ("CurrentEnrollment.")) {

                        if (currentMemberEnrollment != null) {

                            contentProperty = currentCheckedNode.Value.Substring ("CurrentEnrollment.".Length, currentCheckedNode.Value.Length - "CurrentEnrollment.".Length);

                            contentValueObject = Mercury.Server.CommonFunctions.GetPropertyValue (currentMemberEnrollment, contentProperty);

                        }

                    }

                    else if (currentCheckedNode.Value.StartsWith ("CurrentEnrollmentCoverage.")) {

                        if (currentMemberEnrollment != null) {

                            contentProperty = currentCheckedNode.Value.Substring ("CurrentEnrollmentCoverage.".Length, currentCheckedNode.Value.Length - "CurrentEnrollmentCoverage.".Length);

                            contentValueObject = Mercury.Server.CommonFunctions.GetPropertyValue (currentMemberEnrollmentCoverage, contentProperty);

                        }

                    }

                    else if (currentCheckedNode.Value.StartsWith ("CurrentEnrollmentPcp.")) {

                        if (currentMemberEnrollment != null) {

                            contentProperty = currentCheckedNode.Value.Substring ("CurrentEnrollmentPcp.".Length, currentCheckedNode.Value.Length - "CurrentEnrollmentPcp.".Length);

                            contentValueObject = Mercury.Server.CommonFunctions.GetPropertyValue (currentMemberEnrollmentPcp, contentProperty);

                        }

                    }

                    else { contentValueObject = Mercury.Server.CommonFunctions.GetPropertyValue (currentMember, currentCheckedNode.Value); }

                    if (contentValueObject != null) { contentValue = contentValueObject.ToString (); }


                    cell = excelDocument.CreateElement ("Cell");

                    cell.SetAttributeNode (XmlDocument_CreateAttribute (excelDocument, "ss", "Index", "urn:schemas-microsoft-com:office:spreadsheet", columnIndex.ToString ()));

                    row.AppendChild (cell);


                    cellData = excelDocument.CreateElement ("Data");

                    cellData.SetAttributeNode (XmlDocument_CreateAttribute (excelDocument, "ss", "Type", "urn:schemas-microsoft-com:office:spreadsheet", "String"));

                    cellData.InnerText = contentValue;

                    cell.AppendChild (cellData);


                }

                #endregion 


                worksheetTableElement.AppendChild (row);

            }

            #endregion 


            System.Xml.XmlElement worksheetAutoFilterElement = excelDocument.CreateElement ("AutoFilter");

            worksheetAutoFilterElement.SetAttributeNode (XmlDocument_CreateAttribute (excelDocument, "x", "Range", "urn:schemas-microsoft-com:office:excel", "R1C1:R1C1"));

            worksheetAutoFilterElement.SetAttribute ("xmlns", "urn:schemas-microsoft-com:office:excel");

            worksheetElement.AppendChild (worksheetAutoFilterElement);


            Response.Clear ();

            Response.AddHeader ("Content-Disposition", "attachment; filename=DataExplorerResults.xml");

            Response.AddHeader ("Content-Length", excelDocument.OuterXml.Length.ToString ());

            Response.ContentType = "application/octet-stream";

            Response.OutputStream.Write (new System.Text.ASCIIEncoding ().GetBytes (excelDocument.OuterXml.ToCharArray ()), 0, excelDocument.OuterXml.Length);

            Response.End ();



            return;

        }

        protected void DataExplorerNodeResultsGrid_OnGridExporting (Object sender, Telerik.Web.UI.GridExportingArgs e) {

            return;

        }

        #endregion 

    }

}