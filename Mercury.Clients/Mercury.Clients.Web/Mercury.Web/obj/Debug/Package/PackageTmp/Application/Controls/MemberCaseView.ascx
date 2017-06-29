<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberCaseView.ascx.cs" Inherits="Mercury.Web.Application.Controls.MemberCaseView" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div style="display: none"><asp:TextBox ID="UserControlInstanceId" Text="" runat="server" /></div>


<Telerik:RadAjaxManagerProxy ID="TelerikAjaxProxy" runat="server">

    <AjaxSettings>
    
        <Telerik:AjaxSetting AjaxControlID="MemberCaseGrid" >
        
            <UpdatedControls>
            
                <Telerik:AjaxUpdatedControl ControlID="MemberCaseGrid" />
                
            </UpdatedControls>
        
        </Telerik:AjaxSetting>
        
    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>


<Telerik:RadGrid ID="MemberCaseGrid" AllowPaging="false" AllowCustomPaging="false" AutoGenerateColumns="false" 

    OnNeedDataSource="MemberCaseGrid_OnNeedDataSource" 
    
    OnItemCommand="MemberCaseGrid_OnItemCommand" 

    OnItemCreated="MemberCaseGrid_OnItemCreated"
    
    runat="server">

    <MasterTableView Name="MemberCaseGridMasterView" Width="100%" TableLayout="Auto" DataKeyNames="MemberId, Id" CommandItemDisplay="Top" runat="server">
    
        <CommandItemTemplate>
        
            <div style="padding: 6px;">
        
                <asp:LinkButton ID="CreateCaseButton" CommandName="MemberCaseCreate" runat="server"><img src="/Images/Common16/CaseAdd.png" alt="Create New Case" style="border: none; padding-right: 8px;" />Create New Case</asp:LinkButton>
            
            </div>            
        
        </CommandItemTemplate>
    
        <Columns>    

            <Telerik:GridTemplateColumn DataField="Id" HeaderText="Case Id">
            
                <ItemTemplate>
                
                    <a href="#" onclick="javascript:window.open('/Application/MemberCase/MemberCase.aspx?MemberCaseId=<%# Eval ("Id")  %>', '_blank', 'location=no,directories=no,menubar=no,toolbar=no,resizable=yes,scrollbars=yes,status=yes')" title="Open Case" alt="Open Case"><%# Eval ("Id") %></a>

                </ItemTemplate>
            
            </Telerik:GridTemplateColumn>


            <Telerik:GridBoundColumn DataField="ReferenceNumber"  HeaderText="Reference Number" Visible="true" />

            <Telerik:GridBoundColumn DataField="MemberId" UniqueName="MemberId" Visible="false" />

            <Telerik:GridBoundColumn DataField="CareLevelId" UniqueName="CareLevelId" Visible="false" />            

            <Telerik:GridBoundColumn DataField="CareLevelName" UniqueName="CareLevelName" HeaderText="Care Level" ReadOnly="true" Visible="true" />
                        
            <Telerik:GridBoundColumn DataField="AssignedToWorkTeam.Name" HeaderText="Assigned To Team" Visible="true" />

            <Telerik:GridBoundColumn DataField="AssignedToUserDisplayName" HeaderText="Assigned To" Visible="true" />
            
            
            <Telerik:GridBoundColumn DataField="StatusDescription" UniqueName="Status" HeaderText="Status" ReadOnly="true" Visible="true" />


            <Telerik:GridBoundColumn DataField="EffectiveDate" UniqueName="EffectiveDate" HeaderText="Effective" ReadOnly="true" Visible="true" />
        
            <Telerik:GridBoundColumn DataField="TerminationDate" UniqueName="TerminationDate" HeaderText="Termination" ReadOnly="true" Visible="true" />

            <Telerik:GridBoundColumn DataField="CareOutcomeId" UniqueName="CareOutcomeId" Visible="false" />

            <Telerik:GridBoundColumn DataField="CareOutcomeName" UniqueName="CareOutcomeName" HeaderText="Outcome" ReadOnly="true" Visible="true" />

        </Columns>

        <DetailTables>
        
            <Telerik:GridTableView Width="100%" DataKeyNames="MemberCaseId" runat="server">
            
                <ParentTableRelation>
                
                    <Telerik:GridRelationFields MasterKeyField="Id" DetailKeyField="MemberCaseId" />
                
                </ParentTableRelation>
                
                <Columns>
                        
                    <Telerik:GridBoundColumn DataField="MemberCaseId" UniqueName="MemberCaseId" Visible="false" />
                                                          
                    <Telerik:GridBoundColumn DataField="MemberCarePlanId" UniqueName="MemberCarePlanId" Visible="false" />

                    <Telerik:GridBoundColumn DataField="ProblemStatementId" UniqueName="ProblemStatementId" visible="false" />
                    
                    <Telerik:GridBoundColumn DataField="CarePlanId" UniqueName="CarePlanId" Visible="false" />
                    
                    <Telerik:GridBoundColumn DataField="ProviderId" UniqueName="ProviderId" Visible="false" />

                    <Telerik:GridBoundColumn DataField="ProblemStatementText" UniqueName="ProblemStatementText" HeaderText="Problem Statement" />

                    <Telerik:GridBoundColumn DataField="CarePlanName" UniqueName="CarePlanName" HeaderText="Care Plan Name" ReadOnly="true" Visible="true" />
                    
                    <Telerik:GridBoundColumn DataField="ProviderName" UniqueName="ProviderName" HeaderText="Provider" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="NextObjectiveName" UniqueName="NextObjectiveName" HeaderText="Next Objective" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="NextInterventionName" UniqueName="NextInterventionName" HeaderText="Next Intervention" ReadOnly="true" Visible="true" />
                    
                    <Telerik:GridBoundColumn DataField="EffectiveDate" UniqueName="EffectiveDate" HeaderText="Effective" ReadOnly="true" Visible="true" />
                
                    <Telerik:GridBoundColumn DataField="TerminationDate" UniqueName="TerminationDate" HeaderText="Termination" ReadOnly="true" Visible="true" />

                </Columns>
            
            </Telerik:GridTableView>
                        
        </DetailTables>
        
    </MasterTableView>
    
    <ClientSettings>
    
        <Selecting AllowRowSelect="true" />
        
        <Scrolling AllowScroll="true" />
    
    </ClientSettings>

