<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Introduction.ascx.cs" Inherits="Mercury.Web.Tour.Introduction" %>


<div class="TextNormal" style="color: Black; margin: 10px; max-width: 350px;">

    <div>

        <p>Welcome to the Mercury Care Management System.</p>

        
        <p>This is the Application Title Bar, which will contain your Login Display Name 
        
        and the environment to which you are connected. Clicking your name will open a
        
        new window to display your session information - useful for diagnosing problems, 

        viewing team membership, and permissions. </p>


        <p>This tour will provide you with an overview for navigation.</p>

    </div>
    
    <div id="PageSpecificTourNotAvailable" runat="server">
    
        <p>No page specific tour exists for this page. 
        
        Only the overview is available at this time.</p>
    
    </div>

    <div id="PageSpecificTourContainer" style="display: none;" runat="server">

        <asp:LinkButton id="PageSpecificTour" OnClick="PageSpecificTour_OnClick" runat="server">Click here to skip to the page specific tour.</asp:LinkButton>

    </div>
        
</div>


