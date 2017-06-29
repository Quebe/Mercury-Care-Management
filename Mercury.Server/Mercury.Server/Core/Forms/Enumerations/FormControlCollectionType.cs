using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Forms.Enumerations {
    
    // MUST ALWAYS APPEND TO MAINTAIN BACKWARDS COMPATIBILITY

    public enum FormControlCollectionType { 
        
        NotSpecified, 
        
        MemberEnrollment, 
        
        MemberEnrollmentCoverage, 
        
        MemberEnrollmentPcp, 
        
        EntityAddress, 
        
        EntityContactInformation,

        PopulationMembership,

        PopulationMembershipServiceEvent,

        MemberService,

        ProviderContract,

        MemberRelationship
    
    }

}
