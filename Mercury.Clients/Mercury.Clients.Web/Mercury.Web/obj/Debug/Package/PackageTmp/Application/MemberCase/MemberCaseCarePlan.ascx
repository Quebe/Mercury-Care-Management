<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberCaseCarePlan.ascx.cs" Inherits="Mercury.Web.Application.MemberCase.MemberCaseCarePlan" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<%@ Register TagPrefix="MercuryUserControl" TagName="MemberCaseCarePlanSelected" Src="~/Application/MemberCase/MemberCaseCarePlanSelected.ascx" %>

<%@ Register TagPrefix="MercuryUserControl" TagName="MemberCaseCarePlanGoal" Src="~/Application/MemberCase/MemberCaseCarePlanGoal.ascx" %>


<div style="display: none"><asp:TextBox ID="UserControlInstanceId" Text="" runat="server" /></div>

<div style="display: none"><asp:TextBox ID="MemberCaseId" Text="" runat="server"></asp:TextBox></div>

<div style="display: none"><asp:TextBox ID="CarePlanViewMode" Text="CarePlan" runat="server"></asp:TextBox></div>


<Telerik:RadFormDecorator ID="TelerikFormDecorator" DecoratedControls="All" runat="server" />

<div id="IdentifiedProblemStatementSection" visible="false" runat="server">

    <div class="PropertyPageSectionTitle" style="margin-top: 0px;">Identified Problem Statements</div>

    <Telerik:RadGrid ID="IdentifiedProblemStatementsGrid" AutoGenerateColumns="false" runat="server">

        <MasterTableView>

            <Columns>
        
                <Telerik:GridBoundColumn HeaderText="Domain" DataField="ProblemStatement.ProblemDomainName"></Telerik:GridBoundColumn>

                <Telerik:GridBoundColumn HeaderText="Class" DataField="ProblemStatement.ProblemClassName"></Telerik:GridBoundColumn>

                <Telerik:GridBoundColumn HeaderText="Problem" DataField="ProblemStatement.Name"></Telerik:GridBoundColumn>

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

                <Telerik:RadToolBarButton Text="View by Care Plan" AllowSelfUnCheck="true" runat="server"></Telerik:RadToolBarButton>
                    
                <Telerik:RadToolBarButton IsSeparator="true"></Telerik:RadToolBarButton>

                <Telerik:RadToolBarButton Text="Add Problem Statement" PostBack="false" runat="server"></Telerik:RadToolBarButton>
                
                <Telerik:RadToolBarButton IsSeparator="true"></Telerik:RadToolBarButton>

                <Telerik:RadToolBarButton Text="Show Closed" AllowSelfUnCheck="true" runat="server"></Telerik:RadToolBarButton>
                
                <Telerik:RadToolBarButton IsSeparator="true"></Telerik:RadToolBarButton>

                <Telerik:RadToolBarButton Text="Print Care Plan" AllowSelfUnCheck="true" runat="server"></Telerik:RadToolBarButton>
        
            </Items>
    
        </Telerik:RadToolBar>

        </td>

    </tr></table>

</div>


