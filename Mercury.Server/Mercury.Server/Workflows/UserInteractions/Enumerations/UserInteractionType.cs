using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Workflows.UserInteractions.Enumerations {

    public enum UserInteractionType {

        NotSpecified, 
        
        Exception, 
        
        Prompt, 
        
        UserInterfaceUpdate, 
        
        RequireEntity, 
        
        RequireForm, 
        
        ContactEntity, 
        
        SendCorrespondence, 
        
        CreateMemberCarePlan, 
        
        CreateModifyMemberCase, 
        
        OpenImage

    }

}
