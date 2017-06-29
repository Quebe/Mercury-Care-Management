<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SecurityAuthorityDelete.aspx.cs" Inherits="Mercury.Web.Application.Enterprise.Windows.SecurityAuthorityDelete" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head id="Head1" runat="server">

    <title>Untitled Page</title>
    
</head>

<body>

    <form id="SecurityAuthorityDelete" runat="server">
    
    <table cellpadding="0" cellspacing="0" style="width: 100%;"><tr><td align="center" valign="middle">

    <div style="width: 300px; vertical-align: middle; text-align: center; overflow: hidden; font-family: Arial; font-size: 10pt; line-height: 150%; text-align: left;">

        
        <div>
            <span><img src="/Images/Common32/ServerEnvironmentRemove.png" style="float: left; height:32px; margin: 10px 10px" alt="" /></span>
            <span style="line-height: 200%; float: right">
                Are you sure that you want to remove this Security Authority: 
            </span>
        </div>

                    <br style="clear:both" />
        
                    <div style="height: 15px"><span></span></div>
                    
        <div style="width: 100%; font-weight: bold">
                <asp:Label ID="UiSecurityAuthorityName" runat="server" Text="No Security Authority" />
        </div>            

        <div style="width: 100%; font-weight: bold; color: Red">
            <asp:Label ID="UiExceptionMessage" runat="server" Text="Not Committed." Visible="false" />
        </div>                
        
        <p style="line-height: 200%">
            ** This has no affect on user accounts or security groups on the actual Security Authority, 
            and will only remove the configuration from the Enterprise Database.    
        </p>

        <div style="height: 30px; padding: 0px 10px 0px 10px;">
        
            <span style="float: right;"></span>

            <span style="float: right;"><asp:Button ID="ButtonCancel" Text="Cancel" Width="73px" Font-Names="Arial" Font-Size="10pt" runat="Server" /></span>

            <span style="float: right; width: 10px">&nbsp</span>

            <span style="float: right;"><asp:Button ID="ButtonOk" Text="Ok" Width="73px" Font-Names="Arial" Font-Size="10pt" runat="Server" /></span>

        </div>    
    
    </div>
    
    </td></tr></table>
    
    </form>
    
</body>

</html>