<Telerik:RadListView ID="MemberCaseCarePlanListView" AllowMultiItemSelection="true"

        OnNeedDataSource="MemberCaseCarePlanListView_OnNeedDataSource"
        
        DataKeyNames="Id" Visible="false"
        
        runat="server">        
        
    <ItemTemplate>
    
        <div style="margin: .125in; border: 1px solid #215485; background: White;">    
              
            <div class="PropertyPageSectionTitle" style="margin-top: 0px;">
        
                <%# Eval ("Name")%> <asp:LinkButton ID="SelectButton" CommandName="Select" Text="(show details)" ForeColor="#FFE0B3" runat="server" />
        
            </div>
            
            <div style="padding: .125in; background-color: White">

                <Telerik:RadGrid ID="MemberCaseCarePlanListViewProblemsGrid" 
                
                    OnNeedDataSource="MemberCaseCarePlanListViewProblemsGrid_OnNeedDataSource"
                
                    AutoGenerateColumns="false" Skin="Office2007" runat="server">
                    
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="">

                        <Columns>
        
                            <Telerik:GridBoundColumn DataField="Id" HeaderText="Id" Visible="false"></Telerik:GridBoundColumn>

                            <Telerik:GridBoundColumn DataField="ProblemStatementClassificationWithName" HeaderText="Associated Problem Statements"></Telerik:GridBoundColumn>

                            <Telerik:GridBoundColumn DataField="ProblemStatement.Description" HeaderText="Description"></Telerik:GridBoundColumn>

                            <Telerik:GridBoundColumn DataField="MemberCaseProblemClass.AssignedToUserDisplayName" HeaderText="Assigned To"></Telerik:GridBoundColumn>

                            <Telerik:GridBoundColumn DataField="MemberCaseProblemClass.AssignedToProvider.Name" HeaderText="Provider"></Telerik:GridBoundColumn>

                            <Telerik:GridBoundColumn DataField="IsSingleInstance" HeaderText="Is Single Instance"></Telerik:GridBoundColumn>

                        </Columns>                                                                  
                                         
                    </MasterTableView>

                    <ClientSettings>
                                
                        <Scrolling AllowScroll="false" UseStaticHeaders="true" />
                                
                    </ClientSettings>
                
                </Telerik:RadGrid>

            </div>

        </div>

    </ItemTemplate>

    <SelectedItemTemplate>
    
        <div style="margin: .125in; border: 1px solid #215485; background: #A6C6E6;">    
              
            <div class="PropertyPageSectionTitle" style="margin-top: 0px;">
        
                <%# Eval ("Name")%> <asp:LinkButton ID="SelectButton" CommandName="Deselect" Text="(hide details)" ForeColor="#FFE0B3" runat="server" />
        
            </div>
            
            <MercuryUserControl:MemberCaseCarePlanSelected ID="MemberCaseCarePlanSelectedControl" MemberCaseCarePlan="<%# ((Mercury.Client.Core.Individual.Case.MemberCaseCarePlan) Container.DataItem) %>" runat="server" />

        </div>

    </SelectedItemTemplate>
    
    <EmptyDataTemplate>
    
        <div style="margin: .125in; padding: .25in; border: 1px solid #215485; background: White;">There are no Care Plans available for this Case.</div>
            
    </EmptyDataTemplate>

</Telerik:RadListView>

