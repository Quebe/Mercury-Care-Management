using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Core.Enumerations {


    public enum EntityAddressType {

        NotSpecified = 0,

        PhysicalAddress = 1,

        MailingAddress = 31,

        ServiceLocation = 77,

        AlternatePhysicalAddress = 101,

        CorrectedPhysicalAddress = 201,

        AlternateMailingAddress = 131,

        CorrectedMailingAddress = 231

    }

}
