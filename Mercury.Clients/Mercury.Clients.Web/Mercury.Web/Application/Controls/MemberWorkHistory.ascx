<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberWorkHistory.ascx.cs" Inherits="Mercury.Web.Application.Controls.MemberWorkHistory" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<div style="display: none"><asp:TextBox ID="UserControlInstanceId" Text="" runat="server" /></div>

<Telerik:RadAjaxManagerProxy ID="TelerikAjaxProxy" runat="server">

    <AjaxSettings>
    
        <Telerik:AjaxSetting AjaxControlID="MemberWorkHistoryGrid" ><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="MemberWorkHistoryGrid" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>
        
        <Telerik:AjaxSetting AjaxControlID="WorkQueueItemAddButton" ><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="MemberWorkHistoryGrid" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>
        
    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>


<Telerik:RadGrid ID="MemberWorkHistoryGrid" AllowPaging="true" AllowCustomPaging="true" EnableViewState="false"

    OnItemCreated="MemberWorkHistoryGrid_OnItemCreated" 
    
    OnNeedDataSource="MemberWorkHistoryGrid_OnNeedDataSource" 
    
    OnItemDataBound="MemberWorkHistoryGrid_OnItemDataBound"
    
    OnItemCommand="MemberWorkHistoryGrid_OnItemCommand" 
    
    OnPageSizeChanged="MemberWorkHistoryGrid_OnPageSizeChanged"
    
    AutoGenerateColumns="false" runat="server">

    <MasterTableView Name="MemberWorkHistoryMasterView" TableLayout="Auto" CommandItemDisplay="Top" DataKeyNames="WorkQueueId,WorkQueueItemId">
    
        <CommandItemTemplate>
        
            <div>
                                         
                <Telerik:RadToolBar ID="MemberWorkHistoryToolbar" EnableViewState="false" AutoPostBack="true" runat="server">
                    
                    <Items>
                    
                        <Telerik:RadToolBarButton BorderStyle="None">
                        
                            <ItemTemplate>
                                                                    
                                <table cellpadding="0" cellspacing="0" border="0" style="border: none; padding: 0px"><tr>
                                
                                    <td style="width: 20px"><img src="/Images/Common16/WorkQueueItemAdd.png" alt="Add to Work Queue" /></td>
                                    
                                    <td style="width: 160px;">Add Member to Work Queue:</td>
                                    
                                    <td style="width: 300px"><Telerik:RadComboBox ID="WorkQueueSelection" Width="300" runat="server" /></td>
                                    
                                    <td align="center" style="width:  60px;">Priority:</td>

                                    <td style="width:  60px"><Telerik:RadNumericTextBox ID="WorkQueueItemPriority" MinValue="0" MaxValue="100" Width="40" NumberFormat-DecimalDigits="0" runat="server"></Telerik:RadNumericTextBox>&nbsp</td>

                                    <td><asp:Button ID="WorkQueueItemAddButton" CommandName="WorkQueueItemAdd" Text="Add" Width="73px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>
                                
                                </tr></table>
                            
                            </ItemTemplate>
                        
                        </Telerik:RadToolBarButton>                                             
                        
                    </Items>

                </Telerik:RadToolBar>
   
            </div>
            
        </CommandItemTemplate>
    
        <Columns>
    
            <Telerik:GridBoundColumn DataField="WorkQueueItemStatus" Visible="true" />
                
            <Telerik:GridBoundColumn DataField="WorkQueueId" Visible="false" />

            <Telerik:GridBoundColumn DataField="WorkQueueItemId" Visible="false" />
                
            <Telerik:GridBoundColumn DataField="WorkQueueName" HeaderText="Work Queue" Visible="true" />

            <Telerik:GridBoundColumn DataField="WorkflowName" HeaderText="Workflow Name" />

            <Telerik:GridBoundColumn DataField="WorkflowLastStep" HeaderText="Last Step" Visible="true" />
            
            <Telerik:GridBoundColumn DataField="WorkflowNextStep" HeaderText="Next Step" Visible="true" />
            
            <Telerik:GridBoundColumn DataField="AddedDate" HeaderText="Added" Visible="true" />
            
            <Telerik:GridBoundColumn DataField="LastWorkedDate" HeaderText="Last Worked" Visible="true" />
            
            <Telerik:GridBoundColumn DataField="ConstraintDate" HeaderText="Constraint" Visible="true" />
            
            <Telerik:GridBoundColumn DataField="MilestoneDate" HeaderText="Milestone" Visible="true" />
            
            <Telerik:GridBoundColumn DataField="ThresholdDate" HeaderText="Threshold" Visible="false" />

            <Telerik:GridBoundColumn DataField="DueDate" HeaderText="Due" Visible="true" />
            
            <Telerik:GridBoundColumn DataField="CompletionDate" HeaderText="Completed" Visible="true" />

            <Telerik:GridBoundColumn DataField="Outcome" HeaderText="Outcome" Visible="true" />

            <Telerik:GridBoundColumn DataField="Priority" HeaderText="Priority" ItemStyle-HorizontalAlign="Center" />

            <Telerik:GridBoundColumn DataField="AssignedTo" HeaderText="Assigned To" Visible="true" />

            <Telerik:GridBoundColumn DataField="AssignedToDate" HeaderText="Assigned Date" Visible="true" />

        </Columns>

        <DetailTables>
        
            <Telerik:GridTableView Name="WorkQueueItemSendersGrid" DataKeyNames="WorkQueueItemId" AllowPaging="false" Width="100%">
            
                <ParentTableRelation><Telerik:GridRelationFields MasterKeyField="WorkQueueItemId" DetailKeyField="WorkQueueItemId" /></ParentTableRelation>
                
                <Columns>
                                                   
                    <Telerik:GridBoundColumn DataField="WorkQueueItemId" Visible="false" />

                    <Telerik:GridBoundColumn DataField="WorkQueueItemSenderId" Visible="false" />

                    <Telerik:GridBoundColumn DataField="EventDescription" HeaderText="Description" />

                    <Telerik:GridBoundColumn DataField="Priority" HeaderText="Priority"  ItemStyle-HorizontalAlign="Center" />
                        
                    <Telerik:GridBoundColumn DataField="CreateAccountName" HeaderText="CreateAccountName"/>

                    <Telerik:GridBoundColumn DataField="CreateDate" UniqueName="CreateDate" HeaderText="Date"/>

                </Columns>
                
            </Telerik:GridTableView>
        
        </DetailTables>
        
    </MasterTableView>
    
    <ClientSettings>
    
        <Selecting AllowRowSelect="true" />
        
        <Scrolling AllowScroll="true" />
    
    </ClientSettings>
    
    <PagerStyle NextPageText="Next" PrevPageText="Previous"></PagerStyle>

</Telerik:RadGrid>
