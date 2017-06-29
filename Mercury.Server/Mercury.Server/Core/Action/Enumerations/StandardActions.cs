using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Core.Action.Enumerations {

    public enum StandardActions { 
        
        NotSpecified, 
        
        Workflow, 
        
        AddToWorkQueue, 
        
        RouteByRules, 
        
        SendCorrespondence, 
        
        SendCorrespondenceMember, 
        
        SendCorrespondenceProvider, 
        
        SendCorrespondenceSponsor, 
        
        SendCorrespondenceInsurer,

        SendCorrespondenceMemberAndRoute,

        RemoveFromWorkQueue
    
    }

}
