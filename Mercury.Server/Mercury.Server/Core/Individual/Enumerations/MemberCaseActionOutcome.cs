using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Core.Individual.Enumerations {

    public enum MemberCaseActionOutcome { 
        
        ValidationError = -2, UnknownError = -1,

        Success, 
        
        NotFoundError, PermissionDenied, ModifiedError, LockedError, NotAssignedToError,  // 1-5
        
        NoChangeDetectedError, PermissionDeniedCaseStatus, ProblemStatementExists, CannotRemoveLast, MemberCaseCarePlanGoalInterventionExists,  // 6-10
    
        DuplicateFound // 11

    }

}
