using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Security.Providers.ActiveDirectory.ApiLogonUser {

    enum LogonType {

        None, Batch, Interactive, Network, NetworkClearText, NewCredentials, Service, Unlock

    }

    enum LogonProvider {

        Default, WinNT50, WinNT40, WinNT35

    }

}
