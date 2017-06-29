<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberAuthorizationHistory.ascx.cs" Inherits="Mercury.Web.Application.Controls.MemberAuthorizationHistory" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<div style="display: none"><asp:TextBox ID="UserControlInstanceId" Text="" runat="server" /></div>

<Telerik:RadAjaxManagerProxy ID="TelerikAjaxProxy" runat="server">

    <AjaxSettings>
    
        <Telerik:AjaxSetting AjaxControlID="MemberAuthorizationHistoryGrid" ><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="MemberAuthorizationHistoryGrid" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>
        
        <Telerik:AjaxSetting AjaxControlID="MemberPharmacyAuthorizationHistoryGrid" ><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="MemberPharmacyAuthorizationHistoryGrid" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>

    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>
            
<Telerik:RadGrid ID="MemberAuthorizationHistoryGrid" EnableViewState="false" AllowPaging="true" AllowCustomPaging="true" 

    OnNeedDataSource="MemberAuthorizationHistoryGrid_OnNeedDataSource" 
               
    OnItemCommand="MemberAuthorizationHistoryGrid_OnItemCommand" 
               
    OnPageSizeChanged="MemberAuthorizationHistoryGrid_OnPageSizeChanged"
    
    AutoGenerateColumns="false" runat="server">

    <MasterTableView Name="MemberAuthorizationHistoryMasterView" TableLayout="Auto" DataKeyNames="AuthorizationId">
    
        <Columns>
    
            <Telerik:GridBoundColumn DataField="AuthorizationId" HeaderText="AuthorizationId" Visible="false" />

            <Telerik:GridBoundColumn DataField="AuthorizationNumber" HeaderText="Number" />

            <Telerik:GridBoundColumn DataField="MemberId" Visible="false" />

            <Telerik:GridBoundColumn DataField="AuthorizationType" HeaderText="Authorization Type" Visible="true" />
            
            <Telerik:GridBoundColumn DataField="EffectiveDate" HeaderText="Effective" Visible="true" />

            <Telerik:GridBoundColumn DataField="TerminationDate" HeaderText="Termination" Visible="true" />
            
            <Telerik:GridBoundColumn DataField="AuthorizationStatus" HeaderText="Status" Visible="true" />
                        
            <Telerik:GridBoundColumn DataField="PrincipalDiagnosisCode" HeaderText = "Principal*" Visible="true" />

            <Telerik:GridBoundColumn DataField="AdmittingDiagnosisCode" HeaderText = "Admitting*" Visible="false" />

            <Telerik:GridBoundColumn DataField="DischargeDiagnosisCode" HeaderText = "Discharge*" Visible="false" />
            
            <Telerik:GridBoundColumn DataField="ReferringProvider" HeaderText="Referring Provider" Visible="true" />

            <Telerik:GridBoundColumn DataField="ServiceProvider" HeaderText="Service Provider" Visible="true" />
            
            <Telerik:GridBoundColumn DataField="ServiceProviderSpecialtyName" HeaderText="Service Provider Specialty" Visible="true" />
            
            <Telerik:GridBoundColumn DataField="AssignedToUserAccountName" HeaderText="Assigned To" Visible="true" />
            
            <Telerik:GridBoundColumn DataField="ModifiedBy" HeaderText = "Modified By" Visible="true" />

            <Telerik:GridBoundColumn DataField="ModifiedDate" HeaderText = "Modified Date" Visible="true" />

        </Columns>
        
        <DetailTables>

            <Telerik:GridTableView DataKeyNames="AuthorizationId" AllowPaging="false" BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid" Width="100%">
            
                <ParentTableRelation>
                
                    <Telerik:GridRelationFields MasterKeyField="AuthorizationId" DetailKeyField="AuthorizationId" />
                    
                </ParentTableRelation>
                
                <Columns>
                                                       
                    <Telerik:GridBoundColumn DataField="AuthorizationId" Visible="false" />

                    <Telerik:GridBoundColumn DataField="LineNumber" HeaderText="Line" Visible="true" />

                    <Telerik:GridBoundColumn DataField="LineStatus" HeaderText="Status" Visible="true" />
                    
                    <Telerik:GridBoundColumn DataField="ServiceDate" HeaderText="Service Date" Visible="true" />

                    <Telerik:GridBoundColumn DataField="AdmissionDate" HeaderText="Admission Date" Visible="true" />

                    <Telerik:GridBoundColumn DataField="DischargeDate" HeaderText="Discharge Date" Visible="true" />

                    <Telerik:GridBoundColumn DataField="RevenueCode" HeaderText="Revenue*" Visible="true" />

                    <Telerik:GridBoundColumn DataField="RevenueDescription" HeaderText="Revenue Description" Visible="false" />

                    <Telerik:GridBoundColumn DataField="ServiceCode" HeaderText="Service*" Visible="true" />

                    <Telerik:GridBoundColumn DataField="ServiceDescription" HeaderText="Service Description" Visible="false" />

                    <Telerik:GridBoundColumn DataField="ModifierCode1" HeaderText="Modifier" Visible="true" />

                    <Telerik:GridBoundColumn DataField="UtilizedUnits" HeaderText="Utilized Units" Visible="true" />

                    <Telerik:GridBoundColumn DataField="Units" HeaderText="Service Units" Visible="true" />

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
