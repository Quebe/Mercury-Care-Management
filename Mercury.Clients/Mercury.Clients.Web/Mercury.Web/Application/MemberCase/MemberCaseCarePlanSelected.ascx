<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberCaseCarePlanSelected.ascx.cs" Inherits="Mercury.Web.Application.MemberCase.MemberCaseCarePlanSelected" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<%@ Register TagPrefix="MercuryUserControl" TagName="MemberCaseCarePlanGoal" Src="~/Application/MemberCase/MemberCaseCarePlanGoal.ascx" %>

<%@ Register TagPrefix="MercuryUserControl" TagName="MemberCaseCarePlanIntervention" Src="~/Application/MemberCase/MemberCaseCarePlanIntervention.ascx" %>

            
    <div class="BackgroundColorComplementNormal BorderColorDark" style="background-color: White; margin: .125in">

    <!-- CARE PLAN HEADER (BEGIN) -->
                        
    <table width="100%" cellpadding="0" cellspacing="0" style="padding-left: 8px; padding-right: 8px;">
    
        <tr class="" style="height: 36px;">
                
            <td style="text-align: left; width: 18%"><b>Status:&nbsp;</b> <asp:Label ID="MemberCaseCarePlanStatus" runat="server"></asp:Label></td>
                        
            <td style="text-align: left; width: 34%"><b>Severity:</b> 
                        
                <asp:Label id="CaseCareLevel" Text="Not Specified" runat="server" />

                <div id="CaseCareLevelSeverityEdit_<%= this.ClientID  %>" style="display: none;">
                            
                    <Telerik:RadComboBox ID="CaseCareLevelSeveritySelection" Width="100%" runat="server"></Telerik:RadComboBox>

                    <asp:LinkButton ID="CaseCareLevelSeveritySaveLink" OnClick="CaseCareLevelSeveritySaveLink_OnClick" runat="server">(save)</asp:LinkButton>

                </div>

                <div id="CaseLevelChangeSeverityLink" style="display: inline;" runat="server">

                    <a id="CaseLevelChangeSeverityLinkChangeToggle" href="javascript:

                            (function () { 
                        
                                var CaseLevelChangeSeverityLinkChangeToggle = document.getElementById ('CaseLevelChangeSeverityLinkChangeToggle');

                                var CaseCareLevelSeverityEdit = document.getElementById ('CaseCareLevelSeverityEdit_<%= this.ClientID  %>');

                                if (CaseCareLevelSeverityEdit != null) {

                                    if (CaseCareLevelSeverityEdit.style.display == 'none') {

                                        CaseCareLevelSeverityEdit.style.display = 'inline';

                                        CaseLevelChangeSeverityLinkChangeToggle.innerHTML = '(cancel)';

                                    }

                                    else {

                                        CaseCareLevelSeverityEdit.style.display = 'none';

                                        CaseLevelChangeSeverityLinkChangeToggle.innerHTML = '(change)';

                                    }

                                }

                                Page_Repaint();

                                return;

                            })();
                        
                            " style="display: inline;">(change)</a>

                </div>
                            
            </td>

            <td align="right"><asp:HyperLink ID="PeformAssessmentHyperLink" runat="server">Perform Assessment</asp:HyperLink></td>
                                
        </tr>
        
    </table>  

    </div>
                        
    <!-- CARE PLAN HEADER ( END ) -->

    <div style="margin: .125in">
                        
    <Telerik:RadGrid ID="CarePlanGoalGrid" Width="100%" AutoGenerateColumns="false"

        OnItemCommand="CarePlanGoalGrid_OnItemCommand"

        OnNeedDataSource="CarePlanGoalGrid_OnNeedDataSource"

        OnItemCreated="CarePlanGoalGrid_OnItemCreated"
                            
        Skin="Sunset" runat="server">

        <MasterTableView CommandItemDisplay="Bottom" DataKeyNames="MemberCaseCarePlanId, Id">
                        
            <Columns>

                <Telerik:GridBoundColumn DataField="MemberCaseCarePlanId" Visible="false"></Telerik:GridBoundColumn>

                <Telerik:GridBoundColumn DataField="Id" Visible="false"></Telerik:GridBoundColumn>

                <Telerik:GridBoundColumn DataField="Name" HeaderText="Goal" />

                <Telerik:GridBoundColumn DataField="ClinicalNarrative" HeaderText="Clinical" />

                <Telerik:GridBoundColumn DataField="GoalTimeframeDescription" HeaderText="Timeframe" />
                
                <Telerik:GridBoundColumn DataField="Inclusion" HeaderText="Inclusion" />

                <Telerik:GridBoundColumn DataField="InitialValue" HeaderText="Initial" DataFormatString="{0:#.00}" />

                <Telerik:GridBoundColumn DataField="LastValue" HeaderText="Last" DataFormatString="{0:#.00}" />

                <Telerik:GridBoundColumn DataField="TargetValue" HeaderText="Target" DataFormatString="{0:#.00}" />

                <Telerik:GridButtonColumn HeaderText="Action" CommandName="Delete" Text="(delete)" ConfirmText="Are you sure you want to delete this Goal?"></Telerik:GridButtonColumn>
                                                                   
            </Columns>

            <NestedViewTemplate>
                                
                <MercuryUserControl:MemberCaseCarePlanGoal ID="MemberCaseCarePlanGoalControl" CarePlanGoal="<%# Container.DataItem %>" runat="server" />

            </NestedViewTemplate>

            <EditFormSettings EditFormType="Template">
                                
                <FormTemplate>
                                    
                    <div style="padding: .125in; border: 1px solid #215485; line-height: 150%">
                                            
                        Add a new goal to the Care Plan.

                        <table cellpadding="0" cellspacing="0" width="100%"><tr>
                                            
                            <td style="width: 160px;">
                                                
                                <asp:RadioButtonList ID="AddCarePlanGoalTypeSelection" runat="server">

                                    <asp:ListItem Selected="True" Value="0"><span style="color: Black;">Copy from Existing Goal:</span></asp:ListItem>

                                    <asp:ListItem Value="1"><span style="color: Black;">Create new Goal, Name:</span></asp:ListItem>
                                                
                                </asp:RadioButtonList>

                            </td>

                            <td>
                                                   
                                <div><Telerik:RadComboBox ID="AddCarePlanGoalExistingSelection" Width="100%" runat="server"></Telerik:RadComboBox></div>

                                <div><table cellpadding="0" cellspacing="0" width="100%"><tr>
                                                   
                                    <td style="width: 50%"><Telerik:RadTextBox ID="AddCarePlanGoalName" Width="100%" runat="server"></Telerik:RadTextBox></td>

                                    <td style="white-space: nowrap; padding-left: 8px; padding-right: 4px;">Care Measure:</td>
                                                        
                                    <td style="width: 50%"><Telerik:RadComboBox ID="AddCarePlanGoalCareMeasureSelection"  Width="100%" runat="server"></Telerik:RadComboBox></td>
                                                        
                                </tr></table></div>

                            </td>
                                            
                        </tr></table>

                        <div style="height: .125in;"></div>

                        <asp:Button ID="CarePlanGoalInsert" Text="Insert" Width="73" CommandName="PerformInsert" runat="server" />

                        <asp:Button ID="CarePlanGoalCancel" Text="Cancel" Width="73" CommandName="Cancel" CausesValidation="false" runat="server" />

                    </div>
                                    
                </FormTemplate>

            </EditFormSettings>
                                                            
        </MasterTableView>

        <ClientSettings>
                                
            <Scrolling AllowScroll="false" UseStaticHeaders="true" />
                                
        </ClientSettings>
                        
    </Telerik:RadGrid>

    </div>

                        
    <div style="margin: .125in">
                        
    <Telerik:RadGrid ID="CarePlanInterventionGrid" Width="100%" AutoGenerateColumns="false"
    
        OnItemCommand="CarePlanInterventionGrid_OnItemCommand"

        OnNeedDataSource="CarePlanInterventionGrid_OnNeedDataSource" 
                        
        OnItemCreated="CarePlanInterventionGrid_OnItemCreated"

        Skin="Sunset" runat="server">

        <MasterTableView CommandItemDisplay="None" DataKeyNames="">
                        
            <Columns>

                <Telerik:GridBoundColumn DataField="Name" HeaderText="Intervention" />

                <Telerik:GridBoundColumn DataField="StatusDescription" HeaderText="Status" />

                <Telerik:GridBoundColumn DataField="Name" HeaderText="Next Activity" />

                <Telerik:GridBoundColumn DataField="Name" HeaderText="Activity Date" />
                
            </Columns>

            <NestedViewTemplate>

                <MercuryUserControl:MemberCaseCarePlanIntervention ID="MemberCaseCarePlanInterventionControl" CarePlanIntervention="<%# Container.DataItem %>" runat="server" />

            </NestedViewTemplate>
                            
            <EditFormSettings EditFormType="Template">
                                
                <FormTemplate>
                                    
                    <div style="padding: .125in; border: 1px solid #215485; line-height: 150%">
                                            
                        Add a new intervention to the Care Plan.

                        <table cellpadding="0" cellspacing="0" width="100%"><tr>
                                            
                            <td style="width: 200px;">
                                                
                                <asp:RadioButtonList ID="AddCarePlanInterventionTypeSelection" runat="server">

                                    <asp:ListItem Selected="True" Value="0"><span style="color: Black;">Copy from Existing Intervention:</span></asp:ListItem>

                                    <asp:ListItem Value="1"><span style="color: Black;">Create new Intervention, Name:</span></asp:ListItem>
                                                
                                </asp:RadioButtonList>

                            </td>

                            <td>
                                                   
                                <div><Telerik:RadComboBox ID="AddCarePlanInterventionExistingSelection" Width="100%" runat="server"></Telerik:RadComboBox></div>

                                <div><table cellpadding="0" cellspacing="0" width="100%"><tr>
                                                   
                                    <td style="width: 50%"><Telerik:RadTextBox ID="AddCarePlanInterventionName" Width="100%" runat="server"></Telerik:RadTextBox></td>
                                                       
                                </tr></table></div>

                            </td>
                                            
                        </tr></table>

                        <div style="height: .125in;"></div>

                        <asp:Button ID="CarePlanInterventionInsert" Text="Insert" Width="73" CommandName="PerformInsert" runat="server" />

                        <asp:Button ID="CarePlanInterventionCancel" Text="Cancel" Width="73" CommandName="Cancel" CausesValidation="false" runat="server" />

                    </div>
                                    
                </FormTemplate>

            </EditFormSettings>
                                                          
        </MasterTableView>

        <ClientSettings>
                                
            <Scrolling AllowScroll="false" UseStaticHeaders="true" />
                                
        </ClientSettings>
                        
    </Telerik:RadGrid>

    </div>