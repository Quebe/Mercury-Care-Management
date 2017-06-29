<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkQueueItemGridSenders.ascx.cs" Inherits="Mercury.Web.Tour.WorkQueueManagement.WorkQueueItemGridSenders" %>

<div class="TextNormal" style="color: Black; margin: 10px; width: 400px;">

    <div>

        <p style="text-decoration: underline;">Work Queue Management - Work Queue Items</p>
        
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

        <p>* This is the end of the tour.</p>

    </div>
    
</div>
