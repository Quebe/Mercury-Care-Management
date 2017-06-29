<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberClaimHistory.ascx.cs" Inherits="Mercury.Web.Application.Controls.MemberClaimHistory" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<div style="display: none"><asp:TextBox ID="UserControlInstanceId" Text="" runat="server" /></div>

<Telerik:RadAjaxManagerProxy ID="TelerikAjaxProxy" runat="server">

    <AjaxSettings>
    
        <Telerik:AjaxSetting AjaxControlID="MemberClaimHistoryGrid" ><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="MemberClaimHistoryGrid" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>
        
        <Telerik:AjaxSetting AjaxControlID="MemberPharmacyClaimHistoryGrid" ><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="MemberPharmacyClaimHistoryGrid" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>

    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>


<Telerik:RadTabStrip ID="ClaimHistoryTabStrip" MultiPageID="ClaimHistoryMultipage" SelectedIndex="0" ScrollChildren="true" ScrollButtonsPosition="Right" PerTabScrolling="true" runat="server">

    <Tabs>
    
        <Telerik:RadTab Text="Medical" Selected="True"></Telerik:RadTab>

        <Telerik:RadTab Text="Pharmacy"></Telerik:RadTab>

        <Telerik:RadTab Text="Lab"></Telerik:RadTab>
        
    </Tabs>
    
