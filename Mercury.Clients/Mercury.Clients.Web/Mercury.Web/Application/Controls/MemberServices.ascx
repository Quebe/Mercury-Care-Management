<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberServices.ascx.cs" Inherits="Mercury.Web.Application.Controls.MemberServices" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<div style="display: none"><asp:TextBox ID="UserControlInstanceId" Text="" runat="server" /></div>

<Telerik:RadAjaxManagerProxy ID="TelerikAjaxProxy" runat="server">

    <AjaxSettings>
    
        <Telerik:AjaxSetting AjaxControlID="MemberServiceToolbar" ><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="MemberServicesGrid" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>
        
        <Telerik:AjaxSetting AjaxControlID="MemberServicesGrid" ><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="MemberServicesGrid" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>
        
        <Telerik:AjaxSetting AjaxControlID="MemberServiceAction" ><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="MemberServicesGrid" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>

    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>
    

<Telerik:RadGrid ID="MemberServicesGrid" Height="400" AllowPaging="true" AllowCustomPaging="true" AutoGenerateColumns="false" EnableViewState="false"

    OnNeedDataSource="MemberServicesGrid_OnNeedDataSource" 
    
    OnItemDataBound="MemberServicesGrid_OnItemDataBound" 
    
    OnItemCreated="MemberServicesGrid_OnItemCreated"
    
    OnItemCommand="MemberServicesGrid_OnItemCommand"
    
    OnPageSizeChanged="MemberServicesGrid_OnPageSizeChanged"
    
    runat="server">

    <MasterTableView Name="MemberServicesView" TableLayout="Auto" CommandItemDisplay="Top" DataKeyNames="MemberServiceId,AddedManually">
     
        <CommandItemTemplate>
        
            <div>
                                         
                <Telerik:RadToolBar ID="MemberServiceToolbar" OnButtonClick="MemberServiceToolbar_OnButtonClick" EnableViewState="false" AutoPostBack="true" runat="server">
                    
                    <Items>
                    
                        <Telerik:RadToolBarButton Text="Show Hidden" CheckOnClick="true" AllowSelfUnCheck="true" Group="ShowHidden" Checked="false" PostBack="true"  />
                        
                        <Telerik:RadToolBarButton IsSeparator="true" />
                        
                        <Telerik:RadToolBarButton BorderStyle="None">
                        
                            <ItemTemplate>
                                                                    
                                <table cellpadding="0" cellspacing="0" border="0" style="border: none; padding: 0px"><tr>
                                
                                    <td style="width: 16px"><img src="/Images/Common16/ServiceAdd.png" alt="Add Service" /></td>
                                    
                                    <td style="width: 65px;">Add Service:</td>
                                    
                                    <td style="width: 300px"><Telerik:RadComboBox ID="MemberServiceSelection" Width="300" runat="server" /></td>
                                    
                                    <td style="width: 30px;">Date:</td>
                                    
                                    <td style="width: 70px"><Telerik:RadDateInput ID="MemberServiceEventDate" Width="70" runat="server" /></td>

                                    <td><asp:Button ID="MemberServiceAdd" Text="Add" CommandName="MemberServiceAdd" CommandArgument="0" Width="73px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>
                                
                                </tr></table>
                            
                            </ItemTemplate>
                        
                        </Telerik:RadToolBarButton>                                             
                        
                    </Items>

                </Telerik:RadToolBar>
   
            </div>
    
        </CommandItemTemplate>
    
        <Columns>
    
            <Telerik:GridBoundColumn DataField="MemberServiceId" UniqueName="MemberServiceId" HeaderText="Member Service Id" ReadOnly="true" Visible="true" />

            <Telerik:GridBoundColumn DataField="EventDate" UniqueName="EventDate" HeaderText="Event Date" ReadOnly="true" Visible="true" />

            <Telerik:GridBoundColumn DataField="ServiceName" UniqueName="ServiceName" HeaderText="Name" ReadOnly="true" Visible="true" />

            <Telerik:GridBoundColumn DataField="ServiceType" UniqueName="ServiceType" HeaderText="Type" ReadOnly="true" Visible="true" />
        
            <Telerik:GridBoundColumn DataField="AddedManually" UniqueName="AddedManually" HeaderText="Manual" ReadOnly="true" Visible="true" />

            <Telerik:GridBoundColumn DataField="CreateAccountName" UniqueName="CreateAccountName" HeaderText="Created By" ReadOnly="true" Visible="true" />

            <Telerik:GridBoundColumn DataField="CreateDate" UniqueName="CreateDate" HeaderText="Create Date" ReadOnly="true" Visible="true" />

        </Columns>
        
        <DetailTables>
        
            <Telerik:GridTableView DataKeyNames="MemberServiceId" AllowPaging="false" BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid" ShowHeadersWhenNoRecords="false"  Width="100%">
            
                <ParentTableRelation>
                
                    <Telerik:GridRelationFields MasterKeyField="MemberServiceId" DetailKeyField="MemberServiceId" />
                    
                </ParentTableRelation>
                
                <Columns>
                                                       
                    <Telerik:GridBoundColumn DataField="MemberServiceId" UniqueName="MemberServiceId" HeaderText="Member Service Id" ReadOnly="true" Visible="false" />

                    <Telerik:GridBoundColumn DataField="DefinitionId" UniqueName="DefinitionId" HeaderText="Definition Id" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="EventDate" UniqueName="EventDate" HeaderText="Event Date" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="ClaimId" UniqueName="ClaimId" HeaderText="Claim Id" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="ClaimLine" UniqueName="ClaimLine" HeaderText="Line" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="ClaimType" UniqueName="ClaimType" HeaderText="Type" ReadOnly="true" Visible="true" />


                    <Telerik:GridBoundColumn DataField="BillType" UniqueName="BillType" HeaderText="Bill Type*" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="PrincipalDiagnosisCode" UniqueName="PrincipalDiagnosisCode" HeaderText="Principal*" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="DiagnosisCode" UniqueName="DiagnosisCode" HeaderText="Diagnosis*" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="Icd9ProcedureCode" UniqueName="Icd9ProcedureCode" HeaderText="ICD-9 Proc*" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="LocationCode" UniqueName="LocationCode" HeaderText="Location" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="RevenueCode" UniqueName="RevenueCode" HeaderText="Revenue*" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="ProcedureCode" UniqueName="ProcedureCode" HeaderText="Procedure*" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="ModifierCode" UniqueName="ModifierCode" HeaderText="Modifier" ReadOnly="true" Visible="true" />
                    
                    
                    <Telerik:GridBoundColumn DataField="SpecialtyName" UniqueName="SpecialtyName" HeaderText="Specialty" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="IsPcpClaim" UniqueName="IsPcpClaim" HeaderText="PCP Claim" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="NdcCode" UniqueName="NdcCode" HeaderText="NDC" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="Units" UniqueName="Units" HeaderText="Units" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="TherapeuticClassification" UniqueName="TherapeuticClassification" HeaderText="Therapeutic" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="LabLoincCode" UniqueName="LabLoincCode" HeaderText="LOINC" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="LabValue" UniqueName="LabValue" HeaderText="Lab Value" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="Description" UniqueName="Description" HeaderText="Description" ReadOnly="true" Visible="true" />

                </Columns>
                
                <NoRecordsTemplate></NoRecordsTemplate>
            
            </Telerik:GridTableView>
            
            <Telerik:GridTableView DataKeyNames="MemberServiceId" AllowPaging="false" BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid" ShowHeadersWhenNoRecords="false" Width="100%">
            
                <ParentTableRelation>
                
                    <Telerik:GridRelationFields MasterKeyField="MemberServiceId" DetailKeyField="MemberServiceId" />
                    
                </ParentTableRelation>
                
                <Columns>
                                                       
                    <Telerik:GridBoundColumn DataField="MemberServiceId" UniqueName="MemberServiceId" HeaderText="Member Service Id" ReadOnly="true" Visible="false" />

                    <Telerik:GridBoundColumn DataField="DefinitionId" UniqueName="DefinitionId" HeaderText="Definition Id" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="DetailMemberServiceId" UniqueName="DetailMemberServiceId" HeaderText="Detail Member Service Id" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="EventDate" UniqueName="EventDate" HeaderText="Event Date" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="ServiceName" UniqueName="ServiceName" HeaderText="Service Name" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="ServiceType" UniqueName="ServiceType" HeaderText="Service Type" ReadOnly="true" Visible="true" />

                </Columns>
            
                <NoRecordsTemplate></NoRecordsTemplate>
                
            </Telerik:GridTableView>
        
        </DetailTables>
    
    </MasterTableView>
    
    <ClientSettings>
    
        <Selecting AllowRowSelect="true" />
        
        <Scrolling AllowScroll="true" />
    
    </ClientSettings>
    
    <PagerStyle NextPageText="Next" PrevPageText="Previous"></PagerStyle>

