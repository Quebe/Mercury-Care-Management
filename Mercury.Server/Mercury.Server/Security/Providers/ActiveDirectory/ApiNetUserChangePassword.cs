using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Security.Providers.ActiveDirectory.ApiNetUserChangePassword {

    public enum ResultCode {

        Success = 0,

        AccessDenied = 5,

        InvalidPassword = 86,

        NoDomainAccess = 1351,

        AccountLockedOut = 1909,

        UserNotFound = 2221,

        NotPrimary = 2226,

        PasswordRestriction = 2245,

    }

}
