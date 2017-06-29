<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkQueueFilters.ascx.cs" Inherits="Mercury.Web.Tour.WorkQueueManagement.WorkQueueFilters" %>


<div class="TextNormal" style="color: Black; margin: 10px; width: 450px;">

    <div>

        <p style="text-decoration: underline;">Work Queue Management - Work Queue Filters</p>
        
        <p>Clicking the drop-down box provides access to basic filtering of the Work Queue
        
        Items by a few of the key data elements. Filtering is enabled or disabled with the 
        
        checkboxes, which support three states.</p>
        
        
        <table>
        
            <tr>

                <td>Checkbox States: </td>

                <td style="padding-left: .125in;"><img src="/Tour/WorkQueueManagement/Images/Checkbox_Checked.png" alt="Checked" /> Checked (True)</td>

                <td style="padding-left: .125in;"><img src="/Tour/WorkQueueManagement/Images/Checkbox_Unchecked.png" alt="Unchecked" /> Uncheck (False)</td>

                <td style="padding-left: .125in;"><img src="/Tour/WorkQueueManagement/Images/Checkbox_Indeterminate.png" alt="Indeterminate" /> Indeterminate (does not matter or not used)</td>
                
            </tr>
        
        </table>

        <p>Example:</p>

        <ul>
        
            <li><img src="/Tour/WorkQueueManagement/Images/Checkbox_Checked.png" alt="Checked" /> Is Assigned: Only show items that have been assigned to a user.</li>
        
            <li><img src="/Tour/WorkQueueManagement/Images/Checkbox_Unchecked.png" alt="Unchecked" /> Is Assigned: Only show items that have not been assigned to a user.</li>

            <li><img src="/Tour/WorkQueueManagement/Images/Checkbox_Indeterminate.png" alt="Indeterminate" /> Is Assigned: Show items independent of assignment state.</li>

        </ul>

        <p>* Both the Work Queue Item Name filter and the Assigned to User Display Name filter 
        
        require that you enter 
        
        the criteria before clicking the checkbox. Changing the criteria might require
        
        cylcing the checkbox value to force a refresh, or you can click "Refresh" for  
        
        a manual refresh request.</p>
        
    </div>
    
</div>

