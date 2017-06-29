using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Data.Enumerations {

    // Summary:
    //     Operator used in FilterDescriptor class

    public enum DataFilterOperator {
        // Summary:
        //     Left operand must be smaller than the right one
        IsLessThan = 0,
        //
        // Summary:
        //     Left operand must be smaller than or equal to the right one
        IsLessThanOrEqualTo = 1,
        //
        // Summary:
        //     Left operand must be equal to the right one
        IsEqualTo = 2,
        //
        // Summary:
        //     Left operand must be different from the right one
        IsNotEqualTo = 3,
        //
        // Summary:
        //     Left operand must be larger than the right one
        IsGreaterThanOrEqualTo = 4,
        //
        // Summary:
        //     Left operand must be larger than or equal to the right one
        IsGreaterThan = 5,
        //
        // Summary:
        //     Left operand must start with the right one
        StartsWith = 6,
        //
        // Summary:
        //     Left operand must end with the right one
        EndsWith = 7,
        //
        // Summary:
        //     Left operand must contain the right one
        Contains = 8,
        //
        // Summary:
        //     Left operand must be contained in the right one
        IsContainedIn = 9,

        IsNotContainedIn = 10

    }

}
