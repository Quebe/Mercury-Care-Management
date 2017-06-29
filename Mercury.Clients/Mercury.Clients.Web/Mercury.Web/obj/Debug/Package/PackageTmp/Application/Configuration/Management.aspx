<%@ Page Title="" Language="C#" MasterPageFile="~/Application/Application.Master" AutoEventWireup="true" CodeBehind="Management.aspx.cs" Inherits="Mercury.Web.Application.Configuration.Management" %>

<%@ MasterType VirtualPath="~/Application/Application.Master" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="ConfigurationContentControlHeader" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="ConfigurationContentControl" ContentPlaceHolderID="ApplicationContentControl" runat="server">

<asp:ScriptManagerProxy ID="AjaxScriptManagerProxy" runat="server">

    <Scripts>

        <asp:ScriptReference Path="Management.js" />
    
    </Scripts>

</asp:ScriptManagerProxy>

<Telerik:RadAjaxManagerProxy ID="AjaxManagerProxy" runat="server">

    <AjaxSettings>
    
            <Telerik:AjaxSetting AjaxControlID="ConfigurationTree" >
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ConfigurationTree" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ConfigurationGrid" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="ConfigurationGrid" >
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ConfigurationGrid" LoadingPanelID="AjaxLoadingPanel" />
                                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>

    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>


