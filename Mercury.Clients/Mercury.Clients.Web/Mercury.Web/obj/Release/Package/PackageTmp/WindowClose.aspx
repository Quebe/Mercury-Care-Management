<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WindowClose.aspx.cs" Inherits="Mercury.Web.WindowClose" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">

    <title>Untitled Page</title>
    
</head>

<body>

    <script type="text/javascript" >

        if ((window.parent != null) && (window.opener != null)) { 
        
            try { 
        
                window.opener.ChildWindow_OnClose (event); 
                
            }
            
            catch (CloseEventError) { 
            
                // DO NOTHING
            
            }
            
        }
            
        window.close ();
        
    </script>

</body>

</html>
