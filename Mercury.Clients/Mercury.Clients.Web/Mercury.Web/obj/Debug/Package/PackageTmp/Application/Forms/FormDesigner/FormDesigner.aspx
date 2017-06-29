<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeBehind="FormDesigner.aspx.cs" Inherits="Mercury.Web.Application.Forms.FormDesigner.FormDesigner" %>

<%@ Register TagPrefix="FormDesignerControl" TagName="OpenDialog" Src="/Application/Forms/FormDesigner/Controls/OpenDialog.ascx"  %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">

    <title>Mercury Care Management</title>
    
    <link rel="shortcut icon" href="/favicon.ico" type="image/x-icon" />

    <link rel="Stylesheet" href="/Styles/Global.css" type="text/css" />
    
    <link rel="Stylesheet" href="Styles/FormDesigner.css" type="text/css" />

    <style type="text/css">
    
        html { overflow: hidden; }
    
    </style>

</head>

<body style="margin: 0px;" class="TextNormal BackgroundColorLight">

<form id="ApplicationForm" runat="server">


<div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div> 

<div id="AjaxManagerDiv" style="display: none">

    <asp:ScriptManager ID="AjaxScriptManager" AsyncPostBackTimeout="600" runat="Server">

        <Scripts>
                    
            <asp:ScriptReference Path="FormDesigner.js" />
        
        </Scripts>
    
    </asp:ScriptManager>

    <Telerik:RadAjaxManager ID="TelerikAjaxManager" runat="server">
    
        <AjaxSettings>

            <Telerik:AjaxSetting AjaxControlID="FormDesignerToolbar">
            
                <UpdatedControls>
                                
                    <Telerik:AjaxUpdatedControl ControlID="FormDesignerToolbar" />
                                                        
                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarPanel" />
                                                        
                    <Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="FormExplorerTree" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ControlProperties" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="HtmlEditor" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="SelectionItemsGrid" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="OpenDialog" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="CompileMessagesGrid" />
                    
                </UpdatedControls>
                
            </Telerik:AjaxSetting>        
           
            <Telerik:AjaxSetting AjaxControlID="FormExplorerTree">
            
                <UpdatedControls>
                                
                    <Telerik:AjaxUpdatedControl ControlID="FormExplorerTree" LoadingPanelID="AjaxLoadingPanel" />                    
                                                       
                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarPanel" LoadingPanelID="AjaxLoadingPanelWhiteout" />         
                                    
                    <Telerik:AjaxUpdatedControl ControlID="ControlProperties" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="HtmlEditor" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="SelectionItemsGrid" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarPanel" />                                                       
                                                            
                </UpdatedControls>
                
            </Telerik:AjaxSetting>        
            
            <Telerik:AjaxSetting AjaxControlID="HomeToolbar">
            
                <UpdatedControls>
                                
                    <Telerik:AjaxUpdatedControl ControlID="HomeToolbar" />
                                                        
                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarPanel" />
                                                                                                                
                    <Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesignerContainer" LoadingPanelID="AjaxLoadingPanel" />
      
                    <Telerik:AjaxUpdatedControl ControlID="FormExplorerTreeContainer" LoadingPanelID="AjaxLoadingPanel" />
               
                    <Telerik:AjaxUpdatedControl ControlID="ControlProperties" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="HtmlEditor" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="SelectionItemsGrid" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="OpenDialog" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="CompileMessagesGrid" />
                    
                </UpdatedControls>
                
            </Telerik:AjaxSetting>      
            
            <Telerik:AjaxSetting AjaxControlID="StyleToolbarPreview">
            
                <UpdatedControls>
                                
                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarPreview" />
                    
                </UpdatedControls>
                
            </Telerik:AjaxSetting>                    
                    
            <Telerik:AjaxSetting AjaxControlID="StyleToolbarControlSelection">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarControlSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarFont1FontSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarFont1SizeSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />            
                    
                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarFont2" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                                                                                                   
                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarParagraph1" LoadingPanelID="AjaxLoadingPanelWhiteout" />                                                            
                    
                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarParagraph2" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                                        
                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarBox1" LoadingPanelID="AjaxLoadingPanelWhiteout" />                    
                    
                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarBox2" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                </UpdatedControls>
                
            </Telerik:AjaxSetting>        

            <Telerik:AjaxSetting AjaxControlID="StyleToolbarFont1FontSelection">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarFont1FontSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                                                                                                
                    <Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" LoadingPanelID="AjaxLoadingPanel" />
                                                                                                   
                </UpdatedControls>
                
            </Telerik:AjaxSetting>        
            
            <Telerik:AjaxSetting AjaxControlID="StyleToolbarFont1SizeSelection">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarFont1SizeSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                                                                                                
                    <Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" LoadingPanelID="AjaxLoadingPanel" />
                                                                                                   
                </UpdatedControls>
                
            </Telerik:AjaxSetting>        

            <Telerik:AjaxSetting AjaxControlID="StyleToolbarFont2">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarFont2" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                                                                                                
                    <Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" LoadingPanelID="AjaxLoadingPanel" />
                                                                                                   
                </UpdatedControls>
                
            </Telerik:AjaxSetting>                               
           
            <Telerik:AjaxSetting AjaxControlID="StyleFontColor">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="StyleFontColor" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                                                                                                
                    <Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" LoadingPanelID="AjaxLoadingPanel" />
                                                                                                   
                </UpdatedControls>
                
            </Telerik:AjaxSetting>                          
           
            <Telerik:AjaxSetting AjaxControlID="StyleToolbarParagraph1">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarParagraph1" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                                                                                                
                    <Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" LoadingPanelID="AjaxLoadingPanel" />
                                                                                                   
                </UpdatedControls>
                
            </Telerik:AjaxSetting>                               
            
            <Telerik:AjaxSetting AjaxControlID="StyleToolbarParagraph2">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarParagraph2" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                                                                                                
                    <Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" LoadingPanelID="AjaxLoadingPanel" />
                                                                                                   
                </UpdatedControls>
                
            </Telerik:AjaxSetting>      
            
            <Telerik:AjaxSetting AjaxControlID="ParagraphBorderSizeSelection">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ParagraphBorderSizeSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                                                                                                
                    <Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" LoadingPanelID="AjaxLoadingPanel" />
                                                                                                   
                </UpdatedControls>
                
            </Telerik:AjaxSetting>                                           
            
            <Telerik:AjaxSetting AjaxControlID="StyleParagraph2BackgroundColor">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="StyleParagraph2BackgroundColor" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                                                                                                
                    <Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" LoadingPanelID="AjaxLoadingPanel" />
                                                                                                   
                </UpdatedControls>
                
            </Telerik:AjaxSetting>   
           
            <Telerik:AjaxSetting AjaxControlID="StyleToolbarBox1Width">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarBox1Width" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarBox1WidthUnit" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" LoadingPanelID="AjaxLoadingPanel" />
                                                                                                   
                </UpdatedControls>
                
            </Telerik:AjaxSetting>   
            
            <Telerik:AjaxSetting AjaxControlID="StyleToolbarBox1WidthUnit">
            
                <UpdatedControls>               
                    
                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarBox1WidthUnit" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" LoadingPanelID="AjaxLoadingPanel" />
                                                                                                   
                </UpdatedControls>
                
            </Telerik:AjaxSetting>   
            
            <Telerik:AjaxSetting AjaxControlID="StyleToolbarBox1Height">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarBox1Height" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarBox1HeightUnit" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                                                                                                
                    <Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" LoadingPanelID="AjaxLoadingPanel" />
                                                                                                   
                </UpdatedControls>
                
            </Telerik:AjaxSetting>   
                       
            <Telerik:AjaxSetting AjaxControlID="StyleToolbarBox1HeightUnit">
            
                <UpdatedControls>

                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarBox1HeightUnit" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                                                                                                
                    <Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" LoadingPanelID="AjaxLoadingPanel" />
                                                                                                   
                </UpdatedControls>
                
            </Telerik:AjaxSetting>   
            
            <Telerik:AjaxSetting AjaxControlID="StyleToolbarBox2Margin">
            
                <UpdatedControls>

                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarBox2Margin" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                                                                                                
                    <Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" LoadingPanelID="AjaxLoadingPanel" />
                                                                                                   
                </UpdatedControls>
                
            </Telerik:AjaxSetting>   
            
            <Telerik:AjaxSetting AjaxControlID="StyleToolbarBox2Padding">
            
                <UpdatedControls>

                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarBox1HeightUnit" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                                                                                                
                    <Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" LoadingPanelID="AjaxLoadingPanel" />
                                                                                                   
                </UpdatedControls>
                
            </Telerik:AjaxSetting>   
            
            
            <Telerik:AjaxSetting AjaxControlID="RequestServerRefresh">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="RequestServerRefresh" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarPanel" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="FormExplorerTree" LoadingPanelID="AjaxLoadingPanel" />

                    <Telerik:AjaxUpdatedControl ControlID="ControlProperties" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="HtmlEditor" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="SelectionItemsGrid" LoadingPanelID="AjaxLoadingPanel" />
                    
                </UpdatedControls>
                
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="DropControl">
            
                <UpdatedControls>

                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarPanel" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                                                    
                    <Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ControlProperties" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="HtmlEditor" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="SelectionItemsGrid" LoadingPanelID="AjaxLoadingPanel" />
                    
                </UpdatedControls>
                
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="DeleteControl">
            
                <UpdatedControls>
                                                    
                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarPanel" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ControlProperties" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="HtmlEditor" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="SelectionItemsGrid" LoadingPanelID="AjaxLoadingPanel" />
                    
                </UpdatedControls>
                  
            </Telerik:AjaxSetting>
        
            <Telerik:AjaxSetting AjaxControlID="SelectControlProperties">
            
                <UpdatedControls>
                                    
                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarPanel" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ControlProperties" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="HtmlEditor" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="SelectionItemsGrid" LoadingPanelID="AjaxLoadingPanel" />
                    
                </UpdatedControls>
                
            </Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesControlSelection">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="StyleToolbarPanel" LoadingPanelID="AjaxLoadingPanelWhiteout" />         
                                    
                    <Telerik:AjaxUpdatedControl ControlID="ControlProperties" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="HtmlEditor" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="SelectionItemsGrid" LoadingPanelID="AjaxLoadingPanel" />
                    
                </UpdatedControls>
                
            </Telerik:AjaxSetting>
        
        
            <Telerik:AjaxSetting AjaxControlID="PropertiesControlName"><UpdatedControls>
            
                <Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" />
                
                <Telerik:AjaxUpdatedControl ControlID="FormExplorerTree" LoadingPanelID="AjaxLoadingPanel" />

                <Telerik:AjaxUpdatedControl ControlID="PropertiesControlSelection" />
                
                <Telerik:AjaxUpdatedControl ControlID="PropertiesControlName" />
                
            </UpdatedControls></Telerik:AjaxSetting>
        
            <Telerik:AjaxSetting AjaxControlID="PropertiesControlTitle"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="PropertiesControlTitle" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesControlTabIndex"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="PropertiesControlTabIndex" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesControlDisplay"><UpdatedControls>
                
                <Telerik:AjaxUpdatedControl ControlID="PropertiesControlDisplay" />
                
                <Telerik:AjaxUpdatedControl ControlID="FormExplorerTree" LoadingPanelID="AjaxLoadingPanel" />

            </UpdatedControls></Telerik:AjaxSetting>

            
            <Telerik:AjaxSetting AjaxControlID="PropertiesControlEvent">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="PropertiesControlEvent" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="PropertiesControlEventHandler" />
                
                </UpdatedControls>               
            
            </Telerik:AjaxSetting>            

            <Telerik:AjaxSetting AjaxControlID="PropertiesControlEventHandler">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="PropertiesControlEvent" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="PropertiesControlEventHandler" />
                
                </UpdatedControls>               
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="PropertiesControlBindableProperty">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="PropertiesControlBindableProperty" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="PropertiesControlDataBindingControlId" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="PropertiesControlDataBindingContext" /></UpdatedControls></Telerik:AjaxSetting>
    
            <Telerik:AjaxSetting AjaxControlID="PropertiesControlDataBindingControlId">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="PropertiesControlBindableProperty" />

                    <Telerik:AjaxUpdatedControl ControlID="PropertiesControlDataBindingControlId" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="PropertiesControlDataBindingContext" />

                    </UpdatedControls></Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="PropertiesControlDataBindingContext">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="PropertiesControlBindableProperty" />

                    <Telerik:AjaxUpdatedControl ControlID="PropertiesControlDataBindingControlId" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="PropertiesControlDataBindingContext" />

                    </UpdatedControls></Telerik:AjaxSetting>                
            
            <Telerik:AjaxSetting AjaxControlID="PropertiesControlDataSourceControlId"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="PropertiesControlDataBinding" /><Telerik:AjaxUpdatedControl ControlID="PropertiesControlDataSourceControlId" /></UpdatedControls></Telerik:AjaxSetting>
    
            <Telerik:AjaxSetting AjaxControlID="PropertiesControlDataBinding"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesControlDataBinding" /></UpdatedControls></Telerik:AjaxSetting>
            
    
            <Telerik:AjaxSetting AjaxControlID="HtmlEditorCommandButtonsOk"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="ControlProperties" /><Telerik:AjaxUpdatedControl ControlID="HtmlEditor" /></UpdatedControls></Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="HtmlEditorCommandButtonsCancel"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="ControlProperties" /><Telerik:AjaxUpdatedControl ControlID="HtmlEditor" /></UpdatedControls></Telerik:AjaxSetting>


            <Telerik:AjaxSetting AjaxControlID="PropertiesFormEntityType"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesFormEntityType" /><Telerik:AjaxUpdatedControl ControlID="PropertiesControlBindableProperty" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesFormAllowPrecompileEvents"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesFormAllowPrecompileEvents" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesSectionPageBreakAfter"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesSectionPageBreakAfter" /></UpdatedControls></Telerik:AjaxSetting>

            

            <Telerik:AjaxSetting AjaxControlID="PropertiesInputType"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="ControlProperties" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesInputTextMode"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesInputTextMode" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesInputMaxLength"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesInputMaxLength" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesInputColumns"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesInputColumns" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesInputRows"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesInputRows" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesInputWrap"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesInputWrap" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesInputMask"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesInputMask" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesInputEmptyMessage"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesInputEmptyMessage" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesInputValidation"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesInputValidation" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesControlReadOnly"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesControlReadOnly" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesControlRequired"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesControlRequired" /></UpdatedControls></Telerik:AjaxSetting>


            <Telerik:AjaxSetting AjaxControlID="PropertiesInputNumericType"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesInputNumericType" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesInputMinValue"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesInputMinValue" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesInputMaxValue"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesInputMaxValue" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesInputShowSpinButtons"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesInputShowSpinButtons" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesInputButtonPosition"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesInputButtonPosition" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesInputDateFormat"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesInputDateFormat" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesInputDisplayDateFormat"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesInputDisplayDateFormat" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesInputSelectionOnFocus"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesInputSelectionOnFocus" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesInputText"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesInputText" /></UpdatedControls></Telerik:AjaxSetting>


            <Telerik:AjaxSetting AjaxControlID="PropertiesSelectionType"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="ControlProperties" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesSelectionDataSource"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesSelectionDataSource" /><Telerik:AjaxUpdatedControl ControlID="ControlProperties" /></UpdatedControls></Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="PropertiesSelectionReferenceSource"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesSelectionReferenceSource" /><Telerik:AjaxUpdatedControl ControlID="ControlProperties" /></UpdatedControls></Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="PropertiesSelectionReferenceDefault"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesSelectionReferenceDefault" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesSelectionAllowCustomText"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesSelectionAllowCustomText" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesSelectionMode"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesSelectionMode" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesSelectionColumns"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesSelectionColumns" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesSelectionRows"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesSelectionRows" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesSelectionDirection"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesSelectionDirection" /></UpdatedControls></Telerik:AjaxSetting>


            <Telerik:AjaxSetting AjaxControlID="PropertiesButtonText"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesButtonText" /></UpdatedControls></Telerik:AjaxSetting>


            <Telerik:AjaxSetting AjaxControlID="PropertiesEntityType"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesEntityType" /><Telerik:AjaxUpdatedControl ControlID="PropertiesControlBindableProperty" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesEntityDisplayStyle"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesEntityDisplayStyle" /><Telerik:AjaxUpdatedControl ControlID="ControlProperties" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesServiceSelection"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesServiceSelection" /></UpdatedControls></Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="PropertiesServiceDateVisibleSelection"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesServiceDateVisibleSelection" /></UpdatedControls></Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="PropertiesServiceMostRecentDateVisibleSelection"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesServiceMostRecentDateVisibleSelection" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesMetricSelection"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesMetricSelection" /></UpdatedControls></Telerik:AjaxSetting>


            <Telerik:AjaxSetting AjaxControlID="PropertiesLabelText"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesLabelText" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesLabelVisible"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesLabelVisible" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="PropertiesLabelPosition"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="InteractiveFormDesigner" /><Telerik:AjaxUpdatedControl ControlID="PropertiesLabelPosition" /></UpdatedControls></Telerik:AjaxSetting>
            

            <Telerik:AjaxSetting AjaxControlID="SelectionItemsGrid"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="SelectionItemsGrid" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>

       </AjaxSettings>    
    

    </Telerik:RadAjaxManager>

    <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanel" runat="server"></Telerik:RadAjaxLoadingPanel>
    
    <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanelWhiteout" Transparency="75" InitialDelayTime="100" MinDisplayTime="0" Skin="" EnableAjaxSkinRendering="false" runat="server">
    
        <div style="background-color: white; min-height: 100%; height: 100%; opacity: 0.25; filter: alpha(opacity=25); z-index: 10">
    
        </div>
            
    </Telerik:RadAjaxLoadingPanel>
    
    <asp:Button ID="RequestServerRefresh" OnClick="RequestServerRefresh_OnClick" runat="server" />

