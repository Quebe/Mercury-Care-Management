using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Core.Enumerations {

    // THIS ENUMERATION IS DUPLICATED IN THE DATABASE (ContactType TABLE)
    // ANY UPDATES MADE HERE MUST BE ADDED TO THE DATABASE

    public enum EntityContactType { 
        
        NotSpecified, 
        
        Telephone, 
        
        Facsimile, 
        
        Email, 
        
        InPerson, 
        
        ByMail, 
        
        EmergencyPhone, 
        
        Mobile, 
        
        Pager, 
        
        AlternateTelephone,
    
        AlternateFacsimile
    
    }

}
