using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Core.MedicalServices.Enumerations {

    public enum EventDateOrder { 
        
        ClaimFromDate, 
        
        ClaimThruDate, 
        
        ServiceClaimFromDate, 
        
        ServiceClaimThruDate, 
        
        AdmissionClaimFromDate, 
        
        DischargeClaimThruDate,

        ServiceAdmissionClaimFromDate,

        ServiceDischargeClaimThrueDate
    
    }

}
