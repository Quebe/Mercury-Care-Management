using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses {

    [DataContract]
    public class DictionaryResponse : ResponseBase {
        
        #region Private Properties

        [DataMember (Name = "Dictionary")]
        private Dictionary<Int64, String> dictionary = new Dictionary<Int64, String> ();

        #endregion


        #region Public Properties

        public Dictionary<Int64, String> Dictionary { get { return dictionary; } set { dictionary = value; } } // Property: Result

        #endregion

    }

}

