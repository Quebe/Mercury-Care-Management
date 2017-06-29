using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Forms.Structures {

    [Serializable]
    [DataContract (Name = "FormControlCapabilities")]
    public class Capabilities {

        #region Private Properties

        [DataMember (Name = "HasValue")]
        private Boolean hasValue = false;

        [DataMember (Name = "HasLabel")]
        private Boolean hasLabel = false;

        [DataMember (Name = "IsDataSource")]
        private Boolean isDataSource = false;

        [DataMember (Name = "CanDataBind")]
        private Boolean canDataBind = false;

        [DataMember (Name = "HasMultipleDataBindingPoints")]
        private Boolean hasMultipleDataBindingPoints = false;

        #endregion


        #region Public Properties

        public Boolean HasValue { get { return hasValue; } set { hasValue = value; } }

        public Boolean HasLabel { get { return hasLabel; } set { hasLabel = value; } }

        public Boolean IsDataSource { get { return isDataSource; } set { isDataSource = value; } }

        public Boolean CanDataBind { get { return canDataBind; } set { canDataBind = value; } }

        public Boolean HasMultipleDataBindingPoints { get { return hasMultipleDataBindingPoints; } set { hasMultipleDataBindingPoints = value; } }

        #endregion 

    }

}

