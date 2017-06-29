using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Public.Faxing {
    
    [Serializable]
    public class FaxCompletedEventArgs : EventArgs {

        
        #region Private Properties

        private String uniqueId = String.Empty;
        
        private Boolean successful = false;

        private String exceptionMessage = String.Empty;

        #endregion 


        #region Public Properties

        public String UniqueId { get { return uniqueId; } set { uniqueId = value; } }

        public Boolean Successful { get { return successful; } set { successful = value; } }

        public String ExceptionMessage { get { return exceptionMessage; } set { exceptionMessage = value; } }

        #endregion


        #region Constructors

        public FaxCompletedEventArgs (String forUniqueId) {

            uniqueId = forUniqueId;

            successful = true;

            exceptionMessage = String.Empty;

            return;

        }

        public FaxCompletedEventArgs (String forUniqueId, String forExceptionMessage) { 

            uniqueId = forUniqueId;

            successful = (String.IsNullOrWhiteSpace (forExceptionMessage));

            exceptionMessage = forExceptionMessage;

            return;

        }

        #endregion

    }

}
