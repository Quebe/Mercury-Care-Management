<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkQueueMonitorAgingChart.ascx.cs" Inherits="Mercury.Web.Tour.WorkQueueMonitor.WorkQueueMonitorAgingChart" %>

<div class="TextNormal" style="color: Black; margin: 10px; width: 350px;">

    <div>

        <p style="text-decoration: underline;">Work Queue Monitor - Item Aging Chart</p>
        
        <p>The chart provides a bar graph for items in the Work Queue that are Open and further breaks 
        
        out a separate bar for those open items that are Available to be worked today. For each group, 
        
        the left bar is the Available Items in that category and the right bar is the Open items in that 
        
        category. </p>
        
        <p>The graph is relative to the due date of the individual item. The counts represent the 
        
        number of items due within that time period.</p>

    </div>
    
</div>

