﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Core.Action.Enumerations {

    public enum ActionThresholdType { 
        
        NotSpecified, 
        
        CarePlanGoalObjective, 
        
        CarePlanGoalIntervention, 
        
        MemberCarePlanGoalObjective, 
        
        MemberCarePlanGoalIntervention,

        CareLevelActivity
    
    }

}