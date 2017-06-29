<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyAssignedWorkGridColumns2.ascx.cs" Inherits="Mercury.Web.Tour.Workspace.MyAssignedWorkGridColumns2" %>

<div class="TextNormal" style="color: Black; margin: 10px; width: 500px;">

    <div>

        <p style="text-decoration: underline;">My Assigned Work - Work Queue Items</p>
        
        <p>Workflow Column: The name of the Workflow for the Work Queue Item with the ability to
        
        "start" or "resume" the Workflow. Work Queues with Manual workflows will be empty.</p>
        
        <p>Action Column: A list of actions that can be performed on the Work Queue Item depending
        
        on the state of the item and the permissions granted to you.</p>

        <ul>
        
            <li>Suspend: This will set the Work Queue Item to not assigned and place it back into the Work Queue.</li>
        
            <li>Close: This will close the Work Queue Item and set the Completion Date to today. You must have
            
            "Manage" permissions on the Work Queue or the Workflow is a Manual workflow to close it.</li> 

        </ul>

    </div>
    
</div>
