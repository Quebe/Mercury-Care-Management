<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkQueueMonitorSummary.ascx.cs" Inherits="Mercury.Web.Tour.WorkQueueMonitor.WorkQueueMonitorSummary" %>

<div class="TextNormal" style="color: Black; margin: 10px; width: 350px;">

    <div>

        <p style="text-decoration: underline;">Work Queue Monitor - Summary</p>
        
        <p>The grid will only contain Work Queues in which you have the manage permission; they are sorted alphabetically.
        
        The refresh link will update the results in the grid. </p>
        
        <ul>
        
            <li>Select: Clicking the select link will set the Aging Chart to the selected Work Queue.</li>

            <li>First: The First Time in the day a Work Queue Item was worked out of the Work Queue.</li>

            <li>Last: The Last Time in the day a Work Queue Item was worked out of the Work Queue</li>

            <li>Worked: Number of Items worked out of the Work Queue for the day.</li>

            <li>Completed: Number of Worked Item that were Completed out of the Work Queue for the day.</li>

            <li>Available: Number of Items that are available to be worked today.</li>

            <li>Open: Total number of open items in the Work Queue.</li>

            <li>Warning: Number of items that have passed their threshold and are in a Warning state.</li>

            <li>Overdue: Number of items that have passed their due dates and are in a Critical state.</li>

            <li>Users: Number of users who have actively worked an item out of the Work Queue in the last
            
            30 minutes. A User will only be in a single Work Queue in which they most recently worked 
            
            an item.</li>

        </ul>  

    </div>
    
</div>

