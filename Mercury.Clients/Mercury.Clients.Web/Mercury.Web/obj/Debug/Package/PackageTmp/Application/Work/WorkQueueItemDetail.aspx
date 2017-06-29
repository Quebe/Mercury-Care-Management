<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkQueueItemDetail.aspx.cs" Inherits="Mercury.Web.Application.Work.WorkQueueItemDetail" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">

    <title></title>
    
</head>

<body>

<form id="form1" runat="server">

<div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div>    

<div id="AjaxManagerDiv" style="display: none">

    <asp:ScriptManager ID="AjaxScriptManager" runat="Server" />
    
</div>
    
<div id="TitleBar" runat="server">

    <table style="width: 100%; table-layout: fixed; background-color:#6699CC; border-bottom: solid 1px black"><tr>

        <td style="width: 32px"><img id="TitleImage" src="/Images/Common24/WorkQueueItem.png" alt="WorkQueueItem" /></td>    

        <td style="text-align: left; vertical-align: middle; font-family: Calibri, Arial; font-size: 10pt; color: White; font-weight: bold">
        
            <div style="font-family: Calibri, Arial; font-size: 10pt; color: White; font-weight: bold; text-decoration: none;"><asp:Label ID="TitleBarLabel" Style="overflow: hidden" Text="Work Queue Item" runat="server" /></div>
    
        </td>
        
        <td style="width: 25%; overflow: visible; vertical-align: middle">
        

        </td>

    </tr></table>

</div>

