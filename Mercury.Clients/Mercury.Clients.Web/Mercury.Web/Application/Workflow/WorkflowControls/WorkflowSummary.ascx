<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkflowSummary.ascx.cs" Inherits="Mercury.Web.Application.Workflow.WorkflowControls.WorkflowSummary" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<div id="WorkflowSummaryContainer" style=""> 
    
    
    <div style="padding: 4px; border: solid 1px #bbd7fa; margin: .125in;">
    
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

                    <td style="text-align: center;"><%# DataBinder.Eval(Container.DataItem, "UserDisplayName") %></td>

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