</Telerik:RadGrid>


<div style="display: none">

    <asp:TextBox ID="MemberServiceAction_MemberServiceId" runat="server" />
    
    <asp:TextBox ID="MemberServiceAction_CommandName" Text="No Command" runat="server" />
    
    <asp:TextBox ID="MemberServiceAction_Arguments" runat="server" />
    
    <asp:Button  ID="MemberServiceAction" OnClick="MemberServiceAction_OnClick" runat="server" />
    
</div>


<div id="Div1" style="display: none;" runat="server">

<script type="text/javascript">

    function MemberServices_MemberService_OnRemoveManual_<%= MemberServiceAction_MemberServiceId.ClientID.Replace ('.', '_') %> (memberServiceId, serviceName, eventDate) {

        var userConfirmed = confirm("Remove Manual Service: " + serviceName + " on " + eventDate + "?");

        if (userConfirmed) {

            document.getElementById("<%= MemberServiceAction_MemberServiceId.ClientID %>").value = memberServiceId;

            document.getElementById("<%= MemberServiceAction_CommandName.ClientID %>").value = "RemoveManual";

            document.getElementById("<%= MemberServiceAction_Arguments.ClientID %>").value = memberServiceId;

            document.getElementById("<%= MemberServiceAction.ClientID %>").click();

        }

        return;

    }

</script>                         

</div>                