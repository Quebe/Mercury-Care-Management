using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Core.Claims.Enumerations {

    public enum ClaimStatus { Void = -1, NotSpecified, Open, Pend, ReadyToDeny, ReadyToPay, Denied, Paid, Reversed }
    
}
