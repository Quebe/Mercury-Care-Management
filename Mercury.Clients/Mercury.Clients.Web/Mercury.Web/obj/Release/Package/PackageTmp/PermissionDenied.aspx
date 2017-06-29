<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PermissionDenied.aspx.cs" Inherits="Mercury.Web.PermissionDenied" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Permission Denied</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Permission Denied
    
    </div>
    
    <div id="DebugInformationSection" style="font-family: Arial; line-height: 150%" runat="server">
    
        <br />
    
        <asp:Literal ID="DebugInformation" runat="server"></asp:Literal>
    
    </div>
    
    </form>
</body>
</html>
