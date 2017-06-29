using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Public.Faxing {

    [Serializable]
    public class FaxSender {

        #region Private Properties

        private String name = String.Empty;

        private String company = String.Empty;

        private String department = String.Empty;

        private String faxNumber = String.Empty;

        private String voiceNumber = String.Empty;

        private String email = String.Empty;

        #endregion


        #region Public Properties

        public String Name { get { return name; } set { name = value; } }

        public String Company { get { return company; } set { company = value; } }

        public String Department { get { return department; } set { department = value; } }

        public String FaxNumber { get { return faxNumber; } set { faxNumber = value; } }

        public String VoiceNumber { get { return voiceNumber; } set { voiceNumber = value; } }

        public String Email { get { return email; } set { email = value; } }

        #endregion


        #region Constructors

        public FaxSender () { /* DO NOTHING */ }

        public FaxSender (String forSenderName, String forCompany, String forEmail) {

            name = forSenderName;

            company = forCompany;

            email = forEmail;

            return;

        }

        #endregion

    }

}
