using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Public.Interfaces.Security.Enumerations {

    public enum ApiWin32Error {

        WrongPassword = 1323, // Unable to update the password. The value provided as the current password is incorrect.

        IllFormedPassword = 1324, // Unable to update the password. The value provided for the new password contains values that are not allowed in passwords.

        PasswordRestriction = 1325, // Unable to update the password. The value provided for the new password does not meet the length, complexity, or history requirement of the domain.


        UnknownNameOrBadPassword = 1326,  // Logon failure: unknown user name or bad password.

        AccountRestriction = 1327, // Logon failure: user account restriction. Possible reasons are blank passwords not allowed, logon hour restrictions, or a policy restriction has been enforced.

        InvalidLogOnHours = 1328, // Logon failure: account logon time restriction violation.

        InvalidWorkStation = 1329, // Logon failure: user not allowed to log on to this computer.

        PasswordExpired = 1330, // Logon failure: the specified account password has expired.

        AccountDisabled = 1331, // Logon failure: account currently disabled.

        AccountExpired = 1793, // The user's account has expired.

        MustChangePassword = 1907,  // The user's password must be changed before logging on the first time.

        AccountLockedOut = 1909 // The referenced account is currently locked out and may not be logged on to.

    }


}