</div>


<div id="ApplicationTitleBar">

    <table width="100%" cellpadding="0" cellspacing="0">
    
        <tr class="BackgroundColorDark" style="height: 36px;">

            <td style="width: 100%; color: White; font-weight: bold; padding-left: .125in; white-space: nowrap">

                <asp:Label ID="ApplicationTitle" Text="" runat="server" />
                
            </td>

            <td style="padding-left: .125in; padding-right: .25in"><a class="NoDecoration ColorLight HoverTextWhiteBold" href="/Application/Workspace/Workspace.aspx" style="white-space: nowrap; font-weight: bold; text-align: center;">Home</a></td>
            
            <td style="padding-left: .125in; padding-right: .25in"><a class="NoDecoration ColorLight HoverTextWhiteBold" href="/LogOff.aspx" style="white-space: nowrap; font-weight: bold; text-align: center;">Logout</a></td>
    
        </tr>
     
        <tr><td colspan="5" style="width: 100%; height: 1px;" class="BackgroundColorComplementLight"></td></tr>   
        
     </table>

</div>


<div id="DesignerTitleBar" runat="server">

    <Telerik:RadTabStrip ID="ToolbarTabStrip" MultiPageID="ToolbarMultiPage" SelectedIndex="0"  runat="server">
    
        <Tabs>
        
            <Telerik:RadTab Text="Home" />
            
            <Telerik:RadTab Text="Style" />
            
        </Tabs>
    
    </Telerik:RadTabStrip>
    
    <Telerik:RadMultiPage ID="ToolbarMultiPage" SelectedIndex="0" runat="server">
    
        <Telerik:RadPageView ID="ToolbarHomePage" runat="server">
        
            <table cellpadding="0" cellspacing="0"><tr><td>
        
            <Telerik:RadToolBar ID="HomeToolbar" Height="66" OnButtonClick="HomeToolbar_OnButtonClick" runat="server">
    
                <Items>
                
                    <Telerik:RadToolBarButton Text="<u>N</u>ew" Value="New" AccessKey="N" ImagePosition="AboveText" ImageUrl="/Images/Common32/Document.png" />
                    
                    <Telerik:RadToolBarButton Text="<u>O</u>pen" Value="Open" AccessKey="O" ImagePosition="AboveText" ImageUrl="/Images/Common32/FolderOpen.png" />
                    
                    <Telerik:RadToolBarButton Text="<u>S</u>ave" Value="Save" AccessKey="S" ImagePosition="AboveText" ImageUrl="/Images/Common32/Save.png" />
                
                    <Telerik:RadToolBarButton IsSeparator="true" />
                    
                    <Telerik:RadToolBarButton Text="Mode: Form" Value="DesignerModeForm" Group="DesignerMode" ImagePosition="AboveText" CheckOnClick="true" AllowSelfUnCheck="true" ImageUrl="/Images/Common32/Form.png" />
                    
                    <Telerik:RadToolBarButton Text="Mode: Tree" Value="DesignerModeTree" Group="DesignerMode" ImagePosition="AboveText" CheckOnClick="true" AllowSelfUnCheck="true" ImageUrl="/Images/Common32/Document2DatabaseTable.png" />

                    <Telerik:RadToolBarButton IsSeparator="true" />
                
                    <Telerik:RadToolBarButton Text="<u>C</u>ompile" Value="Compile" AccessKey="C" ImagePosition="AboveText" ImageUrl="/Images/Common32/Compile.png" />
                    
                    <Telerik:RadToolBarButton Text="<u>P</u>review" Value="Preview" AccessKey="P" ImagePosition="AboveText" ImageUrl="/Images/Common32/PrintPreview.png" />
                    
                    <Telerik:RadToolBarButton IsSeparator="true" Visible="false" />
                
                    <Telerik:RadToolBarButton Text="E<u>x</u>port" Value="Export" AccessKey="X" ImagePosition="AboveText" ImageUrl="/Images/Common32/DocumentExport.png" Visible="false" />
                    
                    <Telerik:RadToolBarButton Text="<u>I</u>mport" Value="Import" AccessKey="I" ImagePosition="AboveText" ImageUrl="/Images/Common32/DocumentImport.png" Visible="false" />

                </Items>                

            </Telerik:RadToolBar>    
            
            </td></tr></table>
    
        </Telerik:RadPageView>
        
        <Telerik:RadPageView ID="ToolbarStylePage" runat="server">

            <div style="height: 72px; overflow: hidden;">

            <table cellpadding="0" cellspacing="0" id="StyleToolbarPanel" runat="server"><tr>
                
                
                <td valign="top" style="padding-right: 0px;">
                
                    <Telerik:RadToolBar ID="StyleToolbarControlSelection" Height="66" OnButtonClick="StyleToolbar_OnButtonClick" runat="server">

                        <Items>
                        
                            <Telerik:RadToolBarButton Text="Control" Value="Control" ImagePosition="AboveText" Group="ControlLabelSelection" ImageUrl="/Images/Common32/Document.png" CheckOnClick="true" AllowSelfUnCheck="true" />
                            
                            <Telerik:RadToolBarButton IsSeparator="true" />
                            
                            <Telerik:RadToolBarButton Text="Label"   Value="Label"   ImagePosition="AboveText" Group="ControlLabelSelection" ImageUrl="/Images/Common32/Label.png" CheckOnClick="true" AllowSelfUnCheck="true" />
                            
                        </Items>
                        
                    </Telerik:RadToolBar>            

                </td>
                                  
                <td valign="top" style="padding-right: 2px;">
           
                    <table cellpadding="0" cellspacing="0">
                    
                    <tr><td>
                
                    <Telerik:RadToolBar ID="StyleToolbarFont1" runat="server">
                    
                        <Items>
                        
                            <Telerik:RadToolBarButton Value="TextFont">
                                
                                <ItemTemplate>
                                
                                    <table style="height: 30px;"><tr><td valign="middle">
                                        
                                    <Telerik:RadComboBox ID="StyleToolbarFont1FontSelection" Width="145" OnSelectedIndexChanged="StyleToolbarFont1FontSelection_OnSelectedIndexChanged" AutoPostBack="true"  runat="server">
                                   
                                        <ItemTemplate>
                                                                                
                                            <div style="padding:4px 20px 4px 4px; white-space:nowrap; font-family: <%# Container.Value %>" 
                                            
                                            onmouseover="
                                            
                                                this.style.backgroundColor='transparent';
                                                
                                                this.style.backgroundImage='url(/Images/Backgrounds/rcbDropDownHover.gif)';
                                                
                                                this.style.backgroundRepeat='repeat-x';
                                                
                                                this.style.backgroundPosition='0 50%';
                                                
                                                this.style.borderColor='#d7d0b3 #dbce99';
                                                
                                                this.style.borderStyle='Solid';
                                                
                                                this.style.borderWidth='1px';
                                                
                                                this.style.padding='3px 19px 3px 3px;';
                                            
                                            "
                                            
                                            onmouseout="this.style.background=''; this.style.border=''; this.style.padding='4px 20px 4px 4px';"
                                            
                                            ><%# Container.Text %></div>
                                      
                                        </ItemTemplate>

                                        <Items>
                                            <Telerik:RadComboBoxItem Text="Font"                Value="" />
                                            <Telerik:RadComboBoxItem Text="Arial"               Value="Arial, Helvetica, sans-serif" />
                                            <Telerik:RadComboBoxItem Text="Arial Black"         Value="Arial Black, Gadget, sans-serif" />
                                            <Telerik:RadComboBoxItem Text="Comic Sans MS"       Value="Comic Sans MS,cursive" />
                                            <Telerik:RadComboBoxItem Text="Courier"             Value="Courier,monospace" />
                                            <Telerik:RadComboBoxItem Text="Courier New"         Value="Courier New,monospace" />
                                            <Telerik:RadComboBoxItem Text="Georgia"             Value="Georgia,serif" />
                                            <Telerik:RadComboBoxItem Text="Impact"              Value="Impact, Charcoal, sans-serif" />
                                            <Telerik:RadComboBoxItem Text="Lucida Console"      Value="Lucida Console, Monaco, monospace" />
                                            <Telerik:RadComboBoxItem Text="Lucida Sans"         Value="Lucida Sans Unicode, Lucida Grande, sans-serif" />
                                            <Telerik:RadComboBoxItem Text="Palatino Linotype"   Value="Palatino Linotype, Book Antiqua, Palatino, serif" />
                                            <Telerik:RadComboBoxItem Text="Tahoma"              Value="Tahoma, Geneva, sans-serif" />
                                            <Telerik:RadComboBoxItem Text="Times New Roman"     Value="Times New Roman, Times, serif" />
                                            <Telerik:RadComboBoxItem Text="Trebuchet"           Value="Trebuchet MS, Helvetica, sans-serif" />
                                            <Telerik:RadComboBoxItem Text="Verdana"             Value="Verdana, Geneva, sans-serif" />
                                        </Items>

                                    </Telerik:RadComboBox>                                        
                                    
                                    </td></tr></table>
                                
                                </ItemTemplate>
                            
                            </Telerik:RadToolBarButton>
                            
                            <Telerik:RadToolBarButton Value="TextFontSize">
                            
                                <ItemTemplate>
                                
                                    <table style="height: 30px;"><tr><td valign="middle">
                                    
                                    <Telerik:RadComboBox ID="StyleToolbarFont1SizeSelection" Width="50" OnSelectedIndexChanged="StyleToolbarFont1SizeSelection_OnSelectedIndexChanged" AutoPostBack="true"  runat="server">
                                        <Items>
                                            <Telerik:RadComboBoxItem Text="Size" />
                                            <Telerik:RadComboBoxItem Text="8"  Value="8"  />
                                            <Telerik:RadComboBoxItem Text="9"  Value="9"  />
                                            <Telerik:RadComboBoxItem Text="10" Value="10" />
                                            <Telerik:RadComboBoxItem Text="11" Value="11" />
                                            <Telerik:RadComboBoxItem Text="12" Value="12" />
                                            <Telerik:RadComboBoxItem Text="14" Value="14" />
                                            <Telerik:RadComboBoxItem Text="16" Value="16" />
                                            <Telerik:RadComboBoxItem Text="18" Value="18" />
                                            <Telerik:RadComboBoxItem Text="20" Value="20" />
                                            <Telerik:RadComboBoxItem Text="22" Value="22" />
                                            <Telerik:RadComboBoxItem Text="24" Value="24" />
                                            <Telerik:RadComboBoxItem Text="26" Value="26" />
                                            <Telerik:RadComboBoxItem Text="28" Value="28" />
                                            <Telerik:RadComboBoxItem Text="36" Value="36" />
                                            <Telerik:RadComboBoxItem Text="48" Value="48" />
                                            <Telerik:RadComboBoxItem Text="72" Value="72" />
                                        </Items>
                                    </Telerik:RadComboBox>                                        
                                
                                    </td></tr></table>
                                    
                                </ItemTemplate>
                            
                            </Telerik:RadToolBarButton>
                        
                        </Items>
                    
                    </Telerik:RadToolBar>
                    
                    </td></tr>
                    
                    <tr><td>
                    
                    <Telerik:RadToolBar ID="StyleToolbarFont2" OnButtonClick="StyleToolbar_OnButtonClick"  runat="server">
                    
                        <Items>
                        
                            <Telerik:RadToolBarButton ImageUrl="/Images/Common20/TextBold.png"          ToolTip="Bold" Value="TextBold" Group="TextBold" CheckOnClick="true"  AllowSelfUnCheck="true" />
                            
                            <Telerik:RadToolBarButton ImageUrl="/Images/Common20/TextItalic.png"        ToolTip="Italic" Value="TextItalic" Group="TextItalic" CheckOnClick="true" AllowSelfUnCheck="true" />
                            
                            <Telerik:RadToolBarButton ImageUrl="/Images/Common20/TextUnderline.png"     ToolTip="Underline" Value="TextUnderline" Group="TextUnderline"  CheckOnClick="true" AllowSelfUnCheck="true" />
                            
                            <Telerik:RadToolBarButton ImageUrl="/Images/Common20/TextStrikethrough.png" ToolTip="Strikethrough" Value="TextStrikethrough"  Group="TextStrikethrough" CheckOnClick="true" AllowSelfUnCheck="true" />

                            <Telerik:RadToolBarButton ImageUrl="/Images/Common20/TextSmallCaps.png"     ToolTip="Small Caps" Value="TextSmallCaps"  Group="TextSmallCaps" CheckOnClick="true" AllowSelfUnCheck="true"  />
                            
                            <Telerik:RadToolBarButton IsSeparator="true" />
                            
                            <Telerik:RadToolBarButton Height="29" Group="FontColor">
                            
                                <ItemTemplate>
                                
                                    <table cellpadding="0" cellspacing="0">
                                    
                                        <tr><td align="center" style="font-size: 16px; padding-top: 2px; padding-left: 4px; line-height: 100%;">A</td></tr>
                                    
                                        <tr><td style="height: 4px; padding-left: 4px"><div id="FontColorSample" style="background-color: Black; width: 100%; height: 4px" runat="server"></div></td></tr>
                                    
                                    </table>
                                
                                </ItemTemplate>
                            
                            </Telerik:RadToolBarButton>
                            
                            <Telerik:RadToolBarSplitButton Value="TextColor" ToolTip="Font Color" PostBack="false">
                            
                                <Buttons>
                                
                                    <Telerik:RadToolBarButton PostBack="false">
                                    
                                        <ItemTemplate>
                                        
                                            <Telerik:RadColorPicker ID="StyleFontColor" Width="400" OnColorChanged="StyleToolbarFont1FontColor_OnColorChanged" AutoPostBack="true" ShowIcon="false" runat="server" />
                                        
                                        </ItemTemplate>
                                    
                                    </Telerik:RadToolBarButton>
                                
                                </Buttons>
                            
                            </Telerik:RadToolBarSplitButton>
                            
                        </Items>
                        
                    </Telerik:RadToolBar>
                    
                    </td>
                    
                    </tr></table>
                
                </td>
                    
                <td valign="top" style="padding-right: 4px;">
                    
                    <table cellpadding="0" cellspacing="0" style="width: 208px">
                    
                    <tr><td>
                
                    <Telerik:RadToolBar ID="StyleToolbarParagraph1" OnButtonClick="StyleToolbar_OnButtonClick"  runat="server">
                    
                        <Items>
                        
                            <Telerik:RadToolBarButton ImageUrl="/Images/Common20/TextAlignLeft.png"    ToolTip="Align Left"    Group="ParagraphAlign" Value="ParagraphAlignLeft" CheckOnClick="true" AllowSelfUnCheck="true" />
                            
                            <Telerik:RadToolBarButton ImageUrl="/Images/Common20/TextAlignCenter.png"  ToolTip="Align Center"  Group="ParagraphAlign" Value="ParagraphAlignCenter"  CheckOnClick="true" AllowSelfUnCheck="true" />
                            
                            <Telerik:RadToolBarButton ImageUrl="/Images/Common20/TextAlignRight.png"   ToolTip="Align Right"   Group="ParagraphAlign" Value="ParagraphAlignRight"  CheckOnClick="true" AllowSelfUnCheck="true" />
                            
                            <Telerik:RadToolBarButton ImageUrl="/Images/Common20/TextAlignJustify.png" ToolTip="Align Justify" Group="ParagraphAlign" Value="ParagraphAlignJustify"  CheckOnClick="true" AllowSelfUnCheck="true" />
                            
                            <Telerik:RadToolBarButton IsSeparator="true" />

                            <Telerik:RadToolBarSplitButton ImageUrl="/Images/Common20/VerticalAlignMiddle.png" ToolTip="Vertical Align"  Value="ParagraphLineHeight" EnableDefaultButton="false">
                            
                                <Buttons>
                                
                                    <Telerik:RadToolBarButton ImageUrl="/Images/Common20/VerticalAlignTop.png"    Text="Vertical Align Top"    Value="VerticalAlignTop"    Group="VerticalAlign" CheckOnClick="true" AllowSelfUnCheck="true" />
                                    
                                    <Telerik:RadToolBarButton ImageUrl="/Images/Common20/VerticalAlignMiddle.png" Text="Vertical Align Middle" Value="VerticalAlignMiddle" Group="VerticalAlign" CheckOnClick="true" AllowSelfUnCheck="true" />
                                    
                                    <Telerik:RadToolBarButton ImageUrl="/Images/Common20/VerticalAlignBottom.png" Text="Vertical Align Bottom" Value="VerticalAlignBottom" Group="VerticalAlign" CheckOnClick="true" AllowSelfUnCheck="true" />
                                                                    
                                </Buttons>
                            
                            </Telerik:RadToolBarSplitButton>
                            
                            <Telerik:RadToolBarSplitButton ImageUrl="/Images/Common20/LineHeight.png" ToolTip="Line Height" Value="ParagraphLineHeight" EnableDefaultButton="false">
                            
                                <Buttons>
                                
                                    <Telerik:RadToolBarButton Text="100%" Value="LineHeight100%" Group="LineHeight" CheckOnClick="true" AllowSelfUnCheck="true" />
                                    
                                    <Telerik:RadToolBarButton Text="150%" Value="LineHeight150%" Group="LineHeight" CheckOnClick="true" AllowSelfUnCheck="true" />
                                    
                                    <Telerik:RadToolBarButton Text="200%" Value="LineHeight200%" Group="LineHeight" CheckOnClick="true" AllowSelfUnCheck="true" />
                                    
                                    <Telerik:RadToolBarButton Text="250%" Value="LineHeight250%" Group="LineHeight" CheckOnClick="true" AllowSelfUnCheck="true" />
                                    
                                    <Telerik:RadToolBarButton Text="300%" Value="LineHeight300%" Group="LineHeight" CheckOnClick="true" AllowSelfUnCheck="true" />
                                
                                </Buttons>
                            
                            </Telerik:RadToolBarSplitButton>
                            
                        </Items>
                    
                    </Telerik:RadToolBar>
                    
                    </td></tr>
                    
                    <tr><td>
                    
                    <Telerik:RadToolBar ID="StyleToolbarParagraph2" OnButtonClick="StyleToolbar_OnButtonClick"  runat="server">
                    
                        <Items>
                        
                            <Telerik:RadToolBarButton Visible="false" ImageUrl="/Images/Common20/IndentDecrease.png" ToolTip="Decrease Indent" Value="IndentDecrease" />
                         
                            <Telerik:RadToolBarButton Visible="false" ImageUrl="/Images/Common20/IndentIncrease.png" ToolTip="Increase Indent" Value="IndentIncrease" />
                                                
                            <Telerik:RadToolBarSplitButton Value="ParagraphBorder" ImageUrl="/Images/Common20/ParagraphBorderNone.png" ToolTip="Borders" PostBack="false" EnableDefaultButton="false" >
                            
                                <Buttons>
                                
                                    <Telerik:RadToolBarButton ImageUrl="/Images/Common20/ParagraphBorderBottom.png" Value="ParagraphBorderBottom" Text="Bottom Border" />
                                    
                                    <Telerik:RadToolBarButton ImageUrl="/Images/Common20/ParagraphBorderTop.png" Value="ParagraphBorderTop" Text="Top Border" />
                                    
                                    <Telerik:RadToolBarButton ImageUrl="/Images/Common20/ParagraphBorderLeft.png" Value="ParagraphBorderLeft" Text="Left Border" />
                                    
                                    <Telerik:RadToolBarButton ImageUrl="/Images/Common20/ParagraphBorderRight.png" Value="ParagraphBorderRight" Text="Right Border" />
                                    
                                    <Telerik:RadToolBarButton IsSeparator="true" />
                                    
                                    <Telerik:RadToolBarButton ImageUrl="/Images/Common20/ParagraphBorderOutside.png" Value="ParagraphBorderOutside" Text="Outside Borders" />
                                    
                                    <Telerik:RadToolBarButton ImageUrl="/Images/Common20/ParagraphBorderNone.png" Value="ParagraphBorderNone" Text="No Borders" />
                                
                                    <Telerik:RadToolBarButton IsSeparator="true" />

                                    <Telerik:RadToolBarButton ImageUrl="/Images/Common20/ParagraphBorderSolid.png" Value="ParagraphBorderSolid" Group="ParagraphLineStyle" Text="Solid" />
                                    
                                    <Telerik:RadToolBarButton ImageUrl="/Images/Common20/ParagraphBorderDotted.png" Value="ParagraphBorderDotted" Group="ParagraphLineStyle" Text="Dotted" />
                                    
                                    <Telerik:RadToolBarButton ImageUrl="/Images/Common20/ParagraphBorderDashed.png" Value="ParagraphBorderDashed" Group="ParagraphLineStyle" Text="Dashed" />
                                
                                </Buttons>
                            
                            </Telerik:RadToolBarSplitButton>   
                            
                            <Telerik:RadToolBarButton Value="ParagraphBorderSize" ToolTip="Border Size" PostBack="false" >
                            
                                <ItemTemplate>
                                
                                    <table style="height: 29px;"><tr><td valign="middle">
                                    
                                    <Telerik:RadComboBox ID="ParagraphBorderSizeSelection" Width="62" AutoPostBack="true" OnSelectedIndexChanged="ParagraphBorderSizeSelection_OnSelectedIndexChanged"  runat="server">
                                        <Items>
                                            <Telerik:RadComboBoxItem Text="Width" Value="" />
                                            <Telerik:RadComboBoxItem Text="1" Value="1" />
                                            <Telerik:RadComboBoxItem Text="2" Value="2" />
                                            <Telerik:RadComboBoxItem Text="3" Value="3" />
                                            <Telerik:RadComboBoxItem Text="4" Value="4" />
                                            <Telerik:RadComboBoxItem Text="5" Value="5" />
                                            <Telerik:RadComboBoxItem Text="6" Value="6" />
                                            <Telerik:RadComboBoxItem Text="7" Value="7" />
                                            <Telerik:RadComboBoxItem Text="8" Value="8" />
                                            <Telerik:RadComboBoxItem Text="9" Value="9" />
                                        </Items>
                                    </Telerik:RadComboBox>                                        
                                
                                    </td></tr></table>
                                </ItemTemplate>
                            
                            </Telerik:RadToolBarButton>           
                            
                            <Telerik:RadToolBarButton IsSeparator="true" />
                            
                            <Telerik:RadToolBarSplitButton Value="BackgroundColor" ImageUrl="/Images/Common20/Shading.png" PostBack="false" ToolTip="Background Color" EnableDefaultButton="false">
                            
                                <Buttons>
                                
                                    <Telerik:RadToolBarButton PostBack="false">
                                    
                                        <ItemTemplate>
                                        
                                            <Telerik:RadColorPicker ID="StyleParagraph2BackgroundColor" Width="400" OnColorChanged="StyleParagraph2BackgroundColor_OnColorChanged" AutoPostBack="true" ShowIcon="false" runat="server" />
                                        
                                        </ItemTemplate>
                                    
                                    </Telerik:RadToolBarButton>
                                
                                </Buttons>
                            
                            </Telerik:RadToolBarSplitButton>
                                         
                            <Telerik:RadToolBarButton Text="No Wrap" ToolTip="No Wrap" Group="ParagraphNoWrap" Value="ParagraphNoWrap" CheckOnClick="true" AllowSelfUnCheck="true" />                                         
                                                    
                        </Items>
                    
                    </Telerik:RadToolBar>
                
                    </td></tr>
                    
                    </table>
                
                </td>
                
                <td valign="top" style="padding-right: 4px;">
           
                    <table cellpadding="0" cellspacing="0" style="table-layout: auto;">
                    
                    <tr><td><div style="height: 36px; padding-right: 30px; overflow: hidden;">
                
                    <Telerik:RadToolBar ID="StyleToolbarBox1" runat="server">
                    
                        <Items>
                        
                            <Telerik:RadToolBarButton Value="BoxWidth" PostBack="false">
                                
                                <ItemTemplate>
                                
                                    <table style="height: 30px;"><tr>
                                    
                                        <td valign="middle" style="width: 45px; font:11px/18px arial">Width:</td>
                                        
                                        <td><Telerik:RadNumericTextBox ID="StyleToolbarBox1Width" OnTextChanged="StyleToolbarBox1Width_OnTextChanged" AutoPostBack="true" Width="40" MaxLength="4" MinValue="0" MaxValue="9999" NumberFormat-DecimalDigits="3" NumberFormat-GroupSeparator=""   runat="server" /></td>
                                    
                                    </tr></table>
                                
                                </ItemTemplate>
                            
                            </Telerik:RadToolBarButton>
                            
                            <Telerik:RadToolBarButton Value="BoxWidthUnit">
                            
                                <ItemTemplate>
                                
                                    <table style="height: 30px;"><tr><td valign="middle">
                                    
                                    <Telerik:RadComboBox ID="StyleToolbarBox1WidthUnit" EmptyMessage="Units" Width="65" OnSelectedIndexChanged="StyleToolbarBox1WidthUnit_OnSelectedIndexChanged" AutoPostBack="true"  runat="server">
                                        <Items>
                                            <Telerik:RadComboBoxItem Text="%"       Value="%"   />
                                            <Telerik:RadComboBoxItem Text="Pixels"  Value="px"  />
                                            <Telerik:RadComboBoxItem Text="Inches"  Value="in"  />
                                        </Items>
                                    </Telerik:RadComboBox>                                        
                                
                                    </td></tr></table>
                                    
                                </ItemTemplate>
                            
                            </Telerik:RadToolBarButton>
                        
                            <Telerik:RadToolBarButton Value="BoxHeight" PostBack="false">
                                
                                <ItemTemplate>
                                
                                    <table style="height: 30px;"><tr>
                                    
                                        <td valign="middle" style="width: 46px; font:11px/18px arial">Height:</td>
                                        
                                        <td><Telerik:RadNumericTextBox ID="StyleToolbarBox1Height" OnTextChanged="StyleToolbarBox1Height_OnTextChanged" AutoPostBack="true" Width="40" MaxLength="4" MinValue="0" MaxValue="9999" NumberFormat-DecimalDigits="3" NumberFormat-GroupSeparator=""   runat="server" /></td>
                                    
                                    </tr></table>
                                
                                </ItemTemplate>
                            
                            </Telerik:RadToolBarButton>
                            
                            <Telerik:RadToolBarButton Value="BoxHeightUnit">
                            
                                <ItemTemplate>
                                
                                    <table style="height: 30px;"><tr><td valign="middle">
                                    
                                    <Telerik:RadComboBox ID="StyleToolbarBox1HeightUnit" EmptyMessage="Units" Width="65" OnSelectedIndexChanged="StyleToolbarBox1HeightUnit_OnSelectedIndexChanged" AutoPostBack="true"  runat="server">
                                        <Items>
                                            <Telerik:RadComboBoxItem Text="Pixels"  Value="px"  />
                                            <Telerik:RadComboBoxItem Text="Inches"  Value="in"  />
                                        </Items>
                                    </Telerik:RadComboBox>                                        
                                
                                    </td></tr></table>
                                    
                                </ItemTemplate>
                            
                            </Telerik:RadToolBarButton>
                        
                        </Items>
                    
                    </Telerik:RadToolBar>
                    
                    </div></td></tr>
                    
                    <tr><td>
                
                    <Telerik:RadToolBar ID="StyleToolbarBox2" Height="29"  runat="server">
                    
                        <Items>
                        
                            <Telerik:RadToolBarButton Value="BoxMargin" PostBack="false">
                                
                                <ItemTemplate>
                                
                                    <table style="height: 30px;"><tr>
                                    
                                        <td valign="middle" style="width: 45px; font:11px/18px arial">Margin:</td>
                                        
                                        <td><Telerik:RadTextBox ID="StyleToolbarBox2Margin" OnTextChanged="StyleToolbarBox2Margin_OnTextChanged" AutoPostBack="true" Width="115"  runat="server" /></td>
                                    
                                    </tr></table>
                                    
                                </ItemTemplate>
                            
                            </Telerik:RadToolBarButton>
                            
                            <Telerik:RadToolBarButton Value="BoxPadding" PostBack="false">
                                
                                <ItemTemplate>
                                
                                    <table style="height: 30px;"><tr>
                                    
                                        <td valign="middle" style="width: 45px; font:11px/18px arial">Padding:</td>
                                        
                                        <td><Telerik:RadTextBox ID="StyleToolbarBox2Padding" OnTextChanged="StyleToolbarBox2Padding_OnTextChanged" AutoPostBack="true" Width="115"  runat="server" /></td>
                                    
                                    </tr></table>
                                
                                </ItemTemplate>
                            
                            </Telerik:RadToolBarButton>
                        
                        </Items>
                    
                    </Telerik:RadToolBar>
                    
                    </td></tr>
                    
                    </table>
                    
                </td>
           
                <td valign="top" style="padding-right: 0px;">
                
                    <table cellpadding="0" cellspacing="0" style="table-layout: auto;">
                    
                    <tr><td><div style="height: 72px; overflow: hidden;">
                    
                    <Telerik:RadToolBar ID="StyleToolbarPreview" Height="66" OnButtonClick="HomeToolbar_OnButtonClick" runat="server">

                        <Items>
                        
                            <Telerik:RadToolBarButton Text="<u>P</u>review" Value="Preview" AccessKey="P" ImagePosition="AboveText" ImageUrl="/Images/Common32/PrintPreview.png" />
                    
                        </Items>
                        
                    </Telerik:RadToolBar>      
                    
                    </div>     
                
                    </td></tr>
                    
                    </table>
                    
                </td>
                    
            </tr></table>
            
            </div>
            
        </Telerik:RadPageView>
        
    </Telerik:RadMultiPage>
    
