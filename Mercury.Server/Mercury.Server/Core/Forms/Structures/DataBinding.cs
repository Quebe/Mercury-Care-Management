using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Forms.Structures {

    [Serializable]
    [DataContract (Name = "FormControlDataBinding")]
    public class DataBinding {

        #region Private Properties

        [DataMember (Name = "BoundProperty")]
        private String boundProperty = String.Empty;

        [DataMember (Name = "DataBindingType")]
        private Mercury.Server.Core.Forms.Enumerations.FormControlDataBindingType dataBindingType = Mercury.Server.Core.Forms.Enumerations.FormControlDataBindingType.Control;

        [DataMember (Name = "DataSourceControlId")]
        private Guid dataSourceControlId = Guid.Empty;

        [DataMember (Name = "BindingContext")]
        private String bindingContext = String.Empty;

        #endregion


        #region Public Properties

        public String BoundProperty { get { return boundProperty; } set { boundProperty = value; } }

        public Mercury.Server.Core.Forms.Enumerations.FormControlDataBindingType DataBindingType { get { return dataBindingType; } set { dataBindingType = value; } }

        public Guid DataSourceControlId { get { return dataSourceControlId; } set { dataSourceControlId = value; } }

        public String BindingContext { get { return bindingContext; } set { bindingContext = value; } }

        #endregion

    }

}
