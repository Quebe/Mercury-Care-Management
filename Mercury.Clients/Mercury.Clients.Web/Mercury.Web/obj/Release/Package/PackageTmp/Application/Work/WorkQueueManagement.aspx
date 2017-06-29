<%@ Page Title="" Language="C#" MasterPageFile="~/Application/Application.Master" AutoEventWireup="true" CodeBehind="WorkQueueManagement.aspx.cs" Inherits="Mercury.Web.Application.Work.WorkQueueManagement" %>

<%@ MasterType VirtualPath="~/Application/Application.Master" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ApplicationContentControl" runat="server">

<asp:ScriptManagerProxy ID="AjaxScriptManagerProxy" runat="server">

    <Scripts>
   
    </Scripts>

</asp:ScriptManagerProxy>

<Telerik:RadAjaxManagerProxy ID="AjaxManagerProxy" runat="server">

    <AjaxSettings>

        <Telerik:AjaxSetting AjaxControlID="WorkQueueSelection">
        
            <UpdatedControls>
            
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                <Telerik:AjaxUpdatedControl ControlID="WorkQueueMonitorLink" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                <Telerik:AjaxUpdatedControl ControlID="WorkQueueSetGetWorkLink" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                <Telerik:AjaxUpdatedControl ControlID="WorkQueueViewSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemsAvailableCount" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemsSection" LoadingPanelID="AjaxLoadingPanel" />
            
            
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemAssignWorkQueueSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemAssignUserSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />

            </UpdatedControls>
        
        </Telerik:AjaxSetting>
        
        <Telerik:AjaxSetting AjaxControlID="WorkQueueViewSelection">
        
            <UpdatedControls>
            
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueViewSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemsAvailableCount" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemsSection" LoadingPanelID="AjaxLoadingPanel" />
            
            </UpdatedControls>
        
        </Telerik:AjaxSetting>
        
        <Telerik:AjaxSetting AjaxControlID="WorkQueueItemsGrid">
        
            <UpdatedControls>
            
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemsAvailableCount" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemsSection" LoadingPanelID="AjaxLoadingPanel" />
                
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemsAvailableCount" LoadingPanelID="AjaxLoadingPanelWhiteout" />

            </UpdatedControls>
        
        </Telerik:AjaxSetting>
        
        <Telerik:AjaxSetting AjaxControlID="BasicFiltersTreeView">
        
            <UpdatedControls>
            
                <Telerik:AjaxUpdatedControl ControlID="BasicFiltersTreeView" LoadingPanelID="AjaxLoadingPanel" />
                
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemsAvailableCount" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemsSection" LoadingPanelID="AjaxLoadingPanel" />
                
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemsAvailableCount" LoadingPanelID="AjaxLoadingPanelWhiteout" />

            </UpdatedControls>
        
        </Telerik:AjaxSetting>

        
        <Telerik:AjaxSetting AjaxControlID="WorkQueueItemsGridRefresh">
        
            <UpdatedControls>
            
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemsGridRefresh" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemsAvailableCount" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemsSection" LoadingPanelID="AjaxLoadingPanel" />
                
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemsAvailableCount" LoadingPanelID="AjaxLoadingPanelWhiteout" />

            </UpdatedControls>
        
        </Telerik:AjaxSetting>

        
        <Telerik:AjaxSetting AjaxControlID="WorkQueueItemAssignWorkQueueSelection">
        
            <UpdatedControls>
                        
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemAssignWorkQueueSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemAssignUserSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemAssignWindow_ButtonOk" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemAssignWindow_ButtonCancel" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemsAvailableCount" LoadingPanelID="AjaxLoadingPanelWhiteout" />

            </UpdatedControls>
        
        </Telerik:AjaxSetting>
        
        <Telerik:AjaxSetting AjaxControlID="WorkQueueItemAssignWindow_ButtonOk">
        
            <UpdatedControls>
        
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemsAvailableCount" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemsSection" LoadingPanelID="AjaxLoadingPanel" />
                
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemsAvailableCount" LoadingPanelID="AjaxLoadingPanelWhiteout" />

            </UpdatedControls>
        
        </Telerik:AjaxSetting>

    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>


<!-- WORKQUEUE HEADER (BEGIN) -->

