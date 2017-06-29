<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberCaseCarePlanIntervention.ascx.cs" Inherits="Mercury.Web.Application.MemberCase.MemberCaseCarePlanIntervention" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<%@ Register TagPrefix="MercuryUserControl" TagName="MemberCaseCarePlanInterventionActivity" Src="~/Application/MemberCase/MemberCaseCarePlanInterventionActivity.ascx" %>


<div style="margin: .125in; border: 1px solid #215485">

    <div class="PropertyPageSectionTitleComplement" style="margin-top: 0px; padding: 0px;">

        <Telerik:RadGrid ID="CareInterventionAssociationsGrid" 
        
                OnNeedDataSource="CareInterventionAssociationsGrid_OnNeedDataSource"

                AutoGenerateColumns="false" Skin="Office2007" runat="server">

            <MasterTableView CommandItemDisplay="None" DataKeyNames="">

                <Columns>
        
                    <Telerik:GridBoundColumn DataField="Id" HeaderText="Id" Visible="false"></Telerik:GridBoundColumn>

                    <Telerik:GridBoundColumn DataField="MemberCaseCarePlanGoal.MemberCaseCarePlan.ProblemsDescription" HeaderText="Associated with Care Plans"></Telerik:GridBoundColumn>

                    <Telerik:GridBoundColumn DataField="MemberCaseCarePlanGoal.Name" HeaderText="Goal"></Telerik:GridBoundColumn>

                    <Telerik:GridBoundColumn DataField="Inclusion" HeaderText="Inclusion"></Telerik:GridBoundColumn>

                    <Telerik:GridBoundColumn DataField="IsSingleInstance" HeaderText="Is Single Instance"></Telerik:GridBoundColumn>

                </Columns>                                                                  
                                         
            </MasterTableView>

            <ClientSettings>
                                
                <Scrolling AllowScroll="false" UseStaticHeaders="true" />
                                
            </ClientSettings>

        </Telerik:RadGrid>

    </div>
    
</div>

<div style="margin: .125in; border: 1px solid #215485">

    <div class="PropertyPageSectionTitleComplement" style="margin-top: 0px; padding: 0px;">

        <Telerik:RadGrid ID="CarePlanInterventionActivitiesGrid" 
        
                OnItemCommand="CarePlanInterventionActivitiesGrid_OnItemCommand"

                OnNeedDataSource="CarePlanInterventionActivitiesGrid_OnNeedDataSource"

                OnItemCreated="CarePlanInterventionActivitiesGrid_OnItemCreated"

                AutoGenerateColumns="false" runat="server">

            <MasterTableView CommandItemDisplay="Bottom" DataKeyNames="">

                <Columns>
        
                    <Telerik:GridBoundColumn DataField="InterventionActivityDescriptionHtml" HeaderText="Intervention Activity Description"></Telerik:GridBoundColumn>

                    <Telerik:GridBoundColumn DataField="AnchorDescription" HeaderText="Anchor"></Telerik:GridBoundColumn>

                    <Telerik:GridBoundColumn DataField="ScheduleDescription" HeaderText="Schedule"></Telerik:GridBoundColumn>

                    <Telerik:GridBoundColumn DataField="ThresholdsDescription" HeaderText="Thresholds"></Telerik:GridBoundColumn>
                    
                    <Telerik:GridButtonColumn HeaderStyle-Width="60" ItemStyle-Width="60" HeaderText="Action" CommandName="Edit" Text="(edit)" ConfirmText="Are you sure you want to edit this Intervention?"></Telerik:GridButtonColumn>

                    <Telerik:GridButtonColumn HeaderStyle-Width="60" ItemStyle-Width="60" HeaderText="Action" CommandName="Delete" Text="(delete)" ConfirmText="Are you sure you want to delete this Intervention?"></Telerik:GridButtonColumn>
                                                                   
                </Columns>                                                                  
                                
                <EditFormSettings EditFormType="Template">
                                
                    <FormTemplate>
                                    
                        <MercuryUserControl:MemberCaseCarePlanInterventionActivity ID="MemberCaseCarePlanInterventionActivityControl" runat="server" />
   
                                    
                    </FormTemplate>

                </EditFormSettings>
                                                        
            </MasterTableView>

            <ClientSettings>
                                
                <Scrolling AllowScroll="false" UseStaticHeaders="true" />
                                
            </ClientSettings>

        </Telerik:RadGrid>

    </div>
    
</div>