<div id="ConfigurationToolbarStrip" style="">

    <Telerik:RadTabStrip ID="ToolbarTabStrip" MultiPageID="ToolbarMultiPage" SelectedIndex="0" Visible="true" runat="server">
    
        <Tabs>
        
            <Telerik:RadTab Text="General" Visible="true" />
            
            <Telerik:RadTab Text="Work" Visible="true" />

            <Telerik:RadTab Text="Services" Visible="true" />

            <Telerik:RadTab Text="Care Management" Visible="true" />
            
            <Telerik:RadTab Text="Import" Visible="true" />
            
        </Tabs>
    
    </Telerik:RadTabStrip>
    
    <Telerik:RadMultiPage ID="ToolbarMultiPage" SelectedIndex="0" runat="server">
    
        <Telerik:RadPageView ID="PageGeneral" runat="server">
            
            <Telerik:RadToolBar ID="ToolbarGeneral" OnClientButtonClicked="TitleToolbar_OnClientButtonClicked" runat="server">
    
                <Items>
                
                    <Telerik:RadToolBarButton Text="Add <br /> Reporting" Value="AddReportingServer" ImagePosition="AboveText" ImageUrl="/Images/Common32/ReportingServer.png" />

                    <Telerik:RadToolBarButton Text="Add<br />Fax Server" Value="AddFaxServer" ImagePosition="AboveText" ImageUrl="/Images/Common32/FaxServer.png" />
                    
                    <Telerik:RadToolBarButton Text="Add<br />Printer" Value="AddPrinter" ImagePosition="AboveText" ImageUrl="/Images/Common32/Printer.png" />
                    
                    <Telerik:RadToolBarButton IsSeparator="true" />

                    <Telerik:RadToolBarButton Text="Add <br /> Note Type" Value="AddNoteType" ImagePosition="AboveText" ImageUrl="/Images/Common32/Note.png" />
                    
                    <Telerik:RadToolBarButton Text="Add <br /> Contact Regarding" Value="AddContactRegarding" ImagePosition="AboveText" ImageUrl="/Images/Common32/Phone.png" />
                
                    <Telerik:RadToolBarButton Text="Add <br /> Correspondence" Value="AddCorrespondence" ImagePosition="AboveText" ImageUrl="/Images/Common32/Correspondence.png" />
                    
                    <Telerik:RadToolBarButton Text="Form <br /> Designer" Value="FormDesigner" ImagePosition="AboveText" ImageUrl="/Images/Common32/FormDesigner.png" />
                                   
                </Items>
                
            </Telerik:RadToolBar>
        
        </Telerik:RadPageView>
        
        <Telerik:RadPageView ID="PageWork" runat="server">
            
            <Telerik:RadToolBar ID="ToolbarWork" OnClientButtonClicked="TitleToolbar_OnClientButtonClicked" runat="server">
    
                <Items>

                    <Telerik:RadToolBarButton Text="Add <br /> Workflow" Value="AddWorkflow" ImagePosition="AboveText" ImageUrl="/Images/Common32/Workflow.png" />
                    
                    <Telerik:RadToolBarButton Text="Add <br /> Work Team" Value="AddWorkTeam" ImagePosition="AboveText" ImageUrl="/Images/Common32/WorkTeam.png" />

                    <Telerik:RadToolBarButton Text="Add <br /> Work Queue" Value="AddWorkQueue" ImagePosition="AboveText" ImageUrl="/Images/Common32/WorkQueue.png" />
                    
                    <Telerik:RadToolBarButton Text="Add Work <br /> Queue View" Value="AddWorkQueueView" ImagePosition="AboveText" ImageUrl="/Images/Common32/WorkQueueView.png" />
                    
                    <Telerik:RadToolBarButton Text="Add <br /> Work Outcome" Value="AddWorkOutcome" ImagePosition="AboveText" ImageUrl="/Images/Common32/WorkOutcome.png" />

                    <Telerik:RadToolBarButton Text="Add <br /> Routing Rule" Value="AddRoutingRule" ImagePosition="AboveText" ImageUrl="/Images/Common32/RoutingRule.png" />
                         
                </Items>

            </Telerik:RadToolBar>
        
        </Telerik:RadPageView>
        
        <Telerik:RadPageView ID="PageServices" runat="server">
            
            <Telerik:RadToolBar ID="ToolbarServices" OnClientButtonClicked="TitleToolbar_OnClientButtonClicked" runat="server">
    
                <Items>
                
                    <Telerik:RadToolBarButton Text="Add <br />  Singleton" Width="60" Value="AddSingleton" ImagePosition="AboveText" ImageUrl="/Images/Common32/Document2.png" />
                    
                    <Telerik:RadToolBarButton Text="Add <br />  Set" Width="60" Value="AddSet" ImagePosition="AboveText" ImageUrl="/Images/Common32/DocumentCollection.png" />
                    
                    <Telerik:RadToolBarButton Text="Add <br />  Metric" Width="60" Value="AddMetric" ImagePosition="AboveText" ImageUrl="/Images/Common32/Metric.png" />
                    
                    <Telerik:RadToolBarButton Text="Add <br />  Authorized Service" Width="60" Value="AddAuthorizedService" ImagePosition="AboveText" ImageUrl="/Images/Common32/AuthorizedService.png" />
               
                </Items>

            </Telerik:RadToolBar>
        
        </Telerik:RadPageView>       

        <Telerik:RadPageView ID="PageCareManagement" runat="server">
            
            <Telerik:RadToolBar ID="ToolbarCareManagement" OnClientButtonClicked="TitleToolbar_OnClientButtonClicked" runat="server">
    
                <Items>
                
                    <Telerik:RadToolBarButton Text="Add <br /> Condition" Value="AddCondition" ImagePosition="AboveText" ImageUrl="/Images/Common32/Condition.png" />
                    
                    <Telerik:RadToolBarButton Text="Add <br /> Population Type" Value="AddPopulationType" ImagePosition="AboveText" ImageUrl="/Images/Common32/PopulationType.png" />

                    <Telerik:RadToolBarButton Text="Add <br /> Population" Value="AddPopulation" ImagePosition="AboveText" ImageUrl="/Images/Common32/PopulationCareManagement.png" />
                    
                    <Telerik:RadToolBarButton IsSeparator="true" />
                    
                    <Telerik:RadToolBarButton Text="Add Care <br /> Measure Scale" Value="AddCareMeasureScale" ImagePosition="AboveText" ImageUrl="/Images/Common32/CareMeasureScale.png" />
                    
                    <Telerik:RadToolBarButton Text="Add Care <br /> Measure" Value="AddCareMeasure" ImagePosition="AboveText" ImageUrl="/Images/Common32/CareMeasure.png" />
                    
                    <Telerik:RadToolBarButton Text="Add Care <br /> Intervention" Value="AddCareIntervention" ImagePosition="AboveText" ImageUrl="/Images/Common32/CareIntervention.png" />

                    <Telerik:RadToolBarButton Text="Add <br /> Care Level" Value="AddCareLevel" ImagePosition="AboveText" ImageUrl="/Images/Common32/CareLevel.png" />

                    <Telerik:RadToolBarButton Text="Add <br /> Care Plan" Value="AddCarePlan" ImagePosition="AboveText" ImageUrl="/Images/Common32/CarePlan.png" />
                    
                    <Telerik:RadToolBarButton Text="Add <br /> Problem Statement" Value="AddProblemStatement" ImagePosition="AboveText" ImageUrl="/Images/Common32/ProblemStatement.png" />
                    
                    <Telerik:RadToolBarButton Text="Add <br /> Care Outcome" Value="AddCareOutcome" ImagePosition="AboveText" ImageUrl="/Images/Common32/CareOutcome.png" />
                   
                </Items>
                
            </Telerik:RadToolBar>
        
        </Telerik:RadPageView>

        <Telerik:RadPageView ID="PageImport" runat="server">
            
            <Telerik:RadToolBar ID="ToolbarImport" OnClientButtonClicked="TitleToolbar_OnClientButtonClicked" runat="server">
    
                <Items>
                
                    <Telerik:RadToolBarButton Width="60" Text="Import <br /> &nbsp" Value="ConfigurationImport" ImagePosition="AboveText" ImageUrl="/Images/Common32/ServiceSequence.png" />
                    
                    <Telerik:RadToolBarButton Width="60" Text="Import <br /> NCQA " Value="ConfigurationImportNcqaNdc" ImagePosition="AboveText" ImageUrl="/Images/Common32/ImportNcqaNdc.png" />
                   
                </Items>
        
            </Telerik:RadToolBar>
            
        </Telerik:RadPageView>

    </Telerik:RadMultiPage>
        