<div id="WorkQueueHeaderSection" style="padding: .125in; padding-top: .0625in; padding-bottom: .0625in;" runat="server">

    <!-- WORKQUEUE INFORMATION (BEGIN) -->
    
    <div class="BackgroundColorComplementNormal BorderColorDark" style="background-color: White; padding: .125in">
    
        <!-- WORKQUEUE ACTION (BEGIN) -->

        <div id="Div1" style="display: block;" runat="server">
        
            <table cellpadding="0" cellspacing="0" width="100%">
            
                <tr style="height: 32px;">
                        
                    <td style="width: 20px;"><img src="/Images/Common16/WorkQueue.png" style="padding-right: 8px;" alt="WorkQueue" /></td>
                
                    <td style="width: 120px; font-weight: bold;">Work Queue:</td>
                
                    <td style="text-align: left;"><Telerik:RadComboBox ID="WorkQueueSelection" Width="100%" OnSelectedIndexChanged="WorkQueueSelection_OnSelectedIndexChanged" AutoPostBack="true" runat="server"></Telerik:RadComboBox></td>

                    <td style="">
                    
                        <table width="100%" cellpadding="0" cellspacing="0" border="0"><tr>
                            
                            <td style="text-align: center; padding-left: .125in; padding-right: .125in"><a id="WorkQueueSetGetWorkLink" class="NoDecoration ColorComplementDarker HoverTextBlack" href="../PermissionDenied.aspx" style="white-space: nowrap; display: block;" runat="server">(Set Get Work)</a></td>
                        
                            <td style="text-align: center"><a id="WorkQueueMonitorLink" class="NoDecoration ColorComplementDarker HoverTextBlack" href="../PermissionDenied.aspx" style="white-space: nowrap; display: block;" runat="server">(Monitor)</a></td>
                        
                        </tr></table>
                                        
                    </td>

                </tr>

                <tr style="height: 32px;">
                        
                    <td style="width: 20px;"><img src="/Images/Common16/WorkQueueView.png" style="padding-right: 8px;" alt="WorkQueue" /></td>
                
                    <td style="width: 120px; font-weight: bold;">Work Queue View:</td>
                
                    <td colspan="2" style="text-align: left;"><Telerik:RadComboBox ID="WorkQueueViewSelection" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="WorkQueueViewSelection_OnSelectedIndexChanged" runat="server"></Telerik:RadComboBox></td>

                </tr>
                
                <tr style="height: 32px;">
                        
                    <td style="width: 20px;"><img src="/Images/Common16/TableRow.png" style="padding-right: 8px;" alt="Work Queue Filters" /></td>
                
                    <td style="width: 120px; font-weight: bold;">Work Queue Filters:</td>

                    <td colspan="2">

                        <table><tr>
                        
                            <td>

                                <Telerik:RadComboBox ID="BasicFiltersSelection" Width="100%" EnableViewState="true" EmptyMessage="Select or Modify Filters" runat="server">
                        
                                    <ItemTemplate>
                            
                                        <Telerik:RadTreeView ID="BasicFiltersTreeView" EnableViewState="true" CheckBoxes="true" TriStateCheckBoxes="true" OnNodeCheck="BasicFiltersTreeView_OnNodeCheck" AllowNodeEditing="false" ShowLineImages="false" Skin="Windows7" runat="server">
                                
                                            <Nodes>
                                    
                                                <Telerik:RadTreeNode Value="FilterIsCompleted" Text="Is Completed" Checkable="true" PostBack="true" runat="server">
                                        
                                                    <Nodes>
                                            
                                                        <Telerik:RadTreeNode Value="FilterIsCompletedValue1" Checkable="true" Visible="true" Checked="false" Enabled="false" runat="server" />

                                                        <Telerik:RadTreeNode Value="FilterIsCompletedValue2" Checkable="true" Visible="true" Checked="false" Enabled="false" runat="server" />
                                            
                                                    </Nodes>

                                                </Telerik:RadTreeNode>
                                                                        
                                                <Telerik:RadTreeNode Value="FilterIsAssigned" Text="Is Assigned" Checkable="true" PostBack="true" runat="server">
                                        
                                                    <Nodes>
                                            
                                                        <Telerik:RadTreeNode Value="FilterIsAssignedValue1" Checkable="true" Visible="true" Checked="true" Enabled="false" runat="server" />

                                                        <Telerik:RadTreeNode Value="FilterIsAssignedValue2" Checkable="true" Visible="true" Checked="false" Enabled="false" runat="server" />
                                            
                                                    </Nodes>

                                                </Telerik:RadTreeNode>
                                    
                                                <Telerik:RadTreeNode Value="FilterHasConstraintDatePassed" Text="Constraint Date Has Passed" Checkable="true" PostBack="true" runat="server">
                                        
                                                    <Nodes>
                                            
                                                        <Telerik:RadTreeNode Value="FilterHasConstraintDatePassedValue1" Checkable="true" Visible="true" Checked="true" Enabled="false" runat="server" />

                                                        <Telerik:RadTreeNode Value="FilterHasConstraintDatePassedValue2" Checkable="true" Visible="true" Checked="false" Enabled="false" runat="server" />
                                            
                                                    </Nodes>

                                                </Telerik:RadTreeNode>
                                                
                                                <Telerik:RadTreeNode Value="FilterHasThresholdDatePassed" Text="Threshold Date Has Passed" Checkable="true" PostBack="true" runat="server">
                                        
                                                    <Nodes>
                                            
                                                        <Telerik:RadTreeNode Value="FilterHasThresholdDatePassedValue1" Checkable="true" Visible="true" Checked="true" Enabled="false" runat="server" />

                                                        <Telerik:RadTreeNode Value="FilterHasThresholdDatePassedValue2" Checkable="true" Visible="true" Checked="false" Enabled="false" runat="server" />
                                            
                                                    </Nodes>

                                                </Telerik:RadTreeNode>
                                                
                                                <Telerik:RadTreeNode Value="FilterHasDueDatePassed" Text="Due Date Has Passed" Checkable="true" PostBack="true" runat="server">
                                        
                                                    <Nodes>
                                            
                                                        <Telerik:RadTreeNode Value="FilterHasDueDatePassedValue1" Checkable="true" Visible="true" Checked="true" Enabled="false" runat="server" />

                                                        <Telerik:RadTreeNode Value="FilterHasDueDatePassedValue2" Checkable="true" Visible="true" Checked="false" Enabled="false" runat="server" />
                                            
                                                    </Nodes>

                                                </Telerik:RadTreeNode>
                                                
                                                <Telerik:RadTreeNode Value="FilterWithinWorkTimeRestrictions" Text="Within Work Time Restrictions" Checkable="true" PostBack="true" runat="server">
                                        
                                                    <Nodes>
                                            
                                                        <Telerik:RadTreeNode Value="FilterWithinWorkTimeRestrictionsValue1" Checkable="true" Visible="true" Checked="true" Enabled="false" runat="server" />

                                                        <Telerik:RadTreeNode Value="FilterWithinWorkTimeRestrictionsValue2" Checkable="true" Visible="true" Checked="false" Enabled="false" runat="server" />
                                            
                                                    </Nodes>

                                                </Telerik:RadTreeNode>
                                                
                                                <Telerik:RadTreeNode Value="FilterWorkQueueItemName" Text="Work Queue Item Name" Checkable="true" PostBack="true" runat="server">

                                                    <NodeTemplate>
                                                    
                                                        <table style="min-width: 400px;"><tr>

                                                            <td>Work Queue Item Name</td>
                                                    
                                                            <td style="width: 80px;">

                                                            <Telerik:RadComboBox ID="FilterWorkQueueItemNameOperatorSelection" Width="80" ZIndex="9999" runat="server">
                                                        
                                                                <Items>
                                                            
                                                                    <Telerik:RadComboBoxItem Text="Contains" Value="Contains" />

                                                                    <Telerik:RadComboBoxItem Text="Starts With" Value="StartsWith" Selected="true" />

                                                                    <Telerik:RadComboBoxItem Text="Ends With" Value="EndsWith" />
                                                            
                                                                </Items>
                                                        
                                                            </Telerik:RadComboBox>

                                                            </td>

                                                            <td><Telerik:RadTextBox ID="FilterWorkQueueItemNameValue" MaxLength="60" runat="server" /></td>

                                                        </tr></table>
                                                    
                                                    </NodeTemplate>
                                       
                                                </Telerik:RadTreeNode>
                                                
                                                <Telerik:RadTreeNode Value="FilterAssignedToDisplayName" Text="Assigned To Display Name" Checkable="true" PostBack="true" runat="server">

                                                    <NodeTemplate>

                                                        <table style="min-width: 400px;"><tr>

                                                            <td>Assigned To User Display Name</td>

                                                            <td style="width: 80px;">

                                                                <Telerik:RadComboBox ID="FilterAssignedToDisplayNameOperatorSelection" Width="80" ZIndex="9999" runat="server">
                                                        
                                                                    <Items>
                                                            
                                                                        <Telerik:RadComboBoxItem Text="Contains" Value="Contains" />

                                                                        <Telerik:RadComboBoxItem Text="Starts With" Value="StartsWith" Selected="true" />

                                                                        <Telerik:RadComboBoxItem Text="Ends With" Value="EndsWith" />
                                                            
                                                                    </Items>
                                                        
                                                                </Telerik:RadComboBox>

                                                            </td>

                                                        <td><Telerik:RadTextBox ID="FilterAssignedToDisplayNameValue" MaxLength="60" ZIndex="9999" runat="server" /></td>
                                                    
                                                        </tr></table>

                                                    </NodeTemplate>
                                       
                                                </Telerik:RadTreeNode>

                                            </Nodes>

                                
                                        </Telerik:RadTreeView>
                            
                                    </ItemTemplate>

                                    <Items><Telerik:RadComboBoxItem Text="" /></Items>
                        
                                </Telerik:RadComboBox>
                                        
                            </td>

                            <td style="white-space: nowrap; text-align: right; vertical-align: top;"><asp:LinkButton ID="WorkQueueItemsGridRefresh" class="NoDecoration ColorComplementDarker HoverTextBlack"  Text="(refresh)" OnClick="WorkQueueItemsGridRefresh_OnClick" runat="server" /></td>

                            <td style="width: 100px; white-space: nowrap; text-align: right; vertical-align: top;">Filtered/Total: </td>

                            <td style="width: 100px; padding-left: .125in; text-align: center; vertical-align: top;"><asp:Label ID="WorkQueueItemsAvailableCount" Text="N/A" runat="server"></asp:Label></td>

                        </tr></table>

                    </td>

                </tr>

            </table>

        </div>

        <!-- WORKQUEUE ACTION ( END ) -->
            
        <div id="WorkQueueExceptionMessageRow" style="display: none;" runat="server">
        
            <table cellpadding="0" cellspacing="0" width="100%"><tr style="height: 36px;">
                        
                <td style="width: 20px;"><img src="/Images/Common16/Stop.png" style="padding-right: 8px;" alt="Exception Indicator" /></td>
                
                <td style="width: 125px; font-weight: bold; color: #A60000">Exception Occurred:</td>
                
                <td style="text-align: left;"><asp:Label ID="WorkQueueExceptionMessage" runat="server" /></td>

                <td><a class="NoDecoration HoverTextBlack" href="#" ID="WorkQueueExceptionExit" onclick="javascript:window.location='/Application/Workspace/Workspace.aspx';" runat="server">(exit)</a></td>

            </tr></table>
            
        </div>
           
        <div id="WorkQueueInformationalMessageRow" style="display: none;" runat="server">
        
            <table cellpadding="0" cellspacing="0" width="100%"><tr style="height: 36px;">
                        
                <td style="width: 20px;"><img src="/Images/Common16/Informational.png" style="padding-right: 8px;" alt="Informational Indicator" /></td>
                               
                <td class="ColorDark" style="text-align: left;"><asp:Label ID="WorkQueueInformationalMessage" runat="server" /></td>

            </tr></table>
            
        </div>
            
    </div>

    <!-- WORKQUEUE INFORMATION ( END ) -->
       

