<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormViewer.aspx.cs" Inherits="Mercury.Web.Application.Forms.FormViewer.FormViewer" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">

    <title></title>
    
</head>

<body>

<form id="form1" runat="server">

<div>


<div id="AjaxManagerDiv" style="display: none">

    <asp:ScriptManager ID="AjaxScriptManager" runat="server"></asp:ScriptManager>
    
    <Telerik:RadAjaxManager ID="TelerikAjaxManager" DefaultLoadingPanelID="AjaxLoadingPanelWhiteout" runat="server"></Telerik:RadAjaxManager>
    
</div>    
    
    
<div style="display: none"><asp:TextBox ID="FormInstanceId" Text="" runat="server" /></div>    
    
<table cellpadding="0" cellspacing="0" style="width: 100%; background-color: Silver;" border="0"><tr><td align="center" valign="top">

<div id="FormContent" style="width: 8in; min-height: 1in; text-align: left; margin: 8px; padding: 4px; border: solid 1px black; background-color: White; overflow: hidden;" runat="server">
                        

</div>

</td></tr></table>


</div>

</form>
    
</body>

</html>

