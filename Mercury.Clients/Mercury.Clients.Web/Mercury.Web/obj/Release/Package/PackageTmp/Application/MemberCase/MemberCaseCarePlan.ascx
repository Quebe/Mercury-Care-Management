<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberCaseCarePlan.ascx.cs" Inherits="Mercury.Web.Application.MemberCase.MemberCaseCarePlan" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<%@ Register TagPrefix="MercuryUserControl" TagName="MemberCaseCarePlanGoal" Src="~/Application/MemberCase/MemberCaseCarePlanGoal.ascx" %>


<Telerik:RadAjaxManagerProxy ID="TelerikAjaxProxy" runat="server">

    <AjaxSettings>
    
        <Telerik:AjaxSetting AjaxControlID="ProblemClassView">
        
            <UpdatedControls>
            
                <Telerik:AjaxUpdatedControl ControlID="ProblemClassView" />
            
            </UpdatedControls>
        
        </Telerik:AjaxSetting>

        <Telerik:AjaxSetting AjaxControlID="CarePlanListView">
        
            <UpdatedControls>
            
                <Telerik:AjaxUpdatedControl ControlID="CarePlanListView" />
            
            </UpdatedControls>
        
        </Telerik:AjaxSetting>
    
    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>


<div style="display: none"><asp:TextBox ID="UserControlInstanceId" Text="" runat="server" /></div>

<div style="display: none"><asp:TextBox ID="MemberCaseId" Text="" runat="server"></asp:TextBox></div>


<Telerik:RadFormDecorator ID="TelerikFormDecorator" DecoratedControls="All" runat="server" />

<div id="IdentifiedProblemStatementSection" style="display: none;" runat="server">

    <div class="PropertyPageSectionTitle" style="margin-top: 0px;">Identified Problem Statements</div>

    <Telerik:RadGrid ID="IdentifiedProblemStatementsGrid" AutoGenerateColumns="false" runat="server">

        <MasterTableView>

            <Columns>
        
                <Telerik:GridBoundColumn HeaderText="Problem Statement" DataField="ProblemStatementName"></Telerik:GridBoundColumn>

                <Telerik:GridBoundColumn HeaderText="Source" DataField="Source"></Telerik:GridBoundColumn>
            
                <Telerik:GridBoundColumn HeaderStyle-Width="80" ItemStyle-Width="80" HeaderText="Identified" DataField="Identified Date"></Telerik:GridBoundColumn>

                <Telerik:GridBoundColumn HeaderStyle-Width="80" ItemStyle-Width="80" HeaderText="Is Required" DataField="IsRequired"></Telerik:GridBoundColumn>

                <Telerik:GridBoundColumn HeaderStyle-Width="100" ItemStyle-Width="100" HeaderText="Action" DataField="Action"></Telerik:GridBoundColumn>
        
            </Columns>                                                                  
                                
        </MasterTableView>

        <ClientSettings>
                                
            <Scrolling AllowScroll="false" UseStaticHeaders="true" />
                                
        </ClientSettings>

    </Telerik:RadGrid>

</div>

                
<div class="PropertyPageSectionTitle" style="margin-top: 0px; padding: 0px;">

    <table width="100%" cellpadding="0" cellspacing="0"><tr>

        <td>

        <Telerik:RadToolBar ID="CarePlanToolbar" Width="100%" BackColor="Transparent" OnClientButtonClicked="CarePlanToolbar_OnClientButtonClicked" OnButtonClick="CarePlanToolbar_OnButtonClick" runat="server">

            <Items>
                    
                <Telerik:RadToolBarButton Text="Add Problem Statement" PostBack="false" runat="server"></Telerik:RadToolBarButton>
                
                <Telerik:RadToolBarButton IsSeparator="true"></Telerik:RadToolBarButton>

                <Telerik:RadToolBarButton Text="Show Closed Problems" AllowSelfUnCheck="true" runat="server"></Telerik:RadToolBarButton>
                
                <Telerik:RadToolBarButton IsSeparator="true"></Telerik:RadToolBarButton>

                <Telerik:RadToolBarButton Text="Print Care Plan" AllowSelfUnCheck="true" runat="server"></Telerik:RadToolBarButton>
        
            </Items>
    
        </Telerik:RadToolBar>

        </td>

    </tr></table>

</div>