</div>

<!-- WORKQUEUE HEADER ( END ) -->

<!-- WORKQUEUEITEMS (BEGIN) -->

<div id="WorkQueueItemsSection" style="padding: .125in; padding-top: .0625in; padding-bottom: .0625in;" runat="server">
        
        <Telerik:RadSplitter ID="WorkQueueItemsGridSplitter" Orientation="Horizontal" Width="100%" runat="server">

            <Telerik:RadPane ID="WorkQueueItemsGridSplitterPane" Scrolling="None" Width="100%" Height="450" runat="server">

        <Telerik:RadGrid ID="WorkQueueItemsGrid" Width="100%" Height="100%" 
                
                AutoGenerateColumns="false" AllowMultiRowSelection="false" AllowSorting="true" 
            
                AllowFilteringByColumn="false" EnableHeaderContextFilterMenu="false" EnableHeaderContextMenu="false"

                AllowPaging="true" AllowCustomPaging="true" 

                OnNeedDataSource="WorkQueueItemsGrid_OnNeedDataSource"

                OnItemCommand="WorkQueueItemsGrid_OnItemCommand"

                OnSortCommand="WorkQueueItemGrid_OnSortCommand"

                runat="server">

            <MasterTableView Name="WorkQueueItems" DataKeyNames="Id" ClientDataKeyNames="Id,Name,WorkQueueName" IsFilterItemExpanded="false" PageSize="10">

                <Columns>
                
                    <Telerik:GridBoundColumn DataField="Id" UniqueName="Id" Visible="false" />
                
                    <Telerik:GridTemplateColumn DataField="StatusText" HeaderText="Status" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40" ItemStyle-HorizontalAlign="Center" Visible="true">
                    
                        <ItemTemplate>
                        
                            <img src="/Images/Common16/Status<%# Eval ("StatusText") %>.png" alt="Work Queue Item Status" />

                            <div><a href="/Application/Work/WorkQueueItemDetail.aspx?WorkQueueItemId=<%# Eval ("Id") %>" title="Work Queue Item Detail" target="_blank">(detail)</a></div>
                        
                        </ItemTemplate>

                    </Telerik:GridTemplateColumn>
                
                    <Telerik:GridBoundColumn DataField="ItemObjectType" UniqueName="ObjectType" HeaderText="Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false" />
                
                    <Telerik:GridTemplateColumn DataField="Name" UniqueName="Name" HeaderText="Name" Visible="true">
                            
                        <ItemTemplate>
                                
                            <%# Mercury.Web.CommonFunctions.CoreObjectHyperLink ((Mercury.Client.Core.Work.WorkQueueItem) Container.DataItem) %>
                                
                        </ItemTemplate>
                            
                    </Telerik:GridTemplateColumn>
                    
                    <Telerik:GridBoundColumn DataField="ItemGroupKey" UniqueName="ItemGroupKey" HeaderText="Group Key" Visible="true" />
                
                    <Telerik:GridBoundColumn DataField="AddedDate" HeaderText="Added" HeaderStyle-Width="70" ItemStyle-Width="70" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="true" />

                    <Telerik:GridBoundColumn DataField="LastWorkedDate" HeaderText="Worked" HeaderStyle-Width="70" ItemStyle-Width="70" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="true" />
                    
                    <Telerik:GridBoundColumn DataField="ConstraintDate" HeaderText="Constraint" HeaderStyle-Width="70" ItemStyle-Width="70" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="true" />

                    <Telerik:GridBoundColumn DataField="MilestoneDate" HeaderText="Milestone" HeaderStyle-Width="70" ItemStyle-Width="70" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="true" />

                    <Telerik:GridBoundColumn DataField="ThresholdDate" HeaderText="Threshold" HeaderStyle-Width="70" ItemStyle-Width="70" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="true" />

                    <Telerik:GridBoundColumn DataField="DueDate" HeaderText="Due" HeaderStyle-Width="70" ItemStyle-Width="70" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="true" />

                    <Telerik:GridBoundColumn DataField="CompletionDate" HeaderText="Completed" HeaderStyle-Width="70" ItemStyle-Width="70" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="true" />
                
                    <Telerik:GridBoundColumn DataField="Priority" HeaderText="Priority" HeaderStyle-Width="50" ItemStyle-Width="50" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                
                    <Telerik:GridBoundColumn DataField="AssignedToUserDisplayName" UniqueName="AssignedTo" HeaderText="Assigned To" Visible="true" />
                                                     
                    <Telerik:GridTemplateColumn HeaderText="Action" AllowFiltering="false" HeaderStyle-Width="75" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="75" ItemStyle-HorizontalAlign="Center" Visible="true">
                    
                        <ItemTemplate>
                    
                            <div><a href="javascript:WorkQueueItem_Assign (<%# Eval ("Id") %>);" title="Change or Remove the Assignment">(assign)</a></div>
                        
                            <div><a href="javascript:WorkQueueItem_Close (<%# Eval ("Id") %>);" title="Close the Work Queue Item">(close)</a></div>                        
                        
                        </ItemTemplate>

                    </Telerik:GridTemplateColumn>
                              
                </Columns>

                <DetailTables>

                    <Telerik:GridTableView Name="MyAssignedWork_WorkQueueItemsSenderGrid" DataKeyNames="Id" AllowPaging="false" AllowFilteringByColumn="false" TableLayout="Auto" BackColor="AliceBlue" Width="100%">

                        <ParentTableRelation><Telerik:GridRelationFields MasterKeyField="Id" DetailKeyField="WorkQueueItemId" /></ParentTableRelation>                    
                    
                        <Columns>
                                                       
                            <Telerik:GridBoundColumn DataField="WorkQueueItemId" UniqueName="WorkQueueItemId" HeaderText="Id" ReadOnly="true" Visible="false" />

                            <Telerik:GridBoundColumn DataField="WorkQueueItemSenderId" UniqueName="WorkQueueItemSenderId" HeaderText="Id" ReadOnly="true" Visible="false" />
                                        
                            <Telerik:GridBoundColumn DataField="EventDescription" UniqueName="EventDescription" HeaderText="Description" ReadOnly="true" Visible="true" />

                            <Telerik:GridBoundColumn DataField="Priority" UniqueName="Priority" HeaderText="Priority" ItemStyle-HorizontalAlign="Center" />
                
                            <Telerik:GridBoundColumn DataField="CreateAccountInfo.ActionDate" UniqueName="CreateDate" HeaderText="Date" ReadOnly="true" Visible="true" />

                        </Columns>                                           

                        <SortExpressions><Telerik:GridSortExpression FieldName="CreateAccountInfo.ActionDate" SortOrder="Descending" /></SortExpressions>

                    </Telerik:GridTableView>
                
                </DetailTables>

            </MasterTableView>
            
            <ClientSettings EnableRowHoverStyle="true">          

                <Selecting AllowRowSelect="true" />

                <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                
            </ClientSettings>            

            <PagerStyle AlwaysVisible="true" />

        </Telerik:RadGrid>
        
            </Telerik:RadPane>

        </Telerik:RadSplitter>