</div>

<Telerik:RadSplitter ID="SplitterContainer" Orientation="Vertical" Width="100%" BackColor="White" runat="server">
    
    <Telerik:RadPane ID="SplitterPaneTreeView" Width="185px" Scrolling="Both" BackColor="White" runat="server" >
        
        <Telerik:RadTreeView ID="ConfigurationTree" OnNodeClick="ConfigurationTree_OnNodeClick" OnNodeExpand="ConfigurationTree_OnNodeExpand" OnNodeCollapse="ConfigurationTree_OnNodeCollapse" BackColor="White" runat="server">
            
            <Nodes></Nodes>
           
        </Telerik:RadTreeView>
        
    </Telerik:RadPane>
        
    <Telerik:RadSplitBar ID="SplitterBar" runat="server" CollapseMode="Both" />
        
    <Telerik:RadPane ID="SplitterPaneGrid" runat="server" Scrolling="Both" BackColor="White">
    
        <Telerik:RadGrid ID="ConfigurationGrid" 

            AutoGenerateColumns="false" AllowMultiRowSelection="false" AllowSorting="true" 
        
            runat="server">
            
            <MasterTableView runat="server" />
            
            <ClientSettings EnableRowHoverStyle="true">
                
                <Selecting AllowRowSelect="true" />    
                    
                <ClientEvents OnRowContextMenu="ConfigurationGridContextMenu" />              
                
            </ClientSettings>
            
        </Telerik:RadGrid>
        
    </Telerik:RadPane>
    
</Telerik:RadSplitter>


<div id="TelerikWindows" style="display: none">
    
    <Telerik:RadWindowManager ID="TelerikWindowManager" OnClientClose="DialogWindow_OnClose" runat="server">
        
        <Windows>
            
            <Telerik:RadWindow ID="DialogWindow" VisibleOnPageLoad="false" VisibleStatusbar="false" NavigateUrl="~/WindowLoading.aspx" Modal="true" Behavior="Resize,Close,Move" IconUrl="/Images/Common16/Properties.png" runat="server" />
                      
        </Windows>
        
    </Telerik:RadWindowManager>
        
</div>
    
<div id="GridContextMenus" style="display: none">

    <Telerik:RadContextMenu id="ContextMenuConfigurationObject" OnClientItemClicked="ContextMenuConfigurationObject_OnClientItemClicked" runat="server">
        
        <Items>
            
            <Telerik:RadMenuItem Text="Copy" Value="Copy" ImageUrl="/Images/Common16/Copy.png" Visible="true" />

            <Telerik:RadMenuItem Text="Delete" Value="Delete" ImageUrl="/Images/Common16/Delete.png" Visible="false" />

            <Telerik:RadMenuItem Text="Export" Value="Export" ImageUrl="/Images/Common16/DocumentOut.png" />
                        
            <Telerik:RadMenuItem IsSeparator="true" />
            
            <Telerik:RadMenuItem Text="Properties" Value="Properties" ImageUrl="/Images/Common16/Properties.png"  />
                                
        </Items>
        
    </Telerik:RadContextMenu>

    
    <Telerik:RadContextMenu id="ContextMenuForm" OnClientItemClicked="ContextMenuForm_OnClientItemClicked" runat="server">
        
        <Items>
            
            <Telerik:RadMenuItem Text="Delete" Value="Delete" ImageUrl="/Images/Common16/Delete.png" Visible="false" />

            <Telerik:RadMenuItem Text="Export" Value="Export" ImageUrl="/Images/Common16/DocumentOut.png" />
                        
            <Telerik:RadMenuItem IsSeparator="true" />
            
            <Telerik:RadMenuItem Text="Design" Value="Design" ImageUrl="/Images/Common16/FormDesigner.png"  />
                                
        </Items>
        
    </Telerik:RadContextMenu>                
        
</div>


<Telerik:RadCodeBlock ID="RefreshCodeBlock" runat="server">

<script type="text/javascript">

    function ChildWindow_OnClose(forEvent) { // CALLED BY CHILD WINDOW CLOSING

        var configurationTree = $find("<%= ConfigurationTree.ClientID %>");

        if (configurationTree != null) {

            var selectedNode = configurationTree.get_selectedNode();

            if (selectedNode != null) {

                selectedNode.select();

            }

        }

        return;

    }

</script>

</Telerik:RadCodeBlock>


</asp:Content>

