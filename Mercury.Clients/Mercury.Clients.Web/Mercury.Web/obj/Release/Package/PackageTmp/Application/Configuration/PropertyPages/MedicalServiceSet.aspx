<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MedicalServiceSet.aspx.cs" Inherits="Mercury.Web.Application.Configuration.Windows.MedicalServiceSet" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">

    <title>Untitled Page</title>
    
    <style type="text/css">

    .radReadOnlyCss_Office2007 {
    	border:1px solid #999999 !important;
        color:#000 !important;
        font:12px segoe ui, arial,tahoma,sans-serif !important;
	    background:#fff !important;
	    padding:1px 0 1px 1px !important;
    }

    </style>

</head>

<body>

<form id="FormMedicalServiceSet" runat="server">

<div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div>

<div id="AjaxManagerDiv" style="display: none">

    <asp:ScriptManager ID="AjaxScriptManager" runat="Server" />
    
    <Telerik:RadAjaxManager ID="AjaxManager" runat="server">
    
        <AjaxSettings>
        
            <Telerik:AjaxSetting AjaxControlID="ButtonAddSingletonDefinition">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonAddSingletonDefinition" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ServiceDefinitionGrid" LoadingPanelID="AjaxLoadingPanel" />

                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />                    
                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
        
            <Telerik:AjaxSetting AjaxControlID="ButtonAddSetDefinition">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonAddSetDefinition" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ServiceDefinitionGrid" LoadingPanelID="AjaxLoadingPanel" />
                
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />                    
                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="ButtonPreview" >
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonPreview" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ServicePreviewGrid" LoadingPanelID="AjaxLoadingPanel"  />

                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />
                                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
        </AjaxSettings>
        
    </Telerik:RadAjaxManager>
    
    <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanel" Transparency="1" InitialDelayTime="100" MinDisplayTime="0" runat="server">
   
        <div style="text-align: center"><span style="text-align: center"><img src="/Images/Loading.gif" alt="Loading" /></span></div>
    
    </Telerik:RadAjaxLoadingPanel>
    
