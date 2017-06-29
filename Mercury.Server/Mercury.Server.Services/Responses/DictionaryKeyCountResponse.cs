using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses {

    [DataContract]
    public class DictionaryKeyCountResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Dictionary")]
        private Dictionary<String, Int64> dictionary = new Dictionary<String, Int64> ();

        #endregion


        #region Public Properties

        public Dictionary<String, Int64> Dictionary { get { return dictionary; } set { dictionary = value; } } // Property: Result

        #endregion

    }

}