</div>

<!-- WORKQUEUEITEMS ( END ) -->
    
<Telerik:RadWindowManager ID="WorkspaceWindowManager" runat="server">

    <Windows>
            
        <Telerik:RadWindow ID="WorkQueueItemAssignWindow" Behaviors="Close" Modal="true" Width="450" Height="355" VisibleStatusbar="false"  Title="Release or Assign the Work Queue Item" runat="server">

            <ContentTemplate>
                
                <div id="DialogAssignContent">

                    <div style="margin: .125in" >
                    
                        <p class="ColorDark" style="margin-left: .125in; margin-right: .125in; font-weight: bold">Release or Assign the Work Queue Item to a User?</p>
                            
                        <p>This will set the assignment of the Work Queue Item either the selected user or to not assigned and place it back into the Work Queue. 
                        
                            If the Item has been partially worked, it will not reset automatically and will continue from where it was left off at for the next user. 
                    
                        </p>

                        <div style="display: none;"><Telerik:RadTextBox ID="WorkQueueItemAssignId" runat="server"></Telerik:RadTextBox></div>


                        <div class="ColorDark" style="margin-left: .125in; margin-right: .125in; font-weight: bold"><asp:Label id="WorkQueueItemAssignWorkQueueName" runat="server" /></div>

                        <div class="ColorDark" style="margin-left: .125in; margin-right: .125in; font-weight: bold"><asp:Label id="WorkQueueItemAssignName" runat="server" /></div>


                        <div style="overflow: hidden; width: 95%; margin-top: .125in;">

                            <table cellpadding="0" cellspacing="0" border="0"><tr>
                                    
                                <td style="width: 100px; padding-right: .0625in;">Work Queue:</td>
                                    
                                <td><Telerik:RadComboBox ID="WorkQueueItemAssignWorkQueueSelection" OnSelectedIndexChanged="WorkQueueItemAssignWorkQueueSelection_OnSelectedIndexChanged" AutoPostBack="true" Width="98%" runat="server"></Telerik:RadComboBox></td>

                            </tr></table>

                        </div>
                                
                        <div style="overflow: hidden; width: 95%; margin-top: .125in;">

                            <table cellpadding="0" cellspacing="0" border="0"><tr>
                                    
                                <td style="width: 100px; padding-right: .0625in;">Assigned User:</td>
                                    
                                <td><Telerik:RadComboBox ID="WorkQueueItemAssignUserSelection" Width="98%" runat="server"></Telerik:RadComboBox></td>

                            </tr></table>

                        </div>

                        <div class="BackgroundColorComplementNormal" style="margin-top: 5px; margin-bottom: 5px; padding-left: 5px; padding-left: 5px; height: 1px; width: 98%"></div>

                        <div style="height: 20px; padding: 0px 10px 0px 10px;">
   
                            <table cellpadding="0" cellspacing="0" border="0"><tr>

                                <td style="width: 100%;">&nbsp</td>
                                    
                                <td style="width: 80px; padding-right: 10px;"><asp:Button ID="WorkQueueItemAssignWindow_ButtonOk" Text="OK" OnClick="WorkQueueItemAssignWindow_ButtonOk_OnClick" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" runat="Server" /></td>
                
                                <td style="width: 80px; padding-right: 10px;"><asp:Button ID="WorkQueueItemAssignWindow_ButtonCancel" Text="Cancel" OnClientClick="return WorkQueueItemAssignWindow_Close ();" Width="73px" Font-Names="segoe ui, arial" Height="24" Font-Size="11px" runat="server" /></td> 

                            </tr></table>

                        </div>
            
                    </div>

                </div>

            </ContentTemplate>

        </Telerik:RadWindow>
            
        <Telerik:RadWindow ID="WorkQueueItemCloseWindow" Behaviors="Close" Modal="true" Width="500" Height="280" VisibleStatusbar="false"  Title="Close the Work Queue Item" runat="server">
                
            <ContentTemplate>
                
                <div id="DialogCloseContent">

                    <div style="margin: .125in" >

                        <p class="ColorDark" style="margin-left: .125in; margin-right: .125in; font-weight: bold">Close the Work Queue Item?</p>
                            
                        <p>This will close the Work Queue Item and set the Completion Date to today. This Work Queue Item will no longer 

                            be available for working after it is closed. You must select a Work Outcome.
                
                        </p>


                        <div style="display: none;"><Telerik:RadTextBox ID="WorkQueueItemCloseId" runat="server"></Telerik:RadTextBox></div>


                        <div class="ColorDark" style="margin-left: .125in; margin-right: .125in; font-weight: bold"><asp:Label id="WorkQueueItemCloseWorkQueueName" runat="server" /></div>

                        <div class="ColorDark" style="margin-left: .125in; margin-right: .125in; font-weight: bold"><asp:Label id="WorkQueueItemCloseName" runat="server" /></div>


                        <div style="overflow: hidden; width: 98%; margin-top: .125in; margin-bottom: .0625in">

                            <table width="95%" cellpadding="0" cellspacing="0" border="0"><tr>
                                    
                                <td style="width: 100px; padding-right: 10px;">Work Outcome:</td>
                                    
                                <td><Telerik:RadComboBox ID="WorkQueueItemCloseOutcomeSelection" Width="100%" runat="server"></Telerik:RadComboBox></td> 

                            </tr></table>

                        </div>
                                
                        <div class="BackgroundColorComplementNormal" style="margin-top: 5px; margin-bottom: 5px; padding-left: 5px; padding-left: 5px; height: 1px; width: 98%"></div>

                        <div style="height: 20px; padding: 0px 10px 0px 10px;">
   
                            <table cellpadding="0" cellspacing="0" border="0"><tr>

                                <td style="width: 100%;">&nbsp</td>
                                    
                                <td style="width: 80px; padding-right: 10px;"><asp:Button ID="WorkQueueItemCloseWindow_ButtonOk" Text="OK" OnClick="WorkQueueItemCloseWindow_ButtonOk_OnClick" Width="73px" Font-Names="segoe ui, arial" Height="24" Font-Size="11px" runat="Server" /></td>
                                    
                                <td style="width: 80px; padding-right: 10px;"><asp:Button ID="WorkQueueItemCloseWindow_ButtonCancel" Text="Cancel" OnClientClick="return WorkQueueItemCloseWindow_Close ();" Width="73px" Font-Names="segoe ui, arial" Height="24" Font-Size="11px" runat="server" /></td> 

                            </tr></table>

                        </div>
            
                    </div>

                </div>

            </ContentTemplate>
                
        </Telerik:RadWindow>

    </Windows>

