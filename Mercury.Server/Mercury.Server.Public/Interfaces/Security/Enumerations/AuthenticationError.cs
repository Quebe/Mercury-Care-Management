using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Public.Interfaces.Security.Enumerations {

    public enum AuthenticationError {

        NoError,

        InvalidUserOrPassword,

        MustChangePassword,

        CannotChangePassword,

        PasswordExpired,

        AccountLockedDisabledExpired,

        PasswordRestriction,

        MustSelectEnvironment,

        SecurityAuthorityError,

        UnableToCreateSession,

        OtherUndefinedError


    }

}


