<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyAssignedWorkGridColumns.ascx.cs" Inherits="Mercury.Web.Tour.Workspace.MyAssignedWorkGridColumns" %>

<div class="TextNormal" style="color: Black; margin: 10px; width: 500px;">

    <div>

        <p style="text-decoration: underline;">My Assigned Work - Work Queue Items</p>
        
        <p>Status Column: The current status of the Work Queue Item, clicking "(detail)" will open 
        
        the Work Queue Item detail page in a new window.</p>
        
        <ul>
            
            <li><img src="../../Images/Common16/Warning.png" alt="Warning" /> Warning: The item has passed a
            
            Milestone Date or the Threshold Date and is nearing the Due Date.</li>
            
            <li><img src="../../Images/Common16/Critical.png" alt="Critical" /> Critical: The item has passed
            
            the Due Date and is overdue.</li>
            
        </ul>

        <p>Name Column: The name of the Work Queue Item. This can be a link that will open a new window 

        (e.g. a Work Queue Item for a Member will open the Member's Profile page).</p>

        
        
        <p>Date Columns: </p>
        <ul>
        
            <li>Added: The date the item was added to the Work Queue.</li>
        
        <li>Constraint: The date that must pass before the item is available through "Get Work".</li> 

        <li>Worked: The date the item was last worked. </li>

        <li>Due: The date the item is due to be completed.</li>

        </ul>


    </div>
    
</div>