</Telerik:RadGrid>



<!-- DIALOG WINDOWS (BEGIN) -->

<Telerik:RadWindowManager ID="MemberCaseViewWindowManager" runat="server">

    <Windows>
            
        <Telerik:RadWindow ID="MemberCaseExistingWindow" Behaviors="Close" Modal="true" Width="425" Height="275" VisibleStatusbar="false"  Title="Existing Member Case found." Skin="Sunset" runat="server">

            <ContentTemplate>
                
                <div id="DialogContent">

                    <div style="margin: .125in" >

                        <p class="ColorDark" style="margin-left: .125in; margin-right: .125in; font-weight: bold">Existing Member Case found. Do you still want to create a new one?</p>
                            
                        <p>An existing member case was found that is in either open or in development status. Creating a new case will create multiple 

                        open cases, which can be used to divide out responsibilities per care team. Click "OK" to continue creating a new case.
                    
                        </p>
                        
                        <div style="height: 5px;"></div>
                                
                        <div class="BackgroundColorComplementNormal" style="margin-top: 5px; margin-bottom: 5px; padding-left: 5px; padding-left: 5px; height: 1px; width: 98%"></div>

                        <div style="height: 20px; padding: 0px 10px 0px 10px;">
   
                            <table cellpadding="0" cellspacing="0" border="0"><tr>

                                <td style="width: 100%;">&nbsp</td>
                                    
                                <td style="width: 80px; padding-right: 10px;"><asp:Button ID="MemberCaseExistingWindow_ButtonOk" Text="OK" OnClick="MemberCaseExistingWindow_ButtonOk_OnClick" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" runat="Server" /></td>
                
                                <td style="width: 80px; padding-right: 10px;"><asp:Button ID="MemberCaseExistingWindow_ButtonCancel" Text="Cancel" OnClientClick="return MemberCaseExistingWindow_Close ();" Width="73px" Font-Names="segoe ui, arial" Height="24" Font-Size="11px" runat="server" /></td> 

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


        function MemberCaseExistingWindow_Open() {

            var dialogWindow = $find("<%= MemberCaseExistingWindow.ClientID %>");

            dialogWindow.show();

            return false;

        }

        function MemberCaseExistingWindow_Close() {

            var dialogWindow = $find("<%= MemberCaseExistingWindow.ClientID %>");

            dialogWindow.close();

            return false;

        }


    </script>
        
</Telerik:RadCodeBlock>

<!-- DIALOG WINDOWS ( END ) -->