</div>

    
<Telerik:RadSplitter ID="DesignerSplitter" VisibleDuringInit="false"  runat="server">

    <Telerik:RadPane ID="DesignerPaneLeft" MinWidth="22" Scrolling="None" Locked="true" OnClientResized="Pane_OnChange" runat="server">
            
        <Telerik:RadSlidingZone ID="DesignerPaneLeftSlidingZone" Width="22" DockedPaneId="PaneToolbox" runat="server">
        
            <Telerik:RadSlidingPane ID="PaneToolbox" Title="Toolbox" BackColor="#4f81bd" OnClientDocked="Pane_OnChange" OnClientResized="Pane_OnChange" OnClientUndocked="Pane_OnChange" OnClientExpanded="Pane_OnChange" OnClientCollapsed="Pane_OnChange" EnableResize="true" runat="server">
            
                <div id="Div1" class="DesignerToolbarItem">
            
                    <table class="DesignerToolbarItemTable" cellpadding="0" cellspacing="0" style="line-height: 150%">
                    
                        <tr><td class="DesignerToolbarItemTableCellText"><input id="ToggleTitleBars" type="checkbox" onclick="TitleBars_ShowHide (this.checked);" />Title Bars</td></tr>
                     
                        <tr><td class="DesignerToolbarItemTableCellText"><input id="ToggleDropZones" type="checkbox" onclick="DropZones_ShowHide (this.checked);" />Drop Zones</td></tr>
                        
                    </table>
                
                </div>
                
                <div style="padding-top: 12px;"> </div>
                
                <div id="ToolboxItem_Section" class="DesignerToolbarItem" formControlType="Section" onmousedown="ToolbarItem_OnDragBegin (event);">
            
                    <table class="DesignerToolbarItemTable" cellpadding="0" cellspacing="0">
                    
                        <tr>
                            
                            <td class="DesignerToolbarItemTableCellImage"><img alt="Section" title="Section" src="/Images/Common32/SectionBreakContinuous.png" /></td>

                            <td class="DesignerToolbarItemTableCellText">Section</td>
                            
                            <td class="DesignerToolbarItemTableCellArrow"><img alt="Drag" title="Section" src="/Images/Common32/PointerRight.png" /></td>
                        
                        </tr>
                     
                    </table>
                
                </div>
                
                <div style="padding-top: 4px;"> </div>
                
                <div id="ToolboxItem_SectionColumn" class="DesignerToolbarItem" formControlType="SectionColumn" onmousedown="ToolbarItem_OnDragBegin (event);">
            
                    <table class="DesignerToolbarItemTable" cellpadding="0" cellspacing="0">
                    
                        <tr>
                            
                            <td class="DesignerToolbarItemTableCellImage"><img alt="Section" title="Section" src="/Images/Common32/Columns2.png" /></td>

                            <td class="DesignerToolbarItemTableCellText">Column</td>
                            
                            <td class="DesignerToolbarItemTableCellArrow"><img alt="Drag" title="Section" src="/Images/Common32/PointerRight.png" /></td>
                        
                        </tr>
                     
                    </table>
                
                </div>
                
                <div style="padding-top: 12px;"> </div>
                
                <div id="ToolboxItem_Label" class="DesignerToolbarItem" formControlType="Text" onmousedown="ToolbarItem_OnDragBegin (event);" style="display: none;">
                
                    <table class="DesignerToolbarItemTable" cellpadding="0" cellspacing="0">
                    
                        <tr>
                            
                            <td class="DesignerToolbarItemTableCellImage"><img alt="Text" title="Text" src="/Images/Common32/Label.png" /></td>

                            <td class="DesignerToolbarItemTableCellText">Label</td>
                        
                            <td class="DesignerToolbarItemTableCellArrow"><img alt="Drag" title="Text" src="/Images/Common32/PointerRight.png" /></td>
                        
                        </tr>
                        
                    </table>
                    
                </div>                        
                
                <div style="padding-top: 4px; display: none"> </div>
                
                <div id="ToolboxItem_Text" class="DesignerToolbarItem" formControlType="Text" onmousedown="ToolbarItem_OnDragBegin (event);">
                
                    <table class="DesignerToolbarItemTable" cellpadding="0" cellspacing="0">
                    
                        <tr>
                            
                            <td class="DesignerToolbarItemTableCellImage"><img alt="Text" title="Text" src="/Images/Common32/Text.png" /></td>

                            <td class="DesignerToolbarItemTableCellText">Text</td>
                        
                            <td class="DesignerToolbarItemTableCellArrow"><img alt="Drag" title="Text" src="/Images/Common32/PointerRight.png" /></td>
                        
                        </tr>
                        
                    </table>
                    
                </div>                        
                
                <div style="padding-top: 4px;"> </div>
                
                <div id="ToolboxItem_Input" class="DesignerToolbarItem" formControlType="Input" onmousedown="ToolbarItem_OnDragBegin (event);">
                
                    <table class="DesignerToolbarItemTable" cellpadding="0" cellspacing="0">
                    
                        <tr>
                            
                            <td class="DesignerToolbarItemTableCellImage"><img alt="Input" title="Input" src="/Images/Common32/Textbox.png" /></td>

                            <td class="DesignerToolbarItemTableCellText">Input</td>
                        
                            <td class="DesignerToolbarItemTableCellArrow"><img alt="Drag" title="Input" src="/Images/Common32/PointerRight.png" /></td>
                        
                        </tr>
                        
                    </table>
                    
                </div>
                
                <div style="padding-top: 4px;"> </div>
                
                <div id="ToolboxItem_Selection" class="DesignerToolbarItem" formControlType="Selection" onmousedown="ToolbarItem_OnDragBegin (event);">
                
                    <table class="DesignerToolbarItemTable" cellpadding="0" cellspacing="0">
                    
                        <tr>
                            
                            <td class="DesignerToolbarItemTableCellImage"><img alt="Selection" title="Selection" src="/Images/Common32/SelectionControls.png" /></td>

                            <td class="DesignerToolbarItemTableCellText">Selection</td>
                        
                            <td class="DesignerToolbarItemTableCellArrow"><img alt="Drag" title="Selection" src="/Images/Common32/PointerRight.png" /></td>
                        
                        </tr>
                        
                    </table>
                    
                </div>
                
                <div style="padding-top: 4px;"> </div>
                
                <div id="ToolboxItem_Button" class="DesignerToolbarItem" formControlType="Button" onmousedown="ToolbarItem_OnDragBegin (event);">
                
                    <table class="DesignerToolbarItemTable" cellpadding="0" cellspacing="0">
                    
                        <tr>
                            
                            <td class="DesignerToolbarItemTableCellImage"><img alt="Button" title="Button" src="/Images/Common32/Button.png" /></td>

                            <td class="DesignerToolbarItemTableCellText">Button</td>
                        
                            <td class="DesignerToolbarItemTableCellArrow"><img alt="Drag" title="Button" src="/Images/Common32/PointerRight.png" /></td>
                        
                        </tr>
                        
                    </table>
                    
                </div>
                
                
                <div style="padding-top: 12px;"> </div>
                
                <div id="ToolbarItem_Entity" class="DesignerToolbarItem" formControlType="Entity" onmousedown="ToolbarItem_OnDragBegin (event);">
                
                    <table class="DesignerToolbarItemTable" cellpadding="0" cellspacing="0">
                    
                        <tr>
                            
                            <td class="DesignerToolbarItemTableCellImage"><img alt="Entity" title="Entity" src="/Images/Common32/UserProfile.png" /></td>

                            <td class="DesignerToolbarItemTableCellText">Entity</td>
                        
                            <td class="DesignerToolbarItemTableCellArrow"><img alt="Drag" title="Entity" src="/Images/Common32/PointerRight.png" /></td>
                        
                        </tr>
                        
                    </table>
                    
                </div>      
                
                <div style="padding-top: 4px;"> </div>
                
                <div id="ToolbarItem_Collection" class="DesignerToolbarItem" formControlType="Collection" onmousedown="ToolbarItem_OnDragBegin (event);">
                
                    <table class="DesignerToolbarItemTable" cellpadding="0" cellspacing="0">
                    
                        <tr>
                            
                            <td class="DesignerToolbarItemTableCellImage"><img alt="Entity" title="Entity" src="/Images/Common32/Collection.png" /></td>

                            <td class="DesignerToolbarItemTableCellText">Collection</td>
                        
                            <td class="DesignerToolbarItemTableCellArrow"><img alt="Drag" title="Entity" src="/Images/Common32/PointerRight.png" /></td>
                        
                        </tr>
                        
                    </table>
                    
                </div>      
                
                <div style="padding-top: 4px;"> </div>
                
                <div id="ToolbarItem_Address" class="DesignerToolbarItem" formControlType="Address" onmousedown="ToolbarItem_OnDragBegin (event);">
                
                    <table class="DesignerToolbarItemTable" cellpadding="0" cellspacing="0">
                    
                        <tr>
                            
                            <td class="DesignerToolbarItemTableCellImage"><img alt="Address" title="Address" src="/Images/Common32/Address.png" /></td>

                            <td class="DesignerToolbarItemTableCellText">Address</td>
                        
                            <td class="DesignerToolbarItemTableCellArrow"><img alt="Drag" title="Address" src="/Images/Common32/PointerRight.png" /></td>
                        
                        </tr>
                        
                    </table>
                    
                </div>      
                
                <div style="padding-top: 4px;"> </div>
                
                <div id="ToolbarItem_Service" class="DesignerToolbarItem" formControlType="Service" onmousedown="ToolbarItem_OnDragBegin (event);">
                
                    <table class="DesignerToolbarItemTable" cellpadding="0" cellspacing="0">
                    
                        <tr>
                            
                            <td class="DesignerToolbarItemTableCellImage"><img alt="Entity" title="Entity" src="/Images/Common32/Service.png" /></td>

                            <td class="DesignerToolbarItemTableCellText">Service</td>
                        
                            <td class="DesignerToolbarItemTableCellArrow"><img alt="Drag" title="Entity" src="/Images/Common32/PointerRight.png" /></td>
                        
                        </tr>
                        
                    </table>
                    
                </div>      
                
                <div style="padding-top: 4px;"> </div>
                
                <div id="ToolbarItem_Metric" class="DesignerToolbarItem" formControlType="Metric" onmousedown="ToolbarItem_OnDragBegin (event);">
                
                    <table class="DesignerToolbarItemTable" cellpadding="0" cellspacing="0">
                    
                        <tr>
                            
                            <td class="DesignerToolbarItemTableCellImage"><img alt="Entity" title="Entity" src="/Images/Common32/Metric.png" /></td>

                            <td class="DesignerToolbarItemTableCellText">Metric</td>
                        
                            <td class="DesignerToolbarItemTableCellArrow"><img alt="Drag" title="Entity" src="/Images/Common32/PointerRight.png" /></td>
                        
                        </tr>
                        
                    </table>
                    
                </div>      

            </Telerik:RadSlidingPane>
        
        </Telerik:RadSlidingZone>
          
    </Telerik:RadPane>

    <Telerik:RadPane id="DesignerPaneMain" Scrolling="none" runat="server">
    
        <Telerik:RadSplitter ID="DocumentSplitter" VisibleDuringInit="false"  Orientation="Horizontal" BackColor="AliceBlue" ResizeWithParentPane="true" runat="server">
        
            <Telerik:RadPane ID="PaneDocument" Scrolling="Both" Height="800" BackColor="#ababab" runat="server">
            
                <div id="DocumentActions" style="display: none">
                
                    <asp:TextBox ID="DropItemSource" Text="No Source" runat="server" />
                    
                    <asp:TextBox ID="DropItemDestination" Text="No Destination" runat="server" />
                
                    <asp:Button ID="DropControl" OnClick="DropControl_OnClick" runat="server" />
                    
                    <asp:TextBox ID="DeleteControlClientId" Text="No Item" runat = "server" />
                    
                    <asp:Button Id="DeleteControl" OnClick="DeleteControl_OnClick" runat="server" />
                    
                    <asp:Button ID="SelectControlProperties" OnClick="SelectControlProperties_OnClick" Text="" runat="server" />

                </div>
            
                <div id="InteractiveFormDesignerContainer" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%"><tr><td align="center" valign="top">
            
                <div id="InteractiveFormDesigner" style="width: 8in; min-height: 1in; text-align: left; margin: 8px; padding: 4px; border: solid 1px black; background-color: White; overflow: visible;" runat="server">
                                        
                
                </div>
                               
                </td></tr></table>
                
                </div>
                                
                <div id="FormExplorerTreeContainer" runat="server">
            
                <Telerik:RadTreeView ID="FormExplorerTree" AllowNodeEditing="true" OnNodeClick="FormExplorerTree_OnNodeClick" OnNodeEdit="FormExplorerTree_OnNodeEdit" BackColor="White" runat="server">
                
                    <Nodes></Nodes>
               
                </Telerik:RadTreeView>
                
                </div>
                
            </Telerik:RadPane>
        
            <Telerik:RadPane ID="PaneOutput" Scrolling="Both" runat="server">
            
                <Telerik:RadSlidingZone ID="OutputPaneSlidingZone" Height="22" SlideDirection="Top" runat="server">
                
                    <Telerik:RadSlidingPane ID="PaneCompileOutput" MinHeight="22" Title="Compile Output" OnClientDocked="Pane_OnChange" OnClientResized="Pane_OnChange" OnClientUndocked="Pane_OnChange" OnClientExpanded="Pane_OnChange" OnClientCollapsed="Pane_OnChange" EnableResize="true" runat="server">
                    
                        <Telerik:RadGrid ID="CompileMessagesGrid" AutoGenerateColumns="false" AllowAutomaticUpdates="false" AllowMultiRowEdit="true" AllowSorting="true"  runat="server" >
                        
                            <ClientSettings>
                                <Selecting AllowRowSelect="true" />
                            
                            </ClientSettings>
                             
                            <MasterTableView  >
                                
                                <Columns>
                                         
                                    <Telerik:GridBoundColumn DataField="MessageType" ReadOnly="true" UniqueName="MessageType" HeaderText="Type" Visible="true">
                                        <ItemStyle HorizontalAlign="Left" />
                                       
                                    </Telerik:GridBoundColumn>
                                
                                    <Telerik:GridBoundColumn DataField="Description" ReadOnly="true" UniqueName="Description" HeaderText="Description" Visible="true">
                                        <ItemStyle HorizontalAlign="Left" />
                                       
                                    </Telerik:GridBoundColumn>

                                    <Telerik:GridBoundColumn DataField="ControlId" ReadOnly="true" UniqueName="ControlId" HeaderText="Control Id" Visible="true">
                                        <ItemStyle HorizontalAlign="Left" />
                                       
                                    </Telerik:GridBoundColumn>

                                    <Telerik:GridBoundColumn DataField="ControlType" ReadOnly="true" UniqueName="ControlType" HeaderText="Control Type" Visible="true">
                                        <ItemStyle HorizontalAlign="Left" />
                                       
                                    </Telerik:GridBoundColumn>

                                    <Telerik:GridBoundColumn DataField="ControlName" ReadOnly="true" UniqueName="ControlName" HeaderText="Control Name" Visible="true">
                                        <ItemStyle HorizontalAlign="Left" />
                                       
                                    </Telerik:GridBoundColumn>
                                
                                </Columns>
                            
                            </MasterTableView>
                        
                        </Telerik:RadGrid>
                                            
                    </Telerik:RadSlidingPane>
            
                </Telerik:RadSlidingZone>                
            
            </Telerik:RadPane>
        
        </Telerik:RadSplitter>
       
    </Telerik:RadPane>
    
    <Telerik:RadPane ID="DesignerPaneRight" MinWidth="22" Locked="true" OnClientResized="Pane_OnChange" runat="server"  >
            
        <Telerik:RadSlidingZone ID="DesignerPaneRightSlidingZone" Width="22" DockedPaneId="PaneProperties" SlideDirection="Left"  runat="server">

            <Telerik:RadSlidingPane ID="PaneProperties" Title="Properties" Width="325" DockOnOpen="true" BackColor="#4f81bd" OnClientDocked="Pane_OnChange" OnClientResized="Pane_OnChange" OnClientUndocked="Pane_OnChange" OnClientExpanded="Pane_OnChange" OnClientCollapsed="Pane_OnChange" runat="server">
                                        
                <div id="ControlProperties" style="width: 100%;" runat="server">
                
                    <div id="PropertiesActions" style="display: none">
                        
                        <asp:TextBox ID="PropertiesSelectedControlId" runat="server" />
                                        
                    </div>
                
                    <table cellpadding="0" cellspacing="0" style="width: 100%; table-layout: fixed;">
                    
                        <tr style="height: 24px;"><td><Telerik:RadComboBox ID="PropertiesControlSelection"  Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="PropertiesControlSelection_SelectedIndexChanged" runat="server" /></td></tr>
                        
                    </table>
                    
                    <table id="PropertiesRowTable" cellpadding="1" cellspacing="0" style="width: 100%; table-layout: fixed; border: solid 1px black; font-family: Calibri, Arial; font-size: 10pt; background-color: White;">               
                        
                        <tr id="PropertiesRow_ControlId" runat="server">
                        
                            <td style="width: 33%; border-bottom: solid 1px #CCCCCC;">(Id)</td>
                            
                            <td style="border-bottom: solid 1px #CCCCCC;"><Telerik:RadTextBox ID="PropertiesControlId" Width="95%"  ReadOnly="true" runat="server" /></td>
                        
                        </tr>
                        
                        <tr id="PropertiesRow_ControlName" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Name</td>
                            
                            <td style="border-bottom: solid 1px #CCCCCC;"><Telerik:RadTextBox ID="PropertiesControlName" Width="95%"  OnTextChanged="Properties_OnTextChange" AutoPostBack="true" runat="server" /></td>
                        
                        </tr>
                        
                        <tr id="PropertiesRow_ControlTitle" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Title</td>
                            
                            <td style="border-bottom: solid 1px #CCCCCC;"><Telerik:RadTextBox ID="PropertiesControlTitle" Width="95%"  OnTextChanged="Properties_OnTextChange" AutoPostBack="true" runat="server" /></td>
                        
                        </tr>
                        
                        <tr id="PropertiesRow_ControlTabIndex" style="border-bottom: solid 1px;" runat="server">
                            
                            <td style="border-bottom: solid 1px #CCCCCC;">Tab Index</td>
                            
                            <td style="border-bottom: solid 1px #CCCCCC;"><Telerik:RadNumericTextBox ID="PropertiesControlTabIndex" Width="95%"  OnTextChanged="Properties_OnTextChange" MinValue="-1" MaxValue="3200" NumberFormat-DecimalDigits="0" AutoPostBack="true" runat="server" /></td>
                        
                        </tr>
                            
                        
                        <tr id="PropertiesRow_ControlDisplay" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Display</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesControlDisplay"  Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server">
                                
                                    <Items>
                                    
                                        <Telerik:RadComboBoxItem Text="Enabled" Value="0" />
                                        
                                        <Telerik:RadComboBoxItem Text="Disabled" Value="1" />
                                                                                
                                        <Telerik:RadComboBoxItem Text="Not Visible" Value="2" />

                                    </Items>
                                
                                </Telerik:RadComboBox>
                                
                            </td>                            
                        </tr>                       
                        
                        <tr id="PropertiesRow_ControlEvent" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Event</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesControlEvent"  Width="100%"  AutoPostBack="true" MarkFirstMatch="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server"></Telerik:RadComboBox>
                                
                            </td>
                    
                        </tr>
                        
                        <tr id="PropertiesRow_ControlEventHandler" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Event Handler</td>
                            
                            <td style="border-bottom: solid 1px #CCCCCC; white-space: nowrap">
    
                                <table cellpadding="0" cellspacing="0" style="width: 95%; table-layout: fixed"><tr>
                                    <td style="width: 85%"><Telerik:RadTextBox ID="PropertiesControlEventHandler" Width="90%"  ReadOnly="true" runat="server" /></td>
                                    <td style="width: 15%"><button style="height: 20px" onclick="EventHandlerEditorDialog_OnOpenDialog ('FormControl');">...</button></td>
                                </tr></table>
                                                                                              
                            </td>
                                
                        </tr>
                        
                        <tr id="PropertiesRow_ControlBindableProperty" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Bindable Property</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesControlBindableProperty"  Width="100%"  AutoPostBack="true" MarkFirstMatch="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server"></Telerik:RadComboBox>
                                
                            </td>
                    
                        </tr>

                        <tr id="PropertiesRow_ControlDataBindingControlId" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Data Source</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesControlDataBindingControlId"  Width="100%"  AutoPostBack="true" MarkFirstMatch="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server"></Telerik:RadComboBox>
                                
                            </td>
                    
                        </tr>

                        <tr id="PropertiesRow_ControlDataBindingContext" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Data Binding</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesControlDataBindingContext"  Width="100%"  AutoPostBack="true" AllowCustomText="true" MarkFirstMatch="true" OnSelectedIndexChanged="Properties_OnTextChange" OnTextChanged="Properties_OnTextChange" runat="server"></Telerik:RadComboBox>
                                
                            </td>
                    
                        </tr>    

                        <tr id="PropertiesRow_FormEntityType" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Entity Type</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesFormEntityType"  Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server">
                                
                                    <Items>
                                    
                                        <Telerik:RadComboBoxItem Text="Not Specified" Value="0" />

                                        <Telerik:RadComboBoxItem Text="Member" Value="1" />
                                        
                                        <Telerik:RadComboBoxItem Text="Provider" Value="2" />
                                                                                
                                    </Items>
                                
                                </Telerik:RadComboBox>
                                
                            </td>                            
                        </tr>


                        <tr id="PropertiesRow_FormAllowPrecompileEvents" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Precompile Events</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesFormAllowPrecompileEvents"  Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server">
                                
                                    <Items>
                                    
                                        <Telerik:RadComboBoxItem Text="False" Value="False" />

                                        <Telerik:RadComboBoxItem Text="True" Value="True" />
                                                                                                                        
                                    </Items>
                                
                                </Telerik:RadComboBox>
                                
                            </td>                            
                        </tr>


                        <tr id="PropertiesRow_SectionPageBreakAfter" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Page Break After</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesSectionPageBreakAfter"  Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server">
                                
                                    <Items>
                                    
                                        <Telerik:RadComboBoxItem Text="False" Value="False" Selected="true" />

                                        <Telerik:RadComboBoxItem Text="True" Value="True" />
                                                                                                                        
                                    </Items>
                                
                                </Telerik:RadComboBox>
                                
                            </td>             
                                           
                        </tr>

                        <tr id="PropertiesRow_TextValue" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Text</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;"><button style="height: 20px" onclick="HtmlEditor_Display (event);">...</button></td>
                        
                        </tr>
                        
                        <tr id="PropertiesRow_InputType" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Input Type</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesInputType"  Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server">
                                
                                    <Items>
                                    
                                        <Telerik:RadComboBoxItem Text="Text" Value="0" />
                                        
                                        <Telerik:RadComboBoxItem Text="Numeric" Value="1" />
                                        
                                        <Telerik:RadComboBoxItem Text="Date/Time" Value="2" />
                                    
                                    </Items>
                                
                                </Telerik:RadComboBox>
                                
                            </td>
                    
                        </tr>

                        <tr id="PropertiesRow_InputTextMode" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Text Mode</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesInputTextMode"  Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server">
                                
                                    <Items>
                                    
                                        <Telerik:RadComboBoxItem Text="Multi-Line" Value="1" />
                                        
                                        <Telerik:RadComboBoxItem Text="Password" Value="2" />
                                    
                                        <Telerik:RadComboBoxItem Text="Single Line" Value="0" />
                                        
                                    </Items>
                                
                                </Telerik:RadComboBox>
                                
                            </td>
                        
                        </tr>
                        
                        <tr id="PropertiesRow_InputMaxLength" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Max Length</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;"><Telerik:RadNumericTextBox ID="PropertiesInputMaxLength" Width="95%" MinValue="1"  OnTextChanged="Properties_OnTextChange" Type="Number" NumberFormat-DecimalDigits="0" AutoPostBack="true" runat="server" /></td>
                    
                        </tr>
                        
                        <tr id="PropertiesRow_InputColumns" style="border-bottom: solid 1px;" runat="server">
                                                            
                            <td style="border-bottom: solid 1px #CCCCCC;">Columns</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;"><Telerik:RadNumericTextBox ID="PropertiesInputColumns" Width="95%" MinValue="1" MaxValue="1000"  OnTextChanged="Properties_OnTextChange" NumberFormat-DecimalDigits="0" AutoPostBack="true" runat="server" /></td>
                            
                        </tr>                    
                        
                        <tr id="PropertiesRow_InputRows" style="border-bottom: solid 1px;" runat="server">
                                                            
                            <td style="border-bottom: solid 1px #CCCCCC;">Rows</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;"><Telerik:RadNumericTextBox ID="PropertiesInputRows" Width="95%" MinValue="1" MaxValue="1000"  OnTextChanged="Properties_OnTextChange" NumberFormat-DecimalDigits="0" AutoPostBack="true" runat="server" /></td>
                            
                        </tr>                    

                        <tr id="PropertiesRow_InputWrap" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Wrap</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesInputWrap"  Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server">
                                
                                    <Items>
                                    
                                        <Telerik:RadComboBoxItem Text="False" Value="False" />
                                        
                                        <Telerik:RadComboBoxItem Text="True" Value="True" />
                                                                                
                                    </Items>
                                
                                </Telerik:RadComboBox>
                                
                            </td>                            
                        </tr>
                
                        <tr id="PropertiesRow_InputMask" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Mask</td>
                                   
                            <td style="border-bottom: solid 1px #CCCCCC;"><Telerik:RadTextBox ID="PropertiesInputMask" Width="95%"  OnTextChanged="Properties_OnTextChange" AutoPostBack="true" runat="server" /></td>
                
                        </tr>
                

                        <tr id="PropertiesRow_InputNumericType" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Number Type</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesInputNumericType"  Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server">
                                
                                    <Items>
                                    
                                        <Telerik:RadComboBoxItem Text="Currency" Value="3" />

                                        <Telerik:RadComboBoxItem Text="Integer" Value="0" />
                                        
                                        <Telerik:RadComboBoxItem Text="Number" Value="1" />
                                    
                                        <Telerik:RadComboBoxItem Text="Percent" Value="2" />
                                        
                                    </Items>
                                
                                </Telerik:RadComboBox>
                                
                            </td>
                        
                        </tr>
                        
                        <tr id="PropertiesRow_InputMinValue" style="border-bottom: solid 1px;" runat="server">
                                                            
                            <td style="border-bottom: solid 1px #CCCCCC;">Min Value</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;"><Telerik:RadNumericTextBox ID="PropertiesInputMinValue" Width="95%"  OnTextChanged="Properties_OnTextChange" AutoPostBack="true" runat="server" /></td>
                            
                        </tr>                    
                        
                        <tr id="PropertiesRow_InputMaxValue" style="border-bottom: solid 1px;" runat="server">
                                                            
                            <td style="border-bottom: solid 1px #CCCCCC;">Max Value</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;"><Telerik:RadNumericTextBox ID="PropertiesInputMaxValue" Width="95%"  OnTextChanged="Properties_OnTextChange" AutoPostBack="true" runat="server" /></td>
                            
                        </tr>       
                
                        <tr id="PropertiesRow_InputShowSpinButtons" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Show Spinners</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesInputShowSpinButtons"  Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server">
                                
                                    <Items>
                                    
                                        <Telerik:RadComboBoxItem Text="False" Value="False" />
                                        
                                        <Telerik:RadComboBoxItem Text="True" Value="True" />
                                                                                
                                    </Items>
                                
                                </Telerik:RadComboBox>
                                
                            </td>                            
                        </tr>

                        <tr id="PropertiesRow_InputButtonPosition" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Spinner Position</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesInputButtonPosition"  Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server">
                                
                                    <Items>
                                    
                                        <Telerik:RadComboBoxItem Text="Left" Value="0" />
                                        
                                        <Telerik:RadComboBoxItem Text="Right" Value="1" />
                                    
                                    </Items>
                                
                                </Telerik:RadComboBox>
                                
                            </td>
                        
                        </tr>
                
                
                        <tr id="PropertiesRow_InputDateFormat" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Date Format</td>
                                   
                            <td style="border-bottom: solid 1px #CCCCCC;"><Telerik:RadTextBox ID="PropertiesInputDateFormat" Width="95%"  OnTextChanged="Properties_OnTextChange" AutoPostBack="true" runat="server" /></td>
                
                        </tr>
                        
                        <tr id="PropertiesRow_InputDisplayDateFormat" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Display Format</td>
                                   
                            <td style="border-bottom: solid 1px #CCCCCC;"><Telerik:RadTextBox ID="PropertiesInputDisplayDateFormat" Width="95%"  OnTextChanged="Properties_OnTextChange" AutoPostBack="true" runat="server" /></td>
                
                        </tr>
                        
                        
                        <tr id="PropertiesRow_InputText" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Text</td>
                                   
                            <td style="border-bottom: solid 1px #CCCCCC;"><Telerik:RadTextBox ID="PropertiesInputText" Width="95%"  OnTextChanged="Properties_OnTextChange" AutoPostBack="true" runat="server" /></td>
                
                        </tr>
                        
                        <tr id="PropertiesRow_InputEmptyMessage" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Empty Message</td>
                                   
                            <td style="border-bottom: solid 1px #CCCCCC;"><Telerik:RadTextBox ID="PropertiesInputEmptyMessage" Width="95%"  OnTextChanged="Properties_OnTextChange" AutoPostBack="true" runat="server" /></td>
                
                        </tr>
                        
                        <tr id="PropertiesRow_InputValidation" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Validation</td>
                                   
                            <td style="border-bottom: solid 1px #CCCCCC;"><Telerik:RadTextBox ID="PropertiesInputValidation" Width="95%"  OnTextChanged="Properties_OnTextChange" AutoPostBack="true" runat="server" /></td>
                
                        </tr>

                        <tr id="PropertiesRow_ControlReadOnly" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Read Only</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesControlReadOnly"  Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server">
                                
                                    <Items>
                                    
                                        <Telerik:RadComboBoxItem Text="False" Value="False" />
                                        
                                        <Telerik:RadComboBoxItem Text="True" Value="True" />
                                                                                
                                    </Items>
                                
                                </Telerik:RadComboBox>
                                
                            </td>                            
                        </tr>
                
                        <tr id="PropertiesRow_ControlRequired" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Required</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesControlRequired"  Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server">
                                
                                    <Items>
                                    
                                        <Telerik:RadComboBoxItem Text="False" Value="False" />
                                        
                                        <Telerik:RadComboBoxItem Text="True" Value="True" />
                                                                                
                                    </Items>
                                
                                </Telerik:RadComboBox>
                                
                            </td>                            
                        </tr>
                
                        <tr id="PropertiesRow_InputSelectionOnFocus" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">On Focus</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesInputSelectionOnFocus"  Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server">
                                
                                    <Items>
                                    
                                        <Telerik:RadComboBoxItem Text="Do Nothing" Value="0" />

                                        <Telerik:RadComboBoxItem Text="Caret to Beginning" Value="1" />
                                        
                                        <Telerik:RadComboBoxItem Text="Caret to End" Value="2" />
                                    
                                        <Telerik:RadComboBoxItem Text="Select All" Value="3" />
                                        
                                    </Items>
                                
                                </Telerik:RadComboBox>
                                
                            </td>
                        
                        </tr>                            
                        
                                                   
                        <tr id="PropertiesRow_SelectionType" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Selection Type</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesSelectionType"  Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server">
                                
                                    <Items>
                                    
                                        <Telerik:RadComboBoxItem Text="Drop Down List" Value="0" />
                                        
                                        <Telerik:RadComboBoxItem Text="List Box" Value="1" />
                                        
                                        <Telerik:RadComboBoxItem Text="Checkboxes" Value="2" />
                                    
                                        <Telerik:RadComboBoxItem Text="Radiobuttons" Value="3" />

                                    </Items>
                                
                                </Telerik:RadComboBox>
                                
                            </td>
                    
                        </tr>                            
                        
                        <tr id="PropertiesRow_SelectionDataSource" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Selection Source</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesSelectionDataSource"  Width="100%"  AutoPostBack="true" MarkFirstMatch="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server">
                                
                                    <Items>
                                    
                                        <Telerik:RadComboBoxItem Text="Item List" Value="0" />
                                        
                                        <Telerik:RadComboBoxItem Text="Reference Table" Value="1" />
                                                                                
                                    </Items>
                                
                                </Telerik:RadComboBox>
                                
                            </td>
                        
                        </tr>

                        <tr id="PropertiesRow_SelectionReferenceSource" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Reference Source</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesSelectionReferenceSource"  Width="100%"  AutoPostBack="true" MarkFirstMatch="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server">

                                    <Items>
                                    
                                        <Telerik:RadComboBoxItem Text="Program" Value="0" />
                                        
                                        <Telerik:RadComboBoxItem Text="BenefitPlan" Value="1" />
                                                                                
                                        <Telerik:RadComboBoxItem Text="Ethnicity" Value="2" />
                                        
                                        <Telerik:RadComboBoxItem Text="Language" Value="3" />

                                    </Items>
                                                                        
                                </Telerik:RadComboBox>
                                
                            </td>
                        
                        </tr>
                        
                        <tr id="PropertiesRow_SelectionReferenceDefault" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Reference Default</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesSelectionReferenceDefault"  Width="100%"  AutoPostBack="true" MarkFirstMatch="true" EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true" OnSelectedIndexChanged="Properties_OnTextChange" OnItemsRequested="SelectionReferenceDefault_OnItemsRequested" runat="server">

                                </Telerik:RadComboBox>
                                
                            </td>
                        
                        </tr>
                        
                        <tr id="PropertiesRow_SelectionItems" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Items</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;"><asp:Button ID="PropertiesRow_SelectionItemsMore" Text="..." OnClientClick="SelectionItemsDialog_Show ();"  runat="server" /></td>
                        
                        </tr>                            
                        
                        <tr id="PropertiesRow_SelectionAllowCustomText" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Allow Custom Text</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesSelectionAllowCustomText"  Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server">
                                
                                    <Items>
                                    
                                        <Telerik:RadComboBoxItem Text="True" Value="True" />
                                        
                                        <Telerik:RadComboBoxItem Text="False" Value="False" />
                                                                                
                                    </Items>
                                
                                </Telerik:RadComboBox>
                                
                            </td>
                        
                        </tr>
                        
                        <tr id="PropertiesRow_SelectionMode" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Selection Mode</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesSelectionMode"  Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server">
                                
                                    <Items>
                                    
                                        <Telerik:RadComboBoxItem Text="Single" Value="0" />
                                        
                                        <Telerik:RadComboBoxItem Text="Multiple" Value="1" />
                                                                                
                                    </Items>
                                
                                </Telerik:RadComboBox>
                                
                            </td>
                        
                        </tr>
                        
                        <tr id="PropertiesRow_SelectionColumns" style="border-bottom: solid 1px;" runat="server">
                                                            
                            <td style="border-bottom: solid 1px #CCCCCC;">Columns</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;"><Telerik:RadNumericTextBox ID="PropertiesSelectionColumns" Width="95%" MinValue="1" MaxValue="1000"  OnTextChanged="Properties_OnTextChange" NumberFormat-DecimalDigits="0" AutoPostBack="true" runat="server" /></td>
                            
                        </tr>                    
                        
                        <tr id="PropertiesRow_SelectionRows" style="border-bottom: solid 1px;" runat="server">
                                                            
                            <td style="border-bottom: solid 1px #CCCCCC;">Rows</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;"><Telerik:RadNumericTextBox ID="PropertiesSelectionRows" Width="95%" MinValue="1" MaxValue="1000"  OnTextChanged="Properties_OnTextChange" NumberFormat-DecimalDigits="0" AutoPostBack="true" runat="server" /></td>
                            
                        </tr>       
                        
                        <tr id="PropertiesRow_SelectionDirection" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Direction</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesSelectionDirection"  Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server">
                                
                                    <Items>
                                    
                                        <Telerik:RadComboBoxItem Text="Horizontal" Value="0" />
                                        
                                        <Telerik:RadComboBoxItem Text="Vertical" Value="1" />
                                                                                
                                    </Items>
                                
                                </Telerik:RadComboBox>
                                
                            </td>                            
                        </tr>
                        
                        <tr id="PropertiesRow_ButtonText" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Text</td>
                                   
                            <td style="border-bottom: solid 1px #CCCCCC;"><Telerik:RadTextBox ID="PropertiesButtonText" Width="95%"  OnTextChanged="Properties_OnTextChange" AutoPostBack="true" runat="server" /></td>
                
                        </tr>

                        <tr id="PropertiesRow_EntityType" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Type</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesEntityType"  Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server">
                                
                                    <Items>
                                    
                                        <Telerik:RadComboBoxItem Text="Not Specified" Value="0" />

                                        <Telerik:RadComboBoxItem Text="Member" Value="1" />
                                        
                                        <Telerik:RadComboBoxItem Text="Provider" Value="2" />
                                                                                
                                    </Items>
                                
                                </Telerik:RadComboBox>
                                
                            </td>                            
                        </tr>

                        
                        <tr id="PropertiesRow_EntityDisplayStyle" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Display Style</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesEntityDisplayStyle"  Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server">
                                
                                    <Items>
                                    
                                        <Telerik:RadComboBoxItem Text="Normal/Expanded" Value="0" Selected="true" />

                                        <Telerik:RadComboBoxItem Text="Name Only" Value="1" />
                                                                                
                                    </Items>
                                
                                </Telerik:RadComboBox>
                                
                            </td>                            
                        </tr>
                        
                        <tr id="PropertiesRow_EntityDisplayAgeFormat" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Age Format</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesEntityDisplayAgeFormat"  Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server">
                                
                                    <Items>
                                    
                                        <Telerik:RadComboBoxItem Text="In Years" Value="0" Selected="true" />

                                        <Telerik:RadComboBoxItem Text="In Months" Value="1" />
                                                                                
                                    </Items>
                                
                                </Telerik:RadComboBox>
                                
                            </td>                            
                        </tr>

                        <tr id="PropertiesRow_EntityAllowCustomEntityName" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Allow Custom Name</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesEntityAllowCustomEntityName"  Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server">
                                
                                    <Items>
                                    
                                        <Telerik:RadComboBoxItem Text="False" Value="False" Selected="true" />

                                        <Telerik:RadComboBoxItem Text="True" Value="True" />
                                                                                
                                    </Items>
                                
                                </Telerik:RadComboBox>
                                
                            </td>                            
                        </tr>
                        
                        <tr id="PropertiesRow_Service" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Service</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;"><Telerik:RadComboBox ID="PropertiesServiceSelection"  Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server"></Telerik:RadComboBox></td>                            
                            
                        </tr>
                        
                        <tr id="PropertiesRow_ServiceDateVisible" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Show Service Date</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesServiceDateVisibleSelection"  Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server">
                                
                                    <Items>
                                    
                                        <Telerik:RadComboBoxItem Text="False" Value="False" />
                                        
                                        <Telerik:RadComboBoxItem Text="True" Value="True" />
                                                                                
                                    </Items>
                                
                                </Telerik:RadComboBox>
                                
                            </td>                            
                            
                        </tr>
                        
                        <tr id="PropertiesRow_ServiceMostRecentDateVisible" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Show Last Date</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesServiceMostRecentDateVisibleSelection"  Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server">
                                
                                    <Items>
                                    
                                        <Telerik:RadComboBoxItem Text="False" Value="False" />
                                        
                                        <Telerik:RadComboBoxItem Text="True" Value="True" />
                                                                                
                                    </Items>
                                
                                </Telerik:RadComboBox>
                                
                            </td>                            
                            
                        </tr>
                                                
                        <tr id="PropertiesRow_Metric" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Metric</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;"><Telerik:RadComboBox ID="PropertiesMetricSelection"  Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server"></Telerik:RadComboBox></td>                            
                            
                        </tr>

                        
                        <tr id="PropertiesRow_LabelText" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Label</td>
                                   
                            <td style="border-bottom: solid 1px #CCCCCC;"><Telerik:RadTextBox ID="PropertiesLabelText" Width="95%"  OnTextChanged="Properties_OnTextChange" AutoPostBack="true" runat="server" /></td>
                
                        </tr>
                                                    
                        <tr id="PropertiesRow_LabelVisible" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Show Label</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesLabelVisible"  Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server">
                                
                                    <Items>
                                    
                                        <Telerik:RadComboBoxItem Text="False" Value="False" />
                                        
                                        <Telerik:RadComboBoxItem Text="True" Value="True" />
                                                                                
                                    </Items>
                                
                                </Telerik:RadComboBox>
                                
                            </td>                            
                            
                        </tr>
                
                        <tr id="PropertiesRow_LabelPosition" style="border-bottom: solid 1px;" runat="server">
                        
                            <td style="border-bottom: solid 1px #CCCCCC;">Label Position</td>
                            
                            <td align="center" style="border-bottom: solid 1px #CCCCCC;">
                            
                                <Telerik:RadComboBox ID="PropertiesLabelPosition"  Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="Properties_OnTextChange" runat="server">
                                
                                    <Items>
                                    
                                        <Telerik:RadComboBoxItem Text="Left" Value="0" />
                                        
                                        <Telerik:RadComboBoxItem Text="Right" Value="1" />

                                        <Telerik:RadComboBoxItem Text="Top" Value="2" />

                                        <Telerik:RadComboBoxItem Text="Bottom" Value="3" />
                                    
                                    </Items>
                                
                                </Telerik:RadComboBox>
                                
                            </td>
                        
                        </tr>                


                    </table>
                
                </div>
                
            </Telerik:RadSlidingPane>
        
        </Telerik:RadSlidingZone>                
            
    </Telerik:RadPane>
        
