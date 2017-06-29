using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses {

    public class ImportExportResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Result")]
        private List<Objects.ImportExportResult> results = new List<Objects.ImportExportResult> ();

        #endregion


        #region Public Properties

        public List<Objects.ImportExportResult> Results { get { return results; } set { results = value; } }

        #endregion


        #region Constructors

        public ImportExportResponse () { return; }

        public ImportExportResponse (List<Server.ImportExport.Result> forResults) {

            results = new List<Objects.ImportExportResult> ();

            foreach (Server.ImportExport.Result currentResult in forResults) {

                results.Add (new Objects.ImportExportResult (currentResult)); 

            }

            return;

        }

        #endregion 

    }

}