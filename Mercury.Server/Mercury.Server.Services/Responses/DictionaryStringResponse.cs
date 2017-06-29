using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses {

    [DataContract]
    public class DictionaryStringResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Dictionary")]
        private Dictionary<String, String> dictionary = new Dictionary<String, String> ();

        #endregion


        #region Public Properties

        public Dictionary<String, String> Dictionary { get { return dictionary; } set { dictionary = value; } } // Property: Result

        #endregion

    }

}

