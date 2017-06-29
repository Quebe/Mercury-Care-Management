<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberCaseCarePlanInterventionActivity.ascx.cs" Inherits="Mercury.Web.Application.MemberCase.MemberCaseCarePlanInterventionActivity" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


                    <div id="ActivityPropertiesContent" style="margin: 0px 0px 0px 0px; padding: 0px; line-height: 150%;">
                    
                        <div style=" border: solid 0px black">
                        
                            <Telerik:RadTabStrip ID="ActivitiesTabStrip" MultiPageID="ActivitiesMultiPage" SelectedIndex="0" runat="server">
                            
                                <Tabs>
                                
                                    <Telerik:RadTab Text="Activity"></Telerik:RadTab>

                                    <Telerik:RadTab Text="Narrative"></Telerik:RadTab>

                                    <Telerik:RadTab Text="Thresholds"></Telerik:RadTab>
                                    
                                </Tabs>
                                      
                            </Telerik:RadTabStrip>
                            
                            <div style="margin: 0px 0px 0px 0px; height: 210px; line-height: 150%; overflow: hidden;">
                         
                                <Telerik:RadMultiPage ID="ActivitiesMultiPage" SelectedIndex="0" runat="server">
                                
                                    <Telerik:RadPageView ID="PageActivityDefinition" runat="server">

                                        <div style="margin: 2px 10px 2px 10px; padding: 4px;">
                                        
                                            <table width="100%" cellpadding="4" cellspacing="0" border="0"><tr>
                                        
                                                <td style="width: 08%;">Type:</td>

                                                <td style="width: 12%;">

                                                    <Telerik:RadComboBox ID="CareInterventionActivityType" OnSelectedIndexChanged="CareInterventionActivityType_OnSelectedIndexChanged" AutoPostBack="true" Width="100%" runat="server">

                                                        <Items>
                                                        
                                                            <Telerik:RadComboBoxItem Text="Intervention" Value="0" Selected="true" />

                                                            <Telerik:RadComboBoxItem Text="Member Task" Value="1" />
                                                        
                                                        </Items>
                                                    
                                                    </Telerik:RadComboBox>
                                                
                                                </td>

                                                <td style="width: 08%;">Action:</td>

                                                <td style="width: 12%;">
                                            
                                                    <Telerik:RadComboBox ID="ActivityTypeSelection" OnSelectedIndexChanged="ActivityTypeSelection_OnSelectedIndexChanged" AutoPostBack="true" Width="100%" runat="server">

                                                        <Items>
                                                    
                                                            <Telerik:RadComboBoxItem Text="Manual" Value="0" />

                                                            <Telerik:RadComboBoxItem Text="Automation" Value="1" Enabled="false" />

                                                            <Telerik:RadComboBoxItem Text="Workflow" Value="2" />

                                                            <Telerik:RadComboBoxItem Text="Monitor" Value="3" Enabled="false" Visible="false" />
                                                    
                                                        </Items>
                                            
                                                    </Telerik:RadComboBox>
                                                
                                                </td>

                                                <td style="width: 08%; text-align: center;">Activity:</td>
                                            
                                                <td>
                                            
                                                    <Telerik:RadComboBox ID="ActivityActionSelection" OnSelectedIndexChanged="ActivityActionSelection_OnSelectedIndexChanged" AutoPostBack="true" Width="100%" Enabled="false" runat="server"></Telerik:RadComboBox>
                                                
                                                </td>

                                            </tr></table>

                                            <div id="ActivityTypeManualNameDescription" style="" runat="server">

                                                <table width="100%" cellpadding="4" cellspacing="0" border="0">

                                                    <tr>
                                                
                                                        <td style="width: 08%;">Name:</td>
                    
                                                        <td style="width: 92%;"><Telerik:RadTextBox ID="ActivityName" Width="100%" MaxLength="60" EmptyMessage="(optional)" runat="server" /></td>
                    
                                                    </tr>

                                                    <tr style="display: none;">
                                                
                                                        <td valign="top" style="width: 08%;">Description:</td>
                    
                                                        <td style="width: 92%;"><Telerik:RadTextBox ID="ActivityDescription" Width="100%" Rows="2" TextMode="MultiLine" EmptyMessage="(optional)" runat="server" /></td>

                                                    </tr>
                                            
                                                </table>
                    
                                            </div>                                                               
                                            
                                            <table width="100%" cellpadding="4" cellspacing="0" border="0"><tr>
                                        
                                                <td style="width: 08%">Schedule:</td>

                                                <td style="width: 15%">

                                                    <Telerik:RadComboBox ID="ActivityScheduleTypeSelection" 
                                                    
                                                        Width="99%" runat="server">
                                    
                                                        <Items>

                                                            <Telerik:RadComboBoxItem Value="0" Text="By Frequency" />
                                            
                                                            <Telerik:RadComboBoxItem Value="1" Text="Monthly"  />
                                        
                                                            <Telerik:RadComboBoxItem Value="2" Text="Quarterly"  />

                                                            <Telerik:RadComboBoxItem Value="3" Text="Yearly"  />

                                                            <Telerik:RadComboBoxItem Value="4" Text="Birth Month"  />

                                                            <Telerik:RadComboBoxItem Value="5" Text="By Calendar Month"  />

                                                        </Items>
                                    
                                                    </Telerik:RadComboBox>
                                    
                                                </td>

                                                <td style="width: 05%">Value:</td>

                                                <td style="width: 05%"><Telerik:RadNumericTextBox ID="ActivityScheduleValue" Width="98%" NumberFormat-DecimalDigits="0" MinValue="0" Value="0" runat="server" /></td>

                                                <td style="width: 08%">
                                                
                                                    <Telerik:RadComboBox ID="ActivityScheduleQualifierSelection" Width="99%" runat="server">
                                    
                                                        <Items>
                                        
                                                            <Telerik:RadComboBoxItem Value="0" Text="Days" Selected="true" />
                                            
                                                            <Telerik:RadComboBoxItem Value="1" Text="Months" />
                                            
                                                            <Telerik:RadComboBoxItem Value="2" Text="Years" />
                                        
                                                        </Items>
                                    
                                                    </Telerik:RadComboBox>

                                                </td>

                                                <td style="width: 15%"><asp:CheckBox ID="ActivityReoccurring" Text="Reoccurring" Checked="false" runat="server" /></td>

                                                <td style="width: 20%">Relative Constraint Date Value:</td>
                                                
                                                <td style="width: 05%"><Telerik:RadNumericTextBox ID="ActivityConstraintValue" Width="98%" NumberFormat-DecimalDigits="0" MaxValue="0" Value="0" runat="server" /></td>

                                                <td style="width: 08%">
                                                
                                                    <Telerik:RadComboBox ID="ActivityConstraintQualifierSelection" Width="99%" runat="server">
                                    
                                                        <Items>
                                        
                                                            <Telerik:RadComboBoxItem Value="0" Text="Days" Selected="true" />
                                            
                                                            <Telerik:RadComboBoxItem Value="1" Text="Months" />
                                            
                                                            <Telerik:RadComboBoxItem Value="2" Text="Years" />
                                        
                                                        </Items>
                                    
                                                    </Telerik:RadComboBox>

                                                </td>

                                                <td>&nbsp</td>

                                            </tr></table>
                                   
                                            <div id="ActivityParameters" style="display: none;" runat="server">
                            
                                                <Telerik:RadGrid ID="ActivityParametersGrid" Height="110" 
                                            
                                                    OnNeedDataSource="ActivityParametersGrid_OnNeedDataSource" 
                                                
                                                    OnItemCommand="ActivityParametersGrid_OnItemCommand" 
                                                
                                                    OnItemDataBound="ActivityParametersGrid_OnItemDataBound" 
                                                
                                                    AutoGenerateColumns="false" runat="server">
                            
                                                    <MasterTableView TableLayout="Auto" EditMode="EditForms" DataKeyNames="ParameterName">
                                    
                                                        <Columns>
                                    
                                                            <Telerik:GridBoundColumn DataField="ParameterName" UniqueName="ParameterName" HeaderText="Parameter Name" ReadOnly="true" Visible="true" />
                                            
                                                            <Telerik:GridBoundColumn DataField="ParameterValue" UniqueName="ParameterValue" HeaderText="Value" ReadOnly="true" Visible="true" />

                                                            <Telerik:GridEditCommandColumn HeaderText="Action"></Telerik:GridEditCommandColumn>                                                       

                                                        </Columns>
                                
                                                        <EditFormSettings EditFormType="Template">
                                        
                                                            <FormTemplate>
                                                
                                                                <table style="margin: 2px 0px 2px 0px; padding: 4px; line-height: 150%; width: 98%; table-layout: fixed; border: solid 1px black"><tr><td>
                                                    
                                                                    <table style="margin: 2px 0px 2px 0px; padding: 4px; line-height: 150%; width: 99%; table-layout: fixed;"><tr>
                                                                                                            
                                                                        <td style="width: 08%;">Value:&nbsp</td>
                                                        
                                                                        <td style="width: 25%"><Telerik:RadComboBox ID="ActivityParameterValueSelection" Width="99%" runat="server"></Telerik:RadComboBox></td>
                                                    
                                                                        <td style="width: 02%">&nbsp</td>

                                                                        <td style="width: 08%;">Fixed:&nbsp</td>
                                                       
                                                                        <td style="width: 15%"><Telerik:RadTextBox ID="ActivityParameterFixedValue" Width="99%" runat="server" /></td>

                                                                        <td style="width: 03%">&nbsp</td>
                                                        
                                                                        <td style="width: 10%"><asp:Button ID="ButtonActivityParameterUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Add" : "Update" %>' CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' Width="60px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>

                                                                        <td style="width: 02%">&nbsp</td>

                                                                        <td style="width: 10%"><asp:Button ID="ButtonActivityParameterCancel" Text="Cancel" CommandName="Cancel" Width="60px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>
                                                    
                                                                        <td style="width: 01%">&nbsp</td>
                                                        
                                                                    </tr></table>
                                                    
                                                                </td></tr></table>     
                                                
                                                            </FormTemplate>
                                        
                                                        </EditFormSettings>
                                                     
                                                                                
                                                    </MasterTableView>
                                    
                                                    <ClientSettings>
                                    
                                                        <Selecting AllowRowSelect="true" />
                                        
                                                        <Scrolling AllowScroll="true" />
                                    
                                                    </ClientSettings>
                                
                                                </Telerik:RadGrid>
                            
                                            </div>            

                                        </div>

                                    </Telerik:RadPageView>

                                    <Telerik:RadPageView ID="PageActivityNarrative" runat="server">
                                    
                                        <div style="width: 50%; padding: 4px;">Clinical Narrative:</div>
                                   
                                        <div style="width: 100%; padding: 4px;"><Telerik:RadTextBox ID="ActivityClinicalNarrative" Width="99%" Rows="3" TextMode="MultiLine" MaxLength="8000" EmptyMessage="(required)" runat="server" /></div>

                                        <div style="width: 50%; padding: 4px;">Common Narrative:</div>
                                    
                                        <div style="width: 100%; padding: 4px;"><Telerik:RadTextBox ID="ActivityCommonNarrative" Width="99%" Rows="3" TextMode="MultiLine" MaxLength="8000" EmptyMessage="(required)" runat="server" /></div>

                                    </Telerik:RadPageView>
                                    
                                    <Telerik:RadPageView ID="PageActivityThresholds" runat="server">
                                    
                                        <div style="margin: 0px 0px 10px 0px; padding: 0px; line-height: 150%;">
                                        
                                            <Telerik:RadGrid ID="ActivityThresholdsGrid" Height="205" 
                                            
                                                    OnItemCommand="ActivityThresholdsGrid_OnItemCommand" 
                                                
                                                    OnItemDataBound="ActivityThresholdsGrid_OnItemDataBound" 
                                                    
                                                    OnNeedDataSource="ActivityThresholdsGrid_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                                        
                                                <MasterTableView Name="Thresholds" TableLayout="Auto" DataKeyNames="ThresholdId" CommandItemDisplay="Bottom">
                                                
                                                    <Columns>
                                                    
                                                        <Telerik:GridBoundColumn DataField="ThresholdId" UniqueName="ThresholdId" HeaderText="Threshold" ReadOnly="true" Visible="true" />
                                                        
                                                        <Telerik:GridBoundColumn DataField="RelativeValue" UniqueName="RelativeValue" HeaderText="Relative" ReadOnly="true" Visible="true" />
                                                        
                                                        <Telerik:GridBoundColumn DataField="RelativeQualifier" UniqueName="RelativeQualifier" HeaderText="Qualifier" ReadOnly="true" Visible="true" />
                                                        
                                                        <Telerik:GridBoundColumn DataField="Status" UniqueName="Status" HeaderText="Status" ReadOnly="true" Visible="true" />
                                                       
                                                        <Telerik:GridBoundColumn DataField="Action" UniqueName="Action" HeaderText="Action" ReadOnly="true" Visible="false" />

                                                        <Telerik:GridEditCommandColumn></Telerik:GridEditCommandColumn>
                                                        
                                                        <Telerik:GridButtonColumn ConfirmText="Are you sure you want to Delete this Threshold?" ButtonType="LinkButton" CommandName="Delete" Text="Delete" />
              
                                                    </Columns>
                                                    
                                                    <DetailTables>
                                                    
                                                        <Telerik:GridTableView  Name="ThresholdParameters" Width="99%" TableLayout="Auto" DataKeyNames="ThresholdId,ThresholdKey,ParameterName" EditMode="EditForms">
                                                        
                                                            <ParentTableRelation><Telerik:GridRelationFields MasterKeyField="ThresholdId" DetailKeyField="ThresholdId" /></ParentTableRelation>
                                                                 
                                                            <Columns>
                                                        
                                                                <Telerik:GridBoundColumn DataField="ThresholdKey" UniqueName="ThresholdKey" HeaderText="ThresholdKey" ReadOnly="true" Visible="false" />

                                                                <Telerik:GridBoundColumn DataField="ThresholdId" UniqueName="ThresholdId" HeaderText="Threshold" ReadOnly="true" Visible="false" />
                                                                
                                                                <Telerik:GridBoundColumn DataField="ParameterName" UniqueName="ParameterName" HeaderText="Parameter Name" ReadOnly="true" Visible="true" />
                                                                
                                                                <Telerik:GridBoundColumn DataField="ParameterValue" UniqueName="ParameterValue" HeaderText="Value" ReadOnly="true" Visible="true" />
                                                               
                                                                <Telerik:GridEditCommandColumn></Telerik:GridEditCommandColumn>
                                                                
                                                            </Columns>      
                                                            
                                                            <EditFormSettings EditFormType="Template">
                                                            
                                                                <FormTemplate>
                                                                    
                                                                    <table style="margin: 2px 0px 2px 0px; padding: 4px; line-height: 150%; width: 98%; table-layout: fixed; border: solid 1px black"><tr><td>
                                                                        
                                                                        <table style="margin: 2px 0px 2px 0px; padding: 4px; line-height: 150%; width: 99%; table-layout: fixed;"><tr>
                                                                                                                                
                                                                            <td style="width: 08%;">Value:&nbsp</td>
                                                                            
                                                                            <td style="width: 25%"><Telerik:RadComboBox ID="ActivityThresholdParameterValue" Width="99%" runat="server"></Telerik:RadComboBox></td>
                                                                        
                                                                            <td style="width: 02%">&nbsp</td>
                                                                                                        
                                                                            <td style="width: 08%;">Fixed:&nbsp</td>
                                                                           
                                                                            <td style="width: 15%"><Telerik:RadTextBox ID="ActivityThresholdParameterFixedValue" Width="99%" runat="server" /></td>

                                                                            <td style="width: 03%">&nbsp</td>

                                                                            <td style="width: 10%"><asp:Button ID="ActivityThresholdUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Add" : "Update" %>' CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' Width="60px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>

                                                                            <td style="width: 02%">&nbsp</td>

                                                                            <td style="width: 10%"><asp:Button ID="ActivityThresholdCancel" Text="Cancel" CommandName="Cancel" Width="60px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>
                                                                        
                                                                            <td style="width: 01%">&nbsp</td>
                                                                            
                                                                        </tr></table>
                                                                        
                                                                    </td></tr></table>     
                                                                    
                                                                </FormTemplate>
                                                            
                                                            </EditFormSettings>
                                                                                                                
                                                        </Telerik:GridTableView>                                                    
                                                    
                                                    </DetailTables>
                                                
                                                    <CommandItemTemplate>
                                                    
                                                        <div style="padding: 4px;">
                                                        
                                                            <asp:LinkButton ID="ActivityThresholdsGridCommandAddThreshold" CommandName="InitInsert" Visible='<%# !ActivityThresholdsGrid.MasterTableView.IsItemInserted %>' runat="server">Add new Threshold</asp:LinkButton>
                                                        
                                                        </div>
                                                    
                                                    </CommandItemTemplate>
                                                    
                                                    <EditFormSettings EditFormType="Template">
                                                    
                                                        <FormTemplate>
                                                        
                                                            <table width="100%" style="margin: 2px 0px 2px 0px; padding: 4px; line-height: 150%; table-layout: fixed;"><tr>
                                                                                                                        
                                                                <td style="width: 06%;">Relative:&nbsp</td>

                                                                <td style="width: 05%"><Telerik:RadNumericTextBox ID="ActivityThresholdRelativeDateValue" Width="50" NumberFormat-DecimalDigits="0" runat="server" /></td>
                                                               
                                                                <td style="width: 02%">&nbsp</td>
                                                                    
                                                                <td style="width: 08%">
                                                                    
                                                                    <Telerik:RadComboBox ID="ActivityThresholdRelativeDateQualifier" Width="99%" runat="server">
                                                                        
                                                                        <Items>
                                                                            
                                                                            <Telerik:RadComboBoxItem Value="0" Text="Days" Selected="true" />
                                                                                
                                                                            <Telerik:RadComboBoxItem Value="1" Text="Months" />
                                                                                
                                                                            <Telerik:RadComboBoxItem Value="2" Text="Years" />
                                                                            
                                                                        </Items>
                                                                        
                                                                    </Telerik:RadComboBox>
                                                                        
                                                                </td>
                                                                      
                                                                <td style="width: 02%">&nbsp</td>
                                                                    
                                                                <td style="width: 05%;">Status:&nbsp</td>
                                                                    
                                                                <td style="width: 15%">
                                                                    
                                                                    <Telerik:RadComboBox ID="ActivityThresholdStatusSelection" Width="99%" runat="server">
                                                                        
                                                                        <Items>
                                                                            
                                                                            <Telerik:RadComboBoxItem Value="0" Text="No Change" Selected="true" />
                                                                                
                                                                            <Telerik:RadComboBoxItem Value="1" Text="Open"/>
                                                                                
                                                                            <Telerik:RadComboBoxItem Value="2" Text="Warning" />

                                                                            <Telerik:RadComboBoxItem Value="3" Text="Critical" />
                                                                                
                                                                        </Items>
                                                                        
                                                                    </Telerik:RadComboBox>
                                                                        
                                                                </td>
                                                                    
                                                                <td style="width: 02%">&nbsp</td>
                                                                    
                                                                <td style="width: 08%"><asp:Button ID="ButtonActivityThresholdUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Add" : "Update" %>' CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' Width="60px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>

                                                                <td style="width: 01%">&nbsp</td>

                                                                <td style="width: 08%"><asp:Button ID="ButtonActivityThresholdCancel" Text="Cancel" CommandName="Cancel" Width="60px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>
                                                                
                                                                <td style="width: 02%">&nbsp</td>
                                                                    
                                                                <td style="">&nbsp</td>

                                                            </tr></table>
                                                                
                                                        </FormTemplate>
                                                                                                        
                                                    </EditFormSettings>
                                                                                            
                                                </MasterTableView>
                                                
                                                <ClientSettings>
                                                
                                                    <Selecting AllowRowSelect="true" />
                                                    
                                                    <Scrolling AllowScroll="true" />
                                                
                                                </ClientSettings>
                                            
                                            </Telerik:RadGrid>
                                        
                                        </div>            
                                        
                                    </Telerik:RadPageView>
                                    
                                </Telerik:RadMultiPage>
                                
                            </div>
                            
                        </div>
                                     
                        <div style="margin: 2px 10px 0px 10px; padding: 4px; line-height: 150%;">
                           
                            <div style="position: relative; float: right"><asp:Button ID="ButtonUpdateActivity" Text="Update Selected" OnClick="ButtonAddUpdateActivity_OnClick" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></div>
                    
                            <div style="position: relative; float: right"><asp:Button ID="ButtonAddActivity" Text="Add Activity" OnClick="ButtonAddUpdateActivity_OnClick" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></div>
        
                        </div>    
                    
                    </div>                        
                           