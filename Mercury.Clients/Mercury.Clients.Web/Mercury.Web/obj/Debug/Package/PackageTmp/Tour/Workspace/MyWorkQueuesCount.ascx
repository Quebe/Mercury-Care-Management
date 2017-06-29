<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyWorkQueuesCount.ascx.cs" Inherits="Mercury.Web.Tour.Workspace.MyWorkQueuesCount" %>

<div class="TextNormal" style="color: Black; margin: 10px; width: 350px;">

    <div>

        <p style="text-decoration: underline;">My Work Queues - Work Queue Items Count</p>
        
        <p>This count will show the number items in the selected Work Queue in which:</p>

        <ul>
        
            <li>Available is the number of Open items available through the Get Work 
            
            that have not be assigned to another user, meet the Constraint Date criteria,

            and meet the Work Time Restrictions criteria.
            
            </li>

            <li>Open is the total items in the Work Queue that have not been completed
            
            and need to be worked. Only items that can be worked at this moment through 
            
            Get Work will be included in the "Available" item count.</li>
        
        </ul>

        <p>* This count is only updated as you interact with the system (e.g. change 
        
        Work Queues). Other users may be working items out of the same Work Queue which
        
        will not update the count real-time. Any message from "Get Work" is more valid than the count. </p>

    </div>
    
</div>
