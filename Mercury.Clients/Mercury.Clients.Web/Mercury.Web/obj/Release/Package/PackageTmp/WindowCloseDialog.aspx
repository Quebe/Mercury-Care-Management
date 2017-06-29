<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WindowCloseDialog.aspx.cs" Inherits="Mercury.Web.WindowClose" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>

    <script type="text/javascript" >
    
        var windowManager = parent.GetRadWindowManager ();
    
        var radWindow = windowManager.GetWindowByName ("DialogWindow");
    
        radWindow.Close ();
       
    </script>

</body>
</html>
