<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberAuthorizedServices.ascx.cs" Inherits="Mercury.Web.Application.Controls.MemberAuthorizedServices" %>


<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<div style="display: none"><asp:TextBox ID="UserControlInstanceId" Text="" runat="server" /></div>

<Telerik:RadAjaxManagerProxy ID="TelerikAjaxProxy" runat="server">

    <AjaxSettings>
    
        <Telerik:AjaxSetting AjaxControlID="MemberAuthorizedServiceToolbar" ><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="MemberAuthorizedServicesGrid" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>
        
        <Telerik:AjaxSetting AjaxControlID="MemberAuthorizedServicesGrid" ><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="MemberAuthorizedServicesGrid" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>
        
        <Telerik:AjaxSetting AjaxControlID="MemberAuthorizedServiceAction" ><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="MemberAuthorizedServicesGrid" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>

    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>
    

<Telerik:RadGrid ID="MemberAuthorizedServicesGrid" Height="400" AllowPaging="true" AllowCustomPaging="true" AutoGenerateColumns="false" EnableViewState="false"

    OnItemCreated="MemberAuthorizedServicesGrid_OnItemCreated"    

    OnNeedDataSource="MemberAuthorizedServicesGrid_OnNeedDataSource" 
    
    OnItemDataBound="MemberAuthorizedServicesGrid_OnItemDataBound" 
        
    OnItemCommand="MemberAuthorizedServicesGrid_OnItemCommand"
    
    OnPageSizeChanged="MemberAuthorizedServicesGrid_OnPageSizeChanged"
    
    runat="server">

    <MasterTableView Name="MemberAuthorizedServicesView" TableLayout="Auto" CommandItemDisplay="Top" DataKeyNames="MemberAuthorizedServiceId,AddedManually">
     
        <CommandItemTemplate>
        
            <div>
                                         
                <Telerik:RadToolBar ID="MemberAuthorizedServiceToolbar" OnButtonClick="MemberAuthorizedServiceToolbar_OnButtonClick" EnableViewState="false" AutoPostBack="true" runat="server">
                    
                    <Items>
                    
                        <Telerik:RadToolBarButton Text="Show Hidden" CheckOnClick="true" AllowSelfUnCheck="true" Group="ShowHidden" Checked="false" PostBack="true"  />
                        
                    </Items>

                </Telerik:RadToolBar>
   
            </div>
    
        </CommandItemTemplate>
    
        <Columns>
    
            <Telerik:GridBoundColumn DataField="MemberAuthorizedServiceId" UniqueName="MemberAuthorizedServiceId" HeaderText="Member AuthorizedService Id" ReadOnly="true" Visible="true" />

            <Telerik:GridBoundColumn DataField="AuthorizedServiceName" UniqueName="AuthorizedServiceName" HeaderText="Name" ReadOnly="true" Visible="true" />

            <Telerik:GridBoundColumn DataField="EventDate" UniqueName="EventDate" HeaderText="Event Date" ReadOnly="true" Visible="true" />

            <Telerik:GridBoundColumn DataField="InitialIdentifiedDate" UniqueName="InitialIdentifiedDate" HeaderText="Initial Identified Date" ReadOnly="true" Visible="true" />
        
            <Telerik:GridBoundColumn DataField="AddedManually" UniqueName="AddedManually" HeaderText="Manual" ReadOnly="true" Visible="true" />

            <Telerik:GridBoundColumn DataField="CreateAccountName" UniqueName="CreateAccountName" HeaderText="Created By" ReadOnly="true" Visible="true" />

            <Telerik:GridBoundColumn DataField="CreateDate" UniqueName="CreateDate" HeaderText="Create Date" ReadOnly="true" Visible="true" />

        </Columns>
        
        <DetailTables>
        
            <Telerik:GridTableView DataKeyNames="MemberAuthorizedServiceId" AllowPaging="false" BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid" ShowHeadersWhenNoRecords="false"  Width="100%">
            
                <ParentTableRelation>
                
                    <Telerik:GridRelationFields MasterKeyField="MemberAuthorizedServiceId" DetailKeyField="MemberAuthorizedServiceId" />
                    
                </ParentTableRelation>
                
                <Columns>
                                                       
                    <Telerik:GridBoundColumn DataField="MemberAuthorizedServiceId" UniqueName="MemberAuthorizedServiceId" HeaderText="Member AuthorizedService Id" ReadOnly="true" Visible="false" />

                    <Telerik:GridBoundColumn DataField="AuthorizedServiceDefinitionId" UniqueName="AuthorizedServiceDefinitionId" HeaderText="Definition Id" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="EventDate" UniqueName="EventDate" HeaderText="Event Date" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="AuthorizationId" UniqueName="AuthorizationId" HeaderText="Authorization Id" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="AuthorizationNumber" UniqueName="AuthorizationNumber" HeaderText="Authorization Number" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="ExternalAuthorizationId" UniqueName="ExternalAuthorizationId" HeaderText="External Authorization Id" ReadOnly="true" Visible="true" />


                    <Telerik:GridBoundColumn DataField="AuthorizationLine" UniqueName="AuthorizationLine" HeaderText="Line" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="MemberId" UniqueName="MemberId" HeaderText="MemberId" ReadOnly="true" Visible="true" />


                    <Telerik:GridBoundColumn DataField="ReferringProviderId" UniqueName="ReferringProviderId" HeaderText="Referring Provider Id" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="ServiceProviderId" UniqueName="ServiceProviderId" HeaderText="Service Provider Id" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="Category" UniqueName="Category" HeaderText="Category" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="Subcategory" UniqueName="Subcategory" HeaderText="Subcategory" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="ServiceType" UniqueName="ServiceType" HeaderText="Service Type" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="Status" UniqueName="Status" HeaderText="Status" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="ReceviedDate" UniqueName="ReceviedDate" HeaderText="Recevied Date" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="ReferralDate" UniqueName="ReferralDate" HeaderText="Referral Date" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="EffectiveDate" UniqueName="EffectiveDate" HeaderText="Effective Date" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="TerminationDate" UniqueName="TerminationDate" HeaderText="Termination Date" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="ServiceDate" UniqueName="ServiceDate" HeaderText="Service Date" ReadOnly="true" Visible="true" />


                    <Telerik:GridBoundColumn DataField="DiagnosisCode" UniqueName="DiagnosisCode" HeaderText="Diagnosis*" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="RevenueCode" UniqueName="RevenueCode" HeaderText="Revenue*" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="ProcedureCode" UniqueName="ProcedureCode" HeaderText="Procedure*" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="ModifierCode" UniqueName="ModifierCode" HeaderText="Modifier" ReadOnly="true" Visible="true" />
                    
                    
                    <Telerik:GridBoundColumn DataField="SpecialtyName" UniqueName="SpecialtyName" HeaderText="Specialty" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="NdcCode" UniqueName="NdcCode" HeaderText="NDC" ReadOnly="true" Visible="true" />

                    <Telerik:GridBoundColumn DataField="Description" UniqueName="Description" HeaderText="Description" ReadOnly="true" Visible="true" />

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

    <asp:TextBox ID="MemberAuthorizedServiceAction_MemberAuthorizedServiceId" runat="server" />
    
    <asp:TextBox ID="MemberAuthorizedServiceAction_CommandName" Text="No Command" runat="server" />
    
    <asp:TextBox ID="MemberAuthorizedServiceAction_Arguments" runat="server" />
    
    <asp:Button  ID="MemberAuthorizedServiceAction" runat="server" />
    
</div>


<div id="Div1" style="display: none;" runat="server">

<script type="text/javascript">

    function MemberAuthorizedServices_MemberAuthorizedService_OnRemoveManual_<%= MemberAuthorizedServiceAction_MemberAuthorizedServiceId.ClientID.Replace ('.', '_') %> (memberAuthorizedServiceId, serviceName, eventDate) {

        var userConfirmed = confirm("Remove Manual AuthorizedService: " + serviceName + " on " + eventDate + "?");

        if (userConfirmed) {

            document.getElementById("<%= MemberAuthorizedServiceAction_MemberAuthorizedServiceId.ClientID %>").value = memberAuthorizedServiceId;

            document.getElementById("<%= MemberAuthorizedServiceAction_CommandName.ClientID %>").value = "RemoveManual";

            document.getElementById("<%= MemberAuthorizedServiceAction_Arguments.ClientID %>").value = memberAuthorizedServiceId;

            document.getElementById("<%= MemberAuthorizedServiceAction.ClientID %>").click();

        }

        return;

    }

</script>                         

</div>                