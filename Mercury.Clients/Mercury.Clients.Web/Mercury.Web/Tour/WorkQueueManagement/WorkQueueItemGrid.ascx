<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkQueueItemGrid.ascx.cs" Inherits="Mercury.Web.Tour.WorkQueueManagement.WorkQueueItemGrid" %>

<div class="TextNormal" style="color: Black; margin: 10px; width: 500px;">

    <div>

        <p style="text-decoration: underline;">Work Management - Work Queue Items</p>
        
        <p>The grid contains a list of Work Queue Items that match the filter criteria.</p>

        <p>You sort the grid by clicking on the column header. The default state of a column is unsorted, the 
        
        first sort is ascending, and the final sort is descending. The sorted column will have a gray 
        
        background. Only one column can be sorted at a time. </p>

        <p>The grid supports paging of Work Queue Items so that only a small amount (default 10 items)
        
        are displayed at a time. When more are available, you will have a Pager Control like below 
        
        that allows you to go to "First", "Previous", "Specific Page", "Next", or "Last". In addition, 
        
        it provides a total count and the number of pages available.</p>


        <table width="100%"><tr><td align="center"><img src="/Tour/Workspace/Images/MyAssignedWorkGridPager.png" /></td></tr></table>


    </div>
    
</div>
