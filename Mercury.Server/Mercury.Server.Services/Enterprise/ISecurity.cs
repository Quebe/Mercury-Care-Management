using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Mercury.Server.Services.Responses;

namespace Mercury.Server.Services.Enterprise {

    [ServiceContract]
    public interface ISecurity {

        [OperationContract]
        DictionaryResponse SecurityAuthorityDictionary ();

        [OperationContract]
        AuthenticationResponse Authenticate (String authorityName, String accountType, String accountName, String password, String newPassword, String environment);

        [OperationContract]
        AuthenticationResponse AuthenticateWindows (String environment);

        [OperationContract]
        AuthenticationResponse AuthenticateToken (String securityAuthority, String token, String environment);

        [OperationContract]
        BooleanResponse LogOff (String token); 

    }

}
