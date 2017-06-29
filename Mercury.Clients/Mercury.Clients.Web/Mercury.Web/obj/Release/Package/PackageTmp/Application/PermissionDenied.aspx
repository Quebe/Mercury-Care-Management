<%@ Page Title="" Language="C#" MasterPageFile="~/Application/Application.Master" AutoEventWireup="true" CodeBehind="PermissionDenied.aspx.cs" Inherits="Mercury.Web.Application.PermissionDenied" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ApplicationContentControl" runat="server">

<div>

    Permission Denied
    
</div>
    
<div id="DebugInformationSection" style="font-family: Arial; line-height: 150%" runat="server">
    
    <br />
    
    <asp:Literal ID="DebugInformation" runat="server"></asp:Literal>
    
</div>

</asp:Content>
