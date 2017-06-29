using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Public.Faxing {

    public interface IFaxServer {

        #region Public Events

        event EventHandler<FaxCompletedEventArgs> OnFaxCompleted;

        #endregion 


        #region Public Methods

        void Fax (FaxServerConfiguration faxServerConfiguration, FaxSender sender, FaxRecipient recipient, FaxDocument document, Dictionary<String, String> extendedProperties);

        #endregion 

    }

}
