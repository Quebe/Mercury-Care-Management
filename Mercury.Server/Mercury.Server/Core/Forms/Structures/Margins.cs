using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Forms.Structures {

    [Serializable]
    [DataContract (Name = "FormControlMargins")]
    public class Margins {

        #region Private Properties

        [DataMember (Name = "Left")]
        private float left;

        [DataMember (Name = "Top")]
        private float top;

        [DataMember (Name = "Right")]
        private float right;

        [DataMember (Name = "Bottom")]
        private float bottom;

        #endregion


        #region Public Properties

        public float Left { get { return left; } set { left = value; } }

        public float Top { get { return top; } set { top = value; } }

        public float Right { get { return right; } set { right = value; } }

        public float Bottom { get { return bottom; } set { bottom = value; } }

        #endregion

    }
}