</Telerik:RadSplitter>
    
    
    <!-- RADEDITOR (BEGIN) -->
    
    <div id="HtmlEditorContainer" style="border: solid 1px black; background-color: White; display: none; position: absolute; left: 0; top: 0; padding: 4px;" runat="server">
        
        <Telerik:RadEditor ID="HtmlEditor"  ContentFilters="RemoveScripts, MakeUrlsAbsolute, FixUlBoldItalic, FixEnclosingP, IECleanAnchors, MozEmStrong, ConvertFontToSpan, ConvertToXhtml, IndentHTMLContent" runat="server">
        
            <Tools>

                <Telerik:EditorToolGroup>
                
                    <Telerik:EditorTool Name="Cut" />
                    <Telerik:EditorTool Name="Copy" />
                    <Telerik:EditorTool Name="Paste" />
                    <Telerik:EditorTool Name="PasteFromWord" />
                    <Telerik:EditorTool Name="PasteStrip" />
                    
                    <Telerik:EditorSeparator />
                    
                    <Telerik:EditorTool Name="Undo" />
                    <Telerik:EditorTool Name="Redo" />
                
                </Telerik:EditorToolGroup>
                
                <Telerik:EditorToolGroup>
            
                    <Telerik:EditorTool Name="FontName" />
                    <Telerik:EditorTool Name="FontSize" />
                    <Telerik:EditorTool Name="ForeColor" />

                    <Telerik:EditorSeparator />

                    <Telerik:EditorTool Name="Bold" />
                    <Telerik:EditorTool Name="Italic" />
                    <Telerik:EditorTool Name="Underline" />
                    <Telerik:EditorTool Name="StrikeThrough" />

                    <Telerik:EditorSeparator />

                    <Telerik:EditorTool Name="Superscript" />
                    <Telerik:EditorTool Name="Subscript" />

                </Telerik:EditorToolGroup>
                
                <Telerik:EditorToolGroup>

                    <Telerik:EditorTool Name="JustifyLeft" />
                    <Telerik:EditorTool Name="JustifyCenter" />
                    <Telerik:EditorTool Name="JustifyRight" />
                    <Telerik:EditorTool Name="JustifyFull" />
                    <Telerik:EditorTool Name="JustifyNone" />
                
                    <Telerik:EditorSeparator />
                    
                    <Telerik:EditorTool Name="Indent" />
                    <Telerik:EditorTool Name="Outdent" />
                    
                    <Telerik:EditorSeparator />
                    
                    <Telerik:EditorTool Name="InsertOrderedList" />
                    <Telerik:EditorTool Name="InsertunorderedList" />
                    
                </Telerik:EditorToolGroup>
            
            </Tools>
         
            <Content></Content>   
            
        </Telerik:RadEditor>
    
        <table id="HtmlEditorCommandButtons" style="width: 0px;">
            <tr>
            
                <td style="width: 100%">&nbsp</td>
                
                <td><asp:Button ID="HtmlEditorCommandButtonsOk" OnClick="HtmlEditorCommandButtonsOk_OnClick" runat="server" style="width: 80px" Text="Ok" /></td>
                
                <td><asp:Button ID="HtmlEditorCommandButtonsCancel" OnClick="HtmlEditorCommandButtonsCancel_OnClick" runat="server" style="width: 80px" Text="Cancel" /></td>

            </tr>
        
        </table>
    
        
    </div>
    
    <!-- RADEDITOR ( END ) -->
       
               
