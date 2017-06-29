<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkQueueItemGridColumns2.ascx.cs" Inherits="Mercury.Web.Tour.WorkQueueManagement.WorkQueueItemGridColumns2" %>

<div class="TextNormal" style="color: Black; margin: 10px; width: 500px;">

    <div>

        <p style="text-decoration: underline;">Work Queue Management - Work Queue Items</p>
        
        <p>Assigned To: The name of the user the Item is currently assigned to, which prevents

        the Work Queue Item from being available through "Get Work".</p>
        
        <p>Action Column: A list of actions that can be performed on the Work Queue Item depending
        
        on the state of the item and the permissions granted to you.</p>

        <ul>
        
            <li>Assign: This will allow you to change the assignment of an item or to move the item between

            Work Queues.</li>
        
            <li>Close: This will close the Work Queue Item and set the Completion Date to today. You must have
            
            "Manage" permissions on the Work Queue or the Workflow is a Manual workflow to close it.</li> 

        </ul>

    </div>
    
</div>