</Telerik:RadWindowManager>

<Telerik:RadCodeBlock ID="WindowFunctions" runat="server" >

    <script language="javascript" type="text/javascript">

        function WorkQueueItem_Assign(workQueueItemId) {

            // RETREIVE GRID OBJECT, MASTER TABLE VIEW, AND ROW COLLECTION 

            var workQueueItemsGrid = $find("<%= WorkQueueItemsGrid.ClientID %>");

            var masterTableView = workQueueItemsGrid.get_masterTableView();

            var masterTableViewRows = masterTableView.get_dataItems();

            var workQueueItemName = "";

            var workQueueItemWorkQueueName = "";



            // FOREACH ROW, FIND CLICKED ROW AND SELECT IT (REMOVE SELECTION FROM OTHER ROWS)

            for (currentRow in masterTableViewRows) {

                var currentWorkQueueId = masterTableViewRows[currentRow].getDataKeyValue("Id");

                if (currentWorkQueueId == workQueueItemId) {

                    selectedRow = masterTableViewRows[currentRow];

                    masterTableViewRows[currentRow].set_selected(true);


                    workQueueItemName = masterTableViewRows[currentRow].getDataKeyValue("Name");

                    workQueueItemWorkQueueName = masterTableViewRows[currentRow].getDataKeyValue("WorkQueueName");

                }

                else { masterTableViewRows[currentRow].set_selected(false); }

            }


            // SET DIALOG PROPERTIES

            var workQueueItemAssignId = $find("<%= WorkQueueItemAssignId.ClientID %>");

            workQueueItemAssignId.set_value(workQueueItemId);

            var workQueueItemAssignWorkQueueName = document.getElementById("<%= WorkQueueItemAssignWorkQueueName.ClientID %>");

            workQueueItemAssignWorkQueueName.innerText = workQueueItemWorkQueueName;

            var workQueueItemAssignName = document.getElementById("<%= WorkQueueItemAssignName.ClientID %>");

            workQueueItemAssignName.innerText = workQueueItemName;


            // SHOW DIALOG

            var dialogWindow = $find("<%= WorkQueueItemAssignWindow.ClientID %>");

            dialogWindow.show();

            return;

        }

        function WorkQueueItem_Close(workQueueItemId) {

            // RETREIVE GRID OBJECT, MASTER TABLE VIEW, AND ROW COLLECTION 

            var workQueueItemsGrid = $find("<%= WorkQueueItemsGrid.ClientID %>");

            var masterTableView = workQueueItemsGrid.get_masterTableView();

            var masterTableViewRows = masterTableView.get_dataItems();

            var workQueueItemName = "";

            var workQueueItemWorkQueueName = "";



            // FOREACH ROW, FIND CLICKED ROW AND SELECT IT (REMOVE SELECTION FROM OTHER ROWS)

            for (currentRow in masterTableViewRows) {

                var currentWorkQueueId = masterTableViewRows[currentRow].getDataKeyValue("Id");

                if (currentWorkQueueId == workQueueItemId) {

                    selectedRow = masterTableViewRows[currentRow];

                    masterTableViewRows[currentRow].set_selected(true);


                    workQueueItemName = masterTableViewRows[currentRow].getDataKeyValue("Name");

                    workQueueItemWorkQueueName = masterTableViewRows[currentRow].getDataKeyValue("WorkQueueName");

                }

                else { masterTableViewRows[currentRow].set_selected(false); }

            }


            // SET DIALOG PROPERTIES

            var workQueueItemCloseId = $find("<%= WorkQueueItemCloseId.ClientID %>");

            workQueueItemCloseId.set_value(workQueueItemId);

            var workQueueItemCloseWorkQueueName = document.getElementById("<%= WorkQueueItemCloseWorkQueueName.ClientID %>");

            workQueueItemCloseWorkQueueName.innerText = workQueueItemWorkQueueName;

            var workQueueItemCloseName = document.getElementById("<%= WorkQueueItemCloseName.ClientID %>");

            workQueueItemCloseName.innerText = workQueueItemName;


            // SHOW DIALOG

            var dialogWindow = $find("<%= WorkQueueItemCloseWindow.ClientID %>");

            dialogWindow.show();

            return;

        }

        function WorkQueueItemAssignWindow_Close() {

            var dialogWindow = $find("<%= WorkQueueItemAssignWindow.ClientID %>");

            dialogWindow.close();

            return false;

        }

        function WorkQueueItemCloseWindow_Close() {

            var dialogWindow = $find("<%= WorkQueueItemCloseWindow.ClientID %>");

            dialogWindow.close();

            return false;

        }

    </script>
        
