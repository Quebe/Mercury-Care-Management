<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfigurationImport.aspx.cs" Inherits="Mercury.Web.Application.Configuration.Windows.ConfigurationImport" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">

    <title>Untitled Page</title>
    
</head>

<body>

<form id="FormConfigurationImport" method="post" runat="server">


<div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div>

<div id="AjaxManagerDiv" style="display: none">

    <asp:ScriptManager ID="AjaxScriptManager" runat="Server" />
    
    <Telerik:RadAjaxManager ID="AjaxManager" runat="server">
    
        <AjaxSettings>
        
        </AjaxSettings>
        
    </Telerik:RadAjaxManager>
    
</div>


<div id="FileUploadSection" style="font-family: segoe ui, Arial, Helvetica, Sans-Serif; font-size: 10pt; line-height: 150%" runat="server">

    <div style="width: 99%; height: 700px; margin: 1px; border: solid 1px black; overflow: hidden;">

        <Telerik:RadSplitter ID="ImportSplitter" Height="700" ResizeMode="AdjacentPane" VisibleDuringInit="false" Width="100%" runat="server">

            <Telerik:RadPane id="ImportPane" Scrolling="none" Width="66%" runat="server">
                    
                <div style="float: left; width:100%; margin: 1px; border: solid 1px white;">
                   

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                        <div style="float: left;">Configuration File: </div>
                        
                        <div style="float: left;">&nbsp</div>
                    
                        <div style="float: left;"><Telerik:RadUpload ID="TelerikUpload" InputSize="50" InitialFileInputsCount="1" MaxFileInputsCount="1" ControlObjectsVisibility="None" runat="server" /></div>

                        <div style="float: left;"><asp:Button ID="ButtonUploadFile" Text="Submit" Width="73px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" OnClick="ButtonUploadFile_OnClick" runat="Server" /></div>
                        
                    </div>      
                                
                    <div style="margin: 50px">
                    
                    </div>  
                    
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <Telerik:RadGrid ID="ImportResultsGrid" Height="475" AutoGenerateColumns="false" runat="server">
                        
                            <MasterTableView Width="99%" TableLayout="Auto">
                            
                                <Columns>
                            
                                    <Telerik:GridBoundColumn DataField="ObjectType" UniqueName="ObjectType" HeaderText="Type" ReadOnly="true" Visible="true" />

                                    <Telerik:GridBoundColumn DataField="ObjectName" UniqueName="ObjectName" HeaderText="Name" ReadOnly="true" Visible="true" />

                                    <Telerik:GridBoundColumn DataField="Success" UniqueName="Success" HeaderText="Success" ReadOnly="true" Visible="true" />

                                    <Telerik:GridBoundColumn DataField="Exception" UniqueName="Exception" HeaderText="Exception" ReadOnly="true" Visible="true" />

                                    <Telerik:GridBoundColumn DataField="Id" UniqueName="Id" HeaderText="Id" ReadOnly="true" Visible="true" />

                                </Columns>
                            
                            </MasterTableView>
                            
                            <ClientSettings>
                            
                                <Selecting AllowRowSelect="true" />
                                
                                <Scrolling AllowScroll="true" />
                            
                            </ClientSettings>
                        
                        </Telerik:RadGrid>
                        
                    </div>
                       
                </div>               

            </Telerik:RadPane>
            
            <Telerik:RadSplitBar ID="ContentHelpSplitterBar" CollapseMode="Both" runat="server" />

            <Telerik:RadPane ID="HelpPaneContainer" MinWidth="22" Scrolling="None" runat="server"  >
                    
                <Telerik:RadSlidingZone ID="HelpPaneRightSlidingZone" Width="22" DockedPaneId="PaneHelp" SlideDirection="Left" runat="server">

                    <Telerik:RadSlidingPane ID="PaneHelp" Title="Help" Width="250" BackColor="#dee6ee" Scrolling="None" runat="server">
                                               
                        <div style="font-family: Arial; line-height: 150%">                                               
                                               
                        Import file must be an XML file previously exported by the Mercury System. The import currently
                        
                        supports Services, Metrics, and Populations. <br />
                        
                        Use the Select button to browse for the appropriate XML Configuration file on your computer. Once 
                        
                        selected, click Submit to import the Configuration. <br />
                        
                        For existing configuration, the information is not overwritten.    <br />                                           
                        
                        </div>

                    </Telerik:RadSlidingPane>
            
                </Telerik:RadSlidingZone>                
                
            </Telerik:RadPane>
        
        </Telerik:RadSplitter>

    </div>
                    
</div>


<div id="ConfigurationProcessSection" style="font-family: segoe ui, Arial, Helvetica, Sans-Serif; font-size: 10pt; line-height: 150%; display: none;" runat="server">

    <div style="width: 99%; height: 700px; margin: 1px; border: solid 1px black; overflow: hidden;">

       <div style="float: left; width:65%; margin: 1px; border: solid 1px white;">
       
       </div>
           
       <div style="position: relative; float: left; width:33%; height: 661px; margin: 4px; border: solid 1px black; background-color: #dee6ee">
        
       </div>

    </div>
                    
</div>

        <div style="clear: both; height: 10px"><span></span></div>

        <div style="height: 20px; padding: 0px 10px 0px 10px;">
        
            <span style="float: left"><asp:Label ID="SaveResponseLabel" Text="" runat="server" /></span>
        
            <span style="float: right; width: 10px">&nbsp</span>

            <span style="float: right;"><asp:Button ID="ButtonClose" Text="Close" OnClick="ButtonClose_OnClick" Width="73px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></span>

            <span style="float: right; width: 10px">&nbsp</span>

            <span style="float: right;"><asp:Button ID="ButtonOk" Text="OK" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" runat="Server" Visible="false" /></span>

        </div>
                
</form>
    
</body>

</html>