</Telerik:RadTabStrip>
        
        
<Telerik:RadMultiPage ID="ClaimHistoryMultipage" SelectedIndex="0" runat="server">

    <Telerik:RadPageView ID="PageMedical" runat="server">
                     
        <Telerik:RadGrid ID="MemberClaimHistoryGrid" EnableViewState="false" AllowPaging="true" AllowCustomPaging="true" 
        
            OnNeedDataSource="MemberClaimHistoryGrid_OnNeedDataSource" 
                       
            OnItemCommand="MemberClaimHistoryGrid_OnItemCommand" 
                       
            OnPageSizeChanged="MemberClaimHistoryGrid_OnPageSizeChanged"
            
            AutoGenerateColumns="false" runat="server">

            <MasterTableView Name="MemberClaimHistoryMasterView" TableLayout="Auto" DataKeyNames="ClaimId">
            
                <Columns>
            
                    <Telerik:GridBoundColumn DataField="ClaimId" HeaderText="ClaimId" Visible="false" />

                    <Telerik:GridBoundColumn DataField="ClaimNumber" HeaderText="Claim Number" />

                    <Telerik:GridBoundColumn DataField="MemberId" Visible="false" />

                    <Telerik:GridBoundColumn DataField="ServiceProviderId" Visible="false" />

                    <Telerik:GridBoundColumn DataField="PayToProviderId" Visible="false" />

                    <Telerik:GridBoundColumn DataField="ClaimForm" HeaderText="Form" Visible="true" />
                    
                    <Telerik:GridBoundColumn DataField="ClaimFromDate" HeaderText="From Date" Visible="true" />

                    <Telerik:GridBoundColumn DataField="ClaimThruDate" HeaderText="Thru Date" Visible="true" />
                    
                    <Telerik:GridBoundColumn DataField="AdmissionDate" HeaderText="Admission" Visible="true" />

                    <Telerik:GridBoundColumn DataField="Status" HeaderText="Status" Visible="true" />

                    <Telerik:GridBoundColumn DataField="BillType" HeaderText="Bill Type*" Visible="true" />
                    
                    <Telerik:GridBoundColumn DataField="PrimaryDiagnosisCode" HeaderText = "Primary Diagnosis*" Visible="true" />

                    <Telerik:GridBoundColumn DataField="BillingProviderName" HeaderText="Billing Provider" Visible="true" />

                    <Telerik:GridBoundColumn DataField="ServiceProviderName" HeaderText="Service Provider" Visible="true" />

                    <Telerik:GridBoundColumn DataField="BilledAmount" HeaderText="Billed Amount" DataType="System.Decimal" DataFormatString="{0:$###,###,###.##}" Visible="true" />

                    <Telerik:GridBoundColumn DataField="PaidAmount" HeaderText="Paid Amount" DataType="System.Decimal" DataFormatString="{0:$###,###,###.##}" Visible="true" />

                    <Telerik:GridBoundColumn DataField="PaidDate" HeaderText="Paid Date" Visible="true" />

                </Columns>
                
                <DetailTables>

                    <Telerik:GridTableView DataKeyNames="ClaimId" AllowPaging="false" BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid" Width="100%">
                    
                        <ParentTableRelation>
                        
                            <Telerik:GridRelationFields MasterKeyField="ClaimId" DetailKeyField="ClaimId" />
                            
                        </ParentTableRelation>
                        
                        <Columns>
                                                               
                            <Telerik:GridBoundColumn DataField="ClaimId" Visible="false" />

                            <Telerik:GridBoundColumn DataField="LineNumber" HeaderText="Line" Visible="true" />

                            <Telerik:GridBoundColumn DataField="ServiceDateFrom" HeaderText="Service From" Visible="true" />

                            <Telerik:GridBoundColumn DataField="ServiceDateThru" HeaderText="Service Thru" Visible="true" />

                            <Telerik:GridBoundColumn DataField="LineStatus" HeaderText="Status" Visible="true" />

                            <Telerik:GridBoundColumn DataField="LocationCode" HeaderText="Location" Visible="true" />

                            <Telerik:GridBoundColumn DataField="RevenueCode" HeaderText="Revenue*" Visible="true" />

                            <Telerik:GridBoundColumn DataField="RevenueDescription" HeaderText="Revenue Description" Visible="false" />

                            <Telerik:GridBoundColumn DataField="ServiceCode" HeaderText="Service*" Visible="true" />

                            <Telerik:GridBoundColumn DataField="ServiceDescription" HeaderText="Service Description" Visible="false" />

                            <Telerik:GridBoundColumn DataField="ModifierCode1" HeaderText="Modifier" Visible="true" />

                            <Telerik:GridBoundColumn DataField="Units" HeaderText="Units" Visible="true" />

                            <Telerik:GridBoundColumn DataField="DiagnosisCode1" HeaderText="Diagnosis" Visible="false" />
                            
                            <Telerik:GridBoundColumn DataField="DiagnosisDescription1" HeaderText="Diagnosis Description" Visible="false" />

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

    </Telerik:RadPageView>

    <Telerik:RadPageView ID="PagePharmacy" runat="server">
    
        <Telerik:RadGrid ID="MemberPharmacyClaimHistoryGrid" AllowPaging="true" AllowCustomPaging="true" EnableViewState="false" 
        
            OnNeedDataSource="MemberPharmacyClaimHistoryGrid_OnNeedDataSource" 
            
            OnPageSizeChanged="MemberPharmacyClaimHistoryGrid_OnPageSizeChanged"
            
            AutoGenerateColumns="false" runat="server">

            <MasterTableView Name="MemberPharmacyClaimHistoryMasterView" TableLayout="Auto" DataKeyNames="ClaimId">
            
                <Columns>
            
                    <Telerik:GridBoundColumn DataField="ClaimId" HeaderText="ClaimId" Visible="false" />

                    <Telerik:GridBoundColumn DataField="ExternalClaimId" HeaderText="External Claim Id" />

                    <Telerik:GridBoundColumn DataField="MemberId" Visible="false" />

                    <Telerik:GridBoundColumn DataField="ClaimType" HeaderText="Type" Visible="false" />
                    
                    <Telerik:GridBoundColumn DataField="ClaimDateFrom" HeaderText="Date" Visible="true" />

                    <Telerik:GridBoundColumn DataField="ClaimDateThru" HeaderText="Thru Date" Visible="false" />
                    
                    <Telerik:GridBoundColumn DataField="Status" HeaderText="Status" Visible="false" />

                    <Telerik:GridBoundColumn DataField="NdcCode" HeaderText="NDC" Visible="true" />
                    
                    <Telerik:GridBoundColumn DataField="DrugName" HeaderText="Name" Visible="true" />

                    <Telerik:GridBoundColumn DataField="DaysSupply" HeaderText="Days Supply" Visible="true" />
                    
                    <Telerik:GridBoundColumn DataField="DeaClassification" HeaderText="DEA" Visible="true" />

                    <Telerik:GridBoundColumn DataField="TherapeuticClassification" HeaderText="Therapeutic" Visible="true" />

                    <Telerik:GridBoundColumn DataField="PharmacyName" HeaderText="Pharmacy" Visible="true" />

                    <Telerik:GridBoundColumn DataField="ServiceProviderName" HeaderText="Provider" Visible="true" />

                    <Telerik:GridBoundColumn DataField="ServiceProviderSpecialtyName" HeaderText="Specialty" Visible="false" />

                    <Telerik:GridBoundColumn DataField="BilledAmount" HeaderText="Billed Amount" DataType="System.Decimal" DataFormatString="{0:$###,###,###.##}" Visible="true" />

                    <Telerik:GridBoundColumn DataField="PaidAmount" HeaderText="Paid Amount" DataType="System.Decimal" DataFormatString="{0:$###,###,###.##}" Visible="true" />

                </Columns>
                            
            </MasterTableView>
            
            <ClientSettings>
            
                <Selecting AllowRowSelect="true" />
                
                <Scrolling AllowScroll="true" />
            
            </ClientSettings>
            
            <PagerStyle NextPageText="Next" PrevPageText="Previous"></PagerStyle>

        </Telerik:RadGrid>
    
    </Telerik:RadPageView>
    
    <Telerik:RadPageView ID="RadPageView2" runat="server">
    
    
    </Telerik:RadPageView>
    
</Telerik:RadMultiPage>
