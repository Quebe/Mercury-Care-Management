using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Data.Enumerations {
    // Summary:
    //     Enumeration of logical operators for filter collections

    [DataContract (Name = "DataFilterDescriptorLogicalOperator")]
    public enum FilterDescriptorLogicalOperator {
        // Summary:
        //     Filters are AND'ed
        And = 0,
        //
        // Summary:
        //     Filters are OR'ed
        Or = 1,

    }
}
