using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Mercury.Server.Services.Responses {

    [DataContract]
    public class StringListResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "ResultList")]
        private List<String> resultList = new List<String> ();

        #endregion


        #region Public Properties

        public List<String> ResultList { get { return resultList; } set { resultList = value; } } // Property: ResultList

        #endregion

    }

}