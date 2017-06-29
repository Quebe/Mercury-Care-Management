using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "PrinterCollectionResponse")]
    public class PrinterCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Printing.Printer> collection = new List<Server.Printing.Printer> ();

        #endregion


        #region Public Properties

        public List<Server.Printing.Printer> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}