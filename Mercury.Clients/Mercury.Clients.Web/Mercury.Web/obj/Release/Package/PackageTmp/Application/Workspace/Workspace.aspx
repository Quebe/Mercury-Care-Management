<%@ Page Title="" Language="C#" MasterPageFile="~/Application/Application.Master" AutoEventWireup="true" CodeBehind="Workspace.aspx.cs" Inherits="Mercury.Web.Application.Workspace.Workspace" %>

<%@ MasterType VirtualPath="~/Application/Application.Master" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<asp:Content ID="WorkspaceContentControlHead" ContentPlaceHolderID="head" runat="server">

</asp:Content>


<asp:Content ID="WorkspaceContentControl" ContentPlaceHolderID="ApplicationContentControl" runat="server">

<asp:ScriptManagerProxy ID="AjaxScriptManagerProxy" runat="server">

    <Scripts>

        <asp:ScriptReference Path="~/Application/Workspace/Workspace.js" />
    
    </Scripts>

</asp:ScriptManagerProxy>

<Telerik:RadAjaxManagerProxy ID="AjaxManagerProxy" runat="server">

    <AjaxSettings>

        <Telerik:AjaxSetting AjaxControlID="WorkQueueSelection">
        
            <UpdatedControls>
            
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueSelection" LoadingPanelID="AjaxLoadingPanel" />

                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemsAvailableCount" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                <Telerik:AjaxUpdatedControl ControlID="WorkQueueGetWorkButton" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueMonitorLink" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                <Telerik:AjaxUpdatedControl ControlID="WorkQueueManageLink" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                            
                <Telerik:AjaxUpdatedControl ControlID="GetWorkExceptionMessageRow" />
                
                <Telerik:AjaxUpdatedControl ControlID="GetWorkExceptionMessage" />
                
                <Telerik:AjaxUpdatedControl ControlID="GetWorkInformationalMessageRow" />

                <Telerik:AjaxUpdatedControl ControlID="GetWorkInformationalMessage" />


            </UpdatedControls>
        
        </Telerik:AjaxSetting>

        <Telerik:AjaxSetting AjaxControlID="WorkQueueGetWorkButton">
        
            <UpdatedControls>
            
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueGetWorkButton" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemsAvailableCount" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                
                <Telerik:AjaxUpdatedControl ControlID="GetWorkExceptionMessageRow" />
                
                <Telerik:AjaxUpdatedControl ControlID="GetWorkExceptionMessage" />
                
                <Telerik:AjaxUpdatedControl ControlID="GetWorkInformationalMessageRow" />

                <Telerik:AjaxUpdatedControl ControlID="GetWorkInformationalMessage" />

                <Telerik:AjaxUpdatedControl ControlID="MyAssignedWorkGridSplitter" LoadingPanelID="AjaxLoadingPanel" />
                
            </UpdatedControls>
        
        </Telerik:AjaxSetting>

        <Telerik:AjaxSetting AjaxControlID="MyAssignedWork_WorkQueueItemsGrid">
        
            <UpdatedControls>

                <Telerik:AjaxUpdatedControl ControlID="MyAssignedWorkGridSplitter" LoadingPanelID="AjaxLoadingPanel" />
                
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemsAvailableCount" LoadingPanelID="AjaxLoadingPanelWhiteout" />

            </UpdatedControls>

        </Telerik:AjaxSetting>
    
        <Telerik:AjaxSetting AjaxControlID="MyAssignedWorkRefresh">
        
            <UpdatedControls>
            
                <Telerik:AjaxUpdatedControl ControlID="MyAssignedWorkRefresh" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                <Telerik:AjaxUpdatedControl ControlID="MyAssignedWorkGridSplitter" LoadingPanelID="AjaxLoadingPanel" />
                
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemsAvailableCount" LoadingPanelID="AjaxLoadingPanelWhiteout" />

            </UpdatedControls>

        </Telerik:AjaxSetting>
        
        <Telerik:AjaxSetting AjaxControlID="WorkQueueItemSuspendWindow_ButtonOk">
        
            <UpdatedControls>
            
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemSuspendWindow_ButtonOk" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                <Telerik:AjaxUpdatedControl ControlID="MyAssignedWorkGridSplitter" LoadingPanelID="AjaxLoadingPanel" />
                
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemsAvailableCount" LoadingPanelID="AjaxLoadingPanelWhiteout" />

            </UpdatedControls>

        </Telerik:AjaxSetting>
        
        <Telerik:AjaxSetting AjaxControlID="WorkQueueItemCloseWindow_ButtonOk">
        
            <UpdatedControls>
            
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemCloseWindow_ButtonOk" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                <Telerik:AjaxUpdatedControl ControlID="MyAssignedWorkGridSplitter" LoadingPanelID="AjaxLoadingPanel" />
                
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueItemsAvailableCount" LoadingPanelID="AjaxLoadingPanelWhiteout" />

            </UpdatedControls>

        </Telerik:AjaxSetting>

    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>