<Telerik:RadListView ID="ProblemClassListView" OnNeedDataSource="ProblemClassListView_OnNeedDataSource" runat="server">

    <ItemTemplate>
    
        <div style="margin: .125in; border: 1px solid #215485; background: White;">    

            <div class="PropertyPageSectionTitle" style="margin-top: 0px;">

                <%# Eval ("Classification") %> 

            </div>
       
            <!-- PROBLEM CLASS HEADER (BEGIN) -->

            <div style="padding-left: 8px; padding-right: 8px; background-color: White">

                <table width="100%" cellpadding="0" cellspacing="0" ><tr style="height: 28px;">
      
                    <td style="text-align: left; width: 33%"><b>Team Member:</b> 
                        
                        <asp:Label id="ProblemClassAssignedToUserLabel" Text="** Not Assigned" runat="server" />
                        
                        <div id="ProblemClassAssignedToUserEdit" style="display: none;">
                            
                            <Telerik:RadComboBox ID="ProblemClassAssignedToUserSelection" Width="100%" runat="server"></Telerik:RadComboBox>

                            <asp:LinkButton ID="ProblemClassAssignedToUserSaveLink" runat="server">(save)</asp:LinkButton>

                        </div>
                              
                        <div id="ProblemClassAssignedToUserChangeLink" style="display: inline;" runat="server">

                            <a id="ProblemClassAssignedToUserChangeToggle" href="javascript:ProblemClassAssignedToUserChangeToggle_OnClick ();">(change)</a>

                        </div>

                    </td>
                        
                    <td style="text-align: left; width: 33%"><b>Provider:</b> 
                        
                        <asp:Label id="ProblemClassAssignedToProviderLabel" Text="** Not Assigned" runat="server" />
                        
                        <div id="ProblemClassAssignedToProviderEdit" style="display: none;">
                            
                            <Telerik:RadComboBox ID="ProblemClassAssignedToProviderSelection" Width="100%" runat="server"></Telerik:RadComboBox>

                            <asp:LinkButton ID="ProblemClassAssignedToProviderSaveLink" runat="server">(save)</asp:LinkButton>

                        </div>
                              
                        <div id="ProblemClassAssignedToProviderChangeLink" style="display: inline;" runat="server">

                            <a id="ProblemClassAssignedToProviderChangeToggle" href="javascript:ProblemClassAssignedToProviderChangeToggle_OnClick ();">(change)</a>

                        </div>

                    </td>
                
                </table>

            </div>

            <!-- PROBLEM CLASS HEADER ( END ) -->
            

            <Telerik:RadListView ID="CarePlanListView" OnNeedDataSource="CarePlanListView_OnNeedDataSource" AllowMultiItemSelection="true" runat="server">

                <ItemTemplate>
                                                   
                    <div style="margin: .125in; border: 1px solid #215485; background: White;">    

                        <!-- PropertyPageSectionTitle #FFE0B3, PropertyPageSectionTitleComplement Black -->

                        <table class="PropertyPageSectionTitleComplement" width="100%" cellpadding="4" cellspacing="0" style="margin: 0px;">
                        
                            <td><asp:LinkButton ID="SelectButton" CommandName="Select" runat="server"><%# Eval ("ProblemStatementName")%></asp:LinkButton></td>

                            <td style="text-align:right; font-weight: normal"><%# Mercury.Server.CommonFunctions.EnumerationToString (Eval ("CarePlan.Status")) %></td>
                        
                        </table>

                     </div>
 
                </ItemTemplate>

                <SelectedItemTemplate>
    
                    <div style="margin: .125in; border: 1px solid #215485; background: #A6C6E6;">
    
                        <div class="PropertyPageSectionTitle" style="margin-top: 0px;">
        
                            <%# Eval ("ProblemStatementName")%>

                                <asp:LinkButton ID="SelectButton" CommandName="Deselect" Text="(hide details)" ForeColor="#FFE0B3" runat="server" />
        
                        </div>
            
                        <div class="BackgroundColorComplementNormal BorderColorDark" style="background-color: White; margin: .125in">

                        <!-- CARE PLAN HEADER - COMMON (BEGIN) -->
                        
                        <table width="100%" cellpadding="0" cellspacing="0" style="padding-left: 8px; padding-right: 8px;">
    
                            <tr class="" style="height: 36px;">
                
                                <td style="text-align: left; width: 18%"><b>Status:&nbsp;</b> <%# Mercury.Server.CommonFunctions.EnumerationToString (Eval ("CarePlan.Status")) %></td>
                        
                                <td style="text-align: left; width: 34%"><b>Severity:</b> 
                        
                                    <asp:Label id="CaseCareLevel" Text="Not Specified" runat="server" />

                                    <asp:LinkButton ID="CaseLevelChange" runat="server">(change)</asp:LinkButton>
                            
                                </td>
                                
                            </tr>
        
                        </table>  
                        
                        <!-- CARE PLAN HEADER - COMMON ( END ) -->

                        <!-- CARE PLAN HEADER (BEGIN) -->

                        <table width="100%" cellpadding="0" cellspacing="0" style="padding-left: 8px; padding-right: 8px;">
    
                            <tr class="" style="height: 36px;">
                
                                <td style="text-align: left; width: 18%">
                    
                                    <b>Added Date*:&nbsp;</b>
                    
                                    <span title="<%# Eval ("CarePlan.CreateAccountInfo.UserAccountName") %>"><%# Eval ("CarePlan.AddedDateDescription")%> </span>

                                </td>
                    
                                <td style="text-align: left; width: 18%">

                                    <div style="display: <%# ((((Mercury.Server.Application.CaseItemStatus) Eval ("CarePlan.Status")) != Mercury.Server.Application.CaseItemStatus.UnderDevelopment) ? "block;" : "none;") %>">
                    
                                        <b>Effective Date:&nbsp;</b> 
                    
                                        <%# ((((Mercury.Server.Application.CaseItemStatus)Eval ("CarePlan.Status")) != Mercury.Server.Application.CaseItemStatus.UnderDevelopment) ? Eval ("CarePlan.EffectiveDateDescription") : String.Empty)%>

                                    </div>
                    
                                </td>

                                <td style="text-align: left; width: 18%">
                    
                                    <div style="display: <%# ((((Mercury.Server.Application.CaseItemStatus) Eval ("CarePlan.Status")) != Mercury.Server.Application.CaseItemStatus.UnderDevelopment) ? "block;" : "none;") %>">
                    
                                        <b>Termination Date:</b> 
                        
                                        <%# ((((Mercury.Server.Application.CaseItemStatus)Eval ("CarePlan.Status")) != Mercury.Server.Application.CaseItemStatus.UnderDevelopment) ? Eval ("CarePlan.TerminationDateDescription") : String.Empty)%>

                                    </div>
                        
                                </td>
                        
                                <td style="text-align: left; width: 28%">
                    
                                    <div style="display: <%# (((Eval ("CarePlan.CareOutcome")) != null) ? "block;" : "none;") %>">

                                        <b>Outcome:</b> 
                            
                                        <%# Eval ("CarePlan.CareOutcome.Name")%>

                                    </div>

                                </td>
                        
                            </tr>
        
                        </table>  
                        
                        </div>              
            
                        <!-- CARE PLAN HEADER ( END ) -->

                        <div style="margin: .125in">
                        
                        <Telerik:RadGrid ID="CarePlanGoalGrid" Width="100%" AutoGenerateColumns="false" 

                            OnNeedDataSource="CarePlanGoalGrid_OnNeedDataSource" Skin="Sunset"
                        
                            runat="server">

                            <MasterTableView>
                        
                                <Columns>

                                    <Telerik:GridBoundColumn DataField="Name" HeaderText="Goal" />

                                    <Telerik:GridBoundColumn DataField="ClinicalNarrative" HeaderText="Clinical" />

                                    <Telerik:GridBoundColumn DataField="GoalTimeframeDescription" HeaderText="Timeframe" />

                                    <Telerik:GridBoundColumn DataField="CareMeasurementName" HeaderText="Measurement" />

                                    <Telerik:GridButtonColumn HeaderText="Action" Text="(delete)"></Telerik:GridButtonColumn>
                                                                   
                                </Columns>

                                <NestedViewTemplate>
                                
                                    <MercuryUserControl:MemberCaseCarePlanGoal ID="MemberCaseCarePlanGoalControl" CarePlanGoal="<%# Container.DataItem %>" runat="server" />

                                </div>

                                </NestedViewTemplate>
                            
                            </MasterTableView>

                            <ClientSettings>
                                
                                <Scrolling AllowScroll="false" UseStaticHeaders="true" />
                                
                            </ClientSettings>
                        
                        </Telerik:RadGrid>

                        </div>

                        
                        <div style="margin: .125in">
                        
                        <Telerik:RadGrid ID="CarePlanInterventionGrid" Width="100%" AutoGenerateColumns="false"

                            OnNeedDataSource="CarePlanInterventionGrid_OnNeedDataSource" Skin="Sunset"
                        
                            runat="server">

                            <MasterTableView>
                        
                                <Columns>

                                    <Telerik:GridBoundColumn DataField="Name" HeaderText="Intervention" />

                                    <Telerik:GridBoundColumn DataField="Description" HeaderText="Description" />

                                    <Telerik:GridBoundColumn DataField="Inclusion" HeaderText="Inclusion" />

                                    <Telerik:GridButtonColumn HeaderText="Action" Text="(delete)"></Telerik:GridButtonColumn>
                                                                   
                                </Columns>

                                <NestedViewTemplate>
                                
                                <div style="margin: .125in; border: 1px solid #215485">
    
                                    <div class="PropertyPageSectionTitleComplement" style="margin-top: 0px;">
        
                                        Intervention: <span style="font-weight: normal;"><%# Eval ("Name") %></span>
                                    
                                    </div>

                                    <div style="margin: .125in; line-height: 150%"> <!-- CONTENT DIV (BEGIN) -->
                        
                                        <table width="100%" cellpadding="0" cellspacing="0" style="">
    
                                            <tr class="" style="height: 24px;">

                                                <td style="text-align: left; width: 18%"><b>Status:</b> <asp:Label id="CaseStatus" Text="Not Specified" runat="server" /></td>
                        
                                                <td style="text-align: left; width: 18%"><b>Added Date:</b> <asp:Label id="Label2" Text="" runat="server" /></td>
                                    
                                                <td style="text-align: left; width: 18%"><b>Term:</b> <asp:Label id="Label4" Text="Short-term" runat="server" /></td>

                                                <td style="text-align: left; width: 18%"><b>Goal Date:</b> <asp:Label id="Label5" Text="MM/DD/YYYY" runat="server" /></td>
                        
                                            </tr>
        
                                        </table>                
                            
                                        <table width="100%" cellpadding="0" cellspacing="0" style="">
    
                                            <tr class="" style="height: 24px;">

                                                <td style="text-align: left; width: 18%"><b>Effective Date:</b> <asp:Label id="CaseEffectiveDate" Text="" runat="server" /></td>
                        
                                                <td style="text-align: left; width: 18%"><b>Termination Date:</b> <asp:Label id="CaseTerminationDate" Text="< active >" runat="server" /></td>
                        
                                                <td style="text-align: left; width: 28%"><b>Outcome:</b> <asp:Label id="CaseOutcome" Text="Not Specified" runat="server" /></td>
                        
                                            </tr>
        
                                        </table>        

                                    </div>s

                                </div>

                                </NestedViewTemplate>
                            
                            </MasterTableView>

                            <ClientSettings>
                                
                                <Scrolling AllowScroll="false" UseStaticHeaders="true" />
                                
                            </ClientSettings>
                        
                        </Telerik:RadGrid>

                        </div>

                    </div>

                </SelectedItemTemplate>

                <EmptyDataTemplate>
    
                    <div style="margin: .125in; padding: .25in; border: 1px solid #215485; background: White;">There are no Problem Statements or Care Plans available for this Problem Classification.</div>
            
                </EmptyDataTemplate>
    
            </Telerik:RadListView>


         </div>

    </ItemTemplate>

    <SelectedItemTemplate>
    
    </SelectedItemTemplate>
    
    <EmptyDataTemplate>
    
        <div style="margin: .125in; padding: .25in; border: 1px solid #215485; background: White;">There are no Care Plans available for this Case.</div>
            
    </EmptyDataTemplate>

</Telerik:RadListView>

<div style="height: .0625in"></div>


<Telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

<script type="text/javascript">

    function CarePlanToolbar_OnClientButtonClicked(sender, e) {

        var buttonClickedText = e.get_item().get_text();

        var memberCaseId = document.getElementById("<%= MemberCaseId.ClientID %>").value;

        switch (buttonClickedText) {

            case "Add Problem Statement":

                window.location = "/Application/MemberCase/Actions/AddProblemStatement.aspx?MemberCaseId=" + memberCaseId;

                break;

        }

        return;

    }

</script>

</Telerik:RadCodeBlock>

