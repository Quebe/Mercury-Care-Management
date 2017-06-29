using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Public.Faxing {

    [Serializable]
    public class FaxRecipient {

        #region Private Properties

        private String recipientName = String.Empty;

        private String companyName = String.Empty;

        private String departmentName = String.Empty;

        private String recipientEmail = String.Empty;

        private String faxNumber = String.Empty;

        #endregion 


        #region Public Properties

        public String RecipientName { get { return recipientName; } set { recipientName = value; } }

        public String CompanyName { get { return companyName; } set { companyName = value; } }

        public String DepartmentName { get { return departmentName; } set { departmentName = value; } }

        public String RecipientEmail { get { return recipientEmail; } set { recipientEmail = value; } }

        public String FaxNumber { get { return faxNumber; } set { faxNumber = value; } }
            
        #endregion 


        #region Constructors

        public FaxRecipient () { /* DO NOTHING */ }

        public FaxRecipient (String forRecipientName, String forFaxNumber) {

            recipientName = forRecipientName;

            faxNumber = forFaxNumber;

            return;

        }

        #endregion

    }

}
