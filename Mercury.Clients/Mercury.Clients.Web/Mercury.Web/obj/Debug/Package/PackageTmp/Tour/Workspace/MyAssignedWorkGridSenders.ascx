<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyAssignedWorkGridSenders.ascx.cs" Inherits="Mercury.Web.Tour.Workspace.MyAssignedWorkGridSenders" %>

<div class="TextNormal" style="color: Black; margin: 10px; width: 400px;">

    <div>

        <p style="text-decoration: underline;">My Assigned Work - Work Queue Items</p>
        
        <p>Work Queue Items can be “Expanded” to see the Senders (or reasons) why the Item was 
        
        added to the Work Queue. Click the arrow in the left margin of the row for the 
        
        Work Queue Item that you would like to see the Senders to expand it. Clicking again will
        
        collapse the row back to its original state.</p>
        
        <table width="100%">
        
            <tr><td align="center">Collapsed</td><td align="center">Expanded</td></tr>

            <tr>

                <td align="center"><img src="/Tour/Workspace/Images/MyAssignedWorkGridCollapsed.png" alt="Collapsed Item" /></td>

                <td align="center"><img src="/Tour/Workspace/Images/MyAssignedWorkGridExpanded.png" alt="Expanded Item" /></td>
            
            </tr>
                    
        </table>

    </div>
    
</div>
