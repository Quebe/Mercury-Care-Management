using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses {

    public class DataExplorerNodeExecutionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "ObjectType")]
        private String objectType = String.Empty;

        [DataMember (Name = "Collection")]
        private List<Int64> collection = new List<Int64> ();

        [DataMember (Name = "CompileMessages")]
        private List<String> compileMessages = new List<String> ();

        [DataMember (Name = "RowCount")]
        private Int32 rowCount = 0;

        #endregion


        #region Public Properties

        public String ObjectType { get { return objectType; } set { objectType = value; } }

        public List<Int64> Collection { get { return collection; } set { collection = value; } }

        public List<String> CompileMessages { get { return compileMessages; } set { compileMessages = value; } }

        public Int32 RowCount { get { return rowCount; } set { rowCount = value; } }

        #endregion
    }

}