<!-- SELECTION ITEMS (BEGIN) -->   

<Telerik:RadToolTip ID="SelectionItemsDialog" Title="Selection Items" OnClientHide="OnRequestServerRefresh" Width="600" Animation="Fade" Position="Center" RelativeTo="BrowserWindow" ManualClose="true"  runat="server">

    <div style="width: 600px; height: 300px; overflow: hidden">

    <Telerik:RadGrid ID="SelectionItemsGrid" Width="600" 
    
        OnNeedDataSource="SelectionItemsGrid_OnNeedDataSource"
        
        OnItemCommand="SelectionItemsGrid_OnItemCommand"
    
        OnUpdateCommand="SelectionItemsGrid_UpdateCommand "
    
        AutoGenerateColumns="false" 
        
         runat="server">
    
        <MasterTableView Width="600" DataKeyNames="ItemText,ItemValue,ItemEnabled,ItemSelected" CommandItemDisplay="Top" EditMode="InPlace" TableLayout="Fixed" >
         
            <CommandItemTemplate>
            
                <div style="padding: 6px;">
            
                <asp:LinkButton ID="SelectionItemsAddItem" CommandName="AddItem" runat="server"><img src="/Images/Common16/Add.png" alt="Add Item" style="border: none; padding-right: 8px;" />Add Item</asp:LinkButton>
                
                <asp:LinkButton ID="SelectionItemsMoveItemUp" CommandName="MoveItemUp" runat="server"><img src="/Images/Common16/ArrowGreenUp.png" alt="Move Item Up" style="border: none; padding-left: 16px; padding-right: 8px;" />Move Item Up</asp:LinkButton>
                
                <asp:LinkButton ID="SelectionItemsMoveItemDown" CommandName="MoveItemDown" runat="server"><img src="/Images/Common16/ArrowGreenDown.png" alt="Move Item Down" style="border: none; padding-left: 16px; padding-right: 8px;" />Move Item Down</asp:LinkButton>
                
                </div>            
            
            </CommandItemTemplate>
        
            <Columns>

                <Telerik:GridEditCommandColumn HeaderText="&nbsp" ButtonType="LinkButton" UniqueName="EditCommandColumn" />
                            
                <Telerik:GridBoundColumn DataField="ItemText" HeaderText="Text" />
                
                <Telerik:GridBoundColumn DataField="ItemValue" HeaderText="Value" />
                
                <Telerik:GridCheckBoxColumn DataField="ItemEnabled" HeaderText="Enabled" ItemStyle-HorizontalAlign="Center" ReadOnly="false" />
                
                <Telerik:GridCheckBoxColumn DataField="ItemSelected" HeaderText="Selected" ItemStyle-HorizontalAlign="Center" ReadOnly="false" />
            
                <Telerik:GridButtonColumn HeaderText="&nbsp" CommandName="Delete" ButtonType="LinkButton" ConfirmText="Are you sure you want to delete this item?" Text="Delete" />                                            
                                        
            </Columns>        
        
        </MasterTableView>
        
        <ClientSettings AllowRowsDragDrop="false">
        
            <Selecting AllowRowSelect="true" />
            
            <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" ScrollHeight="245" />
            
        </ClientSettings>
    
    </Telerik:RadGrid>

    </div>

</Telerik:RadToolTip>    

<!-- SELECTION ITEMS ( END ) --> 


<Telerik:RadToolTip ID="OpenFormDialog" Title="Open Form" Width="600" Animation="Fade" Position="Center" RelativeTo="BrowserWindow" ManualClose="true"  runat="server">

    <FormDesignerControl:OpenDialog Id="FormDesignerControlOpenDialog" runat="server" />

</Telerik:RadToolTip> 
           
<div id="TelerikWindows" style="display: none;">

    <Telerik:RadWindowManager ID="TelerikWindowManager"  runat="server">
    
        <Windows>
        
            <Telerik:RadWindow ID="DialogEventHandlerEditor" Width="600" Height="100" Modal="true" OnClientClose="EventHandlerEditorDialog_OnClientClose"  ReloadOnShow="true" ShowContentDuringLoad="true" NavigateUrl="/WindowLoading.aspx"  VisibleOnPageLoad="false" VisibleStatusbar="false"  runat="server" />

        </Windows>
    
    </Telerik:RadWindowManager>
    
            <!-- ClientCallBackFunction="EventHandlerEditorDialog_Callback" -->

</div>


</form>

</body>

</html>


