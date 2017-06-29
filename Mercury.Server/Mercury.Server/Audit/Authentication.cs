using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Audit {

    [DataContract (Name = "AuditAuthentication")]
    public class Authentication {

        #region Private Properties

        [DataMember (Name = "SessionToken")]
        private Guid sessionToken = Guid.Empty;

        [DataMember (Name = "LogonDate")]
        private DateTime logonDate;

        [DataMember (Name = "LogoffDate")]
        private DateTime? logoffDate = null;

        [DataMember (Name = "AuthenticationTime")]
        private Int32 authenticationTime = 0;

        [DataMember (Name = "LastActivityTime")]
        private DateTime lastActivityTime;

        [DataMember (Name = "EnvironmentId")]
        private Int64 environmentId;

        [DataMember (Name = "SecurityAuthorityId")]
        private Int64 securityAuthorityId;

        [DataMember (Name = "UserAccountId")]
        private String userAccountId;

        [DataMember (Name = "UserAccountName")]
        private String userAccountName;

        [DataMember (Name = "UserDisplayName")]
        private String userDisplayName = String.Empty;

        #endregion 


        #region Public Properties

        #endregion


        #region Constructors

        #endregion 


        #region Database Functions

        public void MapDataFields (System.Data.DataRow currentRow) {

            sessionToken = (Guid) currentRow["SessionToken"];

            logonDate = (DateTime) currentRow["LogonDateTime"];

            logoffDate = (currentRow["LogoffDateTime"] is DBNull) ? (DateTime?) null : (DateTime) currentRow["LogoffDateTime"];

            authenticationTime = (Int32) currentRow["AuthenticationTime"];

            lastActivityTime = (DateTime) currentRow["LastActivityDateTime"];

            environmentId = (Int64) currentRow["EnvironmentId"];

            securityAuthorityId = (Int64) currentRow["SecurityAuthorityId"];

            userAccountId = (String) currentRow["UserAccountId"];

            userAccountName = (String) currentRow["UserAccountName"];

            userDisplayName = (String) currentRow["UserDisplayName"];

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        #endregion

    }

}
