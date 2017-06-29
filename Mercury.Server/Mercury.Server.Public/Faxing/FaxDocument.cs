using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Public.Faxing {

    [Serializable]
    public class FaxDocument {

        #region Private Properties

        private String uniqueId = String.Empty;
        
        private String subject = String.Empty;

        private ImageStream attachment = null;

        #endregion 


        #region Public Properties

        public String UniqueId { get { return uniqueId; } set { uniqueId = value; } }

        public String Subject { get { return subject; } set { subject = value; } }

        public ImageStream Attachment { get { return attachment; } set { attachment = value; } }


        #endregion


        #region Constructors

        public FaxDocument () { /* DO NOTHING */ }

        public FaxDocument (String forUniqueId, String forSubject, ImageStream forAttachment) {

            uniqueId = forUniqueId;

            subject = forSubject;

            attachment = forAttachment;

            return;

        }

        #endregion

    }

}