<div id="WorkspaceContent" style="padding: .125in;">

    <!-- MY WORK QUEUES (BEGIN) -->
    
    <div class="BackgroundColorComplementNormal BorderColorDark" style="background-color: White; padding: .125in">
    
        <table width="100%" cellpadding="0" cellspacing="0">
        
            <tr style="height: 32px;">

                <td style="width: 110px; white-space: nowrap">My Work Queues: </td>
                
                <td >
                
                    <Telerik:RadComboBox ID="WorkQueueSelection" Width="100%" OnSelectedIndexChanged="WorkQueueSelection_OnSelectedIndexChanged" AutoPostBack="true" runat="server"></Telerik:RadComboBox>
                    
                </td>
            
                <td style="width: 100px; white-space: nowrap; text-align: right">Available/Open: </td>

                <td style="width: 100px; padding-left: .125in; text-align: center"><asp:Label ID="WorkQueueItemsAvailableCount" Text="N/A" runat="server"></asp:Label></td>

            </tr>
            
            <tr style="height: 32px;">

                <td style="font-weight: normal">Get Work: </td>
            
                <td style="font-weight: bold"><asp:LinkButton ID="WorkQueueGetWorkButton" OnClick="WorkQueueGetWorkButton_OnClick" runat="server">(Manual)</asp:LinkButton></td>

                <td colspan="1" style="text-align: center"><a id="WorkQueueMonitorLink" class="NoDecoration ColorComplementDarker HoverTextBlack" href="../PermissionDenied.aspx" style="white-space: nowrap; display: none;" runat="server">(Monitor)</a></td>

                <td colspan="1" style="text-align: center"><a id="WorkQueueManageLink" class="NoDecoration ColorComplementDarker HoverTextBlack" href="../PermissionDenied.aspx" style="white-space: nowrap; display: none;" runat="server">(Manage Work Queue)</a></td>

            </tr>

            <tr><td colspan="4">
            
                <div id="GetWorkExceptionMessageRow" style="display: none;" runat="server"><table cellpadding="0" cellspacing="0" width="100%"><tr>
                        
                <td><img src="/Images/Common16/Stop.png" style="padding-right: 8px;" alt="Exception Indicator" /></td>
                
                <td style="color: #A60000">Exception Occurred: <asp:Label ID="GetWorkExceptionMessage" runat="server" /></td>

            </tr></table></div></td></tr>

            <tr><td colspan="4">
            
                <div id="GetWorkInformationalMessageRow" style="display: none;" runat="server"><table cellpadding="0" cellspacing="0" width="100%" style=""><tr>
                        
                <td style="width: 20px;"><img src="/Images/Common16/Informational.png" style="padding-right: 8px;" alt="Informational Indicator" /></td>
                
                <td class="ColorDark"><asp:Label ID="GetWorkInformationalMessage" runat="server" /></td>

            </tr></table></div></td></tr>
            
        </table>

    </div>

    <!-- MY WORK QUEUES ( END ) -->
    
    <div style="height: .125in;">&nbsp</div>
    

    <!-- MY ASSIGNED WORK (BEGIN) -->
    
    <div id="Workspace_MyAssignedWorkContainer" class="BackgroundColorComplementNormal BorderColorDark" style="background-color: White; padding: .125in">
    
        <table id="Workspace_MyAssignedWorkContainer_TitleTable" width="100%" cellpadding="0" cellspacing="0">
        
            <tr>

                <td style="white-space: nowrap;">My Assigned Work </td>

                <td></td>

                <td colspan="2" style="text-align: right">
                
                    <asp:LinkButton ID="MyAssignedWorkRefresh" CssClass="NoDecoration ColorComplementDarker HoverTextBlack" style="white-space: nowrap" OnClick="MyAssignedWorkRefresh_OnClick" runat="server">(refresh)</asp:LinkButton>
                
                </td>

            </tr>

            <tr style="height: 10px;"><td>&nbsp</td></tr>
            
        </table>
        

        <Telerik:RadSplitter ID="MyAssignedWorkGridSplitter" Orientation="Horizontal" Width="100%" runat="server">

            <Telerik:RadPane ID="MyAssignedworkGridSplitterPane" Scrolling="None" Width="100%" Height="450" runat="server">
                
                <Telerik:RadGrid ID="MyAssignedWork_WorkQueueItemsGrid" Width="100%" Height="100%"
        
                        AutoGenerateColumns="false" AllowMultiRowSelection="false" AllowSorting="true" 
            
                        AllowFilteringByColumn="true" EnableHeaderContextFilterMenu="true" EnableHeaderContextMenu="true"

                        AllowPaging="true" AllowCustomPaging="false" 
            
                        OnNeedDataSource="MyAssignedWork_WorkQueueItemsGrid_OnNeedDataSource"

                        OnItemCommand="MyAssignedWork_WorkQueueItemsGrid_OnItemCommand"

                        runat="server">

                    <MasterTableView Name="WorkQueueItems" DataKeyNames="Id" ClientDataKeyNames="Id,Name,WorkQueueName" IsFilterItemExpanded="false" TableLayout="Fixed" PageSize="10">

                        <Columns>
                
                            <Telerik:GridBoundColumn DataField="Id" UniqueName="Id" Visible="false" />
                
                            <Telerik:GridTemplateColumn DataField="StatusText" HeaderText="Status" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40" ItemStyle-HorizontalAlign="Center" Visible="true">
                    
                                <ItemTemplate>
                        
                                    <img src="/Images/Common16/Status<%# Eval ("StatusText") %>.png" alt="Work Queue Item Status" />

                                    <div><a href="/Application/Work/WorkQueueItemDetail.aspx?WorkQueueItemId=<%# Eval ("Id") %>" title="Work Queue Item Detail" target="_blank">(detail)</a></div>
                        
                                </ItemTemplate>

                            </Telerik:GridTemplateColumn>
                
                            <Telerik:GridBoundColumn DataField="WorkQueueName" UniqueName="WorkQueueName" HeaderText="Queue" Visible="true" />
                
                            <Telerik:GridBoundColumn DataField="ItemObjectType" UniqueName="ObjectType" HeaderText="Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false" />
                
                            <Telerik:GridTemplateColumn DataField="Name" UniqueName="Name" HeaderText="Name" Visible="true">
                            
                                <ItemTemplate>
                                
                                    <%# Mercury.Web.CommonFunctions.CoreObjectHyperLink ((Mercury.Client.Core.Work.WorkQueueItem) Container.DataItem) %>
                                
                                </ItemTemplate>
                            
                            </Telerik:GridTemplateColumn>
                    
                            <Telerik:GridBoundColumn DataField="ItemGroupKey" UniqueName="ItemGroupKey" HeaderText="Group Key" Visible="false" />
                
                            <Telerik:GridBoundColumn DataField="WorkflowNextStep" UniqueName="WorkflowNextStep" HeaderText="Next Step" Visible="true" />
                
                            <Telerik:GridBoundColumn DataField="AddedDate" HeaderText="Added" HeaderStyle-Width="70" ItemStyle-Width="70" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="true" />

                            <Telerik:GridBoundColumn DataField="ConstraintDate" HeaderText="Constraint" HeaderStyle-Width="70" ItemStyle-Width="70" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="true" />

                            <Telerik:GridBoundColumn DataField="LastWorkedDate" HeaderText="Worked" HeaderStyle-Width="70" ItemStyle-Width="70" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="true" />
                    
                            <Telerik:GridBoundColumn DataField="DueDate" HeaderText="Due" HeaderStyle-Width="70" ItemStyle-Width="70" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="true" />
                
                            <Telerik:GridBoundColumn DataField="Priority" HeaderText="Priority" HeaderStyle-Width="50" ItemStyle-Width="50" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                
                            <Telerik:GridBoundColumn DataField="AssignedTo" UniqueName="AssignedTo" HeaderText="Assigned To" Visible="false" />

                            <Telerik:GridTemplateColumn DataField="WorkQueue.WorkflowName" HeaderText="Workflow">
                            
                                <ItemTemplate>
                    
                                    <div><a 
                                    
                                        href="<%#
                                    
                                            (((Int64) Eval ("WorkQueue.WorkflowId")) == 0) ? String.Empty : ("/Application/Workflow/Workflow.aspx?") +
                                            
                                                ((((Guid) Eval ("WorkflowInstanceId")) == Guid.Empty) ?
                                                
                                                    "WorkflowId=" + ((Int64) Eval ("WorkQueue.WorkflowId")).ToString () +

                                                    "&WorkQueueItemId=" + ((Int64) Eval ("Id")).ToString () + 

                                                    "&" + ((String) Eval ("ItemObjectType")) + "Id=" + ((Int64) Eval ("ItemObjectId")).ToString ()
                                                
                                                    :

                                                    "WorkflowId=" + ((Int64) Eval ("WorkQueue.WorkflowId")).ToString () +

                                                    "&WorkflowInstanceId=" + ((Guid) Eval ("WorkflowInstanceId")).ToString ()

                                            )
                                                             
                                    
                                        %>" 
                                        
                                        title="<%# 

                                            (((Int64) Eval ("WorkQueue.WorkflowId")) == 0) ? String.Empty : ((String) Eval ("WorkQueue.WorkflowName")) +
                                            
                                                ((((Guid) Eval ("WorkflowInstanceId")) == Guid.Empty) ? " (start)" : " (resume)")
                                                                                                                                                                                        
                                        %>">
                                    
                                        <%# 

                                            (((Int64) Eval ("WorkQueue.WorkflowId")) == 0) ? String.Empty : ((String) Eval ("WorkQueue.WorkflowName")) +
                                            
                                                ((((Guid) Eval ("WorkflowInstanceId")) == Guid.Empty) ? " (start)" : " (resume)")
                                                                                                                                                                                        
                                        %>
                                        
                                    </a></div>
                        
                                </ItemTemplate>
                            
                            </Telerik:GridTemplateColumn>
                                 
                            <Telerik:GridTemplateColumn HeaderText="Action" AllowFiltering="false" HeaderStyle-Width="75" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="75" ItemStyle-HorizontalAlign="Center" Visible="true">
                    
                                <ItemTemplate>
                    
                                    <div><a href="javascript:WorkQueueItem_Suspend (<%# Eval ("Id") %>);" title="Release or Suspend the Item back to the Work Queue">(suspend)</a></div>
                        
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

                </Telerik:RadGrid>

            </Telerik:RadPane>

        </Telerik:RadSplitter>



        <Telerik:RadWindowManager ID="WorkspaceWindowManager" runat="server">

            <Windows>
            
                <Telerik:RadWindow ID="WorkQueueItemSuspendWindow" Behaviors="Close" Modal="true" Width="450" Height="355" VisibleStatusbar="false"  Title="Release or Suspend the Work Queue Item" runat="server">

                    <ContentTemplate>
                
                        <div id="DialogSuspendContent">

                            <div style="margin: .125in" >

                                <p class="ColorDark" style="margin-left: .125in; margin-right: .125in; font-weight: bold">Release or Suspend the Work Queue Item to the Work Queue?</p>
                            
                                <p>This will set the Work Queue Item to not assigned and place it back into the Work Queue. If you have partially worked the Item, 
                
                                    it will not reset automatically and will continue from where you left off (for the next user). 
                    
                                </p>

                                <p>Setting the Suspend Days to "0" will make the item available based on its current Constraint Date, while setting the 
                
                                Suspend Days to a number higher than "0" will reset the Constraint Date to the new date.</p>

                                <div style="display: none;"><Telerik:RadTextBox ID="WorkQueueItemSuspendId" runat="server"></Telerik:RadTextBox></div>


                                <div class="ColorDark" style="margin-left: .125in; margin-right: .125in; font-weight: bold"><asp:Label id="WorkQueueItemSuspendWorkQueueName" runat="server" /></div>

                                <div class="ColorDark" style="margin-left: .125in; margin-right: .125in; font-weight: bold"><asp:Label id="WorkQueueItemSuspendName" runat="server" /></div>


                                <div style="overflow: hidden; width: 50%; margin-top: .125in;">

                                    <table cellpadding="0" cellspacing="0" border="0"><tr>
                                    
                                        <td style="width: 80px; padding-right: 10px;">Suspend Days:</td>
                                    
                                        <td><Telerik:RadNumericTextBox ID="WorkQueueItemSuspendDays" Width="40" MinValue="0" MaxValue="999" NumberFormat-DecimalDigits="0" EmptyMessage="(days)" Value="0" runat="server"></Telerik:RadNumericTextBox></td> 

                                    </tr></table>

                                </div>
                                
                                <div class="BackgroundColorComplementNormal" style="margin-top: 5px; margin-bottom: 5px; padding-left: 5px; padding-left: 5px; height: 1px; width: 98%"></div>

                                <div style="height: 20px; padding: 0px 10px 0px 10px;">
   
                                    <table cellpadding="0" cellspacing="0" border="0"><tr>

                                        <td style="width: 100%;">&nbsp</td>
                                    
                                        <td style="width: 80px; padding-right: 10px;"><asp:Button ID="WorkQueueItemSuspendWindow_ButtonOk" Text="OK" OnClick="WorkQueueItemSuspendWindow_ButtonOk_OnClick" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" runat="Server" /></td>
                
                                        <td style="width: 80px; padding-right: 10px;"><asp:Button ID="WorkQueueItemSuspendWindow_ButtonCancel" Text="Cancel" OnClientClick="return WorkQueueItemSuspendWindow_Close ();" Width="73px" Font-Names="segoe ui, arial" Height="24" Font-Size="11px" runat="server" /></td> 

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

                function WorkQueueItem_Suspend(workQueueItemId) {

                    // RETREIVE GRID OBJECT, MASTER TABLE VIEW, AND ROW COLLECTION 

                    var workQueueItemsGrid = $find("<%= MyAssignedWork_WorkQueueItemsGrid.ClientID %>");

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

                    var workQueueItemSuspendId = $find("<%= WorkQueueItemSuspendId.ClientID %>");

                    workQueueItemSuspendId.set_value(workQueueItemId);

                    var workQueueItemSuspendWorkQueueName = document.getElementById("<%= WorkQueueItemSuspendWorkQueueName.ClientID %>");

                    workQueueItemSuspendWorkQueueName.innerText = workQueueItemWorkQueueName;

                    var workQueueItemSuspendName = document.getElementById("<%= WorkQueueItemSuspendName.ClientID %>");

                    workQueueItemSuspendName.innerText = workQueueItemName;


                    // SHOW DIALOG

                    var dialogWindow = $find("<%= WorkQueueItemSuspendWindow.ClientID %>");

                    dialogWindow.show();

                    return;

                }

                function WorkQueueItem_Close(workQueueItemId) {

                    // RETREIVE GRID OBJECT, MASTER TABLE VIEW, AND ROW COLLECTION 

                    var workQueueItemsGrid = $find("<%= MyAssignedWork_WorkQueueItemsGrid.ClientID %>");

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

                function WorkQueueItemSuspendWindow_Close() {

                    var dialogWindow = $find("<%= WorkQueueItemSuspendWindow.ClientID %>");

                    dialogWindow.close();

                    return false;

                }

                function WorkQueueItemCloseWindow_Close () {
                
                    var dialogWindow = $find("<%= WorkQueueItemCloseWindow.ClientID %>");

                    dialogWindow.close();

                    return false;

                }

            </script>
        
        </Telerik:RadCodeBlock>

    </div>

    <!-- MY ASSIGNED WORK ( END ) -->
    
</div>

</asp:Content>
