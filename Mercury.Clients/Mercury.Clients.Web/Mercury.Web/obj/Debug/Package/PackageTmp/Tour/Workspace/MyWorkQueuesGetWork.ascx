<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyWorkQueuesGetWork.ascx.cs" Inherits="Mercury.Web.Tour.Workspace.MyWorkQueuesGetWork" %>

<div class="TextNormal" style="color: Black; margin: 10px; min-width: 400px; max-width: 400px;">

    <div>

        <p style="text-decoration: underline;">My Work Queues - Get Work</p>
        
        <p>Clicking this link will get the next available, unassigned Work Queue Item 
        
        from the selected Work Queue and start the associated Workflow (if there 
        
        is a Workflow assigned to the Work Queue).</p>

        <p>Work Queues without an associated Workflow will have "(Manual)" as 
        
        the "Get Work" action. These are business processes that are conducted offline (externally)
        
        from the Mercury system. When you click Get Work for a Manual Work Queue, the 
        
        Work Queue Item is simply assigned to you and available under "My Assigned Work." 
        
        You will complete the Work Queue Item through the external process, and then, you click 
        
        "close" on the Item under "My Assigned Work" to select a Work Outcome and close the Item.</p>

        <p>Using "Get Work" to work a Work Queue prevents any contention or concurrency issues 
        
        with multiple users working the same Work Queue.</p>

        <p>* Other users may be working items out of the same Work Queue which will not update the 
        
        count real-time. Any message from "Get Work" is more valid than the count. </p>

    </div>
    
</div>