<Telerik:RadListView ID="ProblemClassListView" 

        OnNeedDataSource="ProblemClassListView_OnNeedDataSource" 

        DataKeyNames="Id" Visible="true" ClientIDMode="AutoID"
        
        runat="server">

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
                        
                        <div id="ProblemClassAssignedToUserEdit_<%# Eval ("Id") %>" style="display: none;">
                            
                            <Telerik:RadComboBox ID="ProblemClassAssignedToUserSelection" Width="100%" runat="server"></Telerik:RadComboBox>

                            <asp:LinkButton ID="ProblemClassAssignedToUserSaveLink" OnClick="ProblemClassAssignedToUserSaveLink_OnClick" runat="server">(save)</asp:LinkButton>

                        </div>
                              
                        <div id="ProblemClassAssignedToUserChangeLink" style="display: inline;" runat="server">

                            <a id="ProblemClassAssignedToUserChangeToggle_<%# Eval ("Id") %>" href="javascript:ProblemClassAssignedToUserChangeToggle_OnClick ('<%# Eval ("Id") %>');" style="display: inline;">(change)</a>

                        </div>

                    </td>
                        
                    <td style="text-align: left; width: 33%"><b>Provider:</b> 
                        
                        <asp:Label id="ProblemClassAssignedToProviderLabel" Text="** Not Assigned" runat="server" />
                        
                        <div id="ProblemClassAssignedToProviderEdit_<%# Eval ("Id") %>" style="display: none;">
                            
                            <Telerik:RadComboBox ID="ProblemClassAssignedToProviderSelection" Width="100%" OnItemsRequested="ProblemClassAssignedToProviderSelection_OnItemsRequested" EnableLoadOnDemand="true" OnClientItemsRequesting="ProblemClassAssignedToProviderSelection_OnClientItemsRequesting" runat="server"></Telerik:RadComboBox>

                            <asp:LinkButton ID="ProblemClassAssignedToProviderSaveLink"  OnClick="ProblemClassAssignedToProviderSaveLink_OnClick" runat="server">(save)</asp:LinkButton>

                        </div>
                              
                        <div id="ProblemClassAssignedToProviderChangeLink" style="display: inline;" runat="server">

                            <a id="ProblemClassAssignedToProviderChangeToggle_<%# Eval ("Id") %>" href="javascript:ProblemClassAssignedToProviderChangeToggle_OnClick ('<%# Eval ("Id") %>');">(change)</a>

                        </div>

                    </td>
                
                </table>

            </div>

            <!-- PROBLEM CLASS HEADER ( END ) -->
            

            <Telerik:RadListView ID="CarePlanListView" 
            
                OnSelectedIndexChanged="CarePlanListView_OnSelectedIndexChanged"

                OnNeedDataSource="CarePlanListView_OnNeedDataSource" 

                OnItemCommand="CarePlanListView_OnItemCommand"

                OnPreRender="CarePlanListView_OnPreRender"

                DataKeyNames="Id"
                
                AllowMultiItemSelection="true" runat="server">

                <ItemTemplate>
                                                   
                    <div style="margin: .125in; border: 1px solid #215485; background: White;">    

                        <!-- PropertyPageSectionTitle #FFE0B3, PropertyPageSectionTitleComplement Black -->

                        <table class="PropertyPageSectionTitleComplement" width="100%" cellpadding="4" cellspacing="0" style="margin: 0px;">
                        
                            <td><asp:LinkButton ID="SelectButton" CommandName="Select" runat="server"><%# Eval ("ProblemStatementName")%></asp:LinkButton></td>

                            <td style="text-align:right; font-weight: normal"><%# Mercury.Server.CommonFunctions.EnumerationToString (Eval ("MemberCaseCarePlan.Status")) %></td>
                        
                        </table>

                     </div>
 
                </ItemTemplate>

                <SelectedItemTemplate>

                
                    <div style="margin: .125in; border: 1px solid #215485; background: #A6C6E6;">
    
                        <div class="PropertyPageSectionTitle" style="margin-top: 0px;">
        
                            <%# Eval ("ProblemStatementName")%>

                                <asp:LinkButton ID="SelectButton" CommandName="Deselect" Text="(hide details)" ForeColor="#FFE0B3" runat="server" />
        
                        </div>
    
                        <MercuryUserControl:MemberCaseCarePlanSelected ID="MemberCaseCarePlanSelectedControl" MemberCaseCarePlan="<%# ((Mercury.Client.Core.Individual.Case.MemberCaseProblemCarePlan) Container.DataItem).MemberCaseCarePlan %>" runat="server" />
                    
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

    function ProblemClassAssignedToUserChangeToggle_OnClick(problemClassId) {

        var problemClassAssignedToUserChangeToggle = document.getElementById("ProblemClassAssignedToUserChangeToggle_" + problemClassId);

        var problemClassAssignedToUserEdit = document.getElementById("ProblemClassAssignedToUserEdit_" + problemClassId);

        if (problemClassAssignedToUserEdit != null) {

            if (problemClassAssignedToUserEdit.style.display == "none") {

                problemClassAssignedToUserEdit.style.display = "inline";

                problemClassAssignedToUserChangeToggle.innerHTML = "(cancel)";

            }

            else {

                problemClassAssignedToUserEdit.style.display = "none";

                problemClassAssignedToUserChangeToggle.innerHTML = "(change)";

            }

        }

        Page_Repaint();

        return;

    }

    function ProblemClassAssignedToProviderChangeToggle_OnClick(problemClassId) {

        var problemClassAssignedToProviderChangeToggle = document.getElementById("ProblemClassAssignedToProviderChangeToggle_" + problemClassId);

        var problemClassAssignedToProviderEdit = document.getElementById("ProblemClassAssignedToProviderEdit_" + problemClassId);

        if (problemClassAssignedToProviderEdit != null) {

            if (problemClassAssignedToProviderEdit.style.display == "none") {

                problemClassAssignedToProviderEdit.style.display = "inline";

                problemClassAssignedToProviderChangeToggle.innerHTML = "(cancel)";

            }

            else {

                problemClassAssignedToProviderEdit.style.display = "none";

                problemClassAssignedToProviderChangeToggle.innerHTML = "(change)";

            }

        }

        Page_Repaint();

        return;

    }

    function ProblemClassAssignedToProviderSelection_OnClientItemsRequesting(sender, eventArgs) {

        if (sender.get_text().length < 3) {

            eventArgs.set_cancel(true);

        } 

    }

</script>

</Telerik:RadCodeBlock>