</div>
        
        
<div style="font-family: segoe ui, Arial, Helvetica, Sans-Serif; font-size: 10pt; line-height: 150%; min-width: 740px">

    <div style="position: relative; float: left; width: 99%; height: 700px; margin: 1px; border: solid 1px black;">
    
        <Telerik:RadTabStrip ID="PropertiesTab" MultiPageID="PropertiesContent" SelectedIndex="0" runat="server">
        
            <Tabs>
            
                <Telerik:RadTab Text="General"></Telerik:RadTab>

                <Telerik:RadTab Text="Definition"></Telerik:RadTab>

                <Telerik:RadTab Text="Preview"></Telerik:RadTab>

            </Tabs>
                  
        </Telerik:RadTabStrip>

        <Telerik:RadMultiPage ID="PropertiesContent" SelectedIndex="0" runat="server">
        
            <Telerik:RadPageView ID="General" runat="server">
            
                <div style="position: relative; float: left; width:65%; margin: 1px; border: solid 1px white;">
                
                    <div style=" margin: 2px; padding: 2px;">
                    
                        <div style="float: left; height: 32px; width: 40px;"><img src="/Images/Common32/Document2.png" alt="Service Properties" /></div>
                        
                        <div style="float: left; height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">General Properties of the Service</div>
                    
                    </div>
                    
                    
                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px;"><b>Name and Description</b></div>
                      
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Name:</div>
                    
                        <div style="position: relative; float: left; width: 80%; padding: 4px;"><Telerik:RadTextBox ID="ServiceSetName" Width="100%" MaxLength="60" EmptyMessage="(required)" runat="server" /></div>
                    
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Description:</div>
                    
                        <div style="position: relative; float: left; width: 100%; padding: 4px;"><Telerik:RadTextBox ID="ServiceSetDescription" Width="98%" Rows="15" EmptyMessage="(required)" TextMode="MultiLine" runat="server" /></div>


                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Classification:</div>
                    
                        <div style="position: relative; float: left; width: 20%; padding: 4px;">
                    
                            <Telerik:RadComboBox ID="ServiceSetClassification" Width="98%" runat="server">
                            
                                <Items> 

                                    <Telerik:RadComboBoxItem Value="0" Text="Not Specified" runat="server" />

                                    <Telerik:RadComboBoxItem Value="1" Text="Medical" Selected="true" runat="server" />
                                    
                                    <Telerik:RadComboBoxItem Value="2" Text="Medication" runat="server" />
                                    
                                    <Telerik:RadComboBoxItem Value="3" Text="Lab" runat="server" />
                                
                                    <Telerik:RadComboBoxItem Value="4" Text="Immunization" runat="server" />
                                    
                                    <Telerik:RadComboBoxItem Value="5" Text="Diagnosis" runat="server" />

                                    <Telerik:RadComboBoxItem Value="6" Text="Exclusion" runat="server" />
                                
                                </Items>
                                
                            </Telerik:RadComboBox>
                                    
                        </div>

                        <div style="position: relative; float: left; width: 35%">
                        
                            <div style="position: relative; float: left; width: 20%; padding: 4px;">Type:</div>
                        
                            <div style="position: relative; float: left; width: 70%; padding: 4px;">
            
                                <Telerik:RadComboBox ID="ServiceSetType" Width="99%" OnClientSelectedIndexChanged="ServiceSetType_OnClientSelectedIndexChanged" runat="server">
                                
                                    <Items> 

                                        <Telerik:RadComboBoxItem Value="0" Text="Intersection (and)" runat="server" />
                                    
                                        <Telerik:RadComboBoxItem Value="1" Text="Union (or)" runat="server" />
                                        
                                    </Items>
                                    
                                </Telerik:RadComboBox>
                                
                            </div>

                        </div>

                        <div style="position: relative; float: left; width: 20%">
                        
                            <div style="position: relative; float: left; width: 45%; padding: 4px;"><Telerik:RadNumericTextBox ID="ServiceSetWithinDays" Width="99%" NumberFormat-DecimalDigits="0" MinValue="0" MaxValue="64000" runat="server" /></div>

                            <div style="position: relative; float: left; width: 30%; padding: 4px;">Days</div>
                        
                        </div>
                        
                        <div style="clear: both"></div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="ServiceSetEnabled" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Enabled</div>
                        
                        </div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="ServiceSetVisible" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Visible</div>
                        
                        </div>
                        
                    </div>
                

                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px"><b>Created and Modified</b></div>
                
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 50%">
                        
                            <div style="width: 99%; padding: 4px; text-align: center">Created Information</div>
                        

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ServiceSetCreateAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ServiceSetCreateAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ServiceSetCreateAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                            
                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                                
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="ServiceSetCreateDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                            
                            </div>

                        </div>

                        <div style="position: relative; float: left; width: 50%">

                            <div style="width: 99%; padding: 4px; text-align: center">Modified Information</div>


                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ServiceSetModifiedAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div> 
                            
                            
                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ServiceSetModifiedAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>


                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ServiceSetModifiedAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>
                            

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="ServiceSetModifiedDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                                
                            </div>
                            
                          </div>

                    <div>
                    
                    </div>
                
                </div>
                
                </div>

                <div style="position: relative; float: left; width:33%; height: 661px; margin: 4px; border: solid 1px black; background-color: #dee6ee">
                
                </div>

            </Telerik:RadPageView>    
            
        
            <Telerik:RadPageView ID="PageDefinitions" runat="server">
            
                <div id="PageDefinitionsContent" style="margin: 1px; border: solid 1px white;">
                
                    <div style=" margin: 2px; padding: 2px;">
                    
                        <div style="float: left; height: 32px; width: 40px;"><img src="/Images/Common32/Document2DatabaseTable.png" alt="Service Definition" /></div>
                        
                        <div style="float: left; height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Service Identification Criteria</div>
                    
                    </div>
                    
                    
                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px;"><b>Current Criteria</b></div>
                                        
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <Telerik:RadGrid ID="ServiceDefinitionGrid" Height="475" AutoGenerateColumns="false" OnItemCommand="ServiceDefinitionGrid_OnItemCommand" OnDeleteCommand="ServiceDefinitionGrid_OnDeleteCommand" runat="server">
                        
                            <MasterTableView TableLayout="Auto">
                            
                                <Columns>
                            
                                    <Telerik:GridBoundColumn DataField="DefinitionId" UniqueName="DefinitionId" HeaderText="Id" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="ServiceId" UniqueName="ServiceId" HeaderText="Service Id" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="ServiceName" UniqueName="ServiceName" HeaderText="Name" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="ServiceType" UniqueName="ServiceType" HeaderText="Type" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="Enabled" UniqueName="Enabled" HeaderText="Enabled" ReadOnly="true" Visible="true" />

                                    <Telerik:GridButtonColumn CommandName="Delete" ButtonType="LinkButton" ConfirmText="Are you sure you want to delete this definition?" UniqueName="DeleteDefinition" Text="Delete" />
                                    
                                    <Telerik:GridButtonColumn CommandName="ToggleActive" ButtonType="LinkButton" ConfirmText="Are you sure you want to toggle the active status of this definition?" UniqueName="IsActive" Text="Toggle Enabled" />
                                
                                </Columns>
                            
                            
                            </MasterTableView>
                            
                            <ClientSettings>
                            
                                <Selecting AllowRowSelect="true" />
                                
                                <Scrolling AllowScroll="true" />
                            
                            </ClientSettings>
                        
                        </Telerik:RadGrid>
                        
                    </div>
                    
                                      
                    <div id="AddDefinitionDiv" style="clear: both" runat="server">
                      
                        <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px;"><b>Add Criteria</b></div>                      

                        <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                        
                            <div style="position: relative; float: left; width: 25%">Add a Singleton:</div>
                            
                            <div style="position: relative; float: left; width: 55%">
                            
                                <Telerik:RadComboBox ID="ServiceDefinitionSingletonSelection" Width="99%" Height="300" runat="server"></Telerik:RadComboBox>
                        
                            </div>
                       
                            <div style="position: relative; float: right"><asp:Button ID="ButtonAddSingletonDefinition" Text="Add" Width="60" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" OnClick="ButtonAddDefinition_OnClick" runat="server" /></div>
                            
                        </div>
                        
                        <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; line-height: 150%"></div>
                        
                        <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                        
                            <div style="position: relative; float: left; width: 25%">Add another Set:</div>
                            
                            <div style="position: relative; float: left; width: 55%">
                            
                                <Telerik:RadComboBox ID="ServiceDefinitionSetSelection" Width="99%" Height="300" runat="server"></Telerik:RadComboBox>
                        
                            </div>
                       
                            <div style="position: relative; float: right"><asp:Button ID="ButtonAddSetDefinition" Text="Add" Width="60" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" OnClick="ButtonAddDefinition_OnClick" runat="server" /></div>
                            
                        </div>
                    
                    </div>                                      
                    
                </div>
               
            </Telerik:RadPageView>    
            
            <Telerik:RadPageView ID="Preview" runat="server">
            
                <div style="position: relative; float: left; width:65%; margin: 1px; border: solid 1px white;">
                
                    <div style=" margin: 2px; padding: 2px;">
                    
                        <div style="float: left; height: 32px; width: 40px;"><img src="/Images/Common32/Document2Preview.png" alt="Service Preview" /></div>
                        
                        <div style="float: left; height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Service Preview</div>
                    
                    </div>

                </div>            
                        
                <div style="clear: both; margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                
                    <Telerik:RadGrid ID="ServicePreviewGrid" Height="580" AutoGenerateColumns="false" runat="server">
                    
                        <MasterTableView Width="99%" TableLayout="Auto">
                        
                            <Columns>
                        
                                <Telerik:GridBoundColumn DataField="SetDefinitionId" UniqueName="SetDefinitionId" HeaderText="Definition Id" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="MemberServiceId" UniqueName="MemberServiceId" HeaderText="Member Service Id" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="ServiceName" UniqueName="ServiceName" HeaderText="Name" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="ServiceType" UniqueName="ServiceType" HeaderText="Type" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="MemberId" UniqueName="MemberId" HeaderText="Member Id" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="EventDate" UniqueName="EventDate" HeaderText="Event Date" ReadOnly="true" Visible="true" />
                            
                            </Columns>
                        
                        </MasterTableView>
                        
                        <ClientSettings>
                        
                            <Selecting AllowRowSelect="true" />
                            
                            <Scrolling AllowScroll="true" />
                        
                        </ClientSettings>
                    
                    </Telerik:RadGrid>
                    
                </div>
                
                <div style="height: 20px; padding: 0px 10px 0px 10px;">
                
                    <span style="float: left;">Warning: Retreiving services may take time.</span>
                        
                    <span style="float: right;"><asp:Button ID="ButtonPreview" Text="Preview" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" OnClick="ButtonPreview_OnClick" runat="Server" /></span>

                </div>
        
            </Telerik:RadPageView>
        
        </Telerik:RadMultiPage>
           

    </div>


        <div style="clear: both; height: 10px"><span></span></div>

        <div style="height: 20px; padding: 0px 10px 0px 10px;">
        
            <span style="float: left"><asp:Label ID="SaveResponseLabel" Text="" runat="server" /></span>
        
            <span style="float: right;"></span>

            <span style="float: right;"><asp:Button ID="ButtonApply" Text="Apply" Width="73px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" OnClick="ButtonApply_OnClick" runat="Server" /></span>

            <span style="float: right; width: 10px">&nbsp</span>

            <span style="float: right;"><asp:Button ID="ButtonCancel" Text="Cancel" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" OnClick="ButtonCancel_OnClick" runat="Server" /></span>

            <span style="float: right; width: 10px">&nbsp</span>

            <span style="float: right;"><asp:Button ID="ButtonOk" Text="OK" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" OnClick="ButtonOk_OnClick" runat="Server" /></span>

        </div>
                
</div>

<script type="text/javascript"> 

    function HelpPanel_OnHide (panelId) {

        helpPanel = document.getElementById (panelId + "Help");
        
        if (helpPanel != null) {
        
            helpPanel.style.display = "none";
            
            contentPanel = document.getElementById (panelId + "Content");
            
            if (contentPanel != null) { 
            
                contentPanel.style.width = "99%";
                
            }
            
        }

        return;

    }

    function ServiceSetType_OnClientSelectedIndexChanged (sender, eventArgs) {
    
        var selectedItem = eventArgs.get_item ();
        
        var withinDays = $find ("ServiceSetWithinDays");
        
        if (withinDays != null) { 

            if (selectedItem.get_value() == 0) { 
            
                withinDays.enable ();
                
            }
            
            else { withinDays.disable (); }
            
        }

        return;
        
    }

</script>

</form>
    
</body>

</html>
