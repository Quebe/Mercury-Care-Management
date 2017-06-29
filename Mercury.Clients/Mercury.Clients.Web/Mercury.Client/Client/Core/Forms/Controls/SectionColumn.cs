using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Forms.Controls {

    [Serializable]
    public class SectionColumn : Mercury.Client.Core.Forms.Control {

        #region Constructors

        protected void InitializeControl (Application applicationReference) {

            BaseConstructor (applicationReference);

            ControlType = Mercury.Server.Application.FormControlType.SectionColumn;

            return;

        }

        public SectionColumn (Application applicationReference) {

            InitializeControl (applicationReference);

            return;

        }

        public SectionColumn (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Mercury.Server.Application.FormControlSectionColumn serverControl) {

            BaseConstructor (applicationReference, parentControl, serverControl);

            InitializeControl (applicationReference);

            ChildServerControlsToLocal (this, serverControl);

            return;

        }

        #endregion

        public override Boolean AllowChildControl (Mercury.Server.Application.FormControlType childControlType) {

            if (childControlType == Mercury.Server.Application.FormControlType.Undefined) { return false; }

            if (childControlType == Mercury.Server.Application.FormControlType.Form) { return false; }

            return true;

        }

    }

}