<div style="font-family: Arial; font-size: 10pt; line-height: 150%">

    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
    
        <div style="position: relative; float: left; width: 05%; padding: 4px;">Id:</div>
    
        <div style="position: relative; float: left; width: 25%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueItemId" Width="100%" MaxLength="60" ReadOnly="true"  runat="server" /></div>
    
    
        <div style="position: relative; float: left; width: 05%; padding: 4px; text-align: right;">Item:</div>
    
        <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueItemDescription" Width="100%" MaxLength="60" ReadOnly="true" runat="server" /></div>
    
    </div>
    
    
    <div style="clear: both" />
    
    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
        
        <div style="position: relative; float: left; width: 05%; padding: 4px;">Type:</div>
    
        <div style="position: relative; float: left; width: 25%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueItemType" Width="100%" MaxLength="60" ReadOnly="true" runat="server" /></div>
    
        <div style="position: relative; float: left; width: 05%; padding: 4px; text-align: right;">Group:</div>
    
        <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueItemGroupKey" Width="100%" MaxLength="60" ReadOnly="true" runat="server" /></div>
    
    </div>
    
    
    <div style="clear: both" />
    
    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
        
        <div style="position: relative; float: left; width: 10%; padding: 4px; white-space: nowrap; padding-right: 8px;">Work Queue:</div>
    
        <div style="position: relative; float: left; width: 50%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueItemWorkQueueName" Width="100%" MaxLength="60" ReadOnly="true" runat="server" /></div>
    
        <div style="position: relative; float: left; width: 10%; padding: 4px; text-align: right;">Added:</div>
    
        <div style="position: relative; float: left; width: 10%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueItemAddedDate" Width="100%" MaxLength="60" ReadOnly="true" runat="server" /></div>
    
    </div>
    
    <div style="clear: both" />
    
    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
    
        <div style="position: relative; float: left; width: 10%; padding: 4px;">Worked:</div>
    
        <div style="position: relative; float: left; width: 10%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueItemLastWorked" Width="100%" MaxLength="60" ReadOnly="true" runat="server" /></div>
        
        <div style="position: relative; float: left; width: 10%; padding: 4px; text-align: right;">Constraint:</div>
    
        <div style="position: relative; float: left; width: 10%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueItemConstraintDate" Width="100%" MaxLength="60" ReadOnly="true" runat="server" /></div>
    
        <div style="position: relative; float: left; width: 10%; padding: 4px; text-align: right;">Milestone:</div>
    
        <div style="position: relative; float: left; width: 10%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueItemMilestoneDate" Width="100%" MaxLength="60" ReadOnly="true" runat="server" /></div>
    
        <div style="position: relative; float: left; width: 10%; padding: 4px; text-align: right;">Threshold:</div>
    
        <div style="position: relative; float: left; width: 10%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueItemThresholdDate" Width="100%" MaxLength="60" ReadOnly="true" runat="server" /></div>
    
    </div>
    
    <div style="clear: both" />
    
    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
    
        <div style="position: relative; float: left; width: 10%; padding: 4px;">Due Date:</div>
    
        <div style="position: relative; float: left; width: 10%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueItemDueDate" Width="100%" MaxLength="60" ReadOnly="true" runat="server" /></div>
    
        <div style="position: relative; float: left; width: 10%; padding: 4px; text-align: right;">Completion:</div>
    
        <div style="position: relative; float: left; width: 10%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueItemCompletion" Width="100%" MaxLength="60" ReadOnly="true" runat="server" /></div>
    
        <div style="position: relative; float: left; width: 10%; padding: 4px; text-align: right; ">Outcome:</div>
    
        <div style="position: relative; float: left; width: 40%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueItemOutcome" Width="100%" MaxLength="60" ReadOnly="true" runat="server" /></div>
    
    </div>
    
    <div style="clear: both" />
    
    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
    
        <div style="position: relative; float: left; width: 10%; padding: 4px; white-space: nowrap; padding-right: 8px;">Workflow:</div>
    
        <div style="position: relative; float: left; width: 50%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueItemWorkflowName" Width="100%" MaxLength="60" ReadOnly="true" runat="server" /></div>
    
        <div style="position: relative; float: left; width: 10%; padding: 4px; white-space: nowrap; padding-right: 8px; text-align: right;">Instance:</div>
    
        <div style="position: relative; float: left; width: 20%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueItemWorkflowInstanceId" Width="100%" MaxLength="60" ReadOnly="true" runat="server" /></div>
    
    </div>
    
    <div style="clear: both" />
    
    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
    
        <div style="position: relative; float: left; width: 10%; padding: 4px;">Last Step:</div>
    
        <div style="position: relative; float: left; width: 30%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueItemWorkflowLastStep" Width="100%" MaxLength="60" ReadOnly="true" runat="server" /></div>
    
        <div style="position: relative; float: left; width: 10%; padding: 4px; text-align: right;">Next Step:</div>
    
        <div style="position: relative; float: left; width: 30%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueItemWorkflowNextStep" Width="100%" MaxLength="60" ReadOnly="true" runat="server" /></div>
        
    </div>
    
    <div style="clear: both" />
        

    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px"><b>Work Time Restrictions</b></div>
 
    <div style="padding: 4px; border: solid 1px #bbd7fa; margin: .125in; font-size: 8pt">
    
        <asp:Repeater ID="WorkTimeRestrictionsRepeater" runat="server">
        
            <HeaderTemplate>
                    
                <table cellpadding="0" cellspacing="0" style="width: 100%; padding: 2px; line-height: 150%; vertical-align: middle;">
                    
                    <tr style="width: 100%; font-weight: bold; line-height: 150%; vertical-align: middle;">
                    
                        <td style="width: 34%;">Day Of Week</td>
                    
                        <td style="width: 33%;">Start Time</td>
                        
                        <td style="width: 33%;">End Time</td>
                                           
                    </tr>
                    
                     <tr><td colspan="8"><div style="height: 1px; padding-top: 2px; padding-bottom: 2px; border-bottom: solid 1px #EEEEEE;"></div></td></tr>
                
            </HeaderTemplate>
            
            
            <ItemTemplate>
            
                <tr>
                
                    <td><%# DataBinder.Eval(Container.DataItem, "DayOfWeek") %></td>                
                
                    <td><%# DataBinder.Eval(Container.DataItem, "StartTime") %></td>
                
                    <td><%# DataBinder.Eval(Container.DataItem, "EndTime") %></td>

                </tr>

            </ItemTemplate>
            
            <SeparatorTemplate>
            
                <tr><td colspan="8"><div style="height: 1px; padding-top: 2px; padding-bottom: 2px; border-bottom: solid 1px #EEEEEE;"></div></td></tr>
            
            </SeparatorTemplate>
            
            <FooterTemplate>
                    
                </table>
                    
            </FooterTemplate>
        
        </asp:Repeater>
    
    </div>
    
    

    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px"><b>Created, Modified, and Current Assignment</b></div>

    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

        <div style="position: relative; float: left; width: 33%">
        
            <div style="width: 90%; padding: 4px; text-align: center">Created Information</div>
        

            <div style="overflow: hidden;">

                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
            
                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueItemCreateAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

            </div>

            <div style="overflow: hidden;">

                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
            
                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueItemCreateAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

            </div>

            <div style="overflow: hidden;">

                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
            
                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueItemCreateAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
            
            </div>

            <div style="overflow: hidden;">

                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                
                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="WorkQueueItemCreateDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
            
            </div>

        </div>

        <div style="position: relative; float: left; width: 33%">

            <div style="width: 90%; padding: 4px; text-align: center">Modified Information</div>


            <div style="overflow: hidden;">

                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
        
                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueItemModifiedAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                
            </div> 
            
            
            <div style="overflow: hidden;">

                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
        
                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueItemModifiedAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                
            </div>


            <div style="overflow: hidden;">

                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
        
                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueItemModifiedAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                
            </div>
            

            <div style="overflow: hidden;">

                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
            
                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="WorkQueueItemModifiedDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                
            </div>
            
          </div>

        <div style="position: relative; float: left; width: 33%">

            <div style="width: 90%; padding: 4px; text-align: center">Assigned To Information</div>


            <div style="overflow: hidden;">

                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
        
                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueItemAssignedToAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                
            </div> 
            
            
            <div style="overflow: hidden;">

                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
        
                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueItemAssignedToAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                
            </div>


            <div style="overflow: hidden;">

                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
        
                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueItemAssignedToAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                
            </div>
            

            <div style="overflow: hidden;">

                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
            
                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="WorkQueueItemAssignedToDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                
            </div>
            
          </div>
          
    <div>

    </div>

    </div>


    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px"><b>Extended Properties</b></div>
 
    <div style="padding: 4px; border: solid 1px #bbd7fa; margin: .125in; font-size: 8pt">
    
        <asp:Repeater ID="ExtendedPropertiesRepeater" runat="server">
        
            <HeaderTemplate>
                    
                <table cellpadding="0" cellspacing="0" style="width: 100%; padding: 2px; line-height: 150%; vertical-align: middle;">
                    
                    <tr style="width: 100%; font-weight: bold; line-height: 150%; vertical-align: middle;">
                    
                        <td style="width: 50%;">Name</td>
                        
                        <td style="width: 50%;">Value</td>
                                           
                    </tr>
                    
                     <tr><td colspan="8"><div style="height: 1px; padding-top: 2px; padding-bottom: 2px; border-bottom: solid 1px #EEEEEE;"></div></td></tr>
                
            </HeaderTemplate>
            
            
            <ItemTemplate>
            
                <tr>
                
                    <td><%# DataBinder.Eval(Container.DataItem, "PropertyName") %></td>
                
                    <td><%# DataBinder.Eval(Container.DataItem, "PropertyValue") %></td>

                </tr>

            </ItemTemplate>
            
            <SeparatorTemplate>
            
                <tr><td colspan="8"><div style="height: 1px; padding-top: 2px; padding-bottom: 2px; border-bottom: solid 1px #EEEEEE;"></div></td></tr>
            
            </SeparatorTemplate>
            
            <FooterTemplate>
                    
                </table>
                    
            </FooterTemplate>
        
        </asp:Repeater>
    
    </div>
    
    
    
    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px"><b>Senders</b></div>
 
    <div style="padding: 4px; border: solid 1px #bbd7fa; margin: .125in; font-size: 8pt">
    
        <asp:Repeater ID="SendersRepeater" runat="server">
        
            <HeaderTemplate>
                    
                <table cellpadding="0" cellspacing="0" style="width: 100%; padding: 2px; line-height: 150%; vertical-align: middle;">
                    
                    <tr style="width: 100%; font-weight: bold; line-height: 150%; vertical-align: middle;">
                    
                        <td style="width: 20%;">Sender Object Type</td>

                        <td style="width: 20%;">Event Object Type</td>
                        
                        <td style="width: 25%;">Event Description</td>
                                           
                        <td style="width: 20%;">Create Account Name</td>

                        <td style="width: 10%;">Create Date</td>
                        
                    </tr>
                    
                     <tr><td colspan="8"><div style="height: 1px; padding-top: 2px; padding-bottom: 2px; border-bottom: solid 1px #EEEEEE;"></div></td></tr>
                
            </HeaderTemplate>
            
            
            <ItemTemplate>
            
                <tr>
                
                    <td><%# DataBinder.Eval(Container.DataItem, "SenderObjectType") %></td>
                
                    <td><%# DataBinder.Eval(Container.DataItem, "EventObjectType") %></td>

                    <td><%# DataBinder.Eval(Container.DataItem, "EventDescription") %></td>
                
                    <td><%# DataBinder.Eval(Container.DataItem, "CreateAccountName") %></td>

                    <td><%# DataBinder.Eval(Container.DataItem, "CreateDate") %></td>                               

                </tr>

            </ItemTemplate>
            
            <SeparatorTemplate>
            
                <tr><td colspan="8"><div style="height: 1px; padding-top: 2px; padding-bottom: 2px; border-bottom: solid 1px #EEEEEE;"></div></td></tr>
            
            </SeparatorTemplate>
            
            <FooterTemplate>
                    
                </table>
                    
            </FooterTemplate>
        
        </asp:Repeater>
    
    </div>
    
    
    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px"><b>Assignment History</b></div>
 
    <div style="padding: 4px; border: solid 1px #bbd7fa; margin: .125in; font-size: 8pt">
    
        <asp:Repeater ID="AssignmentHistoryRepeater" runat="server">
        
            <HeaderTemplate>
                    
                <table cellpadding="0" cellspacing="0" style="width: 100%; padding: 2px; line-height: 150%; vertical-align: middle;">
                    
                    <tr style="width: 100%; font-weight: bold; line-height: 150%; vertical-align: middle;">
                    
                        <td style="width: 15%;">Work Queue</td>

                        <td style="width: 25%;">Assigned To</td>
                        
                        <td style="width: 15%;">Assigned Date</td>
                                           
                        <td style="width: 25%;">Source</td>

                        <td style="width: 20%;">Assigned By</td>
                        
                    </tr>
                    
                     <tr><td colspan="8"><div style="height: 1px; padding-top: 2px; padding-bottom: 2px; border-bottom: solid 1px #EEEEEE;"></div></td></tr>
                
            </HeaderTemplate>
            
            
            <ItemTemplate>
            
                <tr>
                
                    <td><%# DataBinder.Eval(Container.DataItem, "AssignedToWorkQueue") %></td>
                
                    <td><%# DataBinder.Eval(Container.DataItem, "AssignedTo") %></td>

                    <td><%# DataBinder.Eval(Container.DataItem, "AssignedDate") %></td>
                
                    <td><%# DataBinder.Eval(Container.DataItem, "AssignmentSource") %></td>

                    <td><%# DataBinder.Eval(Container.DataItem, "AssignedBy") %></td>                               

                </tr>

            </ItemTemplate>
            
            <SeparatorTemplate>
            
                <tr><td colspan="8"><div style="height: 1px; padding-top: 2px; padding-bottom: 2px; border-bottom: solid 1px #EEEEEE;"></div></td></tr>
            
            </SeparatorTemplate>
            
            <FooterTemplate>
                    
                </table>
                    
            </FooterTemplate>
        
        </asp:Repeater>
    
    </div>
    
    
    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px"><b>Workflow Steps</b></div>
 
    <div style="padding: 4px; border: solid 1px #bbd7fa; margin: .125in; font-size: 8pt">

        <asp:Repeater ID="WorkflowStepsRepeater" runat="server">
        
            <HeaderTemplate>
                    
                <table cellpadding="0" cellspacing="0" style="width: 100%; padding: 2px; line-height: 150%; vertical-align: middle;font-family: segoe ui, arial; font-size: 8pt;">
                    
                    <tr style="width: 100%; font-weight: bold; line-height: 150%; vertical-align: middle;">
                    
                        <td style="width: 20px;"></td>

                        <td style="width: 15%; text-align: center; white-space: nowrap">Date</td>
                                
                        <td style="width: 25%;">Name</td>
                          
                        <td style="width: 45%;">Description</td>

                        <td style="width: 15%; text-align: center;">User</td>

                    </tr>
                    
                     <tr><td colspan="8"><div style="height: 1px; padding-top: 2px; padding-bottom: 2px; border-bottom: solid 1px #EEEEEE;"></div></td></tr>
                
            </HeaderTemplate>
            
            
            <ItemTemplate>
            
                <tr>

                    <td style="width: 20px; padding: 4px;"><img src="/Images/Common16/<%# DataBinder.Eval (Container.DataItem, "StepStatus") %>.png" alt="Status" /></td>
                
                    <td style="text-align: center;"><%# DataBinder.Eval(Container.DataItem, "StepDate") %></td>
                
                    <td><%# DataBinder.Eval(Container.DataItem, "Name") %></td>

                    <td><%# DataBinder.Eval(Container.DataItem, "Description") %></td>

                    <td style="text-align: center;"><%# DataBinder.Eval (Container.DataItem, "UserDisplayName")%></td>

                </tr>

            </ItemTemplate>
            
            <SeparatorTemplate>
            
                <tr><td colspan="8"><div style="height: 1px; padding-top: 2px; padding-bottom: 2px; border-bottom: solid 1px #EEEEEE;"></div></td></tr>
            
            </SeparatorTemplate>
            
            <FooterTemplate>
                    
                </table>
                    
            </FooterTemplate>
        
        </asp:Repeater>
        
    </div>
    
    
    
    
</div>

</form>

</body>

</html>
