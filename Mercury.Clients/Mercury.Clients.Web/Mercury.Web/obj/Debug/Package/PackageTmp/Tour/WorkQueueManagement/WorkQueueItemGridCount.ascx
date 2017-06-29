<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkQueueItemGridCount.ascx.cs" Inherits="Mercury.Web.Tour.WorkQueueManagement.WorkQueueItemGridCount" %>

<div class="TextNormal" style="color: Black; margin: 10px; width: 350px;">

    <div>

        <p style="text-decoration: underline;">Work Queue Management - Work Queue Items Count</p>
        
        <p>This count will show the number items in the selected Work Queue in which:</p>

        <ul>
        
            <li>Filtered is the number of items that matched the filter criteria you have 

            selected and are visible in the Work Queue Item Results grid below.
            
            </li>

            <li>Total is the number of items in the Work Queue - independent of status
            
            or other properties.</li>
        
        </ul>

        <p>* This count is only updated as you interact with the system (e.g. change 
        
        Work Queues). Other users may be working items out of the same Work Queue which
        
        will not update the count real-time. You can refresh this manually by clicking "(refresh)".</p>

    </div>
    
</div>