</Telerik:RadCodeBlock>

<Telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

<script type="text/javascript">

    if (window.addEventListener) { window.addEventListener('resize', WorkQueueManagement_Body_OnResize, false); } else { window.attachEvent('onresize', WorkQueueManagement_Body_OnResize); }

    function GetWindowHeight() { return (window.innerHeight) ? window.innerHeight : document.documentElement.clientHeight; }


    var isWorkQueueManagementPainting = false;

    setTimeout('WorkQueueManagement_OnPaint()', 500);


    function WorkQueueManagement_OnPaint(forEvent) {

        if (isWorkQueueManagementPainting) { return; }

        isWorkQueueManagementPainting = true;


        var container = document.getElementById("<%= WorkQueueItemsSection.ClientID %>");

        var splitter = $find("<%= WorkQueueItemsGridSplitter.ClientID %>");

        if ((container == null) || (splitter == null)) {

            isWorkQueueManagementPainting = false;

            setTimeout('WorkQueueManagement_OnPaint ()', 100);

            return;

        }


        var availableHeight = GetWindowHeight() - container.offsetTop;


//        availableHeight = availableHeight - document.getElementById("EntityContactStep1SectionTitle").offsetHeight;

//        availableHeight = availableHeight - document.getElementById("EntityContactStep2SectionTitle").offsetHeight;

//        availableHeight = availableHeight - document.getElementById("EntityContactStep2").offsetHeight;

        availableHeight = availableHeight - (13 * 1); // MARGIN * 2

        if (availableHeight < 100) { availableHeight = 100; }

        container.style.height = availableHeight + "px";

        splitter.set_width("100%");

        splitter.set_height(availableHeight);

        
        isWorkQueueManagementPainting = false;

        return;

    }


    function WorkQueueManagement_Body_OnResize(forEvent) {

        WorkQueueManagement_OnPaint(forEvent);

        return;

    }

</script>

</Telerik:RadScriptBlock>


</asp:Content>
