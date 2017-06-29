using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Data {

    [Serializable]
    [DataContract (Name = "AuthorityAccountStamp")]
    public class AuthorityAccountStamp {

        #region Private Properties

        [DataMember (Name = "SecurityAuthorityName")]
        private String authorityName = String.Empty;

        [DataMember (Name = "UserAccountId")]
        private String accountId = String.Empty;

        [DataMember (Name = "UserAccountName")]
        private String accountName = String.Empty;

        [DataMember (Name = "ActionDate")]
        private DateTime actionDate = DateTime.Now;

        #endregion


        #region Public Properties

        public String SecurityAuthorityName { get { return authorityName; } set { authorityName = CommonFunctions.SetValueMaxLength (value, DataTypeConstants.Name); } }

        public String UserAccountId { get { return accountId; } set { accountId = CommonFunctions.SetValueMaxLength (value, DataTypeConstants.Name); } }

        public String UserAccountName { get { return accountName; } set { accountName = CommonFunctions.SetValueMaxLength (value, DataTypeConstants.Name); } }

        public DateTime ActionDate { get { return actionDate; } set { actionDate = value; } }


        public String SecurityAuthorityNameSql { get { return ((!String.IsNullOrEmpty (authorityName)) ? authorityName.Trim ().Replace ("'", "''") : String.Empty); } }

        public String UserAccountIdSql { get { return ((!String.IsNullOrEmpty (accountId)) ? accountId.Trim ().Replace ("'", "''") : String.Empty); } }

        public String UserAccountNameSql { get { return ((!String.IsNullOrEmpty (accountName)) ? accountName.Trim ().Replace ("'", "''") : String.Empty); } }

        public String AccountInfoSql { get { return "'" + SecurityAuthorityNameSql + "', '" + UserAccountIdSql + "', '" + UserAccountNameSql + "'"; } }

        #endregion


        #region Constructor

        public AuthorityAccountStamp () { return; }

        public AuthorityAccountStamp (String authorityName, String accountId, String accountName, DateTime actionDate) {

            SecurityAuthorityName = authorityName;

            UserAccountId = accountId;

            UserAccountName = accountName;

            ActionDate = actionDate;

            return;

        }

        public AuthorityAccountStamp (Session session) {

            SecurityAuthorityName = session.SecurityAuthorityName;

            UserAccountId = session.UserAccountId;

            UserAccountName = session.UserAccountName;

            ActionDate = DateTime.Now;

            return;

        }

        public AuthorityAccountStamp (Application application) {

            SecurityAuthorityName = application.Session.SecurityAuthorityName;

            UserAccountId = application.Session.UserAccountId;

            UserAccountName = application.Session.UserAccountName;

            ActionDate = DateTime.Now;

            return;

        }

        public AuthorityAccountStamp (System.Data.DataRow currentRow, String prefix) {

            MapDataFields (currentRow, prefix);

            return;

        }

        #endregion


        #region Database Functions 

        public void MapDataFields (System.Data.DataRow currentRow, String prefix) {

            try {

                authorityName = (String) currentRow[prefix + "AuthorityName"];
                accountId = (String) currentRow[prefix + "AccountId"];
                accountName = (String) currentRow[prefix + "AccountName"];
                actionDate = (DateTime) currentRow[prefix + "Date"];

            }

            catch { /* do nothing */ }

        }

        #endregion

    }

}
