using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "ReportingServerCollectionResponse")]
    public class ReportingServerCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Reporting.ReportingServer> collection = new List<Server.Reporting.ReportingServer> ();

        #endregion


        #region Public Properties

        public List<Server.Reporting.ReportingServer> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}