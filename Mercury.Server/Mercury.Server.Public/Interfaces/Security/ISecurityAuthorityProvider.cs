using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Public.Interfaces.Security {

    public interface IProvider {

        #region Properties

        Capabilities Capabilities { get; }

        Credentials Credentials { get; set; }

        Exception LastException { get; }

        #endregion


        #region Methods

        Boolean Authenticate ();

        Boolean Authenticate (Credentials userCredentials);

        Dictionary<String, String> GetSecurityGroupDictionary ();

        SecurityGroup GetSecurityGroup (String securityGroupId);

        List<DirectoryEntry> GetSecurityGroupMembership (String securityGroupId);

        List<DirectoryEntry> BrowseDirectory (String directoryPath);

        #endregion

    }

}
